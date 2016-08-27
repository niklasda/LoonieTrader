using System;
using System.IO;
using System.Reflection;
using System.Resources;
using NUnit.Framework;
using Syncfusion.PMML;

namespace LoonieTrader.RestLibrary.Tests.Calc
{
    public class PredictiveTests
    {

        [Test]
        public void TestFormulaParse()
        {
            var anonymousType = new
            {
                Employment = "SelfEmp",
                Education = "Master",
                Marital = "Married",
                Occupation = "Professional",
                Gender = "Male",
                Age = 41,
                Income = 30123.5,
                Deductions = 1022.5,
                Hours = 40
            };

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "LoonieTrader.RestLibrary.Tests.Calc.sample.pmml";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                PMMLEvaluator evaluator = new PMMLEvaluatorFactory().GetPMMLEvaluatorInstance(stream);
                PredictedResult predictedResult = evaluator.GetResult(anonymousType, null);
                Console.WriteLine(predictedResult.PredictedValue);
            }
        }


        [Test]
        public void TestFormula2Parse()
        {
            var anonymousType = new
            {
                Employment = "SelfEmp",
                Education = "Master",
                Marital = "Married",
                Occupation = "Professional",
                Gender = "Male",
                Age = 41,
                Income = 30123.5,
                Deductions = 1022.5,
                Hours = 40
            };

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "LoonieTrader.RestLibrary.Tests.Calc.sample.pmml";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                PMMLDocument pmmlDocument = new PMMLDocument(stream);
                RegressionModelEvaluator regressionModel = new RegressionModelEvaluator(pmmlDocument);
                PredictedResult predictedResult = regressionModel.GetResult(anonymousType, null);
                regressionModel.Dispose();
                Console.WriteLine(predictedResult.PredictedValue);


            }
        }
    }
}