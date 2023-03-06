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
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private List<Exam> _exams;
        public TeacherWindow()
        {
            InitializeComponent();

            this.Loaded += Load;
        }

        private async void Load(object sender, RoutedEventArgs e)
        {
            _exams = await HttpExamsRepository.Instance.GetAllExamsAsync();
            foreach (var exam in _exams)
            {
                this.examsListBox.Items.Add(exam);
            }
        }

        private void txtSearchExam_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Exam> list = new List<Exam>();
            string examName = this.txtSearchExam.Text;
            list = _exams.Where(s => s.ToString().Contains(examName)).ToList();
            this.examsListBox.Items.Clear();
            foreach (Exam exam in list)
            {
                this.examsListBox.Items.Add(exam);
            }
        }

        private async void addExamBtn_Click(object sender, RoutedEventArgs e)
        {
            ExamBuildWindow examInitWindow = new ExamBuildWindow(new Exam());
            examInitWindow.ShowDialog();
            if (examInitWindow.Use)
            {
                Exam exam = examInitWindow.MyExam;
                await HttpExamsRepository.Instance.CreateExamAsync(exam);
            }
            // Reload 
            _exams.Clear();
            _exams = await HttpExamsRepository.Instance.GetAllExamsAsync();
            this.examsListBox.Items.Clear();
            foreach (var ex in _exams)
            {
                this.examsListBox.Items.Add(ex);
            }
        }

        private async void updateExamBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.examsListBox.Items.Count > 0 && this.examsListBox.SelectedIndex > -1)
            {
                Exam exam = (Exam)this.examsListBox.SelectedItem;
                ExamBuildWindow examInitWindow = new ExamBuildWindow(exam);
                examInitWindow.ShowDialog();
                if (examInitWindow.Use)
                {
                    exam = examInitWindow.MyExam;
                    await HttpExamsRepository.Instance.UpdateExamAsync(exam.Id, exam);
                }
                // Reload 
                _exams.Clear();
                _exams = await HttpExamsRepository.Instance.GetAllExamsAsync();
                this.examsListBox.Items.Clear();
                foreach (var ex in _exams)
                {
                    this.examsListBox.Items.Add(ex);
                }
            }
            else
            {
                MessageBox.Show("There is no Exams to Update!");
            }
        }

        private void statsExamBtn_Click(object sender, RoutedEventArgs e)
        {
            string message = "Now need to see statistics about " + this.examsListBox.SelectedItem.ToString();
            MessageBox.Show(message);
        }

    }
}
