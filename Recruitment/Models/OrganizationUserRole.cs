using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Models
{
    public class OrganizationUserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual OrganizationUsersInfo OrganizationUser { get; set; }
        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual OrganizationRoles OrganizationRole { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
