using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;
using Moq;
using FluentAssertions;
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
            var evaluator = new ApplicationEvaluator(null);
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
           // Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoRejected));
            appResult.Should().Be(ApplicationResult.AutoRejected);

        }
        [Test]
        public void Application_WithNoTechStack_TransferredToAutoRejected()
        {
            //Arrange
            var mockvalidator = new Mock<IIdentityValidator>();
            mockvalidator.DefaultValue=DefaultValue.Mock;
            mockvalidator.Setup(x => x.countryDataProvider.CountryData.Country).Returns("Turkey");
            mockvalidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var evaluator = new ApplicationEvaluator(mockvalidator.Object);
            var form = new JobApplication
            {
                
                Application = new Application
                {
                    
                    IdentityNumber = "11",
                    Age = 25,
                    
                },
                TechStackList= new List<string>
                {
                    "C#" 
                }
            };

            //Action
            var appResult = evaluator.EvaluateApplication(form);
            // Assert
            //Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoRejected));
            appResult.Should().Be(ApplicationResult.AutoRejected);

        }
        [Test]
        public void Application_WithTechStackOver75P_TransferredToAutoAccepted()
        {
            //Arrange
            var mockvalidator = new Mock<IIdentityValidator>();
            mockvalidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            mockvalidator.DefaultValue = DefaultValue.Mock;
            mockvalidator.Setup(x => x.countryDataProvider.CountryData.Country).Returns("Turkey");
            var evaluator = new ApplicationEvaluator(mockvalidator.Object);
            var form = new JobApplication
            {
              
                Application = new Application
                {
                    
                    IdentityNumber ="11",
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
           // Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoAccepted));
            appResult.Should().Be(ApplicationResult.AutoAccepted);

        }
        [Test]
        public void Application_WithInvalidIdentityNumber_TransferredToHR()
        {
            //Arrange
            var mockvalidator = new Mock<IIdentityValidator>(MockBehavior.Loose);
            mockvalidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
            mockvalidator.DefaultValue = DefaultValue.Mock;
            mockvalidator.Setup(x => x.countryDataProvider.CountryData.Country).Returns("Turkey");
            var evaluator = new ApplicationEvaluator(mockvalidator.Object);
            var form = new JobApplication
            {
              
                Application = new Application
                {
                   
                    IdentityNumber = "11",
                    Age = 25
                },
                TechStackList = new List<string>
                {
                    "C#", "RabbitMQ", "Mcroservice", "Visual Studio"
                },
                YearsOfExperience = 19
            };

            //Action
            var appResult = evaluator.EvaluateApplication(form);
            // Assert
            //Assert.That(appResult, Is.EqualTo(ApplicationResult.TransferredToHR));
            appResult.Should().Be(ApplicationResult.TransferredToHR);

        }
        [Test]
        public void Application_WithOfficeLocation_TransferredToCTO()
        {
            //Arrange
            var mockvalidator = new Mock<IIdentityValidator>(MockBehavior.Loose);
            mockvalidator.Setup(i => i.countryDataProvider.CountryData.Country).Returns("Germany");
            
          
            var evaluator = new ApplicationEvaluator(mockvalidator.Object);
            var form = new JobApplication
            {
                
                Application = new Application
                {

                    IdentityNumber = "11",
                    Age = 25
                },
                TechStackList = new List<string>
                {
                    "C#", "RabbitMQ", "Mcroservice", "Visual Studio"
                },
                YearsOfExperience = 19
            };

            //Action
            var appResult = evaluator.EvaluateApplication(form);
            // Assert
            //Assert.That(appResult, Is.EqualTo(ApplicationResult.TransferredToCTO));
            appResult.Should().Be(ApplicationResult.TransferredToCTO);
        }
        [Test]
        public void Application_WithOver50_ValidationModeToDetailed()
        {
            // Arrange
      
            var mockvalidator = new Mock<IIdentityValidator>();
            mockvalidator.SetupAllProperties();
            mockvalidator.Setup(i => i.countryDataProvider.CountryData.Country).Returns("TURKEY");
           // mockvalidator.SetupProperty(v => v.ValidationMode);//method içinde yapılan işlemi izlemek için
     
            var evaluator = new ApplicationEvaluator(mockvalidator.Object);
            var form = new JobApplication
            {

                Application = new Application
                {

                    IdentityNumber = "11",
                    Age = 51
                },
                
            };

            //Action
            var appResult = evaluator.EvaluateApplication(form);
            // Assert
            //Assert.That(mockvalidator.Object.ValidationMode, Is.EqualTo(ValidationMode.Detailed));

            mockvalidator.Object.ValidationMode.Should().Be(ValidationMode.Detailed);
        }
        [Test]
        public void Application_WithNullApplication_ThrowsArgumentException()
        {
            // Arrange

            var mockvalidator = new Mock<IIdentityValidator>();
            mockvalidator.SetupAllProperties();
            mockvalidator.Setup(i => i.countryDataProvider.CountryData.Country).Returns("TURKEY");
            // mockvalidator.SetupProperty(v => v.ValidationMode);//method içinde yapılan işlemi izlemek için

            var evaluator = new ApplicationEvaluator(mockvalidator.Object);
            var form = new JobApplication
            {

            };

            //Action
            Action appResult = ()=>evaluator.EvaluateApplication(form);
            // Assert
            //Assert.That(mockvalidator.Object.ValidationMode, Is.EqualTo(ValidationMode.Detailed));

            appResult.Should().Throw<ArgumentNullException>();
        }
        [Test]
        public void Application_WithDefaultValue_IsValidCalled()
        {

            //Arrange
            var mockvalidator = new Mock<IIdentityValidator>();
            mockvalidator.DefaultValue = DefaultValue.Mock;
            mockvalidator.Setup(i => i.countryDataProvider.CountryData.Country).Returns("Turkey");
            var evaluator = new ApplicationEvaluator(mockvalidator.Object);
            var form = new JobApplication
            {

                Application = new Application
                {
                    Age = 25
                },
            };

            //Action
            var appResult = evaluator.EvaluateApplication(form);
            // Assert

           mockvalidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Once);
        }
    }
}