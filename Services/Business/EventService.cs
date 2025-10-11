using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp.Services.Business
{
    public class EventService
    {
        // Stack for managing recently viewed events (LIFO)
        private readonly Stack<Event> _recentlyViewedEvents;

        // Queue for processing event notifications (FIFO)
        private readonly Queue<Announcement> _pendingNotifications;

        // Priority Queue implementation using SortedDictionary
        private readonly SortedDictionary<int, Queue<Event>> _priorityEventQueue;

        // Hash tables and dictionaries for fast lookups
        private readonly Dictionary<string, Event> _eventsById;
        private readonly Dictionary<string, List<Event>> _eventsByCategory;
        private readonly SortedDictionary<DateTime, List<Event>> _eventsByDate;

        // Sets for unique collections
        private readonly HashSet<string> _availableCategories;
        private readonly HashSet<string> _availableLocations;
        private readonly HashSet<string> _popularTags;

        // User preferences and search history
        private readonly Dictionary<string, UserSearchPreference> _userPreferences;
        private readonly Dictionary<string, int> _searchFrequency;

        public EventService()
        {
            _recentlyViewedEvents = new Stack<Event>();
            _pendingNotifications = new Queue<Announcement>();
            _priorityEventQueue = new SortedDictionary<int, Queue<Event>>();

            _eventsById = new Dictionary<string, Event>();
            _eventsByCategory = new Dictionary<string, List<Event>>();
            _eventsByDate = new SortedDictionary<DateTime, List<Event>>();

            _availableCategories = new HashSet<string>();
            _availableLocations = new HashSet<string>();
            _popularTags = new HashSet<string>();

            _userPreferences = new Dictionary<string, UserSearchPreference>();
            _searchFrequency = new Dictionary<string, int>();

            InitializeSampleData();
        }

        #region Event Management

        public void AddEvent(Event newEvent)
        {
            if (newEvent == null) throw new ArgumentNullException(nameof(newEvent));

            // Add to main dictionary
            _eventsById[newEvent.Id] = newEvent;

            // Add to category grouping
            if (!_eventsByCategory.ContainsKey(newEvent.Category))
                _eventsByCategory[newEvent.Category] = new List<Event>();
            _eventsByCategory[newEvent.Category].Add(newEvent);

            // Add to date grouping
            var dateKey = newEvent.Date.Date;
            if (!_eventsByDate.ContainsKey(dateKey))
                _eventsByDate[dateKey] = new List<Event>();
            _eventsByDate[dateKey].Add(newEvent);

            // Add to priority queue
            if (!_priorityEventQueue.ContainsKey(newEvent.Priority))
                _priorityEventQueue[newEvent.Priority] = new Queue<Event>();
            _priorityEventQueue[newEvent.Priority].Enqueue(newEvent);

            // Update sets
            _availableCategories.Add(newEvent.Category);
            _availableLocations.Add(newEvent.Location);
            foreach (var tag in newEvent.Tags)
                _popularTags.Add(tag);
        }

        public Event GetEvent(string eventId)
        {
            _eventsById.TryGetValue(eventId, out Event eventItem);

            if (eventItem != null)
            {
                // Add to recently viewed stack
                _recentlyViewedEvents.Push(eventItem);

                // Keep stack size manageable
                if (_recentlyViewedEvents.Count > 10)
                {
                    var tempStack = new Stack<Event>();
                    for (int i = 0; i < 10; i++)
                        tempStack.Push(_recentlyViewedEvents.Pop());

                    _recentlyViewedEvents.Clear();
                    while (tempStack.Count > 0)
                        _recentlyViewedEvents.Push(tempStack.Pop());
                }
            }

            return eventItem;
        }

        public List<Event> GetEventsByCategory(string category)
        {
            _eventsByCategory.TryGetValue(category, out List<Event> events);
            return events ?? new List<Event>();
        }

        public List<Event> GetEventsByDateRange(DateTime startDate, DateTime endDate)
        {
            var events = new List<Event>();

            foreach (var kvp in _eventsByDate)
            {
                if (kvp.Key >= startDate.Date && kvp.Key <= endDate.Date)
                    events.AddRange(kvp.Value);
            }

            return events.OrderBy(e => e.Date).ToList();
        }

        public List<Event> GetUpcomingEvents(int count = 10)
        {
            var upcomingEvents = new List<Event>();
            var today = DateTime.Now.Date;

            foreach (var kvp in _eventsByDate)
            {
                if (kvp.Key >= today)
                {
                    upcomingEvents.AddRange(kvp.Value.Where(e => e.IsActive));
                    if (upcomingEvents.Count >= count) break;
                }
            }

            return upcomingEvents.Take(count).OrderBy(e => e.Date).ToList();
        }

        #endregion

        #region Priority Queue Operations

        public Event GetNextHighPriorityEvent()
        {
            foreach (var priorityLevel in _priorityEventQueue.Keys.OrderBy(k => k))
            {
                var queue = _priorityEventQueue[priorityLevel];
                if (queue.Count > 0)
                    return queue.Dequeue();
            }
            return null;
        }

        public List<Event> GetEventsByPriority(int priority)
        {
            if (_priorityEventQueue.ContainsKey(priority))
                return _priorityEventQueue[priority].ToList();
            return new List<Event>();
        }

        #endregion

        #region Recently Viewed (Stack Operations)

        public List<Event> GetRecentlyViewedEvents()
        {
            return _recentlyViewedEvents.ToList();
        }

        public Event GetLastViewedEvent()
        {
            return _recentlyViewedEvents.Count > 0 ? _recentlyViewedEvents.Peek() : null;
        }

        public void ClearRecentlyViewed()
        {
            _recentlyViewedEvents.Clear();
        }

        #endregion

        #region Notification Queue Operations

        public void QueueNotification(Announcement announcement)
        {
            if (announcement != null)
                _pendingNotifications.Enqueue(announcement);
        }

        public Announcement GetNextNotification()
        {
            return _pendingNotifications.Count > 0 ? _pendingNotifications.Dequeue() : null;
        }

        public int GetPendingNotificationCount()
        {
            return _pendingNotifications.Count;
        }

        public List<Announcement> GetAllPendingNotifications()
        {
            return _pendingNotifications.ToList();
        }

        #endregion

        #region Sets Operations

        public HashSet<string> GetAvailableCategories()
        {
            return new HashSet<string>(_availableCategories);
        }

        public HashSet<string> GetAvailableLocations()
        {
            return new HashSet<string>(_availableLocations);
        }

        public HashSet<string> GetPopularTags()
        {
            return new HashSet<string>(_popularTags);
        }

        public bool HasEventsInCategory(string category)
        {
            return _availableCategories.Contains(category);
        }

        public bool HasEventsInLocation(string location)
        {
            return _availableLocations.Contains(location);
        }

        #endregion

        #region Search and Recommendations

        public List<Event> SearchEvents(string searchTerm, string userId = "default")
        {
            // Track search frequency
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();

                if (!_searchFrequency.ContainsKey(lowerSearchTerm))
                {
                    _searchFrequency[lowerSearchTerm] = 0;
                }
                _searchFrequency[lowerSearchTerm] += 1;
                // Update user search history
                UpdateUserSearchHistory(userId, lowerSearchTerm);
            }

            var results = new List<Event>();
            var searchTermLower = searchTerm?.ToLower() ?? "";

            foreach (var eventItem in _eventsById.Values)
            {
                if (eventItem.IsActive && MatchesSearchTerm(eventItem, searchTermLower))
                    results.Add(eventItem);
            }

            return results.OrderBy(e => e.Date).ToList();
        }

        public List<Event> GetRecommendedEvents(string userId = "default")
        {
            var recommendations = new List<Event>();

            // Get user preferences
            var userPrefs = GetUserPreferences(userId);

            // Get events from preferred categories
            var categoryRecommendations = GetRecommendationsFromCategories(userPrefs.PreferredCategories);
            recommendations.AddRange(categoryRecommendations);

            // Get events from preferred locations
            var locationRecommendations = GetRecommendationsFromLocations(userPrefs.PreferredLocations);
            recommendations.AddRange(locationRecommendations);

            // Get recommendations based on search history
            var searchBasedRecommendations = GetRecommendationsFromSearchHistory(userPrefs.SearchHistory);
            recommendations.AddRange(searchBasedRecommendations);

            // Remove duplicates and return top recommendations
            var uniqueRecommendations = recommendations
                .GroupBy(e => e.Id)
                .Select(g => g.First())
                .Where(e => e.IsActive && e.Date >= DateTime.Now)
                .OrderBy(e => e.Date)
                .Take(10)
                .ToList();

            return uniqueRecommendations;
        }

        public Dictionary<string, int> GetSearchAnalytics()
        {
            return new Dictionary<string, int>(_searchFrequency);
        }

        #endregion

        #region Private Helper Methods

        private void InitializeSampleData()
        {
            var sampleEvents = new List<Event>
            {
                new Event
                {
                    Title = "Community Clean-Up Day",
                    Description = "Join us for a community-wide clean-up initiative to beautify our neighborhoods.",
                    Date = DateTime.Now.AddDays(7),
                    Location = "Central Park, Johannesburg",
                    Category = "Community Service",
                    Priority = 2,
                    Tags = new HashSet<string> { "environment", "community", "volunteer" },
                    OrganizedBy = "City of Johannesburg",
                    MaxCapacity = 100,
                    CurrentAttendees = 15
                },
                new Event
                {
                    Title = "Municipal Budget Meeting",
                    Description = "Public meeting to discuss the upcoming municipal budget and priorities.",
                    Date = DateTime.Now.AddDays(14),
                    Location = "City Hall, Cape Town",
                    Category = "Government",
                    Priority = 1,
                    Tags = new HashSet<string> { "budget", "government", "public", "meeting" },
                    OrganizedBy = "City of Cape Town",
                    MaxCapacity = 200,
                    CurrentAttendees = 45
                },
                new Event
                {
                    Title = "Heritage Day Celebration",
                    Description = "Celebrate South Africa's rich cultural heritage with music, food, and traditional performances.",
                    Date = DateTime.Now.AddDays(21),
                    Location = "Freedom Square, Pretoria",
                    Category = "Cultural",
                    Priority = 3,
                    Tags = new HashSet<string> { "culture", "heritage", "celebration", "music" },
                    OrganizedBy = "Department of Arts and Culture",
                    MaxCapacity = 500,
                    CurrentAttendees = 120
                }
            };

            foreach (var eventItem in sampleEvents)
            {
                AddEvent(eventItem);
            }

            // Add sample notifications
            var sampleNotifications = new List<Announcement>
            {
                new Announcement
                {
                    Title = "Water Supply Maintenance",
                    Content = "Scheduled water supply maintenance in Sandton area from 9 AM to 3 PM on Saturday.",
                    Category = "Infrastructure",
                    Priority = 2,
                    Department = "Water Department",
                    IsUrgent = false,
                    AffectedAreas = new HashSet<string> { "Sandton", "Rosebank" },
                    ExpiryDate = DateTime.Now.AddDays(7)
                },
                new Announcement
                {
                    Title = "Road Closure Notice",
                    Content = "Main Road will be closed for repairs from Monday to Wednesday next week.",
                    Category = "Transportation",
                    Priority = 1,
                    Department = "Roads Department",
                    IsUrgent = true,
                    AffectedAreas = new HashSet<string> { "CBD", "Newtown" },
                    ExpiryDate = DateTime.Now.AddDays(10)
                }
            };

            foreach (var notification in sampleNotifications)
            {
                QueueNotification(notification);
            }
        }

        private bool MatchesSearchTerm(Event eventItem, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return true;

            return eventItem.Title.ToLower().Contains(searchTerm) ||
                   eventItem.Description.ToLower().Contains(searchTerm) ||
                   eventItem.Category.ToLower().Contains(searchTerm) ||
                   eventItem.Location.ToLower().Contains(searchTerm) ||
                   eventItem.Tags.Any(tag => tag.ToLower().Contains(searchTerm));
        }

        private UserSearchPreference GetUserPreferences(string userId)
        {
            if (!_userPreferences.ContainsKey(userId))
            {
                _userPreferences[userId] = new UserSearchPreference { UserId = userId };
            }
            return _userPreferences[userId];
        }

        private void UpdateUserSearchHistory(string userId, string searchTerm)
        {
            var userPrefs = GetUserPreferences(userId);
            if (!userPrefs.SearchHistory.ContainsKey(searchTerm))
            {
                userPrefs.SearchHistory[searchTerm] = 0;
            }
            userPrefs.SearchHistory[searchTerm] += 1;
            userPrefs.LastUpdated = DateTime.Now;
        }

        private List<Event> GetRecommendationsFromCategories(HashSet<string> preferredCategories)
        {
            var recommendations = new List<Event>();
            foreach (var category in preferredCategories)
            {
                recommendations.AddRange(GetEventsByCategory(category));
            }
            return recommendations;
        }

        private List<Event> GetRecommendationsFromLocations(HashSet<string> preferredLocations)
        {
            var recommendations = new List<Event>();
            foreach (var eventItem in _eventsById.Values)
            {
                if (preferredLocations.Any(loc => eventItem.Location.ToLower().Contains(loc.ToLower())))
                {
                    recommendations.Add(eventItem);
                }
            }
            return recommendations;
        }

        private List<Event> GetRecommendationsFromSearchHistory(Dictionary<string, int> searchHistory)
        {
            var recommendations = new List<Event>();
            var topSearchTerms = searchHistory.OrderByDescending(kvp => kvp.Value).Take(5);

            foreach (var searchTerm in topSearchTerms)
            {
                recommendations.AddRange(SearchEvents(searchTerm.Key, ""));
            }

            return recommendations;
        }

        #endregion

        #region Statistics and Analytics

        public Dictionary<string, object> GetEventStatistics()
        {
            var stats = new Dictionary<string, object>
            {
                ["TotalEvents"] = _eventsById.Count,
                ["ActiveEvents"] = _eventsById.Values.Count(e => e.IsActive),
                ["TotalCategories"] = _availableCategories.Count,
                ["TotalLocations"] = _availableLocations.Count,
                ["TotalTags"] = _popularTags.Count,
                ["RecentlyViewedCount"] = _recentlyViewedEvents.Count,
                ["PendingNotifications"] = _pendingNotifications.Count,
                ["MostPopularCategory"] = GetMostPopularCategory(),
                ["MostSearchedTerm"] = GetMostSearchedTerm()
            };

            return stats;
        }

        private string GetMostPopularCategory()
        {
            return _eventsByCategory.OrderByDescending(kvp => kvp.Value.Count).FirstOrDefault().Key ?? "None";
        }

        private string GetMostSearchedTerm()
        {
            return _searchFrequency.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Key ?? "None";
        }

        #endregion
    }
}