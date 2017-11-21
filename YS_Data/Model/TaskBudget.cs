///对应任务号预算的类


using System;
using System.Data;
using System.Data.SqlClient;


namespace ZCZJ_DPF.YS_Data.Model
{
    public class TaskBudget
    {


        const string VIEW_NODE_INSTANCE_OF_TASK = "VIEW_YS_node_instance_of_task";//任务号的node实例视图        
        const string TABLE_TASK_BUDGET = "YS_TASK_BUDGET";//任务预算表


        public TaskBudget(string task_code)
        {
            //任务预算表取值
            SqlDataReader dr_task = DBCallCommon.GetDRUsingSqlText(string.Format(@"SELECT * FROM {0} WHERE task_code='{1}';", TABLE_TASK_BUDGET, task_code));
            //node实例视图取值
            DataTable dt_node = DBCallCommon.GetDTUsingSqlText(string.Format(@"SELECT * FROM {0} WHERE task_code='{1}';", VIEW_NODE_INSTANCE_OF_TASK, task_code));

            #region 任务预算表赋值

            if (dr_task.Read())
            {
                this.task_budget_id = dr_task["task_budget_id"]+"";
                this.task_code = task_code;
                this.contract_code = dr_task["contract_code"]+"";
                this.project_name = dr_task["project_name"]+"";
                this.equipment_name = dr_task["equipment_name"]+"";
                this.task_weight = dr_task["task_weight"]+"";
                this.task_type = dr_task["task_type"]+"";


                this.total_material_budget_pre = dr_task["total_material_budget_pre"]+"";
                this.labour_budget_pre = dr_task["direct_labour_budget_pre"]+"";
                this.teamwork_budget_pre = dr_task["sub_teamwork_budget_pre"]+"";
                this.cooperative_budget_pre = dr_task["cooperative_product_budget_pre"]+"";
                this.c_total_task_budget_pre = dr_task["c_total_task_budget_pre"]+"";



                this.total_material_budget = dr_task["total_material_budget"]+"";
                this.labour_budget = dr_task["direct_labour_budget"]+"";
                this.teamwork_budget = dr_task["sub_teamwork_budget"]+"";
                this.cooperative_budget = dr_task["cooperative_product_budget"]+"";
                this.c_total_task_budget = dr_task["c_total_task_budget"]+"";
                this.budget_check = dr_task["budget_check"]+"";

                this.ferrous_dep = dr_task["ys_ferrous_metal_dep"]+"";
                this.purchasepart_dep = dr_task["ys_purchase_part_dep"]+"";
                this.paint_dep = dr_task["ys_paint_coating_dep"]+"";
                this.electrical_dep = dr_task["ys_electrical_dep"]+"";
                this.casting_dep = dr_task["ys_casting_forging_dep"]+"";
                this.othermat_dep = dr_task["ys_othermat_cost_dep"]+"";
                this.c_total_material_dep = dr_task["c_total_material_dep"]+"";
                this.purchase_check = dr_task["purchase_check"]+"";

                this.labour_dep = dr_task["direct_labour_dep"]+"";
                this.teamwork_dep = dr_task["sub_teamwork_dep"]+"";
                this.cooperative_dep = dr_task["cooperative_product_dep"]+"";
                this.production_check = dr_task["production_check"]+"";

                this.ferrous_his = dr_task["ys_ferrous_metal"]+"";
                this.purchasepart_his = dr_task["ys_purchase_part"]+"";
                this.paint_his = dr_task["ys_paint_coating"]+"";
                this.electrical_his = dr_task["ys_electrical"]+"";
                this.casting_his = dr_task["ys_casting_forging_cost"]+"";
                this.othermat_his = dr_task["ys_othermat_cost"]+"";
                this.c_total_material_his = dr_task["c_total_material_history"]+"";

                this.maker_id = dr_task["maker_id"]+"";

                //this.start_time = dr_task["start_time"]+"";
                //this.end_time = dr_task["end_time"]+"";
                //this.state = dr_task["state"]+"";
                //this.note = dr_task["note"]+"";
                //this.start_node_instance_id = dr_task["start_node_instance_id"]+"";  
            }
            dr_task.Close();
            #endregion

            #region node实例视图赋值

            #region 财务部预算编制
            DataRow[] drs1 = dt_node.Select("node_definition_id=1");
            if (drs1.Length != 0)
            {
                DataRow dr1 = drs1[0];
                this.node_budget_edit_user_id = dr1["user_id"]+"";
                this.node_budget_edit_user_name = dr1["user_name"]+"";
                this.node_budget_edit_start_time = dr1["start_time"]+"";
                this.node_budget_edit_end_time = dr1["end_time"]+"";
                this.node_budget_edit_note = dr1["note"]+"";
                this.node_budget_edit_state = dr1["state"]+"";
            }
            else
            {
                this.node_budget_edit_user_id = "";
            }
            #endregion

            #region 生产部长反馈分工
            DataRow[] drs2 = dt_node.Select("node_definition_id=2");
            if (drs2.Length != 0)
            {
                DataRow dr2 = drs2[0];
                this.node_production_divide_user_id = dr2["user_id"]+"";
                this.node_production_divide_user_name = dr2["user_name"]+"";
                this.node_production_divide_start_time = dr2["start_time"]+"";
                this.node_production_divide_end_time = dr2["end_time"]+"";
                this.node_production_divide_note = dr2["note"]+"";
                this.node_production_divide_state = dr2["state"]+"";
            }
            else
            {
                this.node_production_divide_user_id = "";
            }
            #endregion

            #region 生产部人工费反馈
            DataRow[] drs3 = dt_node.Select("node_definition_id=3");
            if (drs3.Length != 0)
            {
                DataRow dr3 = drs3[0];
                this.node_labour_dep_user_id = dr3["user_id"]+"";
                this.node_labour_dep_user_name = dr3["user_name"]+"";
                this.node_labour_dep_start_time = dr3["start_time"]+"";
                this.node_labour_dep_end_time = dr3["end_time"]+"";
                this.node_labour_dep_note = dr3["note"]+"";
                this.node_labour_dep_state = dr3["state"]+"";
            }
            else
            {
                this.node_labour_dep_user_id = "";
            }
            #endregion

            #region 生产部分包费反馈
            DataRow[] drs4 = dt_node.Select("node_definition_id=4");
            if (drs4.Length != 0)
            {
                DataRow dr4 = drs4[0];
                this.node_teamwork_dep_user_id = dr4["user_id"]+"";
                this.node_teamwork_dep_user_name = dr4["user_name"]+"";
                this.node_teamwork_dep_start_time = dr4["start_time"]+"";
                this.node_teamwork_dep_end_time = dr4["end_time"]+"";
                this.node_teamwork_dep_note = dr4["note"]+"";
                this.node_teamwork_dep_state = dr4["state"]+"";
            }
            else
            {
                this.node_teamwork_dep_user_id = "";
            }
            #endregion

            #region 生产部外协费反馈
            DataRow[] drs5 = dt_node.Select("node_definition_id=5");
            if (drs5.Length != 0)
            {
                DataRow dr5 = drs5[0];
                this.node_cooperative_dep_user_id = dr5["user_id"]+"";
                this.node_cooperative_dep_user_name = dr5["user_name"]+"";
                this.node_cooperative_dep_start_time = dr5["start_time"]+"";
                this.node_cooperative_dep_end_time = dr5["end_time"]+"";
                this.node_cooperative_dep_note = dr5["note"]+"";
                this.node_cooperative_dep_state = dr5["state"]+"";
            }
            else
            {
                this.node_cooperative_dep_user_id = "";
            }
            #endregion

            #region 生产部长反馈审核
            DataRow[] drs6 = dt_node.Select("node_definition_id=6");
            if (drs6.Length != 0)
            {
                DataRow dr6 = drs6[0];
                this.node_production_check_user_id = dr6["user_id"]+"";
                this.node_production_check_user_name = dr6["user_name"]+"";
                this.node_production_check_start_time = dr6["start_time"]+"";
                this.node_production_check_end_time = dr6["end_time"]+"";
                this.node_production_check_note = dr6["note"]+"";
                this.node_production_check_state = dr6["state"]+"";
            }
            else
            {
                this.node_production_check_user_id = "";
            }
            #endregion

            #region 采购部长反馈分工
            DataRow[] drs7 = dt_node.Select("node_definition_id=7");
            if (drs7.Length != 0)
            {
                DataRow dr7 = drs7[0];
                this.node_purchase_divide_user_id = dr7["user_id"]+"";
                this.node_purchase_divide_user_name = dr7["user_name"]+"";
                this.node_purchase_divide_start_time = dr7["start_time"]+"";
                this.node_purchase_divide_end_time = dr7["end_time"]+"";
                this.node_purchase_divide_note = dr7["note"]+"";
                this.node_purchase_divide_state = dr7["state"]+"";
            }
            else
            {
                this.node_purchase_divide_user_id = "";
            }
            #endregion

            #region 采购部黑色金属反馈
            DataRow[] drs8 = dt_node.Select("node_definition_id=8");
            if (drs8.Length != 0)
            {
                DataRow dr8 = drs8[0];
                this.node_ferrous_dep_user_id = dr8["user_id"]+"";
                this.node_ferrous_dep_user_name = dr8["user_name"]+"";
                this.node_ferrous_dep_start_time = dr8["start_time"]+"";
                this.node_ferrous_dep_end_time = dr8["end_time"]+"";
                this.node_ferrous_dep_note = dr8["note"]+"";
                this.node_ferrous_dep_state = dr8["state"]+"";
            }
            else
            {
                this.node_ferrous_dep_user_id = "";
            }
            #endregion

            #region 采购部外购件反馈
            DataRow[] drs9 = dt_node.Select("node_definition_id=9");
            if (drs9.Length != 0)
            {
                DataRow dr9 = drs9[0];
                this.node_purchasepart_dep_user_id = dr9["user_id"]+"";
                this.node_purchasepart_dep_user_name = dr9["user_name"]+"";
                this.node_purchasepart_dep_start_time = dr9["start_time"]+"";
                this.node_purchasepart_dep_end_time = dr9["end_time"]+"";
                this.node_purchasepart_dep_note = dr9["note"]+"";
                this.node_purchasepart_dep_state = dr9["state"]+"";
            }
            else
            {
                this.node_purchasepart_dep_user_id = "";
            }
            #endregion

            #region 采购部油漆涂料反馈
            DataRow[] drs10 = dt_node.Select("node_definition_id=10");
            if (drs10.Length != 0)
            {
                DataRow dr10 = drs10[0];
                this.node_paint_dep_user_id = dr10["user_id"]+"";
                this.node_paint_dep_user_name = dr10["user_name"]+"";
                this.node_paint_dep_start_time = dr10["start_time"]+"";
                this.node_paint_dep_end_time = dr10["end_time"]+"";
                this.node_paint_dep_note = dr10["note"]+"";
                this.node_paint_dep_state = dr10["state"]+"";
            }
            else
            {
                this.node_paint_dep_user_id = "";
            }
            #endregion

            #region 采购部电器电料反馈
            DataRow[] drs11 = dt_node.Select("node_definition_id=11");
            if (drs11.Length != 0)
            {
                DataRow dr11 = drs11[0];
                this.node_electrical_dep_user_id = dr11["user_id"]+"";
                this.node_electrical_dep_user_name = dr11["user_name"]+"";
                this.node_electrical_dep_start_time = dr11["start_time"]+"";
                this.node_electrical_dep_end_time = dr11["end_time"]+"";
                this.node_electrical_dep_note = dr11["note"]+"";
                this.node_electrical_dep_state = dr11["state"]+"";
            }
            else
            {
                this.node_electrical_dep_user_id ="";
            }
            #endregion

            #region 采购部铸锻件反馈
            DataRow[] drs12 = dt_node.Select("node_definition_id=12");
            if (drs12.Length != 0)
            {
                DataRow dr12 = drs12[0];
                this.node_casting_dep_user_id = dr12["user_id"]+"";
                this.node_casting_dep_user_name = dr12["user_name"]+"";
                this.node_casting_dep_start_time = dr12["start_time"]+"";
                this.node_casting_dep_end_time = dr12["end_time"]+"";
                this.node_casting_dep_note = dr12["note"]+"";
                this.node_casting_dep_state = dr12["state"]+"";
            }
            else
            {
                this.node_casting_dep_user_id = "";
            }
            #endregion

            #region 采购部其他材料反馈
            DataRow[] drs13 = dt_node.Select("node_definition_id=13");
            if (drs13.Length != 0)
            {
                DataRow dr13 = drs13[0];
                this.node_othermat_dep_user_id = dr13["user_id"]+"";
                this.node_othermat_dep_user_name = dr13["user_name"]+"";
                this.node_othermat_dep_start_time = dr13["start_time"]+"";
                this.node_othermat_dep_end_time = dr13["end_time"]+"";
                this.node_othermat_dep_note = dr13["note"]+"";
                this.node_othermat_dep_state = dr13["state"]+"";
            }
            else
            {
                this.node_othermat_dep_user_id = "";
            }
            #endregion

            #region 采购部长反馈审核
            DataRow[] drs14 = dt_node.Select("node_definition_id=14");
            if (drs14.Length != 0)
            {
                DataRow dr14 = drs14[0];
                this.node_purchase_check_user_id = dr14["user_id"]+"";
                this.node_purchase_check_user_name = dr14["user_name"]+"";
                this.node_purchase_check_start_time = dr14["start_time"]+"";
                this.node_purchase_check_end_time = dr14["end_time"]+"";
                this.node_purchase_check_note = dr14["note"]+"";
                this.node_purchase_check_state = dr14["state"]+"";
            }
            else
            {
                this.node_purchase_check_user_id = "";
            }
            #endregion

            #region 财务部预算调整
            DataRow[] drs15 = dt_node.Select("node_definition_id=15");
            if (drs15.Length != 0)
            {
                DataRow dr15 = drs15[0];
                this.node_budget_adjust_user_id = dr15["user_id"]+"";
                this.node_budget_adjust_user_name = dr15["user_name"]+"";
                this.node_budget_adjust_start_time = dr15["start_time"]+"";
                this.node_budget_adjust_end_time = dr15["end_time"]+"";
                this.node_budget_adjust_note = dr15["note"]+"";
                this.node_budget_adjust_state = dr15["state"]+"";
            }
            else
            {
                this.node_budget_adjust_user_id = "";
            }
            #endregion

            #region 财务部长预算审核
            DataRow[] drs16 = dt_node.Select("node_definition_id=16");
            if (drs16.Length != 0)
            {
                DataRow dr16 = drs16[0];
                this.node_budget_check_user_id = dr16["user_id"]+"";
                this.node_budget_check_user_name = dr16["user_name"]+"";
                this.node_budget_check_start_time = dr16["start_time"]+"";
                this.node_budget_check_end_time = dr16["end_time"]+"";
                this.node_budget_check_note = dr16["note"]+"";
                this.node_budget_check_state = dr16["state"]+"";
            }
            else
            {
                this.node_budget_check_user_id = "";
            }
            #endregion
            #endregion

        }

