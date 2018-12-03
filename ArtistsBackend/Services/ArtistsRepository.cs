using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtistsBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtistsBackend.Services
{
    public class ArtistsRepository : IArtistsRepository
    {
        private ArtistsContext _context;
        public ArtistsRepository(ArtistsContext context)
        {
            _context = context;
        }

        public void AddEvent(Event ev)
        {
            _context.Add(ev);
        }

        public bool EventExists(int eventId)
        {
            return _context.Events.Any(ev => ev.Id == eventId);
        }

        public Event GetEvent(int eventId, bool includePerformances)
        {
            if (includePerformances)
            {
                return _context.Events.Include(ev => ev.Address).Include(ev => ev.Performances).
                    ThenInclude(performance => performance.Parts).Where(ev => ev.Id == eventId).FirstOrDefault();
            }
            return _context.Events.Include(ev => ev.Address).Where(ev => ev.Id == eventId).FirstOrDefault();
        }

        public IEnumerable<Event> GetEvents()
        {
            return _context.Events.Include(ev => ev.Address).Include(ev => ev.Performances).
                ThenInclude(performance => performance.Parts).OrderBy(c => c.StartTime).ToList();
        }

        public IEnumerable<Event> GetEventsWithoutPerformances()
        {
            return _context.Events.Include(ev => ev.Address).OrderBy(c => c.StartTime).ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public void DeleteEvent(Event ev)
        {
            _context.Remove(ev);
        }

        public IEnumerable<Performance> GetPerformancesForEvent(int eventId)
        {
            return _context.Performances.Include(p => p.Parts)
                           .Where(p => p.EventId == eventId).ToList();
        }

        public Performance GetPerformanceForEvent(int eventId, int performanceId)
        {
            return _context.Performances
               .Where(p => p.EventId == eventId && p.Id == performanceId).FirstOrDefault();
        }

        public void AddPerformanceForEvent(int eventId, Performance performance)
        {
            var ev = GetEvent(eventId, false);
            ev.Performances.Add(performance);
        }

        public void DeletePerformance(Performance performance)
        {
            _context.Performances.Remove(performance);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
