using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class JobProfileViewModel
    {
        public long Id { get; set; }
        [Required]
        public long OrganizationUserRoleId { get; set; }
        public string OrganizationUserRole { get; set; }
        public long OrganizationId { get; set; }
        public string Organization { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
