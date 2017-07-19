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
    public partial class OM_GDGZSCSP : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.userid = Session["UserID"].ToString();
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
            }
            CheckUser(ControlFinder);
            InitVar();
        }
        protected class asd
        {
            public static string userid;
        }
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "OM_GDGZSCSP";
            pager_org.PrimaryKey = "";
            pager_org.ShowFields = "SPBH,SC_Num,ZDR_Name,ZDR_ID,SHR1_ID,SHR2_ID,ZD_Time,SPZT";
            pager_org.OrderField = "SPBH";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 30;
        }/// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptGDGZSP, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            for (int j = 0; j < rptGDGZSP.Items.Count; j++)
            {
                Label s = (Label)rptGDGZSP.Items[j].FindControl("lbXuHao");
                s.Text = (j + 1 + (pager_org.PageIndex - 1) * UCPaging1.PageSize).ToString();
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSPBH.Text = "";
        }

        private string StrWhere()
        {
            string strWhere = " 1=1";
            if (txtSPBH.Text.ToString().Trim() != "")
            {
                strWhere += " and SPBH like'%" + txtSPBH.Text.Trim() + "%'";
            }
            if (radio_Mytask.Checked == true)
            {
                strWhere = string.Format("(SHR1_ID='{0}' and SPZT='1')or(SHR2_ID='{0}'and SPZT='2')or(ZDR_ID='{0}'and SPZT='0')", asd.userid);
            }
            if (drpState.SelectedValue == "0")
            {
                strWhere += " and SPZT='0'";
            }
            if (drpState.SelectedValue == "1")
            {
                strWhere += " and SPZT='1'";
            }
            if (drpState.SelectedValue == "2")
            {
                strWhere += " and SPZT='2'";
            }
            if (drpState.SelectedValue == "3")
            {
                strWhere += " and SPZT='3'";
            }
            if (drpState.SelectedValue == "4")
            {
                strWhere += " and SPZT='4'";
            }
            return strWhere;
        }

        protected void rptGDGZSP_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
                Label lbSPZT = (Label)e.Item.FindControl("lbSPZT");
                HyperLink hlXiuGai = (HyperLink)e.Item.FindControl("hlXiuGai");
                HyperLink hlContract = (HyperLink)e.Item.FindControl("hlContract");
                hlContract.Visible = false;
                lnkDelete.Visible = false;
                hlXiuGai.Visible = false;
                if (lbSPZT.Text == "初始化" && asd.userid == dr["ZDR_ID"].ToString())
                {
                    lnkDelete.Visible = true;
                    hlXiuGai.Visible = true;
                }
                if (lbSPZT.Text == "待审批" && asd.userid == dr["SHR1_ID"].ToString())
                {
                    hlContract.Visible = true;
                }
                if (lbSPZT.Text == "审批中" && asd.userid == dr["SHR2_ID"].ToString())
                {
                    hlContract.Visible = true;
                }
            }
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string SPBH = ((LinkButton)sender).CommandArgument;
            List<string> list = new List<string>();

            string sqlText = "delete from OM_GDGZSCSP where SPBH='" + SPBH + "'";
            list.Add(sqlText);
            string sqlText1 = "delete from OM_GDGZSC_List  where SPBH='" + SPBH + "'";
            list.Add(sqlText1);
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据异常，请联系管理员！！！');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！！！');", true);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
    }
}
