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
    public partial class PC_Date_chaifen : System.Web.UI.Page
    {
        public string gloabptc
        {
            get
            {
                object str = ViewState["gloabptc"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptc"] = value;
            }
        }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initpagemess();
                purchaseplan_Repeater_Bind();
            }
        }

        private void initpagemess()
        {
            if (Request.QueryString["ptcode"] != null)
            {
                gloabptc = Request.QueryString["ptcode"].ToString();
            }
            else
            {
                gloabptc = "";
            }
            if (Request.QueryString["shape"] != null)
            {
                gloabshape = Request.QueryString["shape"].ToString();
            }
            else
            {
                gloabshape = "";
            }
            if (Request.QueryString["mpid"] != null)
            {
                gloabpcode = Request.QueryString["mpid"].ToString();
            }
            else
            {
                gloabpcode = "";
            }

        }

        private void purchaseplan_Repeater_Bind()
        {
            string sqltext = "";
            sqltext = "SELECT  ptcode,marid, marnm, margg, marcz, margb, marunit,marfzunit,length,width,num,fznum, " +
                       "rpnum, rpfznum,isnull(purstate,0) as purstate, purnote,PUR_MASHAPE,PUR_CSTATE,PUR_ZYDY,PUR_TUHAO  " +
                       "FROM  View_TBPC_PURCHASEPLAN_IRQ_ORDER   where ptcode='" + gloabptc + "' and PUR_CSTATE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            purchaseplan_Repeater.DataSource = dt;
            purchaseplan_Repeater.DataBind();
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            int temp = numtrue();
            if (temp == 0)
            {
                string sql3 = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE like '%" + gloabptc + "%'";
                System.Data.DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql3);
                string ptc = gloabptc + "&" + dt3.Rows.Count;
                    string sql2 = "select * from TBPC_IQRCMPPRICE where PIC_PTCODE='" + gloabptc + "'";//已经下推比价单无法进行拆分
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql2);
                    string sql1 = "select * from TBPC_PURORDERDETAIL where PO_PTCODE='" + gloabptc + "'";//已经下推订单无法进行拆分
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                    if (dt.Rows.Count > 0 || dt1.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经下推比价单或者订单，无法进行拆分！');", true);
                    }
                    else
                    {
                        foreach (RepeaterItem Reitem in purchaseplan_Repeater.Items)
                        {
                            double rpnum = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_RPNUM")).Text.ToString() == "" ? "0" : ((TextBox)Reitem.FindControl("PUR_RPNUM")).Text.ToString());
                            double rpfznum = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_RPFZNUM")).Text.ToString() == "" ? "0" : ((TextBox)Reitem.FindControl("PUR_RPFZNUM")).Text.ToString());
                            double ptnum = Convert.ToDouble(((Label)Reitem.FindControl("PUR_NUM")).Text);
                            double ptfznum = Convert.ToDouble(((Label)Reitem.FindControl("PUR_FZNUM")).Text);
                            double num = ptnum - rpnum;//占用数量
                            double fznum = ptfznum - rpfznum;//占用辅助数量
                            string purzydy = ((TextBox)Reitem.FindControl("PUR_ZYDY")).Text.ToString();
                            if (purzydy != "")
                            {
                                string sql = "delete from TBPC_PURCHASEPLAN where PUR_PTCODE='" + gloabptc + "' and PUR_CSTATE='1'";
                                sqltextlist.Add(sql);
                                string marid = ((Label)Reitem.FindControl("PUR_MARID")).Text;
                                string submarid = marid.Substring(0, 5).ToString();
                                if (submarid == "01.07")
                                {
                                    string sqltext = "insert into TBPC_PURCHASEPLAN (PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_MASHAPE,PUR_NUM,PUR_FZNUM,PUR_RPNUM,PUR_RPFZNUM,PUR_TIMEQ,PUR_PTASMAN,PUR_PTASTIME,PUR_CGMAN,PUR_KEYCOMS,PUR_STATE,PUR_CSTATE,PUR_NOTE,PUR_ZYDY,PUR_TUHAO,PUR_TECUNIT)" +
                                                     "select PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_MASHAPE,PUR_FZNUM as PUR_NUM,PUR_NUM*1000 as PUR_FZNUM,PUR_RPFZNUM as PUR_RPNUM,PUR_RPNUM*1000 as PUR_RPFZNUM,PUR_TIMEQ,PUR_PTASMAN,PUR_PTASTIME,PUR_CGMAN,PUR_KEYCOMS,PUR_STATE,PUR_CSTATE='1',PUR_NOTE,PUR_ZYDY='" + purzydy + "',PUR_TUHAO,PUR_TECUNIT  from TBPC_PURCHASEPLAN where PUR_PTCODE='" + gloabptc + "'";
                                    sqltextlist.Add(sqltext);
                                }
                                else
                                {
                                    string sqltext2 = "insert into TBPC_PURCHASEPLAN (PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_MASHAPE,PUR_NUM,PUR_FZNUM,PUR_RPNUM,PUR_RPFZNUM,PUR_TIMEQ,PUR_PTASMAN,PUR_PTASTIME,PUR_CGMAN,PUR_KEYCOMS,PUR_STATE,PUR_CSTATE,PUR_NOTE,PUR_ZYDY,PUR_TUHAO,PUR_TECUNIT)" +
                                                      "select PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_MASHAPE,PUR_NUM,PUR_FZNUM,PUR_RPNUM,PUR_RPFZNUM,PUR_TIMEQ,PUR_PTASMAN,PUR_PTASTIME,PUR_CGMAN,PUR_KEYCOMS,PUR_STATE,PUR_CSTATE='1',PUR_NOTE,PUR_ZYDY='" + purzydy + "',PUR_TUHAO,PUR_TECUNIT  from TBPC_PURCHASEPLAN where PUR_PTCODE='" + gloabptc + "'";
                                    sqltextlist.Add(sqltext2);
                                }
                                string sqltext1 = "update TBPC_PURCHASEPLAN set PUR_NUM=" + rpnum + ",PUR_FZNUM=" + rpfznum + ",PUR_RPNUM=" + rpnum + ",PUR_RPFZNUM=" + rpfznum + "  where PUR_PTCODE='" + gloabptc + "' and PUR_CSTATE='0'";
                                string sqltext3 = "update TBPC_PURCHASEPLAN set PUR_PTCODE='"+ptc+"',PUR_NUM=" + num + ",PUR_FZNUM=" + fznum + ",PUR_RPNUM=" + num + ",PUR_RPFZNUM=" + fznum + ",PUR_ZYDY='" + purzydy + "'  where PUR_PTCODE='" + gloabptc + "' and PUR_CSTATE='1'";
                                sqltextlist.Add(sqltext1);
                                sqltextlist.Add(sqltext3);
                                DBCallCommon.ExecuteTrans(sqltextlist);
                                Response.Write("<script>javascript:window.close()</script>");
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拆分备注！');", true);
                            }
                        }
                    
                }
               
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购数量超出计划数量！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购数量等于计划数量，没必要拆分！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购数量为0，可直接行关闭！');", true);
            }
        }

        private int numtrue()
        {
            int temp = 0;
            foreach (RepeaterItem Reitem in purchaseplan_Repeater.Items)
            {
                double num = Convert.ToDouble(((Label)Reitem.FindControl("PUR_NUM")).Text.ToString());
                double fznum = Convert.ToDouble(((Label)Reitem.FindControl("PUR_FZNUM")).Text.ToString());
                double rpnum = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_RPNUM")).Text.ToString() == "" ? "0" : ((TextBox)Reitem.FindControl("PUR_RPNUM")).Text.ToString());
                double rpfznum = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_RPFZNUM")).Text.ToString() == "" ? "0" : ((TextBox)Reitem.FindControl("PUR_RPFZNUM")).Text.ToString());
                if (rpnum > num || rpfznum > fznum)//超出需用计划量
                {
                    temp = 1;
                }
                else if (rpnum == num && rpfznum == fznum)//等于需用计划量，不用拆分
                {
                    temp = 2;
                }
                else if (rpnum==0)
                {
                    temp = 3;
                }
                else
                { 
                        temp = 0;
                   
                    
                }
            }
            return temp;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in purchaseplan_Repeater.Items)
            {
                if (Reitem.ItemType != ListItemType.Header && Reitem.ItemType != ListItemType.Footer)
                {
                    double num = Convert.ToDouble(((Label)Reitem.FindControl("PUR_NUM")).Text.ToString());
                    double fznum = Convert.ToDouble(((Label)Reitem.FindControl("PUR_FZNUM")).Text.ToString());
                    double rpnum = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_RPNUM")).Text.ToString() == "" ? "0" : ((TextBox)Reitem.FindControl("PUR_RPNUM")).Text.ToString());

                   TextBox box1= (TextBox)Reitem.FindControl("PUR_RPFZNUM") as TextBox;
                   box1.Text = (fznum * rpnum / num).ToString("0.0000");
                }
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in purchaseplan_Repeater.Items)
            {
                if (Reitem.ItemType != ListItemType.Header && Reitem.ItemType != ListItemType.Footer)
                {
                    double num = Convert.ToDouble(((Label)Reitem.FindControl("PUR_NUM")).Text.ToString());
                    double fznum = Convert.ToDouble(((Label)Reitem.FindControl("PUR_FZNUM")).Text.ToString());

                    double rpfznum = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_RPFZNUM")).Text.ToString() == "" ? "0" : ((TextBox)Reitem.FindControl("PUR_RPFZNUM")).Text.ToString());
                    TextBox box2= ((TextBox)Reitem.FindControl("PUR_RPNUM")) as TextBox;
                    box2.Text = (num * rpfznum / fznum).ToString("0.0000");
                }
            }
        }
        protected void purchaseplan_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close()</script>");
        }

        protected void btn_marclose_Click(object sender, EventArgs e)
        {
            int j = 0;
            foreach (RepeaterItem Reitem in purchaseplan_Repeater.Items)
            {
                string purzydy = ((TextBox)Reitem.FindControl("PUR_ZYDY")).Text.ToString();
                if (purzydy != "")
                {
                    string sqlt = "update TBPC_PURCHASEPLAN set PUR_CSTATE='1',PUR_ZYDY='" + purzydy + "'  where PUR_PTCODE='" + gloabptc + "'";//行关闭
                    DBCallCommon.ExeSqlText(sqlt);

                    string sql1 = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + gloabpcode + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string cstate = dt.Rows[i]["PUR_CSTATE"].ToString();
                            if (cstate == "0")
                            {
                                j++;
                            }
                        }
                    }
                    if (j == 0)
                    {
                        string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='5' where PR_SHEETNO='" + gloabpcode + "'";
                        DBCallCommon.ExeSqlText(sql2);
                        string sql3 = "update TBPC_PURCHASEPLAN set PUR_STATE='3' where PUR_PCODE='" + gloabpcode + "'";
                        DBCallCommon.ExeSqlText(sql3);
                    }
                    Response.Write("<script>javascript:window.close()</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请在占用代用备注列填写行关闭理由！');", true);
                }
            }
        }
    }
}
