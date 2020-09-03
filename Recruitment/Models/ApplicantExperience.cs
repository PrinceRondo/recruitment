using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public partial class ApplicantExperience
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public long? IndustryId { get; set; }
        public long? JobRoleId { get; set; }
        public long? JobTypeId { get; set; }
        public string JobDescription { get; set; }
        public string CompanyName { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public Industry Industry { get; set; }
        public JobRoles JobRole { get; set; }
        public JobTypes JobType { get; set; }
    }
}
