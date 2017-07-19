using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_IntoAccounts : System.Web.UI.Page
    {
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
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
        public DataTable gloabt
        {
            get
            {
                object dt = ViewState["gloabt"];
                return dt == null ? null : (DataTable)dt;
            }
            set
            {
                ViewState["gloabt"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["orderno"] != null)
                {
                    gloabsheetno = Request.QueryString["orderno"].ToString();
                }
                else
                {
                    gloabsheetno = "";
                }
                if (Request.QueryString["ptc"] != null)
                {
                    gloabptc = Request.QueryString["ptc"].ToString();
                }
                else
                {
                    gloabptc = "";
                }
                initialpage();
                PurorderdetailRepeaterbind();
            }
            if (Session["UserName"].ToString() == lab_zdr.Text.ToString())
            {
                Tb_note.Enabled = true;
                confirm.Enabled = true;//只有制单人才能修改数据--保存
                btn_delete.Enabled = true;
            }
            else
            {
                Tb_note.Enabled = false;
                confirm.Enabled = false;
                btn_delete.Enabled = false;

            }
        }
        protected void initialpage()
        {
            string orderno = gloabsheetno;
            string sqltext = "";
            //初始化
            sqltext = " select * from VIEW_TBMP_ACCOUNTS where TA_DOCNUM='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                LabelCode.Text = dt.Rows[0]["TA_DOCNUM"].ToString();
                LabelDate.Text = dt.Rows[0]["TA_ZDTIME"].ToString();
                LabelSupplier.Text = dt.Rows[0]["TA_SUPPLYNAME"].ToString();
                lab_zdr.Text = dt.Rows[0]["TA_ZDRNAME"].ToString();
                Tb_note.Text = dt.Rows[0]["TA_TOTALNOTE"].ToString();
            }

        }
        protected void PurorderdetailRepeaterbind()
        {
            string sqltext = "";
            string orderno = gloabsheetno;
            sqltext = "select A.*,B.BJSJ from  VIEW_TBMP_ACCOUNTS AS A LEFT JOIN (select PTC,BJSJ,rn from (select *,row_number() over(partition by PTC order by ISAGAIN) as rn from View_TBQM_APLYFORITEM) as a where rn<=1 )B ON A.TA_PTC=B.PTC  WHERE TA_DOCNUM='" + gloabsheetno + "' order by TA_DOCNUM DESC,PIC_JGNUM desc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            PurorderdetailRepeater.DataSource = dt;
            PurorderdetailRepeater.DataBind();
            if (PurorderdetailRepeater.Items.Count > 0)
            {
                NoDataPane.Visible = false;
            }
            else
            {
                NoDataPane.Visible = true;
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            string sqlcheck = "select TA_ZDTIME from TBMP_ACCOUNTS where TA_DOCNUM='" + gloabsheetno + "'";
            DataTable dtcheck = DBCallCommon.GetDTUsingSqlText(sqlcheck);
            string zdtimecheck = dtcheck.Rows[0]["TA_ZDTIME"].ToString().Trim();
            string zddatecheck = zdtimecheck.Substring(0, 7).ToString();
            string sqlhsif = "select * from TBFM_WXHS where WXHS_STATE='1' and WXHS_YEAR+'-'+WXHS_MONTH like '" + zddatecheck + "%'";
            DataTable dthsif = DBCallCommon.GetDTUsingSqlText(sqlhsif);
            if (dthsif.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该结算单已核算！');", true);
                return;
            }
            else
            {
                savedate();
            }
        }
        protected void savedate()
        {
            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            string ptc = "";
            string note = "";
            foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
            {
                ptc = ((Label)retim.FindControl("TA_PTC")).Text;
                note = ((TextBox)retim.FindControl("TA_NOTE")).Text;
                sqltext = "update TBMP_ACCOUNTS set TA_NOTE='" + note + "' WHERE TA_PTC='" + ptc + "'";
                sqltextlist.Add(sqltext);
            }
            sqltext = "select sum(TA_MONEY) as amount,sum(TA_WGHT) as weight from TBMP_ACCOUNTS where TA_DOCNUM='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            double f_money = Convert.ToDouble(dt.Rows[0]["amount"].ToString());
            double f_weight = Convert.ToDouble(dt.Rows[0]["weight"].ToString());
            sqltext = "update TBMP_ACCOUNTS set TA_AMOUNT=" + f_money + " ,TA_TOTALWGHT="+f_weight+", TA_TOTALNOTE='" + Tb_note.Text.ToString() + "' where TA_DOCNUM='" + gloabsheetno + "'";
            sqltextlist.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltextlist);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.opener.location.reload();self.close()", true);
        }
        public string get_po_cstate(string i)
        {
            string statestr = "";
            if (i == "1")
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_po_state(string i)
        {
            return "";
        }
        protected void btn_delete_Click(object sender, EventArgs e)
        {
            string sqlcheck = "select TA_ZDTIME from TBMP_ACCOUNTS where TA_DOCNUM='" + gloabsheetno + "'";
            DataTable dtcheck = DBCallCommon.GetDTUsingSqlText(sqlcheck);
            string zdtimecheck = dtcheck.Rows[0]["TA_ZDTIME"].ToString().Trim();
            string zddatecheck = zdtimecheck.Substring(0, 7).ToString();
            string sqlhsif = "select * from TBFM_WXHS where WXHS_STATE='1' and WXHS_YEAR+'-'+WXHS_MONTH like '" + zddatecheck + "%'";
            DataTable dthsif = DBCallCommon.GetDTUsingSqlText(sqlhsif);
            if (dthsif.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该结算单已核算！');", true);
                return;
            }
            else
            {
                List<string> list = new List<string>();
                int i = 0;
                foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
                {
                    CheckBox cb = (CheckBox)Reitem.FindControl("CKBOX_SELECT");
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            i++;
                            string ptc = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TA_PTC")).Text;
                            string sql = "delete from TBMP_ACCOUNTS where TA_PTC='" + ptc + "' and TA_DOCNUM='" + LabelCode.Text.ToString() + "'";
                            list.Add(sql);
                            sql = "update  TBMP_Order set TO_STATE='0'where TO_PTC='" + ptc + "'";
                            list.Add(sql);
                        }
                    }
                }
                if (i == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要删除的数据！');", true);
                }
                else
                {
                    DBCallCommon.ExecuteTrans(list);
                    savedate();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！');window.location.reload();", true);
                }
            }
         
        }
        protected void btn_shangcha_Click(object sender, EventArgs e)
        {
            int i = 0;
           
            string irqsheetno = "";
            foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
            {
                CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        irqsheetno = ((Label)retim.FindControl("TA_ORDERNUM")).Text;
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                return;
            }
            else if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条记录！');", true);
                return;
            }
            else
            {
                Response.Redirect("~/PM_Data/PM_Xie_IntoOrder.aspx?&orderno=" + irqsheetno + "");

            }
        }
    }
}
