using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class QX_View_Detail : System.Web.UI.Page
    {
        DataTable dtDep = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            string Id = "";
            string name = "";
            if (Request["key"] != null)
            {
                Id = Request["key"].ToString();

            }
            if (Request["name"] != null)
            {
                name = Request["name"].ToString();
            }
            hidSTID.Value = Id;
            lblName.Text = name;
            if (!IsPostBack)
            {
                string sql = "select * from OM_VIEWPERMEISION ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                rptProNumCost.DataSource = dt;
                rptProNumCost.DataBind();

            }

        }

        private DataTable GetDep()
        {
            string sql = "select DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE like'[0-9][0-9]'";
            dtDep = DBCallCommon.GetDTUsingSqlText(sql);
            DataRow row = dtDep.NewRow();
            row[0] = "全部";
            row[1] = "00";
            dtDep.Rows.InsertAt(row, 0);
            return dtDep;
        }

        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            CheckBoxList cbl = e.Item.FindControl("cblDep") as CheckBoxList;
            cbl.DataValueField = "DEP_CODE";
            cbl.DataTextField = "DEP_NAME";
            cbl.DataSource = GetDep();
            cbl.DataBind();
            string pId = ((HtmlInputHidden)e.Item.FindControl("pId")).Value;
            string sql = "select * from OM_VIEWPERMEISIONDEP where StId='" + hidSTID.Value + "' and pId='" + pId + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (cbl.Items[i].Value == dt.Rows[j]["DepId"].ToString())
                    {
                        cbl.Items[i].Selected = true;
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql = "delete from OM_VIEWPERMEISIONDEP where StId='" + hidSTID.Value + "'";
            list.Add(sql);
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                RepeaterItem item = rptProNumCost.Items[i] as RepeaterItem;
                CheckBoxList cbl = item.FindControl("cblDep") as CheckBoxList;
                string Id = ((HtmlInputHidden)item.FindControl("pId")).Value;
                for (int j = 0; j < cbl.Items.Count; j++)
                {
                    if (cbl.Items[j].Selected == true)
                    {
                        sql = "insert into OM_VIEWPERMEISIONDEP(pId,DepId,DepName,StId) values('" + Id + "','" + cbl.Items[j].Value + "','" + cbl.Items[j].Text + "','" + hidSTID.Value + "')";
                        list.Add(sql);
                    }
                }

            }
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("QX_View_List.aspx");

        }
    }
}
