using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    /// <summary>
    /// OM_Data_Autocomplete 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class OM_Data_Autocomplete : System.Web.Services.WebService
    {
        [WebMethod]
        public string[] Getdata(string prefixText, int count)
        {
            string[] codes = getSuggestionList(prefixText, count);
            return codes.Where(p => p.IndexOf(prefixText) >= 0).Take(count).ToArray(); //lambda表达式，当p.IndexOf(prefixText)>0时，返回它。
        }

        private string[] getSuggestionList(string prefixText, int count)
        {
            List<string> ID = new List<string>();
            string sql_text = string.Format("select ST_NAME from TBDS_STAFFINFO where ST_NAME like '{0}%' ", prefixText);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_text);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            ID = ID.Distinct<string>().ToList();//去重复
            ID.Remove("");//去掉空值           
            return ID.ToArray();
        }


    }
}
