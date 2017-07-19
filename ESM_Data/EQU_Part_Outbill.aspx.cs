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

namespace ZCZJ_DPF.ESM_Data
{
    public partial class EQU_Part_Outbill : System.Web.UI.Page
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
            Get_Giver();
        }
         private void GetCode()
        {
            sqltext = "select TOP 1 dbo.GetCode(OutCode) AS TopIndex from EQU_Part_Out ORDER BY dbo.GetCode(OutCode) DESC";
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
            lblOutCode.Text = "PAROUT" + code.PadLeft(4, '0');
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
            sqltext = "select ParName,ParType,ParNumSto from EQU_Part_Store where State='1'";
            DBCallCommon.BindGridView(GridView1, sqltext);
            sqltext = "update EQU_Part_Store set State=''";
            DBCallCommon.ExeSqlText(sqltext);
        }
        private void writedata()
        {
            string outname;
            string outtype;
            int numstore;
            int outnum;
            string outnote;
            List<string> list_sql = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                outname = ((Label)gr.FindControl("lblName")).Text;
                outtype = ((Label)gr.FindControl("lblType")).Text;
                numstore = Convert.ToInt32(((TextBox)gr.FindControl("txtNumstore")).Text);
                outnum = Convert.ToInt32(((TextBox)gr.FindControl("txtOutNum")).Text);
                outnote = ((TextBox)gr.FindControl("txtOutNote")).Text;
                numstore -= outnum;
                if (outnum == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有入库数据！！！');", true);
                    return;
                }
                else
                {
                    sqltext = "insert into EQU_Part_Out(OutCode,OutDep,ParName,ParType,OutNum,OutPer,OutDate,OutDoc,OutNote)";
                    sqltext += "values('" + lblOutCode.Text + "','" + ddlDep.SelectedValue + "','" + outname + "','" + outtype + "'," + outnum + ",'" + txt_giver.Text + "','" + lblOutDate.Text + "','" + lblOutDoc.Text + "','" + outnote + "')";
                    list_sql.Add(sqltext);
                    sqltext = "update EQU_Part_Store set ParNumSto=" + numstore + " where ParName='" + outname + "'and ParType='" + outtype + "'";
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.location.href='EQU_Part_Out.aspx'", true);
            }
            else if (st == "NoData")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有出库数据！！！');", true);
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EQU_Part_Out.aspx");
        }
        private void Get_Giver()
        {
            //收料员
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='10' and ST_PD='0'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt1.Rows.Count != 0)
            {
                Table t = new Table();
                TableRow tr = new TableRow();
                TableCell td = new TableCell();

                CheckBoxList cki = new CheckBoxList();
                cki.ID = "cki1";
                cki.DataSource = dt1;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//领导的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数

                td.Controls.Add(cki);
                tr.Cells.Add(td);
                t.Controls.Add(tr);
                pal_select1_inner.Controls.Add(t);
            }
        }
        protected void btn_giver_Click(object sender, EventArgs e)
        {

            CheckBoxList ck = (CheckBoxList)pal_select1_inner.FindControl("cki1");
            if (ck != null)
            {
                for (int j = 0; j < ck.Items.Count; j++)
                {
                    if (ck.Items[j].Selected == true)
                    {
                        giverid.Value = ck.Items[j].Value.ToString();
                        txt_giver.Text = ck.Items[j].Text.ToString();
                        return;
                    }
                }
            }

        }
    }
}
