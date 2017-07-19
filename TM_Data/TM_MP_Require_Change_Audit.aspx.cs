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
    public partial class TM_MP_Require_Change_Audit : System.Web.UI.Page
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
            audit_id = Request.QueryString["mp_change_id"];
            if (change_id != null)
            {
                mp_no.Text = change_id;
            }
            else
            {
                mp_no.Text = audit_id;
            }
            sqlText = "select MP_PJNAME,MP_ENGNAME,MP_SUBMITNM,MP_SUBMITTM,";
            sqlText += "MP_REVIEWANAME,MP_REVIEWAADVC,MP_REVIEWATIME,";
            sqlText += "MP_REVIEWBNAME,MP_REVIEWBADVC,MP_REVIEWBTIME,";
            sqlText += "MP_REVIEWCNAME,MP_REVIEWCADVC,MP_REVIEWCTIME,";
            sqlText += "MP_ADATE,MP_STATE,MP_PJID,MP_ENGID ";
            sqlText += "from TBPM_MPCHANGERVW where MP_CODE='" + mp_no.Text + "'";
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
                pro_id.Value=dr[15].ToString();
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
            sqlText = "select * from TBPM_MPCHANGE where MP_PID='" + mp_no.Text + "' and MP_STATE='0'";
            DBCallCommon.BindGridView(GridView2,sqlText);
            if (status.Value == "0" || status.Value == "1")
            {
                panelid.Value = "1";
                Panel4.Visible = false;
                sqlText = "select * from TBPM_MPCHANGE where MP_PID='" + mp_no.Text + "' and MP_STATE='" + status.Value + "'";
                DBCallCommon.BindGridView(GridView1, sqlText);
            }
            else
            {
                panelid.Value = "0";
                Panel1.Visible = false;
                sqlText = "select * from TBPM_MPCHANGE where MP_PID='" + mp_no.Text + "' and MP_STATE in ('2','3','4','5')";
                DBCallCommon.BindGridView(GridView3, sqlText);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            #region
            string mp_id = "";
            string lab_ID = "";
            string txt_name = "";
            string txt_zongxu = "";
            //string txt_hrcode = "";
            string txt_guige = "";
            string txt_caizhi = "";
            string txt_unit = "";
            string txt_num = "";
            string txt_use = "";
            string txt_type = "";
            string txt_time = "";
            string txt_influence = "";
            string txt_note = "";
            string mp_state = "";
            #endregion
            #region
            sqlText = "select max(MP_STATE) from TBPM_MPCHANGE where MP_PID='" + mp_no.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            while (dr.Read())
            {
                mp_state = dr[0].ToString();
            }
            dr.Close();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr=GridView1.Rows[i];
                mp_id = ((Label)gr.FindControl("mp_id")).Text;
                lab_ID = ((TextBox)gr.FindControl("lab_ID")).Text;
                txt_name = ((TextBox)gr.FindControl("txt_name")).Text;
                txt_zongxu = gr.Cells[2].Text.Trim();
                txt_guige = ((TextBox)gr.FindControl("txt_guige")).Text;
                txt_caizhi = ((TextBox)gr.FindControl("txt_caizhi")).Text;
                //txt_hrcode = txt_name + '_' + txt_guige + '_' + txt_caizhi;
                txt_unit = ((TextBox)gr.FindControl("txt_unit")).Text;
                txt_num = ((TextBox)gr.FindControl("txt_num")).Text;
                txt_use = ((TextBox)gr.FindControl("txt_use")).Text;
                txt_type = ((TextBox)gr.FindControl("txt_type")).Text;
                txt_time = ((TextBox)gr.FindControl("txt_time")).Text;
                txt_influence = ((TextBox)gr.FindControl("txt_influence")).Text;
                txt_note = ((TextBox)gr.FindControl("txt_note")).Text;
                if (mp_state == "0")
                {
                    sqlText = "insert into TBPM_MPCHANGE (MP_PID,MP_ZONGXU,MP_MARID,MP_NAME,MP_PJID,";
                    sqlText += "MP_PJNAME,MP_ENGID,MP_ENGNAME,MP_GUIGE,MP_CAIZHI,MP_UNIT,MP_NUMBER,";
                    sqlText += "MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_NOTE,MP_STATE)";
                    sqlText += " values ('" + mp_no.Text + "','"+txt_zongxu+"','" + lab_ID + "','" + txt_name + "','" + pro_id.Value + "',";
                    sqlText += "'" + lab_proname.Text + "','" + tsa_id.Text + "','" + lab_engname.Text + "','" + txt_guige + "',";
                    sqlText += "'" + txt_caizhi + "','" + txt_unit + "','" + txt_num + "','" + txt_use + "','" + txt_type + "',";
                    sqlText += "'" + txt_time + "','" + txt_influence + "','" + txt_note + "','1')";
                    DBCallCommon.ExeSqlText(sqlText);
                }
                else
                {
                    sqlText = "update TBPM_MPCHANGE set MP_MARID='" + lab_ID + "',MP_NAME='" + txt_name + "',";
                    sqlText += "MP_GUIGE='" + txt_guige + "',MP_CAIZHI='" + txt_caizhi + "',";
                    sqlText += "MP_UNIT='" + txt_unit + "',MP_NUMBER='" + txt_num + "',MP_USAGE='" + txt_use + "',";
                    sqlText += "MP_TYPE='" + txt_type + "',MP_TIMERQ='" + txt_time + "',MP_ENVREFFCT='" + txt_influence + "',";
                    sqlText += "MP_NOTE='" + txt_note + "' where MP_ID='" + mp_id + "'";
                    DBCallCommon.ExeSqlText(sqlText);
                }
            }
            #endregion
            if (mp_state == "0")
            {
                sqlText = "update TBPM_MPCHANGERVW set MP_STATE='1' ";
                sqlText += "where MP_CODE='" + mp_no.Text + "' and MP_STATE='0'";
                DBCallCommon.ExeSqlText(sqlText);
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (txt_first.Text == "")
            {
                Response.Write("<script language=javascript>alert('提示:请选择下一审核人！');history.go(-1);</script>");
                return;
            }
            if (txt_first.Text!=""&&rblfirst.SelectedValue == "")
            {
                sqlText = "update TBPM_MPCHANGE set MP_STATE='2' ";
                sqlText += "where MP_PID='" + mp_no.Text + "' and MP_STATE='1'";
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
                    sqlText = "update TBPM_MPCHANGERVW set MP_SUBMITTM='" + DateTime.Now.ToString() + "',";
                    sqlText += "MP_REVIEWANAME='" + TC_NAME + "',MP_REVIEWA='" + TC_CODE + "',MP_STATE='2' ";
                    sqlText += "where MP_CODE='" + mp_no.Text + "' and MP_STATE='1'";
                }
                if (rblfirst.SelectedValue == "3")
                {
                    sqlText = "update TBPM_MPCHANGERVW set ";
                    sqlText += "MP_REVIEWATIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MP_REVIEWAADVC='" + first_opinion.Text + "',MP_STATE='" + rblfirst.SelectedValue + "' ";
                    sqlText += "where MP_CODE='" + mp_no.Text + "' and MP_STATE='2'";
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
                    sqlText = "update TBPM_MPCHANGE set MP_STATE='3' ";
                    sqlText += "where MP_PID='" + mp_no.Text + "' and MP_STATE='2'";
                    DBCallCommon.ExeSqlText(sqlText);
                    sqlText = "update TBPM_MPCHANGERVW set MP_REVIEWBNAME='" + TC_NAME + "',";
                    sqlText += "MP_REVIEWB='" + TC_CODE + "',MP_REVIEWATIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MP_REVIEWAADVC='" + first_opinion.Text + "',MP_STATE='" + rblfirst.SelectedValue + "' ";
                    sqlText += "where MP_CODE='" + mp_no.Text + "' and MP_STATE='2'";
                }
                if (rblsecond.SelectedValue == "5")
                {
                    sqlText = "update TBPM_MPCHANGERVW set MP_REVIEWBTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MP_REVIEWBADVC='" + second_opinion.Text + "',MP_STATE='" + rblsecond.SelectedValue + "' ";
                    sqlText += "where MP_CODE='" + mp_no.Text + "' and MP_STATE='4'";
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
                    sqlText = "update TBPM_MPCHANGERVW set MP_REVIEWCNAME='" + TC_NAME + "',";
                    sqlText += "MP_REVIEWC='" + TC_CODE + "',MP_REVIEWBTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MP_REVIEWBADVC='" + second_opinion.Text + "',MP_STATE='" + rblsecond.SelectedValue + "' ";
                    sqlText += "where MP_CODE='" + mp_no.Text + "' and MP_STATE='4'";
                }
                if (rblthird.SelectedValue == "8")
                {
                    sqlText = "update TBPM_MPCHANGERVW set MP_ADATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "MP_REVIEWCTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MP_REVIEWCADVC='" + third_opinion.Text + "',MP_STATE='" + rblthird.SelectedValue + "' ";
                    sqlText += "where MP_CODE='" + mp_no.Text + "' and MP_STATE='6'";
                }
                else if (rblthird.SelectedValue == "7")
                {
                    sqlText = "update TBPM_MPCHANGERVW set MP_REVIEWCTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MP_REVIEWCADVC='" + third_opinion.Text + "',MP_STATE='" + rblthird.SelectedValue + "' ";
                    sqlText += "where MP_CODE='" + mp_no.Text + "' and MP_STATE='6'";
                }
            }
            DBCallCommon.ExeSqlText(sqlText);
            if (rblthird.SelectedValue == "8")
            {
                btnsubmit.Enabled = false;
                sqlText = "update TBPM_MPCHANGE set MP_STATE='5' ";
                sqlText += "where MP_PID='" + mp_no.Text + "' and MP_STATE='3'";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('材料计划变更审核通过!');location.href='TM_Leader_Task_Change.aspx';</script>");
            }
            if (rblfirst.SelectedValue == "3" || rblsecond.SelectedValue == "5" || rblthird.SelectedValue == "7")
            {
                btnsubmit.Enabled = false;
                sqlText = "update TBPM_MPCHANGE set MP_STATE='4' ";
                sqlText += "where MP_PID='" + mp_no.Text + "' and MP_STATE in ('2','3')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('材料计划变更驳回，审核终止!');location.href='TM_Leader_Task_Change.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('材料计划变更审核已提交，请等待审核...');location.href='TM_Leader_Task_Change.aspx';</script>");
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
        private double sum2 = 0;
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sum2 += Convert.ToDouble(e.Row.Cells[7].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "合计：";
                e.Row.Cells[6].Text = "kg";
                e.Row.Cells[7].Text = sum2.ToString();
            }
        }
        //private double sum1 = 0;
        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowIndex >= 0)
        //    {
        //        sum1 += Convert.ToDouble(((TextBox)e.Row.FindControl("txt_num")).Text);
        //    }
        //    else if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        e.Row.Cells[5].Text = "合计：";
        //        e.Row.Cells[6].Text = "kg";
        //        e.Row.Cells[7].Text = sum1.ToString();
        //    }
        //}
        private double sum3 = 0;
        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sum3 += Convert.ToDouble(e.Row.Cells[7].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "合计：";
                e.Row.Cells[6].Text = "kg";
                e.Row.Cells[7].Text = sum3.ToString();
            }
        }

        protected void lab_ID_TextChanged(object sender, EventArgs e)
        {
            TextBox row_id = (TextBox)sender;
            GridViewRow gr = (GridViewRow)row_id.Parent.Parent;
            string code = ((TextBox)gr.FindControl("lab_ID")).Text;
            string tuhao = code.Substring(0, 12);
            string sub_tuhao = tuhao.Substring(0, 5);
            string child_tuhao = sub_tuhao.Substring(0, 2);
            ((TextBox)gr.FindControl("lab_ID")).Text = tuhao;
            if(child_tuhao=="02")     //低值易耗
            {
                sqlText="select LVCG_NAME,LVCG_GUIGE,LVCG_UNIT from TBMA_LVCGMAINFO ";
                sqlText+="where LVCG_ID='" + tuhao + "'";
            }
            else if(sub_tuhao=="01.01")  //标准件
            {
                sqlText= "select BZJ_NAME,BZJ_GUIGE,BZJ_UNIT from TBMA_BZJINFO ";
                sqlText+="where BZJ_ID='" + tuhao + "'";
            }
            else
            {
                sqlText="select RM_NAME,RM_GUIGE,RM_UNIT,RM_CAIZHI from TBMA_RAWMAINFO ";
                sqlText+="where RM_ID='" + tuhao + "'";
            }
            SqlDataReader dr=DBCallCommon.GetDRUsingSqlText(sqlText);
            if(dr.Read())
            {
                ((TextBox)gr.FindControl("txt_name")).Text = dr[0].ToString();
                ((TextBox)gr.FindControl("txt_guige")).Text = dr[1].ToString();
                ((TextBox)gr.FindControl("txt_unit")).Text = dr[2].ToString();
                if (child_tuhao != "02" && sub_tuhao != "01.01")
                {
                    ((TextBox)gr.FindControl("txt_caizhi")).Text = dr[3].ToString();
                }
            }
            dr.Close();
        }
    }
}
