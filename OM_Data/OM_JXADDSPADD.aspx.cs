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
using System.Collections.Generic;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_JXADDSPADD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string bh = DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + Session["UserID"].ToString().Trim();
            if (tb_yearmonth.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择年月！');", true);
                return;
            }
            if (lbstid.Text.Trim() == "" || txtname.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写人员信息！');", true);
                return;
            }
            if (txt_jxgzxs.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写绩效工资系数！');", true);
                return;
            }
            if (ddlType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择类型！');", true);
                return;
            }
            string sqltext = "insert into OM_JXADDSP(bh,things,yearmonth,creattime,creatstid,creatstname,jxaddstid,jxaddstname,jxadddepartment,jxgzxs,type) values('" + bh + "','" + txt_contents.Text.Trim() + "','" + tb_yearmonth.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + lbstid.Text.Trim() + "','" + txtname.Text.Trim() + "','" + txtdepartment.Text.Trim() + "','" + txt_jxgzxs.Text.Trim() + "','" + ddlType.SelectedValue.Trim() + "')";
            list.Add(sqltext);
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_JXADDSPdetail.aspx?spid=" + bh);
        }


        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);

            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    lbstid.Text = stid;
                    txtname.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    txtdepartment.Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }
            }
        }
    }
}
