using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using ZCZJ_DPF;



namespace ZCZJ_DPF.YS_Data
{
    public partial class ys_task_budget_detail :BasicPage
    {
        string task_code;
        YS_Data.BLL.ys_task_budget_detail bll = new YS_Data.BLL.ys_task_budget_detail();
        YS_Data.Model.TaskBudget tb;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                task_code = Request.QueryString["tsak_code"].ToString();
                bindControlerValue();
                bindRepeater();
            }


        }
        /// <summary>
        /// 绑定各个控件的值
        /// </summary>
        protected void bindControlerValue()
        {
            tb = new YS_Data.Model.TaskBudget(task_code);
            lb_task_code.Text = tb.task_code;
            lb_contract_code.Text = tb.contract_code;
            lb_project_name.Text = tb.project_name;

            lb_c_total_task_budget.Text = tb.c_total_task_budget;
            txt_total_material_budget.Text = tb.total_material_budget;
            txt_labour_budget.Text = tb.labour_budget;
            txt_teamwork_budget.Text = tb.teamwork_budget;
            txt_coopreative_budget.Text = tb.cooperative_budget;

            txt_labour_dep.Text = tb.labour_dep;
            txt_node_labour_dep_note.Text = tb.node_labour_dep_note;
            lb_node_labour_dep_endtime.Text = tb.node_labour_dep_end_time;
            if (string.IsNullOrEmpty(tb.node_labour_dep_user_name))
                ddl_node_labour_dep_user_name.Text = tb.node_labour_dep_user_name;

            txt_teamwork_dep.Text = tb.teamwork_dep;
            txt_node_teamwork_dep_note.Text = tb.node_teamwork_dep_note;
            lb_node_teamwork_dep_endtime.Text = tb.node_teamwork_dep_end_time;
            if (string.IsNullOrEmpty(tb.node_teamwork_dep_user_name))
                ddl_node_teamwork_dep_user_name.Text = tb.node_teamwork_dep_user_name;

            txt_cooperative_dep.Text = tb.cooperative_dep;
            txt_node_cooperative_dep_note.Text = tb.node_cooperative_dep_note;
            lb_node_cooperative_dep_endtime.Text = tb.node_cooperative_dep_end_time;
            if (string.IsNullOrEmpty(tb.node_cooperative_dep_user_name))
                ddl_node_cooperative_dep_user_name.Text = tb.node_cooperative_dep_user_name;

            lb_c_total_material_dep.Text = tb.c_total_material_dep;
            lb_c_total_material_his.Text = tb.c_total_material_his;

            txt_ferrous_dep.Text = tb.ferrous_dep;
            txt_node_ferrous_dep_note.Text = tb.node_ferrous_dep_note;
            lb_ferrous_his.Text = tb.ferrous_his;
            lb_node_ferrous_dep_endtime.Text = tb.node_ferrous_dep_end_time;
            if (string.IsNullOrEmpty(tb.node_ferrous_dep_user_name))
                ddl_node_ferrous_dep_user_name.Text = tb.node_ferrous_dep_user_name;

            txt_purchasepart_dep.Text = tb.purchasepart_dep;
            txt_node_purchasepart_dep_note.Text = tb.node_purchasepart_dep_note;
            lb_purchasepart_his.Text = tb.purchasepart_his;
            lb_node_purchasepart_dep_endtime.Text = tb.node_purchasepart_dep_end_time;
            if (string.IsNullOrEmpty(tb.node_purchasepart_dep_user_name))
                ddl_node_purchasepart_dep_user_name.Text = tb.node_purchasepart_dep_user_name;

            txt_paint_dep.Text = tb.paint_dep;
            txt_node_paint_dep_note.Text = tb.node_paint_dep_note;
            lb_paint_his.Text = tb.paint_his;
            lb_node_paint_dep_endtime.Text = tb.node_paint_dep_end_time;
            if (string.IsNullOrEmpty(tb.node_paint_dep_user_name))
                ddl_node_paint_dep_user_name.Text = tb.node_paint_dep_user_name;



            txt_electrical_dep.Text = tb.electrical_dep;
            txt_node_electrical_dep_note.Text = tb.node_electrical_dep_note;
            lb_electrical_his.Text = tb.electrical_his;
            lb_node_electrical_dep_endtime.Text = tb.node_electrical_dep_end_time;
            if (string.IsNullOrEmpty(tb.node_electrical_dep_user_name))
                ddl_node_electrical_dep_user_name.Text = tb.node_electrical_dep_user_name;


            txt_casting_dep.Text = tb.casting_dep;
            txt_node_casting_dep_note.Text = tb.node_casting_dep_note;
            lb_casting_his.Text = tb.casting_his;
            lb_node_casting_dep_endtime.Text = tb.node_casting_dep_end_time;
            if (string.IsNullOrEmpty(tb.node_casting_dep_user_name))
                ddl_node_casting_dep_user_name.Text = tb.node_casting_dep_user_name;


            txt_othermat_dep.Text = tb.othermat_dep;
            txt_node_othermat_dep_note.Text = tb.node_othermat_dep_note;
            lb_othermat_his.Text = tb.othermat_his;
            lb_node_othermat_dep_endtime.Text = tb.node_othermat_dep_end_time;
            if (string.IsNullOrEmpty(tb.node_othermat_dep_user_name))
                ddl_node_othermat_dep_user_name.Text = tb.node_othermat_dep_user_name;




        }


        /// <summary>
        /// 绑定各个类材料明细
        /// </summary>
        protected void bindRepeater()
        {
            bll.bindMaterialRepeater(rpt_ferrous, pal_no_ferrous, task_code, "material_code like '01.07%'");
            bll.bindMaterialRepeater(rpt_purchase, pal_no_purchase, task_code, "material_code like '01.11%'");
            bll.bindMaterialRepeater(rpt_paint, pal_no_paint, task_code, "material_code like '01.15%'");
            bll.bindMaterialRepeater(rpt_electrical, pal_no_electrical, task_code, "material_code like '01.03%'");
            bll.bindMaterialRepeater(rpt_casting, pal_no_casting, task_code, "material_code like '01.08%' or material_code like '01.09%'");
            bll.bindMaterialRepeater(rpt_other, pal_no_other, task_code, @"material_code NOT  LIKE '01.07%' AND material_code NOT  LIKE '01.11%' 
                  AND material_code NOT  LIKE '01.15%' AND material_code NOT  LIKE '01.03%' AND material_code NOT  LIKE '01.08%' AND material_code NOT  LIKE '01.09%'");
            bll.bindTaskRepeater(rpt_type, pal_no_type, task_code,"");
        }
    }
}
