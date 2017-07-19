using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_JXGZYESPADD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddepartment();
                contrlkjx();//控件可见性和可用性
                this.BindYearMoth(ddlYear, ddlMonth);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string bh = DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + Session["UserID"].ToString().Trim();
            string tb_yearmonth = ddlYear.Text.ToString().Trim() + "-" + ddlMonth.Text.ToString().Trim();
            if (tb_yearmonth.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择年月！');", true);
                return;
            }
            if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                return;
            }
            string sqltext = "insert into OM_JXGZYESP(bh,things,yearmonth,creattime,creatstid,creatstname,jxadddepartment,shrid1,shrname1,shrid2,shrname2,MonthYuE) values('" + bh + "','" + txt_contents.Text.Trim() + "','" + tb_yearmonth + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + drpdepartment.SelectedItem.Text.Trim() + "','" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_second.Text.Trim() + "','" + txtJXYE.Text.Trim() + "')";
            list.Add(sqltext);
            DBCallCommon.ExecuteTrans(list);




            Response.Redirect("OM_JXGZYESPdetail.aspx?spid=" + bh);
        }


        protected void binddepartment()
        {
            string sql = "SELECT DISTINCT DEP_NAME,DEP_CODE  FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            drpdepartment.DataValueField = "DEP_CODE";
            drpdepartment.DataTextField = "DEP_NAME";
            drpdepartment.DataSource = dt;
            drpdepartment.DataBind();

            ListItem item = new ListItem("--请选择--", "0");
            drpdepartment.Items.Insert(0, item);

        }


        private void contrlkjx()
        {
            txt_first.Enabled = true;
            hlSelect1.Visible = true;
            txt_second.Enabled = true;
            hlSelect2.Visible = true;
        }
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            strWhere();

        }
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            strWhere();

        }
        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));

            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                ddl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            ddl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));

        }

        private string strWhere()
        {

            string strWhere = "select * from TBDS_KaoHe_JXList  where  ";
            if (ddlYear.Text.Trim() != "" && ddlMonth.Text.Trim() != "" && drpdepartment.Text.Trim() != "")
            {
                strWhere += "  Year='" + ddlYear.Text.Trim() + "'";
                strWhere += " and Month='" + ddlMonth.Text.Trim() + "'";
                strWhere += " and DepId='" + drpdepartment.SelectedValue + "'";
            }
            else
            {
                strWhere += " 1=1 ";
            }
            //if (ddlMonth.Text.Trim() != "")
            //{
            //    strWhere += " and Month='" + ddlMonth.Text.Trim() + "'";
            //}
            //if (drpdepartment.Text.Trim() != "")
            //{
            //    strWhere += " and DepId='" + drpdepartment.SelectedValue + "'";
            //}
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strWhere);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtJXYE.Text = dr["YuE"].ToString();
            }
            return strWhere;
        }
        protected void drpdepartment_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            strWhere();
        }
    }
}
