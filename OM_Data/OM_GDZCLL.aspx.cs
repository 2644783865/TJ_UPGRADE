using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GDZCLL : System.Web.UI.Page
    {
        string sqltext = "";
        string flag = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] != null)
                flag = Request.QueryString["FLAG"].ToString();
            if (!IsPostBack)
            {
                if (flag == "PUSH")
                {
                    GetCode();
                    GetDep();
                    initial();
                    lblOutDoc.Text = Session["UserName"].ToString();
                    lblOutDate.Text = DateTime.Now.ToString();
                }
            }
        }
        private void GetCode()
        {
            sqltext = "select TOP 1 dbo.GetIndex(OUTCODE) AS TopIndex from TBOM_GDZCOUT ORDER BY dbo.GetIndex(OUTCODE) DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            lblOutCode.Text = "GDZCOUT" + code.PadLeft(4, '0');
        }
        private void GetDep()//绑定部门
        {
            string sqlText = "select distinct ST_DEPID,DEP_NAME from View_OMstaff where ST_DEPID LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlDep.DataSource = dt;
            ddlDep.DataTextField = "DEP_NAME";
            ddlDep.DataValueField = "DEP_NAME";
            ddlDep.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            ddlDep.Items.Insert(0, item);
        }
        protected void initial()
        {
            sqltext = "select NAME,MODEL,NUMSTORE from TBOM_GDZCSTORE where OUTSTATE='1'";
            DBCallCommon.BindGridView(GridView1, sqltext);
            sqltext = "update TBOM_GDZCSTORE set OUTSTATE=''";
            DBCallCommon.ExeSqlText(sqltext);
        }
        private void writedata()
        {
            string outname;
            string outmodel;
            float numstore;
            float outnum;
            string outnote;
            List<string> list_sql = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                outname = ((Label)gr.FindControl("lblName")).Text;
                outmodel = ((Label)gr.FindControl("lblModel")).Text;
                numstore = Convert.ToSingle(((TextBox)gr.FindControl("txtNumstore")).Text);
                outnum = Convert.ToSingle(((TextBox)gr.FindControl("txtOutNum")).Text);
                outnote = ((TextBox)gr.FindControl("txtOutNote")).Text;
                if (outnum == 0.0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有入库数据！！！');", true);
                    return;
                }
                else
                {
                    sqltext = "insert into TBOM_GDZCOUT(OUTCODE,OUTDEP,OUTNAME,OUTMODEL,OUTNUM,OUTSENDER,OUTDATE,OUTDOC,OUTNOTE)";
                    sqltext += "values('" + lblOutCode.Text + "','" + ddlDep.SelectedValue + "','" + outname + "','" + outmodel + "'," + outnum + ",'" + txtshr.Text + "','" + lblOutDate.Text + "','" + lblOutDoc.Text + "','" + outnote + "')";
                    list_sql.Add(sqltext);
                    sqltext = "update TBOM_GDZCSTORE set NUMSTORE=" + numstore + " where NAME='" + outname + "'and MODEL='" + outmodel + "'";
                    list_sql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(list_sql);
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string st = "OK";
            if (GridView1.Rows.Count == 0)
            {
                st = "NoData";
            }
            if (st == "OK")
            {
                writedata();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.location.href='OM_GdzcStore.aspx'", true);
            }
            else if (st == "NoData")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有出库数据！！！');", true);
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_GdzcOrder.aspx");
        }
    }
}
