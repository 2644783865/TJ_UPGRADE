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
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_KCYECX_Detail : System.Web.UI.Page
    {
        string engid = string.Empty;
        string a = string.Empty;
        string b = string.Empty;
        string c = string.Empty;



        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["id"] != "") && (Request.QueryString["time"] != ""))
            {
                engid = Request.QueryString["id"];
                a = Request.QueryString["year"];
                b = Request.QueryString["period"];
                c = a + '-' + b;

            }
            if (!IsPostBack)
            {
                bindlabel();
                binddate();
                bindBasicInfo(engid);
            }
        }
        private void bindlabel()
        {
            begdate.Text = c;
            enddate.Text = c;
            string sql1 = "Select SI_BEGNUM,SI_BEGBAL,SI_ENDNUM,SI_ENDBAL,MNAME from dbo.View_STORAGEBAL_MAR where SI_MARID='" + engid + "'and SI_YEAR+'-'+SI_PERIOD like '%" + c + "%'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);



            MNAME.Text = dt1.Rows[0][4].ToString();
        }
        private void bindBasicInfo(string mid)
        {
            string sql5 = "select '期初' as ID,'' as CODE,'' as SI_MARID,'' as SI_CHECKDATE, ''as BILLTYPE,SI_BEGNUM ,SI_BEGBAL from View_STORAGEBAL_MAR where SI_MARID='" + engid + "' and SI_YEAR+'-'+SI_PERIOD like '%" + c + "%' ";
            DataTable dt5 = DBCallCommon.GetDTUsingSqlText(sql5);
            this.GridView5.DataSource = dt5;
            this.GridView5.DataBind();

            string sql6 = "select '期末' as ID,'' as CODE,'' as SI_MARID,'' as SI_CHECKDATE, ''as BILLTYPE,SI_ENDNUM ,SI_ENDBAL from View_STORAGEBAL_MAR where SI_MARID='" + engid + "' and SI_YEAR+'-'+SI_PERIOD like '%" + c + "%' ";
            DataTable dt6 = DBCallCommon.GetDTUsingSqlText(sql6);
            this.GridView6.DataSource = dt6;
            this.GridView6.DataBind();

            string sql1 = "select '入库' as WG_ID,WG_CODE,WG_MARID,WG_VERIFYDATE, WG_BILLTYPE,WG_RSNUM,WG_AMOUNT from dbo.View_SM_IN where WG_MARID='" + mid + "' and WG_VERIFYDATE like '%" + c + "%'and WG_STATE='2' and (WG_BILLTYPE='0' or WG_BILLTYPE='1')";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt1.Rows.Count > 0)
            {
                this.GridView1.DataSource = dt1;
                this.GridView1.DataBind();
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    if (gr.Cells[5].Text.ToString() == "0")
                    {
                        gr.Cells[5].Text = "采购入库";
                    }
                    else
                    {
                        gr.Cells[5].Text = "结转备库";
                    }
                }



            }
            else
            {

                DataRow newrow = dt1.NewRow();
                newrow[0] = "入库";
                newrow[1] = "";
                newrow[2] = "";
                newrow[3] = "";
                newrow[4] = "";
                newrow[5] = Convert.ToDecimal("0");
                newrow[6] = Convert.ToDecimal("0");
                dt1.Rows.Add(newrow);

                this.GridView1.DataSource = dt1;
                this.GridView1.DataBind();

            }


            string sql2 = "select '出库' as OP_ID,OutCode,MaterialCode,ApprovedDate,BillType,RealNumber,Amount from dbo.View_SM_OUT where MaterialCode='" + mid + "' and ApprovedDate like '%" + c + "%'and TotalState='2' and (BillType='1' or BillType='2') ";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
            this.GridView2.DataSource = dt2;
            this.GridView2.DataBind();
            if (dt2.Rows.Count > 0)
            {
                foreach (GridViewRow gr in GridView2.Rows)
                {
                    if (gr.Cells[5].Text.ToString() == "1")
                    {
                        gr.Cells[5].Text = "生产领料";
                    }
                    else
                    {
                        gr.Cells[5].Text = "委外出库";
                    }
                }



            }
            else
            {

                DataRow newrow = dt2.NewRow();
                newrow[0] = "出库";
                newrow[1] = "";
                newrow[2] = "";
                newrow[3] = "";
                newrow[4] = "";
                newrow[5] = Convert.ToDecimal("0");
                newrow[6] = Convert.ToDecimal("0");
                dt2.Rows.Add(newrow);

                this.GridView2.DataSource = dt2;
                this.GridView2.DataBind();


            }

            string sql3 = "select '入库' as ID,SI_YEAR+SI_PERIOD+'2' as CODE,SI_MARID,SI_CHECKDATE, '入库调整单'as BILLTYPE,SI_CRCVNUM , isnull(SI_CRVDIFF,0) as SI_CRVDIFF from View_STORAGEBAL_MAR where SI_MARID='" + engid + "' and SI_YEAR+'-'+SI_PERIOD like '%" + c + "%' and SI_PTCODE='1' ";
            DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql3);
            this.GridView3.DataSource = dt3;
            this.GridView3.DataBind();
            if (dt3.Rows.Count > 0)
            {



            }
            else
            {

                DataRow newrow = dt3.NewRow();
                newrow[0] = "入库";
                newrow[1] = "";
                newrow[2] = "";
                newrow[3] = "";
                newrow[4] = "入库调整单";
                newrow[5] = Convert.ToDecimal("0");
                newrow[6] = Convert.ToDecimal("0");
                dt3.Rows.Add(newrow);

                this.GridView3.DataSource = dt3;
                this.GridView3.DataBind();

            }

            string sql4 = "select '期初' as ID,SI_YEAR+SI_PERIOD+'1' as CODE,SI_MARID,SI_CHECKDATE, '期初调整单'as BILLTYPE,SI_BEGNUM ,SI_BEGDIFF from View_STORAGEBAL_MAR where SI_MARID='" + engid + "' and SI_YEAR+'-'+SI_PERIOD like '%" + c + "%' and SI_BEGNUM=0 and isnull(SI_BEGDIFF,0)<>0 ";
            DataTable dt4 = DBCallCommon.GetDTUsingSqlText(sql4);
            this.GridView4.DataSource = dt4;
            this.GridView4.DataBind();
            if (dt4.Rows.Count > 0)
            {



            }
            else
            {

                DataRow newrow = dt4.NewRow();
                newrow[0] = "期初";
                newrow[1] = "";
                newrow[2] = "";
                newrow[3] = "";
                newrow[4] = "期初调整单";
                newrow[5] = Convert.ToDecimal("0");
                newrow[6] = Convert.ToDecimal("0");
                dt4.Rows.Add(newrow);

                this.GridView4.DataSource = dt4;
                this.GridView4.DataBind();
            }


        }

        private void binddate()
        {
            string sql1 = "select  isnull(sum(WG_AMOUNT),0) as TotalAmount,isnull(sum(WG_RSNUM),0) as TotalNum from View_SM_IN where WG_MARID='" + engid + "' and WG_VERIFYDATE like '%" + c + "%'and WG_STATE='2' and (WG_BILLTYPE='0' or WG_BILLTYPE='1')";

            SqlDataReader sdr1 = DBCallCommon.GetDRUsingSqlText(sql1);

            if (sdr1.Read())
            {
                hfdinTotalAmount.Value = sdr1["TotalAmount"].ToString();
                hfdinTotalNum.Value = sdr1["TotalNum"].ToString();
            }
            string sql2 = "select  isnull(sum(AMOUNT),0) as TotalAmount1,isnull(sum(RealNumber),0) as TotalNum1 from dbo.View_SM_OUT where MaterialCode='" + engid + "' and ApprovedDate like '%" + c + "%'and TotalState='2' and  (BillType='1' or BillType='2') ";
            SqlDataReader sdr2 = DBCallCommon.GetDRUsingSqlText(sql2);

            if (sdr2.Read())
            {
                hfdoutTotalAmount.Value = sdr2["TotalAmount1"].ToString();
                hfdoutTotalNum.Value = sdr2["TotalNum1"].ToString();
            }

            string sql3 = "select  isnull(sum(SI_CRCVNUM),0) as TotalAmount1,isnull(sum(SI_CRVDIFF),0) as TotalNum1 from View_STORAGEBAL_MAR where SI_MARID='" + engid + "' and SI_YEAR+'-'+SI_PERIOD like '%" + c + "%'and SI_PTCODE='1'";
            SqlDataReader sdr3 = DBCallCommon.GetDRUsingSqlText(sql3);

            if (sdr3.Read())
            {
                hfdintzTotalAmount.Value = sdr3["TotalAmount1"].ToString();
                hfdintzTotalNum.Value = sdr3["TotalNum1"].ToString();
            }

            string sql4 = "select  isnull(sum(SI_BEGNUM),0) as TotalAmount1,isnull(sum(SI_BEGDIFF),0) as TotalNum1 from View_STORAGEBAL_MAR where SI_MARID='" + engid + "' and SI_YEAR+'-'+SI_PERIOD like '%" + c + "%'and SI_BEGNUM=0 and isnull(SI_BEGDIFF,0)<>0";
            SqlDataReader sdr4 = DBCallCommon.GetDRUsingSqlText(sql4);

            if (sdr4.Read())
            {
                hfdbegtzTotalAmount.Value = sdr4["TotalAmount1"].ToString();
                hfdbegtzTotalNum.Value = sdr4["TotalNum1"].ToString();
            }

        }






        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
            }
            else if (e.Row.RowType == DataControlRowType.Footer)//表尾加合计
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].Text = hfdinTotalNum.Value;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].Text = hfdinTotalAmount.Value;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;

            }

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
            }
            else if (e.Row.RowType == DataControlRowType.Footer)//表尾加合计
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].Text = hfdoutTotalNum.Value;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].Text = hfdoutTotalAmount.Value;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;

            }
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
            }
            else if (e.Row.RowType == DataControlRowType.Footer)//表尾加合计
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].Text = hfdintzTotalAmount.Value;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].Text = hfdintzTotalNum.Value;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;

            }
        }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
            }
            else if (e.Row.RowType == DataControlRowType.Footer)//表尾加合计
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].Text = hfdbegtzTotalAmount.Value;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].Text = hfdbegtzTotalNum.Value;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;

            }
        }

        protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
            }
        }


        protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
            }
        }
    }
    
}