        #region YS_TASK_BUDGET表的字段
        /// <summary>
        /// 任务预算id
        /// </summary>
        public string task_budget_id { get; set; }
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
        /// 任务号重量
        /// </summary>
        public string task_weight { get; set; }
        /// <summary>
        /// 任务号类型（区分任务号种类，用于比较相同类型任务号的预算）
        /// </summary>
        public string task_type { get; set; }
        /// <summary>
        /// 财务部下发：所有材料费预算
        /// </summary>
        public string total_material_budget_pre { get; set; }
        /// <summary>
        /// 财务部下发：直接人工费预算
        /// </summary>
        public string labour_budget_pre { get; set; }
        /// <summary>
        /// 财务部下发：厂内分包费预算
        /// </summary>
        public string teamwork_budget_pre { get; set; }
        /// <summary>
        /// 财务部下发：生产外协费预算
        /// </summary>
        public string cooperative_budget_pre { get; set; }
        /// <summary>
        /// 财务部下发：任务号总预算（计算列，只读）
        /// </summary>
        public string c_total_task_budget_pre { get; set; }        
        /// <summary>
        /// 财务部确定：所有材料费预算
        /// </summary>
        public string total_material_budget { get; set; }
        /// <summary>
        /// 财务部确定：直接人工费预算
        /// </summary>
        public string labour_budget { get; set; }
        /// <summary>
        /// 财务部确定：厂内分包费预算
        /// </summary>
        public string teamwork_budget { get; set; }
        /// <summary>
        /// 财务部确定：生产外协费预算
        /// </summary>
        public string cooperative_budget { get; set; }
        /// <summary>
        /// 财务部确定：任务号总预算（计算列，只读）
        /// </summary>
        public string c_total_task_budget { get; set; }
        /// <summary>
        /// 财务部确定：财务部长对预算的审批结果
        /// 1：同意；2：不同意
        /// </summary>
        public string budget_check { get; set; }
        /// <summary>
        /// 采购部参考值：黑色金属费用
        /// </summary>
        public string ferrous_dep { get; set; }
        /// <summary>
        /// 采购部参考值：外购件费用
        /// </summary>
        public string purchasepart_dep { get; set; }
        /// <summary>
        /// 采购部参考值：油漆涂料费用
        /// </summary>
        public string paint_dep { get; set; }
        /// <summary>
        /// 采购部参考值：电器电料费用
        /// </summary>
        public string electrical_dep { get; set; }
        /// <summary>
        /// 采购部参考值：铸锻件费用
        /// </summary>
        public string casting_dep { get; set; }
        /// <summary>
        /// 采购部参考值：其他材料费用
        /// </summary>
        public string othermat_dep { get; set; }
        /// <summary>
        /// 采购部参考值：所有材料费用（计算列，只读）
        /// </summary>
        public string c_total_material_dep { get; set; }
        /// <summary>
        /// 采购部确定：采购部长对反馈的审批结果
        /// 1：同意；2：不同意
        /// </summary>
        public string purchase_check { get; set; }
        /// <summary>
        /// 生产部参考值：直接人工费用
        /// </summary>
        public string labour_dep { get; set; }
        /// <summary>
        /// 生产部参考值：厂内分包费用
        /// </summary>
        public string teamwork_dep { get; set; }
        /// <summary>
        /// 生产部参考值：生产外协费用
        /// </summary>
        public string cooperative_dep { get; set; }
        /// <summary>
        /// 生产部确定：生产部长对反馈的审批结果
        /// 1：同意；2：不同意
        /// </summary>
        public string production_check { get; set; }
        /// <summary>
        /// 历史参考值：黑色金属费用
        /// </summary>
        public string ferrous_his { get; set; }
        /// <summary>
        /// 历史参考值：外购件费用
        /// </summary>
        public string purchasepart_his { get; set; }
        /// <summary>
        /// 历史参考值：油漆涂料费用
        /// </summary>
        public string paint_his { get; set; }
        /// <summary>
        /// 历史参考值：电器电料费用
        /// </summary>
        public string electrical_his { get; set; }
        /// <summary>
        /// 历史参考值：铸锻件费用
        /// </summary>
        public string casting_his { get; set; }
        /// <summary>
        /// 历史参考值：其他材料费用
        /// </summary>
        public string othermat_his { get; set; }
        /// <summary>
        /// 历史参考值：所有材料费用（计算列，只读）
        /// </summary>
        public string c_total_material_his { get; set; }
        /// <summary>
        /// 财务部：预算编制人id
        /// </summary>
        public string maker_id { get; set; }
        /// <summary>
        /// 预算编制开始时间（以技术部提交时间为准）
        /// </summary>
        public string start_time { get; set; }
        /// <summary>
        /// 预算编制结束时间（以财务部确认预算编制完成时间为准）
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 预算编制状态（1：初步预算，2：部门反馈，3：预算调整，4：预算审核，5：编制完成）
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 预算编制备注，预算编制人填写
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 当前预算编制的第一个node实例id
        /// </summary>
        public string start_node_instance_id { get; set; }

