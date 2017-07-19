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
using System.Drawing;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseOut_LL_ActionResult : System.Web.UI.Page
    {
        protected float tn1 = 0;
        protected int tsn1 = 0;
       
        protected float tn2 = 0;
        protected int tsn2 = 0;
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataLoad();
            }
        }

        protected void DataLoad()
        {
            string parentcode = Request.QueryString["IDP"].ToString();

            string childcode = Request.QueryString["IDC"].ToString();

            string result = Request.QueryString["RES"].ToString();

            LabelParentCode.Text = parentcode;
            LabelChildCode.Text = childcode;


            if (result.Contains("S") == true)
            {
                LabelAction.Text = "拆分";
                LabelResult.Text = "拆分成功！";
                Merge.Visible = false;
                Merge.Enabled = false;
                Back.Visible = false;//返回隐藏
            }
            if (result.Contains("M") == true)
            {
                LabelAction.Text = "合并";
                Close.Visible = false;//关闭隐藏
            }

            string sqlid = "select ID from TBWS_OUT where OP_CODE='" + parentcode + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlid);
            if (dr.Read())
            {
                LabelTrueParentCode.Text=dr["id"].ToString();
            }

            dr.Close();

            sqlid = "select id from TBWS_OUT where OP_CODE='" + childcode + "'";
            dr = DBCallCommon.GetDRUsingSqlText(sqlid);
            if (dr.Read())
            {
                LabelTrueChildCode.Text = dr["id"].ToString();
            }
            dr.Close();

            string sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(RealNumber as float) AS RN,cast(RealSupportNumber as float) AS RQN," +
                    "cast(UnitPrice as float) AS UnitPrice,cast(Amount as float) AS Amount," +
                    "Warehouse AS Warehouse,Location AS Position," +
                    "PTC AS PTC,DetailNote AS Comment " +
                    " FROM View_SM_OUT WHERE OutCode='" + childcode + "' ORDER BY UniqueCode ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(RealNumber as float) AS RN,cast(RealSupportNumber as float) AS RQN," +
                    "cast(UnitPrice as float) AS UnitPrice,cast(Amount as float) AS Amount," +
                    "Warehouse AS Warehouse,Location AS Position," +
                    "PTC AS PTC,DetailNote AS Comment " +
                    " FROM View_SM_OUT WHERE OutCode='" + parentcode + "' order by UniqueCode ";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tn1 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RN"));
                tsn1 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RQN"));
             
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[11].FindControl("LabelTotalNum")).Text = tn1.ToString();
                ((Label)e.Row.Cells[12].FindControl("LabelTotalQN")).Text = tsn1.ToString();
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tn2 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RN"));
                tsn2 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RQN"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[11].FindControl("LabelTotalNum")).Text = tn2.ToString();
                ((Label)e.Row.Cells[12].FindControl("LabelTotalQN")).Text = tsn2.ToString();
            }
        }

        /*
       * 合并
       */

        protected void Merge_Click(object sender, EventArgs e)
        {
            string code = Request.QueryString["IDC"].ToString();
            string constr = DBCallCommon.GetStringValue("connectionStrings");

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("MergeOut", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@sOutCode", SqlDbType.VarChar, 50);	//增加参数入库单号@InCode
            cmd.Parameters["@sOutCode"].Value = code;							//为参数初始化
            cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            con.Close();

            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
            {
                LabelResult.Text = "合并成功！";

                Merge.Enabled = false;

                Back.Visible = false;

                Close.Visible = true;//关闭显示

                GridView1.Visible = false;

                string parentcode = Request.QueryString["IDP"].ToString();

                constr = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                     "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length,Width AS Width," +
                     "LotNumber AS LotNumber,Unit AS Unit,cast(RealNumber as float) AS RN,cast(RealSupportNumber as float) AS RQN," +
                     "cast(UnitPrice as float) AS UnitPrice,cast(Amount as float) AS Amount," +
                     "Warehouse AS Warehouse,Location AS Position," +
                     "PTC AS PTC,DetailNote AS Comment " +
                     " FROM View_SM_OUT WHERE OutCode='" + parentcode + "' ORDER BY UniqueCode ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(constr);
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                LabelResult.ForeColor = Color.Red;
                LabelResult.Text = "合并失败！";
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            if(LabelChildCode.Text.Contains("QT"))
            {
              Response.Redirect("SM_WarehouseOUT_QT.aspx?FLAG=OPEN&&ID=" + LabelChildCode.Text.Trim());
            }
            else
            {
              Response.Redirect("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ID=" + LabelChildCode.Text.Trim());
            }
        }

    }
}
