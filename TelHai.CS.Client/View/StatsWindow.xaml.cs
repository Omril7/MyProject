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
using TelHai.CS.Client.Models;
using TelHai.CS.Client.Repositories;

namespace TelHai.CS.Client.View
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        public List<Submit> _grades;
        public StatsWindow(Exam exam)
        {
            InitializeComponent();
            this._grades = exam.Submissions;
            this.txtTitle.Content = exam.Name;
            this.txtId.Content = exam._id;
            this.Loaded += Load;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            double count = 0;
            foreach (var grade in _grades)
            {
                count += grade._grade;
                this.studentsListBox.Items.Add(grade);
            }
            count = count / (double)_grades.Count;
            count = Math.Round(count, 2);
            this.txtAvg.Content = count.ToString();
        }

        private void studentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Submit grade = (Submit)this.studentsListBox.SelectedItem; ;
            this.txtStudent.Content = grade.StudentName;
            this.txtStudentId.Content = grade.StudentId;
            this.txtStudentGrade.Content = grade._grade;
            if(grade._grade < 56)
            {
                this.txtStudentGrade.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                this.txtStudentGrade.Foreground = new SolidColorBrush(Colors.Green);
            }
            this.errorsListBox.Items.Clear();
            this.errorsListBox.SelectedIndex = -1;
            this.txtSelected.Text = string.Empty;
            this.txtCorrect.Text = string.Empty;
            if (grade.Errors.Count > 0)
            {
                foreach (var error in grade.Errors)
                {
                    this.errorsListBox.Items.Add(error);
                }
            }
        }

        private void errorsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.errorsListBox.SelectedIndex == -1)
            {
                return;
            }
            Error error = (Error)this.errorsListBox.SelectedItem;
            this.txtSelected.Text = error.ChosenAnswer;
            this.txtCorrect.Text = error.CorrectAnswer;
        }
    }
}
