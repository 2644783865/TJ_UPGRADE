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
namespace ZCZJ_DPF.PC_Data.PcJs
{
    public partial class PC_Data_addto_list : System.Web.UI.Page
    {
        public string ptcode_rcode
        {
            get
            {
                object str = ViewState["ptcode_rcode"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["ptcode_rcode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        private void InitInfo()
        {
            if (Request.QueryString["ptcode_rcode"] != null)
            {
                ptcode_rcode = Request.QueryString["ptcode_rcode"].ToString();
            }
            else
            {
                ptcode_rcode = "";
            }
            databind();
        }
        private void databind()
        {
            string sqltext = "";
            string[] Arry;
            Arry = Request.QueryString["ptcode_rcode"].Split(',');

            //string[] stringSeparators = new string[] {"//"};
            //string ptcodercode = ptcode_rcode;
            string providerid = "";
            string ptcode1 = Arry[0].ToString();
            string rcode = Arry[Arry.Length - 1].ToString();//制单人ID
            if (Request.QueryString["zb"] != null)
            {
                sqltext = "SELECT IB_supply from View_TBPC_TOUBIAODETAIL where Pur_ID='" + ptcode1 + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    providerid = dr[0].ToString();
                }
                dr.Close();
            }
            else
            {
                sqltext = "SELECT supplierresid from View_TBPC_IQRCMPPRICE_RVW where PIC_ID='" + ptcode1 + "'";
                SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr2.Read())
                {
                    providerid = dr2[0].ToString();
                }
                dr2.Close();               
            }
            sqltext = "SELECT distinct orderno, supplierid, suppliernm,zdrid, zdrnm,zdtime " +
                         "FROM View_TBPC_PURORDERTOTAL  " +
                         "where zdrid='" + rcode + "' and totalstate='0' and totalcstate='0' and supplierid='" + providerid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btn_concel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }
        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            btn_confirm.Enabled = false;
            string[] Arry;
            Arry = Request.QueryString["ptcode_rcode"].Split(',');

            //string[] stringSeparators = new string[] {"//"};
            //string ptcodercode = ptcode_rcode;
            //string ptcode=ptcodercode.Split(stringSeparators,StringSplitOptions.None)[0].ToString();
            //string rcode = ptcodercode.Split(stringSeparators, StringSplitOptions.None)[1].ToString();
            string orderno="";
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            int temp=checkednum();
            if (temp == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要用来追加的订单！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择其中一个用来追加的订单！');", true);
            }
            else if (temp == 1)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    CheckBox ckb = (CheckBox)gr.FindControl("CheckBox1");
                    if (ckb.Checked)
                    {
                        orderno = gr.Cells[2].Text;
                        break;
                    }
                }
                if (orderno != "")
                {
                    if (Request.QueryString["zb"] != null)
                    {
                        sqltext = "INSERT INTO TBPC_PURORDERDETAIL (PO_CODE, PO_ICLSHEETNO, " +
                                  "PO_PCODE, PO_MARID, PO_LENGTH, PO_WIDTH, PO_QUANTITY, PO_FZNUM," +
                                  "PO_ZXNUM, PO_ZXFZNUM,PO_CTAXUPRICE,PO_TAXRATE," +
                                  "PO_PTCODE,PO_TUHAO,PO_MASHAPE,PO_NOTE,PO_PJID,PO_ENGID) " +
                                  "SELECT '" + orderno + "' AS Expr1,''," +
                                  "ptcode,marid, length, width, " +
                                  "rpnum,rpfznum,rpnum,rpfznum, " +
                                  "price,taxrate,ptcode,Pur_TUHAO,PUR_MASHAPE,IB_NOTE,pjid,engid  " +
                                  "FROM View_TBPC_TOUBIAODETAIL WHERE pur_id in (";
                        for (int i = 0; i < Arry.Length - 2; i++)
                        {
                            sqltext += "'" + Arry[i].ToString() + "',";
                        }
                        sqltext += "'" + Arry[Arry.Length - 2].ToString() + "')";
                        sqltextlist.Add(sqltext);

                        sqltext = "update TBPC_PURORDERTOTAL set PO_ZDDATE='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where PO_CODE='" + orderno + "'";
                        sqltextlist.Add(sqltext);

                        sqltext = "select ptcode from View_TBPC_TOUBIAODETAIL where pur_id in (";
                        for (int i = 0; i < Arry.Length - 2; i++)
                        {
                            sqltext += "'" + Arry[i].ToString() + "',";
                        }
                        sqltext += "'" + Arry[Arry.Length - 2].ToString() + "')";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string ptc = dt.Rows[i]["ptcode"].ToString();
                                sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='7' WHERE PUR_CSTATE='0' and PUR_PTCODE='" + ptc + "'";
                                sqltextlist.Add(sqltext);
                            }

                        }

                        DBCallCommon.ExecuteTrans(sqltextlist);
                    
                    }
                    else
                    {
                        sqltext = "INSERT INTO TBPC_PURORDERDETAIL (PO_CODE, PO_ICLSHEETNO, " +
                                  "PO_PCODE, PO_MARID, PO_LENGTH, PO_WIDTH, PO_QUANTITY, PO_FZNUM," +
                                  "PO_ZXNUM, PO_ZXFZNUM,PO_CTAXUPRICE,PO_TAXRATE," +
                                  "PO_PMODE, PO_KEYCOMS, PO_PTCODE,PO_TUHAO,PO_MASHAPE,PO_NOTE,PO_PJID,PO_ENGID,PO_IFFAST) " +
                                  "SELECT '" + orderno + "' AS Expr1,PIC_SHEETNO," +
                                  "PIC_PTCODE,PIC_MARID, PIC_LENGTH, PIC_WIDTH, " +
                                  "PIC_ZXNUM,PIC_ZXFUNUM,PIC_ZXNUM,PIC_ZXFUNUM, " +
                                  "PIC_PRICE,PIC_SHUILV,PIC_PMODE,PIC_KEYCOMS,PIC_PTCODE,PIC_TUHAO,PIC_MASHAPE,PIC_NOTE,PIC_PJID,PIC_ENGID,PIC_IFFAST  " +
                                  "FROM TBPC_IQRCMPPRICE WHERE PIC_ID in (";
                        for (int i = 0; i < Arry.Length - 2; i++)
                        {
                            sqltext += "'" + Arry[i].ToString() + "',";
                        }
                        sqltext += "'" + Arry[Arry.Length - 2].ToString() + "')";
                        sqltextlist.Add(sqltext);

                        sqltext = "update TBPC_PURORDERTOTAL set PO_ZDDATE='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where PO_CODE='" + orderno + "'";
                        sqltextlist.Add(sqltext);

                        sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='1' where  PIC_STATE='0' and PIC_ID in (";//生成订单
                        for (int i = 0; i < Arry.Length - 2; i++)
                        {
                            sqltext += "'" + Arry[i].ToString() + "',";
                        }
                        sqltext += "'" + Arry[Arry.Length - 2].ToString() + "')";
                        sqltextlist.Add(sqltext);

                        sqltext = "select PIC_PTCODE from TBPC_IQRCMPPRICE where PIC_ID in (";
                        for (int i = 0; i < Arry.Length - 2; i++)
                        {
                            sqltext += "'" + Arry[i].ToString() + "',";
                        }
                        sqltext += "'" + Arry[Arry.Length - 2].ToString() + "')";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string ptc = dt.Rows[i]["PIC_PTCODE"].ToString();
                                sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='7' WHERE PUR_CSTATE='0' and PUR_PTCODE='" + ptc + "'";
                                sqltextlist.Add(sqltext);
                            }

                        }

                        DBCallCommon.ExecuteTrans(sqltextlist);
                    }
                   
                }
                //Response.Write("<script>javascript:window.close();</script>");
                Response.Redirect("PC_TBPC_PurOrder.aspx?orderno=" + orderno + "");
            }
        }
        protected int checkednum()
        {
            int temp=0;
            int j = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox ckb = (CheckBox)gr.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    j++;
                }
            }
            if (j == 1)
            {
                temp = 1;//选择一共订单
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
