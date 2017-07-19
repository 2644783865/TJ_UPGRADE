using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_LSZPJH_HZGL : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
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
            pager_org.TableName = "OM_ZPJH as a left join OM_SP as b on a.JH_HZSJ=b.SPFATHERID";
            pager_org.PrimaryKey = "JH_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "JH_HZBH";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptZPJH, UCPaging1, palNoData);
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
            string sql = " SPZT is not null and JH_LX='LS' ";
            //if (rblRW.SelectedValue == "1")
            //{
            //    if (username != "李圆")
            //    {
            //        sql += " and JH_SQR like'%" + username + "%'";
            //    }
            //}
            if (rblSPZT.SelectedValue == "1")
            {
                sql += " and SPZT='0'";
            }
            else if (rblSPZT.SelectedValue == "2")
            {
                sql += " and SPZT='1'";
            }
            else if (rblSPZT.SelectedValue == "3")
            {
                sql += " and SPZT in('1y','2y')";
            }
            else if (rblSPZT.SelectedValue == "4")
            {
                sql += " and SPZT='y'";
            }
            else if (rblSPZT.SelectedValue == "5")
            {
                sql += " and SPZT='n'";
            }
            if (txtBM.Text.Trim() != "")
            {
                sql += " and JH_ZPBM like'%" + txtBM.Text.Trim() + "%'";
            }
            if (txtGW.Text.Trim() != "")
            {
                sql += " and JH_GWMC like '%" + txtGW.Text.Trim() + "%'";
            }
            return sql;
        }
        #endregion

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        private class asd
        {
            public static string bh;
            public static string hzsj;
        }

        protected void rptZPJH_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                asd.bh = string.Empty;
                asd.hzsj = string.Empty;
                return;
            }
            HyperLink hplAlter = (HyperLink)e.Item.FindControl("hplAlter");
            hplAlter.Visible = false;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            if ((drv["SPZT"].ToString() == "0" || drv["SPZT"].ToString() == "n") && username == drv["ZDR"].ToString())
            {
                hplAlter.Visible = true;
            }

            //去重复显示
            if (drv["JH_HZSJ"].ToString() == asd.hzsj)
            {
                hplAlter.Visible = false;
            }
            else
            {
                asd.hzsj = drv["JH_HZSJ"].ToString();
            }
            if (drv["JH_HZBH"].ToString() == asd.bh)
            {
                e.Item.FindControl("JH_HZBH").Visible = false;
            }
            else
            {
                asd.bh = drv["JH_HZBH"].ToString();
            }
        }

        protected void btnHZ_onserverclick(object sender, EventArgs e)
        {
            //List<string> list = new List<string>();
            //string JH_HZSJ = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //for (int i = 0, length = rptZPJH.Items.Count; i < length; i++)
            //{
            //    CheckBox cbx = (CheckBox)rptZPJH.Items[i].FindControl("cbxXuHao");
            //    if (cbx.Checked)
            //    {
            //        string JH_ID = ((HiddenField)rptZPJH.Items[i].FindControl("JH_ID")).Value;
            //        string JH_SFHZ = ((HiddenField)rptZPJH.Items[i].FindControl("JH_SFHZ")).Value;
            //        string JH_SFTJ = ((HiddenField)rptZPJH.Items[i].FindControl("JH_SFTJ")).Value;

            //        if (JH_SFTJ != "y")
            //        {
            //            Response.Write("<script>alert('您勾选的内容包括未提交的单据，请勾选待汇总汇总的单据！！！')</script>");
            //            return;
            //        }
            //        if (JH_SFHZ == "y")
            //        {
            //            Response.Write("<script>alert('您勾选的内容包括已经汇总的单据，请勾选待汇总的单据！！！')</script>");
            //            return;
            //        }
            //        string sql = "update OM_ZPJH set JH_HZSJ='" + JH_HZSJ + "' where JH_ID='" + JH_ID + "'";
            //        list.Add(sql);
            //    }
            //}
            //if (list.Count == 0)
            //{
            //    Response.Write("<script>alert('请至少勾选一条！！！')</script>");
            //    return;
            //}
            //try
            //{
            //    DBCallCommon.ExecuteTrans(list);
            //}
            //catch
            //{
            //    Response.Write("<script>alert('汇总语句出现问题，请联系管理员！！！')</script>");
            //    return;
            //}
            Response.Redirect("OM_LSZPJH_HZ.aspx?action=collect");
        }
    }
}
