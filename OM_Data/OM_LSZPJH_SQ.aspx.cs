using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_LSZPJH_SQ : System.Web.UI.Page
    {
        string action = string.Empty;
        string username = string.Empty;
        string depname = string.Empty;
        string depid = string.Empty;
        string id = string.Empty;
        DataTable dts = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            username = Session["UserName"].ToString();
            depname = Session["UserDept"].ToString();
            depid = Session["UserDeptID"].ToString();
            id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                if (action == "add")
                {
                    hidJH_SJID.Value = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                }
                else
                {
                    string sql = "select * from OM_ZPJH where JH_ID='" + id + "'";
                    dts = DBCallCommon.GetDTUsingSqlText(sql);
                    hidJH_SJID.Value = dts.Rows[0]["JH_SJID"].ToString();
                }
                BindData();
                PowerControl();
            }
        }

        private void BindData()
        {
            BindDropdownList();
            if (action == "add")
            {
                lbJH_SJ.Text = DateTime.Now.ToString("yyyy");
                lbJH_SQR.Text = username;
                lbJH_SQSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtJH_ZPBM.Text = depname;
            }
            if (action == "read" || action == "alter")
            {
                BindPanel(panJBXX);
            }
        }

        private void PowerControl()
        {
            if (action == "read")
            {
                btnSubmit.Visible = false;
                btnBack.Visible = false;
                btnSave.Visible = false;
            }
        }

        private void BindDropdownList()
        {
            string sql = "select DEP_CODE,DEP_NAME,DEP_FATHERID from TBDS_DEPINFO where DEP_FATHERID='" + depid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlJH_GWMC.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_CODE"].ToString()));
            }
        }

        private void BindPanel(Panel panel)//绑定panel
        {
            string sql = "select * from OM_ZPJH where JH_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
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
                        ddl.SelectedValue = dr[ddl.ID.Substring(3) + "ID"].ToString();
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
            }
        }


        protected void btnSubmit_onserverclick(object sender, EventArgs e)
        {
            if ((SFBC.ifsave == "" || SFBC.ifsave == null) && action == "add")
            {
                Response.Write("<script type='text/javascript'>alert('请先点击保存再提交！！！')</script>");
                return;
            }
            string sql = "update OM_ZPJH set JH_SFTJ ='y', JH_SFHZ='n' where JH_SJID='" + hidJH_SJID.Value + "'";
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch
            {
                Response.Write("<script type='text/javascript'>alert('提交出现问题，请与管理员联系！！！')</script>");
                return;
            }
            SFBC.ifsave = string.Empty;
            SendEmail();
            Response.Redirect("OM_LSZPJH_GL.aspx");
        }

        public class SFBC
        {
            public static string ifsave;
        }

        protected void btnSave_onserverclick(object sender, EventArgs e)
        {
            if (action == "add")
            {
                SFBC.ifsave = "y";
                string sql = Addsql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {

                    Response.Write("<script type='text/javascript'>alert('addsql出现问题，请与管理员联系！！！')</script>");
                    return;
                }
                btnSave.Visible = false;
            }
            if (action == "alter")
            {
                string sql = Altersql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('alter出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }

        }

        private string Addsql()
        {
            string sql = "insert into OM_ZPJH (JH_SJID,JH_SQR,JH_SQSJ,JH_SJ,JH_ZPBM,JH_GWMCID,JH_GWMC,JH_XQLY,JH_ZPFS,JH_ZPRS,JH_ZPGW,JH_ZPZY,JH_ZPYX,JH_ZPXL,JH_ZPXB,JH_ZPNL,JH_ZPYQ,JH_QTYQ,JH_XWDGSJ,JH_NGZDD,JH_QT,JH_SFTJ,JH_LX) values (";
            sql += "'" + hidJH_SJID.Value + "',";
            sql += "'" + lbJH_SQR.Text + "',";
            sql += "'" + lbJH_SQSJ.Text + "',";
            sql += "'" + lbJH_SJ.Text + "',";
            sql += "'" + txtJH_ZPBM.Text.Trim() + "',";
            sql += "'" + ddlJH_GWMC.SelectedValue + "',";
            sql += "'" + ddlJH_GWMC.SelectedItem.Text + "',";
            sql += "'" + rblJH_XQLY.SelectedValue + "',";
            sql += "'" + rblJH_ZPFS.SelectedValue + "',";
            sql += "'" + txtJH_ZPRS.Text.Trim() + "',";
            sql += "'" + txtJH_ZPGW.Text.Trim() + "',";
            sql += "'" + txtJH_ZPZY.Text.Trim() + "',";
            sql += "'" + txtJH_ZPYX.Text.Trim() + "',";
            sql += "'" + txtJH_ZPXL.Text.Trim() + "',";
            sql += "'" + rblJH_ZPXB.SelectedValue + "',";
            sql += "'" + txtJH_ZPNL.Text.Trim() + "',";
            sql += "'" + txtJH_ZPYQ.Text.Trim() + "',";
            sql += "'" + txtJH_QTYQ.Text.Trim() + "',";
            sql += "'" + txtJH_XWDGSJ.Text.Trim() + "',";
            sql += "'" + txtJH_NGZDD.Text.Trim() + "',";
            sql += "'" + txtJH_QT.Text.Trim() + "',";
            sql += "'n','LS'";
            sql += ")";
            return sql;
        }

        private string Altersql()
        {
            string sql = "update OM_ZPJH set ";
            sql += "JH_GWMCID='" + ddlJH_GWMC.SelectedValue + "',";
            sql += "JH_GWMC='" + ddlJH_GWMC.SelectedItem.Text + "',";
            sql += "JH_XQLY='" + rblJH_XQLY.SelectedValue + "',";
            sql += "JH_ZPFS='" + rblJH_ZPFS.SelectedValue + "',";
            sql += "JH_ZPRS='" + txtJH_ZPRS.Text.Trim() + "',";
            sql += "JH_ZPGW='" + txtJH_ZPGW.Text.Trim() + "',";
            sql += "JH_ZPZY='" + txtJH_ZPZY.Text.Trim() + "',";
            sql += "JH_ZPYX='" + txtJH_ZPYX.Text.Trim() + "',";
            sql += "JH_ZPXL='" + txtJH_ZPXL.Text.Trim() + "',";
            sql += "JH_ZPXB='" + rblJH_ZPXB.SelectedValue + "',";
            sql += "JH_ZPNL='" + txtJH_ZPNL.Text.Trim() + "',";
            sql += "JH_ZPYQ='" + txtJH_ZPYQ.Text.Trim() + "',";
            sql += "JH_QTYQ='" + txtJH_QTYQ.Text.Trim() + "',";
            sql += "JH_XWDGSJ='" + txtJH_XWDGSJ.Text.Trim() + "',";
            sql += "JH_NGZDD='" + txtJH_NGZDD.Text.Trim() + "',";
            sql += "JH_QT='" + txtJH_QT.Text.Trim() + "'";
            sql += " where JH_ID=" + id;
            return sql;
        }

        private void SendEmail()
        {
            //if (action == "add")
            //{
            //    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("151"), new List<string>(), new List<string>(), "招聘申请通知", "“" + txtJH_ZPBM.Text + "”申请招聘“" + ddlJH_GWMC.SelectedItem.Text + "”" + txtJH_ZPRS.Text + "人，请您登录系统查看");//给李圆发邮件
            //}
        }


        protected void btnBack_onserverclick(object sender, EventArgs e)
        {
            Response.Redirect("OM_LSZPJH_GL.aspx");
        }
    }
}
