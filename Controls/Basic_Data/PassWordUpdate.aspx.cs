using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ZCZJ_DPF;
using System.Collections.Generic;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class PassWordUpdate : System.Web.UI.Page
    {
        string username = string.Empty;
        string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            userid = Session["UserID"].ToString();
            lblupdate.Visible = false;
        }
        protected void btnCom_Click(object sender, EventArgs e)
        {

            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            if (txtNew.Text.ToString() != txtCofirm.Text.ToString())
            {
                lblupdate.Visible = true;
                lblupdate.Text = "新密码与确认的密码不一致!";
                return;
            }
            string oldPassword = "";
            string Sql0 = "select ST_PASSWORD from TBDS_STAFFINFO where ST_ID='" + Session["UserID"].ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(Sql0);
            if (dr.Read())
            {
                oldPassword = dr["ST_PASSWORD"].ToString();
            }
            dr.Close();
            string nowtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string password = ed.MD5Encrypt(txtNew.Text.ToString(), "!@#$%^&*");
            List<string> list = new List<string>();
            string sql = "update TBDS_STAFFINFO set ST_PASSWORD='" + password + "' where ST_ID='" + Session["UserID"].ToString() + "'";
            list.Add(sql);
            string sql1 = "insert into TBDS_EDITPASSWORDRECORD(stId,stName,editTime,oldPassword,newPassword) values ('" + userid + "','" + username + "' ,'" + nowtime.Trim() + "' ,'" + oldPassword.Trim() + "' ,'" + password.Trim() + "')";
            list.Add(sql1);
            try
            {
                DBCallCommon.ExecuteTrans(list);
                Response.Write("<script language='JavaScript'>alert('修改密码成功!');window.parent.location.href= '../Default.aspx';</script>");
            }
            catch
            {
                Response.Write("<script language='JavaScript'>alert('密码修改失败!')</script>");
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {


        }
    }

}
