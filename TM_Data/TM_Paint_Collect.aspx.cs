using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Paint_Collect : System.Web.UI.Page
    {
        string pId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["pId"] != null)
            {
                pId = Request.QueryString["pId"].ToString();
            }
            string sql = "select * from (select PS_BOTMARID,sum(cast(PS_BOTYONGLIANG as float)) as sumYL,PS_ENGID from (select PS_BOTMARID,PS_BOTYONGLIANG,PS_ENGID from dbo.TBPM_PAINTSCHEMELIST where PS_PID='" + pId + "' and PS_BOTMARID<>''union all select PS_MIDMARID,PS_MIDYONGLIANG,PS_ENGID from dbo.TBPM_PAINTSCHEMELIST where PS_PID='" + pId + "' and PS_MIDMARID<>''union all select PS_TOPMARID,PS_TOPYONGLIANG,PS_ENGID from dbo.TBPM_PAINTSCHEMELIST where PS_PID='" + pId + "' and PS_TOPMARID<>'')a group by PS_BOTMARID,PS_ENGID)b left join dbo.TBMA_MATERIAL as c on b.PS_BOTMARID=c.ID";
            try
            {
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception)
            {
                Response.Write("<script>alert('数据出错，无法汇总')</script>");
            }
        }

        //添加到油漆采购申请
        protected void PurApply_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../PC_Data/PC_TBPC_Otherpur_Bill_edit.aspx?action=add&pId=" + pId);
            }
            catch (Exception)
            {
                Response.Write("<script>alert('数据出错，无法添加到采购申请单')</script>");
            }
        }
    }
}
