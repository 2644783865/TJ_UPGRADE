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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Marreplace_cl : System.Web.UI.Page
    {
        public string gloabstr
        {
            get
            {
                object str = ViewState["gloabstr"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabstr"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mp_id = Request.QueryString["mp_id"].ToString();
                Tb_mpid.Text = mp_id;
                initpagemess();
            }
        }
        private void initpagemess()
        {
            if (Request.QueryString["ptc"] != null)
            {
                gloabstr = Request.QueryString["ptc"].ToString();
            }
            else
            {
                gloabstr = "";
            }
            string sqltext = "";
            sqltext = "select PR_PJID, PR_PJNAME,PR_ENGID, PR_ENGNAME,PR_DEPID, PR_DEPNAME,PR_REVIEWA,PR_REVIEWANM,PR_REVIEWATIME,PR_SQREID,PR_SQRENAME,PR_FZREID, " +
                      "PR_FZRENAME,PR_NOTE  from TBPC_PCHSPLANRVW where PR_SHEETNO='" + Tb_mpid.Text + "'";
            SqlDataReader dr0 = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr0.Read())
            {
                tb_dep.Text = dr0["PR_DEPNAME"].ToString();
                tb_depid.Text = dr0["PR_DEPID"].ToString();
                tb_pj.Text = dr0["PR_PJNAME"].ToString();
                tb_pjid.Text = dr0["PR_PJID"].ToString();
                tb_pjinfo.Text = tb_pjid.Text + "-" + tb_pj.Text;
                tb_eng.Text = dr0["PR_ENGNAME"].ToString();
                tb_engid.Text = dr0["PR_ENGID"].ToString();
                tb_enginfo.Text = tb_engid.Text + "-" + tb_eng.Text;
                Tb_fuziren.Text = dr0["PR_FZRENAME"].ToString();
                Tb_fuzirenid.Text = dr0["PR_FZREID"].ToString();
                tb_note.Text = dr0["PR_NOTE"].ToString();
                TextBoxexecutor.Text = dr0["PR_REVIEWANM"].ToString();
                TextBoxexecutorid.Text = dr0["PR_REVIEWA"].ToString();
                Tb_shijian.Text = dr0["PR_REVIEWATIME"].ToString();
                Tb_shenqingren.Text = dr0["PR_SQRENAME"].ToString();
                Tb_shenqingrenid.Text = dr0["PR_SQREID"].ToString();
            }
            dr0.Close();
            Purchaseplan_startcontentRepeaterdatabind();
        }
        private void Purchaseplan_startcontentRepeaterdatabind()
        {
            string sqltext = "";
            sqltext = "SELECT PUR_PCODE, PUR_PJID, PUR_PJNAME, PUR_ENGID, PUR_ENGNAME, PUR_PTCODE, " +
                      "PUR_MARID, PUR_MARNAME, PUR_MARNORM, PUR_MARTERIAL, PUR_GUOBIAO, PUR_FIXEDSIZE, PUR_NUM, " +
                      "PUR_NUNIT, PUR_USTNUM, PUR_RPNUM, PUR_PUNIT, PUR_STDATE, PUR_PFDATE, PUR_RFDATE, PUR_VICLERK, " +
                      "PUR_VICLERKNM, PUR_CGMAN, PUR_CGMANNM, PUR_PRONODE, PUR_STATE, PUR_NOTE " +
                      "FROM TBPC_PURCHASEPLAN where PUR_PCODE='" + Tb_mpid.Text + "'";
            DBCallCommon.BindRepeater(Purchaseplan_startcontentRepeater, sqltext);
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (Purchaseplan_startcontentRepeater.Items.Count > 0)
            {
                insertinitialdata();//下推
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据！');", true);
            }
        }
        private void insertinitialdata()
        {
            string sqltext = "";
            string submpid = Tb_mpid.Text.Substring(Tb_mpid.Text.IndexOf(".") + 1, 6);
            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
            {
                sqltext = "INSERT INTO TBPC_PURCHASEPLAN(PUR_PCODE,PUR_PJID,PUR_PJNAME,PUR_ENGID,PUR_ENGNAME,PUR_PTCODE,PUR_MARID,PUR_MARNAME,PUR_MARNORM,PUR_MARTERIAL,PUR_GUOBIAO,PUR_FIXEDSIZE,PUR_NUM,PUR_NUNIT,PUR_USTNUM,PUR_RPNUM,PUR_PUNIT,PUR_STDATE,PUR_PRONODE,PUR_STATE) " +
                                                            "VALUES(@PUR_PCODE,@PUR_PJID,@PUR_PJNAME,@PUR_ENGID,@PUR_ENGNAME,@PUR_PTCODE,@PUR_MARID,@PUR_MARNAME,@PUR_MARNORM,@PUR_MARTERIAL,@PUR_GUOBIAO,@PUR_FIXEDSIZE,@PUR_NUM,@PUR_NUNIT,@PUR_USTNUM,@PUR_RPNUM,@PUR_PUNIT,@PUR_STDATE,@PUR_PRONODE,@PUR_STATE)";
                sqlCmd.CommandText = sqltext;
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@PUR_PCODE", Tb_mpid.Text.ToString());
                sqlCmd.Parameters.AddWithValue("@PUR_PJID", tb_pjid.Text.ToString());
                sqlCmd.Parameters.AddWithValue("@PUR_PJNAME", tb_pj.Text);
                sqlCmd.Parameters.AddWithValue("@PUR_ENGID", tb_engid.Text);
                sqlCmd.Parameters.AddWithValue("@PUR_ENGNAME", tb_engid.Text);
                sqlCmd.Parameters.AddWithValue("@PUR_PTCODE", ((Label)Reitem.FindControl("PUR_PTCODE")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_MARID", ((Label)Reitem.FindControl("PUR_MARID")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_MARNAME", ((Label)Reitem.FindControl("PUR_MARNAME")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_MARNORM", ((Label)Reitem.FindControl("PUR_MARNORM")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_MARTERIAL", ((Label)Reitem.FindControl("PUR_MARTERIAL")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_GUOBIAO", ((Label)Reitem.FindControl("PUR_GUOBIAO")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_FIXEDSIZE", ((Label)Reitem.FindControl("PUR_FIXEDSIZE")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_NUM", ((Label)Reitem.FindControl("PUR_NEDDNUM")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_NUNIT", ((Label)Reitem.FindControl("PUR_NUNIT")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_USTNUM", '0');
                sqlCmd.Parameters.AddWithValue("@PUR_RPNUM", ((Label)Reitem.FindControl("PUR_NEDDNUM")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_PUNIT", ((Label)Reitem.FindControl("PUR_NUNIT")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_STDATE", Tb_shijian.Text);
                sqlCmd.Parameters.AddWithValue("@PUR_PRONODE", '0');
                sqlCmd.Parameters.AddWithValue("@PUR_STATE", '0');
                int rowsnum = sqlCmd.ExecuteNonQuery();
            }
            sqltext = "INSERT INTO TBPC_PCHSPLANRVW(PR_SHEETNO,PR_DEPID,PR_DEPNAME,PR_SQREID,PR_SQRENAME,PR_FZREID,PR_FZRENAME,PR_REVIEWA,PR_REVIEWANM,PR_REVIEWATIME,PR_STATE,PR_PRONODE) " +
                                               "VALUES('" + Tb_mpid.Text.ToString() + "' ,'" + tb_depid.Text + "','" + tb_dep.Text + "','" + Tb_shenqingrenid.Text + "','" + Tb_shenqingren.Text
                                               + "','" + Tb_fuzirenid.Text + "','" + Tb_fuziren.Text + "','" + TextBoxexecutorid.Text.ToString() + "','" + TextBoxexecutor.Text.ToString() + "','" + Tb_shijian.Text + "','000','0')";
            DBCallCommon.ExeSqlText(sqltext);
            if (submpid == "JSB MS")
            {
                sqltext = "UPDATE TBPM_OUTSOURCETOTAL SET OST_STATE='9' WHERE OST_STATE='8' AND OST_OUTSOURCENO='" + Tb_mpid.Text.ToString() + "'";
            }
            else
            {
                sqltext = "UPDATE TBPM_MPFORALLRVW SET MP_STATE='9' WHERE MP_STATE='8' AND MP_ID='" + Tb_mpid.Text.ToString() + "'";
            }
            DBCallCommon.ExeSqlText(sqltext);
            DBCallCommon.closeConn(sqlConn);
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx");
        }
        protected void btn_rep_Click(object sender, EventArgs e)
        {
            if (isselected())//是否选择数据
            {
                string sqltext = "";
                string repcode = generatecode();
                SqlCommand sqlCmd = new SqlCommand();
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                sqlConn.Open();
                sqlCmd.Connection = sqlConn;
                sqltext = "INSERT INTO TBPC_MARREPLACETOTAL(MP_CODE,MP_PJID,MP_PJNAME,MP_ENGID,MP_ENGNAME,MP_FILLFMID,MP_FILLFMNM,MP_FILLFMTIME,MP_STATE,MP_NOTE) VALUES(@MP_CODE,@MP_PJID,@MP_PJNAME,@MP_ENGID,@MP_ENGNAME,@MP_FILLFMID,@MP_FILLFMNM,@MP_FILLFMTIME,@MP_STATE,@MP_NOTE)";
                sqlCmd.CommandText = sqltext;
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@MP_CODE", repcode);
                sqlCmd.Parameters.AddWithValue("@MP_PJID", tb_pjid.Text);
                sqlCmd.Parameters.AddWithValue("@MP_PJNAME", tb_pj.Text);
                sqlCmd.Parameters.AddWithValue("@MP_ENGID", tb_engid.Text);
                sqlCmd.Parameters.AddWithValue("@MP_ENGNAME", tb_eng.Text);
                sqlCmd.Parameters.AddWithValue("@MP_FILLFMID", Session["UserID"].ToString());
                sqlCmd.Parameters.AddWithValue("@MP_FILLFMNM", Session["UserName"].ToString());
                sqlCmd.Parameters.AddWithValue("@MP_FILLFMTIME", DateTime.Now.ToString("yyyy-MM-dd"));
                sqlCmd.Parameters.AddWithValue("@MP_STATE", "000");
                sqlCmd.Parameters.AddWithValue("@MP_NOTE", "");
                int rowsnum1 = sqlCmd.ExecuteNonQuery();
                string sqltext1;
                SqlCommand sqlCmd1 = new SqlCommand();
                sqlCmd1.Connection = sqlConn;
                string sqltext2;
                SqlCommand sqlCmd2 = new SqlCommand();
                sqlCmd2.Connection = sqlConn;
                foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Checked)
                        {
                            sqltext1 = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='3' WHERE PUR_PTCODE='" + ((Label)Reitem.FindControl("PUR_PTCODE")).Text + "'";//相似代用
                            DBCallCommon.ExeSqlText(sqltext1);
                        }
                    }
                }
                foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Checked)
                        {
                            sqltext2 = "INSERT INTO TBPC_MARREPLACEDETAIL(MP_CODE,MP_PTCODE,MP_OLDMARID,MP_OLDMARNAME,MP_OLDGUIGE,MP_OLDCAIZHI,MP_OLDGUOBIAO,MP_OLDUNIT,MP_OLDNUMA,MP_STATE) VALUES(@MP_CODE,@MP_PTCODE,@MP_OLDMARID,@MP_OLDMARNAME,@MP_OLDGUIGE,@MP_OLDCAIZHI,@MP_OLDGUOBIAO,@MP_OLDUNIT,@MP_OLDNUMA,@MP_STATE)";
                            sqlCmd2.CommandText = sqltext2;
                            sqlCmd2.Parameters.Clear();
                            sqlCmd2.Parameters.AddWithValue("@MP_CODE", repcode);
                            sqlCmd2.Parameters.AddWithValue("@MP_PTCODE", ((Label)Reitem.FindControl("PUR_PTCODE")).Text.ToString());
                            sqlCmd2.Parameters.AddWithValue("@MP_OLDMARID", ((Label)Reitem.FindControl("PUR_MARID")).Text.ToString());
                            sqlCmd2.Parameters.AddWithValue("@MP_OLDMARNAME", ((Label)Reitem.FindControl("PUR_MARNAME")).Text.ToString());
                            sqlCmd2.Parameters.AddWithValue("@MP_OLDGUIGE", ((Label)Reitem.FindControl("PUR_MARNORM")).Text.ToString());
                            sqlCmd2.Parameters.AddWithValue("@MP_OLDCAIZHI", ((Label)Reitem.FindControl("PUR_MARTERIAL")).Text.ToString());
                            sqlCmd2.Parameters.AddWithValue("@MP_OLDGUOBIAO", ((Label)Reitem.FindControl("PUR_GUOBIAO")).Text.ToString());
                            sqlCmd2.Parameters.AddWithValue("@MP_OLDUNIT", ((Label)Reitem.FindControl("PUR_NUNIT")).Text.ToString());
                            sqlCmd2.Parameters.AddWithValue("@MP_OLDNUMA", ((Label)Reitem.FindControl("PUR_NUM")).Text.ToString());
                            sqlCmd2.Parameters.AddWithValue("@MP_STATE", "0");
                            int rowsnum2 = sqlCmd2.ExecuteNonQuery();
                        }
                    }
                }
                sqlConn.Close();
                Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_panel.aspx?parentpage=myassign&action=edit&sheetno=" + repcode + "&state=000");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }

        }
        //判断是否选择数据
        protected bool isselected()
        {
            bool temp = false;
            foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        temp = true;
                        break;
                    }
                }
            }
            return temp;
        }
        //代用单号
        private string generatecode()
        {
            string repcode = "";
            string subcode = "";
            string sqltext = "SELECT TOP 1 MP_CODE FROM TBPC_MARREPLACETOTAL WHERE MP_CODE LIKE '" + DateTime.Now.Year.ToString() + "0" + "%' ORDER BY MP_CODE DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                subcode = Convert.ToString(dt.Rows[0][0]).Substring(Convert.ToString(dt.Rows[0][0]).ToString().Length - 5, 5);//后五位流水号
                subcode = Convert.ToString(Convert.ToInt32(subcode) + 1);
                subcode = subcode.PadLeft(5, '0');
            }
            else
            {
                subcode = "00001";
            }
            repcode = DateTime.Now.Year.ToString() + "0" + subcode;
            return repcode;
        }
        protected void btn_back_Click(object sender, EventArgs e)//返回
        {
            if (gloabstr != "")
            {
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_start.aspx?ptc=" + gloabstr + "");
            }
            else
            {
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_start.aspx");
            }
        }
        public string get_pur_bjd(string bjdpr, string bjdst)
        {
            string statestr = "";
            if (bjdpr == "2" && bjdst == "1" || Convert.ToInt32(bjdpr) > 2)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_dd(string ddpr, string ddst)
        {
            string statestr = "";
            if (ddpr == "3")
            {
                statestr = "是";
            }
            else
            {

                statestr = "否";

            }
            return statestr;
        }
    }
}
