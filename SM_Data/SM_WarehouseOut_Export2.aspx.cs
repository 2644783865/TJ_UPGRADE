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
    public partial class SM_WarehouseOut_Export2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string GetCondition()
        {
            string condition = "";

            string sql = "select StrCondition from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='4'";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (dt.Rows.Count > 0)
            {
                condition = dt.Rows[0]["StrCondition"].ToString();
            }
            return condition.Replace("*", "'");
        }
        protected void Confirm_Click(object sender, EventArgs e)
        {
            string sql = "SELECT ";
            string attribute = "";
            string condition = GetCondition();
            string order = "";
            if (CheckBoxList1.Items[0].Selected == true)//委外出库单号
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
            if (CheckBoxList19.Items[0].Selected == true)//实发张（支）
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
            if (CheckBoxList22.Items[0].Selected == true)//发料仓库
            {
                attribute += CheckBoxList22.Items[0].Value;
            }
            if (CheckBoxList23.Items[0].Selected == true)//发料仓位
            {
                attribute += CheckBoxList23.Items[0].Value;
            }
            if (CheckBoxList2.Items[0].Selected == true)//加工单位
            {
                attribute += CheckBoxList2.Items[0].Value;
            }
            if (CheckBoxList4.Items[0].Selected == true)//委外类型
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
            if (attribute != "")
            {
                attribute += "right(TSAID,charindex('-',REVERSE(TSAID))-1) AS 生产制号,";
                attribute = attribute.Substring(0, attribute.Length - 1);
                sql += attribute;
                sql += " FROM View_SM_OUTWW ";
            }

            //if (TextBox1.Text != "")
            //{
            //    condition += "AND Code LIKE '%" + TextBox1.Text + "%' ";
            //}
            //if (TextBox2.Text != "")
            //{
            //    condition += "AND Company LIKE '%" + TextBox2.Text + "%' ";
            //}
            //if (TextBox3.Text != "")
            //{
            //    condition += "AND Date LIKE '%" + TextBox3.Text + "%' ";
            //}
            //if (TextBox4.Text != "")
            //{
            //    condition += "AND ProcessType LIKE '%" + TextBox4.Text + "%' ";
            //}
            //if (TextBox5.Text != "")
            //{
            //    condition += "AND Doc LIKE '%" + TextBox5.Text + "%' ";
            //}
            //if (TextBox6.Text != "")
            //{
            //    condition += "AND Verifier LIKE '%" + TextBox6.Text + "%' ";
            //}
            //if (TextBox7.Text != "")
            //{
            //    condition += "AND VerifierDate LIKE '%" + TextBox7.Text + "%' ";
            //}
            //if (TextBox8.Text != "")
            //{
            //    condition += "AND MaterialCode LIKE '%" + TextBox8.Text + "%' ";
            //}
            //if (TextBox9.Text != "")
            //{
            //    condition += "AND MaterialName LIKE '%" + TextBox9.Text + "%' ";
            //}
            //if (TextBox10.Text != "")
            //{
            //    condition += "AND Attribute LIKE '%" + TextBox10.Text + "%' ";
            //}
            //if (TextBox11.Text != "")
            //{
            //    condition += "AND GB LIKE '%" + TextBox11.Text + "%' ";
            //}
            //if (TextBox12.Text != "")
            //{
            //    condition += "AND Standard LIKE '%" + TextBox12.Text + "%' ";
            //}
            //if (TextBox13.Text != "")
            //{
            //    condition += "AND Fixed LIKE '%" + TextBox13.Text + "%' ";
            //}
            //if (TextBox14.Text != "")
            //{
            //    condition += "AND Length LIKE '%" + TextBox14.Text + "%' ";
            //}
            //if (TextBox15.Text != "")
            //{
            //    condition += "AND Width LIKE '%" + TextBox15.Text + "%' ";
            //}
            //if (TextBox16.Text != "")
            //{
            //    condition += "AND LotNumber LIKE '%" + TextBox16.Text + "%' ";
            //}
            //if (TextBox17.Text != "")
            //{
            //    condition += "AND Unit LIKE '%" + TextBox17.Text + "%' ";
            //}
            //if (TextBox18.Text != "")
            //{
            //    condition += "AND RealNumber LIKE '%" + TextBox18.Text + "%' ";
            //}
            //if (TextBox19.Text != "")
            //{
            //    condition += "AND RealSupportNumber LIKE '%" + TextBox19.Text + "%' ";
            //}
            //if (TextBox20.Text != "")
            //{
            //    condition += "AND UnitPrice LIKE '%" + TextBox20.Text + "%' ";
            //}
            //if (TextBox21.Text != "")
            //{
            //    condition += "AND Amount LIKE '%" + TextBox21.Text + "%' ";
            //}
            //if (TextBox22.Text != "")
            //{
            //    condition += "AND Warehouse LIKE '%" + TextBox22.Text + "' ";
            //}
            //if (TextBox23.Text != "")
            //{
            //    condition += "AND Location LIKE '%" + TextBox23.Text + "%' ";
            //}
            //if (TextBox24.Text != "")
            //{
            //    condition += "AND PTC LIKE '%" + TextBox24.Text + "%' ";
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
            if (order != "")
            {
                order = order.Substring(0, order.Length - 1);
                sql += " ORDER BY ";
                sql += order;
            }

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

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

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "委外出库" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  

            if (System.IO.File.Exists(filename))
            {
                DownloadFile.Send(Context, filename);
            }
        }
    }
}
