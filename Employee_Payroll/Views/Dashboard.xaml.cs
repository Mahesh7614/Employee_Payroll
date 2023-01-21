
using System;
using System.Data;
using System.Data.SqlClient;
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
        SqlConnection connection = new SqlConnection(@"Server=localhost;Database=Employee_Payroll_Services;User ID=MAHESH/Mahesh;Password=;TrustServerCertificate=True;integrated security=SSPI;");
        public void LoadGrid()
        {
            SqlCommand command = new SqlCommand("Select EmpID,Name,Profile,Gender,Department,Salary,Start_Date,Notes from Employee_Payroll_Table", connection);
            DataTable table = new DataTable();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            table.Load(reader);
            connection.Close();
            EmployeeDataGrid.ItemsSource = table.DefaultView;
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
        private void DeleteEvent(object sender, RoutedEventArgs e)
        {
            DataRowView data = (DataRowView)EmployeeDataGrid.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Datete Conformation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                SqlCommand command = new SqlCommand("SPDeleteEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@EmpID", data["EmpID"]);
                command.ExecuteNonQuery();
                connection.Close();
                LoadGrid();
            }
        }
        private void EditEvent(object sender, RoutedEventArgs e)
        {
            DataRowView data = (DataRowView)EmployeeDataGrid.SelectedItem;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Name_txt.Text = data["Name"].ToString();
            mainWindow.GenderMenu.Text = data["Gender"].ToString();
            mainWindow.Salary_Value.Text = data["Salary"].ToString();
            string fullDate = data["Start_Date"].ToString();
            string[] start_date = fullDate.Split("-");
            mainWindow.Day_Combo.Text = start_date[0];
            mainWindow.Month_Combo.Text = start_date[1];
            mainWindow.Year_Combo.Text = start_date[2];
            mainWindow.Notes_txt.Text = data["Notes"].ToString();
            mainWindow.EmpID = Convert.ToInt32(data["EmpID"]);
            mainWindow.isUpdate= true;
            this.Close();
            mainWindow.Show();
        }
    }
}
