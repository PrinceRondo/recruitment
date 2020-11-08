using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public partial class OrganizationJobRoles
    {
        public long Id { get; set; }
        [Required]
        public string JobRoleName { get; set; }
        [Required]
        public long? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public OrganizationDepartments Department { get; set; }
        [Required]
        public long JobProfileId { get; set; }
        [ForeignKey("JobProfileId")]
        public virtual JobProfile JobProfile { get; set; }
        //public long? OrganizationId { get; set; }
        //public OrganizationProfile Organization { get; set; }
        //public string RoleLevel { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
