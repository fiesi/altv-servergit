using NAMERP.Vehicle;

using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;

namespace NAMERP.Player
{
    internal class ScriptEvents : IScript
    {
        private static readonly Random _random = new();

        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public static void OnPlayerConnect(Data player, string _)
        {
#if DEBUG
            Alt.LogInfo($"Spieler {player.Name} ({player.Ip}) hat sich verbunden!");
#endif
            player.Dimension = int.MaxValue - _random.Next(999999);
            player.Model = (uint)PedModel.FreemodeMale01;
            player.Spawn(new(344.3341f, -998.8612f, -99.19622f), 0);
            player.Emit("radar", false);
            player.Emit("freeze", false);
            player.Emit("visible", false);
            player.Emit("camera:create", 344.3341, -998.8612, -98.19622, -370.61447, 0, -289.61447, 2, 40);
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public static void OnPlayerDisconnect(Data player, string _)
        {
            if (player.Character == null)
            {
                return;
            }

            player.RemoveFromAllVoice();

            Vehicle.Data[] vehs = player.Character.GetAllExistVehicles();
            foreach (Vehicle.Data veh in vehs)
            {
                veh.Save();
                veh.Destroy();
            }

            Object.RemoveAllByCharId(player.Character.ID);
        }

        [ScriptEvent(ScriptEventType.PlayerDead)]
        public static void OnPlayerDead(IPlayer player, IEntity _1, uint _2)
        {
            player.SendChatMessage(ChatMessageType.Warning, null, "Du bist gestorben! Du wirst in 3 Sekunden wiederbelebt!");
            Task.Delay(3000).ContinueWith(_ => player.Spawn(new(-425, 1123, 325), 0));
        }
    }
}
