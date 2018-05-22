using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.FM_Data
{
    public class ExportDataFromDB
    {
        /// <summary>
        /// 根据生产制号导出材料明细
        /// </summary>
        /// <param name="list_ProductNum"></param>
        public static void ExportMaterialDetail(List<string> list_ProductNum)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("材料明细表") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;
            //Microsoft.Office.Interop.Excel.Style st=workbook.Styles.Add("PropertyBorder", Type.Missing);

            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //复制list_ProductNum.Count-1个WorkSheet对象
            for (int sheetcount = 1; sheetcount < list_ProductNum.Count; sheetcount++)
            {
                ((Worksheet)workbook.Worksheets.get_Item(sheetcount)).Copy(System.Type.Missing, workbook.Worksheets[sheetcount]);
            }
            //((Worksheet)workbook.Worksheets.get_Item(list_ProductNum.Count)).Delete();//删除多余工作表
            for (int num = 0; num < list_ProductNum.Count; num++)
            {
                string sqltext = "select ROW_NUMBER() OVER (ORDER BY MaterialCode DESC) AS Row_Num,* from View_SM_OUT where TSAID like '%" + list_ProductNum[num] + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

                wksheet = (Worksheet)workbook.Sheets[num + 1];//获取工作表
                string name = "";
                if (dt.Rows.Count > 0)
                {
                    name = list_ProductNum[num].ToString();// +dt.Rows[0]["ENG"].ToString();
                }
                else
                {
                    name = list_ProductNum[num].ToString() + "未领料";
                }
                int length = name.Length;
                if (length > 31)
                {
                    length = 31;
                }
                wksheet.Name = name.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-').Substring(0, length);//工作表名称 //替换 ： / ? * [ 或 ]，最长31
                // 填充数据
                object[,] dataArry = new object[dt.Rows.Count, 14];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataArry[i, 0] = dt.Rows[i]["Row_Num"].ToString();
                    dataArry[i, 1] = dt.Rows[i]["ApprovedDate"].ToString().Substring(0, 10);
                    dataArry[i, 2] = dt.Rows[i]["Dep"].ToString();
                    dataArry[i, 3] = dt.Rows[i]["OutCode"].ToString();
                    dataArry[i, 4] = dt.Rows[i]["Warehouse"].ToString();
                    dataArry[i, 5] = dt.Rows[i]["MaterialCode"].ToString();
                    dataArry[i, 6] = dt.Rows[i]["MaterialName"].ToString();
                    dataArry[i, 7] = dt.Rows[i]["Standard"].ToString();
                    dataArry[i, 8] = dt.Rows[i]["Unit"].ToString();
                    dataArry[i, 9] = dt.Rows[i]["RealNumber"].ToString();
                    dataArry[i, 10] = dt.Rows[i]["UnitPrice"].ToString();
                    dataArry[i, 11] = dt.Rows[i]["Amount"].ToString();
                    dataArry[i, 12] = dt.Rows[i]["Doc"].ToString();
                    dataArry[i, 13] = dt.Rows[i]["TSAID"].ToString();
                                     
                        
                    wksheet.get_Range(wksheet.Cells[i + 2, 1], wksheet.Cells[i + 2, 14]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                }
                wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, 14]).Value2 = dataArry;
                //设置列宽
                wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            }
            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportDataFromDB.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        /// <summary>
        ///  根据生产制号导出成本汇总表
        /// </summary> 
        /// <param name="arrayProductNum"></param>
        public static void ExportCostSummary(List<string> list_ProductNum)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("成本汇总表") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
            //查询生产制号连接
            string productNum = "";
            for (int list_count = 0; list_count < list_ProductNum.Count - 1; list_count++)
            {
                productNum += "'" + list_ProductNum[list_count] + "',";
            }
            productNum += "'" + list_ProductNum[list_ProductNum.Count - 1] + "'";
            //连接完成

            string sqltext = "select ROW_NUMBER() OVER (ORDER BY PPL_SCZH DESC) AS Row_Num,*,(PMS_CLGJ+PMS_QTGJ+PMS_DDGJ+PMS_HJGJ+PMS_LBYP+PMS_QDGJ+PMS_QXGJ+PMS_QZYY+PMS_SDGJ) as PMS_DZYH from View_CB_Summary where  PPL_SCZH in(" + productNum + ")";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            wksheet = (Worksheet)workbook.Sheets[1];//获取工作表
            string name = "成本汇总表-（导出日期" + DateTime.Now.ToShortDateString() + ")";//工作表名称
            int length = name.Length;
            if (length > 31)
            {
                length = 31;
            }
            wksheet.Name = name.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-').Substring(0, length);//工作表名称 //替换 ： / ? * [ 或 ]，最长31
            //设置表头
            wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, dt.Rows.Count]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            // 填充数据
            object[,] dataArry1 = new object[dt.Rows.Count, 24];
            object[,] dataArry2 = new object[dt.Rows.Count, 5];
            object[,] dataArry3 = new object[dt.Rows.Count, 2];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataArry1[i, 0] = dt.Rows[i]["Row_Num"].ToString();//序号
                dataArry1[i, 1] = dt.Rows[i]["PPL_SCZH"].ToString();//生产制号
                dataArry1[i, 2] = dt.Rows[i]["PPL_CPMC"].ToString();//产品名称
                dataArry1[i, 3] = dt.Rows[i]["PPL_WGSJ"].ToString();//完工日期
                dataArry1[i, 4] = dt.Rows[i]["PMS_HSJSSL"].ToString();//黑色金属数量
                dataArry1[i, 5] = dt.Rows[i]["PMS_HSJS"].ToString();//黑色金属
                dataArry1[i, 6] = dt.Rows[i]["PMS_BZJ"].ToString();//标准件
                dataArry1[i, 7] = dt.Rows[i]["PMS_CNPJ"].ToString();//厂内配件
                dataArry1[i, 8] = dt.Rows[i]["PMS_DL"].ToString();//电料
                dataArry1[i, 9] = dt.Rows[i]["PMS_GJFM"].ToString();//管件阀门
                dataArry1[i, 10] = dt.Rows[i]["PMS_HCL"].ToString();//焊材类
                dataArry1[i, 11] = dt.Rows[i]["PMS_HGXJ"].ToString();//化工橡胶
                dataArry1[i, 12] = dt.Rows[i]["PMS_JGJ"].ToString();//加工件
                dataArry1[i, 13] = dt.Rows[i]["PMS_MCKY"].ToString();//木材矿窑
                dataArry1[i, 14] = dt.Rows[i]["PMS_RYL"].ToString();//燃油类
                dataArry1[i, 15] = dt.Rows[i]["PMS_WGJ"].ToString();//外购件
                dataArry1[i, 16] = dt.Rows[i]["PMS_WJCL"].ToString();//五金材料
                dataArry1[i, 17] = dt.Rows[i]["PMS_XFQC"].ToString();//消防器材
                dataArry1[i, 18] = dt.Rows[i]["PMS_YSJS"].ToString();//有色金属
                dataArry1[i, 19] = dt.Rows[i]["PMS_YQTL"].ToString();//油漆涂料
                dataArry1[i, 20] = dt.Rows[i]["PMS_ZP"].ToString();//杂品
                dataArry1[i, 21] = dt.Rows[i]["PMS_ZZCL"].ToString();//周转材料
                dataArry1[i, 22] = dt.Rows[i]["PMS_DQDL"].ToString();//电气电料
                dataArry1[i, 23] = dt.Rows[i]["PMS_DZYH"].ToString();//低值易耗品

                dataArry2[i, 0] = dt.Rows[i]["PPC_WWJGF"].ToString();//委外加工费
                dataArry2[i, 1] = dt.Rows[i]["PPC_GZ"].ToString();//工资
                dataArry2[i, 2] = dt.Rows[i]["PPC_BZGZ"].ToString();//包装工资
                dataArry2[i, 3] = dt.Rows[i]["PPC_FLJBX"].ToString();//福利及保险
                dataArry2[i, 4] = dt.Rows[i]["PPC_BZZFLJBX"].ToString();//包装组福利及保险

                dataArry3[i, 0] = dt.Rows[i]["PMC_FTZZFY"].ToString();//分摊制造费用
                dataArry3[i, 1] = dt.Rows[i]["PMC_YF"].ToString();//运费

                //wksheet.Cells[i + 5, 1] = dt.Rows[i]["Row_Num"].ToString();//序号
                //wksheet.Cells[i + 5, 2] = dt.Rows[i]["PPL_SCZH"].ToString();//生产制号
                //wksheet.Cells[i + 5, 3] = dt.Rows[i]["PPL_CPMC"].ToString();//产品名称
                //wksheet.Cells[i + 5, 4] = dt.Rows[i]["PPL_WGSJ"].ToString();//完工日期
                //wksheet.Cells[i + 5, 5] = dt.Rows[i]["PMS_HSJSSL"].ToString();//黑色金属数量
                //wksheet.Cells[i + 5, 6] = dt.Rows[i]["PMS_HSJS"].ToString();//黑色金属
                //wksheet.Cells[i + 5, 7] = dt.Rows[i]["PMS_BZJ"].ToString();//标准件
                //wksheet.Cells[i + 5, 8] = dt.Rows[i]["PMS_CNPJ"].ToString();//厂内配件
                //wksheet.Cells[i + 5, 9] = dt.Rows[i]["PMS_DL"].ToString();//电料
                //wksheet.Cells[i + 5, 10] = dt.Rows[i]["PMS_GJFM"].ToString();//管件阀门
                //wksheet.Cells[i + 5, 11] = dt.Rows[i]["PMS_HCL"].ToString();//焊材类
                //wksheet.Cells[i + 5, 12] = dt.Rows[i]["PMS_HGXJ"].ToString();//化工橡胶

                //wksheet.Cells[i + 5, 13] = dt.Rows[i]["PMS_JGJ"].ToString();//加工件
                //wksheet.Cells[i + 5, 14] = dt.Rows[i]["PMS_MCKY"].ToString();//木材矿窑
                //wksheet.Cells[i + 5, 15] = dt.Rows[i]["PMS_RYL"].ToString();//燃油类
                //wksheet.Cells[i + 5, 16] = dt.Rows[i]["PMS_WGJ"].ToString();//外购件
                //wksheet.Cells[i + 5, 17] = dt.Rows[i]["PMS_WJCL"].ToString();//五金材料
                //wksheet.Cells[i + 5, 18] = dt.Rows[i]["PMS_XFQC"].ToString();//消防器材
                //wksheet.Cells[i + 5, 19] = dt.Rows[i]["PMS_YSJS"].ToString();//有色金属
                //wksheet.Cells[i + 5, 20] = dt.Rows[i]["PMS_YQTL"].ToString();//油漆涂料
                //wksheet.Cells[i + 5, 21] = dt.Rows[i]["PMS_ZP"].ToString();//杂品
                //wksheet.Cells[i + 5, 22] = dt.Rows[i]["PMS_ZZCL"].ToString();//周转材料
                //wksheet.Cells[i + 5, 23] = dt.Rows[i]["PMS_DQDL"].ToString();//电气电料
                //wksheet.Cells[i + 5, 24] = dt.Rows[i]["PMS_DZYH"].ToString();//低值易耗品
                ////wksheet.Cells[i + 5, 25] = dt.Rows[i][""].ToString();//小计
                //wksheet.Cells[i + 5, 26] = dt.Rows[i]["PPC_WWJGF"].ToString();//委外加工费
                //wksheet.Cells[i + 5, 27] = dt.Rows[i]["PPC_GZ"].ToString();//工资
                //wksheet.Cells[i + 5, 28] = dt.Rows[i]["PPC_BZGZ"].ToString();//包装工资
                //wksheet.Cells[i + 5, 29] = dt.Rows[i]["PPC_FLJBX"].ToString();//福利及保险
                //wksheet.Cells[i + 5, 30] = dt.Rows[i]["PPC_BZZFLJBX"].ToString();//包装组福利及保险
                ////wksheet.Cells[i + 5, 31] = dt.Rows[i][""].ToString();//小计
                //wksheet.Cells[i + 5, 32] = dt.Rows[i]["PMC_FTZZFY"].ToString();//分摊制造费用
                //wksheet.Cells[i + 5, 33] = dt.Rows[i]["PMC_YF"].ToString();//运费
                ////wksheet.Cells[i + 5, 34] = dt.Rows[i][""].ToString();//小计
                ////wksheet.Cells[i + 5, 35] = dt.Rows[i][""].ToString();//合计

                //***********行汇总
                string col = (i + 5).ToString();
                string formula1 = "=SUM(F" + col + ":X" + col + ")";
                string formula2 = "=SUM(Z" + col + ":AD" + col + ")";
                string formula3 = "=SUM(AF" + col + ",AG" + col + ")";
                string formula4 = "=SUM(Y" + col + ",AE" + col + ",AH" + col + ")";

                Range rg1 = wksheet.get_Range("Y" + col, System.Type.Missing);
                rg1.Formula = formula1;
                rg1.Calculate();//小计

                Range rg2 = wksheet.get_Range("AE" + col, System.Type.Missing);
                rg2.Formula = formula2;
                rg2.Calculate();//小计

                Range rg3 = wksheet.get_Range("AH" + col, System.Type.Missing);
                rg3.Formula = formula3;
                rg3.Calculate();//小计

                Range rg4 = wksheet.get_Range("AI" + col, System.Type.Missing);
                rg4.Formula = formula4;
                rg4.Calculate();//合计
                //*******************End行汇总

                wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 35]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 35]).Borders.LineStyle = 1;

            }
            wksheet.get_Range("A5", wksheet.Cells[dt.Rows.Count + 4, 24]).Value2 = dataArry1;
            wksheet.get_Range("Z5", wksheet.Cells[dt.Rows.Count + 4, 30]).Value2 = dataArry2;
            wksheet.get_Range("AF5", wksheet.Cells[dt.Rows.Count + 4, 33]).Value2 = dataArry3;

            //列汇总
            Range rg5 = wksheet.get_Range("E" + (dt.Rows.Count + 5).ToString(), System.Type.Missing);
            rg5.Formula = "=SUM(E4:E" + (dt.Rows.Count + 4).ToString() + ")";
            for (int col = 6; col <= 35; col++)
            {
                rg5.Copy(wksheet.Cells[dt.Rows.Count + 5, col]);
            }
            rg5.Calculate();
            wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 5, 1], wksheet.Cells[dt.Rows.Count + 5, 35]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 5, 1], wksheet.Cells[dt.Rows.Count + 5, 35]).Borders.LineStyle = 1;

            wksheet.Cells[dt.Rows.Count + 5, 1] = "汇总";
            Range rghz = wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 5, 1], wksheet.Cells[dt.Rows.Count + 5, 4]);
            rghz.MergeCells = true;
            rghz.HorizontalAlignment = XlHAlign.xlHAlignRight;
            //End列汇总

            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

          

            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportDataFromDB.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        /// <summary>
        /// 根据生产制号导出盈亏汇总表
        /// </summary>
        /// <param name="arrayProductNum"></param>
        public static void ExportProfitLoss(List<string> list_ProductNum)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("产品盈亏汇总表") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt);

            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
            //查询生产制号连接
            string productNum = "";
            for (int list_count = 0; list_count < list_ProductNum.Count - 1; list_count++)
            {
                productNum += "'" + list_ProductNum[list_count] + "',";
            }
            productNum += "'" + list_ProductNum[list_ProductNum.Count - 1] + "'";
            //连接完成

            string sqltext = "select ROW_NUMBER() OVER (ORDER BY PPL_SCZH DESC) AS Row_Num,* from View_CB_Summary where  PPL_SCZH in(" + productNum + ")";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            wksheet = (Worksheet)workbook.Sheets[1];//获取工作表
            string name = "产品盈亏情况表-（导出日期" + DateTime.Now.ToShortDateString() + ")";//工作表名称
            int length = name.Length;
            if (length > 31)
            {
                length = 31;
            }
            wksheet.Name = name.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-').Substring(0, length);//工作表名称 //替换 ： / ? * [ 或 ]，最长31
            //设置表头
            wksheet.get_Range(wksheet.Cells[1, 1], wksheet.Cells[1, dt.Rows.Count]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            // 填充数据
            object[,] dataArry1 = new object[dt.Rows.Count, 10];
            object[,] dataArry2 = new object[dt.Rows.Count, 4];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataArry1[i, 0] = dt.Rows[i]["Row_Num"].ToString();//序号
                dataArry1[i, 1] = dt.Rows[i]["PPL_SCZH"].ToString();//生产制号
                dataArry1[i, 2] = dt.Rows[i]["PPL_WGSJ"].ToString();//完工时间
                dataArry1[i, 3] = dt.Rows[i]["PPL_CPMC"].ToString();//产品名称
                dataArry1[i, 4] = dt.Rows[i]["PPL_HTH"].ToString();//合同号
                dataArry1[i, 5] = dt.Rows[i]["PPL_HTJE"].ToString();//合同金额
                dataArry1[i, 6] = dt.Rows[i]["PPL_BHSHTJE"].ToString();//不含税合同金额
                dataArry1[i, 7] = dt.Rows[i]["PPL_HTZL"].ToString();//合同重量（吨）
                dataArry1[i, 8] = dt.Rows[i]["PPL_ZLGCZL"].ToString();//支领钢材重量（吨）
                dataArry1[i, 9] = dt.Rows[i]["PPL_CPZCB"].ToString();//产品总成本

                dataArry2[i, 0] = dt.Rows[i]["PPL_FTGLFY"].ToString();//分摊管理费用
                dataArry2[i, 1] = dt.Rows[i]["PPL_FTCWFY"].ToString();//分摊财务费用
                dataArry2[i, 2] = dt.Rows[i]["PPL_FTZYYWSJJFJ"].ToString();//分摊主营业务税金及附加
                dataArry2[i, 3] = ((Convert.ToDouble(dt.Rows[i]["PPL_BHSHTJE"].ToString()) - Convert.ToDouble(dt.Rows[i]["PPL_FTGLFY"].ToString()) - Convert.ToDouble(dt.Rows[i]["PPL_FTCWFY"].ToString()) - Convert.ToDouble(dt.Rows[i]["PPL_FTZYYWSJJFJ"].ToString()) - Convert.ToDouble(dt.Rows[i]["PPL_CPZCB"].ToString())) * 0.75).ToString();//净利润
                
                //wksheet.Cells[i + 3, 1] = dt.Rows[i]["Row_Num"].ToString();//序号
                //wksheet.Cells[i + 3, 2] = dt.Rows[i]["PPL_SCZH"].ToString();//生产制号
                //wksheet.Cells[i + 3, 3] = dt.Rows[i]["PPL_WGSJ"].ToString();//完工时间
                //wksheet.Cells[i + 3, 4] = dt.Rows[i]["PPL_CPMC"].ToString();//产品名称
                //wksheet.Cells[i + 3, 5] = dt.Rows[i]["PPL_HTH"].ToString();//合同号
                //wksheet.Cells[i + 3, 6] = dt.Rows[i]["PPL_HTJE"].ToString();//合同金额
                //wksheet.Cells[i + 3, 7] = dt.Rows[i]["PPL_BHSHTJE"].ToString();//不含税合同金额
                //wksheet.Cells[i + 3, 8] = dt.Rows[i]["PPL_HTZL"].ToString();//合同重量（吨）
                //wksheet.Cells[i + 3, 9] = dt.Rows[i]["PPL_ZLGCZL"].ToString();//支领钢材重量（吨）
                //wksheet.Cells[i + 3, 10] = dt.Rows[i]["PPL_CPZCB"].ToString();//产品总成本
                ////wksheet.Cells[i + 3, 11] = dt.Rows[i][""].ToString();//毛利率
                //wksheet.Cells[i + 3, 12] = dt.Rows[i]["PPL_FTGLFY"].ToString();//分摊管理费用
                //wksheet.Cells[i + 3, 13] = dt.Rows[i]["PPL_FTCWFY"].ToString();//分摊财务费用
                //wksheet.Cells[i + 3, 14] = dt.Rows[i]["PPL_FTZYYWSJJFJ"].ToString();//分摊主营业务税金及附加
                //wksheet.Cells[i + 3, 15] = dt.Rows[i]["PPL_JLR"].ToString();//净利润
                ////wksheet.Cells[i + 3, 16] = dt.Rows[i][""].ToString();//净利率


                //***********行汇总
                string col = (i + 3).ToString();
                string formula1 = "=(G" + col + "-J" + col + ")/G" + col + "*100";//毛利率
                string formula2 = "=O" + col + "/G" + col + "*100";

                Range rg1 = wksheet.get_Range("K" + col, System.Type.Missing);
                rg1.Formula = formula1;//毛利率
                rg1.Calculate();
                Range rg2 = wksheet.get_Range("P" + col, System.Type.Missing);
                rg2.Formula = formula2;//毛利率
                rg2.Calculate();

                //左对齐，边框
                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 16]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 16]).Borders.LineStyle = 1;
                //汇总行的毛利率和利润率
                if (i == dt.Rows.Count - 1)
                {
                    rg1.Copy(wksheet.Cells[dt.Rows.Count + 3, 11]);
                    rg2.Copy(wksheet.Cells[dt.Rows.Count + 3, 16]);
                }
                //*******************End行汇总

            }
            wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, 10]).Value2 = dataArry1;
            wksheet.get_Range("L3", wksheet.Cells[dt.Rows.Count + 2, 15]).Value2 = dataArry2;

            //列汇总
            Range rg_col_hz = wksheet.get_Range("F" + (dt.Rows.Count + 3).ToString(), System.Type.Missing);
            rg_col_hz.Formula = "=SUM(F3:F" + (dt.Rows.Count + 2).ToString() + ")";
            for (int col = 6; col <= 16; col++)
            {
                if (col != 11 && col != 16)//非毛利率列和利润率列
                {
                    rg_col_hz.Copy(wksheet.Cells[dt.Rows.Count + 3, col]);
                }
            }
            rg_col_hz.Calculate();
            wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 3, 1], wksheet.Cells[dt.Rows.Count + 3, 16]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 3, 1], wksheet.Cells[dt.Rows.Count + 3, 16]).Borders.LineStyle = 1;

            wksheet.Cells[dt.Rows.Count + 3, 1] = "汇总";
            Range rghz = wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 3, 1], wksheet.Cells[dt.Rows.Count + 3, 5]);
            rghz.MergeCells = true;
            rghz.HorizontalAlignment = XlHAlign.xlHAlignRight;
            //End列汇总
            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportDataFromDB.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

        }

        /// <summary>
        /// 暂估明细查询
        /// </summary>
        public static void ExportZanGuMingXi(System.Data.DataTable objdt, string flag)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            if (flag == "MonthMar")
            {
                workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("暂估明细查询") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Visible = false;    // Excel不显示  
                m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
                wksheet = (Worksheet)workbook.Sheets.get_Item(1);
                System.Data.DataTable dt = objdt;
                //定义二维数组
                object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count + 1];
                // 填充数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataarry[i, 0] = (i + 1).ToString();
                    dataarry[i, 1] = "'" + dt.Rows[i]["SI_MARID"].ToString();//物料代码
                    dataarry[i, 2] = "'" + dt.Rows[i]["MNAME"].ToString();//物料名称
                    dataarry[i, 3] = "'" + dt.Rows[i]["SI_YEAR"].ToString();//年
                    dataarry[i, 4] = "'" + dt.Rows[i]["SI_PERIOD"].ToString();//期间
                    dataarry[i, 5] = "'" + dt.Rows[i]["SI_BEGNUM"].ToString();//期初
                    dataarry[i, 6] = "'" + dt.Rows[i]["SI_BEGBAL"].ToString();//期初金额
                    dataarry[i, 7] = "'" + dt.Rows[i]["SI_CRCVNUM"].ToString();//输入数量
                    dataarry[i, 8] = "'" + dt.Rows[i]["SI_CRCVMNY"].ToString();//输入金额
                    dataarry[i, 9] = "'" + dt.Rows[i]["SI_GJNUM"].ToString();//发出数量
                    dataarry[i, 10] = "'" + dt.Rows[i]["SI_GJMNY"].ToString();//发出金额
                    dataarry[i, 11] = "'" + dt.Rows[i]["SI_ENDNUM"].ToString();//期末数量
                    dataarry[i, 12] = "'" + dt.Rows[i]["SI_ENDBAL"].ToString();//期末金额
                    wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 13]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 13]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                    wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 13]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                wksheet.get_Range("A4", wksheet.Cells[dt.Rows.Count + 3, dt.Columns.Count + 1]).Value2 = dataarry;
                //设置列宽
                wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
                ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

            }
            else if (flag == "MonthType")
            {
                workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("暂估明细查询") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Visible = false;    // Excel不显示  
                m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
                wksheet = (Worksheet)workbook.Sheets.get_Item(1);
                System.Data.DataTable dt = objdt;
                //定义二维数组
                object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count + 1];
                // 填充数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataarry[i, 0] = (i + 1).ToString();
                    dataarry[i, 1] = "'" + dt.Rows[i]["SI_MARID"].ToString();//物料代码
                    dataarry[i, 2] = "'" + dt.Rows[i]["TY_NAME"].ToString();//物料名称
                    dataarry[i, 3] = "'" + dt.Rows[i]["SI_YEAR"].ToString();//年
                    dataarry[i, 4] = "'" + dt.Rows[i]["SI_PERIOD"].ToString();//期间
                    dataarry[i, 5] = "'" + dt.Rows[i]["SI_BEGNUM"].ToString();//期初
                    dataarry[i, 6] = "'" + dt.Rows[i]["SI_BEGBAL"].ToString();//期初金额
                    dataarry[i, 7] = "'" + dt.Rows[i]["SI_CRCVNUM"].ToString();//输入数量
                    dataarry[i, 8] = "'" + dt.Rows[i]["SI_CRCVMNY"].ToString();//输入金额
                    dataarry[i, 9] = "'" + dt.Rows[i]["SI_GJNUM"].ToString();//发出数量
                    dataarry[i, 10] = "'" + dt.Rows[i]["SI_GJMNY"].ToString();//发出金额
                    dataarry[i, 11] = "'" + dt.Rows[i]["SI_ENDNUM"].ToString();//期末数量
                    dataarry[i, 12] = "'" + dt.Rows[i]["SI_ENDBAL"].ToString();//期末金额
                    wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 13]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 13]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                    wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 13]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                wksheet.get_Range("A4", wksheet.Cells[dt.Rows.Count + 3, dt.Columns.Count + 1]).Value2 = dataarry;
                //设置列宽
                wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
                ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

            }

        }
        /// <summary>
        /// 按月汇总导出各种表
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <param name="startMonth"></param>
        /// <param name="endMonth"></param>
        /// <param name="productNum"></param>
        /// <param name="boolArrayExportType"></param>
        public static void ExportMonthSummary(int startYear, int endYear, int startMonth, int endMonth, string productNum, bool[] boolArrayExportType)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("成本汇总表（按月、包括明细）") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //导出表判断
            #region
            int sheetNum_CostMont = 0;//成本表的个数（包括汇总和单月）
            if (!boolArrayExportType[0] && !boolArrayExportType[1])
            {
                ((Worksheet)workbook.Sheets.get_Item(1)).Delete();
            }
            else
            {
                //时间区间（要创建各月成本表的个数）
                int allMoths = (endYear - startYear) * 12 + (endMonth - startMonth) + 1;
                sheetNum_CostMont = (boolArrayExportType[0] == false ? (boolArrayExportType[1] == false ? 0 : allMoths) : (boolArrayExportType[1] == true ? allMoths + 1 : 1));
                for (int sheetcount = 1; sheetcount < sheetNum_CostMont; sheetcount++)
                {
                    ((Worksheet)workbook.Worksheets.get_Item(sheetcount)).Copy(System.Type.Missing, workbook.Worksheets[sheetcount]);
                }
            }

            //生产制号
            List<string> list_ProductNum = new List<string>();
            List<string> list_ProductTime = new List<string>();
            if (!boolArrayExportType[2])
            {
                ((Worksheet)workbook.Sheets.get_Item(2)).Delete();
            }
            else
            {
                //查找出生产制号
                string sqltext = "select PMS_SCZH,cast(PMS_TJNF as varchar(50))+'-'+right(str(PMS_TJYF+100),2) as time from View_CB_Month_Summary where PMS_TJNF>=" + startYear + " and PMS_TJNF<=" + endYear + " and PMS_TJYF>=" + startMonth + " and PMS_TJYF<=" + endMonth + " and PMS_SCZH like '%" + productNum + "%'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        list_ProductNum.Add(dt.Rows[row]["PMS_SCZH"].ToString());
                        list_ProductTime.Add(dt.Rows[row]["time"].ToString());
                    }
                    int sheet_detail_first = sheetNum_CostMont + 1;
                    int all_sheetNums = sheetNum_CostMont + list_ProductNum.Count;
                    for (int sheetcount = sheet_detail_first; sheetcount < all_sheetNums; sheetcount++)
                    {
                        ((Worksheet)workbook.Worksheets.get_Item(sheetcount)).Copy(System.Type.Missing, workbook.Worksheets[sheetcount]);
                    }
                }

            }
            #endregion
            //开始导出数据
            int currentSheet = 2;//用来记录数据写到哪一个工作表中了
            //汇总表（变成按月导出，暂时隐藏）
            #region
            //if (boolArrayExportType[0])
            //{
            //    string year_Month = startYear.ToString() + "-" + startMonth.ToString() + "至" + endYear.ToString() + "-" + endMonth.ToString();
            //    string sqltext = "Select distinct(PMS_SCZH) AS PMS_SCZH,ROW_NUMBER() OVER (ORDER BY PMS_SCZH ASC) AS Row_Num,ENG,'" + year_Month + "' AS YearMonth,SUM(PMS_BZJ) AS PMS_BZJ,SUM(PMS_CNPJ) AS PMS_CNPJ,SUM(PMS_DL) AS PMS_DL,SUM(PMS_GJFM) AS PMS_GJFM,SUM(PMS_HCL) AS PMS_HCL,SUM(PMS_HGXJ) AS PMS_HGXJ,SUM(PMS_HSJSSL) as PMS_HSJSSL,SUM(PMS_HSJS) AS PMS_HSJS,SUM(PMS_JGJ) AS PMS_JGJ,SUM(PMS_MCKY) AS PMS_MCKY,SUM(PMS_RYL) AS PMS_RYL,SUM(PMS_WGJ) AS PMS_WGJ,SUM(PMS_WJCL) AS PMS_WJCL,SUM(PMS_XFQC) AS PMS_XFQC,SUM(PMS_YSJS) AS PMS_YSJS,SUM(PMS_YQTL) AS PMS_YQTL,SUM(PMS_ZP) AS PMS_ZP,SUM(PMS_ZZCL) AS PMS_ZZCL,SUM(PMS_DQDL) AS PMS_DQDL,(SUM(PMS_CLGJ)+SUM(PMS_QTGJ)+SUM(PMS_DDGJ)+SUM(PMS_HJGJ)+SUM(PMS_LBYP)+SUM(PMS_QDGJ)+SUM(PMS_QXGJ)+SUM(PMS_QZYY)+SUM(PMS_SDGJ)) AS PMS_DZYH,SUM(PPC_WWJGF) AS PPC_WWJGF,SUM(PPC_GZ) AS PPC_GZ,SUM(PPC_BZGZ) AS PPC_BZGZ,SUM(PPC_FLJBX) AS PPC_FLJBX,SUM(PPC_BZZFLJBX) AS PPC_BZZFLJBX,SUM(PMC_FTZZFY) AS PMC_FTZZFY,SUM(PMC_YF) AS PMC_YF from View_CB_Month_Summary ";
            //    //sqltext += " where (SUM(PMS_HSJS)!=0 or SUM(PMS_BZJ)!=0 or SUM(PMS_CNPJ)!=0 or SUM(PMS_CNPJ)!=0 or SUM(PMS_DL)!=0 or SUM(PMS_GJFM)!=0 or SUM(PMS_HCL)!=0 or SUM(PMS_HGXJ)!=0 or SUM(PMS_JGJ)!=0 or SUM(PMS_MCKY)!=0 or SUM(PMS_RYL)!=0 or SUM(PMS_WGJ)!=0 or PMS_WJCL!=0 or SUM(PMS_XFQC)!=0 or SUM(PMS_YSJS)!=0 or SUM(PMS_YQTL)!=0 or SUM(PMS_ZP)!=0 or SUM(PMS_ZZCL)!=0 or SUM(PMS_DQDL)!=0 ) ";
            //           sqltext += "Group by PMS_SCZH,ENG";
            //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //    wksheet = (Worksheet)workbook.Sheets[currentSheet];//获取工作表
            //    wksheet.Name = year_Month + "成本汇总表";

            //    if (dt.Rows.Count > 0)
            //    {
            //        object[,] dataArry1 = new object[dt.Rows.Count, 25];
            //        object[,] dataArry2 = new object[dt.Rows.Count, 5];
            //        object[,] dataArry3 = new object[dt.Rows.Count, 2];
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            dataArry1[i, 0] = dt.Rows[i]["Row_Num"].ToString();//序号
            //            dataArry1[i, 1] = dt.Rows[i]["PMS_SCZH"].ToString();//生产制号
            //            dataArry1[i, 2] = dt.Rows[i]["ENG"].ToString();//产品名称
            //            dataArry1[i, 3] = "";//完工日期
            //            dataArry1[i, 4] = dt.Rows[i]["YearMonth"].ToString();//月份
            //            dataArry1[i, 5] = dt.Rows[i]["PMS_HSJSSL"].ToString();//黑色金属数量
            //            dataArry1[i, 6] = dt.Rows[i]["PMS_HSJS"].ToString();//黑色金属
            //            dataArry1[i, 7] = dt.Rows[i]["PMS_BZJ"].ToString();//标准件
            //            dataArry1[i, 8] = dt.Rows[i]["PMS_CNPJ"].ToString();//厂内配件
            //            dataArry1[i, 9] = dt.Rows[i]["PMS_DL"].ToString();//电料
            //            dataArry1[i, 10] = dt.Rows[i]["PMS_GJFM"].ToString();//管件阀门
            //            dataArry1[i, 11] = dt.Rows[i]["PMS_HCL"].ToString();//焊材类
            //            dataArry1[i, 12] = dt.Rows[i]["PMS_HGXJ"].ToString();//化工橡胶
            //            dataArry1[i, 13] = dt.Rows[i]["PMS_JGJ"].ToString();//加工件
            //            dataArry1[i, 14] = dt.Rows[i]["PMS_MCKY"].ToString();//木材矿窑
            //            dataArry1[i, 15] = dt.Rows[i]["PMS_RYL"].ToString();//燃油类
            //            dataArry1[i, 16] = dt.Rows[i]["PMS_WGJ"].ToString();//外购件
            //            dataArry1[i, 17] = dt.Rows[i]["PMS_WJCL"].ToString();//五金材料
            //            dataArry1[i, 18] = dt.Rows[i]["PMS_XFQC"].ToString();//消防器材
            //            dataArry1[i, 19] = dt.Rows[i]["PMS_YSJS"].ToString();//有色金属
            //            dataArry1[i, 20] = dt.Rows[i]["PMS_YQTL"].ToString();//油漆涂料
            //            dataArry1[i, 21] = dt.Rows[i]["PMS_ZP"].ToString();//杂品
            //            dataArry1[i, 22] = dt.Rows[i]["PMS_ZZCL"].ToString();//周转材料
            //            dataArry1[i, 23] = dt.Rows[i]["PMS_DQDL"].ToString();//电气电料
            //            dataArry1[i, 24] = dt.Rows[i]["PMS_DZYH"].ToString();//低值易耗品

            //            dataArry2[i, 0] = dt.Rows[i]["PPC_WWJGF"].ToString();//委外加工费
            //            dataArry2[i, 1] = dt.Rows[i]["PPC_GZ"].ToString();//工资
            //            dataArry2[i, 2] = dt.Rows[i]["PPC_BZGZ"].ToString();//包装工资
            //            dataArry2[i, 3] = dt.Rows[i]["PPC_FLJBX"].ToString();//福利及保险
            //            dataArry2[i, 4] = dt.Rows[i]["PPC_BZZFLJBX"].ToString();//包装组福利及保险

            //            dataArry3[i, 0] = dt.Rows[i]["PMC_FTZZFY"].ToString();//分摊制造费用
            //            dataArry3[i, 1] = dt.Rows[i]["PMC_YF"].ToString();//运费

                      
            //            //***********行汇总
            //            string col = (i + 5).ToString();
            //            string formula1 = "=SUM(G" + col + ":Y" + col + ")";
            //            string formula2 = "=SUM(AA" + col + ":AE" + col + ")";
            //            string formula3 = "=SUM(AG" + col + ",AH" + col + ")";
            //            string formula4 = "=SUM(Z" + col + ",AF" + col + ",AI" + col + ")";

            //            Range rg1 = wksheet.get_Range("Z" + col, System.Type.Missing);
            //            rg1.Formula = formula1;
            //            rg1.Calculate();//小计

            //            Range rg2 = wksheet.get_Range("AF" + col, System.Type.Missing);
            //            rg2.Formula = formula2;
            //            rg2.Calculate();//小计

            //            Range rg3 = wksheet.get_Range("AI" + col, System.Type.Missing);
            //            rg3.Formula = formula3;
            //            rg3.Calculate();//小计

            //            Range rg4 = wksheet.get_Range("AJ" + col, System.Type.Missing);
            //            rg4.Formula = formula4;
            //            rg4.Calculate();//合计
            //            //*******************End行汇总

            //            wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 36]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //            wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 36]).Borders.LineStyle = 1;

            //        }
            //        wksheet.get_Range("A5", wksheet.Cells[dt.Rows.Count + 4, 25]).Value2 = dataArry1;
            //        wksheet.get_Range("AA5", wksheet.Cells[dt.Rows.Count + 4, 31]).Value2 = dataArry2;
            //        wksheet.get_Range("AG5", wksheet.Cells[dt.Rows.Count + 4, 34]).Value2 = dataArry3;

            //        //列汇总
            //        Range rg5 = wksheet.get_Range("F" + (dt.Rows.Count + 5).ToString(), System.Type.Missing);
            //        rg5.Formula = "=SUM(F5:F" + (dt.Rows.Count + 4).ToString() + ")";
            //        for (int col = 7; col <= 36; col++)
            //        {
            //            rg5.Copy(wksheet.Cells[dt.Rows.Count + 5, col]);
            //        }
            //        rg5.Calculate();
            //        wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 5, 1], wksheet.Cells[dt.Rows.Count + 5, 36]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            //        wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 5, 1], wksheet.Cells[dt.Rows.Count + 5, 36]).Borders.LineStyle = 1;

            //        wksheet.Cells[dt.Rows.Count + 5, 1] = "汇总";
            //        Range rghz = wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 5, 1], wksheet.Cells[dt.Rows.Count + 5, 5]);
            //        rghz.MergeCells = true;
            //        rghz.HorizontalAlignment = XlHAlign.xlHAlignRight;
            //        // End列汇总

            //        //设置列宽
            //        wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //    }
            //    currentSheet++;

            //}
            #endregion
            //各月成本表
            List<string> list_yearMonth = new List<string>();
            #region
            if (boolArrayExportType[1])
            {
                list_yearMonth.Clear();
                #region //获取月份：如2011-08
                for (int st_year = startYear; startYear <= endYear; st_year++)
                {
                    int st_month = 1;
                    int end_month = 12;

                    if (st_year == startYear)//开始年份
                    {
                        st_month = startMonth;
                    }
                    if (st_year == endYear)
                    {
                        end_month = endMonth;
                    }
                    for (int i = st_month; i <= end_month; i++)
                    {
                        string yearMonth = "";
                        if (i < 10)
                        {
                            yearMonth = st_year.ToString() + "-0" + i.ToString();
                        }
                        else
                        {
                            yearMonth = st_year.ToString() + "-" + i.ToString();
                        }
                        list_yearMonth.Add(yearMonth);
                    }
                    startYear++;//不增加就会陷入死循环
                }
                #endregion
                #region//对每月信息进行统计
                for (int monthIndex = 0; monthIndex < list_yearMonth.Count; monthIndex++)
                {
                    string sql_month = "select ROW_NUMBER() OVER (ORDER BY PMS_SCZH ASC) AS Row_Num,'" + list_yearMonth[monthIndex].ToString() + "' AS YearMonth,*,(PMS_CLGJ+PMS_QTGJ+PMS_DDGJ+PMS_HJGJ+PMS_LBYP+PMS_QDGJ+PMS_QXGJ+PMS_QZYY+PMS_SDGJ) as PMS_DZYH from View_CB_Month_Summary Where PPC_ID='" + list_yearMonth[monthIndex] + "'";
                    sql_month += " and (PMS_HSJS!=0 or PMS_BZJ!=0 or PMS_CNPJ!=0 or PMS_CNPJ!=0 or PMS_DL!=0 or PMS_GJFM!=0 or PMS_HCL!=0 or PMS_HGXJ!=0 or PMS_JGJ!=0 or PMS_MCKY!=0 or PMS_RYL!=0 or PMS_WGJ!=0 or PMS_WJCL!=0 or PMS_XFQC!=0 or PMS_YSJS!=0 or PMS_YQTL!=0 or PMS_ZP!=0 or PMS_ZZCL!=0 or PMS_DQDL!=0 or (PMS_CLGJ+PMS_QTGJ+PMS_DDGJ+PMS_HJGJ+PMS_LBYP+PMS_QDGJ+PMS_QXGJ+PMS_QZYY+PMS_SDGJ)!=0) ";

                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_month);
                    wksheet = (Worksheet)workbook.Sheets[currentSheet];//获取工作表
                    wksheet.Name = list_yearMonth[monthIndex] + "成本汇总表";
                    if (dt.Rows.Count > 0)
                    {
                        object[,] dataArry1 = new object[dt.Rows.Count, 23];
                        object[,] dataArry2 = new object[dt.Rows.Count, 5];
                        object[,] dataArry3 = new object[dt.Rows.Count, 2];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dataArry1[i, 0] = dt.Rows[i]["Row_Num"].ToString();//序号
                            dataArry1[i, 1] = dt.Rows[i]["PMS_SCZH"].ToString();//生产制号
                            //dataArry1[i, 2] = dt.Rows[i]["ENG"].ToString();//产品名称
                            //dataArry1[i, 3] = dt.Rows[i]["WGRQ"].ToString();//完工日期
                            dataArry1[i, 2] = "'" + dt.Rows[i]["YearMonth"].ToString();//月份
                            dataArry1[i, 3] = dt.Rows[i]["PMS_HSJSSL"].ToString();//黑色金属
                            dataArry1[i, 4] = dt.Rows[i]["PMS_HSJS"].ToString();//黑色金属
                            dataArry1[i, 5] = dt.Rows[i]["PMS_BZJ"].ToString();//标准件
                            dataArry1[i, 6] = dt.Rows[i]["PMS_CNPJ"].ToString();//厂内配件
                            dataArry1[i, 7] = dt.Rows[i]["PMS_DL"].ToString();//电料
                            dataArry1[i, 8] = dt.Rows[i]["PMS_GJFM"].ToString();//管件阀门
                            dataArry1[i, 9] = dt.Rows[i]["PMS_HCL"].ToString();//焊材类
                            dataArry1[i, 10] = dt.Rows[i]["PMS_HGXJ"].ToString();//化工橡胶
                            dataArry1[i, 11] = dt.Rows[i]["PMS_JGJ"].ToString();//加工件
                            dataArry1[i, 12] = dt.Rows[i]["PMS_MCKY"].ToString();//木材矿窑
                            dataArry1[i, 13] = dt.Rows[i]["PMS_RYL"].ToString();//燃油类
                            dataArry1[i, 14] = dt.Rows[i]["PMS_WGJ"].ToString();//外购件
                            dataArry1[i, 15] = dt.Rows[i]["PMS_WJCL"].ToString();//五金材料
                            dataArry1[i, 16] = dt.Rows[i]["PMS_XFQC"].ToString();//消防器材
                            dataArry1[i, 17] = dt.Rows[i]["PMS_YSJS"].ToString();//有色金属
                            dataArry1[i, 18] = dt.Rows[i]["PMS_YQTL"].ToString();//油漆涂料
                            dataArry1[i, 19] = dt.Rows[i]["PMS_ZP"].ToString();//杂品
                            dataArry1[i, 20] = dt.Rows[i]["PMS_ZZCL"].ToString();//周转材料
                            dataArry1[i, 21] = dt.Rows[i]["PMS_DQDL"].ToString();//电气电料
                            dataArry1[i, 22] = dt.Rows[i]["PMS_DZYH"].ToString();//低值易耗品

                            dataArry2[i, 0] = dt.Rows[i]["PPC_WWJGF"].ToString();//委外加工费
                            dataArry2[i, 1] = dt.Rows[i]["PPC_GZ"].ToString();//工资
                            dataArry2[i, 2] = dt.Rows[i]["PPC_BZGZ"].ToString();//包装工资
                            dataArry2[i, 3] = dt.Rows[i]["PPC_FLJBX"].ToString();//福利及保险
                            dataArry2[i, 4] = dt.Rows[i]["PPC_BZZFLJBX"].ToString();//包装组福利及保险

                            dataArry3[i, 0] = dt.Rows[i]["PMC_FTZZFY"].ToString();//分摊制造费用
                            dataArry3[i, 1] = dt.Rows[i]["PMC_YF"].ToString();//运费

                            //wksheet.Cells[i + 5, 1] = dt.Rows[i]["Row_Num"].ToString();//序号
                            //wksheet.Cells[i + 5, 2] = dt.Rows[i]["PMS_SCZH"].ToString();//生产制号
                            //wksheet.Cells[i + 5, 3] = dt.Rows[i]["ENG"].ToString();//产品名称
                            //wksheet.Cells[i + 5, 4] = dt.Rows[i]["WGRQ"].ToString();//完工日期
                            //wksheet.Cells[i + 5, 5] = "'" + dt.Rows[i]["YearMonth"].ToString();//月份
                            //wksheet.Cells[i + 5, 6] = dt.Rows[i]["PMS_HSJSSL"].ToString();//黑色金属
                            //wksheet.Cells[i + 5, 7] = dt.Rows[i]["PMS_HSJS"].ToString();//黑色金属
                            //wksheet.Cells[i + 5, 8] = dt.Rows[i]["PMS_BZJ"].ToString();//标准件
                            //wksheet.Cells[i + 5, 9] = dt.Rows[i]["PMS_CNPJ"].ToString();//厂内配件
                            //wksheet.Cells[i + 5, 10] = dt.Rows[i]["PMS_DL"].ToString();//电料
                            //wksheet.Cells[i + 5, 11] = dt.Rows[i]["PMS_GJFM"].ToString();//管件阀门
                            //wksheet.Cells[i + 5, 12] = dt.Rows[i]["PMS_HCL"].ToString();//焊材类
                            //wksheet.Cells[i + 5, 13] = dt.Rows[i]["PMS_HGXJ"].ToString();//化工橡胶

                            //wksheet.Cells[i + 5, 14] = dt.Rows[i]["PMS_JGJ"].ToString();//加工件
                            //wksheet.Cells[i + 5, 15] = dt.Rows[i]["PMS_MCKY"].ToString();//木材矿窑
                            //wksheet.Cells[i + 5, 16] = dt.Rows[i]["PMS_RYL"].ToString();//燃油类
                            //wksheet.Cells[i + 5, 17] = dt.Rows[i]["PMS_WGJ"].ToString();//外购件
                            //wksheet.Cells[i + 5, 18] = dt.Rows[i]["PMS_WJCL"].ToString();//五金材料
                            //wksheet.Cells[i + 5, 19] = dt.Rows[i]["PMS_XFQC"].ToString();//消防器材
                            //wksheet.Cells[i + 5, 20] = dt.Rows[i]["PMS_YSJS"].ToString();//有色金属
                            //wksheet.Cells[i + 5, 21] = dt.Rows[i]["PMS_YQTL"].ToString();//油漆涂料
                            //wksheet.Cells[i + 5, 22] = dt.Rows[i]["PMS_ZP"].ToString();//杂品
                            //wksheet.Cells[i + 5, 23] = dt.Rows[i]["PMS_ZZCL"].ToString();//周转材料
                            //wksheet.Cells[i + 5, 24] = dt.Rows[i]["PMS_DQDL"].ToString();//电气电料
                            //wksheet.Cells[i + 5, 25] = dt.Rows[i]["PMS_DZYH"].ToString();//低值易耗品
                            ////wksheet.Cells[i + 5, 26] = dt.Rows[i][""].ToString();//小计
                            //wksheet.Cells[i + 5, 27] = dt.Rows[i]["PPC_WWJGF"].ToString();//委外加工费
                            //wksheet.Cells[i + 5, 28] = dt.Rows[i]["PPC_GZ"].ToString();//工资
                            //wksheet.Cells[i + 5, 29] = dt.Rows[i]["PPC_BZGZ"].ToString();//包装工资
                            //wksheet.Cells[i + 5, 30] = dt.Rows[i]["PPC_FLJBX"].ToString();//福利及保险
                            //wksheet.Cells[i + 5, 31] = dt.Rows[i]["PPC_BZZFLJBX"].ToString();//包装组福利及保险
                            ////wksheet.Cells[i + 5, 32] = dt.Rows[i][""].ToString();//小计
                            //wksheet.Cells[i + 5, 33] = dt.Rows[i]["PMC_FTZZFY"].ToString();//分摊制造费用
                            //wksheet.Cells[i + 5, 34] = dt.Rows[i]["PMC_YF"].ToString();//运费
                            ////wksheet.Cells[i + 5, 35] = dt.Rows[i][""].ToString();//小计
                            ////wksheet.Cells[i + 5, 36] = dt.Rows[i][""].ToString();//合计

                            //***********行汇总
                            string col = (i + 5).ToString();
                            string formula1 = "=SUM(E" + col + ":W" + col + ")";
                            string formula2 = "=SUM(Y" + col + ":AC" + col + ")";
                            string formula3 = "=SUM(AE" + col + ",AF" + col + ")";
                            string formula4 = "=SUM(X" + col + ",AD" + col + ",AG" + col + ")";

                            Range rg1 = wksheet.get_Range("X" + col, System.Type.Missing);
                            rg1.Formula = formula1;
                            rg1.Calculate();//小计

                            Range rg2 = wksheet.get_Range("AD" + col, System.Type.Missing);
                            rg2.Formula = formula2;
                            rg2.Calculate();//小计

                            Range rg3 = wksheet.get_Range("AG" + col, System.Type.Missing);
                            rg3.Formula = formula3;
                            rg3.Calculate();//小计

                            Range rg4 = wksheet.get_Range("AH" + col, System.Type.Missing);
                            rg4.Formula = formula4;
                            rg4.Calculate();//合计
                            //*******************End行汇总

                            wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 34]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                            wksheet.get_Range(wksheet.Cells[i + 5, 1], wksheet.Cells[i + 5, 34]).Borders.LineStyle = 1;

                        }
                        wksheet.get_Range("A5", wksheet.Cells[dt.Rows.Count + 4, 23]).Value2 = dataArry1;
                        wksheet.get_Range("Y5", wksheet.Cells[dt.Rows.Count + 4, 29]).Value2 = dataArry2;
                        wksheet.get_Range("AE5", wksheet.Cells[dt.Rows.Count + 4, 32]).Value2 = dataArry3;

                        //列汇总
                        Range rg5 = wksheet.get_Range("D" + (dt.Rows.Count + 5).ToString(), System.Type.Missing);
                        rg5.Formula = "=SUM(D5:D" + (dt.Rows.Count + 4).ToString() + ")";
                        for (int col = 5; col <= 34; col++)
                        {
                            rg5.Copy(wksheet.Cells[dt.Rows.Count + 5, col]);
                        }
                        rg5.Calculate();
                        wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 5, 1], wksheet.Cells[dt.Rows.Count + 5, 34]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                        wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 5, 1], wksheet.Cells[dt.Rows.Count + 5, 34]).Borders.LineStyle = 1;

                        wksheet.Cells[dt.Rows.Count + 5, 1] = "汇总";
                        Range rghz = wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 5, 1], wksheet.Cells[dt.Rows.Count + 5,3]);
                        rghz.MergeCells = true;
                        rghz.HorizontalAlignment = XlHAlign.xlHAlignRight;
                        //End列汇总

                        //设置列宽
                        wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

                    }
                #endregion
                    currentSheet++;
                }
            }
            #endregion
            //材料明细
            #region
            if (boolArrayExportType[2])
            {
                for (int num = 0; num < list_ProductNum.Count; num++)
                {
                    string sqltext = "select ROW_NUMBER() OVER (ORDER BY CASE WHEN substring(MaterialCode,1,5) like '01.07'  THEN 0 ELSE 1 END, WarehouseCode ASC) AS Row_Num,* from View_SM_OUT where TSAID LIKE '%" + list_ProductNum[num] + "%' and ApprovedDate LIKE '" + list_ProductTime[num] + "%'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    wksheet = (Worksheet)workbook.Sheets[currentSheet];//获取工作表
                    string name = "";
                    if (dt.Rows.Count > 0)
                    {
                        name = list_ProductNum[num].ToString() + list_ProductTime[num].ToString() + "材料明细";// +dt.Rows[0]["ENG"].ToString();生产制号中文
                    }
                    else
                    {
                        name = list_ProductNum[num].ToString() + " 未领料！";
                    }
                    int length = name.Length;
                    if (length > 31)
                    {
                        length = 31;
                    }
                    wksheet.Name = name.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-').Substring(0, length);//工作表名称 //替换 ： / ? * [ 或 ]，最长31
                    // 填充数据
                    object[,] dataArry = new object[dt.Rows.Count, 14];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataArry[i, 0] = dt.Rows[i]["Row_Num"].ToString();
                        dataArry[i, 1] = dt.Rows[i]["ApprovedDate"].ToString().Substring(0, 10);
                        dataArry[i, 2] = dt.Rows[i]["Dep"].ToString();
                        dataArry[i, 3] = dt.Rows[i]["OutCode"].ToString();
                        dataArry[i, 4] = dt.Rows[i]["Warehouse"].ToString();
                        dataArry[i, 5] = dt.Rows[i]["MaterialCode"].ToString();
                        dataArry[i, 6] = dt.Rows[i]["MaterialName"].ToString();
                        dataArry[i, 7] = dt.Rows[i]["Standard"].ToString();
                        dataArry[i, 8] = dt.Rows[i]["Unit"].ToString();
                        dataArry[i, 9] = dt.Rows[i]["RealNumber"].ToString();
                        dataArry[i, 10] = dt.Rows[i]["UnitPrice"].ToString();
                        dataArry[i, 11] = dt.Rows[i]["Amount"].ToString();
                        dataArry[i, 12] = dt.Rows[i]["Doc"].ToString();
                        dataArry[i, 13] = dt.Rows[i]["TSAID"].ToString();


                        //wksheet.Cells[i + 2, 1] = dt.Rows[i]["Row_Num"].ToString();
                        //wksheet.Cells[i + 2, 2] = dt.Rows[i]["ApprovedDate"].ToString().Substring(0, 10);
                        //wksheet.Cells[i + 2, 3] = dt.Rows[i]["Dep"].ToString();
                        //wksheet.Cells[i + 2, 4] = dt.Rows[i]["OutCode"].ToString();
                        //wksheet.Cells[i + 2, 5] = dt.Rows[i]["Warehouse"].ToString();
                        //wksheet.Cells[i + 2, 6] = dt.Rows[i]["MaterialCode"].ToString();
                        //wksheet.Cells[i + 2, 7] = dt.Rows[i]["MaterialName"].ToString();
                        //wksheet.Cells[i + 2, 8] = dt.Rows[i]["Standard"].ToString();
                        //wksheet.Cells[i + 2, 9] = dt.Rows[i]["Unit"].ToString();
                        //wksheet.Cells[i + 2, 10] = dt.Rows[i]["RealNumber"].ToString();
                        //wksheet.Cells[i + 2, 11] = dt.Rows[i]["UnitPrice"].ToString();
                        //wksheet.Cells[i + 2, 12] = dt.Rows[i]["Amount"].ToString();
                        //wksheet.Cells[i + 2, 13] = dt.Rows[i]["Doc"].ToString();
                        //wksheet.Cells[i + 2, 14] = dt.Rows[i]["TSAID"].ToString();
                        //wksheet.get_Range(wksheet.Cells[i + 2, 1], wksheet.Cells[i + 2, 14]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    }
                    wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, 14]).Value2 = dataArry;
                    wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, 14]).HorizontalAlignment = XlHAlign.xlHAlignLeft;

                    //设置列宽
                    wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                    currentSheet++;
                }
            }
            #endregion
            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportDataFromDB.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
        private static void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {
                System.IO.FileInfo path = new System.IO.FileInfo(filename);
                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();

               

                m_xlApp.Application.Quit();

                

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

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

                wksheet = null;
                workbook = null;
                m_xlApp = null;

                GC.Collect();    // 强制垃圾回收 
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpContext.Current.Server.UrlEncode(filename));
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
                //System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                System.Web.HttpContext.Current.Response.WriteFile(filename); // 把文件流发送到客户端 

                System.Web.HttpContext.Current.Response.Flush();
                path.Delete();//删除服务器文件
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 生产制号领料统计数据及明细（按本期，本周，当天等）
        /// </summary>
        public static void ExportMatTotalDetail_ProductNum(string starttime1,string endtime1)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("材料明细表及金额汇总（本周、本期等）") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //导出汇总表
            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            wksheet.Name = "项目金额汇总表";
            int currentSheet=1;
            string sql = "select PJ_NAME+'('+PJ_ID+')',SUM(PS_HSJSJE)+SUM(PS_BZJJE)+SUM(PS_CNPJJE)+SUM(PS_DLJE)+SUM(PS_GJFMJE)+SUM(PS_HCLJE)+SUM(PS_HGXJJE)+SUM(PS_JGJJE)+SUM(PS_MCKYJE)+SUM(PS_RYLJE)+SUM(PS_WGJJE)+SUM(PS_WJCLJE)+SUM(PS_XFQCJE)+SUM(PS_YSJSJE)+SUM(PS_YQTLJE)+SUM(PS_ZPJE)+SUM(PS_ZZCLJE)+SUM(PS_DQDLJE)+SUM(PS_GJJE) from View_TBFM_PRIDSTATISTICS where PS_SCZH!='汇总' group by PJ_ID,PJ_NAME ";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt1.Rows.Count > 0)
            {
                System.Data.DataTable ndt = dt1;
                //定义二维数组
                object[,] dataarry = new object[ndt.Rows.Count, ndt.Columns.Count + 1];

                int rowCount = ndt.Rows.Count;

                int colCount = ndt.Columns.Count;
                // 填充数据
                for (int i = 0; i < rowCount; i++)
                {
                    dataarry[i, 0] = (object)(i + 1);

                    for (int j = 0; j < colCount; j++)
                    {

                        dataarry[i, j + 1] = ndt.Rows[i][j];

                    }
                }

                wksheet.get_Range("A3", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                wksheet.get_Range("A3", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).NumberFormatLocal = "@";

                wksheet.get_Range("A3", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).Value2 = dataarry;
                //设置列宽
                wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            }


            //导出汇总表
            wksheet = (Worksheet)workbook.Sheets.get_Item(2);              
            //当前工作表
            wksheet.Name = "领料金额汇总表";
            currentSheet ++;
            string sqltext = "select  ROW_NUMBER() OVER (ORDER BY PS_SCZH ASC) AS Row_Num,* from View_TBFM_PRIDSTATISTICS where PS_SCZH!='汇总' order by PS_SCZH ASC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                object[,] dataArry = new object[dt.Rows.Count, 24];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataArry[i, 0] = dt.Rows[i]["Row_Num"].ToString();//序号
                    dataArry[i, 1] = dt.Rows[i]["PS_SCZH"].ToString();//生产制号
                    dataArry[i, 2] = dt.Rows[i]["PJ_NAME"].ToString();//项目名称
                    dataArry[i, 3] = dt.Rows[i]["TSA_ENGNAME"].ToString();//工程名称                 
                    dataArry[i, 4] = dt.Rows[i]["PS_HSJSSFSL"].ToString();//黑色金属实发数量
                    dataArry[i, 5] = dt.Rows[i]["PS_HSJSJE"].ToString();//黑色金属金额
                    dataArry[i, 6] = dt.Rows[i]["PS_BZJJE"].ToString();//标准件金额
                    dataArry[i, 7] = dt.Rows[i]["PS_CNPJJE"].ToString();//厂内配件金额
                    dataArry[i, 8] = dt.Rows[i]["PS_DLJE"].ToString();//电料金额
                    dataArry[i, 9] = dt.Rows[i]["PS_GJFMJE"].ToString();//管件阀门金额
                    dataArry[i, 10] = dt.Rows[i]["PS_HCLJE"].ToString();//焊材类金额
                    dataArry[i, 11] = dt.Rows[i]["PS_HGXJJE"].ToString();//化工橡胶金额
                    dataArry[i, 12] = dt.Rows[i]["PS_JGJJE"].ToString();//加工件金额
                    dataArry[i, 13] = dt.Rows[i]["PS_MCKYJE"].ToString();//木材矿窑金额
                    dataArry[i, 14] = dt.Rows[i]["PS_RYLJE"].ToString();//燃油类金额
                    dataArry[i, 15] = dt.Rows[i]["PS_WGJJE"].ToString();//外购件金额
                    dataArry[i, 16] = dt.Rows[i]["PS_WJCLJE"].ToString();//五金材料金额
                    dataArry[i, 17] = dt.Rows[i]["PS_XFQCJE"].ToString();//消防器材金额
                    dataArry[i, 18] = dt.Rows[i]["PS_YSJSJE"].ToString();//有色金属金额
                    dataArry[i, 19] = dt.Rows[i]["PS_YQTLJE"].ToString();//油漆涂料金额
                    dataArry[i, 20] = dt.Rows[i]["PS_ZPJE"].ToString();//杂品金额
                    dataArry[i, 21] = dt.Rows[i]["PS_ZZCLJE"].ToString();//周转材料金额
                    dataArry[i, 22] = dt.Rows[i]["PS_DQDLJE"].ToString();//电气电料金额
                    dataArry[i, 23] = dt.Rows[i]["PS_GJJE"].ToString();//低值易耗金额
                                       
                     
                    //行汇总
                    string col = (i + 4).ToString();
                    string formula2 = "=SUM(F" + col + ",G" + col + ",H" + col + ",I" + col + ",J" + col + ",K" + col + ",L" + col + ",M" + col + ",N" + col + ",O" + col + ",P" + col + ",Q" + col + ",R" + col + ",S" + col + ",T" + col + ",U" + col + ",V" + col + ",W" + col + ",X" + col + ")";

                    Range rg2 = wksheet.get_Range("Y" + col, System.Type.Missing);
                    rg2.Formula = formula2;
                    rg2.Calculate();//小计
                }
                wksheet.get_Range("A4", wksheet.Cells[dt.Rows.Count + 3, 24]).Value2 = dataArry;

           #region 超连接

                //超连接
                //判断有多少个生产制号
                string sql_sczhs1 = "select PS_SCZH from TBFM_PRIDSTATISTICS where PS_SCZH!='汇总' order by PS_SCZH ASC";
                System.Data.DataTable dt_sczhs1 = DBCallCommon.GetDTUsingSqlText(sql_sczhs1);
                if (dt_sczhs1.Rows.Count > 0)
                {

                    for (int num = 0; num < dt_sczhs1.Rows.Count; num++)//生产制号个数
                    {
                        string sql_sczh1 = "select ROW_NUMBER() OVER (ORDER BY MaterialCode ASC) AS Row_Num,* from View_SM_OUT where TSAID  LIKE '%" + dt_sczhs1.Rows[num]["PS_SCZH"] + "%' and ApprovedDate>'" + starttime1 + "' and ApprovedDate<'" + endtime1 + "' order by MaterialCode ASC";
                        System.Data.DataTable dt_sczh = DBCallCommon.GetDTUsingSqlText(sql_sczh1);

                        string name = "";
                        if (dt_sczh.Rows.Count > 0)
                        {
                            name = dt_sczhs1.Rows[num]["PS_SCZH"].ToString();
                        }
                        else
                        {
                            name = dt_sczhs1.Rows[num]["PS_SCZH"].ToString() + " 未领料！";
                        }
                        int length = name.Length;
                        if (length > 31)
                        {
                            length = 31;
                        }
                        string aaa = name.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-').Substring(0, length);//工作表名称 //替换 ： / ? * [ 或 ]，最长31

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("=HYPERLINK(");
                        sb.Append('"');
                        sb.Append('#');
                        sb.Append("'");
                        sb.Append(aaa);
                        sb.Append("'!A1");
                        sb.Append('"');
                        //sb.Append(",A2");
                        sb.Append(",");
                        sb.Append('"');
                        sb.Append(num + 1);
                        sb.Append('"');


                        wksheet.get_Range(wksheet.Cells[num + 4, 1], wksheet.Cells[num + 4, 1]).Formula = sb.ToString();

                    }
                }

             #endregion

            //列汇总
                Range rg3 = wksheet.get_Range("E" + (dt.Rows.Count + 4).ToString(), System.Type.Missing);
                rg3.Formula = "=SUM(E4:E" + (dt.Rows.Count + 3).ToString() + ")";
                for (int col = 4; col <= 25; col++)
                {
                    rg3.Copy(wksheet.Cells[dt.Rows.Count + 4, col]);
                }
                rg3.Calculate();
                wksheet.get_Range(wksheet.Cells[4, 1], wksheet.Cells[dt.Rows.Count + 4, 25]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
                wksheet.get_Range(wksheet.Cells[4, 1], wksheet.Cells[dt.Rows.Count + 4, 25]).Borders.LineStyle = 1;

                wksheet.Cells[dt.Rows.Count + 4, 1] = "汇总";
                Range rghz = wksheet.get_Range(wksheet.Cells[dt.Rows.Count + 4, 1], wksheet.Cells[dt.Rows.Count + 4, 4]);
                rghz.MergeCells = true;
                rghz.HorizontalAlignment = XlHAlign.xlHAlignRight;
                // End列汇总
            }

            currentSheet++;
            //导出材料明细
            //判断有多少个生产制号
            string sql_sczhs = "select PS_SCZH from TBFM_PRIDSTATISTICS where PS_SCZH!='汇总' order by PS_SCZH ASC";
            System.Data.DataTable dt_sczhs = DBCallCommon.GetDTUsingSqlText(sql_sczhs);
            if (dt_sczhs.Rows.Count > 0)
            {
                //复制dt.Rows.Count个Sheet3
                for (int sheetcount = 1; sheetcount < dt_sczhs.Rows.Count; sheetcount++)
                {
                    ((Worksheet)workbook.Worksheets.get_Item(2+sheetcount)).Copy(System.Type.Missing, workbook.Worksheets[2+sheetcount]);
                }

                for (int num = 0; num < dt_sczhs.Rows.Count; num++)//生产制号个数
                {
                    string sql_sczh = "select ApprovedDate,Dep,OutCode,Warehouse,MaterialCode,MaterialName,Standard,Unit,RealNumber,UnitPrice,Amount,Doc,TSAID from View_SM_OUT where TSAID  LIKE '%" + dt_sczhs.Rows[num]["PS_SCZH"] + "%' and ApprovedDate>'" + starttime1 + "' and ApprovedDate<'" + endtime1 + "' and BillType!='3' ORDER BY CASE WHEN substring(MaterialCode,1,5) like '01.07'  THEN 0 ELSE 1 END, WarehouseCode ASC";
                    System.Data.DataTable dt_sczh = DBCallCommon.GetDTUsingSqlText(sql_sczh);
                    wksheet = (Worksheet)workbook.Sheets[currentSheet];//获取工作表
                    string name = "";
                    if (dt_sczh.Rows.Count > 0)
                    {
                        name = dt_sczhs.Rows[num]["PS_SCZH"].ToString();
                    }
                    else
                    {
                        name = dt_sczhs.Rows[num]["PS_SCZH"].ToString() + " 未领料！";
                    }
                    int length = name.Length;
                    if (length > 31)
                    {
                        length = 31;
                    }
                    wksheet.Name = name.Replace(':', '-').Replace('/', '-').Replace('?', '-').Replace('？', '-').Replace('*', '-').Replace('[', '-').Replace(']', '-').Substring(0, length);//工作表名称 //替换 ： / ? * [ 或 ]，最长31


                    if (dt_sczh.Rows.Count > 0)
                    {
                        System.Data.DataTable adt = dt_sczh;
                        //定义二维数组
                        object[,] dataarry = new object[adt.Rows.Count, adt.Columns.Count + 1];

                        int rowCount = adt.Rows.Count;

                        int colCount = adt.Columns.Count;
                        // 填充数据
                        for (int i = 0; i < rowCount; i++)
                        {
                            dataarry[i, 0] = (object)(i + 1);

                            for (int j = 0; j < colCount; j++)
                            {

                                dataarry[i, j + 1] = adt.Rows[i][j];

                            }
                        }

                        wksheet.get_Range("A2", wksheet.Cells[adt.Rows.Count + 1, adt.Columns.Count + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        wksheet.get_Range("A2", wksheet.Cells[adt.Rows.Count + 1, adt.Columns.Count + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                        wksheet.get_Range("A2", wksheet.Cells[adt.Rows.Count + 1, adt.Columns.Count + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        wksheet.get_Range("A2", wksheet.Cells[adt.Rows.Count + 1, adt.Columns.Count + 1]).NumberFormatLocal = "@";

                        wksheet.get_Range("A2", wksheet.Cells[adt.Rows.Count + 1, adt.Columns.Count + 1]).Value2 = dataarry;
                        //设置列宽
                        wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                   
                    }
                    //wksheet.get_Range("A2", wksheet.Cells[dt_sczh.Rows.Count + 1, 14]).Value2 = dataArrys;
                    ////设置列宽
                    //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                    currentSheet++;
                }
            }

            
            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportDataFromDB.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 销售成本统计
        /// </summary>
        public static void ExportSaleCostSta(string jishu, string type, string condition)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("销售出库统计") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //导出汇总表
            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            //当前工作表
            wksheet.Name = "销售成本统计";
            //int currentSheet = 1;
            string sqltext = "";

            if (type == "year")
            {
                if (jishu == "03")
                {
                    sqltext = "SELECT PTC, Substring(Date,1,4) as SLYEAR,''as SLMONTH, MATERIALCODE  AS MTID ,MATERIALNAME AS MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    sqltext += " FROM VIEW_SM_OUTXS " + condition;
                }
                else if (jishu == "02")
                {
                    sqltext = "SELECT PTC, Substring(Date,1,4) as SLYEAR, ''as SLMONTH,TYPEID AS MTID ,TY_NAME AS MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    sqltext += " FROM VIEW_SM_OUTXS " + condition;
                }
                else if (jishu == "01")
                {
                    sqltext = "select PTC, Substring(Date,1,4) as SLYEAR,''as SLMONTH, substring(TYPEID,1,2)AS MTID,(case WHEN substring(TYPEID,1,2)='01' THEN '原材料' when substring(TYPEID,1,2)='01' THEN '低值易耗品' else null end) as MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    sqltext += " FROM VIEW_SM_OUTXS " + condition;
                }
            }
            else if (type == "month")
            {
                if (jishu == "03")
                {
                    sqltext = "SELECT PTC, Substring(Date,1,4) as SLYEAR,Substring(Date,6,2) as SLMONTH, MATERIALCODE AS MTID ,MATERIALNAME AS MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    sqltext += " FROM VIEW_SM_OUTXS " + condition;
                }
                else if (jishu == "02")
                {
                    sqltext = "SELECT PTC, Substring(Date,1,4) as SLYEAR, Substring(Date,6,2) as SLMONTH,TYPEID AS MTID ,TY_NAME AS MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    sqltext += " FROM VIEW_SM_OUTXS " + condition;
                }
                else if (jishu == "01")
                {
                    sqltext = "SELECT PTC, Substring(Date,1,4) as SLYEAR,Substring(Date,6,2) as SLMONTH, substring(TYPEID,1,2)AS MTID,(case WHEN substring(TYPEID,1,2)='01' THEN '原材料' when substring(TYPEID,1,2)='01' THEN '低值易耗品' else null end) as MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    sqltext += " FROM VIEW_SM_OUTXS " + condition;
                }
            }
            if (sqltext != "")
            {
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count+1];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataarry[i, 0] = (i + 1).ToString();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            dataarry[i, j+1] = dt.Rows[i][j].ToString();
                        }
                        //wksheet.Cells[i + 3, 1] = (i + 1).ToString();//序号
                        //wksheet.Cells[i + 3, 2] = dt.Rows[i]["PTC"].ToString();//生产制号
                        //wksheet.Cells[i + 3, 3] = dt.Rows[i]["SLYEAR"].ToString();//年
                        //wksheet.Cells[i + 3, 4] = dt.Rows[i]["SLMONTH"].ToString();//月
                        //wksheet.Cells[i + 3, 5] = dt.Rows[i]["MTID"].ToString();//物料编码
                        //wksheet.Cells[i + 3, 6] = dt.Rows[i]["MTNAME"].ToString();//类别编码
                        //wksheet.Cells[i + 3, 7] = dt.Rows[i]["NUMBER"].ToString();//销售数量
                        //wksheet.Cells[i + 3, 8] = dt.Rows[i]["AMOUNT"].ToString();//销售金额

                        wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 8]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                        wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 8]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    }
                    wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count+1]).Value2 = dataarry;
                }
                
                string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
                ExportDataFromDB.ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            }

        }


        // <summary>
        /// 库存期间查询
        /// </summary>
        public static void ExportKuCunQiJianChaXun(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;

            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("库存期间查询") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            System.Data.DataTable dt = objdt;
            //定义二维数组
            object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count + 1];
            // 填充数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataarry[i, 0] = (i + 1).ToString();
                dataarry[i, 1] = "'" + dt.Rows[i]["SI_MARID"].ToString();//物料代码
                dataarry[i, 2] = "'" + dt.Rows[i]["MNAME"].ToString();//物料名称
                dataarry[i, 3] = "'" + dt.Rows[i]["CAIZHI"].ToString();//材质
                dataarry[i, 4] = "'" + dt.Rows[i]["GUIGE"].ToString();//规格
                dataarry[i, 5] = "'" + dt.Rows[i]["GB"].ToString();//国标
                dataarry[i, 6] = "'" + dt.Rows[i]["PURCUNIT"].ToString();//单位
                dataarry[i, 7] = "'" + dt.Rows[i]["SI_YEAR"].ToString();//年度
                dataarry[i, 8] = "'" + dt.Rows[i]["SI_PERIOD"].ToString();//会计期间
                dataarry[i, 9] = "'" + dt.Rows[i]["SI_BEGNUM"].ToString();//期初数量
                dataarry[i, 10] = "'" + dt.Rows[i]["SI_BEGBAL"].ToString();//期初金额
                dataarry[i, 11] = "'" + dt.Rows[i]["SI_CRCVNUM"].ToString();//收入数量
                dataarry[i, 12] = "'" + dt.Rows[i]["SI_CRCVMNY"].ToString();//收入金额
                dataarry[i, 13] = "'" + dt.Rows[i]["SI_CSNDNUM"].ToString();//发出数量
                dataarry[i, 14] = "'" + dt.Rows[i]["SI_CSNDMNY"].ToString();//发出金额
                dataarry[i, 15] = "'" + dt.Rows[i]["SI_ENDNUM"].ToString();//期末数量
                dataarry[i, 16] = "'" + dt.Rows[i]["SI_ENDBAL"].ToString();//期末金额
                wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 17]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 17]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 17]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }
            wksheet.get_Range("A4", wksheet.Cells[dt.Rows.Count + 3, dt.Columns.Count + 1]).Value2 = dataarry;
            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        // <summary>
        /// 成本调整单查询
        /// </summary>
        public static void ExportchengbentiaozhengdanChaXun(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;

            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("成本调整单查询") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            System.Data.DataTable dt = objdt;
            //定义二维数组
            object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count + 1];
            // 填充数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataarry[i, 0] = (i + 1).ToString();
                dataarry[i, 1] = "'" + dt.Rows[i]["SI_MARID"].ToString();//物料代码
                dataarry[i, 2] = "'" + dt.Rows[i]["MNAME"].ToString();//物料名称
                dataarry[i, 3] = "'" + dt.Rows[i]["CAIZHI"].ToString();//材质
                dataarry[i, 4] = "'" + dt.Rows[i]["GUIGE"].ToString();//规格

                dataarry[i, 5] = "'" + dt.Rows[i]["SI_YEAR"].ToString();//年度
                dataarry[i, 6] = "'" + dt.Rows[i]["SI_PERIOD"].ToString();//会计期间
                dataarry[i, 7] = "'" + dt.Rows[i]["DIFF"].ToString();//差额

                wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 8]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 8]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }
            wksheet.get_Range("A4", wksheet.Cells[dt.Rows.Count + 3, dt.Columns.Count + 1]).Value2 = dataarry;
            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 部门完工确认
        /// </summary>
        internal static void ExportConfirm(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("部门完工确认") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            System.Data.DataTable dt = objdt;
            //定义二维数组
            object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count + 1];

            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;
            // 填充数据
            for (int i = 0; i < rowCount; i++)
            {
                dataarry[i, 0] = (object)(i + 1);

                for (int j = 0; j < colCount; j++)
                {

                    dataarry[i, j + 1] = dt.Rows[i][j];

                }
            }

            wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).NumberFormatLocal = "@";

            wksheet.get_Range("A2", wksheet.Cells[dt.Rows.Count + 1, dt.Columns.Count + 1]).Value2 = dataarry;
            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 库存余额查询
        /// </summary>
        public static void ExportKuCunYue(System.Data.DataTable objdt, string flag)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            if (flag == "MarMonth")
            {
                workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("库存余额按月查询") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Visible = false;    // Excel不显示  
                m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
                wksheet = (Worksheet)workbook.Sheets.get_Item(1);
                System.Data.DataTable dt = objdt;
                //定义二维数组
                object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count + 1];

                int rowCount = dt.Rows.Count;

                int colCount = dt.Columns.Count;
                                // 填充数据
                for (int i = 0; i < rowCount; i++)
                {
                    dataarry[i, 0] = (object)(i + 1);

                    for (int j = 0; j < colCount; j++)
                    {

                        dataarry[i, j + 1] = dt.Rows[i][j];

                    }
                }

                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).NumberFormatLocal = "@";

                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).Value2 = dataarry;
                //设置列宽
                wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
                ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            }
            else if (flag == "MarYear")
            {
                workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("库存余额按年查询") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Visible = false;    // Excel不显示  
                m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
                wksheet = (Worksheet)workbook.Sheets.get_Item(1);
                System.Data.DataTable dt = objdt;
                object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count + 1];
                // 填充数据

                int rowCount = dt.Rows.Count;

                int colCount = dt.Columns.Count;

                // 填充数据
                for (int i = 0; i < rowCount; i++)
                {
                    dataarry[i, 0] = (object)(i + 1);

                    for (int j = 0; j < colCount; j++)
                    {

                        dataarry[i, j + 1] = dt.Rows[i][j];

                    }
                }
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).NumberFormatLocal = "@";


                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).Value2 = dataarry;
                //设置列宽
                wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
                ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            }
            else if (flag == "TypeMonth")
            {
                workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("库存余额按月(级别一，二)查询") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Visible = false;    // Excel不显示  
                m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
                wksheet = (Worksheet)workbook.Sheets.get_Item(1);
                System.Data.DataTable dt = objdt;
                object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count + 1];
                // 填充数据

                int rowCount = dt.Rows.Count;

                int colCount = dt.Columns.Count;

                // 填充数据
                for (int i = 0; i < rowCount; i++)
                {
                    dataarry[i, 0] = (object)(i + 1);

                    for (int j = 0; j < colCount; j++)
                    {

                        dataarry[i, j + 1] = dt.Rows[i][j];

                    }
                }
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).NumberFormatLocal = "@";


                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).Value2 = dataarry;
                //设置列宽
                wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
                ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            }
            else if (flag == "TypeYear")
            {
                workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("库存余额按年查询") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Visible = false;    // Excel不显示  
                m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  
                wksheet = (Worksheet)workbook.Sheets.get_Item(1);
                System.Data.DataTable dt = objdt;
                object[,] dataarry = new object[dt.Rows.Count, dt.Columns.Count + 1];
                // 填充数据

                int rowCount = dt.Rows.Count;

                int colCount = dt.Columns.Count;

                // 填充数据
                for (int i = 0; i < rowCount; i++)
                {
                    dataarry[i, 0] = (object)(i + 1);

                    for (int j = 0; j < colCount; j++)
                    {

                        dataarry[i, j + 1] = dt.Rows[i][j];

                    }
                }
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).NumberFormatLocal = "@";

                wksheet.get_Range("A3", wksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count + 1]).Value2 = dataarry;
                //设置列宽
                wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
                ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            }
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("即使库存明细表模版汇总2") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        }


        public static void Export_Month(string starttime1, string endtime1)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;

            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("任务按月统计") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            m_xlApp.Visible = false; // Excel不显示  
            m_xlApp.DisplayAlerts = false; // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //导出汇总表
            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            wksheet.Name = "Sheet1";
            int currentSheet = 1;
            string sql = "select PMS_SCZH,TSA_PJID,CM_REFER,sum(PPC_GZ) as PPL_GZ,sum(PMC_FTZZFY) as PPL_FTZZFY, sum(PMC_FJ) as PPL_FJ, sum(PMC_YF) as PPL_YF, sum(PMC_CN) as PPL_CN,sum(PMC_WX) as PPL_WX,sum(PMC_QT) as PPL_QT,(sum(PMS_BZJ)+sum(PMS_CNPJ)+sum(PMS_DL)+sum(PMS_GJFM)+sum(PMS_HCL)+sum(PMS_HGXJ)+sum(PMS_HSJS)+sum(PMS_JGJ)+sum(PMS_MCKY)+sum(PMS_RYL)+sum(PMS_WGJ)+sum(PMS_WJCL)+sum(PMS_XFQC)+sum(PMS_YSJS)+sum(PMS_YQTL)+sum(PMS_ZP)+sum(PMS_ZZCL)+sum(PMS_DQDL)+sum(PMS_CLGJ)+sum(PMS_QTGJ)+sum(PMS_DDGJ)+sum(PMS_HJGJ)+sum(PMS_LBYP)+sum(PMS_QDGJ)+sum(PMS_QXGJ)+sum(PMS_QZYY)+sum(PMS_SDGJ)) as PMS_DZYH ";
            sql += "from View_CB_Month_Summary where pms_id>='" + starttime1 + "' and pms_id<='" + endtime1 + "'";
            sql += "group by PMS_SCZH,TSA_PJID,CM_REFER";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt1.Rows.Count > 0)
            {
                System.Data.DataTable ndt = dt1;
                //定义二维数组
                object[,] dataarry = new object[ndt.Rows.Count, ndt.Columns.Count + 1];

                int rowCount = ndt.Rows.Count;

                int colCount = ndt.Columns.Count;
                // 填充数据
                for (int i = 0; i < rowCount; i++)
                {
                    dataarry[i, 0] = (object)(i + 1);

                    for (int j = 0; j < colCount; j++)
                    {
                        dataarry[i, j + 1] = ndt.Rows[i][j];
                    }
                }

                wksheet.get_Range("A2", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range("A2", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range("A2", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                wksheet.get_Range("A2", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).NumberFormatLocal = "@";
                wksheet.get_Range("A2", wksheet.Cells[ndt.Rows.Count + 2, ndt.Columns.Count + 1]).Value2 = dataarry;

                //设置列宽
                wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                string filename = System.Web.HttpContext.Current.Server.MapPath(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
                ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

            }
        }
    }
}
