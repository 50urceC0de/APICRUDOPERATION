using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using TestEmployee.Models;

namespace TestEmployee.Controllers
{
    public class HomeController : ApiController
    {
        // GET: api/Home
        public JsonResult<List<Employee>> Get()
        {
            Employee emp = new Employee();
            List<Employee>  lst=emp.GetAllData();
            return Json(lst);
        }

        // GET: api/Home/5
        public JsonResult<Employee> Get(int id)
        {
            Employee emp = new Employee();
            emp = emp.GetOneData(id);
            return Json(emp);
        }

        // POST: api/Home
        public JsonResult<Employee> Post(Employee data)
        {
            Employee emp=new Employee ();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //emp = js.Deserialize<Employee>(data);
            if (data!=null)
            {
               var id= data.InserRecord();
                  emp = data.GetOneData(id);
            }
            return Json(emp);
        }

        // PUT: api/Home/5
        public bool Put(string data)
        {
            Employee emp = new Employee();
            JavaScriptSerializer js = new JavaScriptSerializer();
            emp = js.Deserialize<Employee>(data);
            return emp.UpdateRecord()>0;
        }

        // DELETE: api/Home/5
        public bool Delete(int id)
        {
            Employee emp = new Employee();
            return emp.DeleteRecord(id)>0;
        }
    }
}
