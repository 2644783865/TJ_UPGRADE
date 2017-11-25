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
using System.Data.SqlClient;
using ZCZJ_DPF;

namespace ZCZJ_DPF
{
    public partial class _Default : System.Web.UI.Page
    {
        string power = "1";
        string sq = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                    //sqlText = "update tbbd_session set sn_endtm='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sn_id='" + Session.SessionID + "'";
                    if (Request.Path.Contains("Default.aspx"))
                    {
                        Application.Lock();
                        int num = (int)Application["online"];
                        //if (num > 0)
                        {
                            Application["online"] = (int)Application["online"] + 1;
                        }
                        Application.UnLock();
                    }
                    
                    ////string ip = Common.GetIPAddress();
                    ////string userHostName = Common.GetUserHostName();
                    //string sqlText = "insert into tbbd_session (sn_id,sn_crttm,sn_ip,sn_userhostname,sn_type,sn_num) values('" + Session.SessionID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ip + "','" + userHostName + "','default_pageload','" + (int)Application["online"] + "')";
                    //DBCallCommon.ExeSqlText(sqlText);
                    //UserName.Focus();
            }           
            laberror.Visible = false;
            LoginButton.Click += new ImageClickEventHandler(LoginButton_Click);
            ResetButton.Click +=new ImageClickEventHandler(ResetButton_Click);
        }

        protected void LoginButton_Click(object sender, ImageClickEventArgs e)
        {
           
                if (CheckUser())
                {
                    if (sq == "1")
                    {
                      //  Response.Redirect("MT_Data/Mt_Index.aspx?sq=1");
                        Response.Write("<script>window.location='MT_Data/MT_Index.aspx'</script>");
                    }
                    else
                    {
                       // Response.Redirect("MT_Data/Mt_Index.aspx");
                        Response.Write("<script>window.location='MT_Data/MT_Index.aspx'</script>");
                        //Response.Write("<script>window.location='PM_Data/PM_GongShi_Detail_List.aspx'</script>");
                    }
                }
                else
                {
                    laberror.Visible = true;
                    if (power == "1")
                    {
                        laberror.Text = "您的用户名或者密码错误！";
                    }
                    else
                    {
                        laberror.Text = "您无权限登录系统，若需登录系统，请及时与管理员联系！";
                    }
                }
          


        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            UserName.Text = "";
            PassWord.Text = "";
        }

        protected bool CheckUser()
        {
            bool bUser = false;
            string strText = "";
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string password = ed.MD5Encrypt(PassWord.Text.Trim(), "!@#$%^&*");

            strText = "select a.ST_ID,a.ST_NAME,a.ST_PASSWORD,a.ST_DEPID,b.DEP_NAME,a.R_NAME,a.ST_POSITION ,st_allow_in  ";
            strText += "from TBDS_STAFFINFO as a,TBDS_DEPINFO as b where a.ST_DEPID=b.DEP_CODE ";
            strText += "and (ST_CODE='" + UserName.Text + "' or ST_NAME = '" + UserName.Text + "') ";
            strText += "and ST_PASSWORD='" + password + "' ";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(strText);

            if (dr.Read())
            {
                if (dr["ST_ALLOW_IN"].ToString() != "1")
                {
                    bUser = false;
                    power = "2";
                }
                else
                {
                    if (UserName.Text.Trim().Equals(dr["ST_NAME"].ToString()) || UserName.Text.Trim().Equals(dr["ST_CODE"].ToString()))
                    {
                        if (password.Equals(dr["ST_PASSWORD"].ToString()))
                        {
                            Session["UserName"] = dr["ST_NAME"].ToString();
                            Session["UserGroup"] = dr["R_NAME"].ToString();
                            Session["UserID"] = dr["ST_ID"].ToString();
                            Session["UserDeptID"] = dr["ST_DEPID"].ToString();
                            Session["UserDept"] = dr["DEP_NAME"].ToString();
                            Session["POSITION"] = dr["ST_POSITION"].ToString();
                            //Session["UserNameCode"] = dr["ST_NAMECODE"].ToString();
                            bUser = true;
                        }


                    }
                }
                
            }
            dr.Close();

            return bUser;
        }

        //protected void LoginButton_Click1(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect("~/CM_Data/CM_Index.aspx");         
        //}
       
    }
}
