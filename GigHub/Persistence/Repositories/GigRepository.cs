using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get gigs logged user is attending.
        /// </summary>
        /// <param name="userId">The logged user id.</param>
        /// <returns></returns>
        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        /// <summary>
        /// Get a gig with its attendees.
        /// </summary>
        /// <param name="gigId">The id of the gig.</param>
        /// <returns></returns>
        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        /// <summary>
        /// Get a gig with its artist and genre.
        /// </summary>
        /// <param name="gigId">The id of the gig.</param>
        /// <returns></returns>
        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                            .Include(g => g.Artist)
                            .Include(g => g.Genre)
                            .SingleOrDefault(g => g.Id == gigId);
        }

        /// <summary>
        /// Get gigs user has created that are not canceled.
        /// </summary>
        /// <param name="userId">The user logged id.</param>
        /// <returns></returns>
        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs
                .Where(g =>
                    g.ArtistId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        /// <summary>
        /// Get all upcoming gigs that are not canceled.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Gig> GetUpcomingGigs()
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
        }

        /// <summary>
        /// Saves a gig to the database.
        /// </summary>
        /// <param name="gig"></param>
        public void AddGig(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}