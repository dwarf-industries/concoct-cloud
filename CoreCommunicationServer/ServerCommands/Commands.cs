namespace WWServer.ServerCommands
{
    using System;
    using System.Linq;
    class Commands
    {
        internal static void DisposeConnections()
        {
            ClientController.Clients.ForEach(x =>
            {
                Console.WriteLine($"Diposing Connection {x.Connection.RemoteEndPoint}, loggedId {x.Id}, dbAccountId {x.AccountId}, Is authenicated {x.Authenicated}");
            });
            ClientController.Clients.Clear();
        }


        public static void RemoveClient(int id)
        {
            ClientController.Clients.RemoveAt(ClientController.Clients.FindIndex(x => x.Id == id));
        }
        internal static void DisposeCertainConnection()
        {
        start:
            Console.WriteLine("Please enter connection id to dispose");
            var response = Console.ReadLine();
            switch (response)
            {
                case "ShowClients":
                    ClientController.ShowClients();
                    goto start;
                default:
                    if (int.TryParse(response, out int res))
                    {
                        var client = ClientController.Clients.FirstOrDefault(x => x.Id == res);
                        Console.WriteLine($"Diposing Connection {client.Connection.RemoteEndPoint}, loggedId {client.Id}, dbAccountId {client.AccountId}, Is authenicated {client.Authenicated}");
                    }
                    else if (response == "cancel")
                        return;
                    else
                    {
                        Console.WriteLine("Internal error, input doesn't equal integer, please provide the appropriate id to dispose!");
                        ClientController.ShowClients();
                        goto start;
                    }
                    break;
            }
        }
    }
}
