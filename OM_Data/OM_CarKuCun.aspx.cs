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
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarKuCun : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {

                GetBoundData();
            }
        }
        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "OM_CARKUCUN ";
            pager.PrimaryKey = "KC_ID";
            pager.ShowFields = "*";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 1;
            pager.PageSize = 15;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        private string GetWhere()
        {
            string strWhere = "KC_SL<>'0' ";
            if (txtName.Text.Trim() != "")
            {
                strWhere += " AND KC_MC like '%" + txtName.Text.Trim() + "%'";
            }
            if (txtModel.Text.Trim() != "")
            {
                strWhere += " AND KC_GG like '%" + txtModel.Text.Trim() + "%'";
            }

            return strWhere;
        }
        #endregion

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
        protected void btnTransf_click(object sender, EventArgs e)
        {
           
            List<string> list = new List<string>();
            string sqltxt = "DELETE FROM OM_CAR_ZZB";
            list.Add(sqltxt);
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    string kc_mc = ((Label)Repeater1.Items[i].FindControl("lblbh")).Text;
                    string kc_gg = ((Label)Repeater1.Items[i].FindControl("lblName")).Text;
                    string kc_danwei = ((Label)Repeater1.Items[i].FindControl("lbdanwei")).Text;
                    string kc_danjia = ((Label)Repeater1.Items[i].FindControl("lbdanjia")).Text;
                    sqltxt = "insert into OM_CAR_ZZB (SP_MC,SP_GG,SP_DJ,SP_DANWEI) VALUES ('" + kc_mc + "','" + kc_gg + "','" + kc_danjia + "','" + kc_danwei + "')";
                    list.Add(sqltxt);
                }
            }
            if (list.Count == 1)
            {
                Response.Write("<script>alert('请勾选序号后后再出库！！！')</script>");
            }
            else
            {
                DBCallCommon.ExecuteTrans(list);
                Response.Redirect("OM_CARCK_detail.aspx?FLAG=add&id=1");
            }

        }
    }
}
