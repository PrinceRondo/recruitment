using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class OrganizationRoleViewModel
    {
        public long Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string OrganizationUserId { get; set; }
        [Required]
        public long OrganizationId { get; set; }
        public string Organization { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
