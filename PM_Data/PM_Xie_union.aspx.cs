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
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_union : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //2016.8.25修改
                string contract_task_view = Request["contract_task_view"];
                if (!string.IsNullOrEmpty(contract_task_view))
                {
                    ddl_query.SelectedValue = "1";
                    txt_query.Text = contract_task_view.Trim();
                }

                this.ShenHe();
                this.GetSqlText();
                InitVar();
                this.bindRepeater();
            }
            if (rbl_shenhe.SelectedIndex == 4)
            {
                btnexport.Visible = true;
            }
            else
            {
                btnexport.Visible = false;
            }

            InitVar();

        }
        protected void btn_search1_click(object sender, EventArgs e)
        {
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }

        protected void waixie_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableCell cellbedit = (HtmlTableCell)e.Item.FindControl("bedit");
                Label zdr = e.Item.FindControl("PM_SUBMITID") as Label;
                Label spzt = e.Item.FindControl("PM_SPZT") as Label;
                HyperLink hyp_edit = e.Item.FindControl("hyp_edit") as HyperLink;
                string mspid = ((Label)e.Item.FindControl("MS_PID")).Text;
                //string msid = ((Label)e.Item.FindControl("MS_ID")).Text;
                string DocuNum = ((Label)e.Item.FindControl("PM_DocuNum")).Text;
                ((Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                ((HyperLink)e.Item.FindControl("HyperLink_lookup")).NavigateUrl = "PM_Xie_Audit.aspx?action=view&id=" + Server.UrlEncode(DocuNum) + "";
                //只有制单人本人，且未提交审核或已驳回时才允许修改
                // if (zdr.Text != Session["UserID"].ToString() || (spzt.Text.ToString() != "0" && spzt.Text.ToString() != "4"))
                if (zdr.Text != Session["UserID"].ToString() || (spzt.Text.ToString() != "0" && spzt.Text.ToString() != "4"))
                {
                    hyp_edit.Visible = false;
                }
                else
                {
                    ((Label)e.Item.FindControl("Label1")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("hyp_edit")).NavigateUrl = "PM_Xie_Audit.aspx?action=Editor&id=" + Server.UrlEncode(DocuNum) + "";
                }
            }
        }

        public string get_spzt(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "初始化";
            }
            else if (i == "1")
            {
                state = "提交未审批";
            }
            else if (i == "2")
            {
                state = "审批中";
            }
            else if (i == "3")
            {
                state = "已通过";
            }
            else if (i == "4")
            {
                state = "已驳回";
            }
            return state;
        }
        #region "数据查询，分页"
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
            //pager.TableName = "View_TM_TASKDQO as a left outer join  TBPM_SCWXRVW as b on a.MS_PID=b.PM_PID";
            pager.TableName = "(select distinct b.PM_DocuNum,b.PM_SPZT,b.PM_SUBMITID,b.PM_JHQ,a.MS_ID,a.MS_scwaixie,a.MS_CODE,a.MS_ENGID,a.MS_PJID,a.MS_ENGNAME,a.MS_PID,a.MS_TUHAO,a.MS_ZONGXU,a.MS_NAME,a.MS_GUIGE,a.MS_CAIZHI,a.MS_NUM,a.MS_TUWGHT,a.MS_TUTOTALWGHT,a.MS_MASHAPE,a.MS_LEN,a.MS_WIDTH,a.MS_PROCESS,a.MS_wxtype,CAST(a.MS_XHBZ as varchar(8000))MS_XHBZ,c.CM_PROJ from TBPM_WXDetail  as a left outer join  TBPM_SCWXRVW as b on a.MS_WSID=b.PM_DocuNum left join TBCM_PLAN as c on a.MS_PJID=c.CM_CONTR where MS_PID in (select distinct PM_PID from TBPM_SCWXRVW) and MS_scwaixie<>'0' and CM_PROJ in (select CM_PROJ from TBCM_PLAN where id in (select id from TBCM_BASIC where TSA_ID=MS_ENGID)))t";
            pager.PrimaryKey = "";
            pager.ShowFields = "*";
            pager.OrderField = "PM_DocuNum";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;
            pager.PageSize = 50;

        }
        void Pager_PageChanged(int pageNumber)
        {
            bindRepeater();
        }
        /// <summary>
        /// 绑定tbpc_otherpurbill_list_Repeater数据
        /// </summary>
        private void bindRepeater()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, waixie_list_Repeater, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件  
            }
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetSqlText()
        {
            StringBuilder sqltext = new StringBuilder();
            sqltext.Append("1=1");
            if (ddl_wx.SelectedIndex != 0)
            {
                sqltext.Append("and MS_wxtype='" + ddl_wx.SelectedValue.ToString() + "'");
            }
            //审批状态
            if (rbl_shenhe.SelectedValue.ToString() != "5")
            {
                sqltext.Append("and PM_SPZT='" + rbl_shenhe.SelectedValue.ToString() + "' ");
            }
            if (btn_query.Text.Trim() != "")
            {
                switch (ddl_query.SelectedValue)
                {
                    case "1":
                        sqltext.Append("and MS_ENGID like '%" + txt_query.Text.Trim() + "%'");
                        break;
                    case "2":
                        sqltext.Append("and MS_ENGNAME like '%" + txt_query.Text.Trim() + "%' ");
                        break;
                    case "3":
                        sqltext.Append("and MS_PID like '%" + txt_query.Text.Trim() + "%' ");
                        break;
                    case "4":
                        sqltext.Append("and MS_TUHAO like '%" + txt_query.Text.Trim() + "%' ");
                        break;
                    case "5":
                        sqltext.Append("and MS_MASHAPE like '%" + txt_query.Text.Trim() + "%' ");
                        break;
                    case "6":
                        sqltext.Append("and MS_WSID like '%" + txt_query.Text.Trim() + "%' ");
                        break;
                }
            }
            ViewState["sqlText"] = sqltext.ToString();
            string sqltext2 = "select distinct a.MS_WSID from TBPM_WXDetail  as a left outer join  TBPM_SCWXRVW as b on a.MS_WSID=b.PM_DocuNum where  ";
            string sqltext3 = "select a.MS_CODE from TBPM_WXDetail  as a left outer join  TBPM_SCWXRVW as b on a.MS_WSID=b.PM_DocuNum where ";
            sqltext2 = sqltext2 + sqltext;
            sqltext3 = sqltext3 + sqltext;
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext2);
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext3);
            Label1.Text = Convert.ToString(dt.Rows.Count);
            Label2.Text = Convert.ToString(dt1.Rows.Count);
        }
        protected void ddl_wx_OSIC(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetSqlText();
            this.InitVar();
            this.bindRepeater();
        }

        protected void btn_query_OnClick(object sender, EventArgs e)
        {

            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }
        private void ShenHe()
        {
            int a = 0;//初始化
            int b = 0;//未审批
            int c = 0;//审批中
            int d = 0;//已驳回
            string sqltext1 = "select PM_SPZT from TBPM_SCWXRVW where PM_DocuNum in (select MS_WSID FROM TBPM_WXDetail)";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PM_SPZT"].ToString() == "0")
                {
                    a++;
                }
                if (dt.Rows[i]["PM_SPZT"].ToString() == "1")
                {
                    b++;
                }
                if (dt.Rows[i]["PM_SPZT"].ToString() == "2")
                {
                    c++;
                }
                if (dt.Rows[i]["PM_SPZT"].ToString() == "4")
                {
                    d++;
                }
            }
            rbl_shenhe.Items.Clear();
            rbl_shenhe.Items.Add(new ListItem("全部", "5"));
            if (a != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("初始化" + "</label><label><font color=red>(" + a + ")</font>", "0"));
                rbl_shenhe.SelectedIndex = 1;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("初始化", "0"));
            }
            if (b != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("未审批" + "</label><label><font color=red>(" + b + ")</font>", "1"));
                rbl_shenhe.SelectedIndex = 2;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("未审批", "1"));
            }
            if (c != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("审批中" + "</label><label><font color=red>(" + c + ")</font>", "2"));
                rbl_shenhe.SelectedIndex = 3;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("审批中", "2"));
            }
            rbl_shenhe.Items.Add(new ListItem("已通过", "3"));
            if (d != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回" + "</label><label><font color=red>(" + d + ")</font>", "4"));
                rbl_shenhe.SelectedIndex = 5;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回", "4"));
            }
            rbl_shenhe.SelectedIndex = 0;
        }
        #endregion
        /// <summary>
        /// 下查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_xiacha_OnClick(object sender, EventArgs e)
        {
            int i = 0;
            string mscode = "";
            string bjdsheetno = "";//比价单号
            string sqltext = "";
            foreach (RepeaterItem retim in waixie_list_Repeater.Items)
            {
                CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        mscode = ((Label)retim.FindControl("MS_CODE")).Text;
                        sqltext = "select * from TBMP_IQRCMPPRICE where PIC_CODE='" + mscode + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count >= 1)
                        {
                            bjdsheetno = dt.Rows[0]["PIC_SHEETNO"].ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该条数据尚未生成比价单！');", true);
                            return;

                        }
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                return;
            }
            else if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条记录！');", true);
                return;
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('PM_Xie_check_detail.aspx?num1=1&num2=3&sheetno=" + bjdsheetno + ";", true);
                // Response.Write("<script>window.open('PM_Xie_check_detail.aspx?num1=1&num2=3&sheetno=" + bjdsheetno + "','toolbar=yes')</script>");
                Response.Redirect("~/PM_Data/PM_Xie_check_detail.aspx?sheetno=" + bjdsheetno + "");
            }
        }

        #region 导出功能

        protected void btnexport_Click(object sender, EventArgs e) //导出
        {
            int i = 0;
            string pid = "";
            string taskid = "";
            foreach (RepeaterItem Reitem in waixie_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        pid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PM_DocuNum")).Text;
                        taskid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_ENGID")).Text;
                    }
                }
            }
            if (pid != "")
            {
                if (i == 1)
                {
                    string sqltext = "select MS_TUHAO,MS_ZONGXU,MS_NAME+MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_NUM,MS_TUWGHT,MS_TUTOTALWGHT,MS_MASHAPE,MS_TECHUNIT,MS_YONGLIANG,MS_MATOTALWGHT,isnull(MS_LEN,''),isnull(MS_WIDTH,''),MS_NOTE,MS_XIALIAO,MS_PROCESS,MS_wxtype,isnull(MS_KU,'') as MS_KU,MS_ALLBEIZHU from  TBPM_WXDetail where  MS_WSID='" + pid + "'ORDER BY dbo.f_formatstr(MS_INDEX, '.')";
                    ExportDataItem(sqltext, pid, taskid);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要导出制作明细的批号！');", true);
                }
            }
            else
            {
                Response.Write("<script>alert('请勾选带单号的数据进行导出')</script>");
            }


        }

        private void ExportDataItem(string sqltext, string lotnum, string taskid)
        {

            string filename = "外协汇总表.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("外协汇总表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                ISheet sheet1 = wk.GetSheetAt(1);
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }
                string basic_sql = "SELECT A.PM_DocuNum,A.SUBMITNM,A.PM_PJID,A.PM_PID,A.PM_ENGID,B.CM_PROJ,A.PM_JHQ,B.TSA_ENGNAME,B.TSA_MAP FROM VIEW_TBPM_SCWXRVW as A LEFT JOIN VIEW_CM_Task as B on (A.PM_PJID=B.CM_CONTR AND A.PM_ENGID=B.TSA_ID) where PM_DocuNum='" + lotnum + "'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(basic_sql);
                string basic_sql2 = "select distinct(MS_ENGNAME)from TBPM_WXDetail where MS_ENGID='" + taskid + "'";
                DataTable dt2 = DBCallCommon.GetDTUsingSqlText(basic_sql2);
                IRow row21 = sheet0.GetRow(21);
                row21.GetCell(2).SetCellValue(dt1.Rows[0]["SUBMITNM"].ToString());
                row21.GetCell(4).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                IRow row2 = sheet0.GetRow(2);
                row2.GetCell(1).SetCellValue(dt1.Rows[0]["CM_PROJ"].ToString());
                row2.GetCell(3).SetCellValue(dt1.Rows[0]["PM_PJID"].ToString());
                row2.GetCell(5).SetCellValue(dt1.Rows[0]["PM_PID"].ToString());
                IRow row3 = sheet0.GetRow(3);
                row3.GetCell(1).SetCellValue(dt1.Rows[0]["PM_ENGID"].ToString());
                row3.GetCell(3).SetCellValue(dt2.Rows[0]["MS_ENGNAME"].ToString());
                row3.GetCell(5).SetCellValue(dt1.Rows[0]["PM_JHQ"].ToString());
                IRow row4 = sheet0.GetRow(4);
                row4.GetCell(1).SetCellValue(dt1.Rows[0]["TSA_MAP"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 2);
                    for (int j = 0; j < 16; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                    string[] pross = new string[8];
                    for (int j = 0; j < dt.Rows[i][16].ToString().Split('-').Length; j++)
                    {
                        pross[j] = dt.Rows[i][16].ToString().Split('-')[j];
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        row.CreateCell(j + 16).SetCellValue(pross[j]);
                    }
                    for (int j = 17; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 7).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
                sheet0.ForceFormulaRecalculation = true;
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }

        #endregion
    }
}
