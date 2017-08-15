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
        string tsaId;
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            { 
                tsaId = Request.QueryString["tsaId"].ToString();
                SetTSAInfo(tsaId);
                BindRepeaterSource(rpt_YS_FERROUS_METAL, pal_No_YS_FERROUS_METAL, tsaId, "01.07");//黑色金属
                BindRepeaterSource(rpt_YS_PURCHASE_PART, pan_No_YS_PURCHASE_PART, tsaId, "01.11");//外购件
                BindRepeaterSource(rpt_YS_MACHINING_PART, pan_No_YS_MACHINING_PART, tsaId, "01.08");//加工件
                BindRepeaterSource(rpt_YS_PAINT_COATING, pan_No_YS_PAINT_COATING, tsaId, "01.15");//油漆涂料
                BindRepeaterSource(rpt_YS_ELECTRICAL, pal_No_YS_ELECTRICAL, tsaId, "01.03");//电气电料            
                BindRepeaterSource(rpt_YS_OTHERMAT_COST, pal_No_YS_OTHERMAT_COST, tsaId);//其他材料
            }



        }

        /// <summary>
        /// 设置任务号的基本信息
        /// </summary>
        /// <param name="id">任务号</param>
        public void SetTSAInfo(string id)
        {
            string sql = @"select *,(YS_MATERIAL_COST+YS_LABOUR_COST+YS_TRANS_COST) AS YS_TOTALCOST_ALL,(YS_BUDGET_INCOME-YS_TOTALCOST_ALL) AS YS_PROFIT,YS_PROFIT/YS_TOTALCOST_ALL AS YS_PROFIT_RATE,
(YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_CASTING_FORGING_COST+YS_OTHERMAT_COST) AS materil_history_reference,
(YS_FERROUS_METAL_FB+YS_PURCHASE_PART_FB+YS_MACHINING_PART_FB+YS_PAINT_COATING_FB+YS_ELECTRICAL_FB+YS_CASTING_FORGING_COST_FB+YS_OTHERMAT_COST_FB) AS materil_dispart_reference,
YS_WEIGHT*YS_UNIT_LABOUR_COST_FB AS labour_dispart_reference
from YS_COST_BUDGET where YS_TSA_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            //预算基本信息            
            txt_YS_TSA_ID.Text = id;
            txt_YS_BUDGET_INCOME.Text = dt.Rows[0]["YS_BUDGET_INCOME"].ToString();//任务号预算收入
            txt_YS_WEIGHT.Text = dt.Rows[0]["YS_WEIGHT"].ToString();//任务号重量
            txt_YS_CONTRACT_NO.Text = dt.Rows[0]["YS_CONTRACT_NO"].ToString();//合同号
            txt_YS_PROJECTNAME.Text = dt.Rows[0]["YS_PROJECTNAME"].ToString();//项目名称
            txt_YS_ENGINEERNAME.Text = dt.Rows[0]["YS_ENGINEERNAME"].ToString();
            txt_YS_ADDNAME.Text = dt.Rows[0]["YS_ADDNAME"].ToString();//财务制单人
            txt_YS_ADDTIME.Text = dt.Rows[0]["YS_ADDTIME"].ToString();//提交时间
            txt_YS_NOTE.Text = dt.Rows[0]["YS_NOTE"].ToString();//备注

            //预算费用分配
            txt_YS_MATERIAL_COST.Text = dt.Rows[0]["YS_MATERIAL_COST"].ToString();//材料费
            txt_YS_LABOUR_COST.Text = dt.Rows[0]["YS_LABOUR_COST"].ToString();//人工费
            txt_YS_TRANS_COST.Text = dt.Rows[0]["YS_TRANS_COST"].ToString();//运费
            txt_YS_TOTALCOST_ALL.Text = dt.Rows[0]["YS_TOTALCOST_ALL"].ToString();//预算总额
            txt_YS_PROFIT.Text = dt.Rows[0]["YS_PROFIT"].ToString();//毛利润
            txt_YS_PROFIT_RATE.Text = dt.Rows[0]["YS_PROFIT_RATE"].ToString();//毛利率

            //材料费参考
            txt_YS_FERROUS_METAL.Text = dt.Rows[0]["YS_FERROUS_METAL"].ToString();
            txt_YS_PURCHASE_PART.Text = dt.Rows[0]["YS_PURCHASE_PART"].ToString();
            txt_YS_MACHINING_PART.Text = dt.Rows[0]["YS_MACHINING_PART"].ToString();
            txt_YS_PAINT_COATING.Text = dt.Rows[0]["YS_PAINT_COATING"].ToString();
            txt_YS_ELECTRICAL.Text = dt.Rows[0]["YS_ELECTRICAL"].ToString();
            txt_YS_CASTING_FORGING_COST.Text = dt.Rows[0]["YS_CASTING_FORGING_COST"].ToString();
            txt_YS_OTHERMAT_COST.Text = dt.Rows[0]["YS_OTHERMAT_COST"].ToString();
            txt_materil_history_reference.Text = dt.Rows[0]["materil_history_reference"].ToString();

            //部门反馈
            txt_YS_FERROUS_METAL_FB.Text = dt.Rows[0]["YS_FERROUS_METAL_FB"].ToString();
            txt_YS_PURCHASE_PART_FB.Text = dt.Rows[0]["YS_PURCHASE_PART_FB"].ToString();
            txt_YS_MACHINING_PART_FB.Text = dt.Rows[0]["YS_MACHINING_PART_FB"].ToString();
            txt_YS_PAINT_COATING_FB.Text = dt.Rows[0]["YS_PAINT_COATING_FB"].ToString();
            txt_YS_ELECTRICAL_FB.Text = dt.Rows[0]["YS_ELECTRICAL_FB"].ToString();
            txt_YS_CASTING_FORGING_COST_FB.Text = dt.Rows[0]["YS_CASTING_FORGING_COST_FB"].ToString();
            txt_YS_OTHERMAT_COST_FB.Text = dt.Rows[0]["YS_OTHERMAT_COST_FB"].ToString();
            txt_materil_dispart_reference.Text = dt.Rows[0]["materil_dispart_reference"].ToString();

            txt_YS_UNIT_LABOUR_COST_FB.Text = dt.Rows[0]["YS_UNIT_LABOUR_COST_FB"].ToString();
            txt_labour_dispart_reference.Text = dt.Rows[0]["labour_dispart_reference"].ToString();



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

        /// <summary>
        /// 绑定其他材料Repeater数据
        /// </summary>
        /// <param name="rpt">Repeater</param>
        /// <param name="id">任务号</param>
        /// <param name="condition">like或not like</param>
        /// <param name="code">物料编码前5位，(带.)</param>
        protected void BindRepeaterSource(Repeater rpt, Panel panel, string id)
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            string sql = string.Format("UPDATE dbo.YS_COST_BUDGET SET YS_NOTE='{0}' WHERE YS_TSA_ID='{1}'", txt_YS_NOTE.Text.Trim(), tsaId);
            if (DBCallCommon.ExeSqlTextGetInt(sql)>0)
            {
                Response.Write("<script>alert('保存成功')</script>");
            }
            else
            {
                Response.Write("<script>alert('保存失败')</script>");
            }
        }
    }
}
