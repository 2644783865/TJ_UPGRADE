using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHeDetail_DepartMonth : BasicPage
    {
        string action = string.Empty;
        string key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"];
            if (Request.QueryString["key"] != null)
                key = Request.QueryString["key"];
            if (!IsPostBack)
            {
                hidAction.Value = action;
                hidConext.Value = key;
                if (action == "add")
                {
                    string sql = "select DEP_CODE as DepartId,DEP_NAME as DepartNM,'' as Score,'' as Note from dbo.TBDS_DEPINFO where DEP_CODE like '[0-9][0-9]' and DEP_CODE not in ('08','09','13','01','14','16','17') ";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    this.Det_Repeater.DataSource = dt;
                    this.Det_Repeater.DataBind();

                }
                else
                {

                    ShowData();
                }
                InitP();
                ContralEnable();
            }
            //CheckUser(ControlFinder);
        }

        //0未提交 1审批中 2通过 3驳回
        private void ContralEnable()
        {
            if (hidAction.Value == "add")
            {

                btnAudit.Visible = false;
                rblResult1.Enabled = false;
                first_opinion.Enabled = false;

            }
            else
            {



                if (hidAction.Value == "view")
                {
                    hlSelect1.Visible = false;
                    btnAudit.Visible = false;
                    btnsubmit.Enabled = false;
                    Panel0.Enabled = false;
                    Panel1.Enabled = false;

                }
                else if (hidAction.Value == "edit")
                {
                    hlSelect1.Visible = true;
                    Panel0.Enabled = false;
                    btnAudit.Visible = false;
                }
                else if (hidAction.Value == "audit")
                {
                    hlSelect1.Visible = false;
                    btnsubmit.Visible = false;
                    btnAudit.Visible = true;
                    Panel0.Enabled = false;


                }
            }

        }

        private void ShowData()
        {

            string sql = "select * from TBDS_KaoheDeaprtMonth_Detail  where Context='" + key + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();


        }



        private void InitP()
        {
            if (action == "add")
            {
                lb1.Text = Session["UserName"].ToString();

            }
            else
            {

                //Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM, RESULTA, ZDTIME, TIMEA, OPTIONA, MONEY, JXZong, PeoNum, ConText
                string sql = "select * from TBDS_KaoheDeaprtMonth  where Context='" + key + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    txt_first.Text = dt.Rows[0]["SPRNM"].ToString();
                    firstid.Value = dt.Rows[0]["SPRID"].ToString();
                    first_time.Text = dt.Rows[0]["TIMEA"].ToString();
                    first_opinion.Text = dt.Rows[0]["OPTIONA"].ToString();
                    rblResult1.SelectedValue = dt.Rows[0]["RESULTA"].ToString();
                    txtTime.Text = dt.Rows[0]["ZDTIME"].ToString();

                    hidId.Value = dt.Rows[0]["Id"].ToString();
                    hidConext.Value = key;

                    btnAudit.Visible = false;
                    lb1.Text = dt.Rows[0]["ZDRNM"].ToString();
                    hidState.Value = dt.Rows[0]["State"].ToString();

                    txtKhNianYue.Text = dt.Rows[0]["Year"].ToString() + "-" + dt.Rows[0]["Month"].ToString();

                }

            }
        }

        //审批
        protected void btnAudit_Click(object sender, EventArgs e)
        {

            string sql = "";
            string state = "";
            if (hidAction.Value == "add" || hidAction.Value == "edit")
            {
                sql = "update TBDS_KaoheDeaprtMonth set state='1' where Context='" + hidConext.Value + "'";
                DBCallCommon.ExeSqlText(sql);

                //邮件提醒
                string sprid = "";
                string sptitle = "";
                string spcontent = "";
                sprid = firstid.Value.Trim();
                sptitle = "组织绩效月度成绩审批";
                spcontent = txtKhNianYue.Text.Trim() + "的组织绩效月度成绩需要您审批，请登录查看！";
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                Response.Write("<script>alert('保存成功！');window.location.href='OM_KaoHeList_DepartMonth.aspx';</script>");

            }
            else if (action == "audit")
            {
                if (rblResult1.SelectedIndex == -1)
                {
                    Response.Write("<script>alert('请选择审核结果！')</script>");
                    return;
                }
                else
                {
                    if (rblResult1.SelectedValue == "0")
                    {
                        state = "2";
                    }
                    else if (rblResult1.SelectedValue == "1")
                    {
                        state = "3";
                    }
                    sql = "update TBDS_KaoheDeaprtMonth set state='" + state + "',RESULTA='" + rblResult1.SelectedValue + "',TIMEA='" + DateTime.Now.ToString() + "',OPTIONA='" + first_opinion.Text.Trim() + "'  where Context='" + hidConext.Value + "'";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Write("<script>alert('保存成功！');window.location.href='OM_KaoHeList_DepartMonth.aspx';</script>");

                }
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (Checked())
            {
                List<string> list = new List<string>();

                string sql = "";
                string year = txtKhNianYue.Text.Split('-')[0];
                string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');
                string context = DateTime.Now.ToString("yyyyMMddhhmmss");
                string zdTime = txtTime.Text.Trim();
                if (action == "add")
                {//Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM, RESULTA, ZDTIME, TIMEA, OPTIONA, MONEY
                    //  Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM, RESULTA, ZDTIME, TIMEA, OPTIONA, MONEY, JXZong, PeoNum, ConText
                    sql = string.Format("insert into TBDS_KaoheDeaprtMonth values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", year, month, '0', firstid.Value, txt_first.Text.Trim(), Session["UserId"].ToString(), Session["UserName"].ToString(), "", DateTime.Now.ToString("yyyy-MM-dd"), "", "", context);
                    list.Add(sql); List<string> txt = new List<string>();
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {

                        string DepartNM = ((Label)item.FindControl("txtDepartNM")).Text;
                        string DepartId = ((HtmlInputHidden)item.FindControl("hidDepartId")).Value;
                        string Score = ((TextBox)item.FindControl("txtScore")).Text;
                        string Note = ((TextBox)item.FindControl("txtNote")).Text;

                        //Context, DeaprtId, Note, Score, DepartNM
                        sql = string.Format("insert into TBDS_KaoheDeaprtMonth_Detail values('{0}','{1}','{2}','{3}','{4}')", context, DepartId, Note, Score, DepartNM);
                        list.Add(sql);
                    }
                    hidConext.Value = context;
                    hidState.Value = "0";
                }
                else if (action == "edit")
                {
                    sql = string.Format("update TBDS_KaoheDeaprtMonth set Year='{0}',Month='{1}',State='{2}',SPRID='{3}',SPRNM='{4}' where Context='{5}'", year, month, "0", firstid.Value, txt_first.Text.Trim(), hidConext.Value);
                    list.Add(sql);
                    sql = "delete from TBDS_BZAVERAGE where Context='" + hidConext.Value + "'";
                    list.Add(sql);
                    List<string> txt = new List<string>();
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {

                        string DepartNM = ((Label)item.FindControl("txtDepartNM")).Text;
                        string DepartId = ((HtmlInputHidden)item.FindControl("hidDepartId")).Value;
                        string Score = ((TextBox)item.FindControl("txtScore")).Text;
                        string Note = ((TextBox)item.FindControl("txtNote")).Text;


                        sql = string.Format("insert into TBDS_KaoheDeaprtMonth_Detail values('{0}','{1}','{2}','{3}','{4}')", context, DepartId, Note, Score, DepartNM);
                        list.Add(sql);
                    }
                }
                DBCallCommon.ExecuteTrans(list);
             
                Response.Write("<script>alert('保存成功！');</script>");

                btnsubmit.Visible = false;
                btnAudit.Visible = true;
            }
        }

        private bool Checked()
        {
            bool result = true;
            if (txtTime.Text.Trim() == "")
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请填写考核时间！')", true);
                Response.Write("<script>alert('请填写考核时间！')</script>");
                result = false;
            }
            if (txtKhNianYue.Text.Trim() == "")
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择考核模板！')", true);
                Response.Write("<script>alert('请填写年月！')</script>");
                result = false;
            }
            if (firstid.Value == "")
            {
                Response.Write("<script>alert('请选择审批人！')</script>");
                result = false;
            }
            return result;
        }
    }
}
