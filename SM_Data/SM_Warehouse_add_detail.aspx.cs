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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_add_detail : System.Web.UI.Page
    {
        string Flag = "";
        string marid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Flag = Request.QueryString["flag"];
            marid = Request.QueryString["mar_id"];
            if (!IsPostBack)
            {
                if (Flag == "modify")
                {

                    string strsql = "select * from View_STORAGE_ADD_DELETE where MARID='" + marid + "'";
                    SqlDataReader dr0 = DBCallCommon.GetDRUsingSqlText(strsql);
                    if (dr0.Read())
                    {
                        TextMarid.Text = dr0["MARID"].ToString();
                        TextMarid.Enabled = false;
                        TBmname.Text = dr0["MNAME"].ToString();
                        TBmname.Enabled = false;
                        TBcaizhi.Text = dr0["CAIZHI"].ToString();
                        TBcaizhi.Enabled = false;
                        TBgb.Text = dr0["GB"].ToString();
                        TBgb.Enabled = false;
                        TBguige.Text = dr0["GUIGE"].ToString();
                        TBguige.Enabled = false;
                        txtUnit.Text = dr0["PURCUNIT"].ToString();
                        txtUnit.Enabled = false;
                        TextBoxNumber.Text = dr0["WARNNUM"].ToString();

                        txtReasonableNum.Text = dr0["REASONABLENUM"].ToString();
                        TextBoxNote.Text = dr0["NOTES"].ToString();
                        ddlType.SelectedValue = dr0["Type"].ToString();
                        ddlsfjsbz.SelectedValue = dr0["BZJSBZ"].ToString().Trim();
                    }
                    dr0.Close();
                }
            }

        }
        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Flag == "add")
            {
                string sql = "";
                string marid = TextMarid.Text.ToString();
                sql = "SELECT * FROM TBWS_STORAGE_WARN WHERE MARID='" + marid + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    dr.Close();
                    LabelMessage.Text = "数据库中已经存在该物料号！";
                    return;

                }
                else
                {
                    string warnum = TextBoxNumber.Text.ToString();
                    string reasonnum = txtReasonableNum.Text.ToString();
                    string note = TextBoxNote.Text;
                    string type = ddlType.SelectedValue;
                    sql = "INSERT INTO TBWS_STORAGE_WARN(MARID,WARNNUM,NOTES,Type,BZJSBZ,REASONABLENUM) VALUES('" + marid + "','" + warnum + "','" + note + "','" + type + "','" + ddlsfjsbz.SelectedValue.ToString().Trim() + "','" + reasonnum + "')";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Redirect("SM_Warehouse_add_delete.aspx");
                }
            }
            if (Flag == "modify")
            {

                string strsql = "update TBWS_STORAGE_WARN set WARNNUM='" + TextBoxNumber.Text + "', NOTES='" + TextBoxNote.Text + "',Type='" + ddlType.SelectedValue.ToString().Trim() + "',BZJSBZ='" + ddlsfjsbz.SelectedValue.ToString().Trim() + "',REASONABLENUM='" + txtReasonableNum.Text.Trim() + "' where MARID='" + marid + "'";
                DBCallCommon.ExeSqlText(strsql);
                Response.Redirect("SM_Warehouse_add_delete.aspx");

            }


        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_add_delete.aspx");

        }

        protected void TextMarid_TextChanged(object sender, EventArgs e)
        {

            if ((sender as TextBox).Text.Trim().Length >= 12)
            {
                string marid = (sender as TextBox).Text.Trim().Substring(0, 12);

                string sqlText = "select MNAME,GUIGE,CAIZHI,GB,PURCUNIT from TBMA_MATERIAL where ID='" + marid + "' and STATE='1'";

                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtUnit.Text = dr["PURCUNIT"].ToString();
                        TextMarid.Text = marid;
                        TBmname.Text = dr["MNAME"].ToString();
                        TBguige.Text = dr["GUIGE"].ToString();
                        TBcaizhi.Text = dr["CAIZHI"].ToString();
                        TBgb.Text = dr["GB"].ToString();

                    }
                }

            }
        }
    }
}