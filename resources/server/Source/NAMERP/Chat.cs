using AltV.Net;
using AltV.Net.Elements.Entities;

namespace NAMERP
{
    internal enum ChatMessageType : short
    {
        Normal  = 0,
        Info    = 1,
        Warning = 2,
        Error   = 3,
    };

    internal static class CustomChatExtension
    {
        internal static void SendChatMessage(this IPlayer player, ChatMessageType type, string? from, string message)
        {
            player.Emit("chat:message", (short)type, from, message);
        }

        internal static void SendChatMessage(this IPlayer[] targets, ChatMessageType type, string? from, string message)
        {
            Alt.EmitClients(targets, "chat:message", (short)type, from, message);
        }
    }

    internal class Chat : IScript
    {
        [ClientEvent("chat:message")]
        public static void CE_CHAT_MESSAGE(Player.Data player, string text)
        {
            if (text.StartsWith('/'))
            {
                return;
            }

            IPlayer[] targets = Alt.GetAllPlayers().Where((el) => el.Position.Distance(player.Position) < 10).ToArray();
            targets.SendChatMessage(ChatMessageType.Normal, $"[{player.ID}]", text);
        }
    }
}
