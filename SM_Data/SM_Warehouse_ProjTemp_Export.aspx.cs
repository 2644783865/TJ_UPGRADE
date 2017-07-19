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
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.IO;
using System.Data;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_ProjTemp_Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string GetCondition()
        {
            string condition = "";

            string type = "9";


            string sql = "select StrCondition from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='" + type + "'";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (dt.Rows.Count > 0)
            {
                condition = dt.Rows[0]["StrCondition"].ToString();
            }
            return condition.Replace("^", "'");
        }


        protected void Confirm_Click(object sender, EventArgs e)
        {
            string sql = "SELECT ";

            string attribute = "";

            string condition = GetCondition();


            string order = "";
            if (CheckBoxList1.Items[0].Selected == true)//单号
            {
                attribute += CheckBoxList1.Items[0].Value;
            }
            if (CheckBoxList2.Items[0].Selected == true)//到计划跟踪号
            {
                attribute += CheckBoxList2.Items[0].Value;
            }
            if (CheckBoxList3.Items[0].Selected == true)//调整数量
            {
                attribute += CheckBoxList3.Items[0].Value;
            }
            if (CheckBoxList4.Items[0].Selected == true)//调整张（支）数
            {
                attribute += CheckBoxList4.Items[0].Value;
            }
            if (CheckBoxList5.Items[0].Selected == true)//从计划跟踪号
            {
                attribute += CheckBoxList5.Items[0].Value;
            }
            if (CheckBoxList6.Items[0].Selected == true)//物料编码
            {
                attribute += CheckBoxList6.Items[0].Value;
            }
            if (CheckBoxList7.Items[0].Selected == true)//物料名称
            {
                attribute += CheckBoxList7.Items[0].Value;
            }
            if (CheckBoxList8.Items[0].Selected == true)//规格型号
            {
                attribute += CheckBoxList8.Items[0].Value;
            }
            if (CheckBoxList9.Items[0].Selected == true)//材质
            {
                attribute += CheckBoxList9.Items[0].Value;
            }
            if (CheckBoxList10.Items[0].Selected == true)//项目名称
            {
                attribute += CheckBoxList10.Items[0].Value;
            }
          
            if (CheckBoxList11.Items[0].Selected == true)//是否定尺
            {
                attribute += CheckBoxList11.Items[0].Value;
            }
            if (CheckBoxList12.Items[0].Selected == true)//长
            {
                attribute += CheckBoxList12.Items[0].Value;
            }

            if (CheckBoxList13.Items[0].Selected == true)//宽
            {
                attribute += CheckBoxList13.Items[0].Value;
            }
            if (CheckBoxList14.Items[0].Selected == true)//批号
            {
                attribute += CheckBoxList14.Items[0].Value;
            }
            if (CheckBoxList15.Items[0].Selected == true)//单位
            {
                attribute += CheckBoxList15.Items[0].Value;
            }
            if (CheckBoxList16.Items[0].Selected == true)//可调整数量
            {
                attribute += CheckBoxList16.Items[0].Value;
            }
            if (CheckBoxList17.Items[0].Selected == true)//可调整张（支）数
            {
                attribute += CheckBoxList17.Items[0].Value;
            }
            if (CheckBoxList18.Items[0].Selected == true)//仓库
            {
                attribute += CheckBoxList18.Items[0].Value;
            }
            if (CheckBoxList19.Items[0].Selected == true)//仓位
            {
                attribute += CheckBoxList19.Items[0].Value;
            }
            if (CheckBoxList20.Items[0].Selected == true)//制单人
            {
                attribute += CheckBoxList20.Items[0].Value;
            }
            if (CheckBoxList21.Items[0].Selected == true)//制单日期
            {
                attribute += CheckBoxList21.Items[0].Value;
            }
            if (CheckBoxList22.Items[0].Selected == true)//技术员为审核人
            {
                attribute += CheckBoxList22.Items[0].Value;
            }
            if (CheckBoxList23.Items[0].Selected == true)//技术员审核日期
            {
                attribute += CheckBoxList23.Items[0].Value;
            }
            if (CheckBoxList24.Items[0].Selected == true)//生产制号
            {
                attribute += CheckBoxList24.Items[0].Value;
            }
            if (CheckBoxList25.Items[0].Selected == true)//工程名称
            {
                attribute += CheckBoxList25.Items[0].Value;
            }
            if (CheckBoxList26.Items[0].Selected == true)//国标
            {
                attribute += CheckBoxList26.Items[0].Value;
            }
            if (CheckBoxList27.Items[0].Selected == true)//审批日期
            {
                attribute += CheckBoxList27.Items[0].Value;
            }
            if (CheckBoxList28.Items[0].Selected == true)//审批意见
            {
                attribute += CheckBoxList28.Items[0].Value;
            }
            if (CheckBoxList29.Items[0].Selected == true)//剩余原因
            {
                attribute += CheckBoxList29.Items[0].Value;
            }


            if (attribute != "")
            {
                attribute = attribute.Substring(0, attribute.Length - 1);
                sql += attribute;
                sql += " FROM View_SM_PROJTEMP ";
            }

            //if (TextBox1.Text != "")
            //{
            //    condition += "AND WG_CODE LIKE '%" + TextBox1.Text + "%' ";
            //}
            //if (TextBox2.Text != "")
            //{
            //    condition += "AND SupplierName LIKE '%" + TextBox2.Text + "%' ";
            //}
            //if (TextBox3.Text != "")
            //{
            //    condition += "AND WG_DATE LIKE '%" + TextBox3.Text + "%' ";
            //}
            //if (TextBox4.Text != "")
            //{
            //    condition += "AND DepName LIKE '%" + TextBox4.Text + "%' ";
            //}
            //if (TextBox5.Text != "")
            //{
            //    condition += "AND ClerkName LIKE '%" + TextBox5.Text + "%' ";
            //}
            //if (TextBox6.Text != "")
            //{
            //    condition += "AND DocName LIKE '%" + TextBox6.Text + "%' ";
            //}
            //if (TextBox7.Text != "")
            //{
            //    condition += "AND VerfierName LIKE '%" + TextBox7.Text + "%' ";
            //}
            //if (TextBox8.Text != "")
            //{
            //    condition += "AND WG_MARID LIKE '%" + TextBox8.Text + "%' ";
            //}
            //if (TextBox9.Text != "")
            //{
            //    condition += "AND MNAME LIKE '%" + TextBox9.Text + "%' ";
            //}
            //if (TextBox10.Text != "")
            //{
            //    condition += "AND CAIZHI LIKE '%" + TextBox10.Text + "%' ";
            //}
            //if (TextBox11.Text != "")
            //{
            //    condition += "AND GB LIKE '%" + TextBox11.Text + "%' ";
            //}
            //if (TextBox12.Text != "")
            //{
            //    condition += "AND GUIGE LIKE '%" + TextBox12.Text + "%' ";
            //}
            //if (TextBox13.Text != "")
            //{
            //    condition += "AND WG_FIXEDSIZE LIKE '%" + TextBox13.Text + "%' ";
            //}
            //if (TextBox14.Text != "")
            //{
            //    condition += "AND WG_LENGTH LIKE '%" + TextBox14.Text + "%' ";
            //}
            //if (TextBox15.Text != "")
            //{
            //    condition += "AND WG_WIDTH LIKE '%" + TextBox15.Text + "%' ";
            //}
            //if (TextBox16.Text != "")
            //{
            //    condition += "AND WG_LOTNUM LIKE '%" + TextBox16.Text + "%' ";
            //}
            //if (TextBox17.Text != "")
            //{
            //    condition += "AND CGDW LIKE '%" + TextBox17.Text + "%' ";
            //}
            //if (TextBox18.Text != "")
            //{
            //    condition += "AND WG_RSNUM LIKE '%" + TextBox18.Text + "%' ";
            //}
            //if (TextBox19.Text != "")
            //{
            //    condition += "AND WG_RSFZNUM LIKE '%" + TextBox19.Text + "%' ";
            //}
            //if (TextBox20.Text != "")
            //{
            //    condition += "AND WG_UPRICE LIKE '%" + TextBox20.Text + "%' ";
            //}
            //if (TextBox21.Text != "")
            //{
            //    condition += "AND WG_AMOUNT LIKE '%" + TextBox21.Text + "%' ";
            //}
            //if (TextBox22.Text != "")
            //{
            //    condition += "AND WS_NAME LIKE '%" + TextBox22.Text + "%' ";
            //}
            //if (TextBox23.Text != "")
            //{
            //    condition += "AND WL_NAME LIKE '%" + TextBox23.Text + "%' ";
            //}
            //if (TextBox24.Text != "")
            //{
            //    condition += "AND WG_PTCODE LIKE '%" + TextBox24.Text + "%' ";
            //}
            //if (TextBox25.Text != "")
            //{
            //    condition += "AND WG_ORDERID LIKE '%" + TextBox25.Text + "%' ";
            //}
            //if (TextBox26.Text != "")
            //{
            //    condition += "AND WG_VERIFYDATE LIKE '%" + TextBox26.Text + "%' ";
            //}
            //if (TextBox27.Text != "")
            //{
            //    condition += "AND WG_NOTE LIKE '%" + TextBox27.Text + "%' ";
            //}
            //if (TextBox28.Text != "")
            //{
            //    condition += "AND WG_HDBH LIKE '%" + TextBox28.Text + "%' ";
            //}
            //if (TextBox29.Text != "")
            //{
            //    condition += "AND WG_ABSTRACT LIKE '%" + TextBox28.Text + "%' ";
            //}

            if (condition != "")
            {
                //condition = condition.Substring(3, condition.Length - 3);
                //condition = condition.Substring(0, condition.Length - 1);
                sql += " WHERE ";
                sql += condition;
            }

            if (RadioButtonList1.SelectedValue != string.Empty)
            {
                order += RadioButtonList1.SelectedValue;
            }
            if (RadioButtonList2.SelectedValue != string.Empty)
            {
                order += RadioButtonList2.SelectedValue;
            }
            if (RadioButtonList3.SelectedValue != string.Empty)
            {
                order += RadioButtonList3.SelectedValue;
            }
            if (RadioButtonList4.SelectedValue != string.Empty)
            {
                order += RadioButtonList4.SelectedValue;
            }
            if (RadioButtonList5.SelectedValue != string.Empty)
            {
                order += RadioButtonList5.SelectedValue;
            }
            if (RadioButtonList6.SelectedValue != string.Empty)
            {
                order += RadioButtonList6.SelectedValue;
            }
            if (RadioButtonList7.SelectedValue != string.Empty)
            {
                order += RadioButtonList7.SelectedValue;
            }
            if (RadioButtonList8.SelectedValue != string.Empty)
            {
                order += RadioButtonList8.SelectedValue;
            }
            if (RadioButtonList9.SelectedValue != string.Empty)
            {
                order += RadioButtonList9.SelectedValue;
            }
            if (RadioButtonList10.SelectedValue != string.Empty)
            {
                order += RadioButtonList10.SelectedValue;
            }
            if (RadioButtonList11.SelectedValue != string.Empty)
            {
                order += RadioButtonList11.SelectedValue;
            }
            if (RadioButtonList12.SelectedValue != string.Empty)
            {
                order += RadioButtonList12.SelectedValue;
            }
            if (RadioButtonList13.SelectedValue != string.Empty)
            {
                order += RadioButtonList13.SelectedValue;
            }
            if (RadioButtonList14.SelectedValue != string.Empty)
            {
                order += RadioButtonList14.SelectedValue;
            }
            if (RadioButtonList15.SelectedValue != string.Empty)
            {
                order += RadioButtonList15.SelectedValue;
            }
            if (RadioButtonList16.SelectedValue != string.Empty)
            {
                order += RadioButtonList16.SelectedValue;
            }
            if (RadioButtonList17.SelectedValue != string.Empty)
            {
                order += RadioButtonList17.SelectedValue;
            }
            if (RadioButtonList18.SelectedValue != string.Empty)
            {
                order += RadioButtonList18.SelectedValue;
            }
            if (RadioButtonList19.SelectedValue != string.Empty)
            {
                order += RadioButtonList19.SelectedValue;
            }
            if (RadioButtonList20.SelectedValue != string.Empty)
            {
                order += RadioButtonList20.SelectedValue;
            }
            if (RadioButtonList21.SelectedValue != string.Empty)
            {
                order += RadioButtonList21.SelectedValue;
            }
            if (RadioButtonList22.SelectedValue != string.Empty)
            {
                order += RadioButtonList22.SelectedValue;
            }
            if (RadioButtonList23.SelectedValue != string.Empty)
            {
                order += RadioButtonList23.SelectedValue;
            }
            if (RadioButtonList24.SelectedValue != string.Empty)
            {
                order += RadioButtonList24.SelectedValue;
            }
            if (RadioButtonList25.SelectedValue != string.Empty)
            {
                order += RadioButtonList25.SelectedValue;
            }
            if (RadioButtonList26.SelectedValue != string.Empty)
            {
                order += RadioButtonList26.SelectedValue;
            }
            if (RadioButtonList27.SelectedValue != string.Empty)
            {
                order += RadioButtonList27.SelectedValue;
            }
            if (RadioButtonList28.SelectedValue != string.Empty)
            {
                order += RadioButtonList28.SelectedValue;
            }
            if (RadioButtonList29.SelectedValue != string.Empty)
            {
                order += RadioButtonList29.SelectedValue;
            }

            if (order != "")
            {
                order = order.Substring(0, order.Length - 1);
                sql += " ORDER BY ";
                sql += order;
            }

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







            //----------------------------------------------------------------------

            //设置表头
            //ac.Cells[1, 1] = "序号";
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    ac.Cells[1, i + 2] = dt.Columns[i].ColumnName;
            //}

            //int rowIndex = 2;   // 行  
            //int colIndex = 2;   // 列 

            //// 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    ac.Cells[rowIndex, 1] = "'" + (rowIndex - 1).ToString();
            //    colIndex = 2;
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        if (dt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
            //        {
            //            try
            //            {
            //                // 如果单元格前不加"'"，有些字段会自动采用科学计数法  
            //                ac.Cells[rowIndex, colIndex] = "'" + (Convert.ToDateTime(dt.Rows[i][dt.Columns[j].ColumnName].ToString())).ToString("yyyy-MM-dd");
            //            }
            //            catch (System.FormatException)
            //            {
            //                ac.Cells[rowIndex, colIndex] = "";
            //            }
            //        }
            //        else if (dt.Columns[j].DataType == System.Type.GetType("System.String"))
            //        {
            //            ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
            //        }
            //        else
            //        {
            //            ac.Cells[rowIndex, colIndex] = "'" + dt.Rows[i][dt.Columns[j].ColumnName].ToString();
            //        }
            //        wksheet.get_Range(ac.Cells[rowIndex, colIndex], ac.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //        colIndex++;
            //    }
            //    rowIndex++;
            //}

            //----------------------------------------------------------------------

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "项目结转" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
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
