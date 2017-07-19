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
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Date_BJDchaifen : System.Web.UI.Page
    {
        public string gloabptc
        {
            get
            {
                object str = ViewState["gloabptc"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptc"] = value;
            }
        }
        public string gloabshape
        {
            get
            {
                object str = ViewState["gloabshape"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabshape"] = value;
            }
        }
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initpagemess();
                purchaseplan_Repeater_Bind();
            }
        }
        private void initpagemess()
        {
            if (Request.QueryString["ptcode"] != null)
            {
                gloabptc = Server.UrlDecode(Request.QueryString["ptcode"].ToString());
            }
            else
            {
                gloabptc = "";
            }
            if (Request.QueryString["shape"] != null)
            {
                gloabshape = Server.UrlDecode(Request.QueryString["shape"].ToString());
            }
            else
            {
                gloabshape = "";
            }
            if (Request.QueryString["sheetno"] != null)
            {
                gloabsheetno = Server.UrlDecode(Request.QueryString["sheetno"].ToString());
            }
            else
            {
                gloabsheetno = "";
            }
        }

        private void purchaseplan_Repeater_Bind()
        {
            string sqltext = "";
            sqltext = "SELECT picno, pjid, pjnm, engid, engnm, marid,PIC_MASHAPE ,marnm, margg, margb, marcz, marunit,ptcode,PIC_TUHAO, " +
                      "marfzunit, length, width, marnum, marfznum, marzxnum, marzxfznum,detailnote  " +
                       "FROM  View_TBPC_IQRCMPPRICE   where ptcode='" + gloabptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            purchaseplan_Repeater.DataSource = dt;
            purchaseplan_Repeater.DataBind();
        }

        protected void purchaseplan_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell FZnum1 = (HtmlTableCell)e.Item.FindControl("fznum1");
                HtmlTableCell FZunit1 = (HtmlTableCell)e.Item.FindControl("fzunit1");
                if (gloabshape == "非定尺板" || gloabshape == "标(发运)" || gloabshape == "标(组装)")
                {
                    FZnum1.Visible = false;
                    FZunit1.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableCell FZnum2 = (HtmlTableCell)e.Item.FindControl("fznum2");
                HtmlTableCell FZunit2 = (HtmlTableCell)e.Item.FindControl("fzunit2");
                if (gloabshape == "非定尺板" || gloabshape == "标(发运)" || gloabshape == "标(组装)")
                {
                    FZnum2.Visible = false;
                    FZunit2.Visible = false;
                }
            }
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string sheetno = "";
            string ptcode = "";
            string newptcode="";
            string tuhao = "";
            string marid = "";
            string pjid = "";
            string engid = "";
            double length = 0;
            double width = 0;
            double num1 = 0;
            double fznum1 = 0;
            double num2 = 0;
            double fznum2 = 0;
            string note = "";
            string ghs1 = "";
            string ghs2 = "";
            string ghs3 = "";
            string ghs4 = "";
            string ghs5 = "";
            string ghs6 = "";
            foreach (RepeaterItem Reitem in purchaseplan_Repeater.Items)
            {
                note = ((Label)Reitem.FindControl("PUR_NOTE")).Text.ToString();
                ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text.ToString();
                tuhao = ((Label)Reitem.FindControl("PUR_TUHAO")).Text.ToString();
                marid = ((Label)Reitem.FindControl("PUR_MARID")).Text.ToString();
                length = Convert.ToDouble(((Label)Reitem.FindControl("PUR_LENGTH")).Text.ToString());
                width = Convert.ToDouble(((Label)Reitem.FindControl("PUR_WIDTH")).Text.ToString());
                num1 = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_NUM")).Text.ToString());
                fznum1 = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_FZNUM")).Text.ToString());
                sqltext = "select PIC_SHEETNO,PIC_PJID,PIC_ENGID,PIC_QUANTITY,PIC_FZNUM,PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptcode + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    sheetno = dt.Rows[0]["PIC_SHEETNO"].ToString();
                    pjid = dt.Rows[0]["PIC_PJID"].ToString();
                    engid = dt.Rows[0]["PIC_ENGID"].ToString();
                    num2 = Convert.ToDouble(dt.Rows[0]["PIC_QUANTITY"].ToString());
                    fznum2 = Convert.ToDouble(dt.Rows[0]["PIC_FZNUM"].ToString());
                    ghs1 = dt.Rows[0]["PIC_SUPPLIERAID"].ToString();
                    ghs2 = dt.Rows[0]["PIC_SUPPLIERBID"].ToString();
                    ghs3 = dt.Rows[0]["PIC_SUPPLIERCID"].ToString();
                    ghs4 = dt.Rows[0]["PIC_SUPPLIERDID"].ToString();
                    ghs5 = dt.Rows[0]["PIC_SUPPLIEREID"].ToString();
                    ghs6 = dt.Rows[0]["PIC_SUPPLIERFID"].ToString();
                }
            }
            if (num1 > num2 || fznum1 > fznum2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('拆分数量大于原来的数量！');", true);
            }
            else if (num1 == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('拆分数量为0，请输入正确的数量！');", true);
            }
            else
            {
                if (ptcode.Contains("#"))
                {
                    sqltext = "update TBPC_IQRCMPPRICE set PIC_QUANTITY=" + num1 + ",PIC_FZNUM=" + fznum1 + ",PIC_ZXNUM=" + num1 + ",PIC_ZXFUNUM=" + fznum1 + " where PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    newptcode = generatecode();
                    num1 = num2 - num1;
                    fznum1 = fznum2 - fznum1;
                    sqltext = "insert into TBPC_IQRCMPPRICE (PIC_SHEETNO,PIC_PTCODE,PIC_TUHAO,PIC_PJID,PIC_ENGID,PIC_MARID,PIC_MASHAPE,PIC_LENGTH,PIC_WIDTH,PIC_QUANTITY,PIC_FZNUM,PIC_ZXNUM,PIC_ZXFUNUM,PIC_NOTE,PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID) " +
                                "values ('" + sheetno + "','" + newptcode + "','" + tuhao + "','" + pjid + "','" + engid + "','" + marid + "','" + gloabshape + "','" + length + "','" + width + "','" + num1 + "','" + fznum1 + "','" + num1 + "','" + fznum1 + "','" + note + "','" + ghs1 + "','" + ghs2 + "','" + ghs3 + "','" + ghs4 + "','" + ghs5 + "','" + ghs6 + "')";
                    DBCallCommon.ExeSqlText(sqltext);
                }
                else
                {
                    newptcode = generatecode();
                    sqltext = "insert into TBPC_IQRCMPPRICE (PIC_SHEETNO,PIC_PTCODE,PIC_TUHAO,PIC_PJID,PIC_ENGID,PIC_MARID,PIC_MASHAPE,PIC_LENGTH,PIC_WIDTH,PIC_QUANTITY,PIC_FZNUM,PIC_ZXNUM,PIC_ZXFUNUM,PIC_NOTE, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID) " +
                                "values ('" + sheetno + "','" + newptcode + "','" + tuhao + "','" + pjid + "','" + engid + "','" + marid + "','" + gloabshape + "','" + length + "','" + width + "','" + num1 + "','" + fznum1 + "','" + num1 + "','" + fznum1 + "','" + note + "','" + ghs1 + "','" + ghs2 + "','" + ghs3 + "','" + ghs4 + "','" + ghs5 + "','" + ghs6 + "')";
                    DBCallCommon.ExeSqlText(sqltext);

                    newptcode = generatecode();
                    num1 = num2 - num1;
                    fznum1 = fznum2 - fznum1;
                    sqltext = "insert into TBPC_IQRCMPPRICE (PIC_SHEETNO,PIC_PTCODE,PIC_TUHAO,PIC_PJID,PIC_ENGID,PIC_MARID,PIC_MASHAPE,PIC_LENGTH,PIC_WIDTH,PIC_QUANTITY,PIC_FZNUM,PIC_ZXNUM,PIC_ZXFUNUM,PIC_NOTE,PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID) " +
                               "values ('" + sheetno + "','" + newptcode + "','" + tuhao + "','" + pjid + "','" + engid + "','" + marid + "','" + gloabshape + "','" + length + "','" + width + "','" + num1 + "','" + fznum1 + "','" + num1 + "','" + fznum1 + "','" + note + "','" + ghs1 + "','" + ghs2 + "','" + ghs3 + "','" + ghs4 + "','" + ghs5 + "','" + ghs6 + "')";
                    DBCallCommon.ExeSqlText(sqltext);

                    sqltext = "delete from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                }
                Response.Redirect("TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + gloabsheetno + "");
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + gloabsheetno + "");
        }

        protected string generatecode()
        {
            string pi_id = "";
            if (gloabptc.Contains("#"))
            {
                gloabptc = gloabptc.Substring(0, gloabptc.IndexOf("#")).ToString();
            }
            string tag_pi_id = gloabptc + "#";
            string end_pi_id = "";
            string sqltext = "SELECT TOP 1 PIC_PTCODE FROM TBPC_IQRCMPPRICE WHERE PIC_PTCODE LIKE '" + tag_pi_id + "%' ORDER BY PIC_PTCODE DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32((dt.Rows[0]["PIC_PTCODE"].ToString().Substring(dt.Rows[0]["PIC_PTCODE"].ToString().Length - 1, 1))) + 1);
            }
            else
            {
                end_pi_id = "1";
            }
            pi_id = tag_pi_id + end_pi_id;
            return pi_id;
        }
    }
}
