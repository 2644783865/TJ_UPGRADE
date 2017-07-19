using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_DriverInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = string.Empty;
            string action = string.Empty;
            PagerQueryParam pager = new PagerQueryParam();
            Panel p = new Panel();//添加到界面容器(DIV等       
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
                string sqlselect = "select * from TBOM_DriverList as a left join View_TBDS_STAFFINFO as b on a.DriverId=ST_ID where DriverId='" + id + "'";
                DataSet ds = DBCallCommon.FillDataSet(sqlselect);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    string context = ds.Tables[0].Rows[0]["Context"].ToString();
                    hidContext.Value = context;
                    lblName.Text = ds.Tables[0].Rows[0]["ST_NAME"].ToString();
                    lblDep.Text = ds.Tables[0].Rows[0]["DEP_NAME"].ToString();
                    lblPos.Text = ds.Tables[0].Rows[0]["DEP_POSITION"].ToString();
                    lblWorkNo.Text = ds.Tables[0].Rows[0]["ST_WORKNO"].ToString();
                    if (context != "")
                    {
                        string sqltext = "select distinct(fileName) as fileName from tb_files where BC_CONTEXT='" + context + "'";

                        DataSet ds1 = DBCallCommon.FillDataSet(sqltext);

                        string FilePath = "";
                        string[] imagr = new string[20];
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


            }

        }


        private void Showdata() //将数据绑定到textbox
        {

            string sqlText = "select * from TBOM_DriverInfo  where Context='" + hidContext.Value + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();
        }
    }
}
