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
    public partial class OM_TravelApply : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string stid = "";
        string stdepid = "";
        string stposition = "";
        List<string> listcode = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            stid = Session["UserId"].ToString();
            stdepid = Session["UserDept"].ToString();
            stposition = Session["POSITION"].ToString();
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
            pager.TableName = "(select a.*,b.TA_SHLevel,b.TA_ZDRID,b.TA_SHRIDA,b.TA_SHRIDB,b.TA_SHRIDC from OM_TravelApplyDetail as a left join OM_TravelApply as b on a.TA_Code=b.TA_Code)t";
            pager.PrimaryKey = "";
            pager.ShowFields = "*";
            pager.OrderField = "TA_Code";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 15;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, rptTravel, UCPaging1, NoDataPanel);
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
            for (int j = 0; j < rptTravel.Items.Count; j++)
            {
                Label s = (Label)rptTravel.Items[j].FindControl("lbXuhao");
                s.Text = (j + 1 + (pager.PageIndex - 1) * UCPaging1.PageSize).ToString();
            }
        }

        //筛选条件
        private string strWhere()
        {
            //2017.2.17添加
            string strWhere = "1=1 ";
            if (stdepid.Contains("生产"))
            {
                strWhere = "(TA_SQRDep like '%生产%') or (TA_SQRDep like '%车间%')";
            }
            else if (stposition != "0602" && stposition != "0607")
                strWhere = "TA_SQRDep like '" + stdepid + "%'";
            if (txtTravelSQR.Text.Trim() != "")
            {
                strWhere += " and TA_SQR like '%" + txtTravelSQR.Text.Trim() + "%'";
            }
            if (txtTravelStart.Text.Trim() != "")
            {
                strWhere += " and TA_StartTimePlan >='" + txtTravelStart.Text.Trim() + "'";
            }
            if (txtTravelEnd.Text.Trim() != "")
            {
                strWhere += " and TA_StartTimePlan <='" + txtTravelEnd.Text.Trim() + "'";
            }
            if (rblState.SelectedValue == "0")
            {
                strWhere += " and TA_State='0'";//未提交
            }
            else if (rblState.SelectedValue == "1")
            {
                strWhere += " and TA_State in ('1','2','3')";//审核中
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and TA_State ='4'";//已通过
            }
            else if (rblState.SelectedValue == "3")
            {
                strWhere += " and TA_State ='5'";//已驳回
            }
            else if (rblState.SelectedValue == "4")
            {
                strWhere += " and TA_State ='6'";//已确认
            }
            else if (rblState.SelectedValue == "5")
            {
                strWhere += " and ((TA_State ='0' or TA_State ='4' or TA_State ='5') and TA_ZDRID='" + stid + "') or (TA_State ='1' and TA_SHRIDA='" + stid + "') or ( TA_State='2' and TA_SHRIDB='" + stid + "')or ( TA_State='3' and TA_SHRIDC='" + stid + "')";//我的审核任务
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
        protected void rptTravel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                string lbTA_Code = ((Label)e.Item.FindControl("lbTA_Code")).Text.Trim();
                HyperLink hlkViewOrAudit = e.Item.FindControl("HyperLink1") as HyperLink;
                Label wordtext = e.Item.FindControl("wordtext") as Label;
                HyperLink hlkEdit = e.Item.FindControl("HyperLink2") as HyperLink;
                HyperLink hlkSure = e.Item.FindControl("HyperLink3") as HyperLink;
                LinkButton lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;
                HtmlTableCell validcode = e.Item.FindControl("validcode") as HtmlTableCell;
                wordtext.Text = "查看";
                hlkViewOrAudit.NavigateUrl = "OM_TravelApplyDetail.aspx?action=view&key=" + lbTA_Code + "";
                hlkEdit.Visible = false;
                hlkSure.Visible = false;
                lnkDelete.Visible = false;
                if (rblState.SelectedValue == "0" || rblState.SelectedValue == "3")//0未提交、3已驳回
                {
                    if (stid == dr["TA_ZDRID"].ToString())
                    {
                        hlkEdit.Visible = true;
                        lnkDelete.Visible = true;
                    }
                }
                else if (rblState.SelectedValue == "1")//审核中
                {
                    if (stid == dr["TA_SHRIDA"].ToString() && dr["TA_State"].ToString() == "1")
                    {
                        wordtext.Text = "审核";
                        hlkViewOrAudit.NavigateUrl = "OM_TravelApplyDetail.aspx?action=audit&key=" + lbTA_Code + "";
                    }
                    if (dr["TA_SHLevel"].ToString() != "1")
                    {
                        if (stid == dr["TA_SHRIDB"].ToString() && dr["TA_State"].ToString() == "2")
                        {
                            wordtext.Text = "审核";
                            hlkViewOrAudit.NavigateUrl = "OM_TravelApplyDetail.aspx?action=audit&key=" + lbTA_Code + "";
                        }
                        if (dr["TA_SHLevel"].ToString() == "3")
                        {
                            if (stid == dr["TA_SHRIDC"].ToString() && dr["TA_State"].ToString() == "3")
                            {
                                wordtext.Text = "审核";
                                hlkViewOrAudit.NavigateUrl = "OM_TravelApplyDetail.aspx?action=audit&key=" + lbTA_Code + "";
                            }
                        }
                    }
                }
                else if (rblState.SelectedValue == "2")//已通过
                {
                    if (stid == dr["TA_ZDRID"].ToString())
                    {
                        hlkSure.Visible = true;
                    }
                }
                else if (rblState.SelectedValue == "5")//我的审核任务
                {
                    if (dr["TA_State"].ToString() == "0")
                    {
                        hlkEdit.Visible = true;
                        lnkDelete.Visible = true;
                    }
                    else if (dr["TA_State"].ToString() == "4")
                    {
                        hlkSure.Visible = true;
                    }
                    else if (dr["TA_State"].ToString() == "5")
                    {
                        hlkEdit.Visible = true;
                        lnkDelete.Visible = true;
                    }
                    else
                    {
                        wordtext.Text = "审核";
                        hlkViewOrAudit.NavigateUrl = "OM_TravelApplyDetail.aspx?action=audit&key=" + lbTA_Code + "";
                    }
                }
                else if (rblState.SelectedValue == "")//全部
                {
                    if ((dr["TA_State"].ToString() == "0" || dr["TA_State"].ToString() == "5") && stid == dr["TA_ZDRID"].ToString())
                    {
                        hlkEdit.Visible = true;
                        lnkDelete.Visible = true;
                    }
                    else if (dr["TA_State"].ToString() == "1")
                    {
                        if (stid == dr["TA_SHRIDA"].ToString())
                        {
                            wordtext.Text = "审核";
                            hlkViewOrAudit.NavigateUrl = "OM_TravelApplyDetail.aspx?action=audit&key=" + lbTA_Code + "";
                        }
                    }
                    else if (dr["TA_State"].ToString() == "2")
                    {
                        if (stid == dr["TA_SHRIDB"].ToString())
                        {
                            wordtext.Text = "审核";
                            hlkViewOrAudit.NavigateUrl = "OM_TravelApplyDetail.aspx?action=audit&key=" + lbTA_Code + "";
                        }
                    }
                    else if (dr["TA_State"].ToString() == "3")
                    {
                        if (stid == dr["TA_SHRIDC"].ToString())
                        {
                            wordtext.Text = "审核";
                            hlkViewOrAudit.NavigateUrl = "OM_TravelApplyDetail.aspx?action=audit&key=" + lbTA_Code + "";
                        }
                    }
                    else if (dr["TA_State"].ToString() == "4")
                    {
                        if (stid == dr["TA_ZDRID"].ToString())
                        {
                            hlkSure.Visible = true;
                        }
                    }
                }
                if (listcode.Contains(lbTA_Code))
                {
                    hlkViewOrAudit.Visible = false;
                    hlkEdit.Visible = false;
                    hlkSure.Visible = false;
                    lnkDelete.Visible = false;
                    validcode.InnerText = "";
                }
                else
                    listcode.Add(lbTA_Code);
            }
        }
        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string context = ((LinkButton)sender).CommandArgument;
            List<string> list = new List<string>();
            string sqllist = "delete from OM_TravelApply where TA_Code='" + context.Trim() + "'";
            list.Add(sqllist);
            string sqldetail = "delete from OM_TravelApplyDetail where TA_Code='" + context.Trim() + "'";
            list.Add(sqldetail);
            try
            {
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('单号为" + context.Trim() + "的差旅申请单已删除！');location.href='OM_TravelApply.aspx';", true);
            }
            catch
            {
                Response.Write("<script>alert('数据操作出现问题，请联系管理员！')</script>");
            }
            //UCPaging1.CurrentPage = 1;
            //DataBind();
            //ControlVisible();
        }
    }
}
