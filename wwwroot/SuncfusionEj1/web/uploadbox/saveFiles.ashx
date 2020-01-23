<%@ WebHandler Language="C#" Class="saveFiles" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;

public class saveFiles : IHttpHandler
{
 public void ProcessRequest(HttpContext context)
    {
        string targetFolder = HttpContext.Current.Server.MapPath("uploadfiles");
        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
        }
        HttpRequest request = context.Request;
      
        string requestPath = context.Request.Path;
        HttpFileCollection uploadedFiles = context.Request.Files;
        if (uploadedFiles != null && uploadedFiles.Count > 0 && uploadedFiles[0].FileName!="")
        {
            for (int i = 0; i < uploadedFiles.Count; i++)
            {
                if (uploadedFiles[i].FileName != null && uploadedFiles[i].FileName != "")
                {
                    string fileName = uploadedFiles[i].FileName;
                    int indx = fileName.LastIndexOf("\\");
                    if (indx > -1)
                    {
                        fileName = fileName.Substring(indx + 1);
                    }
                    // uploadedFiles[i].SaveAs(targetFolder + "\\" + fileName);
                }
            }
            // To maintain the same web page instead of navigate to another page in Synchronous upload, we have sent response back.
            if (requestPath.Contains("synchronous.html") || requestPath.Contains("saveFiles.ashx"))
            {
                context.Response.Write("<span id='successmsg'> Successfully uploaded</span>");

                context.Response.WriteFile("uploadIframe.html");
                
            }
        }
        else
        {
            // To maintain the same web page instead of navigate to another page in Synchronous upload, we have sent response back.

            if (requestPath.Contains("synchronous.html") || requestPath.Contains("saveFiles.ashx"))
            {
                context.Response.Write("<span id='successmsg'> Successfully uploaded</span>");
                context.Response.WriteFile("uploadIframe.html");
            }
        }


    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}