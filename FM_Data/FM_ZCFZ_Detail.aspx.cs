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
    public partial class FM_ZCFZ_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] == "ADD")
            {
                btnConfirm.Text = "添加";
                this.Title = "添加资产负债信息";
            }
            else if (Request.QueryString["action"] == "look")
            {
                this.Title = "资产负债信息查看";
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
                this.Title = "修改资产负债信息";
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
                string sqlTextc = "select * from TBFM_ZCFZ where RQBH='" + cx_id + "' and ZCFZ_TYPE='年初数'";
                string sqlTextm = "select * from TBFM_ZCFZ where RQBH='" + cx_id + "' and ZCFZ_TYPE='期末数'";
                DataTable dtc = DBCallCommon.GetDTUsingSqlText(sqlTextc);
                DataRow drc = dtc.Rows[0];
                DataTable dtm = DBCallCommon.GetDTUsingSqlText(sqlTextm);
                DataRow drm = dtm.Rows[0];
                RQBH.Text=drc["RQBH"].ToString();
                foreach (Control contrl in Panel1.Controls)
                {
                    if (contrl is TextBox)
                    {
                        string str = ((TextBox)contrl).ID.ToString();
                       if(str.Substring(str.Length-1,1)=="1")
                       {
                           ((TextBox)contrl).Text = drc[str.Substring(0,str.Length-1)].ToString();
                       }
                       else if(str.Substring(str.Length-1,1)=="2")
                       {
                           ((TextBox)contrl).Text = drm[str.Substring(0,str.Length-1)].ToString();
                       }
                    }
                } 
        }
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            DateShow();
        }
        //确认
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (btnConfirm.Text == "修改")
            {
                #region 更新代码
                string zc_id = Request.QueryString["id"].ToString();
                string sql = string.Empty;
                List<string> list = new List<string>();
                foreach (Control contrl in Panel1.Controls)
                {
                    if (contrl is TextBox)
                    {
                        string str = ((TextBox)contrl).ID.ToString();
                        if (str.Substring(str.Length-1, 1) == "1")
                        {
                            sql = string.Format("update TBFM_ZCFZ set {0}='{1}' where RQBH='{2}' and ZCFZ_TYPE='年初数'", str.Substring(0, str.Length - 1), ((TextBox)contrl).Text, zc_id);
                            list.Add(sql);
                        }
                        else if (str.Substring(str.Length-1, 1) == "2")
                        {
                            sql = string.Format("update TBFM_ZCFZ set {0}='{1}' where RQBH='{2}' and ZCFZ_TYPE='期末数'", str.Substring(0, str.Length - 1), ((TextBox)contrl).Text, zc_id);
                            list.Add(sql);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(list);
                Response.Write("<script>alert('修改成功！')</script>");
                Response.Write("<script>window.close()</script>");
                #endregion
            }
            if (btnConfirm.Text == "添加")
            {
                #region 添加基本数据
                string addTextc;
                string addTextm;
                string ncs = "年初数";
                string qms = "期末数";
                string rqbh = RQBH.Text.ToString();
                string addcolc = string.Empty;
                string addvaluec = string.Empty;
                string addcolm = string.Empty;
                string addvaluem = string.Empty;
                List<string> listc = new List<string>();
                List<string> listm = new List<string>();
                Dictionary<string, object> dicc = new Dictionary<string, object>();
                Dictionary<string, object> dicm = new Dictionary<string, object>();//用于存储输入的数据
                if (IsExist(rqbh))
                {
                    Response.Write("<script>alert('日期编号重复，请重新输入！')</script>");
                }
                else
                {
                    foreach (Control contrl in Panel1.Controls)
                    {
                        if (contrl is TextBox)
                        {
                            string str = ((TextBox)contrl).ID.ToString();
                            if (str.Substring(str.Length - 1, 1) == "1")
                            {
                                dicc.Add(str.Substring(0, str.Length - 1), ((TextBox)contrl).Text);
                            }
                            else if (str.Substring(str.Length - 1, 1) == "2")
                            {
                                dicm.Add(str.Substring(0, str.Length - 1), ((TextBox)contrl).Text);
                            }
                        }
                    }

                    foreach (KeyValuePair<string, object> pair in dicc)
                    {
                        addcolc += pair.Key.ToString() + ",";
                        addvaluec += "'" + pair.Value + "',";
                    }
                    addcolc += "ZCFZ_TYPE,RQBH";
                    addvaluec += "'" + ncs + "','" + rqbh + "'";
                    addTextc = string.Format("insert into TBFM_ZCFZ({0}) values({1})", addcolc, addvaluec);
                    listc.Add(addTextc);
                    foreach (KeyValuePair<string, object> pair in dicm)
                    {
                        addcolm += pair.Key.ToString() + ",";
                        addvaluem += "'" + pair.Value + "',";
                    }
                    addcolm += "ZCFZ_TYPE,RQBH";
                    addvaluem += "'" + qms + "','" + rqbh + "'";
                    addTextm = string.Format("insert into TBFM_ZCFZ({0}) values({1})", addcolm, addvaluem);
                    listm.Add(addTextm);

                #endregion
                    DBCallCommon.ExecuteTrans(listc);
                    DBCallCommon.ExecuteTrans(listm);
                    Response.Write("<script>alert('添加成功！')</script>");
                    Response.Write("<script>window.close()</script>");
                }

            }
        }
        private bool IsExist(string zccode)
        {
            string sqlText = "select * from TBFM_ZCFZ where RQBH='" + zccode + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                dr.Close();
                return true;
            }
            dr.Close();
            return false;
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");

        }
    }
}
