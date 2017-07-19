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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class QX_Power_List : BasicPage
    {
        DbAccess dbl = new DbAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGrid();
            }
            CheckUser(ControlFinder);
        }

        private void bindGrid()
        {
            string sqlText = "select *,'QX_Power_Detail.aspx?Action=Update&&page_id='+cast(a.page_id as varchar(20)) as QX_Power_Detail_URL, ";
            sqlText += "b.name as fatherName from power_page a ,power_page b where b.page_id = a.fatherID order by b.fatherID";
            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            GridView1.DataSource = dbl.fillDataSet(sqlText);
            GridView1.DataBind();
        }

        private void bindGrid1()
        {
            string sqlText = "select *,'QX_Power_Detail.aspx?Action=Update&&page_id='+cast(a.page_id as varchar(20)) as QX_Power_Detail_URL, ";
            sqlText += "b.name as fatherName from power_page a ,power_page b where b.page_id = a.fatherID and  a.NAME like '%" + TextBox1.Text + "%' order by b.fatherID";
            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            GridView1.DataSource = dbl.fillDataSet(sqlText);
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView theGrid = sender as GridView;  // refer to the GridView
            int newPageIndex = 0;

            if (-2 == e.NewPageIndex)
            { // when click the "GO" Button
                TextBox txtNewPageIndex = null;
                //GridViewRow pagerRow = theGrid.Controls[0].Controls[theGrid.Controls[0].Controls.Count - 1] as GridViewRow; // refer to PagerTemplate
                GridViewRow pagerRow = theGrid.BottomPagerRow; //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow

                if (null != pagerRow)
                {
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
                }

                if (null != txtNewPageIndex)
                {
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
                }

            }
            else
            {  // when click the first, last, previous and next Button
                newPageIndex = e.NewPageIndex;
            }

            // check to prevent form the NewPageIndex out of the range
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

            // specify the NewPageIndex
            //Response.Write(newPageIndex);
            //Response.End();
            theGrid.PageIndex = newPageIndex;
            this.bindGrid();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")//删除
            {
                //获取当前操作的行索引
                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = gvrow.RowIndex;

                //根据获取到得行索引，读出该条记录的ID
                string id = ((Label)(GridView1.Rows[index].FindControl("lblID"))).Text.Trim();

                //删除对应的零件
                //根据记录的ID从数据库中删除该记录
                dbl.exSQLv("delete from power_page where page_id='" + id + "'");
                //删除页面包含的控件
                dbl.exSQLv("delete from page_control where page_id='" + id + "'");

                //重新读出附件信息
                bindGrid();

            }
            if (e.CommandName == "Page")
            {
                TextBox tb = GridView1.BottomPagerRow.FindControl("txtNewPageIndex") as TextBox;

                int pageindex = Convert.ToInt32(tb.Text);
                GridView1.PageIndex = pageindex - 1;
                bindGrid();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window:close();</script>");
        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            bindGrid1();
        }
        #region 导出功能(X)

        protected void btnExport0_Click(object sender, EventArgs e) //导出
        {
            string sqltext = "select e.*,f.*,g.R_NAME,h.ST_NAME,h.ST_WORKNO,h.DEP_NAME,h.DEP_POSITION from (select * from(select distinct a. PAGE_ID, a.PAGE,a.IDPATH,SUBSTRING(a.IDPATH,1,3) as FatherPageID,a.CONTROLNUM, a.NAME as PageName,b.R_ID, c.NAME as ButtomName from POWER_PAGE as a left join POWER_ROLE as b on a.PAGE_ID=b.PAGE_ID left join PAGE_CONTROL as c on b.CONTROL_ID=c.CON_ID where PAGELEVEL=2)d) as e left join (select * from(select distinct a. PAGE_ID, a.PAGE,a.IDPATH,SUBSTRING(a.IDPATH,5,3) as FatherPageID,a.CONTROLNUM, a.NAME as PageName,c.NAME as ButtomName from POWER_PAGE as a left join POWER_ROLE as b on a.PAGE_ID=b.PAGE_ID left join PAGE_CONTROL as c on b.CONTROL_ID=c.CON_ID where PAGELEVEL=3)d)as f on e.ButtomName=f.PageName left join ROLE_INFO as g on e.R_ID=g.R_ID left join View_TBDS_STAFFINFO as h on ''''+g.R_NAME+''''=h.R_NAME where g.R_NAME is not null and ST_NAME<>'管理员'and ST_PD<>'1' order by e.PAGE_ID,f.PAGE_ID,e.ButtomName,f.ButtomName,ST_POSITION";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "权限-角色配置表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("权限-角色配置表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    row.CreateCell(1).SetCellValue(dt.Rows[i][1].ToString());
                    row.CreateCell(2).SetCellValue(dt.Rows[i][5].ToString());
                    row.CreateCell(3).SetCellValue(dt.Rows[i][4].ToString());
                    row.CreateCell(4).SetCellValue(dt.Rows[i][9].ToString());
                    row.CreateCell(5).SetCellValue(dt.Rows[i][7].ToString());
                    row.CreateCell(6).SetCellValue(dt.Rows[i][12].ToString());
                    row.CreateCell(7).SetCellValue(dt.Rows[i][14].ToString());
                    row.CreateCell(8).SetCellValue(dt.Rows[i][15].ToString());
                    row.CreateCell(9).SetCellValue(dt.Rows[i][16].ToString());
                    row.CreateCell(10).SetCellValue(dt.Rows[i][17].ToString());
                    row.CreateCell(11).SetCellValue(dt.Rows[i][18].ToString());
                    row.CreateCell(12).SetCellValue(dt.Rows[i][19].ToString());
                }

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        #region 导出权限(X)
        protected void btnExport1_Click(object sender, EventArgs e)
        {
            string filename = "权限-人员配置表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("权限-人员配置表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                string sqlPage = "select a.PAGE_ID as FatherPage_ID,a.PAGE as FatherPage,a.NAME as FatherPageName,b.PAGE_ID,b.PAGE,b.NAME as PageName from (select * from POWER_PAGE where PAGELEVEL=2) as a left join (select * from POWER_PAGE where PAGELEVEL=3) as b on a.PAGE_ID=b.FATHERID where b.PAGE_ID is not null and b.PAGE_ID <>'' order by a.PAGE_ID";
                DataTable dtPage = DBCallCommon.GetDTUsingSqlText(sqlPage);
                string sqlControl = "select a.PAGE_ID,a.CON_ID,a.CONTROL,a.NAME,b.PAGE,b.FATHERID,b.NAME from PAGE_CONTROL as a left join POWER_PAGE as b on a.PAGE_ID=b.PAGE_ID where PAGELEVEL=3";
                DataTable dtControl = DBCallCommon.GetDTUsingSqlText(sqlControl);
                DataView dvControl = new DataView(dtControl);
                string sqlRole = "select distinct a.PAGE_ID,a.CONTROL_ID,b.R_ID,b.R_NAME,c.ST_ID,c.ST_NAME from POWER_ROLE as a left join ROLE_INFO as b on a.R_ID=b.R_ID left join View_TBDS_STAFFINFO as c on ''''+b.R_NAME+''''=c.R_NAME where b.R_NAME is not null and b.R_NAME<>'' and ST_NAME<>'管理员'and ST_PD<>'1'order by ST_ID ";
                DataTable dtRole = DBCallCommon.GetDTUsingSqlText(sqlRole);
                DataView dvRole = new DataView(dtRole);
                string RoleName = "";
                for (int i = 0; i < dtPage.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dtPage.Rows[i][3].ToString()))
                    {
                        dvControl.RowFilter = "PAGE_ID='" + dtPage.Rows[i][3].ToString() + "'";
                        DataTable dtControlNew = dvControl.ToTable();
                        IRow row = sheet0.CreateRow(i + 2);
                        ICell cell0 = row.CreateCell(0);
                        cell0.SetCellValue(i + 1);
                        row.CreateCell(1).SetCellValue(dtPage.Rows[i][2].ToString());
                        row.CreateCell(2).SetCellValue(dtPage.Rows[i][5].ToString());
                        for (int j = 0; j < dtControlNew.Rows.Count; j++)
                        {
                            row.CreateCell(2 * j + 3).SetCellValue(dtControlNew.Rows[j][3].ToString());
                            dvRole.RowFilter = "PAGE_ID='" + dtPage.Rows[i][3].ToString() + "'and CONTROL_ID='" + dtControlNew.Rows[j][1].ToString() + "'";
                            DataTable dtRoleNew = dvRole.ToTable();
                            for (int k = 0; k < dtRoleNew.Rows.Count; k++)
                            {
                                RoleName = RoleName + dtRoleNew.Rows[k][5].ToString() + ",";
                            }
                            row.CreateCell(2 * j + 4).SetCellValue(RoleName.Substring(0, RoleName.Length - 1));
                            RoleName = "";
                        }
                    }
                }
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string filename = "权限-人员配置表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("权限-人员配置表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);

                #region 导出含权限控件页面
                ISheet sheet0 = wk.GetSheetAt(0);
                string sqlPage = "select a.PAGE_ID as FatherPage_ID,a.PAGE as FatherPage,a.NAME as FatherPageName,b.PAGE_ID,b.PAGE,b.NAME as PageName from (select * from POWER_PAGE where PAGELEVEL=2) as a left join (select * from POWER_PAGE where PAGELEVEL=3) as b on a.PAGE_ID=b.FATHERID where b.PAGE_ID is not null and b.PAGE_ID <>'' order by a.PAGE_ID";
                DataTable dtPage = DBCallCommon.GetDTUsingSqlText(sqlPage);
                string sqlControl = "select a.PAGE_ID,a.CON_ID,a.CONTROL,a.NAME,b.PAGE,b.FATHERID,b.NAME from PAGE_CONTROL as a left join POWER_PAGE as b on a.PAGE_ID=b.PAGE_ID where PAGELEVEL=3";
                DataTable dtControl = DBCallCommon.GetDTUsingSqlText(sqlControl);
                DataView dvControl = new DataView(dtControl);
                string sqlRole = "select distinct a.PAGE_ID,a.CONTROL_ID,b.R_ID,b.R_NAME,c.ST_ID,c.ST_NAME from POWER_ROLE as a left join ROLE_INFO as b on a.R_ID=b.R_ID left join View_TBDS_STAFFINFO as c on ''''+b.R_NAME+''''=c.R_NAME where b.R_NAME is not null and b.R_NAME<>'' and ST_NAME<>'管理员'and ST_PD<>'1'order by ST_ID ";
                DataTable dtRole = DBCallCommon.GetDTUsingSqlText(sqlRole);
                DataView dvRole = new DataView(dtRole);

                string RoleName = "";
                string RoleR_ID = "";
                for (int i = 0; i < dtPage.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dtPage.Rows[i][3].ToString()))
                    {
                        dvControl.RowFilter = "PAGE_ID='" + dtPage.Rows[i][3].ToString() + "'";
                        DataTable dtControlNew = dvControl.ToTable();

                        string sqlFilterRole = "select distinct R_ID from POWER_ROLE where CONTROL_ID in (select CON_ID from PAGE_CONTROL where PAGE_ID in (select distinct FATHERID from POWER_PAGE WHERE PAGE_ID='" + dtPage.Rows[i][3].ToString() + "')and NAME in (select distinct NAME from POWER_PAGE WHERE PAGE_ID='" + dtPage.Rows[i][3].ToString() + "')) ";
                        DataTable dtFilterRole = DBCallCommon.GetDTUsingSqlText(sqlFilterRole);

                        IRow row = sheet0.CreateRow(i + 2);
                        ICell cell0 = row.CreateCell(0);
                        cell0.SetCellValue(i + 1);
                        row.CreateCell(1).SetCellValue(dtPage.Rows[i][2].ToString());
                        row.CreateCell(2).SetCellValue(dtPage.Rows[i][5].ToString());
                        for (int j = 0; j < dtControlNew.Rows.Count; j++)
                        {
                            row.CreateCell(2 * j + 3).SetCellValue(dtControlNew.Rows[j][3].ToString());
                            for (int m = 0; m < dtFilterRole.Rows.Count; m++)
                            {
                                RoleR_ID = RoleR_ID + dtFilterRole.Rows[m][0].ToString() + ",";
                            }
                            if (RoleR_ID.Length < 1)
                                dvRole.RowFilter = "PAGE_ID='" + dtPage.Rows[i][3].ToString() + "'and CONTROL_ID='" + dtControlNew.Rows[j][1].ToString() + "'";
                            else
                                dvRole.RowFilter = "PAGE_ID='" + dtPage.Rows[i][3].ToString() + "'and CONTROL_ID='" + dtControlNew.Rows[j][1].ToString() + "' and R_ID in (" + RoleR_ID.Substring(0, RoleR_ID.Length - 1) + ")";
                            DataTable dtRoleNew = dvRole.ToTable();
                            for (int k = 0; k < dtRoleNew.Rows.Count; k++)
                            {
                                RoleName = RoleName + dtRoleNew.Rows[k][5].ToString() + ",";
                            }
                            if (RoleName.Length < 1)
                                row.CreateCell(2 * j + 4).SetCellValue("");
                            else
                                row.CreateCell(2 * j + 4).SetCellValue(RoleName.Substring(0, RoleName.Length - 1));
                            RoleName = "";
                            RoleR_ID = "";
                        }
                    }
                }
                #endregion

                #region 导出权限页面
                ISheet sheet1 = wk.GetSheetAt(1);
                string sqlConID = "select b.NAME as FatherName,a.NAME,a.CON_ID from PAGE_CONTROL as a left join POWER_PAGE as b on a.PAGE_ID=b.PAGE_ID where a.PAGE_ID in (select PAGE_ID from POWER_PAGE where PAGELEVEL=2) order by a.PAGE_ID";
                DataTable dtConID = DBCallCommon.GetDTUsingSqlText(sqlConID);
                DataView dvConID = new DataView(dtConID);
                string sqlRoleConID = "select distinct a.PAGE_ID,a.CONTROL_ID,b.R_ID,b.R_NAME,c.ST_ID,c.ST_NAME from POWER_ROLE as a left join ROLE_INFO as b on a.R_ID=b.R_ID left join View_TBDS_STAFFINFO as c on ''''+b.R_NAME+''''=c.R_NAME where b.R_NAME is not null and b.R_NAME<>'' and ST_NAME<>'管理员'and ST_PD<>'1'order by ST_ID";
                DataTable dtRoleConID = DBCallCommon.GetDTUsingSqlText(sqlRoleConID);
                DataView dvRoleConID = new DataView(dtRoleConID);
                string ST_Name = "";
                for (int i = 0; i < dtConID.Rows.Count; i++)
                {
                    dvRoleConID.RowFilter = "CONTROL_ID='" + dtConID.Rows[i][2].ToString() + "'";
                    DataTable dtRoleConIDNew = dvRoleConID.ToTable();
                    for (int j = 0; j < dtRoleConIDNew.Rows.Count; j++)
                    {
                        ST_Name = ST_Name + dtRoleConIDNew.Rows[j][5].ToString() + ",";
                    }
                    IRow row1 = sheet1.CreateRow(i + 2);
                    ICell cell1 = row1.CreateCell(0);
                    cell1.SetCellValue(i + 1);
                    row1.CreateCell(1).SetCellValue(dtConID.Rows[i][0].ToString());
                    row1.CreateCell(2).SetCellValue(dtConID.Rows[i][1].ToString());
                    if (ST_Name.Length < 1)
                        row1.CreateCell(3).SetCellValue("");
                    else
                        row1.CreateCell(3).SetCellValue(ST_Name.Substring(0, ST_Name.Length - 1));
                    ST_Name = "";
                }

                #endregion

                #region 导出部门级模块
                ISheet sheet2 = wk.GetSheetAt(2);
                string sqlDep = "select distinct a.PAGE_ID,NAME,c.R_NAME,b.R_ID from POWER_PAGE as a left join POWER_ROLE as b on a.PAGE_ID=b.PAGE_ID left join ROLE_INFO as c on b.R_ID=c.R_ID where PAGELEVEL=2 and b.R_ID in (select R_ID from ROLE_INFO) order by b.R_ID";
                DataTable dtDep = DBCallCommon.GetDTUsingSqlText(sqlDep);
                DataView dvDep = new DataView(dtDep);
                string sqlDepPage = "select distinct PAGE_ID from POWER_PAGE where PAGELEVEL=2 order by PAGE_ID";
                DataTable dtDepPage = DBCallCommon.GetDTUsingSqlText(sqlDepPage);
                string R_Name = "";
                for (int i = 0; i < dtDepPage.Rows.Count; i++)
                {
                    dvDep.RowFilter = "PAGE_ID='" + dtDepPage.Rows[i][0].ToString() + "'";
                    DataTable dtDepNew = dvDep.ToTable();
                    for (int j = 0; j < dtDepNew.Rows.Count; j++)
                    {
                        R_Name = R_Name + dtDepNew.Rows[j][2].ToString() + ",";
                    }
                    IRow row2 = sheet2.CreateRow(i + 2);
                    ICell cell2 = row2.CreateCell(0);
                    cell2.SetCellValue(i + 1);
                    row2.CreateCell(1).SetCellValue(dtDepNew.Rows[0][1].ToString());
                    if (R_Name.Length < 1)
                        row2.CreateCell(2).SetCellValue("");
                    else
                        row2.CreateCell(2).SetCellValue(R_Name.Substring(0, R_Name.Length - 1));
                    R_Name = "";
                }
                #endregion

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
