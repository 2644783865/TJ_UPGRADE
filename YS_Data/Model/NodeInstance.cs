using System;

namespace ZCZJ_DPF.YS_Data.Model
{
    public class NodeInstance
    {
        public NodeInstance() { }

        #region Model
        /// <summary>
        /// 当前node实例的id
        /// </summary>
        public int node_instance_id { get; set; }
        /// <summary>
        /// 当前node实例的类的id
        /// </summary>
        public int node_definition_id { get; set; }
        /// <summary>
        /// 当前node实例所属任务号
        /// </summary>
        public string task_code { get; set; }
        /// <summary>
        /// 当前node实例的处理人id
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        /// 当前node实例的开始时间
        /// </summary>
        public DateTime start_time { get; set; }
        /// <summary>
        /// 当前node实例的结束时间
        /// </summary>
        public DateTime end_time { get; set; }
        /// <summary>
        /// 当前node实例的状态（1：未处理，2：已通过，3：被驳回）
        /// </summary>
        public byte state { get; set; }
        /// <summary>
        /// 当前node实例的备注，处理人可写
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 当前node实例的后一步node实例id集合
        /// </summary>
        public string next_node_instance_id { get; set; }
        /// <summary>
        /// 当前node实例的上一步node实例id集合
        /// </summary>
        public string pre_node_instance_id { get; set; }
        #endregion
    }
}
