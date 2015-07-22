using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Data.Models
{
    [Table("Gender")]
    public partial class Gender
    {
        public Gender()
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
