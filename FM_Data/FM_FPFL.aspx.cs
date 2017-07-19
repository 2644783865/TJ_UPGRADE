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
    public partial class FM_FPFL : System.Web.UI.Page
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
        PagerQueryParam pager_org = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "(select GI_DATE,b.GI_CODE as GI_CODE,b.GI_MATCODE as GI_MATCODE,b.GI_NAME as GI_NAME,b.GI_GUIGE as GI_GUIGE,b.GI_UNIT as GI_UNIT,b.GI_NUM as GI_NUM,b.GI_UNITPRICE as GI_UNITPRICE,b.GI_CTAXUPRICE as GI_CTAXUPRICE,b.GI_AMTMNY as GI_AMTMNY,b.GI_CTAMTMNY as GI_CTAMTMNY,b.GI_PMODE as GI_PMODE,b.GI_PTCODE as GI_PTCODE,a.GI_SUPPLIERNM from TBFM_GHINVOICETOTAL as a left join TBFM_GHINVOICEDETAIL as b on a.GI_CODE=b.GI_CODE)t";
            pager_org.PrimaryKey = "GI_PTCODE";
            pager_org.ShowFields = "GI_DATE,GI_CODE,GI_MATCODE,GI_NAME,GI_GUIGE,GI_UNIT,GI_NUM,GI_UNITPRICE,GI_CTAXUPRICE,GI_AMTMNY,GI_CTAMTMNY,GI_PMODE,GI_PTCODE,GI_SUPPLIERNM";
            pager_org.OrderField = "GI_CODE";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
        }

        private string strstring()
        {
            string sqlText = "1=1";
            if (dplYear.SelectedIndex != 0 || dplMoth.SelectedIndex != 0)
            {
                string yearmonth = dplYear.SelectedValue.ToString() +"-"+ dplMoth.SelectedValue.ToString();
                sqlText += " and GI_DATE like '%" + yearmonth.ToString() + "%'";
            }
            if (dplwltype.SelectedValue == "0")
            {
                sqlText += " and GI_MATCODE like '01.07%'";
            }
            if (dplwltype.SelectedValue == "1")
            {
                sqlText += " and GI_MATCODE not like '01.07%'";
            }
            if (!string.IsNullOrEmpty(gys_name.Text.Trim()))
            {
                sqlText += "  and  GI_SUPPLIERNM  like '%" + gys_name.Text.Trim() + "%'";
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
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
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



        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }



        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string condition = strstring();
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbfpbh= (Label)e.Item.FindControl("GI_CODE");
                Label lbwlbm = (Label)e.Item.FindControl("GI_MATCODE");
                Label lbwlmc = (Label)e.Item.FindControl("GI_NAME");

                Label lbhuige = (Label)e.Item.FindControl("GI_GUIGE");
                Label lbunit = (Label)e.Item.FindControl("GI_UNIT");
                Label lbsl = (Label)e.Item.FindControl("GI_NUM");
                Label lbdj = (Label)e.Item.FindControl("GI_UNITPRICE");
                Label lbhsdj = (Label)e.Item.FindControl("GI_CTAXUPRICE");
                Label lbje = (Label)e.Item.FindControl("GI_AMTMNY");
                Label lbhsje = (Label)e.Item.FindControl("GI_CTAMTMNY");
                Label lbwllx = (Label)e.Item.FindControl("GI_PMODE");
                Label lbjhgzh = (Label)e.Item.FindControl("GI_PTCODE");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(sum(isnull(GI_AMTMNY,0)),0) as GI_AMTMNY,isnull(sum(isnull(GI_CTAMTMNY,0)),0) as GI_CTAMTMNY from (select GI_DATE,b.GI_CODE as GI_CODE,b.GI_MATCODE as GI_MATCODE,b.GI_NAME as GI_NAME,b.GI_GUIGE as GI_GUIGE,b.GI_UNIT as GI_UNIT,b.GI_NUM as GI_NUM,b.GI_UNITPRICE as GI_UNITPRICE,b.GI_CTAXUPRICE as GI_CTAXUPRICE,b.GI_AMTMNY as GI_AMTMNY,b.GI_CTAMTMNY as GI_CTAMTMNY,b.GI_PMODE as GI_PMODE,b.GI_PTCODE as GI_PTCODE,a.GI_SUPPLIERNM from TBFM_GHINVOICETOTAL as a left join TBFM_GHINVOICEDETAIL as b on a.GI_CODE=b.GI_CODE)t where " + condition;
                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {
                    jehj = Convert.ToDouble(drhj["GI_AMTMNY"].ToString());
                    hsjehj = Convert.ToDouble(drhj["GI_CTAMTMNY"].ToString());
                }
                drhj.Close();
                Label lbjehj = (Label)e.Item.FindControl("lbjehj");
                Label lbhsjehj = (Label)e.Item.FindControl("lbhsjehj");

                lbjehj.Text = jehj.ToString("0.00");
                lbhsjehj.Text = hsjehj.ToString();
            }
        }


        //2016.12.27按供应商导出
        protected void btnExport_Click(object sender, EventArgs e) //导出
        {
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                string year_month = dplYear.SelectedValue + "-" + dplMoth.SelectedValue;
                string sql_btn = "select GI_SUPPLIERNM,sum(GI_NUM) as totatl_num,sum(GI_AMTMNY) as total_amtmny from (select GI_DATE,b.GI_CODE as GI_CODE,b.GI_MATCODE as GI_MATCODE,b.GI_NAME as GI_NAME,b.GI_GUIGE as GI_GUIGE,b.GI_UNIT as GI_UNIT,b.GI_NUM as GI_NUM,b.GI_UNITPRICE as GI_UNITPRICE,b.GI_CTAXUPRICE as GI_CTAXUPRICE,b.GI_AMTMNY as GI_AMTMNY,b.GI_CTAMTMNY as GI_CTAMTMNY,b.GI_PMODE as GI_PMODE,b.GI_PTCODE as GI_PTCODE,a.GI_SUPPLIERNM from TBFM_GHINVOICETOTAL as a left join TBFM_GHINVOICEDETAIL as b on a.GI_CODE=b.GI_CODE)t where 1=1 and GI_DATE like '%" + year_month + "%' group by GI_SUPPLIERNM";
                exportCommanmethod.exporteasy(sql_btn, "发票明细供应商汇总.xls", "发票明细供应商汇总.xls", true, true, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
            }
        }
    }
}
