using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF
{
    /// <summary>
    /// Summary description for Ajax
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService] //重要，否则无法在客户端调用此方法而导致根本无法实现效果
    public class Ajax : System.Web.Services.WebService
    {
        private string[] autoCompleteWordList = null;
        private string[] autoCompleteall = null;

        [WebMethod]
        public string[] HmCode(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = hmcode(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] hmcode(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select ID + '|' + MNAME + '|' + GUIGE + '|' + CAIZHI + '|' + [TECHUNIT]+'|'+CAST([CONVERTRATE] AS VARCHAR)+'|'+[PURCUNIT]+'|'+[FUZHUUNIT]+'|'+cast(MWEIGHT as varchar)+'|'+GB AS CODE ";
            strsql += " from TBMA_MATERIAL where HMCODE like '" + fixText + "%' and STATE='1'  order by HMCODE,CAIZHI ";
            //strsql = "select CODE from VIEW_HMCODE where HMCODE like '" + fixText + "%'";
            //strsql = "select RM_ID+' '+RM_NAME+' '+RM_GUIGE+' '+RM_CAIZHI as CODE from TBMA_RAWMAINFO where  RM_HMCODE like '" + fixText + "%' union";
            //strsql += " select BZJ_ID+' '+BZJ_NAME+' '+BZJ_GUIGE as CODE from TBMA_BZJINFO where  BZJ_HMCODE like '" + fixText + "%' union";
            //strsql += " select LVCG_ID+' '+LVCG_NAME+' '+LVCG_GUIGE as CODE from TBMA_LVCGMAINFO where  LVCG_HMCODE like '" + fixText + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }
        //固定工资
        [WebMethod]
        public string[] get_st_id(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = GETSTID(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] GETSTID(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select cast(isnull(ST_ID,0) as varchar(10)) + '|' + isnull(ST_NAME,'') + '|' + isnull(DEP_NAME,'') + '|' + isnull(ST_WORKNO,'') ";
            strsql += " from View_TBDS_STAFFINFO where ST_NAME like '%" + fixText + "%' or ST_WORKNO like '%" + fixText + "%' order by ST_WORKNO,ST_NAME ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }

        [WebMethod]
        public string[] get_st_id_new(string prefixText, int count, string contextKey)
        {
            string[] codes = GETSTIDNEW(prefixText);
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] GETSTIDNEW(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select cast(isnull(ST_ID,0) as varchar(10)) + '|' + isnull(ST_NAME,'') + '|' + isnull(DEP_NAME,'') + '|' + isnull(DEP_POSITION,'')";
            strsql += " from View_TBDS_STAFFINFO where ST_NAME like '%" + fixText + "%' order by ST_ID,ST_NAME ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }

        [WebMethod]
        public string[] get_code_st_id(string prefixText, int count, string contextKey)
        {
            string[] codes = GETCODESTID(prefixText);
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] GETCODESTID(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select a.TA_Code+ '|'+cast(isnull(TA_SQRID,0) as varchar(10)) + '|' + isnull(TA_SQR,'') + '|' + isnull(TA_SQRDep,'') + '|' + isnull(TA_SQRDepPos,'')";
            strsql += " from OM_TravelApplyDetail as a left join OM_TravelApply as c on a.TA_Code =c.TA_Code where (a.TA_Code like '%" + fixText + "%' or TA_ZDR like '%" + fixText + "%')and (TA_DaysReal>TA_DaysPlan or TA_IsSure='1') and a.TA_State='6'and TA_SQRID not in (select TD_SQRID from OM_TravelDelayDetail as b where b.TA_Code =a.TA_Code) order by a.TA_Code ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }


        //人员筛选
        [WebMethod]
        public string[] get_st_name(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = GETSTname(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] GETSTname(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select isnull(ST_NAME,'')";
            strsql += " from View_TBDS_STAFFINFO where ST_NAME like '%" + fixText + "%' or ST_WORKNO like '%" + fixText + "%' order by ST_WORKNO,ST_NAME ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }


        [WebMethod]
        public string[] HtCode(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = htcode(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] htcode(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select PCON_BCODE";
            strsql += " from TBPM_CONPCHSINFO where PCON_BCODE like '%" + fixText + "%' and PCON_TYPE='销售合同'  order by PCON_BCODE ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }
        //项目
        [WebMethod]
        public string[] xmmc(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = xmmc(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] xmmc(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select PJ_NAME";
            strsql += " from TBPM_PJINFO where PJ_ID+'/'+PJ_NAME like '%" + fixText + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }
        //任务号，项目，合同
        [WebMethod]
        public string[] gcmc(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = gcmc(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] gcmc(string fixText)
        {
            List<string> ID = new List<string>();

            string strsql = "select distinct TSA_ID+'|'+TSA_PJID+'|'+CM_PROJ";
            strsql += " from View_TM_TaskAssign where TSA_ID+'|'+TSA_PJID+'|'+CM_PROJ like '%" + fixText + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }
        //根据任务号获取图号
        [WebMethod]
        public string[] get_rwh_th(string prefixText, int count, string contextKey)
        {
            ///检测参数是否为空

            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;

            // 如果数组为空
            if (autoCompleteWordList == null)
            {
                //读取数据库的内容
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                sqlConn.Open();
                System.Data.DataTable dt = new System.Data.DataTable();
                string sqltext = "";
                //2为供应商
                sqltext = "SELECT  distinct BM_TUHAO  AS Expr1 FROM TBPM_STRINFODQO WHERE " +
                          "(BM_TUHAO like '%" + prefixText + "%') AND BM_ENGID='" + contextKey + "' ORDER BY BM_TUHAO";
                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                //读取内容文件的数据到临时数组
                string[] temp = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                //将临时数组的内容赋给返回数组
                autoCompleteall = temp;
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }
        //项目
        [WebMethod]
        public string[] kh_xmmc(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = kh_xmmc(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] kh_xmmc(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select distinct TSA_PJID+'|'+CM_PROJ";
            strsql += " from View_TM_TaskAssign where TSA_PJID+'|'+CM_PROJ like '%" + fixText + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }
        //工程
        [WebMethod]
        public string[] kh_gcmc(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = kh_gcmc(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] kh_gcmc(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select ENG_ID+'/'+ENG_NAME";
            strsql += " from TBPM_ENGINFO where ENG_ID+'/'+ENG_NAME like '%" + fixText + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }
        [WebMethod]
        public string[] SHHtCode(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = SHhtcode(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] SHhtcode(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select SH_HTBH";
            strsql += " from CM_SHFWSQ where SH_HTBH like '%" + fixText + "%' order by SH_HTBH ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }
        [WebMethod]
        public string[] IntoTemplate(string prefixText, int count, string contextKey)
        {
            // Create array of parts   
            string[] parts = PartsName();
            // Return matching parts   
            return (from m in parts where m.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select m).Take(count).ToArray();
        }
        private string[] PartsName()
        {
            List<string> Names = new List<string>();
            string strsql = "select PDS_NAME+'_'+PDS_ENGTYPE from TBPD_STRUINFO";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                Names.Add(row[0].ToString().Trim());
            }
            return Names.ToArray();
        }

        [WebMethod]
        public string[] TcAssignNames(string prefixText, int count, string contextKey)
        {
            string[] parts = AssignNames();
            return (from m in parts where m.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select m).Take(count).ToArray();
        }
        private string[] AssignNames()
        {
            List<string> Names = new List<string>();
            string strsql = "select distinct TSA_TCCLERKNM as HM from TBPM_TCTSASSGN ";
            strsql += "union select distinct TSA_PJNAME as HM from TBPM_TCTSASSGN";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                Names.Add(row[0].ToString().Trim());
            }
            return Names.ToArray();
        }

        [WebMethod]
        public string[] TsaIDS(string prefixText, int count, string contextKey)
        {
            string[] parts = tsaids();
            return (from m in parts where m.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select m).Take(count).ToArray();
        }
        private string[] tsaids()
        {
            List<string> ids = new List<string>();
            string strsql = "select TSA_ID from TBPM_TCTSASSGN ";
            strsql += "where TSA_FATHERNODE='0' and cast(TSA_STATE as int)<2 ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ids.Add(row[0].ToString().Trim());
            }
            return ids.ToArray();
        }

        [WebMethod]
        public string[] getDepList(string prefixText, int count)
        {
            LinqDataContext db = new LinqDataContext();
            var result = from u in db.TBDS_DEPINFOs
                         where u.DEP_CODE.StartsWith(prefixText)
                         select u.DEP_CODE + " " + u.DEP_NAME;
            result = result.Take<string>(count);
            string[] deplist = result.ToArray<string>();
            return deplist;
        }

        [WebMethod]
        public string[] getStaffList(string prefixText, int count)
        {
            LinqDataContext db = new LinqDataContext();
            var result = from u in db.TBDS_STAFFINFOs
                         where u.ST_CODE.StartsWith(prefixText)
                         select u.ST_CODE + " " + u.ST_NAME;
            result = result.Take<string>(count);
            string[] stufflist = result.ToArray<string>();
            return stufflist;
        }

        [WebMethod]
        public string[] getWarehouseList(string prefixText, int count)
        {
            LinqDataContext db = new LinqDataContext();
            var result = from u in db.TBWS_WAREHOUSEINFOs
                         where u.WS_ID.StartsWith(prefixText)
                         select u.WS_ID + " " + u.WS_NAME;
            result = result.Take<string>(count);
            string[] warehouselist = result.ToArray<string>();
            return warehouselist;
        }

        [WebMethod]
        public string[] getSupplierList(string prefixText, int count)
        {
            LinqDataContext db = new LinqDataContext();
            var result = from u in db.TBCS_CUSUPINFOs
                         where u.CS_HRCODE.StartsWith(prefixText)
                         select u.CS_CODE + " " + u.CS_NAME;
            result = result.Take<string>(count);
            string[] supplierlist = result.ToArray<string>();
            return supplierlist;
        }

        [WebMethod]
        public string[] getMaterialIDList(string prefixText, int count)
        {
            LinqDataContext db = new LinqDataContext();
            var result1 = from u in db.TBMA_BZJINFOs
                          where u.BZJ_HMCODE.StartsWith(prefixText)
                          select u.BZJ_ID + " " + u.BZJ_NAME;
            var result2 = from v in db.TBMA_RAWMAINFOs
                          where v.RM_HMCODE.StartsWith(prefixText)
                          select v.RM_ID + " " + v.RM_NAME;
            var result3 = from w in db.TBMA_LVCGMAINFOs
                          where w.LVCG_HMCODE.StartsWith(prefixText)
                          select w.LVCG_ID + " " + w.LVCG_NAME;
            result1 = result1.Union<string>(result2);
            result1 = result1.Union<string>(result3);
            result1 = result1.Take<string>(count);
            string[] materialidlist = result1.ToArray<string>();
            return materialidlist;
        }

        [WebMethod]
        public string[] getTctAssign(string prefixText, int count)
        {
            LinqDataContext db = new LinqDataContext();
            var result1 = from u in db.TBPM_TCTSASSGNs
                          where u.TSA_PJNAME.StartsWith(prefixText)
                          select u.TSA_PJNAME;
            var result2 = from u in db.TBPM_TCTSASSGNs
                          where u.TSA_ENGNAME.StartsWith(prefixText)
                          select u.TSA_ENGNAME;
            var result3 = from u in db.TBPM_TCTSASSGNs
                          where u.TSA_TCCLERKNM.StartsWith(prefixText)
                          select u.TSA_TCCLERKNM;
            var result4 = from u in db.TBPM_TCTSASSGNs
                          where u.TSA_PJID.StartsWith(prefixText)
                          select u.TSA_PJID;
            result1 = result1.Union<string>(result2);
            result1 = result1.Union<string>(result3);
            result1 = result1.Union<string>(result4);
            result1 = result1.Take<string>(count);
            string[] getTctAssign = result1.ToArray<string>();
            return getTctAssign;
        }
        //生产制号
        [WebMethod]
        public string[] getEngID(string prefixText, int count)
        {
            List<string> EngID = new List<string>();
            string strsql = "SELECT TSA_PJNAME+'-'+TSA_ENGNAME+'-'+TSA_ID FROM View_TM_TaskAssign WHERE TSA_FATHERNODE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                EngID.Add(row[0].ToString().Trim());
            }
            //return (from m in EngID select m).Take(count).ToArray();
            return EngID.Where(p => p.IndexOf(prefixText.ToUpper()) >= 0).Take(count).ToArray();
        }

        //项目结转备库
        [WebMethod]
        public string[] getEngIDPT(string prefixText, int count)
        {
            List<string> EngID = new List<string>();
            string strsql = "SELECT TSA_PJNAME+'`'+TSA_ENGNAME+'`'+TSA_ID FROM View_TM_TaskAssign WHERE TSA_FATHERNODE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                EngID.Add(row[0].ToString().Trim());
            }
            //return (from m in EngID select m).Take(count).ToArray();
            return EngID.Where(p => p.IndexOf(prefixText.ToUpper()) >= 0).Take(count).ToArray();
        }


        //员工
        [WebMethod]
        public string[] getStaff(string prefixText, int count)
        {
            List<string> Staff = new List<string>();
            string strsql = "SELECT ST_NAME+'-'+ST_CODE FROM TBDS_STAFFINFO WHERE ST_NAME like '%" + prefixText + "%' and ST_CODE NOT LIKE '01%' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                Staff.Add(row[0].ToString().Trim());
            }

            ////return Staff.ToArray();
            return Staff.Where(p => p.IndexOf(prefixText.ToUpper()) >= 0).Take(count).ToArray();
        }



        //计划跟踪号
        [WebMethod]
        public string[] getPTC(string prefixText, int count)
        {
            List<string> PTC = new List<string>();
            string strsql = "SELECT TSA_PJNAME+'-'+TSA_ENGNAME+'-'+TSA_ID FROM View_TM_TaskAssign WHERE TSA_FATHERNODE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                PTC.Add(row[0].ToString().Trim());
            }
            PTC.Insert(0, "中材重机-备库");
            return PTC.Where(p => p.IndexOf(prefixText.ToUpper()) >= 0).Take(count).ToArray();
        }

        //" case when CS_TYPE='1' then '客户' when CS_TYPE='2' then '采购供应商' when CS_TYPE='3' then '客户和供应商'"+
        //                        " when CS_TYPE='4' then '技术外协分包商' when CS_TYPE='5' then '生产外协分包商' "+
        //                        " when CS_TYPE='6' then '原材料销售供应商'  when CS_TYPE='7' then '其它' end as CS_TYPE
        //委外加工单位
        [WebMethod]
        public string[] getCompanyid(string prefixText, int count)
        {
            List<string> CompanyID = new List<string>();
            string strsql = "SELECT DISTINCT CS_NAME+'|'+CS_CODE+'|'+(case when CS_TYPE='4' then '技术外协分包商' else '生产外协分包商' end) FROM TBCS_CUSUPINFO WHERE  (CS_TYPE='4' OR CS_TYPE='5') AND CS_State='0'";//(CS_TYPE='4' OR CS_TYPE='5')
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                CompanyID.Add(row[0].ToString().Trim());
            }
            return CompanyID.Where(p => p.IndexOf(prefixText.ToUpper()) >= 0).Take(count).ToArray();
        }
        //其他入库供应商
        [WebMethod]
        public string[] getotherCompany(string prefixText, int count)
        {
            List<string> CompanyID = new List<string>();
            string strsql = "SELECT DISTINCT CS_NAME+'|'+CS_CODE  FROM TBCS_CUSUPINFO WHERE  CS_State='0'";//(CS_TYPE='4' OR CS_TYPE='5')
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                CompanyID.Add(row[0].ToString().Trim());
            }
            return CompanyID.Where(p => p.IndexOf(prefixText.ToUpper()) >= 0).Take(count).ToArray();
        }

        //得到QC生产制号
        [WebMethod]
        public string[] getQCEngID(string prefixText, int count)
        {
            List<string> EngID = new List<string>();

            string strsql = "SELECT distinct  ISNULL(a.TSA_ID,'')+ '|' + ISNULL(b.CM_PROJ,'')+ '|' + ISNULL(a.TSA_ENGNAME,'')  as Expr1 FROM TBCM_Basic a, TBCM_Plan b WHERE a.ID=b.ID";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);

            foreach (DataRow row in dt.Rows)
            {
                EngID.Add(row[0].ToString().Trim());
            }
            return EngID.Where(p => p.IndexOf(prefixText.ToUpper()) >= 0).Take(count).ToArray();
        }


        [WebMethod] //项目名称
        public String[] GetPJNAME(string prefixText, int count)
        {
            ///检测参数是否为空

            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;

            // 如果数组为空
            if (autoCompleteWordList == null)
            {
                //读取数据库的内容
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                sqlConn.Open();
                DataSet ds = new DataSet();
                DataTable glotb = new DataTable();
                ds = DBCallCommon.FillDataSet("SELECT ISNULL(PJ_NAME,'') + '|' + ISNULL(PJ_ID,'')  as Expr1 FROM TBPM_PJINFO WHERE PJ_NAME LIKE '%" + prefixText + "%' or PJ_ID like '%" + prefixText + "%'");
                glotb.Merge(ds.Tables[0]);
                glotb.AcceptChanges();
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }

        [WebMethod] //工程名称
        public String[] GetENGNAME(string prefixText, int count)
        {
            ///检测参数是否为空

            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;

            // 如果数组为空
            if (autoCompleteWordList == null)
            {
                //读取数据库的内容
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                sqlConn.Open();
                DataSet ds = new DataSet();
                DataTable glotb = new DataTable();
                ds = DBCallCommon.FillDataSet("SELECT ISNULL(TSA_ENGNAME,'') + '|' + ISNULL(TSA_ID,'')  as Expr1 FROM TBPM_TCTSASSGN WHERE (TSA_ENGNAME LIKE '%" + prefixText + "%' or TSA_ID like '%" + prefixText + "%') and TSA_ID not like '%-%'");
                glotb.Merge(ds.Tables[0]);
                glotb.AcceptChanges();
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }

        [WebMethod] //厂商名称
        public String[] GetCompleteProvider(string prefixText, int count)
        {
            ///检测参数是否为空

            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;

            // 如果数组为空
            if (autoCompleteWordList == null)
            {
                //读取数据库的内容
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                sqlConn.Open();
                DataSet ds = new DataSet();
                string sqltext = "";
                //2为供应商
                sqltext = "SELECT CS_NAME  FROM TBCS_CUSUPINFO " +
                          " WHERE (CS_HRCODE LIKE '%" + prefixText + "%' or CS_NAME like '%" + prefixText + "%')and CS_state='0' ORDER BY CS_CODE";
                ds = DBCallCommon.FillDataSet(sqltext);
                //读取内容文件的数据到临时数组
                string[] temp = new string[ds.Tables[0].Rows.Count];
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    temp[i] = dr["CS_NAME"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                //将临时数组的内容赋给返回数组
                autoCompleteall = temp;
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }

        [WebMethod] //合同号
        public String[] GetContractNO(string prefixText, int count)
        {
            ///检测参数是否为空

            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;

            // 如果数组为空
            if (autoCompleteWordList == null)
            {
                //读取数据库的内容
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                sqlConn.Open();
                DataSet ds = new DataSet();
                string sqltext = "";
                //2为供应商
                sqltext = "SELECT PCON_BCODE+'|'+PCON_NAME AS HTBH  FROM TBPM_CONPCHSINFO " +
                          " WHERE (PCON_BCODE LIKE '%" + prefixText + "%' or PCON_NAME like '%" + prefixText + "%')";
                ds = DBCallCommon.FillDataSet(sqltext);
                //读取内容文件的数据到临时数组
                string[] temp = new string[ds.Tables[0].Rows.Count];
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    temp[i] = dr["HTBH"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                //将临时数组的内容赋给返回数组
                autoCompleteall = temp;
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
            }
            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }

        [WebMethod]
        public String[] GetENGNAME(string prefixText, int count, string contextKey)
        {

            List<string> EngID = new List<string>();

            string sql = "SELECT ISNULL(TSA_ENGNAME,'') + '|' + ISNULL(TSA_ID,'')  as Expr1 FROM TBPM_TCTSASSGN WHERE (TSA_ENGNAME LIKE '%" + prefixText + "%' or TSA_ID like '%" + prefixText + "%') and TSA_ID not like '%-%' and TSA_PJID like '%" + contextKey + "%'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            foreach (DataRow row in dt.Rows)
            {
                EngID.Add(row[0].ToString().Trim());
            }
            return EngID.Where(p => p.IndexOf(prefixText.ToUpper()) >= 0).Take(count).ToArray();

        }

        [WebMethod]
        public String[] StorgeCode(string prefixText, int count, string contextKey)
        {

            List<string> StorgeCode = new List<string>();

            string sql = "SELECT top " + count.ToString() + " MaterialCode +' '+isnull(MaterialName,'')+' '+isnull(Standard,'') +' '+substring(PTC,0,case when charindex('_',PTC)>0 then charindex('_',PTC) else DATALENGTH(PTC) end )+' '+cast(cast(Number as float) as varchar(50)) AS MaterialCode ,SQCODE FROM View_SM_Storage WHERE ( MaterialCode LIKE '" + prefixText + "%' OR HMCODE like '" + prefixText + "%') order by MaterialCode";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            foreach (DataRow row in dt.Rows)
            {
                StorgeCode.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["MaterialCode"].ToString(), row["SQCODE"].ToString()));
            }

            return StorgeCode.Take(count).ToArray();
        }

        //任务号
        [WebMethod]
        public string[] getTaskID(string prefixText, int count)
        {
            List<string> EngID = new List<string>();
            string strsql = "SELECT TSA_ID FROM View_TM_TaskAssign union select TSA_ID from TBPM_DETAIL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                EngID.Add(row[0].ToString().Trim());
            }
            //return (from m in EngID select m).Take(count).ToArray();
            return EngID.Where(p => p.IndexOf(prefixText.ToUpper()) >= 0).Take(count).ToArray();
        }


        //油漆计划
        [WebMethod]
        public string[] PaintPS(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = paintPS(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] paintPS(string fixText)
        {
            List<string> ID = new List<string>();
            string strsql = "select PS_ID from TBPM_PAINTSCHEME where PS_ID like '%" + fixText + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            return ID.ToArray();
        }
        //获取车库存
        [WebMethod]
        public string[] carkucun(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = carkucun(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }
        private string[] carkucun(string fixText)
        {
            //if (string.IsNullOrEmpty(fixText) == true || count <= 0) return null;//检测参数是否为空
            List<string> kc_name = new List<string>();
            string strsql = "select top 15 KC_MC+'|'+KC_GG+'|'+KC_DANWEI+'|'+KC_DJ from OM_CARKUCUN where KC_MC like '%" + fixText + "%' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                kc_name.Add(row[0].ToString().Trim());
            }
            return kc_name.ToArray();
        }


        //获取机床信息
        [WebMethod]
        public string[] get_mecinfo(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = get_mecinfo(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }

        private string[] get_mecinfo(string fixText)
        {
            //if (string.IsNullOrEmpty(fixText) == true || count <= 0) return null;//检测参数是否为空
            List<string> mec_code = new List<string>();
            string strsql = "select jc_bh+'|'+jc_type+'|'+jc_gxtypeable+'|'+jc_containper from TBTM_MEC where jc_bh like '%" + fixText + "%' or jc_type like '%" + fixText + "%' or jc_gxtypeable like '%" + fixText + "%' or jc_containper like '%" + fixText + "%' or (jc_bh+jc_type+jc_gxtypeable+jc_containper) like '%" + fixText + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                mec_code.Add(row[0].ToString().Trim());
            }
            return mec_code.ToArray();
        }


        //获取部件信息
        [WebMethod]
        public string[] get_bjinfo(string prefixText, int count, string contextKey)
        {
            // Create array of codes   
            string[] codes = get_bjinfo(prefixText);
            // Return matching codes   
            return (from m in codes select m).Take(count).ToArray();
        }

        private string[] get_bjinfo(string fixText)
        {
            //if (string.IsNullOrEmpty(fixText) == true || count <= 0) return null;//检测参数是否为空
            List<string> bj_code = new List<string>();
            string strsql = "select bjmap+'|'+bjname+'|'+cpname+'|'+context from TBTM_GONGSBASE where context like '%" + fixText + "%' or bjmap like '%" + fixText + "%' or bjname like '%" + fixText + "%' or cpname like '%" + fixText + "%' or (bjmap+bjname+cpname+context) like '%" + fixText + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                bj_code.Add(row[0].ToString().Trim());
            }
            return bj_code.ToArray();
        }





        //获取客户信息
        [WebMethod]
        public string[] get_kehu(string prefixText, int count, string contextKey)
        {
            string[] codes = get_kehuinfo(prefixText);
            return (from m in codes select m).Take(count).ToArray();
        }

        private string[] get_kehuinfo(string fixText)
        {
            List<string> kehu_code = new List<string>();
            string strsql = "select CS_CODE+'|'+CS_NAME+'|'+CS_HRCODE from TBCS_CUSUPINFO where (CS_NAME like '%" + fixText + "%' or CS_HRCODE like '%" + fixText + "%') and CS_TYPE='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dt.Rows)
            {
                kehu_code.Add(row[0].ToString().Trim());
            }
            return kehu_code.ToArray();
        }
    }
}
