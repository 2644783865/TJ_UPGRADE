using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF
{
    public partial class top : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Write("<script>alert('会话已过期,请重新登录！！！');if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");
            }
 
            if (!IsPostBack)
            {
                labAdmin.Text = Session["UserName"].ToString();
                labDepart.Text = Session["UserDept"].ToString();
                if (Session["id"] != null)
                {
                    gdid.Text = Session["id"].ToString();  
                }
                //CheckUser(ControlFinder);
            }

        }

        protected void gdid_TextChanged(object sender, EventArgs e)
        {
            Session["id"] = gdid.Text.Trim();

            if (Session["id"] != null)
            {
                gdid.Text = Session["id"].ToString();
            }
        }
      
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string sqlText = "update tbbd_session set sn_endtm='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' where sn_id='"+Session.SessionID+"'";
            Application.Lock();
            int num = (int)Application["online"];
            if (num > 0)
            {
                Application["online"] = (int)Application["online"] - 1;
            }
            DBCallCommon.ExeSqlText(sqlText);
            Session.Clear();
            //Session.Abandon();
            Application.UnLock();

            //string ip = Common.GetIPAddress();
            //string userHostName = Common.GetUserHostName();
            //sqlText = "insert into tbbd_session (sn_id,sn_crttm,sn_ip,sn_userhostname,sn_type,sn_num) values('" + Session.SessionID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ip + "','" + userHostName + "','安全退出','" + (int)Application["online"] + "')";
            //DBCallCommon.ExeSqlText(sqlText);
            //Response.Write("<script>if(window.parent!=null)window.parent.close();else{window.close();} </script>");
            
            Response.Write("<script>if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");
            
        }
    }
}
