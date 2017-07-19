using System;
using System.IO;
using System.Web;
using ZCZJ_DPF;

public class DownloadFile
{
    public static void Send(HttpContext context, string fileName)
    {
        HttpResponse response = context.Response;
        HttpCookie cookie = context.Request.Cookies["downloadstatus"];
        if (cookie == null)
        {
            cookie = new HttpCookie("downloadstatus", String.Empty);
        }
        HttpCookie errorCookie = new HttpCookie("downloaderror", string.Empty);
        FileInfo TheFile = new FileInfo(fileName);
        if (TheFile.Exists)
        {
            response.Clear();
            cookie.Value = "success";
            response.SetCookie(cookie);
            response.Buffer = true;
            
            response.ContentType = GetContentType(TheFile.Name);
            response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(TheFile.Name, System.Text.Encoding.UTF8));
            response.Charset = "GB2312";//设置输出流的字符集-中文
            response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流的字符集
            response.WriteFile(TheFile.FullName);
            response.Flush();
            response.End();
            TheFile.Delete();
        }
        else
        {
            cookie.Value = "fail";
            errorCookie.Value = "File " + TheFile.Name + " doesn't exists on the server";
            response.SetCookie(cookie);
            response.SetCookie(errorCookie);
        }


    }

    public static string GetContentType(string fileName)
    {
        string contentType = "application/octetstream";
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (registryKey != null && registryKey.GetValue("Content Type") != null)
            contentType = registryKey.GetValue("Content Type").ToString();
        return contentType;
    }

}
