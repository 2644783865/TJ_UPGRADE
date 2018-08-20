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
    public partial class QRInAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string QRInData = Request.Form["QRInData"].ToString().Trim();
            string QRInNum_Str = Request.Form["QRInNum"].ToString().Trim();
            string note = Request.Form["note"].ToString().Trim();
            SaveData(QRInData, QRInNum_Str,note);
        }
        private void SaveData(string QRInData, string QROutNum_Str,string note)
        {
            List<string> list = new List<string>();
            string result = "";
            string sqlInsert = "";
            string[] arrData = QRInData.Split(';');
            decimal QRIn_Num = Convert.ToDecimal(QROutNum_Str);//实收数量
            string QRIn_Time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();//扫码时间
            //数据插入扫码入库物料中间表
            sqlInsert = "insert into midTable_QRIn(QRIn_MatCode, QRIn_PTC, QRIn_Num, QRIn_Time, QRIn_State, QRIn_WHSTATE, QRIn_Note) values('" + arrData[0].ToString().Trim() + "',''," + QRIn_Num + ",'" + QRIn_Time + "','0','0','"+note+"')";
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
            Response.Write(result);
        }
    }
}
