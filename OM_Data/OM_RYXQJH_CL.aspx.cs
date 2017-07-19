using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_RYXQJH_CL : System.Web.UI.Page
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
                //MergeCells();
            }
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
            //MergeCells();
        }

        private void bindrpt()
        {
            pager_org.TableName = "OM_ZPJH as a left join OM_SP as b on a.JH_HZSJ=b.SPFATHERID";
            pager_org.PrimaryKey = "JH_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "JH_HZSJ,JH_ZPBM";
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
            string sql = "JH_HZSJ is not null and SPZT='y' ";
            if (rblCLZT.SelectedValue=="1")
            {
                sql += " and JH_SFCL is null ";
            }
            else if (rblCLZT.SelectedValue=="2")
            {
                sql += " and JH_SFCL='y' ";
            }
            else if (rblCLZT.SelectedValue=="3")
            {
                sql += " and JH_SFCL='b'";
            }
            return sql;
        }
        #endregion

        private void MergeCells()//合并单元格
        {
            //for (int i = rptZPJH.Items.Count - 1; i > 0; i--)
            //{
            //    HtmlTableCell tdJH_HZBH = (HtmlTableCell)rptZPJH.Items[i].FindControl("tdJH_HZBH");
            //    HtmlTableCell tdJH_ZPBM = (HtmlTableCell)rptZPJH.Items[i].FindControl("tdJH_ZPBM");
            //    HtmlTableCell tdJH_GWMC = (HtmlTableCell)rptZPJH.Items[i].FindControl("tdJH_GWMC");

            //    HtmlTableCell tdJH_HZBH1 = (HtmlTableCell)rptZPJH.Items[i - 1].FindControl("tdJH_HZBH");
            //    HtmlTableCell tdJH_ZPBM1 = (HtmlTableCell)rptZPJH.Items[i - 1].FindControl("tdJH_ZPBM");
            //    HtmlTableCell tdJH_GWMC1 = (HtmlTableCell)rptZPJH.Items[i - 1].FindControl("tdJH_GWMC");

            //    tdJH_HZBH.RowSpan = (tdJH_HZBH.RowSpan == -1) ? 1 : tdJH_HZBH.RowSpan;
            //    tdJH_ZPBM.RowSpan = (tdJH_ZPBM.RowSpan == -1) ? 1 : tdJH_ZPBM.RowSpan;
            //    tdJH_GWMC.RowSpan = (tdJH_GWMC.RowSpan == -1) ? 1 : tdJH_GWMC.RowSpan;
            //    tdJH_HZBH1.RowSpan = (tdJH_HZBH1.RowSpan == -1) ? 1 : tdJH_HZBH1.RowSpan;
            //    tdJH_ZPBM1.RowSpan = (tdJH_ZPBM1.RowSpan == -1) ? 1 : tdJH_ZPBM1.RowSpan;
            //    tdJH_GWMC1.RowSpan = (tdJH_GWMC1.RowSpan == -1) ? 1 : tdJH_GWMC1.RowSpan;

            //    if (tdJH_HZBH.InnerText == tdJH_HZBH1.InnerText)
            //    {
            //        tdJH_HZBH.Visible = false;
            //        tdJH_HZBH1.RowSpan += tdJH_HZBH.RowSpan;
            //    }

            //    if (tdJH_ZPBM.InnerText == tdJH_ZPBM1.InnerText)
            //    {
            //        tdJH_ZPBM.Visible = false;
            //        tdJH_ZPBM1.RowSpan += tdJH_ZPBM.RowSpan;
            //    }
            //    if (tdJH_GWMC.InnerText == tdJH_GWMC1.InnerText)
            //    {
            //        tdJH_GWMC.Visible = false;
            //        tdJH_GWMC1.RowSpan += tdJH_GWMC.RowSpan;
            //    }
            //}
        }

        private void Query()
        {
            bindrpt();
        }

        protected void rptZPJH_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trr");
            if (drv["JH_SFCL"].ToString()=="y")
            {
                tr.BgColor = System.Drawing.Color.Green.ToString();
            }
        }

        protected void btnChuLi_onserverclick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0, length = rptZPJH.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptZPJH.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string JH_ID = ((HiddenField)rptZPJH.Items[i].FindControl("JH_ID")).Value;
                    string sql = "update OM_ZPJH set JH_SFCL='y' where JH_ID=" + JH_ID;
                    list.Add(sql);
                }
            }
            if (list.Count == 0)
            {
                Response.Write("<script>alert('请勾选一条进行处理！！！')</script>");
                return;
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                Response.Write("<script>alert('处理的语句出现问题，请与管理员联系！！！')</script>");
                return;
            }
            bindrpt();
        }

        protected void btnDelete_onserverclick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0, length = rptZPJH.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptZPJH.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string JH_ID = ((HiddenField)rptZPJH.Items[i].FindControl("JH_ID")).Value;
                    string sql = "update OM_ZPJH set JH_SFCL='b' where JH_ID=" + JH_ID;
                    list.Add(sql);
                }
            }
            if (list.Count == 0)
            {
                Response.Write("<script>alert('请勾选一条进行处理！！！')</script>");
                return;
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                Response.Write("<script>alert('删除的语句出现问题，请与管理员联系！！！')</script>");
                return;
            }
            bindrpt();
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

    }
}
