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
using System.Data.SqlClient;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_ProductNumber_Statistics : System.Web.UI.Page
    {
        Statistics stc = new Statistics();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
            if (!IsPostBack)
            {
                //CheckUser(ControlFinder);
            }
           
        }
        //定义属性
        public class Statistics
        {
            public string _StartTime;
            public string _EndTime;
            public string _State;
            public string StartTime
            {
                
                set { _StartTime = value; }
                get { return _StartTime; }
            }

            public string EndTime
            {
                
                set { _EndTime = value; }
                get { return _EndTime; }
            }
            public string State
            {
                
                set { _State = value; }
                get { return _State; }
            }
        }

        public string statetimes()
        {
            return stc.StartTime;
        }

        public string endtimes()
        {
            return stc.EndTime;
        }

        public string states()
        {
            return stc.State;
        }

        //存储过程参数传递，执行
        public void PagerQueryParam(Statistics statistcs)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "ProductNumber_Statistics");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Start_Time", statistcs.StartTime, SqlDbType.DateTime, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@End_Time", statistcs.EndTime, SqlDbType.DateTime, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@State", statistcs.State, SqlDbType.Char, 1000);
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //查询
        protected void ddlchaxun_SelectedIndexChanged(object sender, EventArgs e)
        {
            palTime.Visible = false;
            stc.State = ddlState.SelectedValue.ToString();
            switch (ddlchaxun.SelectedValue.ToString())
            {
                case "0"://全部
                    //stc.StartTime = "1900-1-1";
                    //stc.EndTime = "2100-1-1";
                    //this.BindData();
                    rptProductNumStc.DataSource = null;
                    rptProductNumStc.DataBind();
                    btn_export.Visible = false;
                    break;
                case "1"://本期
                    stc.StartTime = DateTime.Now.ToString("yyyy-MM") + "-01";
                    stc.EndTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                    this.BindData();
                    ViewState["starttime"] = stc.StartTime;
                    ViewState["endtime"] = stc.EndTime;
                    break;
                case "2"://本周
                    int weeknow = Convert.ToInt32(System.DateTime.Now.DayOfWeek);
                     //星期日 获取weeknow为0 
                    weeknow=weeknow==0?7:weeknow;
                    int daydiff = (-1) * weeknow + 1;
                    int dayadd = 7 - weeknow;
                    //本周第一天
                    stc.StartTime= System.DateTime.Now.AddDays(daydiff).ToString("yyyy-MM-dd");
                    //本周最后一天
                    stc.EndTime = System.DateTime.Now.AddDays(dayadd).ToString("yyyy-MM-dd");
                    this.BindData();
                    ViewState["starttime"] = stc.StartTime;
                    ViewState["endtime"] = stc.EndTime;
                    break;
                case "3"://当天
                    stc.StartTime = DateTime.Now.ToString("yyyy-MM-dd");
                    stc.EndTime = DateTime.Now.ToString("yyyy-MM-dd");
                    this.BindData();
                    ViewState["starttime"] = stc.StartTime;
                    ViewState["endtime"] = stc.EndTime;
                    break;             
                default:
                    palTime.Visible = true;
                    btn_export.Visible = false;
                    //this.btnQuery_Click(null, null);
                    break;
            }
        }
        //执行存储过程，绑定数据
        private void BindData()
        {
            this.PagerQueryParam(stc);//存储过程执行后直接将数据写入表TBFM_PRIDSTATISTICS
            string sqltext = "select ROW_NUMBER() OVER (ORDER BY PS_SCZH ASC) AS Row_Num, * from VIEW_TBFM_PRIDSTATISTICS order by PS_SCZH ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                rptProductNumStc.DataSource = dt;
                rptProductNumStc.DataBind();
                NoDataPanel.Visible = false;
                div_statistcs.Visible = true;
                btn_export.Visible = true;
            }
            else
            {
                rptProductNumStc.DataSource = null;
                rptProductNumStc.DataBind();
                NoDataPanel.Visible = true;
                div_statistcs.Visible = false;
                btn_export.Visible = false;
            }
            //CheckUser(ControlFinder);
        }
        //自定义时间查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            stc.StartTime = txtStartTime.Text.Trim()==""?"1900-1-1":txtStartTime.Text.Trim();
            stc.EndTime = txtEndTime.Text.Trim() == "" ? "2100-12-12" : txtEndTime.Text.Trim();
            stc.State = ddlState.SelectedValue.ToString();           
            this.BindData();
            ViewState["starttime"] = stc.StartTime;
            ViewState["endtime"] = stc.EndTime;           
        }
        //核算状态
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlchaxun_SelectedIndexChanged(null, null);
        }
        //导出
        protected void btn_export_Click(object sender, EventArgs e)
        {
            rptProductNumStc.DataSource = null;
            rptProductNumStc.DataBind();
            this.ddlchaxun_SelectedIndexChanged(null, null);//防止长时间不导出而他人操作数据得表TBFM_PRIDSTATISTICS中数据改变，故重新执行一遍
            ExportDataFromDB.ExportMatTotalDetail_ProductNum(ViewState["starttime"].ToString(), ViewState["endtime"].ToString());
        }


        private double sl1 = 0;
        private double je1 = 0;
        //private double sl2 = 0;
        private double je2 = 0; 
       // private double sl3 = 0;
        private double je3 = 0;
       // private double sl4 = 0;
        private double je4 = 0;
        //private double sl5 = 0;
        private double je5 = 0;
       // private double sl6 = 0;
        private double je6 = 0;
        //private double sl7 = 0;
        private double je7 = 0;
       // private double sl8= 0;
        private double je8 = 0;
       // private double sl9 = 0;
        private double je9 = 0;
       // private double sl10 = 0;
        private double je10 = 0;
       // private double sl11 = 0;
        private double je11 = 0;
       // private double sl12 = 0;
        private double je12 = 0;
        //private double sl13 = 0;
        private double je13 = 0;
       // private double sl14 = 0;
        private double je14 = 0;
       // private double sl15 = 0;
        private double je15 = 0;
       // private double sl16 = 0;
        private double je16 = 0;
       // private double sl17 = 0;
        private double je17 = 0;
       // private double sl18 = 0;
        private double je18 = 0;
        //private double sl19 = 0;
        private double je19 = 0;
        //private double slzj = 0;
        private double jezj = 0;

        protected void rptProductNumStc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
               
              sl1 += Convert.ToDouble(((Label)e.Item.FindControl("label2")).Text);
              je1 += Convert.ToDouble(((Label)e.Item.FindControl("label3")).Text);

              //sl2 += Convert.ToDouble(((Label)e.Item.FindControl("label4")).Text);
              je2 += Convert.ToDouble(((Label)e.Item.FindControl("label5")).Text);

             // sl3 += Convert.ToDouble(((Label)e.Item.FindControl("label6")).Text);
              je3 += Convert.ToDouble(((Label)e.Item.FindControl("label7")).Text);

              //sl4 += Convert.ToDouble(((Label)e.Item.FindControl("label8")).Text);
              je4 += Convert.ToDouble(((Label)e.Item.FindControl("label9")).Text);

             // sl5 += Convert.ToDouble(((Label)e.Item.FindControl("label10")).Text);
              je5 += Convert.ToDouble(((Label)e.Item.FindControl("label11")).Text);

              //sl6 += Convert.ToDouble(((Label)e.Item.FindControl("label12")).Text);
              je6 += Convert.ToDouble(((Label)e.Item.FindControl("label13")).Text);

             // sl7 += Convert.ToDouble(((Label)e.Item.FindControl("label14")).Text);
              je7 += Convert.ToDouble(((Label)e.Item.FindControl("label15")).Text);

              //sl8 += Convert.ToDouble(((Label)e.Item.FindControl("label16")).Text);
              je8 += Convert.ToDouble(((Label)e.Item.FindControl("label17")).Text);

             // sl9 += Convert.ToDouble(((Label)e.Item.FindControl("label18")).Text);
              je9 += Convert.ToDouble(((Label)e.Item.FindControl("label19")).Text);

              //sl10 += Convert.ToDouble(((Label)e.Item.FindControl("label20")).Text);
              je10 += Convert.ToDouble(((Label)e.Item.FindControl("label21")).Text);

              //sl11 += Convert.ToDouble(((Label)e.Item.FindControl("label22")).Text);
              je11 += Convert.ToDouble(((Label)e.Item.FindControl("label23")).Text);

              //sl12 += Convert.ToDouble(((Label)e.Item.FindControl("label24")).Text);
              je12 += Convert.ToDouble(((Label)e.Item.FindControl("label25")).Text);

              //sl13 += Convert.ToDouble(((Label)e.Item.FindControl("label26")).Text);
              je13 += Convert.ToDouble(((Label)e.Item.FindControl("label27")).Text);

              //sl14 += Convert.ToDouble(((Label)e.Item.FindControl("label28")).Text);
              je14 += Convert.ToDouble(((Label)e.Item.FindControl("label29")).Text);

             // sl15 += Convert.ToDouble(((Label)e.Item.FindControl("label30")).Text);
              je15 += Convert.ToDouble(((Label)e.Item.FindControl("label31")).Text);

             // sl16 += Convert.ToDouble(((Label)e.Item.FindControl("label32")).Text);
              je16 += Convert.ToDouble(((Label)e.Item.FindControl("label33")).Text);

             // sl17 += Convert.ToDouble(((Label)e.Item.FindControl("label34")).Text);
              je17 += Convert.ToDouble(((Label)e.Item.FindControl("label35")).Text);

              //sl18 += Convert.ToDouble(((Label)e.Item.FindControl("label36")).Text);
              je18 += Convert.ToDouble(((Label)e.Item.FindControl("label37")).Text);

              //sl19 += Convert.ToDouble(((Label)e.Item.FindControl("label38")).Text);
              je19 += Convert.ToDouble(((Label)e.Item.FindControl("label39")).Text);

              //slzj += Convert.ToDouble(((Label)e.Item.FindControl("label40")).Text);
              jezj += Convert.ToDouble(((Label)e.Item.FindControl("label41")).Text);                         

                                                     
            }
            
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("label52")).Text = sl1.ToString();
                ((Label)e.Item.FindControl("label53")).Text = je1.ToString();

                //((Label)e.Item.FindControl("label54")).Text = sl2.ToString();
                ((Label)e.Item.FindControl("label55")).Text = je2.ToString();

                //((Label)e.Item.FindControl("label56")).Text = sl3.ToString();
                ((Label)e.Item.FindControl("label57")).Text = je3.ToString();

                //((Label)e.Item.FindControl("label58")).Text = sl4.ToString();
                ((Label)e.Item.FindControl("label59")).Text = je4.ToString();

                //((Label)e.Item.FindControl("label60")).Text = sl5.ToString();
                ((Label)e.Item.FindControl("label61")).Text = je5.ToString();

                //((Label)e.Item.FindControl("label62")).Text = sl6.ToString();
                ((Label)e.Item.FindControl("label63")).Text = je6.ToString();

                //((Label)e.Item.FindControl("label64")).Text = sl7.ToString();
                ((Label)e.Item.FindControl("label65")).Text = je7.ToString();

                //((Label)e.Item.FindControl("label66")).Text = sl8.ToString();
                ((Label)e.Item.FindControl("label67")).Text = je8.ToString();

                //((Label)e.Item.FindControl("label68")).Text = sl9.ToString();
                ((Label)e.Item.FindControl("label69")).Text = je9.ToString();

                //((Label)e.Item.FindControl("label70")).Text = sl10.ToString();
                ((Label)e.Item.FindControl("label71")).Text = je10.ToString();

                //((Label)e.Item.FindControl("label72")).Text = sl11.ToString();
                ((Label)e.Item.FindControl("label73")).Text = je11.ToString();

                //((Label)e.Item.FindControl("label74")).Text = sl12.ToString();
                ((Label)e.Item.FindControl("label75")).Text = je12.ToString();

                //((Label)e.Item.FindControl("label76")).Text = sl13.ToString();
                ((Label)e.Item.FindControl("label77")).Text = je13.ToString();

                //((Label)e.Item.FindControl("label78")).Text = sl14.ToString();
                ((Label)e.Item.FindControl("label79")).Text = je14.ToString();

                //((Label)e.Item.FindControl("label80")).Text = sl15.ToString();
                ((Label)e.Item.FindControl("label81")).Text = je15.ToString();

                //((Label)e.Item.FindControl("label82")).Text = sl16.ToString();
                ((Label)e.Item.FindControl("label83")).Text = je16.ToString();

                //((Label)e.Item.FindControl("label84")).Text = sl17.ToString();
                ((Label)e.Item.FindControl("label85")).Text = je17.ToString();

                //((Label)e.Item.FindControl("label86")).Text = sl18.ToString();
                ((Label)e.Item.FindControl("label87")).Text = je18.ToString();

                //((Label)e.Item.FindControl("label88")).Text = sl19.ToString();
                ((Label)e.Item.FindControl("label89")).Text = je19.ToString();

                //((Label)e.Item.FindControl("label90")).Text = slzj.ToString();
                ((Label)e.Item.FindControl("label91")).Text = jezj.ToString();
               
            }
            
        }


        //public void rptProductNumStc
    }
}
