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
using System.Collections.Generic;

namespace ZCZJ_DPF.QR_Interface
{
    public partial class QROutAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string QROutData = Request.Form["QROutData"].ToString().Trim();
            string QROutNum_Str = Request.Form["QROutNum"].ToString().Trim();
            string TaskID = Request.Form["TaskID"].ToString().Trim();
            string note = Request.Form["note"].ToString().Trim();
            string QROutPerson=Request.Form["QROutPerson"].ToString().Trim();
            SaveData(QROutData, QROutNum_Str, TaskID, note, QROutPerson);
        }
        private void SaveData(string QROutData, string QROutNum_Str, string TaskID, string note, string QROutPerson)
        {
            List<string> list = new List<string>();
            string result = "";
            string sqlInsert = "";
            string[] arrData = QROutData.Split(';');
            string SQ_MARID = arrData[0].ToString().Trim();
            decimal QROut_Num = Convert.ToDecimal(QROutNum_Str);//实发数量，由扫码人员填写并传递过来
            string QROut_Time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();//扫码时间
            if (checkIfEnough(SQ_MARID, QROut_Num))
            {
                //if (checkTaskID(TaskID))
                //{
                    //数据插入扫码出库物料中间表
                    sqlInsert = "insert into midTable_QROut(QROut_MatCode, QROut_SQCODE, QROut_Num, QROut_Time, QROut_State, QROut_WHSTATE, QROut_TaskID, QROut_Note,QROut_Person) values('" + SQ_MARID + "',''," + QROut_Num + ",'" + QROut_Time + "','0','0','" + TaskID + "','" + note + "','" + QROutPerson + "')";
                    list.Add(sqlInsert);
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        result = "{\"result\":\"success\",\"msg\":\"操作成功!\"}";
                    }
                    catch
                    {
                        result = "{\"result\":\"fault\",\"msg\":\"程序执行出现错误!\"}";
                    }
                //}
                //else
                //{
                //    result = "{\"result\":\"fault\",\"msg\":\"任务号不存在!\"}";
                //}
            }
            else
            {
                result = "{\"result\":\"fault\",\"msg\":\"单条物料库存不足!\"}";
            }
            Response.Write(result);
        }
        //检查物料库存够不够
        private bool checkIfEnough(string SQ_MARID, decimal QROut_Num)
        {
            string sqlText = "select max(SQ_NUM) as SQ_NUM from TBWS_STORAGE where SQ_MARID='" + SQ_MARID + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SQ_NUM"].ToString().Trim() != "")
                {
                    if (QROut_Num > Convert.ToDecimal(dt.Rows[0]["SQ_NUM"].ToString().Trim()))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //检查任务号是否存在
        private bool checkTaskID(string TaskID)
        {
            string strsql = "select TSA_ID from (SELECT TSA_ID FROM View_TM_TaskAssign union select TSA_ID from TBPM_DETAIL)t where TSA_ID='" + TaskID + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
