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
    public partial class TM_MP_Require_View : System.Web.UI.Page
    {
        string plan_id;
        string sqlText;
        string tablename = "TBPM_STRINFODQO";
        string fields;
        //string engtype;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            plan_id = Request.QueryString["plan_id"];
            InitParameter();
            InitVar();
            if (!IsPostBack)
            {
                SignStatus(rblstate.SelectedValue);
            }
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

        //标记材料状态
        private void SignStatus(string type)
        {
            int a=0;//未提交
            int b=0;//待审核
            int c=0;//审核中
            int d=0;//驳回
            int e=0;//审核通过
            int f = 0;//驳回已处理
            #region
            switch (type)
            {
                case "0":
                    sqlText = "select MP_STATE from View_TM_MPFORALLRVW where MP_ENGID='" + plan_id + "' ";
                    sqlText += " AND MP_ID LIKE '%MP/%'";
                    break;
                case "1":
                    sqlText = "select MP_STATE from View_TM_MPCHANGERVW where MP_ENGID='" + plan_id + "' ";
                    ////////////////sqlText += "and MP_SUBMITID='" + Session["UserID"].ToString() + "' ";
                    break;
                default: break;
            }
            rblstatus.Items.Clear();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                if (dt.Rows[i]["MP_STATE"].ToString() == "0" || dt.Rows[i]["MP_STATE"].ToString() == "1")
                {
                    a++;
                }
                else if (dt.Rows[i]["MP_STATE"].ToString() == "2")
                {
                    b++;
                }
                else if (dt.Rows[i]["MP_STATE"].ToString() == "4" || dt.Rows[i]["MP_STATE"].ToString() == "6")
                {
                    c++;
                }
                else if (dt.Rows[i]["MP_STATE"].ToString() == "3" || dt.Rows[i]["MP_STATE"].ToString() == "5" || dt.Rows[i]["MP_STATE"].ToString() == "7")
                {
                    d++;
                }
                else if (dt.Rows[i]["MP_STATE"].ToString() == "8")
                {
                    e++;
                }
                else if (dt.Rows[i]["MP_STATE"].ToString() == "9")
                {
                    f++;
                }
            }
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;

            if(a!=0)
            {
                rblstatus.Items.Add(new ListItem("未提交" + "</label><label><font color=red>(" + a + ")</font>", "0,1"));
                rblstatus.SelectedIndex = 0;
                InitParameter();
                InitVar();
                rblstatus_SelectedIndexChanged(null, null);

            }
            else
            {
                rblstatus.Items.Add(new ListItem("未提交", "0,1"));
            }

            if(b!=0)
            {
                rblstatus.Items.Add(new ListItem("待审核" + "</label><label><font color=red>(" + b + ")</font>", "2"));
                if (a == 0)
                {
                    rblstatus.SelectedIndex = 1;
                    InitParameter();
                    InitVar();
                    rblstatus_SelectedIndexChanged(null, null);
                }
            }
            else
            {
                rblstatus.Items.Add(new ListItem("待审核", "2"));
            }

            if(c!=0)
            {
                rblstatus.Items.Add(new ListItem("审核中" + "</label><label><font color=red>(" + c + ")</font>", "4,6"));
                if (a == 0 && b == 0)
                {
                    rblstatus.SelectedIndex = 2;
                    InitParameter();
                    InitVar();
                    rblstatus_SelectedIndexChanged(null, null);
                }
            }
            else
            {
                rblstatus.Items.Add(new ListItem("审核中", "4,6"));
            }

            if(d!=0)
            {
                rblstatus.Items.Add(new ListItem("驳回" + "</label><label><font color=red>(" + d + ")</font>", "3,5,7"));
                if (a == 0 && b == 0 && c == 0)
                {
                    rblstatus.SelectedIndex = 3;
                    InitParameter();
                    InitVar();
                    rblstatus_SelectedIndexChanged(null, null);
                    GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
                }
            }
            else
            {
                rblstatus.Items.Add(new ListItem("驳回", "3,5,7"));
            }

            if(e!=0)
            {
                rblstatus.Items.Add(new ListItem("审核通过" + "</label><label><font color=red>(" + e + ")</font>", "8"));
                if (a == 0 && b == 0 && c == 0 && d == 0)
                {
                    rblstatus.SelectedIndex = 4;
                    InitParameter();
                    InitVar();
                    rblstatus_SelectedIndexChanged(null, null);
                }
            }
            else
            {
                rblstatus.Items.Add(new ListItem("审核通过", "8"));
            }

            if(f!=0)
            {
                rblstatus.Items.Add(new ListItem("驳回已处理" + "</label><label><font color=red>(" + f + ")</font>", "9"));
                if (a == 0 && b == 0 && c == 0 && d == 0&&e==0)
                {
                    rblstatus.SelectedIndex = 5;
                    InitParameter();
                    InitVar();
                    rblstatus_SelectedIndexChanged(null, null);
                }
            }
            else
            {
                rblstatus.Items.Add(new ListItem("驳回已处理", "9"));
            }
            if (rblstatus.SelectedIndex == -1)
            {
                rblstatus.SelectedIndex = 0;
            }
            #endregion
        }

        //初始化参数
        private void InitParameter()
        {
            #region
            if (rblstate.SelectedItem.Text.Trim() == "正常")
            {
                tablename = "View_TM_MPFORALLRVW";
            }
            else
            {
                tablename = "View_TM_MPCHANGERVW";
            }
            #endregion
            if (rblstatus.SelectedValue == "0,1")/////去掉了提交人的限制MP_SUBMITID='" + Session["UserID"].ToString() + "'and 
            {
                sqlText = "MP_ENGID='" + plan_id + "' and MP_STATE in ('0','1')";
            }
            else if (rblstatus.SelectedValue == "4,6")
            {
                sqlText = "MP_ENGID='" + plan_id + "' and MP_STATE in ('4','6')";
            }
            else if (rblstatus.SelectedValue == "3,5,7")
            {
                sqlText = "MP_ENGID='" + plan_id + "' and MP_STATE in ('3','5','7')";
            }
            else
            {
                sqlText = "MP_ENGID='" + plan_id + "' and MP_STATE='" + rblstatus.SelectedValue + "'";
            }
            if (rblstate.SelectedItem.Text.Trim() == "正常")
            {
                sqlText += " AND MP_ID LIKE '%MP/%'";
            }
            fields = " MP_ID,CM_PROJ as MP_PJNAME,TSA_ENGNAME as MP_ENGNAME,MP_SUBMITTM,MP_ADATE,MP_STATE,MP_MASHAPE,MP_ID+'.'+MP_STATE as MP_NO,MP_MAP ";
        }

        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = tablename;
            pager.PrimaryKey = "MP_ID";
            pager.ShowFields = fields;
            pager.OrderField = ddlSort.SelectedValue;
            pager.StrWhere = sqlText;
            pager.OrderType =Convert.ToInt16(ddlSortOrder.SelectedValue);//按任务名称升序排列
            pager.PageSize = 20;
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
        }
        /// <summary>
        /// 变更状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTechRequireData();
            if (GridView1.Rows.Count > 0 && rblstatus.SelectedIndex == 3)//处于驳回时才有作废
            {
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
            }
            else
            {
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
            }
        }
        /// <summary>
        /// 类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SignStatus(rblstate.SelectedValue);
            GetTechRequireData();
            if (GridView1.Rows.Count > 0 && rblstatus.SelectedIndex == 3)//处于驳回时才有作废
            {
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = true;
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = true;
            }
            else
            {
                GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
            }
        }
        /// <summary>
        /// 处理材料计划驳回：驳回》驳回已处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hlDelete_OnClick(object sender, EventArgs e)
        {
            string lotnum =((LinkButton)sender).CommandName;
            string engid=lotnum.Split('.')[0];
           
            List<string> list_sql = new List<string>();
            if (lotnum.Contains("MPBG/"))//变更驳回
            {
                //变更材料计划审核表:修改驳回状态
                list_sql.Add(" update TBPM_MPCHANGERVW set MP_STATE='9' where MP_ID='" + lotnum + "'");
                //原始数据表（修改材料计划提交状态及审核状态）
                list_sql.Add(" update TBPM_ set BM_MPSTATE='0',BM_MPREVIEW='0' where BM_ENGID='" + engid + "' and BM_XUHAO in(select MP_NEWXUHAO from TBPM_MPCHANGE where MP_PID='" + lotnum + "')");
            }
            else if (lotnum.Contains("MP/"))//正常驳回
            {
                //正常材料计划审核表:修改驳回状态
                list_sql.Add(" update TBPM_MPFORALLRVW set MP_STATE='9' where MP_ID='" + lotnum + "'");
                //原始数据表（修改材料计划提交状态及审核状态）
                list_sql.Add(" update TBPM_STRINFODQO set BM_MPSTATE='0', BM_MPREVIEW='0' where BM_ENGID='" + engid + "' and BM_XUHAO in(select MP_NEWXUHAO from TBPM_MPPLAN where MP_PID='" + lotnum + "')");
            }
            else if (lotnum.Contains("MPQX/"))//取消驳回
            {
                //变更材料计划审核表:修改驳回状态
                list_sql.Add(" update TBPM_MPCHANGERVW set MP_STATE='9' where MP_ID='" + lotnum + "'");
            }
            DBCallCommon.ExecuteTrans(list_sql);
            this.SignStatus(rblstate.SelectedValue);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
        }

        public string GetTaskID
        {
            get
            {
                return Request.QueryString["plan_id"];
            }
        }
    }
}
