using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TestEmployee.Models
{
    public class Datalayer
    {
        string constr;
        public Datalayer()
        {
            constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        }
        private int ExecuteNonQuery(string query)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(query, con);

                i = cmd.ExecuteNonQuery();

            }
            return i;
        }
        private void ExecuteSelect(string query, dynamic table)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(table);
                }
                catch (Exception)
                {

                    con.Close();
                }
               
            }
        }
        private void ExecuteProcedure(string proc, SqlParameter[] param, dynamic output)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in param)
                {
                    cmd.Parameters.Add(item);
                }
                cmd.CommandTimeout = 100000;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                output = output == null ? new DataTable() : output;
                da.Fill(output);
            }
        }
        private void ExecuteTransactionQuery(string[] query)
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlTransaction sqlTran = con.BeginTransaction();
                SqlCommand cmd = con.CreateCommand();
                cmd.Transaction = sqlTran;
                try
                {
                    foreach (var q in query)
                    {
                        cmd.CommandText = q;
                        cmd.ExecuteNonQuery();
                    }
                    sqlTran.Commit();
                }
                catch (Exception)
                {

                    sqlTran.Rollback(); ;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        internal int UpdateRecord(Employee employee)
        {
            string sql = "Update [tbl_Employee] set [Name]='" + employee.Name + "' ,[Address]='" + employee.Address + "' ,[Dob]='" + employee.DoB + "' ,[Sal]='" + employee.Salery + "' where Emp_Id=" + employee.EmpId;

            return ExecuteNonQuery(sql);
        }

        internal int DeleteRecord(Employee employee)
        {
            string sql = "Delete from [tbl_Employee] where Emp_Id=" + employee.EmpId;
            return ExecuteNonQuery(sql);
        }
        public int InserRecord(Employee emp)
        {
            int id = 0;
            var EmpNo = "EMP_" + Guid.NewGuid().ToString().Split('-')[0];

            string sql = "INSERT INTO [tbl_Employee] ([Emp_No] ,[Name] ,[Address] ,[Dob] ,[Sal] ,[AddedOn]) VALUES ('" + EmpNo + "','" + emp.Name + "','" + emp.Address + "','" + emp.DoB + "','" + emp.Salery + "',getdate());select SCOPE_IDENTITY()";
            DataTable dt = new DataTable();
            ExecuteSelect(sql, dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0][0]);
            }
            return id;
        }
        //public int InserRecord(Employee emp)
        //{
        //    int id = 0;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(constr))
        //        {
        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }
        //            var EmpNo = "EMP_" + Guid.NewGuid().ToString().Split('-')[0];

        //            string sql = "INSERT INTO [tbl_Employee] ([Emp_No] ,[Name] ,[Address] ,[Dob] ,[Sal] ,[AddedOn]) VALUES ('" + EmpNo + "','" + emp.Name + "','" + emp.Address + "','" + emp.DoB + "','" + emp.Salery + "',getdate());select SCOPE_IDENTITY()";
        //            SqlDataAdapter cmd = new SqlDataAdapter(sql, con);
        //            DataTable dt = new DataTable();
        //            cmd.Fill(dt);
        //            if (dt != null && dt.Rows.Count > 0)
        //            {
        //                id = Convert.ToInt32(dt.Rows[0][0]);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return id;
        //}
        public DataTable SelectRecord(int EmpId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string sql = "SELECT [Emp_Id] ,[Emp_No] ,[Name] ,[Address] ,[Dob] ,[Sal] ,[AddedOn] FROM  [tbl_Employee]";
                    sql += EmpId > 0 ? "where Emp_Id=" + EmpId : "";

                    SqlDataAdapter da = new SqlDataAdapter(sql, con);

                    da.Fill(dt);
                }
            }
            catch (Exception)
            {

            }
            return dt;
        }
    }
}