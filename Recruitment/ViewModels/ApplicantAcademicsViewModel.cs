using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class ApplicantAcademicsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public int QualificationId { get; set; }
        public string QualificationName { get; set; }
        public int InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public bool IsInstitutionVerified { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public bool IsCourseVerified { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public bool IsGradeVerified { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
