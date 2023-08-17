using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_REST.View
{
    internal interface IGetMethods
    {
        (string dt, string err) GetCustomerFullNameById(int custId);
        (DataTable dt, string err) GetCustomer(int custId = 0);
    }
}
