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
using System.Text;
using System.Data.SqlClient;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_ContentReplace : System.Web.UI.Page
    {
       string viewtable;
       string tablename;
       string mptable;
       string mstable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInit();
                TM_BasicFun.BindCklShowHiddenItems("OrgView", cklHiddenShow);
                this.GetRead();
            }
            else
            {
                this.GetListName();
                ViewState["CurrentUCPaging"] = "UCPagingOrg";
                this.InitVar(UCPagingOrg, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER", "dbo.f_formatstr(" + ddlSort.SelectedValue.ToString() + ", '.')", ViewState["Org"].ToString(), 0, 30);
            }
        }

        /// <summary>
        /// 页面信息初始化
        /// </summary>
        protected void PageInit()
        {

            ViewState["TaskID"] = Request.QueryString["TaskID"].ToString();
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
        }
        /// <summary>
        /// 初始化表名
        /// </summary>
        private void GetListName()
        {
            #region
            switch (hdfType.Value)
            {
                case "回转窑":
                    viewtable = "View_TM_HZY";
                    tablename = "TBPM_STRINFOHZY";
                    mptable = "TBPM_MPFORHZY";
                    mstable = "TBPM_MSOFHZY";
                    break;
                case "球、立磨":
                    viewtable = "View_TM_QLM";
                    tablename = "TBPM_STRINFOQLM";
                    mptable = "TBPM_MPFORHZY";
                    mstable = "TBPM_MSOFQLM";
                    break;
                case "篦冷机":
                    viewtable = "View_TM_BLJ";
                    tablename = "TBPM_STRINFOBLJ";
                    mptable = "TBPM_MPFORHZY";
                    mstable = "TBPM_MSOFBLJ";
                    break;
                case "堆取料机":
                    viewtable = "View_TM_DQLJ";
                    tablename = "TBPM_STRINFODQLJ";
                    mptable = "TBPM_MPFORHZY";
                    mstable = "TBPM_MSOFDQJ";
                    break;
                case "钢结构及非标":
                    viewtable = "View_TM_GFB";
                    tablename = "TBPM_STRINFOGFB";
                    mptable = "TBPM_MPFORHZY";
                    mstable = "TBPM_MSOFGFB";
                    break;
                case "电气及其他":
                    viewtable = "View_TM_DQO";
                    tablename = "TBPM_STRINFODQO";
                    mptable = "TBPM_MPFORHZY";
                    mstable = "TBPM_MSOFDQO";
                    break;
                default: break;
            }
            #endregion
        }
        /// <summary>
        /// 查看标签数据，双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _xuhao = e.Row.Cells[6].Text.Trim();

                HiddenField hdforgstate = (HiddenField)e.Row.FindControl("hdfOrgState");

                string url = e.Row.Cells[6].Text.Trim() + ' ' + hdfEngid.Value.Trim() + ' ' + tablename + ' ' + mptable + ' ' + mstable + ' ' + viewtable;
                for (int i = 9; i < 20; i++)
                {
                    e.Row.Cells[i].Attributes["style"] = "Cursor:hand";

                    e.Row.Cells[i].Attributes.Add("title", "双击修改原始数据");

                    e.Row.Cells[i].Attributes["onclick"] = String.Format("javascript:setTimeout(\"if(dbl_click){{dbl_click=false;}}else{{{0}}};\", 1000000000000000);", ClientScript.GetPostBackEventReference(GridView1, "Select$" + e.Row.RowIndex.ToString(), true));
                    // 双击，设置 dbl_click=true，以取消单击响应
                    e.Row.Cells[i].Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLink('" + url + "');", GridView1.DataKeys[e.Row.RowIndex].Value.ToString());

                }
                //标签颜色
                string[] color = hdforgstate.Value.Split('-');//状态-变更状态
                //材料计划
                #region
                if (color[1].ToString() == "1")
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Gray;
                    if (color[0].ToString() == "1")
                    {
                        e.Row.Cells[2].Text = "Y";
                        e.Row.Cells[2].Attributes.Add("title", "删除-材料计划已提交");
                    }
                    else
                    {
                        e.Row.Cells[2].Attributes.Add("title", "删除-材料计划未提交");
                    }
                }
                else if (color[1].ToString() == "2")
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Orange;
                    if (color[0].ToString() == "1")
                    {
                        e.Row.Cells[2].Text = "Y";
                        e.Row.Cells[2].Attributes.Add("title", "增加-材料计划已提交");
                    }
                    else
                    {
                        e.Row.Cells[2].Attributes.Add("title", "增加-材料计划未提交");
                    }
                }
                else if (color[1].ToString() == "3")
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                    if (color[0].ToString() == "1")
                    {
                        e.Row.Cells[2].Text = "Y";
                        e.Row.Cells[2].Attributes.Add("title", "修改-材料计划已提交");
                    }
                    else
                    {
                        e.Row.Cells[2].Attributes.Add("title", "修改-材料计划未提交");
                    }
                }
                else if (color[0].ToString() == "1")
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[2].Attributes.Add("title", "正常-材料计划已提交");
                }
                //添加下查计划项
                //////////////string xuhao_engid = _xuhao + '_' +hdfEngid.Value.Trim();
                //////////////e.Row.Cells[2].Attributes["style"] = "Cursor:hand";
                //////////////e.Row.Cells[2].Attributes.Add("title", "双击下查材料计划");
                //////////////e.Row.Cells[2].Attributes.Add("ondblclick", "MP_DownWardQuery('" + xuhao_engid + "')");
                #endregion
                //制作明细
                #region
                if (color[3].ToString() == "1")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Gray;
                    if (color[2].ToString() == "1")
                    {
                        e.Row.Cells[3].Text = "Y";
                        e.Row.Cells[3].Attributes.Add("title", "删除-制作明细已提交");
                    }
                    else
                    {
                        e.Row.Cells[3].Attributes.Add("title", "删除-制作明细未提交");

                    }
                }
                else if (color[3].ToString() == "2")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Orange;
                    if (color[2].ToString() == "1")
                    {
                        e.Row.Cells[3].Text = "Y";
                        e.Row.Cells[3].Attributes.Add("title", "增加-制作明细已提交");
                    }
                    else
                    {
                        e.Row.Cells[3].Attributes.Add("title", "增加-制作明细未提交");

                    }
                }
                else if (color[3].ToString() == "3")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                    if (color[2].ToString() == "1")
                    {
                        e.Row.Cells[3].Text = "Y";
                        e.Row.Cells[3].Attributes.Add("title", "修改-制作明细已提交");
                    }
                    else
                    {
                        e.Row.Cells[3].Attributes.Add("title", "修改-制作明细未提交");
                    }
                }
                else if (color[2].ToString() == "1")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[3].Attributes.Add("title", "正常-制作明细已提交");
                }
                ////////////////string xuhao_engid_table = _xuhao + ',' + hdfEngid.Value.Trim() + ',' + mstable;
                ////////////////e.Row.Cells[3].Attributes["style"] = "Cursor:hand";
                ////////////////e.Row.Cells[3].Attributes.Add("title", "双击下查材料计划");
                ////////////////e.Row.Cells[3].Attributes.Add("ondblclick", "MS_DownWardQuery('" + xuhao_engid_table + "')");
                #endregion
                //外协
                #region
                if (color[5].ToString() == "1")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Gray;
                    if (color[5].ToString() == "1")
                    {
                        e.Row.Cells[4].Text = "Y";
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "删除-外协(采购)已提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "删除-外协(技术)已提交");
                        }
                    }
                    else
                    {
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "删除-外协(采购)未提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "删除-外协(技术)未提交");
                        }
                    }
                }
                else if (color[5].ToString() == "2")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Orange;
                    if (color[5].ToString() == "1")
                    {
                        e.Row.Cells[4].Text = "Y";
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "增加-外协(采购)已提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "增加-外协(技术)已提交");
                        }
                    }
                    else
                    {
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "增加-外协(采购)未提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "增加-外协(技术)未提交");
                        }
                    }
                }
                else if (color[5].ToString() == "3")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                    if (color[5].ToString() == "1")
                    {
                        e.Row.Cells[4].Text = "Y";
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "修改-外协(采购)已提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "修改-外协(技术)已提交");
                        }
                    }
                    else
                    {
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "修改-外协(采购)未提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "修改-外协(技术)未提交");
                        }
                    }
                }
                else if (color[4].ToString() == "1")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
                    if (color[6].ToString() == "06")
                    {
                        e.Row.Cells[4].Attributes.Add("title", "正常-外协(采购)已提交");
                    }
                    else if (color[6].ToString() == "03")
                    {
                        e.Row.Cells[4].Attributes.Add("title", "正常-外协(技术)已提交");
                    }
                }
                ////////////////e.Row.Cells[4].Attributes["style"] = "Cursor:hand";
                ////////////////e.Row.Cells[4].Attributes.Add("title", "双击下查材料计划");
                ////////////////e.Row.Cells[4].Attributes.Add("ondblclick", "OUT_DownWardQuery('" + xuhao_engid + "')");
                #endregion

                //含物料编码项，查找代用
                #region
                //////string _marid = e.Row.Cells[8].Text.Trim();
                //////if (_marid != "" || _marid != "&nbsp;")
                //////{
                //////    //获取物料对应的计划跟踪号
                //////    string sql_findtracknum = "";
                //////    string _replacefor_xuhao = _xuhao + "," + hdfEngid.Value.Trim() + "," + _marid;
                //////    if (color[0] != "0" || color[1] != "0")//表明提交计划类型为材料计划
                //////    {
                //////        _replacefor_xuhao += "," + "View_TM_MPHZY";
                //////        sql_findtracknum = "select MP_TRACKNUM from View_TM_MPHZY where MP_NEWXUHAO='" + _xuhao + "' and MP_MARID='" + _marid + "' and MP_STATERV='8' AND MP_STATUS='0'";
                //////    }
                //////    else if (color[4] != "0" || color[5] != "0")//表明计划类型为外协计划
                //////    {
                //////        _replacefor_xuhao += "," + "View_TM_OUTSOURCELIST";
                //////        sql_findtracknum = "select OSL_TRACKNUM from View_TM_OUTSOURCELIST where OSL_NEWXUHAO='" + _xuhao + "' and OSL_MARID='" + _marid + "' and OST_STATE='8' AND OSL_STATUS='0'";
                //////    }

                //////    if (sql_findtracknum != "")
                //////    {
                //////        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_findtracknum);
                //////        if (dt.Rows.Count > 0)
                //////        {
                //////            //从代用表中查询是否有代用计划
                //////            string tracknum = dt.Rows[0][0].ToString();
                //////            string sql_findmarreplace = "SELECT marid from View_TBPC_MARREPLACE_total_all_detail where ptcode='" + tracknum + "'";
                //////            SqlDataReader dr_sql_findmarreplace = DBCallCommon.GetDRUsingSqlText(sql_findmarreplace);
                //////            if (dr_sql_findmarreplace.HasRows)
                //////            {
                //////                e.Row.Cells[8].BackColor = System.Drawing.Color.YellowGreen;
                //////                e.Row.Cells[8].ToolTip = "双击查看代用计划";
                //////            }
                //////        }
                //////        e.Row.Cells[8].Attributes["style"] = "Cursor:hand";
                //////        e.Row.Cells[8].Attributes.Add("ondblclick", "MarReplace_DownWardQuery('" + _replacefor_xuhao + "')");

                //////    }
                //////}
                #endregion

                //含物料编码不提计划项标识
                if (color[7].ToString() != "" && color[8] == "N")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[1].Attributes.Add("title", "不提材料计划");
                }
            }
        }
        /// <summary>
        /// 查看标签,读取数据
        /// </summary>
        private void GetRead()
        {
            this.GetListName();
            string sql = "1=2";
            ViewState["Org"] = sql;
            UCPagingOrg.CurrentPage = 1;
            this.InitVar(UCPagingOrg, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER", "dbo.f_formatstr(" + ddlSort.SelectedValue.ToString() + ", '.')", ViewState["Org"].ToString(), 0, 30);
            this.bindGrid(UCPagingOrg, GridView1, NoDataPanel1);
            ViewState["CurrentUCPaging"] = "UCPagingOrg";
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOrgQuery_OnClick(object sender, EventArgs e)
        {
            this.GetListName();
            StringBuilder strb_sql = new StringBuilder();
            strb_sql.Append(" BM_ENGID='" + hdfEngid.Value.Trim() + "'");

            
            if (ddlQueryType.SelectedIndex != 0)
            {
                if (txtMCorZX.Text.Trim() != "")
                {
                    if (ddlQueryType.SelectedValue == "BM_ZONGXU" || ddlQueryType.SelectedValue == "BM_XUHAO")
                    {
                        strb_sql.Append(" and (" + ddlQueryType.SelectedValue.ToString() + "='" + txtMCorZX.Text.Trim() + "' OR " + ddlQueryType.SelectedValue.ToString() + " like '" + txtMCorZX.Text.Trim() + ".%')");
                    }
                    else
                    {
                        strb_sql.Append(" and " + ddlQueryType.SelectedValue.ToString() + " like '%" + txtMCorZX.Text.Trim() + "%'");
                    }
                }
                else
                {
                    strb_sql.Append(" and " + ddlQueryType.SelectedValue.ToString() + "='" + txtMCorZX.Text.Trim() + "'");
                }
            }

            //操作的字段
            if (ddlRepType.SelectedValue == "BM_TUHAO")
            {
                strb_sql.Append(" AND BM_MSSTATE='0' AND BM_OSSTATE='0'");
            }
            else if (ddlRepType.SelectedValue == "BM_MASHAPE" || ddlRepType.SelectedValue == "BM_MASTATE")
            {
                strb_sql.Append(" AND BM_MPSTATE='0' AND BM_OSSTATE='0' AND BM_MSSTATE='0'");
            }

            udqMS.ExistedConditions = strb_sql.ToString();
            ViewState["Org"] = strb_sql.ToString() + UserDefinedQueryConditions.ReturnQueryString((GridView)udqMS.FindControl("GridView1"), (Label)udqMS.FindControl("Label1"));
            UCPagingOrg.CurrentPage = 1;
            this.InitVar(UCPagingOrg, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER", "dbo.f_formatstr(" + ddlSort.SelectedValue.ToString() + ", '.')", ViewState["Org"].ToString(), 0, 30);
            this.bindGrid(UCPagingOrg, GridView1, NoDataPanel1);
            ViewState["CurrentUCPaging"] = "UCPagingOrg";
        }

        /// <summary>
        /// 列的显示与隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cklHiddenShow_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string strCheck = Request.Form["__EVENTTARGET"].ToString();

            int Index = Convert.ToInt16(strCheck.Substring(strCheck.LastIndexOf("$") + 1));

            TM_BasicFun.HiddenShowColumn(GridView1, cklHiddenShow, Index, "OrgView");
        }
         /// <summary>
        /// BUTTON重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqMS.FindControl("GridView1"));
            ddlQueryType.SelectedIndex = 0;
            txtMCorZX.Text = "";
        }

        protected void btnClear_Ud(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqMS.FindControl("GridView1"));
        }

        protected void ddlReplaceType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReplaceType.SelectedValue == "Replace")
            {
                replace.Visible = true;
                add.Visible = false;
                btnSave.Text = "替 换";
                btnSave.CommandName = "Replace";
            }
            else if (ddlReplaceType.SelectedValue == "Add")
            {
                replace.Visible = false;
                add.Visible = true;
                btnSave.Text = "追 加";
                btnSave.CommandName = "Add";
            }
        }

        protected void btnSave_Replace(object sender, EventArgs e)
        {
            string sqltext;
            if (((Button)sender).CommandName == "Replace")
            {
                sqltext = "update " + tablename + " set " + ddlRepType.SelectedValue + "=REPLACE(" + ddlRepType.SelectedValue + ",'" + txtStart.Text.Trim() + "','" + txtEnd.Text.Trim() + "') WHERE " + ViewState["Org"].ToString() + "";
                
                string conStr = DBCallCommon.GetStringValue("connectionStrings");
                SqlConnection conn = new SqlConnection(conStr);
                SqlCommand sc = new SqlCommand();
                sc.CommandType = CommandType.Text;
                sc.Connection = conn;
                sc.CommandText = sqltext;
                DBCallCommon.openConn(conn);
                int rowEffected = sc.ExecuteNonQuery();
                DBCallCommon.closeConn(conn);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('替换成功！！！\\r\\r影响行数【"+rowEffected+"】');", true);
            }
            else if (((Button)sender).CommandName == "Add")
            {
                sqltext = "update " + tablename + " set " + ddlRepType.SelectedValue + "=" + ddlRepType.SelectedValue + "+'  "+txtAdd.Text.Trim()+"' WHERE " + ViewState["Org"].ToString() + "";
                
                string conStr = DBCallCommon.GetStringValue("connectionStrings");
                SqlConnection conn = new SqlConnection(conStr);
                SqlCommand sc = new SqlCommand();
                sc.CommandType = CommandType.Text;
                sc.Connection = conn;
                sc.CommandText = sqltext;
                DBCallCommon.openConn(conn);
                int rowEffected = sc.ExecuteNonQuery();
                DBCallCommon.closeConn(conn);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('追加成功！！！\\r\\r影响行数【"+rowEffected+"】');", true);
            }
        }

        #region  分页  UCPaging

        PagerQueryParam pager_org = new PagerQueryParam();

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar(UCPaging ParamUCPaging, string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, int _ordertype, int _pagesize)
        {
            InitPager(_tablename, _primarykey, _showfields, _orderfield, _strwhere, _ordertype, _pagesize);
            ParamUCPaging.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            ParamUCPaging.PageSize = pager_org.PageSize; //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, int _ordertype, int _pagesize)
        {
            pager_org.TableName = _tablename;
            pager_org.PrimaryKey = _primarykey;
            pager_org.ShowFields = _showfields;
            pager_org.OrderField = _orderfield;
            pager_org.StrWhere = _strwhere;
            pager_org.OrderType = _ordertype; //升序排列
            pager_org.PageSize = _pagesize;
        }

        void Pager_PageChanged(int pageNumber)
        {
            Control[] CRL = this.BindGridParamsRecord(ViewState["CurrentUCPaging"].ToString());
            bindGrid((UCPaging)CRL[0], (GridView)CRL[1], (Panel)CRL[2]);
        }

        private void bindGrid(UCPaging ParamUCPaging, GridView ParamGridView, Panel ParamPanel)
        {
            pager_org.PageIndex = ParamUCPaging.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
            CommonFun.Paging(dt, ParamGridView, ParamUCPaging, ParamPanel);
            if (ParamPanel.Visible)
            {
                ParamUCPaging.Visible = false;
            }
            else
            {
                ParamUCPaging.Visible = true;
                ParamUCPaging.InitPageInfo();  //分页控件中要显示的控件
            }

        }

        /// <summary>
        /// 当前UCPaging
        /// </summary>
        /// <param name="_UCPaging"></param>
        /// <returns></returns>
        protected Control[] BindGridParamsRecord(string _UCPaging)
        {
            Control[] contrl = new Control[3];
            switch (_UCPaging)
            {
                default:
                    contrl[0] = (UCPaging)UCPagingOrg;//原始数据
                    contrl[1] = (GridView)GridView1;
                    contrl[2] = (Panel)NoDataPanel1;
                    break;
            }
            return contrl;
        }
        #endregion
    }
}
