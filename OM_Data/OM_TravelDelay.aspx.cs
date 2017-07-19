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
    public partial class OM_TravelDelay : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string stid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            stid = Session["UserId"].ToString();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                Databind();
            }
            //  CheckUser(ControlFinder);//权限
        }


        #region 初始化分页
        void Pager_PageChanged(int pageNumber)
        {
            Databind();
        }

        private void Databind()
        {
            pager.TableName = "OM_TravelDelay";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "*";
            pager.OrderField = "ID";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 15;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, rptTravelDelay, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            //序号列翻页自增
            for (int j = 0; j < rptTravelDelay.Items.Count; j++)
            {
                Label s = (Label)rptTravelDelay.Items[j].FindControl("lbXuhao");
                s.Text = (j + 1 + (pager.PageIndex - 1) * UCPaging1.PageSize).ToString();
            }
        }

        //筛选条件
        private string strWhere()
        {
            string strWhere = "1=1 ";
            if (txtTravelZDR.Text.Trim() != "")
            {
                strWhere += " and TD_ZDR like '%" + txtTravelZDR.Text.Trim() + "%'";
            }
            if (txtTravelStart.Text.Trim() != "")
            {
                strWhere += " and TD_ZDTime >='" + txtTravelStart.Text.Trim() + "'";
            }
            if (txtTravelEnd.Text.Trim() != "")
            {
                strWhere += " and TD_ZDTime <='" + txtTravelEnd.Text.Trim() + "'";
            }
            if (rblState.SelectedValue == "0")
            {
                strWhere += " and TD_State='0'";//未提交
            }
            else if (rblState.SelectedValue == "1")
            {
                strWhere += " and TD_State in ('1','2','3')";//审核中
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and TD_State ='4'";//已通过
            }
            else if (rblState.SelectedValue == "3")
            {
                strWhere += " and TD_State ='5'";//已驳回
            }
            else if (rblState.SelectedValue == "4")
            {
                strWhere += " and ((TD_State ='0'or TD_State ='5') and TD_ZDRID='" + stid + "') or (TD_State ='1' and TD_SHRIDA='" + stid + "') or ( TD_State='2' and TD_SHRIDB='" + stid + "')or ( TD_State='3' and TD_SHRIDC='" + stid + "')";//我的审核任务
            }
            return strWhere;
        }

        #endregion

        //查询
        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            Databind();
        }

        protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            Databind();
        }

        //可见性控制
        protected void rptTravelDelay_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                HyperLink hlkAudit = e.Item.FindControl("HyperLink2") as HyperLink;
                HyperLink hlkEdit = e.Item.FindControl("HyperLink3") as HyperLink;
                LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;
                hlkAudit.Visible = false;
                hlkEdit.Visible = false;
                lnkDelete.Visible = false;
                if (rblState.SelectedValue == "0")//未提交
                {
                    if (stid == dr["TD_ZDRID"].ToString())
                    {
                        hlkEdit.Visible = true;
                        lnkDelete.Visible = true;
                    }
                }
                else if (rblState.SelectedValue == "1")//审核中
                {
                    if (stid == dr["TD_SHRIDA"].ToString() && dr["TD_State"].ToString() == "1")
                    {
                        hlkAudit.Visible = true;
                    }
                    if (dr["TD_SHLevel"].ToString() != "1")
                    {
                        if (stid == dr["TD_SHRIDB"].ToString() && dr["TD_State"].ToString() == "2")
                        {
                            hlkAudit.Visible = true;
                        }
                        if (dr["TD_SHLevel"].ToString() == "3")
                        {
                            if (stid == dr["TD_SHRIDC"].ToString() && dr["TD_State"].ToString() == "3")
                            {
                                hlkAudit.Visible = true;
                            }
                        }
                    }
                }
                else if (rblState.SelectedValue == "3")//已驳回
                {
                    if (stid == dr["TD_ZDRID"].ToString())
                    {
                        hlkEdit.Visible = true;
                    }
                }
                else if (rblState.SelectedValue == "4")//我的审核任务
                {
                    if (dr["TD_State"].ToString() == "0")
                    {
                        hlkEdit.Visible = true;
                        lnkDelete.Visible = true;
                    }
                    else if (dr["TD_State"].ToString() == "5")
                    {
                        hlkEdit.Visible = true;
                    }
                    else
                    {
                        hlkAudit.Visible = true;
                    }
                }
                else if (rblState.SelectedValue == "")//全部
                {
                    if (dr["TD_State"].ToString() == "0" && stid == dr["TD_ZDRID"].ToString())
                    {
                        hlkEdit.Visible = true;
                        lnkDelete.Visible = true;
                    }
                    else if (dr["TD_State"].ToString() == "1" && stid == dr["TD_SHRIDA"].ToString())
                    {
                        hlkAudit.Visible = true;
                    }
                    else if (dr["TD_State"].ToString() == "2" && stid == dr["TD_SHRIDB"].ToString())
                    {
                        hlkAudit.Visible = true;
                    }
                    else if (dr["TD_State"].ToString() == "3" && stid == dr["TD_SHRIDC"].ToString())
                    {
                        hlkAudit.Visible = true;
                    }
                    else if (dr["TD_State"].ToString() == "5" && stid == dr["TD_ZDRID"].ToString())
                    {
                        hlkEdit.Visible = true;
                    }
                }
            }
        }
        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string context = ((LinkButton)sender).CommandArgument;
            List<string> list = new List<string>();
            string sqllist = "delete from OM_TravelDelay where TD_Code='" + context.Trim() + "'";
            list.Add(sqllist);
            string sqldetail = "delete from OM_TravelDetail where TD_Code='" + context.Trim() + "'";
            list.Add(sqldetail);
            try
            {
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('单号为" + context.Trim() + "的差旅延期信息已删除！');location.href='OM_TravelDelay.aspx';", true);
            }
            catch
            {
                Response.Write("<script>alert('数据操作出现问题，请联系管理员！')</script>");
            }
        }
    }
}

