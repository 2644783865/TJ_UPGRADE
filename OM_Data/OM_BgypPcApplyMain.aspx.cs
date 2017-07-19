using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BgypPcApplyMain : BasicPage
    {
        string sqlText;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                RBLBind();
                InitVar();
                GetAuditData();
            }
            CheckUser(ControlFinder);
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetAuditData();
        }
        protected void GridView1_DATABOUND(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string code = ((Label)e.Row.FindControl("lblCode")).Text.Trim();
                e.Row.Attributes.Add("ondblclick", "javascript: window.showModalDialog('OM_BgypPcApply.aspx?action=view&id=" + code + "','','scrollbars:yes;resizable:no;help:no;status:no;center:yes;dialogHeight:700px;dialogWidth:1200px;');");
                e.Row.Attributes["style"] = "Cursor:hand";
                e.Row.Attributes.Add("title", "双击查看详细信息！");

                string ss = "select * FROM TBOM_BGYPPCAPPLY WHERE PCCODE='" + code + "'";
                DataTable tt = DBCallCommon.GetDTUsingSqlText(ss);
                if (tt.Rows.Count > 0)
                {
                    if (tt.Rows[0]["STATE"].ToString() == "7")//取消
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Gray;

                    }
                    if (tt.Rows[0]["STATE"].ToString() == "3" || tt.Rows[0]["STATE"].ToString() == "5")//驳回
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.DarkBlue;
                        if (Session["UserID"].ToString() == tt.Rows[0]["JBRID"].ToString())
                        {
                            ((HyperLink)e.Row.FindControl("hlTask1")).NavigateUrl = "OM_BgypPcApply.aspx?action=mod&id=" + code + "";
                            ((Label)e.Row.FindControl("state1")).Text = "驳回处理";
                            LinkButton btnDelete = ((LinkButton)e.Row.FindControl("btnDelete"));
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            ((HyperLink)e.Row.FindControl("hlTask1")).NavigateUrl = "OM_BgypPcApply.aspx?action=view&id=" + code + "";
                            ((Label)e.Row.FindControl("state1")).Text = "查看";
                        }
                    }
                    if (tt.Rows[0]["STATE"].ToString() == "1")//待审
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                        if (Session["UserID"].ToString() == tt.Rows[0]["SHRFID"].ToString())
                        {
                            ((HyperLink)e.Row.FindControl("hlTask1")).NavigateUrl = "OM_BgypPcApply.aspx?action=verify&id=" + code + "";
                            ((Label)e.Row.FindControl("state1")).Text = "审核";
                        }
                        else
                        {
                            ((HyperLink)e.Row.FindControl("hlTask1")).NavigateUrl = "OM_BgypPcApply.aspx?action=view&id=" + code + "";
                            ((Label)e.Row.FindControl("state1")).Text = "查看";
                        }
                    }

                    if (tt.Rows[0]["STATE"].ToString() == "2")//通过
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.White;
                        ((HyperLink)e.Row.FindControl("hlTask1")).NavigateUrl = "OM_BgypPcApply.aspx?action=view&id=" + code + "";
                        ((Label)e.Row.FindControl("state1")).Text = "查看";
                    }
                }

                List<string> codeList = new List<string>();
                if (codeList.Contains(code))
                {
                    ((Label)e.Row.FindControl("lblCode")).Text = "";
                }
                else
                {
                    codeList.Add(code);
                }
            }
        }
        /// <summary>
        /// 动态添加审核状态项(待审核、审核中、通过、驳回、驳回已处理)
        /// </summary>
        private void RBLBind()
        {

            //Label state1 = (Label)GridView1.FindControl("state1");
            sqlText = "select count(*) from TBOM_BGYPPCAPPLY where ";
            sqlText += " (SHRFID='" + Session["UserID"].ToString() + "' and STATE='1') or ";
            sqlText += " (SHRSID='" + Session["UserID"].ToString() + "' and STATE='2') ";
            //sqlText += " (JBRID='" + Session["UserID"].ToString() + "' ) ";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    rblstatus.Items.Add(new ListItem("您的审核任务", "0"));
                }
                else
                {
                    rblstatus.Items.Add(new ListItem("您的审核任务" + "<font color=red>(" + dr[0].ToString() + ")</font>", "0"));
                    rblstatus.SelectedIndex = 0;
                }
            }
            dr.Close();
            sqlText = "select count(*) from TBOM_BGYPPCAPPLY where (SHRFID='" + Session["UserID"].ToString() + "' and STATE ='1')";
            sqlText += " or (SHRSID='" + Session["UserID"].ToString() + "' and STATE='2') ";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    rblstatus.Items.Add(new ListItem("审核中", "1"));
                }
                else
                {
                    rblstatus.Items.Add(new ListItem("审核中" + "<font color=red>(" + dr[0].ToString() + ")</font>", "1"));
                    if (rblstatus.SelectedValue != "0")
                    {
                        rblstatus.SelectedIndex = 1;
                    }
                }
                //for (int i = 0; i < GridView1.Rows.Count; i++)
                //{
                //    Label state1 = (Label)GridView1.Rows[i].FindControl("state1");
                //    state1.Text = "查看";
                //}

            }
            dr.Close();
            sqlText = "select count(*) from TBOM_BGYPPCAPPLY where (SHRFID='" + Session["UserID"].ToString() + "'";
            sqlText += " or SHRSID='" + Session["UserID"].ToString() + "' or JBRID='" + Session["UserID"].ToString() + "' ) ";
            sqlText += " and STATE in ('3','5')";
            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    rblstatus.Items.Add(new ListItem("驳回", "2"));
                }
                else
                {
                    rblstatus.Items.Add(new ListItem("驳回" + "<font color=red>(" + dr[0].ToString() + ")</font>", "2"));
                    if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1")
                    {
                        rblstatus.SelectedIndex = 2;
                    }
                }
            }
            dr.Close();
            rblstatus.Items.Add(new ListItem("通过", "3"));
            rblstatus.Items.Add(new ListItem("汇总驳回作废", "4"));
            if (rblstatus.SelectedValue != "0" && rblstatus.SelectedValue != "1" && rblstatus.SelectedValue != "2" )
            {
                rblstatus.SelectedIndex = 0;
            }
        }
        private void InitPager()
        {
            pager.TableName = "TBOM_BGYPPCAPPLY";
            pager.PrimaryKey = "PCCODE";
            pager.ShowFields = "PCCODE,JBR,JBRID,SHRF,SHRFID,SHRFDATE,SHRFNOTE,SHRS,SHRSID,SHRSDATE,SHRSNOTE,STATE,DATE,JINE";

            pager.OrderField = "PCCODE";
            pager.StrWhere = GetSqlWhere();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
        }
        private string GetSqlWhere()
        {
            string sqlwhere = "";
            if (rblstatus.SelectedValue == "0") //我的审核任务
            {
                sqlwhere = "(SHRFID='" + Session["UserID"].ToString() + "' and STATE='1') or ";
                sqlwhere += " (SHRSID='" + Session["UserID"].ToString() + "' and STATE='2')";

            }
            else if (rblstatus.SelectedValue == "1") //审核中
            {
                sqlwhere = "(STATE ='1')";
            }
            else if (rblstatus.SelectedValue == "2") //驳回
            {
                sqlwhere = "(SHRFID='" + Session["UserID"].ToString() + "'or SHRSID='" + Session["UserID"].ToString() + "' or JBRID='" + Session["UserID"].ToString() + "') and STATE in ('3','5')";
            }
            else if (rblstatus.SelectedValue == "3")
            {
                sqlwhere = "(STATE='2' or STATE='7')";
            }
            sqlwhere += "and (HZSTATE is null or HZSTATE<>'2')";
            if (rblstatus.SelectedValue == "4")
            {
                sqlwhere = "(HZSTATE='2')";
            }
            if (txt_starttime.Text != "")
            {
                sqlwhere += " and  DATE >='" + txt_starttime.Text + "'";
            }
            if (txt_endtime.Text != "")
            {
                sqlwhere += " and  DATE <='" + txt_endtime.Text + "'";
            }

            return sqlwhere;
        }
        private void GetAuditData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
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

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAuditData();
        }




        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaochu_OnClick(object sender, EventArgs e)
        {

            string sqltext = ""; ;

            sqltext += "select *,case when state='1' or state='2' then '审核中' when state='3' or state='5' then '驳回' when state='6' or state='7' then '已通过' end as StateHZ from View_TBOM_BGYPPCAPPLYINFO where " + GetSqlWhere() + " order by DATE desc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            sqltext = "select sum(WLJE) from View_TBOM_BGYPPCAPPLYINFO where " + GetSqlWhere();
            string sum = "";
            try
            {
                System.Data.DataTable dtSum = DBCallCommon.GetDTUsingSqlText(sqltext);
                sum = dtSum.Rows[0][0].ToString();
            }
            catch (Exception)
            {

                sum = "error";
            }

            ExportDataItem(dt, sum);

        }




        private void ExportDataItem(System.Data.DataTable objdt, string sum)
        {
            string filename = "办公用品采购明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("办公用品采购明细.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                int j = 0;
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + objdt.Rows[i]["CODE"].ToString());
                    row.CreateCell(2).SetCellValue("" + objdt.Rows[i]["WLBM"].ToString());
                    row.CreateCell(3).SetCellValue("" + objdt.Rows[i]["WLNAME"].ToString());
                    row.CreateCell(4).SetCellValue("" + objdt.Rows[i]["WLMODEL"].ToString());
                    row.CreateCell(5).SetCellValue("" + objdt.Rows[i]["WLNUM"].ToString());
                    row.CreateCell(6).SetCellValue("" + objdt.Rows[i]["WLUNIT"].ToString());
                    row.CreateCell(7).SetCellValue("" + objdt.Rows[i]["WLPRICE"].ToString());
                    row.CreateCell(8).SetCellValue("" + objdt.Rows[i]["WLJE"].ToString());
                    row.CreateCell(9).SetCellValue("" + objdt.Rows[i]["DATE"].ToString());

                    row.CreateCell(10).SetCellValue("" + objdt.Rows[i]["JBR"].ToString());
                    row.CreateCell(11).SetCellValue("" + objdt.Rows[i]["StateHZ"].ToString());

                    j++;
                }
                IRow rowSum = sheet0.CreateRow(j + 1);
                rowSum.CreateCell(7).SetCellValue("总计");
                rowSum.CreateCell(8).SetCellValue(sum);

                for (int i = 0; i <= objdt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string pccode = (sender as LinkButton).CommandArgument.ToString();
            string sql0 = "insert into TBOM_BGYPPCAPPLY_DeleteRecord(PCCODE, JBR, JBRID, SHRF, SHRFID, SHRFNOTE, SHRFDATE, SHRS, SHRSID, SHRSNOTE, SHRSDATE,STATE, NOTE, DATE, JINE, HZSTATE, HZCODE,DELETEPERSON,DELETETIME) select PCCODE, JBR, JBRID, SHRF, SHRFID, SHRFNOTE, SHRFDATE, SHRS, SHRSID, SHRSNOTE, SHRSDATE,STATE, NOTE, DATE, JINE, HZSTATE, HZCODE,'" + Session["UserName"].ToString().Trim() + "'as DELETEPERSON,'" + DateTime.Now.ToString() + "'as DELETETIME from TBOM_BGYPPCAPPLY where PCCODE='" + pccode + "'and STATE='3'";
            list.Add(sql0);
            string sqlText0 = "insert into TBOM_BGYPPCAPPLYINFO_DeleteRecord(CODE, WLCODE, WLBM, WLNAME, WLMODEL, WLUNIT, WLNUM, WLPRICE, WLJE, STATE_rk,DEPNAME, NOTE) select CODE, WLCODE, WLBM, WLNAME, WLMODEL, WLUNIT, WLNUM, WLPRICE, WLJE, STATE_rk,DEPNAME, NOTE from TBOM_BGYPPCAPPLYINFO where CODE='" + pccode + "'";
            list.Add(sqlText0);
            string sql = "delete from TBOM_BGYPPCAPPLY  where PCCODE='" + pccode + "' and STATE='3'";
            list.Add(sql);
            string sqlText = "delete from TBOM_BGYPPCAPPLYINFO  where CODE='" + pccode + "'";
            list.Add(sqlText);
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_BgypPcApplyMain.aspx");
        }
    }
}
