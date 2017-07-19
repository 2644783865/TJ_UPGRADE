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
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_ZongXuSingleSplit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInfo();
            }
        }
        /// <summary>
        /// 页面信息初始化
        /// </summary>
        protected void PageInfo()
        {
            ViewState["view_table"] = Request.QueryString["view_table"];
            ViewState["TaskID"] = Request.QueryString["TaskID"];
            switch (ViewState["view_table"].ToString())
            {
                case "View_TM_HZY":
                    ViewState["str_table"] = "TBPM_STRINFOHZY";
                    ViewState["ms_table"] = "TBPM_MSOFHZY";
                    ViewState["view_ms_table"] = "View_TM_MSOFHZY";
                    break;
                case "View_TM_QLM":
                    ViewState["str_table"] = "TBPM_STRINFOQLM";
                    ViewState["ms_table"] = "TBPM_MSOFQLM";
                    ViewState["view_ms_table"] = "View_TM_MSOFQLM";
                    break;
                case "View_TM_BLJ":
                    ViewState["str_table"] = "TBPM_STRINFOBLJ";
                    ViewState["ms_table"] = "TBPM_MSOFBLJ";
                    ViewState["view_ms_table"] = "View_TM_MSOFBLJ";
                    break;
                case "View_TM_DQLJ":
                    ViewState["str_table"] = "TBPM_STRINFODQLJ";
                    ViewState["ms_table"] = "TBPM_MSOFDQJ";
                    ViewState["view_ms_table"] = "View_TM_MSOFDQJ";
                    break;
                case "View_TM_GFB":
                    ViewState["str_table"] = "TBPM_STRINFOGFB";
                    ViewState["ms_table"] = "TBPM_MSOFGFB";
                    ViewState["view_ms_table"] = "View_TM_MSOFGFB";
                    break;
                case "View_TM_DQO":
                    ViewState["str_table"] = "TBPM_STRINFODQO";
                    ViewState["ms_table"] = "TBPM_MSOFDQO";
                    ViewState["view_ms_table"] = "View_TM_MSOFDQO";
                    break;
                default: break;
            }

            string xh = Request.QueryString["Xuhao"].ToString();
            if (xh != "undefined")
            {
                txtZongXu.Text = xh;
                this.btnQuery_OnClick(null, null);
            }
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            //初始化
            lblPartWeight.Visible = false;

            GridView1.DataSource = null;
            GridView1.DataBind();
            grvOrgData.DataSource = null;
            grvOrgData.DataBind();
            txtShuMu.Text = "";
            //原序号
            ViewState["xuhao"] = txtZongXu.Text.Trim();
            //获取虚拟部件号
            string[] tsplit = ViewState["TaskID"].ToString().Split('-');
            string djbujian = tsplit[1].ToString();
            string xulibujhao = djbujian + ".0";

            if (ViewState["xuhao"].ToString() == djbujian)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('最顶级部件\"" + djbujian + "\"无法拆分！！！');", true);
            }
            else if (ViewState["xuhao"].ToString() == xulibujhao)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('虚拟部件\"" + xulibujhao + "\"无法拆分！！！');", true);
            }
            else
            {
                string sqltext = "select * from " + ViewState["view_table"] + " where BM_ENGID='" + ViewState["TaskID"] + "' and BM_XUHAO='" + ViewState["xuhao"] + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["BM_MSSTATE"].ToString() == "0")//制作明细状态为0的才能进行进行拆分
                    {
                        if (dt.Rows[0]["BM_MSSTATUS"].ToString() != "1")
                        {
                            grvOrgData.DataSource = dt;
                            grvOrgData.DataBind();
                            if (grvOrgData.Rows.Count > 0)
                            {
                                NoDataPanel.Visible = false;
                                //原总序
                                ViewState["zongxu"] = grvOrgData.Rows[0].Cells[2].Text;
                                Form.DefaultButton = btnAdd.UniqueID;

                                string sql_select_part = "select BM_UNITWGHT,BM_TUUNITWGHT from  TBPM_TEMPMARDATA where BM_ENGID='" + ViewState["TaskID"].ToString() + "' and BM_XUHAO='" + ViewState["xuhao"].ToString() + "'";
                                DataTable dt_part = DBCallCommon.GetDTUsingSqlText(sql_select_part);

                                //部件自重
                                if (dt.Rows[0]["BM_MARID"].ToString() == "")
                                {
                                    lblPartWeight.Text = "拆分前单个部件自重(Kg):" + dt_part.Rows[0]["BM_UNITWGHT"].ToString() + "";
                                    lblPartWeight.Visible = true;

                                }
                                else
                                {
                                    lblPartWeight.Text = "";
                                    lblPartWeight.Visible = false;
                                }

                                //图纸上单个重量
                                lblTuPartWeight.Text = "拆分前图纸上单个重量(Kg):" + dt_part.Rows[0]["BM_TUUNITWGHT"].ToString();

                                //拆分制作明细变更记录提示
                                if (dt.Rows[0]["BM_MSSTATUS"].ToString() != "0")//变更记录
                                {
                                    ViewState["SplitItemState"] = "CHG";
                                    if (dt.Rows[0]["BM_MSSTATUS"].ToString() == "2")
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示：拆分记录为制作明细变更【增加】记录！！！');", true);
                                    }
                                    else if (dt.Rows[0]["BM_MSSTATUS"].ToString() == "3")
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示：拆分记录为制作明细变更【修改】记录！！！');", true);
                                    }
                                }
                                else//正常记录
                                {
                                    ViewState["SplitItemState"] = "Nor";
                                }
                            }
                            else
                            {
                                NoDataPanel.Visible = true;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法拆分！！！\\r\\r提示：序号\"" + ViewState["xuhao"].ToString() + "\"为变更删除记录！！！');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法拆分！！！\\r\\r提示：序号\"" + ViewState["xuhao"].ToString() + "\"已提交制作明细！！！');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法拆分！！！\\r\\r提示：序号\"" + ViewState["xuhao"].ToString() + "\"记录不存在！！！');", true);
                }
            }
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (NoDataPanel.Visible)
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('未输入拆分序号或记录不存在！！！');", true);
            }
            else
            {
                DataTable dt = this.GetDataTable();
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
                if (GridView1.Rows.Count > 0)
                {
                    NoDataPanelSplit.Visible = false;
                    Form.DefaultButton = btnConfirm.UniqueID;
                }
                else
                {
                    NoDataPanelSplit.Visible = true;
                }
            }
        }
        /// <summary>
        /// 定义DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            int splitNum = Convert.ToInt16(txtShuMu.Text.Trim());
            string wlbm = grvOrgData.Rows[0].Cells[4].Text.Trim() == "&nbsp;" ? "" : grvOrgData.Rows[0].Cells[4].Text.Trim();
            string blankshape = grvOrgData.Rows[0].Cells[23].Text.Trim() == "&nbsp;" ? "" : grvOrgData.Rows[0].Cells[23].Text.Trim();
            DataTable dt = new DataTable();
            dt.Columns.Add("BM_XUHAO");//序号0
            dt.Columns.Add("BM_TUHAO");//图号(标识号)1
            dt.Columns.Add("BM_ZONGXU");//总序2
            dt.Columns.Add("BM_CHANAME");//名称3
            dt.Columns.Add("BM_MARID");//物料编码4
            dt.Columns.Add("BM_MAGUIGE");//规格5
            dt.Columns.Add("BM_MAQUALITY");//材质6
            dt.Columns.Add("BM_NUMBER");//数量7
            dt.Columns.Add("BM_UNITWGHT");//单重(kg)8
            dt.Columns.Add("BM_TOTALWGHT");//总重(kg)9
            dt.Columns.Add("BM_MAUNITWGHT");//材料单重10
            dt.Columns.Add("BM_MATOTALWGHT");//材料总重11

            dt.Columns.Add("BM_TUUNITWGHT");//增加的图纸上的单重

            dt.Columns.Add("BM_MALENGTH");//长度(mm)12
            dt.Columns.Add("BM_MAWIDTH");//宽度(mm)13
            dt.Columns.Add("BM_MATOTALLGTH");//总长(mm)14
            dt.Columns.Add("BM_MABGZMY");//面域15
            dt.Columns.Add("BM_NOTE");//备注16
            dt.Columns.Add("BM_STANDARD");//国标17
            dt.Columns.Add("BM_PROCESS");//工艺流程18
            dt.Columns.Add("BM_KEYCOMS");//关键部件19
            dt.Columns.Add("BM_ISMANU");//是否体现制作明细20

            for (int i = 0; i < splitNum; i++)
            {
                GridViewRow gr = grvOrgData.Rows[0];
                DataRow newRow = dt.NewRow();
                //序号
                newRow[0] = gr.Cells[0].Text.Trim() == "&nbsp;" ? "" : gr.Cells[0].Text.Trim();
                //图号
                newRow[1] = gr.Cells[1].Text.Trim() == "&nbsp;" ? "" : gr.Cells[1].Text.Trim();
                //总序
                newRow[2] = gr.Cells[2].Text.Trim() == "&nbsp;" ? "" : gr.Cells[2].Text.Trim();
                //名称
                newRow[3] = gr.Cells[3].Text.Trim() == "&nbsp;" ? "" : gr.Cells[3].Text.Trim();
                //物料编码
                newRow[4] = gr.Cells[4].Text.Trim() == "&nbsp;" ? "" : gr.Cells[4].Text.Trim();
                //规格
                newRow[5] = gr.Cells[5].Text.Trim() == "&nbsp;" ? "" : gr.Cells[5].Text.Trim();
                //材质
                newRow[6] = gr.Cells[6].Text.Trim() == "&nbsp;" ? "" : gr.Cells[6].Text.Trim();
                //数量
                newRow[7] = "1";
                //实际单重
                newRow[8] = Math.Round(Convert.ToDouble(gr.Cells[9].Text) / splitNum, 3);
                //实际总重
                newRow[9] = Math.Round(Convert.ToDouble(gr.Cells[9].Text) /splitNum, 3);

                if (wlbm != "")
                {
                    newRow[10] = Math.Round(Convert.ToDouble(gr.Cells[10].Text)/splitNum, 3);//材料 单重
                    newRow[11] = Math.Round(Convert.ToDouble(gr.Cells[11].Text) / splitNum, 3);//材料总重
                }
                else
                {
                    newRow[10] = "0";
                    newRow[11] = "0";
                }
                newRow[12] = Math.Round(Convert.ToDouble(lblTuPartWeight.Text.Split(':')[1])/splitNum, 3);//图纸上单重
                if (blankshape == "型")
                {
                    newRow[13] = Math.Round(Convert.ToDouble(gr.Cells[12].Text)/splitNum, 2);//长度(mm)
                    newRow[14] = Math.Round(Convert.ToDouble(gr.Cells[13].Text)/splitNum, 2);//宽度(mm)
                }
                else
                {
                    newRow[13] = "0"; //Math.Round(Convert.ToDouble(gr.Cells[12].Text), 2);//长度(mm)
                    newRow[14] = "0"; //Math.Round(Convert.ToDouble(gr.Cells[13].Text), 2);//宽度(mm)
                }
                newRow[15] = Math.Round(Convert.ToDouble(gr.Cells[14].Text.Trim()) / splitNum, 2); //gr.Cells[14].Text;//总长(mm)
                newRow[16] = Math.Round(Convert.ToDouble(gr.Cells[15].Text.Trim()) / splitNum, 3);// 面域(m²)
                //备注
                newRow[17] = gr.Cells[20].Text.Trim() == "&nbsp;" ? "" : gr.Cells[20].Text.Trim();
                //国标
                newRow[18] = gr.Cells[16].Text.Trim() == "&nbsp;" ? "" : gr.Cells[16].Text.Trim();
                //工艺流程
                newRow[19] = gr.Cells[17].Text.Trim() == "&nbsp;" ? "" : gr.Cells[17].Text.Trim();
                //关键部件
                newRow[20] = gr.Cells[18].Text.Trim() == "" ? "&nbsp;" : gr.Cells[18].Text.Trim();
                //是否制作明细
                newRow[21] = gr.Cells[19].Text.Trim() == "&nbsp;" ? "" : gr.Cells[19].Text.Trim();
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            txtShuMu.Text = GridView1.Rows.Count.ToString();
            if (GridView1.Rows.Count > 2)
            {
                int delete_row_index = Convert.ToInt16(hdfDeleteRow.Value) - 1;
                DataTable dt = new DataTable();
                dt.Columns.Add("BM_XUHAO");//序号0
                dt.Columns.Add("BM_TUHAO");//图号(标识号)1
                dt.Columns.Add("BM_ZONGXU");//总序2
                dt.Columns.Add("BM_CHANAME");//名称3
                dt.Columns.Add("BM_MARID");//物料编码4
                dt.Columns.Add("BM_MAGUIGE");//规格5
                dt.Columns.Add("BM_MAQUALITY");//材质6
                dt.Columns.Add("BM_NUMBER");//数量7
                dt.Columns.Add("BM_UNITWGHT");//单重(kg)8
                dt.Columns.Add("BM_TOTALWGHT");//总重(kg)9
                dt.Columns.Add("BM_TUUNITWGHT");//增加图纸上的单重
                dt.Columns.Add("BM_MAUNITWGHT");//材料单重10
                dt.Columns.Add("BM_MATOTALWGHT");//材料总重11

                dt.Columns.Add("BM_MALENGTH");//长度(mm)12
                dt.Columns.Add("BM_MAWIDTH");//宽度(mm)13
                dt.Columns.Add("BM_MATOTALLGTH");//总长(mm)14
                dt.Columns.Add("BM_MABGZMY");//面域15
                dt.Columns.Add("BM_NOTE");//备注16

                dt.Columns.Add("BM_STANDARD");//国标17
                dt.Columns.Add("BM_PROCESS");//工艺流程18
                dt.Columns.Add("BM_KEYCOMS");//关键部件19
                dt.Columns.Add("BM_ISMANU");//是否体现制作明细20
                DataRow newrow = dt.NewRow();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((HtmlInputText)gr.FindControl("Index")).Value.Trim();//序号0
                    newRow[1] = ((TextBox)gr.FindControl("txtTuHao")).Text.Trim();//图号(标识号)1
                    newRow[2] = gr.Cells[3].Text.Trim() == "&nbsp;" ? "" : gr.Cells[3].Text.Trim();//总序2
                    newRow[3] = ((TextBox)gr.FindControl("txtChaName")).Text.Trim();//名称3
                    newRow[4] = gr.Cells[5].Text.Trim() == "&nbsp;" ? "" : gr.Cells[5].Text.Trim();//物料编码4
                    newRow[5] = gr.Cells[6].Text.Trim() == "&nbsp;" ? "" : gr.Cells[6].Text.Trim();//规格5
                    newRow[6] = gr.Cells[7].Text.Trim() == "&nbsp;" ? "" : gr.Cells[7].Text.Trim();//材质6

                    newRow[7] = Convert.ToDouble(((HtmlInputText)gr.FindControl("Num")).Value);//数量
                    newRow[8] = Convert.ToDouble(((HtmlInputText)gr.FindControl("danzhong")).Value);//单重
                    newRow[9] = Convert.ToDouble(((HtmlInputText)gr.FindControl("zongzhong")).Value);//总重
                    newRow[10] = Convert.ToDouble(((HtmlInputText)gr.FindControl("tuzidz")).Value);//图纸上的单重(kg) 


                    newRow[11] = Convert.ToDouble(((HtmlInputText)gr.FindControl("cailiaodanzhong")).Value);//材料单重(kg) 
                    newRow[12] = Convert.ToDouble(((HtmlInputText)gr.FindControl("cailiaozongzhong")).Value);//材料总重(kg) 
                    newRow[13] = Convert.ToDouble(((TextBox)gr.FindControl("txtLength")).Text.Trim());//材料长度
                    newRow[14] = Convert.ToDouble(((TextBox)gr.FindControl("txtWidth")).Text.Trim());//材料宽度
                    newRow[15] = Convert.ToDouble(((TextBox)gr.FindControl("txtTotalWidth")).Text.Trim());//材料总长
                    newRow[16] = Convert.ToDouble(((TextBox)gr.FindControl("txtBGZMY")).Text.Trim());//面域
                    newRow[17] = ((TextBox)gr.FindControl("txtBZ")).Text.Trim();//备注

                    newRow[18] = gr.Cells[18].Text.Trim() == "&nbsp;" ? "" : gr.Cells[18].Text.Trim();
                    newRow[19] = gr.Cells[19].Text.Trim() == "&nbsp;" ? "" : gr.Cells[19].Text.Trim();
                    newRow[20] = gr.Cells[20].Text.Trim() == "&nbsp;" ? "" : gr.Cells[20].Text.Trim();
                    newRow[21] = gr.Cells[21].Text.Trim() == "&nbsp;" ? "" : gr.Cells[21].Text.Trim();
                    dt.Rows.Add(newRow);
                }
                dt.Rows.RemoveAt(delete_row_index);
                dt.AcceptChanges();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                if (GridView1.Rows.Count > 0)
                {
                    NoDataPanelSplit.Visible = false;
                }
                else
                {
                    NoDataPanelSplit.Visible = true;
                }
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法删除！\\r\\r提示：\\r\\r删除时数据至少为3行！！！');", true);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 0 || GridView1.Rows.Count == 1)
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法拆分！\\r\\r提示：\\r\\r拆分后记录不存在！！！');", true);
            }
            else
            {
                string check_return = this.NumCheck();
                if (check_return == "true")//可以保存
                {
                    ///拆分序号数量
                    double totalSplitNums = Convert.ToDouble(grvOrgData.Rows[0].Cells[7].Text.Trim());
                    //拆分物料编码
                    string splitMarid = grvOrgData.Rows[0].Cells[4].Text.Trim() == "" ? "" : grvOrgData.Rows[0].Cells[4].Text.Trim();
                    //拆分条数
                    int totalSplitRows = GridView1.Rows.Count;
                    //拆分前序号
                    string beforesplitXuhao = ViewState["xuhao"].ToString();

                    //从原始数据中判断材料计划、制作明细和外协状态
                    string sqltext_checksbm = "select BM_MSSTATE,BM_MSSTATUS,BM_MSREVIEW,BM_MPSTATE,BM_MPSTATUS,BM_MPREVIEW,BM_OSSTATE,BM_OSSTATUS,BM_OSREVIEW from " + ViewState["view_table"].ToString() + " where BM_ENGID='" + ViewState["TaskID"].ToString() + "' and BM_XUHAO='" + beforesplitXuhao + "'";
                    DataTable dt_checksbm = DBCallCommon.GetDTUsingSqlText(sqltext_checksbm);

                    List<string> sql_list = new List<string>();


                    #region 原始数据拆分
                    //查询原序号下的所有数据
                    string sql_all = "select * from " + ViewState["str_table"] + " where BM_ENGID='" + ViewState["TaskID"] + "' and BM_XUHAO='" + ViewState["xuhao"] + "' and BM_ZONGXU='" + ViewState["zongxu"] + "'";
                    SqlDataReader dr_org = DBCallCommon.GetDRUsingSqlText(sql_all);
                    dr_org.Read();
                    string bm_xuhao = "";//序号-页面上读取
                    string bm_msxuhao = dr_org["BM_MSXUHAO"].ToString();//明细序号
                    string bm_tuhao = "";//图号
                    string bm_marid = dr_org["BM_MARID"].ToString();//物料编码
                    string bm_zongxu = dr_org["BM_ZONGXU"].ToString();//总序
                    string bm_chaname = "";//中文名称
                    string bm_engshname = dr_org["BM_ENGSHNAME"].ToString();//英文名称
                    string bm_guige = dr_org["BM_GUIGE"].ToString();//规格
                    string bm_pjid = dr_org["BM_PJID"].ToString();//项目ID
                    string bm_engid = dr_org["BM_ENGID"].ToString();//工程ID
                    string bm_condictionatr = dr_org["BM_CONDICTIONATR"].ToString();//条件属性
                    string bm_beizhuatr = dr_org["BM_BEIZHUATR"].ToString();//备注属性
                    double bm_malength = 0;//Convert.ToDouble(dr_org["BM_MALENGTH"] ==DBNull.Value ? "0" : dr_org["BM_MALENGTH"].ToString());//材料长度
                    double bm_mawidth = 0;// Convert.ToDouble(dr_org["BM_MAWIDTH"] == DBNull.Value ? "0" : dr_org["BM_MALENGTH"].ToString());//材料宽度
                    double bm_maunitwght = 0;// Convert.ToDouble(dr_org["BM_MAUNITWGHT"] == DBNull.Value ? "0" : dr_org["BM_MAUNITWGHT"].ToString());//材料单重
                    double bm_matotalwght = 0;// Convert.ToDouble(dr_org["BM_MATOTALWGHT"] == DBNull.Value ? "0" : dr_org["BM_MATOTALWGHT"].ToString());//材料总重
                    double bm_matotallgth = 0;// Convert.ToDouble(dr_org["BM_MATOTALLGTH"] == DBNull.Value ? "0" : dr_org["BM_MATOTALLGTH"].ToString());//材料总长
                    double bm_mabgzmy = 0;// Convert.ToDouble(dr_org["BM_MABGZMY"] == DBNull.Value ? "0" : dr_org["BM_MABGZMY"].ToString());//不规则面域
                    string bm_tumaqlty = dr_org["BM_TUMAQLTY"].ToString();//图纸上材质
                    string bm_tustad = dr_org["BM_TUSTAD"].ToString();//图纸上标准
                    string bm_tuproblem = dr_org["BM_TUPROBLEM"].ToString();//图纸问题
                    double bm_tuunitwght = 0;// Convert.ToDouble(dr_org["BM_TUUNITWGHT"] == DBNull.Value ? "0" : dr_org["BM_TUUNITWGHT"].ToString());//图纸上单重
                    double bm_tutotalwght = 0;
                    double bm_sigtuunitwght = Convert.ToDouble(dr_org["BM_SIGTUUNITWGHT"].ToString());
                    double bm_sigttotalwght = Convert.ToDouble(dr_org["BM_SIGTTOTALWGHT"].ToString());
                    double bm_calunitwght = Convert.ToDouble(dr_org["BM_CALUNITWGHT"] == DBNull.Value ? "0" : dr_org["BM_CALUNITWGHT"].ToString());//计算的单重
                    double bm_number = 0;//数量-页面上读取
                    double bm_unitwght = Convert.ToDouble(dr_org["BM_UNITWGHT"] == DBNull.Value ? "0" : dr_org["BM_UNITWGHT"].ToString());//单重
                    double bm_totalwght = Convert.ToDouble(dr_org["BM_TOTALWGHT"] == DBNull.Value ? "0" : dr_org["BM_TOTALWGHT"].ToString());//总重
                    double bm_bgzmy = Convert.ToDouble(dr_org["BM_MABGZMY"].ToString());
                    string bm_mashape = dr_org["BM_MASHAPE"].ToString();//毛坯形状
                    string bm_mastate = dr_org["BM_MASTATE"].ToString();//毛坯状态
                    string bm_ismanu = dr_org["BM_ISMANU"].ToString();//是否生产制作明细
                    string bm_process = dr_org["BM_PROCESS"].ToString();//工艺流程
                    string bm_fillman = dr_org["BM_FILLMAN"].ToString();//技术员
                    string bm_filldate = dr_org["BM_FILLDATE"].ToString();//时间
                    string bm_fixedsize = dr_org["BM_FIXEDSIZE"].ToString();//是否定尺
                    string bm_wmarplan = dr_org["BM_WMARPLAN"].ToString();//是否提材料计划
                    string bm_mpstate = dr_org["BM_MPSTATE"].ToString();//材料计划状态
                    string bm_mpstatus = dr_org["BM_MPSTATUS"].ToString();//材料计划变更状态
                    string bm_mpreview = dr_org["BM_MPREVIEW"].ToString();//材料计划审核状态
                    string bm_msstate = dr_org["BM_MSSTATE"].ToString();//明细状态
                    string bm_msstatus = dr_org["BM_MSSTATUS"].ToString();//明细变更状态
                    string bm_mstemp = dr_org["BM_MSTEMP"].ToString();//明细过渡状态
                    string bm_msreview = dr_org["BM_MSREVIEW"].ToString();//明细审核状态
                    string bm_osstate = dr_org["BM_OSSTATE"].ToString();//技术外协状态
                    string bm_osstatus = dr_org["BM_OSSTATUS"].ToString();//技术外协变更状态
                    string bm_osreview = dr_org["BM_OSREVIEW"].ToString();//技术外协审核状态
                    string bm_status = dr_org["BM_STATUS"].ToString();//变更状态(未用)
                    string bm_keycoms = dr_org["BM_KEYCOMS"].ToString();//关键部件
                    string bm_calstatus = dr_org["BM_CALSTATUS"].ToString();//原始数据算料
                    string bm_note = "";//备注
                    string bm_ku = dr_org["BM_KU"].ToString();//库
                    string bm_labour = dr_org["BM_LABOUR"].ToString();//分工
                    dr_org.Close();

                    //删除原来数据

                    //删除原始数据表
                    string sqlText = "delete from " + ViewState["str_table"] + " where BM_ENGID='" + ViewState["TaskID"] + "' and BM_XUHAO='" + ViewState["xuhao"] + "' and BM_ZONGXU='" + ViewState["zongxu"] + "'";
                    sql_list.Add(sqlText);

                    //删除原始数据临时表
                    sqlText = "delete from TBPM_TEMPMARDATA where BM_ENGID='" + ViewState["TaskID"] + "' and BM_XUHAO='" + ViewState["xuhao"] + "'";
                    sql_list.Add(sqlText);

                    for (int jj = 0; jj < GridView1.Rows.Count; jj++)
                    {
                        GridViewRow grow = GridView1.Rows[jj];
                        //新序号
                        bm_xuhao = ((HtmlInputText)grow.FindControl("Index")).Value;
                        //图号
                        bm_tuhao = ((TextBox)grow.FindControl("txtTuHao")).Text.Trim();
                        //数量
                        bm_number = Convert.ToDouble(((HtmlInputText)grow.FindControl("Num")).Value);
                        //中文名称
                        bm_chaname = ((TextBox)grow.FindControl("txtChaName")).Text.Trim();
                        //实际单重
                        bm_unitwght = Convert.ToDouble(((HtmlInputText)grow.FindControl("danzhong")).Value.Trim());
                        //实际总重
                        bm_totalwght = bm_number * bm_unitwght;
                        //材料单重
                        bm_maunitwght = Convert.ToDouble(((HtmlInputText)grow.FindControl("cailiaodanzhong")).Value.Trim());
                        //材料总重
                        bm_matotalwght = bm_number * bm_maunitwght;
                        //材料长度
                        bm_malength = Convert.ToDouble(((TextBox)grow.FindControl("txtLength")).Text.Trim());
                        //材料宽度
                        bm_mawidth = Convert.ToDouble(((TextBox)grow.FindControl("txtWidth")).Text.Trim());
                        //z材料总长
                        bm_matotallgth = Convert.ToDouble(((TextBox)grow.FindControl("txtTotalWidth")).Text.Trim());
                        //图纸上单重
                        bm_tuunitwght = Convert.ToDouble(((HtmlInputText)grow.FindControl("tuzidz")).Value.Trim());
                        //图纸上总重
                        bm_tutotalwght = bm_number * bm_tuunitwght;
                        //不规则面域
                        bm_bgzmy = Convert.ToDouble(((TextBox)grow.FindControl("txtBGZMY")).Text.Trim());
                        //备注
                        bm_note = ((TextBox)grow.FindControl("txtBZ")).Text.Trim();

                        //其他可以在执行存储过程PRO_TM_MSCalWeight时更新，如总重等

                        //插入新记录(原始数据表)
                        sqlText = "insert into " + ViewState["str_table"] + "(BM_XUHAO,BM_MSXUHAO,BM_TUHAO,BM_MARID,BM_ZONGXU,BM_CHANAME,BM_ENGSHNAME,BM_GUIGE,BM_PJID,BM_ENGID,BM_CONDICTIONATR,BM_BEIZHUATR,BM_MALENGTH,BM_MAWIDTH,BM_MAUNITWGHT,BM_MATOTALWGHT,BM_MATOTALLGTH,BM_MABGZMY,BM_TUMAQLTY,BM_TUSTAD,BM_TUPROBLEM,BM_TUUNITWGHT,BM_TUTOTALWGHT,BM_CALUNITWGHT,BM_NUMBER,BM_UNITWGHT,BM_TOTALWGHT,BM_MASHAPE,BM_MASTATE,BM_ISMANU,BM_PROCESS,BM_FILLMAN,BM_FILLDATE,BM_FIXEDSIZE,BM_WMARPLAN,BM_MPSTATE,BM_MPSTATUS,BM_MPREVIEW,BM_MSSTATE,BM_MSSTATUS,BM_MSTEMP,BM_MSREVIEW,BM_OSSTATE,BM_OSSTATUS,BM_OSREVIEW,BM_STATUS,BM_KEYCOMS,BM_CALSTATUS,BM_NOTE,BM_KU,BM_SIGTUUNITWGHT,BM_SIGTTOTALWGHT,BM_LABOUR)" +
                            " Values('" + bm_xuhao + "','" + bm_msxuhao + "','" + bm_tuhao + "','" + bm_marid + "','" + bm_zongxu + "','" + bm_chaname + "','" + bm_engshname + "','" + bm_guige + "','" + bm_pjid + "','" + bm_engid + "','" + bm_condictionatr + "','" + bm_beizhuatr + "'," + bm_malength + "," + bm_mawidth + "," + bm_maunitwght + "," + bm_matotalwght + "," + bm_matotallgth + "," + bm_mabgzmy + ",'" + bm_tumaqlty + "','" + bm_tustad + "','" + bm_tuproblem + "'," + bm_tuunitwght + "," + bm_tutotalwght + "," + bm_calunitwght + "," + bm_number + "," + bm_unitwght + "," + bm_totalwght + ",'" + bm_mashape + "','" + bm_mastate + "','" + bm_ismanu + "','" + bm_process + "','" + bm_fillman + "','" + bm_filldate + "','" + bm_fixedsize + "','"+bm_wmarplan+"','" + bm_mpstate + "','" + bm_mpstatus + "','" + bm_mpreview + "','" + bm_msstate + "','" + bm_msstatus + "','" + bm_mstemp + "','" + bm_msreview + "','" + bm_osstate + "','" + bm_osstatus + "','" + bm_osreview + "','" + bm_status + "','" + bm_keycoms + "','" + bm_calstatus + "','" + bm_note + "','" + bm_ku + "'," + bm_sigtuunitwght + "," + bm_sigttotalwght + ",'"+bm_labour+"')";
                        sql_list.Add(sqlText);
                        //插入原始数据临时表
                        sqlText = "insert into TBPM_TEMPMARDATA([BM_XUHAO],[BM_ZONGXU],[BM_MARID],[BM_ENGID],[BM_MAUNITWGHT],[BM_UNITWGHT],[BM_TUUNITWGHT],[BM_NUMBER],[BM_MAWIDTH],[BM_MALENGTH],[BM_MATOTALLGTH])" +
                            " values('" + bm_xuhao + "','" + bm_zongxu + "','" + bm_marid + "','" + bm_engid + "'," + bm_maunitwght + "," + bm_unitwght + "," + bm_tuunitwght + "," + bm_number + "," + bm_mawidth + "," + bm_malength + "," + bm_matotallgth + ")";
                        sql_list.Add(sqlText);
                    }

                    #endregion

                    #region  制作明细拆分
                    //制作明细拆分-如果制作明细变更状态为修改（删除记录不能拆分），拆分制作明细变更表和制作明细正常表
                    if (dt_checksbm.Rows[0]["BM_MSSTATUS"].ToString() == "0" || dt_checksbm.Rows[0]["BM_MSSTATUS"].ToString() == "2")//拆分制作明细正常记录或变更增加记录--只需要拆分原始数据
                    {
                        ;//原始数据在前面已拆分
                    }
                    else if (dt_checksbm.Rows[0]["BM_MSSTATUS"].ToString() == "3")//拆分制作明细正常表
                    {
                        string sql_msdetail = "select * from " + ViewState["view_ms_table"].ToString() + " where MS_ENGID='" + ViewState["TaskID"] + "' and MS_NEWINDEX='" + beforesplitXuhao + "' and MS_STATUS='0' and MS_REWSTATE='8'";
                        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_msdetail);
                        //有可能拆分的是不在制作明细中体现的
                        if (dr.HasRows)
                        {
                            dr.Read();//无论经过多少次变更，制作明细中某生产制号下某一序号只有一条正常记录
                            string ms_id = dr["MS_ID"].ToString();//ID
                            string ms_pid = dr["MS_PID"].ToString();//批号
                            string ms_chgpid = dr["MS_CHGPID"].ToString();//变更批号
                            string ms_oldindex = dr["MS_OLDINDEX"].ToString();//原序号
                            string ms_newindex = dr["MS_NEWINDEX"].ToString();//新序号
                            string ms_msxuhao = dr["MS_MSXUHAO"].ToString();//明细序号
                            string ms_tuhao = dr["MS_TUHAO"].ToString();//图号
                            string ms_zongxu = dr["MS_ZONGXU"].ToString();//总序
                            string ms_pjid = dr["MS_PJID"].ToString();//项目ID
                            string ms_engid = dr["MS_ENGID"].ToString();//工程ID
                            string ms_marid = dr["MS_MARID"].ToString();//物料编码
                            string ms_name = dr["MS_NAME"].ToString();//名称
                            string ms_guige = dr["MS_GUIGE"].ToString();//规格
                            string ms_caizhi = dr["MS_CAIZHI"].ToString();//材质
                            int ms_unum = Convert.ToInt16(dr["MS_UNUM"].ToString());//数量
                            double ms_uwght = Convert.ToDouble(dr["MS_UWGHT"].ToString());//单重
                            double ms_tlwght = Convert.ToDouble(dr["MS_TLWGHT"].ToString());//总重 
                            string ms_mashape = dr["MS_MASHAPE"].ToString();//毛坯
                            string ms_mastate = dr["MS_MASTATE"].ToString();//状态
                            string ms_standard = dr["MS_STANDARD"].ToString();//标准
                            string ms_process = dr["MS_PROCESS"].ToString();//工艺流程
                            string ms_boxcode = dr["MS_BOXCODE"].ToString();//箱号
                            string ms_keycoms = dr["MS_KEYCOMS"].ToString();//关键部件
                            string ms_state = dr["MS_STATE"].ToString();//状态
                            string ms_status = dr["MS_STATUS"].ToString();//变更状态
                            string ms_code = dr["MS_CODE"].ToString();//变更单号
                            string ms_calstatus = dr["MS_CALSTATUS"].ToString();//算料状态
                            string ms_pdstate = dr["MS_PDSTATE"].ToString();//生产操作状态
                            string ms_ku = dr["MS_KU"].ToString();
                            string ms_note = dr["MS_NOTE"].ToString();//备注
                            dr.Close();

                            //用于存放制作明细拆分后每条记录的数量
                            int[] array_ms_numn = new int[totalSplitRows];
                            if (ms_unum > totalSplitRows)//如果制作明细表中数量大于当前拆分条数，均分
                            {
                                string shul = (ms_unum / totalSplitRows).ToString();
                                int shuliang_aftersp = 0;
                                if (shul.Contains('.'))
                                {
                                    shuliang_aftersp = Convert.ToInt16(shul.Substring(0, shul.LastIndexOf('.')));
                                }
                                else
                                {
                                    shuliang_aftersp = Convert.ToInt16(shul);
                                }

                                for (int i = 0; i < totalSplitRows - 2; i++)
                                {
                                    array_ms_numn[i] = shuliang_aftersp;
                                }
                                array_ms_numn[totalSplitRows - 1] = ms_unum - shuliang_aftersp * (totalSplitRows - 1);
                                //单重(均分)
                                ms_uwght = ms_uwght / totalSplitNums;
                            }
                            else
                            {
                                for (int j = 0; j < totalSplitRows - 1; j++)
                                {
                                    array_ms_numn[j] = 1;
                                }
                                ms_uwght = ms_uwght / totalSplitRows;
                            }
                            //删除原记录
                            string sql_delete_ms_old = "delete from " + ViewState["ms_table"].ToString() + " where MS_ID=" + ms_id + "";
                            sql_list.Add(sql_delete_ms_old);
                            //插入新记录
                            for (int i = 0; i < GridView1.Rows.Count; i++)
                            {
                                GridViewRow grow = GridView1.Rows[i];
                                ms_newindex = ((HtmlInputText)grow.FindControl("Index")).Value;//新序号
                                ms_unum = array_ms_numn[i];//数量
                                ms_tlwght = ms_unum * ms_uwght;//总重
                                string sql_insert_ms = "insert into " + ViewState["ms_table"].ToString() + "(MS_PID,MS_CHGPID,MS_OLDINDEX,MS_NEWINDEX,MS_MSXUHAO,MS_TUHAO,MS_ZONGXU,MS_PJID,MS_ENGID,MS_MARID,MS_NAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,MS_TLWGHT,MS_MASHAPE,MS_MASTATE,MS_STANDARD,MS_PROCESS,MS_BOXCODE,MS_KEYCOMS,MS_STATE,MS_STATUS,MS_CODE,MS_CALSTATUS,MS_PDSTATE,MS_KU,MS_NOTE)" +
                                    " Values('" + ms_pid + "','" + ms_chgpid + "','" + ms_oldindex + "','" + ms_newindex + "','" + ms_msxuhao + "','" + ms_tuhao + "','" + ms_zongxu + "','" + ms_pjid + "','" + ms_engid + "','" + ms_marid + "','" + ms_name + "','" + ms_guige + "','" + ms_caizhi + "'," + ms_unum + "," + ms_uwght + "," + ms_tlwght + ",'" + ms_mashape + "','" + ms_mastate + "','" + ms_standard + "','" + ms_process + "','" + ms_boxcode + "','" + ms_keycoms + "','" + ms_state + "','" + ms_status + "','" + ms_code + "','" + ms_calstatus + "','" + ms_pdstate + "','" + ms_ku + "','" + ms_note + "')";
                                sql_list.Add(sql_insert_ms);
                            }
                        }
                        dr.Close();
                    }
                    #endregion

                    #region 正常材料计划表拆分

                    string mpNorWay = "MpNorNotSplit";
                    if (dt_checksbm.Rows[0]["BM_MPSTATE"].ToString() == "0" && dt_checksbm.Rows[0]["BM_MPSTATUS"].ToString() == "3")
                    {
                        mpNorWay = "Own";
                    }
                    else if (dt_checksbm.Rows[0]["BM_MPSTATE"].ToString() == "1" && dt_checksbm.Rows[0]["BM_MPSTATUS"].ToString() == "0")
                    {
                        mpNorWay = "Org";
                    }
                    else if (dt_checksbm.Rows[0]["BM_MPSTATE"].ToString() == "1" && dt_checksbm.Rows[0]["BM_MPSTATUS"].ToString() == "3" && dt_checksbm.Rows[0]["BM_MPREVIEW"].ToString() != "3")
                    {
                        mpNorWay = "Own";
                    }

                    if (mpNorWay != "MpNorNotSplit")
                    {
                        string sql_mpnor = "select * from View_TM_MPHZY where MP_ENGID='" + ViewState["TaskID"].ToString() + "' and MP_NEWXUHAO='" + beforesplitXuhao + "' and MP_STATUS='0' and MP_STATERV= '8'";
                        SqlDataReader dr_mpnor = DBCallCommon.GetDRUsingSqlText(sql_mpnor);
                        if (dr_mpnor.HasRows)
                        {
                            dr_mpnor.Read();
                            string mp_id = dr_mpnor["MP_ID"].ToString();//ID
                            string mp_pid = dr_mpnor["MP_PID"].ToString();//批号
                            string mp_chgpid = dr_mpnor["MP_CHGPID"].ToString();//变更批号
                            string mp_zongxu = dr_mpnor["MP_ZONGXU"].ToString();//总序
                            string mp_oldxuhao = dr_mpnor["MP_OLDXUHAO"].ToString();//原序号
                            string mp_newxuhao = dr_mpnor["MP_NEWXUHAO"].ToString();//新序号
                            string mp_marid = dr_mpnor["MP_MARID"].ToString();//材料ID
                            double mp_length = Convert.ToDouble(dr_mpnor["MP_LENGTH"].ToString());//长
                            double mp_length_before = mp_length;
                            double mp_width = Convert.ToDouble(dr_mpnor["MP_WIDTH"].ToString());//宽
                            double mp_width_before = mp_width;
                            string mp_pjid = dr_mpnor["MP_PJID"].ToString();//项目ID
                            string mp_engid = dr_mpnor["MP_ENGID"].ToString();//工程ID
                            double mp_weight = Convert.ToDouble(dr_mpnor["MP_WEIGHT"].ToString());//重量
                            double mp_weight_before = mp_weight;
                            double mp_number = Convert.ToDouble(dr_mpnor["MP_NUMBER"].ToString());//需用数量
                            double mp_number_before = mp_number;
                            string mp_usage = dr_mpnor["MP_USAGE"].ToString();//用途 
                            string mp_type = dr_mpnor["MP_TYPE"].ToString();//材料类别
                            string mp_timerq = dr_mpnor["MP_TIMERQ"].ToString();//时间要求
                            string mp_envreffct = dr_mpnor["MP_ENVREFFCT"].ToString();//环保影响
                            string mp_tracknum = dr_mpnor["MP_TRACKNUM"].ToString();//计划跟踪号
                            string mp_keycoms = dr_mpnor["MP_KEYCOMS"].ToString();//关键部件
                            string mp_note = dr_mpnor["MP_NOTE"].ToString();//备注
                            string mp_state = dr_mpnor["MP_STATE"].ToString();//状态(未用字段，不能删)
                            string mp_fixedsize = dr_mpnor["MP_FIXEDSIZE"].ToString();//定尺
                            string mp_status = dr_mpnor["MP_STATUS"].ToString();//变更状态
                            string mp_mashape = dr_mpnor["MP_MASHAPE"].ToString();//毛坯形状
                            string mp_tuhao = dr_mpnor["MP_TUHAO"].ToString();//图号
                            dr_mpnor.Close();

                            string sql_insert_mp = "";

                            if (mpNorWay == "Own")//按自身数量/长/宽等拆分
                            {
                                //不管是定尺还是不定尺，保持长宽不变，重量改为0，数量改为0（插入采购部的是【材料总重】）
                                mp_weight = 0;
                                mp_number = 0;
                                string newxuhaofirst = ((HtmlInputText)GridView1.Rows[0].FindControl("Index")).Value.Trim();
                                string sql_update_mpnor = "update TBPM_MPFORHZY set MP_NEWXUHAO='" + newxuhaofirst + "' where MP_ENGID='" + ViewState["TaskID"].ToString() + "' and MP_NEWXUHAO='" + beforesplitXuhao + "' and MP_STATUS='0' and MP_ID=" + mp_id + "";
                                sql_list.Add(sql_update_mpnor);
                                //拆分时对应的操作是:保持原记录不变，插入totalSplitRows-1条上述记录
                                for (int intmpnor = 1; intmpnor < GridView1.Rows.Count; intmpnor++)
                                {
                                    GridViewRow grow_mpnor = GridView1.Rows[intmpnor];
                                    mp_newxuhao = ((HtmlInputText)grow_mpnor.FindControl("Index")).Value.ToString();
                                    sql_insert_mp = "insert into TBPM_MPFORHZY(MP_PID,MP_CHGPID,MP_ZONGXU,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_LENGTH,MP_WIDTH,MP_PJID,MP_ENGID,MP_WEIGHT,MP_NUMBER,MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_TRACKNUM,MP_KEYCOMS,MP_NOTE,MP_STATE,MP_FIXEDSIZE,MP_STATUS,MP_MASHAPE,MP_TUHAO) " +
                                        " Values('" + mp_pid + "','" + mp_chgpid + "','" + mp_zongxu + "','" + mp_oldxuhao + "','" + mp_newxuhao + "','" + mp_marid + "'," + mp_length + "," + mp_width + ",'" + mp_pjid + "','" + mp_engid + "'," + mp_weight + "," + mp_number + ",'" + mp_usage + "','" + mp_type + "','" + mp_timerq + "','" + mp_envreffct + "','" + mp_tracknum + "','" + mp_keycoms + "','" + mp_note + "','" + mp_state + "','" + mp_fixedsize + "','" + mp_status + "','" + mp_mashape + "','" + mp_tuhao + "')";
                                    sql_list.Add(sql_insert_mp);
                                }
                            }
                            else if (mpNorWay == "Org")//按原始数据拆分
                            {
                                //删除材料计划正常表中的原记录
                                string sql_delete_mp = "delete from TBPM_MPFORHZY where MP_ENGID='" + ViewState["TaskID"].ToString() + "' and MP_NEWXUHAO='" + beforesplitXuhao + "' and MP_STATUS='0' and MP_ID=" + mp_id + "";
                                sql_list.Add(sql_delete_mp);

                                if (bm_fixedsize == "N")
                                #region
                                {
                                    for (int intmpnor = 0; intmpnor < GridView1.Rows.Count; intmpnor++)
                                    {
                                        GridViewRow grow_mpnor = GridView1.Rows[intmpnor];
                                        mp_newxuhao = ((HtmlInputText)grow_mpnor.FindControl("Index")).Value.ToString();
                                        mp_number = Convert.ToDouble(((HtmlInputText)grow_mpnor.FindControl("Num")).Value.ToString());
                                        mp_weight = Convert.ToDouble(((HtmlInputText)grow_mpnor.FindControl("cailiaozongzhong")).Value.ToString());
                                        if (mp_mashape.Contains("型") || mp_mashape.Contains("圆钢"))//型材插入材料总长
                                        {
                                            mp_length = Convert.ToDouble(((TextBox)grow_mpnor.FindControl("txtTotalWidth")).Text.Trim());
                                        }
                                        else
                                        {
                                            mp_length = Convert.ToDouble(((TextBox)grow_mpnor.FindControl("txtLength")).Text.Trim());
                                        }
                                        mp_width = Convert.ToDouble(((TextBox)grow_mpnor.FindControl("txtWidth")).Text.Trim());

                                        sql_insert_mp = "insert into TBPM_MPFORHZY(MP_PID,MP_CHGPID,MP_ZONGXU,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_LENGTH,MP_WIDTH,MP_PJID,MP_ENGID,MP_WEIGHT,MP_NUMBER,MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_TRACKNUM,MP_KEYCOMS,MP_NOTE,MP_STATE,MP_FIXEDSIZE,MP_STATUS,MP_MASHAPE,MP_TUHAO) " +
                                            " Values('" + mp_pid + "','" + mp_chgpid + "','" + mp_zongxu + "','" + mp_oldxuhao + "','" + mp_newxuhao + "','" + mp_marid + "'," + mp_length + "," + mp_width + ",'" + mp_pjid + "','" + mp_engid + "'," + mp_weight + "," + mp_number + ",'" + mp_usage + "','" + mp_type + "','" + mp_timerq + "','" + mp_envreffct + "','" + mp_tracknum + "','" + mp_keycoms + "','" + mp_note + "','" + mp_state + "','" + mp_fixedsize + "','" + mp_status + "','" + mp_mashape + "','" + mp_tuhao + "')";
                                        sql_list.Add(sql_insert_mp);
                                    }
                                }
                                #endregion
                                else if (bm_fixedsize == "Y")
                                #region
                                {
                                    //只改数量和序号
                                    double sum_mpnumber = 0;
                                    if (mp_mashape.Contains("型"))
                                    #region
                                    {
                                        for (int intmpnor = 0; intmpnor < GridView1.Rows.Count; intmpnor++)
                                        {
                                            GridViewRow grow_mpnor = GridView1.Rows[intmpnor];
                                            mp_newxuhao = ((HtmlInputText)grow_mpnor.FindControl("Index")).Value.ToString();
                                            if (intmpnor != (GridView1.Rows.Count - 1))
                                            {
                                                mp_length = Convert.ToDouble(((TextBox)grow_mpnor.FindControl("txtTotalWidth")).ToString());
                                                mp_number = Math.Round(mp_length / mp_length_before, 2);
                                                sum_mpnumber += mp_number;
                                            }
                                            else
                                            {
                                                mp_number = mp_number_before - sum_mpnumber;
                                            }
                                            sql_insert_mp = "insert into TBPM_MPFORHZY(MP_PID,MP_CHGPID,MP_ZONGXU,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_LENGTH,MP_WIDTH,MP_PJID,MP_ENGID,MP_WEIGHT,MP_NUMBER,MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_TRACKNUM,MP_KEYCOMS,MP_NOTE,MP_STATE,MP_FIXEDSIZE,MP_STATUS,MP_MASHAPE,MP_TUHAO) " +
                                                " Values('" + mp_pid + "','" + mp_chgpid + "','" + mp_zongxu + "','" + mp_oldxuhao + "','" + mp_newxuhao + "','" + mp_marid + "'," + mp_length_before + "," + mp_width + ",'" + mp_pjid + "','" + mp_engid + "'," + mp_weight + "," + mp_number + ",'" + mp_usage + "','" + mp_type + "','" + mp_timerq + "','" + mp_envreffct + "','" + mp_tracknum + "','" + mp_keycoms + "','" + mp_note + "','" + mp_state + "','" + mp_fixedsize + "','" + mp_status + "','" + mp_mashape + "','" + mp_tuhao + "')";
                                            sql_list.Add(sql_insert_mp);
                                        }
                                    }
                                    #endregion
                                    else if (!mp_mashape.Contains("标"))//除型材、标准件外
                                    #region
                                    {
                                        if (mp_weight_before > 0)
                                        {
                                            for (int intmpnor = 0; intmpnor < GridView1.Rows.Count; intmpnor++)
                                            {
                                                GridViewRow grow_mpnor = GridView1.Rows[intmpnor];
                                                mp_newxuhao = ((HtmlInputText)grow_mpnor.FindControl("Index")).Value.ToString();

                                                mp_number = Convert.ToDouble(((HtmlInputText)grow_mpnor.FindControl("Num")).Value.ToString());

                                                if (intmpnor != GridView1.Rows.Count - 1)
                                                {
                                                    mp_weight = Convert.ToDouble(((HtmlInputText)grow_mpnor.FindControl("cailiaozongzhong")).Value.ToString());
                                                    mp_number = Math.Round(mp_weight / mp_weight_before, 2);
                                                    sum_mpnumber += mp_number;
                                                }
                                                else
                                                {
                                                    mp_number = mp_number_before - sum_mpnumber;
                                                }

                                                sql_insert_mp = "insert into TBPM_MPFORHZY(MP_PID,MP_CHGPID,MP_ZONGXU,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_LENGTH,MP_WIDTH,MP_PJID,MP_ENGID,MP_WEIGHT,MP_NUMBER,MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_TRACKNUM,MP_KEYCOMS,MP_NOTE,MP_STATE,MP_FIXEDSIZE,MP_STATUS,MP_MASHAPE,MP_TUHAO) " +
                                                    " Values('" + mp_pid + "','" + mp_chgpid + "','" + mp_zongxu + "','" + mp_oldxuhao + "','" + mp_newxuhao + "','" + mp_marid + "'," + mp_length + "," + mp_width + ",'" + mp_pjid + "','" + mp_engid + "'," + mp_width_before + "," + mp_number + ",'" + mp_usage + "','" + mp_type + "','" + mp_timerq + "','" + mp_envreffct + "','" + mp_tracknum + "','" + mp_keycoms + "','" + mp_note + "','" + mp_state + "','" + mp_fixedsize + "','" + mp_status + "','" + mp_mashape + "','" + mp_tuhao + "')";
                                                sql_list.Add(sql_insert_mp);
                                            }
                                        }
                                        else if (mp_width_before > 0 || mp_length_before > 0)
                                        {
                                            for (int intmpnor = 0; intmpnor < GridView1.Rows.Count; intmpnor++)
                                            {
                                                GridViewRow grow_mpnor = GridView1.Rows[intmpnor];
                                                mp_newxuhao = ((HtmlInputText)grow_mpnor.FindControl("Index")).Value.ToString();

                                                mp_number = Convert.ToDouble(((HtmlInputText)grow_mpnor.FindControl("Num")).Value.ToString());
                                                mp_weight = Convert.ToDouble(((HtmlInputText)grow_mpnor.FindControl("cailiaozongzhong")).Value.ToString());
                                                mp_length = Convert.ToDouble(((TextBox)grow_mpnor.FindControl("txtLength")).ToString());
                                                mp_width = Convert.ToDouble(((TextBox)grow_mpnor.FindControl("txtWidth")).ToString());

                                                if (intmpnor != GridView1.Rows.Count - 1)
                                                {
                                                    mp_number = Math.Round(ConvertZeroToOne(mp_length) * ConvertZeroToOne(mp_width) / (ConvertZeroToOne(mp_width_before) * ConvertZeroToOne(mp_length_before)), 2);
                                                    sum_mpnumber += mp_number;
                                                }
                                                else
                                                {
                                                    mp_number = mp_number_before - sum_mpnumber;
                                                }

                                                sql_insert_mp = "insert into TBPM_MPFORHZY(MP_PID,MP_CHGPID,MP_ZONGXU,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_LENGTH,MP_WIDTH,MP_PJID,MP_ENGID,MP_WEIGHT,MP_NUMBER,MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_TRACKNUM,MP_KEYCOMS,MP_NOTE,MP_STATE,MP_FIXEDSIZE,MP_STATUS,MP_MASHAPE,MP_TUHAO) " +
                                                    " Values('" + mp_pid + "','" + mp_chgpid + "','" + mp_zongxu + "','" + mp_oldxuhao + "','" + mp_newxuhao + "','" + mp_marid + "'," + mp_length_before + "," + mp_width_before + ",'" + mp_pjid + "','" + mp_engid + "'," + mp_weight + "," + mp_number + ",'" + mp_usage + "','" + mp_type + "','" + mp_timerq + "','" + mp_envreffct + "','" + mp_tracknum + "','" + mp_keycoms + "','" + mp_note + "','" + mp_state + "','" + mp_fixedsize + "','" + mp_status + "','" + mp_mashape + "','" + mp_tuhao + "')";
                                                sql_list.Add(sql_insert_mp);
                                            }
                                        }
                                    }
                                    #endregion
                                    else //标准件
                                    #region
                                    {
                                        for (int intmpnor = 0; intmpnor < GridView1.Rows.Count; intmpnor++)
                                        {
                                            GridViewRow grow_mpnor = GridView1.Rows[intmpnor];
                                            mp_newxuhao = ((HtmlInputText)grow_mpnor.FindControl("Index")).Value.ToString();
                                            mp_number = Convert.ToDouble(((HtmlInputText)grow_mpnor.FindControl("Num")).Value.ToString());
                                            sql_insert_mp = "insert into TBPM_MPFORHZY(MP_PID,MP_CHGPID,MP_ZONGXU,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_LENGTH,MP_WIDTH,MP_PJID,MP_ENGID,MP_WEIGHT,MP_NUMBER,MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_TRACKNUM,MP_KEYCOMS,MP_NOTE,MP_STATE,MP_FIXEDSIZE,MP_STATUS,MP_MASHAPE,MP_TUHAO) " +
                                                " Values('" + mp_pid + "','" + mp_chgpid + "','" + mp_zongxu + "','" + mp_oldxuhao + "','" + mp_newxuhao + "','" + mp_marid + "'," + mp_length + "," + mp_width + ",'" + mp_pjid + "','" + mp_engid + "'," + mp_width_before + "," + mp_number + ",'" + mp_usage + "','" + mp_type + "','" + mp_timerq + "','" + mp_envreffct + "','" + mp_tracknum + "','" + mp_keycoms + "','" + mp_note + "','" + mp_state + "','" + mp_fixedsize + "','" + mp_status + "','" + mp_mashape + "','" + mp_tuhao + "')";
                                            sql_list.Add(sql_insert_mp);
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        dr_mpnor.Close();
                    }

                    #endregion

                    #region 变更材料计划表拆分
                    string mpChgWay = "MpChgNotSplit";
                    if (dt_checksbm.Rows[0]["BM_MPSTATE"].ToString() == "1" && dt_checksbm.Rows[0]["BM_MPSTATUS"].ToString() == "2" && dt_checksbm.Rows[0]["BM_MPREVIEW"].ToString() != "3")
                    {
                        mpChgWay = "Org";
                    }
                    else if (dt_checksbm.Rows[0]["BM_MPSTATE"].ToString() == "1" && dt_checksbm.Rows[0]["BM_MPSTATUS"].ToString() == "3" && dt_checksbm.Rows[0]["BM_MPREVIEW"].ToString() != "3")
                    {
                        mpChgWay = "Org";
                    }

                    //开始拆分
                    if (mpChgWay != "MpChgNotSplit")
                    {
                        string sql_insert_chg = "";
                        string sql_mpchg = "select * from View_TM_MPCHANGE where MP_ENGID='" + ViewState["TaskID"].ToString() + "' and MP_NEWXUHAO='" + beforesplitXuhao + "'";
                        SqlDataReader dr_mpchg = DBCallCommon.GetDRUsingSqlText(sql_mpchg);
                        if (dr_mpchg.HasRows)
                        {
                            dr_mpchg.Read();
                            string mp_id = dr_mpchg["MP_ID"].ToString();//ID
                            string mp_pid = dr_mpchg["MP_PID"].ToString();//批号
                            string mp_zongxu = dr_mpchg["MP_ZONGXU"].ToString();//总序
                            string mp_oldxuhao = dr_mpchg["MP_OLDXUHAO"].ToString();//原序号
                            string mp_newxuhao = dr_mpchg["MP_NEWXUHAO"].ToString();//新序号
                            string mp_marid = dr_mpchg["MP_MARID"].ToString();//材料ID
                            double mp_length = Convert.ToDouble(dr_mpchg["MP_LENGTH"].ToString());//长
                            double mp_length_before = mp_length;
                            double mp_width = Convert.ToDouble(dr_mpchg["MP_WIDTH"].ToString());//宽
                            double mp_width_before = mp_width;
                            string mp_pjid = dr_mpchg["MP_PJID"].ToString();//项目ID
                            string mp_engid = dr_mpchg["MP_ENGID"].ToString();//工程ID
                            double mp_weight = Convert.ToDouble(dr_mpchg["MP_WEIGHT"].ToString());//重量
                            double mp_weight_before = mp_weight;
                            double mp_number = Convert.ToDouble(dr_mpchg["MP_NUMBER"].ToString());//需用数量
                            double mp_number_before = mp_number;
                            string mp_usage = dr_mpchg["MP_USAGE"].ToString();//用途 
                            string mp_type = dr_mpchg["MP_TYPE"].ToString();//材料类别
                            string mp_timerq = dr_mpchg["MP_TIMERQ"].ToString();//时间要求
                            string mp_envreffct = dr_mpchg["MP_ENVREFFCT"].ToString();//环保影响
                            string mp_tracknum = dr_mpchg["MP_TRACKNUM"].ToString();//计划跟踪号
                            string mp_changestate = dr_mpchg["MP_CHANGESTATE"].ToString();//变更状态
                            string mp_keycoms = dr_mpchg["MP_KEYCOMS"].ToString();//关键部件
                            string mp_fixedsize = dr_mpchg["MP_FIXEDSIZE"].ToString();//定尺
                            string mp_note = dr_mpchg["MP_NOTE"].ToString();//备注
                            string mp_mashape = dr_mpchg["MP_MASHAPE"].ToString();//毛坯形状
                            string mp_tuhao = dr_mpchg["MP_TUHAO"].ToString();//图号

                            dr_mpchg.Close();
                            if (mpChgWay == "Org")
                            {
                                string sql_delete_mpchg = "delete from TBPM_MPCHANGE where MP_ID=" + mp_id + "";
                                sql_list.Add("sql_delete_mpchg");
                                for (int intmpchg = 0; intmpchg < GridView1.Rows.Count; intmpchg++)
                                {
                                    GridViewRow grow_mpnor = GridView1.Rows[intmpchg];
                                    mp_newxuhao = ((HtmlInputText)grow_mpnor.FindControl("Index")).Value.ToString();
                                    mp_number = Convert.ToDouble(((HtmlInputText)grow_mpnor.FindControl("Num")).Value.ToString());
                                    mp_weight = Convert.ToDouble(((HtmlInputText)grow_mpnor.FindControl("cailiaozongzhong")).Value.ToString());
                                    if (mp_mashape.Contains("型"))//型材插入材料总长
                                    {
                                        mp_length = Convert.ToDouble(((TextBox)grow_mpnor.FindControl("txtTotalWidth")).ToString());
                                    }
                                    else
                                    {
                                        mp_length = Convert.ToDouble(((TextBox)grow_mpnor.FindControl("txtLength")).ToString());
                                    }
                                    mp_width = Convert.ToDouble(((TextBox)grow_mpnor.FindControl("txtWidth")).ToString());

                                    sql_insert_chg = "insert into TBPM_MPCHANGE(MP_PID,MP_ZONGXU,MP_OLDXUHAO,MP_NEWXUHAO,MP_MARID,MP_LENGTH,MP_WIDTH,MP_PJID,MP_ENGID,MP_WEIGHT,MP_NUMBER,MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_TRACKNUM,MP_KEYCOMS,MP_NOTE,MP_CHANGESTATE,MP_FIXEDSIZE,MP_MASHAPE,MP_TUHAO) " +
                                        " Values('" + mp_pid + "','" + mp_zongxu + "','" + mp_oldxuhao + "','" + mp_newxuhao + "','" + mp_marid + "'," + mp_length + "," + mp_width + ",'" + mp_pjid + "','" + mp_engid + "'," + mp_weight + "," + mp_number + ",'" + mp_usage + "','" + mp_type + "','" + mp_timerq + "','" + mp_envreffct + "','" + mp_tracknum + "','" + mp_keycoms + "','" + mp_note + "','" + mp_changestate + "','" + mp_fixedsize + "','" + mp_mashape + "','" + mp_tuhao + "')";
                                    sql_list.Add(sql_insert_chg);
                                }
                            }
                        }
                        dr_mpchg.Close();
                    }

                    #endregion

                    #region 正常外协表拆分
                    string outNorWay = "OutNorNotSplit";
                    if (dt_checksbm.Rows[0]["BM_OSSTATE"].ToString() == "0" && dt_checksbm.Rows[0]["BM_OSSTATUS"].ToString() == "3")
                    {
                        outNorWay = "Own";
                    }
                    else if (dt_checksbm.Rows[0]["BM_OSSTATE"].ToString() == "1" && dt_checksbm.Rows[0]["BM_OSSTATUS"].ToString() == "0")
                    {
                        outNorWay = "Org";
                    }
                    else if (dt_checksbm.Rows[0]["BM_OSSTATE"].ToString() == "1" && dt_checksbm.Rows[0]["BM_OSSTATUS"].ToString() == "3" && dt_checksbm.Rows[0]["BM_OSREVIEW"].ToString() != "3")
                    {
                        outNorWay = "Own";
                    }

                    //开始拆分
                    if (outNorWay != "OutNorNotSplit")
                    {
                        string sql_outnor_insert = "";

                        string sql_outnor = "select * from View_TM_OUTSOURCELIST where OSL_ENGID='" + ViewState["TaskID"].ToString() + "' and OSL_NEWXUHAO='" + beforesplitXuhao + "' and OSL_STATUS='0' AND OST_STATE='8'";
                        SqlDataReader dr_outnor = DBCallCommon.GetDRUsingSqlText(sql_outnor);

                        if (dr_outnor.HasRows)
                        {
                            dr_outnor.Read();
                            string osl_id = dr_outnor["OSL_ID"].ToString();//ID
                            string osl_outsourceno = dr_outnor["OSL_OUTSOURCENO"].ToString();//外协批号
                            string osl_outsourcechgno = dr_outnor["OSL_OUTSOURCECHGNO"].ToString();//变更批号
                            string osl_biaoshino = dr_outnor["OSL_BIAOSHINO"].ToString();//标识
                            string osl_pjid = dr_outnor["OSL_PJID"].ToString();//项目ID
                            string osl_engid = dr_outnor["OSL_ENGID"].ToString();//工程ID
                            string osl_oldxuhao = dr_outnor["OSL_OLDXUHAO"].ToString();//原序号
                            string osl_newxuhao = dr_outnor["OSL_NEWXUHAO"].ToString();//新序号
                            string osl_zongxu = dr_outnor["OSL_ZONGXU"].ToString();//总序
                            string osl_marid = dr_outnor["OSL_MARID"].ToString();//物料编码
                            string osl_name = dr_outnor["OSL_NAME"].ToString();//部件名称
                            string osl_keycoms = dr_outnor["OSL_KEYCOMS"].ToString();//关键部件
                            string osl_fixedsize = dr_outnor["OSL_FIXEDSIZE"].ToString();//是否定尺
                            double osl_length = Convert.ToDouble(dr_outnor["OSL_LENGTH"].ToString());//长度
                            double osl_width = Convert.ToDouble(dr_outnor["OSL_WIDTH"].ToString());//宽度
                            double osl_unitwght = Convert.ToDouble(dr_outnor["OSL_UNITWGHT"].ToString());//单重(kg)
                            double osl_number = Convert.ToDouble(dr_outnor["OSL_NUMBER"].ToString());//数量
                            double osl_totalwghtl = Convert.ToDouble(dr_outnor["OSL_TOTALWGHTL"].ToString());//总重(kg)
                            string osl_wdepid = dr_outnor["OSL_WDEPID"].ToString();//外委部门ID
                            string osl_request = dr_outnor["OSL_REQUEST"].ToString();//制作加工要求
                            string osl_reqdate = dr_outnor["OSL_REQDATE"].ToString();//要求完成日期
                            string osl_delsite = dr_outnor["OSL_DELSITE"].ToString();//交货地点
                            string osl_tracknum = dr_outnor["OSL_TRACKNUM"].ToString();//计划跟踪号
                            string osl_state = dr_outnor["OSL_STATE"].ToString();//审核状态
                            string osl_status = dr_outnor["OSL_STATUS"].ToString();//变更状态
                            string osl_note = dr_outnor["OSL_NOTE"].ToString();//备注
                            string osl_mashape = dr_outnor["OSL_MASHAPE"].ToString();//毛坯形状
                            dr_outnor.Close();

                            if (outNorWay == "Own")
                            {
                                osl_length = 0;
                                osl_width = 0;
                                osl_unitwght = 0;
                                osl_number = 0;
                                osl_totalwghtl = 0;
                                //拆分时对应的操作是:保持原记录不变，插入totalSplitRows-1条上述记录
                                string upnewxuhao = ((HtmlInputText)GridView1.Rows[0].FindControl("Index")).Value.Trim();
                                string sql_update_outnor = "update TBPM_OUTSOURCELIST set OSL_NEWXUHAO='" + upnewxuhao + "' where OSL_ENGID='" + ViewState["TaskID"].ToString() + "' and OSL_NEWXUHAO='" + beforesplitXuhao + "' and OSL_STATUS='0' and OST_STATE='8'";
                                sql_list.Add(sql_update_outnor);
                                for (int intoutnor = 1; intoutnor < GridView1.Rows.Count; intoutnor++)
                                {
                                    GridViewRow grow_outnor = GridView1.Rows[intoutnor];
                                    osl_newxuhao = ((HtmlInputText)grow_outnor.FindControl("Index")).Value.ToString();
                                    sql_outnor_insert = "insert into TBPM_OUTSOURCELIST(OSL_OUTSOURCENO,OSL_OUTSOURCECHGNO,OSL_BIAOSHINO,OSL_PJID,OSL_ENGID,OSL_OLDXUHAO,OSL_NEWXUHAO,OSL_ZONGXU,OSL_MARID,OSL_NAME,OSL_KEYCOMS,OSL_FIXEDSIZE,OSL_LENGTH,OSL_WIDTH,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPID,OSL_REQUEST,OSL_REQDATE,OSL_DELSITE,OSL_TRACKNUM,OSL_STATE,OSL_STATUS,OSL_NOTE,OSL_MASHAPE,OSL_MASHAPE)" +
                                        " Values('" + osl_outsourceno + "','" + osl_outsourcechgno + "','" + osl_biaoshino + "','" + osl_pjid + "','" + osl_engid + "','" + osl_oldxuhao + "','" + osl_newxuhao + "','" + osl_zongxu + "','" + osl_marid + "','" + osl_name + "','" + osl_keycoms + "','" + osl_fixedsize + "'," + osl_length + "," + osl_width + "," + osl_unitwght + "," + osl_number + "," + osl_totalwghtl + ",'" + osl_wdepid + "','" + osl_request + "','" + osl_reqdate + "','" + osl_delsite + "','" + osl_tracknum + "','" + osl_state + "','" + osl_status + "','" + osl_note + "','" + osl_mashape + "')";
                                    sql_list.Add(sql_outnor_insert);
                                }
                            }
                            else if (outNorWay == "Org")
                            {
                                string sql_outnor_delete = "delete from TBPM_OUTSOURCELIST where OSL_ENGID='" + ViewState["TaskID"].ToString() + "' and OSL_NEWXUHAO='" + beforesplitXuhao + "' and OSL_STATUS='0' and OST_STATE='8'";
                                sql_list.Add(sql_outnor_delete);
                                for (int intoutchg = 0; intoutchg < GridView1.Rows.Count; intoutchg++)
                                {
                                    GridViewRow grow_outchg = GridView1.Rows[intoutchg];
                                    osl_newxuhao = ((HtmlInputText)grow_outchg.FindControl("Index")).Value.Trim();
                                    osl_length = Convert.ToDouble(((TextBox)grow_outchg.FindControl("txtLength")).Text.Trim());
                                    osl_width = Convert.ToDouble(((TextBox)grow_outchg.FindControl("txtWidth")).Text.Trim());
                                    osl_unitwght = Convert.ToDouble(((HtmlInputText)grow_outchg.FindControl("danzhong")).Value.ToString());//这里究竟是单重还是材料单重，最好要区分
                                    osl_number = Convert.ToDouble(((HtmlInputText)grow_outchg.FindControl("Num")).Value.ToString());
                                    osl_totalwghtl = Convert.ToDouble(((HtmlInputText)grow_outchg.FindControl("zongzhong")).Value.ToString());
                                    sql_outnor_insert = "insert into TBPM_OUTSOURCELIST(OSL_OUTSOURCENO,OSL_OUTSOURCECHGNO,OSL_BIAOSHINO,OSL_PJID,OSL_ENGID,OSL_OLDXUHAO,OSL_NEWXUHAO,OSL_ZONGXU,OSL_MARID,OSL_NAME,OSL_KEYCOMS,OSL_FIXEDSIZE,OSL_LENGTH,OSL_WIDTH,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPID,OSL_REQUEST,OSL_REQDATE,OSL_DELSITE,OSL_TRACKNUM,OSL_STATE,OSL_STATUS,OSL_NOTE,OSL_MASHAPE)" +
                                        " Values('" + osl_outsourceno + "','" + osl_outsourcechgno + "','" + osl_biaoshino + "','" + osl_pjid + "','" + osl_engid + "','" + osl_oldxuhao + "','" + osl_newxuhao + "','" + osl_zongxu + "','" + osl_marid + "','" + osl_name + "','" + osl_keycoms + "','" + osl_fixedsize + "'," + osl_length + "," + osl_width + "," + osl_unitwght + "," + osl_number + "," + osl_totalwghtl + ",'" + osl_wdepid + "','" + osl_request + "','" + osl_reqdate + "','" + osl_delsite + "','" + osl_tracknum + "','" + osl_state + "','" + osl_status + "','" + osl_note + "','" + osl_mashape + "')";
                                    sql_list.Add(sql_outnor_insert);
                                }
                            }
                        }
                        dr_outnor.Close();
                    }
                    #endregion

                    #region 变更外协表拆分
                    string outChgWay = "OutChgNotSplit";
                    if (dt_checksbm.Rows[0]["BM_OSSTATE"].ToString() == "1" && dt_checksbm.Rows[0]["BM_OSSTATUS"].ToString() == "2" && dt_checksbm.Rows[0]["BM_OSREVIEW"].ToString() != "3")
                    {
                        outChgWay = "Org";
                    }
                    else if (dt_checksbm.Rows[0]["BM_OSSTATE"].ToString() == "1" && dt_checksbm.Rows[0]["BM_OSSTATUS"].ToString() == "3" && dt_checksbm.Rows[0]["BM_OSREVIEW"].ToString() != "3")
                    {
                        outChgWay = "Org";
                    }
                    //开始拆分
                    if (outChgWay != "OutChgNotSplit")
                    {
                        string sql_outchg_insert = "";
                        string sql_outchg = "select * from View_TM_OUTSCHANGE where OSL_ENGID='" + ViewState["TaskID"].ToString() + "' and OSL_NEWXUHAO='" + beforesplitXuhao + "' and  (OSL_OUTSOURCENO=(select OSL_OUTSOURCECHGNO from TBPM_OUTSOURCELIST where OSL_ENGID='" + ViewState["TaskID"].ToString() + "' and OSL_NEWXUHAO='" + beforesplitXuhao + "' and OSL_STATUS='0') or OSL_STATUS='2')";
                        SqlDataReader dr_outchg = DBCallCommon.GetDRUsingSqlText(sql_outchg);
                        if (dr_outchg.HasRows)
                        {
                            dr_outchg.Read();
                            string osl_id = dr_outchg["OSL_ID"].ToString();//ID
                            string ost_changecode = dr_outchg["OST_CHANGECODE"].ToString();//委外单号
                            string osl_biaoshino = dr_outchg["OSL_BIAOSHINO"].ToString();//标识号
                            string osl_pjid = dr_outchg["OSL_PJID"].ToString();//项目ID
                            string osl_engid = dr_outchg["OSL_ENGID"].ToString();//工程ID
                            string osl_marid = dr_outchg["OSL_MARID"].ToString();//物料ID
                            string osl_oldxuhao = dr_outchg["OSL_OLDXUHAO"].ToString();//原序号
                            string osl_newxuhao = dr_outchg["OSL_NEWXUHAO"].ToString();//新序号
                            string osl_zongxu = dr_outchg["OSL_ZONGXU"].ToString();//总序
                            string osl_name = dr_outchg["OSL_NAME"].ToString();//物料名称
                            string osl_keycoms = dr_outchg["OSL_KEYCOMS"].ToString();//是否为关键部件
                            string osl_fixedsize = dr_outchg["OSL_FIXEDSIZE"].ToString();//是否定尺
                            double osl_length = Convert.ToDouble(dr_outchg["OSL_LENGTH"].ToString());//长度
                            double osl_width = Convert.ToDouble(dr_outchg["OSL_WIDTH"].ToString());//宽度
                            double osl_unitwght = Convert.ToDouble(dr_outchg["OSL_UNITWGHT"].ToString());//单重(kg)
                            double osl_number = Convert.ToDouble(dr_outchg["OSL_NUMBER"].ToString());//数量
                            double osl_totalwghtl = Convert.ToDouble(dr_outchg["OSL_TOTALWGHTL"].ToString());//总重(kg)
                            string osl_wdepid = dr_outchg["OSL_WDEPID"].ToString();//外委部门ID
                            string osl_request = dr_outchg["OSL_REQUEST"].ToString();//制作加工要求
                            string osl_reqdate = dr_outchg["OSL_REQDATE"].ToString();//要求完成日期
                            string osl_delsite = dr_outchg["OSL_DELSITE"].ToString();//交货地点
                            string osl_tracknum = dr_outchg["OSL_TRACKNUM"].ToString();//计划跟踪号
                            string osl_state = dr_outchg["OSL_STATE"].ToString();//状态
                            string osl_status = dr_outchg["OSL_STATUS"].ToString();//变更状态
                            string osl_note = dr_outchg["OSL_NOTE"].ToString();//备注
                            string osl_mashape = dr_outchg["OSL_MASHAPE"].ToString();//毛坯形状

                            dr_outchg.Close();

                            if (outChgWay == "Org")
                            {
                                string sql_outchg_delete = "delete from TBPM_OUTSOURCELIST where OSL_ID=" + osl_id + "";
                                sql_list.Add(sql_outchg_delete);
                                for (int intoutchg = 0; intoutchg < GridView1.Rows.Count; intoutchg++)
                                {
                                    GridViewRow grow_outchg = GridView1.Rows[intoutchg];
                                    osl_newxuhao = ((HtmlInputText)grow_outchg.FindControl("Index")).Value.Trim();
                                    osl_length = Convert.ToDouble(((TextBox)grow_outchg.FindControl("txtLength")).Text.Trim());
                                    osl_width = Convert.ToDouble(((TextBox)grow_outchg.FindControl("txtWidth")).Text.Trim());
                                    osl_unitwght = Convert.ToDouble(((HtmlInputText)grow_outchg.FindControl("danzhong")).Value.ToString());//这里究竟是单重还是材料单重，最好要区分
                                    osl_number = Convert.ToDouble(((HtmlInputText)grow_outchg.FindControl("Num")).Value.ToString());
                                    osl_totalwghtl = Convert.ToDouble(((HtmlInputText)grow_outchg.FindControl("zongzhong")).Value.ToString());
                                    sql_outchg_insert = "insert into TBPM_OUTSCHANGE(OST_CHANGECODE,OSL_BIAOSHINO,OSL_PJID,OSL_ENGID,OSL_MARID,OSL_OLDXUHAO,OSL_NEWXUHAO,OSL_ZONGXU,OSL_NAME,OSL_KEYCOMS,OSL_FIXEDSIZE,OSL_LENGTH,OSL_WIDTH,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPID,OSL_REQUEST,OSL_REQDATE,OSL_DELSITE,OSL_TRACKNUM,OSL_STATE,OSL_STATUS,OSL_NOTE,OSL_MASHAPE)" +
                                        " Values('" + ost_changecode + "','" + osl_biaoshino + "','" + osl_pjid + "','" + osl_engid + "','" + osl_marid + "','" + osl_oldxuhao + "','" + osl_newxuhao + "','" + osl_zongxu + "','" + osl_name + "','" + osl_keycoms + "','" + osl_fixedsize + "'," + osl_length + "," + osl_width + "," + osl_unitwght + "," + osl_number + "," + osl_totalwghtl + ",'" + osl_wdepid + "','" + osl_request + "','" + osl_reqdate + "','" + osl_delsite + "','" + osl_tracknum + "','" + osl_state + "','" + osl_status + "','" + osl_note + "','" + osl_mashape + "')";
                                    sql_list.Add(sql_outchg_insert);
                                }
                            }
                        }
                        dr_outchg.Close();
                    }
                    #endregion


                    //////////原始数据重新计算重量（计算影响速度，暂不计算）
                    ////////sql_list.Add("exec [PRO_TM_MSCalWeight] '" + ViewState["str_table"] + "','" + ViewState["TaskID"] + "'");
                    //执行事务
                    DBCallCommon.ExecuteTrans(sql_list);

                    //提示信息
                    txtZongXu.Text = "";
                    grvOrgData.DataSource = null;
                    grvOrgData.DataBind();
                    NoDataPanel.Visible = true;
                    txtShuMu.Text = "";
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    NoDataPanelSplit.Visible = true;
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('提示:拆分成功！');", true);
                }
                #region 不能保存情况提示
                else if (check_return.Contains("ShuLiangError"))
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('数量必须为正整数！');", true);
                }
                else if (check_return == "NumberFormatError")//数量不正确
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r数值为空或格式不正确！！！');", true);
                }
                else if (check_return == "TuWghtError")
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r图纸上重量不正确！！！');", true);
                }
                else if (check_return == "PartOwnWeightError")
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r部件总量不正确！！！');", true);
                }
                else if (check_return == "MarOwnWeightError")
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r物料实际总重不正确！！！');", true);
                }
                else if (check_return == "MarWeightError")
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r材料总重不正确！！！');", true);
                }
                else if (check_return == "MyError")
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r面域不正确！！！');", true);
                }
                else if (check_return == "WidthError")
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r材料总长不正确！！！');", true);
                }
                else if (check_return == "StrdNumError")
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r数量不正确！！！');", true);
                }
                else if (check_return.Contains("xuhaoRepeatinPage"))//页面序号重复
                {
                    string[] a = check_return.Split('-');
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r页面上序号" + a[1] + "重复！！！');", true);
                }
                else if (check_return.Contains("xuhaoRepeatwithDataBase"))//与数据库中序号重复
                {
                    string[] a = check_return.Split('-');
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r页面上序号" + a[1] + "与数据库中重复！！！');", true);
                }
                else if (check_return.Contains("zongxuFormatError"))//总序格式错误
                {
                    string[] a = check_return.Split('-');
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r页面上序号" + a[1] + "格式错误或无法拆分！！！');", true);
                }
                ////////else if (check_return.Contains("PartFormatError"))//总序格式错误
                ////////{
                ////////    string[] a = check_return.Split('-');
                ////////    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r页面上序号" + a[1] + "应属于部件，输入有误！！！');", true);
                ////////}
                else if (check_return.Contains("fatherCodeNotExist"))//不存在父部件
                {
                    string[] a = check_return.Split('-');
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r页面上序号" + a[1] + "不存在父部件！！！');", true);
                }
                else if (check_return.Contains("bottomMaterialBelongTo"))//底层物料归属
                {
                    string[] a = check_return.Split('-');
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r序号" + a[1] + "归属父部件为底层物料！！！');", true);
                }
                else if (check_return.Contains("atLeastOne"))//如果非底层物料，至少存在一个原序号
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r拆分部件，页面上必须存在一个序号" + ViewState["xuhao"].ToString() + "！！！');", true);
                }
                else if (check_return.Contains("ChangeBelongError"))//变更记录归属
                {
                    string[] a = check_return.Split('-');
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r序号" + a[1] + "为变更记录，无法归属到已提交部件、正常部件或变更删除部件下！！！');", true);
                }
                else if (check_return.Contains("ChangeBelongError"))//正常记录归属
                {
                    string[] a = check_return.Split('-');
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('无法保存！\\r\\r提示：\\r\\r序号" + a[1] + "为正常记录，无法归属到已提交部件或变更删除部件下！！！');", true);
                }

                #endregion
            }
        }

        private double ConvertZeroToOne(double number)
        {
            if (number > 0)
            {
                return number;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 核对能否提交
        /// </summary>
        private string NumCheck()
        {
            string ret = "true";//返回值
            int all_num = Convert.ToInt16(grvOrgData.Rows[0].Cells[7].Text);//拆分前数量
            double all_sjzongzong = Convert.ToDouble(grvOrgData.Rows[0].Cells[9].Text);//实际总重
            double all_zongchang = Convert.ToDouble(grvOrgData.Rows[0].Cells[14].Text);//总长
            double all_cailiaozongzhong = Convert.ToDouble(grvOrgData.Rows[0].Cells[11].Text);//材料总重
            double all_my = Convert.ToDouble(grvOrgData.Rows[0].Cells[15].Text)*all_num;//面域
            int nums_in_page = GridView1.Rows.Count;//页面上GridView1行数
            string[] array_index = new string[nums_in_page];
            string marid = grvOrgData.Rows[0].Cells[4].Text == "&nbsp;" ? "" : grvOrgData.Rows[0].Cells[4].Text;

            string zzs = @"^[1-9][0-9]*$";//正整数
            Regex rgx_zzs = new Regex(zzs);
            string zs = @"^[0-9]+(\.[0-9]+){0,1}$";//正数
            Regex rgx_zs = new Regex(zs);

            double sum_zongzhong = 0;//实际总重（对于部件而言，总重一致）
            double sum_tuzongzhong = 0;//图上总重
            double sum_cailiaozongzhong = 0;//材料总重(对于物料而言，材料总重正确)
            double sum_my = 0;//对于材料，面域一致
            double sum_shuliang = 0;//数量，标准件要检查数量
            double sum_zongchang = 0;//总长

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow grow_split = GridView1.Rows[i];
                string dz_sp = ((HtmlInputText)grow_split.FindControl("danzhong")).Value.Trim();
                string zongzhong_sp = ((HtmlInputText)grow_split.FindControl("zongzhong")).Value.Trim();
                string shuliang_sp = ((HtmlInputText)grow_split.FindControl("Num")).Value.Trim();
                string caildz_sp = ((HtmlInputText)grow_split.FindControl("cailiaodanzhong")).Value.Trim();
                string cailiaozongzhong_sp = ((HtmlInputText)grow_split.FindControl("cailiaozongzhong")).Value.Trim();
                string changd_sp = ((TextBox)grow_split.FindControl("txtLength")).Text.Trim();
                string kuandu_sp = ((TextBox)grow_split.FindControl("txtWidth")).Text.Trim();
                string zongchang_sp = ((TextBox)grow_split.FindControl("txtTotalWidth")).Text.Trim();
                string my_sp = ((TextBox)grow_split.FindControl("txtBGZMY")).Text.Trim();
                string tudz_sp = ((HtmlInputText)grow_split.FindControl("tuzidz")).Value.Trim();

                array_index[i] = ((HtmlInputText)grow_split.FindControl("Index")).Value.Trim();

                if (!rgx_zzs.IsMatch(shuliang_sp))//验证数量
                {
                    return "ShuLiangError";
                }
                else
                {

                    if (!rgx_zs.IsMatch(dz_sp) || !rgx_zs.IsMatch(zongzhong_sp) || !rgx_zs.IsMatch(caildz_sp) || !rgx_zs.IsMatch(cailiaozongzhong_sp) || !rgx_zs.IsMatch(changd_sp) || !rgx_zs.IsMatch(kuandu_sp) || !rgx_zs.IsMatch(zongchang_sp) || !rgx_zs.IsMatch(my_sp) || !rgx_zs.IsMatch(tudz_sp))//验证数值
                    {
                        return "NumberFormatError";
                    }
                    else
                    {
                        sum_zongzhong += Convert.ToDouble(shuliang_sp) * Convert.ToDouble(dz_sp);
                        sum_tuzongzhong += Convert.ToDouble(shuliang_sp) * Convert.ToDouble(tudz_sp);
                        sum_cailiaozongzhong += Convert.ToDouble(cailiaozongzhong_sp);
                        sum_zongchang += Convert.ToDouble(zongchang_sp);
                        sum_my += Convert.ToDouble(my_sp);
                        sum_shuliang += Convert.ToInt16(shuliang_sp);
                    }
                }
            }
            //图纸上重量检查
            double tu_totalwght = Convert.ToDouble(lblTuPartWeight.Text.Split(':')[1].ToString()) * all_num;
            double margerror = 0;//误差值
            margerror = Math.Abs(tu_totalwght - sum_tuzongzhong);
            if (margerror >= 0.01)//最小重量误差控制在10g
            {
                return "TuWghtError";
            }

            //数量检查

            //材料重量检查

            //重量正确性检查
            if (marid == "")//部件--判断单重
            {
                double ownweight = all_num * Convert.ToDouble(lblPartWeight.Text.Split(':')[1].ToString());
                if (sum_zongzhong - ownweight >= 0.01)//最小重量误差控制在10g
                {
                    return "PartOwnWeightError";
                }
            }
            else//物料
            {
                //实际总量
                margerror = Math.Abs(sum_zongzhong - all_sjzongzong);
                if (margerror >= 0.01)//材料实际总重误差控制在10g
                {
                    return "MarOwnWeightError";
                }
                margerror = Math.Abs(all_cailiaozongzhong - sum_cailiaozongzhong);
                if (margerror >= 0.01) //材料总重最小重量误差控制在10g
                {
                    return "MarWeightError";
                }
                margerror = Math.Abs(all_my - sum_my);
                if (margerror >= 0.001)  //面域误差控制在0.001m2
                {
                    return "MyError";
                }
                margerror = Math.Abs(all_zongchang - sum_zongchang);
                if (margerror > 1)//总长误差控制在1mm
                {
                    return "WidthError";
                }
            }

            string break_flag = "0";
            //序号规范性检查
            #region
            string[] zongxu = grvOrgData.Rows[0].Cells[0].Text.Split('.');
            string firstCharofZX = zongxu[0];
            string pattern = @"^(" + firstCharofZX + "\\.[1-9][0-9]{0,1}|" + firstCharofZX + "\\.0\\.[1-9]+[0-9]*|" + firstCharofZX + "\\.[1-9][0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
            string pattern2 = @"^" + firstCharofZX + "\\.[1-9][0-9]{0,1}$";
            Regex rgx = new Regex(pattern);
            Regex rgx2 = new Regex(pattern2);
            for (int index = 0; index < array_index.Length; index++)
            {
                if (!rgx.IsMatch(array_index[index]))
                {
                    break_flag = "1";
                    ret = "zongxuFormatError-" + array_index[index];
                    break;
                }
                //////////else
                //////////{
                //////////    if(marid!=""&&rgx2.IsMatch(array_index[index]))
                //////////    {
                //////////        break_flag = "1";
                //////////        ret = "PartFormatError-" + array_index[index];
                //////////        break;
                //////////    }
                //////////}
            }

            #endregion
            //如果是父部件拆分后至少存在一个原序号
            #region
            if (break_flag == "0")
            {
                if (marid == "")
                {
                    break_flag = "1";
                    for (int i = 0; i < array_index.Length; i++)
                    {
                        if (array_index[i] == ViewState["xuhao"].ToString())
                        {
                            break_flag = "0";
                            break;
                        }
                    }
                    //无
                    if (break_flag == "1")
                    {
                        ret = "atLeastOne";
                    }
                }
            }
            #endregion
            if (break_flag == "0")
            {
                //页面总序重复检查
                #region
                for (int j = 0; j < array_index.Length - 1; j++)
                {
                    if (break_flag == "0")
                    {
                        for (int k = j + 1; k < array_index.Length; k++)
                        {
                            if (array_index[j] == array_index[k])
                            {
                                break_flag = "1";
                                ret = "xuhaoRepeatinPage-" + array_index[j];
                                break;
                            }
                        }
                    }
                    if (break_flag == "1")
                    {
                        break;
                    }
                }

                #endregion
                //数据库序号检查
                #region
                if (break_flag == "0")
                {
                    for (int m = 0; m < array_index.Length; m++)
                    {
                        string sqltext = "select count(*) from " + ViewState["view_table"] + " where BM_ENGID='" + ViewState["TaskID"] + "' and BM_XUHAO='" + array_index[m] + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            if (array_index[m] != ViewState["xuhao"].ToString())//当前查询序号
                            {
                                ret = "xuhaoRepeatwithDataBase-" + array_index[m];
                                break_flag = "1";
                                break;
                            }
                        }
                    }
                }
                #endregion
                //是否存在父部件检查
                #region
                if (break_flag == "0")
                {
                    for (int n = 0; n < array_index.Length; n++)
                    {
                        string sqltext = "select count(*) from " + ViewState["view_table"] + " where BM_ENGID='" + ViewState["TaskID"] + "' and BM_XUHAO=substring('" + array_index[n] + "',1,len('" + array_index[n] + "')-charindex('.',reverse('" + array_index[n] + "')))";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows[0][0].ToString() == "0")
                        {
                            break_flag = "1";
                            ret = "fatherCodeNotExist-" + array_index[n];
                            break;
                        }
                    }
                }
                #endregion
                //底层物料不存在归属关系检查
                #region
                if (break_flag == "0")
                {
                    for (int n = 0; n < array_index.Length; n++)
                    {
                        string sqltext = "select count(*) from " + ViewState["view_table"] + " where BM_ENGID='" + ViewState["TaskID"] + "' and BM_MARID!='' and BM_XUHAO=substring('" + array_index[n] + "',1,len('" + array_index[n] + "')-charindex('.',reverse('" + array_index[n] + "')))";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows[0][0].ToString() != "0")
                        {
                            break_flag = "1";
                            ret = "bottomMaterialBelongTo-" + array_index[n];
                            break;
                        }
                    }
                }
                #endregion
            }
            //变更拆分记录不能放到已提交部件和未变更部件上
            #region
            if (break_flag == "0")
            {
                string after_sp = "";
                string after_sp_father = "";
                string sql_check_fa = "";

                string splitxh = grvOrgData.Rows[0].Cells[0].Text.Trim();//拆分序号
                string sql_find_msstate = "select BM_MSSTATUS from " + ViewState["str_table"] + " where BM_ENGID='" + ViewState["TaskID"] + "' and BM_XUHAO='" + splitxh + "'";
                SqlDataReader dr_spchgstate = DBCallCommon.GetDRUsingSqlText(sql_find_msstate);
                dr_spchgstate.Read();
                string mschgstate = dr_spchgstate["BM_MSSTATUS"].ToString();//拆分序号的变更状态
                dr_spchgstate.Close();
                if (mschgstate != "0")//变更记录拆分_查找父部件状态（不能放在已提交部件、正常部件和变更删除部件上）
                {
                    for (int cp = 0; cp < GridView1.Rows.Count; cp++)
                    {
                        after_sp = ((HtmlInputText)GridView1.Rows[cp].Cells[1].FindControl("Index")).Value.Trim();
                        after_sp_father = after_sp.Substring(0, after_sp.LastIndexOf('.'));
                        sql_check_fa = "select count(*) from " + ViewState["str_table"] + " where BM_ENGID='" + ViewState["TaskID"] + "' and BM_XUHAO='" + after_sp_father + "' and (BM_MSSTATUS='0' or BM_MSSTATUS='1' or BM_MSSTATE='1')";//不能放在正常、变更删除或已提交的部件下
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_check_fa);
                        if (Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                        {
                            break_flag = "1";
                            ret = "ChangeBelongError-" + after_sp;
                            break;
                        }
                    }
                }
                else//正常记录拆分_查找父部件状态（不能放在已提交部件和变更删除部件上）
                {
                    for (int cp = 0; cp < GridView1.Rows.Count; cp++)
                    {
                        after_sp = ((HtmlInputText)GridView1.Rows[cp].Cells[1].FindControl("Index")).Value.Trim();
                        after_sp_father = after_sp.Substring(0, after_sp.LastIndexOf('.'));
                        sql_check_fa = "select count(*) from " + ViewState["str_table"] + " where BM_ENGID='" + ViewState["TaskID"] + "' and BM_XUHAO='" + after_sp_father + "' and (BM_MSSTATUS!='0' or BM_MSSTATE='1')";//不能放在正常、变更删除或已提交的部件下
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_check_fa);
                        if (Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                        {
                            break_flag = "1";
                            ret = "NorBelongError-" + after_sp;
                            break;
                        }
                    }
                }
            }
            #endregion
            return ret;
        }
    }
}
