using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.PL_Data
{
   
    public partial class MakePlan : System.Web.UI.Page
    {
        string taskId;
        string sqlText;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            taskId = Request.QueryString["tarId"];
            if (!IsPostBack)
            {
                InitInfo();
                InitGridview();
            }
        }
        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitInfo()
        {
            //MP_CODE, MP_PJID, MP_PJNAME, MP_ENGNAME, MP_NOTE, MP_STATE
            sqlText = "select MP_PJID,MP_PJNAME,MP_ENGNAME,MP_DELIVERYDATE,MP_ACTURALDELIVERDATE,fileName ";
            sqlText += "from dbo.VIEW_TBMP_MAINPLANTOTAL where MP_CODE='" + taskId + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = taskId;
                lab_contract.Text = dr[0].ToString();
                lab_proname.Text = dr[1].ToString();
                lab_engname.Text = dr[2].ToString();
                txtDelivery.Text = dr[3].ToString();
                txtDeliveryActural.Text = dr[4].ToString();
            
            }
            dr.Close();
            GetModelName();
        }

        private void GetModelName()
        {
            sqlText = "select MODEL_ID+'|'+MODEL_NAME as NAME,MODEL_ID from TBMP_MAINPLANMODEL";
            string dataText = "NAME";
            string dataValue = "MODEL_ID";
            DBCallCommon.BindDdl(ddlModel, sqlText, dataText, dataValue);

        }
        private void InitGridview()
        {

            sqlText = "select * from TBMP_MAINPLANDETAIL where MP_ENGID='" + tsaid.Text + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void lbnSave_Click(object sender, EventArgs e)
        {
            string ret = this.CheckData();
            if (ret == "0")
            {
                List<string> sqllist = new List<string>();
                sqlText = "update TBMP_MAINPLANTOTAL set MP_DELIVERYDATE='" + txtDelivery.Text.Trim() + "',MP_ACTURALDELIVERDATE='"+txtDeliveryActural.Text.Trim()+"' where MP_CODE='"+tsaid.Text+"'";
                sqllist.Add(sqlText);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    //MP_ID, MP_ENGID, MP_PLNAME, MP_DAYS, MP_STARTDATA, MP_ENDTIME, MP_STATE, MP_ACTURALTIME, MP_TYPE
                    GridViewRow gRow = GridView1.Rows[i];
                    string type = gRow.Cells[1].Text;
                    string plname = gRow.Cells[2].Text;
                    string txtWarningDays = ((HtmlInputText)gRow.FindControl("txtWarningDays")).Value.Trim();
                    string days = ((HtmlInputText)gRow.FindControl("txtDays")).Value.Trim();
                    string startTime = ((HtmlInputText)gRow.FindControl("txtStart")).Value;
                    string endTime = ((HtmlInputText)gRow.FindControl("txtFinish")).Value;
                    string note = ((HtmlInputText)gRow.FindControl("txtNote")).Value;

                    sqlText = "update TBMP_MAINPLANDETAIL set MP_DAYS='" + days + "',MP_WARNINGDAYS='" + txtWarningDays + "',MP_STARTDATE='" + startTime + "',MP_ENDTIME='" + endTime + "',MP_NOTE='" + note + "' where MP_ENGID='" + tsaid.Text + "' and MP_PLNAME='" + plname + "'";
                    sqllist.Add(sqlText);
                }
                sqlText = "update TBMP_MAINPLANTOTAL set MP_STATE=1 where MP_CODE='" + tsaid.Text + "'";
                sqllist.Add(sqlText);
                try
                {
                    DBCallCommon.ExecuteTrans(sqllist);
                    Response.Write("<script>alert('保存成功')</script>");
                    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);

                }
                catch (Exception)
                {

                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据错误，请联系管理员！');", true);
                    Response.Write("<script>alert('数据错误，请联系管理员')</script>");
                }
               
                Response.Write("<script>location.href='MainPlan_View.aspx'</script>");

            }
            else
            {
                if (ret.Contains("warn"))
                {
                    Response.Write("<script>alert('【预警天数】格式错误，应为数字格式')</script>");
                }
                else if (ret.Contains("day"))
                {
                    Response.Write("<script>alert('【计划天数】格式错误，应为数字格式')</script>");
                }
            }
        }

        private string CheckData()
        {
            string result = "0";
            string pattern = @"^\d{1,2}$";
            Regex rgx = new Regex(pattern);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //MP_ID, MP_ENGID, MP_PLNAME, MP_DAYS, MP_STARTDATA, MP_ENDTIME, MP_STATE, MP_ACTURALTIME, MP_TYPE
                GridViewRow gRow = GridView1.Rows[i];

                string txtWarningDays = ((HtmlInputText)gRow.FindControl("txtWarningDays")).Value.Trim();
                string days = ((HtmlInputText)gRow.FindControl("txtDays")).Value.Trim();
                if (!rgx.IsMatch(txtWarningDays))
                {
                    result = "warning";
                    return result;
                }
                else if (!rgx.IsMatch(days))
                {
                    result = "day";
                    return result;
                }

            }
            return result;
        }

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (ddlModel.SelectedIndex==0)
            {
                InitInfo();
                InitGridview();
            }
            else
            {
                string ModelId = ddlModel.SelectedValue;
                string taskId = tsaid.Text.Trim();
                
                sqlText = "exec PL_UseModel '"+ModelId+"','"+taskId+"'";
                try
                {
                   dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                }
                catch (Exception)
                {
                    
                   Response.Write("<script>alert('程序出错，请联系管理员')</script>");
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
        }

    }
}
