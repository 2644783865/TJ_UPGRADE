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
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                task_code = Request.QueryString["tsak_code"].ToString();
                bindRepeater();
            }


        }

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
