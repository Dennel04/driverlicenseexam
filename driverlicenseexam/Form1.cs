using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace driverlicenseexam
{
    public partial class Form1 : Form
    {
        private readonly string[] correctAnswers = {
            "B", "D", "A", "A", "C", "A", "B", "A", "C", "D",
            "B", "C", "D", "A", "D", "C", "C", "B", "D", "A"
        };

        private string[] studentAnswers;
        private int correctCount = 0;
        private int incorrectCount = 0;
        private List<int> incorrectQuestions = new List<int>();

        public Form1()
        {
            InitializeComponent();
            label1.Text = "Press Start to check answers.";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string filePath = "student_answers.txt"; 
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Error: File 'student_answers.txt' not found.");
                return;
            }

            studentAnswers = File.ReadAllLines(filePath);

            if (studentAnswers.Length != correctAnswers.Length)
            {
                MessageBox.Show($"Error: The file must contain exactly {correctAnswers.Length} answers.");
                return;
            }

            CheckAnswers();

            ShowResults();
        }

        private void CheckAnswers()
        {
            correctCount = 0;
            incorrectCount = 0;
            incorrectQuestions.Clear();

            for (int i = 0; i < correctAnswers.Length; i++)
            {
                if (studentAnswers[i].Trim().ToUpper() == correctAnswers[i])
                {
                    correctCount++;
                }
                else
                {
                    incorrectCount++;
                    incorrectQuestions.Add(i + 1);
                }
            }
        }

        private void ShowResults()
        {
            string result = correctCount >= 15
                ? "Congratulations! You passed the exam!"
                : "Sorry, you failed the exam.";

            label1.Text = $"{result}\n" +
                          $"Correct Answers: {correctCount}\n" +
                          $"Incorrect Answers: {incorrectCount}";

            if (incorrectQuestions.Count > 0)
            {
                label1.Text += "\nIncorrect Questions: " + string.Join(", ", incorrectQuestions);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}