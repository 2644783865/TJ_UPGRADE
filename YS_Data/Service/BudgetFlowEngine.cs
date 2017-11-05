using System;
using System.Data;
using System.Linq;
using ZCZJ_DPF;

namespace ZCZJ_DPF.YS_Data.Service
{
    public static class BudgetFlowEngine
    {
        const string VIEW_NODE_INSTANCE_OF_TASK = "VIEW_YS_node_instance_of_task";//任务号的node实例信息视图
        const string TABLE_NODE_INSTANCE = "YS_NODE_INSTANCE";//node实例表

        /// <summary>
        /// 获取任务号的node实例信息
        /// </summary>
        /// <param name="task_code">任务号</param>
        /// <returns>包含查询的任务号node实例信息的DATaTable</returns>
        public static DataTable getNodeInstanceInfoOfTask(string task_code)
        {
            return DBCallCommon.GetDTUsingSqlText(string.Format(@"SELECT * FROM {0} WHERE task_code='{1}';", VIEW_NODE_INSTANCE_OF_TASK, task_code));
        }

        public static void activeFollowNode(int currdetNodeId)
        {

        }

        public static void completeCurrentNode()
        {

        }

        public static void backToPreNode()
        {

        }
    }
}
