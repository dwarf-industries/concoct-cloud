namespace Rokono_Control.DataHandlers.Implementations
{
    using Rokono_Control.DataHandlers.Interfaces;
    using System;
    using System.IO;

    public class Logger : ICustomLogger
    {
        void ICustomLogger.Error(string message, string errorCode)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{DateTime.Now.ToString()}: [Error => {errorCode}]");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        void ICustomLogger.Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now.ToString()}: [Information]");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        void ICustomLogger.Message(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{DateTime.Now.ToString()}: [Message]");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        void ICustomLogger.Warning(string message, string warningCode)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now.ToString()}: [Warning => {warningCode}]");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }


        private void WriteFile(string message)
        {
            string path = @"Log.txt";
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine($"Starting Entry Log {DateTime.Now.ToString()}");
                  
                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(message);
            }

          
        }
    }
}
