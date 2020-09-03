using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class RecruitmentLocationViewModel
    {
        public long Id { get; set; }
        public string OrganizationUserId { get; set; }
        public long OrganizationId { get; set; }
        public string Organization { get; set; }
        public string Location { get; set; }
        public bool IsHeadOfficeStructure { get; set; }
        public int RecruitmentLocationTypeId { get; set; }
        public virtual string RecruitmentLocationType { get; set; }
    }
}
