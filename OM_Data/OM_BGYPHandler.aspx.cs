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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BGYPHandler : System.Web.UI.Page
    {
        string sqlText;
        PagerQueryParam pager = new PagerQueryParam();
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
        #region 1.0获取库存列表，带分页

        public void GetStore()
        {
            int page = Convert.ToInt32(Request["page"]);
            int rows = Convert.ToInt32(Request["rows"]);
            InitPagerStore(rows, page);
            DataTable dt1 = CommonFun.GetDataByPagerQueryParam(pager);


            sqlText = "select count(1) from View_OM_BGYP_STORE  where " + strStore();
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqlText);
            int num = 0;
            if (dt2.Rows.Count > 0)
            {
                num = Convert.ToInt16(dt2.Rows[0][0]);
            }
            string json = JsonHelper.CreateJsonParameters(dt1, true, num);
            Response.Write(json);
        }


        #region 分页
        private void InitPagerStore(int rows, int page)
        {
            pager.TableName = "View_OM_BGYP_STORE";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "Id";
            pager.StrWhere = strStore();
            pager.OrderType = 1;
            pager.PageSize = rows;
            pager.PageIndex = page;
        }
        #endregion

        private string strStore()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(Request.Form["maid"]))
            {
                sb.Append(" and maId like '%" + Request.Form["maid"] + "%'");
            }
            if (!string.IsNullOrEmpty(Request.Form["name"]))
            {
                sb.Append(" and name like '%" + Request.Form["name"] + "%'");
            }
            if (!string.IsNullOrEmpty(Request.Form["canshu"]))
            {
                sb.Append(" and canshu like '%" + Request.Form["canshu"] + "%'");
            }
            if (!string.IsNullOrEmpty(Request.Form["pid"]))
            {
                sb.Append(" and pid like '%" + Request.Form["pid"] + "%'");
            }


            return sb.ToString();
        }
        #endregion

        #region 2.0获取入库列表，带分页

        public void GetIn()
        {
            int page = Convert.ToInt32(Request["page"]);
            int rows = Convert.ToInt32(Request["rows"]);
            InitPagerIn(rows, page);
            DataTable dt1 = CommonFun.GetDataByPagerQueryParam(pager);


            sqlText = "select count(1) from View_OM_BGYP_IN  where " + strStore();
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqlText);
            int num = 0;
            if (dt2.Rows.Count > 0)
            {
                num = Convert.ToInt16(dt2.Rows[0][0]);
            }
            string json = JsonHelper.CreateJsonParameters(dt1, true, num);
            Response.Write(json);
        }


        #region 分页
        private void InitPagerIn(int rows, int page)
        {
            pager.TableName = "View_OM_BGYP_IN";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "rkcode";
            pager.StrWhere = strIn();
            pager.OrderType = 1;
            pager.PageSize = rows;
            pager.PageIndex = page;
        }
        #endregion

        private string strIn()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request.Form["code"]))
            {
                sb.Append(" and rkcode like '%" + Request.Form["code"] + "%'");
            }
            if (!string.IsNullOrEmpty(Request.Form["maid"]))
            {
                sb.Append(" and maId like '%" + Request.Form["maid"] + "%'");
            }
            if (!string.IsNullOrEmpty(Request.Form["name"]))
            {
                sb.Append(" and name like '%" + Request.Form["name"] + "%'");
            }
            if (!string.IsNullOrEmpty(Request.Form["canshu"]))
            {
                sb.Append(" and canshu like '%" + Request.Form["canshu"] + "%'");
            }
            if (!string.IsNullOrEmpty(Request.Form["pid"]))
            {
                sb.Append(" and pid like '%" + Request.Form["pid"] + "%'");
            }


            return sb.ToString();
        }
        #endregion

        public void GetFixedTh()
        {
            string text = Request.Form["q"].ToString();
            sqlText = "select top 15 * from TBMA_OFFICETH  where IsDel=0 and IsBottom='on' and  (name like '%" + text + "%' or maId like '%" + text + "%' or canshu like '%" + text + "%') order by Id desc ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            Response.Write(JsonHelper.CreateJsonOne(dt, false));
        }


        public void deleterkd()
        {
            List<string> sqllist = new List<string>();
            string rkcode = Request["param"];
            //查找入库单数据
            string sqlgetdata = "select * from TBOM_BGYP_IN_DETAIL where wId='" + rkcode + "'";
            DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
            if (dtgetdata.Rows.Count > 0)
            {
                for (int i = 0; i < dtgetdata.Rows.Count; i++)
                {

                    //更新采购订单状态
                    string updateddstate = "update TBOM_BGYPPCAPPLYINFO set STATE_rk=null where CODE='" + dtgetdata.Rows[i]["ordercode"].ToString().Trim() + "' and WLCODE='" + dtgetdata.Rows[i]["sId"].ToString().Trim() + "'";
                    sqllist.Add(updateddstate);
                    //更新库存数量和金额
                    float num = float.Parse(dtgetdata.Rows[i]["num"].ToString().Trim());
                    float price = float.Parse(dtgetdata.Rows[i]["price"].ToString().Trim());
                    string sqltext = "select * from TBOM_BGYP_STORE where mId='" + dtgetdata.Rows[i]["sId"].ToString().Trim() + "'";
                    DataTable dtnum = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dtnum.Rows.Count > 0)
                    {
                        string updatestore="";
                        if (num == float.Parse(dtnum.Rows[0]["num"].ToString().Trim()))
                        {
                            updatestore = "update TBOM_BGYP_STORE set num=num-(" + num + "),price=price-(" + price + ") where mId='" + dtgetdata.Rows[i]["sId"].ToString().Trim() + "'";
                        }
                        else
                        {
                            updatestore = "update TBOM_BGYP_STORE set unPrice=(price-(" + price + "))/(num-(" + num + ")),num=num-(" + num + "),price=price-(" + price + ") where mId='" + dtgetdata.Rows[i]["sId"].ToString().Trim() + "'";
                        }
                        sqllist.Add(updatestore);
                    }

                }
                //删除入库单号
                string deleterkd = "DELETE FROM TBOM_BGYP_IN WHERE Code='" + rkcode + "'";
                sqllist.Add(deleterkd);
                //删除入库单明细
                string deleterkddetail = "DELETE FROM TBOM_BGYP_IN_DETAIL WHERE wId='" + rkcode + "'";
                sqllist.Add(deleterkddetail);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            Response.Write("{\"result\":\"Y\",\"msg\":\"删除成功!\"}");
        }

    }
}
