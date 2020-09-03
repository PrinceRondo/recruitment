using System;
using System.Collections.Generic;

namespace Recruitment.Models
{
    public partial class ApplicantAuthenticationInfo
    {
        public long Id { get; set; }
        public long? ApplicantId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string IsEmailVerified { get; set; }
        public DateTime? IsEmailVerifiedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }

        public ApplicantAuthenticationInfo IdNavigation { get; set; }
        public ApplicantAuthenticationInfo InverseIdNavigation { get; set; }
    }
}
