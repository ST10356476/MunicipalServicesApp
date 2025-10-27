// ServiceRequestService.cs
using MunicipalServicesApp.Data.Interfaces;
using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Services.Business
{
    public class ServiceRequestService
    {
        private readonly IServiceRequestRepository _repository;

        public ServiceRequestService(IServiceRequestRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public string CreateServiceRequest(string title, string description, string category,
                                         string location, int priority, string userId = "default")
        {
            var request = new ServiceRequest
            {
                Title = title,
                Description = description,
                Category = category,
                Location = location,
                Priority = priority,
                UserId = userId,
                AssignedDepartment = AssignDepartment(category)
            };

            _repository.AddRequest(request);
            return request.Id;
        }

        public ServiceRequest GetRequest(string requestId)
        {
            return _repository.GetRequestById(requestId);
        }

        public List<ServiceRequest> GetAllRequests()
        {
            return _repository.GetAllRequests();
        }

        public void UpdateRequestStatus(string requestId, string newStatus, string description, string updatedBy = "System")
        {
            var request = _repository.GetRequestById(requestId);
            if (request != null)
            {
                request.Status = newStatus;
                request.StatusUpdates.Add(new StatusUpdate
                {
                    Status = newStatus,
                    Description = description,
                    UpdatedBy = updatedBy
                });

                if (newStatus == "Resolved")
                {
                    request.ResolvedDate = DateTime.Now;
                }

                _repository.UpdateRequest(request);
            }
        }

        public List<ServiceRequest> GetUserRequests(string userId)
        {
            return _repository.GetRequestsByUser(userId);
        }

        // Advanced data structure operations
        public List<ServiceRequest> GetChronologicalRequests()
        {
            return _repository.GetRequestsChronological();
        }

        public List<ServiceRequest> GetHighPriorityRequests()
        {
            return _repository.GetHighPriorityRequests();
        }

        public ServiceRequest GetNextPriorityRequest()
        {
            return _repository.GetNextPriorityRequest();
        }

        public List<string> GetDepartmentNetwork(string department)
        {
            return _repository.GetDepartmentCollaborations(department);
        }

        public List<(string from, string to, double weight)> GetFullDepartmentNetwork()
        {
            return _repository.GetDepartmentNetwork();
        }

        private string AssignDepartment(string category)
        {
            category = category.ToLower();

            if (category.Contains("water") || category.Contains("sewage"))
                return "Water";
            if (category.Contains("road") || category.Contains("street"))
                return "Roads";
            if (category.Contains("electric") || category.Contains("light"))
                return "Electricity";
            if (category.Contains("park") || category.Contains("recreation"))
                return "Parks";
            if (category.Contains("sanitation") || category.Contains("waste"))
                return "Sanitation";

            return "General Services";
        }
    }
}