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
    public partial class TM_MP_Update_View : System.Web.UI.Page
    {
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
            string action = Request.QueryString["action"];
            string[] fields = action.Split(' ');
            string engid = fields[0].ToString();
            string tablename = fields[1].ToString();
            //sqlText = "select temp1.MP_PID,temp1.MP_MARID,temp1.MP_NAME,temp1.MP_GUIGE,temp1.MP_CAIZHI,temp1.MP_UNIT,";
            //sqlText += "temp1.MP_STANDARD,temp1.MP_KEYCOMS,temp1.MP_WEIGHT-isnull(temp2.MP_WEIGHT,0) AS MP_WEIGHT,";
            //sqlText += "temp1.MP_NUMBER-isnull(temp2.MP_NUMBER,0) AS MP_NUMBER,temp1.MP_USAGE,";
            //sqlText += "temp1.MP_TYPE,temp1.MP_TIMERQ,temp1.MP_ENVREFFCT,temp1.MP_NOTE FROM ";
            //sqlText += "(select MP_PID,MP_MARID,MP_NAME,MP_GUIGE,MP_CAIZHI,MP_UNIT,MP_STANDARD,MP_KEYCOMS,";
            //sqlText += "MP_WEIGHT,MP_NUMBER,MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_NOTE ";
            //sqlText += "from " + tablename + " where MP_PID in (select MP_ID from TBPM_MPFORALLRVW ";
            //sqlText += "where MP_ENGID='" + engid + "' and cast(MP_STATE as int)>7 union ";
            //sqlText += "select MP_ID from TBPM_MPCHANGERVW where MP_ENGID='" + engid + "' ";
            //sqlText += "and cast(MP_STATE as int)>7)) as temp1 left join ";
            //sqlText += "(select MP_PID,MP_MARID,MP_NAME,MP_GUIGE,MP_CAIZHI,MP_UNIT,MP_STANDARD,MP_KEYCOMS,";
            //sqlText += "sum(MP_WEIGHT) as MP_WEIGHT,sum(MP_NUMBER) as MP_NUMBER from TBPM_MPCHANGE ";
            //sqlText += "where MP_PID in (select MP_ID from TBPM_MPCHANGERVW where MP_ENGID='" + engid + "' ";
            //sqlText += "and cast(MP_STATE as int)>7)group by MP_PID,MP_MARID,MP_NAME,MP_GUIGE,MP_CAIZHI,";
            //sqlText += "MP_UNIT,MP_STANDARD,MP_KEYCOMS) as temp2 on temp1.MP_MARID=temp2.MP_MARID ";
            //sqlText += "and temp1.MP_GUIGE=temp2.MP_GUIGE and temp1.MP_KEYCOMS=temp2.MP_KEYCOMS ";
            //sqlText += "and temp1.MP_PID=temp2.MP_PID order by temp1.MP_PID";
            sqlText = "select MP_PID,MP_PJID,MP_PJNAME,MP_ENGID,MP_ENGNAME,"+
                      "MP_MARID,MP_NAME,MP_GUIGE,MP_CAIZHI,MP_STANDARD,MP_UNIT,MP_LENGTH,MP_WIDTH,MP_WEIGHT,"+
                      "MP_NUMBER,MP_USAGE,MP_TYPE,MP_TIMERQ,MP_ENVREFFCT,MP_TRACKNUM,MP_KEYCOMS,MP_NOTE,"+
                      "MP_STATE,MP_FIXEDSIZE,MP_STATUS,MP_ZONGXU,MP_CHGPID,MP_OLDXUHAO,MP_NEWXUHAO from View_TM_MPHZY where MP_STATE='0'";
            DBCallCommon.BindGridView(GridView1,sqlText);
            InitPanel();
        }

        private double sum = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sum += Convert.ToDouble(e.Row.Cells[7].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "合计：";
                e.Row.Cells[6].Text = "kg";
                e.Row.Cells[7].Text = sum.ToString("0.00");
            }
        }
    }
}
