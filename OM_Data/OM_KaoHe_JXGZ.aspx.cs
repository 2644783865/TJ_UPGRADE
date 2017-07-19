using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHe_JXGZ : System.Web.UI.Page
    {
        string year = "";
        string month = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            year = (DateTime.Now.Year).ToString();
            month = (DateTime.Now.Month).ToString();

            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                BindData();
                this.InitPage();
                UCPaging1.CurrentPage = 1;

                InitVar();
                bindGrid();

            }
            InitVar();

        }




        //绑定基本信息
        private void BindData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(2, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
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
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            //dpl_Year.SelectedIndex = 0;
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
            //dpl_Month.SelectedIndex = 0;
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


        #region
        PagerQueryParam pager_org = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager("1=1");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }


        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string where)
        {
            pager_org.TableName = "(select * from View_TBDS_JXGZ as a inner join View_TBDS_KaoheDepartMonth as b on a.DEP_CODE=b.DepartId and a.Year=b.DepMonth_Year and a.Month=b.DepMonth_Month where b.state='2')c";
            pager_org.PrimaryKey = "Id";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "Id,Year,Month";
            pager_org.StrWhere = where;
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 20;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            string sqlwhere = "1=1";


            if (dplYear.SelectedIndex != 0)
            {
                sqlwhere += " and Year='" + dplYear.SelectedValue + "' ";
            }
            if (dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and Month='" + dplMoth.SelectedValue + "' ";
            }

            if (txtname.Text != "")
            {
                sqlwhere += " and (ST_NAME like '%" + txtname.Text.ToString().Trim() + "%' or ST_WORKNO like '%" + txtname.Text.ToString().Trim() + "%') ";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sqlwhere += " and DEP_CODE='" + ddl_Depart.SelectedValue + "'";
            }


            InitPager(sqlwhere);

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

            string sqlText = "select sum(cast(ST_GANGWEIXISHU as float)),sum(cast(Money as float)) from (select * from View_TBDS_JXGZ as a inner join View_TBDS_KaoheDepartMonth as b on a.DEP_CODE=b.DepartId and a.Year=b.DepMonth_Year and a.Month=b.DepMonth_Month where b.state='2')c where  " + sqlwhere;
            dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                lblXiShuTotal.Text = dt.Rows[0][0].ToString();
                lblGZTotal.Text = dt.Rows[0][1].ToString();
            }
        }
        #endregion

        protected void dplMoth_SelectedIndexChanged(object sender, EventArgs e)
        {
            CaculateBzAverage();
            bindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            CaculateBzAverage();
            bindGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();



            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {

                string Id = ((System.Web.UI.WebControls.Label)rptProNumCost.Items[i].FindControl("lblId")).Text.Trim();
                string scoreHp = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("txtScoreHP")).Text.Trim();
                string txtlScoreLD = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("txtlScoreLD")).Text.Trim();
                string lblScoreZong = ((HtmlInputHidden)rptProNumCost.Items[i].FindControl("hidScoreZong")).Value.Trim();
                string txtNote = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("txtNote")).Text.Trim();
                //Id, Kh_Year, Kh_Month, Kh_ScoreHP, Kh_ScoreLD, Kh_ScoreTotal, Kh_BL, Kh_Id, Kh_BeiZhu, ST_NAME, ST_SEQUEN, ST_GENDER, DEP_NAME, POSITION, ST_WORKNO
                string sqlText = "update TBDS_KaoHeTotal set Kh_ScoreHP='" + scoreHp + "',Kh_ScoreLD='" + txtlScoreLD + "',Kh_ScoreTotal='" + lblScoreZong + "',Kh_BeiZhu='" + txtNote + "' where Id='" + Id + "'";
                sql_list.Add(sqlText);
            }



            //更新
            DBCallCommon.ExecuteTrans(sql_list);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已保存！！！');", true);
            sql_list.Clear();

            bindGrid();
        }  //删除功能建议在使用时隐藏
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();

            string sqltext = "";
            foreach (RepeaterItem LabelID in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)LabelID.FindControl("CKBOX_SELECT");
                if (chk.Checked)
                {
                    string Id = ((System.Web.UI.WebControls.Label)LabelID.FindControl("lblId")).Text;


                    sqltext = "delete FROM TBDS_KaoHe_JXGZ WHERE Id='" + Id + "'";
                    sql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(sql);
            CalculateSalary();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！！！');", true);
            bindGrid();
        }


        protected void btnCreat_Click(object sender, EventArgs e)
        {
            if (ddl_Depart.SelectedValue == "00")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择部门！！！');", true);
                return;
            }
            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            string year = dplYear.SelectedValue;
            string month = dplMoth.SelectedValue;

            string sql = "select * from View_TBDS_KaoheDepartMonth where DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and State='2'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未找到当月部门绩效考核数据！！！');", true);
                return;
            }

            CaculateBzAverage();

            List<string> list = new List<string>();

            sql = "delete a from TBDS_KaoHe_JXGZ as a left join TBDS_STAFFINFO as b on a.StId=b.ST_ID where b.ST_DEPID='" + ddl_Depart.SelectedValue + "' and  Year='" + year + "' and Month='" + month + "'";
            list.Add(sql);
            sql = "insert into TBDS_KaoHe_JXGZ select '0','',DepMonth_Year,DepMonth_Month,ST_ID from dbo.TBDS_KaoHeTotal as a left join TBDS_STAFFINFO as b on a.Kh_ID=b.ST_ID inner join  View_TBDS_KaoheDepartMonth as c on b.ST_DEPID=c.DepartId and a.Kh_Year=c.DepMonth_Year and a.Kh_Month=c.DepMonth_Month  where b.ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            list.Add(sql);
            DBCallCommon.ExecuteTrans(list);
            CalculateSalary();
            bindGrid();
        }

        private void CaculateBzAverage()
        {
            string year = dplYear.SelectedValue;
            string month = dplMoth.SelectedValue;

            string js = "0";
            string sql = "select * from TBDS_BZAVERAGE where Year='" + year + "' and Month='" + month + "' and State='2'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                string money = dt.Rows[0]["MONEY"].ToString();
                js = (300 * CommonFun.ComTryDouble(money) / 2500).ToString("0.00");
                lblJS.Text = "300*" + money + "/2500=" + js;
                hidJS.Value = js;
            }
        }

        //计算工资
        private void CalculateSalary()
        {
            string year = dplYear.SelectedValue;
            string month = dplMoth.SelectedValue;


            CaculateBzAverage();

            string js = hidJS.Value;
            string sql = "select DepartId,Score from View_TBDS_KaoheDepartMonth where DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and State='2' and DepartId='" + ddl_Depart.SelectedValue + "'";
            DataTable dtPart = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtPart.Rows.Count > 0)
            {
                for (int i = 0; i < dtPart.Rows.Count; i++)
                {
                    string depId = dtPart.Rows[i]["DepartId"].ToString();
                    string score = dtPart.Rows[i]["Score"].ToString();
                    sql = "select sum(case when ST_GANGWEIXISHU='' or ST_GANGWEIXISHU is null then 1.00 else ST_GANGWEIXISHU end),sum(case when  Kh_ScoreTotal is null then 0 else Kh_ScoreTotal end) from View_TBDS_JXGZ where Year='" + year + "' and Month='" + month + "' and DEP_CODE='" + depId + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        double zongXishu = CommonFun.ComTryDouble(dt.Rows[0][0].ToString());
                        double zongDeiFen = CommonFun.ComTryDouble(dt.Rows[0][1].ToString());
                        if (zongDeiFen == 0)
                        {
                            zongDeiFen = 1.0;
                        }
                        sql = "update a set  Money=case when POSITION_NAME like '%部长%' or POSITION_NAME like '%主管%' then " + CommonFun.ComTryDouble(js) * CommonFun.ComTryDouble(score) / 100.0 + "*ST_GANGWEIXISHU  else " + CommonFun.ComTryDouble(js) * zongXishu * CommonFun.ComTryDouble(score) / zongDeiFen / 100.0 + "*cast(b.Kh_ScoreTotal as float)  end  from TBDS_KaoHe_JXGZ as a left join View_TBDS_JXGZ as b on a.Id=b.Id inner join View_TBDS_KaoheDepartMonth as c on b.DEP_CODE=c.DepartId and b.Year=c.DepMonth_Year and b.Month=c.DepMonth_Month where c.state='2' and b.DEP_CODE='" + depId + "' ";
                        DBCallCommon.ExeSqlText(sql);
                    }

                }
            }

        }

        //提交部门审批

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (ddl_Depart.SelectedValue == "00")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择部门！！！');", true);
                return;
            }
            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            string year = dplYear.SelectedValue;
            string month = dplMoth.SelectedValue;
            string depId = ddl_Depart.SelectedValue;
            string sql = "select * from TBDS_KaoHe_JXList where  Year='" + year + "' and Month='" + month + "' and DepId='" + depId + "' and State<>'3'";
            string context = DateTime.Now.ToString("yyyyMMddhhmmss");
            List<string> list = new List<string>();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该记录已提交过审批，无法再次提交！！！');", true);
                return;
            }
            else
            {
                string stId = "";
                string stName = "";
                sql = "select ST_ID,ST_NAME from  TBDS_STAFFINFO where ST_DEPID='" + depId + "' and ST_POSITION like '%01' and ST_PD='0'";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count == 0 && depId == "12")
                {
                    stId = "69";
                    stName = "陈永秀";
                }
                else if (dt.Rows.Count == 0 && depId == "03")
                {
                    stId = "67";
                    stName = "李小婷";
                }
                if (dt.Rows.Count > 0)
                {
                    stId = dt.Rows[0][0].ToString();
                    stName = dt.Rows[0][1].ToString();
                }
                sql = "select sum(money) from  (select * from View_TBDS_JXGZ as a inner join View_TBDS_KaoheDepartMonth as b on a.DEP_CODE=b.DepartId and a.Year=b.DepMonth_Year and a.Month=b.DepMonth_Month where b.state='2' and DEP_CODE='" + depId + "' and POSITION_NAME not like '%部长%')c";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    string ZongMoney = dt.Rows[0][0].ToString();
                    //Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM, RESULTA, ZDTIME, TIMEA, OPTIONA, ConText, DepId, DepNM
                    sql = "insert into TBDS_KaoHe_JXList(Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM,ZDTIME,ConText,DepId,Zonghe) values('" + year + "','" + month + "','1','" + stId + "','" + stName + "','" + Session["UserId"].ToString() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + context + "','" + depId + "','" + ZongMoney + "')";
                    list.Add(sql);
                    sql = "insert into TBDS_KaoHe_JXDetail select '" + context + "','',Kh_ScoreTotal,Score,ST_GANGWEIXISHU,Money,POSITION_NAME,StId,ST_NAME,DEP_CODE,DEP_NAME from (select * from View_TBDS_JXGZ as a inner join View_TBDS_KaoheDepartMonth as b on a.DEP_CODE=b.DepartId and a.Year=b.DepMonth_Year and a.Month=b.DepMonth_Month where b.state='2' and DEP_CODE='" + depId + "')c";
                    list.Add(sql);
                    DBCallCommon.ExecuteTrans(list);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！！！');", true);
                }

            }
        }
    }
}
