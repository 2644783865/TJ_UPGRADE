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
using ZCZJ_DPF;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_WXZGCX : System.Web.UI.Page
    {
        double zlhj = 0;
        double jehj = 0;
        double hsjehj = 0;
        double fpjehj = 0;
        double fphsjehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
                ViewState["strWhere"] = "1=1";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
                #region 隐藏重复字段
                //for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
                //{
                //    if (i == rptProNumCost.Items.Count - 2)
                //    {
                //        for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                //        {
                //            Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                //            Label lbdat1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                //            Label lbgysm1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                //            Label lbzdrx1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                //            Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                //            if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                //            {
                //                lbjsd1.Visible = false;
                //                lbdat1.Visible = false;
                //                lbgysm1.Visible = false;
                //                lbzdrx1.Visible = false;
                //            }
                //        }
                //    }
                //    Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                //    Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                //    Label lbdate2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                //    Label lbgysmc2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                //    Label lbzdrxm2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                //    if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                //    {
                //        lbjsdh2.Visible = false;
                //        lbdate2.Visible = false;
                //        lbgysmc2.Visible = false;
                //        lbzdrxm2.Visible = false;
                //    }
                //}
                #endregion
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
        }

        //初始化页面
        private void InitPage()
        {
            //显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }
            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month).ToString();
            if (DateTime.Now.Month < 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }


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





        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "(select * from ((select * from View_TBMP_ACCOUNTS)a left join (select * from TBFM_WXGJRELATION)b on a.TA_PTC=b.WXGJ_JHGZH))c";
            pager_org.PrimaryKey = "TA_PTC";
            pager_org.ShowFields = "left(CONVERT(CHAR(10), TA_ZDTIME, 23),10) as TA_ZDDATE,TA_PTC,TA_ENGID,TA_DOCNUM,TA_SUPPLYNAME,TA_ZDRNAME,TA_TUHAO,TA_MNAME,isnull(cast(TA_NUM as int),0) as TA_NUM,isnull(cast(TA_WGHT as float),0) as TA_WGHT,cast(((isnull(cast(TA_PRICE as float),0))/(1+(isnull(PIC_SHUILV,0))/100)) as decimal(12,2)) as PRICE,cast(((isnull(cast(TA_MONEY as float),0))/(1+(isnull(PIC_SHUILV,0))/100)) as decimal(12,2)) as MONEY,(cast(isnull(PIC_SHUILV,0) as char(6))+'%') as PIC_SHUILV,isnull(cast(TA_PRICE as float),0) as TA_PRICE,isnull(cast(TA_MONEY as float),0) as TA_MONEY,TA_WXTYPE,WXGJ_JE,WXGJ_HSJE,WXGJ_FPID";
            pager_org.OrderField = "TA_DOCNUM";
            pager_org.StrWhere = ViewState["strWhere"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
            #region 隐藏重复的字段
            //for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            //{
            //    if (i == rptProNumCost.Items.Count - 2)
            //    {
            //        for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
            //        {
            //            Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
            //            Label lbdat1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
            //            Label lbgysm1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
            //            Label lbzdrx1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
            //            Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
            //            if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
            //            {
            //                lbjsd1.Visible = false;
            //                lbdat1.Visible = false;
            //                lbgysm1.Visible = false;
            //                lbzdrx1.Visible = false;
            //            }
            //        }
            //    }
            //    Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
            //    Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
            //    Label lbdate2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
            //    Label lbgysmc2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
            //    Label lbzdrxm2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
            //    if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
            //    {
            //        lbjsdh2.Visible = false;
            //        lbdate2.Visible = false;
            //        lbgysmc2.Visible = false;
            //        lbzdrxm2.Visible = false;
            //    }
            //}
            #endregion
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        #endregion


        //查询
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            string condition = "1=1";
            if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex == 0)
            {
                condition = "1=1";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                condition += " and (left(CONVERT(CHAR(10), TA_ZDTIME, 23),10)) like '%-" + dplMoth.SelectedValue.ToString() + "-%'";
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                condition += " and (left(CONVERT(CHAR(10), TA_ZDTIME, 23),10)) like '" + dplYear.SelectedValue.ToString() + "-%'";
            }
            else
            {
                condition += " and (left(CONVERT(CHAR(10), TA_ZDTIME, 23),10)) like '" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "%'";
            }
            if (DropDownifgj.SelectedValue == "0")
            {
                condition = "1=1";
                condition += " and (left(CONVERT(CHAR(10), TA_ZDTIME, 23),7)) like '" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "%' and WXGJ_GJDATE like '" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "%' and TA_GJSTATE='3'";
            }
            else if (DropDownifgj.SelectedValue == "1")
            {
                condition += " and TA_GJSTATE='0'";
            }
            else if (DropDownifgj.SelectedValue == "2")
            {
                condition = "1=1";
                condition += " and (left(CONVERT(CHAR(10), TA_ZDTIME, 23),7)) like '" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "%' and (left(CONVERT(CHAR(10), WXGJ_GJDATE, 23),7))>'" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "' and TA_GJSTATE='3'";
            }
            else if (DropDownifgj.SelectedValue == "3")
            {
                condition = "1=1";
                condition += " and (left(CONVERT(CHAR(10), TA_ZDTIME, 23),7))<'" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "' and WXGJ_GJDATE like '" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "%' and TA_GJSTATE='3'";
            }
            if (tbjsdh.Text.ToString().Trim() != "")
            {
                condition += " and TA_DOCNUM like '%" + tbjsdh.Text.ToString().Trim() + "%'";
            }
            if (txtrwh.Text.ToString().Trim() != "")
            {
                condition += " and TA_ENGID like '%" + txtrwh.Text.ToString().Trim() + "%'";
            }
            ViewState["strWhere"] = condition;
            this.InitVar();
            this.bindGrid();
        }
        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)//计算合计值
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lbdate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbdate");
                System.Web.UI.WebControls.Label lbjhgzh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjhgzh");
                System.Web.UI.WebControls.Label lbrwh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbrwh");
                System.Web.UI.WebControls.Label lbjsdh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjsdh");
                System.Web.UI.WebControls.Label lbgysmc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbgysmc");
                System.Web.UI.WebControls.Label lbzdrxm = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzdrxm");
                System.Web.UI.WebControls.Label lbtuhao = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbtuhao");
                System.Web.UI.WebControls.Label lbmname = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbmname");
                System.Web.UI.WebControls.Label lbsl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbsl");
                System.Web.UI.WebControls.Label lbzl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzl");
                System.Web.UI.WebControls.Label lbdj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbdj");
                System.Web.UI.WebControls.Label lbje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbje");
                System.Web.UI.WebControls.Label lbshuilv = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbshuilv");
                System.Web.UI.WebControls.Label lbhsdj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbdj");
                System.Web.UI.WebControls.Label lbhsje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbje");
                System.Web.UI.WebControls.Label lbwxtype = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwxtype");

                System.Web.UI.WebControls.Label lbfpje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfpje");
                System.Web.UI.WebControls.Label lbfphsje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfphsje");
                System.Web.UI.WebControls.Label lbfpbh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfpbh");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(CAST(sum(TA_WGHT) AS FLOAT),0) as TA_totalWGHT,isnull(CAST(sum(TA_MONEY) AS FLOAT),0) as TA_totalMONEY,isnull(sum(cast((TA_MONEY/(((isnull(PIC_SHUILV,0))/100)+1)) as decimal(12,2))),0) as TA_totalbhsje,isnull(sum(WXGJ_JE),0) as totalWXGJ_JE,isnull(sum(WXGJ_HSJE),0) as totalWXGJ_HSJE from (select * from ((select * from View_TBMP_ACCOUNTS)a left join (select * from TBFM_WXGJRELATION)b on a.TA_PTC=b.WXGJ_JHGZH))c  where " + ViewState["strWhere"].ToString();

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlhj);
                if (dt.Rows.Count>0)
                {
                    zlhj = Convert.ToDouble(dt.Rows[0]["TA_totalWGHT"]);
                    jehj = Convert.ToDouble(dt.Rows[0]["TA_totalbhsje"]);
                    hsjehj = Convert.ToDouble(dt.Rows[0]["TA_totalMONEY"]);
                    fpjehj = Convert.ToDouble(dt.Rows[0]["totalWXGJ_JE"]);
                    fphsjehj = Convert.ToDouble(dt.Rows[0]["totalWXGJ_HSJE"]);
                }
                System.Web.UI.WebControls.Label lbzlhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzlhj");
                System.Web.UI.WebControls.Label lbjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjehj");
                System.Web.UI.WebControls.Label lbhsjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsjehj");
                System.Web.UI.WebControls.Label lbfpjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfpjehj");
                System.Web.UI.WebControls.Label lbfphsjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfphsjehj");
                lbzlhj.Text = zlhj.ToString("0.0000");
                lbjehj.Text = jehj.ToString();
                lbhsjehj.Text = hsjehj.ToString();
                lbfpjehj.Text = fpjehj.ToString();
                lbfphsjehj.Text = fphsjehj.ToString();

            }
        }




        protected void btnexport_click(object sender, EventArgs e)
        {
            string condition = ViewState["strWhere"].ToString();
            string sqlwxzg = "select TA_ENGID,isnull(sum(cast(((isnull(cast(TA_MONEY as float),0))/(1+(isnull(PIC_SHUILV,0))/100)) as decimal(12,2))),0) as TA_rwhbhsje,isnull(sum(WXGJ_JE),0) as WXGJ_JEhj from (select * from ((select * from View_TBMP_ACCOUNTS)a left join (select * from TBFM_WXGJRELATION)b on a.TA_PTC=b.WXGJ_JHGZH))c where " + condition + " group by TA_ENGID";
            System.Data.DataTable dtwxzg = DBCallCommon.GetDTUsingSqlText(sqlwxzg);
            string filename = "外协暂估导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("外协暂估导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtwxzg.Rows.Count; i++) 
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i+1);
                    for (int j = 0; j < dtwxzg.Columns.Count; j++)
                    {
                        string str = dtwxzg.Rows[i][j].ToString();
                        row.CreateCell(j+1).SetCellValue(str);
                    }
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= dtwxzg.Columns.Count; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }

                }
                for (int r = 0; r <= dtwxzg.Columns.Count; r++)
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
