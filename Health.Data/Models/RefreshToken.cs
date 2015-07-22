using System;
using System.ComponentModel.DataAnnotations;

namespace Health.Data.Models
{
    public partial class RefreshToken
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        [Required]
        [StringLength(50)]
        public string ClientId { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }

        [StringLength(50)]
        public string DeviceId { get; set; }
    }
}
