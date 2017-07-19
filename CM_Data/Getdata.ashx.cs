using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;

namespace ZCZJ_DPF.CM_Data
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Getdata : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string CM_CONTR = context.Request["CM_CONTR"];
            string sql = "select CM_CUSNAME,CM_EQUIP,CM_EQUIPMAP from TBCM_APPLICA where CM_CONTR='" + CM_CONTR + "'";
            var dt = DBCallCommon.GetDTUsingSqlText(sql);
            string json = string.Empty;
            string str = ",,";
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                //string[] strs = new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString() };
                str = string.Format("{0},{1},{2}", dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            context.Response.Write(str);
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
