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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_HTZHIBIAODetail : System.Web.UI.Page
    {
        string action = "";
        string id_htys = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                action = Request.QueryString["action"].ToString().Trim();
                drpyearbind();
                if (action == "add")
                {
                    initdata();
                }
                else if (action == "edit")
                {
                    binddata();
                }
            }
        }
        //初始化数据
        private void initdata()
        {
            dplYear.SelectedIndex = 0;
            ht_addname.Text = Session["UserName"].ToString().Trim();
            ht_addtime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
        }
        //绑定年月
        private void drpyearbind()
        {
            for (int i = 0; i < 30; i++)
            {
                dplYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dplYear.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }
        //绑定数据
        private void binddata()
        {
            id_htys = Request.QueryString["id_htys"].ToString().Trim();
            string sql = "select * from FM_HTYUSUAN where id_htys=" + CommonFun.ComTryInt(id_htys) + "";
            System.Data.DataTable dt=DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                dplYear.SelectedValue = dt.Rows[0]["ht_year"].ToString().Trim().Substring(0, 4);
                ht_addname.Text = dt.Rows[0]["ht_addname"].ToString().Trim();
                ht_addtime.Text = dt.Rows[0]["ht_addtime"].ToString().Trim();
                ht_yusuanhte.Text = dt.Rows[0]["ht_yusuanhte"].ToString().Trim();
                ht_note.Text = dt.Rows[0]["ht_note"].ToString().Trim();
            }
        }
        //保存
        protected void btnsave_click(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString().Trim();
            string sqltext = "";
            if (dplYear.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年份!');", true);
                return;
            }

            if (action == "add")
            {
                string sqlcheck0 = "select * from FM_HTYUSUAN where ht_year='" + dplYear.SelectedValue.ToString().Trim() + "'";
                DataTable dtcheck0 = DBCallCommon.GetDTUsingSqlText(sqlcheck0);
                if (dtcheck0.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在该年度数据!');", true);
                    return;
                }
                else
                {
                    sqltext = "insert into FM_HTYUSUAN(ht_year,ht_addname,ht_addid,ht_addtime,ht_yusuanhte,ht_note,beiyong1) values('" + dplYear.SelectedValue.ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + ht_addtime.Text.Trim() + "'," + CommonFun.ComTryDecimal(ht_yusuanhte.Text.Trim()) + ",'" + ht_note.Text.Trim() + "','')";
                    DBCallCommon.ExeSqlText(sqltext);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location = window.opener.location.href;window.close();", true);
                }
            }
            else if(action=="edit")
            {
                id_htys = Request.QueryString["id_htys"].ToString().Trim();
                string sqlcheck1 = "select * from FM_HTYUSUAN where ht_year='" + dplYear.SelectedValue.ToString().Trim() + "' and id_htys!='" + id_htys + "'";
                DataTable dtcheck1 = DBCallCommon.GetDTUsingSqlText(sqlcheck1);
                if (dtcheck1.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在该年度数据!');", true);
                    return;
                }
                else
                {
                    sqltext = "update FM_HTYUSUAN set ht_year='" + dplYear.SelectedValue.ToString().Trim() + "',ht_addname='" + Session["UserName"].ToString().Trim() + "',ht_addid='" + Session["UserID"].ToString().Trim() + "',ht_addtime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',ht_yusuanhte=" + CommonFun.ComTryDecimal(ht_yusuanhte.Text.Trim()) + ",ht_note='" + ht_note.Text.Trim() + "' where id_htys='" + id_htys + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location = window.opener.location.href;window.close();", true);
                }
            }
        }
    }
}
