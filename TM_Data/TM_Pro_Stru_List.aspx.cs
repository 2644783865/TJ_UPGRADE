﻿using System;
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
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;


namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Pro_Stru_List : System.Web.UI.Page
    {
        #region
        string tsa_id;
        string sqlText;
        string tablename = "TBPM_STRINFODQO";
        string viewtablename = "View_TM_DQO";

        string tuhao;
        string marid;
        string zongxu;
        string ch_name;
        string en_name;
        string guige;

        string cailiaoguige;
        float cailiaocd;
        float cailiaokd;
        float lilunzhl;
        float cailiaodzh;
        float cailiaozongzhong;
        float cailiaozongchang;
        float bgzmy;
        float mpmy;
        string caizhi;
        float signshuliang;
        float shuliang;
        float p_shuliang;
        float dzh;
        float zongzhong;
        string labunit;
        string cailiaoType;
        string xialiao;
        string biaozhun;
        string process;
        string beizhu;
        string ddlKeyComponents;
        string ddlFixedSize;
        string ddlwmp;//是否提材料计划
        float tudz;//图纸上单重
        float tuzongzhong;//图纸上总重
        string yongliang;
        string note;
        string ku;
        string techunit;

        int count = 0;
        ArrayList Arraylist_samezx = new ArrayList();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            tsa_id = Request.QueryString["tsa_id"];
            if (!IsPostBack)
            {
                InitInfo();
                InitGridview();
                TM_BasicFun.BindCklShowHiddenItems("Input", cklHiddenShow);
                this.Form.DefaultButton = "";
                // this.btnHidden_OnClick(null, null);
                //this.btnHiddenPaper_OnClick(null, null);
                //hkoutside.NavigateUrl = "TM_OutSide_Add.aspx?tsa_id=" + tsa_id;
                hpView.NavigateUrl = "TM_Task_View.aspx?time=" + DateTime.Now.ToString() + "&action=" + tsaid.Text;

                HyperLink3.NavigateUrl = "TM_OrgDataImportAll.aspx?TaskID=" + tsaid.Text;


                //hylKUCheck.NavigateUrl = "TM_WeightKuCheck.aspx?TaskID=" + tsaid.Text.Trim();


            }

            //this.CheckZeroXuHaoExist();
            this.OrgInputNum();

            if (Session["UserID"] == null || Session["UserID"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('用户信息丢失，导致您无法保存数据！！！\\r\\r提示:如果您有未保存的数据，请不要关闭当前页面，重新打开登录界面登录后即可保存');", true);/////window.location.reload();
                return;
            }
        }
        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitInfo()
        {
            sqlText = "select TSA_PJID,TSA_ENGNAME,CM_PROJ,TSA_NUMBER ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;
                lab_contract.Text = dr[0].ToString();

                labprostru.Text = dr[1].ToString() + "设计BOM表";
                lblNumber.Text = dr[3].ToString();
                lab_proname.Text = dr[2].ToString();
            }
            dr.Close();
            //this.BindddlPartName();

        }
        /// <summary>
        /// 已输入条数
        /// </summary>
        private void OrgInputNum()
        {
            string sql = "select (count(*)) as NUM from " + tablename + " where BM_ENGID='" + tsaid.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            dr.Read();
            if (Convert.ToInt32(dr["NUM"].ToString()) > 0)
            {
                btnOrgInputed.Value = "已输入(" + dr["NUM"].ToString() + ")条";
            }
            else
            {
                btnOrgInputed.Value = "已输入(0)条";
            }
            dr.Close();
        }
        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_org_OnClick(object sender, EventArgs e)
        {
            CheckBoxSelectDefine(GridView1, "CheckBox1");
        }

        /// <summary>
        /// CheckBox连选(此函数勿动)
        /// </summary>
        /// <param name="smartgridview"></param>
        /// <param name="ckbname"></param>
        public void CheckBoxSelectDefine(YYControls.SmartGridView smartgridview, string ckbname)
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


        /// <summary>
        /// 初始化Gridview
        /// </summary>
        private void InitGridview()
        {
            System.Data.DataTable dt = this.GetDataFromGrid();
            GridView1.DataSource = dt;
            GridView1.DataBind();
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
            dt.Columns.Add("BM_MAGUIGE");//规格4
            dt.Columns.Add("BM_MAQUALITY");//材质5
            dt.Columns.Add("BM_MALENGTH");//材料长度6
            dt.Columns.Add("BM_MAWIDTH");//材料宽度7
            dt.Columns.Add("BM_NOTE");//备注8
            dt.Columns.Add("BM_SINGNUMBER");//数量9
            dt.Columns.Add("BM_NUMBER");//总数量10
            dt.Columns.Add("BM_PNUMBER");//计划量11
            dt.Columns.Add("BM_TUUNITWGHT");//图纸上单重12
            dt.Columns.Add("BM_TUUNITTOTALWGHT");//图纸总重13            
            dt.Columns.Add("BM_MAUNITWGHT");//材料单重14
            dt.Columns.Add("BM_MATOTALWGHT");//材料总重15
            dt.Columns.Add("BM_TECHUNIT");//单位16
            dt.Columns.Add("BM_YONGLIANG");//材料用量17            
            dt.Columns.Add("BM_MABGZMY");//面域(m2)18
            dt.Columns.Add("BM_MPMY");//计划面域19
            dt.Columns.Add("BM_MATOTALLGTH");//材料总长20
            dt.Columns.Add("BM_MASHAPE");//材料种类21
            dt.Columns.Add("BM_XIALIAO");//下料22
            dt.Columns.Add("BM_PROCESS");//工艺流程23          
            dt.Columns.Add("BM_ALLBEIZHU");//总备注24
            dt.Columns.Add("BM_THRYWGHT");//理论重量25        
            dt.Columns.Add("BM_STANDARD");//国标26 
            dt.Columns.Add("BM_CAIGOUUNIT");//国标27 
            dt.Columns.Add("BM_ISMANU");//制作明细28
            dt.Columns.Add("BM_FIXEDSIZE");//是否定尺29
            dt.Columns.Add("BM_WMARPLAN");//是否提材料计划30
            dt.Columns.Add("BM_KU");//入库
            #endregion
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HtmlInputText)gRow.FindControl("tuhao")).Value.Trim();
                newRow[1] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                newRow[2] = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                newRow[3] = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                newRow[4] = ((HtmlInputText)gRow.FindControl("cailiaoguige")).Value.Trim();
                newRow[5] = ((HtmlInputText)gRow.FindControl("caizhi")).Value.Trim();
                newRow[6] = ((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim();
                newRow[7] = ((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim();
                newRow[8] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("note")).Text.Trim();
                newRow[9] = ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim();
                newRow[10] = ((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim();
                newRow[11] = ((HtmlInputText)gRow.FindControl("plan_shuliang")).Value.Trim();

                newRow[12] = ((HtmlInputText)gRow.FindControl("tudz")).Value.Trim();

                newRow[13] = ((HtmlInputText)gRow.FindControl("tuzhiZZ")).Value.Trim();
                newRow[14] = ((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim();
                newRow[15] = ((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim();
                newRow[16] = ((HtmlInputText)gRow.FindControl("labunit")).Value.Trim();
                newRow[17] = ((HtmlInputText)gRow.FindControl("txtYongliang")).Value.Trim();
                newRow[18] = ((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim();
                newRow[19] = ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim();
                newRow[20] = ((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim();
                newRow[21] = ((HtmlInputText)gRow.FindControl("cailiaoType")).Value.Trim();
                newRow[22] = ((HtmlInputText)gRow.FindControl("xialiao")).Value.Trim();
                newRow[23] = ((HtmlInputText)gRow.FindControl("process")).Value.Trim();
                newRow[24] = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("zongbeizhu")).Text.Trim();
                newRow[25] = ((HtmlInputText)gRow.FindControl("lilunzhl")).Value.Trim();
                newRow[26] = ((HtmlInputText)gRow.FindControl("biaozhun")).Value.Trim();
                newRow[27] = ((HtmlInputText)gRow.FindControl("caigoudanwei")).Value.Trim();
                newRow[28] = ((DropDownList)gRow.FindControl("ddlKeyComponents")).SelectedValue;
                newRow[29] = ((DropDownList)gRow.FindControl("ddlFixedSize")).SelectedValue;
                newRow[30] = ((DropDownList)gRow.FindControl("ddlWmp")).SelectedValue;
                newRow[31] = ((HtmlInputText)gRow.FindControl("ku")).Value.Trim();
                dt.Rows.Add(newRow);
            }
            for (int i = GridView1.Rows.Count; i < 15; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[28] = "Y";
                newRow[29] = "N";
                newRow[30] = "Y";
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        /// <summary>
        /// 原始数据中的插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btninsert_Click(object sender, EventArgs e)
        {
            // if (istid.Value == "1")//相当于确定
            // {
            System.Data.DataTable dt = this.GetDataFromGrid();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gRow.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[28] = "Y";
                    newRow[29] = "N";
                    newRow[30] = "Y";
                    dt.Rows.InsertAt(newRow, i + 1 + count);
                    ///////////dt.Rows.RemoveAt(dt.Rows.Count-1);
                    count++;
                }
            }
            // istid.Value = "0";
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            ////InitGridview();
            //    }
        }
        /// <summary>
        /// 原始数据中的删除,不对数据库操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndelete_Click(object sender, EventArgs e)
        {
            //***********删除数据不对数据库操作************
            //  if (txtid.Value != "0")
            //  {
            System.Data.DataTable dt = this.GetDataFromGrid();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gRow.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            ////////InitGridview();
            //   }
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
                foreach (GridViewRow grow in GridView1.Rows)
                {
                    int cellnum = Convert.ToInt16(num_id[0]);
                    ((HtmlInputText)grow.Cells[cellnum].FindControl(num_id[1])).Value = "";
                }
            }
        }


        /// <summary>
        /// 原始数据中的保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            string sqladdvalidzongxu = "select BM_ENGID,BM_ZONGXU from TBPM_STRINFODQO where BM_ENGID='" + tsaid.Text.ToString().Trim() + "'";
            System.Data.DataTable dtaddvalidzongxu = DBCallCommon.GetDTUsingSqlText(sqladdvalidzongxu);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                string zx = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                DataView dvaddvalidzongxu = new DataView(dtaddvalidzongxu);
                dvaddvalidzongxu.RowFilter = "BM_ZONGXU='" + zx.Trim() + "'";
                System.Data.DataTable newdtaddvalidzongxu = dvaddvalidzongxu.ToTable();
                if (newdtaddvalidzongxu.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('任务号" + tsaid.Text.ToString().Trim() + "总序" + zx.Trim() + "已存在，不能重复输入');", true);
                    return;
                }
            }
            if (hidSave.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勿重复插入数据');", true);
                return;
            }
            string ret = this.CheckMarNotBelongToMar();
            if (ret == "0")//检查无误
            {
                hidSave.Value = "0";
                btnsave.Visible = false;
                List<string> list_sql = new List<string>();

                int insertcount = 0;//插入行数
                #region
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gRow.FindControl("CheckBox1");
                    zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                    #region 获取数据
                    if (zongxu != "")
                    {
                        tuhao = ((HtmlInputText)gRow.FindControl("tuhao")).Value.Trim();
                        marid = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                        ch_name = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                        cailiaoguige = ((HtmlInputText)gRow.FindControl("cailiaoguige")).Value.Trim();
                        caizhi = ((HtmlInputText)gRow.FindControl("caizhi")).Value.Trim();
                        cailiaocd = float.Parse(((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim());
                        cailiaokd = float.Parse(((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim());
                        note = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("note")).Text.Trim();
                        //单台数量
                        signshuliang = float.Parse(((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim() == "" ? "1" : ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim());
                        //总数量
                        shuliang = float.Parse(((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim() == "" ? Convert.ToString(signshuliang * Convert.ToInt32(lblNumber.Text.Trim())) : ((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim());
                        p_shuliang = float.Parse(((HtmlInputText)gRow.FindControl("plan_shuliang")).Value.Trim() == "" ? shuliang.ToString() : ((HtmlInputText)gRow.FindControl("plan_shuliang")).Value.Trim());

                        tudz = float.Parse(((HtmlInputText)gRow.FindControl("tudz")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("tudz")).Value.Trim());

                        tuzongzhong = float.Parse(((HtmlInputText)gRow.FindControl("tuzhiZZ")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("tuzhiZZ")).Value.Trim());
                        //材料单重
                        cailiaodzh = float.Parse(((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim());
                        //材料总重
                        cailiaozongzhong = float.Parse(((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim());
                        cailiaoType = ((HtmlInputText)gRow.FindControl("cailiaoType")).Value.Trim();
                        bgzmy = float.Parse(((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim());
                        mpmy = float.Parse(((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim());
                        techunit = ((HtmlInputText)gRow.FindControl("labunit")).Value.Trim();
                        yongliang = ((HtmlInputText)gRow.FindControl("txtYongliang")).Value.Trim();
                        xialiao = ((HtmlInputText)gRow.FindControl("xialiao")).Value.Trim();
                        process = ((HtmlInputText)gRow.FindControl("process")).Value.Trim();


                        beizhu = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("zongbeizhu")).Text.Trim();
                        lilunzhl = float.Parse(((HtmlInputText)gRow.FindControl("lilunzhl")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("lilunzhl")).Value.Trim());

                        cailiaozongchang = float.Parse(((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim());
                        biaozhun = ((HtmlInputText)gRow.FindControl("biaozhun")).Value.Trim();


                        ddlKeyComponents = ((DropDownList)gRow.FindControl("ddlKeyComponents")).SelectedValue;
                        ddlFixedSize = ((DropDownList)gRow.FindControl("ddlFixedSize")).SelectedValue;
                        ddlwmp = ((DropDownList)gRow.FindControl("ddlWmp")).SelectedValue;
                        ku = ((HtmlInputText)gRow.FindControl("ku")).Value.Trim();




                        Insertbind(list_sql);

                        insertcount++;
                    }
                    #endregion
                }
                #endregion
                DBCallCommon.ExecuteTrans(list_sql);
                btnsave.Visible = true;
                this.OrgInputNum();
                GridView1.DataSource = this.AfterSavedReload();
                GridView1.DataBind();
                hidSave.Value = "1";
                //   btnsave.Enabled = true;
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
            else if (ret.Contains("XuHaoExisted"))//输入部件和物料时要检查序号是否存在
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"对应序号在数据库中已存在！！！');", true);
            }
            else if (ret.Contains("SameZongxuWithoutMaird"))//输入部件和物料时要检查序号是否存在
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r部件总序\"" + outxuhao + "\"不能重复！！！');", true);
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
            else if (ret.Contains("SameZongxu-"))//总序相同的记录物料编码不同
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r总序\"" + outxuhao + "\"有多条记录，但其物料编码不同！！！');", true);
            }
            else if (ret.Contains("序号已存在-"))//填入了序号的记录
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r填入的【序号:\"" + outxuhao + "\"】在数据库中已存在！！！');", true);
            }
            else if (ret.Contains("序号与总序重复-"))//填入了序号的记录
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r填入的【序号:\"" + outxuhao + "\"】在页面上有相同总序记录！！！');", true);
            }
            else if (ret.Contains("页面上序号重复-"))//填入了序号的记录
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r填入的【总号:\"" + outxuhao + "\"】在页面上有多条！！！');", true);
            }
            else if (ret.Contains("NameEmptyError-"))
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r填入的【总号:\"" + outxuhao + "\"】的中文名称不能为空');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r未知错误，请与管理员联系！！！');", true);
            }
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
            dt.Columns.Add("BM_MAGUIGE");//规格4
            dt.Columns.Add("BM_MAQUALITY");//材质5
            dt.Columns.Add("BM_MALENGTH");//材料长度6
            dt.Columns.Add("BM_MAWIDTH");//材料宽度7
            dt.Columns.Add("BM_NOTE");//备注8
            dt.Columns.Add("BM_SINGNUMBER");//数量9
            dt.Columns.Add("BM_NUMBER");//总数量10
            dt.Columns.Add("BM_PNUMBER");//计划量11
            dt.Columns.Add("BM_TUUNITWGHT");//图纸上单重12
            dt.Columns.Add("BM_TUUNITTOTALWGHT");//图纸总重13            
            dt.Columns.Add("BM_MAUNITWGHT");//材料单重14
            dt.Columns.Add("BM_MATOTALWGHT");//材料总重15
            dt.Columns.Add("BM_TECHUNIT");//单位16
            dt.Columns.Add("BM_YONGLIANG");//材料用量17            
            dt.Columns.Add("BM_MABGZMY");//面域(m2)18
            dt.Columns.Add("BM_MPMY");//计划面域19
            dt.Columns.Add("BM_MATOTALLGTH");//材料总长20
            dt.Columns.Add("BM_MASHAPE");//材料种类21
            dt.Columns.Add("BM_XIALIAO");//下料22
            dt.Columns.Add("BM_PROCESS");//工艺流程23          
            dt.Columns.Add("BM_ALLBEIZHU");//总备注24
            dt.Columns.Add("BM_THRYWGHT");//理论重量25        
            dt.Columns.Add("BM_STANDARD");//国标26 
            dt.Columns.Add("BM_CAIGOUUNIT");//国标27 
            dt.Columns.Add("BM_ISMANU");//制作明细28
            dt.Columns.Add("BM_FIXEDSIZE");//是否定尺29
            dt.Columns.Add("BM_WMARPLAN");//是否提材料计划30
            dt.Columns.Add("BM_KU");//下料22
            #endregion
            for (int i = 0; i < 15; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[28] = "Y";
                newRow[29] = "N";
                newRow[30] = "Y";
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;

        }

        /// <summary>
        /// 插入数据(更新)
        /// </summary>
        protected void Insertbind(List<string> list)
        {
            sqlText = "";
            sqlText = "insert into " + tablename + " ";
            sqlText += "(BM_XUHAO,BM_TUHAO,BM_MARID,BM_ZONGXU,BM_CHANAME,BM_GUIGE,BM_ENGSHNAME,BM_PJID,BM_ENGID,";//8
            sqlText += "BM_MALENGTH,BM_MAWIDTH,BM_NUMBER,BM_SINGNUMBER,BM_PNUMBER,BM_MAUNITWGHT,BM_MATOTALWGHT,";//5
            sqlText += "BM_MATOTALLGTH,BM_MABGZMY,BM_UNITWGHT,BM_TOTALWGHT,";//4
            sqlText += "BM_MASHAPE,BM_XIALIAO,BM_FILLMAN,";//3
            sqlText += "BM_PROCESS,BM_NOTE,BM_ISMANU,BM_FIXEDSIZE,";//4
            sqlText += "BM_TUUNITWGHT,BM_TUTOTALWGHT,BM_ALLBEIZHU,BM_WMARPLAN,BM_MPMY,";//8
            sqlText += "BM_OLDINDEX,BM_TECHUNIT,BM_YONGLIANG,BM_KU) values";



            sqlText += "('" + zongxu + "','" + tuhao + "','" + marid + "','" + zongxu + "','" + ch_name + "','" + guige + "','" + en_name + "','" + lab_contract.Text.Trim() + "','" + tsaid.Text + "',";
            sqlText += "'" + cailiaocd + "','" + cailiaokd + "','" + shuliang + "','" + signshuliang + "','" + p_shuliang + "','" + cailiaodzh + "','" + cailiaozongzhong + "',";
            sqlText += "'" + cailiaozongchang + "','" + bgzmy + "','" + dzh + "','" + zongzhong + "',";
            sqlText += "'" + cailiaoType + "','" + xialiao + "','" + Session["UserID"] + "',";
            sqlText += "'" + process + "','" + note + "','" + ddlKeyComponents + "','" + ddlFixedSize + "',";
            sqlText += "'" + tudz + "','" + tuzongzhong + "','" + beizhu + "','" + ddlwmp + "','" + mpmy + "',";
            sqlText += "'" + zongxu + "','" + techunit + "','" + yongliang + "','" + ku + "')";
            list.Add(sqlText);
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private string CheckMarNotBelongToMar()
        {
            string sql_delete = "delete from TBPM_TEMPORGDATA where BM_ENGID='" + tsaid.Text + "'";
            DBCallCommon.ExeSqlText(sql_delete);//删除表TBPM_TEMPORGDATA中该生产制号下数据，防止意外情况未清空上次记录


            ///////////////////string firstCharofZX=a[a.Length-1];
            ///////////////////string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
            string firstCharofZX = "1";
            string pattern = @"^[1-9]{1,2}(\(\d\))?((\.[1-9]{1}\d*)(\(\d\))?)*$";


            //  string pattern2 = @"^" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}$";
            Regex rgx = new Regex(pattern);
            //  Regex rgx2 = new Regex(pattern2);
            string ret = "0";
            List<string> list_sql = new List<string>();
            //对于重复总序及序号的检查
            ArrayList array_marid_zongxu_name = new ArrayList();
            ArrayList array_xuhao = new ArrayList();
            int index = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                string id = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                string zx = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                string mc = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();

                //  string txt_xuhao = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("txtXuhao")).Text.Trim();
                //if (txt_xuhao != "")
                //{
                //    array_xuhao.Add(txt_xuhao);
                //}


                if (zx != "")
                {
                    array_marid_zongxu_name.Add(id + "," + zx);
                    index++;
                }
            }

            if (index > 1)
            {
                for (int i = 0; i < array_marid_zongxu_name.Count - 1; i++)
                {
                    for (int j = i + 1; j < array_marid_zongxu_name.Count; j++)
                    {
                        string zongxu_i = array_marid_zongxu_name[i].ToString().Split(',')[1];
                        string zongxu_j = array_marid_zongxu_name[j].ToString().Split(',')[1];
                        if (zongxu_i == zongxu_j)
                        {
                            string zx_0 = zongxu_i + ",0";
                            if (Arraylist_samezx.Count == 0)
                            {
                                Arraylist_samezx.Add(zx_0);
                            }
                            else
                            {
                                for (int m = 0; m < Arraylist_samezx.Count; m++)
                                {
                                    if (Arraylist_samezx[m].ToString() == zx_0)
                                    {
                                        break;
                                    }
                                    else if (Arraylist_samezx[m].ToString() != zx_0 && m + 1 == Arraylist_samezx.Count)
                                    {
                                        Arraylist_samezx.Add(zx_0);
                                    }
                                }
                            }
                            if (array_marid_zongxu_name[i].ToString() != array_marid_zongxu_name[j].ToString())
                            {
                                string[] aa = array_marid_zongxu_name[i].ToString().Split(',');
                                ret = "SameZongxu-" + aa[1];
                                return ret;
                            }
                        }
                    }
                }
            }

            //if (Arraylist_samezx.Count > 0)
            //{
            //    this.CheckZeroXuHaoExist();
            //}


            //检验总序格式
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                ch_name = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                if (zongxu != "")
                {
                    string mar = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                    if (!rgx.IsMatch(zongxu))
                    {
                        ret = "FormError-" + firstCharofZX + "-" + zongxu;
                        return ret;
                    }
                    else if (ch_name == "")
                    {
                        ret = "NameEmptyError-" + firstCharofZX + "-" + zongxu;
                        return ret;
                    }
                    else
                    {
                        list_sql.Add("insert into TBPM_TEMPORGDATA(BM_ZONGXU,BM_MARID,BM_ENGID) Values('" + zongxu + "','" + mar + "','" + tsaid.Text.Trim() + "')");
                    }
                }
            }



            //检验总序输入临时表
            DBCallCommon.ExecuteTrans(list_sql);

            //部件重复检验
            sqlText = "select BM_ZONGXU from (select BM_ZONGXU,BM_MARID from TBPM_TEMPORGDATA where BM_ENGID='" + tsaid.Text.Trim() + "' union all select BM_ZONGXU,BM_MARID from TBPM_STRINFODQO where BM_ENGID='" + tsaid.Text.Trim() + "')a where BM_MARID='' group by BM_ZONGXU having count(*)>1";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt1.Rows.Count > 0)
            {
                ret = "SameZongxuWithoutMaird-" + dt1.Rows[0][0];
                return ret;
            }

            //检验归属关系
            ParamsCheckMarNotBelongToMar pcmar = new ParamsCheckMarNotBelongToMar();

            pcmar.StrTabeleName = tablename;
            pcmar.TaskID = tsaid.Text;
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
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "PRO_TM_CheckMarNotBelongToMarBOM");
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
            //SqlParameter[] pms = new SqlParameter[] { 
            //new SqlParameter("@StrTable",psv.StrTabeleName),
            //new SqlParameter("@ENG_ID",psv.TaskID)
            //}
            //return SqlHelper.ExecuteDataTable("PRO_TM_CheckMarNotBelongToMarBOM", CommandType.StoredProcedure, pms);
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

            TM_BasicFun.HiddenShowColumn(GridView1, cklHiddenShow, Index, "Input");
        }
    }
}

