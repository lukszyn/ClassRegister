using ClassRegister.CoachApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Unity;

namespace ClassRegister.CoachApp
{
    public class Program
    {
        private IIoHelper _ioHelper;
        private Coach _loggedCoach = null;
        private Course _activeCourse = null;

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
            do
            {
                var credentials = new Credentials()
                {
                    Email = _ioHelper.GetStringFromUser("Enter your email: "),
                    Password = _ioHelper.GetStringFromUser("Enter your password: "),
                };

                _loggedCoach = LogCoach(credentials);
            }
            while (_loggedCoach == null);

            PrintCoachMenu();
        }

        private void PrintCoachMenu()
        {
            bool exit = false;

            do
            {
                Console.WriteLine("Choose option:");
                Console.WriteLine("Press 1 to ...");
                Console.WriteLine("Press 2 to Log out");
                Console.WriteLine("Press 3 to Add attendance");
                Console.WriteLine("Press 0 to Exit");

                int userChoice = _ioHelper.GetIntFromUser("Select option:");

                switch (userChoice)
                {
                    case 1:
                        break;
                    case 2:
                        LogOut();
                        break;
                    case 3:
                        AddAttendance();
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unknown option");
                        break;
                }
            }
            while (!exit);
        }

        private void AddAttendance()
        {
            if (_activeCourse == null) return;

            var studentsOnCourse = GetStudents(_activeCourse.Id);

            var date = _ioHelper.GetDateTimeFromUser("Provide the classes date: ");

            foreach (var student in studentsOnCourse)
            {
                student.Attendances.Add(new Attendance()
                {
                    ClassesDate = date,
                    Status = _ioHelper.GetAttendanceStatus("Enter attendance: 1 - present, 2 - absent, 3 - justified absence")
                });
            }
        }

        private List<Student> GetStudents(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(@$"http://localhost:10500/api/students/{id}").Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<List<Student>>(responseText);
                    return responseObject;
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }

        private void LogOut()
        {
            _loggedCoach = null;
            Run();
        }

        private Coach LogCoach(Credentials credentials)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(@$"http://localhost:10500/api/coaches/credentials?email=" 
                    + credentials.Email +
                    "&password=" + credentials.Password).Result;
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
