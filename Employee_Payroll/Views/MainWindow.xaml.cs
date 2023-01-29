using Employee_Payroll.Model;
using Employee_Payroll.View_Model;
using Employee_Payroll.Views;
using System;
using System.Data;
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
        MainWindows mainWindows = new MainWindows();
        public bool isUpdate = false;
        public int EmpID;
        public EmployeeModel Data()
        {
            var selected_Departemnts = Department_List.Items.Cast<CheckBox>().Where(x => x.IsChecked == true).Select(x => x.Content).ToList();
            string start_Date = Day_Combo.Text + "-" + Month_Combo.Text + "-" + Year_Combo.Text;

            EmployeeModel model = new EmployeeModel()
            {
                EmpID = EmpID,
                Name = Name_txt.Text,
                Profile = mainWindows.profile_Link,
                Gender = GenderMenu.Text,
                Department = string.Join(",", selected_Departemnts),
                Salary = SalarySlider.Value.ToString(),
                Start_Date = start_Date,
                Notes = Notes_txt.Text,
            };
            return model;
        }
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

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isUpdate)
            {
                EmployeeModel model = Data();
                if (mainWindows.IsValid(model))
                {
                    try
                    {
                        mainWindows.InsertEmployee(model);
                        Clear();
                        Dashboard dashboard = new Dashboard();
                        this.Close();
                        dashboard.LoadGrid();
                        dashboard.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                EmployeeModel model = Data();
                if (mainWindows.IsValid(model))
                {
                    try
                    {
                        mainWindows.UpdateEmployee(model);
                        Clear();
                        Dashboard dashboard = new Dashboard();
                        this.Close();
                        dashboard.LoadGrid();
                        dashboard.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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
            dashboard.LoadGrid();
            dashboard.Show();
        }
        private void Radio_Checked(object sender, RoutedEventArgs e)
        {

            mainWindows.profile_Link = (string)(sender as RadioButton).Content;
        }
    }
}
