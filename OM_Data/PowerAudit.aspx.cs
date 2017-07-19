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
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class PowerAudit : System.Web.UI.Page
    {
        string action;
        string auditno;
        public static int roleid;
        protected void Page_Load(object sender, EventArgs e)
        {
            //操作：包括添加add,修改edit,删除delete,查看view,审核audit
            action = Request.QueryString["action"].ToString().Trim();
            //审核编号：识别当前审批单号
            auditno = Request.QueryString["auditno"].ToString().Trim();

            //获取当前单据状态
            string sqlgetstate = "select * from AuditNew where auditno='" + auditno + "'";
            DataTable dtgetstate = DBCallCommon.GetDTUsingSqlText(sqlgetstate);
            if (dtgetstate.Rows.Count > 0)
            {
                hidstate.Value = dtgetstate.Rows[0]["totalstate"].ToString().Trim();
            }
            contrlkjx();//控件可见性和可用性
            if (!IsPostBack)
            {
                //子函数，绑定审批数据
                if (action != "add")
                {
                    binddata();//绑定数据
                }
            }
        }

        //绑定数据
        private void binddata()
        {
            string sql = "select * from AuditNew as a left join PowerContent as b on a.auditno=b.contentno left join View_TBDS_STAFFINFO as c on b.stid=c.ST_ID where auditno='" + auditno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                txt_contentno.Text = dt.Rows[0]["contentno"].ToString().Trim();
                stname.Text = dt.Rows[0]["stname"].ToString().Trim();
                stid.Text = dt.Rows[0]["stid"].ToString().Trim();
                txt_dep.Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                hiddepid.Value = dt.Rows[0]["ST_DEPID"].ToString().Trim();
                DEP_POSITION.Text = dt.Rows[0]["DEP_POSITION"].ToString().Trim();
                role.Text= dt.Rows[0]["R_NAME"].ToString().Trim();
                txt_content.Text = dt.Rows[0]["powercontent"].ToString().Trim();
                string sqlgetroleid = "select R_ID from ROLE_INFO where R_NAME=" + dt.Rows[0]["R_NAME"].ToString().Trim() + "";
                DataTable dtgetroleid = DBCallCommon.GetDTUsingSqlText(sqlgetroleid);
                if (dtgetroleid.Rows.Count > 0)
                {
                    roleid = CommonFun.ComTryInt(dtgetroleid.Rows[0]["R_ID"].ToString().Trim());
                }
                else
                {
                    roleid = 0;
                }
            }
        }

        //控件可见性和可用性
        private void contrlkjx()
        {
            //添加
            if (action == "add")
            {
                btnsave.Visible = true;
            }
            //修改
            if (action == "edit")
            {
                btnsave.Visible = true;
            }
            //查看和审核
            if (action == "view" || action == "audit")
            {
                btnsave.Visible = false;
            }
        }

        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_newstid = (TextBox)sender;

            if (num > 0)
            {
                string st_id = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + st_id + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    stname.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    stid.Text = dt.Rows[0]["ST_ID"].ToString().Trim();
                    txt_dep.Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                    hiddepid.Value = dt.Rows[0]["ST_DEPID"].ToString().Trim();
                    DEP_POSITION.Text = dt.Rows[0]["DEP_POSITION"].ToString().Trim();
                    role.Text = dt.Rows[0]["R_NAME"].ToString().Trim();

                    string sqlgetroleid = "select R_ID from ROLE_INFO where R_NAME=" + dt.Rows[0]["R_NAME"].ToString().Trim() + "";
                    DataTable dtgetroleid = DBCallCommon.GetDTUsingSqlText(sqlgetroleid);
                    if (dtgetroleid.Rows.Count > 0)
                    {
                        roleid = CommonFun.ComTryInt(dtgetroleid.Rows[0]["R_ID"].ToString().Trim());
                    }
                    else
                    {
                        roleid = 0;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }
            }
        }
        //保存
        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            string sqltext = "";
            string shenpibh = "";
            if (txt_content.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入权限变更内容！');", true);
                return;
            }
            if (action == "add")
            {
                shenpibh = "JP" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + "" + Session["UserID"].ToString().Trim() + "";
            }
            else
            {
                shenpibh = auditno;
            }
            if (action == "add")
            {
                sqltext = "insert into PowerContent(contentno,stid,stname,fankui,powercontent) values('" + shenpibh + "','" + stid.Text.Trim() + "','" + stname.Text.Trim() + "','否','" + txt_content.Text.Trim() + "')";
                listsql.Add(sqltext);
                sqltext = "insert into AuditNew(auditno,audittype,addpername,addperid,addtime) values('" + shenpibh + "','" + audittitle.Text.Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "')";//不需要修改
                listsql.Add(sqltext);
            }
            else if (action == "edit")
            {
                sqltext = "update PowerContent set fankui='否',powercontent='" + txt_content.Text.Trim() + "' where contentno='" + shenpibh + "'";
                listsql.Add(sqltext);
            }
            DBCallCommon.ExecuteTrans(listsql);
            Response.Redirect("~/OM_Data/PowerAudit.aspx?action=edit&auditno=" + shenpibh);
        }
    }
}
