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
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_Management : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqltext;
        public string tablename
        {
            get
            {
                string str;
                if (rblstate.SelectedItem.Text.Trim() == "正常")
                {
                    str = "View_TM_MSFORALLRVW";
                }
                else
                {
                    str = "View_TM_MSCHANGERVW";
                }
                return str;
            }
            set { tablename = value; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.DataPJname();
                this.DataENGname();
                InitVar();
                GetManutAssignData(); //数据绑定
            }
            if (rblproplan.SelectedValue != "1")
            {

                GridView1.Columns[1].Visible = false;
                GridView1.Columns[2].Visible = false;
            }
            else
            {
                GridView1.Columns[1].Visible = true;
                GridView1.Columns[2].Visible = true;
            }
            InitVar();
            CheckUser(ControlFinder);
        }
        private void DataPJname()  //绑定项目
        {
            sqltext = "select distinct MS_PJID from " + tablename + "  where MS_STATE='8'order by MS_PJID";
            string DataText = "MS_PJID";
            string DataValue = "MS_PJID";
            DBCallCommon.BindDdl(ddlpjname, sqltext, DataText, DataValue);
        }
        private void DataENGname()
        {
            sqltext = "select distinct MS_ENGNAME,MS_ENGID from " + tablename + "";
            sqltext += " where MS_PJID='" + ddlpjname.SelectedValue + "' and MS_STATE='8'";
            string DataText = "MS_ENGNAME";
            string DataValue = "MS_ENGID";
            DBCallCommon.BindDdl(ddlengname, sqltext, DataText, DataValue);
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetManutAssignData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string mspid = e.Row.Cells[3].Text;
                e.Row.Attributes["style"] = "Cursor:hand";
                string sqltext1 = "select  count(MS_wxtype)as num  from View_TM_WXDetail WHERE MS_wxtype='成品外协' and MS_PID='" + mspid + "'";
                string sqltext2 = "select  count(MS_wxtype)as num  from View_TM_WXDetail WHERE MS_wxtype='工序外协' and MS_PID='" + mspid + "'";
                e.Row.Cells[1].Text = DBCallCommon.GetDTUsingSqlText(sqltext1).Rows[0]["num"].ToString();
                if (e.Row.Cells[1].Text != "0")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.LightBlue;
                }
                e.Row.Cells[2].Text = DBCallCommon.GetDTUsingSqlText(sqltext2).Rows[0]["num"].ToString();
                if (e.Row.Cells[2].Text != "0")
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.LightGreen;
                }
            }

        }
        //初始化分页信息
        private void InitPager()
        {
            string sqltxt = "MS_STATE='8' and MS_WAIXIE='" + rblproplan.SelectedValue.ToString() + "'";
            if (ddlpjname.SelectedValue != "-请选择-")
            {
                sqltxt += " and MS_PJID='" + ddlpjname.SelectedValue + "' ";
            }
            if (ddlengname.SelectedValue != "-请选择-")
            {
                sqltxt += " and MS_ENGID='" + ddlengname.SelectedValue + "' ";
            }
            if (cb_myjob.Checked == true)
            {
                sqltxt += "and MTA_DUY  like '%" + Session["UserName"] + "%'";
            }
            if (txt_ph.Text.ToString() != "")
            {
                sqltxt += "and MS_ID  like '%" + txt_ph.Text.Trim().ToString() + "%'";
            }
            if (txt_contr.Text.ToString() != "")
            {
                sqltxt += "and MS_PJID  like '%" + txt_contr.Text.Trim().ToString() + "%'";
            }
            //pager.TableName = "View_TM_MSFORALLRVW";//制作明细
            //pager.TableName = "View_TM_MSFORALLRVW as A left join TBMP_MANUTSASSGN as B on A.MS_ENGID=B.MTA_ID";
            pager.TableName = "" + tablename + " as A left join TBMP_MANUTSASSGN as B on A.MS_ENGID=B.MTA_ID";
            pager.PrimaryKey = "A.MS_ID";
            pager.ShowFields = "A.MS_ID,A.MS_ENGID,A.MS_PJID,A.MS_PJNAME,A.MS_ENGNAME,A.MS_WAIXIE,A.MS_CHILDENGNAME,B.MTA_DUY";
            pager.OrderField = "MS_ID";
            pager.StrWhere = sqltxt;
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
        }
        protected void GetManutAssignData()
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
        //点击查询时重新邦定GridView，添加查询条件
        private void ReGetManutAssignData()
        {
            InitPager();
            GetManutAssignData();
        }
        protected void rblstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.DataPJname();
            this.DataENGname();
            InitVar();
            ReGetManutAssignData();
        }
        protected void rblproplan_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataPJname();
            this.DataENGname();
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
        protected void ddlpjname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataENGname();
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
        protected void ddlengname_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }
        protected void btn_query_click(object sender, EventArgs e)
        {
            this.DataPJname();
            this.DataENGname();
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();

        }
        protected void cb_myjob_OnCheckedChanged(object sender, EventArgs e)
        {
            this.DataPJname();
            this.DataENGname();
            UCPaging1.CurrentPage = 1;
            ReGetManutAssignData();
        }

        protected void btn_nowx_OnClick(object sender, EventArgs e)
        {
            int i = 0;
            string msid = "";
            string sqltxt = "";
            List<string> list = new List<string>();
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox cb_1 = (CheckBox)gvr.FindControl("check_index");
                if (cb_1 != null)
                {
                    if (cb_1.Checked)
                    {
                        i++;
                        // msid = ((Label)gvr.FindControl("lblPID")).Text.ToString();
                        msid = gvr.Cells[3].Text.ToString();
                        sqltext = "select * from " + tablename + " where MS_ID='" + msid + "' and MS_STATE='8' and MS_WAIXIE='1'";
                        if (DBCallCommon.GetDTUsingSqlText(sqltext).Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('包含有已经制定外协的批号！！！');", true);
                            break;
                        }
                        else
                        {
                            sqltxt = "update " + tablename + " set MS_WAIXIE='2' where MS_STATE='8' and MS_ID='" + msid + "'";// 2 代表不处理的外协
                            list.Add(sqltxt);
                        }
                    }
                }
            }
            if (i > 0)
            {
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('制定成功,共有" + i + "个批号不需要制定外协！！！');window.location.reload();", true);
                // Response.Redirect("~/PM_Data/PM_Xie_Management.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择不需要制定外协的批号！！！');", true);
            }

        }
    }
}
