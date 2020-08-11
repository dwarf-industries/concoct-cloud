using System;
using System.Linq;

namespace Platform.DataHandlers
{
    public class IdGenerator
    {
        public static string GetRandomId()
        {
            var second = DateTime.Now.Second;
            var alphabet =  ("ABCDEFGHIJKLMNOPQRSTUVWXYZ").ToList();
            var max = alphabet.Count;
            var random = new Random();
            var selected = random.Next(0, max);
            return $"{alphabet.ElementAt(selected)}{second}";

        }
    }
}