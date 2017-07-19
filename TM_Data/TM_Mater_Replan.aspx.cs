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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Mater_Replan : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInfo();
            }
        }
        /// <summary>
        /// 页面初始化信息
        /// </summary>
        private void PageInfo()
        {
            this.GetProNameData();
            this.GetEngNameData();
            this.GetSbNameData();
            this.GetChdNameData();
            this.GetFillManData();
            this.GetMaterData();
            this.GetPCodeData();

            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询

        }
        /// <summary>
        /// GridView数据绑定
        /// </summary>
        protected void GetBoundData()
        {
            string str = " ";
            //查询项目
            if (ddlProName.SelectedValue != "-请选择-")
            {
                str += " MP_PJID='" + ddlProName.SelectedValue + "' ";
            }

            //查询工程
            if (ddlEngName.SelectedValue != "-请选择-")
            {
                str += " and dbo.GetNoEngid(MP_ENGID,'-')='" + ddlEngName.SelectedValue + "' ";
            }

            //提交类别
            if (ddlMpOutType.SelectedValue != "-请选择-")
            {
                str += this.GetTypeQureyCondition(ddlMpOutType.SelectedValue);
            }

            //常规查询条件
            if (ViewState["QueryType"].ToString() == "Conventional-Inquires")
            {

                //查询设备
                if (ddlSBname.SelectedValue != "-请选择-")
                {
                    str += " and (MP_NEWXUHAO = '" + ddlSBname.SelectedValue + "' or MP_NEWXUHAO like '" + ddlSBname.SelectedValue + ".%') ";
                }

                //查询部件
                if (ddlBJname.SelectedValue != "-请选择-")
                {
                    str += " and (MP_NEWXUHAO = '" + ddlBJname.SelectedValue + "' or MP_NEWXUHAO like '" + ddlBJname.SelectedValue + ".%')";
                }
                //查询技术员
                if (ddlFillman.SelectedValue != "-请选择-")
                {
                    str += " and MP_ENGID='" + ddlFillman.SelectedValue + "' ";
                }
                //材料名称
                if (ddlmatername.SelectedValue != "-请选择-")
                {
                    str += " and MP_NAME='" + ddlmatername.SelectedValue + "' ";
                }
                //材料类别
                if (ddlmptype.SelectedIndex != 0)
                {
                    if (ddlmptype.SelectedValue == "非定尺板")
                    {
                        str += " and MP_MASHAPERV='非定尺板'";
                    }
                    else if (ddlmptype.SelectedValue == "定尺板")
                    {
                        str += " and MP_MASHAPERV='定尺板'";
                    }
                    else if (ddlmptype.SelectedValue == "型材")
                    {
                        str += " and MP_MASHAPERV='型材'";
                    }
                    else if (ddlmptype.SelectedValue == "标准件")
                    {
                        str += " and MP_MASHAPERV in('标(发运)','标(组装)') ";
                    }
                }

                //按批号查询
                if (ddlPCode.SelectedValue != "-请选择-")
                {
                    str += " and MP_PID='" + ddlPCode.SelectedValue + "'";
                }
            }
            //自定义查询条件
            else if (ViewState["QueryType"].ToString() == "User-Defined")
            {
                if (txtQueryContent.Text.Trim() != "")
                {
                    str += " and " + ddlQueryType.SelectedValue + " like '%" + txtQueryContent.Text.Trim() + "%'";
                }
                else
                {
                    str += " and " + ddlQueryType.SelectedValue + "='" + txtQueryContent.Text.Trim() + "'";
                }
            }

            DataTable dt = GetDataByPagerQueryParam(str,"0","");
            
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                NoDataPanel.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                NoDataPanel.Visible = true;
            }
        }
        
        #region DropDownList绑定
        /// <summary>
        /// 绑定项目名称 材料计划视图View_TM_MPPLAN
        /// </summary>
        private void GetProNameData()
        {
            sqlText = "select distinct BM_PJID,BM_PJID+'‖'+CM_PROJ as BM_PJNAME from VWPM_PROSTRC ORDER BY BM_PJID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlProName.DataSource = dt;
            ddlProName.DataTextField = "BM_PJNAME";
            ddlProName.DataValueField = "BM_PJID";
            ddlProName.DataBind();
            ddlProName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlProName.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定工程名称(为不带'-'的工程)
        /// </summary>
        private void GetEngNameData()
        {
            sqlText = "select TSA_ID,TSA_ID+'‖'+TSA_ENGNAME as TSA_ENGNAME from TBPM_TCTSASSGN ";
            sqlText += " where TSA_ID in (select distinct MP_ENGID from View_TM_MPPLAN ";
            sqlText += "where MP_PJID='" + ddlProName.SelectedValue + "' and MP_STATUS='0' and MP_STATERV='8')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlEngName.DataSource = dt;
            ddlEngName.DataTextField = "TSA_ENGNAME";
            ddlEngName.DataValueField = "TSA_ID";
            ddlEngName.DataBind();
            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;
        }
        
        /// <summary>
        /// 绑定设备名称(为最顶层序号，如1、2、3等)，从原始数据中绑定
        /// </summary>
        private void GetSbNameData()
        {
            sqlText = "select distinct BM_CHANAME,BM_XUHAO,dbo.f_FormatSTR(BM_XUHAO,'.') from VWPM_PROSTRC ";
            sqlText += "where BM_ENGID='" + ddlEngName.SelectedValue + "' and dbo.Splitnum(BM_XUHAO,'.')=0 order by dbo.f_FormatSTR(BM_XUHAO,'.')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlSBname.DataSource = dt;
            ddlSBname.DataTextField = "BM_CHANAME";
            ddlSBname.DataValueField = "BM_XUHAO";
            ddlSBname.DataBind();
            ddlSBname.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlSBname.SelectedIndex = 0;
        }
        
        /// <summary>
        /// 绑定技术员，从技术部任务分工中获取技术员
        /// </summary>
        private void GetFillManData()
        {
            sqlText = "select TSA_ID,TSA_TCCLERKNM from View_TM_TaskAssign where TSA_ID='" + ddlEngName.SelectedValue + "' and charindex('-',TSA_ID)>0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlFillman.DataSource = dt;
            ddlFillman.DataTextField = "TSA_TCCLERKNM";
            ddlFillman.DataValueField = "TSA_ID";
            ddlFillman.DataBind();
            ddlFillman.Items.Insert(0, new ListItem("", "-请选择-"));
            ddlFillman.SelectedIndex = 0;
        }
        
        /// <summary>
        /// 绑定部件名称(序号为1.2、1.3等)
        /// </summary>
        private void GetChdNameData()
        {
            sqlText = "select distinct BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS AS BM_CHANAME,BM_XUHAO,dbo.f_FormatSTR(BM_XUHAO,'.') from VWPM_PROSTRC ";
            sqlText += "where BM_ENGID='" + ddlEngName.SelectedValue + "' and dbo.Splitnum(BM_XUHAO,'.')=1 ";
            if (ddlSBname.SelectedValue != "-请选择-")
            {
                sqlText += " and BM_XUHAO like '" + ddlSBname.SelectedValue + ".%'  ";
            }
            sqlText += " order by BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlBJname.DataSource = dt;
            ddlBJname.DataTextField = "BM_CHANAME";
            ddlBJname.DataValueField = "BM_XUHAO";
            ddlBJname.DataBind();
            ddlBJname.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlBJname.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定材料名称
        /// </summary>
        private void GetMaterData()
        {
            sqlText = "select distinct MP_NAME collate  Chinese_PRC_CS_AS_KS_WS AS MP_NAME from View_TM_MPPLAN where MP_NEWXUHAO like '" + ddlBJname.SelectedValue + ".%' and MP_NAME is not null and MP_STATUS='0' and MP_STATERV='8'  order by MP_NAME collate  Chinese_PRC_CS_AS_KS_WS";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlmatername.DataSource = dt;
            ddlmatername.DataTextField = "MP_NAME";
            ddlmatername.DataValueField = "MP_NAME";
            ddlmatername.DataBind();
            ddlmatername.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlmatername.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定批号
        /// </summary>
        private void GetPCodeData()
        {
            sqlText = "select distinct MP_PID from View_TM_MPPLAN where MP_NEWXUHAO like '" + ddlSBname.SelectedValue + ".%' and MP_ENGID='" + ddlEngName.SelectedValue + "' and MP_STATUS='0' and MP_STATERV='8'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlPCode.DataSource = dt;
            ddlPCode.DataTextField = "MP_PID";
            ddlPCode.DataValueField = "MP_PID";
            ddlPCode.DataBind();
            ddlPCode.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlPCode.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 项目名称查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedValue == "-请选择-")
            {
                ddlEngName.SelectedIndex = 0;
                ddlBJname.SelectedIndex = 0;
            }
            else
            {
                GetEngNameData();//绑定工程
            }
        }
        /// <summary>
        /// 工程名称查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEngName.SelectedValue == "-请选择-")
            {
                ddlBJname.SelectedIndex = 0;
            }
            else
            {
                GetSbNameData(); //绑定设备
                GetChdNameData();//绑定部件
                GetFillManData();//绑定技术员
            }
            GetBoundData(); 
        }

        /// <summary>
        /// 设备类型查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSBname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询


            if (ddlSBname.SelectedValue == "-请选择-")
            {
                ddlFillman.SelectedValue = "-请选择-";
            }
            else
            {
                //获取带'-'的生产制号.从而绑定技术员
                string engcode = ddlEngName.SelectedValue + "-" + ddlSBname.SelectedValue;
                ddlFillman.SelectedValue = engcode;
            }
            GetChdNameData();
            GetPCodeData();
            GetBoundData();
        }
        
        /// <summary>
        /// 部件查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBJname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询

            GetMaterData();
            GetBoundData();
        }
        /// <summary>
        /// 技术员查询(DropDownList绑定的是人名Text和生产制号Value)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFillman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询


            if (ddlFillman.SelectedValue == "-请选择-")
            {
                ddlSBname.SelectedValue = "-请选择-";
            }
            else
            {
                string[] str = (ddlFillman.SelectedValue.ToString()).Split('-');
                ddlSBname.SelectedValue = str[1].ToString();
            }

            GetBoundData();
        }
        
        /// <summary>
        /// 批号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询

            GetBoundData();
        }
        
        /// <summary>
        /// 物料名称查询(是否按批号批号)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlmatername_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询


            GetBoundData();
        }

        /// <summary>
        /// 绑定存储过程
        /// </summary>
        /// <param name="str">查询条件</param>
        /// <returns>返回table</returns>
        public static DataTable GetDataByPagerQueryParam(string str,string type,string martype)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "TM_MaterailTotal");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@strWhere", str, SqlDbType.VarChar, 3000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Type", type, SqlDbType.VarChar, 10);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@MarType", martype, SqlDbType.VarChar, 20);
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
        /// 材料计划类别查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMpOutType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询

            GetBoundData();
        }
        /// <summary>
        /// 根据提交类别获取类别查询条件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetTypeQureyCondition(string type)
        {
            string returnValue = "";
            switch (type)
            {
                case "非定尺板":
                    returnValue = " and MP_MASHAPERV='非定尺板'";
                    break;
                case "型材":
                    returnValue = " and MP_MASHAPERV='型材'";
                    break;
                case "标(发运)":
                    returnValue = " and MP_MASHAPERV='标(发运)'";
                    break;
                case "标(组装)":
                    returnValue = " and MP_MASHAPERV='标(组装)'";
                    break;
                case "定尺板":
                    returnValue = " and MP_MASHAPERV='定尺板'";
                    break;
                case "除开定尺板":
                    returnValue = " and MP_MASHAPERV in('非定尺板','型材','标(发运)','标(组装)')";
                    break;
                default:break;
            }
            return returnValue;
        }

        /// <summary>
        /// Button查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }

            ViewState["QueryType"] = "User-Defined";
            GetBoundData();

        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择项目、工程，重置无效！！！');", true);
                return;
            }
            //自定义
            ddlQueryType.SelectedIndex = 0;
            txtQueryContent.Text = "";
            //常规
            ddlMpOutType.SelectedIndex = 0;
            ddlSBname.SelectedIndex = 0;
            ddlFillman.SelectedIndex = 0;
            ddlBJname.SelectedIndex = 0;
            ddlPCode.SelectedIndex = 0;
            ddlmatername.SelectedIndex = 0;
            ddlmptype.SelectedIndex = 0;


            GetBoundData();
        }
        /// <summary>
        /// 导出材料计划Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnExport_OnClick(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0 || ddlMpOutType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程及提交类别！！！');", true);
                return;
            }
            else
            {
                string str = "";
                //查询项目
                if (ddlProName.SelectedValue != "-请选择-")
                {
                    str += " MP_PJID='" + ddlProName.SelectedValue + "' ";
                }

                //查询工程
                if (ddlEngName.SelectedValue != "-请选择-")
                {
                    str += " and dbo.GetNoEngid(MP_ENGID,'-')='" + ddlEngName.SelectedValue + "' ";
                }

                //提交类别
                if (ddlMpOutType.SelectedValue != "-请选择-")
                {
                    str += this.GetTypeQureyCondition(ddlMpOutType.SelectedValue);
                }
                DataTable dt;
                if(ddlMpOutType.SelectedValue=="定尺板")
                {
                   dt = GetDataByPagerQueryParam(str,"1","1");
                }
                else
                {
                   dt = GetDataByPagerQueryParam(str,"1","0");
                }

                if (dt.Rows.Count > 0)
                {
                    string[] aa = ddlProName.SelectedItem.Text.Split('‖');
                    string prj = aa[1] + "(" + aa[0] + ")";
                    string[] bb = ddlEngName.SelectedItem.Text.Split('‖');
                    string engname = bb[1];
                    string engid = bb[0];
                    ExportTMDataFromDB.ExportAllMpData(dt,prj,engid,engname,ddlMpOutType.SelectedValue);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据！！！');", true);
                }
            }
        }

        protected void GridView1_OnPreRender(object sender, EventArgs e)
        {
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 1);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 2);
        }
    }
}
