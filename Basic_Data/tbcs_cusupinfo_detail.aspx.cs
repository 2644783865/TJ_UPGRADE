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
using ZCZJ_DPF;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class tbcs_cusupinfo_detail : BasicPage
    {
        //protected string ty_zy_change = string.Empty;

        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {


            InitVar();
            if (!IsPostBack)
            {
                //this.GetLocationData();
                //this.GetLocationNextData();
                bindGrid();
            }
            CheckUser(ControlFinder);

        }

        #region "数据分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "TBCS_CUSUPINFO";
            pager.PrimaryKey = "CS_ID";
            pager.ShowFields = "CS_ID,CS_CODE,CS_NAME,CS_HRCODE,CS_ADDRESS,CS_PHONO,CS_COREBS,CS_MANCLERK,CS_MCODE,CS_Scope,CS_State, + case when  CS_TYPE=1 then '客户' when CS_TYPE=2 then '采购供应商' when CS_TYPE=3 then '运输公司'  when CS_TYPE=4 then '技术外协分包商' when CS_TYPE=5 then '生产外协分包商' when CS_TYPE=6 then '原材料销售供应商'  when CS_TYPE=7 then '其它' end AS CS_TYPE,CS_FILLDATE,case when kehutype='1' then '装备集团内' when kehutype='2' then '装备集团外' when kehutype='3' then '自营' else '' end as kehutype";
            pager.OrderField = "CS_ID";
            pager.StrWhere = Strwhere();
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            GetQueryData();
        }

        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, rptTBCS_CUSUPINFO, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
                Panel1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                Panel1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
                //CheckUser(ControlFinder);
            }
        }
        private string Strwhere()
        {
            string strwhere = string.Empty;
            // Resit_cbxAllQuery();
            if ((dopCS_Type.SelectedIndex == 0) && (drpkhtype.SelectedIndex == 0) && (tb_csname.Text == "") && (sfjy.SelectedIndex == 0)) //全部数据
            {
                strwhere = "CS_State=0";
                //   ty_zy_change = "停用";
            }
            else if ((dopCS_Type.SelectedIndex == 0) && (drpkhtype.SelectedIndex == 0) && (tb_csname.Text == "") && (sfjy.SelectedIndex == 1))
            {
                strwhere = "CS_State=1";
                //  ty_zy_change = "启用";

            }
            else
            {
                strwhere = GetMultiQueryString(); //按综合查询条件查询
            }

            return strwhere;
        }

        private void GetQueryData()
        {
            InitPager();
            bindGrid();
        }
        #endregion

        //#region "DropDownList数据绑定"       

        ///// <summary>
        ///// 从地区信息表中绑定地区信息
        ///// </summary>
        //private void GetLocationData()
        //{
        //    string sqltext = "select distinct CL_NAME,CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='ROOT'";
        //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //    dopLocation.Items.Add(new ListItem("-全部-", "%"));
        //    while (dr.Read())
        //    {
        //        dopLocation.Items.Add(new ListItem(dr["CL_NAME"].ToString(), dr["CL_CODE"].ToString()));
        //    }
        //    dr.Close();
        //    dopLocation.SelectedIndex = 0;
        //}
        ///// <summary>
        ///// 从地区表中绑定二级地区信息
        ///// </summary>
        //private void GetLocationNextData()
        //{
        //    dopLocationNext.Items.Clear();
        //    dopLocationNext.Items.Add(new ListItem("-全部-", "%"));
        //    if (dopLocation.SelectedIndex != 0)
        //    {
        //        string fathercode = dopLocation.SelectedValue;
        //        string sqltext = "select distinct CL_NAME from TBCS_LOCINFO where CL_FATHERCODE='" + fathercode + "'";
        //        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //        while (dr.Read())
        //        {
        //            dopLocationNext.Items.Add(new ListItem(dr["CL_NAME"].ToString(), dr["CL_NAME"].ToString()));
        //        }
        //        dr.Close();
        //    }
        //    dopLocationNext.SelectedIndex = 0;
        //}

        //#endregion



        //厂商类别
        //protected void dopCS_Type_TextChanged(object sender, EventArgs e)
        //{
        //    UCPaging1.CurrentPage = 1;
        //    //this.Resit_cbxAllQuery();
        //    //this.GetMultiQueryString();
        //   this.GetQueryData();
        //}

        ////一级地区
        //protected void dopLocation_TextChanged(object sender, EventArgs e)
        //{
        //    UCPaging1.CurrentPage = 1;
        //    this.GetLocationNextData();
        //    //this.Resit_cbxAllQuery();

        //    this.GetQueryData(this.GetMultiQueryString());
        //}

        ////二级地区
        //protected void dopLocationNext_TextChanged(object sender, EventArgs e)
        //{

        //    UCPaging1.CurrentPage = 1;
        //    this.Resit_cbxAllQuery();
        //    this.GetQueryData(this.GetMultiQueryString());
        //}
        //protected void sfjy_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UCPaging1.CurrentPage = 1;
        //    this.Resit_cbxAllQuery();
        //    this.GetQueryData(this.GetMultiQueryString());
        //}
        /// <summary>
        /// 获取综合查询字符串
        /// </summary>
        /// <returns></returns>
        private string GetMultiQueryString()
        {
            string cs_type_text = dopCS_Type.SelectedValue.ToString();

            ////判断地区是否选择全部，因为要根据Value值查找二级地区
            //string cl_name_text = "";
            //if (dopLocation.SelectedIndex == 0)
            //{
            //    cl_name_text = dopLocation.SelectedValue + dopLocationNext.SelectedValue;
            //}
            //else
            //{
            //    cl_name_text = dopLocation.SelectedItem.Text + dopLocationNext.SelectedValue;
            //}
            string cs_name_text = tb_csname.Text;
            string str_Condition = "CS_TYPE like'" + cs_type_text + "%' AND (CS_NAME LIKE '%" + cs_name_text + "%' or  CS_HRCODE like '%" + cs_name_text + "%')";
            if (sfjy.SelectedIndex == 0)
            {
                str_Condition = str_Condition + "AND CS_State=0";
            }
            else if (sfjy.SelectedIndex == 1)
            {
                str_Condition = str_Condition + " AND CS_State=1";
            }
            if (drpkhtype.SelectedIndex != 0)
            {
                str_Condition = str_Condition + " AND kehutype is not null and kehutype='" + drpkhtype.SelectedValue.Trim() + "'";
            }
            return str_Condition;
        }


        //停用厂商
        protected void delete_Click(object sender, CommandEventArgs e)
        {
            string id = e.CommandName;
            // string sql = "select * from TBCS_CUSUP_ADD_DELETE where ID='" + id + "' and CS_ACTION='1'";
            //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('该厂商已提交过停用申请，请查看！！！');window.close();", true);
            //}
            string sql = "select * from TBCS_CUSUPINFO where cs_code='" + id + "' ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (sfjy.SelectedIndex==0)
            {
               // Response.Write("<script>javascript:window.showModalDialog('tbcs_cusup_add_delete.aspx?action=Delete&cs_action=1&id=" + id + "','','DialogWidth=1100px;DialogHeight=1000px')</script>");
                if (dt.Rows[0]["CS_TYPE"].ToString() == "2")
                {
                    Response.Redirect("tbcs_cusup_add_delete.aspx?action=Delete&cs_action=1&id=" + id + "");
                }
                else
                {
                    Response.Redirect("tbcs_cusup_add_delete_old.aspx?action=Delete&cs_action=1&id=" + id + "");
                }
            
            }
            else if (sfjy.SelectedIndex == 1)
            {
                // Response.Write("<script>javascript:window.showModalDialog('tbcs_cusup_add_delete.aspx?action=Delete&cs_action=1&id=" + id + "','','DialogWidth=1100px;DialogHeight=1000px')</script>");
                if (dt.Rows[0]["CS_TYPE"].ToString() == "2")
                {
                     Response.Redirect("tbcs_cusup_add_delete.aspx?action=Delete&cs_action=0&id=" + id + "");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('该功能是针对采购供应商再起启用的功能，您无法进行此项操作！！！');window.close();", true);
                }

            } 



        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetQueryData();
        }
        protected string editCs(string CsId)
        {
            return "javascript:void(window.open('tbcs_cusupinfo_operate.aspx?action=update&id=" + CsId + "'))";
        }

        protected void dopCS_Type_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetQueryData();
        }
        protected void drpkhtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetQueryData();
        }

        protected void sfjy_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetQueryData();
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("tbcs_cusupinfo_detail.aspx");
        }




        //导出
        protected void bt_daochu_click(object sender, EventArgs e)
        {
            string sqlgys = "select CS_NAME,CS_ADDRESS,CS_PHONO,CS_FAX,CS_CONNAME,CS_COREBS,CS_MCODE,CS_Bank,CS_Account,CS_TAX, + case when  CS_TYPE=1 then '客户' when CS_TYPE=2 then '采购供应商' when CS_TYPE=3 then '运输公司'  when CS_TYPE=4 then '技术外协分包商' when CS_TYPE=5 then '生产外协分包商' when CS_TYPE=6 then '原材料销售供应商'  when CS_TYPE=7 then '其它' end AS CS_TYPE,CS_FILLDATE,CS_MANCLERK,CS_NOTE from TBCS_CUSUPINFO where " + Strwhere();
            System.Data.DataTable dtgys = DBCallCommon.GetDTUsingSqlText(sqlgys);
            string filename = "供应商列表.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("供应商列表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtgys.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 2);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dtgys.Columns.Count; j++)
                    {
                        string str = dtgys.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtgys.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }

        public string addcharge()
        {
            //string UserDeptID = Session["UserDeptID"].ToString();
            string positionid = Session["POSITION"].ToString();
         
            // UserDeptID"05"即"采购部"
            //2018.3.28修改为当人员职位id为采购员、采购主管、统计员
            if (positionid == "0712" || positionid == "0713" || positionid == "0714")
            {
                return "javascript:void window.open('tbcs_cusup_add_delete.aspx?action=Add&amp;cs_action=0','','')";
            }
            else
            {
                return "javascript:void window.open('tbcs_cusup_add_delete_old.aspx?action=Add&cs_action=0','','')";
            }
        }
    }
}
