using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Data_addjs_list : System.Web.UI.Page
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
            string providerid = "";
            string ptcode1 = Arry[0].ToString();
            string rcode = Arry[Arry.Length - 1].ToString();//制单人ID

            sqltext = "SELECT TO_SUPPLYID from View_TBMP_Order where TO_ID='" + ptcode1 + "'";
            SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr2.Read())
            {
                providerid = dr2[0].ToString();
            }
            dr2.Close();
            sqltext = "SELECT distinct TA_DOCNUM, TA_SUPPLYID, TA_SUPPLYNAME,TA_ZDR, TA_ZDRNAME,TA_ZDTIME " +
                     "FROM View_TBMP_ACCOUNTS  " +
                     "where TA_ZDR='" + rcode + "'  and TA_SUPPLYID='" + providerid + "'";
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
            string orderno = "";
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            int temp = checkednum();
            if (temp == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要用来追加的订单！');", true);
                return;
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择其中一个用来追加的订单！');", true);
                return;
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


                string sql = "select TA_ZDTIME from TBMP_ACCOUNTS where TA_DOCNUM='" + orderno+"'";
                DataTable dt0=DBCallCommon.GetDTUsingSqlText(sql);
                string zdtime = dt0.Rows[0]["TA_ZDTIME"].ToString().Trim();
                string zddate = zdtime.Substring(0, 7).ToString();
                string sqlwxhs = "select * from TBFM_WXHS where WXHS_STATE='1' and WXHS_YEAR+'-'+WXHS_MONTH like '" + zddate + "%'";
                DataTable dtwxhs = DBCallCommon.GetDTUsingSqlText(sqlwxhs);
                if (dtwxhs.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该结算单已核算，不能再追加！');", true);
                    return;
                }

                else
                {
                    if (orderno != "")
                    {
                        //sqltext = "INSERT INTO TBMP_ACCOUNTS (TA_DOCNUM, TA_ORDERNUM, TA_PTC, TA_ZDR,TA_ZDTIME,TA_NUM,TA_MONEY,TA_WGHT) select '" + orderno + "',TO_DOCNUM, TO_PTC,'" + Session["UserID"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "',PIC_ZXNUM,CAST(PIC_PRICE * PIC_ZXNUM AS decimal(18 , 4)),CAST(TO_UWGHT * PIC_ZXNUM AS decimal(18 , 4)) from VIEW_TBMP_Order  WHERE TO_ID in(";
                        sqltext = "INSERT INTO TBMP_ACCOUNTS (TA_DOCNUM, TA_ORDERNUM, TA_PTC, TA_ZDR,TA_ZDTIME,TA_NUM,TA_MONEY,TA_WGHT) select '" + orderno + "',TO_DOCNUM, TO_PTC,'" + Session["UserID"].ToString() + "','" + zdtime + "',PIC_ZXNUM,CAST(PIC_PRICE * PIC_ZXNUM AS decimal(18 , 4)),CAST(TO_UWGHT * PIC_ZXNUM AS decimal(18 , 4)) from VIEW_TBMP_Order  WHERE TO_ID in(";
                        for (int i = 0; i < Arry.Length - 2; i++)
                        {
                            sqltext += "'" + Arry[i].ToString() + "',";
                        }
                        sqltext += "'" + Arry[Arry.Length - 2].ToString() + "')";
                        sqltextlist.Add(sqltext);

                        //sqltext = "update TBMP_ACCOUNTS set TA_ZDTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where TA_DOCNUM='" + orderno + "'";
                        //sqltextlist.Add(sqltext);

                        sqltext = "update TBMP_Order set TO_STATE='1' where TO_ID in (";//生成订单
                        for (int i = 0; i < Arry.Length - 2; i++)
                        {
                            sqltext += "'" + Arry[i].ToString() + "',";
                        }
                        sqltext += "'" + Arry[Arry.Length - 2].ToString() + "')";
                        sqltextlist.Add(sqltext);
                        DBCallCommon.ExecuteTrans(sqltextlist);
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('追加订单成功！');window.opener.location.replace('PM_Xie_IntoAccounts.aspx?orderno=" + orderno + "');self.close()", true);
                    // Response.Redirect("TBPM_IntoOrder.aspx?orderno=" + orderno + "");
                }
            }
        }
        protected int checkednum()
        {
            int temp = 0;
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
