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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_InOutRecord : System.Web.UI.Page
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
        private double qt_htje = 0;
        private double qt_ykje = 0;
        private double qt_spje = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["xmbh"] = "";
                this.BindPrjName();
                this.BindAllData();
            }
        }

        /// <summary>
        /// 绑定项目名称
        /// </summary>
        private void BindPrjName()
        {
            string sqlText = "select PJ_ID+'/'+PJ_NAME as PJ_NAME,PJ_ID from TBPM_PJINFO";//随着项目的增多，下拉框数据多，考虑将项目是否完工加入查询条件
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            dplXMMC.DataSource = dt;
            dplXMMC.DataTextField = "PJ_NAME";
            dplXMMC.DataValueField = "PJ_ID";
            dplXMMC.DataBind();
            dplXMMC.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplXMMC.SelectedIndex = 0;
        }

        //两种查询方式
        protected void rblHZFS_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvTJ.DataSource = null;
            grvTJ.DataBind();
            
            bool checkState = (rblHZFS.SelectedIndex == 0 ? true : false);
            if (checkState)//按项目查询
            {
                palSJ.Visible = false;
                palXM.Visible = true;
                txtSJ1.Text = "";
                txtSJ2.Text = "";

            }
            else//时间区间
            {
                palXM.Visible = false;
                palSJ.Visible = true;
                dplXMMC.SelectedIndex = 0;

            }
            this.BindAllData();
        }

        private void BindAllData()
        {
            this.BindSummaryDetail();
        }
        /// <summary>
        /// 合同汇总数据
        /// </summary>
        private void BindSummaryDetail()
        {
            string sqlstr1 = "";
            string sqlstr2 = "";
            string sqlstr3 = "";

            ArrayList xmbh_array = new ArrayList();//要查询的项目编号，便于后续项目商务合同及其他合同的查询
            bool checkState = (rblHZFS.SelectedIndex == 0 ? true : false);
            DataTable dt = new DataTable();
            dt.Columns.Add("xmbh", typeof(string));//项目编号
            dt.Columns.Add("xmmc", typeof(string));//项目名称
            dt.Columns.Add("xmje", typeof(decimal));//项目金额
            dt.Columns.Add("xmdkje", typeof(decimal));//到款金额
            dt.Columns.Add("htje", typeof(decimal));//合同金额
            dt.Columns.Add("yzfje", typeof(decimal));//已付款金额
            dt.Columns.Add("spje", typeof(decimal));//索赔金额

            if (checkState)//按项目查询
            {
                #region
                string xhbh = dplXMMC.SelectedValue.ToString();//项目编号
                //收入
                sqlstr1 = "select DISTINCT(PCON_PJNAME) AS XMMC,PCON_PJID AS XMBH,SUM(PCON_JINE) AS XMJE,SUM(PCON_YFK) AS XMYFJE FROM TBPM_CONPCHSINFO WHERE PCON_PJID like'" + xhbh + "' AND PCON_FORM=0 GROUP BY PCON_PJNAME,PCON_PJID";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlstr1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        xmbh_array.Add(dt1.Rows[i]["XMBH"].ToString());
                        //支出
                        sqlstr2 = "select SUM(PCON_JINE) AS HTJE,SUM(PCON_YFK) AS HTYFJE FROM TBPM_CONPCHSINFO WHERE PCON_PJID like'" + dt1.Rows[i]["XMBH"].ToString() + "' AND PCON_FORM!=0 GROUP BY PCON_PJNAME";
                        //索赔
                        sqlstr3 = "select SUM(PCON_SPJE) AS SPJE FROM TBPM_CONPCHSINFO WHERE PCON_PJID like'" + dt1.Rows[i]["XMBH"].ToString() + "' GROUP BY PCON_PJNAME,PCON_PJID ";
                        //三表合一
                        DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqlstr2);
                        DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sqlstr3);
                        if (dt2.Rows.Count > 0)
                        {
                            DataRow newrow;
                            newrow = dt.NewRow();
                            newrow["xmbh"] = dt1.Rows[i]["XMBH"].ToString();
                            newrow["xmmc"] = dt1.Rows[i]["XMMC"].ToString();
                            newrow["xmje"] = Convert.ToDecimal(dt1.Rows[i]["XMJE"].ToString());
                            newrow["xmdkje"] = Convert.ToDecimal(dt1.Rows[i]["XMYFJE"].ToString());
                            newrow["htje"] = Convert.ToDecimal(dt2.Rows[0]["HTJE"].ToString());
                            newrow["yzfje"] = Convert.ToDecimal(dt2.Rows[0]["HTYFJE"].ToString());
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
                        else if (dt2.Rows.Count == 0)//已建立【商务合同】=项目，但该项目下无其他合同
                        {
                            DataRow newrow;
                            newrow = dt.NewRow();
                            newrow["xmbh"] = dt1.Rows[i]["XMBH"].ToString();
                            newrow["xmmc"] = dt1.Rows[i]["XMMC"].ToString();
                            newrow["xmje"] = Convert.ToDecimal(dt1.Rows[i]["XMJE"].ToString());
                            newrow["xmdkje"] = Convert.ToDecimal(dt1.Rows[i]["XMYFJE"].ToString());
                            newrow["htje"] = 0;
                            newrow["yzfje"] = 0;
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
                    }
                }
                else
                {
                    dt.Rows.Clear();
                }
                #endregion
            }
            else//按起止时间查询
            {
                #region
                DateTime dtime1 = Convert.ToDateTime(txtSJ1.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : txtSJ1.Text.Trim());
                DateTime dtime2 = Convert.ToDateTime(txtSJ2.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : txtSJ2.Text.Trim());
                //收入
                sqlstr1 = "select PCON_PJNAME AS XMMC,PCON_PJID AS XMBH,SUM(PCON_JINE) AS XMJE,SUM(PCON_YFK) AS XMYFJE FROM TBPM_CONPCHSINFO WHERE PCON_FORM=0 AND PCON_VALIDDATE>'" + dtime1 + "' AND PCON_VALIDDATE<'" + dtime2 + "'  GROUP BY PCON_PJNAME,PCON_PJID ORDER BY XMMC";
                DataTable dt_sr = DBCallCommon.GetDTUsingSqlText(sqlstr1);
                if (dt_sr.Rows.Count > 0)//如果有相关商务合同记录
                {
                    for (int i = 0; i < dt_sr.Rows.Count; i++)//对于其中的每一条记录，找出其对应支出与索赔总和
                    {
                        xmbh_array.Add(dt_sr.Rows[i]["XMBH"].ToString());
                        //支出_某项目下对应的支出
                        sqlstr2 = "select SUM(PCON_JINE) AS HTJE,SUM(PCON_YFK) AS HTYFJE FROM TBPM_CONPCHSINFO WHERE  PCON_FORM!=0 AND PCON_VALIDDATE>'" + dtime1 + "' AND PCON_VALIDDATE<'" + dtime2 + "' AND PCON_PJID='" + dt_sr.Rows[i]["XMBH"].ToString() + "' GROUP BY PCON_PJNAME";
                        DataTable dt_zc = DBCallCommon.GetDTUsingSqlText(sqlstr2);
                        //索赔_某项目下的索赔
                        sqlstr3 = "select SUM(PCON_SPJE) AS SPJE FROM TBPM_CONPCHSINFO WHERE PCON_VALIDDATE>'" + dtime1 + "' AND PCON_VALIDDATE<'" + dtime2 + "' AND PCON_PJID='" + dt_sr.Rows[i]["XMBH"].ToString() + "'";
                        DataTable dt_sp = DBCallCommon.GetDTUsingSqlText(sqlstr3);
                        //三表合一

                        //分为支出项数据为空和不为空的情况
                        if (dt_zc.Rows.Count > 0)
                        {
                            DataRow newrow;
                            newrow = dt.NewRow();
                            newrow["xmbh"] = dt_sr.Rows[i]["XMBH"].ToString();
                            newrow["xmmc"] = dt_sr.Rows[i]["XMMC"].ToString();
                            newrow["xmje"] = Convert.ToDecimal(dt_sr.Rows[i]["XMJE"].ToString());
                            newrow["xmdkje"] = Convert.ToDecimal(dt_sr.Rows[i]["XMYFJE"].ToString());
                            newrow["htje"] = Convert.ToDecimal(dt_zc.Rows[0]["HTJE"].ToString());
                            newrow["yzfje"] = Convert.ToDecimal(dt_zc.Rows[0]["HTYFJE"].ToString());
                            if (dt_sp.Rows.Count > 0)
                            {
                                newrow["spje"] = Convert.ToDecimal(dt_sp.Rows[0]["SPJE"].ToString());
                            }
                            else
                            {
                                newrow["spje"] = 0;
                            }
                            dt.Rows.Add(newrow);
                        }
                        else if (dt_zc.Rows.Count == 0)//已建立【商务合同】=项目，但该项目下无其他合同
                        {
                            DataRow newrow;
                            newrow = dt.NewRow();
                            newrow["xmbh"] = dt_sr.Rows[i]["XMBH"].ToString();
                            newrow["xmmc"] = dt_sr.Rows[i]["XMMC"].ToString();
                            newrow["xmje"] = Convert.ToDecimal(dt_sr.Rows[i]["XMJE"].ToString());
                            newrow["xmdkje"] = Convert.ToDecimal(dt_sr.Rows[i]["XMYFJE"].ToString());
                            newrow["htje"] = 0;
                            newrow["yzfje"] = 0;
                            if (dt_sp.Rows.Count > 0)
                            {
                                newrow["spje"] = Convert.ToDecimal(dt_sp.Rows[0]["SPJE"].ToString());
                            }
                            else
                            {
                                newrow["spje"] = 0;
                            }
                            dt.Rows.Add(newrow);
                        }
                    }
                }
                else
                {
                    dt.Rows.Clear();
                }
                #endregion
            }
            //*********************************汇总信息**********************************
            if (dt.Rows.Count > 0)
            {
                grvTJ.DataSource = dt;
                grvTJ.DataBind();
                palHZ.Visible = false;
            }
            else
            {
                grvTJ.DataSource = null;
                grvTJ.DataBind();
                palHZ.Visible = true;
            }
                        
            if (this.grvTJ.PageCount > 0)
            {
                this.txt_goto.Text = this.lbl_currentpage.Text = (this.grvTJ.PageIndex + 1).ToString();
                this.lbl_totalpage.Text = this.grvTJ.PageCount.ToString();
                Pal_page.Visible = true;
                if (this.grvTJ.PageIndex == 0)
                {
                    this.lnkbtnFrist.Enabled = false;
                    this.lnkbtnPre.Enabled = false;
                }
                else
                {
                    this.lnkbtnFrist.Enabled = true;
                    this.lnkbtnPre.Enabled = true;
                }
                if (this.grvTJ.PageIndex == this.grvTJ.PageCount - 1)
                {
                    this.lnkbtnNext.Enabled = false;
                    this.lnkbtnLast.Enabled = false;
                }
                else
                {
                    this.lnkbtnNext.Enabled = true;
                    this.lnkbtnLast.Enabled = true;
                }
            }
            else
            {
                Pal_page.Visible = false;
            }

            //********************************合同信息*********************************************
            //string xmbh = "";
            //if (xmbh_array.Count > 0)//按时间查询存在项目编号
            //{
            //    for (int j = 0; j < xmbh_array.Count; j++)
            //    {
            //        xmbh += "'" + xmbh_array[j] + "'" + ",";
            //    }
            //    ViewState["xmbh"] = xmbh.TrimEnd(',');
            //    bindSWQT();
               
            //}
            //else
            //{
            //    grvSW.DataSource = null;
            //    grvSW.DataBind();
            //    palSW.Visible = true;

            //    grvQT.DataSource = null;
            //    grvQT.DataBind();
            //    palQT.Visible = true;
            //}
        }
        protected void dplXMMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindAllData();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            dplXMMC.SelectedIndex = 0;
            this.BindAllData();
        }


        protected void grvTJ_RowCreated1(object sender, GridViewRowEventArgs e)
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
                sum_xmdkje+=Convert.ToDouble(e.Row.Cells[3].Text);//项目到款金额
                sum_htje+=Convert.ToDouble(e.Row.Cells[5].Text);//合同金额
                sum_fkje+=Convert.ToDouble(e.Row.Cells[6].Text);//付款jine
                sum_sp += Convert.ToDouble(e.Row.Cells[8].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "汇总：";
                e.Row.Cells[2].Text =string.Format("{0:c}",sum_xmje);
                e.Row.Cells[3].Text = string.Format("{0:c}", sum_xmdkje);
                e.Row.Cells[4].Text ="合同待收:"+string.Format("{0:c}", sum_xmje-sum_xmdkje);
                e.Row.Cells[5].Text = string.Format("{0:c}", sum_htje);
                e.Row.Cells[6].Text=string.Format("{0:c}",sum_fkje);
                e.Row.Cells[7].Text = "合同待支:"+string.Format("{0:c}",sum_htje-sum_fkje);
                e.Row.Cells[8].Text = "已支付索赔:"+string.Format("{0:c}", sum_sp);
            }


            //遍历所有行设置边框样式
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-color:Black";
            }

            

        }

        protected void grvTJ_DataBound(object sender, EventArgs e)
        {
            ArrayList xmbh_array = new ArrayList();//要查询的项目编号，便于后续项目商务合同及其他合同的查询
            foreach(GridViewRow gr in grvTJ.Rows)
            {
                Label lb_xmbh = (Label)gr.FindControl("lbl_xmbh");
                xmbh_array.Add(lb_xmbh.Text.ToString());
                
            }
            string xmbh = "";
            if (xmbh_array.Count > 0)//按时间查询存在项目编号
            {
                for (int j = 0; j < xmbh_array.Count; j++)
                {
                    xmbh += "'" + xmbh_array[j] + "'" + ",";
                }
                ViewState["xmbh"] = xmbh.TrimEnd(',');
                bindSWQT();

            }
            else
            {
                grvSW.DataSource = null;
                grvSW.DataBind();
                palSW.Visible = true;

                grvQT.DataSource = null;
                grvQT.DataBind();
                palQT.Visible = true;
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
                e.Row.Cells[6].Text = string.Format("{0:0.00%}",sw_ykje/sw_htje);
                e.Row.Cells[7].Text = string.Format("{0:c}", sw_spje);
            }
        }
        //其他合同汇总
        protected void grvQT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                qt_htje += Convert.ToDouble(e.Row.Cells[5].Text);//合同金额
                qt_ykje += Convert.ToDouble(e.Row.Cells[6].Text);//到款金额
                qt_spje += Convert.ToDouble(e.Row.Cells[8].Text);//索赔金额
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "汇总：";
                e.Row.Cells[5].Text = string.Format("{0:c}", qt_htje);
                e.Row.Cells[6].Text = string.Format("{0:c}", qt_ykje);
                e.Row.Cells[7].Text = string.Format("{0:0.00%}", qt_ykje/qt_htje);
                e.Row.Cells[8].Text = string.Format("{0:c}", qt_spje);
            }
        }

        //绑定销售合同和其他合同信息
        private void bindSWQT()
        {
            //商务合同信息
            string sqlsw = "select [PCON_BCODE],[PCON_NAME],[PCON_STATE],[PCON_PJNAME] ,[PCON_JINE] ,[PCON_FORM] ,[PCON_YFK] ,[PCON_ERROR],[PCON_SPJE] from TBPM_CONPCHSINFO where PCON_FORM=0 AND PCON_PJID in(" + ViewState["xmbh"].ToString() + ") order by PCON_FORM,PCON_BCODE";
            DataTable dtsw = DBCallCommon.GetDTUsingSqlText(sqlsw);
            if (dtsw.Rows.Count > 0)
            {
                grvSW.DataSource = dtsw;
                grvSW.DataBind();
                palSW.Visible = false;
            }
            else
            {
                grvSW.DataSource = null;
                grvSW.DataBind();
                palSW.Visible = true;
            }
            //其他合同信息
            string sqlqt = "select [PCON_BCODE],[PCON_NAME],[PCON_STATE],[PCON_PJNAME] ,[PCON_JINE] ,[PCON_FORM] ,[PCON_YFK] ,[PCON_ERROR],[PCON_SPJE] from TBPM_CONPCHSINFO where PCON_FORM!=0 AND PCON_PJID in(" + ViewState["xmbh"].ToString() + ") order by PCON_FORM,PCON_BCODE ";
            DataTable dtqt = DBCallCommon.GetDTUsingSqlText(sqlqt);
            if (dtqt.Rows.Count > 0)
            {
                grvQT.DataSource = dtqt;
                grvQT.DataBind();
                palQT.Visible = false;
            }
            else
            {
                grvQT.DataSource = null;
                grvQT.DataBind();
                palQT.Visible = true;
            }
        }


        //分页
       
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

    }
}
