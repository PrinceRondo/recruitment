using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Models
{
    public class RecruitmentLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string OrganizationUserId { get; set; }
        [ForeignKey("OrganizationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public long OrganizationProfileId { get; set; }
        public virtual OrganizationProfile OrganizationProfile { get; set; }
        public string Location { get; set; }
        public bool IsHeadOfficeStructure { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual RecruitmentLocationType RecruitmentLocationType { get; set; }
    }
}
