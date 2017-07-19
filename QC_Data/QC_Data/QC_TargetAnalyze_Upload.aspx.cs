using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_TargetAnalyze_Upload : System.Web.UI.Page
    {
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string TargetId = Request.QueryString["Id"].ToString();
                if (TargetId != "0")
                {
                    this.InitPage(TargetId);
                    method.Value = "Edit";
                    hidTargetId.Value = TargetId;
                }
                else
                {
                    method.Value = "New";
                }
              

            }
        }

        private void InitPage(string proid)
        {

            sqlText = "select * from TBQC_TARGET_LIST where TARGET_ID=" + proid;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtName.Text = row["TARGET_NAME"].ToString();
                txtBZ.Text = row["TARGET_NOTE"].ToString();
               
            }

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string method1 = method.Value;
            if (method1=="New")
            {
                sqlText = "insert into TBQC_TARGET_LIST values ('"+txtName.Text.Trim()+"','"+txtBZ.Text.Trim()+"')";
            }
            else
            {
                sqlText = "update TBQC_TARGET_LIST set TARGET_NAME='" + txtName.Text.Trim() + "',TARGET_NOTE='" + txtBZ.Text.Trim() + "' where TARGET_ID=" + hidTargetId.Value;
            }
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('数据更新成功！！！');window.close();</script>");
            }
            catch (Exception)
            {

                Response.Write("<script>alert('程序出错请稍后再试！！！');window.close();</script>");
            }
        }

    }
}
