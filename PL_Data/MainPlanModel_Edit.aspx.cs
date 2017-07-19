using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.PL_Data
{
    public partial class MainPlanModel_Edit : System.Web.UI.Page
    {
        string action;
        string id;
        string sqlText;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                InitInfo();
                InitGridview();
            }


        }



        private void InitInfo()
        {
            if (action == "edit")
            {
                //MODEL_ID, MODEL_NAME, MODEL_EDITDATE, MODEL_EDITUSERID, MODEL_RANGE, MODEL_NOTE
                sqlText = "select MODEL_NAME,MODEL_RANGE,MODEL_NOTE ";
                sqlText += "from dbo.TBMP_MAINPLANMODEL where MODEL_ID='" + id + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr.Read())
                {
                    lblModelId.Text = id.ToString();
                    txtModelName.Value = dr[0].ToString();
                    txtModelRange.Value = dr[1].ToString();
                    txtNote.Value = dr[2].ToString();
                }
                dr.Close();
            }
            else if (action == "add")
            {
                sqlText = "select MODEL_ID from TBMP_MAINPLANMODEL order by substring(MODEL_ID,4,6) desc";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count > 0)
                {
                    string num = dt.Rows[0][0].ToString();
                    //取出数据库中最大的ID,加一作为新的ID;

                    lblModelId.Text = "MB_" + (Convert.ToInt32(num.Substring(3, 3)) + 1).ToString().PadLeft(3, '0');
                }
                else
                {
                    lblModelId.Text = "MB_001";
                }
            }

        }
        private void InitGridview()
        {
            if (action == "edit")
            {
                sqlText = "select * from dbo.TBMP_MAINPLANMODELDETAIL where MP_MODELID ='" + id + "'";
                dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            }
            else
            {
                dt = new DataTable();
                //MP_ID, MP_MODELID, MP_PLNAME, MP_DAYS, MP_TYPE, MP_WARNINGDAYS, MP_NOTE
               
                dt.Columns.Add("MP_PLNAME");
                dt.Columns.Add("MP_DAYS");
                dt.Columns.Add("MP_TYPE");
                dt.Columns.Add("MP_WARNINGDAYS");
                dt.Columns.Add("MP_NOTE");
                dt.Columns.Add("MP_ID");
                DataRow newRow = dt.NewRow();
                newRow[0] = "材料计划";
                newRow[2] = "技术准备";
                dt.Rows.Add(newRow);
                DataRow newRow1 = dt.NewRow();
                newRow1[0] = "制作明细";
                newRow1[2] = "技术准备";
                dt.Rows.Add(newRow1);              
                DataRow newRow3 = dt.NewRow();
                newRow3[0] = "非定尺板";
                newRow3[2] = "采购周期";
                dt.Rows.Add(newRow3);
                DataRow newRow4 = dt.NewRow();
                newRow4[0] = "定尺板";
                newRow4[2] = "采购周期";
                dt.Rows.Add(newRow4);
                DataRow newRow5 = dt.NewRow();
                newRow5[0] = "耐磨板";
                newRow5[2] = "采购周期";
                dt.Rows.Add(newRow5);
                DataRow newRow6 = dt.NewRow();
                newRow6[0] = "型材";
                newRow6[2] = "采购周期";
                dt.Rows.Add(newRow6);
                DataRow newRow7 = dt.NewRow();
                newRow7[0] = "圆钢";
                newRow7[2] = "采购周期";
                dt.Rows.Add(newRow7);
                DataRow newRow8 = dt.NewRow();
                newRow8[0] = "标准件";
                newRow8[2] = "采购周期";
                dt.Rows.Add(newRow8);
                DataRow newRow9 = dt.NewRow();
                newRow9[0] = "铸件";
                newRow9[2] = "采购周期";
                dt.Rows.Add(newRow9);
                DataRow newRow10 = dt.NewRow();
                newRow10[0] = "锻件";
                newRow10[2] = "采购周期";
                dt.Rows.Add(newRow10);
                DataRow newRow11 = dt.NewRow();

                newRow11[0] = "采购成品";
                newRow11[2] = "采购周期";
                dt.Rows.Add(newRow11);
                DataRow newRow12 = dt.NewRow();
                newRow12[0] = "成品入库";
                newRow12[2] = "生产周期";
                dt.Rows.Add(newRow12);

            }
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        protected void lbnSave_Click(object sender, EventArgs e)
        {
            string ret = this.CheckData();
            if (ret == "0")
            {
                List<string> sqllist = new List<string>();

                //MODEL_ID, MODEL_NAME, MODEL_EDITDATE, MODEL_EDITUSERID, MODEL_RANGE, MODEL_NOTE
                if (action == "edit")
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        GridViewRow gRow = GridView1.Rows[i];
                        string type = gRow.Cells[1].Text;
                        string plname = gRow.Cells[2].Text;
                        string detailId = ((HiddenField)gRow.FindControl("hidDetailId")).Value.Trim();
                        string txtWarningDays = ((HtmlInputText)gRow.FindControl("txtWarningDays")).Value.Trim();
                        string days = ((HtmlInputText)gRow.FindControl("txtDays")).Value.Trim();
                        string note = ((HtmlInputText)gRow.FindControl("txtDetailNote")).Value;
                        sqlText = "update TBMP_MAINPLANMODELDETAIL set MP_DAYS='" + days + "',MP_WARNINGDAYS='" + txtWarningDays + "',MP_NOTE='" + note + "' where MP_ID='" + detailId + "'";
                        sqllist.Add(sqlText);
                    }
                    sqlText = "update TBMP_MAINPLANMODEL set MODEL_NAME='" + txtModelName.Value.Trim() + "',MODEL_EDITDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',MODEL_EDITUSERID='" + Session["UserID"] + "',MODEL_RANGE='" + txtModelRange.Value.Trim() + "',MODEL_NOTE='"+txtNote.Value.Trim()+"' where MODEL_ID='"+lblModelId.Text.Trim()+"'";
                    sqllist.Add(sqlText);
                }
                else
                {
                    sqlText = "select count(1) from TBMP_MAINPLANMODEL where MODEL_ID='"+lblModelId.Text+"'";
                    dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    if (dt.Rows[0][0].ToString()=="0")
                    {
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            GridViewRow gRow = GridView1.Rows[i];
                            string type = gRow.Cells[1].Text;
                            string plname = gRow.Cells[2].Text;
                            string detailId = ((HiddenField)gRow.FindControl("hidDetailId")).Value.Trim();
                            string txtWarningDays = ((HtmlInputText)gRow.FindControl("txtWarningDays")).Value.Trim();
                            string days = ((HtmlInputText)gRow.FindControl("txtDays")).Value.Trim();
                            string note = ((HtmlInputText)gRow.FindControl("txtDetailNote")).Value;
                            sqlText = "insert into TBMP_MAINPLANMODELDETAIL values('" + lblModelId.Text + "','" + plname + "','" + days + "','" + type + "','" + txtWarningDays + "','" + note + "') ";
                            sqllist.Add(sqlText);
                        }
                        sqlText = "insert into TBMP_MAINPLANMODEL values ('" + lblModelId.Text + "','" + txtModelName.Value.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["UserID"] + "','"+txtModelRange.Value.Trim()+"','"+txtNote.Value.Trim()+"')";
                        sqllist.Add(sqlText);
                    }
                    else
                    {
                        Response.Write("<script>alert('模板编号重复，请重新添加')</script>");
                        return;
                    }
                
                }

               
                try
                {
                    DBCallCommon.ExecuteTrans(sqllist);


                }
                catch (Exception)
                {


                    Response.Write("<script>alert('数据错误，请联系管理员')</script>");
                }
                Response.Write("<script>alert('保存成功')</script>");
                Response.Write("<script>location.href='MainPlanModel_View.aspx'</script>");

            }
            else
            {
                if (ret.Contains("warn"))
                {
                    Response.Write("<script>alert('【预警天数】格式错误，应为数字格式')</script>");
                }
                else if (ret.Contains("day"))
                {
                    Response.Write("<script>alert('【计划天数】格式错误，应为数字格式')</script>");
                }
                else if (ret.Contains("name"))
                {
                    Response.Write("<script>alert('请填写模板名称')</script>");
                }
            }

        }
        private string CheckData()
        {
            string result = "0";
            string pattern = @"^\d{1,2}$";
            Regex rgx = new Regex(pattern);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //MP_ID, MP_ENGID, MP_PLNAME, MP_DAYS, MP_STARTDATA, MP_ENDTIME, MP_STATE, MP_ACTURALTIME, MP_TYPE
                GridViewRow gRow = GridView1.Rows[i];

                string txtWarningDays = ((HtmlInputText)gRow.FindControl("txtWarningDays")).Value.Trim();
                string days = ((HtmlInputText)gRow.FindControl("txtDays")).Value.Trim();
                if (!rgx.IsMatch(txtWarningDays))
                {
                    result = "warning";
                    return result;
                }
                else if (!rgx.IsMatch(days))
                {
                    result = "day";
                    return result;
                }

            }
            if (txtModelName.Value.Trim()=="")
            {
                result = "name";
            }
            return result;
        }
    }
}
