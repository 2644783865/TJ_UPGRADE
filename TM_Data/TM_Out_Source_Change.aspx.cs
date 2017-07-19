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
    public partial class TM_Out_Source_Change : System.Web.UI.Page
    {
        string sqlText = "";
        string outchange_id = "";
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
            outchange_id = Request.QueryString["OSTchange_id"];
            string[] col_fields = outchange_id.Split('_');
            out_no.Text = col_fields[0].ToString();
            string status = col_fields[1].ToString();
            tsa_id.Text = col_fields[2].ToString();
            if (int.Parse(status) >= 8)
            {
                sqlText = "select MAX(OSL_STATE) from TBPM_OUTSCHANGE where OSL_CODE='" + out_no.Text + "'";
                SqlDataReader da = DBCallCommon.GetDRUsingSqlText(sqlText);
                while (da.Read())
                {
                    out_state.Value = da[0].ToString();
                }
                da.Close();
                if (out_state.Value != "")
                {
                    Response.Redirect("TM_Out_Source_Change_Audit.aspx?id=" + out_no.Text);
                }
                else
                {
                    sqlText = "select TSA_PJNAME,TSA_ENGNAME,TSA_PJID ";
                    sqlText += "from TBPM_TCTSASSGN where TSA_ID='" + tsa_id.Text + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                    while (dr.Read())
                    {
                        lab_proname.Text = dr[0].ToString();
                        lab_engname.Text = dr[1].ToString();
                        pro_id.Value = dr[2].ToString();
                    }
                    dr.Close();
                    GetOutChange();
                }
            }
        }
        private void GetOutChange()
        {
            sqlText = "select * from TBPM_OUTSOURCELIST ";
            sqlText += "where OSL_OUTSOURCENO='" + out_no.Text + "' and OSL_STATE='5'";
            DBCallCommon.BindGridView(GridView1,sqlText);
        }

        protected void btnchange_Click(object sender, EventArgs e)
        {
            #region
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
            #endregion
            if (txtid.Value == "1")
            {
                #region
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gr.FindControl("chk");
                    lab_name = ((Label)gr.FindControl("lab_name")).Text;
                    lab_biaozhi = ((Label)gr.FindControl("lab_biaozhi")).Text;
                    lab_guige = ((Label)gr.FindControl("lab_guige")).Text;
                    lab_caizhi = ((Label)gr.FindControl("lab_caizhi")).Text;
                    lab_unitwght = ((Label)gr.FindControl("lab_unitwght")).Text;
                    lab_num = ((Label)gr.FindControl("lab_num")).Text;
                    lab_totalwght = ((Label)gr.FindControl("lab_totalwght")).Text;
                    lab_wdepname = ((Label)gr.FindControl("lab_wdepname")).Text;
                    lab_process = ((Label)gr.FindControl("lab_process")).Text;
                    lab_date = ((Label)gr.FindControl("lab_date")).Text;
                    lab_place = ((Label)gr.FindControl("lab_place")).Text;
                    if (chk.Checked)
                    {
                        sqlText = "insert into TBPM_OUTSCHANGE ";
                        sqlText += "(OSL_CODE,OSL_NAME,OSL_BIAOSHINO,OSL_GUIGE,";
                        sqlText += "OSL_CAIZHI,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPNAME,";
                        sqlText += "OSL_REQUEST,OSL_REQDATE,OSL_DELSITE,OSL_STATE) ";
                        sqlText += "values ('" + out_no.Text + "','" + lab_name + "','" + lab_biaozhi + "','" + lab_guige + "',";
                        sqlText += "'" + lab_caizhi + "','" + lab_unitwght + "','" + lab_num + "','" + lab_totalwght + "',";
                        sqlText += "'" + lab_wdepname + "','" + lab_process + "','" + lab_date + "','" + lab_place + "','0')";
                        DBCallCommon.ExeSqlText(sqlText);
                    }
                }
                #endregion
                sqlText = "insert into TBPM_OUTSCHANGERVW (OST_CHANGECODE,OST_PJID,OST_PJNAME,";
                sqlText += "OST_ENGID,OST_ENGNAME,OST_SUBMITER,OST_SUBMITERNM,OST_STATE) ";
                sqlText += "values ('" + out_no.Text + "','" + pro_id.Value + "','" + lab_proname.Text + "','" + tsa_id.Text + "',";
                sqlText += "'" + lab_engname.Text + "','" + Session["UserID"] + "','" + Session["UserName"] + "','0')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Redirect("TM_Out_Source_Change_Audit.aspx?id=" + out_no.Text);
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
