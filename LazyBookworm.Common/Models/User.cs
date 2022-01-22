using System;
using LazyBookworm.Common.Enums;
using LazyBookworm.Common.Models.Common;

namespace LazyBookworm.Common.Models
{
    internal class User : Person
    {
        public LoginDetails LoginDetails { get; set; }
        public DateTime LastLogin { get; set; }
        public PermissionLevel PermissionLevel { get; set; }
    }
}