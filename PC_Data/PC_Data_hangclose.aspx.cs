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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Data_hangclose : System.Web.UI.Page
    {
        public string gloabpcode
        {
            get
            {
                object str = ViewState["gloabpcode"];
                return str == null ? null : (string)str;
            }

            set
            {
                ViewState["gloabpcode"] = value;
            }
        }
        public string gloabshape
        {
            get
            {
                object str = ViewState["gloabshape"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabshape"] = value;
            }
        }
        public string gloabnum
        {
            get
            {
                object str = ViewState["gloabnum"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabnum"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initpagemess();
                purchaseplan_close_RepeaterBind();
            }
        }

        private void initpagemess()
        {
            if (Request.QueryString["shape"] != null)
            {
                gloabshape =Server.HtmlDecode(Request.QueryString["shape"].ToString());
            }
            else
            {
                gloabshape = "";
            }
            if (Request.QueryString["orderno"] != null)
            {
                gloabpcode = Request.QueryString["orderno"].ToString();
            }
            else
            {
                gloabpcode = "";
            }
            if (Request.QueryString["num"] != null)
            {
                gloabnum = Request.QueryString["num"].ToString();
            }
            else
            {
                gloabnum = "";
            }

        }

        private void purchaseplan_close_RepeaterBind()
        {
            string[] Arry;
            Arry = Request.QueryString["arry"].Split(',');
            string sqltext = "";
            sqltext = "SELECT  PUR_ID,ptcode,marid, marnm, margg, marcz, margb, marunit,marfzunit,length,width,num,fznum, " +
                      "rpnum, rpfznum,isnull(purstate,0) as purstate, purnote,PUR_MASHAPE,PUR_CSTATE,PUR_ZYDY,PUR_TUHAO  " +
                      "FROM  View_TBPC_PURCHASEPLAN_IRQ_ORDER   where  PUR_CSTATE='0' and PUR_ID in (";
            
            for (int i = 0; i < Arry.Length - 1; i++)
            {
                sqltext += "'" + Arry[i].ToString() + "',";
            }

            sqltext += "'" + Arry[Arry.Length - 1].ToString() + "')";
            sqltext += " order by ptcode asc ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            purchaseplan_close_Repeater.DataSource = dt;
            purchaseplan_close_Repeater.DataBind();
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            //Response.Write("<script>javascript:window.returnValue=\"Refesh\";window.close();</script>");
            if (gloabnum == "1")
            {
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + gloabshape + "&mp_id=" + gloabpcode + "");
            }
            else if (gloabnum == "2")
            {
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_assign.aspx");
            }
            
        }
        
        /// <summary>
        /// 正常关闭，占用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btn_marclose_Click(object sender, EventArgs e)
        //{
        //    //int j = 0;
        //    foreach (RepeaterItem Reitem in purchaseplan_close_Repeater.Items)
        //    {
        //        string purzydy = ((TextBox)Reitem.FindControl("PUR_ZYDY")).Text.Trim().ToString();
        //        string ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text.ToString();
        //        if (purzydy != "")
        //        {
        //            string sqlt1 = "update TBPC_PURCHASEPLAN set PUR_CSTATE='1',PUR_ZYDY='" + purzydy + "'  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";//行关闭
        //            DBCallCommon.ExeSqlText(sqlt1);

        //            string sql1 = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + gloabpcode + "' and PUR_CSTATE='0'";
        //            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
        //            if (dt.Rows.Count == 0)
        //            {
        //                string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='5' where PR_SHEETNO='" + gloabpcode + "'";
        //                DBCallCommon.ExeSqlText(sql2);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请在行关闭备注列填写行关闭理由！');", true);
        //            return;
        //        }
        //    }
        //    Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + gloabshape + "&mp_id=" + gloabpcode + "");
        //}
        //占用关闭
        protected void btn_marclose_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            foreach (RepeaterItem Reitem1 in purchaseplan_close_Repeater.Items)
            {
                string purzydy = "占用库存" + "  " + ((TextBox)Reitem1.FindControl("PUR_ZYDY")).Text.ToString();
                string ptcode = ((Label)Reitem1.FindControl("PUR_PTCODE")).Text.ToString();

                if (purzydy != "")
                {
                    string sqlt1 = "update TBPC_PURCHASEPLAN set PUR_CSTATE='1',PUR_ZYDY='" + purzydy + "',Pur_ClosePer='" + Session["UserName"].ToString().Trim() + "',Pur_ColseTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Pue_Closetype='2'  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";//行关闭
                    sqllist.Add(sqlt1);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请在行关闭备注列填写行关闭理由！');", true);
                    return;
                }
            }
            DBCallCommon.ExecuteTrans(sqllist);
            string sql1 = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + gloabpcode + "' and PUR_CSTATE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt.Rows.Count == 0)
            {
                string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='5' where PR_SHEETNO='" + gloabpcode + "'";
                DBCallCommon.ExeSqlText(sql2);
            }

            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + gloabshape + "&mp_id=" + gloabpcode + "");
        }



        //代用关闭
        protected void btn_dyclose_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            foreach (RepeaterItem Reitem1 in purchaseplan_close_Repeater.Items)
            {
                string purzydy = "相似占用" + "  " + ((TextBox)Reitem1.FindControl("PUR_ZYDY")).Text.ToString();
                //string purdy = ((TextBox)Reitem1.FindControl("PUR_ZYDY")).Text.ToString();
                string ptcode = ((Label)Reitem1.FindControl("PUR_PTCODE")).Text.ToString();

                if (purzydy != "")
                {
                    string sqlt1 = "update TBPC_PURCHASEPLAN set PUR_CSTATE='1',PUR_ZYDY='" + purzydy + "',Pur_ClosePer='" + Session["UserName"].ToString().Trim() + "',Pur_ColseTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Pue_Closetype='1'  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";//行关闭
                    sqllist.Add(sqlt1);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请在行关闭备注列填写相似占用规格！');", true);
                    return;
                }
            }
            DBCallCommon.ExecuteTrans(sqllist);
            string sql1 = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + gloabpcode + "' and PUR_CSTATE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt.Rows.Count == 0)
            {
                string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='5' where PR_SHEETNO='" + gloabpcode + "'";
                DBCallCommon.ExeSqlText(sql2);
            }

            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + gloabshape + "&mp_id=" + gloabpcode + "");
        }








        

        protected void purchaseplan_close_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell FZnum1 = (HtmlTableCell)e.Item.FindControl("fznum1");
                HtmlTableCell FZunit1 = (HtmlTableCell)e.Item.FindControl("fzunit1");
                HtmlTableCell RPfznum1 = (HtmlTableCell)e.Item.FindControl("rpfznum1");
                if (gloabshape == "非定尺板" || gloabshape == "标(发运)" || gloabshape == "标(组装)" || gloabshape == "标(工艺)")
                {
                    FZnum1.Visible = false;
                    FZunit1.Visible = false;
                    RPfznum1.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableCell FZnum2 = (HtmlTableCell)e.Item.FindControl("fznum2");
                HtmlTableCell FZunit2 = (HtmlTableCell)e.Item.FindControl("fzunit2");
                HtmlTableCell RPfznum2 = (HtmlTableCell)e.Item.FindControl("rpfznum2");
                if (gloabshape == "非定尺板" || gloabshape == "标(发运)" || gloabshape == "标(组装)" || gloabshape == "标(工艺)")
                {
                    FZnum2.Visible = false;
                    FZunit2.Visible = false;
                    RPfznum2.Visible = false;
                }
            }
        }

        /// <summary>
        /// 意外关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btn_ywclose_Click(object sender, EventArgs e)
        //{
        //    //int j = 0;
        //    foreach (RepeaterItem Reitem in purchaseplan_close_Repeater.Items)
        //    {
        //        string purzydy = ((TextBox)Reitem.FindControl("PUR_ZYDY")).Text.Trim().ToString();
        //        string ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text.ToString();
        //        if (purzydy != "")
        //        {
        //            string sqlt2 = "update TBPC_PURCHASEPLAN set PUR_CSTATE='2',PUR_ZYDY='" + purzydy + "'  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";//行关闭
        //            DBCallCommon.ExeSqlText(sqlt2);

        //            string sql1 = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + gloabpcode + "' and PUR_CSTATE='0'";
        //            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
        //            if (dt.Rows.Count == 0)
        //            {
        //                string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='5' where PR_SHEETNO='" + gloabpcode + "'";
        //                DBCallCommon.ExeSqlText(sql2);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请在行关闭备注列填写行关闭理由！');", true);
        //            return;
        //        }
        //    }
        //    Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + gloabshape + "&mp_id=" + gloabpcode + "");
        //}
        protected void btn_ywclose_Click(object sender, EventArgs e)
        {
            //List<string> sqllist = new List<string>();
            //foreach (Control c in purchaseplan_close_Repeater.Controls)
            //{
            //    Label lblpurzydy = c.FindControl("PUR_ZYDY") as Label;
            //    string purzydy = "";
            //    if (lblpurzydy != null)
            //    {
            //        purzydy = lblpurzydy.Text;   //备注
            //    }

            //    Label lblptcode = c.FindControl("PUR_PTCODE") as Label;
            //    string ptcode = "";
            //    if (lblptcode != null)
            //    {
            //        ptcode = lblptcode.Text;   //计划跟踪号
            //    }
            List<string> sqllist = new List<string>();
            foreach (RepeaterItem Reitem2 in purchaseplan_close_Repeater.Items)
            {
                string purzydy = ((TextBox)Reitem2.FindControl("PUR_ZYDY")).Text.Trim().ToString();
                string ptcode = ((Label)Reitem2.FindControl("PUR_PTCODE")).Text.ToString();
                                              
                if (purzydy != "")
                {
                    string sqlt2 = "update TBPC_PURCHASEPLAN set PUR_CSTATE='2',PUR_ZYDY='" + purzydy + "',Pur_ClosePer='" + Session["UserName"].ToString().Trim() + "',Pur_ColseTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Pue_Closetype='0' where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";//行关闭
                    sqllist.Add(sqlt2);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请在行关闭备注列填写行关闭理由！');", true);
                    return;
                }
            }
            DBCallCommon.ExecuteTrans(sqllist);
            string sql1 = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + gloabpcode + "' and PUR_CSTATE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt.Rows.Count == 0)
            {
                string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='5' where PR_SHEETNO='" + gloabpcode + "'";
                DBCallCommon.ExeSqlText(sql2);
            }

            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + gloabshape + "&mp_id=" + gloabpcode + "");
        }


    }
}
