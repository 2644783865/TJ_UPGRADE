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

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_ZJXGSH_TOTAL : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();

            if (!IsPostBack)
            {
                bindGrid();
            }
        }
        #region 数据查询分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }

        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "View_ZJXGSH_APLYFORINSPECT";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID,AFI_ID,AFI_ENGID,AFI_PJNAME,AFI_ENGNAME,AFI_PARTNAME,AFI_SHJG,AFI_STATUS,AFI_JG,AFI_MANCLERK,AFI_CLERK_SJ,AFI_ISSH";
            pager.OrderField = "ID";//按报检时间排序
            pager.StrWhere = getStrWhere();
            pager.OrderType = 1;//按时间降序排列
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
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
                
            }
            else
            {
                UCPaging1.Visible = true;
                
            }
        }
        #endregion

        private string getStrWhere()
        {
            string condition = "";

            //生产制号
            if (txtENGID.Text != "")
            {
                condition += "AFI_ENGID LIKE '%" + txtENGID.Text + "%' ";
            }

            //项目名称
            if (txtPJNAME.Text != "" && condition != "")
            {
                condition += "AND AFI_PJNAME LIKE '%" + txtPJNAME.Text + "%' ";
            }
            else if (txtPJNAME.Text != "" && condition == "")
            {
                condition += " AFI_PJNAME LIKE '%" + txtPJNAME.Text + "%' ";
            }

            //工程名称
            if (txtENGNAME.Text != "" && condition != "")
            {
                condition += "AND AFI_ENGNAME LIKE '%" + txtENGNAME.Text + "%' ";
            }
            else if (txtENGNAME.Text != "" && condition == "")
            {
                condition += "AFI_ENGNAME LIKE '%" + txtENGNAME.Text + "%' ";
            }

            //申请人
            if (txtapplicant.Text != "" && condition != "")
            {
                condition += "AND AFI_MANCLERK LIKE '%" + txtapplicant.Text + "%' ";
            }
            else if (txtapplicant.Text != "" && condition == "")
            {
                condition += "AFI_MANCLERK LIKE '%" + txtapplicant.Text + "%' ";
            }



            //审批状态
            string userid = Session["UserID"].ToString();
            if (DropDownListSTATE.SelectedValue == "0" && condition != "")
            {
                string strid = userid.Substring(0, 2);
                if (strid == "05")
                {
                    condition += " AND AFI_STATUS='0' AND AFI_FIR_PERID='" + userid + "' and  AFI_FIR_JG is null ";
                }
                else if (strid == "07")
                {
                    condition += " AND AFI_STATUS='0' AND  AFI_SEC_PERID='" + userid + "' and  AFI_SEC_JG is null ";

                }

            }
            else if (DropDownListSTATE.SelectedValue == "0" && condition == "")
            {

                string strid = userid.Substring(0, 2);
                if (strid == "05")
                {
                    condition += " AFI_STATUS='0' AND AFI_FIR_PERID='" + userid + "' and  AFI_FIR_JG is null ";
                }
                else if (strid == "07")
                {
                    condition += " AFI_STATUS='0' AND  AFI_SEC_PERID='" + userid + "' and  AFI_SEC_JG is null ";

                }

            }

            if (DropDownListSTATE.SelectedValue == "1" && condition!="")
            {
                condition += "AND AFI_STATUS='0' ";
            }
            else if (DropDownListSTATE.SelectedValue == "1" && condition == "")
            {
                condition += "AFI_STATUS='0' ";
            }

            if (DropDownListSTATE.SelectedValue == "2" && condition != "")
            {
                condition += "AND AFI_JG='0' ";
            }
            else if (DropDownListSTATE.SelectedValue == "2" && condition == "")
            {
                condition += "AFI_JG='0' ";
            }

            if (DropDownListSTATE.SelectedValue == "3" && condition != "")
            {
                condition += "AND AFI_JG='1' ";
            }
            else if (DropDownListSTATE.SelectedValue == "3" && condition == "")
            {
                condition += "AFI_JG='1' ";
            }
            return condition;
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindGrid();
        }

        protected void STATE_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindGrid();
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
               
                    HyperLink hl = (HyperLink)gr.FindControl("hykqqsh");
                    Label lb = (Label)gr.FindControl("lbid");
                    HiddenField hfd = (HiddenField)gr.FindControl("shjg");
                    Label label1 = (Label)gr.FindControl("jg");
                    if(hfd.Value=="0")
                    {
                        label1.Text = "同意";
                    }
                    if (hfd.Value == "1")
                    {
                        label1.Text = "不同意";
                    }

                    

                    string sql = "select * from TBQM_ZJXGSH where ID='"+lb.Text+"'";
                    
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        string fir_perid = dt.Rows[0]["AFI_FIR_PERID"].ToString();
                        string sec_perid = dt.Rows[0]["AFI_SEC_PERID"].ToString();
                        string thi_perid = dt.Rows[0]["AFI_THI_PERID"].ToString();
                        if (fir_perid == Session["UserID"].ToString() || sec_perid == Session["UserID"].ToString() || thi_perid == Session["UserID"].ToString())
                        {
                            hl.Enabled = true;
                        }
                    }

                
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
            }
        }
    }
}