        #endregion


        #region node实例的字段（每个node的责任人id、责任人姓名、开始时间、结束时间、备注）

        #region 财务部预算编制
        /// <summary>
        /// 财务部预算编制责任人id
        /// </summary>
        public string node_budget_edit_user_id { set; get; }
        /// <summary>
        /// 财务部预算编制责任人姓名
        /// </summary>
        public string node_budget_edit_user_name { set; get; }
        /// <summary>
        /// 财务部预算编制开始时间
        /// </summary>
        public string node_budget_edit_start_time { set; get; }
        /// <summary>
        /// 财务部预算编制结束时间
        /// </summary>
        public string node_budget_edit_end_time { set; get; }
        /// <summary>
        /// 财务部预算编制备注
        /// </summary>
        public string node_budget_edit_note { set; get; }
        /// 财务部预算编制状态
        /// </summary>
        public string node_budget_edit_state { set; get; }
        #endregion


        #region 生产部长反馈分工
        /// 生产部反馈分工状态
        /// </summary>
        public string node_production_divide_state { set; get; }
        /// <summary>
        /// 生产部长反馈分工责任人id
        /// </summary>
        public string node_production_divide_user_id { set; get; }
        /// <summary>
        /// 生产部长反馈分工责任人姓名
        /// </summary>
        public string node_production_divide_user_name { set; get; }
        /// <summary>
        /// 生产部长反馈分工开始时间
        /// </summary>
        public string node_production_divide_start_time { set; get; }
        /// <summary>
        /// 生产部长反馈分工结束时间
        /// </summary>
        public string node_production_divide_end_time { set; get; }
        /// <summary>
        /// 生产部长反馈分工备注
        /// </summary>
        public string node_production_divide_note { set; get; }
        #endregion

