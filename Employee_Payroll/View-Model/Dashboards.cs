
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using Employee_Payroll.Model;
using System.Collections.Generic;

namespace Employee_Payroll.View_Model
{
    public class Dashboards
    {
        public SqlConnection connection = new SqlConnection(@"Server=localhost;Database=Employee_Payroll_Services;User ID=MAHESH/Mahesh;Password=;TrustServerCertificate=True;integrated security=SSPI;");
        public List<EmployeeModel> GetAllEmployee()
        {
            try
            {
                List<EmployeeModel> employeelist = new List<EmployeeModel>();

                SqlCommand command = new SqlCommand("Select EmpID,Name,Profile,Gender,Department,Salary,Start_Date,Notes from Employee_Payroll_Table", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employeelist.Add(new EmployeeModel()
                        {
                            Name = reader.IsDBNull("Name") ? string.Empty : reader.GetString("Name"),
                            EmpID = reader.IsDBNull("EmpID") ? 0 : reader.GetInt32("EmpID"),
                            Profile = reader.IsDBNull("Profile") ? string.Empty : reader.GetString("Profile"),
                            Gender = reader.IsDBNull("Gender") ? string.Empty : reader.GetString("Gender"),
                            Department = reader.IsDBNull("Department") ? string.Empty : reader.GetString("Department"),
                            Salary = reader.IsDBNull("Salary") ? string.Empty : reader.GetString("Salary"),
                            Start_Date = reader.IsDBNull("Start_Date") ? string.Empty : reader.GetString("Start_Date"),
                            Notes = reader.IsDBNull("Notes") ? string.Empty : reader.GetString("Notes"),
                        });
                    }
                }
                connection.Close();
                return employeelist;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void DeleteEmployee(int EmpID)
        {
            try
            {
                SqlCommand command = new SqlCommand("SPDeleteEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@EmpID", EmpID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
