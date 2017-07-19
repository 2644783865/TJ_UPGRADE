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
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_History_price_analysis : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            //CheckUser(ControlFinder);
        }
        protected void drop_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            showdetailprice();
        }
        private void showdetailprice()
        {
            if (Isnotzero())
            {
                Chartdatabind();
                Chart1.Series["价格数据"].ToolTip = "t \t= #VALX\nprice value \t= #VALY{C}";
                Chart1.Series["价格数据"].Label = "#VALY{C}";
                if (Chart1.Series["价格数据"].Points.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('暂无报价！');", true);
                }
                else
                {
                    DataPoint maxValuePoint = Chart1.Series["价格数据"].Points.FindMaxByValue();
                    DataPoint minValuePoint = Chart1.Series["价格数据"].Points.FindMinByValue();
                    if (drop_type.SelectedValue.ToString() == "0")
                    {
                        Chart1.Series["价格数据"].ChartType = SeriesChartType.Line;
                        Chart1.Series["价格数据"].MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Circle;
                        Chart1.Legends["Default"].Docking = Docking.Right;
                        Chart1.Legends["Default"].CustomItems[0].MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Cross;
                        Chart1.Legends["Default"].CustomItems[0].MarkerColor = Color.FromArgb(252, 180, 65);
                        Chart1.Legends["Default"].CustomItems[1].MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Triangle;
                        Chart1.Legends["Default"].CustomItems[1].MarkerColor = Color.FromArgb(224, 64, 10);
                        // Find point with maximum Y value and change color
                        maxValuePoint.MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Triangle;
                        maxValuePoint.Color = Color.FromArgb(224, 64, 10);
                        maxValuePoint.MarkerSize = 10;
                        minValuePoint.MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Cross;
                        minValuePoint.Color = Color.FromArgb(252, 180, 65);
                        minValuePoint.MarkerSize = 10;
                    }
                    if (drop_type.SelectedValue.ToString() == "1")
                    {
                        Chart1.Series["价格数据"].ChartType = SeriesChartType.FastLine;

                    }
                    if (drop_type.SelectedValue.ToString() == "2")
                    {
                        Chart1.Series["价格数据"].ChartType = SeriesChartType.Column;
                        Chart1.Legends["Default"].CustomItems[0].MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Square;
                        Chart1.Legends["Default"].CustomItems[0].MarkerColor = Color.FromArgb(252, 180, 65);
                        Chart1.Legends["Default"].CustomItems[1].MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Square;
                        Chart1.Legends["Default"].CustomItems[1].MarkerColor = Color.FromArgb(224, 64, 10);
                        // Find point with maximum Y value and change color
                        maxValuePoint.Color = Color.FromArgb(224, 64, 10);
                        // Find point with minimum Y value and change color
                        minValuePoint.Color = Color.FromArgb(252, 180, 65);

                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入物料代码！');", true);
            }
        }
        private bool Isnotzero()
        {
            bool b = true;
            if (MARID.Text.ToString().Replace(" ", "") == "")
            {
                b = false;
            }
            return b;
        }
        private void Chartdatabind()
        {
            string sq = MARID.Text.ToString().Replace(" ", "");
            string sqltext;
            if (Tb_time1.Text.ToString() == "" && Tb_time2.Text.ToString() =="")
                {
                    sqltext = "SELECT marid,marnm,margg,marcz,price,CONVERT(varchar(12) , irqdata, 102 ) as irqdata, from  View_TBPC_IQRCMPPRICE_RVW where marid ='" + sq + "' order by irqdata asc";
                
                }
                else
                {
                    if (Tb_time1.Text.ToString() == "")
                    {
                        sqltext = "SELECT marid,marnm,margg,marcz,price,CONVERT(varchar(12) , irqdata, 102 ) as irqdata from  View_TBPC_IQRCMPPRICE_RVW where marid ='" + sq + "' and irqdata <='" + Tb_time2.Text.ToString() + "' order by irqdata asc";

                    }
                    else
                    {
                        if (Tb_time2.Text.ToString() == "")
                        {

                            sqltext = "SELECT marid,marnm,margg,marcz,price,CONVERT(varchar(12) , irqdata, 102 ) as irqdata from  View_TBPC_IQRCMPPRICE_RVW where marid ='" + sq + "' and irqdata >='" + Tb_time1.Text.ToString() + "' order by irqdata asc";


                        }
                        else
                        {

                            sqltext = "SELECT marid,marnm,margg,marcz,price,CONVERT(varchar(12) , irqdata, 102 ) as irqdata  from  View_TBPC_IQRCMPPRICE_RVW where marid ='" + sq + "' and irqdata <='" + Tb_time2.Text.ToString() + "' and irqdata >='" + Tb_time1.Text.ToString() + "' order by irqdata asc";
                            
                        }
                    }
                }
            
        
            
            DataTable dt = new DataTable();
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = new DataView(dt);            
            Chart1.Series["价格数据"].Points.DataBindXY(dv, "irqdata", dv, "price");
        }
        protected void tb_marinfo_textchange(object sender, EventArgs e)
        {
            string marid = "";
            string sqltext = "";
            DataTable glotb = new DataTable();
            if (!(tb_marinfo.Text == "" || tb_marinfo.Text == DBNull.Value.ToString()))
            {
                if (tb_marinfo.Text.ToString().Contains("|"))
                {
                    marid = tb_marinfo.Text.Substring(0, tb_marinfo.Text.ToString().IndexOf("|"));
                    tb_marinfo.Text = marid;
                }
                sqltext = "SELECT ID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT FROM TBMA_MATERIAL WHERE ID='" + tb_marinfo.Text.Replace(" ", "") + "' OR HMCODE='" + tb_marinfo.Text.Replace(" ", "") + "' ORDER BY ID";
                //sqltext = "SELECT BZJ_ID,BZJ_NAME,BZJ_GUIGE,BZJ_UNIT FROM TBMA_BZJINFO WHERE (BZJ_ID = '01.01.000001')";
                glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (glotb.Rows.Count == 1)
                {
                    MARID.Text = glotb.Rows[0]["ID"].ToString();
                    MARNAME.Text = glotb.Rows[0]["MNAME"].ToString();
                    MARNORM.Text = glotb.Rows[0]["GUIGE"].ToString();
                    MARTERIAL.Text = glotb.Rows[0]["CAIZHI"].ToString();
                    GUOBIAO.Text = glotb.Rows[0]["GB"].ToString();
                    NUNIT.Text = glotb.Rows[0]["PURCUNIT"].ToString();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料为空，请重新输入！');", true);
                tb_marinfo.Focus();
            }
        }
       
        protected void Lkb_Click(object sender, EventArgs e)
        {
            showdetailprice();
        }
    }
}
