using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2
{
    public class RedoCommand : ICommandPattern
    {
        private Stack<CalculatorState> _stateHistory;
        private Stack<CalculatorState> _redoStack;
        private MainWindow _calculator;

        public RedoCommand(MainWindow calculator, Stack<CalculatorState> stateHistory, Stack<CalculatorState> redoStack)
        {
            _calculator = calculator;
            _stateHistory = stateHistory;
            _redoStack = redoStack;
        }

        public void Execute()
        {
            if (_redoStack.Count > 0)
            {
                var currentState = new CalculatorState
                {
                    Text = _calculator.TextBoxInput.Text,
                    FirstNumber = _calculator.firstNumber,
                    SecondNumber = _calculator.secondNumber,
                    Operator = _calculator.currentOperator,
                    IsOperatorClicked = _calculator.isOperatorClicked
                };

                _stateHistory.Push(currentState);
                var nextState = _redoStack.Pop();
                _calculator.ApplyState(nextState);
            }
        }
    }
}
