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
    public partial class FM_Create_WareHouseInvDetail : System.Web.UI.Page
    {
        string wgcode = "";
        string arrayWGCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            wgcode = Request.QueryString["wgcode"];
            arrayWGCode = Request.QueryString["arrayWGCode"];
            //单个入库单明细
            if (wgcode != null)
            {
                lblwgCode.Text = "-" + wgcode;
                string sqltext = "select WG_CODE,WG_MARID,MNAME,GUIGE,CGDW,CAIZHI,GB,WG_LOTNUM,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_UPRICE,WG_TAXRATE,WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_ORDERID,ClerkName,DocName,WG_PTCODE,WG_NOTE from View_SM_IN where WG_CODE='" + wgcode + "'order by WG_UNIQUEID";
                rptWGDetail.DataSource = DBCallCommon.GetDTUsingSqlText(sqltext);
                rptWGDetail.DataBind();
            }
            //全部明细
            else if (arrayWGCode != null)
            {
                if (arrayWGCode.Contains("/"))
                {
                    DataTable mondt = new DataTable();
                    for (int i = 0; i < arrayWGCode.Split('/').Length; i++)
                    {
                        string incode = arrayWGCode.Split('/')[i].ToString();
                        string sqltext = "select WG_CODE,WG_MARID,MNAME,GUIGE,CGDW,CAIZHI,GB,WG_LOTNUM,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_UPRICE,WG_TAXRATE,WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_ORDERID,ClerkName,DocName,WG_PTCODE,WG_NOTE from View_SM_IN where WG_CODE='" + incode + "' order by WG_UNIQUEID";
                        DataTable dt = new DataTable();
                        dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        mondt = DTCombine(mondt,dt, i);
                    }
                    rptWGDetail.DataSource = mondt;
                    rptWGDetail.DataBind();
                }
                else
                {
                    string sqltext = "select WG_CODE,WG_MARID,MNAME,GUIGE,CGDW,CAIZHI,GB,WG_LOTNUM,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_UPRICE,WG_TAXRATE,WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_ORDERID,ClerkName,DocName,WG_PTCODE,WG_NOTE from View_SM_IN where WG_CODE='" + arrayWGCode + "' order by WG_UNIQUEID"; 
                    rptWGDetail.DataSource = DBCallCommon.GetDTUsingSqlText(sqltext);
                    rptWGDetail.DataBind();
                }
            }
        }

        protected DataTable DTCombine(DataTable oldDT, DataTable newDT,int i)
        {
            DataTable MonDT;
            if (i == 0)
            {
                MonDT = newDT.Clone();
                object[] obj = new object[newDT.Columns.Count];
                for (int j = 0; j < newDT.Rows.Count; j++)
                {
                    newDT.Rows[j].ItemArray.CopyTo(obj, 0);
                    MonDT.Rows.Add(obj);
                }
            }
            else 
            {
                MonDT = oldDT.Clone();
                object[] obj = new object[oldDT.Columns.Count];
                for (int j = 0; j < oldDT.Rows.Count; j++)
                {
                    oldDT.Rows[j].ItemArray.CopyTo(obj, 0);
                    MonDT.Rows.Add(obj);
                }
                //object[] obj = new object[newDT.Columns.Count];
                for (int j = 0; j < newDT.Rows.Count; j++)    
                {
                    newDT.Rows[j].ItemArray.CopyTo(obj, 0);
                    MonDT.Rows.Add(obj);    
                }
            }
            return MonDT;
        }
    }
}
