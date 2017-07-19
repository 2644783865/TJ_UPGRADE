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
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_FGdzcBaofei_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.action = Request.QueryString["action"];
                asd.id = Request.QueryString["id"];
                asd.userid = Session["UserID"].ToString();
                asd.username = Session["UserName"].ToString();
                binddetail();
                bindshenhe();

            }
        }
        private class asd
        {
            public static string id;
            public static string userid;
            public static string username;
            public static string action;
        }

        //审批明细数据绑定
        private void binddetail()
        {
            string sql = "";
            txtSpbh.Text = GetSPBH();
            if (asd.action == "add")
                sql = "select * from TBOM_GDZCIN  where ID in (" + asd.id.Substring(0, asd.id.Length - 1) + ") and BFSPBH is NULL";
            else
                sql = "select * from TBOM_GDZCIN  where BFSPBH='" + asd.id.ToString() + "'or BFBHBH='" + asd.id.ToString() + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptFGDZCBF.DataSource = dt;
            rptFGDZCBF.DataBind();
        }

        //审批数据绑定
        private void bindshenhe()
        {
            if (asd.action == "add")
            {
                lbZDR.Text = asd.username;
                hidZDRID.Value = asd.userid;
                lbZD_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtSpbh.Text = GetSPBH();
            }
            else
            {
                string sql = "select SPBH,SPJB,ZDR_ID,ZDR_Name,ZD_Time,ZDR_Note,SHR1_ID,SHR1_Name,SHR1_JL,SHR1_Time,SHR1_Note,isnull(SHR2_ID,'') as SHR2_ID,isnull(SHR2_Name,'') as SHR2_Name,isnull(SHR2_Time,'') as SHR2_Time,isnull(SHR2_Note,'') as SHR2_Note,ISNULL(SHR2_JL,'') as SHR2_JL from OM_FGDZCBFSP where SPBH='" + asd.id + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    if (asd.action == "alter")
                    {
                        txtSpbh.Text = dr["SPBH"].ToString();
                        hidZDRID.Value = dr["ZDR_ID"].ToString();
                        lbZDR.Text = dr["ZDR_Name"].ToString();
                        txtZDR_JY.Text = dr["ZDR_Note"].ToString();
                        lbZD_SJ.Text = dr["ZD_Time"].ToString();
                        txtSPR1.Text = dr["SHR1_Name"].ToString();
                        txtSPR2.Text = dr["SHR2_Name"].ToString();
                        if (dr["SPJB"].ToString() == "1")
                        {
                            if (rblSPJB.SelectedValue == "2")
                                rblSPJB.SelectedValue = "2";
                            else
                                rblSPJB.SelectedValue = "1";
                        }
                        if (dr["SPJB"].ToString() == "2")
                        {
                            if (rblSPJB.SelectedValue == "1")
                                rblSPJB.SelectedValue = "1";
                            else
                                rblSPJB.SelectedValue = "2";
                        }
                    }
                    else
                    {
                        rblSPJB.SelectedValue = dr["SPJB"].ToString();
                        txtSpbh.Text = dr["SPBH"].ToString();
                        lbZDR.Text = dr["ZDR_Name"].ToString();
                        txtZDR_JY.Text = dr["ZDR_Note"].ToString();
                        lbZD_SJ.Text = dr["ZD_Time"].ToString();
                        hidSPR1ID.Value = dr["SHR1_ID"].ToString();
                        txtSPR1.Text = dr["SHR1_Name"].ToString();
                        rblSPR1_JL.SelectedValue = dr["SHR1_JL"].ToString();
                        lbSPR1_SJ.Text = dr["SHR1_Time"].ToString();
                        txtSPR1_JY.Text = dr["SHR1_Note"].ToString();
                        if (rblSPJB.SelectedValue == "2")
                        {
                            hidSPR2ID.Value = dr["SHR2_ID"].ToString();
                            txtSPR2.Text = dr["SHR2_Name"].ToString();
                            rblSPR2_JL.SelectedValue = dr["SHR2_JL"].ToString();
                            lbSPR2_SJ.Text = dr["SHR2_Time"].ToString();
                            txtSPR2_JY.Text = dr["SHR2_Note"].ToString();
                        }
                    }
                }
            }
            PowerControl();
        }

        //修改审批级别
        protected void rblSPJB_onchange(object sender, EventArgs e)
        {
            binddetail();
            bindshenhe();
        }

        //控件可用性和可见性
        private void PowerControl()
        {
            SPR1.Visible = false;
            SPR2.Visible = false;
            if (rblSPJB.SelectedValue == "1")
            {
                SPR1.Visible = true;
            }
            if (rblSPJB.SelectedValue == "2")
            {
                SPR1.Visible = true;
                SPR2.Visible = true;
            }
            if (asd.action == "add" || asd.action == "alter")
            {
                panShenhe.Enabled = true;
                txtZDR_JY.Enabled = true;
                rblSPJB.Enabled = true;
                hlSelect1.Visible = true;
                hlSelect2.Visible = true;
                btnSave.Visible = true;
            }
            if (asd.action == "check")
            {
                panShenhe.Enabled = true;
                if (asd.userid == hidSPR1ID.Value.Trim())
                {
                    rblSPR1_JL.Enabled = true;
                    txtSPR1_JY.Enabled = true;
                }
                if (asd.userid == hidSPR2ID.Value.Trim())
                {
                    rblSPR2_JL.Enabled = true;
                    txtSPR2_JY.Enabled = true;
                }
                btnSubmit.Visible = true;
            }
        }

        //绑定审批编号
        protected string GetSPBH()
        {
            string SPBH = "";
            string sql = "select max(SPBH) as SPBH from OM_FGDZCBFSP";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "")
            {
                SPBH = "F-BF" + DateTime.Now.ToString("yyMMdd") + "-0001";
            }
            else
            {
                SPBH = "F-BF" + DateTime.Now.ToString("yyMMdd") + "-" + (CommonFun.ComTryInt(dt.Rows[0][0].ToString().Split('-')[2]) + 1).ToString().PadLeft(4, '0');
            }
            return SPBH;
        }

        //验证保存条件
        protected int SaveValid()
        {
            int a = 0;
            if (asd.userid == hidZDRID.Value.Trim())
            {
                if (rblSPJB.SelectedValue == "")
                {
                    a = 1;
                }
                else if ((rblSPJB.SelectedValue == "1" && txtSPR1.Text == "") || (rblSPJB.SelectedValue == "1" && hidSPR1ID.Value == ""))
                {
                    a = 1;
                }
                else if (rblSPJB.SelectedValue == "2")
                {
                    if (txtSPR1.Text == "" || hidSPR1ID.Value == "" || txtSPR2.Text == "" || hidSPR2ID.Value == "")
                    {
                        a = 1;
                    }
                }
            }
            else if (rblSPJB.SelectedValue == "1")
            {
                if ((asd.userid == hidSPR1ID.Value.Trim() && rblSPR1_JL.SelectedValue == ""))
                {
                    a = 2;
                }
            }
            else if (rblSPJB.SelectedValue == "2")
            {
                if ((asd.userid == hidSPR1ID.Value.Trim() && rblSPR1_JL.SelectedValue == "") || (asd.userid == hidSPR2ID.Value.Trim() && rblSPR2_JL.SelectedValue == ""))
                {
                    a = 2;
                }
            }
            return a;
        }

        //添加数据集合
        private List<string> addlist()
        {
            List<string> list = new List<string>();
            string sqlText = "";
            string sql = "";
            if (rblSPJB.SelectedValue == "1")
            {
                sqlText = "insert into OM_FGDZCBFSP(SPBH,SC_Num,SPJB,ZDR_ID,ZDR_Name,ZDR_Note,ZD_Time,SHR1_ID,SHR1_Name,SPZT) values('" + txtSpbh.Text.Trim() + "','" + rptFGDZCBF.Items.Count.ToString() + "','" + rblSPJB.SelectedValue.Trim() + "','" + hidZDRID.Value.Trim() + "','" + lbZDR.Text.Trim() + "', '" + txtZDR_JY.Text.Trim() + "','" + lbZD_SJ.Text.Trim() + "','" + hidSPR1ID.Value.Trim() + "','" + txtSPR1.Text.Trim() + "','0')";
                list.Add(sqlText);
            }
            else
            {
                sqlText = "insert into OM_FGDZCBFSP(SPBH,SC_Num,SPJB,ZDR_ID,ZDR_Name,ZDR_Note,ZD_Time,SHR1_ID,SHR1_Name,SHR2_ID,SHR2_Name,SPZT) values('" + txtSpbh.Text.Trim() + "','" + rptFGDZCBF.Items.Count.ToString() + "','" + rblSPJB.SelectedValue.Trim() + "','" + hidZDRID.Value.Trim() + "','" + lbZDR.Text.Trim() + "', '" + txtZDR_JY.Text.Trim() + "','" + lbZD_SJ.Text.Trim() + "','" + hidSPR1ID.Value.Trim() + "','" + txtSPR1.Text.Trim() + "','" + hidSPR2ID.Value.Trim() + "','" + txtSPR2.Text.Trim() + "','0')";
                list.Add(sqlText);
            }
            sql = "update TBOM_GDZCIN set BFSPBH='" + txtSpbh.Text.Trim() + "'where ID in (" + asd.id.Substring(0, asd.id.Length - 1) + ") and BFSPBH is NULL";
            list.Add(sql);
            return list;
        }

        //修改数据集合
        private List<string> alterlist()
        {
            List<string> list = new List<string>();
            string sqlText = "";
            lbZD_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (rblSPJB.SelectedValue == "1")
            {
                sqlText = "update OM_FGDZCBFSP set SPJB='" + rblSPJB.SelectedValue.Trim() + "', ZDR_Note='" + txtZDR_JY.Text.Trim() + "',ZD_Time='" + lbZD_SJ.Text.Trim() + "',SHR1_ID='" + hidSPR1ID.Value.Trim() + "',SHR1_Name='" + txtSPR1.Text.Trim() + "',SHR2_ID=NULL,SHR2_Name=NULL where SPBH='" + txtSpbh.Text.Trim() + "'";
            }
            else
            {
                sqlText = "update OM_FGDZCBFSP set SPJB='" + rblSPJB.SelectedValue.Trim() + "', ZDR_Note='" + txtZDR_JY.Text.Trim() + "',ZD_Time='" + lbZD_SJ.Text.Trim() + "',SHR1_ID='" + hidSPR1ID.Value.Trim() + "',SHR1_Name='" + txtSPR1.Text.Trim() + "',SHR2_ID='" + hidSPR2ID.Value.Trim() + "',SHR2_Name='" + txtSPR2.Text.Trim() + "' where SPBH='" + txtSpbh.Text.Trim() + "'";
            }
            list.Add(sqlText);
            return list;
        }

        //审核数据集合
        private List<string> checklist()
        {
            List<string> list = new List<string>();
            string sqlText = "";
            string sql = "";
            string sql1 = "";
            lbSPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lbSPR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (rblSPJB.SelectedValue == "1")
            {
                sqlText = "update OM_FGDZCBFSP set SHR1_Time='" + lbSPR1_SJ.Text.Trim() + "',SHR1_JL='" + rblSPR1_JL.Text.Trim() + "',SHR1_Note='" + txtSPR1_JY.Text.Trim() + "' where SPBH='" + txtSpbh.Text.Trim() + "'";
                list.Add(sqlText);
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = "update OM_FGDZCBFSP set SPZT='3'where SPBH='" + txtSpbh.Text.Trim() + "'";
                    list.Add(sql);
                    sql1 = "update TBOM_GDZCIN set BFZT='1'where BFSPBH ='" + txtSpbh.Text.Trim() + "'";
                    list.Add(sql1);
                }
                else
                {
                    sql = "update OM_FGDZCBFSP set SPZT='4'where SPBH='" + txtSpbh.Text.Trim() + "'";
                    list.Add(sql);
                    sql1 = "update TBOM_GDZCIN set BFBHBH='" + txtSpbh.Text.Trim() + "',BFSPBH=NULL where BFSPBH='" + txtSpbh.Text.Trim() + "'";
                    list.Add(sql1);
                }
            }
            else
            {
                if (asd.userid == hidSPR1ID.Value.Trim())
                {
                    lbSPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sqlText = "update OM_FGDZCBFSP set SHR1_Time='" + lbSPR1_SJ.Text.Trim() + "',SHR1_JL='" + rblSPR1_JL.Text.Trim() + "',SHR1_Note='" + txtSPR1_JY.Text.Trim() + "' where SPBH='" + txtSpbh.Text.Trim() + "'";
                    list.Add(sqlText);
                    if (rblSPR1_JL.SelectedValue == "y")
                    {
                        sql = "update OM_FGDZCBFSP set SPZT='2'where SPBH='" + txtSpbh.Text.Trim() + "'";
                        list.Add(sql);
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR2ID.Value);
                        string _body = "非固定资产报废审批任务:"
                               + "\r\n制单人：" + lbZDR.Text.Trim()
                               + "\r\n制单日期：" + lbZD_SJ.Text.Trim();

                        string _subject = "您有新的【非固定资产报废】需要审批，请及时处理";
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                    else
                    {
                        sql = "update OM_FGDZCBFSP set SPZT='4'where SPBH='" + txtSpbh.Text.Trim() + "'";
                        list.Add(sql);
                        sql1 = "update TBOM_GDZCIN set BFBHBH='" + txtSpbh.Text.Trim() + "',BFSPBH=NULL where BFSPBH='" + txtSpbh.Text.Trim() + "'";
                        list.Add(sql1);
                    }
                }
                else
                {
                    sqlText = "update OM_FGDZCBFSP set SHR2_Time='" + lbSPR2_SJ.Text.Trim() + "',SHR2_JL='" + rblSPR2_JL.Text.Trim() + "',SHR2_Note='" + txtSPR2_JY.Text.Trim() + "' where SPBH='" + txtSpbh.Text.Trim() + "'";
                    list.Add(sqlText);
                    if (rblSPR2_JL.SelectedValue == "y")
                    {
                        sql = "update OM_FGDZCBFSP set SPZT='3'where SPBH='" + txtSpbh.Text.Trim() + "'";
                        list.Add(sql);
                        sql1 = "update TBOM_GDZCIN set BFZT='1'where BFSPBH ='" + txtSpbh.Text.Trim() + "'";
                        list.Add(sql1);
                    }
                    else
                    {
                        sql = "update OM_FGDZCBFSP set SPZT='4'where SPBH='" + txtSpbh.Text.Trim() + "'";
                        list.Add(sql);
                        sql1 = "update TBOM_GDZCIN set BFBHBH='" + txtSpbh.Text.Trim() + "',BFSPBH=NULL where BFSPBH='" + txtSpbh.Text.Trim() + "'";
                        list.Add(sql1);
                    }
                }
            }
            return list;
        }

        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int a = SaveValid();
            if (a == 1)
            {
                Response.Write("<script>alert('请选择审批人后再提交审批！');</script>");
                return;
            }
            if (asd.action == "add")
            {
                List<string> list = addlist();
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Write("<script>alert('保存成功！');</script>");
                        rblSPJB.Enabled = false;
                        txtZDR_JY.Enabled = false;
                        btnSave.Visible = false;
                        btnSubmit.Visible = true;
                    }
                    catch
                    {
                        Response.Write("<script>alert('addlist数据失败，请联系管理员！');</script>");
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('addlist数据失败，请联系管理员！');", true);
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('addlist没有数据！');</script>");
                    return;
                }
            }
            if (asd.action == "alter")
            {
                List<string> list = alterlist();
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Write("<script>alert('修改成功！');</script>");
                        rblSPJB.Enabled = false;
                        txtZDR_JY.Enabled = false;
                        btnSave.Visible = false;
                        btnSubmit.Visible = true;
                    }
                    catch
                    {
                        Response.Write("<script>alert('alterlist数据失败，请联系管理员！');</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('alterlist没有数据！');</script>");
                    return;
                }
            }
        }

        //提交审核状态改变
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int a = SaveValid();
            if (a == 2)
            {
                Response.Write("<script>alert('请选择同意或不同意！');</script>");
                return;
            }
            string sqlText = "";
            if (asd.action == "add" || asd.action == "alter")
            {
                List<string> list = new List<string>();
                sqlText = "update OM_FGDZCBFSP set SPZT='1',ZD_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where SPBH='" + txtSpbh.Text.Trim() + "'";
                list.Add(sqlText);
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR1ID.Value);
                        string _body = "非固定资产报废审批任务:"
                             + "\r\n制单人：" + lbZDR.Text.Trim()
                             + "\r\n制单日期：" + lbZD_SJ.Text.Trim();

                        string _subject = "您有新的【非固定资产报废】需要审批，请及时处理";
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        if (asd.action == "add")
                            Response.Write("<script>window.close()</script>");
                        else
                            Response.Redirect("OM_FGdzcBaofeiSP.aspx");
                    }
                    catch
                    {
                        Response.Write("<script>alert('提交审批出现问题，请联系管理员！！');</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('list没有数据！');</script>");
                    return;
                }
            }
            if (asd.action == "check")
            {
                List<string> list = checklist();
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Redirect("OM_FGdzcBaofeiSP.aspx");
                    }
                    catch
                    {
                        Response.Write("<script>alert('checklist数据失败，请联系管理员！');</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('list没有数据！');</script>");
                    return;
                }
            }
        }

        //返回
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (asd.action == "add" || asd.action == "read")
                Response.Write("<script>window.close()</script>");
            else
                Response.Redirect("OM_FGdzcBaofeiSP.aspx");
        }
    }
}
