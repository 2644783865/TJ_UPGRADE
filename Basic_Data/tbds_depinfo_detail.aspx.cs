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
using System.Data.SqlClient;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class tbds_depinfo_detail : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                //deletebt.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
                //deletebt.Click += new EventHandler(deletebt_Click);
                GetDepartment1();
                string selectaddgroup = selectaddgroupd.SelectedValue;//增加一级/二级部门选择的值
                //string sqltext = "select distinct * from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]' order by DEP_CODE";
                Dfinddept1.SelectedValue = "00";
                databind();
                Dfinddept2.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "全部";
                item.Value = "00";
                Dfinddept2.Items.Insert(0, item);
                Dfinddept2.SelectedValue = "00";
            }
            CheckUser(ControlFinder);
        }
        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }
        private void databind()
        {
            string sqltext;
            string str = Dfinddept1.SelectedValue;
            if (Dfinddept1.SelectedValue == "00")
            {
                sqltext = "select distinct * from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]' and DEP_SFJY='0'";
                setpagerparm("TBDS_DEPINFO", "DEP_CODE", "*", "DEP_CODE", "DEP_CODE LIKE '[0-9][0-9]' and DEP_SFJY='0'", 0, 10);
            }
            else
            {
                //sqltext = "select * from TBDS_DEPINFO where DEP_CODE LIKE '" + str + "[0-9][0-9]' order by DEP_CODE";//不显示第一级
                if (Radiogrouportw.SelectedValue == "1")//查看岗位/班组信息，生产部编码“04”
                {
                    //sqltext = "select * from TBDS_DEPINFO where DEP_CODE LIKE '04%' order by DEP_CODE";
                    sqltext = "select * from TBDS_DEPINFO where DEP_CODE LIKE '" + str + "[0-9][0-9]' and DEP_SFJY='0' order by DEP_CODE";//不显示第一级
                    setpagerparm("TBDS_DEPINFO", "DEP_CODE", "*", "DEP_CODE", "DEP_CODE LIKE '" + str + "[0-9][0-9]' and DEP_SFJY='0'", 0, 10);
                }
                else                                      //查看工种信息
                {
                    sqltext = "select JC_ID AS DEP_CODE,JC_NAME AS DEP_NAME,JC_NOTE AS DEP_NOTE from TBDS_JOBCATINFO WHERE JC_DEPID='04' and JC_SFJY='0' order by JC_ID";
                    setpagerparm("TBDS_JOBCATINFO", "JC_ID", "JC_ID AS DEP_CODE,JC_NAME AS DEP_NAME,JC_NOTE AS DEP_NOTE", "JC_ID", "JC_DEPID='04' and JC_SFJY='0'", 0, 10);
                }
            }
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, tbds_depinfoRepeater, UCPaging1, NoDataPane);
            if (NoDataPane.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
                CheckUser(ControlFinder);
            }
            Lnumber.Text = "共" + Convert.ToString(dt.Rows.Count) + "条记录";
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
        private void GetDepartment1()
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]' and DEP_SFJY='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            Dfinddept1.DataSource = dt;
            Dfinddept1.DataTextField = "DEP_NAME";
            Dfinddept1.DataValueField = "DEP_CODE";
            Dfinddept1.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            Dfinddept1.Items.Insert(0, item);
        }
        protected void Dfinddept1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            Radiogrouportw.Visible = false;
            //Radiogrouportw.Items[0].Selected = true;
            Radiogrouportw.SelectedValue = "1";
            string sqltext;
            if (Dfinddept1.SelectedValue == "00")
            {
                Dfinddept2.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "全部";
                item.Value = "00";
                Dfinddept2.Items.Insert(0, item);
                Dfinddept2.SelectedValue = "00";
                sqltext = "select distinct * from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]' and DEP_SFJY='0'";
            }
            else
            {
                if (Dfinddept1.SelectedValue == "04")//选择生产部
                {
                    Radiogrouportw.Visible = true;
                    Radiogrouportw.SelectedValue = "1";
                }
                Dfinddept2.Items.Clear();
                string str = Dfinddept1.SelectedValue;
                GetDepartment2(str);
                Dfinddept2.SelectedValue = "00";
                //sqltext = "select * from TBDS_DEPINFO where DEP_CODE LIKE '" + str + "[0-9][0-9]' order by DEP_CODE";//不显示第一级
            }
            databind();
        }
        protected void Radiogrouportw_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            //string sqltext;
            if (Radiogrouportw.SelectedValue == "1")//查看岗位/班组信息，生产部编码“04”
            {
                //sqltext = "select * from TBDS_DEPINFO where DEP_CODE LIKE '04%' order by DEP_CODE";
            }
            else                                      //查看工种信息
            {
                //sqltext = "select JC_ID AS DEP_CODE,JC_NAME AS DEP_NAME,JC_NOTE AS DEP_NOTE from TBDS_JOBCATINFO WHERE JC_DEPID='04' order by JC_ID";
            }
            databind();
        }
        private void GetDepartment2(string str)
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '" + str + "[0-9][0-9]' and DEP_SFJY='0' order by DEP_CODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            Dfinddept2.DataSource = dt;
            Dfinddept2.DataTextField = "DEP_NAME";
            Dfinddept2.DataValueField = "DEP_CODE";
            Dfinddept2.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            Dfinddept2.Items.Insert(0, item);
        }

        protected void Dfinddept2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqltext;
            string str1 = Dfinddept1.SelectedValue;
            if (Dfinddept2.SelectedValue == "00")
            {
                sqltext = "select * from TBDS_DEPINFO where DEP_CODE LIKE '" + str1 + "%' and DEP_SFJY='0' order by DEP_CODE";
            }
            else
            {
                string str2 = Dfinddept2.SelectedValue;
                sqltext = "select * from TBDS_DEPINFO where DEP_CODE LIKE '" + str2 + "%' and DEP_SFJY='0' order by DEP_CODE";
            }
            //GetAllDemoData(sqltext);
        }
        protected void selectaddgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectaddgroup = selectaddgroupd.SelectedValue;
            if (selectaddgroup == "0")//无操作
            {

            }
            else
            {
                //Response.Write("<script>javascript:window.open('tbds_depinfo_operate.aspx?action=add&selectaddgroup=" + selectaddgroup + "');</script>");
                Response.Redirect("tbds_depinfo_operate.aspx?action=add&selectaddgroup=" + selectaddgroup);
                //转到修改页面
            }

        }
        protected void deletebt_Click(object sender, EventArgs e)
        {
            Dfinddept1.SelectedValue = "00";
            Dfinddept2.SelectedValue = "00";
            Radiogrouportw.Visible = false;//隐藏
            Radiogrouportw.SelectedValue = "1";//默认为岗位/班组
            DataTable dt = null;
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            DBCallCommon.openConn(sqlConn);
            SqlCommand sqlCmd = new SqlCommand();//选择用
            sqlCmd.Connection = sqlConn;
            SqlCommand sqlCmd1 = new SqlCommand();//删除用
            sqlCmd1.Connection = sqlConn;
            string cannotstr;
            foreach (RepeaterItem e_id in tbds_depinfoRepeater.Items)
            {
                CheckBox chk = (CheckBox)e_id.FindControl("checkboxdep");
                if (chk.Checked)
                {
                    cannotstr = ((Label)e_id.FindControl("DEP_CODE")).Text;
                    #region
                    //查找对象
                    if (Radiogrouportw.SelectedValue == "0")
                    {
                        sqlCmd.CommandText = "select * from TBDS_STAFFINFO where ST_JOBCAT LIKE '" + cannotstr + "%'";
                        sqlCmd1.CommandText = "delete from TBDS_JOBCATINFO where JC_ID LIKE '" + cannotstr + "%'";
                    }
                    else
                    {
                        sqlCmd.CommandText = "select * from TBDS_STAFFINFO where ST_DEPID LIKE '" + cannotstr + "%'";
                        sqlCmd1.CommandText = "delete from TBDS_DEPINFO where DEP_CODE LIKE '" + cannotstr + "%'";
                    }
                    #endregion
                    dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                    if (dt.Rows.Count > 0)
                    {
                        Lnumber.Text = Convert.ToString(dt.Rows.Count);
                        Response.Write("<script>alert('编码为" + cannotstr + "下有人员，不能删除，若想删除，请删除该编码下的所有人员!');</script>");
                    }
                    else
                    {
                        sqlCmd1.ExecuteNonQuery();//执行删除操作
                    }
                }
            }
            UCPaging1.CurrentPage = 1;
            databind();
            DBCallCommon.closeConn(sqlConn);
        }
        protected void editBm(string BmId)
        {
            //  return "javascript:window.open('tbds_depinfo_operate.aspx?action=update&DEP_CODE=" + BmId + "')";
            Response.Write("tbds_depinfo_operate.aspx?action=update&DEP_CODE=" + BmId);

        }
        protected string showBm(string BmId)
        {
            return "javascript:window.open('tbds_depinfo_operate.aspx?action=show&&DEP_CODE=" + BmId + "')";
          //  Response.Write("tbds_depinfo_operate.aspx?action=show&&DEP_CODE=" + BmId);
        }

    }
}
