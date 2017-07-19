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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CBCX : System.Web.UI.Page
    {
        string txtCx = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtCx = Request.QueryString["id"].ToString().Trim();
            if (!IsPostBack)
            {
                bindtime(ddlYear,ddlMonth);
            }

        }
        protected void bindtime(DropDownList ddlYear, DropDownList ddlMonth)
        {
            string shijian = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
            string DQMonth = DateTime.Now.Month.ToString();
            int INTMonth = int.Parse(DQMonth);
            //int INTMonth = 1;
            if (INTMonth > 3)
            {
                int i = 0;
                ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));

                for (int j = 1; j < 4; j++)
                {
                    int t = INTMonth - j;
                    string l = t.ToString();
                    if (t < 10)
                    {
                        l = "0" + t.ToString();
                    }

                    ddlMonth.Items.Add(new ListItem(l.ToString(), l.ToString()));
                }
            }
            else
                if (INTMonth == 3)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
                    }
                    for (int j = 1; j < 3; j++)
                    {
                        int t = INTMonth - j;
                        string l = "0" + t.ToString();
                        ddlMonth.Items.Add(new ListItem(l.ToString(), l.ToString()));
                    }
                    int k = 12;
                    ddlMonth.Items.Add(new ListItem(k.ToString(), k.ToString()));



                }
                else
                    if (INTMonth == 2)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
                        }
                        int j = 1;
                        int t = INTMonth - j;
                        string l = "0" + t.ToString();
                        ddlMonth.Items.Add(new ListItem(l.ToString(), l.ToString()));
                        int a = 12;
                        ddlMonth.Items.Add(new ListItem(a.ToString(), a.ToString()));
                        int k = 11;
                        ddlMonth.Items.Add(new ListItem(k.ToString(), k.ToString()));


                    }
                    else
                        if (INTMonth == 1)
                        {
                            int i = 1;
                            ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
                            int a = 12;
                            ddlMonth.Items.Add(new ListItem(a.ToString(), a.ToString()));
                            int k = 11;
                            ddlMonth.Items.Add(new ListItem(k.ToString(), k.ToString()));
                            int m = 10;
                            ddlMonth.Items.Add(new ListItem(m.ToString(), m.ToString()));
                        }
            

            
        }
        protected void btncx_click(object sender, EventArgs e)
        {
            string sql = "";
            string sql1 = "";
            sql = "select *,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as CB_MonthCB,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as CB_HeJi from (select * from OM_CanBu left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where  ST_ID= '" + txtCx + "' ";
            sql1=sql+" and CB_YearMonth='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
             //DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);

            if (dt.Rows.Count > 0)
            {


                txtCB_TS.Text = dt.Rows[0]["KQ_CBTS"].ToString();
                txtCB_BZ.Text = dt.Rows[0]["CB_BIAOZ"].ToString();
                txtCB_LEIJI.Text = dt.Rows[0]["CB_HeJi"].ToString();
            }
            else
            {
                txtCB_TS.Text = "";
                txtCB_BZ.Text = "";
                txtCB_LEIJI.Text = "";
               
            }
        }
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {  

        }
    }
}
