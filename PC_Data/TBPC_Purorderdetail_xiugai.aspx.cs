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
using System.Drawing;
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class TBPC_Purorderdetail_xiugai : System.Web.UI.Page
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
        }
        protected void initialpage()
        {
            string orderno = gloabsheetno;
            string sqltext = "";
            //业务员下拉框绑定
            sqltext = "select ST_NAME,ST_ID,ST_PD from TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString() + "'";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = dt0.DefaultView;
            dv.RowFilter = "ST_ID= '" + Session["UserID"].ToString()+"'";
            cob_ywy.DataSource = dv;
            cob_ywy.DataTextField = "ST_NAME";
            cob_ywy.DataValueField = "ST_ID";
            cob_ywy.DataBind();
            //主管下拉框绑定
            dv = dt0.DefaultView;
            cob_zg.DataSource = dv;
            cob_zg.DataTextField = "ST_NAME";
            cob_zg.DataValueField = "ST_ID";
            cob_zg.DataBind();
            //初始化
            sqltext = "SELECT orderno AS Code,supplierid,suppliernm AS Supplier,substring(zdtime,1,10) AS Date,abstract AS Abstract," +
                "versionno AS VersionNo,changdate AS ChangeDate,changmanid,changnm AS ChangeMan,changreason AS ChangeReason," +
                "zdrid AS DocumentID,zdrnm AS Document,ywyid AS ClerkID,ywynm AS Clerk,depid AS DepID,depnm AS Dep, " +
                "zgid AS ManagerID,zgnm AS Manager,totalnote AS note,totalstate,totalcstate  "+
                "FROM View_TBPC_PURORDERTOTAL WHERE orderno='" + orderno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                LabelCode.Text = dt.Rows[0]["Code"].ToString();
                LabelDate.Text = dt.Rows[0]["Date"].ToString();
                LabelSupplier.Text = dt.Rows[0]["Supplier"].ToString();
                tb_Abstract.Text = dt.Rows[0]["Abstract"].ToString();
                LabelVersionNo.Text = dt.Rows[0]["VersionNo"].ToString();
                LabelChangeDate.Text = dt.Rows[0]["ChangeDate"].ToString();
                LabelChangeMan.Text = dt.Rows[0]["ChangeMan"].ToString();
                tb_ChangeReason.Text = dt.Rows[0]["ChangeReason"].ToString();
                tbDep.Text = dt.Rows[0]["Dep"].ToString();
                tbDocument.Text = dt.Rows[0]["Document"].ToString();
                lbDepID.Text = dt.Rows[0]["DepID"].ToString();
                lbDocumentID.Text = dt.Rows[0]["DocumentID"].ToString();
                Tb_note.Text = dt.Rows[0]["note"].ToString();
                if (dt.Rows[0]["ClerkID"].ToString() != "")//业务员
                {
                    ListItem listyew = new ListItem(dt.Rows[0]["Clerk"].ToString(), dt.Rows[0]["ClerkID"].ToString());
                    if (cob_ywy.Items.Contains(listyew))
                    {
                        cob_ywy.SelectedValue = dt.Rows[0]["ClerkID"].ToString();
                    }
                }
                if (dt.Rows[0]["ManagerID"].ToString() != "")//主管
                {
                    ListItem listzg = new ListItem(dt.Rows[0]["Manager"].ToString(), dt.Rows[0]["ManagerID"].ToString());
                    if (cob_zg.Items.Contains(listzg))
                    {
                        cob_zg.SelectedValue = dt.Rows[0]["ManagerID"].ToString();
                    }
                }
            }
            sqltext = "SELECT MP_OLDPTCODE FROM TBPC_MPCHANGEDETAIL WHERE MP_STATE='0'";
            gloabt = DBCallCommon.GetDTUsingSqlText(sqltext);
        }
        protected void PurorderdetailRepeaterbind()
        {
            string sqltext = "";
            string orderno = gloabsheetno;
            sqltext = "SELECT orderno as PO_SHEETNO,picno as PO_ICLSHEETNO,ptcode as PO_PCODE ," +
                      "marid as PO_MARID,marnm as PO_MARNAME,margg as PO_GUIGE,marcz as PO_CAIZHI," +
                      "margb as PO_GUOBIAO,marunit as PO_UNIT,convert(float,num) as PO_QUANTITY,convert(float,zxnum) as PO_ZXNUM," +
                      "convert(float,fznum) as PO_FZNUM,convert(float,zxfznum) as PO_ZXFZNUM,marfzunit as PO_FZUNIT,PO_PZ,cgtimerq as PO_CGTIMERQ," +
                      "convert(float,recgdnum) as PO_RECGDNUM,recdate as PO_RECDATE,convert(float,price) as PO_UPRICE,convert(float,ctprice) as PO_CTAXUPRICE," +
                      "convert(float,amount) as PO_AMOUNT,taxrate as PO_TAXRATE,convert(float,ctamount) as PO_TOTALPRICE," +
                      "detailnote as PO_NOTE,planmode as PO_PMODE,ptcode as PO_PTCODE,PO_MASHAPE,length as PO_LENGTH," +
                      "width as PO_WIDTH,keycoms as PO_KEYCOMS,detailstate as PO_STATE,detailcstate as PO_CSTATE," +
                      "engid as PO_ENGID,PO_TUHAO,MWEIGHT,GUIGE  " +
                      "FROM (select * from ((select * from View_TBPC_PURORDERDETAIL_PLAN)a left join (select MWEIGHT,GUIGE,ID as TBMA_ID from TBMA_MATERIAL)b on a.marid=b.TBMA_ID))c WHERE orderno='" + orderno + "' and detailcstate='0' order by ptcode,marnm,margg asc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //DataView dv = null;
            //if (Tb_MARNAME.Text != "")
            //{
            //    dv = dt.DefaultView;
            //    dv.RowFilter = "PO_MARNAME like '%" + Tb_MARNAME.Text + "%'"; //对dataView进行筛选 
            //    dt = dv.ToTable();
            //}
            //if (Tb_MARGG.Text != "")
            //{
            //    dv = dt.DefaultView;
            //    dv.RowFilter = "PO_GUIGE like '%" + Tb_MARGG.Text + "%'"; //对dataView进行筛选 
            //    dt = dv.ToTable();
            //}
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

        //protected void btn_search_click(object sender, EventArgs e)
        //{
        //    PurorderdetailRepeaterbind();
        //}
        //protected void btn_clear_click(object sender, EventArgs e)
        //{
        //    Tb_MARNAME.Text = "";
        //    Tb_MARGG.Text = "";
        //}
        protected void save_Click(object sender, EventArgs e)
        {
            savedate();
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
           
        }
        protected void savedate()
        {
            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            string ptcode = "";
            double zxnum = 0;
            double zxfznum = 0;
            double zje = 0;
            int pz = 0;
            string cgtimerq = "";
            string note = "";
            int temp = cansave();
            if (temp == 0)
            {
                foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
                {
                    //cgfs = ((DropDownList)retim.FindControl("DropDownList1")).SelectedItem.Text;

                    ptcode = ((Label)retim.FindControl("PO_PCODE")).Text;
                    zxnum = Convert.ToDouble(((TextBox)retim.FindControl("PO_ZXNUM")).Text == "" ? "0" : ((TextBox)retim.FindControl("PO_ZXNUM")).Text);
                    zxfznum = Convert.ToDouble(((TextBox)retim.FindControl("PO_ZXFZNUM")).Text == "" ? "0" : ((TextBox)retim.FindControl("PO_ZXFZNUM")).Text);
                    pz = Convert.ToInt32(((TextBox)retim.FindControl("PO_PZ")).Text == "" ? "0" : ((TextBox)retim.FindControl("PO_PZ")).Text);
                    DateTime datime = Convert.ToDateTime(((TextBox)retim.FindControl("PO_CGTIMERQ")).Text);
                    cgtimerq = datime.ToString("yyyy-MM-dd");
                    note = ((TextBox)retim.FindControl("PO_NOTE")).Text;
                    sqltext = "update TBPC_PURORDERDETAIL set PO_ZXNUM='" + zxnum + "',PO_ZXFZNUM='" +
                              zxfznum + "',PO_CGTIMERQ='" + cgtimerq + "',PO_PZ='"+pz+"',PO_NOTE='" + note + "',PO_CGFS='——' " +
                              "where PO_PCODE='" + ptcode + "'";
                    sqltextlist.Add(sqltext);
                }
                sqltext = "select sum(ctamount) as zje from View_TBPC_PURORDERDETAIL_PLAN where orderno='" + LabelCode.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    zje = Convert.ToDouble(dt.Rows[0]["zje"].ToString());
                }
                sqltext = "UPDATE TBPC_PURORDERTOTAL SET PO_NOTE='" + Tb_note.Text.ToString() + "',PO_ABSTRACT='" + tb_Abstract.Text + "', " +
                          "PO_BSMANID='" + cob_ywy.SelectedValue.ToString() + "'," +
                          "PO_TMLDID='" + cob_zg.SelectedValue.ToString() + "',PO_ZJE=" + zje + "  " +
                          "WHERE PO_CODE='" + LabelCode.Text.ToString() + "'";
                sqltextlist.Add(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                Response.Redirect("~/PC_Data/PC_TBPC_PurOrder.aspx?orderno=" + gloabsheetno + "");
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写交货日期！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('交货日期不能小于制单日期！');", true);
            }

        }
        private int cansave()//判断计划日期是否小于制单日期
        {
            int temp = 0;
            int i = 0;
            int j = 0;
            string cgrqtime = "";
            foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
            {
                cgrqtime = ((TextBox)retim.FindControl("PO_CGTIMERQ")).Text;
                
                if (cgrqtime == "")
                {
                    i++;
                }
                else if (Convert.ToDateTime(cgrqtime) <= Convert.ToDateTime(LabelDate.Text))
                {
                    j++;
                }
            }
            if (i > 0)
            {
                temp = 1;
            }
            else if(j>0)
            {
                temp = 2;
            }
            return temp;
        }
        protected void btn_hclose_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string ptcode = "";
            foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    if (((Label)Reitem.FindControl("PO_CSTATE")).Text.ToString() == "0")//未关闭时才可关闭
                    {
                        ptcode = ((Label)Reitem.FindControl("PO_PCODE")).Text;
                        sqltext = "update TBPC_PURORDERDETAIL set PO_CSTATE='1' where PO_PCODE='" + ptcode + "'";//行关闭
                        DBCallCommon.ExeSqlText(sqltext);
                        //sqltext = "update TBPC_IQRCMPPRICE set PIC_QUANTITY=PIC_QUANTITY-'" + float.Parse(((Label)Reitem.FindControl("PO_QUANTITY")).Text) + "' where PIC_PCODE='" + ptcode + "'";
                        //sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='4' where PIC_PCODE='" + ptcode + "'";
                        //DBCallCommon.ExeSqlText(sqltext);
                        //sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "'";
                        //DBCallCommon.ExeSqlText(sqltext);
                        sqltext = "update TBPC_MPCHANGEDETAIL set MP_STATE='1' where MP_STATE='" + ptcode + "'";//变更执行
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            PurorderdetailRepeaterbind();
        }

        //protected void btn_bk_Click(object sender, EventArgs e)
        //{
        //    string sqltext = "";
        //    string ptcode = "";
        //    string marid = "";
        //    string marname = "";
        //    string pjid = "";
        //    string pjname = "";
        //    string engid = "";
        //    string engname = "";
        //    string guige = "";
        //    string caizhi = "";
        //    string unit = "";
        //    float weight = 0;
        //    int num = 0;
        //    string guobiao = "";
        //    string usage = "";
        //    string type = "";
        //    string timerq = "";
        //    string envreffct = "";
        //    string tracknum = "";
        //    string submitid = "";
        //    string submitidname = "";
        //    string submittm = "";
        //    string note = "";
        //    SqlDataReader dr = null;
        //    foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
        //    {
        //        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //        if (cbx != null)//存在行
        //        {
        //            if (cbx.Checked)
        //            {
        //                if (((Label)Reitem.FindControl("PO_STATE")).Text.ToString() == "0")//备库
        //                {
        //                    ptcode = ((Label)Reitem.FindControl("PO_PCODE")).Text;
        //                    sqltext = "select PUR_PJID,PUR_PJNAME,PUR_ENGID,PUR_ENGNAME from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
        //                    dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //                    while (dr.Read())
        //                    {
        //                        pjid = dr["PUR_PJID"].ToString();
        //                        pjname = dr["PUR_PJNAME"].ToString();
        //                        engid = dr["PUR_ENGID"].ToString();
        //                        engname = dr["PUR_ENGNAME"].ToString();
        //                    }
        //                    dr.Close();
        //                    marid = ((Label)Reitem.FindControl("PO_MARID")).Text;
        //                    marname = ((Label)Reitem.FindControl("PO_MARNAME")).Text;
        //                    guige = ((Label)Reitem.FindControl("PO_GUIGE")).Text;
        //                    caizhi = ((Label)Reitem.FindControl("PO_CAIZHI")).Text;
        //                    unit = ((Label)Reitem.FindControl("PO_UNIT")).Text;
        //                    //weight = ((Label)Reitem.FindControl("PO_PCODE")).Text;
        //                    num = Convert.ToInt32(((Label)Reitem.FindControl("PO_QUANTITY")).Text);
        //                    guobiao = ((Label)Reitem.FindControl("PO_GUOBIAO")).Text;
        //                    //usage = ((Label)Reitem.FindControl("PO_PCODE")).Text;
        //                    //type = ((Label)Reitem.FindControl("PO_PCODE")).Text;
        //                    //timerq = ((Label)Reitem.FindControl("PO_PCODE")).Text;
        //                    //envreffct = ((Label)Reitem.FindControl("PO_PCODE")).Text;
        //                    tracknum = ((Label)Reitem.FindControl("PO_PCODE")).Text;
        //                    submitid = lbDocumentID.Text;
        //                    submitidname = tbDocument.Text;
        //                    submittm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //                    note = ((TextBox)Reitem.FindControl("PO_NOTE")).Text;
        //                    sqltext = "update TBPC_PURORDERDETAIL set PO_STATE='2' where PO_PCODE='" + ptcode + "'";//备库
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                    sqltext = "insert into TBPM_MPFORLIB(MP_MARID, MP_NAME, MP_PJID, MP_PJNAME,MP_ENGID, " +
        //                              "MP_ENGNAME, MP_GUIGE, MP_CAIZHI, MP_UNIT, MP_WEIGHT, MP_NUMBER, MP_STANDARD, " +
        //                              "MP_USAGE, MP_TYPE,MP_TIMERQ, MP_ENVREFFCT, MP_TRACKNUM,MP_SUBMITID,MP_SUBMITNAME, " +
        //                              "MP_SUBMITTM,MP_NOTE) " +
        //                              "values ('" + marid + "','" + marname + "','" + pjid + "','" + pjname + "','" +
        //                              engid + "','" + engname + "','" + guige + "','" + caizhi + "','" +
        //                              unit + "','" + weight + "','" + num + "','" + guobiao + "','" +
        //                              usage + "','" + type + "','" + timerq + "','" + envreffct + "','" +
        //                              tracknum + "','" + submitid + "','" + submitidname + "','" + submittm + "','" + note + "')";
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                    sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='5' where PIC_PCODE='" + ptcode + "'";
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                    sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='5' where PUR_PTCODE='" + ptcode + "'";
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                    sqltext = "update TBPC_MPCHANGEDETAIL set MP_STATE='1' where MP_STATE='" + ptcode + "'";//变更执行
        //                    DBCallCommon.ExeSqlText(sqltext);
        //                }
        //            }
        //        }
        //    }
        //}

        protected void btn_concel_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            double jhnum = 0;
            string sqltext = "";
            double zje = 0;
            foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
            {
                CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    jhnum = Convert.ToDouble(((Label)retim.FindControl("PO_RECGDNUM")).Text);
                    if (jhnum != 0)
                    {
                        j++;
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
            }
            else if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择了已入库的数据，不能取消！');", true);
            }
            else
            {
                foreach (RepeaterItem retim in PurorderdetailRepeater.Items)
                {
                    CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        string ptcode = ((Label)retim.FindControl("PO_PCODE")).Text;
                        string BZcode = LabelCode.Text.Substring(0, 4).ToString();
                        if (BZcode == "PORD")
                        {
                            sqltext = "delete from TBPC_PURORDERDETAIL where PO_PCODE='" + ptcode + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                            sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='6' where PUR_PTCODE='" + ptcode + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                            sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='0' where PIC_PTCODE='" + ptcode + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                        else if (BZcode == "ZBPO")
                        {
                            sqltext = "delete from TBPC_PURORDERDETAIL where PO_PCODE='" + ptcode + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                            sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                    }
                }
                sqltext = "select PO_PCODE from TBPC_PURORDERDETAIL where PO_CODE='" + LabelCode.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    sqltext = "delete from TBPC_PURORDERTOTAL where PO_CODE='" + LabelCode.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    Response.Redirect("~/PC_Data/PC_TBPC_PurOrder.aspx?orderno=" + gloabsheetno + "");
                }
                else
                {
                    sqltext = "select sum(ctamount) as zje from View_TBPC_PURORDERDETAIL_PLAN where orderno='" + LabelCode.Text + "' and  detailcstate!='1'";
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt1.Rows.Count > 0)
                    {
                        zje = Convert.ToDouble(dt1.Rows[0]["zje"].ToString());
                    }
                    sqltext = "UPDATE TBPC_PURORDERTOTAL SET PO_ZJE=" + zje + "  WHERE PO_CODE='" + LabelCode.Text.ToString() + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    PurorderdetailRepeaterbind();
                }
            }

        }
        protected void goback_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_PurOrder.aspx?orderno=" + gloabsheetno + "");
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
                        ptcode = ((Label)retim.FindControl("PO_PCODE")).Text;
                        irqsheetno = ((Label)retim.FindControl("PO_ICLSHEETNO")).Text;
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
                Response.Redirect("~/PC_Data/TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + irqsheetno + "&ptc=" + ptcode + "");
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "pur_bjdshangcha('" + ptcode + "');", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "<script>window.open('~/PC_Data/TBPC_IQRCMPPRCLST_checked.aspx?ptc="+ptcode+"')</script>", true);

            }
        }

        protected void btn_xiacha_Click(object sender, EventArgs e)
        {

        }
        protected void hcancel()//取消
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='6' WHERE PUR_PTCODE='" + ((Label)Reitem.FindControl("PO_PCODE")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                        sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='0' where PIC_PCODE = '" + ((Label)Reitem.FindControl("PO_PCODE")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                        sqltext = "DELETE FROM TBPC_PURORDERDETAIL WHERE PO_SHEETNO='" + gloabsheetno + "' AND PO_PCODE='" + ((Label)Reitem.FindControl("PO_PCODE")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                sqltext = "SELECT PO_ID FROM TBPC_PURORDERDETAIL WHERE PO_SHEETNO='" + gloabsheetno + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    sqltext = "DELETE FROM TBPC_PURORDERTOTAL WHERE PO_CODE='" + gloabsheetno + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    Response.Redirect("~/PC_Data/TBPC_Purordertotal_list.aspx");
                }
                PurorderdetailRepeaterbind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

            }
        }
        protected void allcancel()//单子取消，计划、比价单状态都向前反改，删除订单里的当前信息
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='6' where PUR_PTCODE in  " +
                      "(select PO_PCODE from TBPC_PURORDERDETAIL where PO_SHEETNO='" + gloabsheetno + "')";
            sqltexts.Add(sqltext);
            sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='0' where PIC_PCODE in  " +
                      "(select PO_PCODE from TBPC_PURORDERDETAIL where PO_SHEETNO='" + gloabsheetno + "')";
            sqltexts.Add(sqltext);
            sqltext = "delete from TBPC_PURORDERTOTAL WHERE PO_CODE='" + gloabsheetno + "'";
            sqltexts.Add(sqltext);
            sqltext = "delete from TBPC_PURORDERDETAIL WHERE PO_SHEETNO='" + gloabsheetno + "'";
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
        }
        protected void hclose()//行关闭
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        sqltext = "update TBPC_PURORDERDETAIL set PO_CSTATE='1' WHERE PO_SHEETNO='" + gloabsheetno + "' " +
                                  "and PO_PCODE='" + ((Label)Reitem.FindControl("PO_PCODE")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                sqltext = "SELECT PO_ID FROM TBPC_PURORDERDETAIL WHERE PO_SHEETNO='" + gloabsheetno + "' and PO_CSTATE='0'";//是否还存在未关闭的，如果都关闭则整单关闭
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='1'  WHERE PO_CODE='" + gloabsheetno + "'";//单号关闭
                    DBCallCommon.ExeSqlText(sqltext);
                }
                PurorderdetailRepeaterbind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

            }
        }
        protected void fhclose()//行反关闭
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        sqltext = "update TBPC_PURORDERDETAIL set PO_CSTATE='0' WHERE PO_SHEETNO='" + gloabsheetno + "' " +
                                  "and PO_PCODE='" + ((Label)Reitem.FindControl("PO_PCODE")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                sqltext = "SELECT PO_ID FROM TBPC_PURORDERDETAIL WHERE PO_SHEETNO='" + gloabsheetno + "' and PO_CSTATE='0'";//是否还存在未关闭的，如果都存则整单未关闭
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='0'  WHERE PO_CODE='" + gloabsheetno + "'";//单号反关闭
                    DBCallCommon.ExeSqlText(sqltext);
                }
                PurorderdetailRepeaterbind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

            }
        }
        protected void allclose()
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='1'  WHERE PO_CODE='" + gloabsheetno + "'";//单号关闭
            sqltexts.Add(sqltext);
            sqltext = "update  TBPC_PURORDERDETAIL set PO_CSTATE='1' WHERE PO_SHEETNO='" + gloabsheetno + "'";//条目关闭
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
        }//单子关闭
        protected void fallclose()
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='0'  WHERE PO_CODE='" + gloabsheetno + "'";//单号反关闭
            sqltexts.Add(sqltext);
            sqltext = "update  TBPC_PURORDERDETAIL set PO_CSTATE='0' WHERE PO_SHEETNO='" + gloabsheetno + "'";//条目反关闭
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
        }//单子反关闭
        private double sum = 0;
        private double num = 0;
        private double zxnum = 0;
        private double total = 0;
        protected void PurorderdetailRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                
                if (((Label)e.Item.FindControl("PO_AMOUNT")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PO_AMOUNT")).Text.ToString() == System.String.Empty || ((Label)e.Item.FindControl("PO_AMOUNT")).Text.ToString() == "0")
                {
                    total = total + 0;
                    ((Label)e.Item.FindControl("PO_AMOUNT")).Text = "0";
                }
                else
                {
                    total += Convert.ToDouble(((Label)e.Item.FindControl("PO_AMOUNT")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("PO_TOTALPRICE")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PO_TOTALPRICE")).Text.ToString() == System.String.Empty || ((Label)e.Item.FindControl("PO_TOTALPRICE")).Text.ToString() == "0")
                {
                    sum = sum + 0;
                    ((Label)e.Item.FindControl("PO_TOTALPRICE")).Text = "0";
                }
                else
                {
                    sum += Convert.ToDouble(((Label)e.Item.FindControl("PO_TOTALPRICE")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("PO_QUANTITY")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PO_QUANTITY")).Text.ToString() == System.String.Empty || ((Label)e.Item.FindControl("PO_QUANTITY")).Text.ToString() == "0")
                {
                    num = num + 0;
                    ((Label)e.Item.FindControl("PO_QUANTITY")).Text = "0";
                }
                else
                {
                    num += Convert.ToDouble(((Label)e.Item.FindControl("PO_QUANTITY")).Text.ToString());
                }
                if (((TextBox)e.Item.FindControl("PO_ZXNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("PO_ZXNUM")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("PO_ZXNUM")).Text.ToString() == "0")
                {
                    zxnum = zxnum + 0;
                    ((TextBox)e.Item.FindControl("PO_ZXNUM")).Text = "0";
                }
                else
                {
                    zxnum += Convert.ToDouble(((TextBox)e.Item.FindControl("PO_ZXNUM")).Text.ToString());
                }

                if (((Label)e.Item.FindControl("PO_STATE")).Text.ToString() == "1")
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#FFCCCC";
                }
                //变更颜色标记
                string expression = "MP_OLDPTCODE='" + ((Label)e.Item.FindControl("PO_PCODE")).Text + "'";
                string sortOrder = "MP_OLDPTCODE ASC";
                DataRow[] foundRows = gloabt.Select(expression, sortOrder);
                if (foundRows.Length > 0)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#EEB422";
                    //((Label)e.Item.FindControl("Label1")).ForeColor = Color.Red;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)(e.Item.FindControl("Labeltotal10"))).Text = Convert.ToString(sum);
                ((Label)(e.Item.FindControl("totalnum"))).Text = Convert.ToString(num);
                ((Label)(e.Item.FindControl("zxnum"))).Text = Convert.ToString(zxnum);
                ((Label)(e.Item.FindControl("Labeltotal"))).Text = Convert.ToString(total);
            }
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

        protected void PO_CGTIMERQ_Textchanged(object sender, EventArgs e)
        {
            string time = "";
            string time1 = "";
            int k = 0;
            int j = 0;
            foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
            {
                k++;
                TextBox tbx = (TextBox)Reitem.FindControl("PO_CGTIMERQ");
                time1 = ((Label)Reitem.FindControl("Label1")).Text;
                time = tbx.Text;
                if (time!=""&&time1!=time)
                {
                    break;
                }
            }
            foreach (RepeaterItem Reitem in PurorderdetailRepeater.Items)
            {
                j++;
                if (j >= k)
                {
                    ((TextBox)Reitem.FindControl("PO_CGTIMERQ")).Text = time;
                    ((Label)Reitem.FindControl("Label1")).Text = time;
                }
            }
        }


        protected void btnqrfp_click(object sender, EventArgs e)
        {
            List<string> list0 = new List<string>();
            int k = 0;
            string marid = "";
            int m = 0;
            int l = 0;
            for (int i = 0; i < PurorderdetailRepeater.Items.Count; i++)
            {
                if ((PurorderdetailRepeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    k++;
                }
            }
            if (k == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选需要分配的项!')", true);
                return;
            }
            List<string> list = new List<string>();
            for (int i = 0; i < PurorderdetailRepeater.Items.Count; i++)
            {
                if ((PurorderdetailRepeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    marid = (PurorderdetailRepeater.Items[i].FindControl("PO_MARID") as System.Web.UI.WebControls.Label).Text;
                    list.Add(marid);
                }
            }
            bool a = list.TrueForAll(d => d == marid);
            if (a == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('存在不同物料!')", true);
                return;
            }
            else
            {
                double fpzsl = 0;
                double fpfzzsl = 0;
                string mxzxsl0 = "";
                string mxzxfzsl0 = "";
                double mxzxsl0num = 0;
                double mxzxfzsl0num = 0;
                for (int i = 0; i < PurorderdetailRepeater.Items.Count; i++)
                {
                    if ((PurorderdetailRepeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                    {
                        m++;
                        if (mxzxsl0 == "")
                        {
                            //需分配的总数量
                            mxzxsl0 = (PurorderdetailRepeater.Items[i].FindControl("PO_ZXNUM") as System.Web.UI.WebControls.TextBox).Text;
                            mxzxfzsl0 = (PurorderdetailRepeater.Items[i].FindControl("PO_ZXFZNUM") as System.Web.UI.WebControls.TextBox).Text;
                            mxzxsl0num = Convert.ToDouble(((System.Web.UI.WebControls.TextBox)PurorderdetailRepeater.Items[i].FindControl("PO_ZXNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.TextBox)PurorderdetailRepeater.Items[i].FindControl("PO_ZXNUM")).Text);
                            mxzxfzsl0num = Convert.ToDouble(((System.Web.UI.WebControls.TextBox)PurorderdetailRepeater.Items[i].FindControl("PO_ZXFZNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.TextBox)PurorderdetailRepeater.Items[i].FindControl("PO_ZXFZNUM")).Text);
                        }
                        string mxsl = (PurorderdetailRepeater.Items[i].FindControl("PO_QUANTITY") as System.Web.UI.WebControls.Label).Text;
                        string mxfzsl = (PurorderdetailRepeater.Items[i].FindControl("PO_FZNUM") as System.Web.UI.WebControls.Label).Text;
                        if (mxsl == "")
                        {
                            mxsl = "0";
                        }
                        if (mxfzsl == "")
                        {
                            mxfzsl = "0";
                        }
                        //计算分配比例的总数量
                        fpzsl += Convert.ToDouble(mxsl.ToString().Trim());
                        fpfzzsl += Convert.ToDouble(mxfzsl.ToString().Trim());
                    }
                }
                if (fpzsl == 0)
                {
                    fpzsl = 0.00001;
                }
                if (fpfzzsl == 0)
                {
                    fpfzzsl = 0.00001;
                }
                //计算分配比例并分配
                double num = 0;
                double numfz = 0;
                double num1 = 0;
                double num2 = 0;
                for (int i = 0; i < PurorderdetailRepeater.Items.Count; i++)
                {
                    if ((PurorderdetailRepeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                    {
                        l++;
                        string mxsl = (PurorderdetailRepeater.Items[i].FindControl("PO_QUANTITY") as System.Web.UI.WebControls.Label).Text;
                        string mxfzsl = (PurorderdetailRepeater.Items[i].FindControl("PO_FZNUM") as System.Web.UI.WebControls.Label).Text;
                        string jhgzh = (PurorderdetailRepeater.Items[i].FindControl("PO_PCODE") as System.Web.UI.WebControls.Label).Text;
                        System.Web.UI.WebControls.TextBox tbzxmxsl = (System.Web.UI.WebControls.TextBox)PurorderdetailRepeater.Items[i].FindControl("PO_ZXNUM");
                        System.Web.UI.WebControls.TextBox tbzxmxfzsl = (System.Web.UI.WebControls.TextBox)PurorderdetailRepeater.Items[i].FindControl("PO_ZXFZNUM"); 
                        if (l == m)
                        {
                            num1 = mxzxsl0num - num;
                            num2 = mxzxfzsl0num - numfz;
                        }
                        else
                        {
                            double fpl1 = (Convert.ToDouble(mxsl.ToString().Trim())) / fpzsl;
                            double fpl2 = (Convert.ToDouble(mxfzsl.ToString().Trim())) / fpfzzsl;
                            num1 = Math.Round((mxzxsl0num * fpl1),4);
                            num2 = Math.Round((mxzxfzsl0num * fpl2),4);
                            num += num1;
                            numfz += num2;
                        }
                        string sql0 = "update TBPC_PURORDERDETAIL set PO_ZXNUM='" + Math.Round(num1, 4) + "',PO_ZXFZNUM='" +
                              Math.Round(num2, 4) + "',PO_ZXNUMXJ='" + mxzxsl0 + "',PO_ZXFZNUMXJ='" + mxzxfzsl0 + "' where PO_PCODE='" + jhgzh + "'";
                        list0.Add(sql0);
                    }
                }
                DBCallCommon.ExecuteTrans(list0);
            }
            PurorderdetailRepeaterbind();
        }


        protected void btnqueren_Click(object sender, EventArgs e)
        {
            if (tbreason.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写修改原因!')", true);
                return;
            }
            //2017.5.12修改，修改发给部长，没部长发给人本人通知
            string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'";
            System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
            string lead = "";
            if (leader.Rows.Count > 0)
            {
                lead = leader.Rows[0][0].ToString();
            }
            if (!string.IsNullOrEmpty(lead))
            {
                string content = "订单号：" + LabelCode.Text.Trim() + "，修改原因及内容：" + tbreason.Text.Trim() + "。";
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "订单数据修改", content);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购部暂时无部长，请及时与负责人沟通！');", true);
                return;
            }
        }

        protected void btnquxiao_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }
    }
}
