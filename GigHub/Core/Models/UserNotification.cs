﻿using System;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        public string UserId { get; private set; }

        public int NotificationId { get; private set; }

        public bool IsRead { get; private set; }

        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }

        protected UserNotification()
        {
        }

        // Constructor for a new notification
        public UserNotification(ApplicationUser user, Notification notification)
        {
            Notification = notification ?? throw new ArgumentNullException("notification");
            User = user ?? throw new ArgumentNullException("user");
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}