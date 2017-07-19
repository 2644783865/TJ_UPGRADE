using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace ZCZJ_DPF.Contract_Data
{
    public class ContractClass
    {
        /// <summary>
        /// 根据操作状态设置附件上传控件（上传、删除）
        /// </summary>
        /// <param name="at"></param>
        /// <param name="op"></param>
        public static void UploadControlSet(ZCZJ_DPF.Controls.UploadAttachments at, string op)
        {
            if(op=="View")
            {
                at.FindControl("btnUpload").Visible = false;
                GridView gridView1 = (GridView)at.FindControl("GridView1");
                gridView1.Columns[4].Visible = false;

                //查看时隐藏文件描述行和上传按钮行，只显示文件信息行
                HtmlTableRow tr_wjms = (HtmlTableRow)at.FindControl("tr_WJMS");
                HtmlTableRow tr_upload = (HtmlTableRow)at.FindControl("tr_Upload");
                tr_wjms.Visible = false;
                tr_upload.Visible = false;
            }
        }

        public static void UploadControlSet(ZCZJ_DPF.Controls.UploadFiles at, string op)
        {
            if (op == "View")
            {
                at.FindControl("btnUpload").Visible = false;
                GridView gridView1 = (GridView)at.FindControl("GridView1");
                gridView1.Columns[3].Visible = false;

                //查看时隐藏文件描述行和上传按钮行，只显示文件信息行                
                HtmlTableRow tr_upload = (HtmlTableRow)at.FindControl("tr_Upload");               
                tr_upload.Visible = false;
            }
        }

       
        /// <summary>
        /// 初始化附件上传控件(附件控件，Label合同号，索赔类别)
        /// </summary>
        /// <param name="at_control"></param>
        /// <param name="txt"></param>
        /// <param name="sp_type"></param>
        public static void InitUploadControls(ZCZJ_DPF.Controls.UploadAttachments at_control, System.Web.UI.WebControls.Label txt, int sp_type)
        {
            at_control.at_htbh = txt.Text;
            if (at_control.at_htbh == "")
            {
                at_control.Visible = false;//如何合同编号不存在则无法显示Upload控件
            }
            else
            {
                at_control.Visible = true;
                at_control.at_sp = sp_type;//0:业主；1、2：重机索赔；3：分包商
                at_control.at_type = 1;
                at_control.InitData();//重新绑定Upload数据
            }
        }
        /// <summary>
        /// 部门绑定
        /// </summary>
        public static void BindDep(DropDownList dplQKBM)
        {
            string sqltext = "select DEP_NAME,DEP_CODE from  TBDS_DEPINFO where DEP_FATHERID='0'and DEP_CODE!='01'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplQKBM.DataSource = dt;
            dplQKBM.DataTextField = "DEP_NAME";
            dplQKBM.DataValueField = "DEP_CODE";
            dplQKBM.DataBind();
            dplQKBM.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplQKBM.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定制作班组分包商
        /// </summary>
        /// <param name="dplBZFBS"></param>
        public static void BindBZFBS(DropDownList dplBZFBS)
        {
            string sqltext = "SELECT [DEP_CODE] ,[DEP_NAME] FROM [dbo].[TBDS_DEPINFO] WHERE [DEP_FATHERID]='04'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplBZFBS.DataSource = dt;
            dplBZFBS.DataTextField = "DEP_NAME";
            dplBZFBS.DataValueField = "DEP_CODE";
            dplBZFBS.DataBind();
            dplBZFBS.Items.Insert(0, new ListItem("-请选择-", ""));
            dplBZFBS.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定项目名称
        /// </summary>
        public static void BindPrjName(DropDownList cmbpj)
        {
            cmbpj.Items.Clear();
            string sqlText = "select PJ_ID+'/'+PJ_NAME as PJ_NAME,PJ_ID from TBPM_PJINFO";//随着项目的增多，下拉框数据多，考虑将项目是否完工加入查询条件
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            cmbpj.DataSource = dt;
            cmbpj.DataTextField = "PJ_NAME";
            cmbpj.DataValueField = "PJ_ID";
            cmbpj.DataBind();
            cmbpj.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            cmbpj.SelectedIndex = 0;
        }
 
       
        /// <summary>
        /// 绑定合同号
        /// /// </summary>
       
        public static void BindHTH(DropDownList cmbp)
        {
            cmbp.Items.Clear();
            string sqlText = "select PCON_BCODE from TBPM_CONPCHSINFO";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            cmbp.DataSource = dt;
            cmbp.DataTextField = "PCON_BCODE";
            cmbp.DataValueField = "PCON_BCODE";
            cmbp.DataBind();
            cmbp.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            cmbp.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定索赔原因
        /// </summary>
        /// <param name="cbl"></param>
        public static void BindSP_Reason(CheckBoxList cbl)
        {
            cbl.Items.Clear();
            string sqltext = "select SPR_ID,SPR_DESCRIBLE from TBPM_REASONCONTROL";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cbl.DataSource = dt;
            cbl.DataTextField = "SPR_DESCRIBLE";
            cbl.DataValueField = "SPR_ID";
            cbl.DataBind();
        }
     
        /// <summary>
        /// 由于主合同不存在索赔记录，需要添加
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string SubOperateMain(string spbh, string xmbh, string mhtbh, int type)
        {
            //	1	索赔编号	
            string spm_id = spbh;
            //	2	项目编号
            string spm_xmbh = xmbh;
            //	3	主合同编号	
            string spm_htbh = mhtbh;
            //	4	接受部门	
            int spm_jsbm = 0;
            //	5	索赔问题描述	
            string spm_spwtmx = "";
            //	6	索赔金额	
            decimal spm_spje = 0;
            //	7	最终索赔金额	
            decimal spm_zzspje = 0;
            //	8	索赔登记日期	
            string spm_spdjrq = "";
            //	9	是否扣款	
            int spm_sfkk = 1;
            //	10	扣款日期	
            string spm_kkrq = DateTime.Now.ToShortDateString();
            //	11	扣款备注	
            string spm_kkbz = "";
            //	12	技术负责人	
            string spm_jsfzr = "";
            //	13	质量负责人	
            string spm_zlfzr = "";
            //	14	制作方	
            string spm_zzf = "";
            //	15	是否处理	
            int spm_sscl = 1;
            //	16	是否回复	
            int spm_sshf = 1;
            //	17	回复意见	
            string spm_hfyj = "";
            //	18	反馈人	
            string spm_fkr = "";
            //	19	反馈日期	
            string spm_fkrq = "";
            //	20	内部处理意见	
            string spm_nbclyj = "";
            //	21	问题描述	
            string spm_wtms = "";
            //	22	备注	
            string spm_bz = "";
            //	23	负责部门	
            string spm_fzbm = "";
            //	24	索赔类别	
            int spm_splb = type;
            string sqltext = "insert into TBPM_MAINCLAIM(SPM_ID,SPM_XMBH,SPM_HTBH,SPM_JSBM,SPM_SPWTMX,SPM_SPJE,SPM_ZZSPJE,SPM_SPDJRQ,SPM_SFKK,SPM_KKRQ,SPM_KKBZ,SPM_JSFZR,SPM_ZLFZR,SPM_ZZF,SPM_SSCL,SPM_SSHF,SPM_HFYJ,SPM_FKR,SPM_FKRQ,SPM_NBCLYJ,SPM_WTMS,SPM_BZ,SPM_FZBM,SPM_SPLB) " +
                         "Values('" + spm_id + "','" + spm_xmbh + "','" + spm_htbh + "'," + spm_jsbm + ",'" + spm_spwtmx + "','" + spm_spje + "','" + spm_zzspje + "','" + spm_spdjrq + "'," + spm_sfkk + ",'" + spm_kkrq + "','" + spm_kkbz + "','" + spm_jsfzr + "','" + spm_zlfzr + "','" + spm_zzf + "'," + spm_sscl + "," + spm_sshf + ",'" + spm_hfyj + "','" + spm_fkr + "','" + spm_fkrq + "','" + spm_nbclyj + "','" + spm_wtms + "',	'" + spm_bz + "','" + spm_fzbm + "'," + spm_splb + ")";
            return sqltext;
        }


        //删除合同附件
        public static void Del_ConAttachment(string htbh)
        {
            //删除对应的文件，用唯一编号关联
            //一个合同可能对应多个附件，要循环删除
            string sql_atfilepath = "select AT_FILEPATH from TBPM_ATTACHMENTS where AT_HTBH=(" +
                                "select GUID from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "')";
            System.Data.DataTable dt_atfilepath = DBCallCommon.GetDTUsingSqlText(sql_atfilepath);
            for (int i = 0; i < dt_atfilepath.Rows.Count; i++)
            {
                string fileName = dt_atfilepath.Rows[i]["AT_FILEPATH"].ToString();
                //string fileName = DBCallCommon.GetFieldValue(dt_atfilepath.Rows[i]["AT_FILEPATH"].ToString());
                string attachPath = @"E:/合同管理附件";//附件上传位置
                string proj_type = "合同文档";
                string filepath = CommonFun.CreateDirName(attachPath, proj_type) + fileName;

                //判断文件是否存在，如果不存在提示重新上传
                if (System.IO.File.Exists(filepath))
                {
                    DBCallCommon.DeleteFile(filepath);
                    //重新读出附件信息

                }                
            }
             DBCallCommon.ExeSqlText("delete from TBPM_ATTACHMENTS where AT_HTBH=(" +
                             "select GUID from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "')");
        }
 
        //删除合同索赔附件
        public static void Del_SPAttachment(string htbh)
        {
            //删除对应的文件，用唯一编号关联
            //一个合同可能对应多个索赔单号，一个索赔单号可能对应多个附件
            string sql_atfilepath = "select AT_FILEPATH from TBPM_ATTACHMENTS where AT_HTBH in (" +
                                    "select GUID from TBPM_SUBCLAIM where SPS_HTBH='" + htbh + "')";
            System.Data.DataTable dt_atfilepath = DBCallCommon.GetDTUsingSqlText(sql_atfilepath);
            for (int i = 0; i < dt_atfilepath.Rows.Count; i++)
            {
                string fileName = dt_atfilepath.Rows[i]["AT_FILEPATH"].ToString();
                //string fileName = DBCallCommon.GetFieldValue(dt_atfilepath.Rows[i]["AT_FILEPATH"].ToString());
                string attachPath = @"E:/合同管理附件";//附件上传位置
                string proj_type = "索赔文档";
                string filepath = CommonFun.CreateDirName(attachPath, proj_type) + fileName;

                //判断文件是否存在，如果不存在提示重新上传
                if (System.IO.File.Exists(filepath))
                {
                    DBCallCommon.DeleteFile(filepath);
                    //重新读出附件信息

                }
            }
            DBCallCommon.ExeSqlText("delete from TBPM_ATTACHMENTS where AT_HTBH in(" +
                         "select GUID from TBPM_SUBCLAIM where SPS_HTBH='" + htbh + "')");
        }


        //删除合同评审（补充协议）附件
        public static void Del_RevAttachment(string psdh)
        {
            //删除对应的文件，用唯一编号关联
            //一个合同可能对应多个附件，要循环删除
            string sql_atfilepath = "select AT_FILEPATH from TBPM_ATTACHMENTS where AT_HTBH='"+psdh+"'";
            System.Data.DataTable dt_atfilepath = DBCallCommon.GetDTUsingSqlText(sql_atfilepath);
            for (int i = 0; i < dt_atfilepath.Rows.Count; i++)
            {
                string fileName = dt_atfilepath.Rows[i]["AT_FILEPATH"].ToString();
                //string fileName = DBCallCommon.GetFieldValue(dt_atfilepath.Rows[i]["AT_FILEPATH"].ToString());
                string attachPath = @"E:/合同管理附件";//附件上传位置
                string proj_type = "评审合同文档";
                string filepath = CommonFun.CreateDirName(attachPath, proj_type) + fileName;

                //判断文件是否存在，如果不存在提示重新上传
                if (System.IO.File.Exists(filepath))
                {
                    DBCallCommon.DeleteFile(filepath);
                    //重新读出附件信息

                }
            }
            DBCallCommon.ExeSqlText("delete from TBPM_ATTACHMENTS where AT_HTBH='" + psdh + "'");
        }


        //导出合同信息
        public static void ExportDataItem(System.Data.DataTable objdt)
        {

            //导出前先把以前导出过的记录从服务器文件夹中删除掉，避免该文件夹越来越大占用大量资源
            //string folderpath = "ZCZJ_DPF/Contract_Data/ExportFile/";
            //foreach (string str in System.IO.Directory.GetFiles(folderpath))
            //{
            //    FileInfo info = new FileInfo(str);
            //    info.Attributes = FileAttributes.Normal;
            //    info.Delete();
            //}

            System.Data.DataTable dt = objdt;

            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("合同信息模板") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            

            //用数组填充表格
            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count-3;

            object[,] dataArray = new object[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                //dataArray[i, 0] = (i + 1).ToString();不要前面的序号填充列
                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j ] = dt.Rows[i][j];

                }

            }
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount+1, colCount]).Value2 = dataArray;
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount+1, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = System.Web.HttpContext.Current.Server.MapPath("/Contract_Data/ExportFile/" + "合同信息" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        //导出售后服务
        public static void ExportSHFWDataItem(System.Data.DataTable objdt)
        {

            //导出前先把以前导出过的记录从服务器文件夹中删除掉，避免该文件夹越来越大占用大量资源
            //string folderpath = "ZCZJ_DPF/Contract_Data/ExportFile/";
            //foreach (string str in System.IO.Directory.GetFiles(folderpath))
            //{
            //    FileInfo info = new FileInfo(str);
            //    info.Attributes = FileAttributes.Normal;
            //    info.Delete();
            //}

            System.Data.DataTable dt = objdt;

            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("售后服务模板") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);



            //用数组填充表格
            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            object[,] dataArray = new object[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                //dataArray[i, 0] = (i + 1).ToString();不要前面的序号填充列
                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j] = dt.Rows[i][j];

                }

            }
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).Value2 = dataArray;
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = System.Web.HttpContext.Current.Server.MapPath("/Contract_Data/ExportFile/" + "售后服务信息" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
        //导出销售合同信息
        public static void ExportXSDataItem(System.Data.DataTable objdt)
        {

            System.Data.DataTable dt = objdt;

            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("销售合同信息模板") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);



            //用数组填充表格
            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count - 1;

            object[,] dataArray = new object[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                //dataArray[i, 0] = (i + 1).ToString();不要前面的序号填充列
                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j] = dt.Rows[i][j];

                }

            }
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).Value2 = dataArray;
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = System.Web.HttpContext.Current.Server.MapPath("/Contract_Data/ExportFile/" + "销售合同信息" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        //导出请款单
        public static void ExportQKDataItem(System.Data.DataTable objdt)
        {

            System.Data.DataTable dt = objdt;

            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("请款单模板") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);



            //用数组填充表格
            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            object[,] dataArray = new object[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                //dataArray[i, 0] = (i + 1).ToString();不要前面的序号填充列
                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j] = dt.Rows[i][j];

                }

            }
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).Value2 = dataArray;
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = System.Web.HttpContext.Current.Server.MapPath("/Contract_Data/ExportFile/" + "请款单信息" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        //导出采购订单请款
        public static void ExportDDQKDataItem(System.Data.DataTable objdt)
        {
            System.Data.DataTable dt = objdt;

            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购订单请款模板") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            //用数组填充表格
            int rowCount = dt.Rows.Count;
            int colCount = dt.Columns.Count;
            object[,] dataArray = new object[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    dataArray[i, j] = dt.Rows[i][j];
                }
            }
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).Value2 = dataArray;
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = System.Web.HttpContext.Current.Server.MapPath("/Contract_Data/ExportFile/" + "采购订单请款信息" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        //导出付款记录
        public static void ExportFKDataItem(System.Data.DataTable objdt)
        {

            System.Data.DataTable dt = objdt;

            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("付款记录模板") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);



            //用数组填充表格
            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            object[,] dataArray = new object[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                //dataArray[i, 0] = (i + 1).ToString();不要前面的序号填充列
                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j] = dt.Rows[i][j];

                }

            }
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).Value2 = dataArray;
            m_xlApp.get_Range("A2", m_xlApp.Cells[rowCount + 1, colCount]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = System.Web.HttpContext.Current.Server.MapPath("/Contract_Data/ExportFile/" + "付款记录" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
        public static void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();

                //下载

                System.IO.FileInfo path = new System.IO.FileInfo(filename);

                //同步，异步都支持
                HttpResponse contextResponse = HttpContext.Current.Response;
                contextResponse.Redirect(string.Format("~/Contract_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
     
    }
}
