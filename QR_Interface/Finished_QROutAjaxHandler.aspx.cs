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
    public partial class Finished_QROutAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CPQROutData = Request.Form["CPQROutData"].ToString().Trim();
            string CPQROutNum_Str = Request.Form["CPQROutNum"].ToString().Trim();
            string CPnote = Request.Form["CPnote"].ToString().Trim();
            string CPQROutPerson = Request.Form["CPQROutPerson"].ToString().Trim();
            List<string> list = new List<string>();
            string result = "";
            string sqlInsert = "";
            string[] arrData = CPQROutData.Split(';');
            int CPQROutNum = Convert.ToInt32(CPQROutNum_Str);//出庫数量
            string CPQROut_Time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();//扫码时间

            int kcnum = Convert.ToInt32(arrData[12].ToString().Trim());
            int yf = Convert.ToInt32(arrData[11].ToString().Trim());
            if (checkIfEnough(CPQROutNum, kcnum, yf))
            {
                sqlInsert = "insert into midTable_Finished_QROut(CPQROut_TaskID, CPQROut_Zongxu, CPQROut_Num, CPQROut_Time, CPQROut_Person, CPQROut_Note, CPQROut_State) values('" + arrData[3].ToString().Trim() + "','" + arrData[4].ToString().Trim() + "'," + CPQROutNum + ",'" + CPQROut_Time + "','" + CPQROutPerson + "','" + CPnote + "','0')";
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
                result = "{\"result\":\"fault\",\"msg\":\"出库数量不得大于已比价数量或库存数量!\"}";
            }
            Response.Write(result);
        }
        //库存和比价数量验证
        private bool checkIfEnough(int cknum, int kcnum, int yf)
        {
            if (cknum > kcnum || cknum > yf)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
