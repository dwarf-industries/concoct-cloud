

namespace WWServer.TransportLayer
{
    using DbScaffold.Models;
    using Lidgren.Network;
    using StoriesUntoldDataLayer.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    public class ObjectSerializer
    {

        public static void SendOverNetwork(List<object> current, Client client, NetDeliveryMethod method, int channel)
        {

            var om = client.Connection.Peer.CreateMessage();
            try
            {

                current.ForEach(x =>
                {
                    om = ReflectObject(om, x);
                });


                //   client.Connection.SendMessage(om, method, channel);
                Listener.s_server.SendMessage(om, client.Connection, method, channel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }

            //networkMessage.Write();
        }

        private static NetOutgoingMessage ReflectObject(NetOutgoingMessage om, object x)
        {

            var newOm = CastType(x, om);
            if (!newOm.Item2)
            {
                var ObjectFields = x.GetType();

                //var fields = ObjectFields.GetFields();
                var properties = ObjectFields.GetProperties();

                foreach (var field in properties)
                {
                    var getVal = field.GetValue(x);
                    om = CastType(getVal, om).Item1;

                }

            }
            else
                return newOm.Item1;
            return om;
        }

        private static Tuple<NetOutgoingMessage, bool> CastType(object currentProperty, NetOutgoingMessage om)
        {
            object temp = currentProperty; // Get value
            var currentOm = om;
            var modified = default(bool);
            if (temp is int)
            {
                var thisInt = (int)temp;
                om.Write(thisInt);
                modified = true;
            }
            if (temp is short)
            {
                var thisShort = (short)temp;
                om.Write(thisShort);
                modified = true;
            }
            if (temp is long)
            {
                var thisLong = (long)temp;
                om.Write(thisLong);
                modified = true;
            }
            if (temp is ushort)
            {
                var thisUShort = (ushort)temp;
                om.Write(thisUShort);
                modified = true;
            }
            if (temp is sbyte)
            {
                var thisSByte = (sbyte)temp;
                om.Write(thisSByte);
                modified = true;
            }
            if (temp is float)
            {
                var thisFloat = (float)temp;
                om.Write(thisFloat);
                modified = true;
            }
            if (temp is double)
            {
                var thisDouble = (double)temp;
                om.Write(thisDouble);
                modified = true;
            }
            if (temp is bool)
            {
                var thisBool = (bool)temp;
                om.Write(thisBool);
                modified = true;
            }
            if (temp is string)
            {
                var thisString = (string)temp;
                om.Write(thisString);
                modified = true;
            }

            if (temp is ServerPackets)
            {
                var thisPacketId = (ServerPackets)temp;
                var vals = Enum.GetValues(typeof(ServerPackets)).AsQueryable();
                var res = Enum.Parse(typeof(ServerPackets), thisPacketId.ToString());
                om.Write(res.GetHashCode());
                modified = true;
            }
            if (temp is Seasons)
            {
                var thisPacketId = (Seasons)temp;
                var vals = Enum.GetValues(typeof(Seasons)).AsQueryable();
                var res = Enum.Parse(typeof(Seasons), thisPacketId.ToString());
                om.Write(res.GetHashCode());
                modified = true;
            }
            if (temp is ICollection)
            {
                //var tempCollection = temp.GetType();

                //foreach (FieldInfo fields in tempCollection.GetFields())
                //{

                //   om = CastType(fields,currentOm);
                //}
            }
            return new Tuple<NetOutgoingMessage, bool>(om, modified);
        }

        internal static NetOutgoingMessage SendOverNetworkBinary<T>(T data, List<object> packetAndData, Client current, NetOutgoingMessage message)
        {
            var om = default(NetOutgoingMessage);
            if (message != null)
                om = message;
            else
                om = current.Connection.Peer.CreateMessage();
            var bytes = SerializeObject(data);
            try
            {

                if (packetAndData != null)
                    packetAndData.ForEach(x =>
                    {
                        om = ReflectObject(om, x);
                    });
                om.Write(bytes.Length);
                om.Write(bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing data to client! Object is null");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }

            return om;

        }

        public static void SendPacket(NetOutgoingMessage om, Client client, NetDeliveryMethod method, int channel)
        {
            try
            {
                Listener.s_server.SendMessage(om, client.Connection, NetDeliveryMethod.ReliableOrdered, 0);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static T Deserialize<T>(byte[] param)
        {
            using (MemoryStream ms = new MemoryStream(param))
            {
                var br = new BinaryFormatter();
                br.Binder = new CustomizedBinder();
                return (T)br.Deserialize(ms);
            }
        }
        private static byte[] SerializeObject<T>(T param)
        {
            byte[] encMsg = null;
            try
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    var br = new BinaryFormatter();
                    br.Serialize(ms, param);
                    encMsg = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                if (param != null)
                    Console.WriteLine($"Exception thrown while serializing value {param.ToString()}");
                else
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                }
            }
            return encMsg;
        }
    }
}
