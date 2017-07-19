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
    public partial class FM_FJCBHZ : System.Web.UI.Page
    {
        double hsjezj = 0;
        double bhsjezj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                ViewState["StrWhere"] = "1=1";
                this.InitVar();
                this.bindGrid();
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
        }//
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


        #region 分页
        PagerQueryParamGroupBy pager_org = new PagerQueryParamGroupBy();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager_org.TableName = "FM_FJCB";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "FJCB_TSAID,sum(ISNULL(FJCB_HJHSJE,0)) as FJCB_HJHSJE,sum(ISNULL(FJCB_HJBHSJE,0)) as FJCB_HJBHSJE,(FJCB_YEAR+'-'+FJCB_MONTH) as FJCB_YEARMONTH";
            pager_org.OrderField = "FJCB_TSAID";
            pager_org.StrWhere = ViewState["StrWhere"].ToString();
            pager_org.OrderType = 0;
            pager_org.PageSize = 50;
            pager_org.GroupField = "FJCB_TSAID,(FJCB_YEAR+'-'+FJCB_MONTH)";
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        #endregion

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            string sql = "1=1";
            if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex == 0)
            {
                sql = "1=1";
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                sql += " and FJCB_YEAR like '%" + dplYear.SelectedValue.ToString().Trim() + "%'";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                sql += " and FJCB_MONTH like '%" + dplMoth.SelectedValue.ToString().Trim() + "%'";
            }
            else
            {
                sql += " and FJCB_YEAR like '%" + dplYear.SelectedValue.ToString().Trim() + "%' and FJCB_MONTH like '%" + dplMoth.SelectedValue.ToString().Trim() + "%'";
            }
            if (txtrwh.Text != "")
            {
                sql += " and FJCB_TSAID like '%" + txtrwh.Text.ToString().Trim() + "%'";
            }
            ViewState["StrWhere"] = sql;
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }






        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(CAST(sum(FJCB_HJHSJE) AS FLOAT),0) as FJCB_HJHSJEZJ,isnull(CAST(sum(FJCB_HJBHSJE) AS FLOAT),0) as FJCB_HJBHSJEZJ from FM_FJCB where " + ViewState["StrWhere"].ToString();

                SqlDataReader drzj = DBCallCommon.GetDRUsingSqlText(sqlhj);
                if (drzj.Read())
                {
                    hsjezj = Convert.ToDouble(drzj["FJCB_HJHSJEZJ"]);
                    bhsjezj = Convert.ToDouble(drzj["FJCB_HJBHSJEZJ"]);
                }
                drzj.Close();
                System.Web.UI.WebControls.Label lbhsjezj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsjezj");
                System.Web.UI.WebControls.Label lbbhsjezj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbbhsjezj");
                lbhsjezj.Text = hsjezj.ToString();
                lbbhsjezj.Text = bhsjezj.ToString();

            }
        }





        protected void btn_export_Click(object sender, EventArgs e)
        {
            string condition = ViewState["StrWhere"].ToString();
            string sqlfjcb = "select FJCB_TSAID,sum(ISNULL(FJCB_HJHSJE,0)) as FJCB_HJHSJE,sum(ISNULL(FJCB_HJBHSJE,0)) as FJCB_HJBHSJE,(FJCB_YEAR+'-'+FJCB_MONTH) as FJCB_YEARMONTH from FM_FJCB where " + condition + " group by FJCB_TSAID,(FJCB_YEAR+'-'+FJCB_MONTH)";
            System.Data.DataTable dtfjcb = DBCallCommon.GetDTUsingSqlText(sqlfjcb);
            string filename = "分交成本任务号汇总.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("分交成本汇总导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtfjcb.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dtfjcb.Columns.Count; j++)
                    {
                        string str = dtfjcb.Rows[i][j].ToString();
                        row.CreateCell(j+1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtfjcb.Columns.Count; r++)
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
