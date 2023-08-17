using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication_REST.View;

namespace WebApplication_REST.Models
{
    public class ClassPostMethods : IPostMethods
    {
        private readonly ClassDbConfig clsDbConfig = new ClassDbConfig();

        public (string res, string err) FileDelete(int custId)
        {
            var resOut = string.Empty;
            var errOut = string.Empty;
            var resCom = 0;
            try
            {
                var com = new OracleCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SBNK_PRL.PKG_CUSTOMERS_E01.DEL_CUSTOMERS_E01"
                };
                var P_IDN = new OracleParameter
                {
                    ParameterName = "P_IDN",
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Input,
                    Value = DBNull.Value
                };
                if (custId != 0)
                    P_IDN.Value = custId;
                com.Parameters.Add(P_IDN);
                var procRes = new OracleParameter
                {
                    ParameterName = "RES",
                    OracleDbType = OracleDbType.Varchar2,
                    Direction = ParameterDirection.Output,
                    Size = 100
                };
                com.Parameters.Add(procRes);

                (resCom, errOut) = clsDbConfig.InsertDb(com);
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (resOut, errOut);
                resOut = procRes.Value.ToString();
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            return (resOut, errOut);
        }

        public (string res, string custIdn, string err) FileOper(ClassControlsOper cls)
        {
            var resOut = string.Empty;
            var errOut = string.Empty;
            var custIdnOut = string.Empty;
            var resOcm = 0;
            try
            {
                var com = new OracleCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SBNK_PRL.PKG_CUSTOMERS_E01.OPER_CUSTOMERS_E01"
                };
                var P_IDN = new OracleParameter
                {
                    ParameterName = "P_IDN",
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.InputOutput,
                    Value = DBNull.Value
                };
                if (cls.custIdn != 0)
                    P_IDN.Value = cls.custIdn;
                com.Parameters.Add(P_IDN);

                var P_NAME = new OracleParameter
                {
                    ParameterName = "P_NAME",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = cls.name
                };
                com.Parameters.Add(P_NAME);
                var P_SURNAME = new OracleParameter
                {
                    ParameterName = "P_SURNAME",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = cls.surname
                };
                com.Parameters.Add(P_SURNAME);
                var P_BIRTH_PLACE = new OracleParameter
                {
                    ParameterName = "P_BIRTH_PLACE",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = cls.birthPlace
                };
                com.Parameters.Add(P_BIRTH_PLACE);
                var P_BIRTH_DATE = new OracleParameter
                {
                    ParameterName = "P_BIRTH_DATE",
                    OracleDbType = OracleDbType.Date,
                    Value = DBNull.Value
                };
                if (cls.birthDate != "  .  .")
                    P_BIRTH_DATE.Value = DateTime.Parse(cls.birthDate);
                com.Parameters.Add(P_BIRTH_DATE);
                var P_GENDER = new OracleParameter
                {
                    ParameterName = "P_GENDER",
                    OracleDbType = OracleDbType.Int32,
                    Value = cls.gender
                };
                com.Parameters.Add(P_GENDER);
                var P_DOC_NO = new OracleParameter
                {
                    ParameterName = "P_DOC_NO",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = cls.docNo
                };
                com.Parameters.Add(P_DOC_NO);
                var P_FIN_CODE = new OracleParameter
                {
                    ParameterName = "P_FIN_CODE",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = cls.finCode
                };
                com.Parameters.Add(P_FIN_CODE);
                var P_PHONE_NUMBER = new OracleParameter
                {
                    ParameterName = "P_PHONE_NUMBER",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = cls.phoneNumber
                };
                com.Parameters.Add(P_PHONE_NUMBER);
                var P_EMAIL = new OracleParameter
                {
                    ParameterName = "P_EMAIL",
                    OracleDbType = OracleDbType.Varchar2,
                    Value = cls.email
                };
                com.Parameters.Add(P_EMAIL);
                var procRes = new OracleParameter
                {
                    ParameterName = "RES",
                    OracleDbType = OracleDbType.Varchar2,
                    Direction = ParameterDirection.Output,
                    Size = 100
                };
                com.Parameters.Add(procRes);

                (resOcm, errOut) = clsDbConfig.InsertDb(com);
                if (!string.IsNullOrWhiteSpace(errOut))
                    return (resOut, custIdnOut, errOut);
                resOut = procRes.Value.ToString();
                custIdnOut = P_IDN.Value.ToString();
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            return (resOut, custIdnOut, errOut);
        }
    }
}