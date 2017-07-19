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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Packing_Generate : System.Web.UI.Page
    {
        string sqlText;
        string tablename;
        string tuhao;
        string zongxu;
        string name;
        string guige;
        string caizhi;
        string num;
        string uweight;
        string totalweight;
        string process;
        string casenum;
        string remark;
        string[] fields;
        string pk_no;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        //初始化表名
        private void InitList()
        {
            #region
            switch (ms_list.Value)
            {
                case "回转窑":
                    tablename = "TBPM_MSOFHZY";
                    break;
                case "球、立磨":
                    tablename = "TBPM_MSOFQLM";
                    break;
                case "篦冷机":
                    tablename = "TBPM_MSOFBLJ";
                    break;
                case "堆取料机":
                    tablename = "TBPM_MSOFDQJ";
                    break;
                case "钢结构及非标":
                    tablename = "TBPM_MSOFGFB";
                    break;
                case "电气及其他":
                    tablename = "TBPM_MSOFDQO";
                    break;
            }
            #endregion
        }
        //初始化页面信息
        private void InitInfo()
        {
            string action = "";
            if (Request.QueryString["packlist"] != null)
            {
                fields = Request.QueryString["packlist"].ToString().Split('.');
                tsa_id.Text = fields[0].ToString();
                ms_no.Value = fields[0].ToString() + '.' + fields[1].ToString();
                action = fields[2].ToString() + '.' + fields[3].ToString() + '.' + fields[4].ToString();
                status.Value = fields[4].ToString();
            }
            else
            {
                action = Request.QueryString["pkdetail"];
                fields = action.Split('.');
                tsa_id.Text = fields[0].ToString();
                ms_no.Value = fields[0].ToString() + '.' + fields[1].ToString();
                status.Value = fields[2].ToString();
            }
            if (int.Parse(status.Value) > 0)
            {
                Response.Redirect("TM_Packing_List.aspx?id=" + action);
            }
            else
            {
                sqlText = "select TSA_PJID,TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE ";
                sqlText += "from TBPM_TCTSASSGN where TSA_ID='" + tsa_id.Text + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr.Read())
                {
                    pro_id.Value = dr[0].ToString();
                    lab_proname.Text = dr[1].ToString();
                    lab_engname.Text = dr[2].ToString();
                    ms_list.Value = dr[3].ToString();
                }
                dr.Close();
                InitList();
                //GetNewMSDate();
                //DeleteMSOldData();
                sqlText = "select * from " + tablename + " where MS_PID='" + ms_no.Value + "'";
                DBCallCommon.BindGridView(GridView1, sqlText);
            }
        }
        //更新变更物料
        private void GetNewMSDate()
        {
            sqlText= "select MS_TUHAO,MS_ZONGXU,MS_NAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,";
            sqlText+="MS_UWGHT,MS_TLWGHT,MS_PROCESS,MS_TIMERQ,MS_NOTE ";
            sqlText+="from TBPM_MSCHANGE where MS_PID='" + ms_no.Value + "' and MS_ZONGXU=";
            sqlText+="(select MS_ZONGXU from TBPM_MSCHANGE where MS_PID='" + ms_no.Value + "' ";
            sqlText+="group by MS_ZONGXU having count (MS_ZONGXU) > 1) and MS_STATE!='0'";
            DataTable dt=DBCallCommon.GetDTUsingSqlText(sqlText);
            for(int i=0;i<dt.Rows.Count;i++)
            {
                tuhao = dt.Rows[i][0].ToString();
                zongxu = dt.Rows[i][1].ToString();
                name = dt.Rows[i][2].ToString();
                guige = dt.Rows[i][3].ToString();
                caizhi = dt.Rows[i][4].ToString();
                num = dt.Rows[i][5].ToString();
                uweight = dt.Rows[i][6].ToString();
                totalweight = dt.Rows[i][7].ToString();
                process = dt.Rows[i][8].ToString();
                casenum = dt.Rows[i][9].ToString();
                remark = dt.Rows[i][10].ToString();
                sqlText = "insert into " + tablename + " ";
                sqlText += "(MS_PID,MS_TUHAO,MS_ZONGXU,MS_PJID,MS_PJNAME,MS_ENGID,MS_ENGNAME,";
                sqlText += "MS_NAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,MS_TLWGHT,MS_PROCESS,";
                sqlText += "MS_TIMERQ,MS_NOTE,MS_STATE) values ('" + ms_no.Value + "','" + tuhao + "',";
                sqlText += "'" + zongxu + "','" + pro_id.Value + "','" + lab_proname.Text + "','" + tsa_id.Text + "',";
                sqlText += "'" + lab_engname.Text + "','" + name + "','" + guige + "','" + caizhi + "','" + num + "','" + uweight + "',";
                sqlText += "'" + totalweight + "','" + process + "','" + casenum + "','" + remark + "','5')";
                DBCallCommon.ExeSqlText(sqlText);
            }
            
        }

        //删除制作明细的旧记录
        private void DeleteMSOldData()
        {
            sqlText = "delete from "+tablename+" ";
            sqlText += "where MS_PID='" + ms_no.Value + "' and MS_ZONGXU in (select MS_ZONGXU from " + tablename + " ";
            sqlText += "group by MS_ZONGXU having count(MS_ZONGXU)>1) and MS_ID not in ";
            sqlText += "(select MAX(MS_ID) from " + tablename + " group by MS_ZONGXU having count(MS_ZONGXU)>1)";
            DBCallCommon.ExeSqlText(sqlText);
        }

        //删除装箱单的记录
        private void DeletePKOldData()
        {
            sqlText = "delete from TBPM_PACKLISTTOTAL where PLT_PACKLISTNO='" + pk_no + "'";
            DBCallCommon.ExeSqlText(sqlText);
            sqlText = "delete from TBPM_PACKINGLIST where PL_PACKLISTNO='" + pk_no + "'";
            DBCallCommon.ExeSqlText(sqlText);
        }

        protected void packlist_Click(object sender, EventArgs e)
        {
            int count = 0;
            pk_no = tsa_id.Text + "." + "JSB" + "/" + Session["UserNameCode"] + "/" + "PK";
            DeletePKOldData();
            InitList();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                string id = ((Label)gr.FindControl("ID")).Text;
                tuhao = gr.Cells[2].Text;
                name = gr.Cells[4].Text;
                num = gr.Cells[7].Text;
                uweight = gr.Cells[8].Text;
                casenum = ((HtmlInputText)gr.FindControl("xianghao")).Value;
                if (casenum != "")
                {
                    sqlText = "update " + tablename + " set MS_TIMERQ='"+casenum+"' ";
                    sqlText += "where MS_ID='" + id + "'";
                    DBCallCommon.ExeSqlText(sqlText);
                    sqlText = "insert into TBPM_PACKINGLIST ";
                    sqlText += "(PL_PACKLISTNO,PL_PACKAGENO,PL_PACKNAME,PL_MARKINGNO,";
                    sqlText += "PL_PACKQLTY,PL_SINGLENETWGHT)";
                    sqlText += " values ('" + pk_no + "','" + casenum + "','" + name + "','" + tuhao + "',";
                    sqlText +="'" + num + "','" + uweight + "')";
                    DBCallCommon.ExeSqlText(sqlText);
                    count++;
                }
            }
            if (count != 0)
            {
                sqlText = "insert into TBPM_PACKLISTTOTAL (PLT_PACKLISTNO,PLT_PJID,PLT_PJNAME,PLT_ENGID,";
                sqlText += "PLT_ENGNAME,PLT_SUBMITID,PLT_SUBMITNM)";
                sqlText += " values ('" + pk_no + "','" + pro_id.Value + "','" + lab_proname.Text + "','" + tsa_id.Text + "',";
                sqlText += "'" + lab_engname.Text + "','" + Session["UserID"] + "','" + Session["UserName"] + "')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Redirect("TM_Packing_List.aspx?id=" + pk_no + '.' + status.Value);
            }
            else
            {
                Response.Write("<script>alert('提示:装箱单已提交!')</script>");
                return;
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
