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

namespace ZCZJ_DPF.OM_Data
{
    public partial class Sta_operate : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                Showdata(); 
            }
            else
            {
                Response.Write("<script>alert('非法登录！')</script>");
                Response.End();

            }
        }

        protected void Showdata()
        {
            string st_id = Request.QueryString["ST_ID"].ToString();//得到修改人员编码
            string sqlText = "select distinct a.*,b.DEP_NAME,d.DEP_NAME as DEP_POSITION,e.ST_NAME as MANCLERK from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on a.ST_POSITION = d.DEP_CODE left join TBDS_STAFFINFO as e on a.ST_MANCLERK=e.ST_ID where a.ST_ID='" + st_id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DataRow dr = dt.Rows[0];
            foreach (Control lable in Panel1.Controls)
            {
                if (lable is Label)
                {
                    ((Label)lable).Text = dr[((Label)lable).ID.ToString()].ToString();
                }
            }
            sqlText = "select * from TBDS_WORKHIS where ST_ID='" + st_id + "'";
            Det_Repeater.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater.DataBind();
            sqlText = "select * from TBDS_EDUCA where ST_ID='" + st_id + "'";
            Det_Repeater1.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater1.DataBind();
            sqlText = "select * from TBDS_RELATION where ST_ID='" + st_id + "'";
            Det_Repeater2.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater2.DataBind();
            InitVar();
            InitVar1();
            InitVar2();
        }

        private void InitVar()
        {
            if (Det_Repeater.Items.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }

        private void InitVar1()
        {
            if (Det_Repeater1.Items.Count == 0)
            {
                NoDataPanel1.Visible = true;
            }
            else
            {
                NoDataPanel1.Visible = false;
            }
        }

        private void InitVar2()
        {
            if (Det_Repeater2.Items.Count == 0)
            {
                NoDataPanel2.Visible = true;
            }
            else
            {
                NoDataPanel2.Visible = false;
            }
        }
    }
}
