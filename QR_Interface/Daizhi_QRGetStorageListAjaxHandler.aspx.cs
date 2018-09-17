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
using EasyUI;

namespace ZCZJ_DPF.QR_Interface
{
    public partial class Daizhi_QRGetStorageListAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mapcode = Request["mapcode"].ToString().Trim();
            string inprodcode = Request["inprodcode"].ToString().Trim();
            string sql = "select top(10) QRDzID,MapCode,InProdCode,(InNum-OutNum) as StorageNum from midTable_DzFinished_management where MapCode='" + mapcode + "' and InProdCode='" + inprodcode + "' and (InNum-OutNum)>0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string Json = JsonHelper.CreateJsonParameters(dt, false, dt.Rows.Count);
            Response.Write(Json);
        }
    }
}
