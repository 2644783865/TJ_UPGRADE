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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MSAdjustOrgInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInit();
                ((Image)Master.FindControl("Image3")).Visible = false;
            }
        }

        protected void PageInit()
        {
            string taskid = Request.QueryString["TaskID"].ToString();
            string viewtable = Request.QueryString["view_table"].ToString();
            string xuhao = Request.QueryString["Xuhao"].ToString();
            ViewState["TaskID"] = taskid;
            ViewState["view_table"] = viewtable;
            ViewState["Xuhao"] = xuhao;
            if (xuhao != "undefined")
            {
                txtXuHao.Text = xuhao;
                ddlSplitNums.Enabled = false;
            }
            this.GetListName();
            //读取台数
            string sql = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" + taskid.Split('-')[0] + "'";
            SqlDataReader dr_number = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_number.HasRows)
            {
                dr_number.Read();
                lblNumber.Text = dr_number["TSA_NUMBER"].ToString();
                dr_number.Close();
            } 
            this.GetFirstInfoZongxu();

        }

        private void GetFirstInfoZongxu()
        {
            string sql = "select *,cast(BM_MABGZMY as varchar)+' | '+cast(BM_MPMY as varchar) as BM_MY,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER  from " + ViewState["view_table"].ToString() + " where BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_XUHAO='" + txtXuHao.Text.Trim() + "'";
            DataTable dt=DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 1)
                {
                    btnCheck.Visible = false;
                    btnSave.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入序号有记录有多条记录,请修改序号,保证序号不重复！！！');", true);
                }
                else
                {
                    btnCheck.Visible = true;
                    grvOrgData.DataSource = dt;
                    grvOrgData.DataBind();
                    NoDataPanel.Visible = false;
                    NoDataPanel2.Visible = false;
                    ViewState["BM_XUHAO"] = dt.Rows[0]["BM_XUHAO"].ToString();
                    ViewState["BM_TUHAO"] = dt.Rows[0]["BM_TUHAO"].ToString();
                    ViewState["BM_MARID"] = dt.Rows[0]["BM_MARID"].ToString();
                    ViewState["BM_ZONGXU"] = dt.Rows[0]["BM_ZONGXU"].ToString();
                    ViewState["BM_CHANAME"] = dt.Rows[0]["BM_CHANAME"].ToString();
                    ViewState["BM_MALENGTH"] = dt.Rows[0]["BM_MALENGTH"].ToString();
                    ViewState["BM_MAWIDTH"] = dt.Rows[0]["BM_MAWIDTH"].ToString();
                    ViewState["BM_NUMBER"] = dt.Rows[0]["BM_NUMBER"].ToString();
                    ViewState["BM_SINGNUMBER"] = dt.Rows[0]["BM_SINGNUMBER"].ToString();
                    ViewState["BM_PNUMBER"] = dt.Rows[0]["BM_PNUMBER"].ToString();
                    ViewState["BM_UNITWGHT"] = dt.Rows[0]["BM_UNITWGHT"].ToString();
                    ViewState["BM_TOTALWGHT"] = dt.Rows[0]["BM_TOTALWGHT"].ToString();
                    ViewState["BM_MATOTALLGTH"] = dt.Rows[0]["BM_MATOTALLGTH"].ToString();
                    ViewState["BM_MABGZMY"] = dt.Rows[0]["BM_MABGZMY"].ToString();
                    ViewState["BM_MPMY"] = dt.Rows[0]["BM_MPMY"].ToString();
                    ViewState["BM_GUIGE"] = dt.Rows[0]["BM_GUIGE"].ToString();
                    ViewState["BM_NOTE"] = dt.Rows[0]["BM_NOTE"].ToString();
                    ViewState["MA_SHAPE"] = dt.Rows[0]["BM_MASHAPE"].ToString();
                    ViewState["BM_MAUNITWGHT"] = dt.Rows[0]["BM_MAUNITWGHT"].ToString();
                    ViewState["BM_MATOTALWGHT"] = dt.Rows[0]["BM_MATOTALWGHT"].ToString();
                    ViewState["BM_KU"] = dt.Rows[0]["BM_KU"].ToString();

                    ViewState["BM_W"] = dt.Rows[0]["BM_WMARPLAN"].ToString();
                    ViewState["BM_PLAN"] = (dt.Rows[0]["BM_MPSTATE"].ToString() != "0" || dt.Rows[0]["BM_MPSTATUS"].ToString() != "0" || dt.Rows[0]["BM_OSSTATE"].ToString() != "0" || dt.Rows[0]["BM_OSSTATUS"].ToString() != "0");
                    string s = ViewState["BM_PLAN"].ToString();
                    #region
                    ViewState["MarFlag"] = "1";
                    if (dt.Rows[0]["BM_MARID"].ToString() != "")
                    {
                        td_marplan.Visible = true;
                        ddl_OWMarPlan.SelectedValue = ViewState["BM_W"].ToString();
                        if (ViewState["BM_PLAN"].ToString() == "False")//未提计划
                        {
                            ddl_OWMarPlan.Enabled = true;
                            btnWMarPlan.Enabled = true;
                        }
                        else
                        {
                            ddl_OWMarPlan.Enabled = false;
                            btnWMarPlan.Enabled = false;
                        }

                        string[] a = ViewState["TaskID"].ToString().Split('-');
                        string firstCharofZX ="1\\.0";
                        string pattern = @"^(" + firstCharofZX + "(\\.[0-9]+)*)$";
                        Regex rgx = new Regex(pattern);

                        if (rgx.IsMatch(ViewState["BM_XUHAO"].ToString()))
                        {

                            Label1.Text = "(物料-拆分前记录保留)";
                            string sql_mar = "select GUIGE,MWEIGHT from TBMA_MATERIAL where ID='" + dt.Rows[0]["BM_MARID"].ToString() + "'";
                            DataTable dt_mar = DBCallCommon.GetDTUsingSqlText(sql_mar);
                            ViewState["GUIGE"] = dt_mar.Rows[0]["GUIGE"].ToString();
                            ViewState["MWEIGHT"] = dt_mar.Rows[0]["MWEIGHT"].ToString();
                            this.GetInitDataFrom("1");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('拆分记录为物料时,只有序号满足【1.0.n】时才能拆分！！！\\r\\r提示:如果您确定要拆分该条记录,请将序号修改为【1.0.n】的形式');", true);
                        }
                    }
                    else
                    {
                        td_marplan.Visible = false;
                        btnWMarPlan.Enabled = false;
                        Label1.Text = "(部件-拆分前记录更新)";
                        ViewState["MarFlag"] = "0";
                        this.GetInitDataFrom("0");
                    }
                }
            }
            else
            {
                NoDataPanel.Visible = true;
                NoDataPanel2.Visible = true;
                grvOrgData.DataSource = null;
                grvOrgData.DataBind();
                Label1.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();

                Label2.Text = "";
                btnCheck.Visible = false;
                btnSave.Visible = false;
            }
                #endregion
        }

        protected void ddlSplitNums_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSplitNums.SelectedValue != "其它")
            {
                txtSplitNums.Text = ddlSplitNums.SelectedValue;
                txtSplitNums.Enabled = false;
            }
            else
            {
                txtSplitNums.Enabled = true;
                txtSplitNums.Text = "";
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            ddlSplitNums.Enabled = false;
            txtSplitNums.Enabled = false;
            this.GetFirstInfoZongxu();
            ViewState["Xuhao"] = txtXuHao.Text.Trim();
        }
        /// <summary>
        /// 初始拆分
        /// </summary>
        /// <param name="flag"></param>
        protected void GetInitDataFrom(string flag)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            #region
            dt.Columns.Add("BM_XUHAO");//序号0
            dt.Columns.Add("BM_TUHAO");//图号1
            dt.Columns.Add("BM_MARID");//物料编码2
            dt.Columns.Add("BM_ZONGXU");//总序3
            dt.Columns.Add("BM_CHANAME");//中文名称4
            dt.Columns.Add("BM_NOTE");//备注5
            dt.Columns.Add("BM_MALENGTH");//材料长度6
            dt.Columns.Add("BM_MAWIDTH");//材料宽度7
            dt.Columns.Add("BM_NUMBER");//数量8
            dt.Columns.Add("BM_UNITWGHT");//单重9
            dt.Columns.Add("BM_TOTALWGHT");//总重10
            dt.Columns.Add("BM_MATOTALLGTH");//材料总长11
            dt.Columns.Add("BM_MABGZMY");//面域(m2)12
            dt.Columns.Add("BM_GUIGE");//规格13
            dt.Columns.Add("BM_SINGNUMBER");//单台数量14
            dt.Columns.Add("BM_PNUMBER");//计划数量15
            dt.Columns.Add("BM_MAUNITWGHT");//材料单重16
            dt.Columns.Add("BM_MATOTALWGHT");//材料总重16
            dt.Columns.Add("BM_KU");//库18
            dt.Columns.Add("BM_WMARPLAN");//是否提计划19
            dt.Columns.Add("BM_MPMY");//计划面域
            dt.Columns.Add("BM_MSXUHAO");//明细序号
            #endregion
            int a = 0;
            if(flag=="0")//对于部件将原记录插入拆分项中
            {
                a = 1;
                DataRow newRow = dt.NewRow();
                newRow[0] = ViewState["BM_XUHAO"].ToString();
                newRow[1] = ViewState["BM_TUHAO"].ToString();
                newRow[2] = ViewState["BM_MARID"].ToString();
                newRow[3] = ViewState["BM_ZONGXU"].ToString();
                newRow[4] = ViewState["BM_CHANAME"].ToString();
                newRow[5] = ViewState["BM_NOTE"].ToString();
                newRow[6] = ViewState["BM_MALENGTH"].ToString();
                newRow[7] = ViewState["BM_MAWIDTH"].ToString();
                newRow[8] =(Convert.ToDouble(ViewState["BM_NUMBER"].ToString())/Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000");
                newRow[9] = "0";
                newRow[10] = "0";
                newRow[11] =(Convert.ToDouble(ViewState["BM_MATOTALLGTH"].ToString())/Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.00");
                newRow[12] = ViewState["BM_MABGZMY"].ToString();
                newRow[13] = ViewState["BM_GUIGE"].ToString();
                newRow[14] = (Convert.ToDouble(ViewState["BM_SINGNUMBER"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000");
                newRow[15] = (Convert.ToDouble(ViewState["BM_PNUMBER"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000");
                newRow[16] = "0";
                newRow[17] = "0";
                newRow[18] = ViewState["BM_KU"].ToString();
                newRow[19] = "N";
                newRow[20] = ViewState["BM_MPMY"].ToString();
                newRow[21] = "";
                dt.Rows.Add(newRow);
            }

            int num = Convert.ToInt16(txtSplitNums.Text.Trim());

            if (flag != "0" && Convert.ToDouble(ViewState["BM_NUMBER"].ToString()) % Convert.ToInt16(txtSplitNums.Text.Trim()) != 0)//物料，数量不能整除的情况
            {
                string cd;
                string cldz;
                double xishu=1;
                try
                {
                    if (ViewState["BM_MATOTALLGTH"].ToString() != "0" && ViewState["BM_MALENGTH"].ToString() != "0")//已长度提计划
                    {
                        xishu = Convert.ToDouble(ViewState["BM_MATOTALLGTH"].ToString()) / Convert.ToDouble(ViewState["BM_MALENGTH"].ToString()) / Convert.ToDouble(ViewState["BM_PNUMBER"].ToString());
                    }
                    else
                    {
                        xishu = Convert.ToDouble(ViewState["BM_MATOTALWGHT"].ToString()) / Convert.ToDouble(ViewState["BM_MAUNITWGHT"].ToString()) / Convert.ToDouble(ViewState["BM_PNUMBER"].ToString());
                    }
                }
                catch
                {
                    xishu = 1;
                }

                if (ViewState["BM_MATOTALLGTH"].ToString() != "0")
                {
                    cd = (Convert.ToDouble(ViewState["BM_MATOTALLGTH"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim()) / xishu).ToString("0.00");
                }
                else
                {
                    cd = Convert.ToDouble(ViewState["BM_MALENGTH"].ToString()).ToString("0.00");
                }

                cldz = (Convert.ToDouble(ViewState["BM_MATOTALWGHT"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim()) / xishu).ToString("0.000");

                for (int i = a; i < num; i++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = "";//明细序号
                    newRow[1] = ViewState["BM_TUHAO"].ToString();
                    newRow[2] = ViewState["BM_MARID"].ToString();
                    newRow[3] = ViewState["BM_ZONGXU"].ToString();
                    newRow[4] = ViewState["BM_CHANAME"].ToString();
                    newRow[5] = ViewState["BM_NOTE"].ToString();
                    newRow[6] = cd;
                    newRow[7] = ViewState["BM_MAWIDTH"].ToString();
                    newRow[8] = lblNumber.Text.Trim();//数量
                    newRow[9] = (flag == "0" ? "0" : (Convert.ToDouble(ViewState["BM_TOTALWGHT"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())/Convert.ToDouble(lblNumber.Text.Trim())).ToString("0.000")); 
                    newRow[10] = (flag == "0" ? "0" : (Convert.ToDouble(ViewState["BM_TOTALWGHT"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000")); 
                    newRow[11] = (Convert.ToDouble(ViewState["BM_MATOTALLGTH"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.00");
                    newRow[12] = (Convert.ToDouble(ViewState["BM_MABGZMY"].ToString()) * Convert.ToDouble(ViewState["BM_PNUMBER"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000");
                    newRow[13] = ViewState["BM_GUIGE"].ToString();
                    newRow[14] = 1;
                    newRow[15] = lblNumber.Text.Trim();
                    newRow[16] = cldz;
                    newRow[17] = (flag == "0" ? "0" : (Convert.ToDouble(ViewState["BM_MATOTALWGHT"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000")); ;
                    newRow[18] = "";
                    newRow[19] = "N";
                    newRow[20] = (Convert.ToDouble(ViewState["BM_MPMY"].ToString()) * Convert.ToDouble(ViewState["BM_PNUMBER"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000");//计划面域
                    newRow[21] = "";
                    dt.Rows.Add(newRow);
                }
            }
            else
            {
                for (int i = a; i < num; i++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = "";//明细序号
                    newRow[1] = ViewState["BM_TUHAO"].ToString();
                    newRow[2] = ViewState["BM_MARID"].ToString();
                    newRow[3] = ViewState["BM_ZONGXU"].ToString();
                    newRow[4] = ViewState["BM_CHANAME"].ToString();
                    newRow[5] = ViewState["BM_NOTE"].ToString();
                    newRow[6] = ViewState["BM_MALENGTH"].ToString();
                    newRow[7] = ViewState["BM_MAWIDTH"].ToString();
                    newRow[8] = (Convert.ToDouble(ViewState["BM_NUMBER"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000");
                    newRow[9] = (flag == "0" ? "0" : ViewState["BM_UNITWGHT"].ToString());
                    newRow[10] = (flag == "0" ? "0" : (Convert.ToDouble(ViewState["BM_TOTALWGHT"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000")); ;
                    newRow[11] = (Convert.ToDouble(ViewState["BM_MATOTALLGTH"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.00");
                    newRow[12] = ViewState["BM_MABGZMY"].ToString();
                    newRow[13] = ViewState["BM_GUIGE"].ToString();
                    newRow[14] = (Convert.ToDouble(ViewState["BM_SINGNUMBER"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000");
                    newRow[15] = (Convert.ToDouble(ViewState["BM_PNUMBER"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000");
                    newRow[16] = (flag == "0" ? "0" : ViewState["BM_MAUNITWGHT"].ToString());
                    newRow[17] = (flag == "0" ? "0" : (Convert.ToDouble(ViewState["BM_MATOTALWGHT"].ToString()) / Convert.ToInt16(txtSplitNums.Text.Trim())).ToString("0.000")); ;
                    newRow[18] = "";
                    newRow[19] = "N";
                    newRow[20] = ViewState["BM_MPMY"].ToString();//计划面域
                    newRow[21] = "";
                    dt.Rows.Add(newRow);
                }
            }

            dt.AcceptChanges();
            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (flag == "0")//部件;对于变更记录拆分，还需要修改变更表中的序号，这里设定对于部件，原序号不可更改
            {
                ((TextBox)GridView1.Rows[0].Cells[0].FindControl("txtXuHao")).Enabled = false;
                for (int j = 0; j < GridView1.Rows.Count; j++)
                {
                    ((HtmlInputText)GridView1.Rows[j].FindControl("dzh")).Disabled = true;
                    ((HtmlInputText)GridView1.Rows[j].FindControl("zongzhong")).Disabled = true;
                    ((HtmlInputText)GridView1.Rows[j].FindControl("cailiaodzh")).Disabled = true;
                    ((HtmlInputText)GridView1.Rows[j].FindControl("cailiaozongzhong")).Disabled = true;
                }
            }
        }
        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_OnClick(object sender, EventArgs e)
        {
            /*************
             * 拆分后信息
             ***********/
            //实际总重
            double sjzz=0;
            //材料总重
            double clzz = 0;
            //材料总长
            double clzc = 0;
            //面域
            double my = 0;
            //计划面域
            double mpmy = 0;
            //数量
            double shuliang=0;
            //计划数量
            double pshuliang = 0;

            /**************
             * 拆分前信息
             ************/
            double real_shuliang = Convert.ToDouble(ViewState["BM_NUMBER"].ToString());
            double real_pshuliang = Convert.ToDouble(ViewState["BM_PNUMBER"].ToString());
            double real_sjzz = Convert.ToDouble(ViewState["BM_TOTALWGHT"].ToString());
            double real_clzz = Convert.ToDouble(ViewState["BM_MATOTALWGHT"].ToString());
            
            
            if (ViewState["MarFlag"].ToString() == "0")
            {
                real_sjzz = 0;
                real_clzz = 0;
            }

            double real_clzc = Convert.ToDouble(ViewState["BM_MATOTALLGTH"].ToString());
            double real_my = Convert.ToDouble(ViewState["BM_MABGZMY"].ToString()) * Convert.ToDouble(ViewState["BM_PNUMBER"]);
            double real_mpmy = Convert.ToDouble(ViewState["BM_MPMY"].ToString()) * Convert.ToDouble(ViewState["BM_PNUMBER"]);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                double _shuliang = Convert.ToDouble(((HtmlInputText)GridView1.Rows[i].FindControl("shuliang")).Value);
                double _pshuliang = Convert.ToDouble(((HtmlInputText)GridView1.Rows[i].FindControl("p_shuliang")).Value);
                double _sjzz = Convert.ToDouble(((HtmlInputText)GridView1.Rows[i].FindControl("zongzhong")).Value);
                double _clzz = Convert.ToDouble(((HtmlInputText)GridView1.Rows[i].FindControl("cailiaozongzhong")).Value);
                double _clzc = Convert.ToDouble(((HtmlInputText)GridView1.Rows[i].FindControl("cailiaozongchang")).Value);
                double _my = Convert.ToDouble(((HtmlInputText)GridView1.Rows[i].FindControl("bgzmy")).Value);
                double _mpmy = Convert.ToDouble(((HtmlInputText)GridView1.Rows[i].FindControl("mpmy")).Value);

                shuliang += _shuliang;
                pshuliang += _pshuliang;
                sjzz += _sjzz;
                clzz += _clzz;
                clzc += _clzc;
                my += _my * _pshuliang;
                mpmy += _mpmy * _pshuliang;
            }

            double D_value_sjzz = sjzz - real_sjzz;//0.5 实际总重误差值
            double D_value_clzz = clzz - real_clzz;//0.5 材料总重误差值
            double D_value_clzc = clzc - real_clzc;//10  材料总长
            double D_value_my = my - real_my;//0.1  面域
            double D_value_mpmy = mpmy - real_mpmy;//0.1 计划面域
            double D_value_shuliang = shuliang - real_shuliang;//数量
            double D_value_pshuliang = pshuliang - real_pshuliang;//计划数量

            string tooltip = "误差值(拆分前-拆分后):实际总重(≤0.5kg):" + D_value_sjzz.ToString("0.00") + "kg;材料总重(≤0.5kg):" + D_value_clzz.ToString("0.00") + "kg;材料总长(≤10mm):" + D_value_clzc.ToString("0.1") + "mm;面域(≤0.1㎡):" + D_value_my.ToString("0.01") + "㎡;计划面域(≤0.1㎡):" + D_value_mpmy.ToString("0.01") + "㎡";

            if (Math.Abs(D_value_sjzz) > 0.5 || Math.Abs(D_value_clzz) > 0.5 || Math.Abs(D_value_clzc) > 10 || Math.Abs(D_value_my) > 0.1 || Math.Abs(D_value_mpmy) > 0.1)
            {
                ViewState["SaveFlag"] = "False";
                Label2.Text = "无法保存！！！" ;
                btnSave.Visible = false;
            }
            else
            {
                ViewState["SaveFlag"] = "True";
                Label2.Text = "可以保存！！！  ";
                btnSave.Visible = true;
            }

            if (D_value_shuliang != 0)
            {
                Label2.Text += "(数量不等拆分)   ";
            }
            else
            {
                Label2.Text+="(数量相等拆分)   ";
            }

            Label2.Text += tooltip;
            Label2.Visible = true;
            
        }
        /// <summary>
        /// 是否提计划，总项与分项是否提计划调换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnWMarPlan_OnClick(object sender, EventArgs e)
        {
            if(ddl_OWMarPlan.Enabled==false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('原计划已提交，无法互换！！！');", true);
                return;
            }

            if (ddl_OWMarPlan.SelectedValue == "N")
            {
                ddl_OWMarPlan.SelectedValue = "Y";
                foreach (GridViewRow grow in GridView1.Rows)
                {
                    ((DropDownList)grow.FindControl("ddlWMarPlan")).SelectedValue = "N";
                }
            }
            else if (ddl_OWMarPlan.SelectedValue == "Y")
            {
                ddl_OWMarPlan.SelectedValue = "N";
                foreach (GridViewRow grow in GridView1.Rows)
                {
                    ((DropDownList)grow.FindControl("ddlWMarPlan")).SelectedValue = "Y";
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            this.btnCheck_OnClick(null, null);
            if (GridView1.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:没有数据，无法保存!');", true);
                return;
            }

            List<string> list_sql = new List<string>();
            if (ViewState["SaveFlag"].ToString() == "True")
            {
                string ret = this.CheckMarNotBelongToMar();
                if (ret == "0")
                {
                    string _xuhao;
                    string _msxuhao;
                    string _tuhao;
                    string _marid;
                    string _zongxu;
                    string _ch_name;
                    string _beizhu;
                    string _cailiaocd;
                    string _cailiaokd;
                    string _shuliang;
                    string _singshuliang;
                    string _pshuliang;
                    string _dzh;
                    string _zongzhong;
                    string _cailiaozongchang;
                    string _bgzmy;
                    string _mpmy;
                    string _guige;
                    string _cailiaodzh;
                    string _cailiaozongzhong;
                    string _ku;
                    string _wmarplan;
                    string sqltext;

                    //是否提交材料计划状态
                    sqltext = "update " + ViewState["tablename"].ToString() + " set BM_WMARPLAN='" + ddl_OWMarPlan.SelectedValue + "'" +
                        "  WHERE BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_XUHAO='" + ViewState["Xuhao"].ToString() + "'";
                    list_sql.Add(sqltext);

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        _xuhao = ((TextBox)GridView1.Rows[i].FindControl("txtXuHao")).Text.Trim();
                        _msxuhao = ((TextBox)GridView1.Rows[i].FindControl("txtMsXuHao")).Text.Trim();
                        _tuhao = ((HtmlInputText)GridView1.Rows[i].FindControl("tuhao")).Value.Trim();
                        _marid = ((TextBox)GridView1.Rows[i].FindControl("marid")).Text.Trim();
                        _zongxu = ((HtmlInputText)GridView1.Rows[i].FindControl("zongxu")).Value.Trim();
                        _ch_name = ((HtmlInputText)GridView1.Rows[i].FindControl("ch_name")).Value.Trim();
                        _beizhu = ((HtmlInputText)GridView1.Rows[i].FindControl("beizhu")).Value.Trim();
                        _cailiaocd = ((HtmlInputText)GridView1.Rows[i].FindControl("cailiaocd")).Value.Trim();
                        _cailiaokd = ((HtmlInputText)GridView1.Rows[i].FindControl("cailiaokd")).Value.Trim();
                        _shuliang = ((HtmlInputText)GridView1.Rows[i].FindControl("shuliang")).Value.Trim();
                        _pshuliang = ((HtmlInputText)GridView1.Rows[i].FindControl("p_shuliang")).Value.Trim();
                        _singshuliang = ((HtmlInputText)GridView1.Rows[i].FindControl("sing_shuliang")).Value.Trim(); 
                        _dzh = ((HtmlInputText)GridView1.Rows[i].FindControl("dzh")).Value.Trim();
                        _zongzhong = ((HtmlInputText)GridView1.Rows[i].FindControl("zongzhong")).Value.Trim(); ;
                        _cailiaozongchang = ((HtmlInputText)GridView1.Rows[i].FindControl("cailiaozongchang")).Value.Trim();
                        _bgzmy = ((HtmlInputText)GridView1.Rows[i].FindControl("bgzmy")).Value.Trim();
                        _mpmy = ((HtmlInputText)GridView1.Rows[i].FindControl("mpmy")).Value.Trim();
                        _guige = ((HtmlInputText)GridView1.Rows[i].FindControl("guige")).Value.Trim();
                        _cailiaodzh = ((HtmlInputText)GridView1.Rows[i].FindControl("cailiaodzh")).Value.Trim();
                        _cailiaozongzhong = ((HtmlInputText)GridView1.Rows[i].FindControl("cailiaozongzhong")).Value.Trim();
                        _ku = ((HtmlInputText)GridView1.Rows[i].FindControl("ku")).Value.Trim();
                        _wmarplan = ((DropDownList)GridView1.Rows[i].FindControl("ddlWMarPlan")).SelectedValue;


                        if (_marid != "" && ddl_OWMarPlan.SelectedValue=="Y"&&_wmarplan=="Y"&&ckbCheckWmarPlan.Checked)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法保存!\\r\\r提示:原始拆分项提交计划，分项也提交计划，将导致计划重复');", true);
                            return;
                        }

                        if (_marid != "" && ddl_OWMarPlan.SelectedValue == "N" && _wmarplan == "N" && ckbCheckWmarPlan.Checked)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法保存!\\r\\r提示:原始拆分项不提交计划，分项也不提交计划，将导致计划少提');", true);
                            return;
                        }

                        if (ViewState["MarFlag"].ToString() == "0" && i == 0)
                        {
                            sqltext = "update " + ViewState["tablename"].ToString() + " set BM_XUHAO='" + _xuhao + "',BM_MSXUHAO='" + _msxuhao + "',BM_TUHAO='" + _tuhao + "',BM_CHANAME='" + _ch_name + "',BM_NOTE='" + _beizhu + "'" +
                                ",BM_MALENGTH=" + _cailiaocd + ",BM_MAWIDTH=" + _cailiaokd + ",BM_NUMBER=" + _shuliang + ",BM_SINGNUMBER=" + _singshuliang + ",BM_PNUMBER=" + _pshuliang + ",BM_UNITWGHT=" + _dzh + ", BM_TOTALWGHT=" + _zongzhong + ",BM_MAUNITWGHT=" + _cailiaodzh + ", BM_MATOTALWGHT=" + _cailiaozongzhong + ",BM_MATOTALLGTH=" + _cailiaozongchang + ",BM_MABGZMY=" + _bgzmy + ",BM_MPMY=" + _mpmy + ",BM_GUIGE='" + _guige + "'" +
                                "  WHERE BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_XUHAO='" + ViewState["Xuhao"].ToString() + "'";
                            list_sql.Add(sqltext);
                            sqltext = "";
                            sqltext = "update TBPM_TEMPMARDATA set  BM_UNITWGHT=0, BM_NUMBER="+_shuliang+", BM_MAWIDTH="+_cailiaokd+",BM_MALENGTH="+_cailiaocd+", BM_MATOTALLGTH="+_cailiaozongchang+" where BM_ENGID='" + ViewState["TaskID"].ToString() + "' and BM_XUHAO='" + ViewState["Xuhao"].ToString() + "'";
                            list_sql.Add(sqltext);
                        }
                        else
                        {
                            sqltext = "INSERT INTO " + ViewState["tablename"].ToString() + "(" +
                                " BM_XUHAO, BM_MSXUHAO, BM_TUHAO, BM_MARID, BM_ZONGXU, BM_CHANAME, BM_ENGSHNAME, BM_GUIGE, BM_PJID, BM_ENGID," +
                                "BM_CONDICTIONATR, BM_BEIZHUATR, BM_MALENGTH, BM_MAWIDTH, BM_MAUNITWGHT, BM_MATOTALWGHT, BM_MATOTALLGTH, " +
                                "BM_MABGZMY,BM_MPMY, BM_TUMAQLTY, BM_TUSTAD, BM_TUPROBLEM, BM_TUUNITWGHT, BM_TUTOTALWGHT, BM_SIGTUUNITWGHT, " +
                                "BM_SIGTTOTALWGHT, BM_CALUNITWGHT, BM_NUMBER,BM_SINGNUMBER,BM_PNUMBER,BM_UNITWGHT, BM_TOTALWGHT, BM_MASHAPE, BM_MASTATE, BM_ISMANU, " +
                                "BM_PROCESS, BM_FILLMAN, BM_FILLDATE, BM_FIXEDSIZE, BM_WMARPLAN, BM_MPSTATE, BM_MPSTATUS, BM_MPREVIEW, BM_MSSTATE, " +
                                "BM_MSSTATUS, BM_MSTEMP, BM_MSREVIEW, BM_OSSTATE, BM_OSSTATUS, BM_STATUS, BM_OSREVIEW, BM_KEYCOMS, BM_CALSTATUS," +
                                "BM_NOTE, BM_KU, BM_LABOUR) " +
                                " SELECT " +
                                 " '"+_xuhao+"', '"+_msxuhao+"', '" + _tuhao + "', BM_MARID, BM_ZONGXU, '" + _ch_name + "', BM_ENGSHNAME, '" + _guige + "', BM_PJID, BM_ENGID," +
                                "BM_CONDICTIONATR, BM_BEIZHUATR, " + _cailiaocd + ", " + _cailiaokd + ", "+_cailiaodzh+", "+_cailiaozongzhong+", " + _cailiaozongchang + ", " +
                                "" + _bgzmy + ", " + _mpmy + ",BM_TUMAQLTY, BM_TUSTAD, BM_TUPROBLEM, BM_TUUNITWGHT, BM_TUTOTALWGHT, 0, " +
                                "0, BM_CALUNITWGHT, " + _shuliang + ","+_singshuliang+","+_pshuliang+", " + _dzh + ", " + _zongzhong + ", BM_MASHAPE, BM_MASTATE, BM_ISMANU, " +
                                "BM_PROCESS, BM_FILLMAN, BM_FILLDATE, BM_FIXEDSIZE, '"+_wmarplan+"', '0', '0', '0', '0', " +
                                "'0', '0', '0', '0', '0', '0', '0', BM_KEYCOMS, '0'," +
                                "'" + _beizhu + "', BM_KU, BM_LABOUR " +
                                "FROM " + ViewState["tablename"].ToString() + " WHERE BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_XUHAO='" + ViewState["Xuhao"].ToString()+ "'";
                            list_sql.Add(sqltext);
                            sqltext = "INSERT INTO TBPM_TEMPMARDATA(" +
                                    "BM_XUHAO, BM_ZONGXU, BM_MARID, BM_ENGID, BM_MAUNITWGHT, BM_UNITWGHT, BM_TUUNITWGHT, BM_NUMBER, BM_MAWIDTH," +
                                    "BM_MALENGTH, BM_MATOTALLGTH)" +
                                    " Values('" + _xuhao + "','" + _zongxu + "','" + _marid + "','" + ViewState["TaskID"].ToString()+ "',"+_cailiaodzh+","+_dzh+",0,"+_shuliang+","+_cailiaocd+","+_cailiaokd+","+_cailiaozongchang+")";
                            list_sql.Add(sqltext);
                        }
                    }
                    DBCallCommon.ExecuteTrans(list_sql);
                    this.btnClear_OnClick(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已保存！！！');okay();window.close();", true);
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"与在数据库中父序为底层材料！！！');", true);
                }
                else if (ret.Contains("DataBaseRepeat"))//页面总序与数据库中重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"在数据库中已存在！！！');", true);
                }
                else if (ret.Contains("PageRepeat"))//页面总序重复
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"重复！！！');", true);
                }
                else if (ret.Contains("XuHaoExisted"))//输入部件时要检查序号是否存在
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"为部件,该部件序号在数据库中已存在！！！');", true);
                }
                else if (ret.Contains("FormError-"))//总序格式错误
                {
                    string[] aa = ret.Split('-');
                    string firstchar = "1"; /////aa[aa.Length - 2].ToString();
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"格式错误！！！\\r\\r正确格式：\"" + firstchar + "\"或\"" + firstchar + ".m...（m为正整数）');", true);
                }
                else if (ret == "EmptyXuhao")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!序号不能为空！！！');", true);
                }
                else if (ret.Contains("BelongToChgDelete-"))
                {
                    string[] aa = ret.Split('-');
                    string outxuhao = aa[aa.Length - 1].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"归属到变更删除记录上！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:【实际总重】、【材料总重】、【材料总长】、【面域】或【计划面域】不正确,无法保存！！！');", true);
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ddlSplitNums.Enabled = true;

            NoDataPanel.Visible = true;
            NoDataPanel2.Visible = true;
            grvOrgData.DataSource = null;
            grvOrgData.DataBind();
            Label1.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();

            Label2.Text = "";
            btnCheck.Visible = false;
            btnSave.Visible = false;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private string CheckMarNotBelongToMar()
        {
            string sql_delete = "delete from TBPM_TEMPORGDATA where BM_ENGID='" + ViewState["TaskID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql_delete);//删除表TBPM_TEMPORGDATA中该生产制号下数据，防止意外情况未清空上次记录

            string[] a = ViewState["TaskID"].ToString().Split('-');
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "\\.0\\.([1-9]{1}[0-9]{0,}){1}" + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";
            string pattern2 = @"^" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}$";
            Regex rgx = new Regex(pattern);
            Regex rgx2 = new Regex(pattern2);
            string ret = "0";
            List<string> list_sql = new List<string>();
            //检验序号格式
            int startindex;
            if (ViewState["MarFlag"].ToString() == "1")
            {
                startindex = 0;
            }
            else
            {
                startindex = 1;
            }

            for (int i = startindex; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                string xuhao = ((TextBox)gRow.FindControl("txtXuHao")).Text.Trim();
                if (xuhao != "")
                {
                    string mar = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                    if (!rgx.IsMatch(xuhao))
                    {
                        ret = "FormError-" + firstCharofZX + "-" + xuhao;
                        return ret;
                    }
                    else
                    {
                        list_sql.Add("insert into TBPM_TEMPORGDATA(BM_ZONGXU,BM_MARID,BM_ENGID) Values('" + xuhao + "','" + mar + "','" + ViewState["TaskID"].ToString() + "')");
                    }
                }
                else
                {
                    return "EmptyXuhao";
                }
            }
            //检验总序输入临时表
            DBCallCommon.ExecuteTrans(list_sql);
            //检验归属关系
            ParamsCheckMarNotBelongToMar pcmar = new ParamsCheckMarNotBelongToMar();
            pcmar.StrTabeleName = ViewState["tablename"].ToString();
            pcmar.TaskID = ViewState["TaskID"].ToString();
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
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_CheckMarNotBelongToMarByXuhaoOrgInput]");
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
        private void GetListName()
        {
            #region
            switch (ViewState["view_table"].ToString())
            {
                case "View_TM_HZY":
                   ViewState["tablename"] = "TBPM_STRINFOHZY";
                    break;
                case "View_TM_QLM":
                    ViewState["tablename"] = "TBPM_STRINFOQLM";
                    break;
                case "View_TM_BLJ":
                    ViewState["tablename"] = "TBPM_STRINFOBLJ";
                    break;
                case "View_TM_DQLJ":
                    ViewState["tablename"] = "TBPM_STRINFODQLJ";
                    break;
                case "View_TM_GFB":
                    ViewState["tablename"] = "TBPM_STRINFOGFB";
                    break;
                case "View_TM_DQO":
                    ViewState["tablename"] = "TBPM_STRINFODQO";
                    break;
                default: break;
            }
            #endregion
        }

    }
}
