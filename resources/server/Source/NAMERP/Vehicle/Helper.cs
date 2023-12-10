using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;

namespace NAMERP.Vehicle
{
    internal class Helper
    {
        public static Data? CreateVehicle(string model, Position pos, Rotation rot)
        {
            return CreateVehicle(Alt.Hash(model), pos, rot);
        }

        public static Data? CreateVehicle(uint model, Position pos, Rotation rot)
        {
            Data veh = (Data)Alt.CreateVehicle(model, pos, rot);
            if (veh == null)
            {
                return null;
            }

            veh.ManualEngineControl = true;
            veh.EngineOn = false;
            veh.LockState = VehicleLockState.Locked;

            return veh;
        }

        public static void DeleteVehicle(Data veh, bool database = false)
        {
            if (veh == null)
            {
                return;
            }

            if (database)
            {
                DeleteVehicle(veh.ID);
            }
            veh.Destroy();
        }

        public static void DeleteVehicle(long id)
        {
            if (id > 0)
            {
                DatabaseHelper.DeleteVehicle(id);
            }
        }
    }
}
