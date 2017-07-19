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
    public partial class OM_Person_Hol : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"].ToString().Trim() != "150" && Session["UserName"].ToString().Trim() != "管理员")
                {
                    btnupdateinfo.Visible = false;
                    btnsave.Visible = false;
                    btnxglzsjclient.Visible = false;
                }
                if (Session["UserName"].ToString().Trim() == "管理员")
                {
                    btnupdateysy.Visible = true;
                }
                string sqltext = "select * from OM_QINGDATE";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    txtnjqlsj.Text = dt.Rows[0]["nextqingldate"].ToString().Trim();
                }
                BindbmData();
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            CheckUser(ControlFinder);
            this.InitVar();

        }

        /// <summary>
        /// 绑定部门
        /// </summary>
        /// <param name="ddl_Year"></param>
        /// <param name="ddl_Month"></param>
        private void BindbmData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(11, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }


        #region==============================分页======================================
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            txtName.Text = "";
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
            pager_org.TableName = "(select * from OM_NianJiaTJ left join TBDS_STAFFINFO on OM_NianJiaTJ.NJ_ST_ID=TBDS_STAFFINFO.ST_ID)t";
            pager_org.PrimaryKey = "NJ_ID";
            pager_org.ShowFields = "NJ_ID,NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_RUZHITIME,NJ_LIZHITIME,NJ_TZTS,NJ_YSY,NJ_QINGL,NJ_LASTQL,ST_CONTR,ST_SEQUEN,NJ_TYPE";
            pager_org.OrderField = "NJ_ST_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 25;
        }

        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (ddl_Depart.SelectedValue != "00")
            {
                sql += "and NJ_BUMENID='" + ddl_Depart.SelectedValue.ToString() + "'";
            }
            if (txtName.Text != "")
            {
                sql += "and NJ_NAME = '" + txtName.Text.Trim() + "'";
            }
            if (txt_rztimestart.Text.Trim() != "")
            {
                sql += " and NJ_RUZHITIME>='" + txt_rztimestart.Text.Trim() + "'";
            }
            if (txt_rztimeend.Text.Trim() != "")
            {
                sql += " and NJ_RUZHITIME<='" + txt_rztimeend.Text.Trim() + "'";
            }
            if (txt_lztimestart.Text.Trim() != "")
            {
                sql += " and NJ_LIZHITIME>='" + txt_lztimestart.Text.Trim() + "'";
            }
            if (txt_lztimeend.Text.Trim() != "")
            {
                sql += " and NJ_LIZHITIME<='" + txt_lztimeend.Text.Trim() + "'";
            }
            if (txt_ysymin.Text.Trim() != "")
            {
                sql += " and NJ_YSY>=" + CommonFun.ComTryDecimal(txt_ysymin.Text.Trim()) + "";
            }
            if (txt_ysymax.Text.Trim() != "")
            {
                sql += " and NJ_YSY<=" + CommonFun.ComTryDecimal(txt_ysymax.Text.Trim()) + "";
            }
            if (txt_htzt.Text.Trim() != "")
            {
                sql += " and ST_CONTR like '%" + txt_htzt.Text.Trim() + "%'";
            }
            if (txt_gwxl.Text.Trim() != "")
            {
                sql += " and ST_SEQUEN like '%" + txt_gwxl.Text.Trim() + "%'";
            }
            if (radio_symbol.Checked == true)
            {
                sql += " and NJ_ST_ID in(select KQ_ST_ID from (select KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where KQ_DATE like '" + DateTime.Now.Year.ToString().Trim() + "-%' group by KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1)t where (KQ_BINGJ+KQ_SHIJ)>=20)";
            }


            if (RadioButton_zaizhi.Checked == true)
            {
                sql += " and (ST_PD='0' or ST_PD='4')";
            }
            if (RadioButton_lizhi.Checked == true)
            {
                sql += " and ST_PD='1'";
            }
            //可休年假
            //大于等于
            if (txt_kxmin.Text.Trim() != "")
            {
                string str = "0";
                string kxts = "";
                string sqltext = "select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_RUZHITIME,NJ_LIZHITIME,NJ_TZTS,NJ_YSY,NJ_QINGL,NJ_LASTQL from OM_NianJiaTJ where " + sql;
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region
                        //计算年假
                        DateTime datemin;
                        DateTime datemax;
                        try
                        {
                            datemin = DateTime.Parse(dt.Rows[i]["NJ_RUZHITIME"].ToString().Trim());
                            if (dt.Rows[i]["NJ_LIZHITIME"].ToString().Trim() != "")
                            {
                                datemax = DateTime.Parse(dt.Rows[i]["NJ_LIZHITIME"].ToString().Trim());
                            }
                            else
                            {
                                datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            }
                        }
                        catch
                        {
                            datemin = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                        }
                        decimal monthnum = datemax.Month - datemin.Month;
                        decimal yearnum = datemax.Year - datemin.Year;
                        decimal totalmonthnum = yearnum * 12 + monthnum;
                        decimal ysynum = 0;
                        decimal qinglnum = 0;
                        try
                        {
                            ysynum = Convert.ToDecimal(dt.Rows[i]["NJ_YSY"].ToString().Trim());
                            qinglnum = Convert.ToDecimal(dt.Rows[i]["NJ_QINGL"].ToString().Trim());
                        }
                        catch
                        {
                            ysynum = 0;
                            qinglnum = 0;
                        }
                        //大于一年小于十年
                        if (totalmonthnum >= 12 && totalmonthnum < 120)
                        {
                            kxts = (Math.Floor((totalmonthnum - 12) * 5 / 12) - qinglnum).ToString().Trim();
                        }
                        //大于十年小于二十年
                        else if (totalmonthnum >= 120 && totalmonthnum < 240)
                        {
                            kxts = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12) - qinglnum).ToString().Trim();
                        }
                        //二十年以上
                        else if (totalmonthnum>=240)
                        {
                            kxts = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12) - qinglnum).ToString().Trim();
                        }
                        else
                        {
                            kxts = "0";
                        }
                        if (CommonFun.ComTryDecimal(kxts) >= CommonFun.ComTryDecimal(txt_kxmin.Text.Trim()))
                        {
                            str += ",'" + dt.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                        }
                    }
                        #endregion
                }
                sql += " and NJ_ST_ID in(" + str + ")";
            }
            //小于等于
            if (txt_kxmax.Text.Trim() != "")
            {
                string str = "0";
                string kxts = "";
                string sqltext = "select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_RUZHITIME,NJ_LIZHITIME,NJ_TZTS,NJ_YSY,NJ_QINGL,NJ_LASTQL from OM_NianJiaTJ where " + sql;
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region
                        //计算年假
                        DateTime datemin;
                        DateTime datemax;
                        try
                        {
                            datemin = DateTime.Parse(dt.Rows[i]["NJ_RUZHITIME"].ToString().Trim());
                            if (dt.Rows[i]["NJ_LIZHITIME"].ToString().Trim() != "")
                            {
                                datemax = DateTime.Parse(dt.Rows[i]["NJ_LIZHITIME"].ToString().Trim());
                            }
                            else
                            {
                                datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            }
                        }
                        catch
                        {
                            datemin = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                        }
                        decimal monthnum = datemax.Month - datemin.Month;
                        decimal yearnum = datemax.Year - datemin.Year;
                        decimal totalmonthnum = yearnum * 12 + monthnum;
                        decimal ysynum = 0;
                        decimal qinglnum = 0;
                        try
                        {
                            ysynum = Convert.ToDecimal(dt.Rows[i]["NJ_YSY"].ToString().Trim());
                            qinglnum = Convert.ToDecimal(dt.Rows[i]["NJ_QINGL"].ToString().Trim());
                        }
                        catch
                        {
                            ysynum = 0;
                            qinglnum = 0;
                        }
                        //大于一年小于十年
                        if (totalmonthnum >= 12 && totalmonthnum < 120)
                        {
                            kxts = (Math.Floor((totalmonthnum - 12) * 5 / 12) - qinglnum).ToString().Trim();
                        }
                        //大于十年小于二十年
                        else if (totalmonthnum >= 120 && totalmonthnum < 240)
                        {
                            kxts = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12) - qinglnum).ToString().Trim();
                        }
                        //二十年以上
                        else if (totalmonthnum >= 240)
                        {
                            kxts = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12) - qinglnum).ToString().Trim();
                        }
                        else
                        {
                            kxts = "0";
                        }
                        if (CommonFun.ComTryDecimal(kxts) <= CommonFun.ComTryDecimal(txt_kxmax.Text.Trim()))
                        {
                            str += ",'" + dt.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                        }
                    }
                        #endregion
                }
                sql += " and NJ_ST_ID in(" + str + ")";
            }
            //剩余年假
            //大于等于
            if (txt_symin.Text.Trim() != "")
            {
                string str2 = "0";
                string syts = "";
                string sqltext2 = "select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_RUZHITIME,NJ_LIZHITIME,NJ_TZTS,NJ_YSY,NJ_QINGL,NJ_LASTQL from OM_NianJiaTJ where " + sql;
                System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        #region
                        //计算年假
                        DateTime datemin2;
                        DateTime datemax2;
                        try
                        {
                            datemin2 = DateTime.Parse(dt2.Rows[i]["NJ_RUZHITIME"].ToString().Trim());
                            if (dt2.Rows[i]["NJ_LIZHITIME"].ToString().Trim() != "")
                            {
                                datemax2 = DateTime.Parse(dt2.Rows[i]["NJ_LIZHITIME"].ToString().Trim());
                            }
                            else
                            {
                                datemax2 = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            }
                        }
                        catch
                        {
                            datemin2 = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            datemax2 = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                        }
                        decimal monthnum2 = datemax2.Month - datemin2.Month;
                        decimal yearnum2 = datemax2.Year - datemin2.Year;
                        decimal totalmonthnum2 = yearnum2 * 12 + monthnum2;
                        decimal ysynum2 = 0;
                        decimal qinglnum2 = 0;
                        try
                        {
                            ysynum2 = Convert.ToDecimal(dt2.Rows[i]["NJ_YSY"].ToString().Trim());
                            qinglnum2 = Convert.ToDecimal(dt2.Rows[i]["NJ_QINGL"].ToString().Trim());
                        }
                        catch
                        {
                            ysynum2 = 0;
                            qinglnum2 = 0;
                        }
                        //大于一年小于十年
                        if (totalmonthnum2 >= 12 && totalmonthnum2 < 120)
                        {
                            syts = (Math.Floor((totalmonthnum2 - 12) * 5 / 12) - qinglnum2 - ysynum2).ToString().Trim();
                        }
                        //大于十年小于二十年
                        else if (totalmonthnum2 >= 120 && totalmonthnum2 < 240)
                        {
                            syts = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum2 - 120) * 10 / 12) - qinglnum2 - ysynum2).ToString().Trim();
                        }
                        //二十年以上
                        else if (totalmonthnum2 >= 240)
                        {
                            syts = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum2 - 240) * 15 / 12) - qinglnum2 - ysynum2).ToString().Trim();
                        }
                        else
                        {
                            syts = (-(qinglnum2 + ysynum2)).ToString().Trim();
                        }
                        if (CommonFun.ComTryDecimal(syts) >= CommonFun.ComTryDecimal(txt_symin.Text.Trim()))
                        {
                            str2 += ",'" + dt2.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                        }
                    }
                        #endregion
                }
                sql += " and NJ_ST_ID in(" + str2 + ")";
            }
            //小于等于
            if (txt_symax.Text.Trim() != "")
            {
                string str2 = "0";
                string syts = "";
                string sqltext2 = "select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_RUZHITIME,NJ_LIZHITIME,NJ_TZTS,NJ_YSY,NJ_QINGL,NJ_LASTQL from OM_NianJiaTJ where " + sql;
                System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        #region
                        //计算年假
                        DateTime datemin2;
                        DateTime datemax2;
                        try
                        {
                            datemin2 = DateTime.Parse(dt2.Rows[i]["NJ_RUZHITIME"].ToString().Trim());
                            if (dt2.Rows[i]["NJ_LIZHITIME"].ToString().Trim() != "")
                            {
                                datemax2 = DateTime.Parse(dt2.Rows[i]["NJ_LIZHITIME"].ToString().Trim());
                            }
                            else
                            {
                                datemax2 = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            }
                        }
                        catch
                        {
                            datemin2 = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            datemax2 = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                        }
                        decimal monthnum2 = datemax2.Month - datemin2.Month;
                        decimal yearnum2 = datemax2.Year - datemin2.Year;
                        decimal totalmonthnum2 = yearnum2 * 12 + monthnum2;
                        decimal ysynum2 = 0;
                        decimal qinglnum2 = 0;
                        try
                        {
                            ysynum2 = Convert.ToDecimal(dt2.Rows[i]["NJ_YSY"].ToString().Trim());
                            qinglnum2 = Convert.ToDecimal(dt2.Rows[i]["NJ_QINGL"].ToString().Trim());
                        }
                        catch
                        {
                            ysynum2 = 0;
                            qinglnum2 = 0;
                        }
                        //大于一年小于十年
                        if (totalmonthnum2 >= 12 && totalmonthnum2 < 120)
                        {
                            syts = (Math.Floor((totalmonthnum2 - 12) * 5 / 12) - qinglnum2 - ysynum2).ToString().Trim();
                        }
                        //大于十年小于二十年
                        else if (totalmonthnum2 >= 120 && totalmonthnum2 < 240)
                        {
                            syts = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum2 - 120) * 10 / 12) - qinglnum2 - ysynum2).ToString().Trim();
                        }
                        //二十年以上
                        else if (totalmonthnum2 >= 240)
                        {
                            syts = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum2 - 240) * 15 / 12) - qinglnum2 - ysynum2).ToString().Trim();
                        }
                        else
                        {
                            syts = (-(qinglnum2 + ysynum2)).ToString().Trim();
                        }
                        if (CommonFun.ComTryDecimal(syts) <= CommonFun.ComTryDecimal(txt_symax.Text.Trim()))
                        {
                            str2 += ",'" + dt2.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                        }
                    }
                        #endregion
                }
                sql += " and NJ_ST_ID in(" + str2 + ")";
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
            CommonFun.Paging(dt, rptHoliday, UCPaging1, palNoData);
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
            bindrpt();
        }

        protected void btnNameQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }


        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            foreach (Control contrl in palORG.Controls)
            {
                if (contrl is System.Web.UI.WebControls.TextBox)
                {
                    ((System.Web.UI.WebControls.TextBox)contrl).Text = "";
                }
            }
        }



        protected void btnupdateinfo_OnClick(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            string sqlstaffinfo = "select * from View_TBDS_STAFFINFO where ST_PD='0' or ST_LZSJ is not null or ST_PD='4'";
            System.Data.DataTable dtstaffinfo = DBCallCommon.GetDTUsingSqlText(sqlstaffinfo);
            if (dtstaffinfo.Rows.Count > 0)
            {
                for (int i = 0; i < dtstaffinfo.Rows.Count; i++)
                {
                    string sql0 = "select NJ_ST_ID from OM_NianJiaTJ where NJ_ST_ID='" + dtstaffinfo.Rows[i]["ST_ID"].ToString().Trim() + "'";
                    System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql0);
                    if (dt0.Rows.Count > 0)
                    {
                        string sqlupdate = "update OM_NianJiaTJ set NJ_NAME='" + dtstaffinfo.Rows[i]["ST_NAME"].ToString().Trim() + "',NJ_WORKNUMBER='" + dtstaffinfo.Rows[i]["ST_WORKNO"].ToString().Trim() + "',NJ_BUMENID='" + dtstaffinfo.Rows[i]["ST_DEPID"].ToString().Trim() + "',NJ_BUMEN='" + dtstaffinfo.Rows[i]["DEP_NAME"].ToString().Trim() + "',NJ_RUZHITIME='" + dtstaffinfo.Rows[i]["ST_INTIME"].ToString().Trim() + "',NJ_LIZHITIME='" + dtstaffinfo.Rows[i]["ST_LZSJ"].ToString().Trim() + "' where NJ_ST_ID='" + dtstaffinfo.Rows[i]["ST_ID"].ToString().Trim() + "'";
                        list_sql.Add(sqlupdate);
                    }
                    else
                    {
                        string sqladd = "insert into OM_NianJiaTJ(NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMENID,NJ_BUMEN,NJ_RUZHITIME,NJ_LIZHITIME) values('" + dtstaffinfo.Rows[i]["ST_ID"].ToString().Trim() + "','" + dtstaffinfo.Rows[i]["ST_NAME"].ToString().Trim() + "','" + dtstaffinfo.Rows[i]["ST_WORKNO"].ToString().Trim() + "','" + dtstaffinfo.Rows[i]["ST_DEPID"].ToString().Trim() + "','" + dtstaffinfo.Rows[i]["DEP_NAME"].ToString().Trim() + "','" + dtstaffinfo.Rows[i]["ST_INTIME"].ToString().Trim() + "','" + dtstaffinfo.Rows[i]["ST_LZSJ"].ToString().Trim() + "')";
                        list_sql.Add(sqladd);
                    }
                }
            }

            //删除后来离职人员
            //string sqllz = "select NJ_ST_ID from OM_NianJiaTJ where NJ_ST_ID not in(select ST_ID from View_TBDS_STAFFINFO where ST_PD='0' or ST_PD='4')";
            //System.Data.DataTable dtlz = DBCallCommon.GetDTUsingSqlText(sqllz);
            //if (dtlz.Rows.Count > 0)
            //{
            //    for (int j = 0; j < dtlz.Rows.Count; j++)
            //    {
            //        string sqldelete = "delete from OM_NianJiaTJ where NJ_ST_ID='" + dtlz.Rows[j]["NJ_ST_ID"].ToString().Trim() + "'";
            //        list_sql.Add(sqldelete);
            //    }
            //}
            DBCallCommon.ExecuteTrans(list_sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('更新完毕！！！');window.close;", true);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }



        protected void rptHoliday_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (Session["UserDeptID"].ToString().Trim() != "02")
                {
                    HtmlTableCell tzts = e.Item.FindControl("tzts") as HtmlTableCell;
                    tzts.Visible = false;
                    HtmlTableCell tdlook = e.Item.FindControl("tdlook") as HtmlTableCell;
                    tdlook.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lbST_LZSJ = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbST_LZSJ");
                if (lbST_LZSJ.Text.Trim() != "")
                {
                    HtmlTableCell tdlztime = e.Item.FindControl("tdlztime") as HtmlTableCell;
                    tdlztime.BgColor = "red";
                }
                System.Web.UI.WebControls.Label lbNJ_ST_ID = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbNJ_ST_ID");
                string sqlbingjianum = "select * from (select KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where KQ_DATE like '" + DateTime.Now.Year.ToString().Trim() + "-%' group by KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1)t where (KQ_BINGJ+KQ_SHIJ)>=20 and KQ_ST_ID='" + lbNJ_ST_ID.Text.Trim() + "'";
                System.Data.DataTable dtbingjianum = DBCallCommon.GetDTUsingSqlText(sqlbingjianum);
                if (dtbingjianum.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lbcheck = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcheck");
                    lbcheck.Visible = true;
                    lbcheck.BackColor = System.Drawing.Color.Red;
                }

                if (Session["UserDeptID"].ToString().Trim() != "02")
                {
                    HtmlTableCell tdtzts = e.Item.FindControl("tdtzts") as HtmlTableCell;
                    tdtzts.Visible = false;
                    HtmlTableCell lookjl = e.Item.FindControl("lookjl") as HtmlTableCell;
                    lookjl.Visible = false;
                }
                string njstid = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lbNJ_ST_ID")).Text.ToString().Trim();
                System.Web.UI.WebControls.Label lbkxts = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbNJ_KXTS");
                System.Web.UI.WebControls.Label lbljnj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbNJ_LEIJI");
                fupdatenj(njstid, lbkxts, lbljnj);
            }
        }
        private void fupdatenj(string st_id, System.Web.UI.WebControls.Label lbkxts, System.Web.UI.WebControls.Label lbljnj)
        {
            string sqltext = "select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_RUZHITIME,NJ_LIZHITIME,NJ_TZTS,NJ_YSY,NJ_QINGL,NJ_LASTQL from OM_NianJiaTJ where NJ_ST_ID='" + st_id.ToString().Trim() + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                #region
                //计算年假
                DateTime datemin;
                DateTime datemax;
                try
                {
                    datemin = DateTime.Parse(dt.Rows[0]["NJ_RUZHITIME"].ToString().Trim());
                    if (dt.Rows[0]["NJ_LIZHITIME"].ToString().Trim() != "")
                    {
                        datemax = DateTime.Parse(dt.Rows[0]["NJ_LIZHITIME"].ToString().Trim());
                    }
                    else
                    {
                        datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                    }
                }
                catch
                {
                    datemin = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                    datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                }
                decimal monthnum = datemax.Month - datemin.Month;
                decimal yearnum = datemax.Year - datemin.Year;
                decimal totalmonthnum = yearnum * 12 + monthnum;
                decimal ysynum = 0;
                decimal qinglnum = 0;
                try
                {
                    ysynum = Convert.ToDecimal(dt.Rows[0]["NJ_YSY"].ToString().Trim());
                    qinglnum = Convert.ToDecimal(dt.Rows[0]["NJ_QINGL"].ToString().Trim());
                }
                catch
                {
                    ysynum = 0;
                    qinglnum = 0;
                }
                //小于一年
                if (totalmonthnum < 12)
                {
                    lbkxts.Text = "0";
                    lbljnj.Text = (-(qinglnum + ysynum)).ToString().Trim();
                }
                //大于一年小于十年
                else if (totalmonthnum >= 12 && totalmonthnum < 120)
                {
                    lbkxts.Text = (Math.Floor((totalmonthnum - 12) * 5 / 12) - qinglnum).ToString().Trim();
                    lbljnj.Text = (Math.Floor((totalmonthnum - 12) * 5 / 12) - qinglnum - ysynum).ToString().Trim();
                }
                //大于十年小于二十年
                else if (totalmonthnum >= 120 && totalmonthnum < 240)
                {
                    lbkxts.Text = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12) - qinglnum).ToString().Trim();
                    lbljnj.Text = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12) - qinglnum - ysynum).ToString().Trim();
                }
                //二十年以上
                else
                {
                    lbkxts.Text = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12) - qinglnum).ToString().Trim();
                    lbljnj.Text = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12) - qinglnum - ysynum).ToString().Trim();
                }
                #endregion
            }
        }

        protected void btnsave_OnClick(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            int k = 0;//记录选中的行数
            for (int j = 0; j < rptHoliday.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptHoliday.Items[j].FindControl("cbxNumber")).Checked)
                {
                    k++;
                }
            }
            if (k > 0)
            {
                for (int i = 0; i < rptHoliday.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptHoliday.Items[i].FindControl("cbxNumber")).Checked)
                    {
                        string stid = ((System.Web.UI.WebControls.Label)rptHoliday.Items[i].FindControl("lbNJ_ST_ID")).Text.Trim();
                        string tbNJ_TZTS = ((System.Web.UI.WebControls.TextBox)rptHoliday.Items[i].FindControl("tbNJ_TZTS")).Text.Trim();
                        string strtzts = "update OM_NianJiaTJ set NJ_TZTS=" + CommonFun.ComTryDecimal(tbNJ_TZTS.ToString().Trim()) + " where NJ_ST_ID='" + stid + "'";
                        string strqlts = "update OM_NianJiaTJ set NJ_QINGL=NJ_QINGL-(" + (CommonFun.ComTryDecimal(tbNJ_TZTS.ToString().Trim())) + ") where NJ_ST_ID='" + stid + "'";
                        string sqlgetdata="select * from OM_NianJiaTJ where NJ_ST_ID='" + stid + "'";
                        System.Data.DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
                        if(dtgetdata.Rows.Count>0)
                        {
                            string sqlinsertjl = "insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME) values('" + dtgetdata.Rows[0]["NJ_ST_ID"].ToString().Trim() + "','" + dtgetdata.Rows[0]["NJ_NAME"].ToString().Trim() + "','" + dtgetdata.Rows[0]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dtgetdata.Rows[0]["NJ_BUMEN"].ToString().Trim() + "','" + dtgetdata.Rows[0]["NJ_BUMENID"].ToString().Trim() + "'," + CommonFun.ComTryDecimal(tbNJ_TZTS.ToString().Trim()) + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "')";
                            sql_list.Add(sqlinsertjl);
                        }
                        sql_list.Add(strtzts);
                        sql_list.Add(strqlts);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要调整的数据！！！');", true);
                return;
            }

            //更新
            DBCallCommon.ExecuteTrans(sql_list);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        //修改离职时间
        protected void btneditlztime_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            int k = 0;//记录选中的行数
            for (int j = 0; j < rptHoliday.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptHoliday.Items[j].FindControl("cbxNumber")).Checked)
                {
                    k++;
                }
            }
            if (k > 0)
            {
                for (int i = 0; i < rptHoliday.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptHoliday.Items[i].FindControl("cbxNumber")).Checked)
                    {
                        string stid = ((System.Web.UI.WebControls.Label)rptHoliday.Items[i].FindControl("lbNJ_ST_ID")).Text.Trim();
                        string sqlupdatelz = "update OM_NianJiaTJ set NJ_LIZHITIME='" + tblztime.Text.Trim() + "' where NJ_ST_ID='" + stid + "'";
                        sql_list.Add(sqlupdatelz);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选需要修改的数据！！！');", true);
                ModalPopupExtenderSearch2.Hide();
                return;
            }

            //更新
            DBCallCommon.ExecuteTrans(sql_list);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        //取消
        protected void btnclose1_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch1.Hide();
        }

        //取消
        protected void btnclose2_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch2.Hide();
        }

        //取消
        protected void btnclose3_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch3.Hide();
        }





        //根据考勤表更新已使用年假（仅管理员可见）
        protected void btnupdateysy_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sqlnjinfo = "select * from OM_NianJiaTJ";
            System.Data.DataTable dtnjinfo = DBCallCommon.GetDTUsingSqlText(sqlnjinfo);
            for (int i = 0; i < dtnjinfo.Rows.Count; i++)
            {
                string sqlkqinfo = "select sum(KQ_NIANX) as KQ_NIANXhj from OM_KQTJ where KQ_ST_ID='" + dtnjinfo.Rows[i]["NJ_ST_ID"].ToString().Trim() + "' and KQ_DATE>'" + dtnjinfo.Rows[i]["NJ_LASTQL"].ToString().Trim() + "' group by KQ_ST_ID";
                System.Data.DataTable dtkqinfo = DBCallCommon.GetDTUsingSqlText(sqlkqinfo);
                if (dtkqinfo.Rows.Count > 0 && (CommonFun.ComTryDecimal(dtkqinfo.Rows[0]["KQ_NIANXhj"].ToString().Trim())) > 0)
                {
                    string sqlupdate = "update OM_NianJiaTJ set NJ_YSY=" + CommonFun.ComTryDecimal(dtkqinfo.Rows[0]["KQ_NIANXhj"].ToString().Trim()) + " where NJ_ST_ID='" + dtnjinfo.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                    list.Add(sqlupdate);
                }
            }
            DBCallCommon.ExecuteTrans(list);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        #region =======================================================导出===============================================================
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_plexport_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,ST_CONTR,ST_SEQUEN,NJ_RUZHITIME,NJ_LIZHITIME,NJ_YSY,NJ_ST_ID from (select * from OM_NianJiaTJ left join TBDS_STAFFINFO on OM_NianJiaTJ.NJ_ST_ID=TBDS_STAFFINFO.ST_ID)t where " + StrWhere() + " order by NJ_BUMEN";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "年假统计" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("年假统计.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet

                string kxnj = "";
                string synj = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count-1; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }
                    #region
                    //计算年假
                    string sqltextjs = "select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_RUZHITIME,NJ_LIZHITIME,NJ_TZTS,NJ_YSY,NJ_QINGL,NJ_LASTQL from OM_NianJiaTJ where NJ_ST_ID='" + dt.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                    System.Data.DataTable dtjs = DBCallCommon.GetDTUsingSqlText(sqltextjs);
                    if (dtjs.Rows.Count > 0)
                    {
                        DateTime datemin;
                        DateTime datemax;
                        try
                        {
                            datemin = DateTime.Parse(dtjs.Rows[0]["NJ_RUZHITIME"].ToString().Trim());
                            if (dtjs.Rows[0]["NJ_LIZHITIME"].ToString().Trim() != "")
                            {
                                datemax = DateTime.Parse(dtjs.Rows[0]["NJ_LIZHITIME"].ToString().Trim());
                            }
                            else
                            {
                                datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            }
                        }
                        catch
                        {
                            datemin = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                            datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                        }
                        decimal monthnum = datemax.Month - datemin.Month;
                        decimal yearnum = datemax.Year - datemin.Year;
                        decimal totalmonthnum = yearnum * 12 + monthnum;
                        decimal ysynum = 0;
                        decimal qinglnum = 0;
                        try
                        {
                            ysynum = Convert.ToDecimal(dtjs.Rows[0]["NJ_YSY"].ToString().Trim());
                            qinglnum = Convert.ToDecimal(dtjs.Rows[0]["NJ_QINGL"].ToString().Trim());
                        }
                        catch
                        {
                            ysynum = 0;
                            qinglnum = 0;
                        }
                        //小于一年
                        if (totalmonthnum < 12)
                        {
                            kxnj = "0";
                            synj = (-(qinglnum + ysynum)).ToString().Trim();
                        }
                        //大于一年小于十年
                        else if (totalmonthnum >= 12 && totalmonthnum < 120)
                        {
                            kxnj = (Math.Floor((totalmonthnum - 12) * 5 / 12) - qinglnum).ToString().Trim();
                            synj = (Math.Floor((totalmonthnum - 12) * 5 / 12) - qinglnum - ysynum).ToString().Trim();
                        }
                        //大于十年小于二十年
                        else if (totalmonthnum >= 120 && totalmonthnum < 240)
                        {
                            kxnj = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12) - qinglnum).ToString().Trim();
                            synj = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12) - qinglnum - ysynum).ToString().Trim();
                        }
                        //二十年以上
                        else
                        {
                            kxnj = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12) - qinglnum).ToString().Trim();
                            synj = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12) - qinglnum - ysynum).ToString().Trim();
                        }
                    }
                    #endregion
                    row.CreateCell(dt.Columns.Count).SetCellValue(kxnj);
                    row.CreateCell(dt.Columns.Count+1).SetCellValue(synj);
                }
                for (int i = 0; i <= dt.Columns.Count+1; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        protected void radio_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void btnnjqlsj_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (txtnjqlsj.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写清零日期！！！');", true);
                ModalPopupExtenderSearch1.Hide();
                return;
            }
            int result=0;
            result = string.Compare(DateTime.Now.ToString("yyyy-MM-dd").Trim(),txtnjqlsj.Text.Trim());
            if (result>0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('清零日期不能小于当前时间！！！');", true);
                ModalPopupExtenderSearch1.Hide();
                return;
            }


            string sqltext = "select * from OM_QINGDATE";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            if (dt.Rows.Count > 0)
            {
                string sqlupdate = "update OM_QINGDATE set nextqingldate='" + txtnjqlsj.Text.Trim() + "'";
                list.Add(sqlupdate);
            }
            else
            {
                string sqlinsert = "insert into OM_QINGDATE(nextqingldate) values('" + txtnjqlsj.Text.Trim() + "')";
                list.Add(sqlinsert);
            }

            DBCallCommon.ExecuteTrans(list);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void btnholdel_OnClick(object sender, EventArgs e)
        {
            string syts = "";
            string strqldate = "";
            string sqltextql = "select * from OM_QINGDATE";
            System.Data.DataTable dtql = DBCallCommon.GetDTUsingSqlText(sqltextql);
            if (dtql.Rows.Count > 0)
            {
                strqldate = dtql.Rows[0]["nextqingldate"].ToString().Trim();
            }
            if (strqldate == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('需要添加清零日期！！！');", true);
                return;
            }


            string sqltext = "select * from OM_NianJiaTJ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            List<string> list_sql = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //计算年假
                    DateTime datemin;
                    DateTime datemax;
                    try
                    {
                        datemin = DateTime.Parse(dt.Rows[i]["NJ_RUZHITIME"].ToString().Trim());
                        
                        if (dt.Rows[i]["NJ_LIZHITIME"].ToString().Trim() != "")
                        {
                            datemax = DateTime.Parse(dt.Rows[i]["NJ_LIZHITIME"].ToString().Trim());
                        }
                        else
                        {
                            datemax = DateTime.Parse(strqldate);
                        }
                    }
                    catch
                    {
                        datemin = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                        datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                    }
                    decimal monthnum = datemax.Month - datemin.Month;
                    decimal yearnum = datemax.Year - datemin.Year;
                    decimal totalmonthnum = yearnum * 12 + monthnum;
                    decimal ysynum = 0;
                    decimal qinglnum = 0;
                    try
                    {
                        ysynum = Convert.ToDecimal(dt.Rows[i]["NJ_YSY"].ToString().Trim());
                        qinglnum = Convert.ToDecimal(dt.Rows[i]["NJ_QINGL"].ToString().Trim());
                    }
                    catch
                    {
                        ysynum = 0;
                        qinglnum = 0;
                    }
                    //大于一年小于十年
                    if (totalmonthnum >= 12 && totalmonthnum < 120)
                    {
                        syts = (Math.Floor((totalmonthnum - 12) * 5 / 12) - qinglnum - ysynum).ToString().Trim();
                    }
                    //大于十年小于二十年
                    else if (totalmonthnum >= 120 && totalmonthnum < 240)
                    {
                        syts = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12) - qinglnum - ysynum).ToString().Trim();
                    }
                    //二十年以上
                    else if (totalmonthnum >= 240)
                    {
                        syts = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12) - qinglnum - ysynum).ToString().Trim();
                    }
                    else
                    {
                        syts = (-(qinglnum + ysynum)).ToString().Trim();
                    }



                    string sqlupdatedel = "update OM_NianJiaTJ set NJ_QINGL=NJ_QINGL+" + CommonFun.ComTryDecimal(syts) + "+" + ysynum + ",NJ_YSY=0,NJ_LASTQL='" + strqldate + "' where NJ_ST_ID='" + dt.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                    string sqlupdate = "update OM_QINGDATE set nextqingldate=''";
                    
                    list_sql.Add(sqlupdatedel);
                    list_sql.Add(sqlupdate);
                }
            }
            DBCallCommon.ExecuteTrans(list_sql);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        //因为达到第五条规定条件而调整年假的调整天数
        protected void btn_kouchunianjia_OnClick(object sender, EventArgs e)
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
                        //2016.12.7修改
                        //if (shijiadays >= 20)
                        //{
                        //    if (yishiyong >= yearnowtzdays)
                        //    {
                        //        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='事假累计20天,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                        //        listsql.Add(sqltexttz);
                        //    }
                        //    else
                        //    {
                        //        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='事假累计20天,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                        //        listsql.Add(sqltexttz);

                        //        listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','事假累计20天,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                        //    }
                        //}
                        //旷工超过3天
                         if (kuanggongdays >= 3)
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
                UCPaging1.CurrentPage = 1;
                bindrpt();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据！！！');", true);
                ModalPopupExtenderSearch3.Hide();
                return;
            }
        }
    }
}
        #endregion