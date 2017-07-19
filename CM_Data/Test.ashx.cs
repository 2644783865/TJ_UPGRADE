using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace ZCZJ_DPF.CM_Data
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Text : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string bh = context.Request["CM_BIANHAO"];
            string sql = "select CM_BIANHAO from TBCM_FHNOTICE where CM_BIANHAO='" + bh + "'";
            var dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                context.Response.Write("1");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
