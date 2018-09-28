using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepsoitory : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepsoitory(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetUnreadNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }
    }
}