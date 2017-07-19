using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_AjaxHandler : System.Web.UI.Page
    {
        string sqlText;
        string result;
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
        public void FindDepManager()
        {
            string depId = Request["DepId"];
            if (depId != "")
            {
                sqlText = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION = '" + depId + "01'";
                try
                {
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    if (dt.Rows.Count > 0)
                    {
                        result = "{\"name\":\"" + dt.Rows[0]["ST_NAME"] + "\",\"Id\":\"" + dt.Rows[0]["ST_ID"] + "\"}";

                    }
                }
                catch (Exception)
                {

                    result = "{\"msg\":\"数据异常\",\"Id\":\"0\"}";
                }
                Response.Write(result);
              
              
            

            }

        }

    }
}
