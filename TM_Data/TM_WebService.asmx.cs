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

namespace ZCZJ_DPF.TM_Data
{
    /// <summary>
    /// TM_WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class TM_WebService : System.Web.Services.WebService
    {
        [WebMethod]
        #region
        public string[] GetSuggestions(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = getSuggestionList(prefixText, contextKey);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }

        private string[] getSuggestionList(string prefixText, string contextKey)
        {
            List<string> ID = new List<string>();
            string[] array_list = contextKey.Split('@');
            string sql_text;
            sql_text = "select '总序 | '+BM_ZONGXU+' | '+BM_CHANAME FROM " + array_list[0] + " WHERE BM_ENGID='" + array_list[1] + "' AND BM_ZONGXU like '" + prefixText + "%'  order by dbo.f_formatstr(BM_ZONGXU,'.')";


            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_text);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }

        #endregion
        [WebMethod]
        #region
        public string[] GetSuggestions_Tech(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = getSuggestionList_Tech(prefixText, contextKey);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }

        private string[] getSuggestionList_Tech(string prefixText, string contextKey)
        {
            List<string> ID = new List<string>();
            string sql_text;
            if (contextKey != "")
            {
                //sql_text = "select [CM_PROJ]+'‖'+[TSA_PJID]+'‖'+[TSA_ENGNAME]+'‖'+[TSA_ID]+'‖'+isnull([TSA_TCCLERKNM],'') from View_TM_TaskAssign where " + contextKey + " like '" + prefixText + "%'";
                sql_text = "select " + contextKey + " from View_TM_TaskAssign where " + contextKey + " like '" + prefixText + "%'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_text);
                foreach (DataRow row in dt.Rows)
                {
                    ID.Add(row[0].ToString().Trim());
                }

            }

            return ID.ToArray();


        }
        #endregion
    }
}