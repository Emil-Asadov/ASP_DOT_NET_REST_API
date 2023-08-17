using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication_REST.Models
{
    public class ClassDbConfig
    {
        public OracleConnection con { get; set; }
        public string GetConnectionString()
        {
            var connectionString = string.Empty;
            //connectionString = $"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-L7Q1T20)(PORT = 1521))(CONNECT_DATA =(SERVER = dedicated)(SERVICE_NAME = orcl)));Password=Az123987;User ID=RBEMIL";
            connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            return connectionString;
        }
        public (OracleConnection conRes, string errRes) GetConnection()
        {
            var errOut = string.Empty;
            try
            {
                con = new OracleConnection(GetConnectionString());
                con.Open();
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
                return (con, errOut);
            }
            return (con, errOut);
        }

        public (DataTable dtRes, string errRes) FillDT(string query, OracleParameter[] parameterCollection = null)
        {
            var errOut = string.Empty;
            var dtOut = new DataTable();
            try
            {
                (con, errOut) = GetConnection();
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (dtOut, errOut);
                using (con)
                {
                    using (var com = new OracleCommand())
                    {
                        com.CommandText = query;
                        com.CommandType = CommandType.StoredProcedure;
                        com.Connection = con;
                        if (parameterCollection != null)
                            com.Parameters.AddRange(parameterCollection);
                        using (var read = com.ExecuteReader())
                        {
                            dtOut.Load(read);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
            return (dtOut, errOut);
        }
        public (DataTable dtRes, string errRes) FillDT(string query)
        {
            var errOut = string.Empty;
            var dtOut = new DataTable();
            try
            {
                (con, errOut) = GetConnection();
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (dtOut, errOut);
                using (con)
                {
                    using (var com = new OracleCommand())
                    {
                        com.CommandText = query;
                        com.CommandType = CommandType.Text;
                        com.Connection = con;
                        using (var read = com.ExecuteReader())
                        {
                            dtOut.Load(read);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
            return (dtOut, errOut);
        }

        public (string dtRes, string errRes) FillValue(string query)
        {
            var errOut = string.Empty;
            var dtOut = string.Empty;
            try
            {
                (con, errOut) = GetConnection();
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (dtOut, errOut);
                using (con)
                {
                    using (var com = new OracleCommand())
                    {
                        com.CommandText = query;
                        com.CommandType = CommandType.Text;
                        com.Connection = con;
                        dtOut = com.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
            return (dtOut, errOut);
        }
        public (int res, string err) InsertDb(OracleCommand com)
        {
            var myTrans = default(OracleTransaction);
            var errOut = string.Empty;
            var resOut = 0;
            try
            {
                (con, errOut) = GetConnection();
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (resOut, errOut);
                myTrans = con.BeginTransaction();
                using (com)
                {
                    com.Transaction = myTrans;
                    com.Connection = con;
                    resOut = com.ExecuteNonQuery();
                    myTrans.Commit();
                }
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
                myTrans.Rollback();
                return (resOut, errOut);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }

            return (resOut, errOut);
        }
    }
}