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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MSOrgInputMode : System.Web.UI.Page
    {
        #region
        string tsa_id;
        string sqlText ;
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
        float signshuliang;
        float shuliang;
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
        string  tucz;//图纸上材质
        string tuwt;//图纸上问题
        string tustrd;//图纸上标注
        string ku;
        float bjzz;
        string _xuhao;
        string _msxuhao;
        string _ismanum;
        int count = 0;
        ArrayList Arraylist_samezx=new ArrayList();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            tsa_id = Request.QueryString["TaskID"];
            if (!IsPostBack)
            {
                InitInfo();
                InitGridview();
                TM_BasicFun.BindCklShowHiddenItems("MsOrgInputMode", cklHiddenShow);
                this.Form.DefaultButton ="";
                hkoutside.NavigateUrl = "TM_OutSide_Add.aspx?tsa_id=" + tsa_id;
                hpView.NavigateUrl = "TM_Task_View.aspx?action=" + tsaid.Text;
            }

            this.CheckZeroXuHaoExist();
            this.OrgInputNum();

            if(Session["UserID"]==null||Session["UserID"].ToString()=="")
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
            sqlText = "select TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;
                lab_proname.Text = dr[0].ToString();
                lab_engname.Text = dr[1].ToString();
                labprostru.Text = dr[1].ToString() + "设计BOM表";
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
        /// <summary>
        /// 已输入条数
        /// </summary>
        private void OrgInputNum()
        {
            this.GetListName();
            string sql = "select (count(*)) as NUM from " + tablename + " where BM_ENGID='" + tsaid.Text + "'";
            SqlDataReader dr=DBCallCommon.GetDRUsingSqlText(sql);
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
        /// 检查虚拟部件序号是否存在，不存在则创建，如1.0,同时记录1.0下的物料条数
        /// </summary>
        private void CheckZeroXuHaoExist()
        {
            this.GetListName();
            ViewState["virtualPartNumsNow"] = 1;//用于记录虚拟部件下物料记录条数
            string[] taskid_split = tsaid.Text.Trim().Split('-');
            ViewState["zero_index"] = "1.0";
            string sqltext = "select count(*) from " + tablename + " where BM_TASKID='" + taskid_split[0] + "' and (BM_XUHAO='"+ViewState["zero_index"].ToString()+"' or BM_XUHAO like '" + ViewState["zero_index"].ToString() + ".%')";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows[0][0].ToString() == "0")//不存在虚拟部件X.0
            {
                List<string> list_sql = new List<string>();
                //插入原始数据表1/6
                string sql_org = "insert into " + tablename + "(BM_XUHAO,BM_TUHAO,BM_ZONGXU,BM_CHANAME,BM_PJID,BM_ENGID,BM_MAUNITWGHT,BM_MATOTALWGHT,BM_SINGNUMBER,BM_NUMBER,BM_PNUMBER,BM_UNITWGHT,BM_TOTALWGHT,BM_MPSTATE,BM_MSSTATE,BM_OSSTATE,BM_MPSTATUS,BM_MSSTATUS,BM_OSSTATUS) Values('" + ViewState["zero_index"] + "','','" + ViewState["zero_index"] + "','虚拟部件','" + pro_id.Value + "','" + tsaid.Text + "',0,0,1," + lblNumber.Text.Trim() + "," + lblNumber.Text.Trim() + ",0,0,'0','0','0','0','0','0')";
                list_sql.Add(sql_org);
                //插入原始数据物料表TBPM_TEMPMARDATA
                string sql_temp = "insert into TBPM_TEMPMARDATA(BM_XUHAO,BM_ZONGXU,BM_MARID,BM_ENGID,BM_MAUNITWGHT,BM_UNITWGHT,BM_NUMBER,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH)" +
                    " Values('" + ViewState["zero_index"].ToString() + "','" + ViewState["zero_index"].ToString() + "','','" + tsaid.Text + "',0,0,"+lblNumber.Text.Trim()+",0,0,0)";
                list_sql.Add(sql_temp);
                DBCallCommon.ExecuteTrans(list_sql);
            }
            else if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 1)//存在虚拟部件X.0,及子物料
            {
                string sql_top_zero_index = "select top 1 BM_XUHAO from " + tablename + " where BM_ENGID like '" + taskid_split[0] + "-%' and BM_XUHAO like '" + ViewState["zero_index"].ToString() + "%' order by dbo.f_formatstr(BM_XUHAO, '.') desc";
                System.Data.DataTable dt_top = DBCallCommon.GetDTUsingSqlText(sql_top_zero_index);
                string[] aa = dt_top.Rows[0][0].ToString().Split('.');
                ViewState["virtualPartNumsNow"] = Convert.ToInt32(aa[aa.Length - 1].ToString()) + 1;
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
                    tablename = "TBPM_STRINFOHZY";
                    viewtablename = "View_TM_HZY";
                    break;
                case "球、立磨":
                    tablename = "TBPM_STRINFOQLM";
                    viewtablename = "View_TM_QLM";
                    break;
                case "篦冷机":
                    tablename = "TBPM_STRINFOBLJ";
                    viewtablename = "View_TM_BLJ";
                    break;
                case "堆取料机":
                    tablename = "TBPM_STRINFODQLJ";
                    viewtablename = "View_TM_DQLJ";
                    break;
                case "钢结构及非标":
                    tablename = "TBPM_STRINFOGFB";
                    viewtablename = "View_TM_GFB";
                    break;
                case "电气及其他":
                    tablename = "TBPM_STRINFODQO";
                    viewtablename = "View_TM_DQO";
                    break;
                default: break;
            }
            #endregion
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
            dt.Columns.Add("BM_KU");//库
            dt.Columns.Add("BM_TOTALNUMBER");//总数量
            dt.Columns.Add("BM_PNUMBER");//计划量
            dt.Columns.Add("BM_MPMY");//计划面域
            dt.Columns.Add("BM_MSXUHAO");//明细序号
            dt.Columns.Add("BM_XUHAO");//序号
            dt.Columns.Add("BM_ISMANU");
            dt.Columns.Add("BM_PARTOWNWGHT");//部件自重
            #endregion
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
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
                newRow[34] = ((HtmlInputText)gRow.FindControl("total_shuliang")).Value.Trim();
                newRow[35] = ((HtmlInputText)gRow.FindControl("plan_shuliang")).Value.Trim();

                newRow[36] = ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim();

                newRow[37] = ((HtmlInputText)gRow.FindControl("txtMSXuhao")).Value.Trim();
                newRow[38] = ((HtmlInputText)gRow.FindControl("txtXuhao")).Value.Trim();
                newRow[39] = ((DropDownList)gRow.FindControl("ddlIsManu")).SelectedValue;

                newRow[40] = ((HtmlInputText)gRow.FindControl("bjzz")).Value.Trim();

                dt.Rows.Add(newRow);
            }
            for (int i = GridView1.Rows.Count; i < 15; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[30] = "N";
                newRow[31]="N";
                newRow[32]="Y";
                newRow[39] = "Y";
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
            if (istid.Value == "1")//相当于确定
            {
                System.Data.DataTable dt = this.GetDataFromGrid();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gRow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow[30] = "N";
                        newRow[31] = "N";
                        newRow[32] = "Y";
                        newRow[39] = "Y";
                        dt.Rows.InsertAt(newRow, i + 1 + count);
                        ///////////dt.Rows.RemoveAt(dt.Rows.Count-1);
                        count++;
                    }
                }
                istid.Value = "0";
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
                ////InitGridview();
            }
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
                System.Data.DataTable dt=this.GetDataFromGrid();
                for (int i = int.Parse(txtid.Value)-1; i < GridView1.Rows.Count; i++)
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
            }
        }

        /// <summary>
        /// 原始数据中的保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            string ret=this.CheckMarNotBelongToMar();
            if (ret=="0")//检查无误
            {
                btnsave.Visible = false;
                List<string> list_sql = new List<string>();
                GetListName();
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
                        tuhao = ((HtmlInputText)gRow.FindControl("tuhao")).Value;
                        marid = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();

                        _xuhao = ((HtmlInputText)gRow.FindControl("txtXuhao")).Value.Trim();
                        _msxuhao = ((HtmlInputText)gRow.FindControl("txtMSXuhao")).Value.Trim();
                        _ismanum = ((DropDownList)gRow.FindControl("ddlIsManu")).SelectedValue;

                        ch_name = ((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                        en_name = ((HtmlInputText)gRow.FindControl("en_name")).Value.Trim();
                        guige = ((HtmlInputText)gRow.FindControl("guige")).Value.Trim();
                        cailiaoname = ((HtmlInputText)gRow.FindControl("cailiaoname")).Value.Trim();
                        cailiaoguige = ((HtmlInputText)gRow.FindControl("cailiaoguige")).Value.Trim();
                        cailiaocd = float.Parse(((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim()==""?"0":((HtmlInputText)gRow.FindControl("cailiaocd")).Value.Trim());
                        cailiaokd = float.Parse(((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaokd")).Value.Trim());
                        //单台数量
                        signshuliang = float.Parse(((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim() == "" ? "1" : ((HtmlInputText)gRow.FindControl("shuliang")).Value.Trim());
                        //总数量
                        shuliang = signshuliang*Convert.ToInt16(lblNumber.Text.Trim());
                        p_shuliang = float.Parse(((HtmlInputText)gRow.FindControl("plan_shuliang")).Value.Trim() == "" ? shuliang.ToString() : ((HtmlInputText)gRow.FindControl("plan_shuliang")).Value.Trim());
                        lilunzhl = float.Parse(((HtmlInputText)gRow.FindControl("lilunzhl")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("lilunzhl")).Value.Trim());
                        //材料单重
                        cailiaodzh = float.Parse(((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaodzh")).Value.Trim());
                        //材料总重
                        cailiaozongzhong = float.Parse(((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim()==""?"0":((HtmlInputText)gRow.FindControl("cailiaozongzhong")).Value.Trim());
                        cailiaozongchang = float.Parse(((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("cailiaozongchang")).Value.Trim());
                        bgzmy = float.Parse(((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("bgzmy")).Value.Trim());
                        mpmy = float.Parse(((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("mpmy")).Value.Trim());
                        
                        tjsx = ((HtmlInputText)gRow.FindControl("tjsx")).Value.Trim();
                        caizhi = ((HtmlInputText)gRow.FindControl("caizhi")).Value.Trim();

                        //部件自重
                        bjzz = float.Parse(((HtmlInputText)gRow.FindControl("bjzz")).Value.Trim() == "" ? "0" : ((HtmlInputText)gRow.FindControl("bjzz")).Value.Trim());

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
                            dzh = bjzz;
                            //总重
                            zongzhong = bjzz*shuliang;
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
                btnsave.Visible = true;
                GridView1.DataSource = this.AfterSavedReload();
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:原始数据已保存!\\r\\r影响行数:" + insertcount + "');", true);/////window.location.reload();
            }
            else if (ret.Contains("Page-"))//页面上存在底层材料归属
            {
                string[] aa = ret.Split('-'); 
                string outxuhao=aa[aa.Length-1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\""+outxuhao+"\"与页面上其父序均为底层材料！！！');", true);
            }
            else if (ret.Contains("DataBase-"))//页面上记录与数据库中存在归属
            {
                string[] aa = ret.Split('-'); 
                string outxuhao=aa[aa.Length-1].ToString();
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
            else if (ret.Contains("XuHaoExisted"))//输入部件和物料时要检查序号是否存在
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上序号\"" + outxuhao + "\"对应序号在数据库中已存在！！！');", true);
            }
            else if (ret.Contains("FormError-"))//总序格式错误
            {
                string[] aa = ret.Split('-');
                string firstchar = aa[aa.Length - 2].ToString();
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r页面上总序或序号\"" + outxuhao + "\"格式错误！！！\\r\\r正确格式：\"" + firstchar + "\"或\"" + firstchar + ".m...（m为正整数）');", true);
            }
            else if (ret.Contains("FatherNotExist-"))//父序不存在
            {
                string[] aa = ret.Split('-');
                string outxuhao = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r序号\"" + outxuhao + "\"的父序不存在！！！');", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r填入的【序号:\"" + outxuhao + "\"】在页面上有多条！！！');", true);
            }
            else if (ret.Contains("总序在数据库中已存在-"))//总序在数据库中已存在，并且物料编码不同
            {
                string[] aa = ret.Split('-');
                string outzongxu = aa[aa.Length - 1].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r填入的【总序:\"" + outzongxu + "\"】在数据库中已存在，且与页面上相同总序对应的物料编码不同！！！');", true);
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
            dt.Columns.Add("BM_KU");//库
            dt.Columns.Add("BM_TOTALNUMBER");//34
            dt.Columns.Add("BM_PNUMBER");//计划数量
            dt.Columns.Add("BM_MPMY");//计划面域
            dt.Columns.Add("BM_MSXUHAO");//明细序号
            dt.Columns.Add("BM_XUHAO");//序号
            dt.Columns.Add("BM_ISMANU");
            dt.Columns.Add("BM_PARTOWNWGHT");
            #endregion
            for (int i = 0; i < 15; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[30] = "N";
                newRow[31]="N";
                newRow[32]="Y";
                newRow[39] = "Y";
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
            string whInZ = _ismanum;

            sqlText = "";
            sqlText = "insert into " + tablename + " ";
            sqlText += "(BM_MSXUHAO,BM_XUHAO,BM_TUHAO,BM_MARID,BM_ZONGXU,BM_CHANAME,BM_ENGSHNAME,BM_GUIGE,BM_PJID,BM_ENGID,";//8
            sqlText += "BM_MALENGTH,BM_MAWIDTH,BM_NUMBER,BM_SINGNUMBER,BM_PNUMBER,BM_MAUNITWGHT,BM_MATOTALWGHT,";//5
            sqlText += "BM_MATOTALLGTH,BM_MABGZMY,BM_UNITWGHT,BM_TOTALWGHT,";//4
            sqlText += "BM_MASHAPE,BM_MASTATE,BM_FILLMAN,";//3
            sqlText += "BM_PROCESS,BM_NOTE,BM_KEYCOMS,BM_FIXEDSIZE,";//4
            sqlText += "BM_TUUNITWGHT,BM_TUTOTALWGHT,BM_TUMAQLTY,BM_TUPROBLEM,BM_TUSTAD,BM_ISMANU,BM_BEIZHUATR,BM_WMARPLAN,BM_KU,BM_MPMY) values ";//8

            sqlText += "('"+_msxuhao+"','"+_xuhao+"','" + tuhao + "','"+marid+"','" + zongxu + "','" + ch_name + "','" + en_name + "','" + guige + "','" + pro_id.Value + "','" + tsaid.Text + "',";
            sqlText += "'" + cailiaocd + "','" + cailiaokd + "','" + shuliang + "','"+signshuliang+"','"+p_shuliang+"','" + cailiaodzh + "','" + cailiaozongzhong + "',";
            sqlText += "'" + cailiaozongchang + "','" + bgzmy + "','" + dzh + "','" + zongzhong + "',";
            sqlText += "'" + xinzhuang + "','" + zhuangtai + "','" + Session["UserID"] + "',";
            sqlText += "'"+process+"','" + beizhu + "','"+ddlKeyComponents+"','"+ddlFixedSize+"',";
            sqlText += ""+tudz+","+tuzongzhong+",'"+tucz+"','"+tuwt+"','"+tustrd+"','"+whInZ+"','"+tjsx+"','"+ddlwmp+"','"+ku+"',"+mpmy+")";

            list.Add(sqlText);

            sqlText = "";
            sqlText = "insert into TBPM_TEMPMARDATA(BM_ZONGXU,BM_XUHAO,BM_MARID,BM_ENGID,BM_MAUNITWGHT,BM_UNITWGHT,BM_TUUNITWGHT,BM_NUMBER,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH)" +
                " Values('" + zongxu + "','"+_xuhao+"','" + marid + "','" + tsaid.Text + "'," + cailiaozongzhong/p_shuliang + "," + dzh + ","+tudz+"," + shuliang + "," + cailiaocd + "," + cailiaokd + "," + cailiaocd + "*1.05)";//*1.05对钢板是不适用的，对钢板而言不存在材料总长（单个）
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

            string[] a=tsaid.Text.Split('-');
            ///////////////////string firstCharofZX=a[a.Length-1];
            ///////////////////string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";
            string pattern2 = @"^" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}$";
            Regex rgx=new Regex(pattern);
            Regex rgx2=new Regex(pattern2);
            string ret = "0";
            List<string> list_sql = new List<string>();
            //对于重复总序及序号的检查
            ArrayList array_marid_zongxu_name = new ArrayList();
            ArrayList array_xuhao = new ArrayList();
            int index = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                string id=((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                string zx=((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                string mc=((HtmlInputText)gRow.FindControl("ch_name")).Value.Trim();
                string xh = ((HtmlInputText)gRow.FindControl("txtXuhao")).Value.Trim();

                if (zx != ""&&xh!="")
                {
                    array_marid_zongxu_name.Add(id + "," + zx);
                    index++;
                }

                if (xh != "")
                {
                    array_xuhao.Add(xh);
                }

                if(zx!="")//数据库中是否存在该总序
                {
                    string sql_zx = "select count(BM_ZONGXU) from " + tablename + " where BM_ENGID LIKE '" + tsaid.Text.Trim().Split('-')[0] + "-%' AND BM_ZONGXU='" + zx + "' AND BM_MARID!='"+id+"'";
                    if (DBCallCommon.GetDTUsingSqlText(sql_zx).Rows[0][0].ToString() != "0")
                    {
                        return "总序在数据库中已存在-"+zx;
                    }
                }

            }

            if (index > 1)
            {
                for (int i = 0; i < array_marid_zongxu_name.Count-1; i++)
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

            //检验总序、序号格式
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                zongxu = ((HtmlInputText)gRow.FindControl("zongxu")).Value.Trim();
                xuhao = ((HtmlInputText)gRow.FindControl("txtXuhao")).Value.Trim();
                if (zongxu != "")
                {
                    string mar = ((System.Web.UI.WebControls.TextBox)gRow.FindControl("marid")).Text.Trim();
                    if (!rgx.IsMatch(zongxu))
                    {
                        ret = "FormError-" + firstCharofZX + "-" + zongxu;
                        return ret;
                    }
                    else if (!rgx.IsMatch(xuhao))
                    {
                        ret = "FormError-" + firstCharofZX + "-" + xuhao;
                        return ret;
                    }
                    else
                    {
                        list_sql.Add("insert into TBPM_TEMPORGDATA(BM_ZONGXU,BM_MARID,BM_ENGID) Values('" + xuhao + "','" + mar + "','" + tsaid.Text.Trim() + "')");
                    }
                }
            }

            //序号格式验证（1、与数据库中是否重复；2、与页面上序号是否重复；3、页面上序号是否重复）
            #region
            int j_index_xuhao=array_xuhao.Count;
            int i_index_xuhao=j_index_xuhao-1;
            for (int i = 0; i < i_index_xuhao; i++)
            {
                //数据库中序号是否存在
                string sql = "select * from " + tablename + " where BM_ENGID LIKE '" + tsaid.Text.Trim().Split('-')[0] + "-%' AND BM_XUHAO='" + array_xuhao[i] + "'";
                SqlDataReader dr_sql = DBCallCommon.GetDRUsingSqlText(sql);
                
                if (dr_sql.HasRows)
                {
                    dr_sql.Close();
                    return "序号已存在-" + array_xuhao[i];
                }
                else
                {
                    //页面上序号是否重复
                    for (int j = i + 1; j < j_index_xuhao; j++)
                    {
                        if (array_xuhao[i].ToString() == array_xuhao[j].ToString())
                        {
                            ret = "页面上序号重复-" + array_xuhao[i].ToString();
                            return ret;
                        }
                    }
                }
            }

            if (array_xuhao.Count > 0)
            {
                //数据库中序号是否存在
                string sql = "select * from " + tablename + " where BM_ENGID LIKE '" + tsaid.Text.Trim().Split('-')[0] + "-%' AND BM_XUHAO='" + array_xuhao[array_xuhao.Count-1] + "'";
                SqlDataReader dr_sql = DBCallCommon.GetDRUsingSqlText(sql);

                if (dr_sql.HasRows)
                {
                    dr_sql.Close();
                    return "序号已存在-" + array_xuhao[array_xuhao.Count-1];
                }
            }
            #endregion

            //检验总序输入临时表
            DBCallCommon.ExecuteTrans(list_sql);
            //检验归属关系
            ParamsCheckMarNotBelongToMar pcmar = new ParamsCheckMarNotBelongToMar();
            this.GetListName();
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
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_CheckMarNotBelongToMarMSBOM]");
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

            TM_BasicFun.HiddenShowColumn(GridView1, cklHiddenShow, Index, "MsOrgInputMode");
        }
    }
}
