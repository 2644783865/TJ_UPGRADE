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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_Bgyp_Real : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MONTH_MAX();
             
                txt_readonly();
            }
        }
        
        private void txt_readonly()
        {
            MONTH_MAX_02.ReadOnly = true;
            MONTH_MAX_03.ReadOnly = true;
            MONTH_MAX_04.ReadOnly = true;
            MONTH_MAX_05.ReadOnly = true;
            MONTH_MAX_06.ReadOnly = true;
            MONTH_MAX_07.ReadOnly = true;
            MONTH_MAX_10.ReadOnly = true;
            MONTH_MAX_11.ReadOnly = true;
            MONTH_MAX_12.ReadOnly = true;
            MONTH_MAX_ALL.ReadOnly = true;

         
        }

        private void MONTH_MAX()
        {
            string sql_month_max = "select MONTH_MAX from TBOM_BGYP_Month_max where Type='0' order by DEP_CODE ";
            DataTable dt_month_max = DBCallCommon.GetDTUsingSqlText(sql_month_max);
            string sql_month_all = "select SUM(MONTH_MAX) from TBOM_BGYP_Month_max  where Type='0'";
            DataTable dt_month_all = DBCallCommon.GetDTUsingSqlText(sql_month_all);
            MONTH_MAX_ALL.Text = dt_month_all.Rows[0][0].ToString().Trim();
            MONTH_MAX_02.Text = dt_month_max.Rows[0][0].ToString().Trim();
            MONTH_MAX_03.Text = dt_month_max.Rows[1][0].ToString().Trim();
            MONTH_MAX_04.Text = dt_month_max.Rows[2][0].ToString().Trim();
            MONTH_MAX_05.Text = dt_month_max.Rows[3][0].ToString().Trim();
            MONTH_MAX_06.Text = dt_month_max.Rows[4][0].ToString().Trim();
            MONTH_MAX_07.Text = dt_month_max.Rows[5][0].ToString().Trim();
            MONTH_MAX_10.Text = dt_month_max.Rows[6][0].ToString().Trim();
            MONTH_MAX_11.Text = dt_month_max.Rows[7][0].ToString().Trim();
            MONTH_MAX_12.Text = dt_month_max.Rows[8][0].ToString().Trim();
        }



    

        //修改
        protected void change_month_max_onclick(object sender, EventArgs e)
        {
            MONTH_MAX_02.ReadOnly =false;
            MONTH_MAX_03.ReadOnly = false;
            MONTH_MAX_04.ReadOnly = false;
            MONTH_MAX_05.ReadOnly = false;
            MONTH_MAX_06.ReadOnly = false;
            MONTH_MAX_07.ReadOnly = false;
            MONTH_MAX_10.ReadOnly = false;
            MONTH_MAX_11.ReadOnly = false;
            MONTH_MAX_12.ReadOnly = false;
            save_month_max.Visible = true;
        }

        //保存
        protected void save_month_max_onclick(object sender, EventArgs e)
        {
            List<string> update_sql = new List<string>();
            string sql_update_max = "update TBOM_BGYP_Month_max set MONTH_MAX='" + MONTH_MAX_02.Text.ToString() + "' where DEP_CODE='02'  and Type='0'";
            update_sql.Add(sql_update_max);
            sql_update_max = "update TBOM_BGYP_Month_max set MONTH_MAX='" + MONTH_MAX_03.Text.ToString() + "' where DEP_CODE='03' and Type='0'";
            update_sql.Add(sql_update_max);
            sql_update_max = "update TBOM_BGYP_Month_max set MONTH_MAX='" + MONTH_MAX_04.Text.ToString() + "' where DEP_CODE='04' and Type='0'";
            update_sql.Add(sql_update_max);
            sql_update_max = "update TBOM_BGYP_Month_max set MONTH_MAX='" + MONTH_MAX_05.Text.ToString() + "' where DEP_CODE='05' and Type='0'";
            update_sql.Add(sql_update_max);
            sql_update_max = "update TBOM_BGYP_Month_max set MONTH_MAX='" + MONTH_MAX_06.Text.ToString() + "' where DEP_CODE='06' and Type='0'";
            update_sql.Add(sql_update_max);
            sql_update_max = "update TBOM_BGYP_Month_max set MONTH_MAX='" + MONTH_MAX_07.Text.ToString() + "' where DEP_CODE='07' and Type='0'";
            update_sql.Add(sql_update_max);
            sql_update_max = "update TBOM_BGYP_Month_max set MONTH_MAX='" + MONTH_MAX_10.Text.ToString() + "' where DEP_CODE='10' and Type='0'";
            update_sql.Add(sql_update_max);
            sql_update_max = "update TBOM_BGYP_Month_max set MONTH_MAX='" + MONTH_MAX_11.Text.ToString() + "' where DEP_CODE='11' and Type='0'";
            update_sql.Add(sql_update_max);
            sql_update_max = "update TBOM_BGYP_Month_max set MONTH_MAX='" + MONTH_MAX_12.Text.ToString() + "' where DEP_CODE='12' and Type='0'";
            update_sql.Add(sql_update_max);
            DBCallCommon.ExecuteTrans(update_sql);
            Response.Redirect("OM_Bgyp_DingE.aspx");
        }
    }
}
