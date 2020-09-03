using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class OrganizationProfileViewModel
    {

        public long id { get; set; }
        public string userId { get; set; }
        [Required]
        public string companyName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string abbreviation { get; set; }
        public long? industryId { get; set; }
        public string industry { get; set; }
        public string headQuarterAddress { get; set; }
        public string address { get; set; }
        public string contactFirstName { get; set; }
        public string contactLastName { get; set; }
        public string contactEmail { get; set; }
        [Required]
        public string contactPhoneNumber { get; set; }
        public bool isActive { get; set; }
        public DateTime? dateCreated { get; set; }
        public DateTime? dateUpdated { get; set; }
        public string role { get; set; }
    }
}
