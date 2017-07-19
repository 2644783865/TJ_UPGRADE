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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ComputerLIst : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {

                databind();
                ControlVisible();
            }
            //  CheckUser(ControlFinder);
        }


        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            pager.TableName = "OM_COMPUTERLIST";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "Id";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
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

            if (txtStart.Text.Trim() != "")
            {
                strWhere += " and SqTime>='" + txtStart.Text.Trim() + "'";
            }
            if (txtEnd.Text.Trim() != "")
            {
                strWhere += " and SqTime<='" + txtEnd.Text.Trim() + "'";
            }

            if (rblState.SelectedValue == "0")
            {
                strWhere += " and State='0'";
            }
            else if (rblState.SelectedValue == "1")
            {
                strWhere += " and State in ('1','2')";
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and State ='3'";
            }
            else if (rblState.SelectedValue == "3")
            {
                strWhere += " and State ='4'";
            }
            else if (rblState.SelectedValue == "4")
            {
                strWhere += " and ((State ='1' and SPRIDA='" + Session["UserId"].ToString() + "') or ( State='2' and SPRIDB='" + Session["UserId"].ToString() + "')or (State='3' and " + Session["UserGroup"].ToString() + "='管理员'))";
            }
            else if (rblState.SelectedValue == "5")
            {
                strWhere += " and State ='5'";
            }
            return strWhere;
        }

        #endregion

        protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }

        private void ControlVisible()
        {
            foreach (RepeaterItem item in rep_Kaohe.Items)
            {
                HyperLink hlkEdit = item.FindControl("HyperLink2") as HyperLink;
                HyperLink hlkAudit = item.FindControl("HyperLink3") as HyperLink;
                LinkButton hlGZ = item.FindControl("hlGZ") as LinkButton;
                Label lbState = item.FindControl("lbState") as Label;
                if (rblState.SelectedValue == "0" || rblState.SelectedValue == "3")
                {
                    hlkEdit.Visible = true;
                }
                else
                {
                    hlkEdit.Visible = false;
                }
                if (rblState.SelectedValue == "4")
                {
                    if (lbState.Text.ToString() != "3")
                        hlkAudit.Visible = true;
                }
                else
                {
                    hlkAudit.Visible = false;
                }

                if (rblState.SelectedValue == "2")
                {
                    hlGZ.Visible = true;
                }
                else
                {
                    hlGZ.Visible = false;
                }
                if (rblState.SelectedValue == "")
                {
                    if (lbState.Text.ToString() == "3" && Session["UserId"].ToString() == "286")
                    {
                        hlGZ.Visible = true;
                    }
                }
            }
        }

        protected void hlGZ_OnClick(object sender, EventArgs e)
        {
            string context = ((LinkButton)sender).CommandName;
            string sql = "update OM_COMPUTERLIST set State='5',GZRId='" + Session["UserId"].ToString() + "',GZR='" + Session["UserName"].ToString() + "' where Context='" + context + "'";
            DBCallCommon.ExeSqlText(sql);
            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }
    }
}
