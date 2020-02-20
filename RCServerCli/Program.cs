using System;
using System.Linq;
using Newtonsoft.Json;
using RCServerCli.DatabaseHandlers;
using RCServerCli.DataHandlers;

namespace RCServerCli
{
    class Program
    { 
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Rokono Control admin pannel, please type --help to view the full list of commands.");
            System.Console.WriteLine("If you're running a desktop enviroment there is a GUI client available that has the same functionality as this tool.");
            System.Console.WriteLine("RCAP is a tool intended to be user on the server, please refrain from giving access to people that don't know what they are doing as this may damage your existing projects!!!");
            var userId = default(int);
            var projectId = default(int);
            var isValid = false;
            var secondUser = default(int);
            for(int i = 0; i < args.Length; i++)
            {
                switch(args[i])
                {
                  
                    case "Create Account":
                        Console.Write("Username: ");
                        var user = Console.ReadLine();
                        System.Console.WriteLine("");
                        System.Console.Write("Password: ");
                        var password = Console.ReadLine();
                        System.Console.WriteLine("");
                        System.Console.Write("Administrator yes/no");
                        var isAdmin = Console.ReadLine() == "yes" ? true :false;
                        using(var context = new DatabaseController())
                        {
                            context.CreateUser(user,password,isAdmin);
                        }
                    break;
                    case "-LP":
                        using(var context = new DatabaseController())
                        {
                            context.ListAllProjects();
                        }
                    break;
                    case "-LPU":
                        var id = default(int);
                        isValid = int.TryParse(args[i + 1].ToString(), out id);
                        if(isValid)
                            using (var context = new DatabaseController())
                            {
                                context.GetProjectsForUser(id);
                            }
                        else
                            System.Console.WriteLine("Miss matching argument after List Project User, expected INT Id");
                    break;
                    case "-LPRU":

                        bool validateUser = int.TryParse(Console.ReadLine(), out userId);
                        bool validateProjectId = int.TryParse(Console.ReadLine(), out projectId);

                        if(validateUser && validateProjectId)
                            using (var context = new DatabaseController())
                            {
                                context.GetUserRightsForProject(userId,projectId);
                            }
                        else
                            System.Console.WriteLine("Miss matching argument after -LPRU, expected INT INT (userId, projectId");
                    break;
                    case "-LU":
                        using(var context = new DatabaseController())
                            context.ListAllUsers();
                    break;
                    case "-CUP":
                        var userPasswordId = default(int);
                        System.Console.WriteLine("Please choose an user ID");
                        isValid = int.TryParse(Console.ReadLine(), out userId);
                        System.Console.WriteLine("New Password");
                        var passowrd = Console.ReadLine();
                        if(isValid)
                        {
                            using(var context = new DatabaseController())
                            {
                                context.ChangeUserPassword(userPasswordId, passowrd);
                            }
                        }
                        else
                            System.Console.WriteLine($"UserId must be an int, given input is {userPasswordId}");
                    break;
                    case "-PCUP":

                        System.Console.WriteLine("Please choose an user ID");
                        isValid = int.TryParse(Console.ReadLine(), out userId);
                        if(isValid)
                        {
                            using(var context = new DatabaseController())
                            {
                                context.GenerateMailPasswordRequest(userId);
                            }
                        }
                        else
                            System.Console.WriteLine($"UserId must be an int, given input is {userId}");
                    break;
                    case "-BKP":
                        isValid = int.TryParse(Console.ReadLine(), out projectId);
                        if(isValid)
                        {
                            using(var context = new DatabaseController())
                            {
                               var project = context.BackUpSpecificProject(projectId);
                               Backupwriter.CreateBackup($"{project.CurrentProject.ProjectTitle}.json", JsonConvert.SerializeObject(project));
                               System.Console.WriteLine($"Project backup with the name {project.CurrentProject.ProjectTitle},json has been created in the root directory of the tool");
                            }
                        }
                        else
                            System.Console.WriteLine($"ProjectId must be an int, given input is {projectId}");
                    break;
                    case "-SBK":
                        using(var context = new DatabaseController())
                            context.ServerBackup();
                    break;
                    case "-RMUP":
                        System.Console.WriteLine("Input the id of the user that you want to remove");
                        isValid = int.TryParse(Console.ReadLine(), out userId);
                        System.Console.WriteLine("Input the id of the project from which the user is going to be removed");
                        isValid = int.TryParse(Console.ReadLine(), out projectId);
                        if(isValid)
                        {
                            using(var context = new DatabaseController())
                            {
                                context.RemoveUserFromProjectClean(userId, projectId);
                            }
                        }
                         else
                        {
                            System.Console.WriteLine($"UserId must be an int, given input is {userId}");
                            System.Console.WriteLine($"ProjectId must be an int, given input is {projectId}");
 
                        }
                    break;
                    case "-RMUPAU":
                    
                        System.Console.WriteLine("Input the id of the user that you want to remove");
                        isValid = int.TryParse(Console.ReadLine(), out userId);
                        System.Console.WriteLine("Input the id of the project from which the user is going to be removed");
                        isValid = int.TryParse(Console.ReadLine(), out projectId);
                        isValid = int.TryParse(Console.ReadLine(), out secondUser);
                        if(isValid)
                        {
                            using(var context = new DatabaseController())
                            {
                                context.RemoveUserFromProjectAssing(userId, projectId, secondUser);
                            }
                        }
                        else
                        {
                            System.Console.WriteLine($"UserId 1 must be an int, given input is {userId}");
                            System.Console.WriteLine($"ProjectId must be an int, given input is {projectId}");
                            System.Console.WriteLine($"UserId 2 must be an int, given input is {secondUser}");

                        }
                    break;
                    case "--Help":
                        ShowHelpMenu();
                        break;
                }  
            }
        }   

        private static void ShowHelpMenu()
        {
            System.Console.WriteLine("Create Account- takes 3 parameters, username, password and admin status. It will prompt to input the parameters 1 by one, it is advised to keep only one administrator account");
            System.Console.WriteLine("-LP:  Lists all projects created in the server instance, returns a table of projectId, Project Name, Project member count");
            System.Console.WriteLine("-LPU: Lists all projects assigned to a user");
            System.Console.WriteLine("-LPRU: Lists all user assigned rights for a given project, takes as input userId and projectId");
            System.Console.WriteLine("-LU: Lists all registered users, returns a table of all registered users. ");
            System.Console.WriteLine("-CUP: Changes the password of a given user, takes as input user id, new passowrd");
            System.Console.WriteLine("-PCUP: Generates a mail request if the smtp options are set so that the given user can change his own password in the webplatform.");
            System.Console.WriteLine("-BKP: Creates a backup of a specific project and exports it to a file.");
            System.Console.WriteLine("-SBK: Creates a backup of the whole server instance.");
            System.Console.WriteLine("-RMUP: Removes a user from a project, all user assigned tasks and relations will be unassigned");
            System.Console.WriteLine("-RMUPAU: Removes a user from a project, assigns all user work item data to another specific user");

        }
    }
}
