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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class TM_GsBaseManagement : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            CheckUser(ControlFinder);
        }


        #region 分页
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;
        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "TBTM_GONGSBASE";
            pager_org.PrimaryKey = "context";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "zongmap,bjmap";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;
            pager_org.PageSize = 50;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (iptcpname.Value.Trim() != "")
            {
                sql += " and cpname like '%" + iptcpname.Value.Trim() + "%'";
            }
            if (iptcpguige.Value.Trim() != "")
            {
                sql += " and cpguige like '%" + iptcpguige.Value.Trim() + "%'";
            }
            if (iptzongmap.Value.Trim() != "")
            {
                sql += " and zongmap like '%" + iptzongmap.Value.Trim() + "%'";
            }
            if (iptbjname.Value.Trim() != "")
            {
                sql += " and bujianname like '%" + iptbjname.Value.Trim() + "%'";
            }
            if (iptbjmap.Value.Trim() != "")
            {
                sql += " and bujiantuhao like '%" + iptbjmap.Value.Trim() + "'";
            }
            if (radio_zaiyong.Checked == true)
            {
                sql += " and state='0'";
            }
            if (radio_tingyong.Checked == true)
            {
                sql += " and state='1'";
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
            CommonFun.Paging(dt, rptgsbasemanagement, UCPaging1, palNoData);
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

        protected void rptgsbasemanagement_itemdatabind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string zdrname = ((System.Web.UI.WebControls.Label)e.Item.FindControl("zdrname")).Text.Trim();
                string zdrid = ((System.Web.UI.WebControls.Label)e.Item.FindControl("zdrid")).Text.Trim();
                string username = Session["UserName"].ToString().Trim();
                string userid = Session["UserID"].ToString().Trim();
                if (zdrname != username || zdrid != userid)
                {
                    ((System.Web.UI.WebControls.HyperLink)e.Item.FindControl("HyperLinkXG")).Visible = false;
                    ((System.Web.UI.WebControls.LinkButton)e.Item.FindControl("LinkButtonSC")).Visible = false;
                }
            }
        }

        protected void btnsearch_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindrpt();
        }

        protected void radiostate_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindrpt();
        }

        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            string context = (sender as LinkButton).CommandArgument.ToString().Trim();
            string sqlText1 = "delete from TBTM_GONGSBASE where context='" + context + "'";
            string sqlText2 = "delete from TBTM_GONGSBASEDETAIL where detailcontext='" + context + "'";
            DBCallCommon.ExeSqlText(sqlText1);
            DBCallCommon.ExeSqlText(sqlText2);
            this.bindrpt();
        }
    }
}
