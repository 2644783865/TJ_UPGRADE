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
namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_assign_lookup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox_pid.Text = Request.QueryString["sheetno"].ToString();
                Initpage();
                repeaterdatabind();//绑定数据源
            }
        }

        private void Initpage()
        {
            string sqltext = "SELECT distinct PUR_PJID,PUR_PJNAME,PUR_ENGID,PUR_ENGNAME FROM TBPC_PURCHASEPLAN WHERE PUR_PCODE='" + TextBox_pid.Text.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)//有工程数据需要汇总
            {
                tb_pjid.Text = dt.Rows[0]["PUR_PJID"].ToString();
                tb_pj.Text = dt.Rows[0]["PUR_PJNAME"].ToString();
                tb_eng.Text = dt.Rows[0]["PUR_ENGNAME"].ToString();
                tb_engid.Text = dt.Rows[0]["PUR_ENGID"].ToString();
                Init_marty();
            }
            sqltext = "SELECT PR_PTASMAN, PR_PTASMANNM,PR_PTASTIME FROM TBPC_PCHSPLANRVW WHERE PR_SHEETNO='" + TextBox_pid.Text.ToString() + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)//有工程数据需要汇总
            {
                TextBoxexecutor.Text = dt.Rows[0]["PR_PTASMANNM"].ToString();
                TextBoxexecutorbianhao.Text = dt.Rows[0]["PR_PTASMAN"].ToString();
                Tb_shijian.Text = dt.Rows[0]["PR_PTASTIME"].ToString();
            }
        }
        private void Init_marty()
        {
            string sqltext = "SELECT TY_ID AS PUR_TY_ID,TY_NAME AS PUR_TY_NAME FROM TBMA_TYPEINFO WHERE (TY_ID IN (SELECT DISTINCT SUBSTRING(PUR_MARID, 1, 5) AS Expr1 FROM TBPC_PURCHASEPLAN WHERE PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND PUR_PJID= '" + tb_pjid.Text.ToString() + "' AND PUR_ENGID='" + tb_engid.Text.ToString() + "'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                DropDownList_TY.DataSource = dt;
                DropDownList_TY.DataBind();
                ListItem item = new ListItem();
                item.Value = "00";
                item.Text = "--全部--";
                DropDownList_TY.Items.Insert(0, item);
                DropDownList_TY.Items[0].Selected = true;
            }
        }
        private void repeaterdatabind()
        {
            string sqltext;
            if (DropDownList_TY.SelectedValue.ToString() == "00")
            {
                sqltext = "SELECT a.PUR_PJID,a.PUR_ENGID,a.PUR_PTCODE,a.PUR_MARID,a.PUR_MARNAME,a.PUR_MARNORM,a.PUR_MARTERIAL,a.PUR_FIXEDSIZE,a.PUR_RPNUM,a.PUR_NUNIT,a.PUR_CGMAN AS PUR_CGMANCODE,a.PUR_CGMANNM AS PUR_CGMANNAME,a.PUR_PRONODE,a.PUR_NOTE FROM TBPC_PURCHASEPLAN AS a WHERE PUR_PCODE='" + TextBox_pid.Text.ToString() + "'";
            }
            else
            {
                sqltext = "SELECT a.PUR_PJID,a.PUR_ENGID,a.PUR_PTCODE,a.PUR_MARID,a.PUR_MARNAME,a.PUR_MARNORM,a.PUR_MARTERIAL,a.PUR_FIXEDSIZE,a.PUR_RPNUM,a.PUR_NUNIT,a.PUR_CGMAN AS PUR_CGMANCODE,a.PUR_CGMANNM AS PUR_CGMANNAME,a.PUR_PRONODE,a.PUR_NOTE FROM TBPC_PURCHASEPLAN AS a WHERE PUR_PCODE='" + TextBox_pid.Text.ToString() + "' AND PUR_MARID LIKE'" + DropDownList_TY.SelectedValue + "%'";
            }
            DBCallCommon.BindRepeater(tbpc_purshaseplanrealityRepeater, sqltext);
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_assign_list.aspx");
        }
        protected void DropDownList_TY_SelectedIndexChanged(object sender, EventArgs e)
        {
            repeaterdatabind();
        }
        public string get_pr_state(string i)
        {
            string statestr = "";
            if (i == "1")
            {
                statestr = "未分工";
            }
            else
            {
                if (Convert.ToInt32(i) >= 2)
                {
                    statestr = "已分工";
                }
            }
            return statestr;
        }
        public string get_pur_fixed(string i)
        {
            string statestr = "";
            if (i == "0")
            {
                statestr = "不定尺";
            }
            else
            {
                if (i == "1")
                {
                    statestr = "定尺";
                }
            }
            return statestr;
        }
    }
}
