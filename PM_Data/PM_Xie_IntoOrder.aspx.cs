using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_IntoOrder: System.Web.UI.Page
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
            sqltext = " select * from VIEW_TBMP_Order where TO_DOCNUM='"+gloabsheetno+"'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                LabelCode.Text = dt.Rows[0]["TO_DOCNUM"].ToString();
                LabelDate.Text = dt.Rows[0]["TO_ZDTIME"].ToString();
                LabelSupplier.Text = dt.Rows[0]["TO_SUPPLYNAME"].ToString();
                lab_zdr.Text = dt.Rows[0]["TO_ZDRNAME"].ToString();
               // tbDocument.Text = dt.Rows[0]["TOZDRNAME"].ToString();
                //tbDep.Text = dt.Rows[0]["Dep"].ToString();
                //lbDepID.Text = dt.Rows[0]["DepID"].ToString();
               // lbDocumentID.Text = dt.Rows[0]["TO_ZDR"].ToString();
                Tb_note.Text = dt.Rows[0]["TO_NOTE"].ToString();
            }

        }
        protected void PurorderdetailRepeaterbind()
        {
            string sqltext = "";
            string orderno = gloabsheetno;
            sqltext = "select B.BJSJ, TO_DOCNUM, TO_BJDOCNUM, TO_PTC, TO_ZDR, TO_ZDTIME, TO_SUPPLYID, TO_AMOUNT, CONVERT(varchar(12) , TO_JHQ,23) as TO_JHQ, CONVERT(varchar(12) , TO_SJJHQ,23) as TO_SJJHQ, TO_NOTE, TO_STATE, PIC_SHEETNO, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_WXTYPE, PIC_TUHAO, PIC_MNAME, PIC_ZXNUM, PIC_SUPPLYTIME, PIC_PRICE, TO_ZDRNAME, PIC_PTCODE, TO_PROCESS, TO_UWGHT, TO_MONEY, TO_SUPPLYNAME, TO_ENGNAME, TO_TOTALNOTE from VIEW_TBMP_Order AS A LEFT JOIN (select PTC,BJSJ,rn from (select *,row_number() over(partition by PTC order by ISAGAIN) as rn from View_TBQM_APLYFORITEM) as a where rn<=1 ) as B ON A.TO_PTC=B.PTC WHERE TO_DOCNUM='" + gloabsheetno + "' ORDER BY TO_DOCNUM DESC,TO_BJDOCNUM DESC,TO_PTC DESC";
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
            savedate();
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);

        }
        protected void savedate()
        {
            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            string ptc = "";
            //string cgtimerq = "";
            string cgtimerq1 = "";
            string note = "";
                foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
                {
                    ptc = ((Label)retim.FindControl("TO_PTC")).Text;
                    note = ((TextBox)retim.FindControl("TO_NOTE")).Text;
                    sqltext = "update TBMP_Order set TO_NOTE='" + note + "' WHERE TO_PTC='" + ptc + "'";
                    sqltextlist.Add(sqltext);
                }
                sqltext = "select sum(TO_MONEY) as amount from view_TBMP_Order where TO_DOCNUM='" +gloabsheetno+ "'";
           DataTable  dt=DBCallCommon.GetDTUsingSqlText(sqltext);
               double f_money=Convert.ToDouble(dt.Rows[0]["amount"].ToString());
            sqltext="update TBMP_Order set TO_AMOUNT="+f_money+" ,TO_TOTALNOTE='"+Tb_note.Text.ToString()+"' where TO_DOCNUM='"+gloabsheetno+"'";
            sqltextlist.Add(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.opener.location.reload();self.close()", true);
            }
          
        //private int cansave()//判断计划日期是否小于制单日期
        //{
        //    int temp = 0;
        //    int i = 0;
        //    int j = 0;
        //    string cgrqtime = "";
        //    foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
        //    {
        //        cgrqtime = ((TextBox)retim.FindControl("PO_CGTIMERQ")).Text;

        //        if (cgrqtime == "")
        //        {
        //            i++;
        //        }
        //        else if (Convert.ToDateTime(cgrqtime) <= Convert.ToDateTime(LabelDate.Text))
        //        {
        //            j++;
        //        }
        //    }
        //    if (i > 0)
        //    {
        //        temp = 1;
        //    }
        //    else if (j > 0)
        //    {
        //        temp = 2;
        //    }
        //    return temp;
        //}

        //protected void btn_concel_Click(object sender, EventArgs e)
        //{
        //    int i = 0;
        //    int j = 0;
        //    double jhnum = 0;
        //    string sqltext = "";
        //    double zje = 0;
        //    foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
        //    {
        //        CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //        if (cbx.Checked)
        //        {
        //            i++;
        //            jhnum = Convert.ToDouble(((Label)retim.FindControl("PO_RECGDNUM")).Text);
        //            if (jhnum != 0)
        //            {
        //                j++;
        //            }
        //        }
        //    }
        //    if (i == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
        //    }
        //    else if (j > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择了已入库的数据，不能取消！');", true);
        //    }
        //    else
        //    {
        //        foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
        //        {
        //            CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //            if (cbx.Checked)
        //            {
        //                string ptcode = ((Label)retim.FindControl("PO_PCODE")).Text;
        //                string BZcode = LabelCode.Text.Substring(0, 4).ToString();
        //                if (BZcode == "PORD")
        //                {
        //                    sqltext = "delete from TBPC_PURORDERDETAIL where PO_PCODE='" + ptcode + "'";
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                    sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='6' where PUR_PTCODE='" + ptcode + "'";
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                    sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='0' where PIC_PTCODE='" + ptcode + "'";
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                }
        //                else if (BZcode == "ZBPO")
        //                {
        //                    sqltext = "delete from TBPC_PURORDERDETAIL where PO_PCODE='" + ptcode + "'";
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                    sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "'";
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                }
        //            }
        //        }
        //        sqltext = "select PO_PCODE from TBPC_PURORDERDETAIL where PO_CODE='" + LabelCode.Text + "'";
        //        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        if (dt.Rows.Count == 0)
        //        {
        //            sqltext = "delete from TBPC_PURORDERTOTAL where PO_CODE='" + LabelCode.Text + "'";
        //            DBCallCommon.ExeSqlText(sqltext);
        //            Response.Redirect("~/PC_Data/PC_TBPC_PurOrder.aspx?orderno=" + gloabsheetno + "");
        //        }
        //        else
        //        {
        //            sqltext = "select sum(ctamount) as zje from View_TBPC_PURORDERDETAIL_PLAN where orderno='" + LabelCode.Text + "' and  detailcstate!='1'";
        //            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
        //            if (dt1.Rows.Count > 0)
        //            {
        //                zje = Convert.ToDouble(dt1.Rows[0]["zje"].ToString());
        //            }
        //            sqltext = "UPDATE TBPC_PURORDERTOTAL SET PO_ZJE=" + zje + "  WHERE PO_CODE='" + LabelCode.Text.ToString() + "'";
        //            DBCallCommon.ExeSqlText(sqltext);
        //            PurorderdetailRepeaterbind();
        //        }
        //    }

        //}
        protected void goback_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/PC_Data/PC_TBPC_PurOrder.aspx?orderno=" + gloabsheetno + "");
        }

        protected void btn_shangcha_Click(object sender, EventArgs e)
        {
            int i = 0;
            string ptcode = "";
            string irqsheetno = "";
            foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
            {
                CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        ptcode = ((Label)retim.FindControl("TO_PTC")).Text;
                        irqsheetno = ((Label)retim.FindControl("PIC_SHEETNO")).Text;
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
                Response.Redirect("PM_Xie_check_detail.aspx?num1=1&num2=5&sheetno=" + irqsheetno + "");

            }
        }

        protected void btn_xiacha_Click(object sender, EventArgs e)
        {
            int i = 0;
            string ptcode = "";
            string irqsheetno = "";
            string sqltext = "";
            foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
            {
                CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        ptcode = ((Label)retim.FindControl("TO_PTC")).Text;
                       // irqsheetno = ((Label)retim.FindControl("PIC_SHEETNO")).Text;
                        sqltext = "select * from TBMP_ACCOUNTS where TA_PTC='" + ptcode + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count >= 1)
                        {
                            irqsheetno = dt.Rows[0]["TA_DOCNUM"].ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该数据尚未生成结算单！');", true);
                            return;
                        }
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
                Response.Redirect("PM_Xie_IntoAccounts.aspx?&orderno=" + irqsheetno + "");

            }
        }
        //protected void hcancel()//取消
        //{
        //    int i = 0;
        //    string sqltext = "";
        //    foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
        //    {
        //        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //        if (cbx != null)//存在行
        //        {
        //            if (cbx.Checked)
        //            {
        //                i++;
        //                sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='6' WHERE PUR_PTCODE='" + ((Label)Reitem.FindControl("PO_PCODE")).Text.ToString() + "'";
        //                DBCallCommon.ExeSqlText(sqltext);
        //                sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='0' where PIC_PCODE = '" + ((Label)Reitem.FindControl("PO_PCODE")).Text.ToString() + "'";
        //                DBCallCommon.ExeSqlText(sqltext);
        //                sqltext = "DELETE FROM TBPC_PURORDERDETAIL WHERE PO_SHEETNO='" + gloabsheetno + "' AND PO_PCODE='" + ((Label)Reitem.FindControl("PO_PCODE")).Text.ToString() + "'";
        //                DBCallCommon.ExeSqlText(sqltext);
        //            }
        //        }
        //    }
        //    if (i > 0)
        //    {
        //        sqltext = "SELECT PO_ID FROM TBPC_PURORDERDETAIL WHERE PO_SHEETNO='" + gloabsheetno + "'";
        //        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        if (dt.Rows.Count == 0)
        //        {
        //            sqltext = "DELETE FROM TBPC_PURORDERTOTAL WHERE PO_CODE='" + gloabsheetno + "'";
        //            DBCallCommon.ExeSqlText(sqltext);
        //            Response.Redirect("~/PC_Data/TBPC_Purordertotal_list.aspx");
        //        }
        //        PurorderdetailRepeaterbind();
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

        //    }
        //}
        //protected void allcancel()//单子取消，计划、比价单状态都向前反改，删除订单里的当前信息
        //{
        //    string sqltext = "";
        //    List<string> sqltexts = new List<string>();
        //    sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='6' where PUR_PTCODE in  " +
        //              "(select PO_PCODE from TBPC_PURORDERDETAIL where PO_SHEETNO='" + gloabsheetno + "')";
        //    sqltexts.Add(sqltext);
        //    sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='0' where PIC_PCODE in  " +
        //              "(select PO_PCODE from TBPC_PURORDERDETAIL where PO_SHEETNO='" + gloabsheetno + "')";
        //    sqltexts.Add(sqltext);
        //    sqltext = "delete from TBPC_PURORDERTOTAL WHERE PO_CODE='" + gloabsheetno + "'";
        //    sqltexts.Add(sqltext);
        //    sqltext = "delete from TBPC_PURORDERDETAIL WHERE PO_SHEETNO='" + gloabsheetno + "'";
        //    sqltexts.Add(sqltext);
        //    DBCallCommon.ExecuteTrans(sqltexts);
        //}

      
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
             int temp = isselected();
             List<string> list = new List<string>();
                if (temp == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择的数据包含已生成结算单的数据,本次操作无效！');", true);
                }
                else
                {
                    foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
                    {
                        CheckBox cb = (CheckBox)Reitem.FindControl("CKBOX_SELECT");
                        if (cb != null)
                        {
                            if (cb.Checked)
                            {
                                string ptc = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TO_PTC")).Text;
                                string sql = "delete from TBMP_Order where TO_PTC='" + ptc + "' and TO_DOCNUM='" + LabelCode.Text.ToString() + "'";
                                list.Add(sql);
                                sql = "update  TBMP_IQRCMPPRICE set PIC_ORDERSTATE='0'where PTC='" + ptc + "'";
                                list.Add(sql);
                            }
                        }
                    }
                    DBCallCommon.ExecuteTrans(list);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！');window.location.reload();", true);
                }
        }
        protected int isselected()
        {
            int temp = 0;
           
            int i = 0;//是否选择数据
            int j = 0;//是否生成结算单
            foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string ptc = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TO_PTC")).Text;
                        string sql = "select TO_PTC from TBMP_Order where TO_PTC='" + ptc + "' and TO_STATE='1'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt.Rows.Count > 0)
                        {
                            j++;
                            break;
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//是否已生成结算单
            {
                temp = 2;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        //protected void TO_JHQ_Textchanged(object sender, EventArgs e)
        //{
        //    string time = "";
        //    string time1 = "";
        //    int k = 0;
        //    int j = 0;
        //    foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
        //    {
        //        k++;
        //        TextBox tbx = (TextBox)Reitem.FindControl("TO_JHQ");
        //        time1 = ((Label)Reitem.FindControl("Label1")).Text;
        //        time = tbx.Text;
        //        if (time != "" && time1 != time)
        //        {
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
        //    {
        //        j++;
        //        if (j >= k)
        //        {
        //            ((TextBox)Reitem.FindControl("TO_JHQ")).Text = time;
        //            ((Label)Reitem.FindControl("Label1")).Text = time;
        //        }
        //    }
        //}
        //protected void TO_SJJHQ_Textchanged(object sender, EventArgs e)
        //{
        //    string time = "";
        //    string time1 = "";
        //    int k = 0;
        //    int j = 0;
        //    foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
        //    {
        //        k++;
        //        TextBox tbx = (TextBox)Reitem.FindControl("TO_SJJHQ");
        //        time1 = ((Label)Reitem.FindControl("Label2")).Text;
        //        time = tbx.Text;
        //        if (time != "" && time1 != time)
        //        {
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
        //    {
        //        j++;
        //        if (j >= k)
        //        {
        //            ((TextBox)Reitem.FindControl("TO_SJJHQ")).Text = time;
        //            ((Label)Reitem.FindControl("Label2")).Text = time;
        //        }
        //    }
        //}
    }
}
