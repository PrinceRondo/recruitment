using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class ApplicantBiodataViewModel
    {
        public long id { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string OtherName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public long? GenderId { get; set; }
        public string Gender { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public long? MaritalStatusId { get; set; }
        public string MaritalStatus { get; set; }
        [Required]
        public string Address { get; set; }
        public int ApplicantLevelId { get; set; }
        public string ApplicantLevel { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
