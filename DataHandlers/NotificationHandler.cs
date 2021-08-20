using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using Platform.Models;
using Rokono_Control.Models;

namespace Platform.DataHandlers
{
    public class NotificationHandler : IDisposable
    {
        public IConfiguration Configuration;
        RokonoConfig Config;
        public NotificationHandler(IConfiguration configuration)
        {
            Configuration = configuration;
            Config = new RokonoConfig();
            Config.Username = Configuration.GetValue<string>("EmailConfiguration:Username"); 
            Config.Password = Configuration.GetValue<string>("EmailConfiguration:Password"); 
            Config.SMTP =   Configuration.GetValue<string>("EmailConfiguration:SMTP");
            Config.IMAP =  Configuration.GetValue<string>("EmailConfiguration:IMAP");
            Config.SmtpPort=  Configuration.GetValue<string>("EmailConfiguration:SmtpPort");  
            Config.ImapPort = Configuration.GetValue<string>("EmailConfiguration:ImapPort");  
            Config.CompanyName = Configuration.GetValue<string>("EmailConfiguration:CompanyName");
        }


        internal void GenerateNewWorkItemNotification(WorkItem workItem, UserAccounts accounts, string project, List<UserAccounts> receives, string subject)
        {
          
            // var configurationDetails = configData;
            var msgBody =$"<h1>Automatic Message from Concoct Cloud, licensed to {Config.CompanyName}</h1>";
            msgBody += $"<p> a new work item has been assigned to your account {accounts.GitUsername}, you can review it at your dasboard page for project {project}</p>";
            msgBody += $"<div style = 'width:300px;border-left:3px solid {GetCardColor(workItem.WorkItemTypeId)}; padding:2%;'> <h3 style='text-align:center;'> {workItem.Title} <h3> {GetWorkItemText(workItem)} </div>";
            receives.ForEach(x =>
            {
                SendEmail(Config, $"{x.FirstName} {x.LastName}", x.Email, subject, msgBody);

            });

        }

        internal void GeneraBacklogReport(List<WorkItem> items, UserAccounts account)
        {
            account.Email = "kristiformilchev615@gmail.com";

            var msgBody =$"<h1>Automatic Message from Rokono Control, licensed to {Config.CompanyName}</h1>";
            msgBody += $"<p> a new backlog email report has been generated for your account {account.GitUsername}, you for more details reffer to the system administrator managing your instance of Rokono Control at  {Config.CompanyName}";
            items.ForEach(x=>{
                msgBody += $"<div style = 'width:300px;border-left:3px solid {GetCardColor(x.WorkItemTypeId)}; padding:2%;'> <h3 style='text-align:center;'> {x.Title} <h3> {GetWorkItemText(x)} </div>";
            });
            SendEmail(Config, $"{account.FirstName} {account.LastName}", account.Email,  "Rokono Control new work item assigned", msgBody);

        }

        private string GetWorkItemText(WorkItem workItem)
        {
            var result = string.Empty;
            result += "<br/>";

            switch(workItem.WorkItemTypeId)
            {
                case 1:
                    result += "<h3>Reporoduce Steps<h3/>";
                    result += $"<p>{workItem.RepoSteps}</p>";
                    result += "<h3>System Information</p>";
                    result += $"<p>{workItem.SystemInfo}</p>";

                break;
                case 2:
                    result += "<h3>Description</h3>";
                    result += $"<p>{workItem.Description}</p>";
                    result += "<h3>Acceptence Criteria</h3>";
                    result += $"<p>{workItem.AcceptanceCriteria}</p>";
                break;

                case 3:
                    result += "<h3>Description</h3>";
                    result += $"<p>{workItem.Description}</p>";
                break;

                case 4:
                    result += "<h3>Description</h3>";
                    result += $"<p>{workItem.Description}</p>";
                break;

                case 5:
                    result += "<h3>Description</h3>";
                    result += $"<p>{workItem.Description}</p>";
                break;
                case 6:

                break;
                case 7:
                    result += "<h3>Description</h3>";
                    result += $"<p>{workItem.Description}</p>";
                    result += "<h3>Acceptence Criteria</h3>";
                    result += $"<p>{workItem.AcceptanceCriteria}</p>";
                break;
            }
            return result;
        }

        private string GetCardColor(int? workItemTypeId)
        {
            var result = string.Empty;
            switch(workItemTypeId)
            {
                case 1:
                    result = "#bf1111";
                break;
                case 2:
                    result = "#991ad9";
                break;
                  
                case 3:
                    result = "#cdd419";
                break;

                case 4:
                    result = "#13ba4d";
                break;

                case 5:
                    result = "#bf4e11";
                break;
                case 6:
                    result = "#bf1111";
                break;
                case 7:
                    result = "#12b0a3";
                break;
            }
            return "red";
        }

        private bool SendEmail(RokonoConfig configurationDetails, string name, string email, string Subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Subject, configurationDetails.Username));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject =Subject;
            
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
           // bodyBuilder.TextBody = "Hello World!";
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.Connect(configurationDetails.SMTP, int.Parse(configurationDetails.SmtpPort), true);
                client.Authenticate(configurationDetails.Username, configurationDetails.Password);
          
                client.Send(message);
                client.Disconnect(true);
            }
            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Configuration = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~NotificationHandler()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}