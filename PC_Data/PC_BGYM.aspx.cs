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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_BGYM : System.Web.UI.Page
    {
        string ph = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ph = Request.QueryString["bgph"];
            if (!IsPostBack)
            {
                string sqltext = "select planno,Aptcode,PUR_MASHAPE,marid,marnm,margg,marcz,margb,num,marunit,fznum,marfzunit,sqrnm,sqrtime,num as BG_NUM,fznum as BG_FZNUM,BG_NOTE from ((select * from View_TBPC_PLAN_PLACE)a left join (select * from TBPC_BG)b on a.Aptcode=b.BG_PTC) where BG_XTZT='1' and BG_PH='" + ph + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                rptProNumCost.DataSource = dt;
                rptProNumCost.DataBind();
                string sql = "update TBPC_BG set BG_XTZT='0' where BG_XTZT='1'";
                DBCallCommon.ExeSqlText(sql);
            }
        }


        //保存
        protected void btn_save_click(object sender, EventArgs e)
        {
            string bgdate = DateTime.Now.ToString("yyyy-MM-dd");
            List<string> list0 = new List<string>();
            for (int t = 0; t < rptProNumCost.Items.Count; t++)
            {
                TextBox bgnum = (TextBox)rptProNumCost.Items[t].FindControl("BG_NUM");
                TextBox bgfznum = (TextBox)rptProNumCost.Items[t].FindControl("BG_FZNUM");
                Label num = (Label)rptProNumCost.Items[t].FindControl("num");
                Label fznum = (Label)rptProNumCost.Items[t].FindControl("fznum");
                if(bgnum.Text.ToString().Trim()==""||bgfznum.Text.ToString().Trim()=="")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不能为空!')", true);
                    return;
                }
                if (Convert.ToDouble(bgnum.Text.ToString().Trim())<0||Convert.ToDouble(bgfznum.Text.ToString().Trim())<0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请不要填写负数!')", true);
                    return;
                }
                if (Convert.ToDouble(bgnum.Text.ToString().Trim()) > Convert.ToDouble(num.Text.ToString().Trim()) || Convert.ToDouble(bgfznum.Text.ToString().Trim()) > Convert.ToDouble(fznum.Text.ToString().Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更数量不能大于计划数量!')", true);
                    return;
                }
            }
            for (int r = 0; r < rptProNumCost.Items.Count; r++)
            {
                TextBox bgnum = (TextBox)rptProNumCost.Items[r].FindControl("BG_NUM");
                TextBox bgfznum = (TextBox)rptProNumCost.Items[r].FindControl("BG_FZNUM");
                TextBox bgnote=(TextBox)rptProNumCost.Items[r].FindControl("BG_NOTE");
                string ptcsave = (rptProNumCost.Items[r].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                string sqlsave = "update TBPC_BG set BG_NUM='" + Convert.ToDouble(bgnum.Text.ToString().Trim()) + "',BG_FZNUM='" + Convert.ToDouble(bgfznum.Text.ToString().Trim()) + "',BG_DATE='" + bgdate + "',BG_NOTE='" + bgnote.Text.ToString() + "' where BG_PTC='" + ptcsave + "' and BG_STATE='0' and BG_PH='" + ph + "'";
                list0.Add(sqlsave);
            }
            DBCallCommon.ExecuteTrans(list0);
            binddata();
            Button1.Visible = true;
        }


        //提交 BG_STATE:1为已提交，2为一级审核通过，3为二级审核通过，4为三级审核通过，5为驳回
        protected void btn_tj_Click(object sender, EventArgs e)
        {
            List<string> listtj = new List<string>();
            for (int r = 0; r < rptProNumCost.Items.Count; r++)
            {
                string ptctj = (rptProNumCost.Items[r].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                string sqltj = "update TBPC_BG set BG_STATE='1' where BG_PTC='" + ptctj + "' and BG_PH='" + ph + "'";
                listtj.Add(sqltj);
            }
            DBCallCommon.ExecuteTrans(listtj);

            //邮件提醒
            string sprid = "";
            string sptitle = "";
            string spcontent = "";
            sptitle = "物料减少审批";
            spcontent = "有物料减少需要您审批，请登录查看！";
            string sqlgetshraname = "select BG_SHRA from TBPC_BG where BG_PH='" + ph + "'";
            System.Data.DataTable dtgetshraname = DBCallCommon.GetDTUsingSqlText(sqlgetshraname);
            if (dtgetshraname.Rows.Count > 0)
            {
                string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + dtgetshraname.Rows[0]["BG_SHRA"].ToString().Trim() + "'";
                System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                if (dtgetstid.Rows.Count > 0)
                {
                    sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                }
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
            }
            Response.Redirect("PC_TBPC_Purchaseplan_start.aspx");
        }


        //删除
        protected void btn_delete_click(object sender, EventArgs e)
        {
            List<string> listsc = new List<string>();
            for (int r = 0; r < rptProNumCost.Items.Count; r++)
            {
                if ((rptProNumCost.Items[r].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptcdelete = (rptProNumCost.Items[r].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                    string sqldelete = "delete from TBPC_BG where BG_PTC='" + ptcdelete + "' and BG_PH='" + ph + "'";
                    listsc.Add(sqldelete);
                }
            }
            DBCallCommon.ExecuteTrans(listsc);
            binddata();
        }


        private void binddata()
        {
            string sqltextbd = "select * from ((select * from View_TBPC_PLAN_PLACE)a left join (select * from TBPC_BG)b on a.Aptcode=b.BG_PTC) where BG_PH='" + ph + "'";
            DataTable dtbd = DBCallCommon.GetDTUsingSqlText(sqltextbd);
            rptProNumCost.DataSource = dtbd;
            rptProNumCost.DataBind();
        }
    }
}