        #region 生产部人工费反馈
        /// 生产部人工费反馈状态
        /// </summary>
        public string node_labour_dep_state { set; get; }
        /// <summary>
        /// 生产部人工费反馈责任人id
        /// </summary>
        public string node_labour_dep_user_id { set; get; }
        /// <summary>
        /// 生产部人工费反馈责任人姓名
        /// </summary>
        public string node_labour_dep_user_name { set; get; }
        /// <summary>
        /// 生产部人工费反馈开始时间
        /// </summary>
        public string node_labour_dep_start_time { set; get; }
        /// <summary>
        /// 生产部人工费反馈结束时间
        /// </summary>
        public string node_labour_dep_end_time { set; get; }
        /// <summary>
        /// 生产部人工费反馈备注
        /// </summary>
        public string node_labour_dep_note { set; get; }
        #endregion

        #region 生产部分包费反馈
        /// 生产部分包费反馈状态
        /// </summary>
        public string node_teamwork_dep_state { set; get; }
        /// <summary>
        /// 生产部分包费反馈责任人id
        /// </summary>
        public string node_teamwork_dep_user_id { set; get; }
        /// <summary>
        /// 生产部分包费反馈责任人姓名
        /// </summary>
        public string node_teamwork_dep_user_name { set; get; }
        /// <summary>
        /// 生产部分包费反馈开始时间
        /// </summary>
        public string node_teamwork_dep_start_time { set; get; }
        /// <summary>
        /// 生产部分包费反馈结束时间
        /// </summary>
        public string node_teamwork_dep_end_time { set; get; }
        /// <summary>
        /// 生产部分包费反馈备注
        /// </summary>
        public string node_teamwork_dep_note { set; get; }
        #endregion

