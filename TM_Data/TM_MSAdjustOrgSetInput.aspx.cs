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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MSAdjustOrgSetInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Image)Master.FindControl("Image3")).Visible = false;
                this.PageInit();
            }
        }

        protected void PageInit()
        {
            string taskid = Request.QueryString["TaskID"].ToString();
            string viewtable = Request.QueryString["view_table"].ToString();
            string xuhao = Request.QueryString["Xuhao"].ToString();

            ////////////string taskid = "SR/ZHJ/1-1";
            ////////////string viewtable = "View_TM_DQLJ";
            ////////////string xuhao = "1.20.55";
            ViewState["TaskID"] = taskid;
            ViewState["TaskIDSplit"] = taskid.Split('-')[0];
            ViewState["view_table"] = viewtable;
            ViewState["Xuhao"] = xuhao;


            this.GetListName();
            //读取台数
            string sql = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" + taskid.Split('-')[0] + "'";
            System.Data.SqlClient.SqlDataReader dr_number = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_number.HasRows)
            {
                dr_number.Read();
                lblNumber.Text = dr_number["TSA_NUMBER"].ToString();
                dr_number.Close();
            }

            string curentCheckResult = this.CheckCurernt();
            if (curentCheckResult != "OK")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", curentCheckResult, true);
                return;
            }

            if (xuhao != "undefined")
            {
                txtXuHao.Text = xuhao;
                ddlSplitNums.Enabled = false;
                txtXuHao.Enabled = false;
                this.InitBindData();
            }
        }
        /// <summary>
        /// 对当前数据能否拆分的检查
        /// </summary>
        /// <returns></returns>
        protected string CheckCurernt()
        {
            /*************
             * 1、已提交明细部件不能拆分
             * 2、变更删除部件不能拆分
             * ***********/
            string sql_1 = "select BM_XUHAO from " + ViewState["view_table"] + " where BM_TASKID='" + ViewState["TaskIDSplit"].ToString() + "' AND BM_XUHAO='" + ViewState["Xuhao"].ToString() + "' AND BM_MSSTATE='1'";
            System.Data.SqlClient.SqlDataReader dr_1 = DBCallCommon.GetDRUsingSqlText(sql_1);
            if (dr_1.HasRows)
            {
                return @"alert('序号【" + ViewState["Xuhao"].ToString() + "】明细已提交，无法拆分！！！')";
            }
            dr_1.Close();

            string sql_2 = "select BM_XUHAO from " + ViewState["view_table"] + " where BM_TASKID='" + ViewState["TaskIDSplit"].ToString() + "' AND BM_XUHAO='" + ViewState["Xuhao"].ToString() + "' AND (BM_MSSTATUS='1' or BM_MPSTATUS='1' or BM_OSSTATUS='1')";
            System.Data.SqlClient.SqlDataReader dr_2 = DBCallCommon.GetDRUsingSqlText(sql_2);
            if (dr_2.HasRows)
            {
                return @"alert('序号【" + ViewState["Xuhao"].ToString() + "】为变更删除记录，无法拆分！！！')";
            }
            dr_2.Close();
            return "OK";
        }

        /// <summary>
        /// 初始化绑定信息
        /// </summary>
        protected void InitBindData()
        {
            string sql = "select *,cast(BM_MABGZMY as varchar)+' | '+cast(BM_MPMY as varchar) as BM_MY,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER  from " + ViewState["view_table"].ToString() + " where BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_XUHAO='" + txtXuHao.Text.Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            
            if (dt.Rows.Count > 0)
            {
                grvOrgData.DataSource = dt;
                grvOrgData.DataBind();
                NoDataPanel.Visible = false;

                int splitnumber = Convert.ToInt16(dt.Rows[0]["BM_SINGNUMBER"].ToString());
                ViewState["splitnumber"] = splitnumber;
                this.BindDllSplitNums(splitnumber);

                if (splitnumber > 1)
                {
                    lblErrorText.Text = "";
                }
                else
                {
                    lblErrorText.Text = "数量小于1，无法进行成套拆分!";
                }
            }
            else
            {
                NoDataPanel.Visible = true;
                grvOrgData.DataSource = null;
                grvOrgData.DataBind();

            }
        }

        protected void BindDllSplitNums(int number)
        {
            ddlSplitNums.Items.Clear();

            for (int i = 2;i <=number;i++)
            {
                ddlSplitNums.Items.Add(new ListItem("-"+i.ToString()+"-",i.ToString()));
            }

            if (ddlSplitNums.Items.Count > 0)
            {
                ddlSplitNums.SelectedIndex = 0;
            }
            else
            {
                ddlSplitNums.Items.Add(new ListItem("-1-", "1"));
                ddlSplitNums.SelectedIndex = 0;
            }

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

        protected void txtXuHao_OnTextChanged(object sender, EventArgs e)
        {
            ViewState["Xuhao"] = txtXuHao.Text.Trim();
            string curentCheckResult = this.CheckCurernt();
            if (curentCheckResult != "OK")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", curentCheckResult, true);
                return;
            }
            this.InitBindData();
            this.BindSplitTo();
        }

        protected void ddlSplitNums_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (grvOrgData.Rows.Count > 0)
            {
                this.BindSplitTo();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ddlSplitNums.Enabled = true;

            NoDataPanel.Visible = true;
            grvOrgData.DataSource = null;
            grvOrgData.DataBind();

            txtXuHao.Enabled = true;

            GridView1.DataSource = null;
            GridView1.DataBind();

            btnSplit.Visible = false;
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            lblErrorText.Text = "";
            ViewState["Xuhao"] = txtXuHao.Text.Trim();
            string curentCheckResult = this.CheckCurernt();
            if (curentCheckResult != "OK")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", curentCheckResult, true);
                return;
            }
            this.InitBindData();
        }
        /// <summary>
        /// 能否拆分检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_OnClick(object sender, EventArgs e)
        {
            btnSplit.Visible = false;
            if (txtXuHao.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入【序号】！！！');", true);
                return;
            }

            if (grvOrgData.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录，无法操作！！！\\r\\r提示:请输入序号后，点击查询再进行操作');", true);
                return;
            }

            if (ddlSplitNums.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数量为1，无法进行成套拆分！！！');", true);
                return;
            }

            string inputxuhao = txtXuHao.Text.Trim();
            string xiansxuhao = grvOrgData.Rows[0].Cells[0].Text.Trim();

            if (inputxuhao!=xiansxuhao)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入序号【"+inputxuhao+"】与显示序号【"+xiansxuhao+"】不一致！！！');", true);
                return;
            }

            string[] number_array = grvOrgData.Rows[0].Cells[7].Text.Trim().Split('|');
            float singnumber = Convert.ToInt16(number_array[0]);
            float number = Convert.ToInt16(number_array[1]);
            float totalnumber = Convert.ToInt16(number_array[2]);

            if (singnumber ==1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('【单台数量】为1，无法拆分！！！');", true);
                return;
            }

            if (number % singnumber != 0 || totalnumber%singnumber!=0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('【单台数量】与【总数量、计划数量】不成比例，无法拆分！！！');", true);
                return;
            }

            if (number != totalnumber)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('【总数量】与【计划数量】不等，无法拆分！！！');", true);
                return;
            }

            //检查语句（子项中的单重数量、总数量、计划数量检查）

            string sql_check = "select BM_XUHAO from " + ViewState["tablename"].ToString() + " where BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_XUHAO LIKE '" + txtXuHao.Text.Trim() + ".%' and (cast(BM_SINGNUMBER as int)%" + singnumber + "!=0 or cast(BM_NUMBER as int)%" + number + "!=0 or cast(BM_PNUMBER as int)%" + totalnumber + "!=0 or cast(BM_NUMBER as int)%" + singnumber + "!=0 or cast(BM_PNUMBER as int)%" + singnumber + "!=0 or BM_PNUMBER!=BM_NUMBER or cast(BM_NUMBER as int)%cast(BM_SINGNUMBER as int)!=0 or cast(BM_NUMBER as int)%cast(BM_SINGNUMBER as int)!=0)";

            ViewState["sql_check"] = " BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_XUHAO LIKE '" + txtXuHao.Text.Trim() + ".%' and (cast(BM_SINGNUMBER as int)%" + singnumber + "!=0 or cast(BM_NUMBER as int)%" + number + "!=0 or cast(BM_PNUMBER as int)%" + totalnumber + "!=0 or cast(BM_NUMBER as int)%" + singnumber + "!=0 or cast(BM_PNUMBER as int)%" + singnumber + "!=0 or BM_PNUMBER!=BM_NUMBER or cast(BM_NUMBER as int)%cast(BM_SINGNUMBER as int)!=0 or cast(BM_NUMBER as int)%cast(BM_SINGNUMBER as int)!=0)";
            LinkButton1.Visible = true;
            DataTable dt=DBCallCommon.GetDTUsingSqlText(sql_check);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('存在数量不成比例项，无法拆分！！！\\r\\r提示:请点击【不可拆分项查看】按钮，查看无法拆分原因');", true);
                return;
            }
            this.BindSplitTo();
            btnSplit.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该记录可拆分！！！\\r\\r提示:输入【序号】及【数量】后，点击【开始拆分】按钮执行拆分！！！');", true);

        }
        /// <summary>
        /// 根据拆分数目绑定GridView行
        /// </summary>
        protected void BindSplitTo()
        {
            btnSplit.Visible = false;

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("BM_XUHAO");//序号
            dt.Columns.Add("BM_SINGNUMBER");//数量

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)gRow.FindControl("txtNewXuhao")).Text.Trim();
                newRow[1] = ((TextBox)gRow.FindControl("txtNewShuling")).Text.Trim();
                dt.Rows.Add(newRow);
            }

            int spnum = Convert.ToInt16(ddlSplitNums.SelectedValue);
            for (int i = GridView1.Rows.Count; i < spnum; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = grvOrgData.Rows[0].Cells[0].Text;
                newRow[1] = "1";
                dt.Rows.Add(newRow);
            }

            while(spnum<dt.Rows.Count)
            {
                dt.Rows.RemoveAt(dt.Rows.Count-1);
            }

            dt.AcceptChanges();

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        /// <summary>
        /// 开始拆分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSplit_OnClick(object sender, EventArgs e)
        {
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";
            Regex rgx = new Regex(pattern);

            string _xuhao = "";
            string _allxuhao = "";
            int real_num = Convert.ToInt16(ViewState["splitnumber"].ToString());
            int _real_num = 0;
            int _sum_real_num=0;
            string _error = "";

            foreach (GridViewRow grow in GridView1.Rows)
            {
                _xuhao = ((TextBox)grow.FindControl("txtNewXuhao")).Text.Trim();
                _allxuhao += _xuhao + "|";
                _real_num =Convert.ToInt16(((TextBox)grow.FindControl("txtNewShuling")).Text.Trim());
                _sum_real_num += _real_num;
                if(!rgx.IsMatch(_xuhao))
                {
                    _error = @"alert('第【" + (grow.RowIndex + 1).ToString() + "】行,请输入正确的序号格式！！！');";
                    break;
                }
            }

            if (_error != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", _error, true);
            }
            else if (real_num != _sum_real_num)
            {
                string __error = @"alert('无法拆分！！！\r\r拆分后，单台数量之和【" + _sum_real_num + "】不等于原始单台数量【" + real_num + "】');";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", __error, true);
            }
            else
            {
               string retvalue=this.CheckXuhao(_allxuhao);
               if (retvalue == "OK")
               {
                   try
                   {
                       List<string> list_sql = new List<string>();
                       string bm_id;//数量
                       string bm_zongxu;//序号
                       foreach (GridViewRow grow in GridView1.Rows)
                       {
                           bm_id = ((TextBox)grow.FindControl("txtNewShuling")).Text.Trim();
                           bm_zongxu = ((TextBox)grow.FindControl("txtNewXuhao")).Text.Trim();
                           string sql_insert = "insert into TBPM_TEMPORGDATA([BM_ID],[BM_ZONGXU],[BM_MARID],[BM_ENGID]) VALUES('" + bm_id + "','" + bm_zongxu + "','0','" + ViewState["TaskID"].ToString()+ "')";
                           list_sql.Add(sql_insert);
                       }
                       DBCallCommon.ExecuteTrans(list_sql);
                       this.ExecSplit();

                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已保存！！！');okay();window.close();", true);
                   }
                   catch(Exception)
                   {
                       retvalue = @"alert('未知错误，请联系管理员！！！');";
                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", retvalue, true);
                   }
               }
               else
               {
                   ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", retvalue, true);
                   return;
               }
            }
        }
        /// <summary>
        /// 不可拆分项查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_OnClick(object sender, EventArgs e)
        {
            if (grvOrgData.Rows.Count > 0)
            {
                string message = ViewState["view_table"] + "$" + ViewState["sql_check"].ToString();
                message = message.Replace("'", "^");
                string sql_insert = "insert into TBPM_TEMPWHERECONDITION(USERID,WhereCondition) values('" + Session["UserID"].ToString() + "','" + message + "')";
                DBCallCommon.ExeSqlText(sql_insert);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "View_Split();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录！！！');", true);
            }
        }
        /// <summary>
        /// 查看全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton2_OnClick(object sender, EventArgs e)
        {
            if (grvOrgData.Rows.Count > 0)
            {
                string message = ViewState["view_table"] + "$" + "BM_ENGID='" + ViewState["TaskID"] + "' AND (BM_XUHAO='" + ViewState["Xuhao"].ToString() + "' OR BM_XUHAO LIKE '" + ViewState["Xuhao"].ToString() + ".%')"; 
                message = message.Replace("'", "^");
                string sql_insert = "insert into TBPM_TEMPWHERECONDITION(USERID,WhereCondition) values('" + Session["UserID"].ToString() + "','" + message + "')";
                DBCallCommon.ExeSqlText(sql_insert);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "View_Split();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有记录！！！');", true);
            }
        }

        /// <summary>
        /// 序号检查，返回错误值
        /// </summary>
        /// <returns></returns>
        protected string CheckXuhao(string str_allxuhao)
        {
            str_allxuhao = str_allxuhao.Substring(0, str_allxuhao.Length - 1);//去掉最后一个“|”
            string[] array_xuhao = str_allxuhao.Split('|');

            //A------------------
            if (grvOrgData.Rows.Count == 0)
            {
                return @"alert('没有记录，无法操作！！！\r\r提示:请输入序号后，点击查询再进行操作');";
            }
            //A------------------

            //B------------------
            if (ddlSplitNums.Items.Count == 0)
            {
                return @"alert('数量为1，无法进行成套拆分！！！');";
            }
            //B------------------
            string inputxuhao = txtXuHao.Text.Trim();
            string xiansxuhao = grvOrgData.Rows[0].Cells[0].Text.Trim();
            if (inputxuhao != xiansxuhao)
            {
                return @"alert('输入序号【" + inputxuhao + "】与显示序号【" + xiansxuhao + "】不一致！！！');";
            }
            //B------------------

            //C------------------
            string[] number_array = grvOrgData.Rows[0].Cells[7].Text.Trim().Split('|');
            float singnumber = Convert.ToInt16(number_array[0]);
            float number = Convert.ToInt16(number_array[1]);
            float totalnumber = Convert.ToInt16(number_array[2]);

            if (singnumber == 1)
            {
                return @"alert('【单台数量】为1，无法拆分！！！');";
            }

            if (number % singnumber != 0 || totalnumber % singnumber != 0)
            {
                return @"alert('【单台数量】与【总数量、计划数量】不成比例，无法拆分！！！');";
            }

            if (number != totalnumber)
            {
                return @"alert('【总数量】与【计划数量】不等，无法拆分！！！');";
            }

            //检查语句（子项中的单重数量、总数量、计划数量检查）
            string sql_check = "select BM_XUHAO from " + ViewState["tablename"].ToString() + " where BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_XUHAO LIKE '" + txtXuHao.Text.Trim() + ".%' and (cast(BM_SINGNUMBER as int)%" + singnumber + "!=0 or cast(BM_NUMBER as int)%" + number + "!=0 or cast(BM_PNUMBER as int)%" + totalnumber + "!=0 or cast(BM_NUMBER as int)%" + singnumber + "!=0 or cast(BM_PNUMBER as int)%" + singnumber + "!=0 or BM_PNUMBER!=BM_NUMBER or cast(BM_NUMBER as int)%cast(BM_SINGNUMBER as int)!=0 or cast(BM_NUMBER as int)%cast(BM_SINGNUMBER as int)!=0)";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_check);
            if (dt.Rows.Count > 0)
            {
                return @"alert('存在数量不成比例项，无法拆分！！！\r\r提示:请点击【不可拆分项查看】按钮，查看无法拆分原因');";
            }
            //C-------------------

            /****************************
             *1、页面上序号不能存在相同记录
             *2、页面上序号必须同级
            ***************************/
            int array_count=array_xuhao.Length;
            for (int i = 0; i < array_count-1; i++)
            {
                for (int j = i + 1; j < array_count; j++)
                {
                    if(array_xuhao[i]==array_xuhao[j])
                    {
                        return @"alert('页面上存在相同序号【"+array_xuhao[i]+"】');";
                    }
                    int a = array_xuhao[i].Split('.').Length;
                    int b = array_xuhao[j].Split('.').Length;
                    if (array_xuhao[i].Split('.').Length != array_xuhao[j].Split('.').Length)
                    {
                        return @"alert('序号【" + array_xuhao[i] + "】与序号【" + array_xuhao[j] + "】不是同一级');";
                    }
                }
            }

            /*******************************************
             * 1、与数据库中序号不重复
             * 2、不能归属到除开01.08与01.11的其他物料上
             * 3、不能归属到已提交记录上
             * 4、不能归属到变更删除部件上
             * ****************************************/
            foreach (string xh in array_xuhao)
            {
                //1
                if (xh != ViewState["Xuhao"].ToString())
                {
                    string sql_1 = "select BM_XUHAO from " + ViewState["tablename"] + " where BM_TASKID='" + ViewState["TaskIDSplit"].ToString() + "' AND (BM_XUHAO='" + xh + "' OR BM_XUHAO LIKE '" + xh + ".%')";
                    System.Data.SqlClient.SqlDataReader dr_1 = DBCallCommon.GetDRUsingSqlText(sql_1);
                    if (dr_1.HasRows)
                    {
                        return @"alert('【" + xh + "】在数据库中已存在或其下级已存在！！！');";
                    }
                    dr_1.Close();
                }
                //2
                string fj = xh.Substring(0, xh.LastIndexOf('.'));
                string sql_2 = "select BM_XUHAO from " + ViewState["tablename"] + " where BM_TASKID='" + ViewState["TaskIDSplit"].ToString() + "' AND BM_XUHAO='" + fj + "' AND BM_XUHAO!='" + ViewState["Xuhao"].ToString() + "' and BM_MARID!='' and (BM_MARID not like '01.08.%' or BM_MARID not like '01.11.%') ";
                System.Data.SqlClient.SqlDataReader dr_2 = DBCallCommon.GetDRUsingSqlText(sql_2);
                if (dr_2.HasRows)
                {
                    return @"alert('序号【" + xh + "】父级【"+fj+"】存在物料编码！！！');";
                }
                dr_2.Close();
                //3
                string sql_3 = "select BM_XUHAO from " + ViewState["tablename"] + " where BM_TASKID='" + ViewState["TaskIDSplit"].ToString() + "' AND BM_XUHAO='" + fj + "' AND BM_XUHAO!='" + ViewState["Xuhao"].ToString() + "' and BM_MSSTATE!='0' and BM_XUHAO!='1'";
                System.Data.SqlClient.SqlDataReader dr_3 = DBCallCommon.GetDRUsingSqlText(sql_3);
                if (dr_3.HasRows)
                {
                    return @"alert('序号【" + xh + "】父级【" + fj + "】已提交！！！');";
                }
                dr_3.Close();
                //4
                string sql_4 = "select BM_XUHAO from " + ViewState["tablename"] + " where BM_TASKID='" + ViewState["TaskIDSplit"].ToString() + "' AND BM_XUHAO='" + fj + "' AND BM_XUHAO!='" + ViewState["Xuhao"].ToString() + "' and (BM_MSSTATUS='1' OR BM_MPSTATUS='1' OR BM_OSSTATUS='1')";
                System.Data.SqlClient.SqlDataReader dr_4 = DBCallCommon.GetDRUsingSqlText(sql_4);
                if (dr_4.HasRows)
                {
                    return @"alert('序号【" + xh + "】父级【" + fj + "】为变更删除记录！！！');";
                }
                dr_3.Close();
            }
            return "OK";
        }
        /// <summary>
        /// 调用存储过程执行拆分
        /// </summary>
        protected void ExecSplit()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "[PRO_TM_OrgSplitofSets]");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@BM_XUHAO",ViewState["Xuhao"].ToString() , SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@TableName", ViewState["tablename"].ToString(), SqlDbType.Text, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@BM_ENGID", ViewState["TaskID"].ToString(), SqlDbType.Text, 1000);
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
