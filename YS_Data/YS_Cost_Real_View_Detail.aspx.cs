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

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Real_View_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTitle();
            }
        }
        protected void BindTitle()
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string ContractNo = ed.DecryptText(Request.QueryString["CONTRACTNO"].ToString());
            string FatherCode = ed.DecryptText(Request.QueryString["FatherCode"].ToString());
            string sql_title = "select FATHER_NAME from YS_COST_BUDGET_NAME where FATHER_CODE='" + FatherCode + "' ";
            System.Data.DataTable dt_title = DBCallCommon.GetDTUsingSqlText(sql_title);
            if (dt_title.Rows.Count > 0)
            {
                lbl_fathername.Text = dt_title.Rows[0][0].ToString().Trim();   //绑定标题
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
                case "FAC_CONTRACT": this.Bind_LABOR(ContractNo, FatherCode); break;
                //case "MANU_COST":
                //case "SELL_COST":
                //case "MANAGE_COST":
                //case "Taxes_Cost": this.Bind_OTHER(ContractNo, FatherCode); break;
                case "PAINT_COATING":
                case "ELECTRICAL":
                case "PRODUCT_OUT": this.Bind_Paint_Elec_Pro(ContractNo, FatherCode); break;
                default: break;
            }
        }

        protected void Bind_OUT(string ContractNo, string FatherCode)
        {
            string sql_OUT_LAB_MAR = "select YS_CODE,YS_NAME from YS_COST_BUDGET_DETAIL where YS_CONTRACT_NO='" + ContractNo + "'and YS_FATHER='" + FatherCode + "'";
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
            double num_f = 0;
            double price_f = 0;
            double money_f = 0;
            if (ENGID != "")
            {
                string sql_total = "select YS_" + FatherCode + " from YS_COST_REAL where YS_CONTRACT_NO='" + ContractNo + "'";
                System.Data.DataTable dt_total = DBCallCommon.GetDTUsingSqlText(sql_total);
                if (dt_total.Rows.Count > 0)
                {
                    total = (dt_total.Rows[0][0].ToString() == "" ? 0 : double.Parse(dt_total.Rows[0][0].ToString()));  //总金额      
                }

                string sql_money = "SELECT sum(WG_AMOUNT) as WG_AMOUNT, sum(WG_RSNUM) as WG_RSNUM ,sum(WG_AMOUNT)/ sum(WG_RSNUM) as WG_UPRICE" +
                    " FROM [View_SM_IN] where charindex('X',WG_CODE)>0 and WG_DOC like '03%' and CHARINDEX([WG_TASKID] + ';', '" + ENGID + "') > 0 and WG_TASKID!=''";
                for (int j = 0; j < dt_OUT_LAB_MAR.Rows.Count; j++)
                {
                    string sql = "";

                    if (dt_OUT_LAB_MAR.Rows[j]["YS_NAME"].ToString() == "包工包料外协")
                    {
                        sql += sql_money + " and WG_UPRICE>5000";
                    }
                    else if (dt_OUT_LAB_MAR.Rows[j]["YS_NAME"].ToString() == "带料外协")
                    {
                        sql += sql_money + " and WG_UPRICE<5000";
                    }
                    System.Data.DataTable dt_money = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt_money.Rows.Count > 0)
                    {
                        num_f = (dt_money.Rows[0]["WG_RSNUM"].ToString() == "" ? 0 : double.Parse(dt_money.Rows[0]["WG_RSNUM"].ToString()));
                        price_f = (dt_money.Rows[0]["WG_UPRICE"].ToString() == "" ? 0 : double.Parse(dt_money.Rows[0]["WG_UPRICE"].ToString()));
                        money_f = (dt_money.Rows[0]["WG_AMOUNT"].ToString() == "" ? 0 : double.Parse(dt_money.Rows[0]["WG_AMOUNT"].ToString()));
                    }
                    dt_OUT_LAB_MAR.Rows[j]["YS_Union_Amount"] = num_f.ToString("N3");
                    dt_OUT_LAB_MAR.Rows[j]["YS_Average_Price"] = price_f.ToString("F3"); ;
                    dt_OUT_LAB_MAR.Rows[j]["YS_MONEY"] = money_f.ToString("F3"); ;
                }
            }

            lbl_total.Text = total.ToString("N2");

            GridView1.DataSource = dt_OUT_LAB_MAR;
            GridView1.DataBind();
            GridView1.Columns[1].Visible = false;
        }

        protected void Bind_MAR(string ContractNo, string FatherCode)
        {
            string fatherid = "";
            string sql_MAR = "select  YS_CODE,YS_NAME from YS_COST_BUDGET_DETAIL where YS_CONTRACT_NO='" + ContractNo + "'and YS_FATHER='" + FatherCode + "'";
            System.Data.DataTable dt_MAR = DBCallCommon.GetDTUsingSqlText(sql_MAR);
            dt_MAR.Columns.Add("YS_Union_Amount");
            dt_MAR.Columns.Add("YS_Average_Price");
            dt_MAR.Columns.Add("YS_MONEY");

            //获取该合同号下的所有生产制号
            string sql_ENGID = "select PCON_SCH from View_YS_CONTRACT where PCON_BCODE='" + ContractNo + "'";
            System.Data.DataTable dt_ENGID = DBCallCommon.GetDTUsingSqlText(sql_ENGID);
            string ENGID = "";
            if (dt_ENGID.Rows.Count > 0)
            {
                ENGID = dt_ENGID.Rows[0]["PCON_SCH"].ToString();
            }

            double total = 0;   //总金额
            string sql_total = "select YS_" + FatherCode + " from YS_COST_REAL where YS_CONTRACT_NO='" + ContractNo + "'";
            System.Data.DataTable dt_total = DBCallCommon.GetDTUsingSqlText(sql_total);
            if (dt_total.Rows.Count > 0)
            {
                total = (dt_total.Rows[0][0].ToString() == "" ? 0 : double.Parse(dt_total.Rows[0][0].ToString()));  //总金额      
            }

            #region 黑色金属
            if (FatherCode == "FERROUS_METAL")
            {
                fatherid = "01.07";
                string sql_money = "select sum(Amount) as Amount,sum(RealNumber) as RealNumber, sum(Amount)/sum(RealNumber) as UnitPrice from View_SM_OUT where  CHARINDEX(TASKID + ';', '" + ENGID + "') > 0 AND TASKID != '' and MaterialCode like'" + fatherid + "%'";
                for (int j = 0; j < dt_MAR.Rows.Count; j++)
                {
                    string sql = "";
                    if (dt_MAR.Rows[j]["YS_NAME"].ToString() == "轨道系统")
                    {
                        sql = sql_money + " and MaterialName like '%轨道%'";
                    }
                    else if (dt_MAR.Rows[j]["YS_NAME"].ToString() == "定尺板")
                    {
                        sql = sql_money + " and PlanMode='定尺板'";
                    }
                    else if (dt_MAR.Rows[j]["YS_NAME"].ToString() == "普通材料")
                    {
                        sql = sql_money + " and charindex('轨道',MaterialName)=0  and PlanMode !='定尺板'";
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

            #region 外购件、加工件
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

                string sql_money = "select sum(Amount) as Amount,sum(RealNumber) as RealNumber," +
                    " sum(Amount)/sum(RealNumber) as UnitPrice from View_SM_OUT " +
                    "where CHARINDEX(TASKID + ';', '" + ENGID + "') > 0 AND TASKID != '' and MaterialCode like'" + fatherid + "%'";

                //获取明细物料费用
                double main_mar = 0;
                for (int j = 0; j < dt_MAR.Rows.Count; j++)
                {
                    if (dt_MAR.Rows[j]["YS_CODE"].ToString() != "other")
                    {
                        string sql = "";
                        sql = sql_money + " and charindex('" + dt_MAR.Rows[j]["YS_NAME"].ToString() + "',MaterialName)>0 ";

                        //判断预算物料表是否有包含此名称的材料
                        string sql_contain = "select YS_Product_Name from TBBD_Product_type where YS_Product_Tag='1' " +
                            "and charindex('-',YS_Product_Code)>0 and YS_Product_Name like '%" + dt_MAR.Rows[j]["YS_NAME"].ToString() + "%' " +
                            "and YS_Product_Name!='" + dt_MAR.Rows[j]["YS_NAME"].ToString() + "'";
                        DataTable dt_contain = DBCallCommon.GetDTUsingSqlText(sql_contain);
                        for (int s = 0; s < dt_contain.Rows.Count; s++)
                        {
                            sql += " and charindex('" + dt_contain.Rows[s]["YS_Product_Name"] + "',MaterialName)=0";
                        }

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
            #endregion

            lbl_total.Text = total.ToString("N2");

            GridView1.DataSource = dt_MAR;
            GridView1.DataBind();
        }

        protected void Bind_OTHER_MAR(string ContractNo, string FatherCode)
        {
            //获取该合同号下的所有生产制号
            string sql_ENGID = "select PCON_SCH from View_YS_COST_BUDGET_REAL where YS_CONTRACT_NO='" + ContractNo + "'";
            System.Data.DataTable dt_ENGID = DBCallCommon.GetDTUsingSqlText(sql_ENGID);
            string ENGID = "";
            if (dt_ENGID.Rows.Count > 0)
            {
                ENGID = dt_ENGID.Rows[0]["PCON_SCH"].ToString();
            }

            double total = 0;   //总金额
            string sql_total = "select YS_" + FatherCode + " from YS_COST_REAL where YS_CONTRACT_NO='" + ContractNo + "'";
            System.Data.DataTable dt_total = DBCallCommon.GetDTUsingSqlText(sql_total);
            if (dt_total.Rows.Count > 0)
            {
                total = (dt_total.Rows[0][0].ToString() == "" ? 0 : double.Parse(dt_total.Rows[0][0].ToString()));  //总金额      
            }

            string sql_OtherMar = "select  YS_CODE,YS_NAME from YS_COST_BUDGET_DETAIL where YS_CONTRACT_NO='" + ContractNo + "'and YS_FATHER='" + FatherCode + "' ";
            System.Data.DataTable dt_OtherMar = DBCallCommon.GetDTUsingSqlText(sql_OtherMar);
            dt_OtherMar.Columns.Add("YS_Union_Amount");
            dt_OtherMar.Columns.Add("YS_Average_Price");
            dt_OtherMar.Columns.Add("YS_MONEY");
            double main_mar = 0;
            for (int j = 0; j < dt_OtherMar.Rows.Count; j++)
            {
                string sql_code = "select sum(Amount) as Amount from View_SM_OUT where CHARINDEX(TASKID + ';', '" + ENGID + "') > 0 AND TASKID != '' and MaterialCode like '" + dt_OtherMar.Rows[j]["YS_CODE"] + "%'";
                System.Data.DataTable dt_code = DBCallCommon.GetDTUsingSqlText(sql_code);
                double money = 0;
                if (dt_code.Rows.Count > 0)
                {
                    money = (dt_code.Rows[0]["Amount"].ToString() == "" ? 0 : double.Parse(dt_code.Rows[0]["Amount"].ToString()));
                }
                dt_OtherMar.Rows[j]["YS_MONEY"] = money.ToString("F3");
                main_mar += money;
            }

            double other = total - main_mar;
            DataRow[] dr = dt_OtherMar.Select("YS_CODE='other'");   //其它费用
            for (int i = 0; i < dr.Length; i++)
            {
                dr[i]["YS_MONEY"] = other.ToString("F3");
            }

            lbl_total.Text = total.ToString("N2");

            GridView1.DataSource = dt_OtherMar;
            GridView1.DataBind();
            GridView1.Columns[3].Visible = false;    //单价数量隐藏
            GridView1.Columns[4].Visible = false;
        }

        protected void Bind_LABOR(string ContractNo, string FatherCode)
        {
            //获取预算信息、创建datatable
            string sql_LABOR = "select  YS_CODE,YS_NAME from YS_COST_BUDGET_DETAIL where YS_CONTRACT_NO='" + ContractNo + "'and YS_FATHER='" + FatherCode + "' ";
            System.Data.DataTable dt_LABOR = DBCallCommon.GetDTUsingSqlText(sql_LABOR);
            dt_LABOR.Columns.Add("YS_Union_Amount");
            dt_LABOR.Columns.Add("YS_Average_Price");
            dt_LABOR.Columns.Add("YS_MONEY");

            //获取该合同号下的所有生产制号
            string sql_ENGID = "select PCON_SCH from View_YS_CONTRACT where PCON_BCODE='" + ContractNo + "'";
            System.Data.DataTable dt_ENGID = DBCallCommon.GetDTUsingSqlText(sql_ENGID);
            string ENGID = "";
            if (dt_ENGID.Rows.Count > 0)
            {
                ENGID = dt_ENGID.Rows[0]["PCON_SCH"].ToString();
            }

            //班组or厂内
            string PS_BZ = "";
            switch (FatherCode)
            {
                case "TEAM_CONTRACT": PS_BZ = " AND PS_BZ NOT IN ( '盛铭', '宏顺', '唐山远大', '耐迩', '毅达','武汉维治' )";
                    break;
                case "FAC_CONTRACT": PS_BZ = " AND PS_BZ IN ( '盛铭', '宏顺', '唐山远大', '耐迩', '毅达','武汉维治' )";
                    break;
                default: break;
            }

            double total = 0;   //总金额
            double main_mar = 0;  //明细金额
            string sql_total = "select YS_" + FatherCode + " from YS_COST_REAL where YS_CONTRACT_NO='" + ContractNo + "'";
            System.Data.DataTable dt_total = DBCallCommon.GetDTUsingSqlText(sql_total);
            if (dt_total.Rows.Count > 0)
            {
                total = (dt_total.Rows[0][0].ToString() == "" ? 0 : double.Parse(dt_total.Rows[0][0].ToString()));  //总金额      
            }

            if (ENGID != "")
            {
                double money = 0;
                double num = 0;
                double price = 0;
                string sql_money = "select sum(PS_JE) as PS_JE,sum(cast(PS_ZONGZHONG as decimal)) as PS_ZONGZHONG," +
                    "sum(PS_JE)/sum(cast(PS_ZONGZHONG as decimal)) as PS_DJ from View_TBMP_STATISTICS " +
                    "where  CHARINDEX([PS_ENGID] + ';', '" + ENGID + "') > 0 and PS_ENGID!='' " + PS_BZ;

                string sql_name = "select  YS_CODE,YS_NAME from YS_COST_BUDGET_DETAIL where YS_CONTRACT_NO='" + ContractNo + "'and YS_FATHER='" + FatherCode + "'";
                System.Data.DataTable dt_name = DBCallCommon.GetDTUsingSqlText(sql_name);
                for (int j = 0; j < dt_LABOR.Rows.Count; j++)
                {
                    if (dt_LABOR.Rows[j]["YS_CODE"].ToString() != "other")
                    {
                        string sql = "";
                        sql = sql_money + " and PS_ZXNAME ='" + dt_LABOR.Rows[j]["YS_NAME"].ToString() + "'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt.Rows.Count > 0)
                        {
                            num = (dt.Rows[0]["PS_ZONGZHONG"].ToString() == "" ? 0 : double.Parse(dt.Rows[0]["PS_ZONGZHONG"].ToString()));
                            price = (dt.Rows[0]["PS_DJ"].ToString() == "" ? 0 : double.Parse(dt.Rows[0]["PS_DJ"].ToString()));
                            money = (dt.Rows[0]["PS_JE"].ToString() == "" ? 0 : double.Parse(dt.Rows[0]["PS_JE"].ToString()));
                        }
                        dt_LABOR.Rows[j]["YS_Union_Amount"] = num.ToString("N3");
                        dt_LABOR.Rows[j]["YS_Average_Price"] = price.ToString("F3");
                        dt_LABOR.Rows[j]["YS_MONEY"] = money.ToString("F3");
                        main_mar += money;
                    }
                }
            }
            double other = total - main_mar;
            DataRow[] dr = dt_LABOR.Select("YS_CODE='other'");   //其它费用
            for (int i = 0; i < dr.Length; i++)
            {
                dr[i]["YS_MONEY"] = other.ToString("F3");
            }
            lbl_total.Text = total.ToString("N2");
            GridView1.DataSource = dt_LABOR;
            GridView1.DataBind();
        }

        protected void Bind_OTHER(string ContractNo, string FatherCode)
        {
            string sql_OTHER = "select YS_CODE,YS_NAME,YS_MONEY from YS_COST_REAL_DETAIL where YS_CONTRACT_NO='" + ContractNo + "'and YS_FATHER='" + FatherCode + "' and YS_NAME is not null and YS_MONEY is not null";
            System.Data.DataTable dt_OTHER = DBCallCommon.GetDTUsingSqlText(sql_OTHER);
            dt_OTHER.Columns.Add("YS_Union_Amount");
            dt_OTHER.Columns.Add("YS_Average_Price");

            string sql_total = "select YS_" + FatherCode + " from YS_COST_REAL_OTHER where YS_CONTRACT_NO='" + ContractNo + "'";
            System.Data.DataTable dt_total = DBCallCommon.GetDTUsingSqlText(sql_total);
            if (dt_total.Rows.Count > 0)
            {
                string total = dt_total.Rows[0][0].ToString();
                double total_f = (total == "" ? 0 : double.Parse(total));
                lbl_total.Text = total_f.ToString("N2");
            }
            GridView1.DataSource = dt_OTHER;
            GridView1.DataBind();
            GridView1.Columns[3].Visible = false;   //单价数量不显示
            GridView1.Columns[4].Visible = false;
        }

        protected void Bind_Paint_Elec_Pro(string ContractNo, string FatherCode)
        {
            string sql_Tol = "select  YS_CODE,YS_NAME from YS_COST_BUDGET_DETAIL where YS_CONTRACT_NO='" + ContractNo + "'and YS_FATHER='" + FatherCode + "'";
            System.Data.DataTable dt_Tol = DBCallCommon.GetDTUsingSqlText(sql_Tol);
            dt_Tol.Columns.Add("YS_Union_Amount");
            dt_Tol.Columns.Add("YS_Average_Price");
            dt_Tol.Columns.Add("YS_MONEY");

            double total = 0;   //总金额
            string sql_total = "select YS_" + FatherCode + " from YS_COST_REAL where YS_CONTRACT_NO='" + ContractNo + "'";
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
            if (FatherCode == "PRODUCT_OUT")
            {
                GridView1.Columns[1].Visible = false;
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
                string ContractNo = Request.QueryString["ContractNo"].ToString();
                string FatherCode = Request.QueryString["FatherCode"].ToString();
                Encrypt_Decrypt ed = new Encrypt_Decrypt();
                string FatherCode1 = ed.DecryptText(FatherCode);
                switch (FatherCode1)
                {
                    case "PRODUCT_OUT":
                    case "OUT_LAB_MAR": e.Row.Attributes.Add("ondblclick", "PurOUT('" + ContractNo + "','" + FatherCode + "')"); break;
                    case "FERROUS_METAL":
                    case "PURCHASE_PART":
                    case "MACHINING_PART":
                    case "PAINT_COATING":
                    case "ELECTRICAL":
                    case "OTHERMAT_COST": e.Row.Attributes.Add("ondblclick", "PurMAR('" + ContractNo + "','" + FatherCode + "')"); break;
                    case "TEAM_CONTRACT":
                    case "FAC_CONTRACT": e.Row.Attributes.Add("ondblclick", "PurLABOR('" + ContractNo + "','" + FatherCode + "')"); break;
                    default: break;
                }
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看原始数据");
            }
        }
    }
}
