<%@ WebHandler Language="C#" Class="removeFiles" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;

public class removeFiles : IHttpHandler
{
   public void ProcessRequest(HttpContext context)
    {
        System.Collections.Specialized.NameValueCollection s = context.Request.Params;
        string fileName = s["fileNames"];
        string targetFolder = HttpContext.Current.Server.MapPath("uploadfiles");
        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
        }

        string physicalPath = targetFolder + "\\" + fileName;
        if (System.IO.File.Exists(physicalPath))
        {
            System.IO.File.Delete(physicalPath);
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