using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ExpressManage : System.Web.UI.Page
    {
        string stid = "";
        List<string> codeList = new List<string>();
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            stid = Session["UserId"].ToString();
            if (!IsPostBack)
            {
                databind();
            }
            //CheckUser(ControlFinder);
        }

        #region 初始化分页
        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            pager.TableName = "OM_Express";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "*";
            pager.OrderField = "E_Code";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, rptExpress, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            //序号列翻页自增
            for (int j = 0; j < rptExpress.Items.Count; j++)
            {
                Label s = (Label)rptExpress.Items[j].FindControl("lbXuhao");
                s.Text = (j + 1 + (pager.PageIndex - 1) * UCPaging1.PageSize).ToString();

                //获取HeadTemplate里面的控件
                //repeaterId.controls[0].findControl('控件ID')
                //获取FooterTemplate里面的控件
                //repeaterId.controls[repeaterId.controls.count-1].findControl('控件ID')

                HtmlControl edit = rptExpress.Controls[0].FindControl("edit") as HtmlControl;
                HtmlTableCell audit = rptExpress.Controls[0].FindControl("audit") as HtmlTableCell;
                HtmlTableCell sure = rptExpress.Controls[0].FindControl("sure") as HtmlTableCell;
                HtmlTableCell delete = rptExpress.Controls[0].FindControl("delete") as HtmlTableCell;

                HtmlTableCell tdedit = rptExpress.Items[j].FindControl("tdedit") as HtmlTableCell;
                HtmlTableCell tdaudit = rptExpress.Items[j].FindControl("tdaudit") as HtmlTableCell;
                HtmlTableCell tdsure = rptExpress.Items[j].FindControl("tdsure") as HtmlTableCell;
                HtmlTableCell tddelete = rptExpress.Items[j].FindControl("tddelete") as HtmlTableCell;

                edit.Visible = false;
                audit.Visible = false;
                sure.Visible = false;
                delete.Visible = false;

                tdedit.Visible = false;
                tdaudit.Visible = false;
                tdsure.Visible = false;
                tddelete.Visible = false;
                if (rblState.SelectedValue == "0" || rblState.SelectedValue == "3")
                {
                    edit.Visible = true;
                    delete.Visible = true;
                    tdedit.Visible = true;
                    tddelete.Visible = true;
                }
                else if (rblState.SelectedValue == "1")
                {
                    audit.Visible = true;
                    tdaudit.Visible = true;
                }
                else if (rblState.SelectedValue == "2")
                {
                    sure.Visible = true;
                    tdsure.Visible = true;
                }
                else if (rblState.SelectedValue == "5")
                {
                    edit.Visible = true;
                    audit.Visible = true;
                    sure.Visible = true;
                    delete.Visible = true;
                    tdedit.Visible = true;
                    tdaudit.Visible = true;
                    tdsure.Visible = true;
                    tddelete.Visible = true;
                }
            }
            //string sqlhj = "select  sum(cast(isnull(E_ExpressMoney,'0') as decimal(12,2))) from OM_Express where " + strWhere();
            string sqlhj = "select E_Code,E_ExpressMoney from OM_Express where " + strWhere();
            DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);
            if (dthj.Rows.Count > 0)
            {
                divHJ.Visible = true;
                lbCount.Text = dthj.Rows.Count.ToString();
                decimal totalmoney = 0;
                for (int i = 0; i < dthj.Rows.Count; i++)
                {
                    totalmoney += CommonFun.ComTryDecimal(dthj.Rows[i][1].ToString());
                }
                lbTotalMoney.Text = totalmoney.ToString();
            }
            else
                divHJ.Visible = false;
        }

        private string strWhere()
        {
            string strWhere = "1=1 ";

            if (txtE_SQR.Text.Trim() != "")
            {
                strWhere += " and E_SQR like '%" + txtE_SQR.Text.Trim() + "%'";
            }
            if (txtE_ZDTimeS.Text.Trim() != "")
            {
                strWhere += " and E_ZDTime >= '" + txtE_ZDTimeS.Text.Trim() + "%'";
            }
            if (txtE_ZDTimeE.Text.Trim() != "")
            {
                strWhere += " and E_ZDTime <= '" + txtE_ZDTimeE.Text.Trim() + "%'";
            }
            if (txtE_ExpressCode.Text.Trim() != "")
            {
                strWhere += " and E_ExpressCode like '%" + txtE_ExpressCode.Text.Trim() + "%'";
            }
            if (ddlE_Company.SelectedValue != "")
            {
                strWhere += " and E_ExpressCompany like '" + ddlE_Company.SelectedValue + "%'";
            }
            if (ddlE_Type.SelectedValue != "")
            {
                strWhere += " and E_Type like '" + ddlE_Type.SelectedValue + "%'";
            }
            if (rblState.SelectedValue == "0")
            {
                strWhere += " and E_State='0'";
            }
            else if (rblState.SelectedValue == "1")
            {
                strWhere += " and E_State='1'";
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and E_State ='2'";
            }
            else if (rblState.SelectedValue == "3")
            {
                strWhere += " and (E_State ='3'or E_State ='5')";
            }
            else if (rblState.SelectedValue == "4")
            {
                strWhere += " and E_State ='4'";
            }
            else if (rblState.SelectedValue == "5")
            {
                strWhere += " and (((E_State ='0'or E_State ='3' or E_State ='5') and E_ZDRID='" + Session["UserId"].ToString() + "') or (E_State ='1' and E_SHRID='" + Session["UserId"].ToString() + "') or ( E_State='2' and E_SurerID='" + Session["UserId"].ToString() + "'))  ";
            }

            return strWhere;
        }

        #endregion

        protected void rptExpress_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;//以备调用显示数据库字段

                string code = ((Label)e.Item.FindControl("lbE_Code")).Text;
                HyperLink hpView = e.Item.FindControl("hpView") as HyperLink;
                HyperLink hpEdit = e.Item.FindControl("hpEdit") as HyperLink;
                HyperLink hpAudit = e.Item.FindControl("hpAudit") as HyperLink;
                HyperLink hpSure = e.Item.FindControl("hpSure") as HyperLink;
                LinkButton lkbDelete = e.Item.FindControl("lkbDelete") as LinkButton;

                hpEdit.Visible = false;
                hpAudit.Visible = false;
                hpSure.Visible = false;
                lkbDelete.Visible = false;

                if (codeList.Contains(code))
                {
                    ((Label)e.Item.FindControl("lbE_Code")).Text = "";
                    hpView.Visible = false;
                    hpEdit.Visible = false;
                    hpAudit.Visible = false;
                    hpSure.Visible = false;
                    lkbDelete.Visible = false;
                }
                else
                    codeList.Add(code);

                if (dr["E_State"].ToString() == "0")//未提交
                {
                    if (stid == dr["E_ZDRID"].ToString())
                    {
                        hpEdit.Visible = true;
                        lkbDelete.Visible = true;
                    }
                }
                else if (dr["E_State"].ToString() == "1")//审核中
                {
                    if (stid == dr["E_SHRID"].ToString())
                    {
                        hpAudit.Visible = true;
                    }
                }
                else if (dr["E_State"].ToString() == "2")//已通过
                {
                    if (stid == dr["E_SurerID"].ToString())
                    {
                        hpSure.Visible = true;
                    }
                }
                else if (dr["E_State"].ToString() == "3" || dr["E_State"].ToString() == "5")//已驳回
                {
                    if (stid == dr["E_SurerID"].ToString() && dr["E_State"].ToString() == "5")
                    {
                        lkbDelete.Visible = true;
                    }
                    else if (stid == dr["E_ZDRID"].ToString())
                    {
                        hpEdit.Visible = true;
                    }
                }
                else if (rblState.SelectedValue == "5")//我的审核任务
                {
                    if (dr["E_State"].ToString() == "0")
                    {
                        hpEdit.Visible = true;
                        lkbDelete.Visible = true;
                    }
                    else if (dr["E_State"].ToString() == "1")
                    {
                        hpAudit.Visible = true;
                    }
                    else if (dr["E_State"].ToString() == "2")
                    {
                        hpSure.Visible = true;
                    }
                    else if (dr["E_State"].ToString() == "3")
                    {
                        hpEdit.Visible = true;
                    }
                    else if (dr["E_State"].ToString() == "5")
                    {
                        if (stid == dr["E_SurerID"].ToString())
                            lkbDelete.Visible = true;
                        if (stid == dr["E_ZDRID"].ToString())
                            hpEdit.Visible = true;
                    }
                }
            }
        }

        protected void Query(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            databind();
        }

        protected void lkbDelete_OnClick(object sender, EventArgs e)
        {
            string context = ((LinkButton)sender).CommandName;
            string sql = "delete from OM_Express where E_Code='" + context + "'";
            try
            {
                DBCallCommon.ExeSqlText(sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('单号为" + context.Trim() + "的快递申请单已删除！');location.href='OM_ExpressManage.aspx';", true);
            }
            catch
            {
                Response.Write("<script>alert('删除失败，请联系管理员！')</script>");
            }
        }

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            string sqltext = "select E_Code,E_SQR,case when E_Type='0'then '文件' else '物品类' end,case when E_Type='0'then E_FileName else E_ItemName+','+E_ItemWeight end,E_SendTo,E_Address,E_Note,case when E_ExpressCompany='0'then'百世汇通'when E_ExpressCompany='1'then '顺丰' when E_ExpressCompany='2'then '邮政EMS' when E_ExpressCompany='3'then '物流' when E_ExpressCompany='4'then'其他' when E_ExpressCompany='5'then'自行联系' else ''end ,E_ExpressCode,E_ExpressMoney,E_BackNote from OM_Express where " + strWhere();
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "快递台账" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("快递台账表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion
    }
}
