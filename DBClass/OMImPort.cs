using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Text;
using System.Web.UI;


namespace ZCZJ_DPF.CommonClass
{
    public class OMImPort
    {
        /// <summary>
        /// 导入1（列名只有一列）
        /// </summary>
        /// <param name="hpf">读取文件</param>
        /// <param name="list">导入列的集合</param>
        /// <param name="TableName">表名</param>
        public static void Import(HttpPostedFile hpf, List<int> list, string TableName)
        {
            //string filePath = HttpContext.Current.Server.MpPath(hpf.FileName);
            //string filePath = page.MapPath(hpf.);
            using (FileStream fs = File.OpenRead(hpf.FileName))
            {
                //根据文件流创建一个workbook
              IWorkbook wk = new HSSFWorkbook(fs);
                    //获取第一个工作表
              ISheet sheet = wk.GetSheetAt(0);
                        //循环读取每一行数据，由于execel有列名以及序号，从1开始
                        List<string> sql = new List<string>();
                        for (int r = 5; r <= sheet.LastRowNum; r++)
                        {
                            IRow row = sheet.GetRow(r);

                            #region 存入数据库

                            List<string> str = new List<string>();
                            for (int i = 0; i < list.Count; i++)
                            {
                                string cell = row.GetCell(list[i]) == null ? "0" : row.GetCell(list[i]).ToString();
                                str.Add(cell);
                            }
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < str.Count; i++)
                            {
                                sb.Append("'" + str[i] + "',");
                            }
                            string val = sb.ToString() + "'" + DateTime.Now.ToString("yyyy-MM") + "'";

                            string sqlTxt = string.Format("insert into {0} values({1})", TableName, val);
                            sql.Add(sqlTxt);

                            #endregion
                        }
                        DBCallCommon.ExecuteTrans(sql);
                    }
                }
     



        public static void Import1(HttpPostedFile hpf, List<int> list, string TableName)
        {
            //using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath(hpf.FileName)))
            //{
            //    //根据文件流创建一个workbook
            //    using (Workbook wk = new HSSFWorkbook(fs))
            //    {
            //        //获取第一个工作表
            //        using (Sheet sheet = wk.GetSheetAt(0))
            //        {
            //            //循环读取每一行数据，由于execel有列名以及序号，从1开始
            //            List<string> sql = new List<string>();
            //            for (int r = 1; r <= sheet.LastRowNum; r++)
            //            {
            //                Row row = sheet.GetRow(r);

            //                #region 存入数据库

            //                List<string> str = new List<string>();
            //                for (int i = 0; i < list.Count; i++)
            //                {
            //                    string cell = row.GetCell(list[i]) == null ? "0" : row.GetCell(list[i]).ToString();
            //                    str.Add(cell);
            //                }
            //                StringBuilder sb = new StringBuilder();
            //                for (int i = 0; i < str.Count; i++)
            //                {
            //                    sb.Append("'" + str[i] + "',");
            //                }
            //                string val = sb.ToString() + "'" + DateTime.Now.ToString("yyyy-MM") + "'";

            //                string sqlTxt = string.Format("insert into {0} values({1})", TableName, val);
            //                sql.Add(sqlTxt);

            //                #endregion
            //            }
            //            DBCallCommon.ExecuteTrans(sql);
            //        }
            //    }
            //}
        }
    }
}
