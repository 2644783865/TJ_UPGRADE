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
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CARRK_detail : System.Web.UI.Page
    {
        string flag = string.Empty;
        string id = string.Empty;
        string mingchen = "";
        string guige = "";
        string shuliang = "";
        string danjia = "";
        string danwei = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            flag = Request.QueryString["FLAG"].ToString();
            id = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                asd.userid = Session["UserID"].ToString();
                BindData();
            }

        }
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (txtLines.Text.Trim() == "")
            {
                Response.Write("<script>alert('请填写增加行数！！！')</script>");
            }
            else
            {

                int num = Convert.ToInt32(txtLines.Text.Trim());
                CreateNewRow(num);
            }
        }
        private void CreateNewRow(int num)
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();

        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SP_MC");
            dt.Columns.Add("SP_GG");
            dt.Columns.Add("SP_SL");
            dt.Columns.Add("SP_DJ");
            dt.Columns.Add("SP_DANWEI");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)gr.FindControl("txt_mingchen")).Text;
                newRow[1] = ((TextBox)gr.FindControl("txt_guige")).Text;
                newRow[2] = ((TextBox)gr.FindControl("txt_shuliang")).Text;
                newRow[3] = ((TextBox)gr.FindControl("txt_danjia")).Text;
                newRow[4] = ((TextBox)gr.FindControl("txt_danwei")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        protected void btnDelRow_OnClick(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CHK");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要删除的行！');", true);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }

        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_CARRK.aspx");
        }
        private class asd
        {
            public static string userid;
            public static string bh;
            public static DataTable dts;
        }
        private void BindData()
        {
            if (flag == "add")
            {
                lbl_creater.Text = Session["UserName"].ToString();
                lblInDate.Text = DateTime.Now.ToString();
                txtZDR.Text = Session["UserName"].ToString();
                lbZDR_SJ.Text = DateTime.Now.ToString();
                hidZDRID.Value = Session["UserID"].ToString();
            }
            else if (flag == "check")
            {
                string sql = string.Format("select * from OM_CARRK_SP WHERE ZDR_SJ='{0}'", id);
                asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                InitGridView();

                BindPanel(panSP);



            }
            else if (flag == "read")
            {
                string sql = string.Format("select * from OM_CARRK_SP WHERE ZDR_SJ='{0}'", id);
                asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                InitGridView();
                //bind_info();
                BindPanel(panSP);
            }
            PowerControl();
        }
        private void InitGridView()
        {
            string sql = "select * from OM_CARRK_SP WHERE ZDR_SJ='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            lbl_creater.Text = dt.Rows[0]["ZDR"].ToString();
            lblInDate.Text = dt.Rows[0]["ZDR_SJ"].ToString();

        }
        private void BindPanel(Panel panel)//绑定panel
        {
            string sql = "select * from OM_CARRK_SP WHERE ZDR_SJ='" + id + "'";
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
                else if (ctr is HiddenField)
                {
                    HiddenField hid = (HiddenField)ctr;
                    if (list_dc.Contains(hid.ID.Substring(3)))
                    {
                        hid.Value = dr[hid.ID.Substring(3)].ToString();
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
        private void PowerControl()
        {
            if (flag == "check")
            {

                xinzeng.Visible = false;
                txtLines.Visible = false;
                btnAdd.Visible = false;
                btnDelRow.Visible = false;

                panJBXX.Enabled = false;
                panZDR.Enabled = false;
                panSPR1.Enabled = false;
                panSPR2.Enabled = false;
                if (asd.userid == asd.dts.Rows[0]["SPR1_ID"].ToString())
                {
                    panSPR1.Enabled = true;
                }
                if (asd.userid == asd.dts.Rows[0]["SPR2_ID"].ToString())
                {
                    panSPR2.Enabled = true;
                }
            }
            else if (flag == "read")
            {

                xinzeng.Visible = false;
                txtLines.Visible = false;
                btnAdd.Visible = false;
                btnDelRow.Visible = false;
                btnSave.Visible = false;
                btnReturn.Visible = false;


                panZDR.Enabled = false;
                panSPR1.Enabled = false;
                panSPR2.Enabled = false;

            }
        }
        private void writedata()
        {
            List<string> list_sql = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];

                if (((TextBox)gr.FindControl("txt_mingchen")).Text != "" && ((TextBox)gr.FindControl("txt_shuliang")).Text != "")
                {

                    mingchen = ((TextBox)gr.FindControl("txt_mingchen")).Text;
                    guige = ((TextBox)gr.FindControl("txt_guige")).Text;
                    shuliang = ((TextBox)gr.FindControl("txt_shuliang")).Text;
                    danjia = ((TextBox)gr.FindControl("txt_danjia")).Text;
                    danwei = ((TextBox)gr.FindControl("txt_danwei")).Text;
                    double dj = CommonFun.ComTryDouble(danjia);
                    double sl = CommonFun.ComTryDouble(shuliang);
                    double zongjia = dj * sl;
                    string zj = zongjia.ToString();

                    //string sqltext = "insert into OM_CARKUCUN(KC_MC,KC_GG,KC_SL,KC_DJ,KC_ZJ,KC_SJ)";
                    //sqltext += "values('" + mingchen + "','" + guige + "','" + shuliang + "','" + danjia + "','" + zongjia + "','" + lblInDate.Text + "')";
                    //list_sql.Add(sqltext);


                    string sqltext1 = string.Format("insert into OM_CARRK_SP (SP_MC,SP_GG,SP_SL,ZDR,ZDRID,ZDR_SJ,ZDR_JY,SPR1,SPR1_ID,SPR1_JL,SPR1_JY,SPR2,SPR2_ID,SPR2_JL,SPR2_JY,SP_DJ,SP_DANWEI,SPJB,SPZT,SP_ZJ) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", mingchen, guige, shuliang, txtZDR.Text.Trim(), hidZDRID.Value, lbZDR_SJ.Text, txtZDR_JY.Text.Trim(), txtSPR1.Text.Trim(), hidSPR1_ID.Value, "", "", txtSPR2.Text.Trim(), hidSPR2_ID.Value, "", "", danjia, danwei, rblSPJB.SelectedValue, "0", zj);
                    list_sql.Add(sqltext1);


                }
                else
                {
                    Response.Write("<script>alert('请选择“名称、数量”再提交！！！')</script>");
                }

            }
            DBCallCommon.ExecuteTrans(list_sql);
            string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR1_ID.Value);
            string _body = "车品入库审批任务:"
                  + "\r\n制单人：" + lbl_creater.Text.Trim()
                  + "\r\n制单日期：" + lblInDate.Text.Trim();

            string _subject = "您有新的【车品入库】需要审批，请及时处理";
            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string st = "OK";
            if (GridView1.Rows.Count == 0)
            {
                st = "NoData";
            }
            if (st == "OK")
            {
                if (flag == "add")
                {
                    writedata();
                    Response.Write("<script>alert('保存成功！');window.location.href='OM_CARRK.aspx';</script>");
                }
                if (flag == "check")
                {
                    if (asd.userid == asd.dts.Rows[0]["SPR1_ID"].ToString())
                    {
                        if (rblSPR1_JL.SelectedValue != "y" && rblSPR1_JL.SelectedValue != "n")
                        {
                            Response.Write("<script>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
                            return;
                        }
                    }
                    else if (asd.userid == asd.dts.Rows[0]["SPR2_ID"].ToString())
                    {
                        if (rblSPR2_JL.SelectedValue != "y" && rblSPR2_JL.SelectedValue != "n")
                        {
                            Response.Write("<script>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
                            return;
                        }
                    }
                    List<string> list = checklist();
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                    }
                    catch
                    {
                        Response.Write("<script>alert('审批语句出现错误，请与管理员联系！！！')</script>");
                        return;
                    }
                    Response.Redirect("OM_CARRK_SP.aspx");
                }

            }
            else if (st == "NoData")
            {
                Response.Write("<script>alert('提示:无法保存！！！没有入库数据！！！')</script>");
                return;
            }
        }
        private List<string> checklist()
        {
            List<string> list = new List<string>();
            string sql = string.Format("update OM_CARRK_SP set SPR1_JL='{0}',SPR1_JY='{1}',SPR2_JL='{2}',SPR2_JY='{3}' where ZDR_SJ='{4}'", rblSPR1_JL.SelectedValue, txtSPR1_JY.Text.Trim(), rblSPR2_JL.SelectedValue, txtSPR2_JY.Text.Trim(), id);
            list.Add(sql);
            if (asd.dts.Rows[0]["SPJB"].ToString() == "1")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_CARRK_SP set SPZT='y' where ZDR_SJ='{0}'", id);
                    list.Add(sql);
                    string sqlsp = "select * from OM_CARRK_SP where ZDR_SJ='" + id + "'";
                    DataTable dtsp = DBCallCommon.GetDTUsingSqlText(sqlsp);
                    for (int i = 0; i < dtsp.Rows.Count; i++)
                    {
                        string mc = dtsp.Rows[i]["SP_MC"].ToString();
                        string gg = dtsp.Rows[i]["SP_GG"].ToString();
                        string sl = dtsp.Rows[i]["SP_SL"].ToString();
                        string dj = dtsp.Rows[i]["SP_DJ"].ToString();
                        string zj = dtsp.Rows[i]["SP_ZJ"].ToString();
                        string danwei = dtsp.Rows[i]["SP_DANWEI"].ToString();
                        string sqlkc = "select * from OM_CARKUCUN where KC_MC='" + mc + "' and KC_GG='" + gg + "'and KC_DJ='" + dj + "'";
                        DataTable dtkc = DBCallCommon.GetDTUsingSqlText(sqlkc);
                        if (dtkc.Rows.Count == 0)
                        {
                            string sqltext = "insert into OM_CARKUCUN(KC_MC,KC_GG,KC_SL,KC_DJ,KC_ZJ,KC_SJ,KC_DANWEI)";
                            sqltext += "values('" + mc + "','" + gg + "','" + sl + "','" + dj + "','" + zj + "','" + lblInDate.Text + "','" + danwei + "')";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                        else
                        {
                            string kc_sl = dtkc.Rows[0]["KC_SL"].ToString();
                            string kc_zj = dtkc.Rows[0]["KC_ZJ"].ToString();
                            double a = CommonFun.ComTryDouble(sl);
                            double b = CommonFun.ComTryDouble(kc_sl);
                            double c = a + b;
                            double d = CommonFun.ComTryDouble(zj);
                            double e = CommonFun.ComTryDouble(kc_zj);
                            double f = d + e;

                            string sqltext = "update OM_CARKUCUN set KC_SL='" + c.ToString() + "' ,KC_ZJ='" + f.ToString() + "' where KC_MC='" + mc + "' and KC_GG='" + gg + "'and KC_DJ='" + dj + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                    }
                }
            }
            else if (asd.dts.Rows[0]["SPJB"].ToString() == "2")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_CARRK_SP set SPZT='1y' where ZDR_SJ='{0}'", id);
                    list.Add(sql);
                    if (asd.userid == hidSPR1_ID.Value)
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR2_ID.Value);
                        string _body = "车品入库审批任务:"
                              + "\r\n制单人：" + lbl_creater.Text.Trim()
                              + "\r\n制单日期：" + lblInDate.Text.Trim();

                        string _subject = "您有新的【车品入库】需要审批，请及时处理";
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                }
                if (rblSPR2_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_CARRK_SP set SPZT='y' where ZDR_SJ='{0}'", id);
                    list.Add(sql);
                    string sqlsp = "select * from OM_CARRK_SP where ZDR_SJ='" + id + "'";
                    DataTable dtsp = DBCallCommon.GetDTUsingSqlText(sqlsp);
                    for (int i = 0; i < dtsp.Rows.Count; i++)
                    {
                        string mc = dtsp.Rows[i]["SP_MC"].ToString();
                        string gg = dtsp.Rows[i]["SP_GG"].ToString();
                        string sl = dtsp.Rows[i]["SP_SL"].ToString();
                        string dj = dtsp.Rows[i]["SP_DJ"].ToString();
                        string zj = dtsp.Rows[i]["SP_ZJ"].ToString();
                        string danwei = dtsp.Rows[i]["SP_DANWEI"].ToString();
                        string sqlkc = "select * from OM_CARKUCUN where KC_MC='" + mc + "' and KC_GG='" + gg + "'and KC_DJ='" + dj + "'";
                        DataTable dtkc = DBCallCommon.GetDTUsingSqlText(sqlkc);
                        if (dtkc.Rows.Count == 0)
                        {
                            string sqltext = "insert into OM_CARKUCUN(KC_MC,KC_GG,KC_SL,KC_DJ,KC_ZJ,KC_SJ,KC_DANWEI)";
                            sqltext += "values('" + mc + "','" + gg + "','" + sl + "','" + dj + "','" + zj + "','" + lblInDate.Text + "','" + danwei + "')";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                        else
                        {
                            string kc_sl = dtkc.Rows[0]["KC_SL"].ToString();
                            string kc_zj = dtkc.Rows[0]["KC_ZJ"].ToString();
                            double a = CommonFun.ComTryDouble(sl);
                            double b = CommonFun.ComTryDouble(kc_sl);
                            double c = a + b;
                            double d = CommonFun.ComTryDouble(zj);
                            double e = CommonFun.ComTryDouble(kc_zj);
                            double f = d + e;

                            string sqltext = "update OM_CARKUCUN set KC_SL='" + c.ToString() + "' ,KC_ZJ='" + f.ToString() + "' where KC_MC='" + mc + "' and KC_GG='" + gg + "'and KC_DJ='" + dj + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                    }
                }
            }

            if (rblSPR1_JL.SelectedValue == "n" || rblSPR2_JL.SelectedValue == "n")
            {
                sql = string.Format("update OM_CARRK_SP set SPZT='n' where ZDR_SJ='{0}'", id);
                list.Add(sql);
            }
            return list;
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string sql = string.Format("update OM_SP set SPJB='{0}',ZDR_JY='{1}',SPR1='{2}',SPR1_ID='{3}',SPR2='{4}',SPR2_ID='{5}',SPZT='{6}' where ZDR_SJ='{7}'", rblSPJB.SelectedValue, txtZDR.Text.Trim(), txtSPR1.Text.Trim(), hidSPR1_ID.Value, txtSPR2.Text.Trim(), hidSPR2_ID.Value, "0", lbZDR_SJ.Text);
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch
            {
                Response.Write("alert('提交审批的语句出现问题，请联系管理员！！！')");
                return;
            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
