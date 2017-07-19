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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_SCCZSP : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                //{
                //    FileUpload1.Visible = false;
                //    btn_importclient.Visible = false;
                //    radio_all.Visible = false;
                //}
                this.BindYearMoth(ddlYear, ddlMonth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
            }
            CheckUser(ControlFinder);
            InitVar();
        }
        /// <summary>
        /// 绑定年月

        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                ddl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            ddl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }

        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>

        private void InitPage()
        {
            ddlYear.ClearSelection();
            foreach (ListItem li in ddlYear.Items)//显示当前年份
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            ddlMonth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)//显示当前月份
            {
                month = "0" + month;
            }
            foreach (ListItem li in ddlMonth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数

        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "OM_SCCZSH_TOTAL";
            pager_org.PrimaryKey = "SCCZTOL_BH";
            pager_org.ShowFields = "SCCZTOL_BH,SCCZTOL_NY,SCCZTOL_ZDRNAME,SCCZTOL_ZDTIME,SCCZTOL_TOLSTATE,SCCZTOL_NOTE";
            pager_org.OrderField = "SCCZTOL_NY,SCCZTOL_BH";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 20;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and SCCZTOL_NY='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            }
            else if (ddlYear.SelectedIndex == 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and SCCZTOL_NY like '%-" + ddlMonth.SelectedValue.ToString().Trim() + "%'";
            }
            else if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex == 0)
            {
                sql += " and SCCZTOL_NY like '%" + ddlYear.SelectedValue.ToString().Trim() + "-%'";
            }
            if (drp_state.SelectedIndex != 0)
            {
                sql += " and SCCZTOL_TOLSTATE='" + drp_state.SelectedValue.ToString().Trim() + "'";
            }
            if (radio_mytask.Checked == true)
            {
                sql = "((SCCZTOL_ZDRID='" + Session["UserID"].ToString().Trim() + "' and SCCZTOL_TOLSTATE='0') or (SCCZTOL_SHRID1='" + Session["UserID"].ToString().Trim() + "' and SCCZTOL_TOLSTATE='1' and SCCZTOL_SHRZT1='0') or (SCCZTOL_SHRID2='" + Session["UserID"].ToString().Trim() + "' and SCCZTOL_TOLSTATE='1' and SCCZTOL_SHRZT2='0' and SCCZTOL_SHRZT1='1') or (SCCZTOL_SHRID3='" + Session["UserID"].ToString().Trim() + "' and SCCZTOL_TOLSTATE='1' and SCCZTOL_SHRZT3='0' and SCCZTOL_SHRZT1='1' and SCCZTOL_SHRZT2='1'))";
            }
            return sql;
        }
        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptscczsp, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        #endregion


        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        /// <summary>
        /// 年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        /// <summary>
        /// 月份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radio_all_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radio_mytask_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void drp_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }


        protected void btn_import_Click(object sender, EventArgs e)
        {
            List<string> list000 = new List<string>();
            if (tb_yearmonth.Text.ToString().Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写年月！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            string sqlifscgz = "select * from OM_GZHSB where GZ_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "' and (OM_GZSCBZ='0' or OM_GZSCBZ='1')";
            System.Data.DataTable dtisscgz = DBCallCommon.GetDTUsingSqlText(sqlifscgz);
            if (dtisscgz.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            string sqlifspz = "select * from OM_SCCZSH_TOTAL where TOLTYPE='0' and (SCCZTOL_TOLSTATE='1' or SCCZTOL_TOLSTATE='2') and SCCZTOL_NY='" + tb_yearmonth.Text.Trim() + "'";
            System.Data.DataTable dtifspz = DBCallCommon.GetDTUsingSqlText(sqlifspz);
            if (dtifspz.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已有数据审批中或已通过！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            string sqlwtj = "select * from OM_SCCZSH_TOTAL where SCCZTOL_TOLSTATE='0' and TOLTYPE='0' and SCCZTOL_NY='" + tb_yearmonth.Text.Trim() + "'";
            System.Data.DataTable dtwtj = DBCallCommon.GetDTUsingSqlText(sqlwtj);
            if (dtwtj.Rows.Count > 0)
            {
                string sqldelete0 = "delete from OM_SCCZSH_TOTAL where SCCZTOL_TOLSTATE='0' and TOLTYPE='0' and SCCZTOL_NY='" + tb_yearmonth.Text.Trim() + "'";
                list000.Add(sqldelete0);
                for (int n = 0; n < dtwtj.Rows.Count; n++)
                {
                    string sqldelete1 = "delete from OM_SCCZ_FZBZ where TYPE='0' and FZBZ_BH='" + dtwtj.Rows[n]["SCCZTOL_BH"].ToString().Trim() + "'";
                    list000.Add(sqldelete1);
                    string sqldelete2 = "delete from OM_SCCZ_YIXIAN where YIXIAN_BH='" + dtwtj.Rows[n]["SCCZTOL_BH"].ToString().Trim() + "'";
                    list000.Add(sqldelete2);
                }
                DBCallCommon.ExecuteTrans(list000);
            }
            List<string> list = new List<string>();
            string bz_bh = "BZ" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() +Session["UserID"].ToString().Trim();
            string FilePath = @"E:\班组岗位绩效表\";
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件类型不符合要求，请您核对后重新上传！');", true);
                    ModalPopupExtenderSearch.Hide();
                    return;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件上传过程中出现错误！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
                //根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);

                //获取第一个工作表
                ISheet sheet1 = wk.GetSheetAt(0);
                ISheet sheet2 = wk.GetSheetAt(1);
                //辅助班组
                for (int i = 2; i < sheet1.LastRowNum+1; i++)
                {
                    string stidfzbz = "";
                    IRow rowfzbz = sheet1.GetRow(i);
                    ICell cellfzbz01 = rowfzbz.GetCell(1);
                    string strnamecheck = "";
                    try
                    {
                        strnamecheck = cellfzbz01.ToString().Trim();
                    }
                    catch
                    {
                        strnamecheck = "";
                    }
                    if (strnamecheck != "")
                    {
                        string sqlfzbz = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + strnamecheck + "'";
                        System.Data.DataTable dtfzbz = DBCallCommon.GetDTUsingSqlText(sqlfzbz);
                        if (dtfzbz.Rows.Count > 0)
                        {
                            string sqlfzbzinsert = "";
                            stidfzbz = dtfzbz.Rows[0]["ST_ID"].ToString().Trim();
                            sqlfzbzinsert = "'"+bz_bh+"','" + stidfzbz + "','" + tb_yearmonth.Text.ToString().Trim() + "'";
                            for (int k = 23; k <27; k++)
                            {
                                string strfzbz = "";
                                ICell cellfzbz = rowfzbz.GetCell(k);
                                try
                                {
                                    strfzbz = cellfzbz.NumericCellValue.ToString().Trim();
                                }
                                catch
                                {
                                    try
                                    {
                                        strfzbz = cellfzbz.ToString().Trim();
                                    }
                                    catch
                                    {
                                        strfzbz = "";
                                    }
                                }
                                sqlfzbzinsert += ",'" + CommonFun.ComTryDecimal(strfzbz) + "'";
                            }
                            sqlfzbzinsert += ",'0'";
                            string sqltextfzbzinsert = "insert into OM_SCCZ_FZBZ(FZBZ_BH,FZBZ_STID,FZBZ_YEARMONTH,FZBZ_JXGZ,FZBZ_GWGZ,FZBZ_QT,FZBZ_JBGZ,TYPE) values(" + sqlfzbzinsert + ")";
                            list.Add(sqltextfzbzinsert);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //一线
                for (int j = 1; j < sheet2.LastRowNum+1; j++)
                {
                    string stidyx = "";
                    IRow rowyx = sheet2.GetRow(j);
                    ICell cellyx01 = rowyx.GetCell(1);
                    string yxnamecheck = "";
                    try
                    {
                        yxnamecheck = cellyx01.ToString().Trim();
                    }
                    catch
                    {
                        yxnamecheck = "";
                    }
                    if (yxnamecheck != "")
                    {
                        string sqlyx = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + yxnamecheck + "'";
                        System.Data.DataTable dtyx = DBCallCommon.GetDTUsingSqlText(sqlyx);
                        if (dtyx.Rows.Count > 0)
                        {
                            string sqlyxinsert = "";
                            stidyx = dtyx.Rows[0]["ST_ID"].ToString().Trim();
                            sqlyxinsert = "'"+bz_bh+"','" + stidyx + "','" + tb_yearmonth.Text.ToString().Trim() + "'";
                            for (int m = 3; m < 6; m++)
                            {
                                string stryx = "";
                                ICell cellyx = rowyx.GetCell(m);
                                try
                                {
                                    stryx = cellyx.NumericCellValue.ToString().Trim();
                                }
                                catch
                                {
                                    try
                                    {
                                        stryx = cellyx.ToString().Trim();
                                    }
                                    catch
                                    {
                                        stryx = "";
                                    }
                                }
                                sqlyxinsert += ",'" + CommonFun.ComTryDecimal(stryx) + "'";
                            }
                            string sqltextyxinsert = "insert into OM_SCCZ_YIXIAN(YIXIAN_BH,YIXIAN_ST_ID,YIXIAN_YEARMONTH,YIXIAN_JXGZ,YIXIAN_GWGZ,YIXIAN_QT) values(" + sqlyxinsert + ")";
                            list.Add(sqltextyxinsert);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            string sqltotal = "insert into OM_SCCZSH_TOTAL(SCCZTOL_BH,SCCZTOL_NY,SCCZTOL_ZDRID,SCCZTOL_ZDRNAME,SCCZTOL_ZDTIME,SCCZTOL_TOLSTATE,SCCZTOL_NOTE,TOLTYPE) values('" + bz_bh + "','" + tb_yearmonth.Text.Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + tb_NOTE.Text.Trim() + "','0')";
            list.Add(sqltotal);
            DBCallCommon.ExecuteTrans(list);

            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            UCPaging1.CurrentPage = 1;
            bindrpt();
            Response.Redirect("OM_SCCZSP_DETAIL.aspx?bh=" + bz_bh);
        }



        //班组长导入
        protected void QueryButton2_Click(object sender, EventArgs e)
        {
            List<string> list001 = new List<string>();
            if (tb_yearmonth2.Text.ToString().Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写年月！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            string sqlifscgz = "select * from OM_GZHSB where GZ_YEARMONTH='" + tb_yearmonth2.Text.ToString().Trim() + "' and (OM_GZSCBZ='0' or OM_GZSCBZ='1')";
            System.Data.DataTable dtisscgz = DBCallCommon.GetDTUsingSqlText(sqlifscgz);
            if (dtisscgz.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            string sqlifspz = "select * from OM_SCCZSH_TOTAL where TOLTYPE='1' and (SCCZTOL_TOLSTATE='1' or SCCZTOL_TOLSTATE='2') and SCCZTOL_NY='" + tb_yearmonth2.Text.Trim() + "'";
            System.Data.DataTable dtifspz = DBCallCommon.GetDTUsingSqlText(sqlifspz);
            if (dtifspz.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已有数据审批中或已通过！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            string sqlwtj = "select * from OM_SCCZSH_TOTAL where SCCZTOL_TOLSTATE='0' and TOLTYPE='1' and SCCZTOL_NY='" + tb_yearmonth2.Text.Trim() + "'";
            System.Data.DataTable dtwtj = DBCallCommon.GetDTUsingSqlText(sqlwtj);
            if (dtwtj.Rows.Count > 0)
            {
                string sqldelete0 = "delete from OM_SCCZSH_TOTAL where SCCZTOL_TOLSTATE='0' and TOLTYPE='1' and SCCZTOL_NY='" + tb_yearmonth2.Text.Trim() + "'";
                list001.Add(sqldelete0);
                for (int n = 0; n < dtwtj.Rows.Count; n++)
                {
                    string sqldelete1 = "delete from OM_SCCZ_FZBZ where TYPE='1' and FZBZ_BH='" + dtwtj.Rows[n]["SCCZTOL_BH"].ToString().Trim() + "'";
                    list001.Add(sqldelete1);
                }
                DBCallCommon.ExecuteTrans(list001);
            }
            List<string> list = new List<string>();
            string bz_bh = "BZ" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + Session["UserID"].ToString().Trim();
            string FilePath = @"E:\班组岗位绩效表班组长\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //将文件上传到服务器
            HttpPostedFile UserHPF = FileUpload2.PostedFile;
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件类型不符合要求，请您核对后重新上传！');", true);
                    ModalPopupExtenderSearch.Hide();
                    return;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件上传过程中出现错误！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }


            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
                //根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);

                //获取第一个工作表
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 2; i < sheet1.LastRowNum + 1; i++)
                {
                    string stidfzbz = "";
                    IRow rowfzbz = sheet1.GetRow(i);
                    ICell cellfzbz01 = rowfzbz.GetCell(1);
                    string strnamecheck = "";
                    try
                    {
                        strnamecheck = cellfzbz01.ToString().Trim();
                    }
                    catch
                    {
                        strnamecheck = "";
                    }
                    if (strnamecheck != "")
                    {
                        string sqlfzbz = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + strnamecheck + "'";
                        System.Data.DataTable dtfzbz = DBCallCommon.GetDTUsingSqlText(sqlfzbz);
                        if (dtfzbz.Rows.Count > 0)
                        {
                            string sqlfzbzinsert = "";
                            stidfzbz = dtfzbz.Rows[0]["ST_ID"].ToString().Trim();
                            sqlfzbzinsert = "'" + bz_bh + "','" + stidfzbz + "','" + tb_yearmonth2.Text.ToString().Trim() + "'";
                            for (int k = 23; k < 27; k++)
                            {
                                string strfzbz = "";
                                ICell cellfzbz = rowfzbz.GetCell(k);
                                try
                                {
                                    strfzbz = cellfzbz.NumericCellValue.ToString().Trim();
                                }
                                catch
                                {
                                    try
                                    {
                                        strfzbz = cellfzbz.ToString().Trim();
                                    }
                                    catch
                                    {
                                        strfzbz = "";
                                    }
                                }
                                sqlfzbzinsert += ",'" + CommonFun.ComTryDecimal(strfzbz) + "'";
                            }
                            sqlfzbzinsert += ",'1'";
                            string sqltextfzbzinsert = "insert into OM_SCCZ_FZBZ(FZBZ_BH,FZBZ_STID,FZBZ_YEARMONTH,FZBZ_JXGZ,FZBZ_GWGZ,FZBZ_QT,FZBZ_JBGZ,TYPE) values(" + sqlfzbzinsert + ")";
                            list.Add(sqltextfzbzinsert);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            string sqltotal = "insert into OM_SCCZSH_TOTAL(SCCZTOL_BH,SCCZTOL_NY,SCCZTOL_ZDRID,SCCZTOL_ZDRNAME,SCCZTOL_ZDTIME,SCCZTOL_TOLSTATE,SCCZTOL_NOTE,TOLTYPE) values('" + bz_bh + "','" + tb_yearmonth2.Text.Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + tb_NOTE2.Text.Trim() + "','1')";
            list.Add(sqltotal);
            DBCallCommon.ExecuteTrans(list);

            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            UCPaging1.CurrentPage = 1;
            bindrpt();
            Response.Redirect("OM_SCCZSP_DETAIL.aspx?bh=" + bz_bh);
        }

        //取消
        protected void btnClose2_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
    }
}
