using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Contract_SW_SKDETAIL : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {

                this.GetBoundData();
            }
        }
        #region 分页
        protected void GetBoundData()
        {
            InitPager();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, grvYKJL, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private void InitPager()
        {
            pager.TableName = "TBPM_BUSPAYMENTRECORD as a left join TBPM_CONPCHSINFO as b on a.BP_HTBH=b.PCON_BCODE";
            pager.PrimaryKey = "a.ID";
            pager.ShowFields = "BP_ID, BP_HTBH, BP_KXMC, BP_YKRQ, BP_SKRQ, BP_JE, BP_SFJE, BP_SKFS, BP_STATE, BP_NOTEFST, BP_NOTESND, BP_PZ, BP_PZH,b.PCON_PJNAME,PCON_ENGTYPE,PCON_CUSTMNAME,PCON_YZHTH,PCON_JINE,PCON_ENGNAME";
            pager.OrderField = ddlPailie.SelectedValue;
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 20;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr()
        {
            string sql = " 1=1 ";

            if (ddrKxmc.SelectedIndex != 0)
            {
                sql += " and BP_KXMC='" + ddrKxmc.SelectedValue + "'";
            }
            if (txtDuifangConId.Text != "")
            {
                sql += " and PCON_YZHTH like '%" + txtDuifangConId.Text + "%'";
            }
            if (txtProj.Text != "")
            {
                sql += " and PCON_ENGNAME like '%" + txtProj.Text + "%'";
            }
            if (ddlState.SelectedIndex != 0)
            {
                sql += " and BP_STATE='" + ddlState.SelectedValue + "'";
            }
            if (txtConId.Text != "")
            {
                sql += " and BP_HTBH like '%" + txtConId.Text.Trim() + "%'";
            }
            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {

            GetBoundData();
        }



        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            this.GetBoundData();
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sqlText = "select BP_ID,PCON_CUSTMNAME,BP_HTBH,PCON_YZHTH,PCON_ENGNAME,PCON_ENGTYPE,PCON_JINE,BP_JE,BP_YKRQ,cast(BP_NOTEFST as varchar)+'%',case when BP_STATE=0 then '未到账' when BP_STATE=1 then '已到帐' else '' end ,BP_KXMC from TBPM_BUSPAYMENTRECORD as a left join TBPM_CONPCHSINFO as b on a.BP_HTBH=b.PCON_BCODE where " + CreateConStr() + " order by " + ddlPailie.SelectedValue;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            string filename = "付款明细清单.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();

            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("付款明细清单.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);


                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(i.ToString());
                    for (int j = 0; j < 11; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }


                sheet.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
