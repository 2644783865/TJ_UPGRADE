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
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHe_Deadline : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Showdata();
            }
            CheckUser(ControlFinder);
        }

        private void Showdata()
        {
            string sql = "select *  from TBDS_KaoHeDate";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
   
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                string start = ((TextBox)gRow.FindControl("txtStart")).Text;
                string end = ((TextBox)gRow.FindControl("txtEnd")).Text;
                string id = ((HtmlInputHidden)gRow.FindControl("Hidden2")).Value;
                string sql = "update TBDS_KaoHeDate set kh_Start='" + start + "',kh_End='" + end + "' where Id='" + id + "'";

                list.Add(sql);
            }
            DBCallCommon.ExecuteTrans(list);

            Response.Write("<script>alert('保存成功');</script>");
            Showdata();
        }
    }
}
