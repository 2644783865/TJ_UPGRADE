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
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_Update_View : System.Web.UI.Page
    {
        string sqlText;
        string action;
        string[] fields;
        string engid;
        string tablename;
        string pk_no;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            fields = action.Split(' ');
            engid = fields[0].ToString();
            tablename = fields[1].ToString();
            if (!IsPostBack)
            {
                InitInfo();
            }
        }

        //初始化参数
        private void InitPanel()
        {
            if (GridView1.Rows.Count == 0)
            {
                Panel2.Visible = true;
            }
            else
            {
                Panel2.Visible = false;
            }
        }

        //初始化页面
        private void InitInfo()
        {
            tsa_id.Text = engid;
            sqlText = "select TSA_PJID,TSA_PJNAME,TSA_ENGNAME from TBPM_TCTSASSGN ";
            sqlText += "where TSA_ID='" + engid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                pro_id.Value = dr[0].ToString();
                proname.Text = dr[1].ToString();
                engname.Text = dr[2].ToString();
            }
            dr.Close();
            #region
            //sqlText = "select MS_PID,MS_INDEX,MS_TUHAO,MS_ZONGXU,MS_NAME,MS_GUIGE,MS_CAIZHI,";
            //sqlText += "MS_UNUM,MS_UWGHT,MS_TLWGHT,MS_MASHAPE,MS_MASTATE,MS_STANDARD,";
            //sqlText += "MS_PROCESS,MS_TIMERQ,MS_NOTE FROM " + tablename + " ";
            //sqlText += "where MS_ENGID='" + engid + "' and MS_STATUS='0' union ";
            //sqlText += "select MS_PID,MS_INDEX,MS_TUHAO,MS_ZONGXU,MS_NAME,MS_GUIGE,MS_CAIZHI,";
            //sqlText += "MS_UNUM,MS_UWGHT,MS_TLWGHT,MS_MASHAPE,MS_MASTATE,MS_STANDARD,";
            //sqlText += "MS_PROCESS,MS_TIMERQ,MS_NOTE FROM TBPM_MSCHANGE ";
            //sqlText += "where MS_ENGID='" + engid + "' order by MS_ZONGXU ";
            #endregion
            sqlText = "select MS_PID,MS_INDEX,MS_TUHAO,MS_ZONGXU,MS_NAME,MS_GUIGE,MS_CAIZHI,";
            sqlText += "MS_UNUM,MS_UWGHT,MS_TLWGHT,MS_MASHAPE,MS_MASTATE,MS_STANDARD,";
            sqlText += "MS_PROCESS,MS_TIMERQ,MS_NOTE FROM TBPM_MSOFNEW ";
            sqlText += "where MS_ENGID='" + engid + "' and MS_STATE='5' ";
            sqlText += "order by dbo.f_formatstr(MS_INDEX,'.') ";
            DBCallCommon.BindGridView(GridView1, sqlText);
            InitPanel();
        }

        //导出明细
        protected void exportDetail_Click(object sender, EventArgs e)
        {
            //页面导出excel
            #region
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            //Response.ContentType = "application/excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //GridView1.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();
            #endregion
            //SQL导出excel
            sqlText = "select MS_PID,MS_INDEX,MS_TUHAO,MS_ZONGXU,MS_NAME,MS_GUIGE,MS_CAIZHI,";
            sqlText += "MS_UNUM,MS_UWGHT,MS_TLWGHT,MS_MASHAPE,MS_MASTATE,MS_STANDARD,";
            sqlText += "MS_PROCESS,MS_TIMERQ,MS_NOTE FROM TBPM_MSOFNEW ";
            sqlText += "where MS_ENGID='" + engid + "' and MS_STATE='5' ";
            sqlText += "order by dbo.f_formatstr(MS_INDEX,'.') ";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connectionStrings"]);
            SqlDataAdapter da = new SqlDataAdapter(sqlText,conn);
            DataSet ds = new DataSet();
            da.Fill(ds,"table1");
            System.Data.DataTable dt = ds.Tables["table1"];

            StringWriter sw = new StringWriter();
            sw.WriteLine("批号\t序号\t图号\t总序\t名称\t规格\t材质\t数量\t单重(kg)\t总重(kg)\t毛坯\t状态\t国标\t工艺流程\t箱号\t备注");

            foreach (DataRow dr in dt.Rows) 
            {
                sw.WriteLine(dr["MS_PID"] + "\t" + dr["MS_INDEX"] + "\t" + dr["MS_TUHAO"] + "\t" + dr["MS_ZONGXU"] + "\t" + dr["MS_NAME"] + "\t" + dr["MS_GUIGE"] + "\t" + dr["MS_TUHAO"] + "\t" + dr["MS_CAIZHI"] + "\t" + dr["MS_UNUM"] + "\t" + dr["MS_UWGHT"] + "\t" + dr["MS_TLWGHT"] + "\t" + dr["MS_MASHAPE"] + "\t" + dr["MS_MASTATE"] + "\t" + dr["MS_STANDARD"] + "\t" + dr["MS_PROCESS"] + "\t" + dr["MS_TIMERQ"] + "\t" + dr["MS_NOTE"]); 
            }
            sw.Close();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMddHHmmssfff") +".xls"+ ""); 
            Response.ContentType = "application/ms-excel"; 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); 
            Response.Write(sw);
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        //删除装箱单的记录
        private void DeletePKOldData()
        {
            sqlText = "delete from TBPM_PACKLISTTOTAL where PLT_PACKLISTNO='" + pk_no + "'";
            DBCallCommon.ExeSqlText(sqlText);
            sqlText = "delete from TBPM_PACKINGLIST where PL_PACKLISTNO='" + pk_no + "'";
            DBCallCommon.ExeSqlText(sqlText);
        }

        //生成装箱单
        protected void packlist_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (pkid.Value == "1")
            {
                pk_no = engid + "." + "JSB" + "/" + Session["UserNameCode"] + "/" + "PKBG";
                DeletePKOldData();
            }
            else
            {
                pk_no = engid + "." + "JSB" + "/" + Session["UserNameCode"] + "/" + "PK";
                DeletePKOldData();
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                string tuhao = gr.Cells[3].Text;
                string name = gr.Cells[5].Text;
                string num = gr.Cells[8].Text;
                string uweight = gr.Cells[9].Text;
                string casenum = gr.Cells[15].Text;
                if (casenum != "&nbsp;")
                {
                    sqlText = "insert into TBPM_PACKINGLIST (PL_PACKLISTNO,PL_PACKAGENO,PL_PACKNAME,";
                    sqlText += "PL_MARKINGNO,PL_PACKQLTY,PL_SINGLENETWGHT)";
                    sqlText += " values ('" + pk_no + "','" + casenum + "','" + name + "','" + tuhao + "',";
                    sqlText += "'" + num + "','" + uweight + "')";
                    DBCallCommon.ExeSqlText(sqlText);
                    count++;
                }
            }
            if (count != 0)
            {
                sqlText = "insert into TBPM_PACKLISTTOTAL (PLT_PACKLISTNO,PLT_PJID,PLT_PJNAME,";
                sqlText += "PLT_ENGID,PLT_ENGNAME,PLT_SUBMITID,PLT_SUBMITNM)";
                sqlText += " values ('" + pk_no + "','" + pro_id.Value + "','" + proname.Text + "','" + engid + "',";
                sqlText += "'" + engname.Text + "','" + Session["UserID"] + "','" + Session["UserName"] + "')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Redirect("TM_Packing_List.aspx?action=" + pk_no );
            }
        }
    }
}
