using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace Polynom.Tests
{
    [TestFixture]
    public class PolynomTest
    {
        [Test]
        [TestCase(1,2,3,Result = "+2*x^0+4*x^1+6*x^2")]
        [TestCase(1, 2, 3, 4, Result = "+2*x^0+4*x^1+6*x^2+4*x^3")]
        public string Add_Test(params double[] coff)
        {
            Polynom pol1 = new Polynom(1, 2, 3);
            Polynom pol2 = new Polynom(coff);
            return (pol1 + pol2).ToString();
        }

        [Test]
        [TestCase(1, 2, 3, Result = "")]
        [TestCase(1, 2, 3, 4, Result = "-4*x^3")]
        public string Substruct_Test(params double[] coff)
        {
            Polynom pol1 = new Polynom(1, 2, 3);
            Polynom pol2 = new Polynom(coff);
            return (pol1 - pol2).ToString();
        }

        [Test]
        [TestCase(1, 2, 3, Result = "+1*x^0+4*x^1+10*x^2+12*x^3+9*x^4")]
        [TestCase(1, 2, 3, 4, Result = "+1*x^0+4*x^1+10*x^2+16*x^3+17*x^4+12*x^5")]
        public string Mul_Test(params double[] coff)
        {
            Polynom pol1 = new Polynom(1, 2, 3);
            Polynom pol2 = new Polynom(coff);
            return (pol1 * pol2).ToString();
        }

        [Test]
        [TestCase(1,2,3,Result = true)]
        public bool Equal_Test(params double[] coff)
        {
            Polynom pol1 = new Polynom(coff);
            Polynom pol2 = new Polynom(coff);
            return pol1 == pol2;
        }
    }
}
