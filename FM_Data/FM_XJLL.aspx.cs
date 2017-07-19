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
    public partial class FM_XJLL : BasicPage
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
            pager_org.TableName = "TBFM_XJLL";
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
                sqlText = "RQBH like'" + dplYear.SelectedValue.Trim().ToString() + "-%'";
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
            CommonFun.delMult("TBFM_XJLL", "ID", rptProNumCost);
            bindGrid();
            Response.Redirect(Request.Url.ToString());
        }
        protected string editDq(string DqId)
        {
            return "javascript:window.showModalDialog('FM_XJLL_Detail.aspx?action=update&id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
        }
        protected string viewDq(string DqId)
        {
            return "javascript:window.showModalDialog('FM_XJLL_Detail.aspx?action=look&id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
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
            string FilePath = @"E:\现金流量表\";
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
                string ncs = "上月累计";
                string qms = "本年累计数";
                IRow row1 = sheet.GetRow(89);
                ICell cell2 = row1.GetCell(2);
                string rqbhc = cell2.StringCellValue.ToString().Trim();
                string rqbhm = cell2.StringCellValue.ToString().Trim();//得到修改人员编码
                string sqlTextchk = "select * from TBFM_XJLL where RQBH='" + cell2.StringCellValue.ToString().Trim() + "'";
                System.Data.DataTable dtchk = DBCallCommon.GetDTUsingSqlText(sqlTextchk);
                if (dtchk.Rows.Count>0)
                {
                    Response.Write("<script>alert('日期编号已存在！');</script>"); return;
                }
                sqlc = "'" + rqbhc + "','" + ncs + "'";
                sqlm = "'" + rqbhm + "','" + qms + "'";
                IRow row4 = sheet.GetRow(92);
                ICell cell24 = row4.GetCell(2);
                ICell cell34 = row4.GetCell(3);
                sqlc += ",'" + cell24.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell34.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row5 = sheet.GetRow(93);
                ICell cell25 = row5.GetCell(2);
                ICell cell35 = row5.GetCell(3);
                sqlc += ",'" + cell25.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell35.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row6 = sheet.GetRow(94);
                ICell cell26 = row6.GetCell(2);
                ICell cell36 = row6.GetCell(3);
                sqlc += ",'" + cell26.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell36.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row7 = sheet.GetRow(95);
                ICell cell27 = row7.GetCell(2);
                ICell cell37 = row7.GetCell(3);
                sqlc += ",'" + cell27.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell37.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row8 = sheet.GetRow(96);
                ICell cell28 = row8.GetCell(2);
                ICell cell38 = row8.GetCell(3);
                sqlc += ",'" + cell28.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell38.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row9 = sheet.GetRow(97);
                ICell cell29 = row9.GetCell(2);
                ICell cell39 = row9.GetCell(3);
                sqlc += ",'" + cell29.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell39.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row10 = sheet.GetRow(98);
                ICell cell210 = row10.GetCell(2);
                ICell cell310 = row10.GetCell(3);
                sqlc += ",'" + cell210.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell310.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row11 = sheet.GetRow(99);
                ICell cell211 = row11.GetCell(2);
                ICell cell311 = row11.GetCell(3);
                sqlc += ",'" + cell211.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell311.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row12 = sheet.GetRow(100);
                ICell cell212 = row12.GetCell(2);
                ICell cell312 = row12.GetCell(3);
                sqlc += ",'" + cell212.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell312.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row13 = sheet.GetRow(101);
                ICell cell213 = row13.GetCell(2);
                ICell cell313 = row13.GetCell(3);
                sqlc += ",'" + cell213.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell313.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row14 = sheet.GetRow(102);
                ICell cell214 = row14.GetCell(2);
                ICell cell314 = row14.GetCell(3);
                sqlc += ",'" + cell214.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell314.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row15 = sheet.GetRow(103);
                ICell cell215 = row15.GetCell(2);
                ICell cell315 = row15.GetCell(3);
                sqlc += ",'" + cell215.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell315.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row16 = sheet.GetRow(104);
                ICell cell216 = row16.GetCell(2);
                ICell cell316 = row16.GetCell(3);
                sqlc += ",'" + cell216.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell316.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row17 = sheet.GetRow(105);
                ICell cell217 = row17.GetCell(2);
                ICell cell317 = row17.GetCell(3);
                sqlc += ",'" + cell217.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell317.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row18 = sheet.GetRow(106);
                ICell cell218 = row18.GetCell(2);
                ICell cell318 = row18.GetCell(3);
                sqlc += ",'" + cell218.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell318.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row19 = sheet.GetRow(107);
                ICell cell219 = row19.GetCell(2);
                ICell cell319 = row19.GetCell(3);
                sqlc += ",'" + cell219.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell319.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row20 = sheet.GetRow(108);
                ICell cell220 = row20.GetCell(2);
                ICell cell320 = row20.GetCell(3);
                sqlc += ",'" + cell220.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell320.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row21 = sheet.GetRow(109);
                ICell cell221 = row21.GetCell(2);
                ICell cell321 = row21.GetCell(3);
                sqlc += ",'" + cell221.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell321.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row22 = sheet.GetRow(110);
                ICell cell222 = row22.GetCell(2);
                ICell cell322 = row22.GetCell(3);
                sqlc += ",'" + cell222.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell322.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row23 = sheet.GetRow(111);
                ICell cell223 = row23.GetCell(2);
                ICell cell323 = row23.GetCell(3);
                sqlc += ",'" + cell223.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell323.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row24 = sheet.GetRow(112);
                ICell cell224 = row24.GetCell(2);
                ICell cell324 = row24.GetCell(3);
                sqlc += ",'" + cell224.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell324.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row25 = sheet.GetRow(113);
                ICell cell225 = row25.GetCell(2);
                ICell cell325 = row25.GetCell(3);
                sqlc += ",'" + cell225.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell325.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row26 = sheet.GetRow(114);
                ICell cell226 = row26.GetCell(2);
                ICell cell326 = row26.GetCell(3);
                sqlc += ",'" + cell226.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell326.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row27 = sheet.GetRow(115);
                ICell cell227 = row27.GetCell(2);
                ICell cell327 = row27.GetCell(3);
                sqlc += ",'" + cell227.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell327.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row28 = sheet.GetRow(116);
                ICell cell228 = row28.GetCell(2);
                ICell cell328 = row28.GetCell(3);
                sqlc += ",'" + cell228.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell328.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row29 = sheet.GetRow(117);
                ICell cell229 = row29.GetCell(2);
                ICell cell329 = row29.GetCell(3);
                sqlc += ",'" + cell229.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell329.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row30 = sheet.GetRow(118);
                ICell cell230 = row30.GetCell(2);
                ICell cell330 = row30.GetCell(3);
                sqlc += ",'" + cell230.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell330.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row31 = sheet.GetRow(119);
                ICell cell231 = row31.GetCell(2);
                ICell cell331 = row31.GetCell(3);
                sqlc += ",'" + cell231.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell331.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row32 = sheet.GetRow(120);
                ICell cell232 = row32.GetCell(2);
                ICell cell332 = row32.GetCell(3);
                sqlc += ",'" + cell232.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell332.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row33 = sheet.GetRow(121);
                ICell cell233 = row33.GetCell(2);
                ICell cell333 = row33.GetCell(3);
                sqlc += ",'" + cell233.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell333.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row34 = sheet.GetRow(122);
                ICell cell234 = row34.GetCell(2);
                ICell cell334 = row34.GetCell(3);
                sqlc += ",'" + cell234.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell334.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row35 = sheet.GetRow(123);
                ICell cell235 = row35.GetCell(2);
                ICell cell335 = row35.GetCell(3);
                sqlc += ",'" + cell235.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell335.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row36 = sheet.GetRow(124);
                ICell cell236 = row36.GetCell(2);
                ICell cell336 = row36.GetCell(3);
                sqlc += ",'" + cell236.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell336.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row37 = sheet.GetRow(125);
                ICell cell237 = row37.GetCell(2);
                ICell cell337 = row37.GetCell(3);
                sqlc += ",'" + cell237.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell337.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row38 = sheet.GetRow(126);
                ICell cell238 = row38.GetCell(2);
                ICell cell338 = row38.GetCell(3);
                sqlc += ",'" + cell238.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell338.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row39 = sheet.GetRow(127);
                ICell cell239 = row39.GetCell(2);
                ICell cell339 = row39.GetCell(3);
                sqlc += ",'" + cell239.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell339.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row40 = sheet.GetRow(128);
                ICell cell240 = row40.GetCell(2);
                ICell cell340 = row40.GetCell(3);
                sqlc += ",'" + cell240.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell340.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row41 = sheet.GetRow(129);
                ICell cell241 = row41.GetCell(2);
                ICell cell341 = row41.GetCell(3);
                sqlc += ",'" + cell241.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell341.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row42 = sheet.GetRow(130);
                ICell cell242 = row42.GetCell(2);
                ICell cell342 = row42.GetCell(3);
                sqlc += ",'" + cell242.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell342.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row43 = sheet.GetRow(131);
                ICell cell243 = row43.GetCell(2);
                ICell cell343 = row43.GetCell(3);
                sqlc += ",'" + cell243.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell343.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row44 = sheet.GetRow(132);
                ICell cell244 = row44.GetCell(2);
                ICell cell344 = row44.GetCell(3);
                sqlc += ",'" + cell244.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell344.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row45 = sheet.GetRow(133);
                ICell cell245 = row45.GetCell(2);
                ICell cell345 = row45.GetCell(3);
                sqlc += ",'" + cell245.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell345.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row46 = sheet.GetRow(134);
                ICell cell246 = row46.GetCell(2);
                ICell cell346 = row46.GetCell(3);
                sqlc += ",'" + cell246.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell346.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row47 = sheet.GetRow(135);
                ICell cell247 = row47.GetCell(2);
                ICell cell347 = row47.GetCell(3);
                sqlc += ",'" + cell247.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell347.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row50 = sheet.GetRow(138);
                ICell cell250 = row50.GetCell(2);
                ICell cell350 = row50.GetCell(3);
                sqlc += ",'" + cell250.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell350.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row51 = sheet.GetRow(139);
                ICell cell251 = row51.GetCell(2);
                ICell cell351 = row51.GetCell(3);
                sqlc += ",'" + cell251.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell351.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row52 = sheet.GetRow(140);
                ICell cell252 = row52.GetCell(2);
                ICell cell352 = row52.GetCell(3);
                sqlc += ",'" + cell252.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell352.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row53 = sheet.GetRow(141);
                ICell cell253 = row53.GetCell(2);
                ICell cell353 = row53.GetCell(3);
                sqlc += ",'" + cell253.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell353.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row54 = sheet.GetRow(142);
                ICell cell254 = row54.GetCell(2);
                ICell cell354 = row54.GetCell(3);
                sqlc += ",'" + cell254.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell354.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row55 = sheet.GetRow(143);
                ICell cell255 = row55.GetCell(2);
                ICell cell355 = row55.GetCell(3);
                sqlc += ",'" + cell255.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell355.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row56 = sheet.GetRow(144);
                ICell cell256 = row56.GetCell(2);
                ICell cell356 = row56.GetCell(3);
                sqlc += ",'" + cell256.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell356.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row57 = sheet.GetRow(145);
                ICell cell257 = row57.GetCell(2);
                ICell cell357 = row57.GetCell(3);
                sqlc += ",'" + cell257.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell357.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row58 = sheet.GetRow(146);
                ICell cell258 = row58.GetCell(2);
                ICell cell358 = row58.GetCell(3);
                sqlc += ",'" + cell258.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell358.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row59 = sheet.GetRow(147);
                ICell cell259 = row59.GetCell(2);
                ICell cell359 = row59.GetCell(3);
                sqlc += ",'" + cell259.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell359.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row60 = sheet.GetRow(148);
                ICell cell260 = row60.GetCell(2);
                ICell cell360 = row60.GetCell(3);
                sqlc += ",'" + cell260.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell360.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row61 = sheet.GetRow(149);
                ICell cell261 = row61.GetCell(2);
                ICell cell361 = row61.GetCell(3);
                sqlc += ",'" + cell261.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell361.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row62 = sheet.GetRow(150);
                ICell cell262 = row62.GetCell(2);
                ICell cell362 = row62.GetCell(3);
                sqlc += ",'" + cell262.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell362.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row63 = sheet.GetRow(151);
                ICell cell263 = row63.GetCell(2);
                ICell cell363 = row63.GetCell(3);
                sqlc += ",'" + cell263.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell363.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row64 = sheet.GetRow(152);
                ICell cell264 = row64.GetCell(2);
                ICell cell364 = row64.GetCell(3);
                sqlc += ",'" + cell264.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell364.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row65 = sheet.GetRow(153);
                ICell cell265 = row65.GetCell(2);
                ICell cell365 = row65.GetCell(3);
                sqlc += ",'" + cell265.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell365.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row66 = sheet.GetRow(154);
                ICell cell266 = row66.GetCell(2);
                ICell cell366 = row66.GetCell(3);
                sqlc += ",'" + cell266.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell366.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row67 = sheet.GetRow(155);
                ICell cell267 = row67.GetCell(2);
                ICell cell367 = row67.GetCell(3);
                sqlc += ",'" + cell267.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell367.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row68 = sheet.GetRow(156);
                ICell cell268 = row68.GetCell(2);
                ICell cell368 = row68.GetCell(3);
                sqlc += ",'" + cell268.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell368.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row69 = sheet.GetRow(157);
                ICell cell269 = row69.GetCell(2);
                ICell cell369 = row69.GetCell(3);
                sqlc += ",'" + cell269.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell369.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row70 = sheet.GetRow(158);
                ICell cell270 = row70.GetCell(2);
                ICell cell370 = row70.GetCell(3);
                sqlc += ",'" + cell270.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell370.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row71 = sheet.GetRow(159);
                ICell cell271 = row71.GetCell(2);
                ICell cell371 = row71.GetCell(3);
                sqlc += ",'" + cell271.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell371.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row72 = sheet.GetRow(160);
                ICell cell272 = row72.GetCell(2);
                ICell cell372 = row72.GetCell(3);
                sqlc += ",'" + cell272.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell372.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row73 = sheet.GetRow(161);
                ICell cell273 = row73.GetCell(2);
                ICell cell373 = row73.GetCell(3);
                sqlc += ",'" + cell273.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell373.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row74 = sheet.GetRow(162);
                ICell cell274 = row74.GetCell(2);
                ICell cell374 = row74.GetCell(3);
                sqlc += ",'" + cell274.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell374.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row75 = sheet.GetRow(163);
                ICell cell275 = row75.GetCell(2);
                ICell cell375 = row75.GetCell(3);
                sqlc += ",'" + cell275.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell375.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row76 = sheet.GetRow(164);
                ICell cell276 = row76.GetCell(2);
                ICell cell376 = row76.GetCell(3);
                sqlc += ",'" + cell276.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell376.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row77 = sheet.GetRow(165);
                ICell cell277 = row77.GetCell(2);
                ICell cell377 = row77.GetCell(3);
                sqlc += ",'" + cell277.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell377.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row78 = sheet.GetRow(166);
                ICell cell278 = row78.GetCell(2);
                ICell cell378 = row78.GetCell(3);
                sqlc += ",'" + cell278.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell378.NumericCellValue.ToString("0.00").Trim() + "'";

                IRow row79 = sheet.GetRow(167);
                ICell cell279 = row79.GetCell(2);
                ICell cell379 = row79.GetCell(3);
                sqlc += ",'" + cell279.NumericCellValue.ToString("0.00").Trim() + "'";
                sqlm += ",'" + cell379.NumericCellValue.ToString("0.00").Trim() + "'";

                #region 存入数据库

                string sqlTxtc = string.Format("insert into TBFM_XJLL({0}) values({1})", "RQBH,XJLL_TYPE,XJLL_JYHD,XJLL_JYHD_XSTG,XJLL_JYHD_FH,XJLL_JYHD_QT,XJLL_JYHD_LRXJ,XJLL_JYHD_GMJS,XJLL_JYHD_ZFZG,XJLL_JYHD_ZFSF,XJLL_JYHD_ZFQT,XJLL_JYHD_LCXJ,XJLL_JYHD_JE,XJLL_TZHD,XJLL_TZHD_SHTZ,XJLL_TZHD_QDTZ,XJLL_TZHD_CZZCJE,XJLL_TZHD_CZQT,XJLL_TZHD_QT,XJLL_TZHD_LRXJ,XJLL_TZHD_GJZF,XJLL_TZHD_TZZF,XJLL_TZHD_QDJE,XJLL_TZHD_ZFQT,XJLL_TZHD_LCXJ,XJLL_TZHD_JE,XJLL_CZHD,XJLL_CZHD_XSTZ,XJLL_CZHD_QZZGS,XJLL_CZHD_JKSD,XJLL_CZHD_QZZCGJJD,XJLL_CZHD_QZZCZBJD,XJLL_CZHD_SDQT,XJLL_CZHD_LRXJ,XJLL_CZHD_CHZW,XJLL_CZHD_QZZCGJCH,XJLL_CZHD_QZZCZBCH,XJLL_CZHD_FPZF,XJLL_CZHD_QZZF,XJLL_CZHD_ZFQT,XJLL_CZHD_LCXJ,XJLL_CZHD_JE,XJLL_HLBD,XJLL_DJJZE,XJLL_DJJZE_JNCYE,XJLL_NMYE,XJLL_BC_JLTJJY,XJLL_BC_JLTJJY_JL,XJLL_BC_JLTJJY_JSY,XJLL_BC_JLTJJY_JWQRSS,XJLL_BC_JLTJJY_JJT,XJLL_BC_JLTJJY_GDZJ,XJLL_BC_JLTJJY_WXTX,XJLL_BC_JLTJJY_CQDT,XJLL_BC_JLTJJY_CZSS,XJLL_BC_JLTJJY_GDBF,XJLL_BC_JLTJJY_GYBD,XJLL_BC_JLTJJY_CWFY,XJLL_BC_JLTJJY_TZSS,XJLL_BC_JLTJJY_DYSD,XJLL_BC_JLTJJY_CHJS,XJLL_BC_JLTJJY_YSJS,XJLL_BC_JLTJJY_YFZJ,XJLL_BC_JLTJJY_QT,XJLL_BC_JLTJJY_JE,XJLL_BC_BSTC,XJLL_BC_BSTC_ZZZB,XJLL_BC_BSTC_YNKZZQ,XJLL_BC_BSTC_RZZR,XJLL_BC_BSTC_QT,XJLL_BC_JZQK,XJLL_BC_JZQK_QMYE,XJLL_BC_JZQK_JXQC,XJLL_BC_JZQK_JDQM,XJLL_BC_JZQK_JDQC,XJLL_BC_JZQK_JZE", sqlc);
                string sqlTxtm = string.Format("insert into TBFM_XJLL({0}) values({1})", "RQBH,XJLL_TYPE,XJLL_JYHD,XJLL_JYHD_XSTG,XJLL_JYHD_FH,XJLL_JYHD_QT,XJLL_JYHD_LRXJ,XJLL_JYHD_GMJS,XJLL_JYHD_ZFZG,XJLL_JYHD_ZFSF,XJLL_JYHD_ZFQT,XJLL_JYHD_LCXJ,XJLL_JYHD_JE,XJLL_TZHD,XJLL_TZHD_SHTZ,XJLL_TZHD_QDTZ,XJLL_TZHD_CZZCJE,XJLL_TZHD_CZQT,XJLL_TZHD_QT,XJLL_TZHD_LRXJ,XJLL_TZHD_GJZF,XJLL_TZHD_TZZF,XJLL_TZHD_QDJE,XJLL_TZHD_ZFQT,XJLL_TZHD_LCXJ,XJLL_TZHD_JE,XJLL_CZHD,XJLL_CZHD_XSTZ,XJLL_CZHD_QZZGS,XJLL_CZHD_JKSD,XJLL_CZHD_QZZCGJJD,XJLL_CZHD_QZZCZBJD,XJLL_CZHD_SDQT,XJLL_CZHD_LRXJ,XJLL_CZHD_CHZW,XJLL_CZHD_QZZCGJCH,XJLL_CZHD_QZZCZBCH,XJLL_CZHD_FPZF,XJLL_CZHD_QZZF,XJLL_CZHD_ZFQT,XJLL_CZHD_LCXJ,XJLL_CZHD_JE,XJLL_HLBD,XJLL_DJJZE,XJLL_DJJZE_JNCYE,XJLL_NMYE,XJLL_BC_JLTJJY,XJLL_BC_JLTJJY_JL,XJLL_BC_JLTJJY_JSY,XJLL_BC_JLTJJY_JWQRSS,XJLL_BC_JLTJJY_JJT,XJLL_BC_JLTJJY_GDZJ,XJLL_BC_JLTJJY_WXTX,XJLL_BC_JLTJJY_CQDT,XJLL_BC_JLTJJY_CZSS,XJLL_BC_JLTJJY_GDBF,XJLL_BC_JLTJJY_GYBD,XJLL_BC_JLTJJY_CWFY,XJLL_BC_JLTJJY_TZSS,XJLL_BC_JLTJJY_DYSD,XJLL_BC_JLTJJY_CHJS,XJLL_BC_JLTJJY_YSJS,XJLL_BC_JLTJJY_YFZJ,XJLL_BC_JLTJJY_QT,XJLL_BC_JLTJJY_JE,XJLL_BC_BSTC,XJLL_BC_BSTC_ZZZB,XJLL_BC_BSTC_YNKZZQ,XJLL_BC_BSTC_RZZR,XJLL_BC_BSTC_QT,XJLL_BC_JZQK,XJLL_BC_JZQK_QMYE,XJLL_BC_JZQK_JXQC,XJLL_BC_JZQK_JDQM,XJLL_BC_JZQK_JDQC,XJLL_BC_JZQK_JZE", sqlm);
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
                string sqltext = "select * from TBFM_XJLL where RQBH='" + pid + "' and ";

                ExportDataItem(sqltext, pid);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确选择需要导出的资产负债信息！');", true);
            }
        }


        private void ExportDataItem(string sqltext, string pid)
        {

            string filename = "现金流量表.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("现金流量表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);
                string sqltextc = sqltext + "XJLL_TYPE='上月累计'";
                string sqltextm = sqltext + "XJLL_TYPE='本年累计数'";
                System.Data.DataTable dtc = DBCallCommon.GetDTUsingSqlText(sqltextc);
                System.Data.DataTable dtm = DBCallCommon.GetDTUsingSqlText(sqltextm);
                DataRow drc = dtc.Rows[0];
                DataRow drm = dtm.Rows[0];
                IRow row1 = sheet0.GetRow(1);//创建行
                row1.GetCell(2).SetCellValue(drc["RQBH"].ToString());

                IRow row4 = sheet0.GetRow(4);//创建行
                row4.GetCell(2).SetCellValue(drc["XJLL_JYHD"].ToString());
                row4.GetCell(3).SetCellValue(drm["XJLL_JYHD"].ToString());
                

                IRow row5 = sheet0.GetRow(5);
                row5.GetCell(2).SetCellValue(drc["XJLL_JYHD_XSTG"].ToString());
                row5.GetCell(3).SetCellValue(drm["XJLL_JYHD_XSTG"].ToString());

                IRow row6 = sheet0.GetRow(6);
                row6.GetCell(2).SetCellValue(drc["XJLL_JYHD_FH"].ToString());
                row6.GetCell(3).SetCellValue(drm["XJLL_JYHD_FH"].ToString());

                IRow row7 = sheet0.GetRow(7);
                row7.GetCell(2).SetCellValue(drc["XJLL_JYHD_QT"].ToString());
                row7.GetCell(3).SetCellValue(drm["XJLL_JYHD_QT"].ToString());

                IRow row8 = sheet0.GetRow(8);
                row8.GetCell(2).SetCellValue(drc["XJLL_JYHD_LRXJ"].ToString());
                row8.GetCell(3).SetCellValue(drm["XJLL_JYHD_LRXJ"].ToString());

                IRow row9 = sheet0.GetRow(9);
                row9.GetCell(2).SetCellValue(drc["XJLL_JYHD_GMJS"].ToString());
                row9.GetCell(3).SetCellValue(drm["XJLL_JYHD_GMJS"].ToString());

                IRow row10 = sheet0.GetRow(10);
                row10.GetCell(2).SetCellValue(drc["XJLL_JYHD_ZFZG"].ToString());
                row10.GetCell(3).SetCellValue(drm["XJLL_JYHD_ZFZG"].ToString());

                IRow row11 = sheet0.GetRow(11);
                row11.GetCell(2).SetCellValue(drc["XJLL_JYHD_ZFSF"].ToString());
                row11.GetCell(3).SetCellValue(drm["XJLL_JYHD_ZFSF"].ToString());

                IRow row12 = sheet0.GetRow(12);
                row12.GetCell(2).SetCellValue(drc["XJLL_JYHD_ZFQT"].ToString());
                row12.GetCell(3).SetCellValue(drm["XJLL_JYHD_ZFQT"].ToString());

                IRow row13 = sheet0.GetRow(13);
                row13.GetCell(2).SetCellValue(drc["XJLL_JYHD_LCXJ"].ToString());
                row13.GetCell(3).SetCellValue(drm["XJLL_JYHD_LCXJ"].ToString());

                IRow row14 = sheet0.GetRow(14);
                row14.GetCell(2).SetCellValue(drc["XJLL_JYHD_JE"].ToString());
                row14.GetCell(3).SetCellValue(drm["XJLL_JYHD_JE"].ToString());

                IRow row15 = sheet0.GetRow(15);
                row15.GetCell(2).SetCellValue(drc["XJLL_TZHD"].ToString());
                row15.GetCell(3).SetCellValue(drm["XJLL_TZHD"].ToString());

                IRow row16 = sheet0.GetRow(16);
                row16.GetCell(2).SetCellValue(drc["XJLL_TZHD_SHTZ"].ToString());
                row16.GetCell(3).SetCellValue(drm["XJLL_TZHD_SHTZ"].ToString());

                IRow row17 = sheet0.GetRow(17);
                row17.GetCell(2).SetCellValue(drc["XJLL_TZHD_QDTZ"].ToString());
                row17.GetCell(3).SetCellValue(drm["XJLL_TZHD_QDTZ"].ToString());

                IRow row18 = sheet0.GetRow(18);
                row18.GetCell(2).SetCellValue(drc["XJLL_TZHD_CZZCJE"].ToString());
                row18.GetCell(3).SetCellValue(drm["XJLL_TZHD_CZZCJE"].ToString());

                IRow row19 = sheet0.GetRow(19);
                row19.GetCell(2).SetCellValue(drc["XJLL_TZHD_CZQT"].ToString());
                row19.GetCell(3).SetCellValue(drm["XJLL_TZHD_CZQT"].ToString());

                IRow row20 = sheet0.GetRow(20);
                row20.GetCell(2).SetCellValue(drc["XJLL_TZHD_QT"].ToString());
                row20.GetCell(3).SetCellValue(drm["XJLL_TZHD_QT"].ToString());

                IRow row21 = sheet0.GetRow(21);
                row21.GetCell(2).SetCellValue(drc["XJLL_TZHD_LRXJ"].ToString());
                row21.GetCell(3).SetCellValue(drm["XJLL_TZHD_LRXJ"].ToString());

                IRow row22 = sheet0.GetRow(22);
                row22.GetCell(2).SetCellValue(drc["XJLL_TZHD_GJZF"].ToString());
                row22.GetCell(3).SetCellValue(drm["XJLL_TZHD_GJZF"].ToString());

                IRow row23 = sheet0.GetRow(23);
                row23.GetCell(2).SetCellValue(drc["XJLL_TZHD_TZZF"].ToString());
                row23.GetCell(3).SetCellValue(drm["XJLL_TZHD_TZZF"].ToString());

                IRow row24 = sheet0.GetRow(24);
                row24.GetCell(2).SetCellValue(drc["XJLL_TZHD_QDJE"].ToString());
                row24.GetCell(3).SetCellValue(drm["XJLL_TZHD_QDJE"].ToString());

                IRow row25 = sheet0.GetRow(25);
                row25.GetCell(2).SetCellValue(drc["XJLL_TZHD_ZFQT"].ToString());
                row25.GetCell(3).SetCellValue(drm["XJLL_TZHD_ZFQT"].ToString());

                IRow row26 = sheet0.GetRow(26);
                row26.GetCell(2).SetCellValue(drc["XJLL_TZHD_LCXJ"].ToString());
                row26.GetCell(3).SetCellValue(drm["XJLL_TZHD_LCXJ"].ToString());

                IRow row27 = sheet0.GetRow(27);
                row27.GetCell(2).SetCellValue(drc["XJLL_TZHD_JE"].ToString());
                row27.GetCell(3).SetCellValue(drm["XJLL_TZHD_JE"].ToString());

                IRow row28 = sheet0.GetRow(28);
                row28.GetCell(2).SetCellValue(drc["XJLL_CZHD"].ToString());
                row28.GetCell(3).SetCellValue(drm["XJLL_CZHD"].ToString());

                IRow row29 = sheet0.GetRow(29);
                row29.GetCell(2).SetCellValue(drc["XJLL_CZHD_XSTZ"].ToString());
                row29.GetCell(3).SetCellValue(drm["XJLL_CZHD_XSTZ"].ToString());

                IRow row30 = sheet0.GetRow(30);
                row30.GetCell(2).SetCellValue(drc["XJLL_CZHD_QZZGS"].ToString());
                row30.GetCell(3).SetCellValue(drm["XJLL_CZHD_QZZGS"].ToString());

                IRow row31 = sheet0.GetRow(31);
                row31.GetCell(2).SetCellValue(drc["XJLL_CZHD_JKSD"].ToString());
                row31.GetCell(3).SetCellValue(drm["XJLL_CZHD_JKSD"].ToString());

                IRow row32 = sheet0.GetRow(32);
                row32.GetCell(2).SetCellValue(drc["XJLL_CZHD_QZZCGJJD"].ToString());
                row32.GetCell(3).SetCellValue(drm["XJLL_CZHD_QZZCGJJD"].ToString());

                IRow row33 = sheet0.GetRow(33);
                row33.GetCell(2).SetCellValue(drc["XJLL_CZHD_QZZCZBJD"].ToString());
                row33.GetCell(3).SetCellValue(drm["XJLL_CZHD_QZZCZBJD"].ToString());

                IRow row34 = sheet0.GetRow(34);
                row34.GetCell(2).SetCellValue(drc["XJLL_CZHD_SDQT"].ToString());
                row34.GetCell(3).SetCellValue(drm["XJLL_CZHD_SDQT"].ToString());

                IRow row35 = sheet0.GetRow(35);
                row35.GetCell(2).SetCellValue(drc["XJLL_CZHD_LRXJ"].ToString());
                row35.GetCell(3).SetCellValue(drm["XJLL_CZHD_LRXJ"].ToString());

                IRow row36 = sheet0.GetRow(36);
                row36.GetCell(2).SetCellValue(drc["XJLL_CZHD_CHZW"].ToString());
                row36.GetCell(3).SetCellValue(drm["XJLL_CZHD_CHZW"].ToString());

                IRow row37 = sheet0.GetRow(37);
                row37.GetCell(2).SetCellValue(drc["XJLL_CZHD_QZZCGJCH"].ToString());
                row37.GetCell(3).SetCellValue(drm["XJLL_CZHD_QZZCGJCH"].ToString());

                IRow row38 = sheet0.GetRow(38);
                row38.GetCell(2).SetCellValue(drc["XJLL_CZHD_QZZCZBCH"].ToString());
                row38.GetCell(3).SetCellValue(drm["XJLL_CZHD_QZZCZBCH"].ToString());

                IRow row39 = sheet0.GetRow(39);
                row39.GetCell(2).SetCellValue(drc["XJLL_CZHD_FPZF"].ToString());
                row39.GetCell(3).SetCellValue(drm["XJLL_CZHD_FPZF"].ToString());

                IRow row40 = sheet0.GetRow(40);
                row40.GetCell(2).SetCellValue(drc["XJLL_CZHD_QZZF"].ToString());
                row40.GetCell(3).SetCellValue(drm["XJLL_CZHD_QZZF"].ToString());

                IRow row41 = sheet0.GetRow(41);
                row41.GetCell(2).SetCellValue(drc["XJLL_CZHD_ZFQT"].ToString());
                row41.GetCell(3).SetCellValue(drm["XJLL_CZHD_ZFQT"].ToString());

                IRow row42 = sheet0.GetRow(42);
                row42.GetCell(2).SetCellValue(drc["XJLL_CZHD_LCXJ"].ToString());
                row42.GetCell(3).SetCellValue(drm["XJLL_CZHD_LCXJ"].ToString());

                IRow row43 = sheet0.GetRow(43);
                row43.GetCell(2).SetCellValue(drc["XJLL_CZHD_JE"].ToString());
                row43.GetCell(3).SetCellValue(drm["XJLL_CZHD_JE"].ToString());

                IRow row44 = sheet0.GetRow(44);
                row44.GetCell(2).SetCellValue(drc["XJLL_HLBD"].ToString());
                row44.GetCell(3).SetCellValue(drm["XJLL_HLBD"].ToString());

                IRow row45 = sheet0.GetRow(45);
                row45.GetCell(2).SetCellValue(drc["XJLL_DJJZE"].ToString());
                row45.GetCell(3).SetCellValue(drm["XJLL_DJJZE"].ToString());

                IRow row46 = sheet0.GetRow(46);
                row46.GetCell(2).SetCellValue(drc["XJLL_DJJZE_JNCYE"].ToString());
                row46.GetCell(3).SetCellValue(drm["XJLL_DJJZE_JNCYE"].ToString());

                IRow row47 = sheet0.GetRow(47);
                row47.GetCell(2).SetCellValue(drc["XJLL_NMYE"].ToString());
                row47.GetCell(3).SetCellValue(drm["XJLL_NMYE"].ToString());

                IRow row50 = sheet0.GetRow(50);
                row50.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY"].ToString());
                row50.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY"].ToString());

                IRow row51 = sheet0.GetRow(51);
                row51.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_JL"].ToString());
                row51.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_JL"].ToString());

                IRow row52 = sheet0.GetRow(52);
                row52.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_JSY"].ToString());
                row52.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_JSY"].ToString());

                IRow row53 = sheet0.GetRow(53);
                row53.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_JWQRSS"].ToString());
                row53.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_JWQRSS"].ToString());

                IRow row54 = sheet0.GetRow(54);
                row54.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_JJT"].ToString());
                row54.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_JJT"].ToString());

                IRow row55 = sheet0.GetRow(55);
                row55.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_GDZJ"].ToString());
                row55.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_GDZJ"].ToString());

                IRow row56 = sheet0.GetRow(56);
                row56.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_WXTX"].ToString());
                row56.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_WXTX"].ToString());

                IRow row57 = sheet0.GetRow(57);
                row57.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_CQDT"].ToString());
                row57.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_CQDT"].ToString());

                IRow row58 = sheet0.GetRow(58);
                row58.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_CZSS"].ToString());
                row58.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_CZSS"].ToString());

                IRow row59 = sheet0.GetRow(59);
                row59.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_GDBF"].ToString());
                row59.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_GDBF"].ToString());

                IRow row60 = sheet0.GetRow(60);
                row60.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_GYBD"].ToString());
                row60.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_GYBD"].ToString());

                IRow row61 = sheet0.GetRow(61);
                row61.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_CWFY"].ToString());
                row61.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_CWFY"].ToString());

                IRow row62 = sheet0.GetRow(62);
                row62.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_TZSS"].ToString());
                row62.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_TZSS"].ToString());

                IRow row63 = sheet0.GetRow(63);
                row63.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_DYSD"].ToString());
                row63.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_DYSD"].ToString());

                IRow row64 = sheet0.GetRow(64);
                row64.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_CHJS"].ToString());
                row64.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_CHJS"].ToString());

                IRow row65 = sheet0.GetRow(65);
                row65.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_YSJS"].ToString());
                row65.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_YSJS"].ToString());

                IRow row66 = sheet0.GetRow(66);
                row66.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_YFZJ"].ToString());
                row66.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_YFZJ"].ToString());

                IRow row67 = sheet0.GetRow(67);
                row67.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_QT"].ToString());
                row67.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_QT"].ToString());

                IRow row68 = sheet0.GetRow(68);
                row68.GetCell(2).SetCellValue(drc["XJLL_BC_JLTJJY_JE"].ToString());
                row68.GetCell(3).SetCellValue(drm["XJLL_BC_JLTJJY_JE"].ToString());

                IRow row69 = sheet0.GetRow(69);
                row69.GetCell(2).SetCellValue(drc["XJLL_BC_BSTC"].ToString());
                row69.GetCell(3).SetCellValue(drm["XJLL_BC_BSTC"].ToString());

                IRow row70 = sheet0.GetRow(70);
                row70.GetCell(2).SetCellValue(drc["XJLL_BC_BSTC_ZZZB"].ToString());
                row70.GetCell(3).SetCellValue(drm["XJLL_BC_BSTC_ZZZB"].ToString());

                IRow row71 = sheet0.GetRow(71);
                row71.GetCell(2).SetCellValue(drc["XJLL_BC_BSTC_YNKZZQ"].ToString());
                row71.GetCell(3).SetCellValue(drm["XJLL_BC_BSTC_YNKZZQ"].ToString());

                IRow row72 = sheet0.GetRow(72);
                row72.GetCell(2).SetCellValue(drc["XJLL_BC_BSTC_RZZR"].ToString());
                row72.GetCell(3).SetCellValue(drm["XJLL_BC_BSTC_RZZR"].ToString());

                IRow row73 = sheet0.GetRow(73);
                row73.GetCell(2).SetCellValue(drc["XJLL_BC_BSTC_QT"].ToString());
                row73.GetCell(3).SetCellValue(drm["XJLL_BC_BSTC_QT"].ToString());

                IRow row74 = sheet0.GetRow(74);
                row74.GetCell(2).SetCellValue(drc["XJLL_BC_JZQK"].ToString());
                row74.GetCell(3).SetCellValue(drm["XJLL_BC_JZQK"].ToString());

                IRow row75 = sheet0.GetRow(75);
                row75.GetCell(2).SetCellValue(drc["XJLL_BC_JZQK_QMYE"].ToString());
                row75.GetCell(3).SetCellValue(drm["XJLL_BC_JZQK_QMYE"].ToString());

                IRow row76 = sheet0.GetRow(76);
                row76.GetCell(2).SetCellValue(drc["XJLL_BC_JZQK_JXQC"].ToString());
                row76.GetCell(3).SetCellValue(drm["XJLL_BC_JZQK_JXQC"].ToString());

                IRow row77 = sheet0.GetRow(77);
                row77.GetCell(2).SetCellValue(drc["XJLL_BC_JZQK_JDQM"].ToString());
                row77.GetCell(3).SetCellValue(drm["XJLL_BC_JZQK_JDQM"].ToString());

                IRow row78 = sheet0.GetRow(78);
                row78.GetCell(2).SetCellValue(drc["XJLL_BC_JZQK_JDQC"].ToString());
                row78.GetCell(3).SetCellValue(drm["XJLL_BC_JZQK_JDQC"].ToString());

                IRow row79 = sheet0.GetRow(79);
                row79.GetCell(2).SetCellValue(drc["XJLL_BC_JZQK_JZE"].ToString());
                row79.GetCell(3).SetCellValue(drm["XJLL_BC_JZQK_JZE"].ToString());

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