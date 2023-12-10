using System.Data;
using System.Text.Json.Serialization;

using AltV.Net.Elements.Entities;
using AltV.Net.Enums;

using Npgsql;

namespace NAMERP.Character
{
    internal partial class Customization
    {
        [JsonPropertyName("sex")]
        public int Sex { get; set; } = 0;
        [JsonPropertyName("father")]
        public int Father { get; set; } = 0;
        [JsonPropertyName("fatherSkin")]
        public int FatherSkin { get; set; } = 0;
        [JsonPropertyName("mother")]
        public int Mother { get; set; } = 0;
        [JsonPropertyName("motherSkin")]
        public int MotherSkin { get; set; } = 0;
        [JsonPropertyName("faceMix")]
        public double FaceMix { get; set; } = 0;
        [JsonPropertyName("skinMix")]
        public double SkinMix { get; set; } = 0;
    }

    internal partial class Customization
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="charId"></param>
        public void Create(ref NpgsqlConnection conn, long charId)
        {
            NpgsqlCommand cmd = new("SELECT count(1) FROM character_customization WHERE character_id = @character_id LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@character_id", charId);
            long? alreadyExist = (long?)cmd.ExecuteScalar();
            if (alreadyExist == null || alreadyExist > 1)
            {
                return;
            }

            cmd = new("INSERT INTO character_customization (character_id, sex, father, father_skin, mother, mother_skin, face_mix, skin_mix) VALUES (@character_id, @sex, @father, @father_skin, @mother, @mother_skin, @face_mix, @skin_mix)", conn);
            cmd.Parameters.AddWithValue("@character_id", charId);
            cmd.Parameters.AddWithValue("@sex", Sex);
            cmd.Parameters.AddWithValue("@father", Father);
            cmd.Parameters.AddWithValue("@father_skin", FatherSkin);
            cmd.Parameters.AddWithValue("@mother", Mother);
            cmd.Parameters.AddWithValue("@mother_skin", MotherSkin);
            cmd.Parameters.AddWithValue("@face_mix", FaceMix);
            cmd.Parameters.AddWithValue("@skin_mix", SkinMix);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="charId"></param>
        /// <returns></returns>
        public bool Load(ref NpgsqlConnection conn, long charId)
        {
            bool result = false;

            NpgsqlCommand cmd = new("SELECT sex, father, father_skin, mother, mother_skin, face_mix, skin_mix FROM character_customization WHERE character_id = @character_id LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@character_id", charId);
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();

                Sex = rdr.GetInt32("sex");
                Father = rdr.GetInt32("father");
                FatherSkin = rdr.GetInt32("father_skin");
                Mother = rdr.GetInt32("mother");
                MotherSkin = rdr.GetInt32("mother_skin");
                FaceMix = rdr.GetDouble("face_mix");
                SkinMix = rdr.GetDouble("skin_mix");

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
            player.Model = Sex > 0 ? (uint)PedModel.FreemodeFemale01 : (uint)PedModel.FreemodeMale01;
            player.RemoveHeadBlendData();
            player.SetHeadBlendData((uint)Father, (uint)Mother, 0, (uint)FatherSkin, (uint)MotherSkin, 0, (uint)FaceMix, (uint)SkinMix, 0);
        }
    }
}
