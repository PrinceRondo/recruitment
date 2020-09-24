using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class UpdateProfileViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string companyName { get; set; }
        public string phoneNumber { get; set; }
        public string abbreviation { get; set; }
        public long? industryId { get; set; }
        public string industry { get; set; }
        public string headQuarterAddress { get; set; }
        public string address { get; set; }
        public string contactFirstName { get; set; }
        public string contactLastName { get; set; }
        public string contactEmail { get; set; }
        public string contactPhoneNumber { get; set; }
        public bool isActive { get; set; }
        public DateTime? dateUpdated { get; set; }
    }
}
