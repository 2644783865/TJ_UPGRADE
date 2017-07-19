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
using ZCZJ_DPF;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.FM_Data
{
    public partial class CB_FYFT_ZZFY : System.Web.UI.Page
    {
        double zzfyhj = 0;
        double fplhj = 0;
        double gdzzfyhj = 0;
        double kbzzfyhj = 0;
        string year = "";
        string month = "";
        string yearint = "";
        string monthint = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Data.DataTable dt = dtcreate();
                if (dt.Rows.Count > 0)
                {
                    palNoData.Visible = false;
                }
                else
                {
                    palNoData.Visible = true;
                }
                rptProNumCost.DataSource = dt;
                rptProNumCost.DataBind();
            }
        }

        private System.Data.DataTable dtcreate()
        {
            year =  Request.QueryString["year"];
            yearint = (Convert.ToInt32(year)).ToString();
            month = Request.QueryString["month"];
            monthint = (Convert.ToInt32(month)).ToString();
            lbdate.Text = "" + year + "年" + month + "月";
            string sql = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            DataColumn dc1 = new DataColumn("CB_ZZ_TSAID");
            DataColumn dc2 = new DataColumn("CB_ZZFY");
            DataColumn dc3 = new DataColumn("CB_ZZ_FPL");
            DataColumn dc4 = new DataColumn("CB_ZZ_GDZZFY");
            DataColumn dc5 = new DataColumn("CB_ZZ_KBZZFY");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            sql = "select GS_TSAID as CB_ZZ_TSAID,sum(CB_GSFY) as CB_ZZFY,'0' as CB_ZZ_FPL,'0' as CB_ZZ_GDZZFY,'0' as CB_ZZ_KBZZFY from(select GS_TSAID,GS_MONEY as CB_GSFY,DATEYEAR,DATEMONTH from TBMP_GS_LIST where GS_MONEY is not null union all select CNFB_TSAID as GS_TSAID,CNFB_BYREALMONEY as CB_GSFY,CNFB_YEAR as DATEYEAR,CNFB_MONTH as DATEMONTH from TBMP_CNFB_LIST where CNFB_BYREALMONEY is not null)a where (GS_TSAID like '%1%' or GS_TSAID like '%2%' or GS_TSAID like '%3%' or GS_TSAID like '%4%' or GS_TSAID like '%5%' or GS_TSAID like '%6%' or GS_TSAID like '%7%' or GS_TSAID like '%8%' or GS_TSAID like '%9%' or GS_TSAID like '%0%') and ((DATEYEAR='" + yearint + "' and DATEMONTH='" + monthint + "') or (DATEYEAR='" + year + "' and DATEMONTH='" + month + "')) group by GS_TSAID";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            return dt;
        }




        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.TextBox tbid = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbid");
                System.Web.UI.WebControls.TextBox tbzzfy = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbzzfy");
                System.Web.UI.WebControls.TextBox tbfpl = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbfpl");
                System.Web.UI.WebControls.TextBox tbgdzzfy = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbgdzzfy");
                System.Web.UI.WebControls.TextBox tbkbzzfy = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbkbzzfy");
                if (tbfpl.Text == "")
                {
                    tbfpl.Text = "0";
                }
                fplhj += Convert.ToDouble(tbfpl.Text);
                if (tbgdzzfy.Text == "")
                {
                    tbgdzzfy.Text = "0";
                }
                gdzzfyhj += Convert.ToDouble(tbgdzzfy.Text);
                if (tbkbzzfy.Text == "")
                {
                    tbkbzzfy.Text = "0";
                }
                kbzzfyhj += Convert.ToDouble(tbkbzzfy.Text);
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                System.Web.UI.WebControls.Label lbzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzzfyhj");
                System.Web.UI.WebControls.Label lbfplhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfplhj");
                System.Web.UI.WebControls.Label lbgdzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbgdzzfyhj");
                System.Web.UI.WebControls.Label lbkbzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbkbzzfyhj");

                lbzzfyhj.Text = zzfyhj.ToString();
                lbfplhj.Text = Math.Round(fplhj,2).ToString();
                lbgdzzfyhj.Text = Math.Round(gdzzfyhj,2).ToString();
                lbkbzzfyhj.Text = Math.Round(kbzzfyhj,2).ToString();
            }
        }

        //根据工时计算，将费用分摊到每个任务号
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.TextBox tbzzfy = (System.Web.UI.WebControls.TextBox)item.FindControl("tbzzfy");
                zzfyhj += Convert.ToDouble(tbzzfy.Text);
            }
            System.Data.DataTable dt = dtcreate();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double fpl = Math.Round((Convert.ToDouble(dt.Rows[i]["CB_ZZFY"]) / zzfyhj), 10);
                dt.Rows[i]["CB_ZZ_FPL"] = fpl;
                dt.Rows[i]["CB_ZZ_GDZZFY"] = Math.Round((Convert.ToDouble(txtgdzzfy.Text) * fpl), 6);
                dt.Rows[i]["CB_ZZ_KBZZFY"] = Math.Round((Convert.ToDouble(txtkbzzfy.Text) * fpl), 6);
            }
            rptProNumCost.DataSource = dt;
            rptProNumCost.DataBind();

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

            List<string> list_sqlsc = new List<string>();
            list_sqlsc.Clear();
            List<string> list_sqladd = new List<string>();
            list_sqladd.Clear();
            year = Request.QueryString["year"];
            month = Request.QueryString["month"];
            string sqltextsc = "";
            string sqltextadd = "";
            string sqltext = "select * from CB_FT_ZZFY where CB_ZZ_YEAR='" + year + "' and CB_ZZ_MONTH='" + month + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                sqltextsc = "delete from CB_FT_ZZFY where CB_ZZ_YEAR='" + year + "' and CB_ZZ_MONTH='" + month + "'";
                list_sqlsc.Add(sqltextsc);
                DBCallCommon.ExecuteTrans(list_sqlsc);
            }
            foreach (RepeaterItem item in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.TextBox tbid = (System.Web.UI.WebControls.TextBox)item.FindControl("tbid");
                System.Web.UI.WebControls.TextBox tbzzfy = (System.Web.UI.WebControls.TextBox)item.FindControl("tbzzfy");
                System.Web.UI.WebControls.TextBox tbfpl = (System.Web.UI.WebControls.TextBox)item.FindControl("tbfpl");
                System.Web.UI.WebControls.TextBox tbgdzzfy = (System.Web.UI.WebControls.TextBox)item.FindControl("tbgdzzfy");
                System.Web.UI.WebControls.TextBox tbkbzzfy = (System.Web.UI.WebControls.TextBox)item.FindControl("tbkbzzfy");
                sqltextadd = "insert into CB_FT_ZZFY(CB_ZZ_TSAID,CB_ZZFY,CB_ZZ_FPL,CB_ZZ_GDZZFY,CB_ZZ_KBZZFY,CB_ZZ_YEAR,CB_ZZ_MONTH) Values('" + tbid.Text.Trim() + "','" + Convert.ToDouble(tbzzfy.Text.Trim()) + "','" + Convert.ToDouble(tbfpl.Text.Trim()) + "','" + Convert.ToDouble(tbgdzzfy.Text.Trim()) + "','" + Convert.ToDouble(tbkbzzfy.Text.Trim()) + "','" + year + "','" + month + "')";
                list_sqladd.Add(sqltextadd);
            }
            DBCallCommon.ExecuteTrans(list_sqladd);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);

        }


        protected void btn_export_Click(object sender, EventArgs e)
        {
            year = Request.QueryString["year"].ToString().Trim();
            month = Request.QueryString["month"].ToString().Trim();
            string sqlzzfy = "select CB_ZZ_TSAID,cast(CB_ZZFY as decimal(18,2)) as CB_ZZFY,cast(CB_ZZ_FPL as decimal(18,4)) as CB_ZZ_FPL,cast(CB_ZZ_GDZZFY as decimal(18,2)) as CB_ZZ_GDZZFY,cast(CB_ZZ_KBZZFY as decimal(18,2)) as CB_ZZ_KBZZFY,CB_ZZ_YEAR,CB_ZZ_MONTH from CB_FT_ZZFY where CB_ZZ_YEAR='" + year + "' and CB_ZZ_MONTH='" + month + "'";
            System.Data.DataTable dtzzfy = DBCallCommon.GetDTUsingSqlText(sqlzzfy);
            string filename = "制造费用分摊导出"+DateTime.Now.ToString("yyyyMMdd HHmmss").Trim()+".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("制造费用分摊导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtzzfy.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dtzzfy.Columns.Count; j++)
                    {
                        string str = dtzzfy.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtzzfy.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
