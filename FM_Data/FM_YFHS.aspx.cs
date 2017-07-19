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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_YFHS : BasicPage
    {
        double jehj = 0;
        double hsjehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
                string sql = "select * from TBFM_YFHS where YFHS_YEAR='" + dplYear.SelectedValue.ToString() + "' and YFHS_MONTH='" + dplMoth.SelectedValue.ToString() + "' and YFHS_STATE='1'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    btnHS.Visible = false;
                    btnFHS.Visible = true;
                }
                for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
                {
                    if (i == rptProNumCost.Items.Count - 2)
                    {
                        for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                        {
                            Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                            Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                            if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                            {
                                lbjsd1.Visible = false;
                            }
                        }
                    }
                    Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                    Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                    if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                    {
                        lbjsdh2.Visible = false;
                    }
                }
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
            pager_org.TableName = "View_yfInv";
            pager_org.PrimaryKey = "YFGI_CODE";
            pager_org.ShowFields = "YFGI_JSDID,YFGJ_FPID,YFGI_GYSNAME,YFGJ_CPBH,YFGJ_CPMC,YFGJ_WGHT,YFGJ_NUM,YFGJ_JE,YFGJ_HSJE,YFGJ_GJDATE,YFGJ_GJRNAME";
            pager_org.OrderField = "YFGI_CODE";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
        }

        private string strstring()
        {
            string sqlText = "";
            if (dplYear.SelectedIndex != 0 || dplMoth.SelectedIndex != 0)
            {
                sqlText += "YFGJ_YEAR='" + dplYear.SelectedValue.Trim().ToString() + "' and YFGJ_MONTH='" + dplMoth.SelectedValue.Trim().ToString() + "'";
            }
            return sqlText;
        }


        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                }
            }
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, NoDataPanel);
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
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                rptProNumCost.DataSource = null;
                rptProNumCost.DataBind();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
                for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
                {
                    if (i == rptProNumCost.Items.Count - 2)
                    {
                        for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                        {
                            Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                            Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                            if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                            {
                                lbjsd1.Visible = false;
                            }
                        }
                    }
                    Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                    Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                    if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                    {
                        lbjsdh2.Visible = false;
                    }
                }
                string sql = "select * from TBFM_YFHS where YFHS_YEAR='" + dplYear.SelectedValue.ToString() + "' and YFHS_MONTH='" + dplMoth.SelectedValue.ToString() + "' and YFHS_STATE='1'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    btnHS.Visible = false;
                    btnFHS.Visible = true;
                }
                else
                {
                    btnHS.Visible = true;
                    btnFHS.Visible = false;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择年月!')</script>");
                return;
            }
        }


        #region 核算
        protected void btnHS_Click(object sender, EventArgs e)
        {
            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择年月!')</script>");
                return;
            }


            List<string> sql = new List<string>();
            string sqlfpifgj = "select * from TBFM_YFFPTOTAL where YFGI_GJFLAG='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlfpifgj);
            if (dt.Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('还有未勾稽的发票，请勾稽完再核算!')</script>");
                return;
            }//判断是否当月有未勾稽的发票
            string sqlhsstate = "update TBFM_YFFPDETAIL set YFGJ_HSSTATE='1' where YFGJ_YEAR='" + dplYear.SelectedValue.ToString() + "' and YFGJ_MONTH='" + dplMoth.SelectedValue.ToString() + "'";//更改勾稽关系表的核算状态
            DBCallCommon.ExeSqlText(sqlhsstate);
            hstable();//更新核算表信息
            getjsdinfo();//向汇总表中插入当月结算单数据
            //获取当月已勾稽发票的信息，计划跟踪号作为辨识
            string YEARMONTH = dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString();

            //遍历所有月份的结算单的计划跟踪号，与当月已勾稽发票的计划跟踪号相同且年月份相同的，用发票中的数据更新汇总表中的数据，若结算单年月更早，则不改变该计划跟踪号信息，只算出差额，以金额而非含税金额为准，并插入差额表
            string sqltotal = "select * from ((select * from TBFM_YFHZ)a left join (select * from TBFM_YFFPDETAIL where Substring(YFGJ_GJDATE,1,7)='" + YEARMONTH + "')b on a.YFHZ_PTC=b.YFGJ_JHGZH) where YFGJ_FPID is not null";
            DataTable dttotal = DBCallCommon.GetDTUsingSqlText(sqltotal);
            if (dttotal.Rows.Count > 0)
            {
                for (int i = 0; i < dttotal.Rows.Count; i++)
                {
                    double hzmoney = Convert.ToDouble(dttotal.Rows[i]["YFHZ_MONEY"].ToString());
                    double fpmoney = Convert.ToDouble(dttotal.Rows[i]["YFGJ_JE"].ToString());
                    string hzptc = dttotal.Rows[i]["YFHZ_PTC"].ToString();
                    string fpptc = dttotal.Rows[i]["YFGJ_JHGZH"].ToString();
                    if (hzmoney != fpmoney && hzptc == fpptc)
                    {
                        string hzdate = Convert.ToString(dttotal.Rows[i]["YFHZ_YEARMONTH"]).ToString().Substring(0, 7);
                        string fpdate = dttotal.Rows[i]["YFGJ_GJDATE"].ToString().Substring(0, 7);
                        int result;
                        result = string.Compare(fpdate, hzdate);
                        if (hzdate == fpdate)//更新汇总表数据
                        {
                            string sqlhsje = "update TBFM_YFHZ set YFHZ_HSMONEY=(select YFGJ_HSJE from TBFM_YFFPDETAIL where YFGJ_JHGZH='" + fpptc + "') where YFHZ_PTC='" + fpptc + "'";
                            DBCallCommon.ExeSqlText(sqlhsje);
                            string sqlje = "update TBFM_YFHZ set YFHZ_MONEY=(select YFGJ_JE from TBFM_YFFPDETAIL where YFGJ_JHGZH='" + fpptc + "') where YFHZ_PTC='" + fpptc + "'";
                            DBCallCommon.ExeSqlText(sqlje);
                        }
                        else if (result > 0)//向差额表中插入数据
                        {
                            string difrwh = dttotal.Rows[i]["YFHZ_TSAID"].ToString();
                            string difyear = dttotal.Rows[i]["YFGJ_YEAR"].ToString();
                            string difmonth = dttotal.Rows[i]["YFGJ_MONTH"].ToString();
                            string difhzdate = Convert.ToString(dttotal.Rows[i]["YFHZ_YEARMONTH"]).ToString();
                            string difjsdh = dttotal.Rows[i]["YFHZ_JSDH"].ToString();
                            string difjhgzh = dttotal.Rows[i]["YFHZ_PTC"].ToString();
                            string difcpname = dttotal.Rows[i]["YFGJ_CPMC"].ToString();//外协名称
                            string difcpid = dttotal.Rows[i]["YFGJ_CPBH"].ToString();//外协件编号
                            double difhzje = Convert.ToDouble(dttotal.Rows[i]["YFHZ_MONEY"]);//汇总表金额
                            double diffpje = Convert.ToDouble(dttotal.Rows[i]["YFGJ_JE"]);//发票金额
                            double difje = diffpje - difhzje;//差额（发票金额-汇总金额）
                            string sqldif = "insert into TBFM_YFDIF(DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH,DIFYF_JSDDATE,DIFYF_JSDH,DIFYF_JHGZH,DIFYF_BJNAME,DIFYF_BJID,DIFYF_JSDYJE,DIFYF_FPJE,DIFYF_DIFMONEY) Values('" + difrwh + "','" + difyear + "','" + difmonth + "','" + difhzdate + "','" + difjsdh + "','" + difjhgzh + "','" + difcpname + "','" + difcpid + "','" + difhzje + "','" + diffpje + "','" + difje + "')";
                            DBCallCommon.ExeSqlText(sqldif);
                        }
                    }
                }
            }
            btnHS.Visible = false;
            btnFHS.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('本月核算完毕!');", true);
        }

        private void hstable()
        {
            string sql = "insert into TBFM_YFHS(YFHS_YEAR,YFHS_MONTH,YFHS_TIME,YFHS_STATE) values ('" + dplYear.SelectedValue.ToString() + "','" + dplMoth.SelectedValue.ToString() + "','" + DateTime.Now.ToString() + "','1')";
            DBCallCommon.ExeSqlText(sql);
        }

        private void getjsdinfo()
        {
            List<string> sql = new List<string>();
            string date = dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString();
            string sqltext = "select * from PM_CPFYJSD where left(CONVERT(CHAR(10), JS_RQ, 23),7)='" + date.ToString() + "'";
            DataTable dtjsd = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dtjsd.Rows.Count > 0)
            {
                string sqladd = "insert into TBFM_YFHZ(YFHZ_JSDH,YFHZ_PTC,YFHZ_YEARMONTH,YFHZ_NUM,YFHZ_WIGHT,YFHZ_TSAID,YFHZ_MONEY,YFHZ_HSMONEY,YFHZ_BJBH,YFHZ_BJNAME,YFHZ_GJSTATE)" + "select JS_BH,JS_JHGZH,left(CONVERT(CHAR(10), JS_RQ, 23),7),JS_JSSL,JS_ZONGZ,JS_RWH,cast((isnull(cast(JS_HSJE as float),0))/(1+(isnull(JS_SHUIL,0))/100) as decimal(12,2)),JS_HSJE,JS_TUHAO,JS_SBMC,JS_GJSTATE from PM_CPFYJSD where left(CONVERT(CHAR(10), JS_RQ, 23),7)='" + date + "'";
                sql.Add(sqladd);
                DBCallCommon.ExecuteTrans(sql);
            }
        }
        #endregion








        protected void btnFHS_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();
            string date = dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString();
            string sqlhsstate = "update TBFM_YFFPDETAIL set YFGJ_HSSTATE='0' where YFGJ_YEAR='" + dplYear.SelectedValue.ToString() + "' and YFGJ_MONTH='" + dplMoth.SelectedValue.ToString() + "'";//更改勾稽关系表的核算状态
            sql.Add(sqlhsstate);
            string sqldeljsdinfo = "delete from TBFM_YFHZ where YFHZ_YEARMONTH='" + date + "'";
            sql.Add(sqldeljsdinfo);
            string sqldeldif = "delete from TBFM_YFDIF where DIFYF_YEAR='" + dplYear.SelectedValue.ToString() + "' and DIFYF_MONTH='" + dplMoth.SelectedValue.ToString() + "'";
            sql.Add(sqldeldif);
            string sqlhstable = "delete from TBFM_YFHS where YFHS_YEAR='" + dplYear.SelectedValue.ToString() + "' and YFHS_MONTH='" + dplMoth.SelectedValue.ToString() + "'";
            sql.Add(sqlhstable);
            DBCallCommon.ExecuteTrans(sql);
            btnHS.Visible = true;
            btnFHS.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已完成反核算，可以重新核算!');", true);
        }
        #endregion


        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbjsdh = (Label)e.Item.FindControl("lbjsdh");
                Label lbfpbh = (Label)e.Item.FindControl("lbfpbh");
                Label lbgys = (Label)e.Item.FindControl("lbgys");
                Label lbtuhao = (Label)e.Item.FindControl("lbtuhao");
                Label lbsbmc = (Label)e.Item.FindControl("lbsbmc");
                Label lbzl = (Label)e.Item.FindControl("lbzl");
                Label lbsl = (Label)e.Item.FindControl("lbsl");
                Label lbje = (Label)e.Item.FindControl("lbje");
                Label lbhsje = (Label)e.Item.FindControl("lbhsje");
                Label lbgjdate = (Label)e.Item.FindControl("lbgjdate");
                Label lbgjr = (Label)e.Item.FindControl("lbgjr");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(CAST(sum(YFGJ_JE) AS FLOAT),0) as JEHJ,isnull(CAST(sum(YFGJ_HSJE) AS FLOAT),0) as HSJEHJ from View_yfInv where YFGJ_YEAR='" + dplYear.SelectedValue.Trim().ToString() + "' and YFGJ_MONTH='" + dplMoth.SelectedValue.Trim().ToString() + "'";

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {
                    jehj = Convert.ToDouble(drhj["JEHJ"]);
                    hsjehj = Convert.ToDouble(drhj["HSJEHJ"]);
                }
                drhj.Close();
                Label lbjehj = (Label)e.Item.FindControl("lbjehj");
                Label lbhsjehj = (Label)e.Item.FindControl("lbhsjehj");

                lbjehj.Text = jehj.ToString();
                lbhsjehj.Text = hsjehj.ToString();
            }
        }
    }
}
