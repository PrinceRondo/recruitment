using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class DeleteViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public long OrganizationUserRoleId { get; set; }
    }
}
