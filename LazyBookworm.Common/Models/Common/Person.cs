using System;
using System.Net.Mail;
using LazyBookworm.Common.Enums;

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
        public MailAddress MailAddress { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
    }
}