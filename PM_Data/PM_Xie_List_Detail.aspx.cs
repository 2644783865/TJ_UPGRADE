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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_List_Detail : System.Web.UI.Page
    {
        string id = "";
        string engid = "";
        string action = "";
        string look = "";
        string num = "";
        PagerQueryParam pager = new PagerQueryParam();
        public string tablename
        {
            get
            {
                string str;
                if (id.Contains("BG"))
                {
                    str = "View_TM_MSCHANGERVW";
                }
                else
                {
                    str = "View_TM_MSFORALLRVW";
                }
                return str;
            }
            set { tablename = value; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["mnpid"];
            action = Request.QueryString["action"];
            num = Request.QueryString["num"];
            InitVar(id);
            InitVarPager();
            if (!IsPostBack)
            {
                initinfo();
                InitVar(id);
                GetSele();
                InitVarPager();
                GetManutAssignData(); //数据绑定
            }
            if (action == "look")
            {
                addscwx.Visible = false;
                tab_person.Visible = false;
                td_choose.Visible = false;
                btnChuli.Visible = true;
            }
        }

        private void initinfo()
        {
            string sqltext = "";
            sqltext = "select ST_ID,ST_NAME from TBDS_STAFFINFO WHERE ST_DEPID='04'and ST_PD='0'order by ST_ID DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cob_fuziren.DataSource = dt;
            cob_fuziren.DataTextField = "ST_NAME";
            cob_fuziren.DataValueField = "ST_ID";
            cob_fuziren.DataBind();
            cob_sqren.DataSource = dt;
            cob_sqren.DataTextField = "ST_NAME";
            cob_sqren.DataValueField = "ST_ID";
            cob_sqren.DataBind();
            cob_fuziren.SelectedValue = Session["UserID"].ToString();
            cob_sqren.SelectedValue = Session["UserID"].ToString();
            TextBoxexecutor.Text = Session["UserName"].ToString();
            TextBoxexecutorid.Text = Session["UserID"].ToString();
            TextBoxexecutor.Enabled = false;
        }
        protected void InitVar(string str)
        {
            string sqlselect = "select MS_ID,MS_PJID,MS_ENGID,MS_ENGNAME,MS_SUBMITNM,MS_CHILDENGNAME,MS_SUBMITTM,MS_ADATE,MS_PJNAME from " + tablename + " where MS_ID='" + id + "'";
            //string sqlselect = "select MS_ID,MS_PID,MS_PJID,MS_ENGID,MS_ENGNAME,MS_CHILDENGNAME,ST_NAME,MS_SUBMITTM,MS_ADATE,MS_PJNAME,CM_PROJ from View_TM_TASKDQO where MS_PID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlselect);
            if (dt.Rows.Count > 0)
            {
                tsa_id.Text = dt.Rows[0]["MS_ENGID"].ToString();
                engid = dt.Rows[0]["MS_ENGID"].ToString();
                lab_proname.Text = dt.Rows[0]["MS_PJID"].ToString();
                lab_pjid.Text = dt.Rows[0]["MS_PJID"].ToString();
                lab_engname.Text = dt.Rows[0]["MS_CHILDENGNAME"].ToString();
                ms_no.Text = dt.Rows[0]["MS_ID"].ToString();
                txt_plandate.Text = dt.Rows[0]["MS_SUBMITTM"].ToString();
                lbltcname.Text = dt.Rows[0]["MS_SUBMITNM"].ToString();
                lab_proj.Text = dt.Rows[0]["MS_PJNAME"].ToString();
            }
        }
        private void InitVarPager()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager.TableName = "View_TM_TASKDQO";
            pager.PrimaryKey = "MS_ID";
            pager.ShowFields = "*";
            pager.OrderField = "dbo.f_formatstr(MS_ZONGXU, '.')";
            pager.StrWhere = GetStrCondition();
            pager.OrderType = 0;//按任务名称降序排列
            pager.PageSize = Convert.ToInt32(ddl_xianshi.SelectedValue);
        }
        private string GetStrCondition()
        {
            string strWhere = " MS_PID='" + id + "'";
            if (screen1.SelectedValue != "0" || screen2.SelectedValue != "0" || screen3.SelectedValue != "0" || screen4.SelectedValue != "0" || screen5.SelectedValue != "0" || screen6.SelectedValue != "0" || screen7.SelectedValue != "0" || screen8.SelectedValue != "0" || screen9.SelectedValue != "0" || screen10.SelectedValue != "0")
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
                strWhere += SelectStr(screen7, ddlRelation7, Txt7.Text, ddlLogic6.SelectedValue);
                strWhere += SelectStr(screen8, ddlRelation8, Txt8.Text, ddlLogic7.SelectedValue);
                strWhere += SelectStr(screen9, ddlRelation9, Txt9.Text, ddlLogic8.SelectedValue);
                strWhere += SelectStr(screen10, ddlRelation10, Txt10.Text, ddlLogic9.SelectedValue);
                strWhere += ")";
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
        private void Pager_PageChanged(int pageNumber)
        {
            InitPager();
            GetManutAssignData();
        }
        protected void GetManutAssignData()
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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblkeycoms = (Label)e.Row.FindControl("lblkeycoms");
                Label lblwaixie = (Label)e.Row.FindControl("lblwaixie");
                Label lblwxtype = (Label)e.Row.FindControl("lblwxtype");
                if (lblkeycoms.Text.ToString() == "关键部件")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                }
                if (lblwaixie.Text.ToString() == "1" || lblwaixie.Text.ToString() == "2")
                {
                    if (lblwxtype.Text.ToString() == "工序外协")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                        e.Row.Cells[1].Attributes.Add("title", "已生成工序外协计划!");
                    }
                    else
                        if (lblwxtype.Text.ToString() == "成品外协")
                        {
                            e.Row.Cells[1].BackColor = System.Drawing.Color.Blue;
                            e.Row.Cells[1].Attributes.Add("title", "已生成成品外协计划!");
                        }
                }
                else if (lblwaixie.Text.ToString() == "3")
                {
                    if (lblwxtype.Text.ToString() == "工序外协")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
                        e.Row.Cells[1].Attributes.Add("title", "驳回的工序外协计划!");
                    }
                    else
                        if (lblwxtype.Text.ToString() == "成品外协")
                        {
                            e.Row.Cells[1].BackColor = System.Drawing.Color.Pink;
                            e.Row.Cells[1].Attributes.Add("title", "驳回的成品外协计划!");
                        }
                }
            }
        }

        protected void btnChuli_OnClick(object sender, EventArgs e)//处理---事件
        {
            string sqltxt = "select A.*,B.MTA_DUY from View_TM_MSFORALLRVW as A left join TBMP_MANUTSASSGN as B on A.MS_ENGID=B.MTA_ID where MS_ID='" + id + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltxt);
            string mtaduy = dt.Rows[0]["MTA_DUY"].ToString();
            string username = Session["UserName"].ToString();
            if (mtaduy == username)
            {
                string sqltext = "update TBPM_MSFORALLRVW set MS_LOOKSTATUS='1' where MS_ID='" + id + "'and MS_STATE='8'";
                DBCallCommon.ExeSqlText(sqltext);
                //InitVar();
                //this.bindRepeater();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您已成功处理该项目')", true);
                //Response.Write("<script>alert('您已成功处理该项目')</script>");
            }
        }

        protected void btn_move_Click(object sender, EventArgs e)
        {
            int n = 0;
            string sqltext = "";
            List<string> sql = new List<string>();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox chbccheck = (CheckBox)gr.FindControl("chbxcheck");
                Label lblzhujian = (Label)gr.FindControl("lblmsid");
                Label lblwaixie = (Label)gr.FindControl("lblwaixie");
                if ((lblwaixie.Text.ToString() == "" || lblwaixie.Text.ToString() == null) && chbccheck.Checked)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请不要勾选没有生成外协的明细！');", true);
                }
                else
                    if (lblwaixie.Text.ToString() == "1" && chbccheck.Checked)
                    {
                        n++;
                        sqltext = "update View_TM_TASKDQO set MS_scwaixie='0', MS_wxtype='' where MS_ID='" + lblzhujian.Text + "'";
                        sql.Clear();
                        sql.Add(sqltext);
                        DBCallCommon.ExecuteTrans(sql);
                    }

            }
            if (n == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选需要取消外协状态的明细！');", true); return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('勾选的明细已取消外协状态！');", true);
                GetManutAssignData();
            }
        }
        protected void btnproplan_Click(object sender, EventArgs e)
        {
            string sqltext = "";

            string type = "";

            switch (dplscwx_Select.SelectedIndex)
            {
                case 0:
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择生产外协类型！！！');", true); return;
                case 1:
                    type = "工序外协"; break;
                case 2:
                    type = "成品外协"; break;
            }
            int n = 0;
            int i=0;
            int j = 0;
            string doc = "";
            if (action != "add")
            { doc = Convert.ToString(addDocNum()); }
            string id = ms_no.Text.Trim();
            
            List<string> sqlstr = new List<string>();
            List<string> list=new List<string>();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox chbxcheck = (CheckBox)gr.FindControl("chbxcheck");
                Label lblzhujian = (Label)gr.FindControl("lblmsid");
                Label lblmarid = (Label)gr.FindControl("lblmarid");
                Label lblstatus = (Label)gr.FindControl("lblstatus");
                Label lblwaixie = (Label)gr.FindControl("lblwaixie");
                if (chbxcheck.Checked)
                {
                    if (action == "add" && (lblwaixie.Text == "0" || lblwaixie.Text == "3"))
                    {
                        i++;
                        sqltext = "update TBMP_TASKDQO set MS_wxtype='" + dplscwx_Select.SelectedValue.ToString() + "',MS_scwaixie='1' where MS_ID='" + lblzhujian.Text + "'and MS_PID='" + ms_no.Text.ToString() + "'";
                        list.Add(sqltext);
                        string sqltext2 = "insert into TBPM_WXDetail select '" + num + "',*,'' from TBMP_TASKDQO where MS_ID='" + lblzhujian.Text + "'and MS_PID='" + ms_no.Text.ToString() + "'";
                        list.Add(sqltext2);
                    }
                    else
                    {
                        if (lblwaixie.Text == "0" || lblwaixie.Text == "3")//未提交外协或者提交之后被驳回的
                        {
                            n++;
                            sqltext = "update TBMP_TASKDQO set MS_wxtype='" + dplscwx_Select.SelectedValue.ToString() + "',MS_scwaixie='1' where MS_ID='" + lblzhujian.Text + "'and MS_PID='" + ms_no.Text.ToString() + "'";
                           // DBCallCommon.ExeSqlText(sqltext);
                            sqlstr.Add(sqltext);
                            string sqltext1 = "insert into TBPM_WXDetail select '', '" + doc + "',*,'','0' from TBMP_TASKDQO where MS_ID='" + lblzhujian.Text + "'and MS_PID='" + ms_no.Text.ToString() + "'";
                            sqlstr.Add(sqltext1);
                           // DBCallCommon.ExecuteTrans(sqlstr);
                        }
                        else //已经提交外协的需要二次外协的 MS_WXSTATUS=1二次外协标记
                        {
                            j++;
                            string sqltext2 = "insert into TBPM_WXDetail select '', '" + doc + "',*,'','1' from TBMP_TASKDQO where MS_ID='" + lblzhujian.Text + "'and MS_PID='" + ms_no.Text.ToString() + "'";
                            sqlstr.Add(sqltext2);

                           
                        }

                    }
                }
            }
            if (action == "add")// 追加的外协计划
            {
                if (i == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选可以生成生产外协计划的明细！');", true);
                }
                else
                {
                    DBCallCommon.ExecuteTrans(list);
                    Response.Redirect("PM_Xie_Audit.aspx?action=Editor&id=" + num + "");
                }
            }
            else
            {
                if (n == 0&&j==0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选可以生成生产外协计划的明细！');", true); 
                }
                else if (n>0&&j==0)
                {
                    //新增
                    sqltext = "update "+tablename+" set MS_WAIXIE='1'where MS_ID='" + id + "'";
                   // sqlstr.Clear();
                    sqlstr.Add(sqltext);
                 
                    sqltext = "insert into TBPM_SCWXRVW values ('" + doc + "','" + ms_no.Text.ToString().Trim() + "','" + lab_pjid.Text.ToString().Trim() + "','" + engid + "','" + TextBoxexecutorid.Text.ToString().Trim() + "','" + DateTime.Now.ToString() + "','" + cob_sqren.SelectedValue.ToString() + "','" + cob_fuziren.SelectedValue.ToString() + "','','0','')";
                    sqlstr.Add(sqltext);
                    DBCallCommon.ExecuteTrans(sqlstr);
                    sqltext = "update TBPM_WXDetail set MS_CODE= CAST (MS_WSID AS VARCHAR)+'-'+CAST(ID AS VARCHAR) where MS_WSID='" +doc+ "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    Response.Redirect("PM_Xie_Audit.aspx?action=Editor&id=" + doc + "");
                }
                else if (j!=0&&n==0)//有二次外协的任务
                {
                    sqltext = "insert into TBPM_SCWXRVW values ('" + doc + "','" + ms_no.Text.ToString().Trim() + "','" + lab_pjid.Text.ToString().Trim() + "','" + engid + "','" + TextBoxexecutorid.Text.ToString().Trim() + "','" + DateTime.Now.ToString() + "','" + cob_sqren.SelectedValue.ToString() + "','" + cob_fuziren.SelectedValue.ToString() + "','','0','')";
                    sqlstr.Add(sqltext);
                    DBCallCommon.ExecuteTrans(sqlstr);
                    sqltext = "update TBPM_WXDetail set MS_CODE= CAST (MS_WSID AS VARCHAR)+'-'+CAST(ID AS VARCHAR) where MS_WSID='" + doc + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    sqltext = "update TBPM_WXDetail set MS_scwaixie='1' where MS_WSID='" + doc + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    Response.Redirect("PM_Xie_Audit.aspx?action=Editor&id=" + doc + "");
                }
                else if (j != 0 && n != 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勿将一次外协和二次外协的明细一起提交！');", true);               
                }
            }
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
            InitPager();
            GetManutAssignData();
        }
        protected void search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitPager();
            GetManutAssignData();
        }
        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            CheckBoxSelectDefine(GridView1, "chbxcheck");
        }
        /// <summary>
        /// CheckBox连选(此函数勿动)
        /// </summary>
        /// <param name="smartgridview"></param>
        /// <param name="ckbname"></param>
        public void CheckBoxSelectDefine(GridView smartgridview, string ckbname)
        {
            int startindex = -1;
            int endindex = -1;
            for (int i = 0; i < smartgridview.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)smartgridview.Rows[i].FindControl(ckbname);
                if (cbx.Checked)
                {
                    startindex = i;
                    break;
                }
            }

            for (int j = smartgridview.Rows.Count - 1; j > -1; j--)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)smartgridview.Rows[j].FindControl(ckbname);
                if (cbx.Checked)
                {
                    endindex = j;
                    break;
                }
            }

            if (startindex < 0 || endindex < 0 || startindex == endindex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('需要勾选2条记录！！！');", true);
            }
            else
            {
                for (int k = startindex; k <= endindex; k++)
                {
                    System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)smartgridview.Rows[k].FindControl(ckbname);
                    cbx.Checked = true;
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
        protected void btn_allpush_Click(object sender, EventArgs e)
        {
            ////新增
            //List<string> sqlstr = new List<string>();
            //string sqltext = "";
            //string sqlText = "";
            //string doc = "";
            //doc = Convert.ToString(addDocNum());
            //sqltext = "update TBPM_MSFORALLRVW set MS_WAIXIE='1'where MS_ID='" + id + "'";
            //sqlstr.Clear();
            //sqlstr.Add(sqltext);
            //sqlText = "insert into TBPM_SCWXRVW values ('" + doc + "','" + ms_no.Text.ToString().Trim() + "','" + lab_pjid.Text.ToString().Trim() + "','" + engid + "','" + TextBoxexecutorid.Text.ToString().Trim() + "','" + DateTime.Now.ToString() + "','" + cob_sqren.SelectedValue.ToString() + "','" + cob_fuziren.SelectedValue.ToString() + "','','0')";
            //sqlstr.Add(sqlText);
            //DBCallCommon.ExecuteTrans(sqlstr);
            //Response.Redirect("PM_Xie_Audit.aspx?action=Editor&id=" + doc + "");
        }
        private int addDocNum()
        {
            int docnum;
            string sqlselect = " SELECT max(PM_DocuNum) as doc FROM  TBPM_SCWXRVW";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlselect);
            if (dt.Rows[0]["doc"].ToString() != "" && dt.Rows[0]["doc"].ToString() != null)
            {
                docnum = Convert.ToInt32(dt.Rows[0]["doc"].ToString());
                docnum++;
            }
            else
            {
                docnum = 1;
            }
            return docnum;
        }
        private Dictionary<string, string> bindItemList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            //ItemList.Add("NO", "");
            ItemList.Add("0", "请选择");
            ItemList.Add("MS_NAME", "名称");
            ItemList.Add("MS_TUHAO", "图号");
            ItemList.Add("MS_CAIZHI", "材质");
            ItemList.Add("MS_MASHAPE", "材料类型");
            ItemList.Add("MS_PROCESS", "加工工序");
            return ItemList;
        }

        protected void ddl_xianshi_change(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitPager();
            GetManutAssignData();
        }
    }
}
