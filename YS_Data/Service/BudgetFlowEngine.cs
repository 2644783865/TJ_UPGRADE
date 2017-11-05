using System;
using System.Data;
using System.Data.SqlClient;
using ZCZJ_DPF;
using System.Collections.Generic;

namespace ZCZJ_DPF
{
    public static class BudgetFlowEngine
    {
        const string VIEW_NODE_INSTANCE_OF_TASK = "VIEW_YS_node_instance_of_task";//任务号的node实例信息视图
        const string VIEW_TO_NODE = "VIEW_YS_to_node_info";//to节点信息表
        const string TABLE_NODE_INSTANCE = "YS_NODE_INSTANCE";//node实例表
        const string TABLE_NODE_DEFINITION = "YS_NODE_DEFINITION";
        const string TABLE_LINE = "YS_line";//路线信息视图
        const string TABLE_TASK_BUDGET = "YS_TASK_BUDGET";



        /// <summary>
        /// 激活（创建）后续的节点，（没有考虑or_split和or_join，以后补上）
        /// </summary>
        /// <param name="task_code">任务号</param>
        /// <param name="node_definition_id">当前节点的node类id</param>
        /// <param name="user_ids">存储后续节点中所有责任人id的数组，
        /// 按照后续节点的id从小到大排序,例如
        /// 【生产部长、采购部长】
        /// 【人工费反馈人，分包费反馈人，外协反馈人】、
        /// 【黑色金属、外购件、油漆涂料、电器电料、铸锻件、其他材料】</param>
        public static void activeFollowNode(string task_code, string node_definition_id, string[] user_ids)
        {

            if (node_definition_id.Equals("0"))//如果是刚进入该任务
            {
                DBCallCommon.ExeSqlText(string.Format(@"INSERT  INTO {0}
        ( node_definition_id ,
          task_code ,
          user_id ,
          start_time ,
          state       
        )
        VALUES(   1,
                '{1}' ,
                {2} ,
                GETDATE() ,
                1); ", TABLE_NODE_INSTANCE, task_code, user_ids[0]));
            }
            else
            {
                List<string> listsql = new List<string>();
                for (int i = 0; i < user_ids.Length; i++)
                {
                    listsql.Add(string.Format(@"INSERT  INTO {0}
        ( node_definition_id ,
          task_code ,
          user_id ,
          start_time ,
          state       
        )
        SELECT  ( SELECT TOP 1
                            to_node_definition_id
                  FROM     {1}
                  WHERE     from_node_definition_id={2}                            
                            AND to_node_definition_id NOT IN (
                            SELECT TOP {3}
                                    to_node_definition_id
                            FROM    {4}
                            WHERE   from_node_definition_id = {5}
                            ORDER BY from_node_definition_id )
                ) ,
                '{6}' ,
                {7} ,
                GETDATE() ,
                1; ", TABLE_NODE_INSTANCE, TABLE_LINE, node_definition_id, i, TABLE_LINE, node_definition_id, task_code, user_ids[i]));
                }
                DBCallCommon.ExecuteTrans(listsql);
            }

        }


        /// <summary>
        /// 完成当前结点，并判断下一节点的前续节点是否都已经完成
        /// </summary>
        /// <param name="task_code">任务号</param>
        /// <param name="node_definition_id">当前节点的node类id</param>
        /// <param name="note">备注</param>
        /// <returns>true：可以激活下一节点；false：不能激活下一节点</returns>
        public static bool completeCurrentNode(string task_code, string node_definition_id, string note)
        {
            


            //完成当前节点的结束时间、状态、备注更新
            DBCallCommon.ExeSqlText(string.Format(@"UPDATE {0} SET end_time=GETDATE(),state=2,note='{1}' 
WHERE node_definition_id={2} AND task_code='{3}';", TABLE_NODE_INSTANCE, note, node_definition_id, task_code));

            //判断当前节点的类型，1：开始节点；2：中间节点；3：结束节点
            string type = getStringByDR(string.Format(@"SELECT node_definition_type FROM {0} WHERE node_definition_id={1};", TABLE_NODE_DEFINITION, node_definition_id));
           
            if (type.Equals("3"))//如果是结束节点
            {
                //在任务预算表中更新预算编制结束时间，预算编制状态
                DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET end_time=GETDATE(),state=5 WHERE task_code='{1}';", TABLE_TASK_BUDGET, task_code));
                return false;
            }
            else//如果不是结束节点，还有后续节点
            {
                //判断后续节点的逻辑，2：and-join
                string logic = getStringByDR(string.Format(@"SELECT TOP 1 to_node_definition_logic 
FROM {0} WHERE node_definition_id={1};", VIEW_TO_NODE, node_definition_id));

                //如果后续节点是and-joi，且后续节点还有没有完成的前续节点
                if (logic.Equals("2") && (!getStringByDR(string.Format(@"SELECT COUNT(1) FROM (SELECT from_node_definition_id 
FROM dbo.YS_LINE WHERE to_node_definition_id=(SELECT TOP 1 to_node_definition_id FROM dbo.YS_LINE WHERE from_node_definition_id={0}) 
EXCEPT SELECT node_instance_id FROM dbo.YS_NODE_INSTANCE WHERE task_code='{1}' AND state=2 AND node_definition_id IN 
(SELECT from_node_definition_id FROM dbo.YS_LINE WHERE to_node_definition_id=(SELECT TOP 1 to_node_definition_id FROM 
dbo.YS_LINE WHERE from_node_definition_id={2})))t;", node_definition_id, task_code, node_definition_id)).Equals("0")))
                {
                    return false;
                }
                else//如果后续节点不是and-join，或后续节点的前续节点全部完成，激活下一个节点
                {
                    return true;
                }
            }
        }

        public static void backToPreNode()
        {

        }


        /// <summary>
        /// 通过查询语句获取第一个值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string getStringByDR(string sql)
        {
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                string r = dr[0].ToString();
                dr.Close();
                return r;
            }
            else
            {
                dr.Close();
                return "";
            }
        }
    }
}
