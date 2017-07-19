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
using System.IO;
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_CKQKCX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string position = Session["POSITION"].ToString();
            if (!IsPostBack)
            {
                if (position == "0501")
                {
                    btnqr.Enabled = true;
                }
                this.BindYearMoth(dplYear, dplMoth);
                this.bindzdr();
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

        //绑定制单人
        private void bindzdr()
        {
            string sqltext = "";
            sqltext = "select distinct Doc from (select * from ((select * from View_SM_OUT where PTC not like '%备库%' and PTC not like '%beiku%' and PTC not like '%BEIKU%')a left join (select * from TBPC_PURCHASEPLAN as a left join (select BG_PTC,BG_NUM,BG_FZNUM,BG_NAME,BG_NAMEID,BG_STATE,RESULT from TBPC_BG where RESULT='已驳回') as b on a.PUR_PTCODE=b.BG_PTC where PUR_PTCODE not like '%备库%' and PUR_PTCODE not like '%beiku%' and PUR_PTCODE not like '%BEIKU%')b on a.PTC=b.PUR_PTCODE))c";
            string dataText = "Doc";
            string dataValue = "Doc";
            DBCallCommon.BindDdl(drp_zdr, sqltext, dataText, dataValue);
            drp_zdr.SelectedIndex = 0;
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
            pager_org.TableName = "(select *,(isnull(PUR_NUM,0)-isnull(BG_NUM,0)) as PUR_NUMR from ((select * from View_SM_OUT where patindex('%备库%',PTC)=0 and patindex('%beiku%',PTC)=0 and patindex('%BEIKU%',PTC)=0)a left join (select * from TBPC_PURCHASEPLAN as a left join (select BG_PTC,BG_NUM,BG_FZNUM,BG_NAME,BG_NAMEID,BG_STATE,RESULT from TBPC_BG where RESULT='已驳回') as b on a.PUR_PTCODE=b.BG_PTC where patindex('%备库%',PUR_PTCODE)=0 and patindex('%beiku%',PUR_PTCODE)=0 and patindex('%BEIKU%',PUR_PTCODE)=0)b on a.PTC=b.PUR_PTCODE))c";
            pager_org.PrimaryKey = "PTC";
            pager_org.ShowFields = "*,(isnull(RealNumber,0)-isnull(PUR_NUMR,0)) as PUR_CCNUM";
            pager_org.OrderField = "ApprovedDate";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 30;
        }

        private string strstring()
        {
            string sqlText = "isnull(RealNumber,0)>isnull(PUR_NUMR,0) and PUR_NUMR is not null and OutCode not in(select OutCode from View_SM_OUT where patindex('%R%',OutCode)>0)";
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                string yearmonth = dplYear.SelectedValue.ToString() +"-"+ dplMoth.SelectedValue.ToString();
                sqlText += " and  patindex('%" + yearmonth.ToString() + "%',ApprovedDate)>0";
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                sqlText += " and  patindex('" + dplYear.SelectedValue.ToString() + "-%',ApprovedDate)>0";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                sqlText += " and  patindex('%-" + dplMoth.SelectedValue.ToString() + "-%',ApprovedDate)>0";
            }
            if (radio_wqr.Checked)
            {
                sqlText += " and (PUR_CKQR='0' or PUR_CKQR is null)";
            }
            if (radio_yqr.Checked)
            {
                sqlText += " and PUR_CKQR='1'";
            }
            if (tbtsaid.Text != "")
            {
                sqlText += " and patindex('%" + tbtsaid.Text.ToString().Trim() + "%',TSAID)>0";
            }
            if (tbptc.Text != "")
            {
                sqlText += " and patindex('%" + tbptc.Text.ToString().Trim() + "%',PTC)>0";
            }
            if (tbtype.Text != "")
            {
                sqlText += " and patindex('%" + tbtype.Text.ToString().Trim() + "',PlanMode)>0";
            }
            if (drp_ccbl.SelectedValue != "")
            {
                if (drp_ccbl.SelectedValue == "0")
                {
                    sqlText += " and isnull(RealNumber,0)>((isnull(PUR_NUMR,0))*1.05)";
                }
                else if (drp_ccbl.SelectedValue == "1")
                {
                    sqlText += " and isnull(RealNumber,0)>((isnull(PUR_NUMR,0))*1.15)"; 
                }
                else if (drp_ccbl.SelectedValue == "2")
                {
                    sqlText += " and isnull(RealNumber,0)>((isnull(PUR_NUMR,0))*1.3)";
                }
            }
            if (drp_zdr.SelectedIndex != 0)
            {
                sqlText += " and Doc='" + drp_zdr.SelectedValue.Trim() + "'";
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
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lldh1 = (Label)rptProNumCost.Items[i].FindControl("OutCode");
                        Label lldh2 = (Label)rptProNumCost.Items[j].FindControl("OutCode");
                        if (lldh1.Text.ToString() == lldh2.Text.ToString())
                        {
                            lldh1.Visible = false;
                        }
                    }
                }
                Label llddh1 = (Label)rptProNumCost.Items[i].FindControl("OutCode");
                Label llddh2 = (Label)rptProNumCost.Items[i + 1].FindControl("OutCode");
                if (llddh1.Text.ToString() == llddh2.Text.ToString())
                {
                    llddh2.Visible = false;
                }
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



        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        /// <summary>
        /// 年、月份改变 查询
        /// </summary>
        protected void OnSelectedIndexChanged_dplYearMoth(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }


        protected void drp_ccbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void btnqr_OnClick(object sender, EventArgs e)
        {
            int count = 0;
            string ptcode = "";
            List<string> list = new List<string>();
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                if ((rptProNumCost.Items[i].FindControl("CKBOX_SELECT") as CheckBox).Checked)
                {
                    count++;
                    ptcode = ((Label)rptProNumCost.Items[i].FindControl("ptcode")).Text;
                    string sql = "update TBPC_PURCHASEPLAN set PUR_CKQR='1' where PUR_PTCODE='" + ptcode + "'";
                    list.Add(sql);
                }
            }
            if (count > 0)
            {
                DBCallCommon.ExecuteTrans(list);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择要确认的项!')</script>");
                return;
            }
            this.InitVar();
            this.bindGrid();
        }

        protected void radio_wqr_CheckedChanged(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void radio_yqr_CheckedChanged(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void radio_all_CheckedChanged(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }
    }
}
