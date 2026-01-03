using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationLibrary.Services;

namespace JobApplicationLibrary
{
    public class ApplicationEvaluator
    {
        private const int MinAge = 18;
        private const int autoAcceptedYearsOfExperience = 15;
        public List<string> techStackList = new() { "C#", "RabbitMQ", "Mcroservice", "Visual Studio" };
        private IdentityValidator identityValidator;
        public ApplicationEvaluator()
        {
            identityValidator= new IdentityValidator();
        }
        public ApplicationResult EvaluateApplication(Models.JobApplication form)
        {
            if (form.Application.Age < MinAge)
            {
                return ApplicationResult.AutoRejected;
            }
            var validIdentity = identityValidator.IsValid(form.Application.IdentityNumber);

            if (!validIdentity)
                return ApplicationResult.TransferredToHR;

            var sr = GetTechStackMatchCount(form.TechStackList);

            if (sr <= 25)
                return ApplicationResult.AutoRejected;

            if (sr >= 75 && form.YearsOfExperience > autoAcceptedYearsOfExperience)
                return ApplicationResult.AutoAccepted;

            return ApplicationResult.AutoAccepted;
        }
        private double GetTechStackMatchCount(List<string> applicantTechStackList)
        {
            var matchedCount = applicantTechStackList.Intersect(techStackList, StringComparer.OrdinalIgnoreCase).Count();
            return (double)(matchedCount / techStackList.Count) * 100;
        }
    }
    public enum ApplicationResult
    {
        AutoRejected,
        TransferredToHR,
        TransferredToLead,
        TransferredToCTO,
        AutoAccepted
    }
}
