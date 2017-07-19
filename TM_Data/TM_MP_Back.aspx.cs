using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MP_Back : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RBLBind();
                this.InitPage();
                //  GetTaskTypeData();

            }
            VisibleControl();
            ucPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);

        }

        private void VisibleControl()
        {
            if ( rblMyTask.SelectedValue=="0"&&genRblstatus.SelectedValue=="0")
            {
                GridView2.Columns[16].Visible = true;
            }
            else
            {
                GridView2.Columns[16].Visible = false;
            }
        }

        private void RBLBind()
        {
            //未处理
            string sqlText = "select count(*) from TBPC_PLAN_BACK where state='0' and sqrid='" + Session["UserID"] + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    genRblstatus.Items.Add(new ListItem("未处理", "0"));
                }
                else
                {
                    genRblstatus.Items.Add(new ListItem("未处理" + "</label><label><font color=red>(" + dt.Rows[0][0].ToString() + ")</font>", "0"));
                }
            }
            genRblstatus.Items.Add(new ListItem("已处理", "1"));
            genRblstatus.Items.Add(new ListItem("全部", "2"));
            genRblstatus.SelectedIndex = 0;
        }

        private void InitPage()
        {

            ucPaging1.CurrentPage = 1;
            InitPager();
            bindGrid();
        }

        private void InitPager()
        {
            pager.TableName = "View_TBPC_PLAN_BACK";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "Id";
            pager.StrWhere = this.GetSearchList();
            pager.OrderType = 1;//升序排列
            pager.PageSize = 20;
            ucPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        private string GetSearchList()
        {
            string strWhere = " 1=1 ";

            if (txtSearch.Text.Trim() != "")
            {
                strWhere += " and " + ddlSearch.SelectedValue + " like '%" + txtSearch.Text.Trim() + "%'";
            }
            if (genRblstatus.SelectedValue != "2")//待当前审核人审核
            {
                strWhere += " and state='" + genRblstatus.SelectedValue + "' ";
            }
            if (rblMyTask.SelectedValue=="0")
            {
                strWhere += " and sqrid='" + Session["UserID"] + "'";
            }
            return strWhere;
        }

        void Pager_PageChangedMS(int pageNumber)
        {
            InitPager();
            bindGrid();
        }
        private void bindGrid()
        {

            pager.PageIndex = ucPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, GridView2, ucPaging1, NoDataPanelGen);
            if (NoDataPanelGen.Visible)
            {
                ucPaging1.Visible = false;

            }
            else
            {
                ucPaging1.Visible = true;
                ucPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

        }

        protected void hlDeal_OnClick(object sender, EventArgs e)
        {
            string Id = ((LinkButton)sender).CommandArgument;
            string sqlText = "update TBPC_PLAN_BACK set state='1' where Id='"+Id+"'";
            DBCallCommon.ExeSqlText(sqlText);
       
        }


        protected void Search_Click(object sender, EventArgs e)
        {

            InitPage();
          //  GetTaskTypeData();

        }


        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ////string proId = e.Row.Cells[9].Text.Trim();
                //string proId = ((HiddenField)e.Row.FindControl("HIDXUHAO")).Value.Trim();
                //e.Row.Attributes["style"] = "Cursor:hand";

                //if (genRblstatus.SelectedIndex == 1 || genRblstatus.SelectedIndex == 3)
                //{
                //    for (int i = 0; i < 8; i++)
                //    {
                //        e.Row.Cells[i].Attributes.Add("title", "双击修改工序卡片数据");
                //        e.Row.Cells[i].Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLinkGen('" + proId + "');", GridView2.DataKeys[e.Row.RowIndex].Value.ToString());

                //    }
                //}

                //e.Row.Cells[4].Attributes.Add("title", "单击下载附件");


            }
        }

    }
}
