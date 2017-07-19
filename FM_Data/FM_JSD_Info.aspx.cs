using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_JSD_Info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ObjUniqCode = Request.QueryString["UniqId"];
                string ObjUniqCode_diff = "('" + ObjUniqCode.Replace("/", "','") + "')";
                string sql = "select TA_DOCNUM,TA_ORDERNUM,TA_PTC,TA_SUPPLYID,TA_SUPPLYNAME,TA_ZDR,TA_ZDRNAME,left(CONVERT(CHAR(10), TA_ZDTIME, 23),10) as TA_ZDDATE,isnull(TA_NUM,0) as TA_NUM,isnull(TA_PRICE,0) as TA_PRICE,isnull(TA_WGHT,0) as TA_WGHT,isnull(TA_MONEY,0) as TA_MONEY,isnull(TA_AMOUNT,0) as TA_AMOUNT,isnull(TA_TOTALWGHT,0) as TA_TOTALWGHT,TA_WXTYPE,TA_CAIZHI,TA_TUHAO from View_TBMP_ACCOUNTS where TA_PTC in " + ObjUniqCode_diff + "";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                rptProNumCost.DataSource = dt;
                rptProNumCost.DataBind();
                if (dt.Rows.Count == 0)
                {
                    NoDataPanel.Visible = true;
                }
            }
        }
    }
}
