using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseIN_PositionChoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = string.Empty;

                if (Request.QueryString["mcode"] == null)
                {
                    sql = "SELECT DISTINCT WL_ID,WL_NAME FROM TBWS_LOCATION WHERE WL_WSID='" + Request.QueryString["WSCODE"].ToString() + "'  order by WL_NAME";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    RadioButtonListAllWL.DataTextField = "WL_NAME";
                    RadioButtonListAllWL.DataValueField = "WL_ID";
                    RadioButtonListAllWL.DataSource = dt;
                    RadioButtonListAllWL.DataBind();
                }
                else
                {
                    string mcode = Request.QueryString["mcode"].ToString();
                    sql = "SELECT DISTINCT WL_ID,WL_NAME FROM TBWS_LOCATION WHERE WL_WSID='" + Request.QueryString["WSCODE"].ToString() + "' and  WL_ID in ( select SQ_LOCATION from TBWS_STORAGE where SQ_MARID='" + mcode + "' ) order by WL_NAME ";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    dt = DBCallCommon.GetDTUsingSqlText(sql);
                    RadioButtonListWL.DataTextField = "WL_NAME";
                    RadioButtonListWL.DataValueField = "WL_ID";
                    RadioButtonListWL.DataSource = dt;
                    RadioButtonListWL.DataBind();
                   
                    sql = "SELECT DISTINCT WL_ID,WL_NAME FROM TBWS_LOCATION WHERE WL_WSID='" + Request.QueryString["WSCODE"].ToString() + "'  order by WL_NAME";
                    dt = DBCallCommon.GetDTUsingSqlText(sql);
                    RadioButtonListAllWL.DataTextField = "WL_NAME";
                    RadioButtonListAllWL.DataValueField = "WL_ID";
                    RadioButtonListAllWL.DataSource = dt;
                    RadioButtonListAllWL.DataBind();
                }

                sql = "SELECT DISTINCT WS_NAME FROM TBWS_WAREHOUSE WHERE WS_ID='" + Request.QueryString["WSCODE"].ToString() + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelWS.Text = dr["WS_NAME"].ToString();
                }

                dr.Close();
            }
        }


        protected void RadioButtonListWL_SelecedIndexChanged(object sender, EventArgs e)
        {

            string lcode = "0";
            string lname = "待查";

            RadioButtonList objRadioButtonList = sender as RadioButtonList;

            for (int i = 0; i < objRadioButtonList.Items.Count; i++)
            {
                if (objRadioButtonList.Items[i].Selected)
                {
                    lcode = objRadioButtonList.Items[i].Value;
                    lname = objRadioButtonList.Items[i].Text;
                }
            }
            Response.Write("<script>javascript:window.returnValue =new Array('" + lcode + "','" + lname + "');window.close();</script>");
        }

    }
}
