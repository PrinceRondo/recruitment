using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class OrganizationJobeRoleViewModel
    {
        public long Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public long? DepartmentId { get; set; }
        public string Department { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
