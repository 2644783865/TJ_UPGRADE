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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_FJCB_ADD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (tbrwh.Text.ToString() != "" && tbyqzl.Text.ToString() != "" && tbyear.Text.ToString() != "" && tbmonth.Text.ToString() != "")
            {
                string sqladd = "insert into FM_FJCB(FJCB_TSAID,FJCB_YQZL,FJCB_YQYL,FJCB_YQHSDJ,FJCB_YQHSJE,FJCB_XSJYL,FJCB_XSJHSDJ,FJCB_XSJHSJE,FJCB_HJHSJE,FJCB_HJBHSJE,FJCB_YEAR,FJCB_MONTH) values('" + tbrwh.Text.ToString() + "','" + tbyqzl.Text.ToString() + "','" + tbyqyl.Text.ToString() + "','" + tbyqdj.Text.ToString() + "','" + tbyqje.Text.ToString() + "','" + tbxsjyl.Text.ToString() + "','" + tbxsjdj.Text.ToString() + "','" + tbxsjje.Text.ToString() + "','" + tbhjje.Text.ToString() + "','" + tbhjbhsje.Text.ToString() + "','" + tbyear.Text.ToString() + "','" + tbmonth.Text.ToString() + "')";
                DBCallCommon.ExeSqlText(sqladd);
                Response.Write("<script>alert('添加成功！')</script>");
                Response.Write("<script>window.close()</script>");
            }
            else
            {
                Response.Write("<script>alert('请填写年月份和任务号以及油漆种类！')</script>");
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }
    }
}
