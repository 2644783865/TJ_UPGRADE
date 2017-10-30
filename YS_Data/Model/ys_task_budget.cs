using System;


namespace ZCZJ_DPF.YS_Data.Model
{
    public class ys_task_budget
    {
        public ys_task_budget() { }

        #region Model
        /// <summary>
        /// 任务预算id
        /// </summary>
        public int task_budget_id { get; set; }
        /// <summary>
        /// 任务号
        /// </summary>
        public string task_code { get; set; }
        /// <summary>
        /// 任务号所属合同号
        /// </summary>
        public string contract_code { get; set; }
        /// <summary>
        /// 项目名称（一般为业主所在地）
        /// </summary>
        public string project_name { get; set; }
        /// <summary>
        /// 设备名称（为当前任务号下所有的产品名称)
        /// </summary>
        public string equipment_name { get; set; }
        /// <summary>
        /// 任务号类型（区分任务号种类，用于比较相同类型任务号的预算）
        /// </summary>
        public string task_type { get; set; }
        /// <summary>
        /// 财务部确定：所有材料费预算
        /// </summary>
        public decimal total_material_budget { get; set; }
        /// <summary>
        /// 财务部确定：直接人工费预算
        /// </summary>
        public decimal direct_labour_budget { get; set; }
        /// <summary>
        /// 财务部确定：厂内分包费预算
        /// </summary>
        public decimal sub_teamwork_budget { get; set; }
        /// <summary>
        /// 财务部确定：生产外协费预算
        /// </summary>
        public decimal cooperative_product_budget { get; set; }
        /// <summary>
        /// 财务部确定：任务号总预算（计算列，只读）
        /// </summary>
        public decimal c_total_task_budget { get; set; }
        /// <summary>
        /// 采购部参考值：黑色金属费用
        /// </summary>
        public decimal ys_ferrous_metal_dep { get; set; }
        /// <summary>
        /// 采购部参考值：外购件费用
        /// </summary>
        public decimal ys_purchase_part_dep { get; set; }
        /// <summary>
        /// 采购部参考值：油漆涂料费用
        /// </summary>
        public decimal ys_paint_coating_dep { get; set; }
        /// <summary>
        /// 采购部参考值：电器电料费用
        /// </summary>
        public decimal ys_electrical_dep { get; set; }
        /// <summary>
        /// 采购部参考值：铸锻件费用
        /// </summary>
        public decimal ys_casting_forging_dep { get; set; }
        /// <summary>
        /// 采购部参考值：其他材料费用
        /// </summary>
        public decimal ys_othermat_cost_dep { get; set; }
        /// <summary>
        /// 采购部参考值：所有材料费用（计算列，只读）
        /// </summary>
        public decimal c_total_material_dep { get; set; }
        /// <summary>
        /// 生产部参考值：直接人工费用
        /// </summary>
        public decimal direct_labour_dep { get; set; }
        /// <summary>
        /// 生产部参考值：厂内分包费用
        /// </summary>
        public decimal sub_teamwork_dep { get; set; }
        /// <summary>
        /// 生产部参考值：生产外协费用
        /// </summary>
        public decimal cooperative_product_dep { get; set; }
        /// <summary>
        /// 历史参考值：黑色金属费用
        /// </summary>
        public decimal ys_ferrous_metal { get; set; }
        /// <summary>
        /// 历史参考值：外购件费用
        /// </summary>
        public decimal ys_purchase_part { get; set; }
        /// <summary>
        /// 历史参考值：油漆涂料费用
        /// </summary>
        public decimal ys_paint_coating { get; set; }
        /// <summary>
        /// 历史参考值：电器电料费用
        /// </summary>
        public decimal ys_electrical { get; set; }
        /// <summary>
        /// 历史参考值：铸锻件费用
        /// </summary>
        public decimal ys_casting_forging_cost { get; set; }
        /// <summary>
        /// 历史参考值：其他材料费用
        /// </summary>
        public decimal ys_othermat_cost { get; set; }
        /// <summary>
        /// 历史参考值：所有材料费用（计算列，只读）
        /// </summary>
        public decimal c_total_material_history { get; set; }
        /// <summary>
        /// 财务部：预算编制人id
        /// </summary>
        public int maker_id { get; set; }
        /// <summary>
        /// 预算编制开始时间（以技术部提交时间为准）
        /// </summary>
        public DateTime start_time { get; set; }
        /// <summary>
        /// 预算编制结束时间（以财务部确认预算编制完成时间为准）
        /// </summary>
        public DateTime end_time { get; set; }
        /// <summary>
        /// 预算编制状态（1：初步预算，2：部门反馈，3：预算调整，4：预算审核，5：编制完成）
        /// </summary>
        public byte state { get; set; }
        /// <summary>
        /// 预算编制备注，预算编制人填写
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 当前预算编制的第一个node实例id
        /// </summary>
        public int start_node_instance_id { get; set; }

        #endregion
    }
}
