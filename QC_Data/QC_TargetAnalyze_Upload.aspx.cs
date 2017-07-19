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

                loadlastdata();
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
            if (txtName.Text.Trim() == "")
            {
                Response.Write("<script>alert('名称不能为空！！！');window.close();</script>");
                return;
            }
            else
            {
                string method1 = method.Value;
                string sqltext1 = "";
                if (method1 == "New")
                {
                    string sqltext000 = "select * from TBQC_TARGET_LIST where TARGET_NAME='" + txtName.Text.Trim() + "'";
                    DataTable dt000 = DBCallCommon.GetDTUsingSqlText(sqltext000);
                    if (dt000.Rows.Count > 0)
                    {
                        Response.Write("<script>alert('已存在该名称的数据！！！');window.close();</script>");
                        return;
                    }
                    else
                    {
                        sqlText = "insert into TBQC_TARGET_LIST values ('" + txtName.Text.Trim() + "','" + txtBZ.Text.Trim() + "')";
                    }
                }
                else
                {
                    sqlText = "update TBQC_TARGET_LIST set TARGET_NAME='" + txtName.Text.Trim() + "',TARGET_NOTE='" + txtBZ.Text.Trim() + "' where TARGET_ID=" + hidTargetId.Value;
                }
                try
                {
                    DBCallCommon.ExeSqlText(sqlText);
                    if (method1 == "New")
                    {
                        if (ddl_loaddata.SelectedIndex != 0)
                        {
                            string sqltext001="select * from TBQC_TARGET_LIST where TARGET_NAME='" + txtName.Text.Trim() + "'";
                            DataTable dt001=DBCallCommon.GetDTUsingSqlText(sqltext001);
                            if (dt001.Rows.Count > 0)
                            {
                                sqltext1 = "insert into TBQC_TARGET_DETAIL(TARGET_DEPID,TARGET_TIXI,TARGET_MUBIAO,TARGET_MANAGER,TARGET_FID,TARGET_TJBMID) select TARGET_DEPID,TARGET_TIXI,TARGET_MUBIAO,TARGET_MANAGER," + CommonFun.ComTryInt(dt001.Rows[0]["TARGET_ID"].ToString().Trim()) + ",TARGET_TJBMID from TBQC_TARGET_DETAIL where TARGET_FID=" + CommonFun.ComTryInt(ddl_loaddata.SelectedValue.Trim()) + "";
                                DBCallCommon.ExeSqlText(sqltext1);
                            }
                        }
                    }
                    Response.Write("<script>alert('数据更新成功！！！');window.close();</script>");
                }
                catch (Exception)
                {

                    Response.Write("<script>alert('程序出错请稍后再试！！！');window.close();</script>");
                }
            }
        }


        //绑定之前数据
        private void loadlastdata()
        {
            string sqletxt0 = "select TARGET_ID,TARGET_NAME from TBQC_TARGET_LIST";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqletxt0);
            ddl_loaddata.DataSource = dt0;
            ddl_loaddata.DataTextField = "TARGET_NAME";
            ddl_loaddata.DataValueField = "TARGET_ID";
            ddl_loaddata.DataBind();
            ddl_loaddata.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }

    }
}
