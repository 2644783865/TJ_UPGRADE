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
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Order_Detail : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind_ddl_mar();//绑定物料类型 下拉框
                bindGrid();
            }
            InitVar();
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        private void InitPager()
        {
            pager.TableName = "View_TBPC_PURORDERDETAIL_PLAN_TOTAL";
            pager.PrimaryKey = "";
            pager.ShowFields = "orderno,supplierid,suppliernm,zdtime,cgtimerq,pjid,pjnm,engid,pjid+'/'+engnm AS engnm,whstate,convert(float,zxnum) as zxnum,recgdnum,detailstate,detailcstate,marunit,convert(float,ctamount) as ctamount," +
                         "zdrid,zdrnm,shrid,shrnm,totalnote,totalstate,totalcstate,ptcode,PO_TUHAO,marid,marnm,margg,marcz,margb,PO_MASHAPE,length,width,PO_ZJE,PO_CGFS,ctprice,recdate";

            pager.OrderField = "zdtime desc,ptcode,marnm,margg";
            pager.StrWhere = GetStrWhere();
            pager.OrderType = 0;//按时间降序排列
            pager.PageSize = 15;
            //pager.PageIndex = 1;
        }

        protected string GetStrWhere()
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
            string strwhere = " engid='" + ContractNo + "'";
            string Mar_code = Request.QueryString["Marcode"].ToString();
            string nowtime = System.DateTime.Now.ToString("yyyy-MM-dd");
            string Mar_name = Request.QueryString["Marname"].ToString();
            //获取材料ID
            if (Mar_code != "other")
            {
                strwhere += " and marid like '" + Mar_code + "%'";
            }
            else
            {
                strwhere += " and charindex(substring(marid,1,5),'01.07.01.08.01.11.01.15.01.18')=0";
            }
            if (rab_daohuo.SelectedIndex != 0)//全部
            {
                if (rab_daohuo.SelectedIndex == 1)//未提交
                {
                    strwhere += "  and totalstate='0' and totalcstate='0' and detailcstate='0'";
                }
                if (rab_daohuo.SelectedIndex == 2)//未到货
                {
                    strwhere += " and detailstate='1' and recgdnum=0 and detailcstate='0'";
                }
                if (rab_daohuo.SelectedIndex == 3)//逾期未到货
                {
                    strwhere += " and detailstate='1' and recgdnum=0 and detailcstate='0' and cgtimerq<'" + nowtime + "'";
                }
                if (rab_daohuo.SelectedIndex == 4)//部分到货
                {
                    strwhere += " and detailstate='1' and recgdnum<zxnum and recgdnum>0 and detailcstate='0'";
                }
                if (rab_daohuo.SelectedIndex == 5)//已到货
                {
                    strwhere += " and (detailstate='3' or detailstate='2') and detailcstate='0'";
                }
                if (rab_daohuo.SelectedIndex == 6)//逾期到货
                {
                    strwhere += " and (detailstate='3' or detailstate='2') and detailcstate='0' and recdate>cgtimerq";
                }
                if (rab_daohuo.SelectedIndex == 7)//已关闭
                {
                    strwhere += " and detailcstate='1'";
                }
            }
            if (tb_orderno.Text != "")//订单编号
            {
                string str_orderid = tb_orderno.Text.ToString();
                string[] sArray = str_orderid.Split('-');
                strwhere = strwhere + " and (orderno like '%" + sArray[0].ToString() + "%'";
                for (int i = 1; i < sArray.Length; i++)
                {
                    strwhere = strwhere + " or orderno like '%" + sArray[i].ToString() + "%'";
                }
                strwhere = strwhere + " )";
            }
            if (tb_ptc.Text.Trim() != "")//计划跟踪号
            {
                strwhere = strwhere + " and ptcode like '%" + tb_ptc.Text.Trim() + "%'";
            }
            if (DropDownList_mar_type.SelectedIndex != 0)
            {
                strwhere = strwhere + " and PO_MASHAPE='" + DropDownList_mar_type.SelectedItem.Text + "'";
            }
            if (DropDownList_check_result.SelectedIndex != 0)
            {
                if (DropDownList_check_result.SelectedItem.Text == "未报检")
                {
                    strwhere = strwhere + " and PO_CGFS= '——'";
                }
                else
                {
                    strwhere = strwhere + " and PO_CGFS = '" + DropDownList_check_result.SelectedItem.Text + "'";
                }
            }
            if (Mar_name != "其它")
            {
                if (Mar_code == "01.07")
                {
                    if (Mar_name == "定尺板")
                    {
                        strwhere += " and PO_MASHAPE='定尺板'";
                    }
                    else
                        if (Mar_name == "轨道系统")
                        {
                            strwhere = strwhere + " and marnm like '%轨道%'";
                        }
                        else
                            if (Mar_name == "普通材料")
                            {
                                strwhere = strwhere + "  and charindex('轨道',marnm)=0  and PO_MASHAPE!='定尺板'";
                            }
                }
                if (Mar_code == "01.11" || Mar_code == "01.08")
                {
                    strwhere = strwhere + "  and charindex('" + Mar_name + "',marnm)>0 ";
                    //string sql_contain = "select YS_Product_Name from TBBD_Product_type where YS_Product_Tag='1' " +
                    //            "and charindex('-',YS_Product_Code)>0 and YS_Product_Name like '%" + Mar_name + "%' " +
                    //            "and YS_Product_Name!='" + Mar_name + "'";
                    //System.Data.DataTable dt_contain = DBCallCommon.GetDTUsingSqlText(sql_contain);
                    //for (int s = 0; s < dt_contain.Rows.Count; s++)
                    //{
                    //    strwhere += " and charindex('" + dt_contain.Rows[s]["YS_Product_Name"] + "',marnm)=0";
                    //}
                }
                if (Mar_code == "other")
                {
                    string sql_mar_type = "select YS_CODE from YS_COST_BUDGET_DETAIL where YS_NAME='" + Mar_name + "'";
                    System.Data.DataTable dt_mar_type = DBCallCommon.GetDTUsingSqlText(sql_mar_type);
                    if (dt_mar_type.Rows.Count > 0)
                    {
                        strwhere += " and marid like '" + dt_mar_type.Rows[0][0].ToString() + "%'";
                    }
                }
            }
            else
            {
                if (Mar_code == "01.11" || Mar_code == "01.08")
                {
                    string sql_mar_type = "select YS_NAME from YS_COST_BUDGET_DETAIL where YS_TSA_ID='" + ContractNo + "' and YS_CODE like '" + Mar_code + "%'";
                    System.Data.DataTable dt_mar_type = DBCallCommon.GetDTUsingSqlText(sql_mar_type);
                    for (int i = 0; i < dt_mar_type.Rows.Count; i++)
                    {
                        string sql_ctamount = "select marnm from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where engid='" + ContractNo + "' and marid like '" + Mar_code + "%'";
                        sql_ctamount += " and charindex('" + dt_mar_type.Rows[i][0].ToString() + "',marnm)>0";
                        //string sql_contain = "select YS_Product_Name from TBBD_Product_type where YS_Product_Tag='1' " +
                        //        "and charindex('-',YS_Product_Code)>0 and YS_Product_Name like '%" + dt_mar_type.Rows[i][0].ToString() + "%' " +
                        //        "and YS_Product_Name!='" + dt_mar_type.Rows[i][0].ToString() + "'";
                        //System.Data.DataTable dt_contain = DBCallCommon.GetDTUsingSqlText(sql_contain);
                        //for (int s = 0; s < dt_contain.Rows.Count; s++)
                        //{
                        //    sql_ctamount += " and charindex('" + dt_contain.Rows[s]["YS_Product_Name"] + "',marnm)=0";
                        //}
                        strwhere += " and marnm not in (" + sql_ctamount + ")";
                    }

                    //string sql_contain = "select YS_Product_Name from TBBD_Product_type where YS_Product_Tag='1' " +
                    //            "and charindex('-',YS_Product_Code)>0 and YS_Product_Name like '%" + Mar_name + "%' " +
                    //            "and YS_Product_Name!='" + Mar_name + "'";
                    //System.Data.DataTable dt_contain = DBCallCommon.GetDTUsingSqlText(sql_contain);
                    //string str_where_other="";
                    //for (int i = 0; i < dt_mar_type.Rows.Count;i++ )
                    //{
                    //    str_where_other+=" and charindex('" + dt_mar_type.Rows[i]["YS_NAME"] + "',YS_Product_Name)=0";
                    //    strwhere += " and charindex('" + dt_mar_type.Rows[i]["YS_NAME"] + "',marnm)=0";
                    //}
                    //string sql_mar_type_other = "select YS_Product_Name from TBBD_Product_type where YS_Product_Code like '"+Mar_code+"%'";
                    //sql_mar_type_other += str_where_other;
                    //System.Data.DataTable dt_mar_type_other = DBCallCommon.GetDTUsingSqlText(sql_mar_type_other);
                    //for (int j = 0; j < dt_mar_type_other.Rows.Count;j++ )
                    //{
                    //    if (j == 0)
                    //    {
                    //        strwhere += " and ( charindex('" + dt_mar_type_other.Rows[j]["YS_Product_Name"] + "',marnm)>0";
                    //    }
                    //    else
                    //        if (j == (dt_mar_type_other.Rows.Count - 1))
                    //    {
                    //        strwhere += " or charindex('" + dt_mar_type_other.Rows[j]["YS_Product_Name"] + "',marnm)>0 )";
                    //    }
                    //        else
                    //        {
                    //            strwhere += " or charindex('" + dt_mar_type_other.Rows[j]["YS_Product_Name"] + "',marnm)>0";
                    //        }
                    //}
                }
                else
                    if (Mar_code == "other")
                    {
                        string sql_type = "select YS_CODE from YS_COST_BUDGET_DETAIL where YS_TSA_ID='" + ContractNo + "' and YS_FATHER='OTHERMAT_COST' and YS_CODE!='other'";
                        System.Data.DataTable dt_type = DBCallCommon.GetDTUsingSqlText(sql_type);
                        //string str_type_other = "";
                        for (int i = 0; i < dt_type.Rows.Count; i++)
                        {
                            //str_type_other += " and charindex('" + dt_type.Rows[i]["YS_CODE"] + "',YS_Product_Code)=0";
                            strwhere += " and charindex('" + dt_type.Rows[i]["YS_CODE"] + "',marid)=0";
                        }
                        //string sql_type_other = "select YS_Product_Code from TBBD_Product_type where YS_Product_FatherCode='Other'";
                        //sql_type_other += str_type_other;
                        //System.Data.DataTable dt_type_other = DBCallCommon.GetDTUsingSqlText(sql_type_other);
                        //for (int j = 0; j < dt_type_other.Rows.Count; j++)
                        //{
                        //    if (j == 0)
                        //    {
                        //        strwhere += " and ( charindex('" + dt_type_other.Rows[j]["YS_Product_Code"] + "',marid)>0";
                        //    }
                        //    else if (j == (dt_type_other.Rows.Count - 1))
                        //    {
                        //        strwhere += " or charindex('" + dt_type_other.Rows[j]["YS_Product_Code"] + "',marid)>0 )";
                        //    }
                        //    else
                        //    {
                        //        strwhere += " or charindex('" + dt_type_other.Rows[j]["YS_Product_Code"] + "',marid)>0";
                        //    }
                        //}

                    }
            }
            return strwhere;
        }

        //protected string check_ddl_YS()
        //{
        //    Encrypt_Decrypt ed = new Encrypt_Decrypt();
        //    string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
        //    string Mar_code = Request.QueryString["Marcode"].ToString();
        //    string str_ys_name = "";
        //    if (Mar_code=="01.07")
        //    {
        //        if (ddl_YS_name.SelectedItem.Text.ToString()=="定尺板")
        //        {
        //            if (DropDownList_mar_type.SelectedItem.Text.ToString()!="定尺板")
        //            {
        //                str_ys_name = " and PO_MASHAPE='定尺板'";
        //            }
        //        }
        //        else
        //            if (ddl_YS_name.SelectedItem.Text.ToString() == "轨道系统")
        //            {
        //                str_ys_name = " and marnm like '%轨道%'";
        //            }
        //            else if (ddl_YS_name.SelectedItem.Text.ToString() == "普通材料")
        //        {
        //            str_ys_name = "  and charindex('轨道',marnm)=0  and PO_MASHAPE!='定尺板'";
        //        }
        //    }
        //    if (Mar_code=="01.11"||Mar_code=="01.08")
        //    {
        //            str_ys_name = "  and charindex('" + ddl_YS_name.SelectedItem.Text.ToString() + "',marnm)>0 ";
        //            string sql_contain = "select YS_Product_Name from TBBD_Product_type where YS_Product_Tag='1' " +
        //                        "and charindex('-',YS_Product_Code)>0 and YS_Product_Name like '%" + ddl_YS_name.SelectedItem.Text.ToString() + "%' " +
        //                        "and YS_Product_Name!='" + ddl_YS_name.SelectedItem.Text.ToString() + "'";
        //            System.Data.DataTable dt_contain = DBCallCommon.GetDTUsingSqlText(sql_contain);
        //            for (int s = 0; s < dt_contain.Rows.Count; s++)
        //            {
        //                str_ys_name += " and charindex('" + dt_contain.Rows[s]["YS_Product_Name"] + "',marnm)=0";
        //            }
        //    }
        //    if (Mar_code=="other")
        //    {
        //            string sql_mar_type = "select YS_Product_Code from TBBD_Product_type where YS_Product_Name='" + ddl_YS_name.SelectedItem.Text.ToString() + "'";
        //            System.Data.DataTable dt_mar_type = DBCallCommon.GetDTUsingSqlText(sql_mar_type);
        //            if (dt_mar_type.Rows.Count > 0)
        //            {
        //                str_ys_name += " and marid like '" + dt_mar_type.Rows[0][0].ToString() + "%'";
        //            }
        //    }
        //    return str_ys_name;
        //}

        //protected void bind_ddl_YSname()
        //{
        //    Encrypt_Decrypt ed = new Encrypt_Decrypt();
        //    string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
        //    string Mar_code = Request.QueryString["Marcode"].ToString();
        //    string name_type = "";
        //    switch (Mar_code)
        //    {
        //        case "01.07": name_type = "FERROUS_METAL"; break;
        //        case "01.11": name_type = "PURCHASE_PART"; break;
        //        case "01.08": name_type = "MACHINING_PART"; break;
        //        case "01.15": name_type = "PAINT_COATING"; break;
        //        case "01.18": name_type = "ELECTRICAL"; break;
        //        case "other": name_type = "OTHERMAT_COST"; break;
        //        default: break;
        //    }
        //    string sql_YS_NAME = "select distinct YS_NAME,YS_NAME from YS_COST_BUDGET_DETAIL where YS_CONTRACT_NO='" + ContractNo + "' and YS_FATHER='" + name_type + "' and YS_NAME!='其它'";
        //    DBCallCommon.FillDroplist(ddl_YS_name, sql_YS_NAME);
        //}

        protected void bind_ddl_mar()
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
            string Mar_code = Request.QueryString["Marcode"].ToString();
            switch (Mar_code)
            {
                case "01.07": YS_NAME.Text = "黑色金属"; break;
                case "01.11": YS_NAME.Text = "外购件"; break;
                case "01.08": YS_NAME.Text = "加工件"; break;
                case "01.15": YS_NAME.Text = "油漆涂料"; break;
                case "01.18": YS_NAME.Text = "电气电料"; break;
                case "other": YS_NAME.Text = "其他材料费"; break;
                default: break;
            }
            string sql_where = " engid='" + ContractNo + "'";
            //获取材料ID
            if (Mar_code != "other")
            {
                sql_where += " and marid like '" + Mar_code + "%'";
            }
            else
            {
                sql_where += " and charindex(substring(marid,1,5),'01.07.01.08.01.11.01.15.01.18')=0";
            }
            string sqltext_mar_type = "SELECT DISTINCT isnull(PO_MASHAPE,'无') ,PO_MASHAPE FROM View_TBPC_PURORDERDETAIL_PLAN_TOTAL where " + sql_where;
            DBCallCommon.FillDroplist(DropDownList_mar_type, sqltext_mar_type);
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            string shwhere = GetStrWhere();
            string sqltext = "orderno,supplierid,suppliernm,zdtime,cgtimerq,pjid,pjnm,engid,pjid+'/'+engnm AS engnm,whstate,convert(float,zxnum) as zxnum,recgdnum,detailstate,detailcstate,marunit,convert(float,ctamount) as ctamount," +
                         "zdrid,zdrnm,shrid,shrnm,totalnote,totalstate,totalcstate,ptcode,PO_TUHAO,marid,marnm,margg,marcz,margb,PO_MASHAPE,length,width,PO_ZJE,PO_CGFS,ctprice,recdate ";

            InitPager1("View_TBPC_PURORDERDETAIL_PLAN_TOTAL", "", sqltext, "zdtime desc,ptcode,marnm,margg", shwhere);
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Purordertotal_list_Repeater, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
                //CheckUser(ControlFinder);
            }

        }

        protected void Purordertotal_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sql_amount = "select sum(ctamount),sum(zxnum),sum(ctamount)/sum(zxnum) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where ";
                sql_amount += GetStrWhere();
                System.Data.DataTable dt_amount = DBCallCommon.GetDTUsingSqlText(sql_amount);
                for (int i = 0; i < 3; i++)
                {
                    if (dt_amount.Rows[0][i].ToString() == "")
                    {
                        dt_amount.Rows[0][i] = "0";
                    }
                }
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("YS_mar_amount"))).Text = Math.Round(Convert.ToDouble(dt_amount.Rows[0][0].ToString()), 2).ToString();
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("YS_mar_num"))).Text = Math.Round(Convert.ToDouble(dt_amount.Rows[0][1].ToString()), 2).ToString();
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("YS_average_price"))).Text = Math.Round(Convert.ToDouble(dt_amount.Rows[0][2].ToString()), 2).ToString();
            }
        }

        private void InitPager1(string tablename, string key, string showField, string orderField, string where)
        {
            pager.TableName = tablename;
            pager.PrimaryKey = key;
            pager.ShowFields = showField;
            pager.OrderField = orderField;
            pager.StrWhere = where;
            pager.OrderType = 0;//按时间降序排列
            pager.PageSize = 15;
            //pager.PageIndex = 1;
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        #region 导出EXCEL
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            string str_where = GetStrWhere();
            string sqltext = "";
            sqltext = "SELECT   orderno,salescontract, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, PO_MASHAPE,PO_ZJE,PO_CGFS,PO_CRCODE,PO_BILL," +
     "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
  "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
   "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalnote, fax, phono, conname, abstract, " +
    "PO_TUHAO, PO_OperateState from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where " + str_where + "order by orderno desc,ptcode asc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("预算采购订单明细表") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            System.Data.DataTable dt = objdt;

            // 填充数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wksheet.Cells[i + 3, 1] = Convert.ToString(i + 1);//序号

                wksheet.Cells[i + 3, 2] = "'" + dt.Rows[i]["orderno"].ToString();//订单编号

                wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["salescontract"].ToString();//销售合同号

                wksheet.Cells[i + 3, 4] = "'" + dt.Rows[i]["pjnm"].ToString();//项目/工程名称

                wksheet.Cells[i + 3, 5] = "'" + dt.Rows[i]["engnm"].ToString();//项目

                wksheet.Cells[i + 3, 6] = "'" + dt.Rows[i]["suppliernm"].ToString();//供应商

                wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["marid"].ToString();//物料编码

                wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["marnm"].ToString();//名称

                wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["margg"].ToString();//规格

                wksheet.Cells[i + 3, 10] = "'" + dt.Rows[i]["marcz"].ToString();//材质

                wksheet.Cells[i + 3, 11] = "'" + dt.Rows[i]["margb"].ToString();//国标

                wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["zxnum"].ToString();//数量

                wksheet.Cells[i + 3, 13] = "'" + dt.Rows[i]["marunit"].ToString();//单位

                wksheet.Cells[i + 3, 14] = "'" + dt.Rows[i]["ctprice"].ToString();//含税单价

                wksheet.Cells[i + 3, 15] = "'" + dt.Rows[i]["ctamount"].ToString();//含税金额

                wksheet.Cells[i + 3, 16] = "'" + dt.Rows[i]["PO_MASHAPE"].ToString();//类型

                wksheet.Cells[i + 3, 17] = "'" + dt.Rows[i]["length"].ToString();//长度

                wksheet.Cells[i + 3, 18] = "'" + dt.Rows[i]["width"].ToString();//宽度

                wksheet.Cells[i + 3, 19] = "'" + dt.Rows[i]["PO_TUHAO"].ToString();//图号/标识号

                wksheet.Cells[i + 3, 20] = "'" + dt.Rows[i]["PO_ZJE"].ToString();//总金额

                wksheet.Cells[i + 3, 21] = "'" + dt.Rows[i]["zdrnm"].ToString();//制单人

                wksheet.Cells[i + 3, 22] = "'" + dt.Rows[i]["zdtime"].ToString();//制单日期

                wksheet.Cells[i + 3, 23] = "'" + dt.Rows[i]["cgtimerq"].ToString();//交货日期

                wksheet.Cells[i + 3, 24] = "'" + dt.Rows[i]["PO_CGFS"].ToString();//质量报检

                wksheet.Cells[i + 3, 25] = "'" + dt.Rows[i]["PO_CRCODE"].ToString();//订单请款单号

                wksheet.Cells[i + 3, 26] = "'" + dt.Rows[i]["PO_BILL"].ToString();//订单发票

                wksheet.Cells[i + 3, 27] = "'" + dt.Rows[i]["recdate"].ToString();//实际到货日期

                wksheet.Cells[i + 3, 28] = "'" + dt.Rows[i]["totalstate"].ToString();//审核标志

                wksheet.Cells[i + 3, 29] = "'" + dt.Rows[i]["totalnote"].ToString();//备注

                wksheet.Cells[i + 3, 30] = "'" + dt.Rows[i]["ptcode"].ToString();//计划跟踪号

                wksheet.Cells[i + 3, 31] = "'" + dt.Rows[i]["recgdnum"].ToString();//到货数量

                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 31]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 31]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 31]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }
            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("../YS_Data/ExportFile/" + "预算采购订单明细表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();

                //下载

                System.IO.FileInfo path = new System.IO.FileInfo(filename);

                //同步，异步都支持
                HttpResponse contextResponse = HttpContext.Current.Response;
                contextResponse.Redirect(string.Format("../YS_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        protected void rab_daohuo_selectchanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        //查询
        protected void QueryButton_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }
    }
}
