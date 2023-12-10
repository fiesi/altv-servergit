using AltV.Net;
using AltV.Net.Elements.Entities;

using Npgsql;

namespace NAMERP
{
    public class Server : Resource
    {
        private static bool _shutdown = false;
        private static Thread? _customThread = null;

        public override void OnStart()
        {
            Alt.LogInfo("Verbindung zur Datenbank wird hergestellt...");
            Database.GetInstance();

            // We're using this thread to handle some things separately and not block player actions
            _customThread = new Thread(() => {
                while (!_shutdown)
                {
                    long currenTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    Player.Data[] targets = Alt.GetAllPlayers().Cast<Player.Data>().Where((t) => t.Character != null && (currenTimestamp - t.Character.LastPayday) > 300).ToArray();
                    foreach (Player.Data target in targets)
                    {
#pragma warning disable CS8602 // We know that this player has chosen a character
                        target.Character.LastPayday = currenTimestamp;
                        target.Character.AddBank(1200);
                        target.SetSyncedMetaData("bank", target.Character.GetBank());
                    }
                    targets.SendChatMessage(ChatMessageType.Info, null, "Du hast dein Gehalt für eine Stunde erhalten!");

                    Thread.Sleep(500);
                }
            });
            _customThread.Start();
        }

        public override void OnStop()
        {
            Alt.LogWarning("Server wird gestoppt!");
            _shutdown = true;
        }

        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new Player.Factory();
        }

        public override IEntityFactory<IVehicle> GetVehicleFactory()
        {
            return new Vehicle.Factory();
        }
    }
}
