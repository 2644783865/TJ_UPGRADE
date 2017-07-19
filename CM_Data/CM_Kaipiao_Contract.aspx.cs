using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Kaipiao_Contract : System.Web.UI.Page
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
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            string sql = "select sum(kpZongMoney) as KPJE,sum(ZongMoney) as HTJE from (select b.conId,b.TaskId,b.Proj,b.Engname,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE,a.KP_CODE,sum(cast(b.Money as float)) as ZongMoney,sum(cast(b.kpmoney as float)) as kpZongMoney from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,b.TaskId,b.Proj,b.Engname,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE,a.KP_CODE,a.KP_KPNUMBER)c where" + CreateConStr(1);
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
            KPJE.Text = dt1.Rows[0]["KPJE"].ToString();
            HTJE.Text = dt1.Rows[0]["HTJE"].ToString();
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
            pager.TableName = "(select b.conId,b.Proj,b.Map,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE,sum(cast(b.Money as float)) as ZongMoney,sum(cast(b.kpmoney as float)) as kpZongMoney from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,b.Proj,b.Map,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE)c";
            pager.PrimaryKey = "conId";
            pager.ShowFields = "*";
            pager.OrderField = "KP_KPDATE";
            pager.StrWhere = CreateConStr(1);
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 20;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr(int type)
        {
            string sql = " 1=1 ";
            if (txtContract.Text.Trim() != "")
            {
                sql += " and conId like'%" + txtContract.Text.Trim() + "%'";
            }
            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {

            GetBoundData();
        }



        #endregion

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            this.GetBoundData();
        }

    }
}
