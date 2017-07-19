using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;

namespace ZCZJ_DPF.PL_Data
{
    public partial class MainPlan_AjaxHandler : System.Web.UI.Page
    {
        string result;
        protected void Page_Load(object sender, EventArgs e)
        {
            String methodName = Request["method"];
            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);

            if (method == null) throw new Exception("method is null");
            try
            {

                method.Invoke(this, null);
            }
            catch
            {
                throw;
            }
        }

        public void UploadPLFiles()
        {
            if (Request.Files.Count > 0)
            {
                string file = Request.Files[0].FileName;
                string[] files = file.Split('\\');
                string filename = files[files.Length - 1];
                string filepath = @"E:\进度管理附件\主生产计划";
                string guanlianTime = DateTime.Now.ToString("yyyyMMddHHmmssff");
                string url = @"E:\进度管理附件\主生产计划\" + filename;
                if (!File.Exists(url))
                {
                    List<string> listStr = new List<string>();
                    string sqlStr = "insert into TBMP_FILES(BC_CONTEXT,fileLoad,fileUpDate,fileName)";
                    sqlStr += "values('" + guanlianTime + "'";
                    sqlStr += ",'" + filepath + "'";
                    sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                    sqlStr += ",'" + filename + "')";
                    listStr.Add(sqlStr);
                    sqlStr = "insert into TBMP_MAINPLANPROFILES values ('"+Session["UserId"].ToString()+"','"+guanlianTime+"')";
                    listStr.Add(sqlStr);
                    try
                    {
                        Request.Files[0].SaveAs(url);
                        DBCallCommon.ExecuteTrans(listStr);    
                        result = "{\"msg\":\"上传成功！\",\"filename\":\"" + filename + "\"}";
                    }
                    catch (Exception)
                    {
                        result = "{\"msg\":\"上传失败！\"}";
                    }
                  

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


            Response.Write(result);
        }
    }
}
