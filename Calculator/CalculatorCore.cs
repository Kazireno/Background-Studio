using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class CalculatorCore
    {
        double a = 0d;
        double b = 0d;

        public void setA(double A) { a = A; }
        public void setB(double B) { b = B; }
        public Operator @operator;

        public enum Number : int
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
        }

        public enum Operator
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }

        public double Calculating()
        {
            switch (@operator)
            {
                case Operator.Addition:
                    return a + b;
                case Operator.Subtraction:
                    return a - b;
                case Operator.Division:
                    return a / b;
                case Operator.Multiplication:
                    return a * b;
            }
            return 0d;
        }

    }
}
