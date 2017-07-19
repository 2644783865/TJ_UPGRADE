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
using Microsoft.Office.Interop.MSProject;
using System.Collections.Generic;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_MyContractReviewTask : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();        
        protected void Page_Load(object sender, EventArgs e)
        {                       
            if (!IsPostBack)
            {                
                this.rblZT_OnSelectedIndexChanged(null, null);                
            }
            InitVar();
        }

        #region "数据查询，分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "View_TBCR_View_Detail_ALL";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "CR_ID,CR_XMMC,CR_SBMC,CR_FBSMC,CR_ZDRNAME,CR_ZDRQ,CR_HTLX,CR_NOTE,CRD_SHSJ,CRD_PSYJ,CRD_PID,CRD_PIDTYPE,CR_PSZT";
            pager.OrderField = "CR_ZDRQ";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;//按时间升序序排列
            pager.PageSize = 12;
            //pager.PageIndex = 1;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
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
        #endregion

        //不同状态：最近、待评审
        protected void rblZT_OnSelectedIndexChanged(object sender, EventArgs e)
        {            
            string datetime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            if (rblZT.SelectedIndex == 0)//最近评审:一个月内
            {
                if (dplHTLB.SelectedIndex == 0)
                {
                    ViewState["sqlText"] = " CRD_SHSJ>='" + datetime + "' and CRD_PSYJ!='0' and CRD_PID='" + Session["UserID"].ToString() + "'"+
                        " and CRD_PIDTYPE='1' and CR_PSZT in ('1','2')";
                }
                else
                {
                    ViewState["sqlText"] = "CRD_SHSJ>='" + datetime + "' and CRD_PSYJ!='0' and CR_HTLX='" + dplHTLB.SelectedValue.ToString() + "'"+
                        " and CRD_PIDTYPE='1' and CRD_PID='" + Session["UserID"].ToString() + "'and CR_PSZT in ('1','2')";
                }                
                grvSP.Columns[8].Visible = false;
                grvSP.Columns[7].Visible = true;
            }
            else
            {
                if (dplHTLB.SelectedIndex == 0 || dplHTLB.SelectedValue=="0")
                {
                    if (Session["UserDeptID"].ToString() != "01")
                    {
                        ViewState["sqlText"] = " CRD_PSYJ='0' and CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT in ('1','2') and CRD_PIDTYPE='1'";
                    }
                    else
                    {
                        string sql = "select distinct(CR_ID) from View_TBCR_View_Detail_ALL where CRD_PSYJ='0' and CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT in ('1','2') and CRD_PIDTYPE='1'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        List<string> list = new List<string>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sql1 = "select CR_ID from View_TBCR_View_Detail_ALL where CRD_PSYJ='0' and CRD_PID not in ('1','2','310','311') and CR_PSZT in ('1','2') and CRD_PIDTYPE='1' and CR_ID='" + dr["CR_ID"].ToString() + "'";
                            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                            if (dt1.Rows.Count == 0)
                            {
                                list.Add(dr["CR_ID"].ToString());
                            }
                        }
                        string sql2 = "";
                        if (list.Count > 0)
                        {
                            //sql2 = "CR_ID in (";
                            //for (int i = 0, length = list.Count; i < length; i++)
                            //{
                            //    sql2 += "'" + list[i] + "',";
                            //}
                            //sql2 = sql2.Trim(',') + ") and CRD_PSYJ='0' and CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT in ('1','2') and CRD_PIDTYPE='1'";

                            sql2 = "CRD_PSYJ='0' and CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT in ('1','2') and CRD_PIDTYPE='1' and CRD_PID in ('1','2','310','311')";
                        }
                        else
                        {
                            sql2 = "CR_ID is null and CRD_PSYJ='0' and CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT in ('1','2') and CRD_PIDTYPE='1' ";
                        }
                        ViewState["sqlText"] = sql2;
                    } 
                }
                else
                {
                    ViewState["sqlText"] = " CRD_PSYJ='0' and CR_HTLX='" + dplHTLB.SelectedValue.ToString() + "' and CRD_PID='" + Session["UserID"].ToString() + "'" +
                        " and CRD_PIDTYPE='1' and CR_PSZT  in ('1','2')";
                }                
                grvSP.Columns[8].Visible = true;
                grvSP.Columns[7].Visible = false;
            }
            InitVar();
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
           
            path = "CM_ContractView_Audit.aspx";
            Response.Redirect("" + path + "?Action=" + action + "&ID=" + cr_id + "&Type=" + type + "");
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
                HiddenField cr_id = (HiddenField)e.Row.FindControl("hfcr_id");
                string sql_psyj = "select CRD_PSYJ,CRD_DEP from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + cr_id.Value.ToString() + "' and CRD_PID='"+Session["UserID"]+"'";
                DataTable dt_yj = DBCallCommon.GetDTUsingSqlText(sql_psyj);
                if (dt_yj.Rows.Count > 0)
                {
                    if (dt_yj.Rows[0]["CRD_PSYJ"].ToString()=="0")
                    {

                        if (dt_yj.Rows[0]["CRD_DEP"].ToString() == "14")//审计
                        {
                            string sqltext = "select CRD_ID,CRD_DEP,CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + cr_id.Value.ToString() + "' and CRD_DEP not in ('01','14')";
                            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                            int num = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["CRD_PSYJ"].ToString() == "2")
                                {
                                    num++;
                                }
                            }
                            if (num == dt.Rows.Count)
                            {
                                e.Row.Cells[0].BackColor = System.Drawing.Color.Red;                                
                            }
                        }

                        else if (dt_yj.Rows[0]["CRD_DEP"].ToString() == "01")//领导
                        {
                            //分两种情况，有审计和没有审计，如果不经过审计评审，当部门全部通过后，领导可审
                            string sqltext = "select CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + cr_id.Value.ToString() + "'";
                            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                            if (dt.Rows.Count > 0)
                            {
                                //if (dt.Rows[0]["CRD_PSYJ"].ToString() == "2")//审计已审，或领导兼部门负责人
                                //{
                                //    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                                //}
                                string check_BMYJ = "select CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + cr_id.Value.ToString() + "' and CRD_DEP !='01' and CRD_PSYJ!='2' and CRD_PID!='" + Session["UserID"] + "'";
                                DataTable dtbmyj = DBCallCommon.GetDTUsingSqlText(check_BMYJ);
                                if (dtbmyj.Rows.Count <= 0)//没有不通过的
                                {
                                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                                }
                                //if (dtbmyj.Rows.Count > 0)
                                //{
                                //    e.Row.Visible = false;
                                //}
                            }
                            //else
                            //{
                            //    //没有审计时，判断是不是所有部门都审核通过
                            //    string check_BMYJ = "select CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + cr_id.Value.ToString() + "' and CRD_DEP !='01' and CRD_PSYJ!='2' and CRD_PID!='" + Session["UserID"] + "'";
                            //    DataTable dtbmyj = DBCallCommon.GetDTUsingSqlText(check_BMYJ);
                            //    if (dtbmyj.Rows.Count <= 0)//没有不通过的
                            //    {
                            //        e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                            //    }
                            //}
                        }
                        else
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;

                        }
                    }
                }
                
            }
        }
                
    }
}
