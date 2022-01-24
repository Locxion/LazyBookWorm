using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyBookworm.Common.Enums;
using LazyBookworm.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LazyBookworm.Database.Configurations
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("user_accounts");
            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.LastLogin).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.AccountCreation).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.PermissionLevel).IsRequired();
            builder.Property(x => x.IsSuspended).IsRequired();
            builder.OwnsOne(x => x.LoginDetails);
            builder.OwnsOne(x => x.PersonDetails);
        }
    }
}