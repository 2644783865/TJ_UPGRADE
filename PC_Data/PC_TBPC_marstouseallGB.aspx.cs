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
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_marstouseallGB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initpagemess();
                repeaterbind();
            }
        }

        private void Initpagemess()
        {
            string sqltext = "";
            string pcode = Server.UrlDecode(Request.QueryString["pcode"].ToString());
            sqltext = "SELECT PR_PCODE, PR_PJID, PJ_NAME, PR_ENGID, TSA_ENGNAME," +
                     "PR_REVIEWA, PR_REVIEWANM, PR_REVIEWATIME, PR_REVIEWB, PR_REVIEWBNM, " +
                     "PR_REVIEWBTIME, PR_REVIEWBADVC, PR_STATE, PR_NOTE  " +
                     "FROM View_TBPC_MARSTOUSETOTAL where PR_PCODE='" + pcode + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                TextBox_pid.Text = dr["PR_PCODE"].ToString();
                tb_pjid.Text = dr["PR_PJID"].ToString();
                tb_pjname.Text = dr["PJ_NAME"].ToString();
                tb_pjinfo.Text = tb_pjid.Text + tb_pjname.Text;
                tb_engid.Text = dr["PR_ENGID"].ToString();
                tb_engname.Text = dr["TSA_ENGNAME"].ToString();
                tb_enginfo.Text = tb_engid.Text + tb_engname.Text;
                tb_peoid.Text = dr["PR_REVIEWA"].ToString();
                tb_peoname.Text = dr["PR_REVIEWANM"].ToString();
                Tb_shijian.Text = dr["PR_REVIEWATIME"].ToString();
                tb_zh.Text = dr["PR_ENGID"].ToString();
            }
            dr.Close();
        }

        private void repeaterbind()
        {
            string sqltext = "";
            sqltext = "SELECT planno, pjid, pjnm, engid, engnm, ptcode, marid, marnm, margg, margb, marcz, " +
                      "marunit, marfzunit, length, width, num, fznum, usenum, usefznum, allstata, " +
                      "allshstate, allnote  " +
                      "FROM View_TBPC_MARSTOUSEALL where planno='" + TextBox_pid.Text + "' and (allshstate='0' or allshstate='1') order by ptcode asc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            tbpc_gbzyRepeater.DataSource = dt;
            tbpc_gbzyRepeater.DataBind();
            
        }
        protected void btn_concel_Click(object sender, EventArgs e)
        {

        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            string ptcode = "";
            string note = "";
            double num = 0;
            double fznum = 0;
            double plannum = 0;
            double planfznum = 0;
            foreach (RepeaterItem reitem in tbpc_gbzyRepeater.Items)
            {
                note = ((TextBox)reitem.FindControl("PUR_NOTE")).Text;
                ptcode = ((Label)reitem.FindControl("PUR_PTCODE")).Text;
                num = Convert.ToDouble(((TextBox)reitem.FindControl("PUR_USENUM")).Text == "" ? "0" : ((TextBox)reitem.FindControl("PUR_USENUM")).Text);
                fznum = Convert.ToDouble(((TextBox)reitem.FindControl("PUR_USEFZNUM")).Text == "" ? "0" : ((TextBox)reitem.FindControl("PUR_USEFZNUM")).Text);
                plannum = Convert.ToDouble(((Label)reitem.FindControl("PUR_NUM")).Text == "" ? "0" : ((Label)reitem.FindControl("PUR_NUM")).Text);
                planfznum = Convert.ToDouble(((Label)reitem.FindControl("PUR_FZNUM")).Text == "" ? "0" : ((Label)reitem.FindControl("PUR_FZNUM")).Text);
                sqltext = "update TBPC_MARSTOUSEALL set PUR_STATE='1',PUR_SHSTATE='0',PUR_SHYJ='',PUR_USTNUM='" + num + "',PUR_USTFZNUM='" + fznum + "',PUR_NOTE='" + note + "' where PUR_PTCODE='" + ptcode + "'";//状态改为已占用库存
                sqltextlist.Add(sqltext);
                sqltext = "update TBPC_PURCHASEPLAN set PUR_ZYDY='" + note + "' where PUR_PTCODE='" + ptcode + "'";
                sqltextlist.Add(sqltext);
            }
            DBCallCommon.ExecuteTrans(sqltextlist);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
            Response.Redirect("PC_TBPC_Purchaseplan_check_detail.aspx?sheetno=" + Server.UrlEncode(TextBox_pid.Text.Trim()) + "");
        }
        private double sum11 = 0;
        private double sum12 = 0;
        private double sum13 = 0;
        private double sum14 = 0;
        private double num1 = 0;
        private double num2 = 0;
        private double num3 = 0;
        private double num4 = 0;
        protected void btn_back_Click(object sender, EventArgs e)
        {
            string pcode = Request.QueryString["pcode"].ToString();
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_detail.aspx?sheetno=" + pcode + "");
        }
        protected void tbpc_gbzyRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("PUR_NUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PUR_NUM")).Text.ToString() == System.String.Empty)
                {
                    num1 = 0;
                    //((Label)e.Item.FindControl("PUR_NUM")).Text = "0";
                }
                else
                {
                    num1 = Convert.ToDouble(((Label)e.Item.FindControl("PUR_NUM")).Text);
                }
                sum11 += num1;

                if (((Label)e.Item.FindControl("PUR_FZNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PUR_FZNUM")).Text.ToString() == System.String.Empty)
                {
                    num2 = 0;
                    //((Label)e.Item.FindControl("PUR_FZNUM")).Text = "0";
                }
                else
                {
                    num2 = Convert.ToDouble(((Label)e.Item.FindControl("PUR_FZNUM")).Text);
                }
                sum12 += num2;


                if (((TextBox)e.Item.FindControl("PUR_USENUM")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("PUR_USENUM")).Text.ToString() == System.String.Empty)
                {
                    num3 = 0;
                    //((TextBox)e.Item.FindControl("PUR_USENUM")).Text = "0";
                }
                else
                {
                    num3 = Convert.ToDouble(((TextBox)e.Item.FindControl("PUR_USENUM")).Text);
                }
                sum13 += num3;

                if (((TextBox)e.Item.FindControl("PUR_USEFZNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("PUR_USEFZNUM")).Text.ToString() == System.String.Empty)
                {
                    num4 = 0;
                    //((TextBox)e.Item.FindControl("PUR_USEFZNUM")).Text = "0";
                }
                else
                {
                    num4 = Convert.ToDouble(((TextBox)e.Item.FindControl("PUR_USEFZNUM")).Text);
                }
                sum14 += num4;
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((TextBox)(e.Item.FindControl("tb_ntotal"))).Text = Convert.ToString(sum11);
                ((TextBox)(e.Item.FindControl("tb_fztotal"))).Text = Convert.ToString(sum12);
                ((TextBox)(e.Item.FindControl("tb_rpntotal"))).Text = Convert.ToString(sum13);
                ((TextBox)(e.Item.FindControl("tb_rpfztotal"))).Text = Convert.ToString(sum14);
            }
        }
        public string get_all_state(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "未保存";
            }
            else
            {
                state = "已保存";
            }
            return state;
        }
    }
}
