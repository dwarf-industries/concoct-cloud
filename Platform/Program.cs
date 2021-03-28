using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Platform.Models;
using RokonoControl.Models;

namespace Rokono_Control
{
    public class Program
    {
        public static bool HasCompleate { get; set; }
        public static Config Configuration {get; set;}
        public static List<HubMappedMembers> Members { get; set; }
        public static void Main(string[] args)
        {
            if(!File.Exists("Configuration.txt"))
                 Configuration = CreateFile("Configuration.txt");
            else
                Configuration = ReadConfig("Configuration.txt");

            Members = new List<HubMappedMembers>();
     //       InitCron();
            var current = OS.GetCurrent();
            System.Console.WriteLine(current);
 

            var result = RepositoryManager.CommandOutput(current,$@"git log", @"D:\RokonoControl\RokonoControl");
            CreateWebHostBuilder(args).Build().Run();
        }
        
        private static Config CreateFile(string v)
        {
            var configuration = new Config
            {
                ShellScripts = new List<ConfigBindingData>
                {
                    new ConfigBindingData { Name = "CreateProj.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl/CreateProj.sh"},
                    new ConfigBindingData { Name = "AssignGroup.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl/AssignGroup.sh"},
                    new ConfigBindingData { Name = "GetBranches.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl/GetBranches.sh"},
                    new ConfigBindingData { Name = "GetGitList.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl/GetGitList.sh"},
                    new ConfigBindingData { Name = "GetCommitData.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl/GetCommitData.sh"},
                    new ConfigBindingData { Name = "GetCommitFile.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl/GetCommitFile.sh"},
                    new ConfigBindingData { Name = "LsFiles.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl/LsFiles.sh"},
                    new ConfigBindingData { Name = "GetGitList.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl/GetGitList.sh"},
                    new ConfigBindingData { Name = "GetCommitFile.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl/GetCommitFile.sh"},
                    new ConfigBindingData { Name = "GetCommitFile.sh", Path = "/home/kristifordevelopment/ShellScripts/RokonoControl"},

                }
            };
            var config = JsonConvert.SerializeObject(configuration);
            if(!File.Exists(v))
            {
                var cFile = File.Create(v);
                cFile.Close();
                var logWriter = new System.IO.StreamWriter(v);
                logWriter.WriteLine(config);
                logWriter.Dispose();
            }
         
            return configuration;
         }



        private static Config ReadConfig(string path)
        {
            
            var config = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(config);
        }

        //public static void InitCron(string repoName)
        //{
        //    using(var repositoryManager = new GitRepositoryManager())
        //    {
        //        repositoryManager.CollectCommits(repoName);
        //    }
            
        //}

        private static void timer1_Tick(object sender, EventArgs e)
        {
            
   
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:6005")
                 .UseStartup<Startup>();
    }
}
