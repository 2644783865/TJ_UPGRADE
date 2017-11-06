using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    public partial class ys_task_budget_detail : BasicPage
    {
        string task_code;
        YS_Data.BLL.ys_task_budget_detail bll = new YS_Data.BLL.ys_task_budget_detail();
        YS_Data.Model.TaskBudget tb;
        protected void Page_Load(object sender, EventArgs e)
        {
            task_code = Request.QueryString["tsak_code"].ToString();
            tb = new YS_Data.Model.TaskBudget(task_code);

            if (!IsPostBack)
            {

                bindControlerValue();
                bindRepeater();
            }


        }
        /// <summary>
        /// 绑定各个控件的值
        /// </summary>
        protected void bindControlerValue()
        {
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
            txt_node_labour_dep_user_name.Text = tb.node_labour_dep_user_name;
            txt_node_labour_dep_user_id.Text = tb.node_othermat_dep_user_id;

            txt_teamwork_dep.Text = tb.teamwork_dep;
            txt_node_teamwork_dep_note.Text = tb.node_teamwork_dep_note;
            lb_node_teamwork_dep_endtime.Text = tb.node_teamwork_dep_end_time;
            txt_node_teamwork_dep_user_name.Text = tb.node_teamwork_dep_user_name;
            txt_node_teamwork_dep_user_id.Text = tb.node_othermat_dep_user_id;

            txt_cooperative_dep.Text = tb.cooperative_dep;
            txt_node_cooperative_dep_note.Text = tb.node_cooperative_dep_note;
            lb_node_cooperative_dep_endtime.Text = tb.node_cooperative_dep_end_time;
            txt_node_cooperative_dep_user_name.Text = tb.node_cooperative_dep_user_name;
            txt_node_cooperative_dep_user_id.Text = tb.node_othermat_dep_user_id;

            txt_ferrous_dep.Text = tb.ferrous_dep;
            txt_node_ferrous_dep_note.Text = tb.node_ferrous_dep_note;
            lb_ferrous_his.Text = tb.ferrous_his;
            lb_node_ferrous_dep_endtime.Text = tb.node_ferrous_dep_end_time;
            txt_node_ferrous_dep_user_name.Text = tb.node_ferrous_dep_user_name;
            txt_node_ferrous_dep_user_id.Text = tb.node_othermat_dep_user_id;

            txt_purchasepart_dep.Text = tb.purchasepart_dep;
            txt_node_purchasepart_dep_note.Text = tb.node_purchasepart_dep_note;
            lb_purchasepart_his.Text = tb.purchasepart_his;
            lb_node_purchasepart_dep_endtime.Text = tb.node_purchasepart_dep_end_time;
            txt_node_purchasepart_dep_user_name.Text = tb.node_purchasepart_dep_user_name;
            txt_node_purchasepart_dep_user_id.Text = tb.node_othermat_dep_user_id;

            txt_paint_dep.Text = tb.paint_dep;
            txt_node_paint_dep_note.Text = tb.node_paint_dep_note;
            lb_paint_his.Text = tb.paint_his;
            lb_node_paint_dep_endtime.Text = tb.node_paint_dep_end_time;
            txt_node_paint_dep_user_name.Text = tb.node_paint_dep_user_name;
            txt_node_paint_dep_user_id.Text = tb.node_othermat_dep_user_id;



            txt_electrical_dep.Text = tb.electrical_dep;
            txt_node_electrical_dep_note.Text = tb.node_electrical_dep_note;
            lb_electrical_his.Text = tb.electrical_his;
            lb_node_electrical_dep_endtime.Text = tb.node_electrical_dep_end_time;
            txt_node_electrical_dep_user_name.Text = tb.node_electrical_dep_user_name;
            txt_node_electrical_dep_user_id.Text = tb.node_othermat_dep_user_id;


            txt_casting_dep.Text = tb.casting_dep;
            txt_node_casting_dep_note.Text = tb.node_casting_dep_note;
            lb_casting_his.Text = tb.casting_his;
            lb_node_casting_dep_endtime.Text = tb.node_casting_dep_end_time;
            txt_node_casting_dep_user_name.Text = tb.node_casting_dep_user_name;
            txt_node_casting_dep_user_id.Text = tb.node_othermat_dep_user_id;


            txt_othermat_dep.Text = tb.othermat_dep;
            txt_node_othermat_dep_note.Text = tb.node_othermat_dep_note;
            lb_othermat_his.Text = tb.othermat_his;
            lb_node_othermat_dep_endtime.Text = tb.node_othermat_dep_end_time;
            txt_node_othermat_dep_user_name.Text = tb.node_othermat_dep_user_name;
            txt_node_othermat_dep_user_id.Text = tb.node_othermat_dep_user_id;


            lb_c_total_material_dep.Text = tb.c_total_material_dep;
            lb_c_total_material_his.Text = tb.c_total_material_his;


            lb_node_production_check_user_name.Text = tb.node_production_check_user_name;
            rbl_production_check.SelectedIndex = Convert.ToInt16(tb.production_check);
            lb_node_production_check_endtime.Text = tb.node_production_check_end_time;
            txt_node_production_check_note.Text = tb.node_production_check_note;

            lb_node_purchase_check_user_name.Text = tb.node_purchase_check_user_name;
            rbl_purchase_check.SelectedIndex = Convert.ToInt16(tb.purchase_check);
            lb_node_purchase_check_endtime.Text = tb.node_purchase_check_end_time;
            txt_node_purchase_check_note.Text = tb.node_purchase_check_note;

            lb_node_budget_check_user_name.Text = tb.node_budget_check_user_name;
            rbl_budget_check.SelectedIndex = Convert.ToInt16(tb.budget_check);
            lb_node_budget_check_endtime.Text = tb.node_budget_check_end_time;
            txt_node_budget_check_note.Text = tb.node_budget_check_note;
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
            bll.bindTaskRepeater(rpt_type, pal_no_type, task_code, "");
        }


        /// <summary>
        /// 初步预算编制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_budget_submit_Click(object sender, EventArgs e)
        {

            tb.labour_budget = txt_labour_budget.Text.Trim();
            tb.teamwork_budget = txt_teamwork_budget.Text.Trim();
            tb.cooperative_budget = txt_coopreative_budget.Text.Trim();
            tb.total_material_budget = txt_total_material_budget.Text.Trim();
            bll.finishNode("1", tb);
            ((Button)sender).Visible = false;

        }

        /// <summary>
        /// 生产部分工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_production_divide_Click(object sender, EventArgs e)
        {
            tb.node_labour_dep_user_id = txt_node_labour_dep_user_id.Text.Trim();
            tb.node_teamwork_dep_user_id = txt_node_teamwork_dep_user_id.Text.Trim();
            tb.node_cooperative_dep_user_id = txt_node_cooperative_dep_user_id.Text.Trim();
            bll.finishNode("2", tb);
            ((Button)sender).Visible = false;
        }

        /// <summary>
        /// 人工费反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_labour_dep_Click(object sender, EventArgs e)
        {
            tb.labour_dep = txt_labour_dep.Text.Trim();
            tb.node_labour_dep_note = txt_node_labour_dep_note.Text.Trim();
            bll.finishNode("3", tb);
            ((Button)sender).Visible = false;
        }


        /// <summary>
        /// 分包费反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_teamwork_dep_Click(object sender, EventArgs e)
        {
            tb.teamwork_dep = txt_teamwork_dep.Text.Trim();
            tb.node_teamwork_dep_note = txt_node_teamwork_dep_note.Text.Trim();
            bll.finishNode("4", tb);
            ((Button)sender).Visible = false;
        }

        /// <summary>
        /// 外协费反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_cooperative_Click(object sender, EventArgs e)
        {
            tb.cooperative_dep = txt_cooperative_dep.Text.Trim();
            tb.node_cooperative_dep_note = txt_node_cooperative_dep_note.Text.Trim();
            bll.finishNode("5", tb);
            ((Button)sender).Visible = false;
        }

        //缺6

        /// <summary>
        /// 采购部分工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_purchase_divide_Click(object sender, EventArgs e)
        {
            tb.node_ferrous_dep_user_id = txt_node_ferrous_dep_user_id.Text.Trim();
            tb.node_purchasepart_dep_user_id = txt_node_purchasepart_dep_user_id.Text.Trim();
            tb.node_paint_dep_user_id = txt_node_paint_dep_user_id.Text.Trim();
            tb.node_electrical_dep_user_id = txt_node_electrical_dep_user_id.Text.Trim();
            tb.node_casting_dep_user_id = txt_node_casting_dep_user_id.Text.Trim();
            tb.node_othermat_dep_user_id = txt_node_othermat_dep_user_id.Text.Trim();
            bll.finishNode("7", tb);
            ((Button)sender).Visible = false;
        }

        /// <summary>
        /// 黑色金属反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ferrous_dep_Click(object sender, EventArgs e)
        {
            tb.ferrous_dep = txt_ferrous_dep.Text.Trim();
            tb.node_ferrous_dep_note = txt_node_ferrous_dep_note.Text.Trim();
            bll.finishNode("8", tb);
            ((Button)sender).Visible = false;
        }
        /// <summary>
        /// 外购件反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_purchasepart_dep_Click(object sender, EventArgs e)
        {
            tb.purchasepart_dep = txt_purchasepart_dep.Text.Trim();
            tb.node_purchasepart_dep_note = txt_node_purchasepart_dep_note.Text.Trim();
            bll.finishNode("9", tb);
            ((Button)sender).Visible = false;
        }
        /// <summary>
        /// 油漆涂料反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_paint_dep_Click(object sender, EventArgs e)
        {

            tb.paint_dep = txt_paint_dep.Text.Trim();
            tb.node_paint_dep_note = txt_node_paint_dep_note.Text.Trim();
            bll.finishNode("10", tb);
            ((Button)sender).Visible = false;
        }

        /// <summary>
        /// 电器电料反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_electrical_dep_Click(object sender, EventArgs e)
        {

            tb.electrical_dep = txt_electrical_dep.Text.Trim();
            tb.node_electrical_dep_note = txt_node_electrical_dep_note.Text.Trim();
            bll.finishNode("11", tb);
            ((Button)sender).Visible = false;
        }
        /// <summary>
        /// 铸锻件反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_casting_dep_Click(object sender, EventArgs e)
        {

            tb.casting_dep = txt_casting_dep.Text.Trim();
            tb.node_casting_dep_note = txt_node_casting_dep_note.Text.Trim();
            bll.finishNode("12", tb);
            ((Button)sender).Visible = false;
        }

        /// <summary>
        /// 其他材料反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_othermat_dep_Click(object sender, EventArgs e)
        {

            tb.othermat_dep = txt_othermat_dep.Text.Trim();
            tb.node_othermat_dep_note = txt_node_othermat_dep_note.Text.Trim();
            bll.finishNode("13", tb);
            ((Button)sender).Visible = false;
        }








        protected void btn_production_check_Click(object sender, EventArgs e)
        {
            tb.production_check = "0";
            tb.node_production_check_note = txt_node_production_check_note.Text.Trim();

            switch (rbl_production_check.SelectedIndex)
            {
                case 0:                    
                    bll.finishNode("6", tb);
                    ((Button)sender).Visible = false;
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }

        protected void btn_purchase_check_Click(object sender, EventArgs e)
        {
            tb.purchase_check = "0";
            tb.node_purchase_check_note = txt_node_purchase_check_note.Text.Trim();

            switch (rbl_purchase_check.SelectedIndex)
            {
                case 0:
                    bll.finishNode("14", tb);
                    ((Button)sender).Visible = false;
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 预算调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_budget_adjust_Click(object sender, EventArgs e)
        {

            tb.labour_budget = txt_labour_budget.Text.Trim();
            tb.teamwork_budget = txt_teamwork_budget.Text.Trim();
            tb.cooperative_budget = txt_coopreative_budget.Text.Trim();
            tb.total_material_budget = txt_total_material_budget.Text.Trim();
            bll.finishNode("15", tb);
            ((Button)sender).Visible = false;

        }

        protected void btn_budget_check_Click(object sender, EventArgs e)
        {
            tb.budget_check = "0";
            tb.node_budget_check_note = txt_node_budget_check_note.Text.Trim();

            switch (rbl_budget_check.SelectedIndex)
            {
                case 0:
                    bll.finishNode("16", tb);
                    ((Button)sender).Visible = false;
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }




        #region 选择审核结果触发的事件处理程序

        protected void rbl_production_check_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbl_production_check.SelectedIndex)
            {
                case 0: ckl_production_check.Visible = false;
                    break;
                case 1: ckl_production_check.Visible = true;
                    break;
                default:
                    break;
            }
        }

        protected void rbl_purchase_check_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbl_purchase_check.SelectedIndex)
            {
                case 0: ckl_purchase_check.Visible = false;
                    break;
                case 1:
                    //绑定数据
                    ckl_purchase_check.Visible = true;
                    break;
                default:
                    break;
            }
        }

        protected void rbl_budget_check_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbl_budget_check.SelectedIndex)
            {
                case 0: ckl_budget_check.Visible = false;
                    break;
                case 1: ckl_budget_check.Visible = true;
                    break;
                default:
                    break;
            }
        }

        #endregion


















    }
}
