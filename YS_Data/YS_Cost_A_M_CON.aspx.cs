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
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using ZCZJ_DPF;

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_A_M_CON : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ViewState["type"] = Request.QueryString["type"].ToString();   //'1'已完结合同  空：正在进行合同 
                EXEPROCEDURE();
                bind_ddl();
                InitVar();//分页
                GetTechRequireData();
                //control_visible();
            }
            InitVar();
        }
        protected void bind_ddl()
        {
            
            //string sqltext_people = "select distinct YS_ADDNAME,YS_ADDNAME from View_YS_CON_BUDGET_REAL where 1=1 " + type;
            string sqltext_PJ = "SELECT DISTINCT PCON_PJNAME ,PCON_PJNAME FROM View_YS_CON_BUDGET_REAL where 1=1 and YS_XS_Finished='0'";
            string sqltext_ENG = "SELECT DISTINCT PCON_ENGNAME,PCON_ENGNAME FROM View_YS_CON_BUDGET_REAL where 1=1 and YS_XS_Finished='0'";
            
            //DBCallCommon.FillDroplist(dpl_people, sqltext_people);
            DBCallCommon.FillDroplist(ddl_project, sqltext_PJ);
            DBCallCommon.FillDroplist(ddl_engineer, sqltext_ENG);
        }

        protected void control_visible()
        {
            string DEP = Session["UserDeptID"].ToString();
            string name = Session["UserName"].ToString();
            for (int i = 4; i < 40; i++)//从合同号后面的列开始全部隐藏
            {
                GridView1.Columns[i].Visible = false;
            }
            if (DEP == "01" || DEP == "08")//财务部和公司领导
            {
                for (int i = 4; i < 40; i++)//全部显示
                {
                    GridView1.Columns[i].Visible = true;
                }
            }
            else if (DEP == "03")//技术部
            {
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[25].Visible = true;
            }
            else if (DEP == "12")//市场部
            {
                for (int i = 9; i < 15; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
                for (int i = 26; i < 32; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
                GridView1.Columns[22].Visible = true;
                GridView1.Columns[39].Visible = true;
            }
            else if (DEP == "04")//生产部
            {
                for (int i = 15; i < 18; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
                for (int i = 32; i < 35; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
            }
        }

        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetTechRequireData();
        }

        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "View_YS_CON_BUDGET_REAL";
            pager.PrimaryKey = "YS_CONTRACT_NO";
            pager.ShowFields = "YS_CONTRACT_NO,PCON_PJNAME,PCON_ENGNAME,YS_BUDGET_INCOME,(YS_MATERIAL_COST+YS_TEAM_CONTRACT+YS_FAC_CONTRACT+YS_PRODUCT_OUT+YS_TRANS_COST+YS_ZZFY) AS YS_RealCost," +
                "(YS_MATERIAL_COST_BG+YS_TEAM_CONTRACT_BG+YS_FAC_CONTRACT_BG+YS_PRODUCT_OUT_BG+YS_TRANS_COST_BG) AS YS_Cost," +
                "Convert(decimal(10,2), (YS_PROFIT/(YS_PROFIT_BG+1))) as YS_PROFIT_BG_hide_percent,YS_PROFIT_BG,Convert(decimal(10,2),YS_PROFIT/(YS_PROFIT_BG+1)) as YS_PROFIT_BG_percent," +
                "Convert(decimal(10,2), (YS_FERROUS_METAL/(YS_FERROUS_METAL_BG+1))) as YS_FERROUS_METAL_BG_percent,Convert(decimal(10,2), (YS_FERROUS_METAL/(YS_FERROUS_METAL_BG+1))) as YS_FERROUS_METAL_BG_hide_percent,YS_FERROUS_METAL_BG," +
                "Convert(decimal(10,2), (YS_PURCHASE_PART/(YS_PURCHASE_PART_BG+1))) as YS_PURCHASE_PART_BG_percent,Convert(decimal(10,2), (YS_PURCHASE_PART/(YS_PURCHASE_PART_BG+1))) as YS_PURCHASE_PART_BG_hide_percent,YS_PURCHASE_PART_BG," +
                "Convert(decimal(10,2), (YS_CASTING_FORGING/(YS_CASTING_FORGING_COST_BG+1))) as YS_CASTING_FORGING_BG_percent,Convert(decimal(10,2), (YS_CASTING_FORGING/(YS_CASTING_FORGING_COST_BG+1))) as YS_CASTING_FORGING_BG_hide_percent,YS_CASTING_FORGING_COST_BG," +
                "Convert(decimal(10,2), (YS_PAINT_COATING/(YS_PAINT_COATING_BG+1))) as YS_PAINT_COATING_BG_percent,Convert(decimal(10,2), (YS_PAINT_COATING/(YS_PAINT_COATING_BG+1))) as YS_PAINT_COATING_BG_hide_percent,YS_PAINT_COATING_BG," +
                "Convert(decimal(10,2), (YS_ELECTRICAL/(YS_ELECTRICAL_BG+1))) as YS_ELECTRICAL_BG_percent,Convert(decimal(10,2), (YS_ELECTRICAL/(YS_ELECTRICAL_BG+1))) as YS_ELECTRICAL_BG_hide_percent,YS_ELECTRICAL_BG," +
                "Convert(decimal(10,2), (YS_OTHERMAT_COST/(YS_OTHERMAT_COST_BG+1))) as YS_OTHERMAT_COST_BG_percent,Convert(decimal(10,2), (YS_OTHERMAT_COST/(YS_OTHERMAT_COST_BG+1))) as YS_OTHERMAT_COST_BG_hide_percent,YS_OTHERMAT_COST_BG," +
                "Convert(decimal(10,2), (YS_MATERIAL_COST/(YS_MATERIAL_COST_BG+1))) as YS_MATERIAL_COST_BG_percent,Convert(decimal(10,2), (YS_MATERIAL_COST/(YS_MATERIAL_COST_BG+1))) as YS_MATERIAL_COST_BG_hide_percent,YS_MATERIAL_COST_BG," +
                "Convert(decimal(10,2), (YS_TEAM_CONTRACT/(YS_TEAM_CONTRACT_BG+1))) as YS_TEAM_CONTRACT_BG_percent,Convert(decimal(10,2), (YS_TEAM_CONTRACT/(YS_TEAM_CONTRACT_BG+1))) as YS_TEAM_CONTRACT_BG_hide_percent,YS_TEAM_CONTRACT_BG," +
                "Convert(decimal(10,2), (YS_FAC_CONTRACT/(YS_FAC_CONTRACT_BG+1))) as YS_FAC_CONTRACT_BG_percent,Convert(decimal(10,2), (YS_FAC_CONTRACT/(YS_FAC_CONTRACT_BG+1))) as YS_FAC_CONTRACT_BG_hide_percent,YS_FAC_CONTRACT_BG," +
                "Convert(decimal(10,2), (YS_PRODUCT_OUT/(YS_PRODUCT_OUT_BG+1))) as YS_PRODUCT_OUT_BG_percent,Convert(decimal(10,2), (YS_PRODUCT_OUT/(YS_PRODUCT_OUT_BG+1))) as YS_PRODUCT_OUT_BG_hide_percent,YS_PRODUCT_OUT_BG," +
                "Convert(decimal(10,2), (YS_TRANS_COST/(YS_TRANS_COST_BG+1))) as YS_TRANS_COST_BG_percent,Convert(decimal(10,2), (YS_TRANS_COST/(YS_TRANS_COST_BG+1))) as YS_TRANS_COST_BG_hide_percent,YS_TRANS_COST_BG," +
                "YS_PROFIT,YS_ZZFY,YS_FERROUS_METAL,YS_PURCHASE_PART,YS_CASTING_FORGING,YS_PAINT_COATING,YS_ELECTRICAL,YS_OTHERMAT_COST,YS_MATERIAL_COST,YS_TEAM_CONTRACT,YS_FAC_CONTRACT,YS_PRODUCT_OUT,YS_TRANS_COST,YS_XS_Finished,YS_Finshtime,YS_Deadtime";
            pager.OrderField = "YS_Finshtime";
            pager.StrWhere = this.GetStrWhere();
            pager.OrderType = 1;//按合同号升序排列
            pager.PageSize = 10;
        }

        protected void GetTechRequireData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        protected string GetStrWhere()
        {
            string strwhere = " 1=1 ";
            string type = " and YS_XS_Finished='0'";
            strwhere += type;
            strwhere += "  and YS_CONTRACT_NO like '%" + txt_search.Text.ToString() + "%'";

            string this_month = DateTime.Now.ToString("yyyy-MM");
            this_month += "-01";

            if (ddl_project.SelectedIndex != 0)//项目名称
            {
                strwhere += " and PCON_PJNAME='" + ddl_project.SelectedValue + "'";
            }
            if (ddl_engineer.SelectedIndex != 0)//工程名称
            {
                strwhere += " and PCON_ENGNAME='" + ddl_engineer.SelectedValue + "'";
            }

            //结算筛选
            if (rbl_profit.SelectedIndex == 1)
            {
                strwhere += " and YS_Deadtime>GETDATE()";
            }
            else if (rbl_profit.SelectedIndex == 2)
            {
                strwhere += " and YS_Deadtime<=GETDATE()";
            }

            ////生产制号
            //if (txtprotect_num.Text.ToString() != "")
            //{
            //    strwhere += " and PCON_SCH like '%" + txtprotect_num.Text.ToString().Trim() + "%'";
            //}



            //费用筛选
            //#region
            //string[] ddlname = { "dpl_materials", "dpl_labor" };
            //string[] fathername_BG =
            //{

            //    " (YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_CASTING_FORGING_COST_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)",
            //    " (YS_TEAM_CONTRACT_BG+YS_FAC_CONTRACT_BG+YS_PRODUCT_OUT_BG)"

            //};
            //string[] fathername =
            //{

            //    " (YS_FERROUS_METAL+YS_PURCHASE_PART+YS_CASTING_FORGING+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST)",
            //    " (YS_TEAM_CONTRACT+YS_FAC_CONTRACT+YS_PRODUCT_OUT)"

            //};

            //for (int i = 0; i < 2; i++)
            //{
            //    DropDownList ddl = (DropDownList)Pal_condition.FindControl(ddlname[i]);
            //    if (ddl.SelectedIndex == 1)
            //    {
            //        strwhere += " and " + fathername[i] + "<" + fathername_BG[i] + "*0.9";
            //    }
            //    else if (ddl.SelectedIndex == 2)
            //    {
            //        strwhere += " and " + fathername[i] + ">=" + fathername_BG[i] + "*0.9 and " + fathername[i] + "<" + fathername_BG[i] + "";
            //    }
            //    else if (ddl.SelectedIndex == 3)
            //    {
            //        strwhere += " and " + fathername[i] + ">=" + fathername_BG[i] + "";
            //    }
            //}

            //#endregion

            //毛利润
            //if (dpl_all_profit.SelectedIndex == 1)
            //{
            //    strwhere += " and YS_PROFIT>= YS_PROFIT_BG";
            //}
            //else if (dpl_all_profit.SelectedIndex == 2)
            //{
            //    strwhere += " and YS_PROFIT>= YS_PROFIT_BG*0.9 and YS_PROFIT<YS_PROFIT_BG";
            //}
            //else if (dpl_all_profit.SelectedIndex == 3)
            //{
            //    strwhere += " and YS_PROFIT<YS_PROFIT_BG*0.9";
            //}

            //计划提交时间
            //if (txt_make_sta.Text != "")
            //{
            //    strwhere += " and CONVERT(datetime,YS_ADDDATE)>=CONVERT(datetime,'" + txt_make_sta.Text.ToString() + "')";
            //}
            //if (txt_make_end.Text != "")
            //{
            //    strwhere += " and CONVERT(datetime,YS_ADDDATE)<=CONVERT(datetime,'" + txt_make_end.Text.ToString() + "')";
            //}

            //合同完成时间
            //if (finish_sta_time.Text != "")
            //{
            //    strwhere += " and CONVERT(datetime,YS_Finshtime)>=CONVERT(datetime,'" + finish_sta_time.Text.ToString() + "')";
            //}
            //if (finish_end_time.Text != "")
            //{
            //    strwhere += " and CONVERT(datetime,YS_Finshtime)<=CONVERT(datetime,'" + finish_end_time.Text.ToString() + "')";
            //}
            return strwhere;
        }
        #endregion

        protected void GridView1_onrowdatabound(object sender, GridViewRowEventArgs e)
        {
            String controlId = ((GridView)sender).ClientID;
            String uniqueId = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                uniqueId = String.Format("{0}{1}", controlId, e.Row.RowIndex);
                e.Row.Attributes.Add("onclick", String.Format("SelectRow('{0}', this);", uniqueId));
                //e.Row.Attributes.Add("onclick", "ItemOver(this)");  //单击行变色
                string lbl_CONTRACT_NO = ((System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_YS_CONTRACT_NO")).Text.ToString();
                //string lbl_pcon_sch = ((System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_pcon_sch")).Text.ToString();
                e.Row.Cells[1].Attributes.Add("ondblclick", "ShowContract('" + lbl_CONTRACT_NO + "')");//第二格，即合同号上添加双击事件
                e.Row.Cells[1].Attributes.Add("title", "双击关联合同信息！");

                //结算时间预警
                string hidden_deadtime = ((HiddenField)e.Row.FindControl("hidden_YS_Deadtime")).Value.ToString();
                DateTime deadtime = Convert.ToDateTime(hidden_deadtime);
                DateTime Nowtime = DateTime.Now;
                TimeSpan d1 = Nowtime.Subtract(deadtime);
                int d2=d1.Days;
                if (d2 <= 7 && Nowtime < deadtime)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Orange;
                }

                //结算时间报警
                string sql_fin = "select [GS_TIME],[CNFB_TIME],[WX_TIME] from [View_YS_CON_TIME] where [YS_CONTRACT_NO]='" + lbl_CONTRACT_NO + "'";
                System.Data.DataTable dt_fin = DBCallCommon.GetDTUsingSqlText(sql_fin);
                if (dt_fin.Rows.Count > 0)
                {
                    string gs = dt_fin.Rows[0][0].ToString();
                    string cnfb = dt_fin.Rows[0][1].ToString();
                    string wx = dt_fin.Rows[0][2].ToString();
                    if (gs != "")
                    {
                        DateTime gstime = Convert.ToDateTime(gs);
                        if (gstime > deadtime)
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                        }
                    }
                    if (cnfb != "")
                    {
                        DateTime cnfbtime = Convert.ToDateTime(cnfb);
                        if (cnfbtime > deadtime)
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                        }
                    }
                    if (wx != "")
                    {
                        DateTime wxtime = Convert.ToDateTime(wx);
                        if (wxtime > deadtime)
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                        }
                    }
                }



                Encrypt_Decrypt ed = new Encrypt_Decrypt();
                string CONTRACT_NO = ed.EncryptText(lbl_CONTRACT_NO);
                //string PCON_SCH = ed.EncryptText(lbl_pcon_sch);
                string[] fathername = {  "MATERIAL_COST", "TEAM_CONTRACT", "FAC_CONTRACT", "PRODUCT_OUT", "TRANS_COST","FERROUS_METAL", "PURCHASE_PART", "CASTING_FORGING", "PAINT_COATING", "ELECTRICAL", "OTHERMAT_COST" };
                // 利润总额和净利润的红黄预警
                string[] fathername_profit = { "PROFIT" };
                for (int m = 0; m < fathername_profit.Length; m++)
                {
                    //标签提示
                    double percent_O_B = ((HiddenField)e.Row.FindControl("hidden_" + fathername_profit[m])).Value.ToString() == "" ? 0 : Convert.ToDouble(((HiddenField)e.Row.FindControl("hidden_" + fathername_profit[m])).Value.ToString());//订单完成百分比
                    double db_Budget = Math.Round((((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername_profit[m])).Text.ToString()) == "" ? 0 : Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername_profit[m])).Text.ToString()) / 10000, 2);//预算费用
                    double db_Order = Math.Round((((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername_profit[m] + "_R")).Text.ToString()) == "" ? 0 : Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername_profit[m] + "_R")).Text.ToString()) / 10000, 2);//实际费用
                    double db_pass = Math.Round(db_Order - db_Budget, 2);
                    if (db_pass > 0)
                    {
                        e.Row.Cells[m + 8].Attributes["style"] = "Cursor:hand";
                        if (m == 0)
                        {
                            e.Row.Cells[m + 8].Attributes.Add("title", "毛利润达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,高出预算" + db_pass.ToString() + "万");
                        }
                        else
                        {
                            e.Row.Cells[m + 8].Attributes.Add("title", "净利润达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,高出预算" + db_pass.ToString() + "万");
                        }
                    }
                    else
                    {
                        db_pass = Math.Abs(db_pass);
                        if (m == 0)
                        {
                            e.Row.Cells[m + 8].Attributes.Add("title", "毛利润达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,低于预算" + db_pass.ToString() + "万");
                        }
                        else
                        {
                            e.Row.Cells[m + 8].Attributes.Add("title", "净利润达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,低于预算" + db_pass.ToString() + "万");
                        }
                    }
                    //红黄预警
                    if (percent_O_B < 0)
                    {
                        e.Row.Cells[m + 8].BackColor = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        percent_O_B = Math.Abs(percent_O_B);
                        if (percent_O_B > 1.0)
                        {
                            e.Row.Cells[m + 8].BackColor = System.Drawing.Color.Yellow;
                        }
                        if (percent_O_B < 1.0 && percent_O_B > 0.9)
                        {
                            e.Row.Cells[m + 8].BackColor = System.Drawing.Color.PeachPuff;
                        }
                    }
                }
                //红黄预警
                for (int i = 0; i < fathername.Length; i++)
                {
                    double percent_O_B = ((HiddenField)e.Row.FindControl("hidden_" + fathername[i])).Value.ToString() == "" ? 0 : Convert.ToDouble(((HiddenField)e.Row.FindControl("hidden_" + fathername[i])).Value.ToString());
                    percent_O_B = Math.Abs(percent_O_B);
                    if (percent_O_B > 0.9 && percent_O_B < 1.0)
                    {
                        e.Row.Cells[i + 9].BackColor = System.Drawing.Color.PeachPuff;
                    }
                    if (percent_O_B > 1.0)
                    {
                        e.Row.Cells[i + 9].BackColor = System.Drawing.Color.Yellow;
                    }
                }
                for (int j = 9; j < 20; j++)  //添加双击查看明细、红黄预警、进度显示
                {
                    //if (j < 21)
                    //{
                    double percent_O_B = ((HiddenField)e.Row.FindControl("hidden_" + fathername[j - 9])).Value.ToString() == "" ? 0 : Convert.ToDouble(((HiddenField)e.Row.FindControl("hidden_" + fathername[j - 9])).Value.ToString());//订单完成百分比
                    double db_Budget = Math.Round((((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername[j - 9])).Text.ToString()) == "" ? 0 : Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername[j - 9])).Text.ToString()) / 10000, 2);//预算费用
                    double db_Order = Math.Round((((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername[j - 9] + "_R")).Text.ToString()) == "" ? 0 : Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername[j - 9] + "_R")).Text.ToString()) / 10000, 2);//实际费用
                    double db_pass = Math.Round(db_Order - db_Budget, 2);
                    if (j < 20)
                    {
                        //e.Row.Cells[j].Attributes.Add("ondblclick", "PurMarView('" + PCON_SCH + "','" + ed.EncryptText(fathername[j - 9]) + "')");
                        e.Row.Cells[j].Attributes["style"] = "Cursor:hand";
                        if (db_pass > 0)
                        {
                            e.Row.Cells[j].Attributes.Add("title", "实际费用达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,超出预算" + db_pass.ToString() + "万");
                            //e.Row.Cells[j + 12].Attributes.Add("ondblclick", "PurMarView('" + PCON_SCH + "','" + ed.EncryptText(fathername[j - 9]) + "')");
                            e.Row.Cells[j + 12].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[j + 12].Attributes.Add("title", "实际费用达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,超出预算" + db_pass.ToString() + "万");
                        }
                        else
                        {
                            db_pass = Math.Abs(db_pass);
                            e.Row.Cells[j].Attributes.Add("title", "实际费用达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,低于预算" + db_pass.ToString() + "万");
                            //e.Row.Cells[j + 12].Attributes.Add("ondblclick", "PurMarView('" + PCON_SCH + "','" + ed.EncryptText(fathername[j - 9]) + "')");
                            e.Row.Cells[j + 12].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[j + 12].Attributes.Add("title", "实际费用达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,低于预算" + db_pass.ToString() + "万");

                        }
                    }
                    else
                    {
                        if (db_pass > 0)
                        {
                            e.Row.Cells[j].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[j].Attributes.Add("title", "费用达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,超出预算" + db_pass.ToString() + "万");
                        }
                        else
                        {
                            db_pass = Math.Abs(db_pass);
                            e.Row.Cells[j].Attributes.Add("title", "订单达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,低于预算" + db_pass.ToString() + "万");
                        }
                    }
                }
            }
        }

        //查询
        protected void btn_search_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }

        //重置条件
        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    //txtprotect_num.Text = "";
        //    //dpl_waixie.SelectedIndex = 0;
        //    dpl_materials.SelectedIndex = 0;
        //    dpl_labor.SelectedIndex = 0;
        //    //dpl_other.SelectedIndex = 0;
        //    //dpl_people.SelectedIndex = 0;
        //    dpl_all_profit.SelectedIndex = 0;
        //    //finish_sta_time.Text = "";
        //    //finish_end_time.Text = "";
        //    //txt_make_sta.Text = "";
        //    //txt_make_end.Text = "";
        //    ModalPopupExtenderSearch.Show();
        //}

        //取消
        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    ModalPopupExtenderSearch.Hide();
        //}

        //protected void btn_ShowSta_OnClick(object sender, EventArgs e)
        //{
        //    Response.Redirect("YS_Cost_Real_Sta.aspx");
        //    //return "javascript:window.showModalDialog('YS_Cost_Real_Sta.aspx','DialogWidth=800px;DialogHeight=700px')";
        //}

        //#region 导出EXCEL
        //protected void btn_daochu_Click(object sender, EventArgs e)
        //{
        //    string str_where = GetStrWhere();
        //    string sqltext = "";
        //    sqltext = "select [YS_CONTRACT_NO] , [PCON_SCH] , [PCON_PJNAME] , [PCON_ENGNAME] , [YS_BUDGET_INCOME] ,[YS_FERROUS_METAL] + [YS_PURCHASE_PART] + [YS_PAINT_COATING]+ [YS_CASTING_FORGING] + [YS_ELECTRICAL] + [YS_OTHERMAT_COST] + [YS_FAC_CONTRACT] + [YS_TEAM_CONTRACT] + [YS_PRODUCT_OUT] + [YS_TRANS_COST] AS YS_RealCost, [YS_PROFIT_BG], [YS_FERROUS_METAL_BG] , [YS_PURCHASE_PART_BG] , [YS_CASTING_FORGING_COST_BG] , [YS_PAINT_COATING_BG] , [YS_ELECTRICAL_BG], [YS_OTHERMAT_COST_BG] , [YS_TEAM_CONTRACT_BG] , [YS_FAC_CONTRACT_BG] , [YS_PRODUCT_OUT_BG] , [YS_TRANS_COST_BG] ,[YS_PROFIT], [YS_FERROUS_METAL] , [YS_PURCHASE_PART] , [YS_PAINT_COATING] , [YS_CASTING_FORGING] , [YS_ELECTRICAL] , [YS_OTHERMAT_COST], [YS_TEAM_CONTRACT] ,  [YS_FAC_CONTRACT] ,[YS_PRODUCT_OUT] ,[YS_TRANS_COST] ,[YS_ADDNAME], [YS_ADDTIME] , [YS_Finshtime] ,[YS_NOTE] from View_YS_CON_BUDGET_REAL where YS_REVSTATE='2' and PCON_SCH like '%" + txt_search.Text.ToString() + "%'";
        //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //    ExportDataItem(dt);
        //}

        //private void ExportDataItem(System.Data.DataTable objdt)
        //{
        //    Application m_xlApp = new Application();
        //    Workbooks workbooks = m_xlApp.Workbooks;
        //    Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        //    Worksheet wksheet;
        //    workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("成本监控表") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        //    m_xlApp.Visible = false;    // Excel不显示  
        //    m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

        //    wksheet = (Worksheet)workbook.Sheets.get_Item(1);

        //    System.Data.DataTable dt = objdt;

        //    // 填充数据
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        wksheet.Cells[i + 3, 1] = Convert.ToString(i + 1);//序号

        //        wksheet.Cells[i + 3, 2] = "'" + dt.Rows[i]["YS_CONTRACT_NO"].ToString();//合同号

        //        wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["PCON_SCH"].ToString();//任务号

        //        wksheet.Cells[i + 3, 4] = "'" + dt.Rows[i]["PCON_PJNAME"].ToString();//项目名称

        //        wksheet.Cells[i + 3, 5] = "'" + dt.Rows[i]["PCON_ENGNAME"].ToString();//工程名称

        //        wksheet.Cells[i + 3, 6] = "'" + dt.Rows[i]["YS_BUDGET_INCOME"].ToString();//预算收入

        //        wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["YS_RealCost"].ToString();//实际费用合计

        //        wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["YS_PROFIT_BG"].ToString();//毛利润（预）

        //        //wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["YS_PROFIT_TAX_BG"].ToString();//净利润（预）

        //        //wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["YS_OUT_LAB_MAR_BG"].ToString();//技术外协-预

        //        wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["YS_FERROUS_METAL_BG"].ToString();//黑色金属-预

        //        wksheet.Cells[i + 3, 10] = "'" + dt.Rows[i]["YS_PURCHASE_PART_BG"].ToString();//外购件--预

        //        wksheet.Cells[i + 3, 11] = "'" + dt.Rows[i]["YS_CASTING_FORGING_COST_BG"].ToString();//加工件--预

        //        wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["YS_PAINT_COATING_BG"].ToString();//油漆涂料-预

        //        wksheet.Cells[i + 3, 13] = "'" + dt.Rows[i]["YS_ELECTRICAL_BG"].ToString();//电气电料-预

        //        wksheet.Cells[i + 3, 14] = "'" + dt.Rows[i]["YS_OTHERMAT_COST_BG"].ToString();//其他材料费-预

        //        wksheet.Cells[i + 3, 15] = "'" + dt.Rows[i]["YS_TEAM_CONTRACT_BG"].ToString();//班组承包-预

        //        wksheet.Cells[i + 3, 16] = "'" + dt.Rows[i]["YS_FAC_CONTRACT_BG"].ToString();//厂内分包-预

        //        wksheet.Cells[i + 3, 17] = "'" + dt.Rows[i]["YS_PRODUCT_OUT_BG"].ToString();//生产外协-预

        //        //wksheet.Cells[i + 3, 19] = "'" + dt.Rows[i]["YS_MANU_COST_BG"].ToString();//制造费用-预

        //        //wksheet.Cells[i + 3, 20] = "'" + dt.Rows[i]["YS_SELL_COST_BG"].ToString();//销售费用-预

        //        //wksheet.Cells[i + 3, 21] = "'" + dt.Rows[i]["YS_MANAGE_COST_BG"].ToString();//管理费用-预

        //        //wksheet.Cells[i + 3, 22] = "'" + dt.Rows[i]["YS_Taxes_Cost_BG"].ToString();//税金及附加-预

        //        wksheet.Cells[i + 3, 18] = "'" + dt.Rows[i]["YS_TRANS_COST_BG"].ToString();//运费-预

        //        wksheet.Cells[i + 3, 19] = "'" + dt.Rows[i]["YS_PROFIT"].ToString();//毛利润-实

        //        //wksheet.Cells[i + 3, 25] = "'" + dt.Rows[i]["YS_PROFIT_TAX"].ToString();//净利润-实

        //        //wksheet.Cells[i + 3, 26] = dt.Rows[i]["YS_OUT_LAB_MAR"].ToString();//技术外协-实

        //        wksheet.Cells[i + 3, 20] = dt.Rows[i]["YS_FERROUS_METAL"].ToString();//黑色金属-实

        //        wksheet.Cells[i + 3, 21] = dt.Rows[i]["YS_PURCHASE_PART"].ToString();//外购件-实

        //        wksheet.Cells[i + 3, 22] = dt.Rows[i]["YS_CASTING_FORGING"].ToString();//加工件-实

        //        wksheet.Cells[i + 3, 23] = dt.Rows[i]["YS_PAINT_COATING"].ToString();//油漆涂料-实

        //        wksheet.Cells[i + 3, 24] = "'" + dt.Rows[i]["YS_ELECTRICAL"].ToString();//电气电料-实

        //        wksheet.Cells[i + 3, 25] = dt.Rows[i]["YS_OTHERMAT_COST"].ToString();//其他材料费-实

        //        wksheet.Cells[i + 3, 26] = dt.Rows[i]["YS_TEAM_CONTRACT"].ToString();//班组承包-实

        //        wksheet.Cells[i + 3, 27] = dt.Rows[i]["YS_FAC_CONTRACT"].ToString();//厂内分包-实

        //        wksheet.Cells[i + 3, 28] = dt.Rows[i]["YS_PRODUCT_OUT"].ToString();//生产外协-实

        //        //wksheet.Cells[i + 3, 36] = dt.Rows[i]["YS_MANU_COST"].ToString();//制造费用-实

        //        //wksheet.Cells[i + 3, 37] = dt.Rows[i]["YS_SELL_COST"].ToString();//销售费用-实

        //        //wksheet.Cells[i + 3, 38] = dt.Rows[i]["YS_MANAGE_COST"].ToString();//管理费用-实

        //        //wksheet.Cells[i + 3, 39] = dt.Rows[i]["YS_Taxes_Cost"].ToString();//税金及附加-实

        //        wksheet.Cells[i + 3, 29] = dt.Rows[i]["YS_TRANS_COST"].ToString();//运费-实

        //        wksheet.Cells[i + 3, 30] = dt.Rows[i]["YS_ADDNAME"].ToString();//制单人

        //        wksheet.Cells[i + 3, 31] = dt.Rows[i]["YS_ADDTIME"].ToString();//制单时间

        //        wksheet.Cells[i + 3, 32] = dt.Rows[i]["YS_Finshtime"].ToString();//完成时间

        //        wksheet.Cells[i + 3, 33] = dt.Rows[i]["YS_NOTE"].ToString();//备注

        //        wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 34]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 34]).VerticalAlignment = XlVAlign.xlVAlignCenter;
        //        wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 34]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        //    }
        //    //设置列宽
        //    wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

        //    string filename = Server.MapPath("/YS_Data/ExportFile/" + "成本监控表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

        //    ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        //}

        //private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        //{
        //    try
        //    {

        //        workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        workbook.Close(Type.Missing, Type.Missing, Type.Missing);
        //        m_xlApp.Workbooks.Close();
        //        m_xlApp.Quit();
        //        m_xlApp.Application.Quit();

        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

        //        wksheet = null;
        //        workbook = null;
        //        m_xlApp = null;
        //        GC.Collect();

        //        //下载

        //        System.IO.FileInfo path = new System.IO.FileInfo(filename);

        //        //同步，异步都支持
        //        HttpResponse contextResponse = HttpContext.Current.Response;
        //        contextResponse.Redirect(string.Format("~/YS_Data/ExportFile/{0}", path.Name), false);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //#endregion

        protected double GetIMGWidth(string IMG_width)
        {
            double width_img = 0;
            if (IMG_width != "")
            {
                double width = Convert.ToDouble(IMG_width);
                if (width > 1)
                {
                    width_img = 100;
                }
                else if (width == 0)
                {
                    width_img = 0;
                }
                else
                {
                    width_img = width * 100;
                }
            }
            return width_img;
        }

        protected double GetIMGWidth_YS_AMOUNT(string IMG_width)
        {
            double width_img = 100;
            if (IMG_width != "")
            {
                double width = Convert.ToDouble(IMG_width);
                if (width > 1 || width == 1)
                {
                    width_img = 100;
                }
                else if (width == 0)
                {
                    width_img = 0;
                }
                else if (width < 1 && width > 0)
                {
                    width_img = width * 100;
                }
                else
                {
                    width_img = 0;
                }
            }
            else
            {
                width_img = 0;
            }
            return width_img;
        }

        //protected void btnModify_OnClick(object sender, EventArgs e)
        //{
        //    string YS_CONTRACT_NO = "";
        //    string CONTRACT_NO = "";
        //    foreach (GridViewRow grow in GridView1.Rows)
        //    {
        //        System.Web.UI.WebControls.CheckBox ckb = (System.Web.UI.WebControls.CheckBox)grow.FindControl("CheckBox1");
        //        if (ckb.Checked)
        //        {
        //            CONTRACT_NO = ((HiddenField)grow.FindControl("hdfMP_ID")).Value.ToString();
        //            Encrypt_Decrypt ed = new Encrypt_Decrypt();
        //            YS_CONTRACT_NO = ed.EncryptText(CONTRACT_NO);
        //            break;
        //        }
        //    }
        //    if (YS_CONTRACT_NO != "")
        //    {
        //        string sql_fin = "select YS_XS_Finished from TBPM_CONPCHSINFO where PCON_BCODE='" + CONTRACT_NO + "'";
        //        System.Data.DataTable dt_fin = DBCallCommon.GetDTUsingSqlText(sql_fin);
        //        if (dt_fin.Rows.Count > 0)
        //        {
        //            if (dt_fin.Rows[0][0].ToString() == "1")
        //            {
        //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该合同已完成结算！');", true);
        //                return;
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    string sql = DBCallCommon.GetStringValue("connectionStrings");
        //                    sql += "Asynchronous Processing=true;";
        //                    SqlConnection sqlConn = new SqlConnection(sql);
        //                    sqlConn.Open();
        //                    SqlCommand sqlCmd = new SqlCommand("YS_COST_REAL_PROCEDURE", sqlConn);
        //                    sqlCmd.CommandType = CommandType.StoredProcedure;
        //                    sqlCmd.CommandTimeout = 0;
        //                    sqlCmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal
        //                    IAsyncResult result = sqlCmd.BeginExecuteNonQuery();
        //                    sqlCmd.EndExecuteNonQuery(result);
        //                    sqlConn.Close();

        //                }
        //                catch (Exception)
        //                {
        //                    throw;
        //                }
        //                string sql1 = "update TBPM_CONPCHSINFO set YS_XS_Finished='1',YS_Finshtime=GETDATE() where PCON_BCODE='" + CONTRACT_NO + "'";
        //                DBCallCommon.ExeSqlText(sql1);
        //                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('合同结算成功！');", true);
        //                UCPaging1.CurrentPage = 1;
        //                InitVar();
        //                GetTechRequireData();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要修改的行！！！');", true);
        //    }
        //}

        protected void btn_ShowTask_OnClick(object sender, EventArgs e)
        {
            string YS_CONTRACT_NO = "";
            string CONTRACT_NO = "";
            foreach (GridViewRow grow in GridView1.Rows)
            {
                System.Web.UI.WebControls.CheckBox ckb = (System.Web.UI.WebControls.CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    CONTRACT_NO = ((HiddenField)grow.FindControl("hdfMP_ID")).Value.ToString();
                    Encrypt_Decrypt ed = new Encrypt_Decrypt();
                    YS_CONTRACT_NO = ed.EncryptText(CONTRACT_NO);
                    break;
                }
            }
            if (YS_CONTRACT_NO != "")
            {
                string URL = "YS_Cost_Budget_A_M.aspx?ContractNo=" + YS_CONTRACT_NO;
                Response.Redirect(URL);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要查看的行！！！');", true);
            }
        }

        protected void EXEPROCEDURE()
        {
            try
            {
                string sql = DBCallCommon.GetStringValue("connectionStrings");
                sql += "Asynchronous Processing=true;";
                SqlConnection sqlConn = new SqlConnection(sql);
                sqlConn.Open();
                SqlCommand sqlCmd = new SqlCommand("YS_COST_REAL_PROCEDURE", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal
                IAsyncResult result = sqlCmd.BeginExecuteNonQuery();
                sqlCmd.EndExecuteNonQuery(result);
                sqlConn.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
