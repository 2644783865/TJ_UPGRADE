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
    public partial class TM_ProcessCard_List : BasicPage
    {
        string viewtable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RBLBind();
                this.InitPage();
                GetTaskTypeData();
             
            }
            UCPagingPro.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
            CheckUser(ControlFinder);
        }
        PagerQueryParam pager_pro = new PagerQueryParam();
      
        private void InitPage()
        {
            UCPagingPro.CurrentPage = 1;
         
            InitPagerPro();
          
            bindGrid();
        }



        private void InitPagerPro()
        {
            pager_pro.TableName = "View_TM_PROCESS_CARD";
            pager_pro.PrimaryKey = "PRO_ID";
            pager_pro.ShowFields = "*";
            pager_pro.OrderField = "PRO_ID";
            pager_pro.StrWhere = this.GetSearchList("Pro");
            pager_pro.OrderType = 0;//升序排列
            pager_pro.PageSize = 20;
            UCPagingPro.PageSize = pager_pro.PageSize;    //每页显示的记录数
        }

        private void RBLBind()
        {
            //审核中
          string  sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_PROCESS_CARD where PRO_REVIEWA='" + Session["UserID"].ToString() + "' and PRO_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD where PRO_REVIEWB='" + Session["UserID"].ToString() + "' and PRO_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD where PRO_REVIEWC='" + Session["UserID"].ToString() + "' and PRO_STATE='6' ";
            sqlText += ") as temp ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    proRblstatus.Items.Add(new ListItem("您的审核任务", "0"));
                }
                else
                {
                    proRblstatus.Items.Add(new ListItem("您的审核任务" + "</label><label><font color=red>(" + dt.Rows[0][0].ToString() + ")</font>", "0"));
                }
            }

            //未提交
             sqlText = "select count(*) as num from TBPM_PROCESS_CARD where PRO_SUBMITID='" + Session["UserID"].ToString() + "' and PRO_STATE='1'";
             dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count>0)
            {
                if (dt.Rows[0]["num"].ToString()=="0")
                {
                    proRblstatus.Items.Add(new ListItem("未提交", "5"));
                }
                else
                {
                    proRblstatus.Items.Add(new ListItem("未提交" + "</label><label><font color=red>(" + dt.Rows[0]["num"].ToString() + ")</font>", "5"));
                }
            }
            proRblstatus.Items.Add(new ListItem("通过", "1"));
           
            //驳回
            sqlText = "select count(*) as num from TBPM_PROCESS_CARD where PRO_SUBMITID='" + Session["UserID"].ToString() + "' and PRO_STATE in ('3','5','7')";
             dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["num"].ToString() == "0")
                {
                    proRblstatus.Items.Add(new ListItem("驳回", "2"));
                }
                else
                {
                    proRblstatus.Items.Add(new ListItem("驳回" + "</label><label><font color=red>(" + dt.Rows[0]["num"].ToString() + ")</font>", "2"));
                }
            }
            proRblstatus.Items.Add(new ListItem("审核中", "3"));
            proRblstatus.Items.Add(new ListItem("全部", "4"));
            proRblstatus.SelectedIndex = 0;
        }

        private string GetSearchList(string txtType)
        {
            string strWhere = " 1=1 ";
         
                    if (txtSearch.Text.Trim() != "")
                    {
                        strWhere += " and " + ddlSearch.SelectedValue + " like '%" + txtSearch.Text.Trim() + "%'";
                    }
                    if (proRblstatus.SelectedValue == "0")//待当前审核人审核
                    {
                        strWhere += "and (PRO_STATE='2' and PRO_REVIEWA='" + Session["UserID"] + "') ";
                        strWhere += "or  (PRO_STATE ='4' and PRO_REVIEWB='" + Session["UserID"] + "')";
                        strWhere += "or  (PRO_STATE ='6' and PRO_REVIEWC='" + Session["UserID"] + "')";

                    }
                    else if (proRblstatus.SelectedValue == "1")//通过
                    {
                        strWhere += "and PRO_STATE ='8'";
                    }
                    else if (proRblstatus.SelectedValue == "2")//驳回
                    {
                        strWhere += "and PRO_STATE in ('3','5','7') and PRO_SUBMITID='"+Session["UserID"]+"'";
                    }
                    else if (proRblstatus.SelectedValue == "3")//当前审核人在审核中
                    {
                        strWhere += "and PRO_STATE in('2','4','6') ";
                    }
                    else if (proRblstatus.SelectedValue=="5")
                    {
                        strWhere += "and (PRO_STATE='1' and PRO_SUBMITID='" + Session["UserID"] + "') ";
                    }
                 
            return strWhere;
        }
        void Pager_PageChangedMS(int pageNumber)
        {
            InitPagerPro();
            bindGrid();
        }
        private void bindGrid()
        {
            pager_pro.PageIndex = UCPagingPro.CurrentPage;
         
            DataTable dtPro = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_pro);

            CommonFun.Paging(dtPro, GridView1, UCPagingPro, NoDataPanel);
         
            if (NoDataPanel.Visible)
            {
                UCPagingPro.Visible = false;

            }
            else
            {
                UCPagingPro.Visible = true;
                UCPagingPro.InitPageInfo();  //分页控件中要显示的控件

            }


        }

        /// <summary>
        /// 删除工艺卡片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hlDelete_OnClick(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
          
              string  sqlText = "delete from TBPM_PROCESS_CARD where PRO_ID=" + lotnum;

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


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string proId = e.Row.Cells[9].Text.Trim();
                string proId = ((HiddenField)e.Row.FindControl("HIDXUHAO")).Value.Trim();
                e.Row.Attributes["style"] = "Cursor:hand";
                if (proRblstatus.SelectedIndex==1||proRblstatus.SelectedIndex==3)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("title", "双击修改工序卡片数据");
                        e.Row.Cells[i].Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLink('" + proId + "');", GridView1.DataKeys[e.Row.RowIndex].Value.ToString());

                    }
                }
    
                e.Row.Cells[7].Attributes.Add("title", "单击下载附件");

            }
        }
    
        protected void Search_Click(object sender, EventArgs e)
        {

            InitPage();
            GetTaskTypeData();

        }

        private void GetTaskTypeData()
        {
            GridView1.Columns[17].Visible = false;
            GridView1.Columns[18].Visible = false;
            GridView1.Columns[19].Visible = false;
         
            if (proRblstatus.SelectedValue=="0")
            {
                GridView1.Columns[18].Visible = true;
            }
            else if (proRblstatus.SelectedValue == "2" || proRblstatus.SelectedValue == "2")
            {
                GridView1.Columns[18].Visible = true;
                GridView1.Columns[19].Visible = true;
            }
            else if (proRblstatus.SelectedValue=="5")
            {
                GridView1.Columns[17].Visible = true;
                GridView1.Columns[19].Visible = true;
            }

        }
        protected void imgbtnDF_Init(object sender, EventArgs e)
        {
            ToolkitScriptManager1.RegisterPostBackControl((Control)sender);
        }
        protected void download_OnClick(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            string sqlCon = "select fileload,fileName,BC_CONTEXT from View_TM_PROCESS_CARD where PRO_ID=" + lotnum;
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




    }
}
