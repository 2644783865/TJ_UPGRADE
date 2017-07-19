using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.PL_Data
{
    public partial class MainPlanProView : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
            UCPaging.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);

        }
        private void InitPage()
        {

            UCPaging.CurrentPage = 1;
            this.InitPager();
            bindGrid();
        }

        private void InitPager()
        {
            //MP_CODE, MP_PJID, MP_PJNAME, MP_ENGNAME, MP_NOTE, MP_STATE
            pager.TableName = "View_TBMP_MAINPLANPROFILES";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "fileUpDate";
            pager.StrWhere = "1=1";
            pager.OrderType = 1;//升序排列
            pager.PageSize = 20;
            UCPaging.PageSize = pager.PageSize;    //每页显示的记录数
        }

        private void bindGrid()
        {

            pager.PageIndex = UCPaging.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, GridView1, UCPaging, NoDataPanel);

            if (NoDataPanel.Visible)
            {
                UCPaging.Visible = false;

            }
            else
            {
                UCPaging.Visible = true;
                UCPaging.InitPageInfo();  //分页控件中要显示的控件

            }

        }

        private void Pager_PageChangedMS(int pageNumber)
        {
            this.InitPager();
            bindGrid();
        }

        protected void download_OnClick(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            string sqlCon = "select fileload,fileName from View_TBMP_MAINPLANPROFILES where Id='" + lotnum + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlCon);
            if (dt.Rows.Count > 0)
            {
                string strFilePath = dt.Rows[0]["fileload"].ToString() + @"\" + dt.Rows[0]["fileName"].ToString();
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strId = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox cbx = (CheckBox)gRow.FindControl("CheckBox1");
                if (cbx.Checked)
                {
                    string id = ((HtmlInputHidden)gRow.FindControl("hidId")).Value;
                    strId += "'" + id + "',";
                }

            }
            strId = strId.Trim(',');
            if (strId == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要删除的数据！');", true);
            }
            else
            {
                string sqlText = "delete from TBMP_MAINPLANPROFILES where Id in (" + strId + ")";
                DBCallCommon.ExeSqlText(sqlText);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！'); window.location.href='MainPlanProView.aspx'", true);

            }

        }

    }
}
