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
    public partial class OM_Eat : System .Web.UI.Page
    {
        string sqlText;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                RBLBind();
                InitVar();
                GetBoundData();

            }
            //CheckUser(ControlFinder);
            show();
            
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string sqltext = "select count(*) as count from TBOM_EAT where SHENHEID='" + Session["UserID"].ToString() + "' AND TYPE='JL' AND STATE='6'";
            DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (DT.Rows.Count > 0)
            {
                string sqltxt = " select APPLYDATE FROM TBOM_EAT WHERE APPLYID= '" + Session["UserID"].ToString() + "' AND TYPE='SQ' AND STATE='6'";
                DataTable DTT = DBCallCommon.GetDTUsingSqlText(sqltxt);
                if (DTT.Rows.Count > 0)
                {
                    for (int i = 0; i < DTT.Rows.Count; i++)
                    {
                        if (date != DTT.Rows[0]["APPLYDATE"].ToString().Substring(0, 10))
                        {
                          
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您有未确认反馈的信息，请及时确认！！');", true);
                            hlAdd.Visible = false;
                        }
                    }
                }
            }
        }

        protected void show()
        {
            foreach (GridViewRow gr in gridview1.Rows)
            {
                string code = ((Label)gr.FindControl("lblCode")).Text.Trim();
                gr.Attributes.Add("ondblclick", "javascript: window.showModalDialog('OM_EatApplyDetail.aspx?action=view&id=" + code + "','','scrollbars:yes;resizable:no;help:no;status:no;center:yes;dialogHeight:700px;dialogWidth:1200px;');");
                gr.Attributes["style"] = "Cursor:hand";
                gr.Attributes.Add("title", "双击查看详细信息！");

            }

        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitVar();
            GetBoundData();
        }

        protected void gridview1_change(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string fache = ((Label)e.Row.FindControl("lblCode")).Text.ToString();
                string ss = "select STATE,SHENHEID,APPLYID FROM TBOM_EAT WHERE CODE='" + fache + "' and TYPE='SQ'";
                DataTable tt = DBCallCommon.GetDTUsingSqlText(ss);
                if (tt.Rows.Count > 0)
                {
                    if (tt.Rows[0]["STATE"].ToString() == "2")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                        if (Session["UserID"].ToString() == "260")
                        {
                            ((HtmlInputCheckBox)e.Row.FindControl("chk")).Disabled = false;
                        }
                    }
                    if (tt.Rows[0]["STATE"].ToString() == "4")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
                        if (Session["UserID"].ToString() == "260")
                        {
                            ((HyperLink)e.Row.FindControl("hlpfk")).Visible = true;
                        }
                    }
                    if (tt.Rows[0]["STATE"].ToString() == "1")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Gray;
                        if (Session["UserID"].ToString() == tt.Rows[0]["APPLYID"].ToString())
                        {
                            ((HyperLink)e.Row.FindControl("hplmod")).Visible = true;
                        }
                    }
                    if (tt.Rows[0]["STATE"].ToString() == "6")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Lime;
                        if (Session["UserID"].ToString() == tt.Rows[0]["APPLYID"].ToString())
                        {
                            ((HyperLink)e.Row.FindControl("hlpqr")).Visible = true;
                        }

                    }
                    if (tt.Rows[0]["STATE"].ToString() == "7")
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Gray;

                    }
                    if (tt.Rows[0]["STATE"].ToString() == "0")
                    {
                        if (Session["UserID"].ToString() == tt.Rows[0]["SHENHEID"].ToString())
                        {
                            ((Label)e.Row.FindControl("lab_audit_view")).Text = "审核";
                        }
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Orange;

                    }
                    if (tt.Rows[0]["STATE"].ToString() == "8")
                    {
                        ((HyperLink)e.Row.FindControl("hlpfkck")).Visible = true;
                    }
                    if (Session["UserID"].ToString() == "260")
                    {
                        ((HyperLink)e.Row.FindControl("hplmod")).Visible = true;
                    }
                }

                string fache2 = ((Label)e.Row.FindControl("lblCode")).Text.ToString();
                string ss2 = "select STATE,APPLYID,SHENHEID FROM TBOM_EAT WHERE CODE='" + fache + "' and TYPE='JL'";
                DataTable tt2 = DBCallCommon.GetDTUsingSqlText(ss2);
                if (tt2.Rows.Count > 0)
                {
                    if (Session["UserID"].ToString() == tt2.Rows[0]["APPLYID"].ToString())
                    {
                        if (tt2.Rows[0]["STATE"].ToString() == "6")
                        {
                            ((HyperLink)e.Row.FindControl("hlpfkck")).Visible = true;
                        }
                        if (tt2.Rows[0]["STATE"].ToString() == "7")
                        {
                            ((HyperLink)e.Row.FindControl("hlpfkxg")).Visible = true;
                        }
                    }
                }
            }
        }

        //protected void lnkDelete_OnClick(object sender, EventArgs e)
        //{
        //    string id = ((LinkButton)sender).CommandArgument.ToString();
        //    if (((LinkButton)sender).CommandName == "SHANCHU")
        //    {
        //        string sqltext = "delete from TBOM_CARAPPLY where CODE='" + id + "'";
        //        DBCallCommon.ExeSqlText(sqltext);
        //        string sql = "delete from TBOM_CARALLRVW where CODE='" + id + "'";
        //        DBCallCommon.ExeSqlText(sql);
        //        this.GetBoundData();
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
        //    }
        //}
        protected void fache_click(object sender, EventArgs e)
        {
            string jieshouonly = DateTime.Now.ToString();
            for (int i = 0; i < gridview1.Rows.Count; i++)
            {
                GridViewRow gr = gridview1.Rows[i];
                HtmlInputCheckBox cbk = (HtmlInputCheckBox)gr.FindControl("chk");
                if (cbk.Checked)
                {
                    Label lb = new Label();
                    lb = (Label)gr.FindControl("lblCode");
                    string ss = "update TBOM_EAT SET STATE='4' , JIESHOUONLY='" + jieshouonly + "' WHERE CODE='" + lb.Text.ToString() + "'";
                    DBCallCommon.ExeSqlText(ss);
                }
            }
            InitVar();
            GetBoundData();
            //string alert = "<script>window.showModalDialog('OM_EatFankui.aspx?action=jieshou&ID=" + jieshouonly + "','','DialogWidth=450px;DialogHeight=450px;pxstatus:no;center:yes;toolbar=no;menubar=no')</script>";

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", alert, false);
            //Response.Write("<script>opener.location.href=opener.location.href;</script>");
        }
        protected void link_change(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "back")
            {

                string alert = "<script>window.showModalDialog('OM_EatFankui.aspx?action=back&ID=" + id.ToString() + "','','DialogWidth=450px;DialogHeight=450px;pxstatus:no;center:yes;toolbar=no;menubar=no')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                Response.Write("<script>opener.location.href=opener.location.href;</script>");
            }
        }
        //protected void cancel_change(object sender, EventArgs e)
        //{
        //    string id = ((LinkButton)sender).CommandArgument.ToString();
        //    if (((LinkButton)sender).CommandName == "quxiao")
        //    {
        //        string alert = "<script>window.showModalDialog('OM_CarHUICHANG.aspx?action=cancel&ID=" + id.ToString() + "','','DialogWidth=450px;DialogHeight=300px;pxstatus:no;center:yes;toolbar=no;menubar=no')</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
        //        Response.Write("<script>opener.location.href=opener.location.href;</script>");
        //    }
        //}

        private string GetSqlWhere()
        {
            string sqlwhere = "ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE) ) ";
            if (rblstatus.SelectedValue == "0") //全部
            {
                sqlwhere += "  AND (APPLYID='" + Session["UserID"].ToString() + "' or 260='" + Session["UserID"].ToString() + "' or 3='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "')";
            }
            else if (rblstatus.SelectedValue == "1") //部门未审核
            {
                sqlwhere += "  AND STATE ='0' and (APPLYID='" + Session["UserID"].ToString() + "'or  3='" + Session["UserID"].ToString() + "' or 260='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "')";

            }
            else if (rblstatus.SelectedValue == "2") //食堂未接收
            {
                sqlwhere += "  AND STATE ='2'  and (APPLYID='" + Session["UserID"].ToString() + "' or  3='" + Session["UserID"].ToString() + "' or 260='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "')";
            }
            else if (rblstatus.SelectedValue == "3")
            {
                sqlwhere += "  AND STATE='4' and ( APPLYID='" + Session["UserID"].ToString() + "' or  3='" + Session["UserID"].ToString() + "' or 260='" + Session["UserID"].ToString() + "')";
            }
            else if (rblstatus.SelectedValue == "4")
            {
                sqlwhere += "  AND STATE='6' and ( APPLYID='" + Session["UserID"].ToString() + "' or  3='" + Session["UserID"].ToString() + "' or 260='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "')";
            }
            else if (rblstatus.SelectedValue == "5")
            {
                sqlwhere += "  AND STATE='8' and ( APPLYID='" + Session["UserID"].ToString() + "' or  3='" + Session["UserID"].ToString() + "' or 260='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "')";
            }
            else if (rblstatus.SelectedValue=="6")
            {
                if (Session["UserID"].ToString()=="260")
                {
                    sqlwhere += " AND ((STATE='2' or STATE='4') or ((SHENHEID='" + Session["UserID"].ToString() + "') and (STATE='6' or STATE='0')))";
                }
                else
                {
                    sqlwhere += " AND ((APPLYID='" + Session["UserID"].ToString() + "' and STATE='6') or ((SHENHEID='" + Session["UserID"].ToString() + "') and STATE='0'))";
                }
            }
            return sqlwhere;
        }

        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "TBOM_EAT";
            pager.PrimaryKey = "CODE";
            pager.ShowFields = "CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,";
            pager.ShowFields += "BUMENID,BUMENNAME,SHENHEID,SHENHENAME,STATE,SHITANGID,SHITANGNAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,GOODNAME,GOODGUIGE,GOODNUM,GOODDANWEI,GOODMONEY,ALLMONEY,YCTYPE";
            pager.OrderField = "CODE";
            pager.StrWhere = GetSqlWhere();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 50;

        }
        void Pager_PageChanged(int pageNumber)
        {
            InitVar();
            ReGetBoundData();
        }
        /// 动态添加审核状态项(全部、待审核、通过、驳回、食堂接收)
        /// </summary>
        private void RBLBind()
        {
            //蔡未疆，赵洪新登录看到全部信息
            sqlText = "select count(*) from TBOM_EAT WHERE ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE) ) AND "+
                "( APPLYID='" + Session["UserID"].ToString() + "' or  3='" + Session["UserID"].ToString() + "' or "+
                " 260='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "') ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                rblstatus.Items.Add(new ListItem("全部" + "<font color=red>(" + dr[0].ToString() + ")</font>", "0"));
                rblstatus.SelectedIndex = 0;
            }
            dr.Close();
            sqlText = "select count(*) from TBOM_EAT where ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE) ) AND "+
                "STATE ='0' and (APPLYID='" + Session["UserID"].ToString() + "' or   3='" + Session["UserID"].ToString() + "' "+
                "or 260='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "' )";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                rblstatus.Items.Add(new ListItem("部门未审核" + "<font color=red>(" + dr[0].ToString() + ")</font>", "1"));
                if (rblstatus.SelectedValue != "0")
                {
                    rblstatus.SelectedIndex = 1;
                }
            }
            dr.Close();
            sqlText = "select count(*) from TBOM_EAT where ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE) ) AND STATE ='2' "+
                "and (APPLYID='" + Session["UserID"].ToString() + "' or  3='" + Session["UserID"].ToString() + "' "+
                "or 260='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "')";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                rblstatus.Items.Add(new ListItem("食堂未审核" + "<font color=red>(" + dr[0].ToString() + ")</font>", "2"));
                if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1")
                {
                    rblstatus.SelectedIndex = 2;
                }
            }
            dr.Close();
            sqlText = "select count(*) from TBOM_EAT where ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE) ) AND STATE ='4' and (APPLYID='" + Session["UserID"].ToString() + "' or  3='" + Session["UserID"].ToString() + "' or 260='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "') ";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                rblstatus.Items.Add(new ListItem("食堂未反馈" + "<font color=red>(" + dr[0].ToString() + ")</font>", "3"));
                if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1" && rblstatus.SelectedValue != "2")
                {
                    rblstatus.SelectedIndex = 3;
                }
            }
            dr.Close();
            sqlText = "select count(*) from TBOM_EAT where ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE) ) AND STATE ='6' and (APPLYID='" + Session["UserID"].ToString() + "' or  3='" + Session["UserID"].ToString() + "' or 260='" + Session["UserID"].ToString() + "' or SHENHEID='" + Session["UserID"].ToString() + "')";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                rblstatus.Items.Add(new ListItem("部门反馈未确认" + "<font color=red>(" + dr[0].ToString() + ")</font>", "4"));
                if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1" && rblstatus.SelectedValue != "2" && rblstatus.SelectedValue != "3")
                {
                    rblstatus.SelectedIndex = 4;
                }
            }
            dr.Close();
            rblstatus.Items.Add(new ListItem("已完成", "5"));
            if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1" && rblstatus.SelectedValue != "2" && rblstatus.SelectedValue != "3" && rblstatus.SelectedValue != "4")
            {
                rblstatus.SelectedIndex = 5;
            }

            string  count_task ="0";
            int count_task_int = 0;
            //部门领导审核
            string sql_dep_audit = "select distinct SHENHEID from TBOM_EAT where STATE='0'";
            DataTable dt_dep_audit = DBCallCommon.GetDTUsingSqlText(sql_dep_audit);
            for (int i = 0; i < dt_dep_audit.Rows.Count;i++ )
            {
                //部门审核人登录，记录需要审核的数量
                if (Session["UserID"].ToString()==dt_dep_audit.Rows[i][0].ToString())
                {
                    string sqlText_dep_audit = "select count(*) from TBOM_EAT where SHENHEID='" + Session["UserID"].ToString() + "' and ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE)) and STATE='0'";
                    SqlDataReader dr_dep_audit = DBCallCommon.GetDRUsingSqlText(sqlText_dep_audit);
                    if (dr_dep_audit.Read())
                    {
                        count_task = dr_dep_audit[0].ToString();
                        count_task_int += Convert.ToInt32(count_task.ToString());
                    }
                    break;
                }
            }

            int eat_accept = 0;
            int eat_back = 0;
            //赵洪新
          if (Session["UserID"].ToString()=="260")
            {
                //食堂待接收
                string sqlText_eat_accept = "select count(*) from TBOM_EAT where ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE)) and STATE='2'";
                SqlDataReader dr_eat_accept = DBCallCommon.GetDRUsingSqlText(sqlText_eat_accept);
              //食堂未反馈
                string sqlText_eat_back = "select count(*) from TBOM_EAT where ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE)) and STATE='4'";
                SqlDataReader dr_eat_back = DBCallCommon.GetDRUsingSqlText(sqlText_eat_back);
              if (dr_eat_accept.Read())
              {
                  eat_accept=Convert.ToInt32(dr_eat_accept[0].ToString());
              }
              if (dr_eat_back.Read())
              {
                  eat_back=Convert.ToInt32(dr_eat_back[0].ToString());
              }
              count_task = (eat_accept + eat_back).ToString();
              count_task_int += Convert.ToInt32(count_task.ToString());
            }

            //用餐确认
          string sql_eat_confirm = "select distinct APPLYID from TBOM_EAT where STATE='6'";
          DataTable dt_eat_confirm = DBCallCommon.GetDTUsingSqlText(sql_eat_confirm);
          for (int i = 0; i < dt_eat_confirm.Rows.Count; i++)
          {
              if (Session["UserID"].ToString() == dt_eat_confirm.Rows[i][0].ToString())
              {
                  string sqlText_eat_confirm = "select count(*) from TBOM_EAT where APPLYID='" + Session["UserID"].ToString() + "' and ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE)) and STATE='6'";
                  SqlDataReader dr_eat_confirm = DBCallCommon.GetDRUsingSqlText(sqlText_eat_confirm);
                  if (dr_eat_confirm.Read())
                  {
                      count_task = dr_eat_confirm[0].ToString();
                      count_task_int += Convert.ToInt32(count_task.ToString());
                  }
                  break;
              }
          }

          rblstatus.Items.Add(new ListItem("我的任务" + "<font color=red>(" +Convert.ToString(count_task_int)+")</font>", "6"));
            if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1" && rblstatus.SelectedValue != "2" && rblstatus.SelectedValue != "3" && rblstatus.SelectedValue != "4"&&rblstatus.SelectedValue!="5")
            {
                rblstatus.SelectedIndex = 6;
            }
            //rblstatus.SelectedValue = "0";
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, gridview1, UCPaging1, NoDataPanel);
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion
    }
}
