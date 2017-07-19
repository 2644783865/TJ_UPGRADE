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
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Manut_Managent_DL : System.Web.UI.Page
    {
        string id = "";
        string state;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["qsaid"].ToString();
            state = Request.QueryString["state"].ToString();
            
            Info();
            if (!IsPostBack)
            {
                InitVar();
            }
        }
        private void Info()  //调度员班组绑定
        {
            ddlduy.Items.Clear();
            ddlffman.Items.Clear();
            ddlduy.Items.Add(new ListItem("--请选择--", "0"));
            ddlffman.Items.Add(new ListItem("--请选择--", "0"));
            string sqltxt = "select ST_NAME from TBDS_STAFFINFO where ST_POSITION like '0404'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltxt);
            foreach (DataRow dr in dt.Rows)
            {
                ListItem listitem = new ListItem(dr["ST_NAME"].ToString(), dr["ST_NAME"].ToString());
                ddlduy.Items.Add(listitem);
                ddlffman.Items.Add(listitem);
            }
            ddlbanzu.Items.Clear();
            ddlbanzu.Items.Add(new ListItem("--请选择--", "0"));
            string sqltt = "select distinct ST_DEPID1 from TBDS_STAFFINFO where (ST_DEPID='08' or ST_DEPID='09') and ST_DEPID1 is not null";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltt);
            foreach (DataRow dr in dt1.Rows)
            {
                ListItem listitem = new ListItem(dr["ST_DEPID1"].ToString(), dr["ST_DEPID1"].ToString());
                ddlbanzu.Items.Add(listitem);
            }
        }
        private void InitVar()
        {
            string sqlstr = "select MTA_ID,MTA_BANZU,MTA_DUY,MTA_PSTIME,MTA_PFTIME,MTA_ENGNAME,TSA_CONTYPE,TSA_TCCLERKNM,MTA_FFMAN from View_TBMP_TASKASSIGN where MTA_ID='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.Read())
            {
                lblID.Text = dr["MTA_ID"].ToString();               
                lblengname.Text = dr["MTA_ENGNAME"].ToString();
                lbltype.Text = dr["TSA_CONTYPE"].ToString();
                lblfuzeren.Text = dr["TSA_TCCLERKNM"].ToString();
                txtbanzu.Text = dr["MTA_BANZU"].ToString();
                txtdiaoduyuan.Text = dr["MTA_DUY"].ToString();
                ttpstime.Value = dr["MTA_PSTIME"].ToString();
                ttpftime.Value = dr["MTA_PFTIME"].ToString();                
                FFMan.Text = dr["MTA_FFMAN"].ToString();
            }
            dr.Close();
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string sqlupdate;
            List<string> list_add = new List<string>();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            if (txtbanzu.Text.Trim() != "" && txtdiaoduyuan.Text.Trim() != "" && ttpstime.Value != "" && ttpftime.Value != "")
            {
                if (state == "0")  //表示登记
                {
                    sqlupdate = "update TBMP_MANUTSASSGN set ";
                    sqlupdate += "MTA_ENGNAME='" + lblengname.Text.Trim() + "',";
                    sqlupdate += "MTA_TCCLERKNM='" + lblfuzeren.Text.Trim() + "',MTA_BANZU='" + txtbanzu.Text.Trim() + "',MTA_DUY='" + txtdiaoduyuan.Text.Trim() + "',";
                    sqlupdate += "MTA_PSTIME='" + ttpstime.Value.ToString() + "',MTA_PFTIME='" + ttpftime.Value.ToString() + "',MTA_MANASSIGN='" + Session["UserName"] + "',";
                    sqlupdate += "MTA_STATUS='1',MTA_NOTE='" + txtbeizhu.Text.Trim() + "',MTA_CHECKDATE='" + date + "',MTA_FFMAN='" + FFMan.Text.Trim() + "' where MTA_ID='" + id + "'";
                    list_add.Add(sqlupdate);

                }
                else if (state == "1")  //表示修改
                {
                    //更新生产任务表
                    sqlupdate = "update TBMP_MANUTSASSGN set MTA_BANZU='" + txtbanzu.Text.Trim() + "',MTA_DUY='" + txtdiaoduyuan.Text.Trim() + "',";
                    sqlupdate += "MTA_PSTIME='" + ttpstime.Value.ToString() + "',MTA_PFTIME='" + ttpftime.Value.ToString() + "',MTA_MANASSIGN='" + Session["UserName"] + "',";
                    sqlupdate += "MTA_NOTE='" + txtbeizhu.Text.Trim() + "',MTA_FFMAN='" + FFMan.Text.Trim() + "' where MTA_ID='" + id + "'";
                    list_add.Add(sqlupdate);
                }
                else if (state == "2")
                {
                    sqlupdate = "update TBMP_MANUTSASSGN set ";
                    sqlupdate += "MTA_ENGNAME='" + lblengname.Text.Trim() + "',";
                    sqlupdate += "MTA_TCCLERKNM='" + lblfuzeren.Text.Trim() + "',MTA_BANZU='" + txtbanzu.Text.Trim() + "',MTA_DUY='" + txtdiaoduyuan.Text.Trim() + "',";
                    sqlupdate += "MTA_PSTIME='" + ttpstime.Value.ToString() + "',MTA_PFTIME='" + ttpftime.Value.ToString() + "',MTA_MANASSIGN='" + Session["UserName"] + "',";
                    sqlupdate += "MTA_STATUS='1',MTA_NOTE='" + txtbeizhu.Text.Trim() + "',MTA_CHECKDATE='" + date + "',MTA_FFMAN='" + FFMan.Text.Trim() + "' where MTA_ID='" + id + "'";
                    list_add.Add(sqlupdate);
                }

                DBCallCommon.ExecuteTrans(list_add);
                //Response.Redirect("PM_Management_View.aspx");
                Response.Write("<script>alert('保存成功');location.href='PM_Management_View.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript'>alert('请确认已填完所有必要信息！！！');</script>"); 
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PM_Management_View.aspx");
        }
    }
}
