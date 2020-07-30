using Lidgren.Network;

namespace WWServer.LocalModels
{
    public class ActiveZoneHandlers
    {
        public int ZoneId { get; set; }
        public NetConnection Connection { get; set; }

    }
}
