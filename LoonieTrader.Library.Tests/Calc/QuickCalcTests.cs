using NUnit.Framework;
using Syncfusion.Windows.Calculate;

namespace LoonieTrader.Library.Tests.Calc
{
    public class QuickCalcTests
    {
        [Test]
        public void TestFormula()
        {
            //CalcQuickBase calcQuick = new CalcQuickBase();
            CalcQuick calculator = new CalcQuick();
            calculator["A"] = (12).ToString();
            calculator["B"] = (23).ToString();
            calculator["SUM"] = "=[A] + [B]";

            calculator.SetDirty();

            var sum = calculator["SUM"];

            Assert.AreEqual("35", sum);
        }

        [Test]
        public void TestFormulaParse()
        {
            CalcQuick calculator = new CalcQuick();
            string s = calculator.ParseAndCompute("sqrt(2) * 2");
            StringAssert.StartsWith("2,828", s);
        }
    }
}