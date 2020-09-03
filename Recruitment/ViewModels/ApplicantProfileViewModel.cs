using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModel
{
    public class ApplicantProfileViewModel
    {
        public long id { get; set; }
        public string userId { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string address { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public bool isActive { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
        public string role { get; set; }
    }
}
