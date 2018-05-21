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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CARWXSQ_Detail : System.Web.UI.Page
    {
        string id = "";
        string state = "";
        int count = 0;
        string date1;
        string txtCode1;
        string CARNAME1;
        string CARID1;
        string APPLYER1;
        string PLACE1;
        string PLACEDATE1;
        string txt_contents1;
        string moneyall1;
        string managerview1;
        string controllerview1;
        string repair1;
        string note1;
        string BYB;
        string BYA;
        string BYY;
        string BYZ;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
                danhao.Text = id.ToString();
            }
            if (!IsPostBack)
            {

                if (Request.QueryString["action"] == "add")
                {
                    Tab_spxx.Visible = false;
                    InitRepeaterAdd();
                }
                else if (Request.QueryString["action"] == "view")
                {
                    bind_info();
                    InitRepeaterView();
                    btndelete.Visible = false;
                    btninsert.Visible = false;
                    if (Session["UserID"].ToString() == "207" && state == "0")
                    {
                        rblfirst.Enabled = true;
                        first_opinion.Enabled = true;
                    }
                    if (Session["UserID"].ToString() == "150" && state == "2")
                    {
                        rblsecond.Enabled = true;
                        second_opinion.Enabled = true;
                    }
                }
                else if (Request.QueryString["action"] == "mod")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改后将重新进入审批！');", true);
                    bind_info();
                    InitRepeaterView();
                    Tab_spxx.Visible = false;
                }
                else if (Request.QueryString["action"] == "modwx")
                {
                    jl_bind_info();
                    jl_InitRepeaterView();
                    Tab_spxx.Visible = false;
                }
                else if (Request.QueryString["action"] == "modby")
                {
                    jl_bind_info();
                    jl_InitRepeaterView();
                    Tab_spxx.Visible = false;
                }
                else if (Request.QueryString["action"] == "jlwx")
                {
                    Tab_spxx.Visible = false;
                    bind_info();
                    InitRepeaterView();
                }
                else if (Request.QueryString["action"] == "jlby")
                {
                    Tab_spxx.Visible = false;
                    bind_info();
                    InitRepeaterView();
                }

            }
            if (IsPostBack)
            {
                if (Request.QueryString["action"] == "mod")
                {
                    bind_info();
                }
            }
        }
        private void bind_info()
        {
            string sqltext = "select CODE,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,MONEYALL,MANAGERNAME,MANAGERID,STATE,CONTROLLERNAME,CONTROLLERID,CARNAME,CARID,NOTE,REPAIR,CONTROLLERVIEW,MANAGERVIEW,CONTENTS,BYBEFORE,BYAFTER,BYYJ,BYZQ from TBOM_CARWXSQ where CODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {

                date1 = dr["DATE"].ToString();
                txtCode1 = dr["CODE"].ToString();
                CARNAME1 = dr["CARNAME"].ToString();
                CARID1 = dr["CARID"].ToString();
                APPLYER1 = dr["APPLYNAME"].ToString();
                PLACE1 = dr["PLACE"].ToString();
                PLACEDATE1 = dr["PLACEDATE"].ToString();
                txt_contents1 = dr["CONTENTS"].ToString();
                moneyall1 = dr["MONEYALL"].ToString();
                managerview1 = dr["MANAGERVIEW"].ToString();
                controllerview1 = dr["CONTROLLERVIEW"].ToString();
                repair1 = dr["REPAIR"].ToString();
                note1 = dr["NOTE"].ToString();
                state = dr["STATE"].ToString();
                zhaungtai.Text = dr["STATE"].ToString();
                txt_first.Text = dr["MANAGERNAME"].ToString();
                txt_second.Text = dr["CONTROLLERNAME"].ToString();
                BYA = dr["BYAFTER"].ToString();
                BYB = dr["BYBEFORE"].ToString();
                BYY = dr["BYYJ"].ToString();
                BYZ = dr["BYZQ"].ToString();
                if (state == "1")
                {
                    rblfirst.SelectedValue = "1";
                }
                if (state == "2")
                {
                    rblfirst.SelectedValue = "2";
                }
                if (state == "3")
                {
                    rblfirst.SelectedValue = "2";
                    rblsecond.SelectedValue = "3";
                }
                if (state == "4")
                {
                    rblfirst.SelectedValue = "2";
                    rblsecond.SelectedValue = "4";
                }
            }
            dr.Close();
        }
        private void jl_bind_info()
        {
            string sqltext = "select CODE,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,MONEYALL,CARNAME,CARID,NOTE,REPAIR,CONTENTS,BYBEFORE,BYAFTER,BYYJ,BYZQ from TBOM_CARSAFE where CODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {

                date1 = dr["DATE"].ToString();
                txtCode1 = dr["CODE"].ToString();
                CARNAME1 = dr["CARNAME"].ToString();
                CARID1 = dr["CARID"].ToString();
                APPLYER1 = dr["APPLYNAME"].ToString();
                PLACE1 = dr["PLACE"].ToString();
                PLACEDATE1 = dr["PLACEDATE"].ToString();
                txt_contents1 = dr["CONTENTS"].ToString();
                moneyall1 = dr["MONEYALL"].ToString();
                //managerview1 = dr["MANAGERVIEW"].ToString();
                //controllerview1 = dr["CONTROLLERVIEW"].ToString();
                repair1 = dr["REPAIR"].ToString();
                note1 = dr["NOTE"].ToString();
                //state = dr["STATE"].ToString();
                //zhaungtai.Text = dr["STATE"].ToString();
                //txt_first.Text = dr["MANAGERNAME"].ToString();
                //txt_second.Text = dr["CONTROLLERNAME"].ToString();
                BYA = dr["BYAFTER"].ToString();
                BYB = dr["BYBEFORE"].ToString();
                BYY = dr["BYYJ"].ToString();
                BYZ = dr["BYZQ"].ToString();
            }
            dr.Close();
        }
        private string code()
        {
            string code = DateTime.Today.ToString("yyyyMMdd") + '-' + 'W' + 'B';
            string sql = "select MAX(CODE) as CODE FROM TBOM_CODE WHERE CODE LIKE '%" + code + "%'";
            DataTable DT = DBCallCommon.GetDTUsingSqlText(sql);
            if (DT.Rows.Count > 0)
            {
                //string ss = DT.Rows[0]["CODE"].ToString();
                if (DT.Rows[0]["CODE"].ToString() != "")
                {
                    string num = DT.Rows[0]["CODE"].ToString().Substring(11, 3);
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
                string SLQTXT = "insert into TBOM_CODE (CODE) values('" + code + "')";
                DBCallCommon.ExeSqlText(SLQTXT);
            }
            return code;
        }
        private void GetDDlcar(DropDownList ddlobject, string VALUE)
        {
            string datatext;
            string datavalue;
            ddlobject.Items.Clear();
            string sqltext = "select CARNUM+'//'+CARTYPE as CARTYPE,CARNUM from TBOM_CARINFO";
            datatext = "CARTYPE";
            datavalue = "CARNUM";
            DBCallCommon.BindDdl(ddlobject, sqltext, datatext, datavalue);
            ddlobject.SelectedValue = VALUE;
        }
        protected void ddlcar_change(object sender, EventArgs e)
        {
            DropDownList ddlcar = (DropDownList)Repeater1.Controls[0].FindControl("CARID");
            ((TextBox)Repeater1.Controls[0].FindControl("CARNAME")).Text = ddlcar.SelectedItem.Text.Trim().Substring(9).ToString();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (Request.QueryString["action"] == "add")
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    if (Request.QueryString["type"] == "wx")
                    {
                        ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆维修申请";
                        ((Label)e.Item.FindControl("title")).Text = "具体维修计划";
                        ((Label)e.Item.FindControl("lbl_place")).Text = "维修地点";
                        ((Label)e.Item.FindControl("lbl_time")).Text = "维修时间";
                        ((Label)e.Item.FindControl("lbl_bybefore")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_byzq")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_byafter")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_byyj")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_before")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_byzq")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_after")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_byyj")).Visible = false;

                    }
                    else if (Request.QueryString["type"] == "by")
                    {
                        ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆保养申请";
                        ((Label)e.Item.FindControl("title")).Text = "具体保养计划";
                        ((Label)e.Item.FindControl("lbl_place")).Text = "保养地点";
                        ((Label)e.Item.FindControl("lbl_time")).Text = "保养时间";
                    }

                    ((TextBox)Repeater1.Controls[0].FindControl("txtCode")).Text = code();
                    ((HtmlInputText)Repeater1.Controls[0].FindControl("date")).Value = DateTime.Now.ToString();
                    ((TextBox)Repeater1.Controls[0].FindControl("APPLYER")).Text = Session["UserName"].ToString();
                    DropDownList ddlcar = (DropDownList)e.Item.FindControl("CARID");
                    GetDDlcar(ddlcar, ddlcar.SelectedValue.ToString());

                }
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                }
                else if (e.Item.ItemType == ListItemType.Footer)
                {
                    ((TextBox)Repeater1.Controls[Repeater1.Controls.Count - 1].FindControl("apply")).Text = Session["UserName"].ToString();
                }
            }
            else if (Request.QueryString["action"] == "view")
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    ((TextBox)e.Item.FindControl("txtCode")).Text = id;
                    ((TextBox)e.Item.FindControl("CARNAME")).Text = CARNAME1;

                    if (Request.QueryString["type"] == "wx")
                    {
                        ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆维修申请";
                        ((Label)e.Item.FindControl("title")).Text = "具体维修计划";
                        ((Label)e.Item.FindControl("lbl_place")).Text = "维修地点";
                        ((Label)e.Item.FindControl("lbl_time")).Text = "维修时间";
                        ((Label)e.Item.FindControl("lbl_bybefore")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_byzq")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_byafter")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_byyj")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_before")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_byzq")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_after")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_byyj")).Visible = false;
                    }
                    else if (Request.QueryString["type"] == "by")
                    {
                        ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆保养申请";
                        ((Label)e.Item.FindControl("title")).Text = "具体保养计划";
                        ((Label)e.Item.FindControl("lbl_place")).Text = "保养地点";
                        ((Label)e.Item.FindControl("lbl_time")).Text = "保养时间";
                    }
                    DropDownList ddlcar = (DropDownList)e.Item.FindControl("CARID");
                    GetDDlcar(ddlcar, CARID1);
                    ((DropDownList)e.Item.FindControl("CARID")).SelectedValue = CARID1;


                    ((TextBox)e.Item.FindControl("APPLYER")).Text = APPLYER1;
                    ((TextBox)e.Item.FindControl("PLACE")).Text = PLACE1;
                    ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Value = PLACEDATE1;
                    ((HtmlInputText)e.Item.FindControl("date")).Value = date1;
                    ((TextBox)e.Item.FindControl("txt_contents")).Text = txt_contents1;
                    ((TextBox)e.Item.FindControl("txt_before")).Text = BYB;
                    ((TextBox)e.Item.FindControl("txt_byzq")).Text = BYZ;
                    ((TextBox)e.Item.FindControl("txt_after")).Text = BYA;
                    ((TextBox)e.Item.FindControl("txt_byyj")).Text = BYY;

                    ((TextBox)e.Item.FindControl("txtCode")).Enabled = false;
                    ((TextBox)e.Item.FindControl("CARNAME")).Enabled = false;
                    ((DropDownList)e.Item.FindControl("CARID")).Enabled = false;
                    ((TextBox)e.Item.FindControl("APPLYER")).Enabled = false;
                    ((TextBox)e.Item.FindControl("PLACE")).Enabled = false;
                    ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("date")).Disabled = true;
                    ((TextBox)e.Item.FindControl("txt_contents")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txt_before")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txt_byzq")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txt_after")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txt_byyj")).Enabled = false;
                }

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (Request.QueryString["action"] == "view")
                    {
                        ((CheckBox)e.Item.FindControl("cbchecked")).Visible = false;
                        ((TextBox)e.Item.FindControl("goodsname")).Enabled = true;
                        ((HtmlInputText)e.Item.FindControl("goodscount")).Disabled = true;
                        ((TextBox)e.Item.FindControl("goodsunit")).Enabled = true;
                        ((TextBox)e.Item.FindControl("goodsprice")).Enabled = true;
                        ((HtmlInputText)e.Item.FindControl("moneyone")).Disabled = true;
                    }
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    ((TextBox)e.Item.FindControl("managerview")).Text = managerview1;
                    ((TextBox)e.Item.FindControl("controllerview")).Text = controllerview1;
                    ((TextBox)e.Item.FindControl("repair")).Text = repair1;
                    ((TextBox)e.Item.FindControl("apply")).Text = APPLYER1;
                    ((HtmlInputText)e.Item.FindControl("moneyall")).Value = moneyall1;

                    ((TextBox)e.Item.FindControl("managerview")).Enabled = false;
                    ((TextBox)e.Item.FindControl("controllerview")).Enabled = false;
                    ((TextBox)e.Item.FindControl("repair")).Enabled = false;
                    ((TextBox)e.Item.FindControl("apply")).Enabled = false;
                    ((HtmlInputText)e.Item.FindControl("moneyall")).Disabled = true;
                }
            }
            else if (Request.QueryString["action"] == "jlwx")
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    ((TextBox)e.Item.FindControl("txtCode")).Text = id;
                    ((TextBox)e.Item.FindControl("CARNAME")).Text = CARNAME1;
                    ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆维修记录";
                    ((Label)e.Item.FindControl("title")).Text = "具体维修计划";
                    ((Label)e.Item.FindControl("lbl_place")).Text = "维修地点";
                    ((Label)e.Item.FindControl("lbl_time")).Text = "维修时间";
                    ((Label)e.Item.FindControl("lbl_bybefore")).Visible = false;
                    ((Label)e.Item.FindControl("lbl_byzq")).Visible = false;
                    ((Label)e.Item.FindControl("lbl_byafter")).Visible = false;
                    ((Label)e.Item.FindControl("lbl_byyj")).Visible = false;
                    ((TextBox)e.Item.FindControl("txt_before")).Visible = false;
                    ((TextBox)e.Item.FindControl("txt_byzq")).Visible = false;
                    ((TextBox)e.Item.FindControl("txt_after")).Visible = false;
                    ((TextBox)e.Item.FindControl("txt_byyj")).Visible = false;
                    DropDownList ddlcar = (DropDownList)e.Item.FindControl("CARID");
                    GetDDlcar(ddlcar, CARID1);
                    ((DropDownList)e.Item.FindControl("CARID")).SelectedValue = CARID1;


                    ((TextBox)e.Item.FindControl("APPLYER")).Text = APPLYER1;
                    ((TextBox)e.Item.FindControl("PLACE")).Text = PLACE1;
                    ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Value = PLACEDATE1;
                    ((HtmlInputText)e.Item.FindControl("date")).Value = date1;
                    ((TextBox)e.Item.FindControl("txt_contents")).Text = txt_contents1;

                    ((TextBox)e.Item.FindControl("txtCode")).Enabled = false;
                }

                else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                }
                else if (e.Item.ItemType == ListItemType.Footer)
                {
                    ((TextBox)e.Item.FindControl("managerview")).Visible = false;
                    ((TextBox)e.Item.FindControl("controllerview")).Visible = false;
                    ((TextBox)e.Item.FindControl("repair")).Visible = false;
                    ((TextBox)e.Item.FindControl("apply")).Visible = false;
                    ((HtmlInputText)e.Item.FindControl("moneyall")).Value = moneyall1;
                    //((HtmlInputText)e.Item.FindControl("moneyall")).Disabled = true;
                }
            }
            else if (Request.QueryString["action"] == "jlby")
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    ((TextBox)e.Item.FindControl("txtCode")).Text = id;
                    ((TextBox)e.Item.FindControl("CARNAME")).Text = CARNAME1;
                    ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆保养记录";
                    ((Label)e.Item.FindControl("title")).Text = "具体保养计划";
                    ((Label)e.Item.FindControl("lbl_place")).Text = "保养地点";
                    ((Label)e.Item.FindControl("lbl_time")).Text = "保养时间";
                    DropDownList ddlcar = (DropDownList)e.Item.FindControl("CARID");
                    GetDDlcar(ddlcar, CARID1);
                    ((DropDownList)e.Item.FindControl("CARID")).SelectedValue = CARID1;
                    ((TextBox)e.Item.FindControl("txt_before")).Text = BYB;
                    ((TextBox)e.Item.FindControl("txt_byzq")).Text = BYZ;
                    ((TextBox)e.Item.FindControl("txt_after")).Text = BYA;
                    ((TextBox)e.Item.FindControl("txt_byyj")).Text = BYY;

                    ((TextBox)e.Item.FindControl("APPLYER")).Text = APPLYER1;
                    ((TextBox)e.Item.FindControl("PLACE")).Text = PLACE1;
                    ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Value = PLACEDATE1;
                    ((HtmlInputText)e.Item.FindControl("date")).Value = date1;
                    ((TextBox)e.Item.FindControl("txt_contents")).Text = txt_contents1;

                    ((TextBox)e.Item.FindControl("txtCode")).Enabled = false;
                }

                else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                }
                else if (e.Item.ItemType == ListItemType.Footer)
                {
                    ((TextBox)e.Item.FindControl("managerview")).Visible = false;
                    ((TextBox)e.Item.FindControl("controllerview")).Visible = false;
                    ((TextBox)e.Item.FindControl("repair")).Visible = false;
                    ((TextBox)e.Item.FindControl("apply")).Visible = false;
                    ((HtmlInputText)e.Item.FindControl("moneyall")).Value = moneyall1;
                    //((HtmlInputText)e.Item.FindControl("moneyall")).Disabled = true;
                }
            }

            else if (Request.QueryString["action"] == "modwx")
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    ((TextBox)e.Item.FindControl("txtCode")).Text = id;
                    ((TextBox)e.Item.FindControl("CARNAME")).Text = CARNAME1;
                    ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆维修记录";
                    ((Label)e.Item.FindControl("title")).Text = "具体维修计划";
                    ((Label)e.Item.FindControl("lbl_place")).Text = "维修地点";
                    ((Label)e.Item.FindControl("lbl_time")).Text = "维修时间";
                    DropDownList ddlcar = (DropDownList)e.Item.FindControl("CARID");
                    GetDDlcar(ddlcar, CARID1);
                    ((DropDownList)e.Item.FindControl("CARID")).SelectedValue = CARID1;
                    ((TextBox)e.Item.FindControl("txt_before")).Visible = false;
                    ((TextBox)e.Item.FindControl("txt_byzq")).Visible = false;
                    ((TextBox)e.Item.FindControl("txt_after")).Visible = false;
                    ((TextBox)e.Item.FindControl("txt_byyj")).Visible = false;
                    ((Label)e.Item.FindControl("lbl_bybefore")).Visible = false;
                    ((Label)e.Item.FindControl("lbl_byzq")).Visible = false;
                    ((Label)e.Item.FindControl("lbl_byafter")).Visible = false;
                    ((Label)e.Item.FindControl("lbl_byyj")).Visible = false;

                    ((TextBox)e.Item.FindControl("APPLYER")).Text = APPLYER1;
                    ((TextBox)e.Item.FindControl("PLACE")).Text = PLACE1;
                    ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Value = PLACEDATE1;
                    ((HtmlInputText)e.Item.FindControl("date")).Value = date1;
                    ((TextBox)e.Item.FindControl("txt_contents")).Text = txt_contents1;

                    ((TextBox)e.Item.FindControl("txtCode")).Enabled = false;
                }

                else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                }
                else if (e.Item.ItemType == ListItemType.Footer)
                {
                    ((TextBox)e.Item.FindControl("managerview")).Visible = false;
                    ((TextBox)e.Item.FindControl("controllerview")).Visible = false;
                    ((TextBox)e.Item.FindControl("repair")).Visible = false;
                    ((TextBox)e.Item.FindControl("apply")).Visible = false;
                    ((HtmlInputText)e.Item.FindControl("moneyall")).Value = moneyall1;
                }
            }
            else if (Request.QueryString["action"] == "modby")
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    ((TextBox)e.Item.FindControl("txtCode")).Text = id;
                    ((TextBox)e.Item.FindControl("CARNAME")).Text = CARNAME1;
                    ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆保养记录";
                    ((Label)e.Item.FindControl("title")).Text = "具体保养计划";
                    ((Label)e.Item.FindControl("lbl_place")).Text = "保养地点";
                    ((Label)e.Item.FindControl("lbl_time")).Text = "保养时间";
                    DropDownList ddlcar = (DropDownList)e.Item.FindControl("CARID");
                    GetDDlcar(ddlcar, CARID1);
                    ((DropDownList)e.Item.FindControl("CARID")).SelectedValue = CARID1;
                    ((TextBox)e.Item.FindControl("txt_before")).Text = BYB;
                    ((TextBox)e.Item.FindControl("txt_byzq")).Text = BYZ;
                    ((TextBox)e.Item.FindControl("txt_after")).Text = BYA;
                    ((TextBox)e.Item.FindControl("txt_byyj")).Text = BYY;

                    ((TextBox)e.Item.FindControl("APPLYER")).Text = APPLYER1;
                    ((TextBox)e.Item.FindControl("PLACE")).Text = PLACE1;
                    ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Value = PLACEDATE1;
                    ((HtmlInputText)e.Item.FindControl("date")).Value = date1;
                    ((TextBox)e.Item.FindControl("txt_contents")).Text = txt_contents1;

                    ((TextBox)e.Item.FindControl("txtCode")).Enabled = false;
                }

                else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                }
                else if (e.Item.ItemType == ListItemType.Footer)
                {
                    ((TextBox)e.Item.FindControl("managerview")).Visible = false;
                    ((TextBox)e.Item.FindControl("controllerview")).Visible = false;
                    ((TextBox)e.Item.FindControl("repair")).Visible = false;
                    ((TextBox)e.Item.FindControl("apply")).Visible = false;
                    ((HtmlInputText)e.Item.FindControl("moneyall")).Value = moneyall1;
                    //((HtmlInputText)e.Item.FindControl("moneyall")).Disabled = true;
                }
            }

            else if (Request.QueryString["action"] == "mod")
            {
                if (Request.QueryString["type"] == "by")
                {
                    if (e.Item.ItemType == ListItemType.Header)
                    {
                        ((TextBox)e.Item.FindControl("txtCode")).Text = id;
                        ((TextBox)e.Item.FindControl("CARNAME")).Text = CARNAME1;
                        ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆保养申请";
                        ((Label)e.Item.FindControl("title")).Text = "具体保养计划";
                        ((Label)e.Item.FindControl("lbl_place")).Text = "保养地点";
                        ((Label)e.Item.FindControl("lbl_time")).Text = "保养时间";
                        DropDownList ddlcar = (DropDownList)e.Item.FindControl("CARID");
                        GetDDlcar(ddlcar, CARID1);
                        ((DropDownList)e.Item.FindControl("CARID")).SelectedValue = CARID1;
                        ((TextBox)e.Item.FindControl("txt_before")).Text = BYB;
                        ((TextBox)e.Item.FindControl("txt_byzq")).Text = BYZ;
                        ((TextBox)e.Item.FindControl("txt_after")).Text = BYA;
                        ((TextBox)e.Item.FindControl("txt_byyj")).Text = BYY;

                        ((TextBox)e.Item.FindControl("APPLYER")).Text = APPLYER1;
                        ((TextBox)e.Item.FindControl("PLACE")).Text = PLACE1;
                        ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Value = PLACEDATE1;
                        ((HtmlInputText)e.Item.FindControl("date")).Value = date1;
                        ((TextBox)e.Item.FindControl("txt_contents")).Text = txt_contents1;

                        ((TextBox)e.Item.FindControl("txtCode")).Enabled = false;
                        ((TextBox)e.Item.FindControl("APPLYER")).Enabled = false;
                        ((TextBox)e.Item.FindControl("PLACE")).Enabled = false;
                        ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Disabled = true;
                        ((HtmlInputText)e.Item.FindControl("date")).Disabled = true;

                        ((TextBox)e.Item.FindControl("txtCode")).Enabled = false;
                    }

                    else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                    }
                    else if (e.Item.ItemType == ListItemType.Footer)
                    {
                        ((TextBox)e.Item.FindControl("managerview")).Visible = false;
                        ((TextBox)e.Item.FindControl("controllerview")).Visible = false;
                        ((TextBox)e.Item.FindControl("repair")).Visible = false;
                        ((TextBox)e.Item.FindControl("apply")).Visible = false;
                        ((HtmlInputText)e.Item.FindControl("moneyall")).Value = moneyall1;
                        //((HtmlInputText)e.Item.FindControl("moneyall")).Disabled = true;
                    }
                }
                else if (Request.QueryString["type"] == "wx")
                {
                    if (e.Item.ItemType == ListItemType.Header)
                    {
                        ((TextBox)e.Item.FindControl("txtCode")).Text = id;
                        ((TextBox)e.Item.FindControl("CARNAME")).Text = CARNAME1;
                        ((Label)Repeater1.Controls[0].FindControl("iid")).Text = "车辆维修申请";
                        ((Label)e.Item.FindControl("title")).Text = "具体维修计划";
                        ((Label)e.Item.FindControl("lbl_place")).Text = "维修地点";
                        ((Label)e.Item.FindControl("lbl_time")).Text = "维修时间";
                        DropDownList ddlcar = (DropDownList)e.Item.FindControl("CARID");
                        GetDDlcar(ddlcar, CARID1);
                        ((DropDownList)e.Item.FindControl("CARID")).SelectedValue = CARID1;
                        ((TextBox)e.Item.FindControl("txt_before")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_byzq")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_after")).Visible = false;
                        ((TextBox)e.Item.FindControl("txt_byyj")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_bybefore")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_byzq")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_byafter")).Visible = false;
                        ((Label)e.Item.FindControl("lbl_byyj")).Visible = false;

                        ((TextBox)e.Item.FindControl("APPLYER")).Text = APPLYER1;
                        ((TextBox)e.Item.FindControl("PLACE")).Text = PLACE1;
                        ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Value = PLACEDATE1;
                        ((HtmlInputText)e.Item.FindControl("date")).Value = date1;
                        ((TextBox)e.Item.FindControl("txt_contents")).Text = txt_contents1;

                        ((TextBox)e.Item.FindControl("txtCode")).Enabled = false;
                        ((TextBox)e.Item.FindControl("APPLYER")).Enabled = false;
                        ((TextBox)e.Item.FindControl("PLACE")).Enabled = false;
                        ((HtmlInputText)e.Item.FindControl("PLACEDATE")).Disabled = true;
                        ((HtmlInputText)e.Item.FindControl("date")).Disabled = true;
                        //((TextBox)e.Item.FindControl("txt_contents")).Enabled = false;
                    }

                    else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                    }
                    else if (e.Item.ItemType == ListItemType.Footer)
                    {
                        ((TextBox)e.Item.FindControl("managerview")).Visible = false;
                        ((TextBox)e.Item.FindControl("controllerview")).Visible = false;
                        ((TextBox)e.Item.FindControl("repair")).Visible = false;
                        ((TextBox)e.Item.FindControl("apply")).Visible = false;
                        ((HtmlInputText)e.Item.FindControl("moneyall")).Value = moneyall1;
                    }
                }
            }

        }
        protected System.Data.DataTable GetDataFromGrid()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            #region
            dt.Columns.Add("GOODSNAME");
            dt.Columns.Add("GOODSCOUNT");
            dt.Columns.Add("GOODSUNIT");
            dt.Columns.Add("GOODSPRICE");
            dt.Columns.Add("MONEYONE");
            #endregion
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem rItem = Repeater1.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)rItem.FindControl("goodsname")).Text.Trim();
                newRow[1] = ((HtmlInputText)rItem.FindControl("goodscount")).Value.Trim();
                newRow[2] = ((TextBox)rItem.FindControl("goodsunit")).Text.Trim();
                newRow[3] = ((TextBox)rItem.FindControl("goodsprice")).Text.Trim();
                newRow[4] = ((HtmlInputText)rItem.FindControl("moneyone")).Value.Trim();
                dt.Rows.Add(newRow);
            }
            for (int i = Repeater1.Items.Count; i < 6; i++)
            {
                DataRow newRow = dt.NewRow();

                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        private void InitRepeaterAdd()
        {
            System.Data.DataTable dt = this.GetDataFromGrid();
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        private void InitRepeaterView()
        {
            string sqltext = "select GOODSNAME,GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE from TBOM_CARWXSQ where CODE='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        private void jl_InitRepeaterView()
        {
            string sqltext = "select GOODSNAME,GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE from TBOM_CARSAFE where CODE='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void btninsert_Click(object sender, EventArgs e)
        {
            //GetData();
            System.Data.DataTable dt = this.GetDataFromGrid();
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem rItem = Repeater1.Items[i];
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)rItem.FindControl("cbchecked");
                if (chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    dt.Rows.InsertAt(newRow, i + 1 + count);
                    ///////////dt.Rows.RemoveAt(dt.Rows.Count-1);
                    count++;
                }
            }
            jl_bind_info();
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            //***********删除数据不对数据库操作************
            //GetData();
            System.Data.DataTable dt = this.GetDataFromGrid();
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem rItem = Repeater1.Items[i];
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)rItem.FindControl("cbchecked");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            for (int i = dt.Rows.Count; i < 6; i++)
            {
                DataRow newRow = dt.NewRow();

                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            jl_bind_info();
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void btnLoad_OnClick(object sender, EventArgs e)
        {
            string leixing = Request.QueryString["type"].ToString();
            string sqltext = "";
            string date2 = ((HtmlInputText)Repeater1.Controls[0].FindControl("date")).Value.Trim();
            string code2 = ((TextBox)Repeater1.Controls[0].FindControl("txtCode")).Text.Trim();
            string before = ((TextBox)Repeater1.Controls[0].FindControl("txt_before")).Text.Trim();
            string byzq = ((TextBox)Repeater1.Controls[0].FindControl("txt_byzq")).Text.Trim();
            string after = ((TextBox)Repeater1.Controls[0].FindControl("txt_after")).Text.Trim();
            string byyj = ((TextBox)Repeater1.Controls[0].FindControl("txt_byyj")).Text.Trim();

            string carname2 = ((TextBox)Repeater1.Controls[0].FindControl("CARNAME")).Text.Trim();
            string carid2 = ((DropDownList)Repeater1.Controls[0].FindControl("CARID")).SelectedValue.Trim();
            string place2 = ((TextBox)Repeater1.Controls[0].FindControl("PLACE")).Text.Trim();
            string placedate2 = ((HtmlInputText)Repeater1.Controls[0].FindControl("PLACEDATE")).Value.Trim();
            string contents2 = ((TextBox)Repeater1.Controls[0].FindControl("txt_contents")).Text.Trim();
            string moneyall2 = ((HtmlInputText)Repeater1.Controls[Repeater1.Controls.Count - 1].FindControl("moneyall")).Value.Trim();
            string mview2 = ((TextBox)Repeater1.Controls[Repeater1.Controls.Count - 1].FindControl("managerview")).Text.Trim();
            string cview2 = ((TextBox)Repeater1.Controls[Repeater1.Controls.Count - 1].FindControl("controllerview")).Text.Trim();
            string repair2 = ((TextBox)Repeater1.Controls[Repeater1.Controls.Count - 1].FindControl("repair")).Text.Trim();
            string sqrid = Session["UserID"].ToString();
            string allmoney = "0";
            List<string> list_sql = new List<string>();
            if ((after == "" || byyj == "") && leixing == "by")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('下次保养公里数及预警公里数不能为空');", true);
                return;
            }

            if (date2 == "" || placedate2 == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('日期不能为空');", true);
                return;
            }
            if (Request.QueryString["action"] == "add")
            {
                //sqltext = "DELETE FROM TBOM_CARWXSQ WHERE CODE='" + code2.ToString() + "' ";
                //list_sql.Add(sqltext);
                foreach (RepeaterItem ri in Repeater1.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox gname = (TextBox)ri.FindControl("goodsname");
                        if (gname.Text.Trim() != "")
                        {
                            HtmlInputText gcount = (HtmlInputText)ri.FindControl("goodscount");
                            TextBox gunit = (TextBox)ri.FindControl("goodsunit");
                            TextBox gprice = (TextBox)ri.FindControl("goodsprice");
                            HtmlInputText xiaoji = (HtmlInputText)ri.FindControl("moneyone");
                            //allmoney=
                            //allmoney = Convert.ToString(Convert.ToInt32(xiaoji) + Convert.ToInt32(allmoney));
                            sqltext = "insert into TBOM_CARWXSQ (CODE,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,GOODSNAME,GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE,MONEYALL,MANAGERNAME,MANAGERID,STATE,CONTROLLERNAME,CONTROLLERID,CARNAME,CARID,REPAIR,CONTENTS,TYPEID,BYBEFORE,BYAFTER,BYZQ,BYYJ)" +
                                "values('" + code2.ToString() + "','" + Session["UserName"].ToString() + "','" + Session["UserID"].ToString() + "','" + date2 + "','" + place2 + "','" + placedate2 + "','" + gname.Text.Trim() + "','" + gcount.Value.Trim() + "','" + gunit.Text.Trim() + "','" + gprice.Text.Trim() + "','" + xiaoji.Value.Trim() + "','" + moneyall2.Trim() + "','李永亮','207','0','刘晓静','150','" + carname2 + "','" + carid2 + "','" + repair2 + "','" + contents2 + "','" + leixing.Trim() + "','" + before.Trim() + "','" + after.Trim() + "','" + byzq.Trim() + "','" + byyj.Trim() + "')";
                            list_sql.Add(sqltext);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                string _emailto = DBCallCommon.GetEmailAddressByUserID("207");
                string _body = "车辆维修/保养审批任务:"
                      + "\r\n单号：" + code2.ToString()
                      + "\r\n制单日期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string _subject = "您有新的【车辆维修/保养】需要审批，请及时处理:" + code2.ToString();
                DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('申请成功，已进入审批流程！');window.close();", true);
                Response.Redirect("OM_CarWeixiuShenqing.aspx");
            }
            else if (Request.QueryString["action"] == "jlwx")
            {
                foreach (RepeaterItem ri in Repeater1.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox gname = (TextBox)ri.FindControl("goodsname");
                        if (gname.Text.Trim() != "")
                        {
                            HtmlInputText gcount = (HtmlInputText)ri.FindControl("goodscount");
                            TextBox gunit = (TextBox)ri.FindControl("goodsunit");
                            TextBox gprice = (TextBox)ri.FindControl("goodsprice");
                            HtmlInputText xiaoji = (HtmlInputText)ri.FindControl("moneyone");
                            //allmoney = Convert.ToString(Convert.ToInt32(xiaoji) + Convert.ToInt32(allmoney));
                            sqltext = "insert into TBOM_CARSAFE (CODE,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,GOODSNAME,GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE,MONEYALL,CARNAME,CARID,REPAIR,CONTENTS,TYPEID)" +
                                "values('" + code2.ToString() + "','" + Session["UserName"].ToString() + "','" + Session["UserID"].ToString() + "','" + date2 + "','" + place2 + "','" + placedate2 + "','" + gname.Text.Trim() + "','" + gcount.Value.Trim() + "','" + gunit.Text.Trim() + "','" + gprice.Text.Trim() + "','" + xiaoji.Value.Trim() + "','" + moneyall2.Trim() + "','" + carname2 + "','" + carid2 + "','" + repair2 + "','" + contents2 + "','" + leixing.Trim() + "')";
                            list_sql.Add(sqltext);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加维修记录成功！');window.close();", true);
                Response.Redirect("OM_CarWeihu.aspx");
            }
            else if (Request.QueryString["action"] == "jlby")
            {
                foreach (RepeaterItem ri in Repeater1.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox gname = (TextBox)ri.FindControl("goodsname");
                        if (gname.Text.Trim() != "")
                        {
                            HtmlInputText gcount = (HtmlInputText)ri.FindControl("goodscount");
                            TextBox gunit = (TextBox)ri.FindControl("goodsunit");
                            TextBox gprice = (TextBox)ri.FindControl("goodsprice");
                            HtmlInputText xiaoji = (HtmlInputText)ri.FindControl("moneyone");
                            //allmoney = Convert.ToString(Convert.ToInt32(xiaoji) + Convert.ToInt32(allmoney));
                            sqltext = "insert into TBOM_CARSAFE (CODE,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,GOODSNAME,GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE,MONEYALL,CARNAME,CARID,REPAIR,CONTENTS,TYPEID,BYBEFORE,BYAFTER,BYZQ,BYYJ)" +
                                "values('" + code2.ToString() + "','" + Session["UserName"].ToString() + "','" + Session["UserID"].ToString() + "','" + date2 + "','" + place2 + "','" + placedate2 + "','" + gname.Text.Trim() + "','" + gcount.Value.Trim() + "','" + gunit.Text.Trim() + "','" + gprice.Text.Trim() + "','" + xiaoji.Value.Trim() + "','" + moneyall2.Trim() + "','" + carname2 + "','" + carid2 + "','" + repair2 + "','" + contents2 + "','" + leixing.Trim() + "','" + before.Trim() + "','" + after.Trim() + "','" + byzq.Trim() + "','" + byyj.Trim() + "')";
                            list_sql.Add(sqltext);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加保养记录成功！');window.close();", true);
                Response.Redirect("OM_CarWeihu.aspx");
            }
            else if (Request.QueryString["action"] == "mod")
            {
                sqltext = "DELETE FROM TBOM_CARWXSQ WHERE CODE='" + code2.ToString() + "' ";
                list_sql.Add(sqltext);
                foreach (RepeaterItem ri in Repeater1.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox gname = (TextBox)ri.FindControl("goodsname");
                        if (gname.Text.Trim() != "")
                        {
                            HtmlInputText gcount = (HtmlInputText)ri.FindControl("goodscount");
                            TextBox gunit = (TextBox)ri.FindControl("goodsunit");
                            TextBox gprice = (TextBox)ri.FindControl("goodsprice");
                            HtmlInputText xiaoji = (HtmlInputText)ri.FindControl("moneyone");
                            sqltext = "insert into TBOM_CARWXSQ (CODE,APPLYNAME,APPLYID,DATE,PLACE,PLACEDATE,GOODSNAME,GOODSCOUNT,GOODSUNIT,GOODSPRICE,MONEYONE,MONEYALL,MANAGERNAME,MANAGERID,STATE,CONTROLLERNAME,CONTROLLERID,CARNAME,CARID,REPAIR,CONTENTS,TYPEID,BYBEFORE,BYAFTER,BYZQ,BYYJ)" +
                                "values('" + code2.ToString() + "','" + Session["UserName"].ToString() + "','" + Session["UserID"].ToString() + "','" + date2 + "','" + place2 + "','" + placedate2 + "','" + gname.Text.Trim() + "','" + gcount.Value.Trim() + "','" + gunit.Text.Trim() + "','" + gprice.Text.Trim() + "','" + xiaoji.Value.Trim() + "','" + moneyall2.Trim() + "','李永亮','207','0','刘晓静','150','" + carname2 + "','" + carid2 + "','" + repair2 + "','" + contents2 + "','" + leixing.Trim() + "','" + before.Trim() + "','" + after.Trim() + "','" + byzq.Trim() + "','" + byyj.Trim() + "')";
                            list_sql.Add(sqltext);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('重新提交成功，已进入审批流程！');window.close();", true);
                Response.Redirect("OM_CarWeixiuShenqing.aspx");
            }
            else if (Request.QueryString["action"] == "view")
            {

                #region 二级审核2
                if (Session["UserID"].ToString() == "207" && zhaungtai.Text.ToString() == "0")
                {
                    string YIJIAN = rblfirst.SelectedItem.Text.ToString() + ',' + first_opinion.Text.Trim();
                    sqltext = "update TBOM_CARWXSQ SET STATE='" + rblfirst.SelectedValue.Trim() + "',MANAGERVIEW='" + YIJIAN + "' WHERE CODE='" + danhao.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                    if (rblfirst.SelectedValue.Trim() == "2")
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID("150");
                        string _body = "车辆维修/保养审批任务:"
                              + "\r\n单号：" + code2.ToString();

                        string _subject = "您有新的【车辆维修/保养】需要审批，请及时处理:" + code2.ToString();
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }

                }
                else if (Session["UserID"].ToString() == "150" && zhaungtai.Text.ToString() == "2")
                {
                    string yijian2 = rblsecond.SelectedItem.Text.ToString() + ',' + second_opinion.Text.Trim();
                    sqltext = "update TBOM_CARWXSQ SET STATE='" + rblsecond.SelectedValue.Trim() + "',CONTROLLERVIEW='" + yijian2 + "' WHERE CODE='" + danhao.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                }
                #endregion
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核成功');window.opener=null;window.open('','_self');window.close();window.returnValue='refresh'", true);
                //return;
                Response.Redirect("OM_CarWeixiuShenqing.aspx");
            }

        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            Response.Redirect("OM_CarWeixiuShenqing.aspx");
        }


        //关联带出数据
        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            int num1 = (sender as TextBox).Text.Trim().IndexOf("|", num + 1);
            TextBox Tb_newstid = (TextBox)sender;
            RepeaterItem Reitem = (RepeaterItem)Tb_newstid.Parent;

            if (num > 0)
            {
                string KC_MC = (sender as TextBox).Text.Trim().Substring(0, num);
                string KC_GG = (sender as TextBox).Text.Trim().Substring(num + 1, num1 - num - 1);

                string sqlText = "select * from OM_CARKUCUN where KC_MC='" + KC_MC + "' and KC_GG='" + KC_GG + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {

                    ((TextBox)Reitem.FindControl("goodsname")).Text = dt.Rows[0]["KC_MC"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("goodsunit")).Text = dt.Rows[0]["KC_DANWEI"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("goodsprice")).Text = dt.Rows[0]["KC_DJ"].ToString().Trim();
                }

            }
        }
    }
}
