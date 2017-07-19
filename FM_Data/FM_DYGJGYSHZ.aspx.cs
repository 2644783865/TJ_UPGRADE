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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_DYGJGYSHZ : System.Web.UI.Page
    {
        double jehj = 0;
        double hsjehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }

            if (IsPostBack)
            {
                this.InitVar();
            }

        }
        //初始化页面
        private void InitPage()
        {
            //显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }
            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }//

        #region  分页
        PagerQueryParamGroupBy pager_org = new PagerQueryParamGroupBy();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "View_TBFM_YGJZG";
            pager_org.PrimaryKey = "GI_SUPPLIERNM";
            pager_org.ShowFields = "GI_SUPPLIERNM,sum(GI_INAMTMNY) as GI_INAMTMNY,sum(GI_INCATAMTMNY) as GI_INCATAMTMNY";
            pager_org.OrderField = "GI_SUPPLIERNM";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;
            pager_org.PageSize = 50;
            pager_org.GroupField = "GI_SUPPLIERNM";
        }

        private string strstring()
        {
            string sqlText = "1=1";
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                string yearmonth = dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString();
                sqlText += " and WG_VERIFYDATE like '" + yearmonth.ToString() + "%' and GI_DATE like '" + yearmonth.ToString() + "%' and (WG_CODE like 'G%' or WG_CODE like 'T%') and WG_VERIFYDATE is not null";
            }
            return sqlText;
        }


        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager_org);
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
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }
        #endregion
        /// <summary>
        /// 年、月份改变 查询
        /// </summary>
        protected void OnSelectedIndexChanged_dplYearMoth(object sender, EventArgs e)
        {
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                rptProNumCost.DataSource = null;
                rptProNumCost.DataBind();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择年月!')</script>");
                return;
            }
        }



        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string condition = strstring();
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbgys = (Label)e.Item.FindControl("lbgys");
                Label lbje = (Label)e.Item.FindControl("lbje");
                Label lbhsje = (Label)e.Item.FindControl("lbhsje");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(sum(isnull(GI_INAMTMNY,0)),0) as GI_INAMTMNY,isnull(sum(isnull(GI_INCATAMTMNY,0)),0) as GI_INCATAMTMNY from View_TBFM_YGJZG where " + condition;

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {
                    jehj = Convert.ToDouble(drhj["GI_INAMTMNY"].ToString());
                    hsjehj = Convert.ToDouble(drhj["GI_INCATAMTMNY"].ToString());
                }
                drhj.Close();
                Label lbjehj = (Label)e.Item.FindControl("lbjehj");
                Label lbhsjehj = (Label)e.Item.FindControl("lbhsjehj");

                lbjehj.Text = jehj.ToString();
                lbhsjehj.Text = hsjehj.ToString();
            }
        }
    }
}
