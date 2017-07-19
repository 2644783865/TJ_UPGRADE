using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_SHCLD_CX : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        string depid = string.Empty;
        string position = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            username = Session["UserName"].ToString();
            depid = Session["UserDeptID"].ToString();
            position = Session["POSITION"].ToString();
            if (!IsPostBack)
            {
                bindrpt();
            }
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "CM_SHCLD ";
            pager_org.PrimaryKey = "CLD_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "CLD_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptSHFWCLD, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private string StrWhere()
        {
            string sql = "CLD_ID is not null";
            if (ddlSPZT.SelectedValue == "2")
            {
                sql += " and CLD_SPZT='0'";
            }
            else if (ddlSPZT.SelectedValue == "3")
            {
                sql += " and CLD_SPZT in('1y','2y','4y')";
            }
            else if (ddlSPZT.SelectedValue == "4")
            {
                sql += " and CLD_SPZT in('y','cljg_y','over')";
            }
            else if (ddlSPZT.SelectedValue == "5")
            {
                sql += " and CLD_SPZT in('1n','2n','4n','5n')";
            }
            if (ddlCLZT.SelectedValue == "2")
            {
                sql += " and CLD_SPZT='0'";
            }
            else if (ddlCLZT.SelectedValue == "3")
            {
                sql += " and CLD_SPZT not in ('0','over','1n','2n','4n','5n')";
            }
            else if (ddlCLZT.SelectedValue == "4")
            {
                sql += " and CLD_SPZT='over'";
            }
            if (ddlTX.SelectedValue == "2")
            {
                sql += " and (" + ddlTXLX.SelectedValue + " is null or " + ddlTXLX.SelectedValue + "='')";
            }
            else if (ddlTX.SelectedValue == "3")
            {
                sql += " and (" + ddlTXLX.SelectedValue + " is not null and " + ddlTXLX.SelectedValue + "!='')";
            }
            if (txtBH.Value != "")
            {
                sql += " and CLD_BH like '%" + txtBH.Value.Trim() + "%'";
            }
            if (txtHTH.Value != "")
            {
                sql += " and CLD_HTH like '%" + txtHTH.Value.Trim() + "%'";
            }
            if (txtXMMC.Value != "")
            {
                sql += " and CLD_XMMC like '%" + txtXMMC.Value.Trim() + "%'";
            }
            if (txtGKMC.Value != "")
            {
                sql += " and CLD_GKMC like '%" + txtGKMC.Value.Trim() + "%'";
            }
            if (txtRWH.Value != "")
            {
                sql += " and CLD_RWH like '%" + txtRWH.Value.Trim() + "%'";
            }
            return sql;
        }
        #endregion

        protected void rptSHFWCLD_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                Label CLD_SPZT1 = (Label)e.Item.FindControl("CLD_SPZT1");
                Label CLD_CLZT = (Label)e.Item.FindControl("CLD_CLZT");
                if (dr["CLD_SPZT"].ToString() == "0")
                {
                    CLD_SPZT1.Text = "未审批";
                    CLD_CLZT.Text = "未处理";
                }
                else if (dr["CLD_SPZT"].ToString() == "1y")
                {
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "2y")
                {

                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "4y")
                {
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "y")
                {
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "1n" || dr["CLD_SPZT"].ToString() == "2n" || dr["CLD_SPZT"].ToString() == "4n" || dr["CLD_SPZT"].ToString() == "5n")
                {
                    CLD_SPZT1.Text = "未通过";
                    CLD_SPZT1.BackColor = Color.Red;
                }
                else if (dr["CLD_SPZT"].ToString() == "cljg_y")
                {
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "fytj_y")
                {
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "over")
                {
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "已处理";
                    CLD_CLZT.BackColor = Color.LightGreen;
                }
            }


        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaoChu_onclick(object sender, EventArgs e)
        {
            string sql = "select CLD_GKMC,CLD_BH,CLD_HTH,CLD_RWH,CLD_XMMC,CLD_SBMC,CLD_XXJJ,CLD_CLJG,CLD_FWZFY,CLD_ZDR,CLD_ZDSJ from CM_SHCLD ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ExportDataItem(dt);
        }

        private void ExportDataItem(DataTable dt)
        {
            string filename = "售后质量问题处理" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("售后质量问题处理.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                font1.FontName = "仿宋";//字体
                font1.FontHeightInPoints = 9;//字号
                ICellStyle cells = wk.CreateCellStyle();
                cells.SetFont(font1);
                cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;

                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(i + 1);
                    row.Cells[0].CellStyle = cells;
                    for (int j = 0, m = dt.Columns.Count; j < m; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                        row.Cells[j + 1].CellStyle = cells;
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
