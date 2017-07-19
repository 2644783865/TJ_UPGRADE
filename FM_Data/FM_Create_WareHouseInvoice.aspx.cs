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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_Create_WareHouseInvoice : System.Web.UI.Page
    {

        string arrayWG_CODE = "";
        string bindArrayWG_CODE = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            arrayWG_CODE = Request.QueryString["arrayWG_CODE"];
            //传递到下一页面的入库单号
            bindArrayWG_CODE = arrayWG_CODE;
            //当前页面查询的入库单号
            string  arrayWG_CODE_Page="('"+arrayWG_CODE.Replace("/", "','")+"')";
            Page.DataBind();
            if (!IsPostBack)
            {
                this.BindData(arrayWG_CODE_Page);
            }
        }
        //绑定属性，传递到下一页面
        public string ArrayWGCode
        {
            get { return bindArrayWG_CODE; }
            set { bindArrayWG_CODE = value; }
        }
        /// <summary>
        /// 当前页面绑定数据
        /// </summary>
        /// <param name="id"></param>

        private void BindData(string id)
        {
            string sqltext = "select DISTINCT(WG_CODE),(case when WG_BILLTYPE='3' then WG_COMPANY else SupplierName end) as WG_SUPPLIERNM,WG_DATE,DepName as WG_DEPNM,ClerkName as WG_BSMANNM,DocName as WG_ZDNM,ReveicerName as WG_RMMANNM from View_SM_IN a where a.WG_CODE in" + id + "";
           grvRK.DataSource = DBCallCommon.GetDTUsingSqlText(sqltext);
           grvRK.DataBind();
        }

        //创建汇总发票
        protected void btnCreatInv_Click(object sender, EventArgs e)
        {
            string  fpdh="FP" + DateTime.Now.ToString("yyyyMMddHHmmss");
            this.CreateInvOpDataBase(fpdh);
            this.ClientScript.RegisterStartupScript(GetType(), "js", "location.href ='FM_Invoice.aspx?Action=Audit&GI_CODE=" + fpdh + "';", true);

            //this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:alert('发票已创建，请审核！！！');location.href ='FM_Invoice.aspx?Action=Audit&GI_CODE="+fpdh+"';", true);
        }

        /// <summary>
        /// 创建发票,写入数据表中
        /// </summary>
        private void CreateInvOpDataBase(string fpdh)
        {
            List<string> sql = new List<string>();
            //发票编号
            string gi_code = fpdh;
            //入库单号数组
            string[] arrayCode = arrayWG_CODE.Split('/'); 
            //购货发票总表(TBFM_GHINVOICETOTAL)--发票总表

            sql.Add(this.CreateInvTOTAL(arrayCode[0],gi_code));
            
            for (int i = 0; i < arrayCode.Length; i++)
            {
                //第i个单号
                string wg_code = arrayCode[i].ToString();

                //判断是否有下推发票但未勾稽
                string sql1 = " select a.GI_GJFLAG from TBFM_GHINVOICETOTAL a ,TBFM_GJRELATION b where a.GI_CODE=b.GJ_INVOICEID and b.GI_INSTOREID='" + wg_code + "' and  a.GI_GJFLAG='0' ";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                if (dt1.Rows.Count > 0)
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:alert('此入库单下有未勾稽的发票，请先勾稽完！');", true);

                    return;
                }
                
                //入库总表，更改入库单的勾稽状态
                sql.Add(this.CreateWGTotal(wg_code));

                //勾稽关系表，插入勾稽关系信息
                sql.Add(this.CreateGJ(wg_code,gi_code));  
                              
                //第i个入库单下的明细，发票明细
                //购货发票明细表(TBFM_GHINVOICEDETAIL)

                //生产发票明细，与入库单一样
                this.CreateInvDetail(sql,wg_code, gi_code);
                
            }
            //根据发票明细上的金额更新发票总表(总金额、含税金额)
            string str_je = "update TBFM_GHINVOICETOTAL set GI_AMTMNY=(select sum(GI_AMTMNY) from TBFM_GHINVOICEDETAIL where GI_CODE='" +gi_code+ "') where GI_CODE='"+gi_code+"'";
            string str_je_hs = "update TBFM_GHINVOICETOTAL set GI_CTAMTMNY=(select sum(GI_CTAMTMNY) from TBFM_GHINVOICEDETAIL where GI_CODE='" + gi_code + "') where GI_CODE='" + gi_code + "'";
            sql.Add(str_je);
            sql.Add(str_je_hs);
            DBCallCommon.ExecuteTrans(sql);
        }
        #region
        /// <summary>
        /// 购货发票总表-SqlText
        /// </summary>
        /// <param name="wgcode">入库单号</param>
        /// <param name="gicode">发票编号</param>
        /// <returns></returns>
        private string CreateInvTOTAL(string wgcode,string gicode)
        {
            string sqltext = "";
            //编号
            string  gi_code= gicode;

            //供应商
            string gi_supplierid ="";
            //供应商名称
            string gi_suppliernm = "";
            //核销标志 
            string gi_cavflag = "";
            //红蓝字
            string gi_rob = "";
            string sql_wgcode = "select Supplier as WG_SUPPLIERID,(case when WG_BILLTYPE='3' then WG_COMPANY else SupplierName end) as WG_SUPPLIERNM,WG_CAVFLAG,WG_ROB from View_SM_IN where WG_CODE='" + wgcode + "'";
            SqlDataReader dr=DBCallCommon.GetDRUsingSqlText(sql_wgcode);
            if(dr.HasRows)
            {
                dr.Read();
                //供应商
                gi_supplierid = dr["WG_SUPPLIERID"].ToString();
                //供应商名称
                gi_suppliernm =dr["WG_SUPPLIERNM"].ToString();
                //核销标志 
                gi_cavflag = dr["WG_CAVFLAG"].ToString();
                //红蓝字
                gi_rob = dr["WG_ROB"].ToString();
            }
            dr.Close();
            //开户银行
            string gi_accbank = "";
            //发票号码
            string gi_invoiceno = "";
            //地址
            string gi_address = "";

            //日期-创建发票的日期
            //此日期需要根据系统是否关帐来判断发票日期
            string gi_date = string.Empty;
            string sql = "select count(*) from TBFM_HSTOTAL where HS_YEAR='" + System.DateTime.Now.Year.ToString() + "' and HS_MONTH='" + System.DateTime.Now.Month.ToString().PadLeft(2,'0') + "' and HS_STATE='2'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                gi_date = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
            }
            else
            {
                gi_date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            //勾稽标志
            string gi_gjflag = "0";
            //凭证号
            string  gi_pzh="";
            //部门 
            string gi_depid = "";
            //部门名称
            string  gi_depnm="财务部";
            //记账人
            string  gi_jzid="";
            //记账人姓名
            string  gi_jznm="";
            //制单人
            string  gi_zdid="";
            //制单人姓名
            string  gi_zdnm="";
            //状态
            string  gi_state="0";//审核状态
            //备注
            string  gi_note="";
            //总金额	
            string gi_amtmny = "0"; 
            //含税总金额	GI_CTAMTMNY
            string gi_ctamtmny="0";
            sqltext = "insert into TBFM_GHINVOICETOTAL(GI_CODE,GI_SUPPLIERID,GI_SUPPLIERNM,GI_ACCBANK,GI_INVOICENO,GI_ADDRESS,GI_DATE,GI_ROB,GI_GJFLAG,GI_CAVFLAG,GI_PZH,GI_DEPID,GI_DEPNM,GI_JZID,GI_JZNM,GI_ZDID,GI_ZDNM,GI_STATE,GI_NOTE,GI_AMTMNY,GI_CTAMTMNY)" +
                "Values('" + gi_code + "','" + gi_supplierid + "','" + gi_suppliernm + "','" + gi_accbank + "','" + gi_invoiceno + "','" + gi_address + "','" + gi_date + "','" + gi_rob + "','" + gi_gjflag + "','" + gi_cavflag + "','" + gi_pzh + "','" + gi_depid + "','" + gi_depnm + "','" + gi_jzid + "','" + gi_jznm + "','" + gi_zdid + "','" + gi_zdnm + "','" + gi_state + "','" + gi_note + "',"+gi_amtmny+","+gi_ctamtmny+")";
            return sqltext;
        }
        /// <summary>
        /// 更新外购入库总表(TBWS_INSTWGTOTAL)中的钩稽状态：0未下推发票、1：已下推发票、2：已钩稽
        /// </summary>
        /// <param name="wgcoge"></param>
        /// <returns></returns>
        private string CreateWGTotal(string wgcoge)
        {
            string sqltext = "update TBWS_IN set WG_GJSTATE='1' where WG_CODE='"+wgcoge+"'";
            return sqltext;
        }
        /// <summary>
        /// 勾稽关系表-SqlText
        /// </summary>
        /// <param name="rkdh">入库单号</param>
        /// <param name="fpdh">发票编号</param>
        /// <returns></returns>
        private string CreateGJ(string rkdh, string fpdh)
        {
            string sqltext = "insert into TBFM_GJRELATION(GI_INSTOREID,GJ_INVOICEID) values('" + rkdh + "','" + fpdh + "')";
            return sqltext;
        }
        /// <summary>
        /// 购货发票明细表-SqlText
        /// </summary>
        /// <param name="li"></param>
        /// <param name="wgcode"></param>
        /// <returns></returns>
        private void CreateInvDetail(List<string> li, string wgcode,string gicode)
        {
            string sqltext = "insert into TBFM_GHINVOICEDETAIL( GI_CODE,GI_UNICODE,GI_MATCODE,GI_NAME,GI_GUIGE,GI_UNIT,GI_NUM,GI_UNITPRICE,GI_TAXRATE,GI_CTAXUPRICE,GI_AMTMNY,GI_CTAMTMNY,GI_PMODE,GI_PTCODE,GI_ORDERID,GI_INCOED) " +
                       " SELECT '" + gicode + "',WG_UNIQUEID,WG_MARID,MNAME,GUIGE,CGDW,(WG_RSNUM-isnull(WG_GJNUM,0)),WG_UPRICE,WG_TAXRATE,WG_CTAXUPRICE,(WG_AMOUNT-isnull(WG_GJMONY,0)),(WG_CTAMTMNY-ISNULL(WG_GJCVMONY,0)),WG_PMODE,WG_PTCODE,WG_ORDERID,WG_CODE FROM View_SM_IN where WG_CODE='" + wgcode + "' and (isnull(WG_GJFLAG,0)='0' or isnull(WG_GJFLAG,0)='1') ";
            li.Add(sqltext);
        }
      
        #endregion
    }
}
