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
using ZCZJ_DPF;

namespace ZCZJ_DPF.ESM
{
    public partial class EQU_tzsb : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            deletebt.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            deletebt.Click += new EventHandler(deletebt_Click);
            if (!IsPostBack)
            {
                string sqltext;
                sqltext = "select * from EQU_tzsb order by Id";
                setpagerparm("EQU_tzsb","Id","Id*1 as Id,Code,Name,Type,Specification,Ocode,Rcode,Ucode,Manufa,Position,Ustate,Redate,Remark","","Name LIKE'%"+name.Text.ToString().Trim()+"%'",0,10);
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                databind();
            }
        }
        void Pager_PageChanged(int pageNumber)
        {
            databind();
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
        private void databind()
        {
            if (name.Text.ToString().Trim() != "")
            {
                string sqltext;
                sqltext = "select * from EQU_tzsb where Name LIKE'%"+name.Text.ToString().Trim()+"%' order by Id";
                setpagerparm("EQU_tzsb", "Id", "Id*1 as Id,Code,Name,Type,Specification,Ocode,Rcode,Ucode,Manufa,Position,Ustate,Redate,Remark", "", "Name LIKE'%" + name.Text.ToString().Trim() + "%'", 0, 10);
            }
            else
            {
                string sqltext;
                sqltext = "select * from EQU_tzsb order by Id";
                setpagerparm("EQU_tzsb", "Id", "Id*1 as Id,Code,Name,Type,Specification,Ocode,Rcode,Ucode,Manufa,Position,Ustate,Redate,Remark", "", "Name LIKE'%" + name.Text.ToString().Trim() + "%'", 0, 10);
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            }
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt1 = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt1, tzsbRepeater, UCPaging1, NoDataPane);
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
        }
        protected string editYg(string Id)
        {
            return "javascript:window.showModalDialog('EQU_tzsbop.aspx?action=update&&Id=" + Id + "','','DialogWidth=800px;DialogHeight=700px')";
        }
        protected void search_Click(object sender, EventArgs e)
        {
            databind();
            name.Text = "";
        }
        protected void deletebt_Click(object sender, EventArgs e)
        {
            string sqltext;
            sqltext = "select * from EQU_tzsb order by Id";
            setpagerparm("EQU_tzsb", "Id", "Id*1 as Id,Code,Name,Type,Specification,Ocode,Rcode,Ucode,Manufa,Position,Ustate,Redate,Remark", "", "Name LIKE'%" + name.Text.ToString().Trim() + "%'", 0, 10);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string strId = "";
            foreach (RepeaterItem e_id in tzsbRepeater.Items)
            {
                CheckBox chk = (CheckBox)e_id.FindControl("checkboxstaff");
                if (chk.Checked)
                {
                    strId += "'" + ((Label)e_id.FindControl("Id")).Text + "'" + ",";
                }
            }
            if (strId.Length > 1)
            {
                strId = strId.Substring(0, strId.Length - 1);
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("delete from EQU_tzsb where  Id in (" + strId + ")",sqlConn);
                DBCallCommon.openConn(sqlConn);
                SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Close();
            }
            DBCallCommon.closeConn(sqlConn);
            databind();
        }
    }
}
