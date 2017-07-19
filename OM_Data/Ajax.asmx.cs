using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    /// <summary>
    /// Ajax1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
     [System.Web.Script.Services.ScriptService]
    public class Ajax1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] HmCode(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = bgcode(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] bgcode(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select top 15 name+'|'+canshu+'|'+a.price+'|'+unit+'|'+maId+'|'+cast(a.Id as Varchar)+'|'+case when cast(num as varchar) is null then '0' else cast(num as varchar) end +'|'+case when cast(unPrice as varchar) is null then '0' else cast(unPrice as varchar) end  from TBMA_OFFICETH as a  left join dbo.TBOM_BGYP_STORE as b on a.Id=b.mId  where IsDel=0 and IsBottom='on' and  (name like '%" + fixText + "%' or maId like '%" + fixText + "%' or canshu like '%" + fixText + "%') order by a.Id desc  ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }
    }
}
