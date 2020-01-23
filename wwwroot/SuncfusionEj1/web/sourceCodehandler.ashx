<%@ WebHandler Language="C#" Class="sourceCodehandler" %>

using System;
using System.Web;
using System.IO;

public class sourceCodehandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        try
        {
            string url = context.Request["url"];
            url = HttpContext.Current.Server.MapPath(url);
            //Pass the file path and file name to the StreamReader constructor
            using (StreamReader reader = new StreamReader(url))
            {
                String line;
                line = reader.ReadLine();
                while (line != null)
                {
                    context.Response.Write(line + "\n");
                    line = reader.ReadLine();

                }

            }
        }
        catch (Exception e) { }     
        context.Response.ContentType = "text/html";
       
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}