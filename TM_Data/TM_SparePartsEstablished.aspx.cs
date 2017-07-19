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
    public partial class TM_SparePartsEstablished : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInit();
            }
        }

        protected void PageInit()
        {
            ViewState["TaskID"] = Request.QueryString["TaskID"].ToString();

            #region 基本信息
            string sql = "select  TSA_ID, TSA_PJID, TSA_ENGNAME, TSA_ENGSTRTYPE,TSA_PJNAME from View_TM_TaskAssign where TSA_ID='" + ViewState["TaskID"].ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lblProjName.Text = dt.Rows[0]["TSA_PJNAME"].ToString() + "(" + dt.Rows[0]["TSA_PJID"].ToString() + ")";
                lblEngName.Text = dt.Rows[0]["TSA_ENGNAME"].ToString() + "_" + dt.Rows[0]["TSA_ID"].ToString();
                hdfType.Value = dt.Rows[0]["TSA_ENGSTRTYPE"].ToString();
                hdfEngid.Value = ViewState["TaskID"].ToString();
                hdfProid.Value = dt.Rows[0]["TSA_PJID"].ToString();

                switch (dt.Rows[0]["TSA_ENGSTRTYPE"].ToString())
                {
                    case "回转窑":
                        ViewState["tablename"] = "TBPM_STRINFOHZY";
                        ViewState["viewtablename"] = "View_TM_HZY";
                        break;
                    case "球、立磨":
                        ViewState["tablename"] = "TBPM_STRINFOQLM";
                        ViewState["viewtablename"] = "View_TM_QLM";
                        break;
                    case "篦冷机":
                        ViewState["tablename"] = "TBPM_STRINFOBLJ";
                        ViewState["viewtablename"] = "View_TM_BLJ";
                        break;
                    case "堆取料机":
                        ViewState["tablename"] = "TBPM_STRINFODQLJ";
                        ViewState["viewtablename"] = "View_TM_DQLJ";
                        break;
                    case "钢结构及非标":
                        ViewState["tablename"] = "TBPM_STRINFOGFB";
                        ViewState["viewtablename"] = "View_TM_GFB";
                        break;
                    case "电气及其他":
                        ViewState["tablename"] = "TBPM_STRINFODQO";
                        ViewState["viewtablename"] = "View_TM_DQO";
                        break;
                    default: break;
                }
            }
            //读取台数
            string sql_gettaishu = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" + ViewState["TaskID"].ToString().Split('-')[0] + "'";
            SqlDataReader dr_number = DBCallCommon.GetDRUsingSqlText(sql_gettaishu);
            if (dr_number.HasRows)
            {
                dr_number.Read();
                lblNumber.Text = dr_number["TSA_NUMBER"].ToString();
                hdfNumber.Value = dr_number["TSA_NUMBER"].ToString();
                dr_number.Close();
            }
            #endregion

            #region 可生成备件明细
            string sqlbj = "select *,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER from " + ViewState["viewtablename"].ToString() + " where BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_KU='库' and BM_MASHAPE='采' and BM_MASTATE='标' AND BM_MARID<>'' AND BM_BJ='N' AND BM_WMARPLAN='Y' order by dbo.f_formatstr(BM_XUHAO,'.')";
            DataTable dt_bj = DBCallCommon.GetDTUsingSqlText(sqlbj);
            if (dt_bj.Rows.Count > 0)
            {
                GridView1.DataSource = dt_bj;
                GridView1.DataBind();
                NoDataPanel.Visible = false;
                btnCreate.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                NoDataPanel.Visible = true;
                btnCreate.Visible = false;
            }
            #endregion

            this.BindPartsName();

            this.BindFatherBJName();

            txtBiaoshi.Text = "如:FER-SRC135.02.04.04";
            txtBiaoshi.ForeColor = System.Drawing.Color.Gray;
        }

        #region 全选、取消、连选
        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_org_OnClick(object sender, EventArgs e)
        {
            CheckBoxSelectDefine(GridView1, "CheckBox2");
        }

        /// <summary>
        /// CheckBox连选(此函数勿动)
        /// </summary>
        /// <param name="smartgridview"></param>
        /// <param name="ckbname"></param>
        public void CheckBoxSelectDefine(YYControls.SmartGridView smartgridview, string ckbname)
        {
            int startindex = -1;
            int endindex = -1;
            for (int i = 0; i < smartgridview.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)smartgridview.Rows[i].FindControl(ckbname);
                if (cbx.Checked)
                {
                    startindex = i;
                    break;
                }
            }

            for (int j = smartgridview.Rows.Count - 1; j > -1; j--)
            {
                CheckBox cbx = (CheckBox)smartgridview.Rows[j].FindControl(ckbname);
                if (cbx.Checked)
                {
                    endindex = j;
                    break;
                }
            }

            if (startindex < 0 || endindex < 0 || startindex == endindex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('需要勾选2条记录！！！');", true);
            }
            else
            {
                for (int k = startindex; k <= endindex; k++)
                {
                    CheckBox cbx = (CheckBox)smartgridview.Rows[k].FindControl(ckbname);
                    cbx.Checked = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// 生成备件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_OnClick(object sender, EventArgs e)
        {
            if (txtBiaoshi.Text.Trim() == "如:FER-SRC135.02.04.04" || txtBiaoshi.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入【标识前缀】！！！');", true);
                return;
            }

            if (ddlBJFA.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择【生成备件父级】！！！\\r\\r提示:如果没有记录，请在BOM录入界面输入【随机备件】');", true);
            }
            else
            {
                ParamsBj param = new ParamsBj();
                param.BjFather = ddlBJFA.SelectedValue;
                param.BjType = ddlBJType.SelectedValue;
                param.CalType = ddlJiSuanType.SelectedValue;
                param.EngID = ViewState["TaskID"].ToString();
                param.TableName = ViewState["tablename"].ToString();
                param.UserID = Session["UserID"].ToString();
                param.Xishu = txtXiShu.Text.Trim();
                param.BiaoShi = txtBiaoshi.Text.Trim();
                string waitfor = "";
                foreach (GridViewRow grow in GridView1.Rows)
                {
                    if (((CheckBox)grow.FindControl("CheckBox2")).Checked)
                    {
                        waitfor += ((Label)grow.FindControl("Label1")).Text.Trim() + ',';
                    }
                }

                param.WaitFor = waitfor.Substring(0, waitfor.Length - 1);

                if (param.WaitFor.Length > 3000)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('对不起，您选择的记录过多，请减少勾选记录后再试！！！');", true);
                    return;
                }

                try
                {
                    this.ExecSpareParts(param);
                    this.btnQurey_OnClick(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('备件已生成成功！！！\\r\\r提示：如需修改请进入查看界面！！！');", true);
                }
                catch
                {
                    string error_detail = "程序出现未知错误，请联系管理员！！！";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('"+error_detail+"');", true);
                }
                
            }
        }
        /// <summary>
        /// 执行存储过程验证序号（保存时）
        /// </summary>
        /// <param name="psv"></param>
        private void ExecSpareParts(ParamsBj pbj)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_SparePartsGen]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@TableName", pbj.TableName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@EngID", pbj.EngID, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@BjType", pbj.BjType, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@CalType", pbj.CalType, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@BjFather", pbj.BjFather, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@UserID", pbj.UserID, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Xishu", pbj.Xishu, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@WaitFor", pbj.WaitFor, SqlDbType.Text, 3000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@BiaoShi", pbj.BiaoShi, SqlDbType.Text, 3000);
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 清空查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMSClear_OnClick(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqMS.FindControl("GridView1"));
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQurey_OnClick(object sender, EventArgs e)
        {
            string sqltext = "select *,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER from " + ViewState["viewtablename"].ToString() + " where BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_KU='库' and BM_MASHAPE='采' and BM_MASTATE='标' AND BM_MARID<>'' AND BM_BJ='N'";
            if (ddlParts.SelectedIndex != 0)
            {
                sqltext += "AND (BM_XUHAO='"+ddlParts.SelectedValue+"' OR BM_XUHAO LIKE '"+ddlParts.SelectedValue+".%')";
            }
            udqMS.ExistedConditions = sqltext;
            sqltext += UserDefinedQueryConditions.ReturnQueryString((GridView)udqMS.FindControl("GridView1"), (Label)udqMS.FindControl("Label1"));
            sqltext += " order by dbo.f_formatstr(BM_XUHAO,'.')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                NoDataPanel.Visible = false;
                btnCreate.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                NoDataPanel.Visible = true;
                btnCreate.Visible = false;
            }
        }
        /// <summary>
        /// 绑定部件名称
        /// </summary>
        protected void BindPartsName()
        {
            string sqltext = "select BM_XUHAO+'‖'+BM_CHANAME AS BM_NAME,BM_XUHAO FROM " + ViewState["viewtablename"].ToString() + " WHERE BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND dbo.Splitnum(BM_XUHAO,'.')='2' and BM_XUHAO NOT LIKE '1.0.%'  ORDER BY dbo.f_formatstr(BM_XUHAO,'.')";
            string dataText = "BM_NAME";
            string dataValue = "BM_XUHAO";
            DBCallCommon.BindDdl(ddlParts, sqltext, dataText, dataValue);
        }
        /// <summary>
        /// 绑定备件的父级
        /// </summary>
        protected void BindFatherBJName()
        {
            string sqltext = "select BM_XUHAO+'‖'+BM_CHANAME AS BM_NAME,BM_XUHAO FROM " + ViewState["viewtablename"].ToString() + " WHERE BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND dbo.Splitnum(BM_XUHAO,'.')='1' and BM_CHANAME='随机备件'  ORDER BY dbo.f_formatstr(BM_XUHAO,'.')";
            string dataText = "BM_NAME";
            string dataValue = "BM_XUHAO";
            DBCallCommon.BindDdl(ddlBJFA, sqltext, dataText, dataValue);
        }

        private class ParamsBj
        {
            string _TableName;
            string _EngID;
            string _BjType;//创建备件的类型
            string _CalType;//计算方式
            string _BjFather;//备件父级
            string _UserID;
            string _Xishu;
            string _WaitFor;//待创建备件序号
            string _BiaoShi;
            public string TableName
            {
                get { return _TableName; }
                set { _TableName = value; }
            }
            public string EngID
            {
                get { return _EngID; }
                set { _EngID = value; }
            }
            public string BjType
            {
                get { return _BjType; }
                set { _BjType = value; }
            }
            public string CalType
            {
                get { return _CalType; }
                set { _CalType = value; }
            }
            public string UserID
            {
                get { return _UserID; }
                set { _UserID = value; }
            }
            public string Xishu
            {
                get { return _Xishu; }
                set { _Xishu = value; }
            }
            public string BjFather
            {
                get { return _BjFather; }
                set { _BjFather = value; }
            }
            public string WaitFor
            {
                get { return _WaitFor; }
                set { _WaitFor = value; }
            }
            public string BiaoShi
            {
                get { return _BiaoShi; }
                set { _BiaoShi = value; }
            }
        }

    }
}
