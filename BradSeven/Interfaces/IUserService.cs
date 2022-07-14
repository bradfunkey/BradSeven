using BradSeven.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BradSeven.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(string path);
    }
}
