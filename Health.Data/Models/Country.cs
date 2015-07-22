using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Data.Models
{
    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            Addresses = new HashSet<Address>();
            States = new HashSet<State>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
