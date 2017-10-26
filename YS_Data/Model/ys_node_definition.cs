using System;

namespace ZCZJ_DPF.YS_Data.Model
{
    public class ys_node_definition
    {
        public ys_node_definition() { }

        #region Model
        /// <summary>
        /// node类id
        /// </summary>
        public int node_definition_id { get; set; }
        /// <summary>
        /// node类名称
        /// </summary>
        public string node_definition_name { get; set; }
        /// <summary>
        /// node类类型
        /// 1：开始节点
        /// 2：中间节点
        /// 3：结束节点
        /// </summary>
        public byte? node_definition_type { get; set; }
        /// <summary>
        /// node类逻辑
        /// NULL：普通节点
        /// 1：and_split
        /// 2：and_join
        /// 3：or_split
        /// 4：or_join
        /// </summary>
        public byte node_definition_logic { get; set; }
        /// <summary>
        /// 执行该node类的人员角色
        /// </summary>
        public int assigned_role { get; set; }
        #endregion
    }
}
