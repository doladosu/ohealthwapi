using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Data.Models
{
    [Table("Address")]
    public partial class Address
    {
        public Address()
        {
            Patients = new HashSet<Patient>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Street1 { get; set; }

        [Required]
        [StringLength(128)]
        public string Street2 { get; set; }

        [Required]
        [StringLength(128)]
        public string City { get; set; }

        public int StateId { get; set; }

        public int CountryId { get; set; }

        [Required]
        [StringLength(128)]
        public string Zip { get; set; }

        public virtual Country Country { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
