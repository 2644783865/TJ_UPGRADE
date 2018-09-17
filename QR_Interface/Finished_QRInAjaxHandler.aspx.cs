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
    public partial class Finished_QRInAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CPQRInData = Request.Form["CPQRInData"].ToString().Trim();
            string CPQRInNum_Str = Request.Form["CPQRInNum"].ToString().Trim();
            string CPnote = Request.Form["CPnote"].ToString().Trim();
            string CPQRInPerson = Request.Form["CPQRInPerson"].ToString().Trim();

            List<string> list = new List<string>();
            string result = "";
            string sqlInsert = "";
            string[] arrData = CPQRInData.Split(';');
            int CPQRIn_Num = Convert.ToInt32(CPQRInNum_Str);//入庫数量
            string CPQRIn_Time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();//扫码时间

            if (checkIfInstate(arrData[2].ToString().Trim(), arrData[3].ToString().Trim()))
            {
                sqlInsert = "insert into midTable_Finished_QRIn(CPQRIn_TaskID, CPQRIn_Zongxu, CPQRIn_Num, CPQRIn_Time, CPQRIn_Person, CPQRIn_Note, CPQRIn_State) values('" + arrData[2].ToString().Trim() + "','" + arrData[3].ToString().Trim() + "'," + CPQRIn_Num + ",'" + CPQRIn_Time + "','" + CPQRInPerson + "','" + CPnote + "','0')";
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
            }
            else
            {
                result = "{\"result\":\"fault\",\"msg\":\"该成品已入库或正在审核，请勿重复入库!\"}";
            }
            Response.Write(result);
        }

        //检查入库状态
        private bool checkIfInstate(string CurTa, string ZongXu)
        {
            string sqlcheckrk = "select BM_ZONGXU,BM_TUHAO,BM_CHANAME,BM_NUMBER,BM_TUUNITWGHT,BM_YRKNUM,BM_KU,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state from dbo.View_TM_DQO where  BM_MSSTATUS<>'1'  and BM_ENGID='" + CurTa + "' and BM_ZONGXU='" + ZongXu + "' and (BM_KU like '%S%' or BM_MARID='' )";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlcheckrk);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BM_NUMBER"].ToString().Trim() == dt.Rows[0]["BM_YRKNUM"].ToString().Trim())
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
                return true;
            }
        }
        //质检结果是否需要验证？以前有提醒但没有强制验证
    }
}
