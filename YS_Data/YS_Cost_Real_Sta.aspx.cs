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

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Real_Sta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                drawingdata();
            }
        }
        //绘制图形
        private void drawingdata()
        {
            string sqltext0 = "select Convert(decimal(10,2), (YS_PROFIT/10000)) AS YS_PROFIT, Convert(decimal(10,2), (YS_PROFIT_BG/10000)) AS YS_PROFIT_BG, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt0 = new DataTable();
            dt0 = DBCallCommon.GetDTUsingSqlText(sqltext0);

            string sqltext1 = "select Convert(decimal(10,2), YS_PROFIT_RATE) AS YS_PROFIT_RATE, Convert(decimal(10,2),  YS_PROFIT_RATE_BG) AS YS_PROFIT_RATE_BG, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt1 = new DataTable();
            dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);

            string sqltext2 = "select Convert(decimal(10,2), (YS_CL/10000)) AS YS_CL, Convert(decimal(10,2), (YS_CL_BG/10000)) AS YS_CL_BG, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt2 = new DataTable();
            dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);

            string sqltext3 = "select Convert(decimal(10,2), (YS_FERROUS_METAL1/10000)) AS YS_FERROUS_METAL1,Convert(decimal(10,2), (YS_FERROUS_METAL_BG1/10000)) AS YS_FERROUS_METAL_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt3 = new DataTable();
            dt3 = DBCallCommon.GetDTUsingSqlText(sqltext3);

            string sqltext4 = "select Convert(decimal(10,2), (YS_PURCHASE_PART1/10000)) AS YS_PURCHASE_PART1,Convert(decimal(10,2), (YS_PURCHASE_PART_BG1/10000)) AS YS_PURCHASE_PART_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt4 = new DataTable();
            dt4 = DBCallCommon.GetDTUsingSqlText(sqltext4);

            string sqltext5 = "select Convert(decimal(10,2), (YS_MACHINING_PART1/10000)) AS YS_MACHINING_PART1,Convert(decimal(10,2), (YS_MACHINING_PART_BG1/10000)) AS YS_MACHINING_PART_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt5 = new DataTable();
            dt5 = DBCallCommon.GetDTUsingSqlText(sqltext5);

            string sqltext6 = "select Convert(decimal(10,2), (YS_PAINT_COATING1/10000)) AS YS_PAINT_COATING1,Convert(decimal(10,2), (YS_PAINT_COATING_BG1/10000)) AS YS_PAINT_COATING_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt6 = new DataTable();
            dt6 = DBCallCommon.GetDTUsingSqlText(sqltext6);

            string sqltext7 = "select Convert(decimal(10,2), (YS_ELECTRICAL1/10000)) AS YS_ELECTRICAL1,Convert(decimal(10,2), (YS_ELECTRICAL_BG1/10000)) AS YS_ELECTRICAL_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt7 = new DataTable();
            dt7 = DBCallCommon.GetDTUsingSqlText(sqltext7);

            string sqltext8 = "select Convert(decimal(10,2), (YS_OTHERMAT_COST1/10000)) AS YS_OTHERMAT_COST1,Convert(decimal(10,2), (YS_OTHERMAT_COST_BG1/10000)) AS YS_OTHERMAT_COST_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt8 = new DataTable();
            dt8 = DBCallCommon.GetDTUsingSqlText(sqltext8);

            string sqltext9 = "select Convert(decimal(10,2), (YS_RG/10000)) AS YS_RG,Convert(decimal(10,2), (YS_RG_BG/10000)) AS YS_RG_BG, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt9 = new DataTable();
            dt9 = DBCallCommon.GetDTUsingSqlText(sqltext9);

            string sqltext10 = "select Convert(decimal(10,2), (YS_TEAM_CONTRACT1/10000)) AS YS_TEAM_CONTRACT1,Convert(decimal(10,2), (YS_TEAM_CONTRACT_BG1/10000)) AS YS_TEAM_CONTRACT_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt10 = new DataTable();
            dt10 = DBCallCommon.GetDTUsingSqlText(sqltext10);

            string sqltext11 = "select Convert(decimal(10,2), (YS_FAC_CONTRACT1/10000)) AS YS_FAC_CONTRACT1,Convert(decimal(10,2), (YS_FAC_CONTRACT_BG1/10000)) AS YS_FAC_CONTRACT_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt11 = new DataTable();
            dt11 = DBCallCommon.GetDTUsingSqlText(sqltext11);

            string sqltext12 = "select Convert(decimal(10,2), (YS_PRODUCT_OUT1/10000)) AS YS_PRODUCT_OUT1,Convert(decimal(10,2), (YS_PRODUCT_OUT_BG1/10000)) AS YS_PRODUCT_OUT_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt12 = new DataTable();
            dt12 = DBCallCommon.GetDTUsingSqlText(sqltext12);

            string sqltext13 = "select Convert(decimal(10,2), (YS_TRANS_COST1/10000)) AS YS_TRANS_COST1,Convert(decimal(10,2), (YS_TRANS_COST_BG1/10000)) AS YS_TRANS_COST_BG1, YEARMONTH from View_YS__BUDGET_STA where " + strstring1() + " order by YEARMONTH asc";

            DataTable dt13 = new DataTable();
            dt13 = DBCallCommon.GetDTUsingSqlText(sqltext13);

            if (dt0.Rows.Count > 0)
            {
                DataView dv0 = new DataView(dt0);
                Chart1.Series["预算毛利润"].Points.DataBindXY(dv0, "YEARMONTH", dv0, "YS_PROFIT_BG");
                Chart1.Series["实际毛利润"].Points.DataBindXY(dv0, "YEARMONTH", dv0, "YS_PROFIT");
                Chart1.Series["预算毛利润"].Label = "#VALY";
                Chart1.Series["实际毛利润"].Label = "#VALY";
                Chart1.Series["预算毛利润"].ToolTip = "#VALY";
                Chart1.Series["实际毛利润"].ToolTip = "#VALY";
            }
            Chart1.Series["预算毛利润"].ChartType = SeriesChartType.Column;
            Chart1.Series["实际毛利润"].ChartType = SeriesChartType.Column;

            if (dt1.Rows.Count > 0)
            {
                DataView dv1 = new DataView(dt1);
                Chart2.Series["毛利率预算值"].Points.DataBindXY(dv1, "YEARMONTH", dv1, "YS_PROFIT_RATE");
                Chart2.Series["毛利率实际值"].Points.DataBindXY(dv1, "YEARMONTH", dv1, "YS_PROFIT_RATE_BG");
                Chart2.Series["毛利率预算值"].Label = "#VALY" + "%";
                Chart2.Series["毛利率实际值"].Label = "#VALY" + "%";
                Chart2.Series["毛利率预算值"].ToolTip = "#VALY" + "%";
                Chart2.Series["毛利率实际值"].ToolTip = "#VALY" + "%";
            }
            Chart2.Series["毛利率预算值"].ChartType = SeriesChartType.Line;
            Chart2.Series["毛利率实际值"].ChartType = SeriesChartType.Line;

            if (dt2.Rows.Count > 0)
            {
                DataView dv2 = new DataView(dt2);
                Chart3.Series["材料费用预算总值"].Points.DataBindXY(dv2, "YEARMONTH", dv2, "YS_CL_BG");
                Chart3.Series["材料费用实际总值"].Points.DataBindXY(dv2, "YEARMONTH", dv2, "YS_CL");
                Chart3.Series["材料费用预算总值"].Label = "#VALY";
                Chart3.Series["材料费用实际总值"].Label = "#VALY";
                Chart3.Series["材料费用预算总值"].ToolTip = "#VALY";
                Chart3.Series["材料费用实际总值"].ToolTip = "#VALY";
            }
            Chart3.Series["材料费用预算总值"].ChartType = SeriesChartType.Column;
            Chart3.Series["材料费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt3.Rows.Count > 0)
            {
                DataView dv3 = new DataView(dt3);
                Chart4.Series["黑色金属费用预算总值"].Points.DataBindXY(dv3, "YEARMONTH", dv3, "YS_FERROUS_METAL_BG1");
                Chart4.Series["黑色金属费用实际总值"].Points.DataBindXY(dv3, "YEARMONTH", dv3, "YS_FERROUS_METAL1");
                Chart4.Series["黑色金属费用预算总值"].Label = "#VALY";
                Chart4.Series["黑色金属费用实际总值"].Label = "#VALY";
                Chart4.Series["黑色金属费用预算总值"].ToolTip = "#VALY";
                Chart4.Series["黑色金属费用实际总值"].ToolTip = "#VALY";
            }
            Chart4.Series["黑色金属费用预算总值"].ChartType = SeriesChartType.Column;
            Chart4.Series["黑色金属费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt4.Rows.Count > 0)
            {
                DataView dv4 = new DataView(dt4);
                Chart5.Series["外购件费用预算总值"].Points.DataBindXY(dv4, "YEARMONTH", dv4, "YS_PURCHASE_PART_BG1");
                Chart5.Series["外购件费用实际总值"].Points.DataBindXY(dv4, "YEARMONTH", dv4, "YS_PURCHASE_PART1");
                Chart5.Series["外购件费用预算总值"].Label = "#VALY";
                Chart5.Series["外购件费用实际总值"].Label = "#VALY";
                Chart5.Series["外购件费用预算总值"].ToolTip = "#VALY";
                Chart5.Series["外购件费用实际总值"].ToolTip = "#VALY";
            }
            Chart5.Series["外购件费用预算总值"].ChartType = SeriesChartType.Column;
            Chart5.Series["外购件费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt5.Rows.Count > 0)
            {
                DataView dv5 = new DataView(dt5);
                Chart6.Series["加工件费用预算总值"].Points.DataBindXY(dv5, "YEARMONTH", dv5, "YS_MACHINING_PART_BG1");
                Chart6.Series["加工件费用实际总值"].Points.DataBindXY(dv5, "YEARMONTH", dv5, "YS_MACHINING_PART1");
                Chart6.Series["加工件费用预算总值"].Label = "#VALY";
                Chart6.Series["加工件费用实际总值"].Label = "#VALY";
                Chart6.Series["加工件费用预算总值"].ToolTip = "#VALY";
                Chart6.Series["加工件费用实际总值"].ToolTip = "#VALY";
            }
            Chart6.Series["加工件费用预算总值"].ChartType = SeriesChartType.Column;
            Chart6.Series["加工件费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt6.Rows.Count > 0)
            {
                DataView dv6 = new DataView(dt6);
                Chart7.Series["油漆涂料费用预算总值"].Points.DataBindXY(dv6, "YEARMONTH", dv6, "YS_PAINT_COATING_BG1");
                Chart7.Series["油漆涂料费用实际总值"].Points.DataBindXY(dv6, "YEARMONTH", dv6, "YS_PAINT_COATING1");
                Chart7.Series["油漆涂料费用预算总值"].Label = "#VALY";
                Chart7.Series["油漆涂料费用实际总值"].Label = "#VALY";
                Chart7.Series["油漆涂料费用预算总值"].ToolTip = "#VALY";
                Chart7.Series["油漆涂料费用实际总值"].ToolTip = "#VALY";
            }
            Chart7.Series["油漆涂料费用预算总值"].ChartType = SeriesChartType.Column;
            Chart7.Series["油漆涂料费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt7.Rows.Count > 0)
            {
                DataView dv7 = new DataView(dt7);
                Chart8.Series["电气电料费用预算总值"].Points.DataBindXY(dv7, "YEARMONTH", dv7, "YS_ELECTRICAL_BG1");
                Chart8.Series["电气电料费用实际总值"].Points.DataBindXY(dv7, "YEARMONTH", dv7, "YS_ELECTRICAL1");
                Chart8.Series["电气电料费用预算总值"].Label = "#VALY";
                Chart8.Series["电气电料费用实际总值"].Label = "#VALY";
                Chart8.Series["电气电料费用预算总值"].ToolTip = "#VALY";
                Chart8.Series["电气电料费用实际总值"].ToolTip = "#VALY";
            }
            Chart8.Series["电气电料费用预算总值"].ChartType = SeriesChartType.Column;
            Chart8.Series["电气电料费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt8.Rows.Count > 0)
            {
                DataView dv8 = new DataView(dt8);
                Chart9.Series["其他材料费用预算总值"].Points.DataBindXY(dv8, "YEARMONTH", dv8, "YS_OTHERMAT_COST_BG1");
                Chart9.Series["其他材料费用实际总值"].Points.DataBindXY(dv8, "YEARMONTH", dv8, "YS_OTHERMAT_COST1");
                Chart9.Series["其他材料费用预算总值"].Label = "#VALY";
                Chart9.Series["其他材料费用实际总值"].Label = "#VALY";
                Chart9.Series["其他材料费用预算总值"].ToolTip = "#VALY";
                Chart9.Series["其他材料费用实际总值"].ToolTip = "#VALY";
            }
            Chart9.Series["其他材料费用预算总值"].ChartType = SeriesChartType.Column;
            Chart9.Series["其他材料费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt9.Rows.Count > 0)
            {
                DataView dv9 = new DataView(dt9);
                Chart10.Series["人工费用预算总值"].Points.DataBindXY(dv9, "YEARMONTH", dv9, "YS_RG_BG");
                Chart10.Series["人工费用实际总值"].Points.DataBindXY(dv9, "YEARMONTH", dv9, "YS_RG");
                Chart10.Series["人工费用预算总值"].Label = "#VALY";
                Chart10.Series["人工费用实际总值"].Label = "#VALY";
                Chart10.Series["人工费用预算总值"].ToolTip = "#VALY";
                Chart10.Series["人工费用实际总值"].ToolTip = "#VALY";
            }
            Chart10.Series["人工费用预算总值"].ChartType = SeriesChartType.Column;
            Chart10.Series["人工费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt10.Rows.Count > 0)
            {
                DataView dv10 = new DataView(dt10);
                Chart11.Series["直接人工费用预算总值"].Points.DataBindXY(dv10, "YEARMONTH", dv10, "YS_TEAM_CONTRACT_BG1");
                Chart11.Series["直接人工费用实际总值"].Points.DataBindXY(dv10, "YEARMONTH", dv10, "YS_TEAM_CONTRACT1");
                Chart11.Series["直接人工费用预算总值"].Label = "#VALY";
                Chart11.Series["直接人工费用实际总值"].Label = "#VALY";
                Chart11.Series["直接人工费用预算总值"].ToolTip = "#VALY";
                Chart11.Series["直接人工费用实际总值"].ToolTip = "#VALY";
            }
            Chart11.Series["直接人工费用预算总值"].ChartType = SeriesChartType.Column;
            Chart11.Series["直接人工费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt11.Rows.Count > 0)
            {
                DataView dv11 = new DataView(dt11);
                Chart12.Series["厂内分包费用预算总值"].Points.DataBindXY(dv11, "YEARMONTH", dv11, "YS_FAC_CONTRACT_BG1");
                Chart12.Series["厂内分包费用实际总值"].Points.DataBindXY(dv11, "YEARMONTH", dv11, "YS_FAC_CONTRACT1");
                Chart12.Series["厂内分包费用预算总值"].Label = "#VALY";
                Chart12.Series["厂内分包费用实际总值"].Label = "#VALY";
                Chart12.Series["厂内分包费用预算总值"].ToolTip = "#VALY";
                Chart12.Series["厂内分包费用实际总值"].ToolTip = "#VALY";
            }
            Chart12.Series["厂内分包费用预算总值"].ChartType = SeriesChartType.Column;
            Chart12.Series["厂内分包费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt12.Rows.Count > 0)
            {
                DataView dv12 = new DataView(dt12);
                Chart13.Series["生产外协费用预算总值"].Points.DataBindXY(dv12, "YEARMONTH", dv12, "YS_PRODUCT_OUT_BG1");
                Chart13.Series["生产外协费用实际总值"].Points.DataBindXY(dv12, "YEARMONTH", dv12, "YS_PRODUCT_OUT1");
                Chart13.Series["生产外协费用预算总值"].Label = "#VALY";
                Chart13.Series["生产外协费用实际总值"].Label = "#VALY";
                Chart13.Series["生产外协费用预算总值"].ToolTip = "#VALY";
                Chart13.Series["生产外协费用实际总值"].ToolTip = "#VALY";
            }
            Chart13.Series["生产外协费用预算总值"].ChartType = SeriesChartType.Column;
            Chart13.Series["生产外协费用实际总值"].ChartType = SeriesChartType.Column;

            if (dt13.Rows.Count > 0)
            {
                DataView dv13 = new DataView(dt13);
                Chart14.Series["发运费用预算总值"].Points.DataBindXY(dv13, "YEARMONTH", dv13, "YS_TRANS_COST_BG1");
                Chart14.Series["发运费用实际总值"].Points.DataBindXY(dv13, "YEARMONTH", dv13, "YS_TRANS_COST1");
                Chart14.Series["发运费用预算总值"].Label = "#VALY";
                Chart14.Series["发运费用实际总值"].Label = "#VALY";
                Chart14.Series["发运费用预算总值"].ToolTip = "#VALY";
                Chart14.Series["发运费用实际总值"].ToolTip = "#VALY";
            }
            Chart14.Series["发运费用预算总值"].ChartType = SeriesChartType.Column;
            Chart14.Series["发运费用实际总值"].ChartType = SeriesChartType.Column;
        
        }

        private string strstring1()
        {
            string sqlText = "1=1";
            if (yearmonthstart.Value.Trim() == "" && yearmonthend.Value.Trim() == "")
            {
                sqlText += " and YEARMONTH like '" + DateTime.Now.ToString("yyyy").Trim() + "-%'";
            }
            else
            {
                if (yearmonthstart.Value.Trim() != "" && yearmonthend.Value.Trim() != "")
                {
                    sqlText += " and YEARMONTH>='" + yearmonthstart.Value.Trim() + "' and YEARMONTH<='" + yearmonthend.Value.Trim() + "'";
                }
            }
            return sqlText;
        }

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
            //updatedata();
 
            drawingdata();
        }
    }
}
