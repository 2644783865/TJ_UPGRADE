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
using System.Drawing;
using System.Data.SqlClient;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_MTOSplitResult : System.Web.UI.Page
    {
        protected float tzn1 = 0;
        protected int tzfzn1 = 0;
       
        protected float tzn2 = 0;
        protected int tzfzn2 = 0;
       
        
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


            string sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard," +
                    "Fixed AS Fixed,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(TZNUM as float) AS TZN,cast(TZFZNUM as float) AS TZQuantity," +
                    "PTCFrom AS PTCFrom,PTCTo AS PTCTo," +
                    "Warehouse AS Warehouse,Location AS Position," +
                    "OrderCode AS OrderCode,PlanMode AS PlanMode,DetailNote AS Comment" +
                    " FROM View_SM_MTO WHERE MTOCode='" + childcode + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard," +
                    "Fixed AS Fixed,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(TZNUM as float) AS TZN,cast(TZFZNUM as float) AS TZQuantity," +
                    "PTCFrom AS PTCFrom,PTCTo AS PTCTo," +
                    "Warehouse AS Warehouse,Location AS Position," +
                    "OrderCode AS OrderCode,PlanMode AS PlanMode,DetailNote AS Comment" +
                    " FROM View_SM_MTO WHERE MTOCode='" + parentcode + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView2.DataSource = dt;
            GridView2.DataBind();   
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tzn1 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "TZN"));
                tzfzn1 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TZQuantity"));
               
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[12].FindControl("LabelTotalTZNum")).Text = tzn1.ToString();
                ((Label)e.Row.Cells[13].FindControl("LabelTotalTZQuantity")).Text = tzfzn1.ToString();
               
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tzn2 += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "TZN"));
                tzfzn2 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TZQuantity"));
               
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[12].FindControl("LabelTotalTZNum")).Text = tzn2.ToString();
                ((Label)e.Row.Cells[13].FindControl("LabelTotalTZQuantity")).Text = tzfzn2.ToString();                
               
            }
        }


        /*
         * 合并
         */

        protected void Merge_Click(object sender, EventArgs e)
        {
            string subcode = Request.QueryString["IDC"].ToString(); //子单
            string constr = DBCallCommon.GetStringValue("connectionStrings");          

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("MTOMerge", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SubMTOCode", SqlDbType.VarChar, 50);	//增加参数入库单号@InCode
            cmd.Parameters["@SubMTOCode"].Value = subcode;							//为参数初始化，子单号
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

                constr = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard," +
                    "Fixed AS Fixed,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(TZNUM as float) AS TZN,cast(TZFZNUM as float) AS TZQuantity," +
                    "PTCFrom AS PTCFrom,PTCTo AS PTCTo," +
                    "Warehouse AS Warehouse,Location AS Position," +
                    "OrderCode AS OrderCode,PlanMode AS PlanMode,DetailNote AS Comment" +
                    " FROM View_SM_MTO WHERE MTOCode='" + parentcode + "'";
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
            Response.Redirect("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID=" + LabelChildCode.Text.Trim());
        }
    
    }
}
