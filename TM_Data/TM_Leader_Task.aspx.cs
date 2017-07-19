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
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Leader_Task : System.Web.UI.Page
    {
        string tablename;
        string fields;
        string sqlText;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
              
              
                RBLBind();
                SignTask();
                InitVar();
                InitInfo();
                this.SingUndo();

            }
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            ReGetTechAuditData();
        }

        /// <summary>
        /// 动态添加审核状态项(待审核、审核中、通过、驳回、驳回已处理)
        /// </summary>
        private void RBLBind()
        {
            rblstatus.Items.Add(new ListItem("您的审核任务", "0"));
            rblstatus.Items.Add(new ListItem("审核中", "3"));
            rblstatus.Items.Add(new ListItem("通过", "1"));
            rblstatus.Items.Add(new ListItem("驳回", "2"));
            rblstatus.Items.Add(new ListItem("您的驳回任务", "5"));
            rblstatus.Items.Add(new ListItem("驳回已处理", "4"));
            rblstatus.SelectedIndex = 0;
        }

        /// <summary>
        /// 查询任务类型（每次查询时调用）
        /// </summary>
        private void GetTaskTypeData()
        {
            #region
            //类型如果选择为材料计划或未选择(无待审核任务)
            #region 材料计划
            if (rbltask.SelectedValue == "0" || rbltask.SelectedValue == "")
            {
                rbltask.SelectedIndex = 0;
                if (rblstate.SelectedItem.Text.Trim() == "正常")
                {
                    tablename = "View_TM_MPFORALLRVW";
                }
                else
                {
                    tablename = "View_TM_MPCHANGERVW";
                }
                fields = "MP_ID as ID,CM_PROJ as PJNAME,";
                fields += "MP_SUBMITNM as SUBMITNM,MP_SUBMITTM as SUBMITTM,MP_ADATE as ADATE,";
                fields += "MP_REVIEWANAME as REVIEWANAME,MP_REVIEWAADVC as REVIEWAADVC,";
                fields += "MP_REVIEWBNAME as REVIEWBNAME,MP_REVIEWBADVC as REVIEWBADVC,";
                fields += "MP_REVIEWCNAME as REVIEWCNAME,MP_REVIEWCADVC as REVIEWCADVC,";
                fields += "MP_STATE as STATE,MP_MASHAPE as SHAPE,MP_MAP as MAP,MP_CHILDENGNAME as ENGNAME";

                if (rblstatus.SelectedValue == "0")//待当前审核人审核
                {
                    sqlText = "(MP_STATE='2' and MP_REVIEWA='" + Session["UserID"] + "') ";
                    sqlText += "or  (MP_STATE ='4' and MP_REVIEWB='" + Session["UserID"] + "')";
                    sqlText += "or  (MP_STATE ='6' and MP_REVIEWC='" + Session["UserID"] + "')";
                }
                else if (rblstatus.SelectedValue == "1")//通过
                {
                    sqlText = "MP_STATE ='8'";
                }
                else if (rblstatus.SelectedValue == "2")//驳回
                {
                    sqlText = "MP_STATE in ('3','5','7')";
                }
                else if (rblstatus.SelectedValue == "3")//当前审核人在审核中
                {
                    sqlText = "MP_STATE in('2','4','6') and ((MP_STATE='2' and MP_REVIEWA!='" + Session["UserID"] + "') ";
                    sqlText += "or  (MP_STATE='4' and MP_REVIEWB!='" + Session["UserID"] + "')";
                    sqlText += "or  (MP_STATE='6' and MP_REVIEWC!='" + Session["UserID"] + "'))";
                }
                else if (rblstatus.SelectedValue == "4")//驳回已处理
                {
                    sqlText = "MP_STATE ='9'";
                }
                else if (rblstatus.SelectedValue == "5")
                {
                    sqlText = " MP_SUBMITID='" + Session["UserID"] + "' and MP_STATE in ('3','5','7') ";

                }

            
             

                if (ckbTech.Checked)
                {
                    sqlText += " and MP_SUBMITID='" + Session["UserID"].ToString() + "'";
                }

                if (ddlQueryType.SelectedIndex != 0)
                {
                    if (ddlQueryType.SelectedValue == "批号")
                    {
                        sqlText += " and MP_ID like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "合同号")
                    {
                        sqlText += " and TSA_PJID like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "项目名称")
                    {
                        sqlText += " and CM_PROJ like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "设备名称")
                    {
                        sqlText += " and TSA_ENGNAME like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "任务号")
                    {
                        sqlText += " and MP_ENGID like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "技术员")
                    {
                        sqlText += " and MP_SUBMITNM like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                }

                pager.PrimaryKey = "MP_ID";
                pager.OrderField = "cast(MP_SUBMITTM as datetime)";
                GridView1.Columns[5].Visible = true;

                GridView1.Columns[16].Visible = true;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
                GridView1.Columns[19].Visible = false;
                GridView1.Columns[20].Visible = false;
            }
            #endregion
            #region 制作明细
            else if (rbltask.SelectedValue == "1")
            {
                if (rblstate.SelectedItem.Text.Trim() == "正常")
                {
                    tablename = "View_TM_MSFORALLRVW";
                }
                else
                {
                    tablename = "View_TM_MSCHANGERVW";
                }
                fields = "MS_ID as ID,MS_PJNAME as PJNAME,";
                fields += "MS_SUBMITNM as SUBMITNM,MS_SUBMITTM as SUBMITTM,MS_ADATE as ADATE,";
                fields += "MS_REVIEWANAME as REVIEWANAME,MS_REVIEWAADVC as REVIEWAADVC,";
                fields += "MS_REVIEWBNAME as REVIEWBNAME,MS_REVIEWBADVC as REVIEWBADVC,";
                fields += "MS_REVIEWCNAME as REVIEWCNAME,MS_REVIEWCADVC as REVIEWCADVC,MS_MAP as MAP,";
                fields += "MS_STATE as STATE,'' as  SHAPE,MS_CHILDENGNAME as ENGNAME";
                if (rblstatus.SelectedValue == "0")
                {
                    sqlText = "(MS_STATE='2' and MS_REVIEWA='" + Session["UserID"] + "') ";
                    sqlText += "or  (MS_STATE ='4' and MS_REVIEWB='" + Session["UserID"] + "')";
                    sqlText += "or  (MS_STATE ='6' and MS_REVIEWC='" + Session["UserID"] + "')";
                }
                else if (rblstatus.SelectedValue == "1")//通过
                {
                    sqlText = "MS_STATE ='8'";
                }
                else if (rblstatus.SelectedValue == "2")//驳回
                {
                    sqlText = "MS_STATE in ('3','5','7')";
                }
                else if (rblstatus.SelectedValue == "3")//当前审核人审核中
                {
                    sqlText = " MS_STATE in('2','4','6') and ((MS_STATE='2' and MS_REVIEWA!='" + Session["UserID"] + "') ";
                    sqlText += "or  (MS_STATE='4' and MS_REVIEWB!='" + Session["UserID"] + "')";
                    sqlText += "or  (MS_STATE='6' and MS_REVIEWC!='" + Session["UserID"] + "'))";
                }
                else if (rblstatus.SelectedValue == "4")//驳回已处理
                {
                    sqlText = "MS_STATE ='9'";
                }
                else if (rblstatus.SelectedValue == "5")
                {
                    sqlText = " MS_SUBMITID='" + Session["UserID"] + "' and MS_STATE in ('3','5','7') ";

                }


              
           
            
                if (ckbTech.Checked)
                {
                    sqlText += " and MS_SUBMITID='" + Session["UserID"].ToString() + "'";
                }


                if (ddlQueryType.SelectedIndex != 0)
                {
                    if (ddlQueryType.SelectedValue == "批号")
                    {
                        sqlText += " and MS_ID like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                
                    else if (ddlQueryType.SelectedValue == "项目名称")
                    {
                        sqlText += " and MS_PJNAME like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "合同号")
                    {
                        sqlText += " and MS_PJID like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "设备名称")
                    {
                        sqlText += " and MS_ENGNAME like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "任务号")
                    {
                        sqlText += " and MS_ENGID like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "技术员")
                    {
                        sqlText += " and MS_SUBMITNM like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                }

                pager.PrimaryKey = "MS_ID";
                pager.OrderField = "cast(MS_SUBMITTM as datetime)";
                GridView1.Columns[5].Visible = false;

                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = true;
                GridView1.Columns[18].Visible = false;
                GridView1.Columns[19].Visible = false;
                GridView1.Columns[20].Visible = false;
            }
            #endregion
            #region 油漆计划
            else if (rbltask.SelectedValue == "3")
            {
                tablename = "VIEW_TM_PAINTSCHEME";
                fields = "PS_ID as ID,CM_PROJ as PJNAME,TSA_ENGNAME as ENGNAME,";
                fields += "PS_SUBMITNM as SUBMITNM,PS_SUBMITTM as SUBMITTM,PS_ADATE as ADATE,";
                fields += "PS_REVIEWANM as REVIEWANAME,PS_REVIEWAADVC as REVIEWAADVC,";
                fields += "PS_REVIEWBNM as REVIEWBNAME,PS_REVIEWBADVC as REVIEWBADVC,";
                fields += "PS_REVIEWCNM as REVIEWCNAME,PS_REVIEWCADVC as REVIEWCADVC,";
                fields += "PS_STATE as STATE,'' as MAP,'' as SHAPE";
                if (rblstatus.SelectedValue == "0")
                {
                    sqlText = "(PS_STATE='2' and PS_REVIEWA='" + Session["UserID"] + "') ";
                    sqlText += "or  (PS_STATE ='4' and PS_REVIEWB='" + Session["UserID"] + "')";
                    sqlText += "or  (PS_STATE ='6' and PS_REVIEWC='" + Session["UserID"] + "')";
                }
                else if (rblstatus.SelectedValue == "1")//通过
                {
                    sqlText = "PS_STATE ='8'";
                }
                else if (rblstatus.SelectedValue == "2")//驳回
                {
                    sqlText = "PS_STATE in ('3','5','7')";
                }
                else if (rblstatus.SelectedValue == "3")//审核中
                {
                    sqlText = "PS_STATE in('2','4','6') and ((PS_STATE='2' and PS_REVIEWA!='" + Session["UserID"] + "') ";
                    sqlText += "or  (PS_STATE='4' and PS_REVIEWB!='" + Session["UserID"] + "')";
                    sqlText += "or  (PS_STATE='6' and PS_REVIEWC!='" + Session["UserID"] + "'))";
                }
                else if (rblstatus.SelectedValue == "4")//驳回已处理
                {
                    sqlText = "PS_STATE ='9'";
                }
                else if (rblstatus.SelectedValue == "5")
                {
                    sqlText = " PS_SUBMITID='" + Session["UserID"] + "' and PS_STATE in ('3','5','7') ";

                }


             
             
                if (ckbTech.Checked)
                {
                    sqlText += " and PS_SUBMITID='" + Session["UserID"].ToString() + "'";
                }


                if (ddlQueryType.SelectedIndex != 0)
                {
                    if (ddlQueryType.SelectedValue == "批号")
                    {
                        sqlText += " and PS_ID like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "合同号")
                    {
                        sqlText += " and PS_PJID like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "项目名称")
                    {
                        sqlText += " and CM_PROJ like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "设备名称")
                    {
                        sqlText += " and TSA_ENGNAME like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "任务号")
                    {
                        sqlText += " and PS_ENGID like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                    else if (ddlQueryType.SelectedValue == "技术员")
                    {
                        sqlText += " and PS_SUBMITNM like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                }

                pager.PrimaryKey = "PS_ID";
                pager.OrderField = "cast(PS_SUBMITTM as datetime)";
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
                GridView1.Columns[19].Visible = false;
                GridView1.Columns[20].Visible = true;
            }
            #endregion

            if (rblstatus.SelectedValue == "5")
            {
                GridView1.Columns[21].Visible = true;
            }
            else
            {
                GridView1.Columns[21].Visible = false;
            }
            #endregion
        }
        /// <summary>
        /// 标记未完成的任务
        /// </summary>
        private void SingUndo()
        {
            string sql_undo = "";

            /*******************未提交******************************/
            sql_undo = "select sum(num) from (";
            //材料计划
            sql_undo += "select count(*) as num from TBPM_MPFORALLRVW where MP_SUBMITID='" + Session["UserID"].ToString() + "' and MP_STATE in('0','1') ";
            sql_undo += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_SUBMITID='" + Session["UserID"].ToString() + "' and MP_STATE in('0','1') ";
            //制作明细
            sql_undo += " union all select count(*) as num from View_TM_MSFORALLRVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in('0','1') ";
            sql_undo += " union all select count(*) as num from View_TM_MSCHANGERVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in('0','1') ";
            //外协
            sql_undo += " union all select count(*) as num from View_TM_OUTSOURCETOTAL where OST_SUBMITER='" + Session["UserID"].ToString() + "' and OST_STATE in('0','1') ";
            sql_undo += " union all select count(*) as num from View_TM_OUTSCHANGERVW where OST_SUBMITER='" + Session["UserID"].ToString() + "' and OST_STATE in('0','1') ";
            //油漆
            sql_undo += " union all select count(*) as num from VIEW_TM_PAINTSCHEME where PS_SUBMITID='" + Session["UserID"].ToString() + "' and PS_STATE in('0','1') ";

            sql_undo += ") as temp ";

            SqlDataReader dr_usub = DBCallCommon.GetDRUsingSqlText(sql_undo);
            if (dr_usub.Read())
            {
                lblUSub.Text = dr_usub[0].ToString();
            }
            dr_usub.Close();

            /*******************审核中******************************/
            sql_undo = "select sum(num) from (";
            //材料计划
            sql_undo += "select count(*) as num from TBPM_MPFORALLRVW where MP_SUBMITID='" + Session["UserID"].ToString() + "' and MP_STATE in('2','4','6') ";
            sql_undo += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_SUBMITID='" + Session["UserID"].ToString() + "' and MP_STATE in('2','4','6') ";
            //制作明细
            sql_undo += " union all select count(*) as num from View_TM_MSFORALLRVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in('2','4','6') ";
            sql_undo += " union all select count(*) as num from View_TM_MSCHANGERVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in('2','4','6') ";
            //外协
            sql_undo += " union all select count(*) as num from View_TM_OUTSOURCETOTAL where OST_SUBMITER='" + Session["UserID"].ToString() + "' and OST_STATE in('2','4','6') ";
            sql_undo += " union all select count(*) as num from View_TM_OUTSCHANGERVW where OST_SUBMITER='" + Session["UserID"].ToString() + "' and OST_STATE in('2','4','6') ";
            //油漆
            sql_undo += " union all select count(*) as num from VIEW_TM_PAINTSCHEME where PS_SUBMITID='" + Session["UserID"].ToString() + "' and PS_STATE in('2','4','6') ";

            sql_undo += ") as temp ";

            SqlDataReader dr_inrvw = DBCallCommon.GetDRUsingSqlText(sql_undo);
            if (dr_inrvw.Read())
            {
                lblInRvw.Text = dr_inrvw[0].ToString();
            }
            dr_inrvw.Close();
            /*******************驳回******************************/
            sql_undo = "select sum(num) from ( ";
            //材料计划
            sql_undo += "select count(*) as num from TBPM_MPFORALLRVW where MP_SUBMITID='" + Session["UserID"].ToString() + "' and MP_STATE in('3','5','7') ";
            sql_undo += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_SUBMITID='" + Session["UserID"].ToString() + "' and MP_STATE in('3','5','7') ";
            //制作明细
            sql_undo += " union all select count(*) as num from View_TM_MSFORALLRVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in('3','5','7') ";
            sql_undo += " union all select count(*) as num from View_TM_MSCHANGERVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in('3','5','7') ";
            //外协
            sql_undo += " union all select count(*) as num from View_TM_OUTSOURCETOTAL where OST_SUBMITER='" + Session["UserID"].ToString() + "' and OST_STATE in('3','5','7') ";
            sql_undo += " union all select count(*) as num from View_TM_OUTSCHANGERVW where OST_SUBMITER='" + Session["UserID"].ToString() + "' and OST_STATE in('3','5','7') ";
            //油漆
            sql_undo += " union all select count(*) as num from VIEW_TM_PAINTSCHEME where PS_SUBMITID='" + Session["UserID"].ToString() + "' and PS_STATE in('3','5','7') ";

            sql_undo += ") as temp ";

            SqlDataReader dr_rej = DBCallCommon.GetDRUsingSqlText(sql_undo);
            if (dr_rej.Read())
            {
                lblRej.Text = dr_rej[0].ToString();
            }
            dr_rej.Close();
        }

        #region 分页
        //初始化分页信息
        private void InitPager()
        {
            GetTaskTypeData();
            pager.TableName = tablename;
            pager.ShowFields = fields;
            pager.StrWhere = sqlText;
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
        }

        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            GetTechAuditData();
        }

        protected void GetTechAuditData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        protected void ReGetTechAuditData()
        {

            InitPager();
            GetTechAuditData();
        }
        #endregion


        protected void rbltask_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetTechAuditData();
        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetTechAuditData();
        }

        protected void rblstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetTechAuditData();
        }

        protected void ddlpro_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
          
            ReGetTechAuditData();
        }

        protected void ddlEngName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
           
            ReGetTechAuditData();
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetTechAuditData();
        }

        protected void btnClear_btnQuery(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ddlQueryType.SelectedIndex = 0;
            txtQueryContent.Text = "";
           
            ReGetTechAuditData();
        }

        /// <summary>
        /// 标记审核任务(初始化加载时调用)
        /// </summary>
        private void SignTask()
        {
            //所有待审核材料计划
            sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_MPFORALLRVW where MP_REVIEWA='" + Session["UserID"].ToString() + "' and MP_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MPFORALLRVW where MP_REVIEWB='" + Session["UserID"].ToString() + "' and MP_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MPFORALLRVW where MP_REVIEWC='" + Session["UserID"].ToString() + "' and MP_STATE='6' ";
            sqlText += "union all select count(*) as num from TBPM_MPFORALLRVW where MP_SUBMITID='" + Session["UserID"].ToString() + "' and MP_STATE in ('3','5','7') ";
            sqlText += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_REVIEWA='" + Session["UserID"].ToString() + "' and MP_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_REVIEWB='" + Session["UserID"].ToString() + "' and MP_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_REVIEWC='" + Session["UserID"].ToString() + "' and MP_STATE='6' ";
            sqlText += ") as temp ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    rbltask.Items.Add(new ListItem("材料计划", "0"));
                }
                else
                {
                    rbltask.Items.Add(new ListItem("材料计划" + "</label><label><font color=red>(" + dr[0].ToString() + ")</font>", "0"));
                    rbltask.SelectedIndex = 0;
                }
            }
            dr.Close();
            //所有待审核制作明细
            sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_MSFORALLRVW where MS_REVIEWA='" + Session["UserID"].ToString() + "' and MS_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MSFORALLRVW where MS_REVIEWB='" + Session["UserID"].ToString() + "' and MS_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MSFORALLRVW where MS_REVIEWC='" + Session["UserID"].ToString() + "' and MS_STATE='6' ";
            sqlText += "union all select count(*) as num from TBPM_MSFORALLRVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in ('3','5','7')";
            sqlText += " union all select count(*) as num from TBPM_MSCHANGERVW where MS_REVIEWA='" + Session["UserID"].ToString() + "' and MS_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MSCHANGERVW where MS_REVIEWB='" + Session["UserID"].ToString() + "' and MS_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MSCHANGERVW where MS_REVIEWC='" + Session["UserID"].ToString() + "' and MS_STATE='6' ";
            sqlText += "union all select count(*) as num from TBPM_MSCHANGERVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in ('3','5','7') ";
            sqlText += ") as temp ";

            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    rbltask.Items.Add(new ListItem("制作明细", "1"));
                }
                else
                {
                    rbltask.Items.Add(new ListItem("制作明细" + "</label><label><font color=red>(" + dr[0].ToString() + ")</font>", "1"));
                    if (rbltask.SelectedValue != "0")
                    {
                        rbltask.SelectedIndex = 1;
                    }
                }
            }
            dr.Close();
            //待审核油漆计划
            sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_PAINTSCHEME where PS_REVIEWA='" + Session["UserID"].ToString() + "' and PS_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_PAINTSCHEME where PS_REVIEWB='" + Session["UserID"].ToString() + "' and PS_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_PAINTSCHEME where PS_REVIEWC='" + Session["UserID"].ToString() + "' and PS_STATE='6' ";
            sqlText += "union all select count(*) as num from TBPM_PAINTSCHEME where PS_SUBMITID='" + Session["UserID"].ToString() + "' and PS_STATE in ('3','5','7') ";
            sqlText += ") as temp ";

            dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    rbltask.Items.Add(new ListItem("油漆方案", "3"));
                }
                else
                {
                    rbltask.Items.Add(new ListItem("油漆方案" + "</label><label><font color=red> (" + dr[0].ToString() + ")</font>", "3"));
                    if (rbltask.SelectedValue != "0" && rbltask.SelectedValue != "1" && rbltask.SelectedValue != "2")
                    {
                        rbltask.SelectedIndex = 3;
                    }
                }
            }
            dr.Close();

        }


  
  
    

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkbtnCancel_OnClick(object sender, EventArgs e)
        {
            LinkButton linkbtn = (LinkButton)sender;
            string cancelid = linkbtn.CommandArgument;
            string sql = "";
            if (rbltask.SelectedValue == "0")
            {
                sql = "update TBPM_MPFORALLRVW set MP_STATE='9' where MP_ID='" + cancelid + "' ";
            }
            else if (rbltask.SelectedValue == "1")
            {
                if (rblstate.SelectedItem.Text.Trim() == "正常")
                {
                    sql = "update TBPM_MSFORALLRVW set MS_STATE='9' where MS_ID='" + cancelid + "'";
                }
                else
                {
                    sql = "update TBPM_MSCHANGERVW set MS_STATE='9' where MS_ID='" + cancelid + "'";
                }
            }
            else
            {
                sql = "update TBPM_PAINTSCHEME set PS_STATE='9' where PS_ID='" + cancelid + "'";
            }

            DBCallCommon.ExeSqlText(sql);
            if (rbltask.SelectedValue == "0")
            {
                string engid = cancelid.Split('.')[0];
                string sqladd = "update TBPM_STRINFODQO set BM_MPSTATE='0', BM_MPREVIEW='0' where BM_ENGID='" + engid + "' and BM_XUHAO in(select MP_NEWXUHAO from TBPM_MPPLAN where MP_PID='" + cancelid + "')";
                DBCallCommon.ExeSqlText(sqladd);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');document.location.href=document.location.href;", true);
        }
    }
}
