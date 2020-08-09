namespace WWServer
{
    using WWServer.InternalCommands;
    using WWServer.ServerCommands;

    class InternalServerCommands
    {
        public static void CallServerConsoleCmd(string cmd)
        {
            switch (cmd)
            {
                case "Clear_Logged_Users":
                    Commands.DisposeConnections();
                    break;
                case "Clear_Logged_Users /r":
                    Commands.DisposeCertainConnection();
                    break;
                case "List Clients":
                    ClientController.ShowClients();
                    break;
                case "/SetSpeed":
                    CommandHandler.SetClientSpeed();
                    break;
                case "/SetPossition":
                    CommandHandler.SetPosition();
                    break;
                default:
                    break;
            }
        }
    }
}
