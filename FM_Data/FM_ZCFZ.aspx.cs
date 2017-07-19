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
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_ZCFZ : BasicPage
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
            pager_org.TableName = "((SELECT * FROM TBFM_ZCFZ WHERE ZCFZ_TYPE='年初数' and RQBH like '%-01%') union all (select * from TBFM_ZCFZ where ZCFZ_TYPE='期末数'))a";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "ZCFZ_TYPE,RQBH";
            pager_org.StrWhere =strstring();
            pager_org.OrderType =0;//升序排列
            pager_org.PageSize = 30;
        }

        private string strstring()
        {
            string sqlText = "";
            if (dplYear.SelectedValue.Trim().ToString() == "-请选择-"&&dplMoth.SelectedValue.Trim().ToString()=="-请选择-")
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
                sqlText += "RQBH like'" +dplYear.SelectedValue.Trim().ToString()+"-"+dplMoth.SelectedValue.Trim().ToString()+"%'";
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
            CommonFun.delMult("TBFM_ZCFZ", "ID", rptProNumCost);
            bindGrid();
            Response.Redirect(Request.Url.ToString());
        }
        protected string editDq(string DqId)
        {
            return "javascript:window.showModalDialog('FM_ZCFZ_Detail.aspx?action=update&id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
        }
        protected string viewDq(string DqId)
        {
            return "javascript:window.showModalDialog('FM_ZCFZ_Detail.aspx?action=look&id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
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







        //查询
#region 导出功能

        protected void btnexport_Click(object sender, EventArgs e) //导出
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
                string sqltext = "select * from TBFM_ZCFZ where RQBH='" + pid + "' and ";

                ExportDataItem(sqltext, pid);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确选择需要导出的资产负债信息！');", true);
            }
        }

        private void ExportDataItem(string sqltext,string pid)
        {

            string filename = "资产负债表.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("资产负债表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);
                string sqltextc=sqltext+"ZCFZ_TYPE='年初数'";
                string sqltextm=sqltext+"ZCFZ_TYPE='期末数'";
                System.Data.DataTable dtc = DBCallCommon.GetDTUsingSqlText(sqltextc);
                System.Data.DataTable dtm = DBCallCommon.GetDTUsingSqlText(sqltextm);
                DataRow drc = dtc.Rows[0];
                DataRow drm = dtm.Rows[0];

                IRow row1 = sheet0.GetRow(1);//创建行
                row1.GetCell(0).SetCellValue(drc["RQBH"].ToString());

                IRow row5 = sheet0.GetRow(5);//创建行
                row5.GetCell(2).SetCellValue(drc["ZC_LD_HBZJ"].ToString());
                row5.GetCell(3).SetCellValue(drm["ZC_LD_HBZJ"].ToString());
                row5.GetCell(6).SetCellValue(drc["FZ_LD_DQJK"].ToString());
                row5.GetCell(7).SetCellValue(drm["FZ_LD_DQJK"].ToString());

                IRow row6 = sheet0.GetRow(6);
                row6.GetCell(2).SetCellValue(drc["ZC_LD_HBZJ_JS"].ToString());
                row6.GetCell(3).SetCellValue(drm["ZC_LD_HBZJ_JS"].ToString());
                row6.GetCell(6).SetCellValue(drc["FZ_LD_DQJK_JS"].ToString());
                row6.GetCell(7).SetCellValue(drm["FZ_LD_DQJK_JS"].ToString());

                IRow row7 = sheet0.GetRow(7);
                row7.GetCell(2).SetCellValue(drc["ZC_LD_JYJR"].ToString());
                row7.GetCell(3).SetCellValue(drm["ZC_LD_JYJR"].ToString());
                row7.GetCell(6).SetCellValue(drc["FZ_LD_JYJR"].ToString());
                row7.GetCell(7).SetCellValue(drm["FZ_LD_JYJR"].ToString());

                IRow row8 = sheet0.GetRow(8);
                row8.GetCell(2).SetCellValue(drc["ZC_LD_YSPJ"].ToString());
                row8.GetCell(3).SetCellValue(drm["ZC_LD_YSPJ"].ToString());
                row8.GetCell(6).SetCellValue(drc["FZ_LD_YFPJ"].ToString());
                row8.GetCell(7).SetCellValue(drm["FZ_LD_YFPJ"].ToString());

                IRow row9 = sheet0.GetRow(9);
                row9.GetCell(2).SetCellValue(drc["ZC_LD_YSZKYZ"].ToString());
                row9.GetCell(3).SetCellValue(drm["ZC_LD_YSZKYZ"].ToString());
                row9.GetCell(6).SetCellValue(drc["FZ_LD_YFZK"].ToString());
                row9.GetCell(7).SetCellValue(drm["FZ_LD_YFZK"].ToString());

                IRow row10 = sheet0.GetRow(10);
                row10.GetCell(2).SetCellValue(drc["ZC_LD_JH"].ToString());
                row10.GetCell(3).SetCellValue(drm["ZC_LD_JH"].ToString());
                row10.GetCell(6).SetCellValue(drc["FZ_LD_YSKX"].ToString());
                row10.GetCell(7).SetCellValue(drm["FZ_LD_YSKX"].ToString());

                IRow row11 = sheet0.GetRow(11);
                row11.GetCell(2).SetCellValue(drc["ZC_LD_YSZKJZ"].ToString());
                row11.GetCell(3).SetCellValue(drm["ZC_LD_YSZKJZ"].ToString());
                row11.GetCell(6).SetCellValue(drc["FZ_LD_YFXC"].ToString());
                row11.GetCell(7).SetCellValue(drm["FZ_LD_YFXC"].ToString());

                IRow row12 = sheet0.GetRow(12);
                row12.GetCell(2).SetCellValue(drc["ZC_LD_YFKX"].ToString());
                row12.GetCell(3).SetCellValue(drm["ZC_LD_YFKX"].ToString());
                row12.GetCell(6).SetCellValue(drc["FZ_LD_YJSF"].ToString());
                row12.GetCell(7).SetCellValue(drm["FZ_LD_YJSF"].ToString());

                IRow row13 = sheet0.GetRow(13);
                row13.GetCell(2).SetCellValue(drc["ZC_LD_YSLX"].ToString());
                row13.GetCell(3).SetCellValue(drm["ZC_LD_YSLX"].ToString());
                row13.GetCell(6).SetCellValue(drc["FZ_LD_YFLX"].ToString());
                row13.GetCell(7).SetCellValue(drm["FZ_LD_YFLX"].ToString());

                IRow row14 = sheet0.GetRow(14);
                row14.GetCell(2).SetCellValue(drc["ZC_LD_YSGL"].ToString());
                row14.GetCell(3).SetCellValue(drm["ZC_LD_YSGL"].ToString());
                row14.GetCell(6).SetCellValue(drc["FZ_LD_YFGL"].ToString());
                row14.GetCell(7).SetCellValue(drm["FZ_LD_YFGL"].ToString());

                IRow row15 = sheet0.GetRow(15);
                row15.GetCell(2).SetCellValue(drc["ZC_LD_QTYS"].ToString());
                row15.GetCell(3).SetCellValue(drm["ZC_LD_QTYS"].ToString());
                row15.GetCell(6).SetCellValue(drc["FZ_LD_QTYF"].ToString());
                row15.GetCell(7).SetCellValue(drm["FZ_LD_QTYF"].ToString());

                IRow row16 = sheet0.GetRow(16);
                row16.GetCell(2).SetCellValue(drc["ZC_LD_CH"].ToString());
                row16.GetCell(3).SetCellValue(drm["ZC_LD_CH"].ToString());
                row16.GetCell(6).SetCellValue(drc["FZ_LD_YNDF"].ToString());
                row16.GetCell(7).SetCellValue(drm["FZ_LD_YNDF"].ToString());

                IRow row17 = sheet0.GetRow(17);
                row17.GetCell(2).SetCellValue(drc["ZC_LD_YNFLD"].ToString());
                row17.GetCell(3).SetCellValue(drm["ZC_LD_YNFLD"].ToString());
                row17.GetCell(6).SetCellValue(drc["FZ_LD_QT"].ToString());
                row17.GetCell(7).SetCellValue(drm["FZ_LD_QT"].ToString());

                IRow row18 = sheet0.GetRow(18);
                row18.GetCell(2).SetCellValue(drc["ZC_LD_QT"].ToString());
                row18.GetCell(3).SetCellValue(drm["ZC_LD_QT"].ToString());

                IRow row19 = sheet0.GetRow(19);
                row19.GetCell(2).SetCellValue(drc["ZC_LD_HJ"].ToString());
                row19.GetCell(3).SetCellValue(drm["ZC_LD_HJ"].ToString());
                row19.GetCell(6).SetCellValue(drc["FZ_LD_HJ"].ToString());
                row19.GetCell(7).SetCellValue(drm["FZ_LD_HJ"].ToString());

                IRow row21 = sheet0.GetRow(21);
                row21.GetCell(2).SetCellValue(drc["ZC_FLD_KJ"].ToString());
                row21.GetCell(3).SetCellValue(drm["ZC_FLD_KJ"].ToString());
                row21.GetCell(6).SetCellValue(drc["FZ_FLD_CQJK"].ToString());
                row21.GetCell(7).SetCellValue(drm["FZ_FLD_CQJK"].ToString());

                IRow row22 = sheet0.GetRow(22);
                row22.GetCell(2).SetCellValue(drc["ZC_FLD_CDT"].ToString());
                row22.GetCell(3).SetCellValue(drm["ZC_FLD_CDT"].ToString());
                row22.GetCell(6).SetCellValue(drc["FZ_FLD_CQJK_JS"].ToString());
                row22.GetCell(7).SetCellValue(drm["FZ_FLD_CQJK_JS"].ToString());

                IRow row23 = sheet0.GetRow(23);
                row23.GetCell(2).SetCellValue(drc["ZC_FLD_CQYS"].ToString());
                row23.GetCell(3).SetCellValue(drm["ZC_FLD_CQYS"].ToString());
                row23.GetCell(6).SetCellValue(drc["FZ_FLD_YFZJ"].ToString());
                row23.GetCell(7).SetCellValue(drm["FZ_FLD_YFZJ"].ToString());

                IRow row24 = sheet0.GetRow(24);
                row24.GetCell(2).SetCellValue(drc["ZC_FLD_CQGQT"].ToString());
                row24.GetCell(3).SetCellValue(drm["ZC_FLD_CQGQT"].ToString());
                row24.GetCell(6).SetCellValue(drc["FZ_FLD_CQYF"].ToString());
                row24.GetCell(7).SetCellValue(drm["FZ_FLD_CQYF"].ToString());

                IRow row25 = sheet0.GetRow(25);
                row25.GetCell(2).SetCellValue(drc["ZC_FLD_TF"].ToString());
                row25.GetCell(3).SetCellValue(drm["ZC_FLD_TF"].ToString());
                row25.GetCell(6).SetCellValue(drc["FZ_FLD_ZXYF"].ToString());
                row25.GetCell(7).SetCellValue(drm["FZ_FLD_ZXYF"].ToString());

                IRow row26 = sheet0.GetRow(26);
                row26.GetCell(2).SetCellValue(drc["ZC_FLD_GZY"].ToString());
                row26.GetCell(3).SetCellValue(drm["ZC_FLD_GZY"].ToString());
                row26.GetCell(6).SetCellValue(drc["FZ_FLD_YJFZ"].ToString());
                row26.GetCell(7).SetCellValue(drm["FZ_FLD_YJFZ"].ToString());

                IRow row27 = sheet0.GetRow(27);
                row27.GetCell(2).SetCellValue(drc["ZC_FLD_JL"].ToString());
                row27.GetCell(3).SetCellValue(drm["ZC_FLD_JL"].ToString());
                row27.GetCell(6).SetCellValue(drc["FZ_FLD_DYSD"].ToString());
                row27.GetCell(7).SetCellValue(drm["FZ_FLD_DYSD"].ToString());

                IRow row28 = sheet0.GetRow(28);
                row28.GetCell(2).SetCellValue(drc["ZC_FLD_GZJZ"].ToString());
                row28.GetCell(3).SetCellValue(drm["ZC_FLD_GZJZ"].ToString());
                row28.GetCell(6).SetCellValue(drc["FZ_FLD_QT"].ToString());
                row28.GetCell(7).SetCellValue(drm["FZ_FLD_QT"].ToString());

                IRow row29 = sheet0.GetRow(29);
                row29.GetCell(2).SetCellValue(drc["ZC_FLD_JG"].ToString());
                row29.GetCell(3).SetCellValue(drm["ZC_FLD_JG"].ToString());

                IRow row30 = sheet0.GetRow(30);
                row30.GetCell(2).SetCellValue(drc["ZC_FLD_GZJE"].ToString());
                row30.GetCell(3).SetCellValue(drm["ZC_FLD_GZJE"].ToString());
                row30.GetCell(6).SetCellValue(drc["FZ_FLD_HJ"].ToString());
                row30.GetCell(7).SetCellValue(drm["FZ_FLD_HJ"].ToString());

                IRow row31 = sheet0.GetRow(31);
                row31.GetCell(2).SetCellValue(drc["ZC_FLD_ZJ"].ToString());
                row31.GetCell(3).SetCellValue(drm["ZC_FLD_ZJ"].ToString());
                row31.GetCell(6).SetCellValue(drc["FZ_HJ"].ToString());
                row31.GetCell(7).SetCellValue(drm["FZ_HJ"].ToString());

                IRow row32 = sheet0.GetRow(32);
                row32.GetCell(2).SetCellValue(drc["ZC_FLD_GCWZ"].ToString());
                row32.GetCell(3).SetCellValue(drm["ZC_FLD_GCWZ"].ToString());

                IRow row33 = sheet0.GetRow(33);
                row33.GetCell(2).SetCellValue(drc["ZC_FLD_GZQL"].ToString());
                row33.GetCell(3).SetCellValue(drm["ZC_FLD_GZQL"].ToString());
                row33.GetCell(6).SetCellValue(drc["QY_SSZB"].ToString());
                row33.GetCell(7).SetCellValue(drm["QY_SSZB"].ToString());

                IRow row34 = sheet0.GetRow(34);
                row34.GetCell(2).SetCellValue(drc["ZC_FLD_WXZC"].ToString());
                row34.GetCell(3).SetCellValue(drm["ZC_FLD_WXZC"].ToString());
                row34.GetCell(6).SetCellValue(drc["QY_JY"].ToString());
                row34.GetCell(7).SetCellValue(drm["QY_JY"].ToString());

                IRow row35 = sheet0.GetRow(35);
                row35.GetCell(2).SetCellValue(drc["ZC_FLD_KFZC"].ToString());
                row35.GetCell(3).SetCellValue(drm["ZC_FLD_KFZC"].ToString());
                row35.GetCell(6).SetCellValue(drc["QY_ZBGJ"].ToString());
                row35.GetCell(7).SetCellValue(drm["QY_ZBGJ"].ToString());

                IRow row36 = sheet0.GetRow(36);
                row36.GetCell(2).SetCellValue(drc["ZC_FLD_SY"].ToString());
                row36.GetCell(3).SetCellValue(drm["ZC_FLD_SY"].ToString());
                row36.GetCell(6).SetCellValue(drc["QY_JK"].ToString());
                row36.GetCell(7).SetCellValue(drm["QY_JK"].ToString());

                IRow row37 = sheet0.GetRow(37);
                row37.GetCell(2).SetCellValue(drc["ZC_FLD_CQDT"].ToString());
                row37.GetCell(3).SetCellValue(drm["ZC_FLD_CQDT"].ToString());
                row37.GetCell(6).SetCellValue(drc["QY_YYGJ"].ToString());
                row37.GetCell(7).SetCellValue(drm["QY_YYGJ"].ToString());

                IRow row38 = sheet0.GetRow(38);
                row38.GetCell(2).SetCellValue(drc["ZC_FLD_DYSD"].ToString());
                row38.GetCell(3).SetCellValue(drm["ZC_FLD_DYSD"].ToString());
                row38.GetCell(6).SetCellValue(drc["QY_ZXCB"].ToString());
                row38.GetCell(7).SetCellValue(drm["QY_ZXCB"].ToString());

                IRow row39 = sheet0.GetRow(39);
                row39.GetCell(2).SetCellValue(drc["ZC_FLD_QT"].ToString());
                row39.GetCell(3).SetCellValue(drm["ZC_FLD_QT"].ToString());
                row39.GetCell(6).SetCellValue(drc["QY_WFP"].ToString());
                row39.GetCell(7).SetCellValue(drm["QY_WFP"].ToString());

                IRow row40 = sheet0.GetRow(40);
                row40.GetCell(2).SetCellValue(drc["ZC_FLD_HJ"].ToString());
                row40.GetCell(3).SetCellValue(drm["ZC_FLD_HJ"].ToString());
                row40.GetCell(6).SetCellValue(drc["QY_HJ"].ToString());
                row40.GetCell(7).SetCellValue(drm["QY_HJ"].ToString());

                IRow row41 = sheet0.GetRow(41);
                row41.GetCell(2).SetCellValue(drc["ZC_ZJ"].ToString());
                row41.GetCell(3).SetCellValue(drm["ZC_ZJ"].ToString());
                row41.GetCell(6).SetCellValue(drc["FZQY_ZJ"].ToString());
                row41.GetCell(7).SetCellValue(drm["FZQY_ZJ"].ToString());

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

        protected void btn_Import_Click_Click(object sender, EventArgs e)
        {
            List<string> listc = new List<string>();
            List<string> listm = new List<string>();
            string FilePath = @"E:\资产负债表\";
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
                string ncs = "年初数";
                string qms = "期末数";
                IRow row1 = sheet.GetRow(1);
                ICell cell0 = row1.GetCell(0);
               
              //  string rqbhc = cell0.StringCellValue.ToString().Trim();
               // string rqbhm = cell0.StringCellValue.ToString().Trim();//得到修改人员编码
                  string rqbhc = cell0.ToString().Trim();
                string rqbhm = cell0.ToString().Trim();//得到修改人员编码
                string sqlTextchk = "select * from TBFM_ZCFZ where RQBH='" + cell0.ToString().Trim() + "'";
                System.Data.DataTable dtchk = DBCallCommon.GetDTUsingSqlText(sqlTextchk);
                if (dtchk.Rows.Count>0)
                {
                    Response.Write("<script>alert('日期编号已存在！');</script>"); return;
                }
                sqlc = "'" + rqbhc + "','" + ncs + "'";
                sqlm = "'" + rqbhm + "','" + qms + "'";

                try
                {
                    IRow row5 = sheet.GetRow(5);
                    ICell cell25 = row5.GetCell(2);
                    ICell cell35 = row5.GetCell(3);
                    ICell cell65 = row5.GetCell(6);
                    ICell cell75 = row5.GetCell(7);
                    sqlc += ",'" + cell25.NumericCellValue.ToString("0.00").Trim() + "','" + cell65.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell35.NumericCellValue.ToString("0.00").Trim() + "','" + cell75.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row6 = sheet.GetRow(6);
                    ICell cell26 = row6.GetCell(2);
                    ICell cell36 = row6.GetCell(3);
                    ICell cell66 = row6.GetCell(6);
                    ICell cell76 = row6.GetCell(7);
                    sqlc += ",'" + cell26.NumericCellValue.ToString("0.00").Trim() + "','" + cell66.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell36.NumericCellValue.ToString("0.00").Trim() + "','" + cell76.NumericCellValue.ToString("0.00").Trim() + "'";
                   
                    IRow row7 = sheet.GetRow(7);
                    ICell cell27 = row7.GetCell(2);
                    ICell cell37 = row7.GetCell(3);
                    ICell cell67 = row7.GetCell(6);
                    ICell cell77 = row7.GetCell(7);
                    sqlc += ",'" + cell27.NumericCellValue.ToString("0.00").Trim() + "','" + cell67.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell37.NumericCellValue.ToString("0.00").Trim() + "','" + cell77.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row8 = sheet.GetRow(8);
                    ICell cell28 = row8.GetCell(2);
                    ICell cell38 = row8.GetCell(3);
                    ICell cell68 = row8.GetCell(6);
                    ICell cell78 = row8.GetCell(7);
                    sqlc += ",'" + cell28.NumericCellValue.ToString("0.00").Trim() + "','" + cell68.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell38.NumericCellValue.ToString("0.00").Trim() + "','" + cell78.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row9 = sheet.GetRow(9);
                    ICell cell29 = row9.GetCell(2);
                    ICell cell39 = row9.GetCell(3);
                    ICell cell69 = row9.GetCell(6);
                    ICell cell79 = row9.GetCell(7);
                    sqlc += ",'" + cell29.NumericCellValue.ToString("0.00").Trim() + "','" + cell69.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell39.NumericCellValue.ToString("0.00").Trim() + "','" + cell79.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row10 = sheet.GetRow(10);
                    ICell cell210 = row10.GetCell(2);
                    ICell cell310 = row10.GetCell(3);
                    ICell cell610 = row10.GetCell(6);
                    ICell cell710 = row10.GetCell(7);
                    sqlc += ",'" + cell210.NumericCellValue.ToString("0.00").Trim() + "','" + cell610.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell310.NumericCellValue.ToString("0.00").Trim() + "','" + cell710.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row11 = sheet.GetRow(11);
                    ICell cell211 = row11.GetCell(2);
                    ICell cell311 = row11.GetCell(3);
                    ICell cell611 = row11.GetCell(6);
                    ICell cell711 = row11.GetCell(7);
                    sqlc += ",'" + cell211.NumericCellValue.ToString("0.00").Trim() + "','" + cell611.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell311.NumericCellValue.ToString("0.00").Trim() + "','" + cell711.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row12 = sheet.GetRow(12);
                    ICell cell212 = row12.GetCell(2);
                    ICell cell312 = row12.GetCell(3);
                    ICell cell612 = row12.GetCell(6);
                    ICell cell712 = row12.GetCell(7);
                    sqlc += ",'" + cell212.NumericCellValue.ToString("0.00").Trim() + "','" + cell612.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell312.NumericCellValue.ToString("0.00").Trim() + "','" + cell712.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row13 = sheet.GetRow(13);
                    ICell cell213 = row13.GetCell(2);
                    ICell cell313 = row13.GetCell(3);
                    ICell cell613 = row13.GetCell(6);
                    ICell cell713 = row13.GetCell(7);
                    sqlc += ",'" + cell213.NumericCellValue.ToString("0.00").Trim() + "','" + cell613.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell313.NumericCellValue.ToString("0.00").Trim() + "','" + cell713.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row14 = sheet.GetRow(14);
                    ICell cell214 = row14.GetCell(2);
                    ICell cell314 = row14.GetCell(3);
                    ICell cell614 = row14.GetCell(6);
                    ICell cell714 = row14.GetCell(7);
                    sqlc += ",'" + cell214.NumericCellValue.ToString("0.00").Trim() + "','" + cell614.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell314.NumericCellValue.ToString("0.00").Trim() + "','" + cell714.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row15 = sheet.GetRow(15);
                    ICell cell215 = row15.GetCell(2);
                    ICell cell315 = row15.GetCell(3);
                    ICell cell615 = row15.GetCell(6);
                    ICell cell715 = row15.GetCell(7);
                    sqlc += ",'" + cell215.NumericCellValue.ToString("0.00").Trim() + "','" + cell615.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell315.NumericCellValue.ToString("0.00").Trim() + "','" + cell715.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row16 = sheet.GetRow(16);
                    ICell cell216 = row16.GetCell(2);
                    ICell cell316 = row16.GetCell(3);
                    ICell cell616 = row16.GetCell(6);
                    ICell cell716 = row16.GetCell(7);
                    sqlc += ",'" + cell216.NumericCellValue.ToString("0.00").Trim() + "','" + cell616.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell316.NumericCellValue.ToString("0.00").Trim() + "','" + cell716.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row17 = sheet.GetRow(17);
                    ICell cell217 = row17.GetCell(2);
                    ICell cell317 = row17.GetCell(3);
                    ICell cell617 = row17.GetCell(6);
                    ICell cell717 = row17.GetCell(7);
                    sqlc += ",'" + cell217.NumericCellValue.ToString("0.00").Trim() + "','" + cell617.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell317.NumericCellValue.ToString("0.00").Trim() + "','" + cell717.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row18 = sheet.GetRow(18);
                    ICell cell218 = row18.GetCell(2);
                    ICell cell318 = row18.GetCell(3);
                    sqlc += ",'" + cell218.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell318.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row19 = sheet.GetRow(19);
                    ICell cell219 = row19.GetCell(2);
                    ICell cell319 = row19.GetCell(3);
                    ICell cell619 = row19.GetCell(6);
                    ICell cell719 = row19.GetCell(7);
                    sqlc += ",'" + cell219.NumericCellValue.ToString("0.00").Trim() + "','" + cell619.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell319.NumericCellValue.ToString("0.00").Trim() + "','" + cell719.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row21 = sheet.GetRow(21);
                    ICell cell221 = row21.GetCell(2);
                    ICell cell321 = row21.GetCell(3);
                    ICell cell621 = row21.GetCell(6);
                    ICell cell721 = row21.GetCell(7);
                    sqlc += ",'" + cell221.NumericCellValue.ToString("0.00").Trim() + "','" + cell621.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell321.NumericCellValue.ToString("0.00").Trim() + "','" + cell721.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row22 = sheet.GetRow(22);
                    ICell cell222 = row22.GetCell(2);
                    ICell cell322 = row22.GetCell(3);
                    ICell cell622 = row22.GetCell(6);
                    ICell cell722 = row22.GetCell(7);
                    sqlc += ",'" + cell222.NumericCellValue.ToString("0.00").Trim() + "','" + cell622.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell322.NumericCellValue.ToString("0.00").Trim() + "','" + cell722.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row23 = sheet.GetRow(23);
                    ICell cell223 = row23.GetCell(2);
                    ICell cell323 = row23.GetCell(3);
                    ICell cell623 = row23.GetCell(6);
                    ICell cell723 = row23.GetCell(7);
                    sqlc += ",'" + cell223.NumericCellValue.ToString("0.00").Trim() + "','" + cell623.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell323.NumericCellValue.ToString("0.00").Trim() + "','" + cell723.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row24 = sheet.GetRow(24);
                    ICell cell224 = row24.GetCell(2);
                    ICell cell324 = row24.GetCell(3);
                    ICell cell624 = row24.GetCell(6);
                    ICell cell724 = row24.GetCell(7);
                    sqlc += ",'" + cell224.NumericCellValue.ToString("0.00").Trim() + "','" + cell624.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell324.NumericCellValue.ToString("0.00").Trim() + "','" + cell724.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row25 = sheet.GetRow(25);
                    ICell cell225 = row25.GetCell(2);
                    ICell cell325 = row25.GetCell(3);
                    ICell cell625 = row25.GetCell(6);
                    ICell cell725 = row25.GetCell(7);
                    sqlc += ",'" + cell225.NumericCellValue.ToString("0.00").Trim() + "','" + cell625.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell325.NumericCellValue.ToString("0.00").Trim() + "','" + cell725.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row26 = sheet.GetRow(26);
                    ICell cell226 = row26.GetCell(2);
                    ICell cell326 = row26.GetCell(3);
                    ICell cell626 = row26.GetCell(6);
                    ICell cell726 = row26.GetCell(7);
                    sqlc += ",'" + cell226.NumericCellValue.ToString("0.00").Trim() + "','" + cell626.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell326.NumericCellValue.ToString("0.00").Trim() + "','" + cell726.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row27 = sheet.GetRow(27);
                    ICell cell227 = row27.GetCell(2);
                    ICell cell327 = row27.GetCell(3);
                    ICell cell627 = row27.GetCell(6);
                    ICell cell727 = row27.GetCell(7);
                    sqlc += ",'" + cell227.NumericCellValue.ToString("0.00").Trim() + "','" + cell627.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell327.NumericCellValue.ToString("0.00").Trim() + "','" + cell727.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row28 = sheet.GetRow(28);
                    ICell cell228 = row28.GetCell(2);
                    ICell cell328 = row28.GetCell(3);
                    ICell cell628 = row28.GetCell(6);
                    ICell cell728 = row28.GetCell(7);
                    sqlc += ",'" + cell228.NumericCellValue.ToString("0.00").Trim() + "','" + cell628.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell328.NumericCellValue.ToString("0.00").Trim() + "','" + cell728.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row29 = sheet.GetRow(29);
                    ICell cell229 = row28.GetCell(2);
                    ICell cell329 = row28.GetCell(3);
                    sqlc += ",'" + cell229.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell329.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row30 = sheet.GetRow(30);
                    ICell cell230 = row30.GetCell(2);
                    ICell cell330 = row30.GetCell(3);
                    ICell cell630 = row30.GetCell(6);
                    ICell cell730 = row30.GetCell(7);
                    sqlc += ",'" + cell230.NumericCellValue.ToString("0.00").Trim() + "','" + cell630.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell330.NumericCellValue.ToString("0.00").Trim() + "','" + cell730.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row31 = sheet.GetRow(31);
                    ICell cell231 = row31.GetCell(2);
                    ICell cell331 = row31.GetCell(3);
                    ICell cell631 = row31.GetCell(6);
                    ICell cell731 = row31.GetCell(7);
                    sqlc += ",'" + cell231.NumericCellValue.ToString("0.00").Trim() + "','" + cell631.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell331.NumericCellValue.ToString("0.00").Trim() + "','" + cell731.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row32 = sheet.GetRow(32);
                    ICell cell232 = row32.GetCell(2);
                    ICell cell332 = row32.GetCell(3);
                    sqlc += ",'" + cell232.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell332.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row33 = sheet.GetRow(33);
                    ICell cell233 = row33.GetCell(2);
                    ICell cell333 = row33.GetCell(3);
                    ICell cell633 = row33.GetCell(6);
                    ICell cell733 = row33.GetCell(7);
                    sqlc += ",'" + cell233.NumericCellValue.ToString("0.00").Trim() + "','" + cell633.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell333.NumericCellValue.ToString("0.00").Trim() + "','" + cell733.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row34 = sheet.GetRow(34);
                    ICell cell234 = row34.GetCell(2);
                    ICell cell334 = row34.GetCell(3);
                    ICell cell634 = row34.GetCell(6);
                    ICell cell734 = row34.GetCell(7);
                    sqlc += ",'" + cell234.NumericCellValue.ToString("0.00").Trim() + "','" + cell634.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell334.NumericCellValue.ToString("0.00").Trim() + "','" + cell734.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row35 = sheet.GetRow(35);
                    ICell cell235 = row35.GetCell(2);
                    ICell cell335 = row35.GetCell(3);
                    ICell cell635 = row35.GetCell(6);
                    ICell cell735 = row35.GetCell(7);
                    sqlc += ",'" + cell235.NumericCellValue.ToString("0.00").Trim() + "','" + cell635.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell335.NumericCellValue.ToString("0.00").Trim() + "','" + cell735.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row36 = sheet.GetRow(36);
                    ICell cell236 = row36.GetCell(2);
                    ICell cell336 = row36.GetCell(3);
                    ICell cell636 = row36.GetCell(6);
                    ICell cell736 = row36.GetCell(7);
                    sqlc += ",'" + cell236.NumericCellValue.ToString("0.00").Trim() + "','" + cell636.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell336.NumericCellValue.ToString("0.00").Trim() + "','" + cell736.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row37 = sheet.GetRow(37);
                    ICell cell237 = row37.GetCell(2);
                    ICell cell337 = row37.GetCell(3);
                    ICell cell637 = row37.GetCell(6);
                    ICell cell737 = row37.GetCell(7);
                    sqlc += ",'" + cell237.NumericCellValue.ToString("0.00").Trim() + "','" + cell637.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell337.NumericCellValue.ToString("0.00").Trim() + "','" + cell737.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row38 = sheet.GetRow(38);
                    ICell cell238 = row38.GetCell(2);
                    ICell cell338 = row38.GetCell(3);
                    ICell cell638 = row38.GetCell(6);
                    ICell cell738 = row38.GetCell(7);
                    sqlc += ",'" + cell238.NumericCellValue.ToString("0.00").Trim() + "','" + cell638.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell338.NumericCellValue.ToString("0.00").Trim() + "','" + cell738.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row39 = sheet.GetRow(39);
                    ICell cell239 = row39.GetCell(2);
                    ICell cell339 = row39.GetCell(3);
                    ICell cell639 = row39.GetCell(6);
                    ICell cell739 = row39.GetCell(7);
                    sqlc += ",'" + cell239.NumericCellValue.ToString("0.00").Trim() + "','" + cell639.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell339.NumericCellValue.ToString("0.00").Trim() + "','" + cell739.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row40 = sheet.GetRow(40);
                    ICell cell240 = row40.GetCell(2);
                    ICell cell340 = row40.GetCell(3);
                    ICell cell640 = row40.GetCell(6);
                    ICell cell740 = row40.GetCell(7);
                    sqlc += ",'" + cell240.NumericCellValue.ToString("0.00").Trim() + "','" + cell640.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell340.NumericCellValue.ToString("0.00").Trim() + "','" + cell740.NumericCellValue.ToString("0.00").Trim() + "'";

                    IRow row41 = sheet.GetRow(41);
                    ICell cell241 = row41.GetCell(2);
                    ICell cell341 = row41.GetCell(3);
                    ICell cell641 = row41.GetCell(6);
                    ICell cell741 = row41.GetCell(7);
                    sqlc += ",'" + cell241.NumericCellValue.ToString("0.00").Trim() + "','" + cell641.NumericCellValue.ToString("0.00").Trim() + "'";
                    sqlm += ",'" + cell341.NumericCellValue.ToString("0.00").Trim() + "','" + cell741.NumericCellValue.ToString("0.00").Trim() + "'";
                }
                catch(Exception)
                {
                    throw;
                }
                #region 存入数据库

                string sqlTxtc = string.Format("insert into TBFM_ZCFZ({0}) values({1})", "RQBH,ZCFZ_TYPE,ZC_LD_HBZJ,FZ_LD_DQJK,ZC_LD_HBZJ_JS,FZ_LD_DQJK_JS,ZC_LD_JYJR,FZ_LD_JYJR,ZC_LD_YSPJ,FZ_LD_YFPJ,ZC_LD_YSZKYZ,FZ_LD_YFZK,ZC_LD_JH,FZ_LD_YSKX,ZC_LD_YSZKJZ,FZ_LD_YFXC,ZC_LD_YFKX,FZ_LD_YJSF,ZC_LD_YSLX,FZ_LD_YFLX,ZC_LD_YSGL,FZ_LD_YFGL,ZC_LD_QTYS,FZ_LD_QTYF,ZC_LD_CH,FZ_LD_YNDF,ZC_LD_YNFLD,FZ_LD_QT,ZC_LD_QT,ZC_LD_HJ,FZ_LD_HJ,ZC_FLD_KJ,FZ_FLD_CQJK,ZC_FLD_CDT,FZ_FLD_CQJK_JS,ZC_FLD_CQYS,FZ_FLD_YFZJ,ZC_FLD_CQGQT,FZ_FLD_CQYF,ZC_FLD_TF,FZ_FLD_ZXYF,ZC_FLD_GZY,FZ_FLD_YJFZ,ZC_FLD_JL,FZ_FLD_DYSD,ZC_FLD_GZJZ,FZ_FLD_QT,ZC_FLD_JG,ZC_FLD_GZJE,FZ_FLD_HJ,ZC_FLD_ZJ,FZ_HJ,ZC_FLD_GCWZ,ZC_FLD_GZQL,QY_SSZB,ZC_FLD_WXZC,QY_JY,ZC_FLD_KFZC,QY_ZBGJ,ZC_FLD_SY,QY_JK,ZC_FLD_CQDT,QY_YYGJ,ZC_FLD_DYSD,QY_ZXCB,ZC_FLD_QT,QY_WFP,ZC_FLD_HJ,QY_HJ,ZC_ZJ,FZQY_ZJ", sqlc);
                string sqlTxtm = string.Format("insert into TBFM_ZCFZ({0}) values({1})", "RQBH,ZCFZ_TYPE,ZC_LD_HBZJ,FZ_LD_DQJK,ZC_LD_HBZJ_JS,FZ_LD_DQJK_JS,ZC_LD_JYJR,FZ_LD_JYJR,ZC_LD_YSPJ,FZ_LD_YFPJ,ZC_LD_YSZKYZ,FZ_LD_YFZK,ZC_LD_JH,FZ_LD_YSKX,ZC_LD_YSZKJZ,FZ_LD_YFXC,ZC_LD_YFKX,FZ_LD_YJSF,ZC_LD_YSLX,FZ_LD_YFLX,ZC_LD_YSGL,FZ_LD_YFGL,ZC_LD_QTYS,FZ_LD_QTYF,ZC_LD_CH,FZ_LD_YNDF,ZC_LD_YNFLD,FZ_LD_QT,ZC_LD_QT,ZC_LD_HJ,FZ_LD_HJ,ZC_FLD_KJ,FZ_FLD_CQJK,ZC_FLD_CDT,FZ_FLD_CQJK_JS,ZC_FLD_CQYS,FZ_FLD_YFZJ,ZC_FLD_CQGQT,FZ_FLD_CQYF,ZC_FLD_TF,FZ_FLD_ZXYF,ZC_FLD_GZY,FZ_FLD_YJFZ,ZC_FLD_JL,FZ_FLD_DYSD,ZC_FLD_GZJZ,FZ_FLD_QT,ZC_FLD_JG,ZC_FLD_GZJE,FZ_FLD_HJ,ZC_FLD_ZJ,FZ_HJ,ZC_FLD_GCWZ,ZC_FLD_GZQL,QY_SSZB,ZC_FLD_WXZC,QY_JY,ZC_FLD_KFZC,QY_ZBGJ,ZC_FLD_SY,QY_JK,ZC_FLD_CQDT,QY_YYGJ,ZC_FLD_DYSD,QY_ZXCB,ZC_FLD_QT,QY_WFP,ZC_FLD_HJ,QY_HJ,ZC_ZJ,FZQY_ZJ", sqlm);
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
       }
    }
#endregion