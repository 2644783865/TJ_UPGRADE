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
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_HZY_pur : System.Web.UI.Page
    {
        string sqlText;
        string tablename;
        string viewtable;
        string mptable;
        string viewMpTable;
        string strutablenm = "TBPM_STRINFODQO";
        string[] fields;
        //private double sum1 = 0;//重量(汇总前)
        private double sum2 = 0;//重量（汇总后）
        //private double sumnum1 = 0;//数量(汇总前)
        private double sumnum2 = 0;//数量（汇总后）

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_concel.Attributes.Add("OnClick", "Javascript:return confirm('确定取消,执行此操作将取消选中的材料计划?');");
            btn_concel.Click += new EventHandler(btn_concel_Click);
            if (!IsPostBack)
            {
                btn_gosh.Attributes.Add("style", "display:none");
                InitInfo();
            }
            if (IsPostBack)
            {
                if (GridView1.Visible)//只有详细信息分页
                {
                    InitVarMS();
                }
            }
        }
        /// <summary>
        /// DataPanel
        /// </summary>
        private void InitDataPanel(GridView grv)
        {
            if (grv.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
                CheckBox2.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = false;
                grv.Visible = true;
            }
        }
        /// <summary>
        /// 根据QueryString初始化页面信息
        /// </summary>
        protected void InitInfo()
        {
            if (Request.QueryString["id"] != null) //批号 KI/AZ/1-1.JSB MP/WPP/01
            {
                mp_no.Value = Request.QueryString["id"];
                fields = Request.QueryString["id"].ToString().Split('.');
                tsa_id.Text = fields[0].ToString();
                status.Value = "0";// 审核状态 审核状态 初始化为0，1为保存，2为提交，3为一级驳回，4为一级通过，5为二级驳回，6为二级通过，7为三级驳回，8为三级通过
                btn_concel.Visible = true;
                CheckBox2.Visible = true;
                btnreturn.Text = "返回继续勾选";
            }

            #region
            else
            {
                if (Request.QueryString["mpdetail_id"] != null)//未提交、审核中或审核通过进入   批号 KI/AZ/1-1.JSB MP/WPP/01.6
                {
                    fields = Request.QueryString["mpdetail_id"].ToString().Split('.');
                    tsa_id.Text = fields[0].ToString();
                    mp_no.Value = fields[0].ToString() + '.' + fields[1].ToString();
                    status.Value = fields[2].ToString();
                    btn_concel.Visible = true;
                    CheckBox2.Visible = true;
                    if (CommonFun.ComTryInt(status.Value) > 1) //已提交审核
                    {
                        Response.Redirect("TM_MP_Require_Audit.aspx?id=" + mp_no.Value);
                    }
                }
                else if (Request.QueryString["mpedit_id"] != null)//驳回后进入   批号 KI/AZ/1-1.JSB MP/WPP/01.7
                {
                    fields = Request.QueryString["mpedit_id"].ToString().Split('.');
                    tsa_id.Text = fields[0].ToString();
                    mp_no.Value = fields[0].ToString() + '.' + fields[1].ToString();
                    status.Value = fields[2].ToString();
                    btn_concel.Visible = true;
                    CheckBox2.Visible = true;
                }
            }
            #endregion

            sqlText = "select TSA_PJID,TSA_ENGNAME,CM_PROJ ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                lab_contract.Text = dr[0].ToString();
                lab_engname.Text = dr[1].ToString();
                lab_proname.Text = dr[2].ToString();
            }
            dr.Close();
            this.InitList();
            this.GetNameData(ddlMarName);
            this.GetCaiZhiData(ddlMarCaiZhi);
            this.GetGuiGeData(ddlMarGuiGe);
            UCPagingMS.CurrentPage = 1;
            InitVarMS();
            bindGridMS();
        }
        /// <summary>
        /// 初始化表名(材料计划表1/6)
        /// </summary>
        private void InitList()
        {
            fields = mp_no.Value.Split('.');
            string[] cols = fields[1].ToString().Split('/');
            if (cols[0].ToString().Length > 6)   //变更
            {
                tablename = "TBPM_MPCHANGE";
                mptable = "TBPM_MPCHANGERVW";
                viewtable = "View_TM_MPCHANGE";
                viewMpTable = "View_TM_MPCHANGERVW";
            }
            else
            {
                tablename = "TBPM_MPPLAN";
                mptable = "TBPM_MPFORALLRVW";
                viewtable = "View_TM_MPPLAN";
                viewMpTable = "View_TM_MPFORALLRVW";
            }
        }
        /// <summary>
        /// 返回详细信息的查询条件
        /// </summary>
        private string GetSearchList()
        {
            this.InitList();
            //获取类别
            string wherecond = "";

            if (ddlmpName.SelectedItem.Text.Trim() == "标准件")
            {
                wherecond = " and MP_MASHAPE='采' ";
            }
            else if (ddlmpName.SelectedItem.Text.Trim() == "钢材")
            {
                wherecond = " and (MP_MASHAPE='板' or MP_MASHAPE='型' or MP_MASHAPE='圆') ";
            }
            else if (ddlmpName.SelectedItem.Text.Trim() == "铸锻件")
            {
                wherecond = "and ( MP_MASHAPE='锻' or MP_MASHAPE='铸')";
            }
            else if (ddlmpName.SelectedItem.Text.Trim() == "采购成品")
            {
                wherecond = " and MP_MASHAPE  = '采购成品' ";
            }
            else if (ddlmpName.SelectedItem.Text.Trim() == "非金属")
            {
                wherecond = " and MP_MASHAPE  = '非金属' ";
            }

            //材料名称
            if (ddlMarName.SelectedIndex != 0)
            {
                wherecond += " and MP_NAME='" + ddlMarName.SelectedValue + "'";
            }
            //材料规格
            if (ddlMarGuiGe.SelectedIndex != 0)
            {
                wherecond += " and MP_GUIGE='" + ddlMarGuiGe.SelectedValue + "'";
            }
            //材料材质
            if (ddlMarCaiZhi.SelectedIndex != 0)
            {
                wherecond += " and MP_CAIZHI='" + ddlMarCaiZhi.SelectedValue + "'";
            }

            if (ckbShowZero.Checked)
            {
                wherecond += " and (MP_YONGLIANG='0' or MP_YONGLIANG='') ";
            }

            //根据批号查询数据
            string sql_select_detail = " MP_PID='" + mp_no.Value + "'";

            //string sql_select_type = "select MP_MASHAPE from "+mptable+" where MP_ID='"+mp_no.Value+"' ";
            //string matype="";//提交的该批材料计划类别：非定尺板、定尺板、型材、标(发运)、标(组装)
            //SqlDataReader dr=DBCallCommon.GetDRUsingSqlText(sql_select_type);

            //if(dr.HasRows)
            //{
            //    dr.Read();
            //    matype=dr["MP_MASHAPE"].ToString();
            //    dr.Close();
            //}

            //if (matype == "非定尺板")
            //{
            //    GridView1.Columns[13].HeaderText = "张数";//数量》》张数
            //    GridView1.Columns[13].Visible = false;//如果是非定尺不显示数量列
            //}
            //else if (matype == "定尺板")
            //{
            //    GridView1.Columns[13].HeaderText = "张数";//数量》》张数
            //}
            //else if (matype == "型材")
            //{
            //    GridView1.Columns[13].HeaderText = "根数";//数量》》总长
            //}
            //else if (matype == "标(发运)")
            //{
            //    ;
            //}
            //else if (matype == "标(组装)")
            //{
            //    ;
            //}

            //if (matype != "")
            //{
            sql_select_detail += " " + wherecond + "";
            //}

            if (IsPostBack)
            {
                udqOrg.ExistedConditions = sql_select_detail;
                sql_select_detail += UserDefinedQueryConditions.ReturnQueryString((GridView)udqOrg.FindControl("GridView1"), (Label)udqOrg.FindControl("Label1"));
            }
            return sql_select_detail;
        }
        /// <summary>
        /// 绑定汇总后数据（页面上汇总）
        /// </summary>
        private void GetCollList(string ddlselectedType)
        {
            this.InitList();
            string sql_select_type = "select MP_MASHAPE from " + mptable + " where MP_ID='" + mp_no.Value + "' ";
            string matype = "";//提交的该批材料计划类别：非定尺板、定尺板、型材、标(发运)、标(组装)
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_select_type);

            if (dr.HasRows)
            {
                dr.Read();
                matype = dr["MP_MASHAPE"].ToString();
                dr.Close();
            }



            DataTable dt = new DataTable();
            if (viewtable == "View_TM_MPCHANGE")  //变更
            {

            }
            else  //正常
            {

                try
                {
                    SqlConnection sqlConn = new SqlConnection();
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                    DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "TM_MP_COLLECT");
                    DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Table_Name", viewtable, SqlDbType.VarChar, 1000);
                    DBCallCommon.AddParameterToStoredProc(sqlCmd, "@PiHao", mp_no.Value, SqlDbType.VarChar, 1000);
                    DBCallCommon.AddParameterToStoredProc(sqlCmd, "@WhereCon", ddlselectedType, SqlDbType.VarChar, 1000);

                    dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                    sqlConn.Close();
                }
                catch (Exception)
                {
                    throw;
                }

            }


            GridView2.DataSource = dt;
            GridView2.DataBind();


            InitDataPanel(GridView2);
        }
        /// <summary>
        /// 查看详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rad_detail_CheckedChanged(object sender, EventArgs e)
        {
            HyperLink1.Visible = true;
            div_show_zero.Visible = true;

            btnCheck.Visible = true;
            if (this.GetNumbersOfCurrentLot())
            {
                this.btnSaveCheckVisible();

                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView2.Visible = false;

                GridView1.Visible = true;
                ddlmpName.SelectedIndex = 0;
                ddlMarName.SelectedIndex = 0;
                ddlMarCaiZhi.SelectedIndex = 0;
                ddlMarGuiGe.SelectedIndex = 0;


                btn_concel.Visible = true;
                CheckBox2.Visible = true;

                UCPagingMS.CurrentPage = 1;
                InitVarMS();
                bindGridMS();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录！！！');", true);
            }
        }
        /// <summary>
        /// 查看汇总信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rad_sum_CheckedChanged(object sender, EventArgs e)
        {
            HyperLink1.Visible = false;
            div_show_zero.Visible = false;
            if (this.GetNumbersOfCurrentLot())
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                UCPagingMS.Visible = false;
                GridView1.Visible = false;
                GridView2.Visible = true;
                ddlmpName.SelectedIndex = 0;
                ddlMarName.SelectedIndex = 0;
                ddlMarGuiGe.SelectedIndex = 0;
                ddlMarCaiZhi.SelectedIndex = 0;
                btnSaveCheckVisible();

                GetCollList("");
                InitDataPanel(GridView2);

                btn_concel.Visible = false;
                CheckBox2.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录,无法汇总！！！');", true);
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                if (CheckBox2.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        /// <summary>
        /// 取消选中的材料计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_concel_Click(object sender, EventArgs e)
        {
            this.InitList();
            string sqltext = "";
            string xuhao = "";
            int temp = 0;
            temp = checkselect();
            List<string> sqltextlist = new List<string>();
            if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择任何数据,本次操作无效！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择了已提交的数据,不能再做修改,本次操作无效！');", true);
            }
            else
            {
                for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
                {
                    GridViewRow grow = GridView1.Rows[j];
                    CheckBox cbox = (CheckBox)grow.FindControl("CheckBox1");
                    if (cbox.Checked == true)
                    {
                        xuhao = grow.Cells[2].Text;//序号
                        if (!mp_no.Value.Contains(" MPQX/"))  //对于取消的批号，不进行原始数据操作
                        {
                            sqltext = "update  " + strutablenm + "  set BM_MPSTATE='0',BM_MPREVIEW='0'  " +
                                 "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO='" + xuhao + "'";
                            sqltextlist.Add(sqltext);
                        }

                        sqltext = "delete from  " + tablename + "  " +
                                  "where MP_PID='" + mp_no.Value + "' and MP_ENGID='" + tsa_id.Text + "' and  MP_NEWXUHAO='" + xuhao + "'";
                        sqltextlist.Add(sqltext);

                    }
                }

                if (sqltextlist.Count > 0)
                {
                    sqltext = "exec verify '" + tablename + "','MP_PID','" + mptable + "','MP_ID','" + mp_no.Value + "'";
                    sqltextlist.Add(sqltext);

                    DBCallCommon.ExecuteTrans(sqltextlist);

                    UCPagingMS.CurrentPage = 1;
                    InitVarMS();
                    bindGridMS();
                    CheckBox2.Checked = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要取消的记录！');", true);
                }

            }
        }
        /// <summary>
        /// 取消整批计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelAll_OnClick(object sender, EventArgs e)
        {
            this.InitList();
            //@Mpno varchar(100),--批号
            //@OrgTable varchar(50),--原始数据表
            //@MpRvwTable varchar(50),--材料计划审核表
            //@MpDetailTable varchar(50),--材料计划明细表
            //@Engid varchar(50)--生产制号

            string sql = "exec PRO_TM_CancelAllMpByMpno '" + mp_no.Value.Trim() + "','" + strutablenm + "','" + mptable + "','" + tablename + "','" + tsa_id.Text.Trim() + "'";
            DBCallCommon.ExeSqlText(sql);
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            NoDataPanel.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
        }
        private int checkselect()
        {
            int temp = 0;
            int i = 0;
            int k = 0;
            for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
            {
                GridViewRow grow = GridView1.Rows[j];
                CheckBox cbox = (CheckBox)grow.FindControl("CheckBox1");
                if (cbox.Checked == true)
                {
                    i++;
                    if (status.Value == "2" || status.Value == "4" || status.Value == "6" || status.Value == "8")
                    {
                        k++;
                        break;
                    }
                }
            }
            if (i == 0)
            {
                temp = 1;
            }
            else if (k > 0)
            {
                temp = 2;
            }
            return temp;
        }

        /// <summary>
        /// 进入审核界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_gosh_Click(object sender, EventArgs e)
        {
            this.InitList();

            string sql_check_num = "select * from " + viewtable + " where MP_PID='" + mp_no.Value + "' AND  (MP_YONGLIANG='0' or MP_YONGLIANG='' or (MP_PURCUNIT in ('in','T') and (MP_WEIGHT='0' or MP_WEIGHT='')))";
            System.Data.SqlClient.SqlDataReader dr_sql_check_num = DBCallCommon.GetDRUsingSqlText(sql_check_num);

            if (dr_sql_check_num.HasRows)
            {
                dr_sql_check_num.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法保存！\\r\\r存在材料用量为零的项，无法采购！！！\\r\\r请选择【不可提交计划项】，取消后修改计划！！！');", true);
                return;
            }
            dr_sql_check_num.Close();
            string prjEng = this.GetPorjEngName(tsa_id.Text);
            //需要更新的

            string mp_tracknum = "";//计划跟踪号

            string hd_mp_note = "";//隐藏的原来的备注
            string new_mp_note = "";//新的备注

            //更新条件
            string mp_pid = mp_no.Value;//批号
            string mp_tuhao = "";//图号
            string mp_marid = "";//材料ID
            double mp_length = 0;//长度
            double mp_wdth = 0;//宽度
            string mp_fixedsize = "";//定尺
            string ku;
            string sqltext = "";
            List<string> list_sql = new List<string>();
            this.InitList();
            ////查询出该批材料计划类型
            //string sql_select_type = "select MP_MASHAPE from " + mptable + " where MP_ID='" + mp_no.Value + "' ";
            //string matype = "";//提交的该批材料计划类别：非定尺板、定尺板、型材、标(发运)、标(组装)
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_select_type);

            //if (dr.HasRows)
            //{
            //    dr.Read();
            //    matype = dr["MP_MASHAPE"].ToString();
            //    dr.Close();
            //}



            //
            fields = mp_no.Value.Split('.');
            string[] cols = fields[1].ToString().Split('/');
            string pici;



            //确定要更新的表
            if (cols[0].ToString().Trim().Length > 6)//变更
            {
                pici = "BG" + cols[2].ToString().PadLeft(2, '0');
                mptable = "TBPM_MPCHANGERVW";
                tablename = "TBPM_MPCHANGE";
            }
            else  //正常
            {
                pici = cols[1].ToString().PadLeft(2, '0');
                mptable = "TBPM_MPFORALLRVW";
            }

            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                int j = i + 1;
                GridViewRow grow = GridView2.Rows[i];
                string matype = ((Label)grow.Cells[13].FindControl("Lbshape")).Text.Trim();

                mp_tracknum = Regex.Replace(prjEng + '_' + tsa_id.Text + '_' + mp_pid.Split('.')[1] + '_' + j.ToString().PadLeft(4, '0'), @"[\n\r]", "");//计划跟踪号，去除回车

                hd_mp_note = ((HtmlInputHidden)grow.FindControl("hidNote")).Value;
                new_mp_note = ((HtmlInputText)grow.FindControl("txtBeiZhu")).Value;
                //更新条件
                //   mp_tuhao = grow.Cells[1].Text.Trim() == "&nbsp;" ? "" : grow.Cells[1].Text.Trim();//图号
                mp_tuhao = ((Label)grow.FindControl("lblTuhao")).Text == "&nbsp;" ? "" : ((Label)grow.FindControl("lblTuhao")).Text;
                mp_marid = grow.Cells[2].Text;//材料ID
                mp_fixedsize = ((Label)grow.FindControl("lblsfdc2")).Text.Trim();//定尺
                mp_length = Convert.ToDouble(grow.Cells[6].Text.Trim() == "&nbsp;" ? "0" : grow.Cells[6].Text);
                mp_wdth = Convert.ToDouble(grow.Cells[7].Text.Trim() == "&nbsp;" ? "0" : grow.Cells[7].Text);
                ku = grow.Cells[15].Text;
                if (hd_mp_note == "-" || hd_mp_note == "&nbsp;")
                {
                    hd_mp_note = "";
                }

                //明细更新
                if (matype == "板" && mp_fixedsize == "N")
                {
                    sqltext = "update " + tablename + " set MP_TRACKNUM='" + mp_tracknum + "',MP_ALLBEIZHU='" + new_mp_note + "'";
                    sqltext += " where MP_PID='" + mp_pid + "' and MP_MARID='" + mp_marid + "'  and MP_FIXEDSIZE='" + mp_fixedsize + "'";
                }
                else if (matype == "板" && mp_fixedsize == "Y")
                {
                    sqltext = "update " + tablename + " set MP_TRACKNUM='" + mp_tracknum + "',MP_ALLBEIZHU='" + new_mp_note + "'";
                    sqltext += " where MP_PID='" + mp_pid + "'  and MP_MARID='" + mp_marid + "' and MP_LENGTH=" + mp_length + " and MP_WIDTH=" + mp_wdth + "  and MP_FIXEDSIZE='" + mp_fixedsize + "' and MP_TUHAO='" + mp_tuhao + "'";
                }
                else if (matype == "型" || matype == "圆")
                {
                    if (mp_fixedsize == "Y")
                    {
                        sqltext = "update " + tablename + " set MP_TRACKNUM='" + mp_tracknum + "',MP_ALLBEIZHU='" + new_mp_note + "'";
                        sqltext += " where MP_PID='" + mp_pid + "' and MP_MARID='" + mp_marid + "' and MP_FIXEDSIZE='" + mp_fixedsize + "' and  MP_LENGTH=" + mp_length + "  and MP_FIXEDSIZE='" + mp_fixedsize + "'";
                    }
                    else
                    {
                        sqltext = "update " + tablename + " set MP_TRACKNUM='" + mp_tracknum + "',MP_ALLBEIZHU='" + new_mp_note + "'";
                        sqltext += " where MP_PID='" + mp_pid + "' and MP_MARID='" + mp_marid + "' and MP_FIXEDSIZE='" + mp_fixedsize + "'  and MP_FIXEDSIZE='" + mp_fixedsize + "'";
                    }
                }
                else if (matype == "非")
                {
                    sqltext = "update " + tablename + " set MP_TRACKNUM='" + mp_tracknum + "',MP_ALLBEIZHU='" + new_mp_note + "'";
                    sqltext += " where MP_PID='" + mp_pid + "'  and MP_MARID='" + mp_marid + "'   and MP_ALLBEIZHU='" + hd_mp_note + "'";
                }
                else if (matype == "采")
                {
                    if (ku == "S")
                    {
                        sqltext = "update " + tablename + " set MP_TRACKNUM='" + mp_tracknum + "',MP_ALLBEIZHU='" + new_mp_note + "'";
                        sqltext += " where MP_PID='" + mp_pid + "'  and MP_MARID='" + mp_marid + "' and (MP_KU like '%S%' or MP_KU like '%s%' )  and MP_ALLBEIZHU='" + hd_mp_note + "'";
                    }
                    else
                    {
                        sqltext = "update " + tablename + " set MP_TRACKNUM='" + mp_tracknum + "',MP_ALLBEIZHU='" + new_mp_note + "'";
                        sqltext += " where MP_PID='" + mp_pid + "'  and MP_MARID='" + mp_marid + "' and (MP_KU not like '%S%' or MP_KU not like '%s%' )   and MP_ALLBEIZHU='" + hd_mp_note + "'";
                    }
                }
                else
                {
                    sqltext = "update " + tablename + " set MP_TRACKNUM='" + mp_tracknum + "',MP_ALLBEIZHU='" + new_mp_note + "'";
                    sqltext += " where MP_PID='" + mp_pid + "'  and MP_MARID='" + mp_marid + "'   and MP_ALLBEIZHU='" + hd_mp_note + "' and MP_TUHAO='" + mp_tuhao + "'";
                }
                list_sql.Add(sqltext);
            }

            //根据审核状态更新材料计划审核表：此语句的主要目的是针对提交和驳回后的处理
            if (status.Value == "0" || status.Value == "1" || status.Value == "3" || status.Value == "5" || status.Value == "7")
            {
                sqlText = "update " + mptable + " set MP_STATE='1',MP_CHECKLEVEL='3',";
                sqlText += "MP_SUBMITTM='',";
                sqlText += "MP_REVIEWAADVC='',MP_REVIEWATIME='',MP_REVIEWB='',";
                sqlText += "MP_REVIEWBADVC='',MP_REVIEWBTIME='',";
                sqlText += "MP_REVIEWC='',MP_REVIEWCADVC='',";
                sqlText += "MP_REVIEWCTIME='',MP_ADATE='' ";
                sqlText += " where MP_ID='" + mp_pid + "' ";
                list_sql.Add(sqlText);
            }
            DBCallCommon.ExecuteTrans(list_sql);
            Response.Redirect("TM_MP_Require_Audit.aspx?id=" + mp_no.Value);
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Response.Redirect("TM_Task_View.aspx?action=" + tsa_id.Text.Trim());
            }
            else
            {
                Response.Redirect("TM_MP_Require_View.aspx?plan_id=" + tsa_id.Text.Trim());
            }
            //////////ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "history.go(-2);", true);
        }
        /// <summary>
        /// 按类查询（汇总或明细）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.GetNumbersOfCurrentLot())
            {
                this.InitList();
                this.btnSaveCheckVisible();
                string wherecond = "";

                if (rad_sum.Checked)//汇总信息
                {
                    CheckBox2.Visible = false;
                    //材料类别
                    if (ddlmpName.SelectedItem.Text.Trim() == "钢材")
                    {
                        wherecond = " and ( MP_MASHAPE= '板' or MP_MASHAPE='型' or MP_MASHAPE='圆' ) ";
                    }
                    else if (ddlmpName.SelectedItem.Text.Trim() == "标准件")
                    {
                        wherecond = " and MP_MASHAPE='采' ";
                    }
                    else if (ddlmpName.SelectedItem.Text.Trim() == "铸锻件")
                    {
                        wherecond = " and (MP_MASHAPE='锻'or MP_MASHAPE='铸' )";
                    }
                    else if (ddlmpName.SelectedItem.Text.Trim() == "非金属")
                    {
                        wherecond = " and MP_MASHAPE='非'";
                    }
                    else if (ddlmpName.SelectedItem.Text.Trim() == "采购成品")
                    {
                        wherecond = " and MP_MASHAPE ='采购成品'";
                    }
                    //材料名称
                    if (ddlMarName.SelectedIndex != 0)
                    {
                        wherecond += " and MP_NAME='" + ddlMarName.SelectedValue + "'";
                    }
                    //材料规格
                    if (ddlMarGuiGe.SelectedIndex != 0)
                    {
                        wherecond += " and MP_GUIGE='" + ddlMarGuiGe.SelectedValue + "'";
                    }
                    //材料材质
                    if (ddlMarCaiZhi.SelectedIndex != 0)
                    {
                        wherecond += " and MP_CAIZHI='" + ddlMarCaiZhi.SelectedValue + "'";
                    }

                    this.GetCollList(wherecond);
                }
                else//详细信息
                {
                    CheckBox2.Visible = true;
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                    UCPagingMS.CurrentPage = 1;
                    InitVarMS();
                    bindGridMS();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录,无法查询！！！');", true);
            }
        }

        ///// <summary>
        ///// 数据汇总（汇总后）:RowDataBind()
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowIndex >= 0)
        //    {
        //        sum2 += Convert.ToDouble(e.Row.Cells[9].Text == "" ? "0" : e.Row.Cells[9].Text);
        //        sumnum2 += Convert.ToDouble(e.Row.Cells[10].Text == "" ? "0" : e.Row.Cells[10].Text);
        //    }
        //    else if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        e.Row.CssClass = "notbrk";
        //        e.Row.Cells[7].Text = "合计：";
        //        if (ddlmpName.SelectedItem.Text.Trim() == "全部")
        //        {
        //            e.Row.Cells[8].Text = "";
        //        }
        //        else if (ddlmpName.SelectedItem.Text.Trim() == "黑色金属")
        //        {
        //            e.Row.Cells[8].Text = "kg";
        //        }
        //        e.Row.Cells[9].Text = sum2.ToString();
        //        e.Row.Cells[10].Text = sumnum2.ToString();
        //    }
        //}

        /// <summary>
        /// 下推审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            Response.Redirect("TM_MP_Require_Audit.aspx?id=" + mp_no.Value);
        }

        /// <summary>
        /// 保存按钮和下推审核按钮的可见性控制
        /// </summary>
        /// <param name="checkstate"></param>
        private void btnSaveCheckVisible()
        {
            if (ddlmpName.SelectedIndex == 0 && ddlMarName.SelectedIndex == 0 && ddlMarGuiGe.SelectedIndex == 0 && ddlMarCaiZhi.SelectedIndex == 0 && rad_sum.Checked)
            {

                btnCheck.Visible = true;
            }
            else
            {

                btnCheck.Visible = false;
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqOrg.FindControl("GridView1"));
        }

        #region
        PagerQueryParam pager_ms = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVarMS()
        {
            InitPagerMS();
            UCPagingMS.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
            UCPagingMS.PageSize = pager_ms.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPagerMS()
        {
            InitList();
            pager_ms.TableName = viewtable;
            pager_ms.PrimaryKey = "MP_ID";
            pager_ms.ShowFields = "MP_NEWXUHAO,MP_ZONGXU,MP_TUHAO,MP_MARID,MP_NAME,MP_GUIGE,MP_CAIZHI,MP_LENGTH,MP_WIDTH,MP_TECHUNIT,MP_WEIGHT,MP_NUMBER,MP_STANDARD,MP_FIXEDSIZE,MP_KEYCOMS,MP_TIMERQ,MP_TYPE,MP_ENVREFFCT,MP_USAGE,MP_ALLBEIZHU,MP_MASHAPE,MP_TRACKNUM,MP_MIANYU,MP_YONGLIANG,MP_KU";
            pager_ms.OrderField = "MP_TRACKNUM,MP_MARID";
            pager_ms.StrWhere = this.GetSearchList();
            pager_ms.OrderType = 0;//升序排列
            pager_ms.PageSize = 50;
        }
        void Pager_PageChangedMS(int pageNumber)
        {
            bindGridMS();
        }
        private void bindGridMS()
        {
            pager_ms.PageIndex = UCPagingMS.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_ms);
            CommonFun.Paging(dt, GridView1, UCPagingMS, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPagingMS.Visible = false;
            }
            else
            {
                UCPagingMS.Visible = true;
                UCPagingMS.InitPageInfo();  //分页控件中要显示的控件
            }

        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定材料名称
        /// </summary>
        private void GetNameData(DropDownList clname)
        {
            string sqltext = "";
            sqltext = "select distinct MP_NAME collate  Chinese_PRC_CS_AS_KS_WS as MP_NAME from " + viewtable + " ";
            sqltext += "where MP_PID='" + mp_no.Value + "' and MP_NAME is not null order by MP_NAME collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "MP_NAME";
            string dataValue = "MP_NAME";
            DBCallCommon.BindDdl(clname, sqltext, dataText, dataValue);
        }
        /// <summary>
        /// 绑定材料规格
        /// </summary>
        private void GetGuiGeData(DropDownList clname)
        {
            string sqltext = "";
            sqltext = "select distinct MP_GUIGE collate  Chinese_PRC_CS_AS_KS_WS as MP_GUIGE from " + viewtable + " ";
            sqltext += "where MP_PID='" + mp_no.Value + "' and MP_GUIGE is not null order by MP_GUIGE collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "MP_GUIGE";
            string dataValue = "MP_GUIGE";
            DBCallCommon.BindDdl(clname, sqltext, dataText, dataValue);
        }
        /// <summary>
        /// 绑定材料材质
        /// </summary>
        private void GetCaiZhiData(DropDownList clname)
        {
            string sqltext = "";
            sqltext = "select distinct MP_CAIZHI collate  Chinese_PRC_CS_AS_KS_WS as MP_CAIZHI from " + viewtable + " ";
            sqltext += "where MP_PID='" + mp_no.Value + "' and MP_CAIZHI is not null order by MP_CAIZHI collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "MP_CAIZHI";
            string dataValue = "MP_CAIZHI";
            DBCallCommon.BindDdl(clname, sqltext, dataText, dataValue);
        }
        /// <summary>
        /// 获取当前批的数据条数
        /// </summary>
        /// <returns></returns>
        private bool GetNumbersOfCurrentLot()
        {
            this.InitList();
            string sql_select = "select count(*) as Num from " + viewtable + " where MP_PID='" + mp_no.Value + "'";
            DataTable dt_sql_select = DBCallCommon.GetDTUsingSqlText(sql_select);
            if (Convert.ToInt16(dt_sql_select.Rows[0]["Num"].ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据生产制号查询项目工程，返回：项目(项目简称)_工程名称
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        private string GetPorjEngName(string taskid)
        {
            string retVal = "";
            //  string sql = "select CM_PROJ+'('+TSA_PJID+')_'+TSA_ENGNAME AS ProjEng from View_TM_TaskAssign where TSA_ID='" + taskid + "'";
            string sql = "select CM_PROJ+'('+TSA_PJID+')_'+MP_CHILDENGNAME AS ProjEng from " + viewMpTable + " where MP_ID='" + mp_no.Value + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            dr.Read();
            retVal = dr["ProjEng"].ToString();
            dr.Close();
            return retVal;
        }
        #endregion
    }
}
