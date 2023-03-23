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
using System.Windows.Threading;
using TelHai.CS.Client.Models;
using TelHai.CS.Client.Repositories;

namespace TelHai.CS.Client.View
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : Window
    {
        Submit submit { get; set; }
        public Exam exam { get; set; }
        public List<string> Answers { get; set; }
        public int Answered { get; set; }
        private DispatcherTimer timer { get; set; }
        private TimeSpan remainingTime { get; set; }

        public ExamWindow(Exam getExam, string studentName, string studentId)
        {
            InitializeComponent();
            DataContext = this;
            this.exam = getExam;
            Answers = new List<string>();
            submit = new Submit { StudentId = studentId, StudentName = studentName , ExamId = exam._id , _grade = 0};
            this.txtStudent.Text = studentName;
            this.txtId.Text = studentId;
            this.Loaded += Load;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            if(this.exam.IsOrderRandom == true) // order of questions will randomize
            {
                Random rnd = new Random();
                exam.Questions = exam.Questions.Select(x => new { value = x, order = rnd.Next() })
                    .OrderBy(x => x.order).Select(x => x.value).ToList();
            }

            foreach (var question in exam.Questions)
            {
                if (question.IsRand == true) // order of answers will randomize
                {
                    Random rnd = new Random();
                    question.Answers = question.Answers.Select(x => new { value = x, order = rnd.Next() })
                        .OrderBy(x => x.order).Select(x => x.value).ToList();
                }
            }
            // Set the answers list to empty strings but the same size of the Question list
            for (int i = 0; i < this.exam.Questions.Count; i++)
            {
                Answers.Add(string.Empty);
            }
            // insert the questions to ListBox
            this.questionsListBox.ItemsSource = exam.Questions;
            this.questionsListBox.SelectedIndex = 0;
            this.txtNumOfQuestions.Text = exam.Questions.Count.ToString();
            Answered = 0;

            // Set Timer - start time
            timer = new DispatcherTimer();
            double time = (double)exam.TotalTime;
            remainingTime = TimeSpan.FromHours(time);
            timeLabel.Content = remainingTime.ToString(@"hh\:mm\:ss");
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
            if (remainingTime < TimeSpan.Zero)
            {
                remainingTime = TimeSpan.Zero;
                timer.Stop();
                MessageBox.Show("Time is over we sorry");
                SubmitExam();
                this.Close();
            }
            timeLabel.Content = remainingTime.ToString(@"hh\:mm\:ss");
        }

        private void questionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set Title of Question
            Question currQuestion = (Question)questionsListBox.SelectedItem;
            this.txtTitle.Text = currQuestion.Text;

            // Set all the Answers as RadioButtons and put in the StackPanel
            int questionIndex = this.questionsListBox.SelectedIndex;
            if (questionIndex < 0 || questionIndex >= Answers.Count)
            {
                return;
            }
            this.answersStackPanel.Children.Clear();
            if (Answers[questionIndex] == string.Empty) // NOT answer yet
            {
                // Set all answers as RadioButtons and add them to StackPanel
                foreach (var item in currQuestion.Answers)
                {
                    TextBlock tb = new TextBlock { Text = item.Text , TextWrapping = TextWrapping.Wrap , FontSize = 20};
                    RadioButton rb = new RadioButton { Content = tb, GroupName = currQuestion.Text };
                    rb.Checked += RadioButton_Checked;
                    rb.VerticalContentAlignment = VerticalAlignment.Center;
                    rb.Margin = new Thickness(10);
                    this.answersStackPanel.Children.Add(rb);
                }
            }
            else // Have an Answer
            {
                // Set all answers as RadioButtons and add them to StackPanel (mark the answer that the student select before)
                for (int i = 0; i < currQuestion.Answers.Count; i++)
                {
                    TextBlock tb = new TextBlock { Text = currQuestion.Answers[i].Text, TextWrapping = TextWrapping.Wrap , FontSize = 20};
                    if (Answers[questionIndex] == currQuestion.Answers[i].Text)
                    {
                        RadioButton rb = new RadioButton { Content = tb, GroupName = currQuestion.Text, IsChecked = true };
                        rb.Checked += RadioButton_Checked;
                        rb.VerticalContentAlignment = VerticalAlignment.Center;
                        rb.Margin = new Thickness(10);
                        this.answersStackPanel.Children.Add(rb);
                    }
                    else
                    {
                        RadioButton rb = new RadioButton { Content = tb, GroupName = currQuestion.Text, IsChecked = false };
                        rb.Checked += RadioButton_Checked;
                        rb.VerticalContentAlignment = VerticalAlignment.Center;
                        rb.Margin = new Thickness(10);
                        this.answersStackPanel.Children.Add(rb);
                    }
                }
            }
        }

        private void prevQuestion_Click(object sender, RoutedEventArgs e)
        {
            int curr = this.questionsListBox.SelectedIndex;
            if (curr > 0)
            {
                this.questionsListBox.SelectedIndex--;
            }
            else
            {
                MessageBox.Show("This is the first question");
            }
        }

        private void nextQuestion_Click(object sender, RoutedEventArgs e)
        {
            int curr = this.questionsListBox.SelectedIndex;
            if (curr < this.questionsListBox.Items.Count -1)
            {
                this.questionsListBox.SelectedIndex++;
            }
            else
            {
                MessageBox.Show("This is the last question");
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            int index = this.questionsListBox.SelectedIndex;
            if (Answers[index] == string.Empty) // Not have answer yet to Question[index]
            {
                if (Answered < exam.Questions.Count)
                {
                    Answered++;
                    this.txtNumOfAnswered.Text = Answered.ToString();

                    // Set Question answered - paint in LightGreen the background of the question item in the ListBox
                    ListBoxItem? item = questionsListBox.ItemContainerGenerator.ContainerFromItem(questionsListBox.SelectedItem) as ListBoxItem;
                    if(item != null)
                        item.Background = new SolidColorBrush(Colors.LightGreen);
                }
            }
            // Save the selected answer
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                TextBlock tb = rb.Content as TextBlock;
                if (tb != null && rb.IsChecked.Value)
                {
                    Answers[index] = tb.Text;
                }
            }
        }

        private void finishExamBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Answered != exam.Questions.Count) // Too Early
            {
                string msg = "NOT all Questions are answered, are you sure you finished?";
                MessageBoxResult res = MessageBox.Show(msg, "WAIT", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    SubmitExam();
                    Close();
                }
            }
            else
            {
                string msg = "Are you sure you finished?";
                MessageBoxResult res = MessageBox.Show(msg, "WAIT", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    SubmitExam();
                    Close();
                }
            }

        }
    
        private async void SubmitExam()
        {
            // Calculate grade
            int totalTrue = 0;
            for (int i = 0; i < exam.Questions.Count; i++)
            {
                string correctAnswer = exam.Questions[i].Answers.Where(ans => ans.IsCorrect).FirstOrDefault().Text;
                if (this.Answers[i] == string.Empty) // not answered
                {
                    Error err = new Error
                    {
                        QuestionTitle = exam.Questions[i].Text,
                        ChosenAnswer = "Not selected answer",
                        CorrectAnswer = correctAnswer
                    };
                    submit.Errors.Add(err);
                    continue;
                }
                if (this.Answers[i] == correctAnswer) // correct Answer
                {
                    totalTrue += 1;
                }
                else // wrong answer
                {
                    Error err = new Error
                    {
                        QuestionTitle = exam.Questions[i].Text,
                        ChosenAnswer = this.Answers[i],
                        CorrectAnswer = correctAnswer
                    };
                    submit.Errors.Add(err);
                }
            }
            submit._grade = ((double)totalTrue / (double)Answers.Count) * 100;
            submit._grade = Math.Round(submit._grade, 2);
            // Add submit to DB
            await HttpExamsRepository.Instance.CreateSubmitAsync(exam.Id, submit);
        }
    }
}
