using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_CGHT_FPDetail : System.Web.UI.Page
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
            pager.TableName = "TBPC_PURBILLRECORD as a left join PC_CGHT as b on a.BR_HTBH=b.HT_XFHTBH";
            pager.PrimaryKey = "a.ID";
            pager.ShowFields = "BR_ID, BR_HTBH, BR_ENGNAME, BR_KPJE, BR_SL, BR_FPDH, BR_KPRQ, BR_LPRQ, BR_JBR, BR_BZ,HT_GF,HT_GFHTBH";
            pager.OrderField = "BR_HTBH";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 20;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr()
        {
            string sql = " 1=1 ";


            if (txtDFConId.Text != "")
            {
                sql += " and HT_GFHTBH like '%" + txtDFConId.Text + "%'";
            }
            if (txtGF.Text != "")
            {
                sql += " and HT_GF like '%" + txtGF.Text + "%'";
            }
            if (txtConId.Text != "")
            {
                sql += " and BR_HTBH like '%" + txtConId.Text.Trim() + "%'";
            }
            if (txtEngName.Text != "")
            {
                sql += " and BR_ENGNAME like '%" + txtEngName.Text.Trim() + "%'";
            }
            if (txtBeizhu.Text.Trim() != "")
            {
                sql += " and BR_BZ like '%" + txtBeizhu.Text.Trim() + "%'";
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
            string br_id = ((LinkButton)sender).CommandArgument.ToString();


            string str_del = "DELETE FROM TBPC_PURBILLRECORD WHERE BR_ID='" + br_id + "'";
            sqlstr.Add(str_del);
            DBCallCommon.ExecuteTrans(sqlstr);
            GetBoundData();

        }
    }
}
