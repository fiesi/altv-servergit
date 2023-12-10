using System.Data;
using System.Text.Json.Serialization;
using System.Text.Json;

using AltV.Net;
using AltV.Net.Data;

using Npgsql;

namespace NAMERP
{
    internal class OAuthResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
        [JsonPropertyName("global_name")]
        public string GlobalName { get; set; } = string.Empty;
    }

    internal class ClientEvents : IScript
    {
        [ClientEvent("login:token")]
        public static async void CE_LOGIN_TOKEN(Player.Data player, string token)
        {
            OAuthResult? discord = null;

            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Authorization = new("Bearer", token);
                using HttpResponseMessage res = await client.GetAsync("https://discordapp.com/api/users/@me");
                HttpContent content = res.Content;
                string data = await content.ReadAsStringAsync();
                discord = JsonSerializer.Deserialize<OAuthResult>(data);
            }
            catch (Exception ex)
            {
                Alt.LogError(ex.ToString());
                player.Emit("login:error", "Ein unbekannter Fehler ist aufgetreten!");
                return;
            }

            if (discord == null)
            {
                player.Emit("login:error", "Dein Account konnte nicht verifiziert werden!");
                return;
            }

            bool alreadyOnline = Alt.GetAllPlayers().Cast<Player.Data>().Any((t) => t.Discord?.Id == discord.Id);
            if (alreadyOnline)
            {
                player.Emit("login:error", "Dieser Discord ist aktuell mit einem Account angemeldet!");
                return;
            }
            player.Discord = discord;

            NpgsqlConnection conn = Database.GetInstance().GetConnection();
            try
            {
                // Erstelle einen neuen Eintrag für diesen Account falls noch keiner existiert.
                NpgsqlCommand cmd = new("INSERT INTO accounts (discord_id) SELECT @discordId WHERE NOT EXISTS (SELECT 1 FROM accounts WHERE discord_id = @discordId)", conn);
                cmd.Parameters.AddWithValue("@discordId", discord.Id);
                if (cmd.ExecuteNonQuery() < 0) // < 0; Irgendetwas ist schiefgelaufen.
                {
                    player.Emit("login:error", "Es konnte kein neuer Account angelegt werden!");
                    throw new Exception("Irgendetwas ist schiefgelaufen.");
                }

                bool loggedIn = false;

                // Erhalte anhand der Discord-ID die benötigten Informationen von diesem Account.
                cmd = new("SELECT id, admin, premium FROM accounts WHERE discord_id = @discordId LIMIT 1", conn);
                cmd.Parameters.AddWithValue("@discordId", discord.Id);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();

                    player.ID = rdr.GetInt32("id");
                    player.Admin = rdr.GetInt32("admin");
                    player.Premium = rdr.GetInt32("premium");

                    loggedIn = true;
                }
                rdr.Close();

                if (loggedIn)
                {
                    player.SetSyncedMetaData("admin", player.Admin);

                    player.Emit("login:destroy");
                    player.CharacterSelectionCreate(ref conn);
                }
                else
                {
                    player.Emit("login:error", "Wir konnten dich nicht anmelden!");
                }
            }
            catch (Exception ex)
            {
                Alt.Log(ex.ToString());
            }
            finally
            {
                Database.GetInstance().FreeConnection(conn);
            }
        }

        [ClientEvent("admin:noclip")]
        public static void CE_ADMIN_NOCLIP(Player.Data player)
        {
            if (player.Admin < 1)
            {
                return;
            }

            if (player.IsInVehicle)
            {
                return;
            }

            player.Visible = !player.Visible;
            player.Frozen = !player.Frozen;
            player.Emit("admin:noclip");
        }

        [ClientEvent("admin:noclip:position")]
        public static void CE_ADMIN_NOCLIP_POSITION(Player.Data player, Position pos)
        {
            if (player.Admin < 1)
            {
                return;
            }

            player.Position = pos;
        }
    }
}
