using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_RelatedDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LabelPTC.Text = Request.QueryString["PTC"].ToString();

                GetHZ();

                //GetPlan();

                GetZY();
                GetXSDY();
                GetCMP();
                GetOrder();
                GetIn();
                GetOut();
                GetMTO();
                //GetAL();
                GetInvoice();
            }
        }

        //汇总
        protected void GetHZ()
        {
            string ptc = Request.QueryString["PTC"].ToString();

            string sql = "select * from dbo.MS_HZBYPTC ('" + ptc + "')";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterHZ.DataSource = dt;
            RepeaterHZ.DataBind();

        }

        //物料需要计划
        protected void GetPlan()
        {
            string ptc = Request.QueryString["PTC"].ToString();
            string sql = "SELECT planno AS PH,pjnm AS Project,engnm AS Eng," +
                "depnm AS Dep,sqrnm AS Applicant,sqrtime AS Date,ptcode AS PTC," +
                "marid AS MaterialCode,marnm AS MaterialName,margg AS MaterialStandard," +
                "marunit AS Unit,B.num AS Num,B.fznum AS Weight FROM View_TBPC_PURCHASEPLAN_RVW AS A INNER JOIN View_TBPC_Newmarplan AS B ON A.ptcode=B.MP_TRACKNUM " +
                "WHERE ptcode='" + ptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterPlan.DataSource = dt;
            RepeaterPlan.DataBind();
        }

        //占用
        protected void GetZY()
        {
            string ptc = Request.QueryString["PTC"].ToString();
            string sql = "SELECT planno,pjnm,engnm,ptcode,marid,marnm,margg,length,width,marunit,num,usenum FROM View_TBPC_MARSTOUSEALL " +
                "WHERE ptcode='" + ptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterZY.DataSource = dt;
            RepeaterZY.DataBind();
        }

        //相似代用
        //select * from View_TBPC_MARREPLACE_all where  substring(mpcode,5,1)='0'

        protected void GetXSDY()
        {
            string ptc = Request.QueryString["PTC"].ToString();

            string sql = "SELECT mpcode, ptcode,pjnm,engnm,ptcode,marid,marnm,marguige,length,width,marcgunit,num,usenum FROM View_TBPC_MARREPLACE_all_total " +
                "WHERE ptcode='" + ptc + "'and substring(mpcode,5,1)='0'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterXSDY.DataSource = dt;
            RepeaterXSDY.DataBind();
        }

        //询比价单
        protected void GetCMP()
        {
            string ptc = Request.QueryString["PTC"].ToString();
            string sql = "SELECT picno AS Code,pjnm AS Project,zdrnm AS Doc," +
                "zdtime AS Date,totalstate AS CMPState,ptcode AS PTC,marid AS MaterialCode," +
                "marnm AS MaterialName,margg AS MaterialStandard,marunit AS Unit," +
                "num AS Number,detailstate AS State FROM View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE ptcode='" + ptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterCMP.DataSource = dt;
            RepeaterCMP.DataBind();
        }

        protected void GetOrder()
        {
            string ptc = Request.QueryString["PTC"].ToString();
            string sql = "SELECT orderno AS Code,supplierid AS SupplierCode,suppliernm AS Supplier,shtime AS Date," +
                    "ywyid AS ClerkCode,ywynm AS Clerk,depid AS DepCode,depnm AS Dep,totalstate AS OrderState," +
                    "marid AS MaterialCode,marnm AS MaterialName,marcz AS Attribute,margb AS GB," +
                    "margg AS MaterialStandard,marunit AS Unit,zxnum AS Number,recgdnum AS ArrivedNumber,recdate AS ArrivedDate," +
                    "price AS UnitPrice,taxrate AS TaxRate,ctprice AS CTUP,amount AS Amount,ctamount AS CTA," +
                    "planmode AS PlanMode,ptcode AS PTC,detailstate AS ItemState " +
                    " FROM View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE ptcode='" + ptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterOrder.DataSource = dt;
            RepeaterOrder.DataBind();
        }

        protected void GetIn()
        {
            string ptc = Request.QueryString["PTC"].ToString();
            string sql = "SELECT WG_CODE AS Code,SupplierName AS Supplier," +
                    "WG_WAREHOUSE AS Warehouse,WG_DATE AS Date,DepName AS Dep,ClerkName AS Clerk," +
                    "WG_STATE AS State,WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                    "GUIGE AS MaterialStandard,CAIZHI AS Attribute,CGDW AS Unit,WG_RSNUM AS RN," +
                    "WG_UPRICE AS UnitPrice,WG_AMOUNT AS Amount,WG_PTCODE AS PTC " +
                    "FROM View_SM_IN WHERE WG_PTCODE='" + ptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterIn.DataSource = dt;
            RepeaterIn.DataBind();
        }

        protected void GetOut()
        {
            string ptc = Request.QueryString["PTC"].ToString();
            string sql = "SELECT OutCode AS Code,Dep AS Dep,Warehouse AS Warehouse," +
                    "Date AS Date,TotalState AS State,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard," +
                    "Unit AS Unit,UnitPrice AS UnitPrice,DueNumber AS DN,RealNumber AS RN," +
                    "Amount AS Amount,PTC AS PTC FROM View_SM_OUT WHERE PTC='" + ptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterOut.DataSource = dt;
            RepeaterOut.DataBind();
        }

        protected void GetMTO()
        {
            string ptc = Request.QueryString["PTC"].ToString();
            string sql = "SELECT MTOCode AS Code,Date AS Date,Dep AS Dep,Planer AS Planer,Doc AS Document," +
                    "Verifier AS Verifier,VerifyDate AS ApproveDate,TotalState AS State,TotalNote AS Comment,UniqueCode AS UniqueID," +
                    "MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard," +
                    "Attribute AS Attribute,LotNumber AS LotNumber,PTCFrom AS PTCFrom,Warehouse AS Warehouse,Location AS Position,Unit AS Unit," +
                    "KTNUM AS WN,TZNUM AS AdjN,PTCTo AS PTCTo,DetailState AS StateD,DetailNote AS Note " +
                    "FROM View_SM_MTO WHERE PTCFROM='" + ptc + "' OR PTCTo='" + ptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterMTO.DataSource = dt;
            RepeaterMTO.DataBind();
        }

        protected void GetAL()
        {
            //string ptc = Request.QueryString["PTC"].ToString();
            //string sql = "SELECT ALCode AS Code,Date AS Date,Keeper AS Keeping,Doc AS Document,TotalState AS State,UniqueCode AS UniqueID," +
            //        "WarehouseOut AS WarehouseOut,WarehouseIn AS WarehouseIn,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
            //        "Standard AS MaterialStandard,Attribute AS Attribute,Unit AS Unit,LotNumber AS LotNumber," +
            //        "TZNUM AS Number,PTC AS PTC FROM View_SM_Allocation WHERE PTC='" + ptc + "'";
            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //RepeaterAL.DataSource = dt;
            //RepeaterAL.DataBind();
        }

        protected void GetInvoice()
        { 
            string ptc = Request.QueryString["PTC"].ToString();
            string sql = "SELECT a.GI_CODE AS Code,a.GI_SUPPLIERNM AS Supplier,a.GI_ACCBANK AS Bank," +
                "a.GI_INVOICENO AS InvoiceCode,a.GI_PZH AS CertificateCode,a.GI_JZNM AS Accounting," +
                "a.GI_ZDNM AS Document,a.GI_DATE AS RegisterDate,b.GI_PTCODE AS PTC,b.GI_MATCODE AS MaterialCode," +
                "b.GI_NAME AS MaterialName,b.GI_GUIGE AS MaterialStandard,b.GI_UNIT AS Unit,b.GI_NUM AS Number " +
                "FROM TBFM_GHINVOICETOTAL a INNER JOIN TBFM_GHINVOICEDETAIL b ON a.GI_CODE=b.GI_CODE " +
                "WHERE b.GI_PTCODE='" + ptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterInvoice.DataSource = dt;
            RepeaterInvoice.DataBind();
        }


        protected string convertCMP(string state)
        {
            switch (state)
            {
                case "311": return "已完成"; 
                default: return "进行中...";
            }
        }

        protected string convertOrder(string state)
        {
            switch (state)
            {
                case "000": return "未提交";
                case "001": return "采购中...";
                case "100": return "已关闭";
                case "111": return "已完成";
                default: return state;
            }
        }

        protected string convertItem(string state)
        {
            switch (state)
            {
                case "0": return "采购中...";
                case "1": return "已关闭";
                default: return state;
            }
        }
        
        protected string convertIn(string state)
        {
            switch (state)
            {
                case "1":
                    return "未审核";
                case "2":
                    return "已审核";
                default:
                    return state;
            }
        }        
        
        protected string convertOut(string state)
        {
            switch (state)
            {
                case "1":
                    return "未审核";
                case "2":
                    return "已审核";
                default:
                    return state;
            }
        }
        
        protected string convertMTO(string state)
        {
            switch (state)
            {
                case "1":
                    return "未审核";
                case "2":
                    return "已审核";
                default:
                    return state;
            }
        }

        protected string convertAL(string state)
        {
            switch (state)
            {
                case "0":
                    return "未提交";
                case "1":
                    return "待审核";
                case "2" :
                    return "已审核";
                default :
                    return state;
            }
        }
    }
}
