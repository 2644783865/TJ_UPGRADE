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
using ZCZJ_DPF;
using System.Data.SqlClient;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_EATNEWLIST : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
                danyuangehebing();
            }
            //CheckUser(ControlFinder);
            InitVar();
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
            pager_org.TableName = "(select * from OM_EATNEW as a left join OM_EATNEWDETAIL as b on a.eatbh=b.detailbh left join TBDS_STAFFINFO as c on a.eatsqrid=c.ST_ID left join TBDS_DEPINFO as d on c.ST_DEPID=d.DEP_CODE)t";
            pager_org.PrimaryKey = "IDdetail";
            pager_org.ShowFields = "IDdetail,eatbh,DEP_NAME,eatyctime,detailnum,detailprice,(detailnum*detailprice) as detailmoney,eattype,eatsqrname,eatsqtime,eatsqrphone,eatstate,eatsqrnote,baoxiaostate,detailifYP";
            pager_org.OrderField = "eatbh";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 30;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (drp_state.SelectedIndex != 0)
            {
                sql += " and eatstate='" + drp_state.SelectedValue.ToString().Trim() + "'";
            }
            if (radio_mytask.Checked == true)
            {
                sql = "((eatsqrid='" + Session["UserID"].ToString().Trim() + "' and eatstate='0') or (eatshrid1='" + Session["UserID"].ToString().Trim() + "' and eatstate='1' and eatshstate1='0') or (eatshrid2='" + Session["UserID"].ToString().Trim() + "' and eatstate='1' and eatshstate1='1' and eatshstate2='0') or (eatsqrid='" + Session["UserID"].ToString().Trim() + "' and eatstate='3'))";
            }
            if (startdate.Value.Trim() != "")
            {
                sql += " and left(eatyctime,10)>='" + startdate.Value.Trim() + "'";
            }
            if (enddate.Value.Trim() != "")
            {
                sql += " and left(eatyctime,10)<='" + enddate.Value.Trim() + "'";
            }
            if (radio_weibaoxiao.Checked == true)
            {
                sql += " and (baoxiaostate is null or baoxiaostate='')";
            }
            if (radio_yibaoxiao.Checked == true)
            {
                sql += " and baoxiaostate='Y'";
            }

            if (radiowplb_yc.Checked == true)
            {
                sql += " and detailifYP='2'";
            }
            if (radiowplb_yp.Checked == true)
            {
                sql += " and detailifYP='1'";
            }

            string sqltotal = "select sum(detailnum*detailprice) as totalmoney from (select * from OM_EATNEW as a left join OM_EATNEWDETAIL as b on a.eatbh=b.detailbh left join TBDS_STAFFINFO as c on a.eatsqrid=c.ST_ID left join TBDS_DEPINFO as d on c.ST_DEPID=d.DEP_CODE)t where " + sql;
            System.Data.DataTable dttotal = DBCallCommon.GetDTUsingSqlText(sqltotal);
            if (dttotal.Rows.Count > 0)
            {
                lb_totalmoney.Text = dttotal.Rows[0]["totalmoney"].ToString().Trim();
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
            CommonFun.Paging(dt, rptycsq, UCPaging1, palNoData);
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



        protected void radio_all_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }

        protected void radio_mytask_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }

        protected void drp_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }


        protected void radiobaoxiaostate_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }

        protected void radiowplb_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }



        //合并单元格
        private void danyuangehebing()
        {
            for (int i = rptycsq.Items.Count - 1; i > 0; i--)
            {
                HtmlTableCell oCell_previous0 = rptycsq.Items[i - 1].FindControl("tdeatbh") as HtmlTableCell;
                HtmlTableCell oCell0 = rptycsq.Items[i].FindControl("tdeatbh") as HtmlTableCell;

                HtmlTableCell oCell_previous1 = rptycsq.Items[i - 1].FindControl("tdcksh") as HtmlTableCell;
                HtmlTableCell oCell1 = rptycsq.Items[i].FindControl("tdcksh") as HtmlTableCell;

                oCell0.RowSpan = (oCell0.RowSpan == -1) ? 1 : oCell0.RowSpan;
                oCell_previous0.RowSpan = (oCell_previous0.RowSpan == -1) ? 1 : oCell_previous0.RowSpan;

                oCell1.RowSpan = (oCell1.RowSpan == -1) ? 1 : oCell1.RowSpan;
                oCell_previous1.RowSpan = (oCell_previous1.RowSpan == -1) ? 1 : oCell_previous1.RowSpan;



                if (oCell0.InnerText == oCell_previous0.InnerText)
                {
                    oCell0.Visible = false;
                    oCell_previous0.RowSpan += oCell0.RowSpan;

                    oCell1.Visible = false;
                    oCell_previous1.RowSpan += oCell1.RowSpan;

                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }


        protected void btnexport_Click(object sender, EventArgs e)
        {
            string sql = "select DEP_NAME,eatyctime,detailnum,detailprice,(detailnum*detailprice) as detailmoney,eattype,eatsqrname,eatsqtime,eatsqrphone,eatsqrnote,case baoxiaostate when 'Y' then '是' else '否' end as baoxiaostate,case detailifYP when '2' then '用餐' else '饮品' end as detailifYP from (select * from OM_EATNEW as a left join OM_EATNEWDETAIL as b on a.eatbh=b.detailbh left join TBDS_STAFFINFO as c on a.eatsqrid=c.ST_ID left join TBDS_DEPINFO as d on c.ST_DEPID=d.DEP_CODE)t where " + StrWhere() + " order by eatbh DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string filename = "用餐申请导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("用餐申请导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                int zonge = CommonFun.ComTryInt(lb_totalmoney.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                IRow row2 = sheet1.CreateRow(dt.Rows.Count + 1);
                row2.CreateCell(5).SetCellValue("总额：" + zonge.ToString());
                for (int r = 0; r <= dt.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }


        //确认报销
        protected void btnbaoxiao_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            int k = 0;//记录选中的行数
            for (int j = 0; j < rptycsq.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptycsq.Items[j].FindControl("CKBOX_SELECT")).Checked)
                {
                    k++;
                }
            }
            if (k > 0)
            {
                for (int i = 0; i < rptycsq.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptycsq.Items[i].FindControl("CKBOX_SELECT")).Checked)
                    {
                        string eatycbh = ((System.Web.UI.WebControls.Label)rptycsq.Items[i].FindControl("eatycbh")).Text.Trim();
                        string sqltext = "update OM_EATNEW set baoxiaostate='Y' where eatbh='" + eatycbh + "'";
                        sql_list.Add(sqltext);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要确认的项！！！');", true);
                return;
            }

            DBCallCommon.ExecuteTrans(sql_list);
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }

        //取消确认报销
        protected void btnqxbaoxiao_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            int k = 0;//记录选中的行数
            for (int j = 0; j < rptycsq.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptycsq.Items[j].FindControl("CKBOX_SELECT")).Checked)
                {
                    k++;
                }
            }
            if (k > 0)
            {
                for (int i = 0; i < rptycsq.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptycsq.Items[i].FindControl("CKBOX_SELECT")).Checked)
                    {
                        string eatycbh = ((System.Web.UI.WebControls.Label)rptycsq.Items[i].FindControl("eatycbh")).Text.Trim();
                        string sqltext = "update OM_EATNEW set baoxiaostate='' where eatbh='" + eatycbh + "'";
                        sql_list.Add(sqltext);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要取消的项！！！');", true);
                return;
            }

            DBCallCommon.ExecuteTrans(sql_list);
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }
    }
}
