using System.IO;
using System.Text;
using Newtonsoft.Json;
using RCServerCli.Models;

namespace RCServerCli.DataHandlers
{
    public class ConfigManager
    {
        public static void SetupConfig(ProjectConfig config)
        {
            if(!File.Exists("Config.json"))
            {
                using (FileStream fs = File.Create("Config.json"))
                {
                    
                }
                File.WriteAllText("Config.json", JsonConvert.SerializeObject(config));

            }
            else
            {
                File.WriteAllText("Config.json", JsonConvert.SerializeObject(config));
            }
        }

        public static string GetConnectionString()
        {
            var result = JsonConvert.DeserializeObject<ProjectConfig>(File.ReadAllText("Config.json"));
            return $"Server={result.Ip};Database={result.TableName};User ID={result.UserName};Password='{result.Password}';";
        }
    }
}