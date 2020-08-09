using DbScaffold.Models;

namespace WWServer.LocalModels
{
    public class ActiveServerNpcs
    {
        public Npcs Npc { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Rx { get; set; }
        public float Rz { get; set; }
        public float Ry { get; set; }
        public NpcMovingPoints Target;

        public ActiveServerNpcs(Npcs npc, float x, float y, float z, float rX, float rZ, float rY)
        {
            Npc = npc;
            X = x;
            Y = y;
            Z = z;
            Rx = rX;
            Ry = rY;
            Rz = rZ;

            ActivateStopWatch();
        }

        private void ActivateStopWatch()
        {
            while (X != Target.PosX && Z != Target.PosZ)
            {
                //var nextPoint = Npc.AssociatedNpcMovingPoints.
                //Task.Run(() => NpcPackets.MoveNpc(, activeServerNpcs));
            }
        }
    }
}
