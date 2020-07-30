

namespace WWServer
{
    using DbScaffold.Models;
    using Lidgren.Network;
    using StoriesUntoldDataLayer.DataContext;
    using StoriesUntoldDataLayer.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WWServer.Interpritators;
    using WWServer.TransportLayer;

    static class ClientController
    {
        private static int I { get; set; }
        public static List<Client> Clients = new List<Client>();

        internal static int GetCharacterId(int connectionId) => Clients.FirstOrDefault(x => x.Id == connectionId).LoggedInCharacterId;
        internal static Client GetClientBYId(int connectionId) => Clients.FirstOrDefault(x => x.Id == connectionId);
        internal static int GetClientZone(int connectionId) => Clients.FirstOrDefault(x => x.Id == connectionId).InZone;
        internal static List<Client> GetClientsInZone(int zoneId, float posX, float posY, float posZ) => Clients.Where(x => x.InZone == zoneId && x.MatchPossition(posX, posY, posZ) == true).ToList();
        public static Client AddClient(NetConnection connection)
        {
            Clients.Add(new Client(connection.RemoteUniqueIdentifier, Clients.Count + 1, connection));
            return Clients.LastOrDefault();
        }

        internal static Client GetZoneNodeByZoneId(int zoneId) => Clients.FirstOrDefault(x => x.IsNodeConnection && x.InZone == zoneId);
        internal static void SendObjectDataToAll(NetOutgoingMessage data, NetDeliveryMethod netDeliveryMethod, int channel)
        {

            Clients.ForEach(x =>
            {
                if (x.LoggedInCharacterId != 0)
                {
                    if (x.Connection.Status != NetConnectionStatus.Disconnected || x.Connection.Status != NetConnectionStatus.Disconnected)
                    {
                        var message = x.Connection.Peer.CreateMessage();
                        message.Data = data.Data;
                        message.LengthBits = data.LengthBits;
                        message.LengthBytes = data.LengthBytes;
                        // Console.WriteLine($"Sending data to {x._socket.AddressFamily} connection id equals {x.Id} ");
                        SendObjectDataTo(message, x, netDeliveryMethod, channel);
                    }
                }
                else if (x.IsExtension)
                {

                    var message = x.Connection.Peer.CreateMessage();
                    message.Data = data.Data;
                    message.LengthBits = data.LengthBits;
                    message.LengthBytes = data.LengthBytes;
                    // Console.WriteLine($"Sending data to {x._socket.AddressFamily} connection id equals {x.Id} ");
                    SendObjectDataTo(message, x, netDeliveryMethod, channel);

                }
            });
        }

        internal static Client GetClientByConnection(NetConnection senderConnection) => Clients.FirstOrDefault(x => x.Connection == senderConnection);


        internal static void SendDataTo(int connectionID, List<object> data, NetDeliveryMethod netDeliveryMethod, int channel)
        {

            var client = Clients.FirstOrDefault(x => x.Id == connectionID);
            ObjectSerializer.SendOverNetwork(data, client, netDeliveryMethod, channel);

        }
        internal static NetOutgoingMessage WriteMessage<T>(int connectionID, List<object> packetAndId, T current, NetOutgoingMessage om)
        {
            var client = Clients.FirstOrDefault(x => x.Id == connectionID);
            return ObjectSerializer.SendOverNetworkBinary(current, packetAndId, client, om);
        }

        internal static NetOutgoingMessage WriteMessage<T>(Client client, List<object> packetAndId, T current, NetOutgoingMessage om)
        {
            return ObjectSerializer.SendOverNetworkBinary(current, packetAndId, client, om);
        }
        internal static void SendObjectDataTo(NetOutgoingMessage om, Client client, NetDeliveryMethod method, int channel) => ObjectSerializer.SendPacket(om, client, method, channel);

        public static void SendDataAllBut(int connectedID, List<object> data, NetDeliveryMethod netDeliveryMethod, int chanel)
        {
            Clients.ForEach(x =>
            {
                if (x.Id != connectedID)
                {
                    if (x.Connection.Status != NetConnectionStatus.Disconnected || x.Connection.Status != NetConnectionStatus.Disconnecting)
                    {
                        // Console.WriteLine($"Sending data to {x._socket.AddressFamily} connection id equals {x.Id} ");
                        SendDataTo(x.Id, data, netDeliveryMethod, chanel);
                    }
                }
            });

        }
        public static void SendDataAllBut(int connectedID, NetOutgoingMessage data, NetDeliveryMethod netDeliveryMethod, int chanel)
        {
            var currentData = data.Data;
            Clients.ForEach(x =>
            {
                if (x.Id != connectedID)
                {
                    if (x.Connection.Status != NetConnectionStatus.Disconnected || x.Connection.Status != NetConnectionStatus.Disconnected)
                    {
                        var message = x.Connection.Peer.CreateMessage();
                        message.Data = data.Data;
                        message.LengthBits = data.LengthBits;
                        message.LengthBytes = data.LengthBytes;
                        // Console.WriteLine($"Sending data to {x._socket.AddressFamily} connection id equals {x.Id} ");
                        SendObjectDataTo(message, x, netDeliveryMethod, chanel);
                    }
                }
            });

        }

