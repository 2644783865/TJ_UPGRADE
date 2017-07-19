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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_SHUIDFSQ : System.Web.UI.Page
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
            pager_org.TableName = "(select IDsdmx,IDSDF,ssrens,ssnum,OM_SDFY.startdate,OM_SDFY.enddate,stratdf,enddf,pricedf,startsf,endsf,pricesf,ST_NAME,note,peopleid,fangjnum,gscddl,gscdsl,realmoney from OM_SDFY left join OM_SDFdetail on (OM_SDFY.ssnum=OM_SDFdetail.fangjnum and OM_SDFY.startdate=OM_SDFdetail.startdate and OM_SDFY.enddate=OM_SDFdetail.enddate) left join TBDS_STAFFINFO on OM_SDFdetail.peopleid=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t";
            pager_org.PrimaryKey = "IDsdmx";
            pager_org.ShowFields = "IDsdmx,IDSDF,ssrens,ssnum,startdate,enddate,stratdf,enddf,cast((enddf-stratdf-gscddl) as decimal(12,2)) as shiydl,pricedf,cast(((enddf-stratdf-gscddl)*pricedf) as decimal(12,2)) as dianfje,startsf,endsf,cast((endsf-startsf-gscdsl) as decimal(12,2)) as shiysl,pricesf,cast(((endsf-startsf-gscdsl)*pricesf) as decimal(12,2)) as shuifje,ST_NAME,cast((((enddf-stratdf-gscddl)*pricedf)/ssrens+((endsf-startsf-gscdsl)*pricesf)/ssrens) as decimal(12,2)) as renjunfy,realmoney,note,gscddl,gscdsl";
            pager_org.OrderField = "ssnum,startdate,enddate";
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
                sql += " and ssnum like '" + drp_state.SelectedValue.ToString().Trim() + "%'";
            }
            if (tbfjnum.Text.Trim() != "")
            {
                sql += " and ssnum like '%" + tbfjnum.Text.Trim() + "%'";
            }
            if (tbname.Text.Trim() != "")
            {
                sql += " and ST_NAME like '%" + tbname.Text.Trim() + "%'";
            }
            if (txtqsrq.Text.Trim() != "")
            {
                sql += " and enddate >= '" + txtqsrq.Text.Trim() + "'";
            }
            if (txtjzrq.Text.Trim() != "")
            {
                sql += " and enddate <= '" + txtjzrq.Text.Trim() + "'";
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
            CommonFun.Paging(dt, rptsushe, UCPaging1, palNoData);
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


        protected void drp_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }


        protected void btncx_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }
        //合并单元格
        private void danyuangehebing()
        {
            for (int i = rptsushe.Items.Count - 1; i > 0; i--)
            {
                HtmlTableCell oCell_previous0 = rptsushe.Items[i - 1].FindControl("td_IDSDF") as HtmlTableCell;
                HtmlTableCell oCell0 = rptsushe.Items[i].FindControl("td_IDSDF") as HtmlTableCell;

                HtmlTableCell oCell_previous1 = rptsushe.Items[i - 1].FindControl("td_ssnum") as HtmlTableCell;
                HtmlTableCell oCell1 = rptsushe.Items[i].FindControl("td_ssnum") as HtmlTableCell;

                HtmlTableCell oCell_previous2 = rptsushe.Items[i - 1].FindControl("td_startdate") as HtmlTableCell;
                HtmlTableCell oCell2 = rptsushe.Items[i].FindControl("td_startdate") as HtmlTableCell;

                HtmlTableCell oCell_previous3 = rptsushe.Items[i - 1].FindControl("td_enddate") as HtmlTableCell;
                HtmlTableCell oCell3 = rptsushe.Items[i].FindControl("td_enddate") as HtmlTableCell;

                HtmlTableCell oCell_previous4 = rptsushe.Items[i - 1].FindControl("td_stratdf") as HtmlTableCell;
                HtmlTableCell oCell4 = rptsushe.Items[i].FindControl("td_stratdf") as HtmlTableCell;

                HtmlTableCell oCell_previous5 = rptsushe.Items[i - 1].FindControl("td_enddf") as HtmlTableCell;
                HtmlTableCell oCell5 = rptsushe.Items[i].FindControl("td_enddf") as HtmlTableCell;

                HtmlTableCell oCell_previous6 = rptsushe.Items[i - 1].FindControl("td_shiydl") as HtmlTableCell;
                HtmlTableCell oCell6 = rptsushe.Items[i].FindControl("td_shiydl") as HtmlTableCell;

                HtmlTableCell oCell_previous7 = rptsushe.Items[i - 1].FindControl("td_pricedf") as HtmlTableCell;
                HtmlTableCell oCell7 = rptsushe.Items[i].FindControl("td_pricedf") as HtmlTableCell;

                HtmlTableCell oCell_previous8 = rptsushe.Items[i - 1].FindControl("td_dianfje") as HtmlTableCell;
                HtmlTableCell oCell8 = rptsushe.Items[i].FindControl("td_dianfje") as HtmlTableCell;

                HtmlTableCell oCell_previous9 = rptsushe.Items[i - 1].FindControl("td_startsf") as HtmlTableCell;
                HtmlTableCell oCell9 = rptsushe.Items[i].FindControl("td_startsf") as HtmlTableCell;

                HtmlTableCell oCell_previous10 = rptsushe.Items[i - 1].FindControl("td_endsf") as HtmlTableCell;
                HtmlTableCell oCell10 = rptsushe.Items[i].FindControl("td_endsf") as HtmlTableCell;

                HtmlTableCell oCell_previous11 = rptsushe.Items[i - 1].FindControl("td_shiysl") as HtmlTableCell;
                HtmlTableCell oCell11 = rptsushe.Items[i].FindControl("td_shiysl") as HtmlTableCell;

                HtmlTableCell oCell_previous12 = rptsushe.Items[i - 1].FindControl("td_pricesf") as HtmlTableCell;
                HtmlTableCell oCell12 = rptsushe.Items[i].FindControl("td_pricesf") as HtmlTableCell;

                HtmlTableCell oCell_previous13 = rptsushe.Items[i - 1].FindControl("td_shuifje") as HtmlTableCell;
                HtmlTableCell oCell13 = rptsushe.Items[i].FindControl("td_shuifje") as HtmlTableCell;

                HtmlTableCell oCell_previous14 = rptsushe.Items[i - 1].FindControl("td_renjunfy") as HtmlTableCell;
                HtmlTableCell oCell14 = rptsushe.Items[i].FindControl("td_renjunfy") as HtmlTableCell;

                HtmlTableCell oCell_previous15 = rptsushe.Items[i - 1].FindControl("td_note") as HtmlTableCell;
                HtmlTableCell oCell15 = rptsushe.Items[i].FindControl("td_note") as HtmlTableCell;

                oCell0.RowSpan = (oCell0.RowSpan == -1) ? 1 : oCell0.RowSpan;
                oCell_previous0.RowSpan = (oCell_previous0.RowSpan == -1) ? 1 : oCell_previous0.RowSpan;

                oCell1.RowSpan = (oCell1.RowSpan == -1) ? 1 : oCell1.RowSpan;
                oCell_previous1.RowSpan = (oCell_previous1.RowSpan == -1) ? 1 : oCell_previous1.RowSpan;

                oCell2.RowSpan = (oCell2.RowSpan == -1) ? 1 : oCell2.RowSpan;
                oCell_previous2.RowSpan = (oCell_previous2.RowSpan == -1) ? 1 : oCell_previous2.RowSpan;

                oCell3.RowSpan = (oCell3.RowSpan == -1) ? 1 : oCell3.RowSpan;
                oCell_previous3.RowSpan = (oCell_previous3.RowSpan == -1) ? 1 : oCell_previous3.RowSpan;

                oCell4.RowSpan = (oCell4.RowSpan == -1) ? 1 : oCell4.RowSpan;
                oCell_previous4.RowSpan = (oCell_previous4.RowSpan == -1) ? 1 : oCell_previous4.RowSpan;

                oCell5.RowSpan = (oCell5.RowSpan == -1) ? 1 : oCell5.RowSpan;
                oCell_previous5.RowSpan = (oCell_previous5.RowSpan == -1) ? 1 : oCell_previous5.RowSpan;

                oCell6.RowSpan = (oCell6.RowSpan == -1) ? 1 : oCell6.RowSpan;
                oCell_previous6.RowSpan = (oCell_previous6.RowSpan == -1) ? 1 : oCell_previous6.RowSpan;

                oCell7.RowSpan = (oCell7.RowSpan == -1) ? 1 : oCell7.RowSpan;
                oCell_previous7.RowSpan = (oCell_previous7.RowSpan == -1) ? 1 : oCell_previous7.RowSpan;

                oCell8.RowSpan = (oCell8.RowSpan == -1) ? 1 : oCell8.RowSpan;
                oCell_previous8.RowSpan = (oCell_previous8.RowSpan == -1) ? 1 : oCell_previous8.RowSpan;

                oCell9.RowSpan = (oCell9.RowSpan == -1) ? 1 : oCell9.RowSpan;
                oCell_previous9.RowSpan = (oCell_previous9.RowSpan == -1) ? 1 : oCell_previous9.RowSpan;

                oCell10.RowSpan = (oCell10.RowSpan == -1) ? 1 : oCell10.RowSpan;
                oCell_previous10.RowSpan = (oCell_previous10.RowSpan == -1) ? 1 : oCell_previous10.RowSpan;

                oCell11.RowSpan = (oCell11.RowSpan == -1) ? 1 : oCell11.RowSpan;
                oCell_previous11.RowSpan = (oCell_previous11.RowSpan == -1) ? 1 : oCell_previous11.RowSpan;

                oCell12.RowSpan = (oCell12.RowSpan == -1) ? 1 : oCell12.RowSpan;
                oCell_previous12.RowSpan = (oCell_previous12.RowSpan == -1) ? 1 : oCell_previous12.RowSpan;

                oCell13.RowSpan = (oCell13.RowSpan == -1) ? 1 : oCell13.RowSpan;
                oCell_previous13.RowSpan = (oCell_previous13.RowSpan == -1) ? 1 : oCell_previous13.RowSpan;

                oCell14.RowSpan = (oCell14.RowSpan == -1) ? 1 : oCell14.RowSpan;
                oCell_previous14.RowSpan = (oCell_previous14.RowSpan == -1) ? 1 : oCell_previous14.RowSpan;

                oCell15.RowSpan = (oCell15.RowSpan == -1) ? 1 : oCell15.RowSpan;
                oCell_previous15.RowSpan = (oCell_previous15.RowSpan == -1) ? 1 : oCell_previous15.RowSpan;


                if (oCell0.InnerText == oCell_previous0.InnerText)
                {
                    oCell1.Visible = false;
                    oCell_previous1.RowSpan += oCell1.RowSpan;

                    oCell2.Visible = false;
                    oCell_previous2.RowSpan += oCell2.RowSpan;

                    oCell3.Visible = false;
                    oCell_previous3.RowSpan += oCell3.RowSpan;

                    oCell4.Visible = false;
                    oCell_previous4.RowSpan += oCell4.RowSpan;

                    oCell5.Visible = false;
                    oCell_previous5.RowSpan += oCell5.RowSpan;

                    oCell6.Visible = false;
                    oCell_previous6.RowSpan += oCell6.RowSpan;

                    oCell7.Visible = false;
                    oCell_previous7.RowSpan += oCell7.RowSpan;

                    oCell8.Visible = false;
                    oCell_previous8.RowSpan += oCell8.RowSpan;

                    oCell9.Visible = false;
                    oCell_previous9.RowSpan += oCell9.RowSpan;

                    oCell10.Visible = false;
                    oCell_previous10.RowSpan += oCell10.RowSpan;

                    oCell11.Visible = false;
                    oCell_previous11.RowSpan += oCell11.RowSpan;

                    oCell12.Visible = false;
                    oCell_previous12.RowSpan += oCell12.RowSpan;

                    oCell13.Visible = false;
                    oCell_previous13.RowSpan += oCell13.RowSpan;

                    oCell14.Visible = false;
                    oCell_previous14.RowSpan += oCell14.RowSpan;

                    oCell15.Visible = false;
                    oCell_previous15.RowSpan += oCell15.RowSpan;
                }
            }
        }
    }
}
