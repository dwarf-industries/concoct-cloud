namespace WWServer.Packets
{
    using Lidgren.Network;
    using StoriesUntoldDataLayer.DataContext;
    using System.Linq;
    using WWServer.Packets.ClientPackets;

    public class Packet_Connecting
    {
        public static void Connect(NetIncomingMessage data)
        {
            var connectionId = data.ReadInt32();
            var username = data.ReadString();
            var readClientPublic = data.ReadString();

            var connection = ClientController.Clients.FirstOrDefault(x => x.Id == connectionId);
            var result = DatabaseLayer.GetSalt(username);
            if (result != null && connection != null)
            {
                var serverEphemeral = ServerHandleData.SrpServer.GenerateEphemeral(result.Item2);

                connection.ClientSecret = serverEphemeral.Secret;

                //if (connection.Id != 0)
                //{
                //   // connection.AccountId = int.Parse(result.Item2);
                //    ClientController.SendInGame(connectionId);
                //}
                AuthenicationPackets.SendSalt(result.Item1, serverEphemeral.Public, connectionId);

            }
            else
                AuthenicationPackets.SendErrorLogin(connectionId);
        }

        public static void CSrpVerifier(NetIncomingMessage data)
        {
            var connectionId = data.ReadInt32();
            var proof = data.ReadString();
            var email = data.ReadString();
            var publicKey = data.ReadString();
            var connection = ClientController.Clients.FirstOrDefault(x => x.Id == connectionId);
            var result = DatabaseLayer.GetSalt(email);
            try
            {
                if (result != null)
                {
                    var serverSession = ServerHandleData.SrpServer.DeriveSession(connection.ClientSecret,
                    publicKey, result.Item1, email, result.Item2, proof);
                    AuthenicationPackets.SrpHandShakeValidateClientSide(serverSession.Proof, connectionId);
                    connection.AccountId = DatabaseLayer.GetAccountId(email);
                    var duplications = ClientController.Clients.Where(x => x.AccountId == connection.AccountId);
                    if (duplications.Count() > 1)
                    {
                        var oldConnection = duplications.FirstOrDefault();
                        Listener.DisposeConnection(oldConnection);
                    }
                }
            }
            catch (System.Exception)
            {


            }

        }
        public static void SRPHandshake(NetIncomingMessage data)
        {
            var connectionId = data.ReadInt32();
            var connection = ClientController.Clients.FirstOrDefault(x => x.Id == connectionId);

            if (connection.Id != 0)
            {
                // connection.AccountId = int.Parse(result.Item2);
                AuthenicationPackets.SendInGame(connectionId);
            }
        }

        public static void Disconnect(NetIncomingMessage data)
        {
            var playerId = data.ReadInt32();
            AuthenicationPackets.DisconnectPlayer(playerId);
        }
    }
}
