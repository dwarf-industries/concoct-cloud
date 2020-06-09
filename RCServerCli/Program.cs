using System;
using System.Linq;
using Newtonsoft.Json;
using RCServerCli.DatabaseHandlers;
using RCServerCli.DataHandlers;
using RCServerCli.Models;

namespace RCServerCli
{
    class Program
    { 
        
        static void Main(string[] args)
        {         
            System.Console.WriteLine("Attention before using any of the tool commands make sure that you setup your configuration using -Init");
            Console.WriteLine("Welcome to Rokono Control admin pannel, please type --help to view the full list of commands.");
            System.Console.WriteLine("If you're running a desktop enviroment there is a GUI client available that has the same functionality as this tool.");
            System.Console.WriteLine("RCAP is a tool intended to be used on the server, please refrain from giving access to people that don't know what they are doing as this may damage your existing projects!!!");
            var userId = default(int);
            var projectId = default(int);
            var isValid = false;
            var secondUser = default(int);
            for(int i = 0; i < args.Length; i++)
            {
                switch(args[i])
                {
                    case "-Init":
                        System.Console.WriteLine("Username");
                        var dbUsername = Console.ReadLine();
                        System.Console.WriteLine("Password");
                        var dbPassword = Console.ReadLine();
                        System.Console.WriteLine("Database IP");
                        var dbIp = Console.ReadLine();
                        System.Console.WriteLine("Database name");
                        var dbName = Console.ReadLine();
                        ConfigManager.SetupConfig(new ProjectConfig{
                            Password = dbPassword,
                            TableName = dbName,
                            UserName = dbUsername,
                            Ip = dbIp
                        });
                        System.Console.WriteLine("Configuration created");
                    break;
                    case "-AUP":
                        userId = default(int);
                        isValid = int.TryParse(args[i + 1].ToString(), out userId);
                        isValid = int.TryParse(args[i + 2].ToString(), out projectId);
                        
                        if(isValid)
                        {
                           
                            System.Console.WriteLine("Setup user rights.");
                            System.Console.WriteLine("Project wide rights:");
                            var answer = string.Empty;
                            var j = 1;
                            var rights = new UserRights{
                                Documentation = 0,
                                ChatChannelsRule = 0,
                                ManageIterations = 0,
                                ManageUserdays = 0,
                                UpdateUserRights = 0,
                                ViewOtherPeoplesWork = 0,
                                WorkItemRule = 0
                            };
                            var prev = default(int);
                            start:
                            switch(j)
                            {
                                case 1:
                                    System.Console.WriteLine("Allow to manage work items? yes/no?");
                                    prev = j;
                                    answer = Console.ReadLine();
                                    j = CheckUserInput(answer, j);
                                    System.Console.WriteLine(j);
                                    if(j == prev)
                                    {
                                        System.Console.WriteLine("Please answer with yes or no");
                                        goto start; 
                                    }
                                    else if(answer.ToLower()  == "yes")  
                                        rights.WorkItemRule = 1;
                                    goto start;                                
                                break;
                                case 2:
                                    System.Console.WriteLine("Allow to manage chat channels? yes/no?");
                                    prev = j;
                                    answer = Console.ReadLine();
                                    j = CheckUserInput(answer, j);
                                    if(j == prev)
                                    {
                                        System.Console.WriteLine("Please answer with yes or no");
                                        goto start; 
                                    }
                                    else if(answer.ToLower()  == "yes")    
                                        rights.ChatChannelsRule = 1;
                                    goto start;                                
                                break;
                                case 3:
                                    System.Console.WriteLine("Allow to manage user rights? yes/no?");
                                    prev = j;
                                    answer = Console.ReadLine();
                                    j = CheckUserInput(answer, j);
                                    if(j == prev)
                                    {
                                        System.Console.WriteLine("Please answer with yes or no");
                                        goto start; 
                                    }
                                    else if(answer.ToLower()  == "yes")     
                                        rights.UpdateUserRights = 1;
                                    goto start;                                
                                break;
                                case 4:
                                    System.Console.WriteLine("Allow to manage Iterations? yes/no?");
                                    prev = j;
                                    answer = Console.ReadLine();
                                    j = CheckUserInput(answer, j);
                                    if(j == prev)
                                    {
                                        System.Console.WriteLine("Please answer with yes or no");
                                        goto start; 
                                    }
                                    else if(answer.ToLower()  == "yes")     
                                        rights.ManageIterations = 1;
                                    goto start;                                
                                break;
                                case 5:
                                    System.Console.WriteLine("Allow to manage user work days? yes/no?");
                                    prev = j;
                                    answer = Console.ReadLine();
                                    j = CheckUserInput(answer, j);
                                    if(j == prev)
                                    {
                                        System.Console.WriteLine("Please answer with yes or no");
                                        goto start; 
                                    }
                                    else if(answer.ToLower()  == "yes")      
                                        rights.ManageUserdays = 1;
                                    goto start;                                
                                break;
                                case 6:
                                    System.Console.WriteLine("Allow to manage view other peoples work? yes/no?");
                                    prev = j;
                                    answer = Console.ReadLine();
                                    j = CheckUserInput(answer, j);
                                    if(j == prev)
                                    {
                                        System.Console.WriteLine("Please answer with yes or no");
                                        goto start; 
                                    }
                                    else if(answer.ToLower() == "yes")     
                                        rights.ViewOtherPeoplesWork = 1;
                                    goto start;                                
                                break;
                                case 7:
                                    System.Console.WriteLine("Allow to manage documentation? yes/no?");
                                    prev = j;
                                    answer = Console.ReadLine();
                                    j = CheckUserInput(answer, j);
                                    if(j == prev)
                                    {
                                        System.Console.WriteLine("Please answer with yes or no");
                                        goto start; 
                                    }
                                    else if(answer.ToLower() == "yes")     
                                        rights.ViewOtherPeoplesWork = 1;
                                    goto start;                                
                                break;
                            }

                            using (var context = new DatabaseController())
                            {
                                context.AddUserToProject(userId,projectId, rights);
                            }
                        }
                        else
                            System.Console.WriteLine("Miss matching argument after List Project User, expected INT Id");
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
                    case "-RU":
                        System.Console.WriteLine("First Name");
                        var firstName = Console.ReadLine();
                        System.Console.WriteLine("Last Name");
                        var lastName = Console.ReadLine();
                        System.Console.WriteLine("Please add a username for your account prefferably the same as your git username.");
                        var username = Console.ReadLine();
                        System.Console.WriteLine("Please add a email for your account:");
                        var email = Console.ReadLine();
                        System.Console.WriteLine("Please choose a password or leave blank to get one generated for you.");
                        var accountPassword = Console.ReadLine();
                        System.Console.Write("Administrator yes/no");
                        var isAdmin = Console.ReadLine() == "yes" ? true :false;

                       
                        using(var context = new DatabaseController())
                        {
                            context.CreateUser(firstName,lastName,username,email,accountPassword,isAdmin, null);
                        }
                    break;
                    case "--Help":
                        ShowHelpMenu();
                        break;
                }  
            }
        }

        private static int CheckUserInput(string answer, int position)
        {
            var normalize = answer.ToLower();
            switch(normalize)
            {
                case "yes":
                    return position+1;
                case "No":
                    return position+1;
                default:
                    return position;
            }
        }

        private static void ShowHelpMenu()
        {
            System.Console.WriteLine("-AUP [user, projectId]: takes two arguments, usage -AUP 1 1. Assigns a user to a given project.");
            System.Console.WriteLine("-RU: Registers a user in the system.");
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