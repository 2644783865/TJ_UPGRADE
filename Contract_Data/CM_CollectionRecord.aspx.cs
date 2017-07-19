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
using System.Text;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_CollectionRecord : System.Web.UI.Page
    {
        double sw_ykje = 0;
        double sw_dkje = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnQuery.UniqueID;   
            if (!IsPostBack)
            {
                this.BindData();
                Contract_Data.ContractClass.BindPrjName(dplXMMC);
            }
        }
        //所有数据绑定
        private void BindData()
        {
            string sqltext = "SELECT a.[BP_ID],a.[BP_HTBH] ,a.[BP_KXMC] ,a.[BP_YKRQ],a.[BP_SKRQ] ,a.[BP_JE] ,a.[BP_SFJE]  ,a.[BP_SKFS]  ,a.[BP_STATE] ,a.[BP_NOTEFST] ,a.[BP_NOTESND],b.[PCON_PJNAME]" +
                           " FROM [ZCZJDMP].[dbo].[TBPM_BUSPAYMENTRECORD] a " +
                             " INNER JOIN TBPM_CONPCHSINFO b ON a.[BP_HTBH]=b.[PCON_BCODE]";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                grvYK.DataSource = dt;
                grvYK.DataBind();
                palNoData.Visible = false;
            }
            else
            {
                grvYK.DataSource = null;
                grvYK.DataBind();
                palNoData.Visible = true;
            }
        }

        //汇总
        protected void grvYK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sw_ykje += Convert.ToDouble(e.Row.Cells[7].Text);//要款金额
                sw_dkje += Convert.ToDouble(e.Row.Cells[8].Text);//到款金额
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "汇总：";
                e.Row.Cells[7].Text = string.Format("{0:c}", sw_ykje);
                e.Row.Cells[8].Text = string.Format("{0:c}", sw_dkje);
            }
        }
        //合同号查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            rblState.SelectedIndex = 0;
            dplXMMC.SelectedIndex = 0;
            string sqltext = "SELECT a.[BP_ID],a.[BP_HTBH] ,a.[BP_KXMC] ,a.[BP_YKRQ],a.[BP_SKRQ] ,a.[BP_JE] ,a.[BP_SFJE]  ,a.[BP_SKFS]  ,a.[BP_STATE] ,a.[BP_NOTEFST] ,a.[BP_NOTESND],b.[PCON_PJNAME]" +
                           " FROM [ZCZJDMP].[dbo].[TBPM_BUSPAYMENTRECORD] a " +
                             " INNER JOIN TBPM_CONPCHSINFO b ON a.[BP_HTBH]=b.[PCON_BCODE]"+
                             " WHERE a.[BP_HTBH] LIKE '%"+txtHTBH.Text.Trim()+"%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                grvYK.DataSource = dt;
                grvYK.DataBind();
                palNoData.Visible = false;
            }
            else
            {
                grvYK.DataSource = null;
                grvYK.DataBind();
                palNoData.Visible = true;
            }
        }
        //项目查询dplXMMC_onSelectIndexChanged
        protected void dplXMMC_onSelectIndexChanged(object sender, EventArgs e)
        {
            txtHTBH.Text = "";
            rblState.SelectedIndex = 0;
            if (dplXMMC.SelectedIndex != 0)
            {
                string sqltext = "SELECT a.[BP_ID],a.[BP_HTBH] ,a.[BP_KXMC] ,a.[BP_YKRQ],a.[BP_SKRQ] ,a.[BP_JE] ,a.[BP_SFJE]  ,a.[BP_SKFS]  ,a.[BP_STATE] ,a.[BP_NOTEFST] ,a.[BP_NOTESND],b.[PCON_PJNAME]" +
                   " FROM [ZCZJDMP].[dbo].[TBPM_BUSPAYMENTRECORD] a " +
                     " INNER JOIN TBPM_CONPCHSINFO b ON a.[BP_HTBH]=b.[PCON_BCODE]" +
                     " WHERE b.[PCON_PJID]='"+dplXMMC.SelectedValue.ToString()+"'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    grvYK.DataSource = dt;
                    grvYK.DataBind();
                    palNoData.Visible = false;
                }
                else
                {
                    grvYK.DataSource = null;
                    grvYK.DataBind();
                    palNoData.Visible = true;
                }
            }
            else
            {
                this.BindData();
            }
        }
        //状态rblState_OnSelectedIndexChanged
        protected void rblState_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtHTBH.Text = "";
            dplXMMC.SelectedIndex = 0;
            if (rblState.SelectedIndex!=0)
            {
                string sqltext = "SELECT a.[BP_ID],a.[BP_HTBH] ,a.[BP_KXMC] ,a.[BP_YKRQ],a.[BP_SKRQ] ,a.[BP_JE] ,a.[BP_SFJE]  ,a.[BP_SKFS]  ,a.[BP_STATE] ,a.[BP_NOTEFST] ,a.[BP_NOTESND],b.[PCON_PJNAME]" +
                   " FROM [ZCZJDMP].[dbo].[TBPM_BUSPAYMENTRECORD] a " +
                     " INNER JOIN TBPM_CONPCHSINFO b ON a.[BP_HTBH]=b.[PCON_BCODE]" +
                     " WHERE a.[BP_STATE]=" + rblState.SelectedValue.ToString() + "";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    grvYK.DataSource = dt;
                    grvYK.DataBind();
                    palNoData.Visible = false;
                }
                else
                {
                    grvYK.DataSource = null;
                    grvYK.DataBind();
                    palNoData.Visible = true;
                }
            }
            else
            {
                this.BindData();
            }
        }
    }
}
