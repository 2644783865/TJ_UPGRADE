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
    public partial class TM_OutSide_Add : System.Web.UI.Page
    {
        #region
        string tsa_id;
        string sqlText;
        string xuhao;
        string tuhao;
        string marid;
        string typeid;
        string zongxu;
        string ch_name;
        string guige;
        string cailiaoname;
        string cailiaoguige;
        float lilunzhl;
        float cailiaodzh;
        float cailiaozongzhong;
        string caizhi;
        int shuliang;
        int singshuliang;
        int p_shuliang;
        float my;
        float mpmy;
        float dzh;
        float zongzhong;
        float cailiaochangdu;
        float cailiaokudu;
        float cailiaozongchang;
        string labunit;
        string xinzhuang;
        string zhuangtai;
        string biaozhun;
        string process;
        string beizhu;
        string type;
        float tudz;//图纸上单重
        float tuzongzhong;//图纸上总重
        string tucz;//图纸上材质
        string tuwt;//图纸上问题
        string tustrd;//图纸上标注
        string ku;
        string date;
        string zhujima;
        string wmarplan;
        int count = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            tsa_id = Request.QueryString["tsa_id"];
            if (!IsPostBack)
            {
                InitInfo();
                InitGridview();                
            }
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
            sqlText = "select CM_PROJ,TSA_ENGNAME,TSA_PJID,TSA_NUMBER ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;
                lab_proname.Text = dr[0].ToString();
                lab_engname.Text = dr[1].ToString();
                labprostru.Text = dr[1].ToString() + "设计BOM表";
               
                pro_id.Value = dr[2].ToString();
                lblNumber.Text = dr[3].ToString();
            }
            dr.Close();
        }
       

        /// <summary>
        /// //获取加工件物料的最大ID
        /// </summary>
        private void CheckjgMarId() 
        {
            ViewState["jgmaridNum"] = 1;
            sqlText = "select count(*) from TBMA_MATERIAL where ID like '01.08.%'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows[0][0].ToString() != "0")
            {
                sqlText = "select top 1 right(ID,6) from TBMA_MATERIAL where ID like '01.08.%' order by ID desc";
                System.Data.DataTable dt_top = DBCallCommon.GetDTUsingSqlText(sqlText);
                ViewState["jgmaridNum"] = Convert.ToInt64(dt_top.Rows[0][0].ToString()) + 1;
            }
        }
        /// <summary>
        /// //获取外购件物料的最大ID
        /// </summary>
        private void CheckwgMarId()  
        {
            ViewState["wgmaridNum"] = 1;
            sqlText = "select count(*) from TBMA_MATERIAL where ID like '01.11.%'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows[0][0].ToString() != "0")
            {
                sqlText = "select top 1 right(ID,6) from TBMA_MATERIAL where ID like '01.11.%' order by ID desc";
                System.Data.DataTable dt_top = DBCallCommon.GetDTUsingSqlText(sqlText);
                ViewState["wgmaridNum"] = Convert.ToInt64(dt_top.Rows[0][0].ToString()) + 1;
            }
        }
        /// <summary>
        /// //获取标准件物料的最大ID
        /// </summary>
        private void CheckstrMarId()  
        {
            ViewState["strmaridNum"] = 1;
            sqlText = "select count(*) from TBMA_MATERIAL where ID like '01.01.%'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows[0][0].ToString() != "0")
            {
                sqlText = "select top 1 right(ID,6) from TBMA_MATERIAL where ID like '01.01.%' order by ID desc";
                System.Data.DataTable dt_top = DBCallCommon.GetDTUsingSqlText(sqlText);
                ViewState["strmaridNum"] = Convert.ToInt64(dt_top.Rows[0][0].ToString()) + 1;
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
            dt.Columns.Add("BM_CHANAME");//中文名称1
            dt.Columns.Add("BM_ZONGXU");//总序2
            dt.Columns.Add("BM_TYPE");//类型3
            dt.Columns.Add("BM_NUMBER");//数量4
            dt.Columns.Add("BM_MAUNIT");//单位5
            dt.Columns.Add("BM_MALENGTH");//材料长度6
            dt.Columns.Add("BM_MAWIDTH");//材料宽度7
            dt.Columns.Add("BM_NOTE");//下料备注8
            dt.Columns.Add("BM_MABGZMY");//面域9
            dt.Columns.Add("BM_TUUNITWGHT");//单重10
            dt.Columns.Add("BM_TUTOTALWGHT");//总重11
            dt.Columns.Add("BM_MATOTALLGTH");//总长12
            dt.Columns.Add("BM_MAQUALITY");//材质13
            dt.Columns.Add("BM_GUIGE");//规格14
            dt.Columns.Add("BM_STANDARD");//规格14


            dt.Columns.Add("BM_TUMAQLTY");//图纸上材质15

            dt.Columns.Add("BM_TUPROBLEM");//图纸上问题16
            dt.Columns.Add("BM_XIALIAO");//下料方式17
            dt.Columns.Add("BM_PROCESS");//工艺流程18
            dt.Columns.Add("BM_ALLBEIZHU");//备注19
           
          
          
           
            dt.Columns.Add("BM_ISMANU");//是否制作明细20
            dt.Columns.Add("BM_FIXEDSIZE");//是否定尺21
            dt.Columns.Add("BM_WMARPLAN");//是否提计划22
       
            dt.Columns.Add("BM_TOTALNUMBER");//总数量23
            dt.Columns.Add("BM_PNUMBER");//计划数量24
            dt.Columns.Add("BM_MPMY");

            dt.Columns.Add("BM_ZHUJIMA");//助记码

          //  dt.Columns["BM_MASHAPE"].DefaultValue = "采";
            #endregion
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HtmlInputText)gRow.FindControl("tuhao")).Value.Trim();
                newRow[1] = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                newRow[2] = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                newRow[3] = ((DropDownList)gRow.FindControl("ddltype")).SelectedValue;
                newRow[4] = ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim();
                newRow[5] = ((HtmlInputText)gRow.FindControl("labunit")).Value.Trim();
                newRow[6] = ((HtmlInputText)gRow.FindControl("cailiaochangdu")).Value.Trim();
                newRow[7] = ((HtmlInputText)gRow.FindControl("cailiaokuandu")).Value.Trim();
                newRow[8] = ((HtmlInputText)gRow.FindControl("xialiaoNote")).Value.Trim();
                newRow[9] = ((HtmlInputText)gRow.FindControl("my")).Value.Trim();
                newRow[10] = ((HtmlInputText)gRow.FindControl("dzh")).Value.Trim();
                newRow[11] = ((HtmlInputText)gRow.FindControl("zongzhong")).Value.Trim();
                newRow[12] = ((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim();
                newRow[13] = ((HtmlInputText)gRow.FindControl("caizhi")).Value.Trim();
                newRow[14] = ((HtmlInputText)gRow.FindControl("guige")).Value.Trim();
                newRow[15] = ((HtmlInputText)gRow.FindControl("biaozhun")).Value.Trim();
                newRow[16] = ((HtmlInputText)gRow.FindControl("tucz")).Value.Trim();
                newRow[17] = ((HtmlInputText)gRow.FindControl("tuwt")).Value.Trim();
                newRow[18] = ((HtmlInputText)gRow.FindControl("xialiao")).Value.Trim();
                newRow[19] = ((HtmlInputText)gRow.FindControl("process")).Value.Trim();
                newRow[20] = ((HtmlInputText)gRow.FindControl("beizhu")).Value.Trim();
                newRow[21] = ((DropDownList)gRow.FindControl("ddlKeyComponents")).SelectedValue;
                newRow[22] = ((DropDownList)gRow.FindControl("ddlFixedSize")).SelectedValue;
                newRow[23] = ((DropDownList)gRow.FindControl("ddlWmarPlan")).SelectedValue;
                newRow[24] = ((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim();
                newRow[25] = ((HtmlInputText)gRow.FindControl("p_shuliang")).Value.Trim();
                newRow[26] = ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim();
                newRow[27] = ((HtmlInputText)gRow.FindControl("zjm")).Value.Trim();
                dt.Rows.Add(newRow);
            }

            for (int i = GridView1.Rows.Count; i < 15; i++)
            {
                DataRow newRow = dt.NewRow();
               
                newRow[21] = "Y";
                newRow[22] = "N";
                newRow[23] = "Y";
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
          //  if (istid.Value == "1")//相当于确定
           // {
                System.Data.DataTable dt = this.GetDataFromGrid();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gRow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow[20] = "Y";
                        newRow[21] = "N";
                        newRow[22] = "Y";
                        dt.Rows.InsertAt(newRow, i + 1 + count);
                        ///////dt.Rows.RemoveAt(GridView1.Rows.Count - 1);
                        count++;
                    }
                }
                istid.Value = "0";
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
                InitGridview();
         //   }
        }
        /// <summary>
        /// 原始数据中的删除,不对数据库操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndelete_Click(object sender, EventArgs e)
        {
            //***********删除数据不对数据库操作************
            if (txtid.Value != "0")
            {
                System.Data.DataTable dt = this.GetDataFromGrid();
                for (int i = int.Parse(txtid.Value) - 1; i < GridView1.Rows.Count; i++)
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
                InitGridview();
            }
        }

        /// <summary>
        /// 原始数据中的保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
          
            /*获取最大物料编码号
             * 还存在一点漏洞就是如果如果多人同时提交，
             * 当前获取的物料ID并非最大的物料ID，但ID
             * 是主键，不会影响数据的正确性，但会报错。
            */
            this.CheckjgMarId();
            this.CheckwgMarId();
            ///////////this.CheckstrMarId();
            //
            string ret = this.CheckMarNotBelongToMar();
            if (ret == "0")//检查无误
            {
                btnsave.Visible = false;
                List<string> list_sql = new List<string>();
               
                int insertcount = 0;//插入行数
                date = DateTime.Now.ToString("yyyy-MM-dd");
                #region
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gRow.FindControl("CheckBox1");
                    zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value;
                    type = ((DropDownList)gRow.FindControl("ddltype")).SelectedValue;
                    guige = ((HtmlInputText)gRow.FindControl("guige")).Value.Trim();
                    ch_name = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                    zhujima = ((HtmlInputText)gRow.FindControl("zjm")).Value.Trim();
                    labunit = ((HtmlInputText)gRow.FindControl("labunit")).Value.Trim();

                    #region 获取数据
                    if (zongxu != "" && type != "0" && guige != "" && ch_name!="")
                    {
                        tuhao = ((HtmlInputText)gRow.FindControl("tuhao")).Value;

                        if (type == "1" || type == "3")  //表示协A和“标”
                        {
                            typeid = "01.11";
                            marid = "01.11." + (ViewState["wgmaridNum"].ToString()).PadLeft(6, '0');
                            ViewState["wgmaridNum"] = Convert.ToInt64(ViewState["wgmaridNum"].ToString()) + 1;
                        }
                
                        else //其他：半、半退、半正、半调、协B、成品
                        {
                            typeid = "01.08";
                            marid = "01.08." + (ViewState["jgmaridNum"].ToString()).PadLeft(6, '0');
                            ViewState["jgmaridNum"] = Convert.ToInt64(ViewState["jgmaridNum"].ToString()) + 1;
                        }
                        xuhao = zongxu;

                        beizhu = ((HtmlInputText)gRow.FindControl("beizhu")).Value.Trim();


                        cailiaoname = ch_name;
                        cailiaoguige = guige;

                        shuliang = int.Parse(((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim() == "" ? lblNumber.Text : ((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim());
                        singshuliang = int.Parse(((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim() == "" ? "1" : ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim());
                        p_shuliang = int.Parse(((HtmlInputText)gRow.FindControl("p_shuliang")).Value.Trim() == "" ? lblNumber.Text : ((HtmlInputText)gRow.FindControl("p_shuliang")).Value.Trim());

                        caizhi = ((HtmlInputText)gRow.FindControl("caizhi")).Value.Trim();

                        //材料长度
                        cailiaochangdu = float.Parse(((HtmlInputText)gRow.FindControl("cailiaochangdu")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaochangdu")).Value.Trim());
                        //材料宽度
                        cailiaokudu = float.Parse(((HtmlInputText)gRow.FindControl("cailiaokuandu")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaokuandu")).Value.Trim());
                        my = float.Parse(((HtmlInputText)gRow.FindControl("my")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("my")).Value.Trim());
                        mpmy = float.Parse(((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim());
                        //材料总长
                        cailiaozongchang = float.Parse(((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim());
                        //单重
                        dzh = float.Parse(((HtmlInputText)gRow.FindControl("dzh")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("dzh")).Value.Trim());
                        lilunzhl = dzh; //理论重量
                        //总重
                        zongzhong = dzh*shuliang;
                        biaozhun = ((HtmlInputText)gRow.FindControl("biaozhun")).Value.Trim();
                       
                        process = ((HtmlInputText)gRow.FindControl("process")).Value.Trim();
                        //材料单重
                        cailiaodzh = dzh;
                        cailiaozongzhong = dzh*p_shuliang; //材料总重
                        tudz = dzh;
                        tuzongzhong = tudz * shuliang;
                        tucz = ((HtmlInputText)gRow.FindControl("tucz")).Value.Trim();
                        tuwt = ((HtmlInputText)gRow.FindControl("tuwt")).Value.Trim();
                      //  tustrd = ((HtmlInputText)gRow.FindControl("tubz")).Value.Trim();
                   //     ku = ((HtmlInputText)gRow.FindControl("ku")).Value.Trim();
                        wmarplan = ((DropDownList)gRow.FindControl("ddlWmarPlan")).SelectedValue.Trim();
                        Insertbind(list_sql);
                        insertcount++;
                    }
                    else if (zongxu != "" && (type == "0" || guige == "" || ch_name == "" || zhujima == "" || labunit==""))
                    {
                        btnsave.Visible = true;
                        int j = i + 1;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法保存，第【"+j+"】行助记码、中文名称、总序、类型、单位、规格不能为空！！！\\r\\r提示:\\r(1)、如果没有规格，请将图号输入至规格中；\\r(2)、附图采购件也需要将图号输入规格中；\\r(3)、规格格式:规格-图号或图号。');", true);
                        return;
                    }
                    #endregion
                }
                #endregion
                DBCallCommon.ExecuteTrans(list_sql);
                btnsave.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:原始数据已保存!\\r\\r影响行数:" + insertcount + "');window.location.reload();", true);
            }
            #region 错误提示
            else if (ret.Contains("True-"))//页面上将导致物料重复
            {
                string[] aa = ret.Split('-');
                string bb = aa[1];
                string cc = aa[2];
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上第【" + bb + "】行与第【"+cc+"】行将导致物料编码重复！！！');", true);
            }
            else if (ret.Contains("TrueMarDB-"))//页面上与数据库中物料重复
            {
                string[] aa = ret.Split('-');
                string bb = aa[1];
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r" + bb + "！！！');", true);
            }
            else if (ret.Contains("DataBaseRepeat"))//页面总序与数据库中重复
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"在数据库中已存在！！！');", true);
            }
            else if (ret.Contains("DataBase"))//数据库中父序存在物料编码
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"的父序为底层材料！！！');", true);
            }
            else if (ret.Contains("Page"))//页面上物料归属
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"为底层物料，其页面上父序也为底层物料！！！');", true);
            }
            else if (ret.Contains("PageRepeat"))//页面总序重复
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"重复！！！');", true);
            }
            else if (ret.Contains("FormError-"))//总序格式错误
            {
                string[] aa = ret.Split('-');
                string firstchar = aa[aa.Length - 2].ToString();
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"格式错误！！！\\r\\r正确格式：\"" + firstchar + "\"或\"" + firstchar + ".m\"或\"" + firstchar + "\".m.n...（0<m<100,n>0,m、n为正整数）');", true);
            }
            else if (ret.Contains("XuHaoExisted-"))
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"对应序号已存在！！！');", true);
            }
            else if (ret.Contains("FatherNotExist-"))
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序\"" + outxuhao + "\"的父序在数据库不存在！！！');", true);
            }
            else if (ret.Contains("NoWeight-"))
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r请输入重量，外购加工件，重量必须大于零！！！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r未知错误，请与管理员联系！！！');", true);
            }
            #endregion 
        }
        
        /// <summary>
        /// 插入数据(更新)
        /// </summary>
        protected void Insertbind(List<string> list)
        {
            sqlText = "";
            sqlText = "insert into TBPM_STRINFODQO ";
            sqlText += "(BM_XUHAO,BM_TUHAO,BM_MARID,BM_ZONGXU,BM_CHANAME,BM_GUIGE,BM_PJID,BM_ENGID,";//8
            sqlText += "BM_NUMBER,BM_SINGNUMBER,BM_PNUMBER,BM_MAUNITWGHT,BM_MATOTALWGHT,";//5
            sqlText += "BM_UNITWGHT,BM_TOTALWGHT,";//4
            sqlText += "BM_MASHAPE,BM_MASTATE,BM_FILLMAN,";//3
            sqlText += "BM_PROCESS,BM_NOTE,BM_KEYCOMS,";//4
            sqlText += "BM_TUUNITWGHT,BM_TUTOTALWGHT,BM_TUMAQLTY,BM_TUPROBLEM,BM_TUSTAD,BM_ISMANU,BM_MABGZMY,BM_MALENGTH,BM_MATOTALLGTH,BM_MAWIDTH,BM_KU,BM_WMARPLAN,BM_MPMY) values ";//9

            sqlText += "('" + xuhao + "','" + tuhao + "','" + marid + "','" + zongxu + "','" + ch_name + "','" + guige + "','" + pro_id.Value + "','" + tsaid.Text + "',";
            sqlText += "'" + shuliang + "','"+singshuliang+"','"+p_shuliang+"','" + cailiaodzh + "','" + cailiaozongzhong + "',";
            sqlText += "'" + dzh + "','" + zongzhong + "',";
            sqlText += "'" + xinzhuang + "','" + zhuangtai + "','" + Session["UserID"] + "',";
            sqlText += "'" + process + "','" + beizhu + "','Y',";
            sqlText += "" + tudz + "," + tuzongzhong + ",'" + tucz + "','" + tuwt + "','" + tustrd + "','Y',"+my+","+cailiaochangdu+","+cailiaozongchang+","+cailiaokudu+",'"+ku+"','"+wmarplan+"',"+mpmy+")";

            list.Add(sqlText);

            sqlText = "";
            sqlText = "insert into TBPM_TEMPMARDATA(BM_ZONGXU,BM_XUHAO,BM_MARID,BM_ENGID,BM_MAUNITWGHT,BM_UNITWGHT,BM_TUUNITWGHT,BM_NUMBER,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH)" +
                " Values('" + zongxu + "','" + xuhao + "','" + marid + "','" + tsaid.Text + "'," + cailiaodzh + "," + dzh + "," + tudz + "," + shuliang + ","+cailiaochangdu+","+cailiaokudu+","+cailiaozongchang+")";
            list.Add(sqlText);


            //生成物料编码 单位已统一
            sqlText = "";
            //////////////////if (ch_name == "钢格板" || ch_name == "栅格板" || ch_name=="钢板网")
            //////////////////{
            //////////////////    sqlText = "insert into TBMA_MATERIAL(ID,MNAME,HMCODE,GUIGE,MWEIGHT,GB,CAIZHI,TECHUNIT,CONVERTRATE,PURCUNIT,FUZHUUNIT,TYPEID,FILLDATE,CLERK,STATE,NOTE) ";
            //////////////////    sqlText += " values('" + marid + "','" + ch_name + "','" + hmcode + "','" + guige + "','" + lilunzhl + "','" + biaozhun + "','" + caizhi + "',";
            //////////////////    sqlText += "'" + labunit + "',1000,'T','" + labunit + "','" + typeid + "','" + date + "','" + Session["UserID"].ToString() + "','1','" + beizhu + "')";
            //////////////////}
            //////////////////else
            //////////////////{
            sqlText = "insert into TBMA_MATERIAL(ID,MNAME,HMCODE,GUIGE,MWEIGHT,GB,CAIZHI,TECHUNIT,CONVERTRATE,PURCUNIT,FUZHUUNIT,TYPEID,FILLDATE,CLERK,STATE,NOTE) ";
            sqlText += " values('" + marid + "','" + ch_name + "','" + zhujima + "','" + guige + "','" + lilunzhl + "','" + biaozhun + "','" + caizhi + "',";
            sqlText += "'" + labunit + "',1,'" + labunit + "','T','" + typeid + "','" + date + "','" + Session["UserID"].ToString() + "','1','" + beizhu + "')";
            //////////////////}

            list.Add(sqlText);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private string CheckMarNotBelongToMar()
        {
            this.CheckjgMarId();
            this.CheckwgMarId();
            string sql_delete = "delete from TBPM_TEMPORGDATA where BM_ENGID='" + tsaid.Text + "'";
            DBCallCommon.ExeSqlText(sql_delete);//删除表TBPM_TEMPORGDATA中该生产制号下数据，防止意外情况未清空上次记录

            string [,] array_checkmarreapte=new string[200,6];
            int arryCapacity = 0;

           // string[] a = tsaid.Text.Split('-');
            //////////string firstCharofZX = a[a.Length - 1];
            /////////string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";

            string pattern2 = @"^" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}$";
            Regex rgx = new Regex(pattern);
            Regex rgx2 = new Regex(pattern2);
            string ret = "0";
            List<string> list_sql = new List<string>();
            //检验总序格式
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                //总序
                zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                //类型
                type = ((DropDownList)gRow.FindControl("ddltype")).SelectedValue;
                //名称
                ch_name = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                //规格
                guige = ((HtmlInputText)gRow.FindControl("guige")).Value.Trim();
                //材质
                caizhi = ((HtmlInputText)gRow.FindControl("caizhi")).Value.Trim();
                //国标
                biaozhun = ((HtmlInputText)gRow.FindControl("biaozhun")).Value.Trim();
                //重量
                string zh = ((HtmlInputText)gRow.FindControl("dzh")).Value.Trim();
                
                if (zongxu != "" & type != "0")
                {
                    if (!rgx.IsMatch(zongxu))
                    {
                        ret = "FormError-" + firstCharofZX + "-" + zongxu;
                        return ret;
                    }
                    else if (zh == "" || zh == "0")
                    {
                        ret = "NoWeight-" + zongxu;
                        return ret;
                    }
                    else
                    {
                        array_checkmarreapte[arryCapacity, 0] = ch_name;
                        array_checkmarreapte[arryCapacity, 1] = guige;
                        array_checkmarreapte[arryCapacity, 2] = caizhi;
                        array_checkmarreapte[arryCapacity, 3] = biaozhun;
                        array_checkmarreapte[arryCapacity, 4] =Convert.ToInt16(i + 1).ToString();
                        array_checkmarreapte[arryCapacity, 5] = type;
                        arryCapacity++;

                        list_sql.Add("insert into TBPM_TEMPORGDATA(BM_ZONGXU,BM_MARID,BM_ENGID) Values('" + zongxu + "','AllHaveMar','" + tsaid.Text.Trim() + "')");
                    }
                }
            }

            //检验是否有重复的物料（页面上）
            string retValue = this.CheckMarRepeat(array_checkmarreapte,arryCapacity);
            if (retValue.Contains("True-"))
            {
                return retValue;
            }
            //检验页面上将生成的物料与数据库中是否重复
            string retDBValue = this.CheckMarReapteInDB(array_checkmarreapte, arryCapacity);
            if (retDBValue.Contains("TrueMarDB-"))
            {
                return retDBValue;
            }
            //检验总序输入临时表
            DBCallCommon.ExecuteTrans(list_sql);
            //检验归属关系
            ParamsCheckMarNotBelongToMar pcmar = new ParamsCheckMarNotBelongToMar();
          
            pcmar.StrTabeleName = "TBPM_STRINFODQO";
            pcmar.TaskID = tsaid.Text;
            if (ckbAdd.Checked)
            {
                pcmar.Child = "2";//可以录入归属物料
            }
            else
            {
                pcmar.Child = "1";//不能存在归属物料
            }
            System.Data.DataTable dt = this.ExecMarCheck(pcmar);
            ret = dt.Rows[0][0].ToString();
            return ret;
        }
        /// <summary>
        /// 检验要生成的物料编码是否有重复的(页面上)
        /// </summary>
        /// <param name="array_check"></param>
        /// <returns></returns>
        private string CheckMarRepeat(string[,] array_check,int capacity)
        {
            string  arrayreturn="False";
            for (int i = 0; i < capacity-1; i++)
            {
                for (int j = i + 1; j < capacity; j++)
                {
                    bool flag = (array_check[i, 0] == array_check[j, 0]) && (array_check[i, 1] == array_check[j, 1]) && (array_check[i, 2] == array_check[j, 2]) && (array_check[i, 3] == array_check[j, 3]);
                    if(flag)
                    {
                        string a = array_check[i, 4];
                        string b = array_check[j, 4];
                        arrayreturn="True-"+a+"-"+b;
                    }
                }
            }
            return arrayreturn;
        }
        /// <summary>
        /// 检验物料编码与数据是否重复
        /// </summary>
        /// <param name="array_check"></param>
        /// <param name="capacity"></param>
        /// <returns></returns>
        private string CheckMarReapteInDB(string[,] array_check, int capacity)
        {
            string retVal = "False";//没有重复的
            string sql = "";
            string typeid = "";
            for (int i = 0; i < capacity; i++)
            {
                switch (array_check[i,5])
                {
                    case "1": typeid = "01.08";
                        break;
                    default:
                        typeid = "01.11";
                        break;
                }
                sql = "select * from TBMA_MATERIAL where TYPEID='" + typeid + "' and MNAME='" + array_check[i, 0] + "' AND GUIGE='" + array_check[i, 1] + "' AND CAIZHI='" + array_check[i, 2] + "' AND GB='" + array_check[i, 3] + "' and STATE='1'";//在用的物料中是否存在与添加物料重复的
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    retVal = "TrueMarDB-第" + array_check[i, 4] + "行输入将导致物料重复\\r已存在物料信息>>\\r物料编码:" + dr["ID"].ToString() + " 名称:" + dr["MNAME"].ToString() + " 助记码:" + dr["HMCODE"].ToString();
                    dr.Close();
                    break;
                }
            }
            return retVal;
        }
        /// <summary>
        /// 存储过程参数类
        /// </summary>
        private class ParamsCheckMarNotBelongToMar
        {
            private string _StrTabeleName;
            private string _TaskID;
            private string _Child;
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
            public string Child
            {
                get { return _Child; }
                set { _Child = value; }
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
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_CheckMarNotBelongToMarOutAside]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@StrTable", psv.StrTabeleName, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@ENG_ID", psv.TaskID, SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Child", psv.Child, SqlDbType.Text, 1000);
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
