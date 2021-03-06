﻿using System;
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

        string depid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            depid = Session["UserDeptID"].ToString();
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
            ControlVisible();
        }

        private void databind()
        {
            pager.TableName = "OM_COMPUTERLIST LEFT JOIN (select Context, Type from OM_COMPUTERDETAIL)t ON OM_COMPUTERLIST.Context=t.Context";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "Id,Code,SQRId,SQR,SqDepId,SqDep,SqTime,GZRId,GZR,Note,SPLevel,SPRIDA,SPRNMA,SPRESULTA,SPTIMEA,SPNOTEA,SPRIDB,SPRNMB,SPRESULTB,SPTIMEB,SPNOTEB,OM_COMPUTERLIST.Context,State,Type";
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
            string strWhere = "1=1";

            
            //综合办公室只看"网络设备"，生产管理部门只看"其他",其他部门的只能看自己部门申请的情况
            if (depid == "02")   //综合办公室
            {
                strWhere += " and (Type='0' or" + " SqDepId='" + depid + "')";
            }
            else if (depid == "04")   //生产管理部门
            {
                strWhere += " and (Type='1' or" + " SqDepId='" + depid + "')";
            }
            else
            {
                strWhere += " and SqDepId='" + depid + "'";
            }

            //起始时间
            if (txtStart.Text.Trim() != "")
            {
                strWhere += " and SqTime>='" + txtStart.Text.Trim() + "'";
            }
            if (txtEnd.Text.Trim() != "")
            {
                strWhere += " and SqTime<='" + txtEnd.Text.Trim() + "'";
            }


            //未确认，已确认等
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
                strWhere += " and ((State ='1' and SPRIDA='" + Session["UserId"].ToString() + "') or ( State='2' and SPRIDB='" + Session["UserId"].ToString() + "')or (State='3' and '管理员' in (" + Session["UserGroup"].ToString() + ")))";
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
                HyperLink hlkEdit = item.FindControl("HyperLink2") as HyperLink;   //编辑 
                HyperLink hlkAudit = item.FindControl("HyperLink3") as HyperLink;  //审核
                LinkButton hlGZ = item.FindControl("hlGZ") as LinkButton;    //确认维修吗
                Label lbState = item.FindControl("lbState") as Label;     //确认维修按钮

                //未提交0 审核中1 已通过2 已驳回3 已确认5 我的审核任务4
                if (rblState.SelectedValue == "0" || rblState.SelectedValue == "3")
                {
                    hlkEdit.Visible = true;
                }
                else
                {
                    hlkEdit.Visible = false;
                }

                //我的审核任务 审核按钮可见
                if (rblState.SelectedValue == "4")
                {
                    if (lbState.Text.ToString() != "3")
                        hlkAudit.Visible = true;
                }
                else
                {
                    hlkAudit.Visible = false;
                }

                //已通过 确认维修按钮可见
                if (rblState.SelectedValue == "2" && (depid == "02" || depid == "04"))
                {
                    hlGZ.Visible = true;
                }
                else
                {
                    hlGZ.Visible = false;
                }

                //全部
                if (rblState.SelectedValue == "") 
                {
                    if (lbState.Text.ToString() == "3" && (depid == "02" || depid == "04"))
                    {
                        hlGZ.Visible = true;
                    }
                }
            }
        }

        //确认维修之后把维修人的信息添加上去
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
