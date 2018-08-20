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
using System.Reflection;
using EasyUI;
using System.Collections.Generic;

namespace ZCZJ_DPF.QR_Interface
{
    public partial class PTCAssignedAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String methodName = Request["method"];
            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);
            if (method == null) throw new Exception("method is null");
            try
            {
                method.Invoke(this, null);
            }
            catch
            {
                throw;
            }
        }
        //获取关联订单内容
        public void getInPTC()
        {
            string MaterialCode = Request["MaterialCode"].ToString().Trim();
            string sql = "select ptcode as qrptcode,orderno,suppliernm,marid,marnm,margg,marcz from (select a.*,b.RESULT,s.PUR_NUM as dingenum from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,BJSJ desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC left join TBPC_PURCHASEPLAN as s on a.ptcode=s.PUR_PTCODE)t where marid='" + MaterialCode + "' and totalstate='1' and totalcstate='0' AND (detailstate='1' or detailstate='3') and detailcstate='0' and Type is null AND  (RESULT='合格' or RESULT='免检' or RESULT='让步接收')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string Json = JsonHelper.CreateJsonParameters(dt, false, dt.Rows.Count);
            Response.Write(Json);
        }
        //为扫码入库分配计划跟踪号
        public void AssignedInPTC()
        {
            List<string> list = new List<string>();
            string result = "";
            string QRIn_ID = Request["QRIn_ID"].ToString().Trim();
            string QRPTC = Request["QRPTC"].ToString().Trim();
            string sql = "update midTable_QRIn set QRIn_PTC='" + QRPTC + "' where CONVERT(varchar(20),QRIn_ID)='" + QRIn_ID + "'";
            list.Add(sql);
            try
            {
                DBCallCommon.ExecuteTrans(list);
                result = "true";
            }
            catch
            {
                result = "false";
            }
            Response.Write(result);
        }
        //获取关联库存内容
        public void getOutPTC()
        {
            string MaterialCode = Request["MaterialCode"].ToString().Trim();
            string sql = "select SQ_CODE as SQCODE,SQ_PTC as PTC,SQ_MARID as MaterialCode,MNAME as MaterialName,GUIGE as MaterialStandard,CAIZHI as Attribute,SQ_NUM as stonum from TBWS_STORAGE as a left join TBMA_MATERIAL as b on a.SQ_MARID=b.ID where SQ_MARID='" + MaterialCode + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string Json = JsonHelper.CreateJsonParameters(dt, false, dt.Rows.Count);
            Response.Write(Json);
        }
        //为扫码出库分配计划跟踪号
        public void AssignedOutPTC()
        {
            List<string> list = new List<string>();
            string result = "";
            string QROut_ID = Request["QROut_ID"].ToString().Trim();
            string SQCODE = Request["SQCODE"].ToString().Trim();
            string sql = "update midTable_QROut set QROut_SQCODE='" + SQCODE + "' where QROut_ID=" + Convert.ToInt32(QROut_ID) + "";
            list.Add(sql);
            try
            {
                DBCallCommon.ExecuteTrans(list);
                result = "true";
            }
            catch
            {
                result = "false";
            }
            Response.Write(result);
        }
    }
}
