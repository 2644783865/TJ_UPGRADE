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
using System.Data.SqlClient;
namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Data_zhuijiaBJD : System.Web.UI.Page
    {
        public string globeptcode
        {
            get
            {
                object str = ViewState["globeptcode"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["globeptcode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
                databind();
            }
        }
        private void InitInfo()
        {
            if (Request.QueryString["ptcode"] != null)
            {
                globeptcode = Request.QueryString["ptcode"].ToString();
            }
            else
            {
                globeptcode = "";
            }
        }

        private void databind()
        {
            string sqltext = "";
            string Zid=Session["UserID"].ToString();
            sqltext = "select picno,irqdata,zdrnm,zdrid,totalstate,totalnote from View_TBPC_IQRCMPPRCRVW where zdrid='" + Zid + "' and totalstate='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            purchaseplan_Repeater.DataSource = dt;
            purchaseplan_Repeater.DataBind();
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            string ghs1 = "";
            string ghs2 = "";
            string ghs3 = "";
            string ghs4 = "";
            string ghs5 = "";
            string ghs6 = "";
            string[] Arry;
            Arry = globeptcode.Split(',');
            string sqltext = "";
            int temp = checkednum();
            string PICNO = "";
            if (temp == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要用来追加的比价单！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择其中一个比价单！');", true);
            }
            else
            {
                foreach (RepeaterItem Reitem in purchaseplan_Repeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        PICNO = ((Label)Reitem.FindControl("picno")).Text;
                        break;
                    }
                }
                sqltext = "select  PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + PICNO + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    ghs1 = dt.Rows[0]["PIC_SUPPLIERAID"].ToString();
                    ghs2 = dt.Rows[0]["PIC_SUPPLIERBID"].ToString();
                    ghs3 = dt.Rows[0]["PIC_SUPPLIERCID"].ToString();
                    ghs4 = dt.Rows[0]["PIC_SUPPLIERDID"].ToString();
                    ghs5 = dt.Rows[0]["PIC_SUPPLIEREID"].ToString();
                    ghs6 = dt.Rows[0]["PIC_SUPPLIERFID"].ToString();
                }
                sqltext = "INSERT INTO TBPC_IQRCMPPRICE (PIC_SHEETNO, PIC_PTCODE, " +
                             "PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_MARID, PIC_MASHAPE, PIC_LENGTH," +
                             "PIC_WIDTH, PIC_QUANTITY,PIC_FZNUM,PIC_ZXNUM," +
                             "PIC_ZXFUNUM, PIC_ZDJNUM, PIC_KEYCOMS,PIC_NOTE,PIC_TECUNIT,PIC_SQRID,PIC_PICODE,PIC_IFFAST) " +
                             "SELECT '" + PICNO + "' AS Expr1,ptcode," +
                             "PUR_TUHAO,pjid, engid, marid, " +
                             "PUR_MASHAPE,length,width,rpnum, " +
                             //"rpfznum,rpnum,rpfznum,case when (charindex('_WX',ptcode)>0) then rpfznum else 0 end as zdjnum,keycoms,purnote  " +
                             "rpfznum,rpnum,rpfznum,rpfznum,keycoms,purnote,marfzunit,sqrid,planno,PUR_IFFAST  " +
                             "FROM View_TBPC_PURCHASEPLAN_RVW WHERE PUR_CSTATE='0' and  PUR_ID in (";
                for (int i = 0; i < Arry.Length - 1; i++)
                {
                    sqltext += "'" + Arry[i].ToString() + "',";
                }
                sqltext += "'" + Arry[Arry.Length - 1].ToString() + "')";
                sqltextlist.Add(sqltext);
                sqltext = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERAID='" + ghs1 + "',PIC_SUPPLIERBID='" + ghs2 + "',PIC_SUPPLIERCID='" + ghs3 + "',PIC_SUPPLIERDID='" + ghs4 + "',PIC_SUPPLIEREID='" + ghs5 + "',PIC_SUPPLIERFID='" + ghs6 + "' where PIC_SHEETNO='" + PICNO + "'";
                sqltextlist.Add(sqltext);
                

                sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='6' WHERE PUR_CSTATE='0' and " +
                              "PUR_ID in (";//生成比价单
                for (int i = 0; i < Arry.Length - 1; i++)
                {
                    sqltext += "'" + Arry[i].ToString() + "',";
                }
                sqltext += "'" + Arry[Arry.Length - 1].ToString() + "')";
                sqltextlist.Add(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_compareprice.aspx?sheetno=" + PICNO);
            }
        }

        protected int checkednum()
        {
            int temp = 0;
            int j = 0;
            foreach (RepeaterItem Reitem in purchaseplan_Repeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    j++;
                }
            }
            if (j == 1)
            {
                temp = 1;//选择一个订单
            }
            else if (j > 1)
            {
                temp = 2;//选择了超过一个订单
            }
            else
            {
                temp = 0;//没选择订单
            }
            return temp;
        }
    }
}
