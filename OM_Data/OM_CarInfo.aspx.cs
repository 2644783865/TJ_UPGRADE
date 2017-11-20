using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarInfo : System.Web.UI.Page
    {
        string id = string.Empty;
        string action = string.Empty;
        PagerQueryParam pager = new PagerQueryParam();
        Panel p = new Panel();//添加到界面容器(DIV等          
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"].ToString();
            }
            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString();
            }
            if (!IsPostBack)
            {
                string sqlselect = "select * from TBOM_CARINFO where CARNUM='" + id + "'";
                DataSet ds = DBCallCommon.FillDataSet(sqlselect);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblCarNum.Value = ds.Tables[0].Rows[0]["CARNUM"].ToString();

                    lblCarType.Text = ds.Tables[0].Rows[0]["CARTYPE"].ToString();

                    string context = ds.Tables[0].Rows[0]["BC_CONTEXT"].ToString();

                    lblCapacity.Text = ds.Tables[0].Rows[0]["CARCAPACITY"].ToString();

                    lblMileage.Text = ds.Tables[0].Rows[0]["MILEAGE"].ToString();

                    lblOil.Text = ds.Tables[0].Rows[0]["OIL"].ToString();

                    lblManufacturer.Text = ds.Tables[0].Rows[0]["MANUFACTURER"].ToString();

                    lblColor.Text = ds.Tables[0].Rows[0]["COLOR"].ToString();

                    lblTunnage.Text = ds.Tables[0].Rows[0]["TUNNAGE"].ToString();

                    lblDate.Text = ds.Tables[0].Rows[0]["BUYDATE"].ToString();
                    txtcard.Text = ds.Tables[0].Rows[0]["CARD"].ToString();
                    //    txtfzr.Text = ds.Tables[0].Rows[0]["FZR"].ToString();
                    hlkFzr.Text = ds.Tables[0].Rows[0]["FZR"].ToString();
                    hlkFzr.NavigateUrl = "~/OM_Data/OM_DriverInfo.aspx?action=look&id=" + ds.Tables[0].Rows[0]["FZRID"].ToString();

                    lbl_cardye.Text = ds.Tables[0].Rows[0]["CARDYE"].ToString();
                    lblMudidi.Text = ds.Tables[0].Rows[0]["MUDIDI"].ToString();
                    lblChuCheSJ.Text = ds.Tables[0].Rows[0]["NOWSIJI"].ToString();
                    link_wx.NavigateUrl = "~/OM_Data/OM_CarWeihu.aspx?action=carInfoWx&carId=" + ds.Tables[0].Rows[0]["CARNUM"].ToString();
                    link_by.NavigateUrl = "~/OM_Data/OM_CarWeihu.aspx?action=carInfoBy&carId=" + ds.Tables[0].Rows[0]["CARNUM"].ToString();
                    link_jy.NavigateUrl = "~/OM_Data/OM_CarWeihu.aspx?action=carInfoJy&carId=" + ds.Tables[0].Rows[0]["CARNUM"].ToString();
                    link_CarYunXingInfo.NavigateUrl = "~/OM_Data/OM_CarRecord.aspx?action=carInfoYX&carId=" + ds.Tables[0].Rows[0]["CARNUM"].ToString();

                    if (ds.Tables[0].Rows[0]["STATE"].ToString() == "0")
                    {
                        lblState.Text = "在厂";
                    }
                    else
                    {
                        lblState.Text = "不在厂";
                    }
                    lblNote.Text = ds.Tables[0].Rows[0]["NOTE"].ToString();

                    if (context != "")
                    {
                        string sqltext = "select distinct(fileName) as fileName from tb_files where BC_CONTEXT='" + context + "'";

                        DataSet ds1 = DBCallCommon.FillDataSet(sqltext);

                        string FilePath = "";
                        string[] imagr = new string[50];
                        int n = 0;
                        string ss = "";
                        string mm = "";


                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            {
                                FilePath = ds1.Tables[0].Rows[i]["fileName"].ToString();
                                imagr[i] = "<img src='" + "../Assets/images/om/" + "2014" + "/" + FilePath + "' height=200px width=200px/>";
                                n++;


                            }
                            imagenum.Value = n.ToString();
                            for (int j = 0; j < n; j++)
                            {
                                ss = imagr[j].ToString();
                                mm += ss;
                            }
                            ImageCar.Value = mm.ToString();
                        }
                    }
                }

                Showdata();
                BindYear(ddl_wx);
                BindYear(ddl_by);
                BindYear(ddl_jy);

                double m = 0;
                string sql = "select MONEYONE FROM TBOM_CARSAFE WHERE PLACEDATE LIKE '%" + ddl_wx.SelectedValue.ToString() + "%' AND TYPEID='wx'and CARID='" + id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        m += CommonFun.ComTryDouble(dt.Rows[i]["MONEYONE"].ToString());
                    }
                }
                lbl_wx.Text = m.ToString();
                double nn = 0;
                string sqll = "select MONEYONE FROM TBOM_CARSAFE WHERE PLACEDATE LIKE '%" + ddl_by.SelectedValue.ToString() + "%' AND TYPEID='by'and CARID='" + id + "'";
                DataTable dtt = DBCallCommon.GetDTUsingSqlText(sqll);
                if (dtt.Rows.Count > 0)
                {
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {

                        nn += CommonFun.ComTryDouble(dtt.Rows[i]["MONEYONE"].ToString());
                    }
                }
                lbl_by.Text = nn.ToString();


                sql = "select * from (select CARID, cast(isnull(BYAFTER,0) as float)-cast(isnull(BYYJ,0) as float) as YJGL,PLACEDATE,BYBEFORE,BYAFTER,ROW_NUMBER() OVER(PARTITION by CARID ORDER BY PLACEDATE DESC) AS rownum from TBOM_CARSAFE where TYPEID='by' group by CARID,BYAFTER,BYYJ,PLACEDATE,BYBEFORE )a where CARID='" + id + "' and ROWNUM<=1";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lblLastBy.Text = dt.Rows[0]["BYBEFORE"].ToString();
                    lblNextBy.Text = dt.Rows[0]["BYAFTER"].ToString();
                    if (CommonFun.ComTryDouble(lblMileage.Text) > CommonFun.ComTryDouble(dt.Rows[0]["YJGL"].ToString()))
                    {
                        lblYujing.Text = "该车需要保养!";
                    }
                }


                //加油费

                double jy = 0;
                sql = "select MONEY FROM TBOM_CAROIL WHERE RQ LIKE '%" + ddl_jy.SelectedValue.ToString() + "%' and CARNUM='" + id + "'";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        jy += CommonFun.ComTryDouble(dt.Rows[i]["MONEY"].ToString());
                    }
                }
                lbl_jy.Text = jy.ToString();


                sql = "select * from TBOM_CAROIL where CarNum='" + id + "' order by RQ desc";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lblYouHao.Text = dt.Rows[0]["OILWEAR"].ToString();
                }

            }
        }
        protected void BindYear(DropDownList dpl_Year)
        {
            for (int i = 0; i < 10; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            dpl_Year.SelectedValue = DateTime.Now.Year.ToString();
        }
        //protected void BindMonth(DropDownList yuefen)
        //{
        //    for (int i = 1; i <= 12;i++ )
        //    {
        //        yuefen.Items.Add(new ListItem(i,i));

        //    }
        //    yuefen.SelectedValue = DateTime.Now.Month.ToString();
        //}

        protected void ddl_by_changed(object sender, EventArgs e)
        {
            int m = 0;
            string sql = "select MONEYONE FROM TBOM_CARSAFE WHERE PLACEDATE LIKE '%" + ddl_by.SelectedValue.ToString() + "%' AND TYPEID='by'and CARID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    m += Convert.ToInt32(dt.Rows[i]["MONEYONE"].ToString());
                }
            }
            lbl_by.Text = m.ToString();
        }


        protected void ddl_wx_changed(object sender, EventArgs e)
        {
            int m = 0;
            string sql = "select MONEYONE FROM TBOM_CARSAFE WHERE PLACEDATE LIKE '%" + ddl_wx.SelectedValue.ToString() + "%' AND TYPEID='wx'and CARID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    m += Convert.ToInt32(dt.Rows[i]["MONEYONE"].ToString());
                }
            }
            lbl_wx.Text = m.ToString();
        }
        protected void ddl_jy_changed(object sender, EventArgs e)
        {
            //加油费

            double jy = 0;
            string sql = "select MONEY FROM TBOM_CAROIL WHERE RQ LIKE '%" + ddl_jy.SelectedValue.ToString() + "%' and CARNUM='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    jy += CommonFun.ComTryDouble(dt.Rows[i]["MONEY"].ToString());
                }
            }
            lbl_jy.Text = jy.ToString();
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_Car.aspx");
        }
        private void InitVar()
        {
            if (Det_Repeater.Items.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }
        private void Showdata() //将数据绑定到textbox
        {
            //string carnum1 = "";
            //string sqltext = "select CARNUM from TBOM_CARINFO where ID = '" + id + "'";
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            //if (dr.Read())
            //{
            //    carnum1 = dr["CARNUM"].ToString();
            //}
            //dr.Close();
            string sqlText = "select BXNAME,STARTDATE,ENDDATE,NOTE,BXJE from TBOM_CARBX  where CARNUM='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();
            InitVar();
        }

    }
}
