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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KAOQINADD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(ddlYear, ddlMonth);
            }
        }

        /// <summary>
        /// 绑定年月
        /// </summary>
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            list_sql.Clear();

            string sql = "select ST_NAME,ST_WORKNO from TBDS_STAFFINFO where ST_NAME is not null";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count != 0)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ST_NAME"].ToString() == txtName.Text.Trim())
                    {
                        i++;
                    }
                }
                if (i == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您输入的人员不存在！！！');", true);
                    return;
                }
                if (i == 1)
                {
                    //string sqltext = "select * from View_OM_STAFF where ST_NAME is not null";
                    //DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["ST_NAME"].ToString() == txtName.Text.Trim())
                        {
                            txtGongHao.Text = dr["ST_WORKNO"].ToString();
                        }
                    }

                }
                if (i > 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您输入的人员有多个，请手动输入相匹配的工号！！！');", true);
                }
            }
            if (ddlYear.SelectedValue == "-请选择-" || ddlMonth.SelectedValue == "-请选择-")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择时间！！！');", true);
                return;
            }

            list_sql.Add("insert into OM_KQTJ(KQ_PersonID,KQ_DATE) Values('" + txtGongHao.Text + "','" + ddlYear.SelectedValue +"-"+ddlMonth.SelectedValue + "')");
            DBCallCommon.ExecuteTrans(list_sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员已添加！！！');window.close();", true);

        }
    }
}
