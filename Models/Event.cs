using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; } // 1 = Critical, 2 = High, 3 = Medium, 4 = Low
        public HashSet<string> Tags { get; set; }
        public string OrganizedBy { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentAttendees { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

        public Event()
        {
            Id = Guid.NewGuid().ToString();
            Tags = new HashSet<string>();
            CreatedDate = DateTime.Now;
            IsActive = true;
            Status = "Scheduled";
        }

        public string GetPriorityText()
        {
            switch (Priority)
            {
                case 1:
                    return "Critical";
                case 2:
                    return "High";
                case 3:
                    return "Medium";
                case 4:
                    return "Low";
                default:
                    return "Unknown";
            }
        }

        public bool HasAvailableSpots()
        {
            return CurrentAttendees < MaxCapacity;
        }

        public int GetAvailableSpots()
        {
            return Math.Max(0, MaxCapacity - CurrentAttendees);
        }
    }

    public class Announcement
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; }
        public string Department { get; set; }
        public bool IsUrgent { get; set; }
        public HashSet<string> AffectedAreas { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }

        public Announcement()
        {
            Id = Guid.NewGuid().ToString();
            PublishedDate = DateTime.Now;
            AffectedAreas = new HashSet<string>();
            Status = "Active";
        }

        public bool IsExpired()
        {
            return DateTime.Now > ExpiryDate;
        }

        public string GetPriorityText()
        {
            switch (Priority)
            {
                case 1:
                    return "Critical";
                case 2:
                    return "High";
                case 3:
                    return "Medium";
                case 4:
                    return "Low";
                default:
                    return "Unknown";
            }
        }
    }

    public class UserSearchPreference
    {
        public string UserId { get; set; }
        public HashSet<string> PreferredCategories { get; set; }
        public HashSet<string> PreferredLocations { get; set; }
        public Dictionary<string, int> SearchHistory { get; set; }
        public DateTime LastUpdated { get; set; }

        public UserSearchPreference()
        {
            PreferredCategories = new HashSet<string>();
            PreferredLocations = new HashSet<string>();
            SearchHistory = new Dictionary<string, int>();
            LastUpdated = DateTime.Now;
        }
    }
}