using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_MTONotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetNote();
            }
        }

        protected void GetNote()
        {
            string sql = "";
            string state = DropDownListState.SelectedValue;
            if (state == "1")
            {
                sql = "SELECT MP_ID as ID,MP_CHCODE AS CHCODE, MP_PTC AS PTC,MP_MARID AS MaterialCode,MP_NAME AS MaterialName," +
                    "MP_GUIGE AS MaterialStandard,MP_CAIZHI AS Attribute,MP_STANDARD AS GB," +
                    "MP_UNIT AS Unit,MP_TZNUM AS Weight,MP_TZFZNUM AS Number," +
                    "MP_TYPE AS Type,MP_SUBMITNAME AS Clerk,MP_SUBMITDATE AS Date,MP_NOTE AS Note " +
                    "FROM View_TBPM_MPFORLIB WHERE MP_TZSTATE='1' order by MP_CHCODE,MP_PTC,MP_MARID";
            }
            if (state == "2")
            {
                sql = "SELECT MP_ID as ID,MP_CHCODE AS CHCODE, MP_PTC AS PTC,MP_MARID AS MaterialCode,MP_NAME AS MaterialName," +
                   "MP_GUIGE AS MaterialStandard,MP_CAIZHI AS Attribute,MP_STANDARD AS GB," +
                   "MP_UNIT AS Unit,MP_TZNUM AS Weight,MP_TZFZNUM AS Number," +
                   "MP_TYPE AS Type,MP_SUBMITNAME AS Clerk,MP_SUBMITDATE AS Date,MP_NOTE AS Note " +
                   "FROM View_TBPM_MPFORLIB WHERE MP_TZSTATE='2' order by MP_CHCODE,MP_PTC,MP_MARID";
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            if (dt.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }

        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetNote();
        }

        protected void Readed_Click(object sender, EventArgs e)
        {

            List<string> sqllist = new List<string>();  

            string sql = "UPDATE TBPM_MPFORLIB SET MP_WSSTATE='' WHERE MP_WSSTATE='TZ" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
              CheckBox cb=(CheckBox)Repeater1.Items[i].FindControl("CheckBox1");
              if(cb.Checked)
                {
                    Label lbid = (Label)Repeater1.Items[i].FindControl("LabelID");

                    sql = "UPDATE TBPM_MPFORLIB SET MP_WSSTATE='TZ" + Session["UserID"].ToString() + "' WHERE MP_ID='" + lbid.Text + "'";

                    sqllist.Add(sql);
                }
            }

            if (sqllist.Count == 1)
            {

                string script = @"alert('请选择需要调整的条目!');";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);

                return;
            }

            string execsql = "exec BGTOMTO @userid='" + Session["UserID"].ToString() + "'";

            sqllist.Add(execsql);

            sql = "UPDATE TBPM_MPFORLIB SET MP_WSSTATE='' WHERE MP_WSSTATE='TZ" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            DBCallCommon.ExecuteTrans(sqllist);

           

            //Response.Redirect("SM_Warehouse_MTOBGAdjust.aspx?FLAG=PUSHMTO&&ID=NEW");

            string script1 = @"window.open('SM_Warehouse_MTOBGAdjust.aspx?FLAG=PUSHMTO&&ID=NEW');";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script1, true);
            
        }
    }
}
