using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AjaxControlToolkit;


namespace ZCZJ_DPF.CM_Data
{
    
    public partial class CM_Notice : System.Web.UI.Page
    {
        string action = "";
        string sql = "";
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
            //Response.Redirect("CM_Notice_Main.aspx");
        }

        //生成商务联系单号
        public void Execute_BUSID()
        {
            string pi_id = "";
            string tag_pi_id = "B" + DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "");
            string end_pi_id = "";
            string sqltext = "SELECT TOP 1 DCS_ID FROM TBPM_DEPCONSHEET WHERE DCS_ID LIKE '" + tag_pi_id + "%' ORDER BY DCS_ID DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32((dt.Rows[0]["DCS_ID"].ToString().Substring(dt.Rows[0]["DCS_ID"].ToString().Length - 4, 4))) + 1);
                end_pi_id = end_pi_id.PadLeft(4, '0');
            }
            else
            {
                end_pi_id = "0001";
            }
            pi_id = tag_pi_id + end_pi_id;
            CM_tbID.Text = pi_id;
        }

        //修改
        public void modification()
        {
            sql = "select * from TBPM_DEPCONSHEET where DCS_ID='" + id + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                tbproject.Text = dr["DCS_PROJECT"].ToString();
                ComboBox1.SelectedItem.Text = dr["DCS_PROJECTID"].ToString();
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

            sql = " select * from TBPM_DEPCONSTHNDOUT where DCS_CSID='" + id + "'   ";
            dr = DBCallCommon.GetDRUsingSqlText(sql);
            while (dr.Read())
            {
                 for (int j = 0; j < CB_Department.Items.Count; j++)
                 {
                     if (CB_Department.Items[j].Value == dr["DCS_HDDEPID"].ToString())
                     {
                         CB_Department.Items[j].Selected = true;
                     }
                 }
            }
            dr.Close();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //转换编辑器内容
            string News_content = Server.HtmlEncode(this.txtNews_content.Text.Replace("'", "''"));

            sql = "insert into TBPM_DEPCONSHEET(DCS_ID,DCS_PROJECT,DCS_PROJECTID,DCS_TYPE,DCS_CONTEXT,DCS_EDITOR,DCS_DATE) ";
            sql += " values('" + CM_tbID.Text + "','" + tbproject.Text + "','" + ComboBox1.SelectedItem.Text + "','" + rdtype.SelectedValue + "','" + News_content + "','" + tbEditor.Text + "' ,'" + tbDate.Text + "'   )";
            DBCallCommon.GetDRUsingSqlText(sql);

            string deparetment = "";
            for (int j = 0; j < CB_Department.Items.Count; j++)
            {
                if (CB_Department.Items[j].Selected == true)
                {
                    deparetment = CB_Department.Items[j].Value ;                 

                    sql = "insert into TBPM_DEPCONSTHNDOUT(DCS_CSID,DCS_HDDEPID,DCS_TYPE,DCS_HDDEPNAME) ";
                    sql += " values('" + CM_tbID.Text + "','" + deparetment + "','" + rdtype.SelectedValue + "','" + CB_Department.Items[j].Text + "' )";

                    DBCallCommon.GetDRUsingSqlText(sql);
                }

            }
            //deparetment = deparetment.Substring(0,deparetment.Length-1);
           

            Response.Redirect("CM_Notice_Main.aspx");
           
           
        }


        void comboBoxData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PT_PJID");
            dt.Columns.Add("PT_PJNAME");
            dt.Rows.Add("全部", "0");
            string sql = "select  PJ_ID, PJ_NAME from TBPM_PJINFO";//读取所有的项目名称
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sql);
            while (dr1.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = dr1["PJ_ID"].ToString();
                dr[1] = dr1["PJ_NAME"].ToString();
                dt.Rows.Add(dr);
            }
            dr1.Close();
            ComboBox1.DataSource = dt;
            ComboBox1.DataTextField = "PT_PJID";
            ComboBox1.DataValueField = "PT_PJNAME";
            ComboBox1.DataBind();

        }

        protected void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbproject.Text = ComboBox1.SelectedItem.Value.ToString();
        }

        void departmentBind()
        {
            sql = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO ";
            sql += "where DEP_CODE LIKE '[0-9][0-9]' and DEP_CODE!='" + Session["UserDeptID"] + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
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
                sql = "update TBPM_DEPCONSTHNDOUT set ";
                sql += "DCS_TIME='" + DateTime.Now.ToString() + "',DCS_STATE='1' ";
                sql += "where DCS_CSID='" + id + "' and DCS_HDDEPID='" + dpcode + "'";
                DBCallCommon.ExeSqlText(sql);
            }
        }
        
    }
}
