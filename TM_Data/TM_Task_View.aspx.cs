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
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Task_View : System.Web.UI.Page
    {

        #region
        string sqlText;
        string tsa_id;
        string viewtable = "View_TM_DQO";
        string tablename = "TBPM_STRINFODQO";
        string mptable = "TBPM_MPPLAN";
        string mstable = "TBPM_MKDETAIL";
        string xuhao;
        string tuhao;
        string marid;
        string zongxu;
        string cailiaoname;
        string cailiaoguige;
        float cailiaozongzhong;
        string caizhi;
        double shuliang;
        string labunit;
        string keycoms;
        string mp_pici;
        string category;
        string influence;
        string yongliang;
        string techunit;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                this.InitInfo();
                TM_BasicFun.BindCklShowHiddenItems("OrgView", cklHiddenShow);
            }
            else if (IsPostBack)
            {
                this.PagePostBack();
            }
        }
        /// <summary>
        /// 页面回发分页初始化
        /// </summary>
        private void PagePostBack()
        {

            string tapindex = TabContainer1.ActiveTab.ID.ToString();
            switch (tapindex)
            {
                case "TabPanel2"://材料计划
                    ViewState["CurrentUCPaging"] = "UCPagingMP";

                    this.InitVar(UCPagingMP, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) AS NUMBER", "dbo.f_formatstr(BM_XUHAO, '.')", ViewState["Mp"].ToString(), 0, Convert.ToInt16(ViewState["mpPageSize"]));
                    break;
                case "TabPanel3":
                    ViewState["CurrentUCPaging"] = "UCPagingMS";
                    this.InitVarMS();
                    break;
                default:
                    ViewState["CurrentUCPaging"] = "UCPagingOrg";
                    this.InitVar(UCPagingOrg, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["Org"].ToString(), 0, 20);
                    break;
            }
        }
        /// <summary>
        /// 初始化页面(基本信息)
        /// </summary>
        private void InitInfo()
        {
            tsa_id = Request.QueryString["action"];
            sqlText = "select TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID,TSA_TCCLERK,CM_PROJ ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;
                lab_proname.Text = dr["CM_PROJ"].ToString();
                labprostru.Text = dr["TSA_ENGNAME"].ToString() + "设计BOM表";
                lab_hetongNum.Text = dr["TSA_PJID"].ToString();
                ViewState["TSA_TCCLERK"] = dr["TSA_TCCLERK"].ToString();
            }
            dr.Close();


            this.OrgDropDownListBind();
            this.MpDropDownListBind();
            this.MsDropDownListBind();

            this.GetRead();
            this.GetMP();

            this.GetMS();
            ViewState["CurrentUCPaging"] = "UCPagingOrg";
            this.Form.DefaultButton = btnOrgQuery.UniqueID;
            this.InitTreeView("");
            this.InitTreeViewMp("");
        }

        #region 原始数据标签
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

                string url = e.Row.Cells[6].Text.Trim() + ' ' + tsaid.Text + ' ' + tablename + ' ' + mptable + ' ' + mstable + ' ' + viewtable;
                if (ViewState["TSA_TCCLERK"].ToString() == Session["UserID"].ToString())
                {
                    for (int i = 9; i < 20; i++)
                    {
                        e.Row.Cells[i].Attributes["style"] = "Cursor:hand";

                        e.Row.Cells[i].Attributes.Add("title", "勾选修改原始数据");

                        //e.Row.Cells[i].Attributes["onclick"] = String.Format("javascript:setTimeout(\"if(dbl_click){{dbl_click=false;}}else{{{0}}};\", 1000000000000000);", ClientScript.GetPostBackEventReference(GridView1, "Select$" + e.Row.RowIndex.ToString(), true));
                        // 双击，设置 dbl_click=true，以取消单击响应
                        //e.Row.Cells[i].Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLink('" + url + "');", GridView1.DataKeys[e.Row.RowIndex].Value.ToString());

                    }
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
                string xuhao_engid = _xuhao + '_' + tsaid.Text.Trim();
                e.Row.Cells[2].Attributes["style"] = "Cursor:hand";
                e.Row.Cells[2].Attributes.Add("title", "双击下查材料计划");
                e.Row.Cells[2].Attributes.Add("ondblclick", "MP_DownWardQuery('" + xuhao_engid + "')");
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
                string xuhao_engid_table = _xuhao + ',' + tsaid.Text.Trim() + ',' + mstable;
                e.Row.Cells[3].Attributes["style"] = "Cursor:hand";
                e.Row.Cells[3].Attributes.Add("title", "双击下查明细");
                e.Row.Cells[3].Attributes.Add("ondblclick", "MS_DownWardQuery('" + xuhao_engid_table + "')");
                #endregion

                //含物料编码项，查找代用
                #region
                string _marid = e.Row.Cells[5].Text.Trim();
                if (_marid != "" && _marid != "&nbsp;")   //原来是||，此处更改为&&
                {
                    //获取物料对应的计划跟踪号
                    string sql_findtracknum = "";
                    string _replacefor_xuhao = _xuhao + "," + tsaid.Text.Trim() + "," + _marid;
                    if (color[0] != "0" || color[1] != "0")//表明提交计划类型为材料计划
                    {
                        _replacefor_xuhao += "," + "View_TM_MPPLAN";
                        sql_findtracknum = "select MP_TRACKNUM from View_TM_MPPLAN where MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_NEWXUHAO='" + _xuhao + "' and MP_MARID='" + _marid + "' and MP_STATERV='8' AND MP_STATUS='0'";
                    }

                    if (sql_findtracknum != "")
                    {
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_findtracknum);
                        if (dt.Rows.Count > 0)
                        {
                            //从代用表中查询是否有代用计划
                            string tracknum = dt.Rows[0][0].ToString();
                            string sql_findmarreplace = "SELECT marid from View_TBPC_MARREPLACE_total_all_detail where ptcode='" + tracknum + "'";
                            SqlDataReader dr_sql_findmarreplace = DBCallCommon.GetDRUsingSqlText(sql_findmarreplace);
                            if (dr_sql_findmarreplace.HasRows)
                            {
                                e.Row.Cells[5].BackColor = System.Drawing.Color.YellowGreen;
                                e.Row.Cells[5].ToolTip = "双击查看代用计划";
                            }
                        }
                        e.Row.Cells[5].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[5].Attributes.Add("ondblclick", "MarReplace_DownWardQuery('" + _replacefor_xuhao + "')");

                    }
                }
                #endregion
                //含物料编码不提计划项标识
                #region
                if (color[4].ToString() != "" && color[5] == "N")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[1].Attributes.Add("title", "不提材料计划");
                }
                if (color[6].ToString() == "N")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[1].Attributes.Add("title", "不提制作明细");

                }
                #endregion
            }
        }

        /// <summary>
        /// 修改原始数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_OnClick(object sender, EventArgs e)
        {

            string EditZongXu = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                string marid = gRow.Cells[5].Text.Trim();
                string zongxu = gRow.Cells[6].Text.Trim();
                if (chk.Checked)
                {
                    //if (!(marid == "" || marid == "&nbsp;"))
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选部件进行数据修改');", true);
                    //    return;
                    //}
                    sqlText = "select count(1) from View_TM_DQO where BM_ENGID='" + tsaid.Text.Trim() + "'and (BM_ZONGXU='" + zongxu + "' or BM_ZONGXU like '" + zongxu + ".%') and  (BM_MPSTATE<>0 or BM_MPSTATUS<>0 or BM_MSSTATE<>0 or BM_MSSTATUS<>0)";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        EditZongXu += zongxu + ",";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该记录【" + zongxu + "】存在已提交或已变更的记录');", true);
                    }

                }

            }
            if (EditZongXu.Length > 1)
            {
                if (EditZongXu.Trim(',').Split(',').Length > 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能勾选一条记录');", true);
                }
                else
                {
                    EditZongXu = EditZongXu.Trim(',');
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "EditData('" + tsaid.Text + "','" + EditZongXu + "')", true);
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选一条记录');", true);
            }
        }
        /// <summary>
        /// 明细变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton2_OnClick(object sender, EventArgs e)
        {

            string EditZongXu = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                string marid = gRow.Cells[5].Text.Trim();
                string zongxu = gRow.Cells[6].Text.Trim();
                if (chk.Checked)
                {
                    //if (!(marid == "" || marid == "&nbsp;"))
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选部件进行数据修改');", true);
                    //    return;
                    //}
                    //if (zongxu == "1")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勿勾选总序为【1】的部件');", true);
                    //    return;
                    //}
                    //sqlText = "select count(1) from View_TM_DQO where BM_ENGID='" + tsaid.Text.Trim() + "'and (BM_ZONGXU like '" + zongxu + ".%') and (BM_MPSTATE='0' and BM_MPSTATUS='0') and BM_WMARPLAN='Y' and BM_MARID<>''";
                    //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    //if (dt.Rows[0][0].ToString() != "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该部件【" + zongxu + "】应全部提交材料计划才可执行变更！');", true);
                    //    return;
                    //}
                    //sqlText = "select count(1) from View_TM_DQO where BM_ENGID='" + tsaid.Text.Trim() + "'and (BM_ZONGXU='" + zongxu + "' or BM_ZONGXU like '" + zongxu + ".%') and (BM_MSSTATE='0' and BM_ISMANU='Y' and BM_MSSTATUS='0')";
                    //dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    //if (dt.Rows[0][0].ToString() != "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该部件【" + zongxu + "】应全部提交制作明细才可执行变更！');", true);
                    //    return;
                    //}

                    //sqlText = "select count(1) from View_TM_DQO where BM_ENGID='" + tsaid.Text.Trim() + "'and (BM_ZONGXU='" + zongxu + "' or BM_ZONGXU like '" + zongxu + ".%') and  (BM_MSREVIEW ='1' or BM_MPREVIEW='1')";
                    //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    //if (dt.Rows[0][0].ToString() != "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该部件【" + zongxu + "】下有记录正在审核，无法执行变更');", true);
                    //}
                    //else
                    //{
                    //    EditZongXu += zongxu + ",";
                    //}

                    EditZongXu += zongxu + ",";
                }

            }
            if (EditZongXu.Length > 1)
            {
                if (EditZongXu.Trim(',').Split(',').Length > 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能勾选一条记录');", true);
                }
                else
                {
                    EditZongXu = EditZongXu.Trim(',');
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "ChangeData('" + tsaid.Text.Trim() + "','" + EditZongXu + "')", true);
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选一条记录');", true);
            }
        }

        /// <summary>
        /// 变更取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton3_OnClick(object sender, EventArgs e)
        {

            string EditZongXu = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                string marid = gRow.Cells[5].Text.Trim();
                string zongxu = gRow.Cells[6].Text.Trim();
                if (chk.Checked)
                {

                    //sqlText = "select count(1) from View_TM_DQO where BM_ENGID='" + tsaid.Text.Trim() + "'and (BM_ZONGXU like '" + zongxu + ".%') and (BM_MPSTATE='0' and BM_WMARPLAN='Y')";
                    //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    //if (dt.Rows[0][0].ToString() != "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该部件【" + zongxu + "】应全部提交材料计划才可执行变更！');", true);
                    //    return;
                    //}
                    //sqlText = "select count(1) from View_TM_DQO where BM_ENGID='" + tsaid.Text.Trim() + "'and (BM_ZONGXU='" + zongxu + "' or BM_ZONGXU like '" + zongxu + ".%') and (BM_MSSTATE='0' and BM_ISMANU='Y' and BM_MSSTATUS='0')";
                    //dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    //if (dt.Rows[0][0].ToString() != "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该部件【" + zongxu + "】应全部提交制作明细才可执行变更！');", true);
                    //    return;
                    //}

                    //sqlText = "select count(1) from View_TM_DQO where BM_ENGID='" + tsaid.Text.Trim() + "'and (BM_ZONGXU='" + zongxu + "' or BM_ZONGXU like '" + zongxu + ".%') and  (BM_MSREVIEW ='1' or BM_MPREVIEW='1')";
                    //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    //if (dt.Rows[0][0].ToString() != "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该部件【" + zongxu + "】下有记录正在审核，无法执行变更');", true);
                    //}
                    //else
                    //{
                        EditZongXu += zongxu + ",";
                    //}


                }

            }
            if (EditZongXu.Length > 1)
            {
                if (EditZongXu.Trim(',').Split(',').Length > 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能勾选一条记录');", true);
                }
                else
                {
                    EditZongXu = EditZongXu.Trim(',');
                    sqlText = "update TBPM_STRINFODQO set BM_MSSTATE='0',BM_MSSTATUS='1',BM_MSREVIEW='0',BM_MPSTATE='0',BM_MPREVIEW='0',BM_MPSTATUS='1' where BM_ENGID='" + tsaid.Text.Trim() + "' and (BM_ZONGXU='" + EditZongXu + "' or BM_ZONGXU like '" + EditZongXu + ".%') ";
                    DBCallCommon.ExeSqlText(sqlText);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更成功，请继续执行下步操作');window.location.reload();", true);/////window.location.reload();
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选一条记录');", true);
            }
        }



        /// <summary>
        /// 单条变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton4_OnClick(object sender, EventArgs e)
        {

            string EditZongXu = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                string marid = gRow.Cells[5].Text.Trim();
                string zongxu = gRow.Cells[6].Text.Trim();
                if (chk.Checked)
                {

                    EditZongXu += zongxu + ",";
                }

            }
            if (EditZongXu.Length > 1)
            {
                if (EditZongXu.Trim(',').Split(',').Length > 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能勾选一条记录');", true);
                }
                else
                {
                    EditZongXu = EditZongXu.Trim(',');
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "ChangeOneData('" + tsaid.Text.Trim() + "','" + EditZongXu + "')", true);
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选一条记录');", true);
            }
        }

        /// <summary>
        /// 查看标签,读取数据
        /// </summary>
        private void GetRead()
        {

            string sql = "BM_ENGID='" + tsaid.Text + "'";
            ViewState["Org"] = sql;
            UCPagingOrg.CurrentPage = 1;
            this.InitVar(UCPagingOrg, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["Org"].ToString(), 0, 20);
            this.bindGrid(UCPagingOrg, GridView1, NoDataPanel1);
            ViewState["CurrentUCPaging"] = "UCPagingOrg";
        }

        #region 查看标签DropDownList绑定
        /// <summary>
        /// 查看标签的DropDownList绑定
        /// </summary>
        private void OrgDropDownListBind()
        {

            this.GetPartsName(ddlpartsname);
            this.GetNameData(ddlmatername);
            this.GetGuigeData(ddlguige);
            this.GetCaizhiData(ddlcaizhi);
            this.GetOrgShape(ddlOrgShape);


        }
        #endregion

        /// <summary>
        /// 查看标签中查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlmatername_SelectedIndexChanged(object sender, EventArgs e)
        {

            StringBuilder strb_sql = new StringBuilder();
            strb_sql.Append(" BM_ENGID='" + tsaid.Text + "'");

            if (ddlWMarPlan.SelectedIndex != 0)
            {
                strb_sql.Append(" and BM_WMARPLAN='" + ddlWMarPlan.SelectedValue + "' and BM_MARID!=''");
            }

            if (ddlpartsname.SelectedIndex != 0)//部件
            {
                strb_sql.Append(" AND BM_XUHAO like '" + ddlpartsname.SelectedValue.ToString() + "%'");
            }

            if (ddlmatername.SelectedIndex != 0)//物料名称
            {
                strb_sql.Append(" AND BM_MANAME='" + ddlmatername.SelectedValue.ToString() + "'");
            }
            if (ddlguige.SelectedIndex != 0)//规格
            {
                strb_sql.Append(" AND BM_MAGUIGE='" + ddlguige.SelectedValue.ToString() + "'");
            }

            if (ddlcaizhi.SelectedIndex != 0)//材质
            {
                strb_sql.Append(" AND BM_MAQUALITY='" + ddlcaizhi.SelectedValue.ToString() + "'");
            }

            if (ddlOrgFix.SelectedIndex != 0)//是否定尺
            {
                strb_sql.Append(" AND BM_FIXEDSIZE='" + ddlOrgFix.SelectedValue.ToString() + "'");
            }

            if (ddlOrgInMS.SelectedIndex != 0)//是否体现明细
            {
                strb_sql.Append(" AND BM_ISMANU='" + ddlOrgInMS.SelectedValue.ToString() + "'");
            }

            //if (ddlOrgKey.SelectedIndex != 0)//关键部件
            //{
            //    strb_sql.Append(" AND BM_KEYCOMS='" + ddlOrgKey.SelectedValue.ToString() + "'");
            //}
            //if (ddlOrgState.SelectedIndex != 0)//毛坯状态
            //{
            //    strb_sql.Append(" AND BM_MASTATE='" + ddlOrgState.SelectedValue.ToString() + "'");
            //}
            if (ddlOrgShape.SelectedIndex != 0)//毛坯形状
            {
                strb_sql.Append(" AND BM_MASHAPE='" + ddlOrgShape.SelectedValue.ToString() + "'");
            }





            if (ddlOrgMPState.SelectedIndex != 0)//材料计划
            {
                if (ddlOrgMPState.SelectedValue == "WSC")
                {
                    strb_sql.Append(" AND BM_MPSTATE='0' AND BM_MPREVIEW='0' and BM_MARID!='' AND BM_OSSTATE='0' AND BM_WMARPLAN='Y' ");
                }
                else if (ddlOrgMPState.SelectedValue == "WTJ")
                {
                    strb_sql.Append(" AND BM_MPSTATE='1' AND BM_MPREVIEW='0' and BM_MARID!='' AND BM_OSSTATE='0' AND BM_WMARPLAN='Y'");
                }
                else if (ddlOrgMPState.SelectedValue == "SSZ")
                {
                    strb_sql.Append(" AND BM_MPSTATE='1' AND BM_MPREVIEW='1' and BM_MARID!='' AND BM_OSSTATE='0' AND BM_WMARPLAN='Y'");
                }
                else if (ddlOrgMPState.SelectedValue == "TG")
                {
                    strb_sql.Append(" AND BM_MPSTATE='1' AND BM_MPREVIEW='3' and BM_MARID!='' AND BM_OSSTATE='0' AND BM_WMARPLAN='Y'");
                }
                else if (ddlOrgMPState.SelectedValue == "BH")
                {
                    strb_sql.Append(" AND BM_MPREVIEW='2' and BM_MARID!='' AND BM_OSSTATE='0' AND BM_WMARPLAN='Y'");
                }
            }
            if (ddlOrgMSState.SelectedIndex != 0)//制作明细
            {
                if (ddlOrgMSState.SelectedValue == "WSC")
                {
                    strb_sql.Append(" AND BM_MSSTATE='0' AND BM_MSREVIEW='0'");
                }
                else if (ddlOrgMSState.SelectedValue == "SSZ")
                {
                    strb_sql.Append(" AND BM_MSSTATE='1' AND BM_MSREVIEW='1'");
                }
                else if (ddlOrgMSState.SelectedValue == "TG")
                {
                    strb_sql.Append(" AND BM_MSSTATE='1' AND BM_MSREVIEW='3'");
                }
                else if (ddlOrgMSState.SelectedValue == "BH")
                {
                    strb_sql.Append(" BM_MSREVIEW='2'");
                }
            }

            //if (ddlOrgOUTState.SelectedIndex != 0)//外协
            //{
            //    if (ddlOrgOUTState.SelectedValue == "WSC")
            //    {
            //        strb_sql.Append(" AND BM_OSSTATE='0' AND BM_OSREVIEW='0' and BM_MARID!='' and BM_MPSTATE='0' AND BM_WMARPLAN='Y' ");
            //    }
            //    else if (ddlOrgOUTState.SelectedValue == "WTJ")
            //    {
            //        strb_sql.Append(" AND BM_OSSTATE='1' AND BM_OSREVIEW='0' and BM_MARID!='' and BM_MPSTATE='0' AND BM_WMARPLAN='Y' ");
            //    }
            //    else if (ddlOrgOUTState.SelectedValue == "SSZ")
            //    {
            //        strb_sql.Append(" AND BM_OSSTATE='1' AND BM_OSREVIEW='1' and BM_MARID!='' and BM_MPSTATE='0' AND BM_WMARPLAN='Y' ");
            //    }
            //    else if (ddlOrgOUTState.SelectedValue == "TG")
            //    {
            //        strb_sql.Append(" AND BM_OSSTATE='1' AND BM_OSREVIEW='3' and BM_MARID!='' and BM_MPSTATE='0' AND BM_WMARPLAN='Y' ");
            //    }
            //    else if (ddlOrgOUTState.SelectedValue == "BH")
            //    {
            //        strb_sql.Append(" AND BM_OSREVIEW='2' and BM_MARID!='' and BM_MPSTATE='0' AND BM_WMARPLAN='Y'");
            //    }
            //}
            //拆分记录
            //if (ddlSplitNumber.SelectedIndex != 0)
            //{
            //    if (ddlSplitNumber.SelectedValue == "Y")
            //    {
            //        strb_sql.Append(" and BM_ZONGXU in(select BM_ZONGXU from " + viewtable + " where bm_engid='" + tsaid.Text.Trim() + "'  group by bm_zongxu having  count(bm_zongxu)>1) ");
            //    }
            //    else if (ddlSplitNumber.SelectedValue == "N")
            //    {
            //        strb_sql.Append(" and BM_ZONGXU in(select BM_ZONGXU from " + viewtable + " where bm_engid='" + tsaid.Text.Trim() + "'  group by bm_zongxu having  count(bm_zongxu)=1) ");
            //    }
            //}
            //是否加系数
            if (ddlXishu.SelectedIndex != 0)
            {
                if (ddlXishu.SelectedValue == "N")
                {
                    strb_sql.Append(" AND [BM_MARID]!='' AND dbo.GetMarginCoefficient([BM_MAUNIT],[BM_MAUNITWGHT],[BM_MATOTALWGHT],[BM_MALENGTH],[BM_MATOTALLGTH],[BM_NUMBER],[BM_PNUMBER],[BM_MABGZMY],[BM_MPMY])<1.001");
                }
                else if (ddlXishu.SelectedValue == "Y")
                {
                    strb_sql.Append(" AND [BM_MARID]!='' AND dbo.GetMarginCoefficient([BM_MAUNIT],[BM_MAUNITWGHT],[BM_MATOTALWGHT],[BM_MALENGTH],[BM_MATOTALLGTH],[BM_NUMBER],[BM_PNUMBER],[BM_MABGZMY],[BM_MPMY])>1");
                }
                else if (ddlXishu.SelectedValue == "1.05")
                {
                    strb_sql.Append(" AND [BM_MARID]!='' AND dbo.GetMarginCoefficient([BM_MAUNIT],[BM_MAUNITWGHT],[BM_MATOTALWGHT],[BM_MALENGTH],[BM_MATOTALLGTH],[BM_NUMBER],[BM_PNUMBER],[BM_MABGZMY],[BM_MPMY])>1 AND dbo.GetMarginCoefficient([BM_MAUNIT],[BM_MAUNITWGHT],[BM_MATOTALWGHT],[BM_MALENGTH],[BM_MATOTALLGTH],[BM_NUMBER],[BM_PNUMBER],[BM_MABGZMY],[BM_MPMY]) BETWEEN 1 AND 1.05 ");
                }
                else if (ddlXishu.SelectedValue == "1.051.01")
                {
                    strb_sql.Append(" AND [BM_MARID]!='' AND dbo.GetMarginCoefficient([BM_MAUNIT],[BM_MAUNITWGHT],[BM_MATOTALWGHT],[BM_MALENGTH],[BM_MATOTALLGTH],[BM_NUMBER],[BM_PNUMBER],[BM_MABGZMY],[BM_MPMY]) BETWEEN 1 AND 1.05");
                }
                else if (ddlXishu.SelectedValue == "1.1")
                {
                    strb_sql.Append(" AND [BM_MARID]!='' AND dbo.GetMarginCoefficient([BM_MAUNIT],[BM_MAUNITWGHT],[BM_MATOTALWGHT],[BM_MALENGTH],[BM_MATOTALLGTH],[BM_NUMBER],[BM_PNUMBER],[BM_MABGZMY],[BM_MPMY])>1.11");
                }
            }
            //显示级数
            if (ddlOrgJishu.SelectedIndex != 0)
            {
                strb_sql.Append(" AND [dbo].[Splitnum](BM_ZONGXU,'.')=" + ddlOrgJishu.SelectedValue + " ");
            }

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

            udqcMp.ExistedConditions = strb_sql.ToString();
            ViewState["Org"] = strb_sql.ToString() + UserDefinedQueryConditions.ReturnQueryString((GridView)udqOrg.FindControl("GridView1"), (Label)udqOrg.FindControl("Label1"));
            UCPagingOrg.CurrentPage = 1;
            this.InitVar(UCPagingOrg, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["Org"].ToString(), 0, 20);
            this.bindGrid(UCPagingOrg, GridView1, NoDataPanel1);
            ViewState["CurrentUCPaging"] = "UCPagingOrg";
        }

        /// <summary>
        /// Button查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOrgQuery_OnClick(object sender, EventArgs e)
        {
            this.ddlmatername_SelectedIndexChanged(null, null);
        }

        protected void btnOrgClear_OnClick(object sender, EventArgs e)
        {
            //部件名称
            ddlpartsname.ClearSelection();
            ddlpartsname.Items[0].Selected = true;

            //材料名称
            ddlmatername.ClearSelection();
            ddlmatername.Items[0].Selected = true;

            //材料规格
            ddlguige.ClearSelection();
            ddlguige.Items[0].Selected = true;

            //材料材质
            ddlcaizhi.ClearSelection();
            ddlcaizhi.Items[0].Selected = true;

            //体现明细
            ddlOrgInMS.ClearSelection();
            ddlOrgInMS.Items[0].Selected = true;

            ////材料种类
            //ddlOrgShape.ClearSelection();
            //ddlOrgShape.Items[0].Selected = true;

            //是否定尺
            ddlOrgFix.ClearSelection();
            ddlOrgFix.Items[0].Selected = true;

            //制作明细
            ddlOrgMSState.ClearSelection();
            ddlOrgMSState.Items[0].Selected = true;

            ////材料计划
            //ddlOrgMPState.ClearSelection();
            //ddlOrgMPState.Items[0].Selected = true;

            //余量系数
            ddlXishu.ClearSelection();
            ddlXishu.Items[0].Selected = true;

            //显示级数
            ddlOrgJishu.ClearSelection();
            ddlOrgJishu.Items[0].Selected = true;


            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqOrg.FindControl("GridView1"));
        }
        /// <summary>
        /// BUTTON重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqOrg.FindControl("GridView1"));

            ddlWMarPlan.SelectedIndex = 0;
            ddlQueryType.SelectedIndex = 0;
            txtMCorZX.Text = "";
            ddlpartsname.SelectedIndex = 0;
            ddlmatername.SelectedIndex = 0;
            ddlcaizhi.SelectedIndex = 0;
            ddlguige.SelectedIndex = 0;
            ddlOrgShape.SelectedIndex = 0;
            ddlOrgMPState.SelectedIndex = 0;

            ddlOrgInMS.SelectedIndex = 0;

            ddlOrgFix.SelectedIndex = 0;
            ddlOrgMSState.SelectedIndex = 0;
            this.GetRead();
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该操作！！！');", true);
                return;
            }

            string type = "0";

            // ExportTMDataFromDB.ExportOrgData(tsaid.Text, viewtable, type, ViewState["Org"].ToString());
        }
        /// <summary>
        /// 原始数据删除(未提交)--删除最顶级部件时还存在问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
                return;
            }


            string ret = "";
            string marid = "";
            ViewState["OK"] = "ok";
            string xuhao = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox cbx = (CheckBox)gRow.FindControl("CheckBox1");
                marid = gRow.Cells[5].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[5].Text.Trim();
                if (cbx.Checked)
                {
                    string id = ((HtmlInputHidden)gRow.FindControl("hidBmId")).Value;
                    xuhao = gRow.Cells[6].Text;
                    ret = this.CheckSubmit(xuhao);
                    if (ret == "true")
                    {
                        //删除部件及其子部件
                        //删除原始数据物料表(TBPM_TEMPMARDATA)
                        //调用存储过程
                        string sql = " exec [PRO_TM_Org_Delete] '" + id + "','" + xuhao + "','" + tsaid.Text + "'";
                        DBCallCommon.ExeSqlText(sql);

                    }
                    else
                    {
                        ViewState["OK"] = "breakflag";
                        break;
                    }
                }
            }


            //无法删除的提示
            if (ViewState["OK"].ToString() == "breakflag")
            {
                if (ret == "Submited")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法删除！！！\\r\\r提示:总序【" + xuhao + "】或该序号下记录已提交,只能进行变更删除！');", true);
                }
                else if (ret == "VirtualPart")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('虚拟部件【" + xuhao + "】无法删除！！！');", true);
                }

            }
            else//删除成功提示
            {
                this.btnOrgQuery_OnClick(null, null);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除!');", true);
            }
        }
        /// <summary>
        /// 检查要删除的条目下是否有提交的记录
        /// </summary>
        /// <param name="zx"></param>
        /// <returns></returns>
        private string CheckSubmit(string zx)
        {

            string ret = "true";

            string[] a = tsaid.Text.Split('-');
            string firstCharofZX = "1";
            string pattern = @"^" + firstCharofZX + "\\.0$";
            Regex rgx = new Regex(pattern);
            if (rgx.IsMatch(zx))
            {
                ret = "VirtualPart";
            }
            else
            {
                string sqltext = "select count(*) from " + viewtable + " where BM_ENGID='" + tsaid.Text + "' and (BM_MPSTATE='1' or BM_MSSTATE='1' or BM_OSSTATE='1' or BM_MPSTATUS!='0' or BM_MSSTATUS!='0' or BM_OSSTATE!='0')  and (BM_XUHAO='" + zx + "' or BM_XUHAO like '" + zx + ".%') and (BM_XUHAO='" + zx + "' or BM_XUHAO not like '1.0.%')";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    ret = "Submited";
                }
            }
            return ret;
        }

        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_org_OnClick(object sender, EventArgs e)
        {
            CheckBoxSelectDefine(GridView1, "CheckBox1");

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
        /// 勾选多条是否提材料计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnWmar_Click(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
                return;
            }

            List<string> list_att = new List<string>();
            foreach (GridViewRow grow in GridView1.Rows)
            {
                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    string xuhao = grow.Cells[6].Text.Trim();
                    list_att.Add("update " + tablename + " set  BM_WMARPLAN=(CASE WHEN BM_WMARPLAN='Y' THEN 'N' ELSE 'Y' END) where BM_ENGID='" + tsaid.Text.Trim() + "' and BM_XUHAO='" + xuhao + "' and BM_MPSTATE='0' AND BM_MPSTATUS='0' AND BM_OSSTATE='0' AND BM_OSSTATUS='0'");
                }
            }

            if (list_att.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要操作的项！！！');", true);
                return;
            }

            try
            {
                DBCallCommon.ExecuteTrans(list_att);
                this.btnOrgQuery_OnClick(null, null);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改成功！！！');", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未知错误，请重试或联系管理员！！！');", true);
            }
        }



        #endregion

        #region 材料计划标签
        /// <summary>
        /// 材料计划标签,数据读取
        /// </summary>
        private void GetMP()
        {
            ViewState["mpPageSize"] = "100";
            ViewState["Mp"] = " BM_ENGID='" + tsaid.Text + "' and BM_MPSTATE='0'  and BM_MARID!='' and BM_WMARPLAN='Y'";

            this.BtnNormalChangeVisible("MP");
        }

        #region 材料计划标签DropDownList绑定

        private void MpDropDownListBind()
        {

            this.GetPartsName(ddlbjname);
            this.GetNameData(ddlname);
            this.GetOrgShape(ddlmpxz);

        }
        #endregion

        /// <summary>
        /// 材料计划标签中按类批量选择物料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlname_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpid.Value = "0";//是否勾选项的标志



            StringBuilder strb_sql = new StringBuilder();
            strb_sql.Append(" BM_ENGID='" + tsaid.Text + "'  and BM_MARID!='' and BM_WMARPLAN='Y'");
            //BM_MASTATE not in('半','半退','半正','半调','协A','协B','成交') and 
            if (ddlmptype.SelectedIndex != 0)
            {
                if (ddlmptype.SelectedValue == "非定尺板")
                {
                    strb_sql.Append(" and BM_MASHAPE='板' and BM_FIXEDSIZE='N'");
                }
                else if (ddlmptype.SelectedValue == "定尺板")
                {
                    strb_sql.Append(" and BM_MASHAPE='板' and BM_FIXEDSIZE='Y'");
                }
                else if (ddlmptype.SelectedValue == "型材")
                {
                    strb_sql.Append(" and (BM_MASHAPE='型' or BM_MASHAPE='圆钢')");
                }
                //else if (ddlmptype.SelectedValue == "标(发运)")
                //{
                //    strb_sql.Append(" and BM_KU='库' and BM_MASHAPE='采' and BM_MASTATE='标'");
                //}
                else if (ddlmptype.SelectedValue == "标(组装)")
                {
                    strb_sql.Append(" and (BM_KU!='库' OR BM_KU IS NULL) and BM_MASHAPE='采' and BM_MASTATE='标'");
                }
            }

            if (ddlbjname.SelectedIndex != 0) //部件名称
            {
                strb_sql.Append(" AND (BM_ZONGXU='" + ddlbjname.SelectedValue + "' OR BM_ZONGXU like '" + ddlbjname.SelectedValue.ToString() + ".%')");
            }

            if (ddlname.SelectedIndex != 0)//材料名称
            {
                strb_sql.Append("and BM_MANAME='" + ddlname.SelectedItem.Text + "'");
            }

            if (ddlmpxz.SelectedIndex != 0)//毛坯形状
            {
                strb_sql.Append(" and BM_MASHAPE='" + ddlmpxz.SelectedItem.Text + "'");
            }
            //if (ddlmpzt.SelectedIndex != 0)//毛坯状态
            //{
            //    strb_sql.Append(" and BM_MASTATE='" + ddlmpzt.SelectedItem.Text + "'");
            //}

            if (ddlChange.SelectedValue == "0")//变更状态-正常(材料计划状态、材料计划变更状态、制作明细变更状态)
            {
                strb_sql.Append(" and BM_MPSTATE='0' and BM_MPSTATUS='0'");
            }
            else if (ddlChange.SelectedValue == "1")//变更状态-变更（材料计划状态、材料计划变更状态、制作明细变更状态）
            {
                strb_sql.Append(" and BM_MPSTATE='0' and BM_MPSTATUS!='0'  and BM_SUBMITCHG='Y'");//and BM_MSSTATUS='0'
            }
            else if (ddlChange.SelectedValue == "2")//取消材料计划（所有提交过的材料计划）
            {
                strb_sql.Append(" and BM_MPSTATE='1' and BM_MPREVIEW='3' and BM_MSSTATUS='0' and BM_XUHAO NOT IN(SELECT MP_NEWXUHAO FROM View_TM_MPCHANGE WHERE MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_PID LIKE '%.JSB MPQX/%' AND MP_STATE<>'9') ");
            }

            string sql_cbl = "";
            foreach (ListItem listitem in cblMarid.Items)
            {
                if (listitem.Selected)
                {
                    sql_cbl += " BM_MANAME='" + listitem.Value + "' or ";
                }
            }
            if (sql_cbl.Trim() != "")
            {
                sql_cbl = " and (" + sql_cbl.Substring(0, sql_cbl.Length - 4) + ")";
            }
            strb_sql.Append(sql_cbl);


            udqcMp.ExistedConditions = strb_sql.ToString();
            ViewState["Mp"] = strb_sql.ToString() + UserDefinedQueryConditions.ReturnQueryString((GridView)udqcMp.FindControl("GridView1"), (Label)udqcMp.FindControl("Label1")) + this.QueryStringTreeView(TreeViewMp);
            UCPagingMP.CurrentPage = 1;
            this.InitVar(UCPagingMP, viewtable, "BM_ID", "", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["Mp"].ToString(), 0, Convert.ToInt16(ViewState["mpPageSize"]));
            this.bindGrid(UCPagingMP, GridView2, NoDataPanel2);
            ViewState["CurrentUCPaging"] = "UCPagingMP";

            this.BtnNormalChangeVisible("MP");
            //  this.ckbMpMore_OnCheckedChanged(null, null);
        }
        /// <summary>
        /// 材料计划标签中按类批量选择物料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlnamewithcheckboxlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpid.Value = "0";//是否勾选项的标志


            //设置计划系数的可见性及物料名称绑定
            if (ddlChange.SelectedValue == "0" || ddlChange.SelectedValue == "1")
            {


                string sqltext = "select DISTINCT BM_MANAME as A ,BM_MANAME as B from " + viewtable + " WHERE BM_MARID<>'' AND BM_MANAME IS NOT NULL AND  BM_ENGID='" + tsaid.Text + "' ";
                if (ddlmptype.SelectedValue == "板")
                {
                    sqltext += " and BM_MASHAPE='板'";
                }
                else if (ddlmptype.SelectedValue == "型")
                {
                    sqltext += " and (BM_MASHAPE='型' or BM_MASHAPE='圆')";
                }
                else if (ddlmptype.SelectedValue == "采")
                {
                    sqltext += " and BM_MASHAPE='采' ";
                }
                else if (ddlmptype.SelectedValue == "采购成品")
                {
                    sqltext += " and BM_MASHAPE='采购成品' ";
                }
                else if (ddlmptype.SelectedValue == "非")
                {
                    sqltext += " and BM_MASHAPE='非' ";
                }
                else if (ddlmptype.SelectedValue == "铸件")
                {
                    sqltext += " and  BM_MASHAPE='铸' ";
                }
                else if (ddlmptype.SelectedValue == "锻件")
                {
                    sqltext += " and  BM_MASHAPE='锻' ";
                }


                if (ddlChange.SelectedValue == "0")//变更状态-正常(材料计划状态、材料计划变更状态、制作明细变更状态)
                {
                    sqltext += " and BM_MPSTATE='0' and BM_MPSTATUS='0'";
                }
                else if (ddlChange.SelectedValue == "1")//变更状态-变更（材料计划状态、材料计划变更状态、制作明细变更状态）
                {
                    sqltext += " and BM_MPSTATE='0' and BM_MPSTATUS<>'0' and BM_MPSTATE='0' ";//and BM_MSSTATUS='0'
                    GridView2.Columns[0].Visible = false;
                    InitTreeMp("");
                }

                DBCallCommon.FillCheckBoxList(cblMarid, sqltext);
            }





            StringBuilder strb_sql = new StringBuilder();
            strb_sql.Append(" BM_ENGID='" + tsaid.Text + "'  and BM_WMARPLAN='Y'");
            //and BM_MASTATE not in('半','半退','半正','半调','协A','协B','成交')
            if (ddlmptype.SelectedIndex != 0)
            {
                if (ddlmptype.SelectedValue == "钢材")
                {
                    strb_sql.Append(" and (BM_MASHAPE='板' or BM_MASHAPE='型' or BM_MASHAPE='圆')");
                }
                else if (ddlmptype.SelectedValue == "采")
                {
                    strb_sql.Append(" and BM_MASHAPE='采'");
                }
                else if (ddlmptype.SelectedValue == "采购成品")
                {
                    strb_sql.Append(" and BM_MASHAPE='采购成品'");
                }
                else if (ddlmptype.SelectedValue == "非")
                {
                    strb_sql.Append(" and BM_MASHAPE='非'");
                }
                else if (ddlmptype.SelectedValue == "铸件")
                {
                    strb_sql.Append(" and  BM_MASHAPE='铸' ");
                }
                else if (ddlmptype.SelectedValue == "锻件")
                {
                    strb_sql.Append(" and BM_MASHAPE='锻'");
                }

            }

            if (ddlbjname.SelectedIndex != 0) //部件名称
            {
                strb_sql.Append(" AND (BM_ZONGXU='" + ddlbjname.SelectedValue + "' OR BM_ZONGXU like '" + ddlbjname.SelectedValue.ToString() + ".%')");
            }

            if (ddlname.SelectedIndex != 0)//材料名称
            {
                strb_sql.Append("and BM_MANAME='" + ddlname.SelectedItem.Text + "'");
            }

            if (ddlmpxz.SelectedIndex != 0)//材料类型
            {
                strb_sql.Append(" and BM_MASHAPE='" + ddlmpxz.SelectedItem.Text + "'");
            }


            if (ddlChange.SelectedValue == "0")//变更状态-正常(材料计划状态、材料计划变更状态、制作明细变更状态)
            {
                strb_sql.Append(" and BM_MPSTATE='0' and BM_MPSTATUS='0' and BM_MARID!=''");
                GridView2.Columns[0].Visible = true;
            }
            else if (ddlChange.SelectedValue == "1")//变更状态-变更（材料计划状态、材料计划变更状态、制作明细变更状态）
            {
                strb_sql.Append(" and BM_MPSTATUS<>'0' and BM_MPSTATE='0'");//and BM_MSSTATUS='0'
                GridView2.Columns[0].Visible = false;
            }
            else if (ddlChange.SelectedValue == "2")//取消材料计划（所有提交过的材料计划）
            {
                strb_sql.Append(" and BM_MPSTATE='1' and BM_MPREVIEW='3' and BM_MSSTATUS='0' and BM_XUHAO NOT IN(SELECT MP_NEWXUHAO FROM View_TM_MPCHANGE WHERE MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_PID LIKE '%.JSB MPQX/%' AND MP_STATE<>'9' and BM_MARID!='') ");
                GridView2.Columns[0].Visible = true;
            }

            string sql_cbl = "";
            foreach (ListItem listitem in cblMarid.Items)
            {
                if (listitem.Selected)
                {
                    sql_cbl += " BM_MANAME='" + listitem.Value + "' or ";
                }
            }
            if (sql_cbl.Trim() != "")
            {
                sql_cbl = " and (" + sql_cbl.Substring(0, sql_cbl.Length - 4) + ")";
            }
            strb_sql.Append(sql_cbl);


            udqcMp.ExistedConditions = strb_sql.ToString();
            ViewState["Mp"] = strb_sql.ToString() + UserDefinedQueryConditions.ReturnQueryString((GridView)udqcMp.FindControl("GridView1"), (Label)udqcMp.FindControl("Label1")) + this.QueryStringTreeView(TreeViewMp);
            UCPagingMP.CurrentPage = 1;
            this.InitVar(UCPagingMP, viewtable, "BM_ID", "", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["Mp"].ToString(), 0, Convert.ToInt16(ViewState["mpPageSize"]));
            this.bindGrid(UCPagingMP, GridView2, NoDataPanel2);
            ViewState["CurrentUCPaging"] = "UCPagingMP";

            this.BtnNormalChangeVisible("MP");
            //  this.ckbMpMore_OnCheckedChanged(null, null);
        }

        protected void btnMPClear_OnClick(object sender, EventArgs e)
        {
            //部件名称
            ddlbjname.ClearSelection();
            ddlbjname.Items[0].Selected = true;

            //材料名称
            ddlname.ClearSelection();
            ddlname.Items[0].Selected = true;

            //材料类型
            ddlmpxz.ClearSelection();
            ddlmpxz.Items[0].Selected = true;

            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqcMp.FindControl("GridView1"));
        }
        /// <summary>
        /// 设置材料计划标签颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView2_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((HiddenField)e.Row.Cells[1].FindControl("hdfMPtate")).Value == "1")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Green;//已提材料计划
                }

                HiddenField hfmpchg = (HiddenField)e.Row.Cells[1].FindControl("hdfMPChg");

                if (hfmpchg.Value == "1")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Gray;//材料计划变更删除
                }
                else if (hfmpchg.Value == "2")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Orange;//材料计划变更增加
                }
                else if (hfmpchg.Value == "3")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;//材料计划变更修改
                }


            }
        }
        /// <summary>
        /// 生成材料计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mpsubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
                return;
            }
            if (ddlShebei.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择设备及图号');", true);
                return;
            }
            List<string> list_mp = new List<string>();
            string fixedsize;
            string status;
            string state;
            double cailiaocd;
            double cailiaokd;
            string ku;
            double my;
            string maopi;
            string mptypenow = ddlmptype.SelectedValue.ToString();
            StringBuilder strb_sql = new StringBuilder();
            if (mpid.Value == "1") //相当于选择确定
            {
                string mp_no = "";
                //是否存在未提交的计划
                string sql_findsame = "select TOP 1 MP_ID from TBPM_MPFORALLRVW where MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_ID LIKE '% MP/%' AND MP_STATE IN('0','1') AND MP_MASHAPE='" + ddlmptype.SelectedValue + "'  order by dbo.SubPcode(MP_ID,'/') desc";
                SqlDataReader dr_findsame = DBCallCommon.GetDRUsingSqlText(sql_findsame);
                if (dr_findsame.HasRows)//有未提交审核项
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您有相同类型的材料计划未提交，请提交该计划或取消后再操作！！！')", true);
                    return;
                }
                else
                {
                    sqlText = "select top 1 dbo.SubPcode(MP_ID,'/') as pc from TBPM_MPFORALLRVW ";
                    sqlText += "where MP_ENGID='" + tsaid.Text + "' AND MP_ID LIKE '%MP/%' order by dbo.SubPcode(MP_ID,'/') desc";//审核状态 初始化为0，1为保存，2为提交，3为一级驳回，4为一级通过，5为二级驳回，6为二级通过，7为三级驳回，8为三级通过。
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        mp_pici = (int.Parse(dr["pc"].ToString()) + 1).ToString();
                        dr.Close();
                    }
                    else
                    {
                        mp_pici = "1";
                    }

                    mp_no = tsaid.Text + "." + "MP/" + mp_pici.PadLeft(3, '0');
                }

                sql_findsame = "select TSA_REVIEWERID from TBPM_TCTSASSGN where TSA_ID='" + tsaid.Text + "' ";
                string REVIEWA = DBCallCommon.GetDTUsingSqlText(sql_findsame).Rows[0][0].ToString();
                string sql_ph = "";
                if (chkiffast.Checked)
                {
                    sql_ph = "insert into TBPM_MPFORALLRVW(MP_ID,MP_PJID,MP_ENGID,MP_ENGTYPE,MP_SUBMITID,MP_CHECKLEVEL,MP_MASHAPE,MP_REVIEWA,MP_MAP,MP_CHILDENGNAME,MP_IFFAST) " +
                             "VALUES('" + mp_no + "','" + pro_id.Value + "','" + tsaid.Text + "','" + eng_type.Value + "','" +
                             Session["UserID"] + "','3','" + ddlmptype.SelectedValue + "','" + REVIEWA + "','" + ddlShebei.SelectedValue.Split('|')[1] + "','" + ddlShebei.SelectedValue.Split('|')[0] + "','1')";//默认三级审核
                }
                else
                {
                    sql_ph = "insert into TBPM_MPFORALLRVW(MP_ID,MP_PJID,MP_ENGID,MP_ENGTYPE,MP_SUBMITID,MP_CHECKLEVEL,MP_MASHAPE,MP_REVIEWA,MP_MAP,MP_CHILDENGNAME,MP_IFFAST) " +
                                                "VALUES('" + mp_no + "','" + pro_id.Value + "','" + tsaid.Text + "','" + eng_type.Value + "','" +
                                                Session["UserID"] + "','3','" + ddlmptype.SelectedValue + "','" + REVIEWA + "','" + ddlShebei.SelectedValue.Split('|')[1] + "','" + ddlShebei.SelectedValue.Split('|')[0] + "',NULL)";//默认三级审
                }
                list_mp.Add(sql_ph);





                double cailiaozc;
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView2.Rows[i];
                    CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        //xuhao = gRow.Cells[2].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[2].Text.Trim();
                        tuhao = gRow.Cells[2].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[2].Text.Trim();
                        marid = gRow.Cells[3].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[3].Text.Trim();
                        zongxu = gRow.Cells[4].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[4].Text.Trim();
                        my = Convert.ToDouble(gRow.Cells[16].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[16].Text.Trim());
                        cailiaoname = gRow.Cells[5].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[5].Text.Trim();
                        cailiaoguige = gRow.Cells[6].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[6].Text.Trim();
                        caizhi = gRow.Cells[7].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[7].Text.Trim();
                        maopi = gRow.Cells[13].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[13].Text.Trim();
                        fixedsize = gRow.Cells[19].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[19].Text.Trim();
                        cailiaozc = Convert.ToDouble(gRow.Cells[17].Text.Trim() == "&nbsp;" ? "0" : gRow.Cells[17].Text.Trim());
                        yongliang = gRow.Cells[10].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[10].Text.Trim();
                        techunit = gRow.Cells[9].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[9].Text.Trim();
                        cailiaocd = Convert.ToDouble(gRow.Cells[14].Text.Trim());
                        cailiaokd = Convert.ToDouble(gRow.Cells[15].Text.Trim());
                        ku = gRow.Cells[23].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[23].Text.Trim();
                        //  string bz = gRow.Cells[22].Text.Trim();
                        string bz = ((Label)gRow.FindControl("lblAllbeizhu")).Text.Trim();

                        if (bz == "&nbsp;" || (maopi == "板" && fixedsize == "N"))
                        {
                            bz = "";
                        }

                        labunit = gRow.Cells[9].Text.Trim();
                        cailiaozongzhong = float.Parse(gRow.Cells[12].Text.Trim());

                        shuliang = double.Parse(gRow.Cells[8].Text.Trim());

                        string sql_find_unit = "SELECT PURCUNIT FROM  TBMA_MATERIAL  where ID='" + marid + "'";
                        DataTable dt_mar = DBCallCommon.GetDTUsingSqlText(sql_find_unit);

                        //如果是标准件中的垫板，且材料总长不为零，插入采购数量为【材料总长】；&& (cailiaoname == "条形密封垫" || cailiaoname == "密封垫" || cailiaoname == "密封圈" || ch_name == "密封绳")



                        status = "0";//正常
                        state = "0";//初始化

                        //更新原始数据
                        strb_sql.Append("update "+tablename+" set BM_MPSTATE='1'");
                        strb_sql.Append(" where BM_ENGID='"+tsaid.Text.Trim()+"' and BM_ZONGXU='"+zongxu+"'");
                        //strb_sql.Append("update "+tablename+" set BM_MPSTATE='1'");
                        //strb_sql.Append(" where BM_ENGID='"+tsaid.Text.Trim()+"' and BM_ZONGXU='"+zongxu+"'");
                        list_mp.Add(strb_sql.ToString());
                        strb_sql.Remove(0, strb_sql.Length);
                        //材料插入明细表
                        strb_sql.Append("insert into " + mptable + "(MP_PID,MP_CHGPID,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_PJID,MP_ENGID,MP_WEIGHT,MP_NUMBER,MP_LENGTH,MP_WIDTH,MP_ZONGXU,MP_STATUS,MP_STATE,MP_MASHAPE,MP_TUHAO,MP_ALLBEIZHU,MP_MIANYU,MP_FIXEDSIZE,MP_YONGLIANG,MP_TECHUNIT,MP_KU)");
                        strb_sql.Append(" values('" + mp_no + "','','" + zongxu + "','" + zongxu + "','" + marid + "','" + pro_id.Value + "','" + tsaid.Text + "','" + cailiaozongzhong + "','" + shuliang + "','" + cailiaocd + "','" + cailiaokd + "','" + zongxu + "','" + status + "','" + state + "','" + maopi + "','" + tuhao + "','" + bz + "','" + my + "','" + fixedsize + "','" + yongliang + "','" + techunit + "','" + ku + "')");
                        list_mp.Add(strb_sql.ToString());
                        strb_sql.Remove(0, strb_sql.Length);
                    }
                }

                mpid.Value = "0";
                //try
                //{
                DBCallCommon.ExecuteTrans(list_mp);
                //}
                //catch
                //{

                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法生成计划！！！\\r\\r提示:勾选的记录中，有已经生成或取消计划的记录！！！')", true);
                //    return;
                //}
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "location.href='" + "TM_HZY_pur.aspx?id=" + mp_no + "'", true);

            }


        }
        /// <summary>
        /// 材料计划变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mpChange_Click(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
                return;
            }
            //string fixedzize;
            //string status;
            //string state;
            //double cailiaocd;
            //double cailiaokd;
            //double my;
            //string maopi;
            List<string> list_sql = new List<string>();
            //string mpchgtypenow = ddlmptype.SelectedValue.ToString();

            int first_level_count = TreeMp.Nodes[0].ChildNodes.Count;
            string xuhao_str = "";
            for (int i = 0; i < first_level_count; i++)
            {
                TreeNode childnode = (TreeNode)TreeMp.Nodes[0].ChildNodes[i];
                xuhao_str += this.GetChildNodeXuhao(childnode, "");
            }
            xuhao_str = this.get_xu_without_xia(xuhao_str);

            int selected_parts = xuhao_str.Split('$').Length - 1;

            if (selected_parts > 0 && selected_parts <= 25)
            {
                xuhao_str = xuhao_str.Substring(0, xuhao_str.Length - 1);
                Response.Redirect("TM_MP_CONTRAST.aspx?id=" + tsaid.Text + "&Xuhao=" + xuhao_str + "");
            }
            else if (selected_parts > 25)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('最多只能勾选25条记录！！！')", true);
            }
            else if (selected_parts == 0)
            {
                Response.Redirect("TM_MP_CONTRAST.aspx?id=" + tsaid.Text + "&Xuhao=1");
            }
            //string mp_no = "";
            //string mp_no_real = "";

            ////是否存在材料类型相同的未提交的计划
            //string sql_findsame = "select TOP 1 MP_ID from TBPM_MPCHANGERVW where MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_ID LIKE '% MPBG/%' AND MP_MASHAPE='" + ddlmptype.SelectedValue + "'  AND MP_STATE IN('0','1') order by dbo.SubPcode(MP_ID,'/') desc";
            //SqlDataReader dr_findsame = DBCallCommon.GetDRUsingSqlText(sql_findsame);
            //if (dr_findsame.HasRows)//有相同计划类型的未提交审核项
            //{
            //    dr_findsame.Read();
            //    mp_no_real = dr_findsame["MP_ID"].ToString();
            //    dr_findsame.Close();
            //}
            //else
            //{
            //    //判断材料计划变更提交次数
            //    sqlText = "select top 1 dbo.SubPcode(MP_ID,'/') from TBPM_MPCHANGERVW ";
            //    sqlText += "where MP_ENGID='" + tsaid.Text + "' and MP_ID like '% MPBG/%' order by dbo.SubPcode(MP_ID,'/') desc";
            //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            //    if (dr.HasRows)
            //    {
            //        dr.Read();
            //        mp_pici = (int.Parse(dr[0].ToString()) + 1).ToString();
            //        dr.Close();
            //    }
            //    else
            //    {
            //        mp_pici = "1";
            //    }

            //    mp_no = tsaid.Text + "." + "JSB MPBG" +  "/" + mp_pici.PadLeft(3, '0');

            //    mp_no_real = tsaid.Text + "." + "JSB MPBG" + "/" + mp_pici.PadLeft(3, '0') + this.GetAllLotNum();

            //}




            //    string existmptype = "";

            //    sqlText = "select MP_ID,MP_MASHAPE from TBPM_MPCHANGERVW where MP_ID='" + mp_no_real + "'";
            //    SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqlText);

            //    if (!dr1.HasRows)//如果批号不存在，插入批号
            //    {
            //        sqlText = "insert into TBPM_MPCHANGERVW ";
            //        sqlText += "(MP_ID,MP_PJID,MP_ENGID,MP_ENGTYPE,MP_SUBMITID,MP_CHECKLEVEL,MP_MASHAPE) ";
            //        sqlText += " VALUES ('" + mp_no_real + "','" + pro_id.Value + "',";
            //        sqlText += "'" + tsaid.Text + "','" + eng_type.Value + "','" + Session["UserID"] + "','3','" + ddlmptype.SelectedValue + "')";
            //        list_sql.Add(sqlText);
            //    }
            //    else
            //    {
            //        dr1.Read();
            //        mp_no_real = dr1["MP_ID"].ToString();
            //        existmptype = dr1["MP_MASHAPE"].ToString();
            //        dr1.Close();
            //    }

            //    if (existmptype != "" && existmptype != mpchgtypenow)
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您有未提交的【" + existmptype + "】变更材料计划，请提交该计划或取消后再操作！！！')", true);
            //    }
            //    else
            //    {

            //        double cailiaozc;
            //        for (int i = 0; i < GridView2.Rows.Count; i++)
            //        {
            //            GridViewRow gRow = GridView2.Rows[i];
            //            CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
            //            if (chk.Checked)
            //            {
            //                xuhao = gRow.Cells[4].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[4].Text.Trim();
            //                tuhao = gRow.Cells[2].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[2].Text.Trim();
            //                marid = gRow.Cells[3].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[3].Text.Trim();
            //                zongxu = gRow.Cells[4].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[4].Text.Trim();
            //                cailiaoname = gRow.Cells[5].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[5].Text.Trim();
            //                cailiaoguige = gRow.Cells[6].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[6].Text.Trim();
            //                maopi = gRow.Cells[17].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[17].Text.Trim();
            //                if (maopi == "型" || maopi == "圆")
            //                {
            //                    cailiaocd = Convert.ToDouble(gRow.Cells[13].Text.Trim());
            //                }
            //                else
            //                {
            //                    cailiaocd = Convert.ToDouble(gRow.Cells[7].Text.Trim());
            //                }
            //                cailiaokd = Convert.ToDouble(gRow.Cells[8].Text.Trim());
            //                shuliang = double.Parse(gRow.Cells[11].Text.Trim());
            //                cailiaozongzhong = float.Parse(gRow.Cells[12].Text.Trim());
            //                cailiaozc = Convert.ToDouble(gRow.Cells[13].Text.Trim() == "&nbsp;" ? "0" : gRow.Cells[13].Text.Trim());
            //                my = Convert.ToDouble(gRow.Cells[14].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[14].Text.Trim());
            //                fixedzize = gRow.Cells[18].Text.Trim();
            //                string bz = gRow.Cells[21].Text.Trim();                                
            //                caizhi = gRow.Cells[15].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[15].Text.Trim();                          
            //                if (bz == "&nbsp;" || ((maopi == "型" || maopi == "圆" || maopi == "板") && fixedzize == "N"))
            //                {
            //                    bz = "";
            //                }

            //                labunit = gRow.Cells[16].Text.Trim();



            //                string sql_find_unit = "SELECT PURCUNIT FROM  TBMA_MATERIAL  where ID='" + marid + "'";
            //                DataTable dt_mar = DBCallCommon.GetDTUsingSqlText(sql_find_unit);

            //                //如果是标准件中的垫板，且材料总长不为零，插入采购数量为【材料总长】；&& (cailiaoname == "条形密封垫" || cailiaoname == "密封垫" || cailiaoname == "密封圈" || ch_name == "密封绳")
            //                if (marid.Contains("01.01.") || (maopi != "板" && maopi != "型" && maopi != "圆钢"))
            //                {
            //                    if (dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "米" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "M" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "m")
            //                    {
            //                        shuliang = cailiaozc / 1000;
            //                    }
            //                    if (marid.Contains("01.01"))
            //                    {
            //                        cailiaozongzhong = 0;
            //                    }
            //                }

            //                if (dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "平方米" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "M2" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "㎡")
            //                {
            //                    shuliang = my;
            //                }



            //                status = "0";//正常
            //                state = "0";//初始化


            //                status = ((Label)gRow.FindControl("lblMPChangeState")).Text.Trim();
            //                state = "0";

            //                switch (status)
            //                {
            //                    case "删除":
            //                        state = "1";
            //                        break;
            //                    case "增加":
            //                        state = "2";
            //                        break;
            //                    case "修改":
            //                        state = "3";
            //                        break;
            //                    case "正常":
            //                        state = "0";
            //                        break;
            //                    default:
            //                        break;
            //                }

            //                //更新原始数据变更状态
            //                sqlText = "update " + tablename + " set BM_MPSTATE='1' ";
            //                sqlText += "where BM_ENGID='" + tsaid.Text + "' and BM_XUHAO='" + xuhao + "'";
            //                list_sql.Add(sqlText);
            //                //插入变更记录
            //                sqlText = "insert into TBPM_MPCHANGE ";
            //                sqlText += "(MP_PID,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_PJID,MP_ENGID,MP_WEIGHT,";
            //                sqlText += " MP_NUMBER,MP_TYPE,MP_LENGTH,MP_WIDTH,MP_FIXEDSIZE,";
            //                sqlText += " MP_ZONGXU,MP_CHANGESTATE,MP_MASHAPE,MP_TUHAO,MP_ALLBEIZHU) values ";
            //                sqlText += "('" + mp_no_real + "','" + xuhao + "','" + xuhao + "','" + marid + "','" + pro_id.Value + "',";
            //                sqlText += " '" + tsaid.Text + "','" + cailiaozongzhong + "','" + shuliang + "'," ;
            //                sqlText += " '" + category + "','" + cailiaocd + "','" + cailiaokd + "',";
            //                sqlText += " '" + fixedzize + "','" + zongxu + "','" + state + "','" + maopi + "','" + tuhao + "','" + bz + "')";
            //                list_sql.Add(sqlText);
            //                //变更前记录更新变更批号
            //                sqlText = "update TBPM_MPPLAN set MP_CHGPID='" + mp_no_real + "' where MP_NEWXUHAO='" + xuhao + "' and MP_STATUS='0' and MP_PID=(SELECT MP_PID FROM View_TM_MPPLAN WHERE MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_NEWXUHAO='" + xuhao + "' and MP_STATUS='0' and MP_STATERV='8')";
            //                list_sql.Add(sqlText);
            //            }
            //        }
            //        mpid.Value = "0";
            //        try
            //        {
            //            DBCallCommon.ExecuteTrans(list_sql);
            //        }
            //        catch
            //        {
            //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法生成计划！！！\\r\\r提示:勾选的记录中，有已经生成或取消计划的记录！！！')", true);
            //            return;
            //        }
            //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "location.href='" + "TM_HZY_pur.aspx?id=" + mp_no_real + "'", true);
            //}



        }
        /// <summary>
        /// 取消材料计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mpCutDown_Click(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
                return;
            }

            string fixedzize;
            string state;
            double cailiaocd;
            double cailiaokd;
            double my;
            string maopi;
            List<string> list_sql = new List<string>();
            string mpchgtypenow = ddlmptype.SelectedValue.ToString();
            if (mpid.Value == "1")
            {
                string mp_no = "";
                string mp_no_real = "";

                //是否存在材料类型相同的未提交的计划
                string sql_findsame = "select TOP 1 MP_ID from TBPM_MPCHANGERVW where MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_ID LIKE '% MPQX/%' AND MP_MASHAPE='" + ddlmptype.SelectedValue + "'  AND MP_STATE IN('0','1') order by dbo.SubPcode(MP_ID,'/') desc";
                SqlDataReader dr_findsame = DBCallCommon.GetDRUsingSqlText(sql_findsame);
                if (dr_findsame.HasRows)//有相同计划类型的未提交审核项
                {
                    dr_findsame.Read();
                    mp_no_real = dr_findsame["MP_ID"].ToString();
                    dr_findsame.Close();
                }
                else
                {
                    //判断材料计划变更提交次数
                    sqlText = "select top 1 dbo.SubPcode(MP_ID,'/') from TBPM_MPCHANGERVW ";
                    sqlText += "where MP_ENGID='" + tsaid.Text + "' and MP_ID like '% MPQX/%'  order by dbo.SubPcode(MP_ID,'/') desc";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        mp_pici = (int.Parse(dr[0].ToString()) + 1).ToString();
                        dr.Close();
                    }
                    else
                    {
                        mp_pici = "1";
                    }

                    mp_no = tsaid.Text + "." + "MPQX" + "/" + Session["UserNameCode"] + "/" + mp_pici.PadLeft(3, '0');

                }

                if (Session["UserNameCode"] == null || Session["UserNameCode"].ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('会话已到期，请重新登录！！！');", true);
                }
                else
                {

                    string existmptype = "";
                    sqlText = "select MP_ID,MP_MASHAPE from TBPM_MPCHANGERVW where MP_ID ='" + mp_no_real + "'";
                    SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqlText);

                    if (!dr1.HasRows)//如果批号不存在，插入批号
                    {
                        if (chkiffast.Checked)
                        {
                            sqlText = "insert into TBPM_MPCHANGERVW ";
                            sqlText += "(MP_ID,MP_PJID,MP_ENGID,MP_ENGTYPE,MP_SUBMITID,MP_CHECKLEVEL,MP_MASHAPE,MP_IFFAST) ";
                            sqlText += " VALUES ('" + mp_no_real + "','" + pro_id.Value + "',";
                            sqlText += "'" + tsaid.Text + "','" + eng_type.Value + "','" + Session["UserID"] + "','3','" + ddlmptype.SelectedValue + "','1')";
                        }
                        else
                        {
                            sqlText = "insert into TBPM_MPCHANGERVW ";
                            sqlText += "(MP_ID,MP_PJID,MP_ENGID,MP_ENGTYPE,MP_SUBMITID,MP_CHECKLEVEL,MP_MASHAPE,MP_IFFAST) ";
                            sqlText += " VALUES ('" + mp_no_real + "','" + pro_id.Value + "',";
                            sqlText += "'" + tsaid.Text + "','" + eng_type.Value + "','" + Session["UserID"] + "','3','" + ddlmptype.SelectedValue + "',NULL)";
                        }
                        list_sql.Add(sqlText);
                    }
                    else
                    {
                        dr1.Read();
                        existmptype = dr1["MP_MASHAPE"].ToString();
                        mp_no_real = dr1["MP_ID"].ToString();
                        dr1.Close();
                    }

                    if (existmptype != "" && existmptype != mpchgtypenow)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您有未提交待取消的【" + existmptype + "】计划，请提交该计划或取消后再操作！！！')", true);
                    }
                    else
                    {

                        double cailiaozc;
                        for (int i = 0; i < GridView2.Rows.Count; i++)
                        {
                            GridViewRow gRow = GridView2.Rows[i];
                            CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                            if (chk.Checked)
                            {
                                xuhao = gRow.Cells[2].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[2].Text.Trim();
                                tuhao = gRow.Cells[3].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[3].Text.Trim();
                                marid = gRow.Cells[4].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[4].Text.Trim();
                                zongxu = gRow.Cells[5].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[5].Text.Trim();
                                my = Convert.ToDouble(gRow.Cells[15].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[15].Text.Trim());
                                cailiaoname = gRow.Cells[6].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[6].Text.Trim();
                                cailiaoguige = gRow.Cells[7].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[7].Text.Trim();
                                caizhi = gRow.Cells[16].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[16].Text.Trim();
                                maopi = gRow.Cells[18].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[18].Text.Trim();
                                cailiaozc = Convert.ToDouble(gRow.Cells[14].Text.Trim() == "&nbsp;" ? "0" : gRow.Cells[14].Text.Trim());


                                cailiaocd = Convert.ToDouble(gRow.Cells[8].Text.Trim());

                                cailiaokd = Convert.ToDouble(gRow.Cells[9].Text.Trim());
                                string bz = gRow.Cells[28].Text.Trim();
                                fixedzize = gRow.Cells[22].Text.Trim();
                                if (bz == "&nbsp;" || ((maopi == "型" || maopi == "圆钢" || maopi == "板") && fixedzize == "N"))
                                {
                                    bz = "";
                                }
                                if (caizhi == "&nbsp;")
                                {
                                    caizhi = "";
                                    category = "一般材料";
                                    influence = "无影响";
                                }
                                else if (caizhi == "10.9")
                                {
                                    category = "重要材料";
                                    influence = "无影响";
                                }
                                else if (cailiaoname.Contains("钢") || maopi.Contains("型") || caizhi.Contains("玻璃纤维") || caizhi.Contains("矿棉") || caizhi.Contains("石棉") || caizhi.Contains("盘根") || caizhi.Contains("橡胶"))
                                {
                                    category = "重要材料";
                                    influence = "有影响";
                                }
                                else
                                {
                                    category = "一般材料";
                                    influence = "无影响";
                                }

                                labunit = gRow.Cells[17].Text.Trim();
                                cailiaozongzhong = float.Parse(gRow.Cells[13].Text.Trim());
                                shuliang = double.Parse(gRow.Cells[12].Text.Trim());

                                string sql_find_unit = "SELECT PURCUNIT FROM  TBMA_MATERIAL  where ID='" + marid + "'";
                                DataTable dt_mar = DBCallCommon.GetDTUsingSqlText(sql_find_unit);

                                //如果是标准件中的垫板，且材料总长不为零，插入采购数量为【材料总长】；&& (cailiaoname == "条形密封垫" || cailiaoname == "密封垫" || cailiaoname == "密封圈" || ch_name == "密封绳")
                                if (marid.Contains("01.01") || (maopi != "板" && maopi != "型" && maopi != "圆钢"))
                                {
                                    if (dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "米" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "M" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "m")
                                    {
                                        shuliang = cailiaozc / 1000;
                                    }

                                    if (marid.Contains("01.01"))
                                    {
                                        cailiaozongzhong = 0;
                                    }
                                }

                                if (dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "平方米" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "M2" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || dt_mar.Rows[0]["PURCUNIT"].ToString().Trim() == "㎡")
                                {
                                    shuliang = double.Parse(gRow.Cells[12].Text.Trim()) * my;
                                }

                                state = "1";//所有删除
                                keycoms = gRow.Cells[24].Text.Trim();

                                if (keycoms == "&nbsp;")
                                {
                                    keycoms = "";
                                }

                                sqlText = "insert into TBPM_MPCHANGE ";
                                sqlText += "(MP_PID,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_PJID,MP_ENGID,MP_WEIGHT,";
                                sqlText += " MP_NUMBER,MP_KEYCOMS,MP_TYPE,MP_ENVREFFCT,MP_LENGTH,MP_WIDTH,MP_FIXEDSIZE,";
                                sqlText += " MP_ZONGXU,MP_CHANGESTATE,MP_MASHAPE,MP_TUHAO,MP_NOTE) values ";
                                sqlText += "('" + mp_no_real + "','" + xuhao + "','" + xuhao + "','" + marid + "','" + pro_id.Value + "',";
                                sqlText += " '" + tsaid.Text + "','" + cailiaozongzhong + "','" + shuliang + "','" + keycoms + "',";
                                sqlText += " '" + category + "','" + influence + "'," + cailiaocd + "," + cailiaokd + ",";
                                sqlText += " '" + fixedzize + "','" + zongxu + "','" + state + "','" + maopi + "','" + tuhao + "','')";
                                list_sql.Add(sqlText);

                                //变更前记录更新变更批号
                                sqlText = "update TBPM_MPFORHZY set MP_CHGPID='" + mp_no_real + "' where MP_NEWXUHAO='" + xuhao + "' and MP_STATUS='0' and MP_PID=(SELECT MP_PID FROM View_TM_MPPLAN WHERE MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_NEWXUHAO='" + xuhao + "' and MP_STATUS='0' and MP_STATERV='8')";
                                list_sql.Add(sqlText);
                            }
                        }
                        mpid.Value = "0";
                        try
                        {
                            DBCallCommon.ExecuteTrans(list_sql);
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法生成计划！！！\\r\\r提示:勾选的记录中，有已经生成或取消计划的记录！！！')", true);
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "location.href='" + "TM_HZY_pur.aspx?id=" + mp_no_real + "'", true);
                    }
                }
            }

        }

        protected void ckbMpMore_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ckbMpMore.Checked)
            {
                ViewState["mpPageSize"] = "1000";
            }
            else
            {
                ViewState["mpPageSize"] = "100";
            }
            UCPagingMP.CurrentPage = 1;
            this.InitVar(UCPagingMP, viewtable, "BM_ID", "", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["Mp"].ToString(), 0, Convert.ToInt16(ViewState["mpPageSize"]));
            this.bindGrid(UCPagingMP, GridView2, NoDataPanel2);
            ViewState["CurrentUCPaging"] = "UCPagingMP";

            this.BtnNormalChangeVisible("MP");
            //if (ckbMpMore.Checked && ddlChange.SelectedValue != "2")
            //{
            //    mpsubmit.Visible = false;
            //    mpChange.Visible = false;
            //    mpCutDown.Visible = false;
            //    lkbtnToMpPage.Visible = true;
            //}
            //else
            //{
            //    lkbtnToMpPage.Visible = false;
            //    this.BtnNormalChangeVisible("MP");
            //}
        }

        /// <summary>
        /// 执行存储过程验证序号（保存时）
        /// </summary>
        /// <param name="psv"></param>
        private void ExecAddAllowance(string Engid, string StrWhere, string Xishu, string AllowanceType)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_AddAllowance]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Engid", Engid, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@StrWhere", StrWhere, SqlDbType.Text, 3000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Xishu", Xishu, SqlDbType.Text, 10);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@AllowanceType", AllowanceType, SqlDbType.Text, 3000);
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
        /// 调转到快捷生成材料计划页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkbtnToMpPage_OnClick(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
                return;
            }

            if (ddlChange.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择【提交类型】！！！')", true);
                return;
            }

            //对未提交计划进行检验,获取实际批号数据



            string mp_no = "";
            string mp_no_real = "";

            #region 正常计划
            if (ddlChange.SelectedValue == "0")//正常计划
            {
                //是否存在材料类型相同的未提交的计划
                string sql_findsame = "select TOP 1 MP_ID from TBPM_MPFORALLRVW where MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_ID LIKE '% MP/%' AND MP_MASHAPE='" + ddlmptype.SelectedValue + "'  AND MP_STATE IN('0','1') order by dbo.SubPcode(MP_ID,'/') desc";
                SqlDataReader dr_findsame = DBCallCommon.GetDRUsingSqlText(sql_findsame);
                if (dr_findsame.HasRows)//有相同计划类型的未提交审核项
                {
                    dr_findsame.Read();
                    mp_no_real = dr_findsame["MP_ID"].ToString();
                    dr_findsame.Close();
                }
                else
                {
                    sqlText = "select top 1 dbo.SubPcode(MP_ID,'/') as pc from TBPM_MPFORALLRVW ";
                    sqlText += "where MP_ENGID='" + tsaid.Text + "' AND MP_ID LIKE '% MP/%' order by dbo.SubPcode(MP_ID,'/') desc";//审核状态 初始化为0，1为保存，2为提交，3为一级驳回，4为一级通过，5为二级驳回，6为二级通过，7为三级驳回，8为三级通过。
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        mp_pici = (int.Parse(dr["pc"].ToString()) + 1).ToString();
                        dr.Close();
                    }
                    else
                    {
                        mp_pici = "1";
                    }

                    mp_no = tsaid.Text + "." + "JSB" + "/" + Session["UserNameCode"] + "/" + mp_pici.PadLeft(3, '0');
                }

            }
            #endregion

            #region 变更计划
            else if (ddlChange.SelectedValue == "1")//变更计划
            {
                //是否存在材料类型相同的未提交的计划
                string sql_findsame = "select TOP 1 MP_ID from TBPM_MPCHANGERVW where MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_ID LIKE '% MPBG/%' AND MP_MASHAPE='" + ddlmptype.SelectedValue + "'  AND MP_STATE IN('0','1') order by dbo.SubPcode(MP_ID,'/') desc";
                SqlDataReader dr_findsame = DBCallCommon.GetDRUsingSqlText(sql_findsame);
                if (dr_findsame.HasRows)//有相同计划类型的未提交审核项
                {
                    dr_findsame.Read();
                    mp_no_real = dr_findsame["MP_ID"].ToString();
                    dr_findsame.Close();
                }
                else
                {
                    //判断材料计划变更提交次数
                    sqlText = "select top 1 dbo.SubPcode(MP_ID,'/') from TBPM_MPCHANGERVW ";
                    sqlText += "where MP_ENGID='" + tsaid.Text + "' and MP_ID like '% MPBG/%' order by dbo.SubPcode(MP_ID,'/') desc";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        mp_pici = (int.Parse(dr[0].ToString()) + 1).ToString();
                        dr.Close();
                    }
                    else
                    {
                        mp_pici = "1";
                    }

                    mp_no = tsaid.Text + "." + "MPBG" + "/" + Session["UserNameCode"] + "/" + mp_pici.PadLeft(3, '0');

                }

            }
            #endregion

            #region 取消计划
            else if (ddlChange.SelectedValue == "2")//取消计划
            {
                //是否存在材料类型相同的未提交的计划
                string sql_findsame = "select TOP 1 MP_ID from TBPM_MPCHANGERVW where MP_ENGID='" + tsaid.Text.Trim() + "' AND MP_ID LIKE '% MPQX/%' AND MP_MASHAPE='" + ddlmptype.SelectedValue + "'  AND MP_STATE IN('0','1') order by dbo.SubPcode(MP_ID,'/') desc";
                SqlDataReader dr_findsame = DBCallCommon.GetDRUsingSqlText(sql_findsame);
                if (dr_findsame.HasRows)//有相同计划类型的未提交审核项
                {
                    dr_findsame.Read();
                    mp_no_real = dr_findsame["MP_ID"].ToString();
                    dr_findsame.Close();
                }
                else
                {
                    //判断材料计划变更提交次数
                    sqlText = "select top 1 dbo.SubPcode(MP_ID,'/') from TBPM_MPCHANGERVW ";
                    sqlText += "where MP_ENGID='" + tsaid.Text + "' and MP_ID like '% MPQX/%'  order by dbo.SubPcode(MP_ID,'/') desc";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        mp_pici = (int.Parse(dr[0].ToString()) + 1).ToString();
                        dr.Close();
                    }
                    else
                    {
                        mp_pici = "1";
                    }

                    mp_no = tsaid.Text + "." + "MPQX" + "/" + Session["UserNameCode"] + "/" + mp_pici.PadLeft(3, '0');
                }
            }
            #endregion


            string strwhere = ViewState["Mp"].ToString();
            string mptype = ddlmptype.SelectedValue;
            string mpchange = ddlChange.SelectedValue;
            string mpno = mp_no_real;
            string pjid = pro_id.Value;
            string engtype = eng_type.Value;
            string engid = tsaid.Text;

            string passedString = "TM_MpMoreCreate.aspx?tablename=" + Server.HtmlEncode(viewtable);
            passedString += "&strwhere=" + Server.HtmlEncode(strwhere);
            passedString += "&mptype=" + Server.HtmlEncode(mptype);
            passedString += "&mpchange=" + Server.HtmlEncode(mpchange);
            passedString += "&mpno=" + Server.HtmlEncode(mpno);
            passedString += "&pjid=" + Server.HtmlEncode(pjid);
            passedString += "&engtype=" + Server.HtmlEncode(engtype);
            passedString += "&engid=" + Server.HtmlEncode(engid);
            passedString += "&orgtable=" + Server.HtmlEncode(tablename);

            Response.Redirect(passedString);

        }

        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_mp_OnClick(object sender, EventArgs e)
        {
            CheckBoxSelectDefine(GridView2, "CheckBox1");
        }
        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnSave_Replace(object sender, EventArgs e)
        //{
        //    if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
        //        return;
        //    }

        //    string sqltext = "";
        //    List<string> list_sql = new List<string>();

        //    foreach (GridViewRow grow in GridView1.Rows)
        //    {
        //        CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
        //        if (ckb.Checked)
        //        {
        //            string xuhao = grow.Cells[6].Text.Trim();

        //            if (ddlReplaceType.SelectedValue == "Replace")
        //            {
        //                sqltext = "update " + tablename + " set " + ddlRepType.SelectedValue + "=REPLACE(" + ddlRepType.SelectedValue + ",'" + txtStart.Text.Trim() + "','" + txtEnd.Text.Trim() + "') WHERE BM_ENGID='" + tsaid.Text.Trim() + "' AND BM_XUHAO='" + xuhao + "'";
        //            }
        //            else if (ddlReplaceType.SelectedValue == "Add")
        //            {
        //                sqltext = "update " + tablename + " set " + ddlRepType.SelectedValue + "=" + ddlRepType.SelectedValue + "+'  " + txtStart.Text.Trim() + "' WHERE BM_ENGID='" + tsaid.Text.Trim() + "' AND BM_XUHAO='" + xuhao + "'";
        //            }

        //            if (ddlRepType.SelectedValue == "BM_TUHAO")
        //            {
        //                sqltext += " AND BM_MSSTATE='0' AND BM_OSSTATE='0'";
        //            }
        //            //else if (ddlRepType.SelectedValue == "BM_MASHAPE" || ddlRepType.SelectedValue == "BM_MASTATE")
        //            //{
        //            //    sqltext+=" AND BM_MPSTATE='0' AND BM_OSSTATE='0' AND BM_MSSTATE='0'";
        //            //}
        //            list_sql.Add(sqltext);
        //        }
        //    }
        //    try
        //    {
        //        DBCallCommon.ExecuteTrans(list_sql);
        //        this.btnOrgQuery_OnClick(null, null);
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
        //    }
        //    catch (Exception)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未知错误，请重试或联系管理员！！！');", true);
        //    }
        //}

        private void InitTreeViewMp(string FZ_condition)
        {

            TreeViewMp.Nodes.Clear();
            TreeNode tnode = new TreeNode("部件(序号-总序-名称)", "0");
            TreeViewMp.Nodes.Add(tnode);
            TreeNode tn = TreeViewMp.Nodes[0];
            tn.PopulateOnDemand = false;
            string sql = "select case when (dbo.Splitnum(BM_XUHAO,'.')=0) then '0' else substring(BM_XUHAO,1,len(BM_XUHAO)-charindex('.',reverse(BM_XUHAO))) end as BM_FXUHAO,BM_XUHAO,(CASE WHEN BM_ZONGXU='' THEN '总序空' ELSE BM_ZONGXU END) AS BM_ZONGXU,BM_CHANAME AS  BM_NAME,dbo.Splitnum(BM_XUHAO,'.') as nParent from " + tablename + " where BM_ENGID='" + tsaid.Text.Trim() + "' AND dbo.Splitnum(BM_XUHAO,'.')<=" + ddlJishuMp.SelectedValue + "  AND dbo.Splitnum(BM_XUHAO,'.')>0  ORDER BY dbo.f_formatstr(BM_ZONGXU, '.')";
            DataTable xuhaoDataTable = DBCallCommon.GetDTUsingSqlText(sql);
            this.InitTreeMp(xuhaoDataTable, tn.ChildNodes, 1, "0");
        }

        protected void InitTreeMp(DataTable xuhaoDataTable, TreeNodeCollection Nds, int nParentID, string Fxuhao)//nParentID父节点ID
        {
            DataView dv = new DataView();
            TreeNode tmpNd;
            //int strId;
            dv.Table = xuhaoDataTable;
            dv.RowFilter = "nParent='" + nParentID + "' and BM_FXUHAO='" + Fxuhao + "'";

            foreach (DataRowView objRow in dv)
            {
                tmpNd = new TreeNode();
                tmpNd.Text = objRow["BM_XUHAO"].ToString() + "-" + objRow["BM_ZONGXU"].ToString() + "-" + objRow["BM_NAME"].ToString();
                tmpNd.Value = objRow["BM_ZONGXU"].ToString();
                tmpNd.ShowCheckBox = true;
                Nds.Add(tmpNd);
                this.InitTreeMp(xuhaoDataTable, Nds[Nds.Count - 1].ChildNodes, ((int)objRow["nParent"]) + 1, objRow["BM_XUHAO"].ToString());
            }
        }
        protected void ddlJishuMp_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitTreeViewMp("");
        }
        #endregion

        #region 制作明细标签
        /// <summary>
        /// 制作明细标签,数据读取
        /// </summary>
        private void GetMS()
        {
            ViewState["Ms"] = "BM_ENGID='" + tsaid.Text + "'";
            ////////UCPagingMS.CurrentPage = 1;
            ////////this.InitVarMS();
            ////////this.bindGridMS();
            this.BtnNormalChangeVisible("MS");
        }
        /// <summary>
        /// 设置标签颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView4_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((HiddenField)e.Row.Cells[0].FindControl("hdfMSState")).Value == "1")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Green;//已提
                }

                HiddenField hfmpchg = (HiddenField)e.Row.Cells[0].FindControl("hdfMSChg");

                if (hfmpchg.Value == "1")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Gray;//制作明细变更删除
                }
                else if (hfmpchg.Value == "2")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Orange;//制作明细变更增加
                }
                else if (hfmpchg.Value == "3")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;//制作明细变更修改
                }

            }
        }

        #region 制作明细标签DropDownList绑定

        private void MsDropDownListBind()
        {
            this.GetPartsName(ddpMSbjName);
            this.GetShebeiName(ddlShebei);
        }

        private void GetShebeiName(DropDownList ddlShebei)
        {
            sqlText = "select BM_TUHAO,BM_CHANAME+'|'+BM_TUHAO AS BM_NAME from TBPM_STRINFODQO where BM_ENGID='" + tsaid.Text + "' and dbo.Splitnum(BM_ZONGXU,'.')<2";
            string dataText = "BM_NAME";
            string dataValue = "BM_NAME";
            DBCallCommon.BindDdl(ddlShebei, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 制作明细标签部件查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddpMSbjName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMSChange.SelectedValue == "1")//变更
            {
                tr_1.Visible = false;

                tr_4.Visible = false;
                tr_5.Visible = true;
                this.InitTreeViewMs("");
            }
            else
            {
                tr_1.Visible = true;

                tr_4.Visible = true;
                tr_5.Visible = false;
            }


            StringBuilder strb_mp = new StringBuilder();
            strb_mp.Append(" BM_ENGID='" + tsaid.Text + "'");

            if (ddpMSbjName.SelectedIndex != 0)//部件名称
            {
                string a = " AND BM_ZONGXU LIKE '" + ddpMSbjName.SelectedValue.ToString() + "%'";
                strb_mp.Append(a);
            }

            if (ddlMSinMS.SelectedIndex != 0)//是否体现在制作明细中
            {
                string b = " AND BM_ISMANU='" + ddlMSinMS.SelectedValue.ToString() + "'";
                strb_mp.Append(b);
            }

            if (ddlMSChange.SelectedIndex != 0)//变更状态
            {
                string d = "";
                if (ddlMSChange.SelectedValue == "0")
                {
                    d = " AND BM_MSSTATUS='0' AND BM_MSSTATE='0'";
                }
                else if (ddlMSChange.SelectedValue == "1")
                {
                    d = " AND BM_MSSTATUS!='0' AND BM_MSSTATE='0'";
                }
                strb_mp.Append(d);
            }


            udqMS.ExistedConditions = strb_mp.ToString();
            ViewState["Ms"] = strb_mp.ToString() + UserDefinedQueryConditions.ReturnQueryString((GridView)udqMS.FindControl("GridView1"), (Label)udqMS.FindControl("Label1"));
            UCPagingMS.CurrentPage = 1;
            this.InitVarMS();
            this.bindGridMS();
            this.BtnNormalChangeVisible("MS");
        }
        /// <summary>
        /// 制造明细清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMSClear_OnClick(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqMS.FindControl("GridView1"));
        }

        #endregion

        /// <summary>
        /// 生成制作明细>转到调整界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mssubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
                return;
            }

            #region
            ////this.GetListName();
            ////if (ddlAdjustPart.SelectedValue == "0")//自定义调整
            ////{
            ////    int selected_parts = 0;
            ////    string xuhao_str = "";
            ////    for (int i = 0; i < cblXuHao.Items.Count; i++)
            ////    {
            ////        if (cblXuHao.Items[i].Selected && cblXuHao.Items[i].Enabled)
            ////        {
            ////            selected_parts++;
            ////            xuhao_str += cblXuHao.Items[i].Value + "$";
            ////        }
            ////    }
            ////    xuhao_str += tsaid.Text + "$" + tablename;
            ////    if (selected_parts > 0&&selected_parts<=25)
            ////    {
            ////        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "location.href='" + "TM_MS_TEMP_InitData.aspx?NorMS=" + xuhao_str + "'", true);
            ////    }
            ////    else if (selected_parts > 25)
            ////    {
            ////        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('最多只能勾选25条记录！！！')", true);
            ////    }
            ////    else if (selected_parts == 0)
            ////    {
            ////        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要调整的部件序号！！！');", true);
            ////    }
            ////}
            ////else
            ////{
            ////    string[] aa = tsaid.Text.Split('-');
            ////    string xuhao = "1";
            ////    xuhao += "$"+tsaid.Text + "$" + tablename;
            ////    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "location.href='" + "TM_MS_TEMP_InitData.aspx?NorMS=" + xuhao + "'", true);
            ////}
            #endregion

            if (ddlAdjustPart.SelectedValue == "0")//自定义调整
            {
                int selected_parts = 0;
                string xuhao_str = "";

                int first_level_count = TreeView1.Nodes[0].ChildNodes.Count;

                for (int i = 0; i < first_level_count; i++)
                {
                    TreeNode childnode = (TreeNode)TreeView1.Nodes[0].ChildNodes[i];
                    xuhao_str += this.GetChildNodeXuhao(childnode, "");
                }
                xuhao_str = this.get_xu_without_xia(xuhao_str);

                xuhao_str += tsaid.Text + "$" + tablename;
                selected_parts = xuhao_str.Split('$').Length - 2;

                if (selected_parts > 0 && selected_parts <= 40000)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "location.href='" + "TM_MS_WaitforCreate.aspx?NorMS=" + xuhao_str + "'", true);
                }
                else if (selected_parts > 40000)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('最多只能勾选40000条记录！！！')", true);
                }
                else if (selected_parts == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要调整的部件序号！！！');", true);
                }
            }
            else
            {

                string xuhao = "";
                xuhao += "$" + tsaid.Text + "$" + tablename;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "location.href='" + "TM_MS_WaitforCreate.aspx?NorMS=" + xuhao + "'", true);
            }
        }

        protected string GetChildNodeXuhao(TreeNode childnodes, string retValue)
        {
            if (childnodes.Checked)
            {
                retValue += childnodes.Value + "$";
            }
            else
            {
                TreeNode tn_child = childnodes;
                while (tn_child.ChildNodes.Count > 0)
                {
                    foreach (TreeNode tn in tn_child.ChildNodes)
                    {
                        tn_child = tn;
                        retValue = GetChildNodeXuhao(tn_child, retValue);
                    }
                }
            }
            return retValue;
        }



        //2016.10.25修改，对传参修改，最后一个存在将字节点传入的情况，下面方法避免
        protected string get_xu_without_xia(string nodes_xh)
        {
            string nodes_xh_split_1 = "";
            string nodes_xh_split_2 = "";
            string nodes_return = "";
            string nodes_temp = "0";
            string[] nodes_xh_split = nodes_xh.Split(new string[] { "$" }, StringSplitOptions.RemoveEmptyEntries);
            if (nodes_xh_split.Length == 1)
            {
                nodes_return = nodes_xh + "$";
            }
            else
            {
                for (int i = 0; i < nodes_xh_split.Length - 1; i++)
                {
                    if (nodes_temp == "0")
                    {
                        nodes_xh_split_1 = nodes_xh_split[i];
                        nodes_xh_split_2 = nodes_xh_split[i + 1];
                        if (nodes_xh_split_1.Length > nodes_xh_split_2.Length)
                        {
                            nodes_return += nodes_xh_split_1 + "$";
                        }
                        else if (nodes_xh_split_2.Substring(0, nodes_xh_split_1.Length) != nodes_xh_split_1)
                        {
                            nodes_return += nodes_xh_split_1 + "$";
                        }
                        else
                        {
                            nodes_temp = nodes_xh_split_1;
                            nodes_return += nodes_temp + "$";
                        }
                        if (i == nodes_xh_split.Length - 2)
                        {
                            nodes_return += nodes_xh_split_2 + "$";
                        }
                    }
                    else
                    {

                        nodes_xh_split_2 = nodes_xh_split[i + 1];
                        if (nodes_temp.Length > nodes_xh_split_2.Length)
                        {
                            nodes_temp = "0";
                        }
                        if (nodes_xh_split_2.Substring(0, nodes_temp.Length) != nodes_temp)
                        {
                            nodes_temp = "0";
                        }
                        if (i == nodes_xh_split.Length - 2 && nodes_temp == "0")
                        {
                            nodes_return += nodes_xh_split_2 + "$";
                        }

                    }

                }
            }
            return nodes_return;
        }

        private void InitTreeView(string FZ_condition)
        {
            TreeView1.Nodes.Clear();
            TreeNode tnode = new TreeNode("未提交明细(序号-总序-名称)", "0");
            TreeView1.Nodes.Add(tnode);
            TreeNode tn = TreeView1.Nodes[0];
            tn.PopulateOnDemand = false;
            string sql = "select  case when (dbo.Splitnum(BM_XUHAO,'.')=0) then '0' else substring(BM_XUHAO,1,len(BM_XUHAO)-charindex('.',reverse(BM_XUHAO))) end as BM_FXUHAO,BM_ZONGXU,CASE  WHEN BM_MSSTATUS!='0' THEN BM_CHANAME+'(变更)' WHEN BM_MSSTATE='0' THEN BM_CHANAME ELSE BM_CHANAME+'(已提交)' END AS  BM_NAME,BM_MSSTATE,dbo.Splitnum(BM_XUHAO,'.') as nParent from " + tablename + " where BM_ENGID='" + tsaid.Text + "' AND dbo.Splitnum(BM_XUHAO,'.')<=3   ORDER BY dbo.f_formatstr(BM_ZONGXU, '.')"; tn.PopulateOnDemand = false;
            DataTable xuhaoDataTable = DBCallCommon.GetDTUsingSqlText(sql);
            this.InitTree(xuhaoDataTable, tn.ChildNodes, 0, "0");
            TreeView1.CollapseAll();
        }

        protected void InitTree(DataTable xuhaoDataTable, TreeNodeCollection Nds, int nParentID, string Fxuhao)//nParentID父节点ID
        {
            DataView dv = new DataView();
            TreeNode tmpNd;
            //int strId;
            dv.Table = xuhaoDataTable;

            dv.RowFilter = "  nParent='" + nParentID + "' and BM_FXUHAO='" + Fxuhao + "'";


            foreach (DataRowView objRow in dv)
            {
                tmpNd = new TreeNode();
                tmpNd.Text = objRow["BM_ZONGXU"].ToString() + "-" + objRow["BM_NAME"].ToString();
                tmpNd.Value = objRow["BM_ZONGXU"].ToString();
                tmpNd.ShowCheckBox = true;
                Nds.Add(tmpNd);
                this.InitTree(xuhaoDataTable, Nds[Nds.Count - 1].ChildNodes, ((int)objRow["nParent"]) + 1, objRow["BM_ZONGXU"].ToString());
            }
        }
        /// <summary>
        /// 跳转到制作明细变更变更调整界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
                return;
            }

            int first_level_count = TreeViewMsChange.Nodes[0].ChildNodes.Count;
            string xuhao_str = "";
            for (int i = 0; i < first_level_count; i++)
            {
                TreeNode childnode = (TreeNode)TreeViewMsChange.Nodes[0].ChildNodes[i];
                xuhao_str += this.GetChildNodeXuhao(childnode, "");
            }
            xuhao_str = this.get_xu_without_xia(xuhao_str);


            int selected_parts = xuhao_str.Split('$').Length - 1;

            if (selected_parts > 0 && selected_parts <= 40000)
            {
                xuhao_str = xuhao_str.Substring(0, xuhao_str.Length - 1);
                Response.Redirect("TM_MS_WaitforCreate_Change.aspx?TaskID=" + tsaid.Text + "&Xuhao=" + xuhao_str + "");
            }
            else if (selected_parts > 40000)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('最多只能勾选40000条记录！！！')", true);
            }
            else if (selected_parts == 0)
            {
                Response.Redirect("TM_MS_WaitforCreate_Change.aspx?TaskID=" + tsaid.Text + "&Xuhao=0");
            }
        }

        /// <summary>
        /// 导出制作明细表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportMs_OnClick(object sender, EventArgs e)
        {
            if (ViewState["TSA_TCCLERK"].ToString() != Session["UserID"].ToString())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行该项操作！！！');", true);
                return;
            }

            ExportTMDataFromDB.ExportMsFromOrg(tsaid.Text);
        }

        private void InitTreeViewMs(string FZ_condition)
        {

            TreeViewMsChange.Nodes.Clear();
            TreeNode tnode = new TreeNode("部件(序号-总序-名称)", "0");
            TreeViewMsChange.Nodes.Add(tnode);
            TreeNode tn = TreeViewMsChange.Nodes[0];
            tn.PopulateOnDemand = false;
            string sql = "select case when (dbo.Splitnum(BM_XUHAO,'.')=0) then '0' else substring(BM_XUHAO,1,len(BM_XUHAO)-charindex('.',reverse(BM_XUHAO))) end as BM_FXUHAO,BM_XUHAO,(CASE WHEN BM_ZONGXU='' THEN '总序空' ELSE BM_ZONGXU END) AS BM_ZONGXU,BM_CHANAME AS  BM_NAME,dbo.Splitnum(BM_XUHAO,'.') as nParent from " + tablename + " where BM_ENGID='" + tsaid.Text.Trim() + "'  AND dbo.Splitnum(BM_XUHAO,'.')<=3   ORDER BY dbo.f_formatstr(BM_ZONGXU, '.')";
            DataTable xuhaoDataTable = DBCallCommon.GetDTUsingSqlText(sql);
            this.InitTreeMs(xuhaoDataTable, tn.ChildNodes, 0, "0");
        }
        private void InitTreeMp(string FZ_condition)
        {

            TreeMp.Nodes.Clear();
            TreeNode tnode = new TreeNode("部件(序号-总序-名称)", "0");
            TreeMp.Nodes.Add(tnode);
            TreeNode tn = TreeMp.Nodes[0];
            tn.PopulateOnDemand = false;
            string sql = "select substring(BM_XUHAO,1,len(BM_XUHAO)-charindex('.',reverse(BM_XUHAO))) AS BM_FXUHAO,BM_XUHAO,(CASE WHEN BM_ZONGXU='' THEN '总序空' ELSE BM_ZONGXU END) AS BM_ZONGXU,BM_CHANAME AS  BM_NAME,dbo.Splitnum(BM_XUHAO,'.') as nParent from " + tablename + " where BM_ENGID='" + tsaid.Text.Trim() + "'   AND dbo.Splitnum(BM_XUHAO,'.')<=3  AND dbo.Splitnum(BM_XUHAO,'.')>0  ORDER BY dbo.f_formatstr(BM_ZONGXU, '.')";
            DataTable xuhaoDataTable = DBCallCommon.GetDTUsingSqlText(sql);
            this.InitTreeMs(xuhaoDataTable, tn.ChildNodes, 1, "1");
        }

        protected void InitTreeMs(DataTable xuhaoDataTable, TreeNodeCollection Nds, int nParentID, string Fxuhao)//nParentID父节点ID
        {
            DataView dv = new DataView();
            TreeNode tmpNd;
            //int strId;
            dv.Table = xuhaoDataTable;
            dv.RowFilter = "nParent='" + nParentID + "' and BM_FXUHAO='" + Fxuhao + "'";

            foreach (DataRowView objRow in dv)
            {
                tmpNd = new TreeNode();
                tmpNd.Text = objRow["BM_XUHAO"].ToString() + "-" + objRow["BM_ZONGXU"].ToString() + "-" + objRow["BM_NAME"].ToString();
                tmpNd.Value = objRow["BM_XUHAO"].ToString();
                tmpNd.ShowCheckBox = true;
                Nds.Add(tmpNd);
                this.InitTreeMs(xuhaoDataTable, Nds[Nds.Count - 1].ChildNodes, ((int)objRow["nParent"]) + 1, objRow["BM_XUHAO"].ToString());
            }
        }


        #region 制作明细标签 分页
        PagerQueryParam pager_ms = new PagerQueryParam();

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVarMS()
        {
            InitPagerMS();
            UCPagingMS.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
            UCPagingMS.PageSize = pager_ms.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPagerMS()
        {

            pager_ms.TableName = viewtable;
            pager_ms.PrimaryKey = "BM_ID";
            pager_ms.ShowFields = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) AS NUMBER";
            pager_ms.OrderField = "dbo.f_formatstr(BM_XUHAO, '.')";
            pager_ms.StrWhere = ViewState["Ms"].ToString();
            pager_ms.OrderType = 0;//升序排列
            pager_ms.PageSize = 30;
        }
        void Pager_PageChangedMS(int pageNumber)
        {
            bindGridMS();
        }
        private void bindGridMS()
        {
            pager_ms.PageIndex = UCPagingMS.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_ms);
            CommonFun.Paging(dt, GridView4, UCPagingMS, NoDataPanel3);
            if (NoDataPanel3.Visible)
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

        #endregion




        protected void InitTreeOut(DataTable xuhaoDataTable, TreeNodeCollection Nds, int nParentID, string Fxuhao)//nParentID父节点ID
        {
            DataView dv = new DataView();
            TreeNode tmpNd;
            //int strId;
            dv.Table = xuhaoDataTable;
            dv.RowFilter = "nParent='" + nParentID + "' and BM_FXUHAO='" + Fxuhao + "'";

            foreach (DataRowView objRow in dv)
            {
                tmpNd = new TreeNode();
                tmpNd.Text = objRow["BM_XUHAO"].ToString() + "-" + objRow["BM_ZONGXU"].ToString() + "-" + objRow["BM_NAME"].ToString();
                tmpNd.Value = objRow["BM_ZONGXU"].ToString();
                tmpNd.ShowCheckBox = true;
                Nds.Add(tmpNd);
                this.InitTreeOut(xuhaoDataTable, Nds[Nds.Count - 1].ChildNodes, ((int)objRow["nParent"]) + 1, objRow["BM_XUHAO"].ToString());
            }
        }


        #region 公共
        /// <summary>
        /// 变更按钮与正常按钮的可见性
        /// </summary>
        private void BtnNormalChangeVisible(string type)
        {
            switch (type)
            {
                case "MP":
                    mpsubmit.Visible = true;
                    mpChange.Visible = true;
                    mpCutDown.Visible = true;

                    if (ddlChange.SelectedValue == "-请选择-")
                    {
                        mpsubmit.Visible = false;
                        mpChange.Visible = false;
                        mpCutDown.Visible = false;
                    }
                    else if (ddlChange.SelectedValue == "0")//正常
                    {
                        mpChange.Visible = false;
                        mpCutDown.Visible = false;
                        if (GridView2.Rows.Count == 0)
                        {
                            mpsubmit.Visible = false;
                        }
                    }
                    else if (ddlChange.SelectedValue == "1")//变更
                    {
                        mpsubmit.Visible = false;
                        mpCutDown.Visible = false;
                        if (GridView2.Rows.Count == 0)
                        {
                            mpChange.Visible = false;
                        }
                    }
                    else if (ddlChange.SelectedValue == "2")//取消
                    {
                        mpsubmit.Visible = false;
                        mpChange.Visible = false;
                        if (GridView2.Rows.Count == 0)
                        {
                            mpCutDown.Visible = false;
                        }
                    }
                    break;
                case "MS":
                    chSubmit.Visible = true;
                    mssubmit.Visible = true;
                    if (ddlMSChange.SelectedIndex == 0)
                    {
                        chSubmit.Visible = false;
                        mssubmit.Visible = false;
                    }
                    else if (ddlMSChange.SelectedValue == "0")//正常
                    {
                        chSubmit.Visible = false;
                        if (GridView4.Rows.Count == 0)
                        {
                            mssubmit.Visible = false;
                        }
                    }
                    else if (ddlMSChange.SelectedValue == "1")//变更
                    {
                        mssubmit.Visible = false;
                        if (GridView4.Rows.Count == 0)
                        {
                            chSubmit.Visible = false;
                        }
                    }
                    break;

                case "OS":

                    break;
                default: break;
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
        #endregion

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
                case "UCPagingMP":
                    contrl[0] = (UCPaging)UCPagingMP;//材料计划
                    contrl[1] = (GridView)GridView2;
                    contrl[2] = (Panel)NoDataPanel2;
                    break;
                case "UCPagingMS":
                    contrl[0] = (UCPaging)UCPagingMS;//材料计划
                    contrl[1] = (GridView)GridView4;
                    contrl[2] = (Panel)NoDataPanel3;
                    break;
                default:
                    contrl[0] = (UCPaging)UCPagingOrg;//原始数据
                    contrl[1] = (GridView)GridView1;
                    contrl[2] = (Panel)NoDataPanel1;
                    break;
            }
            return contrl;
        }

        /// <summary>
        /// CheckBox连选(此函数勿动)
        /// </summary>
        /// <param name="smartgridview"></param>
        /// <param name="ckbname"></param>
        public void CheckBoxSelectDefine(GridView smartgridview, string ckbname)
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

        protected void ExecuteTrans(List<string> sqlTexts)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlConn.Open();

            //启用事务
            SqlTransaction sqlTran = sqlConn.BeginTransaction();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandTimeout = 10000;
            sqlCmd.Transaction = sqlTran;
            try
            {
                foreach (string sqlText in sqlTexts)
                {
                    sqlCmd.CommandText = sqlText;
                    sqlCmd.ExecuteNonQuery();
                }
                sqlTran.Commit();
            }
            catch (Exception)
            {
                sqlTran.Rollback();
                throw;
            }
            finally
            {
                DBCallCommon.closeConn(sqlConn);
            }
        }

        #region DropDownList绑定
        /// <summary>
        /// 绑定部件名称
        /// </summary>
        private void GetPartsName(DropDownList bjname)
        {
            sqlText = "select BM_ZONGXU+'|'+BM_CHANAME AS BM_CHANAME,BM_ZONGXU from " + viewtable + " ";
            sqlText += "where BM_ENGID='" + tsaid.Text + "' and  (BM_MARID='' or BM_MARID is null) order by BM_ZONGXU,BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "BM_CHANAME";
            string dataValue = "BM_ZONGXU";
            DBCallCommon.BindDdl(bjname, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 绑定材料名称
        /// </summary>
        private void GetNameData(DropDownList clname)
        {
            sqlText = "select distinct BM_MANAME collate  Chinese_PRC_CS_AS_KS_WS as BM_MANAME from " + viewtable + " ";
            sqlText += "where BM_ENGID='" + tsaid.Text + "' and BM_MANAME is not null order by BM_MANAME collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "BM_MANAME";
            string dataValue = "BM_MANAME";
            DBCallCommon.BindDdl(clname, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 绑定材料规格
        /// </summary>
        private void GetGuigeData(DropDownList gg)
        {
            sqlText = "select distinct BM_MAGUIGE collate  Chinese_PRC_CS_AS_KS_WS as BM_MAGUIGE from " + viewtable + " ";
            sqlText += "where BM_ENGID='" + tsaid.Text + "' and BM_MAGUIGE is not null  order by BM_MAGUIGE collate  Chinese_PRC_CS_AS_KS_WS ";
            string dataText = "BM_MAGUIGE";
            string dataValue = "BM_MAGUIGE";
            DBCallCommon.BindDdl(gg, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 绑定材料材质
        /// </summary>
        private void GetCaizhiData(DropDownList cz)
        {
            sqlText = "select distinct BM_MAQUALITY collate  Chinese_PRC_CS_AS_KS_WS as BM_MAQUALITY from " + viewtable + " ";
            sqlText += "where BM_ENGID='" + tsaid.Text + "' and BM_MAQUALITY is not null order by BM_MAQUALITY collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "BM_MAQUALITY";
            string dataValue = "BM_MAQUALITY";
            DBCallCommon.BindDdl(cz, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 材料形状
        /// </summary>
        private void GetOrgShape(DropDownList shape)
        {
            sqlText = "select distinct BM_MASHAPE collate  Chinese_PRC_CS_AS_KS_WS as BM_MASHAPE from " + viewtable + " ";
            sqlText += "where BM_ENGID='" + tsaid.Text + "' and BM_MASHAPE is not null order by BM_MASHAPE collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "BM_MASHAPE";
            string dataValue = "BM_MASHAPE";
            DBCallCommon.BindDdl(shape, sqlText, dataText, dataValue);
        }



        #endregion

        /// <summary>
        /// 返回TreeView查询子串
        /// </summary>
        /// <param name="treeName"></param>
        /// <returns></returns>
        protected string QueryStringTreeView(TreeView treeName)
        {
            int first_level_count = treeName.Nodes[0].ChildNodes.Count;
            string xuhao_str = "";
            for (int i = 0; i < first_level_count; i++)
            {
                TreeNode childnode = (TreeNode)treeName.Nodes[0].ChildNodes[i];
                xuhao_str += this.GetChildNodeZongxu(childnode, "");
            }

            if (xuhao_str != "")
            {
                xuhao_str = " AND (" + xuhao_str.Substring(0, xuhao_str.Length - 4) + ")";
            }
            return xuhao_str;
        }

        protected string GetChildNodeZongxu(TreeNode childnodes, string retValue)
        {
            if (childnodes.Checked)
            {
                retValue += "  BM_ZONGXU='" + childnodes.Value + "' OR BM_ZONGXU LIKE '" + childnodes.Value + ".%'  OR ";
            }
            else
            {
                TreeNode tn_child = childnodes;
                while (tn_child.ChildNodes.Count > 0)
                {
                    foreach (TreeNode tn in tn_child.ChildNodes)
                    {
                        tn_child = tn;
                        retValue = GetChildNodeZongxu(tn_child, retValue);
                    }
                }
            }
            return retValue;
        }

        #endregion

        protected void btnCalWeight_Click(object sender, EventArgs e)
        {
            sqlText = "select sum(BM_TUTOTALWGHT),sum(BM_MATOTALWGHT) from TBPM_STRINFODQO where BM_ENGID='" + tsaid.Text + "' and " + ViewState["Org"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                txtTotalTu.Value = dt.Rows[0][0].ToString();
                txtTotalMP.Value = dt.Rows[0][1].ToString();
            }
        }

        protected void btnChangeMap_Click(object sender, EventArgs e)
        {
            string sql = string.Format("update TBPM_STRINFODQO set {3}='{0}' where BM_ENGID='{1}' and {3}='{2}'", txtChangeMap.Value, tsaid.Text, txtEquipMap.Value, ddlChangeItem.SelectedValue);
            DBCallCommon.ExeSqlText(sql);
            this.ddlmatername_SelectedIndexChanged(null, null);
        }
    }
}
