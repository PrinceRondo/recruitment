using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class OrganizationUserRoleViewModel
    {
        public long Id { get; set; }
        public long ProfileId { get; set; }
        public long RoleId { get; set; }
        public IEnumerable<long> RoleIdList { get; set; }
        public string RoleName { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
