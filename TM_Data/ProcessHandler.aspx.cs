using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;

namespace ZCZJ_DPF.TM_Data
{

    public partial class ProcessHandler : System.Web.UI.Page
    {
        string txtEngName;
        string txtEngModel;
        string txtPartName;
        string txtTuHao;
        string hidGuanLianTime;
        string txtBZ;
        string proId;
        string sqlText;
        string result;
        string txtName;
        string txtBANCI;
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
        //工艺类卡片新增
        public void ProcessCardNew()
        {
            txtEngName = Request["txtEngName"];
            txtEngModel = Request["txtEngModel"];
            txtPartName = Request["txtPartName"];
            txtTuHao = Request["txtTuHao"];
            hidGuanLianTime = Request["hidGuanLianTime"];
            txtBZ = Request["txtBZ"];
            proId = Request["proId"];
            txtBANCI = Request["proBanci"];
            sqlText = "insert into TBPM_PROCESS_CARD(PRO_ENGNAME, PRO_ENGMODEL, PRO_PARTNAME, PRO_TUHAO, PRO_FUJIAN, PRO_ISSUEDTIME, PRO_NOTE, PRO_SUBMITID,PRO_STATE,PRO_CHECKLEVEL,PRO_BANCI) values('" + txtEngName + "','" + txtEngModel + "','" + txtPartName + "','" + txtTuHao + "','" + hidGuanLianTime + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + txtBZ + "','" + Session["UserID"].ToString() + "','1','2','" + txtBANCI + "')";
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                result = "{\"msg\":\"更新成功\"}";
            }
            catch (Exception)
            {

                result = "{\"msg\":\"数据异常\"}";
            }
            Response.Write(result);

        }
        //工艺类卡片更新
        public void ProcessCardEdit()
        {
            txtEngName = Request["txtEngName"];
            txtEngModel = Request["txtEngModel"];
            txtPartName = Request["txtPartName"];
            txtTuHao = Request["txtTuHao"];
            hidGuanLianTime = Request["hidGuanLianTime"];
            txtBZ = Request["txtBZ"];
            proId = Request["proId"];
             txtBANCI = Request["proBanci"];
             sqlText = "update TBPM_PROCESS_CARD set PRO_ENGNAME='" + txtEngName + "',PRO_ENGMODEL='" + txtEngModel + "',PRO_PARTNAME='" + txtPartName + "',PRO_TUHAO='" + txtTuHao + "',PRO_NOTE='" + txtBZ + "',PRO_FUJIAN='" + hidGuanLianTime + "',PRO_STATE='1',PRO_CHECKLEVEL='2',PRO_BANCI='" + txtBANCI + "'  where PRO_ID=" + proId;

            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                result = "{\"msg\":\"更新成功\"}";
            }
            catch (Exception)
            {

                result = "{\"msg\":\"数据异常\"}";
            }
            Response.Write(result);
        }
        //通用类卡片新增
        public void GeneralCardNew()
        {
            txtName = Request["txtName"];
            txtBANCI = Request["txtBANCI"];
            hidGuanLianTime = Request["hidGuanLianTime"];
            txtBZ = Request["txtBZ"];
            proId = Request["proId"];
            sqlText = "insert into TBPM_PROCESS_CARD_GENERAL(PRO_NAME, PRO_BANCI, PRO_ISSUEDTIME, PRO_NOTE, PRO_FUJIAN,PRO_SUBMITID,PRO_STATE,PRO_CHECKLEVEL) values('" + txtName + "','" + txtBANCI + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + txtBZ + "','" + hidGuanLianTime + "','" + Session["UserID"] + "','1','2')";
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                result = "{\"msg\":\"更新成功\"}";
            }
            catch (Exception)
            {

                result = "{\"msg\":\"数据异常\"}";
            }
            Response.Write(result);

        }
        //工艺类卡片更新
        public void GeneralCardEdit()
        {
            txtName = Request["txtName"];
            txtBANCI = Request["txtBANCI"];

            hidGuanLianTime = Request["hidGuanLianTime"];
            txtBZ = Request["txtBZ"];
            proId = Request["proId"];
            sqlText = "update TBPM_PROCESS_CARD_GENERAL set PRO_NAME='" + txtName + "',PRO_BANCI='" + txtBANCI + "',PRO_NOTE='" + txtBZ + "',PRO_FUJIAN='" + hidGuanLianTime + "',PRO_STATE='1',PRO_CHECKLEVEL='2' where PRO_ID=" + proId;

            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                result = "{\"msg\":\"更新成功\"}";
            }
            catch (Exception)
            {

                result = "{\"msg\":\"数据异常\"}";
            }
            Response.Write(result);
        }
    }
}
