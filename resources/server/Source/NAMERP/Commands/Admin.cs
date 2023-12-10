using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;

using Npgsql;

namespace NAMERP.Commands
{
    internal class Admin : IScript
    {
        [Command("test")]
        public static void CMD_TEST(Player.Data player, double fuel_tank, double fuel_consumption)
        {
            if (player.Admin < 5)
            {
                return;
            }

            if (!player.IsInVehicle)
            {
                return;
            }

            NpgsqlCommand cmd = new("INSERT INTO vehicle_configuration (model, fuel_tank, fuel_consumption) VALUES (@model, @fuelTank, @fuelConsumption)");
            cmd.Parameters.AddWithValue("@model", (int)player.Vehicle.Model);
            cmd.Parameters.AddWithValue("@fuelTank", fuel_tank);
            cmd.Parameters.AddWithValue("@fuelConsumption", fuel_consumption);
            try
            {
                Database.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Alt.LogError(ex.ToString());
                player.SendChatMessage(ChatMessageType.Error, null, "Ein Fehler ist aufgetreten! (Schau in der Konsole)");
            }
        }

        [Command("fuelveh")]
        public static void CMD_FUELVEH(Player.Data player, double value)
        {
            if (player.Admin < 5)
            {
                return;
            }

            IVehicle veh = player.Vehicle;
            if (veh == null)
            {
                return;
            }

            veh.GetSyncedMetaData("fuel", out double fuel);
            veh.GetSyncedMetaData("fuel_tank", out double fuelTank);
            if (fuel + value > fuelTank)
            {
                player.SendChatMessage(ChatMessageType.Error, null, $"Diese Menge passt nicht in das aktuelle Fahrzeug rein! Getankt: {fuel}, Kapazität: {fuelTank}");
                return;
            }
            veh.SetSyncedMetaData("fuel", fuel + value);
        }

        [Command("tempveh")]
        public static void CMD_TEMPVEH(Player.Data player, string model)
        {
            if (player.Admin < 5)
            {
                return;
            }

            Alt.CreateVehicle(Alt.Hash(model), player.Position, player.Rotation);
        }

        [Command("veh")]
        public static void CMD_VEH(Player.Data player, string model)
        {
            if (player.Admin < 5)
            {
                return;
            }

            if (player.Character == null)
            {
                player.Kick("Hoppla!");
                return;
            }

            long vehId = Vehicle.DatabaseHelper.CreateVehicle(player.Character.ID, (int)Alt.Hash(model), player.Position, player.Rotation);
            if (vehId == 0)
            {
                return;
            }

            Vehicle.Data? veh = Vehicle.DatabaseHelper.LoadVehicle(vehId);
            if (veh != null)
            {
                veh.ID = vehId;
                veh.Owner = player.Character.ID;

                veh.SetSyncedMetaData("id", veh.ID);
            }
        }

        [Command("delveh")]
        public static void CMD_DELVEH(Player.Data player, uint id)
        {
            if (player.Admin < 5)
            {
                return;
            }

            Vehicle.Data? veh = (Vehicle.Data?)Alt.GetVehicleById(id);
            if (veh == null)
            {
                return;
            }
            Vehicle.Helper.DeleteVehicle(veh, true);
        }

        [Command("tempdelveh")]
        public static void CMD_TEMPDELVEH(Player.Data player, uint id)
        {
            if (player.Admin < 1)
            {
                return;
            }

            Vehicle.Data? veh = (Vehicle.Data?)Alt.GetVehicleById(id);
            if (veh == null)
            {
                return;
            }
            Vehicle.Helper.DeleteVehicle(veh, false);
        }

        /* ===== TELEPORT ===== */

        [Command("tp")]
        public static void CMD_TP(Player.Data player, uint id)
        {
            if (player.Admin < 1)
            {
                return;
            }

            Player.Data target = (Player.Data)Alt.GetPlayerById(id);
            if (target == null)
            {
                player.SendChatMessage(ChatMessageType.Error, null, $"Es ist kein Spieler mit der ID ({id}) online!");
                return;
            }

            player.Position = target.Position;
        }

        [Command("tph")]
        public static void CMD_TPH(Player.Data player, uint id)
        {
            if (player.Admin < 1)
            {
                return;
            }

            Player.Data target = (Player.Data)Alt.GetPlayerById(id);
            if (target == null)
            {
                player.SendChatMessage(ChatMessageType.Error, null, $"Es ist kein Spieler mit der ID ({id}) online!");
                return;
            }

            target.Position = player.Position;
        }

        [Command("tpc")]
        public static void CMD_TPC(Player.Data player, int posX, int posY, int posZ)
        {
            if (player.Admin < 1)
            {
                return;
            }

            player.Position = new(posX, posY, posZ);
        }

        /* ===== OTHERS ===== */

        [Command("setadmin")]
        public static void CMD_SETADMIN(Player.Data player, long targetId, int level)
        {
            if (player.Admin < 1337)
            {
                return;
            }

            NpgsqlCommand cmd = new("UPDATE accounts SET admin = @adminLevel FROM characters WHERE characters.id = @characterId AND characters.account_id = accounts.id");
            cmd.Parameters.AddWithValue("@characterId", targetId);
            cmd.Parameters.AddWithValue("@adminLevel", level);
            try
            {
                Database.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Alt.LogError(ex.ToString());
            }

            Player.Data? target = Alt.GetAllPlayers().Cast<Player.Data?>().FirstOrDefault((el) => el?.Character?.ID == targetId);
            if (target != null)
            {
                target.Admin = level;
                target.SendChatMessage(ChatMessageType.Info, null, $"Du hast Admin {level} erhalten!");
            }

            player.SendChatMessage(ChatMessageType.Info, null, $"Du hast dem Spieler mit der Char.ID {targetId} Admin {level} gegeben.");
        }

        [Command("weapon")]
        public static void CMD_WEAPON(Player.Data player, uint targetId, string model, int ammo = 0)
        {
            if (player.Admin < 4)
            {
                return;
            }

            IPlayer target = Alt.GetPlayerById(targetId);
            if (target == null)
            {
                player.SendChatMessage(ChatMessageType.Error, null, $"Spieler mit der dynamischen \"{targetId}\" nicht gefunden!");
                return;
            }

            target.GiveWeapon(Alt.Hash(model), ammo, true);
        }

        [Command("notification")]
        public static void CMD_TEST(Player.Data player, string text)
        {
            if (player.Admin < 5)
            {
                return;
            }

            player.Emit("notification:show", "CHAR_YOUTUBE", "Header", "Small details", text);
        }
    }
}
