using MunicipalServicesApp.Common.Exceptions;
using MunicipalServicesApp.Data.Interfaces;
using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Services.Business
{
    public class IssueService
    {
        private readonly IIssueRepository _issueRepository;

        public IssueService(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
        }

        public string SubmitIssue(string location, string category, string description, string attachedFilePath)
        {
            var validationResult = ValidateIssueData(location, category, description);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ErrorMessage);
            }

            var issue = new Issue
            {
                Location = location.Trim(),
                Category = category,
                Description = description.Trim(),
                AttachedFilePath = attachedFilePath
            };

            _issueRepository.AddIssue(issue);
            return issue.Id;
        }

        public List<Issue> GetAllIssues()
        {
            return _issueRepository.GetAllIssues();
        }

        public int GetTotalSubmittedIssues()
        {
            return _issueRepository.GetTotalIssueCount();
        }

        private ValidationResult ValidateIssueData(string location, string category, string description)
        {
            if (string.IsNullOrWhiteSpace(location))
                return new ValidationResult(false, "Location is required.");

            if (string.IsNullOrWhiteSpace(category))
                return new ValidationResult(false, "Category is required.");

            if (string.IsNullOrWhiteSpace(description))
                return new ValidationResult(false, "Description is required.");

            if (description.Trim().Length < 10)
                return new ValidationResult(false, "Description must be at least 10 characters long.");

            return new ValidationResult(true, string.Empty);
        }
    }
}