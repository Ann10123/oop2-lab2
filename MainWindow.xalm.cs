using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOP2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double firstNumber = 0; 
        private double secondNumber = 0;
        private bool isOperatorClicked = false; 
        private string currentOperator = "";
        private Stack<string> history = new Stack<string>();
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                if (isOperatorClicked)
                {
                    TextBoxInput.Clear();
                    isOperatorClicked = false; 
                }
                TextBoxInput.Text += (e.Key - Key.NumPad0).ToString();
            }
            else if (e.Key == Key.Add)
            {
                PlusButton_Click(sender, e);
            }
            else if (e.Key == Key.Subtract)
            {
                MinusButton_Click(sender, e);
            }
            else if (e.Key == Key.Multiply)
            {
                MultiplyButton_Click(sender, e);
            }
            else if (e.Key == Key.Divide)
            {
                DivisionButton_Click(sender, e);
            }
            else if (e.Key == Key.Decimal)
            {
                if (!TextBoxInput.Text.Contains(","))
                {
                    TextBoxInput.Text += ",";
                }
            }
            else if (e.Key == Key.Enter)
            {
                EqualButton_Click(sender, e);
            }
            else if (e.Key == Key.Back)
            {
                if (TextBoxInput.Text.Length > 0)
                {
                    TextBoxInput.Text = TextBoxInput.Text.Substring(0, TextBoxInput.Text.Length - 1);
                }
            }
            else if (e.Key == Key.C)
            {
                ClearButton_Click(sender, e);
            }
            else if (e.Key == Key.Escape)
            {
                ClearEntryButton_Click(sender, e);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (isOperatorClicked)
            {
                TextBoxInput.Text = button.Content.ToString();
                history.Push(TextBoxInput.Text);
                isOperatorClicked = false;
            }
            else
            {
                TextBoxInput.Text += button.Content.ToString();
            }
        }
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                currentOperator = "+";
                isOperatorClicked = true; 
            }
        }
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                currentOperator = "-";
                isOperatorClicked = true;
            }
        }
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                currentOperator = "*";
                isOperatorClicked = true;
            }
        }
        private void DivisionButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                currentOperator = "/";
                isOperatorClicked = true;
            }
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxInput.Clear();
            isOperatorClicked = false;
            firstNumber = 0;
            secondNumber = 0;
            currentOperator = "";
        }
        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            if (history.Count > 0)
            {
                TextBoxInput.Text = history.Pop();
            }
        }
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxInput.Text))
            {
                TextBoxInput.Text = TextBoxInput.Text.Substring(0, TextBoxInput.Text.Length - 1);
            }
        }
        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out secondNumber))
            {
                if (currentOperator == "+")
                {
                    double result = firstNumber + secondNumber;
                    TextBoxInput.Text = result.ToString();
                }
                if (currentOperator == "-")
                {
                    double result = firstNumber - secondNumber;
                    TextBoxInput.Text = result.ToString();
                }
                if (currentOperator == "*")
                {
                    double result = firstNumber * secondNumber;
                    TextBoxInput.Text = result.ToString();
                }
                if (currentOperator == "/")
                {
                    if (secondNumber != 0)
                    {
                        double result = firstNumber / secondNumber;
                        TextBoxInput.Text = result.ToString();
                    }
                    else
                    {
                        MessageBox.Show($"На нуль ділити не можна!");
                    }
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
