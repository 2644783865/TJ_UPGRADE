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
    public partial class OM_TravelApplyDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            asd.action = Request.QueryString["action"];
            asd.key = Request.QueryString["key"];
            asd.userid = Session["UserID"].ToString();
            asd.username = Session["UserName"].ToString();

            if (!IsPostBack)
            {
                binddetail();
                bindshenhe();
                if (asd.action == "add")
                    CreateNewRow(5);
            }
            PowerControl();
        }

        private class asd
        {
            public static string key;
            public static string userid;
            public static string username;
            public static string action;
        }

        //审批明细数据绑定
        private void binddetail()
        {
            string sql = "";
            string sql0 = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql0);
            if (asd.action == "add")
            {
                lbTA_Code.Text = GetCode();
            }
            else
            {
                sql = "select * from OM_TravelApplyDetail  where TA_Code ='" + asd.key + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                rptTravelDetail.DataSource = dt;
                rptTravelDetail.DataBind();
            }
        }

        //审批数据绑定
        private void bindshenhe()
        {
            string sql0 = "select DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE in (select ST_DEPID from TBDS_STAFFINFO where ST_ID='" + asd.userid + "')";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql0);
            lbTA_ZDRDep.Text = dt0.Rows[0][0].ToString();
            HidTA_ZDRDepID.Value = dt0.Rows[0][1].ToString();
            if (asd.action == "add")
            {
                lbTA_ZDR.Text = asd.username;
                HidTA_ZDRID.Value = asd.userid;
            }
            else
            {
                string sql = "select TA_IsSure,TA_Code,TA_ZDR,TA_ZDRID,TA_ZDTime,isnull(TA_SureTime,'')TA_SureTime,TA_SHLevel,TA_SHRA,TA_SHRIDA,TA_SHJLA,TA_SHYJA,TA_SHTimeA,isnull(TA_SHRB,'')TA_SHRB,isnull(TA_SHRIDB,'')TA_SHRIDB,isnull(TA_SHJLB,'')TA_SHJLB, isnull(TA_SHYJB,'')TA_SHYJB,isnull(TA_SHTimeB,'')TA_SHTimeB,isnull(TA_SHRC,'')TA_SHRC,isnull(TA_SHRIDC,'')TA_SHRIDC,isnull(TA_SHJLC,'')TA_SHJLC,isnull(TA_SHYJC,'')TA_SHYJC,isnull(TA_SHTimeC,'')TA_SHTimeC,TA_State from OM_TravelApply where TA_Code='" + asd.key + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    if (dr["TA_IsSure"].ToString() == "1")
                        chkSure.Checked = true;
                    else
                        chkSure.Checked = false;
                    if (asd.action != "edit")
                        rblSPJB.SelectedValue = dr["TA_SHLevel"].ToString();
                    lbTA_Code.Text = dr["TA_Code"].ToString();
                    lbTA_ZDR.Text = dr["TA_ZDR"].ToString();
                    HidTA_ZDRID.Value = dr["TA_ZDRID"].ToString();
                    lbTA_ZDTime.Text = dr["TA_ZDTime"].ToString();
                    lbTA_SureTime.Text = dr["TA_SureTime"].ToString();
                    txt_first.Text = dr["TA_SHRA"].ToString();
                    firstid.Value = dr["TA_SHRIDA"].ToString();

                    if ((dr["TA_State"].ToString() == "1" || dr["TA_State"].ToString() == "0") && dr["TA_SHLevel"].ToString() != "1")
                    {
                        txt_second.Text = dr["TA_SHRB"].ToString();
                        secondid.Value = dr["TA_SHRIDB"].ToString();
                        if (dr["TA_SHLevel"].ToString() == "3")
                        {
                            txt_third.Text = dr["TA_SHRC"].ToString();
                            thirdid.Value = dr["TA_SHRIDC"].ToString();
                        }
                    }
                    if (dr["TA_State"].ToString() != "1" && dr["TA_State"].ToString() != "0")
                    {
                        rblResult1.SelectedValue = dr["TA_SHJLA"].ToString();
                        first_time.Text = dr["TA_SHTimeA"].ToString();
                        first_opinion.Text = dr["TA_SHYJA"].ToString();
                        if (dr["TA_SHLevel"].ToString() != "1")
                        {
                            txt_second.Text = dr["TA_SHRB"].ToString();
                            secondid.Value = dr["TA_SHRIDB"].ToString();

                            if (dr["TA_SHLevel"].ToString() == "2")
                            {
                                if (dr["TA_State"].ToString() != "2")
                                {
                                    if (dr["TA_SHJLB"].ToString() != "")
                                        rblResult2.SelectedValue = dr["TA_SHJLB"].ToString();
                                    second_time.Text = dr["TA_SHTimeB"].ToString();
                                    second_opinion.Text = dr["TA_SHYJB"].ToString();
                                }
                            }
                            else
                            {
                                txt_third.Text = dr["TA_SHRC"].ToString();
                                thirdid.Value = dr["TA_SHRIDC"].ToString();
                                if (dr["TA_State"].ToString() != "2")
                                {
                                    txt_second.Text = dr["TA_SHRB"].ToString();
                                    secondid.Value = dr["TA_SHRIDB"].ToString();
                                    if (dr["TA_SHJLB"].ToString() != "")
                                        rblResult2.SelectedValue = dr["TA_SHJLB"].ToString();
                                    second_time.Text = dr["TA_SHTimeB"].ToString();
                                    second_opinion.Text = dr["TA_SHYJB"].ToString();
                                    if (dr["TA_State"].ToString() != "3")
                                    {
                                        txt_third.Text = dr["TA_SHRC"].ToString();
                                        thirdid.Value = dr["TA_SHRIDC"].ToString();
                                        if (dr["TA_SHJLC"].ToString() != "")
                                            rblResult3.SelectedValue = dr["TA_SHJLC"].ToString();
                                        third_time.Text = dr["TA_SHTimeC"].ToString();
                                        third_opinion.Text = dr["TA_SHYJC"].ToString();

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //修改审批级别
        protected void rblSPJB_onchange(object sender, EventArgs e)
        {
            DataTable dtnew = this.GetDataTable();
            binddetail();
            bindshenhe();
            PowerControl();
            this.rptTravelDetail.DataSource = dtnew;
            this.rptTravelDetail.DataBind();
        }

        //控件可用性和可见性
        private void PowerControl()
        {
            btnSave.Visible = false;
            btnSubmit.Visible = false;
            PanelDe.Enabled = false;
            PanelSH.Enabled = false;
            btnDelete.Visible = false;
            btnAdd.Visible = false;
            if (rblSPJB.SelectedValue == "1")
            {
                Panel3.Visible = false;
                Panel4.Visible = false;
            }
            if (rblSPJB.SelectedValue == "2")
            {
                Panel3.Visible = true;
                Panel4.Visible = false;
            }
            if (rblSPJB.SelectedValue == "3")
            {
                Panel3.Visible = true;
                Panel4.Visible = true;
            }
            if (asd.action == "add" || asd.action == "edit")
            {
                PanelDe.Enabled = true;
                PanelSH.Enabled = true;
                btnAdd.Visible = true;
                btnDelete.Visible = true;
                rblResult1.Enabled = false;
                rblResult2.Enabled = false;
                rblResult3.Enabled = false;
                first_opinion.Enabled = false;
                second_opinion.Enabled = false;
                third_opinion.Enabled = false;
                btnSave.Visible = true;
            }
            else if (asd.action == "audit")
            {
                btnSubmit.Visible = true;
                PanelSH.Enabled = true;
                rblSPJB.Enabled = false;
                hlSelect1.Visible = false;
                hlSelect2.Visible = false;
                hlSelect3.Visible = false;
                if (asd.userid == firstid.Value.Trim())
                {
                    rblResult2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblResult3.Enabled = false;
                    third_opinion.Enabled = false;
                }
                if (asd.userid == secondid.Value.Trim())
                {
                    rblResult1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblResult3.Enabled = false;
                    third_opinion.Enabled = false;
                }
                if (asd.userid == thirdid.Value.Trim())
                {
                    rblResult1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblResult2.Enabled = false;
                    second_opinion.Enabled = false;
                }
            }
            if (asd.action == "sure")
            {
                btnSave.Visible = true;
                PanelDe.Enabled = true;
                txtNum.Enabled = false;
            }
            if (rptTravelDetail.Items.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }

        //绑定申请编号
        protected string GetCode()
        {
            string TA_code = "";
            string sql = "select max(TA_code) as TA_code from OM_TravelApply";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "")
            {
                TA_code = "TA_SQ" + DateTime.Now.ToString("yyMMdd") + "00001";
            }
            else
            {
                TA_code = "TA_SQ" + DateTime.Now.ToString("yyMMdd") + (CommonFun.ComTryInt(dt.Rows[0][0].ToString().Substring(11, 5)) + 1).ToString().PadLeft(5, '0');
            }
            return TA_code;
        }

        //增加、删除行
        # region
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TA_SQR");
            dt.Columns.Add("TA_SQRID");
            dt.Columns.Add("TA_SQRDep");
            dt.Columns.Add("TA_SQRDepPos");
            dt.Columns.Add("TA_StartTimePlan");
            dt.Columns.Add("TA_EndTimePlan");
            dt.Columns.Add("TA_DaysPlan");
            dt.Columns.Add("TA_Event");
            dt.Columns.Add("TA_Place");
            dt.Columns.Add("TA_StartTimeReal");
            dt.Columns.Add("TA_EndTimeReal");
            dt.Columns.Add("TA_DaysReal");
            dt.Columns.Add("TA_Note");
            foreach (RepeaterItem retItem in rptTravelDetail.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("txtTA_SQR")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("txtTA_SQRID")).Text;
                    newRow[2] = ((Label)retItem.FindControl("txtTA_SQRDep")).Text;
                    newRow[3] = ((Label)retItem.FindControl("txtTA_SQRDepPos")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("txtTA_StartTimePlan")).Text;
                    newRow[5] = ((TextBox)retItem.FindControl("txtTA_EndTimePlan")).Text;
                    newRow[6] = ((TextBox)retItem.FindControl("txtTA_DaysPlan")).Text;
                    newRow[7] = ((TextBox)retItem.FindControl("txtTA_Event")).Text;
                    newRow[8] = ((TextBox)retItem.FindControl("txtTA_Place")).Text;
                    newRow[9] = ((TextBox)retItem.FindControl("txtTA_StartTimeReal")).Text;
                    newRow[10] = ((TextBox)retItem.FindControl("txtTA_EndTimeReal")).Text;
                    newRow[11] = ((TextBox)retItem.FindControl("txtTA_DaysReal")).Text;
                    newRow[12] = ((TextBox)retItem.FindControl("txtTA_Note")).Text;

                    dt.Rows.Add(newRow);
                }
            }
            this.rptTravelDetail.DataSource = dt;
            this.rptTravelDetail.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
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
                dt.Rows.Add(newRow);
            }
            this.rptTravelDetail.DataSource = dt;
            this.rptTravelDetail.DataBind();
            PowerControl();
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TA_SQR");
            dt.Columns.Add("TA_SQRID");
            dt.Columns.Add("TA_SQRDep");
            dt.Columns.Add("TA_SQRDepPos");
            dt.Columns.Add("TA_StartTimePlan");
            dt.Columns.Add("TA_EndTimePlan");
            dt.Columns.Add("TA_DaysPlan");
            dt.Columns.Add("TA_Event");
            dt.Columns.Add("TA_Place");
            dt.Columns.Add("TA_StartTimeReal");
            dt.Columns.Add("TA_EndTimeReal");
            dt.Columns.Add("TA_DaysReal");
            dt.Columns.Add("TA_Note");
            foreach (RepeaterItem retItem in rptTravelDetail.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("txtTA_SQR")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("txtTA_SQRID")).Text;
                newRow[2] = ((Label)retItem.FindControl("txtTA_SQRDep")).Text;
                newRow[3] = ((Label)retItem.FindControl("txtTA_SQRDepPos")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("txtTA_StartTimePlan")).Text;
                newRow[5] = ((TextBox)retItem.FindControl("txtTA_EndTimePlan")).Text;
                newRow[6] = ((TextBox)retItem.FindControl("txtTA_DaysPlan")).Text;
                newRow[7] = ((TextBox)retItem.FindControl("txtTA_Event")).Text;
                newRow[8] = ((TextBox)retItem.FindControl("txtTA_Place")).Text;
                newRow[9] = ((TextBox)retItem.FindControl("txtTA_StartTimeReal")).Text;
                newRow[10] = ((TextBox)retItem.FindControl("txtTA_EndTimeReal")).Text;
                newRow[11] = ((TextBox)retItem.FindControl("txtTA_DaysReal")).Text;
                newRow[12] = ((TextBox)retItem.FindControl("txtTA_Note")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        #endregion

        protected void SQRName_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_newstid = (TextBox)sender;
            RepeaterItem Reitem = (RepeaterItem)Tb_newstid.Parent;

            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    ((TextBox)Reitem.FindControl("txtTA_SQRID")).Text = stid;
                    ((TextBox)Reitem.FindControl("txtTA_SQR")).Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    ((Label)Reitem.FindControl("txtTA_SQRDep")).Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                    ((Label)Reitem.FindControl("txtTA_SQRDepPos")).Text = dt.Rows[0]["DEP_POSITION"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }
            }
        }

        protected void rptTravelDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtTA_SQR = (TextBox)e.Item.FindControl("txtTA_SQR");
                TextBox txtTA_StartTimePlan = (TextBox)e.Item.FindControl("txtTA_StartTimePlan");
                TextBox txtTA_EndTimePlan = (TextBox)e.Item.FindControl("txtTA_EndTimePlan");
                TextBox txtTA_DaysPlan = (TextBox)e.Item.FindControl("txtTA_DaysPlan");
                TextBox txtTA_Event = (TextBox)e.Item.FindControl("txtTA_Event");
                TextBox txtTA_Place = (TextBox)e.Item.FindControl("txtTA_Place");
                TextBox txtTA_StartTimeReal = (TextBox)e.Item.FindControl("txtTA_StartTimeReal");
                TextBox txtTA_EndTimeReal = (TextBox)e.Item.FindControl("txtTA_EndTimeReal");
                TextBox txtTA_DaysReal = (TextBox)e.Item.FindControl("txtTA_DaysReal");
                TextBox txtTA_Note = (TextBox)e.Item.FindControl("txtTA_Note");


                txtTA_SQR.Enabled = true;
                txtTA_StartTimePlan.Enabled = true;
                txtTA_EndTimePlan.Enabled = false;
                txtTA_DaysPlan.Enabled = true;
                txtTA_Event.Enabled = true;
                txtTA_Place.Enabled = true;
                txtTA_StartTimeReal.Enabled = false;
                txtTA_EndTimeReal.Enabled = false;
                txtTA_DaysReal.Enabled = false;
                txtTA_Note.Enabled = true;

                if (asd.action != "add" && asd.action != "edit")
                {
                    txtTA_SQR.Enabled = false;
                    txtTA_StartTimePlan.Enabled = false;
                    txtTA_DaysPlan.Enabled = false;
                    txtTA_Event.Enabled = false;
                    txtTA_Place.Enabled = false;
                    txtTA_Note.Enabled = false;
                    if (asd.action == "sure")
                    {
                        txtTA_StartTimeReal.Enabled = true;
                        txtTA_DaysReal.Enabled = true;
                        txtTA_Note.Enabled = true;
                    }
                }
            }
        }

        //添加数据集合
        private List<string> addlist()
        {

            List<string> list = new List<string>();
            string sqlText = "";
            string sql = "";
            string sqlkey = "";
            string value = "";
            if (rblSPJB.SelectedValue == "1")
            {
                sqlkey = "TA_SHRIDA,TA_SHRA";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "'";
            }
            else if (rblSPJB.SelectedValue == "2")
            {
                sqlkey = "TA_SHRIDA,TA_SHRA,TA_SHRIDB,TA_SHRB";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_second.Text.Trim() + "'";
            }
            else
            {
                sqlkey = "TA_SHRIDA,TA_SHRA,TA_SHRIDB,TA_SHRB,TA_SHRIDC,TA_SHRC";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_second.Text.Trim() + "','" + thirdid.Value.Trim() + "','" + txt_third.Text.Trim() + "'";
            }
            for (int i = 0; i < rptTravelDetail.Items.Count; i++)
            {
                TextBox txtTA_SQR = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_SQR");
                TextBox txtTA_SQRID = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_SQRID");
                Label txtTA_SQRDep = (Label)rptTravelDetail.Items[i].FindControl("txtTA_SQRDep");
                Label txtTA_SQRDepPos = (Label)rptTravelDetail.Items[i].FindControl("txtTA_SQRDepPos");
                if (txtTA_SQRID.Text.Trim() != "")
                {
                    TextBox txtTA_StartTimePlan = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_StartTimePlan");
                    TextBox txtTA_EndTimePlan = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_EndTimePlan");
                    TextBox txtTA_DaysPlan = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_DaysPlan");
                    TextBox txtTA_Event = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_Event");
                    TextBox txtTA_Place = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_Place");
                    TextBox txtTA_Note = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_Note");

                    sql = "insert into OM_TravelApplyDetail(TA_Code,TA_SQR,TA_SQRID,TA_SQRDep,TA_SQRDepPos,TA_StartTimePlan,TA_EndTimePlan,TA_DaysPlan,TA_Event,TA_Place,TA_Note,TA_State)values('" + lbTA_Code.Text.Trim() + "','" + txtTA_SQR.Text.Trim() + "','" + txtTA_SQRID.Text.Trim() + "','" + txtTA_SQRDep.Text.Trim() + "','" + txtTA_SQRDepPos.Text.Trim() + "','" + txtTA_StartTimePlan.Text.Trim() + "','" + txtTA_EndTimePlan.Text.Trim() + "','" + txtTA_DaysPlan.Text.Trim() + "','" + txtTA_Event.Text.Trim() + "','" + txtTA_Place.Text.Trim() + "','" + txtTA_Note.Text.Trim() + "','0')";
                    list.Add(sql);
                }
            }
            string TA_IsSure = "";
            if (chkSure.Checked == true)
                TA_IsSure = "1";
            sqlText = "insert into OM_TravelApply(TA_IsSure,TA_Code,TA_ZDR,TA_ZDRID,TA_ZDTime,TA_SHLevel,TA_State," + sqlkey + ") values('" + TA_IsSure + "','" + lbTA_Code.Text.Trim() + "','" + lbTA_ZDR.Text.Trim() + "','" + HidTA_ZDRID.Value.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + rblSPJB.SelectedValue.Trim() + "','0'," + value + ")";
            list.Add(sqlText);
            return list;
        }

        //修改数据集合
        private List<string> editlist()
        {
            List<string> list = new List<string>();
            string sqlText = "";
            string sql = "";
            string sqlkey = "";
            string value = "";
            if (rblSPJB.SelectedValue == "1")
            {
                sqlkey = "TA_SHRIDA,TA_SHRA";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "'";
            }
            else if (rblSPJB.SelectedValue == "2")
            {
                sqlkey = "TA_SHRIDA,TA_SHRA,TA_SHRIDB,TA_SHRB";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_second.Text.Trim() + "'";
            }
            else
            {
                sqlkey = "TA_SHRIDA,TA_SHRA,TA_SHRIDB,TA_SHRB,TA_SHRIDC,TA_SHRC";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_second.Text.Trim() + "','" + thirdid.Value.Trim() + "','" + txt_third.Text.Trim() + "'";
            }
            string sql0 = "delete from OM_TravelApply where TA_Code='" + lbTA_Code.Text.Trim() + "'";
            list.Add(sql0);
            string sql1 = "delete from OM_TravelApplyDetail where TA_Code='" + lbTA_Code.Text.Trim() + "'";
            list.Add(sql1);
            for (int i = 0; i < rptTravelDetail.Items.Count; i++)
            {
                TextBox txtTA_SQR = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_SQR");
                TextBox txtTA_SQRID = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_SQRID");
                Label txtTA_SQRDep = (Label)rptTravelDetail.Items[i].FindControl("txtTA_SQRDep");
                Label txtTA_SQRDepPos = (Label)rptTravelDetail.Items[i].FindControl("txtTA_SQRDepPos");
                if (txtTA_SQRID.Text.Trim() != "")
                {
                    TextBox txtTA_StartTimePlan = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_StartTimePlan");
                    TextBox txtTA_EndTimePlan = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_EndTimePlan");
                    TextBox txtTA_DaysPlan = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_DaysPlan");
                    TextBox txtTA_Event = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_Event");
                    TextBox txtTA_Place = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_Place");
                    TextBox txtTA_Note = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_Note");

                    sql = "insert into OM_TravelApplyDetail(TA_Code,TA_SQR,TA_SQRID,TA_SQRDep,TA_SQRDepPos,TA_StartTimePlan,TA_EndTimePlan,TA_DaysPlan,TA_Event,TA_Place,TA_Note,TA_State)values('" + lbTA_Code.Text.Trim() + "','" + txtTA_SQR.Text.Trim() + "','" + txtTA_SQRID.Text.Trim() + "','" + txtTA_SQRDep.Text.Trim() + "','" + txtTA_SQRDepPos.Text.Trim() + "','" + txtTA_StartTimePlan.Text.Trim() + "','" + txtTA_EndTimePlan.Text.Trim() + "','" + txtTA_DaysPlan.Text.Trim() + "','" + txtTA_Event.Text.Trim() + "','" + txtTA_Place.Text.Trim() + "','" + txtTA_Note.Text.Trim() + "','0')";
                    list.Add(sql);
                }
            }

            string TA_IsSure = "";
            if (chkSure.Checked == true)
                TA_IsSure = "1";
            sqlText = "insert into OM_TravelApply(TA_IsSure,TA_Code,TA_ZDR,TA_ZDRID,TA_ZDTime,TA_SHLevel,TA_State," + sqlkey + ") values('" + TA_IsSure + "','" + lbTA_Code.Text.Trim() + "','" + lbTA_ZDR.Text.Trim() + "','" + HidTA_ZDRID.Value.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + rblSPJB.SelectedValue.Trim() + "','0'," + value + ")";
            list.Add(sqlText);
            return list;
        }

        //审核数据集合
        private List<string> auditlist()
        {
            List<string> list = new List<string>();
            string sqlText = "";
            string sql = "";
            string sql0 = "";
            string t_state = "";
            first_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            second_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            third_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (rblResult1.SelectedValue == "1" || rblResult2.SelectedValue == "1" || rblResult3.SelectedValue == "1")
            {
                t_state = "5";
            }
            else
            {
                if (rblSPJB.SelectedValue == "1" && rblResult1.SelectedValue == "0")
                {
                    t_state = "4";
                }
                if (rblSPJB.SelectedValue != "1")
                {
                    if (rblResult1.SelectedValue == "0" && rblResult2.SelectedValue != "0" && rblResult2.SelectedValue != "1")
                    {
                        t_state = "2";
                    }
                    if (rblResult2.SelectedValue == "0")
                    {
                        t_state = "4";
                    }
                    if (rblSPJB.SelectedValue == "3")
                    {
                        if (rblResult2.SelectedValue == "0" && rblResult3.SelectedValue != "0" && rblResult3.SelectedValue != "1")
                        {
                            t_state = "3";
                        }
                        if (rblResult3.SelectedValue == "0")
                        {
                            t_state = "4";
                        }
                    }
                }
            }
            if (asd.userid == firstid.Value)
            {
                sqlText = "update OM_TravelApply set TA_SHTimeA='" + first_time.Text.Trim() + "',TA_SHJLA='" + rblResult1.SelectedValue.Trim() + "',TA_SHYJA='" + first_opinion.Text.Trim() + "',TA_State='" + t_state + "' where TA_Code='" + lbTA_Code.Text.Trim() + "'";
                list.Add(sqlText);
            }

            if (asd.userid == secondid.Value)
            {
                sql0 = "update OM_TravelApply set TA_SHTimeB='" + second_time.Text.Trim() + "',TA_SHJLB='" + rblResult2.SelectedValue.Trim() + "',TA_SHYJB='" + second_opinion.Text.Trim() + "',TA_State='" + t_state + "' where TA_Code='" + lbTA_Code.Text.Trim() + "'";
                list.Add(sql0);
            }
            if (asd.userid == thirdid.Value)
            {
                sql0 = "update OM_TravelApply set TA_SHTimeC='" + third_time.Text.Trim() + "',TA_SHJLC='" + rblResult3.SelectedValue.Trim() + "',TA_SHYJC='" + third_opinion.Text.Trim() + "',TA_State='" + t_state + "' where TA_Code='" + lbTA_Code.Text.Trim() + "'";
                list.Add(sql0);
            }
            sql = "update OM_TravelApplyDetail set TA_State='" + t_state + "' where TA_Code='" + lbTA_Code.Text.Trim() + "'";
            list.Add(sql);
            return list;
        }

        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int m = 0;
            for (int i = 0; i < rptTravelDetail.Items.Count; i++)
            {
                TextBox txtTA_SQRID = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_SQRID");
                if (txtTA_SQRID.Text.Trim() != "")
                {
                    m++;
                    TextBox txtTA_StartTimePlan = rptTravelDetail.Items[i].FindControl("txtTA_StartTimePlan") as TextBox;
                    TextBox txtTA_DaysPlan = rptTravelDetail.Items[i].FindControl("txtTA_DaysPlan") as TextBox;
                    TextBox txtTA_StartTimeReal = rptTravelDetail.Items[i].FindControl("txtTA_StartTimeReal") as TextBox;
                    TextBox txtTA_DaysReal = rptTravelDetail.Items[i].FindControl("txtTA_DaysReal") as TextBox;
                    if (((asd.action == "add" || asd.action == "edit") && (string.IsNullOrEmpty(txtTA_StartTimePlan.Text.ToString().Trim()) || string.IsNullOrEmpty(txtTA_DaysPlan.Text.ToString().Trim()))) || (asd.action == "sure" && (string.IsNullOrEmpty(txtTA_StartTimeReal.Text.ToString().Trim()) || string.IsNullOrEmpty(txtTA_DaysReal.Text.ToString().Trim()))))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写完整出差时间和天数！');", true);
                        return;
                    }
                }
            }
            if (m == 0)
            {
                Response.Write("<script>alert('请填写至少一行出差人员信息！');</script>");
                return;
            }
            List<string> list = new List<string>();
            if (asd.action == "add")
            {
                list = addlist();
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Write("<script>alert('保存成功！');</script>");
                        btnSave.Visible = false;
                        btnSubmit.Visible = true;
                        rblSPJB.Visible = false;
                        btnAdd.Visible = false;
                        btnDelete.Visible = false;
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
            if (asd.action == "edit")
            {
                list = editlist();
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Write("<script>alert('修改成功！');</script>");
                        btnSave.Visible = false;
                        btnSubmit.Visible = true;
                        rblSPJB.Visible = false;
                        btnAdd.Visible = false;
                        btnDelete.Visible = false;
                    }
                    catch
                    {
                        Response.Write("<script>alert('editlist数据失败，请联系管理员！');</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('editlist没有数据！');</script>");
                    return;
                }
            }
            if (asd.action == "sure")
            {
                string sql = "";
                string sql0 = "";
                string sql1 = "";
                for (int i = 0; i < rptTravelDetail.Items.Count; i++)
                {
                    TextBox txtTA_SQRID = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_SQRID");
                    TextBox txtTA_StartTimeReal = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_StartTimeReal");
                    TextBox txtTA_EndTimeReal = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_EndTimeReal");
                    TextBox txtTA_DaysReal = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_DaysReal");
                    TextBox txtTA_Note = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_Note");

                    sql = "update OM_TravelApplyDetail set TA_StartTimeReal='" + txtTA_StartTimeReal.Text.ToString().Trim() + "',TA_EndTimeReal='" + txtTA_EndTimeReal.Text.ToString().Trim() + "',TA_DaysReal='" + txtTA_DaysReal.Text.ToString().Trim() + "',TA_Note='" + txtTA_Note.Text.ToString().Trim() + "' where TA_Code ='" + asd.key + "'and TA_SQRID='" + txtTA_SQRID.Text.ToString().Trim() + "'";
                    list.Add(sql);
                }
                if (list != null)
                {
                    sql0 = "update OM_TravelApplyDetail set TA_State='6' where TA_Code ='" + asd.key + "'";
                    list.Add(sql0);
                    sql1 = "update OM_TravelApply set TA_State='6', TA_SureTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where TA_Code ='" + asd.key + "'";
                    list.Add(sql1);
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Write("<script>alert('出差已确认！');</script>");
                    }
                    catch
                    {
                        Response.Write("<script>alert('确认出现问题，请联系管理员！');</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('没有数据！');</script>");
                    return;
                }
            }
        }

        //提交审核状态改变
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sqlText = "";
            string sql = "";
            List<string> list = new List<string>();
            if (asd.action == "add" || asd.action == "edit")
            {
                sqlText = "update OM_TravelApply set TA_State='1',TA_ZDTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'where TA_Code ='" + lbTA_Code.Text.Trim() + "'";
                list.Add(sqlText);
                sql = "update OM_TravelApplyDetail set TA_State='1'where TA_Code ='" + lbTA_Code.Text.Trim() + "'";
                list.Add(sql);
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        //邮件提醒
                        string sqladd = "select TA_SHRIDA from OM_TravelApply where TA_State='1'and TA_Code ='" + lbTA_Code.Text.Trim() + "'";
                        DataTable dtadd = DBCallCommon.GetDTUsingSqlText(sqladd);
                        if (dtadd.Rows.Count > 0)
                        {
                            string _emailto = DBCallCommon.GetEmailAddressByUserID(dtadd.Rows[0]["TA_SHRIDA"].ToString());
                            string _body = "差旅申请审批任务:"
                                  + "\r\n申请单号：" + lbTA_Code.Text.Trim()
                                  + "\r\n制单人：" + lbTA_ZDR.Text.Trim()
                                  + "\r\n制单日期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            string _subject = "您有新的【差旅申请】需要审批，请及时处理:" + lbTA_Code.Text.Trim();
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        Response.Redirect("OM_TravelApply.aspx");
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
            if (asd.action == "audit")
            {
                list = auditlist();
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        List<string> listnew = new List<string>();
                        string sqlnew = "";
                        string sqlnewT = "";
                        if (chkSure.Checked == true && (rblResult3.SelectedValue == "0" || (rblSPJB.SelectedValue == "2" && rblResult2.SelectedValue == "0") || (rblSPJB.SelectedValue == "1" && rblResult1.SelectedValue == "0")))
                        {
                            for (int i = 0; i < rptTravelDetail.Items.Count; i++)
                            {
                                TextBox txtTA_SQRID = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_SQRID");
                                if (txtTA_SQRID.Text.Trim() != "")
                                {
                                    TextBox txtTA_StartTimePlan = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_StartTimePlan");
                                    TextBox txtTA_EndTimePlan = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_EndTimePlan");
                                    TextBox txtTA_DaysPlan = (TextBox)rptTravelDetail.Items[i].FindControl("txtTA_DaysPlan");

                                    sqlnew = "update OM_TravelApplyDetail set TA_StartTimeReal='" + txtTA_StartTimePlan.Text.ToString().Trim() + "',TA_EndTimeReal='" + txtTA_EndTimePlan.Text.ToString().Trim() + "',TA_DaysReal='" + txtTA_DaysPlan.Text.ToString().Trim() + "',TA_State='6' where TA_Code ='" + asd.key + "'and TA_SQRID='" + txtTA_SQRID.Text.ToString().Trim() + "'";
                                    listnew.Add(sqlnew);
                                }
                            }
                            sqlnewT = "update OM_TravelApply set TA_State='6',TA_SureTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'where TA_Code ='" + lbTA_Code.Text.Trim() + "'";
                            listnew.Add(sqlnewT);
                            DBCallCommon.ExecuteTrans(listnew);
                        }

                        //邮件提醒
                        sql = "select TA_State,TA_ZDRID,TA_SHRIDB,TA_SHRIDC from OM_TravelApply where (TA_State='2'or TA_State='3'or TA_State='4')and TA_Code ='" + lbTA_Code.Text.Trim() + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt.Rows.Count > 0)
                        {
                            string id_emailto = "";
                            if (dt.Rows[0]["TA_State"].ToString() == "2")
                                id_emailto = dt.Rows[0]["TA_SHRIDB"].ToString();
                            else if (dt.Rows[0]["TA_State"].ToString() == "3")
                                id_emailto = dt.Rows[0]["TA_SHRIDC"].ToString();
                            else
                                id_emailto = dt.Rows[0]["TA_ZDRID"].ToString();
                            string _emailto = DBCallCommon.GetEmailAddressByUserID(id_emailto);
                            string _body = "差旅申请审批任务:"
                                  + "\r\n申请单号：" + lbTA_Code.Text.Trim()
                                  + "\r\n制单人：" + lbTA_ZDR.Text.Trim()
                                  + "\r\n制单日期：" + lbTA_ZDTime.Text.Trim();

                            string _subject = "您有新的【差旅申请】需要审批，请及时处理:" + lbTA_Code.Text.Trim();
                            if (dt.Rows[0]["TA_State"].ToString() == "4")
                                _subject = "您有新的【差旅申请】需要确认，请及时处理:" + lbTA_Code.Text.Trim();
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        Response.Redirect("OM_TravelApply.aspx");
                    }
                    catch
                    {
                        Response.Write("<script>alert('auditlist数据失败，请联系管理员！');</script>");
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
    }
}
