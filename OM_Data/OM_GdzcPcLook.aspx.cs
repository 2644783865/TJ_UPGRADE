using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GdzcPcLook : System.Web.UI.Page
    {
        string sqltext = "";
        string action = string.Empty;
        string id = string.Empty;
        double price = 0;
        string state = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"].ToString();
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                if (action == "look")
                {
                    this.GetDataByID(id);
                    //Tab_spxx.Visible = false;
                    txtshr.Visible = false;
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("txtUprice")).Visible = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtTprice")).Visible = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtUprice")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("txtTprice")).Enabled = false;
                    }
                }
                if (action == "review")
                {
                    this.GetDataByID(id);
                    lbltitle1.Visible = false;
                    lbltitle2.Visible = true;
                    btnLoad.Visible = true;
               
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("txtUprice")).Visible = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtTprice")).Visible = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtUprice")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("txtTprice")).Enabled = false;
                    }
                    txtshr.Visible = false;
                }
                if (action=="firstLook")
                {
                    btnSubmit.Visible = true;
                    lblshr.Visible = true;
                    txtshr.Visible = true;
                    hlSelect0.Visible = true;
                    lbltitle1.Visible = false;
                    lbltitle2.Visible = true;
                    this.GetDataByID(id);
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("txtUprice")).Visible = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtTprice")).Visible = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtUprice")).Enabled = true;
                        ((TextBox)GridView1.Rows[i].FindControl("txtTprice")).Enabled = true;
                    }
                }
                GVBind(AddGridViewFiles);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox tprice = (TextBox)e.Row.FindControl("txtTprice");
                if (tprice.Text != "")
                {
                    price += Convert.ToDouble(tprice.Text);
                }
                else
                {
                    price += 0;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label zje = (Label)e.Row.FindControl("lblzje");
                zje.Text = string.Format("{0:c2}", price);
            }
        }
        protected void GetDataByID(string id)
        {
            sqltext = "select DISTINCT(CODE) AS CODE,DEPARTMENT,LINKMAN,PHONE,REASON,NOTE,AGENT,AGENTID,ADDTIME,STATE,BC_CONTEXT from TBOM_GDZCPCAPPLY where CODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                lblCode.Text = dr["CODE"].ToString();
                lblDepartment.Text = dr["DEPARTMENT"].ToString();
                lblLinkman.Text = dr["LINKMAN"].ToString();
                lblPhone.Text = dr["PHONE"].ToString();
                lblReason.Text = dr["REASON"].ToString();
                lblNote.Text = dr["NOTE"].ToString();
                lblAgent.Text = dr["AGENT"].ToString();
                lblAgent_id.Text = dr["AGENTID"].ToString();
                lblAddtime.Text = dr["ADDTIME"].ToString();
                lbl_context.Text = dr["BC_CONTEXT"].ToString();
                
                if (dr["STATE"].ToString() != "0")
                {
                }
                //if(Session["UserID"].ToString()==lblAgent_id.Text)
                //{
                //    Tab_spxx.Visible = true;
                //}
            }
            dr.Close();
            sqltext = "select ID,NAME,MODEL,NUM,LOCATION,XQTIME,UPRICE,TPRICE from TBOM_GDZCPCAPPLY where CODE='" + id + "'";
            DBCallCommon.BindGridView(GridView1, sqltext);
            string sqltext1 = "select CARRVWA,CARRVWAID,CARRVWAADVC,CARRVWATIME,CARRVWB,CARRVWBID,CARRVWBADVC,CARRVWBTIME,CARRVWC,CARRVWCID,CARRVWCADVC,CARRVWCTIME,STATUS from TBOM_GDZCRVW where CODE='" + id + "'";
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqltext1);
            if (dr1.Read())
            {
                txt_first.Text = dr1["CARRVWA"].ToString();
                firstid.Value = dr1["CARRVWAID"].ToString();
                first_opinion.Text = dr1["CARRVWAADVC"].ToString();
                first_time.Text = dr1["CARRVWATIME"].ToString();
                txt_second.Text = dr1["CARRVWB"].ToString();
                secondid.Value = dr1["CARRVWBID"].ToString();
                second_opinion.Text = dr1["CARRVWBADVC"].ToString();
                second_time.Text = dr1["CARRVWBTIME"].ToString();
                lblState.Text = dr1["STATUS"].ToString();
                if (Session["UserID"].ToString() == firstid.Value || Session["UserID"].ToString() == secondid.Value || Session["UserID"].ToString() == lblAgent_id.Text)
                {
                    Tab_spxx.Visible = true;
                    //if (Session["UserID"].ToString() == firstid.Value || Session["UserID"].ToString() == secondid.Value)
                    //{
                    //    btnLoad.Visible = true;
                    //}
                }
                lbl_state.Text = lblState.Text.ToString();
                state = lblState.Text;
                switch (state)
                {
                    case "1":
                        if (Session["UserID"].ToString() == firstid.Value)
                        {
                            rblfirst.Enabled = true;
                            first_opinion.Enabled = true;
                            hlSelect2.Visible = true;
                            first_time.Text = DateTime.Now.ToString();
                        }

                        break;
                    case "2":
                        if (Session["UserID"].ToString() == secondid.Value)
                        {
                            hlSelect2.Visible = true;
                            rblsecond.Enabled = true;
                            second_opinion.Enabled = true;
                            rblsecond.SelectedIndex = 0;
                            second_time.Text = DateTime.Now.ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
            dr1.Close();
        }
  
        #region 删除文件、下载文件

        void GVBind(GridView gv)
        {
            string sql = "select * from tb_files where BC_CONTEXT='" + lbl_context.Text + "' and fileload like '%ZCZJ_DPF%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            gv.DataSource = dt;
            gv.DataBind();
            gv.DataKeyNames = new string[] { "fileID" };
        }

       
        protected void imgbtnDF_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            //Response.Write("gvr");
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            //获取文件真实姓名
            string sqlStr = "select fileload,fileName from tb_files where fileID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["fileload"].ToString() + @"\" + ds.Tables[0].Rows[0]["fileName"].ToString();
            //Response.Write(strFilePath);
            if (File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }

        #endregion

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string uprice;
            string tprice;
            string rowid;
            List<string> list_sql = new List<string>();
            if (price != '0')
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    uprice = ((TextBox)gr.FindControl("txtUprice")).Text.ToString();
                    tprice = ((TextBox)gr.FindControl("txtTprice")).Text.ToString();
                    rowid = ((Label)gr.FindControl("lblid")).Text.ToString();
                    sqltext = "update TBOM_GDZCPCAPPLY set [UPRICE]='" + uprice + "',[TPRICE]='" + tprice + "',[SHRID]='" + shrid.Value + "',[SHR]='" + txtshr.Text + "',[STATE]=1 where [ID]='" + rowid + "'";
                    list_sql.Add(sqltext);

                }
                sqltext = "update TBOM_GDZCRVW set CARRVWA='" + txtshr.Text.Trim() + "',CARRVWAID='" + shrid.Value.Trim() + "',STATUS='1' where CODE='" + lblCode.Text.Trim() + "'";
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                string _emailto = DBCallCommon.GetEmailAddressByUserID(firstid.Value);
                string _body = "固定资产采购审批任务:"
                      + "\r\n单号：" + lblCode.Text.ToString().Trim()
                      + "\r\n制单日期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string _subject = "您有新的【固定资产采购】需要审批，请及时处理:" + lblCode.Text.ToString().Trim();
                DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功,请等待审批！');window.location.href='OM_GdzcPcPlan.aspx'", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写金额！！！');", true);
            }
        }
        protected void btnLoad_OnClick(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            if (Request.QueryString["action"] == "review")
            {
                if (firstid.Value == Session["UserID"].ToString() && lbl_state.Text == "1")
                {

                    string sql = "update TBOM_GDZCRVW set CARRVWAADVC='" + first_opinion.Text.ToString() + "',CARRVWATIME='" + first_time.Text.ToString() + "',CARRVWB='" + txt_second.Text.ToString() + "',CARRVWBID='" + secondid.Value.Trim() + "',STATUS='" + rblfirst.SelectedValue.ToString() + "' WHERE CODE='" + lblCode.Text.Trim() + "'";
                    list_sql.Add(sql);
                    DBCallCommon.ExecuteTrans(list_sql);
                    if (rblfirst.SelectedValue.ToString() == "2")
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(secondid.Value);
                        string _body = "固定资产采购审批任务:"
                              + "\r\n单号：" + lblCode.Text.ToString().Trim()
                              + "\r\n制单日期：" + lblAddtime.Text.ToString().Trim();

                        string _subject = "您有新的【固定资产采购】需要审批，请及时处理:" + lblCode.Text.ToString().Trim();
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核成功，提交下一级！');window.location.href='OM_GdzcPcPlan.aspx'", true);
                }
                if (secondid.Value == Session["UserID"].ToString() && lbl_state.Text == "2")
                {

                    string sql = "update TBOM_GDZCRVW set CARRVWBADVC='" + second_opinion.Text.ToString() + "',CARRVWBTIME='" + second_time.Text.ToString() + "',STATUS='" + rblsecond.SelectedValue.ToString() + "' WHERE CODE='" + lblCode.Text.Trim() + "'";
                    list_sql.Add(sql);
                    if (rblsecond.SelectedValue == "4")
                    {
                        sql = "update TBOM_GDZCRVW set STATUS='6' WHERE CODE='" + lblCode.Text.Trim() + "'";
                        list_sql.Add(sql);
                    }
                    DBCallCommon.ExecuteTrans(list_sql);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核成功，申请通过！');window.location.href='OM_GdzcPcPlan.aspx'", true);
                }
               
                //Response.Redirect("OM_GdzcPcPlan.aspx");
            }
        }

        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_GdzcPcPlan.aspx");
        }
    }
}
