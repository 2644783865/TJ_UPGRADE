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

using AjaxControlToolkit;
using System.Data.SqlClient;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Contact_List : System.Web.UI.Page
    {
        string action = "";
        string sqlText;
        string id = "";
        string dpcode = "";
        string status = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            tbOriginate.Text = Session["UserDept"].ToString();
            tbEditor.Text = Session["UserName"].ToString();

            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
            }

            if (!IsPostBack)
            {
                departmentBind();
                comboBoxData();
                comboxEngID();
            }

            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"];
                if (action == "add")
                {
                    Execute_BUSID();
                    tbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else if (action == "update")
                {
                    modification();
                }
                else if (action == "view")
                {
                    CB_Department.Enabled = false;
                    btnsubmit.Enabled = false;
                    rdtype.Enabled = false;
                    modification();
                    txtNews_content.Visible = false;
                }
                else
                {
                    string[] fields = action.Split('_');
                    id = fields[0].ToString();
                    dpcode = fields[1].ToString();
                    status = fields[2].ToString();
                    CB_Department.Enabled = false;
                    btnsubmit.Enabled = false;
                    rdtype.Enabled = false;
                    modification();
                    GetInitInfo();//下发部门反馈信息
                    txtNews_content.Visible = false;
                }
            }

        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }

        //生成技术联系单号
        public void Execute_BUSID()
        {
            string pi_id = "";
            string tag_pi_id = "JS"+"." + DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "");
            string end_pi_id = "";
            sqlText = "SELECT TOP 1 DCS_ID FROM TBPM_DEPCONSHEET ";
            sqlText+="WHERE DCS_ID LIKE '" + tag_pi_id + "%' ORDER BY DCS_ID DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32((dt.Rows[0]["DCS_ID"].ToString().Substring(dt.Rows[0]["DCS_ID"].ToString().Length - 4, 4))) + 1);
                end_pi_id = end_pi_id.PadLeft(4, '0');
            }
            else
            {
                end_pi_id = "0001";
            }
            pi_id = tag_pi_id +"."+ end_pi_id;
            CM_tbID.Text = pi_id;
        }

        //修改
        public void modification()
        {
            sqlText = "select * from TBPM_DEPCONSHEET where DCS_ID='" + id + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tbproject.Text = dr["DCS_PROJECT"].ToString();
                ComboBox1.SelectedItem.Text = dr["DCS_PROJECTID"].ToString();
                ComboBox2.SelectedItem.Text = dr["DCS_ENGID"].ToString();
                engname.Text = dr["DCS_ENGNAME"].ToString();
                CM_tbID.Text = dr["DCS_ID"].ToString();
                rdtype.SelectedValue = dr["DCS_TYPE"].ToString();
                tbEditor.Text = dr["DCS_EDITOR"].ToString();
                tbDate.Text = dr["DCS_DATE"].ToString();

                //转换编辑器内容               
                tbcontent.Text = Server.HtmlDecode(dr["DCS_CONTEXT"].ToString());
                txtNews_content.Text = Server.HtmlDecode(dr["DCS_CONTEXT"].ToString());

                tbcontent.Visible = true;
            }
            dr.Close();

            sqlText = " select * from TBPM_DEPCONSTHNDOUT where DCS_CSID='" + id + "' ";
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqlText);
            while (dr1.Read())
            {
                for (int j = 0; j < CB_Department.Items.Count; j++)
                {
                    if (CB_Department.Items[j].Value == dr1["DCS_HDDEPID"].ToString())
                    {
                        CB_Department.Items[j].Selected = true;
                    }
                }
            }
            dr1.Close();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //转换编辑器内容
            string News_content = Server.HtmlEncode(this.txtNews_content.Text.Replace("'", "''"));

            sqlText = "insert into TBPM_DEPCONSHEET(DCS_ID,DCS_PROJECT,DCS_PROJECTID,";
            sqlText += "DCS_ENGID,DCS_ENGNAME,DCS_TYPE,DCS_DEPID,DCS_DEPNAME,";
            sqlText += "DCS_CONTEXT,DCS_EDITORID,DCS_EDITOR,DCS_DATE) values ";
            sqlText += "('" + CM_tbID.Text + "','" + tbproject.Text + "','" + ComboBox1.SelectedItem.Text + "',";
            sqlText += "'"+ComboBox2.SelectedItem.Text+"','"+engname.Text+"','" + rdtype.SelectedValue + "',";
            sqlText += "'" + Session["UserDeptID"] + "','" + Session["UserDept"] + "','" + News_content + "',";
            sqlText += "'" + Session["UserID"] + "','" + tbEditor.Text + "' ,'" + tbDate.Text + "'   )";
            //DBCallCommon.GetDRUsingSqlText(sqlText);
            DBCallCommon.ExeSqlText(sqlText);

            string deparetment = "";
            for (int j = 0; j < CB_Department.Items.Count; j++)
            {
                if (CB_Department.Items[j].Selected == true)
                {
                    deparetment = CB_Department.Items[j].Value;
                    sqlText = "insert into TBPM_DEPCONSTHNDOUT(DCS_CSID,DCS_HDDEPID,DCS_TYPE,";
                    sqlText += "DCS_HDDEPNAME) values ";
                    sqlText += "('" + CM_tbID.Text + "','" + deparetment + "','" + rdtype.SelectedValue + "',";
                    sqlText+=" '" + CB_Department.Items[j].Text + "' )";
                    DBCallCommon.ExeSqlText(sqlText);
                    //DBCallCommon.GetDRUsingSqlText(sqlText);
                }
            }
            Response.Redirect("TM_Notice_Main.aspx");
        }

        //绑定项目ID
        private void comboBoxData()
        {
            sqlText = "select  distinct TSA_PJID, TSA_PJNAME from TBPM_TCTSASSGN ";
            sqlText += "where TSA_TCCLERK='" + Session["UserID"] + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ComboBox1.DataSource = dt;
            ComboBox1.DataTextField = "TSA_PJID";
            ComboBox1.DataValueField = "TSA_PJNAME";
            ComboBox1.DataBind();
            ComboBox1.Items.Insert(0, new ListItem("全部", "0"));
            ComboBox1.SelectedIndex = 0;
        }

        //绑定工程ID
        private void comboxEngID()
        {
            sqlText = "select  TSA_ID, TSA_ENGNAME from TBPM_TCTSASSGN ";
            sqlText += "where TSA_TCCLERK='" + Session["UserID"] + "' ";
            sqlText += "and TSA_PJID='" + ComboBox1.SelectedItem.Text.Trim() + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ComboBox2.DataSource = dt;
            ComboBox2.DataTextField = "TSA_ID";
            ComboBox2.DataValueField = "TSA_ENGNAME";
            ComboBox2.DataBind();
            ComboBox2.Items.Insert(0, new ListItem("全部", "0"));
            ComboBox2.SelectedIndex = 0;
        }

        protected void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbproject.Text = ComboBox1.SelectedValue;
            comboxEngID();
        }

        protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            engname.Text = ComboBox2.SelectedValue;
        }

        private void departmentBind()
        {
            sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO ";
            sqlText += "where DEP_CODE LIKE '[0-9][0-9]' and DEP_CODE!='" + Session["UserDeptID"] + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            CB_Department.DataSource = dt;
            CB_Department.DataTextField = "DEP_NAME";
            CB_Department.DataValueField = "DEP_CODE";
            CB_Department.DataBind();
        }

        //下发部门信息反馈
        private void GetInitInfo()
        {
            if (Session["UserGroup"].ToString().Contains("技术部助理") || Session["UserGroup"].ToString().Contains("技术部长"))
            {
                sqlText = "update TBPM_DEPCONSTHNDOUT set ";
                sqlText += "DCS_TIME='" + DateTime.Now.ToString() + "',DCS_STATE='1' ";
                sqlText += "where DCS_CSID='" + id + "' and DCS_HDDEPID='" + dpcode + "'";
                DBCallCommon.ExeSqlText(sqlText);
            }
        }
    }
}
