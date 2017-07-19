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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ContractEditDialog : System.Web.UI.Page
    {
        string action = string.Empty;
        string stid = string.Empty;
        string stname = string.Empty;
        string cstcontract = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString();
            stid = Request.QueryString["ST_ID"].ToString();
            stname = Request.QueryString["ST_NAME"].ToString();
            cstcontract = Request.QueryString["C_STContract"].ToString();

            if (!IsPostBack)
            {
                ShowDate();
                PowerCheck();
            }
        }

        protected void ShowDate()
        {
            string sql = "select substring(C_Contract1,0,11)C_ContractS1, substring(C_Contract1,12,11)C_ContractE1,substring(C_Contract2,0,11)C_ContractS2,substring(C_Contract2,12,11)C_ContractE2,substring(C_Contract3,0,11)C_ContractS3, substring(C_Contract3,12,11)C_ContractE3,substring(C_Contract4,0,11)C_ContractS4,substring(C_Contract4,12,11)C_ContractE4,substring(C_Contract5,0,11)C_ContractS5, substring(C_Contract5,12,11)C_ContractE5,substring(C_Contract6,0,11)C_ContractS6,substring(C_Contract6,12,11)C_ContractE6,substring(C_Contract7,0,11)C_ContractS7, substring(C_Contract7,12,11)C_ContractE7,substring(C_Contract8,0,11)C_ContractS8,substring(C_Contract8,12,11)C_ContractE8,substring(C_Contract9,0,11)C_ContractS9, substring(C_Contract9,12,11)C_ContractE9,substring(C_Contract10,0,11)C_ContractS10,substring(C_Contract10,12,11)C_ContractE10,substring(C_Contract11,0,11)C_ContractS11, substring(C_Contract11,12,11)C_ContractE11,substring(C_Contract12,0,11)C_ContractS12,substring(C_Contract12,12,11)C_ContractE12,substring(C_Contract13,0,11)C_ContractS13, substring(C_Contract13,12,11)C_ContractE13,substring(C_Contract14,0,11)C_ContractS14,substring(C_Contract14,12,11)C_ContractE14,substring(C_Contract15,0,11)C_ContractS15, substring(C_Contract15,12,11)C_ContractE15,substring(C_Contract16,0,11)C_ContractS16,substring(C_Contract16,12,11)C_ContractE16,substring(C_Contract17,0,11)C_ContractS17, substring(C_Contract17,12,11)C_ContractE17,substring(C_Contract18,0,11)C_ContractS18,substring(C_Contract18,12,11)C_ContractE18,substring(C_Contract19,0,11)C_ContractS19, substring(C_Contract19,12,11)C_ContractE19,substring(C_Contract20,0,11)C_ContractS20,substring(C_Contract20,12,11)C_ContractE20,substring(C_Contract21,0,11)C_ContractS21, substring(C_Contract21,12,11)C_ContractE21,substring(C_Contract22,0,11)C_ContractS22,substring(C_Contract22,12,11)C_ContractE22,substring(C_Contract23,0,11)C_ContractS23, substring(C_Contract23,12,11)C_ContractE23,substring(C_Contract24,0,11)C_ContractS24,substring(C_Contract24,12,11)C_ContractE24,substring(C_Contract25,0,11)C_ContractS25, substring(C_Contract25,12,11)C_ContractE25,substring(C_Contract26,0,11)C_ContractS26,substring(C_Contract26,12,11)C_ContractE26,substring(C_Contract27,0,11)C_ContractS27, substring(C_Contract27,12,11)C_ContractE27,substring(C_Contract28,0,11)C_ContractS28,substring(C_Contract28,12,11)C_ContractE28,substring(C_Contract29,0,11)C_ContractS29, substring(C_Contract29,12,11)C_ContractE29,substring(C_Contract30,0,11)C_ContractS30,substring(C_Contract30,12,11)C_ContractE30,C_EditNote FROM OM_ContractRecord where C_STID='" + stid.Trim() + "'and C_STContract='" + cstcontract + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (Control item in DivRank.Controls)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Text = dt.Rows[0][((TextBox)item).ID.ToString()].ToString();
                    }
                }
                C_EditNote.Text = dt.Rows[0]["C_EditNote"].ToString();
            }
            lbtop.Text = "合同签订信息-" + stname + "(" + cstcontract + ")";
        }

        protected void PowerCheck()
        {
            if (action == "view")
                PanInfo.Enabled = false;
            else
                PanInfo.Enabled = true;
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int time = CommonFun.ComTryInt(inputmax.Value) + 1;
            int times = time / 2;

            #region
            List<string> list = new List<string>();
            string txt1 = C_ContractS1.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE1.Text.ToString().Trim().Replace('-', '.');
            string txt2 = C_ContractS2.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE2.Text.ToString().Trim().Replace('-', '.');
            string txt3 = C_ContractS3.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE3.Text.ToString().Trim().Replace('-', '.');
            string txt4 = C_ContractS4.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE4.Text.ToString().Trim().Replace('-', '.');
            string txt5 = C_ContractS5.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE5.Text.ToString().Trim().Replace('-', '.');
            string txt6 = C_ContractS6.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE6.Text.ToString().Trim().Replace('-', '.');
            string txt7 = C_ContractS7.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE7.Text.ToString().Trim().Replace('-', '.');
            string txt8 = C_ContractS8.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE8.Text.ToString().Trim().Replace('-', '.');
            string txt9 = C_ContractS9.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE9.Text.ToString().Trim().Replace('-', '.');
            string txt10 = C_ContractS10.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE10.Text.ToString().Trim().Replace('-', '.');
            string txt11 = C_ContractS11.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE11.Text.ToString().Trim().Replace('-', '.');
            string txt12 = C_ContractS12.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE12.Text.ToString().Trim().Replace('-', '.');
            string txt13 = C_ContractS13.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE13.Text.ToString().Trim().Replace('-', '.');
            string txt14 = C_ContractS14.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE14.Text.ToString().Trim().Replace('-', '.');
            string txt15 = C_ContractS15.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE15.Text.ToString().Trim().Replace('-', '.');
            string txt16 = C_ContractS16.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE16.Text.ToString().Trim().Replace('-', '.');
            string txt17 = C_ContractS17.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE17.Text.ToString().Trim().Replace('-', '.');
            string txt18 = C_ContractS18.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE18.Text.ToString().Trim().Replace('-', '.');
            string txt19 = C_ContractS19.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE19.Text.ToString().Trim().Replace('-', '.');
            string txt20 = C_ContractS20.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE20.Text.ToString().Trim().Replace('-', '.');
            string txt21 = C_ContractS21.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE21.Text.ToString().Trim().Replace('-', '.');
            string txt22 = C_ContractS22.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE22.Text.ToString().Trim().Replace('-', '.');
            string txt23 = C_ContractS23.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE23.Text.ToString().Trim().Replace('-', '.');
            string txt24 = C_ContractS24.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE24.Text.ToString().Trim().Replace('-', '.');
            string txt25 = C_ContractS25.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE25.Text.ToString().Trim().Replace('-', '.');
            string txt26 = C_ContractS26.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE26.Text.ToString().Trim().Replace('-', '.');
            string txt27 = C_ContractS27.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE27.Text.ToString().Trim().Replace('-', '.');
            string txt28 = C_ContractS28.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE28.Text.ToString().Trim().Replace('-', '.');
            string txt29 = C_ContractS29.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE29.Text.ToString().Trim().Replace('-', '.');
            string txt30 = C_ContractS30.Text.ToString().Trim().Replace('-', '.') + "-" + C_ContractE30.Text.ToString().Trim().Replace('-', '.');
            #endregion

            string key = "C_Contract1,C_Contract2,C_Contract3,C_Contract4,C_Contract5,C_Contract6,C_Contract7,C_Contract8,C_Contract9,C_Contract10,C_Contract11,C_Contract12,C_Contract13,C_Contract14,C_Contract15,C_Contract16,C_Contract17,C_Contract18,C_Contract19,C_Contract20,C_Contract21,C_Contract22,C_Contract23,C_Contract24,C_Contract25,C_Contract26,C_Contract27,C_Contract28,C_Contract29,C_Contract30";
            string value = "'" + txt1 + "','" + txt2 + "','" + txt3 + "','" + txt4 + "','" + txt5 + "','" + txt6 + "','" + txt7 + "','" + txt8 + "','" + txt9 + "','" + txt10 + "','" + txt11 + "','" + txt12 + "','" + txt13 + "','" + txt14 + "','" + txt15 + "','" + txt16 + "','" + txt17 + "','" + txt18 + "','" + txt19 + "','" + txt20 + "','" + txt21 + "','" + txt22 + "','" + txt23 + "','" + txt24 + "','" + txt25 + "','" + txt26 + "','" + txt27 + "','" + txt28 + "','" + txt29 + "','" + txt30 + "'";
            string sql = "";
            string sqldelete = "";
            if (times > 0)
            {
                for (int i = 0; i < 30; i++)
                {
                    if (value.Split(',')[i].Split('-')[0].Length > 1 && value.Split(',')[i].Split('-')[0].Length != 11)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写开始日期');", true);
                        return;
                    }
                    if (value.Split(',')[i].Split('-')[0] == "'" && value.Split(',')[i].Split('-')[1].Length > 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('开始日期不能为空');", true);
                        return;
                    }
                    sql = "update OM_ContractRecord set " + key.Split(',')[i] + " = " + value.Split(',')[i] + " where C_STID='" + stid.Trim() + "'and C_STContract='" + cstcontract + "'";
                    list.Add(sql);
                    if (value.Split(',')[i].Length == 3)
                    {
                        for (int j = i; j < 30; j++)
                        {
                            sqldelete = "update OM_ContractRecord set " + key.Split(',')[j] + " = '' where C_STID='" + stid.Trim() + "'and C_STContract='" + cstcontract + "'";
                            list.Add(sqldelete);
                        }
                        break;
                    }
                }
                string sqlText = "update OM_ContractRecord set C_EditPersonID='" + Session["UserID"].ToString() + "', C_EditPerson='" + Session["UserName"].ToString() + "', C_EditTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', C_EditNote='" + C_EditNote.Text.ToString().Trim() + "'where C_STID='" + stid.Trim() + "'and C_STContract='" + cstcontract + "'";
                list.Add(sqlText);
            }
            else
            {
                sql = "delete from OM_ContractRecord where C_STID='" + stid.Trim() + "'and C_STContract='" + cstcontract + "'";
                list.Add(sql);
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "closeWin", "<script> this.close();window.opener.location.reload();</script>", false);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改失败！');", true);
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Write("<script>window.close();</script>");
        }
    }
}
