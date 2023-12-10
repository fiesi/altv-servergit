using System.Data;

using NAMERP.Vehicle;

using AltV.Net;

using Npgsql;

namespace NAMERP.Character
{
    internal partial class Data
    {
        /* ===== PUBLIC FIELDS ===== */
        public long ID { get; set; } = 0;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public int BankPin { get; set; } = 0;
        public int Wanteds { get; set; } = 0;
        public int Organization { get; set; } = 0;
        public int OrganizationRank { get; set; } = 0;
        public int Family { get; set; } = 0;
        public int FamilyRank { get; set; } = 0;

        public long LastPayday { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        /* ===== PROTECTED FIELDS ===== */
        protected int _money = 0;
        protected int _bank = 0;

        /* ===== CLASS FIELDS ===== */
        public Customization Customization { get; set; } = new();
        public Clothing Clothing { get; set; } = new();
    }

    internal partial class Data
    {
        /* ===== MONEY ===== */
        public int GetMoney() => _money;
        public void SetMoney(int money, bool updateDb = true)
        {
            _money = money;

            if (updateDb)
            {
                NpgsqlCommand cmd = new("UPDATE characters SET money = @money WHERE id = @characterId");
                cmd.Parameters.AddWithValue("@money", _money);
                cmd.Parameters.AddWithValue("@characterId", ID);
                try
                {
                    Database.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    Alt.LogError(ex.Message);
                }
            }
        }
        public void AddMoney(int money) => SetMoney(_money + money);
        public void RemoveMoney(int money) => SetMoney(_money - money);

        /* ===== BANK ===== */
        public int GetBank() => _bank;
        public void SetBank(int money, bool updateDb = true)
        {
            _bank = money;

            if (updateDb)
            {
                NpgsqlCommand cmd = new("UPDATE characters SET bank = @bank WHERE id = @characterId");
                cmd.Parameters.AddWithValue("@bank", _bank);
                cmd.Parameters.AddWithValue("@characterId", ID);
                try
                {
                    Database.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    Alt.LogError(ex.Message);
                }
            }
        }
        public void AddBank(int money, bool updateDb = true) => SetBank(_bank + money, updateDb);
        public void RemoveBank(int money, bool updateDb = true) => SetBank(_bank - money, updateDb);
    }

    internal partial class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public Data() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="player"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="customizationData"></param>
        /// <param name="clothingData"></param>
        /// <returns></returns>
        public static bool Create(
            ref NpgsqlConnection conn, 
            Player.Data player, 
            string firstname, 
            string lastname, 
            ref Customization newCustomization, 
            ref Clothing newClothing
        )
        {
            NpgsqlCommand cmd = new("SELECT count(1) FROM characters WHERE firstname = @firstname AND lastname = @lastname LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            long userFound = (long?)cmd.ExecuteScalar() ?? 0;
            if (userFound > 0)
            {
                player.Emit("character:creation:error", "Dieser Vorname in Kombination mit Nachname ist bereits vergeben!");
                return false;
            }

            cmd = new("INSERT INTO characters (account_id, firstname, lastname) VALUES (@accountId, @firstname, @lastname) RETURNING id", conn);
            cmd.Parameters.AddWithValue("@accountId", player.ID);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            long? newCharId = (long?)cmd.ExecuteScalar();
            if (newCharId == null)
            {
                player.Emit("character:creation:error", "Ein unbekannter Fehler ist aufgetreten!");
                return false;
            }

            newCustomization.Create(ref conn, (long)newCharId);
            newClothing.Create(ref conn, (long)newCharId);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="player"></param>
        /// <param name="charId"></param>
        public static void Select(ref NpgsqlConnection conn, Player.Data player, int charId)
        {
            NpgsqlCommand cmd = new("SELECT id, firstname, lastname, money, bank, bank_pin, wanteds, organization, organization_rank, family, family_rank FROM characters WHERE id = @id AND account_id = @accountId LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@id", charId);
            cmd.Parameters.AddWithValue("@accountId", player.ID);
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();

                player.Character = new()
                {
                    ID = rdr.GetInt32("id"),
                    Firstname = rdr.GetString("firstname"),
                    Lastname = rdr.GetString("lastname"),
                    BankPin = rdr.GetInt32("bank_pin"),
                    Wanteds = rdr.GetInt32("wanteds"),
                    Organization = rdr.GetInt32("organization"),
                    OrganizationRank = rdr.GetInt32("organization_rank"),
                    Family = rdr.GetInt32("family"),
                    FamilyRank = rdr.GetInt32("family_rank"),
                };

                player.Character.SetMoney(rdr.GetInt32("money"), false);
                player.Character.SetBank(rdr.GetInt32("bank"), false);
            }
            rdr.Close();

            if (player.Character == null)
            {
                player.Kick("Dein Charakter konnte nicht ausgewählt werden!");
                return;
            }

            if (!player.Character.Customization.Load(ref conn, player.Character.ID) || !player.Character.Clothing.Load(ref conn, player.Character.ID))
            {
                player.Kick("Ein Fehler beim Versuch deinen Charakter zu laden ist aufgetreten!");
                return;
            }

            player.Character.Customization.Reset(player);
            player.Character.Clothing.Reset(player);

            // Set synced data
            player.SetSyncedMetaData("id", charId);
            player.SetSyncedMetaData("name", $"{player.Character.Firstname} {player.Character.Lastname}");
            player.SetSyncedMetaData("money", player.Character.GetMoney());
            player.SetSyncedMetaData("bank", player.Character.GetBank());
            player.SetSyncedMetaData("wanteds", player.Character.Wanteds);

            // Set ped data
            player.Dimension = 0;
            player.Position = new(436.491f, -982.172f, 30.699f);

            // Restore everything
            player.Emit("character:selection:destroy");
            player.Emit("radar", true);
            player.Emit("freeze", false);
            player.Emit("visible", true);
            player.Emit("camera:destroy");
            player.Emit("hud:create", player.Id, charId);
            player.Emit("chat:create");

            // Load all other stuff
            player.AddToAllVoice();
            player.Character.LoadAllVehicles();
        }
    }
}
