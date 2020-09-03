using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class RemoveUserRoleViewModel
    {
        public long ProfileId { get; set; }
        public IEnumerable<long> RoleIdList { get; set; }
    }
}
