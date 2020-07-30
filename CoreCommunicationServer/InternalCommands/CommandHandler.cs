using System;
using System.Linq;

namespace WWServer.InternalCommands
{
    public class CommandHandler
    {
        public static void SetClientSpeed()
        {
            Console.WriteLine("Supply user id");
            var userId = int.Parse(Console.ReadLine());
            Console.WriteLine("Supply user speed");
            var speed = float.Parse(Console.ReadLine());
            ClientController.Clients.FirstOrDefault(x => x.Id == userId).MovementSpeed = speed;

        }

        internal static void SetPosition()
        {
            Console.WriteLine("Input the value of X: ");
            var x = float.Parse(Console.ReadLine());
            Console.WriteLine("Input the value of Y: ");
            var y = float.Parse(Console.ReadLine());
            Console.WriteLine("Input the value of Z: ");
            var z = float.Parse(Console.ReadLine());
            Console.WriteLine("Input a user Id");
            var id = int.Parse(Console.ReadLine());


        }
    }
}
