using MunicipalServicesApp.Models;
using System.Collections.Generic;

namespace MunicipalServicesApp.Data.Interfaces
{
    public interface IServiceRequestRepository
    {
        void AddRequest(ServiceRequest request);
        ServiceRequest GetRequestById(string id);
        List<ServiceRequest> GetAllRequests();
        void UpdateRequest(ServiceRequest request);
        List<ServiceRequest> GetRequestsByStatus(string status);
        List<ServiceRequest> GetRequestsByUser(string userId);
        List<ServiceRequest> GetRequestsChronological();
        List<ServiceRequest> GetHighPriorityRequests();
        ServiceRequest GetNextPriorityRequest();
        List<string> GetDepartmentCollaborations(string department);
        List<(string from, string to, double weight)> GetDepartmentNetwork();
    }
}