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
using System.Text;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_BulkCopy : System.Web.UI.Page
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
        double cailiaocd;//材料长度
        double cailiaokd;//材料宽度
        double cailiaodzh;//材料单重
        double cailiaozongzhong;//材料总重
        double cailiaozongchang;//材料总长
        double bgzmy;//面域
        double mpmy;
        double tudz;//图纸单重
        double tuzongzhong;//图纸总重
        string tucz;//图纸材质
        string tubz;//图纸标准
        string tuwt;//图纸问题
        double shuliang;//数量
        double singshuliang;
        double p_shuliang;
        double dzh;//单重
        double zongzhong;//总重
        string xinzhuang;//形状
        string zhuangtai;//状态
        string process;//工艺流程
        string beizhu;//备注
        string ddlKeyComponents;//关键部件
        string ddlFixedSize;//是否定尺
        double bjzz;//部件自重
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Image)Master.FindControl("Image2")).Visible = false;
                ((Image)Master.FindControl("Image3")).Visible = false;
                TM_BasicFun.BindCklShowHiddenItems("Copy", cklHiddenShow);

                this.InitInfo();
                this.GetData();
            }

            if (Session["UserID"] == null || Session["UserID"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('用户信息丢失，导致您无法保存数据！！！\\r\\r提示:如果您有未保存的数据，请不要关闭当前页面，重新打开登录界面登录后即可保存');", true);/////window.location.reload();
                return;
            }
        }
        /// <summary>
        /// 初始化页面(基本信息)
        /// </summary>
        private void InitInfo()
        {
            string tsa_id = "";
            string sqlText = "";
            tsa_id = Request.QueryString["action"];
            sqlText = "select CM_PROJ,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
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

            //读取台数
            string sql = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr_number = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_number.HasRows)
            {
                dr_number.Read();
                lblNumber.Text = dr_number["TSA_NUMBER"].ToString();
                dr_number.Close();
            }
        }

         


        private void GetData()
        {
      
            string[] Arry;
            Arry = Request.QueryString["arry"].Split(',');
            sqlText = "select * from View_TM_DQO where BM_ENGID='" + tsaid.Text.Trim() + "' and BM_XUHAO in (";
            for (int i = 0; i < Arry.Length-1; i++)
            {
                sqlText += "'" + Arry[i].ToString() + "',";
            }
            sqlText += "'" + Arry[Arry.Length - 1].ToString() + "')";
            sqlText += " order by dbo.f_formatstr(BM_ZONGXU,'.')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                btnSave.Visible = true;
                btnDelete.Visible = true;
                NoDataPanel.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnSave.Visible = false;
                btnDelete.Visible = false;
                NoDataPanel.Visible = true;
            }
        }

        /// <summary>
        /// 清空重量列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearColum_OnClick(object sender, EventArgs e)
        {
            string[] array_name = new string[4] {"dzh", "cailiaodzh", "cailiaozongzhong", "zongzhong" };
            foreach (GridViewRow gr in GridView1.Rows)
            {
                TextBox txt_marid = (TextBox)gr.FindControl("marid");
                if(txt_marid.Text.Trim()=="")
                {
                    foreach (string ctlname in array_name)
                    {
                        ((HtmlInputText)gr.FindControl(ctlname)).Value = "0";
                    }
                }
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            DataTable dt=this.GetCreatTable();
            int deletedrows = 0;
            for (int j = 0; j < GridView1.Rows.Count; j++)
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
                NoDataPanel.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnSave.Visible = false;
                btnDelete.Visible = false;
                NoDataPanel.Visible = true;
            }
        }
        /// <summary>
        /// 创建临时Table
        /// </summary>
        protected DataTable GetCreatTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BM_TUHAO");//图号
            dt.Columns.Add("BM_MARID");//物料编码
            dt.Columns.Add("BM_ZONGXU");//总序
            dt.Columns.Add("BM_CHANAME");//中文名称
            dt.Columns.Add("BM_NOTE");//备注
            dt.Columns.Add("BM_MALENGTH");//材料长度
            dt.Columns.Add("BM_MAWIDTH");//材料宽度
            dt.Columns.Add("BM_SINGNUMBER");//数量
            dt.Columns.Add("BM_UNITWGHT");//实际单重
            dt.Columns.Add("BM_MAUNITWGHT");//材料单重
            dt.Columns.Add("BM_MATOTALWGHT");//材料总重
            dt.Columns.Add("BM_MABGZMY");//面域
            dt.Columns.Add("BM_TUUNITWGHT");//图纸单重
            dt.Columns.Add("BM_TUMAQLTY");//图纸上的材质
            dt.Columns.Add("BM_TUSTAD");//图纸上标准
            dt.Columns.Add("BM_TUPROBLEM");//图纸上问题
            dt.Columns.Add("BM_MAQUALITY");//材质
            dt.Columns.Add("BM_GUIGE");//规格
            dt.Columns.Add("BM_MANAME");//材料名称
            dt.Columns.Add("BM_MAGUIGE");//材料规格
            dt.Columns.Add("BM_THRYWGHT");//理论重量
            dt.Columns.Add("BM_TOTALWGHT");//总重
            dt.Columns.Add("BM_MATOTALLGTH");//材料 总长
            dt.Columns.Add("BM_MAUNIT");//单位
            dt.Columns.Add("BM_STANDARD");//国标
            dt.Columns.Add("BM_MASHAPE");//毛坯
            dt.Columns.Add("BM_MASTATE");//状态
            dt.Columns.Add("BM_PROCESS");//工艺流程
            dt.Columns.Add("BM_ENGSHNAME");//英文名称
            dt.Columns.Add("BM_KEYCOMS");//关键部件
            dt.Columns.Add("BM_FIXEDSIZE");//是否定尺
            dt.Columns.Add("BM_NUMBER");//总数量
            dt.Columns.Add("BM_PNUMBER");//计划数量
            dt.Columns.Add("BM_MPMY");
            dt.Columns.Add("BM_PARTOWNWGHT");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HtmlInputText)gRow.FindControl("tuhao")).Value.Trim(); 
                newRow[1] = ((TextBox)gRow.FindControl("marid")).Text.Trim();
                newRow[2]= ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                newRow[3] = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                newRow[4] = ((HtmlInputText)gRow.FindControl("beizhu")).Value.Trim();
                newRow[5] = ((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim();
                newRow[6] = ((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim();
                newRow[7] = ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim();//总数量
                newRow[8] = ((HtmlInputText)gRow.FindControl("dzh")).Value.Trim();
                newRow[9] = ((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim();
                newRow[10] = ((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim();
                newRow[11] = ((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim();
                newRow[12] = ((HtmlInputText)gRow.FindControl("tudz")).Value.Trim();
                newRow[13] = ((HtmlInputText)gRow.FindControl("tucz")).Value.Trim();
                newRow[14] = ((HtmlInputText)gRow.FindControl("tubz")).Value.Trim();
                newRow[15] = ((HtmlInputText)gRow.FindControl("tuwt")).Value.Trim();
                newRow[16] = ((HtmlInputText)gRow.FindControl("caizhi")).Value.Trim();
                newRow[17] = ((HtmlInputText)gRow.FindControl("guige")).Value.Trim();
                newRow[18] = ((HtmlInputText)gRow.FindControl("cailiaoname")).Value.Trim();
                newRow[19] = ((HtmlInputText)gRow.FindControl("cailiaoguige")).Value.Trim();
                newRow[20] = ((HtmlInputText)gRow.FindControl("lilunzhl")).Value.Trim();
                newRow[21] = ((HtmlInputText)gRow.FindControl("zongzhong")).Value.Trim();
                newRow[22] = ((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim();
                newRow[23] = ((HtmlInputText)gRow.FindControl("labunit")).Value.Trim();
                newRow[24] = ((HtmlInputText)gRow.FindControl("biaozhun")).Value.Trim();
                newRow[25] = ((HtmlInputText)gRow.FindControl("xinzhuang")).Value.Trim();
                newRow[26] = ((HtmlInputText)gRow.FindControl("zhuangtai")).Value.Trim();
                newRow[27] = ((HtmlInputText)gRow.FindControl("process")).Value.Trim();
                newRow[28] = ((HtmlInputText)gRow.FindControl("en_name")).Value.Trim();
                newRow[29] = ((DropDownList)gRow.FindControl("ddlKeyComponents")).SelectedValue.ToString();
                newRow[30] = ((DropDownList)gRow.FindControl("ddlFixedSize")).SelectedValue.ToString();
                newRow[31] = ((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim();
                newRow[32] = ((HtmlInputText)gRow.FindControl("p_shuliang")).Value.Trim();
                newRow[33] = ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim();
                newRow[34] = ((HtmlInputText)gRow.FindControl("bjzz")).Value.Trim();
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string ret = this.CheckMarNotBelongToMar();
            if (ret == "0")//检查无误
            {
                string tixian = "N";  //是否体现在制作明细
                List<string> list_sql = new List<string>();
                int insertcount = 0;//插入行数
                #region
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    //总序
                    zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value;
                    marid = ((TextBox)gRow.FindControl("marid")).Text.Trim();
                    #region 获取数据
                    if (zongxu != "")
                    {
                        //图号
                        tuhao = ((HtmlInputText)gRow.FindControl("tuhao")).Value;
                        if (marid != "")//物料归属到虚拟部件下
                        {
                            xuhao = zongxu;
                            if (marid.Contains("01.01"))//标准件体现在制作明细中
                            {
                                tixian = "Y";
                            }
                            else
                            {
                                tixian = "N";
                            }
                        }
                        else  //部件体现在制作明细中
                        {
                            xuhao = zongxu;
                            tixian = "Y";
                        }
                        //中文名称
                        ch_name = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                        //英文名称
                        en_name = ((HtmlInputText)gRow.FindControl("en_name")).Value.Trim();
                        //材料长度
                        cailiaocd = Convert.ToDouble(((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim());
                        //材料宽度
                        cailiaokd = Convert.ToDouble(((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim());
                        //数量
                        shuliang = Convert.ToDouble(((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim() == "" ? lblNumber.Text : ((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim());
                        singshuliang = Convert.ToDouble(((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim() == "" ? "1" : ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim());
                        p_shuliang = Convert.ToDouble(((HtmlInputText)gRow.FindControl("p_shuliang")).Value.Trim() == "" ? lblNumber.Text : ((HtmlInputText)gRow.FindControl("p_shuliang")).Value.Trim());
                        //单重
                        dzh = Convert.ToDouble(((HtmlInputText)gRow.FindControl("dzh")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("dzh")).Value.Trim());
                        //部件自重
                        bjzz = Convert.ToDouble(((HtmlInputText)gRow.FindControl("bjzz")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("bjzz")).Value.Trim());
                        //材料单重
                        cailiaodzh = Convert.ToDouble(((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim());
                        //材料总重
                        cailiaozongzhong = Convert.ToDouble(((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim());
                        //面域
                        bgzmy = Convert.ToDouble(((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim());
                        //计划面域
                        mpmy = Convert.ToDouble(((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim());
                        //图纸单重
                        tudz = Convert.ToDouble(((HtmlInputText)gRow.FindControl("tudz")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("tudz")).Value.Trim());
                        //图纸总重
                        tuzongzhong = tudz * shuliang;
                        //图纸材质
                        tucz = ((HtmlInputText)gRow.FindControl("tucz")).Value.Trim();
                        //图纸标准
                        tubz = ((HtmlInputText)gRow.FindControl("tubz")).Value.Trim();
                        //图纸问题
                        tuwt = ((HtmlInputText)gRow.FindControl("tuwt")).Value.Trim();
                        //规格
                        guige = ((HtmlInputText)gRow.FindControl("guige")).Value.Trim();
                        //总重
                        zongzhong = dzh * shuliang;
                        //材料总长
                        cailiaozongchang = Convert.ToDouble(((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim());
                        //形状
                        xinzhuang = ((HtmlInputText)gRow.FindControl("xinzhuang")).Value.Trim();
                        //状态
                        zhuangtai = ((HtmlInputText)gRow.FindControl("zhuangtai")).Value.Trim();
                        //工艺流程
                        process = ((HtmlInputText)gRow.FindControl("process")).Value.Trim();
                        //备注
                        beizhu = ((HtmlInputText)gRow.FindControl("beizhu")).Value.Trim();
                        //关键部件
                        ddlKeyComponents = ((DropDownList)gRow.FindControl("ddlKeyComponents")).SelectedValue;
                        //是否定尺
                        ddlFixedSize = ((DropDownList)gRow.FindControl("ddlFixedSize")).SelectedValue;
                        #region  插入数据
                        if (marid == "")
                        {
                            dzh = bjzz;
                            cailiaodzh = 0;
                            zongzhong = bjzz*shuliang;
                            cailiaozongzhong = 0;
                        }

                        sqlText = "insert into TBPM_STRINFODQO ";
                        sqlText += "(BM_XUHAO,BM_TUHAO,BM_MARID,BM_ZONGXU,BM_CHANAME,BM_ENGSHNAME,BM_GUIGE,BM_PJID,BM_ENGID,";//8
                        sqlText += "BM_MALENGTH,BM_MAWIDTH,BM_NUMBER,BM_SINGNUMBER,BM_PNUMBER,BM_MAUNITWGHT,BM_MATOTALWGHT,";//5
                        sqlText += "BM_MATOTALLGTH,BM_MABGZMY,BM_UNITWGHT,BM_TOTALWGHT,";//4
                        sqlText += "BM_MASHAPE,BM_MASTATE,BM_FILLMAN,";//3
                        sqlText += "BM_PROCESS,BM_NOTE,BM_KEYCOMS,BM_FIXEDSIZE,BM_ISMANU,";//5
                        sqlText += "BM_TUUNITWGHT,BM_TUTOTALWGHT,BM_TUMAQLTY,BM_TUPROBLEM,BM_TUSTAD,BM_MPMY) values ";//4

                        sqlText += "('" + xuhao + "','" + tuhao + "','" + marid + "','" + zongxu + "','" + ch_name + "','" + en_name + "','" + guige + "','" + pro_id.Value + "','" + tsaid.Text + "',";
                        sqlText += "'" + cailiaocd + "','" + cailiaokd + "','" + shuliang + "','"+singshuliang+"','"+p_shuliang+"','" + cailiaodzh + "','" + cailiaozongzhong + "',";
                        sqlText += "'" + cailiaozongchang + "','" + bgzmy + "','" + dzh + "','" + zongzhong + "',";
                        sqlText += "'" + xinzhuang + "','" + zhuangtai + "','" + Session["UserID"] + "',";
                        sqlText += "'" + process + "','" + beizhu + "','" + ddlKeyComponents + "','" + ddlFixedSize + "','" + tixian + "',";
                        sqlText += "'" + tudz + "','" + tuzongzhong + "','" + tucz + "','" + tuwt + "','" + tubz + "',"+mpmy+")";

                        list_sql.Add(sqlText);

                        sqlText = "";
                        sqlText = "insert into TBPM_TEMPMARDATA(BM_ZONGXU,BM_XUHAO,BM_MARID,BM_ENGID,BM_MAUNITWGHT,BM_UNITWGHT,BM_TUUNITWGHT,BM_NUMBER,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH)" +
                            " Values('" + zongxu + "','" + xuhao + "','" + marid + "','" + tsaid.Text + "'," + cailiaodzh + "," + dzh + "," + tudz + "," + shuliang + "," + cailiaocd + "," + cailiaokd + "," + cailiaocd + "*1.05)";//*1.05对钢板是不适用的，对钢板而言不存在材料总长（单个）
                        list_sql.Add(sqlText);
                        #endregion
                        insertcount++;
                    }
                    #endregion
                }
                #endregion
                DBCallCommon.ExecuteTrans(list_sql);
                if (insertcount == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:输入的总序不能为空！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:原始数据已保存!\\r\\r影响行数:" + insertcount + "');window.returnValue=\"Refesh\";window.close();", true);////////////////window.returnValue=\"Refesh\";window.close();
                }
            }
            #region
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
            else if(ret.Contains("XuHaoExisted"))
            {
                string[] aa = ret.Split('-');
                string firstchar = aa[aa.Length - 2].ToString();
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r与页面上总序\"" + outxuhao + "\"相同的序号已存在！！！');", true);
            }
            else if (ret.Contains("FatherNotExist-"))//父序不存在
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r总序\"" + outxuhao + "\"的父序不存在！！！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r未知错误，请与管理员联系......');", true);
            }
            #endregion
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
            /////////////////////////string firstCharofZX = a[a.Length - 1];
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";

            /////////////////////////string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
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
                        string mar = ((TextBox)gRow.FindControl("marid")).Text.Trim();
                        list_sql.Add("insert into TBPM_TEMPORGDATA(BM_ZONGXU,BM_MARID,BM_ENGID) Values('" + zongxu + "','" + mar + "','" + tsaid.Text.Trim() + "')");
                    }
                }
            }
            //检验总序输入临时表
            DBCallCommon.ExecuteTrans(list_sql);
            //检验归属关系
            ParamsCheckMarNotBelongToMar pcmar = new ParamsCheckMarNotBelongToMar();
            pcmar.StrTabeleName = "View_TM_DQO";
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
        /// <summary>
        /// 列的显示与隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cklHiddenShow_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string strCheck = Request.Form["__EVENTTARGET"].ToString();

            int Index =Convert.ToInt16(strCheck.Substring(strCheck.LastIndexOf("$") + 1));

            TM_BasicFun.HiddenShowColumn(GridView1, cklHiddenShow, Index,"Copy");
        }
    }
}
