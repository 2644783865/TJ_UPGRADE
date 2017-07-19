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
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarApplyAudit : BasicPage
    {
        string sqlText;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                //this.BindPjName();
                //this.BindTeccName();
                //ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
                //ddlEngName.SelectedIndex = 0;
                RBLBind();
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
        protected void GridView1_DATABOUND(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string code = ((Label)e.Row.FindControl("lblCode")).Text.Trim();
                e.Row.Attributes.Add("ondblclick", "javascript: window.showModalDialog('OM_CarApplyDetail.aspx?action=view&id=" + code + "','','scrollbars:yes;resizable:no;help:no;status:no;center:yes;dialogHeight:700px;dialogWidth:1200px;');");
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看详细信息！");

                string ss = "select STATE,FANKUI,FACHE,HUICHE,SJID,APPLYERID FROM View_TBOM_CARAPLLRVW WHERE CODE='" + code + "'";
                DataTable tt = DBCallCommon.GetDTUsingSqlText(ss);
                if (tt.Rows.Count > 0)
                {
                    if (tt.Rows[0]["STATE"].ToString() == "9")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Gray;
                    }
                    if (tt.Rows[0]["STATE"].ToString() == "3" || tt.Rows[0]["STATE"].ToString() == "5" || tt.Rows[0]["STATE"].ToString() == "7")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.DarkBlue;
                    }
                    if (tt.Rows[0]["STATE"].ToString() == "1" || tt.Rows[0]["STATE"].ToString() == "2" || tt.Rows[0]["STATE"].ToString() == "0")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                    }
                    if (tt.Rows[0]["STATE"].ToString() == "8")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }
        /// <summary>
        /// 动态添加审核状态项(待审核、审核中、通过、驳回、驳回已处理)
        /// </summary>
        private void RBLBind()
        {

            //Label state1 = (Label)GridView1.FindControl("state1");
            sqlText = "select count(*) from View_TBOM_CARAPLLRVW where ";
            sqlText += " (FIRSTMAN='" + Session["UserID"].ToString() + "' and STATE='0') or ";
            sqlText += " (SECONDMAN='" + Session["UserID"].ToString() + "' and STATE='1') or ";
            sqlText += " (THIRDMAN='" + Session["UserID"].ToString() + "' and STATE='2')";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    rblstatus.Items.Add(new ListItem("您的审核任务", "0"));
                }
                else
                {
                    rblstatus.Items.Add(new ListItem("您的审核任务" + "<font color=red>(" + dr[0].ToString() + ")</font>", "0"));
                    rblstatus.SelectedIndex = 0;
                }
            }
            dr.Close();
            sqlText = "select count(*) from View_TBOM_CARAPLLRVW where (FIRSTMAN='" + Session["UserID"].ToString() + "' and STATE in ('1','2'))";
            sqlText += " or (SECONDMAN='" + Session["UserID"].ToString() + "' and STATE='4')";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    rblstatus.Items.Add(new ListItem("审核中", "1"));
                }
                else
                {
                    rblstatus.Items.Add(new ListItem("审核中" + "<font color=red>(" + dr[0].ToString() + ")</font>", "1"));
                    if (rblstatus.SelectedValue != "0")
                    {
                        rblstatus.SelectedIndex = 1;
                    }
                }
                //for (int i = 0; i < GridView1.Rows.Count; i++)
                //{
                //    Label state1 = (Label)GridView1.Rows[i].FindControl("state1");
                //    state1.Text = "查看";
                //}

            }
            dr.Close();
            sqlText = "select count(*) from View_TBOM_CARAPLLRVW where (FIRSTMAN='" + Session["UserID"].ToString() + "'";
            sqlText += " or SECONDMAN='" + Session["UserID"].ToString() + "' or THIRDMAN='" + Session["UserID"].ToString() + "') ";
            sqlText += " and STATE in ('3','5','7')";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    rblstatus.Items.Add(new ListItem("驳回", "2"));
                }
                else
                {
                    rblstatus.Items.Add(new ListItem("驳回" + "<font color=red>(" + dr[0].ToString() + ")</font>", "2"));
                    if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1")
                    {
                        rblstatus.SelectedIndex = 2;
                    }
                }
            }
            dr.Close();
            rblstatus.Items.Add(new ListItem("通过", "3"));
            if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1" && rblstatus.SelectedValue != "2")
            {
                rblstatus.SelectedIndex = 0;
            }
        }
        private void InitPager()
        {
            pager.TableName = "View_TBOM_CARAPLLRVW";
            pager.PrimaryKey = "CODE";
            pager.ShowFields = "CODE,APPLYER,APPLYERID,SQTIME,FIRSTMAN,SECONDMAN,";
            pager.ShowFields += "THIRDMAN,FIRSTMANNM,THIRDMANNM,SECONDMANNM,FIRSTTIME,";
            pager.ShowFields += "SECONDTIME,THIRDTIME,STATE,TYPE,TYPEID,AUDITLEVEL,USETIME1,BOHUI,BOHUINOTE,DEPARTMENT,YDTIME,TIME1,TIME2,NUM,REASON,SFPLACE,DESTINATION";
            pager.OrderField = "CODE";
            pager.StrWhere = GetSqlWhere();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
        }
        private string GetSqlWhere()

        {
            string sqlwhere = "";
            if (rblstatus.SelectedValue == "0") //我的审核任务
            {
                sqlwhere = "(FIRSTMAN='" + Session["UserID"].ToString() + "' and STATE='0') or ";
                sqlwhere += " (SECONDMAN='" + Session["UserID"].ToString() + "' and STATE='1') or ";
                sqlwhere += " (THIRDMAN='" + Session["UserID"].ToString() + "' and STATE='2')";
            }
            else if (rblstatus.SelectedValue == "1") //审核中
            {
                sqlwhere = "(FIRSTMAN='" + Session["UserID"].ToString() + "' and STATE in ('2','1'))";
                sqlwhere += "  or (SECONDMAN='" + Session["UserID"].ToString() + "' and STATE='4')";
            }
            else if (rblstatus.SelectedValue == "2") //驳回
            {
                sqlwhere = "(FIRSTMAN='" + Session["UserID"].ToString() + "'or SECONDMAN='" + Session["UserID"].ToString() + "'";
                sqlwhere += " or THIRDMAN='" + Session["UserID"].ToString() + "') and STATE in ('3','5','7')";
            }
            else if (rblstatus.SelectedValue == "3")
            {
                sqlwhere = "(FIRSTMAN='" + Session["UserID"].ToString() + "'or SECONDMAN='" + Session["UserID"].ToString() + "'";
                sqlwhere += " or THIRDMAN='" + Session["UserID"].ToString() + "') and (STATE='8' or STATE='9')";
            }
            //else
            //{
            //    sqlwhere = "(FIRSTMAN='" + Session["UserID"].ToString() + "'or SECONDMAN='" + Session["UserID"].ToString() + "'";
            //    sqlwhere += " or THIRDMAN='" + Session["UserID"].ToString() + "') and STATE='9'";
            //}
            return sqlwhere;
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

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAuditData();
        }
    }
}
