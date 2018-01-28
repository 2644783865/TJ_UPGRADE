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
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_GongShi_List : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
      
        protected void Page_Load(object sender, EventArgs e)
        {           
            
            if (!IsPostBack)
            {              
                this.GetSqlText();//获取查询条件
                InitVar();
                this.bindRepeater();
            }
            InitVar();
            CheckUser(ControlFinder);
        }
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "TBMP_GS_COL_LIST";
            pager.PrimaryKey = "";
            pager.ShowFields = "Id,GS_CUSNAME,GS_CONTR,GS_TSAID,GS_TSAMONEY,DATEYEAR,DATEMONTH,GS_CHECK";
            pager.OrderField = "DATEYEAR";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = 50;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindRepeater();
        }
        /// <summary>
        /// 绑定PM_GongShi_List_Repeater数据
        /// </summary>
        private void bindRepeater()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, PM_GongShi_List_Repeater, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件  
            }
        }
        private void GetSqlText()
        {
            StringBuilder sqltext = new StringBuilder();
            if (0 == 0)
            { sqltext.Append("0=0"); }
            sqltext.Append(" and GS_CONTR like '%" + txt_search.Text.ToString()+"%'");
            if (ddlgongshiyear.SelectedValue.ToString() != "%")
            {
                sqltext.Append(" and DATEYEAR='" + ddlgongshiyear.SelectedValue.ToString() + "'");
            }
            if (ddlgongshimonth.SelectedValue.ToString() != "%")
            {
                sqltext.Append(" and DATEMONTH='" + ddlgongshimonth.SelectedValue.ToString() + "'");
            }
            sqltext.Append(" and IsDel='0'");
            ViewState["sqlText"] = sqltext.ToString();
        }
        protected void PM_GongShi_List_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label GS_TSAID = e.Item.FindControl("GS_TSAID") as System.Web.UI.WebControls.Label;
                if (check_state(GS_TSAID.Text))
                {
                    HtmlTableCell num = (HtmlTableCell)e.Item.FindControl("num");
                    num.BgColor = "#FF0000";
                }
            }
        }
        private bool check_state(string GS_TSAID)
        {
            bool yesorno = false;
            string sqltext = "select * from TBMP_MANUTSASSGN where MTA_ID='" + GS_TSAID + "'";
            System.Data.DataTable dt=DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count==0)
            {
                yesorno = true;
            }
            return yesorno;
        }
        protected string showYg(string GS_CUSNAME, string GS_CONTR, string GS_TSAID, string DATEYEAR, string DATEMONTH)
        {
            //return "javascript:window.showModalDialog('PM_GongShi_edit.aspx?action=edit&&Id=" + Id + "','','DialogWidth=800px;DialogHeight=700px')";
            //return "javascript:window.showModalDialog('PM_GongShi_Detail_List.aspx?customerName=" + GS_CUSNAME + "&contractNum=" + GS_CONTR + "&tsaId=" + GS_TSAID + "&year=" + DATEYEAR + "&month=" + DATEMONTH + "','','DialogWidth=800px;DialogHeight=700px')";
            string str="javascript:window.location='PM_GongShi_Detail_List.aspx?customerName=" + GS_CUSNAME + "&contractNum=" + GS_CONTR + "&tsaId=" + GS_TSAID + "&year=" + DATEYEAR + "&month=" + DATEMONTH+"'";
            return str;
        }
        public string get_yyyymm(string i, string j)
        {
            string state = "";
            state = i + '.' + j;
            return state;
        }      
        protected void ddlgongshiyear_SelectedIndexChanged(object sender, EventArgs e)
        {            
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }
        protected void ddlgongshimonth_SelectedIndexChanged(object sender, EventArgs e)
        {            
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }
        /// <summary>
        /// Excel表导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void daochu_Click(object sender, EventArgs e)
        {
            int i = 0;
            string path = Server.MapPath("/PM_Data/ExportFile");


            if (ddlgongshiyear.SelectedValue !="&" && ddlgongshimonth.SelectedValue != "&")
            {
                if (!Directory.Exists(path))//创建文件夹
                {
                    Directory.CreateDirectory(path);
                }
                foreach (string fileName in Directory.GetFiles(path))
                {
                    string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    System.IO.File.Delete(path + "\\" + newName);//删除文件下储存的文件
                }
                string sqltext = "select GS_CUSNAME,GS_CONTR,GS_TSAID,GS_TUHAO,GS_TUMING,GS_EQUID,GS_EQUNAME,GS_EQUFACTOR,GS_HOURS,GS_MONEY,GS_NOTE from TBMP_GS_LIST where DATEYEAR like'" + ddlgongshiyear.SelectedValue.ToString() + "' and DATEMONTH like'" + ddlgongshimonth.SelectedValue.ToString() + "' and IsDel='0'";
                ExportDataItem(sqltext, ddlgongshiyear.SelectedValue.ToString(), ddlgongshimonth.SelectedValue.ToString());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要导出工时的年份及月份！');", true);
            }
        }
        private void ExportDataItem(string sqltext, string year, string month)
        {
            string filename = "工时汇总表.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            int dateyear = Convert.ToInt16(ddlgongshiyear.SelectedValue.ToString());
            int datemonth =Convert.ToInt16( ddlgongshimonth.SelectedValue.ToString());
            int dateyear1;
            int datemonth1;
            if (datemonth == 1)
            {
                dateyear1 = dateyear - 1;
                datemonth1 = 12;
            }
            else
            {
               dateyear1=dateyear;
               datemonth1 = datemonth - 1;
            }
            string biaotou = " "+dateyear1+"年"+datemonth1+"月21日-" + dateyear + "年" + datemonth + "月20日项目工时汇总 ";
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("工时汇总表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
               
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    //System.Web.HttpContext.Current.
                    Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }

                //string basic_sql = "select MS_PJID, MS_ENGID,MS_PJNAME, MS_ENGNAME,MS_MAP from " + revtable + " where MS_ID='" + lotnum + "'";
                //System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(basic_sql);
                //IRow row2 = sheet0.GetRow(2);
                //row2.GetCell(1).SetCellValue(dt1.Rows[0]["MS_PJNAME"].ToString());
                //row2.GetCell(3).SetCellValue(dt1.Rows[0]["MS_PJID"].ToString());
                //row2.GetCell(5).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                //IRow row3 = sheet0.GetRow(3);
                //row3.GetCell(1).SetCellValue(dt1.Rows[0]["MS_ENGID"].ToString());
                //IRow row4 = sheet0.GetRow(4);
                //row4.GetCell(1).SetCellValue(dt1.Rows[0]["MS_MAP"].ToString());
                    

                else
                {
                   IRow row0=sheet0.GetRow(0);
                    row0.GetCell(0).SetCellValue(biaotou);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet0.CreateRow(i+2);
                        row.CreateCell(0).SetCellValue(i+1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                }
                sheet0.ForceFormulaRecalculation = true;
            

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();


                //Object Opt = System.Type.Missing;
                //Application m_xlApp = new Application();
                //Workbooks workbooks = m_xlApp.Workbooks;
                //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                //Worksheet wksheet;
                //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("工时汇总") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

                //m_xlApp.Visible = false;     // Excel不显示  
                //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

                //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

                //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

                ////设置工作薄名称
                ////string name = year + month + "工时汇总";        
                ////wksheet.Name = name;
                //int rowCount = dt.Rows.Count;
                //int colCount = dt.Columns.Count;          
                //for (int i = 0; i < rowCount; i++)
                //{
                //    wksheet.Cells[i + 2, 1] = Convert.ToString(i + 1);//序号
                //    for (int j = 0; j < 8; j++)
                //    {
                //        wksheet.Cells[i + 2, j + 2] = "'" + dt.Rows[i][j].ToString();
                //    }
                //    wksheet.get_Range(wksheet.Cells[i + 2, 1], wksheet.Cells[i + 2, 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //    wksheet.get_Range(wksheet.Cells[i + 2, 1], wksheet.Cells[i + 2, 9]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                //    wksheet.get_Range(wksheet.Cells[i + 2, 1], wksheet.Cells[i + 2, 9]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //}
                //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应


                //string filename = System.Web.HttpContext.Current.Server.MapPath("~/PM_Data/ExportFile/工时汇总" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
                //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            }
        }

        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet) //输出Excel文件并退出
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
                contextResponse.Redirect(string.Format("~/PM_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void btnSC_Click(object sender, EventArgs e)
        {
            string sqldelete1 = "delete from TBMP_GS_COL_LIST where DATEYEAR='" + ddlgongshiyear.SelectedValue.ToString() + "' and DATEMONTH='" + ddlgongshimonth.SelectedValue.ToString() + "'";
            string sqldelete2 = "delete from TBMP_GS_LIST where DATEYEAR='" + ddlgongshiyear.SelectedValue.ToString() + "' and DATEMONTH='" + ddlgongshimonth.SelectedValue.ToString() + "'";            
            DBCallCommon.ExeSqlText(sqldelete1);
            DBCallCommon.ExeSqlText(sqldelete2);
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }

        protected void btn_search_OnClick(object sender, EventArgs e)
        {
            this.GetSqlText();       
            InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }
    }
}
