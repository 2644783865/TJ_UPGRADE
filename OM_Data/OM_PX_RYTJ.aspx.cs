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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_PX_RYTJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnadd_click(object sender, EventArgs e)
        {
            string sqltxt = "";
            sqltxt = "select * from View_TBDS_STAFFINFO where ST_ID='" + firstid.Value + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltxt);

            if (dt.Rows.Count > 0)
            {
                string stid = dt.Rows[0]["ST_ID"].ToString();
                string bmid = dt.Rows[0]["ST_DEPID"].ToString();
                string bm= dt.Rows[0]["DEP_NAME"].ToString();
                string sql = "";
                sql = "insert into OM_PXJBXS (XS_NAME,XS_STID,XS_BMID,XS_BM) values ('" + txtNAME.Text.ToString().Trim() + "','" + stid + "','" + bmid + "','" + bm + "')";
                DBCallCommon.ExeSqlText(sql);
                Response.Redirect("OM_PXJBXS.aspx");
            }
            //else
            //{
            //    Response.Write("<script type='text/javascript'>alert('该公司无此人员！！！')</script>");
            //}



        }
    }
}
