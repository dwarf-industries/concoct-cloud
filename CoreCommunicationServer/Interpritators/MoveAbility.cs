namespace WWServer.Interpritators
{
    using DbScaffold.Models;
    using System.Collections.Generic;
    using WWServer.LocalModels;

    public class MoveAbility
    {
        private static string Name { get; set; }
        private static int TimeLenght { get; set; }
        public static int ConnectionId;

        public static void Register(Abilities activeAbility, Possition possition, string name, int zone, int connectionId)
        {
            Program.ActiveAbilities.Add(new ActiveAbility
            {
                ActiveName = name,
                CurrentAbility = activeAbility,
                Possition = possition,
                Zone = zone,
                ConnectionId = connectionId,
                ComboPos = 0
            });
            Name = name;
            TimeLenght = activeAbility.HitTime.Value;
            ConnectionId = connectionId;
            // Start a thread that calls a parameterized static method.

        }



        internal static void Init() => Program.ActiveAbilities = new List<ActiveAbility>();


    }
}
