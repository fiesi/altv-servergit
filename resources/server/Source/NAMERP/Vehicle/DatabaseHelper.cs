using System.Data;

using AltV.Net;
using AltV.Net.Data;

using Npgsql;

namespace NAMERP.Vehicle
{
    internal static class DatabaseHelper
    {
        public static Data[] GetAllExistVehicles(this Character.Data pChar)
        {
            return Alt.GetAllVehicles().Cast<Data>().Where((el) => el.ID != 0 && el.Owner == pChar.ID).ToArray();
        }

        public static void LoadAllVehicles(this Character.Data pChar)
        {
            NpgsqlConnection conn = Database.GetInstance().GetConnection();
            NpgsqlCommand cmd = new("SELECT v.*, c.fuel_tank, c.fuel_consumption FROM vehicles v JOIN vehicle_configuration c ON c.model = v.model WHERE owner = @ownerId", conn);
            cmd.Parameters.AddWithValue("@ownerId", pChar.ID);
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Position pos = new((float)rdr.GetDouble("pos_x"), (float)rdr.GetDouble("pos_y"), (float)rdr.GetDouble("pos_z"));
                    Rotation rot = new((float)rdr.GetDouble("rot_r"), (float)rdr.GetDouble("rot_p"), (float)rdr.GetDouble("rot_y"));
                    Data? veh = Helper.CreateVehicle((uint)rdr.GetInt32("model"), pos, rot);
                    if (veh == null)
                    {
                        continue;
                    }

                    veh.ID = rdr.GetInt64("id");
                    veh.Owner = rdr.GetInt64("owner");

                    veh.BodyHealth = (uint)rdr.GetInt32("body_health");
                    veh.EngineHealth = rdr.GetInt32("engine_health");

                    veh.SetSyncedMetaData("id", veh.ID);
                    veh.SetSyncedMetaData("fuel", rdr.GetDouble("fuel"));
                    veh.SetSyncedMetaData("fuel_tank", rdr.GetDouble("fuel_tank"));
                    veh.SetSyncedMetaData("fuel_consumption", rdr.GetDouble("fuel_consumption"));
                }
            }
            rdr.Close();
            Database.GetInstance().FreeConnection(conn);
        }

        public static Data? LoadVehicle(long id)
        {
            Data? result = null;

            NpgsqlConnection conn = Database.GetInstance().GetConnection();
            NpgsqlCommand cmd = new("SELECT v.*, c.fuel_tank, c.fuel_consumption FROM vehicles v JOIN vehicle_configuration c ON c.model = v.model WHERE v.id = @id LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();

                Position pos = new((float)rdr.GetDouble("pos_x"), (float)rdr.GetDouble("pos_y"), (float)rdr.GetDouble("pos_z"));
                Rotation rot = new((float)rdr.GetDouble("rot_r"), (float)rdr.GetDouble("rot_p"), (float)rdr.GetDouble("rot_y"));
                result = Helper.CreateVehicle((uint)rdr.GetInt32("model"), pos, rot);
                if (result != null)
                {
                    result.ID = rdr.GetInt64("id");
                    result.Owner = rdr.GetInt64("owner");

                    result.BodyHealth = (uint)rdr.GetInt32("body_health");
                    result.EngineHealth = rdr.GetInt32("engine_health");

                    result.SetSyncedMetaData("id", result.ID);
                    result.SetSyncedMetaData("fuel", rdr.GetDouble("fuel"));
                    result.SetSyncedMetaData("fuel_tank", rdr.GetDouble("fuel_tank"));
                    result.SetSyncedMetaData("fuel_consumption", rdr.GetDouble("fuel_consumption"));
                }
            }
            rdr.Close();
            Database.GetInstance().FreeConnection(conn);

            return result;
        }

        public static void Save(this Data veh)
        {
            NpgsqlConnection conn = Database.GetInstance().GetConnection();
            NpgsqlCommand cmd = new("UPDATE vehicles SET body_health = @bodyHealth, engine_health = @engineHealth, pos_x = @posX, pos_y = @posY, pos_z = @posZ, rot_r = @rotR, rot_p = @rotP, rot_y = @rotY, fuel = @fuel WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@id", veh.ID);
            cmd.Parameters.AddWithValue("@bodyHealth", (int)veh.BodyHealth);
            cmd.Parameters.AddWithValue("@engineHealth", veh.EngineHealth);
            Position pos = veh.Position;
            cmd.Parameters.AddWithValue("@posX", pos.X);
            cmd.Parameters.AddWithValue("@posY", pos.Y);
            cmd.Parameters.AddWithValue("@posZ", pos.Z);
            Rotation rot = veh.Rotation;
            cmd.Parameters.AddWithValue("@rotR", rot.Roll);
            cmd.Parameters.AddWithValue("@rotP", rot.Pitch);
            cmd.Parameters.AddWithValue("@rotY", rot.Yaw);
            veh.GetSyncedMetaData("fuel", out double fuel);
            cmd.Parameters.AddWithValue("@fuel", fuel);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Alt.LogError(ex.ToString());
            }

            Database.GetInstance().FreeConnection(conn);
        }

        public static long CreateVehicle(long owner, int model, Position pos, Rotation rot)
        {
            NpgsqlConnection conn = Database.GetInstance().GetConnection();
            NpgsqlCommand cmd = new("INSERT INTO vehicles (owner, model, pos_x, pos_y, pos_z, rot_r, rot_p, rot_y) VALUES (@owner, @model, @posX, @posY, @posZ, @rotR, @rotP, @rotY) RETURNING id", conn);
            cmd.Parameters.AddWithValue("@owner", owner);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@posX", pos.X);
            cmd.Parameters.AddWithValue("@posY", pos.Y);
            cmd.Parameters.AddWithValue("@posZ", pos.Z);
            cmd.Parameters.AddWithValue("@rotR", rot.Roll);
            cmd.Parameters.AddWithValue("@rotP", rot.Pitch);
            cmd.Parameters.AddWithValue("@rotY", rot.Yaw);

            long result = 0;
            try
            {
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    result = rdr.GetInt64("id");
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Alt.LogError(ex.ToString());
            }
            finally
            {
                Database.GetInstance().FreeConnection(conn);
            }
        
            return result;
        }

        public static void DeleteVehicle(long id)
        {
            NpgsqlConnection conn = Database.GetInstance().GetConnection();
            NpgsqlCommand cmd = new("DELETE FROM vehicles WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Alt.LogError(ex.ToString());
            }
            finally
            {
                Database.GetInstance().FreeConnection(conn);
            }
        }
    }
}
