using DbScaffold.Models;
using StoriesUntoldDataLayer.DataContext;
using System.Threading;
using WWServer.LocalModels;
using WWServer.Packets.ClientPackets;

namespace WWServer.Interpritators
{
    public class AbilityChargers
    {
        static int Counter;
        static double AbilityMultiplayer = 2;
        static AssociatedCharacterAbilities Ability;
        public static bool Interrupt;
        public static void Charge(int abilityId, int connectionId)
        {
            if (Ability == null)
                Ability = DatabaseLayer.GetAbilityInCharacter(abilityId, ClientController.GetCharacterId(connectionId));
            start:
            if (Interrupt)
                return;
            Ability.Ability.Damage *= AbilityMultiplayer;
            Thread.Sleep(2000);
            if (Counter == 3)
            {


            }
            else
            {
                Counter++;
                goto start;
            }
        }

        public static void Release(int connectionId, Abilities ability, bool charge, string name, Possition possition)
        {
            if (charge)
                PlayerAttackPackets.SendPlayerAttack(connectionId, Ability.Ability.Damage, Ability, name, possition);
            else
                PlayerAttackPackets.SendPlayerAttack(connectionId, ability.Damage, new AssociatedCharacterAbilities { Ability = ability, HasPos = 0 }, name, possition);

            Interrupt = true;
        }


    }
}
