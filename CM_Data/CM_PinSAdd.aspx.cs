using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_PinSAdd1 : System.Web.UI.Page
    {
        string type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            type = Request.QueryString["Action"];
            if (!IsPostBack)
            {
                GetDepartment();
            }
        }

        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddl_dep.DataSource = dt;
            ddl_dep.DataTextField = "DEP_NAME";
            ddl_dep.DataValueField = "DEP_CODE";
            ddl_dep.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddl_dep.Items.Insert(0, item);
            ddl_dep.SelectedValue = "00";
        }

        protected void btn_Continue_Click(object sender, EventArgs e)
        {
            if (hdfPSRID.Value != "")
            {
                string sqlcheck = string.Format("select * from TBCM_HT_SETTING where per_type='{0}' and dep_id='{1}' and per_id='{2}' and per_sfjy=0", type, ddl_dep.SelectedValue, hdfPSRID.Value);
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlcheck);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('重复添加！');", true); return;
                }
                else
                {
                    string sqltext = string.Format("insert into TBCM_HT_SETTING(dep_id,per_id,per_type) values('{0}','{1}','{2}')", ddl_dep.SelectedValue, hdfPSRID.Value, type);
                    DBCallCommon.ExeSqlText(sqltext);
                    if (((Button)sender).ID == "btn_Continue")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('添加成功');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('添加成功');window.opener=null;window.open('','_self');window.close();", true);
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('添加失败，请检查添加项！');</script>");
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterStartupScript(GetType(), "", "window.close();", true);
        }
    }
}
