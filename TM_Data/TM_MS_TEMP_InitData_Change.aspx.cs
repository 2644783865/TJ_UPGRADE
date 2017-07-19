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
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_TEMP_InitData_Change : System.Web.UI.Page
    {
        string sqlText;
        int count = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.InitVar();
            }
            if (!IsPostBack)
            {
                this.Page.DataBind();
                this.InitInfo();
                Session["MSNorSubmited"] = "未提交";
                this.Form.DefaultButton = btnDataSave.UniqueID;
                hylKUCheck.NavigateUrl = "TM_WeightKuCheck.aspx?TaskID=" + tsaid.Text.Trim();
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

            tsaid.Text = Request.QueryString["TaskID"].ToString();
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
            string sql = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" + tsaid.Text.Split('-')[0] + "'";
            SqlDataReader dr_number = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_number.HasRows)
            {
                dr_number.Read();
                lblNumber.Text = dr_number["TSA_NUMBER"].ToString();
                dr_number.Close();
            }
            this.InitListName();
            this.AllXuhao_Sql();
            this.partsName();
            //初始化数据绑定
            ViewState["sqlText"] = ViewState["sql_forAll"];
            string a = ViewState["sql_forAll"].ToString();
            UCPaging2.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
          
            hdfTableName.Value = ViewState["view_tablename"].ToString();
            hdfTaskid.Value = tsaid.Text.Trim();
        }
       

        /// <summary>
        /// 用于前台绑定
        /// </summary>
        /// <returns></returns>
        public string view_table()
        {
            return ViewState["view_tablename"].ToString();
        }
        public string PassedQueryString
        {
            get
            {
                return "";
            }
        }
        /// <summary>
        /// 查询出全部变更的（不包含变更删除项）
        /// </summary>
        /// <returns></returns>
        private void AllXuhao_Sql()
        {
            StringBuilder strb_sql = new StringBuilder();
            strb_sql.Append(" BM_ENGID='" + tsaid.Text + "' and (BM_MSSTATUS='2' or BM_MSSTATUS='3') and BM_MSSTATE='0'");//列出增加和修改的项
            string str_xuhao = Request.QueryString["Xuhao"].ToString();
            string[] arrayxuhao = str_xuhao.Split('$');

            string strsqlxuho = "";
            foreach (string str in arrayxuhao)
            {
                strsqlxuho+=" BM_XUHAO='"+str+"' OR BM_XUHAO LIKE '"+str+".%' OR ";
            }
            strsqlxuho = " AND (" + strsqlxuho.Substring(0, strsqlxuho.Length - 3) + ")";
            strb_sql.Append(strsqlxuho);

            ViewState["sql_forAll"] = strb_sql.ToString();
        }

        protected string strWhereXuhao()
        {
            string str_xuhao = Request.QueryString["Xuhao"].ToString();
            string[] arrayxuhao = str_xuhao.Split('$');

            string strsqlxuho = "";
            foreach (string str in arrayxuhao)
            {
                strsqlxuho += " BM_XUHAO='" + str + "' OR BM_XUHAO LIKE '" + str + ".%' OR ";
            }
            strsqlxuho = strsqlxuho.Substring(0, strsqlxuho.Length - 3);
            return strsqlxuho;
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
            dt1.Columns.Add("BM_MSSTATUS");
            dt1.Columns.Add("BM_MAQUALITY");
            dt1.Columns.Add("BM_UNITWGHT");
            dt1.Columns.Add("BM_TOTALWGHT");
            dt1.Columns.Add("BM_MASHAPE");
            dt1.Columns.Add("BM_MASTATE");
            dt1.Columns.Add("BM_STANDARD");
            dt1.Columns.Add("BM_KEYCOMS");
            dt1.Columns.Add("BM_ID");
            dt1.Columns["BM_ISMANU"].DefaultValue = "N";

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt1.NewRow();
                newRow["BM_MSXUHAO"] = ((HtmlInputText)gRow.FindControl("MsIndex")).Value;//明细序号
                newRow["BM_XUHAO"] = ((HtmlInputText)gRow.FindControl("Index")).Value;//原始序号
                newRow["BM_TUHAO"] = ((HtmlInputText)gRow.FindControl("tuhao")).Value;//图号(标识号)
                newRow["BM_ZONGXU"] = ((HtmlInputText)gRow.FindControl("zongxu")).Value;//总序
                newRow["BM_CHANAME"] = ((TextBox)gRow.FindControl("txtName")).Text;//名称
                newRow["BM_ISMANU"] = ((DropDownList)gRow.FindControl("ddlISMANU")).SelectedValue;//是否体现
                newRow["BM_KU"] = ((HtmlInputText)gRow.FindControl("ku")).Value;//库
                newRow["BM_GUIGE"] = ((HtmlInputText)gRow.FindControl("guige")).Value.Trim();//规格
                newRow["NUMBER"] = gRow.Cells[10].Text == "&nbsp;" ? "" : gRow.Cells[10].Text;//数量
                newRow["BM_PROCESS"] = ((HtmlInputText)gRow.FindControl("process")).Value;//工艺流程
                newRow["BM_NOTE"] = ((HtmlInputText)gRow.FindControl("beizhu")).Value;//备注                
                newRow["BM_MSSTATUS"] = (((Label)gRow.FindControl("lblChangeSta")).Text.Trim() == "删除" ? "1" : ((Label)gRow.FindControl("lblChangeSta")).Text.Trim() == "修改" ? "3" : ((Label)gRow.FindControl("lblChangeSta")).Text.Trim() == "增加" ? "2" : "0");
                newRow["BM_MAQUALITY"] = gRow.Cells[14].Text == "&nbsp;" ? "" : gRow.Cells[14].Text;//材质
                newRow["BM_UNITWGHT"] = gRow.Cells[15].Text == "&nbsp;" ? "" : gRow.Cells[15].Text;//单重(kg)
                newRow["BM_TOTALWGHT"] = gRow.Cells[16].Text == "&nbsp;" ? "" : gRow.Cells[16].Text;//总重(kg)
                newRow["BM_MASHAPE"] = gRow.Cells[17].Text == "&nbsp;" ? "" : gRow.Cells[17].Text;//毛坯
                newRow["BM_MASTATE"] = gRow.Cells[18].Text == "&nbsp;" ? "" : gRow.Cells[18].Text;//状态
                newRow["BM_STANDARD"] = gRow.Cells[19].Text == "&nbsp;" ? "" : gRow.Cells[19].Text;//国标
                newRow["BM_KEYCOMS"] = gRow.Cells[20].Text == "&nbsp;" ? "" : gRow.Cells[20].Text;//关键部件
                newRow["BM_ID"] = ((HtmlInputHidden)gRow.FindControl("hdfID")).Value; //ID
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
                    string status = ((Label)gRow.FindControl("lblChangeSta")).Text;
                    if (status == "增加")//在调整变更制作明细时，如果删除的是制作明细变更增加的记录，则将原始数据中该条记录制作明细变更状态置0,同时修改材料计划和外协状态
                    {
                        List<string> list_sql_Aupdate = new List<string>();
                        string id = ((HtmlInputHidden)gRow.FindControl("hdfID")).Value.Trim();
                        list_sql_Aupdate.Add("exec PRO_TM_MSChangeAdjustDelete '" + ViewState["view_tablename"] + "','" + tsaid.Text + "','" + id + "'");
                        DBCallCommon.ExecuteTrans(list_sql_Aupdate);
                    }
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
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:未知异常,请稍后再试！！！');window.close();", true);
            }
        }
        /// <summary>
        /// 复制插入总序对应的数据(1/6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hdbtn_Click(object sender, EventArgs e)
        {
            count = int.Parse(index.Value) - 1;
            GridViewRow gr = GridView1.Rows[count];
            InitListName();
            string zx = zongxu.Value.ToString();
            //获取虚拟部件号
            string[] tsplit = tsaid.Text.Trim().Split('-');
            string xulibujhao = tsplit[1].ToString() + ".0";
            if (zx != xulibujhao)
            {
                sqlText = "select BM_ID,BM_ZONGXU,BM_MSSTATUS,BM_XUHAO,BM_TUHAO,BM_CHANAME,BM_GUIGE,BM_MAQUALITY,BM_NUMBER,BM_UNITWGHT,BM_MSXUHAO,";
                sqlText += "BM_TOTALWGHT,BM_MASHAPE,BM_MASTATE,BM_KU,BM_STANDARD,BM_PROCESS,BM_KEYCOMS,BM_ISMANU,BM_NOTE ";
                sqlText += "from " + ViewState["view_tablename"] + " where BM_MSSTATE='0' and BM_ENGID='" + tsaid.Text + "' and BM_ZONGXU='" + zongxu.Value + "' ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["BM_MSSTATUS"].ToString() != "1")//插入非删除变更记录
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
                        if (dt.Rows[0]["BM_MSSTATUS"].ToString() == "0")//插入正常记录
                        {
                            ((Label)gr.FindControl("lblChangeSta")).Text = "增加";//标明增加，保存数据时改变状态
                        }
                        else
                        {
                            ((Label)gr.FindControl("lblChangeSta")).Text = dt.Rows[0]["BM_MSSTATUS"].ToString() == "1" ? "删除" : dt.Rows[0]["BM_MSSTATUS"].ToString() == "2" ? "增加" : dt.Rows[0]["BM_MSSTATUS"].ToString() == "3" ? "修改" : "";
                        }
                        gr.Cells[14].Text = dt.Rows[0]["BM_MAQUALITY"].ToString();//材质                        
                        gr.Cells[15].Text = dt.Rows[0]["BM_UNITWGHT"].ToString();//单重(kg)
                        gr.Cells[16].Text = dt.Rows[0]["BM_TOTALWGHT"].ToString();//总重(kg)
                        gr.Cells[17].Text = dt.Rows[0]["BM_MASHAPE"].ToString();//毛坯
                        gr.Cells[18].Text = dt.Rows[0]["BM_MASTATE"].ToString();//状态
                        gr.Cells[19].Text = dt.Rows[0]["BM_STANDARD"].ToString();//国标
                        gr.Cells[20].Text = dt.Rows[0]["BM_KEYCOMS"].ToString();//关键部件
                        ((HtmlInputHidden)gr.FindControl("hdfID")).Value = dt.Rows[0]["BM_ID"].ToString();//ID
                    }
                    else
                    {
                        ((HtmlInputText)gr.FindControl("MsIndex")).Value = "";//明细序号
                        ((HtmlInputText)gr.FindControl("Index")).Value = "";//原始序号
                        ((HtmlInputText)gr.FindControl("tuhao")).Value = "";//图号(标识号)
                        //////////((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                        ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                        ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                        ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                        ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                        gr.Cells[10].Text = "";//数量
                        ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                        ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                        ((Label)gr.FindControl("lblChangeSta")).Text = "";
                        gr.Cells[14].Text = "";//材质                        
                        gr.Cells[15].Text = "";//单重(kg)
                        gr.Cells[16].Text = "";//总重(kg)
                        gr.Cells[17].Text = "";//毛坯
                        gr.Cells[18].Text = "";//状态
                        gr.Cells[19].Text = "";//国标
                        gr.Cells[20].Text = "";//关键部件
                        ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('插入的记录为变更删除条目，无法插入！！！');", true);
                    }
                }
                else if (dt.Rows.Count == 0)
                {
                    ((HtmlInputText)gr.FindControl("MsIndex")).Value = "";//明细序号
                    ((HtmlInputText)gr.FindControl("Index")).Value = "";//原始序号
                    ((HtmlInputText)gr.FindControl("tuhao")).Value = "";//图号(标识号)
                    //////////((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                    ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                    ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                    ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                    ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                    gr.Cells[10].Text = "";//数量
                    ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                    ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                    ((Label)gr.FindControl("lblChangeSta")).Text = "";
                    gr.Cells[14].Text = "";//材质                        
                    gr.Cells[15].Text = "";//单重(kg)
                    gr.Cells[16].Text = "";//总重(kg)
                    gr.Cells[17].Text = "";//毛坯
                    gr.Cells[18].Text = "";//状态
                    gr.Cells[19].Text = "";//国标
                    gr.Cells[20].Text = "";//关键部件
                    ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该总序不存在或为已提交的非变更记录！！！');", true);
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
                //////////((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                gr.Cells[10].Text = "";//数量
                ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                ((Label)gr.FindControl("lblChangeSta")).Text = "";
                gr.Cells[14].Text = "";//材质                        
                gr.Cells[15].Text = "";//单重(kg)
                gr.Cells[16].Text = "";//总重(kg)
                gr.Cells[17].Text = "";//毛坯
                gr.Cells[18].Text = "";//状态
                gr.Cells[19].Text = "";//国标
                gr.Cells[20].Text = "";//关键部件
                ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('虚拟部件无法插入！！！');", true);
            }

        }
        /// <summary>
        /// 复制插入序号对应的数据(1/6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hdbtnxuhao_Click(object sender, EventArgs e)
        {
            cbxXuhaoCopy.Checked = false;
            lblshortcut.Value = "(当前:无)";
            count = int.Parse(index.Value) - 1;
            GridViewRow gr = GridView1.Rows[count];
            InitListName();
            string zx = zongxu.Value.ToString();
            //获取虚拟部件号
            string[] tsplit = tsaid.Text.Trim().Split('-');
            string xulibujhao = tsplit[1].ToString() + ".0";
            if (zx != xulibujhao)
            {
                sqlText = "select BM_ID,BM_ZONGXU,BM_MSSTATUS,BM_XUHAO,BM_ZONGXU,BM_TUHAO,BM_CHANAME,BM_GUIGE,BM_MAQUALITY,BM_NUMBER,BM_UNITWGHT,BM_MSXUHAO,";
                sqlText += "BM_TOTALWGHT,BM_MASHAPE,BM_MASTATE,BM_KU,BM_STANDARD,BM_PROCESS,BM_KEYCOMS,BM_ISMANU,BM_NOTE ";
                sqlText += "from " + ViewState["view_tablename"] + " where BM_MSSTATE='0' and BM_ENGID='" + tsaid.Text + "' and BM_XUHAO='" + zongxu.Value + "' ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["BM_MSSTATUS"].ToString() != "1")
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
                        if (dt.Rows[0]["BM_MSSTATUS"].ToString() == "0")//插入正常记录
                        {
                            ((Label)gr.FindControl("lblChangeSta")).Text = "增加";//标明增加，保存数据时改变状态
                        }
                        else
                        {
                            ((Label)gr.FindControl("lblChangeSta")).Text = dt.Rows[0]["BM_MSSTATUS"].ToString() == "1" ? "删除" : dt.Rows[0]["BM_MSSTATUS"].ToString() == "2" ? "增加" : dt.Rows[0]["BM_MSSTATUS"].ToString() == "3" ? "修改" : "";
                        }
                        gr.Cells[14].Text = dt.Rows[0]["BM_MAQUALITY"].ToString();//材质                        
                        gr.Cells[15].Text = dt.Rows[0]["BM_UNITWGHT"].ToString();//单重(kg)
                        gr.Cells[16].Text = dt.Rows[0]["BM_TOTALWGHT"].ToString();//总重(kg)
                        gr.Cells[17].Text = dt.Rows[0]["BM_MASHAPE"].ToString();//毛坯
                        gr.Cells[18].Text = dt.Rows[0]["BM_MASTATE"].ToString();//状态
                        gr.Cells[19].Text = dt.Rows[0]["BM_STANDARD"].ToString();//国标
                        gr.Cells[20].Text = dt.Rows[0]["BM_KEYCOMS"].ToString();//关键部件
                        ((HtmlInputHidden)gr.FindControl("hdfID")).Value = dt.Rows[0]["BM_ID"].ToString();//ID
                    }
                    else
                    {
                        ((HtmlInputText)gr.FindControl("MsIndex")).Value = "";//明细序号
                        /////////((HtmlInputText)gr.FindControl("Index")).Value = "";//原始序号
                        ((HtmlInputText)gr.FindControl("tuhao")).Value = "";//图号(标识号)
                        ((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                        ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                        ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                        ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                        ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                        gr.Cells[10].Text = "";//数量
                        ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                        ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                        ((Label)gr.FindControl("lblChangeSta")).Text = "";
                        gr.Cells[14].Text = "";//材质                        
                        gr.Cells[15].Text = "";//单重(kg)
                        gr.Cells[16].Text = "";//总重(kg)
                        gr.Cells[17].Text = "";//毛坯
                        gr.Cells[18].Text = "";//状态
                        gr.Cells[19].Text = "";//国标
                        gr.Cells[20].Text = "";//关键部件
                        ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('插入的记录为变更删除条目，无法插入！！！');", true);

                    }
                }
                else if (dt.Rows.Count == 0)
                {
                    ((HtmlInputText)gr.FindControl("MsIndex")).Value = "";//明细序号
                    /////////((HtmlInputText)gr.FindControl("Index")).Value = "";//原始序号
                    ((HtmlInputText)gr.FindControl("tuhao")).Value = "";//图号(标识号)
                    ((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                    ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                    ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                    ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                    ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                    gr.Cells[10].Text = "";//数量
                    ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                    ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                    ((Label)gr.FindControl("lblChangeSta")).Text = "";
                    gr.Cells[14].Text = "";//材质                        
                    gr.Cells[15].Text = "";//单重(kg)
                    gr.Cells[16].Text = "";//总重(kg)
                    gr.Cells[17].Text = "";//毛坯
                    gr.Cells[18].Text = "";//状态
                    gr.Cells[19].Text = "";//国标
                    gr.Cells[20].Text = "";//关键部件
                    ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该序号不存在或已提交！！！');", true);
                }
            }
            else
            {
                ((HtmlInputText)gr.FindControl("MsIndex")).Value = "";//明细序号
                /////////((HtmlInputText)gr.FindControl("Index")).Value = "";//原始序号
                ((HtmlInputText)gr.FindControl("tuhao")).Value = "";//图号(标识号)
                ((HtmlInputText)gr.FindControl("zongxu")).Value = "";//总序
                ((TextBox)gr.FindControl("txtName")).Text = "";//名称
                ((DropDownList)gr.FindControl("ddlISMANU")).SelectedValue = "N";//是否体现
                ((HtmlInputText)gr.FindControl("ku")).Value = "";//库
                ((HtmlInputText)gr.FindControl("guige")).Value = "";//规格
                gr.Cells[10].Text = "";//数量
                ((HtmlInputText)gr.FindControl("process")).Value = "";//工艺流程
                ((HtmlInputText)gr.FindControl("beizhu")).Value = "";//备注
                ((Label)gr.FindControl("lblChangeSta")).Text = "";
                gr.Cells[14].Text = "";//材质                        
                gr.Cells[15].Text = "";//单重(kg)
                gr.Cells[16].Text = "";//总重(kg)
                gr.Cells[17].Text = "";//毛坯
                gr.Cells[18].Text = "";//状态
                gr.Cells[19].Text = "";//国标
                gr.Cells[20].Text = "";//关键部件
                ((HtmlInputHidden)gr.FindControl("hdfID")).Value = "";//ID
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('虚拟部件无法插入！！！');", true);
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
                        ViewState["insertflag"] = "0" + (i + 1).ToString();
                        break;
                    }
                    else
                    {
                        string id = ((HtmlInputHidden)(grow.FindControl("hdfID"))).Value.Trim();
                        string xuhao = ((HtmlInputText)(grow.FindControl("Index"))).Value.Trim();//序号
                        string tuhao = ((HtmlInputText)(grow.FindControl("tuhao"))).Value.Trim();
                        string zongxu = ((HtmlInputText)(grow.FindControl("zongxu"))).Value.Trim();
                        string stauts = ((Label)grow.FindControl("lblChangeSta")).Text.Trim() == "增加" ? "2" : ((Label)grow.FindControl("lblChangeSta")).Text.Trim() == "修改" ? "3" : ((Label)grow.FindControl("lblChangeSta")).Text.Trim() == "删除" ? "1" : "0";
                        string ismunu = ((DropDownList)grow.FindControl("ddlISMANU")).SelectedValue.ToString();
                        string ku = ((HtmlInputText)(grow.FindControl("ku"))).Value.Trim();
                        string beizhu = ((HtmlInputText)(grow.FindControl("beizhu"))).Value.Trim();
                        string process = ((HtmlInputText)(grow.FindControl("process"))).Value.Trim();
                        string ms_xuhao = ((HtmlInputText)(grow.FindControl("MsIndex"))).Value.Trim();
                        string guige = ((HtmlInputText)grow.FindControl("guige")).Value.Trim();
                        string name = ((TextBox)grow.FindControl("txtName")).Text.Trim();
                        string zongzhong = grow.Cells[10].Text.Trim();
                        if (xuhao == "")
                        {
                            ViewState["insertflag"] = "2" + (i + 1).ToString();
                            break;
                        }
                        else
                        {
                            if (ms_xuhao != "")
                            {
                                string sql_select_msexist = "select BM_MSXUHAO from " + ViewState["tablename"].ToString() + " where BM_ENGID='" + tsaid.Text + "' and BM_MSXUHAO='" + ms_xuhao + "' and  BM_ID<>" + id + " and BM_XUHAO like '" + xuhao + ".%'";
                                SqlDataReader dr_select_msexist = DBCallCommon.GetDRUsingSqlText(sql_select_msexist);
                                if (dr_select_msexist.HasRows)
                                {
                                    return "1" + (i + 1).ToString();
                                }
                            }
                            List<string> list_sql = new List<string>();
                            //找到原序号
                            string old_xuhao = "";
                            string sql_old_xuhao = "select BM_XUHAO from " + ViewState["tablename"] + " where BM_ID=" + id + " and BM_ENGID='" + tsaid.Text + "'";
                            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_old_xuhao);
                            dr.Read();
                            old_xuhao = dr["BM_XUHAO"].ToString();
                            dr.Close();
                            // [PRO_TM_MSChangeAdjustUpdate] 
                            // @StrTabeleName varchar(50),--原始数据表
                            // @MsTableName varchar(50),--制作明细表
                            // @TaskID varchar(50),--生产制号
                            // @OldIndex varchar(50),--旧序号
                            // @NewIndex varchar(50),--新序号
                            // @TuHao varchar(100),--图号
                            // @Ku  varchar(50),--库
                            // @Status char(1),--变更状态
                            // @ID varchar(50)--原始数据ID号
                            // @Note varchar(200),--备注
                            //@Process varchar(200),--工艺流程
                            //@MSIndex varchar(50)--明细序号
                            //@Guige varchar(50)--规格
                            //@Name varchar(50)--中文名称

                            ParamofIndexUpdate pra = new ParamofIndexUpdate();
                            pra.StrTabeleName = ViewState["tablename"].ToString();
                            pra.MsTableName = ViewState["mstable"].ToString();
                            pra.TaskID = tsaid.Text;
                            pra.OldIndex = old_xuhao;
                            pra.NewIndex = xuhao;
                            pra.TuHao = tuhao;
                            pra.Ku = ku;
                            pra.Status = stauts;
                            pra.ID = id;
                            pra.IsManu = ismunu;
                            pra.BeiZhu = beizhu;
                            pra.Process = process;
                            pra.MSXuHao = ms_xuhao;
                            pra.Guige = guige;
                            pra.Name = name;
                            string sql_update = "exec [PRO_TM_MSChangeAdjustUpdate] '" + pra.StrTabeleName + "','" + pra.MsTableName + "','" + pra.TaskID + "','" + pra.OldIndex + "','" + pra.NewIndex + "','" + pra.TuHao + "','" + pra.Ku + "','" + pra.Status + "','" + pra.ID + "','" + pra.IsManu + "','" + pra.BeiZhu + "','" + pra.Process + "','" + pra.MSXuHao + "','"+pra.Guige+"','"+pra.Name+"'";
                            list_sql.Add(sql_update);
                            DBCallCommon.ExecuteTrans(list_sql);
                            ViewState["insertflag"] = "3"; //正常
                        }
                    }
                }
                ret = ViewState["insertflag"].ToString();
            }
            return ret;
        }

        private class ParamofIndexUpdate
        {
            private string _StrTabeleName;
            private string _MsTableName;
            private string _TaskID;
            private string _OldIndex;
            private string _NewIndex;
            private string _TuHao;
            private string _Ku;
            private string _Status;
            private string _ID;
            private string _IsManu;
            private string _BeiZhu;
            private string _Process;
            private string _MSXuHao;
            private string _Guige;
            private string _Name;
            public string StrTabeleName
            {
                get { return _StrTabeleName; }
                set { _StrTabeleName = value; }
            }
            public string MsTableName
            {
                get { return _MsTableName; }
                set { _MsTableName = value; }
            }
            public string TaskID
            {
                get { return _TaskID; }
                set { _TaskID = value; }
            }
            public string OldIndex
            {
                get { return _OldIndex; }
                set { _OldIndex = value; }
            }
            public string NewIndex
            {
                get { return _NewIndex; }
                set { _NewIndex = value; }
            }
            public string TuHao
            {
                get { return _TuHao; }
                set { _TuHao = value; }
            }
            public string Ku
            {
                get { return _Ku; }
                set { _Ku = value; }
            }
            public string Status
            {
                get { return _Status; }
                set { _Status = value; }
            }
            public string ID
            {
                get { return _ID; }
                set { _ID = value; }
            }
            public string IsManu
            {
                get { return _IsManu; }
                set { _IsManu = value; }
            }

            public string BeiZhu
            {
                get { return _BeiZhu; }
                set { _BeiZhu = value; }
            }
            public string Process
            {
                get { return _Process; }
                set { _Process = value; }
            }
            public string MSXuHao
            {
                get { return _MSXuHao; }
                set { _MSXuHao = value; }
            }

            public string Guige
            {
                get { return _Guige; }
                set { _Guige = value; }
            }

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }
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
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[0-9]+)*)$";
            Regex rgx = new Regex(pattern);
            string ret = "0";
            List<string> list_sql = new List<string>();
            //检验总序格式
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                string xuhao = ((HtmlInputText)gRow.FindControl("Index")).Value.Trim();
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
                            string sql_mar = "select BM_MARID,BM_MSSTATUS from " + ViewState["tablename"] + " where BM_ENGID='" + tsaid.Text + "' and BM_ID=" + id + "";
                            string mar = "";
                            string mschgstate = "";
                            SqlDataReader dr_mar = DBCallCommon.GetDRUsingSqlText(sql_mar);
                            if (dr_mar.HasRows)
                            {
                                dr_mar.Read();
                                mar = dr_mar["BM_MARID"].ToString();
                                mschgstate = dr_mar["BM_MSSTATUS"].ToString();
                            }
                            dr_mar.Close();
                            if (mschgstate != "1")//非变更删除插入临时表中
                            {
                                list_sql.Add("insert into TBPM_TEMPORGDATA(BM_ID,BM_ZONGXU,BM_MARID,BM_ENGID) Values('" + id + "','" + xuhao + "','" + mar + "','" + tsaid.Text.Trim() + "')");
                            }
                        }
                        else
                        {
                            ret = "0-" + (i + 1).ToString();
                            return ret;
                        }
                    }
                }
            }
            //检验总序输入临时表
            DBCallCommon.ExecuteTrans(list_sql);
            //检验归属关系
            ParamsCheckMarNotBelongToMar pcmar = new ParamsCheckMarNotBelongToMar();
            pcmar.StrTabeleName = ViewState["tablename"].ToString();
            pcmar.TaskID = tsaid.Text;
            DataTable dt = this.ExecMarCheck(pcmar);
            if (dt.Rows.Count > 0)
            {
                ret = dt.Rows[0][0].ToString();
            }
            else
            {
                ret = "UndefinedError";
            }
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
        /// <summary>
        /// 初始化表名
        /// </summary>
        private void InitListName()
        {

                    ViewState["tablename"] = "TBPM_STRINFODQO";
                    ViewState["view_tablename"] = "View_TM_DQO";
                    ViewState["mstable"] = "TBPM_MKDETAIL";
                    ViewState["mptable"] = "TBPM_MPPLAN";

            
        }

        #region 查询
        /// <summary>
        ///绑定部件名称 
        /// </summary>
        private void partsName()
        {
            sqlText = "select BM_CHANAME,BM_XUHAO from " + ViewState["tablename"] + " where BM_ENGID='" + tsaid.Text + "' and (BM_XUHAO like '[1-9].[1-9]' OR BM_XUHAO like '[1-9].[1-9][0-9]' OR BM_XUHAO like '[1-9][0-9].[1-9]' OR BM_XUHAO like '[1-9][0-9].[1-9][0-9]') and BM_MSSTATUS!='0'";
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
            this.InitListName();
            UCPaging2.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Event_Query(object sender, EventArgs e)
        {
            ViewState["sqlText"] = ViewState["sql_forAll"].ToString();
            if (ddlParts.SelectedIndex != 0)
            {
                ViewState["sqlText"]+=" and (BM_XUHAO='" + ddlParts.SelectedValue + "' or BM_XUHAO like '" + ddlParts.SelectedValue + "%')";
            }

            if (ddlinMS.SelectedIndex != 0)
            {
                ViewState["sqlText"]+=" and BM_ISMANU='" + ddlinMS.SelectedValue + "'";
            }

            if (ddlXH_ZX.SelectedIndex != 0)
            {
                if (ddlXH_ZX.SelectedValue == "BM_TUHAO" || ddlXH_ZX.SelectedValue == "BM_NOTE")
                {
                    ViewState["sqlText"] += "  and " + ddlXH_ZX.SelectedValue + " like '%" + txtXH_ZX.Text.Trim() + "%'";
                }
                else if (ddlXH_ZX.SelectedValue == "BM_MSXUHAO")
                {
                    ViewState["sqlText"] += "  and " + ddlXH_ZX.SelectedValue + " like '" + txtXH_ZX.Text.Trim() + "%'";
                }
                else
                {
                    ViewState["sqlText"] += "  and (" + ddlXH_ZX.SelectedValue + "='" + txtXH_ZX.Text.Trim() + "' OR " + ddlXH_ZX.SelectedValue + " like '" + txtXH_ZX.Text.Trim() + ".%')";
                }
            }

            if (ddlOrgJishu.SelectedIndex != 0)
            {
                ViewState["sqlText"]+=" AND [dbo].[Splitnum](" + ddlShowType.SelectedValue + ",'.')=" + ddlOrgJishu.SelectedValue + " ";
            }

            if (ddlKU.SelectedIndex != 0)
            {
                ViewState["sqlText"] += " and BM_KU='" + ddlKU.SelectedValue.ToString() + "'";
            }

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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行序号、总序或数量不能为空！！！');", true);
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
            else if (ret.Contains("DataBaseRepeat-"))//非变更删除记录归属到删除记录上
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"归属到变更删除部件上！！！');", true);
            }
        }
        /// <summary>
        /// 添加变更增加项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddChange_Onclick(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            list_sql.Add("exec PRO_TM_AddChange '" + tsaid.Text.Trim() + "'");
            try
            {
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已添加,即将刷新页面!');window.location.reload();", true);

            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('程序出现未知错误，请与管理员联系!');", true);
            }

            
        }
        /// <summary>
        /// 重量计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_onClick(object sender, EventArgs e)
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行序号为空！！！');", true);
            }
            else if (flag == "3")//正常--接下来继续验证数据库中每一序号都存在父部件
            {
                this.InitListName();
                ParamsCheckMarNotBelongToMar parafather = new ParamsCheckMarNotBelongToMar();
                parafather.StrTabeleName = ViewState["tablename"].ToString();
                parafather.TaskID = tsaid.Text;
                parafather.StrWhere = "1=1";
                DataTable dt = this.ExecFatherExistCheck(parafather);
                string returnValue = dt.Rows[0][0].ToString();
                if (returnValue == "0")
                {
                    DBCallCommon.ExeSqlText(" exec PRO_TM_MSCalWeight '" + parafather.StrTabeleName + "','" + parafather.TaskID + "','BM_XUHAO=''1'' OR BM_XUHAO LIKE ''1.%'''");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:已计算!');window.location.reload();", true);
                }
                else if (returnValue.Contains("FNotExist-"))//FNotExist-2.1.1-2.1
                {
                    string[] aa = returnValue.Split('-');
                    string outxuhao = aa[aa.Length - 2].ToString();
                    string outzongxu = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法计算!\\r\\r序号\"" + outxuhao + "\"无父部件！！！(总序" + outzongxu + ")');", true);
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
            else if (ret.Contains("DataBaseRepeat-"))//非变更删除记录归属到删除记录上
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"归属到变更删除部件上！！！');", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【'+" + index + "+'】行序号、总序或数量不能为空！！！');", true);
            }
            else if (flag == "3")//正常--接下来继续验证数据库中每一序号都存在父部件
            {
                this.InitListName();
                ParamsCheckMarNotBelongToMar parafather = new ParamsCheckMarNotBelongToMar();
                parafather.StrTabeleName = ViewState["tablename"].ToString();
                parafather.TaskID = tsaid.Text;
                parafather.StrWhere = this.strWhereXuhao();
                DataTable dt = this.ExecFatherExistCheck(parafather);
                string returnValue = dt.Rows[0][0].ToString();
                if (returnValue == "0") //正常可以提交制作明细
                {
                    Response.Redirect("TM_MS_WaitforCreate_Change.aspx?TaskID=" + tsaid.Text + "&Xuhao=" + Request.QueryString["Xuhao"].ToString()+ "");
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
            else if (ret.Contains("DataBaseRepeat-"))//非变更删除记录归属到删除记录上
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"归属到变更删除部件上！！！');", true);
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
            UCPaging2.PageMSChanged += new UCPagingOfMS.PageHandlerOfMS(Pager_PageChanged);
            UCPaging2.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            this.InitListName();
            pager.TableName = ViewState["view_tablename"].ToString();
            pager.PrimaryKey = "BM_ID";
            pager.ShowFields = "BM_ID,BM_MSSTATUS,BM_XUHAO,BM_TUHAO,BM_ZONGXU,BM_CHANAME,BM_GUIGE,BM_MAQUALITY,BM_NUMBER,BM_UNITWGHT,BM_TOTALWGHT,BM_MASHAPE,BM_MASTATE,BM_KU,BM_STANDARD,BM_PROCESS,BM_KEYCOMS,BM_ISMANU,BM_NOTE,BM_MSXUHAO,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) AS NUMBER";
            pager.OrderField = "dbo.f_formatstr(" + ddlSort.SelectedValue + ", '.')";
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
                if (html_index.Contains("1.0."))
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[1].ToolTip = "绿色标识为归属到“1.0”的物料或部件！！！";
                }
                e.Row.Cells[10].ToolTip = "双击修改原始数据！！！";
                string url = html_index + ' ' + tsaid.Text.Trim() + ' ' + ViewState["tablename"].ToString() + ' ' + ViewState["mptable"].ToString() + ' ' + ViewState["mstable"].ToString() + ' ' + ViewState["view_tablename"].ToString();
                e.Row.Cells[10].Attributes.Add("ondblclick", "javascript:openLink('" + url + "')");
                e.Row.Cells[10].Attributes.Add("style", "Cursor:hand");
            }
        }
    }
}