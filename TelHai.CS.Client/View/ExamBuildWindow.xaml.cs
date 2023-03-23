using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ExamBuildWindow.xaml
    /// </summary>
    public partial class ExamBuildWindow : Window
    {
        public Exam MyExam { get; set; }
        public bool Use { get; set; }
        public ExamBuildWindow(Exam exam)
        {
            InitializeComponent();
            MyExam = exam;
            Use = false;
            DataContext = MyExam;
            this.Loaded += Load;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            if(MyExam._id == string.Empty)
            {
                MyExam._id = Guid.NewGuid().ToString();
            }
            foreach (var question in MyExam.Questions)
            {
                this.questionsListBox.Items.Add(question);
            }
        }

        private void addExamBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtExamName.Text == "")
            {
                MessageBox.Show("Please Enter Exam Name");
                return;
            }
            if (this.txtExamDay.Text == "" || this.txtExamMonth.Text == "" || this.txtExamYear.Text == "")
            {
                MessageBox.Show("Please Enter Full Exam Date");
                return;
            }
            if (this.txtExamTeacher.Text == "")
            {
                MessageBox.Show("Please Enter Exam Teacher Name");
                return;
            }
            if (this.txtExamStartHour.Text == "" || this.txtExamStartMinute.Text == "")
            {
                MessageBox.Show("Please Enter Full Start Hour");
                return;
            }
            if (this.txtExamTotalTime.Text == "")
            {
                MessageBox.Show("Please Enter Full Total Time");
                return;
            }
            if (this.questionsListBox.Items.Count < 2)
            {
                MessageBox.Show("Please Enter at Least 2 Questions...");
                return;
            }
            MyExam.Questions.Clear();
            foreach (var item in questionsListBox.Items)
            {
                MyExam.Questions.Add((Question)item);
            }
            this.Close();
        }

        private async void addQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            QuestionWindow questionWindow = new QuestionWindow();
            questionWindow.ShowDialog();
            if (questionWindow.MyQuestion != null)
            {
                this.questionsListBox.Items.Add(questionWindow.MyQuestion);
                // Now we know that somthing is changed
                Use = true;
                await HttpExamsRepository.Instance.CreateQuestionAsync(MyExam.Id, questionWindow.MyQuestion);
            }
        }

        private async void removeQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.questionsListBox.Items.Count > 0 && this.questionsListBox.SelectedIndex != -1)
            {
                await HttpExamsRepository.Instance.DeleteQuestionAsync(MyExam.Id, ((Question)this.questionsListBox.SelectedItem).Id);
                this.questionsListBox.Items.Remove(this.questionsListBox.SelectedItem);
                this.questionsListBox.SelectedIndex = 0;
                // Now we know that somthing is changed
                Use = true;
            }
        }

        private void textChanged(object sender, TextChangedEventArgs e)
        {
            // Now we know that somthing is changed
            Use = true;
        }

        private void IsRand_Checked(object sender, RoutedEventArgs e)
        {
            // Now we know that somthing is changed
            Use = true;
        }
    }
}
