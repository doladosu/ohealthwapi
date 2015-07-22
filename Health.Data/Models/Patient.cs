using System;
using System.ComponentModel.DataAnnotations;

namespace Health.Data.Models
{
    public partial class Patient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public int GenderId { get; set; }

        public int RaceId { get; set; }

        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Race Race { get; set; }
    }
}
