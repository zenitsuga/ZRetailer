using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZPOS.Classes
{
    public class Database
    {
        public SqlConnection sqlConn_Server;
        public SqlConnection sqlConn_Client;

        public bool _ExecuteNonQuery(string SQLStatement)
        {
            bool result = false;
            try
            {
                //string SqlConnstr = SQLConnBuilder(DBPath);
                if (sqlConn_Client.State == ConnectionState.Open)
                {
                    sqlConn_Client.Close();
                }
                sqlConn_Client.Open();
                if (sqlConn_Client.State == ConnectionState.Open)
                {
                    SqlCommand sqlcom = new SqlCommand(SQLStatement, sqlConn_Client);
                    sqlcom.ExecuteNonQuery();
                    result = true;
                }
            }
            catch
            {
            }
            return result;
        }
        public DataTable _ExecuteQuery(string SQLStatement)
        {
            DataTable dtResult = new DataTable();
            try
            {
                if (sqlConn_Client.State == ConnectionState.Closed)
                {
                    sqlConn_Client.Open();
                }
                if (sqlConn_Client.State == ConnectionState.Open)
                {
                    SqlDataAdapter sda = new SqlDataAdapter(SQLStatement, sqlConn_Client);
                    sda.Fill(dtResult);
                }
            }
            catch
            {
            }
            return dtResult;
        }
        public bool ExecuteNonQuery(string SQLStatement)
        {
            bool result = false;
            try
            {
                //string SqlConnstr = SQLConnBuilder(DBPath);
                if (sqlConn_Server.State == ConnectionState.Closed)
                {
                    sqlConn_Server.Open();
                }
                if (sqlConn_Server.State == ConnectionState.Open)
                {
                    SqlCommand sqlcom = new SqlCommand(SQLStatement, sqlConn_Server);
                    sqlcom.ExecuteReader();
                    result = true;
                }
            }
            catch
            {
            }
            return result;
        }
        public DataTable ExecuteQuery(string SQLStatement)
        {
            DataTable dtResult = new DataTable();
            try
            {
                if (sqlConn_Server.State == ConnectionState.Open)
                {
                    sqlConn_Server.Close();
                }
                sqlConn_Server.Open();
                if (sqlConn_Server.State == ConnectionState.Open)
                {
                    SqlDataAdapter sda = new SqlDataAdapter(SQLStatement, sqlConn_Server);
                    sda.Fill(dtResult);
                }
            }
            catch
            {
            }
            return dtResult;
        }
        public bool CopyBulkTableForSync(string tableName,string branchCode)
        {
            bool result = false;
            try
            {
                string Query=string.Empty;
                // Select data from Products table
                
                    Query = "SELECT * FROM " + tableName.Replace("_","") + (tableName.Contains("tbl_") ?  " where BranchCode='" + branchCode + "'":string.Empty);
                    if (sqlConn_Server.State == ConnectionState.Closed)
                    {
                        sqlConn_Server.Open();
                    }
                    if (sqlConn_Client.State == ConnectionState.Closed)
                    {
                        sqlConn_Client.Open();
                    }
                    string QueryTruncate = "truncate table " + (tableName.Contains("tbl_") ? tableName.Replace("_", "") : tableName);
                    SqlCommand cmdTruncate = new SqlCommand(QueryTruncate, sqlConn_Client);
                    cmdTruncate.ExecuteNonQuery();
                    cmdTruncate.Dispose();

                SqlCommand cmd = new SqlCommand(Query,sqlConn_Server);
                // Execute reader
                SqlDataReader reader = cmd.ExecuteReader();
                // Create SqlBulkCopy
                SqlBulkCopy bulkData = new SqlBulkCopy(sqlConn_Client);
                // Set destination table name
                bulkData.DestinationTableName =  (tableName.Contains("tbl_") ? tableName.Replace("_",""):tableName);
                // Write data
                bulkData.WriteToServer(reader);
                // Close objects
                bulkData.Close();
                cmd.Dispose();
                sqlConn_Server.Close();
                sqlConn_Client.Close();
            }
            catch
            {
            }
            return result;
        }
    }
}
