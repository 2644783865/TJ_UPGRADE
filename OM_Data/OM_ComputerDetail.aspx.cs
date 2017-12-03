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
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ComputerDetail : System.Web.UI.Page
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
                if (action == "add")
                {
                    CreateNewRow(1);
                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("Name");
                    //dt.Columns.Add("Type");
                    //dt.Columns.Add("Num");
                    //this.Det_Repeater.DataSource = dt;
                    //this.Det_Repeater.DataBind();

                }
                else
                {
                    hidConext.Value = key;
                    ShowData();
                }
                InitP();

            }
            ContralEnable();
        }


        private void ShowData()
        {

            string sql = "select * from OM_COMPUTERDETAIL where Context='" + key + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();


        }





        protected string generateTempCode()
        {

            List<string> lt = new List<string>();

            string sql = "SELECT Code FROM TBOM_BGYP_INCODE WHERE len(Code)=11 and left(Code,2)='CO'";

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    lt.Add(sdr["Code"].ToString());
                }
            }

            sdr.Close();

            string[] wsidlist = lt.ToArray();

            if (wsidlist.Count<string>() == 0)
            {
                return "CO000000001";
            }
            else
            {
                string tempstr = wsidlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(2, 9)));
                tempnum++;
                tempstr = "CO" + tempnum.ToString().PadLeft(9, '0');
                return tempstr;
            }
        }



        private void InitP()
        {
            if (action == "add")
            {
                //初始化单号

                lblCode.Text = generateTempCode();

                string sql = "INSERT INTO TBOM_BGYP_INCODE(Code) VALUES ('" + lblCode.Text + "')";

                DBCallCommon.ExeSqlText(sql);


                lb1.Text = Session["UserName"].ToString();
                txtTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                lblDep.Text = Session["UserDept"].ToString();

            }
            else
            {
                //Code, SQRId, SQR, SqDepId, SqDep, SqTime, GZRId, GZR, Note, SPLevel, SPRIDA, SPRNMA, SPRESULTA, SPTIMEA, SPNOTEA, SPRIDB, SPRNMB, SPRESULTB, SPTIMEB, SPNOTEB, Context

                string sql = "select * from OM_COMPUTERLIST  where Context='" + key + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lblCode.Text = dt.Rows[0]["Code"].ToString();
                    lb1.Text = dt.Rows[0]["SQR"].ToString();
                    lblDep.Text = dt.Rows[0]["SqDep"].ToString();
                    txtTime.Text = dt.Rows[0]["SqTime"].ToString();
                    txtNote.Text = dt.Rows[0]["Note"].ToString();
                    rblSHJS.SelectedValue = dt.Rows[0]["SPLevel"].ToString();

                    txt_first.Text = dt.Rows[0]["SPRNMA"].ToString();
                    firstid.Value = dt.Rows[0]["SPRIDA"].ToString();
                    first_time.Text = dt.Rows[0]["SPTIMEA"].ToString();
                    first_opinion.Text = dt.Rows[0]["SPNOTEA"].ToString();
                    rblResult1.SelectedValue = dt.Rows[0]["SPRESULTA"].ToString();



                    txt_second.Text = dt.Rows[0]["SPRNMB"].ToString();
                    secondid.Value = dt.Rows[0]["SPRIDB"].ToString();
                    second_time.Text = dt.Rows[0]["SPTIMEB"].ToString();
                    second_opinion.Text = dt.Rows[0]["SPNOTEB"].ToString();
                    rblResult2.SelectedValue = dt.Rows[0]["SPRESULTB"].ToString();


                    //hidConext.Value = key;

                    btnAudit.Visible = false;
                    hidConext.Value = dt.Rows[0]["Context"].ToString();
                    hidState.Value = dt.Rows[0]["State"].ToString();
                }

            }
        }


        //0未提交 1审批中 2通过 3驳回
        private void ContralEnable()
        {
            if (hidAction.Value == "add")
            {
                NoDataPanel.Visible = true;
                btnAudit.Visible = false;
                rblResult1.Enabled = false;
                first_opinion.Enabled = false;
                rblResult2.Enabled = false;
                second_opinion.Enabled = false;

            }
            else
            {



                if (hidAction.Value == "view")
                {
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    btnAudit.Visible = false;
                    btnsubmit.Enabled = false;
                    Panel0.Enabled = false;
                    Panel1.Enabled = false;
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;
                    rblSHJS.Enabled = false;

                }
                else if (hidAction.Value == "edit")
                {
                    hlSelect1.Visible = true;
                    hlSelect2.Visible = true;

                    btnAudit.Visible = false;
                }
                else if (hidAction.Value == "audit")
                {
                    rblSHJS.Enabled = false;
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    btnsubmit.Visible = false;
                    btnAudit.Visible = true;
                    if (hidState.Value == "1")
                    {
                        Panel0.Enabled = false;
                        Panel1.Enabled = false;

                        Panel3.Enabled = false;
                    }
                    if (hidState.Value == "2")
                    {
                        Panel0.Enabled = false;
                        Panel1.Enabled = false;

                        Panel2.Enabled = false;
                    }


                }
            }

            if (rblSHJS.SelectedValue == "0")
            {
                Panel2.Visible = false;
                Panel3.Visible = false;
            }
            else if (rblSHJS.SelectedValue == "1")
            {
                Panel2.Visible = true;
                Panel3.Visible = false;
            }
            else
            {
                Panel2.Visible = true;
                Panel3.Visible = true;
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Type");
            dt.Columns.Add("Num");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("txt1")).Text;
                    newRow[1] = ((DropDownList)retItem.FindControl("ddlType")).SelectedValue;
                    newRow[2] = ((TextBox)retItem.FindControl("txt3")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
                NoDataPanel.Visible = false;
            }
            else
            {
                Response.Write("<script>alert('请输入数字！')</script>");
            }
        }


        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[1] = "0";
                dt.Rows.Add(newRow);
            }

            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();

        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Type");
            dt.Columns.Add("Num");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("txt1")).Text;
                newRow[1] = ((DropDownList)retItem.FindControl("ddlType")).SelectedValue;
                newRow[2] = ((TextBox)retItem.FindControl("txt3")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }


        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            if (Checked())
            {
                List<string> list = new List<string>();

                string sql = "";

                string context = DateTime.Now.ToString("yyyyMMddhhmmss");
                string zdTime = txtTime.Text.Trim();
                string code = generateTempCode();
                string level = rblSHJS.SelectedValue;
                if (action == "add")
                {
                    //Code, SQRId, SQR, SqDepId, SqDep, SqTime, GZRId, GZR, Note, SPLevel, SPRIDA, SPRNMA, SPRESULTA, SPTIMEA, SPNOTEA, SPRIDB, SPRNMB, SPRESULTB, SPTIMEB, SPNOTEB, Context
                    sql = string.Format("insert into OM_COMPUTERLIST values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')", code, Session["UserId"].ToString(), lb1.Text, Session["UserDeptID"].ToString(), lblDep.Text, txtTime.Text.Trim(), "", "", txtNote.Text.Trim(), level, firstid.Value, txt_first.Text.Trim(), "", "", "", secondid.Value, txt_second.Text, "", "", "", context, "0");
                    list.Add(sql);
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        string name = ((TextBox)item.FindControl("txt1")).Text;
                        string type = ((DropDownList)item.FindControl("ddlType")).SelectedValue;
                        string num = ((TextBox)item.FindControl("txt3")).Text;

                        if (name == "")
                        {
                            Response.Write("<script>alert('维修内容不能为空');</script>");
                            return;
                        }

                        sql = string.Format("insert into OM_COMPUTERDETAIL values('{0}','{1}','{2}','{3}')", context, name, type, num);
                        list.Add(sql);
                    }
                    hidConext.Value = context;
                }
                else if (action == "edit")
                {
                    sql = string.Format("update OM_COMPUTERLIST set SqTime='{0}',Note='{1}',SPLevel='{2}',SPRIDA='{3}',SPRNMA='{4}',SPRIDB='{5}',SPRNMB='{6}',State='0' where Context='{7}'", txtTime.Text, txtNote.Text, rblSHJS.SelectedValue, firstid.Value, txt_first.Text.Trim(), secondid.Value, txt_second.Text.Trim(), hidConext.Value);
                    list.Add(sql);
                    sql = "delete from OM_COMPUTERDETAIL where Context='" + hidConext.Value + "'";
                    list.Add(sql);
                    List<string> txt = new List<string>();
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {

                        string name = ((TextBox)item.FindControl("txt1")).Text;
                        string type = ((DropDownList)item.FindControl("ddlType")).SelectedValue;
                        string num = ((TextBox)item.FindControl("txt3")).Text;

                        if (name == "")
                        {
                            Response.Write("<script>alert('维修内容不能为空');</script>");
                            return;
                        }

                        sql = string.Format("insert into OM_COMPUTERDETAIL values('{0}','{1}','{2}','{3}')", hidConext.Value, name, type, num);
                        list.Add(sql);
                    }
                }
                DBCallCommon.ExecuteTrans(list);

                hidState.Value = "0";
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
                Response.Write("<script>alert('请填写制单时间时间！')</script>");
                result = false;
            }
            if (rblSHJS.SelectedValue != "0")
            {
                if (firstid.Value == "")
                {
                    Response.Write("<script>alert('请选择一级审批人！')</script>");
                    result = false;
                }
                if (rblSHJS.SelectedValue == "2" && secondid.Value == "")
                {
                    Response.Write("<script>alert('请选择二级审批人！')</script>");
                    result = false;
                }
            }
            return result;
        }


        //审批
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string sql = "";
            string state = hidState.Value;
            string level = rblSHJS.SelectedValue;
            if (hidAction.Value == "add" || hidAction.Value == "edit")
            {
                sql = "update OM_COMPUTERLIST set state='1' where Context='" + hidConext.Value + "'";
                DBCallCommon.ExeSqlText(sql);
                //邮件提醒
                string _emailto = "";
                if (rblSHJS.SelectedValue == "0")
                {
                    _emailto = DBCallCommon.GetEmailAddressByUserID("286");
                    sql = "update OM_COMPUTERLIST set state='3'  where Context='" + hidConext.Value + "'";
                    DBCallCommon.ExeSqlText(sql);
                }
                else
                    _emailto = DBCallCommon.GetEmailAddressByUserID(firstid.Value);
                string _body = "办公设备报修审批任务:"
                   + "\r\n制单人：" + lb1.Text.Trim()
                   + "\r\n制单日期：" + txtTime.Text.Trim();

                string _subject = "您有新的【办公设备报修】需要审批，请及时处理";
                DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                Response.Write("<script>alert('保存成功！');window.location.href='OM_ComputerLIst.aspx';</script>");

            }
            else if (action == "audit")
            {
                if (state == "1")
                {
                    if (rblResult1.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果！')</script>");
                        return;
                    }
                }
                if (state == "2")
                {
                    if (rblResult2.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果！')</script>");
                        return;
                    }
                }
                //0初始化，1审批中，2一级通过，3全部通过，4驳回
                if (level == "1")
                {
                    if (rblResult1.SelectedValue == "0")
                    {
                        state = "3";
                    }
                    else
                    {
                        state = "4";
                    }
                }
                else if (level == "2")
                {
                    if (state == "1")
                    {
                        if (rblResult1.SelectedValue == "0")
                        {
                            state = "2";
                        }
                        else
                        {
                            state = "4";
                        }
                    }
                    else if (state == "2")
                    {
                        if (rblResult2.SelectedValue == "0")
                        {
                            state = "3";
                        }
                        else
                        {
                            state = "4";
                        }
                    }
                }

                sql = "update OM_COMPUTERLIST set state='" + state + "',SPRESULTA='" + rblResult1.SelectedValue + "',SPTIMEA='" + DateTime.Now.ToString() + "',SPNOTEA='" + first_opinion.Text.Trim() + "',SPRESULTB='" + rblResult2.SelectedValue + "',SPTIMEB='" + DateTime.Now.ToString() + "',SPNOTEB='" + second_opinion.Text.Trim() + "'  where Context='" + hidConext.Value + "'";
                DBCallCommon.ExeSqlText(sql);
                //邮件提醒
                string sqladd = "select state,GZR,SPRIDB from OM_COMPUTERLIST where (state='2'or state='3')and  Context='" + hidConext.Value + "'";
                DataTable dtadd = DBCallCommon.GetDTUsingSqlText(sqladd);
                if (dtadd.Rows.Count > 0)
                {
                    string id_emailto = "";
                    if (dtadd.Rows[0]["state"].ToString() == "2")
                        id_emailto = dtadd.Rows[0]["SPRIDB"].ToString();
                    else
                        id_emailto = dtadd.Rows[0]["GZR"].ToString();
                    string _emailto = DBCallCommon.GetEmailAddressByUserID(id_emailto);
                    string _body = "办公设备报修审批任务:"
                       + "\r\n制单人：" + lb1.Text.Trim()
                       + "\r\n制单日期：" + txtTime.Text.Trim();

                    string _subject = "您有新的【办公设备报修】需要审批，请及时处理";
                    if (dtadd.Rows[0]["state"].ToString() == "3")
                        _subject = "您有新的【办公设备报修】需要确认，请及时处理";
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                }
                Response.Write("<script>alert('保存成功！');window.location.href='OM_ComputerLIst.aspx';</script>");


            }

        }
    }
}
