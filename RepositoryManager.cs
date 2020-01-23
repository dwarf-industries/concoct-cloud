
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Rokono_Control.Models;

namespace Rokono_Control
{
    public class RepositoryManager
    {
        public static bool AddNewProject(string repoName,string projectName, List<UserAccounts> accounts)
        {
            var current = OS.GetCurrent();
            System.Console.WriteLine(current);
            if(current == "gnu")
            { 
                try{
                    System.Console.WriteLine(ExecuteCmd("/bin/bash", $"{Program.Configuration.ShellScripts.FirstOrDefault(x=>x.Name == "CreateProj.sh").Path} {repoName} {projectName}"));  
    
                    accounts.ForEach(x=>{
                        System.Console.WriteLine(ExecuteCmd("/bin/bash", $"  {Program.Configuration.ShellScripts.FirstOrDefault(y=>y.Name == "AssignGroup.sh").Path} {projectName}Contribute {x.GitUsername}"));     
                    });
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return false;
                }
          
            }
            return true;
        }

        public static string ExecuteCmd(string os,string arguments)
        {
             var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{arguments}\"", 					 
                   // FileName = "ping",
                  //  Arguments = $"localhost",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}