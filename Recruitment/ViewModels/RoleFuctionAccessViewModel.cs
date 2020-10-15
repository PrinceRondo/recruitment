using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class RoleFuctionAccessViewModel
    {
        public long Id { get; set; }
        [Required]
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        [Required]
        public int FunctionId { get; set; }
        public string FunctionName { get; set; }
        [Required]
        public int AccessId { get; set; }
        public string AccessType { get; set; }
        public string CreatedBy { get; set; }
        public long OrganizationId { get; set; }
        public string Organization { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
