using BradSeven.Data;
using BradSeven.Service.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BradSeven
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }
        public async Task<List<User>> GetUsers(string path)
        {
            var list = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(path))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            return list;
        }
    }
}
