using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class ApplicantExperienceViewModel
    {
        public long Id { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public long? IndustryId { get; set; }
        public string Industry { get; set; }
        [Required]
        public long? JobRoleId { get; set; }
        public string JobRole { get; set; }
        [Required]
        public long? JobTypeId { get; set; }
        public string JobType { get; set; }
        public string JobDescription { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
