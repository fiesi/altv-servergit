using System.Data;
using System.Text.Json;

using AltV.Net;

using Npgsql;

namespace NAMERP.Player
{
    internal partial class Data
    {
        /* ===== PUBLIC FIELDS ===== */
        public long ID { get; set; } = 0;
        public int Admin { get; set; } = 0;
        public int Premium { get; set; } = 0;

        /* ===== CLASS FIELDS ===== */
        public OAuthResult? Discord { get; set; } = null;
        public Character.Data? Character { get; set; } = null;
    }

    internal partial class Data : AltV.Net.Elements.Entities.Player
    {
        public Data(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id) { }

        private List<string> GetAllCharacters(ref NpgsqlConnection conn)
        {
            List<string> characters = new();

            NpgsqlCommand cmd = new("SELECT id, firstname, lastname, money, bank FROM characters WHERE account_id = @accountId", conn);
            cmd.Parameters.AddWithValue("@accountId", ID);
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    var character = new
                    {
                        id = rdr.GetInt32("id"),
                        firstname = rdr.GetString("firstname"),
                        lastname = rdr.GetString("lastname"),
                        money = rdr.GetInt32("money"),
                        bank = rdr.GetInt32("bank"),
                    };
                    string? jsonData = JsonSerializer.Serialize(character);
                    if (jsonData != null)
                    {
                        characters.Add(jsonData);
                    }
                }
            }
            rdr.Close();

            return characters;
        }

        public void CharacterSelectionCreate(ref NpgsqlConnection conn)
        {
            List<string> characters = GetAllCharacters(ref conn);
            Emit("character:selection:create", characters);
        }
    }
}
