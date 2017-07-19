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
using System.Runtime.InteropServices;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;


namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_Warn_Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            string sql = "SELECT ";
            string attribute = "";
            string condition = "";
            string order = "";
            
            if (CheckBoxList1.Items[0].Selected == true)//物料编码
            {
                attribute += CheckBoxList1.Items[0].Value;
            }
            if (CheckBoxList2.Items[0].Selected == true)//物料名称
            {
                attribute += CheckBoxList2.Items[0].Value;
            }
            if (CheckBoxList3.Items[0].Selected == true)//规格型号
            {
                attribute += CheckBoxList3.Items[0].Value;
            }
            if (CheckBoxList4.Items[0].Selected == true)//材质
            {
                attribute += CheckBoxList4.Items[0].Value;
            }
            if (CheckBoxList5.Items[0].Selected == true)//国标
            {
                attribute += CheckBoxList5.Items[0].Value;
            }
            if (CheckBoxList6.Items[0].Selected == true)//单位
            {
                attribute += CheckBoxList6.Items[0].Value;
            }
            if (CheckBoxList7.Items[0].Selected == true)//安全库存
            {
                attribute += CheckBoxList7.Items[0].Value;
            }
            
            if (attribute != "")
            {
                attribute = attribute.Substring(0, attribute.Length - 1);
                sql += attribute;
                sql += " FROM View_STORAGE_ADD_DELETE ";
            }

            
           
            if (TextBox1.Text != "")
            {
                condition += "AND MaterialCode LIKE '%" + TextBox1.Text + "%' ";
            }
            if (TextBox2.Text != "")
            {
                condition += "AND MaterialName LIKE '%" + TextBox2.Text + "%' ";
            }
            if (TextBox3.Text != "")
            {
                condition += "AND Attribute LIKE '%" + TextBox3.Text + "%' ";
            }
            if (TextBox4.Text != "")
            {
                condition += "AND GB LIKE '%" + TextBox4.Text + "%' ";
            }
            if (TextBox5.Text != "")
            {
                condition += "AND Standard LIKE '%" + TextBox5.Text + "%' ";
            }
            if (TextBox6.Text != "")
            {
                condition += "AND Fixed LIKE '%" + TextBox6.Text + "%' ";
            }
            if (TextBox7.Text != "")
            {
                condition += "AND Length LIKE '%" + TextBox7.Text + "%' ";
            }
           
            if (condition != "")
            {
                condition = condition.Substring(3, condition.Length - 3);
                condition = condition.Substring(0, condition.Length - 1);
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

            // 设置单元格格式  
            //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).NumberFormat = "@";
            //wksheet.get_Range(ac.Cells[6, 1], ac.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).Font.Size = 12;

            //设置表头
            ac.Cells[1, 1] = "序号";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ac.Cells[1, i + 2] = dt.Columns[i].ColumnName;
            }

            int rowIndex = 2;   // 行  
            int colIndex = 2;   // 列 
            // 填充数据很简单无非是一个2重循环，下面看起来逻辑很复杂是因为添加了合并单元格的代码  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ac.Cells[rowIndex, 1] = "'" + (rowIndex - 1).ToString();
                colIndex = 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
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
            string filename = Server.MapPath("/SM_Data/ExportFile/" + "WARN" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  

            //Response.Write("<script type='text/javascript'>window.open('" + filename + "', '_bank', 'height=500, width=800, top=50, left=50, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");  

            if (System.IO.File.Exists(filename))
            {
                DownloadFile.Send(Context, filename);
            }
        }
    }
}
