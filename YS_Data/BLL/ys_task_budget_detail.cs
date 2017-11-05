using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


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
                case "1": 
                    DBCallCommon.ExeSqlText(string.Format("UPDATE {0} SET direct_labour_budget={1},sub_teamwork_budget={2},cooperative_product_budget={3},total_material_budget={4} WHERE task_code='{5}';", TABLE_TASK_BUDGET, tb.labour_budget, tb.teamwork_budget, tb.cooperative_budget, tb.total_material_budget, tb.task_code));
                    //获得生产部长、采购部长id
                    string[] ids = new string[] { BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0401 AND ST_PD=0;"), BudgetFlowEngine.getStringByDR("SELECT ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION=0501 AND ST_PD=0;") };

                    if (BudgetFlowEngine.completeCurrentNode(tb.task_code, node_definition_id, "NULL"))
                        BudgetFlowEngine.activeFollowNode(tb.task_code, node_definition_id, ids);
                    break;
                case"2":
                    break;
            }
        }


    }
}
