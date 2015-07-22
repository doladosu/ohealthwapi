using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Data.Models
{
    [Table("Race")]
    public partial class Race
    {
        public Race()
        {
            Patients = new HashSet<Patient>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
