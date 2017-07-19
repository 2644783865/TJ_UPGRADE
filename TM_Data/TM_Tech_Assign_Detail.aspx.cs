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
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Tech_Assign_Detail : System.Web.UI.Page
    {
        string tmdetail_id;
        string sqlText;
        List<string> str = new List<string>();
        string _emailto;
        string _body;
        string _subject;
        protected void Page_Load(object sender, EventArgs e)
        {
            tmdetail_id = Request.QueryString["tmdetail_id"];
            if (!IsPostBack)
            {
                InitInfo();
                ShowInfo(); //绑定设备基本信息

            }
        }

        private void ShowInfo()
        {
            string sqldata = "select * from TBCM_BASIC where TSA_ID='" + tsaid.Text.ToString().Trim() + "'";
            DataTable data = DBCallCommon.GetDTUsingSqlText(sqldata);
            Repeater1.DataSource = data;
            Repeater1.DataBind();
        }
        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitInfo()
        {
            //TSA_ID, TSA_PJID, TSA_ENGNAME, TSA_ENGSTRTYPE, TSA_TCCLERK, TSA_ENGTYPE, TSA_DRAWCODE, TSA_DEVICECODE, TSA_DESIGNCOM, TSA_MODELCODE, TSA_RECVDATE, TSA_CONTYPE, TSA_DRAWSTATE, TSA_STARTDATE, TSA_PAINTINGPLAN, TSA_MANCLERK, TSA_STATE, Expr1, TSA_TCCLERKNM, TSA_MANCLERKNAME, TSA_STATUS, TSA_ENGSTRSMTYPE, TSA_ADDTIME, TSA_NUMBER, ST_NAME,, TSA_NOTE, TSA_BUYER, TSA_PARTNAME, TSA_PARTTUHAO, TSA_DELIVERYTIME, TSA_STARTTIME, TSA_MSFINISHTIME, TSA_MPCOLLECTTIME, TSA_TECHTIME, TSA_TUZHUANGTIME, TSA_ZHUANGXIANGDANTIME

            sqlText = "select TSA_ID,CM_PROJ,TSA_ENGNAME,TSA_NUMBER,TSA_PJID,TSA_BUYER,TSA_MODELCODE,TSA_DEVICECODE,TSA_DESIGNCOM,TSA_CONTYPE,TSA_RECVDATE,TSA_DRAWSTATE,TSA_DELIVERYTIME,TSA_TCCLERKNM,TSA_MANCLERKNAME,TSA_STARTTIME,TSA_MSFINISHTIME,TSA_MPCOLLECTTIME,TSA_TECHTIME,TSA_TUZHUANGTIME,TSA_ZHUANGXIANGDANTIME,TSA_STARTDATE,TSA_STATE,TSA_ADDTIME,TSA_REVIEWER,TSA_YQREVIEWER,TSA_YQHZR,TSA_REVIEWERID,TSA_YQREVIEWERID,TSA_YQHZRID,TSA_TCCLERK";
            sqlText += " from View_TM_TaskAssign where TSA_ID='" + tmdetail_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                #region
                txtContractId.Text = dr["TSA_PJID"].ToString();
                tsaid.Text = dr["TSA_ID"].ToString();
                txtProname.Text = dr["CM_PROJ"].ToString();
                txtBuyer.Text = dr["TSA_BUYER"].ToString();
                txtEngname.Text = dr["TSA_ENGNAME"].ToString();
                txtModelcode.Text = dr["TSA_MODELCODE"].ToString();
                txtDevicecode.Text = dr["TSA_DEVICECODE"].ToString();
                designcom.Text = dr["TSA_DESIGNCOM"].ToString();
                txt_second.Value = dr["TSA_REVIEWER"].ToString();
                recvdate.Value = dr["TSA_RECVDATE"].ToString();
                drawstate.Value = dr["TSA_DRAWSTATE"].ToString();
                txtDeliveryData.Value = dr["TSA_DELIVERYTIME"].ToString();
                txt_first.Value = dr["TSA_TCCLERKNM"].ToString();
                startdate.Value = dr["TSA_STARTDATE"].ToString();
                txtMSComTime.Value = dr["TSA_MSFINISHTIME"].ToString();
                txtMPTime.Value = dr["TSA_MPCOLLECTTIME"].ToString();
                txtTecTime.Value = dr["TSA_TECHTIME"].ToString();
                txtTuZhuangTime.Value = dr["TSA_TUZHUANGTIME"].ToString();
                txtZhuangXiangDanTime.Value = dr["TSA_ZHUANGXIANGDANTIME"].ToString();
                rblstatus.SelectedValue = dr["TSA_STATE"].ToString();
                txt_third.Value = dr["TSA_YQREVIEWER"].ToString();
                txt_fourth.Value = dr["TSA_YQHZR"].ToString();
                secondid.Value = dr["TSA_REVIEWERID"].ToString();
                thirdid.Value = dr["TSA_YQREVIEWERID"].ToString();
                fourthid.Value = dr["TSA_YQHZRID"].ToString();
                firstid.Value = dr["TSA_TCCLERK"].ToString();
                if (txt_first.Value != "" && Convert.ToInt16(rblstatus.SelectedValue) > 0)
                {
                    keyid.Value = "1";//避免主键约束报错
                }
                #endregion
            }
            dr.Close();
            this.InitControlState();
        }
        /// <summary>
        /// 初始化控件状态
        /// </summary>
        private void InitControlState()
        {
            if (Convert.ToInt16(rblstatus.SelectedValue) > 0)
            {
                rblstatus.Items[0].Enabled = false;

            }
            //A如果是待分工，无法选择停工和完工，类型可修改
            else if (rblstatus.SelectedValue == "0")
            {
                rblstatus.Items[2].Enabled = false;
                rblstatus.Items[3].Enabled = false;

            }
            //完工或停工无法指定技术员
            if (Convert.ToInt16(rblstatus.SelectedValue) > 1 && Convert.ToInt16(rblstatus.SelectedValue) < 4)
            {
                hlSelect.Visible = false;
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
                List<string> sqlist = new List<string>();
                string sqlText_TaskAssign = "";

                sqlText_TaskAssign = "update TBPM_TCTSASSGN set TSA_BUYER='" + txtBuyer.Text.Trim() + "',";
                sqlText_TaskAssign += "TSA_MODELCODE='" + txtModelcode.Text.Trim() + "',TSA_DEVICECODE='" + txtDevicecode.Text.Trim() + "',";

                sqlText_TaskAssign += "TSA_DESIGNCOM='" + designcom.Text.Trim() + "',TSA_REVIEWERID='" + secondid.Value.Trim() + "',TSA_REVIEWER='" + txt_second.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_YQREVIEWERID='" + thirdid.Value.Trim() + "',TSA_YQREVIEWER='" + txt_third.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_YQHZRID='" + fourthid.Value.Trim() + "',TSA_YQHZR='" + txt_fourth.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_RECVDATE='" + recvdate.Value.Trim() + "',TSA_DRAWSTATE='" + drawstate.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_DELIVERYTIME='" + txtDeliveryData.Value.Trim() + "',TSA_TCCLERK='" + firstid.Value + "',";

                sqlText_TaskAssign += "TSA_STARTTIME='" + startdate.Value.Trim() + "',TSA_MSFINISHTIME='" + txtMSComTime.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_MPCOLLECTTIME='" + txtMPTime.Value.Trim() + "',TSA_TECHTIME='" + txtTecTime.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_TUZHUANGTIME='" + txtTuZhuangTime.Value.Trim() + "',TSA_ZHUANGXIANGDANTIME='" + txtZhuangXiangDanTime.Value.Trim() + "',";
                sqlText_TaskAssign += "TSA_STATE='" + rblstatus.SelectedValue + "',TSA_MANCLERK='" + Session["UserID"] + "',TSA_STARTDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where TSA_ID='" + tmdetail_id + "'";

                sqlist.Add(sqlText_TaskAssign);

                #region 验证是否技术任务已保存但未提交时任务负责人修改

                string mpidsql = "update TBPM_MPFORALLRVW set MP_SUBMITID='" + firstid.Value.Trim() + "' where MP_ENGID='" + tmdetail_id + "'and MP_STATE in ('0','1')";
                sqlist.Add(mpidsql);
                string msidsql = "update TBPM_MSFORALLRVW set MS_SUBMITID='" + firstid.Value.Trim() + "' where MS_ENGID='" + tmdetail_id + "'and MS_STATE in ('0','1')";
                sqlist.Add(msidsql);
                string mpchangeidsql = "update TBPM_MPCHANGERVW set MP_SUBMITID='" + firstid.Value.Trim() + "' where MP_ENGID='" + tmdetail_id + "'and MP_STATE in ('0','1')";
                sqlist.Add(mpchangeidsql);
                string mschangeidsql = "update TBPM_MSCHANGERVW set MS_SUBMITID='" + firstid.Value.Trim() + "' where MS_ENGID='" + tmdetail_id + "'and MS_STATE in ('0','1')";
                sqlist.Add(mschangeidsql);
                string paintidsql = "update TBPM_PAINTSCHEME set PS_SUBMITID='" + fourthid.Value.Trim() + "' where PS_ENGID='" + tmdetail_id + "'and PS_STATE in ('0','1')";
                sqlist.Add(paintidsql);

                #endregion

                #region 主生产计划任务开始
                sqlText = "update TBMP_MAINPLANDETAIL set MP_STATE=1 where MP_ENGID='" + tsaid.Text.Trim() + "' and MP_TYPE='技术准备'";
                sqlist.Add(sqlText);
                #endregion

                DBCallCommon.ExecuteTrans(sqlist);

                #region 发送邮件
                _emailto = DBCallCommon.GetEmailAddressByUserID(firstid.Value.Trim());

                _body = "技术任务分工:"
                       + "\r\n合 同 号：" + txtContractId.Text.Trim()
                       + "\r\n项目名称：" + txtProname.Text.Trim() + "_" + tsaid.Text.Trim() + ""
                       + "\r\n设备名称：" + txtEngname.Text.Trim()
                       + "\r\n技 术 员: " + txt_first.Value.Trim()
                       + "\r\n接收日期:" + recvdate.Value.Trim();

                _subject = "您有新的任务分工，请及时处理 " + txtProname.Text.Trim() + "_" + tsaid.Text.Trim() + "" + "_" + txtEngname.Text.Trim();
                DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                #endregion


                #region 发送邮件油漆
              string  _emailtoyq = DBCallCommon.GetEmailAddressByUserID(fourthid.Value.Trim());

              
                DBCallCommon.SendEmail(_emailtoyq, null, null, _subject, _body);
                #endregion


                Response.Redirect("TM_Tech_assign.aspx");
                //}
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:进行中，必须指定评审人！！！');", true);
            }
            //2016.6.30 改
            else if (ret.Contains("4PersonUnSelect"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:进行中，必须指定涂装-油漆负责人！！！');", true);
            }
            else if (ret.Contains("3PersonUnSelect"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:进行中，必须指定涂装-油漆评审人！！！');", true);
            }
            else if (ret.Contains("NUM"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:台数必须为数字格式');", true);
            }
            else if (ret.Contains("VisuralTask"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:该任务为虚拟任务，请选择对应选项！');", true);
            }

        }
        /// <summary>
        /// 保存检查
        /// </summary>
        /// <returns></returns>
        protected string CheckSave()
        {
            if (txtContractId.Text.Trim() == "JSB.BOM001")
            {
                if (rblstatus.SelectedValue != "4")
                {
                    return "VisuralTask";
                }
            }
            else
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
                    else if (secondid.Value.Trim() == "")//未选择技术审批人
                    {
                        return "2PersonUnSelect";
                    }
                    //  2016.6.30改
                    else if (fourthid.Value.Trim() == "" && (!string.IsNullOrEmpty(thirdid.Value.ToString().Trim())))//未选择涂装-油漆负责人
                    {
                        return "4PersonUnSelect";
                    }
                    else if (thirdid.Value.Trim() == "" && (!string.IsNullOrEmpty(fourthid.Value.ToString().Trim())))//未选择涂装-油漆评审人
                    {
                        return "3PersonUnSelect";
                    }

                }
                else if (rblstatus.SelectedValue == "2")//完工
                {
                    //if (realfishdate.Text.Trim() == "")
                    //{
                    //    return "3TimeLost";
                    //}
                }

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
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Header")
            {
                if (str.Count < 1)
                {
                    str.Add(((DataRowView)e.Item.DataItem).Row[1].ToString());
                }
                else
                {
                    if (str.Contains(((DataRowView)e.Item.DataItem).Row[1].ToString()))
                    {
                        ((DataRowView)e.Item.DataItem).Row[1] = "";
                        e.Item.DataBind();
                    }
                    else
                    {
                        str.Add(((DataRowView)e.Item.DataItem).Row[1].ToString());
                    }
                }
                ((DataRowView)e.Item.DataItem).Row.AcceptChanges();
            }
        }



    }
}