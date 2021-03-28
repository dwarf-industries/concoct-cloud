
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        public static string CommandOutput(string os, string command,  string workingDirectory = null)
        {
            switch(os)
            {
                case "win":
                    return ReadCommandOutputWin(command, workingDirectory);
                case "gnu":
                    return ExecuteCmd(workingDirectory, command);
            }
            return string.Empty;
        }


        private static string ReadCommandOutputWin(string command, string workingDirectory = null)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);

                procStartInfo.RedirectStandardError = procStartInfo.RedirectStandardInput = procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                if (null != workingDirectory)
                {
                    procStartInfo.WorkingDirectory = workingDirectory;
                }

                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                StringBuilder sb = new StringBuilder();
                proc.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    sb.AppendLine(e.Data);
                };
                proc.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    sb.AppendLine(e.Data);
                };

                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();
                return sb.ToString();
            }
            catch (Exception objException)
            {
                return $"Error in command: {command}, {objException.Message}";
            }
        }

        public static string ExecuteCmd(string arguments, string workingDiectory = null)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{arguments}\"",
                    // FileName = "ping",
                    //  Arguments = $"localhost",
                    WorkingDirectory = workingDiectory,
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