using MunicipalServicesApp.Data.Interfaces;
using MunicipalServicesApp.Data.Structures;
using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp.Data.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly List<ServiceRequest> _requests;
        private readonly ServiceRequestBST _bst;
        private readonly PriorityAVLTree _avlTree;
        private readonly PriorityHeap _priorityHeap;
        private readonly ServiceDepartmentGraph _departmentGraph;

        public ServiceRequestRepository()
        {
            _requests = new List<ServiceRequest>();
            _bst = new ServiceRequestBST();
            _avlTree = new PriorityAVLTree();
            _priorityHeap = new PriorityHeap();
            _departmentGraph = new ServiceDepartmentGraph();

            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Initialize departments and collaborations
            var departments = new[] { "Water", "Electricity", "Roads", "Sanitation", "Parks" };
            foreach (var dept in departments)
            {
                _departmentGraph.AddDepartment(dept);
            }

            // Add collaborations between departments
            _departmentGraph.AddCollaboration("Water", "Roads", 0.8);
            _departmentGraph.AddCollaboration("Electricity", "Roads", 0.9);
            _departmentGraph.AddCollaboration("Sanitation", "Water", 0.7);
            _departmentGraph.AddCollaboration("Parks", "Sanitation", 0.6);

            // Add sample service requests
            var sampleRequests = new List<ServiceRequest>
            {
                new ServiceRequest
                {
                    Title = "Water Pipe Burst",
                    Description = "Major water pipe burst on Main Street causing flooding",
                    Category = "Water & Sewage",
                    Location = "123 Main Street, Johannesburg",
                    Priority = 1,
                    Status = "InProgress",
                    AssignedDepartment = "Water",
                    EstimatedDuration = 48
                },
                new ServiceRequest
                {
                    Title = "Pothole Repair",
                    Description = "Large pothole on Oak Avenue needs urgent repair",
                    Category = "Roads",
                    Location = "456 Oak Avenue, Cape Town",
                    Priority = 2,
                    Status = "Submitted",
                    AssignedDepartment = "Roads",
                    EstimatedDuration = 24
                },
                new ServiceRequest
                {
                    Title = "Street Light Outage",
                    Description = "Multiple street lights not working in residential area",
                    Category = "Utilities",
                    Location = "789 Pine Road, Durban",
                    Priority = 3,
                    Status = "Resolved",
                    AssignedDepartment = "Electricity",
                    EstimatedDuration = 12
                },
                new ServiceRequest
                {
                    Title = "Park Maintenance",
                    Description = "Playground equipment needs repair in Central Park",
                    Category = "Parks & Recreation",
                    Location = "Central Park, Pretoria",
                    Priority = 4,
                    Status = "Submitted",
                    AssignedDepartment = "Parks",
                    EstimatedDuration = 72
                }
            };

            foreach (var request in sampleRequests)
            {
                AddRequest(request);
            }
        }

        public void AddRequest(ServiceRequest request)
        {
            _requests.Add(request);
            _bst.Insert(request);
            _avlTree.Insert(request);
            _priorityHeap.Enqueue(request);

            // Add status update
            request.StatusUpdates.Add(new StatusUpdate
            {
                Status = request.Status,
                Description = "Request submitted successfully",
                UpdatedBy = "System"
            });
        }

        public ServiceRequest GetRequestById(string id)
        {
            // Search in BST
            var result = _bst.Search(id);
            if (result != null) return result;

            // Fallback to linear search
            return _requests.FirstOrDefault(r => r.Id == id);
        }

        public List<ServiceRequest> GetAllRequests()
        {
            return new List<ServiceRequest>(_requests);
        }

        public void UpdateRequest(ServiceRequest request)
        {
            var existing = _requests.FirstOrDefault(r => r.Id == request.Id);
            if (existing != null)
            {
                var index = _requests.IndexOf(existing);
                _requests[index] = request;
            }
        }

        public List<ServiceRequest> GetRequestsByStatus(string status)
        {
            return _requests.Where(r => r.Status == status).ToList();
        }

        public List<ServiceRequest> GetRequestsByUser(string userId)
        {
            return _requests.Where(r => r.UserId == userId).ToList();
        }

        // Advanced data structure operations
        public List<ServiceRequest> GetRequestsChronological()
        {
            return _bst.InOrderTraversal();
        }

        public List<ServiceRequest> GetHighPriorityRequests()
        {
            return _avlTree.GetHighPriorityRequests();
        }

        public ServiceRequest GetNextPriorityRequest()
        {
            return _priorityHeap.Peek();
        }

        public List<string> GetDepartmentCollaborations(string department)
        {
            return _departmentGraph.BreadthFirstSearch(department);
        }

        public List<(string from, string to, double weight)> GetDepartmentNetwork()
        {
            return _departmentGraph.GetMinimumSpanningTree();
        }
    }
}