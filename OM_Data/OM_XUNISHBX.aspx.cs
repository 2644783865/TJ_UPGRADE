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
    public partial class OM_XUNISHBX : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                {
                    btnSave.Visible = false;
                    btnDelete.Visible = false;
                    FileUpload1.Visible = false;
                    btnimport.Visible = false;
                    btnexport.Visible = false;
                }
                BindbmData();
                this.BindYearMoth(dplYear, dplMoth);

                this.InitPage();
                UCPaging1.CurrentPage = 1;

                InitVar();
                bindGrid();
            }
            CheckUser(ControlFinder);
            InitVar();
        }


        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            txtname.Text = "";
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
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)
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
        }


        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-(i - 2)).Year.ToString(), DateTime.Now.AddYears(-(i - 2)).Year.ToString()));
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


        private void BindbmData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(19, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }


        #region
        PagerQueryParam pager_org = new PagerQueryParam();
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
        private void InitPager()
        {
            pager_org.TableName = "View_OM_SHBX";
            pager_org.PrimaryKey = "SH_STID";
            pager_org.ShowFields = "*,(isnull(SH_QYYLDW,0)+isnull(SH_SYBXDW,0)+isnull(SH_JBYLDW,0)+isnull(SH_GSDW,0)+isnull(SH_SYDW,0)+isnull(SH_QYYLDWB,0)+isnull(SH_SYBXDWB,0)+isnull(SH_JBYLDWB,0)+isnull(SH_GSDWB,0)+isnull(SH_SYDWB,0)) as SH_DWBXHJ,(isnull(SH_QYYLGR,0)+isnull(SH_SYBXGR,0)+isnull(SH_JBYLGR,0)+isnull(SH_QYYLGRB,0)+isnull(SH_SYBXGRB,0)+isnull(SH_JBYLGRB,0)+isnull(SH_DEYLGR,0)) as SH_GRBXHJ,(isnull(SH_QYYLDW,0)+isnull(SH_SYBXDW,0)+isnull(SH_JBYLDW,0)+isnull(SH_GSDW,0)+isnull(SH_SYDW,0)+isnull(SH_QYYLDWB,0)+isnull(SH_SYBXDWB,0)+isnull(SH_JBYLDWB,0)+isnull(SH_GSDWB,0)+isnull(SH_SYDWB,0)+isnull(SH_QYYLGR,0)+isnull(SH_SYBXGR,0)+isnull(SH_JBYLGR,0)+isnull(SH_QYYLGRB,0)+isnull(SH_SYBXGRB,0)+isnull(SH_JBYLGRB,0)+isnull(SH_DEYLGR,0)+isnull(SH_QT,0)) as SH_GRXJ ";
            pager_org.OrderField = "DEP_NAME,SH_DATE,SH_STID";
            pager_org.StrWhere = Creatconstr();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 30;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
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

        }




        private string Creatconstr()
        {
            string sqlwhere = "1=1 and symbol='X'";
            string sqltotalnum = "select count(*) as totalnum from View_OM_SHBX where ";
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and SH_DATE='" + dplYear.SelectedValue + "-" + dplMoth.SelectedValue + "' ";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and SH_DATE like '%-" + dplMoth.SelectedValue.ToString().Trim() + "' ";
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                sqlwhere += " and SH_DATE like '" + dplYear.SelectedValue.ToString().Trim() + "-%' ";
            }
            else
            {
                sqlwhere += " and SH_DATE like '%%' ";
            }
            if (txtname.Text != "")
            {
                sqlwhere += " and (ST_NAME like '%" + txtname.Text.ToString().Trim() + "%' or ST_WORKNO like '%" + txtname.Text.ToString().Trim() + "%') ";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sqlwhere += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            if (tb_CXstarttime.Text.Trim() != "" && tb_CXendtime.Text.Trim() != "")
            {
                sqlwhere += " and SH_DATE>='" + tb_CXstarttime.Text.Trim() + "' and SH_DATE<='" + tb_CXendtime.Text.Trim() + "'";
            }
            sqltotalnum = sqltotalnum + sqlwhere;
            System.Data.DataTable dtnum = DBCallCommon.GetDTUsingSqlText(sqltotalnum);
            lbtotalnum.Text = dtnum.Rows[0]["totalnum"].ToString().Trim();
            return sqlwhere;
        }
        #endregion



        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资，不能修改！！！');", true);
                return;
            }
            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择修改数据的年月份！！！');", true);
                return;
            }
            int k = 0;//记录选中的行数
            for (int j = 0; j < rptProNumCost.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptProNumCost.Items[j].FindControl("CKBOX_SELECT")).Checked)
                {
                    k++;
                }
            }
            if (k > 0 && dplYear.SelectedIndex != 0 && dplYear.SelectedIndex != 0)
            {
                for (int i = 0; i < rptProNumCost.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptProNumCost.Items[i].FindControl("CKBOX_SELECT")).Checked)
                    {
                        string stid = ((System.Web.UI.WebControls.Label)rptProNumCost.Items[i].FindControl("lb_stid")).Text.Trim();

                        string tb_js = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_JS")).Text.Trim();
                        string tb_qyyldw = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_qyyldw")).Text.Trim();
                        string tb_sybxdw = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_sybxdw")).Text.Trim();
                        string tb_jbyldw = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_jbyldw")).Text.Trim();
                        string tb_gsdw = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_gsdw")).Text.Trim();
                        string tb_sydw = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_sydw")).Text.Trim();

                        string tb_qyyldwb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_qyyldwb")).Text.Trim();
                        string tb_sybxdwb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_sybxdwb")).Text.Trim();
                        string tb_jbyldwb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_jbyldwb")).Text.Trim();
                        string tb_gsdwb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_gsdwb")).Text.Trim();
                        string tb_sydwb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_sydwb")).Text.Trim();

                        string tb_qyylgr = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_qyylgr")).Text.Trim();
                        string tb_sybxgr = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_sybxgr")).Text.Trim();
                        string tb_jbylgr = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_jbylgr")).Text.Trim();

                        string tb_qyylgrb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_qyylgrb")).Text.Trim();
                        string tb_sybxgrb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_sybxgrb")).Text.Trim();
                        string tb_jbylgrb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_jbylgrb")).Text.Trim();
                        string tb_deylgr = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_deylgr")).Text.Trim();

                        string tb_shqt = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_shqt")).Text.Trim();

                        string str2 = "update OM_SHBX set SH_JS=" + CommonFun.ComTryDecimal(tb_js) + ",SH_QYYLDW=" + CommonFun.ComTryDecimal(tb_qyyldw) + ",SH_SYBXDW=" + CommonFun.ComTryDecimal(tb_sybxdw) + ",SH_JBYLDW=" + CommonFun.ComTryDecimal(tb_jbyldw) + ",SH_GSDW=" + CommonFun.ComTryDecimal(tb_gsdw) + ",SH_SYDW=" + CommonFun.ComTryDecimal(tb_sydw) + ",SH_QYYLDWB=" + CommonFun.ComTryDecimal(tb_qyyldwb) + ",SH_SYBXDWB=" + CommonFun.ComTryDecimal(tb_sybxdwb) + ",SH_JBYLDWB=" + CommonFun.ComTryDecimal(tb_jbyldwb) + ",SH_GSDWB=" + CommonFun.ComTryDecimal(tb_gsdwb) + ",SH_SYDWB=" + CommonFun.ComTryDecimal(tb_sydwb) + ",SH_QYYLGR=" + CommonFun.ComTryDecimal(tb_qyylgr) + ",SH_SYBXGR=" + CommonFun.ComTryDecimal(tb_sybxgr) + ",SH_JBYLGR=" + CommonFun.ComTryDecimal(tb_jbylgr) + ",";
                        str2 += " SH_QYYLGRB=" + CommonFun.ComTryDecimal(tb_qyylgrb) + ", ";
                        str2 += " SH_SYBXGRB=" + CommonFun.ComTryDecimal(tb_sybxgrb) + ",";
                        str2 += " SH_JBYLGRB=" + CommonFun.ComTryDecimal(tb_jbylgrb) + ",";
                        str2 += " SH_DEYLGR=" + CommonFun.ComTryDecimal(tb_deylgr) + ",SH_QT=" + CommonFun.ComTryDecimal(tb_shqt) + "";

                        str2 += " where SH_DATE='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' AND SH_STID='" + stid + "' and symbol='X'";
                        sql_list.Add(str2);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要修改的数据！！！');", true);
                return;
            }

            //更新
            DBCallCommon.ExecuteTrans(sql_list);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已保存！！！');", true);
            sql_list.Clear();

            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();

        }


        private string existif()
        {
            string strexist = "N";
            string sqlText = "select * from OM_SHBX where SH_DATE>='" + tb_StartTime.Text.ToString().Trim() + "' and SH_DATE<='" + tb_EndTime.Text.ToString().Trim() + "' and symbol='X'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                strexist = "Y";
            }
            return strexist;
        }

        //导入
        protected void btnimport_Click(object sender, EventArgs e)
        {
            if (QueryButton.Text == "确认" && existif() == "Y")
            {
                QueryButton.Text = "重新导入";
                message.Visible = true;
                message.Text = "提示：该月数据已存在,覆盖原数据请点击'重新导入'!";
                ModalPopupExtenderSearch.Show();
                return;
            }
            List<string> list = new List<string>();
            int num = 0;
            string strweidaoru = "";
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH>='" + tb_StartTime.Text.ToString().Trim() + "' and GZ_YEARMONTH<='" + tb_EndTime.Text.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('有月份已生成工资，不能导入！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            if (tb_StartTime.Text.ToString().Trim() != "" && tb_EndTime.Text.ToString().Trim() != "")
            {
                string sqlTextchk = "select * from OM_SHBX where SH_DATE>='" + tb_StartTime.Text.ToString().Trim() + "' and SH_DATE<='" + tb_EndTime.Text.ToString().Trim() + "' and symbol='X'";
                System.Data.DataTable dtchk = DBCallCommon.GetDTUsingSqlText(sqlTextchk);
                if (dtchk.Rows.Count > 0)
                {
                    string sqldelete = "delete from OM_SHBX where SH_DATE>='" + tb_StartTime.Text.ToString().Trim() + "' and SH_DATE<='" + tb_EndTime.Text.ToString().Trim() + "' and symbol='X'";
                    DBCallCommon.ExeSqlText(sqldelete);
                }
            }
            string FilePath = @"E:\社会保险表\";
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

                ISheet sheet1 = wk.GetSheetAt(0);
                decimal jsnum = 0;
                string sqlbl = "select * from OM_SHBXBL";
                System.Data.DataTable dtbl = DBCallCommon.GetDTUsingSqlText(sqlbl);
                if (dtbl.Rows.Count > 0)
                {
                    if (tb_StartTime.Text.ToString().Trim() != "" && tb_EndTime.Text.ToString().Trim() != "")
                    {
                        for (int i = 1; i < sheet1.LastRowNum + 1; i++)
                        {
                            string sh_stid = "";
                            string strcell02 = "";
                            IRow row = sheet1.GetRow(i);
                            ICell cell02 = row.GetCell(2);
                            try
                            {
                                strcell02 = cell02.ToString().Trim();
                            }
                            catch
                            {
                                strcell02 = "";
                            }
                            if (strcell02 != "")
                            {
                                ICell cell2 = row.GetCell(2);
                                string strcell2 = cell2.ToString().Trim();
                                string sqltext = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + strcell2 + "'";
                                System.Data.DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
                                if (dttext.Rows.Count > 0)
                                {
                                    sh_stid = dttext.Rows[0]["ST_ID"].ToString().Trim();
                                    ICell cell5 = row.GetCell(5);
                                    string strcell5 = "";
                                    try
                                    {
                                        strcell5 = cell5.NumericCellValue.ToString().Trim();
                                    }
                                    catch
                                    {
                                        strcell5 = cell5.ToString().Trim();
                                    }
                                    jsnum = CommonFun.ComTryDecimal(strcell5);
                                    DateTime datestart;
                                    DateTime dateend;
                                    datestart = DateTime.Parse(tb_StartTime.Text.ToString().Trim());
                                    dateend = DateTime.Parse(tb_EndTime.Text.ToString().Trim());
                                    int monthnum = (dateend.Year - datestart.Year) * 12 + dateend.Month - datestart.Month + 1;
                                    for (int j = 0; j < monthnum; j++)
                                    {
                                        string sql = "";
                                        sql += "'" + sh_stid + "'," + jsnum + "";//基数
                                        DateTime dateinsert = datestart.AddMonths(j);
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BL_QYYLBX"].ToString().Trim())) + "";//企业养老保险（单位）
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BL_SYBX"].ToString().Trim())) + "";//失业保险（单位）
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BL_JBYL"].ToString().Trim())) + "";//基本医疗保险门诊大额医疗（单位）
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BL_GSBX"].ToString().Trim())) + "";//工伤保险（单位）
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BL_SHENGYU"].ToString().Trim())) + "";//生育保险（单位）
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BL_QYYLGR"].ToString().Trim())) + "";//企业养老保险（个人）
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BL_SYBXGR"].ToString().Trim())) + "";//失业保险（个人）
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BL_JBYLGR"].ToString().Trim())) + "";//基本医疗保险门诊大额医疗（个人）
                                        sql += ",'" + dateinsert.ToString("yyyy-MM").Trim() + "'";//年月
                                        sql += ",'" + tb_StartTime.Text.Trim() + "—" + tb_EndTime.Text.Trim() + "','X'";//区间
                                        string sqltext0 = "insert into OM_SHBX(SH_STID,SH_JS,SH_QYYLDW,SH_SYBXDW,SH_JBYLDW,SH_GSDW,SH_SYDW,SH_QYYLGR,SH_SYBXGR,SH_JBYLGR,SH_DATE,SH_SJQJ,symbol) values(" + sql + ")";
                                        list.Add(sqltext0);
                                        num++;
                                    }
                                }
                                else
                                {
                                    strweidaoru = strweidaoru + "," + strcell2;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择起止年月！！！');", true);
                        ModalPopupExtenderSearch.Hide();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请添加缴费比例！！！');", true);
                    ModalPopupExtenderSearch.Hide();
                    return;
                }
            }
            DBCallCommon.ExecuteTrans(list);

            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            ModalPopupExtenderSearch.Hide();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导入成功,共导入" + num.ToString().Trim() + "条数据,以下数据未导入:" + strweidaoru + "！！！');", true);
        }


        //删除功能建议在使用时隐藏
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资，不能删除！！！');", true);
                return;
            }
            string sqltext = "";
            foreach (RepeaterItem LabelID in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)LabelID.FindControl("CKBOX_SELECT");
                if (chk.Checked)
                {
                    string check = ((System.Web.UI.WebControls.Label)LabelID.FindControl("lb_stid")).Text;
                    sqltext = "delete FROM OM_SHBX WHERE SH_STID='" + check + "' and SH_DATE='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' and symbol='X'";
                    sql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(sql);

            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }


        protected void dplMoth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }


        //取消
        protected void btnqx_import_Click(object sender, EventArgs e)
        {
            QueryButton.Text = "确认";
            message.Visible = false;
            message.Text = "";
            ModalPopupExtenderSearch.Hide();
        }

        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }


        protected void rptProNumCost_itemdatabind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select sum(SH_QYYLDW) as SH_QYYLDWhj,sum(SH_SYBXDW) as SH_SYBXDWhj,sum(SH_JBYLDW) as SH_JBYLDWhj,sum(SH_GSDW) as SH_GSDWhj,sum(SH_SYDW) as SH_SYDWhj,sum(SH_QYYLDWB) as SH_QYYLDWBhj,sum(SH_SYBXDWB) as SH_SYBXDWBhj,sum(SH_JBYLDWB) as SH_JBYLDWBhj,sum(SH_GSDWB) as SH_GSDWBhj,sum(SH_SYDWB) as SH_SYDWBhj,(sum(SH_QYYLDW)+sum(SH_SYBXDW)+sum(SH_JBYLDW)+sum(SH_GSDW)+sum(SH_SYDW)+sum(SH_QYYLDWB)+sum(SH_SYBXDWB)+sum(SH_JBYLDWB)+sum(SH_GSDWB)+sum(SH_SYDWB)) as SH_DWBXHJhj,sum(SH_QYYLGR) as SH_QYYLGRhj,sum(SH_SYBXGR) as SH_SYBXGRhj,sum(SH_JBYLGR) as SH_JBYLGRhj,sum(SH_QYYLGRB) as SH_QYYLGRBhj,sum(SH_SYBXGRB) as SH_SYBXGRBhj,sum(SH_JBYLGRB) as SH_JBYLGRBhj,sum(SH_DEYLGR) as SH_DEYLGRhj,convert(decimal(9,4),sum(SH_QYYLGR))+convert(decimal(9,4),sum(SH_SYBXGR))+convert(decimal(9,4),sum(SH_JBYLGR))+convert(decimal(9,4),sum(SH_DEYLGR))+convert(decimal(9,4),sum(SH_QYYLGRB))+convert(decimal(9,4),sum(SH_SYBXGRB))+convert(decimal(9,4),sum(SH_JBYLGRB))as SH_GRBXHJhj,sum(SH_QT) as SH_QThj,convert(decimal(9,4),sum(SH_QYYLDW))+convert(decimal(9,4),sum(SH_SYBXDW))+convert(decimal(9,4),sum(SH_JBYLDW))+convert(decimal(9,4),sum(SH_GSDW))+convert(decimal(9,4),sum(SH_SYDW))+convert(decimal(9,4),sum(SH_QYYLDWB))+convert(decimal(9,4),sum(SH_SYBXDWB))+convert(decimal(9,4),sum(SH_JBYLDWB))+convert(decimal(9,4),sum(SH_GSDWB))+convert(decimal(9,4),sum(SH_SYDWB))+convert(decimal(9,4),sum(SH_QYYLGR))+convert(decimal(9,4),sum(SH_QYYLGRB))+convert(decimal(9,4),sum(SH_DEYLGR))+convert(decimal(9,4),sum(SH_JBYLGR))+convert(decimal(9,4),sum(SH_SYBXGR))+convert(decimal(9,4),sum(SH_QT))+convert(decimal(9,4),sum(SH_JBYLGRB))+convert(decimal(9,4),sum(SH_SYBXGRB)) as SH_BXZJ from View_OM_SHBX where " + Creatconstr();

                System.Data.DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);
                if (dthj.Rows.Count > 0)
                {
                    string totalSH_QYYLDW = dthj.Rows[0]["SH_QYYLDWhj"].ToString().Trim();
                    string totalSH_SYBXDW = dthj.Rows[0]["SH_SYBXDWhj"].ToString().Trim();
                    string totalSH_JBYLDW = dthj.Rows[0]["SH_JBYLDWhj"].ToString().Trim();
                    string totalSH_GSDW = dthj.Rows[0]["SH_GSDWhj"].ToString().Trim();
                    string totalSH_SYDW = dthj.Rows[0]["SH_SYDWhj"].ToString().Trim();
                    string totalSH_QYYLDWB = dthj.Rows[0]["SH_QYYLDWBhj"].ToString().Trim();
                    string totalSH_SYBXDWB = dthj.Rows[0]["SH_SYBXDWBhj"].ToString().Trim();
                    string totalSH_JBYLDWB = dthj.Rows[0]["SH_JBYLDWBhj"].ToString().Trim();
                    string totalSH_GSDWB = dthj.Rows[0]["SH_GSDWBhj"].ToString().Trim();
                    string totalSH_SYDWB = dthj.Rows[0]["SH_SYDWBhj"].ToString().Trim();
                    string totalSH_DWBXHJ = dthj.Rows[0]["SH_DWBXHJhj"].ToString().Trim();
                    string totalSH_QYYLGR = dthj.Rows[0]["SH_QYYLGRhj"].ToString().Trim();
                    string totalSH_SYBXGR = dthj.Rows[0]["SH_SYBXGRhj"].ToString().Trim();
                    string totalSH_JBYLGR = dthj.Rows[0]["SH_JBYLGRhj"].ToString().Trim();
                    string totalSH_QYYLGRB = dthj.Rows[0]["SH_QYYLGRBhj"].ToString().Trim();
                    string totalSH_SYBXGRB = dthj.Rows[0]["SH_SYBXGRBhj"].ToString().Trim();
                    string totalSH_JBYLGRB = dthj.Rows[0]["SH_JBYLGRBhj"].ToString().Trim();
                    string totalSH_DEYLGR = dthj.Rows[0]["SH_DEYLGRhj"].ToString().Trim();
                    string totalSH_GRBXHJ = dthj.Rows[0]["SH_GRBXHJhj"].ToString().Trim();
                    string totalSH_QT = dthj.Rows[0]["SH_QThj"].ToString().Trim();
                    string totalSH_BXZJ = dthj.Rows[0]["SH_BXZJ"].ToString().Trim();



                    System.Web.UI.WebControls.Label lb_qyyldwhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_qyyldwhj");
                    System.Web.UI.WebControls.Label lb_sybxdwhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_sybxdwhj");
                    System.Web.UI.WebControls.Label lb_jbyldwhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_jbyldwhj");
                    System.Web.UI.WebControls.Label lb_gsdwhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_gsdwhj");
                    System.Web.UI.WebControls.Label lb_sydwhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_sydwhj");
                    System.Web.UI.WebControls.Label lb_qyyldwbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_qyyldwbhj");
                    System.Web.UI.WebControls.Label lb_sybxdwbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_sybxdwbhj");
                    System.Web.UI.WebControls.Label lb_jbyldwbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_jbyldwbhj");
                    System.Web.UI.WebControls.Label lb_gsdwbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_gsdwbhj");
                    System.Web.UI.WebControls.Label lb_sydwbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_sydwbhj");

                    System.Web.UI.WebControls.Label lb_bxgrzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_bxgrzj");

                    System.Web.UI.WebControls.Label lb_qyylgrhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_qyylgrhj");
                    System.Web.UI.WebControls.Label lb_sybxgrhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_sybxgrhj");
                    System.Web.UI.WebControls.Label lb_jbylgrhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_jbylgrhj");
                    System.Web.UI.WebControls.Label lb_qyylgrbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_qyylgrbhj");
                    System.Web.UI.WebControls.Label lb_sybxgrbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_sybxgrbhj");
                    System.Web.UI.WebControls.Label lb_jbylgrbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_jbylgrbhj");
                    System.Web.UI.WebControls.Label lb_deylgrhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_deylgrhj");

                    System.Web.UI.WebControls.Label lb_bxdwzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_bxdwzj");

                    System.Web.UI.WebControls.Label lb_shqthj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_shqthj");
                    System.Web.UI.WebControls.Label lb_grxjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_grxjhj");




                    lb_qyyldwhj.Text = totalSH_QYYLDW;
                    lb_sybxdwhj.Text = totalSH_SYBXDW;
                    lb_jbyldwhj.Text = totalSH_JBYLDW;
                    lb_gsdwhj.Text = totalSH_GSDW;
                    lb_sydwhj.Text = totalSH_SYDW;
                    lb_qyyldwbhj.Text = totalSH_QYYLDWB;
                    lb_sybxdwbhj.Text = totalSH_SYBXDWB;
                    lb_jbyldwbhj.Text = totalSH_JBYLDWB;
                    lb_gsdwbhj.Text = totalSH_GSDWB;
                    lb_sydwbhj.Text = totalSH_SYDWB;

                    lb_bxgrzj.Text = totalSH_DWBXHJ;

                    lb_qyylgrhj.Text = totalSH_QYYLGR;
                    lb_sybxgrhj.Text = totalSH_SYBXGR;
                    lb_jbylgrhj.Text = totalSH_JBYLGR;
                    lb_qyylgrbhj.Text = totalSH_QYYLGRB;
                    lb_sybxgrbhj.Text = totalSH_SYBXGRB;
                    lb_jbylgrbhj.Text = totalSH_JBYLGRB;
                    lb_deylgrhj.Text = totalSH_DEYLGR;

                    lb_bxdwzj.Text = Math.Round(CommonFun.ComTryDecimal(totalSH_GRBXHJ), 2, MidpointRounding.AwayFromZero).ToString();

                    lb_shqthj.Text = totalSH_QT;
                    lb_grxjhj.Text = Math.Round(CommonFun.ComTryDecimal(totalSH_BXZJ), 2, MidpointRounding.AwayFromZero).ToString();
                }
            }
        }


        //导出
        protected void btnexport_Click(object sender, EventArgs e)
        {
            string sqlshbx = "select SH_DATE,ST_WORKNO,ST_NAME,ST_CONTR,DEP_NAME,SH_JS,ST_REGIST,SH_QYYLDW,SH_SYBXDW,SH_JBYLDW,SH_GSDW,SH_SYDW,SH_QYYLDWB,SH_SYBXDWB,SH_JBYLDWB,SH_GSDWB,SH_SYDWB,(isnull(SH_QYYLDW,0)+isnull(SH_SYBXDW,0)+isnull(SH_JBYLDW,0)+isnull(SH_GSDW,0)+isnull(SH_SYDW,0)+isnull(SH_QYYLDWB,0)+isnull(SH_SYBXDWB,0)+isnull(SH_JBYLDWB,0)+isnull(SH_GSDWB,0)+isnull(SH_SYDWB,0)) as SH_DWBXHJ,SH_QYYLGR,SH_SYBXGR,SH_JBYLGR,SH_QYYLGRB,SH_SYBXGRB,SH_JBYLGRB,SH_DEYLGR,(isnull(SH_QYYLGR,0)+isnull(SH_SYBXGR,0)+isnull(SH_JBYLGR,0)+isnull(SH_QYYLGRB,0)+isnull(SH_SYBXGRB,0)+isnull(SH_JBYLGRB,0)+isnull(SH_DEYLGR,0)) as SH_GRBXHJ,SH_QT,(isnull(SH_QYYLDW,0)+isnull(SH_SYBXDW,0)+isnull(SH_JBYLDW,0)+isnull(SH_GSDW,0)+isnull(SH_SYDW,0)+isnull(SH_QYYLDWB,0)+isnull(SH_SYBXDWB,0)+isnull(SH_JBYLDWB,0)+isnull(SH_GSDWB,0)+isnull(SH_SYDWB,0)+isnull(SH_QYYLGR,0)+isnull(SH_SYBXGR,0)+isnull(SH_JBYLGR,0)+isnull(SH_QYYLGRB,0)+isnull(SH_SYBXGRB,0)+isnull(SH_JBYLGRB,0)+isnull(SH_DEYLGR,0)+isnull(SH_QT,0)) as SH_GRXJ,SH_NOTE from View_OM_SHBX where " + Creatconstr();
            System.Data.DataTable dtshbx = DBCallCommon.GetDTUsingSqlText(sqlshbx);
            string filename = "社会保险导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("社会保险导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtshbx.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dtshbx.Columns.Count; j++)
                    {
                        string str = dtshbx.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtshbx.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
