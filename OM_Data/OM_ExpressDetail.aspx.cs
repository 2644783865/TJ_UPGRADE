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
using ZCZJ_DPF.CommonClass;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ExpressDetail1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            unitparam.action = Request.QueryString["action"];
            unitparam.key = Request.QueryString["key"];
            unitparam.userid = Session["UserID"].ToString();
            unitparam.username = Session["UserName"].ToString();
            unitparam.depid = Session["UserDeptID"].ToString();
            unitparam.depname = Session["UserDept"].ToString();

            if (!IsPostBack)
            {
                binddetail();
                // bindshenhe();
            }
            PowerControl();
        }

        //定义全局变量
        private class unitparam
        {
            public static string key;
            public static string userid;
            public static string username;
            public static string depid;
            public static string depname;
            public static string action;
        }

        //申请单数据绑定
        private void binddetail()
        {
            string sql = "";
            DataTable dt;
            if (unitparam.action == "add")
            {
                lb_E_Code.Text = GetCode();
                lb_E_ZDR.Text = unitparam.username;
                lb_E_ZDRID.Text = unitparam.userid;
                lb_E_SQRDep.Text = unitparam.depname;
                lb_E_ZDTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string position = unitparam.depid + "01";
                if (unitparam.depid == "15")
                {
                    //对焊事业部于来义
                    position = "0301";
                }
                if (unitparam.depid == "08" || unitparam.depid == "09")
                {
                    position = "0401";
                }
                else if (unitparam.depid == "13")
                {
                    position = "1001";
                }
                else if (unitparam.depid == "14"||unitparam.depid=="02")
                {
                    //办公室刘晓静
                    position = "0210";
                }
                else if (unitparam.depid == "12")  
                {
                    position = "1207";
                }
                else if (unitparam.depid == "03")
                {
                    position = "0302";
                }
                sql = "select ST_NAME,ST_ID,ST_POSITION from TBDS_STAFFINFO where ST_POSITION='0207'or (ST_POSITION = '" + position + "'and ST_PD='0')";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][2].ToString() != "0207")
                    {
                        txt_E_SHR.Text = dt.Rows[i][0].ToString();
                        txt_E_SHRID.Text = dt.Rows[i][1].ToString();
                    }
                    else
                    {
                        lb_E_Surer.Text = dt.Rows[i][0].ToString();
                        hid_E_SurerID.Value = dt.Rows[i][1].ToString();
                    }
                }
            }
            else
            {
                sql = "select * from OM_Express where E_Code ='" + unitparam.key + "'";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                PanelDic.BindPanel(PanelDetail, dt);
                PanelDic.BindPanel(PanelShenhe, dt);
                if (dt.Rows[0]["E_Type"].ToString() == "1")
                {
                    td_wenjian0.Visible = false;
                    td_wenjian1.Visible = false;
                    td_wuping0.Visible = true;
                    td_wuping1.Visible = true;
                    td_wuping2.Visible = true;
                    td_wuping3.Visible = true;
                    txt_E_ItemName.Text = dt.Rows[0]["E_ItemName"].ToString();
                    txt_E_ItemWeight.Text = dt.Rows[0]["E_ItemWeight"].ToString();
                }
                else
                    txt_E_FileName.Text = dt.Rows[0]["E_FileName"].ToString();
                if (unitparam.action == "sure")
                {
                    txt_E_ExpressTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txt_E_ExpressMoney.Text = "";
                }
            }
        }

        //控件可用性和可见性
        private void PowerControl()
        {
            PanelExpress.Visible = true;
            PanelExpressBack.Visible = false;
            PanelDetail.Visible = true;
            PanelShenhe.Visible = true;
            btnSave.Visible = false;
            btnSubmit.Visible = false;
            btnSure.Visible = false;
            if (unitparam.action == "add" || unitparam.action == "edit")
            {
                PanelShenhe.Visible = false;
                btnSave.Visible = true;
            }
            else
            {
                if (unitparam.action == "audit")
                {
                    PanelDetail.Enabled = false;
                    btnSubmit.Visible = true;
                }
                else if (unitparam.action == "sure")
                {
                    PanelExpress.Enabled = false;
                    PanelExpressBack.Visible = true;
                    PanelShenhe.Enabled = false;
                    btnSure.Visible = true;
                }
                else
                {
                    PanelDetail.Enabled = false;
                    PanelExpressBack.Visible = true;
                    PanelShenhe.Enabled = false;
                }
            }
        }

        //生成快递申请号
        private string GetCode()
        {
            string e_code = "";
            string sql = "select max(E_Code) from OM_Express ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "")
            {
                e_code = "KD-" + DateTime.Now.ToString("yyyyMMdd") + "-01";
            }
            else
            {
                e_code = "KD-" + DateTime.Now.ToString("yyyyMMdd") + "-" + (CommonFun.ComTryDecimal(dt.Rows[0][0].ToString().Substring(12, 2)) + 1).ToString().PadLeft(2, '0');
            }
            return e_code;
        }

        //保存申请单数据
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (unitparam.action == "add")
            {
                Dictionary<string, string> dic1 = PanelDic.DicPan(PanelDetail, "OM_Express", new Dictionary<string, string>());
                Dictionary<string, string> dic2 = PanelDic.DicPan(PanelShenhe, "OM_Express", new Dictionary<string, string>());
                string key = "";
                string value = "";
                foreach (KeyValuePair<string, string> pair in dic1)
                {
                    key += pair.Key.ToString() + ",";
                    value += "'" + pair.Value.ToString() + "',";
                }
                foreach (KeyValuePair<string, string> pair in dic2)
                {
                    key += pair.Key.ToString() + ",";
                    value += "'" + pair.Value.ToString() + "',";
                }
                key += "E_State";//申请单状态0-初始化，1-待审批,2-已通过,3-已驳回，4-已反馈，5-反馈驳回
                value += "'0'";
                if (ddl_E_Type.SelectedValue != "0")
                {
                    key += ",E_ItemName,E_ItemWeight";
                    value += ",'" + txt_E_ItemName.Text.Trim() + "','" + txt_E_ItemWeight.Text.Trim() + "'";
                }
                else
                {
                    key += ",E_FileName";
                    value += ",'" + txt_E_FileName.Text.Trim() + "'";
                }
                string sql = "insert into OM_Express (" + key + ") values (" + value + ")";
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                    btnSave.Visible = false;
                    btnSubmit.Visible = true;
                    PanelDetail.Enabled = false;
                    PanelShenhe.Enabled = false;
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据保存失败！');", true);
                }
            }
            else
            {
                string sqltext = "delete from OM_Express where E_Code='" + unitparam.key + "'";
                DBCallCommon.ExeSqlText(sqltext);
                Dictionary<string, string> dic1 = PanelDic.DicPan(PanelDetail, "OM_Express", new Dictionary<string, string>());
                Dictionary<string, string> dic2 = PanelDic.DicPan(PanelShenhe, "OM_Express", new Dictionary<string, string>());
                string key = "";
                string value = "";
                foreach (KeyValuePair<string, string> pair in dic1)
                {
                    key += pair.Key.ToString() + ",";
                    value += "'" + pair.Value.ToString() + "',";
                }
                foreach (KeyValuePair<string, string> pair in dic2)
                {
                    key += pair.Key.ToString() + ",";
                    value += "'" + pair.Value.ToString() + "',";
                }
                key += "E_State";//申请单状态0-初始化，1-待审批,2-已通过,3-已驳回，4-已反馈，5-反馈驳回
                value += "'0'";
                if (ddl_E_Type.SelectedValue != "0")
                {
                    key += ",E_ItemName,E_ItemWeight";
                    value += ",'" + txt_E_ItemName.Text.Trim() + "','" + txt_E_ItemWeight.Text.Trim() + "'";
                }
                else
                {
                    key += ",E_FileName";
                    value += ",'" + txt_E_FileName.Text.Trim() + "'";
                }
                string sql = "insert into OM_Express (" + key + ") values (" + value + ")";
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                    btnSave.Visible = false;
                    btnSubmit.Visible = true;
                    PanelDetail.Enabled = false;
                    PanelShenhe.Enabled = false;
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据保存失败！');", true);
                }
            }

        }

        //申请单提交审批
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            if (unitparam.action == "add" || unitparam.action == "edit")
            {
                sqltext = "update OM_Express set E_State='1' where E_Code='" + lb_E_Code.Text.Trim() + "'";
                DBCallCommon.ExeSqlText(sqltext);
                if (unitparam.action == "add")
                    Response.Redirect("OM_ExpressManage.aspx");
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('申请已进入审批流程');window.opener.location.reload();window.close();", true);
                if (unitparam.action == "add")
                    SendMail(txt_E_SHRID.Text.Trim(), "快递", "审核", lb_E_Code.Text.Trim(), lb_E_ZDR.Text.Trim(), lb_E_ZDTime.Text.Trim());
            }
            else
            {
                if (string.IsNullOrEmpty(rbl_E_SHResult.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写审核意见');", true);
                    return;
                }
                sqltext = "update OM_Express set E_SHResult='" + rbl_E_SHResult.SelectedValue + "',E_SHNote='" + txt_E_SHNote.Text.Trim() + "',E_SHTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',E_State='" + rbl_E_SHResult.SelectedValue + "' where E_Code='" + unitparam.key + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核成功');window.opener.location.reload();window.close();", true);
                SendMail(hid_E_SurerID.Value, "快递", "反馈", unitparam.key, lb_E_ZDR.Text.Trim(), lb_E_ZDTime.Text.Trim());
            }
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核成功');window.opener=null;window.open('','_self');window.close();window.returnValue='refresh'", true);//本窗口打开新页面
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="emailto">收件人ID</param>
        /// <param name="theme">操作主题</param>
        /// <param name="operation">操作类型</param>
        /// <param name="code">单号</param>
        /// <param name="maker">制单人</param>
        /// <param name="time">制单日期</param>
        private void SendMail(string emailto, string theme, string operation, string code, string maker, string time)
        {
            string _emailto = DBCallCommon.GetEmailAddressByUserID(emailto);
            string _body = theme + operation + "任务:"
                  + "\r\n单号：" + code
                  + "\r\n制单人：" + maker
                  + "\r\n制单日期：" + time;

            string _subject = "您有新的【" + theme + "】需要" + operation + "，请及时处理";
            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
        }

        //行政专员确认
        protected void btnSure_OnClick(object sender, EventArgs e)
        {
            string sqltext = "update OM_Express set E_SureTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',E_ExpressCompany='" + ddl_E_ExpressCompany.SelectedValue + "',E_ExpressCode='" + txt_E_ExpressCode.Text.Trim() + "',E_ExpressWeight='" + txt_E_ExpressWeight.Text.Trim() + "',E_ExpressMoney='" + txt_E_ExpressMoney.Text.Trim() + "',E_ExpressTime='" + txt_E_ExpressTime.Text.Trim() + "',E_BackNote='" + txt_E_BackNote.Text.Trim() + "'";
            if (ckb_E_BackRefuse.Checked == true)
                sqltext += " ,E_BackRefuse='y',E_State='5' where E_Code='" + unitparam.key + "'";
            else
                sqltext += ",E_State='4' where E_Code='" + unitparam.key + "'";
            DBCallCommon.ExeSqlText(sqltext);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('反馈完成');window.opener.location.reload();window.close();", true);
        }

        //申请人名字改变
        protected void SQRName_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            //TextBox Tb_newstid = (TextBox)sender;
            //RepeaterItem Reitem = (RepeaterItem)Tb_newstid.Parent;
            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["DEP_NAME"].ToString().Trim() != lb_E_SQRDep.Text)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('申请人和制单人必须为同一部门人员！');", true);
                        txt_E_SQR.Text = "";
                        txt_E_SQR.Focus();
                        return;
                    }
                    txt_E_SQRID.Text = stid;
                    txt_E_SQR.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lb_E_SQRDep.Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                    // ((Label)Reitem.FindControl("txtTA_SQRDepPos")).Text = dt.Rows[0]["DEP_POSITION"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入正确的姓名，从弹出的姓名下拉框选择！');", true);
                txt_E_SQR.Text = "";
                txt_E_SQR.Focus();
            }
        }

        //快递类型改变
        protected void ddl_E_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_E_Type.SelectedValue == "0")
            {
                td_wenjian0.Visible = true;
                td_wenjian1.Visible = true;
                td_wuping0.Visible = false;
                td_wuping1.Visible = false;
                td_wuping2.Visible = false;
                td_wuping3.Visible = false;
            }
            else
            {
                td_wenjian0.Visible = false;
                td_wenjian1.Visible = false;
                td_wuping0.Visible = true;
                td_wuping1.Visible = true;
                td_wuping2.Visible = true;
                td_wuping3.Visible = true;
            }
        }

        //返回
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            if (unitparam.action == "add")
                Response.Redirect("OM_ExpressManage.aspx");
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location.reload();window.close();", true);
        }
    }
}
