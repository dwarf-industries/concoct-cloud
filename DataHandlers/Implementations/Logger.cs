namespace Rokono_Control.DataHandlers.Implementations
{
    using Rokono_Control.DataHandlers.Interfaces;
    using System;
    using System.IO;

    public class Logger : ICustomLogger
    {
        void ICustomLogger.Error(string message)
        {
            var logData = $"{DateTime.Now.ToString()}: {message}";
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(logData);
            Console.ResetColor();
        }

        void ICustomLogger.Info(string message)
        {
            var logData = $"{DateTime.Now.ToString()}: {message}";
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(logData);
            Console.ResetColor();
        }

        void ICustomLogger.Message(string message)
        {
            var logData = $"{DateTime.Now.ToString()}: {message}"; 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(logData);
            Console.ResetColor();
        }

        void ICustomLogger.Warning(string message)
        {
            var logData = $"{DateTime.Now.ToString()}: {message}";
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(logData);
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
