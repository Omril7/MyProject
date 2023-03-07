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

namespace TelHai.CS.Client.View
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        public Question MyQuestion { get; set; }
        public QuestionWindow()
        {
            InitializeComponent();
            MyQuestion = new Question();
        }

        private void addOptionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtAnswer.Text == "")
            {
                MessageBox.Show("Please Write Your Answer!");
            }
            else
            {
                this.optionsListBox.Items.Add(this.txtAnswer.Text);
                this.txtAnswer.Text = "";
                this.optionsListBox.SelectedIndex = this.optionsListBox.Items.Count - 1;
            }
        }

        private void removeOptionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.optionsListBox.Items.Count > 0)
            {
                var index = this.optionsListBox.SelectedIndex;
                this.optionsListBox.Items.Remove(this.optionsListBox.Items[index]);
                this.optionsListBox.SelectedIndex = 0;
            }
        }

        private void addQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtQuestion.Text == "")
            {
                MessageBox.Show("Please Write Your Question...");
                return;
            }
            if (this.optionsListBox.Items.Count < 2)
            {
                MessageBox.Show("Please Enter at Least 2 Answers...");
                return;
            }
            MyQuestion.Text = this.txtQuestion.Text;
            if (this.IsRand.IsChecked == true)
            {
                MyQuestion.IsRand = true;
            }
            foreach (var item in this.optionsListBox.Items)
            {
                string? text = item.ToString();
                if(text != null)
                {
                    Answer answer = new Answer(text, false);
                    if (this.optionsListBox.SelectedIndex == this.optionsListBox.Items.IndexOf(item))
                    {
                        answer.IsCorrect = true;
                    }
                    MyQuestion.Answers.Add(answer);
                }
            }
            this.Close();
        }
    }
}
