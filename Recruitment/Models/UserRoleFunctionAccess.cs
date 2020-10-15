using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Models
{
    public class UserRoleFunctionAccess
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual OrganizationRoles OrganizationRoles { get; set; }
        [Required]
        public int FunctionId { get; set; }
        [ForeignKey("FunctionId")]
        public virtual UserFunction UserFunction { get; set; }
        [Required]
        public int AccessId { get; set; }
        [ForeignKey("AccessId")]
        public virtual UserAccessType UserAccessType { get; set; }
        //[Required]
        //public long OrganizationId { get; set; }
        //[ForeignKey("OrganizationId")]
        //public virtual OrganizationProfile OrganizationProfile { get; set; }
        //[Required]
        //public string CreatedBy { get; set; }
        //[ForeignKey("CreatedBy")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
