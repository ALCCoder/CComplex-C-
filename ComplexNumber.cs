using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Task11
{
     class ComplexNumber : IEquatable<ComplexNumber>, IComparable<ComplexNumber>, IComparable
    {
        

        private Fraction Re { get; set; }
        private Fraction Im { get; set; }
        public ComplexNumber(Fraction re, Fraction im) : this()
        {
            Re = re;
            Im = im;
        }
        public ComplexNumber()
        {
            Re = new Fraction(0);
            Im = new Fraction(0);
        }

        public event EventHandler<DivideByZeroEventArgs> DivideByZero;

        public static Fraction Module(ComplexNumber com)
        {
            return new Fraction((com.Re * com.Re) + (com.Im * com.Im)).GetSqrtFraction();
        }

        static public ComplexNumber operator +(ComplexNumber leftNum, ComplexNumber rightNum)
        {
            return Sum(leftNum, rightNum);
        }

        static public ComplexNumber Sum(ComplexNumber leftNum, ComplexNumber rightNum)
        {
            return new ComplexNumber(leftNum.Re + rightNum.Re, leftNum.Im + rightNum.Im);
        }

        static public ComplexNumber operator -(ComplexNumber leftNum, ComplexNumber rightNum)
        {
            return Sub(leftNum, rightNum);
        }

        static public ComplexNumber Sub(ComplexNumber leftNum, ComplexNumber rightNum)
        {
            return new ComplexNumber(leftNum.Re - rightNum.Re, leftNum.Im - rightNum.Im);
        }

        //(x1 + iy1)(x2 + iy2) = x1x2 - y1y2 + i(x2y1 + y2x1)
        static public ComplexNumber operator *(ComplexNumber leftNum, ComplexNumber rightNum)
        {
            return Mul(leftNum, rightNum);
        }

        static public ComplexNumber Mul(ComplexNumber leftNum, ComplexNumber rightNum)
        {
            return new ComplexNumber(leftNum.Re * rightNum.Re - leftNum.Im * rightNum.Im,
                                    leftNum.Re * rightNum.Im + leftNum.Im * rightNum.Re);
        }

        static public ComplexNumber operator *(ComplexNumber complNum, double number)
        {
            return NumberMul(complNum, number);
        }

        static public ComplexNumber operator *(double number, ComplexNumber complNum)
        {
            return NumberMul(complNum, number);
        }

        static public ComplexNumber NumberMul(ComplexNumber complNum, double number)
        {
            return new ComplexNumber((dynamic)complNum.Re * number,
                                   (dynamic)complNum.Im * number);
        }

        //(x1 + iy1) / (x2 + iy2) = (x1 + iy1) * (x2 - iy2) / (x2^2 + y2^2)
        static public ComplexNumber operator /(ComplexNumber leftNum, ComplexNumber rightNum)
        {
            return Div(leftNum, rightNum);
        }

        public static ComplexNumber Div(ComplexNumber com1, ComplexNumber com2)
        {
            if (com2.Re.Equals(0) && com2.Im.Equals(0))
                throw new DivideByZeroException("В знаменателе не может быть нуля");
            return new ComplexNumber()
            {
                Re = (com1.Re * com2.Re + com1.Im * com2.Im) / (com2.Re * com2.Re + com2.Im * com2.Im),
                Im = (com1.Im * com2.Re + com2.Im * com1.Re) / (com2.Re * com2.Re + com2.Im * com2.Im)
            };
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            if(obj is ComplexNumber)
            {
                return Equals(obj as ComplexNumber);
            }
            return false;
        }

        public bool Equals(ComplexNumber cnum)
        {
            if(cnum is null)
            {
                return false;
            }
            return Re.Equals(cnum.Re) && Im.Equals(cnum.Im);
        }
      


        public override string ToString()
        {
            return $"[{Re} + {Im}i]";
        }

        
        

        public int CompareTo(object obj)
        {

            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            else if (obj is ComplexNumber)
            {
                return CompareTo(obj as ComplexNumber);
            }
            else
            {
                throw new ArgumentException("Invalid type", nameof(obj));
            }
          

         
        }
        public int CompareTo(ComplexNumber other)
        {
            if (Equals(other))
                return 0;
            if (Module(this) > Module(other))
                return 1;
            return -1;
        }

        //CompareTo за тем чтобы если сравнивать объект с чем угодно аналогия с equals
        public static bool operator >(ComplexNumber a, ComplexNumber b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator <(ComplexNumber a, ComplexNumber b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator <=(ComplexNumber a, ComplexNumber b)
        {
            return a.CompareTo(b) <= 0;
        }

        public static bool operator >=(ComplexNumber a, ComplexNumber b)
        {
            return a.CompareTo(b) >= 0;
        }
    }
}
