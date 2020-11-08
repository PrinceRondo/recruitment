using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class JobProfileDetailViewModel
    {
        public long Id { get; set; }
        public long JobProfileId { get; set; }
        public string JobProfile { get; set; }
        public int JobElementId { get; set; }
        public string JobProfileElement { get; set; }
        public long OrganizationId { get; set; }
        public string Organization { get; set; }
        public long OrganizationUserRoleId { get; set; }
        public string OrganizationUserRole { get; set; }
        public bool IsMandatory { get; set; }
        public string MinRequirement { get; set; }
        public string MaxRequirement { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
