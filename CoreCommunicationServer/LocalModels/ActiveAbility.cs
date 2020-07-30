namespace WWServer.LocalModels
{
    using DbScaffold.Models;
    public class ActiveAbility
    {
        public Abilities CurrentAbility { get; set; }
        public string ActiveName { get; set; }
        public Possition Possition { get; set; }
        public int Zone { get; set; }
        public int ConnectionId { get; set; }
        public int ComboPos { get; set; }
    }
}
