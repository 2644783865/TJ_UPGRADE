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
        public void bindTaskRepeater(Repeater rpt, Panel panel, string task_code,string type)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(string.Format(@"SELECT task_code,contract_code,project_name,
equipment_name,total_material_budget,direct_labour_budget,sub_teamwork_budget,cooperative_product_budget,c_total_task_budget  
FROM dbo.YS_TASK_BUDGET WHERE   task_code <>'{0}' AND task_type='{1}' AND state=5;", task_code,type));
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





    }
}
