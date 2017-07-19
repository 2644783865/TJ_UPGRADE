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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_AjaxFinished : System.Web.UI.Page
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

        #region 入库时，获取可入库的BOM列表
        public void GetBomData()
        {
            taskId = Request.Form["taskId"].ToString();
            if (Request.Form["id"] == null)
            {
                sqlText = "select BM_ZONGXU,BM_TUHAO,BM_CHANAME,BM_NUMBER,BM_TUUNITWGHT,BM_YRKNUM,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state from dbo.View_TM_DQO where  BM_MSSTATUS<>'1' and dbo.Splitnum(BM_ZONGXU,'.')=0  and BM_ENGID='" + taskId + "' ORDER BY dbo.f_formatstr(BM_ZONGXU, '.')";
            }
            else
            {
                string id = Request.Form["id"].ToString();
                int level = id.Split('.').Length;
                sqlText = "select BM_ZONGXU,BM_TUHAO,BM_CHANAME,BM_NUMBER,BM_TUUNITWGHT,BM_YRKNUM,BM_KU,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state from dbo.View_TM_DQO where  BM_MSSTATUS<>'1'  and BM_ENGID='" + taskId + "' and BM_ZONGXU like '" + Request.Form["id"].ToString() + ".%' and dbo.Splitnum(BM_ZONGXU,'.')=" + level.ToString() + " and (BM_KU like '%S%' or BM_MARID='' )  ORDER BY dbo.f_formatstr(BM_ZONGXU, '.')";
            }

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            Response.Write(JsonHelper.CreateJsonOne(dt, true));
        }
        #endregion

        #region 获取库存数据
        public void GetKuCun()
        {

            if (Request["id"] == null)
            {
                int page = Convert.ToInt32(Request["page"]);
                int rows = Convert.ToInt32(Request["rows"]);
                InitPager(rows, page);
                DataTable dt1 = CommonFun.GetDataByPagerQueryParam(pager);


                sqlText = "select count(1) from TBPM_STRINFODQO where" + strWhere();
                DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqlText);
                int num = 0;
                if (dt2.Rows.Count > 0)
                {
                    num = Convert.ToInt16(dt2.Rows[0][0]);
                }
                string json = JsonHelper.CreateJsonParameters(dt1, true, num);
                Response.Write(json);
            }
            else
            {
                string id = Request["id"].ToString();
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

                sqlText = "select a.*,b.*,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state,c.CM_PROJ from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU left join View_CM_TSAJOINPROJ as c on a.BM_ENGID=c.TSA_ID  where dbo.Splitnum(BM_ZONGXU,'.')=" + level + "and a.BM_ENGID='" + engid + "' and a.BM_ZONGXU like '" + zongxu + ".%' and (BM_KU like '%S%' or BM_MARID='' )";
                dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                string json = JsonHelper.CreateJsonOne(dt, true);
                Response.Write(json);



            }

        }

        #endregion

        #region 分页
        private void InitPager(int rows, int page)
        {
            pager.TableName = "TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU left join View_CM_TSAJOINPROJ as c on a.BM_ENGID=c.TSA_ID ";
            pager.PrimaryKey = "BM_ID";
            pager.ShowFields = "a.*,b.*,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state,c.CM_PROJ";
            pager.OrderField = "BM_ID";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = rows;
            pager.PageIndex = page;
        }
        #endregion


        public void ProductIn()
        {
            taskId = Request.Form["taskId"].ToString();
            //string sqlquality = "select AFI_ID,PTC,AFI_TSDEP,AFI_MANNM,AFI_RQSTCDATE,AFI_ENDDATE,AFI_QCMANNM,PARTNM,Marid,GUIGE,CAIZHI,RESULT,AFI_ASSGSTATE from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID left join dbo.TBMA_MATERIAL as c on b.Marid=c.ID where PTC like '%" + taskId + "%'and RESULT not  in ('合格')";
            //DataTable dtquality = DBCallCommon.GetDTUsingSqlText(sqlquality);
            //if (dtquality.Rows.Count > 0 && (Session["POSITION"].ToString() != "1205"||Session["POSITION"].ToString() != "1201"))//特殊情况允许质量主管提交入库单
            //{
            //    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('任务号为【" + taskId + "】下存在未经报检或检验不合格的报检子项,不能提交入库单');", true);
            //    result = "{\"msg\":\"任务号为【" + taskId + "】下存在未经报检或检验不合格的报检子项,如需要提交此类入库单，请联系质量主管\"}";
            //}
            //else
            //{
            string zongxus = Request.Form["zongxu"].ToString().Trim(',');
            string sqr = Request.Form["sqr"].ToString();
            string fzr = Request.Form["fzr"].ToString();
            var docNum = Request.Form["docNum"].ToString();
            List<string> sqllist = new List<string>();
            for (int i = 0; i < zongxus.Split(',').Length; i++)
            {
                sqlText = "insert into TBMP_FINISHED_IN (TFI_PROJ,TFI_SINGNUMBER,TFI_DOCNUM,TFI_ZONGXU,TSA_ID,TFI_NAME,TFI_MAP,TFI_RKNUM,TFI_NUMBER,TFI_WGHT,KU,INDATE,DocuPersonID,REVIEWA,SQRID,SPZT) select CM_PROJ,BM_SINGNUMBER,'" + docNum + "','" + zongxus.Split(',')[i] + "',BM_ENGID,BM_CHANAME,BM_TUHAO,BM_NUMBER,BM_NUMBER,BM_TUUNITWGHT,BM_KU,'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["UserID"] + "','" + fzr + "','" + sqr + "','0' from View_TM_DQO as a left join View_CM_TSAJOINPROJ as b on a.BM_ENGID=b.TSA_ID  where BM_ENGID='" + taskId + "' and BM_ZONGXU='" + zongxus.Split(',')[i] + "'";
                //sqllist.Add(sqlText);
                //sqlText = "update dbo.View_TM_DQO set BM_YRKNUM=BM_NUMBER where BM_PJID not like 'JSB.%' and BM_MSSTATUS<>'1' and BM_ENGID='" + taskId + "' and (BM_ZONGXU = '" + zongxus.Split(',')[i] + "' or BM_ZONGXU like '" + zongxus.Split(',')[i] + ".%')";
                sqllist.Add(sqlText);
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
            //}
            Response.Write(result);


            //string sqltext = "insert into TBMP_FINISHED_IN (TFI_PROJ,TFI_SINGNUMBER,TFI_DOCNUM,TFI_FATHERID,TFI_ZONGXU,TSA_ID,TFI_NAME,TFI_MAP,TFI_RKNUM,TFI_NUMBER,TFI_WGHT,INDATE,NOTE,DocuPersonID,REVIEWA,SQRID,SPZT) VALUES ('" + lab_proj.Text.ToString() + "','" + tfi_singnumber + "','" + tfi_docnum + "','" + tfi_fatherid + "','" + tfi_index + "','" + txt_tsa.Text.ToString() + "','" + tfi_name + "','" + tfi_map + "'," + rknum1 + ",'" + tfi_number + "','" + tfi_wght + "','" + lblInDate.Text.ToString() + "','" + txt_note.Text.ToString().Trim() + "','" + TextBoxexecutorid.Text.ToString() + "','" + cob_fuziren.SelectedValue.ToString() + "','" + cob_sqren.SelectedValue.ToString() + "','0')";
            //              //DBCallCommon.ExeSqlText(sqltext);
            //                list.Add(sqltext);
            //                //sqltext = "update dbo.View_TM_DQO set BM_YRKNUM="+rknum1+" where BM_PJID not like 'JSB.%' and BM_MSSTATUS<>'1' and dbo.Splitnum(BM_ZONGXU,'.')<2 and BM_MARID='' and BM_ENGID='" + txt_tsa.Text.ToString() + "' and BM_ZONGXU = '" + tfi_index + "'";
            //                //list.Add(sqltext);


        }
        private string strWhere()
        {
            string tsaid = Request.Form["tsaid"];
            // string proj = Request.Form["proj"];
            string map = Request.Form["map"];
            string name = Request.Form["name"];

            StringBuilder sb = new StringBuilder();
            sb.Append(" dbo.Splitnum(BM_ZONGXU,'.')=0 and BM_PJID not like 'JSB.%' ");
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

            return sb.ToString();
        }

    }
}
