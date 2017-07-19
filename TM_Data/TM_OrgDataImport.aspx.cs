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
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_OrgDataImport : System.Web.UI.Page
    {
        #region
        string tsa_id;
        string sqlText;
        string tablename;
        string viewtablename;
        string xuhao;
        string tuhao;
        string marid;
        string zongxu;
        string ch_name;
        string en_name;
        string guige;
        string cailiaoname;
        string cailiaoguige;
        float cailiaocd;
        float cailiaokd;
        float lilunzhl;
        float cailiaodzh;
        float cailiaozongzhong;
        float cailiaozongchang;
        float bgzmy;
        float mpmy;
        string tjsx;
        string caizhi;
        float shuliang;
        float singshuliang;
        float p_shuliang;
        float dzh;
        float zongzhong;
        string labunit;
        string xinzhuang;
        string zhuangtai;
        string biaozhun;
        string process;
        string beizhu;
        string ddlKeyComponents;
        string ddlFixedSize;
        string ddlwmp;//是否提材料计划
        float tudz;//图纸上单重
        float tuzongzhong;//图纸上总重
        string tucz;//图纸上材质
        string tuwt;//图纸上问题
        string tustrd;//图纸上标注
        string ku;
        int count = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInit();
                TM_BasicFun.BindCklShowHiddenItems("Input", cklHiddenShow);
            }
            if (IsPostBack)
            {
                if (ViewState["viewtablename_src"] != null)
                {
                    this.InitVar();
                }
            }
        }

        protected void PageInit()
        {
            ViewState["TaskID"] = Request.QueryString["TaskID"].ToString();
            string sql = "select  TSA_ID, TSA_PJID, TSA_ENGNAME,CM_PROJ from View_TM_TaskAssign where TSA_ID='" + ViewState["TaskID"].ToString() + "'";
            DataTable dt=DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lblProjName.Text = dt.Rows[0]["CM_PROJ"].ToString() + "(" + dt.Rows[0]["TSA_PJID"].ToString() + ")";
                lblEngName.Text = dt.Rows[0]["TSA_ENGNAME"].ToString() + "_" + dt.Rows[0]["TSA_ID"].ToString();
              
              

           
                        ViewState["tablename"] = "TBPM_STRINFODQO";
                        ViewState["viewtablename"] = "View_TM_DQO";
                 
            }
            this.BindProjName();

            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;
            ddlbjname.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlbjname.SelectedIndex = 0;

          //  this.CheckZeroXuHaoExist();
            this.btnHidden_OnClick(null, null);
            this.btnHiddenPaper_OnClick(null, null);

            //读取台数
            string sql_gettaishu = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" + ViewState["TaskID"].ToString().Split('-')[0] + "'";
            SqlDataReader dr_number = DBCallCommon.GetDRUsingSqlText(sql_gettaishu);
            if (dr_number.HasRows)
            {
                dr_number.Read();
                lblNumber.Text = dr_number["TSA_NUMBER"].ToString();
                dr_number.Close();
            }

        }
        /// <summary>
        /// 绑定项目名称
        /// </summary>
        protected void BindProjName()
        {
          //  string sql_pj = "select  PJ_ID,PJ_ID+'|'+PJ_NAME as PJ_NAME from TBPM_PJINFO order by PJ_ID";
          //  string dataText = "PJ_NAME";
           // string dataValue = "PJ_ID";
          //  DBCallCommon.BindAJAXCombox(ddlProName, sql_pj, dataText, dataValue);
            string sql_pj = "select distinct CM_PROJ,TSA_PJID+'|'+CM_PROJ as PJ_NAME from View_TM_TaskAssign order by TSA_PJID+'|'+CM_PROJ desc";
            string dataText = "PJ_NAME";
            string dataValue = "CM_PROJ";
            DBCallCommon.BindAJAXCombox(ddlProName, sql_pj, dataText, dataValue);
        }
        /// <summary>
        /// 绑定工程名称
        /// </summary>
        protected void BindEngName()
        {
            //string sql_eng = "select TSA_ID+'%'+TSA_ENGSTRTYPE as TSA_ID, TSA_ID+'|'+TSA_ENGNAME as TSA_ENGNAME from View_TM_TaskAssign where TSA_PJID='" + ddlProName.SelectedValue + "' and TSA_FATHERNODE='0' order by TSA_ID";
            //string dataText = "TSA_ENGNAME";
            //string dataValue = "TSA_ID";
            //DBCallCommon.BindAJAXCombox(ddlEngName, sql_eng, dataText, dataValue);
            string sql_eng = "select  TSA_ID, TSA_ID+'|'+TSA_ENGNAME as TSA_ENGNAME from View_TM_TaskAssign where CM_PROJ='" + ddlProName.SelectedValue + "'  order by TSA_ID";
            string dataText = "TSA_ENGNAME";
            string dataValue = "TSA_ID";
            DBCallCommon.BindAJAXCombox(ddlEngName, sql_eng, dataText, dataValue);
        }
        /// <summary>
        /// 绑定部件名称
        /// </summary>
        protected void BindPartName()
        {
            if (ddlEngName.SelectedIndex != 0)
            {

                string engid = ddlEngName.SelectedValue;
               
                                            string sql_part = "select BM_ZONGXU,BM_ZONGXU+'|'+BM_CHANAME as BM_CHANAME from View_TM_DQO where (BM_MARID='' OR BM_MARID IS NULL) AND BM_ENGID = '" + engid + "' ORDER BY dbo.f_formatstr('BM_ZONGXU','.')";
                string dataText = "BM_CHANAME";
                string dataValue = "BM_ZONGXU";
                DBCallCommon.BindAJAXCombox(ddlbjname, sql_part, dataText, dataValue);
            }
            else
            {
                ddlbjname.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
                ddlbjname.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 项目改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindEngName();
            ddlbjname.Items.Clear();
            this.BindPartName();
            if (ddlProName.SelectedIndex != 0 && ddlEngName.SelectedIndex != 0)
            {
                UCPaging1.CurrentPage = 1;
                InitVar();
                this.GetBoundData();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                NoDataPanel.Visible = true;
                UCPaging1.Visible = false;
            }
        }
        /// <summary>
        /// 工程名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindPartName();
            if (ddlProName.SelectedIndex != 0 && ddlEngName.SelectedIndex != 0)
            {
                UCPaging1.CurrentPage = 1;
                InitVar();
                this.GetBoundData();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                NoDataPanel.Visible = true;
                UCPaging1.Visible = false;
            }
        }
        /// <summary>
        /// 部件名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlbjname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex != 0 && ddlEngName.SelectedIndex != 0)
            {
                UCPaging1.CurrentPage = 1;
                InitVar();
                this.GetBoundData();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                NoDataPanel.Visible = true;
                UCPaging1.Visible = false;
            }
        }
        /// <summary>
        ///查询条件清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMSClear_OnClick(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqMS.FindControl("GridView1"));
        }
        /// <summary>
        /// 返回Where条件
        /// </summary>
        /// <returns></returns>
        protected string GetStrWhere()
        {
            string returnValue="";
            if (ddlbjname.SelectedIndex != 0)
            {
                returnValue = "BM_PJID='"+ddlProName.SelectedValue+"' AND BM_ENGID LIKE '"+ddlEngName.SelectedValue.Split('%')[0]+"-%' and (BM_ZONGXU='"+ddlbjname.SelectedValue+"' OR BM_ZONGXU LIKE '"+ddlbjname.SelectedValue+".%')";
            }
            else
            {
                returnValue = "BM_PJID='" + ddlProName.SelectedValue + "' AND BM_ENGID LIKE '" + ddlEngName.SelectedValue.Split('%')[0] + "-%'";
            }

            udqMS.ExistedConditions = returnValue;
            returnValue = returnValue + UserDefinedQueryConditions.ReturnQueryString((GridView)udqMS.FindControl("GridView1"), (Label)udqMS.FindControl("Label1"));

            return returnValue;

        }
        /// <summary>
        /// 数据源连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSourceSelect_OnClick(object sender, EventArgs e)
        {
            this.CheckBoxSelectDefine(GridView1, "CheckBox1");
        }
        /// <summary>
        /// 标记已勾选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelected_OnClick(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
            {
                ddlProName.Enabled = false;
                ddlEngName.Enabled = false;

                int selectrows = 0;

                ViewState["SourceTaskId"] = ddlEngName.SelectedValue.Split('%')[0];
                List<string> list_xuhao = new List<string>();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox ckb = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                    if (ckb.Checked)
                    {
                        selectrows++;
                        string xuhao = GridView1.Rows[i].Cells[3].Text.Trim();
                        list_xuhao.Add(xuhao);

                        ckb.Checked = false;
                    }
                }

                int all_rows = SmartGridView1.Rows.Count;
                if (all_rows + selectrows > 20)
                {
                    int select_num_now = 20 - all_rows;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('一次最多可复制20条，您只能勾选\"" + select_num_now + "\"条了！！！');", true);
                }
                else
                {
                    if (selectrows > 0)
                    {
                        this.AddNewSelectItems(list_xuhao);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('标记已完成，可进入目标工程页修改！！！');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有选择标记项，无法标记！！！');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据，无法标记！！！');", true);
            }
        }

        protected void AddNewSelectItems(List<string> list_new_xuhao)
        {
            DataTable dt = this.GetDataFromGrid();
            foreach (string xuhao in list_new_xuhao)
            {
                string sql_text = "select * from " + ViewState["viewtablename_src"].ToString() + " where BM_ENGID like '" + ViewState["SourceTaskId"].ToString() + "-%' AND BM_XUHAO='" + xuhao + "'";
                DataTable dt_new_xuhao = DBCallCommon.GetDTUsingSqlText(sql_text);
                DataRow newrow = dt.NewRow();
                newrow[0] = dt_new_xuhao.Rows[0]["BM_TUHAO"].ToString();
                newrow[1] = dt_new_xuhao.Rows[0]["BM_MARID"].ToString();
                newrow[2] = dt_new_xuhao.Rows[0]["BM_ZONGXU"].ToString();
                newrow[3] = dt_new_xuhao.Rows[0]["BM_CHANAME"].ToString();
                newrow[4] = dt_new_xuhao.Rows[0]["BM_NOTE"].ToString();
                newrow[5] = dt_new_xuhao.Rows[0]["BM_MALENGTH"].ToString();
                newrow[6] = dt_new_xuhao.Rows[0]["BM_MAWIDTH"].ToString();
                newrow[7] = dt_new_xuhao.Rows[0]["BM_SINGNUMBER"].ToString();//总数量
                newrow[8] = dt_new_xuhao.Rows[0]["BM_UNITWGHT"].ToString();
                newrow[9] = dt_new_xuhao.Rows[0]["BM_MAUNITWGHT"].ToString();
                newrow[10] = Convert.ToDouble(dt_new_xuhao.Rows[0]["BM_MAUNITWGHT"].ToString()) *Convert.ToDouble(dt_new_xuhao.Rows[0]["BM_SINGNUMBER"].ToString())* Convert.ToInt16(lblNumber.Text);//现材料总重=材料单重*单台数量*现台数
                newrow[11] = dt_new_xuhao.Rows[0]["BM_MABGZMY"].ToString();
                newrow[12] = dt_new_xuhao.Rows[0]["BM_BEIZHUATR"].ToString();
                newrow[13] = dt_new_xuhao.Rows[0]["BM_TUUNITWGHT"].ToString();
                newrow[14] = dt_new_xuhao.Rows[0]["BM_TUMAQLTY"].ToString();
                newrow[15] = dt_new_xuhao.Rows[0]["BM_TUSTAD"].ToString();
                newrow[16] = dt_new_xuhao.Rows[0]["BM_TUPROBLEM"].ToString();
                newrow[17] = dt_new_xuhao.Rows[0]["BM_MAQUALITY"].ToString();
                newrow[18] = dt_new_xuhao.Rows[0]["BM_GUIGE"].ToString();
                newrow[19] = dt_new_xuhao.Rows[0]["BM_MANAME"].ToString();
                newrow[20] = dt_new_xuhao.Rows[0]["BM_MAGUIGE"].ToString();
                newrow[21] = dt_new_xuhao.Rows[0]["BM_THRYWGHT"].ToString();
                newrow[22] = Convert.ToDouble(dt_new_xuhao.Rows[0]["BM_UNITWGHT"].ToString()) * Convert.ToDouble(dt_new_xuhao.Rows[0]["BM_SINGNUMBER"].ToString()) * Convert.ToInt16(lblNumber.Text);//现总重=单重*单台数量*现台数
                newrow[23] =(Convert.ToDouble(dt_new_xuhao.Rows[0]["BM_MATOTALLGTH"].ToString())/Convert.ToDouble(dt_new_xuhao.Rows[0]["BM_PNUMBER"].ToString()))* Convert.ToDouble(dt_new_xuhao.Rows[0]["BM_SINGNUMBER"].ToString()) * Convert.ToInt16(lblNumber.Text);//现总长=(材料总长/计划数量)*单台数量*现台数
                newrow[24] = dt_new_xuhao.Rows[0]["BM_MAUNIT"].ToString();
                newrow[25] = dt_new_xuhao.Rows[0]["BM_STANDARD"].ToString();
                newrow[26] = dt_new_xuhao.Rows[0]["BM_MASHAPE"].ToString();
                newrow[27] = dt_new_xuhao.Rows[0]["BM_MASTATE"].ToString();
                newrow[28] = dt_new_xuhao.Rows[0]["BM_PROCESS"].ToString();
                newrow[29] = dt_new_xuhao.Rows[0]["BM_ENGSHNAME"].ToString();
                newrow[30] = dt_new_xuhao.Rows[0]["BM_KEYCOMS"].ToString();
                newrow[31] = dt_new_xuhao.Rows[0]["BM_FIXEDSIZE"].ToString();
                newrow[32] = dt_new_xuhao.Rows[0]["BM_WMARPLAN"].ToString();
                newrow[33] = dt_new_xuhao.Rows[0]["BM_KU"].ToString();
                newrow[34] = Convert.ToDouble(dt_new_xuhao.Rows[0]["BM_SINGNUMBER"].ToString()) * Convert.ToInt16(lblNumber.Text);//现总数量=单台数量*现台数
                newrow[35] = Convert.ToDouble(dt_new_xuhao.Rows[0]["BM_SINGNUMBER"].ToString()) * Convert.ToInt16(lblNumber.Text);//计划总数量=单台数量*现台数
                newrow[36] = dt_new_xuhao.Rows[0]["BM_MPMY"].ToString();
                
                dt.Rows.Add(newrow);
            }
            dt.AcceptChanges();

            if (dt.Rows.Count > 0)
            {
                SmartGridView1.DataSource = dt;
                SmartGridView1.DataBind();
                Panel1.Visible = false;
            }
            else
            {
                SmartGridView1.DataSource = null;
                SmartGridView1.DataBind();
                Panel1.Visible = true;
            }

        }
        /// <summary>
        /// 目标工程连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTargetSelect_OnClick(object sender, EventArgs e)
        {
           this.CheckBoxSelectDefine(SmartGridView1, "CheckBox1");
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string ret = this.CheckMarNotBelongToMar();
            if (ret == "0")//检查无误
            {
                btnSave.Visible = false;
                List<string> list_sql = new List<string>();
                int insertcount = 0;//插入行数
                #region
                for (int i = 0; i < SmartGridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = SmartGridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gRow.FindControl("CheckBox1");
                    zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                    #region 获取数据
                    if (zongxu != "")
                    {
                        tuhao = ((HtmlInputText)gRow.FindControl("tuhao")).Value;
                        marid = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                        xuhao = zongxu;
                        ch_name = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                        en_name = ((HtmlInputText)gRow.FindControl("en_name")).Value.Trim();
                        guige = ((HtmlInputText)gRow.FindControl("guige")).Value.Trim();
                        cailiaoname = ((HtmlInputText)gRow.FindControl("cailiaoname")).Value.Trim();
                        cailiaoguige = ((HtmlInputText)gRow.FindControl("cailiaoguige")).Value.Trim();
                        cailiaocd = float.Parse(((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim());
                        cailiaokd = float.Parse(((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim());
                        singshuliang = float.Parse(((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim() == "" ? "1" : ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim());
                        shuliang = singshuliang*Convert.ToInt16(lblNumber.Text.Trim());
                        p_shuliang = float.Parse(((HtmlInputText)gRow.FindControl("plan_shuliang")).Value.Trim() == "" ? shuliang.ToString() : ((HtmlInputText)gRow.FindControl("plan_shuliang")).Value.Trim());
                        lilunzhl = float.Parse(((HtmlInputText)gRow.FindControl("lilunzhl")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("lilunzhl")).Value.Trim());
                        //材料单重
                        cailiaodzh = float.Parse(((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim());
                        //材料总重
                        cailiaozongzhong = float.Parse(((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim());
                        cailiaozongchang = float.Parse(((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim());
                        bgzmy = float.Parse(((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim());
                        mpmy = float.Parse(((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim());
                        tjsx = ((HtmlInputText)gRow.FindControl("tjsx")).Value.Trim();
                        caizhi = ((HtmlInputText)gRow.FindControl("caizhi")).Value.Trim();
                        if (marid != "")
                        {
                            //单重
                            dzh = float.Parse(((HtmlInputText)gRow.FindControl("dzh")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("dzh")).Value.Trim());
                            //总重
                            zongzhong = float.Parse(((HtmlInputText)gRow.FindControl("zongzhong")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("zongzhong")).Value.Trim());
                        }
                        else
                        {
                            //单重
                            dzh = 0;
                            //总重
                            zongzhong = 0;
                            //材料单重
                            cailiaodzh = 0;
                            //材料总重
                            cailiaozongzhong = 0;
                        }

                        labunit = ((HtmlInputText)gRow.FindControl("labunit")).Value.Trim();
                        xinzhuang = ((HtmlInputText)gRow.FindControl("xinzhuang")).Value.Trim();
                        zhuangtai = ((HtmlInputText)gRow.FindControl("zhuangtai")).Value.Trim();
                        biaozhun = ((HtmlInputText)gRow.FindControl("biaozhun")).Value.Trim();
                        process = ((HtmlInputText)gRow.FindControl("process")).Value.Trim();
                        beizhu = ((HtmlInputText)gRow.FindControl("beizhu")).Value.Trim();
                        ddlKeyComponents = ((DropDownList)gRow.FindControl("ddlKeyComponents")).SelectedValue;
                        ddlFixedSize = ((DropDownList)gRow.FindControl("ddlFixedSize")).SelectedValue;
                        ddlwmp = ((DropDownList)gRow.FindControl("ddlWmp")).SelectedValue;
                        tudz = float.Parse(((HtmlInputText)gRow.FindControl("tudz")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("tudz")).Value.Trim());
                        if (tudz == 0)
                        {
                            tudz = float.Parse(((HtmlInputText)gRow.FindControl("dzh")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("dzh")).Value.Trim());
                        }
                        
                        tuzongzhong = tudz * shuliang;
                        tucz = ((HtmlInputText)gRow.FindControl("tucz")).Value.Trim();
                        tuwt = ((HtmlInputText)gRow.FindControl("tuwt")).Value.Trim();
                        tustrd = ((HtmlInputText)gRow.FindControl("tubz")).Value.Trim();
                        ku = ((HtmlInputText)gRow.FindControl("ku")).Value.Trim();
                        Insertbind(list_sql);
                        insertcount++;
                    }
                    #endregion
                }
                #endregion
                DBCallCommon.ExecuteTrans(list_sql);
                btnSave.Visible = true;
                SmartGridView1.DataSource = this.AfterSavedReload();
                SmartGridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:原始数据已保存!\\r\\r影响行数:" + insertcount + "');", true);/////window.location.reload();
            }
            else if (ret.Contains("Page-"))//页面上存在底层材料归属
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"与页面上其父序均为底层材料！！！');", true);
            }
            else if (ret.Contains("DataBase-"))//页面上记录与数据库中存在归属
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"与在数据库中父序为底层材料！！！');", true);
            }
            else if (ret.Contains("DataBaseRepeat"))//页面总序与数据库中重复
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"在数据库中已存在！！！');", true);
            }
            else if (ret.Contains("PageRepeat"))//页面总序重复
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"重复！！！');", true);
            }
            else if (ret.Contains("XuHaoExisted"))//输入部件时要检查序号是否存在
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"，与其相同的序号在数据库中已存在！！！');", true);
            }
            else if (ret.Contains("FormError-"))//总序格式错误
            {
                string[] aa = ret.Split('-');
                string firstchar = aa[aa.Length - 2].ToString();
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"格式错误！！！\\r\\r正确格式：\"" + firstchar + "\"或\"" + firstchar + ".m...（m为正整数）');", true);
            }
            else if (ret.Contains("FatherNotExist-"))//父序不存在
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r总序\"" + outxuhao + "\"的父序不存在！！！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r未知错误，请与管理员联系！！！');", true);
            }
        }
        /// <summary>
        /// 检查虚拟部件序号是否存在，不存在则创建，如1.0,同时记录1.0下的物料条数
        /// </summary>
        private void CheckZeroXuHaoExist()
        {
            ViewState["virtualPartNumsNow"] = 1;//用于记录虚拟部件下物料记录条数
            string[] taskid_split =hdfEngid.Value.Trim().Split('-');
            ViewState["zero_index"] = "1.0";
            string sqltext = "select count(*) from " + ViewState["tablename"].ToString() + " where BM_ENGID LIKE '" + taskid_split[0] + "-%' and BM_XUHAO like '" + ViewState["zero_index"].ToString() + "%'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows[0][0].ToString() == "0")//不存在虚拟部件X.0
            {
                List<string> list_sql = new List<string>();
                //插入原始数据表1/6
                string sql_org = "insert into " + ViewState["tablename"].ToString() + "(BM_XUHAO,BM_TUHAO,BM_ZONGXU,BM_CHANAME,BM_PJID,BM_ENGID,BM_MAUNITWGHT,BM_MATOTALWGHT,BM_SINGNUMBER,BM_NUMBER,BM_UNITWGHT,BM_TOTALWGHT,BM_MPSTATE,BM_MSSTATE,BM_OSSTATE,BM_MPSTATUS,BM_MSSTATUS,BM_OSSTATUS) Values('" + ViewState["zero_index"] + "','','" + ViewState["zero_index"] + "','虚拟部件','" + hdfProid.Value + "','" + hdfEngid.Value + "',0,0,1,"+lblNumber.Text+",0,0,'0','0','0','0','0','0')";
                list_sql.Add(sql_org);
                //插入原始数据物料表TBPM_TEMPMARDATA
                string sql_temp = "insert into TBPM_TEMPMARDATA(BM_XUHAO,BM_ZONGXU,BM_MARID,BM_ENGID,BM_MAUNITWGHT,BM_UNITWGHT,BM_NUMBER,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH)" +
                    " Values('" + ViewState["zero_index"].ToString() + "','" + ViewState["zero_index"].ToString() + "','','" + hdfEngid.Value + "',0,0,1,0,0,0)";
                list_sql.Add(sql_temp);
                DBCallCommon.ExecuteTrans(list_sql);
            }
        }
        /// <summary>
        /// 数据删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            if (txtid.Value != "0")
            {
                System.Data.DataTable dt = this.GetDataFromGrid();
                for (int i = int.Parse(txtid.Value) - 1; i < SmartGridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = SmartGridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gRow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        dt.Rows.RemoveAt(i - count);
                        count++;
                    }
                }
                this.SmartGridView1.DataSource = dt;
                this.SmartGridView1.DataBind();
                ////////////InitGridview();
            }
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInsert_OnClick(object sender, EventArgs e)
        {
            if (istid.Value == "1")//相当于确定
            {
                System.Data.DataTable dt = this.GetDataFromGrid();
                for (int i = 0; i < SmartGridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = SmartGridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gRow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow[30] = "N";
                        newRow[31] = "N";
                        newRow[32] = "Y";
                        dt.Rows.InsertAt(newRow, i + 1 + count);
                        ////////dt.Rows.RemoveAt(SmartGridView1.Rows.Count - 1);
                        count++;
                    }
                }
                this.SmartGridView1.DataSource = dt;
                this.SmartGridView1.DataBind();
                ////////////InitGridview();
            }
        }
        /// <summary>
        /// 初始化Gridview
        /// </summary>
        private void InitGridview()
        {
            System.Data.DataTable dt = this.GetDataFromGrid();
            SmartGridView1.DataSource = dt;
            SmartGridView1.DataBind();
        }

        /// <summary>
        /// 返回源工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnGotoSource_OnClick(object sender, EventArgs e)
        {
            TabContainer1.ActiveTabIndex = 0;
        }
        /// <summary>
        /// 全部删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReload_OnClick(object sender, EventArgs e)
        {
            SmartGridView1.DataSource = null;
            SmartGridView1.DataBind();

            SmartGridView1.DataSource = this.AfterSavedReload();
            SmartGridView1.DataBind();


        }
        /// <summary>
        /// 数据源清空重选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ddlProName.Enabled = true;
            ddlEngName.Enabled = true;
        }
        /// <summary>
        /// 转到目标页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnGotoTarget_OnClick(object sender, EventArgs e)
        {
            TabContainer1.ActiveTabIndex = 1;
        }

        /// <summary>
        /// 定义Table
        /// </summary>
        /// <returns></returns>
        protected System.Data.DataTable GetDataFromGrid()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            #region
            dt.Columns.Add("BM_TUHAO");//图号0
            dt.Columns.Add("BM_MARID");//物料编码1
            dt.Columns.Add("BM_ZONGXU");//总序2
            dt.Columns.Add("BM_CHANAME");//中文名称3
            dt.Columns.Add("BM_NOTE");//备注4
            dt.Columns.Add("BM_MALENGTH");//材料长度5
            dt.Columns.Add("BM_MAWIDTH");//材料宽度6
            dt.Columns.Add("BM_NUMBER");//数量7
            dt.Columns.Add("BM_UNITWGHT");//单重8
            dt.Columns.Add("BM_MAUNITWGHT");//材料单重9
            dt.Columns.Add("BM_MATOTALWGHT");//材料总重10
            dt.Columns.Add("BM_MABGZMY");//面域(m2)11
            dt.Columns.Add("BM_BEIZHUATR");
            dt.Columns.Add("BM_TUUNITWGHT");//图纸上单重12
            dt.Columns.Add("BM_TUMAQLTY");//图纸上材质13
            dt.Columns.Add("BM_TUSTAD");//图纸上标准13
            dt.Columns.Add("BM_TUPROBLEM");//图纸上问题15
            dt.Columns.Add("BM_MAQUALITY");//材质16
            dt.Columns.Add("BM_GUIGE");//规格17
            dt.Columns.Add("BM_MANAME");//材料名称18
            dt.Columns.Add("BM_MAGUIGE");//材料规格19
            dt.Columns.Add("BM_THRYWGHT");//理论重量20
            dt.Columns.Add("BM_TOTALWGHT");//总重21
            dt.Columns.Add("BM_MATOTALLGTH");//材料总长22
            dt.Columns.Add("BM_MAUNIT");//单位23
            dt.Columns.Add("BM_STANDARD");//国标24
            dt.Columns.Add("BM_MASHAPE");//毛坯25
            dt.Columns.Add("BM_MASTATE");//状态26
            dt.Columns.Add("BM_PROCESS");//工艺流程27
            dt.Columns.Add("BM_ENGSHNAME");//英文名称28
            dt.Columns.Add("BM_KEYCOMS");//关键部件29
            dt.Columns.Add("BM_FIXEDSIZE");//是否定尺30
            dt.Columns.Add("BM_WMARPLAN");//是否提材料计划32
            dt.Columns.Add("BM_KU");//33
            dt.Columns.Add("BM_TOTALNUMBER");//34
            dt.Columns.Add("BM_PNUMBER");//计划数量
            dt.Columns.Add("BM_MPMY");//计划面域
            #endregion
            for (int i = 0; i <SmartGridView1.Rows.Count; i++)
            {
                GridViewRow gRow = SmartGridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HtmlInputText)gRow.FindControl("tuhao")).Value;
                newRow[1] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                newRow[2] = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                newRow[3] = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                newRow[4] = ((HtmlInputText)gRow.FindControl("beizhu")).Value.Trim();
                newRow[5] = ((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim();
                newRow[6] = ((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim();
                newRow[7] = ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim();
                newRow[8] = ((HtmlInputText)gRow.FindControl("dzh")).Value.Trim();
                newRow[9] = ((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim();
                newRow[10] = ((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim();
                newRow[11] = ((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim();
                newRow[12] = ((HtmlInputText)gRow.FindControl("tjsx")).Value.Trim();

                newRow[13] = ((HtmlInputText)gRow.FindControl("tudz")).Value.Trim();
                newRow[14] = ((HtmlInputText)gRow.FindControl("tucz")).Value.Trim();
                newRow[15] = ((HtmlInputText)gRow.FindControl("tubz")).Value.Trim();
                newRow[16] = ((HtmlInputText)gRow.FindControl("tuwt")).Value.Trim();

                newRow[17] = ((HtmlInputText)gRow.FindControl("caizhi")).Value.Trim();
                newRow[18] = ((HtmlInputText)gRow.FindControl("guige")).Value.Trim();
                newRow[19] = ((HtmlInputText)gRow.FindControl("cailiaoname")).Value.Trim();
                newRow[20] = ((HtmlInputText)gRow.FindControl("cailiaoguige")).Value.Trim();
                newRow[21] = ((HtmlInputText)gRow.FindControl("lilunzhl")).Value.Trim();
                newRow[22] = ((HtmlInputText)gRow.FindControl("zongzhong")).Value.Trim();
                newRow[23] = ((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim();
                newRow[24] = ((HtmlInputText)gRow.FindControl("labunit")).Value.Trim();
                newRow[25] = ((HtmlInputText)gRow.FindControl("biaozhun")).Value.Trim();
                newRow[26] = ((HtmlInputText)gRow.FindControl("xinzhuang")).Value.Trim();
                newRow[27] = ((HtmlInputText)gRow.FindControl("zhuangtai")).Value.Trim();
                newRow[28] = ((HtmlInputText)gRow.FindControl("process")).Value.Trim();
                newRow[29] = ((HtmlInputText)gRow.FindControl("en_name")).Value.Trim();
                newRow[30] = ((DropDownList)gRow.FindControl("ddlKeyComponents")).SelectedValue;
                newRow[31] = ((DropDownList)gRow.FindControl("ddlFixedSize")).SelectedValue;
                newRow[32] = ((DropDownList)gRow.FindControl("ddlWmp")).SelectedValue;
                newRow[33] = ((HtmlInputText)gRow.FindControl("ku")).Value.Trim();
                newRow[34] = ((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim();//总数量
                newRow[35] = ((HtmlInputText)gRow.FindControl("plan_shuliang")).Value.Trim();//计划数量
                newRow[36] = ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim();//计划面域
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }


        #region 分页
        PagerQueryParam pager = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = ViewState["viewtablename_src"].ToString();
            pager.PrimaryKey = "BM_ID";
            pager.ShowFields = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER";
            pager.OrderField = "dbo.f_formatstr("+ddlSort.SelectedValue+",'.')";
            pager.StrWhere = this.GetStrWhere();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 20;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion


        /// <summary>
        /// CheckBox连选(此函数勿动)
        /// </summary>
        /// <param name="smartgridview"></param>
        /// <param name="ckbname"></param>
        protected void CheckBoxSelectDefine(YYControls.SmartGridView smartgridview, string ckbname)
        {
            int startindex = -1;
            int endindex = -1;
            for (int i = 0; i < smartgridview.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)smartgridview.Rows[i].FindControl(ckbname);
                if (cbx.Checked)
                {
                    startindex = i;
                    break;
                }
            }

            for (int j = smartgridview.Rows.Count - 1; j > -1; j--)
            {
                CheckBox cbx = (CheckBox)smartgridview.Rows[j].FindControl(ckbname);
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
                    CheckBox cbx = (CheckBox)smartgridview.Rows[k].FindControl(ckbname);
                    cbx.Checked = true;
                }
            }
        }


        #region 更多操作
        /// <summary>
        /// 显示隐藏非输入列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHidden_OnClick(object sender, EventArgs e)
        {
            if (btnHidden.Text == "隐藏非输入列")
            {
                btnHidden.Text = "显示非输入列";

                SmartGridView1.Columns[19].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[21].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[22].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[23].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[24].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[26].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[27].ItemStyle.CssClass = "hidden";

                SmartGridView1.Columns[19].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[21].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[22].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[23].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[24].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[26].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[27].HeaderStyle.CssClass = "hidden";
            }
            else
            {
                btnHidden.Text = "隐藏非输入列";
                SmartGridView1.Columns[19].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[21].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[22].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[23].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[24].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[26].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[27].ItemStyle.CssClass = "show";

                SmartGridView1.Columns[19].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[21].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[22].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[23].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[24].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[26].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[27].HeaderStyle.CssClass = "show";
            }

        }
        protected void btnHiddenPaper_OnClick(object sender, EventArgs e)
        {
            if (btnHiddenPaper.Text == "隐藏图纸信息列")
            {
                btnHiddenPaper.Text = "显示图纸信息列";

                ///////SmartGridView1.Columns[15].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[16].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[17].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[18].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[31].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[32].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[33].ItemStyle.CssClass = "hidden";
                SmartGridView1.Columns[34].ItemStyle.CssClass = "hidden";

                ///////SmartGridView1.Columns[15].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[16].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[17].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[18].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[31].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[32].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[33].HeaderStyle.CssClass = "hidden";
                SmartGridView1.Columns[34].HeaderStyle.CssClass = "hidden";
            }
            else
            {
                btnHiddenPaper.Text = "隐藏图纸信息列";
                //////SmartGridView1.Columns[15].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[16].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[17].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[18].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[31].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[32].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[33].ItemStyle.CssClass = "show";
                SmartGridView1.Columns[34].ItemStyle.CssClass = "show";

                //////SmartGridView1.Columns[15].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[16].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[17].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[18].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[31].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[32].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[33].HeaderStyle.CssClass = "show";
                SmartGridView1.Columns[34].HeaderStyle.CssClass = "show";
            }
        }
        protected void btnShowAll_OnClick(object sender, EventArgs e)
        {
            if (btnHidden.Text == "显示非输入列")
            {
                this.btnHidden_OnClick(null, null);
            }

            if (btnHiddenPaper.Text == "显示图纸信息列")
            {
                this.btnHiddenPaper_OnClick(null, null);
            }

            foreach (ListItem itseleted in ckbColumn.Items)
            {
                if (itseleted.Selected)
                {
                    itseleted.Selected = false;
                    int i = Convert.ToInt16(itseleted.Value);
                    SmartGridView1.Columns[i].ItemStyle.CssClass = "show";
                    SmartGridView1.Columns[i].HeaderStyle.CssClass = "show";
                }
            }
        }
        protected void btnClearColumn_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (ListItem listitem in ckbClearColumns.Items)
            {
                if (listitem.Selected)
                {
                    listitem.Selected = false;
                    foreach (string nun in listitem.Value.Split(','))
                    {
                        list.Add(nun);
                    }
                }
            }
            foreach (string num in list)
            {
                string[] num_id = num.Split('-');
                foreach (GridViewRow grow in SmartGridView1.Rows)
                {
                    int cellnum = Convert.ToInt16(num_id[0]);
                    ((HtmlInputText)grow.Cells[cellnum].FindControl(num_id[1])).Value = "";
                }
            }
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            foreach (ListItem itseleted in ckbColumn.Items)
            {
                int i = Convert.ToInt16(itseleted.Value);
                if (itseleted.Selected)
                {
                    SmartGridView1.Columns[i].ItemStyle.CssClass = "hidden";
                    SmartGridView1.Columns[i].HeaderStyle.CssClass = "hidden";
                }
                else
                {
                    SmartGridView1.Columns[i].ItemStyle.CssClass = "show";
                    SmartGridView1.Columns[i].HeaderStyle.CssClass = "show";
                }
            }
        }
        #endregion


        /// <summary>
        /// 清空重量列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearColum_OnClick(object sender, EventArgs e)
        {
            string[] array_name = new string[4] { "dzh", "cailiaodzh", "cailiaozongzhong", "zongzhong" };
            foreach (GridViewRow gr in SmartGridView1.Rows)
            {
                TextBox txt_marid = (TextBox)gr.FindControl("marid");
                if (txt_marid.Text.Trim() == "")
                {
                    foreach (string ctlname in array_name)
                    {
                        ((HtmlInputText)gr.FindControl(ctlname)).Value = "0";
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已置0！！！');", true);
        }

        /// <summary>
        /// 插入数据(更新)
        /// </summary>
        protected void Insertbind(List<string> list)
        {
            string whInZ = "N";
            if (marid.Contains("01.01"))//标准件体现在制作明细中
            {
                whInZ = "Y";
            }
            else if (marid == "")//不含物料编码的部件体现在制作明细中
            {
                whInZ = "Y";
            }

            sqlText = "";
            sqlText = "insert into " + ViewState["tablename"] + " ";
            sqlText += "(BM_XUHAO,BM_TUHAO,BM_MARID,BM_ZONGXU,BM_CHANAME,BM_ENGSHNAME,BM_GUIGE,BM_PJID,BM_ENGID,";//8
            sqlText += "BM_MALENGTH,BM_MAWIDTH,BM_NUMBER,BM_SINGNUMBER,BM_PNUMBER,BM_MAUNITWGHT,BM_MATOTALWGHT,";//5
            sqlText += "BM_MATOTALLGTH,BM_MABGZMY,BM_UNITWGHT,BM_TOTALWGHT,";//4
            sqlText += "BM_MASHAPE,BM_MASTATE,BM_FILLMAN,";//3
            sqlText += "BM_PROCESS,BM_NOTE,BM_KEYCOMS,BM_FIXEDSIZE,";//4
            sqlText += "BM_TUUNITWGHT,BM_TUTOTALWGHT,BM_TUMAQLTY,BM_TUPROBLEM,BM_TUSTAD,BM_ISMANU,BM_BEIZHUATR,BM_WMARPLAN,BM_KU,BM_MPMY) values ";//8

            sqlText += "('" + xuhao + "','" + tuhao + "','" + marid + "','" + zongxu + "','" + ch_name + "','" + en_name + "','" + guige + "','" + hdfProid.Value + "','" + hdfEngid.Value + "',";
            sqlText += "'" + cailiaocd + "','" + cailiaokd + "','" + shuliang + "','"+singshuliang+"','"+p_shuliang+"','" + cailiaodzh + "','" + cailiaozongzhong + "',";
            sqlText += "'" + cailiaozongchang + "','" + bgzmy + "','" + dzh + "','" + zongzhong + "',";
            sqlText += "'" + xinzhuang + "','" + zhuangtai + "','" + Session["UserID"] + "',";
            sqlText += "'" + process + "','" + beizhu + "','" + ddlKeyComponents + "','" + ddlFixedSize + "',";
            sqlText += "" + tudz + "," + tuzongzhong + ",'" + tucz + "','" + tuwt + "','" + tustrd + "','" + whInZ + "','" + tjsx + "','" + ddlwmp + "','"+ku+"',"+mpmy+")";

            list.Add(sqlText);

            sqlText = "";
            sqlText = "insert into TBPM_TEMPMARDATA(BM_ZONGXU,BM_XUHAO,BM_MARID,BM_ENGID,BM_MAUNITWGHT,BM_UNITWGHT,BM_TUUNITWGHT,BM_NUMBER,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH)" +
                " Values('" + zongxu + "','" + xuhao + "','" + marid + "','" + hdfEngid.Value + "'," + cailiaozongzhong / p_shuliang + "," + dzh + "," + tudz + "," + shuliang + "," + cailiaocd + "," + cailiaokd + "," + cailiaocd + "*1.05)";//*1.05对钢板是不适用的，对钢板而言不存在材料总长（单个）
            list.Add(sqlText);
        }

        /// <summary>
        /// 保存后重新加载页面
        /// </summary>
        /// <returns></returns>
        protected System.Data.DataTable AfterSavedReload()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            #region
            dt.Columns.Add("BM_TUHAO");//图号0
            dt.Columns.Add("BM_MARID");//物料编码1
            dt.Columns.Add("BM_ZONGXU");//总序2
            dt.Columns.Add("BM_CHANAME");//中文名称3
            dt.Columns.Add("BM_NOTE");//备注4
            dt.Columns.Add("BM_MALENGTH");//材料长度5
            dt.Columns.Add("BM_MAWIDTH");//材料宽度6
            dt.Columns.Add("BM_NUMBER");//数量7
            dt.Columns.Add("BM_UNITWGHT");//单重8
            dt.Columns.Add("BM_MAUNITWGHT");//材料单重9
            dt.Columns.Add("BM_MATOTALWGHT");//材料总重10
            dt.Columns.Add("BM_MABGZMY");//面域(m2)11
            dt.Columns.Add("BM_BEIZHUATR");
            dt.Columns.Add("BM_TUUNITWGHT");//图纸上单重12
            dt.Columns.Add("BM_TUMAQLTY");//图纸上材质13
            dt.Columns.Add("BM_TUSTAD");//图纸上标准13
            dt.Columns.Add("BM_TUPROBLEM");//图纸上问题15
            dt.Columns.Add("BM_MAQUALITY");//材质16
            dt.Columns.Add("BM_GUIGE");//规格17
            dt.Columns.Add("BM_MANAME");//材料名称18
            dt.Columns.Add("BM_MAGUIGE");//材料规格19
            dt.Columns.Add("BM_THRYWGHT");//理论重量20
            dt.Columns.Add("BM_TOTALWGHT");//总重21
            dt.Columns.Add("BM_MATOTALLGTH");//材料总长22
            dt.Columns.Add("BM_MAUNIT");//单位23
            dt.Columns.Add("BM_STANDARD");//国标24
            dt.Columns.Add("BM_MASHAPE");//毛坯25
            dt.Columns.Add("BM_MASTATE");//状态26
            dt.Columns.Add("BM_PROCESS");//工艺流程27
            dt.Columns.Add("BM_ENGSHNAME");//英文名称28
            dt.Columns.Add("BM_KEYCOMS");//关键部件29
            dt.Columns.Add("BM_FIXEDSIZE");//是否定尺30
            dt.Columns.Add("BM_WMARPLAN");//是否提材料计划
            dt.Columns.Add("BM_KU");
            dt.Columns.Add("BM_TOTALNUMBER");
            dt.Columns.Add("BM_PNUMBER");//计划数量
            dt.Columns.Add("BM_MPMY");
            #endregion
            for (int i = 0; i < 1; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[30] = "N";
                newRow[31] = "N";
                newRow[32] = "Y";
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            Panel1.Visible = false;
            return dt;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private string CheckMarNotBelongToMar()
        {
            string sql_delete = "delete from TBPM_TEMPORGDATA where BM_ENGID='" + hdfEngid.Value + "'";
            DBCallCommon.ExeSqlText(sql_delete);//删除表TBPM_TEMPORGDATA中该生产制号下数据，防止意外情况未清空上次记录

            string[] a =hdfEngid.Value.Split('-');
            ////////////////////string firstCharofZX = a[a.Length - 1];
            ///////////////////string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";
            string pattern2 = @"^" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}$";
            Regex rgx = new Regex(pattern);
            Regex rgx2 = new Regex(pattern2);
            string ret = "0";
            List<string> list_sql = new List<string>();
            //检验总序格式
            for (int i = 0; i < SmartGridView1.Rows.Count; i++)
            {
                GridViewRow gRow = SmartGridView1.Rows[i];
                zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                if (zongxu != "")
                {
                    string mar = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                    if (!rgx.IsMatch(zongxu))
                    {
                        ret = "FormError-" + firstCharofZX + "-" + zongxu;
                        return ret;
                    }
                    else
                    {
                        list_sql.Add("insert into TBPM_TEMPORGDATA(BM_ZONGXU,BM_MARID,BM_ENGID) Values('" + zongxu + "','" + mar + "','" + hdfEngid.Value + "')");
                    }
                }
            }
            //检验总序输入临时表
            DBCallCommon.ExecuteTrans(list_sql);
            //检验归属关系
            ParamsCheckMarNotBelongToMar pcmar = new ParamsCheckMarNotBelongToMar();
            pcmar.StrTabeleName = ViewState["tablename"].ToString();
            pcmar.TaskID = hdfEngid.Value;
            System.Data.DataTable dt = this.ExecMarCheck(pcmar);
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
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="psv"></param>
        private System.Data.DataTable ExecMarCheck(ParamsCheckMarNotBelongToMar psv)
        {
            System.Data.DataTable dt;
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_CheckMarNotBelongToMar]");
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
        /// 列的显示与隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cklHiddenShow_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string strCheck = Request.Form["__EVENTTARGET"].ToString();

            int Index = Convert.ToInt16(strCheck.Substring(strCheck.LastIndexOf("$") + 1));

            TM_BasicFun.HiddenShowColumn(SmartGridView1, cklHiddenShow, Index, "Input");
        }
    }
}
