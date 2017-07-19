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
    public partial class TM_JICHUANGBASE : BasicPage
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
            pager_org.TableName = "TBTM_MEC";
            pager_org.PrimaryKey = "id";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "jc_bh";
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
            if (iptjcbh.Value.Trim() != "")
            {
                sql += " and jc_bh like '%" + iptjcbh.Value.Trim() + "%'";
            }
            if (iptjctype.Value.Trim() != "")
            {
                sql += " and jc_type like '%" + iptjctype.Value.Trim() + "%'";
            }
            if (iptjcable.Value.Trim() != "")
            {
                sql += " and jc_gxtypeable like '%" + iptjcable.Value.Trim() + "%'";
            }
            if (iptaddman.Value.Trim() != "")
            {
                sql += " and jc_addman like '%" + iptaddman.Value.Trim() + "%'";
            }
            if (radio_zaiyong.Checked == true)
            {
                sql += " and jc_state='0'";
            }
            if (radio_tingyong.Checked == true)
            {
                sql += " and jc_state='1'";
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
            CommonFun.Paging(dt, rptjclook, UCPaging1, palNoData);
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


        protected void rptjclook_itemdatabind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string zdrname = ((System.Web.UI.WebControls.Label)e.Item.FindControl("jc_addman")).Text.Trim();
                string zdrid = ((System.Web.UI.WebControls.Label)e.Item.FindControl("jc_addmanid")).Text.Trim();
                string username = Session["UserName"].ToString().Trim();
                string userid = Session["UserID"].ToString().Trim();
                if (zdrname != username || zdrid != userid)
                {
                    ((System.Web.UI.WebControls.LinkButton)e.Item.FindControl("LinkButtonSC")).Visible = false;
                }
            }
        }
        #endregion

        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            string id = (sender as LinkButton).CommandArgument.ToString().Trim();
            string sqlText = "delete from TBTM_MEC where id='" + id + "'";
            DBCallCommon.ExeSqlText(sqlText);
            this.bindrpt();
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
    }
}
