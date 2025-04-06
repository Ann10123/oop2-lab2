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
        private Stack<CalculatorState> redoStack = new Stack<CalculatorState>();
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
                UndoButton_Click(sender, e);
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
                SaveState();
                currentOperator = "π";
                isOperatorClicked = true;
            }
        }
        private void EpsButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                SaveState();
                currentOperator = "e";
                isOperatorClicked = true;
            }
        }
        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                SaveState();
                currentOperator = "nʸ";
                isOperatorClicked = true;
            }
        }
        private void SqrtButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                SaveState();
                currentOperator = "√x";
                isOperatorClicked = true;
            }
        }
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out firstNumber))
            {
                SaveState();
                currentOperator = "log";
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
            stateHistory.Clear();
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            if (stateHistory.Count > 0)
            {
                var currentState = new CalculatorState
                {
                    Text = TextBoxInput.Text,
                    FirstNumber = firstNumber,
                    SecondNumber = secondNumber,
                    Operator = currentOperator,
                    IsOperatorClicked = isOperatorClicked
                };

                redoStack.Push(currentState);

                var previousState = stateHistory.Pop();
                ApplyState(previousState);
            }
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            if (redoStack.Count > 0)
            {
                SaveState(); // поточний стан іде в історію

                var nextState = redoStack.Pop();
                ApplyState(nextState);
            }
        }

        private void ApplyState(CalculatorState state)
        {
            TextBoxInput.Text = state.Text;
            firstNumber = state.FirstNumber;
            secondNumber = state.SecondNumber;
            currentOperator = state.Operator;
            isOperatorClicked = state.IsOperatorClicked;
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
                    case "nʸ":
                        result = Math.Pow(firstNumber, secondNumber);
                        break;
                    case "log":
                        if (firstNumber > 0)
                            result = Math.Log10(firstNumber);
                        else
                        {
                            MessageBox.Show("Логарифм можна обчислити тільки для чисел більше 0!");
                        }
                        break;
                    case "e":
                        result = Math.E * firstNumber;
                        break;
                    case "π":
                        result = firstNumber * Math.PI;
                        break;
                    case "√x":
                        if (firstNumber >= 0)
                            result = Math.Sqrt(firstNumber);
                        else
                        {
                            MessageBox.Show("Не можна знайти корінь з від’ємного числа!");
                        }
                        break;
                }
                TextBoxInput.Text = result.ToString();
                firstNumber = result;
                isOperatorClicked = false;
            }
        }
        private void BurgerButton_Click(object sender, RoutedEventArgs e)
        {
            ExtraButtonsColumn.Width = new GridLength(1, GridUnitType.Star);
            SetAdditionalButtonsVisibility(Visibility.Visible);
            isMenuOpen = true;

            ButtonBurger.Visibility = Visibility.Collapsed;
            ButtonBurger2.Visibility = Visibility.Visible;
        }
        private void Burger2Button_Click(object sender, RoutedEventArgs e)
        {
            ExtraButtonsColumn.Width = new GridLength(0);
            SetAdditionalButtonsVisibility(Visibility.Collapsed);
            isMenuOpen = false;

            ButtonBurger2.Visibility = Visibility.Collapsed;
            ButtonBurger.Visibility = Visibility.Visible;
        }
        private void SetAdditionalButtonsVisibility(Visibility visibility)
        {
            ButtonSqrt.Visibility = visibility;
            ButtonPi.Visibility = visibility;
            ButtonPower.Visibility = visibility;
            ButtonLog.Visibility = visibility;
            ButtonEps.Visibility = visibility;
            ButtonRedo.Visibility = visibility;
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
