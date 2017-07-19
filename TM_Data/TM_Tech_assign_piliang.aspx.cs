using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Tech_assign_piliang : System.Web.UI.Page
    {
        string sqlText;
        string whereConstr;
        string taskID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string taskID = Request.QueryString["taskID"];
            if (!IsPostBack)
            {
                tsaid.Text = taskID.Trim('/');

            }
        }
        /// <summary>
        /// 确认保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string ret = this.CheckSave();
            if (ret.Contains("OK"))
            {
                whereConstr = tsaid.Text.Replace("/", "','");
                whereConstr = "('" + whereConstr + "')";

                List<string> sqlist = new List<string>();
                string sqlText_TaskAssign = "";
                sqlText_TaskAssign = "update TBPM_TCTSASSGN set TSA_REVIEWERID='" + secondid.Value.Trim() + "',TSA_REVIEWER='" + txt_second.Value.Trim() + "',TSA_TCCLERK='" + firstid.Value + "',";
                sqlText_TaskAssign += "TSA_YQREVIEWERID='" + thirdid.Value.Trim() + "',TSA_YQREVIEWER='" + txt_third.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_YQHZRID='" + fourthid.Value.Trim() + "',TSA_YQHZR='" + txt_fourth.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_STARTTIME='" + startdate.Value.Trim() + "',TSA_MSFINISHTIME='" + txtMSComTime.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_MPCOLLECTTIME='" + txtMPTime.Value.Trim() + "',TSA_TECHTIME='" + txtTecTime.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_TUZHUANGTIME='" + txtTuZhuangTime.Value.Trim() + "',TSA_ZHUANGXIANGDANTIME='" + txtZhuangXiangDanTime.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_STATE='" + rblstatus.SelectedValue + "',TSA_MANCLERK='" + Session["UserID"] + "' where TSA_ID in " + whereConstr ;

                sqlist.Add(sqlText_TaskAssign);
                #region 主生产计划任务开始
                //sqlText = "update TBMP_MAINPLANDETAIL set MP_STATE=1 where MP_ENGID in " + whereConstr + " and MP_TYPE='技术准备'";
                //sqlist.Add(sqlText);
                #endregion
                DBCallCommon.ExecuteTrans(sqlist);
                sqlText = "select TSA_ID,CM_PROJ,TSA_ENGNAME,TSA_NUMBER,TSA_PJID,TSA_BUYER,TSA_MODELCODE,TSA_DEVICECODE,TSA_DESIGNCOM,TSA_CONTYPE,TSA_RECVDATE,TSA_DRAWSTATE,TSA_DELIVERYTIME,TSA_TCCLERKNM,TSA_MANCLERKNAME,TSA_STARTTIME,TSA_MSFINISHTIME,TSA_MPCOLLECTTIME,TSA_TECHTIME,TSA_TUZHUANGTIME,TSA_ZHUANGXIANGDANTIME,TSA_STARTDATE,TSA_STATE,TSA_ADDTIME,TSA_REVIEWER,TSA_YQREVIEWER,TSA_YQHZR";
                sqlText += " from View_TM_TaskAssign where TSA_ID='" + taskID + "'";
               DataTable dt= DBCallCommon.GetDTUsingSqlText(sqlText);
               if (dt.Rows.Count>0)
               {
                   DataRow dr = dt.Rows[0];
                   string txtContractId = dr["TSA_PJID"].ToString();
                   string tsaid1 = dr["TSA_ID"].ToString();
                   string txtProname = dr["CM_PROJ"].ToString();
                   string txtEngname = dr["TSA_ENGNAME"].ToString();
                   string recvdate = dr["TSA_RECVDATE"].ToString();
                   #region 发送邮件
                   string _emailto = DBCallCommon.GetEmailAddressByUserID(firstid.Value.Trim());

                   string _body = "技术任务分工:"
                           + "\r\n合 同 号：" + txtContractId.Trim()
                           + "\r\n项目名称：" + txtProname.Trim() + "_" + tsaid1.Trim() + ""
                           + "\r\n设备名称：" + txtEngname.Trim()
                           + "\r\n技 术 员: " + txt_first.Value.Trim()
                           + "\r\n接收日期:" + recvdate.Trim();

                   string _subject = "您有新的任务分工，请及时处理 " + txtProname.Trim() + "_" + tsaid1.Trim() + "" + "_" + txtEngname.Trim();
                   DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                   #endregion
                   #region 发送邮件油漆
                   string _emailtoyq = DBCallCommon.GetEmailAddressByUserID(fourthid.Value.Trim());


                   DBCallCommon.SendEmail(_emailtoyq, null, null, _subject, _body);
                   #endregion
               }
  
                Response.Redirect("TM_Tech_assign.aspx");
            }

            else if (ret.Contains("0"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:待分工，无法指定技术员！！！');", true);
            }
            else if (ret.Contains("1PersonUnSelect"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:进行中，必须指定技术负责人！！！');", true);
            }
            else if (ret.Contains("2PersonUnSelect"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:进行中，必须指定技术评审人！！！');", true);
            }
            else if (ret.Contains("3TimeLost"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:完工，必须指定实际完工时间！！！');", true);
            }
            else if (ret.Contains("NUM"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:台数必须为数字格式');", true);
            }

        }
        /// <summary>
        /// 保存检查
        /// </summary>
        /// <returns></returns>
        protected string CheckSave()
        {
            if (rblstatus.SelectedValue == "0")//待分工
            {
                if (txt_first.Value.Trim() != "" || firstid.Value.Trim() != "")
                {
                    return "0";
                }
                else if (txt_second.Value.Trim() != "" || secondid.Value.Trim() != "")
                {
                    return "1";
                }
            }
            else if (rblstatus.SelectedValue == "1")//进行中
            {
                if (firstid.Value.Trim() == "")//未选择技术员
                {
                    return "1PersonUnSelect";
                }
                else if (secondid.Value.Trim() == "")//未选择技术员
                {
                    return "2PersonUnSelect";
                }

            }
            else if (rblstatus.SelectedValue == "2")//完工
            {
                //if (realfishdate.Text.Trim() == "")
                //{
                //    return "3TimeLost";
                //}
            }


            return "OK";



        }
        /// <summary>
        /// 取消返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TM_Tech_assign.aspx");
        }
    }
}
