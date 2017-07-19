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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_CPFYJSDOTHER : System.Web.UI.Page
    {
        string action = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                action = Request.QueryString["action"].ToString().Trim();
                if (action == "add")
                {
                    BindData();
                    CreateNewRow(12);
                }
            }
        }

        private void BindData()
        {
            string sql = "select  max(convert(int, substring(JS_BH,3,8)))+1 as maxJS_BH from PM_CPFYJSD where JS_BH like '%Other%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int max = 0;
            if (dt.Rows[0]["maxJS_BH"].ToString() == "")
            {
                max = 1;
            }
            else
            {
                max = Convert.ToInt32(dt.Rows[0]["maxJS_BH"].ToString());
            }
            lbJS_BH.Text = "JS" + max.ToString().PadLeft(8, '0')+"Other";//其他运费结算单号

            //如果当月运费已核算，那么之后生成的结算单自动转为下个月
            string sqlyfhs = "select * from TBFM_YFHS where YFHS_STATE='1' and YFHS_YEAR+'-'+YFHS_MONTH like '" + DateTime.Now.ToString("yyyy-MM") + "%'";
            System.Data.DataTable dtyfhs = DBCallCommon.GetDTUsingSqlText(sqlyfhs);
            lbJS_RQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (dtyfhs.Rows.Count > 0)
            {
                if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
                {
                    lbJS_RQ.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
                }
                else if (Convert.ToInt32(DateTime.Now.Month.ToString()) == 12)
                {
                    lbJS_RQ.Text = DateTime.Now.AddYears(1).AddMonths(-11).ToString("yyyy-MM") + "-01";
                }
            }
            lbJS_ZDR.Text = Session["UserName"].ToString();
            lbzdrid.Text = Session["UserID"].ToString();
        }

        protected void TextBoxCompany_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_yssname = (TextBox)sender;
            if (num > 0)
            {
                string yssname = (sender as TextBox).Text.Trim().Substring(0, num);
                string sqltext = "select * from TBCS_CUSUPINFO where CS_NAME='" + yssname + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    tbJS_GYS.Text = dt.Rows[0]["CS_NAME"].ToString().Trim();
                }
            }
        }


        protected void JS_SHDW_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_yssname = (TextBox)sender;
            RepeaterItem Reitem = (RepeaterItem)Tb_yssname.Parent;
            if (num > 0)
            {
                string yssname = (sender as TextBox).Text.Trim().Substring(0, num);
                string sqltext = "select * from TBCS_CUSUPINFO where CS_NAME='" + yssname + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    ((TextBox)Reitem.FindControl("JS_SHDW")).Text = dt.Rows[0]["CS_NAME"].ToString().Trim();
                }
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (tbJS_GYS.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写供应商信息！');", true);
                return;
            }
            List<string> list_sql = new List<string>();
            list_sql.Clear();
            string sqltext = "";
            action = Request.QueryString["action"].ToString().Trim();
            int num = 0;
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                if (((TextBox)rptProNumCost.Items[i].FindControl("JS_RWH")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("JS_SHUIL")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("JS_HSJE")).Text.Trim() != "")
                {
                    num++;
                }
            }
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写要添加的数据！');", true);
                return;
            }

            if (action == "add")
            {
                for (int i = 0; i < rptProNumCost.Items.Count; i++)
                {
                    if (((TextBox)rptProNumCost.Items[i].FindControl("JS_RWH")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("JS_SHUIL")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("JS_HSJE")).Text.Trim() != "")
                    {
                        string jhgzh=((TextBox)rptProNumCost.Items[i].FindControl("JS_RWH")).Text.Trim()+"_"+lbJS_BH.Text.Trim()+"_"+lbzdrid.Text.Trim()+"_"+((Label)rptProNumCost.Items[i].FindControl("XUHAO")).Text.Trim();
                        sqltext = "insert into PM_CPFYJSD(JS_FATHERID,JS_BH,JS_RQ,JS_ZDR,JS_GYS,JS_BZ,JS_JHGZH,JS_RWH,JS_JHQ,JS_SHDW,JS_BJSL,JS_SHUIL,JS_HSJE) values('" + lbJS_BH.Text.Trim() + "','" + lbJS_BH.Text.Trim() + "','" + lbJS_RQ.Text.Trim() + "','" + lbJS_ZDR.Text.Trim() + "','" + tbJS_GYS.Text.Trim() + "','" + txtJS_BZ.Text.Trim() + "','" + jhgzh + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("JS_RWH")).Text.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("JS_JHQ")).Text.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("JS_SHDW")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JS_BJSL")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JS_SHUIL")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JS_HSJE")).Text.Trim()) + ")";
                        list_sql.Add(sqltext);
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
            }
            Response.Redirect("PM_CPFYJSD.aspx?action=read&SHEETNO=" + lbJS_BH.Text.Trim());
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
            }
            else
            {
                Response.Write("<script>alert('请输入数字！')</script>");
            }
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["JS_SHUIL"].ToString().Trim() == "")
                {
                    dt.Rows[j]["JS_SHUIL"] = "11";
                }
            }
            List<string> col = new List<string>();
            this.rptProNumCost.DataSource = dt;
            this.rptProNumCost.DataBind();
            InitVar(col);
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("JS_RWH");
            dt.Columns.Add("JS_JHQ");
            dt.Columns.Add("JS_SHDW");
            dt.Columns.Add("JS_BJSL");
            dt.Columns.Add("JS_SHUIL");
            dt.Columns.Add("JS_HSJE");
            foreach (RepeaterItem retItem in rptProNumCost.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("JS_RWH")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("JS_JHQ")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("JS_SHDW")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("JS_BJSL")).Text;
                if (((TextBox)retItem.FindControl("JS_SHUIL")).Text.Trim() == "")
                {
                    ((TextBox)retItem.FindControl("JS_SHUIL")).Text = "11";
                }
                newRow[4] = ((TextBox)retItem.FindControl("JS_SHUIL")).Text;
                newRow[5] = ((TextBox)retItem.FindControl("JS_HSJE")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar(List<string> col)
        {
            if (rptProNumCost.Items.Count == 0)
            {
                NoDataPane.Visible = true;
            }
            else
            {
                NoDataPane.Visible = false;
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("JS_RWH");
            dt.Columns.Add("JS_JHQ");
            dt.Columns.Add("JS_SHDW");
            dt.Columns.Add("JS_BJSL");
            dt.Columns.Add("JS_SHUIL");
            dt.Columns.Add("JS_HSJE");
            foreach (RepeaterItem retItem in rptProNumCost.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("JS_RWH")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("JS_JHQ")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("JS_SHDW")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("JS_BJSL")).Text;
                    if (((TextBox)retItem.FindControl("JS_SHUIL")).Text.Trim() == "")
                    {
                        ((TextBox)retItem.FindControl("JS_SHUIL")).Text = "11";
                    }
                    newRow[4] = ((TextBox)retItem.FindControl("JS_SHUIL")).Text;
                    newRow[5] = ((TextBox)retItem.FindControl("JS_HSJE")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rptProNumCost.DataSource = dt;
            this.rptProNumCost.DataBind();
            InitVar(col);
        }


    }
}
