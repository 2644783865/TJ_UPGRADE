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
    public partial class EQU_Part_Inbill : System.Web.UI.Page
    {
        string sqltext = "";
        string flag = string.Empty;
        string id = string.Empty;
        string code = "";
        string name = "";
        string type = "";
        int num = 0;
        int numstore = 0;
        int innum = 0;
        string note = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] != null)
                flag = Request.QueryString["FLAG"].ToString();
            if (Request.QueryString["ID"] != null)
                id = Request.QueryString["ID"].ToString();
            if (!IsPostBack)
            {
                if (flag == "PUSH")
                {
                    GetCode();
                    lblInDoc.Text = Session["UserName"].ToString();
                    DocuPerId.Text = Session["UserID"].ToString();
                    lblInDate.Text = DateTime.Now.ToString();
                    this.GetDataByID(id);
                    
                }
            }
            Get_Receiver();
        }
        private void Get_Receiver()
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
        protected void btn_receiver_Click(object sender, EventArgs e)
        {

            CheckBoxList ck = (CheckBoxList)pal_select1_inner.FindControl("cki1");
            if (ck != null)
            {
                for (int j = 0; j < ck.Items.Count; j++)
                {
                    if (ck.Items[j].Selected == true)
                    {
                        receiverid.Value = ck.Items[j].Value.ToString();
                        txt_receiver.Text= ck.Items[j].Text.ToString();
                        return;
                    }
                }
            }

        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DocuNum");

            dt.Columns.Add("EquName");
            dt.Columns.Add("EquType");
            dt.Columns.Add("EquNum");
            dt.Columns.Add("ParNumSto");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((Label)gr.FindControl("lblCode")).Text;
                
                newRow[1] = ((Label)gr.FindControl("lblName")).Text;
                newRow[2] = ((Label)gr.FindControl("lbltype")).Text;
                newRow[3] = ((Label)gr.FindControl("lblNum")).Text;
                newRow[4] = ((TextBox)gr.FindControl("txtNumstore")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        protected void btnDelRow_OnClick(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CHK");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要删除的行！');", true);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
        private void GetCode()
        {
            sqltext = "select TOP 1 dbo.GetCode(InCode) AS TopIndex from EQU_Part_In ORDER BY dbo.GetCode(InCode) DESC";
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
            lblInCode.Text = "PARIN" + code.PadLeft(4, '0');//入库单前缀是PARIN，后接四位数字
        }
        protected void GetDataByID(string id)
        {
            sqltext = "select a.DocuNum,a.EquName,a.EquType,a.EquNum,b.ParNumSto from EQU_Need as a,EQU_Part_Store as b where  a.EquName=b.ParName and a.DocuNum='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count.ToString() != "0")
            {
                DBCallCommon.BindGridView(GridView1, sqltext);
            }
            else
            {
                sqltext = "select DocuNum,EquName,EquType,EquNum,ParNumSto='0' from EQU_Need  where DocuNum='" + id + "'";
                DBCallCommon.BindGridView(GridView1, sqltext); 
            }
            
        }
        private void writedata()
        {
            List<string> list_sql = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                code = ((Label)gr.FindControl("lblCode")).Text;
                name = ((Label)gr.FindControl("lblName")).Text;
                type = ((Label)gr.FindControl("lblType")).Text;
                num = Convert.ToInt32(((Label)gr.FindControl("lblNum")).Text);
                numstore = Convert.ToInt32(((TextBox)gr.FindControl("txtNumstore")).Text);
                innum = Convert.ToInt32(((TextBox)gr.FindControl("txtInNum")).Text);
                numstore += innum;
                note = ((TextBox)gr.FindControl("txtNote")).Text;
                if (innum == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有入库数据！！！');", true);
                    return;
                }
                else
                {
                    sqltext = "insert into EQU_Part_In(InCode,Code,Name,Type,InNum,ReceiverId,InDate,InDocuId,Note)";
                    sqltext += "values('" + lblInCode.Text + "','" + code + "','" + name + "','" + type + "'," + innum + ",'" + receiverid.Value + "','" + lblInDate.Text + "','" + DocuPerId.Text + "','" + note + "')";
                    list_sql.Add(sqltext);
                    if (numstore.ToString() != innum.ToString())
                    {
                        sqltext = "update EQU_Part_Store set ParNumSto=" + numstore + " where ParName='" + name + "'and ParType='" + type + "'";
                        list_sql.Add(sqltext);
                    }
                    else
                    {
                        sqltext = "insert into EQU_Part_Store (ParNumSto,ParName,ParType) values('" + numstore + "','" + name + "','" + type + "')";
                        list_sql.Add(sqltext);
                    }
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.location.href='EQU_Part_Order.aspx'", true);
            }
            else if (st == "NoData")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有入库数据！！！');", true);
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EQU_Part_Order.aspx");
        }
    }
}
