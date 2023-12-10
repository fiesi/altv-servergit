using System.Data;
using System.Text.Json.Serialization;

using AltV.Net.Elements.Entities;

using Npgsql;

namespace NAMERP.Character
{
    internal partial class Clothing
    {
        [JsonPropertyName("hair")]
        public int Hair { get; set; } = 0;
        [JsonPropertyName("hairColorPrimary")]
        public int HairColorPrimary { get; set; } = 0;
        [JsonPropertyName("hairColorSecondary")]
        public int HairColorSecondary { get; set; } = 0;
    }

    internal partial class Clothing
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="charId"></param>
        /// <exception cref="Exception"></exception>
        public void Create(ref NpgsqlConnection conn, long charId)
        {
            NpgsqlCommand cmd = new("SELECT count(1) FROM character_clothing WHERE character_id = @character_id LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@character_id", charId);
            long? alreadyExist = (long?)cmd.ExecuteScalar();
            if (alreadyExist == null || alreadyExist > 1)
            {
                throw new Exception("This character already has clothing data");
            }

            cmd = new("INSERT INTO character_clothing (character_id, hair, hair_color_primary, hair_color_secondary) VALUES (@character_id, @hair, @hair_color_primary, @hair_color_secondary)", conn);
            cmd.Parameters.AddWithValue("@character_id", charId);
            cmd.Parameters.AddWithValue("@hair", Hair);
            cmd.Parameters.AddWithValue("@hair_color_primary", HairColorPrimary);
            cmd.Parameters.AddWithValue("@hair_color_secondary", HairColorSecondary);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="charId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Load(ref NpgsqlConnection conn, long charId)
        {
            bool result = false;

            NpgsqlCommand cmd = new("SELECT hair, hair_color_primary, hair_color_secondary FROM character_clothing WHERE character_id = @charId LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@charId", charId);
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();

                Hair = rdr.GetInt32("hair");
                HairColorPrimary = rdr.GetInt32("hair_color_primary");
                HairColorSecondary = rdr.GetInt32("hair_color_secondary");

                result = true;
            }
            rdr.Close();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        public void Reset(IPlayer player)
        {
            player.SetClothes(2, (ushort)Hair, 0, 0);
            player.HairColor = (byte)HairColorPrimary;
            player.HairHighlightColor = (byte)HairColorSecondary;
        }
    }
}
