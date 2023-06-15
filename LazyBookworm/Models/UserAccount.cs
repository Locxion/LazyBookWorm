using LazyBookworm.Common.Enums;
using LazyBookworm.Common.Models.Common;
using System;

namespace LazyBookworm.Common.Models
{
    public class UserAccount : Person
    {
        public Guid ID { get; set; }
        public LoginDetails LoginDetails { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime AccountCreation { get; set; }
        public PermissionLevel PermissionLevel { get; set; }
        public bool IsSuspended { get; set; } = false;
    }
}