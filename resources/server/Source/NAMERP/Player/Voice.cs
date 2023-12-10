using AltV.Net;
using AltV.Net.Elements.Entities;

namespace NAMERP.Player
{
    internal class Voice : IScript
    {
        [ClientEvent("voice:range")]
        public static void CE_VOICE_RANGE(IPlayer player)
        {
            player.GetSyncedMetaData("voice:range", out int range);
            VoiceRange newRange = ++range > (int)VoiceRange.Long ? VoiceRange.Short : (VoiceRange)range;
            player.SwitchVoiceRange(newRange);
        }
    }
}
