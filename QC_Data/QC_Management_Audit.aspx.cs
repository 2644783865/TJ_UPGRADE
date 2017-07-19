using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Management_Audit : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
            if (!IsPostBack)
            {
                bindGrid();
            }
            CheckUser(ControlFinder);
        }
        private void bindGrid()
        {
            pager.TableName = "TBQC_MANAGEMENT_AUDIT";
            pager.PrimaryKey = "PRO_ID";
            pager.ShowFields = "*";
            pager.OrderField = "PRO_ID";
            pager.StrWhere = "";
            pager.OrderType = 0;//升序排列
            pager.PageSize = 20;
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
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
        void Pager_PageChangedMS(int pageNumber)
        {
            bindGrid();
        }
        protected void LinkButDel_Click(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            string sqltext = "delete from TBQC_MANAGEMENT_AUDIT where PRO_ID=" + lotnum;
            DBCallCommon.ExeSqlText(sqltext);
            bindGrid();

        }

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            string sqlCon = "select b.fileload,b.fileName from TBQC_MANAGEMENT_AUDIT as a left join TBQC_FILES as b on a.PRO_FUJIAN=b.fileName where PRO_ID=" + lotnum;
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

    }
}
