using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AZEEMCRUDWITHADO.Models
{
    public class EmployeeDataAccess
    {
        DBConnection dbConnection;
        public EmployeeDataAccess()
        {
            dbConnection = new DBConnection();
        }

        public List<Employees> GetEmployees()
        {
            string sp = "SP_EMPLOYEES";
            SqlCommand sql = new SqlCommand(sp,dbConnection.Connection);
            sql.Parameters.AddWithValue("@action", "select_join");
            sql.CommandType = CommandType.StoredProcedure;
            if (dbConnection.Connection.State == ConnectionState.Closed)
            {
               dbConnection.Connection.Open();
            }

            SqlDataReader dr = sql.ExecuteReader();
            List<Employees> employees = new List<Employees>();
             while(dr.Read())
            {
                Employees Emp = new Employees();
                Emp.Id = (int)dr["id"];
                Emp.Name = dr["name"].ToString();
                Emp.Email = dr["email"].ToString();
                Emp.Gender = dr["gender"].ToString();
                Emp.Mobile = dr["mobile"].ToString();
                Emp.DName = dr["department"].ToString();
                employees.Add(Emp);
            }

            dbConnection.Connection.Close();
            return employees;
        }




    }
}
