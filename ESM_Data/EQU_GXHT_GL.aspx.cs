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
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;

namespace ZCZJ_DPF.ESM_Data
{
    public partial class EQU_GXHT_GL : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string userid = string.Empty;
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            userid = Session["UserID"].ToString();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                bindrpt();
            }
            if (username == "管理员")
            {
                btnBackCheck.Visible = true;
            }
            CheckUser(ControlFinder);
        }
        protected void rptGXHT_OnItemDataBound(object sender, RepeaterItemEventArgs e)//控制审批权限
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                HyperLink hplXiuGai = (HyperLink)e.Item.FindControl("hplXiuGai");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                HyperLink hplCheck = (HyperLink)e.Item.FindControl("hplCheck");
                hplCheck.Visible = false;
                hplXiuGai.Visible = false;
                btnDelete.Visible = false;
                //if (dr["HT_HTBH"].ToString() == "4")
                //{
                //    HT_HTBH.BgColor = System.Drawing.Color.LightBlue.ToString();
                //}
                if (dr["HT_SPZT"].ToString() == "0")
                {
                    if (userid == dr["HT_ZDRID"].ToString())
                    {
                        hplXiuGai.Visible = true;
                        btnDelete.Visible = true;
                    }
                }
                if (dr["HT_SPZT"].ToString() == "1")
                {
                    if (userid == dr["HT_SHR1ID"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                }
                if (dr["HT_SPZT"].ToString() == "1y")
                {
                    if (userid == dr["HT_SHR2ID"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                    if (userid == dr["HT_SHR3ID"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                }
                if (dr["HT_SPZT"].ToString() == "2.1y")
                {
                    if (userid == dr["HT_SHR3ID"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                }
                if (dr["HT_SPZT"].ToString() == "2.2y")
                {
                    if (userid == dr["HT_SHR2ID"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                }
                if (dr["HT_SPZT"].ToString() == "2y")
                {
                    if (userid == dr["HT_SHR4ID"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                }
                if (dr["HT_SPZT"].ToString() == "3y")
                {
                    if (userid == dr["HT_SHR5ID"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                }
            }
        }
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }
        private void BindrblSP()
        {
            string sql = "";
            DataTable dt = new DataTable();
            sql = "select count (HT_ID) from EQU_GXHT where HT_SPZT is not null";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[0].Text = string.Format("全部合同（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());

            sql = "select count (HT_ID) from EQU_GXHT where HT_SPZT ='1'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[1].Text = string.Format("待审批（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());

            sql = "select count (HT_ID) from EQU_GXHT where HT_SPZT in ('1y','2.1y','2.2y','2y','3y')";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[2].Text = string.Format("审批中（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());


            sql = "select count (HT_ID) from EQU_GXHT where HT_SPZT ='3'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[3].Text = string.Format("已通过（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());

            sql = "select count (HT_ID) from EQU_GXHT where HT_SPZT ='4'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[4].Text = string.Format("已驳回（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());

            sql = string.Format("select count (HT_ID) from EQU_GXHT where (HT_SHR1ID='{0}' and (HT_SHR1_JL is null or HT_SHR1_JL='') and HT_SPZT='1') or (HT_SHR2ID='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='1y') or (HT_SHR3ID='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='1y')or (HT_SHR2ID='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='2.2y')or (HT_SHR3ID='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='2.1y') or (HT_SHR4ID='{0}' and (HT_SHR4_JL is null or HT_SHR4_JL='') and HT_SPZT='2y') or (HT_SHR5ID='{0}' and (HT_SHR5_JL is null or HT_SHR5_JL='') and HT_SPZT='3y')", userid);
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[5].Text = string.Format("我的审批任务（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());
        }
        private string StrWhere()
        {
            string sql = "0=0";
            if (rblSP.SelectedValue == "1")//待审批
            {
                sql += " and HT_SPZT='1'";
            }
            else if (rblSP.SelectedValue == "2")//审批中
            {
                sql += " and HT_SPZT in ('1y','2.1y','2.2y','2y','3y')";
            }
            else if (rblSP.SelectedValue == "3")//已通过
            {
                sql += " and HT_SPZT='3'";
            }
            else if (rblSP.SelectedValue == "4")//已驳回
            {
                sql += " and HT_SPZT='4'";
            }
            if (rblSP.SelectedValue == "5")//我的审批任务
            {
                sql = string.Format("(HT_SHR1ID='{0}' and (HT_SHR1_JL is null or HT_SHR1_JL='') and HT_SPZT='1') or (HT_SHR2ID='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='1y') or (HT_SHR3ID='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='1y')or (HT_SHR2ID='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='2.2y') or (HT_SHR3ID='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='2.1y') or (HT_SHR4ID='{0}' and (HT_SHR4_JL is null or HT_SHR4_JL='') and HT_SPZT='2y') or (HT_SHR5ID='{0}' and (HT_SHR5_JL is null or HT_SHR5_JL='') and HT_SPZT='3y')", userid);
            }
            if (txtHTBH.Text.Trim() != "")
            {
                sql += " and HT_HTBH like'%" + txtHTBH.Text.Trim() + "%'";
            }
            if (txtGYS.Text.Trim() != "")
            {
                sql += " and HT_GF like '%" + txtGYS.Text.Trim() + "%'";
            }
            if (txtQSSJ.Text.Trim() != "")
            {
                sql += " and HT_ZDSJ >= '" + txtQSSJ.Text.Trim() + "'";
            }
            if (txtJZSJ.Text.Trim() != "")
            {
                sql += " and HT_ZDSJ <= '" + txtJZSJ.Text.Trim() + "'";
            }
            return sql;
        }
        private int PageSize()
        {
            int pagesize = Convert.ToInt32(ddlRowCount.SelectedItem.Text);
            return pagesize;
        }
        private void bindrpt()
        {
            pager_org.TableName = "EQU_GXHT";
            pager_org.PrimaryKey = "HT_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "HT_HTBH";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = PageSize();
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptGXHT, UCPaging1, NoDataPane);
            int num = 0;
            double money = 0;
            foreach (RepeaterItem item in rptGXHT.Controls)
            {
                if (item.ItemType == ListItemType.Footer)
                {
                    string sql = "select HT_ID,HT_HTBH,HT_HTZJ,HT_GF,HT_DH from EQU_GXHT where " + pager_org.StrWhere;
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    foreach (DataRow dr in dt1.Rows)
                    {
                        string[] a = dr["HT_HTZJ"].ToString().Split(new char[] { '(', '元' }, StringSplitOptions.None);
                        for (int i = 0, length = a.Length; i < length; i++)
                        {
                            money += CommonFun.ComTryDouble(a[i]);
                        }
                    }
                    num = dt1.Rows.Count;
                    ((Label)item.FindControl("lbNUM")).Text = num.ToString();
                    ((Label)item.FindControl("lbMONEY")).Text = money.ToString();
                    //if (num <= 1)
                    //{
                    //    btnBatchDelete.Visible = false;
                    //    quanxianKJ.Visible = false;
                    //}
                    break;
                }
            }
            for (int j = 0; j < rptGXHT.Items.Count; j++)
            {
                Label s = (Label)rptGXHT.Items[j].FindControl("lblXuHao");
                s.Text = (j + 1 + (pager_org.PageIndex - 1) * UCPaging1.PageSize).ToString();
            }
            if (NoDataPane.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            BindrblSP();
        }
        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        # region 导出功能
        protected void btnDaoChu_OnClick(object sender, EventArgs e)
        {
            string ht_bh = "";
            string sql = "";
            for (int i = 0; i < rptGXHT.Items.Count; i++)
            {
                if (((CheckBox)rptGXHT.Items[i].FindControl("cbxXuHao")).Checked == true)
                {
                    ht_bh = ((Label)rptGXHT.Items[i].FindControl("lbHT_HTBH")).Text;
                    sql = "select * from EQU_GXHT left join EQU_GX_Detail on EQU_GXHT.HT_HTBH=EQU_GX_Detail.EQU_FATHERID where HT_HTBH='" + ht_bh + "'order by EQU_GXHT.HT_HTBH";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    ExportDataItem(dt);
                    break;
                }
                else
                {
                    Response.Write("<script>alert('请勾选要导出的合同！')</script>");
                    return;
                }
            }
        }
        private void ExportDataItem(System.Data.DataTable dt)
        {
            int rptcount = dt.Rows.Count;
            DataRow dr = dt.Rows[0];
            string filename = "设备购销合同--" + dr["HT_GF"].ToString() + dr["HT_QDSJ"].ToString() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("设备购销合同.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 6, rptcount + 6, 1, 7));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 7, rptcount + 7, 1, 7));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 8, rptcount + 21, 0, 7));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 22, rptcount + 22, 0, 4));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 22, rptcount + 22, 5, 7));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 23, rptcount + 23, 0, 1));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 23, rptcount + 23, 2, 4));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 23, rptcount + 23, 5, 6));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 24, rptcount + 24, 0, 1));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 24, rptcount + 24, 2, 4));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 24, rptcount + 24, 5, 6));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 25, rptcount + 25, 0, 1));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 25, rptcount + 25, 2, 4));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 25, rptcount + 25, 5, 6));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 26, rptcount + 26, 0, 1));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 26, rptcount + 26, 2, 4));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 26, rptcount + 26, 5, 6));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 27, rptcount + 27, 0, 1));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 27, rptcount + 27, 2, 4));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 27, rptcount + 27, 5, 6));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 28, rptcount + 28, 0, 1));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 28, rptcount + 28, 2, 4));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 28, rptcount + 28, 5, 6));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 29, rptcount + 29, 0, 1));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 29, rptcount + 29, 2, 4));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 29, rptcount + 29, 5, 6));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 30, rptcount + 30, 0, 1));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 30, rptcount + 30, 2, 4));
                sheet0.AddMergedRegion(new CellRangeAddress(rptcount + 30, rptcount + 30, 5, 6));

                IRow row2 = sheet0.GetRow(2);//合同编号
                row2.GetCell(6).SetCellValue(dr["HT_HTBH"].ToString());

                IRow row3 = sheet0.GetRow(3);//卖方、签订时间
                row3.GetCell(1).SetCellValue(dr["HT_GF"].ToString());

                row3.GetCell(6).SetCellValue(dr["HT_QDSJ"].ToString().Substring(0, 4) + "年" + dr["HT_QDSJ"].ToString().Substring(5, 2) + "月" + dr["HT_QDSJ"].ToString().Substring(8, 2) + "日");

                for (int i = 6; i <= rptcount + 5; i++)
                {
                    IRow rowi = sheet0.GetRow(i);
                    rowi.CreateCell(7);
                    sheet0.AddMergedRegion(new CellRangeAddress(i, i, 1, 2));
                    rowi.GetCell(0).SetCellValue(dr["EQU_Type"].ToString());//设备名称
                    rowi.GetCell(1).SetCellValue(dr["EQU_Name"].ToString());//设备分项名称、规格、型号
                    rowi.GetCell(3).SetCellValue(dr["EQU_Unit"].ToString());//单位
                    rowi.GetCell(4).SetCellValue(dr["EQU_Num"].ToString());//数量
                    rowi.GetCell(5).SetCellValue(dr["EQU_UPrice"].ToString());//单价
                    rowi.GetCell(6).SetCellValue(dr["EQU_TMoney"].ToString());//合计金额
                    rowi.GetCell(7).SetCellValue(dr["EQU_Note"].ToString());//备注
                }

                IRow rowa = sheet0.GetRow(rptcount + 6);
                rowa.GetCell(0).SetCellValue("总金额：");
                rowa.GetCell(1).SetCellValue(dr["HT_HTZJ"].ToString());//总金额

                IRow rowb = sheet0.GetRow(rptcount + 7);
                rowb.GetCell(0).SetCellValue("交货时间：");
                rowb.GetCell(1).SetCellValue(dr["HT_JHSJ"].ToString());//交货时间

                IRow rowc = sheet0.GetRow(rptcount + 8);
                rowc.GetCell(0).CellStyle.WrapText = true;
                rowc.GetCell(0).CellStyle.Alignment = HorizontalAlignment.LEFT;
                //rowc.GetCell(0).CellStyle.VerticalAlignment = VerticalAlignment.TOP;
                rowc.GetCell(0).SetCellValue(dr["HT_TK"].ToString());//合同条款

                IRow rowd = sheet0.GetRow(rptcount + 22);
                rowd.GetCell(0).SetCellValue("需方：");//需方
                rowd.GetCell(5).SetCellValue("供方：");//供方

                IRow rowe = sheet0.CreateRow(rptcount + 23);
                rowe.CreateCell(0).SetCellValue("单位名称：");
                rowe.CreateCell(2).SetCellValue("中材（天津）重型机械有限公司");
                rowe.CreateCell(5).SetCellValue("单位名称：");
                rowe.CreateCell(7).SetCellValue(dr["HT_DWMC"].ToString());//单位名称


                IRow rowf = sheet0.CreateRow(rptcount + 24);
                rowf.CreateCell(0).SetCellValue("法人代表：");
                rowf.CreateCell(2).SetCellValue("彭明德");
                rowf.CreateCell(5).SetCellValue("法定代表人：");
                rowf.CreateCell(7).SetCellValue(dr["HT_FDDBR"].ToString());//法定代表人

                IRow rowg = sheet0.CreateRow(rptcount + 25);
                rowg.CreateCell(0).SetCellValue("开户银行：");
                rowg.CreateCell(2).SetCellValue("工商银行天津市双原道支行");
                rowg.CreateCell(5).SetCellValue("开户银行：");
                rowg.CreateCell(7).SetCellValue(dr["HT_KHYH"].ToString());//开户银行

                IRow rowh = sheet0.CreateRow(rptcount + 26);
                rowh.CreateCell(0).SetCellValue("账号：");
                rowh.CreateCell(2).SetCellValue("302035309102231000");
                rowh.CreateCell(5).SetCellValue("账号：");
                rowh.CreateCell(7).SetCellValue(dr["HT_ZH"].ToString());//账号

                IRow rowj = sheet0.CreateRow(rptcount + 27);
                rowj.CreateCell(0).SetCellValue("电话：");
                rowj.CreateCell(2).SetCellValue("022-86890126");
                rowj.CreateCell(5).SetCellValue("电话：");
                rowj.CreateCell(7).SetCellValue(dr["HT_DH"].ToString());//电话

                IRow rowk = sheet0.CreateRow(rptcount + 28);
                rowk.CreateCell(0).SetCellValue("传真：");
                rowk.CreateCell(2).SetCellValue("022-86890156");
                rowk.CreateCell(5).SetCellValue("传真：");
                rowk.CreateCell(7).SetCellValue(dr["HT_CZ"].ToString());//传真

                IRow rowl = sheet0.CreateRow(rptcount + 29);
                rowl.CreateCell(0).SetCellValue("地址：");
                rowl.CreateCell(2).SetCellValue("天津市北辰引河里北道1号");
                rowl.CreateCell(5).SetCellValue("地址：");
                rowl.CreateCell(7).SetCellValue(dr["HT_DZ"].ToString());//地址

                IRow rowm = sheet0.CreateRow(rptcount + 30);
                rowm.CreateCell(0).SetCellValue("邮编：");
                rowm.CreateCell(2).SetCellValue("300400");
                rowm.CreateCell(5).SetCellValue("邮编：");
                rowm.CreateCell(7).SetCellValue(dr["HT_YB"].ToString());//邮编

                sheet0.ForceFormulaRecalculation = true;


                for (int n = rptcount + 22; n <= rptcount + 30; n++)
                {
                    sheet0.GetRow(n).Height = 20 * 20;
                    sheet0.GetColumnStyle(7).Alignment = HorizontalAlignment.CENTER;
                    //sheet0.GetColumnStyle(7).VerticalAlignment = VerticalAlignment.CENTER;
                    sheet0.GetRow(n).GetCell(0).CellStyle.Alignment = HorizontalAlignment.CENTER;
                    sheet0.GetRow(n).GetCell(2).CellStyle.Alignment = HorizontalAlignment.CENTER;
                    sheet0.GetRow(n).GetCell(5).CellStyle.Alignment = HorizontalAlignment.CENTER;
                }

                NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                font1.FontName = "宋体";//字体
                font1.FontHeightInPoints = 11;//字号
                ICellStyle cells = wk.CreateCellStyle();
                cells.SetFont(font1);
                cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        # endregion
        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            txtHTBH.Text = "";
            txtGYS.Text = "";
            txtQSSJ.Text = "";
            txtJZSJ.Text = "";
        }
        protected void btnDelete_OnClick(object sender, EventArgs e)//删除事件
        {
            List<string> list = new List<string>();
            string id = ((LinkButton)sender).CommandArgument.ToString();
            string sql = "delete from EQU_GXHT where HT_HTBH='" + id + "'";
            list.Add(sql);
            sql = "delete from EQU_GX_Detail where EQU_FATHERID='" + id + "'";
            list.Add(sql);
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                Response.Write("<script>alert('删除失败，请联系管理员！')</script>");
                return;
            }
            Response.Write("<script>alert('该合同已成功删除！')</script>");
            bindrpt();
        }
        protected void btnBatchDelete_OnClick(object sender, EventArgs e)//批量删除事件
        {
            List<string> list = new List<string>();
            int times = 0;
            string ht_bh = "";
            for (int i = 0, length = rptGXHT.Items.Count; i < length; i++)
            {
                if (((CheckBox)rptGXHT.Items[i].FindControl("cbxXuHao")).Checked == true && ((Label)rptGXHT.Items[i].FindControl("lbHT_SPZT")).Text == "初始化")
                {
                    ht_bh = ((Label)rptGXHT.Items[i].FindControl("lbHT_HTBH")).Text;
                    string sql = "delete from EQU_GXHT where HT_HTBH='" + ht_bh + "'";
                    list.Add(sql);
                    string sql1 = "delete from EQU_GX_Detail where EQU_FATHERID='" + ht_bh + "'";
                    list.Add(sql1);
                    times++;
                }
            }
            if (times == 0)
            {
                Response.Write("<script>alert('新增合同一经提交不可删除，请勾选要删除的初始化合同！')</script>");
                return;
            }
            else
            {
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    Response.Write("<script>alert('选中的初始化合同已成功删除！')</script>");
                    bindrpt();
                }
                catch
                {
                    Response.Write("<script>alert('数据操作出现问题，请联系管理员！')</script>");
                }
            }
        }
        protected void btnBackCheck_OnClick(object sender, EventArgs e)//返审事件
        {
            string ht_bh = "";
            int times = 0;
            for (int i = 0, length = rptGXHT.Items.Count; i < length; i++)
            {
                if (((CheckBox)rptGXHT.Items[i].FindControl("cbxXuHao")).Checked == true)
                {
                    ht_bh = ((Label)rptGXHT.Items[i].FindControl("lbHT_HTBH")).Text;
                    string sql = "update EQU_GXHT set  ";
                    string sqltext = "select * from EQU_GXHT where HT_HTBH='" + ht_bh + "'and HT_SPZT='3'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["HT_SPLX"].ToString() == "1")
                        {
                            sql += "HT_SHR1_JL='',HT_SHR1_SJ='',HT_SHR1_JY='',";
                        }
                        if (dt.Rows[0]["HT_SPLX"].ToString() == "2")
                        {
                            sql += "HT_SHR1_JL='',HT_SHR1_SJ='',HT_SHR1_JY='',";
                            sql += "HT_SHR2_JL='',HT_SHR2_SJ='',HT_SHR2_JY='',";
                        }
                        if (dt.Rows[0]["HT_SPLX"].ToString() == "3")
                        {
                            sql += "HT_SHR1_JL='',HT_SHR1_SJ='',HT_SHR1_JY='',";
                            sql += "HT_SHR2_JL='',HT_SHR2_SJ='',HT_SHR2_JY='',";
                            sql += "HT_SHR3_JL='',HT_SHR3_SJ='',HT_SHR3_JY='',";
                        }
                        //if (dt.Rows[0]["HT_SPLX"].ToString() == "4")
                        //{
                        sql += "HT_SHR1_JL='',HT_SHR1_SJ='',HT_SHR1_JY='',";
                        sql += "HT_SHR2_JL='',HT_SHR2_SJ='',HT_SHR2_JY='',";
                        sql += "HT_SHR3_JL='',HT_SHR3_SJ='',HT_SHR3_JY='',";
                        sql += "HT_SHR4_JL='',HT_SHR4_SJ='',HT_SHR4_JY='',";
                        sql += "HT_SHR5_JL='',HT_SHR5_SJ='',HT_SHR5_JY='',";
                        //}
                        sql += "HT_SPZT='0'";
                        sql += " where HT_HTBH='" + ht_bh + "'";
                        DBCallCommon.ExeSqlText(sql);
                        times++;
                    }
                }
            }
            Response.Write("<script>alert( '" + times + "份已通过合同被返审')</script>");
            bindrpt();
        }
    }
}
