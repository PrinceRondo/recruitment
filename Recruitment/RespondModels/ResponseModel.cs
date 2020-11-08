using Recruitment.Models;
using Recruitment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.RespondModels
{
    public class ResponseModel
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class JobRoleResponseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public long Id { get; set; }
        public string JobRole { get; set; }
        public long DepartmentId { get; set; }
        public string Department { get; set; }
        public long? JobProfileId { get; set; }
        public string JobProfile { get; set; }
        public IList<JobProfileDetailViewModel> JobProfileDetails { get; set; }
    }
}
