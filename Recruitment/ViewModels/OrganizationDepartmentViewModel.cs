using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class OrganizationDepartmentViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string DepartmentName { get; set; }
        public long OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationUserId { get; set; }
        public long RecruitmentLocationId { get; set; }
        public string RecruitmentLocation { get; set; }
        public bool IsHeadOffice { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
