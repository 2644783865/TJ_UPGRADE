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
    public partial class FM_CWZHIBIAODetail : System.Web.UI.Page
    {
        string action = "";
        string id_cwzb = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                action = Request.QueryString["action"].ToString().Trim();
                drpyearmonthbind();
                if (action == "add")
                {
                    initdata();
                }
                else if (action == "edit")
                {
                    binddata();
                }
                else if (action == "view")
                {
                    binddata();
                    btnsave.Visible = false;
                }
            }
        }
        //初始化数据
        private void initdata()
        {
            dplYear.SelectedIndex = 0;
            dplMoth.SelectedIndex = 0;
            cw_zdrname.Text = Session["UserName"].ToString().Trim();
            cw_zdtime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
        }
        //绑定年月
        private void drpyearmonthbind()
        {
            for (int i = 0; i < 30; i++)
            {
                dplYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dplYear.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dplMoth.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dplMoth.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }

        //绑定数据
        private void binddata()
        {
            id_cwzb = Request.QueryString["id_cwzb"].ToString().Trim();
            string sql = "select *,(yychengben+xsfeiyong+glfeiyong+cwfeiyong) as cbfeiyonghj,(yyshouru-yychengben-xsfeiyong-glfeiyong-cwfeiyong) as lrzonge from FM_CWZHIBIAO where id_cwzb=" + CommonFun.ComTryInt(id_cwzb) + "";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                dplYear.SelectedValue = dt.Rows[0]["cw_yearmonth"].ToString().Trim().Substring(0, 4);
                dplMoth.SelectedValue = dt.Rows[0]["cw_yearmonth"].ToString().Trim().Substring(5, 2);
                cw_zdrname.Text = dt.Rows[0]["cw_zdrname"].ToString().Trim();
                cw_zdtime.Text = dt.Rows[0]["cw_zdtime"].ToString().Trim();
                yychengben.Text = dt.Rows[0]["yychengben"].ToString().Trim();
                xsfeiyong.Text = dt.Rows[0]["xsfeiyong"].ToString().Trim();
                glfeiyong.Text = dt.Rows[0]["glfeiyong"].ToString().Trim();
                cwfeiyong.Text = dt.Rows[0]["cwfeiyong"].ToString().Trim();
                yychengbenys.Text = dt.Rows[0]["yychengbenys"].ToString().Trim();
                xsfeiyongys.Text = dt.Rows[0]["xsfeiyongys"].ToString().Trim();
                glfeiyongys.Text = dt.Rows[0]["glfeiyongys"].ToString().Trim();
                cwfeiyongys.Text = dt.Rows[0]["cwfeiyongys"].ToString().Trim();
                cbfeiyonghj.Text = dt.Rows[0]["cbfeiyonghj"].ToString().Trim();
                cbfeiyonghjys.Text = dt.Rows[0]["cbfeiyonghjys"].ToString().Trim();
                yyshouru.Text = dt.Rows[0]["yyshouru"].ToString().Trim();
                yyshouruys.Text = dt.Rows[0]["yyshouruys"].ToString().Trim();
                lrzonge.Text = dt.Rows[0]["lrzonge"].ToString().Trim();
                lrzongeys.Text = dt.Rows[0]["lrzongeys"].ToString().Trim();
                cw_note.Text = dt.Rows[0]["cw_note"].ToString().Trim();
            }
        }

        //年月份改变查询
        protected void OnSelectedIndexChanged_dplYearMoth(object sender, EventArgs e)
        {
            string sqltext = "";
            if (dplYear.SelectedIndex != 0)
            {
                sqltext = "select * from FM_CWZHIBIAO where cw_yearmonth like '" + dplYear.SelectedValue.ToString().Trim() + "-%'";
                System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt0.Rows.Count > 0)
                {
                    yychengbenys.Text = dt0.Rows[0]["yychengbenys"].ToString().Trim();
                    xsfeiyongys.Text = dt0.Rows[0]["xsfeiyongys"].ToString().Trim();
                    glfeiyongys.Text = dt0.Rows[0]["glfeiyongys"].ToString().Trim();
                    cwfeiyongys.Text = dt0.Rows[0]["cwfeiyongys"].ToString().Trim();
                    cbfeiyonghjys.Text = dt0.Rows[0]["cbfeiyonghjys"].ToString().Trim();
                    yyshouruys.Text = dt0.Rows[0]["yyshouruys"].ToString().Trim();
                    lrzongeys.Text = dt0.Rows[0]["lrzongeys"].ToString().Trim();
                }
                else
                {
                    yychengbenys.Text = "";
                    xsfeiyongys.Text = "";
                    glfeiyongys.Text = "";
                    cwfeiyongys.Text = "";
                    cbfeiyonghjys.Text = "";
                    yyshouruys.Text = "";
                    lrzongeys.Text = "";
                }
                if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
                {
                    sqltext = "select * from TBFM_LRFP where RQBH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' and LRFP_TYPE='本年累计数'";
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt1.Rows.Count > 0)
                    {
                        yychengben.Text = Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_JYCB"].ToString().Trim())) / 10000), 2).ToString().Trim();
                        xsfeiyong.Text = Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_XSFY"].ToString().Trim())) / 10000), 2).ToString().Trim();
                        glfeiyong.Text = Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_GLFY"].ToString().Trim())) / 10000), 2).ToString().Trim();
                        cwfeiyong.Text = Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_CWFY"].ToString().Trim())) / 10000), 2).ToString().Trim();
                        cbfeiyonghj.Text = (Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_JYCB"].ToString().Trim())) / 10000), 2) + Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_XSFY"].ToString().Trim())) / 10000), 2) + Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_GLFY"].ToString().Trim())) / 10000), 2) + Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_CWFY"].ToString().Trim())) / 10000), 2)).ToString().Trim();
                        yyshouru.Text = Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR"].ToString().Trim())) / 10000), 2).ToString().Trim();
                        lrzonge.Text = (Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR"].ToString().Trim())) / 10000), 2) - Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_JYCB"].ToString().Trim())) / 10000), 2) - Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_XSFY"].ToString().Trim())) / 10000), 2) - Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_GLFY"].ToString().Trim())) / 10000), 2) - Math.Round(((CommonFun.ComTryDouble(dt1.Rows[0]["LRFP_YYSR_CWFY"].ToString().Trim())) / 10000), 2)).ToString().Trim();
                    }
                    else
                    {
                        yychengben.Text = "";
                        xsfeiyong.Text = "";
                        glfeiyong.Text = "";
                        cwfeiyong.Text = "";
                        cbfeiyonghj.Text = "";
                        yyshouru.Text = "";
                        lrzonge.Text = "";
                    }
                }
            }
        }

        //保存
        protected void btnsave_click(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString().Trim();
            string sqltext = "";
            if (dplMoth.SelectedIndex == 0 || dplYear.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月!');", true);
                return;
            }

            //获取下一周期年月
            string yearmonth = dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim();
            DateTime yearmonthnow = DateTime.Parse(yearmonth);
            string yearmonthnext = yearmonthnow.AddMonths(1).ToString("yyyy-MM").Trim();

            if (action == "add")
            {
                string sqlcheck0 = "select * from FM_CWZHIBIAO where cw_yearmonth='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "'";
                DataTable dtcheck0 = DBCallCommon.GetDTUsingSqlText(sqlcheck0);
                if (dtcheck0.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在该年月数据!');", true);
                    return;
                }
                else
                {
                    sqltext = "insert into FM_CWZHIBIAO(cw_yearmonth,cw_nextyearmonth,cw_zdrname,cw_zdrid,cw_zdtime,yychengben,yychengbenys,xsfeiyong,xsfeiyongys,glfeiyong,glfeiyongys,cwfeiyong,cwfeiyongys,cbfeiyonghjys,yyshouru,yyshouruys,lrzongeys,cw_note,beiyong1) values('" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "','" + yearmonthnext + "','" + Session["UserName"].ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + cw_zdtime.Text.Trim() + "'," + CommonFun.ComTryDecimal(yychengben.Text.Trim()) + "," + CommonFun.ComTryDecimal(yychengbenys.Text.Trim()) + "," + CommonFun.ComTryDecimal(xsfeiyong.Text.Trim()) + "," + CommonFun.ComTryDecimal(xsfeiyongys.Text.Trim()) + "," + CommonFun.ComTryDecimal(glfeiyong.Text.Trim()) + "," + CommonFun.ComTryDecimal(glfeiyongys.Text.Trim()) + "," + CommonFun.ComTryDecimal(cwfeiyong.Text.Trim()) + "," + CommonFun.ComTryDecimal(cwfeiyongys.Text.Trim()) + "," + CommonFun.ComTryDecimal(cbfeiyonghjys.Text.Trim()) + "," + CommonFun.ComTryDecimal(yyshouru.Text.Trim()) + "," + CommonFun.ComTryDecimal(yyshouruys.Text.Trim()) + "," + CommonFun.ComTryDecimal(lrzongeys.Text.Trim()) + ",'" + cw_note.Text.Trim() + "','')";
                    DBCallCommon.ExeSqlText(sqltext);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location = window.opener.location.href;window.close();", true);
                }
            }
            else if (action == "edit")
            {
                id_cwzb = Request.QueryString["id_cwzb"].ToString().Trim();
                string sqlcheck1 = "select * from FM_CWZHIBIAO where cw_yearmonth='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' and id_cwzb!='" + id_cwzb + "'";
                DataTable dtcheck1 = DBCallCommon.GetDTUsingSqlText(sqlcheck1);
                if (dtcheck1.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在该年月数据!');", true);
                    return;
                }
                else
                {
                    sqltext = "update FM_CWZHIBIAO set cw_yearmonth='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "',cw_nextyearmonth='" + yearmonthnext + "',cw_zdrname='" + Session["UserName"].ToString().Trim() + "',cw_zdrid='" + Session["UserID"].ToString().Trim() + "',cw_zdtime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',yychengben=" + CommonFun.ComTryDecimal(yychengben.Text.Trim()) + ",yychengbenys=" + CommonFun.ComTryDecimal(yychengbenys.Text.Trim()) + ",xsfeiyong=" + CommonFun.ComTryDecimal(xsfeiyong.Text.Trim()) + ",xsfeiyongys=" + CommonFun.ComTryDecimal(xsfeiyongys.Text.Trim()) + ",glfeiyong=" + CommonFun.ComTryDecimal(glfeiyong.Text.Trim()) + ",glfeiyongys=" + CommonFun.ComTryDecimal(glfeiyongys.Text.Trim()) + ",cwfeiyong=" + CommonFun.ComTryDecimal(cwfeiyong.Text.Trim()) + ",cwfeiyongys=" + CommonFun.ComTryDecimal(cwfeiyongys.Text.Trim()) + ",cbfeiyonghjys=" + CommonFun.ComTryDecimal(cbfeiyonghjys.Text.Trim()) + ",yyshouru=" + CommonFun.ComTryDecimal(yyshouru.Text.Trim()) + ",yyshouruys=" + CommonFun.ComTryDecimal(yyshouruys.Text.Trim()) + ",lrzongeys=" + CommonFun.ComTryDecimal(lrzongeys.Text.Trim()) + ",cw_note='" + cw_note.Text.Trim() + "' where id_cwzb='" + id_cwzb + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location = window.opener.location.href;window.close();", true);
                }
            }
        }
    }
}
