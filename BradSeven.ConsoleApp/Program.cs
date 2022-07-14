using BradSeven.Common;
using BradSeven.Data;
using BradSeven.Managers;
using BradSeven.Managers.Interfaces;
using BradSeven.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace BradSeven.ConsoleApp
{
    class Program
    {
        private static string _uri;
        private static int _ageInput;
        private static int _userIdInput;
        private static IUserManager _mgr = new UserManager();

        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
           .AddSingleton<IUserManager, UserManager>()
            .AddSingleton<IUserService, UserService>()
           .BuildServiceProvider();

            _mgr = serviceProvider.GetService<IUserManager>();

            Init(args);
            ShowUsersAsync().GetAwaiter().GetResult();
        }

        private static void Init(string[] args)
        {
            _uri = ConfigurationManager.AppSettings.Get("URI");
            _ageInput = int.Parse(ConfigurationManager.AppSettings.Get("AgeInput"));
            _userIdInput = int.Parse(ConfigurationManager.AppSettings.Get("UserIdInput"));
            //override these if the console app was run from command line with args, fail gracefully but give a message
            if (args.Length > 0)
            {
                if (UriHelper.IsValidURI(args[0]))
                    _uri = args[0];
                else
                    Console.WriteLine("First argument is not a valid URI, using default value of " + _uri);

                if (args.Length == 2)
                {
                    if (!int.TryParse(args[1], out _ageInput))
                    {
                        Console.WriteLine("Second argument AgeInput should be a number, using default value of " + _ageInput);
                    }

                }
                if (args.Length == 3)
                {
                    if (!int.TryParse(args[2], out _userIdInput))
                    {
                        Console.WriteLine("Third argument UserIdInput should be a number, using default value of " + _userIdInput);
                    }
                }
            }
        }

        static async Task ShowUsersAsync()
        {
            List<User> users = await _mgr.LoadUsers(_uri);

            if (users != null)
            {
                //The users full name for id=_userIdInput
                User userWithIdUserIdInput = users.Find(o => o.Id == _userIdInput);
                if (userWithIdUserIdInput != null)
                    Console.WriteLine("User's name with ID=" + _userIdInput + " is: " + userWithIdUserIdInput.Name);
                else
                    Console.WriteLine("User with ID=" + _userIdInput + " does NOT exist..");

                //All the users first names(comma separated) who are _ageInput          
                Console.WriteLine(_mgr.GetFirstNamesAsCSV(users, _ageInput));

                //  The number of genders per Age, displayed from youngest to oldest                    
                var ageStats = _mgr.GetAgeStats(users);
                foreach (var stat in ageStats)
                {
                    Console.WriteLine("Age : " + stat.Age + " Female: " + stat.FemaleCount + " Male: " + stat.MaleCount + " Unknown: " + stat.OtherCount);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            else
                Console.WriteLine("Users list is empty");
        }
    }
}
