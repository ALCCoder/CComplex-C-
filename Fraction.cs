using System.Dynamic;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task11
{
    class Fraction : IComparable, IComparable<Fraction>, IEquatable<Fraction>
    {
        public static double EPS = 0.000001;
        private BigInteger numerator { get; set; }
        private BigInteger denominator { get; set; }

        public Fraction()
        {
            numerator = 0;
            denominator = 1;
        }

        public Fraction(BigInteger a)
        {
            numerator = a;
            denominator = 1;
        }

        public Fraction(BigInteger num, BigInteger denom)
        {
            if (denom.IsZero)
            {
                throw new DivideByZeroException("Denominator is 0");
            }
            numerator = BigInteger.Abs(num);
            denominator = BigInteger.Abs(denom);
            if (num * denom < 0)
            {
                numerator *= -1;
            }
        }

        public static Fraction Add(Fraction frac1, Fraction frac2)
        {
            if (frac1 == null || frac2 == null)
            {
                return null;
            }
            else
            {
                Fraction newFraction = new Fraction
                {
                    numerator = frac1.numerator * frac2.denominator + frac2.numerator * frac1.denominator,
                    denominator = frac1.denominator * frac2.denominator
                };
                newFraction.Reduce();
                return newFraction;
            }
        }

        public static Fraction operator +(Fraction frac1, Fraction frac2)
        {
            return Add(frac1, frac2);
        }

        public static Fraction Sub(Fraction frac1, Fraction frac2)
        {
            if (frac1 == null || frac2 == null)
            {
                return null;
            }
            else
            {
                Fraction newFraction = new Fraction
                {
                    numerator = frac1.numerator * frac2.denominator - frac2.numerator * frac1.denominator,
                    denominator = frac1.denominator * frac2.denominator
                };
                newFraction.Reduce();
                return newFraction;
            }
        }

        public static Fraction operator -(Fraction frac1, Fraction frac2)
        {
            return Sub(frac1, frac2);
        }


        public static Fraction Mul(Fraction frac1, Fraction frac2)
        {
            Fraction newFraction = new Fraction
            {
                numerator = frac1.numerator * frac2.numerator,
                denominator = frac1.denominator * frac2.denominator
            };

            newFraction.Reduce();
            return newFraction;
        }

        public static Fraction operator *(Fraction frac1, Fraction frac2)
        {
            return Mul(frac1, frac2);
        }
       
        

        public static Fraction Div(Fraction frac1, Fraction frac2)
        {

            Fraction newFraction = new Fraction();
            if (newFraction.denominator.IsZero)
            {
                throw new DivideByZeroException("The denominator cannot be zero");
            }

            newFraction.denominator = BigInteger.Multiply(frac1.denominator, frac2.numerator);
            if (newFraction.denominator < 0)
                newFraction.denominator *= -1;
            if (frac1.denominator * frac2.numerator < 0)
                newFraction.numerator = -1 * frac1.numerator * frac2.denominator;
            else
                newFraction.numerator = frac1.numerator * frac2.denominator;


            newFraction.Reduce();
            return newFraction;
        }

        public static Fraction operator /(Fraction frac1, Fraction frac2)
        {
            return Div(frac1, frac2);
        }

        //private static BigInteger GetGreatestCommonDivisor(BigInteger a, BigInteger b)
        //{
        //    while (b != 0)
        //    {
        //        BigInteger temp = b;
        //        b = a % b;
        //        a = temp;
        //    }

        //    if (a < 0)
        //        a *= -1;

        //    return a;
        //}
        public BigInteger Sqrt(BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        private Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }

        public BigInteger GetSqrtNumerator()
        {
            return Sqrt(numerator);
        }

        public BigInteger GetSqrtDenominator()
        {
            return Sqrt(denominator);
        }

        public Fraction GetSqrtFraction()
        {
            return new Fraction(GetSqrtNumerator(), GetSqrtDenominator());
        }
        public Fraction Reduce()
        {
            Fraction result = this;
            //BigInteger greatestCommonDivisor = GetGreatestCommonDivisor(numerator, denominator);
            try
            {
                result.numerator /= BigInteger.GreatestCommonDivisor(numerator, denominator);
                result.denominator /= BigInteger.GreatestCommonDivisor(numerator, denominator);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Unable to calculate the greatest common divisor:");
                Console.WriteLine("   {0} is an invalid value for {1}",
                                  e.ActualValue, e.ParamName);
            }

            return result;
        }

        public override string ToString()
        {
            return "[" + numerator + "/" + denominator + "]";
        }

        public int CompareTo(Fraction other)
        {
            if (other == null)
                throw new ArgumentNullException("Null fraction");
            if (Equals(other))
            {
                return 0;
            }
            Fraction a = Reduce();
            Fraction b = other.Reduce();
            if (a.numerator * b.denominator > b.numerator * a.denominator)
            {
                return 1;
            }
            return -1;
        }

        public int CompareTo(object frac)
        {
            Fraction f = frac as Fraction;
            if (f == null) throw new ArgumentNullException("Can't be compare");
            if (Equals(f))
            {
                return 0;
            }
            Fraction a = Reduce();
            Fraction b = f.Reduce();
            if (a.numerator * b.denominator > b.numerator * a.denominator)
            {
                return 1;
            }
            return -1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Fraction)
                return Equals(obj as Fraction);

            return false;
        }

        public bool Equals(Fraction fraction)
        {
            if (fraction == null)
            {
                throw new ArgumentException("Can't compare null objects!");
            }
            Fraction a = Reduce();
            Fraction b = fraction.Reduce();
            return (a.numerator - b.numerator).IsZero && (a.denominator - b.denominator).IsZero;
        }


        public static bool operator >(Fraction a, Fraction b)
        {
            return a.CompareTo(b) > 0;
        }
        public static bool operator >(Fraction a, int b)
        {
            return a > new Fraction(b);
        }
        public static bool operator >(int a, Fraction b)
        {
            return new Fraction(a) > b;
        }
        public static bool operator <(Fraction a, Fraction b)
        {
            return a.CompareTo(b) < 0;
        }
        public static bool operator <(Fraction a, int b)
        {
            return a < new Fraction(b);
        }
        public static bool operator <(int a, Fraction b)
        {
            return new Fraction(a) < b;
        }
        public static bool operator >=(Fraction a, Fraction b)
        {
            return a.CompareTo(b) >= 0;
        }
        public static bool operator >=(Fraction a, int b)
        {
            return a >= new Fraction(b);
        }
        public static bool operator >=(int a, Fraction b)
        {
            return new Fraction(a) >= b;
        }
        public static bool operator <=(Fraction a, Fraction b)
        {
            return a.CompareTo(b) <= 0;
        }
        public static bool operator <=(Fraction a, int b)
        {
            return a <= new Fraction(b);
        }
        public static bool operator <=(int a, Fraction b)
        {
            return new Fraction(a) <= b;
        }
    }
}
