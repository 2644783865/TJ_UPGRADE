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
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_YULIAO_INBILL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initinfo();
            }
        }
        private void initinfo()
        {
            if (Request.QueryString["action"].ToString() == "add")
            {
                lblInDoc.Text = Session["UserName"].ToString();
                lblInDocID.Text = Session["UserID"].ToString();
                lblInDate.Text = DateTime.Now.ToString();
                newdocunum();
                CreateNewRow();
            }
        }
        private void newdocunum()
        {
            string sqltext;
            sqltext = "select TOP 1 dbo.GetCode(INCODE) AS TopIndex from TBWS_YULIAO_IN ORDER BY dbo.GetCode(INCODE) DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            lblInCode.Text = "YULIAOIN" + code.PadLeft(4, '0');
        }
        //定义DataTable
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Marid");
            dt.Columns.Add("Name");
            dt.Columns.Add("CAIZHI");
            dt.Columns.Add("GUIGE");
            dt.Columns.Add("MWEIGHT");
            dt.Columns.Add("Length");
            dt.Columns.Add("Width");
            dt.Columns.Add("INNUM");
            dt.Columns.Add("TUXING");
            dt.Columns.Add("Weight");
            dt.Columns.Add("NOTE");
            for (int i = 0; i < SM_YULIAO_List_Repeater.Items.Count; i++)
            {
                RepeaterItem Reitem = SM_YULIAO_List_Repeater.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)Reitem.FindControl("Marid")).Text;
                newRow[1] = ((TextBox)Reitem.FindControl("Name")).Text;
                newRow[2] = ((TextBox)Reitem.FindControl("CAIZHI")).Text;
                newRow[3] = ((TextBox)Reitem.FindControl("GUIGE")).Text;
                newRow[4] = ((TextBox)Reitem.FindControl("MWEIGHT")).Text;
                newRow[5] = ((TextBox)Reitem.FindControl("Length")).Text;
                newRow[6] = ((TextBox)Reitem.FindControl("Width")).Text;
                newRow[7] = ((TextBox)Reitem.FindControl("INNUM")).Text;
                newRow[8] = ((DropDownList)Reitem.FindControl("TUXING")).Text;
                newRow[9] = ((TextBox)Reitem.FindControl("Weight")).Text;
                newRow[10] = ((TextBox)Reitem.FindControl("NOTE")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        //生成输入1行函数
        private void CreateNewRow()
        {
            DataTable dt = this.GetDataTable();
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            this.SM_YULIAO_List_Repeater.DataSource = dt;
            this.SM_YULIAO_List_Repeater.DataBind();
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            if (Request.QueryString["action"].ToString() == "add")
            {
                string sqltext = "";
                string marid = "";
                string Length = "";
                string Width = "";
                string INNUM = "";
                string TUXING = "";
                string Weight = "";
                string NOTE = "";
                foreach (RepeaterItem Reitem in SM_YULIAO_List_Repeater.Items)
                {
                    marid = ((TextBox)Reitem.FindControl("Marid")).Text.Trim();
                    Length = ((TextBox)Reitem.FindControl("Length")).Text.Trim();
                    Width = ((TextBox)Reitem.FindControl("Width")).Text.Trim();
                    TUXING = ((DropDownList)Reitem.FindControl("TUXING")).Text.Trim();
                    INNUM = ((TextBox)Reitem.FindControl("INNUM")).Text.Trim();
                    Weight = ((TextBox)Reitem.FindControl("Weight")).Text.Trim();
                    NOTE = ((TextBox)Reitem.FindControl("NOTE")).Text.Trim();
                    Regex regNum = new Regex(@"^-?(\d)*$");
                    Regex regWeight = new Regex(@"^-?(\d)*((.?)(\d){1,4})?$");
                    if (!regNum.IsMatch(INNUM))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入正确的入库数量！');", true);
                        return;
                    }
                    if (!regWeight.IsMatch(Weight))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入正确的入库重量！');", true);
                        return;
                    }
                    if (!string.IsNullOrEmpty(marid))
                    {
                        sqltext = "insert into TBWS_YULIAO_IN (INCODE, Marid, INNUM, RECEIVERID, INDATE, NOTE, Length, Width, Tuxing, Weight)" +
                                         "values('" + lblInCode.Text.Trim().ToString() + "','" + marid + "','" + INNUM + "','" + lblInDocID.Text.Trim().ToString() + "','" + lblInDate.Text.Trim().ToString() + "','" + NOTE + "','" + Length + "','" + Width + "','" + TUXING + "','" + Weight + "')";
                        list_sql.Add(sqltext);
                        string sqltext1 = "select distinct NUMBER,Weight FROM TBWS_YULIAO_RESTORE WHERE Marid='" + marid + "' and Length='" + Length + "' and Width='" + Width + "' and Tuxing='" + TUXING + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext1);

                        if (dt.Rows.Count.ToString() != "0")
                        {
                            int NUMBER = Convert.ToInt32(dt.Rows[0]["NUMBER"].ToString());
                            string Weight1 = dt.Rows[0]["Weight"].ToString();
                            NUMBER = NUMBER + Convert.ToInt32(INNUM);
                            Weight1 = Weight1 + Weight;
                            if (NUMBER!=0)
                            {
                                sqltext = "update TBWS_YULIAO_RESTORE set NUMBER='" + NUMBER + "',Weight='" + Weight1 + "' WHERE Marid='" + marid + "' and Lenght=" + Length + " and Width=" + Width + " and Tuxing='" + TUXING + "'";
                            }
                            else
                            {
                                sqltext = "delete from  TBWS_YULIAO_RESTORE WHERE Marid='" + marid + "' and Lenght=" + Length + " and Width=" + Width + " and Tuxing='" + TUXING + "'";
                            }
                           
                            list_sql.Add(sqltext);
                        }
                        else
                        {
                            sqltext = "insert into TBWS_YULIAO_RESTORE (Marid, NUMBER, Length, Width, Tuxing, Weight) values ('" + marid + "','" + INNUM + "','" + Length + "','" + Width + "','" + TUXING + "','" + Weight + "')";
                            list_sql.Add(sqltext);
                        }

                    }

                }
            }
            DBCallCommon.ExecuteTrans(list_sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.location.href='SM_YULIAO_IN.aspx'", true);
        }
        protected void btn_addrow_Click(object sender, EventArgs e)
        {
            CreateNewRow();
        }
        protected void btn_delectrow_Click(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < SM_YULIAO_List_Repeater.Items.Count; i++)
            {
                RepeaterItem Reitem = SM_YULIAO_List_Repeater.Items[i];
                CheckBox chk = (CheckBox)Reitem.FindControl("CHK");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要删除的行！');", true);
            }
            this.SM_YULIAO_List_Repeater.DataSource = dt;
            this.SM_YULIAO_List_Repeater.DataBind();
        }
    }
}
