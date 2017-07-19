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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class OnlineMan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //getSession();
            if (Session["UserName"] == "" || Session["UserName"] == null)
            {
                Response.Write("<script>if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");

            }
            else
            {
                Label1.Text = Session["UserName"].ToString();
            }            
        }

        protected void getSession()
        {
            for (int i = 0; i < Session.Count; i++)
            {
                string x = Session.Contents[i].ToString();               
            }
            //Label1.Text = Session.Count.ToString();
            //string aa = ed.MD5Decrypt(password, "!@#$%^&*");
        }

        //密码还原
        protected void btnCom_Click(object sender, EventArgs e)
        {
            string sqltext = "SELECT ST_PASSWORD FROM TBDS_STAFFINFO WHERE ST_CODE ='" + Session["UserID"].ToString() + "'";
            SqlDataReader dr =  DBCallCommon.GetDRUsingSqlText(sqltext);
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            if (dr.Read())
            {
                txtPassword.Text = ed.MD5Decrypt(dr["ST_PASSWORD"].ToString(), "!@#$%^&*");
            }
            dr.Close();
          
        }

        //加密
        protected void Button1_Click(object sender, EventArgs e)
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            if (TextBox1.Text.Trim() != "")
            {
                TextBox2.Text = ed.MD5Encrypt(TextBox1.Text, "!@#$%^&*");
            }
            
            //if (dr.Read())
            //{
            //    txtPassword.Text = ed.MD5Decrypt(dr["ST_PASSWORD"].ToString(), "!@#$%^&*");
            //}
            //dr.Close();

        }

        //解密
        protected void Button2_Click(object sender, EventArgs e)
        {
            //string sqltext = "SELECT ST_PASSWORD FROM TBDS_STAFFINFOR WHERE ST_CODE ='" + Session["ST_CODE"].ToString() + "'";
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            if (TextBox1.Text.Trim() != "")
            {
                TextBox2.Text = ed.MD5Decrypt(TextBox1.Text, "!@#$%^&*");
                
            }

        }   

    }
}
