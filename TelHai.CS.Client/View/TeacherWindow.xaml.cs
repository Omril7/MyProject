using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

            this.Loaded += loadFunc;
        }

        private async void loadFunc(object sender, RoutedEventArgs e)
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
            string query = this.txtSearchExam.Text;
            list = _exams.Where(s => s.ToString().ToLower().Contains(query)).ToList();
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
                this.tempListBox.Items.Add(exam);
                Save(exam);
            }
        }

        private async void updateExamBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.examsListBox.Items.Count > 0 && this.examsListBox.SelectedIndex > -1)
            {
                Exam exam = (Exam)this.examsListBox.SelectedItem;
                ExamBuildWindow examBuildWindow = new ExamBuildWindow(exam);
                examBuildWindow.ShowDialog();
                if (examBuildWindow.Use)
                {
                    exam = examBuildWindow.MyExam;
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
                MessageBox.Show("Please select Exam from DB!");
            }
        }

        private async void deleteExamBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.examsListBox.Items.Count > 0 && this.examsListBox.SelectedIndex > -1)
            {
                Exam exam = (Exam)this.examsListBox.SelectedItem;
                await HttpExamsRepository.Instance.DeleteExamAsync(exam.Id);
                
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
                MessageBox.Show("Please select Exam from DB!");
            }
        }

        private async void statsExamBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.examsListBox.Items.Count > 0 && this.examsListBox.SelectedIndex > -1)
            {
                Exam exam = (Exam)this.examsListBox.SelectedItem;
                exam = await HttpExamsRepository.Instance.GetExamAsync(exam.Id);
                StatsWindow statsWindow = new StatsWindow(exam);
                statsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select Exam from DB!");
            }
        }

        private void loadExamBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string jsonPath = openFileDialog.FileName;
                Load(jsonPath);
            }
        }

        private async void addToDbBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.tempListBox.SelectedIndex == -1) return;
            Exam exam = this.tempListBox.SelectedItem as Exam;
            if(exam != null)
            {
                await HttpExamsRepository.Instance.CreateExamAsync(exam);
            }
            this.tempListBox.Items.RemoveAt(this.tempListBox.SelectedIndex);
            // Reload 
            _exams.Clear();
            _exams = await HttpExamsRepository.Instance.GetAllExamsAsync();
            this.examsListBox.Items.Clear();
            foreach (var ex in _exams)
            {
                this.examsListBox.Items.Add(ex);
            }
        }

        public void Save(Exam exam)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonExamsString = JsonSerializer.Serialize<Exam>(exam, options);

            if (!Directory.Exists("AppData"))
            {
                Directory.CreateDirectory("AppData");
            }
            string path = "AppData/" + exam.Name + ".json";
            File.WriteAllText(path, jsonExamsString);
        }

        public void Load(string path)
        {
            if (path != string.Empty)
            {
                string examsText = File.ReadAllText(path);
                var exam = JsonSerializer.Deserialize<Exam>(examsText);

                this.tempListBox.Items.Add(exam);
            }
        }

        
    }
}
