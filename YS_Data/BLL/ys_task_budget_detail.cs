using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;


namespace ZCZJ_DPF.YS_Data.BLL
{
    public class ys_task_budget_detail
    {
        const string TABLE_TASK_BUDGET = "YS_TASK_BUDGET";

        /// <summary>
        /// 绑定材料明细的repeater
        /// </summary>
        /// <param name="rpt">要绑定的repeater</param>
        /// <param name="panel">无数据面板</param>
        /// <param name="task_code">任务号</param>
        /// <param name="condition">物料查询条件</param>
        public void bindMaterialRepeater(Repeater rpt, Panel panel, string task_code, string materialCondition)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(string.Format(@"SELECT material_code,name,[standard],quality,unit,amount,weight,unit_price,
CONVERT(DECIMAL(18,2),c_total_cost) c_total_cost FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND  {1};", task_code, materialCondition));
            if (dt.Rows.Count != 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.Visible = false;
                panel.Visible = true;
            }

        }


        /// <summary>
        /// 绑定同类型任务号repeater
        /// </summary>
        /// <param name="rpt">要绑定的repeater</param>
        /// <param name="panel">无数据面板</param>
        /// <param name="task_code">任务号</param>
        /// <param name="type">任务号类型</param>
        public void bindTaskRepeater(Repeater rpt, Panel panel, string task_code, string type)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(string.Format(@"SELECT task_code,contract_code,project_name,
equipment_name,total_material_budget,direct_labour_budget,sub_teamwork_budget,cooperative_product_budget,c_total_task_budget  
FROM dbo.YS_TASK_BUDGET WHERE   task_code <>'{0}' AND task_type='{1}' AND state=5;", task_code, type));
            if (dt.Rows.Count != 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.Visible = false;
                panel.Visible = true;
            }

        }


        /// <summary>
        /// 完成当前节点并激活下一节点
        /// </summary>
        /// <param name="node_definition_id">当前节点的node类id</param>
        /// <param name="tb">任务预算对象</param>
        public void finishNode(string node_definition_id, YS_Data.Model.TaskBudget tb)
        {
            switch (node_definition_id)
            {
                case "1": //财务初步编制
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET direct_labour_budget={1},sub_teamwork_budget={2},cooperative_product_budget={3},total_material_budget={4}, state=2 WHERE task_code='{5}';", TABLE_TASK_BUDGET, tb.labour_budget, tb.teamwork_budget, tb.cooperative_budget, tb.total_material_budget, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, "NULL"))
                    {
                        string[] ids1 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0401 AND ST_PD=0;"), BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0501 AND ST_PD=0;") };//获得生产部长、采购部长id
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids1);
                    }
                    break;
                case "2"://生产部长分工
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, "NULL"))
                    {
                        string[] ids2 = new string[] { tb.node_labour_dep_user_id, tb.node_teamwork_dep_user_id, tb.node_cooperative_dep_user_id };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids2);
                    }
                    break;
                case "3"://人工费反馈
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET direct_labour_dep={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.labour_dep, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_labour_dep_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET production_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids3 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0401 AND ST_PD=0;") };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids3);
                    }
                    break;
                case "4"://分包费反馈
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET sub_teamwork_dep={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.teamwork_dep, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_teamwork_dep_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET production_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids4 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0401 AND ST_PD=0;") };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids4);
                    }
                    break;
                case "5"://外协费反馈
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET cooperative_product_dep={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.cooperative_dep, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_cooperative_dep_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET production_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids5 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0401 AND ST_PD=0;") };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids5);
                    }
                    break;
                case "6"://生产部长审核
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET production_check={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.production_check, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_production_check_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET state=3 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids6 = new string[] { "63" };//季凌云
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids6);
                    }
                    break;
                case "7"://采购部长分工
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, "NULL"))
                    {
                        string[] ids7 = new string[] { tb.node_ferrous_dep_user_id, tb.node_purchasepart_dep_user_id, tb.node_paint_dep_user_id, tb.node_electrical_dep_user_id, tb.node_casting_dep_user_id, tb.node_othermat_dep_user_id };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids7);
                    }
                    break;

                case "8"://黑色金属反馈
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET ys_ferrous_metal_dep={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.ferrous_dep, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_ferrous_dep_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET purchase_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids8 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0501 AND ST_PD=0;") };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids8);
                    }
                    break;
                case "9"://外购件反馈
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET ys_purchase_part_dep={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.purchasepart_dep, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_purchasepart_dep_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET purchase_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids9 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0501 AND ST_PD=0;") };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids9);
                    }
                    break;
                case "10"://油漆涂料反馈
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET ys_paint_coating_dep={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.paint_dep, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_paint_dep_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET purchase_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids10 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0501 AND ST_PD=0;") };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids10);
                    }
                    break;
                case "11"://电器电料反馈
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET ys_electrical_dep={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.electrical_dep, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_electrical_dep_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET purchase_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids11 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0501 AND ST_PD=0;") };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids11);
                    }
                    break;
                case "12"://铸锻件反馈
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET ys_casting_forging_dep={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.casting_dep, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_casting_dep_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET purchase_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids12 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0501 AND ST_PD=0;") };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids12);
                    }
                    break;
                case "13"://其他材料反馈
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET ys_othermat_cost_dep={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.othermat_dep, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_othermat_dep_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET purchase_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids13 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0501 AND ST_PD=0;") };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids13);
                    }
                    break;
                case "14"://采购部长审核
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET purchase_check={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.purchase_check, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_purchase_check_note))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET state=3 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids14 = new string[] { "63" };//季凌云
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids14);
                    }
                    break;

                case "15": //预算调整
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET direct_labour_budget={1},sub_teamwork_budget={2},cooperative_product_budget={3},total_material_budget={4}, state=4 WHERE task_code='{5}';", TABLE_TASK_BUDGET, tb.labour_budget, tb.teamwork_budget, tb.cooperative_budget, tb.total_material_budget, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, "NULL"))
                    {
                        DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET budget_check=2 WHERE task_code='{1}';", TABLE_TASK_BUDGET, tb.task_code));
                        string[] ids15 = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0601 AND ST_PD=0;") };//获得财务部长id
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids15);
                    }
                    break;
                case "16"://财务部长审核
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET budget_check={1}, state=5 WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.budget_check, tb.task_code));
                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, tb.node_budget_check_note))
                    {
                        string[] ids16 = new string[] { "0" };
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids16);
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 完成当前节点并驳回到指定节点
        /// </summary>
        /// <param name="node_definition_id">当前节点的node类id</param>
        /// <param name="tb">任务预算对象</param>
        public void rejectNode(string node_definition_id, YS_Data.Model.TaskBudget tb, string inids)
        {
            switch (node_definition_id)
            {
                case "6"://生产部驳回
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET production_check={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.production_check, tb.task_code));
                    BudgetFlowEngine.backToPreNode(tb.task_code, tb.node_production_check_note, node_definition_id, inids);
                    break;

                case "14": //采购部驳回
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET purchase_check={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.purchase_check, tb.task_code));
                    BudgetFlowEngine.backToPreNode(tb.task_code, tb.node_purchase_check_note, node_definition_id, inids);
                    break;
                case "16"://财务部驳回
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET state=3,budget_check={1} WHERE task_code='{2}';", TABLE_TASK_BUDGET, tb.budget_check, tb.task_code));
                    BudgetFlowEngine.backToPreNode(tb.task_code, tb.node_budget_check_note, node_definition_id, inids);
                    break;
                default:
                    break;
            }
        }



        /// <summary>
        /// 用sql动态绑定CheckBoxList
        /// </summary>
        /// <param name="cbl">CheckBoxList控件</param>
        /// <param name="sql">查询语句</param>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public void bindCheckBoxList(CheckBoxList ckl, string sql, string text, string value)
        {
            ckl.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            ckl.DataTextField = text;
            ckl.DataValueField = value;
            ckl.DataBind();
        }


        /// <summary>
        /// 获取CheckBoxList选中的值
        /// </summary>
        /// <param name="ckl"></param>
        /// <returns></returns>
        public string getCheckBodListSelectedValue(CheckBoxList ckl)
        {
            string s = "";
            foreach (ListItem li in ckl.Items)
            {
                if (li.Selected) s += li.Value + ",";
            }
            return s.Substring(0, s.Length - 1);

        }

    }
}
