using System;
using System.Collections.Generic;

namespace Recruitment.Models
{
    public partial class JobTypes
    {
        public JobTypes()
        {
            ApplicantExperience = new HashSet<ApplicantExperience>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<ApplicantExperience> ApplicantExperience { get; set; }
    }
}
