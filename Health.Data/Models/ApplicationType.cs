using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Data.Models
{
    [Table("ApplicationType")]
    public partial class ApplicationType
    {
        public ApplicationType()
        {
            Clients = new HashSet<Client>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
