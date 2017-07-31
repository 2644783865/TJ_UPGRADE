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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Gongshi_detail_edit : BasicPage
    {
        string action, Id;


        protected void Page_Load(object sender, EventArgs e)
        {
            //CheckUser(ControlFinder);

            action = Request.QueryString["action"];
            Id = Request.QueryString["Id"];

            if (!IsPostBack)
            {
                if (action.Equals("edit"))
                {
                    StringBuilder sqlText = new StringBuilder();
                    sqlText.Append("select * from ");
                    sqlText.Append("TBMP_GS_LIST ");
                    sqlText.Append("where Id='" + Id + "'");


                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText.ToString());
                    if (dr.Read())
                    {
                        lbCUSNAME.Text = dr["GS_CUSNAME"].ToString();
                        lbCONTR.Text = dr["GS_CONTR"].ToString();
                        lbTSAID.Text = dr["GS_TSAID"].ToString();
                        lbYEAR.Text = dr["DATEYEAR"].ToString();
                        lbMONTH.Text = dr["DATEMONTH"].ToString();
                        txtTUHAO.Text = dr["GS_TUHAO"].ToString();
                        txtTUMING.Text = dr["GS_TUMING"].ToString();
                        txtEQUID.Text = dr["GS_EQUID"].ToString();
                        txtEQUNAME.Text = dr["GS_EQUNAME"].ToString();
                        txtEQUFACTOR.Text = dr["GS_EQUFACTOR"].ToString();
                        txtEQUHOUR.Text = dr["GS_HOURS"].ToString();
                        txtEQUMONEY.Text = dr["GS_MONEY"].ToString();
                        txtNOTE.Text = dr["GS_NOTE"].ToString();
                    }
                    dr.Close();
                }
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (action.Equals("edit"))
            {
                StringBuilder sqlText = new StringBuilder();
                sqlText.Append("update TBMP_GS_LIST set ");
                sqlText.Append("GS_TUHAO='" + txtTUHAO.Text.Trim() + "',");
                sqlText.Append("GS_TUMING='" + txtTUMING.Text.Trim() + "',");
                sqlText.Append("GS_EQUID='" + txtEQUID.Text.Trim() + "',");
                sqlText.Append("GS_EQUNAME='" + txtEQUNAME.Text.Trim() + "',");
                sqlText.Append("GS_EQUFACTOR='" + txtEQUFACTOR.Text.Trim() + "',");
                sqlText.Append("GS_HOURS='" + txtEQUHOUR.Text.Trim() + "',");
                sqlText.Append("GS_MONEY='" + txtEQUMONEY.Text.Trim() + "',");
                sqlText.Append("GS_NOTE='" + txtNOTE.Text.Trim() + "'");
                sqlText.Append(" where Id='" + Id + "'");
                DBCallCommon.ExeSqlText(sqlText.ToString());
                //UPDATE TBMP_GS_COL_LIST SET TBMP_GS_COL_LIST.GS_TSAMONEY= (select SUM(GS_MONEY) FROM TBMP_GS_LIST AS A WHERE A.GS_TSAID='15249BJ1-1') WHERE TBMP_GS_COL_LIST.GS_TSAID='15249BJ1-1';

                sqlText.Remove(0, sqlText.Length);
                sqlText.Append("UPDATE TBMP_GS_COL_LIST SET TBMP_GS_COL_LIST.GS_TSAMONEY= (select SUM(GS_MONEY) FROM TBMP_GS_LIST WHERE ");
                sqlText.Append("TBMP_GS_LIST.GS_CUSNAME='"+lbCUSNAME.Text.Trim()+"'");
                sqlText.Append("and TBMP_GS_LIST.GS_CONTR='"+lbCONTR.Text.Trim()+"'");
                sqlText.Append("and TBMP_GS_LIST.GS_TSAID='" + lbTSAID.Text.Trim() + "'");
                sqlText.Append("and TBMP_GS_LIST.DATEYEAR='" + lbYEAR.Text.Trim() + "'");
                sqlText.Append("and TBMP_GS_LIST.DATEMONTH='" + lbMONTH.Text.Trim() + "'");
                sqlText.Append(") WHERE ");
                sqlText.Append("TBMP_GS_COL_LIST.GS_CUSNAME='"+lbCUSNAME.Text.Trim()+"'");
                sqlText.Append("AND TBMP_GS_COL_LIST.GS_CONTR='"+lbCONTR.Text.Trim()+"'");
                sqlText.Append("AND TBMP_GS_COL_LIST.GS_TSAID='" + lbTSAID.Text.Trim()+ "'");
                sqlText.Append("and TBMP_GS_COL_LIST.DATEYEAR='" + lbYEAR.Text.Trim() + "'");
                sqlText.Append("and TBMP_GS_COL_LIST.DATEMONTH='" + lbMONTH.Text.Trim() + "'");
                DBCallCommon.ExeSqlText(sqlText.ToString());

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "if(confirm('操作成功，是否返回明细界面？')){window.close();window.opener.location.reload();}", true);
            }
        }
    }
}
