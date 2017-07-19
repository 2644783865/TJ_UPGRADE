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
using System.Data.SqlClient;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Packing_List_Audit : System.Web.UI.Page
    {
        string sqlText = "";
        string pk_id = "";
        string pk_no = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        private void InitInfo()
        {
            btnsubmit.Text = "提 交";
            string PLT_NO = "";
            string status = "";
            pk_id = Request.QueryString["id"];
            pk_no = Request.QueryString["pk_audit_id"];
            if (pk_id != null)
            {
                PLT_NO = pk_id;
            }
            else
            {
                PLT_NO = pk_no;
            }
            sqlText = "select PLT_PACKLISTNO,PLT_CONTRACTOR,PLT_OWNER,PLT_SHIPNO,";
            sqlText += "PLT_DELIVERYDATE,PLT_SYSTEMNUM,PLT_GOODSNAME,PLT_SUBMITNM,";
            sqlText += "PLT_SUBMITTM,PLT_REVIEWANAME,PLT_REVIEWAADVC,PLT_REVIEWATIME,";
            sqlText += "PLT_REVIEWBNAME,PLT_REVIEWBADVC,PLT_REVIEWBTIME,PLT_REVIEWCNAME,";
            sqlText += "PLT_REVIEWCADVC,PLT_REVIEWCTIME,PLT_ADATE,PLT_STATE,PLT_PJNAME,";
            sqlText += "PLT_ENGNAME,PLT_ENGID from TBPM_PACKLISTTOTAL ";
            sqlText += "where PLT_PACKLISTNO='" + PLT_NO + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            #region
            if (dr.Read())
            {
                packlist_no.Value = dr[0].ToString();
                Gen_Contractor.Text = dr[1].ToString();
                Owner.Text = dr[2].ToString();
                Ship_No.Text = dr[3].ToString();
                Con_Date.Text = dr[4].ToString();
                Sys_No.Text = dr[5].ToString();
                Goods_Name.Text = dr[6].ToString();
                Signature.Text = dr[7].ToString();
                txt_editor.Text = dr[7].ToString();
                txt_plandate.Text = dr[8].ToString();
                txt_first.Text = dr[9].ToString();
                first_opinion.Text = dr[10].ToString();
                first_time.Text = dr[11].ToString();
                txt_second.Text = dr[12].ToString();
                second_opinion.Text = dr[13].ToString();
                second_time.Text = dr[14].ToString();
                txt_third.Text = dr[15].ToString();
                third_opinion.Text = dr[16].ToString();
                third_time.Text = dr[17].ToString();
                status = dr[19].ToString();
                lab_proname.Text = dr[20].ToString();
                lab_engname.Text = dr[21].ToString();
                tsa_id.Text = dr[22].ToString();
                if (Session["UserGroup"].ToString().Contains("技术员"))
                {
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                if (status == "2")
                {
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                if (status == "3")
                {
                    rblfirst.SelectedValue = "3";
                    btnsubmit.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                if (status == "5")
                {
                    rblfirst.SelectedValue = "4";
                    rblsecond.SelectedValue = "5";
                    btnsubmit.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                if (status == "7")
                {
                    rblfirst.SelectedValue = "4";
                    rblsecond.SelectedValue = "6";
                    rblthird.SelectedValue = "7";
                    btnsubmit.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                if (status == "4")
                {
                    rblfirst.SelectedValue = "4";
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                if (status == "6")
                {
                    rblfirst.SelectedValue = "4";
                    rblsecond.SelectedValue = "6";
                    btnsubmit.Text = "确 定";
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                }
                if (status == "8" || status == "9")
                {
                    rblfirst.SelectedValue = "4";
                    rblsecond.SelectedValue = "6";
                    rblthird.SelectedValue = "8";
                    ImageVerify.Visible = true;
                    btnsubmit.Text = "确 定";
                    btnsubmit.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
            }
            dr.Close();
            #endregion
            if (status == "0")
            {
                sqlText = "select * from TBPM_PACKINGLIST ";
                sqlText += "where PL_PACKLISTNO='" + PLT_NO + "' and PL_STATE='0'";
            }
            else
            {
                sqlText = "select * from TBPM_PACKINGLIST ";
                sqlText += "where PL_PACKLISTNO='" + PLT_NO + "' and PL_STATE!='0'";
            }
            DBCallCommon.BindGridView(GridView1, sqlText);
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (txt_first.Text == "")
            {
                Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                return;
            }
            if (rblfirst.SelectedValue == "")
            {
                sqlText = "update TBPM_PACKINGLIST set PL_STATE='2' ";
                sqlText += "where PL_PACKLISTNO='" + packlist_no.Value + "' and PL_STATE='1'";
                DBCallCommon.ExeSqlText(sqlText);
            }
            //string TC_NAME = "";
            //string TC_CODE = "";
            if (txt_first.Text != "" && txt_second.Text == "" && txt_third.Text == "")
            {
                //TC_NAME = txt_first.Text;
                hlSelect2.Enabled = false;
                second_opinion.Enabled = false;
                rblsecond.Enabled = false;
                hlSelect3.Enabled = false;
                third_opinion.Enabled = false;
                rblthird.Enabled = false;
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text == "")
            {
                //TC_NAME = txt_second.Text;
                second_opinion.Enabled = false;
                rblsecond.Enabled = false;
                hlSelect3.Enabled = false;
                third_opinion.Enabled = false;
                rblthird.Enabled = false;
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text != "")
            {
                //TC_NAME = txt_third.Text;
                btnsubmit.Text = "确 定";
            }
            //sqlText = "select ST_CODE from TBDS_STAFFINFO ";
            //sqlText += "where ST_NAME='" + TC_NAME + "'";
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            //while (dr.Read())
            //{
            //    TC_CODE = dr[0].ToString();
            //}
            //dr.Close();
            if (txt_first.Text != "" && txt_second.Text == "" && txt_third.Text == "")
            {
                if (rblfirst.SelectedValue == "")
                {
                    sqlText = "update TBPM_PACKLISTTOTAL set PLT_SUBMITTM='" + DateTime.Now.ToString() + "',";
                    sqlText += "PLT_REVIEWANAME='" + txt_first.Text + "',PLT_REVIEWA='" + firstid.Value + "',";
                    sqlText += "PLT_STATE='2' where PLT_PACKLISTNO='" + packlist_no.Value + "' and PLT_STATE='1'";
                }
                if (rblfirst.SelectedValue == "3")
                {
                    sqlText = "update TBPM_PACKLISTTOTAL set ";
                    sqlText += "PLT_REVIEWATIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "PLT_REVIEWAADVC='" + first_opinion.Text + "',PLT_STATE='" + rblfirst.SelectedValue + "' ";
                    sqlText += "where PLT_PACKLISTNO='" + packlist_no.Value + "' and PLT_STATE='2'";
                }
                else if (rblfirst.SelectedValue == "4")
                {
                    Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                    return;
                }
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text == "")
            {
                if (rblfirst.SelectedValue == "4" && rblsecond.SelectedValue == "")
                {
                    string strsql = "update TBPM_PACKINGLIST set PL_STATE='3' ";
                    strsql += "where PL_PACKLISTNO='" + packlist_no.Value + "' and PL_STATE='2'";
                    DBCallCommon.ExeSqlText(strsql);
                    sqlText = "update TBPM_PACKLISTTOTAL set ";
                    sqlText += "PLT_REVIEWBNAME='" + txt_second.Text + "',";
                    sqlText += "PLT_REVIEWB='" + secondid.Value + "',PLT_REVIEWATIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "PLT_REVIEWAADVC='" + first_opinion.Text + "',PLT_STATE='" + rblfirst.SelectedValue + "' ";
                    sqlText += "where PLT_PACKLISTNO='" + packlist_no.Value + "' and PLT_STATE='2'";
                }
                if (rblsecond.SelectedValue == "5")
                {
                    sqlText = "update TBPM_PACKLISTTOTAL set ";
                    sqlText += "PLT_REVIEWBTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "PLT_REVIEWBADVC='" + second_opinion.Text + "',PLT_STATE='" + rblsecond.SelectedValue + "' ";
                    sqlText += "where PLT_PACKLISTNO='" + packlist_no.Value + "' and PLT_STATE='4'";
                }
                else if (rblsecond.SelectedValue == "6")
                {
                    Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                    return;
                }
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text != "")
            {
                if (rblsecond.SelectedValue == "6" && rblthird.SelectedValue == "")
                {
                    sqlText = "update TBPM_PACKLISTTOTAL set ";
                    sqlText += "PLT_REVIEWCNAME='" + txt_third.Text + "',";
                    sqlText += "PLT_REVIEWC='" + thirdid.Value + "',PLT_REVIEWBTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "PLT_REVIEWBADVC='" + second_opinion.Text + "',PLT_STATE='" + rblsecond.SelectedValue + "' ";
                    sqlText += "where PLT_PACKLISTNO='" + packlist_no.Value + "' and PLT_STATE='4'";
                }
                else if (rblthird.SelectedValue == "8")
                {
                    sqlText = "update TBPM_TCTSENROLL set TSA_ZXSHEET='" + DateTime.Now.ToString() + "' ";
                    sqlText += "where TSA_NO=(select top 1 TSA_NO from TBPM_TCTSENROLL ";
                    sqlText += "where TSA_ID='" + tsa_id.Text + "' and TSA_ZXSHEET is null) and ";
                    sqlText += "TSA_ID='" + tsa_id.Text + "' and TSA_ZXSHEET is null";
                    DBCallCommon.ExeSqlText(sqlText);
                    //装箱单审核通过后任务完工
                    sqlText = "update TBPM_TCTSASSGN set TSA_STATE='2' ";
                    sqlText += "where TSA_ID='" + tsa_id.Text + "'";
                    sqlText = "update TBPM_PACKLISTTOTAL set PLT_ADATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "PLT_REVIEWCTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "PLT_REVIEWCADVC='" + third_opinion.Text + "',PLT_STATE='" + rblthird.SelectedValue + "' ";
                    sqlText += "where PLT_PACKLISTNO='" + packlist_no.Value + "' and PLT_STATE='6'";
                }
                else if (rblthird.SelectedValue == "7")
                {
                    sqlText = "update TBPM_PACKLISTTOTAL set PLT_REVIEWCTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "PLT_REVIEWCADVC='" + third_opinion.Text + "',PLT_STATE='" + rblthird.SelectedValue + "' ";
                    sqlText += "where PLT_PACKLISTNO='" + packlist_no.Value + "' and PLT_STATE='6'";
                }
            }
            DBCallCommon.ExeSqlText(sqlText);
            if (rblthird.SelectedValue == "8")
            {
                btnsubmit.Enabled = false;
                sqlText = "update TBPM_PACKINGLIST set PL_STATE='5' ";
                sqlText += "where PL_PACKLISTNO='" + packlist_no.Value + "' and PL_STATE='3'";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('装箱单审核通过!');location.href='TM_Leader_Task.aspx';</script>");
            }
            else if (rblfirst.SelectedValue == "3" || rblsecond.SelectedValue == "5" || rblthird.SelectedValue == "7")
            {
                btnsubmit.Enabled = false;
                sqlText = "update TBPM_PACKINGLIST set PL_STATE='4' ";
                sqlText += "where PL_PACKLISTNO='" + packlist_no.Value + "' and PL_STATE in ('2','3')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('装箱单驳回，审核终止!');location.href='TM_Leader_Task.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('装箱单已提交，请等待审核...');location.href='TM_Leader_Task.aspx';</script>");
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
