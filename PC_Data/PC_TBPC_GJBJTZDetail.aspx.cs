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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_GJBJTZDetail : System.Web.UI.Page
    {
        public string Globeaction
        {
            get
            {
                object str = ViewState["Globeaction"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Globeaction"] = value;
            }
        }
        public string GlobeID
        {
            get
            {
                object str = ViewState["GlobeID"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["GlobeID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initpagemess();
                initpower();
            }
        }

        private void initpagemess()
        {
            string sqltext = "";
            sqltext = "select ST_NAME,ST_CODE from TBDS_STAFFINFO WHERE ST_DEPID='06'";
            string dataText = "ST_NAME";
            string dataValue = "ST_CODE";
            DBCallCommon.BindDdl(drp_cgy, sqltext, dataText, dataValue);
            drp_cgy.SelectedIndex = 0;

            string sqltext1 = "select ST_NAME,ST_CODE from TBDS_STAFFINFO WHERE ST_DEPID='07'";
            string dataText1 = "ST_NAME";
            string dataValue1 = "ST_CODE";
            DBCallCommon.BindDdl(drp_cyjsr, sqltext1, dataText1, dataValue1);
            drp_cyjsr.SelectedIndex = 0;

            DBCallCommon.BindDdl(drp_cyrkr, sqltext1, dataText1, dataValue1);
            drp_cyrkr.SelectedIndex = 0;

            DBCallCommon.BindDdl(drp_cyckr, sqltext1, dataText1, dataValue1);
            drp_cyckr.SelectedIndex = 0;

            if (Request.QueryString["Action"] != null)
            {
                Globeaction = Request.QueryString["Action"].ToString();
            }
            else
            {
                Globeaction = "";
            }
            if (Request.QueryString["kc_id"] != null)
            {
                GlobeID = Request.QueryString["kc_id"].ToString();
            }
            else
            {
                GlobeID = "";
            }
            if (Globeaction == "Look")
            {
                Pan_cg.Enabled = false;
                Pan_zl.Enabled = false;
                Pan_cy.Enabled = false;
                Pan_sc.Enabled = false;
                tb_lph.Enabled = false;
                tb_supply.Enabled = false;
                btn_submit.Visible = false;
            }
            else if (Globeaction == "Edit")
            {
                if (Session["UserDeptID"].ToString() == "06")//采购部
                {
                    Pan_zl.Enabled = false;
                    Pan_cy.Enabled = false;
                    Pan_sc.Enabled = false;
                }
                else if (Session["UserDeptID"].ToString() == "05")//质量部
                {
                    Pan_cg.Enabled = false;
                    Pan_cy.Enabled = false;
                    Pan_sc.Enabled = false;
                    tb_lph.Enabled = false;
                    tb_supply.Enabled = false;
                }
                else if (Session["UserDeptID"].ToString() == "07")//储运部
                {
                    Pan_cg.Enabled = false;
                    Pan_zl.Enabled = false;
                    Pan_sc.Enabled = false;
                    tb_lph.Enabled = false;
                    tb_supply.Enabled = false;
                }
                else if (Session["UserDeptID"].ToString() == "04")//生产部
                {
                    Pan_cg.Enabled = false;
                    Pan_zl.Enabled = false;
                    Pan_cy.Enabled = false;
                    tb_lph.Enabled = false;
                    tb_supply.Enabled = false;
                }
            }

        }
        private void initpower()
        {
            string sqltext = "";
            sqltext = "SELECT  KC_ID, PJ_NAME, KC_PJID, KC_SUPPLY, CS_NAME, KC_ENGID, TSA_ENGNAME, KC_GJNM, KC_LPH, KC_CGJCSJ, KC_CGJCZT, KC_CGNOTE," +
                     " KC_ZLCCJC, KC_ZLCCTM, KC_ZLCFJC, KC_ZLCFTM, KC_ZLCSJC, KC_ZLCSTM, KC_ZLNOTE, KC_CYJSTM, KC_CYRKTM, KC_CYRKR, KC_CYXHDD, " +
                     " KC_CYNOTE, KC_SCPSCL, KC_SCTM, KC_SCCFDD, KC_SCNOTE, KC_CGY, KC_CYJSR, KC_CYJSRNM, KC_CYRKRNM, KC_CGYNM, KC_CYCKTM, " +
                      "KC_CYCKR, KC_CYCKRNM  " +
                      "FROM View_TBPC_GJBJTZdetail where KC_ID=" + GlobeID + "";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                tb_pjnm.Text = dt.Rows[0]["PJ_NAME"].ToString();
                tb_engnm.Text = dt.Rows[0]["TSA_ENGNAME"].ToString();
                tb_gjnm.Text = dt.Rows[0]["KC_GJNM"].ToString();
                tb_lph.Text = dt.Rows[0]["KC_LPH"].ToString();
                tb_supply.Text = dt.Rows[0]["CS_NAME"].ToString();
                tb_supplyid.Text = dt.Rows[0]["KC_SUPPLY"].ToString();

                ip_jctm.Text = dt.Rows[0]["KC_CGJCSJ"].ToString();
                tb_jczt.Text = dt.Rows[0]["KC_CGJCZT"].ToString();
                drp_cgy.SelectedValue = dt.Rows[0]["KC_CGY"].ToString();
                tb_cgnote.Text = dt.Rows[0]["KC_CGNOTE"].ToString();

                tb_zlccjc.Text = dt.Rows[0]["KC_ZLCCJC"].ToString();
                tb_zlcctm.Text = dt.Rows[0]["KC_ZLCCTM"].ToString();
                tb_zlcfjc.Text = dt.Rows[0]["KC_ZLCFJC"].ToString();
                tb_zlcftm.Text = dt.Rows[0]["KC_ZLCFTM"].ToString();
                tb_zlcsjc.Text = dt.Rows[0]["KC_ZLCSJC"].ToString();
                tb_zlcstm.Text = dt.Rows[0]["KC_ZLCSTM"].ToString();
                tb_zlnote.Text = dt.Rows[0]["KC_ZLNOTE"].ToString();

                tb_cyjstm.Text = dt.Rows[0]["KC_CYJSTM"].ToString();
                drp_cyjsr.SelectedValue = dt.Rows[0]["KC_CYJSR"].ToString();
                tb_cyrktm.Text = dt.Rows[0]["KC_CYRKTM"].ToString();
                drp_cyrkr.SelectedValue = dt.Rows[0]["KC_CYRKR"].ToString();
                tb_cycktm.Text = dt.Rows[0]["KC_CYCKTM"].ToString();
                drp_cyckr.SelectedValue = dt.Rows[0]["KC_CYCKR"].ToString();
                tb_cyxhdd.Text = dt.Rows[0]["KC_CYXHDD"].ToString();
                tb_cynote.Text = dt.Rows[0]["KC_CYNOTE"].ToString();

                tb_scpscl.Text = dt.Rows[0]["KC_SCPSCL"].ToString();
                tb_scpstm.Text = dt.Rows[0]["KC_SCTM"].ToString();
                tb_sccfdd.Text = dt.Rows[0]["KC_SCCFDD"].ToString();
                tb_scnote.Text = dt.Rows[0]["KC_SCNOTE"].ToString();
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            string sql = "";
            sql = "update TBPC_KEYCOMTZ set KC_LPH='" + tb_lph.Text + "',KC_SUPPLY='" + tb_supplyid.Text.Trim() + "',KC_GJNM='" + tb_gjnm.Text.Trim() + "',KC_CGJCSJ='" + ip_jctm.Text + "',KC_CGJCZT='" + tb_jczt.Text + "', " +
                 "KC_CGY='" + drp_cgy.SelectedValue.ToString() + "',KC_CGNOTE='" + tb_cgnote.Text + "',KC_ZLCCJC='" + tb_zlccjc.Text + "',KC_ZLCCTM='" + tb_zlcctm.Text + "'," +
                 "KC_ZLCFJC='" + tb_zlcfjc.Text + "',KC_ZLCFTM='" + tb_zlcftm.Text + "',KC_ZLCSJC='" + tb_zlcsjc.Text + "',KC_ZLCSTM='" + tb_zlcstm.Text + "',KC_ZLNOTE='" + tb_zlnote.Text + "'," +
                 "KC_CYJSTM='" + tb_cyjstm.Text + "',KC_CYJSR='" + drp_cyjsr.SelectedValue.ToString() + "',KC_CYRKTM='" + tb_cyrktm.Text + "',KC_CYRKR='" + drp_cyrkr.SelectedValue.ToString() + "'," +
                 "KC_CYCKTM='" + tb_cycktm.Text + "',KC_CYCKR='" + drp_cyckr.SelectedValue.ToString() + "',KC_CYXHDD='" + tb_cyxhdd.Text + "',KC_CYNOTE='" + tb_cynote.Text + "'," +
                 "KC_SCPSCL='" + tb_scpscl.Text + "',KC_SCTM='" + tb_scpstm.Text.ToString() + "',KC_SCCFDD='" + tb_sccfdd.Text + "',KC_SCNOTE='" + tb_scnote.Text + "' where KC_ID='" + GlobeID + "'";
            DBCallCommon.ExeSqlText(sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
        }


        protected void tb_supply_Textchanged(object sender, EventArgs e)
        {
            if (tb_supply.Text.ToString().Contains("|"))
            {
                string[] Arry = tb_supply.Text.Split('|');
                tb_supply.Text = Arry[0].ToString().Trim();
                tb_supplyid.Text = Arry[1].ToString().Trim();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写供应商！');", true);
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_GJBJTZ.aspx");
        }
    }
}
