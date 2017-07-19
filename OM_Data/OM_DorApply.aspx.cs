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
    public partial class OM_DorApply : System.Web.UI.Page
    {
        string sqltext = "";
        string id = "";
        string status = "";//审核状态
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] == "add")
                {
                    GetCode();
                    lblDorAddtime.Text = DateTime.Now.ToString();
                    rblfirst.Enabled = false;
                    first_opinion.Enabled = false;
                    txt_second.Enabled = false;
                    rblsecond.Enabled = false;
                    second_opinion.Enabled = false;
                    txt_third.Enabled = false;
                    rblthird.Enabled = false;
                    third_opinion.Enabled = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                }
                else if (Request.QueryString["action"] == "update")
                {
                    bind_info();
                    GetMPVerifyData();
                    btnBack.Visible = true;
                    chk.Visible = false;
                    rblfirst.Enabled = false;
                    first_opinion.Enabled = false;
                    txt_second.Enabled = false;
                    rblsecond.Enabled = false;
                    second_opinion.Enabled = false;
                    txt_third.Enabled = false;
                    rblthird.Enabled = false;
                    third_opinion.Enabled = false;
                }
                else
                {
                    GetMPVerifyData();
                    bind_info();
                    hlSelect0.Visible = false;
                    chk.Visible = false;
                    txtshr.Enabled = false;
                    txtWno.Enabled = false;
                    txtDep.Enabled = false;
                    txtPos.Enabled = false;
                    txtRea.Enabled = false;
                    InDate.Disabled = true;
                    txtRoom.Enabled = false;
                }
            }
        }
        private void GetCode()
        {
            sqltext = "select TOP 1 dbo.GetIndex(DORCODE) AS TopIndex from TBOM_DORAPPLY ORDER BY dbo.GetIndex(DORCODE) DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            lblDorCode.Text = "DORM" + code.PadLeft(4, '0');
        }
        private bool AuditPowerCheck()
        {
            bool retVal = true;
            if (Request.QueryString["id"] != null)
            {
                if (lblStatus.Text == "2")
                {
                    if (Session["UserName"].ToString() != txt_first.Text.Trim())
                    {
                        retVal = false;
                    }
                }
                else if (lblStatus.Text == "4")
                {
                    if (Session["UserName"].ToString() != txt_second.Text.Trim())
                    {
                        retVal = false;
                    }
                }
                else if (lblStatus.Text == "6")
                {
                    if (Session["UserName"].ToString() != txt_third.Text.Trim())
                    {
                        retVal = false;
                    }
                }
            }
            return retVal;
        }
        public void bind_info()
        {
            sqltext = "select DORCODE,DORNAME,DORWNO,DORDEP,DORPOS,DORREA,DORROOM,DORINDATE,DORADDTIME from TBOM_DORAPPLY where DORCODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {
                lblDorCode.Text = dr["DORCODE"].ToString();
                txtshr.Text = dr["DORNAME"].ToString();
                txtWno.Text = dr["DORWNO"].ToString();
                txtDep.Text = dr["DORDEP"].ToString();
                txtPos.Text = dr["DORPOS"].ToString();
                txtRea.Text = dr["DORREA"].ToString();
                txtRoom.Text = dr["DORROOM"].ToString();
                InDate.Value = dr["DORINDATE"].ToString();
                lblDorAddtime.Text = dr["DORADDTIME"].ToString();
            }
            dr.Close();
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string name = txtshr.Text.Trim();
            string wno = txtWno.Text.Trim();
            string dep = txtDep.Text.Trim();
            string pos = txtPos.Text.Trim();
            string rea = txtRea.Text.Trim();
            string room = txtRoom.Text.Trim();
            string indate = InDate.Value;
            string addtime = lblDorAddtime.Text;
            List<string> list_sql = new List<string>();
            if (Request.QueryString["action"] == "add")
            {
                sqltext = "insert into TBOM_DORAPPLY(DORCODE,DORNAME,DORWNO,DORDEP,DORPOS,DORREA,DORROOM,DORINDATE,DORADDTIME,DORSTATE)" +
                    " values('" + lblDorCode.Text + "','" + txtshr.Text.Trim() + "','" + txtWno.Text.Trim() + "','" + txtDep.Text.Trim() + "','" + txtPos.Text.Trim() + "','" + txtRea.Text.Trim() + "','" + txtRoom.Text.Trim() + "','" + InDate.Value + "','" + addtime + "',0)";
                list_sql.Add(sqltext);
                sqltext = "insert into TBOM_DORRVW(DORCODE,MP_REVIEWA,MP_STATE,MP_CHECKLEVEL) values('" + lblDorCode.Text + "','" + txt_first.Text.Trim() + "',2,'" + rblSHJS.SelectedValue + "')";
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！');window.location.href='OM_Dor.aspx'", true);
            }
            else if (Request.QueryString["action"] == "update")
            {
                sqltext = "update TBOM_DORAPPLY set DORNAME='" + name + "',DORWNO='" + wno + "',DORDEP='" + dep + "',DORPOS='" + pos + "',DORREA='" + rea + "',DORROOM='" + room + "',DORINDATE='" + indate + "',DORADDTIME='" + addtime + "' where DORCODE='" + id + "'";
                list_sql.Add(sqltext);
                sqltext = "update TBOM_DORRVW set MP_REVIEWA='" + txt_first.Text.Trim() + "',MP_CHECKLEVEL='" + rblSHJS.SelectedValue + "' where DORCODE='" + id + "'";
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改成功，已提交！');window.location.href='OM_Dor.aspx'", true);
            }
            else if(Request.QueryString["action"] == "review")
            {
                if ((rblfirst.SelectedIndex == 1 && first_opinion.Text.Trim() == "") || (rblsecond.SelectedIndex == 1 && second_opinion.Text.Trim() == "") || (rblthird.SelectedIndex == 1 && third_opinion.Text.Trim() == ""))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您的审核意见为【不同意】,请填写意见！！！');", true);
                    return;
                }
                //List<string> list_sql = new List<string>();
                //if(rblfirst.SelectedIndex == 0)
                //{
                //    sqltext = "update TBOM_DORAPPLY set DORSHRADVC='" + first_opinion.Text.Trim() + "',DORSHRTIME='" + first_time.Text + "',DORSTATE=1 where DORCODE='" + id + "'";
                //    list_sql.Add(sqltext);
                //    sqltext = "update TBDS_STAFFINFO set ST_ZHUSU=1 where ST_ID='" + shrid.Value + "'";
                //    list_sql.Add(sqltext);
                //}
                //else if (rblfirst.SelectedIndex == 1)
                //{
                //    sqltext = "update TBOM_DORAPPLY set DORSHRADVC='" + first_opinion.Text.Trim() + "',DORSHRTIME='" + first_time.Text + "',DORSTATE=2 where DORCODE='" + id + "'";
                //    list_sql.Add(sqltext);
                //    sqltext = "update TBDS_STAFFINFO set ST_ZHUSU=0 where ST_ID='" + shrid.Value + "'";
                //    list_sql.Add(sqltext);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有审批，不能提交！');", true);
                //    return;
                //}
                //DBCallCommon.ExecuteTrans(list_sql);
                //Response.Redirect("OM_Dor.aspx");
                if (this.AuditPowerCheck())
                {
                    status = lblStatus.Text;
                    int checklevel = Convert.ToInt16(rblSHJS.SelectedValue);
                    if (this.CheckSelect(checklevel))
                    {
                        string reviewdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        switch (status)
                        {
                            case "2":
                                #region 一级审核状态
                                if (checklevel == 1)
                                {
                                    if (rblfirst.SelectedIndex == 0)//同意-审核通过
                                    {
                                        sqltext = "update TBOM_DORRVW set MP_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MP_REVIEWATIME='" + reviewdate + "',MP_STATE=8 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBOM_DORAPPLY set DORSTATE=2 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBDS_STAFFINFO set ST_ZHUSU=1 where ST_ID='" + shrid.Value + "'";
                                        list_sql.Add(sqltext);
                                    }
                                    else if (rblfirst.SelectedIndex == 1)//一级驳回
                                    {
                                        sqltext = "update TBOM_DORRVW set MP_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MP_REVIEWATIME='" + reviewdate + "',MP_STATE=3 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBOM_DORAPPLY set DORSTATE=3 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBDS_STAFFINFO set ST_ZHUSU=0 where ST_ID='" + shrid.Value + "'";
                                        list_sql.Add(sqltext);
                                    }
                                }
                                else if (checklevel > 1)
                                {
                                    if (rblfirst.SelectedIndex == 0)//同意-继续下推审核
                                    {
                                        sqltext = "update TBOM_DORRVW set MP_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MP_REVIEWATIME='" + reviewdate + "',MP_REVIEWB='" + txt_second.Text.Trim() + "',MP_STATE=4 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBOM_DORAPPLY set DORSTATE=1 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                    }
                                    else if (rblfirst.SelectedIndex == 1)//一级驳回
                                    {
                                        sqltext = "update TBOM_DORRVW set MP_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MP_REVIEWATIME='" + reviewdate + "',MP_STATE=3 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBOM_DORAPPLY set DORSTATE=3 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                    }
                                }
                                break;
                                #endregion
                            case "4":
                                #region 二级审核状态
                                if (checklevel == 2)
                                {
                                    if (rblsecond.SelectedIndex == 0)//通过
                                    {
                                        sqltext = "update TBOM_DORRVW set MP_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MP_REVIEWBTIME='" + reviewdate + "',MP_STATE=8 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBOM_DORAPPLY set DORSTATE=2 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBDS_STAFFINFO set ST_ZHUSU=1 where ST_ID='" + shrid.Value + "'";
                                        list_sql.Add(sqltext);
                                    }
                                    else if (rblsecond.SelectedIndex == 1)//驳回
                                    {
                                        sqltext = "update TBOM_DORRVW set MP_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MP_REVIEWBTIME='" + reviewdate + "',MP_STATE=5 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBOM_DORAPPLY set DORSTATE=3 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBDS_STAFFINFO set ST_ZHUSU=0 where ST_ID='" + shrid.Value + "'";
                                        list_sql.Add(sqltext);
                                    }
                                }
                                else if (checklevel > 2)
                                {
                                    if (rblsecond.SelectedIndex == 0)//同意-继续下推审核
                                    {
                                        sqltext = "update TBOM_DORRVW set MP_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MP_REVIEWBTIME='" + reviewdate + "',MP_REVIEWC='" + txt_third.Text + "',MP_STATE=6 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBOM_DORAPPLY set DORSTATE=1 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                    }
                                    else if (rblsecond.SelectedIndex == 1)//二级驳回
                                    {
                                        sqltext = "update TBOM_DORRVW set MP_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MP_REVIEWBTIME='" + reviewdate + "',MP_STATE=5 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                        sqltext = "update TBOM_DORAPPLY set DORSTATE=3 where DORCODE='" + id + "'";
                                        list_sql.Add(sqltext);
                                    }
                                }
                                break;
                                #endregion
                            case "6":
                                #region 三级审核状态
                                if (rblthird.SelectedIndex == 0)//通过
                                {
                                    sqltext = "update TBOM_DORRVW set MP_REVIEWCADVC='" + third_opinion.Text.Trim() + "',MP_REVIEWCTIME='" + reviewdate + "',MP_STATE=8 where DORCODE='" + id + "'";
                                    list_sql.Add(sqltext);
                                    sqltext = "update TBOM_DORAPPLY set DORSTATE=2 where DORCODE='" + id + "'";
                                    list_sql.Add(sqltext);
                                    sqltext = "update TBDS_STAFFINFO set ST_ZHUSU=1 where ST_ID='" + shrid.Value + "'";
                                    list_sql.Add(sqltext);
                                }
                                else if (rblthird.SelectedIndex == 1)//三级驳回
                                {
                                    sqltext = "update TBOM_DORRVW set MP_REVIEWCADVC='" + third_opinion.Text.Trim() + "',MP_REVIEWCTIME='" + reviewdate + "',MP_STATE=7 where DORCODE='" + id + "'";
                                    list_sql.Add(sqltext);
                                    sqltext = "update TBOM_DORAPPLY set DORSTATE=3 where DORCODE='" + id + "'";
                                    list_sql.Add(sqltext);
                                    sqltext = "update TBDS_STAFFINFO set ST_ZHUSU=0 where ST_ID='" + shrid.Value + "'";
                                    list_sql.Add(sqltext);
                                }
                                break;
                                #endregion
                            default: break;
                        }
                        DBCallCommon.ExecuteTrans(list_sql);
                        Response.Redirect("OM_Dor.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您已完成审核或无权审核该任务！！！');", true);
                    return;
                }
            }
        }
        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            sqltext = "update TBOM_DORAPPLY set DORSTATE=4 where DORCODE='" + lblDorCode.Text + "'";
            DBCallCommon.ExeSqlText(sqltext);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已退宿！');window.location.href='OM_Dor.aspx'", true);
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_Dor.aspx");
        }

        protected void txtshr_TextChanged(object sender, EventArgs e)
        {
            sqltext = "select ST_WORKNO,DEP_NAME,ST_POSITION from View_OMstaff where ST_ID='" + shrid.Value + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {
                txtWno.Text = dr["ST_WORKNO"].ToString();
                txtDep.Text = dr["DEP_NAME"].ToString();
                txtPos.Text = dr["ST_POSITION"].ToString();
            }
            dr.Close();
        }

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (chk.Checked)
            {
                hlSelect0.Visible = false;
            }
            else
            {
                hlSelect0.Visible = true;
            }
        }
        private void GetMPVerifyData()
        {
            string level = "";//审核级数
            sqltext = "select MP_REVIEWA,MP_REVIEWAADVC,MP_REVIEWATIME,MP_REVIEWB,";//4
            sqltext += "MP_REVIEWBADVC,MP_REVIEWBTIME,MP_REVIEWC,MP_REVIEWCADVC,";//4
            sqltext += "MP_REVIEWCTIME,MP_STATE,MP_CHECKLEVEL from TBOM_DORRVW where DORCODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);

            if (dr.HasRows)
            {
                #region 读取数据
                dr.Read();
                txt_first.Text = dr["MP_REVIEWA"].ToString();
                first_opinion.Text = dr["MP_REVIEWAADVC"].ToString();
                first_time.Text = dr["MP_REVIEWATIME"].ToString();
                txt_second.Text = dr["MP_REVIEWB"].ToString();
                second_opinion.Text = dr["MP_REVIEWBADVC"].ToString();
                second_time.Text = dr["MP_REVIEWBTIME"].ToString();
                txt_third.Text = dr["MP_REVIEWC"].ToString();
                third_opinion.Text = dr["MP_REVIEWCADVC"].ToString();
                third_time.Text = dr["MP_REVIEWCTIME"].ToString();
                status = dr["MP_STATE"].ToString();//当前批材料计划的审核状态
                lblStatus.Text = status;
                this.RblOfThreeStateConfirm(status, dr["MP_CHECKLEVEL"].ToString());
                level = dr["MP_CHECKLEVEL"].ToString();//审核级数
                rblSHJS.SelectedIndex = Convert.ToInt16(dr["MP_CHECKLEVEL"].ToString()) - 1;
                #endregion

                #region 状态判断
                //if (Request.QueryString["id"] != null)  //查看进入(针对提交人)
                //{
                //    if ((status == "1" || status == "0") && level == "3")//未指定审核人
                //    {
                //        rblSHJS.SelectedIndex = 2; //默认三级审核
                //        plandate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //        txt_plandate.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");

                //        hlSelect1.Visible = true;
                //        first_opinion.Enabled = false;
                //        rblfirst.Enabled = false;
                //        txt_first.Enabled = false;

                //        hlSelect2.Visible = false;
                //        second_opinion.Enabled = false;
                //        rblsecond.Enabled = false;
                //        txt_second.Enabled = false;

                //        hlSelect3.Visible = false;
                //        third_opinion.Enabled = false;
                //        rblthird.Enabled = false;
                //        txt_third.Enabled = false;

                //        txtBZ.Enabled = true;//备注
                //        txtBZ.BorderStyle = BorderStyle.Solid;

                //    }
                //    else
                //    {
                //        txtBZ.ReadOnly = true;//备注
                //        txtBZ.BorderStyle = BorderStyle.None;

                //        rblSHJS.Enabled = false;
                //        btnsubmit.Visible = false;

                //        hlSelect1.Visible = false;
                //        first_opinion.Enabled = false;
                //        rblfirst.Enabled = false;
                //        txt_first.Enabled = false;

                //        hlSelect2.Visible = false;
                //        second_opinion.Enabled = false;
                //        rblsecond.Enabled = false;
                //        txt_second.Enabled = false;

                //        hlSelect3.Visible = false;
                //        third_opinion.Enabled = false;
                //        rblthird.Enabled = false;
                //        txt_third.Enabled = false;
                //    }
                //}

                if (Request.QueryString["id"] != null)//审核人进入
                {
                    rblSHJS.Enabled = false;
                    if (status == "2")//一级审核
                    {
                        if (level == "1")
                        {
                            hlSelect1.Visible = false;
                            first_opinion.Enabled = true;
                            rblfirst.Enabled = true;
                            txt_first.Enabled = true;

                            hlSelect2.Visible = false;
                            second_opinion.Enabled = false;
                            rblsecond.Enabled = false;
                            txt_second.Enabled = false;

                            hlSelect3.Visible = false;
                            third_opinion.Enabled = false;
                            rblthird.Enabled = false;
                            txt_third.Enabled = false;
                        }
                        else
                        {
                            hlSelect1.Visible = false;
                            first_opinion.Enabled = true;
                            rblfirst.Enabled = true;
                            txt_first.Enabled = true;

                            hlSelect2.Visible = true;
                            second_opinion.Enabled = false;
                            rblsecond.Enabled = false;
                            txt_second.Enabled = true;

                            hlSelect3.Visible = false;
                            third_opinion.Enabled = false;
                            rblthird.Enabled = false;
                            txt_third.Enabled = false;
                        }
                    }
                    else if (status == "4")//一级审核通过，二级审核
                    {
                        if (level == "2")
                        {
                            hlSelect1.Visible = false;
                            first_opinion.Enabled = false;
                            rblfirst.Enabled = false;
                            txt_first.Enabled = false;

                            hlSelect2.Visible = false;
                            second_opinion.Enabled = true;
                            rblsecond.Enabled = true;
                            txt_second.Enabled = true;

                            hlSelect3.Visible = false;
                            third_opinion.Enabled = false;
                            rblthird.Enabled = false;
                            txt_third.Enabled = false;
                        }
                        else
                        {
                            hlSelect1.Visible = false;
                            first_opinion.Enabled = false;
                            rblfirst.Enabled = false;
                            txt_first.Enabled = false;

                            hlSelect2.Visible = false;
                            second_opinion.Enabled = true;
                            rblsecond.Enabled = true;
                            txt_second.Enabled = true;

                            hlSelect3.Visible = true;
                            third_opinion.Enabled = false;
                            rblthird.Enabled = false;
                            txt_third.Enabled = true;
                        }
                    }
                    else if (status == "6")//二级审核通过，三级审核
                    {
                        hlSelect1.Visible = false;
                        first_opinion.Enabled = false;
                        rblfirst.Enabled = false;
                        txt_first.Enabled = false;

                        hlSelect2.Visible = false;
                        second_opinion.Enabled = false;
                        rblsecond.Enabled = false;
                        txt_second.Enabled = false;

                        hlSelect3.Visible = false;
                        third_opinion.Enabled = true;
                        rblthird.Enabled = true;
                        txt_third.Enabled = true;
                    }
                    else
                    {
                        btnSubmit.Visible = false;

                        hlSelect1.Visible = false;
                        first_opinion.Enabled = false;
                        rblfirst.Enabled = false;
                        txt_first.Enabled = false;

                        hlSelect2.Visible = false;
                        second_opinion.Enabled = false;
                        rblsecond.Enabled = false;
                        txt_second.Enabled = false;

                        hlSelect3.Visible = false;
                        third_opinion.Enabled = false;
                        rblthird.Enabled = false;
                        txt_third.Enabled = false;
                    }
                }
                #endregion
            }
            dr.Close();
        }
        private void RblOfThreeStateConfirm(string state, string level)
        {
            if (level != "0")
            {
                switch (state)
                {
                    case "2":
                        rblfirst.SelectedIndex = -1;
                        rblsecond.SelectedIndex = -1;
                        rblthird.SelectedIndex = -1;
                        break;
                    case "3":
                        rblfirst.SelectedIndex = 1;
                        rblsecond.SelectedIndex = -1;
                        rblthird.SelectedIndex = -1;
                        break;
                    case "4":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = -1;
                        rblthird.SelectedIndex = -1;
                        break;
                    case "5":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 1;
                        rblthird.SelectedIndex = -1;
                        break;
                    case "6":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 0;
                        rblthird.SelectedIndex = -1;
                        break;
                    case "7":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 0;
                        rblthird.SelectedIndex = 1;
                        break;
                    case "8":
                        if (level == "1")
                        {
                            rblfirst.SelectedIndex = 0;
                            rblsecond.SelectedIndex = -1;
                            rblthird.SelectedIndex = -1;
                        }
                        else if (level == "2")
                        {
                            rblfirst.SelectedIndex = 0;
                            rblsecond.SelectedIndex = 0;
                            rblthird.SelectedIndex = -1;
                        }
                        else if (level == "3")
                        {
                            rblfirst.SelectedIndex = 0;
                            rblsecond.SelectedIndex = 0;
                            rblthird.SelectedIndex = 0;
                        }
                        break;
                    case "0":
                    case "1":
                    default: break;
                }
            }
        }
        private bool CheckSelect(int level)
        {
            bool ret = false;
            if (level == 1)
            {
                if (rblfirst.SelectedIndex == 0 || rblfirst.SelectedIndex == 1)//一级审核
                {
                    ret = true;
                }
            }
            else if (level == 2)
            {
                if (status == "2")//二级审核正在进行第一级审核
                {
                    if (rblfirst.SelectedIndex == 1)//一级审核不同意
                    {
                        ret = true;
                    }
                    else if (rblfirst.SelectedIndex == 0)//一级审核同意
                    {
                        if (secondid.Value != "")//指定二级审核人
                        {
                            ret = true;
                        }
                    }
                }
                else if (status == "4")//二级审核正在进行第二级审核
                {
                    if (rblsecond.SelectedIndex == 0 || rblsecond.SelectedIndex == 1)
                    {
                        ret = true;
                    }
                }
            }
            else if (level == 3)
            {
                if (status == "2")//三级审核正在进行第一级审核
                {
                    if (rblfirst.SelectedIndex == 1)//一级审核不同意
                    {
                        ret = true;
                    }
                    else if (rblfirst.SelectedIndex == 0)//一级审核同意
                    {
                        if (secondid.Value != "")//指定二级审核人
                        {
                            ret = true;
                        }
                    }
                }
                else if (status == "4")//三级审核正在进行第二级审核
                {
                    if (rblsecond.SelectedIndex == 1)//二级审核不同意
                    {
                        ret = true;
                    }
                    else if (rblsecond.SelectedIndex == 0)//二级审核同意
                    {
                        if (thirdid.Value != "")//指定三级审核人
                        {
                            ret = true;
                        }
                    }
                }
                else if (status == "6") //三级审核正在进行三级审核
                {
                    if (rblthird.SelectedIndex == 0 || rblthird.SelectedIndex == 1)
                    {
                        ret = true;
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 一级审核意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblfirst_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblfirst.SelectedIndex == 0)
            {
                hlSelect2.Visible = true;
                first_opinion.Text = "同意";
            }
            else
            {
                hlSelect2.Visible = false;
                first_opinion.Text = "";
            }
        }
        /// <summary>
        /// 二级审核意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblsecond_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblsecond.SelectedIndex == 0)
            {
                hlSelect3.Visible = true;
                second_opinion.Text = "同意";
            }
            else
            {
                hlSelect3.Visible = false;
                second_opinion.Text = "";
            }

        }
        /// <summary>
        /// 三级审核意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblthird_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblthird.SelectedIndex == 0)
            {
                third_opinion.Text = "同意";
            }
            else
            {
                third_opinion.Text = "";
            }
        }
    }
}