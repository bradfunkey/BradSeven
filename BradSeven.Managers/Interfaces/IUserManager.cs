using BradSeven.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BradSeven.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<List<User>> LoadUsers(string url);
       
        List<User> GetUsersByAge(List<User> users, int age);

        string GetFirstNamesAsCSV(List<User> users);
        string GetFirstNamesAsCSV(List<User> users, int age);

        List<string> GetUsersFirstnames(List<User> users);

        List<AgeStat> GetAgeStats(List<User> users);
    }
}