        #region 生产部外协费反馈
        /// 生产部外协费反馈状态
        /// </summary>
        public string node_cooperative_dep_state { set; get; }
        /// <summary>
        /// 生产部外协费反馈责任人id
        /// </summary>
        public string node_cooperative_dep_user_id { set; get; }
        /// <summary>
        /// 生产部外协费反馈责任人姓名
        /// </summary>
        public string node_cooperative_dep_user_name { set; get; }
        /// <summary>
        /// 生产部外协费反馈开始时间
        /// </summary>
        public string node_cooperative_dep_start_time { set; get; }
        /// <summary>
        /// 生产部外协费反馈结束时间
        /// </summary>
        public string node_cooperative_dep_end_time { set; get; }
        /// <summary>
        /// 生产部外协费反馈备注
        /// </summary>
        public string node_cooperative_dep_note { set; get; }
        #endregion

        #region 生产部长反馈审核
        /// 生产部长反馈审核状态
        /// </summary>
        public string node_production_check_state { set; get; }
        /// <summary>
        /// 生产部长反馈审核责任人id
        /// </summary>
        public string node_production_check_user_id { set; get; }
        /// <summary>
        /// 生产部长反馈审核责任人姓名
        /// </summary>
        public string node_production_check_user_name { set; get; }
        /// <summary>
        /// 生产部长反馈审核开始时间
        /// </summary>
        public string node_production_check_start_time { set; get; }
        /// <summary>
        /// 生产部长反馈审核结束时间
        /// </summary>
        public string node_production_check_end_time { set; get; }
        /// <summary>
        /// 生产部长反馈审核备注
        /// </summary>
        public string node_production_check_note { set; get; }
        #endregion



