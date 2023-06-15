using LazyBookworm.Common.Enums;
using System;

namespace LazyBookworm.Common.Models.Common
{
    public class Person
    {
        public string Name { get; set; }
        public string Forename { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string MailAddress { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
    }
}