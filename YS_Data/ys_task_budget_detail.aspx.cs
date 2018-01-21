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
using System.IO;
//using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using ZCZJ_DPF;



namespace ZCZJ_DPF.YS_Data
{
    public partial class ys_task_budget_detail : BasicPage
    {
        string task_code, userid;
        YS_Data.BLL.ys_task_budget_detail bll = new YS_Data.BLL.ys_task_budget_detail();
        YS_Data.Model.TaskBudget tb;
        protected void Page_Load(object sender, EventArgs e)
        {
            task_code = Request.QueryString["tsak_code"]+"";
            tb = new YS_Data.Model.TaskBudget(task_code);

            if (!IsPostBack)
            {               
                userid = Session["UserID"]+"";
                initControlUseable();
                bindControlValue();
                bindRepeater();
            }

        }
        /// <summary>
        /// 绑定各个控件的值
        /// </summary>
        protected void bindControlValue()
        {
            lb_task_code.Text = tb.task_code;
            lb_contract_code.Text = tb.contract_code;
            lb_project_name.Text = tb.project_name;
            lb_task_weight.Text = tb.task_weight;

            lb_c_total_task_budget_pre.Text = tb.c_total_task_budget_pre;
            txt_total_material_budget_pre.Text = tb.total_material_budget_pre;
            txt_labour_budget_pre.Text = tb.labour_budget_pre;
            txt_teamwork_budget_pre.Text = tb.teamwork_budget_pre;
            txt_coopreative_budget_pre.Text = tb.cooperative_budget_pre;

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
            bll.bindMaterialRepeater(rpt_casting, pal_no_casting, task_code, "(material_code like '01.08%' or material_code like '01.09%')");
            bll.bindMaterialRepeater(rpt_other, pal_no_other, task_code, @"(material_code NOT  LIKE '01.07%' AND material_code NOT  LIKE '01.11%' 
                  AND material_code NOT  LIKE '01.15%' AND material_code NOT  LIKE '01.03%' AND material_code NOT  LIKE '01.08%' AND material_code NOT  LIKE '01.09%')");
            bll.bindTaskRepeater(rpt_type, pal_no_type, task_code, tb.task_type);
        }

