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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MarReplace_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
        }

        protected void InitPage()
        {
            if (Request.QueryString["tracknum"] != null)
            {
                string tracknum = Request.QueryString["tracknum"].ToString().Split('&')[0];

                string sqltext = "SELECT [MP_NEWXUHAO] AS XUHAO,[MP_ZONGXU] AS ZONGXU,[MP_TUHAO] AS TUHAO,[MP_MARID] AS MARID,[MP_NAME] AS MARNAME,[MP_GUIGE] AS GUIGE,[MP_CAIZHI] AS CAIZHI,[MP_LENGTH] AS LENGTH,[MP_WIDTH] AS WIDTH,[MP_UNIT] AS UNIT,[MP_WEIGHT] AS WEIGHT,[MP_NUMBER] AS NUMBER,[MP_STANDARD] AS STANDARD,[MP_FIXEDSIZE] AS FIXEDSIZE,[MP_KEYCOMS] AS KEYCOMS,[MP_TIMERQ] AS TIMERQ,[MP_TYPE] AS MARTYPE,[MP_ENVREFFCT] AS ENVREFFCT,[MP_USAGE] AS USAGE,[MP_NOTE] AS NOTE,[MP_TRACKNUM] AS TRACKNUM FROM [View_TM_MPHZY] WHERE [MP_TRACKNUM]='"+tracknum+"'"+
                                 " UNION ALL "+
                                 "SELECT [OSL_NEWXUHAO] AS XUHAO,[OSL_ZONGXU] AS ZONGXU,[OSL_BIAOSHINO] AS TUHAO,[OSL_MARID] AS MARID,[OSL_NAME] AS MARNAME,[OSL_GUIGE] AS GUIGE,[OSL_CAIZHI] AS CAIZHI,[OSL_LENGTH] AS LENGTH,[OSL_WIDTH] AS WIDTH,[OSL_UNIT] AS UNIT,[OSL_TOTALWGHTL] AS WEIGHT,[OSL_NUMBER] AS NUMBER,[OSL_STANDARD] AS STANDARD,[OSL_FIXEDSIZE] AS FIXEDSIZE,[OSL_KEYCOMS] AS KEYCOMS,[OSL_REQDATE] AS TIMERQ,[OST_OUTTYPE] AS MARTYPE,'' AS ENVREFFCT,'' AS USAGE,[OSL_REQUEST]+' '+ISNULL([OSL_KU],'') AS NOTE,[OSL_TRACKNUM] AS TRACKNUM FROM [View_TM_OUTSOURCELIST]WHERE [OSL_TRACKNUM]='"+tracknum+"'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    Panel1.Visible = false;
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    Panel1.Visible = false;
                }

            }
        }
        private double sum_cd;
        private double sum_wgt;
        private double sum_num;

        protected void GridView1_OnDataBound(object sender,GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sum_cd += Convert.ToDouble(e.Row.Cells[8].Text == "" ? "0" : e.Row.Cells[8].Text);
                sum_wgt += Convert.ToDouble(e.Row.Cells[11].Text == "" ? "0" : e.Row.Cells[11].Text);
                sum_num += Convert.ToDouble(e.Row.Cells[12].Text == "" ? "0" : e.Row.Cells[12].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text = "合计：";
                e.Row.Cells[8].Text = sum_cd.ToString();
                e.Row.Cells[11].Text = sum_wgt.ToString();
                e.Row.Cells[12].Text = sum_num.ToString();
            }
        }
    }
}
