using System;
using System.Collections.Generic;
using System.Linq;
using WWServer.LocalModels;
using WWServer.Packets.InternalPackets;

namespace WWServer
{
    class InternalDataHandlers
    {
        private delegate void Packet_(IncomingData data);
        static Dictionary<long, Packet_> packets;

        public static void InitializePackets()
        {
            Console.WriteLine("Initializing Network Packets...");
            packets = new Dictionary<long, Packet_>
            {
                { (int)InternalPackets.SMovement, Movement.MoveCurrent},
                { (int)InternalPackets.SNpcMovement, Movement.MoveCurrentNpc},


            };
        }

        public static void HandleDataPackets(IncomingData im)
        {

            Packet_ packet;

            var packetIdentifier = (int)im.CurrentData.FirstOrDefault();
            if (packets.TryGetValue(packetIdentifier, out packet))
            {
                packet.Invoke(im);
            }
        }

        private static int GetIdentifier(string Id)
        {
            var res = Enum.Parse(typeof(InternalPackets), Id);
            return res.GetHashCode();
        }
    }
}
