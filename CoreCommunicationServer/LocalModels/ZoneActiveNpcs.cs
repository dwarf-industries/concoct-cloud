using DbScaffold.Models;
using System.Collections.Generic;

namespace WWServer.LocalModels
{
    public class ZoneActiveNpcs
    {
        public int ZoneId { get; set; }
        public List<Npcs> Npcs;
    }
}
