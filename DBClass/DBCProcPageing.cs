using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ZCZJ_DPF
{
    public class DBCProcPageing
    {
        public static DataTable Projects_Select(int PageNumber, int PageSize)
        {
            System.Data.SqlClient.SqlConnection sqlConn = new SqlConnection();
            System.Data.SqlClient.SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "procgetprojectsbypage");
            DBCallCommon.AddParameterToStoredProc(sqlCmd, "@PageNumber", PageNumber.ToString(), SqlDbType.Int, 20);
            DBCallCommon.AddParameterToStoredProc(sqlCmd, "@PageSize", PageSize.ToString(), SqlDbType.Int, 20);
            DataTable dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
            sqlConn.Close();
            return dt;
        }
    }
}
