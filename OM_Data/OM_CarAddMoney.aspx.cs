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
    public partial class OM_CarAddMoney : System.Web.UI.Page
    {
        string action = string.Empty;
        string carnum = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString();
            }
            if (Request.QueryString["flag"] != null)
            {
                carnum = Request.QueryString["flag"].ToString();
            }
            if (!this.IsPostBack)
            {
                if (action == "add")
                {
                   
                    ddlDRIVER();
                    ddlcard();
                    txtrq.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    //leixing.Value = ddl_leixing.SelectedValue.ToString();
                    //lblAddtime.Text = DateTime.Now.ToString();
                    //txtCarNum.Text = ddlcarnum.SelectedValue.Trim();
                }

            }
        }

        private void ddlcard()
        {
            ddl_card.Items.Clear();
            string sql = "select distinct(CARDID) as CARDID FROM TBOM_CAROILCARD";
            string datavalue = "CARDID";
            DBCallCommon.BindDdl(ddl_card, sql, datavalue, datavalue);

        }
        protected void ddl_leixing_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_leixing.SelectedValue == "card")
            {
                string sql = "select CARDID FROM TBOM_CAROILCARD" ;
                DataTable DT = DBCallCommon.GetDTUsingSqlText(sql);
                if (DT.Rows.Count > 0)
                {
                    ddl_card.SelectedValue = DT.Rows[0]["CARDID"].ToString();

                }
                ddl_card.Visible = true;

                before_ye.Visible = true;
                lbl_bye.Visible = true;
                je.Visible = true;
                txt_czje.Visible = true;
                lbl_zzye.Visible = true;
                zzye.Visible = true;
            }
            if (ddl_leixing.SelectedValue == "cash")
            {
                ddl_card.Visible = false;

                before_ye.Visible = false;
                lbl_bye.Visible = false;
                je.Visible = false;
                txt_czje.Visible = false;
                lbl_zzye.Visible = false;
                zzye.Visible = false;
            }
        }
        protected void ddl_card_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlL = "select CARDYE from TBOM_CAROILCARD WHERE CARDID='" + ddl_card.SelectedValue.ToString() + "'";
            DataTable TT = DBCallCommon.GetDTUsingSqlText(sqlL);
            if (TT.Rows.Count > 0)
            {

                lbl_bye.Value = TT.Rows[TT.Rows.Count - 1]["CARDYE"].ToString();

            }
        }
        private void ddlDRIVER()
        {
            txtDriver.Items.Clear();
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO WHERE ST_POSITION='0202'";
            string datetext = "ST_NAME";
            string datavalue = "ST_ID";
            DBCallCommon.BindDdl(txtDriver, sql, datetext, datavalue);
            txtDriver.SelectedValue = Session["UserID"].ToString();
        }
        protected void btnOK_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            string je = txt_czje.Value.ToString();
            if (action == "add")
            {

                    if (je == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入充值金额！')", true);
                        return;
                    }
                    else
                    {
                        //查询该车牌号下ID最大的一条记录，即最后更新的一条记录
                        //string sql = "select CARLICHENG,RQ FROM TBOM_CAROIL WHERE CARNUM='" + ddlcarnum.SelectedValue.Trim() + "'and ID in (select max(ID) FROM TBOM_CAROIL WHERE CARNUM='" + ddlcarnum.SelectedValue.Trim() + "')";
                        //DataTable DT = DBCallCommon.GetDTUsingSqlText(sql);
                        //if (DT.Rows.Count > 0)
                        //{
                        //    double lichenng1 = Convert.ToDouble(DT.Rows[0]["CARLICHENG"].ToString());//最近一条记录的里程数
                        //    //=================
                        //    double ss = Convert.ToDouble(licheng.Value.ToString());//填写的里程数
                        //    double oil = Convert.ToDouble(txtOilNum.Value.ToString());//加油量
                        //    int compare_time = 0;
                        //    string txtrp_time = txtrq.Text.ToString().Trim();//填写的时间   
                        //    string DT_time = DT.Rows[0]["RQ"].ToString();//数据库时间
                        //    compare_time = string.Compare(txtrq.Text.ToString().Trim(), DT.Rows[0]["RQ"].ToString());
                        //    //DateTime dt_now = DateTime.Parse(txtrq.Text.ToString().Trim());//填写的时间
                        //    //DateTime dt_SQL = DateTime.Parse(DT.Rows[0]["RQ"].ToString());//数据库最后一条数据的时间
                        //    if (ss <= lichenng1 && compare_time >= 0)//如果填写的里程数小于数据库里面的里程数，而时间却较大则跳出提示
                        //    {
                        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请注意所填里程数和时间！')", true);
                        //        return;
                        //    }
                        //    else
                        //    {
                        //        if (compare_time <= 0 && ss >= lichenng1)
                        //        {
                        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请注意所填里程数和时间！')", true);
                        //            return;
                        //        }
                        //        double tt1 = Math.Abs(ss - lichenng1);
                        //        double tt = oil / tt1;
                        //        tt = Math.Round(tt, 2);
                                //===============================在加油记录表中插入一条数据，即为此次添加记录
                                sqltext = "insert into TBOM_CAROIL(RQ,DRIVER,DRIVERID,NOTE,CARDID,CARDYE,TYPE,CARDBYE,CARDCZ) " +
                                    "values('" + txtrq.Text.Trim() + "','" + txtDriver.SelectedItem.Text.Trim() + "','" + txtDriver.SelectedValue.Trim() + "','" +
                                    "" + txtNote.Text.Trim() + "','" + ddl_card.SelectedValue.ToString() + "','" + zzye.Value.ToString() + "','" +
                                    "" + ddl_leixing.SelectedValue.ToString() + "','" + lbl_bye.Value.ToString() + "','" + txt_czje.Value.ToString() + "')";
                                DBCallCommon.ExeSqlText(sqltext);
                                //更新花费后卡金额，充值金额
                                string SQL = "UPDATE TBOM_CAROILCARD SET CARDYE='" + zzye.Value.ToString() + "' ,CARDCZ='" + txt_czje.Value.ToString() + "' WHERE CARDID='" + ddl_card.SelectedValue.ToString() + "'";
                                DBCallCommon.ExeSqlText(SQL);

                                //更新花费后卡金额
                                string SQL2 = "UPDATE TBOM_CARINFO SET CARDYE='" + zzye.Value.ToString() + "'  WHERE CARD='" + ddl_card.SelectedValue.ToString() + "'";
                                DBCallCommon.ExeSqlText(SQL2);
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('增添记录成功！');window.close();", true);
                                Response.Redirect("OM_CarWeihu.aspx");
                            }
                        }
                        //return;
                    }
   
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_CarWeihu.aspx");
        }
    }
}
