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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_JJZBTJ : BasicPage
    {
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
            //CheckUser(ControlFinder);
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
        }


        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "FM_JJZBTJ";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "*,(YEAR+'-'+MONTH) as RQBH";
            pager_org.OrderField = "YEAR,MONTH";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 30;
        }

        private string strstring()
        {
            string sqlText = "1=1";
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                sqlText += " and YEAR='" + dplYear.SelectedValue.ToString().Trim() + "' and MONTH='" + dplMoth.SelectedValue.ToString().Trim() + "'";
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

        //删除
        protected void btnSC_Click(object sender, EventArgs e)
        {
            CommonFun.delMult("FM_JJZBTJ", "ID", rptProNumCost);
            bindGrid();
            Response.Redirect(Request.Url.ToString());
        }

        //修改
        protected string editDq(string Id)
        {
            return "javascript:window.showModalDialog('FM_JJZBTJ_Detail.aspx?action=update&id=" + Id + "','','DialogWidth=800px;DialogHeight=600px')";
        }
        //查看
        protected string viewDq(string Id)
        {
            return "javascript:window.showModalDialog('FM_JJZBTJ_Detail.aspx?action=look&id=" + Id + "','','DialogWidth=800px;DialogHeight=600px')";
        }



        protected void OnSelectedIndexChanged_dplYearMoth(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }
    }
}
