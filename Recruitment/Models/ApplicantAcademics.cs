using Recruitment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Models
{
    public class ApplicantAcademics
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int QualificationId { get; set; }
        public virtual Qualification Qualification { get; set; }
        public int InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }
        public string InstitutionName { get; set; }
        public bool IsInstitutionVerified { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public string CourseName { get; set; }
        public bool IsCourseVerified { get; set; }
        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }
        public string GradeName { get; set; }
        public bool IsGradeVerified { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
