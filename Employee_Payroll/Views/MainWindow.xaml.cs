using Employee_Payroll.Views;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Employee_Payroll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public string profile_Link = "";
        public bool isUpdate = false;
        public int EmpID;
        SqlConnection connection = new SqlConnection(@"Server=localhost;Database=Employee_Payroll_Services;User ID=MAHESH/Mahesh;Password=;TrustServerCertificate=True;integrated security=SSPI;");

        public void Clear()
        {
            Name_txt.Clear();
            Notes_txt.Clear();
            a_radio.IsChecked = false;
            b_radio.IsChecked = false;
            c_radio.IsChecked = false;
            d_radio.IsChecked = false;
            GenderMenu.Text = "";
            HR.IsChecked = false;
            Finanace.IsChecked = false;
            Sales.IsChecked = false;
            Engineer.IsChecked = false;
            Others.IsChecked = false;
            SalarySlider.Value = 0;
            Day_Combo.Text = "";
            Month_Combo.Text = "";
            Year_Combo.Text = "";
        }
        public bool IsValid()
        {
            if (Name_txt.Text == String.Empty)
            {
                MessageBox.Show("Name Field is Required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (a_radio.IsChecked == false && b_radio.IsChecked == false && c_radio.IsChecked == false && d_radio.IsChecked == false)
            {
                MessageBox.Show("Profile Image is not selected \nPlease Select Profile Image.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (HR.IsChecked == false && Finanace.IsChecked == false && Sales.IsChecked == false && Engineer.IsChecked == false && Others.IsChecked == false)
            {
                MessageBox.Show("Department is not Selected \nPlease Select Departments ", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (GenderMenu.Text == String.Empty)
            {
                MessageBox.Show("Gender is not Selected \nPlease Select Gender", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (SalarySlider.Value == 0)
            {
                MessageBox.Show("Salary cannot be Zero", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isUpdate)
            {
                if (IsValid())
                {
                    string start_Date = Day_Combo.Text + "-" + Month_Combo.Text + "-" + Year_Combo.Text;
                    var selected_Departemnts = Department_List.Items.Cast<CheckBox>().Where(x => x.IsChecked == true).Select(x => x.Content).ToList();
                    using (connection)
                    {
                        try
                        {
                            SqlCommand command = new SqlCommand("SPInsertEmployee", connection);
                            command.CommandType = CommandType.StoredProcedure;

                            connection.Open();

                            command.Parameters.AddWithValue("@Name", Name_txt.Text);
                            command.Parameters.AddWithValue("@Profile", profile_Link);
                            command.Parameters.AddWithValue("@Gender", GenderMenu.Text);
                            command.Parameters.AddWithValue("@Department", string.Join(",", selected_Departemnts));
                            command.Parameters.AddWithValue("@Salary", Salary_Value.Text);
                            command.Parameters.AddWithValue("@Start_Date", start_Date);
                            command.Parameters.AddWithValue("@Notes", Notes_txt.Text);

                            command.ExecuteNonQuery();
                            MessageBox.Show("User Added Successfully", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                            connection.Close();
                            Clear();
                            Dashboard dashboard = new Dashboard();
                            this.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Cancel_button_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            this.Close();
        }
        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            profile_Link = (string)(sender as RadioButton).Content;
        }
    }
}
