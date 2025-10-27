// ServiceRequest.cs - Updated
using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Models
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public string Status { get; set; } // Submitted, InProgress, Resolved, Closed
        public int Priority { get; set; } // 1=Critical, 2=High, 3=Medium, 4=Low
        public string AssignedDepartment { get; set; }
        public List<StatusUpdate> StatusUpdates { get; set; }
        public double EstimatedDuration { get; set; } // in hours

        public ServiceRequest()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
            Status = "Submitted";
            StatusUpdates = new List<StatusUpdate>();
            Priority = 3; // Default to Medium
        }

        public string GetPriorityText()
        {
            switch (Priority)
            {
                case 1: return "Critical";
                case 2: return "High";
                case 3: return "Medium";
                case 4: return "Low";
                default: return "Unknown";
            }
        }

        public int CompareTo(ServiceRequest other)
        {
            // Compare by priority first, then by creation date
            if (Priority != other.Priority)
                return Priority.CompareTo(other.Priority);

            return CreatedDate.CompareTo(other.CreatedDate);
        }
    }

    public class StatusUpdate
    {
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string UpdatedBy { get; set; }

        public StatusUpdate()
        {
            Timestamp = DateTime.Now;
            UpdatedBy = "System";
        }
    }
}