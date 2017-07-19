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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_InventoryReportFrom : System.Web.UI.Page
    {

        string id = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["ID"];

            if (!IsPostBack)
            {
                BindInfo(id);
                BindItem(id);
            }
        }
        private void BindInfo(string FromID)
        {
            string sql = "SELECT PDCode AS Code,State,Warehouse,PJ,MT,isnull(PD_KSTATE,0) as PD_KSTATE, isnull(PD_YSTATE,0) as PD_YSTATE,PlanerCode,ZDRID,VerifierCode FROM View_SM_InventorySchema WHERE PDCode='" + FromID + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
             
            if (dr.Read())
            {
                LabelCode.Text = dr["Code"].ToString();

                LabelState.Text = dr["State"].ToString();

                LabelWarehouse.Text = dr["Warehouse"].ToString();

                LabelEng.Text = dr["PJ"].ToString();

                LabelMar.Text = dr["MT"].ToString();

                LabelKState.Text = dr["PD_KSTATE"].ToString();

                LabelYState.Text = dr["PD_YSTATE"].ToString();

                LabelUserID.Text = dr["VerifierCode"].ToString();

                LabelZDR.Text = dr["ZDRID"].ToString();
            
            }

            dr.Close();

            if (LabelState.Text.Trim()=="3")
            {
                //调整图片显示
                ImageState.Visible = true;
                ImageYState.Visible = false;
                ImageKState.Visible = false;
            }
            if (LabelUserID.Text.Trim() == Session["UserID"].ToString() || LabelZDR.Text.Trim() == Session["UserID"].ToString())
            {
                btnIn.Enabled = true;
                btnOut.Enabled = true;
            }
              

        }


        //盘盈物料
        private void BindItem(string FromID)
        {
            string sql = string.Empty;

            if (RadioButtonListType.SelectedValue == "0")
            {
                btnOut.Visible = false;
                btnIn.Visible = true;

                sql = "select * from View_SM_InventoryReport where PDCode='" + FromID + "' and DiffNumber>0";

                BindYingOrKuiItem(sql,0);
            }
            else
            {
                btnOut.Visible = true;
                btnIn.Visible = false;

                sql = "select * from View_SM_InventoryReport where PDCode='" + FromID + "' and DiffNumber<0";
                BindYingOrKuiItem(sql, 1);
            }

            

        }

        private void BindYingOrKuiItem(string strsql,int state)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
           
            GridViewItem.DataSource = dt;
            GridViewItem.DataBind();
            System.Collections.Generic.List<string> ltsql = new System.Collections.Generic.List<string>();
            if (state == 0)
            {
                //盘盈
                if (dt.Rows.Count == 0)
                {

                    //第一次
                    if (!IsPostBack)
                    {
                        //状态未更改

                        if (LabelYState.Text == "0")
                        {
                            string sqlstr = "update TBWS_INVENTORYSCHEMA set PD_YSTATE='2' where PD_CODE='" + LabelCode.Text.Trim() + "'";
                            ltsql.Add(sqlstr);

                            sqlstr = "update TBWS_INVENTORYSCHEMA set PD_DONESTATE='3' where PD_YSTATE='1' and PD_KSTATE='1' and PD_CODE='" + LabelCode.Text.Trim() + "'";
                            ltsql.Add(sqlstr);

                            DBCallCommon.ExecuteTrans(ltsql);
                        }
                    }

                    btnIn.Visible = false;
                    btnInView.Visible = false;

                    btnOut.Visible = false;
                    btnOutView.Visible = false;
                 
                    ImageYState.Visible = false;
                    ImageKState.Visible = false;

                }
                else
                {
                    if (LabelYState.Text != "0")
                    {
                        
                        btnIn.Visible = false;
                        btnInView.Visible = true;

                        btnOut.Visible = false;
                        btnOutView.Visible = false;

                        if (LabelState.Text!="3")
                        {
                            //盘盈图片显示
                            if (LabelYState.Text == "2")
                            {
                                ImageYState.Visible = true;
                                ImageKState.Visible = false;
                            }
                        }
                    }
                   
                }
            }
            else
            {
                //盘亏
                if (dt.Rows.Count == 0)
                {
                   
                    if (LabelKState.Text == "0")
                    {
                        string sqlstr = "update TBWS_INVENTORYSCHEMA set PD_KSTATE='2' where PD_CODE='" + LabelCode.Text.Trim() + "'";
                        ltsql.Add(sqlstr);

                        sqlstr = "update TBWS_INVENTORYSCHEMA set PD_DONESTATE='3' where PD_YSTATE='2' and PD_KSTATE='2' and PD_CODE='" + LabelCode.Text.Trim() + "'";
                        ltsql.Add(sqlstr);

                        DBCallCommon.ExecuteTrans(ltsql);
                    }
                   

                    btnIn.Visible = false;
                    btnInView.Visible = false;

                    btnOut.Visible = false;
                    btnOutView.Visible = false;

                    ImageKState.Visible = false;
                    ImageYState.Visible = false;
               
                   
                }
                else
                {
                    if (LabelKState.Text != "0")
                    {
                        //
                        btnIn.Visible = false;
                        btnInView.Visible = false;

                        btnOut.Visible = false;
                        btnOutView.Visible = true;

                        if (LabelState.Text != "3")
                        {
                            if (LabelKState.Text == "2")
                            {
                                //盘亏图片显示
                                ImageKState.Visible = true;
                                ImageYState.Visible = false;
                            }
                        }
                    }
                    
                }

            }
            

        }

        protected void RadioButtonListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindItem(id);
        }
        //盘盈入库
        protected void btnIn_Click(object sender, EventArgs e)
        {

            string sqlstring = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection con = new SqlConnection(sqlstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("AdjustIn", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@InventoryCode", SqlDbType.VarChar, 50);				//增加参数截止日期@EndDate
            cmd.Parameters["@InventoryCode"].Value = LabelCode.Text;			        //系统封账时间
            cmd.Parameters.Add("@SessionID", SqlDbType.VarChar, 50);		//增加参数截止日期@EndDate
            cmd.Parameters["@SessionID"].Value = LabelUserID.Text.Trim();		//点击系统时的时间

            cmd.ExecuteNonQuery();
            con.Close();

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>OpenOut(1);</script>");
           
        }
        //盘亏出库
        protected void btnOut_Click(object sender, EventArgs e)
        {
            string sqlstring = DBCallCommon.GetStringValue("connectionStrings");

            SqlConnection con = new SqlConnection(sqlstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("AdjustOut", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@InventoryCode", SqlDbType.VarChar, 50);				//增加参数截止日期@EndDate
            cmd.Parameters["@InventoryCode"].Value = LabelCode.Text;			        //系统封账时间
            cmd.Parameters.Add("@SessionID", SqlDbType.VarChar, 50);		//增加参数截止日期@EndDate
            cmd.Parameters["@SessionID"].Value = LabelUserID.Text.Trim();		//点击系统时的时间

            cmd.ExecuteNonQuery();
            con.Close();

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>OpenOut(0);</script>");
        }
    }
}
