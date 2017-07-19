using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using ZCZJ_DPF;
using System.Data.SqlClient;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_SALARYBASEDATA : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            CheckUser(ControlFinder);
            this.InitVar();
        }

        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            BindbmData();
        }

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数

        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "View_OM_SALARYBASEDATA";
            pager_org.PrimaryKey = "ST_ID";
            pager_org.ShowFields = "ID,PERSON_GH,ST_ID,BINGJIA_BASEDATAOLD,BINGJIA_BASEDATANEW,BINGJIA_CZRID,BINGJIA_CZRNAME,BINGJIA_CZTIME,BINGJIA_NOTE,JIABAN_BASEDATAOLD,JIABAN_BASEDATANEW,JIABAN_CZRID,JIABAN_CZRNAME,JIABAN_CZTIME,JIABAN_NOTE,NIANJIA_BASEDATAOLD,NIANJIA_BASEDATANEW,NIANJIA_CZRID,NIANJIA_CZRNAME,NIANJIA_CZTIME,NIANJIA_NOTE,YKGW_BASEDATAOLD,YKGW_BASEDATANEW,YKGW_CZRID,YKGW_CZRNAME,YKGW_CZTIME,YKGW_NOTE,ST_NAME,ST_SEQUEN,ST_DEPID,ST_PD,DEP_NAME,DEP_CODE";
            pager_org.OrderField = "DEP_NAME";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 200;
        }

        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (txtName.Text != "")
            {
                sql += " and ST_NAME like '%" + txtName.Text + "%'";
            }
            if (txtgh.Text != "")
            {
                sql += " and PERSON_GH like '%" + txtgh.Text.ToString().Trim() + "%'";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sql += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            if (txtgwxl.Text.Trim() != "")
            {
                sql += " and ST_SEQUEN like '%" + txtgwxl.Text.Trim() + "%'";
            }
            if (radio_zaizhi.Checked)
            {
                sql += " and ST_PD='0'";
            }
            else if (radio_lizhi.Checked)
            {
                sql += " and ST_PD='1'";
            }
            else if (radio_other.Checked)
            {
                sql += " and ST_PD not in('0','1')";
            }
            return sql;
        }
        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptbasedata, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        #endregion


        private void BindbmData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(80, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }

        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncx_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radiozhistate_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            List<string> sqltext = new List<string>();
            sqltext.Clear();
            int times = 0;
            string stid = "";
            foreach (RepeaterItem rptitem in rptbasedata.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("cbxSelect");
                System.Web.UI.WebControls.Label ST_ID = (System.Web.UI.WebControls.Label)rptitem.FindControl("ST_ID");
                if (cbx.Checked == true)
                {
                    stid += ST_ID.Text.ToString() + ",";
                    times++;
                }
            }
            if (times == 0)
            {
                Response.Write("<script>alert('请勾选要删除的数据！')</script>");
                return;
            }
            else
            {
                Response.Write("<script>window.open('OM_SALARYBASEDATADETAIL_ADDDELETE.aspx?FLAG=delete&stid=" + stid.Substring(0, stid.Length - 1) + "','_blank','height=500px,width=1200px')</script>");
            }
        }

        protected string editDq(string stid, string datatype)
        {
            return "javascript:window.showModalDialog('OM_SALARYBASEDATADETAIL_EDIT.aspx?FLAG=edit&stid=" + stid + "&datatype=" + datatype + "','','DialogWidth=1200px;DialogHeight=500px')";
        }
        protected string CKDq(string st_id, string datatype)
        {
            return "javascript:window.showModalDialog('OM_SALARYBASEDATAJLCK.aspx?id=" + st_id + "&datatype=" + datatype + "','','DialogWidth=1200px;DialogHeight=750px')";
        }



        #region 批量导出

        protected void btn_plexport_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select ST_NAME,PERSON_GH,ST_SEQUEN,ST_PD,DEP_NAME,BINGJIA_BASEDATANEW,BINGJIA_BASEDATAOLD,BINGJIA_CZRNAME,BINGJIA_CZTIME,BINGJIA_NOTE,JIABAN_BASEDATANEW,JIABAN_BASEDATAOLD,JIABAN_CZRNAME,JIABAN_CZTIME,JIABAN_NOTE,NIANJIA_BASEDATANEW,NIANJIA_BASEDATAOLD,NIANJIA_CZRNAME,NIANJIA_CZTIME,NIANJIA_NOTE,YKGW_BASEDATANEW,YKGW_BASEDATAOLD,YKGW_CZRNAME,YKGW_CZTIME,YKGW_NOTE from View_OM_SALARYBASEDATA where " + StrWhere() + " order by PERSON_GH";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "薪酬基数" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("薪酬基数导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }
                for (int i = 0; i <= dt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        //病假
        protected void btnbingjia_click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            if (iptbingjia.Value.Trim() != "")
            {
                string sqltext = "update View_OM_SALARYBASEDATA set BINGJIA_BASEDATANEW=" + CommonFun.ComTryDecimal(iptbingjia.Value.Trim()) + " where " + StrWhere();
                listsql.Add(sqltext);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('填写内容不能为空！');</script>");
                return;
            }
            DBCallCommon.ExecuteTrans(listsql);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        //加班
        protected void btnjiaban_click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            if (iptjiaban.Value.Trim() != "")
            {
                string sqltext = "update View_OM_SALARYBASEDATA set JIABAN_BASEDATANEW=" + CommonFun.ComTryDecimal(iptjiaban.Value.Trim()) + " where " + StrWhere();
                listsql.Add(sqltext);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('填写内容不能为空！');</script>");
                return;
            }
            DBCallCommon.ExecuteTrans(listsql);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        //年假
        protected void btnnianjia_click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            if (iptnianjia.Value.Trim() != "")
            {
                string sqltext = "update View_OM_SALARYBASEDATA set NIANJIA_BASEDATANEW=" + CommonFun.ComTryDecimal(iptnianjia.Value.Trim()) + " where " + StrWhere();
                listsql.Add(sqltext);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('填写内容不能为空！');</script>");
                return;
            }
            DBCallCommon.ExecuteTrans(listsql);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        //应扣岗位
        protected void btnykgw_click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            string sqltext = "";
            string sqlgetgdgzinfo = "select * from OM_GDGZ where " + StrWhere();
            System.Data.DataTable dtgdgzinfo = DBCallCommon.GetDTUsingSqlText(sqlgetgdgzinfo);
            if (dtgdgzinfo.Rows.Count > 0)
            {
                for (int i = 0; i < dtgdgzinfo.Rows.Count; i++)
                {
                    sqltext = "update OM_SALARYBASEDATA set YKGW_BASEDATANEW=" + CommonFun.ComTryDecimal(dtgdgzinfo.Rows[i]["GDGZ"].ToString().Trim()) + " where ST_ID='" + dtgdgzinfo.Rows[i]["ST_ID"].ToString().Trim() + "'";
                    listsql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(listsql);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        //更新人员信息
        protected void btnupdateperinfo_click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            string sqltext = "";
            sqltext = "insert into OM_SALARYBASEDATA(PERSON_GH,ST_ID) select ST_WORKNO,ST_ID from TBDS_STAFFINFO where ST_ID not in(select ST_ID from OM_SALARYBASEDATA) and ST_NAME is not null and ST_NAME!='' and ST_WORKNO is not null and ST_WORKNO!=''";
            listsql.Add(sqltext);
            DBCallCommon.ExecuteTrans(listsql);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
    }
}
