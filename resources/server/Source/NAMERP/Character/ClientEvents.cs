using AltV.Net;

using Npgsql;

namespace NAMERP.Character
{
    internal class ClientEvents : IScript
    {
        [ClientEvent("character:select")]
        public static void CE_CHARACTER_SELECT(Player.Data player, int id)
        {
            NpgsqlConnection conn = Database.GetInstance().GetConnection();
            try
            {
                Data.Select(ref conn, player, id);
            }
            catch (Exception ex)
            {
                Alt.Log(ex.ToString());
                player.Kick("Hoppla!");
            }
            finally
            {
                Database.GetInstance().FreeConnection(conn);
            }
        }

        [ClientEvent("character:create")]
        public static void CE_CHARACTER_CREATE(Player.Data player)
        {
            player.Emit("character:selection:destroy");
            player.Emit("character:creation:create");
        }

        [ClientEvent("character:creation:save")]
        public static void CE_CHARACTER_CREATION_SAVE(
            Player.Data player,
            string firstname,
            string lastname,
            Dictionary<string, object> rawCustomization,
            Dictionary<string, object> rawClothing
        )
        {
            if (firstname.Length < 3 || firstname.Length > 11 || lastname.Length < 3 || lastname.Length > 11)
            {
                return;
            }

            NpgsqlConnection conn = Database.GetInstance().GetConnection();
            try
            {
                Customization? newCustomization = rawCustomization.ToClass<Customization>();
                Clothing? newClothing = rawClothing.ToClass<Clothing>();
                if (newCustomization == null || newClothing == null)
                {
                    throw new Exception("Fehlende Konfiguration oder Klamotten!");
                }


                if (Data.Create(ref conn, player, firstname, lastname, ref newCustomization, ref newClothing))
                {
                    player.Emit("character:creation:destroy");
                    player.CharacterSelectionCreate(ref conn);
                }
            }
            catch (Exception ex)
            {
                Alt.Log(ex.ToString());
                player.Kick("Hoppla!");
            }
            finally
            {
                Database.GetInstance().FreeConnection(conn);
            }
        }

        [ClientEvent("character:creation:cancel")]
        public static void CE_CHARACTER_CREATION_CANCEL(Player.Data player)
        {
            player.Emit("character:creation:destroy");

            NpgsqlConnection conn = Database.GetInstance().GetConnection();
            try
            {
                player.CharacterSelectionCreate(ref conn);
            }
            catch (Exception ex)
            {
                Alt.Log(ex.ToString());
                player.Kick("Hoppla!");
            }
            finally
            {
                Database.GetInstance().FreeConnection(conn);
            }
        }
    }
}
