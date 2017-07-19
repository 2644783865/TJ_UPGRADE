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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] == "ADD")
            {
                Confirm.Text = "添加";
            }
            else
            {
                Confirm.Text = "修改";
            }
            if (!IsPostBack)
            {
                GetParent();
                if (Request.QueryString["FLAG"].ToString() == "MODI")
                {
                    string id = Request.QueryString["ID"];
                    string sql = "SELECT * FROM TBWS_WAREHOUSE WHERE WS_ID='" + id + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxName.Text = dr["WS_NAME"].ToString();
                        if (dr["WS_FATHERID"].ToString() == "ROOT")
                        {
                            DropDownListParent.Items.FindByValue("ROOT").Selected = true;
                        }
                        else 
                        {
                            DropDownListParent.Items.FindByValue(dr["WS_FATHERID"].ToString()).Selected = true;
                        }
                        DropDownListParent.Enabled = false;
                        TextBoxNote.Text = dr["WS_NOTE"].ToString();
                    }
                    dr.Close();
                }
            }
        }

        private void GetParent()
        {
            string sql = "SELECT DISTINCT WS_NAME,WS_ID FROM TBWS_WAREHOUSE WHERE WS_FATHERID='ROOT' ORDER BY WS_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DataRow row = dt.NewRow();
            row["WS_NAME"] = "ROOT";
            row["WS_ID"] = "ROOT";
            dt.Rows.InsertAt(row, 0);
            DropDownListParent.DataTextField = "WS_NAME";
            DropDownListParent.DataValueField = "WS_ID";
            DropDownListParent.DataSource = dt;
            DropDownListParent.DataBind();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Confirm.Text == "添加")
            {
                Response.Redirect("SM_Warehouse_Manage.aspx");
            }
            else if (Confirm.Text == "修改")
            {
                Response.Redirect("SM_Warehouse_Manage.aspx?ID=" + Request.QueryString["ID"].ToString());
            }
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Confirm.Text == "修改")
            {
                    string name = TextBoxName.Text;
                    DateTime date = DateTime.Now;
                    string clerk = Session["UserID"].ToString();
                    string note = TextBoxNote.Text;
                    string id = Request.QueryString["ID"];
                    string sql = "";
                    sql = "UPDATE TBWS_WAREHOUSE SET WS_NAME='" + name + "',WS_FILLDATE='" +
                        date + "',WS_CLERK='" + clerk + "',WS_NOTE='" + note + "' WHERE WS_ID='" + id + "'";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Redirect("SM_Warehouse_Manage.aspx?ID=" + Request.QueryString["ID"].ToString());
            }
            if (Confirm.Text == "添加")
            {
                    string sql = "";
                    string name = TextBoxName.Text;
                    sql = "SELECT * FROM TBWS_WAREHOUSE WHERE WS_NAME='" + name + "'";
                    SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr2.Read())
                    {
                        dr2.Close();
                        LabelMessage.Text = "数据库中已经存在该仓库！";
                        return;
                    }
                    dr2.Close();
                    string id = generateID();
                    string pid = DropDownListParent.SelectedValue;
                    DateTime date = DateTime.Now;
                    string clerk = Session["UserID"].ToString();
                    string note = TextBoxNote.Text;
                    sql =
                        "INSERT INTO TBWS_WAREHOUSE(WS_ID,WS_NAME,WS_FATHERID,WS_FILLDATE,WS_CLERK,WS_NOTE) VALUES('" +
                            id + "','" + name + "','" + pid + "','" + date + "','" + clerk + "','" + note + "')";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Redirect("SM_Warehouse_Manage.aspx");
            }
        }

        protected string generateID()
        {
            string levelone = "";
            string idcode = "";
            //插入第一级分类情况
            if (DropDownListParent.SelectedValue == "ROOT")
            {
                DataTable tb = new DataTable();
                string sql = "SELECT DISTINCT WS_ID AS typeid  FROM TBWS_WAREHOUSE WHERE WS_FATHERID='ROOT'";
                tb = DBCallCommon.GetDTUsingSqlText(sql);
                if (tb.Rows.Count == 0)
                {
                    return "01";
                }
                string sqlcom = "SELECT MAX(CAST(WS_ID AS INT)) AS con  FROM TBWS_WAREHOUSE WHERE WS_FATHERID='ROOT'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlcom);
                int count = 0;
                if (dr.Read())
                {
                    count = Convert.ToInt32(dr["con"]);
                }
                dr.Close();
                if (tb.Rows.Count < count)
                {
                    for (int i = 1; i <= 99; i++)
                    {
                        string num = "";
                        if (i < 10)
                        {
                            num = "0" + i.ToString();
                        }
                        else
                        {
                            num = i.ToString();
                        }
                        if (tb.Select("typeid='" + num + "'").Length == 0)
                        {
                            levelone = num;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    int i = tb.Rows.Count;
                    i++;
                    string num = "";
                    if (i < 10)
                    {
                        num = "0" + i.ToString();
                    }
                    else
                    {
                        num = i.ToString();
                    }
                    levelone = num;
                }
                idcode = levelone;
            }

            //插入第二级分类情况
            if (DropDownListParent.SelectedValue != "ROOT")
            {
                //获取一级编号
                string sql = "";
                levelone = DropDownListParent.SelectedValue;
                //获取二级编号
                DataTable tb = new DataTable();
                sql = "SELECT DISTINCT WS_ID AS typeid  FROM TBWS_WAREHOUSE WHERE WS_ID LIKE '" + levelone + "%'";
                tb = DBCallCommon.GetDTUsingSqlText(sql);
                for (int i = 1; i <= 99; i++)
                {
                    string num = "";
                    if (i < 10)
                    {
                        num = levelone + "." + "0" + i.ToString();
                    }
                    else
                    {
                        num = levelone + "." + i.ToString();
                    }
                    if (tb.Select("typeid='" + num + "'").Length == 0)
                    {
                        idcode = num;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return idcode;
        }
    }
}
