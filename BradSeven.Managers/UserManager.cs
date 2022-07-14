using BradSeven.Common;
using BradSeven.Data;
using BradSeven.Managers.Interfaces;
using BradSeven.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradSeven.Managers
{
    public class UserManager : IUserManager
    {
        private UserService _service;
        public UserManager()
        {

        }
        public UserManager(IUserService service)
        {
            _service = new UserService();
        }

        public async Task<List<User>> LoadUsers(string url)
        {
            return await _service.GetUsers(url);           
        }              

        public List<User> GetUsersByAge(List<User> users, int age)
        {
            return users?.Where(x => x.Age == age).ToList();
        }

        public List<string> GetUsersFirstnames(List<User> users)
        {
            return users?.Select(x => x.First).ToList();
        }

        public string GetFirstNamesAsCSV(List<User> users)
        {
            return CSVHelper.ListToCSV(GetUsersFirstnames(users));
        }

        public string GetFirstNamesAsCSV(List<User> users, int age)
        {
            users = GetUsersByAge(users, age);
            return GetFirstNamesAsCSV(users);
        }

        public List<AgeStat> GetAgeStats(List<User> users)
        {
            var ageStats = users
                     .GroupBy(l => new { l.Age })
                     .OrderBy(x => x.Key.Age)
                     .Select(u => new AgeStat
                     {
                         Age= u.Key.Age,
                         FemaleCount = u.Where(l => l.Gender.ToLower() == "f").Count(),
                         MaleCount = u.Where(l => l.Gender.ToLower() == "m").Count(),
                         TotalCount = u.Count()
                     })
                     .ToList();

            return ageStats;
        }
    }
}
