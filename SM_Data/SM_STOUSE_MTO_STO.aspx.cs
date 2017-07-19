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
    public partial class SM_STOUSE_MTO_STO : System.Web.UI.Page
    {
        string pid = string.Empty;
        string ptc = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ptc"]!=null)
            {
                ptc = Server.UrlDecode(Request.QueryString["ptc"]);
            }
            if (Request.QueryString["pid"] != null)
            {
                pid = Server.UrlDecode(Request.QueryString["pid"]);
            }
            if (!IsPostBack)
            {
                BindStoUseInfo();
                BindStoData();
            }
        }
        private void BindStoUseInfo()
        {
            string sql = "select PUR_MARID,cast(PUR_USTNUM as float) as PUR_USTNUM from TBPC_MARSTOUSEALL where PUR_PCODE='" + pid + "' and PUR_PTCODE='" + ptc + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                LabelMar.Text = dr["PUR_MARID"].ToString();
                LabelUseNum.Text = dr["PUR_USTNUM"].ToString();
            }
            dr.Close();
        }


        private void BindStoData()
        {
            string sql = "select SQCODE,PTC,MaterialCode,MaterialName,Standard,Attribute,GB,Length,Width,Unit,cast(Number as float) as Number,WarehouseCode,Warehouse,LocationCode,Location,Fixed,OrderCode,LotNumber,PlanMode," + LabelUseNum.Text.Trim() + "as stouse  from View_SM_Storage where MaterialCode='" + LabelMar.Text.Trim() + "' and PTC like '%备库%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        double storagenum = 0; 
        double bgnum = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {           

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                storagenum += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Number"));
                bgnum += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "stouse"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "合计：";
                e.Row.Cells[10].Text = Math.Round(storagenum, 4).ToString();
                ((Label)e.Row.Cells[11].FindControl("LabelSumbg")).Text = Math.Round(bgnum, 4).ToString();
                //e.Row.Cells[11].Text = Math.Round(bgnum,4).ToString();

            }
        }
        protected void btn_stouse_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();

            string strsql = string.Empty;

            strsql = "delete from TBPC_MARSTOUSEALLDETAIL where PUR_PCODE='" + pid + "' and PUR_PTCODE='" + ptc + "'";

            sqllist.Add(strsql);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
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


                    strsql = "insert into TBPC_MARSTOUSEALLDETAIL (PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_USTNUM,PUR_SQCODE,PUR_WarehouseCode,PUR_PositionCode,PUR_FIXED,PUR_LotNumber,PUR_OrderCode,PUR_PlanMode) VALUES ('" + pid + "','" + ptc + "','" + marid + "','" + length + "','" + width + "','" + DN + "','" + stonum + "','" + sqcode + "','" + ws + "','" + lp + "','" + Fixed + "','" + LotNumber + "','" + OrderCode + "','" + PlanMode + "')";

                    sqllist.Add(strsql);
                }
            }
            if ((sqllist.Count == 1) && (GridView1.Rows.Count > 0))
            {
                string script = @"alert('请选择要占用的物料！');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                return;
            }
            strsql = "delete from TBPC_MARSTOUSEALLDETAIL where PUR_PCODE='" + pid + "' and PUR_PTCODE='" + ptc + "' and PUR_USTNUM=0 ";

            sqllist.Add(strsql);

            DBCallCommon.ExecuteTrans(sqllist);

            bool isuse=false;

            string selsql = "select count(*) from TBPC_MARSTOUSEALLDETAIL where PUR_PCODE='" + pid + "' and PUR_PTCODE='" + ptc + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(selsql);
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0)
                {
                   isuse=true;
                }
            }
            dr.Close();

            if (isuse)
            {
                selsql = "update TBPC_MARSTOUSEALL set PUR_ISSTOUSE='1' where PUR_PCODE='" + pid + "' and PUR_PTCODE='" + ptc + "'";
                DBCallCommon.ExeSqlText(selsql);
            }
            else
            {
                selsql = "update TBPC_MARSTOUSEALL set PUR_ISSTOUSE='0' where PUR_PCODE='" + pid + "' and PUR_PTCODE='" + ptc + "'";
                DBCallCommon.ExeSqlText(selsql);
            }

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>window.returnValue = true; window.close();</script>");

        }

    }
}
