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

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Budget_O_M_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTitle();
            RorY();
        }

        protected void BindTitle()
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
            string FatherCode = ed.DecryptText(Request.QueryString["FatherCode"].ToString());
            string sql_title = "select FATHER_NAME from YS_COST_BUDGET_NAME where FATHER_CODE='" + FatherCode + "' ";
            System.Data.DataTable dt_title = DBCallCommon.GetDTUsingSqlText(sql_title);
            if (dt_title.Rows.Count > 0)
            {
                lbl_fathername.Text = dt_title.Rows[0][0].ToString().Trim();   //绑定标题
            }

            string sql_total_BG = "select YS_" + FatherCode + "*1.17 from YS_COST_BUDGET where YS_TSA_ID='" + ContractNo + "'";

            //获取预算总费用
            System.Data.DataTable dt_total_BG = DBCallCommon.GetDTUsingSqlText(sql_total_BG);
            if (dt_total_BG.Rows.Count > 0)
            {
                string total_BG = dt_total_BG.Rows[0][0].ToString();
                float total_BG_f = (total_BG == "" ? 0 : Convert.ToSingle(total_BG));
                lbl_total_BG.Text = total_BG_f.ToString("N2");
            }
            BindData(ContractNo, FatherCode);
        }

        protected void BindData(string ContractNo, string FatherCode)
        {
            switch (FatherCode)
            {
                //case "OUT_LAB_MAR": this.Bind_OUT(ContractNo, FatherCode); break;
                case "FERROUS_METAL":
                case "PURCHASE_PART":
                case "MACHINING_PART": this.Bind_MAR(ContractNo, FatherCode); break;
                case "OTHERMAT_COST": this.Bind_OTHER_MAR(ContractNo, FatherCode); break;
                case "TEAM_CONTRACT":
                case "FAC_CONTRACT":
                case "MANU_COST":
                case "SELL_COST":
                case "MANAGE_COST":
                case "Taxes_Cost":
                case "PAINT_COATING":
                case "ELECTRICAL":
                case "PRODUCT_OUT": this.Bind_Paint_Elec_Pro(ContractNo, FatherCode); break;
                default: break;
            }
        }

        protected void Bind_OUT(string ContractNo, string FatherCode)
        {
            string sql_OUT_LAB_MAR = "select  YS_CODE,YS_NAME,YS_MONEY*1.17 as YS_MONEY_BG,YS_Union_Amount as YS_Union_Amount_BG," +
                   "YS_Average_Price as YS_Average_Price_BG from YS_COST_BUDGET_DETAIL where YS_TSA_ID='" + ContractNo + "' and YS_FATHER='OUT_LAB_MAR'";
            System.Data.DataTable dt_OUT_LAB_MAR = DBCallCommon.GetDTUsingSqlText(sql_OUT_LAB_MAR);
            dt_OUT_LAB_MAR.Columns.Add("YS_MONEY");
            dt_OUT_LAB_MAR.Columns.Add("YS_Union_Amount");
            dt_OUT_LAB_MAR.Columns.Add("YS_Average_Price");

            //获取该合同号下的所有生产制号
            string sql_ENGID = "select PCON_SCH from View_YS_CONTRACT where PCON_BCODE='" + ContractNo + "'";
            System.Data.DataTable dt_ENGID = DBCallCommon.GetDTUsingSqlText(sql_ENGID);
            string ENGID = "";
            if (dt_ENGID.Rows.Count > 0)
            {
                ENGID = dt_ENGID.Rows[0]["PCON_SCH"].ToString();
            }

            double total = 0;
            string sql_total = "select YS_" + FatherCode + " from YS_COST_BUDGET_ORDER where PCON_SCH='" + ContractNo + "'";
            System.Data.DataTable dt_total = DBCallCommon.GetDTUsingSqlText(sql_total);
            if (dt_total.Rows.Count > 0)
            {
                total = (dt_total.Rows[0][0].ToString() == "" ? 0 : double.Parse(dt_total.Rows[0][0].ToString()));  //总金额      
            }
            lbl_total.Text = total.ToString("N2");

            string sql_money_real = "";
            sql_money_real = "select SUM(WW_FINALJE) AS YS_MONEY,SUM(WW_TOTALWEIGHT) AS YS_Union_Amount,Convert(decimal(16,2),SUM(WW_FINALJE)/SUM(WW_TOTALWEIGHT)) as YS_Average_Price from View_WW_BJ where CHARINDEX([WW_SCZH] + ';', '" + ENGID + "')>0";
            for (int i = 0; i < dt_OUT_LAB_MAR.Rows.Count; i++)
            {
                double money_R = 0;
                double num_R = 0;
                double price_R = 0;
                if (dt_OUT_LAB_MAR.Rows[i]["YS_NAME"].ToString() == "包工包料外协")
                {
                    sql_money_real += " and WW_STATE='包工包料外协'";
                }
                if (dt_OUT_LAB_MAR.Rows[i]["YS_NAME"].ToString() == "带料外协")
                {
                    sql_money_real += " and WW_STATE='带料外协'";
                }
                System.Data.DataTable dt_money_real = DBCallCommon.GetDTUsingSqlText(sql_money_real);
                if (dt_money_real.Rows.Count > 0)
                {
                    money_R = (dt_money_real.Rows[0]["YS_MONEY"].ToString() == "" ? 0 : double.Parse(dt_money_real.Rows[0]["YS_MONEY"].ToString()));
                    num_R = (dt_money_real.Rows[0]["YS_Union_Amount"].ToString() == "" ? 0 : double.Parse(dt_money_real.Rows[0]["YS_Union_Amount"].ToString()));
                    price_R = (dt_money_real.Rows[0]["YS_Average_Price"].ToString() == "" ? 0 : double.Parse(dt_money_real.Rows[0]["YS_Average_Price"].ToString()));
                }
                dt_OUT_LAB_MAR.Rows[i]["YS_Union_Amount"] = num_R.ToString("N3");
                dt_OUT_LAB_MAR.Rows[i]["YS_Average_Price"] = price_R.ToString("F3");
                dt_OUT_LAB_MAR.Rows[i]["YS_MONEY"] = money_R.ToString("F3");
            }
            GridView1.DataSource = dt_OUT_LAB_MAR;
            GridView1.DataBind();
        }

        protected void Bind_MAR(string ContractNo, string FatherCode)
        {
            string fatherid = "";
            string sql_MAR = "select YS_CODE,YS_NAME,YS_MONEY*1.17 as YS_MONEY_BG,YS_Union_Amount as YS_Union_Amount_BG," +
                "YS_Average_Price as YS_Average_Price_BG from YS_COST_BUDGET_DETAIL where YS_TSA_ID='" + ContractNo + "'" +
                " and YS_FATHER='" + FatherCode + "' ";
            System.Data.DataTable dt_MAR = DBCallCommon.GetDTUsingSqlText(sql_MAR);
            dt_MAR.Columns.Add("YS_Union_Amount");
            dt_MAR.Columns.Add("YS_Average_Price");
            dt_MAR.Columns.Add("YS_MONEY");

            double total = 0;   //总金额
            string sql_total = "select YS_" + FatherCode + " from View_YS_COST_BUDGET_ORDER where PCON_SCH='" + ContractNo + "'";
            System.Data.DataTable dt_total = DBCallCommon.GetDTUsingSqlText(sql_total);
            if (dt_total.Rows.Count > 0)
            {
                total = (dt_total.Rows[0][0].ToString() == "" ? 0 : double.Parse(dt_total.Rows[0][0].ToString()));  //总金额      
            }
            lbl_total.Text = total.ToString("N2");

            #region 黑色金属
            if (FatherCode == "FERROUS_METAL")
            {
                fatherid = "01.07";
                string sql_money = "select sum(ctamount) as Amount,sum(zxnum) as RealNumber, sum(ctamount)/sum(zxnum) as UnitPrice from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where marid like'" + fatherid + "%' and engid='" + ContractNo + "'";
                for (int j = 0; j < dt_MAR.Rows.Count; j++)
                {
                    string sql = "";
                    if (dt_MAR.Rows[j]["YS_NAME"].ToString() == "轨道系统")
                    {
                        sql = sql_money + " and marnm like '%轨道%'";
                    }
                    else if (dt_MAR.Rows[j]["YS_NAME"].ToString() == "定尺板")
                    {
                        sql = sql_money + " and PO_MASHAPE='定尺板'";
                    }
                    else if (dt_MAR.Rows[j]["YS_NAME"].ToString() == "普通材料")
                    {
                        sql = sql_money + " and charindex('轨道',marnm)=0  and PO_MASHAPE!='定尺板'";
                    }
                    double money = 0;
                    double num = 0;
                    double price = 0;
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        money = (dt.Rows[0]["Amount"].ToString() == "" ? 0 : double.Parse(dt.Rows[0]["Amount"].ToString()));
                        num = (dt.Rows[0]["RealNumber"].ToString() == "" ? 0 : double.Parse(dt.Rows[0]["RealNumber"].ToString()));
                        price = (dt.Rows[0]["UnitPrice"].ToString() == "" ? 0 : double.Parse(dt.Rows[0]["UnitPrice"].ToString()));
                    }

                    dt_MAR.Rows[j]["YS_Union_Amount"] = num.ToString("N3");
                    dt_MAR.Rows[j]["YS_Average_Price"] = price.ToString("F3");
                    dt_MAR.Rows[j]["YS_MONEY"] = money.ToString("F3");
                }
            }
            #endregion
            #region 外购件，加工件
            else if (FatherCode == "PURCHASE_PART" || FatherCode == "MACHINING_PART")
            {
                if (FatherCode == "PURCHASE_PART")
                {
                    fatherid = "01.11";
                }
                else
                {
                    fatherid = "01.08";
                }
                string sql_money = "select sum(ctamount) as Amount,sum(zxnum) as RealNumber," +
                    " sum(ctamount)/sum(zxnum) as UnitPrice from View_TBPC_PURORDERDETAIL_PLAN_TOTAL " +
                    "where engid='" + ContractNo + "' and marid like'" + fatherid + "%'";

                //获取明细物料费用
                double main_mar = 0;
                for (int j = 0; j < dt_MAR.Rows.Count; j++)
                {
                    if (dt_MAR.Rows[j]["YS_CODE"].ToString() != "other")
                    {
                        string sql = "";
                        sql += sql_money + " and charindex('" + dt_MAR.Rows[j]["YS_NAME"].ToString() + "',marnm)>0 ";

                        ////判断预算物料表是否有包含此名称的材料
                        //string sql_contain = "select YS_Product_Name from TBBD_Product_type where YS_Product_Tag='1' " +
                        //    "and charindex('-',YS_Product_Code)>0 and YS_Product_Name like '%" + dt_MAR.Rows[j]["YS_NAME"].ToString() + "%' " +
                        //    "and YS_Product_Name!='" + dt_MAR.Rows[j]["YS_NAME"].ToString() + "'";
                        //DataTable dt_contain = DBCallCommon.GetDTUsingSqlText(sql_contain);
                        //for (int s = 0; s < dt_contain.Rows.Count; s++)
                        //{
                        //    sql += " and charindex('" + dt_contain.Rows[s]["YS_Product_Name"] + "',marnm)=0";
                        //}
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                        double money = 0;
                        double num = 0;
                        double price = 0;
                        if (dt.Rows.Count > 0)
                        {
                            money = (dt.Rows[0]["Amount"].ToString() == "" ? 0 : double.Parse(dt.Rows[0]["Amount"].ToString()));
                            num = (dt.Rows[0]["RealNumber"].ToString() == "" ? 0 : double.Parse(dt.Rows[0]["RealNumber"].ToString()));
                            price = (dt.Rows[0]["UnitPrice"].ToString() == "" ? 0 : double.Parse(dt.Rows[0]["UnitPrice"].ToString()));
                        }
                        dt_MAR.Rows[j]["YS_Union_Amount"] = num.ToString("N3");
                        dt_MAR.Rows[j]["YS_Average_Price"] = price.ToString("F3");
                        dt_MAR.Rows[j]["YS_MONEY"] = money.ToString("F3");
                        main_mar += money;
                    }
                }
                double other = total - main_mar;
                DataRow[] dr = dt_MAR.Select("YS_CODE='other'");   //其它费用
                for (int i = 0; i < dr.Length; i++)
                {
                    dr[i]["YS_MONEY"] = other.ToString("F3");
                }
            }

            GridView1.DataSource = dt_MAR;
            GridView1.DataBind();
            #endregion
        }

        protected void Bind_OTHER_MAR(string ContractNo, string FatherCode)
        {
            double total = 0;   //总金额
            string sql_total = "select YS_" + FatherCode + " from YS_COST_BUDGET_ORDER where PCON_SCH='" + ContractNo + "'";
            System.Data.DataTable dt_total = DBCallCommon.GetDTUsingSqlText(sql_total);
            if (dt_total.Rows.Count > 0)
            {
                total = (dt_total.Rows[0][0].ToString() == "" ? 0 : double.Parse(dt_total.Rows[0][0].ToString()));  //总金额      
            }
            lbl_total.Text = total.ToString("N2");

            string sql_OtherMar = "select YS_CODE,YS_NAME,YS_MONEY*1.17 as YS_MONEY_BG,YS_Union_Amount as YS_Union_Amount_BG," +
                "YS_Average_Price as YS_Average_Price_BG from YS_COST_BUDGET_DETAIL where YS_TSA_ID='" + ContractNo + "'" +
                " and YS_FATHER='" + FatherCode + "'";
            System.Data.DataTable dt_OtherMar = DBCallCommon.GetDTUsingSqlText(sql_OtherMar);
            string str_other = "";
            string str_other_product = "";
            for (int i = 0; i < dt_OtherMar.Rows.Count; i++)
            {
                str_other_product += " and charindex('" + dt_OtherMar.Rows[i]["YS_CODE"] + "',YS_Product_Code)=0";
                str_other += " and charindex('" + dt_OtherMar.Rows[i]["YS_CODE"] + "',marid)=0";
            }

            dt_OtherMar.Columns.Add("YS_Union_Amount");
            dt_OtherMar.Columns.Add("YS_Average_Price");
            dt_OtherMar.Columns.Add("YS_MONEY");

            for (int j = 0; j < dt_OtherMar.Rows.Count; j++)
            {
                string sql_code = "select sum(ctamount) as Amount from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where marid like '" + dt_OtherMar.Rows[j]["YS_CODE"] + "%' and engid='" + ContractNo + "'";
                System.Data.DataTable dt_code = DBCallCommon.GetDTUsingSqlText(sql_code);
                double money = 0;
                if (dt_code.Rows.Count > 0)
                {
                    money = (dt_code.Rows[0]["Amount"].ToString() == "" ? 0 : double.Parse(dt_code.Rows[0]["Amount"].ToString()));
                }
                dt_OtherMar.Rows[j]["YS_MONEY"] = money.ToString("F3");
            }
            string sql_other_type = "select YS_Product_Code from TBBD_Product_type where YS_Product_FatherCode='Other'";
            sql_other_type += str_other_product;
            System.Data.DataTable dt_other_type = DBCallCommon.GetDTUsingSqlText(sql_other_type);
            string sql_total_other = "select sum(ctamount) as Amount from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where engid='" + ContractNo + "' and charindex(substring(marid,1,5),'01.07.01.08.01.11.01.15.01.18')=0";
            sql_total_other += str_other;
            for (int i = 0; i < dt_other_type.Rows.Count; i++)
            {
                if (i == 0)
                {
                    sql_total_other += " and ( charindex('" + dt_other_type.Rows[i]["YS_Product_Code"] + "',marid)>0";
                }
                else if (i == (dt_other_type.Rows.Count - 1))
                {
                    sql_total_other += " or charindex('" + dt_other_type.Rows[i]["YS_Product_Code"] + "',marid)>0 )";
                }
                else
                {
                    sql_total_other += " or charindex('" + dt_other_type.Rows[i]["YS_Product_Code"] + "',marid)>0";
                }
            }
            System.Data.DataTable dt_total_other = DBCallCommon.GetDTUsingSqlText(sql_total_other);

            double other = (dt_total_other.Rows[0]["Amount"].ToString() == "" ? 0 : double.Parse(dt_total_other.Rows[0]["Amount"].ToString()));
            DataRow[] dr = dt_OtherMar.Select("YS_CODE='other'");   //其它费用
            for (int i = 0; i < dr.Length; i++)
            {
                dr[i]["YS_MONEY"] = other.ToString("F3");
            }

            GridView1.DataSource = dt_OtherMar;
            GridView1.DataBind();
            GridView1.Columns[3].Visible = false;    //单价数量隐藏
            GridView1.Columns[4].Visible = false;
            GridView1.Columns[6].Visible = false;    //单价数量隐藏
            GridView1.Columns[7].Visible = false;
        }

        protected void Bind_Paint_Elec_Pro(string ContractNo, string FatherCode)
        {
            //获取预算信息、创建datatable
            string sql_Tol = "select  YS_CODE,YS_NAME,YS_MONEY*1.17 as YS_MONEY_BG,YS_Union_Amount as YS_Union_Amount_BG,YS_Average_Price as YS_Average_Price_BG from YS_COST_BUDGET_DETAIL where YS_TSA_ID='" + ContractNo + "'and YS_FATHER='" + FatherCode + "'";
            System.Data.DataTable dt_Tol = DBCallCommon.GetDTUsingSqlText(sql_Tol);
            dt_Tol.Columns.Add("YS_Union_Amount");
            dt_Tol.Columns.Add("YS_Average_Price");
            dt_Tol.Columns.Add("YS_MONEY");

            double total = 0;   //总金额
            string sql_total = "select YS_" + FatherCode + " from YS_COST_BUDGET_ORDER where PCON_SCH='" + ContractNo + "'";
            System.Data.DataTable dt_total = DBCallCommon.GetDTUsingSqlText(sql_total);
            if (dt_total.Rows.Count > 0)
            {
                total = (dt_total.Rows[0][0].ToString() == "" ? 0 : double.Parse(dt_total.Rows[0][0].ToString()));  //总金额      
            }
            dt_Tol.Rows[0]["YS_MONEY"] = total.ToString("F3");
            lbl_total.Text = total.ToString("N2");

            GridView1.DataSource = dt_Tol;
            GridView1.DataBind();
            GridView1.Columns[3].Visible = false;
            GridView1.Columns[4].Visible = false;
            GridView1.Columns[6].Visible = false;
            GridView1.Columns[7].Visible = false;
            if (FatherCode == "PRODUCT_OUT")
            {
                GridView1.Columns[1].Visible = false;
            }
        }

        protected void RorY()
        {
            string budget = lbl_total_BG.Text.ToString();
            string real = lbl_total.Text.ToString();
            float budget_f = (budget == "" ? 0 : Convert.ToSingle(budget));
            float real_f = (real == "" ? 0 : Convert.ToSingle(real));

            if (real_f > budget_f * 0.9 && real_f < budget_f)         //超过预算90%
            {
                lbl_total.BackColor = System.Drawing.Color.LightSalmon;
            }
            else if (real_f > budget_f)         //超过预算
            {
                lbl_total.BackColor = System.Drawing.Color.Yellow;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindTitle();
        }

        protected void GridView1_onrowdatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string budget = e.Row.Cells[5].Text.ToString().Trim();
                float budget_num = (budget == "&nbsp;" ? 0 : Convert.ToSingle(budget));

                string real = e.Row.Cells[8].Text.ToString().Trim();
                float real_num = (real == "&nbsp;" ? 0 : Convert.ToSingle(real));

                if (real_num > budget_num * 0.9 && real_num < budget_num)         //超过预算90%
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.PeachPuff;
                }
                else if (real_num > budget_num)        //超过预算
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                }

                string ContractNo = Request.QueryString["ContractNo"].ToString();
                string FatherCode = Request.QueryString["FatherCode"].ToString();
                Encrypt_Decrypt ed = new Encrypt_Decrypt();
                string FatherCode1 = ed.DecryptText(FatherCode);
                string mar_code = "";
                switch (FatherCode1)
                {
                    case "FERROUS_METAL": mar_code = "01.07"; break;
                    case "PURCHASE_PART": mar_code = "01.11"; break;
                    case "MACHINING_PART": mar_code = "01.08"; break;
                    case "PAINT_COATING": mar_code = "01.15"; break;
                    case "ELECTRICAL": mar_code = "01.18"; break;
                    case "OTHERMAT_COST": mar_code = "other"; break;
                    default: break;
                }
                string mar_name = e.Row.Cells[2].Text.ToString();
                if (FatherCode1 != "OUT_LAB_MAR")
                {
                    e.Row.Attributes.Add("ondblclick", "PurMAR('" + ContractNo + "','" + mar_code + "','" + mar_name + "')");
                    e.Row.Attributes["style"] = "Cursor:hand";
                    e.Row.Attributes.Add("title", "双击查看原始数据");
                }
            }
        }
    }
}
