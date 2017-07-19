using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_ContractMarket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            DataTable dt = GetTable();
            foreach (GridViewRow gr in GridHeSuan.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < 4; i++)
                {
                    dr[i] = ((ITextControl)gr.Cells[i].FindControl("txt" + i)).Text;
                }
                dt.Rows.Add(dr);
            }
            for (int i = 0; i < int.Parse(num.Text); i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            GridHeSuan.DataSource = dt;
            GridHeSuan.DataBind();
        }

        protected DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CM_XM");
            dt.Columns.Add("CM_ZL");
            dt.Columns.Add("CM_DJ");
            dt.Columns.Add("CM_ZJ");
            return dt;
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((LinkButton)sender).CommandArgument) - 1;
            DataTable dt = GetTable();
            for (int i = 0; i < GridHeSuan.Rows.Count; i++)
            {
                if (i != id)
                {
                    GridViewRow gr = GridHeSuan.Rows[i];
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < 4; j++)
                    {
                        dr[j] = ((ITextControl)gr.Cells[j].FindControl("txt" + j)).Text;
                    }
                    dt.Rows.Add(dr);
                }
            }
            GridHeSuan.DataSource = dt;
            GridHeSuan.DataBind();
        }
    }
}
