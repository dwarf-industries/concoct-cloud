# RokonoControl
An open source project management software build around agile planning. 

An open source project for agile planning, buld as an alternative for all paid solutions that charge rediclious ammount of money
per month per user for a kanban board. Have in mind that the project is not finished yet and some parts of the build aren't working yet,
however it's a good alternative to most "free" solutions out there.

Technical details:

The webplatform is build around asp .net core, Javascript es 5, syncfusion and few shell scripts that are used to control pre git webhooks
Shell scripts usually handle automatic git repository creation, assigning users to teams and managing git rights.

All of the shell script features are currently disabled becuase i am migrating the scripts to the git webhooks.

The project includes basic features for agile planning, such as creating tasks, features, epics, issues, bugs, assigning them to 
team members managing teams and members for multiple projects at the same time.

The database is build in MSSQL server behind entity framework core, however for the MySql and ado.net fans you are free to convert
all Linq queries to sql and add a MySql database as well using the creation scripts from the backup file.

In order to build the solution please follow these steps:

git clone https://github.com/KristiforMilchev/RokonoControl,
Open the project directory and look for the file named DbCreation open the content and paste it in your editor of choice for sql
Create the database and then if you're using master please install entity framework tools globally and paste in the folllowing command


dotnet ef dbcontext scaffold "Server=ServerIp;Database=DatabaseName;User ID=YourSQlUser;Password='YourSqlPassword';"  Microsoft.EntityFrameworkCore.SqlServer -o Models -f

dotnet restore
dotnet build
dotnet public -c Release -r linux-x64 (Or whatever platform you're trying to build for) 
Add the content of your publish directory to your favorite server and route it to your own private network or public network.
Make sure to add the proper SSL certificates if you're using the prgram in production.


Current version:
Project - 0.0.1a
.net core version 3.1
