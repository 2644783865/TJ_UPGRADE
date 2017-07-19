using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_ProcessCard_List_Gen : BasicPage
    {
        PagerQueryParam pager_gen = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                RBLBind();
                this.InitPage();
                GetTaskTypeData();
               
            }

            UCPagingGen.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
            CheckUser(ControlFinder);
        }
        private void InitPage()
        {

            UCPagingGen.CurrentPage = 1;
            InitPagerGen();
            bindGrid();
        }

        private void InitPagerGen()
        {
            pager_gen.TableName = "View_TM_PROCESS_CARD_GENERAL";
            pager_gen.PrimaryKey = "PRO_ID";
            pager_gen.ShowFields = "*";
            pager_gen.OrderField = "PRO_ID";
            pager_gen.StrWhere = this.GetSearchList();
            pager_gen.OrderType = 0;//升序排列
            pager_gen.PageSize = 20;
            UCPagingGen.PageSize = pager_gen.PageSize;    //每页显示的记录数
        }

        private void bindGrid()
        {

            pager_gen.PageIndex = UCPagingGen.CurrentPage;
            DataTable dtGen = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_gen);
            CommonFun.Paging(dtGen, GridView2, UCPagingGen, NoDataPanelGen);
            if (NoDataPanelGen.Visible)
            {
                UCPagingGen.Visible = false;

            }
            else
            {
                UCPagingGen.Visible = true;
                UCPagingGen.InitPageInfo();  //分页控件中要显示的控件
            }

        }

        void Pager_PageChangedMS(int pageNumber)
        {
            InitPagerGen();
            bindGrid();
        }

        private void RBLBind()
        {
            //审核中
            string sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_REVIEWA='" + Session["UserID"].ToString() + "' and PRO_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_REVIEWB='" + Session["UserID"].ToString() + "' and PRO_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_REVIEWC='" + Session["UserID"].ToString() + "' and PRO_STATE='6' ";
            sqlText += ") as temp ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    genRblstatus.Items.Add(new ListItem("您的审核任务", "0"));
                }
                else
                {
                    genRblstatus.Items.Add(new ListItem("您的审核任务" + "</label><label><font color=red>(" + dt.Rows[0][0].ToString() + ")</font>", "0"));
                }
            }

            //未提交
            sqlText = "select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_SUBMITID='" + Session["UserID"].ToString() + "' and PRO_STATE='1'";
            dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["num"].ToString() == "0")
                {
                    genRblstatus.Items.Add(new ListItem("未提交", "5"));
                }
                else
                {
                    genRblstatus.Items.Add(new ListItem("未提交" + "</label><label><font color=red>(" + dt.Rows[0]["num"].ToString() + ")</font>", "5"));
                }
            }
            genRblstatus.Items.Add(new ListItem("通过", "1"));

            //驳回
            sqlText = "select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_SUBMITID='" + Session["UserID"].ToString() + "' and PRO_STATE in ('3','5','7')";
            dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["num"].ToString() == "0")
                {
                    genRblstatus.Items.Add(new ListItem("驳回", "2"));
                }
                else
                {
                    genRblstatus.Items.Add(new ListItem("驳回" + "</label><label><font color=red>(" + dt.Rows[0]["num"].ToString() + ")</font>", "2"));
                }
            }
            genRblstatus.Items.Add(new ListItem("审核中", "3"));
            genRblstatus.Items.Add(new ListItem("全部", "4"));

            genRblstatus.SelectedIndex = 0;
        }



        private string GetSearchList()
        {
            string strWhere = " 1=1 ";

            if (txtSearchGen.Text.Trim() != "")
            {
                strWhere += " and " + ddlGeneralProCard.SelectedValue + " like '%" + txtSearchGen.Text.Trim() + "%'";
            }
            if (genRblstatus.SelectedValue == "0")//待当前审核人审核
            {
                strWhere += "and (PRO_STATE='2' and PRO_REVIEWA='" + Session["UserID"] + "') ";
                strWhere += "or  (PRO_STATE ='4' and PRO_REVIEWB='" + Session["UserID"] + "')";
                strWhere += "or  (PRO_STATE ='6' and PRO_REVIEWC='" + Session["UserID"] + "')";

            }
            else if (genRblstatus.SelectedValue == "1")//通过
            {
                strWhere += "and PRO_STATE ='8'";
            }
            else if (genRblstatus.SelectedValue == "2")//驳回
            {
                strWhere += "and PRO_STATE in ('3','5','7') and PRO_SUBMITID='" + Session["UserID"] + "'";
            }
            else if (genRblstatus.SelectedValue == "3")//当前审核人在审核中
            {
                strWhere += "and  PRO_STATE in('2','4','6') ";
            }
            else if (genRblstatus.SelectedValue == "5")
            {
                strWhere += "and (PRO_STATE='1' and PRO_SUBMITID='" + Session["UserID"] + "') ";
            }
            return strWhere;
        }

        private void GetTaskTypeData()
        {

            GridView2.Columns[14].Visible = false;
            GridView2.Columns[15].Visible = false;
            GridView2.Columns[16].Visible = false;


            if (genRblstatus.SelectedValue == "0")
            {
                GridView2.Columns[15].Visible = true;
            }
            else if (genRblstatus.SelectedValue == "2")
            {
                GridView2.Columns[15].Visible = true;
                GridView2.Columns[16].Visible = true;
            }
            else if (genRblstatus.SelectedValue == "5")
            {
                GridView2.Columns[14].Visible = true;
                GridView2.Columns[16].Visible = true;
            }

        }


        protected void download_OnClick(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;

            string sqlCon = "select fileload,fileName,BC_CONTEXT from View_TM_PROCESS_CARD_GENERAL where PRO_ID=" + lotnum;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlCon);
            if (dt.Rows.Count > 0)
            {
                string strFilePath = dt.Rows[0]["fileload"].ToString() + @"\" + dt.Rows[0]["BC_CONTEXT"].ToString() + dt.Rows[0]["fileName"].ToString();
                if (File.Exists(strFilePath))
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
                    Response.AppendHeader("Content-Length", file.Length.ToString());
                    Response.WriteFile(file.FullName);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件已被删除，请通知相关人员上传文件！');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据库异常，请通知管理员！');", true);
            }


        }

        protected void Search_Click(object sender, EventArgs e)
        {

            InitPage();
            GetTaskTypeData();

        }

        /// <summary>
        /// 删除工艺卡片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hlDelete_OnClick(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            string sqlText = "";

 
                sqlText = "delete from TBPM_PROCESS_CARD_GENERAL where PRO_ID=" + lotnum;

            try
            {
                DBCallCommon.ExeSqlText(sqlText);
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据异常，请联系管理员！！！');", true);
            }
            this.InitPage();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
            //GridView1.DataSource = null;
            //GridView1.DataBind();
        }


        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string proId = e.Row.Cells[9].Text.Trim();
                string proId = ((HiddenField)e.Row.FindControl("HIDXUHAO")).Value.Trim();
                e.Row.Attributes["style"] = "Cursor:hand";

                if (genRblstatus.SelectedIndex == 1 || genRblstatus.SelectedIndex == 3)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("title", "双击修改工序卡片数据");
                        e.Row.Cells[i].Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLinkGen('" + proId + "');", GridView2.DataKeys[e.Row.RowIndex].Value.ToString());

                    }
                }

                e.Row.Cells[4].Attributes.Add("title", "单击下载附件");


            }
        }



    }
}
