using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using NPOI.HSSF.UserModel;
using System.Data;
using NPOI.HSSF.Model;
using NPOI.SS.UserModel;
using ZCZJ_DPF.CommonClass;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_DaoRu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            HttpPostedFile hpf = FileUpload1.PostedFile;
            List<int> list = new List<int>() { 1 };
            string tName = string.Empty;
            Button bt = (Button)sender;
            string id = bt.ID;
            if (id == "btn")
            {
                for (int i = 4; i < 33; i++)
                {
                    list.Add(i);
                }
                tName = "OM_KQTJ";
                OMImPort.Import(hpf, list, tName);
            }
            else if (id == "btn1")
            {
                list.Add(3);
                tName = "OM_GDGZ";
                OMImPort.Import(hpf, list, tName);
            }
            else if (id == "btn2")
            {
                for (int i = 7; i < 17; i++)
                {
                    list.Add(i);
                }
                tName = "OM_GJJ";
                OMImPort.Import(hpf, list, tName);
            }
            else if (id == "btn3")
            {
                list.Add(4);
                for (int i = 6; i < 24; i++)
                {
                    list.Add(i);
                }
                tName = "OM_SHBX";
                OMImPort.Import(hpf, list, tName);
            }
            else if (id == "btn5")
            {
                for (int i = 4; i < 10; i++)
                {
                    list.Add(i);
                }
                tName = "OM_SCCZ";
                OMImPort.Import(hpf, list, tName);
            }
            else if (id == "btn6")
            {
                for (int i = 4; i < 10; i++)
                {
                    list.Add(i);
                }
                tName = "OM_BMJX";
                OMImPort.Import(hpf, list, tName);
            }
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            HttpPostedFile hpf = FileUpload1.PostedFile;
            List<int> list = new List<int>() { 2 };
            double[] a = new double[] { 0.2, 0.02, 0.015, 0.008, 0.1, 0.11, 0.08, 0.01, 0.02, 0.11 };//系数
            for (int i = 5; i < 7; i++)
            {
                list.Add(i);
            }
            using (FileStream fs = File.OpenRead(hpf.FileName))
            {
                //根据文件流创建一个workbook
                using (Workbook wk = new HSSFWorkbook(fs))
                {
                    //获取第一个工作表
                    using (Sheet sheet = wk.GetSheetAt(0))
                    {
                        //循环读取每一行数据，由于execel有列名以及序号，从1开始
                        List<string> sql = new List<string>();
                        for (int r = 2; r <= sheet.LastRowNum; r++)
                        {
                            Row row = sheet.GetRow(r);

                            #region 存入数据库

                            List<double> str = new List<double>();
                            for (int i = 0; i < list.Count; i++)
                            {
                                double cell = Convert.ToDouble(row.GetCell(list[i]) == null ? "0" : row.GetCell(list[i]).ToString());
                                str.Add(cell);
                            }
                            double JfBase = Convert.ToDouble(row.GetCell(list[2]) == null ? "0" : row.GetCell(list[2]).ToString());
                            double sum = 0;
                            for (int i = 0; i < 6; i++)
                            {
                                double cal = JfBase * a[i];
                                str.Add(cal);
                                sum += cal;
                            }
                            double cell13 = Convert.ToDouble(row.GetCell(13) == null ? "0" : row.GetCell(13).ToString());
                            str.Add(cell13);
                            str.Add(sum);
                            double sum1 = 0;
                            for (int i = 6; i < 9; i++)
                            {
                                double cal = JfBase * a[i];
                                str.Add(cal);
                                sum1 += cal;
                            }
                            double cell18 = Convert.ToDouble(row.GetCell(18) == null ? "0" : row.GetCell(18).ToString());
                            str.Add(cell18);
                            str.Add(sum1);
                            str.Add(JfBase * a[9]);
                            double cell21 = Convert.ToDouble(row.GetCell(21) == null ? "0" : row.GetCell(21).ToString());
                            str.Add(cell21);
                            str.Add(sum1 + JfBase * a[9] + cell21);
                            double cell23 = Convert.ToDouble(row.GetCell(23) == null ? "0" : row.GetCell(23).ToString());
                            str.Add(cell23);
                            double cell24 = Convert.ToDouble(row.GetCell(24) == null ? "0" : row.GetCell(24).ToString());
                            str.Add(cell24);
                            str.Add(sum + sum1 + cell23 + cell24);
                            StringBuilder sb = new StringBuilder();
                            sb.Append("'" + str[0] + "',").Append("'" + row.GetCell(4) == null ? "0" : row.GetCell(23).ToString() + "',");
                            for (int i = 1; i < str.Count; i++)
                            {
                                sb.Append("'" + str[i] + "',");
                            }
                            string val = sb.ToString().Substring(0, sb.Length - 1);

                            string sqlTxt = string.Format("insert into {0} values({1})", "OM_LDBX", val);
                            sql.Add(sqlTxt);

                            #endregion
                        }
                        DBCallCommon.ExecuteTrans(sql);
                    }
                }
            }
        }
    }
}
