using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_FGdzcStore : System.Web.UI.Page
    {
        string flag;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                //if (flag == "ToStore")
                //{
                //    btnPush.Visible = true;
                //}
                GetBoundData();
            }

        }
        private string GetWhere()
        {
            string strWhere = "BIANHAO !='' and INTYPE='1' and SPZT='y' and BFZT='0'";

            if (txtName.Text.Trim() != "")
            {
                strWhere += " AND NAME like '%" + txtName.Text.Trim() + "%'";
            }
            if (txtModel.Text.Trim() != "")
            {
                strWhere += " AND MODEL like '%" + txtModel.Text.Trim() + "%'";
            }
            if (txtType.Text.Trim() != "")
            {
                strWhere += " AND TYPE like '%" + txtType.Text.Trim() + "%'";
            }
            if (txtPer.Text.Trim() != "")
            {

                strWhere += " and syr like '%" + txtPer.Text.Trim() + "%'";
            }

            return strWhere;
        }

        protected void btnBaofei_Click(object sender, EventArgs e)
        {
            int times = 0;
            string stid = "";
            foreach (RepeaterItem rptitem in Repeater1.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("CheckBox1");
                System.Web.UI.WebControls.Label lbid = (System.Web.UI.WebControls.Label)rptitem.FindControl("lbID");
                if (cbx.Checked == true)
                {
                    stid += CommonFun.ComTryInt(lbid.Text.ToString()) + ",";
                    times++;
                }
            }
            if (times == 0)
            {
                Response.Write("<script>alert('请勾选报废的非固定资产项！')</script>");
                return;
            }
            else
            {
                Response.Write("<script>window.open('OM_FGdzcBaofei_Detail.aspx?action=add&id=" + stid + "','_blank','height=500px,width=1200px')</script>");
            }
        }
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtModel.Text = "";
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
        protected void btnTransf_click(object sender, EventArgs e)
        {
            string bh = "";
            string name = "";
            string model = "";
            string former = "";
            string formerid = "";
            string bumen = "";
            string bumenid = "";
            string date = "";
            string place = "";
            string nx = "";
            string jiazhi = "";
            string note = "";
            List<string> list = new List<string>();
            string sqltxt = "DELETE FROM TBOM_TRANSBH";
            list.Add(sqltxt);
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    bh = ((Label)Repeater1.Items[i].FindControl("lblbh")).Text;
                    name = ((Label)Repeater1.Items[i].FindControl("lblName")).Text;
                    model = ((Label)Repeater1.Items[i].FindControl("lblModel")).Text;
                    former = ((Label)Repeater1.Items[i].FindControl("lblsyr")).Text;
                    formerid = ((Label)Repeater1.Items[i].FindControl("lblsyrid")).Text;
                    bumen = ((Label)Repeater1.Items[i].FindControl("lblsybm")).Text;
                    bumenid = ((Label)Repeater1.Items[i].FindControl("lblsybumenid")).Text;
                    date = ((Label)Repeater1.Items[i].FindControl("lbldate")).Text;
                    nx = ((Label)Repeater1.Items[i].FindControl("lblnx")).Text;
                    jiazhi = ((Label)Repeater1.Items[i].FindControl("lbljz")).Text;
                    place = ((Label)Repeater1.Items[i].FindControl("lblplace")).Text;
                    note = ((Label)Repeater1.Items[i].FindControl("lblnote")).Text;
                    sqltxt = "insert into TBOM_TRANSBH (BH,NAME,MODEL,SYR,SYRID,BUMEN,BUMENID,DATE,PLACE,NX,JIAZHI,NOTE,TRANTYPE) VALUES ('" + bh + "','" + name + "','" + model + "','" + former + "','" + formerid + "','" + bumen + "','" + bumenid + "','" + date + "','" + place + "','" + nx + "','" + jiazhi + "','" + note + "','1')";
                    list.Add(sqltxt);
                }
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_FGdzcTrans.aspx?action=add");
        }
        //protected void btnPush_OnClick(object sender, EventArgs e)
        //{
        //    List<string> sqllist = new List<string>();
        //    string sql = "UPDATE TBOM_GDZCSTORE SET OUTSTATE=''";
        //    sqllist.Add(sql);
        //    string name = "";
        //    string model = "";
        //    for (int i = 0; i < Repeater1.Items.Count; i++)
        //    {
        //        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
        //        {
        //            name = ((Label)Repeater1.Items[i].FindControl("lblName")).Text;
        //            model = ((Label)Repeater1.Items[i].FindControl("lblModel")).Text;
        //            sql = "UPDATE TBOM_GDZCSTORE SET OUTSTATE='1' WHERE NAME='" + name + "' AND MODEL='" + model + "'";
        //            sqllist.Add(sql);
        //        }
        //    }
        //    if (sqllist.Count < 2)
        //    {
        //        string alert = "<script>alert('请选择下推条目！！！')</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
        //        return;
        //    }
        //    DBCallCommon.ExecuteTrans(sqllist);
        //    Response.Redirect("OM_GDZCLL.aspx?FLAG=PUSH");
        //}

        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "TBOM_GDZCIN as a left join OM_SP as b on a.INCODE=b.SPFATHERID";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "*";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 0;
            pager.PageSize = 15;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion
    }
}
