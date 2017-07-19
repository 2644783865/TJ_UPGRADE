using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ZhuanZPS : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                doPost(rblZT);
                bindGrid();
            }
        }

        #region "数据查询，分页"

        /// <summary>
        /// 分页初始化
        /// </summary>
        private void bindGrid()
        {
            pager.TableName = "VIEW_TBDS_PS";
            pager.PrimaryKey = "ST_ID";
            pager.ShowFields = "*";
            pager.OrderField = "ST_ZDSJ";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;//按时间升序序排列
            pager.PageSize = 12;
            UCPaging1.PageSize = pager.PageSize; //每页显示的记录数
            //pager.PageIndex = 1;
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvSP, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }

        private string strWhere()
        {
            string sqlText = "";
            string datetime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            if (rblZT.SelectedValue == "a")
            {
                sqlText = "ST_PIDTYPE='1'";
                grvSP.Columns[7].Visible = true;
                grvSP.Columns[8].Visible = true;
                grvSP.Columns[9].Visible = false;
            }
            else if (rblZT.SelectedValue == "0")//最近评审:一个月内
            {
                sqlText = "ST_SHSJ>='" + datetime + "' and ST_PID='" + Session["UserID"].ToString() + "' and ST_PIDTYPE='1' and ST_PSZT in ('2','3')";
                grvSP.Columns[7].Visible = true;
                grvSP.Columns[8].Visible = false;
                grvSP.Columns[9].Visible = false;
            }
            else
            {
                sqlText = "ST_PID='" + Session["UserID"].ToString() + "' and ST_PIDTYPE='1' and ST_PSZT='1'";
                grvSP.Columns[7].Visible = false;
                grvSP.Columns[8].Visible = false;
                grvSP.Columns[9].Visible = true;
            }
            if (ddlSP.SelectedValue != "%")
            {
                sqlText += " and ST_TYPE='" + ddlSP.SelectedValue.ToString() + "'";
            }
            return sqlText;
        }

        void Pager_PageChanged(int pageNumber)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        #endregion

        //不同状态：最近、待评审
        protected void ddlSP_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }
        /// <summary>
        /// 查看、审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAction_OnClick(object sender, EventArgs e)
        {
            string cr_id = ((LinkButton)sender).CommandArgument.ToString();
            string path = "";
            string type = "";
            string action = "";
            string[] split = ((LinkButton)sender).CommandName.ToString().Split(new Char[] { '|' });
            type = split[0];
            action = split[1];
            path = "OM_MyTask_PS.aspx";
            Response.Redirect("" + path + "?Action=" + action + "&ID=" + cr_id + "&Type=" + type + "");
        }

        protected string Edit(string str)
        {
            string[] strs = str.Split('|');
            string id = strs[0].ToString();
            string per_id = strs[1].ToString();
            string type = strs[2].ToString();
            if (type == "0")
            {
                return "javascript:window.showModalDialog('OM_ZZEdit.aspx?Action=Edit&ID=" + id + "&ST_ID=" + per_id + "','','dialogWidth=500px;dialogHeight=570px')";
            }
            else if (type == "1")
            {
                return "javascript:window.showModalDialog('OM_HTEdit.aspx?Action=Edit&ID=" + id + "&ST_ID=" + per_id + "','','dialogWidth=500px;dialogHeight=600px')";
            }
            else if (type == "2")
            {
                return "javascript:window.showModalDialog('OM_FlowEdit.aspx?Action=Edit&ID=" + id + "&ST_ID=" + per_id + "','','dialogWidth=500px;dialogHeight=570px')";
            }
            else
            {
                return "javascript:window.showModalDialog('OM_FlowEdit.aspx?Action=Edit&ID=" + id + "&ST_ID=" + per_id + "','','dialogWidth=500px;dialogHeight=570px')";
            }
        }

        //标记当前审批任务为红色
        protected void grv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                e.Row.Attributes.Add("onclick", "RowClick(this)");
                e.Row.Attributes["style"] = "Cursor:pointer";
                Label lbl_id = (Label)e.Row.FindControl("lbl_id");//序号
                lbl_id.Text = ((this.pager.PageIndex - 1) * this.pager.PageSize + e.Row.RowIndex + 1).ToString();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField id = (HiddenField)e.Row.FindControl("hf_id");

                #region 提醒代码

                string sql_psyj = "select ST_PSYJ,ST_DEP from TBDS_PSVIEW where ST_ID='" + id.Value.ToString() + "' and ST_PID='" + Session["UserID"] + "'";
                DataTable dt_yj = DBCallCommon.GetDTUsingSqlText(sql_psyj);
                if (dt_yj.Rows.Count > 0)
                {
                    if (dt_yj.Rows[0]["ST_PSYJ"].ToString() == "1")
                    {
                        if (dt_yj.Rows[0]["ST_DEP"].ToString() == "01")//领导
                        {
                            //当部门全部通过后，领导可审
                            string sqltext = "select ST_PSYJ from TBDS_PSVIEW where ST_ID='" + id.Value.ToString() + "'";
                            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                            if (dt.Rows.Count > 0)
                            {
                                sqltext = "select ST_PSYJ from TBDS_PSVIEW where ST_ID='" + id.Value.ToString() + "' and ST_DEP !='01' and ST_PSYJ!='2' and ST_PID!='" + Session["UserID"] + "'";
                                DataTable dtbmyj = DBCallCommon.GetDTUsingSqlText(sqltext);
                                if (dtbmyj.Rows.Count <= 0)//没有不通过的
                                {
                                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    e.Row.Cells[9].Text = "";
                                }
                            }
                        }
                        else
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                        }
                    }
                }
                if (((Label)e.Row.FindControl("ST_PSZT")).Text == "被驳回")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                }

                #endregion

                //制单人登录，当申请被驳回，或者没人开始审批时可修改
                int state = 0;
                string yjs = string.Empty;
                string sql = "select * from TBDS_PSDETAIL where ST_ID='" + id.Value + "' and ST_PSZT='3'";
                DataTable ps = DBCallCommon.GetDTUsingSqlText(sql);
                sql = "select * from TBDS_PSDETAIL where ST_ID='" + id.Value + "' and ST_ZDR='" + Session["UserID"].ToString() + "'";
                DataTable ps1 = DBCallCommon.GetDTUsingSqlText(sql);
                sql = "select * from TBDS_PSVIEW where ST_ID='" + id.Value + "' and ST_PIDTYPE='1'";
                DataTable zt = DBCallCommon.GetDTUsingSqlText(sql);
                for (int i = 0; i < zt.Rows.Count; i++)
                {
                    if (zt.Rows[i]["ST_PSYJ"].ToString() != "1")
                    {
                        state++;
                    }
                }
                if (ps1.Rows.Count != 0)
                {
                    if (ps.Rows.Count == 0 && state != 0)
                    {
                        if (ps1.Rows[0]["ST_PSZT"].ToString() == "2")
                        {
                            e.Row.Cells[8].Text = "";
                        }
                    }
                }
                else
                {
                    e.Row.Cells[8].Text = "";
                }
            }
        }

        protected void doPost(RadioButtonList rbList)
        {
            foreach (ListItem item in rbList.Items)
            {
                //为预设项添加doPostBack JS  
                if (item.Selected)
                {
                    item.Attributes.Add("onclick", String.Format("javascript:setTimeout('__doPostBack(\\'{0}${1}\\',\\'\\')', 0)", rbList.UniqueID, rbList.Items.IndexOf(item)));
                }
            }
        }
    }
}
