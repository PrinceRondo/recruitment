using System;
using System.Collections.Generic;

namespace Recruitment.Models
{
    public partial class Industry
    {
        public Industry()
        {
            JobRoles = new HashSet<JobRoles>();
            OrganisationInfo = new HashSet<OrganizationProfile>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ApplicantExperience ApplicantExperienceInfo { get; set; }
        public ICollection<JobRoles> JobRoles { get; set; }
        public ICollection<OrganizationProfile> OrganisationInfo { get; set; }
    }
}
