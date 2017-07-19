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
    public partial class TM_Out_Source_View : System.Web.UI.Page
    {
        string out_id;
        string sqlText;
        string fields;
        string tablename;
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            out_id = Request.QueryString["out_id"];
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
            GetTechOutData();
        }

        //标记材料状态
        private void SignStatus(string type)
        {
            int a = 0;//未提交
            int b = 0;//待审核
            int c = 0;//审核中
            int d = 0;//驳回
            int e = 0;//审核通过
            int f=0;//驳回已处理
            #region
            switch(type)
            {
                case "0":
                    sqlText = "select OST_STATE from TBPM_OUTSOURCETOTAL where OST_ENGID='" + out_id + "' ";
                    sqlText += "and  OST_OUTSOURCENO like '% WX/%' ";
                    break;
                case "1":
                    sqlText = "select OST_STATE from TBPM_OUTSCHANGERVW where OST_ENGID='" + out_id + "' ";
                    sqlText += "and OST_SUBMITER='" + Session["UserID"].ToString() + "' ";
                    break;
                default:
                    break;
            }
            DataTable  dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            for(int i=0;i<dt.Rows.Count;i++)
            {
                if (dt.Rows[i]["OST_STATE"].ToString() == "0" || dt.Rows[i]["OST_STATE"].ToString() == "1")  //未提交
                {
                    a++;
                }
                else if (dt.Rows[i]["OST_STATE"].ToString() == "2")    //提交(待审核)
                {
                    b++;
                }
                else if (dt.Rows[i]["OST_STATE"].ToString() == "4" || dt.Rows[i]["OST_STATE"].ToString() == "6") //审核中
                {
                    c++;
                }
                else if (dt.Rows[i]["OST_STATE"].ToString() == "3" || dt.Rows[i]["OST_STATE"].ToString() == "5" || dt.Rows[i]["OST_STATE"].ToString() == "7")
                {
                    d++;
                }
                else if(dt.Rows[i]["OST_STATE"].ToString()=="8")
                {
                    e++;
                }
                else if(dt.Rows[i]["OST_STATE"].ToString()=="9")
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
            #endregion
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
        }


        //初始化参数
        private void InitParameter()
        {
            if (rblstate.SelectedItem.Text.Trim() == "正常")
            {
                tablename = "View_TM_OUTSOURCETOTAL";
            }
            else
            {
                tablename = "View_TM_OUTSCHANGERVW";
            }
            #region
            if (rblstatus.SelectedValue == "0,1")////OST_SUBMITER='" + Session["UserID"].ToString() + "' and
            {
                sqlText = " OST_ENGID='" + out_id + "' and OST_STATE in ('0','1') ";
            }
            else if (rblstatus.SelectedValue == "4,6")
            {
                sqlText = " OST_ENGID='" + out_id + "' and OST_STATE in ('4','6') ";
            }
            else if (rblstatus.SelectedValue == "3,5,7")
            {
                sqlText = " OST_ENGID='" + out_id + "' and OST_STATE in ('3','5','7') ";
            }
            else if (rblstatus.SelectedValue == "2")
            {
                sqlText = " OST_ENGID='" + out_id + "' and OST_STATE='" + rblstatus.SelectedValue + "' ";
            }
            else if (rblstatus.SelectedValue == "8")
            {
                sqlText = " OST_ENGID='" + out_id + "' and OST_STATE='" + rblstatus.SelectedValue + "' ";
            }
            else if (rblstatus.SelectedValue == "9")
            {
                sqlText = " OST_ENGID='" + out_id + "' and OST_STATE='" + rblstatus.SelectedValue + "' ";
            }
            #endregion
            if (rblstate.SelectedItem.Text.Trim() == "正常")
            {
                sqlText += " and OST_OUTSOURCENO like '% WX/%'";
            }
            fields = "OST_OUTSOURCENO,OST_PJNAME,OST_ENGNAME,OST_MDATE,OST_ADATE,OST_STATE,OST_OUTTYPE,OST_OUTSOURCENO+'.'+OST_STATE as OST_ID  ";
        }

        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = tablename;
            pager.PrimaryKey = "OST_OUTSOURCENO";
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
            GetTechOutData();
        }

        protected void GetTechOutData()
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
            GetTechOutData();
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

        protected void rblstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SignStatus(rblstate.SelectedValue);
            GetTechOutData();
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
        /// 作废：改变审核表中的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hlDelete_OnClick(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandName;
            string engid = lotnum.Split('.')[0];
            string tablenameOrg = this.GetOrgTableNameByLotNum(lotnum);
            List<string> list_sql = new List<string>();
            if (lotnum.Contains(" WXBG/"))//变更驳回
            {
                //变更外协审核表:修改驳回状态
                list_sql.Add(" update  TBPM_OUTSCHANGERVW set OST_STATE='9' where OST_CHANGECODE='" + lotnum + "'");
                //原始数据表（修改材料计划提交状态及审核状态）
                list_sql.Add(" update " + tablenameOrg + " set BM_OSSTATE='0' and BM_OSREVIEW='0' where BM_ENGID='" + engid + "' and BM_XUHAO in(select OSL_NEWXUHAO from TBPM_OUTSCHANGE where OST_CHANGECODE='" + lotnum + "')");
            }
            else if (lotnum.Contains(" WX/"))//正常驳回
            {
                //正常外协审核表:修改驳回状态
                list_sql.Add(" update TBPM_OUTSOURCETOTAL set OST_STATE='9' where OST_OUTSOURCENO='" + lotnum + "'");
                //原始数据表（修改外协提交状态及审核状态）
                list_sql.Add(" update " + tablenameOrg + " set BM_OSSTATE='0', BM_OSREVIEW='0' where BM_ENGID='" + engid + "' and BM_XUHAO in(select OSL_NEWXUHAO from TBPM_OUTSOURCELIST where OSL_OUTSOURCENO='" + lotnum + "')");
            }
            else if (lotnum.Contains(" WXQX/"))//取消驳回
            {
                //变更外协审核表:修改驳回状态
                list_sql.Add(" update TBPM_MPCHANGERVW set OST_STATE='9' where OST_CHANGECODE='" + lotnum + "'");
            }
            DBCallCommon.ExecuteTrans(list_sql);
            this.SignStatus(rblstate.SelectedValue);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
        }
        /// <summary>
        /// 根据批号获得原始数据表
        /// </summary>
        /// <param name="lotnumber"></param>
        /// <returns></returns>
        protected string GetOrgTableNameByLotNum(string lotnumber)
        {
            string tskid = lotnumber.Split('.')[0];
            string engtype = "";
            string strutablenm = "";
            string sql_select_tv = "select TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID from View_TM_TaskAssign where TSA_ID='" + tskid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_select_tv);
            if (dr.HasRows)
            {
                dr.Read();
                engtype = dr["TSA_ENGSTRTYPE"].ToString();//工程结构类型
                dr.Close();
            }
            #region
            switch (engtype)
            {

                case "回转窑":
                    strutablenm = "TBPM_STRINFOHZY";
                    break;
                case "球、立磨":
                    strutablenm = "TBPM_STRINFOQLM";
                    break;
                case "篦冷机":
                    strutablenm = "TBPM_STRINFOBLJ";
                    break;
                case "堆取料机":
                    strutablenm = "TBPM_STRINFODQLJ";
                    break;
                case "钢结构及非标":
                    strutablenm = "TBPM_STRINFOGFB";
                    break;
                case "电气及其他":
                    strutablenm = "TBPM_STRINFODQO";
                    break;
            }
            #endregion
            return strutablenm;
        }


        public string GetTaskID
        {
            get
            {
                return Request.QueryString["out_id"];
            }
        }

    }
}
