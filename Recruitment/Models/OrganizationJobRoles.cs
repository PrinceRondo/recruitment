using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public partial class OrganizationJobRoles
    {
        public long Id { get; set; }
        public string JobRoleName { get; set; }
        public long? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public OrganizationDepartments Department { get; set; }
        //public long? OrganizationId { get; set; }
        //public OrganizationProfile Organization { get; set; }
        //public string RoleLevel { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
