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
    public partial class TM_MP_Require_Change : System.Web.UI.Page
    {
        string sqlText = "";
        string mpchange_id = "";
        string tablename = "";
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
            mpchange_id = Request.QueryString["mpchange_id"];
            string[] col_fields = mpchange_id.Split('_');
            mp_no.Text = col_fields[0].ToString();
            string status = col_fields[1].ToString();
            tsa_id.Text = col_fields[2].ToString();
            if (int.Parse(status) >= 8)
            {
                sqlText = "select MAX(MP_STATE) from TBPM_MPCHANGE where MP_PID='" + mp_no.Text + "'";
                SqlDataReader da = DBCallCommon.GetDRUsingSqlText(sqlText);
                while (da.Read())
                {
                    mp_state.Value = da[0].ToString();
                }
                da.Close();
                if (mp_state.Value != "")
                {
                    Response.Redirect("TM_MP_Require_Change_Audit.aspx?id=" + mp_no.Text);
                }
                else
                {
                    sqlText = "select TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
                    sqlText += "from TBPM_TCTSASSGN where TSA_ID='" + tsa_id.Text + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                    while (dr.Read())
                    {
                        lab_proname.Text = dr[0].ToString();
                        lab_engname.Text = dr[1].ToString();
                        eng_type.Value = dr[2].ToString();
                        pro_id.Value = dr[3].ToString();
                    }
                    dr.Close();
                    GetSearchList();
                }
            }
        }
        //初始化表名
        private void InitList()
        {
            #region
            switch (eng_type.Value)
            {
                case "回转窑":
                    tablename = "TBPM_MPFORHZY";
                    break;
                case "球、立磨":
                    tablename = "TBPM_MPFORQLM";
                    break;
                case "篦冷机":
                    tablename = "TBPM_MPFORBLJ";
                    break;
                case "堆取料机":
                    tablename = "TBPM_MPFORDQJ";
                    break;
                case "钢结构及非标":
                    tablename = "TBPM_MPFORGFB";
                    break;
                case "电气及其他":
                    tablename = "TBPM_MPFORDQO";
                    break;
            }
            #endregion
        }
        //查询不同的材料需用表
        private void GetSearchList()
        {
            InitList();
            sqlText = "select * from " + tablename + " ";
            sqlText+="where MP_PID='" + mp_no.Text + "' and MP_STATE='5'";
            DBCallCommon.BindGridView(GridView1, sqlText);
        }

        protected void btnchange_Click(object sender, EventArgs e)
        {
            #region
            string txt_zongxu = "";
            string txt_mpid = "";
            string txt_hmcode = "";
            string txt_name = "";
            string txt_guige = "";
            string txt_caizhi = "";
            string txt_unit = "";
            string txt_num = "";
            string txt_fixsize = "";
            string txt_use = "";
            string txt_type = "";
            string txt_time = "";
            string txt_influence = "";
            string txt_note = "";
            #endregion
            if (txtid.Value == "1")
            {
                #region
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gr.FindControl("chk");
                    txt_zongxu = ((Label)gr.FindControl("txt_zongxu")).Text;
                    txt_mpid = ((Label)gr.FindControl("txt_mpid")).Text;
                    txt_hmcode = ((Label)gr.FindControl("txt_hmcode")).Text;
                    txt_name = ((Label)gr.FindControl("txt_name")).Text;
                    txt_guige = ((Label)gr.FindControl("txt_guige")).Text;
                    txt_caizhi = ((Label)gr.FindControl("txt_caizhi")).Text;
                    txt_unit = ((Label)gr.FindControl("txt_unit")).Text;
                    txt_num = ((Label)gr.FindControl("txt_num")).Text;
                    txt_fixsize = ((Label)gr.FindControl("txt_fixsize")).Text;
                    if (txt_fixsize == "定尺")
                    {
                        txt_fixsize = "1";
                    }
                    if (txt_fixsize == "非定尺")
                    {
                        txt_fixsize = "0";
                    }
                    else
                    {
                        txt_fixsize = "";
                    }
                    txt_use = ((Label)gr.FindControl("txt_use")).Text;
                    txt_type = ((Label)gr.FindControl("txt_type")).Text;
                    txt_time = ((Label)gr.FindControl("txt_time")).Text;
                    txt_influence = ((Label)gr.FindControl("txt_influence")).Text;
                    txt_note = ((Label)gr.FindControl("txt_note")).Text;
                    if (chk.Checked)
                    {
                        sqlText = "insert into TBPM_MPCHANGE ";
                        sqlText += "(MP_PID,MP_MARID,MP_NAME,MP_PJID,MP_PJNAME,MP_ENGID,MP_ENGNAME,";
                        sqlText += "MP_ZONGXU,MP_HRCODE,MP_GUIGE,MP_CAIZHI,MP_UNIT,MP_NUMBER,MP_USAGE,";
                        sqlText += "MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_NOTE,MP_STATE,MP_FIXEDSIZE) ";
                        sqlText += "values ('" + mp_no.Text + "','" + txt_mpid + "','" + txt_name + "','" + pro_id.Value + "',";
                        sqlText += "'" + lab_proname.Text + "','" + tsa_id.Text + "','" + lab_engname.Text + "','" + txt_zongxu + "',";
                        sqlText += "'" + txt_hmcode + "','" + txt_guige + "','" + txt_caizhi + "','" + txt_unit + "','" + txt_num + "',";
                        sqlText += "'" + txt_use + "','" + txt_type + "','" + txt_time + "','" + txt_influence + "','" + txt_note + "','0','"+txt_fixsize+"')";
                        DBCallCommon.ExeSqlText(sqlText);
                    }
                }
                #endregion
                sqlText = "insert into TBPM_MPCHANGERVW (MP_CODE,MP_PJID,MP_PJNAME,MP_ENGID,";
                sqlText += "MP_ENGNAME,MP_ENGTYPE,MP_SUBMITID,MP_SUBMITNM,MP_STATE) values ";
                sqlText += "('" + mp_no.Text + "','" + pro_id.Value + "','" + lab_proname.Text + "','" + tsa_id.Text + "',";
                sqlText += "'" + lab_engname.Text + "','" + eng_type.Value + "','" + Session["UserID"] + "','" + Session["UserName"] + "','0')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Redirect("TM_MP_Require_Change_Audit.aspx?id=" + mp_no.Text);
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
