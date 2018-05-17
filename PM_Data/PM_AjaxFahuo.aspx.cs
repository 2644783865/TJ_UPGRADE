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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_AjaxFahuo : System.Web.UI.Page
    {
        string taskId = "";
        string sqlText;
        string result;
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

        #region 获取库存数据
        public void GetKuCun()
        {

            if (Request["id"] == null)
            {
                int page = Convert.ToInt32(Request["page"]);
                int rows = Convert.ToInt32(Request["rows"]);
                InitPager(rows, page);
                DataTable dt1 = CommonFun.GetDataByPagerQueryParam(pager);
                sqlText = "select a.*,b.*,c.CM_FHNUM as bjnum,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state,CM_BIANHAO,c.tsa_unit,c.cm_cusname,c.tsa_idnote,c.cm_fid,c.cm_id,c.CM_FHNUM,c.CM_BJZT,d.CM_PROJ,e.FH_ZONGXU,f.HB_KCZONGXU,cast(a.BM_ID as varchar)+'|'+isnull(CM_BIANHAO,'')+'|'+cast(row_number() over( order by BM_ID) as varchar) as pID from  TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU  left join View_CM_FaHuo as c on a.BM_ENGID=c.TSA_ID and a.BM_ZONGXU=c.ID left join View_CM_TSAJOINPROJ as d on a.BM_ENGID=d.TSA_ID left join (select distinct dbo.f_SubHeadCode('.',ID) as FH_ZONGXU,TSA_ID from View_CM_FaHuo)e on a.BM_ENGID=e.TSA_ID and a.BM_ZONGXU=e.FH_ZONGXU left join (select distinct dbo.f_SubHeadCode('.',KC_ZONGXU) as HB_KCZONGXU,KC_TSA from TBMP_FINISHED_STORE where KC_KCNUM is not null and KC_KCNUM<>'0')f on a.BM_ENGID=f.KC_TSA and a.BM_ZONGXU=f.HB_KCZONGXU  where " + strWhere();
                DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqlText);
                int num = 0;
                if (dt2.Rows.Count > 0)
                {
                    num = Convert.ToInt16(dt2.Rows.Count);
                }
                string json = JsonHelper.CreateJsonParameters(dt1, true, num);
                Response.Write(json);
            }
            else
            {
                string id = Request["id"].ToString().Split('|')[0];
                string bianhao = Request["id"].ToString().Split('|')[1];
                string zongxu = "";
                string engid = "";
                int level = 0;
                sqlText = "select BM_ZONGXU,BM_ENGID from TBPM_STRINFODQO where BM_ID='" + id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count > 0)
                {
                    zongxu = dt.Rows[0][0].ToString();
                    level = zongxu.Split('.').Length;
                    engid = dt.Rows[0][1].ToString();

                }

                sqlText = "select a.*,b.*,c.CM_FHNUM as bjnum,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state,c.cm_bianhao,c.tsa_unit,c.cm_cusname,c.tsa_idnote,c.cm_fid,c.cm_id,c.CM_FHNUM,d.CM_PROJ,cast(a.BM_ID as varchar)+'|'+isnull(CM_BIANHAO,'')+'|'+cast(row_number() over( order by BM_ID) as varchar) as pID from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU left join View_CM_FaHuo as c on a.BM_ENGID=c.TSA_ID and a.BM_ZONGXU=c.ID  left join View_CM_TSAJOINPROJ as d on a.BM_ENGID=d.TSA_ID    where dbo.Splitnum(BM_ZONGXU,'.')=" + level + "and a.BM_ENGID='" + engid + "' and a.BM_ZONGXU like '" + zongxu + ".%' and (BM_KU<>'' or BM_MARID='' and (BM_FHSTATE is null or BM_FHSTATE='0' or BM_FHSTATE='4') ) and ( CM_BIANHAO like '%" + bianhao + "%' or CM_BIANHAO is null )";
                dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                string json = JsonHelper.CreateJsonOne(dt, true);
                Response.Write(json);



            }

        }

        #endregion

        #region 分页
        private void InitPager(int rows, int page)
        {
            pager.TableName = "(select a.*,b.*,c.CM_FHNUM as bjnum,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state,CM_BIANHAO,c.tsa_unit,c.cm_cusname,c.tsa_idnote,c.cm_fid,c.cm_id,c.CM_FHNUM,c.CM_BJZT,d.CM_PROJ,e.FH_ZONGXU,f.HB_KCZONGXU,cast(a.BM_ID as varchar)+'|'+isnull(CM_BIANHAO,'')+'|'+cast(row_number() over( order by BM_ID) as varchar) as pID from  TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU  left join View_CM_FaHuo as c on a.BM_ENGID=c.TSA_ID and a.BM_ZONGXU=c.ID left join View_CM_TSAJOINPROJ as d on a.BM_ENGID=d.TSA_ID left join (select distinct dbo.f_SubHeadCode('.',ID) as FH_ZONGXU,TSA_ID from View_CM_FaHuo)e on a.BM_ENGID=e.TSA_ID and a.BM_ZONGXU=e.FH_ZONGXU left join (select distinct dbo.f_SubHeadCode('.',KC_ZONGXU) as HB_KCZONGXU,KC_TSA from TBMP_FINISHED_STORE where KC_KCNUM is not null and KC_KCNUM<>'0')f on a.BM_ENGID=f.KC_TSA and a.BM_ZONGXU=f.HB_KCZONGXU)t ";
            pager.PrimaryKey = "BM_ID";
            pager.ShowFields = "*";
            pager.OrderField = "BM_ID";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = rows;
            pager.PageIndex = page;
        }
        #endregion

        public void FayunBJ()
        {
            List<string> sqllist = new List<string>();

            string data = Request.Form["data"].ToString();
            JArray ja = (JArray)JsonConvert.DeserializeObject(data);

            string sheetcode = encodesheetno();//生成比价单号
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string manid = Session["UserID"].ToString();
            string sqltext1 = "insert into TBMP_FAYUNPRCRVW(ICL_SHEETNO,ICL_IQRDATE,ICL_REVIEWA,ICL_CSTATE,ICL_STATE) VALUES('" + sheetcode + "','" + time + "','" + manid + "','0','0')";
            sqllist.Add(sqltext1);

            foreach (JObject Jobj in ja)
            {
                //更改税率10，,2018.5.16
                sqlText = "insert into TBMP_FAYUNPRICE(PM_ZONGXU,PM_FATHERID,PM_FID,PM_BIANHAO,PM_SHEETNO,TSA_ID,PM_ENGNAME,PM_MAP,PM_FYNUM,PM_SHUILV) VALUES('" + Jobj["zongxu"].ToString() + "','" + Jobj["pid"].ToString().Split('|')[0] + "','" + Jobj["fid"].ToString() + "','" + Jobj["bianhao"].ToString() + "','" + sheetcode + "','" + Jobj["tsaid"].ToString() + "','" + Jobj["name"].ToString() + "','" + Jobj["map"].ToString() + "','" + Jobj["bjnum"].ToString() + "','10')";
                sqllist.Add(sqlText);
                //string state = "1";
                //if (CommonFun.ComTryInt(Jobj["bjnum"].ToString()) < CommonFun.ComTryInt(Jobj["num"].ToString()))
                //{
                //    state = "4";//部分比价
                //}
                //string sqltext3 = "update TBPM_STRINFODQO set BM_FHSTATE='" + state + "' where BM_ENGID='" + Jobj["tsaid"] + "' and (BM_ZONGXU ='" + Jobj["zongxu"] + "')";
                string sqltext3 = "update TBPM_STRINFODQO set BM_YFNUM=BM_YFNUM+" + Jobj["bjnum"].ToString() + " where BM_ID='" + Jobj["pid"].ToString().Split('|')[0] + "' and BM_ZONGXU='" + Jobj["zongxu"] + "'";
                sqllist.Add(sqltext3);
                // 
                string sql = "update a set CM_YBJNUM=CM_YBJNUM+" + Jobj["bjnum"].ToString() + " from  TBCM_FHBASIC as a left join TBCM_FHNOTICE as b on a.CM_FID=b.CM_FID   where CM_ID='" + Jobj["pid"].ToString().Split('|')[0] + "' and ID='" + Jobj["zongxu"] + "' and b.CM_BIANHAO='" + Jobj["bianhao"] + "'";
                sqllist.Add(sql);
            }
            foreach (JObject Jobj in ja)
            {
                sqlText = "update TBPM_STRINFODQO set BM_FHSTATE=case when BM_YFNUM<BM_NUMBER then '4' else '2' end  where  BM_ENGID='" + Jobj["tsaid"].ToString() + "' and BM_ZONGXU='" + Jobj["zongxu"] + "'";
                sqllist.Add(sqlText);
                string sql1 = "update a set CM_BJZT=case when CM_YBJNUM< CM_FHNUM then '4' else '2' end from TBCM_FHBASIC  as a left join TBCM_FHNOTICE as b on  a.CM_FID=b.CM_FID  where CM_ID='" + Jobj["pid"].ToString().Split('|')[0] + "' and ID='" + Jobj["zongxu"] + "' and b.CM_BIANHAO='" + Jobj["bianhao"] + "'";
                sqllist.Add(sql1);
            }

            result = "{\"state\":\"success\",\"sheetcode\":\"" + sheetcode + "\",}";
            try
            {

                DBCallCommon.ExecuteTrans(sqllist);
            }
            catch (Exception)
            {

                result = "{\"state\":\"error\"}";
            }
            Response.Write(result);


        }
        public void BuBJ()
        {
            List<string> sqllist = new List<string>();

            string data = Request.Form["data"].ToString();
            JArray ja = (JArray)JsonConvert.DeserializeObject(data);

            foreach (JObject Jobj in ja)
            {
               
                string sql = "update TBPM_STRINFODQO set BM_YFNUM=BM_YFNUM+" + Jobj["bjnum"].ToString() + " where BM_ID='" + Jobj["pid"].ToString().Split('|')[0] + "' and BM_ZONGXU='" + Jobj["zongxu"] + "'";
                sqllist.Add(sql);
                // 
                sql = "update a set CM_YBJNUM=CM_YBJNUM+" + Jobj["bjnum"].ToString() + " from  TBCM_FHBASIC as a left join TBCM_FHNOTICE as b on a.CM_FID=b.CM_FID   where CM_ID='" + Jobj["pid"].ToString().Split('|')[0] + "' and ID='" + Jobj["zongxu"] + "' and b.CM_BIANHAO='" + Jobj["bianhao"] + "'";
                sqllist.Add(sql);
            }

            foreach (JObject Jobj in ja)
            {
                string sql1 = "update TBPM_STRINFODQO set BM_FHSTATE=case when BM_YFNUM<BM_NUMBER then '4' else '2' end  where  BM_ENGID='" + Jobj["tsaid"].ToString() + "' and BM_ZONGXU='" + Jobj["zongxu"] + "'";
                sqllist.Add(sql1);
                sql1 = "update TBCM_FHBASIC set CM_BJZT=case when CM_YBJNUM< CM_FHNUM then '4' else '2' end where CM_ID='" + Jobj["pid"].ToString().Split('|')[0] + "' and ID='" + Jobj["zongxu"] + "'";
                sqllist.Add(sql1);
            }

            result = "{\"msg\":\"操作成功！\"}";
            try
            {
                DBCallCommon.ExecuteTrans(sqllist);
            }
            catch (Exception)
            {

                result = "{\"msg\":\"数据错误，请联系管理员\"}";
            }
            Response.Write(result);
        }

        private string strWhere()
        {
            string tsaid = Request.Form["tsaid"];
            string proj = Request.Form["proj"];
            string map = Request.Form["map"];
            string name = Request.Form["name"];

            StringBuilder sb = new StringBuilder();
            sb.Append(" dbo.Splitnum(BM_ZONGXU,'.')=0 and BM_PJID not like 'JSB.%' and (BM_FHSTATE is null or BM_FHSTATE='0' or BM_FHSTATE='4') and  FH_ZONGXU is not null and HB_KCZONGXU is not null and (CM_BJZT is null or CM_BJZT in ('0','4'))");
            //sb.Append(" dbo.Splitnum(BM_ZONGXU,'.')=0 and BM_PJID not like 'JSB.%' and (BM_FHSTATE is null or BM_FHSTATE='0' or BM_FHSTATE='4') and  FH_ZONGXU is not null and HB_KCZONGXU is not null and (CM_BJZT is null or CM_BJZT in ('0','4')) and (substring(BM_XUHAO,1,1)+BM_ENGID) in(select bianhao from (select (substring(BM_XUHAO,1,1)+BM_ENGID) as bianhao,sum(KC_KCNUM) as kucunnum from (select a.*,b.*,c.CM_FHNUM as bjnum,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state,CM_BIANHAO,c.tsa_unit,c.cm_cusname,c.tsa_idnote,c.cm_fid,c.cm_id,c.CM_FHNUM,c.CM_BJZT,d.CM_PROJ,e.FH_ZONGXU,f.HB_KCZONGXU,cast(a.BM_ID as varchar)+'|'+isnull(CM_BIANHAO,'')+'|'+cast(row_number() over( order by BM_ID) as varchar) as pID from  TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU  left join View_CM_FaHuo as c on a.BM_ENGID=c.TSA_ID and a.BM_ZONGXU=c.ID left join View_CM_TSAJOINPROJ as d on a.BM_ENGID=d.TSA_ID left join (select distinct dbo.f_SubHeadCode('.',ID) as FH_ZONGXU,TSA_ID from View_CM_FaHuo)e on a.BM_ENGID=e.TSA_ID and a.BM_ZONGXU=e.FH_ZONGXU left join (select distinct dbo.f_SubHeadCode('.',KC_ZONGXU) as HB_KCZONGXU,KC_TSA from TBMP_FINISHED_STORE where KC_KCNUM is not null and KC_KCNUM<>'0')f on a.BM_ENGID=f.KC_TSA and a.BM_ZONGXU=f.HB_KCZONGXU)t where BM_KU!='' and BM_KU is not null group by substring(BM_XUHAO,1,1)+BM_ENGID)s where kucunnum is not null and kucunnum!=0)");
            if (!string.IsNullOrEmpty(tsaid))
            {
                sb.Append(" and BM_ENGID like '%" + tsaid + "%'");
            }
            if (!string.IsNullOrEmpty(map))
            {
                sb.Append(" and BM_TUHAO like '%" + map + "%'");
            }
            if (!string.IsNullOrEmpty(name))
            {
                sb.Append(" and BM_CHANAME like '%" + name + "%'");
            }
            if (!string.IsNullOrEmpty(proj))
            {
                sb.Append(" and CM_PROJ like '%" + proj + "%'");
            }

            return sb.ToString();
        }

        protected string encodesheetno()
        {
            string sheetcode = "";
            string sqltext = "select top 1 ICL_SHEETNO FROM TBMP_FAYUNPRCRVW ORDER BY ICL_SHEETNO DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                sheetcode = dt.Rows[0][0].ToString();
                sheetcode = Convert.ToString(Convert.ToInt32(sheetcode) + 1);
                sheetcode = sheetcode.PadLeft(8, '0');
            }
            else
            {
                sheetcode = "00000001";
            }
            return sheetcode;
        }

    }
}
