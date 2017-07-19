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

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_My_Task : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
       
        protected void Page_Load(object sender, EventArgs e)
        { 
            
            if (!IsPostBack)
            { 
                zjy_bind();
                if (Session["UserDeptID"].ToString() == "03")
                {
                    drp_zjy.SelectedValue = Session["UserName"].ToString().Trim();
                }
                InitVar(); 
                bindGrid();
            }
           InitVar();
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }

        //初始化分布信息
        private void InitPager()
        {
            //质量任务分工，技术任务分工、技术员登记
            //状态----表示任务分工完事，但是未验收完成
            pager.TableName = "View_TCTSASSGN_QTSASSGN";
            pager.PrimaryKey = "QSA_ID";
            pager.ShowFields = "*";
            pager.OrderField = "QSA_ENGID";//按工程制号排序
            pager.StrWhere = CreateReConStr();
            pager.OrderType = 0;
            pager.PageSize = 10;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
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

         //   CheckUser(ControlFinder);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
                string sqlText;
                //序号，表名，生产制号带-
                //string sqlText = "SELECT count(*) FROM  " + GridView1.DataKeys[e.Row.RowIndex]["ES_TBNAME"].ToString() + " as a LEFT OUTER JOIN TBQM_QTSRESULT as b ON a.MS_ID=b.QSA_PH  WHERE MS_ENGID='" + GridView1.DataKeys[e.Row.RowIndex]["engid"].ToString() + "'AND (MS_STATE='5' and MS_CHSTATE!='1') AND MS_REQSTATE=1  AND (MS_INDEX LIKE '" + GridView1.DataKeys[e.Row.RowIndex]["QSA_ZONGXU"].ToString() + ".%' OR MS_INDEX='" + GridView1.DataKeys[e.Row.RowIndex]["QSA_ZONGXU"].ToString() + "')";
                //System.Data.SqlClient.SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                //if (dr.Read())
                //{
                //    if (dr[0].ToString() == "0")
                //    {
                //        (e.Row.FindControl("lb_MyTask") as Label).Visible = false;
                //    }
                //    else
                //    {
                //        (e.Row.FindControl("lb_MyTask") as Label).Visible = true;
                //        (e.Row.FindControl("lb_MyTask") as Label).Text = "(" + dr[0].ToString() + ")";
                //    }
                //}
                //dr.Close();

                //防腐的质检数量
                //TSA_ID，不带-
                //sqlText = "SELECT QSA_QCCHGER FROM TBQM_QTSASSGN WHERE QSA_ENGID='" + GridView1.DataKeys[e.Row.RowIndex]["TSA_ID"].ToString() + "'  ";
                //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                //if (dt.Rows.Count > 0)
                //{
                //    if (dt.Rows[0]["QSA_QCCHGER"].ToString() == Session["UserID"].ToString())
                //    {
                //        sqlText = "SELECT count(*) FROM  " + GridView1.DataKeys[e.Row.RowIndex]["ES_TBNAME"].ToString() + " as a LEFT OUTER JOIN TBQM_QTSRESULT as b ON a.MS_ID=b.QSA_PH  WHERE MS_ENGID='" + GridView1.DataKeys[e.Row.RowIndex]["engid"].ToString() + "'AND (MS_STATE='5' and MS_CHSTATE!='1') AND MS_REQSTATE=1 AND (TASK_FF='2' OR TASK_FF='4') AND TASK_BAOZ='0'AND (MS_INDEX LIKE '" + GridView1.DataKeys[e.Row.RowIndex]["QSA_ZONGXU"].ToString() + ".%' OR MS_INDEX='" + GridView1.DataKeys[e.Row.RowIndex]["QSA_ZONGXU"].ToString() + "')";
                //        System.Data.SqlClient.SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqlText);
                //        if (dr1.Read())
                //        {
                //            if (dr1[0].ToString() == "0")
                //            {
                //                (e.Row.FindControl("lb_MyTask") as Label).Visible = false;
                //            }
                //            else
                //            {
                //                (e.Row.FindControl("lb_MyTask") as Label).Visible = true;
                //                (e.Row.FindControl("lb_MyTask") as Label).Text = "(" + dr1[0].ToString() + ")";
                //            }
                //        }

                //        dr1.Close();
                //    }
                //}
                
            }
        }
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetTechAssignData();
        }

        protected void zjy_bind()
        {
            string sqltext = "select  QSA_QCCLERKNM from View_TCTSASSGN_QTSASSGN where QSA_QCCLERKNM is not null  group by QSA_QCCLERKNM ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            drp_zjy.DataSource = dt;
            drp_zjy.DataTextField = "QSA_QCCLERKNM";
            drp_zjy.DataValueField = "QSA_QCCLERKNM";
            drp_zjy.DataBind();
            drp_zjy.Items.Insert(0, new ListItem("请选择", "请选择"));
            drp_zjy.SelectedIndex = 0;
        }
      
        private string CreateReConStr()
        {
            ////需要改变
            string status = rblstatus.SelectedValue.ToString();
            string strWhere = string.Empty;

            strWhere = " QSA_STATE='" + status + "'";
            if (xmmc.Text != "")
            {
                strWhere += " AND TSA_PJNAME like '%" + xmmc.Text.Trim() + "%'";
            }
            if (sczh.Text != "")
            {
                strWhere += " AND TSA_ID like '%" + sczh.Text.Trim() + "%'";
            }
            if (drp_zjy.SelectedIndex == 0 | drp_zjy.SelectedIndex == -1)
            {
            }
            else
            {
                strWhere += "AND QSA_QCCLERKNM='" + drp_zjy.SelectedValue + "'";
            }
            return strWhere;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetTechAssignData();
        }

        private void ReGetTechAssignData()
        {
            bindGrid();
        }


     

        #region 无用

        protected void btn_all_task_Click(object sender, EventArgs e)
        {
            //isVisual = true;
            UCPaging1.CurrentPage = 1;
            RebindAllGrid();
        }
        private void RebindAllGrid()
        {
            InitPager();
            pager.StrWhere = CreateAllConStr();
            bindGrid();
        }
        private string CreateAllConStr()
        {
            //需要改变
            string status = rblstatus.SelectedItem.Value;
            string strWhere = " QSA_STATE='" + status + "'AND QSA_ISWX='0'";
            return strWhere;
        }


        #endregion

        protected void drp_zjy_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }
    }
}
