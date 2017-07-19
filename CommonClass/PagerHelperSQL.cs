using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

//using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

//using SMS.Common;

namespace ZCZJ_DPF
{
    public class PagerHelperSQL2000
    {
        //设置企业数据库
        //private Database db = DatabaseFactory.CreateDatabase();
        /// <summary>
        /// PagerHelperSQL2000 构造函数
        /// </summary>
        public PagerHelperSQL2000()
        { }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="Order_Field">排序字段</param>
        /// <param name="OrderType">排序类型（1:升序　0:降序）</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="TotalCount">返回总数</param>
        /// <returns>DataTable</returns>
        // public DataTable GetDataListBuPagerQueryParam(PagerQueryParam pager, out int TotalCount, out String ErrMsg)
        //{
        public DataTable GetDataByPagerQueryParam(PagerQueryParam pager)
        {
            //TotalCount = pager.TotalCount;
            //ErrMsg = string.Empty;
            DataTable dt = new DataTable();
            //if (pager == null)
            //{
            //    ErrMsg = "出入参数PagerQueryParam为NULL";
            //    return dt;
            //}
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "procpaging");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Table_Name", pager.TableName, SqlDbType.VarChar, 100);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Primary_Key", pager.PrimaryKey, SqlDbType.VarChar, 100);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Show_Fields", pager.ShowFields, SqlDbType.VarChar, 100);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Order_Field", pager.OrderField, SqlDbType.VarChar, 100);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@OrderType", pager.OrderType.ToString(), SqlDbType.VarChar, 100);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@where_Condition", pager.StrWhere, SqlDbType.VarChar, 300);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Page_Size", pager.PageSize.ToString(), SqlDbType.Int, 20);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Page_Index", pager.PageIndex.ToString(), SqlDbType.Int, 20);
                //DBCallCommon.AddParameterToStoredProc(sqlCmd, "@TotalCount", pager.TotalCount.ToString(), SqlDbType.Int, 20);
                //DBCallCommon.AddParameterToStoredProc(sqlCmd, "@ReTotalCount", "10", SqlDbType.Int, 20);
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
                //return dt;

                //DbCommand dbCommand = db.GetStoredProcCommand("SMSPagination");
                //db.AddInParameter(dbCommand, "Table_Name", DbType.AnsiString, pager.TableName);
                //db.AddInParameter(dbCommand, "Primary_Key", DbType.AnsiString, pager.PrimaryKey);
                //db.AddInParameter(dbCommand, "Show_Fields", DbType.AnsiString, pager.ShowFields);
                //db.AddInParameter(dbCommand, "Order_Field", DbType.AnsiString, pager.OrderField);
                //db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, pager.OrderType);
                //db.AddInParameter(dbCommand, "where_Condition", DbType.String, pager.StrWhere);
                //db.AddInParameter(dbCommand, "Page_Size", DbType.Int32, pager.PageSize);
                //db.AddInParameter(dbCommand, "Page_Index", DbType.Int32, pager.PageIndex);                
                //db.AddInParameter(dbCommand, "TotalCount", DbType.Int32, pager.TotalCount);
                //db.AddOutParameter(dbCommand, "ReTotalCount", DbType.Int32, 10);
                //using (DataSet ds = db.ExecuteDataSet(dbCommand))
                //{
                //    if (pager.PageIndex == 1)
                //    {
                //        if (db.GetParameterValue(dbCommand, "ReTotalCount") != DBNull.Value && db.GetParameterValue(dbCommand, "TotalCount") != null)
                //        {
                //            TotalCount = Convert.ToInt32((db.GetParameterValue(dbCommand, "ReTotalCount")));                            
                //        }                        
                //    }
                //    dt = ds.Tables[0];
                //}
            }
            catch (Exception)
            {
                throw;
            }           
            return dt;
        }

    }
}
