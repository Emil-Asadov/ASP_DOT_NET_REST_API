using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication_REST.View;

namespace WebApplication_REST.Models
{
    public class ClassGetMethods : IGetMethods
    {
        private readonly ClassDbConfig clsDbConfig = new ClassDbConfig();
        public (DataTable dt, string err) GetCustomer(int custId = 0)
        {
            var dtRes = new DataTable();
            var errRes = string.Empty;
            var query = "SBNK_PRL.PKG_CUSTOMERS_E01.GET_CUSTOMERS_E01";
            try
            {
                var V_RES = new OracleParameter
                {
                    ParameterName = "V_RES",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.ReturnValue
                };
                OracleParameter[] arr = null;
                var P_IDN = new OracleParameter
                {
                    ParameterName = "P_IDN",
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Input,
                    Value = DBNull.Value
                };
                if (custId != 0)
                {
                    P_IDN.Value = custId;
                    arr = new OracleParameter[] { V_RES, P_IDN };
                }
                else
                    arr = new OracleParameter[] { V_RES };

                (dtRes, errRes) = clsDbConfig.FillDT(query, arr);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (dtRes, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (dtRes, errRes);
        }

        public (string dt, string err) GetCustomerFullNameById(int custId)
        {
            var dtRes = string.Empty;
            var errRes = string.Empty;
            var query = $"SELECT SBNK_PRL.PKG_CUSTOMERS_E01.GET_CUSTOMER_FULL_NAME(P_CUST_ID => {custId}) RES FROM DUAL";
            try
            {
                var V_RES = new OracleParameter
                {
                    ParameterName = "V_RES",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.ReturnValue
                };

                (dtRes, errRes) = clsDbConfig.FillValue(query);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (dtRes, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }

            return (dtRes, errRes);
        }
    }
}