using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

namespace ZCZJ_DPF.FM_Data
{
    public partial class CB_Confirm : System.Web.UI.Page
    {    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 0; i < ddlDep.Items.Count; i++)
                {
                    if (ddlDep.Items[i].Value == Session["UserDeptID"].ToString())
                    {
                        ddlDep.Items[i].Selected = true;
                        break;
                    }
                }
                btnQuery_OnClick(null, null);
                InitPage();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
        }

        private void InitPage()
        {
            //GetSqlText();
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindGrid();
        }
      
       protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            InitPage();   
        }

     public string GetSqlText()
       {
           StringBuilder strb = new StringBuilder();
           
           if (ddlDep.SelectedIndex != 0)
            {
                if (ddlDep.SelectedValue == "03")//技术
                {
                    if (RadioButtonList1.SelectedValue == "0")
                    {
                        strb.Append("(JFY='否' or JWW='否')");
                    }
                    else if (RadioButtonList1.SelectedValue == "1")
                    {
                        strb.Append("STATUS=0 and JFY= '是' and JWW='是'");
                    }
                    else
                    {
                        strb.Append("STATUS=1");
                    }
                }
                else if (ddlDep.SelectedValue == "12")//市场
                {
                    if (RadioButtonList1.SelectedValue == "0")
                    {
                        strb.Append("(HT_ID='' or DANWEI='' or	JSL='' or DANJIA='' or PRICE='') and STATUS=0 and JFY= '是' and JWW='是'");
                    }
                    else if (RadioButtonList1.SelectedValue == "1")
                    {
                        strb.Append("STATUS=0 and HT_ID!='' and DANWEI!='' and	JSL!='' and DANJIA!='' and PRICE!=''");
                    }
                    else
                    {
                        strb.Append("STATUS=1");
                    }
                }
                else if (ddlDep.SelectedValue == "07")//储运
                {
                    if (RadioButtonList1.SelectedValue == "0")
                    {
                        strb.Append("(CFY='否' or CFP='否' or CCRK='否') and STATUS=0 and HT_ID!='' and DANWEI!='' and JSL!='' and DANJIA!='' and PRICE!=''");
                    }
                    else if(RadioButtonList1.SelectedValue == "1")
                    {
                        strb.Append("(CFY='是' or CFP='是' or CCRK='是')");
                    }
                    else
                    {
                        strb.Append("STATUS=1");
                    }

                }
                else if (ddlDep.SelectedValue == "04")//生产部
                {
                    if (RadioButtonList1.SelectedValue == "0")
                    {
                        strb.Append("SJS='否' and STATUS=0 and CFY='是' and CFP='是' and CCRK='是'");
                    }
                    else if (RadioButtonList1.SelectedValue == "1")
                    {
                        strb.Append("SJS='是'");
                    }
                    else
                    {
                        strb.Append("STATUS=1");
                    }
                }
                else if (ddlDep.SelectedValue == "06")//采购部
                {
                    if (RadioButtonList1.SelectedValue == "0")
                    {
                        strb.Append(" CGFP='否' and STATUS=0 and SJS='是'");
                    }
                    else if (RadioButtonList1.SelectedValue == "1")
                    {
                        strb.Append(" CGFP='是'");
                    }
                    else
                    {
                        strb.Append("STATUS=1");
                    }
                }

                else if (ddlDep.SelectedValue == "09")//电器制造部
                {
                    if (RadioButtonList1.SelectedValue == "0")
                    {
                        strb.Append("DQFP='否' and STATUS=0 and CGFP='是'");
                    }
                    else if(RadioButtonList1.SelectedValue == "1")
                    {
                        strb.Append("DQFP='是'");
                    }
                    else
                    {
                        strb.Append("STATUS=1");
                    }

                }
            }
            else
            {
                if (RadioButtonList1.SelectedValue == "2")
                {
                    strb.Append("STATUS=1");
                }
                else
                {
                    strb.Append("STATUS=0"); 
                }
            }
           //查询类型条件

               if (ddlType.SelectedValue == "1")
               {
                   strb.Append (" and TASK_ID like '%" + txtTaskID.Text.Trim() + "%'");
               }
               else if (ddlType.SelectedValue == "2")
               {
                   strb.Append (" and PRJ like '%" + txtTaskID.Text.Trim() + "%'");
               }
               else 
               {
                   strb.Append("  and ENG like '%" + txtTaskID.Text.Trim() + "%'");
               }


               return strb.ToString();
    }

       protected void btnExport_Onclick(object sender,EventArgs e)
       {
           string sql = "select TASK_ID,PRJ,ENG,WGRQ from TBCB_BMCONFIRM where " + GetSqlText() + "";
           DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
           if (dt.Rows.Count > 0)
           {
               ExportDataFromDB.ExportConfirm(dt);
           }

       }


        #region  分页 
        PagerQueryParam pager_org = new PagerQueryParam();

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager_org.TableName = "TBCB_BMCONFIRM";
            pager_org.PrimaryKey = "TASK_ID";
            pager_org.ShowFields = "";
            pager_org.OrderField = "TASK_ID";
            pager_org.StrWhere = GetSqlText();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 30;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
            CommonFun.Paging(dt, Rbm, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

        }
        #endregion
    }
}
