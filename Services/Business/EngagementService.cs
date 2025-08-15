using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Services.Business
{
    public class EngagementService
    {
        private readonly Random _random;
        private int _currentEngagementLevel;

        public EngagementService()
        {
            _random = new Random();
            _currentEngagementLevel = 25;
        }

        public EngagementData UpdateEngagement()
        {
            int increment = _random.Next(5, 15);
            _currentEngagementLevel = Math.Min(_currentEngagementLevel + increment, 100);

            return new EngagementData
            {
                Level = _currentEngagementLevel,
                Message = GetEngagementMessage(_currentEngagementLevel)
            };
        }

        public EngagementData GetCurrentEngagement()
        {
            return new EngagementData
            {
                Level = _currentEngagementLevel,
                Message = GetEngagementMessage(_currentEngagementLevel)
            };
        }

        private string GetEngagementMessage(int level)
        {
            if (level < 30) return "Community Engagement Level: Building...";
            if (level < 60) return "Community Engagement Level: Growing...";
            if (level < 90) return "Community Engagement Level: Strong!";
            return "Community Engagement Level: Excellent!";
        }
    }
}