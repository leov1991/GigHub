using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get future attendance for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                            .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                            .ToList();
        }

        /// <summary>
        /// Get an attendance to a gig for an user
        /// </summary>
        /// <param name="gigId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Attendance GetAttendance(int gigId, string userId)
        {
            return _context.Attendances.SingleOrDefault(a => a.Attendee.Id == userId && a.GigId == gigId);
        }

        /// <summary>
        /// Add an attendance to the collection
        /// </summary>
        /// <param name="attendance"></param>
        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        /// <summary>
        /// Remove an attendance from the collection
        /// </summary>
        /// <param name="attendance"></param>
        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}