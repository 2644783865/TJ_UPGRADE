using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseOut_Export4 : System.Web.UI.Page
    {
        private string GetCondition()
        {
            string condition = "";

            string sql = "select StrCondition from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='10'";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (dt.Rows.Count > 0)
            {
                condition = dt.Rows[0]["StrCondition"].ToString();
            }
            return condition.Replace("*", "'");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (RadioButtonListStyle.SelectedValue == "0")
            {
                string sql = "SELECT ";

                string attribute = "";

                string condition = GetCondition();

                string order = "";

                if (CheckBoxList1.Items[0].Selected == true)//出库单号
                {
                    attribute += CheckBoxList1.Items[0].Value;
                }
                if (CheckBoxList24.Items[0].Selected == true)//计划跟踪号
                {
                    attribute += CheckBoxList24.Items[0].Value;
                }
                if (CheckBoxList8.Items[0].Selected == true)//物料编码
                {
                    attribute += CheckBoxList8.Items[0].Value;
                }
                if (CheckBoxList9.Items[0].Selected == true)//物料名称
                {
                    attribute += CheckBoxList9.Items[0].Value;
                }
                if (CheckBoxList12.Items[0].Selected == true)//规格型号
                {
                    attribute += CheckBoxList12.Items[0].Value;
                }
                if (CheckBoxList10.Items[0].Selected == true)//材质
                {
                    attribute += CheckBoxList10.Items[0].Value;
                }
                if (CheckBoxList11.Items[0].Selected == true)//国标
                {
                    attribute += CheckBoxList11.Items[0].Value;
                }
                if (CheckBoxList13.Items[0].Selected == true)//是否定尺
                {
                    attribute += CheckBoxList13.Items[0].Value;
                }
                if (CheckBoxList14.Items[0].Selected == true)//长
                {
                    attribute += CheckBoxList14.Items[0].Value;
                }
                if (CheckBoxList15.Items[0].Selected == true)//宽
                {
                    attribute += CheckBoxList15.Items[0].Value;
                }
                if (CheckBoxList16.Items[0].Selected == true)//批号
                {
                    attribute += CheckBoxList16.Items[0].Value;
                }
                if (CheckBoxList17.Items[0].Selected == true)//单位
                {
                    attribute += CheckBoxList17.Items[0].Value;
                }
                if (CheckBoxList18.Items[0].Selected == true)//实发数量
                {
                    attribute += CheckBoxList18.Items[0].Value;
                }
                if (CheckBoxList19.Items[0].Selected == true)//实发长支
                {
                    attribute += CheckBoxList19.Items[0].Value;
                }
                if (CheckBoxList20.Items[0].Selected == true)//单价
                {
                    attribute += CheckBoxList20.Items[0].Value;
                }
                if (CheckBoxList21.Items[0].Selected == true)//金额
                {
                    attribute += CheckBoxList21.Items[0].Value;
                }
                if (CheckBoxList28.Items[0].Selected == true)//含税单价
                {
                    attribute += CheckBoxList28.Items[0].Value;
                }
                if (CheckBoxList29.Items[0].Selected == true)//含税金额
                {
                    attribute += CheckBoxList29.Items[0].Value;
                }

                if (CheckBoxList22.Items[0].Selected == true)//发料仓库
                {
                    attribute += CheckBoxList22.Items[0].Value;
                }
                if (CheckBoxList23.Items[0].Selected == true)//发料仓位
                {
                    attribute += CheckBoxList23.Items[0].Value;
                }
                if (CheckBoxList2.Items[0].Selected == true)//领料部门
                {
                    attribute += CheckBoxList2.Items[0].Value;
                }

                if (CheckBoxList25.Items[0].Selected == true)//制作班组
                {
                    attribute += CheckBoxList25.Items[0].Value;
                }
                if (CheckBoxList26.Items[0].Selected == true)//生产制号
                {
                    attribute += CheckBoxList26.Items[0].Value;
                }

                if (CheckBoxList30.Items[0].Selected == true)//中文生产制号
                {
                    attribute += CheckBoxList30.Items[0].Value;
                }

                if (CheckBoxList27.Items[0].Selected == true)//子项名称
                {
                    attribute += CheckBoxList27.Items[0].Value;
                }
                if (CheckBoxList4.Items[0].Selected == true)//发料人
                {
                    attribute += CheckBoxList4.Items[0].Value;
                }
                if (CheckBoxList5.Items[0].Selected == true)//制单人
                {
                    attribute += CheckBoxList5.Items[0].Value;
                }
                if (CheckBoxList3.Items[0].Selected == true)//制单日期
                {
                    attribute += CheckBoxList3.Items[0].Value;
                }
                if (CheckBoxList6.Items[0].Selected == true)//审核人
                {
                    attribute += CheckBoxList6.Items[0].Value;
                }
                if (CheckBoxList7.Items[0].Selected == true)//审核日期
                {
                    attribute += CheckBoxList7.Items[0].Value;
                }
                if (CheckBoxList31.Items[0].Selected == true)//备注
                {
                    attribute += CheckBoxList31.Items[0].Value;
                }
                if (attribute != "")
                {
                    attribute = attribute.Substring(0, attribute.Length - 1);
                    sql += attribute;
                    sql += " FROM View_SM_OUT ";
                }

                if (condition != "")
                {
                    sql += " WHERE ";
                    sql += condition;
                }

                if (RadioButtonList1.SelectedValue != null)
                {
                    order += RadioButtonList1.SelectedValue;
                }
                if (RadioButtonList2.SelectedValue != null)
                {
                    order += RadioButtonList2.SelectedValue;
                }
                if (RadioButtonList3.SelectedValue != null)
                {
                    order += RadioButtonList3.SelectedValue;
                }
                if (RadioButtonList4.SelectedValue != null)
                {
                    order += RadioButtonList4.SelectedValue;
                }
                if (RadioButtonList5.SelectedValue != null)
                {
                    order += RadioButtonList5.SelectedValue;
                }
                if (RadioButtonList6.SelectedValue != null)
                {
                    order += RadioButtonList6.SelectedValue;
                }
                if (RadioButtonList7.SelectedValue != null)
                {
                    order += RadioButtonList7.SelectedValue;
                }
                if (RadioButtonList8.SelectedValue != null)
                {
                    order += RadioButtonList8.SelectedValue;
                }
                if (RadioButtonList9.SelectedValue != null)
                {
                    order += RadioButtonList9.SelectedValue;
                }
                if (RadioButtonList10.SelectedValue != null)
                {
                    order += RadioButtonList10.SelectedValue;
                }
                if (RadioButtonList11.SelectedValue != null)
                {
                    order += RadioButtonList11.SelectedValue;
                }
                if (RadioButtonList12.SelectedValue != null)
                {
                    order += RadioButtonList12.SelectedValue;
                }
                if (RadioButtonList13.SelectedValue != null)
                {
                    order += RadioButtonList13.SelectedValue;
                }
                if (RadioButtonList14.SelectedValue != null)
                {
                    order += RadioButtonList14.SelectedValue;
                }
                if (RadioButtonList15.SelectedValue != null)
                {
                    order += RadioButtonList15.SelectedValue;
                }
                if (RadioButtonList16.SelectedValue != null)
                {
                    order += RadioButtonList16.SelectedValue;
                }
                if (RadioButtonList17.SelectedValue != null)
                {
                    order += RadioButtonList17.SelectedValue;
                }
                if (RadioButtonList18.SelectedValue != null)
                {
                    order += RadioButtonList18.SelectedValue;
                }
                if (RadioButtonList19.SelectedValue != null)
                {
                    order += RadioButtonList19.SelectedValue;
                }
                if (RadioButtonList20.SelectedValue != null)
                {
                    order += RadioButtonList20.SelectedValue;
                }
                if (RadioButtonList21.SelectedValue != null)
                {
                    order += RadioButtonList21.SelectedValue;
                }
                if (RadioButtonList22.SelectedValue != null)
                {
                    order += RadioButtonList22.SelectedValue;
                }
                if (RadioButtonList23.SelectedValue != null)
                {
                    order += RadioButtonList23.SelectedValue;
                }
                if (RadioButtonList24.SelectedValue != null)
                {
                    order += RadioButtonList24.SelectedValue;
                }
                if (RadioButtonList25.SelectedValue != null)
                {
                    order += RadioButtonList25.SelectedValue;
                }

                if (RadioButtonList26.SelectedValue != null)
                {
                    order += RadioButtonList26.SelectedValue;
                }
                if (RadioButtonList27.SelectedValue != null)
                {
                    order += RadioButtonList27.SelectedValue;
                }
                if (RadioButtonList28.SelectedValue != null)
                {
                    order += RadioButtonList28.SelectedValue;
                }
                if (RadioButtonList29.SelectedValue != null)
                {
                    order += RadioButtonList29.SelectedValue;
                }
                if (RadioButtonList31.SelectedValue != null)
                {
                    order += RadioButtonList31.SelectedValue;
                }
                if (order != "")
                {
                    order = order.Substring(0, order.Length - 1);
                    sql += " ORDER BY ";
                    sql += order;
                }


                ExportExcel(sql);
            }
            //else if (RadioButtonListStyle.SelectedValue == "1")
            //{
            //    string strsql = "select MaterialName AS 物料名称,sum(RealNumber) AS 数量,sum(Amount) AS 金额 from View_SM_OUT where " + GetCondition() + "  GROUP BY MaterialName";

            //    ExportExcel(strsql);
            //}
            //else if (RadioButtonListStyle.SelectedValue == "2")
            //{
            //    string strsql = "select TSAID AS 生产制号,MaterialName AS 物料名称,sum(RealNumber) AS 数量,sum(Amount) AS 金额 from View_SM_OUT where " + GetCondition() + "  GROUP BY TSAID,MaterialName order by TSAID";

            //    ExportExcel(strsql);
            //}
            //else if (RadioButtonListStyle.SelectedValue == "3")
            //{
            //    string sql = "SELECT ";

            //    string attribute = "";

            //    string condition = GetCondition();

            //    string order = "";

            //    if (CheckBoxList1.Items[0].Selected == true)//出库单号
            //    {
            //        attribute += CheckBoxList1.Items[0].Value;
            //    }
            //    if (CheckBoxList24.Items[0].Selected == true)//计划跟踪号
            //    {
            //        attribute += CheckBoxList24.Items[0].Value;
            //    }
            //    if (CheckBoxList8.Items[0].Selected == true)//物料编码
            //    {
            //        attribute += CheckBoxList8.Items[0].Value;
            //    }
            //    if (CheckBoxList9.Items[0].Selected == true)//物料名称
            //    {
            //        attribute += CheckBoxList9.Items[0].Value;
            //    }
            //    if (CheckBoxList12.Items[0].Selected == true)//规格型号
            //    {
            //        attribute += CheckBoxList12.Items[0].Value;
            //    }
            //    if (CheckBoxList10.Items[0].Selected == true)//材质
            //    {
            //        attribute += CheckBoxList10.Items[0].Value;
            //    }
            //    if (CheckBoxList11.Items[0].Selected == true)//国标
            //    {
            //        attribute += CheckBoxList11.Items[0].Value;
            //    }
            //    if (CheckBoxList13.Items[0].Selected == true)//是否定尺
            //    {
            //        attribute += CheckBoxList13.Items[0].Value;
            //    }
            //    if (CheckBoxList14.Items[0].Selected == true)//长
            //    {
            //        attribute += CheckBoxList14.Items[0].Value;
            //    }
            //    if (CheckBoxList15.Items[0].Selected == true)//宽
            //    {
            //        attribute += CheckBoxList15.Items[0].Value;
            //    }
            //    if (CheckBoxList16.Items[0].Selected == true)//批号
            //    {
            //        attribute += CheckBoxList16.Items[0].Value;
            //    }
            //    if (CheckBoxList17.Items[0].Selected == true)//单位
            //    {
            //        attribute += CheckBoxList17.Items[0].Value;
            //    }
            //    if (CheckBoxList18.Items[0].Selected == true)//实发数量
            //    {
            //        attribute += CheckBoxList18.Items[0].Value;
            //    }
            //    if (CheckBoxList19.Items[0].Selected == true)//实发长支
            //    {
            //        attribute += CheckBoxList19.Items[0].Value;
            //    }
            //    if (CheckBoxList20.Items[0].Selected == true)//单价
            //    {
            //        attribute += CheckBoxList20.Items[0].Value;
            //    }
            //    if (CheckBoxList21.Items[0].Selected == true)//金额
            //    {
            //        attribute += CheckBoxList21.Items[0].Value;
            //    }
            //    if (CheckBoxList20.Items[0].Selected == true)//含税单价
            //    {
            //        attribute += CheckBoxList28.Items[0].Value;
            //    }
            //    if (CheckBoxList21.Items[0].Selected == true)//含税金额
            //    {
            //        attribute += CheckBoxList29.Items[0].Value;
            //    }

            //    if (CheckBoxList22.Items[0].Selected == true)//发料仓库
            //    {
            //        attribute += CheckBoxList22.Items[0].Value;
            //    }
            //    if (CheckBoxList23.Items[0].Selected == true)//发料仓位
            //    {
            //        attribute += CheckBoxList23.Items[0].Value;
            //    }
            //    if (CheckBoxList2.Items[0].Selected == true)//领料部门
            //    {
            //        attribute += CheckBoxList2.Items[0].Value;
            //    }

            //    if (CheckBoxList25.Items[0].Selected == true)//制作班组
            //    {
            //        attribute += CheckBoxList25.Items[0].Value;
            //    }
            //    if (CheckBoxList26.Items[0].Selected == true)//生产制号
            //    {
            //        attribute += CheckBoxList26.Items[0].Value;
            //    }

            //    if (CheckBoxList30.Items[0].Selected == true)//中文生产制号
            //    {
            //        attribute += CheckBoxList30.Items[0].Value;
            //    }

            //    if (CheckBoxList27.Items[0].Selected == true)//子项名称
            //    {
            //        attribute += CheckBoxList27.Items[0].Value;
            //    }
            //    if (CheckBoxList4.Items[0].Selected == true)//发料人
            //    {
            //        attribute += CheckBoxList4.Items[0].Value;
            //    }
            //    if (CheckBoxList5.Items[0].Selected == true)//制单人
            //    {
            //        attribute += CheckBoxList5.Items[0].Value;
            //    }
            //    if (CheckBoxList3.Items[0].Selected == true)//制单日期
            //    {
            //        attribute += CheckBoxList3.Items[0].Value;
            //    }
            //    if (CheckBoxList6.Items[0].Selected == true)//审核人
            //    {
            //        attribute += CheckBoxList6.Items[0].Value;
            //    }
            //    if (CheckBoxList7.Items[0].Selected == true)//审核日期
            //    {
            //        attribute += CheckBoxList7.Items[0].Value;
            //    }
            //    if (attribute != "")
            //    {
            //        attribute = attribute.Substring(0, attribute.Length - 1);
            //        sql += attribute;
            //        sql += " FROM View_SM_OUT ";
            //    }

            //    if (condition != "")
            //    {
            //        sql += " WHERE ";
            //        sql += condition;
            //    }


            //    //按照班组，年月排序

            //    sql += " ORDER BY Dep asc, ApprovedDate asc";




            //    ExportExcel1(sql);
            //}
            //else if (RadioButtonListStyle.SelectedValue == "4")
            //{
            //    string strsql = "select TSAID AS 生产制号,MaterialCode AS 物料代码,MaterialName as 物料名称,Standard as 规格,GB as 国标,Attribute as 材质,sum(RealNumber) AS 数量,sum(Amount) AS 金额 from View_SM_OUT where " + GetCondition() + "  GROUP BY TSAID,MaterialCode,MaterialName,Standard,GB,Attribute order by TSAID";
            //    ExportExcel(strsql);
            //}
        }

        private void ExportExcel1(string strsql)
        {
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    if (dt.Rows[i]["领料部门"].ToString() != dt.Rows[i - 1]["领料部门"].ToString())
                    {

                        DataRow newrow = dt.NewRow();

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {

                            newrow[j] = "";
                        }

                        dt.Rows.InsertAt(newrow, i);

                        i = i + 1;
                    }
                }
            }



            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

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

                    dataArray[i, j + 1] = dt.Rows[i][j];

                }
            }

            //设置表头
            wksheet.Cells[1, 1] = "序号";

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                wksheet.Cells[1, i + 2] = dt.Columns[i].ColumnName;

            }

            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).Value2 = dataArray;

            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "其他出库" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            if (File.Exists(filename))
            {
                DownloadFile.Send(Context, filename);
            }
        }




        private void ExportExcel(string strsql)
        {
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);

            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

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

                    dataArray[i, j + 1] = dt.Rows[i][j];

                }
            }

            //设置表头
            wksheet.Cells[1, 1] = "序号";

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                wksheet.Cells[1, i + 2] = dt.Columns[i].ColumnName;

                //实际发料张数或支数
                if (dt.Columns[i].ColumnName == "实际发料重量" || dt.Columns[i].ColumnName == "金额" || dt.Columns[i].ColumnName == "含税金额" || dt.Columns[i].ColumnName == "实际发料张数或支数")
                {
                    wksheet.get_Range(getExcelColumnLabel(i + 2) + "1", wksheet.Cells[1 + rowCount, i + 2]).NumberFormatLocal = "G/通用格式";
                }
            }


            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).Value2 = dataArray;

            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "其他出库" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  
            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  
            if (File.Exists(filename))
            {
                DownloadFile.Send(Context, filename);
            }
        }

        //将EXCEL列数转换为列字母
        private string getExcelColumnLabel(int index)
        {
            String rs = "";

            do
            {
                index--;
                rs = ((char)(index % 26 + (int)'A')) + rs;
                index = (int)((index - index % 26) / 26);
            } while (index > 0);

            return rs;
        }
    }
}