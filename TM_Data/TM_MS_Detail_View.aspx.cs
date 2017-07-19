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
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_Detail_View : System.Web.UI.Page
    {
        string detail_id;
        string sqlText;
        string tablename ;
        string tabalnameOrg="TBPM_STRINFODQO";
        string mstable="TBPM_MKDETAIL";
        string fields;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            detail_id = Request.QueryString["detail_id"];
            if (!IsPostBack)
            {
                SignStatus(rblstate.SelectedValue);
            }
            InitParameter();
            InitVar();
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetTechDetailData();
        }

        //标记制作明细状态
        private void SignStatus(string type)
        {
            int a = 0;//未提交
            int b = 0;//待审核
            int c = 0;//审核中
            int d = 0;//驳回
            int e = 0;//审核通过
            int f = 0;//驳回已处理
            #region
            switch (type)
            {
                case "0":
                    sqlText = "select MS_STATE from TBPM_MSFORALLRVW where MS_ENGID='" + detail_id + "' ";
                    sqlText += "and  MS_ID like '%MS/%' ";
                    break;
                case "1":
                    sqlText = "select MS_STATE from TBPM_MSCHANGERVW where MS_ENGID='" + detail_id + "' ";
                    sqlText += "and MS_SUBMITID='" + Session["UserID"].ToString() + "' ";
                    break;

            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
           
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                if (dt.Rows[i]["MS_STATE"].ToString() == "0" || dt.Rows[i]["MS_STATE"].ToString() == "1")
                {
                    a++;
                }
                else if (dt.Rows[i]["MS_STATE"].ToString() == "2")
                {
                    b++;
                }
                else if (dt.Rows[i]["MS_STATE"].ToString() == "4" || dt.Rows[i]["MS_STATE"].ToString() == "6")
                {
                    c++;
                }
                else if (dt.Rows[i]["MS_STATE"].ToString() == "3" || dt.Rows[i]["MS_STATE"].ToString() == "5" || dt.Rows[i]["MS_STATE"].ToString() == "7")
                {
                    d++;
                }
                else if (dt.Rows[i]["MS_STATE"].ToString() == "8")
                {
                    e++;
                }
                else if (dt.Rows[i]["MS_STATE"].ToString() == "9")
                {
                    f++;
                }
            }
            rblstatus.Items.Clear();

            if (a != 0)
            {
                rblstatus.Items.Add(new ListItem("未提交" + "</label><label><font color=red>(" + a + ")</font>", "0,1"));
                rblstatus.SelectedIndex = 0;
            }
            else
            {
                rblstatus.Items.Add(new ListItem("未提交", "0,1"));
            }

            if (b != 0)
            {
                rblstatus.Items.Add(new ListItem("待审核" + "</label><label><font color=red>(" + b + ")</font>", "2"));
                if (a == 0)
                {
                    rblstatus.SelectedIndex = 1;
                }
            }
            else
            {
                rblstatus.Items.Add(new ListItem("待审核", "2"));
            }
            if (c != 0)
            {
                rblstatus.Items.Add(new ListItem("审核中" + "</label><label><font color=red>(" + c + ")</font>", "4,6"));
                if (a == 0 && b == 0)
                {
                    rblstatus.SelectedIndex = 2;
                }
            }
            else
            {
                rblstatus.Items.Add(new ListItem("审核中", "4,6"));
            }
            if (d != 0)
            {
                rblstatus.Items.Add(new ListItem("驳回" + "</label><label><font color=red>(" + d + ")</font>", "3,5,7"));
                if (a == 0 && b == 0 && c == 0)
                {
                    rblstatus.SelectedIndex = 3;
                }
            }
            else
            {
                rblstatus.Items.Add(new ListItem("驳回", "3,5,7"));
            }
            if (e != 0)
            {
                rblstatus.Items.Add(new ListItem("审核通过" + "</label><label><font color=red>(" + e + ")</font>", "8"));
                if (a == 0 && b == 0 && c == 0 && d == 0)
                {
                    rblstatus.SelectedIndex = 4;
                }
            }
            else 
            {
                rblstatus.Items.Add(new ListItem("审核通过", "8"));
            }

            if (f != 0)
            {
                rblstatus.Items.Add(new ListItem("驳回已处理" + "</label><label><font color=red>(" + f + ")</font>", "9"));
                if (a == 0 && b == 0 && c == 0 && d == 0 && e == 0)
                {
                    rblstatus.SelectedIndex = 5;
                }
            }
            else
            {
                rblstatus.Items.Add(new ListItem("驳回已处理", "9"));
            }
            if (rblstatus.SelectedIndex != -1)
            {
                InitParameter();
                InitVar();
                rblstatus_SelectedIndexChanged(null, null);
            }
            else
            {
                rblstatus.SelectedIndex = 0;
            }
            
            #endregion
        }

        //初始化参数
        private void InitParameter()
        {
            if (rblstate.SelectedItem.Text.Trim() == "正常")
            {
                tablename = "View_TM_MSFORALLRVW";
            }
            else
            {
                tablename = "VIEW_TM_MSCHANGERVW";
            }

            if (rblstatus.SelectedValue == "0,1")//MS_SUBMITID='" + Session["UserID"].ToString() + "' and
            {
                sqlText = "  MS_ENGID='" + detail_id + "' and MS_STATE in ('0','1') ";
            }
            else if (rblstatus.SelectedValue == "4,6")
            {
                sqlText = " MS_ENGID='" + detail_id + "' and MS_STATE in ('4','6') ";
            }
            else if (rblstatus.SelectedValue == "3,5,7")
            {
                sqlText = " MS_ENGID='" + detail_id + "' and MS_STATE in ('3','5','7') ";
            }
            else if (rblstatus.SelectedValue == "8")
            {
                sqlText = " MS_ENGID='" + detail_id + "' and MS_STATE='" + rblstatus.SelectedValue + "'";
            }
            else if (rblstatus.SelectedValue == "9")
            {
                sqlText = " MS_ENGID='" + detail_id + "' and MS_STATE='" + rblstatus.SelectedValue + "'";
            
            }
            else if(rblstatus.SelectedValue=="2")
            {
                sqlText = " MS_ENGID='" + detail_id + "' and MS_STATE='"+rblstatus.SelectedValue+"' ";

            }

            if (rblstate.SelectedItem.Text.Trim() == "正常")
            {
                sqlText += " and MS_ID like '%MS/%'";
            }

            fields = " MS_ID,MS_PJNAME,MS_ENGNAME,MS_SUBMITTM,MS_ADATE,MS_STATE,MS_ID as MS_NO,MS_MAP";
        }

        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = tablename;
            pager.PrimaryKey = "MS_ID";
            pager.ShowFields = fields;
            pager.OrderField = ddlSort.SelectedValue;
            pager.StrWhere = sqlText;
            pager.OrderType = Convert.ToInt16(ddlSortOrder.SelectedValue);//按任务名称升序排列
            pager.PageSize = 20;
        }

        //初始化信息（给页面控件赋值）

        private void InitInfo()
        {
            //绑定数据
            GetTechDetailData();
        }

        protected void GetTechDetailData()
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
            GetTechDetailData();
            if (GridView1.Rows.Count > 0 && rblstatus.SelectedIndex == 3)
            {
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
            }
            else if (GridView1.Rows.Count > 0 && rblstatus.SelectedIndex == 0)
            {
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
            }
            else
            {
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
            }
        }

        protected void rblstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTechDetailData();
            SignStatus(rblstate.SelectedValue);
            if (GridView1.Rows.Count > 0 && rblstatus.SelectedIndex == 3)
            {
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
            }
            else if (GridView1.Rows.Count > 0 && rblstatus.SelectedIndex == 0)
            {
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
            }
            else
            {
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
            }
        }



        /// <summary>
        /// 取消或作废制作明细（取消：未提交；作废：驳回后处理）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hlDelete_OnClick(object sender, EventArgs e)
        {
            string  lotnum= ((LinkButton)sender).CommandArgument;
            string  cmd= ((LinkButton)sender).CommandName;
            string taskid=lotnum.Split('.')[0];
            List<string> list_sql = new List<string>();
            if (cmd == "del")//作废
            {
                if (lotnum.Contains("MS/"))//正常作废
                {
                    list_sql.Add("update TBPM_MSFORALLRVW set MS_STATE='9' WHERE MS_ID='" + lotnum + "'");
                }
                else if(lotnum.Contains("MSBG/"))//变更作废
                {
                    list_sql.Add("update TBPM_MSCHANGERVW set MS_STATE='9' WHERE MS_ID='" + lotnum + "'");
                }
            }
            else if (cmd == "cancelMS")//未提交取消(注意SQL语句顺序)
            {
               
                list_sql.Add("exec [PRO_TM_CancelMSPlan] '"+lotnum+"','"+taskid+"','"+tabalnameOrg+"','"+mstable+"'");
                ///已由上述存储过程替换
                //////////////////if (lotnum.Contains(" MS/"))//正常取消
                //////////////////{
                //////////////////    list_sql.Add("update " + tabalnameOrg + " set BM_MSSTATE='0',BM_MSREVIEW='0' where BM_ENGID='" + taskid + "'  and BM_MSSTATE='1' and BM_MSSTATUS='0' and dbo.f_SubBJ(BM_XUHAO,'.') in (select distinct dbo.f_SubBJ(MS_NEWINDEX,'.') as MS_NEWINDEX from " + mstable + " where MS_PID='" + lotnum + "' and dbo.f_SubBJ(MS_NEWINDEX,'.') is not null)");

                //////////////////    list_sql.Add("delete from TBPM_MSFORALLRVW where MS_ID='" + lotnum + "'");
                //////////////////    list_sql.Add("delete from " + mstable + " where MS_PID='" + lotnum + "'");
                //////////////////}
                //////////////////else if (lotnum.Contains(" MSBG/"))//变更取消
                //////////////////{
                //////////////////    list_sql.Add("update " + tabalnameOrg + " set BM_MSSTATE='0',BM_MSREVIEW='0' where BM_ENGID='" + taskid + "' and BM_MSSTATE='1' and BM_MSSTATUS!='0' and dbo.f_SubBJ(BM_XUHAO,'.') in (select distinct dbo.f_SubBJ(MS_NEWINDEX,'.') as MS_NEWINDEX from TBPM_MSCHANGE where MS_PID='" + lotnum + "' and dbo.f_SubBJ(MS_NEWINDEX,'.') is not null)");
                    
                //////////////////    list_sql.Add("delete from TBPM_MSCHANGERVW where MS_ID='" + lotnum + "'");
                //////////////////    list_sql.Add("delete from TBPM_MSCHANGE where MS_PID='" + lotnum + "'");
                //////////////////    list_sql.Add("update " + mstable + " set MS_CHGPID='' where MS_CHGPID='" + lotnum + "'");
                //////////////////}
            }
            GridView1.DataSource = null;
            GridView1.DataBind();
            DBCallCommon.ExecuteTrans(list_sql);
            this.SignStatus(rblstate.SelectedValue);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
        }

        public string GetTaskID
        {
            get
            {
                return Request.QueryString["detail_id"];
            }
        }

        /// <summary>
        /// 勾选项导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportCheck_OnClick(object sender, EventArgs e)
        {
            string array_lotnum="";
            foreach (GridViewRow grow in GridView1.Rows)
            {
                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    array_lotnum += "'"+grow.Cells[1].Text.Trim()+"',";
                }
            }

            if (array_lotnum == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要导出的批号！！！');", true);
            }
            else if (array_lotnum.Split(',').Length>1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请逐条勾导出！！！');", true);
            }
            else
            {
                array_lotnum = array_lotnum.Substring(0, array_lotnum.Length - 1);
                ExportTMDataFromDB.ExportMSData(array_lotnum, array_lotnum.Split('.')[1]);
            }
        }
    }
}
