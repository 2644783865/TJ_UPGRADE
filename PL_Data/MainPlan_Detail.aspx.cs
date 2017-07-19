using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace ZCZJ_DPF.PL_Data
{
    public partial class MainPlan_Detail : System.Web.UI.Page
    {
        string taskId;
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            taskId = Request.QueryString["Id"];
            if (!IsPostBack)
            {
                InitInfo();
                InitGridview();
            }
        }
        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void InitInfo()
        {
            //MP_CODE, MP_PJID, MP_PJNAME, MP_ENGNAME, MP_NOTE, MP_STATE
            sqlText = "select MP_PJID,MP_PJNAME,MP_ENGNAME ";
            sqlText += "from dbo.TBMP_MAINPLANTOTAL where MP_CODE='" + taskId + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = taskId;
                lab_contract.Text = dr[0].ToString();
                lab_proname.Text = dr[1].ToString();
                lab_engname.Text = dr[2].ToString();

            }
            dr.Close();
        }
        private void InitGridview()
        {

            sqlText = "select * from TBMP_MAINPLANDETAIL where MP_ENGID='" + tsaid.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int state = Convert.ToInt32(((HtmlInputHidden)e.Row.FindControl("hidState")).Value);

                DateTime endTime = new DateTime();
                if (DateTime.TryParse(e.Row.Cells[6].Text, out endTime))
                {
                    TimeSpan span = endTime - DateTime.Now;
                    int warndays = Convert.ToInt32(e.Row.Cells[4].Text.Trim());


                    if (e.Row.Cells[3].Text == "0")
                    {
                        e.Row.Cells[7].Text = "无";
                        e.Row.BackColor = Color.Silver;
                    }
                    else
                    {
                        if (state == 2)
                        {
                            try
                            {
                                DateTime actural = Convert.ToDateTime(e.Row.Cells[8].Text);
                                TimeSpan spanActural = actural - endTime;
                                if (spanActural.TotalDays > 0)
                                {
                                    e.Row.BackColor = Color.OrangeRed;
                                }
                            }
                            catch (Exception)
                            {

                                e.Row.BackColor = Color.Gray;
                            }
                        }
                        else
                        {


                            if (state == 1 && span.TotalDays > warndays)
                            {

                                e.Row.BackColor = Color.LawnGreen;
                            }
                            if (span.TotalDays > -1 && span.TotalDays <= warndays)
                            {
                                e.Row.BackColor = Color.Yellow;
                            }
                            if (span.TotalDays <= -1)
                            {
                                e.Row.BackColor = Color.OrangeRed;
                            }

                        }
                    }
                
                }
                else
                {
                    e.Row.Cells[7].Text = "无";
                    e.Row.BackColor = Color.Silver;
                }
            }
        }


    }
}
