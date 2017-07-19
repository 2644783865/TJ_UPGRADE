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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_AjaxHandler : System.Web.UI.Page
    {
        string sqlText;
        string result;
        PagerQueryParam pager = new PagerQueryParam();
        int rows;
        int page;
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

        public void FindDepPeo()
        {
            string depId = Request.Form["Id"];
            sqlText = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='"+depId+"' and st_pd='0'";
            DataTable dt=DBCallCommon.GetDTUsingSqlText(sqlText);
            string result = JsonHelper.CreateJsonOne(dt, false);
            Response.Write(result);
        }
        public void FindDepbySTId()
        {
            string stId = Session["UserID"].ToString();
            sqlText = "select ST_DEPID from TBDS_STAFFINFO where ST_ID='" + stId + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            string depCode = "02";
            if (dt.Rows.Count>0)
            {
                depCode = dt.Rows[0][0].ToString();
            }
            result = "{\"dep\":\"" + depCode + "\"}";
            Response.Write(result);
        }
    }
}
