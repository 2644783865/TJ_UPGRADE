using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarApply : BasicPage
    {
        string sqlText;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            //rblstatus.SelectedValue = "0";
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            string ti = DateTime.Now.ToLocalTime().ToString();
            //string TIMENOW = DateTime.Now.ToString("yyyyMMddHHmmss");
            string sql = "select CODE from TBOM_CARAPPLY WHERE YDTIME<'" + ti + "'";
            List<string> list = new List<string>();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sqltxt = "update TBOM_CARAPPLY set FACHE='1' WHERE CODE IN('" + dt.Rows[i]["CODE"].ToString() + "')";
                    list.Add(sqltxt);
                }
            }
            DBCallCommon.ExecuteTrans(list);
            //string sqlL = "select * from (select *,row_number() over(partition by CARNUM order by CODE desc) as rownum from TBOM_CARAPPLY WHERE FACHE='1' and YDTIME<'" + ti + "' AND HUICHE='0')a where rownum<=1";
            //DataTable dtt = DBCallCommon.GetDTUsingSqlText(sqlL);
            //if (dtt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtt.Rows.Count; i++)
            //    {
            //        string sqltxt = "update TBOM_CARINFO set STATE='1',MUDIDI='" + dtt.Rows[i]["DESTINATION"].ToString() + "',NOWSIJI='" + dtt.Rows[i]["SJNAME"].ToString() + "' WHERE CARNUM= '" + dtt.Rows[i]["CARNUM"].ToString().Substring(0, 7) + "'";
            //        DBCallCommon.ExeSqlText(sqltxt);
            //    }
            //}


            if (!IsPostBack)
            {
                gridview1.Columns[0].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[0].ItemStyle.CssClass = "fixed";
                gridview1.Columns[1].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[1].ItemStyle.CssClass = "fixed";
                gridview1.Columns[2].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[2].ItemStyle.CssClass = "fixed";
                gridview1.Columns[3].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[3].ItemStyle.CssClass = "fixed";
                gridview1.Columns[4].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[4].ItemStyle.CssClass = "fixed";
                gridview1.Columns[5].HeaderStyle.CssClass = "fixed";
                gridview1.Columns[5].ItemStyle.CssClass = "fixed";
                ddl();
                RBLBind();
                InitVar();
                GetBoundData();
                list.Clear();
                string sqltext ;
                for (int i = 0; i < gridview1.Rows.Count; i++)
                {
                    GridViewRow gr = gridview1.Rows[i];
                    Label lb = new Label();
                    lb = (Label)gr.FindControl("lbid");
                    sqltext = "select distinct(FACHEONLY) as FACHEONLY , CARNUM FROM TBOM_CARAPPLY WHERE ID='" + lb.Text.ToString() + "' AND HUICHE='0' AND FACHE='1' and FACHEONLY!=''";
                    DataTable tt = DBCallCommon.GetDTUsingSqlText(sqltext);

                    if (tt.Rows.Count > 0)
                    {
                        string ts = "select MILEAGE from TBOM_CARINFO  where CARNUM='" + tt.Rows[0]["CARNUM"].ToString().Substring(0, 7) + "'";
                        DataTable nt = DBCallCommon.GetDTUsingSqlText(ts);
                        if (nt.Rows.Count > 0)
                        {
                            string ss = "update TBOM_CARAPPLY SET LICHENG1='" + nt.Rows[0]["MILEAGE"].ToString() + "' WHERE FACHEONLY='" + tt.Rows[0]["FACHEONLY"].ToString() + "'";
                            list.Add(ss);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(list);
            }
            CheckUser(ControlFinder);
          //  show();
        }
    
        private void ddl()
        {

            ddlcarnum.Items.Clear();
          //string sql = "select CARNUM+'//'+CARTYPE AS CARNUM from TBOM_CARINFO ";
            string sql = "select CARNUM+'//'+CARTYPE AS CARNUM from TBOM_CARINFO where [STATE]=0 and isdel='正常'";
            string datavalue = "CARNUM";
            DBCallCommon.BindDdl(ddlcarnum, sql, datavalue, datavalue);
        }
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitVar();
            GetBoundData();
        }
        protected void ddlcarnum_changed(object sender, EventArgs e)
        {
            //GetBoundData();
            if (ddlcarnum.SelectedIndex.ToString() != "0")
            {
                //ddlcarnum.Visible = false;
                fache.Visible = true;
            }
        }
        protected void gridview1_change(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.LimeGreen;
                e.Row.Cells[11].BackColor = System.Drawing.Color.LimeGreen;
                e.Row.Cells[12].BackColor = System.Drawing.Color.LimeGreen;
                e.Row.Cells[13].BackColor = System.Drawing.Color.LimeGreen;
                Label applyerID = (Label)e.Row.FindControl("lblApplyerID");
                string code = ((Label)e.Row.FindControl("lblCode")).Text.ToString();
                string state = ((HtmlInputHidden)e.Row.FindControl("hidState")).Value;
                string fache = ((HtmlInputHidden)e.Row.FindControl("hidFaChe")).Value;
                string huiche = ((HtmlInputHidden)e.Row.FindControl("hidHuiChe")).Value;
                string sjId = ((HtmlInputHidden)e.Row.FindControl("hidSJID")).Value;
                string applyerId = ((HtmlInputHidden)e.Row.FindControl("hidApplyerId")).Value;
              
                    if (state == "8")
                    {
                        //e.Row.Cells[1].BackColor = System.Drawing.Color.LightGreen;
                    }
                    if (state == "8" && Session["UserID"].ToString() == "50" && fache == "0")
                    {
                        //CheckBox check1 = e.Row.FindControl("CheckBox1");
                        //check1.Enabled = true;
                        ((HtmlInputCheckBox)e.Row.FindControl("chk")).Disabled = false;
                        //e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                        //((Label)e.Row.FindControl("xh")).BackColor = System.Drawing.Color.Green;
                    }
                    if (fache == "0")
                    {
                        ((Label)e.Row.FindControl("HUICHE")).Visible = false;
                    }
                    if ((state == "3" || state  == "5" || state  == "7") && applyerID.Text.Trim() == Session["UserID"].ToString())
                    {
                        ((HyperLink)e.Row.FindControl("hplmod")).Visible = true;
                        ((LinkButton)e.Row.FindControl("lnkDelete")).Visible = true;

                    }
                    if (huiche == "0" && fache== "1")
                    {
                        if (sjId == Session["UserID"].ToString() || Session["UserID"].ToString() == "50")
                        {
                            ((LinkButton)e.Row.FindControl("link")).Visible = true;
                        }
                        if (state== "9")
                        {
                            ((Label)e.Row.FindControl("HUICHE")).Visible = false;
                            ((LinkButton)e.Row.FindControl("link")).Visible = false;
                        }
                    }
                    if (Session["UserID"].ToString() == "50" && state  == "8" && fache  == "0")
                    {
                        ((LinkButton)e.Row.FindControl("quxiao")).Visible = true;


                    }
                    if (state  == "9")
                    {
                        e.Row.Cells[3].BackColor = System.Drawing.Color.Gray;
                    }
                    if (state  == "3" || state  == "5" || state  == "7")
                    {
                        e.Row.Cells[3].BackColor = System.Drawing.Color.DarkBlue;
                    }
                    if (state  == "1" || state  == "2" || state  == "0")
                    {
                        e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                    }
                
                if (Session["UserID"].ToString() == "50")
                {
                    ((LinkButton)e.Row.FindControl("lnkDelete")).Visible = true;
                }

              
                e.Row.Attributes.Add("ondblclick", "javascript: window.showModalDialog('OM_CarApplyDetail.aspx?action=view&id=" + code + "','','scrollbars:yes;resizable:no;help:no;status:no;center:yes;dialogHeight:700px;dialogWidth:1200px;');");
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看详细信息！");


            }
            
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "SHANCHU")
            {
                //2017.1.11添加，针对选择发车后删除的情况
                string sql_fa_car = "select CARNUM from  TBOM_CARAPPLY where CODE='" + id + "'";
                DataTable dt_fa_car = DBCallCommon.GetDTUsingSqlText(sql_fa_car);
                string carnum_exi = dt_fa_car.Rows[0]["CARNUM"].ToString();
                if (carnum_exi.Length > 0)
                {
                    string carnum = carnum_exi.Substring(0, 7);
                    string sql_car_all = "select * from TBOM_CARAPPLY where CARNUM like '" + carnum + "%' order by code desc";
                    DataTable dt_fa_car_all = DBCallCommon.GetDTUsingSqlText(sql_car_all);
                    if ((!string.IsNullOrEmpty(dt_fa_car_all.Rows[0]["SJID"].ToString())) && (dt_fa_car_all.Rows[0]["HUICHE"].ToString() == "0") && (dt_fa_car_all.Rows[0]["CODE"].ToString() == id))
                    {
                        string sqltext_info = "update  TBOM_CARINFO set STATE=0 where CARNUM='" + carnum + "'";
                        DBCallCommon.ExeSqlText(sqltext_info);
                    }
                }


                string sqltext = "delete from TBOM_CARAPPLY where CODE='" + id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                string sql = "delete from TBOM_CARALLRVW where CODE='" + id + "'";
                DBCallCommon.ExeSqlText(sql);
                InitVar();
                this.GetBoundData();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
            }
        }
        protected void fache_click(object sender, EventArgs e)
        {
            //string id = ((LinkButton)sender).CommandArgument.ToString();
            string facheonly = DateTime.Now.ToString();


            string carnum = ddlcarnum.SelectedValue.Trim();
            string carnum2 = carnum.Substring(0, 7);
            string carnum3 = carnum.Substring(0, 13);
            for (int i = 0; i < gridview1.Rows.Count; i++)
            {
                GridViewRow gr = gridview1.Rows[i];
                HtmlInputCheckBox cbk = (HtmlInputCheckBox)gr.FindControl("chk");
                if (cbk.Checked)
                {
                    Label lb = new Label();
                    lb = (Label)gr.FindControl("lbid");
                    //string sqltext = "update TBOM_CARINFO SET STATE='2' where CARNUM='" + carnum2 + "'";
                    //DBCallCommon.ExeSqlText(sqltext);
                    string sql = "select MILEAGE from TBOM_CARINFO  where CARNUM='" + carnum2 + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        string ss = "update TBOM_CARAPPLY SET CARNUM='" + carnum3 + "',LICHENG1='" + dt.Rows[0]["MILEAGE"].ToString() + "',FANKUI='1',FACHEONLY='" + facheonly + "' WHERE ID='" + lb.Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(ss);
                    }
                    //this.GetBoundData();
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:发车成功！！！');", true);
                }
            }
            string alert = "<script>window.showModalDialog('OM_CarHUICHANG.aspx?action=fache&ID=" + facheonly + "','','DialogWidth=450px;DialogHeight=450px;pxstatus:no;center:yes;toolbar=no;menubar=no')</script>";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "", alert, false);
            Response.Write("<script>opener.location.href=opener.location.href;</script>");
            //this.GetBoundData();
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:发车成功！！！');", true); 
        }
        protected void link_change(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "back")
            {

                string alert = "<script>window.showModalDialog('OM_CarHUICHANG.aspx?action=huiche&ID=" + id.ToString() + "','','DialogWidth=450px;DialogHeight=450px;pxstatus:no;center:yes;toolbar=no;menubar=no')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                Response.Write("<script>opener.location.href=opener.location.href;</script>");
            }
            //this.GetBoundData();
        }
        protected void cancel_change(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "quxiao")
            {
                string alert = "<script>window.showModalDialog('OM_CarHUICHANG.aspx?action=cancel&ID=" + id.ToString() + "','','DialogWidth=450px;DialogHeight=300px;pxstatus:no;center:yes;toolbar=no;menubar=no')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                Response.Write("<script>opener.location.href=opener.location.href;</script>");
            }
        }

        private string GetSqlWhere()
        {
            string sqlwhere = "";
            //rblstatus.SelectedValue = "0";
            if (rblstatus.SelectedValue == "0") //全部
            {
                sqlwhere = "APPLYERID='" + Session["UserID"].ToString() + "' or SJID='" + Session["UserID"].ToString() + "' or (50='" + Session["UserID"].ToString() + "' and STATE IN ('5','1','2','8')) or 3='" + Session["UserID"].ToString() + "'";
            }
            else if (rblstatus.SelectedValue == "1") //审核中
            {
                sqlwhere = "(STATE in ('0','1','2') and APPLYERID='" + Session["UserID"].ToString() + "') or  (STATE in ('1','2') and 50='" + Session["UserID"].ToString() + "') or (STATE in ('0','1','2') and 3='" + Session["UserID"].ToString() + "') or (STATE in ('0','1','2') and SJID='" + Session["UserID"].ToString() + "')";

            }
            else if (rblstatus.SelectedValue == "2") //驳回
            {
                sqlwhere = "(STATE in ('3','5','7') and APPLYERID='" + Session["UserID"].ToString() + "') or (STATE in ('5','7') and 50='" + Session["UserID"].ToString() + "') or (STATE in ('3','5','7') and 3='" + Session["UserID"].ToString() + "')  or (STATE in ('3','5','7') and SJID='" + Session["UserID"].ToString() + "')";
            }
            else if (rblstatus.SelectedValue == "3")
            {
                sqlwhere = " (STATE='8' and APPLYERID='" + Session["UserID"].ToString() + "') or (STATE='8' and 50='" + Session["UserID"].ToString() + "') or (STATE='8' and 3='" + Session["UserID"].ToString() + "'or (STATE='8' and SJID='" + Session["UserID"].ToString() + "'))";
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
            pager.TableName = "View_TBOM_CARAPLLRVW";
            pager.PrimaryKey = "CODE";
            pager.ShowFields = "CODE,APPLYER,APPLYERID,SQTIME,FIRSTMAN,SECONDMAN,PHONE,";
            pager.ShowFields += "THIRDMAN,FIRSTMANNM,THIRDMANNM,SECONDMANNM,FIRSTTIME,";
            pager.ShowFields += "SECONDTIME,THIRDTIME,STATE,TYPE,TYPEID,BOHUI,BOHUINOTE,DEPARTMENT,TIME1,TIME2,YDTIME,USETIME1,USETIME2,SJNAME,SJID,SJCALL,NUM,REASON,SFPLACE,DESTINATION,FACHE,HUICHE,ID,CARNUM";
            pager.OrderField = "CODE";
            pager.StrWhere = GetSqlWhere();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;

        }
        void Pager_PageChanged(int pageNumber)
        {
            InitVar();
            ReGetBoundData();
        }
        /// 动态添加审核状态项(待审核、审核中、通过、驳回、驳回已处理)
        /// </summary>
        private void RBLBind()
        {
            sqlText = "select count(*) from View_TBOM_CARAPLLRVW WHERE APPLYERID='" + Session["UserID"].ToString() + "' or ( 50='" + Session["UserID"].ToString() + "' and STATE IN ('5','1','2','8')) or  3='" + Session["UserID"].ToString() + "' or  SJID='" + Session["UserID"].ToString() + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                rblstatus.Items.Add(new ListItem("全部" + "<font color=red>(" + dr[0].ToString() + ")</font>", "0"));
                rblstatus.SelectedIndex = 0;
            }
            dr.Close();
            sqlText = "select count(*) from View_TBOM_CARAPLLRVW where (STATE in ('0','1','2') and APPLYERID='" + Session["UserID"].ToString() + "') or  (STATE in ('1','2') and 50='" + Session["UserID"].ToString() + "') or (STATE in ('0','1','2') and 3='" + Session["UserID"].ToString() + "') or (STATE in ('0','1','2') and SJID='" + Session["UserID"].ToString() + "')";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                rblstatus.Items.Add(new ListItem("审核中" + "<font color=red>(" + dr[0].ToString() + ")</font>", "1"));
                if (rblstatus.SelectedValue != "0")
                {
                    rblstatus.SelectedIndex = 1;
                }
            }
            dr.Close();
            sqlText = "select count(*) from View_TBOM_CARAPLLRVW where (STATE in ('3','5','7') and APPLYERID='" + Session["UserID"].ToString() + "') or (STATE in ('5','7') and 50='" + Session["UserID"].ToString() + "') or (STATE in ('3','5','7') and 3='" + Session["UserID"].ToString() + "') or (STATE in ('3','5','7') and SJID='" + Session["UserID"].ToString() + "')";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                rblstatus.Items.Add(new ListItem("驳回" + "<font color=red>(" + dr[0].ToString() + ")</font>", "2"));
                if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1")
                {
                    rblstatus.SelectedIndex = 2;
                }
            }
            dr.Close();
            rblstatus.Items.Add(new ListItem("通过", "3"));
            if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1" && rblstatus.SelectedValue != "2")
            {
                rblstatus.SelectedIndex = 3;
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
