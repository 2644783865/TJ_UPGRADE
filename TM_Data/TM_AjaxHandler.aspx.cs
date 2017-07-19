using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;
using System.IO;
using EasyUI;
using System.Text;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_AjaxHandler : System.Web.UI.Page
    {
        string sql;
        string txtName;
        string txtBANCI;
        string hidGuanLianTime;
        string txtBZ;
        string proId;
        string result;
        string tablename;
        string sqlText;
        string txtEngName;
        string txtEngModel;
        string txtPartName;
        string txtTuHao;
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
        //虚拟任务
        public void AddVisualTask()
        {
            string taskid = Request.Form["taskId"].ToString();
            string shebei = Request.Form["shebei"].ToString();
            //if (taskid.Contains('.'))
            //{
            //    Response.Write("errtaskid");
            //}
            //else
            //{
            sql = "select count(1) from TBPM_TCTSASSGN where TSA_ID='" + taskid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (!dt.Rows[0][0].ToString().Contains('0'))
            {
                Response.Write("false");
            }
            else
            {
                sql = "insert into TBPM_TCTSASSGN(TSA_ID,TSA_PJID,TSA_ENGNAME) values ('" + taskid + "','JSB.BOM001','" + shebei + "')";
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
            //}
        }
        //工装任务
        public void AddgongzTask()
        {
            List<string> list = new List<string>();
            string taskidgz = Request.Form["taskIdgz"].ToString();
            string shebeigz = Request.Form["shebeigz"].ToString();
            if (taskidgz.Contains('.'))
            {
                Response.Write("errtaskid");
            }
            else
            {
                sql = "select count(1) from TBPM_TCTSASSGN where TSA_ID='" + taskidgz + "'";
                DataTable dtgz = DBCallCommon.GetDTUsingSqlText(sql);
                if (!dtgz.Rows[0][0].ToString().Contains('0'))
                {
                    Response.Write("false");
                }
                else
                {
                    sql = "insert into TBPM_TCTSASSGN(TSA_ID,TSA_PJID,TSA_ENGNAME) values ('" + taskidgz + "','GONGZHUANG','" + shebeigz + "')";
                    list.Add(sql);
                    string sqlText = "insert into TBMP_MANUTSASSGN(MTA_ID,MTA_PJID,MTA_ENGNAME) values ('" + taskidgz + "','GONGZHUANG','" + shebeigz + "')";
                    list.Add(sqlText);
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Write("true");
                    }
                    catch (Exception)
                    {

                        Response.Write("false");
                    }
                }
            }
        }




        public void UploadGSFiles()
        {
            string result = "";
            if (Request.Files.Count > 0)
            {
                string file = Request.Files[0].FileName;
                string[] files = file.Split('\\');
                string filename = files[files.Length - 1];
                string filepath = @"E:\技术管理附件\工时管理";
                string guanlianTime = DateTime.Now.ToString("yyyyMMddHHmmssff");
                string url = @"E:\技术管理附件\工时管理\" + guanlianTime + filename;
                if (!File.Exists(url))
                {

                    string sqlStr = "insert into TBPM_FILES(BC_CONTEXT,fileLoad,fileUpDate,fileName)";
                    sqlStr += "values('" + guanlianTime + "'";
                    sqlStr += ",'" + filepath + "'";
                    sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                    sqlStr += ",'" + filename + "')";

                    //打开与数据库的连接
                    DBCallCommon.ExeSqlText(sqlStr);
                    Request.Files[0].SaveAs(url);
                    result = "{\"msg\":\"上传成功！\",\"guanlianTime\":\"" + guanlianTime + "\",\"filename\":\"" + filename + "\"}";

                }
                else
                {
                    result = "{\"msg\":\"该文件与服务器某一文件重名，请核对后在上传！\"}";
                }


            }
            else
            {

                result = "{\"msg\":\"请选择文件！\"}";
            }


            Response.Write(result);
            Response.End();
        }

        //工时新增
        public void GongShiNew()
        {
            txtName = Request["txtName"];
            txtBANCI = Request["txtBANCI"];
            hidGuanLianTime = Request["hidGuanLianTime"];
            txtBZ = Request["txtBZ"];
            proId = Request["proId"];
            txtEngName = Request["txtEngName"];
            txtEngModel = Request["txtEngModel"];
            txtPartName = Request["txtPartName"];
            txtTuHao = Request["txtTuHao"];
            sql = "insert into TBPM_GONGSHI(PRO_NAME, PRO_BANCI, PRO_ISSUEDTIME, PRO_NOTE, PRO_FUJIAN,PRO_SUBMITID,PRO_STATE,PRO_CHECKLEVEL,PRO_ENGNAME, PRO_ENGMODEL, PRO_PARTNAME, PRO_TUHAO) values('" + txtName + "','" + txtBANCI + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + txtBZ + "','" + hidGuanLianTime + "','" + Session["UserID"] + "','1','2','" + txtEngName + "','" + txtEngModel + "','" + txtPartName + "','" + txtTuHao + "')";
            try
            {
                DBCallCommon.ExeSqlText(sql);
                result = "{\"msg\":\"更新成功\"}";
            }
            catch (Exception)
            {

                result = "{\"msg\":\"数据异常\"}";
            }
            Response.Write(result);

        }

        //工时更新
        public void GongShiEdit()
        {
            txtName = Request["txtName"];
            txtBANCI = Request["txtBANCI"];
            txtEngName = Request["txtEngName"];
            txtEngModel = Request["txtEngModel"];
            txtPartName = Request["txtPartName"];
            txtTuHao = Request["txtTuHao"];
            hidGuanLianTime = Request["hidGuanLianTime"];
            txtBZ = Request["txtBZ"];
            proId = Request["proId"];
            sql = "update TBPM_GONGSHI set PRO_NAME='" + txtName + "',PRO_BANCI='" + txtBANCI + "',PRO_NOTE='" + txtBZ + "',PRO_FUJIAN='" + hidGuanLianTime + "',PRO_STATE='1',PRO_CHECKLEVEL='2',PRO_ENGNAME='" + txtEngName + "',PRO_ENGMODEL='" + txtEngModel + "',PRO_PARTNAME='" + txtPartName + "',PRO_TUHAO='" + txtTuHao + "' where PRO_ID=" + proId;

            try
            {
                DBCallCommon.ExeSqlText(sql);
                result = "{\"msg\":\"更新成功\"}";
            }
            catch (Exception)
            {

                result = "{\"msg\":\"数据异常\"}";
            }
            Response.Write(result);
        }
        //获取制作明细数据
        public void GetMSdata()
        {
            string msId = Request.Form["msId"].ToString();

            InitList(msId);
            int page = Convert.ToInt32(Request["page"]);
            int rows = Convert.ToInt32(Request["rows"]);
            InitPager(rows, page, tablename);
            DataTable dt1 = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);


            sqlText = "select count(1) from " + tablename + " where" + strWhere();
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqlText);
            int num = 0;
            if (dt2.Rows.Count > 0)
            {
                num = Convert.ToInt16(dt2.Rows[0][0]);
            }
            string json = JsonHelper.CreateJsonParameters(dt1, true, num);
            Response.Write(json);
        }


        //删除批注

        //更新批注或变更备注
        public void DeletePiZhu()
        {
            string mspId = Request.Form["txtPId"].ToString();
            InitList(mspId);
            sqlText = "update " + tablename + " set MS_PIZHUBEIZHU=''  where MS_PID='" + mspId + "'";
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                result = "{\"msg\":\"更新成功\"}";


            }
            catch (Exception)
            {

                result = "{\"msg\":\"更新失败\"}";
            }
            Response.Write(result);
        }

        //更新批注或变更备注
        public void EditBeiZhu()
        {
            string mspId = Request.Form["msPId"].ToString();
            string txtPZ = Request.Form["txtPZ"].ToString();
            string txtBG = Request.Form["txtBG"].ToString();
            string msId = Request.Form["msId"].ToString();
            InitList(mspId);
            sqlText = "update " + tablename + " set MS_PIZHUBEIZHU='" + txtPZ + "',MS_BIANGENGBEIZHU='" + txtBG + "' where MS_ID='" + msId + "'";
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                result = "{\"state\":\"ok\"}";


            }
            catch (Exception)
            {

                result = "{\"state\":\"error\"}";
            }
            Response.Write(result);
        }

        //更新整批的批注

        public void UpdatePiZhu()
        {
            string mspId = Request.Form["txtPId"].ToString();
            string tsaId = Request.Form["txtTsaid"].ToString();
            InitList(mspId);
            sqlText = "update a set a.MS_PIZHUBEIZHU=b.MS_PIZHUBEIZHU from " + tablename + " as a left join (select MS_ZONGXU,MS_PIZHUBEIZHU,ROW_NUMBER()over(partition by MS_ZONGXU order by MS_PID desc )rownum  from " + tablename + " where MS_ENGID='" + tsaId + "' and MS_PIZHUBEIZHU is not null and MS_PIZHUBEIZHU <>'' )b on a.MS_ZONGXU=b.MS_ZONGXU  where a.MS_PID='" + mspId + "'";
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                result = "{\"msg\":\"更新成功\"}";

            }
            catch (Exception)
            {

                result = "{\"msg\":\"更新失败\"}";
            }
            Response.Write(result);


        }

        //判断是正常明细表还是变更明细表
        private void InitList(string msId)
        {

            string[] fields = msId.Split('.');
            string[] cols = fields[1].ToString().Split('/');
            if (cols[0].ToString().Length > 6)  //变更
            {
                tablename = "TBPM_MSCHANGE";
            }
            else
            {
                tablename = "TBPM_MKDETAIL";
            }
        }

        #region 分页
        private void InitPager(int rows, int page, string tablename)
        {
            pager.TableName = tablename;
            pager.PrimaryKey = "MS_ID";
            pager.ShowFields = "*";
            pager.OrderField = "dbo.f_formatstr(MS_NEWINDEX, '.')";
            pager.StrWhere = strWhere();
            pager.OrderType = 0;
            pager.PageSize = rows;
            pager.PageIndex = page;
        }
        #endregion
        private string strWhere()
        {
            string msId = Request.Form["msId"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" MS_PID='" + msId + "'");
            return sb.ToString();
        }
    }
}
