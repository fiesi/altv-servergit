using AltV.Net;
using AltV.Net.Elements.Entities;

namespace NAMERP
{
    internal enum VoiceRange
    {
        Short = 0,
        Normal = 1,
        Long = 2,
    };

    internal static class Voice
    {
        private static readonly IVoiceChannel[] _ranges = new[]
        {
            Alt.CreateVoiceChannel(true, 3),
            Alt.CreateVoiceChannel(true, 6),
            Alt.CreateVoiceChannel(true, 10)
        };

        public static void AddToAllVoice(this IPlayer player)
        {
            player.SetSyncedMetaData("voice:range", 0);

            for (int i = 0; i < _ranges.Length; i++)
            {
                if (!_ranges[i].HasPlayer(player))
                {
                    _ranges[i].AddPlayer(player);
                }

                _ranges[i].MutePlayer(player);
            }

            _ranges[0].UnmutePlayer(player);
        }

        public static void RemoveFromAllVoice(this IPlayer player)
        {
            for (int i = 0; i < _ranges.Length; i++)
            {
                if (_ranges[i].HasPlayer(player))
                {
                    _ranges[i].RemovePlayer(player);
                }
            }
        }

        public static void SwitchVoiceRange(this IPlayer player, VoiceRange range)
        {
            int sourceRange = (int)range;
            player.SetSyncedMetaData("voice:range", sourceRange);

            for (int i = 0; i < _ranges.Length; i++)
            {
                if (i != sourceRange)
                {
                    _ranges[i].MutePlayer(player);
                }
            }

            _ranges[sourceRange].UnmutePlayer(player);
        }
    }
}
