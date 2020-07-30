namespace WWServer.Interpritators
{
    using DbScaffold.Models;
    using System.Collections.Generic;
    using System.Threading;
    using WWServer.LocalModels;

    class StopWatchCallers
    {
        private static int CurrentId { get; set; }
        private static Mounts Mount { get; set; }
        private static int ConnectionId { get; set; }
        public static void CharacterMounting(int id, int loggedInCharacterId, Mounts mount)
        {
            CurrentId = loggedInCharacterId;
            Mount = mount;
            ConnectionId = id;
            Thread newThread = new Thread(Start);
            newThread.Start();
        }


        private static void Start(object obj)
        {
            new DelayHandlerUnified(500, new IncomingData
            {
                CurrentData = new List<object>
                 {
                     InternalPackets.SCharacterMounted,
                     CurrentId,
                     Mount,
                     ConnectionId
                 }
            });
        }
    }
}
