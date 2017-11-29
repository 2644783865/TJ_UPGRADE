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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchange_new : System.Web.UI.Page
    {
        string name = "";
        string userid = "";
        string position = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            name = Session["UserName"].ToString();
            userid = Session["UserID"].ToString();
            position = Session["POSITION"].ToString();
            if (!IsPostBack)
            {
                //初始化 
                ViewState["StrWhere"] = "(BG_PTC is not null and BG_PTC!='' and BG_STATE='0' and BG_NAMEID='" + userid + "') or ";

                //审核人A
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDA='" + userid + "' and BG_SHYJA IS NULL) or ";

                //审核人B
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDB='" + userid + "' and BG_SHYJB IS NULL and BG_SHYJA IS NOT NULL) or ";

                //审核人C
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDC='" + userid + "' and BG_SHYJC IS NULL and BG_SHYJB IS NOT NULL ) or ";

                //审核人D
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDD='" + userid + "' and BG_SHYJD IS NULL and BG_SHYJC IS NOT NULL )";

                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
                for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
                {
                    if (i == rptProNumCost.Items.Count - 2)
                    {
                        for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                        {
                            Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                            Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("BG_PH");
                            if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                            {
                                lbjsd1.Visible = false;
                            }
                        }
                    }
                    Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                    Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("BG_PH");
                    if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                    {
                        lbjsdh2.Visible = false;
                    }
                }


                //备注变更提醒
                string depid = Session["UserDeptID"].ToString();
                string sqltextchangenote = "";
                if (depid == "03")
                {
                    sqltextchangenote = "select * from TBPC_changebeizhu where changestate='2'";
                    DataTable dtchangenote = DBCallCommon.GetDTUsingSqlText(sqltextchangenote);
                    if (dtchangenote.Rows.Count > 0)
                    {
                        Label5.Text = "(" + dtchangenote.Rows.Count.ToString().Trim() + ")";
                    }
                }
                else if (depid == "05")
                {
                    sqltextchangenote = "select * from TBPC_changebeizhu where changestate='0' or changestate='' or changestate is null";
                    DataTable dtchangenote = DBCallCommon.GetDTUsingSqlText(sqltextchangenote);
                    if (dtchangenote.Rows.Count > 0)
                    {
                        Label5.Text = "(" + dtchangenote.Rows.Count.ToString().Trim() + ")";
                    }
                }
                else
                {
                    Label5.Visible = false;
                }
            }

            if (IsPostBack)
            {
                this.InitVar();
            }

        }

        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "(select * from ((select * from View_TBPC_PLAN_PLACE)a left join (select * from TBPC_BG)b on a.Aptcode=b.BG_PTC))c";
            pager_org.PrimaryKey = "BG_PH,BG_PTC";
            pager_org.ShowFields = "BG_PH,Aptcode,PUR_MASHAPE,BG_NAME,BG_DATE,BG_NUM,BG_FZNUM,RESULT,case when BG_STATE='0' then '初始化' when BG_STATE='1' then '已提交' when BG_STATE>='6' then '已驳回' when (BG_STATE='5'and RESULT='已执行') then '已通过' when (BG_STATE='4' and RESULT='已执行') then '已通过'  else '审核中' end as shzt,BG_NOTE";
            pager_org.OrderField = "BG_PH";
            pager_org.StrWhere = ViewState["StrWhere"].ToString();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 30;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("BG_PH");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("BG_PH");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                }
            }
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

        }
        #endregion



        protected void drp_state_SelectedIndexChanged(object sender,EventArgs e)
        {
            string condition = "BG_PTC is not null and BG_PTC!=''";

            //我的任务
            if (radio_mytask.Checked)
            {
                string name = Session["UserName"].ToString();
                string userid = Session["UserID"].ToString();

                //初始化 
                ViewState["StrWhere"] = "(BG_PTC is not null and BG_PTC!='' and BG_STATE='0' and BG_NAMEID='" + userid + "') or ";

                //审核人A
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDA='" + userid + "' and BG_SHYJA IS NULL) or " ;

                //审核人B
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDB='" + userid + "' and BG_SHYJB IS NULL and BG_SHYJA IS NOT NULL) or ";

                //审核人C
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDC='" + userid + "' and BG_SHYJC IS NULL and BG_SHYJB IS NOT NULL ) or ";

                //审核人D
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDD='" + userid + "' and BG_SHYJD IS NULL and BG_SHYJC IS NOT NULL )";

                condition = ViewState["StrWhere"].ToString();
            }

            if (drp_state.SelectedIndex != 0)
            {
                condition += " and (BG_STATE='" + drp_state.SelectedValue.ToString().Trim() + "' or RESULT='" + drp_state.SelectedValue.ToString().Trim() + "')";
            }
            if (tbbgph.Text.ToString().Trim() != "")
            {
                condition += " and BG_PH like '%" + tbbgph.Text.ToString().Trim() + "%'";
            }
            if (tbptc.Text.ToString().Trim() != "")
            {
                condition += " and BG_PTC like '%" + tbptc.Text.ToString().Trim() + "%'";
            }

            ViewState["StrWhere"] = condition;
            this.InitVar();
            this.bindGrid();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("BG_PH");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("BG_PH");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                }
            }
        }

        protected void btncx_click(object sender, EventArgs e)
        {
            string condition1 = "BG_PTC is not null and BG_PTC!=''";
            if (radio_mytask.Checked)
            {
                string name = Session["UserName"].ToString();
                string userid = Session["UserID"].ToString();
                string position = Session["POSITION"].ToString();

                //初始化 
                ViewState["StrWhere"] = "(BG_PTC is not null and BG_PTC!='' and BG_STATE='0' and BG_NAMEID='" + userid + "') or ";

                //审核人A
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDA='" + userid + "' and BG_SHYJA IS NULL) or ";

                //审核人B
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDB='" + userid + "' and BG_SHYJB IS NULL and BG_SHYJA IS NOT NULL) or ";

                //审核人C
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDC='" + userid + "' and BG_SHYJC IS NULL and BG_SHYJB IS NOT NULL ) or ";

                //审核人D
                ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDD='" + userid + "' and BG_SHYJD IS NULL and BG_SHYJC IS NOT NULL )";

                condition1 = ViewState["StrWhere"].ToString();
            }
            if (tbbgph.Text.ToString().Trim() != "")
            {
                condition1 += " and BG_PH like '%" + tbbgph.Text.ToString().Trim() + "%'";
            }
            if (tbptc.Text.ToString().Trim() != "")
            {
                condition1 += " and BG_PTC like '%" + tbptc.Text.ToString().Trim() + "%'";
            }
            if (drp_state.SelectedIndex != 0)
            {
                condition1 += " and (BG_STATE='" + drp_state.SelectedValue.ToString().Trim() + "' or RESULT='" + drp_state.SelectedValue.ToString().Trim() + "')";
            }

            ViewState["StrWhere"] = condition1;
            this.InitVar();
            this.bindGrid();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("BG_PH");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("BG_PH");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                }
            }
        }


        protected void radio_all_CheckedChanged(object sender, EventArgs e)
        {
            string condition2 = "BG_PTC is not null and BG_PTC!=''";
            if (tbbgph.Text.ToString().Trim() != "")
            {
                condition2 += " and BG_PH like '%" + tbbgph.Text.ToString().Trim() + "%'";
            }
            if (tbptc.Text.ToString().Trim() != "")
            {
                condition2 += " and BG_PTC like '%" + tbptc.Text.ToString().Trim() + "%'";
            }
            if (drp_state.SelectedIndex != 0)
            {
                condition2 += " and (BG_STATE='" + drp_state.SelectedValue.ToString().Trim() + "' or RESULT='" + drp_state.SelectedValue.ToString().Trim() + "')";
            }

            ViewState["StrWhere"] = condition2;
            this.InitVar();
            this.bindGrid();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("BG_PH");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("BG_PH");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                }
            }
        }


        protected void radio_mytask_CheckedChanged(object sender, EventArgs e)
        {
            string condition3 = "";
            string name = Session["UserName"].ToString();
            string position = Session["POSITION"].ToString();
            string userid = Session["UserID"].ToString();

            //初始化 
            ViewState["StrWhere"] = "(BG_PTC is not null and BG_PTC!='' and BG_STATE='0' and BG_NAMEID='" + userid + "') or ";

            //审核人A
            ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDA='" + userid + "' and BG_SHYJA IS NULL) or ";

            //审核人B
            ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDB='" + userid + "' and BG_SHYJB IS NULL and BG_SHYJA IS NOT NULL) or ";

            //审核人C
            ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDC='" + userid + "' and BG_SHYJC IS NULL and BG_SHYJB IS NOT NULL ) or ";

            //审核人D
            ViewState["StrWhere"] += "(BG_PTC is not null and BG_PTC!='' and BG_SHRIDD='" + userid + "' and BG_SHYJD IS NULL and BG_SHYJC IS NOT NULL )";

            condition3 = ViewState["StrWhere"].ToString();
            if (tbbgph.Text.ToString().Trim() != "")
            {
                condition3 += " and BG_PH like '%" + tbbgph.Text.ToString().Trim() + "%'";
            }
            if (tbptc.Text.ToString().Trim() != "")
            {
                condition3 += " and BG_PTC like '%" + tbptc.Text.ToString().Trim() + "%'";
            }
            if (drp_state.SelectedIndex != 0)
            {
                condition3 += " and (BG_STATE='" + drp_state.SelectedValue.ToString().Trim() + "' or RESULT='" + drp_state.SelectedValue.ToString().Trim() + "')";
            }

            ViewState["StrWhere"] = condition3;
            this.InitVar();
            this.bindGrid();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("BG_PH");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("BG_PH");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("BG_PH");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                }
            }
        }
    }
}
