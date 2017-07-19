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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_check_detail : System.Web.UI.Page
    {
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
        public DataTable gloabt
        {
            get
            {
                object dt = ViewState["gloabt"];
                return dt == null ? null : (DataTable)dt;
            }
            set
            {
                ViewState["gloabt"] = value;
            }
        }
        public string gloabstate
        {
            get
            {
                object state = ViewState["gloabstate"];
                return state == null ? null : (string)state;
            }
            set
            {
                ViewState["gloabstate"] = value;
            }
        }
        public string gloabshape
        {
            get
            {
                object str = ViewState["gloabshape"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabshape"] = value;
            }
        }
        public string gloablaiyuan
        {
            get
            {
                object str = ViewState["gloablaiyuan"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloablaiyuan"] = value;
            }
        }
        public string gloabding
        {
            get
            {
                object str = ViewState["gloabding"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabding"] = value;
            }
        }
        public string gloabptc
        {
            get
            {
                object str = ViewState["gloabptc"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptc"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["sheetno"] != null)
                {
                    gloabsheetno = Server.UrlDecode(Request.QueryString["sheetno"].ToString());
                }
                else
                {
                    gloabsheetno = "";
                }
                if (Request.QueryString["laiyuan"] != null)
                {
                    gloablaiyuan = Request.QueryString["laiyuan"].ToString();
                }
                else
                {
                    gloablaiyuan = "";
                }
                if (Request.QueryString["ptc"] != null)
                {
                    gloabptc = Request.QueryString["ptc"].ToString();
                }
                else
                {
                    gloabptc = "";
                }
                Initpage();
                Initpower();
                tbpc_purshaseplancheck_datialRepeaterdatabind();
                if (gloabstate != "3" && gloabstate != "4")
                {
                    ModalPopupExtenderSearch.Show();
                }
            }
            //CheckUser(ControlFinder);
        }


        private void Initpage()
        {
            TextBoxNO.Text = gloabsheetno;//单号
            string sqltext = "";
            string strpeo=Session["UserName"].ToString().Trim();
            //审批人及审批主管的选择
            string sqltext1 = "";
            string sqltext2 = "";
            sqltext1 = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_NAME='" + strpeo.ToString().Trim() + "' and ST_DEPID='05'";
            sqltext2 = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='05' and ST_NAME='刘春利'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
            if (dt1.Rows.Count > 0)
            {
                Tb_shenpiren.Text = dt1.Rows[0]["ST_NAME"].ToString().Trim();
                Tb_shenpirenid.Text = dt1.Rows[0]["ST_ID"].ToString().Trim();
            }
            if (dt2.Rows.Count > 0)
            {
                Tb_zhuguan.Text = dt2.Rows[0]["ST_NAME"].ToString().Trim();
                Tb_zhuguanid.Text = dt2.Rows[0]["ST_ID"].ToString().Trim();
            }


            sqltext = "SELECT PR_REVIEWA,PR_REVIEWANM," +
                      "PR_PJID,PJ_NAME,PR_ENGID," +
                      "TSA_ENGNAME,PR_REVIEWATIME," +
                      "PR_REVIEWB,PR_REVIEWBNM," +
                      "PR_REVIEWBTIME,PR_REVIEWBADVC," +
                      "PR_ZHUGID,PR_ZHUGNM,PR_STATE,PR_SHSTATE  " +
                      "FROM View_TBPC_MARSTOUSETOTAL WHERE PR_PCODE='" + TextBoxNO.Text.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                TextBoxexecutor.Text = dt.Rows[0]["PR_REVIEWANM"].ToString();
                TextBoxexecutorid.Text = dt.Rows[0]["PR_REVIEWA"].ToString();
                if (dt.Rows[0]["PR_REVIEWB"].ToString() != "" || dt.Rows[0]["PR_ZHUGID"].ToString()!="" || dt.Rows[0]["PR_REVIEWBNM"].ToString() != "" || dt.Rows[0]["PR_REVIEWBNM"].ToString() != "")
                {
                    Tb_shenpirenid.Text = dt.Rows[0]["PR_REVIEWB"].ToString();
                    Tb_shenpiren.Text = dt.Rows[0]["PR_REVIEWBNM"].ToString();
                    Tb_zhuguan.Text = dt.Rows[0]["PR_ZHUGNM"].ToString();
                    Tb_zhuguanid.Text = dt.Rows[0]["PR_ZHUGID"].ToString();
                }
                Tb_shijian.Text = dt.Rows[0]["PR_REVIEWATIME"].ToString();
                TextBox_shenheren.Text = dt.Rows[0]["PR_REVIEWBNM"].ToString();
                TextBox_shenhesj.Text = dt.Rows[0]["PR_REVIEWBTIME"].ToString();
                suggestion.Text = dt.Rows[0]["PR_REVIEWBADVC"].ToString();
                tb_pj.Text = dt.Rows[0]["PJ_NAME"].ToString();
                tb_pjid.Text = dt.Rows[0]["PR_PJID"].ToString();
                tb_eng.Text = dt.Rows[0]["TSA_ENGNAME"].ToString();
                tb_engid.Text = dt.Rows[0]["PR_ENGID"].ToString();
                gloabstate = dt.Rows[0]["PR_STATE"].ToString();

            }
        }
        private void Initpower()
        {
            string loginid = Session["UserID"].ToString().Trim();
            string depid = Session["UserDeptID"].ToString().Trim();
            string position = Session["POSITION"].ToString().Trim();
            if (loginid == TextBoxexecutorid.Text && loginid == Tb_shenpirenid.Text)
            {
                if (gloabstate == "1" || gloabstate == "0")//未提交
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = true;
                    btn_delete.Visible = true;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = false;
                    Btn_MTO.Visible = false;
                }
                else if (gloabstate == "2")//提交
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = true;
                    suggestion.BackColor = System.Drawing.Color.Coral;
                    //Panel_btn.Visible = true;
                    Panel_view.Visible = true;
                    btn_fanshen.Visible = true;
                    Btn_MTO.Visible = false;
                }
                else if (gloabstate == "3")//驳回
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = true;
                    btn_delete.Visible = true;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = true;
                    Btn_MTO.Visible = false;
                }
                else if (gloabstate == "4")//审核通过
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = true;
                    Btn_MTO.Visible = false;     
                }
                else
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = false;
                    Btn_MTO.Visible = false;
                }
            }
            else if (loginid == TextBoxexecutorid.Text)//制单人
            {
                if (gloabstate == "1" || gloabstate == "0")//未提交
                {
                    Tb_shenpiren.Visible = false;
                    Tb_zhuguan.Visible = false;

                    pan_edit.Visible = true;
                    btn_delete.Visible = true;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = false;
                    Btn_MTO.Visible = false;

                }
                else if (gloabstate == "2")//提交
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = false;
                    Btn_MTO.Visible = false;
                }
                else if (gloabstate == "3")//驳回
                {
                    Tb_shenpiren.Visible = false;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = true;
                    btn_delete.Visible = true;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = false;
                    Btn_MTO.Visible = false;
                }
                else if (gloabstate == "4")//审核通过
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = false;
                    Btn_MTO.Visible = false;
                }
                else
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = false;
                    Btn_MTO.Visible = false;
                }
            }
            else if (loginid == Tb_shenpirenid.Text || position == "0501")//审批人
            {
                if (gloabstate == "1")//未提交
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = false;
                    Btn_MTO.Visible = false;
                }
                else if (gloabstate == "2")//提交
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = true;
                    suggestion.BackColor = System.Drawing.Color.Coral;
                    //Panel_btn.Visible = true;
                    Panel_view.Visible = true;
                    btn_fanshen.Visible = true;
                    Btn_MTO.Visible = false;
                }
                else if (gloabstate == "3")//驳回
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = true;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = true;
                    Btn_MTO.Visible = false;
                }
                else if (gloabstate == "4")//审核通过
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = true;
                    Btn_MTO.Visible = false;
                }
                else
                {
                    Tb_shenpiren.Visible = true;
                    Tb_zhuguan.Visible = true;

                    pan_edit.Visible = false;
                    btn_delete.Visible = false;
                    Panel_shenhe.Enabled = false;
                    //Panel_btn.Visible = false;
                    Panel_view.Visible = false;
                    btn_fanshen.Visible = false;
                    Btn_MTO.Visible = false;
                }
            }
            else//其他
            {
                Tb_shenpiren.Visible = true;
                Tb_zhuguan.Visible = true;

                pan_edit.Visible = false;
                btn_delete.Visible = false;
                Panel_shenhe.Enabled = false;
                //Panel_btn.Visible = false;
                Panel_view.Visible = false;
                btn_fanshen.Visible = false;

            }
        }
        private void tbpc_purshaseplancheck_datialRepeaterdatabind()
        {
            string sqltext = "";
            DataTable dt = new DataTable();
            DataView dv = null;

            sqltext = "SELECT ptcode AS PUR_PTCODE,marid AS PUR_MARID,marnm AS PUR_MARNAME," +
                        "margg AS PUR_MARNORM,marcz AS PUR_MARTERIAL,margb AS PUR_GUOBIAO," +
                        "num AS PUR_NUM,fznum AS PUR_FZNUM,marunit AS PUR_NUNIT," +
                        "usenum as PUR_USTNUM,allnote as PUR_NOTE,PUR_SHYJ,allshstate  FROM View_TBPC_MARSTOUSEALL " +
                        "where planno='" + TextBoxNO.Text.ToString() + "' order by ptcode asc";

            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (Radio_quanbu.Checked)//全部
            {
                dv = dt.DefaultView;
                dv.RowFilter = "";
                dt = dv.ToTable();
            }
            if (Radio_weish.Checked)//未审核
            {
                dv = dt.DefaultView;
                dv.RowFilter = "allshstate='0'";
                dt = dv.ToTable();
            }
            if (Radio_buty.Checked)//未驳回
            {
                dv = dt.DefaultView;
                dv.RowFilter = "allshstate='1'";
                dt = dv.ToTable();
            }
            if (Radio_tg.Checked)//未通过
            {
                dv = dt.DefaultView;
                dv.RowFilter = "allshstate='2'";
                dt = dv.ToTable();
            }

            tbpc_purshaseplancheck_datialRepeater.DataSource = dt;
            tbpc_purshaseplancheck_datialRepeater.DataBind();
            if (tbpc_purshaseplancheck_datialRepeater.Items.Count > 0)
            {
                NoDataPane1.Visible = false;
            }
            else
            {
                NoDataPane1.Visible = true;
            }
        }
        protected void Radio_quanbu_CheckedChanged(object sender, EventArgs e)
        {
            tbpc_purshaseplancheck_datialRepeaterdatabind();
        }
        protected void Radio_weish_CheckedChanged(object sender, EventArgs e)
        {
            tbpc_purshaseplancheck_datialRepeaterdatabind();
        }
        protected void Radio_buty_CheckedChanged(object sender, EventArgs e)
        {
            tbpc_purshaseplancheck_datialRepeaterdatabind();
        }
        protected void Radio_tg_CheckedChanged(object sender, EventArgs e)
        {
            tbpc_purshaseplancheck_datialRepeaterdatabind();
        }

        public string get_shstate(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) == 0)
            {
                statestr = "";
            }
            else if (Convert.ToInt32(i) == 1)
            {
                statestr = "驳回";
            }
            else if (Convert.ToInt32(i) == 2)
            {
                statestr = "同意";
            }
            return statestr;
        }
        protected void confirm_Click(object sender, EventArgs e)//确定
        {
            string sqltext = "";
            string purshstate = "";
            string ptcode = "";
            string shyj = "";
            int i = 0;
            int j = 0;
            int k = 0;
            List<string> sqltextlist = new List<string>();
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                purshstate = ((Label)Reitem.FindControl("PUR_SHTATE")).Text.Replace(" ", "");
                shyj = ((TextBox)Reitem.FindControl("PUR_SHYJ")).Text.Replace(" ", "");
                if (purshstate == "")
                {
                    i++;
                }
                if (purshstate == "驳回")
                {
                    k++;
                    if (shyj == "")
                    {
                        j++;
                    }
                }
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('还有没确定审核意见的数据！');", true);
                return;
            }
            else if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('驳回的数据请填写驳回理由！');", true);
                return;
            }
            else
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
                {
                    ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
                    purshstate = ((Label)Reitem.FindControl("PUR_SHTATE")).Text.Replace(" ", "");
                    shyj = ((TextBox)Reitem.FindControl("PUR_SHYJ")).Text.ToString();
                    if (purshstate == "同意")
                    {
                        sqltext = "UPDATE TBPC_MARSTOUSEALL SET PUR_SHSTATE='2',PUR_SHYJ='" + shyj + "'  WHERE PUR_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                        sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE = '1'  where PUR_PTCODE = '" + ptcode + "' and PUR_CSTATE='1'";//同意时把占用物料的状态改成1
                        sqltextlist.Add(sqltext);
                    }
                    else if (purshstate == "驳回")
                    {
                        sqltext = "UPDATE TBPC_MARSTOUSEALL SET PUR_SHSTATE='1',PUR_SHYJ='" + shyj + "'  where PUR_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                }
                if (k == 0)//没有驳回的就是全部通过了
                {
                    sqltext = "UPDATE TBPC_MARSTOUSETOTAL SET PR_STATE='4',PR_REVIEWBTIME='" +
                              DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,PR_REVIEWBADVC='" +
                              suggestion.Text.ToString() + "' WHERE PR_PCODE='" + TextBoxNO.Text.ToString() + "'";
                    sqltextlist.Add(sqltext);

                    //自动添加一个MTO调整单
                    AddMTO(sqltextlist);

                }
                else if (k > 0)//有一条驳回的，总体状态就是驳回
                {
                    sqltext = "UPDATE TBPC_MARSTOUSETOTAL SET PR_STATE='3',PR_REVIEWBTIME='" +
                                   DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,PR_REVIEWBADVC='" +
                                   suggestion.Text.ToString() + "'  WHERE PR_PCODE='" + TextBoxNO.Text.ToString() + "'";
                    sqltextlist.Add(sqltext);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('存在未同意的项，未进行MTO调整！');", true);
                }
                DBCallCommon.ExecuteTrans(sqltextlist);



                if (Request.QueryString["sheetno"] != null)
                {
                    gloabsheetno = Server.UrlDecode(Request.QueryString["sheetno"].ToString());
                }
                else
                {
                    gloabsheetno = "";
                }
                if (Request.QueryString["laiyuan"] != null)
                {
                    gloablaiyuan = Request.QueryString["laiyuan"].ToString();
                }
                else
                {
                    gloablaiyuan = "";
                }
                if (Request.QueryString["ptc"] != null)
                {
                    gloabptc = Request.QueryString["ptc"].ToString();
                }
                else
                {
                    gloabptc = "";
                }
                Initpage();
                Initpower();
                tbpc_purshaseplancheck_datialRepeaterdatabind();
            }
        }

        #region 生成MTO调整单

        string TextBoxDate;
        string TextBoxPTCTo = "";
        string TextBoxComment;
        string LabelDoc;
        string LabelDocCode;
        string LabelApproveDate;
        string LabelClosingAccount;
        private void AddMTO(List<string> sqllist)
        {
            if (Initial())
            {
                ClosingAccountDate(TextBoxDate);
                if (isLock() == false)
                {
                    string script = "alert('系统正在结账,请稍后...');";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                    LabelMessage.Text = "（系统正在结账,请稍后...）";
                    return;
                }
                else
                {
                    string Code = GenerateCode();

                    string sqlstate = "select mto_state from tbws_mto where mto_code='" + Code + "'";

                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                    {
                        if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["mto_state"].ToString() == "2")
                        {

                            string script = "alert('单据已审核，单据不能再审核！');";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);

                            return;
                        }
                    }

                    bool HasError = false;
                    int ErrorType = 0;

                    string sql = "";

                    string Date = TextBoxDate;
                    string TargetCode = TextBoxPTCTo.Split('-')[TextBoxPTCTo.Split('-').Length - 1];
                    string Comment = TextBoxComment;
                    string PlanerCode = "";
                    string DepCode = "";
                    string DocCode = LabelDocCode;

                    string VerifierCode = Session["UserID"].ToString();

                    string ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    //if(LabelApproveDate.Text.Trim())

                    //上一次的审核日期LabelApproveDate.Text.Trim()，可能有值，也可能为空，有值表示反审，无值表示第一次审核

                    //如果核算之后，是不能反审的
                    if (LabelClosingAccount == "NoTime")
                    {
                        //未封账
                        ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        //封账
                        sql = "SELECT COUNT(*) FROM TBFM_HSTOTAL WHERE HS_YEAR=substring(convert(varchar(50),getdate(),112),1,4) AND HS_MONTH=substring(convert(varchar(50),getdate(),112),5,2) AND HS_STATE='2'";
                        DataTable dtcount = DBCallCommon.GetDTUsingSqlText(sql);
                        if (Convert.ToInt32(dtcount.Rows[0][0]) == 0)
                        {
                            //无反核算
                            //if(LabelApproveDate.Text.Trim())
                            ApproveDate = getNextMonth() + " 07:59:59";
                            Date = getNextMonth();
                        }
                        else
                        {
                            //有反核算
                            //得看上次审核时间，是不是本月的
                            if (LabelApproveDate.Trim().Length > 8)
                            {
                                //多次审核
                                if (Convert.ToInt32(LabelApproveDate.Trim().Substring(0, 4)) == System.DateTime.Now.Year && Convert.ToInt32(LabelApproveDate.Trim().Substring(5, 2)) == System.DateTime.Now.Month)
                                {
                                    //是本月时间
                                    ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    //不是本月
                                    ApproveDate = getNextMonth() + " 07:59:59";
                                    Date = getNextMonth();
                                }
                            }
                            else
                            {
                                //第一次审核
                                ApproveDate = getNextMonth() + " 07:59:59";
                            }
                        }

                    }

                    sql = "DELETE FROM TBWS_MTO WHERE MTO_CODE='" + Code + "'";
                    sqllist.Add(sql);
                    sql = "INSERT INTO TBWS_MTO(MTO_CODE,MTO_DATE,MTO_TARGETID," +
                        "MTO_DEP,MTO_PLANER,MTO_DOC,MTO_VERIFIER,MTO_VERIFYDATE," +
                        "MTO_STATE,MTO_NOTE,MTO_RealTime) VALUES('" + Code + "','" +
                        Date + "','" + TargetCode + "','" + DepCode + "','" + PlanerCode + "','" + DocCode + "','" +
                        VerifierCode + "','" + ApproveDate + "','1','" + Comment + "',convert(varchar(50),getdate(),120))";
                    sqllist.Add(sql);
                    sql = "DELETE FROM TBWS_MTODETAIL WHERE MTO_CODE='" + Code + "'";
                    sqllist.Add(sql);
                    for (int i = 0; i < mto.Rows.Count; i++)
                    {
                        DataRow dr = mto.Rows[i];
                        string UniqueID = Code + (i + 1).ToString();
                        string SQCODE = dr["SQCODE"].ToString();
                        string MaterialCode = dr["MaterialCode"].ToString();
                        string Fixed = dr["Fixed"].ToString();
                        float Length = 0;
                        try { Length = Convert.ToSingle(dr["lLength"].ToString()); }
                        catch { }
                        float Width = 0;
                        try { Width = Convert.ToSingle(dr["Width"].ToString()); }
                        catch { }
                        string LotNumber = dr["LotNumber"].ToString();
                        string PTCFrom = dr["PTCFrom"].ToString();
                        string WarehouseCode = dr["WarehouseCode"].ToString();

                        string PositionCode = dr["PositionCode"].ToString();

                        if (PositionCode == string.Empty || PositionCode == "0")
                        {
                            HasError = true;
                            ErrorType = 3;
                            break;
                        }

                        float WN = 0;
                        try { WN = Convert.ToSingle(dr["WN"].ToString()); }
                        catch { }
                        Int32 WQN = 0;
                        try { WQN = Convert.ToInt32(dr["WQN"].ToString()); }
                        catch { }

                        string sqlzy = "SELECT ptcode,usenum FROM View_TBPC_MARSTOUSEALL where planno='" + TextBoxNO.Text.ToString() + "' and marid='" + MaterialCode + "'";
                        DataTable zy = DBCallCommon.GetDTUsingSqlText(sqlzy);
                        string PTCTo = "--请选择--";
                        string usenum = "0";
                        if (zy.Rows.Count > 0)
                        {
                            PTCTo = zy.Rows[0]["ptcode"].ToString();
                            usenum = zy.Rows[0]["usenum"].ToString();
                        }
                        if (PTCTo == "--请选择--" || PTCTo == string.Empty)
                        {
                            HasError = true;
                            ErrorType = 1;
                            break;
                        }

                        if (PTCTo == PTCFrom)
                        {
                            HasError = true;
                            ErrorType = 4;
                            break;
                        }
                        string PlanMode = dr["PlanMode"].ToString();
                        float AdjN = 0;
                        try { AdjN = Convert.ToSingle(usenum); }
                        catch { }

                        if (AdjN == 0)
                        {
                            HasError = true;
                            ErrorType = 2;
                            break;
                        }

                        Int32 AdjQN = 0;
                        try { AdjQN = Convert.ToInt32(dr["AdjQN"].ToString()); }
                        catch { }


                        string OrderID = dr["OrderID"].ToString();
                        string Note = dr["Note"].ToString();
                        sql = "INSERT INTO TBWS_MTODETAIL(MTO_UNIQUEID,MTO_CODE,MTO_SQCODE,MTO_MARID," +
                            "MTO_FIXED,MTO_LENGTH,MTO_WIDTH,MTO_PCODE,MTO_FROMPTCODE," +
                            "MTO_WAREHOUSE,MTO_LOCATION,MTO_KTNUM,MTO_KTFZNUM,MTO_TOPTCODE,MTO_TZNUM," +
                            "MTO_TZFZNUM,MTO_PMODE,MTO_ORDERID,MTO_NOTE,MTO_STATE) VALUES('" + UniqueID + "','" +
                            Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                            Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "'," +
                            WN + "," + WQN + ",'" + PTCTo + "'," + AdjN + "," + AdjQN + ",'" + PlanMode + "','" +
                            OrderID + "','" + Note + "','')";
                        sqllist.Add(sql);
                    }

                    if (HasError)
                    {
                        sqllist.Clear();

                        if (ErrorType == 1)
                        {
                            LabelMessage.Text = "（计划跟踪号为空，单据不能审核！）";

                            string script = "alert('计划跟踪号为空，单据不能审核！');";

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                            return;
                        }
                        else if (ErrorType == 2)
                        {

                            LabelMessage.Text = "（调整数量为0，单据不能审核！）";

                            string script = "alert('调整数量为0，单据不能审核！');";

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                            return;
                        }
                        else if (ErrorType == 3)
                        {

                            LabelMessage.Text = "（仓位为空，单据不能审核！）";

                            string script = "alert('仓位为空，单据不能审核！');";

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                            return;
                        }
                        else if (ErrorType == 4)
                        {

                            LabelMessage.Text = "（调整之后计划跟踪号，不能与调整之前的相同！）";

                            string script = "alert('调整之后计划跟踪号，不能与调整之前的相同！');";

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                            return;
                        }

                    }
                    else
                    {


                        DBCallCommon.ExecuteTrans(sqllist);
                        sqllist.Clear();

                        sql = DBCallCommon.GetStringValue("connectionStrings");
                        SqlConnection con = new SqlConnection(sql);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("MTO", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@MTOCode", SqlDbType.VarChar, 50);				//增加参数
                        cmd.Parameters["@MTOCode"].Value = Code;							//为参数初始化
                        cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                        {
                            //string action = "NOR";


                            sqllist.Clear();

                            string strsql = "update TBPC_MARSTOUSEALL set PUR_ISSTOUSE='2' where PUR_PCODE='" + TextBoxNO.Text.Trim() + "'";

                            sqllist.Add(strsql);


                            strsql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='3' WHERE MP_MTO='" + Code.Trim() + "'";
                            sqllist.Add(strsql);



                            DBCallCommon.ExecuteTrans(sqllist);
                        }
                        if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                        {
                            LabelMessage.Text = "（无法通过审核：部分物料不存在！）";
                        }
                        if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                        {
                            LabelMessage.Text = "（无法通过审核：部分物料数量小于调整数量！）";
                        }
                    }
                }
            }
            else
            {
                LabelMessage.Text = "（自动MTO调整出错(有物料不存在!)，请到MTO调整查看！）";

                string script = "alert('自动MTO调整出错(有物料不存在!)，请到MTO调整查看！');";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                return;
            }
        }

        //初始化数据
        DataTable mto = new DataTable();
        DataTable dtzy = new DataTable();
        private bool Initial()
        {
            bool b = true;
            List<string> list = new List<string>();
            string sqltext = "SELECT marid FROM View_TBPC_MARSTOUSEALL where planno='" + TextBoxNO.Text.ToString() + "' order by ptcode asc";
            dtzy = DBCallCommon.GetDTUsingSqlText(sqltext);

            for (int i = 0; i < dtzy.Rows.Count; i++)
            {
                string marid = dtzy.Rows[i][0].ToString();
                sqltext = "select SQCODE from View_SM_Storage where (SQCODE like '%备库%' or SQCODE like '%BEIKU%' or SQCODE like '%beiku%') and MaterialCode='" + marid + "' order by Number DESC";
                DataTable dtkc = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtkc.Rows.Count > 0)
                {
                    sqltext = "UPDATE TBWS_STORAGE SET SQ_STATE='MTO" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + dtkc.Rows[0][0].ToString() + "'";
                    list.Add(sqltext);
                }
                else
                {
                    b = false;
                }
            }

            DBCallCommon.ExecuteTrans(list);

            string LabelCode = GenerateCode();
            TextBoxDate = DateTime.Now.ToString("yyyy-MM-dd");
            string sql = "insert into TBWS_MTOCODE (MTO_CODE) VALUES ('" + LabelCode + "')";

            DBCallCommon.ExeSqlText(sql);
            //LabelState.Text = "0";
            ClosingAccountDate(TextBoxDate.Trim());
            TextBoxComment = "";
            LabelDoc = Session["UserName"].ToString();
            LabelDocCode = Session["UserID"].ToString();
            LabelApproveDate = "";

            sqltext = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length," +
                "Width AS Width,LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTCFrom," +
                "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                "Location AS Position,Unit AS Unit,cast(Number as float) AS WN,cast(SupportNumber as float) AS WQN,PTCTO='--请选择--',cast(Number as float) AS AdjN," +
                "cast(SupportNumber as float) AS AdjQN,OrderCode AS OrderID,Note AS Note " +
                "FROM View_SM_Storage WHERE State='MTO" + Session["UserID"].ToString() + "'";


            mto = DBCallCommon.GetDTUsingSqlText(sqltext);

            sqltext = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='MTO" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sqltext);

            return b;
        }

        protected bool isLock()
        {
            string nowyear = DateTime.Now.Year.ToString();
            string nowmonth = DateTime.Now.Month.ToString();
            string sqllock = "select HS_KEY from TBWS_LOCKTABLE where HS_YEAR='" + nowyear + "' AND HS_MONTH='" + nowmonth + "'";
            SqlDataReader drlock = DBCallCommon.GetDRUsingSqlText(sqllock, 5);
            bool flag = true;
            try
            {


                if (drlock.Read())
                {
                    if (drlock["HS_KEY"].ToString() == "1")
                    {
                        flag = false;
                    }
                }
                drlock.Close();
                return flag;
            }
            catch (Exception)
            {
                drlock.Close();
                return false;
            }
        }

        //生成调整单编号
        protected string GenerateCode()
        {
            string Code = "";
            string sql = "SELECT MAX(MTO_CODE) AS MaxCode FROM TBWS_MTOCODE WHERE LEN(MTO_CODE)=10";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                if (dr["MaxCode"] != DBNull.Value)
                {
                    Code = dr["MaxCode"].ToString();
                }
            }
            dr.Close();
            if (Code == "")
            {
                return "MTO0000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((Code.Substring(3, 7)));
                tempnum++;
                Code = "MTO" + tempnum.ToString().PadLeft(7, '0');
                return Code;
            }
        }

        //获取系统封账时间
        private void ClosingAccountDate(string ZDDate)
        {
            string NowDate = ZDDate;
            //查找本期系统关账时间
            string sql = "select HS_TIME from TBFM_HSTOTAL where  HS_YEAR='" + NowDate.Substring(0, 4) + "' and HS_MONTH='" + NowDate.Substring(5, 2) + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                LabelClosingAccount = dt.Rows[0]["HS_TIME"].ToString();
            }
            else
            {
                LabelClosingAccount = "NoTime";
            }

        }

        //得到下个月的第一天
        protected string getNextMonth()
        {
            string objymd = string.Empty;

            string ymd = System.DateTime.Now.ToString("yyyyMMdd");
            //年
            string yy = ymd.Substring(0, 4);

            //月
            string mt = string.Empty;

            int m = Convert.ToInt16(ymd.Substring(4, 2));
            m = m + 1;
            if (m > 12)
            {
                m = 1;
                int y = Convert.ToInt32(yy);
                y = y + 1;
                yy = y.ToString();
            }
            if (m.ToString().Length < 2)
            {
                mt = "0" + m.ToString();
            }
            else
            {
                mt = m.ToString();
            }

            //返回值
            objymd = yy + "-" + mt + "-" + "01";

            return objymd;
        }

        #endregion


        protected void btn_delete_click(object sender, EventArgs e)
        {
            int temp = candelete();
            string ptcode = "";
            string sqltext = "";
            if (temp == 0)
            {
                foreach (RepeaterItem Retem in tbpc_purshaseplancheck_datialRepeater.Items)
                {
                    CheckBox cbx = Retem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        ptcode = ((Label)Retem.FindControl("PUR_PTCODE")).Text.ToString();
                        sqltext = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count > 0)
                        {
                            string cgr = dt.Rows[0]["PUR_CGMAN"].ToString();
                            if (dt.Rows.Count == 2)
                            {
                                if (cgr == "")
                                {
                                    sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='0' where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                                    DBCallCommon.ExeSqlText(sqltext);
                                }
                                else
                                {
                                    sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                                    DBCallCommon.ExeSqlText(sqltext);
                                }
                            }
                            else
                            {
                                if (cgr == "")
                                {
                                    sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='0'  where PUR_PTCODE='" + ptcode + "'";
                                    DBCallCommon.ExeSqlText(sqltext);
                                }
                                else
                                {
                                    sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='4'  where PUR_PTCODE='" + ptcode + "'";
                                    DBCallCommon.ExeSqlText(sqltext);
                                }
                            }
                        }
                        sqltext = "delete from TBPC_MARSTOUSEALLDETAIL where PUR_PTCODE='" + ptcode + "'";
                        DBCallCommon.ExeSqlText(sqltext);

                        sqltext = "delete from TBPC_MARSTOUSEALL where PUR_PTCODE='" + ptcode + "'";
                        DBCallCommon.ExeSqlText(sqltext);





                        string sql = "";
                        //double num = 0;
                        //double fznum = 0;

                        sql = "update TBPC_PURCHASEPLAN set PUR_CSTATE='0',PUR_ZYDY='',Pue_Closetype=NULL  where PUR_PTCODE='" + ptcode + "' and (PUR_CSTATE='1' or PUR_CSTATE='2')";
                        DBCallCommon.ExeSqlText(sql);

                        sql = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + TextBoxNO.Text + "' and PUR_CSTATE='0' and PUR_STATE<'3'";
                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt1.Rows.Count > 0)
                        {
                            string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='0' where PR_SHEETNO='" + TextBoxNO.Text + "'";
                            DBCallCommon.ExeSqlText(sql2);
                        }
                    }
                }
                sqltext = "select * from TBPC_MARSTOUSEALL where PUR_PCODE='" + TextBoxNO.Text + "'";
                DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt2.Rows.Count == 0)
                {
                    sqltext = "delete from TBPC_MARSTOUSETOTAL where PR_PCODE='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！');", true);
                tbpc_purshaseplancheck_datialRepeaterdatabind();
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据中包含已通过的数据，不能删除！');", true);
            }
        }

        private int candelete()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            string shstate = "";
            int j = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    shstate = ((Label)Reitem.FindControl("allshstate")).Text.ToString();
                    if (shstate == "2")//有已通过的数据不能删
                    {
                        j++;
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)
            {
                temp = 2;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        protected void btn_tijiao_Click(object sender, EventArgs e)//编辑完场提交
        {
            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            string ptcode = "";
            string zgid = "";
            string zgnm = "";
            string zdid = "";
            string zdname = "";
            string shid = "";
            string shnm = "";
            string zdtm = "";
            string marid = "";
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text.ToString();
                marid = ((Label)Reitem.FindControl("PUR_MARID")).Text.ToString();
                string sql = "select PUR_STATE from TBPC_MARSTOUSEALL where PUR_MARID='" + marid + "' and PUR_PTCODE='" + ptcode + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    string state = dr["PUR_STATE"].ToString();
                    if (state == "0")
                    {
                        i++;
                        break;
                    }
                }
                dr.Close();
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('有未保存的记录,不能提交！');", true);
            }
            else
            {

                zgid = Tb_zhuguanid.Text.ToString().Trim();
                zgnm = Tb_zhuguan.Text.ToString().Trim();
                shid = Tb_shenpirenid.Text.ToString().Trim();
                shnm = Tb_shenpiren.Text.ToString().Trim();
                zdtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                zdid = TextBoxexecutorid.Text;
                zdname = TextBoxexecutor.Text;
                sqltext = "UPDATE TBPC_MARSTOUSETOTAL SET PR_ZHUGID='" + zgid + "'," +
                          "PR_REVIEWA='" + zdid + "',PR_REVIEWATIME='" + zdtm + "'," +
                          "PR_REVIEWB='" + shid + "',PR_STATE='2',PR_REVIEWBTIME='',PR_REVIEWBADVC='',PR_SHSTATE='0'  " +
                          "WHERE PR_PCODE='" + TextBoxNO.Text.ToString() + "'";
                sqltextlist.Add(sqltext);
                foreach (RepeaterItem reitem in tbpc_purshaseplancheck_datialRepeater.Items)
                {
                    string purshstate = ((Label)reitem.FindControl("PUR_SHTATE")).Text.Replace(" ", "");
                    ptcode = ((Label)reitem.FindControl("PUR_PTCODE")).Text;
                    if (purshstate == "驳回")
                    {
                        sqltext = "UPDATE TBPC_MARSTOUSEALL SET PUR_SHSTATE='0',PUR_SHYJ=''  where PUR_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                //Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx");
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_detail.aspx?sheetno=" + Server.UrlEncode(TextBoxNO.Text) + "");
            }


        }
        protected void btn_xiugai_Click(object sender, EventArgs e)
        {
            string marid = "";
            string submarid = "";
            bool temp = false;
            if (tbpc_purshaseplancheck_datialRepeater.Items.Count > 0)
            {
                foreach (RepeaterItem reitem in tbpc_purshaseplancheck_datialRepeater.Items)
                {
                    marid = ((Label)reitem.FindControl("PUR_MARID")).Text;
                    submarid = marid.Substring(0, 5);
                    if (submarid == "01.01")
                    {
                        temp = true;
                        break;
                    }
                }
                Response.Redirect("~/PC_Data/PC_TBPC_marstouseallGB.aspx?pcode=" + Server.UrlEncode(TextBoxNO.Text.ToString()) + "");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据,本次操作无效！');", true);
            }
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx");
        }

        double plannum = 0;
        double zhannum = 0;
        protected void tbpc_purshaseplancheck_datialRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
            if (e.Item.ItemIndex >= 0)
            {
                string PUR_SHTATE = ((Label)e.Item.FindControl("PUR_SHTATE")).Text;
                if (PUR_SHTATE == "驳回")
                {
                    ((Label)e.Item.FindControl("PUR_SHTATE")).ForeColor = System.Drawing.Color.Red;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                plannum += Convert.ToDouble(((Label)e.Item.FindControl("PUR_NUM")).Text);
                zhannum += Convert.ToDouble(((Label)e.Item.FindControl("PUR_USTNUM")).Text);
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("LabelPlanNum")).Text = plannum.ToString();
                ((Label)e.Item.FindControl("LabelZhanNum")).Text = zhannum.ToString();

            }
        }

        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Enabled != false)
                        {
                            cbx.Checked = true;
                        }
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }
        protected void btn_LX_click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            int start = 0;
            int finish = 0;
            int k = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                j++;
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                if (cbx.Checked)
                {
                    i++;
                    if (start == 0)
                    {
                        start = j;
                    }
                    else
                    {
                        finish = j;
                    }
                }
            }
            if (i == 2)
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
                {
                    k++;
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                    if (k >= start && k <= finish)
                    {
                        cbx.Checked = true;
                    }
                    if (k > finish)
                    {
                        cbx.Checked = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择连续的区间！');", true);
            }
        }

        protected void btn_QX_click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                cbx.Checked = false;
            }
        }

        protected void radio_ty_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                if (cbx.Checked)
                {
                    i++;
                    ((Label)Reitem.FindControl("PUR_SHTATE")).Text = "同意";
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
            }
            radio_ty.Checked = false;
        }
        protected void radio_bh_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                if (cbx.Checked)
                {
                    i++;
                    ((Label)Reitem.FindControl("PUR_SHTATE")).Text = "驳回";
                    ((Label)Reitem.FindControl("PUR_SHTATE")).ForeColor = System.Drawing.Color.Red;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
            }
            radio_bh.Checked = false;
        }

        protected void btn_fanshen_click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                if (cbx.Checked)
                {
                    i++;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要反审的数据！');", true);
            }
            else
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
                {
                    string ptcode = "";
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                    if (cbx.Checked)
                    {
                        ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
                        sqltext = "UPDATE TBPC_MARSTOUSEALL SET PUR_SHSTATE='0',PUR_SHYJ=''  where PUR_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                }
                sqltext = "UPDATE TBPC_MARSTOUSETOTAL SET PR_STATE='2',PR_REVIEWBTIME='' ,PR_REVIEWBADVC='' WHERE PR_PCODE='" + TextBoxNO.Text.ToString() + "'";
                sqltextlist.Add(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_detail.aspx?sheetno=" + Server.UrlEncode(TextBoxNO.Text) + "");
            }

        }

        protected void Btn_MTO_Click(object sender, EventArgs e)
        {
            ;
            string script = "window.open('../SM_Data/SM_STOUSE_MTO.aspx?ID=" + Server.UrlEncode(TextBoxNO.Text.Trim()) + "');";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }


        protected void QueryButton_Click(object sender, EventArgs e)
        {
            //全选
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Enabled != false)
                    {
                        cbx.Checked = true;
                    }
                }
            }


            //审核意见
            int r = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                if (cbx.Checked)
                {
                    r++;
                    ((Label)Reitem.FindControl("PUR_SHTATE")).Text = "同意";
                }
            }
            if (r == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
            }
            radio_ty.Checked = false;


            //审核
            string sqltext = "";
            string purshstate = "";
            string ptcode = "";
            string shyj = "";
            int i = 0;
            int j = 0;
            int k = 0;
            List<string> sqltextlist = new List<string>();
            foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
            {
                purshstate = ((Label)Reitem.FindControl("PUR_SHTATE")).Text.Replace(" ", "");
                shyj = ((TextBox)Reitem.FindControl("PUR_SHYJ")).Text.Replace(" ", "");
                if (purshstate == "")
                {
                    i++;
                }
                if (purshstate == "驳回")
                {
                    k++;
                    if (shyj == "")
                    {
                        j++;
                    }
                }
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('还有没确定审核意见的数据！');", true);
                return;
            }
            else if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('驳回的数据请填写驳回理由！');", true);
                return;
            }
            else
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplancheck_datialRepeater.Items)
                {
                    ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
                    purshstate = ((Label)Reitem.FindControl("PUR_SHTATE")).Text.Replace(" ", "");
                    shyj = ((TextBox)Reitem.FindControl("PUR_SHYJ")).Text.ToString();
                    if (purshstate == "同意")
                    {
                        sqltext = "UPDATE TBPC_MARSTOUSEALL SET PUR_SHSTATE='2',PUR_SHYJ='" + shyj + "'  WHERE PUR_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                        sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE = '1'  where PUR_PTCODE = '" + ptcode + "' and PUR_CSTATE='1'";//同意时把占用物料的状态改成1
                        sqltextlist.Add(sqltext);
                    }
                    else if (purshstate == "驳回")
                    {
                        sqltext = "UPDATE TBPC_MARSTOUSEALL SET PUR_SHSTATE='1',PUR_SHYJ='" + shyj + "'  where PUR_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                }
                if (k == 0)//没有驳回的就是全部通过了
                {
                    sqltext = "UPDATE TBPC_MARSTOUSETOTAL SET PR_STATE='4',PR_REVIEWBTIME='" +
                              DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,PR_REVIEWBADVC='" +
                              suggestion.Text.ToString() + "' WHERE PR_PCODE='" + TextBoxNO.Text.ToString() + "'";
                    sqltextlist.Add(sqltext);

                    //自动添加一个MTO调整单
                    AddMTO(sqltextlist);

                }
                else if (k > 0)//有一条驳回的，总体状态就是驳回
                {
                    sqltext = "UPDATE TBPC_MARSTOUSETOTAL SET PR_STATE='3',PR_REVIEWBTIME='" +
                                   DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,PR_REVIEWBADVC='" +
                                   suggestion.Text.ToString() + "'  WHERE PR_PCODE='" + TextBoxNO.Text.ToString() + "'";
                    sqltextlist.Add(sqltext);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('存在未同意的项，未进行MTO调整！');", true);
                }
                DBCallCommon.ExecuteTrans(sqltextlist);



                if (Request.QueryString["sheetno"] != null)
                {
                    gloabsheetno = Server.UrlDecode(Request.QueryString["sheetno"].ToString());
                }
                else
                {
                    gloabsheetno = "";
                }
                if (Request.QueryString["laiyuan"] != null)
                {
                    gloablaiyuan = Request.QueryString["laiyuan"].ToString();
                }
                else
                {
                    gloablaiyuan = "";
                }
                if (Request.QueryString["ptc"] != null)
                {
                    gloabptc = Request.QueryString["ptc"].ToString();
                }
                else
                {
                    gloabptc = "";
                }
                Initpage();
                Initpower();
                tbpc_purshaseplancheck_datialRepeaterdatabind();
            }
        }
    }
}
