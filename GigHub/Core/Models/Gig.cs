using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        /// <summary>
        /// Cancel this gig
        /// </summary>
        public void Cancel()
        {
            // Set gig as canceled
            IsCanceled = true;

            // Create a notification for the attendees
            var notification = Notification.GigCanceled(this);

            // Create the user notification for each attendee on the gig's attendances collection
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime newDateTime, string newVenue, byte newGenre)
        {
            // Create a notification and set the original values
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            // Change the values
            Venue = newVenue;
            DateTime = newDateTime;
            GenreId = newGenre;

            // Create the user notification for each attendee on the gig's attendances collection
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}