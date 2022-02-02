using LazyBookworm.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LazyBookworm.Database.Configurations
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("user_accounts");
            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.ID).IsUnique();
            builder.Property(x => x.LastLogin);
            builder.Property(x => x.AccountCreation).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.PermissionLevel).IsRequired();
            builder.Property(x => x.IsSuspended).IsRequired();
            builder.OwnsOne(x => x.LoginDetails);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Forename).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.Country).IsRequired();
            builder.Property(x => x.MailAddress).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Notes).IsRequired();

            builder.Ignore(x => x.IsSelected);
        }
    }
}