using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace ZCZJ_DPF.QC_Data
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class QC_PicHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Files.Count > 0)
            {
                string funcNum = context.Request.QueryString["CKEditorFuncNum"];
                string file = context.Request.Files[0].FileName;
                string filepath=context.Server.MapPath("Pic");
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssff");
                string url = filepath + "\\" + newFileName;
                if (!File.Exists(url))
                {
                    System.IO.Directory.CreateDirectory(filepath);


                    context.Request.Files[0].SaveAs(url);

                    //context.Response.Write("<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(" + funcNum + ",'" + "Pic/" + newFileName + "','');</script>");
                    context.Response.Write("Pic/" + newFileName);

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
}
