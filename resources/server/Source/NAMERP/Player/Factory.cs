using AltV.Net;
using AltV.Net.Elements.Entities;

namespace NAMERP.Player
{
    internal class Factory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(ICore core, IntPtr entityPointer, uint id)
        {
            return new Data(core, entityPointer, id);
        }
    }
}