        /// <summary>
        /// 根据登陆人以及预算的节点控制空间的可见性
        /// </summary>
        protected void initControlUseable()
        {
            //预算编制
            if (tb.node_budget_edit_user_id.Equals(userid) && (tb.node_budget_edit_state.Equals("1") || tb.node_budget_edit_state.Equals("3")))
            {
                txt_labour_budget_pre.Enabled = true;
                txt_teamwork_budget_pre.Enabled = true;
                txt_coopreative_budget_pre.Enabled = true;
                txt_total_material_budget_pre.Enabled = true;
                btn_budget_submit.Visible = true;
            }
            //生产部分工
            if (tb.node_production_divide_user_id.Equals(userid) && (tb.node_production_divide_state.Equals("1") || tb.node_production_divide_state.Equals("3")))
            {
                txt_node_labour_dep_user_name.Enabled = true;
                img_labour.Visible = true;
                txt_node_teamwork_dep_user_name.Enabled = true;
                img_teamwork.Visible = true;
                txt_node_cooperative_dep_user_name.Enabled = true;
                img_cooperative.Visible = true;
                btn_production_divide.Visible = true;
            }
            //采购部分工
            if (tb.node_purchase_divide_user_id.Equals(userid) && (tb.node_purchase_divide_state.Equals("1") || tb.node_purchase_divide_state.Equals("3")))
            {
                txt_node_ferrous_dep_user_name.Enabled = true;
                img_ferrous.Visible = true;
                txt_node_purchasepart_dep_user_name.Enabled = true;
                img_purchasepart.Visible = true;
                txt_node_paint_dep_user_name.Enabled = true;
                img_paint.Visible = true;
                txt_node_electrical_dep_user_name.Enabled = true;
                img_electrical.Visible = true;
                txt_node_casting_dep_user_name.Enabled = true;
                img_casting.Visible = true;
                txt_node_othermat_dep_user_name.Enabled = true;
                img_othermat.Visible = true;
                btn_purchase_divide.Visible = true;
            }
            //人工费反馈
            if (tb.node_labour_dep_user_id.Equals(userid) && (tb.node_labour_dep_state.Equals("1") || tb.node_labour_dep_state.Equals("3")))
            {
                txt_labour_dep.Enabled = true;
                txt_node_labour_dep_note.Enabled = true;
                btn_labour_dep.Visible = true;
            }

            //分包费反馈
            if (tb.node_teamwork_dep_user_id.Equals(userid) && (tb.node_teamwork_dep_state.Equals("1") || tb.node_teamwork_dep_state.Equals("3")))
            {
                txt_teamwork_dep.Enabled = true;
                txt_node_teamwork_dep_note.Enabled = true;
                btn_teamwork_dep.Visible = true;
            }

            //外协费反馈
            if (tb.node_cooperative_dep_user_id.Equals(userid) && (tb.node_cooperative_dep_state.Equals("1") || tb.node_cooperative_dep_state.Equals("3")))
            {
                txt_cooperative_dep.Enabled = true;
                txt_node_cooperative_dep_note.Enabled = true;
                btn_cooperative_dep.Visible = true;
            }
            //黑色金属反馈
            if (tb.node_ferrous_dep_user_id.Equals(userid) && (tb.node_ferrous_dep_state.Equals("1") || tb.node_ferrous_dep_state.Equals("3")))
            {
                txt_ferrous_dep.Enabled = true;
                txt_node_ferrous_dep_note.Enabled = true;
                btn_ferrous_dep.Visible = true;
            }
            //外购件反馈
            if (tb.node_purchasepart_dep_user_id.Equals(userid) && (tb.node_purchasepart_dep_state.Equals("1") || tb.node_purchasepart_dep_state.Equals("3")))
            {
                txt_purchasepart_dep.Enabled = true;
                txt_node_purchasepart_dep_note.Enabled = true;
                btn_purchasepart_dep.Visible = true;
            }
            //油漆涂料反馈
            if (tb.node_paint_dep_user_id.Equals(userid) && (tb.node_paint_dep_state.Equals("1") || tb.node_paint_dep_state.Equals("3")))
            {
                txt_paint_dep.Enabled = true;
                txt_node_paint_dep_note.Enabled = true;
                btn_paint_dep.Visible = true;
            }
            //电器电料反馈
            if (tb.node_electrical_dep_user_id.Equals(userid) && (tb.node_electrical_dep_state.Equals("1") || tb.node_electrical_dep_state.Equals("3")))
            {
                txt_electrical_dep.Enabled = true;
                txt_node_electrical_dep_note.Enabled = true;
                btn_electrical_dep.Visible = true;
            }
            //铸锻件反馈
            if (tb.node_casting_dep_user_id.Equals(userid) && (tb.node_casting_dep_state.Equals("1") || tb.node_casting_dep_state.Equals("3")))
            {
                txt_casting_dep.Enabled = true;
                txt_node_casting_dep_note.Enabled = true;
                btn_casting_dep.Visible = true;
            }
            //其他材料反馈
            if (tb.node_othermat_dep_user_id.Equals(userid) && (tb.node_othermat_dep_state.Equals("1") || tb.node_othermat_dep_state.Equals("3")))
            {
                txt_othermat_dep.Enabled = true;
                txt_node_othermat_dep_note.Enabled = true;
                btn_othermat_dep.Visible = true;
            }
            //生产部审核
            if (tb.node_production_check_user_id.Equals(userid) && (tb.node_production_check_state.Equals("1") || tb.node_production_check_state.Equals("3")))
            {
                rbl_production_check.Enabled = true;
                txt_node_production_check_note.Enabled = true;
                btn_production_check.Visible = true;
            }
            //采购部审核
            if (tb.node_purchase_check_user_id.Equals(userid) && (tb.node_purchase_check_state.Equals("1") || tb.node_purchase_check_state.Equals("3")))
            {
                rbl_purchase_check.Enabled = true;
                txt_node_purchase_check_note.Enabled = true;
                btn_purchase_check.Visible = true;
            }
            //预算调整
            if (tb.node_budget_adjust_user_id.Equals(userid) && (tb.node_budget_adjust_state.Equals("1") || tb.node_budget_adjust_state.Equals("3")))
            {
                txt_labour_budget.Enabled = true;
                txt_teamwork_budget.Enabled = true;
                txt_coopreative_budget.Enabled = true;
                txt_total_material_budget.Enabled = true;
                btn_budget_adjust.Visible = true;
            }
            //财务部审核
            if (tb.node_budget_check_user_id.Equals(userid) && (tb.node_budget_check_state.Equals("1") || tb.node_budget_check_state.Equals("3")))
            {
                rbl_budget_check.Enabled = true;
                txt_node_budget_check_note.Enabled = true;
                btn_budget_check.Visible = true;
            }
        }

