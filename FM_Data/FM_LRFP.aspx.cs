using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using ZCZJ_DPF;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_LRFP : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }

            if (IsPostBack)
            {
                this.InitVar();
            }
            CheckUser(ControlFinder);
        }
        //初始化页面
        private void InitPage()
        {
            //显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }
            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }//
        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "TBFM_LRFP";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "RQBH";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 30;
        }

        private string strstring()
        {
            string sqlText = "";
            if (dplYear.SelectedValue.Trim().ToString() == "-请选择-" && dplMoth.SelectedValue.Trim().ToString() == "-请选择-")
            {
                sqlText = "1=1";
            }
            else if (dplYear.SelectedValue.Trim().ToString() != "-请选择-" && dplMoth.SelectedValue.Trim().ToString() == "-请选择-")
            {
                sqlText = "RQBH like'" + dplYear.SelectedValue.Trim().ToString()+"-%'";
            }
            else if (dplYear.SelectedValue.Trim().ToString() == "-请选择-" && dplMoth.SelectedValue.Trim().ToString() != "-请选择-")
            {
                sqlText = "RQBH like'%-" + dplMoth.SelectedValue.Trim().ToString() + "%'";
            }
            else
            {
                sqlText += "RQBH like'" + dplYear.SelectedValue.Trim().ToString() + "-" + dplMoth.SelectedValue.Trim().ToString() + "%'";
            }
            return sqlText;
        }


        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
            CheckUser(ControlFinder);

        }
        #endregion

        protected void btnSC_Click(object sender, EventArgs e)
        {
            CommonFun.delMult("TBFM_LRFP", "ID", rptProNumCost);
            bindGrid();
            Response.Redirect(Request.Url.ToString());
        }
        protected string editDq(string DqId)
        {
            return "javascript:window.showModalDialog('FM_LRFP_Detail.aspx?action=update&id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
        }
        protected string viewDq(string DqId)
        {
            return "javascript:window.showModalDialog('FM_LRFP_Detail.aspx?action=look&id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
        }

        protected void btnCX_Click(object sender, EventArgs e)
        {
            this.InitVar();
            this.bindGrid();
        }

        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }
        /// <summary>
        /// 年、月份改变 查询
        /// </summary>
        protected void OnSelectedIndexChanged_dplYearMoth(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }


        protected void btn_Import_Click_Click(object sender, EventArgs e)
        {
            List<string> listc = new List<string>();
            List<string> listm = new List<string>();
            string FilePath = @"E:\利润及利润分配表\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //将文件上传到服务器
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型   
                if (fileContentType == "application/vnd.ms-excel")
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        UserHPF.SaveAs(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName));//将上传的文件存放在指定的文件夹中 
                    }
                }
                else
                {
                    Response.Write("<script>alert('文件类型不符合要求，请您核对后重新上传！');</script>"); return;
                }
            }
            catch
            {
                Response.Write("<script>alert('文件上传过程中出现错误！');</script>"); return;
            }

            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
                //根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);

                //获取第一个工作表
                ISheet sheet = wk.GetSheetAt(0);

                //循环读取每一行数据，由于execel有列名以及序号，从1开始
                string sqlc = "";
                string sqlm = "";
                string ncs = "本期数";
                string qms = "本年累计数";
                IRow row1 = sheet.GetRow(44);
                ICell cell0 = row1.GetCell(0);
                string rqbhc = cell0.StringCellValue.ToString().Trim();
                string rqbhm = cell0.StringCellValue.ToString().Trim();//得到修改人员编码
                string sqlTextchk = "select * from TBFM_LRFP where RQBH='" + cell0.StringCellValue.ToString().Trim() + "'";
                System.Data.DataTable dtchk = DBCallCommon.GetDTUsingSqlText(sqlTextchk);
                if (dtchk.Rows.Count > 0)
                {
                    Response.Write("<script>alert('日期编号已存在！');</script>"); return;
                }
                sqlc = "'" + rqbhc + "','" + ncs + "'";
                sqlm = "'" + rqbhm + "','" + qms + "'";

                IRow row4 = sheet.GetRow(47);
                ICell cell24 = row4.GetCell(2);
                ICell cell34 = row4.GetCell(3);
                sqlc += ",'" + cell24.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell34.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row5 = sheet.GetRow(48);
                ICell cell25 = row5.GetCell(2);
                ICell cell35 = row5.GetCell(3);
                sqlc += ",'" + cell25.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell35.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row6 = sheet.GetRow(49);
                ICell cell26 = row6.GetCell(2);
                ICell cell36 = row6.GetCell(3);
                sqlc += ",'" + cell26.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell36.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row7 = sheet.GetRow(50);
                ICell cell27 = row7.GetCell(2);
                ICell cell37 = row7.GetCell(3);
                sqlc += ",'" + cell27.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell37.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row8 = sheet.GetRow(51);
                ICell cell28 = row8.GetCell(2);
                ICell cell38 = row8.GetCell(3);
                sqlc += ",'" + cell28.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell38.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row9 = sheet.GetRow(52);
                ICell cell29 = row9.GetCell(2);
                ICell cell39 = row9.GetCell(3);
                sqlc += ",'" + cell29.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell39.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row10 = sheet.GetRow(53);
                ICell cell210 = row10.GetCell(2);
                ICell cell310 = row10.GetCell(3);
                sqlc += ",'" + cell210.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell310.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row11 = sheet.GetRow(54);
                ICell cell211 = row11.GetCell(2);
                ICell cell311 = row11.GetCell(3);
                sqlc += ",'" + cell211.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell311.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row12 = sheet.GetRow(55);
                ICell cell212 = row12.GetCell(2);
                ICell cell312 = row12.GetCell(3);
                sqlc += ",'" + cell212.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell312.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row13 = sheet.GetRow(56);
                ICell cell213 = row13.GetCell(2);
                ICell cell313 = row13.GetCell(3);
                sqlc += ",'" + cell213.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell313.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row14 = sheet.GetRow(57);
                ICell cell214 = row14.GetCell(2);
                ICell cell314 = row14.GetCell(3);
                sqlc += ",'" + cell214.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell314.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row15 = sheet.GetRow(58);
                ICell cell215 = row15.GetCell(2);
                ICell cell315 = row15.GetCell(3);
                sqlc += ",'" + cell215.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell315.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row16 = sheet.GetRow(58);
                ICell cell216 = row16.GetCell(2);
                ICell cell316 = row16.GetCell(3);
                sqlc += ",'" + cell216.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell316.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row17 = sheet.GetRow(60);
                ICell cell217 = row17.GetCell(2);
                ICell cell317 = row17.GetCell(3);
                sqlc += ",'" + cell217.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell317.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row18 = sheet.GetRow(61);
                ICell cell218 = row18.GetCell(2);
                ICell cell318 = row18.GetCell(3);
                sqlc += ",'" + cell218.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell318.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row19 = sheet.GetRow(62);
                ICell cell219 = row19.GetCell(2);
                ICell cell319 = row19.GetCell(3);
                sqlc += ",'" + cell219.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell319.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row20 = sheet.GetRow(63);
                ICell cell220 = row20.GetCell(2);
                ICell cell320 = row20.GetCell(3);
                sqlc += ",'" + cell220.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell320.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row21 = sheet.GetRow(64);
                ICell cell221 = row21.GetCell(2);
                ICell cell321 = row21.GetCell(3);
                sqlc += ",'" + cell221.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell321.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row22 = sheet.GetRow(65);
                ICell cell222 = row22.GetCell(2);
                ICell cell322 = row22.GetCell(3);
                sqlc += ",'" + cell222.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell322.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row23 = sheet.GetRow(66);
                ICell cell223 = row23.GetCell(2);
                ICell cell323 = row23.GetCell(3);
                sqlc += ",'" + cell223.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell323.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row24 = sheet.GetRow(67);
                ICell cell224 = row24.GetCell(2);
                ICell cell324 = row24.GetCell(3);
                sqlc += ",'" + cell224.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell324.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row27 = sheet.GetRow(70);
                ICell cell327 = row27.GetCell(3);
                sqlm += ",'" + cell327.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row28 = sheet.GetRow(71);
                ICell cell328 = row28.GetCell(3);
                sqlm += ",'" + cell328.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row29 = sheet.GetRow(72);
                ICell cell329 = row29.GetCell(3);
                sqlm += ",'" + cell329.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row30 = sheet.GetRow(73);
                ICell cell230 = row30.GetCell(2);
                ICell cell330 = row30.GetCell(3);
                sqlm += ",'" + cell330.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row31 = sheet.GetRow(74);
                ICell cell331 = row31.GetCell(3);
                sqlm += ",'" + cell331.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row32 = sheet.GetRow(75);
                ICell cell332 = row32.GetCell(3);
                sqlm += ",'" + cell332.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row33 = sheet.GetRow(76);
                ICell cell333 = row33.GetCell(3);
                sqlm += ",'" + cell333.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row34 = sheet.GetRow(77);
                ICell cell334 = row34.GetCell(3);
                sqlm += ",'" + cell334.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row35 = sheet.GetRow(78);
                ICell cell235 = row35.GetCell(2);
                ICell cell335 = row35.GetCell(3);
                sqlm += ",'" + cell335.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row36 = sheet.GetRow(79);
                ICell cell336 = row36.GetCell(3);
                sqlm += ",'" + cell336.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row37 = sheet.GetRow(80);
                ICell cell237 = row37.GetCell(2);
                ICell cell337 = row37.GetCell(3);
                sqlm += ",'" + cell337.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row38 = sheet.GetRow(81);
                ICell cell338 = row38.GetCell(3);
                sqlm += ",'" + cell338.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row39 = sheet.GetRow(82);
                ICell cell239 = row39.GetCell(2);
                ICell cell339 = row39.GetCell(3);
                sqlm += ",'" + cell339.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row40 = sheet.GetRow(83);
                ICell cell340 = row40.GetCell(3);
                sqlm += ",'" + cell340.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row41 = sheet.GetRow(84);
                ICell cell341 = row41.GetCell(3);
                sqlm += ",'" + cell341.NumericCellValue.ToString("0.00").Trim() + "'";

                #region 存入数据库

                string sqlTxtc = string.Format("insert into TBFM_LRFP({0}) values({1})", "RQBH,LRFP_TYPE,LRFP_YYSR,LRFP_YYSR_ZYSR,LRFP_YYSR_QTSR,LRFP_YYSR_JYCB,LRFP_YYSR_ZYCB,LRFP_YYSR_QTCB,LRFP_YYSR_SJFJ,LRFP_YYSR_XSFY,LRFP_YYSR_GLFY,LRFP_YYSR_CWFY,LRFP_YYSR_JZSS,LRFP_YYSR_JZBD,LRFP_YYSR_TZSY,LRFP_YYSR_LYHY,LRFP_YYLR,LRFP_YYLR_YWSR,LRFP_YYLR_YWZC,LRFP_YYLR_FLDSS,LRFP_LRZE,LRFP_LRZE_SDSF,LRFP_JLR", sqlc);
                string sqlTxtm = string.Format("insert into TBFM_LRFP({0}) values({1})", "RQBH,LRFP_TYPE,LRFP_YYSR,LRFP_YYSR_ZYSR,LRFP_YYSR_QTSR,LRFP_YYSR_JYCB,LRFP_YYSR_ZYCB,LRFP_YYSR_QTCB,LRFP_YYSR_SJFJ,LRFP_YYSR_XSFY,LRFP_YYSR_GLFY,LRFP_YYSR_CWFY,LRFP_YYSR_JZSS,LRFP_YYSR_JZBD,LRFP_YYSR_TZSY,LRFP_YYSR_LYHY,LRFP_YYLR,LRFP_YYLR_YWSR,LRFP_YYLR_YWZC,LRFP_YYLR_FLDSS,LRFP_LRZE,LRFP_LRZE_SDSF,LRFP_JLR,LRFP_NCWFP,LRFP_QTZR,LRFP_KGFP,LRFP_KGFP_FDYYGJ,LRFP_KGFP_FDGY,LRFP_KGFP_JLFL,LRFP_KGFP_CBJJ,LRFP_KGFP_QYFZ,LRFP_KGFP_LRGH,LRFP_KGTZFP,LRFP_KGTZFP_YFYXG,LRFP_KGTZFP_RYYY,LRFP_KGTZFP_YFPTG,LRFP_KGTZFP_ZZZB,LRFP_WFPLR", sqlm);
                listc.Add(sqlTxtc);
                listm.Add(sqlTxtm);

                #endregion
            }
            DBCallCommon.ExecuteTrans(listc);
            DBCallCommon.ExecuteTrans(listm);
            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            bindGrid();
            Response.Redirect(Request.Url.ToString());
        }




        #region 导出功能

        protected void btnexport_Click(object sender, EventArgs e)
        {
            int i = 0;
            string pid = "";
            foreach (RepeaterItem Reitem in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("chkDel") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        pid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("RQBH")).Text;
                    }
                }
            }
            if (i == 1)
            {
                string sqltext = "select * from TBFM_LRFP where RQBH='" + pid + "' and ";

                ExportDataItem(sqltext, pid);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确选择需要导出的资产负债信息！');", true);
            }
        }


        private void ExportDataItem(string sqltext, string pid)
        {

            string filename = "利润分配表.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("利润分配表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);
                string sqltextc = sqltext + "LRFP_TYPE='本期数'";
                string sqltextm = sqltext + "LRFP_TYPE='本年累计数'";
                System.Data.DataTable dtc = DBCallCommon.GetDTUsingSqlText(sqltextc);
                System.Data.DataTable dtm = DBCallCommon.GetDTUsingSqlText(sqltextm);
                DataRow drc = dtc.Rows[0];
                DataRow drm = dtm.Rows[0];
                IRow row1 = sheet0.GetRow(1);//创建行
                row1.GetCell(0).SetCellValue(drc["RQBH"].ToString());

                IRow row4 = sheet0.GetRow(4);//创建行
                row4.GetCell(2).SetCellValue(drc["LRFP_YYSR"].ToString());
                row4.GetCell(3).SetCellValue(drm["LRFP_YYSR"].ToString());


                IRow row5 = sheet0.GetRow(5);
                row5.GetCell(2).SetCellValue(drc["LRFP_YYSR_ZYSR"].ToString());
                row5.GetCell(3).SetCellValue(drm["LRFP_YYSR_ZYSR"].ToString());

                IRow row6 = sheet0.GetRow(6);
                row6.GetCell(2).SetCellValue(drc["LRFP_YYSR_QTSR"].ToString());
                row6.GetCell(3).SetCellValue(drm["LRFP_YYSR_QTSR"].ToString());

                IRow row7 = sheet0.GetRow(7);
                row7.GetCell(2).SetCellValue(drc["LRFP_YYSR_JYCB"].ToString());
                row7.GetCell(3).SetCellValue(drm["LRFP_YYSR_JYCB"].ToString());

                IRow row8 = sheet0.GetRow(8);
                row8.GetCell(2).SetCellValue(drc["LRFP_YYSR_ZYCB"].ToString());
                row8.GetCell(3).SetCellValue(drm["LRFP_YYSR_ZYCB"].ToString());

                IRow row9 = sheet0.GetRow(9);
                row9.GetCell(2).SetCellValue(drc["LRFP_YYSR_QTCB"].ToString());
                row9.GetCell(3).SetCellValue(drm["LRFP_YYSR_QTCB"].ToString());

                IRow row10 = sheet0.GetRow(10);
                row10.GetCell(2).SetCellValue(drc["LRFP_YYSR_SJFJ"].ToString());
                row10.GetCell(3).SetCellValue(drm["LRFP_YYSR_SJFJ"].ToString());

                IRow row11 = sheet0.GetRow(11);
                row11.GetCell(2).SetCellValue(drc["LRFP_YYSR_XSFY"].ToString());
                row11.GetCell(3).SetCellValue(drm["LRFP_YYSR_XSFY"].ToString());

                IRow row12 = sheet0.GetRow(12);
                row12.GetCell(2).SetCellValue(drc["LRFP_YYSR_XSFY"].ToString());
                row12.GetCell(3).SetCellValue(drm["LRFP_YYSR_XSFY"].ToString());

                IRow row13 = sheet0.GetRow(13);
                row13.GetCell(2).SetCellValue(drc["LRFP_YYSR_CWFY"].ToString());
                row13.GetCell(3).SetCellValue(drm["LRFP_YYSR_CWFY"].ToString());

                IRow row14 = sheet0.GetRow(14);
                row14.GetCell(2).SetCellValue(drc["LRFP_YYSR_JZSS"].ToString());
                row14.GetCell(3).SetCellValue(drm["LRFP_YYSR_JZSS"].ToString());

                IRow row15 = sheet0.GetRow(15);
                row15.GetCell(2).SetCellValue(drc["LRFP_YYSR_JZBD"].ToString());
                row15.GetCell(3).SetCellValue(drm["LRFP_YYSR_JZBD"].ToString());

                IRow row16 = sheet0.GetRow(16);
                row16.GetCell(2).SetCellValue(drc["LRFP_YYSR_TZSY"].ToString());
                row16.GetCell(3).SetCellValue(drm["LRFP_YYSR_TZSY"].ToString());

                IRow row17 = sheet0.GetRow(17);
                row17.GetCell(2).SetCellValue(drc["LRFP_YYSR_LYHY"].ToString());
                row17.GetCell(3).SetCellValue(drm["LRFP_YYSR_LYHY"].ToString());

                IRow row18 = sheet0.GetRow(18);
                row18.GetCell(2).SetCellValue(drc["LRFP_YYLR"].ToString());
                row18.GetCell(3).SetCellValue(drm["LRFP_YYLR"].ToString());

                IRow row19 = sheet0.GetRow(19);
                row19.GetCell(2).SetCellValue(drc["LRFP_YYLR_YWSR"].ToString());
                row19.GetCell(3).SetCellValue(drm["LRFP_YYLR_YWSR"].ToString());

                IRow row20 = sheet0.GetRow(20);
                row20.GetCell(2).SetCellValue(drc["LRFP_YYLR_YWZC"].ToString());
                row20.GetCell(3).SetCellValue(drm["LRFP_YYLR_YWZC"].ToString());

                IRow row21 = sheet0.GetRow(21);
                row21.GetCell(2).SetCellValue(drc["LRFP_YYLR_FLDSS"].ToString());
                row21.GetCell(3).SetCellValue(drm["LRFP_YYLR_FLDSS"].ToString());

                IRow row22 = sheet0.GetRow(22);
                row22.GetCell(2).SetCellValue(drc["LRFP_LRZE"].ToString());
                row22.GetCell(3).SetCellValue(drm["LRFP_LRZE"].ToString());

                IRow row23 = sheet0.GetRow(23);
                row23.GetCell(2).SetCellValue(drc["LRFP_LRZE_SDSF"].ToString());
                row23.GetCell(3).SetCellValue(drm["LRFP_LRZE_SDSF"].ToString());

                IRow row24 = sheet0.GetRow(24);
                row24.GetCell(2).SetCellValue(drc["LRFP_JLR"].ToString());
                row24.GetCell(3).SetCellValue(drm["LRFP_JLR"].ToString());

                IRow row27 = sheet0.GetRow(27);
                row27.GetCell(3).SetCellValue(drm["LRFP_NCWFP"].ToString());

                IRow row28 = sheet0.GetRow(28);
                row28.GetCell(3).SetCellValue(drm["LRFP_QTZR"].ToString());

                IRow row29 = sheet0.GetRow(29);
                row29.GetCell(3).SetCellValue(drm["LRFP_KGFP"].ToString());

                IRow row30 = sheet0.GetRow(30);
                row30.GetCell(3).SetCellValue(drm["LRFP_KGFP_FDYYGJ"].ToString());

                IRow row31 = sheet0.GetRow(31);
                row31.GetCell(3).SetCellValue(drm["LRFP_KGFP_FDGY"].ToString());

                IRow row32 = sheet0.GetRow(32);
                row32.GetCell(3).SetCellValue(drm["LRFP_KGFP_JLFL"].ToString());

                IRow row33 = sheet0.GetRow(33);
                row33.GetCell(3).SetCellValue(drm["LRFP_KGFP_CBJJ"].ToString());

                IRow row34 = sheet0.GetRow(34);
                row34.GetCell(3).SetCellValue(drm["LRFP_KGFP_QYFZ"].ToString());

                IRow row35 = sheet0.GetRow(35);
                row35.GetCell(3).SetCellValue(drm["LRFP_KGFP_LRGH"].ToString());

                IRow row36 = sheet0.GetRow(36);
                row36.GetCell(3).SetCellValue(drm["LRFP_KGTZFP"].ToString());

                IRow row37 = sheet0.GetRow(37);
                row37.GetCell(3).SetCellValue(drm["LRFP_KGTZFP_YFYXG"].ToString());

                IRow row38 = sheet0.GetRow(38);
                row38.GetCell(3).SetCellValue(drm["LRFP_KGTZFP_RYYY"].ToString());

                IRow row39 = sheet0.GetRow(39);
                row39.GetCell(3).SetCellValue(drm["LRFP_KGTZFP_YFPTG"].ToString());

                IRow row40 = sheet0.GetRow(40);
                row40.GetCell(3).SetCellValue(drm["LRFP_KGTZFP_ZZZB"].ToString());

                IRow row41 = sheet0.GetRow(41);
                row41.GetCell(3).SetCellValue(drm["LRFP_WFPLR"].ToString());

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
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
                contextResponse.Redirect(string.Format("~/FM_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
        #endregion