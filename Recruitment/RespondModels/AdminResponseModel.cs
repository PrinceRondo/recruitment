using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.RespondModels
{
    public class AdminResponseModel
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }

    }
}
