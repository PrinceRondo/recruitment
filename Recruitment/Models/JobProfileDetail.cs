using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Models
{
    public class JobProfileDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long JobProfileId { get; set; }
        [ForeignKey("JobProfileId")]
        public virtual JobProfile JobProfile { get; set; }
        [Required]
        public int JobElementId { get; set; }
        [ForeignKey("JobElementId")]
        public virtual JobProfileElement JobProfileElement { get; set; }
        [Required]
        public bool IsMandatory { get; set; }
        public string MinRequirement { get; set; }
        public string MaxRequirement { get; set; }
        public string Comment { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
