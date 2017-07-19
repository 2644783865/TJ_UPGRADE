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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_ZanGu_RuKu_Manage_View : System.Web.UI.Page
    {

        double InAmount = 0;
        double InCTAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = string.Empty;
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    id = Request.QueryString["ID"];
                    BindInItem(id);
                }
            }
        }

        private void BindInItem(string incode)
        {
            string sql = "select WG_CODE,WG_MARID,MNAME,GUIGE,CGDW,cast(WG_RSNUM as float) AS WG_RSNUM,cast(WG_UPRICE as float) as WG_UPRICE,cast(WG_AMOUNT as float) as WG_AMOUNT,WG_TAXRATE,cast(WG_CTAXUPRICE as float) as WG_CTAXUPRICE, cast(WG_CTAMTMNY as float) as WG_CTAMTMNY,HMCODE from View_SM_IN where WG_CODE='" + incode + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridViewIn.DataSource = dt;
            GridViewIn.DataBind();
        }

        protected void GridViewIn_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                InAmount += Convert.ToDouble(e.Row.Cells[8].Text);

                InCTAmount += Convert.ToDouble(e.Row.Cells[11].Text);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "合计:";
                e.Row.Cells[8].Text = InAmount.ToString();
                e.Row.Cells[11].Text = InCTAmount.ToString();
            }

        }
    }
}
