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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_GSFPMANAGEMENT : BasicPage
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
            pager_org.TableName = "TBTM_GSMANAGEMENT";
            pager_org.PrimaryKey = "gs_bh";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "gs_jcbh,gs_yearmonth";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;
            pager_org.PageSize = 30;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (yearmonth.Value.Trim() != "")
            {
                sql += " and gs_yearmonth like '%" + yearmonth.Value.Trim() + "%'";
            }
            if (iptjcbh.Value.Trim() != "")
            {
                sql += " and gs_jcbh like '%" + iptjcbh.Value.Trim() + "%'";
            }
            if (iptjctype.Value.Trim() != "")
            {
                sql += " and gs_jctype like '%" + iptjctype.Value.Trim() + "%'";
            }
            if (iptcpname.Value.Trim() != "")
            {
                sql += " and gs_cpname like '%" + iptcpname.Value.Trim() + "%'";
            }
            if (iptcpguige.Value.Trim() != "")
            {
                sql += " and gs_cpguige like '%" + iptcpguige.Value.Trim() + "'";
            }



            if (ipttolmap.Value.Trim() != "")
            {
                sql += " and gs_zongmap like '%" + ipttolmap.Value.Trim() + "%'";
            }
            if (iptbjtuhao.Value.Trim() != "")
            {
                sql += " and gs_bjth like '%" + iptbjtuhao.Value.Trim() + "'";
            }
            if (iptbjname.Value.Trim() != "")
            {
                sql += " and gs_bjname like '%" + iptbjname.Value.Trim() + "'";
            }
            if (radio_yijiesuan.Checked == true)
            {
                sql += " and gs_state='0'";
            }
            if (radio_weijiesuan.Checked == true)
            {
                sql += " and gs_state='1'";
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
            CommonFun.Paging(dt, rptgsmanagement, UCPaging1, palNoData);
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


        protected void rptgsmanagement_itemdatabind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string zdrname = ((System.Web.UI.WebControls.Label)e.Item.FindControl("gs_zdrname")).Text.Trim();
                string zdrid = ((System.Web.UI.WebControls.Label)e.Item.FindControl("gs_zdrid")).Text.Trim();
                string username = Session["UserName"].ToString().Trim();
                string userid = Session["UserID"].ToString().Trim();
                if (zdrname != username || zdrid != userid)
                {
                    ((System.Web.UI.WebControls.HyperLink)e.Item.FindControl("HyperLinkXG")).Visible = false;
                    ((System.Web.UI.WebControls.LinkButton)e.Item.FindControl("LinkButtonSC")).Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqltext = "select sum(gs_bjnum) as gs_bjnumhj,sum(gs_bjtotalgs) as gs_bjtotalgshj,sum(gs_realbjnum) as gs_realbjnumhj,sum(gs_realbjtotalgs) as gs_realbjtotalgshj from TBTM_GSMANAGEMENT where " + StrWhere();
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    Label lbdenumhj = (Label)e.Item.FindControl("lbdenumhj");
                    Label lbdetolgshj = (Label)e.Item.FindControl("lbdetolgshj");
                    Label lbjsnumhj = (Label)e.Item.FindControl("lbjsnumhj");
                    Label lbjstolgshj = (Label)e.Item.FindControl("lbjstolgshj");
                    lbdenumhj.Text = dt.Rows[0]["gs_bjnumhj"].ToString().Trim();
                    lbdetolgshj.Text = dt.Rows[0]["gs_bjtotalgshj"].ToString().Trim();
                    lbjsnumhj.Text = dt.Rows[0]["gs_realbjnumhj"].ToString().Trim();
                    lbjstolgshj.Text = dt.Rows[0]["gs_realbjtotalgshj"].ToString().Trim();
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
            string gs_bh = (sender as LinkButton).CommandArgument.ToString().Trim();
            string sqlText1 = "delete from TBTM_GSMANAGEMENT where gs_bh='" + gs_bh + "'";
            string sqlText2 = "delete from TBTM_GSDETAIL where gs_detailbh='" + gs_bh + "'";
            DBCallCommon.ExeSqlText(sqlText1);
            DBCallCommon.ExeSqlText(sqlText2);
            this.bindrpt();
        }
    }
}
