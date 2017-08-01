using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_TZTHTZDGL : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUser(ControlFinder);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sqlFields = "tzd_bh,tzd_gkmc,tzd_zdsj,tzd_nr,nr_th,nr_tm,nr_bz,tzd_spzt";
            string sqlTableName = "(select a.*,stuff((select '|'+ replace(NR_TH,' ','') from CM_TZTHTZD_NR as b where (b.NR_FATHERID=a.TZD_SJID)FOR xml path('')),1,1,'')as NR_TH,stuff((select '|'+ replace(NR_TM,' ','') from CM_TZTHTZD_NR as b where (b.NR_FATHERID=a.TZD_SJID)FOR xml path('')),1,1,'')as NR_TM,stuff((select '|'+ replace(NR_BZ,' ','') from CM_TZTHTZD_NR as b where (b.NR_FATHERID=a.TZD_SJID)FOR xml path('')),1,1,'')as NR_BZ,'closed' as state ,'" + Session["UserName"].ToString() + "' as username,'07'as depid from CM_TZTHTZD as a )t";
            string sqlExport = string.Format("SELECT {0} FROM {1} ", sqlFields, sqlTableName);
            string sqlWhere = GetWhere();

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlExport += " WHERE ";
                sqlExport += sqlWhere;
            }

            string filename = string.Format("图纸替换通知单{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            string filestandard = "图纸替换通知单.xls";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlExport);
            exportCommanmethod.exporteasy(dt, filename, filestandard, 1, true, false, true);
        }

        private string GetWhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request.Form["ddlSPZT"]))
            {
                switch (Request.Form["ddlSPZT"])
                {
                    case "1":
                        sb.Append(" AND TZD_SPZT = '0' ");
                        break;
                    case "2":
                        sb.Append(" AND TZD_SPZT = '1' ");
                        break;
                    case "3":
                        sb.Append(" AND TZD_SPZT = '10' ");
                        break;
                    case "4":
                        sb.Append(" AND TZD_SPZT = '11' ");
                        break;
                }
            }
            if (Request.Form["rblRW"].ToString() == "1")
            {
                sb.Append(string.Format("AND TZD_ZDR = '{0}' or TZD_SPR1 = '{0}' or TZD_SPR2 = '{0}' ", Session["UserName"].ToString()));
            }
            if (!string.IsNullOrEmpty(Request.Form["txtbh"]))
            {
                sb.Append(string.Format("AND TZD_BH LIKE '%{0}%'", Request.Form["txtbh"]));
            }
            return sb.ToString();            
        }
    }
}
