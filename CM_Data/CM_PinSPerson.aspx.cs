using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_PinSPerson : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "评审人员设置";
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                this.bindGrid();
            }
            CheckUser(ControlFinder);
        }

        #region "数据查询，分页"
        private void InitPager()
        {
            pager.TableName = "TBCM_HT_SETTING as a left join TBDS_STAFFINFO as b on a.per_id=b.ST_ID left join TBDS_DEPINFO as c on b.ST_POSITION=c.DEP_CODE left join TBDS_DEPINFO as d on a.dep_id = d.DEP_CODE";
            //"((select *,'计划单评审' as ps_cont from TBCM_RW_SETTING union all select *,'投标评审' as ps_cont from TBCM_TB_SETTING)t left join TBDS_STAFFINFO as a on t.per_id=a.ST_ID left join TBDS_DEPINFO as b on a.ST_POSITION=b.DEP_CODE left join TBDS_DEPINFO as d on t.dep_id = d.DEP_CODE)";
            pager.PrimaryKey = "id";
            pager.ShowFields = "a.*,d.DEP_NAME,b.ST_NAME,c.DEP_NAME as ST_POSITION,(CASE WHEN b.ST_PD='0' THEN '在用' ELSE '停用' END) as State";
            pager.OrderField = "per_type";
            pager.StrWhere = StrWhere(); ////AND B.ST_PD=0 A.PER_ID=B.ST_CODE AND (A.PER_SFJY=0)
            pager.OrderType = 1;//按时间升序序排列
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
        }
        protected string StrWhere()
        {
            //2017.4.17修改,此处只有销售合同可用
            string strWhere = "per_type='0'";
            //string strWhere = "";
            //string strWhere = "";
            if (dplPS.SelectedValue != "a")
            {
                //2017.4.17修改,此处只有销售合同可用
                strWhere = "per_type='0'";
                //strWhere = "per_type='" + dplPS.SelectedValue + "'";
            }
            return strWhere;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            InitPager();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvPS, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
                //CheckUser(ControlFinder);
            }
        }
        #endregion

        protected void Lbtn_Del_Click(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            object[] arg = id.Split(',');
            string per_id = arg[0].ToString();
            string per_type = arg[1].ToString();
            //2017.4.17修改,此处只有销售合同可用
            string sql_del = string.Format("delete from TBCM_HT_SETTING  where id='{0}' and per_type={1}", per_id, per_type);
            //string sql_del = string.Format("update TBCM_HT_SETTING set per_sfjy='1' where id='{0}' and per_type={1}", per_id, per_type);
            DBCallCommon.ExeSqlText(sql_del);
            this.bindGrid();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('删除成功');", true);
        }

        protected void dplPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }
    }
}
