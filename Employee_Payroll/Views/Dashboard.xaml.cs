
using Employee_Payroll.Model;
using Employee_Payroll.View_Model;
using System;
using System.Windows;

namespace Employee_Payroll.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadGrid();
        }
        Dashboards dashboards = new Dashboards();
        public void LoadGrid()
        {
            try
            {
                EmployeeDataGrid.ItemsSource = dashboards.GetAllEmployee();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
        private void DeleteEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                EmployeeModel data = (EmployeeModel)EmployeeDataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Datete Conformation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    dashboards.DeleteEmployee(data.EmpID);
                    LoadGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EditEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                EmployeeModel data = (EmployeeModel)EmployeeDataGrid.SelectedItem;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Name_txt.Text = data.Name;
                mainWindow.GenderMenu.Text = data.Gender;
                mainWindow.Salary_Value.Text = data.Salary;
                string fullDate = data.Start_Date;
                string[] start_date = fullDate.Split("-");
                mainWindow.Day_Combo.Text = start_date[0];
                mainWindow.Month_Combo.Text = start_date[1];
                mainWindow.Year_Combo.Text = start_date[2];
                mainWindow.Notes_txt.Text = data.Notes;
                mainWindow.EmpID = data.EmpID;
                mainWindow.isUpdate = true;
                this.Close();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
