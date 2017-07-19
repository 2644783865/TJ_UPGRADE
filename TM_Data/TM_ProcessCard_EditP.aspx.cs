using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_ProcessCard_EditP : System.Web.UI.Page
    {
        string sqlText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ProId = Request.QueryString["Id"].ToString();
                proId.Value = Request.QueryString["Id"].ToString();
                if (ProId == "0")
                {
                   
                    method.Value = "ProcessCardNew";
                }
                else
                {
                   
                    method.Value = "ProcessCardEdit";
                    this.InitPage(ProId);
                }
            }
        }

        private void InitPage(string proid)
        {
            //PRO_ID, PRO_ENGNAME, PRO_ENGMODEL, PRO_PARTNAME, PRO_TUHAO, PRO_FUJIAN, PRO_ISSUEDTIME, PRO_NOTE, fileID, BC_CONTEXT, fileload, fileUpDate, fileName
            sqlText = "select * from View_TM_PROCESS_CARD where PRO_ID=" + proid;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtEngName.Text = row["PRO_ENGNAME"].ToString();
                txtEngModel.Text = row["PRO_ENGMODEL"].ToString();
                txtPartName.Text = row["PRO_PARTNAME"].ToString();
                txtTuHao.Text = row["PRO_TUHAO"].ToString();
                HyperLink1.Text = row["fileName"].ToString();
                txtBZ.Text = row["PRO_NOTE"].ToString();
                hidGuanLianTime.Value = row["PRO_FUJIAN"].ToString();
                txtBanci.Text= row["PRO_BANCI"].ToString();
            }

        }


       
      
    }
}
