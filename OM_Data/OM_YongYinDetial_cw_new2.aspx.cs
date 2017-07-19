using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_YongYinDetial_cw_new2 : System.Web.UI.Page
    {
        string filePath = "";
        string action = string.Empty;
        string key = string.Empty;
        string type = string.Empty;
        string spjb = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"];
            if (Request.QueryString["key"] != null)
                key = Request.QueryString["key"];
            if (Request.QueryString["type"] != null)
                type = Request.QueryString["type"];
            if (Request.QueryString["spjb"] != null)
                spjb = Request.QueryString["spjb"];
            if (!IsPostBack)
            {
                rblSPJB.SelectedValue = spjb;
                hidAction.Value = action;

                InitP();
                if (action == "add")
                {
                    hidType.Value = type;

                    CreateNewRow(1);


                }
                else
                {
                    hidConext.Value = key;

                    ShowData();

                }



            }

            GVBind();
            ContralEnable();

        }
        private void ShowData()
        {

            string sql = "select * from OM_YONGYINDETAIL where Context='" + key + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();


        }


        protected string generateTempCode()
        {

            List<string> lt = new List<string>();

            string sql = "SELECT Code FROM TBOM_BGYP_INCODE WHERE len(Code)=11 and left(Code,2)='YY'";

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
                return "YY000000001";
            }
            else
            {
                string tempstr = wsidlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(2, 9)));
                tempnum++;
                tempstr = "YY" + tempnum.ToString().PadLeft(9, '0');
                return tempstr;
            }
        }



        private void InitP()
        {
            if (type == "6")
            {
                lblType.Text = "公章申请";
            }
            if (type == "7")
            {
                lblType.Text = "资质文件借阅";
            }
            if (type == "8")
            {
                lblType.Text = "领导人姓名章";
            }

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
                string sql = "select * from OM_YONGYINLIST  where Context='" + key + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lblCode.Text = dt.Rows[0]["Code"].ToString();
                    lb1.Text = dt.Rows[0]["SQR"].ToString();
                    lblDep.Text = dt.Rows[0]["SqDep"].ToString();
                    txtTime.Text = dt.Rows[0]["SqTime"].ToString();
                    txtNote.Text = dt.Rows[0]["Note"].ToString();

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
                    hidType.Value = dt.Rows[0]["SPLevel"].ToString();



                    txt_third.Text = dt.Rows[0]["SPRNMC"].ToString();
                    thirdid.Value = dt.Rows[0]["SPRIDC"].ToString();
                    third_time.Text = dt.Rows[0]["SPTIMEC"].ToString();
                    third_opinion.Text = dt.Rows[0]["SPNOTEC"].ToString();
                    rblResult3.SelectedValue = dt.Rows[0]["SPRESULTC"].ToString();

                    txt_fourth.Text = dt.Rows[0]["SPRNMD"].ToString();
                    fourthid.Value = dt.Rows[0]["SPRIDD"].ToString();
                    fourth_time.Text = dt.Rows[0]["SPTIMED"].ToString();
                    fourth_opinion.Text = dt.Rows[0]["SPNOTED"].ToString();
                    rblResult4.SelectedValue = dt.Rows[0]["SPRESULTD"].ToString();
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
                rblResult3.Enabled = false;
                third_opinion.Enabled = false;
                fourth_opinion.Enabled = false;
            }
            else
            {



                if (hidAction.Value == "view")
                {
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                    hlSelect4.Visible = false;
                    btnAudit.Visible = false;
                    btnsubmit.Enabled = false;
                    Panel0.Enabled = false;
                    Panel1.Enabled = false;
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;
                    Panel6.Enabled = false;
                    Panel5.Enabled = false;

                    bntupload.Enabled = false;
                    for (int i = 0; i <= gvfileslist.Rows.Count - 1; i++)
                    {
                        ImageButton imgbtn = (ImageButton)gvfileslist.Rows[i].FindControl("imgbtndelete");
                        imgbtn.Visible = false;
                    }


                }
                else if (hidAction.Value == "edit")
                {
                    NoDataPanel.Visible = true;
                    btnAudit.Visible = false;
                    rblResult1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblResult2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblResult3.Enabled = false;
                    third_opinion.Enabled = false;
                    fourth_opinion.Enabled = false;
                }
                else if (hidAction.Value == "audit")
                {

                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                    hlSelect4.Visible = false;

                    btnsubmit.Visible = false;
                    btnAudit.Visible = true;
                    if (hidState.Value == "1")
                    {
                        Panel0.Enabled = false;
                        Panel1.Enabled = false;
                        Panel5.Enabled = false;
                        Panel3.Enabled = false;
                        Panel4.Enabled = false;
                        Panel6.Enabled = false;
                    }
                    if (hidState.Value == "2")
                    {
                        Panel0.Enabled = false;
                        Panel1.Enabled = false;
                        Panel4.Enabled = false;
                        Panel2.Enabled = false;
                        Panel5.Enabled = false;
                        Panel6.Enabled = false;
                    }
                    if (hidState.Value == "3")
                    {
                        Panel0.Enabled = false;
                        Panel1.Enabled = false;
                        Panel3.Enabled = false;
                        Panel2.Enabled = false;
                        Panel5.Enabled = false;
                        Panel6.Enabled = false;
                    }

                    if (hidState.Value == "7")
                    {
                        Panel0.Enabled = false;
                        Panel1.Enabled = false;
                        Panel3.Enabled = false;
                        Panel2.Enabled = false;
                        Panel5.Enabled = false;
                        Panel4.Enabled = false;
                    }

                    bntupload.Enabled = false;
                    for (int i = 0; i <= gvfileslist.Rows.Count - 1; i++)
                    {
                        ImageButton imgbtn = (ImageButton)gvfileslist.Rows[i].FindControl("imgbtndelete");
                        imgbtn.Visible = false;
                    }


                }
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Type");
            dt.Columns.Add("Num");
            dt.Columns.Add("TaskId");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("txt1")).Text;
                    newRow[1] = ((DropDownList)retItem.FindControl("ddlType")).SelectedValue;
                    newRow[2] = ((TextBox)retItem.FindControl("txt3")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("txtTaskId")).Text;
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
                newRow[1] = "";
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
            dt.Columns.Add("TaskId");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("txt1")).Text;
                newRow[1] = ((DropDownList)retItem.FindControl("ddlType")).SelectedValue;
                newRow[2] = ((TextBox)retItem.FindControl("txt3")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("txtTaskId")).Text;
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
                string code = lblCode.Text;

                if (action == "add")
                {

                    if (rblSPJB.SelectedValue == "1")
                    {
                        sql = string.Format("insert into OM_YONGYINLIST values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}')", code, Session["UserId"].ToString(), lb1.Text, Session["UserDeptID"].ToString(), lblDep.Text, txtTime.Text.Trim(), "", "", txtNote.Text.Trim(), hidType.Value, firstid.Value, txt_first.Text.Trim(), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", context, "0", rblSPJB.SelectedValue);
                        list.Add(sql);
                    }
                    else if (rblSPJB.SelectedValue == "2")
                    {
                        sql = string.Format("insert into OM_YONGYINLIST values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}')", code, Session["UserId"].ToString(), lb1.Text, Session["UserDeptID"].ToString(), lblDep.Text, txtTime.Text.Trim(), "", "", txtNote.Text.Trim(), hidType.Value, firstid.Value, txt_first.Text.Trim(), "", "", "", secondid.Value, txt_second.Text, "", "", "", "", "", "", "", "", "", "", "", "", "", context, "0", rblSPJB.SelectedValue);
                        list.Add(sql);
                    }
                    else if (rblSPJB.SelectedValue == "3")
                    {
                        sql = string.Format("insert into OM_YONGYINLIST values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}')", code, Session["UserId"].ToString(), lb1.Text, Session["UserDeptID"].ToString(), lblDep.Text, txtTime.Text.Trim(), "", "", txtNote.Text.Trim(), hidType.Value, firstid.Value, txt_first.Text.Trim(), "", "", "", secondid.Value, txt_second.Text, "", "", "", thirdid.Value, txt_third.Text, "", "", "", "", "", "", "", "", context, "0", rblSPJB.SelectedValue);
                        list.Add(sql);
                    }
                    else if (rblSPJB.SelectedValue == "4")
                    {
                        sql = string.Format("insert into OM_YONGYINLIST values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}')", code, Session["UserId"].ToString(), lb1.Text, Session["UserDeptID"].ToString(), lblDep.Text, txtTime.Text.Trim(), "", "", txtNote.Text.Trim(), hidType.Value, firstid.Value, txt_first.Text.Trim(), "", "", "", secondid.Value, txt_second.Text, "", "", "", thirdid.Value, txt_third.Text, "", "", "", fourthid.Value, txt_fourth.Text, "", "", "", context, "0", rblSPJB.SelectedValue);
                        list.Add(sql);
                    }


                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        string name = ((TextBox)item.FindControl("txt1")).Text;
                        string type = ((DropDownList)item.FindControl("ddlType")).SelectedValue;
                        string num = ((TextBox)item.FindControl("txt3")).Text;
                        string TaskId = ((TextBox)item.FindControl("txtTaskId")).Text;
                        if (name == "")
                        {
                            Response.Write("<script>alert('文件名称(用途)不能为空！');</script>");
                            return;
                        }
                        if (type == "")
                        {
                            Response.Write("<script>alert('请选择用印类型');</script>");
                            return;
                        }
                        if (num == "")
                        {
                            Response.Write("<script>alert('数量不能为空！');</script>");
                            return;
                        }
                        //if (type == "3" && TaskId == "")
                        //{
                        //    Response.Write("<script>alert('合同号/任务号、项目名称不能为空！');</script>");
                        //    return;

                        //}
                        sql = string.Format("insert into OM_YONGYINDETAIL values('{0}','{1}','{2}','{3}','{4}')", context, name, type, num, TaskId);
                        list.Add(sql);
                    }
                    hidConext.Value = context;
                }
                else if (action == "edit")
                {
                    if (rblSPJB.SelectedValue == "1")
                    {
                        sql = string.Format("update OM_YONGYINLIST set SqTime='{0}',Note='{1}',SPLevel='{2}',SPRIDA='{3}',SPRNMA='{4}',SPRIDB='{5}',SPRNMB='{6}',State='0',SPRIDC='{7}',SPRNMC='{8}',SPRIDD='{9}',SPRNMD='{10}',SPJB='{11}' where Context='{12}'", txtTime.Text, txtNote.Text, hidType.Value, firstid.Value, txt_first.Text.Trim(), "", "", "", "", "", "", rblSPJB.SelectedValue, hidConext.Value);
                        list.Add(sql);
                    }
                    else if (rblSPJB.SelectedValue == "2")
                    {
                        sql = string.Format("update OM_YONGYINLIST set SqTime='{0}',Note='{1}',SPLevel='{2}',SPRIDA='{3}',SPRNMA='{4}',SPRIDB='{5}',SPRNMB='{6}',State='0',SPRIDC='{7}',SPRNMC='{8}',SPRIDD='{9}',SPRNMD='{10}',SPJB='{11}' where Context='{12}'", txtTime.Text, txtNote.Text, hidType.Value, firstid.Value, txt_first.Text.Trim(), secondid.Value, txt_second.Text.Trim(), "", "", "", "", rblSPJB.SelectedValue, hidConext.Value);
                        list.Add(sql);
                    }
                    else if (rblSPJB.SelectedValue == "3")
                    {
                        sql = string.Format("update OM_YONGYINLIST set SqTime='{0}',Note='{1}',SPLevel='{2}',SPRIDA='{3}',SPRNMA='{4}',SPRIDB='{5}',SPRNMB='{6}',State='0',SPRIDC='{7}',SPRNMC='{8}',SPRIDD='{9}',SPRNMD='{10}',SPJB='{11}' where Context='{12}'", txtTime.Text, txtNote.Text, hidType.Value, firstid.Value, txt_first.Text.Trim(), secondid.Value, txt_second.Text.Trim(), thirdid.Value, txt_third.Text.Trim(), "", "", rblSPJB.SelectedValue, hidConext.Value);
                        list.Add(sql);
                    }
                    else if (rblSPJB.SelectedValue == "4")
                    {
                        sql = string.Format("update OM_YONGYINLIST set SqTime='{0}',Note='{1}',SPLevel='{2}',SPRIDA='{3}',SPRNMA='{4}',SPRIDB='{5}',SPRNMB='{6}',State='0',SPRIDC='{7}',SPRNMC='{8}',SPRIDD='{9}',SPRNMD='{10}',SPJB='{11}' where Context='{12}'", txtTime.Text, txtNote.Text, hidType.Value, firstid.Value, txt_first.Text.Trim(), secondid.Value, txt_second.Text.Trim(), thirdid.Value, txt_third.Text.Trim(), fourthid.Value, txt_fourth.Text.Trim(), rblSPJB.SelectedValue, hidConext.Value);
                        list.Add(sql);
                    }

                    sql = "delete from OM_YONGYINDETAIL where Context='" + hidConext.Value + "'";
                    list.Add(sql);
                    List<string> txt = new List<string>();
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {

                        string name = ((TextBox)item.FindControl("txt1")).Text;
                        string type = ((DropDownList)item.FindControl("ddlType")).SelectedValue;
                        string num = ((TextBox)item.FindControl("txt3")).Text;
                        string TaskId = ((TextBox)item.FindControl("txtTaskId")).Text;
                        if (name == "")
                        {
                            Response.Write("<script>alert('文件名称(用途)不能为空！');</script>");
                            return;
                        }
                        if (type == "")
                        {
                            Response.Write("<script>alert('请选择用印类型');</script>");
                            return;
                        }
                        if (num == "")
                        {
                            Response.Write("<script>alert('数量不能为空！');</script>");
                            return;
                        }
                        sql = string.Format("insert into OM_YONGYINDETAIL values('{0}','{1}','{2}','{3}','{4}')", hidConext.Value, name, type, num, TaskId);
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

            if (firstid.Value == "")
            {
                Response.Write("<script>alert('请选择一级审批人！')</script>");
                result = false;
            }
            if (secondid.Value == "" && rblSPJB.SelectedValue == "2")
            {
                Response.Write("<script>alert('请选择二级审批人！')</script>");
                result = false;
            }
            if (rblSPJB.SelectedValue == "3")
            {
                if (secondid.Value == "")
                {
                    Response.Write("<script>alert('请选择二级审批人！')</script>");
                    result = false;
                }
                else if (thirdid.Value == "")
                {
                    Response.Write("<script>alert('请选择三级审批人！')</script>");
                    result = false;
                }
            }
            if (rblSPJB.SelectedValue == "4")
            {
                if (secondid.Value == "")
                {
                    Response.Write("<script>alert('请选择二级审批人！')</script>");
                    result = false;
                }
                else if (thirdid.Value == "")
                {
                    Response.Write("<script>alert('请选择三级审批人！')</script>");
                    result = false;
                }
                else if (fourthid.Value == "")
                {
                    Response.Write("<script>alert('请选择四级审批人！')</script>");
                    result = false;
                }
            }
            return result;
        }




        //审批
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string sql = "";
            string sql_gz = "";
            string state = hidState.Value;
            string newState = "0";
            if (hidAction.Value == "add" || hidAction.Value == "edit")
            {
                sql = "update OM_YONGYINLIST set state='1' where Context='" + hidConext.Value + "'";
                DBCallCommon.ExeSqlText(sql);
                //邮件提醒
                string _emailto = DBCallCommon.GetEmailAddressByUserID(firstid.Value);
                string _body = "用印审批任务:"
                      + "\r\n制单人：" + lb1.Text.Trim()
                      + "\r\n制单日期：" + txtTime.Text.Trim();

                string _subject = "您有新的【用印】需要审批，请及时处理";
                DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                Response.Write("<script>alert('保存成功！');window.location.href='OM_YongYinList.aspx';</script>");

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
                if (state == "3")
                {
                    if (rblResult3.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果！')</script>");
                        return;
                    }
                }
                if (state == "7")
                {
                    if (rblResult4.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果！')</script>");
                        return;
                    }
                }
                //0初始化，1审批中，2一级通过，3二级通过，4全部通过 ，5驳回
                if (rblSPJB.SelectedValue == "1")
                {
                    if (state == "1")
                    {
                        if (rblResult1.SelectedValue == "0")
                        {
                            newState = "4";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                }

                if (rblSPJB.SelectedValue == "2")
                {
                    if (state == "1")
                    {
                        if (rblResult1.SelectedValue == "0")
                        {
                            newState = "2";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                    if (state == "2")
                    {
                        if (rblResult2.SelectedValue == "0")
                        {
                            newState = "4";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }

                }
                else if (rblSPJB.SelectedValue == "3")
                {
                    if (state == "1")
                    {
                        if (rblResult1.SelectedValue == "0")
                        {
                            newState = "2";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                    if (state == "2")
                    {
                        if (rblResult2.SelectedValue == "0")
                        {
                            newState = "3";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                    if (state == "3")
                    {
                        if (rblResult3.SelectedValue == "0")
                        {
                            newState = "4";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                }
                else if (rblSPJB.SelectedValue == "4")
                {
                    if (state == "1")
                    {
                        if (rblResult1.SelectedValue == "0")
                        {
                            newState = "2";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                    if (state == "2")
                    {
                        if (rblResult2.SelectedValue == "0")
                        {
                            newState = "3";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                    if (state == "3")
                    {
                        if (rblResult3.SelectedValue == "0")
                        {
                            newState = "7";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                    if (state == "7")
                    {
                        if (rblResult3.SelectedValue == "0")
                        {
                            newState = "4";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                }


                if (state == "1")
                {
                    sql = "update OM_YONGYINLIST set state='" + newState + "',SPRESULTA='" + rblResult1.SelectedValue + "',SPTIMEA='" + DateTime.Now.ToString() + "',SPNOTEA='" + first_opinion.Text.Trim() + "'  where Context='" + hidConext.Value + "'";
                }
                else if (state == "2")
                {
                    sql = "update OM_YONGYINLIST set state='" + newState + "',SPRESULTB='" + rblResult2.SelectedValue + "',SPTIMEB='" + DateTime.Now.ToString() + "',SPNOTEB='" + second_opinion.Text.Trim() + "'  where Context='" + hidConext.Value + "'";
                }
                else if (state == "3")
                {
                    sql = "update OM_YONGYINLIST set state='" + newState + "',SPRESULTC='" + rblResult3.SelectedValue + "',SPTIMEC='" + DateTime.Now.ToString() + "',SPNOTEC='" + third_opinion.Text.Trim() + "'  where Context='" + hidConext.Value + "'";
                }
                else if (state == "7")
                {
                    sql = "update OM_YONGYINLIST set state='" + newState + "',SPRESULTD='" + rblResult4.SelectedValue + "',SPTIMED='" + DateTime.Now.ToString() + "',SPNOTED='" + fourth_opinion.Text.Trim() + "'  where Context='" + hidConext.Value + "'";
                }
                DBCallCommon.ExeSqlText(sql);


                //邮件提醒
                string sqladd = "select State,GZR,SPRIDB,SPRIDC,SPRIDD from OM_YONGYINLIST where (State='2'or State='3'or State='4')and Context='" + hidConext.Value + "'";
                DataTable dtadd = DBCallCommon.GetDTUsingSqlText(sqladd);
                if (dtadd.Rows.Count > 0)
                {
                    string id_emailto = "";
                    if (dtadd.Rows[0]["State"].ToString() == "2")
                        id_emailto = dtadd.Rows[0]["SPRIDB"].ToString();
                    else if (dtadd.Rows[0]["State"].ToString() == "3")
                        id_emailto = dtadd.Rows[0]["SPRIDC"].ToString();
                    else if (dtadd.Rows[0]["State"].ToString() == "4")
                        id_emailto = dtadd.Rows[0]["SPRIDD"].ToString();
                    else
                        id_emailto = dtadd.Rows[0]["GZR"].ToString();
                    string _emailto = DBCallCommon.GetEmailAddressByUserID(id_emailto);
                    string _body = "用印审批任务:"
                       + "\r\n制单人：" + lb1.Text.Trim()
                       + "\r\n制单日期：" + txtTime.Text.Trim();

                    string _subject = "您有新的【用印】需要审批，请及时处理";
                    if (dtadd.Rows[0]["State"].ToString() == "4")
                        _subject = "您有新的【用印】需要盖章，请及时处理";
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                }
                Response.Write("<script>alert('保存成功！');window.location.href='OM_YongYinList.aspx';</script>");


            }

        }

        //上传
        protected void bntupload_Click(object sender, EventArgs e)
        {
            //执行上传文件
            uploafFile();

        }

        //执行上传
        private void uploafFile()
        {
            int IntIsUF = 0;

            //获取文件保存的路径 
            filePath = @"E:/用印管理附件";//附件上传位置            

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            try
            {
                HttpPostedFile hpf = FileUpload1.PostedFile;

                string fileContentType = hpf.ContentType.ToLower();// 获取客户端发送的文件的 MIME 内容类型  

                if (fileContentType == "application/msword" || fileContentType == "application/vnd.ms-excel" || fileContentType == "application/pdf" || fileContentType == "image/pjpeg" || fileContentType == "image/jpg" || fileContentType == "image/jpeg" || fileContentType == "image/gif" || fileContentType == "image/png" || fileContentType == "image/bmp" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")//传送文件类型
                {
                    if (hpf.ContentLength > 0)
                    {
                        //if (hpf.ContentLength < (2048 * 1024))
                        //{
                        string strFileName = System.IO.Path.GetFileName(hpf.FileName);
                        //string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + strFileName;
                        if (!File.Exists(filePath + @"\" + strFileName))
                        {
                            string sqlStr = string.Empty;
                            //定义插入字符串，将上传文件信息保存在数据库中

                            sqlStr = "insert into OM_YONGYIN_Attach (OM_YY_CODE,SAVEURL,UPLOADDATE,FILENAME)";
                            sqlStr += "values('" + lblCode.Text + "' ";
                            sqlStr += ",'" + filePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString() + "'";
                            sqlStr += ",'" + strFileName + "')";

                            DBCallCommon.ExeSqlText(sqlStr);
                            hpf.SaveAs(filePath + @"\" + strFileName);
                            //isHasFile = true;
                            IntIsUF = 1;
                        }
                        else
                        {
                            filesError.Visible = true;
                            filesError.Text = "上传文件名与服务器文件名重名，请您核对后重新命名上传！";
                            IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError.Visible = true;
                    filesError.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch (Exception e)
            {
                filesError.Text = "文件上传过程中出现错误！" + e.ToString();
                filesError.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "请选择上传文件!";
            }
            GVBind();
        }
        //初始化
        private void GVBind()
        {
            string sql = "select * from OM_YONGYIN_Attach where OM_YY_CODE='" + lblCode.Text + "' ";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            gvfileslist.DataSource = ds.Tables[0];
            gvfileslist.DataBind();
            gvfileslist.DataKeyNames = new string[] { "ID" };
        }

        //删除
        protected void imgbtndelete_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            string idd = ((Label)gvr.FindControl("lbid")).Text;
            //获取文件真实姓名
            string sqlStr = "select SAVEURL,FILENAME from OM_YONGYIN_Attach where ID='" + idd + "'";
            //在文件夹Files下，停用该文件
            DeleteFile(sqlStr);
            string sqlDelStr = "delete from OM_YONGYIN_Attach where ID='" + idd + "'";//停用数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            GVBind();

        }

        //数据库中删除
        protected void DeleteFile(string sqlStr)
        {
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
            //调用File类的Delete方法，停用指定文件
            File.Delete(strFilePath);//文件不存在也不会引发异常
        }

        //下载
        protected void imgbtndownload_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            string idd = ((Label)gvr.FindControl("lbid")).Text;

            //获取文件真实姓名
            string sqlStr = "select SAVEURL,FILENAME from OM_YONGYIN_Attach where ID='" + idd + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
            Response.Write(strFilePath);





            //判断文件是否存在，如果不存在提示重新上传
            if (System.IO.File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }
    }
}
