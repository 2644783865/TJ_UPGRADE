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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_OrgDataImportAll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInit();
                ViewState["CurrentFX"] = 1;
                this.GetAllControl();
                lblNumber.Text = "1";
            }
            if (IsPostBack)
            {

                this.InitVar();

                this.GetAllControl();
            }
        }

        protected void PageInit()
        {
            ViewState["TaskID"] = Request.QueryString["TaskID"].ToString();
            string sql = "select  TSA_ID, TSA_PJID, TSA_ENGNAME,CM_PROJ from View_TM_TaskAssign where TSA_ID='" + ViewState["TaskID"].ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lblProjName.Text = dt.Rows[0]["CM_PROJ"].ToString() + "(" + dt.Rows[0]["TSA_ID"].ToString() + ")";
                lblEngName.Text = dt.Rows[0]["TSA_ENGNAME"].ToString();


            }
            this.BindProjName();

            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;

        }
        /// <summary>
        /// 绑定项目名称
        /// </summary>
        protected void BindProjName()
        {
            string sql_pj = "select distinct TSA_PJID,TSA_PJID+'|'+isnull(CM_PROJ,'') as PJ_NAME from View_TM_TaskAssign order by TSA_PJID desc";
            string dataText = "PJ_NAME";
            string dataValue = "TSA_PJID";
            DBCallCommon.BindAJAXCombox(ddlProName, sql_pj, dataText, dataValue);
        }
        /// <summary>
        /// 绑定设备名称
        /// </summary>
        protected void BindEngName()
        {
            string sql_eng = "select  TSA_ID, TSA_ID+'|'+TSA_ENGNAME as TSA_ENGNAME from View_TM_TaskAssign where TSA_PJID='" + ddlProName.SelectedValue + "'  order by TSA_ID";
            string dataText = "TSA_ENGNAME";
            string dataValue = "TSA_ID";
            DBCallCommon.BindAJAXCombox(ddlEngName, sql_eng, dataText, dataValue);
        }
        /// <summary>
        /// 项目改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindEngName();
            if (ddlProName.SelectedIndex != 0 && ddlEngName.SelectedIndex != 0)
            {
                UCPaging1.CurrentPage = 1;
                InitVar();
                this.GetBoundData();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                NoDataPanel.Visible = true;
                UCPaging1.Visible = false;
            }
            tb_all.Visible = false;
        }
        /// <summary>
        /// 工程名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex != 0 && ddlEngName.SelectedIndex != 0)
            {

                string engid = ddlEngName.SelectedValue;
                tb_all.Visible = true;
                InitVar();
                this.GetBoundData();
                this.GetContextKey();
            }
            else
            {
                tb_all.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                NoDataPanel.Visible = true;
                UCPaging1.Visible = false;

            }
        }
        /// <summary>
        /// 返回Where条件
        /// </summary>
        /// <returns></returns>
        protected string GetStrWhere()
        {
            string returnValue = "";
            returnValue = " BM_ENGID='" + ddlEngName.SelectedValue + "'";
            return returnValue;
        }

        protected void btnAddFX_OnClick(object sender, EventArgs e)
        {
            this.AddControls();
        }

        protected void GetAllControl()
        {
            for (int i = 0; i < Convert.ToInt16((ViewState["CurrentFX"]).ToString()); i++)
            {
                Control uiz = this.Page.LoadControl("../Controls/UserInputZongxu.ascx");
                uiz.ID = "UserInputZongxu_" + (i + 1).ToString();

                TableRow tr1 = new TableRow();
                tr1.ID = "tr1_" + (i + 1).ToString();
                TableCell td1 = new TableCell();
                td1.ID = "td1_" + (i + 1).ToString();

                td1.Controls.Add(uiz);
                tr1.Cells.Add(td1);
                tb1.Rows.Add(tr1);

                ZCZJ_DPF.UserInputZongxu tt = (ZCZJ_DPF.UserInputZongxu)uiz;


                tt.FZ = ddlFZ.SelectedValue;
                tt.TabelName = "View_TM_DQO";
                tt.TaskID = ddlEngName.SelectedValue;
                tt.TabelNameTarget = "View_TM_DQO";
                tt.TaskIDTarget = ViewState["TaskID"].ToString();

                ((AjaxControlToolkit.AutoCompleteExtender)tt.FindControl("AutoCompleteExtender")).ContextKey = tt.TabelName + "@" + tt.TaskID;//BM_ZONGXU(BM_XUHAO)@TableName@TaskID

            }
            ((ImageButton)((ZCZJ_DPF.UserInputZongxu)tb1.FindControl("UserInputZongxu_1")).FindControl("imgbDelete")).Visible = false;
        }

        protected void AddControls()
        {
            int i = Convert.ToInt16(ViewState["CurrentFX"].ToString());
            Control uiz = this.Page.LoadControl("../Controls/UserInputZongxu.ascx");
            uiz.ID = "UserInputZongxu_" + (i + 1).ToString();
            TableRow tr1 = new TableRow();
            tr1.ID = "tr1_" + (i + 1).ToString();
            TableCell td1 = new TableCell();
            td1.ID = "td1_" + (i + 1).ToString();

            td1.Controls.Add(uiz);
            tr1.Cells.Add(td1);
            tb1.Rows.Add(tr1);
            ViewState["CurrentFX"] = Convert.ToInt16(ViewState["CurrentFX"].ToString()) + 1;
        }

        protected void GetContextKey()
        {
            for (int i = 0; i < Convert.ToInt16((ViewState["CurrentFX"]).ToString()); i++)
            {
                ZCZJ_DPF.UserInputZongxu tt = (ZCZJ_DPF.UserInputZongxu)tb1.FindControl("UserInputZongxu_" + (i + 1).ToString());

                tt.FZ = ddlFZ.SelectedValue;
                tt.TabelName = "View_TM_DQO";
                tt.TaskID = ddlEngName.SelectedValue.Split('%')[0];
                tt.TabelNameTarget = "View_TM_DQO";
                tt.TaskIDTarget = ViewState["TaskID"].ToString();

                ((AjaxControlToolkit.AutoCompleteExtender)tt.FindControl("AutoCompleteExtender")).ContextKey = tt.TabelName + "@" + tt.TaskID;//BM_ZONGXU(BM_XUHAO)@TableName@TaskID
            }
        }

        protected void ddlZF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetContextKey();
        }


        /// <summary>
        /// 开始导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ImportAll(object sender, EventArgs e)
        {
            string retValue = this.CheckEnableSubmit();
            if (retValue == "Ok")
            {
                ParamsImport psv = new ParamsImport();
                psv.Tg_TableName = "TBPM_STRINFODQO";
                psv.Tg_Engid = ViewState["TaskID"].ToString();
                ////psv.Tg_FatherIndex = "";

                psv.Src_TableName = "View_TM_DQO";
                psv.Src_Engid = ddlEngName.SelectedValue.Split('%')[0];
                ////psv.Src_FatherIndex = "";



                psv.Bef_RepTuhao_1 = txtOldTu_1.Text.Trim();
                psv.Aft_RepTuhao_1 = txtNewTu_1.Text.Trim();
                psv.Bef_RepTuhao_2 = txtOldTu_2.Text.Trim();
                psv.Aft_RepTuhao_2 = txtNewTu_2.Text.Trim();

                for (int i = 0; i < Convert.ToInt16((ViewState["CurrentFX"]).ToString()); i++)
                {
                    ZCZJ_DPF.UserInputZongxu tt = (ZCZJ_DPF.UserInputZongxu)tb1.FindControl("UserInputZongxu_" + (i + 1).ToString());
                    if (tt.Visible)
                    {
                        psv.Src_FatherIndex = ((TextBox)tt.FindControl("txtBeforeFX")).Text.Trim().Split('|')[1].Trim();
                        psv.Tg_FatherIndex = ((TextBox)tt.FindControl("txtAfterFX")).Text.Trim();
                        psv.NotImport = tt.ArrayNotImport.Replace("-", "'");
                        psv.Tg_TotalTaishu = lblNumber.Text.Trim();
                        try
                        {
                            this.ExecImport(psv);
                            ((TextBox)tt.FindControl("txtBeforeFX")).Text = "";
                            ((TextBox)tt.FindControl("txtAfterFX")).Text = "";
                        }
                        catch (Exception)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导入【" + psv.Src_FatherIndex + "】时出错，该父序及后续父序导入未完成！！！\\r\\r可能原因:复制导致目标工程中序号重复');", true);
                            return;
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导入完成！！！');", true);

            }
            else if (retValue.Contains("BelongTo"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法导入！！！\\r【复制后父序】存在归属关系！！！');", true);
            }
            else if (retValue.Contains("Empty"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法导入！！！\\r【待复制父序】及【复制后父序】不能为空！！！');", true);
            }
            else if (retValue.Contains("NoFather"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法导入！！！\\r父级不存在！！！');", true);
            }
        }

        /// <summary>
        /// 存储过程参数类
        /// </summary>
        private class ParamsImport
        {
            string _Tg_TableName;
            string _Tg_Engid;
            string _Tg_FatherIndex;

            string _Tg_TotalTaishu;

            string _Src_TableName;
            string _Src_Engid;
            string _Src_FatherIndex;

            ////int _Sc_TotalTaishu;

            string _Coype_Type;

            string _Bef_RepTuhao_1;
            string _Aft_RepTuhao_1;
            string _Bef_RepTuhao_2;
            string _Aft_RepTuhao_2;

            string _NotImport;

            public string Tg_TableName
            {
                get { return _Tg_TableName; }
                set { _Tg_TableName = value; }
            }
            public string Tg_Engid
            {
                get { return _Tg_Engid; }
                set { _Tg_Engid = value; }
            }
            public string Tg_FatherIndex
            {
                get { return _Tg_FatherIndex; }
                set { _Tg_FatherIndex = value; }
            }

            public string Tg_TotalTaishu
            {
                get { return _Tg_TotalTaishu; }
                set { _Tg_TotalTaishu = value; }
            }

            public string Src_TableName
            {
                get { return _Src_TableName; }
                set { _Src_TableName = value; }
            }

            public string Src_Engid
            {
                get { return _Src_Engid; }
                set { _Src_Engid = value; }
            }
            public string Src_FatherIndex
            {
                get { return _Src_FatherIndex; }
                set { _Src_FatherIndex = value; }
            }
            //////public int Sc_TotalTaishu
            //////{
            //////    get { return _Sc_TotalTaishu; }
            //////    set { _Sc_TotalTaishu = value; }
            //////}

            public string Coype_Type
            {
                get { return _Coype_Type; }
                set { _Coype_Type = value; }
            }
            public string Bef_RepTuhao_1
            {
                get { return _Bef_RepTuhao_1; }
                set { _Bef_RepTuhao_1 = value; }
            }
            public string Aft_RepTuhao_1
            {
                get { return _Aft_RepTuhao_1; }
                set { _Aft_RepTuhao_1 = value; }
            }
            public string Bef_RepTuhao_2
            {
                get { return _Bef_RepTuhao_2; }
                set { _Bef_RepTuhao_2 = value; }
            }
            public string Aft_RepTuhao_2
            {
                get { return _Aft_RepTuhao_2; }
                set { _Aft_RepTuhao_2 = value; }
            }

            public string NotImport
            {
                get { return _NotImport; }
                set { _NotImport = value; }
            }

        }

        /// <summary>
        /// 执行存储过程验证序号（保存时）
        /// </summary>
        /// <param name="psv"></param>
        private void ExecImport(ParamsImport psv)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_ImportDataAll_Modify]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Tg_TableName", psv.Tg_TableName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Tg_Engid", psv.Tg_Engid, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Tg_FatherIndex", psv.Tg_FatherIndex, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Tg_TotalTaishu", psv.Tg_TotalTaishu, SqlDbType.Int, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Src_TableName", psv.Src_TableName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Src_Engid", psv.Src_Engid, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Src_FatherIndex", psv.Src_FatherIndex, SqlDbType.Text, 1000);
                //////DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Sc_TotalTaishu", psv.Sc_TotalTaishu, SqlDbType.Int, 1000);
                //  DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Coype_Type", psv.Coype_Type, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Bef_RepTuhao_1", psv.Bef_RepTuhao_1, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Aft_RepTuhao_1", psv.Aft_RepTuhao_1, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Bef_RepTuhao_2", psv.Bef_RepTuhao_2, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Aft_RepTuhao_2", psv.Aft_RepTuhao_2, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@NotImport", psv.NotImport, SqlDbType.Text, 4000);
                sqlConn.Open();
                sqlCmd.CommandTimeout = 0;
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected string CheckEnableSubmit()
        {
            ArrayList array_bx_ax = new ArrayList();

            for (int i = 0; i < Convert.ToInt16((ViewState["CurrentFX"]).ToString()); i++)
            {
                ZCZJ_DPF.UserInputZongxu tt = (ZCZJ_DPF.UserInputZongxu)tb1.FindControl("UserInputZongxu_" + (i + 1).ToString());
                if (tt.Visible)
                {
                    CopyBx_Ax cba = new CopyBx_Ax(((TextBox)tt.FindControl("txtBeforeFX")).Text, ((TextBox)tt.FindControl("txtAfterFX")).Text);
                    array_bx_ax.Add(cba);
                    if (((Label)tt.FindControl("lblTip")).Visible && ((Label)tt.FindControl("lblTip")).Text == "没有父级，无法导入")
                    {
                        return "NoFather";
                    }
                }
            }

            foreach (object obj in array_bx_ax)
            {
                CopyBx_Ax ba = obj as CopyBx_Ax;
                if (ba.AX == "" || ba.BX == "")
                {
                    return "Empty";
                }
            }

            int cytimes = array_bx_ax.Count;
            for (int i = 0; i < cytimes; i++)
            {
                for (int j = 0; j < cytimes; j++)
                {
                    if (i != j)
                    {
                        CopyBx_Ax ba_i = array_bx_ax[i] as CopyBx_Ax;
                        CopyBx_Ax ba_j = array_bx_ax[j] as CopyBx_Ax;
                        if (ba_i.AX.Contains(ba_j.AX))
                        {
                            return "BelongTo";
                        }
                    }
                }
            }
            return "Ok";
        }

        public class CopyBx_Ax
        {
            public CopyBx_Ax() { }

            public CopyBx_Ax(string bx, string ax)
            {
                this.AX = ax;
                this.BX = bx;
            }

            private string ax;
            public string AX
            {
                get { return ax; }
                set { ax = value; }
            }

            private string bx;
            public string BX
            {
                get { return bx; }
                set { bx = value; }
            }
        }

        #region 分页
        PagerQueryParam pager = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "View_TM_DQO";
            pager.PrimaryKey = "BM_ID";
            pager.ShowFields = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER";
            pager.OrderField = "dbo.f_formatstr(BM_ZONGXU,'.')";
            pager.StrWhere = this.GetStrWhere();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 20;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion
    }
}