        #region 采购部长反馈分工
        /// 采购部长反馈分工状态
        /// </summary>
        public string node_purchase_divide_state { set; get; }
        /// <summary>
        /// 采购部长反馈分工责任人id
        /// </summary>
        public string node_purchase_divide_user_id { set; get; }
        /// <summary>
        /// 采购部长反馈分工责任人姓名
        /// </summary>
        public string node_purchase_divide_user_name { set; get; }
        /// <summary>
        /// 采购部长反馈分工开始时间
        /// </summary>
        public string node_purchase_divide_start_time { set; get; }
        /// <summary>
        /// 采购部长反馈分工结束时间
        /// </summary>
        public string node_purchase_divide_end_time { set; get; }
        /// <summary>
        /// 采购部长反馈分工备注
        /// </summary>
        public string node_purchase_divide_note { set; get; }
        #endregion

        #region 采购部黑色金属反馈
        /// 采购部黑色金属馈状态
        /// </summary>
        public string node_ferrous_dep_state { set; get; }
        /// <summary>
        /// 采购部黑色金属反馈责任人id
        /// </summary>
        public string node_ferrous_dep_user_id { set; get; }
        /// <summary>
        /// 采购部黑色金属反馈责任人姓名
        /// </summary>
        public string node_ferrous_dep_user_name { set; get; }
        /// <summary>
        /// 采购部黑色金属反馈开始时间
        /// </summary>
        public string node_ferrous_dep_start_time { set; get; }
        /// <summary>
        /// 采购部黑色金属反馈结束时间
        /// </summary>
        public string node_ferrous_dep_end_time { set; get; }
        /// <summary>
        /// 采购部黑色金属反馈备注
        /// </summary>
        public string node_ferrous_dep_note { set; get; }
        #endregion

