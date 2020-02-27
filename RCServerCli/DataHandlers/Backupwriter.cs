using System.IO;

namespace RCServerCli.DataHandlers
{
    public class Backupwriter
    {
        public static void CreateBackup(string filename, string content)
        {
            if(File.Exists(filename))
                File.Delete(filename);
        
            using(var file = File.Create(filename))
            {
            }

            File.WriteAllText(filename,content);
        }

    }
}