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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class tbds_locinfo_detail : System.Web.UI.Page
    {
        string location_id = "";
        string addgropstr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            message.Visible = false;
            InitVar();
            if (!IsPostBack)
            {
                InitInfo();
                
            }
        }
        //初始化变量
        private void InitVar()
        {
           string  actionstr = Request.QueryString["Action"].ToString();
            if (actionstr == "add")
            {
                btnConfirm.Text = "  添 加  ";
                addgropstr = Request.QueryString["selectaddgroup"].ToString();//获取添加级别（一级/二级
                if (addgropstr == "1")                                                //一级，添加部门，默认无父节点，编码为两位
                {
                    lblfatherName.Visible = false;
                    ddlfatherName.Visible = false;
                    this.Title = "添加地区";
                    Laddmessage.Text = "添加一级地区";
                    txtID.Text = getlocationcode();
                    txtID.Enabled = false;
                    ddlfatherName.Items.Clear();
                    ListItem item = new ListItem();
                    item.Text = "0";
                    item.Value = "0";
                    ddlfatherName.Items.Insert(0, item);
                }
                if (addgropstr == "2")
                {
                    
                    lblfatherName.Visible = true;
                    ddlfatherName.Visible = true;
                    this.Title = "添加地区";
                    Laddmessage.Text = "添加二级地区";
                }
                txtName.Text = Session["UserName"].ToString();
                txtstarttime.Text = System.DateTime.Now.ToString();
            }
            else
            {
                btnConfirm.Text = "  修 改  ";
                string updateid = Request.QueryString["id"].ToString();
                this.Title = "修改地区";
                Laddmessage.Text = "修改地区";
                txtName.Text = Session["UserName"].ToString();
                txtstarttime.Text = System.DateTime.Now.ToString();
            }

            if (Request.QueryString["id"] != null)
            {
                location_id = Request.QueryString["id"];
            }
        }
        private string getlocationcode()      //一级地区编码
        {
            string Code = "";
            int code1 = 0;
            string sqltxt = "select CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='ROOT'";
            DataSet ds = DBCallCommon.FillDataSet(sqltxt);
            if (ds.Tables[0].Rows.Count > 1)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 2; i++)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[i + 1]["CL_CODE"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["CL_CODE"].ToString()) != 1)
                    {
                        code1 = Convert.ToInt32(ds.Tables[0].Rows[i]["CL_CODE"]) + 1;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (code1 == 0)
                {
                    code1 = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["CL_CODE"].ToString()) + 1;
                }
                if (code1 < 10)
                {
                    Code = "0" + code1.ToString();
                }
                else
                {
                    Code = code1.ToString();
                }
            }
            else if (ds.Tables[0].Rows.Count == 1)
            {
                Code = "02";
            }
            else
            {
                Code = "01";
            }
            return Code;
        }

        private void GetDepartment()    //dropdownlist绑定
        {
            string sqlText = "select distinct CL_CODE,CL_NAME from TBCS_LOCINFO where CL_FATHERCODE='ROOT'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlfatherName.DataSource = dt;
            ddlfatherName.DataTextField = "CL_NAME";
            ddlfatherName.DataValueField = "CL_CODE";
            ddlfatherName.DataBind();
            ListItem item = new ListItem();
            item.Text = "--请选择上级地区--";
            item.Value = "0";
            ddlfatherName.Items.Insert(0, item);
        }

        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            GetDepartment();
            if (location_id != "")//修改
            {
                
                string sqlText = "select * from TBCS_LOCINFO  where CL_CODE='" + location_id + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr.Read())
                { 
                    if (dr["CL_FATHERCODE"].ToString() == "ROOT")
                    {
                        lblfatherName.Visible = false;
                        ddlfatherName.Visible = false;
                        txtID.Text = dr["CL_CODE"].ToString();
                        txtID.Enabled = false;
                        txtLocname.Text = dr["CL_NAME"].ToString();
                        txtLocname.Enabled = false;
                    }
                    else
                    {
                        lblfatherName.Visible = true;
                        ddlfatherName.Visible = true;
                        string sqlText1 = "select CL_NAME from TBCS_LOCINFO where CL_CODE='" + dr["CL_FATHERCODE"].ToString() + "'";
                        SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqlText1);
                        if (dr1.Read())
                        {
                            ddlfatherName.Items.FindByText(dr1["CL_NAME"].ToString()).Selected = true;
                            ddlfatherName.Enabled = false;
                            txtID.Text = dr["CL_CODE"].ToString();
                            txtID.Enabled = false;
                        }
                        txtLocname.Text = dr["CL_NAME"].ToString();
                        txtLocname.Enabled = true;
                        dr1.Close();
                    }
                    txtNote.Text = dr["CL_NOTE"].ToString();
                }
                dr.Close();
            }
        }
        protected void fatherlocation_SelectedIndexChanged(object sender, EventArgs e)      //二级地区编码
        {
            string sqltxt = "select CL_CODE from TBCS_LOCINFO  where CL_NAME='" + ddlfatherName.SelectedItem.Text.Trim() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltxt);
            if (dr.Read())
            {
                txtID.Text = getsecondlocationid(dr["CL_CODE"].ToString()); //生产二级地区编码
                txtID.Enabled = false;
            }
            dr.Close();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string sqlText = "";
            if (btnConfirm.Text == "  修 改  ")
            {   
                sqlText = "update TBCS_LOCINFO set ";
                if (ddlfatherName.SelectedItem.Text.Trim() == "--请选择上级地区--")
                {
                    sqlText += "CL_NAME='" + txtLocname.Text.Trim()+ "',";
                }
                else
                {
                    if (IsExist(txtLocname.Text.Trim()))
                    {
                        message.Visible = true;
                        message.Text = "已经存在该地区，请确保地区的惟一性";
                        return;
                    }
                    else
                    {
                        sqlText += "CL_NAME='" + txtLocname.Text.Trim() + "',";
                    }

                }
                sqlText += "CL_MANCLERK='" + Session["UserName"].ToString().Trim() + "',";
                sqlText += "CL_FILLDATE='" + System.DateTime.Now.ToString().Trim() + "',";
                sqlText += "CL_NOTE='" + txtNote.Text.Trim() + "'";
                sqlText += " where CL_CODE=" + location_id;
            }

            if (btnConfirm.Text == "  添 加  ")
            {
                //Response.Write(actionstr); Response.End();
                sqlText = "insert into TBCS_LOCINFO(CL_CODE,CL_NAME,CL_FATHERCODE,CL_MANCLERK,CL_FILLDATE,CL_NOTE) values (";
                if (addgropstr == "1")
                {
                    if (IsExist(txtLocname.Text.Trim()))
                    {
                        message.Visible = true;
                        message.Text = "已经存在该地区，请确保地区的惟一性";
                        return;
                    }
                    else
                    {
                        sqlText += "'" + txtID.Text.Trim() + "',";
                        sqlText += "'" + txtLocname.Text.Trim() + "',";
                    }
                    sqlText += "'ROOT',";
                    sqlText += "'" + txtName.Text.Trim() + "',";
                    sqlText += "'" + txtstarttime.Text.Trim() + "',";
                    sqlText += "'" + txtNote.Text.Trim() + "')";
                }
                else
                {
                    if (IsExist(txtLocname.Text.Trim()))
                    {
                        message.Visible = true;
                        message.Text = "已经存在该地区，请确保地区的惟一性";
                        return;
                    }
                    else
                    {
                        sqlText += "'" + txtID.Text.Trim() + "',";
                        sqlText += "'" + txtLocname.Text.Trim() + "',";
                    }
                    sqlText += "'" + ddlfatherName.SelectedItem.Value.ToString() + "',";
                    sqlText += "'" + txtName.Text.Trim() + "',";
                    sqlText += "'" + txtstarttime.Text.Trim() + "',";
                    sqlText += "'" + txtNote.Text.Trim() + "')";
                }      
            }
            DBCallCommon.ExeSqlText(sqlText);
            Response.Write("<script>javascript:window.close();</script>");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");

        }

        //验证地区名称是不是存在
        private bool IsExist(string name)
        {
            string sqlText = "select CL_CODE from TBCS_LOCINFO where CL_NAME='" + name + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                dr.Close();
                return true;
            }
            dr.Close();
            return false;
        }

        //自动生成二级地区的两位编号
        private string getsecondlocationid(string strcode)
        {
            string Code = "";
            int code1 = 0;
            string sqltxt = "select CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='"+strcode +"'";
            DataSet ds = DBCallCommon.FillDataSet(sqltxt);
            if (ds.Tables[0].Rows.Count > 1)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 2; i++)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[i + 1]["CL_CODE"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["CL_CODE"].ToString()) != 1)
                    {
                         code1  = Convert.ToInt32(ds.Tables[0].Rows[i]["CL_CODE"].ToString()) + 1;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (code1 == 0)
                {
                    code1 = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["CL_CODE"].ToString()) + 1;
                }
                if (code1 < 1000)
                {
                    Code = "0" + code1.ToString();
                }
                else
                {
                    Code = code1.ToString();
                }
            }
            else if (ds.Tables[0].Rows.Count == 1)
            {
                Code = strcode + "02";
            }
            else
            {
                Code = strcode + "01";
            }
            return Code;
        }
       
    }
}
