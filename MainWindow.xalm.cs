using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OOP2
{
    public interface ICommandPattern
    {
        void Execute();
    }
    public partial class MainWindow : Window
    {
        public double firstNumber = 0;
        public double secondNumber = 0;
        public bool isOperatorClicked = false;
        public string currentOperator = "";
        private bool isMenuOpen = false;

        private Stack<CalculatorState> stateHistory = new Stack<CalculatorState>();
        private Stack<CalculatorState> redoStack = new Stack<CalculatorState>();

        private ICommandPattern _undoCommand;
        private ICommandPattern _redoCommand;
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
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            SaveState();
            if (!string.IsNullOrEmpty(TextBoxInput.Text))
            {
                TextBoxInput.Text = TextBoxInput.Text[..^1];
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
            redoStack.Clear();
        }
        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            _undoCommand.Execute();
        }
        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            _redoCommand.Execute();
        }
        public void ApplyState(CalculatorState state)
        {
            TextBoxInput.Text = state.Text;
            firstNumber = state.FirstNumber;
            secondNumber = state.SecondNumber;
            currentOperator = state.Operator;
            isOperatorClicked = state.IsOperatorClicked;
        }
        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBoxInput.Text, out secondNumber))
            {
                SaveState();
                double result = 0;

                switch (currentOperator)
                {
                    case "+": result = firstNumber + secondNumber; break;
                    case "-": result = firstNumber - secondNumber; break;
                    case "*": result = firstNumber * secondNumber; break;
                    case "/":
                        if (secondNumber == 0)
                        {
                            MessageBox.Show("На нуль ділити не можна!");
                            return;
                        }
                        result = firstNumber / secondNumber;
                        break;
                    case "nʸ": result = Math.Pow(firstNumber, secondNumber); break;
                    case "log":
                        if (firstNumber <= 0)
                        {
                            MessageBox.Show("Логарифм можливий тільки для чисел більше 0!");
                            return;
                        }
                        result = Math.Log10(firstNumber); break;
                    case "√x":
                        if (firstNumber < 0)
                        {
                            MessageBox.Show("Не можна взяти корінь з від’ємного числа!");
                            return;
                        }
                        result = Math.Sqrt(firstNumber); break;
                    case "e": result = firstNumber * Math.E; break;
                    case "π": result = firstNumber * Math.PI; break;
                }
                TextBoxInput.Text = result.ToString();
                firstNumber = result;
                isOperatorClicked = false;
            }
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
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
            else if (e.Key == Key.Add) PlusButton_Click(sender, e);
            else if (e.Key == Key.Subtract) MinusButton_Click(sender, e);
            else if (e.Key == Key.Multiply) MultiplyButton_Click(sender, e);
            else if (e.Key == Key.Divide) DivisionButton_Click(sender, e);
            else if (e.Key == Key.Decimal)
            {
                SaveState();
                if (!TextBoxInput.Text.Contains(",")) TextBoxInput.Text += ",";
            }
            else if (e.Key == Key.P) PiButton_Click(sender, e);
            else if (e.Key == Key.S) SqrtButton_Click(sender, e);
            else if (e.Key == Key.E) EpsButton_Click(sender, e);
            else if (e.Key == Key.L) LogButton_Click(sender, e);
            else if (e.Key == Key.D) PowerButton_Click(sender, e);
            else if (e.Key == Key.Enter) EqualButton_Click(sender, e);
            else if (e.Key == Key.Back) BackspaceButton_Click(sender, e);
            else if (e.Key == Key.C) ClearButton_Click(sender, e);
            else if (e.Key == Key.Escape) UndoButton_Click(sender, e);
            else if (e.Key == Key.Y && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                RedoButton_Click(sender, e);
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
        }
        public MainWindow()
        {
            InitializeComponent();
            _undoCommand = new UndoCommand(this, stateHistory, redoStack);
            _redoCommand = new RedoCommand(this, stateHistory, redoStack);
        }
    }
}
