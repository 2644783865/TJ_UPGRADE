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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Claim_Total : BasicPage
    {
        string sqltext = "";
        double spje = 0;
        string splb = "";//索赔类别
        double zzspje = 0;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitVar();
            if (!IsPostBack)
            {                
                this.InitVar();
                if (Request.QueryString["splb"] != null)
                {
                    splb = Request.QueryString["splb"].ToString();                    
                    dplSPLB.SelectedValue = Request.QueryString["splb"].ToString();
                    pager.StrWhere = "SPLB='" + splb + "'";
                }
                
                this.bindGrid();
            }
            CheckUser(ControlFinder);
        }  
       
        #region "数据查询，分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "Claim_Main_Sub";//视图
            pager.PrimaryKey = "SPDH";
            pager.ShowFields = "";
            pager.OrderField = "SPDH";
            pager.StrWhere = sqltext;
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;
            
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvTJ, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
            CheckUser(ControlFinder);
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetSqlText()
        {
            sqltext = "";

            //项目            
            sqltext += "XMBH LIKE '%" + tb_pjid.Text.Trim() + "%'";            

            //索赔类别
            if (dplSPLB.SelectedIndex != 0)
            {
                sqltext += " and SPLB=" + dplSPLB.SelectedValue.ToString() + "";
            }

            //合同号/索赔单位
            sqltext += " and (GYS LIKE '%" + txtSEARCHBOX.Text.Trim() + "%' or HTBH LIKE '%" + txtSEARCHBOX.Text.Trim() + "%')";

            //部门
            if (dplSLBM.SelectedIndex != 0)
            {
                sqltext += " and BM='" + dplSLBM.SelectedValue.ToString() + "'";
               
            }

            //是否扣款
            if (dplYN.SelectedIndex != 0)//
            {
                sqltext += " and SFKK='" + dplYN.SelectedValue.ToString() + "'";
            }
        }

        #endregion       

        protected void grvTJ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                spje += Convert.ToDouble(e.Row.Cells[5].Text);//索赔金额
                zzspje += Convert.ToDouble(e.Row.Cells[6].Text);//最终索赔金额

                //点击行变色
                e.Row.Attributes.Add("onclick", "RowClick(this)");
                e.Row.Attributes["style"] = "Cursor:pointer";

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "当前页汇总：";
                e.Row.Cells[5].Text = string.Format("{0:c2}", spje);
                e.Row.Cells[6].Text = string.Format("{0:c2}", zzspje);
            }

        }
        protected void btnQuery_onClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
        }
        //索赔添加
        protected void btnConfrim_onClick(object sender, EventArgs e)
        {
            if (dplSPLB_Select.SelectedIndex != 0)
            {
                switch (dplSPLB_Select.SelectedValue.ToString())
                {
                    case "0":
                        Response.Redirect("CM_Claim_YZ.aspx?Action=Add&splb=0");
                        break;
                    case "1":
                        Response.Redirect("CM_Claim_ZJYZ.aspx?Action=Add&splb=1");
                        break;
                    case "2":
                        Response.Redirect("CM_Claim_ZJFBS.aspx?Action=Add&splb=2");
                        break;
                    case "3":
                        Response.Redirect("CM_Claim_ZJFBS.aspx?Action=Add&splb=3");
                        break;
                    case "4":
                        Response.Redirect("CM_Claim_FBS.aspx?Action=Add&splb=4");
                        break;
                    case "5":
                        Response.Redirect("CM_Claim_FBS.aspx?Action=Add&splb=5");
                        break;
                }
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('请选择索赔类别！！！');", true);
            }
        }

        //选定的项目发生变化时
        protected void tb_pjinfo_Textchanged(object sender, EventArgs e)
        {
            string pjname = "";
            string pjid = "";
            if (tb_pjinfo.Text.ToString().Contains("|"))
            {
                pjname = tb_pjinfo.Text.Substring(0, tb_pjinfo.Text.ToString().IndexOf("|"));
                pjid = tb_pjinfo.Text.Substring(tb_pjinfo.Text.ToString().IndexOf("|") + 1);
                tb_pjinfo.Text = pjname;
                tb_pjid.Text = pjid;
                this.btnQuery_onClick(null, null);               
            }
            else
            {
                tb_pjinfo.Text = "";
                tb_pjid.Text = "";
                
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写项目！');", true); return;
            }
        }

        //重置
        protected void btnReset_onClick(object sender, EventArgs e)
        {
            tb_pjinfo.Text = "";
            tb_pjid.Text = "";
            dplSLBM.SelectedIndex = 0;
            dplSPLB.SelectedIndex = 0;
            dplYN.SelectedIndex = 0;
            txtSEARCHBOX.Text = "";

            this.btnQuery_onClick(null, null);
        }
    }
}
