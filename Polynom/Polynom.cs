using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynom
{
    public class Polynom
    {
        private double[] coefficients;
        public int Power { get; }
        public Polynom(params double[] coefficient)
        {
            coefficients = new double[coefficient.Length];
            Power = coefficient.Length - 1;
            for (int i = 0; i < Power + 1; i++)
                coefficients[i] = coefficient[i];
        }

        public double this[int index]
        {
            get
            {
                if(index > Power || index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index),"Out of Range");
                return coefficients[index];
            }
        }

        public static Polynom operator +(Polynom lhs, Polynom rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException("Require non-null argument");
            return Add(lhs, rhs);
        }
        public static Polynom Add(Polynom lhs,Polynom rhs)
        {
            if (rhs == null || lhs == null)
                throw new ArgumentNullException("Require non-null argument");
            if (rhs.Power > lhs.Power)
            {
                Polynom temp = rhs;
                rhs = lhs;
                lhs = temp;
            }
            return new Polynom(lhs.coefficients.Select((x, index) => x + (index > rhs.coefficients.Length - 1 ? 0 : rhs.coefficients[index])).ToArray());
        }

        public static Polynom operator *(Polynom lhs, Polynom rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException("Require non-null argument");
            return Multiply(lhs, rhs);
        }
        public static Polynom Multiply(Polynom lhs,Polynom rhs)
        {
            double[] parametres = Allocate(lhs.Power + rhs.Power + 1);
            for (int i = 0; i <= lhs.Power; i++)
                for (int j = 0; j <= rhs.Power; j++)
                    parametres[i + j] += lhs[i] * rhs[j];
            return new Polynom(parametres);
        }

        public static Polynom operator -(Polynom lhs, Polynom rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException("Require non-null argument");
            return Substruct(lhs, rhs);
        }
        public static Polynom Substruct(Polynom lhs, Polynom rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException("Require non-null argument");
            if (rhs.Power > lhs.Power)
            {
                Polynom temp = PositiveToNegative(rhs);
                rhs = lhs;
                lhs = temp;
                return Add(rhs, lhs);
            }

            return new Polynom(lhs.coefficients.Select((x, index) => x - (index > rhs.coefficients.Length - 1 ? 0 : rhs.coefficients[index])).ToArray());
        }

        public static bool operator ==(Polynom lhs, Polynom rhs)
        {
            if (System.Object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }

            return lhs.Equals(rhs);
        }
        public static bool operator !=(Polynom lhs, Polynom rhs)
        {
            return !(lhs == rhs);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Polynom polynom = obj as Polynom;
            if (coefficients.Length != polynom.coefficients.Length)
                return false;
            for (int i = 0; i < coefficients.Length; i++)
                if (coefficients[i] != polynom.coefficients[i])
                    return false;
            return true;
        }
        public override int GetHashCode()
        {
            int hashCode = (int)coefficients[0];
            for (int i = 1; i < coefficients.Length; i++)
                hashCode ^= (int)coefficients[i];
            return hashCode;
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < coefficients.Length; i++)
                result += (coefficients[i] > 0 ? "+" : "") + (coefficients[i] == 0 ? "" : (coefficients[i] + "*x^" + i));
            return result;
        }
        private static Polynom PositiveToNegative(Polynom polynom)
        {
            double[] array = new double[polynom.Power+1];
            for (int i = 0; i < array.Length; i++)
                array[i] = polynom[i] * -1;
            return new Polynom(array);
        }
        private static double[] Allocate(int length)
        {
            double[] array = new double[length];
            for (int i = 0; i < length; i++)
                array[i] = 0;
            return array;
        }
    }
}
