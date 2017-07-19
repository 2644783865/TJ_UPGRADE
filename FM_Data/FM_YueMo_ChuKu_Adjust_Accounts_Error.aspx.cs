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
using Microsoft.Office.Interop.Excel;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_YueMo_ChuKu_Adjust_Accounts_Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = string.Empty;

            if (!IsPostBack)
            {
                id = Request.QueryString["errorID"];
                BindErrorInfoByID(id);
            }
        }

        private void BindErrorInfoByID(string ID)
        {
            if(ID=="2")
            {
                GridViewNoVerityOut.Visible = false;

                LabelErrorMsg.Text = "系统有未审核的入库单，请审核之后，进行核算!";
                BindNoVerityInItem();
            }
            else if (ID == "3")
            {
                GridViewNoVerityIn.Visible = false;

                LabelErrorMsg.Text = "系统有未审核的出库单，请审核之后，进行核算!";
                BindNoVerityOutItem();
            }
            else if (ID == "4")
            {
                GridViewNoVerityOut.Visible = false;

                LabelErrorMsg.Text = "系统在核算期间，出现了未核算的入库单，请重新核算！";

                BindNoHSInItem();
            }

            else if (ID == "5")
            {
                GridViewNoVerityIn.Visible = false;

                LabelErrorMsg.Text = "系统在核算期间，出现了未核算的出库单，请重新核算！";

                BindNoHSOutItem();
            }
        }

        private void BindNoVerityInItem()
        {
           
            string sql = "select WG_CODE,DocName,WG_DATE,VerfierName,left(WG_VERIFYDATE,10) as WG_VERIFYDATE from View_SM_INTotal where WG_STATE='1' AND WG_VERIFYDATE<=convert(varchar(50),getdate(),120)";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            GridViewNoVerityIn.DataSource = dt;
            GridViewNoVerityIn.DataBind();

        }

        private void BindNoVerityOutItem()
        {

            string sql = "select id as OutCode,Doc,Date,Verifier,left(ApprovedDate,10) as ApprovedDate from View_SM_OUTTotal where OP_STATE='1' AND ApprovedDate<=convert(varchar(50),getdate(),120)";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            GridViewNoVerityOut.DataSource = dt;
            GridViewNoVerityOut.DataBind();

        }
        //WG_HSFLAG

        private void BindNoHSInItem()
        {

            string sql = "select WG_CODE,DocName,WG_DATE,VerfierName,left(WG_VERIFYDATE,10) as WG_VERIFYDATE from View_SM_INTotal where WG_HSFLAG='0' AND WG_VERIFYDATE<=convert(varchar(50),getdate(),120)";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            GridViewNoVerityIn.DataSource = dt;
            GridViewNoVerityIn.DataBind();

        }

        private void BindNoHSOutItem()
        {

            string sql = "select id as OutCode,Doc,Date,Verifier,left(ApprovedDate,10) as ApprovedDate from View_SM_OUTTotal where OP_HSFLAG='0' AND ApprovedDate<=convert(varchar(50),getdate(),120)";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            GridViewNoVerityOut.DataSource = dt;
            GridViewNoVerityOut.DataBind();

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sql = "select WG_CODE as '单号',DocName as '制单人',WG_DATE as '制单日期','入库单' as '单据类型' from View_SM_INTotal where WG_STATE='1' AND WG_VERIFYDATE<=convert(varchar(50),getdate(),120)";
            sql += " union all ";
            sql += " select cast(id as varchar(50)) as '单号',Doc as '制单人',Date as '制单日期','出库单' as '单据类型' from View_SM_OUTTotal where OP_STATE='1' AND ApprovedDate<=convert(varchar(50),getdate(),120)";


            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ApplicationClass ac = new ApplicationClass();

            ac.Visible = false;     // Excel不显示  
            ac.DisplayAlerts = false;  // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  


            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).NumberFormatLocal = "@";
            object[,] dataArray = new object[rowCount, colCount + 1];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = (object)(i + 1);

                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j + 1] = ("'" + dt.Rows[i][j].ToString()) as object;

                }
            }

            //设置表头
            wksheet.Cells[1, 1] = "序号";

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                wksheet.Cells[1, i + 2] = dt.Columns[i].ColumnName;
            }
            //A2表示第二行第一列
            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).Value2 = dataArray;


            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
          
            string filename = Server.MapPath("/SM_Data/ExportFile/" + "单据" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();

            #region kill excel process

            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process p in procs)
            {
                int baseAdd = p.MainModule.BaseAddress.ToInt32();
                //oXL is Excel.ApplicationClass object 
                if (baseAdd == ac.Hinstance)
                {
                    p.Kill();
                    break;
                }

            }
            #endregion

            wkbook = null;
            ac = null;
            GC.Collect();

            if (File.Exists(filename))
            {
                DownloadFile.Send(Context, filename);
            }
        
        
        }
    }
}
