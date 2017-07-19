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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_Desk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LabelYear.Text = "本期年:"+System.DateTime.Now.Year.ToString();
                LabelMonth.Text = "本期月:" + System.DateTime.Now.Month.ToString().PadLeft(2, '0');
                string sqlText = "select count(*) from TBFM_HSTAOTALIN where HS_YEAR='" + System.DateTime.Now.Year.ToString() + "' and HS_MONTH='" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and HS_STATE='1'";
                System.Data.SqlClient.SqlDataReader rk_dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (rk_dr.HasRows)
                {
                    if (rk_dr.Read())
                    {
                        if (Convert.ToInt32(rk_dr[0]) > 0)
                        {
                            lb_rkhs.Text = "Y";
                        }
                        else
                        {
                            lb_rkhs.Text = "N";
                        }
                    }
                }
                rk_dr.Close();
                sqlText = "select count(*) from TBFM_HSTOTAL where HS_YEAR='" + System.DateTime.Now.Year.ToString() + "' and HS_MONTH='" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and HS_STATE='2'";
                System.Data.SqlClient.SqlDataReader ck_dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (ck_dr.HasRows)
                {
                    if (ck_dr.Read())
                    {
                        if (Convert.ToInt32(ck_dr[0]) > 0)
                        {
                            lb_ckhs.Text = "Y";
                            lb_qmgz.Text = "Y";
                        }
                        else
                        {
                            lb_ckhs.Text = "N";
                            lb_qmgz.Text = "N";
                        }
                    }
                }
                ck_dr.Close();
            }
        }
    }
}
