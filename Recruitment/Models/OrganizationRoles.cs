using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public partial class OrganizationRoles
    {
        public OrganizationRoles()
        {
            OrganizationUsersInfo = new HashSet<OrganizationUsersInfo>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string OrganizationUserId { get; set; }
        [ForeignKey("OrganizationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public long OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual OrganizationProfile OrganizationProfile { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public ICollection<OrganizationUsersInfo> OrganizationUsersInfo { get; set; }
    }
}
