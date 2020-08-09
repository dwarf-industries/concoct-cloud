namespace StoriesUntoldDataLayer.Model
{
    using DbScaffold.Models;
    using Lidgren.Network;
    using System.Collections.Generic;

    public class Client
    {

        // public ReceivePacket Receive { get; set; }
        public int Id { get; set; }
        public bool Authenicated { get; set; }
        public int AccountId { get; set; }
        public int LoggedInCharacterId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public Indicator ActiveIndicator { get; set; }
        public NetConnection Connection { get; set; }
        public List<InventoryItems> InventoryItems { get; set; }
        public Mounts Mount;
        public bool IsBlocking { get; set; }
        public int InZone { get; set; }
        public float MovementSpeed;
        public int ComboModifier { get; set; }

        public float Health { get; set; }
        public SpeedModifiers CurrentSpeedModifier;

        public ClientPossition ClientPosssition { get; set; }
        public ClientPossition TemporaryPossition { get; set; }
        public Weapons Weapon;
        public bool IsExtension { get; set; }
        public bool IsNodeConnection { get; set; }
        //  public List<InvetoryItem> InventoryItems { get; set; }
        public Client(long socket, int id, NetConnection connection)
        {
            //    Receive = new ReceivePacket(socket, id);
            // Receive.StartReceiving();

            Id = id;
            Authenicated = false;
            Connection = connection;
            ChangeSpeed(SpeedModifiers.Walking);
        }

        public bool MatchPossition(float x, float y, float z)
        {
            return ClientPosssition.X == x && ClientPosssition.Z == z ? true : false;
        }

        public void ChangeSpeed(SpeedModifiers modifiers)
        {
            switch (modifiers)
            {
                case SpeedModifiers.Walking:
                    MovementSpeed = 2;
                    break;
                case SpeedModifiers.Running:
                    MovementSpeed = 6;
                    break;
                case SpeedModifiers.InjuredWalk:
                    MovementSpeed = 1;
                    break;
                case SpeedModifiers.InjuredRunning:
                    MovementSpeed = 3;
                    break;
                case SpeedModifiers.RWalking:
                    MovementSpeed = 3;
                    break;
                case SpeedModifiers.RRunning:
                    MovementSpeed = (float)Mount.MountSpeed;
                    break;
                default:
                    break;
            }
        }

        public int Attack(AssociatedCharacterAbilities ability)
        {
            if (ability == null)
                return 0;
            ComboModifier++;
            if (ComboModifier > ability.Ability.AssociatedAbilityCombos.Count)
                ComboModifier = 0;
            return ComboModifier;
        }
    }
}
