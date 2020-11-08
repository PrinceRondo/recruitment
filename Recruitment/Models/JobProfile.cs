using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Models
{
    public class JobProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual OrganizationProfile Organization { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
