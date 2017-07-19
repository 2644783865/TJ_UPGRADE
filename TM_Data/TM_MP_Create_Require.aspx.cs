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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MP_Create_Require : System.Web.UI.Page
    {
        string sqlText;
        string mp_no;
        string mp_pici;
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
                GetDataFromGrid();
            }
        }

        //初始化页面
        private void InitInfo()
        {
            string[] fields = Request.QueryString["action"].ToString().Split(' ');
            tsaid.Text = fields[0].ToString();
            tablename.Value = fields[1].ToString();
            sqlText = "select TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsaid.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                lab_proname.Text = dr[0].ToString();
                lab_engname.Text = dr[1].ToString();
                eng_type.Value = dr[2].ToString();
                pro_id.Value = dr[3].ToString();
            }
            dr.Close();
        }

        //初始化GridView
        private void GetDataFromGrid()
        {
            DataTable dt = new DataTable();
            #region
            dt.Columns.Add("MP_MARID");
            dt.Columns.Add("MP_NAME");
            dt.Columns.Add("MP_GUIGE");
            dt.Columns.Add("MP_CAIZHI");
            dt.Columns.Add("MP_UNIT");
            dt.Columns.Add("MP_WEIGHT");
            dt.Columns.Add("MP_NUMBER");
            dt.Columns.Add("MP_STANDARD");
            dt.Columns.Add("MP_USAGE");
            dt.Columns.Add("MP_TIMERQ");
            dt.Columns.Add("MP_TYPE");
            dt.Columns.Add("MP_ENVREFFCT");
            dt.Columns.Add("MP_NOTE");
            #endregion
            for (int i = GridView1.Rows.Count; i < 20; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        //定义临时Table
        protected DataTable TempTable()
        {
            DataTable dt = new DataTable();
            #region
            dt.Columns.Add("MP_MARID");
            dt.Columns.Add("MP_NAME");
            dt.Columns.Add("MP_GUIGE");
            dt.Columns.Add("MP_CAIZHI");
            dt.Columns.Add("MP_UNIT");
            dt.Columns.Add("MP_WEIGHT");
            dt.Columns.Add("MP_NUMBER");
            dt.Columns.Add("MP_STANDARD");
            dt.Columns.Add("MP_USAGE");
            dt.Columns.Add("MP_TIMERQ");
            dt.Columns.Add("MP_TYPE");
            dt.Columns.Add("MP_ENVREFFCT");
            dt.Columns.Add("MP_NOTE");
            #endregion
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)gRow.FindControl("marid")).Text;
                newRow[1] = ((HtmlInputText)gRow.FindControl("name")).Value;
                newRow[2] = ((HtmlInputText)gRow.FindControl("guige")).Value;
                newRow[3] = ((HtmlInputText)gRow.FindControl("caizhi")).Value;
                newRow[4] = ((HtmlInputText)gRow.FindControl("unit")).Value;
                newRow[5] = ((HtmlInputText)gRow.FindControl("weight")).Value;
                newRow[6] = ((HtmlInputText)gRow.FindControl("number")).Value;
                newRow[7] = ((HtmlInputText)gRow.FindControl("standard")).Value;
                newRow[8] = ((HtmlInputText)gRow.FindControl("application")).Value;
                newRow[9] = ((HtmlInputText)gRow.FindControl("time")).Value;
                newRow[10] = ((HtmlInputText)gRow.FindControl("category")).Value;
                newRow[11] = ((HtmlInputText)gRow.FindControl("influence")).Value;
                newRow[12] = ((HtmlInputText)gRow.FindControl("remark")).Value;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        //生成输入行函数
        private void CreateNewRow(int num)
        {
            DataTable dt = this.TempTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }

        //材料计划新增行
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (add_id.Value == "1")
            {
                CreateNewRow(Convert.ToInt32(txtnum.Value));
                txtnum.Value = "";
            }
        }

        //材料计划插入
        protected void btninsert_Click(object sender, EventArgs e)
        {
            if (istid.Value == "1")
            {
                DataTable dt = this.TempTable();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        DataRow newRow = dt.NewRow();
                        dt.Rows.InsertAt(newRow, i + 1 + count);
                        //dt.Rows.RemoveAt(GridView1.Rows.Count - 1);
                        count++;
                    }
                }
                istid.Value = "0";
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
            }
        }

        //材料计划删除
        protected void btndelete_Click(object sender, EventArgs e)
        {
            //***********删除数据不对数据库操作************
            if (txtid.Value == "1")
            {
                DataTable dt = this.TempTable();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        dt.Rows.RemoveAt(i - count);
                        count++;
                    }
                }
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
            }
        }

        //材料计划批次
        private void mpNum()
        {
            sqlText = "select count(*) from TBPM_MPFORALLRVW ";
            sqlText += "where MP_ENGID='" + tsaid.Text + "' and cast(MP_STATE as int)>1";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                mp_pici = (int.Parse(dr[0].ToString()) + 1).ToString();
            }
            dr.Close();
            mp_no = tsaid.Text + "." + "JSB MP" + "/" + Session["UserNameCode"] + "/" + mp_pici.PadLeft(2, '0');
        }

        //保存材料
        protected void btnsave_Click(object sender, EventArgs e)
        {
            mpNum();//材料计划批号
            sqlText = "select count(*) from TBPM_MPFORALLRVW where MP_ID='" + mp_no + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                count = int.Parse(dr[0].ToString());
            }
            dr.Close();
            if (count == 0)
            {
                sqlText = "insert into TBPM_MPFORALLRVW ";
                sqlText += "(MP_ID,MP_PJID,MP_ENGID,MP_ENGTYPE,MP_SUBMITID,";
                sqlText += "MP_CHECKLEVEL) VALUES ('" + mp_no + "','" + pro_id.Value + "',";
                sqlText += "'" + tsaid.Text + "',";
                sqlText += "'" + eng_type.Value + "','" + Session["UserID"] + "','3')";
                DBCallCommon.ExeSqlText(sqlText);
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                string marid = ((TextBox)gr.FindControl("marid")).Text;
                if (marid != "")
                {
                    string mp_tracknum = tsaid.Text + '_' + mp_pici.PadLeft(2, '0') + '_' + i.ToString().PadLeft(4, '0');//计划跟踪号
                    string name = ((HtmlInputText)gr.FindControl("name")).Value;
                    string guige = ((HtmlInputText)gr.FindControl("guige")).Value;
                    string caizhi = ((HtmlInputText)gr.FindControl("caizhi")).Value;
                    string unit = ((HtmlInputText)gr.FindControl("unit")).Value;
                    string weight = ((HtmlInputText)gr.FindControl("weight")).Value;
                    string number = ((HtmlInputText)gr.FindControl("number")).Value;
                    string standard = ((HtmlInputText)gr.FindControl("standard")).Value;
                    string application = ((HtmlInputText)gr.FindControl("application")).Value;
                    string time = ((HtmlInputText)gr.FindControl("time")).Value;
                    string category = ((HtmlInputText)gr.FindControl("category")).Value;
                    string influence = ((HtmlInputText)gr.FindControl("influence")).Value;
                    string remark = ((HtmlInputText)gr.FindControl("remark")).Value;
                    sqlText = "insert into "+tablename.Value+" ";
                    sqlText += "(MP_PID,MP_CHGPID,MP_MARID,MP_PJID,MP_ENGID,";
                    sqlText += "MP_WEIGHT,MP_NUMBER,MP_USAGE,";
                    sqlText += "MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_NOTE,MP_TRACKNUM) ";
                    sqlText += "values ('" + mp_no + "','','" + marid + "','" + pro_id.Value + "',";
                    sqlText += "'" + tsaid.Text + "',";
                    sqlText += "'" + weight + "','" + number + "','" + application + "',";
                    sqlText += "'" + category + "','" + time + "','" + influence + "','" + remark + "','"+mp_tracknum+"')";
                    DBCallCommon.ExeSqlText(sqlText);
                }
            }
            //Response.Write("<script language=javascript>alert('材料保存成功!');window.location.reload();</script>");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:材料保存成功!');window.location.reload();", true);
        }
    }
}
