using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;

namespace NAMERP.Commands
{
    internal class Normal : IScript
    {
        [CommandEvent(CommandEventType.CommandNotFound)]
        public static void OnCommandNotFound(IPlayer player, string cmd)
        {
            player.SendChatMessage(ChatMessageType.Error, null, $"Befehl \"{cmd}\" nicht gefunden!");
        }

        [Command("givekey")]
        public static void CMD_GIVEKEY(Player.Data player, uint targetId)
        {
            if (player.ID == targetId)
            {
                player.SendChatMessage(ChatMessageType.Warning, null, "Du kannst dir nicht selber einen Schlüssel geben!");
                return;
            }

            Vehicle.Data veh = (Vehicle.Data)player.Vehicle;
            if (veh == null)
            {
                player.SendChatMessage(ChatMessageType.Warning, null, "Du musst in einem Fahrzeug sitzen!");
                return;
            }

            if (!veh.HasKey(player.ID, true))
            {
                player.SendChatMessage(ChatMessageType.Warning, null, "Du musst Besitzer von diesem Fahrzeug sein!");
                return;
            }

            Player.Data target = (Player.Data)Alt.GetPlayerById(targetId);
            if (target == null)
            {
                player.SendChatMessage(ChatMessageType.Warning, null, "Dieser Spieler ist gerade nicht anwesend!");
                return;
            }
            if (target.Position.Distance(player.Position) < 5)
            {
                player.SendChatMessage(ChatMessageType.Warning, null, "Dieser Spieler befindet sich nicht in deiner Nähe!");
                return;
            }

            veh.GiveTempKey(target.ID);
        }

        [Command("clearobjects")]
        public static void CMD_CLEAROBJECTS(Player.Data player)
        {
            if (player.Character == null)
            {
                return;
            }

            if (Object.PlayerObjects.ContainsKey(player.Character.ID))
            {
                Object.RemoveAllByCharId(player.Character.ID);
            }
        }
    }
}
