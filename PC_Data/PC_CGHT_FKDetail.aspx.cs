using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_CGHT_FKDetail : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {

                this.GetBoundData();
            }
        }
        #region 分页
        protected void GetBoundData()
        {
            InitPager();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, grvYKJL, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private void InitPager()
        {
            pager.TableName = "TBPC_PURPAYMENTRECORD as a left join PC_CGHT as b on a.BP_HTBH=b.HT_XFHTBH";
            pager.PrimaryKey = "a.ID";
            pager.ShowFields = "BP_ID, BP_HTBH, BP_KXMC, BP_YKRQ, BP_JE, BP_SKFS, BP_NOTE, BP_ZFBL, BP_SCBHTH, BP_SHEBEI,HT_HTZJ,HT_GF";
            pager.OrderField = "BP_HTBH";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 20;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr()
        {
            string sql = " 1=1 ";

            if (ddrKxmc.SelectedIndex != 0)
            {
                sql += " and BP_KXMC='" + ddrKxmc.SelectedValue + "'";
            }

            if (scbConId.Text != "")
            {
                sql += " and BP_SCBHTH like '%" + scbConId.Text + "%'";
            }
            if (txtGF.Text != "")
            {
                sql += " and HT_GF like '%" + txtGF.Text + "%'";
            }
            if (txtConId.Text != "")
            {
                sql += " and BP_HTBH like '%" + txtConId.Text.Trim() + "%'";
            }
            if (txtBeizhu.Text.Trim() != "")
            {
                sql += " and BP_NOTE like '%" + txtBeizhu.Text.Trim() + "%'";
            }
            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {

            GetBoundData();
        }



        #endregion
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            this.GetBoundData();
        }
        //删除要款
        protected void Lbtn_Del_OnClick(object sender, EventArgs e)
        {
            List<string> sqlstr = new List<string>();
            string bp_id = ((LinkButton)sender).CommandArgument.ToString();


            string str_del = "delete from TBPC_PURPAYMENTRECORD WHERE BP_ID='" + bp_id + "'";
            sqlstr.Add(str_del);
            DBCallCommon.ExecuteTrans(sqlstr);
            GetBoundData();

        }
    }
}
