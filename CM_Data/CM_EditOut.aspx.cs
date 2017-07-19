using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_EditOut : System.Web.UI.Page
    {
        string CM_ID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CM_ID = Request.QueryString["CM_ID"].ToString();
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                bindData();
            }
        }

        private void bindData()
        {
            string sql = "select CM_BTOUT from TBCM_CUSTOMER where CM_ID='" + CM_ID + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                string cmout = dt.Rows[0][0].ToString();
                if (cmout == "1")
                {
                    btnConfirm.Visible = false;
                    btnAll.Visible = false;
                }
            }
            sql = "select * from TBCM_CUSINFO where CM_ID='" + CM_ID + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                rptCK.DataSource = dt;
                rptCK.DataBind();
                NoDataPanelSee();
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }

        #region 增加、删除行
        protected void btnAdd_OnClick(object sender, EventArgs e) //增加行的函数
        {
            CreateNewRow(1);
            NoDataPanelSee();
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.rptCK.DataSource = dt;
            this.rptCK.DataBind();
            //InitVar();
        }

        private DataTable GetDataTable() //临时表
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CM_OUT");
            dt.Columns.Add("CM_OUTNUM");
            dt.Columns.Add("CM_OUTDATE");
            dt.Columns.Add("CM_NOTE");

            foreach (RepeaterItem retItem in rptCK.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HiddenField)retItem.FindControl("ID")).Value;
                newRow[1] = ((TextBox)retItem.FindControl("CM_OUT")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("CM_OUTNUM")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("CM_OUTDATE")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("CM_NOTE")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)//删除一行
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CM_OUT");
            dt.Columns.Add("CM_OUTNUM");
            dt.Columns.Add("CM_OUTDATE");
            dt.Columns.Add("CM_NOTE");
            foreach (RepeaterItem retItem in rptCK.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("cbxXuHao");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((HiddenField)retItem.FindControl("ID")).Value;
                    newRow[1] = ((TextBox)retItem.FindControl("CM_OUT")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("CM_OUTNUM")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("CM_OUTDATE")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("CM_NOTE")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rptCK.DataSource = dt;
            this.rptCK.DataBind();
            NoDataPanelSee();
        }
        private void NoDataPanelSee()
        {
            if (rptCK.Items.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }
        #endregion

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (rptCK.Items.Count==0)
            {
                Response.Write("<script>alert('您还未添加出库项目！！！')</script>");
                return;
            }
            List<string> list = new List<string>();
            //更新TBCM_CUSTOMER表
            string sql = string.Format("update TBCM_CUSTOMER set CM_OUTNUM='{0}',CM_OUT='{1}',CM_OUTDATE='{2}',CM_NOTE='{3}',CM_KGYUAN='{4}' where CM_ID='{5}'", ((TextBox)rptCK.Items[0].FindControl("CM_OUTNUM")).Text, ((TextBox)rptCK.Items[0].FindControl("CM_OUT")).Text, ((TextBox)rptCK.Items[0].FindControl("CM_OUTDATE")).Text, ((TextBox)rptCK.Items[0].FindControl("CM_NOTE")).Text, UserID.Value, CM_ID);
            list.Add(sql);
            //删除TBCM_CUSINFO的旧数据
            sql = "delete from TBCM_CUSINFO where CM_ID='"+CM_ID+"'";
            list.Add(sql);
            foreach (RepeaterItem item in rptCK.Items)
            {
                string cm_out=((TextBox)item.FindControl("CM_OUT")).Text;
                string cm_outnum = ((TextBox)item.FindControl("CM_OUTNUM")).Text;
                string cm_outdate = ((TextBox)item.FindControl("CM_OUTDATE")).Text;
                string cm_note = ((TextBox)item.FindControl("CM_NOTE")).Text;
                sql = string.Format("insert into TBCM_CUSINFO (CM_ID,CM_OUT,CM_OUTNUM,CM_OUTDATE,CM_NOTE) values('{0}','{1}','{2}','{3}','{4}')", CM_ID, cm_out, cm_outnum, cm_outdate, cm_note);
                list.Add(sql);
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch 
            {
                Response.Write("<script>alert('添加的sql语句出现问题，请与管理员联系！！！')</script>");
            }
            //Response.Write("<script>alert('添加成功！')</script>");
            Response.Write("<script>window.close()</script>");
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            if (rptCK.Items.Count == 0)
            {
                Response.Write("<script>alert('您还未添加出库项目！！！')</script>");
                return;
            }
            List<string> list = new List<string>();
            //更新TBCM_CUSTOMER表
            string sql = string.Format("update TBCM_CUSTOMER set CM_BTOUT='1',CM_OUTNUM='{0}',CM_OUT='{1}',CM_OUTDATE='{2}',CM_NOTE='{3}',CM_KGYUAN='{4}' where CM_ID='{5}'", ((TextBox)rptCK.Items[0].FindControl("CM_OUTNUM")).Text, ((TextBox)rptCK.Items[0].FindControl("CM_OUT")).Text, ((TextBox)rptCK.Items[0].FindControl("CM_OUTDATE")).Text, ((TextBox)rptCK.Items[0].FindControl("CM_NOTE")).Text, UserID.Value, CM_ID);
            list.Add(sql);
            //删除TBCM_CUSINFO的旧数据
            sql = "delete from TBCM_CUSINFO where CM_ID='" + CM_ID + "'";
            list.Add(sql);
            foreach (RepeaterItem item in rptCK.Items)
            {
                string cm_out = ((TextBox)item.FindControl("CM_OUT")).Text;
                string cm_outnum = ((TextBox)item.FindControl("CM_OUTNUM")).Text;
                string cm_outdate = ((TextBox)item.FindControl("CM_OUTDATE")).Text;
                string cm_note = ((TextBox)item.FindControl("CM_NOTE")).Text;
                sql = string.Format("insert into TBCM_CUSINFO (CM_ID,CM_OUT,CM_OUTNUM,CM_OUTDATE,CM_NOTE) values('{0}','{1}','{2}','{3}','{4}')", CM_ID, cm_out, cm_outnum, cm_outdate, cm_note);
                list.Add(sql);
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                Response.Write("<script>alert('添加的sql语句出现问题，请与管理员联系！！！')</script>");
            }
            Response.Write("<script>window.close()</script>");
        }
    }
}
