namespace WWServer
{
    using DbScaffold.Models;
    using StoriesUntoldDataLayer.DataContext;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WWServer.Interpritators;
    using WWServer.LocalModels;

    class Program
    {
        public static List<ActiveAbility> ActiveAbilities { get; set; }
        public static Seasons CurrentSeason { get; set; }
        public static List<ActiveMovementSpeeds> ActiveMovementSpeeds = DatabaseLayer.GetPossibleSpeeds();
        public static List<ActiveZoneHandlers> ActiveZoneHandlers { get; set; }
        public static List<ZoneActiveNpcs> ZoneActiveNpcs { get; set; }
        static void Main(string[] args)
        {
            //ObjectSerializer.SendOverNetwork(new UserAccounts
            //{
            //    Address = "asd",
            //    BillingAddress = "",
            //    Username = "asd",
            //    Id = 1

            //}, typeof(UserAccounts),null);
            ZoneActiveNpcs = new List<ZoneActiveNpcs>();
            ActiveZoneHandlers = new List<ActiveZoneHandlers>();
            ServerHandleData.InitializePackets();
            MoveAbility.Init();
            var activeNpcs = DatabaseLayer.GetNpcsByZone(1);
            InternalDataHandlers.InitializePackets();
            Task.Run(() => ExecuteLocalCmd());
            Listener.StartServer();
            // Task.Run(() => Interpritators.NpcsHandler.ActivateNpcs(activeNpcs));


            //var res = DatabaseLayer.GetZoneQuests(1);
            //res.ForEach(x =>
            //{
            //    Console.WriteLine($"Quest name {x.Quest.Name}");
            //});
            //Console.ReadLine();


        }


        static void ExecuteLocalCmd()
        {
        start:
            var cmd = Console.ReadLine();
            InternalServerCommands.CallServerConsoleCmd(cmd);
            goto start;
        }
    }
}
