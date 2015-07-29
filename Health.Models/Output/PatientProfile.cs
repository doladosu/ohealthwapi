using System.Collections.Generic;

namespace Health.Models.Output
{
    public class PatientProfile
    {
        public Patient Patient { get; set; }
        public IEnumerable<Address> Addresses {get; set; }
        public Insurance Insurance { get; set; }
    }
}