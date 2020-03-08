
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FKristiforMilchev%2FRokonoControl.svg?type=shield)](https://app.fossa.io/projects/git%2Bgithub.com%2FKristiforMilchev%2FRokonoControl?ref=badge_shield)


Rokono Control is an open source project management system that combines lots of different features. Its based over the agile project management metodologies and the default planning workflow follows the pattern of Epic->Feature->User Story -> Task | Bug | Issue. It offers dynamic user interface allowing to manage multiple projects at the same time, assign team members, manage member rights. One of its core features is to connect team members while allowing agile workflow during the development of the product that you're working on, thus we are integrating internal messaging systems so you can connect with your team members without the use of third party applications. I am also working on personalized experiences so that developers can focus on their work while administrators focus on user rights management and managers can focus on planning boards and investors can focus on dashboards with charts that show  their project progress. The software comes with a set of tools that allow server administrators to easily backup all projects and move them to a different server instance on the fly.

The software is still in active development and is not officially released modules brake from day to day while i work on it so if you're using a build that is working for you
at the moment please do not update it daily if you don't want to get some changes that might brake your workflow. Some of the mentioned features and other that aren't added to the readme are still in early development stage and are not part of the Repository
## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

The project is created using asp.net core 3.1, its not based on blazor because its a version that is being migrated from asp.net core v 1.0 so you need to get the proper runtime from Microsoft.
The Database Layer is written in entity framework core using Database First approach.
Currently we offer two database files that are inside the RokonoContol/Platform directory MSSQLGenerationScript.sql contains the creations scripts for MSSQL server
and MySqlDatabaseGenerationScript.sql contains the scripts for MySql databases, currently the default database that is used for testing is MSSQL so if you're doing a clone make sure to scaffold your proper database.

In order to install .net core runtime go to this link https://dotnet.microsoft.com/download all UI components are delivered trough CDN but if you'd like you can go and download them from Syncfusion 

Database of your choice either MSSQL or MySql.

### Installing
 
 -You can easily get the system running, all you need to do in order to get started is to install the dependencies mentioned above and then you need to navigate to the root directory of the project
 open up powershell on windows or any terminal on your favorite linux distribution and execute the following command cd Platform && dotnet -c Release -r target patform for linux (linux-x64)
 -Once that is done you need to navigate to /Platform/bin/Release/netcoreapp3.1/publish take the content of that folder and deploy it on your favorite server like NGINX 
 -Edit the configuration file inside NGINX create a systemctl service to start automatically under linux https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-3.1 here is a good tutorial by Microsoft

The last step is to deploy your database either by adding it to an existing server or installing either MSSQL or MySQL server. 

- [MySQL server installation guide](https://dev.mysql.com/doc/mysql-installation-excerpt/5.7/en/) 
- [MSSQL server installation guide](https://docs.microsoft.com/en-us/sql/database-engine/install-windows/install-sql-server?view=sql-server-ver15)

Once you have installed or deployed server its time to deploy the database, you can do that by navigating inside the cloned project folder then click on the folder named Platform and find the corresponding creation script for your database environment. 

-For MySql use: MySqlDatabaseGenerationScript.sql
-For MSSQL use: MSSQLGenerationScript.sql

If its all setup its time to scaffold your context in order to connect everything together, the project uses database first approach so its important to scaffold the database when you're deploying on a new hosting environment it might work without it just by changing the connection string although i advise of doing a fresh scaffold each time you upgrade.

For MSSQL Server use this guide: 
https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx
For MySQL:
Use the same guide but change  Microsoft.EntityFrameworkCore.SqlServer to Pomelo.EntityFrameworkCore.MySql
Detailed guide if you're converting the databases to use another engine or server can be found here :
https://docs.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli

## Deploying

The last step is to initialize the default server configuration, go to the rokono-cl folder. Make sure that you build the tool and deploying only on the hosting environment, do not give access to people that don't have administrative status as this tool can manage all projects deployed on the system.

Once you have deploying the tool use rokono-cl --Help for the full set of commands, make sure that you create remote administrator account, initialize default project setups and you should be ready to login create your first project 

## Contributing

Please read [CONTRIBUTING.md](https://github.com/KristiforMilchev/RokonoControl/blob/master/Contributing.md) for details on our code of conduct, and the process for submitting pull requests to us.

For a list of planned features and tasks you can view the project public board at:


https://platform.rokonocontrol.com/Boards/PublicBoard?projectId=5012&iteration=1004&person=0
## Authors

* **Kristifor Milchev** - *Initial work* - (https://github.com/kristiformilchev)

See also the  list of [contributors](https://github.com/KristiforMilchev/RokonoControl/blob/master/CODE_OF_CONDUCT.md) who participated in this project.

## License

This project is licensed under the Non-Profit Open Software License 3.0 (NPOSL-3.0) - see the [LICENSE.md](https://github.com/KristiforMilchev/RokonoControl/blob/master/License.md) file for details



[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FKristiforMilchev%2FRokonoControl.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2FKristiforMilchev%2FRokonoControl?ref=badge_large)
