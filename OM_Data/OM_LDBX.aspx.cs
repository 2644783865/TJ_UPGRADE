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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Model;
using NPOI.SS.UserModel;
using ZCZJ_DPF.CommonClass;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_LDBX : BasicPage
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
                    hlUpdate.Visible = false;
                    hlBLXGJL.Visible = false;
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
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-(i-2)).Year.ToString(), DateTime.Now.AddYears(-(i-2)).Year.ToString()));
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
            System.Data.DataTable dt = DBCallCommon.GetPermeision(14, stId);
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
            pager_org.TableName = "View_OM_LDBX";
            pager_org.PrimaryKey = "LD_STID";
            pager_org.ShowFields = "*,(isnull(LD_YLBXD,0)+isnull(LD_SYBXD,0)+isnull(LD_GSBXD,0)+isnull(LD_SYD,0)+isnull(LD_YLD,0)+isnull(LD_GJJD,0)+isnull(LD_DWB,0)) as LD_DWH,(isnull(LD_YLGR,0)+isnull(LD_SYGR,0)+isnull(LD_JBYLGR,0)+isnull(LD_YLDE,0)) as LD_BXGRH,(isnull(LD_YLGR,0)+isnull(LD_SYGR,0)+isnull(LD_JBYLGR,0)+isnull(LD_YLDE,0)+isnull(LD_GJJGR,0)+isnull(LD_GRBJ,0)) as LD_HJGR,(isnull(LD_YLBXD,0)+isnull(LD_SYBXD,0)+isnull(LD_GSBXD,0)+isnull(LD_SYD,0)+isnull(LD_YLD,0)+isnull(LD_GJJD,0)+isnull(LD_DWB,0)+isnull(LD_YLGR,0)+isnull(LD_SYGR,0)+isnull(LD_JBYLGR,0)+isnull(LD_YLDE,0)+isnull(LD_GJJGR,0)+isnull(LD_GRBJ,0)+isnull(LD_ZGFY,0)) as LD_ZJGR ";
            pager_org.OrderField = "DEP_NAME,LD_STID,LD_DATE";
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
            string sqlwhere = "1=1";
            string sqltotalnum = "select count(*) as totalnum from View_OM_LDBX where ";
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and LD_DATE='" + dplYear.SelectedValue + "-" + dplMoth.SelectedValue + "' ";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and LD_DATE like '%-" + dplMoth.SelectedValue.ToString().Trim() + "' ";
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                sqlwhere += " and LD_DATE like '" + dplYear.SelectedValue.ToString().Trim() + "-%' ";
            }
            else
            {
                sqlwhere += " and LD_DATE like '%%' ";
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
                sqlwhere += " and LD_DATE>='" + tb_CXstarttime.Text.Trim() + "' and LD_DATE<='" + tb_CXendtime.Text.Trim() + "'";
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
            if (k > 0 && dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                for (int i = 0; i < rptProNumCost.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptProNumCost.Items[i].FindControl("CKBOX_SELECT")).Checked)
                    {
                        string stid = ((System.Web.UI.WebControls.Label)rptProNumCost.Items[i].FindControl("lbLD_STID")).Text.Trim();

                        string tbLD_JFJS = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_JFJS")).Text.Trim();
                        string tbLD_GJJS = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_GJJS")).Text.Trim();
                        string tbLD_YLBXD = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_YLBXD")).Text.Trim();
                        string tbLD_SYBXD = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_SYBXD")).Text.Trim();

                        string tbLD_GSBXD = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_GSBXD")).Text.Trim();
                        string tbLD_SYD = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_SYD")).Text.Trim();
                        string tbLD_YLD = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_YLD")).Text.Trim();
                        string tbLD_GJJD = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_GJJD")).Text.Trim();

                        string tbLD_DWB = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_DWB")).Text.Trim();
                        string tbLD_YLGR = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_YLGR")).Text.Trim();
                        string tbLD_SYGR = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_SYGR")).Text.Trim();
                        string tbLD_JBYLGR = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_JBYLGR")).Text.Trim();


                        string tbLD_YLDE = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_YLDE")).Text.Trim();
                        string tbLD_GJJGR = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_GJJGR")).Text.Trim();
                        string tbLD_GRBJ = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_GRBJ")).Text.Trim();
                        string tbLD_ZGFY = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_ZGFY")).Text.Trim();

                        string tbLD_BJLX = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tbLD_BJLX")).Text.Trim();


                        string str2 = "update OM_LDBX set LD_JFJS=" + CommonFun.ComTryDecimal(tbLD_JFJS) + ",LD_GJJS=" + CommonFun.ComTryDecimal(tbLD_GJJS) + ",LD_YLBXD=" + CommonFun.ComTryDecimal(tbLD_YLBXD) + ",LD_SYBXD=" + CommonFun.ComTryDecimal(tbLD_SYBXD) + ",";
                        str2 += " LD_GSBXD=" + CommonFun.ComTryDecimal(tbLD_GSBXD) + ",LD_SYD=" + CommonFun.ComTryDecimal(tbLD_SYD) + ",LD_YLD=" + CommonFun.ComTryDecimal(tbLD_YLD) + ",LD_GJJD=" + CommonFun.ComTryDecimal(tbLD_GJJD) + ",LD_DWB=" + CommonFun.ComTryDecimal(tbLD_DWB) + ",  ";
                        str2 += "LD_YLGR=" + CommonFun.ComTryDecimal(tbLD_YLGR) + ",LD_SYGR=" + CommonFun.ComTryDecimal(tbLD_SYGR) + ",LD_JBYLGR=" + CommonFun.ComTryDecimal(tbLD_JBYLGR) + ",LD_YLDE=" + CommonFun.ComTryDecimal(tbLD_YLDE) + ",  ";
                        str2 += "LD_GJJGR=" + CommonFun.ComTryDecimal(tbLD_GJJGR) + ",LD_GRBJ=" + CommonFun.ComTryDecimal(tbLD_GRBJ) + ",LD_ZGFY=" + CommonFun.ComTryDecimal(tbLD_ZGFY) + ",  ";
                        str2 += " LD_BJLX=" + CommonFun.ComTryDecimal(tbLD_BJLX) + "";
                        str2 += " where LD_DATE='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' AND LD_STID='" + stid + "'";
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

            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }


        private string existif()
        {
            string strexist = "N";
            string sqlText = "select * from OM_LDBX where LD_DATE>='" + tb_StartTime.Text.ToString().Trim() + "' and LD_DATE<='" + tb_EndTime.Text.ToString().Trim() + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                strexist = "Y";
            }
            return strexist;
        }
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
                string sqlTextchk = "select * from OM_LDBX where LD_DATE>='" + tb_StartTime.Text.ToString().Trim() + "' and LD_DATE<='" + tb_EndTime.Text.ToString().Trim() + "'";
                System.Data.DataTable dtchk = DBCallCommon.GetDTUsingSqlText(sqlTextchk);
                if (dtchk.Rows.Count > 0)
                {
                    string sqldelete = "delete from OM_LDBX where LD_DATE>='" + tb_StartTime.Text.ToString().Trim() + "' and LD_DATE<='" + tb_EndTime.Text.ToString().Trim() + "'";
                    DBCallCommon.ExeSqlText(sqldelete);
                }
            }


            string FilePath = @"E:\劳动保险及公积金表\";
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
                decimal bxjsnum = 0;
                decimal gjjsnum = 0;
                //招工费用
                decimal zhaogongfy=0;

                string sqlbl = "select * from OM_LDBXBL";
                System.Data.DataTable dtbl = DBCallCommon.GetDTUsingSqlText(sqlbl);
                if (dtbl.Rows.Count > 0)
                {
                    if (tb_StartTime.Text.ToString().Trim() != "" && tb_EndTime.Text.ToString().Trim() != "")
                    {
                        for (int i = 7; i < sheet1.LastRowNum + 1; i++)
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
                                    ICell cell4 = row.GetCell(4);
                                    ICell cell5 = row.GetCell(5);
                                    string strcell4 = "";
                                    string strcell5 = "";

                                    //招工费用
                                    ICell cell22 = row.GetCell(22);
                                    string strcell22 = "";
                                    try
                                    {
                                        strcell4 = cell4.NumericCellValue.ToString().Trim();
                                        strcell5 = cell5.NumericCellValue.ToString().Trim();

                                        //招工费用
                                        strcell22 = cell22.NumericCellValue.ToString().Trim();
                                    }
                                    catch
                                    {
                                        strcell4 = cell4.ToString().Trim();
                                        strcell5 = cell5.ToString().Trim();

                                        //招工费用
                                        strcell22 = cell22.ToString().Trim();
                                    }
                                    bxjsnum = CommonFun.ComTryDecimal(strcell4);
                                    gjjsnum = CommonFun.ComTryDecimal(strcell5);

                                    //招工费用
                                    zhaogongfy=CommonFun.ComTryDecimal(strcell22);

                                    DateTime datestart;
                                    DateTime dateend;
                                    datestart = DateTime.Parse(tb_StartTime.Text.ToString().Trim());
                                    dateend = DateTime.Parse(tb_EndTime.Text.ToString().Trim());
                                    int monthnum = (dateend.Year - datestart.Year) * 12 + dateend.Month - datestart.Month + 1;


                                    for (int j = 0; j < monthnum; j++)
                                    {
                                        string sql = "";
                                        sql += "'" + sh_stid + "'," + bxjsnum + "," + gjjsnum + "";//基数
                                        DateTime dateinsert = datestart.AddMonths(j);



                                        sql += "," + (bxjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_YLBXD"].ToString().Trim())) + "";
                                        sql += "," + (bxjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_SYBXD"].ToString().Trim())) + "";
                                        sql += "," + (bxjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_GSBXD"].ToString().Trim())) + "";
                                        sql += "," + (bxjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_SYD"].ToString().Trim())) + "";
                                        sql += "," + (bxjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_YLD"].ToString().Trim())) + "";
                                        sql += "," + (gjjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_GJJD"].ToString().Trim())) + "";
                                        sql += "," + (bxjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_YLGR"].ToString().Trim())) + "";
                                        sql += "," + (bxjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_SYGR"].ToString().Trim())) + "";
                                        sql += "," + (bxjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_JBYLGR"].ToString().Trim())) + "";
                                        sql += "," + Math.Round(gjjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_GJJGR"].ToString().Trim())) + "";
                                       // decimal t1 = Math.Round(gjjsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["BLLD_GJJGR"].ToString().Trim()));测试个人公积金整数
                                        //招工费用
                                        sql += "," + zhaogongfy + "";

                                        sql += ",'" + dateinsert.ToString("yyyy-MM").Trim() + "'";//年月
                                        sql += ",'" + tb_StartTime.Text.Trim() + "—" + tb_EndTime.Text.Trim() + "'";//区间
                                        string sqltext0 = "insert into OM_LDBX(LD_STID,LD_JFJS,LD_GJJS,LD_YLBXD,LD_SYBXD,LD_GSBXD,LD_SYD,LD_YLD,LD_GJJD,LD_YLGR,LD_SYGR,LD_JBYLGR,LD_GJJGR,LD_ZGFY,LD_DATE,LD_SJQJ) values(" + sql + ")";
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
                    string check = ((System.Web.UI.WebControls.Label)LabelID.FindControl("lbLD_STID")).Text;
                    sqltext = "delete FROM OM_LDBX WHERE LD_STID='" + check + "' and LD_DATE='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "'";
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
                string sqlhj = "select sum(LD_YLBXD) as LD_YLBXDhj,sum(LD_SYBXD) as LD_SYBXDhj,sum(LD_GSBXD) as LD_GSBXDhj,sum(LD_SYD) as LD_SYDhj,sum(LD_YLD) as LD_YLDhj,sum(LD_GJJD) as LD_GJJDhj,sum(LD_DWB) as LD_DWBhj,(sum(LD_YLBXD)+sum(LD_SYBXD)+sum(LD_GSBXD)+sum(LD_SYD)+sum(LD_YLD)+sum(cast(LD_GJJD as float))+sum(LD_DWB)) as LD_DWHhj,sum(LD_YLGR) as LD_YLGRhj,sum(LD_SYGR) as LD_SYGRhj,sum(LD_JBYLGR) as LD_JBYLGRhj,sum(LD_YLDE) as LD_YLDEhj,(sum(LD_YLGR)+sum(LD_SYGR)+sum(LD_JBYLGR)+sum(LD_YLDE)) as LD_BXGRHhj,sum(LD_GJJGR) as LD_GJJGRhj,sum(LD_GRBJ) as LD_GRBJhj,(sum(LD_YLGR)+sum(LD_SYGR)+sum(LD_JBYLGR)+sum(LD_YLDE)+sum(cast(LD_GJJGR as float))+sum(LD_GRBJ)) as LD_HJGRhj,sum(LD_ZGFY) as LD_ZGFYhj,sum(LD_BJLX) as LD_BJLXhj,(sum(LD_YLBXD)+sum(LD_SYBXD)+sum(LD_GSBXD)+sum(LD_SYD)+sum(LD_YLD)+sum(cast(LD_GJJD as float))+sum(LD_DWB)+sum(LD_YLGR)+sum(LD_SYGR)+sum(LD_JBYLGR)+sum(LD_YLDE)+sum(cast(LD_GJJGR as float))+sum(LD_GRBJ)+sum(LD_BJLX)+sum(LD_ZGFY)) as LD_ZJGRhj from View_OM_LDBX where " + Creatconstr();

                System.Data.DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);
                if (dthj.Rows.Count > 0)
                {
                    string totalLD_YLBXD = dthj.Rows[0]["LD_YLBXDhj"].ToString().Trim();
                    string totalLD_SYBXD = dthj.Rows[0]["LD_SYBXDhj"].ToString().Trim();
                    string totalLD_GSBXD = dthj.Rows[0]["LD_GSBXDhj"].ToString().Trim();
                    string totalLD_SYD = dthj.Rows[0]["LD_SYDhj"].ToString().Trim();
                    string totalLD_YLD = dthj.Rows[0]["LD_YLDhj"].ToString().Trim();
                    string totalLD_GJJD = dthj.Rows[0]["LD_GJJDhj"].ToString().Trim();
                    string totalLD_DWB = dthj.Rows[0]["LD_DWBhj"].ToString().Trim();
                    string totalLD_DWH = dthj.Rows[0]["LD_DWHhj"].ToString().Trim();
                    string totalLD_YLGR = dthj.Rows[0]["LD_YLGRhj"].ToString().Trim();
                    string totalLD_SYGR = dthj.Rows[0]["LD_SYGRhj"].ToString().Trim();
                    string totalLD_JBYLGR = dthj.Rows[0]["LD_JBYLGRhj"].ToString().Trim();
                    string totalLD_YLDE = dthj.Rows[0]["LD_YLDEhj"].ToString().Trim();
                    string totalLD_BXGRH = dthj.Rows[0]["LD_BXGRHhj"].ToString().Trim();
                    string totalLD_GJJGR = dthj.Rows[0]["LD_GJJGRhj"].ToString().Trim();
                    string totalLD_GRBJ = dthj.Rows[0]["LD_GRBJhj"].ToString().Trim();
                    string totalLD_HJGR = dthj.Rows[0]["LD_HJGRhj"].ToString().Trim();
                    string totalLD_ZGFY = dthj.Rows[0]["LD_ZGFYhj"].ToString().Trim();
                    string totalLD_BJLX = dthj.Rows[0]["LD_BJLXhj"].ToString().Trim();
                    string totalLD_ZJGR = dthj.Rows[0]["LD_ZJGRhj"].ToString().Trim();



                    System.Web.UI.WebControls.Label lb_LD_YLBXDhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_YLBXDhj");
                    System.Web.UI.WebControls.Label lb_LD_SYBXDhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_SYBXDhj");
                    System.Web.UI.WebControls.Label lb_LD_GSBXDhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_GSBXDhj");
                    System.Web.UI.WebControls.Label lb_LD_SYDhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_SYDhj");
                    System.Web.UI.WebControls.Label lb_LD_YLDhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_YLDhj");
                    System.Web.UI.WebControls.Label lb_LD_GJJDhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_GJJDhj");
                    System.Web.UI.WebControls.Label lb_LD_DWBhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_DWBhj");
                    System.Web.UI.WebControls.Label lb_LD_DWHhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_DWHhj");
                    System.Web.UI.WebControls.Label lb_LD_YLGRhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_YLGRhj");
                    System.Web.UI.WebControls.Label lb_LD_SYGRhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_SYGRhj");
                    System.Web.UI.WebControls.Label lb_LD_JBYLGRhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_JBYLGRhj");
                    System.Web.UI.WebControls.Label lb_LD_YLDEhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_YLDEhj");
                    System.Web.UI.WebControls.Label lb_LD_BXGRHhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_BXGRHhj");
                    System.Web.UI.WebControls.Label lb_LD_GJJGRhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_GJJGRhj");
                    System.Web.UI.WebControls.Label lb_LD_GRBJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_GRBJhj");
                    System.Web.UI.WebControls.Label lb_LD_HJGRhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_HJGRhj");
                    System.Web.UI.WebControls.Label lb_LD_ZGFYhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_ZGFYhj");
                    System.Web.UI.WebControls.Label lb_LD_BJLXhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_BJLXhj");
                    System.Web.UI.WebControls.Label lb_LD_ZJGRhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_LD_ZJGRhj");




                    lb_LD_YLBXDhj.Text = totalLD_YLBXD;
                    lb_LD_SYBXDhj.Text = totalLD_SYBXD;
                    lb_LD_GSBXDhj.Text = totalLD_GSBXD;
                    lb_LD_SYDhj.Text = totalLD_SYD;
                    lb_LD_YLDhj.Text = totalLD_YLD;
                    lb_LD_GJJDhj.Text = totalLD_GJJD;
                    lb_LD_DWBhj.Text = totalLD_DWB;
                    lb_LD_DWHhj.Text = totalLD_DWH;
                    lb_LD_YLGRhj.Text = totalLD_YLGR;
                    lb_LD_SYGRhj.Text = totalLD_SYGR;
                    lb_LD_JBYLGRhj.Text = totalLD_JBYLGR;
                    lb_LD_YLDEhj.Text = totalLD_YLDE;
                    lb_LD_BXGRHhj.Text = totalLD_BXGRH;
                    lb_LD_GJJGRhj.Text = totalLD_GJJGR;
                    lb_LD_GRBJhj.Text = totalLD_GRBJ;
                    lb_LD_HJGRhj.Text = totalLD_HJGR;
                    lb_LD_ZGFYhj.Text = totalLD_ZGFY;
                    lb_LD_BJLXhj.Text = totalLD_BJLX;
                    lb_LD_ZJGRhj.Text = totalLD_ZJGR;
                }
            }
        }

        //导出
         protected void btnexport_Click(object sender, EventArgs e)
        {
            string sqlldbx = "select DEP_NAME,ST_WORKNO,ST_NAME,LD_DATE,LD_JFJS,LD_GJJS,LD_YLBXD,LD_SYBXD,LD_GSBXD,LD_SYD,LD_YLD,LD_GJJD,LD_DWB,(isnull(LD_YLBXD,0)+isnull(LD_SYBXD,0)+isnull(LD_GSBXD,0)+isnull(LD_SYD,0)+isnull(LD_YLD,0)+isnull(LD_GJJD,0)+isnull(LD_DWB,0)) as LD_DWH,LD_YLGR,LD_SYGR,LD_JBYLGR,LD_YLDE,(isnull(LD_YLGR,0)+isnull(LD_SYGR,0)+isnull(LD_JBYLGR,0)+isnull(LD_YLDE,0)) as LD_BXGRH,LD_GJJGR,LD_GRBJ,(isnull(LD_YLGR,0)+isnull(LD_SYGR,0)+isnull(LD_JBYLGR,0)+isnull(LD_YLDE,0)+isnull(LD_GJJGR,0)+isnull(LD_GRBJ,0)) as LD_HJGR,LD_ZGFY,LD_BJLX,(isnull(LD_YLBXD,0)+isnull(LD_SYBXD,0)+isnull(LD_GSBXD,0)+isnull(LD_SYD,0)+isnull(LD_YLD,0)+isnull(LD_GJJD,0)+isnull(LD_DWB,0)+isnull(LD_YLGR,0)+isnull(LD_SYGR,0)+isnull(LD_JBYLGR,0)+isnull(LD_YLDE,0)+isnull(LD_GJJGR,0)+isnull(LD_GRBJ,0)+isnull(LD_ZGFY,0)) as LD_ZJGR from View_OM_LDBX where " + Creatconstr();
            System.Data.DataTable dtldbx = DBCallCommon.GetDTUsingSqlText(sqlldbx);
            string filename = "派遣人员保险公积金导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("派遣人员保险公积金导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtldbx.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dtldbx.Columns.Count; j++)
                    {
                        string str = dtldbx.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtldbx.Columns.Count; r++)
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
