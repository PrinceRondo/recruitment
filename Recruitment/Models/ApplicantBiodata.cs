using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public partial class ApplicantBiodata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public long? GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long? MaritalStatusId { get; set; }
        public string Address { get; set; }
        public int ApplicantLevelId { get; set; }
        public virtual ApplicantLevel ApplicantLevel { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
    }
}
