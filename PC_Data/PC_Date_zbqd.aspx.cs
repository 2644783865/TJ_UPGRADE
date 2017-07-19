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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Date_zbqd : System.Web.UI.Page
    {
        string pcode = "";
        string guobiao = "";
        string sql = "";
        string guige = "";
        string mid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["mid"] != null)
            {
                mid = Server.UrlDecode(Request.QueryString["mid"].ToString());
            }
            if (Request.QueryString["pc"] != null)
            {
                pcode = Server.UrlDecode(Request.QueryString["pc"].ToString());
            }
            if (Request.QueryString["guobiao"] != null)
            {
                guobiao = Server.UrlDecode(Request.QueryString["guobiao"].ToString());
            }
            if (Request.QueryString["register"] != null)
            {
                guige = Server.UrlDecode(Request.QueryString["register"].ToString());
            }
            if (!IsPostBack)
            {
               
                bind();
            }
            
        }

        void bind()
        {
            sql = " select * from View_TBPC_PURCHASEPLAN_IRQ_ORDER where ptcode='" + pcode + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            tbpc_repeater.DataSource = dt;
            tbpc_repeater.DataBind();

            sql = " select * from View_TBPCZB  where guige='" + guige + "' and gb='" + guobiao + "' ";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rep_zb.DataSource = dt;
            rep_zb.DataBind();

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            string ghs = "";
            string dj = "";
            string ibdate = "";
            string ibfidate = "";
            bool check = false;
            
            
            foreach (RepeaterItem Reitem in rep_zb.Items)
            {
                if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                {
                    check = true;
                    ghs = ((System.Web.UI.WebControls.Label)Reitem.FindControl("lbsuupplyid")).Text;
                    dj = ((System.Web.UI.WebControls.Label)Reitem.FindControl("lbprice")).Text;
                    ibdate = ((System.Web.UI.WebControls.Label)Reitem.FindControl("lbdate")).Text;
                    ibfidate = ((System.Web.UI.WebControls.Label)Reitem.FindControl("lbfidate")).Text;
                    
                }
            }
            //判断是否选择了数据行
            if (!check)//没有选择行，且不是添加和导出合同操作
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请勾选要操作的数据行！');", true); return;
            }

            else
            {
                sql = "select * from TBPC_INVITEBID where IB_MARID='" + mid + "' and IB_STATE = '0' ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('招标物料已经存在！');", true); return;
                }
                else
                {
                    sql = " insert into TBPC_INVITEBID(IB_MARID,IB_SUPPLY,IB_PRICE,IB_DATE,IB_SUBMIT,IB_NOTE,IB_FIDATE) ";
                    sql += " values('" + mid + "','" + ghs + "','" + dj + "','" + ibdate + "','" + Session["UserID"].ToString() + "','从采购计划调整','" + ibfidate + "')";
                    DBCallCommon.GetDRUsingSqlText(sql);

                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('添加成功！');", true); return;
                
                }
            }
        }

        protected void btncx_Click(object sender, EventArgs e)
        {
            string condition = "";
            if (tbgg.Text != "")
            {
                condition += "guige='" + tbgg.Text + "'";
            }
            if (tbgg.Text == "" && tbgb.Text != "")
            {
                condition += "gb='" + tbgb.Text + "'";
            }
            if (tbgg.Text != "" && tbgb.Text != "")
            {
                condition += "and gb='" + tbgb.Text + "'";
            }
            sql = " select * from View_TBPCZB  where ";
            sql += condition;
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
            rep_zb.DataSource = dt1;
            rep_zb.DataBind();
        }
    }
}
