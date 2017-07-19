using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace ZCZJ_DPF.TM_Data
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ProcessHandler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "";
            if (context.Request.Files.Count > 0)
            {
                string file = context.Request.Files[0].FileName;
                string[] files = file.Split('\\');
                string filename = files[files.Length - 1];
                string filepath = @"E:\技术管理附件\工艺类卡片";
                string guanlianTime = DateTime.Now.ToString("yyyyMMddHHmmssff");
                string url = @"E:\技术管理附件\工艺类卡片\" + guanlianTime+filename;
                if (!File.Exists(url))
                {

                    string sqlStr = "insert into TBPM_FILES(BC_CONTEXT,fileLoad,fileUpDate,fileName)";
                    sqlStr += "values('" + guanlianTime + "'";
                    sqlStr += ",'" + filepath + "'";
                    sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                    sqlStr += ",'" + filename + "')";

                    //打开与数据库的连接
                    DBCallCommon.ExeSqlText(sqlStr);
                    context.Request.Files[0].SaveAs(url);
                    result = "{\"msg\":\"上传成功！\",\"guanlianTime\":\"" + guanlianTime + "\",\"filename\":\"" + filename + "\"}";

                }
                else
                {
                    result = "{\"msg\":\"该文件与服务器某一文件重名，请核对后在上传！\"}";
                }


            }
            else
            {

                result = "{\"msg\":\"请选择文件！\"}";
            }


            context.Response.Write(result);
            context.Response.End();
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
