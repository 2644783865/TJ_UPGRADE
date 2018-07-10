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
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_details_manage : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqltext;
        //string tablename;
        public string tablename
        {
            get
            {
                string str;
                if (rblstate.SelectedItem.Text.Trim() == "正常")
                {
                    str = "View_TM_MSFORALLRVW";
                }
                else
                {
                    str = "View_TM_MSCHANGERVW";
                }
                return str;
            }
            set { tablename = value; }

        }
        public string get_pur_bjdsh(string i)
        {
            string statestr = "";
            if (i == "1")
            {
                statestr = "<span style='color:red'>已完工</span>";
            }
            else if (i == "0")
            {
                statestr = "未完工";
            }
            else { statestr = ""; }
            return statestr;
        }
        public string get_color(string i)
        {
            string statestr = "";
            if (i == "1")
            {
                statestr = "<span style='color:red'></span>";
            }
            return statestr;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string depid = Session["UserDeptID"].ToString();

                //如果是技术部就隐藏已处理和未处理
                if (depid == "03")
                {
                  look_state.Visible = false; 
                  rb_2.Checked = true;              //全部
                  
                }

                //如果是生产部门就隐藏已完工和未完工
                if (depid =="04")
	            {
                    rb_3.Visible = false; //全部
                    rb_1.Visible = false;
                    rb_2.Visible = false;
	            }

                //我的任务
                cb_myjob.Checked = false;


                //this.DataPJname();
                //this.DataENGname();
                Datafengong();
                GetSele();
                InitVar();
                //  InitPager();
                GetEnable();
                this.bindRepeater();

            }
            if (rblstate.SelectedIndex == 1)
            {
                btn_finish.Visible = false;
                btn_look.Visible = true;
                foreach (RepeaterItem reitem in details_repeater.Items)
                {
                    System.Web.UI.WebControls.Label lab_status = (System.Web.UI.WebControls.Label)reitem.FindControl("lab_status");
                    System.Web.UI.WebControls.Label lab_status1 = (System.Web.UI.WebControls.Label)reitem.FindControl("lab_status1");
                    lab_status.Visible = true;
                    lab_status1.Visible = false;
                }
            }
            else
            {
                btn_finish.Visible = true;
                btn_look.Visible = false;
                foreach (RepeaterItem reitem in details_repeater.Items)
                {
                    System.Web.UI.WebControls.Label lab_status = (System.Web.UI.WebControls.Label)reitem.FindControl("lab_status");
                    System.Web.UI.WebControls.Label lab_status1 = (System.Web.UI.WebControls.Label)reitem.FindControl("lab_status1");
                    lab_status.Visible = false;
                    lab_status1.Visible = true;
                }
            }
            InitVar();
            ControlVisible();//控制确定按钮可见性
            CheckUser(ControlFinder);
        }
        //2016.11.11修改，次数为基数艾广修特设，做确定查看处理，显示查看按钮

        private void ControlVisible()
        {
            foreach (RepeaterItem item in details_repeater.Items)
            {
                LinkButton btn_Confirm_ck_f = item.FindControl("btn_Confirm_ck") as LinkButton;
                string[] sfqu_ck = btn_Confirm_ck_f.CommandArgument.ToString().Split(',');
                string mp_code = sfqu_ck[0].Trim();
                string mp_ck_bt = sfqu_ck[1].Trim();
                btn_Confirm_ck_f.Visible = false;
                if (mp_ck_bt == "1" && Session["UserName"].ToString() == "艾广修")
                {
                    btn_Confirm_ck_f.Visible = true;
                }
            }
        }


        //private void DataPJname()  //绑定项目
        //{
        //    sqltext = "select distinct MS_PJID from " +tablename+ " where MS_STATE='8'";
        //    string DataText = "MS_PJID";
        //    string DataValue = "MS_PJID";
        //    DBCallCommon.BindDdl(ddlpjname, sqltext, DataText, DataValue);
        //}
        //private void DataENGname()
        //{
        //    sqltext = "select distinct MS_ENGNAME,MS_ENGID from " + tablename + " where MS_PJID='" + ddlpjname.SelectedValue + "' and MS_STATE='8'";
        //    string DataText = "MS_ENGNAME";
        //    string DataValue = "MS_ENGID";
        //    DBCallCommon.BindDdl(ddlengname, sqltext, DataText, DataValue);
        //}
        private void Datafengong()
        {
           // sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_POSITION='0314'and ST_PD='0'";
            sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_NAEM='艾广修'or ST_NAEM='崔涛'or ST_NAEM='吴红霞'and ST_PD='0'";
            string DataText = "ST_NAME";
            string DataValue = "ST_NAME";
            DBCallCommon.BindDdl(ddl_fengfong, sqltext, DataText, DataValue);
        }

        private void GetEnable()
        {
            if (Session["UserName"].ToString() == "艾广修")
            {
                btn_fengong.Enabled = true;
                btn_finish.Enabled = true;
            }
            else
            {
                btn_finish.Enabled = false;
                btn_fengong.Enabled = false;
            }
        }


        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        void Pager_PageChanged(int pageNumber)
        {
            InitPager();
            bindRepeater();
        }
        private void InitPager()
        {
            string sqltxt = "MS_STATE='8'";

            //if (ddlpjname.SelectedValue != "-请选择-")
            //{
            //    sqltxt += " and MS_PJID='" + ddlpjname.SelectedValue + "' ";
            //}
            //if (ddlengname.SelectedValue != "-请选择-")
            //{
            //    sqltxt += " and MS_ENGID='" + ddlengname.SelectedValue + "' ";
            //}
            sqltxt += GetStrCondition();
            pager.TableName = "" + tablename + " as A left join TBMP_MANUTSASSGN AS B on A.MS_ENGID=B.MTA_ID ";
            // pager.TableName = "View_TM_MSFORALLRVW";//制作明细
            pager.PrimaryKey = "MS_ID";
            pager.ShowFields = "A.*,B.MTA_DUY";
            pager.OrderField = "MS_ADATE";
            pager.StrWhere = sqltxt;
            pager.OrderType = 1;//按时间顺序排列
            pager.PageSize = 20;
        }
        private void bindRepeater()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, details_repeater, UCPaging1, NoDataPanel);
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

        //protected void ddlpjname_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UCPaging1.CurrentPage = 1;
        //    this.DataENGname();
        //    InitVar();
        //    this.bindRepeater();
        //}
        protected void btnHTH_OnClick(object sender, EventArgs e)
        {
            InitVar();
            this.bindRepeater();
            ControlVisible();
        }

        protected void rblstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            //this.DataPJname();
            //this.DataENGname();
            InitVar();
            this.bindRepeater();
            ControlVisible();
        }
        protected void ddlengname_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitVar();
            this.bindRepeater();
            ControlVisible();
        }
        #region 导出功能

        protected void btnexport_Click(object sender, EventArgs e) //导出
        {
            int i = 0;
            string pid = "";
            string taskid = "";
            foreach (RepeaterItem Reitem in details_repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        pid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_ID")).Text;
                        taskid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_ENGID")).Text;
                    }
                }
            }
            if (i == 1)
            {
                string sqltext = "";
                if (pid.Contains("BG"))
                {
                    sqltext = "select MS_TUHAO,MS_ZONGXU,MS_NAME+MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_NUM,MS_TUWGHT,MS_TUTOTALWGHT,MS_MASHAPE,MS_TECHUNIT,MS_YONGLIANG,MS_MATOTALWGHT,isnull(MS_LEN,''),isnull(MS_WIDTH,''),MS_NOTE,MS_XIALIAO,MS_PROCESS,isnull(MS_KU,'') as MS_KU,MS_ALLBEIZHU,MS_BIANGENGBEIZHU from TBMP_TASKDQO where MS_PID='" + pid + "' ORDER BY dbo.f_formatstr(MS_INDEX, '.')";

                }
                else
                {
                    sqltext = "select MS_TUHAO,MS_ZONGXU,MS_NAME+MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_NUM,MS_TUWGHT,MS_TUTOTALWGHT,MS_MASHAPE,MS_TECHUNIT,MS_YONGLIANG,MS_MATOTALWGHT,isnull(MS_LEN,''),isnull(MS_WIDTH,''),MS_NOTE,MS_XIALIAO,MS_PROCESS,isnull(MS_KU,'') as MS_KU,MS_ALLBEIZHU from TBMP_TASKDQO where MS_PID='" + pid + "' ORDER BY dbo.f_formatstr(MS_INDEX, '.')";
                }
                ExportDataItem(sqltext, pid, taskid);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要导出制作明细的批号！');", true);
            }

        }

        private void ExportDataItem(string sqltext, string lotnum, string taskid)
        {
            string revtable = "";
            if (lotnum.Contains("BG"))
            {
                revtable = "View_TM_MSCHANGERVW";//变更
                string basic_sql1 = "select MS_PJNAME, MS_ENGNAME, MS_MAP from " + revtable + " where MS_ID='" + lotnum + "'";
                System.Data.DataTable dt_1 = DBCallCommon.GetDTUsingSqlText(basic_sql1);
                string pjname = dt_1.Rows[0]["MS_PJNAME"].ToString();
                string tuhao = dt_1.Rows[0]["MS_MAP"].ToString().Replace(',', '-');

                string engname = dt_1.Rows[0]["MS_ENGNAME"].ToString();
                string filename = "" + pjname + "/" + tuhao + "/" + engname + "/产品明细变更表.xls";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                HttpContext.Current.Response.Clear();
                //1.读取Excel到FileStream 
                using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("产品明细变更表.xls")))
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
                    string basic_sql = "select MS_PJID, MS_ENGID,MS_PJNAME,MS_REVIEWBNAME,MS_REVIEWANAME,MS_SUBMITNM,CONVERT(VARCHAR(10),MS_SUBMITTM,23) as MS_SUBMITTM ,CONVERT(VARCHAR(10),MS_REVIEWBTIME,23) as MS_REVIEWBTIME,CONVERT(VARCHAR(10),MS_REVIEWATIME,23) as MS_REVIEWATIME, MS_ENGNAME,MS_CHILDENGNAME,MS_MAP from " + revtable + " where MS_ID='" + lotnum + "'";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(basic_sql);
                    string sqltext_1 = "select QSA_QCCLERKNM from TBQM_QTSASSGN where QSA_ENGID='" + taskid + "'";
                    System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext_1);
                    IRow row2 = sheet0.GetRow(2);
                    row2.GetCell(1).SetCellValue(dt1.Rows[0]["MS_PJNAME"].ToString());
                    row2.GetCell(3).SetCellValue(dt1.Rows[0]["MS_PJID"].ToString());
                    row2.GetCell(5).SetCellValue(dt1.Rows[0]["MS_ENGNAME"].ToString());
                    IRow row3 = sheet0.GetRow(3);
                    row3.GetCell(1).SetCellValue(dt1.Rows[0]["MS_ENGID"].ToString());
                    row3.GetCell(3).SetCellValue(dt1.Rows[0]["MS_CHILDENGNAME"].ToString());
                    if (dt2.Rows.Count > 0)
                        row3.GetCell(5).SetCellValue(dt2.Rows[0]["QSA_QCCLERKNM"].ToString());
                    IRow row4 = sheet0.GetRow(4);
                    row4.GetCell(1).SetCellValue(dt1.Rows[0]["MS_MAP"].ToString());
                    IRow row17 = sheet0.GetRow(17);
                    row17.GetCell(2).SetCellValue(dt1.Rows[0]["MS_REVIEWBNAME"].ToString());
                    row17.GetCell(4).SetCellValue(dt1.Rows[0]["MS_REVIEWBTIME"].ToString());
                    IRow row19 = sheet0.GetRow(19);
                    row19.GetCell(2).SetCellValue(dt1.Rows[0]["MS_REVIEWANAME"].ToString());
                    row19.GetCell(4).SetCellValue(dt1.Rows[0]["MS_REVIEWATIME"].ToString());
                    IRow row21 = sheet0.GetRow(21);
                    row21.GetCell(2).SetCellValue(dt1.Rows[0]["MS_SUBMITNM"].ToString());
                    row21.GetCell(4).SetCellValue(dt1.Rows[0]["MS_SUBMITTM"].ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 2);
                        for (int j = 0; j < 16; j++)
                        {
                            row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        }
                        string[] pross = new string[20];
                        for (int j = 0; j < dt.Rows[i][16].ToString().Split('-').Length; j++)
                        {
                            pross[j] = dt.Rows[i][16].ToString().Split('-')[j];
                        }
                        for (int j = 0; j < 20; j++)
                        {
                            row.CreateCell(j + 16).SetCellValue(pross[j]);
                        }
                        for (int j = 17; j < dt.Columns.Count; j++)
                        {
                            row.CreateCell(j + 19).SetCellValue(dt.Rows[i][j].ToString());
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
            else
            {
                revtable = "View_TM_MSFORALLRVW";//正常
                string basic_sql1 = "select MS_PJNAME, MS_ENGNAME, MS_MAP from " + revtable + " where MS_ID='" + lotnum + "'";
                System.Data.DataTable dt_1 = DBCallCommon.GetDTUsingSqlText(basic_sql1);
                string pjname = dt_1.Rows[0]["MS_PJNAME"].ToString();
                string tuhao = dt_1.Rows[0]["MS_MAP"].ToString().Replace(',', '-');
                string engname = dt_1.Rows[0]["MS_ENGNAME"].ToString();
                string filename = "" + pjname + "/" + tuhao + "/" + engname + "/产品明细表.xls";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                HttpContext.Current.Response.Clear();
                //1.读取Excel到FileStream 
                using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("产品明细表.xls")))
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

                    string basic_sql = "select MS_PJID, MS_ENGID,MS_PJNAME,MS_REVIEWBNAME,MS_REVIEWANAME,MS_SUBMITNM,CONVERT(VARCHAR(10),MS_SUBMITTM,23) as MS_SUBMITTM ,CONVERT(VARCHAR(10),MS_REVIEWBTIME,23) as MS_REVIEWBTIME,CONVERT(VARCHAR(10),MS_REVIEWATIME,23) as MS_REVIEWATIME, MS_ENGNAME,MS_CHILDENGNAME,MS_MAP from " + revtable + " where MS_ID='" + lotnum + "'";

                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(basic_sql);
                    string sqltext_1 = "select QSA_QCCLERKNM from TBQM_QTSASSGN where QSA_ENGID='" + taskid + "'";
                    System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext_1);
                    IRow row2 = sheet0.GetRow(2);
                    row2.GetCell(1).SetCellValue(dt1.Rows[0]["MS_PJNAME"].ToString());
                    row2.GetCell(3).SetCellValue(dt1.Rows[0]["MS_PJID"].ToString());
                    // row2.GetCell(5).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                    row2.GetCell(5).SetCellValue(dt1.Rows[0]["MS_ENGNAME"].ToString());
                    IRow row3 = sheet0.GetRow(3);
                    row3.GetCell(1).SetCellValue(dt1.Rows[0]["MS_ENGID"].ToString());
                    row3.GetCell(3).SetCellValue(dt1.Rows[0]["MS_CHILDENGNAME"].ToString());
                    if (dt2.Rows.Count > 0)
                        row3.GetCell(5).SetCellValue(dt2.Rows[0]["QSA_QCCLERKNM"].ToString());
                    IRow row4 = sheet0.GetRow(4);
                    row4.GetCell(1).SetCellValue(dt1.Rows[0]["MS_MAP"].ToString());
                    IRow row17 = sheet0.GetRow(17);
                    row17.GetCell(2).SetCellValue(dt1.Rows[0]["MS_REVIEWBNAME"].ToString());
                    row17.GetCell(4).SetCellValue(dt1.Rows[0]["MS_REVIEWBTIME"].ToString());
                    IRow row19 = sheet0.GetRow(19);
                    row19.GetCell(2).SetCellValue(dt1.Rows[0]["MS_REVIEWANAME"].ToString());
                    row19.GetCell(4).SetCellValue(dt1.Rows[0]["MS_REVIEWATIME"].ToString());
                    IRow row21 = sheet0.GetRow(21);
                    row21.GetCell(2).SetCellValue(dt1.Rows[0]["MS_SUBMITNM"].ToString());
                    row21.GetCell(4).SetCellValue(dt1.Rows[0]["MS_SUBMITTM"].ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 2);
                        for (int j = 0; j < 16; j++)
                        {
                            row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        }
                        string[] pross = new string[20];
                        for (int j = 0; j < dt.Rows[i][16].ToString().Split('-').Length; j++)
                        {
                            pross[j] = dt.Rows[i][16].ToString().Split('-')[j];
                        }
                        for (int j = 0; j < 20; j++)
                        {
                            row.CreateCell(j + 16).SetCellValue(pross[j]);
                        }
                        for (int j = 17; j < dt.Columns.Count; j++)
                        {
                            row.CreateCell(j + 19).SetCellValue(dt.Rows[i][j].ToString());
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

        }

        #endregion
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<string> list_str = new List<string>();
            int i = 0;
            foreach (RepeaterItem Reitem in details_repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT");
                if (cb.Checked)
                {
                    i++;
                    string msid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_ID")).Text.ToString();
                    if (ddl_fengfong.SelectedIndex != 0)
                    {
                        sqltext = "update " + tablename + " set MS_PERSON='" + ddl_fengfong.SelectedValue.ToString() + "' WHERE MS_ID='" + msid + "'";
                        list_str.Add(sqltext);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择相应的分工人员！');", true);
                        break;
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据！');", true);
            }
            else if (i >= 1)
            {
                DBCallCommon.ExecuteTrans(list_str);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！')", true);
            }
            ControlVisible();

        }
        protected void Ibtn_look_click(object sender, EventArgs e)
        {
            string msid = ((System.Web.UI.WebControls.Button)sender).CommandArgument;
            if (tablename == "View_TM_MSFORALLRVW")
            {
                string sqltxt = "select A.*,B.MTA_DUY from View_TM_MSFORALLRVW as A left join TBMP_MANUTSASSGN as B on A.MS_ENGID=B.MTA_ID where MS_ID='" + msid + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltxt);
                //string mtaduy = dt.Rows[0]["MTA_DUY"].ToString();
                //string username = Session["UserName"].ToString();
                //if (mtaduy == username)
                //{
                //    string sqltext = "update TBPM_MSFORALLRVW set MS_LOOKSTATUS='1' where MS_ID='" + msid + "'and MS_STATE='8'";
                //    DBCallCommon.ExeSqlText(sqltext);
                //    InitVar();
                //    this.bindRepeater();
                //}
                //Response.Redirect("PM_Xie_List_Detail.aspx?action=look&mnpid=" + msid + "");
                Response.Write("<script>window.open('PM_Xie_List_Detail.aspx?action=look&mnpid=" + msid + "');</script>");
            }
            else
            {
                //Response.Redirect("../TM_Data/TM_MS_Detail_Audit.aspx?ms_audit_id="+msid+"");
                Response.Write("<script>window.open('../TM_Data/TM_MS_Detail_Audit.aspx?ms_audit_id=" + msid + "');</script>");
            }
            InitVar();
            this.bindRepeater();
            ControlVisible();

        }
        //protected void btn_query_OnClick(object sender, EventArgs e)
        //{
        //    UCPaging1.CurrentPage = 1;
        //    InitVar();
        //    this.bindRepeater();
        //}
        protected void btn_finish_OnClick(object sender, EventArgs e)
        {
            int i = 0;
            string msid = "";
            List<string> list_sql = new List<string>();
            foreach (RepeaterItem reitem in details_repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cb_1 = (System.Web.UI.WebControls.CheckBox)reitem.FindControl("CKBOX_SELECT");
                if (cb_1 != null)
                {
                    if (cb_1.Checked)
                    {
                        i++;
                        msid = ((System.Web.UI.WebControls.Label)reitem.FindControl("MS_ID")).Text.ToString();
                        string sqltxt = "update  TBPM_MSFORALLRVW SET MS_FINISHSTATUS='1' where MS_ID='" + msid + "' and MS_STATE='8'";
                        list_sql.Add(sqltxt);
                    }
                }
            }
            if (i > 0)
            {
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('制定成功,共有" + i + "条批号已完工！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要确定完工的批号！');", true);
            }
            InitVar();
            this.bindRepeater();
            ControlVisible();
        }

        protected void btn_look_OnClick(object sender, EventArgs e)
        {
            int i = 0;
            string msid = "";
            List<string> list = new List<string>();
            foreach (RepeaterItem reitem in details_repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cb_1 = (System.Web.UI.WebControls.CheckBox)reitem.FindControl("CKBOX_SELECT");
                if (cb_1 != null)
                {
                    if (cb_1.Checked)
                    {
                        i++;
                        msid = ((System.Web.UI.WebControls.Label)reitem.FindControl("MS_ID")).Text.ToString();
                        string sqltxt = "update  TBPM_MSCHANGERVW SET MS_FINSTATUS='1' where MS_ID='" + msid + "'and MS_STATE='8'";
                        list.Add(sqltxt);
                    }
                }
            }
            if (i > 0)
            {
                DBCallCommon.ExecuteTrans(list);
                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('确定！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要确定的变更批号！');", true);
            }
            InitVar();
            this.bindRepeater();
            ControlVisible();
        }

        protected void cb_myjob_OnCheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            //this.DataPJname();
            //this.DataENGname();
            InitVar();
            this.bindRepeater();
            ControlVisible();
        }
        protected void details_repeater_databound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label ms_lookstatus = (System.Web.UI.WebControls.Label)e.Item.FindControl("MS_LOOKSTATUS");
                System.Web.UI.WebControls.Label lab_rownum = (System.Web.UI.WebControls.Label)e.Item.FindControl("rownum");
                System.Web.UI.WebControls.Label ms_finstatus = (System.Web.UI.WebControls.Label)e.Item.FindControl("lab_status");
                string ms_finstatus1 = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lab_status1")).Text;
                string msperson = ((System.Web.UI.WebControls.Label)e.Item.FindControl("MS_PERSON")).Text;
                string lab_status = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lab_status")).Text;

                HtmlTableCell cell1 = e.Item.FindControl("td_1") as HtmlTableCell;
                HtmlTableCell cell5 = e.Item.FindControl("td_5") as HtmlTableCell;
                HtmlTableCell td_12 = e.Item.FindControl("td_12") as HtmlTableCell;

                if (lab_status == "已确定变更")
                {
                    td_12.BgColor = "Green";
                }
                // HtmlTableCell cell12 = e.Item.FindControl("td_12") as HtmlTableCell;
                if (ms_lookstatus.Text.ToString() == "1")
                {
                    // lab_rownum.BackColor = System.Drawing.Color.Green;
                    cell1.BgColor = "Green";
                }
                switch (msperson)
                {
                    case "张骞": cell5.BgColor = "LightBlue";
                        break;

                    case "崔涛": cell5.BgColor = "LightGreen";
                        break;
                    case "吴红霞": cell5.BgColor = "Yellow";
                        break;
                }
            }

        }
        private void GetSele()
        {
            foreach (Control item in select.Controls[1].Controls[0].Controls)
            {
                if (item is DropDownList)
                {
                    if (item.ID.Contains("screen"))
                    {
                        ((DropDownList)item).DataSource = bindItemList();
                        ((DropDownList)item).DataTextField = "value";
                        ((DropDownList)item).DataValueField = "key";
                        ((DropDownList)item).DataBind();
                        ((DropDownList)item).SelectedValue = "0";
                    }
                }
            }
        }
        private Dictionary<string, string> bindItemList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            //ItemList.Add("NO", "");
            ItemList.Add("0", "请选择");
            ItemList.Add("MS_ID", "批号");
            ItemList.Add("MS_PJNAME", "项目名称");
            ItemList.Add("MS_ENGID", "任务号");
            ItemList.Add("MS_CHILDENGNAME", "子项名称");
            ItemList.Add("MS_MAP", "图号");
            //ItemList.Add("MS_PROCESS", "加工工序");
            return ItemList;
        }
        /// <summary>
        /// 重置条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void reset_Click(object sender, EventArgs e)
        {
            foreach (Control item in select.Controls[1].Controls[0].Controls)
            {
                if (!string.IsNullOrEmpty(item.ID))
                {
                    if (item is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)item).Text = "";
                    }
                    if (item is DropDownList)
                    {
                        if (item.ID.Substring(0, 6).ToString() == "screen")
                        {
                            ((DropDownList)item).SelectedValue = "0";
                        }
                        else if (item.ID.Substring(0, 8).ToString() == "ddlLogic")
                        {
                            ((DropDownList)item).SelectedValue = "OR";
                        }
                        else if (item.ID.Substring(0, 11).ToString() == "ddlRelation")
                        {
                            ((DropDownList)item).SelectedValue = "0";
                        }
                    }
                }
            }
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindRepeater();
            ControlVisible();
        }
        protected void search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindRepeater();
            ControlVisible();
        }
        private string GetStrCondition()
        {
            string strWhere = "";
            if (screen1.SelectedValue != "0" || screen2.SelectedValue != "0" || screen3.SelectedValue != "0" || screen4.SelectedValue != "0" || screen5.SelectedValue != "0" || screen6.SelectedValue != "0")
            {
                if (SelectStr(screen1, ddlRelation1, Txt1.Text, "") != "")
                {
                    strWhere += "and (" + SelectStr(screen1, ddlRelation1, Txt1.Text, "");
                }
                else
                {
                    strWhere += "and (1=1 ";
                }
                strWhere += SelectStr(screen2, ddlRelation2, Txt2.Text, ddlLogic1.SelectedValue);
                strWhere += SelectStr(screen3, ddlRelation3, Txt3.Text, ddlLogic2.SelectedValue);
                strWhere += SelectStr(screen4, ddlRelation4, Txt4.Text, ddlLogic3.SelectedValue);
                strWhere += SelectStr(screen5, ddlRelation5, Txt5.Text, ddlLogic4.SelectedValue);
                strWhere += SelectStr(screen6, ddlRelation6, Txt6.Text, ddlLogic5.SelectedValue);
                strWhere += ")";
            }

            if (txtHTH.Text.Trim() != "")
            {
                strWhere += " and MS_PJID like '%" + txtHTH.Text.Trim() + "%'";
            }

            //我的任务
            if (cb_myjob.Checked)
            {
                strWhere += "and (MTA_DUY='" + Session["UserName"].ToString() + "'or MS_PERSON='" + Session["UserName"].ToString() + "')";
            }

            //已完工
            if (rb_1.Checked)
            {
                strWhere += " and MS_FINISHSTATUS='1'";
            }

            //未完工
            if (rb_2.Checked)
            {
                strWhere += " and MS_FINISHSTATUS='0'";
            }

            //已处理
            if (look_state.SelectedValue == "1")
            {
                strWhere += " and  MS_LOOKSTATUS='1'";
            }
            //未处理
            else if (look_state.SelectedValue == "2")
            {
                strWhere += " and  MS_LOOKSTATUS='0'";
            }

            return strWhere;
        }
        private string SelectStr(DropDownList ddl, DropDownList ddl1, string txt, string logic) //选择条件拼接字符串
        {
            string sqlstr = string.Empty;
            if (ddl.SelectedValue != "0")
            {
                switch (ddl1.SelectedValue)
                {
                    case "0":
                        sqlstr = string.Format("{0} {1} like '%{2}%' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "1":
                        sqlstr = string.Format("{0} {1} not like '%{2}%' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "2":
                        sqlstr = string.Format("{0} {1}='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "3":
                        sqlstr = string.Format("{0} {1}!='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "4":
                        sqlstr = string.Format("{0} {1}>'{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "5":
                        sqlstr = string.Format("{0} {1}>='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "6":
                        sqlstr = string.Format("{0} {1}<'{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "7":
                        sqlstr = string.Format("{0} {1}<='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                }
            }
            return sqlstr;
        }
        protected void btn_Confirm_Click(object sender, EventArgs e)
        {
            if (rblstate.SelectedValue == "1")
            {
                string[] sfqu_ck = ((LinkButton)sender).CommandArgument.ToString().Split(',');
                string msid = sfqu_ck[0].Trim();
                string mp_ck_bt = sfqu_ck[1].Trim();
                string sql_ck_qr = "update TBPM_MSCHANGERVW set MS_CK_BT='2' where  MS_ID='" + msid + "' and MS_STATE='8'";
                DBCallCommon.ExeSqlText(sql_ck_qr);
            }
            else if (rblstate.SelectedValue == "0")
            {
                string[] sfqu_ck = ((LinkButton)sender).CommandArgument.ToString().Split(',');
                string msid = sfqu_ck[0].Trim();
                string mp_ck_bt = sfqu_ck[1].Trim();
                string sql_ck_qr = "update TBPM_MSFORALLRVW set MS_CK_BT='2' where  MS_ID='" + msid + "' and MS_STATE='8'";
                DBCallCommon.ExeSqlText(sql_ck_qr);
            }
            InitVar();
            this.bindRepeater();
            ControlVisible();
        }

        //已处理或未处理
        protected void Look_State_Change(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.bindRepeater();
            ControlVisible();
        }

    }
}