        public static void SendDataToAll(List<object> bytes, NetDeliveryMethod netDeliveryMethod, int chanel)
        {
            Clients.ForEach(x =>
            {
                if (x.Connection != null && x.LoggedInCharacterId != 0)
                {

                    SendDataTo(x.Id, bytes, netDeliveryMethod, chanel);
                }
                else if (x.IsExtension)
                {

                    SendDataTo(x.Id, bytes, netDeliveryMethod, chanel);

                }
            });
        }

        public static void SendInWorldLoadCharacters(int connectionId, NetDeliveryMethod netDeliveryMethod, int chanel)
        {
            //gets the client of the current joining player
            var playerClient = Clients.FirstOrDefault(x => x.Id == connectionId);
            //Send all player in the current scene
            Clients.ForEach(x =>
            {
                if (x.Connection != null && x.LoggedInCharacterId != 0)
                {
                    if (x.Id != connectionId)
                    {
                        var playerData = PlayerData(connectionId, x.Id);
                        if (playerData != null)
                            SendObjectDataTo(playerData, playerClient, netDeliveryMethod, chanel);
                    }
                }
                else if (x.IsExtension)
                {
                    if (x.Id != connectionId)
                    {
                        var playerData = PlayerData(x.Id, connectionId);
                        if (playerData != null)
                            SendObjectDataTo(playerData, x, netDeliveryMethod, chanel);
                    }
                }
            });
            var currentPlayer = PlayerData(connectionId, connectionId);
            if (currentPlayer != null)
                SendObjectDataTo(currentPlayer, playerClient, netDeliveryMethod, chanel);
        }

        public static void SendCharacterToClientsInZone(int connectionId, NetDeliveryMethod netDeliveryMethod, int chanel)
        {
            Clients.ForEach(x =>
            {
                if (x.Connection != null && x.LoggedInCharacterId != 0)
                {
                    if (x.Id != connectionId)
                    {
                        var playerData = PlayerData(x.Id, connectionId);
                        if (playerData != null)
                            SendObjectDataTo(playerData, x, netDeliveryMethod, chanel);
                    }
                }
                else if (x.IsExtension)
                {
                    if (x.Id != connectionId)
                    {
                        var playerData = PlayerData(x.Id, connectionId);
                        if (playerData != null)
                            SendObjectDataTo(playerData, x, netDeliveryMethod, chanel);
                    }
                }
            });
        }
        public static NetOutgoingMessage PlayerData(int connectionId, int accId)
        {
            try
            {
                var clientInto = Clients.FirstOrDefault(x => x.Id == accId);
                if (clientInto.Connection.Status == NetConnectionStatus.Disconnected || clientInto.Connection.Status == NetConnectionStatus.Disconnecting)
                    return null;
                var data = DatabaseLayer.GetCharacterData(clientInto.AccountId, clientInto.LoggedInCharacterId);
                data = Converters.GetCharacterRecepie(data);
                Clients.FirstOrDefault(x => x.Id == accId).ClientPosssition = new ClientPossition
                {
                    Rw = (float)data.RotationW,
                    Rx = (float)data.RotationX,
                    Ry = (float)data.RotationY,
                    Rz = (float)data.RotationZ,
                    X = (float)data.PossitionX,
                    Y = (float)data.PossitionY,
                    Z = (float)data.PossitionZ

                };
                Clients.FirstOrDefault(x => x.Id == accId).Weapon = data.EquipedWeapon;
                Clients.FirstOrDefault(x => x.Id == accId).Health = 100;
                var characterId = Clients.FirstOrDefault(x => x.Id == accId).LoggedInCharacterId;
                var listData = new List<object> {
                    ServerPackets.SPlayerData ,
                    accId,
                    characterId
                };
                var client = Clients.FirstOrDefault(x => x.Id == connectionId);

                var om = WriteMessage<Characters>(connectionId, listData, data, null);
                return om;
            }
            catch
            {
                return null;
            }

        }

        public static void ShowClients()
        {
            foreach (var item in Clients)
            {
                Console.WriteLine($"ConnectionId : {item.Id}");
                Console.WriteLine(item.Connection == null ? "Socket missing" : "Present");
                Console.WriteLine($"User Ip address : {item.Connection.RemoteEndPoint}");
            }
        }
    }
}
