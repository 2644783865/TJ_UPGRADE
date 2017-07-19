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
using System.Text;
using AjaxControlToolkit;


namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MP_Old_View : System.Web.UI.Page
    {
        protected static string mp_no;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }

        private void InitInfo()
        {
            //变更后材料计划
            string action = Request.QueryString["action"];
            lbllotNum.Text = action;
            mp_no = action;
            string sqltxt = "select * from View_TM_MPCHANGE where MP_PID='" + action + "' order by dbo.f_FormatSTR(MP_NEWXUHAO,'.') ";
            DBCallCommon.BindGridView(GridView1,sqltxt);
            
            //本批引起计划增加或减少量
            GridView2.DataSource = this.GetCurLotIncRedQutyOfMar(action);
            GridView2.DataBind();
            if (GridView2.Rows.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }


        private  DataTable GetCurLotIncRedQutyOfMar(string bglotnum)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "PRO_TM_GetIncRedQtyOfMar");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Change_LotNum", bglotnum, SqlDbType.VarChar, 3000);
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }


        #region ShowModual
        [System.Web.Services.WebMethodAttribute(),
           System.Web.Script.Services.ScriptMethodAttribute()]
        public static string GetMpDynamicContent(string contextKey)
        {
            StringBuilder sTemp = new StringBuilder();
            string sql_getoutbefore = "select MP_NEWXUHAO,MP_TUHAO,MP_MARID,MP_NAME,MP_GUIGE,MP_UNIT,MP_WEIGHT,MP_NUMBER,MP_CAIZHI,MP_STANDARD,MP_KEYCOMS,MP_FIXEDSIZE from View_TM_MPHZY where MP_NEWXUHAO='" + contextKey + "' AND MP_CHGPID='" + mp_no + "'";
            DataTable dt_chgbef = DBCallCommon.GetDTUsingSqlText(sql_getoutbefore);

            sTemp.Append("<table style='background-color:#f3f3f3; border: #B9D3EE 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");
            sTemp.Append("<tr><td colspan='16' style='background-color:#B9D3EE; color:white;' align='center'><b>变更前信息:</b></td></tr>");


            if (dt_chgbef.Rows.Count > 0)
            {

                sTemp.Append("<tr><td><b>序号</b></td>");
                sTemp.Append("<td align='center'><b>图号</b></td>");
                sTemp.Append("<td align='center'><b>材料ID</b></td>");
                sTemp.Append("<td align='center'><b>材料名称</b></td>");
                sTemp.Append("<td align='center'><b>规格</b></td>");
                sTemp.Append("<td align='center'><b>单位(kg)</b></td>");
                sTemp.Append("<td align='center'><b>重量</b></td>");
                sTemp.Append("<td align='center'><b>数量</b></td>");
                sTemp.Append("<td align='center'><b>材质</b></td>");
                sTemp.Append("<td align='center'><b>标准</b></td>");
                sTemp.Append("<td align='center'><b>关键部件</b></td>");
                sTemp.Append("<td align='center'><b>是否定尺</b></td>");

                for (int i = 0; i < dt_chgbef.Rows.Count; i++)
                {
                    sTemp.Append("<tr style='border-width:1px'>");

                    for (int j = 0; j < dt_chgbef.Columns.Count; j++)
                    {
                        sTemp.Append("<td  style='white-space:pre;' align='center'>" + dt_chgbef.Rows[i][j].ToString() + "</td>");

                    }
                    sTemp.Append("</tr>");
                }
            }
            else
            {
                sTemp.Append("<tr><td colspan='16'><i>没有记录...</i></td></tr>");
            }
            sTemp.Append("</table>");

            return sTemp.ToString();
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (mp_no.Contains(".JSB MPBG/") || mp_no.Contains(".JSB MPQX/"))
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Programmatically reference the PopupControlExtender
                    PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;
                    // Set the BehaviorID
                    string behaviorID = string.Concat("pce", e.Row.RowIndex);
                    pce.BehaviorID = behaviorID;

                    // Programmatically reference the Image control
                    HyperLink hpl = (HyperLink)e.Row.FindControl("hplBeforeChg");

                    // Add the clie nt-side attributes (onmouseover & onmouseout)
                    string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
                    string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

                    hpl.Attributes.Add("onmouseover", OnMouseOverScript);
                    hpl.Attributes.Add("onmouseout", OnMouseOutScript);
                }
            }
        }
        #endregion


    }
}
