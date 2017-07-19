using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_CNFB_ADD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (tbyear.Text.ToString() != "" && tbmonth.Text.ToString() != "")
            {
                string sqladd = "insert into TBMP_CNFB_LIST(CNFB_PROJNAME,CNFB_HTID,CNFB_TSAID,CNFB_TH,CNFB_SBNAME,CNFB_NUM,CNFB_BYMYMONEY,CNFB_BYREALMONEY,CNFB_YEAR,CNFB_MONTH,CNFB_TYPE) values('" + tbprojname.Text.ToString() + "','" + tbprojid.Text.ToString() + "','" + tbrwh.Text.ToString() + "','" + tbth.Text.ToString() + "','" + tbsbname.Text.ToString() + "','" + tbsl.Text.ToString() + "','" + tbbymymoney.Text.ToString() + "','" + tbbyrealmoney.Text.ToString() + "','" + tbyear.Text.ToString() + "','" + tbmonth.Text.ToString() + "','" + tbtype.Text.ToString() + "')";
                DBCallCommon.ExeSqlText(sqladd);
                Response.Write("<script>alert('添加成功！')</script>");
                Response.Write("<script>window.close()</script>");
            }
            else
            {
                Response.Write("<script>alert('请填写年月信息！')</script>");
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }
    }
}
