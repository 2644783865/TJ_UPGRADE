using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_JHDQX : System.Web.UI.Page
    {
        string id = string.Empty;
        string action = string.Empty;
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["ID"];
            action = Request.QueryString["action"];
            username = Session["UserName"].ToString();
            if (!IsPostBack)
            {
                BindData();
                PowerControl();
            }
        }

        private void BindData()
        {
            string sql1 = "select a.*,b.ST_NAME from TBCM_PLAN as a left join TBDS_STAFFINFO as b on b.ST_ID=a.CM_MANCLERK where ID='" + id + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            string sql2 = "select a.CM_ID,TSA_CANCEL,TSA_ID,TSA_ENGNAME,TSA_MAP,TSA_NUMBER,TSA_UNIT,TSA_MATERIAL,TSA_IDNOTE,b.CM_REFER as TSA_TYPENAME from TBCM_BASIC as a left join TBCM_TYPE as b on a.TSA_TYPE=b.CM_TYPE where ID='" + id + "'";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
            if (dt1.Rows.Count > 0)
            {
                BindPanel(dt1, panJBXX);
            }
            gvMX.DataSource = dt2;
            gvMX.DataBind();
            if (action == "cancel")
            {
                txtZDR.Text = username;
                lbZDR_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (action == "read" || action == "check" || action == "alter")
            {
                string sql3 = "select * from CM_SP where SPFATHERID='" + id + "' and SPLX='JHDQX'";
                DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql3);
                BindPanel(dt3, panZDR);
                BindPanel(dt3, panSPR1);

                if (action == "check")
                {
                    lbSPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
        }

        private void PowerControl()
        {
            if (action == "cancel")
            {
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                TabPanel2.Visible = true;
            }
            else if (action == "read")
            {
                btnSubmit.Visible = false;
                btnBack.Visible = false;
                TabPanel2.Visible = false;
            }
            else if (action == "check")
            {
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                TabPanel2.Visible = false;
            }
        }

        protected void gvMX_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                string TSA_CANCEL = ((HiddenField)e.Row.FindControl("cancel")).Value;
                CheckBox cbx = (CheckBox)e.Row.FindControl("cbxXUHAO");
                if (TSA_CANCEL == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    cbx.Checked = true;
                }
            }
        }

        private void BindPanel(DataTable dt, Panel panel)
        {
            for (int i = 0, length = panel.Controls.Count; i < length; i++)
            {
                if (panel.Controls[i] is TextBox)
                {
                    TextBox txt = (TextBox)panel.Controls[i];
                    if (dt.Columns.Contains(txt.ID.Substring(3)))
                    {
                        txt.Text = dt.Rows[0][txt.ID.Substring(3)].ToString();
                    }
                }
                else if (panel.Controls[i] is Label)
                {
                    Label lb = (Label)panel.Controls[i];
                    if (dt.Columns.Contains(lb.ID.Substring(2)))
                    {
                        lb.Text = dt.Rows[0][lb.ID.Substring(2)].ToString();
                    }
                }
                else if (panel.Controls[i] is HiddenField)
                {
                    HiddenField hid = (HiddenField)panel.Controls[i];
                    if (dt.Columns.Contains(hid.ID.Substring(3)))
                    {
                        hid.Value = dt.Rows[0][hid.ID.Substring(3)].ToString();
                    }
                }
                else if (panel.Controls[i] is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)panel.Controls[i];
                    if (dt.Columns.Contains(rbl.ID.Substring(3)))
                    {
                        for (int j = 0, a = rbl.Items.Count; j < a; j++)
                        {
                            if (rbl.Items[j].Value == dt.Rows[0][rbl.ID.Substring(3)].ToString())
                            {
                                rbl.Items[j].Selected = true;
                                break;
                            }
                        }
                    }
                }
                else if (panel.Controls[i] is CheckBoxList)
                {
                    CheckBoxList cbxl = (CheckBoxList)panel.Controls[i];
                    if (dt.Columns.Contains(cbxl.ID.Substring(4)))
                    {
                        if (dt.Rows[0][cbxl.ID.Substring(4)].ToString().Contains('|') == true)
                        {
                            string[] str = dt.Rows[0][cbxl.ID.Substring(4)].ToString().Split('|');
                            for (int j = 0, a = str.Length; j < a; j++)
                            {
                                for (int k = 0, b = cbxl.Items.Count; k < b; k++)
                                {
                                    if (cbxl.Items[k].Text == str[j])
                                    {
                                        cbxl.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int k = 0, b = cbxl.Items.Count; k < b; k++)
                            {
                                if (cbxl.Items[k].Text == dt.Rows[0][cbxl.ID.Substring(4)].ToString())
                                {
                                    cbxl.Items[k].Selected = true;
                                    break;
                                }
                            }
                        }
                    }

                }
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (action == "cancel")
            {
                int num = 0;
                for (int i = 0, length = gvMX.Rows.Count; i < length; i++)
                {
                    CheckBox cbx = (CheckBox)gvMX.Rows[i].FindControl("cbxXUHAO");
                    if (cbx.Checked)
                    {
                        num++;
                        string cm_id = ((HiddenField)gvMX.Rows[i].FindControl("hide")).Value;
                    }
                }
                if (num == 0)
                {
                    Response.Write("<script>alert('请您勾选需要取消的项！！！')</script>");
                    return;
                }
                else
                {
                    if (num == gvMX.Rows.Count)
                    {
                        Response.Write("<script>alert('整张单据将被取消！！！')</script>");
                    }
                    List<string> list = Cancelsql();
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        SendEmail();
                    }
                    catch
                    {
                        Response.Write("<script>alert('cancel语句出现错误，请与管理员联系！！！')</script>");
                        return;
                    }
                }
            }
            else if (action == "check")
            {
                if (rblSPR1_JL.SelectedValue != "1" && rblSPR1_JL.SelectedValue != "2")
                {
                    Response.Write("<script>alert('请选择“同意”或者“不同意”！！！')</script>");
                    return;
                }
                List<string> list = Checksql();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    if (rblSPR1_JL.SelectedValue == "1")
                    {
                       // if (!CheckBoxList1.Items[1].Selected)
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("67"), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + lbCM_CONTR.Text.ToString().Trim() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                        //if (!CheckBoxList6.Items[0].Selected)
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("69"), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + lbCM_CONTR.Text.ToString().Trim() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                        if (!CheckBoxList3.Items[3].Selected)
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("95"), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + lbCM_CONTR.Text.ToString().Trim() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                    }
                }
                catch
                {
                    Response.Write("<script>alert('Checksql语句出现错误，请与管理员联系！！！')</script>");
                    return;
                }
            }

            Response.Redirect("CM_JHDQXSP.aspx");
        }

        private List<string> Cancelsql()
        {
            List<string> list = new List<string>();
            string sql = "update TBCM_BASIC set TSA_CANCEL='1' where CM_ID in (";
            int num = 0;
            for (int i = 0, length = gvMX.Rows.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)gvMX.Rows[i].FindControl("cbxXUHAO");
                if (cbx.Checked)
                {
                    num++;
                    string cm_id = ((HiddenField)gvMX.Rows[i].FindControl("hide")).Value;
                    sql += "'" + cm_id + "',";
                }
            }
            sql = sql.Trim(',');
            sql += ")";
            list.Add(sql);
            //如果全部取消，则把计划单打上标记
            if (num == gvMX.Rows.Count)
            {
                sql = "update TBCM_PLAN set CM_CANCEL='1' where ID='" + id + "'";
                list.Add(sql);
            }
            //向审批表中添加数据
            if (num > 0)
            {
                sql = "insert into CM_SP (SPFATHERID,SPLX,SPJB,ZDR,ZDR_SJ,ZDR_JY,SPR1,SPZT) values (";
                sql += "'" + hidID.Value + "',";
                sql += "'JHDQX',";
                sql += "'1',";
                sql += "'" + txtZDR.Text.Trim() + "',";
                sql += "'" + lbZDR_SJ.Text.Trim() + "',";
                sql += "'" + txtZDR_JY.Text.Trim() + "',";
                sql += "'" + txtSPR1.Text.Trim() + "',";
                sql += "'0')";
                list.Add(sql);
            }
            return list;
        }

        private List<string> Checksql()
        {
            List<string> list = new List<string>();
            string sqlText = "";
            string sql = "update CM_SP set ";
            sql += "SPR1_JL='" + rblSPR1_JL.SelectedValue + "',";
            sql += "SPR1_SJ='" + lbSPR1_SJ.Text + "',";
            sql += "SPR1_JY='" + txtSPR1_JY.Text.Trim() + "',";
            if (rblSPR1_JL.SelectedValue == "2")
            {
                sql += "SPZT='n'";
            }
            else
            {
                sql += "SPZT='y'";
            }
            sql += "where SPFATHERID='" + id + "' and SPLX='JHDQX'";
            list.Add(sql);
            if (rblSPR1_JL.SelectedValue == "1")
            {
                for (int i = 0; i < gvMX.Rows.Count; i++)
                {
                    if (((CheckBox)gvMX.Rows[i].FindControl("cbxXUHAO")).Checked)
                    {
                        string TSA_ID = ((System.Web.UI.WebControls.Label)gvMX.Rows[i].FindControl("TSA_ID")).Text.ToString();
                        string TSA_ENGNAME = ((System.Web.UI.WebControls.Label)gvMX.Rows[i].FindControl("TSA_ENGNAME")).Text.ToString();
                        string TSA_MAP = ((System.Web.UI.WebControls.Label)gvMX.Rows[i].FindControl("TSA_MAP")).Text.ToString();
                        string TSA_NUMBER = ((System.Web.UI.WebControls.Label)gvMX.Rows[i].FindControl("TSA_NUMBER")).Text.ToString();
                        string TSA_UNIT = ((System.Web.UI.WebControls.Label)gvMX.Rows[i].FindControl("TSA_UNIT")).Text.ToString();
                        string TSA_MATERIAL = ((System.Web.UI.WebControls.Label)gvMX.Rows[i].FindControl("TSA_MATERIAL")).Text.ToString();
                        sqlText = "update TBPM_DETAIL set CM_JHDTYPE='4' where CM_CONTR='" + lbCM_CONTR.Text.ToString().Trim() + "' and TSA_ID='" + TSA_ID + "' and CM_ENGNAME='" + TSA_ENGNAME + "'and CM_MAP='" + TSA_MAP + "'and CM_MATERIAL='" + TSA_MATERIAL + "'and CM_NUMBER='" + TSA_NUMBER + "'and CM_UNIT='" + TSA_UNIT + "'";
                        list.Add(sqlText);
                    }
                }
            }
            if (rblSPR1_JL.SelectedValue == "2")
            {
                sql = "update TBCM_BASIC set TSA_CANCEL='0' where CM_ID in (";
                int num = 0;
                for (int i = 0, length = gvMX.Rows.Count; i < length; i++)
                {
                    CheckBox cbx = (CheckBox)gvMX.Rows[i].FindControl("cbxXUHAO");
                    if (cbx.Checked)
                    {
                        num++;
                        string cm_id = ((HiddenField)gvMX.Rows[i].FindControl("hide")).Value;
                        sql += "'" + cm_id + "',";
                    }
                }
                sql = sql.Trim(',');
                sql += ")";
                list.Add(sql);
                if (num == gvMX.Rows.Count)
                {
                    sql = "update TBCM_PLAN set CM_CANCEL='0' where ID='" + id + "'";
                    list.Add(sql);
                }
            }
            return list;
        }

        private List<string> Alterlist()
        {
            List<string> list = new List<string>();

            return list;
        }

        private void SendEmail()
        {
            if (action == "cancel")
            {
                string sql = " select * from TBCM_PLAN where ID='" + id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                foreach (ListItem item in CheckBoxList0.Items)
                {
                    if (item.Selected)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + dt.Rows[0]["CM_CONTR"].ToString() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                    }
                }
                //jishubu
                foreach (ListItem item in CheckBoxList1.Items)
                {
                    if (item.Selected)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + dt.Rows[0]["CM_CONTR"].ToString() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                    }
                }
                //zhiliangbu
                foreach (ListItem item in CheckBoxList6.Items)
                {
                    if (item.Selected)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + dt.Rows[0]["CM_CONTR"].ToString() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                    }
                }
                foreach (ListItem item in CheckBoxList2.Items)
                {
                    if (item.Selected)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + dt.Rows[0]["CM_CONTR"].ToString() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                    }
                }
                foreach (ListItem item in CheckBoxList3.Items)
                {
                    if (item.Selected)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + dt.Rows[0]["CM_CONTR"].ToString() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                    }
                }
                foreach (ListItem item in CheckBoxList4.Items)
                {
                    if (item.Selected)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + dt.Rows[0]["CM_CONTR"].ToString() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                    }
                }
                foreach (ListItem item in CheckBoxList5.Items)
                {
                    if (item.Selected)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "经营计划单取消通知", "合同号为“" + dt.Rows[0]["CM_CONTR"].ToString() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审中查看”");
                    }
                }
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("47"), new List<string>(), new List<string>(), "经营计划单取消审批", "合同号为“" + dt.Rows[0]["CM_CONTR"].ToString() + "”存在取消的部分，请登录系统“市场管理”模块中的“取消评审”审批");
            }
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CM_JHDQXSP.aspx");
        }
    }
}
