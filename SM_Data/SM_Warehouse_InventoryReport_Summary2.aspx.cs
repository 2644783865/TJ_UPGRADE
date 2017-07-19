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
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_InventoryReport_Summary2 : System.Web.UI.Page
    {

        private float sum21 = 0;
        private float sum22 = 0;
        private float sum23 = 0;
        private float sum24 = 0;
        private float sum25 = 0;
        private float sum26 = 0;
        private float sum27 = 0;
        private float sum28 = 0;
        private float sum29 = 0;
        private float sum210 = 0;
        private float sum211 = 0;
        private float sum212 = 0;


        PagerQueryParamGroupBy pager1 = new PagerQueryParamGroupBy();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar2(); ;
            if (!IsPostBack)
            {
                bindGrid2();
                bindInfo();
            }
        }
        private void bindInfo()
        {
            string sql = "SELECT ExportDate as ExportDate,Warehouse" +
               " FROM View_SM_InventorySchema WHERE PDCode='" + Server.UrlDecode(Request.QueryString["code"]) + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                LabelTime.Text = dr["ExportDate"].ToString();//系统封账时间

                LabelWarehouse.Text = dr["Warehouse"].ToString();
            }

        }
        private void InitVar2()
        {
            Summary2();
            UCPaging3.PageChanged += new UCPaging.PageHandler(Pager_PageChanged2);
            UCPaging3.PageSize = pager1.PageSize;
        }
        void Pager_PageChanged2(int pageNumber)
        {
            bindGrid2();
        }
        private void bindGrid2()
        {
            bindSunNum();
            pager1.PageIndex = UCPaging3.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager1);
            CommonFun.Paging(dt, Repeater3, UCPaging3, NoDataPanel3);
            if (NoDataPanel3.Visible)
            {
                UCPaging3.Visible = false;
            }
            else
            {
                UCPaging3.Visible = true;
                UCPaging3.InitPageInfo();
            }
        }

        private void bindSunNum()
        {
            string sql = "select cast(sum(NumberInAccount) as float) as NumberInAccount,cast(sum(AmountInAccount) as float) as AmountInAccount, cast(sum(NumberNotIn) as float) as NumberNotIn,cast(sum(AmountNotIn) as float) as AmountNotIn,cast(sum(NumberNotOut) as float) as NumberNotOut,cast(sum(AmountNotOut) as float) as AmountNotOut,cast(sum(DueNumber) as float) as DueNumber,cast(sum(DueAmount) as float) as DueAmount,cast(sum(RealNumber) as float) as RealNumber,cast(sum(RealAmount) as float) as RealAmount,cast(sum(DiffNumber) as float) as DiffNumber,cast(sum(DiffAmount) as float) as DiffAmount from View_SM_InventoryReport where " + pager1.StrWhere;
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                sumnuminaccount.Value = dr["NumberInAccount"].ToString();
                
                sumamountinaccount.Value = dr["AmountInAccount"].ToString();
                sumnotinnum.Value = dr["NumberNotIn"].ToString();
                
                sumnotinamount.Value = dr["AmountNotIn"].ToString();
                sumnotoutnum.Value = dr["NumberNotOut"].ToString();
               
                sumnotoutamount.Value = dr["AmountNotOut"].ToString();
                sumduenum.Value = dr["DueNumber"].ToString();

                sumdueamount.Value = dr["DueAmount"].ToString();

                sumrealnum.Value = dr["RealNumber"].ToString();
               
                sumrealamount.Value = dr["RealAmount"].ToString();

                sumdiffnum.Value = dr["DiffNumber"].ToString();
               
                sumdiffamount.Value = dr["DiffAmount"].ToString();

            }
            dr.Close();

        }



        protected void Summary2()
        {
            pager1.TableName = "View_SM_InventoryReport";

            pager1.PrimaryKey = "MaterialCode";

            pager1.ShowFields = " MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Unit,cast(UnitPrice as float) AS UnitPrice,cast(SUM(NumberInAccount) as float) AS NumInAccount,cast(SUM(AmountInAccount) as float) AS AmountInAccount,cast(SUM(NumberNotIn) as float) AS NumNotIn,cast(SUM(AmountNotIn) as float) AS AmountNotIn,cast(SUM(NumberNotOut) as float) AS NumNotOut,cast(SUM(AmountNotOut) as float) AS AmountNotOut,cast(SUM(DueNumber) as float) AS NumDueToAccount,cast(SUM(DueAmount) as float) AS AmountDueToAccount,cast(SUM(RealNumber) as float) AS NumInventory,cast(SUM(RealAmount) as float) AS AmountInventory,cast(SUM(DiffNumber) as float) AS NumDiff,cast(SUM(DiffAmount) as float) AS AmountDiff";
            //数据库中的主键
            pager1.OrderField = " MaterialCode,MaterialName,Attribute,Standard,GB,Unit,UnitPrice";

            pager1.GroupField = " MaterialCode,MaterialName,Attribute,Standard,GB,Unit,UnitPrice";

            pager1.OrderType = 0;
            /**/
            pager1.StrWhere = "PDCode='" + Server.UrlDecode(Request.QueryString["code"]) + "'";

            pager1.PageSize = 100;

        }

        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = e.Item.DataItem as DataRowView;
                if (row["NumInAccount"].ToString() != "")
                {
                    sum21 += Convert.ToSingle(row["NumInAccount"]);
                }
                if (row["AmountInAccount"].ToString() != "")
                {
                    sum22 += Convert.ToSingle(row["AmountInAccount"]);
                }
                if (row["NumNotIn"].ToString() != "")
                {
                    sum23 += Convert.ToSingle(row["NumNotIn"]);
                }
                if (row["AmountNotIn"].ToString() != "")
                {
                    sum24 += Convert.ToSingle(row["AmountNotIn"]);
                }
                if (row["NumNotOut"].ToString() != "")
                {
                    sum25 += Convert.ToSingle(row["NumNotOut"]);
                }
                if (row["AmountNotOut"].ToString() != "")
                {
                    sum26 += Convert.ToSingle(row["AmountNotOut"]);
                }
                if (row["NumDueToAccount"].ToString() != "")
                {
                    sum27 += Convert.ToSingle(row["NumDueToAccount"]);
                }
                if (row["AmountDueToAccount"].ToString() != "")
                {
                    sum28 += Convert.ToSingle(row["AmountDueToAccount"]);
                }
                if (row["NumInventory"].ToString() != "")
                {
                    sum29 += Convert.ToSingle(row["NumInventory"]);
                }
                if (row["AmountInventory"].ToString() != "")
                {
                    sum210 += Convert.ToSingle(row["AmountInventory"]);
                }
                if (row["NumDiff"].ToString() != "")
                {
                    sum211 += Convert.ToSingle(row["NumDiff"]);
                }
                if (row["AmountDiff"].ToString() != "")
                {
                    sum212 += Convert.ToSingle(row["AmountDiff"]);
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum1")).Text = sum21.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum2")).Text = sum22.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum3")).Text = sum23.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum4")).Text = sum24.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum5")).Text = sum25.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum6")).Text = sum26.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum7")).Text = sum27.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum8")).Text = sum28.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum9")).Text = sum29.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum10")).Text = sum210.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum11")).Text = sum211.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum12")).Text = sum212.ToString();

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum1")).Text = sumnuminaccount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum2")).Text = sumamountinaccount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum3")).Text = sumnotinnum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum4")).Text = sumnotinamount.Value;

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum5")).Text = sumnotoutnum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum6")).Text = sumnotoutamount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum7")).Text = sumduenum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum8")).Text = sumdueamount.Value;

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum9")).Text = sumrealnum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum10")).Text = sumrealamount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum11")).Text = sumdiffnum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum12")).Text = sumdiffamount.Value;

            }
        }

        //导出汇总1

        protected void ExportSummary1_Click(object sender, EventArgs e)
        {

            string sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute,Standard AS MaterialStandard,GB,Unit AS Unit,cast(UnitPrice as float) AS UnitPrice," +
                "cast(SUM(NumberInAccount) as float) AS NumInAccount,cast(SUM(AmountInAccount) as float) AS AmountInAccount,cast(SUM(NumberNotIn) as float) AS NumNotIn,cast(SUM(AmountNotIn) as float) AS AmountNotIn," +
                "cast(SUM(NumberNotOut) as float) AS NumNotOut,cast(SUM(AmountNotOut) as float) AS AmountNotOut,cast(SUM(DueNumber) as float) AS NumDueToAccount,cast(SUM(DueAmount) as float) AS AmountDueToAccount," +
                " cast(SUM(RealNumber) as float) AS NumInventory,cast(SUM(RealAmount) as float) AS AmountInventory,cast(SUM(DiffNumber) as float) AS NumDiff,cast(SUM(DiffAmount) as float) AS AmountDiff " +
                "FROM View_SM_InventoryReport " +
                "WHERE PDCode='" + Server.UrlDecode(Request.QueryString["code"]) +
                "' GROUP BY MaterialCode,MaterialName,Attribute,Standard,GB,Unit,UnitPrice order by MaterialCode,MaterialName,Attribute,Standard,GB,Unit,UnitPrice";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ExportDataTwo(dt);

        }
        private void ExportDataTwo(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("库存盘点基本模版汇总2") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            wksheet.get_Range("I4", "I4").Value2 = "仓库名称：" + LabelWarehouse.Text.Trim();
            wksheet.get_Range("S4", "S4").Value2 = "盘点日期：" + LabelTime.Text.Trim().Substring(0, 10);

            System.Data.DataTable dt = objdt;


            int rowCount = objdt.Rows.Count;

            int colCount = objdt.Columns.Count;

            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 3]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 3]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 3]).NumberFormatLocal = "@";

            object[,] dataArray = new object[rowCount, colCount + 1];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = (object)(i + 1);

                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j + 1] = objdt.Rows[i][j];

                }
            }

            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 1]).Value2 = dataArray;


            //设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "SummaryOne" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

        }

        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();


                #region kill excel process

                System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");

                foreach (System.Diagnostics.Process p in procs)
                {
                    int baseAdd = p.MainModule.BaseAddress.ToInt32();
                    //oXL is Excel.ApplicationClass object 
                    if (baseAdd == m_xlApp.Hinstance)
                    {
                        p.Kill();
                        break;
                    }
                }

                #endregion


                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();

                DownloadFile.Send(Context, filename);

                string script = String.Format("EndDownload()");

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "EndDownload();", true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
