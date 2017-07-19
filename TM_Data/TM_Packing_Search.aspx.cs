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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Packing_Search : System.Web.UI.Page
    {
        string sqlText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetComProName();
                GetComEngName();
                InitVar();
            }
        }
        private void InitVar()
        {
            if (GridView1.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }
        //绑定项目名称
        private void GetComProName()
        {
            sqlText = "select distinct PLT_PJID,PLT_PJID+'‖'+PLT_PJNAME as PLT_PJNAME from TBPM_PACKLISTTOTAL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlProName.DataSource = dt;
            ddlProName.DataTextField = "PLT_PJNAME";
            ddlProName.DataValueField = "PLT_PJID";
            ddlProName.DataBind();
            ddlProName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlProName.SelectedIndex = 0;
        }
        //绑定工程名称
        private void GetComEngName()
        {
            sqlText = "select distinct PLT_ENGID,PLT_ENGID+'‖'+PLT_ENGNAME AS PLT_ENGNAME from TBPM_PACKLISTTOTAL ";
            sqlText += "where PLT_PJID='" + ddlProName.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlEngName.DataSource = dt;
            ddlEngName.DataTextField = "PLT_ENGNAME";
            ddlEngName.DataValueField = "PLT_ENGID";
            ddlEngName.DataBind();
            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;
        }
        //查询项目、工程下的技术外协
        private void GetPackData()
        {
            List<string> nos = new List<string>();
            if (ddlProName.SelectedValue != "-请选择-" && ddlEngName.SelectedValue == "-请选择-")
            {
                sqlText = "select PLT_PACKLISTNO from TBPM_PACKLISTTOTAL ";
                sqlText += "where PLT_PJID='" + ddlProName.SelectedValue + "'";
            }
            else if (ddlProName.SelectedValue != "-请选择-" && ddlEngName.SelectedValue != "-请选择-")
            {
                sqlText = "select PLT_PACKLISTNO from TBPM_PACKLISTTOTAL ";
                sqlText += "where PLT_ENGID='" + ddlEngName.SelectedValue + "'";
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            foreach (DataRow row in dt.Rows)
            {
                nos.Add(row[0].ToString().Trim());
            }
            string[] col_fields = nos.ToArray();
            string pk_no = "";
            for (int i = 0; i < col_fields.Length; i++)
            {
                pk_no = pk_no + "'" + col_fields[i].ToString() + "'" + ",";
            }
            string PLT_NOS = pk_no.Trim(',');
            if (PLT_NOS != "")
            {
                sqlText = "select PL_NO,PL_PACKAGENO,PL_ITEMNO,PL_PACKNAME,PL_MARKINGNO,PL_PACKQLTY,";
                sqlText += "PL_PACKTYPE,PL_PACKDIML,PL_PACKDIMW,PL_PACKDIMH,PL_TOTALVOLUME,";
                sqlText += "PL_SINGLENETWGHT,PL_SINGLEGROSSWGHT,PL_TOTALGROSSWGHT,PL_DESCRIPTION,";
                sqlText += "PL_OUTLINEDRAWING,PL_STATE ";
                sqlText += "from TBPM_PACKINGLIST where PL_PACKLISTNO in (" + PLT_NOS + ")";
            }
            else
            {
                sqlText = "select PL_NO,PL_PACKAGENO,PL_ITEMNO,PL_PACKNAME,PL_MARKINGNO,PL_PACKQLTY,";
                sqlText += "PL_PACKTYPE,PL_PACKDIML,PL_PACKDIMW,PL_PACKDIMH,PL_TOTALVOLUME,";
                sqlText += "PL_SINGLENETWGHT,PL_SINGLEGROSSWGHT,PL_TOTALGROSSWGHT,PL_DESCRIPTION,";
                sqlText += "PL_OUTLINEDRAWING,PL_STATE ";
                sqlText += "from TBPM_PACKINGLIST where PL_PACKLISTNO=''";
            }
            DBCallCommon.BindGridView(GridView1, sqlText);
            InitVar();
        }
        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedValue == "-请选择-")
            {
                ddlEngName.SelectedIndex = 0;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
                GetComEngName();
                GetPackData();
            }
            InitVar();
        }

        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedValue != "-请选择-")
            {
                GetPackData();
            }
            else
            {
                Response.Write("<script>alert('提示:请选择项目名称!');</script>");
                return;
            }
            InitVar();
        }
    }
}
