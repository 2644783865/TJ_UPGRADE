using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_FaHuoNotice : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                bindGrid();
            }
        }


        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            InitPager();
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
                UCPaging1.InitPageInfo();
            }
        }

        private void InitPager()
        {
            pager.TableName = "View_CM_FaHuo";
            pager.PrimaryKey = "CM_FID";
            pager.ShowFields = "*";
            pager.OrderField = "CM_ZDTIME";
            pager.StrWhere = ConWhere();
            pager.OrderType = 1;
            pager.PageSize = 15;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string ConWhere()
        {
            string StrWhere = "CM_CONFIRM='2'";
            StrWhere += " and " + ddlBz.SelectedValue + " like '%" + txtBox.Text + "%'";
            if (rblFHZT.SelectedValue != "2")
            {
                StrWhere += " and CM_FHZT='" + rblFHZT.SelectedValue + "'";
            }
            return StrWhere;
        }

        protected void lbtnSolve_OnClick(object sender, EventArgs e)
        {
            string CM_ID = ((LinkButton)sender).CommandArgument.ToString();
            for (int i = 0, length = Repeater1.Items.Count; i < length; i++)
            {
                if (((HiddenField)Repeater1.Items[i].FindControl("CM_ID")).Value == CM_ID)
                {
                    string CM_FHSJ = ((TextBox)Repeater1.Items[i].FindControl("CM_FHSJ")).Text.Trim();
                    string CM_BIANHAO = ((HiddenField)Repeater1.Items[i].FindControl("CM_BIANHAO")).Value;
                    if (CM_FHSJ == "")
                    {
                        Response.Write("<script>alert('请先填写发货时间再处理！！！')</script>");
                        return;
                    }
                    else
                    {
                        string sql = "update TBCM_FHNOTICE set CM_FHZT='1',CM_FHSJ='" + CM_FHSJ + "' where CM_BIANHAO='" + CM_BIANHAO + "'";
                        try
                        {
                            DBCallCommon.ExeSqlText(sql);
                        }
                        catch
                        {
                            Response.Write("<script>alert('处理sql语句出现问题，请与管理员联系！！！')</script>");
                            return;
                        }
                    }
                }
            }
            Response.Write("<script>alert('处理成功！！！')</script>");
        }

        protected void Query(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }
        List<string> list = new List<string>();
        List<string> col = new List<string>() { "CM_BIANHAO", "CM_CUSNAME", "CM_PROJ", "CM_CONTR", "CM_SH", "CM_JH", "CM_LXR", "CM_LXFS", "CM_JHTIME", "CM_ZDTIME", "MANCLERK", "CM_CONFIRM" };
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Header")
            {
                string fid = ((DataRowView)e.Item.DataItem).Row["CM_FID"].ToString();
                string CM_FHZT = ((HiddenField)e.Item.FindControl("CM_FHZT")).Value;
                LinkButton lbtn = (LinkButton)e.Item.FindControl("lbtnSolve");
                //string sql = "select CM_MANCLERK,CM_BMZG,CM_CONFIRM,CM_YJ1,CM_GSLD from TBCM_FHNOTICE where CM_FID='" + fid + "'";
                //DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (list.Count == 0)
                {
                    list.Add(fid);
                }
                else if (list.Contains(fid))
                {
                    for (int i = 0; i < col.Count; i++)
                    {

                        ((DataRowView)e.Item.DataItem).Row[col[i]] = "";
                        e.Item.FindControl("hyperlink2").Visible = false;
                    }
                    e.Item.DataBind();
                }
                else
                {
                    list.Add(fid);
                }
                if (CM_FHZT == "1")
                {
                    lbtn.Visible = false;
                }

            }
        }

        #region 导出功能

        protected void btnExport_Click(object sender, EventArgs e) //导出
        {
            string sqltext = "select CM_BIANHAO,CM_CUSNAME,CM_PROJ,CM_CONTR,TSA_ENGNAME,TSA_MAP,CM_SH,CM_JH,CM_LXR,CM_LXFS,CM_JHTIME,CM_ZDTIME,MANCLERK from View_CM_FaHuo where " + ConWhere() + "order by CM_ZDTIME desc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "发货通知" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("发货通知表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion
    }
}


