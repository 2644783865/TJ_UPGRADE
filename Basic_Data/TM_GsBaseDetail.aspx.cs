using System;
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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class TM_GsBaseDetail : System.Web.UI.Page
    {
        string action = "";
        string context = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                action = Request.QueryString["action"].ToString().Trim();
                if (action == "add")
                {
                    defaultdata();
                    CreateNewRow(12);
                }
                else if (action == "edit")
                {
                    lbusername.Text = Session["UserName"].ToString().Trim();
                    lbuserid.Text = Session["UserID"].ToString().Trim();
                    binddata();
                }
                else if (action == "view")
                {
                    binddata();
                    btnsave.Visible = false;
                    btnadd.Visible = false;
                    delete.Visible = false;
                }
            }
        }
        //默认数据
        private void defaultdata()
        {
            lbzdper.Text = Session["UserName"].ToString().Trim();
            lbzdperid.Text = Session["UserID"].ToString().Trim();
            lbusername.Text = Session["UserName"].ToString().Trim();
            lbuserid.Text = Session["UserID"].ToString().Trim();
            lbzdtime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
        }
        //绑定数据
        private void binddata()
        {
            context = Request.QueryString["context"].ToString().Trim();
            string sqltext0 = "select * from TBTM_GONGSBASE where context='" + context + "'";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext0);
            if (dt0.Rows.Count > 0)
            {
                lbzdper.Text = dt0.Rows[0]["zdrname"].ToString().Trim();
                lbzdperid.Text = dt0.Rows[0]["zdrid"].ToString().Trim();
                lbzdtime.Text = dt0.Rows[0]["zdtime"].ToString().Trim();
                rad_state.SelectedValue = dt0.Rows[0]["state"].ToString().Trim();
                txtcpname.Text = dt0.Rows[0]["cpname"].ToString().Trim();
                txtcpguige.Text = dt0.Rows[0]["cpguige"].ToString().Trim();
                txtzongmap.Text = dt0.Rows[0]["zongmap"].ToString().Trim();
                txtbjname.Text = dt0.Rows[0]["bjname"].ToString().Trim();
                txtbjmap.Text = dt0.Rows[0]["bjmap"].ToString().Trim();
                txtbjpergs.Text = dt0.Rows[0]["bjpergs"].ToString().Trim();
                txtcontext.Text = dt0.Rows[0]["context"].ToString().Trim();
                txtnote.Text = dt0.Rows[0]["notetotal"].ToString().Trim();
            }
            string sqltext1 = "select * from TBTM_GONGSBASEDETAIL where detailcontext='" + context + "' order by ljmap,mxid";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            if (dt1.Rows.Count > 0)
            {
                Det_Repeater.DataSource = dt1;
                Det_Repeater.DataBind();
            }
        }



        //增加行
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
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("mxid");
            dt.Columns.Add("ljmap");
            dt.Columns.Add("ljname");
            dt.Columns.Add("ljnum");
            dt.Columns.Add("cfjmapbh");
            dt.Columns.Add("cfjname");
            dt.Columns.Add("cfjnum");
            dt.Columns.Add("gxdetal");
            dt.Columns.Add("cfjpergs");
            dt.Columns.Add("cfjtolgs");
            dt.Columns.Add("notedetail");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HiddenField)retItem.FindControl("mxid")).Value.Trim();
                newRow[1] = ((TextBox)retItem.FindControl("ljmap")).Text.Trim();
                newRow[2] = ((TextBox)retItem.FindControl("ljname")).Text.Trim();
                newRow[3] = ((TextBox)retItem.FindControl("ljnum")).Text.Trim();
                newRow[4] = ((TextBox)retItem.FindControl("cfjmapbh")).Text.Trim();
                newRow[5] = ((TextBox)retItem.FindControl("cfjname")).Text.Trim();
                newRow[6] = ((TextBox)retItem.FindControl("cfjnum")).Text.Trim();
                newRow[7] = ((TextBox)retItem.FindControl("gxdetal")).Text.Trim();
                newRow[8] = ((TextBox)retItem.FindControl("cfjpergs")).Text.Trim();
                newRow[9] = ((TextBox)retItem.FindControl("cfjtolgs")).Text.Trim();
                newRow[10] = ((TextBox)retItem.FindControl("notedetail")).Text.Trim();
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        //删除行
        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("mxid");
            dt.Columns.Add("ljmap");
            dt.Columns.Add("ljname");
            dt.Columns.Add("ljnum");
            dt.Columns.Add("cfjmapbh");
            dt.Columns.Add("cfjname");
            dt.Columns.Add("cfjnum");
            dt.Columns.Add("gxdetal");
            dt.Columns.Add("cfjpergs");
            dt.Columns.Add("cfjtolgs");
            dt.Columns.Add("notedetail");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((HiddenField)retItem.FindControl("mxid")).Value.Trim();
                    newRow[1] = ((TextBox)retItem.FindControl("ljmap")).Text.Trim();
                    newRow[2] = ((TextBox)retItem.FindControl("ljname")).Text.Trim();
                    newRow[3] = ((TextBox)retItem.FindControl("ljnum")).Text.Trim();
                    newRow[4] = ((TextBox)retItem.FindControl("cfjmapbh")).Text.Trim();
                    newRow[5] = ((TextBox)retItem.FindControl("cfjname")).Text.Trim();
                    newRow[6] = ((TextBox)retItem.FindControl("cfjnum")).Text.Trim();
                    newRow[7] = ((TextBox)retItem.FindControl("gxdetal")).Text.Trim();
                    newRow[8] = ((TextBox)retItem.FindControl("cfjpergs")).Text.Trim();
                    newRow[9] = ((TextBox)retItem.FindControl("cfjtolgs")).Text.Trim();
                    newRow[10] = ((TextBox)retItem.FindControl("notedetail")).Text.Trim();
                    dt.Rows.Add(newRow);
                }
            }
            for (int i = 0; i < Det_Repeater.Items.Count; i++)
            {
                CheckBox chkdel = (CheckBox)Det_Repeater.Items[i].FindControl("chk");
                if (chkdel.Checked)
                {
                    string cfjtolgs = ((TextBox)Det_Repeater.Items[i].FindControl("cfjtolgs")).Text.Trim();
                    string ljnum=((TextBox)Det_Repeater.Items[i].FindControl("ljnum")).Text.Trim();
                    if (cfjtolgs != "" && ljnum != "" && txtbjpergs.Text.Trim() != "" && txtbjpergs.Text.Trim()!="0")
                    {
                        string bjpergs = txtbjpergs.Text.Trim();
                        txtbjpergs.Text = ((CommonFun.ComTryDecimal(bjpergs)) - (CommonFun.ComTryDecimal(cfjtolgs)) * (CommonFun.ComTryDecimal(ljnum))).ToString().Trim();
                    }
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
        }

        protected void btnsave_click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            string sqltext = "";
            action = Request.QueryString["action"].ToString().Trim();
            if (txtbjmap.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写图号!');", true);
                return;
            }
            if (action == "add")
            {
                string newcontext = DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + "-" + lbuserid.Text.Trim();
                string sqladd0 = "select * from TBTM_GONGSBASE where bjmap='" + txtbjmap.Text.Trim() + "'";
                DataTable dtadd0 = DBCallCommon.GetDTUsingSqlText(sqladd0);
                if (dtadd0.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在该图号!');", true);
                    return;
                }
                else
                {
                    sqltext = "insert into TBTM_GONGSBASE(context,cpname,cpguige,zongmap,bjmap,bjname,bjpergs,notetotal,state,zdrname,zdrid,zdtime) values('" + newcontext + "','" + txtcpname.Text.Trim() + "','" + txtcpguige.Text.Trim() + "','" + txtzongmap.Text.Trim() + "','" + txtbjmap.Text.Trim() + "','" + txtbjname.Text.Trim() + "'," + CommonFun.ComTryDecimal(txtbjpergs.Text.Trim()) + ",'" + txtnote.Text.Trim() + "','" + rad_state.SelectedValue.Trim() + "','" + lbusername.Text.Trim() + "','" + lbuserid.Text.Trim() + "','" + lbzdtime.Text.Trim() + "')";
                    list_sql.Add(sqltext);
                    string ljmap="";
                    string ljname="";
                    for (int i = 0; i < Det_Repeater.Items.Count; i++)
                    {
                        if (((TextBox)Det_Repeater.Items[i].FindControl("ljmap")).Text.Trim() != "")
                        {
                            ljmap = ((TextBox)Det_Repeater.Items[i].FindControl("ljmap")).Text.Trim();
                            ljname = ((TextBox)Det_Repeater.Items[i].FindControl("ljname")).Text.Trim();
                        }
                        if (((TextBox)Det_Repeater.Items[i].FindControl("cfjmapbh")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfjname")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfjnum")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gxdetal")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfjpergs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfjtolgs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("notedetail")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("ljnum")).Text.Trim() != "")
                        {
                            sqltext = "insert into TBTM_GONGSBASEDETAIL(detailcontext,ljmap,ljname,ljnum,cfjmapbh,cfjname,cfjnum,gxdetal,cfjpergs,cfjtolgs,notedetail) values('" + newcontext + "','" + ljmap + "','" + ljname + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("ljnum")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("cfjmapbh")).Text.Trim() + "','" + ((TextBox)Det_Repeater.Items[i].FindControl("cfjname")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfjnum")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("gxdetal")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfjpergs")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfjtolgs")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("notedetail")).Text.Trim() + "')";
                            list_sql.Add(sqltext);
                        }
                    }
                    DBCallCommon.ExecuteTrans(list_sql);
                    Response.Redirect("TM_GsBaseDetail.aspx?action=edit&context=" + newcontext);
                }
            }
            else if (action == "edit")
            {
                context = Request.QueryString["context"].ToString().Trim();
                string sqledit0 = "select * from TBTM_GONGSBASE where bjmap='" + txtbjmap.Text.Trim() + "' and context!='" + context + "'";
                DataTable dtedit0 = DBCallCommon.GetDTUsingSqlText(sqledit0);
                if (dtedit0.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在该图号!');", true);
                    return;
                }
                else
                {
                    sqltext = "update TBTM_GONGSBASE set cpname='" + txtcpname.Text.Trim() + "',cpguige='" + txtcpguige.Text.Trim() + "',zongmap='" + txtzongmap.Text.Trim() + "',bjmap='" + txtbjmap.Text.Trim() + "',bjname='" + txtbjname.Text.Trim() + "',bjpergs=" + CommonFun.ComTryDecimal(txtbjpergs.Text.Trim()) + ",notetotal='" + txtnote.Text.Trim() + "',state='" + rad_state.SelectedValue.Trim() + "',zdrname='" + lbusername.Text.Trim() + "',zdrid='" + lbuserid.Text.Trim() + "',zdtime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where context='" + context + "'";
                    list_sql.Add(sqltext);
                    string sqldelete = "delete from TBTM_GONGSBASEDETAIL where detailcontext='" + context + "'";
                    DBCallCommon.ExeSqlText(sqldelete);
                    string ljmap = "";
                    string ljname = "";
                    for (int i = 0; i < Det_Repeater.Items.Count; i++)
                    {
                        if (((TextBox)Det_Repeater.Items[i].FindControl("ljmap")).Text.Trim() != "")
                        {
                            ljmap = ((TextBox)Det_Repeater.Items[i].FindControl("ljmap")).Text.Trim();
                            ljname = ((TextBox)Det_Repeater.Items[i].FindControl("ljname")).Text.Trim();
                        }
                        if (((TextBox)Det_Repeater.Items[i].FindControl("cfjmapbh")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfjname")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfjnum")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gxdetal")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfjpergs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfjtolgs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("notedetail")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("ljnum")).Text.Trim() != "")
                        {
                            sqltext = "insert into TBTM_GONGSBASEDETAIL(detailcontext,ljmap,ljname,ljnum,cfjmapbh,cfjname,cfjnum,gxdetal,cfjpergs,cfjtolgs,notedetail) values('" + context + "','" + ljmap + "','" + ljname + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("ljnum")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("cfjmapbh")).Text.Trim() + "','" + ((TextBox)Det_Repeater.Items[i].FindControl("cfjname")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfjnum")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("gxdetal")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfjpergs")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfjtolgs")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("notedetail")).Text.Trim() + "')";
                            list_sql.Add(sqltext);
                        }
                    }
                    DBCallCommon.ExecuteTrans(list_sql);
                    binddata();
                }
            }
        }
    }
}
