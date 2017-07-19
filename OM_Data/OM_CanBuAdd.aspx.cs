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
using Microsoft.Office.Interop.MSProject;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CanBuAdd : System.Web.UI.Page
    {
        string action = string.Empty;
        string id = string.Empty;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                if (action == "add")
                {
                    this.BindYearMoth(ddlYear, ddlMoth);
                }

                if (action == "Alter")
                {
                    
                    this.BindYearMoth(ddlYear, ddlMoth);
                    this.GetDataByID(id);
                }
            }
            
        }

        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
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

        private void GetDataByID(string id)
        {
            string sqltext = "select *,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as CB_MonthCB,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as CB_HeJi from (select * from OM_CanBu left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where CB_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string Yearmonth = dt.Rows[0]["CB_YearMonth"].ToString().Trim();
            string Year = Yearmonth.Substring(0, 4);
            string Month = Yearmonth.Substring(5, 2);

            txtST_WORKNO.Text = dt.Rows[0]["ST_WORKNO"].ToString().Trim();
            txtST_NAME.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
            txtDEP_NAME.Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();

            lbstid.Text = dt.Rows[0]["CB_STID"].ToString().Trim();

            txtKQ_CHUQIN.Text = dt.Rows[0]["KQ_CHUQIN"].ToString().Trim();
            txtKQ_CBTS.Text = dt.Rows[0]["KQ_CBTS"].ToString().Trim();
            txt_cbtzts.Text = dt.Rows[0]["CB_TZTS"].ToString().Trim();
            txtCB_BIAOZ.Text = dt.Rows[0]["CB_BIAOZ"].ToString().Trim();
            txtCB_MonthCB.Text = dt.Rows[0]["CB_MonthCB"].ToString().Trim();
            txtCB_BuShangYue.Text = dt.Rows[0]["CB_BuShangYue"].ToString().Trim();
            txtCB_HeJi.Text = dt.Rows[0]["CB_HeJi"].ToString().Trim();
            txtCB_BeiZhu.Text = dt.Rows[0]["CB_BeiZhu"].ToString().Trim();
            ddlYear.SelectedValue = Year;
            ddlMoth.SelectedValue = Month;
        
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex == 0 || ddlMoth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！');", true);
                return;
            }
            if (action=="add")
            {
                List<string> sqllist = new List<string>();
                sqllist.Clear();
                string sql = "";
                string sqlgetbh = "select * from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMoth.SelectedValue.ToString().Trim() + "' and state='0'";
                DataTable dtgetbh = DBCallCommon.GetDTUsingSqlText(sqlgetbh);
                if (dtgetbh.Rows.Count > 0)
                {
                    sql = "insert into OM_CanBu (CB_YearMonth,CB_STID,CB_TZTS,CB_BIAOZ,CB_BuShangYue,CB_BeiZhu,detailbh)";
                    sql += "values ('" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMoth.SelectedValue.ToString().Trim() + "','" + lbstid.Text.ToString().Trim() + "'," + CommonFun.ComTryDecimal(txt_cbtzts.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtCB_BIAOZ.Text.ToString().Trim()) + "," + CommonFun.ComTryDecimal(txtCB_BuShangYue.Text.ToString().Trim()) + ",'" + txtCB_BeiZhu.Text.ToString().Trim() + "','" + dtgetbh.Rows[0]["bh"].ToString().Trim() + "')";
                    sqllist.Add(sql);
                    DBCallCommon.ExecuteTrans(sqllist);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员添加成功！！！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该餐补数据还未生成或已提交审批！！！');", true);
                    return;
                }
            }

            if (action=="Alter")
            {
                List<string> sqllist = new List<string>();
                sqllist.Clear();
                txtST_WORKNO.Enabled = false;
                txtST_NAME.Enabled = false;
                txtDEP_NAME.Enabled = false;
                string sql = "";
                sql = "update OM_CanBu set CB_YearMonth='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMoth.SelectedValue.ToString().Trim() + "',CB_TZTS=" + CommonFun.ComTryDecimal(txt_cbtzts.Text.Trim()) + ",CB_BuShangYue='" + CommonFun.ComTryDecimal(txtCB_BuShangYue.Text.ToString().Trim()) + "',CB_BIAOZ='" + CommonFun.ComTryDecimal(txtCB_BIAOZ.Text.ToString().Trim()) + "',CB_BeiZhu='" + txtCB_BeiZhu.Text.ToString().Trim() + "' where CB_ID='" + id + "'";
                sqllist.Add(sql);
                DBCallCommon.ExecuteTrans(sqllist);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员已修改！！！');", true);
            }
           
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_CanBu.aspx");
        }

        //联想
        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    txtST_NAME.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lbstid.Text = dt.Rows[0]["ST_ID"].ToString().Trim();
                    txtDEP_NAME.Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                    txtST_WORKNO.Text = dt.Rows[0]["ST_WORKNO"].ToString().Trim();
                }
            }

        }
        //计算月餐补合计
        protected void txtCB_BuShangYue_TextChanged(object senser, EventArgs e)
        {
            if (CommonFun.ComTryDecimal(txtCB_BuShangYue.Text.Trim()) > 0)
            {
                txtCB_HeJi.Text = ((CommonFun.ComTryDecimal(txtCB_MonthCB.Text.Trim())) + (CommonFun.ComTryDecimal(txtCB_BuShangYue.Text.Trim()))).ToString().Trim();
            }
        }
    }
}
