using System;

namespace Health.Models.Output
{
    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProxyId { get; set; }
        public int Gender { get; set; }
        public DateTime Dob { get; set; }
        public int Race { get; set; }
        public int Id { get; set; }
    }
}