using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseIn_ActionResult : System.Web.UI.Page
    {
        protected float tn1 = 0;
        protected int tsn1 = 0;
        protected float ta1 = 0;
        protected float tcta1 = 0;
        protected float tn2 = 0;
        protected int tsn2 = 0;
        protected float ta2 = 0;
        protected float tcta2 = 0;
        
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
                Verification.Visible = false;
                Verification.Enabled = false;
                FVerification.Visible = false;
            }
            if (result.Contains("M") == true)
            {
                LabelAction.Text = "合并";
                Verification.Visible = false;
                Verification.Enabled = false;
                FVerification.Visible = false;
                Close.Visible = false;//关闭隐藏
            }
            if (result.Contains("V") == true)
            {
                Merge.Visible = false;
                Merge.Enabled = false;
                FVerification.Visible = false;
                LabelAction.Text = "红字核销";

                Close.Visible = false;//关闭隐藏
            }
            if (result.Contains("F") == true)
            {
                Merge.Visible = false;
                Merge.Enabled = false;

                Verification.Visible = false;
                LabelAction.Text = "红字反核销";

                Close.Visible = false;//关闭隐藏
            }

            string sql = "SELECT WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                    "CAIZHI AS Attribute,GB AS GB,GUIGE AS MaterialStandard," +
                    "WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width," +
                    "WG_LOTNUM AS LotNumber,CGDW AS Unit,WG_RSNUM AS RN,WG_RSFZNUM AS Quantity," +
                    "WG_UPRICE AS UnitPrice,WG_TAXRATE AS TaxRate,WG_CTAXUPRICE AS CTUP," +
                    "WG_AMOUNT AS Amount,WG_CTAMTMNY AS CTA," +
                    "WS_NAME AS Warehouse,WL_NAME AS Position," +
                    "WG_ORDERID AS OrderCode,WG_PMODE AS PlanMode,WG_PTCODE AS PTC,WG_NOTE AS Comment" +
                    " FROM View_SM_IN WHERE WG_CODE='" + childcode + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            sql = "SELECT WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                                "CAIZHI AS Attribute,GB AS GB,GUIGE AS MaterialStandard," +
                                "WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width," +
                                "WG_LOTNUM AS LotNumber,CGDW AS Unit,WG_RSNUM AS RN,WG_RSFZNUM AS Quantity," +
                                "WG_UPRICE AS UnitPrice,WG_TAXRATE AS TaxRate,WG_CTAXUPRICE AS CTUP," +
                                "WG_AMOUNT AS Amount,WG_CTAMTMNY AS CTA," +
                                "WS_NAME AS Warehouse,WL_NAME AS Position," +
                                "WG_ORDERID AS OrderCode,WG_PMODE AS PlanMode,WG_PTCODE AS PTC,WG_NOTE AS Comment" +
                                " FROM View_SM_IN WHERE WG_CODE='" + parentcode + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView2.DataSource = dt;
            GridView2.DataBind();   
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tn1 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RN"));
                tsn1 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                ta1 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "Amount"));
                tcta1 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "CTA"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[11].FindControl("LabelTotalNum")).Text = tn1.ToString();
                ((Label)e.Row.Cells[12].FindControl("LabelTotalQN")).Text = tsn1.ToString();
                ((Label)e.Row.Cells[16].FindControl("LabelTotalAmount")).Text = ta1.ToString();
                ((Label)e.Row.Cells[17].FindControl("LabelTotalCTA")).Text = tcta1.ToString();
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tn2 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RN"));
                tsn2 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                ta2 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "Amount"));
                tcta2 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "CTA"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[11].FindControl("LabelTotalNum")).Text = tn2.ToString();
                ((Label)e.Row.Cells[12].FindControl("LabelTotalQN")).Text = tsn2.ToString();                
                ((Label)e.Row.Cells[16].FindControl("LabelTotalAmount")).Text = ta2.ToString();
                ((Label)e.Row.Cells[17].FindControl("LabelTotalCTA")).Text = tcta2.ToString();
            }
        }


        /*
         * 合并
         */

        protected void Merge_Click(object sender, EventArgs e)
        {
            string code = Request.QueryString["IDC"].ToString();
            string constr = DBCallCommon.GetStringValue("connectionStrings");

            string sql = "select WG_CAVFLAG,WG_GJSTATE from TBWS_IN where WG_CODE='" + code + "'";

            DataSet ds = DBCallCommon.FillDataSet(sql);
            if(ds.Tables[0].Rows.Count>0)
            {
                if (ds.Tables[0].Rows[0]["WG_CAVFLAG"].ToString() == "1")
                {
                    LabelResult.ForeColor = Color.Red;
                    LabelResult.Text = "已核销的子入库单，不能合并！";
                    return;
                }
                if (ds.Tables[0].Rows[0]["WG_GJSTATE"].ToString() == "1")
                {
                    LabelResult.ForeColor = Color.Red;
                    LabelResult.Text = "已勾稽的子入库单，不能合并！";
                    return;
                }
            }

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("WGMerge", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InCode", SqlDbType.VarChar, 50);	//增加参数入库单号@InCode
            cmd.Parameters["@InCode"].Value = code;							//为参数初始化
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

                constr = "SELECT WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                    "CAIZHI AS Attribute,GB AS GB,GUIGE AS MaterialStandard," +
                    "WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width," +
                    "WG_LOTNUM AS LotNumber,CGDW AS Unit,WG_RSNUM AS RN,WG_RSFZNUM AS Quantity," +
                    "WG_UPRICE AS UnitPrice,WG_TAXRATE AS TaxRate,WG_CTAXUPRICE AS CTUP," +
                    "WG_AMOUNT AS Amount,WG_CTAMTMNY AS CTA," +
                    "WS_NAME AS Warehouse,WL_NAME AS Position," +
                    "WG_ORDERID AS OrderCode,WG_PMODE AS PlanMode,WG_PTCODE AS PTC,WG_NOTE AS Comment" +
                    " FROM View_SM_IN WHERE WG_CODE='" + parentcode + "'";
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
        /*
         * 核销
         */
        protected void Verification_Click(object sender, EventArgs e)
        {
            string code = Request.QueryString["IDC"].ToString();
            string sql = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection con = new SqlConnection(sql);
            con.Open();
            SqlCommand cmd = new SqlCommand("Verification", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RedCode", SqlDbType.VarChar, 50);	    //增加参数入库单号@RedCode
            cmd.Parameters["@RedCode"].Value = code;							//为参数初始化
            cmd.Parameters.Add("@Information", SqlDbType.VarChar, 50).Direction = ParameterDirection.ReturnValue;			//增加输出参数@Information
            cmd.ExecuteNonQuery();
            con.Close();
            if (cmd.Parameters["@Information"].Value.ToString() == "0")
            {
                LabelResult.Text = "核销成功！";
            }
            if (cmd.Parameters["@Information"].Value.ToString() == "1")
            {
                LabelResult.ForeColor = Color.Red;
                LabelResult.Text = "所选红字入库单与原入库单不匹配无法核销，请检查！";
            }

            Verification.Enabled = false; //核销不可以

            Back.Visible = false;

            Close.Visible = true;//关闭显示
        }


        /*
         * 反核销
         */
        protected void FVerification_Click(object sender, EventArgs e)
        {
            string code = Request.QueryString["IDC"].ToString();
            string sql = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection con = new SqlConnection(sql);
            con.Open();
            SqlCommand cmd = new SqlCommand("AntiVerification", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RedCode", SqlDbType.VarChar, 50);	    //增加参数入库单号@RedCode
            cmd.Parameters["@RedCode"].Value = code;							//为参数初始化
            cmd.Parameters.Add("@Information", SqlDbType.VarChar, 50).Direction = ParameterDirection.ReturnValue;			//增加输出参数@Information
            cmd.ExecuteNonQuery();
            con.Close();
            if (cmd.Parameters["@Information"].Value.ToString() == "0")
            {
                LabelResult.Text = "反核销成功！";
            }
            if (cmd.Parameters["@Information"].Value.ToString() == "1")
            {
                LabelResult.ForeColor = Color.Red;
                LabelResult.Text = "所选红字入库单与原入库单不匹配无法核销，请检查！";
            }

            FVerification.Enabled = false; //反核销不可见

            Back.Visible = false;

            Close.Visible = true;//关闭显示
        }



        protected void Back_Click(object sender, EventArgs e)
        {
            if ((LabelChildCode.Text.Trim()).Contains('G'))
            {
                Response.Redirect("SM_WarehouseIN_WG.aspx?FLAG=READ&&ID=" + LabelChildCode.Text.Trim());
            }
            else
            {               
              Response.Redirect("SM_WarehouseIN_WG_WTB.aspx?FLAG=READ&&ID=" + LabelChildCode.Text.Trim());                
            }
        }
    }
}
