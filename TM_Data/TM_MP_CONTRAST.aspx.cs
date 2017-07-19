using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ZCZJ_DPF.TM_Data
{

    public partial class TM_MP_CONTRAST : System.Web.UI.Page
    {

        string sqlText;
        string[] Xuhao;
        string field;
        string strWhere;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }


        }

        private void InitInfo()
        {
            if (Request.QueryString["id"] != null)
            {

                tsa_id.Text = Request.QueryString["id"].ToString();

                sqlText = "select TSA_PJID,TSA_ENGNAME,CM_PROJ ";
                sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id.Text + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr.Read())
                {
                    lab_contract.Text = dr[0].ToString();
                    lab_engname.Text = dr[1].ToString();
                    lab_proname.Text = dr[2].ToString();
                }
                dr.Close();

                this.GetNameData(ddlXuhao);

                UCPagingMS.CurrentPage = 1;
                InitVar();
                bindGrid();
            }
        }

        private string GetZiXu()
        {

            field = Request.QueryString["Xuhao"];
            Xuhao = field.Split('$');
            string temp;
            if (field.Length == 1)
            {
                temp = "(BM_XUHAO like  '" + Xuhao[0] + ".%')";

            }
            else
            {
                temp = " (BM_XUHAO like  '" + Xuhao[0] + ".%'";

                for (int i = 1; i < Xuhao.Length; i++)
                {
                    temp += "or BM_XUHAO like '" + Xuhao[i] + ".%'";

                }
                temp += ")";

            }
            return temp;

        }
        private string GetFuXu()
        {

            field = Request.QueryString["Xuhao"];
            Xuhao = field.Split('$');
            string temp;
            if (field.Length == 1)
            {
                temp = "(BM_XUHAO =  '" + Xuhao[0] + "')";

            }
            else
            {
                temp = " (BM_XUHAO =  '" + Xuhao[0] + "'";

                for (int i = 1; i < Xuhao.Length; i++)
                {
                    temp += "or BM_XUHAO = '" + Xuhao[i] + "'";

                }
                temp += ")";

            }
            return temp;

        }

        /// <summary>
        /// 绑定新旧序号
        /// </summary>
        private void GetNameData(DropDownList clname)
        {
            string sqltext = "";
            sqltext = "select BM_ZONGXU+'|'+BM_CHANAME  as BM_XUHAO,BM_ZONGXU from TBPM_STRINFODQO ";
            sqltext += "where BM_ENGID='" + tsa_id.Text.Trim() + "' and " + GetFuXu() + " order by BM_ZONGXU ";
            string dataText = "BM_XUHAO";
            string dataValue = "BM_ZONGXU";
            DBCallCommon.BindDdl(clname, sqltext, dataText, dataValue);
        }
        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rad_detail_CheckedChanged(object sender, EventArgs e)
        {

            GridView1.Visible = true;
            GridView2.Visible = true;
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView3.Visible = true;
            GridView4.Visible = true;
            GridView4.DataSource = null;
            GridView4.DataBind();
            ddlmpName.SelectedIndex = 0;
            ddlXuhao.SelectedIndex = 0;
            UCPagingMS.CurrentPage = 1;
            InitVar();
            bindGrid();
        }
        /// <summary>
        /// 查看汇总信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rad_sum_CheckedChanged(object sender, EventArgs e)
        {

            if (GetNumbersOfCurrentLot())
            {
                GridView1.DataSource = null;
                GridView2.DataSource = null;
                GridView1.DataBind();
                GridView2.DataBind();
                GridView1.Visible = false;
                GridView2.Visible = false;
                GridView3.Visible = true;
                GridView4.Visible = true;
                GetCollList();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录,无法汇总！！！');", true);
            }

        }

        private void GetCollList()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = UseProc("View_TM_DQO", tsa_id.Text.Trim(), this.GetSearchList() + " and BM_MPSTATUS='3' and BM_MPSTATE='0'");
            dt2 = UseProc("View_TM_TEMPMARDATA", tsa_id.Text.Trim(), this.GetSearchList() + " and BM_ZONGXU in (select BM_ZONGXU from View_TM_DQO where  BM_MPSTATE='0' and BM_MPSTATUS<>'0' and  " + GetSearchList() + " )");
            GridView3.DataSource = dt1;
            GridView3.DataBind();
            GridView4.DataSource = dt2;
            GridView4.DataBind();
        }

        private DataTable UseProc(string viewtable, string taskId, string whereCon)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "TM_MPCHANGE_COLLECT");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Table_Name", viewtable, SqlDbType.VarChar, 1000);
                //DBCallCommon.AddParameterToStoredProc(sqlCmd, "@TaskId", taskId, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@WhereCon", whereCon, SqlDbType.VarChar, 1000);
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        /// <summary>
        /// 按类查询（汇总或明细）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlmpName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.GetNumbersOfCurrentLot())
            {


                if (rad_sum.Checked)//汇总信息
                {
                    GetCollList();

                }
                else//详细信息
                {

                    UCPagingMS.CurrentPage = 1;
                    InitVar();
                    bindGrid();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录,无法查询！！！');", true);
            }

        }

        private bool GetNumbersOfCurrentLot()
        {
            string sql_select = "select count(*) as Num from View_TM_DQO where BM_ENGID='" + tsa_id.Text.Trim() + "'and BM_WMARPLAN='Y' and BM_MPSTATUS<>'0' and " + GetZiXu();
            DataTable dt_sql_select = DBCallCommon.GetDTUsingSqlText(sql_select);
            if (Convert.ToInt16(dt_sql_select.Rows[0]["Num"].ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region
        PagerQueryParam pager_new = new PagerQueryParam();
        PagerQueryParam pager_old = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPagerMS();
            InitPagerMT();
            UCPagingMS.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
            UCPagingMS.PageSize = pager_new.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 旧表分页初始化
        /// </summary>
        private void InitPagerMT()
        {
            pager_old.TableName = "View_TM_TEMPMARDATA";
            pager_old.PrimaryKey = "idd";
            pager_old.ShowFields = "*";
            pager_old.OrderField = "BM_OLDINDEX";
            pager_old.StrWhere = this.GetSearchList() + " and BM_ZONGXU in (select BM_ZONGXU from View_TM_DQO where  BM_MPSTATE='0'  and BM_MPSTATUS<>'0' and  " + GetSearchList() + " )";
            pager_old.OrderType = 0;
            pager_old.PageSize = 50;
        }
        /// <summary>
        /// 新表分页初始化
        /// </summary>
        private void InitPagerMS()
        {

            pager_new.TableName = "View_TM_DQO";
            pager_new.PrimaryKey = "BM_ID";
            pager_new.ShowFields = "*";
            pager_new.OrderField = "BM_ZONGXU";
            pager_new.StrWhere = this.GetSearchList() + " and BM_MPSTATUS='3' and BM_MPSTATE='0' ";
            pager_new.OrderType = 0;//升序排列
            pager_new.PageSize = 50;
        }
        void Pager_PageChangedMS(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_new.PageIndex = UCPagingMS.CurrentPage;
            pager_old.PageIndex = UCPagingMS.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_new);
            DataTable dtOld = CommonFun.GetDataByPagerQueryParam(pager_old);
            CommonFun.Paging(dt, GridView1, UCPagingMS, NoDataPanel);
            CommonFun.Paging(dtOld, GridView2, UCPagingMS, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPagingMS.Visible = false;
            }
            else
            {
                UCPagingMS.Visible = true;
                UCPagingMS.InitPageInfo();  //分页控件中要显示的控件
            }

        }
        #endregion

        /// <summary>
        /// 返回详细信息的查询条件
        /// </summary>
        private string GetSearchList()
        {

            //获取类别
            string wherecond = "BM_ENGID='" + tsa_id.Text.Trim() + "'and BM_WMARPLAN='Y'  and BM_MARID<>''  and " + GetZiXu();

            if (ddlmpName.SelectedItem.Text.Trim() == "标准件")
            {
                wherecond += " and BM_MASHAPE='采' ";
            }
            else if (ddlmpName.SelectedItem.Text.Trim() == "钢材")
            {
                wherecond += " and (BM_MASHAPE='板' or BM_MASHAPE='型' or BM_MASHAPE='圆') ";
            }
            else if (ddlmpName.SelectedItem.Text.Trim() == "铸锻件")
            {
                wherecond += "and ( BM_MASHAPE='锻' or BM_MASHAPE='铸')";
            }
            else if (ddlmpName.SelectedItem.Text.Trim() == "采购成品")
            {
                wherecond += " and BM_MASHAPE  = '采购成品' ";
            }
            else if (ddlmpName.SelectedItem.Text.Trim() == "非金属")
            {
                wherecond += " and BM_MASHAPE  = '非金属' ";
            }

            //新旧序号
            if (ddlXuhao.SelectedIndex != 0)
            {
                wherecond += " and BM_ZONGXU like '" + ddlXuhao.SelectedValue + ".%'";
            }
            return wherecond;
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            if (this.GetNumbersOfCurrentLot())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('材料计划变更功能关闭！');", true);
                return;
                sqlText = "update TBPM_STRINFODQO set BM_MPSTATE='1',BM_MPSTATUS='0' where BM_ENGID='" + tsa_id.Text.Trim() + "' and " + GetZiXu() + " or " + GetFuXu();
                try
                {
                    DBCallCommon.ExeSqlText(sqlText);
                }
                catch (Exception)
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('程序异常，请联系管理员！');", true);
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该批记录已更改为【已提交】');location.href='TM_Task_View.aspx?time=" + DateTime.Now.ToString() + "&action=" + tsa_id.Text.Trim() + "';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录,无法操作！！！');", true);
            }

        }
    }
}