        #region 各个节点的处理程序
        /// <summary>
        /// 初步预算编制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_budget_submit_Click(object sender, EventArgs e)
        {
            tb.labour_budget_pre = txt_labour_budget_pre.Text.Trim();
            tb.teamwork_budget_pre = txt_teamwork_budget_pre.Text.Trim();
            tb.cooperative_budget_pre = txt_coopreative_budget_pre.Text.Trim();
            tb.total_material_budget_pre = txt_total_material_budget_pre.Text.Trim();
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

        /// <summary>
        /// 生产部长审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_production_check_Click(object sender, EventArgs e)
        {
            tb.production_check = rbl_production_check.SelectedIndex+"";
            tb.node_production_check_note = txt_node_production_check_note.Text.Trim();

            switch (rbl_production_check.SelectedIndex)
            {
                case 0://同意
                    bll.finishNode("6", tb);
                    ((Button)sender).Visible = false;
                    break;
                case 1://不同意
                    bll.rejectNode("6", tb, bll.getCheckBodListSelectedValue(ckl_production_check));
                    ((Button)sender).Visible = false;
                    break;
                default:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script>alert('请选择审核结果！');</script>");
                    break;
            }
        }

        /// <summary>
        /// 采购部长审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_purchase_check_Click(object sender, EventArgs e)
        {
            tb.purchase_check = rbl_purchase_check.SelectedIndex+"";
            tb.node_purchase_check_note = txt_node_purchase_check_note.Text.Trim();

            switch (rbl_purchase_check.SelectedIndex)
            {
                case 0://同意
                    bll.finishNode("14", tb);
                    ((Button)sender).Visible = false;
                    break;
                case 1://不同意
                    bll.rejectNode("14", tb, bll.getCheckBodListSelectedValue(ckl_purchase_check));
                    ((Button)sender).Visible = false;
                    break;
                default:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script>alert('请选择审核结果！');</script>");
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

        /// <summary>
        /// 财务部长预算审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_budget_check_Click(object sender, EventArgs e)
        {
            tb.budget_check = rbl_budget_check.SelectedIndex+"";
            tb.node_budget_check_note = txt_node_budget_check_note.Text.Trim();

            switch (rbl_budget_check.SelectedIndex)
            {
                case 0:
                    bll.finishNode("16", tb);
                    ((System.Web.UI.WebControls.Button)sender).Visible = false;
                    break;
                case 1:
                    bll.rejectNode("16", tb, bll.getCheckBodListSelectedValue(ckl_budget_check));
                    ((System.Web.UI.WebControls.Button)sender).Visible = false;
                    break;
                default:
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script>alert('请选择审核结果！');</script>");
                    break;
            }
        }
        #endregion



        #region 选择审核结果触发的事件处理程序
        /// <summary>
        /// 生产审核结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbl_production_check_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbl_production_check.SelectedIndex)
            {
                case 0: ckl_production_check.Visible = false;
                    break;
                case 1:
                    bll.bindCheckBoxList(ckl_production_check, "SELECT node_definition_id,node_definition_name FROM dbo.YS_NODE_DEFINITION WHERE node_definition_id in (SELECT  from_node_definition_id FROM dbo.YS_LINE WHERE to_node_definition_id=6);", "node_definition_name", "node_definition_id");
                    ckl_production_check.Visible = true;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 采购审核结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbl_purchase_check_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbl_purchase_check.SelectedIndex)
            {
                case 0: ckl_purchase_check.Visible = false;
                    break;
                case 1:
                    bll.bindCheckBoxList(ckl_purchase_check, "SELECT node_definition_id,node_definition_name FROM dbo.YS_NODE_DEFINITION WHERE node_definition_id in (SELECT  from_node_definition_id FROM dbo.YS_LINE WHERE to_node_definition_id=14);", "node_definition_name", "node_definition_id");
                    ckl_purchase_check.Visible = true;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 财务审核结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbl_budget_check_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbl_budget_check.SelectedIndex)
            {
                case 0:
                    ckl_budget_check.Visible = false;
                    break;
                case 1:
                    bll.bindCheckBoxList(ckl_budget_check, "SELECT node_definition_id,node_definition_name FROM dbo.YS_NODE_DEFINITION WHERE node_definition_id in (SELECT  from_node_definition_id FROM dbo.YS_LINE WHERE to_node_definition_id=16);", "node_definition_name", "node_definition_id");
                    ckl_budget_check.Visible = true;
                    break;
                default:
                    break;
            }
        }

        #endregion







        #region 导出EXCEL
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            string sqltext1 = string.Format(@"SELECT material_code,name,[standard],quality,unit,amount,unit_price,CONVERT(DECIMAL(18,2),c_total_cost) c_total_cost FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND  material_code like '01.07%'", task_code);
            string sqltext2 = string.Format(@"SELECT material_code,name,[standard],quality,unit,amount,unit_price,CONVERT(DECIMAL(18,2),c_total_cost) c_total_cost FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND  material_code like '01.11%'", task_code);
            string sqltext3 = string.Format(@"SELECT material_code,name,[standard],quality,unit,amount,unit_price,CONVERT(DECIMAL(18,2),c_total_cost) c_total_cost FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND  material_code like '01.15%'", task_code);
            string sqltext4 = string.Format(@"SELECT material_code,name,[standard],quality,unit,amount,unit_price,CONVERT(DECIMAL(18,2),c_total_cost) c_total_cost FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND  material_code like '01.03%'", task_code);
            string sqltext5 = string.Format(@"SELECT material_code,name,[standard],quality,unit,amount,weight,unit_price,CONVERT(DECIMAL(18,2),c_total_cost) c_total_cost FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND  (material_code like '01.08%' or material_code like '01.09%')", task_code);
            string sqltext6 = string.Format(@"SELECT material_code,name,[standard],quality,unit,amount,unit_price,CONVERT(DECIMAL(18,2),c_total_cost) c_total_cost FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND  (material_code NOT  LIKE '01.07%' AND material_code NOT  LIKE '01.11%' AND material_code NOT  LIKE '01.15%' AND material_code NOT  LIKE '01.03%' AND material_code NOT  LIKE '01.08%' AND material_code NOT  LIKE '01.09%')", task_code);


            System.Data.DataTable d1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            System.Data.DataTable d2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
            System.Data.DataTable d3 = DBCallCommon.GetDTUsingSqlText(sqltext3);
            System.Data.DataTable d4 = DBCallCommon.GetDTUsingSqlText(sqltext4);
            System.Data.DataTable d5 = DBCallCommon.GetDTUsingSqlText(sqltext5);
            System.Data.DataTable d6 = DBCallCommon.GetDTUsingSqlText(sqltext6);
            string filename = task_code + "预算材料明细.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("预算材料明细模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);//创建第一个sheet
                ISheet sheet2 = wk.GetSheetAt(1);
                ISheet sheet3 = wk.GetSheetAt(2);
                ISheet sheet4 = wk.GetSheetAt(3);
                ISheet sheet5 = wk.GetSheetAt(4);
                ISheet sheet6 = wk.GetSheetAt(5);

                if (d1.Rows.Count > 0)
                {
                    for (int i = 0; i < d1.Rows.Count; i++)
                    {
                        IRow row = sheet1.GetRow(i + 2);
                        row = sheet1.CreateRow(i + 2);
                        row.CreateCell(0).SetCellValue(i + 1);
                        for (int j = 0; j < d1.Columns.Count; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(d1.Rows[i][j].ToString());
                        }

                    }
                    for (int r = 0; r <= d1.Columns.Count + 8; r++)
                    {
                        sheet1.AutoSizeColumn(r);
                    }
                }

                if (d2.Rows.Count > 0)
                {
                    for (int i = 0; i < d2.Rows.Count; i++)
                    {
                        IRow row = sheet2.GetRow(i + 2);
                        row = sheet2.CreateRow(i + 2);
                        row.CreateCell(0).SetCellValue(i + 1);
                        for (int j = 0; j < d2.Columns.Count; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(d2.Rows[i][j].ToString());
                        }

                    }
                    for (int r = 0; r <= d2.Columns.Count + 8; r++)
                    {
                        sheet2.AutoSizeColumn(r);
                    }
                }

                if (d3.Rows.Count > 0)
                {
                    for (int i = 0; i < d3.Rows.Count; i++)
                    {
                        IRow row = sheet3.GetRow(i + 2);
                        row = sheet3.CreateRow(i + 2);
                        row.CreateCell(0).SetCellValue(i + 1);
                        for (int j = 0; j < d3.Columns.Count; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(d3.Rows[i][j].ToString());
                        }

                    }
                    for (int r = 0; r <= d3.Columns.Count + 8; r++)
                    {
                        sheet3.AutoSizeColumn(r);
                    }
                }

                if (d4.Rows.Count > 0)
                {
                    for (int i = 0; i < d4.Rows.Count; i++)
                    {
                        IRow row = sheet4.GetRow(i + 2);
                        row = sheet4.CreateRow(i + 2);
                        row.CreateCell(0).SetCellValue(i + 1);
                        for (int j = 0; j < d4.Columns.Count; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(d4.Rows[i][j].ToString());
                        }

                    }
                    for (int r = 0; r <= d4.Columns.Count + 8; r++)
                    {
                        sheet4.AutoSizeColumn(r);
                    }
                }

                if (d5.Rows.Count > 0)
                {
                    for (int i = 0; i < d5.Rows.Count; i++)
                    {
                        IRow row = sheet5.GetRow(i + 2);
                        row = sheet5.CreateRow(i + 2);
                        row.CreateCell(0).SetCellValue(i + 1);
                        for (int j = 0; j < d5.Columns.Count; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(d5.Rows[i][j].ToString());
                        }

                    }
                    for (int r = 0; r <= d5.Columns.Count + 8; r++)
                    {
                        sheet5.AutoSizeColumn(r);
                    }
                }

                if (d6.Rows.Count > 0)
                {
                    for (int i = 0; i < d6.Rows.Count; i++)
                    {
                        IRow row = sheet6.GetRow(i + 2);
                        row = sheet6.CreateRow(i + 2);
                        row.CreateCell(0).SetCellValue(i + 1);
                        for (int j = 0; j < d6.Columns.Count; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(d6.Rows[i][j].ToString());
                        }

                    }
                    for (int r = 0; r <= d6.Columns.Count + 8; r++)
                    {
                        sheet6.AutoSizeColumn(r);
                    }
                }

                //if (dtpqps.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtpqps.Rows.Count; i++)
                //    {
                //        IRow row = sheet2.GetRow(i + 2);
                //        row = sheet2.CreateRow(i + 2);
                //        row.CreateCell(0).SetCellValue(i + 1);
                //        for (int j = 0; j < dtpqps.Columns.Count - 2; j++)
                //        {
                //            row.CreateCell(j + 1).SetCellValue(dtpqps.Rows[i][j].ToString());
                //        }
                //        row.CreateCell(10).SetCellValue(dtpqps.Rows[i]["CNFB_BYMYMONEY"].ToString());
                //        row.CreateCell(12).SetCellValue(dtpqps.Rows[i]["CNFB_BYREALMONEY"].ToString());
                //    }
                //    for (int r = 0; r <= dtpqps.Columns.Count + 8; r++)
                //    {
                //        sheet2.AutoSizeColumn(r);
                //    }
                //}

                sheet1.ForceFormulaRecalculation = true;
                sheet2.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion

    }
}
