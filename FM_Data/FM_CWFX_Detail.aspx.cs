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
using System.IO;
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_CWFX_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Request.QueryString["action"] == "look")
            {
                this.Title = "财务分析信息查看";
                DateShow();
                foreach (Control contrl in Panel1.Controls)
                {
                    if (contrl is TextBox)
                    {
                        ((TextBox)contrl).Enabled = false;
                    }
                }
                btnConfirm.Visible = false;
                btnCancel.Visible = false;
            }
            else
            {
                this.Title = "修改财务分析信息";
                btnConfirm.Text = "修改";
                RQBH.Enabled = false;
                if (!IsPostBack)
                {
                    InitInfo();

                }
            }
        }
        private void DateShow()
        {
            string cx_id = Request.QueryString["id"].ToString();//得到修改人员编码
            string sqlText = "select * from TBFM_CWFX where RQBH='" + cx_id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DataRow dr = dt.Rows[0];
            RQBH.Text = dr["RQBH"].ToString();
            foreach (Control contrl in Panel1.Controls)
            {
                if (contrl is TextBox)
                {
                    ((TextBox)contrl).Text = dr[((TextBox)contrl).ID.ToString()].ToString();
                }
            }
        }
        private void InitInfo()
        {
            DateShow();
        }

        #region 修改
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
                string zc_id = Request.QueryString["id"].ToString();
                string sql = string.Empty;
                List<string> list = new List<string>();
                foreach (Control contrl in Panel1.Controls)
                {
                    if (contrl is TextBox)
                    {
                        string str = ((TextBox)contrl).ID.ToString();
                        sql = string.Format("update TBFM_CWFX set {0}='{1}' where RQBH='{2}'", ((TextBox)contrl).ID.ToString(), ((TextBox)contrl).Text, zc_id);
                            list.Add(sql);
                    }
                }
                DBCallCommon.ExecuteTrans(list);
                Response.Write("<script>alert('修改成功！')</script>");
                Response.Write("<script>window.close()</script>");
        }
        #endregion


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");

        }
    }
}
