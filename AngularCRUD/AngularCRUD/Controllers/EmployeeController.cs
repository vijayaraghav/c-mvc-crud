using AngularCRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        static string connstr = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connstr);
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        //GET:-
        public JsonResult GetAllEmployee()
        {
            List<Employee> employee = new List<Employee>();
            string strQuery = "select * from Employee";
            SqlCommand cmd = new SqlCommand(strQuery, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Employee emp = new Employee();
                emp.emp_id = Convert.ToInt32(dr["emp_id"]);
                emp.emp_name = Convert.ToString(dr["emp_name"]);
                emp.emp_age = Convert.ToInt32(dr["emp_age"]);
                emp.emp_city = Convert.ToString(dr["emp_City"]);
                employee.Add(emp);
            }
            con.Close();
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        //POST:-
        public string Insert_Employee(Employee emp)
        {
            if (emp != null)
            {
                string strQuery = "insert into Employee(emp_name,emp_city,emp_age) values (@name,@city,@age)";
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = emp.emp_name;
                cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = emp.emp_city;
                cmd.Parameters.Add("@age", SqlDbType.Int).Value = emp.emp_age;
                cmd.CommandType = CommandType.Text;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Employee Added Successfully";
                }
                catch (Exception e)
                {
                    con.Close();
                    return e.Message;
                }
            }
            else
            {
                return "employee cannot be inserted! TRY AGAIN";
            }
        }

        //UPDATE:- 
        public string Update_Employee(Employee emp)
        {
            if (emp != null)
            {
                string strQuery = "update Employee set emp_name=@name,emp_age=@age,emp_city=@city where emp_id='"+emp.emp_id+"'";
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = emp.emp_name;
                cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = emp.emp_city;
                cmd.Parameters.Add("@age", SqlDbType.Int).Value = emp.emp_age;
                cmd.CommandType = CommandType.Text;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Employee Updated Successfully";
                }
                catch (Exception e)
                {
                    con.Close();
                    return e.Message;
                }
            }
            else
            {
                return "employee cannot be updated! TRY AGAIN";
            }
        }

        //DELETE:-
        public string Delete_Employee(Employee emp)
        {
            if (emp != null)
            {
                string strQuery = "delete from Employee where emp_id='" + emp.emp_id + "'";
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.CommandType = CommandType.Text;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Employee Deleted Successfully";
                }
                catch (Exception e)
                {
                    con.Close();
                    return e.Message;
                }

            }
            else
            {
                return "employee cannot be Deleted! TRY AGAIN";
            }
        }


    }
}