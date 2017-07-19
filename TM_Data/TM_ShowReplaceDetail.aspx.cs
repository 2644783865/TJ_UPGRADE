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
    public partial class TM_ShowReplaceDetail : System.Web.UI.Page
    {
        string viewtable;
        string tablename;
        string mptable;
        string mstable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
            if (IsPostBack)
            {
                this.PagePostBack();
            }

        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        protected void InitPage()
        {
            string tsa_id = "";
            string[] fields;
            string sqlText = "";
            tsa_id = Request.QueryString["TaskID"].ToString();
            ViewState["Marid"] = Request.QueryString["Marid"].ToString();
            fields = tsa_id.Split('-');
            sqlText = "select CM_PROJ,TSA_ENGNAME,TSA_PJID ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;

                proname.Text = dr[0].ToString();
                engname.Text = dr[1].ToString();
               
            }
            dr.Close();
            this.GetListName();
            this.AbleToReplace();
            this.UnalbeToReplace();
        }


        protected void AbleToReplace()
        {
            string sql = "BM_ENGID='" + tsaid.Text + "' and BM_MARID='" + ViewState["Marid"].ToString() + "' AND BM_MSSTATE='0' AND BM_MPSTATE='0' AND BM_OSSTATE='0'";
            ViewState["able"] = sql;
            UCPaging1.CurrentPage = 1;
            this.InitVar(UCPaging1, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["able"].ToString(), 0, 30);
            this.bindGrid(UCPaging1, SmartGridView1, NoDataPanel1);
            ViewState["CurrentUCPaging"] = "UCPaging1";
        }

        protected void UnalbeToReplace()
        {
            string sql = "BM_ENGID='" + tsaid.Text + "' and BM_MARID='" + ViewState["Marid"].ToString() + "' AND (BM_MSSTATE<>'0' OR BM_MPSTATE<>'0' OR BM_OSSTATE<>'0')";
            ViewState["unalble"] = sql;
            UCPaging2.CurrentPage = 1;
            this.InitVar(UCPaging2, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["unalble"].ToString(), 0, 30);
            this.bindGrid(UCPaging2, SmartGridView2, NoDataPanel2);
            ViewState["CurrentUCPaging"] = "UCPaging2";
        }
        /// <summary>
        /// 初始化表名
        /// </summary>
        private void GetListName()
        {
            viewtable = "View_TM_DQO";
            tablename = "TBPM_STRINFODQO";
            mptable = "TBPM_MPFORHZY";
            mstable = "TBPM_MKDETAIL";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _xuhao = e.Row.Cells[6].Text.Trim();

                HiddenField hdforgstate = (HiddenField)e.Row.FindControl("hdfOrgState");

                string url = e.Row.Cells[6].Text.Trim() + ' ' + tsaid.Text + ' ' + tablename + ' ' + mptable + ' ' + mstable + ' ' + viewtable;
                for (int i = 9; i < 20; i++)
                {
                    e.Row.Cells[i].Attributes["style"] = "Cursor:hand";

                    e.Row.Cells[i].Attributes.Add("title", "双击修改原始数据");

                    e.Row.Cells[i].Attributes["onclick"] = String.Format("javascript:setTimeout(\"if(dbl_click){{dbl_click=false;}}else{{{0}}};\", 1000000000000000);", ClientScript.GetPostBackEventReference(SmartGridView1, "Select$" + e.Row.RowIndex.ToString(), true));
                    // 双击，设置 dbl_click=true，以取消单击响应
                    e.Row.Cells[i].Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLink('" + url + "');", SmartGridView1.DataKeys[e.Row.RowIndex].Value.ToString());

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
                e.Row.Cells[3].Attributes.Add("title", "双击下查材料计划");
                e.Row.Cells[3].Attributes.Add("ondblclick", "MS_DownWardQuery('" + xuhao_engid_table + "')");
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
                e.Row.Cells[4].Attributes["style"] = "Cursor:hand";
                e.Row.Cells[4].Attributes.Add("title", "双击下查材料计划");
                e.Row.Cells[4].Attributes.Add("ondblclick", "OUT_DownWardQuery('" + xuhao_engid + "')");
                #endregion

                //含物料编码项，查找代用
                #region
                string _marid = e.Row.Cells[8].Text.Trim();
                if (_marid != "" || _marid != "&nbsp;")
                {
                    //获取物料对应的计划跟踪号
                    string sql_findtracknum = "";
                    string _replacefor_xuhao = _xuhao + "," + tsaid.Text.Trim() + "," + _marid;
                    if (color[0] != "0" || color[1] != "0")//表明提交计划类型为材料计划
                    {
                        _replacefor_xuhao += "," + "View_TM_MPHZY";
                        sql_findtracknum = "select MP_TRACKNUM from View_TM_MPHZY where MP_NEWXUHAO='" + _xuhao + "' and MP_MARID='" + _marid + "' and MP_STATERV='8' AND MP_STATUS='0'";
                    }
                    else if (color[4] != "0" || color[5] != "0")//表明计划类型为外协计划
                    {
                        _replacefor_xuhao += "," + "View_TM_OUTSOURCELIST";
                        sql_findtracknum = "select OSL_TRACKNUM from View_TM_OUTSOURCELIST where OSL_NEWXUHAO='" + _xuhao + "' and OSL_MARID='" + _marid + "' and OST_STATE='8' AND OSL_STATUS='0'";
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
                                e.Row.Cells[8].BackColor = System.Drawing.Color.YellowGreen;
                                e.Row.Cells[8].ToolTip = "双击查看代用计划";
                            }
                        }
                        e.Row.Cells[8].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[8].Attributes.Add("ondblclick", "MarReplace_DownWardQuery('" + _replacefor_xuhao + "')");
                    }
                }
                #endregion

                //含物料编码不提计划项标识
                if (color[7].ToString() != "" && color[8] == "N")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[1].Attributes.Add("title", "不提材料计划");
                }
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _xuhao = e.Row.Cells[6].Text.Trim();

                HiddenField hdforgstate = (HiddenField)e.Row.FindControl("hdfOrgState");

                string url = e.Row.Cells[6].Text.Trim() + ' ' + tsaid.Text + ' ' + tablename + ' ' + mptable + ' ' + mstable + ' ' + viewtable;
                for (int i = 9; i < 20; i++)
                {
                    e.Row.Cells[i].Attributes["style"] = "Cursor:hand";

                    e.Row.Cells[i].Attributes.Add("title", "双击修改原始数据");

                    e.Row.Cells[i].Attributes["onclick"] = String.Format("javascript:setTimeout(\"if(dbl_click){{dbl_click=false;}}else{{{0}}};\", 1000000000000000);", ClientScript.GetPostBackEventReference(SmartGridView2, "Select$" + e.Row.RowIndex.ToString(), true));
                    // 双击，设置 dbl_click=true，以取消单击响应
                    e.Row.Cells[i].Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLink('" + url + "');", SmartGridView2.DataKeys[e.Row.RowIndex].Value.ToString());

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
                e.Row.Cells[3].Attributes.Add("title", "双击下查材料计划");
                e.Row.Cells[3].Attributes.Add("ondblclick", "MS_DownWardQuery('" + xuhao_engid_table + "')");
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
                e.Row.Cells[4].Attributes["style"] = "Cursor:hand";
                e.Row.Cells[4].Attributes.Add("title", "双击下查材料计划");
                e.Row.Cells[4].Attributes.Add("ondblclick", "OUT_DownWardQuery('" + xuhao_engid + "')");
                #endregion

                //含物料编码项，查找代用
                #region
                string _marid = e.Row.Cells[8].Text.Trim();
                if (_marid != "" || _marid != "&nbsp;")
                {
                    //获取物料对应的计划跟踪号
                    string sql_findtracknum = "";
                    string _replacefor_xuhao = _xuhao + "," + tsaid.Text.Trim() + "," + _marid;
                    if (color[0] != "0" || color[1] != "0")//表明提交计划类型为材料计划
                    {
                        _replacefor_xuhao += "," + "View_TM_MPHZY";
                        sql_findtracknum = "select MP_TRACKNUM from View_TM_MPHZY where MP_NEWXUHAO='" + _xuhao + "' and MP_MARID='" + _marid + "' and MP_STATERV='8' AND MP_STATUS='0'";
                    }
                    else if (color[4] != "0" || color[5] != "0")//表明计划类型为外协计划
                    {
                        _replacefor_xuhao += "," + "View_TM_OUTSOURCELIST";
                        sql_findtracknum = "select OSL_TRACKNUM from View_TM_OUTSOURCELIST where OSL_NEWXUHAO='" + _xuhao + "' and OSL_MARID='" + _marid + "' and OST_STATE='8' AND OSL_STATUS='0'";
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
                                e.Row.Cells[8].BackColor = System.Drawing.Color.YellowGreen;
                                e.Row.Cells[8].ToolTip = "双击查看代用计划";
                            }
                        }
                        e.Row.Cells[8].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[8].Attributes.Add("ondblclick", "MarReplace_DownWardQuery('" + _replacefor_xuhao + "')");
                    }
                }
                #endregion

                //含物料编码不提计划项标识
                if (color[7].ToString() != "" && color[8] == "N")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[1].Attributes.Add("title", "不提材料计划");
                }
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
                case "UCPaging1":
                    contrl[0] = (UCPaging)UCPaging1;//材料计划
                    contrl[1] = (GridView)SmartGridView1;
                    contrl[2] = (Panel)NoDataPanel1;
                    break;
                case "UCPaging2":
                    contrl[0] = (UCPaging)UCPaging2;//材料计划
                    contrl[1] = (GridView)SmartGridView2;
                    contrl[2] = (Panel)NoDataPanel2;
                    break;
                default:
                    break;
            }
            return contrl;
        }

        /// <summary>
        /// 页面回发分页初始化
        /// </summary>
        private void PagePostBack()
        {
            this.GetListName();
            string tapindex = TabContainer1.ActiveTab.ID.ToString();
            switch (tapindex)
            {
                case "TabPanel1"://材料计划
                    ViewState["CurrentUCPaging"] = "UCPaging2";
                    this.InitVar(UCPaging1, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) AS NUMBER", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["able"].ToString(), 0, 30);
                    break;
                case "TabPanel2":
                    ViewState["CurrentUCPaging"] = "UCPaging2";
                    this.InitVar(UCPaging2, viewtable, "BM_ID", "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) AS NUMBER", "dbo.f_formatstr(BM_ZONGXU, '.')", ViewState["unalble"].ToString(), 0, 30);
                    break;
                default:
                    break;
            }
        }
    }
}
