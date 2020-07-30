namespace WWServer.Packets
{
    using Lidgren.Network;
    using System;

    public class Packet_ThankYouMessage
    {
        public static void ThankYou(NetIncomingMessage data)
        {
            string msg = data.ReadString();
            Console.WriteLine($"From Client: {msg}");
        }
    }
}
