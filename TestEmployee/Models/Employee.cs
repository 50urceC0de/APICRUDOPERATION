using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TestEmployee.Models
{
    public class Employee
    {
        private Datalayer dl;
        public int EmpId { get; set; }
        public string Emp_NO { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DoB { get; set; }
        public double Salery { get; set; }
        public Employee()
        {
            dl = new Datalayer();
        }
        public int InserRecord()
        {
            int status = 0;
            Datalayer dl = new Datalayer();
            status=dl.InserRecord(this);
            return status;
        }
        public List<Employee> GetAllData()
        {
            List<Employee> lst = new List<Employee>();
            DataTable dt=dl.SelectRecord(0);
            if (dt!=null && dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.EmpId = Convert.ToInt32(dt.Rows[i]["Emp_Id"]);
                    emp.Emp_NO = (dt.Rows[i]["Emp_Id"].ToString());
                    emp.Name = (dt.Rows[i]["Name"].ToString());
                    emp.Address = (dt.Rows[i]["Address"].ToString());
                    emp.DoB = (dt.Rows[i]["Dob"].ToString());
                    emp.Salery = Convert.ToDouble(dt.Rows[i]["Sal"].ToString());
                    lst.Add(emp);
                }
            }
            return lst;
        }
        public Employee GetOneData(int id)
        {
            Employee emp = new Employee();
            DataTable dt = dl.SelectRecord(id);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    emp.EmpId = Convert.ToInt32(dt.Rows[i]["Emp_Id"]);
                    emp.Emp_NO = (dt.Rows[i]["Emp_Id"].ToString());
                    emp.Name = (dt.Rows[i]["Name"].ToString());
                    emp.Address = (dt.Rows[i]["Address"].ToString());
                    emp.DoB = (dt.Rows[i]["Dob"].ToString());
                    emp.Salery = Convert.ToDouble(dt.Rows[i]["Sal"].ToString());
                }
            }
            return emp;
        }
        public int UpdateRecord()
        {
            int status = 0;
            status = dl.UpdateRecord(this);
            return status;
        }
        public int DeleteRecord(int id)
        {
            this.EmpId = id;
            int status = 0;
            status = dl.DeleteRecord(this);
            return status;
        }
    }
}