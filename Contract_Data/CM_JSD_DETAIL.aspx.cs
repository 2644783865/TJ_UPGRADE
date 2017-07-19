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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_JSD_DETAIL : System.Web.UI.Page
    {
        string htbh = "";
        string action = "";
        double jsje = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["condetail_id"] != null)
            {
                htbh = Request.QueryString["condetail_id"]; // 合同编号    
            }
            if (Request.QueryString["Action"] != null)
            {
                action = Request.QueryString["Action"].ToString();
            }

            
            if (!IsPostBack)
            {
                BindGV();
                initpage();
            }
            
           // Page.DataBind();
        }

        public string CondetailID
        {
            get
            {
                return lbl_HTBH.Text;
            }
        }

        private void initpage()
        {            
            //绑定合同信息
            BindConInfo();
        }

        private void BindGV()
        {
            DataTable DT = GetDetailData();
            if (DT.Rows.Count > 0)
            {
                grvjsd.DataSource = DT;
                grvjsd.DataBind();
            }
            else
            {
                grvjsd.DataSource = null;
                grvjsd.EmptyDataText = "没有数据";
            }

            
        }

        private void BindConInfo()
        {
            string sql = "";            
            string jsdate = "";            
            string bankname = "";//开户行
            string bankaccount = "";//银行帐号 
            
            if (action == "add") //添加
            {
                
                string sqladd = "select CS_BANK,CS_ACCOUNT from TBPM_CONPCHSINFO AS A LEFT OUTER JOIN TBCS_CUSUPINFO AS B ON A.PCON_CUSTMID=B.CS_CODE where PCON_BCODE='" + htbh + "'";
                DataTable dtadd = DBCallCommon.GetDTUsingSqlText(sqladd);
                if (dtadd.Rows.Count > 0)
                {                    
                    bankname = dtadd.Rows[0][0].ToString(); 
                    bankaccount = dtadd.Rows[0][1].ToString();
                }
                jsdate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else  //读取
            {
                string sqljsdetail = " select * from TBCM_JSDDETAIL where conid='" + htbh + "' ";
                DataTable dtjsdetail = DBCallCommon.GetDTUsingSqlText(sqljsdetail);
                if (dtjsdetail.Rows.Count > 0)
                { 
                    jsdate = dtjsdetail.Rows[0]["JSDDATE"].ToString();//结算日期                    
                    bankname = dtjsdetail.Rows[0]["DEPOSITBANK"].ToString();
                    bankaccount = dtjsdetail.Rows[0]["BANKACUNUM"].ToString();                   
                }

                if (action == "view")
                {
                    btn_Save.Visible = false;                     
                }
            }
            
            sql = "select  PCON_NAME,";
            sql += " PCON_BCODE,PCON_CUSTMNAME,'" + bankname + "' as PCON_DEPOSITBANK,'" + bankaccount + "' as PCON_BANKACUNUM,PCON_BALANCEACNT,";
            sql += " dbo.L2U(" + Convert.ToDouble(ViewState["jsje"].ToString()) + ",1) as JSJEDX from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (dt.Rows.Count > 0)
            {
                lbl_HTMC.Text = dt.Rows[0]["PCON_NAME"].ToString(); //合同名称
                lbl_HTBH.Text = dt.Rows[0]["PCON_BCODE"].ToString(); //合同编号
                lbl_SUPNAME.Text = dt.Rows[0]["PCON_CUSTMNAME"].ToString();  //供货商名称
                tb_khh.Text = dt.Rows[0]["PCON_DEPOSITBANK"].ToString();   //开户行
                tb_zh.Text = dt.Rows[0]["PCON_BANKACUNUM"].ToString();    // 帐号
                lbl_JSDATE.Text = jsdate;     //结算日期
                
                lbl_JSJE.Text = string.Format("{0:c}", Convert.ToDouble(ViewState["jsje"].ToString())); // 结算金额
                lbl_JSJEDX.Text = dt.Rows[0]["JSJEDX"].ToString();    //结算金额大写               
            }
          
        }

        //入库明细
        private DataTable GetDetailData()
        {
            DataTable dt_jsdetail = null;
            if (action == "add")
            {
                string b = " ";

                string sql = " select PCON_ORDERID from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "'  ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    string[] a = dt.Rows[0]["PCON_ORDERID"].ToString().Split(',');
                    for (int i = 0; i <= a.Length - 1; i++)
                    {
                        string c = "'" + a[i].ToString() + "'" + ",";
                        b += c;
                    }
                    b = b.Substring(0, b.Length - 1);
                    sql = " select marid,marnm,margg,margb,marcz,marunit,convert(float,zxnum) as zxnum ,convert(float,recgdnum) as recgdnum,convert(float,ctprice) as ctprice,convert(float,ctamount) as ctamount ,convert(float,recgdnum*ctprice) as sjamount,0 as balance,orderno as bezhu ,0 as addnum";
                    sql += "  from View_TBPC_PURORDERDETAIL_PLAN  where orderno in (" + b + ") ";
                    sql += "  order by orderno";
                    dt_jsdetail = DBCallCommon.GetDTUsingSqlText(sql);
                   
                }

            }
            else
            {
                string sqltext = "select a.marid,b.mname as marnm,b.guige as margg,b.caizhi as marcz,b.gb as margb,b.purcunit as marunit,"+
                                 " a.htnum as zxnum, a.htunitprice as ctprice,a.htsummoney as ctamount,a.inputnum as recgdnum,a.jsmoney as sjamount,a.balance,a.note as bezhu ,a.addnum"+
                                 " from TBCM_JSDMARINFO as a,TBMA_MATERIAL as b where a.marid=b.id and conid='" + htbh + "'";
                dt_jsdetail = DBCallCommon.GetDTUsingSqlText(sqltext);
            }

            if (dt_jsdetail.Rows.Count > 0)
            {
                for (int i = 0; i <= dt_jsdetail.Rows.Count - 1; i++)
                {

                    jsje +=Convert.ToDouble(dt_jsdetail.Rows[i]["sjamount"].ToString());
                    jsje +=Convert.ToDouble(dt_jsdetail.Rows[i]["balance"].ToString());
                }
                jsje = Math.Round(jsje, 2);
            }
            ViewState["jsje"] = jsje.ToString();//结算金额为调整后的金额

            return dt_jsdetail;
        }                 

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            btn_Save.Visible = false;
            string time = lbl_JSDATE.Text;//日期            
            string khh = tb_khh.Text.Trim();//开户行
            string zh = tb_zh.Text.Trim();//帐号
            
            List<string> sqlstr = new List<string>();

            string sql = "";
            string sql2 = "";
            string sql3 = "";
            
            if (grvjsd.Rows.Count > 0)
            {

                if (action == "add")
                {                   
                    sql = " insert into TBCM_JSDDETAIL(CONID,JSDDATE,DEPOSITBANK,BANKACUNUM) " +
                                 " values('" + htbh + "','" + time + "','" + khh + "','" + zh + "') ";

                    sqlstr.Add(sql);

                }
                else if (action == "edit")
                {
                    sql = "update TBCM_JSDDETAIL set DEPOSITBANK='" + khh + "',BANKACUNUM='" + zh + "' where CONID='" + htbh + "'";
                    sql2 = "delete from TBCM_JSDMARINFO where CONID='" + htbh + "'"; //更新之前先删除，再插入新记录
                    sqlstr.Add(sql);
                    sqlstr.Add(sql2);
                }
                for (int i = 0; i < grvjsd.Rows.Count; i++)
                {                    
                    string marid = ((HiddenField)grvjsd.Rows[i].FindControl("hdmarid")).Value.Trim();//物料编码
                    double htnum = Convert.ToDouble(((HiddenField)grvjsd.Rows[i].FindControl("hdhtnum")).Value.Trim());//合同数量
                    double htunitprice = Convert.ToDouble(((HiddenField)grvjsd.Rows[i].FindControl("hdhtunitprice")).Value.Trim());//合同单价
                    double htsummoney = Convert.ToDouble(((HiddenField)grvjsd.Rows[i].FindControl("hdhtsummoney")).Value.Trim());//合同金额
                    double inputnum = Convert.ToDouble(((HiddenField)grvjsd.Rows[i].FindControl("hdinputnum")).Value.Trim());//实际供货数量
                    double jsmoney = Convert.ToDouble(((HiddenField)grvjsd.Rows[i].FindControl("hdjsmoney")).Value.Trim());//实际结算金额
                    double balance = Convert.ToDouble(((TextBox)grvjsd.Rows[i].FindControl("tb_balance")).Text.Trim());//差额
                    string note = ((TextBox)grvjsd.Rows[i].FindControl("tb_note")).Text.Trim();//备注——订单号

                    double addnum = Convert.ToDouble(((TextBox)grvjsd.Rows[i].FindControl("tb_addnum")).Text.Trim());//结算数量调整
                    
                    sql3 = "INSERT INTO TBCM_JSDMARINFO (CONID,MARID,HTNUM,HTUNITPRICE,HTSUMMONEY,INPUTNUM,JSMONEY,BALANCE,NOTE,ADDNUM)" +
                               " VALUES ('" + htbh + "','" + marid + "'," + htnum + "," + htunitprice + "," + htsummoney + "," + inputnum + "," + jsmoney + "," + balance + ",'" + note + "','" + addnum + "')";                   
                   
                    sqlstr.Add(sql3);
                }
                DBCallCommon.ExecuteTrans(sqlstr);
                //刷新
                BindGV();
                initpage();
                //保存的时候将调整后的结算金额update到合同表中，以保证两者一致
                UpdateJSJEInCon();
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.opener.location.reload();", true); return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('没有可以添加的内容！');", true);
            }
                       
        }

        //更新合同表中的结算金额为现在调整过的金额
        private void UpdateJSJEInCon()
        {
            List<string> sqlstr = new List<string>();
            string sql = "";
            double jsje = Convert.ToDouble(ViewState["jsje"].ToString());//结算金额
            sql = "UPDATE TBPM_CONPCHSINFO SET PCON_BALANCEACNT=" + jsje + " WHERE PCON_BCODE='" + htbh + "'";
            sqlstr.Add(sql);
            DBCallCommon.ExecuteTrans(sqlstr);
        }
       
    }
}
