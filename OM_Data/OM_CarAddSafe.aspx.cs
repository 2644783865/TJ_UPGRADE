using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarAddSafe : System.Web.UI.Page
    {
        string action = string.Empty;
        string carnum = "";
        string code = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString();
            }
            if (Request.QueryString["flag"] != null)
            {
                carnum = Request.QueryString["flag"].ToString();
            }
            if (Request.QueryString["ID"] != null)
            {
                code = Request.QueryString["ID"].ToString();
            }
            if (!this.IsPostBack)
            {
                if (action == "add")
                {
                    ddl();
                    ddlDRIVER();
                    lblAddtime.Text = DateTime.Now.ToString();
                    txtSafeTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                   
                    
                }
            }
        }
        private void ddl()
        {
            ddlcarnum.Items.Clear();
            string sql="select CARNUM from TBOM_CARINFO";

            string datavalue = "CARNUM";
            DBCallCommon.BindDdl(ddlcarnum,sql,datavalue,datavalue);
        }
        private void ddlDRIVER()
        {
            txtSafer.Items.Clear();
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO WHERE ST_DEPID='02'";
            string datetext = "ST_NAME";
            string datavalue = "ST_ID";
            DBCallCommon.BindDdl(txtSafer, sql, datetext, datavalue);
            txtSafer.SelectedValue = Session["UserID"].ToString();
        }
        protected void btnOK_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            if (action == "add")
            {
                if (ddlcarnum.SelectedValue.ToString() == "-请选择-")
                {
                    txtCarNum.Text = "";
                    return;
                }
                else
                {
                    txtCarNum.Text = ddlcarnum.SelectedValue.ToString();
                    sqltext = "insert into TBOM_CARSAFE(CARNUM,SAFETIME,SAFETEXT,SAFESITE,SAFECOST,SAFER,SAFERID,ADDTIME) values('" + ddlcarnum.SelectedValue.Trim() + "','" + txtSafeTime.Text.Trim() + "','" + txtSafeText.Text.Trim() + "','" + txtSafeSite.Text.Trim() + "','" + txtSafeCost.Text.Trim() + "','"+txtSafer.SelectedItem.Text.Trim()+"','" + txtSafer.SelectedValue.Trim() + "','" + lblAddtime.Text.Trim() + "')";
                    DBCallCommon.ExeSqlText(sqltext);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('增添记录成功！');window.close();", true);
                    Response.Redirect("OM_CarWeihu.aspx");
                    //return;
                }
                
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_CarWeihu.aspx");
        }
    }
}
