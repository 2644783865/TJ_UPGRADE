using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_InventorySummary : System.Web.UI.Page
    {
        private float n1 = 0;
        private float n2 = 0;
        private float n3 = 0;
        private float n4 = 0;
        private float n5 = 0;
        private float n6 = 0;
        private float n7 = 0;
        private float n8 = 0;
        private float n9 = 0;
        private float n10 = 0;
        private float n11 = 0;
        private float n12 = 0;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initial();
            }
        }

        protected void initial()
        {
            string code = Request.QueryString["ID"].ToString();
            string sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard,Unit AS Unit,UnitPrice AS UnitPrice," +
                "SUM(NumberInAccount) AS NumInAccount,SUM(AmountInAccount) AS AmountInAccount,SUM(NumberNotIn) AS NumNotIn,SUM(AmountNotIn) AS AmountNotIn," +
                "SUM(NumberNotOut) AS NumNotOut,SUM(AmountNotOut) AS AmountNotOut,SUM(DueNumber) AS NumDueToAccount,SUM(DueAmount) AS AmountDueToAccount," +
                "SUM(RealNumber) AS NumInventory,SUM(RealAmount) AS AmountInventory,SUM(DiffNumber) AS NumDiff,SUM(DiffAmount) AS AmountDiff " +
                "FROM View_SM_InventoryReport " +
                "WHERE PDCode='" + code +
                "' GROUP BY MaterialCode,MaterialName,Standard,Unit,UnitPrice "; 
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = e.Item.DataItem as DataRowView;
                if (row["NumInAccount"].ToString() != "")
                {
                    n1 += Convert.ToSingle(row["NumInAccount"]);
                }
                if (row["AmountInAccount"].ToString() != "")
                {
                    n2 += Convert.ToSingle(row["AmountInAccount"]);
                }
                if (row["NumNotIn"].ToString() != "")
                {
                    n3 += Convert.ToSingle(row["NumNotIn"]);
                }
                if (row["AmountNotIn"].ToString() != "")
                {
                    n4 += Convert.ToSingle(row["AmountNotIn"]);
                }
                if (row["NumNotOut"].ToString() != "")
                {
                    n5 += Convert.ToSingle(row["NumNotOut"]);
                }
                if (row["AmountNotOut"].ToString() != "")
                {
                    n6 += Convert.ToSingle(row["AmountNotOut"]);
                }
                if (row["NumDueToAccount"].ToString() != "")
                {
                    n7 += Convert.ToSingle(row["NumDueToAccount"]);
                }
                if (row["AmountDueToAccount"].ToString() != "")
                {
                    n8 += Convert.ToSingle(row["AmountDueToAccount"]);
                }
                if (row["NumInventory"].ToString() != "")
                {
                    n9 += Convert.ToSingle(row["NumInventory"]);
                }
                if (row["AmountInventory"].ToString() != "")
                {
                    n10 += Convert.ToSingle(row["AmountInventory"]);
                }
                if (row["NumDiff"].ToString() != "")
                {
                    n11 += Convert.ToSingle(row["NumDiff"]);
                }
                if (row["AmountDiff"].ToString() != "")
                {
                    n12 += Convert.ToSingle(row["AmountDiff"]);
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum1")).Text = n1.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum2")).Text = n2.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum3")).Text = n3.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum4")).Text = n4.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum5")).Text = n5.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum6")).Text = n6.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum7")).Text = n7.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum8")).Text = n8.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum9")).Text = n9.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum10")).Text = n10.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum11")).Text = n11.ToString();
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelSum12")).Text = n12.ToString();
            }
        }

        //数据库改变，导出应该从视图中获得数据
        protected void ExportFile_Click(object sender, EventArgs e)
        {
            string sql = "SELECT MaterialCode,MaterialName,Standard,Unit,UnitPrice," +
                "NumberInAccount,AmountInAccount,NumberNotIn,AmountNotIn," +
                "NumberNotOut,AmountNotOut,DueNumber,DueAmount,RealNumber," +
                "RealAmount,DiffNumber,DiffAmount,Reason,Note " +
                "FROM View_SM_InventorySummary WHERE PDCode='" + Request.QueryString["ID"] + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  
            int rowIndex = 9;   // 行  
            int colIndex = 2;   // 列  
            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
            // 设置单元格格式  
            //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).NumberFormat = "@";
            //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).Font.Size = 12;

            //设置表头 
            ac.Cells[6, 1] = "序号";
            Range range;
            range = wksheet.get_Range(wksheet.Cells[6, 1], wksheet.Cells[8, 1]);  
            range.MergeCells = true;
            ac.Cells[6, 2] = "存货编码";
            range = wksheet.get_Range(wksheet.Cells[6, 2], wksheet.Cells[8, 2]);  
            range.MergeCells = true;
            ac.Cells[6, 3] = "存货名称和规格";
            range = wksheet.get_Range(wksheet.Cells[6, 3], wksheet.Cells[8, 4]); 
            range.MergeCells = true;
            ac.Cells[6, 5] = "单位";
            range = wksheet.get_Range(wksheet.Cells[6, 5], wksheet.Cells[8, 5]); 
            range.MergeCells = true;
            ac.Cells[6, 6] = "单价";
            range = wksheet.get_Range(wksheet.Cells[6, 6], wksheet.Cells[8, 6]);  
            range.MergeCells = true;
            ac.Cells[6, 7] = "账面记录";
            range = wksheet.get_Range(wksheet.Cells[6, 7], wksheet.Cells[7, 8]);  
            range.MergeCells = true;
            ac.Cells[8, 7] = "数量";
            ac.Cells[8, 8] = "金额";
            ac.Cells[6, 9] = "尚未入账数量";
            range = wksheet.get_Range(wksheet.Cells[6, 9], wksheet.Cells[6, 12]);  
            range.MergeCells = true;
            ac.Cells[7, 9] = "入库";
            range = wksheet.get_Range(wksheet.Cells[7, 9], wksheet.Cells[7, 10]);  
            range.MergeCells = true;
            ac.Cells[7, 11] = "出库";
            range = wksheet.get_Range(wksheet.Cells[7, 11], wksheet.Cells[7, 12]);  
            range.MergeCells = true;
            ac.Cells[8, 9] = "数量";
            ac.Cells[8, 10] = "金额";
            ac.Cells[8, 11] = "数量";
            ac.Cells[8, 12] = "金额";
            ac.Cells[6, 13] = "应结存";
            range = wksheet.get_Range(wksheet.Cells[6, 13], wksheet.Cells[7, 14]); 
            range.MergeCells = true;
            ac.Cells[8, 13] = "数量";
            ac.Cells[8, 14] = "金额";
            ac.Cells[6, 15] = "盘点记录";
            range = wksheet.get_Range(wksheet.Cells[6, 15], wksheet.Cells[7, 16]);  
            range.MergeCells = true;
            ac.Cells[8, 15] = "数量";
            ac.Cells[8, 16] = "金额";
            ac.Cells[6, 17] = "差异记录";
            range = wksheet.get_Range(wksheet.Cells[6, 17], wksheet.Cells[7, 18]);  
            range.MergeCells = true;
            ac.Cells[8, 17] = "数量";
            ac.Cells[8, 18] = "金额";
            ac.Cells[6, 19] = "差异原因";
            range = wksheet.get_Range(wksheet.Cells[6, 19], wksheet.Cells[8, 19]);
            range.MergeCells = true;
            ac.Cells[6, 20] = "备注";
            range = wksheet.get_Range(wksheet.Cells[6, 20], wksheet.Cells[8, 20]); 
            range.MergeCells = true;
            // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                colIndex = 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ac.Cells[rowIndex, 1] = "'" + (rowIndex - 8).ToString();
                    if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                    {
                        try
                        {
                            // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
                            ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
                        }
                        catch (System.FormatException)
                        {
                            ac.Cells[rowIndex, colIndex] = "";
                        }
                    }
                    else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    else
                    {
                        ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                    wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    colIndex++;
                }
                rowIndex++;
            }
            string filename = Server.MapPath("/SM_Data/ExportFile/" + "Summary" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            Response.WriteFile(filename); // 把文件流发送到客户端 
            Response.End();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string code = Request.QueryString["ID"].ToString();
            string sql = "";
            List<string> sqllist = new List<string>();

            sql = "DELETE FROM TBWS_INVENTORYSUMMARY WHERE PD_CODE='" + code + "'";
            sqllist.Add(sql);
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                string MaterialCode = ((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelMaterialCode")).Text;
                float UnitPrice = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelUnitPrice")).Text);
                float NumInAccount = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelNumInAccount")).Text);
                float AmountInAccount = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelAmountInAccount")).Text);
                float NumNotIn = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelNumNotIn")).Text);
                float AmountNotIn = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelAmountNotIn")).Text);
                float NumNotOut = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelNumNotOut")).Text);
                float AmountNotOut = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelAmountNotOut")).Text);
                float NumDueToAccount = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelNumDueToAccount")).Text);
                float AmountDueToAccount = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelAmountDueToAccount")).Text);
                float NumInventory = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelNumInventory")).Text);
                float AmountInventory = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelAmountInventory")).Text);
                float NumDiff = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelNumDiff")).Text);
                float AmountDiff = Convert.ToSingle(((System.Web.UI.WebControls.Label)Repeater1.Items[i].FindControl("LabelAmountDiff")).Text);
                string Reason = ((System.Web.UI.WebControls.TextBox)Repeater1.Items[i].FindControl("TextBoxReason")).Text;
                string Comment = ((System.Web.UI.WebControls.TextBox)Repeater1.Items[i].FindControl("TextBoxComment")).Text;

                sql = "INSERT INTO TBWS_INVENTORYSUMMARY(PD_CODE,PD_MARID," +
                    "PD_UNITPRICE,PD_NUM,PD_AMOUNT,PD_NUMNOTIN,PD_AMOUNTNOTIN," +
                    "PD_NUMNOTOUT,PD_AMOUNTNOTOUT,PD_DUENUM,PD_DUEAMOUNT,PD_REALNUM,PD_REALAMOUNT," +
                    "PD_DIFFNUM,PD_DIFFAMOUNT,PD_REASON,PD_NOTE) VALUES('" + code + "','" + MaterialCode + "','" +
                    UnitPrice + "','" + NumInAccount + "','" + AmountInAccount + "','" + NumNotIn + "','" + AmountNotIn + "','" +
                    NumNotOut + "','" + AmountNotOut + "','" + NumDueToAccount + "','" + AmountDueToAccount + "','" + NumInventory + "','" +
                    AmountInventory + "','" + NumDiff + "','" + AmountDiff + "','" + Reason + "','" + Comment + "')";
                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            LabelMessage.Text = "保存成功";
        }

    }
}
