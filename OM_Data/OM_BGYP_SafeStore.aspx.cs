using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BGYP_SafeStore : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                UCPaging1.CurrentPage = 1;

                InitVar();
                bindGrid();
            }
            InitVar();
            CheckUser(ControlFinder);
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
            pager_org.TableName = "(select a.*,b.num,b.unPrice from dbo.TBMA_OFFICETH as a left join TBOM_BGYP_STORE as b on a.Id=b.mId where a.kc is not null and a.kc<>'' and a.kc<>'0' and IsDel ='0' )c";
            pager_org.PrimaryKey = "Id";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "Id";
            pager_org.StrWhere = where;
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 20;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            string sqlwhere = "1=1";
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

        }
        #endregion




        protected void btnAddSafe_Click(object sender, EventArgs e)
        {
            string str = "(";
            int num = 0;
            foreach (RepeaterItem Item in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)Item.FindControl("CKBOX_SELECT");
                if (chk.Checked)
                {
                    string maId = ((Label)Item.FindControl("lblMaId")).Text;

                    str += "'" + maId + "',";
                    num++;
                }
            }
            str = str.Trim(',') + ")";
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项！！！');", true);
            }
            else
            {
                Response.Redirect("~/OM_Data/OM_BgypPcApply.aspx?action=safePC&maId=" + str);
            }

        }


        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string kcNum = ((Label)e.Item.FindControl("lblNum")).Text;
                string safeNum = ((Label)e.Item.FindControl("lblKC")).Text;
                if (CommonFun.ComTryDouble(kcNum) < CommonFun.ComTryDouble(safeNum))
                {
                    ((HtmlTableCell)e.Item.FindControl("td1")).BgColor = "Red";
                }

            }
        }

    }
}
