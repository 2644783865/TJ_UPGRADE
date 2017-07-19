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

namespace ZCZJ_DPF.ESM
{
    public partial class EQU_tzsbop : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string actionstr = Request.QueryString["action"].ToString();
                if (actionstr == "update")
                {
                    string Id = Request.QueryString["Id"].ToString();
                    sql = "select * from EQU_tzsb where Id='"+Id+"'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        Code.Text = dr["Code"].ToString();
                        Name.Text = dr["Name"].ToString();
                        Type.Text = dr["Type"].ToString();
                        Specification.Text = dr["Specification"].ToString();
                        Ocode.Text = dr["Ocode"].ToString();
                        Rcode.Text = dr["Rcode"].ToString();
                        Ucode.Text = dr["Ucode"].ToString();
                        Manufa.Text = dr["Manufa"].ToString();
                        Position.Text = dr["Position"].ToString();
                        Ustate.Text = dr["Ustate"].ToString();
                        Redate.Text = dr["Redate"].ToString();
                        Remark.Text = dr["Remark"].ToString();
                    }
                    dr.Close();
                }
                else if (actionstr == "add")
                {

                }
            }

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string actionstr = Request.QueryString["action"].ToString();
            if (actionstr == "add")
            {
                string sql = "";
                sql = "insert into EQU_tzsb(Code,Name,Type,Specification,Ocode,Rcode,Ucode,Manufa,Position,Ustate,Redate,Remark) values('" + Code.Text.Trim() + "','" + Name.Text.Trim() + "','" + Type.Text.Trim() + "','" + Specification.Text.Trim() + "','" + Ocode.Text.Trim() + "','" + Rcode.Text.Trim() + "','" + Ucode.Text.Trim() + "','" + Manufa.Text.Trim() + "','" + Position.Text.Trim() + "','" + Ustate.Text.Trim() + "','" + Redate.Text.Trim() + "','" + Remark.Text.Trim() + "')";
                DBCallCommon.ExeSqlText(sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "if(confirm('操作成功，返回主界面？')){window.close();window.opener.location.reload();}", true);
            }
            else if (actionstr == "update")
            {
                string Id = Request.QueryString["Id"].ToString();
                string sqltext = "";
                sqltext = "update EQU_tzsb set Code='" + Code.Text.Trim() + "',Name='" + Name.Text.Trim() + "',Type='" + Type.Text.Trim() + "',Specification='" + Specification.Text.Trim() + "',Ocode='" + Ocode.Text.Trim() + "',Rcode='" + Rcode.Text.Trim() + "',Ucode='" + Ucode.Text.Trim() + "',Manufa='" + Manufa.Text.Trim() + "',Position='" + Position.Text.Trim() + "',Ustate='" + Ustate.Text.Trim() + "',Redate='" + Redate.Text.Trim() + "',Remark='" + Remark.Text.Trim() + "'where  Id = '" + Id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "if(confirm('操作成功，返回主界面？')){window.close();window.opener.location.reload();}", true);
            }
        }
    }
}
