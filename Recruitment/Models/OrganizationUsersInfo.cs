using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public partial class OrganizationUsersInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        //public string Username { get; set; }
        //public string Password { get; set; }
        
        //public string StaffOrganisationId { get; set; }
        public string PhoneNumber { get; set; }
        public long? OrganisationId { get; set; }
        //public long? OrganisationRoleId { get; set; }
        //public long? IsActivated { get; set; }
        //public DateTime? LastLoginDate { get; set; }
        //public DateTime? LastPasswordChangedDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public OrganizationProfile Organisation { get; set; }
        //public OrganizationRoles OrganisationRole { get; set; }
    }
}
