using AltV.Net.Elements.Entities;
using AltV.Net;

namespace NAMERP.Vehicle
{
    internal class Factory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(ICore core, IntPtr entityPointer, uint id)
        {
            return new Data(core, entityPointer, id);
        }
    }
}
