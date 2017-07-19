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
using System.Collections.Generic;
using ZCZJ_DPF;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.FM_Data
{
    public partial class CB_FYFT : System.Web.UI.Page
    {

        double gsfyhj = 0.0001;
        double jjfplhj = 0;
        double cnjjhj = 0;
        double jgyzhj = 0.0001;
        double jgyzfplhj = 0;
        double jgyzfthj = 0;
        //double gzfplhj = 0;
        //double gzhj = 0;
        string year = "";
        string month = "";
        string yearint = "";
        string monthint = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
            year = Request.QueryString["year"];
            month = Request.QueryString["month"];
            yearint = (Convert.ToInt32(year)).ToString();
            monthint = (Convert.ToInt32(month)).ToString();
            lbdate.Text = "" + year + "年" + month + "月";
            string sql = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            DataColumn dc1 = new DataColumn("CB_TSAID");
            DataColumn dc2 = new DataColumn("CB_GSFY");
            DataColumn dc3 = new DataColumn("CB_JJFPL");
            DataColumn dc4 = new DataColumn("CB_CNJJ");
            DataColumn dc5 = new DataColumn("CB_JGYZ");
            DataColumn dc6 = new DataColumn("CB_JGYZFPL");
            DataColumn dc7 = new DataColumn("CB_JGYZFT");
            DataColumn dc8 = new DataColumn("CB_GZFPL");
            DataColumn dc9 = new DataColumn("CB_GZ");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
            //2017.4.10修改
            if ((yearint == "2016" && month == "12") || (yearint == "2017" && month == "01") || (yearint == "2017" && month == "02") || (yearint == "2017" && month == "03"))
            {
                sql = "select TSAID as CB_TSAID,isnull(GS_MONEY,0) as CB_GSFY,'0' as CB_JJFPL,'0' as CB_CNJJ,isnull(CNFB_BYREALMONEY,0) as CB_JGYZ,'0' as CB_JGYZFPL,'0' as CB_JGYZFT,(isnull(GS_MONEY,0)+isnull(CNFB_BYREALMONEY,0)) as CB_ZJRG,'0' as CB_GZFPL,'0' as CB_GZ from ((select GS_TSAID as TSAID from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH='" + monthint + "' and GS_MONEY is not null) union (select CNFB_TSAID as TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID!='' and CNFB_TSAID is not null and (CNFB_TYPE like '%N%' or CNFB_TYPE like '%喷漆喷砂%')))a left join (select GS_TSAID,sum(isnull(GS_MONEY,0)) as GS_MONEY from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH ='" + monthint + "' and GS_MONEY is not null group by GS_TSAID)b on a.TSAID=b.GS_TSAID left join (select CNFB_TSAID,sum(isnull(CNFB_BYREALMONEY,0)) as CNFB_BYREALMONEY from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH ='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID is not null and CNFB_TSAID!='' and (CNFB_TYPE like '%N%' or CNFB_TYPE like '%喷漆喷砂%')  group by CNFB_TSAID)c on a.TSAID=c.CNFB_TSAID";
            }
            else
            {
                sql = "select TSAID as CB_TSAID,isnull(GS_MONEY,0) as CB_GSFY,'0' as CB_JJFPL,'0' as CB_CNJJ,isnull(CNFB_BYREALMONEY,0) as CB_JGYZ,'0' as CB_JGYZFPL,'0' as CB_JGYZFT,(isnull(GS_MONEY,0)+isnull(CNFB_BYREALMONEY,0)) as CB_ZJRG,'0' as CB_GZFPL,'0' as CB_GZ from ((select GS_TSAID as TSAID from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH='" + monthint + "' and GS_MONEY is not null) union (select CNFB_TSAID as TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID!='' and CNFB_TSAID is not null and CNFB_TYPE like '%N%'))a left join (select GS_TSAID,sum(isnull(GS_MONEY,0)) as GS_MONEY from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH ='" + monthint + "' and GS_MONEY is not null group by GS_TSAID)b on a.TSAID=b.GS_TSAID left join (select CNFB_TSAID,sum(isnull(CNFB_BYREALMONEY,0)) as CNFB_BYREALMONEY from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH ='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID is not null and CNFB_TSAID!='' and CNFB_TYPE like '%N%' group by CNFB_TSAID)c on a.TSAID=c.CNFB_TSAID";
            }
            //2016.12.27修改，将喷漆喷砂包含在内
            //if ((yearint == "2016" && month == "12") || (string.Compare(yearint,"2016")>0))
            //{
            //    sql = "select TSAID as CB_TSAID,isnull(GS_MONEY,0) as CB_GSFY,'0' as CB_JJFPL,'0' as CB_CNJJ,isnull(CNFB_BYREALMONEY,0) as CB_JGYZ,'0' as CB_JGYZFPL,'0' as CB_JGYZFT,(isnull(GS_MONEY,0)+isnull(CNFB_BYREALMONEY,0)) as CB_ZJRG,'0' as CB_GZFPL,'0' as CB_GZ from ((select GS_TSAID as TSAID from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH='" + monthint + "' and GS_MONEY is not null) union (select CNFB_TSAID as TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID!='' and CNFB_TSAID is not null and (CNFB_TYPE like '%N%' or CNFB_TYPE like '%喷漆喷砂%')))a left join (select GS_TSAID,sum(isnull(GS_MONEY,0)) as GS_MONEY from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH ='" + monthint + "' and GS_MONEY is not null group by GS_TSAID)b on a.TSAID=b.GS_TSAID left join (select CNFB_TSAID,sum(isnull(CNFB_BYREALMONEY,0)) as CNFB_BYREALMONEY from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH ='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID is not null and CNFB_TSAID!='' and (CNFB_TYPE like '%N%' or CNFB_TYPE like '%喷漆喷砂%')  group by CNFB_TSAID)c on a.TSAID=c.CNFB_TSAID";
            //}
            //else
            //{
            //    sql = "select TSAID as CB_TSAID,isnull(GS_MONEY,0) as CB_GSFY,'0' as CB_JJFPL,'0' as CB_CNJJ,isnull(CNFB_BYREALMONEY,0) as CB_JGYZ,'0' as CB_JGYZFPL,'0' as CB_JGYZFT,(isnull(GS_MONEY,0)+isnull(CNFB_BYREALMONEY,0)) as CB_ZJRG,'0' as CB_GZFPL,'0' as CB_GZ from ((select GS_TSAID as TSAID from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH='" + monthint + "' and GS_MONEY is not null) union (select CNFB_TSAID as TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID!='' and CNFB_TSAID is not null and CNFB_TYPE like '%N%'))a left join (select GS_TSAID,sum(isnull(GS_MONEY,0)) as GS_MONEY from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH ='" + monthint + "' and GS_MONEY is not null group by GS_TSAID)b on a.TSAID=b.GS_TSAID left join (select CNFB_TSAID,sum(isnull(CNFB_BYREALMONEY,0)) as CNFB_BYREALMONEY from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH ='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID is not null and CNFB_TSAID!='' and CNFB_TYPE like '%N%' group by CNFB_TSAID)c on a.TSAID=c.CNFB_TSAID";
            //}
            //原始语句
            //sql = "select TSAID as CB_TSAID,isnull(GS_MONEY,0) as CB_GSFY,'0' as CB_JJFPL,'0' as CB_CNJJ,isnull(CNFB_BYREALMONEY,0) as CB_JGYZ,'0' as CB_JGYZFPL,'0' as CB_JGYZFT,(isnull(GS_MONEY,0)+isnull(CNFB_BYREALMONEY,0)) as CB_ZJRG,'0' as CB_GZFPL,'0' as CB_GZ from ((select GS_TSAID as TSAID from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH='" + monthint + "' and GS_MONEY is not null) union (select CNFB_TSAID as TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID!='' and CNFB_TSAID is not null and CNFB_TYPE like '%N%'))a left join (select GS_TSAID,sum(isnull(GS_MONEY,0)) as GS_MONEY from TBMP_GS_LIST where DATEYEAR='" + yearint + "' and DATEMONTH ='" + monthint + "' and GS_MONEY is not null group by GS_TSAID)b on a.TSAID=b.GS_TSAID left join (select CNFB_TSAID,sum(isnull(CNFB_BYREALMONEY,0)) as CNFB_BYREALMONEY from TBMP_CNFB_LIST where CNFB_YEAR='" + year + "' and CNFB_MONTH ='" + month + "' and CNFB_BYREALMONEY is not null and CNFB_TSAID is not null and CNFB_TSAID!='' and CNFB_TYPE like '%N%' group by CNFB_TSAID)c on a.TSAID=c.CNFB_TSAID";
            
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            return dt;
        }




        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.TextBox tbid = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbid");
                System.Web.UI.WebControls.TextBox tbgsfy = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbgsfy");
                System.Web.UI.WebControls.TextBox tbjjfpl = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbjjfpl");
                System.Web.UI.WebControls.TextBox tbcnjj = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbcnjj");
                System.Web.UI.WebControls.TextBox tbjgyz = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbjgyz");
                System.Web.UI.WebControls.TextBox tbjgyzfpl = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbjgyzfpl");
                System.Web.UI.WebControls.TextBox tbjgyzft = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("tbjgyzft");
                //TextBox tbgzfpl = (TextBox)e.Item.FindControl("tbgzfpl");
                //TextBox tbgz = (TextBox)e.Item.FindControl("tbgz");

                if (tbjjfpl.Text == "")
                {
                    tbjjfpl.Text = "0";
                }
                jjfplhj += Convert.ToDouble(tbjjfpl.Text);
                if (tbcnjj.Text == "")
                {
                    tbcnjj.Text = "0";
                }
                cnjjhj += Convert.ToDouble(tbcnjj.Text);
                //if (tbgz.Text == "")
                //{
                //    tbgz.Text = "0";
                //}
                //gzhj += Convert.ToDouble(tbgz.Text);


                if (tbjgyzfpl.Text == "")
                {
                    tbjgyzfpl.Text = "0";
                }
                jgyzfplhj += Convert.ToDouble(tbjgyzfpl.Text);
                if (tbjgyzft.Text == "")
                {
                    tbjgyzft.Text = "0";
                }
                jgyzfthj += Convert.ToDouble(tbjgyzft.Text);
                //if (tbgzfpl.Text == "")
                //{
                //    tbgzfpl.Text = "0";
                //}
                //gzfplhj += Convert.ToDouble(tbgzfpl.Text);
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                System.Web.UI.WebControls.Label lbgsfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbgsfyhj");
                System.Web.UI.WebControls.Label lbjjfplhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjjfplhj");
                System.Web.UI.WebControls.Label lbcnjjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcnjjhj");

                System.Web.UI.WebControls.Label lbjgyzhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjgyzhj");
                System.Web.UI.WebControls.Label lbjgyzfplhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjgyzfplhj");
                System.Web.UI.WebControls.Label lbjgyzfthj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjgyzfthj");

                //Label lbgzfplhj = (Label)e.Item.FindControl("lbgzfplhj");
                //Label lbgzhj = (Label)e.Item.FindControl("lbgzhj");
                if (gsfyhj == 0)
                {
                    gsfyhj = 0.0001;
                }
                if (jgyzhj == 0)
                {
                    jgyzhj = 0.0001;
                }
                lbgsfyhj.Text = gsfyhj.ToString("0.00");
                lbjjfplhj.Text = Math.Round(jjfplhj,2).ToString();
                lbcnjjhj.Text = Math.Round(cnjjhj,2).ToString();

                lbjgyzhj.Text = jgyzhj.ToString("0.00");
                lbjgyzfplhj.Text = Math.Round(jgyzfplhj,2).ToString();
                lbjgyzfthj.Text = Math.Round(jgyzfthj,2).ToString();

                //lbgzfplhj.Text = Math.Round(gzfplhj,2).ToString();
                //lbgzhj.Text = Math.Round(gzhj,2).ToString();
            }
        }

        //根据工时计算，将费用分摊到每个任务号
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.TextBox tbgsfy = (System.Web.UI.WebControls.TextBox)item.FindControl("tbgsfy");
                gsfyhj += Convert.ToDouble(tbgsfy.Text);
                System.Web.UI.WebControls.TextBox tbjgyz = (System.Web.UI.WebControls.TextBox)item.FindControl("tbjgyz");
                jgyzhj += Convert.ToDouble(tbjgyz.Text);
            }
            System.Data.DataTable dt = dtcreate();
            for(int i=0;i<dt.Rows.Count;i++)
            { 
                double jjfpl = Math.Round((Convert.ToDouble(dt.Rows[i]["CB_GSFY"]) / gsfyhj), 10);
                double jgyzfpl = Math.Round((Convert.ToDouble(dt.Rows[i]["CB_JGYZ"]) / jgyzhj), 10);
                //double gzfpl = Math.Round((Convert.ToDouble(dt.Rows[i]["CB_ZJRG"]) / (gsfyhj+jgyzhj)), 10);
                dt.Rows[i]["CB_JJFPL"] = jjfpl;
                dt.Rows[i]["CB_JGYZFPL"] = jgyzfpl;
                //dt.Rows[i]["CB_GZFPL"] = gzfpl;
                dt.Rows[i]["CB_CNJJ"] = Math.Round((Convert.ToDouble(txtcnjj.Text) * jjfpl), 6);
                dt.Rows[i]["CB_JGYZFT"] = Math.Round((Convert.ToDouble(txtjgyzft.Text) * jgyzfpl), 6);
                //dt.Rows[i]["CB_GZ"] = Math.Round((Convert.ToDouble(txtgz.Text) * gzfpl), 6);
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
            string sqltext = "select * from CB_FT_JGYZJJ where CB_YEAR='" + year + "' and CB_MONTH='" + month + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                sqltextsc = "delete from CB_FT_JGYZJJ where CB_YEAR='" + year + "' and CB_MONTH='" + month + "'";
                list_sqlsc.Add(sqltextsc);
                DBCallCommon.ExecuteTrans(list_sqlsc);
            }
            foreach (RepeaterItem item in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.TextBox tbid = (System.Web.UI.WebControls.TextBox)item.FindControl("tbid");
                System.Web.UI.WebControls.TextBox tbgsfy = (System.Web.UI.WebControls.TextBox)item.FindControl("tbgsfy");
                System.Web.UI.WebControls.TextBox tbjjfpl = (System.Web.UI.WebControls.TextBox)item.FindControl("tbjjfpl");
                System.Web.UI.WebControls.TextBox tbcnjj = (System.Web.UI.WebControls.TextBox)item.FindControl("tbcnjj");

                System.Web.UI.WebControls.TextBox tbjgyz = (System.Web.UI.WebControls.TextBox)item.FindControl("tbjgyz");
                System.Web.UI.WebControls.TextBox tbjgyzfpl = (System.Web.UI.WebControls.TextBox)item.FindControl("tbjgyzfpl");
                System.Web.UI.WebControls.TextBox tbjgyzft = (System.Web.UI.WebControls.TextBox)item.FindControl("tbjgyzft");

                //TextBox tbgzfpl = (TextBox)item.FindControl("tbgzfpl");
                //TextBox tbgz = (TextBox)item.FindControl("tbgz");
                //sqltextadd = "insert into CB_FT_JGYZJJ(CB_TSAID,CB_GSFY,CB_JJFPL,CB_CNJJ,CB_JGYZ,CB_JGYZFPL,CB_JGYZFT,CB_GZFPL,CB_GZ,CB_YEAR,CB_MONTH) Values('" + tbid.Text.Trim() + "','" + Convert.ToDouble(tbgsfy.Text.Trim()) + "','" +Convert.ToDouble(tbjjfpl.Text.Trim()) + "','" + Convert.ToDouble(tbcnjj.Text.Trim()) + "','"+Convert.ToDouble(tbjgyz.Text.Trim())+"','"+Convert.ToDouble(tbjgyzfpl.Text.Trim())+"','"+Convert.ToDouble(tbjgyzft.Text.Trim())+"','"+Convert.ToDouble(tbgzfpl.Text.Trim())+"','"+ Convert.ToDouble(tbgz.Text.Trim()) + "','" + year + "','" + month + "')";
                sqltextadd = "insert into CB_FT_JGYZJJ(CB_TSAID,CB_GSFY,CB_JJFPL,CB_CNJJ,CB_JGYZ,CB_JGYZFPL,CB_JGYZFT,CB_YEAR,CB_MONTH) Values('" + tbid.Text.Trim() + "','" + Convert.ToDouble(tbgsfy.Text.Trim()) + "','" + Convert.ToDouble(tbjjfpl.Text.Trim()) + "','" + Convert.ToDouble(tbcnjj.Text.Trim()) + "','" + Convert.ToDouble(tbjgyz.Text.Trim()) + "','" + Convert.ToDouble(tbjgyzfpl.Text.Trim()) + "','" + Convert.ToDouble(tbjgyzft.Text.Trim()) + "','" + year + "','" + month + "')";
                list_sqladd.Add(sqltextadd);
            }
            DBCallCommon.ExecuteTrans(list_sqladd);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);

        }



        protected void btn_export_Click(object sender, EventArgs e)
        {
            year = Request.QueryString["year"].ToString().Trim();
            month = Request.QueryString["month"].ToString().Trim();
            string sqlzjrg = "select CB_TSAID,cast(CB_GSFY as decimal(18,2)) as CB_GSFY,cast(CB_JJFPL as decimal(18,4)) as CB_JJFPL,cast(CB_CNJJ as decimal(18,2)) as CB_CNJJ,cast(CB_JGYZ as decimal(18,2)) as CB_JGYZ,cast(CB_JGYZFPL as decimal(18,4)) as CB_JGYZFPL,cast(CB_JGYZFT as decimal(18,2)) as CB_JGYZFT,CB_YEAR,CB_MONTH from CB_FT_JGYZJJ where CB_YEAR='" + year + "' and CB_MONTH='" + month + "'";
            System.Data.DataTable dtzjrg = DBCallCommon.GetDTUsingSqlText(sqlzjrg);
            string filename = "直接人工费分摊导出" + DateTime.Now.ToString("yyyyMMdd HHmmss").Trim() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("直接人工费分摊导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtzjrg.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dtzjrg.Columns.Count; j++)
                    {
                        string str = dtzjrg.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtzjrg.Columns.Count; r++)
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
