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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_STOUSE_MTO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindStoUse(Server.UrlDecode(Request.QueryString["ID"]));
            }
        }

        private void BindStoUse(string pid)
        {
            string sql = "select planno,pjnm,engnm,engid,ptcode,marid,marnm,margg,marcz,margb,length,width,marunit,cast(num as float) as num,cast(usenum as float) as usenum,allnote,PUR_ISSTOUSE from View_TBPC_MARSTOUSEALL where planno='" + pid + "' and allshstate='2'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "ShowStoUseModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["ptcode"].ToString()) + "','" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["planno"].ToString()) + "');");
                if (GridView1.DataKeys[e.Row.RowIndex]["PUR_ISSTOUSE"].ToString() == "1")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                }
                else if (GridView1.DataKeys[e.Row.RowIndex]["PUR_ISSTOUSE"].ToString() == "2")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Blue;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Blue;
                }
            }
        }

        protected void btn_adjust_Click(object sender, EventArgs e)
        {
            string selsql = "select a.* from TBPC_MARSTOUSEALLDETAIL AS a left OUTER JOIN TBPC_MARSTOUSEALL AS b on a.PUR_PTCODE = b.PUR_PTCODE AND a.PUR_PCODE = b.PUR_PCODE where a.PUR_PCODE='" + Server.UrlDecode(Request.QueryString["ID"]) + "' and b.PUR_ISSTOUSE='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(selsql);

            if (dt.Rows.Count > 0)
            {
                List<string> sqllist = new List<string>();

                string Code = generateMTOCode();

                string strsql = "insert into TBWS_MTOCODE (MTO_CODE) VALUES ('" + Code + "')";

                DBCallCommon.ExeSqlText(strsql);

                string sql = "";

                string Date = System.DateTime.Now.ToString("yyyy-MM-dd");
                string TargetCode = "备库";

                string PlanerCode = Session["UserID"].ToString();
                string DepCode = "07";
                string DocCode = Session["UserID"].ToString();
                string VerifierCode = "";
                string ApproveDate = "";
                string Comment = "";

                sql = "DELETE FROM TBWS_MTO WHERE MTO_CODE='" + Code + "'";
                sqllist.Add(sql);

                sql = "INSERT INTO TBWS_MTO(MTO_CODE,MTO_DATE,MTO_TARGETID," +
                    "MTO_DEP,MTO_PLANER,MTO_DOC,MTO_VERIFIER,MTO_VERIFYDATE," +
                    "MTO_STATE,MTO_NOTE) VALUES('" + Code + "','" +
                    Date + "','" + TargetCode + "','" + DepCode + "','" + PlanerCode + "','" + DocCode + "','" +
                    VerifierCode + "','" + ApproveDate + "','1','" + Comment + "')";

                sqllist.Add(sql);

                sql = "DELETE FROM TBWS_MTODETAIL WHERE MTO_CODE='" + Code + "'";

                sqllist.Add(sql);


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');

                    string MaterialCode = dt.Rows[i]["PUR_MARID"].ToString();

                    string Fixed = dt.Rows[i]["PUR_FIXED"].ToString();

                    string Length = dt.Rows[i]["PUR_LENGTH"].ToString();

                    string Width = dt.Rows[i]["PUR_WIDTH"].ToString();

                    string LotNumber = dt.Rows[i]["PUR_LotNumber"].ToString();

                    string PTCTO = dt.Rows[i]["PUR_PTCODE"].ToString();

                    string WarehouseCode = dt.Rows[i]["PUR_WarehouseCode"].ToString();

                    string PositionCode = dt.Rows[i]["PUR_PositionCode"].ToString();

                    string SQCODE = dt.Rows[i]["PUR_SQCODE"].ToString();

                    string DN = dt.Rows[i]["PUR_NUM"].ToString();

                    string RN = dt.Rows[i]["PUR_USTNUM"].ToString();

                    string PlanMode = dt.Rows[i]["PUR_PlanMode"].ToString();

                    string OrderID = dt.Rows[i]["PUR_OrderCode"].ToString();

                    string Note = "";

                    sql = "INSERT INTO TBWS_MTODETAIL(MTO_UNIQUEID,MTO_CODE,MTO_SQCODE,MTO_MARID," +
                           "MTO_FIXED,MTO_LENGTH,MTO_WIDTH,MTO_PCODE,MTO_FROMPTCODE," +
                           "MTO_WAREHOUSE,MTO_LOCATION,MTO_KTNUM,MTO_KTFZNUM,MTO_TOPTCODE,MTO_TZNUM," +
                           "MTO_TZFZNUM,MTO_PMODE,MTO_ORDERID,MTO_NOTE,MTO_STATE) VALUES('" + UniqueID + "','" +
                           Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                           Length + "','" + Width + "','" + LotNumber + "','备库','" + WarehouseCode + "','" + PositionCode + "','" +
                           DN + "','0','" + PTCTO + "','" + RN + "','0','" + PlanMode + "','" +
                           OrderID + "','" + Note + "','')";

                    sqllist.Add(sql);

                    sql = "update TBPC_MARSTOUSEALL set PUR_OPERSTATE='" + Code + Session["UserID"].ToString() + "' where PUR_PCODE='" + Server.UrlDecode(Request.QueryString["ID"]) + "' and PUR_PTCODE='" + PTCTO + "'";

                    sqllist.Add(sql);

                }

                DBCallCommon.ExecuteTrans(sqllist);

                sqllist.Clear();

                //string script = @"window.open('SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID=" + Code + "');";

                Response.Redirect("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID=" + Code);
            }
            else
            {
                Response.Redirect("SM_Warehouse_MTOAdjust.aspx?FLAG=PUSHMTO&&ID=NEW");
            }

        }

        //生成MTO单号
        protected string generateMTOCode()
        {
            string Code = "";
            string sql = "SELECT MAX(MTO_CODE) AS MaxCode FROM TBWS_MTOCODE WHERE LEN(MTO_CODE)=10";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                if (dr["MaxCode"] != DBNull.Value)
                {
                    Code = dr["MaxCode"].ToString();
                }
            }
            dr.Close();
            if (Code == "")
            {
                return "MTO0000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((Code.Substring(3, 7)));
                tempnum++;
                Code = "MTO" + tempnum.ToString().PadLeft(7, '0');
                return Code;
            }
        }
    }
}
