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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_ClaimReason_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindAllData();
            }
        }

        //数据绑定
        private void BindAllData()
        {
            string sqltext = "select * from TBPM_REASONCONTROL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                grvClaimReason.DataSource = dt;
                grvClaimReason.DataKeyNames = new string[] { "SPR_ID" };
                grvClaimReason.DataBind();
                palClaimReasonAdd.Visible = false;
            }
            else
            {
                grvClaimReason.DataSource = null;
                grvClaimReason.DataBind();
                palClaimReasonAdd.Visible = true;
            }
        }
        
        protected void grvClaimReason_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) 
            { 
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) 
                 { 
                     ((LinkButton)e.Row.Cells[3].Controls[0].FindControl("lbtnDelete")).Attributes.Add("onclick", "javascript:return confirm('确认要删除：\"" + e.Row.Cells[1].Text + "\"吗？\\r\\r提示：删除后,再次添加该原因不再对该原因下已有记录进行统计！')"); 
                 } 
            } 
        }
        //更新
        protected void grvClaimReason_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = grvClaimReason.DataKeys[e.RowIndex].Value.ToString();//Values[0]
            //string SPR_DESCRIBLE = grvClaimReason.Rows[e.RowIndex].Cells[1].Text.ToString();//未修改项值的获取
            string SPR_DESCRIBLE = ((TextBox)(grvClaimReason.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString();//修改项值的获取格式
            string sqltext = "update TBPM_REASONCONTROL set SPR_DESCRIBLE='"+SPR_DESCRIBLE+"' where SPR_ID=" + id + "";
            DBCallCommon.ExeSqlText(sqltext);
            grvClaimReason.EditIndex = -1;
            this.BindAllData();
            grvClaimReason.Columns[3].Visible = true;

        }
        //编辑
        protected void grvClaimReason_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvClaimReason.Columns[3].Visible = false;
            grvClaimReason.EditIndex = e.NewEditIndex;
            this.BindAllData();
        }
        //删除
        protected void grvClaimReason_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = grvClaimReason.DataKeys[e.RowIndex].Value.ToString();
            string sqltext = "delete from TBPM_REASONCONTROL where SPR_ID=" + id + "";
            DBCallCommon.ExeSqlText(sqltext);
            this.BindAllData();
            this.ClientScript.RegisterStartupScript(GetType(), "js", "window.returnValue=\"refresh\"", true);
        }
        //取消
        protected void grvClaimReason_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvClaimReason.Columns[3].Visible = true;
            grvClaimReason.EditIndex = -1;
            this.BindAllData();
        }
        //添加
        protected void btnClaimReasonAdd_Click(object sender, EventArgs e)
        {
            if (txtClaimReason.Text.Trim() == "")
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:alert('不能为空！')", true);
            }
            else
            {
                string sqltext = "insert into TBPM_REASONCONTROL(SPR_DESCRIBLE,SPR_ACCNUM) Values('" + txtClaimReason.Text.Trim() + "',0)";
                DBCallCommon.ExeSqlText(sqltext);
                this.BindAllData();
                txtClaimReason.Text = "";
                this.ClientScript.RegisterStartupScript(GetType(), "js", "window.returnValue=\"refresh\"", true);
            }
        }
    }
}
