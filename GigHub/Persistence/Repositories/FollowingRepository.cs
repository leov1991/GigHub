using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a following between two users
        /// </summary>
        /// <param name="followerId"></param>
        /// <param name="followeeId"></param>
        /// <returns></returns>
        public Following GetFollowing(string followerId, string followeeId)
        {
            return _context.Followings.SingleOrDefault(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
        }

        /// <summary>
        /// Add a following to the collection
        /// </summary>
        /// <param name="following"></param>
        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        /// <summary>
        /// Remove a following from the collection
        /// </summary>
        /// <param name="following"></param>
        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}