using ArtistsBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend.Services
{
    public interface IArtistsRepository
    {
        bool EventExists(int eventId);
        IEnumerable<Event> GetEvents();

        IEnumerable<Event> GetEventsWithoutPerformances();

        IEnumerable<Performance> GetPerformancesForEvent(int eventId);
        Performance GetPerformanceForEvent(int eventId, int performanceId);

        Event GetEvent(int eventId, bool includePerformances);
        void AddEvent(Event ev);
        void AddPerformanceForEvent(int eventId, Performance performance);
        User GetUser(int userId);
        bool Save();
        void DeleteEvent(Event ev);

        void DeletePerformance(Performance performance);
    }
}
