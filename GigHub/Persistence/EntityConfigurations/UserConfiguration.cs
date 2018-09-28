using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public UserConfiguration()
        {
            Property(u => u.Name).IsRequired().HasMaxLength(100);

            // An user has many followers which have many followees
            HasMany(u => u.Followers).WithRequired(f => f.Followee).WillCascadeOnDelete(false);

            // An user has many followees wich have many followers
            HasMany(u => u.Followees).WithRequired(f => f.Follower).WillCascadeOnDelete(false);
        }
    }
}