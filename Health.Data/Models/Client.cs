using System.ComponentModel.DataAnnotations;

namespace Health.Data.Models
{
    public partial class Client
    {
        public string Id { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int ApplicationType { get; set; }

        public bool Active { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        [StringLength(100)]
        public string AllowedOrigin { get; set; }

        public virtual ApplicationType ApplicationType1 { get; set; }
    }
}
