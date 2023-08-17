using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services;
using WebApplication_REST.Models;
using WebApplication_REST.View;

namespace WebApplication_REST.Controllers
{
    public class HomeController : ApiController
    {
        private readonly IGetMethods clsGetMethods = new ClassGetMethods();
        private readonly IPostMethods clsPostMethods = new ClassPostMethods();

        private (IQueryable<ClassControls> res, string err) GetCustomer()
        {
            var errRes = string.Empty;
            var dt = new DataTable();
            try
            {
                (dt, errRes) = clsGetMethods.GetCustomer();
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (null, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }
            return (dt.AsEnumerable().AsQueryable().Select(row => new ClassControls
            {
                custIdn = int.Parse(row["IDN"].ToString()),
                name = row["NAME"].ToString(),
                surname = row["SURNAME"].ToString(),
                birthPlace = row["BIRTH_PLACE"].ToString(),
                birthDate = row["BIRTH_DATE"].ToString(),
                genderName = row["GENDER_NAME"].ToString(),
                docNo = row["DOC_NO"].ToString(),
                finCode = row["FIN_CODE"].ToString(),
                phoneNumber = row["PHONE_NUMBER"].ToString(),
                email = row["EMAIL"].ToString()
            }), errRes);
        }
        private (List<ClassControls> res, string err) GetCustomerLast()
        {
            var errRes = string.Empty;
            var dt = new DataTable();
            var lst = new List<ClassControls>();
            try
            {
                (dt, errRes) = clsGetMethods.GetCustomer();
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (null, errRes);
                var cls = new ClassControls();
                foreach (var item in dt.Rows)
                {
                    //cls = new ClassControls
                    //{
                    //    custIdn = int.Parse(item["IDN"].ToString()),
                    //    name = dt.Rows[i]["NAME"].ToString(),
                    //    surname = dt.Rows[i]["SURNAME"].ToString(),
                    //    birthPlace = dt.Rows[i]["BIRTH_PLACE"].ToString(),
                    //    birthDate = dt.Rows[i]["BIRTH_DATE"].ToString(),
                    //    genderName = dt.Rows[i]["GENDER_NAME"].ToString(),
                    //    docNo = dt.Rows[i]["DOC_NO"].ToString(),
                    //    finCode = dt.Rows[i]["FIN_CODE"].ToString(),
                    //    phoneNumber = dt.Rows[i]["PHONE_NUMBER"].ToString(),
                    //    email = dt.Rows[i]["EMAIL"].ToString()
                    //};
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cls = new ClassControls
                    {
                        custIdn = int.Parse(dt.Rows[i]["IDN"].ToString()),
                        name = dt.Rows[i]["NAME"].ToString(),
                        surname = dt.Rows[i]["SURNAME"].ToString(),
                        birthPlace = dt.Rows[i]["BIRTH_PLACE"].ToString(),
                        birthDate = dt.Rows[i]["BIRTH_DATE"].ToString(),
                        genderName = dt.Rows[i]["GENDER_NAME"].ToString(),
                        docNo = dt.Rows[i]["DOC_NO"].ToString(),
                        finCode = dt.Rows[i]["FIN_CODE"].ToString(),
                        phoneNumber = dt.Rows[i]["PHONE_NUMBER"].ToString(),
                        email = dt.Rows[i]["EMAIL"].ToString()
                    };
                    lst.Add(cls);
                }
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }
            return (lst, errRes);
        }
        private (ClassControls res, string err) GetCustomerNew(int custId)
        {
            var errRes = string.Empty;
            var dt = new DataTable();
            try
            {
                (dt, errRes) = clsGetMethods.GetCustomer(custId);
                if (!string.IsNullOrWhiteSpace(errRes))
                    return (null, errRes);
            }
            catch (Exception ex)
            {
                errRes = ex.Message;
            }
            var cls = new ClassControls()
            {
                custIdn = int.Parse(dt.Rows[0]["IDN"].ToString()),
                name = dt.Rows[0]["NAME"].ToString(),
                surname = dt.Rows[0]["SURNAME"].ToString(),
                birthPlace = dt.Rows[0]["BIRTH_PLACE"].ToString(),
                birthDate = dt.Rows[0]["BIRTH_DATE"].ToString(),
                genderName = dt.Rows[0]["GENDER_NAME"].ToString(),
                docNo = dt.Rows[0]["DOC_NO"].ToString(),
                finCode = dt.Rows[0]["FIN_CODE"].ToString(),
                phoneNumber = dt.Rows[0]["PHONE_NUMBER"].ToString(),
                email = dt.Rows[0]["EMAIL"].ToString()
            };
            return (cls, errRes);
        }

        [HttpGet]
        [Route("CustomerAllList")]
        //http://localhost:64747/CustomerAllList
        public List<ClassControls> GetCustomerFull()
        {
            (var lst, var err) = GetCustomerLast();
            if (!string.IsNullOrWhiteSpace(err))
                return null;
            return lst;
        }

        [HttpGet]
        [Route("CustomerFullName/{id}")]
        //http://localhost:64747/CustomerFullName/25
        public string GetCustomerFullName(int id)
        {
            (var lst, var err) = clsGetMethods.GetCustomerFullNameById(id);
            if (!string.IsNullOrWhiteSpace(err))
                return null;
            return lst;
        }
        [HttpGet]
        [Route("CustomerById/{id}")]
        //http://localhost:64747/CustomerById/25
        public ClassControls GetCustomerById(int id)
        {
            (var lst, var err) = GetCustomerNew(id);
            if (!string.IsNullOrWhiteSpace(err))
                return null;
            return lst;
        }
        [HttpPost]
        [Route("CustomerOper")]
        public IHttpActionResult CustomerOper([FromBody] ClassControlsOper cls)
        {
            (var res, var custIdnOut, var err) = clsPostMethods.FileOper(cls);
            if (!string.IsNullOrWhiteSpace(err))
                return BadRequest(err);
            if (res != "4")
                return BadRequest(res);
            //err = GetCustomer();
            //if (!string.IsNullOrWhiteSpace(err))
            //{
            //    MessageBox.Show(err);
            //    return;
            //}
            res = $"Əməliyyat yerinə yetirildi. {custIdnOut} qeydiyyatlı müştəri";
            if (cls.custIdn == 0)
                res += $@" əlavə edildi.";
            else
                res += $@" yeniləndi.";
            return Ok(res);
        }
        [HttpPost]
        [Route("CustomerDelete")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            (var res, var err) = clsPostMethods.FileDelete(id);
            if (!string.IsNullOrWhiteSpace(err))
                return BadRequest(err);
            if (res != "4")
                return BadRequest(res);
            //err = GetCustomer();
            //if (!string.IsNullOrWhiteSpace(err))
            //{
            //    MessageBox.Show(err);
            //    return;
            //}
            res = $"Əməliyyat yerinə yetirildi. {id} qeydiyyatlı müştəri silindi.";

            return Ok(res);
        }
    }
}
