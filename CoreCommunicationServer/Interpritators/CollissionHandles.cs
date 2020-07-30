using DbScaffold.Models;
namespace WWServer.Interpritators
{
    public class CollissionHandles
    {
#pragma warning disable CS0169 // The field 'CollissionHandles.Ability' is never used
        static Abilities Ability;
#pragma warning restore CS0169 // The field 'CollissionHandles.Ability' is never used
        public static void AbilityEffectStarted(AssociatedCharacterAbilities associatedCharacterAbilities)
        {
            //var timer = new Timer(CallBack);

            //Ability = associatedCharacterAbilities.Ability;
        }

        //private static void CallBack(object state)
        //{

        //    ClientController.Clients.ForEach(x =>
        //    {
        //        if (Ability.PosX == x.ClientPosssition.X && Ability.PosZ == x.ClientPosssition.Z)
        //        {

        //        }
        //    });
        //}
    }
}
