using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_Bgyp_Store : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUser(ControlFinder);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sqlFields = "maid,name,canshu,pname,num,unit,unprice,price";
            string sqlTableName = "View_OM_BGYP_STORE";
            string sqlExport = string.Format("SELECT {0} FROM {1} ", sqlFields, sqlTableName);
            string sqlWhere = GetWhere();

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlExport += " WHERE ";
                sqlExport += sqlWhere;
            }

            string filename = string.Format("办公用品库存{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            string filestandard = "办公用品库存.xls";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlExport);
            exportCommanmethod.exporteasy(dt, filename, filestandard, 1, true, false, true);
        }

        private string GetWhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Request["maid"]))
            {
                sb.Append(string.Format("AND maId LIKE '%{0}%' ", Request.Form["maid"]));
            }
            if (!string.IsNullOrEmpty(Request["name"]))
            {
                sb.Append(string.Format("AND name LIKE '%{0}%'", Request.Form["name"]));
            }
            if (!string.IsNullOrEmpty(Request["canshu"]))
            {
                sb.Append(string.Format("AND canshu LIKE '%{0}%'",Request.Form["canshu"]));
            }
            if (!string.IsNullOrEmpty(Request["pid"]))
            {
                sb.Append(string.Format("AND pid LIKE '%{0}%'",Request.Form["pid"]));
            }
            return sb.ToString();
        }

    }
}
