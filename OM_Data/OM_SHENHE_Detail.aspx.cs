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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_SHENHE_Detail : System.Web.UI.Page
    {
        string sqltext;
        string action;
        string msid;
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            action = Request.QueryString["action"];
            if (!IsPostBack)
            {
                if (action == "modify")
                {
                    IntVar();
                }
            }
        }
        private void IntVar()
        {
            msid = Request.QueryString["msid"];
            sqltext = "select TYPE,TYPE_ID,FIRSTMAN,SECONDMAN,THIRDMAN,STATE,AUDITLEVEL,FIRSTMANNM,SECONDMANNM,THIRDMANNM from View_TBOM_SHENHE where ID='" + msid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                txtleixing.Text = dt.Rows[0]["TYPE"].ToString();
                leixingid.Value = dt.Rows[0]["TYPE_ID"].ToString();
                rblSHJS.SelectedValue = dt.Rows[0]["AUDITLEVEL"].ToString();
                txt_first.Text = dt.Rows[0]["FIRSTMANNM"].ToString();
                firstid.Value = dt.Rows[0]["FIRSTMAN"].ToString();
                txt_second.Text = dt.Rows[0]["SECONDMANNM"].ToString();
                secondid.Value = dt.Rows[0]["SECONDMAN"].ToString();
                txt_third.Text = dt.Rows[0]["THIRDMANNM"].ToString();
                thirdid.Value = dt.Rows[0]["THIRDMAN"].ToString();
                rblstate.SelectedValue = dt.Rows[0]["STATE"].ToString();

            }

            //rblSHJS.Enabled = false;
            //hlSelect1.Visible = false;
            //hlSelect2.Visible = false;
            //hlSelect3.Visible = false;
            //hlleixing.Visible = false;
        }
        protected void rblSHJS_change(object sender,EventArgs e)
        {
            string dengji = rblSHJS.SelectedValue.ToString();
            if(dengji=="1")
            {
                hlSelect1.Visible = true;
                hlSelect2.Visible = false;
                hlSelect3.Visible = false;
                secondid.Value = "259";
                thirdid.Value = "259";
            }
            if (dengji == "2")
            {
                hlSelect1.Visible = true;
                hlSelect2.Visible = true;
                hlSelect3.Visible = false;
                thirdid.Value = "259";
            }
            if (dengji == "3")
            {
                hlSelect1.Visible = true;
                hlSelect2.Visible = true;
                hlSelect3.Visible = true;
            }

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string TT = "";
            string ss = "select DEP_NAME FROM TBDS_DEPINFO WHERE DEP_CODE = '" + leixingid.Value.ToString() + "'";
            DataTable tt = DBCallCommon.GetDTUsingSqlText(ss);
            if (tt.Rows.Count > 0)
            {
                TT = tt.Rows[0]["DEP_NAME"].ToString();
            }
            action = Request.QueryString["action"];
            if (action == "add")
            {
                //if (rblSHJS.SelectedValue.ToString() == "1" && firstid.Value.ToString() == "")
                //{
                //    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择一级审核人！');", true);
                //    return;
                //}
                //if ((rblSHJS.SelectedValue.ToString() == "2" && firstid.Value.ToString() == "") || (rblSHJS.SelectedValue.ToString() == "2" && secondid.Value.ToString() == ""))
                //{
                //    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择一级//二级审核人！');", true);
                //    return;
                //}
                //if ((rblSHJS.SelectedValue.ToString() == "3" && firstid.Value.ToString() == "") || (rblSHJS.SelectedValue.ToString() == "3" && secondid.Value.ToString() == "") || (rblSHJS.SelectedValue.ToString() == "3" && thirdid.Value.ToString() == ""))
                //{
                //    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择一级//二级//三级审核人！');", true);
                //    return;
                //}
                sqltext = "insert into TBOM_SHENHE(TYPE,FIRSTMAN,SECONDMAN,THIRDMAN,STATE,AUDITLEVEL,";
                sqltext += "CREATTM,CREATER,TYPE_ID) values('" + TT.Trim() + "','" + firstid.Value.Trim() + "','" + secondid.Value.ToString() + "',";
                sqltext += "'" + thirdid.Value.ToString() + "','" + rblstate.SelectedValue.ToString() + "','" + rblSHJS.SelectedValue.ToString() + "',";
                sqltext += "'" + System.DateTime.Now.ToString() + "','" + Session["UserID"].ToString() + "','" + leixingid.Value.ToString() + "')";
                DBCallCommon.ExeSqlText(sqltext);
                Response.Redirect("OM_SHENHE.aspx");
            }
            else
            {
                msid = Request.QueryString["msid"];
                sqltext = "update TBOM_SHENHE set TYPE='" + TT.Trim() + "',TYPE_ID='" + leixingid.Value.ToString() + "',STATE='" + rblstate.SelectedValue + "',FIRSTMAN='" + firstid.Value.ToString() + "',SECONDMAN='" + secondid.Value.ToString() + "',THIRDMAN='" + thirdid.Value.ToString() + "',CREATTM='" + System.DateTime.Now.ToString() + "',AUDITLEVEL='"+rblSHJS.SelectedValue.ToString()+"' where ID='" + msid + "'";
                DBCallCommon.ExeSqlText(sqltext);
                Response.Redirect("OM_SHENHE.aspx");
            }
            
        }
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("OM_SHENHE.aspx");
        }
    }
}
