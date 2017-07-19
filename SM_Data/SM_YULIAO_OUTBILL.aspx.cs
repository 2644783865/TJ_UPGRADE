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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_YULIAO_OUTBILL : System.Web.UI.Page
    {
        string sqltext = "";
        string flag = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] != null)
                flag = Request.QueryString["FLAG"].ToString();
            if (!IsPostBack)
            {
                if (flag == "PUSH")
                {
                    GetCode();

                    initial();
                    lblOutDoc.Text = Session["UserName"].ToString();
                    lblOutDocID.Text = Session["UserID"].ToString();
                    lblOutDate.Text = DateTime.Now.ToString();
                }
            }
        }
        private void GetCode()
        {
            sqltext = "select TOP 1 dbo.GetCode(OUTCODE) AS TopIndex from TBWS_YULIAO_OUT ORDER BY dbo.GetCode(OUTCODE) DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            lblOutCode.Text = "YULIAOOUT" + code.PadLeft(4, '0');
        }

        protected void initial()
        {
            sqltext = "select a.Id,Marid,NUMBER,b.MNAME as Name,b.GUIGE,b.Caizhi,Length,Width,Weight,TUXING from TBWS_YULIAO_RESTORE as a left join TBMA_MATERIAL as b on a.Marid=b.ID where a.STATE='1'";
            DBCallCommon.BindGridView(GridView1, sqltext);
            sqltext = "update TBWS_YULIAO_RESTORE set STATE='0'";
            DBCallCommon.ExeSqlText(sqltext);
        }
        private void writedata()
        {
            string Marid;
            string Id;
            int numstore;
            string weightStore;
            string outWeight;
            int outnum;
            string outnote;
            string Length;
            string Width;
            string Tuxing;
            List<string> list_sql = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                Marid = ((Label)gr.FindControl("lblMarid")).Text;
                Id = ((HtmlInputHidden)gr.FindControl("hidId")).Value;
                numstore = Convert.ToInt32(((TextBox)gr.FindControl("txtstore")).Text);
                outWeight = ((TextBox)gr.FindControl("txtOutWeight")).Text;
                weightStore = ((Label)gr.FindControl("lblWeight")).Text;
                outnum = Convert.ToInt32(((TextBox)gr.FindControl("txtoutnum")).Text);
                outnote = ((TextBox)gr.FindControl("txtnote")).Text;
                Length = ((Label)gr.FindControl("Length")).Text; Width = ((Label)gr.FindControl("Width")).Text; Tuxing = ((Label)gr.FindControl("Tuxing")).Text;
                if (outnum == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有出库数据！！！');", true);
                    return;
                }
                else
                {
                    sqltext = "insert into TBWS_YULIAO_OUT(OUTCODE,Marid,OUTNUM,PJNAME,OUTPERID,OUTDATE,DOCUPERID,NOTE,Length, Width, Tuxing, Weight)";
                    sqltext += "values('" + lblOutCode.Text + "','" + Marid + "'," + outnum + ",'" + txtTaskId.Text.Trim() + "','" + hidGiverId.Value.ToString() + "','" + lblOutDate.Text + "','" + lblOutDocID.Text + "','" + outnote + "','" + Length + "','" + Width + "','" + Tuxing + "','" + outWeight + "')";
                    list_sql.Add(sqltext);
                    if (numstore == outnum)
                    {
                        sqltext = "delete from  TBWS_YULIAO_RESTORE  where Id='" + Id + "'";
                    }
                    else
                    {
                        Decimal weight = Convert.ToDecimal(weightStore) - Convert.ToDecimal(outWeight);
                        sqltext = "update TBWS_YULIAO_RESTORE set NUMBER=" + (numstore - outnum) + ",Weight='" + weight + "' where Id='" + Id + "'";

                    }


                    list_sql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(list_sql);
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string st = "OK";
            if (txtGiver.Text=="")
            {
                Response.Write("<script>alert('请选择领料人！');</script>");
                return;
            }
            if (txtTaskId.Text == "")
            {
                Response.Write("<script>alert('请输入任务号！');</script>");
                return;
            }
            if (GridView1.Rows.Count == 0)
            {
                st = "NoData";
            }
            if (st == "OK")
            {
                writedata();
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.location.href='SM_YULIAO_OUT.aspx'", true);
                Response.Write("<script>alert('保存成功！');window.location.href='SM_YULIAO_OUT.aspx'</script>");
            }
            else if (st == "NoData")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有出库数据！！！');", true);
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("SM_YULIAO_OUT.aspx");
        }


    }
}
