using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public partial class JobRoles
    {
        public JobRoles()
        {
            ApplicantExperience = new HashSet<ApplicantExperience>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long? IndustryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public Industry Industry { get; set; }
        public ICollection<ApplicantExperience> ApplicantExperience { get; set; }
    }
}
