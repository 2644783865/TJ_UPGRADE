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

namespace ZCZJ_DPF.FM_Data
{
    public partial class SCZHLL_PJ : System.Web.UI.Page
    {
        string sql = "";
        string SCZH = "";
        string Start = "";
        string End = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"].ToString() != null)
                {
                    SCZH = Request.QueryString["id"].ToString();
                    Start = Request.QueryString["starttime"].ToString();
                    End = Request.QueryString["endtime"].ToString();
                }
            }
            if (Request.QueryString["m"] != null)
            {
                bind();
            }
            else
            {
                databind();
            }
        }

        public void databind()
        {
            string startime = Convert.ToDateTime(Start).ToString("yyyy-MM-dd");
            string endtime = Convert.ToDateTime(End).AddDays(1).ToString("yyyy-MM-dd");
            sql = " select ROW_NUMBER() OVER (ORDER BY CASE WHEN substring(MaterialCode,1,5) like '01.07'  THEN 0 ELSE 1 END, WarehouseCode ASC) AS Row_Num,* from View_SM_OUT where TSAID  LIKE '%" + SCZH + "' and ApprovedDate>='" + startime + "' and  ApprovedDate< '" + endtime + "' and (billtype!='3') ";
            DataTable dt=DBCallCommon.GetDTUsingSqlText(sql);
            GridViewIn.DataSource = dt;
            GridViewIn.DataBind();
        }


        public void bind()
        {
            string startime = Convert.ToDateTime(Start).ToString("yyyy-MM-dd");
            string endtime = Convert.ToDateTime(End).ToString("yyyy-MM-dd");
            sql = " select ROW_NUMBER() OVER (ORDER BY CASE WHEN substring(MaterialCode,1,5) like '01.07'  THEN 0 ELSE 1 END, WarehouseCode ASC) AS Row_Num,* from View_SM_OUT where PTC  LIKE '%" + SCZH + "' and ApprovedDate>='" + startime + "' and  ApprovedDate< '" + endtime + "' and billtype='3' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridViewIn.DataSource = dt;
            GridViewIn.DataBind();

        }
    }
}
