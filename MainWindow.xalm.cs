using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOP2
{
    public partial class MainWindow : Window
    {
        private double firstNumber = 0;
        private double secondNumber = 0;
        private bool isOperatorClicked = false;
        private string currentOperator = "";
        private bool isMenuOpen = false;
        private Stack<CalculatorState> stateHistory = new Stack<CalculatorState>();
        private void SaveState()
        {
            stateHistory.Push(new CalculatorState
            {
                Text = TextBoxInput.Text,
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operator = currentOperator,
                IsOperatorClicked = isOperatorClicked
            });
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                SaveState();
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
                SaveState();
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
                BackspaceButton_Click(sender, e);
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
            SaveState();
            var button = sender as Button;
            if (isOperatorClicked)
            {
                TextBoxInput.Text = button.Content.ToString();
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
                SaveState();
                currentOperator = "+";
                isOperatorClicked = true;
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                SaveState();
                currentOperator = "-";
                isOperatorClicked = true;
            }
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                SaveState();
                currentOperator = "*";
                isOperatorClicked = true;
            }
        }

        private void DivisionButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                SaveState();
                currentOperator = "/";
                isOperatorClicked = true;
            }
        }
        private void PiButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                double result = firstNumber * Math.PI;
                TextBoxInput.Text = result.ToString();
            }
        }
        private void EpsButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                double result = Math.E * firstNumber;
                TextBoxInput.Text = result.ToString();
            }
        }
        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            // Перевірка, чи є значення в текстовому полі
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                // Запитуємо у користувача степінь
                string input = Microsoft.VisualBasic.Interaction.InputBox("Введіть степінь:", "Степінь", "2");
                if (double.TryParse(input, out double exponent))
                {
                    // Підносимо число до вказаного степеня
                    double result = Math.Pow(firstNumber, exponent);
                    TextBoxInput.Text = result.ToString(); // Виводимо результат
                }
            }
        }
        private void SqrtButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                if (firstNumber >= 0)
                {
                    double result = Math.Sqrt(firstNumber);
                    TextBoxInput.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show("Не можна знайти корінь з від’ємного числа!");
                }
            }
        }
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber) && firstNumber > 0)
            {
                double result = Math.Log10(firstNumber);
                TextBoxInput.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("Логарифм можна обчислити тільки для чисел більше 0.");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxInput.Clear();
            isOperatorClicked = false;
            firstNumber = 0;
            secondNumber = 0;
            currentOperator = "";
            stateHistory.Clear();
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            if (stateHistory.Count > 0)
            {
                var previousState = stateHistory.Pop();
                TextBoxInput.Text = previousState.Text;
                firstNumber = previousState.FirstNumber;
                secondNumber = previousState.SecondNumber;
                currentOperator = previousState.Operator;
                isOperatorClicked = previousState.IsOperatorClicked;
            }
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            SaveState();
            if (!string.IsNullOrEmpty(TextBoxInput.Text))
            {
                TextBoxInput.Text = TextBoxInput.Text.Substring(0, TextBoxInput.Text.Length - 1);
            }
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out secondNumber))
            {
                SaveState();
                double result = 0;

                switch (currentOperator)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-": 
                        result = firstNumber - secondNumber;
                        break;
                    case "*":
                        result = firstNumber * secondNumber;
                        break;
                    case "/":
                        if (secondNumber != 0)
                            result = firstNumber / secondNumber;
                        else
                        {
                            MessageBox.Show("На нуль ділити не можна!");
                            return;
                        }
                        break;
                    case "nʸ";
                        result = Math.Pow(firstNumber, secondNumber);
                        break;
                }

                TextBoxInput.Text = result.ToString();
                firstNumber = result;
                isOperatorClicked = false;
            }
        }
        private void BurgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen)
            {
                ExtraButtonsColumn.Width = new GridLength(0);
                SetAdditionalButtonsVisibility(Visibility.Collapsed);
            }
            else
            {
                ExtraButtonsColumn.Width = new GridLength(1, GridUnitType.Star);
                SetAdditionalButtonsVisibility(Visibility.Visible);
            }
            isMenuOpen = !isMenuOpen;
        }
        private void SetAdditionalButtonsVisibility(Visibility visibility)
        {
            ButtonSqrt.Visibility = visibility;
            ButtonPi.Visibility = visibility;
            ButtonPower.Visibility = visibility;
            ButtonLog.Visibility = visibility;
            ButtonEps.Visibility = visibility;
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
    public class CalculatorState
    {
        public string Text { get; set; }
        public double FirstNumber { get; set; }
        public double SecondNumber { get; set; }
        public string Operator { get; set; }
        public bool IsOperatorClicked { get; set; }
    }
}
