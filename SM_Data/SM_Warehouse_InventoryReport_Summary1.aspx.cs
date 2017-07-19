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
    public partial class SM_Warehouse_InventoryReport_Summary1 : System.Web.UI.Page
    {

      

        private float sum11 = 0;
        private float sum12 = 0;
        private float sum13 = 0;
        private float sum14 = 0;
        private float sum15 = 0;
        private float sum16 = 0;
        private float sum17 = 0;
        private float sum18 = 0;
        private float sum19 = 0;
        private float sum110 = 0;
        private float sum111 = 0;
        private float sum112 = 0;
        private float sum113 = 0;
        private float sum114 = 0;
        private float sum115 = 0;
        private float sum116 = 0;
        private float sum117 = 0;
        private float sum118 = 0;

        PagerQueryParamGroupBy pager1 = new PagerQueryParamGroupBy();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar1();
            if (!IsPostBack)
            {
                bindGrid1();
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
        protected void Summary1()
        {

            pager1.TableName = "View_SM_InventoryReport";

            pager1.PrimaryKey = "MaterialCode+cast(NumberInAccount as varchar(50))+cast(Width as varchar(50))";

            pager1.ShowFields = "MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute,GB,Standard AS MaterialStandard,Unit AS Unit,cast((case when isnull(sum(SupportNumberInAccount),0)<>0 then round(sum(NumberInAccount)/sum(SupportNumberInAccount),4) else sum(NumberInAccount) end ) as float) AS UnitWeight,Length AS Length,Width AS Width,cast(SUM(NumberInAccount) as float) AS NumInAccount,cast(SUM(SupportNumberInAccount) as float) AS SNInAccount,cast(SUM(AmountInAccount) as float) AS AmountInAccount,cast(SUM(NumberNotIn) as float) AS NumNotIn,cast(SUM(SupportNumberNotIn) as float) AS SNNotIn,cast(SUM(AmountNotIn) as float) AS AmountNotIn,cast(SUM(NumberNotOut) as float) AS NumNotOut,cast(SUM(SupportNumberNotOut) as float) AS SNNotOut,cast(SUM(AmountNotOut) as float) AS AmountNotOut,cast(SUM(DueNumber) as float) AS NumDueToAccount,cast(SUM(DueSupportNumber) as float) AS SNDueToAccount, cast(SUM(DueAmount) as float) AS AmountDueToAccount,cast(SUM(RealNumber) as float) AS NumInventory,cast(SUM(RealSupportNumber) as float) AS SNInventory,cast(SUM(RealAmount) as float) AS AmountInventory,cast(SUM(DiffNumber) as float) AS NumDiff,cast(SUM(DiffSupportNumber) as float) AS SNDiff,cast(SUM(DiffAmount) as float) AS AmountDiff";
            //数据库中的主键
            pager1.OrderField = "MaterialCode,MaterialName,Attribute,GB,Standard,Unit,Length,Width";

            pager1.GroupField = "MaterialCode,MaterialName,Attribute,GB,Standard,Unit,Length,Width";

            pager1.OrderType = 0;
           
            pager1.StrWhere = "PDCode='" + Server.UrlDecode(Request.QueryString["code"]) + "'";

            pager1.PageSize = 100;

        }




        private void InitVar1()
        {
            Summary1();
            UCPaging2.PageChanged += new UCPaging.PageHandler(Pager_PageChanged1);
            UCPaging2.PageSize = pager1.PageSize;
        }

        void Pager_PageChanged1(int pageNumber)
        {
            bindGrid1();
        }


        private void bindGrid1()
        {
            bindSunNum();
            pager1.PageIndex = UCPaging2.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager1);
            CommonFun.Paging(dt, Repeater2, UCPaging2, NoDataPanel2);
            if (NoDataPanel2.Visible)
            {
                UCPaging2.Visible = false;
            }
            else
            {
                UCPaging2.Visible = true;
                UCPaging2.InitPageInfo();
            }
           
        }

        private void bindSunNum()
        {
            string sql = "select cast(sum(NumberInAccount) as float) as NumberInAccount,cast(sum(SupportNumberInAccount) as float) as SupportNumberInAccount,cast(sum(AmountInAccount) as float) as AmountInAccount, cast(sum(NumberNotIn) as float) as NumberNotIn,cast(sum(SupportNumberNotIn) as float) as SupportNumberNotIn,cast(sum(AmountNotIn) as float) as AmountNotIn,cast(sum(NumberNotOut) as float) as NumberNotOut,cast(sum(SupportNumberNotOut) as float) as SupportNumberNotOut,cast(sum(AmountNotOut) as float) as AmountNotOut,cast(sum(DueNumber) as float) as DueNumber,cast(sum(DueSupportNumber) as float) as DueSupportNumber,cast(sum(DueAmount) as float) as DueAmount,cast(sum(RealNumber) as float) as RealNumber,cast(sum(RealSupportNumber) as float) as RealSupportNumber,cast(sum(RealAmount) as float) as RealAmount,cast(sum(DiffNumber) as float) as DiffNumber,cast(sum(DiffSupportNumber) as float) as DiffSupportNumber,cast(sum(DiffAmount) as float) as DiffAmount from View_SM_InventoryReport where " + pager1.StrWhere;
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                sumnuminaccount.Value = dr["NumberInAccount"].ToString();
                sumfzninaccount.Value = dr["SupportNumberInAccount"].ToString();
                sumamountinaccount.Value = dr["AmountInAccount"].ToString();
                sumnotinnum.Value = dr["NumberNotIn"].ToString();
                sumnotinfznum.Value = dr["SupportNumberNotIn"].ToString();
                sumnotinamount.Value = dr["AmountNotIn"].ToString();
                sumnotoutnum.Value = dr["NumberNotOut"].ToString();
                sumnotoutfznum.Value = dr["SupportNumberNotOut"].ToString();
                sumnotoutamount.Value = dr["AmountNotOut"].ToString();
                sumduenum.Value = dr["DueNumber"].ToString();
                sumduefznum.Value = dr["DueSupportNumber"].ToString();

                sumdueamount.Value = dr["DueAmount"].ToString();

                sumrealnum.Value = dr["RealNumber"].ToString();
                sumrealfznum.Value = dr["RealSupportNumber"].ToString();
                sumrealamount.Value = dr["RealAmount"].ToString();

                sumdiffnum.Value = dr["DiffNumber"].ToString();
                sumdifffznum.Value = dr["DiffSupportNumber"].ToString();
                sumdiffamount.Value = dr["DiffAmount"].ToString();

            }
            dr.Close();

        }



        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = e.Item.DataItem as DataRowView;
                if (row["NumInAccount"].ToString() != "")
                {
                    sum11 += Convert.ToSingle(row["NumInAccount"]);
                }
                if (row["SNInAccount"].ToString() != "")
                {
                    sum12 += Convert.ToSingle(row["SNInAccount"]);
                }
                if (row["AmountInAccount"].ToString() != "")
                {
                    sum13 += Convert.ToSingle(row["AmountInAccount"]);
                }
                if (row["NumNotIn"].ToString() != "")
                {
                    sum14 += Convert.ToSingle(row["NumNotIn"]);
                }
                if (row["SNNotIn"].ToString() != "")
                {
                    sum15 += Convert.ToSingle(row["SNNotIn"]);
                }
                if (row["AmountNotIn"].ToString() != "")
                {
                    sum16 += Convert.ToSingle(row["AmountNotIn"]);
                }
                if (row["NumNotOut"].ToString() != "")
                {
                    sum17 += Convert.ToSingle(row["NumNotOut"]);
                }
                if (row["SNNotOut"].ToString() != "")
                {
                    sum18 += Convert.ToSingle(row["SNNotOut"]);
                }
                if (row["AmountNotOut"].ToString() != "")
                {
                    sum19 += Convert.ToSingle(row["AmountNotOut"]);
                }
                if (row["NumDueToAccount"].ToString() != "")
                {
                    sum110 += Convert.ToSingle(row["NumDueToAccount"]);
                }
                if (row["SNDueToAccount"].ToString() != "")
                {
                    sum111 += Convert.ToSingle(row["SNDueToAccount"]);
                }
                if (row["AmountDueToAccount"].ToString() != "")
                {
                    sum112 += Convert.ToSingle(row["AmountDueToAccount"]);
                }
                if (row["NumInventory"].ToString() != "")
                {
                    sum113 += Convert.ToSingle(row["NumInventory"]);
                }
                if (row["SNInventory"].ToString() != "")
                {
                    sum114 += Convert.ToSingle(row["SNInventory"]);
                }
                if (row["AmountInventory"].ToString() != "")
                {
                    sum115 += Convert.ToSingle(row["AmountInventory"]);
                }
                if (row["NumDiff"].ToString() != "")
                {
                    sum116 += Convert.ToSingle(row["NumDiff"]);
                }
                if (row["SNDiff"].ToString() != "")
                {
                    sum117 += Convert.ToSingle(row["SNDiff"]);
                }
                if (row["AmountDiff"].ToString() != "")
                {
                    sum118 += Convert.ToSingle(row["AmountDiff"]);
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum1")).Text = sum11.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum2")).Text = sum12.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum3")).Text = sum13.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum4")).Text = sum14.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum5")).Text = sum15.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum6")).Text = sum16.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum7")).Text = sum17.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum8")).Text = sum18.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum9")).Text = sum19.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum10")).Text = sum110.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum11")).Text = sum111.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum12")).Text = sum112.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum13")).Text = sum113.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum14")).Text = sum114.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum15")).Text = sum115.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum16")).Text = sum116.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum17")).Text = sum117.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum18")).Text = sum118.ToString();

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum1")).Text = sumnuminaccount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum2")).Text = sumfzninaccount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum3")).Text = sumamountinaccount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum4")).Text = sumnotinnum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum5")).Text = sumnotinfznum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum6")).Text = sumnotinamount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum7")).Text = sumnotoutnum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum8")).Text = sumnotoutfznum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum9")).Text = sumnotoutamount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum10")).Text = sumduenum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum11")).Text = sumduefznum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum12")).Text = sumdueamount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum13")).Text = sumrealnum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum14")).Text = sumrealfznum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum15")).Text = sumrealamount.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum16")).Text = sumdiffnum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum17")).Text = sumdifffznum.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("Sum18")).Text = sumdiffamount.Value;
            }
        }

        //导出汇总1

        protected void ExportSummary1_Click(object sender, EventArgs e)
        {

            //按照物料代码，规格型号，长，宽，单重汇总，主要是为了汇总张数
            string sql = "SELECT  MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute,Standard,GB," +
                "Length AS Length,Width AS Width,Unit AS Unit,cast((case when isnull(sum(SupportNumberInAccount),0)<>0 then round(sum(NumberInAccount)/sum(SupportNumberInAccount),4) else sum(NumberInAccount) end ) as float) AS UnitWeight," +
                "SUM(NumberInAccount) AS NumInAccount,SUM(SupportNumberInAccount) AS SNInAccount," +
                "SUM(NumberNotIn) AS NumNotIn,SUM(SupportNumberNotIn) AS SNNotIn," +
                "SUM(NumberNotOut) AS NumNotOut,SUM(SupportNumberNotOut) AS SNNotOut," +
                "SUM(DueNumber) AS NumDueToAccount,SUM(DueSupportNumber) AS SNDueToAccount," +
                "SUM(RealNumber) AS NumInventory,SUM(RealSupportNumber) AS SNInventory," +
                "SUM(DiffNumber) AS NumDiff,SUM(DiffSupportNumber) AS SNDiff " +
                "FROM View_SM_InventoryReport " +
                "WHERE PDCode='" + Server.UrlDecode(Request.QueryString["code"]) +
                "' GROUP BY MaterialCode,MaterialName,Attribute,Standard,GB,Unit,UnitPrice,NumberInAccount,Length,Width order by MaterialCode,MaterialName,Attribute,GB,Standard,Unit,NumberInAccount,Length,Width";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ExportDataOne(dt);

        }
        private void ExportDataOne(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("库存盘点基本模版汇总1") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            wksheet.get_Range("L4", "L4").Value2 = "仓库名称：" + LabelWarehouse.Text.Trim();
            wksheet.get_Range("V4", "V4").Value2 = "盘点日期：" + LabelTime.Text.Trim().Substring(0, 10);
            System.Data.DataTable dt = objdt;


            int rowCount = objdt.Rows.Count;

            int colCount = objdt.Columns.Count;

            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 3]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 3]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 3]).NumberFormatLocal = "@";

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
