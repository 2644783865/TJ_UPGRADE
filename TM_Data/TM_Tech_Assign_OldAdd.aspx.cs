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
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Tech_Assign_OldAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetProID();
                this.GetEngID();
            }
        }

        /// <summary>
        /// 绑定项目编号
        /// </summary>
        private void GetProID()
        {
            string sqlText = "";
            sqlText = "select PJ_ID+'‖'+PJ_NAME as PJ_PID from TBPM_PJINFO order by PJ_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            proid.DataSource = dt;
            proid.DataTextField = "PJ_PID";
            proid.DataValueField = "PJ_PID";
            proid.DataBind();
            proid.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            proid.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定工程代号
        /// </summary>
        private void GetEngID()
        {
            string sqlText = "";
            sqlText = "select distinct ENG_ID,ENG_ID+'‖'+ENG_NAME as ENG_NAME from TBPM_ENGINFO ORDER BY ENG_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            engid.DataSource = dt;
            engid.DataTextField = "ENG_NAME";
            engid.DataValueField = "ENG_NAME";
            engid.DataBind();
            engid.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            engid.SelectedIndex = 0;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (txtTaskID.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r请填写【生产制号】！！！');", true);
                return;
            }

            if (txtEngName.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r请填写【工程名称】！！！');", true);
                return;
            }

            if (proid.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r请选择【项目代号】！！！');", true);
                return;
            }

            if (engid.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r请选择【工程简称】！！！');", true);
                return;
            }

            string pattern = @"^(([A-Z]+\/[A-Z]([0-9]|[A-Z])+\/[0-9]{1,3}){1}(\(O\)|\(DQO\)){1}){1}$";
            Regex rgx=new Regex(pattern);

            if (!rgx.IsMatch(txtTaskID.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r【生产制号】格式不正确！！！');", true);
                return;
            }
            else
            {
                string[] aa=txtTaskID.Text.Trim().Split('/');
                if (aa[0] != engid.SelectedValue.Split('‖')[0])
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r【生产制号】中的工程简称与选择的【工程简称】不一致！！！');", true);
                    return;
                }

                if (aa[1] != proid.SelectedValue.Split('‖')[0])
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r【生产制号】中的项目代号与选择的【项目代号】不一致！！！');", true);
                    return;
                }

                //检查生产制号是否存在
                string sql_find_exist = "select TSA_ID from TBPM_TCTSASSGN where TSA_ID='" + txtTaskID.Text.Trim() + "'";
                System.Data.SqlClient.SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_find_exist);
                if (dr.HasRows)
                {
                    dr.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r生产制号已存在！！！');", true);
                    return;
                }
                dr.Close();
                //同一项目下的该制号是否存在
                string[] array_gxn = txtTaskID.Text.Trim().Split('/');
                int number =Convert.ToInt16(array_gxn[2].Substring(0, array_gxn[2].IndexOf('(')));
                string siminar1 = @"%/" + array_gxn[1] + @"/"+number.ToString();
                string siminar2 = @"%/" + array_gxn[1] + @"/" + number.ToString()+"(O)";
                string siminar3 = @"%/" + array_gxn[1] + @"/" + number.ToString()+"(DQO)";
                string siminar4 = @"%/" + array_gxn[1] + @"/" + number.ToString().PadLeft(3,'0')+"(O)";
                string siminar5 = @"%/" + array_gxn[1] + @"/" + number.ToString().PadLeft(3, '0') + "(DQO)";
                string sql_find_same = "select TSA_ID from TBPM_TCTSASSGN where TSA_ID like '" + siminar1 + "' OR TSA_ID like '" + siminar2 + "' OR TSA_ID like '" + siminar3 + "'  OR TSA_ID like '" + siminar4 + "'  OR TSA_ID like '" + siminar5 + "'";
                System.Data.SqlClient.SqlDataReader dr_simr = DBCallCommon.GetDRUsingSqlText(sql_find_same);
                if (dr_simr.HasRows)
                {
                    dr_simr.Read();
                    string task_1 = dr_simr["TSA_ID"].ToString();
                    dr_simr.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r已存在生产制号【"+task_1+"】！！！');", true);
                    return;
                }
                dr_simr.Close();
                //旧生产制号导入
                string sqlText = "";
                string taskid = txtTaskID.Text.Trim();
                string[] fields = taskid.Split('/');
                string engcode = fields[0].ToString();
                string engname = txtEngName.Text.Trim();
                string  engtype = "";
                string smalltype = engid.SelectedValue.Split('‖')[1];//小类
                string dq = "N";
                if (taskid.Contains("DQO"))
                {
                    dq = "Y";
                }

                string sql_dlengtype = "select ET_FTYPE from TBPM_ENGTYPE where ET_STYPE='" + smalltype + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_dlengtype);
                engtype = dt.Rows[0]["ET_FTYPE"].ToString();

                List<String> list_sql = new List<string>();

                //插入技术任务分工表(TBPM_TCTSASSGN)
                sqlText = "insert into TBPM_TCTSASSGN (TSA_ID,TSA_PJID,TSA_ENGNAME,";
                sqlText += "TSA_ENGSTRTYPE,TSA_STFORCODE,TSA_ENGSTRSMTYPE,TSA_ASSIGNTOELC,TSA_ADDTIME) values('" + taskid + "',";
                sqlText += "'" + proid.SelectedValue.Split('‖')[0] + "','" + engname + "',";
                sqlText += "'" + engtype + "','" + engcode + "','" + smalltype + "','" + dq + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                list_sql.Add(sqlText);

                //插入部门完工确认表(TBCB_BMCONFIRM)
                sqlText = "insert into TBCB_BMCONFIRM(TASK_ID,PRJ,ENG) values('" + taskid + "','" + proid.SelectedValue.Split('‖')[1] + "','" + engname + "')";
                list_sql.Add(sqlText);

                DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("TM_Tech_assign.aspx");
                //////ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加成功！！！');", true);

            }
        }

    }
}
