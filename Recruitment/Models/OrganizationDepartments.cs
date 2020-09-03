using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public partial class OrganizationDepartments
    {
        public OrganizationDepartments()
        {
            OrganisationJobRoles = new HashSet<OrganizationJobRoles>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Code { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public long RecruitmentLocationId { get; set; }
        public virtual RecruitmentLocation RecruitmentLocation { get; set; }
        [Required]
        public bool IsHeadOffice { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public ICollection<OrganizationJobRoles> OrganisationJobRoles { get; set; }
    }
}
