using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_JJZBTJ_Detail : System.Web.UI.Page
    {
        string action = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                action = Request.QueryString["action"].ToString().Trim();
                if (action == "update")
                {
                    datashow();
                }
                else if (action == "look")
                {
                    datashow();
                    dplYear.Enabled = false;
                    dplMoth.Enabled = false;
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
            }
        }

        //数据绑定
        private void datashow()
        {
            string sqlbind = "select * from FM_JJZBTJ where ID='" + Request.QueryString["id"].ToString().Trim() + "'";
            DataTable dtbind = DBCallCommon.GetDTUsingSqlText(sqlbind);
            if (dtbind.Rows.Count > 0)
            {
                dplYear.SelectedValue = dtbind.Rows[0]["YEAR"].ToString().Trim();
                dplMoth.SelectedValue = dtbind.Rows[0]["MONTH"].ToString().Trim();
                foreach (Control contrl in Panel1.Controls)
                {
                    if (contrl is TextBox)
                    {
                        string str = ((TextBox)contrl).ID.ToString();
                        ((TextBox)contrl).Text = dtbind.Rows[0][str].ToString();
                    }
                } 
            }
        }



        //绑定年月
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }



        //是否存在
        private bool IsExist(string nian,string yue)
        {
            string sqlText = "select * from FM_JJZBTJ where YEAR='" + nian.ToString().Trim() + "' and MONTH='" + yue.ToString().Trim() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                dr.Close();
                return true;
            }
            dr.Close();
            return false;
        }


        //年月改变
        protected void OnSelectedIndexChanged_dplYearMoth(object sender,EventArgs e)
        {
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                string sqltext = "select LRFP_JLR from TBFM_LRFP where LRFP_TYPE='本年累计数' and RQBH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    JLR_YWC.Text = dt.Rows[0]["LRFP_JLR"].ToString().Trim();
                }
                else
                {
                    JLR_YWC.Text = "";
                }
            }
        }

        //确定
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择年月!')</script>");
                return;
            }
            action = Request.QueryString["action"].ToString().Trim();
            if (action == "add")
            {
                string addText;
                string addcol = string.Empty;
                string addvalue = string.Empty;
                string nian = dplYear.SelectedValue.ToString().Trim();
                string yue = dplMoth.SelectedValue.ToString().Trim();
                List<string> listadd = new List<string>();
                Dictionary<string, object> dicadd = new Dictionary<string, object>();//用于存储输入的数据
                if (IsExist(nian, yue))
                {
                    Response.Write("<script>alert('年月已存在，请重新选择！')</script>");
                }
                else
                {
                    foreach (Control contrl in Panel1.Controls)
                    {
                        if (contrl is TextBox)
                        {
                            string str = ((TextBox)contrl).ID.ToString();
                            dicadd.Add(str, ((TextBox)contrl).Text);
                        }
                    }

                    foreach (KeyValuePair<string, object> pair in dicadd)
                    {
                        addcol += pair.Key.ToString() + ",";
                        addvalue += "'" + pair.Value + "',";
                    }
                    addcol += "YEAR,MONTH";
                    addvalue += "'" + nian + "','" + yue + "'";
                    addText = string.Format("insert into FM_JJZBTJ({0}) values({1})", addcol, addvalue);
                    listadd.Add(addText);


                    DBCallCommon.ExecuteTrans(listadd);
                    Response.Write("<script>alert('保存成功！')</script>");
                    Response.Write("<script>window.close()</script>");
                }
            }
            else if (action == "update")
            {
                string id = Request.QueryString["id"].ToString();
                string sqlupdate = string.Empty;
                List<string> list = new List<string>();
                foreach (Control contrl in Panel1.Controls)
                {
                    if (contrl is TextBox)
                    {
                        string str = ((TextBox)contrl).ID.ToString();
                        sqlupdate = string.Format("update FM_JJZBTJ set {0}='{1}' where ID='{2}'", str, ((TextBox)contrl).Text, id);
                        string sqldate = "update FM_JJZBTJ set YEAR='" + dplYear.SelectedValue.ToString().Trim() + "',MONTH='" + dplMoth.SelectedValue.ToString().Trim() + "' where ID='" + id + "'";
                        list.Add(sqlupdate);
                        list.Add(sqldate);
                    }
                }
                DBCallCommon.ExecuteTrans(list);
                Response.Write("<script>alert('修改成功！')</script>");
                Response.Write("<script>window.close()</script>");
            }
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>"); 
        }
    }
}
