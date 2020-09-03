using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Models
{
    public class OrganizationDocument
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string OrganizationUserId { get; set; }
        [ForeignKey("OrganizationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public long OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public virtual OrganizationProfile OrganizationProfile { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
