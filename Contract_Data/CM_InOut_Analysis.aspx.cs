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
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_InOut_Analysis : System.Web.UI.Page
    {
        //收支汇总
        private double sum_xmje = 0;
        private double sum_xmdkje = 0;
        private double sum_htje = 0;
        private double sum_fkje = 0;
        private double sum_sp = 0;
        //商务汇总
        private double sw_htje = 0;
        private double sw_ykje = 0;
        private double sw_spje = 0;
        //其他汇总
        private double fb_htje = 0;
        private double fb_ykje = 0;
        private double fb_spje = 0;

        private double cg_htje = 0;
        private double cg_ykje = 0;
        private double cg_spje = 0;

        private double fy_htje = 0;
        private double fy_ykje = 0;
        private double fy_spje = 0;

        private double qt_htje = 0;
        private double qt_ykje = 0;
        private double qt_spje = 0;
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                ViewState["xmbh"] = "";                
                this.BindAllData();
            }
        }

        private void Chartdatabind()
        {

            string sqltext = "SELECT SUM(PCON_JINE) AS HTJE," +
                             " CASE PCON_FORM WHEN '0' THEN '销售合同'" +
                               " WHEN '1' THEN '委外合同'" +
                               " WHEN '2' THEN '采购合同'" +
                               " WHEN '3' THEN '运输合同'" +
                               " WHEN '4' THEN '其他合同'" +
                            " END" +
                            " AS HTLX FROM" +
                            " TBPM_CONPCHSINFO " +
                            " WHERE PCON_PJID ='"+tb_pjid.Text.Trim()+"'" +
                            " GROUP BY PCON_FORM";


            DataTable dt = new DataTable();
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = new DataView(dt);
            Chart1.Series["收支对比"].Points.DataBindXY(dv, "HTLX", dv, "HTJE");
            Chart1.Series["收支对比"].Label = "#VALY{C}";
           
        }

        /**************************************************/       

        private void BindAllData()
        {
            this.BindSummaryDetail();
            this.bindHT();
        }
        /// <summary>
        /// 合同汇总数据
        /// </summary>
        private void BindSummaryDetail()
        {
            string sqlstr1 = "";
            string sqlstr2 = "";
            string sqlstr3 = "";
            
            
            DataTable dt = new DataTable();
            dt.Columns.Add("xmbh", typeof(string));//项目编号
            dt.Columns.Add("xmmc", typeof(string));//项目名称
            dt.Columns.Add("xmje", typeof(decimal));//项目金额
            dt.Columns.Add("xmdkje", typeof(decimal));//到款金额
            dt.Columns.Add("htje", typeof(decimal));//合同金额
            dt.Columns.Add("yzfje", typeof(decimal));//已付款金额
            dt.Columns.Add("spje", typeof(decimal));//索赔金额

           
            #region  收支汇总
                string xmbh = tb_pjid.Text.Trim();//项目编号
                //收入
                sqlstr1 = "select DISTINCT([PCON_PJID]+'/'+[PCON_PJNAME]) AS XMMC,PCON_PJID AS XMBH,SUM(PCON_JINE) AS XMJE,SUM(PCON_YFK) AS XMYFJE FROM TBPM_CONPCHSINFO WHERE PCON_PJID ='" + xmbh + "' AND PCON_FORM=0 GROUP BY PCON_PJNAME,PCON_PJID";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlstr1);

                //支出
                sqlstr2 = "select DISTINCT([PCON_PJID]+'/'+[PCON_PJNAME]) AS XMMC,SUM(PCON_JINE) AS HTJE,SUM(PCON_YFK) AS HTYFJE FROM TBPM_CONPCHSINFO WHERE PCON_PJID ='" + xmbh + "' AND PCON_FORM!=0 GROUP BY PCON_PJNAME,PCON_PJID";
                //索赔
                sqlstr3 = "select SUM(PCON_SPJE) AS SPJE FROM TBPM_CONPCHSINFO WHERE PCON_PJID ='" + xmbh + "' GROUP BY PCON_PJNAME,PCON_PJID ";
                //三表合一
                DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqlstr2);
                DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sqlstr3);

                if (dt1.Rows.Count > 0||dt2.Rows.Count>0)
                {
                    string xmmc = "";
                    decimal xmje = 0;
                    decimal xmdkje = 0;
                    decimal htje = 0;
                    decimal yzfje = 0;                    
                    
                    if (dt1.Rows.Count > 0)
                    {
                        xmmc = dt1.Rows[0]["XMMC"].ToString();
                        xmje = Convert.ToDecimal(dt1.Rows[0]["XMJE"].ToString());
                        xmdkje = Convert.ToDecimal(dt1.Rows[0]["XMYFJE"].ToString());
                    }
                    if (dt2.Rows.Count > 0)
                    {
                        xmmc = dt2.Rows[0]["XMMC"].ToString();
                        htje = Convert.ToDecimal(dt2.Rows[0]["HTJE"].ToString());
                        yzfje = Convert.ToDecimal(dt2.Rows[0]["HTYFJE"].ToString());
                    }
                       
                    DataRow newrow;
                    newrow = dt.NewRow();
                    newrow["xmmc"] = xmmc;
                    newrow["xmje"] = xmje;
                    newrow["xmdkje"] = xmdkje;
                    newrow["htje"] = htje;
                    newrow["yzfje"] = yzfje;
                    if (dt3.Rows.Count > 0)
                    {
                        newrow["spje"] = Convert.ToDecimal(dt3.Rows[0]["SPJE"].ToString());
                    }
                    else
                    {
                        newrow["spje"] = 0;
                    }
                    dt.Rows.Add(newrow);
                    
                }
                else
                {
                    dt.Rows.Clear();
                }
                #endregion
            
            //*********************************汇总信息**********************************
            if (dt.Rows.Count > 0)
            {
                grvTJ.DataSource = dt;
                grvTJ.DataBind();                
            }
            else
            {
                grvTJ.DataSource = null;
                grvTJ.DataBind();
            }
        }


        protected void lkb_confirm_Click(object sender, EventArgs e)
        {            
            this.BindAllData();
            Chartdatabind();
        }


        protected void grvTJ_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:

                    //总表头                
                    TableCellCollection tcHeader = e.Row.Cells;
                    tcHeader.Clear();
                    //第一行表头
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[0].Attributes.Add("rowspan", "2");
                    tcHeader[0].Text = "序号";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[1].Attributes.Add("rowspan", "2");
                    tcHeader[1].Text = "项目名称";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[2].Attributes.Add("colspan", "3");
                    tcHeader[2].Text = "收入";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[3].Attributes.Add("colspan", "3");
                    tcHeader[3].Text = "支出";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[4].Attributes.Add("rowspan", "2");
                    tcHeader[4].Text = "索赔金额</th></tr><tr>";
                    //tcHeader.Add(new TableHeaderCell());
                    //tcHeader[5].Attributes.Add("rowspan", "2");
                    //tcHeader[5].Text = "详细</th></tr><tr>";
                    //第二行表头
                    //<HeaderStyle BackColor="" Font-Bold="True" ForeColor="White" />
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[5].Attributes.Add("bgcolor", "#A8B7EC");
                    tcHeader[5].Attributes.Add("color", "White");
                    tcHeader[5].Text = "项目金额";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[6].Attributes.Add("bgcolor", "#A8B7EC");
                    tcHeader[6].Attributes.Add("color", "White");
                    tcHeader[6].Text = "到款金额";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[7].Attributes.Add("bgcolor", "#A8B7EC");
                    tcHeader[7].Attributes.Add("color", "White");
                    tcHeader[7].Text = "收款比例";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[8].Attributes.Add("bgcolor", "#A8B7EC");
                    tcHeader[8].Attributes.Add("color", "Red");
                    tcHeader[8].Text = "合同金额";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[9].Attributes.Add("bgcolor", "#A8B7EC");
                    tcHeader[9].Attributes.Add("color", "Red");
                    tcHeader[9].Text = "支付金额";
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[10].Attributes.Add("bgcolor", "#A8B7EC");
                    tcHeader[10].Attributes.Add("color", "White");
                    tcHeader[10].Text = "付款比例";
                    break;
            }
        }

        //收支汇总
        protected void grvTJ_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowIndex >= 0)
            {
                sum_xmje += Convert.ToDouble(e.Row.Cells[2].Text);//项目金额
                sum_xmdkje += Convert.ToDouble(e.Row.Cells[3].Text);//项目到款金额
                sum_htje += Convert.ToDouble(e.Row.Cells[5].Text);//合同金额
                sum_fkje += Convert.ToDouble(e.Row.Cells[6].Text);//付款jine
                sum_sp += Convert.ToDouble(e.Row.Cells[8].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "汇总：";
                e.Row.Cells[2].Text = string.Format("{0:c}", sum_xmje);
                e.Row.Cells[3].Text = string.Format("{0:c}", sum_xmdkje);
                e.Row.Cells[4].Text = "合同待收:" + string.Format("{0:c}", sum_xmje - sum_xmdkje);
                e.Row.Cells[5].Text = string.Format("{0:c}", sum_htje);
                e.Row.Cells[6].Text = string.Format("{0:c}", sum_fkje);
                e.Row.Cells[7].Text = "合同待支:" + string.Format("{0:c}", sum_htje - sum_fkje);
                e.Row.Cells[8].Text = "已支付索赔:" + string.Format("{0:c}", sum_sp);
            }

            //遍历所有行设置边框样式
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-color:Black";
            }
        }        

        //商务合同汇总
        protected void grvSW_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sw_htje += Convert.ToDouble(e.Row.Cells[4].Text);//合同金额
                sw_ykje += Convert.ToDouble(e.Row.Cells[5].Text);//到款金额
                sw_spje += Convert.ToDouble(e.Row.Cells[7].Text);//索赔金额
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "汇总：";
                e.Row.Cells[4].Text = string.Format("{0:c}", sw_htje);
                e.Row.Cells[5].Text = string.Format("{0:c}", sw_ykje);
                e.Row.Cells[6].Text = string.Format("{0:0.00%}", sw_ykje / sw_htje);
                e.Row.Cells[7].Text = string.Format("{0:c}", sw_spje);
            }
        }

        //委外合同汇总
        protected void grvFB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                fb_htje += Convert.ToDouble(e.Row.Cells[4].Text);//合同金额
                fb_ykje += Convert.ToDouble(e.Row.Cells[5].Text);//到款金额
                fb_spje += Convert.ToDouble(e.Row.Cells[7].Text);//索赔金额
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "汇总：";
                e.Row.Cells[4].Text = string.Format("{0:c}", fb_htje);
                e.Row.Cells[5].Text = string.Format("{0:c}", fb_ykje);
                e.Row.Cells[6].Text = string.Format("{0:0.00%}", fb_ykje / fb_htje);
                e.Row.Cells[7].Text = string.Format("{0:c}", fb_spje);
            }
        }

        //采购合同汇总
        protected void grvCG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                cg_htje += Convert.ToDouble(e.Row.Cells[4].Text);//合同金额
                cg_ykje += Convert.ToDouble(e.Row.Cells[5].Text);//到款金额
                cg_spje += Convert.ToDouble(e.Row.Cells[7].Text);//索赔金额
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "汇总：";
                e.Row.Cells[4].Text = string.Format("{0:c}", cg_htje);
                e.Row.Cells[5].Text = string.Format("{0:c}", cg_ykje);
                e.Row.Cells[6].Text = string.Format("{0:0.00%}", cg_ykje / cg_htje);
                e.Row.Cells[7].Text = string.Format("{0:c}", cg_spje);
            }
        }

        //发运合同汇总
        protected void grvFY_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                fy_htje += Convert.ToDouble(e.Row.Cells[4].Text);//合同金额
                fy_ykje += Convert.ToDouble(e.Row.Cells[5].Text);//到款金额
                fy_spje += Convert.ToDouble(e.Row.Cells[7].Text);//索赔金额
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "汇总：";
                e.Row.Cells[4].Text = string.Format("{0:c}", fy_htje);
                e.Row.Cells[5].Text = string.Format("{0:c}", fy_ykje);
                e.Row.Cells[6].Text = string.Format("{0:0.00%}", fy_ykje / fy_htje);
                e.Row.Cells[7].Text = string.Format("{0:c}", fy_spje);
            }
        }

        //其他合同汇总
        protected void grvQT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                qt_htje += Convert.ToDouble(e.Row.Cells[4].Text);//合同金额
                qt_ykje += Convert.ToDouble(e.Row.Cells[5].Text);//到款金额
                qt_spje += Convert.ToDouble(e.Row.Cells[7].Text);//索赔金额
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "汇总：";
                e.Row.Cells[4].Text = string.Format("{0:c}", qt_htje);
                e.Row.Cells[5].Text = string.Format("{0:c}", qt_ykje);
                e.Row.Cells[6].Text = string.Format("{0:0.00%}", qt_ykje / qt_htje);
                e.Row.Cells[7].Text = string.Format("{0:c}", qt_spje);
            }
        }        

        //绑定销售合同和其他合同信息
        private void bindHT()
        {
            BindHTDetail(0, grvSW);
            BindHTDetail(1, grvFB);
            BindHTDetail(2, grvCG);
            BindHTDetail(3, grvFY);
            BindHTDetail(4, grvQT);
        }        

        private void BindHTDetail(int pcon_form,GridView gv)
        {
            //绑定合同信息
            string sql = "select [PCON_BCODE],[PCON_NAME],[PCON_STATE],[PCON_PJID]+'/'+[PCON_PJNAME] AS PCON_PJNAME ,[PCON_JINE] ,[PCON_FORM] ,[PCON_YFK] ,[PCON_ERROR],[PCON_SPJE] from TBPM_CONPCHSINFO where PCON_FORM="+pcon_form+" "+
                " AND PCON_PJID ='" + tb_pjid.Text.Trim() + "' order by PCON_FORM,PCON_BCODE ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                gv.DataSource = dt;
                gv.DataBind();                
            }
            else
            {
                gv.DataSource = null;
                gv.DataBind();                
            }
        }

        #region  分页

        /// <summary>
        /// 重新绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnGoto_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(txt_goto.Text.ToString());
            if (index <= grvTJ.PageCount && index > 0)
            {
                this.grvTJ.PageIndex = index - 1;
                this.BindAllData();
            }
        }
        protected void lnkbtnFrist_Click(object sender, EventArgs e)
        {
            this.grvTJ.PageIndex = 0;
            this.BindAllData();
        }
        protected void lnkbtnPre_Click(object sender, EventArgs e)
        {
            if (this.grvTJ.PageIndex > 0)
            {
                this.grvTJ.PageIndex = this.grvTJ.PageIndex - 1;
                this.BindAllData();
            }
        }
        protected void lnkbtnNext_Click(object sender, EventArgs e)
        {
            if (this.grvTJ.PageIndex < this.grvTJ.PageCount)
            {
                this.grvTJ.PageIndex = this.grvTJ.PageIndex + 1;
                this.BindAllData();
            }
        }
        protected void lnkbtnLast_Click(object sender, EventArgs e)
        {
            this.grvTJ.PageIndex = this.grvTJ.PageCount;
            this.BindAllData();
        }

        #endregion

        //选定的项目发生变化时
        protected void tb_pjinfo_Textchanged(object sender, EventArgs e)
        {
            string pjname = "";
            string pjid = "";
            if (tb_pjinfo.Text.ToString().Contains("|"))
            {
                pjname = tb_pjinfo.Text.Substring(0, tb_pjinfo.Text.ToString().IndexOf("|"));
                pjid = tb_pjinfo.Text.Substring(tb_pjinfo.Text.ToString().IndexOf("|") + 1);
                tb_pjinfo.Text = pjname;
                tb_pjid.Text = pjid;

                this.BindAllData();
                Chartdatabind();
            }
            else
            {
                tb_pjinfo.Text = "";
                tb_pjid.Text = "";
               
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写项目！');", true); return;
            }

        }

    }
}
