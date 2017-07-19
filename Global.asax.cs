using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;

namespace ZCZJ_DPF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {           
            
            if (Application["online"] == null || Application["online"].ToString() == "")
            {
                Application["online"] = 0;
            }
            //string ip = Common.GetIPAddress();
            //string userHostName = Common.GetUserHostName();
            //string sqlText = "insert into tbbd_session (sn_id,sn_crttm,sn_ip,sn_userhostname,sn_type,sn_num) values('','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ip + "','" + userHostName + "','Application_start','" + (int)Application["online"] + "')";
            //DBCallCommon.ExeSqlText(sqlText);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //if (Request.Path.Contains("Index.aspx"))
            //{
            Application.Lock();//锁定后，只有这个Session能够会话
            int num = (int)Application["online"];
            Application["online"] = num + 1;
            Application.UnLock();//会话完毕后解锁
            //}

            //string url = Request.FilePath;
            //string ip = Common.GetIPAddress();
            //string userHostName = Common.GetUserHostName();
            //string sqlText = "insert into tbbd_session (sn_id,sn_crttm,sn_ip,sn_userhostname,sn_type,sn_num,SN_URL) values('" + Session.SessionID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ip + "','" + userHostName + "','Session_start','" + (int)Application["online"] + "','"+url+"')";
            //DBCallCommon.ExeSqlText(sqlText);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
           

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["online"] = (int)Application["online"] - 1;
            Application.UnLock();

            //string ip = Common.GetIPAddress();
            //string userHostName = Common.GetUserHostName();
            //string sqlText = "insert into tbbd_session (sn_id,sn_crttm,sn_ip,sn_userhostname,sn_type,sn_num) values('" + Session.SessionID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ip + "','" + userHostName + "','Session_End','" + (int)Application["online"] + "')";
            //DBCallCommon.ExeSqlText(sqlText);
        }

        protected void Application_End(object sender, EventArgs e)
        {
           
        }
    }
}