using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void AddGig(Gig gig);

        Gig GetGig(int gigId);

        IEnumerable<Gig> GetGigsUserAttending(string userId);

        Gig GetGigWithAttendees(int gigId);

        IEnumerable<Gig> GetUpcomingGigs();

        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
    }
}