using MunicipalServicesApp.Models;
using MunicipalServicesApp.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MunicipalServicesApp.Utilities
{
    /// <summary>
    /// Demonstration class showcasing the advanced data structures implementation
    /// This class demonstrates the key data structures required for Part 2:
    /// - Stacks, Queues, Priority Queues (15 marks)
    /// - Hash Tables, Dictionaries, Sorted Dictionaries (15 marks) 
    /// - Sets (10 marks)
    /// - Recommendation Feature (30 marks)
    /// </summary>
    public class DataStructuresDemo
    {
        private readonly EventService _eventService;

        public DataStructuresDemo()
        {
            _eventService = new EventService();
        }

        /// <summary>
        /// Demonstrates Stack implementation for Recently Viewed Events (LIFO - Last In, First Out)
        /// </summary>
        public string DemonstrateStackOperations()
        {
            var demo = new StringBuilder();
            demo.AppendLine("=== STACK DEMONSTRATION (Recently Viewed Events) ===");
            demo.AppendLine("Stack follows LIFO (Last In, First Out) principle");
            demo.AppendLine();

            // Simulate viewing several events
            var events = _eventService.GetUpcomingEvents(5);

            demo.AppendLine("Viewing events in sequence:");
            foreach (var eventItem in events)
            {
                _eventService.GetEvent(eventItem.Id); // This adds to the stack
                demo.AppendLine($"- Viewed: {eventItem.Title}");
            }

            demo.AppendLine();
            demo.AppendLine("Recently viewed events (Stack - LIFO order):");
            var recentlyViewed = _eventService.GetRecentlyViewedEvents();
            for (int i = 0; i < recentlyViewed.Count; i++)
            {
                demo.AppendLine($"{i + 1}. {recentlyViewed[i].Title} (Last viewed)");
            }

            demo.AppendLine();
            demo.AppendLine("Stack Operations:");
            demo.AppendLine($"- Push: Add event to top of stack when viewed");
            demo.AppendLine($"- Peek: Get last viewed event without removing: {_eventService.GetLastViewedEvent()?.Title ?? "None"}");
            demo.AppendLine($"- Stack Size: {recentlyViewed.Count} events");

            return demo.ToString();
        }

        /// <summary>
        /// Demonstrates Queue implementation for Notification Processing (FIFO - First In, First Out)
        /// </summary>
        public string DemonstrateQueueOperations()
        {
            var demo = new StringBuilder();
            demo.AppendLine("=== QUEUE DEMONSTRATION (Notification Processing) ===");
            demo.AppendLine("Queue follows FIFO (First In, First Out) principle");
            demo.AppendLine();

            // Add some test notifications
            var testNotifications = new List<Announcement>
            {
                new Announcement
                {
                    Title = "Water Maintenance Alert",
                    Content = "Scheduled maintenance in Sandton area",
                    Priority = 2,
                    IsUrgent = false
                },
                new Announcement
                {
                    Title = "Road Closure Notice",
                    Content = "Main Street closed for repairs",
                    Priority = 1,
                    IsUrgent = true
                },
                new Announcement
                {
                    Title = "Community Meeting",
                    Content = "Monthly community meeting this Friday",
                    Priority = 3,
                    IsUrgent = false
                }
            };

            demo.AppendLine("Adding notifications to queue:");
            foreach (var notification in testNotifications)
            {
                _eventService.QueueNotification(notification);
                demo.AppendLine($"- Queued: {notification.Title} (Priority: {notification.Priority})");
            }

            demo.AppendLine();
            demo.AppendLine($"Total notifications in queue: {_eventService.GetPendingNotificationCount()}");

            demo.AppendLine();
            demo.AppendLine("Processing notifications (FIFO order):");
            int count = 1;
            Announcement nextNotification;
            while ((nextNotification = _eventService.GetNextNotification()) != null)
            {
                demo.AppendLine($"{count}. Processing: {nextNotification.Title}");
                count++;
            }

            demo.AppendLine();
            demo.AppendLine("Queue Operations:");
            demo.AppendLine("- Enqueue: Add notification to rear of queue");
            demo.AppendLine("- Dequeue: Remove and return notification from front of queue");
            demo.AppendLine("- First added notification is first to be processed");

            return demo.ToString();
        }

        /// <summary>
        /// Demonstrates Priority Queue implementation using SortedDictionary
        /// </summary>
        public string DemonstratePriorityQueueOperations()
        {
            var demo = new StringBuilder();
            demo.AppendLine("=== PRIORITY QUEUE DEMONSTRATION ===");
            demo.AppendLine("Priority Queue processes items based on priority level (1=Critical, 2=High, 3=Medium, 4=Low)");
            demo.AppendLine();

            // Add events with different priorities
            var priorityEvents = new List<Event>
            {
                new Event
                {
                    Title = "Low Priority Community Cleanup",
                    Priority = 4,
                    Category = "Community Service"
                },
                new Event
                {
                    Title = "Critical: Emergency Town Hall Meeting",
                    Priority = 1,
                    Category = "Government"
                },
                new Event
                {
                    Title = "Medium Priority: Art Festival",
                    Priority = 3,
                    Category = "Cultural"
                },
                new Event
                {
                    Title = "High Priority: Budget Discussion",
                    Priority = 2,
                    Category = "Government"
                }
            };

            demo.AppendLine("Adding events to priority queue:");
            foreach (var eventItem in priorityEvents)
            {
                _eventService.AddEvent(eventItem);
                demo.AppendLine($"- Added: {eventItem.Title} (Priority: {eventItem.Priority} - {eventItem.GetPriorityText()})");
            }

            demo.AppendLine();
            demo.AppendLine("Processing events by priority (Critical first, then High, Medium, Low):");

            Event nextEvent;
            int processCount = 1;
            while ((nextEvent = _eventService.GetNextHighPriorityEvent()) != null && processCount <= 4)
            {
                demo.AppendLine($"{processCount}. {nextEvent.Title} (Priority: {nextEvent.GetPriorityText()})");
                processCount++;
            }

            demo.AppendLine();
            demo.AppendLine("Priority Queue Benefits:");
            demo.AppendLine("- Critical events are always processed first");
            demo.AppendLine("- Efficient for emergency management systems");
            demo.AppendLine("- Maintains order within same priority levels");

            return demo.ToString();
        }

        /// <summary>
        /// Demonstrates Hash Tables, Dictionaries, and Sorted Dictionaries
        /// </summary>
        public string DemonstrateDictionaryOperations()
        {
            var demo = new StringBuilder();
            demo.AppendLine("=== HASH TABLES & DICTIONARIES DEMONSTRATION ===");
            demo.AppendLine();

            // Dictionary operations
            demo.AppendLine("1. HASH TABLE/DICTIONARY (O(1) average lookup time):");
            demo.AppendLine("Used for fast event lookups by ID, category grouping, and user preferences");
            demo.AppendLine();

            var events = _eventService.GetUpcomingEvents(3);
            if (events.Count > 0)
            {
                var firstEvent = events[0];
                demo.AppendLine($"Fast lookup test - Finding event by ID: {firstEvent.Id.Substring(0, 8)}...");
                var foundEvent = _eventService.GetEvent(firstEvent.Id);
                demo.AppendLine($"Result: {(foundEvent != null ? "✓ Found: " + foundEvent.Title : "✗ Not found")}");
            }

            demo.AppendLine();
            demo.AppendLine("2. CATEGORY GROUPING (Dictionary<string, List<Event>>):");
            var categories = _eventService.GetAvailableCategories();
            foreach (var category in categories.Take(3))
            {
                var categoryEvents = _eventService.GetEventsByCategory(category);
                demo.AppendLine($"- {category}: {categoryEvents.Count} events");
            }

            demo.AppendLine();
            demo.AppendLine("3. SORTED DICTIONARY (Maintains ordered keys):");
            demo.AppendLine("Used for date-based event organization");
            var dateRangeEvents = _eventService.GetEventsByDateRange(DateTime.Now, DateTime.Now.AddDays(30));
            demo.AppendLine($"Events in next 30 days (sorted by date): {dateRangeEvents.Count}");

            foreach (var eventItem in dateRangeEvents.Take(3))
            {
                demo.AppendLine($"- {eventItem.Date:MMM dd}: {eventItem.Title}");
            }

            demo.AppendLine();
            demo.AppendLine("Dictionary Benefits:");
            demo.AppendLine("- O(1) average case lookup time");
            demo.AppendLine("- Efficient key-value storage");
            demo.AppendLine("- SortedDictionary maintains key ordering");

            return demo.ToString();
        }

        /// <summary>
        /// Demonstrates Set operations for unique collections
        /// </summary>
        public string DemonstrateSetOperations()
        {
            var demo = new StringBuilder();
            demo.AppendLine("=== SET OPERATIONS DEMONSTRATION ===");
            demo.AppendLine("Sets store unique elements and provide efficient set operations");
            demo.AppendLine();

            // Available categories set
            var categories = _eventService.GetAvailableCategories();
            demo.AppendLine($"1. UNIQUE CATEGORIES SET (HashSet<string>):");
            demo.AppendLine($"Total unique categories: {categories.Count}");
            demo.AppendLine($"Categories: {string.Join(", ", categories.Take(5))}");

            demo.AppendLine();
            demo.AppendLine("Set Operations:");
            demo.AppendLine($"- Contains 'Government': {categories.Contains("Government")}");
            demo.AppendLine($"- Contains 'NonExistent': {categories.Contains("NonExistent")}");

            // Available locations set
            var locations = _eventService.GetAvailableLocations();
            demo.AppendLine();
            demo.AppendLine($"2. UNIQUE LOCATIONS SET (HashSet<string>):");
            demo.AppendLine($"Total unique locations: {locations.Count}");
            demo.AppendLine($"Locations: {string.Join(", ", locations.Take(3))}");

            // Popular tags set
            var tags = _eventService.GetPopularTags();
            demo.AppendLine();
            demo.AppendLine($"3. POPULAR TAGS SET (HashSet<string>):");
            demo.AppendLine($"Total unique tags: {tags.Count}");
            demo.AppendLine($"Tags: {string.Join(", ", tags.Take(5))}");

            // Set intersection example
            demo.AppendLine();
            demo.AppendLine("4. SET INTERSECTION EXAMPLE:");
            var userPreferredCategories = new HashSet<string> { "Government", "Community Service", "Cultural" };
            var availablePreferred = categories.Intersect(userPreferredCategories).ToHashSet();
            demo.AppendLine($"User preferred categories: {string.Join(", ", userPreferredCategories)}");
            demo.AppendLine($"Available preferred categories: {string.Join(", ", availablePreferred)}");

            demo.AppendLine();
            demo.AppendLine("Set Benefits:");
            demo.AppendLine("- O(1) average case for add, remove, contains operations");
            demo.AppendLine("- Automatically handles uniqueness");
            demo.AppendLine("- Efficient set operations (union, intersection, difference)");
            demo.AppendLine("- Used for tags, categories, locations, and user preferences");

            return demo.ToString();
        }

        /// <summary>
        /// Demonstrates the recommendation algorithm using multiple data structures
        /// </summary>
        public string DemonstrateRecommendationSystem()
        {
            var demo = new StringBuilder();
            demo.AppendLine("=== RECOMMENDATION SYSTEM DEMONSTRATION ===");
            demo.AppendLine("Combines multiple data structures for intelligent recommendations");
            demo.AppendLine();

            // Simulate user activity
            demo.AppendLine("1. SIMULATING USER ACTIVITY:");

            // Search activity
            var searchTerms = new[] { "community", "government", "cultural", "meeting" };
            foreach (var term in searchTerms)
            {
                _eventService.SearchEvents(term, "demo_user");
                demo.AppendLine($"- Searched for: '{term}'");
            }

            // View some events
            var upcomingEvents = _eventService.GetUpcomingEvents(3);
            foreach (var eventItem in upcomingEvents)
            {
                _eventService.GetEvent(eventItem.Id);
                demo.AppendLine($"- Viewed: {eventItem.Title}");
            }

            demo.AppendLine();
            demo.AppendLine("2. GENERATING RECOMMENDATIONS:");
            var recommendations = _eventService.GetRecommendedEvents("demo_user");
            demo.AppendLine($"Total recommendations generated: {recommendations.Count}");

            demo.AppendLine();
            demo.AppendLine("Top 5 Recommendations:");
            foreach (var rec in recommendations.Take(5))
            {
                demo.AppendLine($"- {rec.Title} ({rec.Category}) - {rec.Date:MMM dd}");
            }

            demo.AppendLine();
            demo.AppendLine("3. SEARCH ANALYTICS:");
            var searchAnalytics = _eventService.GetSearchAnalytics();
            demo.AppendLine("Most searched terms:");
            foreach (var search in searchAnalytics.OrderByDescending(kvp => kvp.Value).Take(3))
            {
                demo.AppendLine($"- '{search.Key}': {search.Value} searches");
            }

            demo.AppendLine();
            demo.AppendLine("4. RECOMMENDATION ALGORITHM DATA STRUCTURES:");
            demo.AppendLine("- HashSet<string>: User preferred categories");
            demo.AppendLine("- HashSet<string>: User preferred locations");
            demo.AppendLine("- Dictionary<string, int>: Search history with frequency");
            demo.AppendLine("- Dictionary<string, Event>: Fast event lookups");
            demo.AppendLine("- Dictionary<string, List<Event>>: Category-based grouping");
            demo.AppendLine("- Stack<Event>: Recently viewed events");

            demo.AppendLine();
            demo.AppendLine("RECOMMENDATION BENEFITS:");
            demo.AppendLine("- Analyzes user search patterns and preferences");
            demo.AppendLine("- Uses frequency-based ranking");
            demo.AppendLine("- Combines category, location, and search-based recommendations");
            demo.AppendLine("- Provides personalized experience");
            demo.AppendLine("- Efficient O(1) lookups for real-time recommendations");

            return demo.ToString();
        }

        /// <summary>
        /// Demonstrates all data structures working together
        /// </summary>
        public string DemonstrateIntegratedSystem()
        {
            var demo = new StringBuilder();
            demo.AppendLine("=== INTEGRATED SYSTEM DEMONSTRATION ===");
            demo.AppendLine("All data structures working together in the Municipal Services Application");
            demo.AppendLine();

            var stats = _eventService.GetEventStatistics();

            demo.AppendLine("SYSTEM STATISTICS:");
            demo.AppendLine($"Total Events: {stats["TotalEvents"]}");
            demo.AppendLine($"Active Events: {stats["ActiveEvents"]}");
            demo.AppendLine($"Categories (Set): {stats["TotalCategories"]}");
            demo.AppendLine($"Locations (Set): {stats["TotalLocations"]}");
            demo.AppendLine($"Tags (Set): {stats["TotalTags"]}");
            demo.AppendLine($"Recently Viewed (Stack): {stats["RecentlyViewedCount"]}");
            demo.AppendLine($"Pending Notifications (Queue): {stats["PendingNotifications"]}");

            demo.AppendLine();
            demo.AppendLine("DATA STRUCTURE IMPLEMENTATION SUMMARY:");
            demo.AppendLine();

            demo.AppendLine("✓ STACKS (LIFO): Recently viewed events tracking");
            demo.AppendLine("  - Push: Add viewed event to top");
            demo.AppendLine("  - Pop: Remove most recent event");
            demo.AppendLine("  - Peek: View most recent without removal");

            demo.AppendLine();
            demo.AppendLine("✓ QUEUES (FIFO): Notification processing");
            demo.AppendLine("  - Enqueue: Add notification to rear");
            demo.AppendLine("  - Dequeue: Remove notification from front");
            demo.AppendLine("  - First added is first processed");

            demo.AppendLine();
            demo.AppendLine("✓ PRIORITY QUEUES: Event importance management");
            demo.AppendLine("  - SortedDictionary<int, Queue<Event>>");
            demo.AppendLine("  - Critical events processed first");
            demo.AppendLine("  - Maintains order within priority levels");

            demo.AppendLine();
            demo.AppendLine("✓ HASH TABLES/DICTIONARIES: Fast lookups");
            demo.AppendLine("  - Dictionary<string, Event>: Event ID to event mapping");
            demo.AppendLine("  - Dictionary<string, List<Event>>: Category grouping");
            demo.AppendLine("  - Dictionary<string, int>: Search frequency tracking");

            demo.AppendLine();
            demo.AppendLine("✓ SORTED DICTIONARIES: Ordered data");
            demo.AppendLine("  - SortedDictionary<DateTime, List<Event>>: Date-ordered events");
            demo.AppendLine("  - Maintains chronological order automatically");

            demo.AppendLine();
            demo.AppendLine("✓ SETS: Unique collections");
            demo.AppendLine("  - HashSet<string>: Categories, locations, tags");
            demo.AppendLine("  - Automatic uniqueness enforcement");
            demo.AppendLine("  - Efficient set operations");

            demo.AppendLine();
            demo.AppendLine("✓ RECOMMENDATION SYSTEM: AI-like intelligence");
            demo.AppendLine("  - Analyzes user behavior patterns");
            demo.AppendLine("  - Uses multiple data structures for decisions");
            demo.AppendLine("  - Provides personalized suggestions");

            return demo.ToString();
        }

        /// <summary>
        /// Generates a comprehensive report of all demonstrations
        /// </summary>
        public string GenerateFullReport()
        {
            var report = new StringBuilder();

            report.AppendLine("MUNICIPAL SERVICES APPLICATION - PART 2");
            report.AppendLine("ADVANCED DATA STRUCTURES IMPLEMENTATION REPORT");
            report.AppendLine("=" + new string('=', 60));
            report.AppendLine();

            report.AppendLine(DemonstrateStackOperations());
            report.AppendLine();

            report.AppendLine(DemonstrateQueueOperations());
            report.AppendLine();

            report.AppendLine(DemonstratePriorityQueueOperations());
            report.AppendLine();

            report.AppendLine(DemonstrateDictionaryOperations());
            report.AppendLine();

            report.AppendLine(DemonstrateSetOperations());
            report.AppendLine();

            report.AppendLine(DemonstrateRecommendationSystem());
            report.AppendLine();

            report.AppendLine(DemonstrateIntegratedSystem());

            return report.ToString();
        }
    }
}