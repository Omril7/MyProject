using System;
using System.Collections.Generic;
using System.Linq;
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
                MessageBox.Show("Please Enter User & Password!");
                return;
            }
            if (studentButton.IsChecked.Value)
            {
                StudentWindow sw = new StudentWindow();
                this.Close();
                sw.ShowDialog();
            }
            else if (teacherButton.IsChecked.Value)
            {
                TeacherWindow lw = new TeacherWindow();
                this.Close();
                lw.ShowDialog();
            }
        }
    }
}