        #region 采购部外购件反馈
        /// 采购部外购件馈状态
        /// </summary>
        public string node_purchasepart_dep_state { set; get; }
        /// <summary>
        /// 采购部外购件反馈责任人id
        /// </summary>
        public string node_purchasepart_dep_user_id { set; get; }
        /// <summary>
        /// 采购部外购件反馈责任人姓名
        /// </summary>
        public string node_purchasepart_dep_user_name { set; get; }
        /// <summary>
        /// 采购部外购件反馈开始时间
        /// </summary>
        public string node_purchasepart_dep_start_time { set; get; }
        /// <summary>
        /// 采购部外购件反馈结束时间
        /// </summary>
        public string node_purchasepart_dep_end_time { set; get; }
        /// <summary>
        /// 采购部外购件反馈备注
        /// </summary>
        public string node_purchasepart_dep_note { set; get; }
        #endregion

        #region 采购部油漆涂料反馈
        /// 采购部油漆涂料馈状态
        /// </summary>
        public string node_paint_dep_state { set; get; }
        /// <summary>
        /// 采购部油漆涂料反馈责任人id
        /// </summary>
        public string node_paint_dep_user_id { set; get; }
        /// <summary>
        /// 采购部油漆涂料反馈责任人姓名
        /// </summary>
        public string node_paint_dep_user_name { set; get; }
        /// <summary>
        /// 采购部油漆涂料反馈开始时间
        /// </summary>
        public string node_paint_dep_start_time { set; get; }
        /// <summary>
        /// 采购部油漆涂料反馈结束时间
        /// </summary>
        public string node_paint_dep_end_time { set; get; }
        /// <summary>
        /// 采购部油漆涂料反馈备注
        /// </summary>
        public string node_paint_dep_note { set; get; }
        #endregion

        #region 采购部电器电料反馈
        /// 采购部电器电料馈状态
        /// </summary>
        public string node_electrical_dep_state { set; get; }
        /// <summary>
        /// 采购部电器电料反馈责任人id
        /// </summary>
        public string node_electrical_dep_user_id { set; get; }
        /// <summary>
        /// 采购部电器电料反馈责任人姓名
        /// </summary>
        public string node_electrical_dep_user_name { set; get; }
        /// <summary>
        /// 采购部电器电料反馈开始时间
        /// </summary>
        public string node_electrical_dep_start_time { set; get; }
        /// <summary>
        /// 采购部电器电料反馈结束时间
        /// </summary>
        public string node_electrical_dep_end_time { set; get; }
        /// <summary>
        /// 采购部电器电料反馈备注
        /// </summary>
        public string node_electrical_dep_note { set; get; }
        #endregion

