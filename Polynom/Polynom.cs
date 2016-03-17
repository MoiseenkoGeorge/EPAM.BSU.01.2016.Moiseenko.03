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
        public Polynom(params double[] coefficient)
        {
            coefficients = new double[coefficient.Length];
            coefficients = coefficient;
        }
    }
}
