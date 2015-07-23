using System;

namespace Health.Models.Output
{
    public class PatientProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProxyId { get; set; }
        public int Gender { get; set; }
        public DateTime Dob { get; set; }
        public int Race { get; set; }
    }
}
