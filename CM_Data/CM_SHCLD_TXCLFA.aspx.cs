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
using System.Drawing;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_SHCLD_TXCLFA : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        string depid = string.Empty;
        string position = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            username = Session["UserName"].ToString();
            depid = Session["UserDeptID"].ToString();
            position = Session["POSITION"].ToString();
            if (!IsPostBack)
            {
                bindrpt();
            }
        }
        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "CM_SHCLD ";
            pager_org.PrimaryKey = "CLD_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "CLD_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptSHFWCLD, UCPaging1, palNoData);
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

        private string StrWhere()
        {
            string sql = "CLD_ID is not null ";
            if (rblRW.SelectedValue == "2")
            {
                sql += string.Format("and (CLD_CLFA_TXR='{0}' and (CLD_CLFA is null or CLD_CLFA='')) and CLD_SPZT ='1y'", username);
            }
            return sql;
        }
        #endregion
        protected void rptSHFWCLD_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                HyperLink hplTXFA = (HyperLink)e.Item.FindControl("hplTXFA");
                Label CLD_SPZT1 = (Label)e.Item.FindControl("CLD_SPZT1");
                Label CLD_CLZT = (Label)e.Item.FindControl("CLD_CLZT");
                hplTXFA.Visible = false;
                if (dr["CLD_SPZT"].ToString() == "0")
                {
                    CLD_SPZT1.Text = "未审批";
                    CLD_CLZT.Text = "未处理";
                }
                else if (dr["CLD_SPZT"].ToString() == "1y")
                {
                    if (username == dr["CLD_CLFA_TXR"].ToString())
                    {
                        hplTXFA.Visible = true;
                    }
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "2y")
                {
                    if (username == dr["CLD_CLFA_TXR"].ToString())
                    {
                        hplTXFA.Visible = true;
                    }
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "4y")
                {
                    if (username == dr["CLD_CLFA_TXR"].ToString())
                    {
                        hplTXFA.Visible = true;
                    }
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "y")
                {
                    if (username == dr["CLD_CLFA_TXR"].ToString())
                    {
                        hplTXFA.Visible = true;
                    }
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "1n" || dr["CLD_SPZT"].ToString() == "2n" || dr["CLD_SPZT"].ToString() == "4n" || dr["CLD_SPZT"].ToString() == "5n")
                {
                    CLD_SPZT1.Text = "未通过";
                    CLD_SPZT1.BackColor = Color.Red;
                }
                else if (dr["CLD_SPZT"].ToString() == "cljg_y")
                {
                    if (username == dr["CLD_CLFA_TXR"].ToString())
                    {
                        hplTXFA.Visible = true;
                    }
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "fytj_y")
                {
                    if (username == dr["CLD_CLFA_TXR"].ToString())
                    {
                        hplTXFA.Visible = true;
                    }
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "over")
                {
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "已处理";
                    CLD_CLZT.BackColor = Color.LightGreen;
                }
            }

        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }
    }
}
