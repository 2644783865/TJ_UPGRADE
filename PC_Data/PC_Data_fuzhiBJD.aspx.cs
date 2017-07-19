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
    public partial class PC_Data_fuzhiBJD : System.Web.UI.Page
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
                gloabptc = Request.QueryString["ptcode"].ToString();
            }
            else
            {
                gloabptc = "";
            }
            if (Request.QueryString["sheetno"] != null)
            {
                gloabsheetno = Request.QueryString["sheetno"].ToString();
            }
            else
            {
                gloabsheetno = "";
            }
        }

        private void purchaseplan_Repeater_Bind()
        {
            string[] Arry;
            Arry = gloabptc.Split(',');
            string sqltext = "";
            sqltext = "SELECT picno, pjid, pjnm, engid, engnm, marid,PIC_MASHAPE ,marnm, margg, margb, marcz, marunit,ptcode,PIC_TUHAO, " +
                      "marfzunit, length, width, marnum, marfznum, marzxnum, marzxfznum,detailnote  " +
                      "FROM  View_TBPC_IQRCMPPRICE   where  ptcode in (";

            for (int i = 0; i < Arry.Length - 1; i++)
            {
                sqltext += "'" + Arry[i].ToString() + "',";
            }

            sqltext += "'" + Arry[Arry.Length - 1].ToString() + "')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            purchaseplan_Repeater.DataSource = dt;
            purchaseplan_Repeater.DataBind();
        }

        protected void purchaseplan_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
 
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string ptcode = "";
            string newptcode = "";
            string sheetcode = BJDno();
            string tuhao = "";
            string marid = "";
            string pjid = "";
            string engid = "";
            string shape = "";
            double num = 0;
            double fznum = 0;
            double length = 0;
            double width = 0;

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string manid = Session["UserID"].ToString();
            sqltext = "INSERT INTO TBPC_IQRCMPPRCRVW(ICL_SHEETNO,ICL_IQRDATE," +
                       "ICL_REVIEWA) VALUES('" + sheetcode + "','" + time + "','" + manid + "')";
            DBCallCommon.ExeSqlText(sqltext);

            foreach (RepeaterItem Reitem in purchaseplan_Repeater.Items)
            {
                ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text.ToString();
                tuhao = ((Label)Reitem.FindControl("PUR_TUHAO")).Text.ToString();
                marid = ((Label)Reitem.FindControl("PUR_MARID")).Text.ToString();
                length = Convert.ToDouble(((Label)Reitem.FindControl("PUR_LENGTH")).Text.ToString());
                width = Convert.ToDouble(((Label)Reitem.FindControl("PUR_WIDTH")).Text.ToString());
                num = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_NUM")).Text.ToString());
                fznum = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_FZNUM")).Text.ToString());
                sqltext = "select PIC_PJID,PIC_ENGID,PIC_MASHAPE from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptcode + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    pjid = dt.Rows[0]["PIC_PJID"].ToString();
                    engid = dt.Rows[0]["PIC_ENGID"].ToString();
                    shape = dt.Rows[0]["PIC_MASHAPE"].ToString();
                }
                newptcode = BJDptcode(ptcode);
                sqltext = "insert into TBPC_IQRCMPPRICE (PIC_SHEETNO,PIC_PTCODE,PIC_TUHAO,PIC_PJID,PIC_ENGID,PIC_MARID,PIC_MASHAPE,PIC_LENGTH,PIC_WIDTH,PIC_QUANTITY,PIC_FZNUM,PIC_ZXNUM,PIC_ZXFUNUM) " +
                               "values ('" + sheetcode + "','" + newptcode + "','" + tuhao + "','" + pjid + "','" + engid + "','" + marid + "','" + shape + "','" + length + "','" + width + "','" + num + "','" + fznum + "','" + num + "','" + fznum + "')";
                DBCallCommon.ExeSqlText(sqltext);
            }
            Response.Redirect("TBPC_IQRCMPPRCLST_checked.aspx");
        }

        private string BJDno()
        {
            string pi_id = "";
            string tag_pi_id = "FZ";
            string end_pi_id = "";
            string sqltext = "SELECT TOP 1 ICL_SHEETNO FROM TBPC_IQRCMPPRCRVW WHERE ICL_SHEETNO LIKE '" + tag_pi_id + "%' ORDER BY ICL_SHEETNO DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32(dt.Rows[0]["ICL_SHEETNO"].ToString().Substring((dt.Rows[0]["ICL_SHEETNO"].ToString().Length - 6), 6)) + 1);
                end_pi_id = end_pi_id.PadLeft(6, '0');
            }
            else
            {
                end_pi_id = "000001";
            }
            pi_id = tag_pi_id + end_pi_id;
            return pi_id;
        }

        protected string BJDptcode(string ptc)
        {
            string pi_id = "";
            string tag_pi_id = ptc + "@";
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


        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + gloabsheetno + "");
        }
    }
}
