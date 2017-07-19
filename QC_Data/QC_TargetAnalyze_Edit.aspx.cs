using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_TargetAnalyze_Edit : System.Web.UI.Page
    {
        string sqlText;
        string action;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                action = Request["action"].ToString();
            }
            if (!IsPostBack)
            {
                BindPart();
                InitInfo();
                initdata();
                //InitGridview();
                //GetDepartment();
                //GetTJDepartment();
                if (action == "view")
                {
                    btnSave.Visible = false;
                }
            }
        }

        private void BindPart()
        {
            string stId = Session["UserId"].ToString();

            DataTable dt = DBCallCommon.GetPermeision(22, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }
        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList ddl = (DropDownList)(GridView1.Rows[i].FindControl("ddlDepId"));
                ddl.DataSource = dt;
                ddl.DataTextField = "DEP_NAME";
                ddl.DataValueField = "DEP_CODE";
                ddl.DataBind();
                ListItem it = new ListItem();
                it.Text = "全部";
                it.Value = "00";
                ddl.Items.Insert(0, it);
                ddl.SelectedValue = ((HtmlInputHidden)GridView1.Rows[i].FindControl("txtDepId")).Value;

            }
        }
        private void GetTJDepartment()//绑定统计部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList ddl = (DropDownList)(GridView1.Rows[i].FindControl("ddlTJDepId"));
                ddl.DataSource = dt;
                ddl.DataTextField = "DEP_NAME";
                ddl.DataValueField = "DEP_CODE";
                ddl.DataBind();
                ListItem it = new ListItem();
                it.Text = "全部";
                it.Value = "00";
                ddl.Items.Insert(0, it);
                ddl.SelectedValue = ((HtmlInputHidden)GridView1.Rows[i].FindControl("txtTJDepId")).Value;

            }
        }
        private void InitGridview()
        {
            if (action == "view")
            {
                sqlText = "select * from TBQC_TARGET_DETAIL where TARGET_FID='" + hidId.Value + "'";
            }
            else
            {
                sqlText = "select * from TBQC_TARGET_DETAIL where TARGET_FID='" + hidId.Value + "'";
                if (ddl_Depart.SelectedValue != "00")
                {
                    sqlText += " and TARGET_DEPID='" + ddl_Depart.SelectedValue + "'";
                }

            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        private void InitInfo()
        {
            string Id = Request.QueryString["tarId"];
            sqlText = "select * from TBQC_TARGET_LIST where TARGET_ID=" + Id;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["TARGET_NAME"].ToString().Trim();
                hidId.Value = dt.Rows[0]["TARGET_ID"].ToString().Trim();
            }

        }

        protected void ddl_Depart_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitGridview();
            GetDepartment();
            GetTJDepartment();
        }
        protected void initdata()
        {
            lb_bm.Visible = false;
            ddl_Depart.Visible = false;
            sqlText = "select  a.*, b.DEP_CODE, b.DEP_NAME from TBQC_TARGET_DETAIL as a left join TBDS_DEPINFO as b on a.TARGET_TJBMID=b.DEP_CODE where TARGET_TJBMID='" + Session["UserDeptID"] + "' and TARGET_FID=" + CommonFun.ComTryInt(hidId.Value) + "";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            string sql = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList ddlDep = (DropDownList)(GridView1.Rows[i].FindControl("ddlDepId"));
                ddlDep.DataSource = dt0;
                ddlDep.DataTextField = "DEP_NAME";
                ddlDep.DataValueField = "DEP_CODE";
                ddlDep.DataBind();
                ddlDep.SelectedValue = ((HtmlInputHidden)GridView1.Rows[i].FindControl("txtDepId")).Value;

                DropDownList ddlTJDep = (DropDownList)(GridView1.Rows[i].FindControl("ddlTJDepId"));
                ddlTJDep.DataSource = dt0;
                ddlTJDep.DataTextField = "DEP_NAME";
                ddlTJDep.DataValueField = "DEP_CODE";
                ddlTJDep.DataBind();
                ddlTJDep.SelectedValue = ((HtmlInputHidden)GridView1.Rows[i].FindControl("txtTJDepId")).Value;

                if (action == "view" || action == "add")
                {
                    ddlDep.Enabled = false;
                    ddlTJDep.Enabled = false;

                }
                else
                {
                    ddlDep.Enabled = true;
                    ddlTJDep.Enabled = true;
                }
            }
        }
        protected void rbl_Depart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl_Depart.SelectedValue == "1")
            {
                initdata();
            }
            else
            {
                lb_bm.Visible = true; ;
                ddl_Depart.Visible = true;
                BindPart();
                InitInfo();
                InitGridview();
                GetDepartment();
                GetTJDepartment();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            int rownum = 0;
            //查找在何处插入行
            foreach (GridViewRow grow in GridView1.Rows)
            {

                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    rownum = Convert.ToInt32(((Label)grow.FindControl("lblIndex")).Text.Trim()) - 1;
                    break;
                }


            }
            //如果没有，在结尾处插入
            if (rownum == -1)
            {
                rownum = GridView1.Rows.Count;
            }
            DataTable dt = CreatDataTable();
            for (int i = 0; i < rownum; i++)
            {

                CreatNewRowByGridView(dt, i);
            }
            for (int i = 0; i < Convert.ToInt32(txtNum.Text.Trim()); i++)
            {
                DataRow newrow = dt.NewRow();
                newrow[0] = "00";

                dt.Rows.Add(newrow);
            }
            if (rownum != GridView1.Rows.Count)
            {
                for (int i = rownum; i < GridView1.Rows.Count; i++)
                {
                    CreatNewRowByGridView(dt, i);
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GetDepartment();
            GetTJDepartment();
        }

        private void CreatNewRowByGridView(DataTable dt, int i)
        {
            GridViewRow gRow = GridView1.Rows[i];
            DataRow newRow = dt.NewRow();
            newRow[0] = ((DropDownList)gRow.FindControl("ddlDepId")).SelectedValue;
            newRow[1] = ((DropDownList)gRow.FindControl("ddlTiXi")).SelectedValue;
            newRow[2] = ((HtmlInputText)gRow.FindControl("txtManager")).Value.Trim();
            newRow[3] = ((HtmlInputText)gRow.FindControl("txtMuBiao")).Value.Trim();
            newRow[4] = ((HtmlInputText)gRow.FindControl("txtJan")).Value.Trim();
            newRow[5] = ((HtmlInputText)gRow.FindControl("txtFeb")).Value.Trim();
            newRow[6] = ((HtmlInputText)gRow.FindControl("txtMar")).Value.Trim();
            newRow[7] = ((HtmlInputText)gRow.FindControl("txtApr")).Value.Trim();
            newRow[8] = ((HtmlInputText)gRow.FindControl("txtMay")).Value.Trim();
            newRow[9] = ((HtmlInputText)gRow.FindControl("txtJun")).Value.Trim();
            newRow[10] = ((HtmlInputText)gRow.FindControl("txtJuy")).Value.Trim();
            newRow[11] = ((HtmlInputText)gRow.FindControl("txtAug")).Value.Trim();
            newRow[12] = ((HtmlInputText)gRow.FindControl("txtSep")).Value.Trim();
            newRow[13] = ((HtmlInputText)gRow.FindControl("txtOct")).Value.Trim();
            newRow[14] = ((HtmlInputText)gRow.FindControl("txtNov")).Value.Trim();
            newRow[15] = ((HtmlInputText)gRow.FindControl("txtDec")).Value.Trim();
            newRow[16] = ((HtmlInputText)gRow.FindControl("txtComplete")).Value.Trim();
            newRow[17] = ((HtmlInputHidden)gRow.FindControl("hidTarId")).Value.Trim();
            newRow[18] = ((DropDownList)gRow.FindControl("ddlTJDepId")).SelectedValue;
            dt.Rows.Add(newRow);
        }

        private DataTable CreatDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TARGET_DEPID");
            dt.Columns.Add("TARGET_TIXI");
            dt.Columns.Add("TARGET_MANAGER");
            dt.Columns.Add("TARGET_MUBIAO");
            dt.Columns.Add("TARGET_JAN");
            dt.Columns.Add("TARGET_FEB");
            dt.Columns.Add("TARGET_MAR");
            dt.Columns.Add("TARGET_APR");
            dt.Columns.Add("TARGET_MAY");
            dt.Columns.Add("TARGET_JUN");
            dt.Columns.Add("TARGET_JUL");
            dt.Columns.Add("TARGET_AUG");
            dt.Columns.Add("TARGET_SEP");
            dt.Columns.Add("TARGET_OCT");
            dt.Columns.Add("TARGET_NOV");
            dt.Columns.Add("TARGET_DEC");
            dt.Columns.Add("TARGET_COMPLETE");
            dt.Columns.Add("TARGET_ID");
            dt.Columns.Add("TARGET_TJBMID");
            return dt;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = CreatDataTable();
            List<string> list001 = new List<string>();
            string sql002 = "";
            foreach (GridViewRow grow in GridView1.Rows)
            {
                //string delNum="";
                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (!ckb.Checked)
                {
                    CreatNewRowByGridView(dt, Convert.ToInt32(((Label)grow.FindControl("lblIndex")).Text.Trim()) - 1);
                }
                else
                {
                    sql002 = "delete from TBQC_TARGET_DETAIL where TARGET_ID='" + ((HtmlInputHidden)grow.FindControl("hidTarId")).Value.Trim() + "' ";
                    list001.Add(sql002);
                }

            }
            DBCallCommon.ExecuteTrans(list001);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GetDepartment();
            GetTJDepartment();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (action == "add")
            {
                foreach (GridViewRow gRow in GridView1.Rows)
                {
                    string id = ((HtmlInputHidden)gRow.FindControl("hidTarId")).Value.Trim();
                    sqlText = "delete from TBQC_TARGET_DETAIL where TARGET_ID='" + id + "' ";
                    list.Add(sqlText);
                }
                foreach (GridViewRow gRow in GridView1.Rows)
                {
                    string id = ((HtmlInputHidden)gRow.FindControl("hidTarId")).Value;
                    string depId = ((DropDownList)gRow.FindControl("ddlDepId")).SelectedValue;
                    string TiXi = ((DropDownList)gRow.FindControl("ddlTiXi")).SelectedValue;
                    string Manager = ((HtmlInputText)gRow.FindControl("txtManager")).Value.Trim();
                    string MuBiao = ((HtmlInputText)gRow.FindControl("txtMuBiao")).Value.Trim();
                    string Jan = ((HtmlInputText)gRow.FindControl("txtJan")).Value.Trim();
                    string Feb = ((HtmlInputText)gRow.FindControl("txtFeb")).Value.Trim();
                    string Mar = ((HtmlInputText)gRow.FindControl("txtMar")).Value.Trim();
                    string Apr = ((HtmlInputText)gRow.FindControl("txtApr")).Value.Trim();
                    string May = ((HtmlInputText)gRow.FindControl("txtMay")).Value.Trim();
                    string Jun = ((HtmlInputText)gRow.FindControl("txtJun")).Value.Trim();
                    string Juy = ((HtmlInputText)gRow.FindControl("txtJuy")).Value.Trim();
                    string Aug = ((HtmlInputText)gRow.FindControl("txtAug")).Value.Trim();
                    string Sep = ((HtmlInputText)gRow.FindControl("txtSep")).Value.Trim();
                    string Oct = ((HtmlInputText)gRow.FindControl("txtOct")).Value.Trim();
                    string Nov = ((HtmlInputText)gRow.FindControl("txtNov")).Value.Trim();
                    string Dec = ((HtmlInputText)gRow.FindControl("txtDec")).Value.Trim();
                    string complete = ((HtmlInputText)gRow.FindControl("txtComplete")).Value.Trim();
                    string tjdepId = ((DropDownList)gRow.FindControl("ddlTJDepId")).SelectedValue;
                    string fId = hidId.Value;
                    if (!(depId == "00" & TiXi == "" & Manager == "" & MuBiao == "") && !(depId != "00" & TiXi != "" & Manager != "" & MuBiao != ""))//四项中有1-3项为空
                    {

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请将基本信息补全,第" + Convert.ToInt32(((Label)gRow.FindControl("lblIndex")).Text.Trim()) + "行');", true);
                        return;
                    }
                    if (depId != "00" & TiXi != "" & Manager != "" & MuBiao != "")
                    {
                        sqlText = "insert into TBQC_TARGET_DETAIL values('" + depId + "','" + TiXi + "','" + MuBiao + "','" + Manager + "','" + Jan + "','" + Feb + "','" + Mar + "','" + Apr + "','" + May + "','" + Jun + "','" + Juy + "','" + Aug + "','" + Sep + "','" + Oct + "','" + Nov + "','" + Dec + "'," + fId + ",'" + complete + "','" + tjdepId + "')";
                        list.Add(sqlText);
                    }
                }

            }
            else if (action == "edit")
            {
                foreach (GridViewRow gRow in GridView1.Rows)
                {
                    string id = ((HtmlInputHidden)gRow.FindControl("hidTarId")).Value;
                    string depId = ((DropDownList)gRow.FindControl("ddlDepId")).SelectedValue;
                    string TiXi = ((DropDownList)gRow.FindControl("ddlTiXi")).SelectedValue;
                    string Manager = ((HtmlInputText)gRow.FindControl("txtManager")).Value.Trim();
                    string MuBiao = ((HtmlInputText)gRow.FindControl("txtMuBiao")).Value.Trim();
                    string Jan = ((HtmlInputText)gRow.FindControl("txtJan")).Value.Trim();
                    string Feb = ((HtmlInputText)gRow.FindControl("txtFeb")).Value.Trim();
                    string Mar = ((HtmlInputText)gRow.FindControl("txtMar")).Value.Trim();
                    string Apr = ((HtmlInputText)gRow.FindControl("txtApr")).Value.Trim();
                    string May = ((HtmlInputText)gRow.FindControl("txtMay")).Value.Trim();
                    string Jun = ((HtmlInputText)gRow.FindControl("txtJun")).Value.Trim();
                    string Juy = ((HtmlInputText)gRow.FindControl("txtJuy")).Value.Trim();
                    string Aug = ((HtmlInputText)gRow.FindControl("txtAug")).Value.Trim();
                    string Sep = ((HtmlInputText)gRow.FindControl("txtSep")).Value.Trim();
                    string Oct = ((HtmlInputText)gRow.FindControl("txtOct")).Value.Trim();
                    string Nov = ((HtmlInputText)gRow.FindControl("txtNov")).Value.Trim();
                    string Dec = ((HtmlInputText)gRow.FindControl("txtDec")).Value.Trim();
                    string complete = ((HtmlInputText)gRow.FindControl("txtComplete")).Value.Trim();
                    string tjdepId = ((DropDownList)gRow.FindControl("ddlTJDepId")).SelectedValue;
                    string fId = hidId.Value;

                    sqlText = "update TBQC_TARGET_DETAIL set TARGET_JAN='" + Jan + "', TARGET_FEB='" + Feb + "', TARGET_MAR='" + Mar + "', TARGET_APR='" + Apr + "', TARGET_MAY='" + May + "', TARGET_JUN='" + Jun + "', TARGET_JUL='" + Juy + "', TARGET_AUG='" + Aug + "', TARGET_SEP='" + Sep + "', TARGET_OCT='" + Oct + "', TARGET_NOV='" + Nov + "', TARGET_DEC='" + Dec + "',TARGET_TJBMID='" + tjdepId + "' where  TARGET_ID='" + id + "' ";
                    list.Add(sqlText);

                }
            }

            try
            {
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功');", true);
                return;
            }
            catch (Exception)
            {

                throw;
            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (action == "edit")
                {
                    ((DropDownList)e.Row.FindControl("ddlDepId")).Enabled = false;
                    ((DropDownList)e.Row.FindControl("ddlTiXi")).Enabled = false;
                    ((HtmlInputText)e.Row.FindControl("txtManager")).Disabled = true;
                    ((HtmlInputText)e.Row.FindControl("txtMuBiao")).Disabled = true;
                    ((HtmlInputText)e.Row.FindControl("txtComplete")).Disabled = true;
                }

            }
        }





        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaochu_OnClick(object sender, EventArgs e)
        {



            //string sqlText = "select * from TBQC_TARGET_DETAIL as a left join TBDS_DEPINFO as b on a.TARGET_DEPID=b.DEP_CODE where TARGET_FID='" + hidId.Value + "'";
            string sqlText = "select a.*,b.*,c.DEP_NAME as TJDEP_NAME from TBQC_TARGET_DETAIL as a left join TBDS_DEPINFO as b on a.TARGET_DEPID=b.DEP_CODE left join TBDS_DEPINFO as c on a.TARGET_TJBMID =c.DEP_CODE  where TARGET_FID='" + hidId.Value + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ExportDataItem(dt);
        }




        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "目标分解" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("目标分解.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                //TARGET_ID, TARGET_DEPID, TARGET_TIXI, TARGET_MUBIAO, TARGET_MANAGER, TARGET_JAN, TARGET_FEB, TARGET_MAR, TARGET_APR, TARGET_MAY, TARGET_JUN, TARGET_JUL, TARGET_AUG, TARGET_SEP, TARGET_OCT, TARGET_NOV, TARGET_DEC, TARGET_FID, TARGET_COMPLETE
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + objdt.Rows[i]["DEP_NAME"].ToString());
                    row.CreateCell(2).SetCellValue("" + objdt.Rows[i]["TARGET_TIXI"].ToString());
                    row.CreateCell(3).SetCellValue("" + objdt.Rows[i]["TARGET_MANAGER"].ToString());
                    row.CreateCell(4).SetCellValue("" + objdt.Rows[i]["TARGET_MUBIAO"].ToString());
                    row.CreateCell(5).SetCellValue("" + objdt.Rows[i]["TARGET_COMPLETE"].ToString());
                    row.CreateCell(6).SetCellValue("" + objdt.Rows[i]["TJDEP_NAME"].ToString());
                    row.CreateCell(7).SetCellValue("" + objdt.Rows[i]["TARGET_JAN"].ToString());
                    row.CreateCell(8).SetCellValue("" + objdt.Rows[i]["TARGET_FEB"].ToString());
                    row.CreateCell(9).SetCellValue("" + objdt.Rows[i]["TARGET_MAR"].ToString());
                    row.CreateCell(10).SetCellValue("" + objdt.Rows[i]["TARGET_APR"].ToString());
                    row.CreateCell(11).SetCellValue("" + objdt.Rows[i]["TARGET_MAY"].ToString());
                    row.CreateCell(12).SetCellValue("" + objdt.Rows[i]["TARGET_JUN"].ToString());
                    row.CreateCell(13).SetCellValue("" + objdt.Rows[i]["TARGET_JUL"].ToString());
                    row.CreateCell(14).SetCellValue("" + objdt.Rows[i]["TARGET_AUG"].ToString());
                    row.CreateCell(15).SetCellValue("" + objdt.Rows[i]["TARGET_SEP"].ToString());
                    row.CreateCell(16).SetCellValue("" + objdt.Rows[i]["TARGET_OCT"].ToString());
                    row.CreateCell(17).SetCellValue("" + objdt.Rows[i]["TARGET_NOV"].ToString());
                    row.CreateCell(18).SetCellValue("" + objdt.Rows[i]["TARGET_DEC"].ToString());

                }

                for (int i = 0; i <= objdt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
