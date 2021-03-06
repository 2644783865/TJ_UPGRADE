﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using ZCZJ_DPF.CommonClass;
using System.Text;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GZTZADD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateNewRow(12);
            }
        }

        #region 增加删除行

        protected void btnadd_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
            }
            else
            {
                Response.Write("<script>alert('请输入数字！')</script>");
            }
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            List<string> col = new List<string>();
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar(col);
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("gztz_name");
            dt.Columns.Add("gztz_stid");
            dt.Columns.Add("gztz_gh");
            dt.Columns.Add("gztz_bm");

            dt.Columns.Add("gztz_bfjbf");
            dt.Columns.Add("gztz_bfzybf");

            dt.Columns.Add("gztz_bf");
            dt.Columns.Add("gztz_bk");
            dt.Columns.Add("gztz_note");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                for (int i = 1; i < 10; i++)
                {
                    newRow[i - 1] = ((TextBox)retItem.FindControl("txt" + i)).Text;
                }
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar(List<string> col)
        {
            if (Det_Repeater.Items.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
                delete.Visible = true;
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("gztz_name");
            dt.Columns.Add("gztz_stid");
            dt.Columns.Add("gztz_gh");
            dt.Columns.Add("gztz_bm");

            dt.Columns.Add("gztz_bfjbf");
            dt.Columns.Add("gztz_bfzybf");

            dt.Columns.Add("gztz_bf");
            dt.Columns.Add("gztz_bk");
            dt.Columns.Add("gztz_note");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    for (int i = 1; i < 10; i++)
                    {
                        newRow[i - 1] = ((TextBox)retItem.FindControl("txt" + i)).Text;
                    }
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar(col);
        }

        #endregion

        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_newstid = (TextBox)sender;
            RepeaterItem Reitem = (RepeaterItem)Tb_newstid.Parent;
            
            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    ((TextBox)Reitem.FindControl("txt2")).Text = stid;
                    ((TextBox)Reitem.FindControl("txt1")).Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txt3")).Text = dt.Rows[0]["ST_WORKNO"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txt4")).Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }
            }
        }


        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string bh = "TZ" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim();
            string stryearmonth = "";
            int num = 0;
            if (tb_yearmonth.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择年月！');", true);
                return;
            }
            string sqlifcz = "select * from View_OM_GZQD where QD_YEARMONTH='" + tb_yearmonth.Text.Trim() + "' and (OM_GZSCBZ='0' or OM_GZSCBZ='1')";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifcz);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月工资清单正在审核或已审核通过！');", true);
                return;
            }
            for (int i = 0; i < Det_Repeater.Items.Count; i++)
            {
                if (((TextBox)Det_Repeater.Items[i].FindControl("txt2")).Text.Trim() != "" && ((TextBox)Det_Repeater.Items[i].FindControl("txt1")).Text.Trim() != "")
                {
                    num++;
                }
            }
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写要调整的数据！');", true);
                return;
            }
            else
            {
                for (int j = 0; j < Det_Repeater.Items.Count; j++)
                {
                    if (((TextBox)Det_Repeater.Items[j].FindControl("txt2")).Text.Trim() != "" && ((TextBox)Det_Repeater.Items[j].FindControl("txt1")).Text.Trim() != "")
                    {
                        stryearmonth = tb_yearmonth.Text.Trim();
                        TextBox txt2 = (TextBox)Det_Repeater.Items[j].FindControl("txt2");
                        TextBox txt5 = (TextBox)Det_Repeater.Items[j].FindControl("txt5");
                        TextBox txt6 = (TextBox)Det_Repeater.Items[j].FindControl("txt6");
                        TextBox txt7 = (TextBox)Det_Repeater.Items[j].FindControl("txt7");
                        TextBox txt8 = (TextBox)Det_Repeater.Items[j].FindControl("txt8");
                        TextBox txt9 = (TextBox)Det_Repeater.Items[j].FindControl("txt9");
                        if (CommonFun.ComTryDecimal(txt8.Text.Trim()) > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('补扣应为负数！');", true);
                            return;
                        }
                        string sql = "insert into OM_GZTZdetal(GZTZ_SPBH,GZTZ_YEARMONTH,GZTZ_STID,GZTZ_BFJBF,GZTZ_BFZYBF,GZTZ_BF,GZTZ_BK,GZTZ_NOTE) values('" + bh + "','" + stryearmonth + "','" + txt2.Text.Trim() + "'," + CommonFun.ComTryDecimal(txt5.Text.Trim()) + "," + CommonFun.ComTryDecimal(txt6.Text.Trim()) + "," + CommonFun.ComTryDecimal(txt7.Text.Trim()) + "," + CommonFun.ComTryDecimal(txt8.Text.Trim()) + ",'" + txt9.Text.Trim() + "')";
                        list.Add(sql);
                    }
                }
                string sqlinsertsp = "insert into OM_GZTZSP(GZTZSP_SPBH,GZTZSP_YEARMONTH,GZTZSP_SQRSTID,GZTZSP_SQRNAME,GZTZSP_SQTIME) values('" + bh + "','" + stryearmonth + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "')";
                list.Add(sqlinsertsp);
                DBCallCommon.ExecuteTrans(list);
                Response.Redirect("OM_GZTZSPdetail.aspx?spid=" + bh);
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (!FileUpload.HasFile)
            {
                Response.Write("<script>alert('尚未上传Excel表格')</script>");
                return;
            }

            if (System.IO.Path.GetExtension(FileUpload.FileName).ToString().ToLower() != ".xls")
            {
                Response.Write("<script>alert('只可以选择Excel（.xls）文件')</script>");
                return;
            }
            try
            {
                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + FileUpload.FileName;
                string savePath = @"E:/薪酬异动" + filename;//TOCHANGE
                FileUpload.SaveAs(savePath);
                ExcelHelper EH = new ExcelHelper(savePath);
                DataTable ImportDate = EH.ExportExcelToDataTable();
                if (EH.IsError(ImportDate, "补发加班费") || EH.IsError(ImportDate, "补发中夜班费") || EH.IsError(ImportDate, "调整补发") || EH.IsError(ImportDate, "调整补扣"))
                {
                    Response.Write("<script>alert('Excel数据有误！请检查标记为ERROR的单元格。')</script>");
                }
                Det_Repeater.DataSource = GetImportDate(ImportDate);
                Det_Repeater.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('导入出错！请检查导入文件格式！"+ex.Message+"')</script>");
                return;
            }           
        }

        protected DataTable GetImportDate(DataTable ExcelDt) 
        {
            ExcelDt.Columns["姓名"].ColumnName = "gztz_name";
            ExcelDt.Columns["补发加班费"].ColumnName = "gztz_bfjbf";
            ExcelDt.Columns["补发中夜班费"].ColumnName = "gztz_bfzybf";
            ExcelDt.Columns["调整补发"].ColumnName = "gztz_bf";
            ExcelDt.Columns["调整补扣"].ColumnName = "gztz_bk";
            ExcelDt.Columns["备注"].ColumnName = "gztz_note";
            ExcelDt.Columns.Add("gztz_stid", Type.GetType("System.String"));
            ExcelDt.Columns.Add("gztz_gh", Type.GetType("System.String"));
            ExcelDt.Columns.Add("gztz_bm", Type.GetType("System.String"));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ExcelDt.Rows.Count;i++ )
            {
                sb.Append("'");
                sb.Append(ExcelDt.Rows[i]["gztz_name"].ToString());
                sb.Append("'");
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            string sql = string.Format("SELECT ST_ID AS 'gztz_stid',ST_NAME AS 'gztz_name',DEP_NAME AS 'gztz_bm',ST_WORKNO AS 'gztz_gh' FROM dbo.View_TBDS_STAFFINFO WHERE ST_NAME in ({0})", sb.ToString());
            string _name = string.Empty;
            try
            {
                DataTable SQLDt = DBCallCommon.GetDTUsingSqlText(sql);
                for (int j = 0; j < ExcelDt.Rows.Count;j++ )
                {
                    _name = ExcelDt.Rows[j]["gztz_name"].ToString();
                    DataRow dr = (SQLDt.Select(string.Format("gztz_name = '{0}'", _name)))[0];
                    ExcelDt.Rows[j]["gztz_stid"] = dr["gztz_stid"];
                    ExcelDt.Rows[j]["gztz_gh"] = dr["gztz_gh"];
                    ExcelDt.Rows[j]["gztz_bm"] = dr["gztz_bm"];
                }
            }
            catch (System.Exception ex)
            {
                string ErrorMessage = string.Format("<script>alert('数据：{0} 存在问题，请检查！错误信息：{1}')</script>", _name, ex.Message.ToString());
                Response.Write(ErrorMessage);
            }

            return ExcelDt;
        }
    }
}
