using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Budget_Add_Detail : System.Web.UI.Page
    {
        int shengChan, caiGou, caiWu, jinDu, shenHe, yiJi, erJi;
        protected void Page_Load(object sender, EventArgs e)
        {
            //头部任务号相关信息
            string tsaId = Request.QueryString["tsaId"].ToString();
            SetTSAInfo(tsaId);
            BindRepeaterSource(rpt_YS_FERROUS_METAL,pal_No_YS_FERROUS_METAL, tsaId,  "01.07");//黑色金属
            BindRepeaterSource(rpt_YS_PURCHASE_PART, pan_No_YS_PURCHASE_PART,tsaId, "01.11");//外购件
            BindRepeaterSource(rpt_YS_MACHINING_PART,pan_No_YS_MACHINING_PART, tsaId,  "01.08");//加工件
            BindRepeaterSource(rpt_YS_PAINT_COATING,pan_No_YS_PAINT_COATING, tsaId,"01.15");//油漆涂料
            BindRepeaterSource(rpt_YS_ELECTRICAL,pal_No_YS_ELECTRICAL, tsaId, "01.03");//电气电料            
            BindRepeaterSource(rpt_YS_OTHERMAT_COST,pal_No_YS_OTHERMAT_COST, tsaId);//其他材料

        }

        /// <summary>
        /// 设置任务号的基本信息
        /// </summary>
        /// <param name="id">任务号</param>
        public void SetTSAInfo(string id)
        {
            txt_YS_TSA_ID.Text = id;
            string sql = "select * from YS_COST_BUDGET where YS_TSA_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            txt_YS_CONTRACT_NO.Text = dt.Rows[0]["YS_CONTRACT_NO"].ToString();
            txt_YS_PROJECTNAME.Text = dt.Rows[0]["YS_PROJECTNAME"].ToString();
            txt_YS_TRANS_COST.Text = dt.Rows[0]["YS_TRANS_COST"].ToString();

            //获取生产、采购、财务、编制进度、审核、一级审核、二级审核的状态
            shengChan = int.Parse(dt.Rows[0]["YS_SHENGCHAN"].ToString());
            caiGou = int.Parse(dt.Rows[0]["YS_CAIGOU"].ToString());
            caiWu = int.Parse(dt.Rows[0]["YS_CAIWU"].ToString());
            jinDu = int.Parse(dt.Rows[0]["YS_STATE"].ToString());
            shenHe = int.Parse(dt.Rows[0]["YS_REVSTATE"].ToString());
            yiJi = int.Parse(dt.Rows[0]["YS_FIRST_REVSTATE"].ToString());
            erJi = int.Parse(dt.Rows[0]["YS_SECOND_REVSTATE"].ToString());            
        }

        /// <summary>
        /// 绑定Repeater数据
        /// </summary>
        /// <param name="rpt">Repeater</param>
        /// <param name="id">任务号</param>       
        /// <param name="code">物料编码前5位，(带.)</param>
        protected void BindRepeaterSource(Repeater rpt, Panel panel, string id, string code)
        {
            string sql1 = String.Format(@"select YS_CODE ,YS_NAME ,YS_Union_Amount ,YS_Average_Price,YS_Average_Price_FB from  YS_COST_BUDGET_DETAIL 
where YS_TSA_ID='{0}' AND YS_CODE LIKE '{1}%' ORDER BY YS_CODE", id, code);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt.Rows.Count!=0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.Visible = false;
                panel.Visible = true;
            }
            
        }

        /// <summary>
        /// 绑定其他材料Repeater数据
        /// </summary>
        /// <param name="rpt">Repeater</param>
        /// <param name="id">任务号</param>
        /// <param name="condition">like或not like</param>
        /// <param name="code">物料编码前5位，(带.)</param>
        protected void BindRepeaterSource(Repeater rpt,Panel panel,string id)
        {
            string sql = String.Format(@"select YS_CODE ,YS_NAME ,YS_Union_Amount ,YS_Average_Price,YS_Average_Price_FB from  YS_COST_BUDGET_DETAIL 
where YS_TSA_ID='{0}' AND YS_CODE NOT LIKE '01.07%' AND YS_CODE NOT LIKE '01.11%' AND YS_CODE NOT LIKE '01.08%' AND YS_CODE NOT LIKE '01.15%' AND YS_CODE NOT LIKE '01.03%' ORDER BY YS_CODE", id);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count != 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.Visible = false;
                panel.Visible = true;
            }
        }






        public string GetProduct(string n1, string n2)
        {
            return (Convert.ToDouble(n1) * Convert.ToDouble(n2)).ToString("0.0000");
        }
    }
}
