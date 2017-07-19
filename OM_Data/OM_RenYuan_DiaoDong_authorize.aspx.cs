using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;


namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_RenYuan_DiaoDong_authorize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind_ddl();
                string action_in = Request.QueryString["action"].ToString();
                if (action_in == "add")
                {
                    creat_code();//生成申请单号
                    bind_add();//添加调动申请，绑定数据
                }
                else
                    if (action_in == "audit")
                    {
                        bind_audit();//进入审核，绑定数据
                    
                    }
                    else
                        if (action_in == "view")
                        {
                            bind_view();//查看进入，绑定数据
                        }
                authorize_rating();//进入审核等级
            }
        }

        //生成申请单号
        protected void creat_code()
        {
            string apply_code = "";
            double apply_code_d = 0;
            string sqltext = "select max(MOVE_CODE) from dbo.OM_RenYuanDiaoDong";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows[0][0].ToString() != "")
            {
                apply_code = dt.Rows[0][0].ToString();
                apply_code_d = Convert.ToDouble(apply_code);
                apply_code_d += 1;
                LAB_MOVE_CODE.Text = apply_code_d.ToString();
            }
            else
            {
                LAB_MOVE_CODE.Text = "10000000";
            }

        }

        //添加申请
        protected void bind_add()
        {

            RBL_MOVE_TYPE.SelectedIndex = 0;
            ddl_MOVE_OUTPART.SelectedIndex = 0;
            ddl_MOVE_INPART.SelectedIndex = 0;
            ddl_MOVE_WORK.SelectedIndex = 0;
            TXT_MOVE_STARTTIME.Text = "";
            TXT_MOVE_ENDTIME.Text = "";
            TXT_MOVE_REASON.Text = "";
        }

        //审核
        protected void bind_audit()
        {
            Panel1.Enabled = false;
            binddate();
            control_enable();
            string NAME = Session["UserName"].ToString();
            string datatime = DateTime.Now.ToString("yyyy-MM-dd");
            string code = Request.QueryString["CODE"].ToString();
            if (NAME == txt_first.Text.ToString())
            {
                first_time.Text = datatime.ToString();
                rblfirst.Enabled = true;
                first_opinion.Enabled = true;
            }
            if (NAME == txt_second.Text.ToString())
            {
                second_time.Text = datatime.ToString();
                rblsecond.Enabled = true;
                second_opinion.Enabled = true;
            }
            if (NAME == txt_third.Text.ToString())
            {
                third_time.Text = datatime.ToString();
                rblthird.Enabled = true;
                third_opinion.Enabled = true;
            }
            if (NAME == txt_fourth.Text.ToString())
            {
                fourth_time.Text = datatime.ToString();
                rblfourth.Enabled = true;
                fourth_opinion.Enabled = true;
            }
            if (NAME == txt_fifth.Text.ToString())
            {
                fifth_time.Text = datatime.ToString();
                rblfifth.Enabled = true;
                fifth_opinion.Enabled = true;
            }
            if (NAME == txt_sixth.Text.ToString())
            {
                sixth_time.Text = datatime.ToString();
                rblsixth.Enabled = true;
                sixth_opinion.Enabled = true;
            }
        }

        //查看
        protected void bind_view()
        {
            btnsave.Visible = false;
            Panel1.Enabled = false;
            binddate();

            Type1.Enabled = false;
            Type2.Enabled = false;
            Type3.Enabled = false;
            Type4.Enabled = false;
            Type5.Enabled = false;
            Type6.Enabled = false;
        }

        //控制是否可用
        protected void control_enable()
        {
            txt_first.Enabled = false;
            txt_second.Enabled = false;
            txt_third.Enabled = false;
            txt_fourth.Enabled = false;
            txt_fifth.Enabled = false;
            txt_sixth.Enabled = false;

            rblfirst.Enabled = false;
            rblsecond.Enabled = false;
            rblthird.Enabled = false;
            rblfourth.Enabled = false;
            rblfifth.Enabled = false;
            rblsixth.Enabled = false;

            first_opinion.Enabled = false;
            second_opinion.Enabled = false;
            third_opinion.Enabled = false;
            fourth_opinion.Enabled = false;
            fifth_opinion.Enabled = false;
            sixth_opinion.Enabled = false;


            Type1.Enabled = false;
            Type2.Enabled = false;
            Type3.Enabled = false;
            Type4.Enabled = false;
            Type5.Enabled = false;
            Type6.Enabled = false;
        }

        //审核和查看进入时绑定数据
        protected void binddate()
        {

            string code = Request.QueryString["CODE"].ToString();
            string datatime = DateTime.Now.ToString("yyyy-MM-dd");
            string sqltext = "select * from dbo.OM_RenYuanDiaoDong where MOVE_CODE='" + code + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                LAB_MOVE_CODE.Text = code;

                ListItem item = new ListItem(dt.Rows[0]["MOVE_PERNAME"].ToString(), dt.Rows[0]["MOVE_PERNAME"].ToString());
                ddlPer.Items.Add(item);
                string move_type = dt.Rows[0]["MOVE_TYPE"].ToString();
                if (dt.Rows[0]["MOVE_TYPE"].ToString() == "0")
                {
                    RBL_MOVE_TYPE.Items[0].Selected = true;
                }
                else
                    if (dt.Rows[0]["MOVE_TYPE"].ToString() == "1")
                    {
                        RBL_MOVE_TYPE.Items[1].Selected = true;
                    }
                ddl_MOVE_OUTPART.SelectedValue = dt.Rows[0]["MOVE_OUTPARTID"].ToString();
                // ddl_MOVE_OUTPART.SelectedItem.Text = dt.Rows[0]["MOVE_OUTPART"].ToString();
                //   ddl_MOVE_INPART.SelectedItem.Text = dt.Rows[0]["MOVE_INPART"].ToString();
                ddl_MOVE_INPART.SelectedValue = dt.Rows[0]["MOVE_INPARTID"].ToString();
                bind_work_ddl(dt.Rows[0]["MOVE_INPARTID"].ToString());
                ddl_MOVE_WORK.SelectedValue = dt.Rows[0]["MOVE_WORKID"].ToString();
                TXT_MOVE_STARTTIME.Text = dt.Rows[0]["MOVE_STARTTIME"].ToString();
                TXT_MOVE_ENDTIME.Text = dt.Rows[0]["MOVE_ENDTIME"].ToString();
                TXT_MOVE_REASON.Text = dt.Rows[0]["MOVE_REASON"].ToString();

                bind_gridview(dt.Rows[0]["MOVE_PERNAME"].ToString());
                ddl_authorize_rating.SelectedValue = dt.Rows[0]["MOVE_AUTH_RATING"].ToString();
                //第一个审批人信息
                txt_first.Text = dt.Rows[0]["FIRST_PER"].ToString();
                if (dt.Rows[0]["FIRST_JG"].ToString() != "" && dt.Rows[0]["FIRST_SJ"].ToString() != "")
                {
                    rblfirst.SelectedValue = dt.Rows[0]["FIRST_JG"].ToString();
                    first_time.Text = dt.Rows[0]["FIRST_SJ"].ToString();
                    first_opinion.Text = dt.Rows[0]["FIRST_YJ"].ToString();
                   
                }
                Type1.SelectedValue = dt.Rows[0]["Type1"].ToString();
                //第二个审批人信息
                txt_second.Text = dt.Rows[0]["SECOND_PER"].ToString();
                if (dt.Rows[0]["SECOND_JG"].ToString() != "" && dt.Rows[0]["SECOND_SJ"].ToString() != "")
                {
                    rblsecond.SelectedValue = dt.Rows[0]["SECOND_JG"].ToString();
                    second_time.Text = dt.Rows[0]["SECOND_SJ"].ToString();
                    second_opinion.Text = dt.Rows[0]["SECOND_YJ"].ToString();
                }

                Type2.SelectedValue = dt.Rows[0]["Type2"].ToString();  
                //第三个审批人信息
                txt_third.Text = dt.Rows[0]["THIRD_PER"].ToString();
                if (dt.Rows[0]["THIRD_JG"].ToString() != "" && dt.Rows[0]["THIRD_SJ"].ToString() != "")
                {
                    rblthird.SelectedValue = dt.Rows[0]["THIRD_JG"].ToString();
                    third_opinion.Text = dt.Rows[0]["THIRD_YJ"].ToString();
                    third_time.Text = dt.Rows[0]["THIRD_SJ"].ToString();
                   
                }
                Type3.SelectedValue = dt.Rows[0]["Type3"].ToString();
                //第四个审批人信息
                txt_fourth.Text = dt.Rows[0]["FOURTH_PER"].ToString();
                if (dt.Rows[0]["FOURTH_JG"].ToString() != "" && dt.Rows[0]["FOURTH_SJ"].ToString() != "")
                {
                    rblfourth.SelectedValue = dt.Rows[0]["FOURTH_JG"].ToString();
                    fourth_time.Text = dt.Rows[0]["FOURTH_SJ"].ToString();
                    fourth_opinion.Text = dt.Rows[0]["FOURTH_YJ"].ToString();
                   
                }
                Type4.SelectedValue = dt.Rows[0]["Type4"].ToString();
                //第五个审批人信息
                txt_fifth.Text = dt.Rows[0]["FIFTH_PER"].ToString();
                if (dt.Rows[0]["FIFTH_JG"].ToString() != "" && dt.Rows[0]["FIFTH_SJ"].ToString() != "")
                {
                    rblfifth.SelectedValue = dt.Rows[0]["FIFTH_JG"].ToString();
                    fifth_time.Text = dt.Rows[0]["FIFTH_SJ"].ToString();
                    fifth_opinion.Text = dt.Rows[0]["FIFTH_YJ"].ToString();
                    
                }
                Type5.SelectedValue = dt.Rows[0]["Type5"].ToString();
                //第六个审批人信息
                txt_sixth.Text = dt.Rows[0]["SIXTH_PER"].ToString();
                if (dt.Rows[0]["SIXTH_JG"].ToString() != "" && dt.Rows[0]["SIXTH_SJ"].ToString() != "")
                {
                    rblsixth.SelectedValue = dt.Rows[0]["SIXTH_JG"].ToString();
                    sixth_time.Text = dt.Rows[0]["SIXTH_SJ"].ToString();
                    sixth_opinion.Text = dt.Rows[0]["SIXTH_YJ"].ToString();
                    
                }
                Type6.SelectedValue = dt.Rows[0]["Type6"].ToString();
            }
            sqltext = " select * from OM_RenYuanDDTranGDZC where RyCode='" + code + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        //审核等级
        protected void authorize_rating()
        {
            pal_depart_first.Visible = false;
            pal_depart_second.Visible = false;
            pal_depart_third.Visible = false;
            pal_depart_fourth.Visible = false;
            pal_depart_fifth.Visible = false;
            pal_depart_sixth.Visible = false;
            if (ddl_authorize_rating.SelectedValue == "1")
            {
                pal_depart_first.Visible = true;
            }
            if (ddl_authorize_rating.SelectedValue == "2")
            {
                pal_depart_first.Visible = true;
                pal_depart_second.Visible = true;
            }
            if (ddl_authorize_rating.SelectedValue == "3")
            {
                pal_depart_first.Visible = true;
                pal_depart_second.Visible = true;
                pal_depart_third.Visible = true;
            }
            if (ddl_authorize_rating.SelectedValue == "4")
            {
                pal_depart_first.Visible = true;
                pal_depart_second.Visible = true;
                pal_depart_third.Visible = true;
                pal_depart_fourth.Visible = true;
            }
            if (ddl_authorize_rating.SelectedValue == "5")
            {
                pal_depart_first.Visible = true;
                pal_depart_second.Visible = true;
                pal_depart_third.Visible = true;
                pal_depart_fourth.Visible = true;
                pal_depart_fifth.Visible = true;
            }
            if (ddl_authorize_rating.SelectedValue == "6")
            {
                pal_depart_first.Visible = true;
                pal_depart_second.Visible = true;
                pal_depart_third.Visible = true;
                pal_depart_fourth.Visible = true;
                pal_depart_fifth.Visible = true;
                pal_depart_sixth.Visible = true;
            }
        }

        //保存
        protected void Btn_save_onclick(object sender, EventArgs e)
        {

            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            string action_in = Request.QueryString["action"].ToString();

            string MOVE_CODE = LAB_MOVE_CODE.Text.ToString();
            string MOVE_AUTH_RATING = ddl_authorize_rating.Text.ToString();
            string MOVE_PERNAME = ddlPer.SelectedValue;
            string MOVE_TYPE = RBL_MOVE_TYPE.SelectedValue.ToString();
            string MOVE_OUTPART = ddl_MOVE_OUTPART.SelectedItem.Text.ToString();
            string MOVE_INPART = ddl_MOVE_INPART.SelectedItem.Text.ToString();
            string MOVE_WORK = ddl_MOVE_WORK.SelectedItem.Text.ToString();
            string MOVE_STARTTIME = TXT_MOVE_STARTTIME.Text.ToString();
            string MOVE_ENDTIME = TXT_MOVE_ENDTIME.Text.ToString();
            string MOVE_REASON = TXT_MOVE_REASON.Text.ToString();

            string FIRST_PER = txt_first.Text.ToString();
            string SECOND_PER = txt_second.Text.ToString();
            string THIRD_PER = txt_third.Text.ToString();
            string FOURTH_PER = txt_fourth.Text.ToString();
            string FIFTH_PER = txt_fifth.Text.ToString();
            string SIXTH_PER = txt_sixth.Text.ToString();

            if (action_in == "add")
            {
                string[] fathername = { txt_first.Text.ToString(), txt_second.Text.ToString(), txt_third.Text.ToString(), txt_fourth.Text.ToString(), txt_fifth.Text.ToString(), txt_sixth.Text.ToString() };
                int name_void = 0;
                int name_equal = 0;
                for (int i = 0; i < ddl_authorize_rating.SelectedIndex + 1; i++)
                {
                    if (fathername[i].ToString() == "")
                    {
                        name_void += 1;
                    }
                    for (int j = 0; j < i; j++)
                    {
                        if (fathername[i].ToString() == fathername[j].ToString())
                        {
                            name_equal += 1;
                        }
                    }
                }
                if (name_void > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审批人不能为空！');", true); return;
                }
                else if (name_equal > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审批人不能为相同！');", true); return;
                }
                else
                {
                    string sql_add = "insert into OM_RenYuanDiaoDong (MOVE_CODE, MOVE_TYPE, MOVE_PERNAME, MOVE_INPART, MOVE_WORK, MOVE_OUTPART, MOVE_STARTTIME, MOVE_ENDTIME, MOVE_REASON, MOVE_AUTH_RATING, FIRST_PER, SECOND_PER,THIRD_PER,FOURTH_PER,FIFTH_PER,SIXTH_PER,MOVE_STATE, MOVE_INPARTID, MOVE_OUTPARTID, MOVE_WORKID,Type1, Type2, Type3, Type4, Type5, Type6) values ('" + MOVE_CODE + "','" + MOVE_TYPE + "','" + MOVE_PERNAME + "','" + MOVE_INPART + "','" + MOVE_WORK + "','" + MOVE_OUTPART + "','" + MOVE_STARTTIME + "','" + MOVE_ENDTIME + "','" + MOVE_REASON + "','" + MOVE_AUTH_RATING + "','" + FIRST_PER + "','" + SECOND_PER + "','" + THIRD_PER + "','" + FOURTH_PER + "','" + FIFTH_PER + "','" + SIXTH_PER + "','0','" + ddl_MOVE_INPART.SelectedValue + "','" + ddl_MOVE_OUTPART.SelectedValue + "','" + ddl_MOVE_WORK.SelectedValue + "','" + Type1.SelectedValue + "','" + Type2.SelectedValue + "','" + Type3.SelectedValue + "','" + Type4.SelectedValue + "','" + Type5.SelectedValue + "','" + Type6.SelectedValue + "')";
                    list.Add(sql_add);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sptitle = "人员调动审批";
                    spcontent = ddlPer.SelectedValue.Trim() + "的人员调动需要您审批，请登录查看！";
                    string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_first.Text.Trim() + "'";
                    System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }


                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    string GDZC_CODE = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_CODE")).Value;
                    string GDZC_NAME = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_NAME")).Value;
                    string GDZC_MODEL = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_MODEL")).Value;
                    string GDZC_BIANHAO = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_BIANHAO")).Value;
                    string ddlIsZY = ((DropDownList)GridView1.Rows[i].FindControl("ddlIsZY")).SelectedValue;
                    string JJR = ((TextBox)GridView1.Rows[i].FindControl("JJR")).Text;
                    string hidPer = ((HtmlInputHidden)GridView1.Rows[i].FindControl("hidPer")).Value;
                    string GDZC_PLACE = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_PLACE")).Value;
                    if (hidPer==""&&ddlIsZY=="转移")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请重新选择固定资产交接人！');", true); return;
                    }
                    string sql = "insert into OM_RenYuanDDTranGDZC (RyCode, INCODE, NAME, MODEL, ISTRAN, JERID, JERNM,BIANHAO,PLACE) values ('" + MOVE_CODE + "','" + GDZC_CODE + "','" + GDZC_NAME + "','" + GDZC_MODEL + "','" + ddlIsZY + "','" + hidPer + "','" + JJR + "','" + GDZC_BIANHAO + "','" + GDZC_PLACE + "')";
                    list.Add(sql);
                }
                DBCallCommon.ExecuteTrans(list);
                Response.Redirect("OM_RenYuanDiaoDongMain.aspx");


            }
            if (action_in == "audit")
            {
                string NAME = Session["UserName"].ToString();
                string sql_audit = "update OM_RenYuanDiaoDong set ";
                string MOVE_STATE = "0";
                string JG = "选择";
                if (NAME == txt_first.Text.ToString())
                {
                    if (rblfirst.SelectedValue.ToString() == "Y")
                    {
                        MOVE_STATE = "1";
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "人员调动审批";
                        spcontent = ddlPer.SelectedValue.Trim() + "的人员调动需要您审批，请登录查看！";
                        string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_second.Text.Trim() + "'";
                        System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                        if (dtgetsprid.Rows.Count > 0)
                        {
                            sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                    }
                    else if (rblfirst.SelectedValue.ToString() == "N")
                    {
                        MOVE_STATE = "9";
                    }
                    else
                    {
                        JG = "未选择";
                    }
                    sql_audit += " FIRST_PER='" + txt_first.Text.ToString() + "',FIRST_JG='" + rblfirst.SelectedValue.ToString() + "',FIRST_SJ='" + first_time.Text.ToString() + "',FIRST_YJ='" + first_opinion.Text.ToString() + "',MOVE_STATE='" + MOVE_STATE + "'";
                }
                if (NAME == txt_second.Text.ToString())
                {
                    if (rblsecond.SelectedValue.ToString() == "Y")
                    {
                        MOVE_STATE = "2";
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "人员调动审批";
                        spcontent = ddlPer.SelectedValue.Trim() + "的人员调动需要您审批，请登录查看！";
                        string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_third.Text.Trim() + "'";
                        System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                        if (dtgetsprid.Rows.Count > 0)
                        {
                            sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                    }
                    else if (rblsecond.SelectedValue.ToString() == "N")
                    {
                        MOVE_STATE = "9";
                    }
                    else
                    {
                        JG = "未选择";
                    }
                    sql_audit += " SECOND_PER='" + txt_second.Text.ToString() + "',SECOND_JG='" + rblsecond.SelectedValue.ToString() + "',SECOND_SJ='" + second_time.Text.ToString() + "',SECOND_YJ='" + second_opinion.Text.ToString() + "',MOVE_STATE='" + MOVE_STATE + "'";
                }
                if (NAME == txt_third.Text.ToString())
                {
                    if (rblthird.SelectedValue.ToString() == "Y")
                    {
                        MOVE_STATE = "3";
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "人员调动审批";
                        spcontent = ddlPer.SelectedValue.Trim() + "的人员调动需要您审批，请登录查看！";
                        string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_fourth.Text.Trim() + "'";
                        System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                        if (dtgetsprid.Rows.Count > 0)
                        {
                            sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                    }
                    else if (rblthird.SelectedValue.ToString() == "N")
                    {
                        MOVE_STATE = "9";
                    }
                    else
                    {
                        JG = "未选择";
                    }
                    sql_audit += " THIRD_PER='" + txt_third.Text.ToString() + "',THIRD_JG='" + rblthird.SelectedValue.ToString() + "',THIRD_SJ='" + third_time.Text.ToString() + "',THIRD_YJ='" + third_opinion.Text.ToString() + "',MOVE_STATE='" + MOVE_STATE + "'";
                }
                if (NAME == txt_fourth.Text.ToString())
                {
                    if (rblfourth.SelectedValue.ToString() == "Y")
                    {
                        MOVE_STATE = "4";
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "人员调动审批";
                        spcontent = ddlPer.SelectedValue.Trim() + "的人员调动需要您审批，请登录查看！";
                        string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_fifth.Text.Trim() + "'";
                        System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                        if (dtgetsprid.Rows.Count > 0)
                        {
                            sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                    }
                    else if (rblfourth.SelectedValue.ToString() == "N")
                    {
                        MOVE_STATE = "9";
                    }
                    else
                    {
                        JG = "未选择";
                    }
                    sql_audit += " FOURTH_PER='" + txt_fourth.Text.ToString() + "',FOURTH_JG='" + rblfourth.SelectedValue.ToString() + "',FOURTH_SJ='" + fourth_time.Text.ToString() + "',FOURTH_YJ='" + fourth_opinion.Text.ToString() + "',MOVE_STATE='" + MOVE_STATE + "'";
                }
                if (NAME == txt_fifth.Text.ToString())
                {
                    if (rblfifth.SelectedValue.ToString() == "Y")
                    {
                        MOVE_STATE = "5";
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "人员调动审批";
                        spcontent = ddlPer.SelectedValue.Trim() + "的人员调动需要您审批，请登录查看！";
                        string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_sixth.Text.Trim() + "'";
                        System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                        if (dtgetsprid.Rows.Count > 0)
                        {
                            sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                    }
                    else if (rblfifth.SelectedValue.ToString() == "N")
                    {
                        MOVE_STATE = "9";
                    }
                    else
                    {
                        JG = "未选择";
                    }
                    sql_audit += " FIFTH_PER='" + txt_fifth.Text.ToString() + "',FIFTH_JG='" + rblfifth.SelectedValue.ToString() + "',FIFTH_SJ='" + fifth_time.Text.ToString() + "',FIFTH_YJ='" + fifth_opinion.Text.ToString() + "',MOVE_STATE='" + MOVE_STATE + "'";
                }
                if (NAME == txt_sixth.Text.ToString())
                {
                    if (rblsixth.SelectedValue.ToString() == "Y")
                    {
                        MOVE_STATE = "6";
                    }
                    else if (rblsixth.SelectedValue.ToString() == "N")
                    {
                        MOVE_STATE = "9";
                    }
                    else
                    {
                        JG = "未选择";
                    }
                    sql_audit += " SIXTH_PER='" + txt_sixth.Text.ToString() + "',SIXTH_JG='" + rblsixth.SelectedValue.ToString() + "',SIXTH_SJ='" + sixth_time.Text.ToString() + "',SIXTH_YJ='" + sixth_opinion.Text.ToString() + "',MOVE_STATE='" + MOVE_STATE + "'";
                }
                sql_audit += " where MOVE_CODE='" + MOVE_CODE.ToString() + "'";
                if (JG == "未选择")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择【同意】或【不同意】！');", true); return;
                }
                else
                {
                    DBCallCommon.ExeSqlText(sql_audit);
                    if (ddl_authorize_rating.SelectedValue.ToString() == MOVE_STATE)
                    {
                        List<string> listResult = new List<string>();
                        string sql_update_dep = "update TBDS_STAFFINFO set ST_DEPID='" + ddl_MOVE_INPART.SelectedValue.ToString() + "',ST_POSITION='" + ddl_MOVE_WORK.SelectedValue + "' where ST_NAME='" + ddlPer.SelectedValue + "'";
                        listResult.Add(sql_update_dep);

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {

                            string GDZC_CODE = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_CODE")).Value;
                            string GDZC_NAME = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_NAME")).Value;
                            string GDZC_MODEL = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_MODEL")).Value;
                            string ddlIsZY = ((DropDownList)GridView1.Rows[i].FindControl("ddlIsZY")).SelectedValue;
                            string JJR = ((TextBox)GridView1.Rows[i].FindControl("JJR")).Text;
                            string hidPer = ((HtmlInputHidden)GridView1.Rows[i].FindControl("hidPer")).Value;
                            string GDZC_BIANHAO = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_BIANHAO")).Value;
                            string GDZC_PLACE = ((HtmlInputText)GridView1.Rows[i].FindControl("GDZC_PLACE")).Value;
                            if (ddlIsZY == "转移")
                            {
                                string sql = "update TBOM_GDZCIN SET SYR='" + JJR + "',SYRID='" + hidPer + "',SYBUMEN='" + ddl_MOVE_INPART.SelectedItem.Text + "',SYBUMENID='" + ddl_MOVE_INPART.SelectedValue + "',SYDATE='" + TXT_MOVE_STARTTIME.Text + "',PLACE='" + GDZC_PLACE + "' WHERE BIANHAO='" + GDZC_BIANHAO + "'";
                                listResult.Add(sql);
                            }


                        }
                        DBCallCommon.ExecuteTrans(listResult);
                        Response.Redirect("OM_RenYuanDiaoDongMain.aspx");
                    }
                }
            }
        }

        //关闭
        protected void close(object sender, EventArgs e)
        {
            Response.Redirect("OM_RenYuanDiaoDongMain.aspx");
        }

        //审核等级选择
        protected void ddl_authorize_rating_selectchanged(object sender, EventArgs e)
        {
            pal_depart_first.Visible = true;
            pal_depart_second.Visible = true;
            pal_depart_third.Visible = true;
            pal_depart_fourth.Visible = true;
            pal_depart_fifth.Visible = true;
            pal_depart_sixth.Visible = true;
            string ShenHeDengJi = ddl_authorize_rating.SelectedValue.ToString();
            if (ShenHeDengJi == "1")
            {
                pal_depart_second.Visible = false;
                pal_depart_third.Visible = false;
                pal_depart_fourth.Visible = false;
                pal_depart_fifth.Visible = false;
                pal_depart_sixth.Visible = false;
            }
            else
                if (ShenHeDengJi == "2")
                {
                    pal_depart_third.Visible = false;
                    pal_depart_fourth.Visible = false;
                    pal_depart_fifth.Visible = false;
                    pal_depart_sixth.Visible = false;
                }
                else
                    if (ShenHeDengJi == "3")
                    {
                        pal_depart_fourth.Visible = false;
                        pal_depart_fifth.Visible = false;
                        pal_depart_sixth.Visible = false;
                    }
                    else
                        if (ShenHeDengJi == "4")
                        {
                            pal_depart_fifth.Visible = false;
                            pal_depart_sixth.Visible = false;
                        }
                        else
                            if (ShenHeDengJi == "5")
                            {
                                pal_depart_sixth.Visible = false;
                            }
        }

        //调出部门选择
        protected void ddl_OUTpartselectchanged(object sender, EventArgs e)
        {

            string sql = "select ST_NAME,ST_NAME from TBDS_STAFFINFO where ST_DEPID='" + ddl_MOVE_OUTPART.SelectedValue + "' and  ST_PD='0' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                ddlPer.DataSource = dt;
                ddlPer.DataTextField = "ST_NAME";
                ddlPer.DataValueField = "ST_NAME";
                ddlPer.DataBind();
                bind_gridview(ddlPer.SelectedValue);
            }
        }



        //调出人选择
        protected void ddlPer_selectchanged(object sender, EventArgs e)
        {
            bind_gridview(ddlPer.SelectedValue);
        }

        //调入部门选择
        protected void ddl_INpartselectchanged(object sender, EventArgs e)
        {
            if (ddl_MOVE_INPART.SelectedIndex != 0)
            {
                bind_work_ddl(ddl_MOVE_INPART.SelectedValue);
            }
        }

        //绑定工作岗位
        protected void bind_work_ddl(string ddl_work)
        {
            string sqlText = "select DEP_CODE,DEP_NAME from  TBDS_DEPINFO  where DEP_CODE like '"+ddl_work+"%' and DEP_CODE like '[0-9][0-9][0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddl_MOVE_WORK.DataSource = dt;
            ddl_MOVE_WORK.DataTextField = "DEP_NAME";
            ddl_MOVE_WORK.DataValueField = "DEP_CODE";
            ddl_MOVE_WORK.DataBind();
            ListItem item = new ListItem();
            item.Text = "-请选择-";
            item.Value = "00";
            ddl_MOVE_WORK.Items.Insert(0, item);
            ddl_MOVE_WORK.SelectedValue = "00";
        }

        //绑定资产
        protected void bind_gridview(string SYR)
        {
            string sqltxt = "select INCODE,NAME,MODEL,'转移' as ISTRAN,'' as JERNM,'' as JERID,BIANHAO,PLACE  from TBOM_GDZCIN WHERE SYR='" + SYR.ToString() + "'";
            DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltxt);
            GridView1.DataSource = DT;
            GridView1.DataBind();
        }

        //绑定调入部门和调出部门
        protected void bind_ddl()
        {
            string sqltext_dep = "select distinct DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DBCallCommon.FillDroplist(ddl_MOVE_OUTPART, sqltext_dep);
            DBCallCommon.FillDroplist(ddl_MOVE_INPART, sqltext_dep);
        }
    }
}
