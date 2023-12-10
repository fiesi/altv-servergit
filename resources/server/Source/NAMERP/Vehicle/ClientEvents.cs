using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;

namespace NAMERP.Vehicle
{
    internal class ClientEvents : IScript
    {
        [ClientEvent("vehicle:update")]
        public static void CE_VEHICLE_UPDATE(Player.Data player, Dictionary<string, object> values)
        {
            if (!player.IsInVehicle)
            {
                return;
            }
            if (player.Seat != 1)
            {
                return;
            }

            Data veh = (Data)player.Vehicle;

            foreach ((string key, object val) in values)
            {
                switch (key)
                {
                    case "fuel":
                        veh.SetSyncedMetaData("fuel", Convert.ToDouble(val));
                        break;
                }
            }
        }

        [ClientEvent("vehicle:engine")]
        public static void CE_VEHICLE_ENGINE(Player.Data player)
        {
            if (!player.IsInVehicle)
            {
                return;
            }
            if (player.Seat != 1)
            {
                return;
            }
            
            Data veh = (Data)player.Vehicle;
            if (!veh.HasKey(player.ID))
            {
                return;
            }

            if (veh.HasSyncedMetaData("fuel"))
            {
                veh.GetSyncedMetaData("fuel", out double fuel);
                if (fuel <= 0)
                {
                    veh.EngineOn = false;
                    return;
                }
            }

            veh.EngineOn = !veh.EngineOn;
        }

        [ClientEvent("vehicle:lock")]
        public static void CE_VEHICLE_LOCK(Player.Data player)
        {
            Data? veh = null;
            
            if (player.IsInVehicle)
            {
                veh = (Data)player.Vehicle;
                if (!veh.HasKey(player.ID))
                {
                    return;
                }
            }
            else
            {
                veh = Alt.GetAllVehicles()
                    .Cast<Data>()
                    .Where((el) => player.Position.Distance(el.Position) < 5f && el.HasKey(player.ID))
                    .OrderBy((el) => player.Position.Distance(el.Position))
                    .FirstOrDefault();
            }

            if (veh != null)
            {
                veh.LockState = veh.LockState == VehicleLockState.Locked ? VehicleLockState.Unlocked : VehicleLockState.Locked;
                IPlayer[] targets = Alt.GetAllPlayers()
                    .Where((el) => el.IsEntityInStreamingRange(veh))
                    .ToArray();
                Alt.EmitClients(targets, "vehicle:lock", veh.Id, 8, 750);

                player.PlayAnimation("anim@mp_player_intmenu@key_fob@", "fob_click", 8.0f, -8.0f, 1500, 48, 0, false, false, false);
            }
        }
    }
}
