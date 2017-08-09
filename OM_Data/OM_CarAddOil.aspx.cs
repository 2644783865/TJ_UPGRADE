using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarAddOil : System.Web.UI.Page
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
                    ddl();
                    ddlDRIVER();
                    ddlcard();
                    txtrq.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    //leixing.Value = ddl_leixing.SelectedValue.ToString();
                    //lblAddtime.Text = DateTime.Now.ToString();
                    //txtCarNum.Text = ddlcarnum.SelectedValue.Trim();
                }

            }
        }
        private void ddl()
        {
            ddlcarnum.Items.Clear();
            string sql = "select CARNUM from TBOM_CARINFO";

            string datavalue = "CARNUM";
            DBCallCommon.BindDdl(ddlcarnum, sql, datavalue, datavalue);
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
                string sql = "select CARDID FROM TBOM_CAROILCARD where CARNUM='" + ddlcarnum.SelectedValue.ToString() + "'";
                DataTable DT = DBCallCommon.GetDTUsingSqlText(sql);
                if (DT.Rows.Count > 0)
                {
                    ddl_card.SelectedValue = DT.Rows[0]["CARDID"].ToString();

                }
                ddl_card.Visible = true;
 
     
                before_ye.Visible = true;
                lbl_bye.Visible = true;
  
     
                lbl_zzye.Visible = true;
                zzye.Visible = true;
            }
            if (ddl_leixing.SelectedValue == "cash")
            {
                ddl_card.Visible = false;


                before_ye.Visible = false;
                lbl_bye.Visible = false;


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
            string jine = txtMoney.Value.ToString();
            if (action == "add")
            {
                if (ddlcarnum.SelectedValue.ToString() == "-请选择-")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择车牌号！')", true);
                    return;
                }
                else
                {
                    if (jine == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入花费金额！')", true);
                        return;
                    }
                    else
                    {
                        //查询该车牌号下ID最大的一条记录，即最后更新的一条记录
                        string sql = "select CARLICHENG,RQ FROM TBOM_CAROIL WHERE CARNUM='" + ddlcarnum.SelectedValue.Trim() + "'and ID in (select max(ID) FROM TBOM_CAROIL WHERE CARNUM='" + ddlcarnum.SelectedValue.Trim() + "')";
                        DataTable DT = DBCallCommon.GetDTUsingSqlText(sql);
                        if (DT.Rows.Count > 0)
                        {
                            double lichenng1 = Convert.ToDouble(DT.Rows[0]["CARLICHENG"].ToString());//最近一条记录的里程数
                            //=================
                            double ss = Convert.ToDouble(licheng.Value.ToString());//填写的里程数
                            double oil = Convert.ToDouble(txtOilNum.Value.ToString());//加油量
                            int compare_time = 0;
                            string txtrp_time = txtrq.Text.ToString().Trim();//填写的时间   
                            string DT_time = DT.Rows[0]["RQ"].ToString();//数据库时间
                            compare_time = string.Compare(txtrq.Text.ToString().Trim(), DT.Rows[0]["RQ"].ToString());
                            //DateTime dt_now = DateTime.Parse(txtrq.Text.ToString().Trim());//填写的时间
                            //DateTime dt_SQL = DateTime.Parse(DT.Rows[0]["RQ"].ToString());//数据库最后一条数据的时间

                            #region 注释输入检查功能[2017.8.8]
                            //if (ss <= lichenng1 && compare_time >= 0)//如果填写的里程数小于数据库里面的里程数，而时间却较大则跳出提示
                            //{
                            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请注意所填里程数和时间！')", true);
                            //    return;
                            //}
                            //else
                            //{
                                //注释输入检查[2017.8.8]
                                //if (compare_time <= 0 && ss >= lichenng1)
                                //{
                                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请注意所填里程数和时间！')", true);
                                //    return;
                            //}
                            #endregion

                            #region 新增验证加油前公里数需大于最近出车结束里程数
                            //string sqladd = "select CODE,isnull(TIME2,'')TIME2,isnull(LICHENG2,'')LICHENG2 from TBOM_CARAPPLY where CARNUM like '" + ddlcarnum.SelectedValue.Trim() + "%'order by TIME2 desc";
                                //DataTable dtadd = DBCallCommon.GetDTUsingSqlText(sqladd);
                                //if (dtadd.Rows.Count > 0)
                                //{
                                //    double LICHENG2 = CommonFun.ComTryDouble(dtadd.Rows[0]["LICHENG2"].ToString());
                                //    if (ss <= LICHENG2)
                                //    {
                                //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所填里程数需大于最近出车结束里程！')", true);
                                //        return;
                                //    }
                                //}
                                #endregion

                                double tt1 = Math.Abs(ss - lichenng1);
                                double tt = oil / tt1*100;
                                tt = Math.Round(tt, 2);
                                if (ddl_leixing.SelectedValue == "card")
                                {
                                    //===============================在加油记录表中插入一条数据，即为此次添加记录
                                    sqltext = "insert into TBOM_CAROIL(CARNUM,RQ,DRIVER,DRIVERID,UPRICE,OILNUM,MONEY,NOTE,CARDID,CARDYE,TYPE,OILTYPE,CARDBYE,CARLICHENG,OILWEAR) " +
                                        "values('" + ddlcarnum.SelectedValue.Trim() + "','" + txtrq.Text.Trim() + "','" + txtDriver.SelectedItem.Text.Trim() + "','" + txtDriver.SelectedValue.Trim() + "','" +
                                        "" + txtUprice.Value.Trim() + "','" + txtOilNum.Value.Trim() + "','" + jine + "','" + txtNote.Text.Trim() + "','" + ddl_card.SelectedValue.ToString() + "','" + zzye.Value.ToString() + "','" +
                                        "" + ddl_leixing.SelectedValue.ToString() + "','" + ddl_oil.SelectedValue.ToString() + "','" + lbl_bye.Value.ToString() + "','" + ss + "','" + tt + "')";
                                    DBCallCommon.ExeSqlText(sqltext);
                                    //更新花费后卡金额，充值金额
                                    string SQL = "UPDATE TBOM_CAROILCARD SET CARDYE='" + zzye.Value.ToString() + "'  WHERE CARDID='" + ddl_card.SelectedValue.ToString() + "'";
                                    DBCallCommon.ExeSqlText(SQL);
                                    //更新花费后卡金额
                                    string SQL2 = "UPDATE TBOM_CARINFO SET CARDYE='" + zzye.Value.ToString() + "'  WHERE CARD='" + ddl_card.SelectedValue.ToString() + "'";
                                    DBCallCommon.ExeSqlText(SQL2);
                                }
                                else
                                {
                                    //===============================在加油记录表中插入一条数据，即为此次添加记录
                                    sqltext = "insert into TBOM_CAROIL(CARNUM,RQ,DRIVER,DRIVERID,UPRICE,OILNUM,MONEY,NOTE,CARDID,CARDYE,TYPE,OILTYPE,CARDBYE,CARLICHENG,OILWEAR) " +
                                        "values('" + ddlcarnum.SelectedValue.Trim() + "','" + txtrq.Text.Trim() + "','" + txtDriver.SelectedItem.Text.Trim() + "','" + txtDriver.SelectedValue.Trim() + "','" +
                                        "" + txtUprice.Value.Trim() + "','" + txtOilNum.Value.Trim() + "','" + jine + "','" + txtNote.Text.Trim() + "','-','-','" +
                                        "" + ddl_leixing.SelectedValue.ToString() + "','" + ddl_oil.SelectedValue.ToString() + "','-','" + ss + "','" + tt + "')";
                                    DBCallCommon.ExeSqlText(sqltext);
                                }
                                string sql1 = "select OIL FROM TBOM_CARINFO WHERE CARNUM='" + ddlcarnum.SelectedValue.Trim() + "'";
                                DataTable TT = DBCallCommon.GetDTUsingSqlText(sql1);
                                if (TT.Rows.Count > 0)
                                {
                                    double oil1 = Convert.ToDouble(TT.Rows[0]["OIL"].ToString());
                                    oil1 += Convert.ToDouble(txtOilNum.Value.Trim());
                                    //OIL油？
                                    sql1 = "update TBOM_CARINFO SET OIL='" + oil1 + "' WHERE CARNUM='" + ddlcarnum.SelectedValue.Trim() + "'";
                                    DBCallCommon.ExeSqlText(sql1);
                                }
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('增添记录成功！');window.close();", true);
                                Response.Redirect("OM_CarWeihu.aspx");
                            //}
                        }
                        //若为新车，新增第一条数据
                        else
                        {
                            double ss = Convert.ToDouble(licheng.Value.ToString());//填写的里程数
                            double oil = Convert.ToDouble(txtOilNum.Value.ToString());//加油量
                            string txtrp_time = txtrq.Text.ToString().Trim();//填写的时间   
                            //string DT_time = DT.Rows[0]["RQ"].ToString();//数据库时间

                            //double tt1 = Math.Abs(ss - lichenng1);
                            //double tt = oil / tt1;
                            //tt = Math.Round(tt, 2);
                            //===============================在加油记录表中插入一条数据，即为此次添加记录
                            sqltext = "insert into TBOM_CAROIL(CARNUM,RQ,DRIVER,DRIVERID,UPRICE,OILNUM,MONEY,NOTE,CARDID,CARDYE,TYPE,OILTYPE,CARDBYE,CARLICHENG,OILWEAR) " +
                                "values('" + ddlcarnum.SelectedValue.Trim() + "','" + txtrq.Text.Trim() + "','" + txtDriver.SelectedItem.Text.Trim() + "','" + txtDriver.SelectedValue.Trim() + "','" +
                                "" + txtUprice.Value.Trim() + "','" + txtOilNum.Value.Trim() + "','" + jine + "','" + txtNote.Text.Trim() + "','" + ddl_card.SelectedValue.ToString() + "','" + zzye.Value.ToString() + "','" +
                                "" + ddl_leixing.SelectedValue.ToString() + "','" + ddl_oil.SelectedValue.ToString() + "','" + lbl_bye.Value.ToString() + "','" + ss + "','')";
                            DBCallCommon.ExeSqlText(sqltext);
                            //更新花费后卡金额，充值金额
                            string SQL = "UPDATE TBOM_CAROILCARD SET CARDYE='" + zzye.Value.ToString() + "'  WHERE CARDID='" + ddl_card.SelectedValue.ToString() + "'";
                            DBCallCommon.ExeSqlText(SQL);
                            string sql1 = "select OIL FROM TBOM_CARINFO WHERE CARNUM='" + ddlcarnum.SelectedValue.Trim() + "'";
                            DataTable TT = DBCallCommon.GetDTUsingSqlText(sql1);
                            if (!string.IsNullOrEmpty(TT.Rows[0]["OIL"].ToString()))
                            {
                                double oil1 = Convert.ToDouble(TT.Rows[0]["OIL"].ToString());
                                oil1 += Convert.ToDouble(txtOilNum.Value.Trim());
                                //OIL油？
                                sql1 = "update TBOM_CARINFO SET OIL='" + oil1 + "' WHERE CARNUM='" + ddlcarnum.SelectedValue.Trim() + "'";
                                DBCallCommon.ExeSqlText(sql1);
                            }
                            else
                            {
                                double oil1 = 0;
                                oil1 += Convert.ToDouble(txtOilNum.Value.Trim());
                                //OIL油？
                                sql1 = "update TBOM_CARINFO SET OIL='" + oil1 + "' WHERE CARNUM='" + ddlcarnum.SelectedValue.Trim() + "'";
                                DBCallCommon.ExeSqlText(sql1);
                            }
                            //更新花费后卡金额
                            string SQL2 = "UPDATE TBOM_CARINFO SET CARDYE='" + zzye.Value.ToString() + "'  WHERE CARD='" + ddl_card.SelectedValue.ToString() + "'";
                            DBCallCommon.ExeSqlText(SQL2);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('增添记录成功！');window.close();", true);
                            Response.Redirect("OM_CarWeihu.aspx");
                        }
                        //return;
                    }
                }
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_CarWeihu.aspx");
        }
    }
}
