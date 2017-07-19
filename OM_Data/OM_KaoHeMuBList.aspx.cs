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
    public partial class OM_KaoHeMuBList : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                BindPart();
                databind();

            } CheckUser(ControlFinder);
        }



        private void BindPart()
        {
            string Stid = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(23, Stid);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();

            ListItem itemNew = new ListItem();
            itemNew.Text = "通用模板";
            itemNew.Value = "TongY";
            ddl_Depart.Items.Insert(0, itemNew);
        }


        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            pager.TableName = "View_TBDS_KaoHeMB";
            pager.PrimaryKey = "";
            pager.ShowFields = "*";
            pager.OrderField = "kh_Dep";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 100;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, rep_Kaohe, UCPaging1, NoDataPanel);
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

        private string strWhere()
        {
            string strWhere = "1=1 ";

            if (ddl_Depart.SelectedValue != "00")
            {
                strWhere += "and kh_Dep='" + ddl_Depart.SelectedValue + "'";

            }
            return strWhere;
        }

        #endregion
        protected void ddl_Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            databind();
        }
        protected void rep_Kaohe_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    string hidAddPer = ((HtmlInputHidden)e.Item.FindControl("hidAddPer")).Value;
            //    if (hidAddPer != Session["UserId"].ToString() && Session["UserId"].ToString() != "150" && Session["UserId"].ToString() != "151" && Session["UserId"].ToString() != "3")
            //    {
            //        HyperLink hpMuBEdit = e.Item.FindControl("hpMuBEdit") as HyperLink;
            //        //hpMuBEdit.Visible = false;
            //    }
            //}
        }
    }
}
