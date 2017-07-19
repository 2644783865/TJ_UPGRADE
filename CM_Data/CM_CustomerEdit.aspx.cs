using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_CustomerEdit : System.Web.UI.Page
    {
        string CM_ID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CM_ID = Request.QueryString["CM_ID"].ToString();
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                bindData();
            }
        }

        private void bindData()
        {
            string sql = "select a.*,CONVERT(VARCHAR(10),CM_INDATE,120) as CM_INDATE1,CONVERT(VARCHAR(10),CM_CMDATE,120) as CM_CMDATE1,b.ST_NAME,CM_NOTE,c.ST_NAME as CM_KEEPER from TBCM_CUSTOMER as a left join TBDS_STAFFINFO as b on a.CM_MANCLERK=b.ST_ID left join TBDS_STAFFINFO as c on a.CM_INKEEP=c.ST_ID where CM_ID='" + CM_ID + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (Control control in Panel1.Controls)
                {
                    if (control is TextBox)
                    {
                        ((TextBox)control).Text = dt.Rows[0][control.ID].ToString();
                    }
                    else if (control is DropDownList)
                    {
                        ((DropDownList)control).SelectedValue = dt.Rows[0][control.ID].ToString();
                    }
                    else if (control is Label)
                    {
                        ((Label)control).Text = dt.Rows[0][control.ID].ToString();
                    }
                }
                inKeep.Value = dt.Rows[0]["CM_INKEEP"].ToString();
                hd_tsa.Value = dt.Rows[0]["TSA_ID"].ToString();
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (TSA_ID.Text != hd_tsa.Value)
            {
                string choose = "select * from TBQM_QTSASSGN where QSA_ENGID='" + TSA_ID.Text + "'";
                string zjy = "";
                bool tj = false;
                DataTable chdt = DBCallCommon.GetDTUsingSqlText(choose);
                if (chdt.Rows.Count > 0)
                {
                    zjy = chdt.Rows[0]["QSA_QCCLERK"].ToString();
                }
                else
                {
                    choose = "select * from TBQM_SetInspectPerson where ID='16' and num='顾客财产'";
                    chdt = DBCallCommon.GetDTUsingSqlText(choose);
                    if (chdt.Rows.Count > 0)
                    {
                        zjy = chdt.Rows[0]["InspectPerson"].ToString();
                    }
                    else
                    {
                        tj = true;
                    }
                }
                if (zjy == "")
                {
                    tj = true;
                }
                if (tj)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "tishi", "alert('找不到质检员，请联系质量部！');", true); return;
                }
                string str = "update TBCM_CUSTOMER set CM_ZJYUAN='" + zjy + "' where CM_ID='" + CM_ID + "'";
                list.Add(str);
            }
            string sql = string.Format("update TBCM_CUSTOMER set CM_PJNAME='{0}',CM_CONTR='{1}',CM_COSTERM='{2}',CM_PIC='{3}',CM_EQUIP='{4}',CM_APPNAME='{5}',CM_CMDATE='{6}',CM_NUM='{7}',CM_PLACE='{8}',CM_INKEEP='{9}',CM_KGYUAN='{9}',CM_INDATE='{10}',CM_MANCLERK='{11}',CM_INOROUT='{12}',CM_NOTE='{13}',CM_BIANHAO='{14}',TSA_ID='{16}',CM_CHECK='' where CM_ID='{15}'", CM_PJNAME.Text, CM_CONTR.Text, CM_COSTERM.Text, CM_PIC.Text, CM_EQUIP.Text, CM_APPNAME.Text, CM_CMDATE1.Text, CM_NUM.Text, CM_PLACE.Text, inKeep.Value, CM_INDATE1.Text, UserID.Value, CM_INOROUT.SelectedValue, CM_NOTE.Text, CM_BIANHAO.Text, CM_ID, TSA_ID.Text);
            list.Add(sql);
            DBCallCommon.ExecuteTrans(list);
            //Response.Write("<script>alert('添加成功！')</script>");
            Response.Write("<script>window.close()</script>");
        }
    }
}
