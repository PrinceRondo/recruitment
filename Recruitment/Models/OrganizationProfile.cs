using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public partial class OrganizationProfile
    {
        public OrganizationProfile()
        {
            OrganizationUsersInfo = new HashSet<OrganizationUsersInfo>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Abbreviation { get; set; }
        public long? IndustryId { get; set; }
        public string HeadQuarterAddress { get; set; }
        public string Address { get; set; }
        [Required]
        public string ContactFirstName { get; set; }
        [Required]
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        [Required]
        public string ContactPhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public Industry Industry { get; set; }
        public OrganizationJobRoles OrganizationJobRoles { get; set; }
        public ICollection<OrganizationUsersInfo> OrganizationUsersInfo { get; set; }
    }
}
