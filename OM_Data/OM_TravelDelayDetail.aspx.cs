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
    public partial class OM_TravelDelayDetail : System.Web.UI.Page
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
            if (asd.action == "add")
            {
                lbTD_Code.Text = GetCode();
            }
            else
            {
                sql = "select * from OM_TravelDelayDetail  where TD_Code ='" + asd.key + "'order by TA_Code";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                rptTravelDelayDetail.DataSource = dt;
                rptTravelDelayDetail.DataBind();
                if (asd.action != "edit")
                    TextBoxHidden(rptTravelDelayDetail);
            }
        }

        //审批数据绑定
        private void bindshenhe()
        {
            string sql0 = "select DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE in (select ST_DEPID from TBDS_STAFFINFO where ST_ID='" + asd.userid + "')";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql0);
            lbTD_ZDRDep.Text = dt0.Rows[0][0].ToString();
            HidTD_ZDRDepID.Value = dt0.Rows[0][1].ToString();
            if (asd.action == "add")
            {
                lbTD_ZDR.Text = asd.username;
                HidTD_ZDRID.Value = asd.userid;
            }
            else
            {
                string sql = "select TD_Code,TD_ZDR,TD_ZDRID,TD_ZDTime,TD_SHLevel,TD_SHRA,TD_SHRIDA,TD_SHJLA,TD_SHYJA,TD_SHTimeA,isnull(TD_SHRB,'')TD_SHRB,isnull(TD_SHRIDB,'')TD_SHRIDB,isnull(TD_SHJLB,'')TD_SHJLB, isnull(TD_SHYJB,'')TD_SHYJB,isnull(TD_SHTimeB,'')TD_SHTimeB,isnull(TD_SHRC,'')TD_SHRC,isnull(TD_SHRIDC,'')TD_SHRIDC,isnull(TD_SHJLC,'')TD_SHJLC,isnull(TD_SHYJC,'')TD_SHYJC,isnull(TD_SHTimeC,'')TD_SHTimeC,TD_State from OM_TravelDelay where TD_Code='" + asd.key + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    if (asd.action != "edit")
                        rblSPJB.SelectedValue = dr["TD_SHLevel"].ToString();
                    lbTD_Code.Text = dr["TD_Code"].ToString();
                    lbTD_ZDR.Text = dr["TD_ZDR"].ToString();
                    HidTD_ZDRID.Value = dr["TD_ZDRID"].ToString();
                    lbTD_ZDTime.Text = dr["TD_ZDTime"].ToString();
                    txt_first.Text = dr["TD_SHRA"].ToString();
                    firstid.Value = dr["TD_SHRIDA"].ToString();

                    if ((dr["TD_State"].ToString() == "1" || dr["TD_State"].ToString() == "0") && dr["TD_SHLevel"].ToString() != "1")
                    {
                        txt_second.Text = dr["TD_SHRB"].ToString();
                        secondid.Value = dr["TD_SHRIDB"].ToString();
                        if (dr["TD_SHLevel"].ToString() == "3")
                        {
                            txt_third.Text = dr["TD_SHRC"].ToString();
                            thirdid.Value = dr["TD_SHRIDC"].ToString();
                        }
                    }
                    if (dr["TD_State"].ToString() != "1" && dr["TD_State"].ToString() != "0")
                    {
                        rblResult1.SelectedValue = dr["TD_SHJLA"].ToString();
                        first_time.Text = dr["TD_SHTimeA"].ToString();
                        first_opinion.Text = dr["TD_SHYJA"].ToString();
                        if (dr["TD_SHLevel"].ToString() != "1")
                        {
                            txt_second.Text = dr["TD_SHRB"].ToString();
                            secondid.Value = dr["TD_SHRIDB"].ToString();

                            if (dr["TD_SHLevel"].ToString() == "2")
                            {
                                if (dr["TD_State"].ToString() != "2")
                                {
                                    if (dr["TD_SHJLB"].ToString() != "")
                                        rblResult2.SelectedValue = dr["TD_SHJLB"].ToString();
                                    second_time.Text = dr["TD_SHTimeB"].ToString();
                                    second_opinion.Text = dr["TD_SHYJB"].ToString();
                                }
                            }
                            else
                            {
                                txt_third.Text = dr["TD_SHRC"].ToString();
                                thirdid.Value = dr["TD_SHRIDC"].ToString();
                                if (dr["TD_State"].ToString() != "2")
                                {
                                    txt_second.Text = dr["TD_SHRB"].ToString();
                                    secondid.Value = dr["TD_SHRIDB"].ToString();
                                    if (dr["TD_SHJLB"].ToString() != "")
                                        rblResult2.SelectedValue = dr["TD_SHJLB"].ToString();
                                    second_time.Text = dr["TD_SHTimeB"].ToString();
                                    second_opinion.Text = dr["TD_SHYJB"].ToString();
                                    if (dr["TD_State"].ToString() != "3")
                                    {
                                        txt_third.Text = dr["TD_SHRC"].ToString();
                                        thirdid.Value = dr["TD_SHRIDC"].ToString();
                                        if (dr["TD_SHJLC"].ToString() != "")
                                            rblResult3.SelectedValue = dr["TD_SHJLC"].ToString();
                                        third_time.Text = dr["TD_SHTimeC"].ToString();
                                        third_opinion.Text = dr["TD_SHYJC"].ToString();

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //合并单元格
        private void TextBoxHidden(Repeater rpt)
        {
            if (rpt.Items.Count < 2)
            {
                return;
            }
            TextBox txtT_Code = rpt.Items[0].FindControl("txtT_Code") as TextBox;
            for (int i = 1; i < rpt.Items.Count; i++)
            {
                TextBox txtT_CodeI = rpt.Items[i].FindControl("txtT_Code") as TextBox;
                if (txtT_Code.Text.ToString().Trim() == txtT_CodeI.Text.ToString().Trim())
                {
                    txtT_CodeI.Visible = false;
                }
                else
                {
                    txtT_Code = txtT_CodeI;
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
            this.rptTravelDelayDetail.DataSource = dtnew;
            this.rptTravelDelayDetail.DataBind();
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
            if (rptTravelDelayDetail.Items.Count == 0)
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
            string TD_code = "";
            string sql = "select max(TD_code) as TD_code from OM_TravelDelay";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "")
            {
                TD_code = "TD_SQ" + DateTime.Now.ToString("yyMMdd") + "00001";
            }
            else
            {
                TD_code = "TD_SQ" + DateTime.Now.ToString("yyMMdd") + (CommonFun.ComTryInt(dt.Rows[0][0].ToString().Substring(11, 5)) + 1).ToString().PadLeft(5, '0');
            }
            return TD_code;
        }

        //增加、删除行
        # region
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TA_Code");
            dt.Columns.Add("TD_SQR");
            dt.Columns.Add("TD_SQRID");
            dt.Columns.Add("TD_SQRDep");
            dt.Columns.Add("TD_SQRDepPos");
            dt.Columns.Add("TD_StartTimePlan");
            dt.Columns.Add("TD_EndTimePlan");
            dt.Columns.Add("TD_DaysPlan");
            dt.Columns.Add("TD_Event");
            dt.Columns.Add("TD_Place");
            dt.Columns.Add("TD_StartTimeReal");
            dt.Columns.Add("TD_EndTimeReal");
            dt.Columns.Add("TD_DaysReal");
            dt.Columns.Add("TD_DelayNote");
            foreach (RepeaterItem retItem in rptTravelDelayDetail.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("txtT_Code")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("txtTD_SQR")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("txtTD_SQRID")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("txtTD_SQRDep")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("txtTD_SQRDepPos")).Text;
                    newRow[5] = ((TextBox)retItem.FindControl("txtTD_StartTimePlan")).Text;
                    newRow[6] = ((TextBox)retItem.FindControl("txtTD_EndTimePlan")).Text;
                    newRow[7] = ((TextBox)retItem.FindControl("txtTD_DaysPlan")).Text;
                    newRow[8] = ((TextBox)retItem.FindControl("txtTD_Event")).Text;
                    newRow[9] = ((TextBox)retItem.FindControl("txtTD_Place")).Text;
                    newRow[10] = ((TextBox)retItem.FindControl("txtTD_StartTimeReal")).Text;
                    newRow[11] = ((TextBox)retItem.FindControl("txtTD_EndTimeReal")).Text;
                    newRow[12] = ((TextBox)retItem.FindControl("txtTD_DaysReal")).Text;
                    newRow[13] = ((TextBox)retItem.FindControl("txtTD_DelayNote")).Text;

                    dt.Rows.Add(newRow);
                }
            }
            this.rptTravelDelayDetail.DataSource = dt;
            this.rptTravelDelayDetail.DataBind();
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
            this.rptTravelDelayDetail.DataSource = dt;
            this.rptTravelDelayDetail.DataBind();
            PowerControl();
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TA_Code");
            dt.Columns.Add("TD_SQR");
            dt.Columns.Add("TD_SQRID");
            dt.Columns.Add("TD_SQRDep");
            dt.Columns.Add("TD_SQRDepPos");
            dt.Columns.Add("TD_StartTimePlan");
            dt.Columns.Add("TD_EndTimePlan");
            dt.Columns.Add("TD_DaysPlan");
            dt.Columns.Add("TD_Event");
            dt.Columns.Add("TD_Place");
            dt.Columns.Add("TD_StartTimeReal");
            dt.Columns.Add("TD_EndTimeReal");
            dt.Columns.Add("TD_DaysReal");
            dt.Columns.Add("TD_DelayNote");
            foreach (RepeaterItem retItem in rptTravelDelayDetail.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("txtT_Code")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("txtTD_SQR")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("txtTD_SQRID")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("txtTD_SQRDep")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("txtTD_SQRDepPos")).Text;
                newRow[5] = ((TextBox)retItem.FindControl("txtTD_StartTimePlan")).Text;
                newRow[6] = ((TextBox)retItem.FindControl("txtTD_EndTimePlan")).Text;
                newRow[7] = ((TextBox)retItem.FindControl("txtTD_DaysPlan")).Text;
                newRow[8] = ((TextBox)retItem.FindControl("txtTD_Event")).Text;
                newRow[9] = ((TextBox)retItem.FindControl("txtTD_Place")).Text;
                newRow[10] = ((TextBox)retItem.FindControl("txtTD_StartTimeReal")).Text;
                newRow[11] = ((TextBox)retItem.FindControl("txtTD_EndTimeReal")).Text;
                newRow[12] = ((TextBox)retItem.FindControl("txtTD_DaysReal")).Text;
                newRow[13] = ((TextBox)retItem.FindControl("txtTD_DelayNote")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        #endregion

        protected void T_Code_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_newstid = (TextBox)sender;
            RepeaterItem Reitem = (RepeaterItem)Tb_newstid.Parent;

            if (num > 0)
            {
                string tcode = (sender as TextBox).Text.Trim().Substring(0, num);
                string stid = (sender as TextBox).Text.Trim().Substring(num + 1, (sender as TextBox).Text.Trim().IndexOf("|", num + 1) - num - 1);

                string sqlText = "select a.* from OM_TravelApplyDetail as a left join OM_TravelApply as b on a.TA_Code=b.TA_Code where a.TA_Code='" + tcode + "'and TA_SQRID='" + stid + "' and (TA_DaysReal>TA_DaysPlan or TA_IsSure='1') ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    ((TextBox)Reitem.FindControl("txtT_Code")).Text = tcode;
                    ((TextBox)Reitem.FindControl("txtTD_SQRID")).Text = dt.Rows[0]["TA_SQRID"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_SQR")).Text = dt.Rows[0]["TA_SQR"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_SQRDep")).Text = dt.Rows[0]["TA_SQRDep"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_SQRDepPos")).Text = dt.Rows[0]["TA_SQRDepPos"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_StartTimePlan")).Text = dt.Rows[0]["TA_StartTimePlan"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_EndTimePlan")).Text = dt.Rows[0]["TA_EndTimePlan"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_DaysPlan")).Text = dt.Rows[0]["TA_DaysPlan"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_Event")).Text = dt.Rows[0]["TA_Event"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_Place")).Text = dt.Rows[0]["TA_Place"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_StartTimeReal")).Text = dt.Rows[0]["TA_StartTimeReal"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_EndTimeReal")).Text = dt.Rows[0]["TA_EndTimeReal"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtTD_DaysReal")).Text = dt.Rows[0]["TA_DaysReal"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('差旅延期信息不存在，请重新输入！');", true);
                }
            }
        }

        protected void rptTravelDelayDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtT_Code = (TextBox)e.Item.FindControl("txtT_Code");
                TextBox txtTD_DelayNote = (TextBox)e.Item.FindControl("txtTD_DelayNote");

                txtT_Code.Enabled = true;
                txtTD_DelayNote.Enabled = true;

                if (asd.action != "add" && asd.action != "edit")
                {
                    txtT_Code.Enabled = false;
                    txtTD_DelayNote.Enabled = false;
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
                sqlkey = "TD_SHRIDA,TD_SHRA";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "'";
            }
            else if (rblSPJB.SelectedValue == "2")
            {
                sqlkey = "TD_SHRIDA,TD_SHRA,TD_SHRIDB,TD_SHRB";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_second.Text.Trim() + "'";
            }
            else
            {
                sqlkey = "TD_SHRIDA,TD_SHRA,TD_SHRIDB,TD_SHRB,TD_SHRIDC,TD_SHRC";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_second.Text.Trim() + "','" + thirdid.Value.Trim() + "','" + txt_third.Text.Trim() + "'";
            }
            for (int i = 0; i < rptTravelDelayDetail.Items.Count; i++)
            {
                TextBox txtT_Code = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtT_Code");
                TextBox txtTD_SQR = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_SQR");
                TextBox txtTD_SQRID = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_SQRID");
                TextBox txtTD_SQRDep = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_SQRDep");
                TextBox txtTD_SQRDepPos = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_SQRDepPos");
                if (txtTD_SQRID.Text.Trim() != "")
                {
                    TextBox txtTD_StartTimePlan = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_StartTimePlan");
                    TextBox txtTD_EndTimePlan = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_EndTimePlan");
                    TextBox txtTD_DaysPlan = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_DaysPlan");
                    TextBox txtTD_Event = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_Event");
                    TextBox txtTD_Place = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_Place");
                    TextBox txtTD_StartTimeReal = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_StartTimeReal");
                    TextBox txtTD_EndTimeReal = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_EndTimeReal");
                    TextBox txtTD_DaysReal = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_DaysReal");
                    TextBox txtTD_DelayNote = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_DelayNote");

                    sql = "insert into OM_TravelDelayDetail(TD_Code,TA_Code,TD_SQR,TD_SQRID,TD_SQRDep,TD_SQRDepPos,TD_StartTimePlan,TD_EndTimePlan,TD_DaysPlan,TD_Event,TD_Place,TD_StartTimeReal,TD_EndTimeReal,TD_DaysReal,TD_DelayNote,TD_State)values('" + lbTD_Code.Text.Trim() + "','" + txtT_Code.Text.Trim() + "','" + txtTD_SQR.Text.Trim() + "','" + txtTD_SQRID.Text.Trim() + "','" + txtTD_SQRDep.Text.Trim() + "','" + txtTD_SQRDepPos.Text.Trim() + "','" + txtTD_StartTimePlan.Text.Trim() + "','" + txtTD_EndTimePlan.Text.Trim() + "','" + txtTD_DaysPlan.Text.Trim() + "','" + txtTD_Event.Text.Trim() + "','" + txtTD_Place.Text.Trim() + "','" + txtTD_StartTimeReal.Text.Trim() + "','" + txtTD_EndTimeReal.Text.Trim() + "','" + txtTD_DaysReal.Text.Trim() + "','" + txtTD_DelayNote.Text.Trim() + "','0')";
                    list.Add(sql);
                }
            }

            sqlText = "insert into OM_TravelDelay(TD_Code,TD_ZDR,TD_ZDRID,TD_ZDTime,TD_SHLevel,TD_State," + sqlkey + ") values('" + lbTD_Code.Text.Trim() + "','" + lbTD_ZDR.Text.Trim() + "','" + HidTD_ZDRID.Value.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + rblSPJB.SelectedValue.Trim() + "','0'," + value + ")";
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
                sqlkey = "TD_SHRIDA,TD_SHRA";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "'";
            }
            else if (rblSPJB.SelectedValue == "2")
            {
                sqlkey = "TD_SHRIDA,TD_SHRA,TD_SHRIDB,TD_SHRB";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_second.Text.Trim() + "'";
            }
            else
            {
                sqlkey = "TD_SHRIDA,TD_SHRA,TD_SHRIDB,TD_SHRB,TD_SHRIDC,TD_SHRC";
                value = "'" + firstid.Value.Trim() + "','" + txt_first.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_second.Text.Trim() + "','" + thirdid.Value.Trim() + "','" + txt_third.Text.Trim() + "'";
            }
            string sql0 = "delete from OM_TravelDelay where TD_Code='" + lbTD_Code.Text.Trim() + "'";
            list.Add(sql0);
            string sql1 = "delete from OM_TravelDelayDetail where TD_Code='" + lbTD_Code.Text.Trim() + "'";
            list.Add(sql1);
            for (int i = 0; i < rptTravelDelayDetail.Items.Count; i++)
            {
                TextBox txtT_Code = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtT_Code");
                TextBox txtTD_SQR = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_SQR");
                TextBox txtTD_SQRID = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_SQRID");
                TextBox txtTD_SQRDep = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_SQRDep");
                TextBox txtTD_SQRDepPos = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_SQRDepPos");
                if (txtTD_SQRID.Text.Trim() != "")
                {
                    TextBox txtTD_StartTimePlan = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_StartTimePlan");
                    TextBox txtTD_EndTimePlan = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_EndTimePlan");
                    TextBox txtTD_DaysPlan = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_DaysPlan");
                    TextBox txtTD_Event = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_Event");
                    TextBox txtTD_Place = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_Place");
                    TextBox txtTD_StartTimeReal = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_StartTimeReal");
                    TextBox txtTD_EndTimeReal = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_EndTimeReal");
                    TextBox txtTD_DaysReal = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_DaysReal");
                    TextBox txtTD_DelayNote = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_DelayNote");

                    sql = "insert into OM_TravelDelayDetail(TD_Code,TA_Code,TD_SQR,TD_SQRID,TD_SQRDep,TD_SQRDepPos,TD_StartTimePlan,TD_EndTimePlan,TD_DaysPlan,TD_Event,TD_Place,TD_StartTimeReal,TD_EndTimeReal,TD_DaysReal,TD_DelayNote,TD_State)values('" + lbTD_Code.Text.Trim() + "','" + txtT_Code.Text.Trim() + "','" + txtTD_SQR.Text.Trim() + "','" + txtTD_SQRID.Text.Trim() + "','" + txtTD_SQRDep.Text.Trim() + "','" + txtTD_SQRDepPos.Text.Trim() + "','" + txtTD_StartTimePlan.Text.Trim() + "','" + txtTD_EndTimePlan.Text.Trim() + "','" + txtTD_DaysPlan.Text.Trim() + "','" + txtTD_Event.Text.Trim() + "','" + txtTD_Place.Text.Trim() + "','" + txtTD_StartTimeReal.Text.Trim() + "','" + txtTD_EndTimeReal.Text.Trim() + "','" + txtTD_DaysReal.Text.Trim() + "','" + txtTD_DelayNote.Text.Trim() + "','0')";
                    list.Add(sql);
                }
            }

            sqlText = "insert into OM_TravelDelay(TD_Code,TD_ZDR,TD_ZDRID,TD_ZDTime,TD_SHLevel,TD_State," + sqlkey + ") values('" + lbTD_Code.Text.Trim() + "','" + lbTD_ZDR.Text.Trim() + "','" + HidTD_ZDRID.Value.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + rblSPJB.SelectedValue.Trim() + "','0'," + value + ")";
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
            string td_state = "";
            first_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            second_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            third_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (rblResult1.SelectedValue == "1" || rblResult2.SelectedValue == "1" || rblResult3.SelectedValue == "1")
            {
                td_state = "5";
            }
            else
            {
                if (rblSPJB.SelectedValue == "1" && rblResult1.SelectedValue == "0")
                {
                    td_state = "4";
                }
                if (rblSPJB.SelectedValue != "1")
                {
                    if (rblResult1.SelectedValue == "0" && rblResult2.SelectedValue != "0" && rblResult2.SelectedValue != "1")
                    {
                        td_state = "2";
                    }
                    if (rblResult2.SelectedValue == "0")
                    {
                        td_state = "4";
                    }
                    if (rblSPJB.SelectedValue == "3")
                    {
                        if (rblResult2.SelectedValue == "0" && rblResult3.SelectedValue != "0" && rblResult3.SelectedValue != "1")
                        {
                            td_state = "3";
                        }
                        if (rblResult3.SelectedValue == "0")
                        {
                            td_state = "4";
                        }
                    }
                }
            }
            if (asd.userid == firstid.Value)
            {
                sqlText = "update OM_TravelDelay set TD_SHTimeA='" + first_time.Text.Trim() + "',TD_SHJLA='" + rblResult1.SelectedValue.Trim() + "',TD_SHYJA='" + first_opinion.Text.Trim() + "',TD_State='" + td_state + "' where TD_Code='" + lbTD_Code.Text.Trim() + "'";
                list.Add(sqlText);
            }

            if (asd.userid == secondid.Value)
            {
                sql0 = "update OM_TravelDelay set TD_SHTimeB='" + second_time.Text.Trim() + "',TD_SHJLB='" + rblResult2.SelectedValue.Trim() + "',TD_SHYJB='" + second_opinion.Text.Trim() + "',TD_State='" + td_state + "' where TD_Code='" + lbTD_Code.Text.Trim() + "'";
                list.Add(sql0);
            }
            if (asd.userid == thirdid.Value)
            {
                sql0 = "update OM_TravelDelay set TD_SHTimeC='" + third_time.Text.Trim() + "',TD_SHJLC='" + rblResult3.SelectedValue.Trim() + "',TD_SHYJC='" + third_opinion.Text.Trim() + "',TD_State='" + td_state + "' where TD_Code='" + lbTD_Code.Text.Trim() + "'";
                list.Add(sql0);
            }
            sql = "update OM_TravelDelayDetail set TD_State='" + td_state + "' where TD_Code='" + lbTD_Code.Text.Trim() + "'";
            list.Add(sql);
            return list;
        }

        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
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
        }

        //提交审核状态改变
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sqlText = "";
            string sql = "";
            List<string> list = new List<string>();
            if (asd.action == "add" || asd.action == "edit")
            {
                sqlText = "update OM_TravelDelay set TD_State='1',TD_ZDTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'where TD_Code ='" + lbTD_Code.Text.Trim() + "'";
                list.Add(sqlText);
                sql = "update OM_TravelDelayDetail set TD_State='1'where TD_Code ='" + lbTD_Code.Text.Trim() + "'";
                list.Add(sql);
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        //邮件提醒
                        string sqladd = "select TD_SHRIDA from OM_TravelDelay where TD_State='1'and TD_Code ='" + lbTD_Code.Text.Trim() + "'";
                        DataTable dtadd = DBCallCommon.GetDTUsingSqlText(sqladd);
                        if (dtadd.Rows.Count > 0)
                        {
                            string _emailto = DBCallCommon.GetEmailAddressByUserID(dtadd.Rows[0]["TD_SHRIDA"].ToString());
                            string _body = "差旅延期审批任务:"
                                  + "\r\n延期单号：" + lbTD_Code.Text.Trim()
                                  + "\r\n制单人：" + lbTD_ZDR.Text.Trim()
                                  + "\r\n制单日期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            string _subject = "您有新的【差旅延期】需要审批，请及时处理:" + lbTD_Code.Text.Trim();
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        Response.Redirect("OM_TravelDelay.aspx");
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
                        List<string> listback = new List<string>();
                        string sqlapply = "";
                        string sqlback = "select TD_Code,TD_SHLevel,TD_State from OM_TravelDelay where TD_Code='" + asd.key + "'";
                        DataTable dtback = DBCallCommon.GetDTUsingSqlText(sqlback);
                        if (dtback.Rows.Count > 0)
                        {
                            if (dtback.Rows[0]["TD_State"].ToString() == "4")
                            {
                                for (int i = 0; i < rptTravelDelayDetail.Items.Count; i++)
                                {
                                    TextBox txtT_Code = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtT_Code");
                                    TextBox txtTD_SQRID = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_SQRID");
                                    if (txtTD_SQRID.Text.Trim() != "")
                                    {
                                        TextBox txtTD_StartTimeReal = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_StartTimeReal");
                                        TextBox txtTD_EndTimeReal = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_EndTimeReal");
                                        TextBox txtTD_DaysReal = (TextBox)rptTravelDelayDetail.Items[i].FindControl("txtTD_DaysReal");

                                        sqlapply = "update OM_TravelApplyDetail set TA_StartTimeReal='" + txtTD_StartTimeReal.Text.ToString().Trim() + "',TA_EndTimeReal='" + txtTD_EndTimeReal.Text.ToString().Trim() + "',TA_DaysReal='" + txtTD_DaysReal.Text.ToString().Trim() + "' where TA_Code ='" + txtT_Code.Text.ToString().Trim() + "'and TA_SQRID='" + txtTD_SQRID.Text.ToString().Trim() + "'";
                                        listback.Add(sqlapply);
                                    }
                                }
                                DBCallCommon.ExecuteTrans(listback);
                            }
                        }

                        //邮件提醒
                        sql = "select TD_State,TD_SHRIDB,TD_SHRIDC from OM_TravelDelay where (TD_State='2' or TD_State='3') and TD_Code ='" + lbTD_Code.Text.Trim() + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt.Rows.Count > 0)
                        {
                            string id_emailto = "";
                            if (dt.Rows[0]["TD_State"].ToString() == "2")
                                id_emailto = dt.Rows[0]["TD_SHRIDB"].ToString();
                            else
                                id_emailto = dt.Rows[0]["TD_SHRIDC"].ToString();
                            string _emailto = DBCallCommon.GetEmailAddressByUserID(id_emailto);
                            string _body = "差旅延期审批任务:"
                                  + "\r\n延期单号：" + lbTD_Code.Text.Trim()
                                  + "\r\n制单人：" + lbTD_ZDR.Text.Trim()
                                  + "\r\n制单日期：" + lbTD_ZDTime.Text.Trim();

                            string _subject = "您有新的【差旅延期】需要审批，请及时处理:" + lbTD_Code.Text.Trim();
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        Response.Redirect("OM_TravelDelay.aspx");
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
