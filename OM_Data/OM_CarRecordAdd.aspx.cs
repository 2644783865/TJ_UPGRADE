using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarRecordAdd : System.Web.UI.Page
    {
        string action = string.Empty;
        string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"].ToString();
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();
            if (!this.IsPostBack)
            {
                this.BindYearMoth(ddl_Year, ddl_Month);
                this.InitPage();
                if (action == "update")
                {
                    this.GetDataByID(id);
                }
            }
        }

        protected void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }

        private void InitPage()
        {
            ddl_Year.ClearSelection();
            foreach (ListItem li in ddl_Year.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            ddl_Month.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in ddl_Month.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }

        protected void GetDataByID(string id)
        {
            string sqltext = "select * from TBOM_CARRECORD where ID = " + id + "";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            dr.Read();
            ddl_Year.SelectedValue = dr["YEAR"].ToString();
            ddl_Month.SelectedValue = dr["MONTH"].ToString();
            txtCarNum.Text = dr["CARNUM"].ToString();
            txtTime.Text = dr["TIME"].ToString();
            txtOrigin.Text = dr["ORIGIN"].ToString();
            txtDestination.Text = dr["DESTINATION"].ToString();
            txtStartMile.Text = dr["STARTMILE"].ToString();
            txtFinalMile.Text = dr["FINALMILE"].ToString();
            txtDriver.Text = dr["DRIVER"].ToString();
            txtCotroller.Text = dr["COTROLLER"].ToString();
            txtUprice.Text = dr["UPRICE"].ToString();
            txtUoil.Text = dr["UOIL"].ToString();
            txtMoney.Text = dr["MONEY"].ToString();
            txtNote.Text = dr["NOTE"].ToString();
            dr.Close();
        }

        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            string year = ddl_Year.SelectedValue;
            string month = ddl_Month.SelectedValue;
            string carnum = txtCarNum.Text.Trim();
            string time = txtTime.Text.Trim();
            string origin = txtOrigin.Text.Trim();
            string destination = txtDestination.Text.Trim();
            string startmile = txtStartMile.Text.Trim();
            string finalmile = txtFinalMile.Text.Trim();
            string driver = txtDriver.Text.Trim();
            string cotroller = txtCotroller.Text.Trim();
            string uprice = txtUprice.Text.Trim();
            string uoil = txtUoil.Text.Trim();
            string money = txtMoney.Text.Trim();
            string note = txtNote.Text.Trim();
            string sqltext = "";
            if (id !="")
            {
                sqltext = "update TBOM_CARRECORD set [YEAR]='" + year + "',[MONTH]='" + month + "',[CARNUM]='" + carnum + "',[TIME]='" + time + "',[ORIGIN]='" + origin + "',[DESTINATION]='" + destination + "',[STARTMILE]='" + startmile + "',[FINALMILE]='" + finalmile + "',[DRIVER]='" + driver + "',[COTROLLER]='" + cotroller + "',[UPRICE]='" + uprice + "',[UOIL]='" + uoil + "',[MONEY]='" + money + "',[NOTE]='" + note + "' where ID =" + id + "";
            }
            else
            {
                sqltext = "insert into TBOM_CARRECORD(YEAR,MONTH,CARNUM,TIME,ORIGIN,DESTINATION,STARTMILE,FINALMILE,DRIVER,COTROLLER,UPRICE,UOIL,MONEY,NOTE) values('" + year + "','" + month + "','" + carnum + "','" + time + "','" + origin + "','" + destination + "','" + startmile + "','" + finalmile + "','" + driver + "','" + cotroller + "','" + uprice + "','" + uoil + "','" + money + "','" + note + "')";
            }
            DBCallCommon.ExeSqlText(sqltext);
            Response.Redirect("OM_CarRecord.aspx");
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_CarRecord.aspx");
        }
    }
}
