using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_AjaxHandler : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqlText;
        string result;
        protected void Page_Load(object sender, EventArgs e)
        {
            String methodName = Request["method"];
            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);

            if (method == null) throw new Exception("method is null");

            try
            {

                method.Invoke(this, null);
            }
            catch
            {
                throw;
            }
        }

        public void BackTask()
        {
            string ptc = Request.Form["ptc"].ToString();
            string note = Request.Form["txtNote"].ToString();
           
            List<string> list = new List<string>();

            string[] arrPTC = ptc.Trim('|').Split('|');
            for (int i = 0; i < arrPTC.Length; i++)
            {
                string sonPTC = arrPTC[i];
                string engid = sonPTC.Split('_')[sonPTC.Split('_').Length - 3];


                //将计划驳回至技术部，采购部数据更改状态，隐藏在界面，保留原数据
                sqlText = "update TBPC_PURCHASEPLAN set PUR_STATE='8' where PUR_PTCODE='" + sonPTC + "'";
                list.Add(sqlText);
                if (sonPTC.Contains("XZ"))
                {
                    sqlText = "update TBPC_OTPURPLAN set MP_STATE='1' where MP_PTCODE='" + sonPTC + "'";
                    list.Add(sqlText);
                }
                else
                {
                    sqlText = "update TBPM_MPPLAN set MP_STATE='1' where MP_TRACKNUM='" + sonPTC + "'";
                    list.Add(sqlText);
                    sqlText = " update TBPM_STRINFODQO set BM_MPSTATE='0', BM_MPREVIEW='0' where BM_ENGID='" + engid + "' and BM_XUHAO in(select MP_NEWXUHAO from TBPM_MPPLAN where MP_TRACKNUM='" + sonPTC + "')";
                    list.Add(sqlText);
                }

                //Aptcode, Bptcode, marid, marnm, margg, marcz, margb, length, width, pjnm, pjid, engid, engnm, depid, depnm, sqrid, sqrnm, sqrtime, pur_shape, PUR_TUHAO, planno, PUR_ZYDY, PUR_CSTATE, prstate, num, fznum, PUR_MASHAPE, PUR_ID, purstate, marunit, marfzunit
                sqlText = "insert into TBPC_PLAN_BACK select Aptcode, marid, length, width, pjid, engid, engnm, depid, depnm, sqrid, sqrnm, sqrtime, pur_shape, PUR_TUHAO, planno, num, fznum, marfzunit,'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["UserID"] + "','" + Session["UserName"] + "','0','" + note + "' from View_TBPC_PLAN_PLACE where Aptcode='" + sonPTC + "'";
                list.Add(sqlText);
            }
            result = "{\"msg\":\"变更成功，请刷新页面！\"}";
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch (Exception)
            {

                result = "{\"msg\":\"系统数据出错，请联系管理员！\"}";
            }

            Response.Write(result);
        }
    }
}
