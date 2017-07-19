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
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_TOOL_OUTBILL : System.Web.UI.Page
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
                   
                    initial();
                    lblOutDoc.Text = Session["UserName"].ToString();
                    lblOutDocID.Text = Session["UserID"].ToString();
                    lblOutDate.Text = DateTime.Now.ToString();
                }
            }
        }
        private void GetCode()
        {
            sqltext = "select TOP 1 dbo.GetCode(OUTCODE) AS TopIndex from TBMP_TOOL_OUT ORDER BY dbo.GetCode(OUTCODE) DESC";
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
            lblOutCode.Text = "TOOLOUT" + code.PadLeft(4, '0');
        }
       
        protected void initial()
        {
            sqltext = "select TYPE,NAME,MODEL,NUMBER from TBMP_TOOL_RESTORE where STATE='1'";
            DBCallCommon.BindGridView(GridView1, sqltext);
            sqltext = "update TBMP_TOOL_RESTORE set STATE=''";
            DBCallCommon.ExeSqlText(sqltext);
        }
        private void writedata()
        {
            string type;
            string name;
            string model;
            int numstore;
            int outnum;
            string outnote;
            List<string> list_sql = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                type = ((Label)gr.FindControl("lbltype")).Text;
                name = ((Label)gr.FindControl("lblname")).Text;
                model = ((Label)gr.FindControl("lblmodel")).Text;
                numstore = Convert.ToInt32(((TextBox)gr.FindControl("txtstore")).Text);
                outnum = Convert.ToInt32(((TextBox)gr.FindControl("txtoutnum")).Text);
                outnote = ((TextBox)gr.FindControl("txtnote")).Text;
                numstore -= outnum;
                if (outnum == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有出库数据！！！');", true);
                    return;
                }
                else
                {
                    sqltext = "insert into TBMP_TOOL_OUT(OUTCODE,TYPE,NAME,MODEL,OUTNUM,OUTPERID,OUTDATE,DOCUPERID,NOTE)";
                    sqltext += "values('" + lblOutCode.Text + "','" + type + "','" + name + "','" + model + "'," + outnum + ",'" + giverid.Value.ToString() + "','" + lblOutDate.Text + "','" + lblOutDocID.Text + "','" + outnote + "')";
                    list_sql.Add(sqltext);
                    sqltext = "update TBMP_TOOL_RESTORE set NUMBER=" + numstore + " where TYPE='" + type + "'and NAME='" + name + "'and MODEL='"+model+"'";
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
                Response.Write("<script>alert('保存成功！');window.location.href='PM_TOOL_OUT.aspx'</script>");
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.location.href='PM_TOOL_OUT.aspx'", true);
            }
            else if (st == "NoData")
            {
                Response.Write("<script>alert('提示:无法保存！！！没有出库数据！！！')</script>");
                return;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有出库数据！！！');", true);
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PM_TOOL_OUT.aspx");
        }
       
    }
}
