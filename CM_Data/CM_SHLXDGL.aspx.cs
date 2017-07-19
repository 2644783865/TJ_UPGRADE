using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_SHLXDGL : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        string depid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            depid = Session["UserDeptID"].ToString();
            if (!IsPostBack)
            {
                bindrpt();
            }
            CheckUser(ControlFinder);
        }

        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }
        private void bindrpt()
        {
            pager_org.TableName = "CM_SHLXD ";
            pager_org.PrimaryKey = "LXD_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "LXD_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptSHFWLXD, UCPaging1, palNoData);
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
            string sql = "LXD_ID is not null";
            if (rblRW.SelectedValue == "2")
            {
                sql += " and (LXD_ZDR='" + username + "' or ";
                sql += "(LXD_SPR1='" + username + "' and LXD_SPZT='0') or ";
                sql += "(LXD_SPR2='" + username + "' and LXD_SPZT='1') or ";
                sql += "(LXD_SPR3='" + username + "' and LXD_SPZT='2') or ";
                sql += " LXD_FWJGTXR='" + username + "' or ";
                sql += " LXD_FWFYTJR='" + username + "' or ";
                sql += "(LXD_FZBMID like'%" + depid + "%' and (LXD_FWGC is null or LXD_FWGC='')) or ";
                sql += "(LXD_FWFYDEPID like'%" + depid + "%' and (LXD_FWZFY is null or LXD_FWZFY='')))";
            }
            else if (ddlSPZT.SelectedValue == "2")
            {
                sql += " and LXD_SPZT='0'";
            }
            else if (ddlSPZT.SelectedValue == "3")
            {
                sql += " and LXD_SPZT in ('1','2')";
            }
            else if (ddlSPZT.SelectedValue == "4")
            {
                sql += " and LXD_SPZT='10'";
            }
            else if (ddlSPZT.SelectedValue == "5")
            {
                sql += " and LXD_SPZT='11'";
            }
            else if (ddlSPZT.SelectedValue == "6")
            {
                sql += " and LXD_SPZT='13'";
            }
            if (txtLXD_GKMC.Text.Trim() != "")
            {
                sql += " and LXD_GKMC like '%" + txtLXD_GKMC.Text.Trim() + "%'";
            }
            if (txtLXD_BH.Text.Trim() != "")
            {
                sql += " and LXD_BH like '%" + txtLXD_BH.Text.Trim() + "%'";
            }
            if (txtLXD_HTH.Text.Trim() != "")
            {
                sql += " and LXD_HTH like '%" + txtLXD_HTH.Text.Trim() + "%'";
            }
            if (txtLXD_RWH.Text.Trim() != "")
            {
                sql += " and LXD_RWH like '%" + txtLXD_RWH.Text.Trim() + "%'";
            }
            if (txtLXD_XMMC.Text.Trim() != "")
            {
                sql += " and LXD_XMMC like '%" + txtLXD_XMMC.Text.Trim() + "%'";
            }
            if (txtLXD_SBMC.Text.Trim() != "")
            {
                sql += " and LXD_SBMC like '%" + txtLXD_SBMC.Text.Trim() + "%'";
            }
            if (txtLXD_ZDR.Text.Trim() != "")
            {
                sql += " and LXD_ZDR like '%" + txtLXD_ZDR.Text.Trim() + "%'";
            }
            if (txtLXD_ZDSJ.Text.Trim() != "")
            {
                sql += " and LXD_ZDSJ like '%" + txtLXD_ZDSJ.Text.Trim() + "%'";
            }
            return sql;
        }

        protected void rptSHFWLXD_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label LXD_ID = (Label)e.Item.FindControl("LXD_ID");
                Label LXD_SPZT1 = (Label)e.Item.FindControl("LXD_SPZT1");
                HyperLink hplSP = (HyperLink)e.Item.FindControl("hplSP");
                HyperLink hplTX = (HyperLink)e.Item.FindControl("hplTX");
                HyperLink hplTJ = (HyperLink)e.Item.FindControl("hplTJ");
                hplSP.Visible = false;
                hplTX.Visible = false;
                hplTJ.Visible = false;
                string sql = "select * from CM_SHLXD where LXD_ID=" + LXD_ID.Text;
                DataRow dr = DBCallCommon.GetDTUsingSqlText(sql).Rows[0];
                if (dr["LXD_SPZT"].ToString() == "0")
                {
                    if (username == dr["LXD_SPR1"].ToString())
                    {
                        hplSP.Visible = true;
                    }
                    LXD_SPZT1.Text = "未审批";
                }
                else if (dr["LXD_SPZT"].ToString() == "1")
                {
                    if (username == dr["LXD_SPR2"].ToString())
                    {
                        hplSP.Visible = true;
                    }
                    LXD_SPZT1.Text = "审批中";
                }
                else if (dr["LXD_SPZT"].ToString() == "2")
                {
                    if (username == dr["LXD_SPR3"].ToString())
                    {
                        hplSP.Visible = true;
                    }
                    LXD_SPZT1.Text = "审批中";
                }
                else if (dr["LXD_SPZT"].ToString() == "10")
                {
                    if (dr["LXD_FZBMID"].ToString().Contains(depid))
                    {
                        hplTX.Visible = true;
                    }
                    LXD_SPZT1.Text = "已通过";
                }
                else if (dr["LXD_SPZT"].ToString() == "11")
                {
                    LXD_SPZT1.Text = "未通过";
                }
                else if (dr["LXD_SPZT"].ToString() == "12")
                {
                    if (depid == "06")
                    {
                        hplTJ.Visible = true;
                    }
                    LXD_SPZT1.Text = "已通过";
                }
                else if (dr["LXD_SPZT"].ToString() == "13")
                {
                    LXD_SPZT1.Text = "已处理";
                }
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            string sql = "";
            for (int i = 0, length = rptSHFWLXD.Items.Count; i < length; i++)
            {
                CheckBox cbxXuHao = (CheckBox)rptSHFWLXD.Items[i].FindControl("cbxXuHao");
                Label LXD_ID = (Label)rptSHFWLXD.Items[i].FindControl("LXD_ID");
                if (cbxXuHao.Checked == true)
                {
                    sql += "delete from CM_SHLXD where LXD_ID='" + LXD_ID.Text + "'";
                    string sql1 = " select LXD_ID,LXD_SPZT from CM_SHLXD where LXD_ID='" + LXD_ID.Text + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                    if (dt.Rows[0]["LXD_SPZT"].ToString() != "0")
                    {
                        Response.Write("<script>alert('这一条数据已不在初始化状态，不能删除！！！')</script>");
                        return;
                    }
                    break;
                }
            }
            if (sql == "")
            {
                Response.Write("<script>alert('请勾选一条数据')</script>");
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
                    Response.Write("<script>alert('删除sql语句有误')</script>");
                    return;
                }
            }
            Response.Write("<script>alert('您已经成功删除该数据')</script>");
            bindrpt();
        }
        protected void btnAlter_OnClick(object sender, EventArgs e)
        {
            string id = "";
            for (int i = 0, length = rptSHFWLXD.Items.Count; i < length; i++)
            {
                CheckBox cbxXuHao = (CheckBox)rptSHFWLXD.Items[i].FindControl("cbxXuHao");
                Label LXD_ID = (Label)rptSHFWLXD.Items[i].FindControl("LXD_ID");
                if (cbxXuHao.Checked == true)
                {
                    id = LXD_ID.Text;
                    string sql1 = " select LXD_ID,LXD_SPZT from CM_SHLXD where LXD_ID='" + LXD_ID.Text + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                    if (dt.Rows[0]["LXD_SPZT"].ToString() != "0")
                    {
                        Response.Write("<script>alert('这一条数据已不在初始化状态，不能修改！！！')</script>");
                        return;
                    }
                    break;
                }
            }
            if (id == "")
            {
                Response.Write("<script>alert('请勾选一条数据')</script>");
                return;
            }
            else
            {
                Response.Redirect("CM_SHLXD.aspx?action=alter&id=" + id);
            }
        }

        protected void btnRefuse_OnClick(object sender, EventArgs e)
        {
            string id = "";
            for (int i = 0, length = rptSHFWLXD.Items.Count; i < length; i++)
            {
                CheckBox cbxXuHao = (CheckBox)rptSHFWLXD.Items[i].FindControl("cbxXuHao");
                Label LXD_ID = (Label)rptSHFWLXD.Items[i].FindControl("LXD_ID");
                if (cbxXuHao.Checked == true)
                {
                    id = LXD_ID.Text;
                    break;
                }
            }
            if (id == "")
            {
                Response.Write("<script>alert('请勾选一条数据')</script>");
                return;
            }
            else
            {
                string sql1 = "select * from CM_SHLXD where  LXD_ID=" + id;
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                List<string> listall = new List<string>();
                List<string> list = new List<string>();
                for (int i = 0, length = dt.Columns.Count; i < length; i++)
                {
                    listall.Add(dt.Columns[i].ColumnName.ToString());
                }
                listall.Remove("LXD_ID");
                listall.Remove("LXD_SJID");
                listall.Remove("LXD_BH");
                listall.Remove("LXD_XMMC");
                listall.Remove("LXD_HTH");
                listall.Remove("LXD_SBMC");
                listall.Remove("LXD_GKMC");
                listall.Remove("LXD_RWH");
                listall.Remove("LXD_XXJJ");
                listall.Remove("LXD_FWYQ");
                listall.Remove("LXD_FWDZ");
                listall.Remove("LXD_LXFS");
                listall.Remove("LXD_SJYQ");
                listall.Remove("LXD_ZDR");
                listall.Remove("LXD_ZDSJ");
                listall.Remove("LXD_ZDJY");
                listall.Remove("LXD_SPR1");
                listall.Remove("LXD_SPZT");
                string sql = "update CM_SHLXD set ";
                for (int i = 0, length = listall.Count; i < length; i++)
                {
                    sql += listall[i] + "= null,";
                }
                sql += "LXD_SPZT='0'";
                sql += " where LXD_ID=" + id;
                list.Add(sql);
                sql = string.Empty;
                sql = "delete from CM_SHLXD_FY where FY_LXDID='" + id + "'";
                list.Add(sql);
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('驳回的sql语句出现问题，请联系管理员！！！')</script>");
                    return;
                }
                Response.Write("<script>alert('您已成功驳回该条数据，改单据将会进入初始状态！！！')</script>");
                Response.Redirect("CM_SHLXDGL.aspx");
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CM_SHLXD.aspx?action=add");
        }

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaoChu_onclick(object sender, EventArgs e)
        {
            string sql = "select LXD_GKMC,LXD_BH,LXD_HTH,LXD_RWH,LXD_XMMC,LXD_SBMC,LXD_XXJJ,LXD_FWGC,LXD_FWZFY,LXD_ZDR,LXD_ZDSJ from CM_SHLXD ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ExportDataItem(dt);
        }

        private void ExportDataItem(DataTable dt)
        {
            string filename = "售后服务处理" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("售后服务.xls")))
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
