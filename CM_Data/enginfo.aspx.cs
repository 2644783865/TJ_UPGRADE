using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class enginfo : BasicPage
    {
         PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {           
            InitVar();
            if (!IsPostBack)
            {
                bindGrid();                
            }
            //CheckUser(ControlFinder);
        }

        private void InitVar()
        {
            delete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }

        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "TBPM_ENGINFO as a,TBDS_STAFFINFO as b";
            pager.PrimaryKey = "a.ENG_ID";
            //在pager.ShowFields中加一个ENG_PJID
            pager.ShowFields = "a.ENG_ID,a.ENG_NAME,b.ST_NAME,a.ENG_MANDATE,a.ENG_NOTE,a.ENG_STRTYPE";
            pager.OrderField = "a.ENG_STARTDATE";
            pager.StrWhere = "a.ENG_MANCLERK=b.ST_ID";
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
            CommonFun.Paging(dt, Reproject1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
                //CheckUser(ControlFinder);
            }
            
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> eng_ID = new List<string>();
            string strID = "";
            int rowEffected = 0;
            string sql_checkEngidUsed = "";

            foreach (RepeaterItem labID in Reproject1.Items)
            {
                CheckBox chk = (CheckBox)labID.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    //查找该CheckBox所对应纪录的id号,在labID中
                    strID = ((Label)labID.FindControl("eng_ID")).Text;
                    sql_checkEngidUsed = "select TSA_STFORCODE from TBPM_TCTSASSGN where TSA_STFORCODE='" + strID + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_checkEngidUsed);
                    if (dr.HasRows)
                    {
                        Response.Write("<script>alert('工程代号:【" + strID + "】已被引用,无法删除！！！')</script>");
                        return;
                    }
                    eng_ID.Add(strID);
                }
            }
            lbl_Info.Text += strID;
            foreach (string id in eng_ID)
            {
                DBCallCommon.DeleteENGByENG_ID(id);
                rowEffected++;
            }
            //防止刷新
            Response.Redirect("enginfo.aspx?q=1");
            Response.Write("<script>alert('数据已删除！！！\\r\\r提示:共删除"+rowEffected+"条记录')</script>");
        }    
       

        protected void btn_Query_Click(object sender, EventArgs e)
        {
            pager.TableName = "TBPM_ENGINFO as a,TBDS_STAFFINFO as b";
            pager.PrimaryKey = "a.ENG_ID";
            pager.ShowFields = "a.ENG_ID,a.ENG_NAME,b.ST_NAME,a.ENG_MANDATE,a.ENG_NOTE,a.ENG_STRTYPE";
            pager.OrderField = "a.ENG_STARTDATE";
            pager.StrWhere = "a."+ddlQueryTye.SelectedValue.ToString()+" LIKE '%" + tb_Query.Text.ToString() + "%' and a.ENG_MANCLERK=b.ST_ID";         
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = 10;
            //////pager.PageIndex = 1;
            UCPaging1.CurrentPage = 1;
            bindGrid();            
        }

        protected void btn_Showall_Click(object sender, EventArgs e)
        {
            InitPager();
            UCPaging1.CurrentPage = 1;
            tb_Query.Text = "";
            bindGrid();
        }

        protected string editGc(string GcId)
        {
            return "javascript:window.showModalDialog('enginfoDetail.aspx?action=update&id=" + GcId + "','','DialogWidth=800px;DialogHeight=500px')";
        }
    }
    
}