        #region 采购部铸锻件反馈
        /// 采购部铸锻件馈状态
        /// </summary>
        public string node_casting_dep_state { set; get; }
        /// <summary>
        /// 采购部铸锻件反馈责任人id
        /// </summary>
        public string node_casting_dep_user_id { set; get; }
        /// <summary>
        /// 采购部铸锻件反馈责任人姓名
        /// </summary>
        public string node_casting_dep_user_name { set; get; }
        /// <summary>
        /// 采购部铸锻件反馈开始时间
        /// </summary>
        public string node_casting_dep_start_time { set; get; }
        /// <summary>
        /// 采购部铸锻件反馈结束时间
        /// </summary>
        public string node_casting_dep_end_time { set; get; }
        /// <summary>
        /// 采购部铸锻件反馈备注
        /// </summary>
        public string node_casting_dep_note { set; get; }
        #endregion

        #region 采购部其他材料反馈
        /// 采购部其他材料反馈状态
        /// </summary>
        public string node_othermat_dep_state { set; get; }
        /// <summary>
        /// 采购部其他材料反馈责任人id
        /// </summary>
        public string node_othermat_dep_user_id { set; get; }
        /// <summary>
        /// 采购部其他材料反馈责任人姓名
        /// </summary>
        public string node_othermat_dep_user_name { set; get; }
        /// <summary>
        /// 采购部其他材料反馈开始时间
        /// </summary>
        public string node_othermat_dep_start_time { set; get; }
        /// <summary>
        /// 采购部其他材料反馈结束时间
        /// </summary>
        public string node_othermat_dep_end_time { set; get; }
        /// <summary>
        /// 采购部其他材料反馈备注
        /// </summary>
        public string node_othermat_dep_note { set; get; }
        #endregion

        #region 采购部长反馈审核
        /// 采购部长反馈审核状态
        /// </summary>
        public string node_purchase_check_state { set; get; }
        /// <summary>
        /// 采购部长反馈审核责任人id
        /// </summary>
        public string node_purchase_check_user_id { set; get; }
        /// <summary>
        /// 采购部长反馈审核责任人姓名
        /// </summary>
        public string node_purchase_check_user_name { set; get; }
        /// <summary>
        /// 采购部长反馈审核开始时间
        /// </summary>
        public string node_purchase_check_start_time { set; get; }
        /// <summary>
        /// 采购部长反馈审核结束时间
        /// </summary>
        public string node_purchase_check_end_time { set; get; }
        /// <summary>
        /// 采购部长反馈审核备注
        /// </summary>
        public string node_purchase_check_note { set; get; }
        #endregion


        #region 财务部预算调整
        /// 财务部预算调整状态
        /// </summary>
        public string node_budget_adjust_state { set; get; }
        /// <summary>
        /// 财务部预算调整责任人id
        /// </summary>
        public string node_budget_adjust_user_id { set; get; }
        /// <summary>
        /// 财务部预算调整责任人姓名
        /// </summary>
        public string node_budget_adjust_user_name { set; get; }
        /// <summary>
        /// 财务部预算调整开始时间
        /// </summary>
        public string node_budget_adjust_start_time { set; get; }
        /// <summary>
        /// 财务部预算调整结束时间
        /// </summary>
        public string node_budget_adjust_end_time { set; get; }
        /// <summary>
        /// 财务部预算调整备注
        /// </summary>
        public string node_budget_adjust_note { set; get; }
        #endregion

        #region 财务部长预算审核
        /// 财务部预算审核状态
        /// </summary>
        public string node_budget_check_state { set; get; }
        /// <summary>
        /// 财务部长预算审核责任人id
        /// </summary>
        public string node_budget_check_user_id { set; get; }
        /// <summary>
        /// 财务部长预算审核责任人姓名
        /// </summary>
        public string node_budget_check_user_name { set; get; }
        /// <summary>
        /// 财务部长预算审核开始时间
        /// </summary>
        public string node_budget_check_start_time { set; get; }
        /// <summary>
        /// 财务部长预算审核结束时间
        /// </summary>
        public string node_budget_check_end_time { set; get; }
        /// <summary>
        /// 财务部长预算审核备注
        /// </summary>
        public string node_budget_check_note { set; get; }
        #endregion



        #endregion
    }
}
