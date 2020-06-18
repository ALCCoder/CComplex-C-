using System;
using System.Collections.Generic;

namespace Task11
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexNumber num1 = new ComplexNumber(2.0, 3.0);
            ComplexNumber num2 = new ComplexNumber(1.0, 4.0);
            ComplexNumber num3 = new ComplexNumber(5.0, 3.0);
            ComplexNumber num4 = new ComplexNumber(9.0, 0.0);
            ComplexNumber num5 = new ComplexNumber(0.0, 0.0);


            ComplexNumber num6 = new ComplexNumber(2);
            ConsoleWrite("Sum", num1 + num2);
            ConsoleWrite("Minus", num2 - num1);
            ConsoleWrite("Mult", num1*num2);
            ConsoleWrite("Divide", num1/num2);
            
           
        }
        static public void func(object obj, DivideByZeroEventArgs args)
        {
            Console.WriteLine("In func");
        }
    }
}
