using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Real_View : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_title();
                InitVar();//分页
                GetTechRequireData();
                //control_visible();
            }
            InitVar();
           // CheckUser(ControlFinder); 
        }

        protected void Bind_title()
        {
            BindProject();
            BindEngineer();
            string sql_updatetime = "select top 1 YS_UPDATE_TIME from YS_COST_REAL ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_updatetime);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() != "")
                {
                    DateTime time = Convert.ToDateTime(dt.Rows[0][0].ToString());
                    lab_updatetime.Text = time.ToString("yyyy-MM-dd HH:mm");
                }
            }
            else
            {
                lab_updatetime.Text = "未更新";
            }
        }

        protected void control_visible()
        {
            string DEP = Session["UserDeptID"].ToString();
            string name = Session["UserName"].ToString();
            for (int i = 4; i < 23; i++)//从合同号后面的列开始全部隐藏
            {
                GridView1.Columns[i].Visible = false;
            }
            if (name == "邓朝晖" || name == "于建会" || DEP == "08") //财务部和公司领导
            {
                for (int i = 4; i < 23; i++)//全部显示
                {
                    GridView1.Columns[i].Visible = true;
                }
            }
            else if (DEP == "03" || name == "张超臣")//技术部
            {
                GridView1.Columns[4].Visible = true;
            }
            else if (DEP == "12" || name == "李冰飞")//市场部
            {
                GridView1.Columns[18].Visible = true;  //运费
                GridView1.Columns[19].Visible = true;  //材料费小计
                for (int i = 5; i < 11; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
            }
            else if (DEP == "04" || name == "柳强")//生产部
            {
                for (int i = 11; i < 14; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
            }
            else
            {
            }
        }

        protected void BindProject()//绑定项目名称下拉框
        {
            string sqltext = "SELECT DISTINCT PCON_PJNAME AS DDLVALUE,PCON_PJNAME AS DDLTEXT FROM View_YS_COST_BUDGET_REAL where YS_REVSTATE='3' ";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_project, sqltext, dataText, dataValue);
        }

        protected void BindEngineer()//绑定工程名称下拉框
        {
            string sqltext = "SELECT DISTINCT PCON_ENGNAME AS DDLVALUE,PCON_ENGNAME AS DDLTEXT FROM View_YS_COST_BUDGET_REAL where YS_REVSTATE='3' and PCON_PJNAME='" + ddl_project.SelectedItem.ToString() + "'";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_engineer, sqltext, dataText, dataValue);
        }

        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
            //if (rbl_type.SelectedIndex != 0)
            //{
            //    ckb_JS_OK.Checked = false;
            //    ckb_JS_OK.Visible = false;
            //}
            //else
            //{
            //    ckb_JS_OK.Visible = true;
            //}
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetTechRequireData();
        }

        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "View_YS_COST_BUDGET_REAL";
            pager.PrimaryKey = "YS_CONTRACT_NO";
            pager.ShowFields = "YS_CONTRACT_NO,PCON_SCH,PCON_PJNAME,PCON_ENGNAME,YS_FERROUS_METAL," +
            "YS_PURCHASE_PART,YS_MACHINING_PART,YS_PAINT_COATING,YS_ELECTRICAL,YS_OTHERMAT_COST,YS_TEAM_CONTRACT, " +
            "YS_FAC_CONTRACT,YS_PRODUCT_OUT,YS_TRANS_COST," +
            "YS_ADDDATE,YS_NOTE,YS_REVSTATE,[YS_XS_Finished],[YS_Finshtime]," +
            "YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST AS YS_MAR_SUM";

            pager.OrderField = "YS_ADDDATE";
            pager.StrWhere = this.GetStrWhere();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
        }

        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            //绑定数据
            GetTechRequireData();
        }

        protected void GetTechRequireData()
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

        protected string GetStrWhere()
        {
            string strwhere = " 1=1 ";
            strwhere += " and YS_REVSTATE='3' and PCON_SCH like '%" + txt_search.Text.ToString() + "%'";

            string this_month = DateTime.Now.ToString("yyyy-MM");
            this_month += "-01";

            if (ddl_project.SelectedIndex != 0)//项目名称
            {
                strwhere += " and PCON_PJNAME='" + ddl_project.SelectedValue + "'";
            }
            if (ddl_engineer.SelectedIndex != 0)//工程名称
            {
                strwhere += " and PCON_ENGNAME='" + ddl_engineer.SelectedValue + "'";
            }

            if (rbl_type.SelectedValue == "0")
            {
                strwhere += " and YS_XS_Finished is NULL  ";
                //strwhere += " and (YS_ADDDATE='' or YS_ADDDATE is null) ";
            }
            else if (rbl_type.SelectedValue == "1")
            {
                strwhere += " and YS_XS_Finished='1' ";
                //strwhere += " and YS_ADDDATE is not null and datalength(YS_ADDDATE)<>0 ";
            }

            //if (ckb_JS_OK.Checked == true)
            //{
            //    strwhere += " and YS_XS_Finished='1' ";
            //}

            return strwhere;
        }//改
        #endregion

        protected string GetJSState(string JsState, string FinState)
        {
            string retValue = "";

            if (JsState == "")
            {
                if (FinState == "1")
                {
                    retValue = "可结算";
                }

                else
                {
                    retValue = "未结算";
                }
            }

            else
            {
                retValue = "已结算";
            }

            return retValue;
        }

        protected string GetFinState(string FinState)
        {
            string retValue = "";
            if (FinState == "1")
            {
                retValue = "已完结";
            }
            else
            {
                retValue = "未完结";
            }

            return retValue;
        }

        protected void GridView1_onrowdatabound(object sender, GridViewRowEventArgs e)
        {
            String controlId = ((GridView)sender).ClientID;
            String uniqueId = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                uniqueId = String.Format("{0}{1}", controlId, e.Row.RowIndex);
                e.Row.Attributes.Add("onclick", String.Format("SelectRow('{0}', this);", uniqueId));
                //e.Row.Attributes.Add("onclick", "ItemOver(this)");  //单击行变色
                string lbl_CONTRACT_NO = ((Label)e.Row.FindControl("lbl_YS_CONTRACT_NO")).Text.ToString();
                string lbl_TSA_ID = ((Label)e.Row.FindControl("lbl_YS_TSA_ID")).Text.ToString();
                e.Row.Cells[1].Attributes.Add("ondblclick", "ShowContract('" + lbl_CONTRACT_NO + "')");
                e.Row.Cells[1].Attributes.Add("title", "双击关联合同信息！");

                Encrypt_Decrypt ed = new Encrypt_Decrypt();
                string TSA_ID = ed.EncryptText(lbl_TSA_ID);
                string CONTRACT_NO = ed.EncryptText(lbl_CONTRACT_NO);
                string[] fathername = {  "FERROUS_METAL", "PURCHASE_PART", "MACHINING_PART", "PAINT_COATING", "ELECTRICAL", "OTHERMAT_COST", "TEAM_CONTRACT", "FAC_CONTRACT", "PRODUCT_OUT" };
                for (int i = 0; i < fathername.Length; i++)
                {
                    e.Row.Cells[i + 4].Attributes.Add("ondblclick", "PurMarView('" + TSA_ID + "','" + ed.EncryptText(fathername[i]) + "')");
                    e.Row.Cells[i + 4].Attributes["style"] = "Cursor:hand";
                    e.Row.Cells[i + 4].Attributes.Add("title", "双击查看明细");
                }
                if (Session["UserDeptID"].ToString() == "08")
                {
                    string lab_JSstate = ((Label)e.Row.FindControl("lab_JSstate")).Text.ToString();
                    string lab_Finstate = ((Label)e.Row.FindControl("lab_Finstate")).Text.ToString();
                    if (lab_JSstate == "可结算")
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void btn_search_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }

        protected void ddl_project_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindEngineer();
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }

        protected void ckb_JS_OK_OnCheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }

        protected void btnModify_OnClick(object sender, EventArgs e)
        {
            string YS_CONTRACT_NO = "";
            string CONTRACT_NO = "";
            foreach (GridViewRow grow in GridView1.Rows)
            {
                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    CONTRACT_NO = ((HiddenField)grow.FindControl("hdfMP_ID")).Value.ToString();
                    Encrypt_Decrypt ed = new Encrypt_Decrypt();
                    YS_CONTRACT_NO = ed.EncryptText(CONTRACT_NO);
                    break;
                }
            }
            if (YS_CONTRACT_NO != "")
            {
                string sql_fin = "select YS_XS_Finished from YS_COST_BUDGET where YS_TSA_ID='" + CONTRACT_NO + "'";
                DataTable dt_fin = DBCallCommon.GetDTUsingSqlText(sql_fin);
                if (dt_fin.Rows.Count > 0)
                {
                    if (dt_fin.Rows[0][0].ToString() == "1")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该任务已完成结算！');", true);
                        return;
                    }
                    else
                    {
                        string sql = "update YS_COST_BUDGET set YS_XS_Finished='1',YS_Finshtime=GETDATE() where YS_TSA_ID='" + CONTRACT_NO + "'";
                        DBCallCommon.ExeSqlText(sql);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('任务结算成功！');", true);
                        UCPaging1.CurrentPage = 1;
                        InitVar();
                        GetTechRequireData();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要修改的行！！！');", true);
            }
        }

        protected void Btn_update_OnClick(object sender, EventArgs e)
        {
            try
            {
                string sql = DBCallCommon.GetStringValue("connectionStrings");
                sql += "Asynchronous Processing=true;";
                SqlConnection sqlConn = new SqlConnection(sql);
                sqlConn.Open();
                SqlCommand sqlCmd = new SqlCommand("YS_COST_REAL_PROCEDURE", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal
                IAsyncResult result = sqlCmd.BeginExecuteNonQuery();
                sqlCmd.EndExecuteNonQuery(result);
                sqlConn.Close();
                if (Convert.ToInt32(sqlCmd.Parameters["@retVal"].Value) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('更新成功！！！');location.reload();", true);
                    UCPaging1.CurrentPage = 1;
                    InitVar();
                    GetTechRequireData();
                    lab_updatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
