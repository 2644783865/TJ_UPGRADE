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
    public partial class TM_MS_Detail_Change : System.Web.UI.Page
    {
        string sqlText = "";
        string mstable;
        string[] fields;
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
            fields = Request.QueryString["mschange"].ToString().Split('.');
            tsa_id.Text = fields[0].ToString();
            ms_no.Text = fields[0].ToString()+'.'+fields[1].ToString();
            string status = fields[2].ToString();
            if (int.Parse(status) >= 8)
            {
                sqlText = "select MAX(MS_STATE) from TBPM_MSCHANGE where MS_PID='" + ms_no.Text + "'";
                SqlDataReader da = DBCallCommon.GetDRUsingSqlText(sqlText);
                while (da.Read())
                {
                    ms_state.Value = da[0].ToString();
                }
                da.Close();
                if (ms_state.Value != "")
                {
                    Response.Redirect("TM_MS_Detail_Change_Audit.aspx?id=" + ms_no.Text);
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
        private void GetListName()
        {
            #region
            switch (eng_type.Value)
            {
                case "回转窑":
                    mstable = "TBPM_MSOFHZY";
                    break;
                case "球、立磨":
                    mstable = "TBPM_MSOFQLM";
                    break;
                case "篦冷机":
                    mstable = "TBPM_MSOFBLJ";
                    break;
                case "堆取料机":
                    mstable = "TBPM_MSOFDQJ";
                    break;
                case "钢结构及非标":
                    mstable = "TBPM_MSOFGFB";
                    break;
                case "电气及其他":
                    mstable = "TBPM_MSOFDQO";
                    break;
            }
            #endregion
        }

        //查询不同的制作明细表
        private void GetSearchList()
        {
            GetListName();
            sqlText = "select * from "+mstable+" where MS_PID='" + ms_no.Text + "' and MS_STATE='5'";
            DBCallCommon.BindGridView(GridView1, sqlText);
        }

        protected void btnchange_Click(object sender, EventArgs e)
        {
            #region
            string txt_tuhao = "";
            string txt_zongxu = "";
            string txt_name = "";
            string txt_guige = "";
            string txt_caizhi = "";
            string txt_num = "";
            string txt_uwght = "";
            string txt_tlwght = "";
            string txt_process = "";
            string txt_caseno = "";
            string txt_explain = "";
            string txt_note = "";
            #endregion
            if (txtid.Value == "1")
            {
                #region
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gr.FindControl("chk");
                    txt_tuhao = ((Label)gr.FindControl("txt_tuhao")).Text;
                    txt_zongxu = ((Label)gr.FindControl("txt_zongxu")).Text;
                    txt_name = ((Label)gr.FindControl("txt_name")).Text;
                    txt_guige = ((Label)gr.FindControl("txt_guige")).Text;
                    txt_caizhi = ((Label)gr.FindControl("txt_caizhi")).Text;
                    txt_num = ((Label)gr.FindControl("txt_num")).Text;
                    txt_uwght = ((Label)gr.FindControl("txt_uwght")).Text;
                    txt_tlwght = ((Label)gr.FindControl("txt_tlwght")).Text;
                    txt_process = ((Label)gr.FindControl("txt_process")).Text;
                    txt_caseno = ((Label)gr.FindControl("txt_caseno")).Text;
                    txt_explain = ((Label)gr.FindControl("txt_explain")).Text;
                    txt_note = ((Label)gr.FindControl("txt_note")).Text;
                    if (chk.Checked)
                    {
                        sqlText = "insert into TBPM_MSCHANGE ";
                        sqlText += "(MS_PID,MS_TUHAO,MS_ZONGXU,MS_PJID,MS_PJNAME,MS_ENGID,MS_ENGNAME,";
                        sqlText += "MS_NAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,MS_TLWGHT,";
                        sqlText += "MS_PROCESS,MS_TIMERQ,MS_ENVREFFCT,MS_NOTE,MS_STATE) ";
                        sqlText += "values ('" + ms_no.Text + "','" + txt_tuhao + "','" + txt_zongxu + "','" + pro_id.Value + "',";
                        sqlText += "'" + lab_proname.Text + "','" + tsa_id.Text + "','" + lab_engname.Text + "','" + txt_name + "',";
                        sqlText += "'" + txt_guige + "','" + txt_caizhi + "','" + txt_num + "','" + txt_uwght + "','" + txt_tlwght + "',";
                        sqlText += "'" + txt_process + "','" + txt_caseno + "','" + txt_explain + "','" + txt_note + "','0')";
                        DBCallCommon.ExeSqlText(sqlText);
                        sqlText = "update " + mstable + " set MS_STATE='6' ";
                        sqlText += "where MS_ENGID='" + tsa_id.Text + "' and MS_ZONGXU='" + txt_zongxu + "'";
                        DBCallCommon.ExeSqlText(sqlText);
                    }
                }
                #endregion
                sqlText = "insert into TBPM_MSCHANGERVW (MS_CODE,MS_PJID,MS_PJNAME,MS_ENGID,";
                sqlText += "MS_ENGNAME,MS_ENGTYPE,MS_SUBMITID,MS_SUBMITNM,MS_STATE) values ";
                sqlText += "('" + ms_no.Text + "','" + pro_id.Value + "','" + lab_proname.Text + "','" + tsa_id.Text + "',";
                sqlText += "'" + lab_engname.Text + "','" + eng_type.Value + "','" + Session["UserID"] + "','" + Session["UserName"] + "','0')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Redirect("TM_MS_Detail_Change_Audit.aspx?id=" + ms_no.Text);
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
