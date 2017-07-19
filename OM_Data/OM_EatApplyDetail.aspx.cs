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
    public partial class OM_EatApplyDetail : System.Web.UI.Page
    {
        string id = "";
        string state = "";
        int count = 0;
        string applyname = "";
        string applyid = "";
        string fzr = "";
        string fzrid = "";
        string usetime = "";
        string peoplenum = "";
        string peopleguige = "";
        string applydate = "";
        string code = "";
        string applyphone = "";
        string note = "";
        string allmoney = "";
        //string zhaungtai = "";
        string shenehnote = "";
        string yctype = "";
      //public  string user_depid = "";

        string state_jl = "";
        string applyname_jl = "";
        string fzr_jl = "";
        string fzrid_jl = "";
        string usetime_jl = "";
        string peoplenum_jl = "";
        string peopleguige_jl = "";
        string applydate_jl = "";
        string code_jl = "";
        string applyphone_jl = "";
        string note_jl = "";
        string allmoney_jl = "";
        //string zhaungtai = "";
        string shenehnote_jl = "";
        string YCTP_JL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
                danhao.Text = id.ToString();
            }
            if (!IsPostBack)
            {

                if (Request.QueryString["action"] == "add")//新增用餐申请
                {
                    Tab_spxx.Visible = false;//审批信息不可见
                    InitRepeaterAdd();//添加三行
                    btnLoad.Visible = true;//保存 可见
                    TabContainer2.Visible = false;//反馈内容不可见
                    btndelete2.Visible = false;//反馈  删除
                    btninsert2.Visible = false;//反馈  插入
                    btnLoad2.Visible = false;//反馈    保存
                    btnReturn2.Visible = false;//反馈  返回
                    lab_caozuo2.Visible = false;
                    txtCode.Text = CODE();//编号   自动生成
                    date.Value = DateTime.Now.ToString();
                    txt_apply.Text = Session["UserName"].ToString();//申请人
                    txt_apply_id.Text = Session["UserID"].ToString();//申请人编号
                     //user_depid = Session["UserDeptID"].ToString();
                    //Request.Form[dep]. = user_depid.ToString();
                    //dep.Value = user_depid.ToString();
                    
                    //dep.Disabled = true;
                   
                }
                else if (Request.QueryString["action"] == "view")//查看  审核
                {
                    bind_info();
                    InitRepeaterView();
                    btndelete.Visible = false;
                    btninsert.Visible = false;
                    //((HyperLink)Repeater1.Controls[0].FindControl("hlSelect1")).Visible = false;
                    hlSelect1.Visible = false;
                    TabContainer2.Visible = false;
                    btndelete2.Visible = false;
                    btninsert2.Visible = false;
                    btnLoad2.Visible = false;
                    btnReturn2.Visible = false;
                    lab_caozuo2.Visible = false;
                    moneyall.Value = allmoney;
                    moneyall.Disabled = true;
                }
                else if (Request.QueryString["action"] == "mod")//修改
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改后将重新进入审批！');", true);
                    bind_info();
                    InitRepeaterView();
                    btnLoad.Visible = true;
                    Tab_spxx.Visible = false;
                    TabContainer2.Visible = false;
                    btndelete2.Visible = false;
                    btninsert2.Visible = false;
                    btnLoad2.Visible = false;
                    btnReturn2.Visible = false;
                    lab_caozuo2.Visible = false;
                    txtCode.Text = id;
                    date.Value = applydate;
                    txt_apply.Text = applyname;
                    txt_apply_id.Text = applyid;
                    txt_phone.Text = applyphone;
                    txt_first_fzr.Value = fzr;
                    firstid_fzr.Value = fzrid;
                    txt_guige.Text = peopleguige;
                    txt_renshu.Text = peoplenum;
                    usetime1.Value = usetime;
                    txt_contents.Text = note;
                   moneyall.Value = allmoney;
                   rad_yctype.SelectedValue = yctype;

                }
                else if (Request.QueryString["action"] == "addjl")//食堂反馈
                {
                    btndelete.Visible = false;
                    btninsert.Visible = false;
                    btnLoad.Visible = false;
                    btnReturn.Visible = false;
                    Tab_spxx.Visible = false;
                    lab_caozuo.Visible = false;

                    bind_info();//绑定申请内容
                    InitRepeaterView();
                    bind_info2();//绑定反馈内容
                    InitRepeaterView2();
                    btnLoad2.Visible = true;
                    txtCode2.Text = id;
                    date2.Value = DateTime.Now.ToString();
                    txt_apply2.Text = Session["UserName"].ToString();
                    txt_apply2_id.Text = Session["UserID"].ToString();
                    //((TextBox)e.Item.FindControl("txt_phone2")).Text = applyphone;
                    txt_first_fzr2.Value = applyname;
                    firstid_fzr2.Value = applyid;
                    txt_guige2.Text = peopleguige;
                    txt_renshu2.Text = peoplenum;
                    usetime22.Value = usetime;
                    txt_contents2.Text = note;
                    moneyall2.Value = allmoney;
                    //moneyall.Value = allmoney;
                    moneyall.Disabled = true;
                }
                else if (Request.QueryString["action"] == "modjl")
                {
                    btndelete.Visible = false;
                    btninsert.Visible = false;
                    btnLoad.Visible = false;
                    btnLoad2.Visible = true;
                    btnReturn.Visible = false;
                    Tab_spxx.Visible = false;
                    bind_info();
                    InitRepeaterView();
                    bind_info2();
                    InitRepeaterView3();
                    txtCode2.Text = id;
                    date2.Value = applydate_jl;
                    txt_apply2.Text = applyname_jl;
                    txt_phone2.Text = applyphone_jl;
                    txt_first_fzr2.Value = fzr_jl;
                    firstid_fzr2.Value = fzrid_jl;
                    txt_guige2.Text = peopleguige_jl;
                    txt_renshu2.Text = peoplenum_jl;
                    usetime22.Value = usetime_jl;
                    txt_contents2.Text = note_jl;
                    moneyall2.Value = allmoney_jl;
                    moneyall.Disabled = true;
                }
                else if (Request.QueryString["action"] == "viewjl")
                {

                    btndelete.Visible = false;
                    btninsert.Visible = false;
                    btnLoad.Visible = false;
                    btnReturn.Visible = false;
                    Tab_spxx.Visible = false;
                    bind_info();
                    InitRepeaterView();
                    bind_info2();
                    InitRepeaterView3();
                    btndelete2.Visible = false;
                    btninsert2.Visible = false;
                    moneyall.Disabled = true;
                    if (Session["UserID"].ToString() == applyid||Session["UserID"].ToString() == fzrid ||Session["UserID"].ToString() == "260")
                    {
                        Tabfk.Visible = true;
                    }
                    txtCode2.Text = id;
                    date2.Value = applydate_jl;
                    txt_apply2.Text = applyname_jl;
                    txt_phone2.Text = applyphone_jl;
                    txt_first_fzr2.Value = fzr_jl;
                    firstid_fzr2.Value = fzrid_jl;
                    txt_guige2.Text = peopleguige_jl;
                    txt_renshu2.Text = peoplenum_jl;
                    usetime22.Value = usetime_jl;
                    txt_contents2.Text = note_jl;

                    date2.Disabled = true;
                    txt_apply2.Enabled = false;
                    txt_phone2.Enabled = false;
                    txt_first_fzr2.Disabled = true;
                    txt_guige2.Enabled = false;
                    txt_renshu2.Enabled = false;
                    usetime22.Disabled = true;
                    txt_contents2.Enabled = false;
                    moneyall2.Value = allmoney_jl;
                    moneyall2.Disabled = true;
                }
            }
            //CheckUser(ControlFinder);
        }
        #region 申请人
        private void bind_info()
        {
            string sqltext = "select CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,STATE,SHITANGID,SHITANGNAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,ALLMONEY,SHENHENOTE,YCTYPE from TBOM_EAT where CODE='" + id + "' AND TYPE='SQ'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {

                applyname = dr["APPLYNAME"].ToString();//申请人
                applyid = dr["APPLYID"].ToString();//申请人编号
                applydate = dr["APPLYDATE"].ToString();//申请日期

                fzr = dr["SHENHENAME"].ToString();
                fzrid = dr["SHENHEID"].ToString();

                applyphone = dr["APPLYPHONE"].ToString();
                peopleguige = dr["PEOPLEGUIGE"].ToString();
                peoplenum = dr["PEOPLENUM"].ToString();
                usetime = dr["USETIME"].ToString();
                note = dr["NOTE"].ToString();
                allmoney = dr["ALLMONEY"].ToString();
                state = dr["STATE"].ToString();
                //zhaungtai.Text = dr["STATE"].ToString();
                shenehnote = dr["SHENHENOTE"].ToString();
                txt_first.Text = fzr;//审批人
                //rblfirst.SelectedValue = dr["STATE"].ToString();
                first_opinion.Text = dr["SHENHENOTE"].ToString();//审核意见
                //rad_yctype.SelectedValue = dr["YCTYPE"].ToString();
                yctype = dr["YCTYPE"].ToString();

                if (state == "2")
                {
                    rblfirst.SelectedValue = state.ToString();
                }
                else if (state != "0")
                {
                    rblfirst.SelectedValue = "1";
                }
            }
            dr.Close();
        }

        private string CODE()
        {
            string code2 = DateTime.Today.ToString("yyyyMMdd") + '-' + 'C' + 'T';
            string sql = "select MAX(CODE) as CODE FROM TBOM_CODE WHERE CODE LIKE '%" + code2 + "%'";
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
                    code2 += num;
                }

                else
                {
                    code2 += "001";
                }
                string SLQTXT = "insert into TBOM_CODE (CODE) values('" + code2 + "')";
                DBCallCommon.ExeSqlText(SLQTXT);
            }
            return code2;
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (Request.QueryString["action"] == "add")
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                }
            }
            else if (Request.QueryString["action"] == "mod")
            {
               
                btnLoad.Visible = true;
            }
            else if (Request.QueryString["action"] == "view" || Request.QueryString["action"] == "addjl" || Request.QueryString["action"] == "modjl" || Request.QueryString["action"] == "viewjl")
            {
                txtCode.Text = id;
                date.Value = applydate;
                txt_apply.Text = applyname;
                txt_phone.Text = applyphone;
                txt_first_fzr.Value = fzr;
                firstid_fzr.Value = fzrid;
                txt_guige.Text = peopleguige;
                txt_renshu.Text = peoplenum;
                usetime1.Value = usetime;
                txt_contents.Text = note;
                moneyall.Value = allmoney;
                rad_yctype.SelectedValue = yctype;

                date.Disabled = true;
                txt_apply.Enabled = false;
                txt_phone.Enabled = false;
                txt_first_fzr.Disabled = true;
                txt_guige.Enabled = false;
                txt_renshu.Enabled = false;
                usetime1.Disabled = true;
                txt_contents.Enabled = false;
                rad_yctype.Enabled = false;
                
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ((CheckBox)e.Item.FindControl("cbchecked")).Visible = false;
                    ((HtmlInputText)e.Item.FindControl("goodsname")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("goodsguige")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("goodsnum")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("goodsdanwei")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("goodsprice")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("moneyone")).Disabled = true;
                }
                
                if (Session["UserID"].ToString() == fzrid && state == "0")
                {
                    rblfirst.Enabled = true;
                    first_opinion.Enabled = true;
                    btnLoad.Visible = true;
                }
            }
        }
        protected System.Data.DataTable GetDataFromGrid()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            #region
            dt.Columns.Add("GOODNAME");
            dt.Columns.Add("GOODGUIGE");
            dt.Columns.Add("GOODNUM");
            dt.Columns.Add("GOODDANWEI");
            dt.Columns.Add("GOODPRICE");
            dt.Columns.Add("GOODMONEY");
            #endregion
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem rItem = Repeater1.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HtmlInputText)rItem.FindControl("goodsname")).Value.Trim();
                newRow[1] = ((HtmlInputText)rItem.FindControl("goodsguige")).Value.Trim();
                newRow[2] = ((HtmlInputText)rItem.FindControl("goodsnum")).Value.Trim();
                newRow[3] = ((HtmlInputText)rItem.FindControl("goodsdanwei")).Value.Trim();
                newRow[4] = ((HtmlInputText)rItem.FindControl("goodsprice")).Value.Trim();
                newRow[5] = ((HtmlInputText)rItem.FindControl("moneyone")).Value.Trim();
                dt.Rows.Add(newRow);
            }
            for (int i = Repeater1.Items.Count; i < 3; i++)
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
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
        }
        private void InitRepeaterView()
        {
            string sqltext = "select GOODNAME,GOODGUIGE,GOODNUM,GOODDANWEI,GOODPRICE,GOODMONEY from TBOM_EAT where CODE='" + id + "' AND TYPE='SQ'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void btninsert_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = this.GetDataFromGrid();
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem rItem = Repeater1.Items[i];
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)rItem.FindControl("cbchecked");
                if (chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    dt.Rows.InsertAt(newRow, i + 1 + count);
                    count++;
                }
            }
            dt.AcceptChanges();
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
            bind_info();
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
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
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
            bind_info();
        }
        protected void btnLoad_OnClick(object sender, EventArgs e)
        {
            //申请内容
            int xunhuan=0;
            int mm = 0;
            string code2 = txtCode.Text.ToString();//单号
            string apllyid2 = Session["UserID"].ToString();//登录人
            string applyname2 = txt_apply.Text.ToString();//申请人
            string applyphone2 = txt_phone.Text.ToString();//
            string applydate2 = date.Value.ToString();
            string peoplenum2 = txt_renshu.Text.ToString();//用餐人数
            string peopleguige2 = txt_guige.Text.ToString();//用餐标准
            string usetime2 = usetime1.Value.ToString();//用餐时间
            string fzr2 = txt_first_fzr.Value.ToString();//部门审批人
            string bumenid2 = Session["UserDeptID"].ToString();//部门编号
            string bumenname2 = Session["UserDept"].ToString();//部门名称
            string fzrid2 = firstid_fzr.Value.ToString();//部门审批人ID
            string note2 = txt_contents.Text.ToString();//备注
            string allmoney2 = moneyall.Value.ToString();//合计
            string shenhenote2 = first_opinion.Text.ToString();//审核意见
            string yctp = rad_yctype.SelectedValue.ToString();//用餐类型
            string sqltext = "";
            List<string> list_sql = new List<string>();
            if (Request.QueryString["action"] == "add")//新增用餐申请
            {
                foreach (RepeaterItem ri in Repeater1.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        mm++;
                        HtmlInputText goodsname = (HtmlInputText)ri.FindControl("goodsname");
                        if (goodsname.Value.Trim() == "")
                        {
                            xunhuan++;
                          
                        }
                        
                        if(goodsname.Value.Trim() != "")
                        {
                            HtmlInputText goodsguige = (HtmlInputText)ri.FindControl("goodsguige");
                            HtmlInputText goodsnum = (HtmlInputText)ri.FindControl("goodsnum");
                            HtmlInputText goodsdanwei = (HtmlInputText)ri.FindControl("goodsdanwei");
                            HtmlInputText goodsprice = (HtmlInputText)ri.FindControl("goodsprice");
                            HtmlInputText moneyone = (HtmlInputText)ri.FindControl("moneyone");
                            sqltext = "insert into TBOM_EAT (CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,GOODNAME,GOODGUIGE,GOODNUM,GOODDANWEI,GOODPRICE,GOODMONEY,ALLMONEY,TYPE,YCTYPE) values ('" + code2.ToString() + "','" + txt_apply.Text.ToString() + "','" + txt_apply_id.Text.ToString() + "','" + applydate2 + "','" + applyphone2 + "','" + bumenid2 + "','" + bumenname2 + "','" + fzrid2 + "','" + fzr2 + "','" + usetime2 + "','" + peoplenum2 + "','" + peopleguige2 + "','" + note2 + "','" + goodsname.Value.ToString() + "','" + goodsguige.Value.ToString() + "','" + goodsnum.Value.ToString() + "','" + goodsdanwei.Value.ToString() + "','" + goodsprice.Value.ToString() + "','" + moneyone.Value.ToString() + "','" + allmoney2 + "','SQ','"+yctp+"')";
                            list_sql.Add(sqltext);
                        }

                    }
                }
                if (xunhuan == mm)
                {
                    sqltext = "insert into TBOM_EAT (CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,ALLMONEY,TYPE,YCTYPE) values('" + code2.ToString() + "','" + txt_apply.Text.ToString() + "','" + txt_apply_id.Text.ToString() + "','" + applydate2 + "','" + applyphone2 + "','" + bumenid2 + "','" + bumenname2 + "','" + fzrid2 + "','" + fzr2 + "','" + usetime2 + "','" + peoplenum2 + "','" + peopleguige2 + "','" + note2 + "','" + allmoney2 + "','SQ','"+yctp+"')";
                    list_sql.Add(sqltext);
                }
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('申请成功，已进入审批流程！');window.close();", true);
                Response.Redirect("OM_Eat.aspx");
            }

            else if (Request.QueryString["action"] == "mod")//修改 或者  驳回处理
            {
                sqltext = "DELETE FROM TBOM_EAT WHERE CODE='" + code2.ToString() + "' ";
                list_sql.Add(sqltext);
                foreach (RepeaterItem ri in Repeater1.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        mm++;
                        HtmlInputText goodsname = (HtmlInputText)ri.FindControl("goodsname");
                        if (goodsname.Value.Trim() == "")
                        {
                            xunhuan++;

                        }
                        if (goodsname.Value.Trim() != "")
                        {
                            HtmlInputText goodsguige = (HtmlInputText)ri.FindControl("goodsguige");
                            HtmlInputText goodsnum = (HtmlInputText)ri.FindControl("goodsnum");
                            HtmlInputText goodsdanwei = (HtmlInputText)ri.FindControl("goodsdanwei");
                            HtmlInputText goodsprice = (HtmlInputText)ri.FindControl("goodsprice");
                            HtmlInputText moneyone = (HtmlInputText)ri.FindControl("moneyone");
                            sqltext = "insert into TBOM_EAT (CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,GOODNAME,GOODGUIGE,GOODNUM,GOODDANWEI,GOODPRICE,GOODMONEY,ALLMONEY,TYPE,YCTYPE)" +
                                "values('" + code2.ToString() + "','" + txt_apply.Text.ToString() + "','" + txt_apply_id.Text.ToString() + "','" + applydate2 + "','" + applyphone2 + "','" + bumenid2 + "','" + bumenname2 + "','" + fzrid2 + "','" + fzr2 + "','" + usetime2 + "','" + peoplenum2 + "','" + peopleguige2 + "','" + note2 + "','" + goodsname.Value.ToString() + "','" + goodsguige.Value.ToString() + "','" + goodsnum.Value.ToString() + "','" + goodsdanwei.Value.ToString() + "','" + goodsprice.Value.ToString() + "','" + moneyone.Value.ToString() + "','" + allmoney2 + "','SQ','"+yctp+"')";
                            list_sql.Add(sqltext);
                        }
                    }
                }
                if (xunhuan == mm)
                {
                    sqltext = "insert into TBOM_EAT (CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,ALLMONEY,TYPE,YCTYPE)" +
                        "values('" + code2.ToString() + "','" + txt_apply.Text.ToString() + "','" + txt_apply_id.Text.ToString() + "','" + applydate2 + "','" + applyphone2 + "','" + bumenid2 + "','" + bumenname2 + "','" + fzrid2 + "','" + fzr2 + "','" + usetime2 + "','" + peoplenum2 + "','" + peopleguige2 + "','" + note2 + "','" + allmoney2 + "','SQ','" + yctp + "')";
                    list_sql.Add(sqltext);
                }
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('重新提交成功，已进入审批流程！');window.close();", true);
                Response.Redirect("OM_Eat.aspx");
            }
            else if (Request.QueryString["action"] == "view")//查看进入 或者  审核进入
            {

                #region 一级审核
                if (Session["UserID"].ToString() == fzrid2)
                {
                    //string YIJIAN = rblfirst.SelectedItem.Text.ToString() + ',' + first_opinion.Text.Trim();
                    sqltext = "update TBOM_EAT SET STATE='" + rblfirst.SelectedValue.Trim() + "',SHENHENOTE='" + shenhenote2 + "' WHERE CODE='" + danhao.Text.Trim() + "' AND TYPE='SQ'";
                    list_sql.Add(sqltext);

                }

                #endregion
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核成功');window.opener=null;window.open('','_self');window.close();window.returnValue='refresh'", true);
                //return;
                Response.Redirect("OM_Eat.aspx");
            }

        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            Response.Redirect("OM_Eat.aspx");
        }
        #endregion

        #region 食堂
        private void bind_info2()
        {

            string sqltext = "select CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,STATE,SHITANGID,SHITANGNAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,ALLMONEY,SHENHENOTE,YCTYPE from TBOM_EAT where CODE='" + id + "' AND TYPE='JL'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {

                applyname_jl = dr["APPLYNAME"].ToString();
                applydate_jl = dr["APPLYDATE"].ToString();
                fzr_jl = dr["SHENHENAME"].ToString();
                fzrid_jl = dr["SHENHEID"].ToString();
                applyphone_jl = dr["APPLYPHONE"].ToString();
                peopleguige_jl = dr["PEOPLEGUIGE"].ToString();
                peoplenum_jl = dr["PEOPLENUM"].ToString();
                usetime_jl = dr["USETIME"].ToString();
                note_jl = dr["NOTE"].ToString();
                allmoney_jl = dr["ALLMONEY"].ToString();
                state_jl = dr["STATE"].ToString();
                //zhaungtai.Text = dr["STATE"].ToString();
                //shenehnote_jl = dr["SHENHENOTE"].ToString();
                shenhenote3.Text = dr["SHENHENOTE"].ToString();
                rblfirst2.SelectedValue = dr["STATE"].ToString();
                txt_fankuiren.Text = dr["SHENHENAME"].ToString();
                txt_fankuirenid.Value = dr["SHENHEID"].ToString();
                YCTP_JL = dr["YCTYPE"].ToString();
            }
            dr.Close();
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (Request.QueryString["action"] == "addjl")
            {               
            }
            else if (Request.QueryString["action"] == "modjl")
            {
                btnLoad2.Visible = true;
            }
            else if (Request.QueryString["action"] == "viewjl")
            {
                 if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ((CheckBox)e.Item.FindControl("cbchecked")).Visible = false;
                    ((HtmlInputText)e.Item.FindControl("goodsname2")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("goodsguige2")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("goodsnum2")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("goodsdanwei2")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("goodsprice2")).Disabled = true;
                    ((HtmlInputText)e.Item.FindControl("moneyone2")).Disabled = true;
                }
                if (Session["UserID"].ToString() == applyid && state == "6")
                {
                    rblfirst2.Enabled = true;
                    shenhenote3.Enabled = true;
                    btnLoad2.Visible = true;
                }
            }
        }
        protected System.Data.DataTable GetDataFromGrid2()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            #region
            dt.Columns.Add("GOODNAME");
            dt.Columns.Add("GOODGUIGE");
            dt.Columns.Add("GOODNUM");
            dt.Columns.Add("GOODDANWEI");
            dt.Columns.Add("GOODPRICE");
            dt.Columns.Add("GOODMONEY");
            #endregion
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                RepeaterItem rItem = Repeater2.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HtmlInputText)rItem.FindControl("goodsname2")).Value.Trim();
                newRow[1] = ((HtmlInputText)rItem.FindControl("goodsguige2")).Value.Trim();
                newRow[2] = ((HtmlInputText)rItem.FindControl("goodsnum2")).Value.Trim();
                newRow[3] = ((HtmlInputText)rItem.FindControl("goodsdanwei2")).Value.Trim();
                newRow[4] = ((HtmlInputText)rItem.FindControl("goodsprice2")).Value.Trim();
                newRow[5] = ((HtmlInputText)rItem.FindControl("moneyone2")).Value.Trim();
                dt.Rows.Add(newRow);
            }
            for (int i = Repeater2.Items.Count; i < 3; i++)
            {
                DataRow newRow = dt.NewRow();

                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        private void InitRepeaterAdd2()
        {
            System.Data.DataTable dt = this.GetDataFromGrid2();
            this.Repeater2.DataSource = dt;
            this.Repeater2.DataBind();
        }
        private void InitRepeaterView2()
        {
            string sqltext = "select GOODNAME,GOODGUIGE,GOODNUM,GOODDANWEI,GOODPRICE,GOODMONEY from TBOM_EAT where CODE='" + id + "' and TYPE='SQ'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            this.Repeater2.DataSource = dt;
            this.Repeater2.DataBind();
        }
        private void InitRepeaterView3()
        {
            string sqltext = "select GOODNAME,GOODGUIGE,GOODNUM,GOODDANWEI,GOODPRICE,GOODMONEY from TBOM_EAT where CODE='" + id + "' and TYPE='JL'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            this.Repeater2.DataSource = dt;
            this.Repeater2.DataBind();
        }
        protected void btninsert2_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = this.GetDataFromGrid2();
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                RepeaterItem rItem = Repeater2.Items[i];
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)rItem.FindControl("cbchecked");
                if (chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    dt.Rows.InsertAt(newRow, i + 1 + count);
                    count++;
                }
            }
            dt.AcceptChanges();
            this.Repeater2.DataSource = dt;
            this.Repeater2.DataBind();
            //if (Request.QueryString["action"] == "addjl")
            //{
            //    bind_info();
            //}
            //if (Request.QueryString["action"] == "modjl")
            //{
            //    bind_info2();
            //}
        }
        protected void btndelete2_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = this.GetDataFromGrid2();
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                RepeaterItem rItem = Repeater2.Items[i];
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
            this.Repeater2.DataSource = dt;
            this.Repeater2.DataBind();
            //if (Request.QueryString["action"] == "addjl")
            //{
            //    bind_info();
            //}
            //if (Request.QueryString["action"] == "modjl")
            //{
            //    bind_info2();
            //}
        }
        protected void btnLoad2_OnClick(object sender, EventArgs e)
        {
            int xunhuan = 0;
            int mm = 0;
            string code2 = txtCode2.Text.ToString();
            string apllyid2 = Session["UserID"].ToString();
            string applyname2 = txt_apply2.Text.ToString();

            string applyphone2 = txt_phone2.Text.ToString();
            string applydate2 = date2.Value.ToString();
            string peoplenum2 = txt_renshu2.Text.ToString();
            string peopleguige2 = txt_guige2.Text.ToString();
            string usetime2 = usetime22.Value.ToString();
            string fzr2 = txt_first_fzr2.Value.ToString();
            string bumenid2 = Session["UserDeptID"].ToString();
            string bumenname2 = Session["UserDept"].ToString();
            string fzrid2 = firstid_fzr2.Value.ToString();
            string note2 = txt_contents2.Text.ToString();
            string allmoney2 = moneyall2.Value.ToString();
            string shenhenote2 = first_opinion.Text.ToString();
            string yctp2 = rad_yctype.SelectedValue.ToString();
            string sqltext = "";
            List<string> list_sql = new List<string>();
            if (Request.QueryString["action"] == "addjl")
            {
                foreach (RepeaterItem ri in Repeater2.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        mm++;
                        HtmlInputText goodsname = (HtmlInputText)ri.FindControl("goodsname2");
                        if (goodsname.Value.Trim() == "")
                        {
                            xunhuan++;
                            
                        }
                        if (goodsname.Value.Trim() != "")
                        {
                            HtmlInputText goodsguige = (HtmlInputText)ri.FindControl("goodsguige2");
                            HtmlInputText goodsnum = (HtmlInputText)ri.FindControl("goodsnum2");
                            HtmlInputText goodsdanwei = (HtmlInputText)ri.FindControl("goodsdanwei2");
                            HtmlInputText goodsprice = (HtmlInputText)ri.FindControl("goodsprice2");
                            HtmlInputText moneyone = (HtmlInputText)ri.FindControl("moneyone2");
                            sqltext = "insert into TBOM_EAT (CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,GOODNAME,GOODGUIGE,GOODNUM,GOODDANWEI,GOODPRICE,GOODMONEY,ALLMONEY,TYPE,STATE,YCTYPE)" +
                                "values('" + code2.ToString() + "','" + txt_apply2.Text.ToString() + "','" + txt_apply2_id.Text.ToString() + "','" + applydate2 + "','" + applyphone2 + "','" + bumenid2 + "','" + bumenname2 + "','" + fzrid2 + "','" + fzr2 + "','" + usetime2 + "','" + peoplenum2 + "','" + peopleguige2 + "','" + note2 + "','" + goodsname.Value.ToString() + "','" + goodsguige.Value.ToString() + "','" + goodsnum.Value.ToString() + "','" + goodsdanwei.Value.ToString() + "','" + goodsprice.Value.ToString() + "','" + moneyone.Value.ToString() + "','" + allmoney2 + "','JL','6','" + yctp2 + "')";

                            list_sql.Add(sqltext);
                            sqltext = "update TBOM_EAT SET STATE='6'WHERE CODE='" + code2 + "' AND TYPE='SQ'";
                            list_sql.Add(sqltext);
                        }
                    }
                }
                if (xunhuan == mm)
                {
                    sqltext = "insert into TBOM_EAT (CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,ALLMONEY,TYPE,STATE,YCTYPE)" +
                        "values('" + code2.ToString() + "','" + txt_apply2.Text.ToString() + "','" + txt_apply2_id.Text.ToString() + "','" + applydate2 + "','" + applyphone2 + "','" + bumenid2 + "','" + bumenname2 + "','" + fzrid2 + "','" + fzr2 + "','" + usetime2 + "','" + peoplenum2 + "','" + peopleguige2 + "','" + note2 + "','" + allmoney2 + "','JL','6','" + yctp2 + "')";

                    list_sql.Add(sqltext);
                    sqltext = "update TBOM_EAT SET STATE='6'WHERE CODE='" + code2 + "' AND TYPE='SQ'";
                    list_sql.Add(sqltext);
                }
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('反馈成功，等待确认！');window.close();", true);
                Response.Redirect("OM_Eat.aspx");
            }

            else if (Request.QueryString["action"] == "modjl")
            {
                sqltext = "DELETE FROM TBOM_EAT WHERE CODE='" + code2.ToString() + "' and TYPE='JL'";
                list_sql.Add(sqltext);
                foreach (RepeaterItem ri in Repeater2.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        mm++;
                        HtmlInputText goodsname = (HtmlInputText)ri.FindControl("goodsname2");
                        if (goodsname.Value.Trim() == "")
                        {
                            xunhuan++;

                        }
                        if (goodsname.Value.Trim() != "")
                        {
                            HtmlInputText goodsguige = (HtmlInputText)ri.FindControl("goodsguige2");
                            HtmlInputText goodsnum = (HtmlInputText)ri.FindControl("goodsnum2");
                            HtmlInputText goodsdanwei = (HtmlInputText)ri.FindControl("goodsdanwei2");
                            HtmlInputText goodsprice = (HtmlInputText)ri.FindControl("goodsprice2");
                            HtmlInputText moneyone = (HtmlInputText)ri.FindControl("moneyone2");
                            sqltext = "insert into TBOM_EAT (CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,GOODNAME,GOODGUIGE,GOODNUM,GOODDANWEI,GOODPRICE,GOODMONEY,ALLMONEY,TYPE,STATE,YCTYPE)" +
                                "values('" + code2.ToString() + "','" + txt_apply2.Text.ToString() + "','" + txt_apply2_id.Text.ToString()+ "','" + applydate2 + "','" + bumenid2 + "','" + bumenname2 + "','" + fzrid2 + "','" + fzr2 + "','" + usetime2 + "','" + peoplenum2 + "','" + peopleguige2 + "','" + note2 + "','" + goodsname.Value.ToString() + "','" + goodsguige.Value.ToString() + "','" + goodsnum.Value.ToString() + "','" + goodsdanwei.Value.ToString() + "','" + goodsprice.Value.ToString() + "','" + moneyone.Value.ToString() + "','" + allmoney2 + "','JL','6','" + yctp2 + "')";
                            list_sql.Add(sqltext);
                            sqltext = "update TBOM_EAT SET STATE='6'WHERE CODE='" + code2 + "' AND TYPE='SQ'";
                            list_sql.Add(sqltext);
                        }
                    }
                }
                if (xunhuan == mm)
                {
                    sqltext = "insert into TBOM_EAT (CODE,APPLYNAME,APPLYID,APPLYDATE,APPLYPHONE,BUMENID,BUMENNAME,SHENHEID,SHENHENAME,USETIME,PEOPLENUM,PEOPLEGUIGE,NOTE,ALLMONEY,TYPE,STATE,YCTYPE)" +
                        "values('" + code2.ToString() + "','" + txt_apply2.Text.ToString() + "','" + txt_apply2_id.Text.ToString() + "','" + applydate2 + "','" + applyphone2 + "','" + bumenid2 + "','" + bumenname2 + "','" + fzrid2 + "','" + fzr2 + "','" + usetime2 + "','" + peoplenum2 + "','" + peopleguige2 + "','" + note2 + "','" + allmoney2 + "','JL','6','" + yctp2 + "')";

                    list_sql.Add(sqltext);
                    sqltext = "update TBOM_EAT SET STATE='6'WHERE CODE='" + code2 + "' AND TYPE='SQ'";
                    list_sql.Add(sqltext);
                }
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('重新反馈成功，等待确认！');window.close();", true);
                Response.Redirect("OM_Eat.aspx");
            }
            else if (Request.QueryString["action"] == "viewjl")
            {

                #region 一级确认
                if (Session["UserID"].ToString() == txt_fankuirenid.Value.ToString())
                {
                    //string YIJIAN = rblfirst.SelectedItem.Text.ToString() + ',' + first_opinion.Text.Trim();
                    sqltext = "update TBOM_EAT SET STATE='" + rblfirst2.SelectedValue.Trim() + "',SHENHENOTE='" + shenhenote3.Text.ToString() + "' WHERE CODE='" + danhao.Text.Trim() + "' and TYPE='JL'";
                    list_sql.Add(sqltext);
                    sqltext = "update TBOM_EAT SET STATE='" + rblfirst2.SelectedValue.Trim() + "' WHERE CODE='" + danhao.Text.ToString() + "' AND TYPE='SQ'";
                    list_sql.Add(sqltext);

                }

                #endregion
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('确认成功');window.opener=null;window.open('','_self');window.close();window.returnValue='refresh'", true);
                //return;
                Response.Redirect("OM_Eat.aspx");
            }

        }
        protected void btnReturn2_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            Response.Redirect("OM_Eat.aspx");
        }
        #endregion
    }
}
