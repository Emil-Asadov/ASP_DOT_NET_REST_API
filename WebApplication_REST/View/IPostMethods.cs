using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_REST.Models;

namespace WebApplication_REST.View
{
    public interface IPostMethods
    {
        (string res, string err) FileDelete(int custId);
        (string res,string custIdn, string err) FileOper(ClassControlsOper cls);
    }
}
