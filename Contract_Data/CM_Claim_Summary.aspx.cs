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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Claim_Summary : System.Web.UI.Page
    {
        decimal yz = 0;
        decimal zjyz = 0;
        decimal zjfbs = 0;
        decimal fbs = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindPjName(dplXMMC);
                this.BindHZ(0);
            }
        }
        /// <summary>
        /// 绑定项目名称
        /// </summary>
        private void BindPjName(DropDownList dpl)
        {
            string sqlText = "select PJ_ID+'/'+PJ_NAME as PJ_NAME,PJ_ID from TBPM_PJINFO";//随着项目的增多，下拉框数据多，考虑将项目是否完工加入查询条件
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            dpl.DataSource = dt;
            dpl.DataTextField = "PJ_NAME";
            dpl.DataValueField = "PJ_ID";
            dpl.DataBind();
            dpl.Items.Insert(0, new ListItem("-请选择-", "%"));
            dpl.SelectedIndex = 0;

        }

        private void BindHZ(int hzlb)
        {
            switch (hzlb)
            {
                //对于某一索赔单号，主合同中有记录的，分包合同中不一定有记录，但分包合同中有记录的，主合同中一定有记录
                case 0: this.BindDataUsingXMMC();
                    break;
                default: this.BindDataUsingSJ();
                    break;
            }
        }

        /// <summary>
        /// 根据项目编号汇总索赔数据
        /// </summary>
        private void BindDataUsingXMMC()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("XMMC", typeof(string));//项目名称
            dt.Columns.Add("SPDH", typeof(string));//索赔单号
            dt.Columns.Add("YZ", typeof(decimal));//YZ
            dt.Columns.Add("ZJYZ", typeof(decimal));//ZJYZ            
            dt.Columns.Add("ZJFBS", typeof(decimal));//ZJFBS
            dt.Columns.Add("FBS", typeof(decimal));//FBS  
            //项目编号
            string XMBH = dplXMMC.SelectedValue.ToString();
            //(1)查询出该项目编号下的所有索赔单号
            string sqltextSPDH = "select SPM_ID from TBPM_MAINCLAIM where SPM_XMBH LIKE '" + XMBH + "'";
            DataTable dtSPDH = DBCallCommon.GetDTUsingSqlText(sqltextSPDH);
            if (dtSPDH.Rows.Count > 0)
            {
                //(2)针对每一索赔单号，依次找出
                for (int i = 0; i < dtSPDH.Rows.Count; i++)
                {
                    bool check_zero_yz = false;//判断业主、重机对业主索赔金额是否同时为零
                    bool check_zero_fbs = false;//判断分包商、重机对分包商索赔金额是否同时为零
                    DataRow newrow;
                    newrow = dt.NewRow();
                    string spdh = dtSPDH.Rows[i]["SPM_ID"].ToString();
                    //索赔单号和项目名称
                    string sqldh_mc = "select b.PCON_PJNAME as XMMC,a.SPM_ID as SPDH from TBPM_MAINCLAIM a,TBPM_CONPCHSINFO b where a.SPM_XMBH=b.PCON_PJID and a.SPM_ID='" + spdh + "' GROUP BY a.SPM_ID,b.PCON_PJNAME";
                    DataTable dtdh_mc = DBCallCommon.GetDTUsingSqlText(sqldh_mc);
                    newrow["XMMC"] = dtdh_mc.Rows[0]["XMMC"].ToString();
                    newrow["SPDH"] = dtdh_mc.Rows[0]["SPDH"].ToString();
                    //YZ_ZJYZ_索赔金额
                    //已扣款的索赔金额
                    string sqlYZ = "select SPM_ZZSPJE as JE,SPM_SPLB AS SPLB from TBPM_MAINCLAIM  where SPM_ID='" + spdh + "'and SPM_SFKK=0 GROUP BY SPM_ID,SPM_SPLB,SPM_ZZSPJE";
                    DataTable dtyz = DBCallCommon.GetDTUsingSqlText(sqlYZ);

                    if (dtyz.Rows.Count > 0)
                    {
                        //如果是重机对业主索赔
                        if (dtyz.Rows[0]["SPLB"].ToString() == "1")
                        {
                            newrow["YZ"] = 0.00;
                            newrow["ZJYZ"] = Convert.ToDecimal(dtyz.Rows[0]["JE"].ToString());
                        }
                        else if (dtyz.Rows[0]["SPLB"].ToString() == "0")
                        {
                            newrow["YZ"] = Convert.ToDecimal(dtyz.Rows[0]["JE"].ToString());
                            newrow["ZJYZ"] = 0.00;
                        }
                    }
                    else
                    {
                        newrow["YZ"] = 0.00;
                        newrow["ZJYZ"] = 0.00;
                        check_zero_yz = true;
                    }

                    //FBS_YZFBS
                    string sqlFBS = "select SPS_SPLB AS SPLB,SPS_ZZSPJE as JE from TBPM_SUBCLAIM where SPS_ID='" + spdh + "' and SPS_SFKK=0 GROUP BY SPS_SPLB,SPS_ZZSPJE";
                    DataTable dtfbs = DBCallCommon.GetDTUsingSqlText(sqlFBS);
                    if (dtfbs.Rows.Count > 0)
                    {
                        if (dtfbs.Rows[0]["SPLB"].ToString() == "4" || dtfbs.Rows[0]["SPLB"].ToString() == "5")//分包商、供应商对重机
                        {
                            newrow["ZJFBS"] = 0;
                            newrow["FBS"] = Convert.ToDecimal(dtfbs.Rows[0]["JE"].ToString());
                        }
                        else if (dtfbs.Rows[0]["SPLB"].ToString() == "2" || dtfbs.Rows[0]["SPLB"].ToString() == "3")//重机对分包商、供应商反索赔
                        {
                            newrow["ZJFBS"] = Convert.ToDecimal(dtfbs.Rows[0]["JE"].ToString());
                            newrow["FBS"] = 0;
                        }
                    }
                    else//该索赔单号下分包商无索赔记录
                    {
                        newrow["ZJFBS"] = 0;
                        newrow["FBS"] = 0;
                        check_zero_fbs = true;
                    }
                    //如果四个金额都为零，则不添加
                    if (!check_zero_yz || !check_zero_fbs)
                    {
                        dt.Rows.Add(newrow);
                    }
                }

            }
            if (dt.Rows.Count > 0)
            {
                grvHZ.DataSource = dt;
                grvHZ.DataBind();
                NoDataPanel.Visible = false;
            }
            else
            {
                grvHZ.DataSource = null;
                grvHZ.DataBind();
                NoDataPanel.Visible = true;
            }
            
            if (this.grvHZ.PageCount > 0)
            {
                this.txt_goto.Text=this.lbl_currentpage.Text=(this.grvHZ.PageIndex+1).ToString();
                this.lbl_totalpage.Text = this.grvHZ.PageCount.ToString();
                Pal_page.Visible = true;
                if (this.grvHZ.PageIndex == 0)
                {
                    this.lnkbtnFrist.Enabled = false;
                    this.lnkbtnPre.Enabled = false;
                }
                else
                {
                    this.lnkbtnFrist.Enabled = true;
                    this.lnkbtnPre.Enabled = true;
                }
                if (this.grvHZ.PageIndex == this.grvHZ.PageCount - 1)
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

        }

        /// <summary>
        ///根据时间汇总索赔数据
        /// </summary>
        private void BindDataUsingSJ()
        {

        }

        protected void rblHZLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool state = rblHZLB.SelectedValue.ToString() == "0" ? true : false;
            if (state)
            {
                palSJHZ.Visible = false;
                palXMHZ.Visible = true;
            }
            else
            {
                palXMHZ.Visible = false;
                palSJHZ.Visible = true;
            }
        }

        protected void dplXMMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int state = Convert.ToInt16(rblHZLB.SelectedValue.ToString());
            this.BindHZ(state);
        }

        protected void grvHZ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                yz += Convert.ToDecimal(e.Row.Cells[3].Text);
                zjyz += Convert.ToDecimal(e.Row.Cells[4].Text);
                zjfbs += Convert.ToDecimal(e.Row.Cells[5].Text);
                fbs += Convert.ToDecimal(e.Row.Cells[6].Text);
                //ce += Convert.ToDecimal(e.Row.Cells[7].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "汇总：";
                e.Row.Cells[3].Text = string.Format("{0:N2}", yz);
                e.Row.Cells[4].Text = string.Format("{0:N2}", zjyz);
                e.Row.Cells[5].Text = string.Format("{0:N2}", zjfbs);
                e.Row.Cells[6].Text = string.Format("{0:N2}", fbs);
                decimal ce = Convert.ToDecimal(e.Row.Cells[4].Text) + Convert.ToDecimal(e.Row.Cells[5].Text) - Convert.ToDecimal(e.Row.Cells[3].Text) - Convert.ToDecimal(e.Row.Cells[6].Text);
                if (ce < 0)
                {
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                }
                e.Row.Cells[7].Text = string.Format("{0:N2}", ce);
            }
        }


         #region 分页

        /// <summary>
        /// 重新绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnGoto_Click(object sender, EventArgs e)
        {
            int index =Convert.ToInt32(txt_goto.Text.ToString());            
            if (index <= grvHZ.PageCount && index > 0)
            {
                this.grvHZ.PageIndex = index-1;
                int state = Convert.ToInt16(rblHZLB.SelectedValue.ToString());
                this.BindHZ(state);
            }
        }
        protected void lnkbtnFrist_Click(object sender, EventArgs e)
        {
            this.grvHZ.PageIndex = 0;
            int state = Convert.ToInt16(rblHZLB.SelectedValue.ToString());
            this.BindHZ(state);
        }
        protected void lnkbtnPre_Click(object sender, EventArgs e)
        {
            if (this.grvHZ.PageIndex > 0)
            {
                this.grvHZ.PageIndex = this.grvHZ.PageIndex - 1;
                int state = Convert.ToInt16(rblHZLB.SelectedValue.ToString());
                this.BindHZ(state);
            }
        }
        protected void lnkbtnNext_Click(object sender, EventArgs e)
        {
            if (this.grvHZ.PageIndex < this.grvHZ.PageCount)
            {
                this.grvHZ.PageIndex = this.grvHZ.PageIndex + 1;
                int state = Convert.ToInt16(rblHZLB.SelectedValue.ToString());
                this.BindHZ(state);
            }
        }
        protected void lnkbtnLast_Click(object sender, EventArgs e)
        {
            this.grvHZ.PageIndex = this.grvHZ.PageCount;
            int state = Convert.ToInt16(rblHZLB.SelectedValue.ToString());
            this.BindHZ(state);
        } 

      #endregion
    }
}
