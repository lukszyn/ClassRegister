using System;
using Unity;
using ClassRegister.coach.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClassRegister.coach
{
    class Program
    {
        private IIoHelper _ioHelper;

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


            //PrintActiveCourse(coach);
            
        }

        private void PrintActiveCourse(Coach coach)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($@"http://localhost:10500/api/courses/{coach.Id}").Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<List<Course>>(responseText);
                    Console.WriteLine("Your active courses:");
                    foreach (var course in responseObject)
                    {
                        _ioHelper.PrintCourse(course);
                    }

                    SelectActiveCourse();
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                }
            }
        }

        private void SelectActiveCourse()
        {
            var courseId = _ioHelper.GetIntFromUser("Select course id:");

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($@"http://localhost:10500/api/courses/{courseId}").Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<Course>(responseText);
                    CourseOptions(responseObject);
                }
            }
        }

        private void CourseOptions(Course responseObject)
        {
            Console.WriteLine("Tu będzie więcej opcji. pracujemy nad tym ;)");
        }
    }
}
