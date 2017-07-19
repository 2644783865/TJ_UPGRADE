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
    public partial class TM_Packing_List : System.Web.UI.Page
    {
        string sqlText;
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        private void InitInfo()
        {
            if (Request.QueryString["action"] != null)
            {
                pk_no.Value = Request.QueryString["action"];
                status.Value = "0";
            }
            else
            {
                string[] fields = Request.QueryString["id"].ToString().Split('.');
                pk_no.Value = fields[0].ToString() + '.' + fields[1].ToString();
                status.Value = fields[2].ToString();
                if (int.Parse(status.Value) > 1)
                {
                    Response.Redirect("TM_Packing_List_Audit.aspx?id=" + pk_no.Value);
                }
            }
            sqlText = "select PLT_CONTRACTOR,PLT_OWNER,PLT_SHIPNO,PLT_SYSTEMNUM,PLT_GOODSNAME,";
            sqlText += "PLT_DELIVERYDATE,PLT_SUBMITNM from TBPM_PACKLISTTOTAL ";
            sqlText += "where PLT_PACKLISTNO='" + pk_no.Value + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                Gen_Contractor.Value = dr[0].ToString();
                Owner.Value = dr[1].ToString();
                Ship_No.Value = dr[2].ToString();
                Sys_No.Value = dr[3].ToString();
                Goods_Name.Value = dr[4].ToString();
                Con_Date.Value = dr[5].ToString();
                Signature.Value = dr[6].ToString();
            }
            dr.Close();
            GetPkData();
        }

        private void GetPkData()
        {
            sqlText = "select * from TBPM_PACKINGLIST ";
            if (status.Value == "3" || status.Value == "5" || status.Value == "7")
            {
                sqlText += "where PL_PACKLISTNO='" + pk_no.Value + "' and PL_STATE='4'";
            }
            else
            {
                sqlText += "where PL_PACKLISTNO='" + pk_no.Value + "' and PL_STATE in ('0','1','2','3','5')";
            }
            DBCallCommon.BindGridView(GridView1, sqlText);
        }
        //定义表的结构
        protected DataTable GetDataFrom()
        {
            DataTable dt1 = new DataTable("Table1");
            #region
            dt1.Columns.Add("PL_NO");
            dt1.Columns.Add("PL_PACKAGENO");
            dt1.Columns.Add("PL_ITEMNO");
            dt1.Columns.Add("PL_PACKNAME");
            dt1.Columns.Add("PL_MARKINGNO");
            dt1.Columns.Add("PL_PACKQLTY");
            dt1.Columns.Add("PL_PACKTYPE");
            dt1.Columns.Add("PL_PACKDIML");
            dt1.Columns.Add("PL_PACKDIMW");
            dt1.Columns.Add("PL_PACKDIMH");
            dt1.Columns.Add("PL_TOTALVOLUME");
            dt1.Columns.Add("PL_SINGLENETWGHT");
            dt1.Columns.Add("PL_SINGLEGROSSWGHT");
            dt1.Columns.Add("PL_TOTALGROSSWGHT");
            dt1.Columns.Add("PL_DESCRIPTION");
            dt1.Columns.Add("PL_OUTLINEDRAWING");
            #endregion
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt1.NewRow();
                #region
                newRow[0] = ((TextBox)gRow.FindControl("lblIndex")).Text;
                newRow[1] = ((TextBox)gRow.FindControl("case_no")).Text;
                newRow[2] = ((TextBox)gRow.FindControl("lab_Itemno")).Text;
                newRow[3] = ((TextBox)gRow.FindControl("lab_name")).Text;
                newRow[4] = ((TextBox)gRow.FindControl("lab_biaozhi")).Text;
                newRow[5] = ((TextBox)gRow.FindControl("txt_num")).Text;
                newRow[6] = ((TextBox)gRow.FindControl("txt_style")).Text;
                newRow[7] = ((TextBox)gRow.FindControl("txt_long")).Text;
                newRow[8] = ((TextBox)gRow.FindControl("lab_width")).Text;
                newRow[9] = ((TextBox)gRow.FindControl("lab_height")).Text;
                newRow[10] = ((TextBox)gRow.FindControl("lab_volume")).Text;
                newRow[11] = ((TextBox)gRow.FindControl("txt_netweight")).Text;
                newRow[12] = ((TextBox)gRow.FindControl("txt_grossweight")).Text;
                newRow[13] = ((TextBox)gRow.FindControl("txt_totalweight")).Text;
                newRow[14] = ((TextBox)gRow.FindControl("txt_cause")).Text;
                newRow[15] = ((TextBox)gRow.FindControl("txt_view")).Text;
                #endregion
                dt1.Rows.Add(newRow);
            }
            dt1.AcceptChanges();
            return dt1;
        }
        //删除函数
        private void Deletebind()
        {
            sqlText = "delete from TBPM_PACKINGLIST ";
            sqlText += "where PL_PACKLISTNO='" + pk_no.Value + "' and PL_STATE in ('0','1','4')";
            DBCallCommon.ExeSqlText(sqlText);
        }
        //插入操作
        protected void btninsert_Click(object sender, EventArgs e)
        {
            if (istid.Value == "1")
            {
                DataTable dt = this.GetDataFrom();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gRow.FindControl("chk");
                    if (chk.Checked)
                    {
                        DataRow newRow = dt.NewRow();
                        dt.Rows.InsertAt(newRow, i + 1 + count);
                        count++;
                    }
                }
                istid.Value = "0";
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
            }
        }
        //删除操作
        protected void btndelete_Click(object sender, EventArgs e)
        {
            DataTable dt = this.GetDataFrom();
            if (txtid.Value == "1")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gRow.FindControl("chk");
                    if (chk.Checked)
                    {
                        dt.Rows.RemoveAt(i - count);
                        count++;
                    }
                }
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
        //保存操作
        protected void btnsave_Click(object sender, EventArgs e)
        {
            string lblIndex = "";
            string case_no = "";
            string lab_Itemno = "";
            string lab_name = "";
            string lab_biaozhi = "";
            string txt_num = "";
            string txt_style = "";
            string txt_long = "";
            string lab_width = "";
            string lab_height = "";
            string lab_volume = "";
            string txt_netweight = "";
            string txt_grossweight = "";
            string txt_totalweight = "";
            string txt_cause = "";
            string txt_view = "";
            Deletebind();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                lblIndex = ((TextBox)gRow.FindControl("lblIndex")).Text;
                case_no = ((TextBox)gRow.FindControl("case_no")).Text;
                lab_Itemno = ((TextBox)gRow.FindControl("lab_Itemno")).Text;
                lab_name = ((TextBox)gRow.FindControl("lab_name")).Text;
                lab_biaozhi = ((TextBox)gRow.FindControl("lab_biaozhi")).Text;
                txt_num = ((TextBox)gRow.FindControl("txt_num")).Text;
                txt_style = ((TextBox)gRow.FindControl("txt_style")).Text;
                txt_long = ((TextBox)gRow.FindControl("txt_long")).Text;
                lab_width = ((TextBox)gRow.FindControl("lab_width")).Text;
                lab_height = ((TextBox)gRow.FindControl("lab_height")).Text;
                lab_volume = ((TextBox)gRow.FindControl("lab_volume")).Text;
                txt_netweight = ((TextBox)gRow.FindControl("txt_netweight")).Text;
                txt_grossweight = ((TextBox)gRow.FindControl("txt_grossweight")).Text;
                txt_totalweight = ((TextBox)gRow.FindControl("txt_totalweight")).Text;
                txt_cause = ((TextBox)gRow.FindControl("txt_cause")).Text;
                txt_view = ((TextBox)gRow.FindControl("txt_view")).Text;
                sqlText = "insert into TBPM_PACKINGLIST ";
                sqlText += "(PL_NO,PL_PACKLISTNO,PL_PACKAGENO,PL_ITEMNO,PL_PACKNAME,PL_MARKINGNO,";
                sqlText += "PL_PACKQLTY,PL_PACKTYPE,PL_PACKDIML,PL_PACKDIMW,PL_PACKDIMH,";
                sqlText += "PL_TOTALVOLUME,PL_SINGLENETWGHT,PL_SINGLEGROSSWGHT,PL_TOTALGROSSWGHT,";
                sqlText += "PL_DESCRIPTION,PL_OUTLINEDRAWING,PL_STATE) values ('" + lblIndex + "','"+pk_no.Value+"',";
                sqlText += "'" + case_no + "','" + lab_Itemno + "','" + lab_name + "','" + lab_biaozhi + "','" + txt_num + "','" + txt_style + "',";
                sqlText += "'" + txt_long + "','" + lab_width + "','" + lab_height + "','" + lab_volume + "','" + txt_netweight + "',";
                sqlText += "'" + txt_grossweight + "','" + txt_totalweight + "','" + txt_cause + "','" + txt_view + "','1')";
                DBCallCommon.ExeSqlText(sqlText);
            }
            if (status.Value == "0" || status.Value == "1" || status.Value == "3" || status.Value == "5" || status.Value == "7")
            {
                sqlText = "update TBPM_PACKLISTTOTAL set PLT_STATE='1',";
                sqlText += "PLT_CONTRACTOR='" + Gen_Contractor.Value + "',PLT_OWNER='" + Owner.Value+ "',";
                sqlText += "PLT_SHIPNO='" + Ship_No.Value + "',PLT_DELIVERYDATE='" + Con_Date.Value+ "',";
                sqlText += "PLT_SYSTEMNUM='" + Sys_No.Value + "',PLT_GOODSNAME='" + Goods_Name.Value+ "',";
                sqlText += "PLT_REVIEWA='',PLT_REVIEWANAME='',PLT_REVIEWAADVC='',";
                sqlText += "PLT_REVIEWATIME='',PLT_REVIEWB='',PLT_REVIEWBNAME='',";
                sqlText += "PLT_REVIEWBADVC='',PLT_REVIEWBTIME='',PLT_REVIEWC='',";
                sqlText += "PLT_REVIEWCNAME='',PLT_REVIEWCADVC='',PLT_REVIEWCTIME='',PLT_ADATE='' ";
                sqlText += "where PLT_PACKLISTNO='" + pk_no.Value + "' and PLT_STATE='" + status.Value + "'";
                DBCallCommon.ExeSqlText(sqlText);
            }
            Response.Redirect("TM_Packing_List_Audit.aspx?id=" + pk_no.Value);
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
