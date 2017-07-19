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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class WL_Material_Maintain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            delete.Click += new EventHandler(delete_Click);
            delete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            if (!IsPostBack)
            {
                getClassOne();
                getClassTwo();
                initialGridView();
            }   
        }

        protected void getClassOne()
        {
            string sql = "SELECT DISTINCT TY_FATHERNAME FROM TBMA_TYPEINFO WHERE TY_FATHERNAME<>'ROOT' ORDER BY TY_FATHERNAME";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DDLclass.DataSource = dt;
            DDLclass.DataTextField = "TY_FATHERNAME";
            DDLclass.DataValueField = "TY_FATHERNAME";
            DDLclass.DataBind();
        }

        protected void getClassTwo()
        {
            string sql = "SELECT DISTINCT TY_NAME FROM TBMA_TYPEINFO WHERE TY_FATHERNAME='" + DDLclass.SelectedItem.Text.ToString() + "'  ORDER BY TY_NAME";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DDLname.DataSource = dt;
            DDLname.DataTextField = "TY_NAME";
            DDLname.DataValueField = "TY_NAME";
            DDLname.DataBind();
        }

        protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            getClassTwo();
        }
        
        protected void initialGridView()
        {
            DataTable dt1 = new DataTable();
            DataColumn tc0 = new DataColumn();
            tc0.DataType = System.Type.GetType("System.String");
            tc0.ColumnName = "name";
            dt1.Columns.Add(tc0);
            DataColumn tc1 = new DataColumn();
            tc1.DataType = System.Type.GetType("System.String");
            tc1.ColumnName = "guige";
            dt1.Columns.Add(tc1);
            DataColumn tc2 = new DataColumn();
            tc2.DataType = System.Type.GetType("System.String");
            tc2.ColumnName = "caizhi";
            dt1.Columns.Add(tc2);
            DataColumn tc3 = new DataColumn();
            tc3.DataType = System.Type.GetType("System.String");
            tc3.ColumnName = "guobiao";
            dt1.Columns.Add(tc3);
            DataColumn tc4 = new DataColumn();
            tc4.DataType = System.Type.GetType("System.String");
            tc4.ColumnName = "helpcode";
            dt1.Columns.Add(tc4);
            DataColumn tc5 = new DataColumn();
            tc5.DataType = System.Type.GetType("System.Single");
            tc5.ColumnName = "meterweight";
            dt1.Columns.Add(tc5);
            DataColumn tc6 = new DataColumn();
            tc6.DataType = System.Type.GetType("System.Single");
            tc6.ColumnName = "meterarea";
            dt1.Columns.Add(tc6);
            DataColumn tc7 = new DataColumn();
            tc7.DataType = System.Type.GetType("System.String");
            tc7.ColumnName = "unit";
            dt1.Columns.Add(tc7);
            DataColumn tc8 = new DataColumn();
            tc8.DataType = System.Type.GetType("System.Decimal");
            tc8.ColumnName = "price";
            dt1.Columns.Add(tc8);
            DataColumn tc9 = new DataColumn();
            tc9.DataType = System.Type.GetType("System.String");
            tc9.ColumnName = "status";
            dt1.Columns.Add(tc9);
            DataColumn tc10 = new DataColumn();
            tc10.DataType = System.Type.GetType("System.String");
            tc10.ColumnName = "comment";
            dt1.Columns.Add(tc10);
            DataRow newRow = dt1.NewRow();
            dt1.Rows.Add(newRow);
            GridView1.DataSource = dt1;
            GridView1.DataBind();
        }

        protected DataTable getDataFromGridView()
        {
            DataTable dt1 = new DataTable();
            DataColumn tc0 = new DataColumn();
            tc0.DataType = System.Type.GetType("System.String");
            tc0.ColumnName = "name";
            dt1.Columns.Add(tc0);
            DataColumn tc1 = new DataColumn();
            tc1.DataType = System.Type.GetType("System.String");
            tc1.ColumnName = "standard";
            dt1.Columns.Add(tc1);
            DataColumn tc2 = new DataColumn();
            tc2.DataType = System.Type.GetType("System.String");
            tc2.ColumnName = "caizhi";
            dt1.Columns.Add(tc2);
            DataColumn tc3 = new DataColumn();
            tc3.DataType = System.Type.GetType("System.String");
            tc3.ColumnName = "guobiao";
            dt1.Columns.Add(tc3);
            DataColumn tc4 = new DataColumn();
            tc4.DataType = System.Type.GetType("System.String");
            tc4.ColumnName = "helpcode";
            dt1.Columns.Add(tc4);
            DataColumn tc5 = new DataColumn();
            tc5.DataType = System.Type.GetType("System.String");
            tc5.ColumnName = "meterweight";
            dt1.Columns.Add(tc5);
            DataColumn tc6 = new DataColumn();
            tc6.DataType = System.Type.GetType("System.String");
            tc6.ColumnName = "meterarea";
            dt1.Columns.Add(tc6);           
            DataColumn tc7 = new DataColumn();
            tc7.DataType = System.Type.GetType("System.String");
            tc7.ColumnName = "unit";
            dt1.Columns.Add(tc7);
            DataColumn tc8 = new DataColumn();
            tc8.DataType = System.Type.GetType("System.String");
            tc8.ColumnName = "price";
            dt1.Columns.Add(tc8);
            DataColumn tc9 = new DataColumn();
            tc9.DataType = System.Type.GetType("System.String");
            tc9.ColumnName = "status";
            dt1.Columns.Add(tc9);
            DataColumn tc10 = new DataColumn();
            tc10.DataType = System.Type.GetType("System.String");
            tc10.ColumnName = "comment";
            dt1.Columns.Add(tc10);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt1.NewRow();
                newRow["name"] = ((TextBox)gRow.FindControl("TextBoxname")).Text;
                newRow["standard"] = ((TextBox)gRow.FindControl("TextBoxstandard")).Text;
                newRow["caizhi"] = ((TextBox)gRow.FindControl("Textcaizhi")).Text;
                newRow["guobiao"] = ((TextBox)gRow.FindControl("TextBoxguobiao")).Text;             
                newRow["helpcode"] = ((TextBox)gRow.FindControl("TextBoxhelpcode")).Text;
                newRow["meterweight"] = ((TextBox)gRow.FindControl("TextBoxmeterweight")).Text;
                newRow["meterarea"] = ((TextBox)gRow.FindControl("TextBoxmeterarea")).Text;               
                newRow["unit"] = ((TextBox)gRow.FindControl("TextBoxunit")).Text;
                newRow["price"] = ((TextBox)gRow.FindControl("TextBoxprice")).Text;
                newRow["status"] = ((RadioButtonList)gRow.FindControl("RadioButtonListstatus")).SelectedItem.Value.ToString();
                newRow["comment"] = ((TextBox)gRow.FindControl("TextBoxcomment")).Text;
                dt1.Rows.Add(newRow);
            }
            return dt1;
        }

        protected void insertRow()
        {
            try
            {
                int count = Convert.ToInt32(TextBoxappend.Text);
                TextBoxappend.Text = "";
                DataTable tb;
                tb = getDataFromGridView();
                for (int i = 0; i < count; i++)
                {
                    DataRow row = tb.NewRow();
                    tb.Rows.Add(row);
                }
                GridView1.DataSource = tb;
                GridView1.DataBind();
                message.Text = "";
            }
            catch
            {
                message.Text = "请正确输入追加的行数！";
            }
        }

        protected void append_Click(object sender, EventArgs e)
        {
            insertRow();
        }

        protected void deleteRow()
        {
            int count = 0;
            DataTable tb;
            tb = getDataFromGridView();
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox chk = ((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1"));
                if (chk.Checked == true)
                {
                    tb.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            GridView1.DataSource = tb;
            GridView1.DataBind();
        }
        
        protected void delete_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        protected int checkData()
        {
            int flag = 1;
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                string tempname1 = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxname")).Text;
                string tempstandard1 = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxstandard")).Text;
                string tempcaizhi = ((TextBox)this.GridView1.Rows[i].FindControl("Textcaizhi")).Text;
                string tempguobiao = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxguobiao")).Text;

                for (int j = i + 1; j < this.GridView1.Rows.Count; j++)
                {
                    string tempname2 = ((TextBox)this.GridView1.Rows[j].FindControl("TextBoxname")).Text;
                    string tempstandard2 = ((TextBox)this.GridView1.Rows[j].FindControl("TextBoxstandard")).Text;
                    string tempcaizhi2 = ((TextBox)this.GridView1.Rows[i].FindControl("Textcaizhi")).Text;
                    string tempguobiao2 = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxguobiao")).Text;
                    if ((tempname1 == tempname2) && (tempstandard1 == tempstandard2) && (tempcaizhi==tempcaizhi2) && (tempguobiao==tempguobiao2))
                    {
                        this.GridView1.Rows[i].BackColor = System.Drawing.Color.Orange;
                        this.GridView1.Rows[j].BackColor = System.Drawing.Color.Orange;
                        flag = 0;
                    }
                }
                string sql = "";
                if (DDLname.SelectedItem.Text == "标准件")
                {
                    sql = "SELECT * FROM TBMA_BZJINFO WHERE BZJ_NAME='" + tempname1 + "' AND BZJ_GUIGE='" + tempstandard1 + "' and BZJ_CAIZHI='"+tempcaizhi+"' and BZJ_GUOBIAO='"+tempguobiao+"' ";
                }
                if ((DDLclass.SelectedItem.Text == "原材料") && (DDLname.SelectedItem.Text != "标准件"))
                {
                    sql = "SELECT * FROM TBMA_RAWMAINFO WHERE RM_NAME='" + tempname1 + "' AND RM_GUIGE='" + tempstandard1 + "' and RM_CAIZHI='"+tempcaizhi+"' AND RM_GUOBIAO='"+tempguobiao+"'  ";
                }
                if (DDLclass.SelectedItem.Text == "低值易耗品")
                {
                    sql = "SELECT * FROM TBMA_LVCGMAINFO WHERE LVCG_NAME='" + tempname1 + "' AND LVCG_GUIGE='" + tempstandard1 + "' AND LVCG_CAIZHI='"+tempcaizhi+"' AND LVCG_GUOBIAO='"+tempguobiao+"' ";
                }
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    this.GridView1.Rows[i].BackColor = System.Drawing.Color.Red;
                    flag = 0;
                }
                dr.Close();
            }
            message.Text = flag.ToString();
            return flag;
        }

        protected void addRecord()
        {
            List<string> sqllist = new List<string>();
            string parentid = getParentID();
            int curmaxid = maxID(parentid);
            string sql = "";

            if (DDLname.SelectedItem.Text == "标准件")
            {
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                    string id = generateID(parentid, curmaxid, i);
                    string name = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxname")).Text;
                    string fullname = generateFullName(name);
                    string standard = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxstandard")).Text;
                    string caizhi = ((TextBox)this.GridView1.Rows[i].FindControl("Textcaizhi")).Text;
                    string guobiao = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxguobiao")).Text;
                    string helpcode = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxhelpcode")).Text;
                    string meterweight = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxmeterweight")).Text;
                    string meterarea = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxmeterarea")).Text;
                    string unit = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxunit")).Text;
              
                    decimal price = 0;
                    if (((TextBox)this.GridView1.Rows[i].FindControl("TextBoxprice")).Text != "")
                    {
                        price = Convert.ToDecimal(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxprice")).Text);
                    }
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string person = Session["UserName"].ToString(); //根据当前登录状况确定，与权限相关
                    string status = ((RadioButtonList)this.GridView1.Rows[i].FindControl("RadioButtonListstatus")).SelectedValue;
                    string comment = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxcomment")).Text;
                    sql = "INSERT INTO TBMA_BZJINFO(BZJ_ID,BZJ_NAME,BZJ_FULLNAME,BZJ_GUIGE,BZJ_CAIZHI,BZJ_GUOBIAO,BZJ_HMCODE,BZJ_UNIT,BZJ_PRICE,BZJ_FATHERID,BZJ_FILLDATE,BZJ_MANCLERK,BZJ_STATE,BZJ_NOTE) VALUES('" + id + "','" + name + "','" + fullname + "','"+standard+"','"+caizhi+"','"+guobiao+"','" + helpcode + "','" + unit + "','" + price + "','" + parentid + "','" + date + "','" + person + "','" + status + "','" + comment + "')";
                    sqllist.Add(sql);
                    sql = "";
                }
                DBCallCommon.ExecuteTrans(sqllist);
            }

            if ((DDLclass.SelectedItem.Text == "原材料") && (DDLname.SelectedItem.Text != "标准件"))
            {
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                    string id = generateID(parentid, curmaxid, i);
                    string name = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxname")).Text;
                    string fullname = generateFullName(name);
                    string standard = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxstandard")).Text;
                    string caizhi = ((TextBox)this.GridView1.Rows[i].FindControl("Textcaizhi")).Text;
                    string guobiao = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxguobiao")).Text;
                    string helpcode = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxhelpcode")).Text;
                    string meterweight = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxmeterweight")).Text;
                    string meterarea = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxmeterarea")).Text;
                    string unit = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxunit")).Text;
                    decimal price = 0;
                    if (((TextBox)this.GridView1.Rows[i].FindControl("TextBoxprice")).Text != "")
                    {
                        price = Convert.ToDecimal(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxprice")).Text);
                    }
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string person = Session["UserName"].ToString(); //根据当前登录状况确定，与权限相关
                    string status = ((RadioButtonList)this.GridView1.Rows[i].FindControl("RadioButtonListstatus")).SelectedValue;
                    string comment = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxcomment")).Text;
                    sql = "INSERT INTO TBMA_RAWMAINFO(RM_ID,RM_NAME,RM_FULLNAME,RM_GUIGE,RM_CAIZHI,RM_GUOBIAO,RM_HMCODE,RM_UNIT,RM_PRICE,RM_FATHERID,RM_FILLDATE,RM_MANCLERK,RM_STATE,RM_NOTE) VALUES('" + id + "','" + name + "','" + fullname + "','" + standard + "','" + caizhi + "','" + guobiao + "','" + helpcode + "','" + unit + "','" + price + "','" + parentid + "','" + date + "','" + person + "','" + status + "','" + comment + "')";
                    sqllist.Add(sql);
                }
                DBCallCommon.ExecuteTrans(sqllist);
            }

            if (DDLclass.SelectedItem.Text == "低值易耗品")
            {
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                    string id = generateID(parentid, curmaxid, i);
                    string name = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxname")).Text;
                    string fullname = generateFullName(name);
                    string standard = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxstandard")).Text;
                    string caizhi = ((TextBox)this.GridView1.Rows[i].FindControl("Textcaizhi")).Text;
                    string guobiao = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxguobiao")).Text;
                    string helpcode = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxhelpcode")).Text;
                    string meterweight = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxmeterweight")).Text;
                    string meterarea = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxmeterarea")).Text;
                    string unit = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxunit")).Text;         
                    decimal price = 0;
                    if (((TextBox)this.GridView1.Rows[i].FindControl("TextBoxprice")).Text != "")
                    {
                        price = Convert.ToDecimal(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxprice")).Text);
                    }
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string person = Session["UserName"].ToString(); //根据当前登录状况确定，与权限相关
                    string status = ((RadioButtonList)this.GridView1.Rows[i].FindControl("RadioButtonListstatus")).SelectedValue;
                    string comment = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxcomment")).Text;
                    sql = "INSERT INTO TBMA_RAWMAINFO(LVCG_ID,LVCG_NAME,LVCG_FULLNAME,LVCG_GUIGE,LVCG_CAIZHI,LVCG_GUOBIAO,LVCG_HMCODE,LVCG_UNIT,LVCG_PRICE,LVCG_FATHERID,LVCG_FILLDATE,LVCG_MANCLERK,LVCG_STATE,LVCG_NOTE) VALUES('" + id + "','" + name + "','" + fullname + "','" + standard + "','" + caizhi + "','" + guobiao + "','" + helpcode + "','" + unit + "','" + price + "','" + parentid + "','" + date + "','" + person + "','" + status + "','" + comment + "')";
                    sqllist.Add(sql);                   
                }
                DBCallCommon.ExecuteTrans(sqllist);
            }
            sqllist.Clear();
        }

        protected string generateID(string parentid,int curmaxid,int i)
        {
            int mcode = curmaxid + i + 1;
            if (mcode < 10)
            {
                return (parentid + "." + "00000" + mcode.ToString());
            }
            if ((10 <= mcode) && (mcode < 100))
            {
                return (parentid + "." + "0000" + mcode.ToString());
            }
            if ((100 <= mcode) && (mcode < 1000))
            {
                return (parentid + "." + "000" + mcode.ToString());
            }
            if ((1000 <= mcode) && (mcode < 10000))
            {
                return (parentid + "." + "00" + mcode.ToString());
            }
            if ((10000 <= mcode) && (mcode < 100000))
            {
                return (parentid + "." + "0" + mcode.ToString());
            }
            if ((100000 <= mcode) && (mcode < 1000000))
            {
                return (parentid + "." + mcode.ToString());
            }
            return "";
        }

        protected int maxID(string pid)
        {
            int tempmax = 0;
            string sql = "";
            DataTable tb = new DataTable();
            DataColumn tc = new DataColumn("mid", System.Type.GetType("System.String"));
            tb.Columns.Add(tc);
            if (DDLname.SelectedItem.Text == "标准件")
            {
                sql = "SELECT DISTINCT BZJ_ID AS mid  FROM TBMA_BZJINFO WHERE BZJ_ID LIKE '" + pid + "_______'";
            }
            if ((DDLclass.SelectedItem.Text == "原材料") && (DDLname.SelectedItem.Text != "标准件"))
            {
                sql = "SELECT DISTINCT RM_ID AS mid  FROM TBMA_RAWMAINFO WHERE RM_ID LIKE '" + pid + "_______'";
            }
            if (DDLclass.SelectedItem.Text == "低值易耗品")
            {
                sql = "SELECT DISTINCT LVCG_ID AS mid  FROM TBMA_LVCGMAINFO WHERE LVCG_ID LIKE '" + pid + "_______'";
            }
            tb = DBCallCommon.GetDTUsingSqlText(sql);
            if (tb.Rows.Count == 0) 
            {
                return tempmax;
            }
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                int temp = Convert.ToInt32((tb.Rows[i]["mid"].ToString()).Substring(6, 6));
                if (temp > tempmax)
                {
                    tempmax = temp;
                }
            }  
            tb.Clear();
            return tempmax;
        }

        protected string generateFullName(string name)
        {
            return (DDLclass.SelectedItem.Text + "_" + DDLname.SelectedItem.Text + "_" + name);
        }

        protected string getParentID()
        {
            string pid = "";
            string sql = "SELECT DISTINCT TY_ID AS fatherid FROM TBMA_TYPEINFO WHERE TY_NAME='" + DDLname.SelectedItem.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                pid = dr["fatherid"].ToString();
            }
            dr.Close();
            return pid;
        }

        protected void confirm_Click(object sender, EventArgs e)
        {
            int flag = checkData();
            if (flag == 1)
            {
                addRecord();
                Response.Redirect("WL_Material_List.aspx");
            }
            else
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Text = "请检查输入数据！";
            }
        }

        protected void goback_Click(object sender, EventArgs e)
        {
            Response.Redirect("WL_Material_List.aspx");
        }

    }
}
