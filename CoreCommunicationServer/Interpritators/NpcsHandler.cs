using DbScaffold.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WWServer.Packets.ClientPackets;

namespace WWServer.Interpritators
{
    public class NpcsHandler
    {

#pragma warning disable CS0649 // Field 'NpcsHandler.Npcs' is never assigned to, and will always have its default value null
        private static List<LocalModels.ActiveServerNpcs> Npcs;
#pragma warning restore CS0649 // Field 'NpcsHandler.Npcs' is never assigned to, and will always have its default value null

        public static void ActivateNpcs(List<Npcs> npcs)
        {
            npcs.ForEach(x =>
            {
                var npc = new LocalModels.ActiveServerNpcs
                (
                    x,
                    Converters.CastDoubleToFloat(x.RotationY.Value),
                    Converters.CastDoubleToFloat(x.RotationZ.Value),
                    Converters.CastDoubleToFloat(x.RotationY.Value),
                    Converters.CastDoubleToFloat(x.PossitionX.Value),
                    Converters.CastDoubleToFloat(x.PossitionY.Value),
                    Converters.CastDoubleToFloat(x.PossitionZ.Value)
                );
            });

            Npcs.ForEach(x =>
            {
                ActivateMovement(x);
            });
        }

        private static void ActivateMovement(LocalModels.ActiveServerNpcs activeServerNpcs)
        {
            Task.Run(() => NpcPackets.MoveNpc(activeServerNpcs.Npc.AssociatedNpcMovingPoints.FirstOrDefault().Point, activeServerNpcs));
        }
    }
}
