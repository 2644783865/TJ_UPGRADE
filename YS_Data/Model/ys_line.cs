using System;

namespace ZCZJ_DPF.YS_Data.Model
{
    public class ys_line
    {
        public ys_line() { }

        #region Model
        /// <summary>
        /// 线段id
        /// </summary>
        public int line_id { get; set; }
        /// <summary>
        /// 线段名称
        /// </summary>
        public string line_name { get; set; }
        /// <summary>
        /// 线段来源
        /// </summary>
        public int to_node_definition_id { get; set; }
        /// <summary>
        /// 线段去向
        /// </summary>
        public int from_node_definition_id { get; set; }
        #endregion
    }
}
