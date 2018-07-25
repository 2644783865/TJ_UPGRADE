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
        public void initPager(PagerQueryParam pager,string taskCode,string contractCode,string projectName,string state,string myState,string userid)
        {            
            pager.TableName = " ys_task_budget ";
            pager.PrimaryKey = "task_budget_id";
            pager.ShowFields = " task_code,contract_code,project_name,equipment_name as product_name,task_type,case when state='1' then '初步预算'when state='2' then '部门反馈'when state='3' then '财务调整'when state='4' then '预算审核' when state='5' then '编制完成' end  AS state,c_total_task_budget ,change_time,change_name";
            pager.StrWhere = this.getStrWhere(taskCode,contractCode,projectName,state,myState,userid);
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
        public string getStrWhere(string taskCode, string contractCode, string projectName,string state ,string myState,string userid) 
        {
            StringBuilder strWhere = new StringBuilder(" 1=1 ");            

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
            if(myState=="1")//未处理
            {
                strWhere.Append("and task_code IN ( SELECT DISTINCT task_code FROM dbo.YS_NODE_INSTANCE WHERE state=1 AND user_id=" + userid + ")");
            }
            else if (myState =="3")//被驳回
            {
                strWhere.Append("and task_code IN ( SELECT DISTINCT task_code FROM dbo.YS_NODE_INSTANCE WHERE state=3 AND user_id=" + userid + ")");
            }

            return strWhere.ToString();
        }


    }
}
