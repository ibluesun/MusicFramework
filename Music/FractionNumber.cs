
namespace LostParticles.MusicFramework
{
    public class Fraction
    {
        private int num, den;
        public Fraction(int num, int den)
        {
            this.num = num;
            this.den = den;
        }

        public int Numerator
        {
            get
            {
                return num;
            }
        }

        public int Denominator
        {
            get
            {
                return den;
            }
        }

        // overload operator +
        public static Fraction operator +(Fraction a, Fraction b)
        {
            int num = a.num * b.den + b.num * a.den;
            int den = a.den * b.den;


            for (int fac = 24; fac>=2; fac--)
            {
                if ((num % fac == 0) && (den % fac == 0))
                {
                    num = num / fac;
                    den = den / fac;
                    break;
                }
            }

            return new Fraction(num, den);
        }


        // overload operator *
        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.num * b.num, a.den * b.den);
        }

        public Fraction Clone()
        {
            return new Fraction(num, den);
        }

        // define operator double
        public static implicit operator double(Fraction f)
        {
            return (double)f.num / f.den;
        }

        public double Value
        {
            get
            {
                return (double)num / den;
            }
        }
    }

}
