using ClassRegister.CoachApp.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Unity;

namespace ClassRegister.CoachApp
{
    public class Program
    {
        private IIoHelper _ioHelper;
        private readonly Coach _loggedCoach = null;

        public Program(IIoHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        static void Main(string[] args)
        {
            var container = new DIContainerProvider().GetContainer();

            container.Resolve<Program>().Run();
        }



        private void Run()
        {
            bool exit = false;

            do
            {
                Console.WriteLine("Welcome to the trainer app!");
                Console.WriteLine("Choose option:");
                Console.WriteLine("Press 1 to Log In");
                Console.WriteLine("Press 2 to Exit");

                int adminChoice = _ioHelper.GetIntFromUser("Select option:");

                switch (adminChoice)
                {
                    case 1:
                        LogIn();
                        break;
                    case 2:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unknown option");
                        break;
                }
            }
            while (!exit);
        }

        private void LogIn()
        {
            var credentials = new Credentials()
            {
                Email = _ioHelper.GetEmailFromUser("Enter your email: "),
                Password = _ioHelper.GetEmailFromUser("Enter your password: "),
            };

            LogCoach(credentials);
        }

        private Coach LogCoach(Credentials credentials)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(@$"http://localhost:10500/api/students?credentials=" + credentials).Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<Coach>(responseText);
                    return responseObject;
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}
