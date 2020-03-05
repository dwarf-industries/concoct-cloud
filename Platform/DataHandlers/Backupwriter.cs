using System.IO;
using Newtonsoft.Json;
using Platform.Models;

namespace Platform.DataHandlers
{
    public class Backupwriter
    {
        public static string CreateBackup(string filename, OutboundBackupModel content)
        {
            if(File.Exists(filename))
                File.Delete(filename);
        
            using(var file = File.Create(filename))
            {

            }
            File.WriteAllText(filename,content.Serialized);

            return content.Serialized;
        }

    }
}