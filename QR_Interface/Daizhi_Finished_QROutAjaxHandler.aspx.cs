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
    public partial class Daizhi_Finished_QROutAjaxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string DzCPQROutData = Request.Form["DzCPQROutData"].ToString().Trim();
            string DzCPQROutNum = Request.Form["DzCPQROutNum"].ToString().Trim();
            string TaskCode_Out = Request.Form["TaskCode_Out"].ToString().Trim();
            string RealAddrs_Out = Request.Form["RealAddrs_Out"].ToString().Trim();

            string DfReason_Out = Request.Form["DfReason_Out"].ToString().Trim();
            string Note_Out = Request.Form["Note_Out"].ToString().Trim();
            string DzCPQROutPerson = Request.Form["DzCPQROutPerson"].ToString().Trim();
            List<string> list = new List<string>();
            string result = "";
            string sqlInsert = "";
            string sqlUpdate = "";
            string[] arrData = DzCPQROutData.Split(';');

            //已出库数量和本次出库数量
            int hasOutNum = 0;
            decimal hasOutMoney=(decimal)0.00;
            string sql = "select *,(InNum-OutNum) as StorageNum,(InMoney-OutMoney) as StorageMoney from midTable_DzFinished_management where QRDzID=" + CommonFun.ComTryInt(arrData[0].ToString().Trim()) + "";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                hasOutNum = CommonFun.ComTryInt(dt.Rows[0]["OutNum"].ToString().Trim());
                hasOutMoney = CommonFun.ComTryDecimal(dt.Rows[0]["OutMoney"].ToString().Trim());
            }
            int cknum = CommonFun.ComTryInt(DzCPQROutNum);
            int kcnum = CommonFun.ComTryInt(dt.Rows[0]["StorageNum"].ToString().Trim());

            decimal InMoney = CommonFun.ComTryDecimal(dt.Rows[0]["InMoney"].ToString().Trim());
            decimal StorageMoney = CommonFun.ComTryDecimal(dt.Rows[0]["StorageMoney"].ToString().Trim());

            decimal ThisOutMoney = (decimal)0.00;
            decimal TotalOutMony = (decimal)0.00;


            if (checkIfEnough(cknum, kcnum))
            {
                //累加出库数量，计算累计出库金额
                if (cknum == kcnum)
                {
                    ThisOutMoney = InMoney - hasOutMoney;
                    TotalOutMony = InMoney;
                }
                else
                {
                    ThisOutMoney = cknum * StorageMoney / kcnum;
                    TotalOutMony = hasOutMoney + ThisOutMoney;
                }
                //插入出库数据
                sqlInsert = "insert into midTable_DzFinished_Out(UniqID_Out, OldYzName_Out, OldHtCode_Out, OldTaskCode_Out, OldYzHtCode_Out, OldProjName_Out, ProdName_Out, MapCode_Out, Caizhi_Out, OutProdCode_Out, SingleWeight_Out, OutNum_Out, OutUnit_Out, Money_Out, RealAddrs_Out, TaskCode_Out, SmPerson_Out, OutTime_Out, ZnZw_Out, IfERP_Out, DfReason_Out, Note_Out) values(" + CommonFun.ComTryInt(arrData[0].ToString().Trim()) + ",'" + arrData[1].ToString().Trim() + "','" + arrData[2].ToString().Trim() + "','" + arrData[3].ToString().Trim() + "','" + arrData[4].ToString().Trim() + "','" + arrData[5].ToString().Trim() + "','" + arrData[6].ToString().Trim() + "','" + arrData[7].ToString().Trim() + "','" + arrData[8].ToString().Trim() + "','" + arrData[9].ToString().Trim() + "'," + CommonFun.ComTryDecimal(arrData[13].ToString().Trim()) + "," + cknum + ",'" + arrData[11].ToString().Trim() + "'," + ThisOutMoney + ",'" + arrData[18].ToString().Trim() + "','" + arrData[17].ToString().Trim() + "','" + DfReason_Out + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + arrData[14].ToString().Trim() + "','" + arrData[15].ToString().Trim() + "','" + arrData[19].ToString().Trim() + "','" + arrData[20].ToString().Trim() + "')";
                list.Add(sqlInsert);

                //更新汇总数据
                sqlUpdate = "update midTable_DzFinished_management set OutNum=OutNum+" + cknum + ",OutMoney=" + TotalOutMony + " where QRDzID=" + CommonFun.ComTryInt(arrData[0].ToString().Trim()) + "";
                list.Add(sqlUpdate);
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
                result = "{\"result\":\"fault\",\"msg\":\"出库数量不得大于对应的库存数量!\"}";
            }
            Response.Write(result);
        }

        //库存和出库数量验证
        private bool checkIfEnough(int cknum, int kcnum)
        {
            if (cknum > kcnum)
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
