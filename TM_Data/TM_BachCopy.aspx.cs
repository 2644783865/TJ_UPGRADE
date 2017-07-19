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
    public partial class TM_BachCopy : System.Web.UI.Page
    {
        #region
        string sqlText;
        string xuhao;//序号
        string tuhao;//图号
        string marid;//物料编码
        string zongxu;//总序
        string ch_name;//中文名称
        string en_name;//英文名称
        string guige;//规格
        string cailiaoname;//材料名称
        double cailiaocd;//材料长度
        double cailiaokd;//材料宽度
        double cailiaodzh;//材料单重
        double cailiaozongzhong;//材料总重
        double cailiaozongchang;//材料总长
        double bgzmy;//面域
        double shuliang;//数量
        double dzh;//单重
        double zongzhong;//总重
        string xinzhuang;//形状
        string zhuangtai;//状态
        string process;//工艺流程
        string beizhu;//备注
        string tixian;//体现制作明细
        string ddlKeyComponents;//关键部件
        string ddlFixedSize;//是否定尺
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitInfo();
            }
        }
        /// <summary>
        /// 初始化页面(基本信息)
        /// </summary>
        private void InitInfo()
        {
            string tsa_id = "";
            string[] fields;
            string sqlText = "";
            tsa_id = Request.QueryString["action"];
            fields = tsa_id.Split('-');
            sqlText = "select TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;

                proname.Text = dr[0].ToString();
                engname.Text = dr[1].ToString();
                eng_type.Value = dr[2].ToString();
                pro_id.Value = dr[3].ToString();
            }
            dr.Close();
            this.GetListName();
            string arry=Request.QueryString["arry"];
            if(arry!="undefined")
            {
                txtItem.Text = arry;
                this.ClickButton();
            }
        }
        /// <summary>
        /// 初始化表名
        /// </summary>
        private void GetListName()
        {
            #region
            switch (eng_type.Value)
            {
                case "回转窑":
                    ViewState["viewtable"] = "View_TM_HZY";
                    ViewState["tablename"] = "TBPM_STRINFOHZY";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFHZY";
                    break;
                case "球、立磨":
                    ViewState["viewtable"] = "View_TM_QLM";
                    ViewState["tablename"] = "TBPM_STRINFOQLM";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFQLM";
                    break;
                case "篦冷机":
                    ViewState["viewtable"] = "View_TM_BLJ";
                    ViewState["tablename"] = "TBPM_STRINFOBLJ";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFBLJ";
                    break;
                case "堆取料机":
                    ViewState["viewtable"] = "View_TM_DQLJ";
                    ViewState["tablename"] = "TBPM_STRINFODQLJ";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFDQJ";
                    break;
                case "钢结构及非标":
                    ViewState["viewtable"] = "View_TM_GFB";
                    ViewState["tablename"] = "TBPM_STRINFOGFB";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFGFB";
                    break;
                case "电气及其他":
                    ViewState["viewtable"] = "View_TM_DQO";
                    ViewState["tablename"] = "TBPM_STRINFODQO";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFDQO";
                    break;
                default: break;
            }
            #endregion
        }
        private void ClickButton()
        {
            ViewState["CopyXuHao"] = txtItem.Text.Trim();
            string sqltext = "select isnull(BM_MARID,'') as BM_MARID,BM_XUHAO,BM_TUHAO,BM_ZONGXU,BM_CHANAME,BM_NUMBER,BM_MAUNITWGHT,BM_MATOTALWGHT,BM_UNITWGHT,BM_TOTALWGHT,BM_GUIGE,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH,BM_MAQUALITY,BM_FIXEDSIZE from " + ViewState["viewtable"] + " where BM_ENGID='" + tsaid.Text + "' and BM_XUHAO='" + txtItem.Text.Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                grv.DataSource = dt;
                grv.DataBind();
                NoDataPanel.Visible = false;
                btnAdd.Visible = true;
                btnDelete.Visible = false;
                btnSave.Visible = false;

                if (dt.Rows[0]["BM_MARID"].ToString() == "")
                {
                    btnAdd.Visible = false;
                    btnDelete.Visible = false;
                    btnSave.Visible = false;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:您输入的是部件，无法进行批量复制！！！');", true);
                }
            }
            else
            {
                grv.DataSource = null;
                grv.DataBind();
                NoDataPanel.Visible = true;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnSave.Visible = false;

                txtZongXu.Text = "";
                txtNum.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            this.ClickButton();
        }
        /// <summary>
        /// 复制（数据绑定）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            btnDelete.Visible = false;
            btnSave.Visible = false;
            string ret = this.CheckZongXuProper();
            if (ret=="Yes")
            {
                int nums = Convert.ToInt16(txtNum.Text.Trim());
                string[] zongxuArray = this.ZongXuArray(txtZongXu.Text, nums);
                this.GetDataTable(nums, zongxuArray);
            }
            else if (ret.Contains("Existed"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法复制，总序已存在！！！');", true);
            }
            else if (ret.Contains("FatherError"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法复制，起始总序\"" + txtZongXu.Text.Trim() + "\"的父序不存在或为物料！！！');", true);
            }
            else if (ret.Contains("FormatError"))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法复制，请输入正确的总序格式！！！');", true);
            }
        }
        /// <summary>
        /// 定义绑定的DataTable
        /// </summary>
        /// <returns></returns>
        protected void GetDataTable(int tiaoshu,string[] array)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BM_TUHAO");//图号
            dt.Columns.Add("BM_ZONGXU");//总序
            dt.Columns.Add("BM_CHANAME");//中文名称
            dt.Columns.Add("BM_NUMBER");//数量
            dt.Columns.Add("BM_MAQUALITY");//材质
            dt.Columns.Add("BM_NOTE");//备注
            dt.Columns.Add("BM_FIXEDSIZE");//是否定尺
            for (int i = 0; i < tiaoshu; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = "";//图号
                newRow[1] = array[i];//总序
                newRow[2] = grv.Rows[0].Cells[5].Text.Trim();//中文名称
                newRow[3] = grv.Rows[0].Cells[9].Text.Trim();//数量
                newRow[4] = grv.Rows[0].Cells[16].Text.Trim() == "&nbsp;" ? "" : grv.Rows[0].Cells[16].Text.Trim();//材质
                newRow[5] = "";//备注
                newRow[6] = "N";//是否定尺
                dt.Rows.Add(newRow);
            }
            btnAdd.Visible = true;
            btnDelete.Visible = true;
            btnSave.Visible = true;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        /// <summary>
        /// 获取删除后的Table
        /// </summary>
        protected void GetDeleteTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BM_TUHAO");//图号
            dt.Columns.Add("BM_ZONGXU");//总序
            dt.Columns.Add("BM_CHANAME");//中文名称
            dt.Columns.Add("BM_NUMBER");//数量
            dt.Columns.Add("BM_MAQUALITY");//材质
            dt.Columns.Add("BM_NOTE");//备注
            dt.Columns.Add("BM_FIXEDSIZE");//是否定尺
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HtmlInputText)gRow.FindControl("tuhao")).Value.Trim();
                newRow[1] = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                newRow[2] = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                newRow[3] = ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim();
                newRow[4] = gRow.Cells[6].Text.Trim() == "&nbsp;" ? "" : gRow.Cells[0].Text.Trim();
                newRow[5] = ((HtmlInputText)gRow.FindControl("beizhu")).Value.Trim();
                newRow[6] = ((DropDownList)gRow.FindControl("ddlFixedSize")).SelectedValue.ToString();
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            //删除
            int startindexdelete=Convert.ToInt16(txtDeleteNums.Value)>0?Convert.ToInt16(txtDeleteNums.Value)-1:100;
            int deletedrows = 0;
            for (int j = startindexdelete; j < GridView1.Rows.Count; j++)
            {
                CheckBox cbx = (CheckBox)GridView1.Rows[j].FindControl("CheckBox1");
                if (cbx.Checked)
                {
                    dt.Rows.RemoveAt(j-deletedrows);
                    deletedrows++;
                }
            }
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                btnSave.Visible = true;
                btnDelete.Visible = true;
                btnAdd.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnSave.Visible = false;
                btnDelete.Visible = false;
                btnAdd.Visible = false;
            }
        }
        /// <summary>
        /// 判断输入的起始总序是否符合
        /// </summary>
        /// <returns></returns>
        protected string CheckZongXuProper()
        {
            string returnValue="Yes";

            string zx = txtZongXu.Text.Trim();
            string[] a = tsaid.Text.Split('-');
            //////////////////string firstCharofZX = a[a.Length - 1];
            ///////////////////////////string pattern = @"^(" + firstCharofZX + "\\.[1-9]{1}[0-9]*|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";            
            string pattern2 = @"^" + firstCharofZX + "\\.[1-9]{1}[0-9]*$";
            Regex rgx = new Regex(pattern);//物料和部件
            Regex rgx2 = new Regex(pattern2);//部件

            if (rgx.IsMatch(zx))//总序满足格式
            {
                //总序是否存在
                string sqltext = "select BM_ZONGXU from " + ViewState["viewtable"] + " where BM_ENGID like '" + a[0] + "-%' and BM_ZONGXU='" + txtZongXu.Text.Trim() + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                if (dr.HasRows)
                {
                    returnValue = "Existed";
                    return returnValue;
                }
                dr.Close();
                //总序存在父序且为物料
                string fzongxu = txtZongXu.Text.Trim().Substring(0, txtZongXu.Text.Trim().LastIndexOf('.'));
                sqltext = "select BM_ZONGXU from " + ViewState["viewtable"] + " where BM_ENGID like '" + a[0] + "-%' and BM_ZONGXU='" + fzongxu + "' and (BM_MARID='' or BM_MARID is null)";
                SqlDataReader dr_fzx = DBCallCommon.GetDRUsingSqlText(sqltext);
                if (!dr_fzx.HasRows)
                {
                    returnValue = "FatherError";
                    return returnValue;
                }
                dr.Close();
                return returnValue;
            }
            else
            {
                returnValue = "FormatError";
                return returnValue;
            }
        }
        /// <summary>
        /// 形成总序序列
        /// </summary>
        /// <returns></returns>
        protected string[] ZongXuArray(string zongxu,int tiaoshu)
        {
            string[] aa=zongxu.Split('.');
            string[] returnarray = new string[tiaoshu];
            int last = Convert.ToInt16(aa[aa.Length - 1]);
            string forheard = zongxu.Substring(0,zongxu.LastIndexOf('.')+1);
            for (int i = 0; i < tiaoshu; i++)
            {
                returnarray[i] = forheard + last.ToString();
                last++;
            }
            return returnarray;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            this.GetDeleteTable();
        }

        /// <summary>
        /// 原始数据中的保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string ret = this.CheckMarNotBelongToMar();
            if (ret == "0")//检查无误
            {
                //获取待复制记录的相关数据
                this.GetDataOfCopyItem();

                List<string> list_sql = new List<string>();
                int insertcount = 0;//插入行数
                #region
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    //总序
                    zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value;
                    #region 获取数据
                    if (zongxu != "")
                    {
                        //图号
                        tuhao = ((HtmlInputText)gRow.FindControl("tuhao")).Value;
                        xuhao = zongxu;
                        //中文名称
                        ch_name = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                        //数量
                        shuliang = Convert.ToDouble(((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim());
                        //材料总重
                        cailiaozongzhong = cailiaodzh*shuliang;
                        //材料总长
                        cailiaozongchang =Convert.ToDouble(ViewState["cailiaozongchang"].ToString()) * shuliang;
                        //面域
                        bgzmy =Convert.ToDouble(ViewState["bgzmy"].ToString()) * shuliang;
                        //总重
                        zongzhong = dzh * shuliang;
                        //备注
                        beizhu = ((HtmlInputText)gRow.FindControl("beizhu")).Value.Trim();
                        //是否定尺
                        ddlFixedSize = ((DropDownList)gRow.FindControl("ddlFixedSize")).SelectedValue;
                        Insertbind(list_sql);
                        insertcount++;
                    }
                    #endregion
                }
                #endregion
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:原始数据已保存!\\r\\r影响行数:" + insertcount + "');window.returnValue=\"Refesh\";window.close();", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"与数据库中其父序均为底层材料！！！');", true);
            }
            else if (ret.Contains("FormError-"))//总序格式错误
            {
                string[] aa = ret.Split('-');
                string firstchar = aa[aa.Length - 2].ToString();
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"格式错误！！！\\r\\r正确格式：\"" + firstchar + "\"或\"" + firstchar + ".m...\"（m为正整数）');", true);
            }
            else if (ret.Contains("PageRepeat"))
            {
                string[] aa = ret.Split('-');
                string firstchar = aa[aa.Length - 2].ToString();
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"重复！！！');", true);
            }
            else if (ret.Contains("DataBaseRepeat"))
            {
                string[] aa = ret.Split('-');
                string firstchar = aa[aa.Length - 2].ToString();
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"与数据库中重复！！！');", true);
            }
            else if (ret.Contains("XuHaoExisted"))//输入部件和物料时要检查序号是否存在
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"对应序号在数据库中已存在！！！');", true);
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
        /// 获取待复制记录数据
        /// </summary>
        private void GetDataOfCopyItem()
        {
            string sql_select_org = "select * from " + ViewState["viewtable"] + " where BM_ENGID='" + tsaid.Text.Trim() + "' and BM_XUHAO='" + ViewState["CopyXuHao"].ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_org);
            xuhao = dt.Rows[0]["BM_XUHAO"].ToString();//序号
            //tuhao = dt["BM_TUHAO"].ToString();//图号
            marid = dt.Rows[0]["BM_MARID"].ToString();//物料编码
            //zongxu = dt["BM_ZONGXU"].ToString();//总序
            //ch_name = dt["BM_CHANAME"].ToString();//中文名称
            en_name = dt.Rows[0]["BM_ENGSHNAME"].ToString();//英文名称
            guige = dt.Rows[0]["BM_GUIGE"].ToString();//规格
            cailiaocd = Convert.ToDouble(dt.Rows[0]["BM_MALENGTH"].ToString());//材料长度
            cailiaokd = Convert.ToDouble(dt.Rows[0]["BM_MAWIDTH"].ToString());//材料宽度
            cailiaodzh = Convert.ToDouble(dt.Rows[0]["BM_MAUNITWGHT"].ToString());//材料单重
            //cailiaozongzhong = Convert.ToDouble(dt["BM_MATOTALWGHT"].ToString());//材料总重
            ViewState["cailiaozongchang"] = Convert.ToDouble(dt.Rows[0]["BM_MATOTALLGTH"].ToString()) / Convert.ToDouble(dt.Rows[0]["BM_NUMBER"].ToString());//材料总长（单个）
            ViewState["bgzmy"] = Convert.ToDouble(dt.Rows[0]["BM_MABGZMY"].ToString()) / Convert.ToDouble(dt.Rows[0]["BM_NUMBER"].ToString());//单个面域
            shuliang = Convert.ToDouble(dt.Rows[0]["BM_NUMBER"].ToString());//数量
            dzh = Convert.ToDouble(dt.Rows[0]["BM_UNITWGHT"].ToString());//单重
            //zongzhong = Convert.ToDouble(dt["BM_TOTALWGHT"].ToString());//总重
            xinzhuang = dt.Rows[0]["BM_MASHAPE"].ToString();//形状
            zhuangtai = dt.Rows[0]["BM_MASTATE"].ToString();//状态
            process = dt.Rows[0]["BM_PROCESS"].ToString();//工艺流程
            tixian = dt.Rows[0]["BM_ISMANU"].ToString();//体现明细
            ddlKeyComponents = dt.Rows[0]["BM_KEYCOMS"].ToString();//关键部件
            //ddlFixedSize = dt["BM_FIXEDSIZE"].ToString();//是否定尺
            //beizhu = dt["BM_NOTE"].ToString();//备注
        }
        /// <summary>
        /// 插入数据(更新)
        /// </summary>
        protected void Insertbind(List<string> list)
        {
            sqlText = "";
            sqlText = "insert into " + ViewState["tablename"] + " ";
            sqlText += "(BM_XUHAO,BM_TUHAO,BM_MARID,BM_ZONGXU,BM_CHANAME,BM_ENGSHNAME,BM_GUIGE,BM_PJID,BM_ENGID,";//8
            sqlText += "BM_MALENGTH,BM_MAWIDTH,BM_NUMBER,BM_MAUNITWGHT,BM_MATOTALWGHT,";//5
            sqlText += "BM_MATOTALLGTH,BM_MABGZMY,BM_UNITWGHT,BM_TOTALWGHT,";//4
            sqlText += "BM_MASHAPE,BM_MASTATE,BM_FILLMAN,";//3
            sqlText += "BM_PROCESS,BM_NOTE,BM_KEYCOMS,BM_FIXEDSIZE,BM_ISMANU) values ";//5

            sqlText += "('" + xuhao + "','" + tuhao + "','" + marid + "','" + zongxu + "','" + ch_name + "','" + en_name + "','" + guige + "','" + pro_id.Value + "','" + tsaid.Text + "',";
            sqlText += "'" + cailiaocd + "','" + cailiaokd + "','" + shuliang + "','" + cailiaodzh + "','" + cailiaozongzhong + "',";
            sqlText += "'" + cailiaozongchang + "','" + bgzmy + "','" + dzh + "','" + zongzhong + "',";
            sqlText += "'" + xinzhuang + "','" + zhuangtai + "','" + Session["UserID"] + "',";
            sqlText += "'" + process + "','" + beizhu + "','" + ddlKeyComponents + "','" + ddlFixedSize + "','"+tixian+"')";

            list.Add(sqlText);

            sqlText = "";
            sqlText = "insert into TBPM_TEMPMARDATA(BM_ZONGXU,BM_MARID,BM_ENGID,BM_MAUNITWGHT,BM_UNITWGHT,BM_NUMBER,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH)" +
                " Values('" + zongxu + "','" + marid + "','" + tsaid.Text + "'," + cailiaodzh + "," + dzh + "," + shuliang + "," + cailiaocd + "," + cailiaokd + "," + ViewState["cailiaozongchang"].ToString() + ")";
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

            string[] a = tsaid.Text.Split('-');
            ///////////////////////string firstCharofZX = a[a.Length - 1];
            string firstCharofZX="1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";
            /////////////////////////////////string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
            string pattern2 = @"^" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}$";
            Regex rgx = new Regex(pattern);
            Regex rgx2 = new Regex(pattern2);
            string ret = "0";
            List<string> list_sql = new List<string>();
            //检验总序格式
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                if (zongxu != "")
                {
                    if (!rgx.IsMatch(zongxu))
                    {
                        ret = "FormError-" + firstCharofZX + "-" + zongxu;
                        return ret;
                    }
                    else
                    {
                        string mar = "NotConcern";//由于批量复制是只是物料，这里不考虑物料为空的情况
                        if (mar != "")
                        {
                           list_sql.Add("insert into TBPM_TEMPORGDATA(BM_ZONGXU,BM_MARID,BM_ENGID) Values('" + zongxu + "','" + mar + "','" + tsaid.Text.Trim() + "')");
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
        private DataTable ExecMarCheck(ParamsCheckMarNotBelongToMar psv)
        {
            DataTable dt;
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
    }
}
