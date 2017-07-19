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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_GYSHZ : System.Web.UI.Page
    {
        double jehj = 0;
        double hsjehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
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
            string month = (DateTime.Now.Month - 1).ToString();
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

        #region  分页
        PagerQueryParamGroupBy pager_org = new PagerQueryParamGroupBy();

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "(select WG_CODE,left(CONVERT(CHAR(10),WG_VERIFYDATE,23),10) as WG_VERIFYDATE,case when SupplierName is null then WG_COMPANY else SupplierName end as SupplierName,WG_MARID,MNAME,GUIGE,CAIZHI,WG_RSNUM,WG_UPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_GJSTATE,WG_GJFLAG from View_SM_IN)t";
            pager_org.PrimaryKey = "SupplierName";
            pager_org.ShowFields = "SupplierName,sum(WG_AMOUNT) as WG_AMOUNT,sum(WG_CTAMTMNY) as WG_CTAMTMNY";
            pager_org.OrderField = "SupplierName";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;
            pager_org.PageSize = 50;
            pager_org.GroupField = "SupplierName";
        }

        private string strstring()
        {
            string sqlText = "1=1";
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                string yearmonth = dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString();
                sqlText += " and WG_VERIFYDATE like '" + yearmonth.ToString() + "%' and (WG_CODE like 'G%' or WG_CODE like 'T%') and (WG_GJFLAG is null or WG_GJFLAG='0')";
            }

            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                string yearmonth = dplYear.SelectedValue.ToString() + "-";
                sqlText += " and WG_VERIFYDATE like '" + yearmonth.ToString() + "%' and (WG_CODE like 'G%' or WG_CODE like 'T%') and (WG_GJFLAG is null or WG_GJFLAG='0')";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                string yearmonth = "-" + dplMoth.SelectedValue.ToString() + "-";
                sqlText += " and WG_VERIFYDATE like '%" + yearmonth.ToString() + "%' and (WG_CODE like 'G%' or WG_CODE like 'T%') and (WG_GJFLAG is null or WG_GJFLAG='0')";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex == 0)
            {
                sqlText += " and (WG_CODE like 'G%' or WG_CODE like 'T%') and (WG_GJFLAG is null or WG_GJFLAG='0')";
            }
            if (gys.Text.ToString() != "")
            {
                sqlText += " and (WG_COMPANY like '%" + gys.Text.ToString().Trim() + "%' or SupplierName like '%" + gys.Text.ToString().Trim() + "%')";
            }
            return sqlText;
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
        #endregion
        /// <summary>
        /// 年、月份改变 查询
        /// </summary>
        protected void OnSelectedIndexChanged_dplYearMoth(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }



        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }



        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string condition = strstring();
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbje = (Label)e.Item.FindControl("lbje");
                Label lbhsje = (Label)e.Item.FindControl("lbhsje");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(sum(isnull(WG_AMOUNT,0)),0) as WG_AMOUNT,isnull(sum(isnull(WG_CTAMTMNY,0)),0) as WG_CTAMTMNY from View_SM_IN where " + condition;

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {
                    jehj = Convert.ToDouble(drhj["WG_AMOUNT"].ToString());
                    hsjehj = Convert.ToDouble(drhj["WG_CTAMTMNY"].ToString());
                }
                drhj.Close();
                Label lbjehj = (Label)e.Item.FindControl("lbjehj");
                Label lbhsjehj = (Label)e.Item.FindControl("lbhsjehj");

                lbjehj.Text = jehj.ToString();
                lbhsjehj.Text = hsjehj.ToString();
            }
        }


        //导出
        protected void btnexport_OnClick(object sender, EventArgs e)
        {
            string sql = "select SupplierName,sum(WG_AMOUNT) as WG_AMOUNT,sum(WG_CTAMTMNY) as WG_CTAMTMNY from (select WG_CODE,left(CONVERT(CHAR(10),WG_VERIFYDATE,23),10) as WG_VERIFYDATE,case when SupplierName is null then WG_COMPANY else SupplierName end as SupplierName,WG_MARID,MNAME,GUIGE,CAIZHI,WG_RSNUM,WG_UPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_GJSTATE,WG_GJFLAG from View_SM_IN)t where " + strstring() + " group by SupplierName";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string filename = "未勾稽物料按供应商汇总.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("未勾稽物料按供应商汇总.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dt.Columns.Count; r++)
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
