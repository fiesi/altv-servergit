using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace NAMERP
{
    internal class Object : IScript
    {
        public static readonly Dictionary<long, List<IObject>> PlayerObjects = new();

        [ClientEvent("object:create")]
        public static void SE_CREATE_OBJECT(Player.Data player, uint model, Position pos, Rotation rot)
        {
            if (player.Character == null)
            {
                return;
            }

            if (!PlayerObjects.ContainsKey(player.Character.ID))
            {
                PlayerObjects.Add(player.Character.ID, new());
            }
            else if (player.Admin < 4 && PlayerObjects[player.Character.ID].Count == 2)
            {
                player.SendChatMessage(ChatMessageType.Error, null, "Du kannst nicht mehr als 2 Objekte gleichzeitig erschaffen. Nutze `/clearobjects` um alte Objekte zu entfernen.");
                return;
            }

            IObject obj = Alt.CreateObject(model, pos, rot);
            obj.PlaceOnGroundProperly(); // Doesn't work (v15.60)
            obj.Frozen = true;

            PlayerObjects[player.Character.ID].Add(obj);
        }

        public static void RemoveAllByCharId(long charId)
        {
            if (!PlayerObjects.ContainsKey(charId))
            {
                return;
            }

            List<IObject> list = PlayerObjects[charId];
            foreach (IObject obj in list)
            {
                obj?.Destroy();
            }
            PlayerObjects.Remove(charId);
        }
    }
}
