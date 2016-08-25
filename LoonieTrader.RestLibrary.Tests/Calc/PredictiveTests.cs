using System;
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
                Sepal_Length = 4.3,
                Sepal_Width = 3.2,
                Petal_Length = 0.4,
                Petal_Width = 1.1
            };

            string pmmlFilePath = "Sample.pmml";
            PMMLEvaluator PMMLEvaluator = new PMMLEvaluatorFactory().GetPMMLEvaluatorInstance(pmmlFilePath);

            //Gets the predicted result
            PredictedResult predictedResult = PMMLEvaluator.GetResult(anonymousType, null);
            Console.WriteLine(predictedResult.PredictedValue);
        }


        [Test]
        public void TestFormula2Parse()
        {
            var anonymousType = new
            {
                total_bill = 23.68,
                sex = "Male",
                smoker = "No",
                day = "Sun",
                time = "Dinner",
                size = 2
            };

            string pmmlFilePath = "Sample.pmml";

            //Create instance for PMML Document
            PMMLDocument pmmlDocument = new PMMLDocument(pmmlFilePath);

            //Create instance for Mining model
            RegressionModelEvaluator regressionModel = new RegressionModelEvaluator(pmmlDocument);

            //Gets the predicted result
            PredictedResult predictedResult = regressionModel.GetResult(anonymousType, null);

            regressionModel.Dispose();
            //Displays the predicted result

            Console.WriteLine(predictedResult.PredictedValue);
        }
    }
}