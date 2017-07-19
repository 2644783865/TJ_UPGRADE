using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZCZJ_DPF;
using System.Data.SqlClient;

namespace testpage
{
    public partial class information : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "员工基本信息管理";
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            deletebt.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            deletebt.Click += new EventHandler(deletebt_Click);
            if (!IsPostBack)
            {
                GetDepartment1();
                DDlgroup.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "全部";
                item.Value = "00";
                DDlgroup.Items.Insert(0, item);
                DDlgroup.SelectedValue = "00";
                databind();
            }
        }

        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            string sqltext;
            string str1 = DDlpartment.SelectedValue;
            string str2 = DDlgroup.SelectedValue;

            if (DDlpartment.SelectedValue == "00")//部门选择“全部”，此时岗位/班组为“全部”
            {
                #region
                sqltext = "select distinct a.*,b.DEP_NAME from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE order by a.ST_ID";//选择所有部门人员
                setpagerparm(" TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE", " ST_ID ", "a.*,b.DEP_NAME", "", "a.ST_PD=0", 0, 10);
                #endregion
            }
            else//DropDownList1.SelectedValue != "00"
            {
                #region
                if (DDlgroup.SelectedValue == "00")
                {

                    sqltext = "select distinct a.*,b.DEP_NAME,d.DEP_NAME from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on left(a.ST_CODE,4)=d.DEP_CODE where a.ST_CODE LIKE'" + str1 + "%' and a.ST_PD=0 order by a.ST_CODE";//查看所有选定部门的人员
                    setpagerparm(" TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on left(a.ST_CODE,4)=d.DEP_CODE", " ST_CODE ", " a.*,b.DEP_NAME,d.DEP_NAME as DEP_NAME1 ", "", " a.ST_DEPID LIKE'" + str1 + "%' and a.ST_PD=0 ", 0, 10);
                }
                else//DropDownListgroup.SelectedValue != "00"
                {
                    sqltext = "select distinct a.*,b.DEP_NAME,d.DEP_NAME from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on left(a.ST_CODE,4)=d.DEP_CODE where a.ST_CODE LIKE'" + str2 + "%' and a.ST_PD=0 order by a.ST_CODE";//查看选定班组/岗位的人员
                    setpagerparm(" TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on left(a.ST_CODE,4)=d.DEP_CODE", " ST_CODE ", " a.*,b.DEP_NAME,d.DEP_NAME as DEP_NAME1", "", " a.ST_DEPID LIKE'" + str2 + "%' and a.ST_PD=0 ", 0, 10);
                }

                #endregion
            }
            if (name.Text.ToString().Trim() != "")
            {
                #region
                sqltext = "select distinct a.*,b.DEP_NAME,d.DEP_NAME from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on left(a.ST_CODE,4)=d.DEP_CODE where a.ST_NAME LIKE'%" + name.Text.ToString().Trim() + "%' and a.ST_PD=0 order by a.ST_CODE";
                setpagerparm(" TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on left(a.ST_CODE,4)=d.DEP_CODE", " ST_CODE ", " a.*,b.DEP_NAME,d.DEP_NAME as DEP_NAME1 ", "", " a.ST_NAME LIKE'%" + name.Text.ToString().Trim() + "%' and a.ST_PD=0 ", 0, 10);
                #endregion    
            }
            #region
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, tbds_staffinfoRepeater, UCPaging1, NoDataPane);
            if (NoDataPane.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
                //CheckUser(ControlFinder);
            }
            #endregion
            //Lnumber.Text = "共" + Convert.ToString(dt.Rows.Count) + "条记录";
        }

        private void setpagerparm(string TableName, string PrimaryKey, string ShowFields, string OrderField, string StrWhere, int OrderType, int PageSize)
        {
            pager.TableName = TableName;
            pager.PrimaryKey = PrimaryKey;
            pager.ShowFields = ShowFields;
            pager.OrderField = OrderField;
            pager.StrWhere = StrWhere;
            pager.OrderType = OrderType;
            pager.PageSize = PageSize;
            UCPaging1.PageSize = PageSize;
        }

        private void GetDepartment1()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DDlpartment.DataSource = dt;
            DDlpartment.DataTextField = "DEP_NAME";
            DDlpartment.DataValueField = "DEP_CODE";
            DDlpartment.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            DDlpartment.Items.Insert(0, item);
            DDlpartment.SelectedValue = "00";
        }

        private void GetDepartment2(string str)//绑定岗位
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '" + str + "[0-9][0-9]' order by DEP_CODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DDlgroup.DataSource = dt;
            DDlgroup.DataTextField = "DEP_NAME";
            DDlgroup.DataValueField = "DEP_CODE";
            DDlgroup.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            DDlgroup.Items.Insert(0, item);
            ListItem item1 = new ListItem();
            item1.Text = "无";
            item1.Value = str + "00";
            DDlgroup.Items.Insert(1, item1);
            DDlgroup.SelectedValue = "00";
        }

        protected void DDlpartment_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DDlpartment.SelectedValue == "00")
            {
                DDlgroup.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "全部";
                item.Value = "00";
                DDlgroup.Items.Insert(0, item);
                DDlgroup.SelectedValue = "00";
            }
            else
            {
                DDlgroup.Items.Clear();
                string str = DDlpartment.SelectedValue;
                GetDepartment2(str);
            }
            UCPaging1.CurrentPage = 1;
            databind();
        }

        protected void DDlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            databind();
        }

        protected void deletebt_Click(object sender, EventArgs e)
        {
            string strId = "";
            DDlpartment.SelectedValue = "00";
            DDlgroup.SelectedValue = "00";
            foreach (RepeaterItem e_id in tbds_staffinfoRepeater.Items)
            {
                CheckBox chk = (CheckBox)e_id.FindControl("checkboxstaff");
                if (chk.Checked)
                {
                    strId += "'" + ((Label)e_id.FindControl("ST_CODE")).Text + "'" + ",";
                }
            }
            if (strId.Length > 1)
            {
                strId = strId.Substring(0, strId.Length - 1);
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("update TBDS_STAFFINFO set ST_PD=1 where  ST_ID in (" + strId + ")", sqlConn);
                DBCallCommon.openConn(sqlConn);
                SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Close();
            }
            DBCallCommon.closeConn(sqlConn);
            databind();
        }

        protected string editYg(string YgId)
        {
            return "javascript:window.showModalDialog('tbds_staffinfo_operate.aspx?action=update&&ST_CODE=" + YgId + "','','DialogWidth=700px;DialogHeight=650px')";
        }

        protected string showYg(string YgId)
        {
            return "javascript:window.showModalDialog('tbds_staffinfo_operate.aspx?action=show&&ST_CODE=" + YgId + "','','DialogWidth=700px;DialogHeight=650px')";
        }
        protected void search_Click(object sender, EventArgs e)
        {
            databind();
            name.Text = "";
        }

        protected void refresh_Click(object sender, EventArgs e)
        {
            databind();
        }

        /// <summary>tbds_staffinfo_operate.aspx
        /// /////////
        /// </summary>
        //private void GridViewDataBind()
        //{
        //    string sqlResult = "select * from T_basic";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlResult);
        //    //GridView1.DataSource = dt;
        //    //GridView1.DataBind();
        //    Repeater1.DataSource = dt;
        //    Repeater1.DataBind();
        //}

        //private void GetDepartment1()//绑定部门
        //{
        //    string sqlText = "select distinct tposition from T_basic";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
        //    DDlpartment.DataSource = dt;
        //    DDlpartment.DataTextField = "tposition";
        //    DDlpartment.DataBind();
        //    ListItem item = new ListItem();
        //    item.Text = "全部";
        //    item.Value = "00";
        //    DDlpartment.Items.Insert(0, item);
        //    DDlpartment.SelectedValue = "00";
        //}

        //private void GetDepartment2(string str)//绑定岗位
        //{
        //    string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '" + str + "[0-9][0-9]' order by DEP_CODE";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
        //    DDlgroup.DataSource = dt;
        //    DDlgroup.DataTextField = "DEP_NAME";
        //    DDlgroup.DataValueField = "DEP_CODE";
        //    DDlgroup.DataBind();
        //    ListItem item = new ListItem();
        //    item.Text = "全部";
        //    item.Value = "00";
        //    DDlgroup.Items.Insert(0, item);
        //    ListItem item1 = new ListItem();
        //    item1.Text = "无";
        //    item1.Value = str + "00";
        //    DDlgroup.Items.Insert(1, item1);
        //    DDlgroup.SelectedValue = "00";
        //}

        //protected void DDlpartment_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DDlpartment.SelectedValue == "00")
        //    {
        //        DDlgroup.Items.Clear();
        //        ListItem item = new ListItem();
        //        item.Text = "全部";
        //        item.Value = "00";
        //        DDlgroup.Items.Insert(0, item);
        //        DDlgroup.SelectedValue = "00";
        //    }
        //    else
        //    {
        //        DDlgroup.Items.Clear();
        //        string str = DDlpartment.SelectedValue;
        //        GetDepartment2(str);
        //    }
        //}

    }

}
