using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Models
{
    public class DocumentType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UniqueCode { get; set; }
        [Required]
        public string Type { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual DocumentCategory DocumentCategory { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
