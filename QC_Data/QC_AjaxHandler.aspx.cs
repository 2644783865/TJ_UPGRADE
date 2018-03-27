using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;
using EasyUI;
using System.Text;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_AjaxHandler : System.Web.UI.Page
    {
        string sqlText;
        string result;
        PagerQueryParam pager = new PagerQueryParam();
        int rows;
        int page;
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
        public void FindDepManager()
        {
            string depId = Request["DepId"];
            if (depId != "")
            {
                sqlText = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION = '" + depId + "01'";
                try
                {
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    if (dt.Rows.Count > 0)
                    {
                        result = "{\"name\":\"" + dt.Rows[0]["ST_NAME"] + "\",\"Id\":\"" + dt.Rows[0]["ST_ID"] + "\"}";

                    }
                }
                catch (Exception)
                {

                    result = "{\"msg\":\"数据异常\",\"Id\":\"0\"}";
                }
                Response.Write(result);
            }

        }
        public void InitDep()
        {
            sqlText = "select DEP_CODE,DEP_NAME from dbo.TBDS_DEPINFO where DEP_FATHERID='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            Response.Write(JsonHelper.CreateJsonOne(dt, true));

        }
        public void InitDepPeo()
        {

            int page = Convert.ToInt32(Request["page"]);
            int rows = Convert.ToInt32(Request["rows"]);
            InitPager(rows, page);
            DataTable dt1 = CommonFun.GetDataByPagerQueryParam(pager);


            sqlText = "select count(1) from View_TBDS_STAFFINFO where" + strWhere();
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqlText);
            int num = 0;
            if (dt2.Rows.Count > 0)
            {
                num = Convert.ToInt16(dt2.Rows[0][0]);
            }
            string json = JsonHelper.CreateJsonParameters(dt1, true, num);
            Response.Write(json);


        }
        private void InitPager(int rows, int page)
        {
            pager.TableName = "View_TBDS_STAFFINFO";
            pager.PrimaryKey = "ST_ID";
            pager.ShowFields = "ST_NAME,ST_GENDER,DEP_POSITION,DEP_NAME,ST_ID,ST_WORKNO";
            pager.OrderField = "DEP_POSITION,ST_ID";
            pager.StrWhere = strWhere();

            pager.OrderType = 1;


            pager.PageSize = rows;
            pager.PageIndex = page;
        }

        private string strWhere()
        {
            string dep = Request["dep"];
            string auditjs = Request["auditjs"];
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");

            sb.Append("and ST_DEPID='" + dep + "' and ST_PD in ('0','2')");

            //二级审核人
            if (auditjs.ToString().Trim() == "2")
            {
                sb.Append(" and DEP_POSITION in('部长','副部长','董事长兼总经理','总经理','副总经理','技术总监')");
            }

            //三级审核人
            if (auditjs.ToString().Trim() == "3")
            {
                sb.Append(" and ST_DEPID='01'");
            }


            return sb.ToString();
        }
        public void InspecAssign()
        {
            string msg;
            string AFI_ID = Request["AFI_ID"];
            string qcclerk = Request["perID"];
            string qcclerknm = Request["perName"];
            string sql = "update TBQM_APLYFORINSPCT set AFI_QCMAN='" + qcclerk + "',AFI_QCMANNM='" + qcclerknm + "',AFI_ASMAN='" + Session["UserID"] + "',AFI_ASMANNM='" + Session["UserName"] + "',AFI_ASSTIME='" + DateTime.Now.ToString() + "',AFI_ASSGSTATE='1' where AFI_ID='" + AFI_ID + "'";

            try
            {
                DBCallCommon.ExeSqlText(sql);
                msg = "保存成功";
            }
            catch (Exception)
            {

                msg = "无法分工，请联系管理员";
            }
            Response.Write(msg);

        }
        public void GetTechPerson()
        {
            string engId = Request["EngId"];
            string sqlText = "select TSA_TCCLERKNM from View_TM_TaskAssign where TSA_ID='" + engId + "'";

            try
            {
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count > 0)
                {
                    Response.Write("{\"msg\":true,\"techName\":\"" + dt.Rows[0][0].ToString() + "\"}");
                }
            }
            catch (Exception)
            {

                Response.Write("{\"msg\":false,\"}");
            }

        }
        public void GetAllTechPer()
        {
            sqlText = "select ST_ID,ST_NAME from View_TBDS_STAFFINFO where ST_DEPID='12'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            string Json = JsonHelper.CreateJsonOne(dt, false);
            Response.Write(Json);

        }

        public void SetInspectPer()
        {
            string num = Request["num"];
            string per = Request["per"];
            string isDiret = Request["isDiret"];
            string sql = "update TBQM_SetInspectPerson set IsDiret='" + isDiret + "',InspectPerson='" + per + "',AddPerson='" + Session["UserID"].ToString() + "',Addtime='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where num='" + num + "'";
            try
            {
                DBCallCommon.ExeSqlText(sql);
                Response.Write("true");
            }
            catch (Exception)
            {

                Response.Write("false");
            }
        }
        #region 不合格评审单，采购分工
        public void PurAssign()
        {
            string Id = Request.Form["Id"];
            string name = Request.Form["name"];
            string orderId = Request.Form["orderId"];
            List<string> list = new List<string>();
            string[] subOrder = orderId.Trim(',').Split(',');

            for (int i = 0; i < subOrder.Length; i++)
            {
                sqlText = "update TBQC_RejectPro_Info set PurMan='" + Id + "',PurManNM='" + name + "' where Order_id='" + subOrder[i] + "'";
                list.Add(sqlText);
            }

            try
            {
                DBCallCommon.ExecuteTrans(list);
                result = "success";
            }
            catch (Exception)
            {

                result = "error"; ;
            }

            Response.Write(result);
        }
        #endregion


    }
}
