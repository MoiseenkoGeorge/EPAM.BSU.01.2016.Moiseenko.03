using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynom
{
    public class Polynom
    {
        public Polynom(params double[] coefficient)
        {
            coefficients = coefficient;
        }

        public static Polynom operator +(Polynom lhs, Polynom rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException("Require non-null argument");
            return lhs.Add(rhs);
        }
        public Polynom Add(Polynom rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException("Require non-null argument");
            if (coefficients.Length > rhs.coefficients.Length)
                return op_Add(this, rhs);
            else
                return op_Add(rhs, this);
        }

        public static Polynom operator *(Polynom lhs, Polynom rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException("Require non-null argument");
            return lhs.Multiply(rhs);
        }
        public Polynom Multiply(Polynom rhs)
        {
            double[] parametres = Allocate(coefficients.Length + rhs.coefficients.Length - 1);
            for (int i = 0; i < coefficients.Length; i++)
                for (int j = 0; j < rhs.coefficients.Length; j++)
                    parametres[i + j] += coefficients[i] * rhs.coefficients[j];
            return new Polynom(parametres);
        }

        public static Polynom operator -(Polynom lhs, Polynom rhs)
        {
            if (lhs == null || rhs == null)
                throw new ArgumentNullException("Require non-null argument");
            return lhs.Substruct(rhs);
        }
        public Polynom Substruct(Polynom rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException("Require non-null argument");
            if (coefficients.Length > rhs.coefficients.Length)
                return op_Substruction(this, rhs);
            else
                return op_Add(new Polynom(PositiveToNegative(rhs.coefficients)), this);
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

        private double[] coefficients;
        private Polynom op_Add(Polynom lhs, Polynom rhs)
        {
            return new Polynom(lhs.coefficients.Select((x, index) => x + (index > rhs.coefficients.Length - 1 ? 0 : rhs.coefficients[index])).ToArray());
        }
        private Polynom op_Substruction(Polynom lhs, Polynom rhs)
        {
            return new Polynom(lhs.coefficients.Select((x, index) => x - (index > rhs.coefficients.Length - 1 ? 0 : rhs.coefficients[index])).ToArray());
        }
        private double[] PositiveToNegative(double[] array)
        {
            double[] resultArray = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
                resultArray[i] = array[i] * -1;
            return resultArray;
        }
        private double[] Allocate(int length)
        {
            double[] array = new double[length];
            for (int i = 0; i < length; i++)
                array[i] = 0;
            return array;
        }
    }
}
