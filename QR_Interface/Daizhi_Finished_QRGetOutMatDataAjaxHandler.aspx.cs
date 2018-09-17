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

namespace ZCZJ_DPF.QR_Interface
{
    public partial class Daizhi_Finished_QRGetOutMatDataAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request.Form["param"].ToString().Trim();
            List<string> list = new List<string>();
            string result = "";
            string sql = "";
            if (param.Contains(";"))
            {
                sql = "select *,(InNum-OutNum) as StorageNum,(InMoney-OutMoney) as StorageMoney from midTable_DzFinished_management where MapCode+';'+InProdCode='" + param + "' and (InNum-OutNum)>0";//如果是初始化补齐
            }
            else
            {
                sql = "select *,(InNum-OutNum) as StorageNum,(InMoney-OutMoney) as StorageMoney from midTable_DzFinished_management where QRDzID=" + CommonFun.ComTryInt(param) + " and (InNum-OutNum)>0";//如果是选择数据时补齐
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["QRDzID"].ToString().Trim() + ";" + dt.Rows[0]["OldYzName"].ToString().Trim() + ";" + dt.Rows[0]["OldHtCode"].ToString().Trim() + ";" + dt.Rows[0]["OldTaskCode"].ToString().Trim() + ";" + dt.Rows[0]["OldYzHtCode"].ToString().Trim() + ";" + dt.Rows[0]["OldProjName"].ToString().Trim() + ";" + dt.Rows[0]["ProdName"].ToString().Trim() + ";" + dt.Rows[0]["MapCode"].ToString().Trim() + ";" + dt.Rows[0]["Caizhi"].ToString().Trim() + ";" + dt.Rows[0]["InProdCode"].ToString().Trim() + ";" + dt.Rows[0]["StorageNum"].ToString().Trim() + ";" + dt.Rows[0]["InUnit"].ToString().Trim() + ";" + dt.Rows[0]["StorageMoney"].ToString().Trim() + ";" + dt.Rows[0]["SingleWeight"].ToString().Trim() + ";" + dt.Rows[0]["ZnZw"].ToString().Trim() + ";" + dt.Rows[0]["IfERP"].ToString().Trim() + ";;;;;";
            }
            else
            {
                result = "error";
            }
            Response.Write(result);
        }
    }
}
