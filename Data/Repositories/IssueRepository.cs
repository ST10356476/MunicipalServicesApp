using MunicipalServicesApp.Data.Interfaces;
using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Data.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly List<Issue> _issues;

        public IssueRepository()
        {
            _issues = new List<Issue>();
        }

        public void AddIssue(Issue issue)
        {
            if (issue == null) throw new ArgumentNullException(nameof(issue));
            _issues.Add(issue);
        }

        public List<Issue> GetAllIssues()
        {
            return new List<Issue>(_issues);
        }

        public Issue GetIssueById(string id)
        {
            return _issues.FirstOrDefault(i => i.Id == id);
        }

        public int GetTotalIssueCount()
        {
            return _issues.Count;
        }
    }
}