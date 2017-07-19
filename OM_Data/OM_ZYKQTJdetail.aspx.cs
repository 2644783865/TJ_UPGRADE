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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ZYKQTJdetail : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        //string stid = "";
        //string nianyue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //try
                //{
                //    stid = Request.QueryString["stid"].ToString().Trim();
                //    nianyue = Request.QueryString["yearmonth"].ToString().Trim();
                //}
                //catch
                //{
                //    stid = "";
                //    nianyue = "";
                //}
                if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                {
                    FileUpload1.Visible = false;
                    btn_importclient.Visible = false;
                }
                BindbmData();
                bindddlbz();
                this.BindYearMoth(ddlYear, ddlMonth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
                titlebind();
                danyuangehebing();
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


        private void BindbmData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(10, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }

        private void bindddlbz()
        {
            string sql = "SELECT DISTINCT ST_DEPID1 FROM TBDS_STAFFINFO where ST_DEPID1!='' and ST_DEPID1 not in('0','1','2','3','4','5','6','7','8','9')";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlbz.DataTextField = "ST_DEPID1";
            ddlbz.DataValueField = "ST_DEPID1";
            ddlbz.DataSource = dt;
            ddlbz.DataBind();
            ListItem item = new ListItem("--请选择--", "--请选择--");
            ddlbz.Items.Insert(0, item);
        }

        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>

        private void InitPage()
        {
            txtName.Text = "";
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
            pager_org.TableName = "View_OM_KQTJZYdetail";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "MXZY_STID,MXZY_YEARMONTH";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 30;
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
                sql += " and MXZY_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            }
            //if (stid != "" && nianyue != "")
            //{
            //    sql += " and MXZY_STID='" + stid + "' and MXZY_YEARMONTH='" + nianyue + "'";
            //    ddlYear.SelectedValue = nianyue.Substring(0, 4);
            //    ddlMonth.SelectedValue = nianyue.Substring(5);
            //}
            if (txtName.Text != "")
            {
                sql += " and ST_NAME like '%" + txtName.Text.ToString().Trim() + "%'";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sql += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            if (ddlbz.SelectedIndex != 0)
            {
                sql += " and ST_DEPID1='" + ddlbz.SelectedValue + "'";
            }
            return sql;
        }
        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
            titlebind();
            danyuangehebing();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptKQTJ, UCPaging1, palNoData);
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

        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }

        protected void ddlbz_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }

        /// <summary>
        /// 年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }
        /// <summary>
        /// 月份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
        }



        private void titlebind()
        {
            string sqltext0 = "select BTZY_1,BTZY_2,BTZY_3,BTZY_4,BTZY_5,BTZY_6,BTZY_7,BTZY_8,BTZY_9,BTZY_10,BTZY_11,BTZY_12,BTZY_13,BTZY_14,BTZY_15,BTZY_16,BTZY_17,BTZY_18,BTZY_19,BTZY_20,BTZY_21,BTZY_22,BTZY_23,BTZY_24,BTZY_25,BTZY_26,BTZY_27,BTZY_28,BTZY_29,BTZY_30,BTZY_31 from OM_ZY_KQtitle where BTZY_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and BTZY_TYPE='0'";
             System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext0);
            string sqltext1 = "select BTZY_1,BTZY_2,BTZY_3,BTZY_4,BTZY_5,BTZY_6,BTZY_7,BTZY_8,BTZY_9,BTZY_10,BTZY_11,BTZY_12,BTZY_13,BTZY_14,BTZY_15,BTZY_16,BTZY_17,BTZY_18,BTZY_19,BTZY_20,BTZY_21,BTZY_22,BTZY_23,BTZY_24,BTZY_25,BTZY_26,BTZY_27,BTZY_28,BTZY_29,BTZY_30,BTZY_31 from OM_ZY_KQtitle where BTZY_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and BTZY_TYPE='1'";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            if (dt0.Rows.Count > 0 && dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Columns.Count; i++)
                {
                    ((System.Web.UI.WebControls.Label)rptKQTJ.Controls[0].FindControl(dt0.Columns[i].ToString().Trim())).Text = dt0.Rows[0][dt0.Columns[i].ToString().Trim()].ToString().Trim();
                }

                for (int i = 0; i < dt1.Columns.Count; i++)
                {
                    ((System.Web.UI.WebControls.Label)rptKQTJ.Controls[0].FindControl("WEEK" + dt1.Columns[i].ToString().Trim())).Text = dt1.Rows[0][dt1.Columns[i].ToString().Trim()].ToString().Trim();
                }
            }
        }



        private void danyuangehebing()
        {
            for (int i = rptKQTJ.Items.Count - 1; i > 0; i--)
            {
                HtmlTableCell oCell_previous0 = rptKQTJ.Items[i - 1].FindControl("td_ST_WORKNO") as HtmlTableCell;
                HtmlTableCell oCell0 = rptKQTJ.Items[i].FindControl("td_ST_WORKNO") as HtmlTableCell;

                HtmlTableCell oCell_previous1 = rptKQTJ.Items[i - 1].FindControl("td_ST_NAME") as HtmlTableCell;
                HtmlTableCell oCell1 = rptKQTJ.Items[i].FindControl("td_ST_NAME") as HtmlTableCell;

                HtmlTableCell oCell_previous2 = rptKQTJ.Items[i - 1].FindControl("td_DEP_NAME") as HtmlTableCell;
                HtmlTableCell oCell2 = rptKQTJ.Items[i].FindControl("td_DEP_NAME") as HtmlTableCell;

                HtmlTableCell oCell_previous3 = rptKQTJ.Items[i - 1].FindControl("td_ST_DEPID1") as HtmlTableCell;
                HtmlTableCell oCell3 = rptKQTJ.Items[i].FindControl("td_ST_DEPID1") as HtmlTableCell;

                HtmlTableCell oCell_previous4 = rptKQTJ.Items[i - 1].FindControl("td_MXZY_YEARMONTH") as HtmlTableCell;
                HtmlTableCell oCell4 = rptKQTJ.Items[i].FindControl("td_MXZY_YEARMONTH") as HtmlTableCell;

                HtmlTableCell oCell_previous5 = rptKQTJ.Items[i - 1].FindControl("td_MXZY_GZRCQ") as HtmlTableCell;
                HtmlTableCell oCell5 = rptKQTJ.Items[i].FindControl("td_MXZY_GZRCQ") as HtmlTableCell;

                oCell0.RowSpan = (oCell0.RowSpan == -1) ? 1 : oCell0.RowSpan;
                oCell_previous0.RowSpan = (oCell_previous0.RowSpan == -1) ? 1 : oCell_previous0.RowSpan;

                oCell1.RowSpan = (oCell1.RowSpan == -1) ? 1 : oCell1.RowSpan;
                oCell_previous1.RowSpan = (oCell_previous1.RowSpan == -1) ? 1 : oCell_previous1.RowSpan;

                oCell2.RowSpan = (oCell2.RowSpan == -1) ? 1 : oCell2.RowSpan;
                oCell_previous2.RowSpan = (oCell_previous2.RowSpan == -1) ? 1 : oCell_previous2.RowSpan;

                oCell3.RowSpan = (oCell3.RowSpan == -1) ? 1 : oCell3.RowSpan;
                oCell_previous3.RowSpan = (oCell_previous3.RowSpan == -1) ? 1 : oCell_previous3.RowSpan;

                oCell4.RowSpan = (oCell4.RowSpan == -1) ? 1 : oCell4.RowSpan;
                oCell_previous4.RowSpan = (oCell_previous4.RowSpan == -1) ? 1 : oCell_previous4.RowSpan;

                oCell5.RowSpan = (oCell5.RowSpan == -1) ? 1 : oCell5.RowSpan;
                oCell_previous5.RowSpan = (oCell_previous5.RowSpan == -1) ? 1 : oCell_previous5.RowSpan;

                if (oCell0.InnerText == oCell_previous0.InnerText)
                {
                    oCell0.Visible = false;
                    oCell_previous0.RowSpan += oCell0.RowSpan;

                    oCell1.Visible = false;
                    oCell_previous1.RowSpan += oCell1.RowSpan;

                    oCell2.Visible = false;
                    oCell_previous2.RowSpan += oCell2.RowSpan;

                    oCell3.Visible = false;
                    oCell_previous3.RowSpan += oCell3.RowSpan;

                    oCell4.Visible = false;
                    oCell_previous4.RowSpan += oCell4.RowSpan;

                    oCell5.Visible = false;
                    oCell_previous5.RowSpan += oCell5.RowSpan;
                }
            }
        }

        private string existif()
        {
            string strexist = "N";
            string sql = "select * from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='1'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                strexist = "Y";
            }
            return strexist;
        }
        //导入
        protected void btn_import_Click(object sender, EventArgs e)
        {
            if (QueryButton.Text == "确认" && existif() == "Y")
            {
                QueryButton.Text = "重新导入";
                message.Visible = true;
                message.Text = "提示：该月数据已存在,覆盖原数据请点击'重新导入'!";
                ModalPopupExtenderSearch.Show();
                return;
            }
            List<string> listsql = new List<string>();
            int num = 0;
            string strweidaoru = "";
            string sqlifscgz = "select * from OM_GZHSB where GZ_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
            System.Data.DataTable dtisscgz = DBCallCommon.GetDTUsingSqlText(sqlifscgz);
            if (dtisscgz.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资！！！');", true);
                return;
            }
            string sqldysj = "select * from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='1'";
            System.Data.DataTable dtdysj = DBCallCommon.GetDTUsingSqlText(sqldysj);
            if (dtdysj.Rows.Count > 0)
            {
                List<string> listdelete = new List<string>();
                string sqldrdeletenj = "";
                string sqldrnj = "select * from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='1'";
                System.Data.DataTable dtdrnj = DBCallCommon.GetDTUsingSqlText(sqldrnj);
                if (dtdrnj.Rows.Count > 0)
                {
                    for (int t = 0; t < dtdrnj.Rows.Count; t++)
                    {
                        sqldrdeletenj = "update OM_NianJiaTJ set NJ_YSY=NJ_YSY-" + CommonFun.ComTryDecimal(dtdrnj.Rows[t]["KQ_NIANX"].ToString().Trim()) + " where NJ_ST_ID='" + dtdrnj.Rows[t]["KQ_ST_ID"] + "'";
                        listdelete.Add(sqldrdeletenj);
                    }
                }
                string sqldeldykqsj = "delete from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='1'";
                string sqldeldytiyle = "delete from OM_ZY_KQtitle where BTZY_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                string sqldeldydetail = "delete from OM_ZY_KQdetail where MXZY_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                listdelete.Add(sqldeldykqsj);
                listdelete.Add(sqldeldytiyle);
                listdelete.Add(sqldeldydetail);
                DBCallCommon.ExecuteTrans(listdelete);
            }


            //导入数据
            string FilePath = @"E:\整月考勤明细\";
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
                if (tb_yearmonth.Text.Trim() != "")
                {
                    //导入表头
                    string sqltitledate = "";
                    string sqltitleweek = "";
                    string strdate = "";
                    string strweek = "";
                    IRow rowdateweek = sheet1.GetRow(3);
                    sqltitledate += "'" + tb_yearmonth.Text.Trim() + "','0'";
                    sqltitleweek += "'" + tb_yearmonth.Text.Trim() + "','1'";
                    for (int k = 6; k < 37; k++)
                    {
                        ICell celldateweek = rowdateweek.GetCell(6);
                        ICellStyle cellStyle = wk.CreateCellStyle();
                        IDataFormat format = wk.CreateDataFormat();
                        cellStyle.DataFormat = format.GetFormat("yyyy-MM-d");
                        celldateweek.CellStyle = cellStyle;
                        try
                        {

                            strdate = (DateTime.Parse(celldateweek.ToString().Trim())).AddDays(k - 6).ToString("yyyy-MM-d").Substring(8);
                            strweek = (DateTime.Parse(celldateweek.ToString().Trim())).AddDays(k - 6).ToString("ddd").Trim();
                        }
                        catch
                        {
                            strdate = "";
                            strweek = "";
                        }
                        sqltitledate += ",'" + strdate + "'";
                        sqltitleweek += ",'" + strweek + "'";
                    }
                    string sqlkqtitledate = "insert into OM_ZY_KQtitle(BTZY_YEARMONTH,BTZY_TYPE,BTZY_1,BTZY_2,BTZY_3,BTZY_4,BTZY_5,BTZY_6,BTZY_7,BTZY_8,BTZY_9,BTZY_10,BTZY_11,BTZY_12,BTZY_13,BTZY_14,BTZY_15,BTZY_16,BTZY_17,BTZY_18,BTZY_19,BTZY_20,BTZY_21,BTZY_22,BTZY_23,BTZY_24,BTZY_25,BTZY_26,BTZY_27,BTZY_28,BTZY_29,BTZY_30,BTZY_31) values(" + sqltitledate + ")";
                    string sqlkqtitleweek = "insert into OM_ZY_KQtitle(BTZY_YEARMONTH,BTZY_TYPE,BTZY_1,BTZY_2,BTZY_3,BTZY_4,BTZY_5,BTZY_6,BTZY_7,BTZY_8,BTZY_9,BTZY_10,BTZY_11,BTZY_12,BTZY_13,BTZY_14,BTZY_15,BTZY_16,BTZY_17,BTZY_18,BTZY_19,BTZY_20,BTZY_21,BTZY_22,BTZY_23,BTZY_24,BTZY_25,BTZY_26,BTZY_27,BTZY_28,BTZY_29,BTZY_30,BTZY_31) values(" + sqltitleweek + ")";
                    listsql.Add(sqlkqtitledate);
                    listsql.Add(sqlkqtitleweek);




                    //导入考勤明细
                    string strname = "";
                    string sqltext = "";
                    string kq_stid = "";
                    string strgzrcq = "";
                    for (int i = 6; i < sheet1.LastRowNum + 1; i++)
                    {
                        System.Data.DataTable dtfindstid;
                        if (i % 3 == 0)
                        {
                            sqltext = "";
                            strname = "";
                            strgzrcq = "";
                            IRow row03 = sheet1.GetRow(i);
                            ICell cell03 = row03.GetCell(3);
                            ICell cell38 = row03.GetCell(38);
                            try
                            {
                                strname = cell03.ToString().Trim();
                                strgzrcq = cell38.NumericCellValue.ToString().Trim();
                            }
                            catch
                            {
                                strname = "";
                                try
                                {
                                    strgzrcq = cell38.ToString().Trim();
                                }
                                catch
                                {
                                    strgzrcq = "";
                                }
                            }
                            if (strname != "")
                            {
                                string sqlfindstid = "select * from TBDS_STAFFINFO where ST_NAME='" + strname + "'";
                                dtfindstid = DBCallCommon.GetDTUsingSqlText(sqlfindstid);
                                if (dtfindstid.Rows.Count > 0)
                                {
                                    kq_stid = dtfindstid.Rows[0]["ST_ID"].ToString().Trim();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        sqltext = "'" + kq_stid.Trim() + "','" + tb_yearmonth.Text.Trim() + "'";
                        IRow rowdetail = sheet1.GetRow(i);
                        for (int m = 5; m < 37; m++)
                        {
                            string kq_detail = "";
                            ICell celldetail = rowdetail.GetCell(m);
                            try
                            {
                                kq_detail = celldetail.ToString().Trim();
                            }
                            catch
                            {
                                kq_detail = "";
                            }
                            sqltext += ",'" + kq_detail.Trim() + "'";
                        }
                        sqltext += ",'" + strgzrcq.Trim() + "'";
                        string sqlkqdetail = "insert into OM_ZY_KQdetail(MXZY_STID,MXZY_YEARMONTH,MXZY_SJD,MXZY_1,MXZY_2,MXZY_3,MXZY_4,MXZY_5,MXZY_6,MXZY_7,MXZY_8,MXZY_9,MXZY_10,MXZY_11,MXZY_12,MXZY_13,MXZY_14,MXZY_15,MXZY_16,MXZY_17,MXZY_18,MXZY_19,MXZY_20,MXZY_21,MXZY_22,MXZY_23,MXZY_24,MXZY_25,MXZY_26,MXZY_27,MXZY_28,MXZY_29,MXZY_30,MXZY_31,MXZY_GZRCQ) values(" + sqltext + ")";
                        listsql.Add(sqlkqdetail);
                    }


                    //导入考勤数据
                    for (int n = 6; n < sheet1.LastRowNum + 1; n += 3)
                    {
                        string kqtoatal_stid = "";
                        string strkqname = "";
                        string sqlkqdata = "";
                        IRow rowkqtotal = sheet1.GetRow(n);
                        ICell cell003 = rowkqtotal.GetCell(3);
                        try
                        {
                            strkqname = cell003.ToString().Trim();
                        }
                        catch
                        {
                            strkqname = "";
                        }
                        if (strkqname != "")
                        {
                            string sqlkqstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + strkqname + "'";
                            System.Data.DataTable dtkqstid = DBCallCommon.GetDTUsingSqlText(sqlkqstid);
                            if (dtkqstid.Rows.Count > 0)
                            {
                                kqtoatal_stid = dtkqstid.Rows[0]["ST_ID"].ToString().Trim();
                                sqlkqdata += "'" + kqtoatal_stid + "','" + tb_yearmonth.Text.Trim() + "'";
                                for (int t = 45; t < 74; t++)
                                {
                                    string strkqdata = "";
                                    ICell cellkqdata = rowkqtotal.GetCell(t);
                                    try
                                    {
                                        strkqdata = cellkqdata.NumericCellValue.ToString();
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            strkqdata = cellkqdata.ToString().Trim();
                                        }
                                        catch
                                        {
                                            strkqdata = "";
                                        }
                                    }
                                    sqlkqdata += ",'" + strkqdata + "'";
                                }
                                sqlkqdata += ",'1'";
                                string sqlkqdetail = "insert into OM_KQTJ(KQ_ST_ID,KQ_DATE,KQ_CHUQIN,KQ_GNCC,KQ_GWCC,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_BEIYONG1,KQ_BEIYONG2,KQ_BEIYONG3,KQ_BEIYONG4,KQ_BEIYONG5,KQ_BEIYONG6,KQ_QTJIA,KQ_JIEDIAO,KQ_ZMJBAN,KQ_JRJIAB,KQ_ZHIBAN,KQ_YEBAN,KQ_ZHONGB,KQ_CBTS,KQ_YSGZ,KQ_BEIZHU,KQ_TYPE) values(" + sqlkqdata + ")";
                                listsql.Add(sqlkqdetail);
                                num++;
                            }
                            else
                            {
                                strweidaoru = strweidaoru + "," + strkqname;
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择导入年月！！！');", true);
                    ModalPopupExtenderSearch.Hide();
                    return;
                }
            }

            DBCallCommon.ExecuteTrans(listsql);

            drupdateysynianj();//导入更新年假信息
            updatecbts();//餐补天数去小数

            nianjiatz();//因为达到第五条规定条件而调整年假的调整天数

            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindrpt();
            titlebind();
            danyuangehebing();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导入成功,共导入" + num.ToString().Trim() + "条数据,以下数据未导入:" + strweidaoru + "！！！');", true);
        }
        //餐补天数去小数
        private void updatecbts()
        {
            List<string> listcbts = new List<string>();
            string sqlcbts = "";
            string sqlgetdata = "select * from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.Trim() + "' and KQ_TYPE='1'";
            System.Data.DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
            if (dtgetdata.Rows.Count > 0)
            {
                for (int i = 0; i < dtgetdata.Rows.Count; i++)
                {
                    if(dtgetdata.Rows[i]["KQ_CBTS"].ToString().Trim().Contains("."))
                    {
                        sqlcbts = "update OM_KQTJ set KQ_CBTS=" + Math.Floor(CommonFun.ComTryDecimal(dtgetdata.Rows[i]["KQ_CBTS"].ToString().Trim())) + " where KQ_DATE='" + tb_yearmonth.Text.Trim() + "' and KQ_ST_ID='" + dtgetdata.Rows[i]["KQ_ST_ID"].ToString().Trim() + "' and KQ_TYPE='1'";
                        listcbts.Add(sqlcbts);
                    }
                }
                DBCallCommon.ExecuteTrans(listcbts);
            }
        }

        //导入更新年假信息
        private void drupdateysynianj()
        {
            List<string> listupdate = new List<string>();
            string sqlupdate = "";
            string sqlgetdata = "select KQ_ST_ID,sum(KQ_NIANX) as KQ_NIANXnew from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='1' group by KQ_ST_ID";
            System.Data.DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
            if (dtgetdata.Rows.Count > 0)
            {
                for (int i = 0; i < dtgetdata.Rows.Count; i++)
                {
                    sqlupdate = "update OM_NianJiaTJ set NJ_YSY=NJ_YSY+" + CommonFun.ComTryDecimal(dtgetdata.Rows[i]["KQ_NIANXnew"].ToString().Trim()) + " where NJ_ST_ID='" + dtgetdata.Rows[i]["KQ_ST_ID"] + "'";
                    listupdate.Add(sqlupdate);
                }
            }
            DBCallCommon.ExecuteTrans(listupdate);
        }
        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            QueryButton.Text = "确认";
            message.Visible = false;
            message.Text = "";
            ModalPopupExtenderSearch.Hide();
        }



        //因为达到第五条规定条件而调整年假的调整天数
        private void nianjiatz()
        {
            if (tb_yearmonth.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写截止月份！！！');", true);
                return;
            }

            if (CommonFun.ComTryInt(tb_yearmonth.Text.Trim().Substring(0, 4)) > CommonFun.ComTryInt(DateTime.Now.ToString("yyyy").Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所选年份不能超过当前年份！！！');", true);
                return;
            }
            string sqltexttz = "";

            string sqldetail = "";
            List<string> listsql = new List<string>();
            double yearnowtzdays = 0;//当年可休年假
            double yearnexttzdays = 0;//下一年可休年假

            //获取下一年份
            int nowyear = CommonFun.ComTryInt(tb_yearmonth.Text.Trim().Substring(0, 4));
            int nextyear = nowyear + 1;

            double shijiadays = 0;
            double bingjiadays = 0;
            double kuanggongdays = 0;
            double yishiyong = 0;

            //更新上一年需要在今年扣除的数据
            sqltexttz = "update OM_NianJiaTJ set NJ_QINGL=NJ_QINGL+NJ_TZDAYS,NJ_IFTZ='1',NJ_TZYEAR=NULL,NJ_TYPE='扣除上一年需要在当前年份扣除的年假' where NJ_IFTZ is null and NJ_TZYEAR='" + nowyear + "'";
            DBCallCommon.ExeSqlText(sqltexttz);

            sqltexttz = "insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_TZDAYS,'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',NJ_TYPE from OM_NianJiaTJ where NJ_IFTZ is null and NJ_TZYEAR='" + nowyear + "'";
            DBCallCommon.ExeSqlText(sqltexttz);
            //获取要循环的数据
            sqltexttz = "select * from (select * from OM_NianJiaTJ left join TBDS_STAFFINFO on OM_NianJiaTJ.NJ_ST_ID=TBDS_STAFFINFO.ST_ID)t where (ST_PD='0' or ST_PD='1' or ST_PD='4') and ((NJ_TZYEAR is null and NJ_IFTZ is null) or (NJ_IFTZ='1' and NJ_TZYEAR!='" + tb_yearmonth.Text.Trim().Substring(0, 4) + "' and NJ_TZYEAR is not null) or (NJ_IFTZ='1' and NJ_TZYEAR is null))";
            System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltexttz);
            if (dt0.Rows.Count > 0)
            {
                //循环数据
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    sqldetail = "select sum(KQ_BINGJ) as bingjiadays,sum(KQ_SHIJ) as shijiadays,sum(KQ_NIANX) as yishiyong,sum(KQ_KUANGG) as kuanggongdays from OM_KQTJ where KQ_DATE like '" + tb_yearmonth.Text.Trim().Substring(0, 5) + "%' and KQ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                    System.Data.DataTable dtdetail = DBCallCommon.GetDTUsingSqlText(sqldetail);
                    if (dtdetail.Rows.Count > 0)
                    {
                        shijiadays = CommonFun.ComTryDouble(dtdetail.Rows[0]["shijiadays"].ToString().Trim());
                        bingjiadays = CommonFun.ComTryDouble(dtdetail.Rows[0]["bingjiadays"].ToString().Trim());
                        kuanggongdays = CommonFun.ComTryDouble(dtdetail.Rows[0]["kuanggongdays"].ToString().Trim());
                        yishiyong = CommonFun.ComTryDouble(dtdetail.Rows[0]["yishiyong"].ToString().Trim());
                        //获取工龄
                        DateTime datemin;
                        DateTime datemax;
                        try
                        {
                            datemin = DateTime.Parse(dt0.Rows[i]["NJ_RUZHITIME"].ToString().Trim());
                            if (dt0.Rows[i]["NJ_LIZHITIME"].ToString().Trim() != "")
                            {
                                datemax = DateTime.Parse(dt0.Rows[i]["NJ_LIZHITIME"].ToString().Trim());
                            }
                            else
                            {
                                datemax = DateTime.Parse(tb_yearmonth.Text.Trim().Substring(0, 4) + ".12.30");
                            }
                        }
                        catch
                        {
                            datemin = DateTime.Parse(tb_yearmonth.Text.Trim().Substring(0, 4) + ".12.30");
                            datemax = DateTime.Parse(tb_yearmonth.Text.Trim().Substring(0, 4) + ".12.30");
                        }
                        double monthnum = datemax.Month - datemin.Month;
                        double yearnum = datemax.Year - datemin.Year;
                        double totalmonthlast = yearnum * 12 + monthnum - 12;
                        double totalmonthnum = yearnum * 12 + monthnum;
                        double totalmonthnext = yearnum * 12 + monthnum + 12;

                        double lastljkx = 0;
                        double nowljkx = 0;
                        double nextljkx = 0;
                        //计算上一年累计可休年假
                        //小于一年
                        if (totalmonthlast < 12)
                        {
                            lastljkx = 0;
                        }
                        //大于一年小于十年
                        else if (totalmonthlast >= 12 && totalmonthlast < 120)
                        {
                            lastljkx = Math.Floor((totalmonthlast - 12) * 5 / 12);
                        }
                        //大于十年小于二十年
                        else if (totalmonthlast >= 120 && totalmonthlast < 240)
                        {
                            lastljkx = Math.Floor((120 - 12) * 5 / 12 + (totalmonthlast - 120) * 10 / 12);
                        }
                        //二十年以上
                        else
                        {
                            lastljkx = Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthlast - 240) * 15 / 12);
                        }
                        //计算当年累计可休年假
                        if (totalmonthnum < 12)
                        {
                            nowljkx = 0;
                        }
                        //大于一年小于十年
                        else if (totalmonthnum >= 12 && totalmonthnum < 120)
                        {
                            nowljkx = Math.Floor((totalmonthnum - 12) * 5 / 12);
                        }
                        //大于十年小于二十年
                        else if (totalmonthnum >= 120 && totalmonthnum < 240)
                        {
                            nowljkx = Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12);
                        }
                        //二十年以上
                        else
                        {
                            nowljkx = Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12);
                        }
                        //计算下一年累计可休年假
                        if (totalmonthnext < 12)
                        {
                            nextljkx = 0;
                        }
                        //大于一年小于十年
                        else if (totalmonthnext >= 12 && totalmonthnext < 120)
                        {
                            nextljkx = Math.Floor((totalmonthnext - 12) * 5 / 12);
                        }
                        //大于十年小于二十年
                        else if (totalmonthnext >= 120 && totalmonthnext < 240)
                        {
                            nextljkx = Math.Floor((120 - 12) * 5 / 12 + (totalmonthnext - 120) * 10 / 12);
                        }
                        //二十年以上
                        else
                        {
                            nextljkx = Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnext - 240) * 15 / 12);
                        }

                        //得到当年可休年假和下一年可休年假
                        yearnowtzdays = nowljkx - lastljkx;
                        yearnexttzdays = nextljkx - nowljkx;

                        //事假超过20天
                        if (shijiadays >= 20)
                        {
                            if (yishiyong >= yearnowtzdays)
                            {
                                sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='事假累计20天,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                listsql.Add(sqltexttz);
                            }
                            else
                            {
                                sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='事假累计20天,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                listsql.Add(sqltexttz);

                                listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','事假累计20天,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                            }
                        }
                        //旷工超过3天
                        else if (kuanggongdays >= 3)
                        {
                            if (yishiyong >= yearnowtzdays)
                            {
                                sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='累计旷工3天及以上,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                listsql.Add(sqltexttz);
                            }
                            else
                            {
                                sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='累计旷工3天及以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                listsql.Add(sqltexttz);
                                listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','累计旷工3天及以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                            }
                        }
                        //病假时间超过上限(每月按20.83天算，最小单位0.5天)
                        else
                        {
                            //小于一年
                            if (totalmonthnum < 12)
                            {

                            }
                            //大于一年小于十年
                            else if (totalmonthnum >= 12 && totalmonthnum < 120)
                            {
                                if (bingjiadays >= 41.5)//两个月
                                {
                                    if (yishiyong >= yearnowtzdays)
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='病假累计2个月以上,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                    }
                                    else
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='病假累计2个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                        listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','病假累计2个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                                    }
                                }
                            }
                            //大于十年小于二十年
                            else if (totalmonthnum >= 120 && totalmonthnum < 240)
                            {
                                if (bingjiadays >= 62)//三个月
                                {
                                    if (yishiyong >= yearnowtzdays)
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='病假累计3个月以上,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                    }
                                    else
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='病假累计3个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                        listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','病假累计3个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                                    }
                                }
                            }
                            //二十年以上
                            else
                            {
                                if (bingjiadays >= 83)//四个月
                                {
                                    if (yishiyong >= yearnowtzdays)
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='病假累计4个月以上,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                    }
                                    else
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='病假累计4个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);

                                        listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','病假累计4个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                                    }
                                }
                            }
                        }
                    }
                }

                DBCallCommon.ExecuteTrans(listsql);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据！！！');", true);
                return;
            }
        }
    }
}
