using System.Formats.Tar;
using System.Threading.Channels;
using JobApplicationLibrary.Models;


namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluatorUnitTest
    {

        //UnitOfWork_Condition_ExpectedResult
        //Condition_ExpectedResult

        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejected()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication
            {
                Application = new Application
                {
                    Age = 17
                },
            };

            //Action
            var appResult=evaluator.EvaluateApplication(form);
            // Assert
            Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoRejected));
            
        }
        [Test]
        public void Application_WithNoTechStack_TransferredToAutoRejected()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication
            {
                Application = new Application
                {
                    IdentityNumber = "11",
                    Age = 25
                },
                TechStackList= new List<string>
                {
                    "C#" 
                }
            };

            //Action
            var appResult = evaluator.EvaluateApplication(form);
            // Assert
            Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoRejected));

        }
        [Test]
        public void Application_WithTechStackOver75P_TransferredToAutoAccepted()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication
            {
                Application = new Application
                {
                    IdentityNumber="11",
                    Age = 25
                },
                TechStackList = new List<string>
                {
                    "C#", "RabbitMQ", "Mcroservice", "Visual Studio"
                },
                YearsOfExperience= 19
            };

            //Action
            var appResult = evaluator.EvaluateApplication(form);
            // Assert
            Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoAccepted));

        }

    }
}