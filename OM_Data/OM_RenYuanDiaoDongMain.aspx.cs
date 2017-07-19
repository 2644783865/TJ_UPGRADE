using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_RenYuanDiaoDongMain :BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                InitVar();
                GetAuditData();
            }
            CheckUser(ControlFinder);
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetAuditData();
        }

        private void InitPager()
        {
            pager.TableName = "OM_RenYuanDiaoDong";
            pager.PrimaryKey = "MOVE_CODE";
            pager.ShowFields = "MOVE_CODE,MOVE_TYPE, MOVE_PERNAME, MOVE_INPART, MOVE_WORK, MOVE_OUTPART, MOVE_STARTTIME, MOVE_ENDTIME, MOVE_REASON,MOVE_STATE";

            pager.OrderField = "MOVE_CODE";
            pager.StrWhere = GetSqlWhere();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
        }

         private string GetSqlWhere()
         {
             string strwhere = " 1=1";
             string NAME = Session["UserName"].ToString();
             string sqltext = "select MOVE_AUTH_RATING, FIRST_PER,FIRST_SJ, FIRST_JG, SECOND_PER,SECOND_SJ, SECOND_JG, THIRD_PER, THIRD_SJ, THIRD_JG, FOURTH_PER, FOURTH_SJ, FOURTH_JG, FIFTH_PER, FIFTH_SJ, FIFTH_JG, SIXTH_PER,SIXTH_SJ, SIXTH_JG, MOVE_STATE from OM_RenYuanDiaoDong";
             DataTable dt=DBCallCommon.GetDTUsingSqlText(sqltext);
             if (rblstatus.SelectedValue.ToString()=="0")//您的审核任务
             {
                 strwhere += " and ((FIRST_PER='" + NAME.ToString() + "' and MOVE_STATE='0' and MOVE_STATE<MOVE_AUTH_RATING) or ";
                 strwhere += " (SECOND_PER='" + NAME.ToString() + "' and MOVE_STATE='1' and MOVE_STATE<MOVE_AUTH_RATING) or ";
                 strwhere += " (THIRD_PER='" + NAME.ToString() + "' and MOVE_STATE='2' and MOVE_STATE<MOVE_AUTH_RATING) or ";
                 strwhere += " (FOURTH_PER='" + NAME.ToString() + "' and MOVE_STATE='3' and MOVE_STATE<MOVE_AUTH_RATING) or ";
                 strwhere += " (FIFTH_PER='" + NAME.ToString() + "' and MOVE_STATE='4' and MOVE_STATE<MOVE_AUTH_RATING) or ";
                 strwhere += " (SIXTH_PER='" + NAME.ToString() + "' and MOVE_STATE='5' and MOVE_STATE<MOVE_AUTH_RATING))";
             }
             if (rblstatus.SelectedValue.ToString() == "1")//审核中
             {
                 strwhere += " and MOVE_STATE<MOVE_AUTH_RATING";
             }
             if (rblstatus.SelectedValue.ToString() == "2")//驳回
             {
                 strwhere += " and MOVE_STATE='9'";
             }
             if (rblstatus.SelectedValue.ToString() == "3")
             {
                 strwhere += " and MOVE_STATE=MOVE_AUTH_RATING";
             }
             return strwhere;
         }

         private void GetAuditData()
         {
             pager.PageIndex = UCPaging1.CurrentPage;
             DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
             CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
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


         protected void rblstatus_selectindexchanged(object sender, EventArgs e)
        {
            InitVar();
            GetAuditData();
        }

         protected void GridView1_DATABOUND(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 string NAME = Session["UserName"].ToString();
                 string code = ((Label)e.Row.FindControl("lblCode")).Text.Trim();
                 string SQLTEXT = "select MOVE_AUTH_RATING,FIRST_PER,FIRST_JG, SECOND_PER,SECOND_JG, THIRD_PER,THIRD_JG, FOURTH_PER,FOURTH_JG, FIFTH_PER,FIFTH_JG, SIXTH_PER,SIXTH_JG, MOVE_STATE FROM OM_RenYuanDiaoDong WHERE MOVE_CODE='" + code + "'";
                 DataTable dt = DBCallCommon.GetDTUsingSqlText(SQLTEXT);
                 if (dt.Rows.Count>0)
                 {
                     //状态显示
                     double AUTH_RATING=Convert.ToDouble(dt.Rows[0]["MOVE_AUTH_RATING"].ToString());
                     double state=Convert.ToDouble(dt.Rows[0]["MOVE_STATE"].ToString());
                         if (AUTH_RATING==state)
                         {
                             ((Label)e.Row.FindControl("status")).Text = "通过";
                         }
                         else
                             if (AUTH_RATING>state)
                             {
                                 ((Label)e.Row.FindControl("status")).Text = "审核中";
                             }
                         else
                                 if (AUTH_RATING<state)
                                 {
                                     ((Label)e.Row.FindControl("status")).Text = "驳回";
                                 }

                     //审核查看显示
                         if (NAME != dt.Rows[0]["FIRST_PER"].ToString() && NAME != dt.Rows[0]["SECOND_PER"].ToString() && NAME != dt.Rows[0]["THIRD_PER"].ToString() && NAME != dt.Rows[0]["FOURTH_PER"].ToString() && NAME != dt.Rows[0]["FIFTH_PER"].ToString() && NAME != dt.Rows[0]["SIXTH_PER"].ToString())
                     {
                         ((HyperLink)e.Row.FindControl("hlTask1")).NavigateUrl = "OM_RenYuan_DiaoDong_authorize.aspx?action=view&CODE=" + code + "";
                         ((Label)e.Row.FindControl("state1")).Text = "查看";
                     }
                     else
                         if ((NAME == dt.Rows[0]["FIRST_PER"].ToString() && dt.Rows[0]["FIRST_JG"].ToString() != "")||
                             (NAME == dt.Rows[0]["SECOND_PER"].ToString() && dt.Rows[0]["SECOND_JG"].ToString() != "")||
                             (NAME == dt.Rows[0]["THIRD_PER"].ToString() && dt.Rows[0]["THIRD_JG"].ToString() != "")||
                             (NAME == dt.Rows[0]["FOURTH_PER"].ToString() && dt.Rows[0]["FOURTH_JG"].ToString() != "")||
                             (NAME == dt.Rows[0]["FIFTH_PER"].ToString() && dt.Rows[0]["FIFTH_JG"].ToString() != "")||
                             (NAME == dt.Rows[0]["SIXTH_PER"].ToString() && dt.Rows[0]["SIXTH_JG"].ToString() != ""))
                         {
                             ((HyperLink)e.Row.FindControl("hlTask1")).NavigateUrl = "OM_RenYuan_DiaoDong_authorize.aspx?action=view&CODE=" + code + "";
                                 ((Label)e.Row.FindControl("state1")).Text = "查看";
                         }
                     else
                             if ((NAME == dt.Rows[0]["FIRST_PER"].ToString() && dt.Rows[0]["FIRST_JG"].ToString() == "" && state == 0 && state < AUTH_RATING) ||
                             (NAME == dt.Rows[0]["SECOND_PER"].ToString() && dt.Rows[0]["SECOND_JG"].ToString() == ""&&state==1&&state<AUTH_RATING)||
                             (NAME == dt.Rows[0]["THIRD_PER"].ToString() && dt.Rows[0]["THIRD_JG"].ToString() == "" && state == 2 && state < AUTH_RATING) ||
                             (NAME == dt.Rows[0]["FOURTH_PER"].ToString() && dt.Rows[0]["FOURTH_JG"].ToString() == "" && state == 3 && state < AUTH_RATING) ||
                             (NAME == dt.Rows[0]["FIFTH_PER"].ToString() && dt.Rows[0]["FIFTH_JG"].ToString() == "" && state == 4 && state < AUTH_RATING) ||
                             (NAME == dt.Rows[0]["SIXTH_PER"].ToString() && dt.Rows[0]["SIXTH_JG"].ToString() == "" && state == 5 && state < AUTH_RATING))
                             {
                                 ((HyperLink)e.Row.FindControl("hlTask1")).NavigateUrl = "OM_RenYuan_DiaoDong_authorize.aspx?action=audit&CODE=" + code + "";
                                 ((Label)e.Row.FindControl("state1")).Text = "审核";
                             }
                     else
                             {
                                 ((HyperLink)e.Row.FindControl("hlTask1")).NavigateUrl = "OM_RenYuan_DiaoDong_authorize.aspx?action=view&CODE=" + code + "";
                                 ((Label)e.Row.FindControl("state1")).Text = "查看";
                             }
                 }
             }
         }
    }
}
