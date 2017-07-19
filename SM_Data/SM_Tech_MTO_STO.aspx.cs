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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Tech_MTO_STO : System.Web.UI.Page
    {
        string pid = string.Empty;

        string ptc = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["pid"] != null)
            {
                pid = Server.UrlDecode(Request.QueryString["pid"]);
            }
            if (Request.QueryString["ptc"] != null)
            {
                ptc = Server.UrlDecode(Request.QueryString["ptc"]);
            }
            if (!IsPostBack)
            {
                BindStoUseInfo();
                BindStoData();
            }
        }

        private void BindStoUseInfo()
        {
            string sql = "select MP_MARID,cast(MP_BGNUM as float) as MP_BGNUM from TBPC_MPTEMPCHANGE where MP_ID='" + pid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                LabelMar.Text = dr["MP_MARID"].ToString();
                LabelUseNum.Text = dr["MP_BGNUM"].ToString();
            }
            dr.Close();
        }

        private void BindStoData()
        {
            string sql = "select SQCODE,PTC,MaterialCode,MaterialName,Standard,Attribute,GB,Length,Width,Unit,cast(Number as float) as Number,WarehouseCode,Warehouse,LocationCode,Location,Fixed,OrderCode,LotNumber,PlanMode,Number as stouse from View_SM_Storage where  PTC='" + ptc + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (dt.Rows.Count == 0)
            {
                btn_stouse.Text = "关闭";
                Panelshow.Visible = true;

                ShowOutInformation();
            }
        }

        protected void ShowOutInformation()
        {
            string sqltext = "select OutCode,PTC,Dep AS DEP,left(ApprovedDate,10) AS VeryfyTIME,Sender AS Sender,Verifier,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB,Standard AS Standard,Length AS Length,Width AS Width,Unit AS Unit,cast(RealNumber as float) AS RealNum,Warehouse AS OutWarehouse,Location AS OutPosition,ZZBZNM as ZZBZ from View_SM_OUT where PTC='" + ptc + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridViewshow.DataSource = dt;
            GridViewshow.DataBind();

        }
        double storagenum = 0;
        double bgnum = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string gg = e.Row.Cells[10].Text;
                storagenum += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Number"));
                bgnum += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "stouse"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "合计：";
                e.Row.Cells[10].Text = Math.Round(storagenum,4).ToString();
                ((Label)e.Row.Cells[11].FindControl("LabelSumbg")).Text = Math.Round(bgnum, 4).ToString();
                //e.Row.Cells[11].Text = Math.Round(bgnum,4).ToString();                 
            }
        }  

        protected void btn_stouse_Click(object sender, EventArgs e)
        {

            List<string> sqllist = new List<string>();

            string strsql = string.Empty;

            strsql = "delete from TBWS_MTOBG_DETAIL where PUR_PCODE='" + pid + "' and PUR_PTCODE='" + ptc + "'";

            sqllist.Add(strsql);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked)
                {
                    string sqcode = GridView1.DataKeys[i]["SQCODE"].ToString();

                    string marid = GridView1.Rows[i].Cells[2].Text.Trim();

                    string length = GridView1.Rows[i].Cells[7].Text.Trim();

                    string width = GridView1.Rows[i].Cells[8].Text.Trim();

                    string DN = GridView1.Rows[i].Cells[10].Text.Trim();

                    string ws = (GridView1.Rows[i].FindControl("LabelWarehouseCode") as Label).Text.Trim();

                    string lp = (GridView1.Rows[i].FindControl("LabelLocationCode") as Label).Text.Trim();

                    string LotNumber = (GridView1.Rows[i].FindControl("LabelLotNumber") as Label).Text.Trim();

                    string Fixed = (GridView1.Rows[i].FindControl("LabelFixed") as Label).Text.Trim();

                    string OrderCode = (GridView1.Rows[i].FindControl("LabelOrderCode") as Label).Text.Trim();

                    string PlanMode = (GridView1.Rows[i].FindControl("LabelPlanMode") as Label).Text.Trim();

                    float stonum = 0;
                    try
                    {
                        stonum = Convert.ToSingle((GridView1.Rows[i].FindControl("TextBoxUserNum") as TextBox).Text.Trim());

                    }
                    catch { stonum = 0; }


                    strsql = "insert into TBWS_MTOBG_DETAIL (PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_USTNUM,PUR_SQCODE,PUR_WarehouseCode,PUR_PositionCode,PUR_FIXED,PUR_LotNumber,PUR_OrderCode,PUR_PlanMode) VALUES ('" + pid + "','" + ptc + "','" + marid + "','" + length + "','" + width + "','" + DN + "','" + stonum + "','" + sqcode + "','" + ws + "','" + lp + "','" + Fixed + "','" + LotNumber + "','" + OrderCode + "','" + PlanMode + "')";

                    sqllist.Add(strsql);
                }

            }
            if ((sqllist.Count == 1) && (GridView1.Rows.Count > 0))
            {
                string script = @"alert('请选择要变更的物料！');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                return;
            }

            strsql = "delete from TBWS_MTOBG_DETAIL where PUR_PCODE='" + pid + "' and PUR_PTCODE='" + ptc + "' and PUR_USTNUM=0 ";

            sqllist.Add(strsql);

            DBCallCommon.ExecuteTrans(sqllist);

            bool isuse = false;
            float sumnum = 0;
            string selsql = "select isnull(sum(PUR_USTNUM),0) as SUMTNUM  from TBWS_MTOBG_DETAIL where PUR_PCODE='" + pid + "' and PUR_PTCODE='" + ptc + "'";


            //DataTable dt = DBCallCommon.GetDTUsingSqlText(selsql);
            //if(dt.Rows.Count>0)
            //{
            //    sumnum = Convert.ToSingle(dt.Rows[0]["SUMTNUM"]);
            //    isuse = true;
            //}

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(selsql);
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    sumnum = Convert.ToSingle(dr["SUMTNUM"]);
                    if (sumnum >0)
                    {
                        isuse = true;
                    }
                }
            }

            dr.Close();

            if (isuse)
            {
                //1表示提交，2表示调整中，3表示已调整
                selsql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='2',MP_BKNUM='" + sumnum + "',MP_EXECID='" + Session["UserID"].ToString() + "' where MP_ID='" + pid + "'";
                DBCallCommon.ExeSqlText(selsql);
            }
            else
            {
                if ((sender as Button).Text == "关闭")
                {
                    selsql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='4',MP_BKNUM='" + sumnum + "',MP_EXECID='"+Session["UserID"].ToString()+"' where MP_ID='" + pid + "'";
                    DBCallCommon.ExeSqlText(selsql);
                }
                else
                {
                    selsql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='1' where MP_ID='" + pid + "'";
                    DBCallCommon.ExeSqlText(selsql);
                }
            }

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>window.returnValue = true; window.close();</script>");

        }

    }
}
