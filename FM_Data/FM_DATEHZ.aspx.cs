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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_DATEHZ : System.Web.UI.Page
    {
        double jehj = 0;
        double hsjehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }

            if (IsPostBack)
            {
                this.InitVar();
            }
        }

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
            pager_org.TableName = "(select WG_CODE,left(CONVERT(CHAR(10),WG_VERIFYDATE,23),10) as WG_VERIFYDATE,WG_MARID,MNAME,GUIGE,CAIZHI,WG_RSNUM,WG_AMOUNT,WG_CTAMTMNY,WG_GJFLAG from View_SM_IN)t";
            pager_org.PrimaryKey = "SupplierName";
            pager_org.ShowFields = "WG_MARID,MNAME,GUIGE,CAIZHI,sum(WG_RSNUM) as WG_RSNUM,sum(WG_AMOUNT) as WG_AMOUNT,sum(WG_CTAMTMNY) as WG_CTAMTMNY";
            pager_org.OrderField = "WG_MARID";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;
            pager_org.PageSize = 50;
            pager_org.GroupField = "WG_MARID,MNAME,GUIGE,CAIZHI";
        }

        private string strstring()
        {
            string sqlText = "1=1 and (WG_CODE like 'G%' or WG_CODE like 'T%') and (WG_GJFLAG is null or WG_GJFLAG='0')";
            if (txtStartTime.Text.Trim() != "" && txtEndTime.Text.Trim()!="")
            {
                sqlText += " and WG_VERIFYDATE>='" + txtStartTime.Text.Trim() + "' and WG_VERIFYDATE<='" + txtEndTime.Text.Trim() + "'";
            }
            if (txtmarid.Text.ToString().Trim() != "")
            {
                sqlText += " and (WG_MARID like '%" + txtmarid.Text.ToString().Trim() + "%'";
            }
            if (txtmarname.Text.ToString().Trim() != "")
            {
                sqlText += " and (MNAME like '%" + txtmarname.Text.ToString().Trim() + "%'";
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
        #endregion

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
            string sql = "select WG_MARID,MNAME,GUIGE,CAIZHI,sum(WG_RSNUM) as WG_RSNUM,sum(WG_AMOUNT) as WG_AMOUNT,sum(WG_CTAMTMNY) as WG_CTAMTMNY from (select WG_CODE,left(CONVERT(CHAR(10),WG_VERIFYDATE,23),10) as WG_VERIFYDATE,WG_MARID,MNAME,GUIGE,CAIZHI,WG_RSNUM,WG_AMOUNT,WG_CTAMTMNY,WG_GJFLAG from View_SM_IN)t where " + strstring() + " group by WG_MARID,MNAME,GUIGE,CAIZHI";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string filename = "未勾稽物料按日期汇总.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("未勾稽物料按日期汇总.xls")))
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
