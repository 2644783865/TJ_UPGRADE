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
    public partial class OM_CX : System.Web.UI.Page
    {
        string txtCx = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtCx = Request.QueryString["id"].ToString().Trim();
            if (!IsPostBack)
            {
                //bindtime(ddlYear, ddlMonth);
                btncx_click();
               
            }
        }
        protected void btncx_click()
        {

            //if (asd.first=="1")
            //{
            //    asd.mimi = txtpassword.Text;
            //}
            
            string sql = "";

            //Encrypt_Decrypt ed = new Encrypt_Decrypt();
            //string password = ed.MD5Encrypt(txtpassword.Text.Trim(), "!@#$%^&*");

            sql = "select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_RUZHITIME,NJ_LIZHITIME,NJ_TZTS,NJ_YSY,NJ_QINGL,NJ_LASTQL from OM_NianJiaTJ as a left join TBDS_STAFFINFO as b on a.NJ_ST_ID=b.ST_ID where NJ_ST_ID= '" + txtCx + "' ";


            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string txtkxts = "";
            string txtljnj = "";
            if (dt.Rows.Count > 0)
            {
                #region
                //计算年假
                DateTime datemin;
                DateTime datemax;
                try
                {
                    datemin = DateTime.Parse(dt.Rows[0]["NJ_RUZHITIME"].ToString().Trim());
                    if (dt.Rows[0]["NJ_LIZHITIME"].ToString().Trim() != "")
                    {
                        datemax = DateTime.Parse(dt.Rows[0]["NJ_LIZHITIME"].ToString().Trim());
                    }
                    else
                    {
                        datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                    }
                }
                catch
                {
                    datemin = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                    datemax = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                }
                decimal monthnum = datemax.Month - datemin.Month;
                decimal yearnum = datemax.Year - datemin.Year;
                decimal totalmonthnum = yearnum * 12 + monthnum;
                decimal ysynum = 0;
                decimal qinglnum = 0;
                try
                {
                    ysynum = Convert.ToDecimal(dt.Rows[0]["NJ_YSY"].ToString().Trim());
                    qinglnum = Convert.ToDecimal(dt.Rows[0]["NJ_QINGL"].ToString().Trim());
                }
                catch
                {
                    ysynum = 0;
                    qinglnum = 0;
                }
                //小于一年
                if (totalmonthnum < 12)
                {
                    txtkxts = "0";
                    txtljnj = (-(qinglnum + ysynum)).ToString().Trim();
                }
                //大于一年小于十年
                else if (totalmonthnum >= 12 && totalmonthnum < 120)
                {
                    txtkxts = (Math.Floor((totalmonthnum - 12) * 5 / 12) - qinglnum).ToString().Trim();
                    txtljnj = (Math.Floor((totalmonthnum - 12) * 5 / 12) - qinglnum - ysynum).ToString().Trim();
                }
                //大于十年小于二十年
                else if (totalmonthnum >= 120 && totalmonthnum < 240)
                {
                    txtkxts = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12) - qinglnum).ToString().Trim();
                    txtljnj = (Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12) - qinglnum - ysynum).ToString().Trim();
                }
                //二十年以上
                else
                {
                    txtkxts = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12) - qinglnum).ToString().Trim();
                    txtljnj = (Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12) - qinglnum - ysynum).ToString().Trim();
                }
                #endregion

                txtNJ_KXTS.Text = txtkxts;
                txtNJ_LEIJI.Text = txtljnj;
                txtNJ_YSY.Text = dt.Rows[0]["NJ_YSY"].ToString();
            }
            else
            {
                txtNJ_KXTS.Text = "";
                txtNJ_LEIJI.Text = "";
                txtNJ_YSY.Text = "";
                //Response.Write("<script type='text/javascript'>alert('您的用户名或者密码错误！！！')</script>");
            }
            //string sql2 = "";
            //string sql1 = "";


            //sql2 = "select *,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as CB_MonthCB,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as CB_HeJi from (select * from OM_CanBu left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where  ST_NAME= '" + txtCx + "'";
            //sql1 = sql2 + " and CB_YearMonth='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            //DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
            //DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            //if (dt2.Rows.Count < 1)
            //{
            //    Response.Write("<script type='text/javascript'>alert('您的用户名或者密码错误！！！')</script>");
            //}
            //if (dt1.Rows.Count > 0)
            //{


            //    txtCB_TS.Text = dt1.Rows[0]["KQ_CBTS"].ToString();
            //    txtCB_BZ.Text = dt1.Rows[0]["CB_BIAOZ"].ToString();
            //    txtCB_LEIJI.Text = dt1.Rows[0]["CB_HeJi"].ToString();
            //}
            //else
            //{
            //    txtCB_TS.Text = "";
            //    txtCB_BZ.Text = "";
            //    txtCB_LEIJI.Text = "";

            //}

            //asd.first = "2";
        }
        //protected void bindtime(DropDownList ddlYear, DropDownList ddlMonth)
        //{
        //    string shijian = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
        //    string DQMonth = DateTime.Now.Month.ToString();
        //    int INTMonth = int.Parse(DQMonth);
        //    //int INTMonth = 1;
        //    if (INTMonth > 3)
        //    {
        //        int i = 0;
        //        ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));

        //        for (int j = 1; j < 4; j++)
        //        {
        //            int t = INTMonth - j;
        //            string l = t.ToString();
        //            if (t < 10)
        //            {
        //                l = "0" + t.ToString();
        //            }

        //            ddlMonth.Items.Add(new ListItem(l.ToString(), l.ToString()));
        //        }
        //    }
        //    else
        //        if (INTMonth == 3)
        //        {
        //            for (int i = 0; i < 2; i++)
        //            {
        //                ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
        //            }
        //            for (int j = 1; j < 3; j++)
        //            {
        //                int t = INTMonth - j;
        //                string l = "0" + t.ToString();
        //                ddlMonth.Items.Add(new ListItem(l.ToString(), l.ToString()));
        //            }
        //            int k = 12;
        //            ddlMonth.Items.Add(new ListItem(k.ToString(), k.ToString()));



        //        }
        //        else
        //            if (INTMonth == 2)
        //            {
        //                for (int i = 0; i < 2; i++)
        //                {
        //                    ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
        //                }
        //                int j = 1;
        //                int t = INTMonth - j;
        //                string l = "0" + t.ToString();
        //                ddlMonth.Items.Add(new ListItem(l.ToString(), l.ToString()));
        //                int a = 12;
        //                ddlMonth.Items.Add(new ListItem(a.ToString(), a.ToString()));
        //                int k = 11;
        //                ddlMonth.Items.Add(new ListItem(k.ToString(), k.ToString()));


        //            }
        //            else
        //                if (INTMonth == 1)
        //                {
        //                    int i = 1;
        //                    ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
        //                    int a = 12;
        //                    ddlMonth.Items.Add(new ListItem(a.ToString(), a.ToString()));
        //                    int k = 11;
        //                    ddlMonth.Items.Add(new ListItem(k.ToString(), k.ToString()));
        //                    int m = 10;
        //                    ddlMonth.Items.Add(new ListItem(m.ToString(), m.ToString()));
        //                }



        //}
    }
}
