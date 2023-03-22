using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TelHai.CS.Client.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = this.txtUser.Text.ToString();
            string pass = this.txtPass.Password.ToString();

            if(user == string.Empty || pass == string.Empty)
            {
                string msg = "Please enter Username and Password";
                MessageBox.Show(msg);
                return;
            }
            if (studentButton.IsChecked.Value)
            {
                //if (AuthenticateStudent(user, pass))
                //{
                //    StudentWindow sw = new StudentWindow();
                //    this.Close();
                //    sw.ShowDialog();
                //}
                //else
                //{
                //    string msg = "Wrong details to log in";
                //    MessageBox.Show(msg);
                //    return;
                //}
                StudentWindow sw = new StudentWindow();
                this.Close();
                sw.ShowDialog();
            }
            else if (teacherButton.IsChecked.Value)
            {
                //if (AuthenticateTeacher(user, pass))
                //{
                //    TeacherWindow lw = new TeacherWindow();
                //    this.Close();
                //    lw.ShowDialog();
                //}
                //else
                //{
                //    string msg = "Wrong details to log in";
                //    MessageBox.Show(msg);
                //    return;
                //}
                TeacherWindow lw = new TeacherWindow();
                this.Close();
                lw.ShowDialog();
            }
        }
    
        private bool AuthenticateStudent(string user, string pass)
        {
           if (user.ToLower() == "student" && pass == "12345678")
           {
                return true;
           }
            return false;
        }

        private bool AuthenticateTeacher(string user, string pass)
        {
            if (user.ToLower() == "admin" && pass == "admin")
            {
                return true;
            }
            return false;
        }
    }
}
