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
    public partial class Daizhi_Finished_QRInAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string DzCPQRInData = Request.Form["DzCPQRInData"].ToString().Trim();
            string DzCPQRInNum = Request.Form["DzCPQRInNum"].ToString().Trim();
            string Money_In = Request.Form["Money_In"].ToString().Trim();
            string Note_In = Request.Form["Note_In"].ToString().Trim();
            string DzCPQRInPerson = Request.Form["DzCPQRInPerson"].ToString().Trim();

            List<string> list = new List<string>();
            string result = "";
            string sqlInsert = "";
            string[] arrData = DzCPQRInData.Split(';');

            //插入数据
            sqlInsert = "insert into midTable_DzFinished_management(OldYzName, OldHtCode, OldTaskCode, OldYzHtCode, OldProjName, ProdName, MapCode, Caizhi, InProdCode, SmPerson, InTime, InNum, InUnit, InMoney, SingleWeight, ZnZw, IfERP, InNote) values('" + arrData[0].ToString().Trim() + "','" + arrData[1].ToString().Trim() + "','" + arrData[2].ToString().Trim() + "','" + arrData[3].ToString().Trim() + "','" + arrData[4].ToString().Trim() + "','" + arrData[5].ToString().Trim() + "','" + arrData[6].ToString().Trim() + "','" + arrData[7].ToString().Trim() + "','" + arrData[8].ToString().Trim() + "','" + DzCPQRInPerson + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "'," + CommonFun.ComTryInt(DzCPQRInNum) + ",'" + arrData[10].ToString().Trim() + "'," + CommonFun.ComTryDecimal(Money_In) + "," + CommonFun.ComTryDecimal(arrData[12].ToString().Trim()) + ",'" + arrData[13].ToString().Trim() + "','" + arrData[14].ToString().Trim() + "','" + Note_In + "')";
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
