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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class WL_Type_Edit : System.Web.UI.Page
    {
        //初始化页面判断是添加还是修改记录
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] == "ADD")
            {
                btnConfirm.Text = "添加";
            }
            else
            {
                btnConfirm.Text = "修改";
            }
            if (!IsPostBack)
            {
                this.Title = "物料分类管理";
                GetParent();
                if (Request.QueryString["FLAG"].ToString() == "MODI")
                {
                    string id = Request.QueryString["ID"];
                    string sql = "SELECT * FROM TBMA_TYPEINFO WHERE TY_ID='" + id + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql); ;
                    if (dr.Read())
                    {
                        t_name.Text = dr["TY_NAME"].ToString();
                        t_parentname.Items.FindByValue(dr["TY_FATHERID"].ToString()).Selected = true;
                        t_parentname.Enabled = false;
                        t_status.SelectedValue = dr["TY_STATE"].ToString();
                        t_comment.Text = dr["TY_NOTE"].ToString();
                    }
                    dr.Close();
                }
            }
        }

        //为父类名称动态绑定选项
        void GetParent()
        {
            string sql = "SELECT DISTINCT TY_ID,TY_NAME FROM TBMA_TYPEINFO WHERE TY_FATHERID='ROOT' ORDER BY TY_NAME";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            t_parentname.DataSource = dt;
            t_parentname.DataTextField = "TY_NAME";
            t_parentname.DataValueField = "TY_ID";
            t_parentname.DataBind();
            t_parentname.Items.Insert(0, "ROOT");
        }

        //执行确定操作
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
           if (btnConfirm.Text == "修改")
            {
                    string name = t_name.Text;
                    string parentname = t_parentname.SelectedItem.Value;
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string person = Session["UserName"].ToString();
                    string status = t_status.SelectedItem.Value;
                    string comment = t_comment.Text;

                    string id = Request.QueryString["ID"];
                    string sql = "";
                    sql = "UPDATE TBMA_TYPEINFO SET TY_NAME='" + name + "',TY_FATHERID='" + parentname + "',TY_FILLDATE='" + date + "',TY_CLERK='" + person + "',TY_STATE='" + status + "',TY_NOTE='" + comment + "' WHERE TY_ID='" + id + "'";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Write("<script>javascript:window.close();</script>");
            }
            if (btnConfirm.Text == "添加")
            {

                    string sql = "";
                    string name = t_name.Text;
                    sql = "SELECT * FROM TBMA_TYPEINFO WHERE TY_NAME='" + name + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        dr.Close();
                        message.Text = "数据库中已经存在该分类！";
                        return;
                    }
                    dr.Close();
                    string id = generateID();
                    string parentname = t_parentname.SelectedItem.Value;
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string person = Session["UserName"].ToString();//当前操作人
                    string status = t_status.SelectedItem.Value;
                    string comment = t_comment.Text;
                    sql =
                        "INSERT INTO TBMA_TYPEINFO(TY_ID,TY_NAME,TY_FATHERID,TY_FILLDATE,TY_CLERK,TY_STATE,TY_NOTE) VALUES('" + id + "','" + name + "','" + parentname + "','" + date + "','" + person + "','" + status + "','" + comment + "')";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Write("<script>javascript:window.close();</script>");
            }
        }

        //ID生成函数
        protected string generateID()
        {
            string levelone = "";
            string idcode = "";
            //插入第一级分类情况
            if (t_parentname.SelectedItem.Value == "ROOT")
            {
                DataTable tb = new DataTable();
                DataColumn tc = new DataColumn("typeid", System.Type.GetType("System.String"));
                tb.Columns.Add(tc);
                string sql = "SELECT DISTINCT TY_ID AS typeid  FROM TBMA_TYPEINFO WHERE TY_FATHERID='ROOT'";
                tb = DBCallCommon.GetDTUsingSqlText(sql);
                if (tb.Rows.Count == 0)
                {
                    return "01";
                }
                string sqlcom = "SELECT MAX(CAST(TY_ID AS INT)) AS con  FROM TBMA_TYPEINFO WHERE TY_FATHERID='ROOT'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlcom);
                dr.Read();
                int count = Convert.ToInt32(dr["con"]); 
                dr.Close();
                if (tb.Rows.Count<count)
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
                        if (tb.Select("typeid='" + num + "'").Length==0)
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
            if (t_parentname.SelectedItem.Value != "ROOT")
            {
                //获取一级编号
                string sql = "";
                sql = "SELECT DISTINCT TY_ID  FROM TBMA_TYPEINFO WHERE TY_ID='" + t_parentname.SelectedItem.Value + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    levelone = dr["TY_ID"].ToString();
                }
                dr.Close();
                //获取二级编号
                DataTable tb = new DataTable();
                DataColumn tc = new DataColumn("typeid", System.Type.GetType("System.String"));
                tb.Columns.Add(tc);
                sql = "SELECT DISTINCT TY_ID AS typeid  FROM TBMA_TYPEINFO WHERE TY_ID LIKE '" + levelone + "%'";
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

        //执行取消操作返回列表
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "window.close()", true);
        }
    }
}
