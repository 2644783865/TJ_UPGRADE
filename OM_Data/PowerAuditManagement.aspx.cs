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

namespace ZCZJ_DPF.OM_Data
{
    public partial class PowerAuditManagement : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindbmData();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
                ControlVisible();
            }
            //CheckUser(ControlFinder);
        }


        #region 分页
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
            pager_org.TableName = "AuditNew as a left join PowerContent as b on a.auditno=b.contentno left join View_TBDS_STAFFINFO as c on b.stid=c.ST_ID";//不同页面数据源根据需要调整
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "auditno";//按该列排序
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//降序排列，0为升序排列
            pager_org.PageSize = 25;//每页显示条数
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1 and audittype='" + audittitle.Text.Trim() + "'";
            if (txtName.Text != "")
            {
                sql += " and addpername like '%" + txtName.Text + "%'";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sql += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            if (startdate.Value.Trim() != "")
            {
                sql += " and addtime>='" + startdate.Value.Trim() + "'";
            }
            if (enddate.Value.Trim() != "")
            {
                sql += " and addtime<='" + enddate.Value.Trim() + "'";
            }
            if (shstate.SelectedIndex != 0)
            {
                sql += " and totalstate='" + shstate.SelectedValue.Trim() + "'";
            }

            if (task.SelectedValue.Trim() == "0")
            {
                sql += " and ((totalstate='1' and ((auditperid1=" + Session["UserID"].ToString().Trim() + " and auditstate1='0') or (auditperid2=" + Session["UserID"].ToString().Trim() + " and auditstate1='1' and auditstate2='0') or (auditperid3=" + Session["UserID"].ToString().Trim() + " and auditstate1='1' and auditstate2='1' and auditstate3='0'))) or (totalstate='0' and addperid=" + Session["UserID"].ToString().Trim() + ") or (totalstate='3' and addperid=" + Session["UserID"].ToString().Trim() + ") or (totalstate='2' and fankuistate='0' and fankui='是' and addperid=" + Session["UserID"].ToString().Trim() + "))";
            }
            return sql;
        }
        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
            ControlVisible();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptpower, UCPaging1, palNoData);
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

        private void BindbmData()
        {
            //string stId = Session["UserId"].ToString();
            //System.Data.DataTable dt = DBCallCommon.GetPermeision(82, stId);
            string sqltext = "select distinct DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'order by DEP_CODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
            ddl_Depart.Items.Insert(0, new ListItem("全部", "00"));
        }

        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitVar();
            UCPaging1.CurrentPage = 1;
            bindrpt();
            ControlVisible();
        }
        protected void shstate_CheckedChanged(object sender, EventArgs e)
        {
            InitVar();
            UCPaging1.CurrentPage = 1;
            bindrpt();
            ControlVisible();
        }

        private void ControlVisible()
        {
            foreach (RepeaterItem item in rptpower.Items)
            {
                HyperLink hlkedit = item.FindControl("HyperLinkXG") as HyperLink;
                LinkButton hlkdelete = item.FindControl("HyperLinkSC") as LinkButton;
                HyperLink hlkaudit = item.FindControl("HyperLinkSH") as HyperLink;
                Label totalstate = item.FindControl("totalstate") as Label;
                if (task.SelectedValue.Trim() == "0")
                {
                    if (totalstate.Text.Trim() == "审核中")
                    {
                        hlkedit.Visible = false;
                        hlkdelete.Visible = false;
                        hlkaudit.Visible = true;
                    }
                    if (totalstate.Text.Trim() == "初始化" || totalstate.Text.Trim() == "驳回")
                    {
                        hlkedit.Visible = true;
                        hlkdelete.Visible = true;
                        hlkaudit.Visible = false;
                    }
                    if (totalstate.Text.Trim() == "已通过")
                    {
                        hlkedit.Visible = false;
                        hlkdelete.Visible = false;
                        hlkaudit.Visible = false;
                    }
                }
                else
                {
                    hlkedit.Visible = false;
                    hlkdelete.Visible = false;
                    hlkaudit.Visible = false;
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncx_click(object sender, EventArgs e)
        {
            InitVar();
            UCPaging1.CurrentPage = 1;
            bindrpt();
            ControlVisible();
        }
        //删除
        protected void hlDelete_OnClick(object sender, EventArgs e)
        {
            string auditno = (sender as LinkButton).CommandArgument.ToString().Trim();
            string sqltext = "delete from AuditNew where auditno='" + auditno + "'";
            DBCallCommon.ExeSqlText(sqltext);
            sqltext = "delete from PowerContent where contentno='" + auditno + "'";
            DBCallCommon.ExeSqlText(sqltext);
            InitVar();
            UCPaging1.CurrentPage = 1;
            bindrpt();
            ControlVisible();
        }
    }
}
