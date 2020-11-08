using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Helper
{
    public class Utility
    {
        public enum level
        {
            Level_0 = 1,
            Level_1 = 2,
            Level_2 = 3, 
            Level_3 = 4,
            Level_4 = 5,
            Level_5 = 6,
            Level_6 = 7,
            Level_7 = 8,
            Level_8 = 9
        }

        public enum RecruitmentLocationType
        {
            Head_Office = 1,
            Branch = 2,
        }

        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
        "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
        "abcdefghijkmnopqrstuvwxyz",    // lowercase
        "0123456789",                   // digits
        "!@$?_-"                        // non-alphanumeric
    };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
    public class UserAccess
    {
        public enum Access
        {
            Create = 1, 
            Read = 2, 
            Update = 3,
            Delete = 4,
            Download = 5
        }
        public enum Function
        {
            Manage_Job_Vacancy = 1,
            Manage_Job_Postings = 2,
            Manage_Interview_Templates = 3,
            Manage_Interview_Rooms = 4,
            Manage_Job_Profiles = 5,
            Process_Applications = 6,
            Manage_Schedules = 7,
            Schedule_Tests_and_Interviews = 8,
            Manage_Reports = 9,
            Review_Job_Postings = 10,
            Review_Job_Profiles = 11,
            Review_Applications = 12,
            Review_Templates = 13,
            Review_Schedules = 14,
            Review_Tests_and_Interviews = 15,
            Approve_Applications = 16,
            Manage_Tasks_and_Events = 17,
            Submit_Interview_Scores = 18,
            Manage_Profile = 19
        }
    }
}
