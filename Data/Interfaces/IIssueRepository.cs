using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Data.Interfaces
{
    public interface IIssueRepository
    {
        void AddIssue(Issue issue);
        List<Issue> GetAllIssues();
        Issue GetIssueById(string id);
        int GetTotalIssueCount();
    }
}