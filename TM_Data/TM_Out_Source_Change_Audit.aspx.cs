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
    public partial class TM_Out_Source_Change_Audit : System.Web.UI.Page
    {
        string sqlText = "";
        string change_id = "";
        string audit_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        //初始化页面
        private void InitInfo()
        {
            btnsubmit.Text = "提 交";
            change_id = Request.QueryString["id"];
            audit_id = Request.QueryString["ost_change_id"];
            if (change_id != null)
            {
                out_no.Text = change_id;
            }
            else
            {
                out_no.Text = audit_id;
            }
            sqlText = "select OST_PJNAME,OST_ENGNAME,OST_SUBMITERNM,OST_MDATE,";
            sqlText += "OST_REVIEWERANM,OST_REVIEWAADVC,OST_REVIEWADATE,";
            sqlText += "OST_REVIEWERBNM,OST_REVIEWBADVC,OST_REVIEWBDATE,";
            sqlText += "OST_REVIEWERCNM,OST_REVIEWCADVC,OST_REVIEWCDATE,";
            sqlText += "OST_ADATE,OST_STATE,OST_PJID,OST_ENGID ";
            sqlText += "from TBPM_OUTSCHANGERVW where OST_CHANGECODE='" + out_no.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            while (dr.Read())
            {
                #region
                lab_proname.Text = dr[0].ToString();
                proname.Text = dr[0].ToString();
                lab_engname.Text = dr[1].ToString();
                engname.Text = dr[1].ToString();
                txt_editor.Text = dr[2].ToString();
                txt_plandate.Text = dr[3].ToString();
                plandate.Text = dr[3].ToString();
                txt_first.Text = dr[4].ToString();
                first_opinion.Text = dr[5].ToString();
                first_time.Text = dr[6].ToString();
                txt_second.Text = dr[7].ToString();
                second_opinion.Text = dr[8].ToString();
                second_time.Text = dr[9].ToString();
                txt_third.Text = dr[10].ToString();
                third_opinion.Text = dr[11].ToString();
                third_time.Text = dr[12].ToString();
                txt_approval.Text = dr[13].ToString();
                status.Value = dr[14].ToString();
                pro_id.Value = dr[15].ToString();
                tsa_id.Text = dr[16].ToString();
                #endregion
                if (int.Parse(status.Value) < 2)
                {
                    btnsave.Visible = true;
                }
                #region
                if (status.Value == "3")//一级审核驳回
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
                if (status.Value == "5")//二级审核驳回
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
                if (status.Value == "7")//三级审核驳回
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
                if (status.Value == "4")//一级审核通过
                {
                    rblfirst.SelectedValue = "4";
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                }
                if (status.Value == "6")//二级审核通过
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
                if (status.Value == "8" || status.Value == "9")//三级审核通过
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
                #endregion
            }
            dr.Close();
            sqlText = "select * from TBPM_OUTSCHANGE where OSL_CODE='" + out_no.Text + "' and OSL_STATE='0'";
            DBCallCommon.BindGridView(GridView2, sqlText);
            if (status.Value == "0" || status.Value == "1")
            {
                Panel4.Visible = false;
                sqlText = "select * from TBPM_OUTSCHANGE where OSL_CODE='" + out_no.Text + "' and OSL_STATE='" + status.Value + "'";
                DBCallCommon.BindGridView(GridView1, sqlText);
            }
            else
            {
                Panel1.Visible = false;
                sqlText = "select * from TBPM_OUTSCHANGE where OSL_CODE='" + out_no.Text + "' and OSL_STATE in ('2','3','4','5')";
                DBCallCommon.BindGridView(GridView3, sqlText);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            #region
            string out_id = "";
            string lab_name = "";
            string lab_biaozhi = "";
            string lab_guige = "";
            string lab_caizhi = "";
            string lab_unitwght = "";
            string lab_num = "";
            string lab_totalwght = "";
            string lab_wdepname = "";
            string lab_process = "";
            string lab_date = "";
            string lab_place = "";
            //string out_state = "";
            #endregion
            #region
            //sqlText = "select max(OSL_STATE) from TBPM_OUTSCHANGE where OSL_CODE='" + out_no.Text + "'";
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            //while (dr.Read())
            //{
            //    out_state = dr[0].ToString();
            //}
            //dr.Close();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                out_id = ((Label)gr.FindControl("out_id")).Text;
                lab_name = ((TextBox)gr.FindControl("lab_name")).Text;
                lab_biaozhi = ((TextBox)gr.FindControl("lab_biaozhi")).Text;
                lab_guige = ((TextBox)gr.FindControl("lab_guige")).Text;
                lab_caizhi = ((TextBox)gr.FindControl("lab_caizhi")).Text;
                lab_unitwght = ((TextBox)gr.FindControl("lab_unitwght")).Text;
                lab_num = ((TextBox)gr.FindControl("lab_num")).Text;
                lab_totalwght = ((TextBox)gr.FindControl("lab_totalwght")).Text;
                lab_wdepname = ((TextBox)gr.FindControl("lab_wdepname")).Text;
                lab_process = ((TextBox)gr.FindControl("lab_process")).Text;
                lab_date = ((TextBox)gr.FindControl("lab_date")).Text;
                lab_place = ((TextBox)gr.FindControl("lab_place")).Text;
                if (status.Value == "0")
                {
                    sqlText = "update TBPM_OUTSCHANGERVW set OST_STATE='1' ";
                    sqlText += "where OST_CHANGECODE='" + out_no.Text + "' and OST_STATE='0'";
                    DBCallCommon.ExeSqlText(sqlText);
                    sqlText = "insert into TBPM_OUTSCHANGE (OSL_CODE,OSL_NAME,OSL_BIAOSHINO,OSL_GUIGE,";
                    sqlText += "OSL_CAIZHI,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPNAME,";
                    sqlText += "OSL_REQUEST,OSL_REQDATE,OSL_DELSITE,OSL_STATE)";
                    sqlText += " values ('" + out_no.Text + "','" + lab_name + "','" + lab_biaozhi + "','" + lab_guige + "',";
                    sqlText += "'" + lab_caizhi + "','" + lab_unitwght + "','" + lab_num + "',";
                    sqlText += "'" + lab_totalwght + "','" + lab_wdepname + "','" + lab_process + "','" + lab_date + "',";
                    sqlText += "'" + lab_place + "','1')";
                }
                else
                {
                    sqlText = "update TBPM_OUTSCHANGE set OSL_NAME='" + lab_name + "',OSL_BIAOSHINO='" + lab_biaozhi + "',";
                    sqlText += "OSL_GUIGE='" + lab_guige + "',OSL_CAIZHI='" + lab_caizhi + "',";
                    sqlText += "OSL_UNITWGHT='" + lab_unitwght + "',OSL_NUMBER='" + lab_num + "',";
                    sqlText += "OSL_TOTALWGHTL='" + lab_totalwght + "',OSL_WDEPNAME='" + lab_wdepname + "',";
                    sqlText += "OSL_REQUEST='" + lab_process + "',OSL_REQDATE='" + lab_date + "',";
                    sqlText += "OSL_DELSITE='" + lab_place + "' where OSL_ID='" + out_id + "'";
                }
                DBCallCommon.ExeSqlText(sqlText);
            }
            #endregion
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (txt_first.Text == "")
            {
                Response.Write("<script language=javascript>alert('提示:请选择下一审核人！');history.go(-1);</script>");
                return;
            }
            if (txt_first.Text != "" && rblfirst.SelectedValue == "")
            {
                sqlText = "update TBPM_OUTSCHANGE set OSL_STATE='2' ";
                sqlText += "where OSL_CODE='" + out_no.Text + "' and OSL_STATE='1'";
                DBCallCommon.ExeSqlText(sqlText);
            }
            string TC_NAME = "";
            string TC_CODE = "";
            if (txt_first.Text != "" && txt_second.Text == "" && txt_third.Text == "")
            {
                TC_NAME = txt_first.Text;
                hlSelect2.Enabled = false;
                second_opinion.Enabled = false;
                rblsecond.Enabled = false;
                hlSelect3.Enabled = false;
                third_opinion.Enabled = false;
                rblthird.Enabled = false;
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text == "")
            {
                TC_NAME = txt_second.Text;
                second_opinion.Enabled = false;
                rblsecond.Enabled = false;
                hlSelect3.Enabled = false;
                third_opinion.Enabled = false;
                rblthird.Enabled = false;
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text != "")
            {
                TC_NAME = txt_third.Text;
                btnsubmit.Text = "确 定";
            }
            sqlText = "select ST_CODE from TBDS_STAFFINFO ";
            sqlText += "where ST_NAME='" + TC_NAME + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            while (dr.Read())
            {
                TC_CODE = dr[0].ToString();
            }
            dr.Close();
            if (txt_first.Text != "" && txt_second.Text == "" && txt_third.Text == "")
            {
                if (rblfirst.SelectedValue == "")
                {
                    sqlText = "update TBPM_OUTSCHANGERVW set OST_MDATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "OST_REVIEWERANM='" + TC_NAME + "',OST_REVIEWERA='" + TC_CODE + "',OST_STATE='2' ";
                    sqlText += "where OST_CHANGECODE='" + out_no.Text + "' and OST_STATE='1'";
                }
                if (rblfirst.SelectedValue == "3")
                {
                    sqlText = "update TBPM_OUTSCHANGERVW set ";
                    sqlText += "OST_REVIEWERATIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "OST_REVIEWAADVC='" + first_opinion.Text + "',OST_STATE='" + rblfirst.SelectedValue + "' ";
                    sqlText += "where OST_CHANGECODE='" + out_no.Text + "' and OST_STATE='2'";
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
                    sqlText = "update TBPM_OUTSCHANGE set OSL_STATE='3' ";
                    sqlText += "where OSL_CODE='" + out_no.Text + "' and OSL_STATE='2' ";
                    DBCallCommon.ExeSqlText(sqlText);
                    sqlText = "update TBPM_OUTSCHANGERVW set OST_REVIEWERBNM='" + TC_NAME + "',";
                    sqlText += "OST_REVIEWERB='" + TC_CODE + "',OST_REVIEWADATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "OST_REVIEWAADVC='" + first_opinion.Text + "',OST_STATE='" + rblfirst.SelectedValue + "' ";
                    sqlText += "where OST_CHANGECODE='" + out_no.Text + "' and OST_STATE='2'";
                }
                if (rblsecond.SelectedValue == "5")
                {
                    sqlText = "update TBPM_OUTSCHANGERVW set OST_REVIEWBDATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "OST_REVIEWBADVC='" + second_opinion.Text + "',OST_STATE='" + rblsecond.SelectedValue + "' ";
                    sqlText += "where OST_CHANGECODE='" + out_no.Text + "' and OST_STATE='4'";
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
                    sqlText = "update TBPM_OUTSCHANGERVW set OST_REVIEWERCNM='" + TC_NAME + "',";
                    sqlText += "OST_REVIEWERC='" + TC_CODE + "',OST_REVIEWBDATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "OST_REVIEWBADVC='" + second_opinion.Text + "',OST_STATE='" + rblsecond.SelectedValue + "' ";
                    sqlText += "where OST_CHANGECODE='" + out_no.Text + "' and OST_STATE='4'";
                }
                if (rblthird.SelectedValue == "8")
                {
                    sqlText = "update TBPM_OUTSCHANGERVW set OST_ADATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "OST_REVIEWCDATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "OST_REVIEWCADVC='" + third_opinion.Text + "',OST_STATE='" + rblthird.SelectedValue + "' ";
                    sqlText += "where OST_CHANGECODE='" + out_no.Text + "' and OST_STATE='6'";
                }
                else if (rblthird.SelectedValue == "7")
                {
                    sqlText = "update TBPM_OUTSCHANGERVW set OST_REVIEWCDATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "OST_REVIEWCADVC='" + third_opinion.Text + "',OST_STATE='" + rblthird.SelectedValue + "' ";
                    sqlText += "where OST_CHANGECODE='" + out_no.Text + "' and OST_STATE='6'";
                }
            }
            DBCallCommon.ExeSqlText(sqlText);
            if (rblthird.SelectedValue == "8")
            {
                btnsubmit.Enabled = false;
                sqlText = "update TBPM_OUTSCHANGE set OSL_STATE='5' ";
                sqlText += "where OSL_CODE='" + out_no.Text + "' and OSL_STATE='3'";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('技术外协变更审核通过!');location.href='TM_Leader_Task_Change.aspx';</script>");
            }
            if (rblfirst.SelectedValue == "3" || rblsecond.SelectedValue == "5" || rblthird.SelectedValue == "7")
            {
                btnsubmit.Enabled = false;
                sqlText = "update TBPM_OUTSCHANGE set OSL_STATE='4' ";
                sqlText += "where OSL_CODE='" + out_no.Text + "' and OSL_STATE in ('2','3')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('技术外协变更驳回，审核终止!');location.href='TM_Leader_Task_Change.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('技术外协变更审核已提交，请等待审核...');location.href='TM_Leader_Task_Change.aspx';</script>");
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
