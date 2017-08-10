using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarApplyDetail : System.Web.UI.Page
    {
        string id = "";
        string state = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"];
                }
                if (Request.QueryString["action"] == "add")
                {
                    span1.Visible = true;
                    Tab_spxx.Visible = false;
                    txt_sfd.Text = "公司本部";
                    sqr.Text = Session["UserName"].ToString();
                    sqrq.Text = DateTime.Now.ToString();
                    ddlleixing();
                    code();
                    btnLoad.Visible = true;
                    ydtime.Visible = false;
                    txtTime1.Visible = false;
                    txtTime2.Visible = false;
                    txtlicheng1.Enabled = false;
                    txtlicheng2.Enabled = false;
                    driver.Enabled = false;
                    carnum.Enabled = false;
                    usetime1.Value = DateTime.Now.ToString();
                    string depid = Session["UserDeptID"].ToString();
                    string sql = "select ID FROM TBOM_SHENHE WHERE TYPE_ID ='" + depid.ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        ddl_leixing.SelectedValue = dr["ID"].ToString();
                    }
                    dr.Close();
                    txtDepartment.Text = Session["UserDept"].ToString();
                }
                else if (Request.QueryString["action"] == "view")
                {
                    ddlleixing();
                    bind_info();
                    txtCode.Enabled = false;
                    txtDepartment.Enabled = false;
                    driver.Enabled = false;
                    carnum.Enabled = false;
                    txtReason.Enabled = false;
                    txt_sfd.Enabled = false;
                    txtMdd.Enabled = false;
                    txtNum.Enabled = false;
                    //txtTime1.Enabled = false;
                    //txtTime2.Enabled = false;
                    txtlicheng2.Enabled = false;
                    txtlicheng1.Enabled = false;
                    txtPhone.Enabled = false;
                    txtNote.Enabled = false;
                    //hlSelect0.Visible = false;
                    //Tab_spxx.Visible = false;
                    ddl_leixing.Enabled = false;
                    usetime1.Disabled = true;
                    usetime2.Disabled = true;
                    ydtime.Disabled = true;
                    //btnLoad.Visible = false;
                    if (Request.QueryString["diff"]=="look")
                    {
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;
                    }
                }
                else if (Request.QueryString["action"] == "mod")
                {
                    ddlleixing();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改后将重新进入审批！');", true);
                    bind_info();
                    sqrq.Text = DateTime.Now.ToString();
                    txtCode.Enabled = false;
                    Tab_spxx.Visible = false;
                    btnLoad.Visible = true;
                }
                else
                {
                    ddlleixing();
                    bind_info();
                    txtCode.Enabled = false;
                    txtDepartment.Enabled = false;
                    txtReason.Enabled = false;
                    txt_sfd.Enabled = false;
                    txtMdd.Enabled = false;
                    txtNum.Enabled = false;
                    //txtTime1.Enabled = false;
                    //txtTime2.Enabled = false;
                    txtlicheng1.Enabled = false;
                    txtlicheng2.Enabled = false;
                    txtPhone.Enabled = false;
                    txtNote.Enabled = false;

                    //hlSelect0.Visible = false;
                    ddl_leixing.Enabled = false;
                }
            }
        }
        public void bind_info()
        {
            string sqltext = "select CODE,DEPARTMENT,REASON,SFPLACE,DESTINATION,USETIME1,USETIME2,YDTIME,NUM,APPLYER,PHONE,TIME1,TIME2,NOTE,SQTIME,SHRNAME,SHRID,LICHENG1,LICHENG2,SJNAME,CARNUM,SJCALL,CANCEL from TBOM_CARAPPLY where CODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())//绑定用车信息
            {
                txtCode.Text = dr["CODE"].ToString();
                txtDepartment.Text = dr["DEPARTMENT"].ToString();
                txtReason.Text = dr["REASON"].ToString();
                txt_sfd.Text = dr["SFPLACE"].ToString();
                txtMdd.Text = dr["DESTINATION"].ToString();
                txtNum.Text = dr["NUM"].ToString();
                sqr.Text = dr["APPLYER"].ToString();
                txtPhone.Text = dr["PHONE"].ToString();
                usetime1.Value = dr["USETIME1"].ToString();
                usetime2.Value = dr["USETIME2"].ToString();
                ydtime.Value = Convert.ToDateTime(dr["YDTIME"]).ToString("yyyy-MM-dd HH:mm:ss");
                txtTime1.Value = dr["TIME1"].ToString();
                txtTime2.Value = dr["TIME2"].ToString();
                txtlicheng1.Text = dr["LICHENG1"].ToString();
                txtlicheng2.Text = dr["LICHENG2"].ToString();
                txtNote.Text = dr["NOTE"].ToString();
                sqrq.Text = dr["SQTIME"].ToString();
                ddl_leixing.SelectedValue = dr["SHRID"].ToString();
                carnum.Text = dr["CARNUM"].ToString();
                driver.Text = dr["SJNAME"].ToString() + ' ' + dr["SJCALL"].ToString();
                lblquxiao.Text = dr["CANCEL"].ToString();
            }
            dr.Close();
            string sqltext1 = "select FIRSTMANNM,FIRSTNOTE,FIRSTTIME,FIRSTMAN,SECONDMANNM,SECONDNOTE,SECONDTIME,SECONDMAN,THIRDMANNM,THIRDNOTE,THIRDTIME,THIRDMAN,STATE,AUDITLEVEL from View_TBOM_CARAPLLRVW where CODE='" + id + "'";
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqltext1);
            if (dr1.Read())
            {
                level.Text = dr1["AUDITLEVEL"].ToString();//审核等级
                txt_first.Text = dr1["FIRSTMANNM"].ToString();//第一个审核人
                first_opinion.Text = dr1["FIRSTNOTE"].ToString();//备注
                first_time.Text = dr1["FIRSTTIME"].ToString();//时间
                firstid.Value = dr1["FIRSTMAN"].ToString();//审核人CODE

                txt_second.Text = dr1["SECONDMANNM"].ToString();
                second_opinion.Text = dr1["SECONDNOTE"].ToString();
                second_time.Text = dr1["SECONDTIME"].ToString();
                secondid.Value = dr1["SECONDMAN"].ToString();

                txt_third.Text = dr1["THIRDMANNM"].ToString();
                third_opinion.Text = dr1["THIRDNOTE"].ToString();
                third_time.Text = dr1["THIRDTIME"].ToString();
                thirdid.Value = dr1["THIRDMAN"].ToString();

                lblState.Text = dr1["STATE"].ToString();//审核状态
                state = lblState.Text;
                zhuangtai.Text = dr1["STATE"].ToString();
                if (level.Text == "1")
                {
                    tr2.Visible = false;
                    tr3.Visible = false;
                }
                if (level.Text == "2")
                {
                    tr3.Visible = false;
                }
                //if (level.Text == "1")
                //{
                //    tr1.Visible = false;
                //}
                switch (state)
                {
                    case "0":
                        rblfirst.SelectedIndex = -1;
                        rblsecond.SelectedIndex = -1;
                        rblthird.SelectedIndex = -1;
                        if (Session["UserID"].ToString() == firstid.Value)
                        {
                            rblfirst.SelectedIndex = 0;
                            rblsecond.SelectedIndex = -1;
                            rblthird.SelectedIndex = -1;
                            rblfirst.Enabled = true;
                            first_opinion.Enabled = true;
                            first_time.Text = DateTime.Now.ToString();
                            btnLoad.Visible = true;
                        }
                        break;
                    case "1":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = -1;
                        rblthird.SelectedIndex = -1;
                        if (Session["UserID"].ToString() == secondid.Value)
                        {
                            rblfirst.SelectedIndex = 0;
                            rblsecond.SelectedIndex = 0;
                            rblthird.SelectedIndex = -1;
                            rblsecond.Enabled = true;

                            second_opinion.Enabled = true;
                            second_time.Text = DateTime.Now.ToString();
                            btnLoad.Visible = true;
                        }
                        break;
                    case "2":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 0;
                        rblthird.SelectedIndex = -1;
                        if (Session["UserID"].ToString() == thirdid.Value)
                        {
                            rblfirst.SelectedIndex = 0;
                            rblsecond.SelectedIndex = 0;
                            rblthird.SelectedIndex = 0;
                            rblthird.Enabled = true;
                            third_opinion.Enabled = true;
                            third_time.Text = DateTime.Now.ToString();
                            btnLoad.Visible = true;
                        }
                        break;
                    case "3"://第一个审核人不同意
                        rblfirst.SelectedValue = "3";
                        rblsecond.SelectedIndex = -1;
                        rblthird.SelectedIndex = -1;
                        //btnLoad.Visible = false;
                        break;
                    case "5"://第二个审核人不同意
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 1;
                        rblthird.SelectedIndex = -1;
                        //rblfirst.SelectedValue = "5";
                        //btnLoad.Visible = false;
                        break;
                    case "7"://第二个审核人同意
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 0;
                        rblthird.SelectedIndex = 1;
                        //rblfirst.SelectedValue = "7";
                        //btnLoad.Visible = false;
                        break;
                    case "8":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 0;
                        rblthird.SelectedIndex = 0;
                        //    txt_first.Enabled = false;
                        //    //hlSelect1.Visible = false;
                        //    rblfirst.Enabled = false;
                        //    first_opinion.Enabled = false;
                        //    txt_second.Enabled = false;
                        //    //hlSelect2.Visible = false;
                        //    rblsecond.Enabled = false;
                        //    second_opinion.Enabled = false;
                        break;
                    case "9":
                        quxiao.Visible = true;
                        //rblfirst.SelectedValue = "7";
                        //btnLoad.Visible = false;
                        break;
                    default:
                        //    rblfirst.SelectedIndex = -1;
                        //    rblsecond.SelectedIndex = -1;
                        //    txt_first.Enabled = false;
                        //    //hlSelect1.Visible = false;
                        //    txt_second.Enabled = true;
                        //    //hlSelect2.Visible = true;
                        //    rblsecond.Enabled = false;
                        //    second_opinion.Enabled = false;
                        //    first_time.Text = DateTime.Now.ToString();
                        break;
                }
            }
            dr1.Close();
        }

        //protected void rblfirst_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (rblfirst.SelectedIndex == 0)
        //    {
        //        txt_second.Enabled = true;
        //        hlSelect2.Visible = true;
        //    }
        //}
        private void code()
        {
            string code = DateTime.Today.ToString("yyyyMMdd") + '-' + 'Y';
            string sql = "select MAX(CODE) as CODE FROM TBOM_CODE WHERE CODE LIKE '%" + code + "%'";
            DataTable DT = DBCallCommon.GetDTUsingSqlText(sql);
            if (DT.Rows.Count > 0)
            {
                //string ss = DT.Rows[0]["CODE"].ToString();
                if (DT.Rows[0]["CODE"].ToString() != "")
                {
                    string num = DT.Rows[0]["CODE"].ToString().Substring(10, 3);
                    int I = Int32.Parse(num) + 1;
                    num = Convert.ToString(I);
                    if (I < 10)
                    {
                        num = "00" + num;
                    }
                    else if (I >= 10 && I < 100)
                    {
                        num = "0" + num;
                    }
                    code += num;
                }
                else
                {
                    code += "001";
                }
                string SLQTXT = "insert into TBOM_CODE (CODE) values ('" + code + "')";
                DBCallCommon.ExeSqlText(SLQTXT);
            }
            //string year1 = code.Substring(0,4).ToString();
            //string month1 = code.Substring(5,2).ToString();
            //string day1 = code.Substring(8,2).ToString();
            //string hour1 = DateTime.Now.Hour.ToString();
            //string minute1 = DateTime.Now.Minute.ToString();
            //string second1 = DateTime.Now.Second.ToString();
            //code = year1 + month1 + day1 + hour1 + minute1 + second1;

            txtCode.Text = code;

            //txtTime1.Value = DateTime.Now.ToString();
        }
        private void ddlleixing()
        {
            ddl_leixing.Items.Clear();
            string sql = "select ID,TYPE FROM TBOM_SHENHE WHERE STATE='0'";
            DBCallCommon.BindDdl(ddl_leixing, sql, "TYPE", "ID");
        }

        protected void btnLoad_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            string sqrid = Session["UserID"].ToString();
            List<string> list_sql = new List<string>();
            if (Request.QueryString["action"] == "add")
            {
                if (ddl_leixing.SelectedIndex==0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核类型！');", true);
                    return;
                }
                else
                {
                    //sqltext = "delete from TBOM_CARAPPLY where CODE='" + txtCode.Text.Trim() + "'";
                    //list_sql.Add(sqltext);
                    //int Num = Convert.ToInt16(txtNum.Text.Trim());
                    sqltext = "insert into TBOM_CARAPPLY(CODE,DEPARTMENT,REASON,SFPLACE,DESTINATION,NUM,APPLYER,APPLYERID,PHONE,TIME1,TIME2,NOTE,FANKUI,SQTIME,LICHENG1,LICHENG2,SHRID,USETIME1,USETIME2)" +
                    "Values('" + txtCode.Text.Trim() + "','" + txtDepartment.Text.Trim() + "','" + txtReason.Text.Trim() + "','"+txt_sfd.Text.Trim()+"','" + txtMdd.Text.Trim() + "','" + txtNum.Text.Trim() + "','" + sqr.Text.Trim() + "','" + sqrid.Trim() + "','" + txtPhone.Text.Trim() + "','" + txtTime1.Value.Trim() + "','" + txtTime2.Value.Trim() + "','" + txtNote.Text.Trim() + "','0','" + sqrq.Text.Trim() + "','" + txtlicheng1.Text.Trim() + "','" + txtlicheng2.Text.Trim() + "','" + ddl_leixing.SelectedValue.Trim() + "','" + usetime1.Value.Trim() + "','" + usetime2.Value.Trim() + "')";
                    list_sql.Add(sqltext);
                    sqltext = "insert into TBOM_CARALLRVW(CODE,APPLYER,SQTIME,STATE,APPLYERID,TYPE,TYPEID) values('" + txtCode.Text.Trim() + "','" + sqr.Text.Trim() + "','" + sqrq.Text.Trim() + "','0','" + sqrid.Trim() + "','" + ddl_leixing.SelectedItem.Text + "','" + ddl_leixing.SelectedValue.Trim() + "')";
                    list_sql.Add(sqltext);
                    DBCallCommon.ExecuteTrans(list_sql);
                    //邮件提醒
                    string sqladd = "select FIRSTMAN from View_TBOM_CARAPLLRVW where STATE='0'and CODE ='" + txtCode.Text.Trim() + "'";
                    DataTable dtadd = DBCallCommon.GetDTUsingSqlText(sqladd);
                    if (dtadd.Rows.Count > 0)
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(dtadd.Rows[0]["FIRSTMAN"].ToString());
                        string _body = "用车申请审批任务:"
                              + "\r\n申请单号：" + txtCode.Text.Trim()
                              + "\r\n制单人：" + sqr.Text.Trim()
                              + "\r\n制单日期：" + sqrq.Text.Trim();

                        string _subject = "您有新的【用车申请】需要审批，请及时处理:" + txtCode.Text.Trim();
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('申请成功，已进入审批流程！');window.opener=null;window.open('','_self');window.close();window.returnValue='refresh'", true);
                    return;
                }
            }
            else if (Request.QueryString["action"] == "mod")
            {
                //int Num = Convert.ToInt16(txtNum.Text.Trim());
                sqltext = "update TBOM_CARAPPLY set DEPARTMENT='" + txtDepartment.Text.Trim() + "',REASON='" + txtReason.Text.Trim() + "',SFPLACE='"+txt_sfd.Text.Trim()+"',DESTINATION='" + txtMdd.Text.Trim() + "',NUM='" + txtNum.Text.Trim() + "',APPLYER='" + sqr.Text.Trim() + "',APPLYERID='" + sqrid.Trim() + "',PHONE='" + txtPhone.Text.Trim() + "',TIME1='" + txtTime1.Value.Trim() + "',TIME2='" + txtTime2.Value.Trim() + "',NOTE='" + txtNote.Text.Trim() + "',SQTIME='" + sqrq.Text.Trim() + "',USETIME1='" + usetime1.Value.Trim() + "',USETIME2='" + usetime2.Value.Trim() + "' where CODE='" + txtCode.Text.Trim() + "'";
                list_sql.Add(sqltext);
                sqltext = "update TBOM_CARALLRVW set SQTIME='" + sqrq.Text.Trim() + "',FIRSTNOTE='',SECONDNOTE='',THIRDNOTE='',FIRSTTIME='',SECONDTIME='',THIRDTIME='',STATE=0 where CODE='" + txtCode.Text.Trim() + "'";
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                //Response.Redirect("OM_CarApply.aspx");
            }
            else if (Request.QueryString["action"] == "view")
            {
                //string 
                #region 一级审核1
                if (level.Text == "1")
                {
                    if (Session["UserID"].ToString() == firstid.Value && zhuangtai.Text == "0")
                    {
                        if (rblfirst.SelectedValue.Trim() == "3")
                        {
                            sqltext = "update TBOM_CARALLRVW SET STATE='" + rblfirst.SelectedValue.Trim() + "',FIRSTTIME='" + first_time.Text + "',FIRSTNOTE='" + first_opinion.Text + "' WHERE CODE='" + txtCode.Text.Trim() + "'";
                            list_sql.Add(sqltext);
                            //sqltext = "update TBOM_CARAPPLY SET STATE='" + rblfirst.SelectedValue.Trim() + "'";
                            //list_sql.Add(sqltext);
                        }
                        else
                        {
                            sqltext = "update TBOM_CARALLRVW SET STATE='8',FIRSTTIME='" + first_time.Text + "',FIRSTNOTE='" + first_opinion.Text + "' WHERE CODE='" + txtCode.Text.Trim() + "'";
                            list_sql.Add(sqltext);
                            //sqltext = "update TBOM_CARAPPLY SET STATE='8'";
                            //list_sql.Add(sqltext);
                        }
                    }
                }
                #endregion
                #region 二级审核2
                if (level.Text == "2")
                {
                    if (Session["UserID"].ToString() == firstid.Value && zhuangtai.Text == "0")
                    {
                        sqltext = "update TBOM_CARALLRVW SET STATE='" + rblfirst.SelectedValue.Trim() + "',FIRSTTIME='" + first_time.Text + "',FIRSTNOTE='" + first_opinion.Text + "' WHERE CODE='" + txtCode.Text.Trim() + "'";
                        list_sql.Add(sqltext);
                        //sqltext = "update TBOM_CARAPPLY SET STATE='" + rblfirst.SelectedValue.Trim() + "'";
                        //list_sql.Add(sqltext);

                    }
                    else if (Session["UserID"].ToString() == secondid.Value && zhuangtai.Text == "1")
                    {
                        if (rblsecond.SelectedValue.Trim() == "5")
                        {
                            sqltext = "update TBOM_CARALLRVW SET STATE='" + rblsecond.SelectedValue.Trim() + "',SECONDTIME='" + second_time.Text + "',SECONDNOTE='" + second_opinion.Text + "' WHERE CODE='" + txtCode.Text.Trim() + "'";
                            list_sql.Add(sqltext);
                            //sqltext = "update TBOM_CARAPPLY SET STATE='" + rblsecond.SelectedValue.Trim() + "'";
                            //list_sql.Add(sqltext);
                        }
                        else
                        {
                            sqltext = "update TBOM_CARALLRVW SET STATE='8',SECONDTIME='" + second_time.Text + "',SECONDNOTE='" + second_opinion.Text + "' WHERE CODE='" + txtCode.Text.Trim() + "'";
                            list_sql.Add(sqltext);
                            //    sqltext = "update TBOM_CARAPPLY SET STATE='8'";
                            //    list_sql.Add(sqltext);
                            //}
                        }
                    }
                }
                #endregion
                #region 三级审核3
                if (level.Text == "3")
                {
                    if (Session["UserID"].ToString() == firstid.Value && zhuangtai.Text == "0")
                    {
                        sqltext = "update TBOM_CARALLRVW SET STATE='" + rblfirst.SelectedValue.Trim() + "',FIRSTTIME='" + first_time.Text + "',FIRSTNOTE='" + first_opinion.Text + "' WHERE CODE='" + txtCode.Text.Trim() + "'";
                        list_sql.Add(sqltext);
                        //sqltext = "update TBOM_CARAPPLY SET STATE='" + rblfirst.SelectedValue.Trim() + "'";
                        //list_sql.Add(sqltext);

                    }
                    else if (Session["UserID"].ToString() == secondid.Value && zhuangtai.Text == "1")
                    {
                        sqltext = "update TBOM_CARALLRVW SET STATE='" + rblsecond.SelectedValue.Trim() + "',SECONDTIME='" + second_time.Text + "',SECONDNOTE='" + second_opinion.Text + "' WHERE CODE='" + txtCode.Text.Trim() + "'";
                        list_sql.Add(sqltext);
                        //sqltext = "update TBOM_CARAPPLY SET STATE='" + rblsecond.SelectedValue.Trim() + "'";
                        //list_sql.Add(sqltext);

                    }
                    else if (Session["UserID"].ToString() == thirdid.Value && zhuangtai.Text == "2")
                    {
                        if (rblthird.SelectedValue.Trim() == "7")
                        {
                            sqltext = "update TBOM_CARALLRVW SET STATE='" + rblthird.SelectedValue.Trim() + "',THIRDTIME='" + third_time.Text + "',THIRDNOTE='" + third_opinion.Text + "' WHERE CODE='" + txtCode.Text.Trim() + "'";
                            list_sql.Add(sqltext);
                            //sqltext = "update TBOM_CARAPPLY SET STATE='" + rblthird.SelectedValue.Trim() + "'";
                            //list_sql.Add(sqltext);
                        }
                        else
                        {
                            sqltext = "update TBOM_CARALLRVW SET STATE='8',THIRDTIME='" + third_time.Text + "',THIRDNOTE='" + third_opinion.Text + "' WHERE CODE='" + txtCode.Text.Trim() + "'";
                            list_sql.Add(sqltext);
                            //sqltext = "update TBOM_CARAPPLY SET STATE='8'";
                            //list_sql.Add(sqltext);
                        }
                    }
                }
                #endregion
                DBCallCommon.ExecuteTrans(list_sql);
                //邮件提醒
                string sql = "select STATE,SECONDMAN,THIRDMAN from View_TBOM_CARAPLLRVW where (STATE='1'or STATE='2')and CODE ='" + txtCode.Text.Trim() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    string id_emailto = "";
                    if (dt.Rows[0]["STATE"].ToString() == "1")
                        id_emailto = dt.Rows[0]["SECONDMAN"].ToString();
                    else
                        id_emailto = dt.Rows[0]["THIRDMAN"].ToString();
                    string _emailto = DBCallCommon.GetEmailAddressByUserID(id_emailto);
                    string _body = "用车申请审批任务:"
                          + "\r\n申请单号：" + txtCode.Text.Trim()
                          + "\r\n制单人：" + sqr.Text.Trim()
                          + "\r\n制单日期：" + sqrq.Text.Trim();

                    string _subject = "您有新的【用车申请】需要审批，请及时处理:" + txtCode.Text.Trim();
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核成功');window.opener=null;window.open('','_self');window.close();window.returnValue='refresh'", true);
                //return;
                Response.Redirect("OM_CarApply.aspx");

            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            Response.Redirect("OM_CarApply.aspx");
        }
    }
}
