using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Position_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] == "ADD")
            {
                Confirm.Text = "添加";
            }
            else
            {
                Confirm.Text = "修改";
            }
            if (!IsPostBack)
            {
                GetParent();
                if (Request.QueryString["FLAG"].ToString() == "MODI")
                {
                    string id = Request.QueryString["ID"];
                    string sql = "SELECT * FROM TBWS_LOCATION WHERE WL_ID='" + id + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxName.Text = dr["WL_NAME"].ToString();
                        DropDownListParent.Items.FindByValue(dr["WL_WSID"].ToString()).Selected = true;
                        DropDownListParent.Enabled = false;
                        TextBoxNote.Text = dr["WL_NOTE"].ToString();
                    }
                    dr.Close();
                }
            }
        }

        private void GetParent()
        {
            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID<>'ROOT' ORDER BY WS_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListParent.DataSource = dt;
            DropDownListParent.DataTextField = "WS_NAME";
            DropDownListParent.DataValueField = "WS_ID";
            DropDownListParent.DataBind();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Confirm.Text == "添加")
            {
                Response.Redirect("SM_Position_Manage.aspx");
            }
            else if (Confirm.Text == "修改")
            {
                Response.Redirect("SM_Position_Manage.aspx?ID=" + Request.QueryString["ID"].ToString());
            }
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Confirm.Text == "修改")
            {
                try
                {
                    string name = TextBoxName.Text;
                    DateTime date = DateTime.Now;
                    string clerk = Session["UserID"].ToString();
                    string note = TextBoxNote.Text;
                    string id = Request.QueryString["ID"];
                    string sql = "";
                    sql = "UPDATE TBWS_LOCATION SET WL_NAME='" + name + "',WL_FILLDATE='" +
                        date + "',WL_CLERK='" + clerk +"',WL_NOTE='" + note + "' WHERE WL_ID='" + id + "'";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Redirect("SM_Position_Manage.aspx?ID=" + Request.QueryString["ID"].ToString());
                }
                catch
                {
                    LabelMessage.Text = "数据更新失败！";
                }
            }
            if (Confirm.Text == "添加")
            {
                try
                {
                    string sql = "";
                    string name = TextBoxName.Text;
                    sql = "SELECT * FROM TBWS_LOCATION WHERE WL_NAME='" + name + "' AND WL_WSID='" + DropDownListParent.SelectedValue + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        dr.Close();
                        LabelMessage.Text = "数据库中已经存在该仓位！";
                        return;
                    }
                    dr.Close();
                    string id = generateID();
                    string pid = DropDownListParent.SelectedValue;
                    DateTime date = DateTime.Now;
                    string clerk = Session["UserID"].ToString();
                    string note = TextBoxNote.Text;
                    sql =
                        "INSERT INTO TBWS_LOCATION(WL_ID,WL_NAME,WL_WSID,WL_FILLDATE,WL_CLERK,WL_NOTE) VALUES('" +
                        id + "','" + name + "','" + pid + "','" + date + "','" + clerk + "','" + note + "')";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Redirect("SM_Position_Manage.aspx");
                }
                catch
                {
                    LabelMessage.Text = "添加记录失败！";
                }
            }
        }

        protected string generateID()
        {
            string id = "";
            string wsid = DropDownListParent.SelectedValue;
            string sql = "SELECT WL_ID as wlid FROM TBWS_LOCATION WHERE WL_ID LIKE '" + wsid + "%'";
            DataTable  tb = DBCallCommon.GetDTUsingSqlText(sql);
            if (tb.Rows.Count != 0)
            {
                for (int i = 1; i <= 999; i++)
                {
                    if (i < 10)
                    {
                        id = wsid + ".00" + i.ToString();
                    }
                    if((i >= 10)&&(i<100))
                    {
                        id = wsid + ".0" + i.ToString();
                    }
                    if (i >= 100)
                    {
                        id = wsid +"."+ i.ToString();
                    }
                    if (tb.Select("wlid='" + id + "'").Length == 0)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                id = wsid + ".001";
            }
            return id;
        }
   
    }
}
