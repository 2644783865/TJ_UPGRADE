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
    public partial class TM_BulkEdit : System.Web.UI.Page
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
        string lblxuhao;
        double bjzz;
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
            string sql = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" + tsa_id.Split('-')[0] + "'";
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
            sqlText = "select * from View_TM_TaskAssign where BM_ENGID='" + tsaid.Text.Trim() + "' and BM_XUHAO in (";
            for (int i = 0; i < Arry.Length - 1; i++)
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

                foreach (GridViewRow grow in GridView1.Rows)
                {
                    TextBox marid = (TextBox)grow.FindControl("marid");
                    if (marid.Text.Trim()== "")
                    {
                        marid.Enabled = false;
                        marid.BackColor = System.Drawing.Color.Gray;
                    }
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnSave.Visible = false;
                btnDelete.Visible = false;
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            DataTable dt = this.GetCreatTable();
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

                foreach (GridViewRow grow in GridView1.Rows)
                {
                    TextBox marid = (TextBox)grow.FindControl("marid");
                    if (!marid.Enabled)
                    {
                        marid.Enabled = false;
                        marid.BackColor = System.Drawing.Color.Gray;
                    }
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnSave.Visible = false;
                btnDelete.Visible = false;
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
            dt.Columns.Add("BM_NUMBER");//总数量
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
            dt.Columns.Add("BM_MATOTALLGTH");//材料总长
            dt.Columns.Add("BM_MAUNIT");//单位
            dt.Columns.Add("BM_STANDARD");//国标
            dt.Columns.Add("BM_MASHAPE");//毛坯
            dt.Columns.Add("BM_MASTATE");//状态
            dt.Columns.Add("BM_PROCESS");//工艺流程
            dt.Columns.Add("BM_ENGSHNAME");//英文名称
            dt.Columns.Add("BM_KEYCOMS");//关键部件
            dt.Columns.Add("BM_FIXEDSIZE");//是否定尺
            dt.Columns.Add("BM_XUHAO");//序号
            dt.Columns.Add("BM_SINGNUMBER");
            dt.Columns.Add("BM_PNUMBER");
            dt.Columns.Add("BM_MPMY");
            dt.Columns.Add("BM_PARTOWNWGHT");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HtmlInputText)gRow.FindControl("tuhao")).Value.Trim();
                newRow[1] = ((TextBox)gRow.FindControl("marid")).Text.Trim();
                newRow[2] = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                newRow[3] = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                newRow[4] = ((HtmlInputText)gRow.FindControl("beizhu")).Value.Trim();
                newRow[5] = ((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim();
                newRow[6] = ((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim();
                newRow[7] = ((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim();//总数量
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
                newRow[31] = ((Label)gRow.FindControl("lblxuhao")).Text.ToString();
                newRow[32]=((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim();//单台数量
                newRow[33] = ((HtmlInputText)gRow.FindControl("p_shuliang")).Value.Trim();//单台数量
                newRow[34] = ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim();//
                newRow[35] = ((HtmlInputText)gRow.FindControl("bjzz")).Value.Trim();//
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string ret = "0";
            foreach (GridViewRow grow in GridView1.Rows)
            {
                TextBox txt_marid = (TextBox)grow.FindControl("marid");
                if (txt_marid.Enabled)
                {
                    if (txt_marid.Text.Trim() == "")
                    {
                        ret = "1";
                        break;
                    }
                }
            }
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
                        //自重
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
                        //隐藏字段（序号）
                        lblxuhao=((Label )gRow.FindControl("lblxuhao")).Text.ToString();
                        #region  更新数据

                        //修改时所有部件重量置0
                        if (marid == "")
                        {
                            dzh = bjzz;
                            zongzhong = bjzz*shuliang;
                            cailiaodzh = 0;
                            cailiaozongzhong = 0;
                        }

                        sqlText = "update " + ViewState["tablename"] + " set ";
                        sqlText += "BM_TUHAO='" + tuhao + "',BM_MARID='" + marid + "',BM_ZONGXU='" + zongxu + "',BM_CHANAME='" + ch_name + "',";
                        sqlText += "BM_ENGSHNAME='" + en_name + "',BM_GUIGE='" + guige + "',BM_MALENGTH='" + cailiaocd + "',BM_MAWIDTH='" + cailiaokd + "',";
                        sqlText += "BM_NUMBER='" + shuliang + "',BM_SINGNUMBER='" + singshuliang + "',BM_PNUMBER='" + p_shuliang + "',BM_MAUNITWGHT='" + cailiaodzh + "',BM_MATOTALWGHT='" + cailiaozongzhong + "',";
                        sqlText += "BM_MATOTALLGTH='" + cailiaozongchang + "',BM_MABGZMY='" + bgzmy + "',BM_UNITWGHT='" + dzh + "',BM_TOTALWGHT='" + zongzhong + "',";
                        sqlText += "BM_MASHAPE='" + xinzhuang + "',BM_MASTATE='" + zhuangtai + "',BM_PROCESS='" + process + "',";
                        sqlText += "BM_NOTE='" + beizhu + "',BM_KEYCOMS='" + ddlKeyComponents + "',BM_FIXEDSIZE='" + ddlFixedSize + "',BM_ISMANU='" + tixian + "',";
                        sqlText += "BM_TUUNITWGHT='" + tudz + "',BM_TUTOTALWGHT='" + tuzongzhong + "',BM_TUMAQLTY='" + tucz + "',BM_TUPROBLEM='" + tuwt + "',BM_TUSTAD='" + tubz + "',BM_MPMY="+mpmy+"";
                        sqlText += " where BM_ENGID='" + tsaid.Text + "' and BM_XUHAO='" + lblxuhao + "'";
                        list_sql.Add(sqlText);

                        sqlText = "update TBPM_TEMPMARDATA set BM_ZONGXU='" + zongxu + "',BM_MARID='" + marid + "',BM_MAUNITWGHT=" + cailiaodzh + ",BM_UNITWGHT=" + dzh + ",BM_TUUNITWGHT=" + tudz + ",BM_NUMBER=" + shuliang + ",";
                        sqlText += "BM_MALENGTH=" + cailiaocd + ",BM_MAWIDTH=" + cailiaokd + ",BM_MATOTALLGTH=" + cailiaocd + "*1.05  where BM_ENGID='" + tsaid.Text + "' and BM_XUHAO='" + lblxuhao + "'";
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:原始数据已更新!\\r\\r影响行数:" + insertcount + "');window.returnValue=\"Refesh\";window.close();", true);
                }
            }
            else if (ret == "1")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r如果原始数据有物料编码，则保存时必须有物料编码');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r未知错误，请与管理员联系......');", true);
            }
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

            TM_BasicFun.HiddenShowColumn(GridView1, cklHiddenShow, Index, "Copy");
        }
    }
}
