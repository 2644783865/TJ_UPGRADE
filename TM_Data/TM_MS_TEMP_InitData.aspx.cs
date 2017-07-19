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
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.TM_Data
{
   
    public partial class TM_MS_TEMP_InitData : System.Web.UI.Page
    {
        string sqlText;
        string tablename = "TBPM_STRINFODQO";
        string view_tablename = "View_TM_DQO";
        string mstable = "TBPM_MKDETAIL";
        string mptable="TBPM_MPPLAN";
        int count = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.DataBind();
                this.InitInfo();
                Session["MSNorSubmited"] = "未提交";//防止在后面页刷新导致的提交
                this.Form.DefaultButton = btnDataSave.UniqueID;
                hylKUCheck.NavigateUrl = "TM_WeightKuCheck.aspx?TaskID=" + tsaid.Text.Trim();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
        }
        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            this.InitVar();
            this.bindGrid();
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitInfo()
        {
            //所有控件状态初始化
            ddlParts.SelectedIndex = 0;
            ddlinMS.SelectedIndex = 0;
            ddlXH_ZX.SelectedIndex = 0;
            txtXH_ZX.Text = "";
            ddlKU.SelectedIndex = 0;
            ddlSort.SelectedIndex = 1;//总序排序

            if (Request.QueryString["NorMS"] != null)//正常:制作明细序号$生产制号$原始数据表名
            {
                string[] array = Request.QueryString["NorMS"].ToString().Split('$');
                tsaid.Text = array[array.Length - 2];
                ViewState["strtable"] = array[array.Length - 1];
                this.partsName(array);
                this.AllXuhao_Sql(array);
            }
            //获取项目名称，工程名称，设备类型等
            sqlText = "select TSA_PJID,TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsaid.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                pro_id.Value = dr[0].ToString();
                proname.Text = dr[1].ToString();
                engname.Text = dr[2].ToString();
                eng_type.Value = dr[3].ToString();
            }
            dr.Close();

            //读取台数
            string sql = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" +tsaid.Text.Split('-')[0] + "'";
            SqlDataReader dr_number = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_number.HasRows)
            {
                dr_number.Read();
                lblNumber.Text = dr_number["TSA_NUMBER"].ToString();
                dr_number.Close();
            }
            //初始化数据绑定
            ViewState["sqlText"] = ViewState["sql_forAll"];
            UCPaging2.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
            this.GetFyZzData();

            hdfTableName.Value = view_tablename;
            hdfTaskid.Value = tsaid.Text.Trim();
        }
        /// <summary>
        /// 用于前台绑定
        /// </summary>
        /// <returns></returns>
        public string view_table()
        {
           
            return view_tablename;
        }
        public string PassedQueryString
        {
            get
            {
                return Request.QueryString["NorMS"].ToString();
            }
        }
        /// <summary>
        /// 绑定发运组装
        /// </summary>
        private void GetFyZzData()
        {
           
            sqlText = "select distinct BM_KU from " + view_tablename + " where BM_ENGID='" + tsaid.Text + "' and BM_KU is not null ";
            string dataText = "BM_KU";
            string dataValue = "BM_KU";
            DBCallCommon.BindDdl(ddlKU, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 生产全部查询的where条件，按序号查询，多个
        /// </summary>
        /// <returns></returns>
        private void AllXuhao_Sql(string[] array_xuhao)
        {
            StringBuilder strb_sql = new StringBuilder();
            string[] fbj = array_xuhao[0].Split('.');
            //查询序号数组对应的总序
            string allxuhaostring = Request.QueryString["NorMS"].ToString().Substring(0, Request.QueryString["NorMS"].ToString().Substring(0, Request.QueryString["NorMS"].ToString().LastIndexOf('$')).LastIndexOf('$'));
            string[] array_xuhao_cal = allxuhaostring.Split('$');
            string allxuhaowhere = "'" + allxuhaostring.Replace("$", "','") + "'";
            string sql_allzongxu = "select BM_ZONGXU from " + ViewState["strtable"] + " where BM_ENGID='" + tsaid.Text.Trim() + "' AND BM_XUHAO in(" + allxuhaowhere + ")";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_allzongxu);
            string[] array_zongxu=new string[dt.Rows.Count];

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                array_zongxu[j] = dt.Rows[j]["BM_ZONGXU"].ToString();
            }

            //调整数据读取字符串
            strb_sql.Append(" BM_ENGID='" + tsaid.Text + "' and (BM_XUHAO like '" + array_xuhao[0] + ".%' OR BM_XUHAO='" + array_xuhao[0] + "' or BM_XUHAO='" + fbj[0] + "'");
            //该序号下的数据
            if (array_xuhao.Length > 3)
            {
                for (int i = 0; i < array_xuhao.Length - 3; i++)
                {
                    strb_sql.Append(" or BM_XUHAO like '" + array_xuhao[i + 1] + ".%' OR BM_XUHAO='"+array_xuhao[i+1]+"'");
                }
            }

            //该总序下的数据
            for (int k = 0; k < array_zongxu.Length; k++)
            {
                if (array_zongxu[k] != "")
                {
                    strb_sql.Append(" or BM_ZONGXU='" + array_zongxu[k] + "' or BM_ZONGXU like '" + array_zongxu[k] + ".%'");
                }
            }
            //未提交的正常记录
            string[] aa = tsaid.Text.Split('-');
            string virtualIndex="1.0";
            strb_sql.Append(") and BM_MSSTATE='0' and BM_MSSTATUS='0' and BM_XUHAO<>'"+virtualIndex+"'");
            ViewState["sql_forAll"] = strb_sql.ToString();

            //用于计算的sql
            ViewState["sql_forCal"] = "";
            foreach (string xuhao in array_xuhao_cal)
            {
                ViewState["sql_forCal"] += " BM_XUHAO=''" + xuhao + "'' OR BM_XUHAO LIKE ''" + xuhao + ".%''  OR ";
            }
            ViewState["sql_forCal"] = ViewState["sql_forCal"].ToString().Substring(0, ViewState["sql_forCal"].ToString().Length - 3);

        }
        /// <summary>
        /// 定义制作明细调整表
        /// </summary>
        /// <returns></returns>
        protected DataTable GetDataFrom()
        {
            DataTable dt1 = new DataTable("Table1");
            dt1.Columns.Add("BM_MSXUHAO");
            dt1.Columns.Add("BM_XUHAO");
            dt1.Columns.Add("BM_TUHAO");
            dt1.Columns.Add("BM_ZONGXU");
            dt1.Columns.Add("BM_CHANAME");
            dt1.Columns.Add("BM_ISMANU");
            dt1.Columns.Add("BM_KU");
            dt1.Columns.Add("BM_GUIGE");
            dt1.Columns.Add("NUMBER");
            dt1.Columns.Add("BM_PROCESS");
            dt1.Columns.Add("BM_NOTE");
            dt1.Columns.Add("BM_MAQUALITY");
            dt1.Columns.Add("BM_UNITWGHT");
            dt1.Columns.Add("BM_TOTALWGHT");
            dt1.Columns.Add("BM_MASHAPE");
            dt1.Columns.Add("BM_MASTATE");
            dt1.Columns.Add("BM_STANDARD");
            dt1.Columns.Add("BM_KEYCOMS");
            dt1.Columns.Add("BM_ID");
            dt1.Columns.Add("BM_OSSTATE");

            dt1.Columns["BM_ISMANU"].DefaultValue = "N";

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt1.NewRow();
                newRow[0] = ((HtmlInputText)gRow.FindControl("MsIndex")).Value;//明细序号
                newRow[1] = ((HtmlInputText)gRow.FindControl("Index")).Value;//原始序号
                newRow[2] = ((HtmlInputText)gRow.FindControl("tuhao")).Value;//图号(标识号)
                newRow[3] = ((HtmlInputText)gRow.FindControl("zongxu")).Value;//总序
                newRow[4] = ((TextBox)gRow.FindControl("txtName")).Text;//名称
                newRow[5] = ((DropDownList)gRow.FindControl("ddlISMANU")).SelectedValue;//是否体现
                newRow[6] = ((HtmlInputText)gRow.FindControl("ku")).Value;//库
                newRow[7] = ((HtmlInputText)gRow.FindControl("guige")).Value;//规格
                newRow[8] = gRow.Cells[10].Text == "&nbsp;" ? "" : gRow.Cells[10].Text;//数量

                newRow[9] = ((HtmlInputText)gRow.FindControl("process")).Value;//工艺流程
                newRow[10] = ((HtmlInputText)gRow.FindControl("beizhu")).Value;//备注

                newRow[11] = gRow.Cells[13].Text == "&nbsp;" ? "" : gRow.Cells[13].Text;//材质
                newRow[12] = gRow.Cells[14].Text == "&nbsp;" ? "" : gRow.Cells[14].Text;//单重(kg)
                newRow[13] = gRow.Cells[15].Text == "&nbsp;" ? "" : gRow.Cells[15].Text;//总重(kg)
                newRow[14] = gRow.Cells[16].Text == "&nbsp;" ? "" : gRow.Cells[16].Text;//毛坯
                newRow[15] = gRow.Cells[17].Text == "&nbsp;" ? "" : gRow.Cells[17].Text;//状态
                newRow[16] = gRow.Cells[18].Text == "&nbsp;" ? "" : gRow.Cells[18].Text;//国标
                newRow[17] = gRow.Cells[19].Text == "&nbsp;" ? "" : gRow.Cells[19].Text;//关键部件
                newRow[18] = ((HtmlInputHidden)gRow.FindControl("hdfID")).Value; //ID
                newRow[19] = ((HtmlInputHidden)gRow.FindControl("hdfOSSTATE")).Value;
                dt1.Rows.Add(newRow);
            }

            dt1.AcceptChanges();
            return dt1;
        }
        /// <summary>
        /// 删除一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndelete_Click(object sender, EventArgs e)
        {
            DataTable dt = this.GetDataFrom();
            for (int i = int.Parse(txtid.Value) - 1; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gRow.FindControl("chk");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
        /// <summary>
        /// 插入空行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btninsert_Click(object sender, EventArgs e)
        {
            if (Session["MSNorSubmited"].ToString() == "未提交")
            {
                string ret = this.GridViewDataSave();
                string flag = ret.Substring(0, 1);
                string index = ret.Substring(1);
                if (flag == "0")//记录不存在
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行记录不存在！！！');", true);
                }
                else if (flag == "1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行明细序号已存在！！！');", true);
                }
                else if (flag == "2")//空
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行序号、总序或数量不能为空！！！');", true);
                }
                else if (flag == "3")//正常
                {
                    count = int.Parse(istid.Value);
                    if (count > 0)
                    {
                        DataTable dt = this.GetDataFrom();
                        DataRow newRow = dt.NewRow();
                        dt.Rows.InsertAt(newRow, count);
                        istid.Value = "0";
                        this.GridView1.DataSource = dt;
                        this.GridView1.DataBind();
                    }
                }
                else if (ret.Contains("Page-"))//页面上存在底层材料归属
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"与页面上其父序均为底层材料！！！');", true);
                }
                else if (ret.Contains("DataBase-"))//页面上记录与数据库中存在归属
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"归属到数据库中的父序为底层材料！！！');", true);
                }
                else if (ret.Contains("FormError-"))//总序格式错误
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"格式错误！！！\\r\\r正确格式：1或1.m（m为整数）');", true);
                }
                else if (ret.Contains("PageRepeat-"))//页面上序号重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"重复！！！');", true);
                }
                else if (ret.Contains("DataBaseRepeat-"))//页面序号与数据库中重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"与数据库中重复！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已提交,即将关闭当前窗口！！！');window.close();", true);
            }
        }
        /// <summary>
        /// 复制插入总序对应的数据(1/6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hdbtn_Click(object sender, EventArgs e)
        {
            if (Session["MSNorSubmited"].ToString() == "未提交" || Session["MSNorSubmited"]==null)
            {
                count = int.Parse(index.Value) - 1;
                GridViewRow gr = GridView1.Rows[count];
             
                string zx = zongxu.Value.ToString();
                //获取虚拟部件号
                string[] tsplit = tsaid.Text.Trim().Split('-');
                string xulibujhao = tsplit[1].ToString() + ".0";
                if (zx != xulibujhao)
                {
                    if (Request.QueryString["NorMS"] != null)//正常
                    {
                        sqlText = "select BM_ID,BM_ZONGXU,BM_XUHAO,BM_TUHAO,BM_CHANAME,BM_GUIGE,BM_MAQUALITY,BM_NUMBER,BM_UNITWGHT,";
                        sqlText += "BM_TOTALWGHT,BM_MASHAPE,BM_MASTATE,BM_KU,BM_STANDARD,BM_PROCESS,BM_KEYCOMS,BM_ISMANU,BM_NOTE,BM_MSXUHAO ";
                        sqlText += "from " + view_tablename + " where BM_MSSTATE='0'  and BM_MSSTATUS='0' and BM_ENGID='" + tsaid.Text + "' and BM_ZONGXU='" + zongxu.Value + "' ";
                    }

                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                    if (dt.Rows.Count == 1)
                    {
                        ((HtmlInputText)gr.FindControl("MsIndex")).Value = dt.Rows[0]["BM_MSXUHAO"].ToString();//明细序号
                        ((HtmlInputText)gr.FindControl("Index")).Value = dt.Rows[0]["BM_XUHAO"].ToString();//原始序号
                        ((HtmlInputText)gr.FindControl("tuhao")).Value = dt.Rows[0]["BM_TUHAO"].ToString();//图号(标识号)
                        ((HtmlInputText)gr.FindControl("zongxu")).Value = dt.Rows[0]["BM_ZONGXU"].ToString();//总序
                        ((TextBox)gr.FindControl("txtName")).Text = dt.Rows[0]["BM_CHANAME"].ToString();//名称
                        ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = dt.Rows[0]["BM_ISMANU"].ToString();//是否体现
                        ((HtmlInputText)gr.FindControl("ku")).Value = dt.Rows[0]["BM_KU"].ToString();//库
                        ((HtmlInputText)gr.FindControl("guige")).Value = dt.Rows[0]["BM_GUIGE"].ToString();//规格
                        gr.Cells[10].Text = dt.Rows[0]["BM_NUMBER"].ToString();//数量
                        ((HtmlInputText)gr.FindControl("process")).Value = dt.Rows[0]["BM_PROCESS"].ToString();//工艺流程
                        ((HtmlInputText)gr.FindControl("beizhu")).Value = dt.Rows[0]["BM_NOTE"].ToString();//备注
                        gr.Cells[13].Text = dt.Rows[0]["BM_MAQUALITY"].ToString();//材质
                        gr.Cells[14].Text = dt.Rows[0]["BM_UNITWGHT"].ToString();//单重(kg)
                        gr.Cells[15].Text = dt.Rows[0]["BM_TOTALWGHT"].ToString();//总重(kg)
                        gr.Cells[16].Text = dt.Rows[0]["BM_MASHAPE"].ToString();//毛坯
                        gr.Cells[17].Text = dt.Rows[0]["BM_MASTATE"].ToString();//状态
                        gr.Cells[18].Text = dt.Rows[0]["BM_STANDARD"].ToString();//国标
                        gr.Cells[19].Text = dt.Rows[0]["BM_KEYCOMS"].ToString();//关键部件
                        ((HtmlInputHidden)gr.FindControl("hdfID")).Value = dt.Rows[0]["BM_ID"].ToString();//ID
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        ((HtmlInputText)gr.FindControl("MsIndex")).Value = "";//明细序号
                        ((HtmlInputText)gr.FindControl("Index")).Value = "";//原始序号
                        ((HtmlInputText)gr.FindControl("tuhao")).Value = "";//图号(标识号)
                        /////////((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                        ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                        ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                        ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                        ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                        gr.Cells[10].Text = "";//数量
                        ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                        ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                        gr.Cells[13].Text = "";//材质
                        gr.Cells[14].Text = "";//单重(kg)
                        gr.Cells[15].Text = "";//总重(kg)
                        gr.Cells[16].Text = "";//毛坯
                        gr.Cells[17].Text = "";//状态
                        gr.Cells[18].Text = "";//国标
                        gr.Cells[19].Text = "";//关键部件
                        ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID
                        if (Request.QueryString["NorMS"] != null)//正常,无数据
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该总序不存在或已提交！！！');", true);
                        }
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        string zongxu_chongfu = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            zongxu_chongfu += "【" + dt.Rows[i]["BM_XUHAO"].ToString() + "】";
                        }
                        cbxXuhaoCopy.Checked = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该总序已拆分，请用序号输入！！！\\r\\r提示：\\r\\r重复总序对应序号：" + zongxu_chongfu + "');", true);
                    }
                }
                else
                {
                    ((HtmlInputText)gr.FindControl("MsIndex")).Value = "";//明细序号
                    ((HtmlInputText)gr.FindControl("Index")).Value = "";//原始序号
                    ((HtmlInputText)gr.FindControl("tuhao")).Value = "";//图号(标识号)
                    /////////((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                    ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                    ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                    ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                    ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                    gr.Cells[10].Text = "";//数量
                    ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                    ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                    gr.Cells[13].Text = "";//材质
                    gr.Cells[14].Text = "";//单重(kg)
                    gr.Cells[15].Text = "";//总重(kg)
                    gr.Cells[16].Text = "";//毛坯
                    gr.Cells[17].Text = "";//状态
                    gr.Cells[18].Text = "";//国标
                    gr.Cells[19].Text = "";//关键部件
                    ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('虚拟部件无法插入！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已提交,即将关闭当前窗口！！！');window.close();", true);
            }
        }
        /// <summary>
        /// 复制插入序号对应的数据(1/6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hdbtnxuhao_Click(object sender, EventArgs e)
        {
            if (Session["MSNorSubmited"].ToString() == "未提交" || Session["MSNorSubmited"] == null)
            {
                cbxXuhaoCopy.Checked = false;
                lblshortcut.Value = "(当前:无)";
                count = int.Parse(index.Value) - 1;
                GridViewRow gr = GridView1.Rows[count];
               
                string zx = zongxu.Value.ToString();
                //获取虚拟部件号
                string[] tsplit = tsaid.Text.Trim().Split('-');
                string xulibujhao = tsplit[1].ToString() + ".0";
                if (zx != xulibujhao)
                {
                    if (Request.QueryString["NorMS"] != null)//正常
                    {
                        sqlText = "select BM_ID,BM_XUHAO,BM_ZONGXU,BM_TUHAO,BM_CHANAME,BM_GUIGE,BM_MAQUALITY,BM_NUMBER,BM_UNITWGHT,BM_MSXUHAO,BM_ZONGXU,";
                        sqlText += "BM_TOTALWGHT,BM_MASHAPE,BM_MASTATE,BM_KU,BM_STANDARD,BM_PROCESS,BM_KEYCOMS,BM_ISMANU,BM_NOTE ";
                        sqlText += "from " + view_tablename + " where BM_MSSTATE='0'  and BM_MSSTATUS='0' and BM_ENGID='" + tsaid.Text + "' and BM_XUHAO='" + zongxu.Value + "' ";
                    }
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    if (dt.Rows.Count == 1)
                    {
                        ((HtmlInputText)gr.FindControl("MsIndex")).Value = dt.Rows[0]["BM_MSXUHAO"].ToString();//明细序号
                        ((HtmlInputText)gr.FindControl("Index")).Value = dt.Rows[0]["BM_XUHAO"].ToString();//原始序号
                        ((HtmlInputText)gr.FindControl("tuhao")).Value = dt.Rows[0]["BM_TUHAO"].ToString();//图号(标识号)
                        ((HtmlInputText)gr.FindControl("zongxu")).Value = dt.Rows[0]["BM_ZONGXU"].ToString();//总序
                        ((TextBox)gr.FindControl("txtName")).Text = dt.Rows[0]["BM_CHANAME"].ToString();//名称
                        ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = dt.Rows[0]["BM_ISMANU"].ToString();//是否体现
                        ((HtmlInputText)gr.FindControl("ku")).Value = dt.Rows[0]["BM_KU"].ToString();//库
                        ((HtmlInputText)gr.FindControl("guige")).Value = dt.Rows[0]["BM_GUIGE"].ToString();//规格
                        gr.Cells[10].Text = dt.Rows[0]["BM_NUMBER"].ToString();//数量
                        ((HtmlInputText)gr.FindControl("process")).Value = dt.Rows[0]["BM_PROCESS"].ToString();//工艺流程
                        ((HtmlInputText)gr.FindControl("beizhu")).Value = dt.Rows[0]["BM_NOTE"].ToString();//备注
                        gr.Cells[13].Text = dt.Rows[0]["BM_MAQUALITY"].ToString();//材质
                        gr.Cells[14].Text = dt.Rows[0]["BM_UNITWGHT"].ToString();//单重(kg)
                        gr.Cells[15].Text = dt.Rows[0]["BM_TOTALWGHT"].ToString();//总重(kg)
                        gr.Cells[16].Text = dt.Rows[0]["BM_MASHAPE"].ToString();//毛坯
                        gr.Cells[17].Text = dt.Rows[0]["BM_MASTATE"].ToString();//状态
                        gr.Cells[18].Text = dt.Rows[0]["BM_STANDARD"].ToString();//国标
                        gr.Cells[19].Text = dt.Rows[0]["BM_KEYCOMS"].ToString();//关键部件
                        ((HtmlInputHidden)gr.FindControl("hdfID")).Value = dt.Rows[0]["BM_ID"].ToString();//ID
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        ((HtmlInputText)gr.FindControl("MsIndex")).Value = "";//明细序号
                        //////////((HtmlInputText)gr.FindControl("Index")).Value = "";//原始序号
                        ((HtmlInputText)gr.FindControl("tuhao")).Value = "";//图号(标识号)
                        ((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                        ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                        ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                        ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                        ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                        gr.Cells[10].Text = "";//数量
                        ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                        ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                        gr.Cells[13].Text = "";//材质
                        gr.Cells[14].Text = "";//单重(kg)
                        gr.Cells[15].Text = "";//总重(kg)
                        gr.Cells[16].Text = "";//毛坯
                        gr.Cells[17].Text = "";//状态
                        gr.Cells[18].Text = "";//国标
                        gr.Cells[19].Text = "";//关键部件
                        ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID
                        if (Request.QueryString["NorMS"] != null)//正常,无数据
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该序号不存在或已提交！！！');", true);
                        }
                    }
                }
                else
                {
                    ((HtmlInputText)gr.FindControl("MsIndex")).Value = "";//明细序号
                    //////////((HtmlInputText)gr.FindControl("Index")).Value = "";//原始序号
                    ((HtmlInputText)gr.FindControl("tuhao")).Value = "";//图号(标识号)
                    ((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                    ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                    ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                    ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                    ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                    gr.Cells[10].Text = "";//数量
                    ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                    ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                    gr.Cells[13].Text = "";//材质
                    gr.Cells[14].Text = "";//单重(kg)
                    gr.Cells[15].Text = "";//总重(kg)
                    gr.Cells[16].Text = "";//毛坯
                    gr.Cells[17].Text = "";//状态
                    gr.Cells[18].Text = "";//国标
                    gr.Cells[19].Text = "";//关键部件
                    ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('虚拟部件无法插入！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已提交,即将关闭当前窗口！！！');window.close();", true);
            }
        }
        /// <summary>
        /// 点击插入或保存时GridView数据保存
        /// </summary>
        protected string GridViewDataSave()
        {
           
            string ret = CheckMarNotBelongToMar();//序号的相关检查
            if (ret == "0")//验证序号正常
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow grow = GridView1.Rows[i];
                    string existflag = ((HtmlInputHidden)(grow.FindControl("hdfID"))).Value.Trim();
                    if (existflag == "")//如果ID为空，说明插入的为不存在记录
                    {
                        if (Request.QueryString["NorMS"] != null)//正常不存在
                        {
                            ViewState["insertflag"] = "0" + (i + 1).ToString();
                        }
                        break;
                    }
                    else
                    {
                        string id = ((HtmlInputHidden)(grow.FindControl("hdfID"))).Value.Trim();
                        string xuhao = ((HtmlInputText)(grow.FindControl("Index"))).Value.Trim();//序号
                        string tuhao = ((HtmlInputText)(grow.FindControl("tuhao"))).Value.Trim();
                        string zongxu = ((HtmlInputText)(grow.FindControl("zongxu"))).Value.Trim();
                        string ku = ((HtmlInputText)(grow.FindControl("ku"))).Value.Trim();
                        string ismunu = ((DropDownList)grow.FindControl("ddlISMANU")).SelectedValue.ToString();
                        string beizhu = ((HtmlInputText)(grow.FindControl("beizhu"))).Value.Trim();
                        string process = ((HtmlInputText)(grow.FindControl("process"))).Value.Trim();
                        string ms_xuhao = ((HtmlInputText)(grow.FindControl("MsIndex"))).Value.Trim();
                        string guige = ((HtmlInputText)grow.FindControl("guige")).Value.Trim();
                        string name = ((TextBox)grow.FindControl("txtName")).Text.Trim();
                        if (xuhao == "")//不为空
                        {
                            ViewState["insertflag"] = "2" + (i + 1).ToString();
                            break;
                        }
                        else
                        {
                            if (ms_xuhao != "")
                            {
                                // 明细序号不控制—孙利波-2012.10.31
                                //string sql_select_msexist = "select BM_MSXUHAO from " + tablename + " where BM_ENGID='" + tsaid.Text + "' AND BM_MSXUHAO='" + ms_xuhao + "' and BM_ID<>" + id + " and BM_XUHAO like '"+xuhao+".%'";
                                //SqlDataReader dr_select_msexist = DBCallCommon.GetDRUsingSqlText(sql_select_msexist);
                                
                                //if (dr_select_msexist.HasRows)
                                //{
                                //    return "1" + (i + 1).ToString();
                                //}
                            }
                            List<string> list_sql = new List<string>();
                            //找到原序号
                            string old_xuhao = "";
                            string sql_old_xuhao = "select BM_XUHAO from " + tablename + " where BM_ID=" + id + " and BM_ENGID='" + tsaid.Text + "'";
                            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_old_xuhao);
                            dr.Read();
                            old_xuhao = dr["BM_XUHAO"].ToString();
                            dr.Close();
                            //更新原始数据序号，图号
                            string sqltext = "";
                            sqltext = "update " + tablename + " set BM_XUHAO='" + xuhao + "',BM_TUHAO='" + tuhao + "',BM_KU='" + ku + "',BM_ISMANU='" + ismunu + "',BM_MSXUHAO='" + ms_xuhao + "',BM_PROCESS='" + process + "',BM_NOTE='" + beizhu + "',BM_GUIGE='" + guige + "',BM_CHANAME='"+name+"' where BM_ID=" + id + " and BM_ZONGXU='" + zongxu + "'";
                            list_sql.Add(sqltext);
                            //更新原始数据临时表序号
                            sqltext = "update TBPM_TEMPMARDATA set BM_XUHAO ='" + xuhao + "' where BM_ZONGXU='" + zongxu + "' and BM_XUHAO='" + old_xuhao + "' and BM_ENGID='" + tsaid.Text.Trim() + "'";
                            list_sql.Add(sqltext);
                            if (xuhao != old_xuhao)//如果更改了序号
                            {
                                //制作明细序号更新
                                string sql_ms_nor = "update " + mstable + " set MS_NEWINDEX='" + xuhao + "' where MS_ENGID='" + tsaid.Text + "' and MS_NEWINDEX='" + old_xuhao + "'";
                                list_sql.Add(sql_ms_nor);

                                //更新已提材料计划中的序号
                                //正常的材料计划更新 原序号	MP_OLDXUHAO	
                                string sql_mar_nor = "update TBPM_MPFORHZY set MP_NEWXUHAO='" + xuhao + "' where MP_ENGID='" + tsaid.Text + "' and MP_NEWXUHAO='" + old_xuhao + "' and MP_STATUS='0'";
                                list_sql.Add(sql_mar_nor);

                                //变更的材料计划更新 原序号	MP_OLDXUHAO	
                                string sql_mar_chg = "update TBPM_MPCHANGE set MP_OLDXUHAO='" + xuhao + "',MP_NEWXUHAO='" + xuhao + "' where MP_ENGID='" + tsaid.Text + "' and MP_OLDXUHAO='" + old_xuhao + "'";
                                list_sql.Add(sql_mar_chg);


                                //更新已提交外协的序号
                                //正常外协表 原序号	OSL_NEWXUHAO
                                string sql_out_nor = "update TBPM_OUTSOURCELIST set OSL_NEWXUHAO='" + xuhao + "' where OSL_ENGID='" + tsaid.Text + "' and OSL_NEWXUHAO='" + old_xuhao + "' and  OSL_STATUS='0'";
                                list_sql.Add(sql_out_nor);

                                //变更外协表 原序号	OSL_OLDXUHAO = 新序号 OSL_NEWXUHAO
                                string sql_out_chg = "update TBPM_OUTSCHANGE set OSL_OLDXUHAO='" + xuhao + "' where OSL_ENGID='" + tsaid.Text + "' and OSL_OLDXUHAO='" + old_xuhao + "'";
                                list_sql.Add(sql_out_chg);
                            }
                            DBCallCommon.ExecuteTrans(list_sql);
                            ViewState["insertflag"] = "3"; //正常
                        }
                    }
                }
                ret=ViewState["insertflag"].ToString();
            }
            return ret;
        }
        /// <summary>
        /// 保存插入时的验证序号
        /// </summary>
        /// <returns></returns>
        private string CheckMarNotBelongToMar()
        {
            string sql_delete = "delete from TBPM_TEMPORGDATA where BM_ENGID='" + tsaid.Text + "'";
            DBCallCommon.ExeSqlText(sql_delete);//删除表TBPM_TEMPORGDATA中该生产制号下数据，防止意外情况未清空上次记录

            
            string[] a = tsaid.Text.Split('-');
            //////////////////string firstCharofZX = a[a.Length - 1];
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[0-9]+)*)$";
            Regex rgx = new Regex(pattern);
            string ret = "0";
            List<string> list_sql = new List<string>();
            //检验总序格式
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                string  xuhao = ((HtmlInputText)gRow.FindControl("Index")).Value.Trim();
                if (xuhao != "")
                {
                    if (!rgx.IsMatch(xuhao))
                    {
                        ret = "FormError-" + xuhao;//格式不对
                        return ret;
                    }
                    else
                    {
                        string id = ((HtmlInputHidden)gRow.FindControl("hdfID")).Value.Trim();
                        if (id != "")
                        {
                            string sql_mar = "select BM_MARID from " + tablename + " where BM_ENGID='" + tsaid.Text + "' and BM_ID=" + id + "";
                            string mar = "";
                            SqlDataReader dr_mar = DBCallCommon.GetDRUsingSqlText(sql_mar);
                            if (dr_mar.HasRows)
                            {
                                dr_mar.Read();
                                mar = dr_mar["BM_MARID"].ToString();
                            }
                            dr_mar.Close();
                            list_sql.Add("insert into TBPM_TEMPORGDATA(BM_ID,BM_ZONGXU,BM_MARID,BM_ENGID) Values('" + id + "','" + xuhao + "','" + mar + "','" + tsaid.Text.Trim() + "')");
                        }
                        else
                        {
                            ret = "0-"+(i+1).ToString();
                            return ret;
                        }
                    }
                }
            }
            //检验总序输入临时表
            DBCallCommon.ExecuteTrans(list_sql);
            //检验归属关系
            ParamsCheckMarNotBelongToMar pcmar = new ParamsCheckMarNotBelongToMar();
            pcmar.StrTabeleName = tablename;
            pcmar.TaskID = tsaid.Text;
            DataTable dt = this.ExecMarCheck(pcmar);
            ret = dt.Rows[0][0].ToString();
            return ret;
        }
        /// <summary>
        /// 存储过程参数类
        /// </summary>
        private class ParamsCheckMarNotBelongToMar
        {
            private string _StrTabeleName;
            private string _TaskID;
            private string _StrWhere;
            public string StrTabeleName
            {
                get { return _StrTabeleName; }
                set { _StrTabeleName = value; }
            }
            public string TaskID
            {
                get { return _TaskID; }
                set { _TaskID = value; }
            }
            public string StrWhere
            {
                get { return _StrWhere; }
                set { _StrWhere = value; }
            }
        }
        /// <summary>
        /// 执行存储过程验证序号（保存时）
        /// </summary>
        /// <param name="psv"></param>
        private DataTable ExecMarCheck(ParamsCheckMarNotBelongToMar psv)
        {
            DataTable dt;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_CheckMarNotBelongToMarByXuhao]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@StrTable", psv.StrTabeleName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@ENG_ID", psv.TaskID, SqlDbType.Text, 1000);
                sqlConn.Open();
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #region 查询
        /// <summary>
        ///绑定部件名称 
        /// </summary>
        private void partsName(string[] array_xuhao)
        {
            string array_string = "";
            int array_lengh=array_xuhao.Length;
            if (array_lengh == 1)
            {
                array_string = "'" + array_xuhao[0] + "'";
            }
            else
            {
                array_string = "'" + array_xuhao[0] + "'";
                for (int i = 1; i < array_lengh - 2; i++)
                {
                    array_string += ",'" + array_xuhao[i] + "'";
                }
            }
            sqlText = "select BM_CHANAME,BM_XUHAO from " + ViewState["strtable"] + " where BM_ENGID='" + tsaid.Text + "' and BM_XUHAO in("+array_string+")";
            string dataText = "BM_CHANAME";
            string dataValue = "BM_XUHAO";
            DBCallCommon.BindDdl(ddlParts, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 排序方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSort_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataReload();
        }
        /// <summary>
        /// 查询条件改变，重新加载数据
        /// </summary>
        private void DataReload()
        {
            UCPaging2.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void Event_Query(object sender, EventArgs e)
        {
            StringBuilder strb_sql = new StringBuilder();
            strb_sql.Append(ViewState["sql_forAll"].ToString());
            if (ddlParts.SelectedIndex != 0)
            {
                strb_sql.Append(" and (BM_XUHAO='" + ddlParts.SelectedValue + "' OR BM_XUHAO like '" + ddlParts.SelectedValue + ".%')");
            }

            if (ddlinMS.SelectedIndex != 0)
            {
                strb_sql.Append(" and BM_ISMANU='" + ddlinMS.SelectedValue + "'");
            }

            if (ddlOrgJishu.SelectedIndex != 0)
            {
                strb_sql.Append(" AND [dbo].[Splitnum](" + ddlShowType.SelectedValue + ",'.')=" + ddlOrgJishu.SelectedValue + " ");
            }

            if (ddlXH_ZX.SelectedIndex != 0)
            {
                if (ddlXH_ZX.SelectedValue == "BM_TUHAO" || ddlXH_ZX.SelectedValue == "BM_NOTE")
                {
                    strb_sql.Append("  and " + ddlXH_ZX.SelectedValue + " like '%" + txtXH_ZX.Text.Trim() + "%'");
                }
                else if (ddlXH_ZX.SelectedValue == "BM_MSXUHAO")
                {
                    strb_sql.Append( "  and " + ddlXH_ZX.SelectedValue + " like '" + txtXH_ZX.Text.Trim() + "%'");
                }
                else
                {
                    strb_sql.Append("  and (" + ddlXH_ZX.SelectedValue + "='" + txtXH_ZX.Text.Trim() + "' OR " + ddlXH_ZX.SelectedValue + " like '" + txtXH_ZX.Text.Trim() + ".%')");
                }
            }

            if (ddlKU.SelectedIndex != 0)
            {
                strb_sql.Append("  and BM_KU='" + ddlKU.SelectedValue + "'");
            }

            ViewState["sqlText"] = strb_sql.ToString();
            this.DataReload();
        }

        #endregion

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDataSave_Onclick(object sender, EventArgs e)
        {
            if (Session["MSNorSubmited"].ToString() == "未提交")
            {
                string ret;
                string flag;
                string index;
                try
                {
                    ret = this.GridViewDataSave();
                    flag = ret.Substring(0, 1);
                    index = ret.Substring(1);
                }
                catch (Exception error)
                {
                    string message = error.Message.ToString().Replace("\'", "|");
                    message = @"alert('无法保存！！！\r\r错误信息:" + message + "\r\r可能原因:保存后页面上序号与数据中重复！！！');";
                    message = message.Replace("\r\n", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", message, true);
                    return;
                }

                if (flag == "0")//记录不存在
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行记录不存在！！！');", true);
                }
                else if (flag == "1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行明细序号已存在！！！');", true);
                }
                else if (flag == "2")//空
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行序号不能为空！！！');", true);
                }
                else if (flag == "3")//正常
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已保存！');", true);
                }
                else if (ret.Contains("Page-"))//页面上存在底层材料归属
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"与页面上其父序均为底层材料！！！');", true);
                }
                else if (ret.Contains("DataBase-"))//页面上记录与数据库中存在归属
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"归属到数据库中的父序为底层材料！！！');", true);
                }
                else if (ret.Contains("FormError-"))//总序格式错误
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"格式错误！！！\\r\\r正确格式：1或1.m（m为整数）');", true);
                }
                else if (ret.Contains("PageRepeat-"))//页面上序号重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"重复！！！');", true);
                }
                else if (ret.Contains("DataBaseRepeat-"))//页面序号与数据库中重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"与数据库中重复！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已提交,即将关闭当前窗口！！！');window.close();", true);
            }
        }

        /// <summary>
        /// 重量计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_onClick(object sender, EventArgs e)
        {
            if (Session["MSNorSubmited"].ToString() == "未提交")
            {
                string ret = this.GridViewDataSave();
                string flag = ret.Substring(0, 1);
                string index = ret.Substring(1);
                if (flag == "0")//记录不存在
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行记录不存在！！！');", true);
                }
                else if (flag == "2")//空
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行序号、总序或数量不能为空！！！');", true);
                }
                else if (flag == "1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行明细序号已存在！！！');", true);
                }
                else if (flag == "3")//正常--接下来继续验证数据库中每一序号都存在父部件
                {
                   
                    ParamsCheckMarNotBelongToMar parafather = new ParamsCheckMarNotBelongToMar();
                    parafather.StrTabeleName = tablename;
                    parafather.TaskID = tsaid.Text;
                    parafather.StrWhere = ViewState["sql_forCal"].ToString();
                    DataTable dt = this.ExecFatherExistCheck(parafather);
                    string returnValue = dt.Rows[0][0].ToString();
                    if (returnValue == "0")
                    {
                        string sql_pro_calweight = "exec PRO_TM_MSCalWeight '" + tablename + "','" + tsaid.Text.Trim() + "','" + ViewState["sql_forCal"].ToString()+ "'";
                        DBCallCommon.ExeSqlText(sql_pro_calweight);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:已计算!');window.location.reload();", true);
                    }
                    else if (returnValue.Contains("FNotExist-"))//FNotExist-2.1.1-2.1
                    {
                        string[] aa = returnValue.Split('-');
                        string outxuhao = aa[aa.Length - 2].ToString();
                        string outzongxu = aa[aa.Length - 1].ToString();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法计算!\\r\\r序号\"" + outxuhao + "(总序" + outzongxu + ")\"无父部件！！！');", true);
                    }
                }
                else if (ret.Contains("Page-"))//页面上存在底层材料归属
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"与页面上其父序均为底层材料！！！');", true);
                }
                else if (ret.Contains("DataBase-"))//页面上记录与数据库中存在归属
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"归属到数据库中的父序为底层材料！！！');", true);
                }
                else if (ret.Contains("FormError-"))//总序格式错误
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"格式错误！！！\\r\\r正确格式：m或m.n（m,n为整数，m为生产制号拆分序号）');", true);
                }
                else if (ret.Contains("PageRepeat-"))//页面上序号重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"重复！！！');", true);
                }
                else if (ret.Contains("DataBaseRepeat-"))//页面序号与数据库中重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"与数据库中重复！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已提交,即将关闭当前窗口！！！');window.close();", true);
            }
        }

        /// <summary>
        /// 执行存储过程验证序号（计算时）
        /// </summary>
        /// <param name="psv"></param>
        private DataTable ExecFatherExistCheck(ParamsCheckMarNotBelongToMar psv)
        {
            DataTable dt;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_CheckFatherXuhaoExist]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@StrTable", psv.StrTabeleName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@BM_ENGID", psv.TaskID, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@StrWhere", psv.StrWhere, SqlDbType.Text, 1000);
                sqlConn.Open();
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        /// <summary>
        /// 生成明细，下推审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMS_OnClick(object sender, EventArgs e)
        {
            if (Session["MSNorSubmited"].ToString() == "未提交")
            {
                List<string> list_ms_sql = new List<string>();

                string ret = this.GridViewDataSave();
                string flag = ret.Substring(0, 1);
                string index = ret.Substring(1);
                if (flag == "0")//记录不存在
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行记录不存在！！！');", true);
                }
                else if (flag == "1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行明细序号已存在！！！');", true);
                }
                else if (flag == "2")//空
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行序号不能为空！！！');", true);
                }
                else if (flag == "3")//正常--接下来继续验证数据库中每一序号都存在父部件
                {
                   
                    ParamsCheckMarNotBelongToMar parafather = new ParamsCheckMarNotBelongToMar();
                    parafather.StrTabeleName = tablename;
                    parafather.TaskID = tsaid.Text;
                    parafather.StrWhere = ViewState["sql_forCal"].ToString();
                    DataTable dt = this.ExecFatherExistCheck(parafather);
                    string returnValue = dt.Rows[0][0].ToString();
                    if (returnValue == "0") //正常可以提交制作明细
                    {
                        //string sql_pro_calweight = "exec PRO_TM_MSCalWeight '" + tablename + "','" + tsaid.Text.Trim() + "'";
                        //DBCallCommon.ExeSqlText(sql_pro_calweight);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "MsWaitingCreat();", true);
                        Response.Redirect("TM_MS_WaitforCreate.aspx?MSNor=" + PassedQueryString + "");
                    }
                    else if (returnValue.Contains("FNotExist-"))//FNotExist-2.1.1-2.1
                    {
                        string[] aa = returnValue.Split('-');
                        string outxuhao = aa[aa.Length - 2].ToString();
                        string outzongxu = aa[aa.Length - 1].ToString();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法计算!\\r\\r序号\"" + outxuhao + "(总序" + outzongxu + ")\"无父部件！！！');", true);
                    }
                }
                else if (ret.Contains("Page-"))//页面上存在底层材料归属
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"与页面上其父序均为底层材料！！！');", true);
                }
                else if (ret.Contains("DataBase-"))//页面上记录与数据库中存在归属
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"归属到数据库中的父序为底层材料！！！');", true);
                }
                else if (ret.Contains("FormError-"))//总序格式错误
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"格式错误！！！\\r\\r正确格式：m或m.n（m,n为整数，m为生产制号拆分序号）');", true);
                }
                else if (ret.Contains("PageRepeat-"))//页面上序号重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"重复！！！');", true);
                }
                else if (ret.Contains("DataBaseRepeat-"))//页面序号与数据库中重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"与数据库中重复！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已提交,即将关闭当前窗口！！！');window.close();", true);
            }
        }

        #region  分页

        PagerQueryParam pager = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging2.PageMSChanged+= new UCPagingOfMS.PageHandlerOfMS(Pager_PageChanged);
            UCPaging2.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName =view_tablename;
            pager.PrimaryKey = "BM_ID";
            pager.ShowFields = "BM_ID,BM_XUHAO,BM_TUHAO,BM_ZONGXU,BM_CHANAME,BM_GUIGE,BM_MAQUALITY,BM_NUMBER,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) AS NUMBER,BM_UNITWGHT,BM_TOTALWGHT,BM_MASHAPE,BM_MASTATE,BM_KU,BM_STANDARD,BM_PROCESS,BM_KEYCOMS,BM_ISMANU,BM_NOTE,BM_MSXUHAO,BM_OSSTATE";
            pager.OrderField = "dbo.f_formatstr("+ddlSort.SelectedValue+", '.')";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 0;//升序排列
            pager.PageSize = 30;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging2.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging2, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging2.Visible = false;
            }
            else
            {
                UCPaging2.Visible = true;
                UCPaging2.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string html_index = ((HtmlInputText)(e.Row.FindControl("Index"))).Value.Trim();
                string osstate = ((HtmlInputHidden)(e.Row.FindControl("hdfOSSTATE"))).Value.Trim();
                if (html_index.Contains("1.0."))
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[1].ToolTip = "绿色标识为归属到“1.0”的物料或部件！！！";
                }

                if (osstate == "1")
                {
                    /////((HtmlInputText)e.Row.FindControl("tuhao")).Attributes.Add("readOnly", "true");
                    ((HtmlInputText)e.Row.FindControl("tuhao")).Attributes.Remove("ondblclick");
                    ((HtmlInputText)e.Row.FindControl("tuhao")).Attributes.Add("style", "background:yellow");
                }


                e.Row.Cells[10].ToolTip = "双击修改原始数据！！！";
                string url=html_index+' '+tsaid.Text.Trim()+' '+tablename+' '+mptable+' '+mstable+' '+view_tablename;
                e.Row.Cells[10].Attributes.Add("ondblclick", "javascript:openLink('" + url + "')");
                e.Row.Cells[10].Attributes.Add("style", "Cursor:hand");
            }
        }

    }
}