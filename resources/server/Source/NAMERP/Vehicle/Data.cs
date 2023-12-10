using AltV.Net;

namespace NAMERP.Vehicle
{
    internal partial class Data
    {
        /* ===== PUBLIC FIELDS ===== */
        public long ID { get; set; } = 0;
        public long Owner { get; set; } = 0;

        /* ===== PROTECTED FIELDS ===== */
        protected readonly List<long> _tempKeys = new();
    }

    internal partial class Data
    {
        /* ===== TEMPKEYS ===== */
        public bool HasKey(long id, bool ownerOnly = false) => ownerOnly ? Owner == id : Owner == id || HasTempKey(id);
        public bool HasTempKey(long id) => _tempKeys.Contains(id);
        public void GiveTempKey(long id)
        {
            if (HasTempKey(id))
            {
                return;
            }

            _tempKeys.Add(id);
        }
    }

    internal partial class Data : AltV.Net.Elements.Entities.Vehicle
    {
        public Data(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id) { }
    }
}
