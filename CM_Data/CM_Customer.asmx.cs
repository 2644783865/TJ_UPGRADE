using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.ComponentModel;

namespace ZCZJ_DPF.CM_Data
{
    /// <summary>
    /// CM_Customer1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class CM_Customer1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string[] Getdata(string prefixText, int count, string contextKey)
        {
            string[] codes = getSuggestionList(prefixText, contextKey);
            return codes.Where(p => p.IndexOf(prefixText) >= 0).Take(count).ToArray();
        }

        private string[] getSuggestionList(string prefixText, string contextKey)
        {
            List<string> ID = new List<string>();
            string sql_text = string.Format("select {0} from TBCM_CUSTOMER", contextKey);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_text);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            ID = ID.Distinct<string>().ToList();//去重复
            ID.Remove("");//去掉空值           
            return ID.ToArray();
        }

        [WebMethod]
        public string[] GetFile(string prefixText, int count, string contextKey)
        {
            string[] codes = getFileList(prefixText, contextKey);
            return codes.Where(p => p.IndexOf(prefixText) >= 0).Take(count).ToArray();
        }

        private string[] getFileList(string prefixText, string contextKey)
        {
            List<string> ID = new List<string>();
            string sql_text = string.Format("select {0} from TBCM_APPLICA as a right join TBCM_RECORD as b on a.CM_ID=b.CM_ID", contextKey);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_text);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            ID = ID.Distinct<string>().ToList();//去重复
            ID.Remove("");//去掉空值           
            return ID.ToArray();
        }

        [WebMethod]
        public string[] GetNotice(string prefixText, int count, string contextKey)
        {
            string[] codes = getNoticeList(prefixText, contextKey);
            return codes.Where(p => p.IndexOf(prefixText) >= 0).Take(count).ToArray();
        }

        private string[] getNoticeList(string prefixText, string contextKey)
        {
            List<string> ID = new List<string>();
            string sql_text = string.Format("select {0} from View_CM_FaHuo", contextKey);
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
