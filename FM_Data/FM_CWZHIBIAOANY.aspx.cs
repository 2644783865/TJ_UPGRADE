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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_CWZHIBIAOANY : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
                drawingdata();
            }
            CheckUser(ControlFinder);
        }
        //绑定数据
        private void binddata()
        {
            //数据绑定1
            string sqltext0 = "select *,(yychengben+xsfeiyong+glfeiyong+cwfeiyong) as cbfeiyonghj,(yyshouru-yychengben-xsfeiyong-glfeiyong-cwfeiyong) as lrzonge from FM_CWZHIBIAO where " + strstring1() + " order by cw_yearmonth desc";
            System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext0);
            if (dt0.Rows.Count > 0)
            {
                rptdata0.DataSource = dt0;
                rptdata0.DataBind();
            }
        }
        //绘制图形
        private void drawingdata()
        {
            string sqltext0 = "select *,(yychengben+xsfeiyong+glfeiyong+cwfeiyong) as cbfeiyonghj,(yyshouru-yychengben-xsfeiyong-glfeiyong-cwfeiyong) as lrzonge,case when yyshouru=0 then 0 else cast((yyshouru-yychengben)*100/yyshouru as decimal(12,2)) end as maolilv from FM_CWZHIBIAO where " + strstring1() + " order by cw_yearmonth asc";

            DataTable dt0 = new DataTable();
            dt0 = DBCallCommon.GetDTUsingSqlText(sqltext0);

            //去年同期数据
            string sqltext1 = "select *,(yychengben+xsfeiyong+glfeiyong+cwfeiyong) as cbfeiyonghj,(yyshouru-yychengben-xsfeiyong-glfeiyong-cwfeiyong) as lrzonge,case when yyshouru=0 then 0 else cast((yyshouru-yychengben)*100/yyshouru as decimal(12,2)) end as lastmaolilv from FM_CWZHIBIAO where " + strstring2() + " order by cw_yearmonth asc";

            DataTable dt1 = new DataTable();
            dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);

            //净利润及利润率
            string sqltext2 = "select RQBH,cast(cast(LRFP_JLR as float)/10000 as decimal(12,2)) as LRFP_JLR,case when (LRFP_YYSR='0' or LRFP_YYSR is null or LRFP_YYSR='') then 0 else cast(cast(LRFP_JLR as float)*100/cast(LRFP_YYSR as float) as decimal(12,2)) end as jlirunlv from TBFM_LRFP where " + strstring3() + " order by RQBH asc";

            DataTable dt2 = new DataTable();
            dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);

            //净利润及利润率去年同期值
            string sqltext3 = "select RQBH,cast(cast(LRFP_JLR as float)/10000 as decimal(12,2)) as LRFP_JLR,case when (LRFP_YYSR='0' or LRFP_YYSR is null or LRFP_YYSR='') then 0 else cast(cast(LRFP_JLR as float)*100/cast(LRFP_YYSR as float) as decimal(12,2)) end as jlirunlv from TBFM_LRFP where " + strstring4() + " order by RQBH asc";

            DataTable dt3 = new DataTable();
            dt3 = DBCallCommon.GetDTUsingSqlText(sqltext3);


            if (dt0.Rows.Count > 0)
            {
                DataView dv0 = new DataView(dt0);
                Chart1.Series["营业收入"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "yyshouru");
                Chart1.Series["营业成本"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "yychengben");
                Chart1.Series["营业收入"].Label = "#VALY";
                Chart1.Series["营业成本"].Label = "#VALY";
                Chart1.Series["营业收入"].ToolTip = "#VALY";
                Chart1.Series["营业成本"].ToolTip = "#VALY";

                Chart6.Series["毛利率"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "maolilv");
                Chart6.Series["毛利率"].Label = "#VALY"+"%";
                Chart6.Series["毛利率"].ToolTip = "#VALY"+"%";

                Chart2.Series["营业成本"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "yychengben");
                //Chart2.Series["营业成本预算"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "yychengbenys");
                Chart2.Series["营业成本"].Label = "#VALY";
                //Chart2.Series["营业成本预算"].Label = "#VALY";
                Chart2.Series["营业成本"].ToolTip = "#VALY";
                //Chart2.Series["营业成本预算"].ToolTip = "#VALY";

                Chart3.Series["销售费用"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "xsfeiyong");
                //Chart3.Series["销售费用预算"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "xsfeiyongys");
                Chart3.Series["销售费用"].Label = "#VALY";
                //Chart3.Series["销售费用预算"].Label = "#VALY";
                Chart3.Series["销售费用"].ToolTip = "#VALY";
                //Chart3.Series["销售费用预算"].ToolTip = "#VALY";

                Chart4.Series["管理费用"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "glfeiyong");
                //Chart4.Series["管理费用预算"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "glfeiyongys");
                Chart4.Series["管理费用"].Label = "#VALY";
                //Chart4.Series["管理费用预算"].Label = "#VALY";
                Chart4.Series["管理费用"].ToolTip = "#VALY";
                //Chart4.Series["管理费用预算"].ToolTip = "#VALY";

                Chart5.Series["财务费用"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "cwfeiyong");
                //Chart5.Series["财务费用预算"].Points.DataBindXY(dv0, "cw_yearmonth", dv0, "cwfeiyongys");
                Chart5.Series["财务费用"].Label = "#VALY";
                //Chart5.Series["财务费用预算"].Label = "#VALY";
                Chart5.Series["财务费用"].ToolTip = "#VALY";
                //Chart5.Series["财务费用预算"].ToolTip = "#VALY";
            }

            //上一年度数据
            if (dt1.Rows.Count > 0)
            {
                DataView dv1 = new DataView(dt1);
                Chart1.Series["营业收入去年同期值"].Points.DataBindY(dv1, "yyshouru");
                Chart1.Series["营业成本去年同期值"].Points.DataBindY(dv1, "yychengben");
                Chart1.Series["营业收入去年同期值"].Label = "#VALY";
                Chart1.Series["营业成本去年同期值"].Label = "#VALY";
                Chart1.Series["营业收入去年同期值"].ToolTip = "#VALY";
                Chart1.Series["营业成本去年同期值"].ToolTip = "#VALY";
                Chart1.Series["营业收入去年同期值"].LabelForeColor = System.Drawing.Color.Red;
                Chart1.Series["营业成本去年同期值"].LabelForeColor = System.Drawing.Color.Red;

                Chart6.Series["毛利率去年同期值"].Points.DataBindY(dv1, "lastmaolilv");
                Chart6.Series["毛利率去年同期值"].Label = "#VALY"+"%";
                Chart6.Series["毛利率去年同期值"].ToolTip = "#VALY"+"%";
                Chart6.Series["毛利率去年同期值"].LabelForeColor = System.Drawing.Color.Red;

                Chart2.Series["营业成本去年同期值"].Points.DataBindY(dv1, "yychengben");
                Chart2.Series["营业成本去年同期值"].Label = "#VALY";
                Chart2.Series["营业成本去年同期值"].ToolTip = "#VALY";
                Chart2.Series["营业成本去年同期值"].LabelForeColor = System.Drawing.Color.Red;

                Chart3.Series["销售费用去年同期值"].Points.DataBindY(dv1, "xsfeiyong");
                Chart3.Series["销售费用去年同期值"].Label = "#VALY";
                Chart3.Series["销售费用去年同期值"].ToolTip = "#VALY";
                Chart3.Series["销售费用去年同期值"].LabelForeColor = System.Drawing.Color.Red;

                Chart4.Series["管理费用去年同期值"].Points.DataBindY(dv1, "glfeiyong");
                Chart4.Series["管理费用去年同期值"].Label = "#VALY";
                Chart4.Series["管理费用去年同期值"].ToolTip = "#VALY";
                Chart4.Series["管理费用去年同期值"].LabelForeColor = System.Drawing.Color.Red;

                Chart5.Series["财务费用去年同期值"].Points.DataBindY(dv1, "cwfeiyong");
                Chart5.Series["财务费用去年同期值"].Label = "#VALY";
                Chart5.Series["财务费用去年同期值"].ToolTip = "#VALY";
                Chart5.Series["财务费用去年同期值"].LabelForeColor = System.Drawing.Color.Red;
            }

            //净利润及利润率
            if (dt2.Rows.Count > 0)
            {
                DataView dv2 = new DataView(dt2);
                Chart7.Series["净利润"].Points.DataBindXY(dv2, "RQBH", dv2, "LRFP_JLR");
                Chart8.Series["利润率"].Points.DataBindXY(dv2, "RQBH", dv2, "jlirunlv");
                Chart7.Series["净利润"].Label = "#VALY";
                Chart8.Series["利润率"].Label = "#VALY" + "%";
                Chart7.Series["净利润"].ToolTip = "#VALY";
                Chart8.Series["利润率"].ToolTip = "#VALY" + "%";
            }

            //净利润及利润率去年同期值
            if (dt3.Rows.Count > 0)
            {
                DataView dv3 = new DataView(dt3);
                Chart7.Series["净利润去年同期值"].Points.DataBindXY(dv3, "RQBH", dv3, "LRFP_JLR");
                Chart8.Series["利润率去年同期值"].Points.DataBindXY(dv3, "RQBH", dv3, "jlirunlv");
                Chart7.Series["净利润去年同期值"].Label = "#VALY";
                Chart8.Series["利润率去年同期值"].Label = "#VALY" + "%";
                Chart7.Series["净利润去年同期值"].ToolTip = "#VALY";
                Chart8.Series["利润率去年同期值"].ToolTip = "#VALY" + "%";
                Chart7.Series["净利润去年同期值"].LabelForeColor = System.Drawing.Color.Red;
                Chart8.Series["利润率去年同期值"].LabelForeColor = System.Drawing.Color.Red;
            }


            Chart1.Series["营业收入"].ChartType = SeriesChartType.Column;
            Chart1.Series["营业成本"].ChartType = SeriesChartType.Column;

            Chart6.Series["毛利率"].ChartType = SeriesChartType.Line;

            Chart2.Series["营业成本"].ChartType = SeriesChartType.Column;
            //Chart2.Series["营业成本预算"].ChartType = SeriesChartType.Column;

            Chart3.Series["销售费用"].ChartType = SeriesChartType.Column;
            //Chart3.Series["销售费用预算"].ChartType = SeriesChartType.Column;

            Chart4.Series["管理费用"].ChartType = SeriesChartType.Column;
            //Chart4.Series["管理费用预算"].ChartType = SeriesChartType.Column;

            Chart5.Series["财务费用"].ChartType = SeriesChartType.Column;
            //Chart5.Series["财务费用预算"].ChartType = SeriesChartType.Column;
            //去年数据
            Chart1.Series["营业收入去年同期值"].ChartType = SeriesChartType.Column;
            Chart1.Series["营业成本去年同期值"].ChartType = SeriesChartType.Column;

            Chart2.Series["营业成本去年同期值"].ChartType = SeriesChartType.Column;

            Chart3.Series["销售费用去年同期值"].ChartType = SeriesChartType.Column;

            Chart4.Series["管理费用去年同期值"].ChartType = SeriesChartType.Column;

            Chart5.Series["财务费用去年同期值"].ChartType = SeriesChartType.Column;

            Chart6.Series["毛利率去年同期值"].ChartType = SeriesChartType.Line;

            //净利润及利润率
            Chart7.Series["净利润"].ChartType = SeriesChartType.Column;
            Chart8.Series["利润率"].ChartType = SeriesChartType.Line;

            //净利润及利润率去年同期值
            Chart7.Series["净利润去年同期值"].ChartType = SeriesChartType.Column;
            Chart8.Series["利润率去年同期值"].ChartType = SeriesChartType.Line;
        }
        //查询条件1
        private string strstring1()
        {
            string sqlText = "1=1";
            if (yearmonthstart.Value.Trim() == "" && yearmonthend.Value.Trim() == "")
            {
                sqlText += " and cw_yearmonth like '" + DateTime.Now.ToString("yyyy").Trim() + "-%'";
            }
            else
            {
                if (yearmonthstart.Value.Trim() != "" && yearmonthend.Value.Trim() != "")
                {
                    sqlText += " and cw_yearmonth>='" + yearmonthstart.Value.Trim() + "' and cw_yearmonth<='" + yearmonthend.Value.Trim() + "'";
                }
            }
            return sqlText;
        }
        //查询条件2
        private string strstring2()
        {
            string sqlText = "1=1";
            if (yearmonthstart.Value.Trim() == "" && yearmonthend.Value.Trim() == "")
            {
                sqlText += " and cw_yearmonth like '" + DateTime.Now.AddYears(-1).ToString("yyyy").Trim() +"-%'";
            }
            else
            {
                if (yearmonthstart.Value.Trim() != "" && yearmonthend.Value.Trim() != "")
                {

                    DateTime startyearmonth = DateTime.Parse(yearmonthstart.Value.Trim());
                    DateTime endyearmonth = DateTime.Parse(yearmonthend.Value.Trim());
                    string laststartyearmonth = startyearmonth.AddYears(-1).ToString("yyyy-MM").Trim();
                    string lastendyearmonth = endyearmonth.AddYears(-1).ToString("yyyy-MM").Trim();
                    sqlText += " and cw_yearmonth>='" + laststartyearmonth + "' and cw_yearmonth<='" + lastendyearmonth + "'";
                }
            }
            return sqlText;
        }
        //查询条件3
        private string strstring3()
        {
            string sqlText = "LRFP_TYPE='本年累计数'";
            if (yearmonthstart.Value.Trim() == "" && yearmonthend.Value.Trim() == "")
            {
                sqlText += " and RQBH like '" + DateTime.Now.ToString("yyyy").Trim() + "-%'";
            }
            else
            {
                if (yearmonthstart.Value.Trim() != "" && yearmonthend.Value.Trim() != "")
                {
                    sqlText += " and RQBH>='" + yearmonthstart.Value.Trim() + "' and RQBH<='" + yearmonthend.Value.Trim() + "'";
                }
            }
            return sqlText;
        }

        //查询条件4
        private string strstring4()
        {
            string sqlText = "LRFP_TYPE='本年累计数'";
            if (yearmonthstart.Value.Trim() == "" && yearmonthend.Value.Trim() == "")
            {
                sqlText += " and RQBH like '" + DateTime.Now.AddYears(-1).ToString("yyyy").Trim() + "-%'";
            }
            else
            {
                if (yearmonthstart.Value.Trim() != "" && yearmonthend.Value.Trim() != "")
                {
                    DateTime startyearmonth = DateTime.Parse(yearmonthstart.Value.Trim());
                    DateTime endyearmonth = DateTime.Parse(yearmonthend.Value.Trim());
                    string laststartyearmonth = startyearmonth.AddYears(-1).ToString("yyyy-MM").Trim();
                    string lastendyearmonth = endyearmonth.AddYears(-1).ToString("yyyy-MM").Trim();
                    sqlText += " and RQBH>='" + laststartyearmonth + "' and RQBH<='" + lastendyearmonth + "'";
                }
            }
            return sqlText;
        }

        //查询
        protected void btnsearch_click(object sender, EventArgs e)
        {
            if (yearmonthstart.Value.Trim() == "" || yearmonthend.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择起始年月！');", true);
                return;
            }
            if (yearmonthstart.Value.Trim().Substring(0, 4).ToString().Trim() != yearmonthend.Value.Trim().Substring(0, 4).ToString().Trim())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('时间不能跨年度！');", true);
                return;
            }
            updatedata();
            binddata();
            drawingdata();
        }

        //更新基础数据，插入基础数据中不存在的数据
        private void updatedata()
        {
            List<string> listsql = new List<string>();
            string sqltext = "";
            DateTime startyearmonthins = DateTime.Parse(yearmonthstart.Value.Trim());
            DateTime endyearmonthins = DateTime.Parse(yearmonthend.Value.Trim());
            string laststartyearmonthins = startyearmonthins.AddYears(-1).ToString("yyyy-MM").Trim();
            string lastendyearmonthins = endyearmonthins.AddYears(-1).ToString("yyyy-MM").Trim();
            //删除原有数据
            
            string sqldelete1 = "delete from FM_CWZHIBIAO where cw_yearmonth>='" + yearmonthstart.Value.Trim() + "' and cw_yearmonth<='" + yearmonthend.Value.Trim() + "'";
            DBCallCommon.ExeSqlText(sqldelete1);
            string sqldelete2 = "delete from FM_CWZHIBIAO where cw_yearmonth>='" + laststartyearmonthins + "' and cw_yearmonth<='" + lastendyearmonthins + "'";
            DBCallCommon.ExeSqlText(sqldelete2);

            DateTime nianyuestart;
            DateTime nianyueend;
            nianyuestart = DateTime.Parse(yearmonthstart.Value.ToString().Trim());
            nianyueend = DateTime.Parse(yearmonthend.Value.ToString().Trim());

            //上一年度
            DateTime lastnianyuestart;
            DateTime lastnianyueend;
            lastnianyuestart = DateTime.Parse(laststartyearmonthins);
            lastnianyueend = DateTime.Parse(lastendyearmonthins);

            int monthnum = (nianyueend.Year - nianyuestart.Year) * 12 + nianyueend.Month - nianyuestart.Month + 1;
            for (int j = 0; j < monthnum; j++)
            {
                DateTime nianyuethis = nianyuestart.AddMonths(j);
                DateTime nianyuethisnext = nianyuestart.AddMonths(j + 1);
                string nianyue = nianyuethis.ToString("yyyy-MM").Trim();
                string nianyuenext = nianyuethisnext.ToString("yyyy-MM").Trim();
                string sqlcheck0 = "select * from FM_CWZHIBIAO where cw_yearmonth='" + nianyue + "'";
                DataTable dtcheck0 = DBCallCommon.GetDTUsingSqlText(sqlcheck0);
                if (dtcheck0.Rows.Count == 0)
                {
                    string sqlcheck1 = "select * from TBFM_LRFP where RQBH='" + nianyue + "' and LRFP_TYPE='本年累计数'";
                    DataTable dtcheck1 = DBCallCommon.GetDTUsingSqlText(sqlcheck1);
                    //预算
                    //string sqlcheck2 = "select * from FM_CWZHIBIAO where cw_yearmonth like '" + nianyue.Substring(0, 4).ToString().Trim() + "-%'";
                    //DataTable dtcheck2 = DBCallCommon.GetDTUsingSqlText(sqlcheck2);
                    if (dtcheck1.Rows.Count > 0)
                    {
                        //sqltext = "insert into FM_CWZHIBIAO(cw_yearmonth,cw_nextyearmonth,cw_zdrname,cw_zdrid,cw_zdtime,yychengben,yychengbenys,xsfeiyong,xsfeiyongys,glfeiyong,glfeiyongys,cwfeiyong,cwfeiyongys,cbfeiyonghjys,yyshouru,yyshouruys,lrzongeys,cw_note,beiyong1) values('" + nianyue + "','" + nianyuenext + "','" + Session["UserName"].ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "'," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR_JYCB"].ToString().Trim())) / 10000), 2) + "," + CommonFun.ComTryDecimal(dtcheck2.Rows[0]["yychengbenys"].ToString().Trim()) + "," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR_XSFY"].ToString().Trim())) / 10000), 2) + "," + CommonFun.ComTryDecimal(dtcheck2.Rows[0]["xsfeiyongys"].ToString().Trim()) + "," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR_GLFY"].ToString().Trim())) / 10000), 2) + "," + CommonFun.ComTryDecimal(dtcheck2.Rows[0]["glfeiyongys"].ToString().Trim()) + "," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR_CWFY"].ToString().Trim())) / 10000), 2) + "," + CommonFun.ComTryDecimal(dtcheck2.Rows[0]["cwfeiyongys"].ToString().Trim()) + "," + CommonFun.ComTryDecimal(dtcheck2.Rows[0]["cbfeiyonghjys"].ToString().Trim()) + "," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR"].ToString().Trim())) / 10000), 2) + "," + CommonFun.ComTryDecimal(dtcheck2.Rows[0]["yyshouruys"].ToString().Trim()) + "," + CommonFun.ComTryDecimal(dtcheck2.Rows[0]["lrzongeys"].ToString().Trim()) + ",'','')";
                        sqltext = "insert into FM_CWZHIBIAO(cw_yearmonth,cw_nextyearmonth,cw_zdrname,cw_zdrid,cw_zdtime,yychengben,xsfeiyong,glfeiyong,cwfeiyong,yyshouru,cw_note,beiyong1) values('" + nianyue + "','" + nianyuenext + "','" + Session["UserName"].ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "'," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR_JYCB"].ToString().Trim())) / 10000), 2) + "," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR_XSFY"].ToString().Trim())) / 10000), 2) + "," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR_GLFY"].ToString().Trim())) / 10000), 2) + "," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR_CWFY"].ToString().Trim())) / 10000), 2) + "," + Math.Round(((CommonFun.ComTryDouble(dtcheck1.Rows[0]["LRFP_YYSR"].ToString().Trim())) / 10000), 2) + ",'','')";
                        listsql.Add(sqltext);
                    }
                }



                DateTime lastnianyuethis = lastnianyuestart.AddMonths(j);
                DateTime lastnianyuethisnext = lastnianyuestart.AddMonths(j + 1);
                string lastnianyue = lastnianyuethis.ToString("yyyy-MM").Trim();
                string lastnianyuenext = lastnianyuethisnext.ToString("yyyy-MM").Trim();
                string lastsqlcheck0 = "select * from FM_CWZHIBIAO where cw_yearmonth='" + lastnianyue + "'";
                DataTable lastdtcheck0 = DBCallCommon.GetDTUsingSqlText(lastsqlcheck0);
                if (lastdtcheck0.Rows.Count == 0)
                {
                    string lastsqlcheck1 = "select * from TBFM_LRFP where RQBH='" + lastnianyue + "' and LRFP_TYPE='本年累计数'";
                    DataTable lastdtcheck1 = DBCallCommon.GetDTUsingSqlText(lastsqlcheck1);
                    if (lastdtcheck1.Rows.Count > 0)
                    {
                        sqltext = "insert into FM_CWZHIBIAO(cw_yearmonth,cw_nextyearmonth,cw_zdrname,cw_zdrid,cw_zdtime,yychengben,xsfeiyong,glfeiyong,cwfeiyong,yyshouru,cw_note,beiyong1) values('" + lastnianyue + "','" + lastnianyuenext + "','" + Session["UserName"].ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "'," + Math.Round(((CommonFun.ComTryDouble(lastdtcheck1.Rows[0]["LRFP_YYSR_JYCB"].ToString().Trim())) / 10000), 2) + "," + Math.Round(((CommonFun.ComTryDouble(lastdtcheck1.Rows[0]["LRFP_YYSR_XSFY"].ToString().Trim())) / 10000), 2) + "," + Math.Round(((CommonFun.ComTryDouble(lastdtcheck1.Rows[0]["LRFP_YYSR_GLFY"].ToString().Trim())) / 10000), 2) + "," + Math.Round(((CommonFun.ComTryDouble(lastdtcheck1.Rows[0]["LRFP_YYSR_CWFY"].ToString().Trim())) / 10000), 2) + "," + Math.Round(((CommonFun.ComTryDouble(lastdtcheck1.Rows[0]["LRFP_YYSR"].ToString().Trim())) / 10000), 2) + ",'','')";
                        listsql.Add(sqltext);
                    }
                }
            }
            DBCallCommon.ExecuteTrans(listsql);
        }
    }
}
