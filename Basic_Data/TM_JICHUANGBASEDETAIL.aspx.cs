using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class TM_JICHUANGBASEDETAIL : System.Web.UI.Page
    {
        string action = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                action=Request.QueryString["action"].ToString().Trim();
                if (action == "add")
                {
                    lbaddper.Text = Session["UserName"].ToString().Trim();
                    lbaddperid.Text = Session["UserID"].ToString().Trim();
                    lbaddtime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
                }
            }
        }


        protected void btnsave_click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            string sqltext = "";
            string sqladd0 = "select * from TBTM_MEC where jc_bh='" + jc_bh.Text.Trim() + "'";
            DataTable dtadd0 = DBCallCommon.GetDTUsingSqlText(sqladd0);
            if (dtadd0.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('机床编号已存在!');", true);
                return;
            }
            else
            {
                if (jc_bh.Text.Trim() == "" || jc_type.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写机床编号和类型!');", true);
                    return;
                }
                else
                {
                    sqltext = "insert into TBTM_MEC(jc_bh,jc_type,jc_gxtypeable,jc_containper,jc_note,jc_state,jc_addman,jc_addmanid,jc_addtime) values('" + jc_bh.Text.Trim() + "','" + jc_type.Text.Trim() + "','" + jc_gxtypeable.Text.Trim() + "','" + jc_containper.Text.Trim() + "','" + jc_note.Text.Trim() + "','" + rad_state.SelectedValue.Trim() + "','" + lbaddper.Text.Trim() + "','" + lbaddperid.Text.Trim() + "','" + lbaddtime.Text.Trim() + "')";
                    list_sql.Add(sqltext);
                    DBCallCommon.ExecuteTrans(list_sql);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location = window.opener.location.href;window.close();", true);
                }   
            }
        }
    }
}
