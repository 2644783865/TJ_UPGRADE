using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_PXXQ_SQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.action = Request.QueryString["action"];
                asd.userid = Session["UserID"].ToString();
                asd.username = Session["UserName"].ToString();
                asd.depname = Session["UserDept"].ToString();
                BindData();
                PowerControl();
            }
        }

        private class asd
        {
            public static string username;
            public static string userid;
            public static string depname;
            public static string sjid;
            public static string action;
            public static string id;
            public static DataTable dt;
            public static DataTable dt1;
        }

        private void BindData()
        {
            if (asd.action == "add")
            {
                asd.sjid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                lbDC_TXR.Text = asd.username;
                lbDC_TXRBM.Text = asd.depname;
                lbDC_TXSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                hidTXRID.Value = asd.userid;
            }
            else if (asd.action=="read")
            {
                asd.id = Request.QueryString["id"];
                string sql = "select * from OM_PXDC where DC_ID = "+asd.id;
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                BindPanel(panJBXX);
                sql = string.Empty;
                sql = "select * from OM_PXDC_NR where FATHERID ='"+asd.id+"'";
                asd.dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                rptNR.DataSource = asd.dt1;
                rptNR.DataBind();
            }
            else if (asd.action=="alter")
            {
                asd.id = Request.QueryString["id"];
                string sql = "select * from OM_PXDC where DC_ID = " + asd.id;
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                BindPanel(panJBXX);
                sql = string.Empty;
                sql = "select * from OM_PXDC_NR where FATHERID ='" + asd.id + "'";
                asd.dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                rptNR.DataSource = asd.dt1;
                rptNR.DataBind();
            }
        }

        private void PowerControl()
        {   
            if (asd.action=="read")
            {
                btnSave.Visible = false;
                btnBack.Visible = false;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                panJBXX.Enabled = false;
                foreach (Control ctr in rptNR.Controls)
                {
                    if (ctr is TextBox)
                    {
                        ((TextBox)ctr).Enabled = false;
                    }
                }
            }
        }

        private void BindPanel(Panel panel)//绑定panel
        {
            DataTable dt = asd.dt;
            List<string> list_dc = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                list_dc.Add(dc.ColumnName);
            }
            DataRow dr = dt.Rows[0];
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is TextBox)
                {
                    TextBox txt = (TextBox)ctr;
                    if (list_dc.Contains(txt.ID.Substring(3)))
                    {
                        txt.Text = dr[txt.ID.Substring(3)].ToString();
                    }
                }
                else if (ctr is Label)
                {
                    Label lb = (Label)ctr;
                    if (list_dc.Contains(lb.ID.Substring(2)))
                    {
                        lb.Text = dr[lb.ID.Substring(2)].ToString();
                    }
                }
                else if (ctr is DropDownList)
                {
                    DropDownList ddl = (DropDownList)ctr;
                    if (list_dc.Contains(ddl.ID.Substring(3)))
                    {
                        ddl.SelectedValue = dr[ddl.ID.Substring(3)].ToString();
                    }

                }
                else if (ctr is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)ctr;
                    if (list_dc.Contains(rbl.ID.Substring(3)))
                    {
                        if (dr[rbl.ID.Substring(3)].ToString() != "0")
                        {
                            rbl.SelectedValue = dr[rbl.ID.Substring(3)].ToString();
                        }
                    }
                }
                else if (ctr is Panel)
                {
                    Panel pan = (Panel)ctr;
                    BindPanel(pan);
                }
                else if (ctr is CheckBox)
                {
                    CheckBox cbx = (CheckBox)ctr;
                    if (list_dc.Contains(cbx.ID.Substring(3)))
                    {
                        if (dr[cbx.ID.Substring(3)].ToString() != "")
                        {
                            cbx.Checked = true;
                        }
                        else
                        {
                            cbx.Checked = false;
                        }
                    }
                }
                else if (ctr is CheckBoxList)
                {
                    CheckBoxList cbx = (CheckBoxList)ctr;
                    string[] a = dr[cbx.ID.Substring(3)].ToString().Split('|');
                    foreach (ListItem item in cbx.Items)
                    {
                        if (a.Contains(item.Value))
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }
            }
        }

        #region 增加、删除行
        protected void btnAdd_OnClick(object sender, EventArgs e) //增加行的函数
        {
            CreateNewRow(1);
            NoDataPanelSee();
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.rptNR.DataSource = dt;
            this.rptNR.DataBind();
            //InitVar();
        }

        private DataTable GetDataTable() //临时表
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PXXM");
            dt.Columns.Add("PXSJ");
            dt.Columns.Add("PXJS");
            dt.Columns.Add("PXMD");
            dt.Columns.Add("PXFS");
            dt.Columns.Add("BZ");
            foreach (RepeaterItem retItem in rptNR.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("PXXM")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("PXSJ")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("PXJS")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("PXMD")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("PXFS")).Text;
                newRow[5] = ((TextBox)retItem.FindControl("BZ")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)//删除一行
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PXXM");
            dt.Columns.Add("PXSJ");
            dt.Columns.Add("PXJS");
            dt.Columns.Add("PXMD");
            dt.Columns.Add("PXFS");
            dt.Columns.Add("BZ");
            foreach (RepeaterItem retItem in rptNR.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("PXXM")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("PXSJ")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("PXJS")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("PXMD")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("PXFS")).Text;
                    newRow[5] = ((TextBox)retItem.FindControl("BZ")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rptNR.DataSource = dt;
            this.rptNR.DataBind();
            NoDataPanelSee();
        }
        private void NoDataPanelSee()
        {
            if (rptNR.Items.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }
        #endregion

        protected void btnSave_onserverclick(object sender, EventArgs e)
        {
            if (asd.action == "add")
            {
                List<string> list = addlist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('新增sql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            else if (asd.action == "alter")
            {
                List<string> list = alterlist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('修改sql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            Response.Redirect("OM_PXXQ_GL.aspx");
        }

        private List<string> addlist()
        {
            List<string> list = new List<string>();
            string sql = " insert into OM_PXDC (DC_SJID,DC_TXR,DC_TXRID,DC_TXSJ,DC_TXRBM,DC_FM,DC_FMQT,DC_HSSJ,DC_HSSJQT,DC_LB,DC_LBQT,DC_FS,DC_FSQT,DC_XS,DC_XSQT,DC_XM,DC_XMQT) values (";
            sql += "'" + asd.sjid + "',";
            sql += "'" + lbDC_TXR.Text + "',";
            sql += "'" + hidTXRID.Value + "',";
            sql += "'" + lbDC_TXSJ.Text + "',";
            sql += "'" + lbDC_TXRBM.Text + "',";
            sql += "'" + cbxsql(cbxDC_FM) + "',";
            sql += "'" + txtDC_FMQT.Text.Trim() + "',";
            sql += "'" + cbxsql(cbxDC_HSSJ) + "',";
            sql += "'" + txtDC_HSSJQT.Text.Trim() + "',";
            sql += "'" + cbxsql(cbxDC_LB) + "',";
            sql += "'" + txtDC_LBQT.Text.Trim() + "',";
            sql += "'" + cbxsql(cbxDC_FS) + "',";
            sql += "'" + txtDC_FSQT.Text.Trim() + "',";
            sql += "'" + cbxsql(cbxDC_XS) + "',";
            sql += "'" + txtDC_XSQT.Text.Trim() + "',";
            sql += "'" + cbxsql(cbxDC_XM) + "',";
            sql += "'" + txtDC_XMQT.Text.Trim() + "',";
            sql = sql.Trim(',');
            sql += ")";
            list.Add(sql);
            sql = string.Empty;
            for (int i = 0, length = rptNR.Items.Count; i < length; i++)
            {
                string PXXM = ((TextBox)rptNR.Items[i].FindControl("PXXM")).Text.Trim();
                string PXSJ = ((TextBox)rptNR.Items[i].FindControl("PXSJ")).Text.Trim();
                string PXJS = ((TextBox)rptNR.Items[i].FindControl("PXJS")).Text.Trim();
                string PXMD = ((TextBox)rptNR.Items[i].FindControl("PXMD")).Text.Trim();
                string PXFS = ((TextBox)rptNR.Items[i].FindControl("PXFS")).Text.Trim();
                string BZ = ((TextBox)rptNR.Items[i].FindControl("BZ")).Text.Trim();
                sql = " insert into OM_PXDC_NR (FATHERID,PXXM,PXSJ,PXJS,PXMD,PXFS,BZ) select DC_ID as FATHERID ,'" + PXXM + "' as PXXM,'" + PXSJ + "' as PXSJ,'" + PXJS + "' as PXJS,'" + PXMD + "' as PXMD,'" + PXFS + "' as PXFS,'" + BZ + "' as BZ from OM_PXDC where DC_SJID='"+asd.sjid+"'";
                list.Add(sql);
                sql = string.Empty;
            }
            return list;
        }

        private string cbxsql(CheckBoxList cbx)
        {
            string sql = "";
            foreach (ListItem item in cbx.Items)
            {
                if (item.Selected)
                {
                    sql += item.Value + "|";
                }
            }
            sql = sql.Trim('|');
            return sql;
        }

        private List<string> alterlist()
        {
            List<string> list = new List<string>();
            string sql = " update OM_PXDC set ";
            sql += "DC_FM='" + cbxsql(cbxDC_FM)+"',";
            sql += "DC_FMQT='" + txtDC_FMQT.Text + "',";
            sql += "DC_HSSJ='" + cbxsql(cbxDC_HSSJ) + "',";
            sql += "DC_HSSJQT='" + txtDC_HSSJQT.Text.Trim() + "',";
            sql += "DC_LB='" + cbxsql(cbxDC_LB) + "',";
            sql += "DC_LBQT='" + txtDC_LBQT.Text.Trim() + "',";
            sql += "DC_FS='" + cbxsql(cbxDC_FS) + "',";
            sql += "DC_FSQT='" + txtDC_FSQT.Text.Trim() + "',";
            sql += "DC_XS='" + cbxsql(cbxDC_XS) + "',";
            sql += "DC_XSQT='" + txtDC_XSQT.Text.Trim() + "',";
            sql += "DC_XM='" + cbxsql(cbxDC_XM) + "',";
            sql += "DC_XMQT='" + txtDC_XMQT.Text.Trim() + "'";
            sql += " where DC_ID ="+asd.id;
            list.Add(sql);
            sql = string.Empty;
            for (int i = 0, length = rptNR.Items.Count; i < length; i++)
            {
                string PXXM = ((TextBox)rptNR.Items[i].FindControl("PXXM")).Text.Trim();
                string PXSJ = ((TextBox)rptNR.Items[i].FindControl("PXSJ")).Text.Trim();
                string PXJS = ((TextBox)rptNR.Items[i].FindControl("PXJS")).Text.Trim();
                string PXMD = ((TextBox)rptNR.Items[i].FindControl("PXMD")).Text.Trim();
                string PXFS = ((TextBox)rptNR.Items[i].FindControl("PXFS")).Text.Trim();
                string BZ = ((TextBox)rptNR.Items[i].FindControl("BZ")).Text.Trim();
                sql = " update OM_PXDC_NR set ";
                sql += "PXXM='" + PXXM+"',";
                sql += "PXSJ='" + PXSJ + "',";
                sql += "PXJS='" + PXJS + "',";
                sql += "PXMD='" + PXMD + "',";
                sql += "PXFS='" + PXFS + "',";
                sql += "BZ='" + BZ + "'";
                sql += " where FATHERID='"+asd.id+"'";
                list.Add(sql);
                sql = string.Empty;
            }
            return list;
        }

        protected void btnBack_onserverclick(object sender, EventArgs e)
        {
            Response.Redirect("OM_PXXQ_GL.aspx");
        }
    }
}
