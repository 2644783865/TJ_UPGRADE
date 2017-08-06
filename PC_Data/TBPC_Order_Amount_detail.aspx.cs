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

namespace ZCZJ_DPF.PC_Data
{
    public partial class TBPC_Order_Amount_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bindtitle();
            BindData();
        }

        protected void bindtitle()
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
            string sql_amount = "select Convert(decimal(16,2),(YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_MACHINING_PART_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)*1.17) AS YS_AMOUNT_B,YS_MAR_AMOUNT AS YS_AMOUNT_0 FROM View_YS_COST_BUDGET_ORDER WHERE PCON_SCH='" + ContractNo + "'";
            System.Data.DataTable dt_amount = DBCallCommon.GetDTUsingSqlText(sql_amount);
            if (dt_amount.Rows.Count > 0)
            {
                double ys_amount_b = dt_amount.Rows[0][0].ToString() == "" ? 0 : Convert.ToDouble(dt_amount.Rows[0][0].ToString());
                double ys_amount_o = dt_amount.Rows[0][1].ToString() == "" ? 0 : Convert.ToDouble(dt_amount.Rows[0][1].ToString());
                double ys_amount_b_million = Math.Round(ys_amount_b / 10000, 1);
                double ys_amount_o_million = Math.Round(ys_amount_o / 10000, 1);
                lab_YS_AMOUNT_B.Text = ys_amount_b_million.ToString() + "万";
                lab_YS_AMOUNT_O.Text = ys_amount_o_million.ToString() + "万";
                if (ys_amount_o > 0.9 * ys_amount_b && ys_amount_o < ys_amount_b * 1.0)
                {
                    lab_YS_AMOUNT_O.BackColor = System.Drawing.Color.PeachPuff;
                }
                if (ys_amount_o > ys_amount_b * 1.0)
                {
                    lab_YS_AMOUNT_O.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }

        protected void BindData()
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
            string[] fathername = { "FERROUS_METAL", "PURCHASE_PART", "MACHINING_PART", "PAINT_COATING", "ELECTRICAL", "OTHERMAT_COST" };
            string[] mar_name = { "黑色金属", "外购件", "加工件", "油漆涂料", "电气电料", "其他材料费" };
            string sql_mar_amount = "select Convert(decimal(16,2), YS_FERROUS_METAL_BG*1.17) as YS_FERROUS_METAL_BG,YS_FERROUS_METAL," +
                "Convert(decimal(16,2), YS_PURCHASE_PART_BG*1.17) as YS_PURCHASE_PART_BG,YS_PURCHASE_PART," +
                "Convert(decimal(16,2), YS_MACHINING_PART_BG*1.17) as YS_MACHINING_PART_BG,YS_MACHINING_PART," +
                "Convert(decimal(16,2), YS_PAINT_COATING_BG*1.17) as YS_PAINT_COATING_BG,YS_PAINT_COATING," +
                "Convert(decimal(16,2), YS_ELECTRICAL_BG*1.17) as YS_ELECTRICAL_BG,YS_ELECTRICAL," +
                "Convert(decimal(16,2), YS_OTHERMAT_COST_BG*1.17) as YS_OTHERMAT_COST_BG,YS_OTHERMAT_COST," +
                "Convert(decimal(16,2),(YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_MACHINING_PART_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)*1.17) AS YS_MAR_AMOUNT_BG," +
                "(YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST) AS YS_MAR_AMOUNT," +
                "Convert(decimal(16,2), 100*(YS_FERROUS_METAL/(YS_FERROUS_METAL_BG*1.17+1))) as YS_FERROUS_METAL_BG_percent," +
                "Convert(decimal(16,2), 100*(YS_PURCHASE_PART/(YS_PURCHASE_PART_BG*1.17+1))) as YS_PURCHASE_PART_BG_percent," +
                "Convert(decimal(16,2), 100*(YS_MACHINING_PART/(YS_MACHINING_PART_BG*1.17+1))) as YS_MACHINING_PART_BG_percent," +
                "Convert(decimal(16,2), 100*(YS_PAINT_COATING/(YS_PAINT_COATING_BG*1.17+1))) as YS_PAINT_COATING_BG_percent," +
                "Convert(decimal(16,2), 100*(YS_ELECTRICAL/(YS_ELECTRICAL_BG*1.17+1))) as YS_ELECTRICAL_BG_percent," +
                "Convert(decimal(16,2), 100*(YS_OTHERMAT_COST/(YS_OTHERMAT_COST_BG*1.17+1))) as YS_OTHERMAT_COST_BG_percent," +
                "Convert(decimal(16,2), 100*((YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST)/((YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_MACHINING_PART_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)*1.17+1))) from View_YS_COST_BUDGET_ORDER where PCON_SCH='" + ContractNo + "'";
            System.Data.DataTable dt_mar_amount = DBCallCommon.GetDTUsingSqlText(sql_mar_amount);
            string CONTRACT_NO = ed.EncryptText(ContractNo);
            CreateNewRow(7);
            int i = 0;
            foreach (RepeaterItem Item in tbpc_otherpurbillRepeater.Items)
            {
                switch (i)
                {
                    case 0:
                        ((Label)Item.FindControl("lab_YS_NAME")).Text = "黑色金属";
                        ((Label)Item.FindControl("lab_YS_MAR_BG")).Text = dt_mar_amount.Rows[0][0].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR")).Text = dt_mar_amount.Rows[0][1].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR_Percent")).Text = dt_mar_amount.Rows[0][14].ToString() + "%";
                        ((HyperLink)Item.FindControl("Hpl_detail")).NavigateUrl = "../YS_Data/YS_Cost_Budget_O_M_Detail.aspx?ContractNo=" + CONTRACT_NO + "&FatherCode=" + ed.EncryptText("FERROUS_METAL") + "";
                        break;
                    case 1:
                        ((Label)Item.FindControl("lab_YS_NAME")).Text = "外购件";
                        ((Label)Item.FindControl("lab_YS_MAR_BG")).Text = dt_mar_amount.Rows[0][2].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR")).Text = dt_mar_amount.Rows[0][3].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR_Percent")).Text = dt_mar_amount.Rows[0][15].ToString() + "%";
                        ((HyperLink)Item.FindControl("Hpl_detail")).NavigateUrl = "../YS_Data/YS_Cost_Budget_O_M_Detail.aspx?ContractNo=" + CONTRACT_NO + "&FatherCode=" + ed.EncryptText("PURCHASE_PART") + "";
                        break;
                    case 2:
                        ((Label)Item.FindControl("lab_YS_NAME")).Text = "加工件";
                        ((Label)Item.FindControl("lab_YS_MAR_BG")).Text = dt_mar_amount.Rows[0][4].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR")).Text = dt_mar_amount.Rows[0][5].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR_Percent")).Text = dt_mar_amount.Rows[0][16].ToString() + "%";
                        ((HyperLink)Item.FindControl("Hpl_detail")).NavigateUrl = "../YS_Data/YS_Cost_Budget_O_M_Detail.aspx?ContractNo=" + CONTRACT_NO + "&FatherCode=" + ed.EncryptText("MACHINING_PART") + "";
                        break;
                    case 3:
                        ((Label)Item.FindControl("lab_YS_NAME")).Text = "油漆涂料";
                        ((Label)Item.FindControl("lab_YS_MAR_BG")).Text = dt_mar_amount.Rows[0][6].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR")).Text = dt_mar_amount.Rows[0][7].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR_Percent")).Text = dt_mar_amount.Rows[0][17].ToString() + "%";
                        ((HyperLink)Item.FindControl("Hpl_detail")).NavigateUrl = "../YS_Data/YS_Cost_Budget_O_M_Detail.aspx?ContractNo=" + CONTRACT_NO + "&FatherCode=" + ed.EncryptText("PAINT_COATING") + "";
                        break;
                    case 4:
                        ((Label)Item.FindControl("lab_YS_NAME")).Text = "电气电料";
                        ((Label)Item.FindControl("lab_YS_MAR_BG")).Text = dt_mar_amount.Rows[0][8].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR")).Text = dt_mar_amount.Rows[0][9].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR_Percent")).Text = dt_mar_amount.Rows[0][18].ToString() + "%";
                        ((HyperLink)Item.FindControl("Hpl_detail")).NavigateUrl = "../YS_Data/YS_Cost_Budget_O_M_Detail.aspx?ContractNo=" + CONTRACT_NO + "&FatherCode=" + ed.EncryptText("ELECTRICAL") + "";
                        break;
                    case 5:
                        ((Label)Item.FindControl("lab_YS_NAME")).Text = "其他材料费";
                        ((Label)Item.FindControl("lab_YS_MAR_BG")).Text = dt_mar_amount.Rows[0][10].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR")).Text = dt_mar_amount.Rows[0][11].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR_Percent")).Text = dt_mar_amount.Rows[0][19].ToString() + "%";
                        ((HyperLink)Item.FindControl("Hpl_detail")).NavigateUrl = "../YS_Data/YS_Cost_Budget_O_M_Detail.aspx?ContractNo=" + CONTRACT_NO + "&FatherCode=" + ed.EncryptText("OTHERMAT_COST") + "";
                        break;
                    case 6:
                        ((Label)Item.FindControl("lab_YS_NAME")).Text = "合计";
                        ((Label)Item.FindControl("lab_YS_MAR_BG")).Text = dt_mar_amount.Rows[0][12].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR")).Text = dt_mar_amount.Rows[0][13].ToString();
                        ((Label)Item.FindControl("lab_YS_MAR_Percent")).Text = dt_mar_amount.Rows[0][20].ToString() + "%";
                        ((HyperLink)Item.FindControl("Hpl_detail")).NavigateUrl = "";
                        ((Label)Item.FindControl("check_look")).Text = "";
                        ((Image)Item.FindControl("Image1")).Visible = false;
                        break;
                    default: break;
                }
                i++;
            }
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("YS_NAME");
            dt.Columns.Add("YS_MAR_BG");
            dt.Columns.Add("YS_MAR");
            for (int i = 0; i < tbpc_otherpurbillRepeater.Items.Count; i++)
            {
                RepeaterItem Reitem = tbpc_otherpurbillRepeater.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)Reitem.FindControl("YS_NAME")).Text;
                newRow[1] = ((TextBox)Reitem.FindControl("YS_MAR_BG")).Text;
                newRow[2] = ((TextBox)Reitem.FindControl("YS_MAR")).Text;

                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void CreateNewRow(int num)
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.tbpc_otherpurbillRepeater.DataSource = dt;
            this.tbpc_otherpurbillRepeater.DataBind();
        }


    }
}
