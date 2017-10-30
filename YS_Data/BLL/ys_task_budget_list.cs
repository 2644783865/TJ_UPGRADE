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
using System.Text;

namespace ZCZJ_DPF.YS_Data.BLL
{
    public class ys_task_budget_list
    {
        /// <summary>
        /// 初始化页面查询对象
        /// </summary>
        /// <param name="taskCode">任务号</param>
        /// <param name="contractCode">合同号</param>
        /// <param name="projectName">项目名称</param>
        /// <param name="state">编制进度</param>
        public void initPager(PagerQueryParam pager,string taskCode,string contractCode,string projectName,string state)
        {            
            pager.TableName = " ys_task_budget ";
            pager.PrimaryKey = "task_budget_id";
            pager.ShowFields = " task_code,contract_code,project_name,equipment_name,task_type,state,c_total_task_budget ";
            pager.StrWhere = this.getStrWhere(taskCode,contractCode,projectName,state);
            pager.OrderField = "start_time";
            pager.OrderType = 1;
            pager.PageSize = 10;
        }

        /// <summary>
        /// 获取查询对象的where语句
        /// </summary>
        /// <param name="taskCode">任务号</param>
        /// <param name="contractCode">合同号</param>
        /// <param name="projectName">项目名称</param>
        /// <param name="state">编制进度</param>
        /// <returns>查询对象的where语句</returns>
        public string getStrWhere(string taskCode, string contractCode, string projectName,string state) 
        {
            StringBuilder strWhere = new StringBuilder(" 1=1");            
            if(!string.IsNullOrEmpty(taskCode))
            {
                strWhere.Append("and task_code like '%"+taskCode+"%' ");
            }
            if (!string.IsNullOrEmpty(contractCode))
            {
                strWhere.Append("and contract_code like '%" + contractCode + "%' ");
            }
            if (!string.IsNullOrEmpty(projectName))
            {
                strWhere.Append("and project_name like '%" + projectName + "%' ");
            }
            if (!(string.IsNullOrEmpty(state)||state.Equals("-请选择-")))
            {
                strWhere.Append("and state =" +state);
            }
            return strWhere.ToString();
        }

        /// <summary>
        /// 翻译预算编制状态
        /// </summary>
        /// <param name="type">数据库查到的编制状态，是阿拉伯数字</param>
        /// <returns>根据阿拉伯数字翻译后的编制状态</returns>
        public string getTaskState(string type)
        {
            switch (type)
            {
                case "1": return "初步预算";
                case "2": return "部门反馈";
                case "3": return "预算调整";
                case "4": return "预算审核";
                case "5": return "编制完成";
                default: return "";
            }
        }
    }
}
