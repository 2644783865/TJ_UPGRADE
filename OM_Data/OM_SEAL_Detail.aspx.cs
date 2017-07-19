using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Date
{
    public partial class OM_SEAL_Detail : System.Web.UI.Page
    {
        string id = "";
        string state = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"];
                }
                if (Request.QueryString["action"] == "add")
                {
                    Tab_spxx.Visible = false;
                    sqr.Text = Session["UserName"].ToString();
                    sqrq.Text = DateTime.Now.ToString();
                }
                else if (Request.QueryString["action"] == "view")
                {
                    bind_info();
                    txtCode.Enabled = false;
                    txtDep.Enabled = false;
                    txtMan.Enabled = false;
                    txtFile.Enabled = false;
                    rbl_Type.Enabled = false;
                    txtNum.Enabled = false;
                    txtNote.Enabled = false;
                    hlSelect0.Visible = false;
                    Tab_spxx.Visible = false;
                    txtshr.Enabled = false;
                }
                else if (Request.QueryString["action"] == "mod")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改后将重新进入审批！');", true);
                    bind_info();
                    sqrq.Text = DateTime.Now.ToString();
                    txtCode.Enabled = false;
                    Tab_spxx.Visible = false;
                }
                else
                {
                    bind_info();
                    txtCode.Enabled = false;
                    txtDep.Enabled = false;
                    txtMan.Enabled = false;
                    txtFile.Enabled = false;
                    rbl_Type.Enabled = false;
                    txtNum.Enabled = false;
                    txtNote.Enabled = false;
                    hlSelect0.Visible = false;
                    txtshr.Enabled = false;
                }
            }
        }

        public void bind_info()
        {
            string sqltext = "select ID,CODE,SM_DEPM,SM_MANUSE,SM_FNAME,SM_TYPE,SM_AMOUNT,SM_NOTE,SM_MANAPL,SM_MANAPR,SM_TIME from TBOM_SEAL where CODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {
                txtCode.Text = dr["CODE"].ToString();
                txtDep.Text = dr["SM_DEPM"].ToString();
                txtMan.Text = dr["SM_MANUSE"].ToString();
                txtFile.Text = dr["SM_FNAME"].ToString();
                rbl_Type.SelectedValue = dr["SM_TYPE"].ToString();
                txtNum.Text = dr["SM_AMOUNT"].ToString();
                sqr.Text = dr["SM_MANAPL"].ToString();
                txtNote.Text = dr["SM_NOTE"].ToString();
                sqrq.Text = dr["SM_TIME"].ToString();
                txtshr.Text = dr["SM_MANAPR"].ToString();
            }
            dr.Close();
            string sqltext1 = "select ID,SC_CHECK1,SC_CHECKCON1,SC_CHECKTIME1,SC_CHECK2,SC_CHECKCON2,SC_CHECKTIME2,SC_STATE from TBOM_SEALCHECK  where CODE='" + id + "'";
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqltext1);
            if (dr1.Read())
            {
                txt_first.Text = dr1["SC_CHECK1"].ToString();
                first_opinion.Text = dr1["SC_CHECKCON1"].ToString();
                first_time.Text = dr1["SC_CHECKTIME1"].ToString();
                txt_second.Text = dr1["SC_CHECK2"].ToString();
                second_opinion.Text = dr1["SC_CHECKCON2"].ToString();
                second_time.Text = dr1["SC_CHECKTIME2"].ToString();
                lblState.Text= dr1["SC_STATE"].ToString();
                state = lblState.Text;
                switch (state)
                {
                    case "1":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = -1;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;
                        if (Session["UserName"].ToString() == txt_first.Text)
                        {
                            txt_second.Enabled = false;
                            hlSelect2.Visible = false;
                            rblsecond.Enabled = false;
                            second_opinion.Enabled = false;
                        }
                        second_time.Text = DateTime.Now.ToString();
                        break;
                    case "2"://
                        rblfirst.SelectedIndex = 1;
                        rblsecond.SelectedIndex = -1;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;
                        second_time.Text = DateTime.Now.ToString();
                        break;
                    case "3"://
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 0;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;
                        break;
                    case "4":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = -1;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;
                        break;
                    default:
                        rblfirst.SelectedIndex = -1;
                        rblsecond.SelectedIndex = -1;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        txt_second.Enabled = true;
                        hlSelect2.Visible = true;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;
                        first_time.Text = DateTime.Now.ToString();
                        break;
                }
            }
            dr1.Close();
        }

        protected void rbl_Type_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLoad_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            List<string> list_sql = new List<string>();
            if (Request.QueryString["action"] == "add")
            {
                int Num = Convert.ToInt16(txtNum.Text.Trim());
                sqltext = "insert into TBOM_SEAL(CODE,SM_DEPM,SM_MANUSE,SM_FNAME,SM_TYPE,SM_AMOUNT,SM_NOTE,SM_MANAPR,SM_MANAPL,SM_TIME,SM_TCSTATE)" +
                "Values('" + txtCode.Text.Trim() + "','" + txtDep.Text.Trim() + "','" + txtMan.Text.Trim() + "','" + txtFile.Text.Trim() + "','" + rbl_Type.SelectedValue + "'," + Num + " ,'" + txtNote.Text.Trim() + "','" + txtshr.Text.Trim() + "','" + sqr.Text.Trim() + "','" + sqrq.Text.Trim() + "',1)";
                list_sql.Add(sqltext);
                sqltext = "insert into TBOM_SEALCHECK(CODE,SC_APPLYER,SC_SQTIME,SC_STATE,SC_CHECK1) values('" + txtCode.Text.Trim() + "','" + sqr.Text.Trim() + "','" + sqrq.Text.Trim() + "',0,'" + txtshr.Text.Trim() + "')";
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('申请成功，已发送邮件并进入审批！');window.opener=null;window.open('','_self');window.close();window.returnValue='refresh'", true);
                return;
            }
            else if (Request.QueryString["action"] == "mod")
            {
                int Num = Convert.ToInt16(txtNum.Text.Trim());
                sqltext = "update TBOM_SEAL set SM_DEPM='" + txtDep.Text.Trim() + "',SM_MANUSE='" + txtMan.Text.Trim() + "',SM_FNAME='" + txtFile.Text.Trim() + "',SM_TYPE='" + rbl_Type.SelectedValue + "',SM_AMOUNT=" + Num + ",SM_MANAPR='" + txtshr.Text.Trim() + "',SM_MANAPL='" + sqr.Text.Trim() + "',SM_NOTE='" + txtNote.Text.Trim() + "',SM_TIME='" + sqrq.Text.Trim() + "',SM_TCSTATE=1 where CODE='" + txtCode.Text.Trim() + "'";
                list_sql.Add(sqltext);
                sqltext = "update TBOM_SEALCHECK set SC_SQTIME='" + sqrq.Text.Trim() + "',SC_STATE=0 where CODE='" + txtCode.Text.Trim() + "'";
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("OM_SEAL.aspx");
            }
            else if (Request.QueryString["action"] == "review")
            {
                if (lblState.Text== "1" & rblsecond.SelectedIndex == 0)
                {
                    sqltext = "update TBOM_SEALCHECK set SC_STATE=3,SC_CHECKTIME2='" + second_time.Text.Trim() + "',SC_CHECKCON2='" + second_opinion.Text.Trim() + "' where CODE='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                    sqltext = "update TBOM_SEAL set SM_TCSTATE=3 where CODE='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                }
                else if (lblState.Text== "1" & rblsecond.SelectedIndex == 1)
                {
                    sqltext = "update TBOM_SEALCHECK set SC_STATE=4,SC_CHECKTIME2='" + second_time.Text.Trim() + "',SC_CHECKCON2='" + second_opinion.Text.Trim() + "' where CODE='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                    sqltext = "update TBOM_SEAL set SM_TCSTATE=0 where CODE='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                }
                else if (lblState.Text== "0" & rblfirst.SelectedIndex == 0)
                {
                    sqltext = "update TBOM_SEALCHECK set SC_STATE=1,SC_CHECKTIME1='" + first_time.Text.Trim() + "',SC_CHECKCON1='" + first_opinion.Text.Trim() + "',SC_CHECK2='" + txt_second.Text.Trim() + "' where CODE='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                    sqltext = "update TBOM_SEAL set SM_TCSTATE=2 where CODE='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                }
                else if (lblState.Text== "0" & rblfirst.SelectedIndex == 1)
                {
                    sqltext = "update TBOM_SEALCHECK set SC_STATE=2,SC_CHECKTIME2='" + second_time.Text.Trim() + "',SC_CHECKCON2='" + second_opinion.Text.Trim() + "' where CODE='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                    sqltext = "update TBOM_SEAL set SM_TCSTATE=0 where CODE='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                }
                DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("OM_SEAL.aspx");
            }
        }

        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            Response.Redirect("OM_SEAL.aspx");
        }
    }
}
