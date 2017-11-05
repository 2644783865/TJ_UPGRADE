using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ZCZJ_DPF;
using System.Text;
namespace ZCZJ_DPF.YS_Data.BLL
{
    public class ys_contract_budget_list
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="contractCode"></param>
        /// <param name="projectName"></param>
        public void initPager(PagerQueryParam pager, string contractCode, string projectName)
        {
            pager.TableName = " View_YS_contract_budget ";
            pager.PrimaryKey = "contract_code";
            pager.ShowFields = " contract_code,contract_customer_name,contract_project_name,contract_equipment_name ,contract_income,contract_task_budget,contract_trans_budget,contract_profit,contract_profit_rate ";
            pager.StrWhere = this.getStrWhere( contractCode, projectName);
            pager.OrderField = "contract_code";
            pager.OrderType = 1;
            pager.PageSize = 10;
        }

        public string getStrWhere( string contractCode, string projectName)
        {
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            
            if (!string.IsNullOrEmpty(contractCode))
            {
                strWhere.Append("and contract_code like '%" + contractCode + "%' ");
            }
            if (!string.IsNullOrEmpty(projectName))
            {
                strWhere.Append("and contract_project_name like '%" + projectName + "%' ");
            }
            
            return strWhere.ToString();
        }
    }
}
