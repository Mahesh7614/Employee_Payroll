
using System.Windows;
using System;
using System.Data.SqlClient;
using System.Data;
using Employee_Payroll.Model;

namespace Employee_Payroll.View_Model
{
    public class MainWindows
    {
        Dashboards dashboards = new Dashboards();
        public string profile_Link = "";
        public int EmpID;
        public bool IsValid(EmployeeModel model)
        {
            if (model.Name == String.Empty)
            {
                MessageBox.Show("Name Field is Required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.Profile == String.Empty)
            {
                MessageBox.Show("Profile Image is not selected \nPlease Select Profile Image.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.Department == String.Empty)
            {
                MessageBox.Show("Department is not Selected \nPlease Select Departments ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.Gender == String.Empty)
            {
                MessageBox.Show("Gender is not Selected \nPlease Select Gender", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.Salary == "0")
            {
                MessageBox.Show("Salary cannot be Zero", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (model.Start_Date == String.Empty )
            {
                MessageBox.Show("Please Select Start Date", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public void InsertEmployee(EmployeeModel employee)
        {

            using (dashboards.connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("SPInsertEmployee", dashboards.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    dashboards.connection.Open();

                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Profile", employee.Profile);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@Start_Date", employee.Start_Date);
                    command.Parameters.AddWithValue("@Notes", employee.Notes);

                    int addOrNot = command.ExecuteNonQuery();
                    if (addOrNot >= 1)
                    {
                        MessageBox.Show("User Added Successfully", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("User Name Already Exists!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    dashboards.connection.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        public void UpdateEmployee(EmployeeModel employee)
        {
            using (dashboards.connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("SPUpdateEmployee", dashboards.connection);
                    command.CommandType = CommandType.StoredProcedure;

                    dashboards.connection.Open();
                    command.Parameters.AddWithValue("@EmpID", employee.EmpID);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Profile", employee.Profile);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@Start_Date", employee.Start_Date);
                    command.Parameters.AddWithValue("@Notes", employee.Notes);


                    command.ExecuteNonQuery();
                    MessageBox.Show("Updated User Data Successfully", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);

                    dashboards.connection.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
