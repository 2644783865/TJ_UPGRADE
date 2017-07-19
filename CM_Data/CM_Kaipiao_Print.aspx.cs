using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Kaipiao_Print : System.Web.UI.Page
    {
        string id;
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();//识别号
            BindInfo();
            BindDetail();

        }

        private void BindInfo()
        {
            sqlText = "select * from CM_KAIPIAO where Id='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                hidId.Value = id;
                hidTaskId.Value = row["KP_TaskID"].ToString();
                KP_CODE.Text = row["KP_CODE"].ToString();
                KP_ZDRNM.Text = row["KP_ZDRNM"].ToString();
                KP_COM.Text = row["KP_COM"].ToString();
                KP_ADDRESS.Text = row["KP_ADDRESS"].ToString();
                KP_SHEBEI.Text = row["KP_SHEBEI"].ToString();
                KP_ACCOUNT.Text = row["KP_ACCOUNT"].ToString();
                KP_SHUIHAO.Text = row["KP_SHUIHAO"].ToString();
                KP_BANK.Text = row["KP_BANK"].ToString();
                KP_TEL.Text = row["KP_TEL"].ToString();
                KP_CONZONGJIA.Text = row["KP_CONZONGJIA"].ToString();
                KP_DAOKUANJE.Text = row["KP_DAOKUANJE"].ToString();
                KP_CONDITION.Text = row["KP_CONDITION"].ToString();
                KP_YIKAIJE.Text = row["KP_YIKAIJE"].ToString();
                KP_BENCIJE.Text = row["KP_BENCIJE"].ToString();
                KP_JIAOFUFS.Text = row["KP_JIAOFUFS"].ToString();
                KP_TIQIANKP.SelectedValue = row["KP_TIQIANKP"].ToString();
                KP_ZDTIME.Text = row["KP_ZDTIME"].ToString();
                hidHSState.Value = row["KP_HSSTATE"].ToString();
                hidSPState.Value = row["KP_SPSTATE"].ToString();
                KP_KPNUMBER.Text = row["KP_KPNUMBER"].ToString();
                KP_NOTE.Text = row["KP_NOTE"].ToString();


                //审核信息
                tbspr1.Text = row["KP_SHRNMA"].ToString();
                if (row["KP_RESULTA"].ToString().Trim() != "")
                {
                    tbspjl1.Text = row["KP_RESULTA"].ToString().Trim() == "0" ? "同意" : "不同意";
                }
                tbsptime1.Text = row["KP_SHTIMEA"].ToString();
                tbspr2.Text = row["KP_SHRNMB"].ToString();
                if (row["KP_RESULTB"].ToString().Trim() != "")
                {
                    tbspjl2.Text = row["KP_RESULTB"].ToString().Trim() == "0" ? "同意" : "不同意";
                }
                tbsptime2.Text = row["KP_SHTIMEB"].ToString();
            }
        }

        private void BindDetail()
        {
            sqlText = "select * from CM_KAIPIAO_DETAIL where cId='" + hidTaskId.Value + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            decimal tot_money = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tot_money += CommonFun.ComTryDecimal(dt.Rows[i]["kpmoney"].ToString().Trim());
                }
                lb_select_money.Text = tot_money.ToString();
            }
        }
    }
}
