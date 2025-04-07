using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2
{
    public class CalculatorState
    {
        public string Text { get; set; }
        public double FirstNumber { get; set; }
        public double SecondNumber { get; set; }
        public string Operator { get; set; }
        public bool IsOperatorClicked { get; set; }
    }
}
