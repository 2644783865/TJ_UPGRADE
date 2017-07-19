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
    public partial class OM_SHUIDFSP : BasicPage
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
            pager_org.TableName = "(select distinct * from (select OM_SDFY.spbh as spbh,OM_SDFYSP.creatstname as creatstname,OM_SDFYSP.creatstid as creatstid,OM_SDFYSP.creattime as creattime,OM_SDFYSP.state as state,OM_SDFYSP.shrname as shrname,OM_SDFYSP.shrid as shrid from OM_SDFYSP left join OM_SDFY on OM_SDFYSP.spbh=OM_SDFY.spbh)t)s";// ,OM_SDFY.startdate as startdate,OM_SDFY.enddate as enddate
            pager_org.PrimaryKey = "";
            pager_org.ShowFields = "spbh,creatstname,creatstid,shrname,shrid,creattime,state";//,startdate,enddate
            pager_org.OrderField = "spbh";
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
            CommonFun.Paging(dt, rptsushe, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            for (int j = 0; j < rptsushe.Items.Count; j++)
            {
                Label s = (Label)rptsushe.Items[j].FindControl("lbXuHao");
                s.Text = (j + 1 + (pager_org.PageIndex - 1) * UCPaging1.PageSize).ToString();
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }
        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            txtSPBH.Text = "";
        }

        private string StrWhere()
        {

            string strWhere = " 1=1";
            if (txtSPBH.Text.ToString().Trim() != "")
            {
                strWhere += " and spbh like'%" + txtSPBH.Text.Trim() + "%'";
            }
            if (radio_mytask.Checked == true)
            {
                strWhere = string.Format("(shrid='{0}' and state='1')or(creatstid='{0}'and state='0')", asd.userid);
            }
            if (drp_state.SelectedValue == "0")
            {
                strWhere += " and state='0'";
            }
            if (drp_state.SelectedValue == "1")
            {
                strWhere += " and state='1'";
            }
            if (drp_state.SelectedValue == "2")
            {
                strWhere += " and state='2'";
            }
            if (drp_state.SelectedValue == "3")
            {
                strWhere += " and state='3'";
            }
            return strWhere;
        }

        protected void rptsushe_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                LinkButton lnkDelete = (LinkButton)e.Item.FindControl("lnkDelete");
                Label lbstate = (Label)e.Item.FindControl("lbstate");
                HyperLink hlXiuGai = (HyperLink)e.Item.FindControl("hlXiuGai");
                HyperLink hlContract = (HyperLink)e.Item.FindControl("hlContract");
                Label lbcreatstid = (Label)e.Item.FindControl("lbcreatstid");
                Label lbshrid = (Label)e.Item.FindControl("lbshrid");
                hlContract.Visible = false;
                lnkDelete.Visible = false;
                hlXiuGai.Visible = false;
                if ((lbstate.Text == "初始化" || lbstate.Text == "已驳回") && asd.userid == lbcreatstid.Text.ToString())
                {
                    lnkDelete.Visible = true;
                    hlXiuGai.Visible = true;
                }
                if (lbstate.Text == "待审批" && asd.userid == lbshrid.Text.ToString())
                {
                    hlContract.Visible = true;
                }
            }
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string spbh = ((LinkButton)sender).CommandArgument;
            List<string> list = new List<string>();

            string sqlText = "delete from OM_SDFYSP where spbh='" + spbh + "'";
            list.Add(sqlText);
            string sqlText1 = "update OM_SDFY set spbh=NULL, state='0' where spbh='" + spbh + "'";
            list.Add(sqlText1);
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据异常，请联系管理员！！！');", true);
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！！！');", true);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
    }
}
