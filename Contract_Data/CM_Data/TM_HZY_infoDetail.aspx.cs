using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ZCZJ_DPF.CM_Data
{
    public partial class TM_HZY_infoDetail : System.Web.UI.Page
    {

        static string action = string.Empty;
        //static string PT_ID = string.Empty;
        static string PT_CODE = string.Empty;
        static string PT_PJID = string.Empty;
        static string PT_ENGID = string.Empty;
        string sql = "";
        string PT_ENGNAME = "";
        string PT_PJNAME = "";      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.QueryString["project"] != null && Request.QueryString["eng_ID"] != null)
                {
                    PT_PJID = Request.QueryString["project"].ToString();
                    PT_ENGID = Request.QueryString["eng_ID"].ToString();
                    sql = "select ENG_NAME,ENG_PJNAME from TBPM_ENGINFO  where  ENG_ID='" + PT_ENGID + "'and ENG_PJID='" + PT_PJID + "' ";
                  
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        PT_PJNAME = dr["ENG_PJNAME"].ToString();
                        PT_ENGNAME = dr["ENG_NAME"].ToString();
                    }
                    dr.Close();
                    tbproject.Text = PT_PJNAME;
                    tbeng.Text = PT_ENGNAME;
                }
                if (Request.QueryString["id"] == null && Request.QueryString["action"] == null)
                    throw new NoNullAllowedException("不允许空值传递！");
                else
                {                    
                    action = Request.QueryString["action"].ToString();
                    switch (action)
                    {
                        case "add":
                            {
                                ViewPanel.Visible = false;
                                break;
                            }
                        case "view":
                            {
                                UpdatePanel.Visible = false;
                                PT_CODE = Request.QueryString["id"].ToString();
                                //Response.Write("更新页面" + PT_CODE);
                                bindData();
                                break;
                            }
                        case "update":
                            {
                                ViewPanel.Visible = false;
                                PT_CODE = Request.QueryString["id"].ToString();
                                updateData();
                                break;
                            }
                    }
                }
            }  
        }

        protected void Submit_Click(object sender, EventArgs e)//提交任务单到生产任务单内容和生产任务单总表
        {
                 
            string PT_CODE = TextBox1.Text.Trim();//生产任务编号
            string PT_ENGCONTEXT = TextBox2.Text.Trim();//工程内容
            float  PT_QUANTITY = Convert.ToInt32(TextBox3.Text);//数量
            float  PT_ENGNUM = Convert.ToInt32(TextBox4.Text);// 工程量
            string PT_DLVRYDATE = DateTime.Now.ToShortDateString();//交货日期
            string PT_DRAWINGDATE = DateTime.Now.ToShortDateString();//图纸交付时间
            float  PT_COST = Convert.ToInt32(TextBox5.Text);//成本
            string PT_STATE = TextBox6.Text.Trim();//状态
            string PT_NOTE = TextBox7.Text.Trim();//备注
            string PT_PMCHARGERNM = TextBox8.Text.Trim();//项目管理部负责人
            string PT_BPCHARGERNM = TextBox9.Text.Trim();//经营策划部负责人
            string PT_PDCHARGERNM = TextBox10.Text.Trim();//生产单位负责人
            string PT_ICKCOMNM = TextBox11.Text.Trim();//收入核算单位
            string PT_PMEXECUTENM = TextBox12.Text.Trim();//项目管理部经办人
            string PT_BPEXECUTENM = TextBox13.Text.Trim();//经营策划部经办人
            string PT_PDEXECUTENM = TextBox14.Text.Trim();//生产单位经办人
            string PT_TNCHARGERNM = TextBox15.Text.Trim();//技术负责人
            if (action == "update")
            {
                string sqlUpdate1 = "update TBMP_PRDCTTASKDETAIL set PT_CODE='" + PT_CODE + "',PT_ENGCONTEXT='" + PT_ENGCONTEXT + "',PT_QUANTITY='" + PT_QUANTITY + "',PT_ENGNUM='" + PT_ENGNUM + "',PT_DLVRYDATE='" + PT_DLVRYDATE + "',PT_DRAWINGDATE='" + PT_DRAWINGDATE + "',PT_COST='" + PT_COST + "',PT_NOTE='" + PT_NOTE + "'where PT_CODE='" + PT_CODE + "'";
                string sqlUpdate2 = "update TBMP_PRDCTTASKTOTAL set PT_CODE='" + PT_CODE + "',PT_PMCHARGERNM='" + PT_PMCHARGERNM + "',PT_BPCHARGERNM='" + PT_BPCHARGERNM + "',PT_PDCHARGERNM='" + PT_PDCHARGERNM + "',PT_ICKCOMNM='" + PT_ICKCOMNM + "',PT_PMEXECUTENM='" + PT_PMEXECUTENM + "',PT_BPEXECUTENM='" + PT_BPEXECUTENM + "',PT_PDEXECUTENM='" + PT_PDEXECUTENM + "',PT_TNCHARGERNM ='" + PT_TNCHARGERNM + "'where PT_CODE='" + PT_CODE + "'";
                DBCallCommon.ExeSqlText(sqlUpdate1);
                DBCallCommon.ExeSqlText(sqlUpdate2);
                
            }
            else
            {
                //插入命令
               
                string sqlInsert1 = "insert into TBMP_PRDCTTASKDETAIL (PT_CODE,PT_ENGCONTEXT,PT_QUANTITY ,PT_ENGNUM,PT_DLVRYDATE,PT_DRAWINGDATE,PT_COST,PT_STATE,PT_NOTE ) values( '" + PT_CODE + "','" + PT_ENGCONTEXT + "','" + PT_QUANTITY + "','" + PT_ENGNUM + "','" + PT_DLVRYDATE + "','" + PT_DRAWINGDATE + "','" + PT_COST + "','" + PT_STATE + "','" + PT_NOTE + "')";
                string sqlInsert2 = "insert into TBMP_PRDCTTASKTOTAL (PT_PJID,PT_PJNAME,PT_ENGID,PT_ENGNAME,PT_CODE,PT_PMCHARGERNM,PT_BPCHARGERNM ,PT_PDCHARGERNM,PT_ICKCOMNM ,PT_PMEXECUTENM,PT_BPEXECUTENM,PT_PDEXECUTENM,PT_TNCHARGERNM ) values('" + PT_PJID + "','" + tbproject.Text + "','" + PT_ENGID + "','" + tbeng.Text + "','" + PT_CODE + "','" + PT_PMCHARGERNM + "','" + PT_BPCHARGERNM + "','" + PT_PDCHARGERNM + "','" + PT_ICKCOMNM + "','" + PT_PMEXECUTENM + "','" + PT_BPEXECUTENM + "','" + PT_PDEXECUTENM + "','" + PT_TNCHARGERNM + "')";
                List<string> sqlTexts = new List<string>();
                sqlTexts.Add(sqlInsert1);
                sqlTexts.Add(sqlInsert2);
                DBCallCommon.ExecuteTrans(sqlTexts);

                sql = "insert into TBPM_TCTSASSGN (TSA_TSNAME,TSA_PJID,TSA_PJNAME,TSA_ENGID,";
                sql += "TSA_ENGNAME,TSA_STATE)values ('" + PT_ENGCONTEXT + "','" + PT_PJID + "','" + tbproject.Text + "','" + PT_ENGID + "','" + tbeng.Text + "','0')";
              
                DBCallCommon.ExeSqlText(sql);
            }
            
            Response.Redirect("TM_HZY_info.aspx");    
        }

        private void bindData()  //绑定数据
        {
            string sqlselect1 = "select * from TBMP_PRDCTTASKDETAIL where PT_CODE='" + PT_CODE + "'";
            DataSet ds1 = DBCallCommon.FillDataSet(sqlselect1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Label1.Text = ds1.Tables[0].Rows[0]["PT_CODE"].ToString();//生产任务编号
                Label2.Text = ds1.Tables[0].Rows[0]["PT_ENGCONTEXT"].ToString();//工程内容
                Label3.Text = ds1.Tables[0].Rows[0]["PT_QUANTITY"].ToString();//数量
                Label4.Text = ds1.Tables[0].Rows[0]["PT_ENGNUM"].ToString();// 工程量
                Label5.Text = ds1.Tables[0].Rows[0]["PT_DLVRYDATE"].ToString();//交货日期
                Label6.Text = ds1.Tables[0].Rows[0]["PT_DRAWINGDATE"].ToString();//图纸交付时间
                Label7.Text = ds1.Tables[0].Rows[0]["PT_COST"].ToString();//成本
                Label8.Text = ds1.Tables[0].Rows[0]["PT_STATE"].ToString();//状态
                Label9.Text = ds1.Tables[0].Rows[0]["PT_NOTE"].ToString();//备注
            }
            string sqlselect2 = "select * from TBMP_PRDCTTASKTOTAL where PT_CODE='" + PT_CODE + "'";
            DataSet ds2 = DBCallCommon.FillDataSet(sqlselect2);
            if (ds2.Tables[0].Rows.Count > 0)
            {

                //生产任务编号
                tbproject.Text = ds2.Tables[0].Rows[0]["PT_PJNAME"].ToString();//项目
                tbeng.Text = ds2.Tables[0].Rows[0]["PT_ENGNAME"].ToString();   //工程
                Label10.Text = ds2.Tables[0].Rows[0]["PT_PMCHARGERNM"].ToString();//项目管理部负责人
                Label11.Text = ds2.Tables[0].Rows[0]["PT_BPCHARGERNM"].ToString();//经营策划部负责人
                Label12.Text = ds2.Tables[0].Rows[0]["PT_PDCHARGERNM"].ToString();//生产单位负责人
                Label13.Text = ds2.Tables[0].Rows[0]["PT_ICKCOMNM"].ToString();//收入核算单位
                Label14.Text = ds2.Tables[0].Rows[0]["PT_PMEXECUTENM"].ToString();//项目管理部经办人
                Label15.Text = ds2.Tables[0].Rows[0]["PT_BPEXECUTENM"].ToString();//经营策划部经办人
                Label16.Text = ds2.Tables[0].Rows[0]["PT_PDEXECUTENM"].ToString();//生产单位经办人 
                Label17.Text = ds2.Tables[0].Rows[0]["PT_TNCHARGERNM"].ToString(); //技术负责人


            }

        }

        private void updateData()  //更新绑定
        {

            string sqlselect1 = " select * from TBMP_PRDCTTASKDETAIL where PT_CODE='" + PT_CODE + "'";
            DataSet ds1 = DBCallCommon.FillDataSet(sqlselect1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                TextBox1.Text = ds1.Tables[0].Rows[0]["PT_CODE"].ToString();//生产任务编号
                TextBox2.Text = ds1.Tables[0].Rows[0]["PT_ENGCONTEXT"].ToString();//工程内容
                TextBox3.Text = ds1.Tables[0].Rows[0]["PT_QUANTITY"].ToString();//数量
                TextBox4.Text = ds1.Tables[0].Rows[0]["PT_ENGNUM"].ToString();// 工程量
                txt1.Value  = ds1.Tables[0].Rows[0]["PT_DLVRYDATE"].ToString();//交货日期
                txt2.Value  = ds1.Tables[0].Rows[0]["PT_DRAWINGDATE"].ToString();//图纸交付时间
                TextBox5.Text = ds1.Tables[0].Rows[0]["PT_COST"].ToString();//成本
                TextBox6.Text = ds1.Tables[0].Rows[0]["PT_STATE"].ToString();//状态
                TextBox7.Text = ds1.Tables[0].Rows[0]["PT_NOTE"].ToString();//备注 
            }

            string sqlselect2 = "select * from TBMP_PRDCTTASKTOTAL where PT_CODE='" + PT_CODE + "'";
            DataSet ds2 = DBCallCommon.FillDataSet(sqlselect2);
            if (ds2.Tables[0].Rows.Count > 0)
            {      
                
                //生产任务编号
                tbproject.Text = ds2.Tables[0].Rows[0]["PT_PJNAME"].ToString();//项目
                tbeng.Text = ds2.Tables[0].Rows[0]["PT_ENGNAME"].ToString();   //工程
                TextBox8.Text = ds2.Tables[0].Rows[0]["PT_PMCHARGERNM"].ToString();//项目管理部负责人
                TextBox9.Text = ds2.Tables[0].Rows[0]["PT_BPCHARGERNM"].ToString();//经营策划部负责人
                TextBox10.Text = ds2.Tables[0].Rows[0]["PT_PDCHARGERNM"].ToString();//生产单位负责人
                TextBox11.Text = ds2.Tables[0].Rows[0]["PT_ICKCOMNM"].ToString();//收入核算单位
                TextBox12.Text = ds2.Tables[0].Rows[0]["PT_PMEXECUTENM"].ToString();//项目管理部经办人
                TextBox13.Text = ds2.Tables[0].Rows[0]["PT_BPEXECUTENM"].ToString();//经营策划部经办人
                TextBox14.Text = ds2.Tables[0].Rows[0]["PT_PDEXECUTENM"].ToString();//生产单位经办人 
                TextBox15.Text = ds2.Tables[0].Rows[0]["PT_TNCHARGERNM"].ToString(); //技术负责人
            }
           
        }

        protected void Ruturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TM_HZY_info.aspx");
        }

       

       
    }
}
