namespace WWServer
{
    using Lidgren.Network;
    using StoriesUntoldDataLayer.DataContext;
    using StoriesUntoldDataLayer.Model;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using WWServer.Packets.ClientPackets;

    public class Listener
    {
        public static bool Running { get; set; }
        private static int Port = 9637; // on this port we will listen
        public static NetServer s_server;
#pragma warning disable CS0169 // The field 'Listener.s_settingsWindow' is never used
        private static NetPeerSettingsWindow s_settingsWindow;
#pragma warning restore CS0169 // The field 'Listener.s_settingsWindow' is never used
        private static void Output(string text)
        {
            Console.WriteLine(text);
        }


        private static void Application_Idle()
        {
            while (Running)
            {
                NetIncomingMessage im;
                while ((im = s_server.ReadMessage()) != null)
                {
                    // handle incoming message
                    switch (im.MessageType)
                    {
                        case NetIncomingMessageType.ConnectionApproval:
                            {
                                im.SenderConnection.Approve();
                                Console.WriteLine("Sent data to client");
                            }
                            break;

                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.ErrorMessage:
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.VerboseDebugMessage:
                            string text = im.ReadString();
                            Output(text);
                            break;

                        case NetIncomingMessageType.StatusChanged:
                            NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();

                            string reason = im.ReadString();
                            Output(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " " + status + ": " + reason);
                            if (status == NetConnectionStatus.RespondedAwaitingApproval)
                            {
                                NetIncomingMessage hail = im.SenderConnection.RemoteHailMessage;
                                im.SenderConnection.Approve();
                            }
                            if (status == NetConnectionStatus.Connected)
                            {
                                im.SenderConnection.CanSendImmediately(NetDeliveryMethod.ReliableOrdered, 0);
                                Output("Remote hail: " + im.SenderConnection.RemoteHailMessage.ReadString());

                                var client = ClientController.AddClient(im.SenderConnection);
                                AuthenicationPackets.InitializeConnection(client.Id);

                            }
                            if (status == NetConnectionStatus.Disconnected)
                            {
                                var connection = ClientController.Clients.FirstOrDefault(x => x.Connection == im.SenderConnection);
                                DisposeConnection(connection);
                            }

                            UpdateConnectionsList();
                            break;
                        case NetIncomingMessageType.Data:
                            ServerHandleData.HandleDataPackets(im);
                            break;
                        default:
                            Output("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes " + im.DeliveryMethod + "|" + im.SequenceChannel);
                            break;
                    }
                    s_server.Recycle(im);
                }
                Thread.Sleep(1);

            }
        }

        public static void DisposeConnection(Client client)
        {
            if (client == null)
                return;

            if (client.LoggedInCharacterId != 0)
            {
                DatabaseLayer.UpdateCharacterPossition(client.LoggedInCharacterId, client.ClientPosssition);
            }
            ClientController.Clients.Remove(client);
        }

        private static void UpdateConnectionsList()
        {
            //s_form.listBox1.Items.Clear();

            foreach (NetConnection conn in s_server.Connections)
            {
                string str = NetUtility.ToHexString(conn.RemoteUniqueIdentifier) + " from " + conn.RemoteEndPoint.ToString() + " [" + conn.Status + "]";
                //    s_form.listBox1.Items.Add(str);
            }
        }

        // called by the UI
        public static void StartServer()
        { // set up network
            NetPeerConfiguration config = new NetPeerConfiguration("game");
            config.MaximumConnections = 100;
            config.Port = Port;
            Console.WriteLine(config.LocalAddress.AddressFamily);
            s_server = new NetServer(config);
            s_server.Start();

            Task.Run(() => WeatherPackets.SetWorldTime());
            Listener.Running = true;
            Application_Idle();
        }

        // called by the UI
        public static void Shutdown()
        {
            s_server.Shutdown("Requested by user");
        }

    }
}
