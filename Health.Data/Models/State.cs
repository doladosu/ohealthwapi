using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Data.Models
{
    [Table("State")]
    public partial class State
    {
        public State()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual Country Country { get; set; }
    }
}
