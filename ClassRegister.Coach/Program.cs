using ClassRegister.CoachApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
                Console.WriteLine("Press 1 to Select active course");
                Console.WriteLine("Press 2 to Log out");
                Console.WriteLine("Press 3 to Add attendance");
                Console.WriteLine("Press 0 to Exit");

                int userChoice = _ioHelper.GetIntFromUser("Select option:");

                switch (userChoice)
                {
                    case 1:
                        SelectActiveCourse();
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
            if (_activeCourse == null)
            {
                Console.WriteLine("First select the active course");
                Console.WriteLine();
                return;
            }

            var studentsOnCourse = GetStudents(_activeCourse.Id);
            if (studentsOnCourse.Count == 0)
            {
                Console.WriteLine("No students");
                Console.WriteLine();
                return;
            }

            var date = _ioHelper.GetDateTimeFromUser("Provide the classes date: ");

            foreach (var student in studentsOnCourse)
            {
                Console.WriteLine($"{student.Name} { student.Surname}");

                if (student.Attendances == null)
                {
                    student.Attendances = new List<Attendance>();
                }

                student.Attendances.Add(new Attendance()
                {
                    ClassesDate = date,
                    Status = _ioHelper.GetAttendanceStatus("Enter attendance: 1 - present, 2 - absent, 3 - justified absence")
                });

                UpdateStudentAttendance(student);
            }
        }

        private void UpdateStudentAttendance(Student student)
        {
            var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PutAsync(@"http://localhost:10500/api/students", content).Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Success. Response content: {responseText}");
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                }
            }
        }

        private List<Student> GetStudents(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(@$"http://localhost:10500/api/students/all/{id}").Result;
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
            _activeCourse = null;
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

        private void PrintActiveCourse(int coachId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($@"http://localhost:10500/api/coaches/{coachId}/courses").Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<List<Course>>(responseText);
                    Console.WriteLine("Your active courses:");
                    foreach (var course in responseObject)
                    {
                        _ioHelper.PrintCourse(course);
                    }
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                }
            }
        }

        private void SelectActiveCourse()
        {
            PrintActiveCourse(_loggedCoach.Id);

            var courseId = _ioHelper.GetIntFromUser("Select course id:");

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync($@"http://localhost:10500/api/courses/{courseId}").Result;
                var responseText = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<Course>(responseText);
                    _activeCourse = responseObject;
                }
                else
                {
                    Console.WriteLine($"Failed. Status code: {response.StatusCode}");
                }
            }
        }
    }
}
