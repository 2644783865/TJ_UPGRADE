using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_ProcessCard_Gen : BasicPage
    {
        PagerQueryParam pager_gen = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.InitPage();

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
            pager_gen.OrderField = "PRO_ADATE";
            pager_gen.StrWhere = this.GetSearchList();
            pager_gen.OrderType = 1;
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

        private string GetSearchList()
        {
            string strWhere = " PRO_STATE='8'";

            if (txtSearchGen.Text.Trim() != "")
            {
                strWhere += " and " + ddlGeneralProCard.SelectedValue + " like '%" + txtSearchGen.Text.Trim() + "%'";
            }
            return strWhere;
        }

        protected void download_OnClick(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;

            string sqlCon = "select fileload,fileName from View_TM_PROCESS_CARD_GENERAL where PRO_ID=" + lotnum;
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

        }


        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string proId = ((HiddenField)e.Row.FindControl("HIDXUHAO")).Value.Trim();
                string sql = " select * from TBPM_PROCESS_CARD_GENERAL where PRO_ID ='" + proId + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0]["PRO_IFZTSEE"].ToString().Trim() == "1")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
                    Label lb = (Label)e.Row.FindControl("lbPRO_IFZTSEE");
                    lb.Text = "已查看";
                    lb.BackColor = System.Drawing.Color.Yellow;
                }
                if (dt.Rows[0]["PRO_IFZTSEE"].ToString().Trim() == "0")
                {
                    Label lb = (Label)e.Row.FindControl("lbPRO_IFZTSEE");
                    lb.Text = "未查看";
                }
                if (dt.Rows[0]["PRO_IFDDSEE"].ToString().Trim() == "1")
                {
                    Label lb = (Label)e.Row.FindControl("lbPRO_IFDDSEE");
                    lb.Text = "已查看";
                    lb.BackColor = System.Drawing.Color.Green;
                }
                if (dt.Rows[0]["PRO_IFDDSEE"].ToString().Trim() == "0")
                {
                    Label lb = (Label)e.Row.FindControl("lbPRO_IFDDSEE");
                    lb.Text = "未查看";
                }
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Cells[5].Attributes.Add("title", "单击下载附件");
            }
        }

        protected void btnZTSEE_OnClick(object sender, EventArgs e)
        {
            string sql = "";
            for (int i = 0, length = GridView2.Rows.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)GridView2.Rows[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    HiddenField hid = (HiddenField)GridView2.Rows[i].FindControl("HIDXUHAO");
                    sql = "update TBPM_PROCESS_CARD_GENERAL set PRO_IFZTSEE='1' where PRO_ID='" + hid.Value + "'";
                }
            }
            if (sql == "")
            {
                Response.Write("<script type='text/javascript'>alert('请勾选一条数据')</script>");
                return;
            }
            else
            {
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('更改语句出现问题，请联系管理员')</script>");
                }
            }
            InitPage();
        }

        protected void btnDDSEE_OnClick(object sender, EventArgs e)
        {
            string sql = "";
            for (int i = 0, length = GridView2.Rows.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)GridView2.Rows[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    HiddenField hid = (HiddenField)GridView2.Rows[i].FindControl("HIDXUHAO");
                    sql = "update TBPM_PROCESS_CARD_GENERAL set PRO_IFDDSEE='1' where PRO_ID='" + hid.Value + "'";
                }
            }
            if (sql == "")
            {
                Response.Write("<script type='text/javascript'>alert('请勾选一条数据')</script>");
                return;
            }
            else
            {
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('更改语句出现问题，请联系管理员')</script>");
                }
            }
            InitPage();
        }
    }
}
