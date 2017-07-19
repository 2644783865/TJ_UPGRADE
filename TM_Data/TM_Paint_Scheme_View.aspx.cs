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
    public partial class TM_Paint_Scheme_View : System.Web.UI.Page
    {
        string plan_id;
        string sqlText;
        string tablename;
        string fields;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            plan_id = Request.QueryString["paint"];
            ViewState["plan_id"] = plan_id;
            if (!IsPostBack)
            {
                SignStatus();
            }
            InitParameter();
            InitVar();
            if (!IsPostBack)
            {
                
                InitInfo();                
                //hlTo.NavigateUrl = "TM_Paint_Scheme_Create.aspx?add=" + plan_id;
            }
        }

        //标记油漆方案状态
        private void SignStatus()
        {
            int a = 0;//未提交
            int b = 0;//待审核
            int c = 0;//审核中
            int d = 0;//驳回
            int e = 0;//审核通过
            int f = 0;//驳回已处理
            #region
            sqlText = "select PS_STATE from TBPM_PAINTSCHEME where PS_ENGID='" + plan_id + "' ";
           // sqlText += "and PS_SUBMITID='" + Session["UserID"].ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            while (dr.Read())
            {
                if (dr[0].ToString() == "0" || dr[0].ToString() == "1")  //未提交
                {
                    a++;
                }
                else if (dr[0].ToString() == "2")    //提交(待审核)
                {
                    b++;
                }
                else if (dr[0].ToString() == "4" || dr[0].ToString() == "6") //审核中
                {
                    c++;
                }
                else if (dr[0].ToString() == "3" || dr[0].ToString() == "5" || dr[0].ToString() == "7")
                {
                    d++; //驳回
                }
                else if(dr[0].ToString()=="8")
                {
                    e++; //审核通过
                }
                else if (dr[0].ToString() == "9")
                {
                    f++; //驳回已处理
                }
            }
            dr.Close();
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
            #endregion
        }


        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetTechRequireData();
        }
        //初始化参数
        private void InitParameter()
        {
            plan_id = Request.QueryString["paint"];
            tablename = "VIEW_TM_PAINTSCHEME";
            ViewState["tablename"] = "TBPM_PAINTSCHEME";
            ViewState["tablename_list"] = "TBPM_PAINTSCHEMELIST";
            if (rblstatus.SelectedIndex==0)//未提交
            {
                sqlText = "PS_ENGID='" + plan_id + "' and PS_STATE in ('0','1')";
            }
            else if (rblstatus.SelectedIndex == 1)//待审核
            {
                sqlText = "PS_ENGID='" + plan_id + "' and PS_STATE='" + rblstatus.SelectedValue + "'";
            }
            else if (rblstatus.SelectedIndex == 2) //审核中
            {
                sqlText = "PS_ENGID='" + plan_id + "' and PS_STATE in ('4','6')";
            }
            else if (rblstatus.SelectedIndex == 3)//驳回
            {
                sqlText = "PS_ENGID='" + plan_id + "' and PS_STATE in ('3','5','7')";
            }
            else if(rblstatus.SelectedIndex==4) //审核通过
            {
                sqlText = "PS_ENGID='" + plan_id + "' and PS_STATE='" + rblstatus.SelectedValue + "'";
            }
            else if (rblstatus.SelectedIndex == 5) //驳回已处理
            {
                sqlText = "PS_ENGID='" + plan_id + "' and PS_STATE='" + rblstatus.SelectedValue + "'";
            }
            else
            {
                sqlText = "PS_ENGID='" + plan_id + "' ";

            }
            fields = " PS_ID,PS_ENGID,CM_PROJ,PS_PJID,TSA_ENGNAME,PS_SUBMITTM,PS_ADATE,PS_STATE,PS_ID+'.'+PS_STATE as PS_NO ";
        }
        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = tablename;
            pager.PrimaryKey = "PS_ID";
            pager.ShowFields = fields;
            pager.OrderField = "PS_ID";
            pager.StrWhere = sqlText;
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 10;
        }
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            //绑定数据
            GetTechRequireData();
        }

        protected void GetTechRequireData()
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
            GetChangeData();
        }
        /// <summary>
        /// 各审核状态下的操作
        /// </summary>
        private void GetChangeData()
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                string status = ((Label)gr.FindControl("lab_state")).Text;
                if (status == "未提交")
                {
                    GridView1.Columns[9].Visible=false;//编辑
                    GridView1.Columns[10].Visible = false;//作废
                    GridView1.Columns[11].Visible = true;//删除
                }
                else if (status == "待审核")
                {
                    GridView1.Columns[9].Visible = false;//编辑
                    GridView1.Columns[10].Visible = false;//作废
                    GridView1.Columns[11].Visible = false;//删除
                }
                else if (status == "审核中...")
                {
                    GridView1.Columns[9].Visible = false;//编辑
                    GridView1.Columns[10].Visible = false;//作废
                    GridView1.Columns[11].Visible = false;//删除
                }
                else if (status == "驳回")
                {
                    GridView1.Columns[9].Visible = true;//编辑
                    GridView1.Columns[10].Visible = true;//作废
                    GridView1.Columns[11].Visible = false;//删除
                }
                else if (status == "通过")
                {
                    GridView1.Columns[9].Visible = false;//编辑
                    GridView1.Columns[10].Visible = false;//作废
                    GridView1.Columns[11].Visible = false;//删除
                }
                else if (status == "已处理")
                {
                    GridView1.Columns[9].Visible = false;//编辑
                    GridView1.Columns[10].Visible = false;//作废
                    GridView1.Columns[11].Visible = false;//删除
                }
            }
        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTechRequireData();
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkbtnCancel_OnClick(object sender, EventArgs e)
        {
            LinkButton linkbtn = (LinkButton)sender;
            string cancelid = linkbtn.CommandArgument;
            string sqltext = "update " + ViewState["tablename"] + " set PS_STATE='9' where PS_ID='"+cancelid+"'";
            DBCallCommon.ExeSqlText(sqltext);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');document.location.href=document.location.href;", true);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkbtnDelete_OnClick(object sender, EventArgs e)
        {
            LinkButton linkbtn = (LinkButton)sender;
            string deleteid = linkbtn.CommandArgument;
            List<string> list_sql = new List<string>();
            string sqltext = "delete from " + ViewState["tablename"] + " where PS_ID='" + deleteid + "'";
            list_sql.Add(sqltext);
            sqltext = "delete from " + ViewState["tablename_list"] + " where PS_PID='"+deleteid+"'";
            list_sql.Add(sqltext);
            DBCallCommon.ExecuteTrans(list_sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');document.location.href=document.location.href;", true);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnNew_OnClick(object sender, EventArgs e)
        {
            if (this.UnSubmitLot())
            {
                lnkbtnNew.PostBackUrl = "TM_Paint_Scheme_Create.aspx?add=" + ViewState["plan_id"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您有未提交的涂装方案！！！\\r\\r提示:您可选择以下操作:\\r(1)、删除未提交涂装方案\\r(2)、在未提交涂装方案上修改\\r(3)、提交后新建');", true);
            }
        }

        private bool UnSubmitLot()
        {
            string sqltext = "select count(*) AS Num from " + ViewState["tablename"] + " where PS_ENGID='" + ViewState["plan_id"].ToString() + "'  and cast(PS_STATE as int)<2";
            int num = 0;
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                num =Convert.ToInt16(dr["Num"].ToString());
                dr.Close();

            }

            if (num > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetTaskID
        {
            get
            {
                return ViewState["plan_id"].ToString();
            }
        }
    }
}
